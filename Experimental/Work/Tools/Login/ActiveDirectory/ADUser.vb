Imports System.DirectoryServices.ActiveDirectory, System.DirectoryServices
Imports System.Security.Principal, System.Threading
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace Login
    ''' <summary>Reprezentuje uživatele z ActiveDirectory propojeného na tabulku LoginUser</summary>
    Public Class ADUser : Implements IDisposable, IUser
#Region "Shared"
        ''' <summary>Instance tøídy. Mùže být jen jedna živá instance</summary>
        Private Shared Instance As ADUser
        ''' <summary>Název aplikace, pokud byl v <see cref="Login"/> zmìnìn, jinak null nebo <see cref="String.Empty"/></summary>
        Private _ApplicationTitle As String
        ''' <summary>Vrací název aplikace (standartnì My.Application.Info.Title, pokud nebyl v <see cref="Login"/> zmìnìn)</summary>
        Private ReadOnly Property AppTitle() As String
            Get
                If _ApplicationTitle Is Nothing OrElse _ApplicationTitle = "" Then
                    Return My.Application.Info.Title
                Else
                    Return _ApplicationTitle
                End If
            End Get
        End Property
        ''' <summary>Zaloguje uživatele a vrátí jeho instanci</summary>
        ''' <param name="AllowInteractive">Povolit interaktivní pøihlášení</param>
        ''' <param name="AllowTray">Povolit SysTray ikonu</param>
        ''' <param name="DisplayAppName">Zobrazovaný název aplikace, pokud má být jiný než My.Application.Info.Title</param>
        ''' <returns>Zalogovaný uživatel, nebo null, pokud zalogování selhalo</returns>
        ''' <exception cref="UserNotFoundException">Uživatele se nepodaøilo ovìøit v ActiveDirectory a <paramref name="AllowInteractive"/> je false =nebo= Nepodaøilo se ovìøit uživatele vùèi MySQL a <paramref name="AllowInteractive"/> je false</exception>
        ''' <exception cref="KeyNotFoundException">Object ActiveDirectory neposkytl požadovanou vlastnost (userPrincipalName; nebo Name, givenName, sn, sAMAcountName pøi vytváøení nového uživatele) a <paramref name="AllowInteractive"/> je false</exception>
        <Obsolete("Použijte tøídu OracleADUser")> _
        Public Shared Function Login(Optional ByVal AllowInteractive As Boolean = True, Optional ByVal AllowTray As Boolean = True, Optional ByVal DisplayAppName As String = Nothing) As ADUser
            If Instance Is Nothing OrElse Instance.IsDisposed Then
                Instance = Nothing
                Try
                    Instance = New ADUser(AllowInteractive, AllowTray, DisplayAppName)
                Catch ex As UserNotFoundException
                    If AllowInteractive Then
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "EOS Login - Chyba")
                    Else : Throw : End If
                Catch ex As KeyNotFoundException
                    If AllowInteractive Then
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "EOS Login - Chyba")
                    Else : Throw : End If
                Catch ex As OperationCanceledException
                End Try
            End If
            Return Instance
        End Function
        ''' <summary>Inicializátor - nastaví <see cref="AppDomain.CurrentDomain">AppDomain.CurrentDomain</see>.<see cref="AppDomain.SetPrincipalPolicy">SetPrincipalPolicy</see> na <see cref="PrincipalPolicy.WindowsPrincipal"/></summary>
        Shared Sub New()
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal)
        End Sub
        ''' <summary>Vrací jedinou instanci <see cref="ADUser"/>, která je v rámci palikace</summary>
        ''' <returns>Instance nebo null, pokud žádná instance neexistuje, nebo je disposed</returns>
        <Obsolete("Použijte tøídu OracleADUser")> _
        Public Shared ReadOnly Property [Default]() As ADUser
            Get
                If Instance Is Nothing OrElse Instance.IsDisposed Then Return Nothing
                Return Instance
            End Get
        End Property

#End Region
        ''' <summary>Id uživatele v tabulce LoginUser na MySQL</summary>
        Private _Id As Integer
        ''' <summary>Skupiny, které vstupují v platnost jako skupiny pro uživatele</summary>
        Private GroupOverride As New List(Of String)
        ''' <summary>Uživatel z active directory</summary>
        Private ADUser As DirectoryEntry
        ''' <summary>SysTray ikona</summary>
        Private WithEvents _TrayIcon As NotifyIcon
        Public Property TrayIcon() As NotifyIcon
            Get
                Return _TrayIcon
            End Get
            Private Set(ByVal value As NotifyIcon)
                If value IsNot TrayIcon AndAlso value IsNot Nothing Then
                    Dim menu As New ContextMenuStrip
                    value.ContextMenuStrip = menu
                    'menu.Items.Add("Nástroje ...", Nothing, AddressOf Me.TrayIcon_DoubleClick)
                    'With menu.Items(menu.Items.Count - 1)
                    '    .Name = tmiTools
                    '    .Font = New Font(.Font, .Font.Style Or FontStyle.Bold)
                    'End With
                End If
                _TrayIcon = value
            End Set
        End Property
        Private Const tmiTools$ = "tmiTools"
        ''' <summary>CTor</summary>
        ''' <param name="AllowInteractive">Povoluje interaktivní možnosti pøihlášení: Pokud uživatel má zaplý CapsLock a drží Shif pøihlá sí jej pod jinými právy; pokud nelze uživatele pøihlásit pod právy OS vyzve jej k zadání hesla; pokud se nepodaøí uživatele napárovat na MySQL vyzve jej k vytvoøení páru nebo k vytvoøení nového konta.</param>
        ''' <param name="AllowTray">Povolí zobrazit ikonu EOS login v SysTray</param>
        ''' <param name="AppDisplayName">Alternativní zobrazované jméno aplikace</param>
        ''' <exception cref="UserNotFoundException">Uživatele se nepodaøilo ovìøit v ActiveDirectory =nebo= Nepodaøilo se ovìøit uživatele vùèi MySQL a <paramref name="AllowInteractive"/> je false</exception>
        ''' <exception cref="OperationCanceledException">Uživatel pøihlášení stornoval nebo stronoval nìkterou z dílèích operací (Uživatel stornoval pøihlašování ke staré databázi =nebo= uživatel stornoval dozat na propojení starého úètu s ActiveDirectory) (pøi interaktivním pøihlášení; uživatel o tom ví, že nìco stornoval, není potøeba jej informovat)</exception>
        ''' <exception cref="KeyNotFoundException">Object ActiveDirectory neposkytl požadovanou vlastnost (userPrincipalName; nebo Name, givenName, sn, sAMAcountName pøi vytváøení nového uživatele)</exception>
        Private Sub New(Optional ByVal AllowInteractive As Boolean = True, Optional ByVal AllowTray As Boolean = True, Optional ByVal AppDisplayName As String = "")
            Me._ApplicationTitle = AppDisplayName
            If AllowInteractive AndAlso (My.Computer.Keyboard.CapsLock AndAlso My.Computer.Keyboard.ShiftKeyDown) Then
                LoginInteractive()
                If My.Computer.Keyboard.CapsLock AndAlso My.Computer.Keyboard.ShiftKeyDown Then
                    Dim frm As New frmGroupOverride(ADHelpers.GetGroups(ADUser))
                    If frm.ShowDialog() = DialogResult.OK Then
                        For Each Item As String In frm.clbGroups.CheckedItems
                            GroupOverride.Add(Item)
                        Next Item
                        If AllowTray Then SysTray()
                        Return
                    End If
                End If
            Else
                Try
                    LoginInternal(AllowInteractive)
                Catch ex As UserNotFoundException
                    If Not AllowInteractive Then Throw
                    If MsgBox("Nepodaøilo se vás ovìøit vùèi ActiveDirectory. Chcete se pøihlásit ruènì?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "EOS Login @ " & My.Application.Info.Title) = MsgBoxResult.Yes Then
                        LoginInteractive()
                    Else
                        Throw New OperationCanceledException("Uživatel si nepøál interaktivní pøihlašování po té co automatické selhalo.", ex)
                    End If
                End Try
            End If
            GroupOverride = ADHelpers.GetGroups(ADUser)
            If AllowTray Then SysTray()
        End Sub
        ''' <summary>Zobrazí ikonu v SysTray</summary>
        Private Sub SysTray()
            TrayIcon = New NotifyIcon()
            'TrayIcon.Icon = My.Resources.EOS
            TrayIcon.Text = "Login @ " & Me.AppTitle
            TrayIcon.Visible = True
            Try
                TrayIcon.BalloonTipIcon = ToolTipIcon.Info
                TrayIcon.BalloonTipText = "Pøihlášen jako " & CStr(ADUser.Properties!displayName(0))
                TrayIcon.BalloonTipTitle = "Login @ " & Me.AppTitle
                TrayIcon.ShowBalloonTip(15000)
            Catch : End Try
        End Sub
        ''' <summary>Interaktivní pøihlášení k ActiveDirectory</summary>
        ''' <exception cref="OperationCanceledException">Uživatel pøihlášení stornoval nebo stronoval nìkterou z dílèích operací (Uživatel stornoval pøihlašování ke staré databázi =nebo= uživatel stornoval dozat na propojení starého úètu s ActiveDirectory)</exception>
        ''' <exception cref="UserNotFoundException">Uživatele se nepodaøilo ovìøit v ActiveDirectory</exception>
        ''' <exception cref="KeyNotFoundException">Object ActiveDirectory neposkytl požadovanou vlastnost (userPrincipalName; nebo Name, givenName, sn, sAMAcountName pøi vytváøení nového uživatele)</exception>
        ''' <exception cref="COMException">Chyba Active Directory</exception>
        Private Sub LoginInteractive()
            Dim ld As New LoginDialog
            ld.AdditionalButton = LoginButtonState.Hidden
            ld.Text = "ActiveDirectory login @ " & Me.AppTitle
            ld.UserName = Environment.UserName
            Try
                AddHandler ld.OKClick, AddressOf InteractiveClick
                If ld.ShowDialog() = DialogResult.OK Then
                    Dim u As SearchResult = GetUser(ld.UserName, ld.Password)
                    If u Is Nothing Then Throw New UserNotFoundException("Uživatel nebyl v ActiveDirectory nalezen nebo heslo je špané")
                    'VerifyInMySQL(u)
                Else
                    Throw New OperationCanceledException("Uživatel stronoval interaktivní pøihlašování.")
                End If
            Finally
                ld.Dispose()
            End Try
        End Sub



        '        ''' <summary>Ovìøí uživatele z ActiveDirectory proti MySQL (EOS logi, LoginUser)</summary>
        '        ''' <param name="ADUser">Výsledek hledání uživatele v ActiveDirectory</param>
        '        ''' <param name="AllowInteractive">Povolení interaktivního režimu</param>
        '        ''' <exception cref="UserNotFoundException">Nepodaøilo se ovìøit uživatele vùèi MySQL a <paramref name="AllowInteractive"/> je false</exception>
        '        ''' <exception cref="KeyNotFoundException">Object ActiveDirectory neposkytl požadovanou vlastnost (userPrincipalName; nebo Name, givenName, sn, sAMAcountName pøi vytváøení nového uživatele)</exception>
        '        ''' <exception cref="OperationCanceledException">Uživatel stornoval pøihlašování ke staré databázi =nebo= uživatel stornoval dozat na propojení starého úètu s ActiveDirectory</exception>
        '        Private Sub VerifyInMySQL(ByVal ADUser As SearchResult, Optional ByVal AllowInteractive As Boolean = True)
        '            Dim ta As New LoginDataTableAdapters.taUser
        '            Dim taD As New LoginDataTableAdapters.taDeletedUsers
        '            Try
        '                Dim DBUsers As LoginData.UserDataTable = ta.GetByUID(ADUser.Properties!userPrincipalName(0))
        '                Dim DeletedDBUsers As LoginData.DeletedUsersDataTable = taD.GetByUid(ADUser.Properties!userPrincipalName(0))
        '                If DBUsers.Count = 0 AndAlso DeletedDBUsers.Count <> 0 Then
        '                    'Obnova smazaného
        '                    Try
        '                        taD.Undelete(DeletedDBUsers(0).ID)
        '                        DBUsers = ta.GetByUID(ADUser.Properties!userPrincipalName(0))
        '                    Catch
        '                        GoTo EndAssoc
        '                    End Try
        '                ElseIf DBUsers.Count = 0 Then
        '                    Try
        '                        DBUsers = ta.GetByName(ADUser.Properties!sn(0), ADUser.Properties!givenName(0))
        '                    Catch
        '                        GoTo EndAssoc
        '                    End Try
        '                    If DBUsers.Count = 1 Then
        '                        Dim taQ As New LoginDataTableAdapters.LoginQ
        '                        taQ.SetUID(ADUser.Properties!userPrincipalName(0), DBUsers(0).ID)
        '                        DBUsers = ta.GetByUID(ADUser.Properties!userPrincipalName(0))
        '                    End If
        'EndAssoc:
        '                End If
        '                If DBUsers.Count <> 1 Then
        '                    If AllowInteractive Then
        '                        Select Case MsgBox(String.Format("{0}, pravdìpodobnì se k aplikaci pøihlašujete poprvé.{1}Používali jste aplikaci již døíve a chcete se pøipojit ke svému starému úètu?{1}{1}Poznámka: Starý úèet = ten co nebyl CZxxxx.", ADUser.Properties!userPrincipalName(0), vbCrLf), MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.ApplicationModal Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.YesNoCancel, "První pøihlášení: EOS login @ " & Me.AppTitle)
        '                            Case MsgBoxResult.Yes
        '                                AssociateOld(ADUser)
        '                            Case MsgBoxResult.No
        '                                Try
        '                                    AssociateNew(ADUser)
        '                                Catch ex As MySql.Data.MySqlClient.MySqlException When ex.Message = String.Format("Duplicate entry '{0}' for key 2", ADUser.Properties!Name(0))
        '                                    Dim [Catch] As Boolean = True
        '                                    Try
        '                                        If MsgBox(String.Format("Nového uživatele se nepodaøilo, založit protože uživatelské jméno {0} již existuje.{1}Chcete se s tímto uživatelským jménem a pùvodním heslem pøihlásit ke svému starému úètu?", ADUser.Properties!Name(0), vbCrLf), MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation, "Chyba - nelze založit nový úèet") = MsgBoxResult.Yes Then
        '                                            [Catch] = True
        '                                            AssociateOld(ADUser)
        '                                        Else
        '                                            Throw New OperationCanceledException("Uživatel se rozhodl nepøipojit k MySQL ve chvíli, kdy založení nového úètu nebylo možné z dùvodu již existujícího uživatelského jména.", ex)
        '                                        End If
        '                                    Catch ex2 As IndexOutOfRangeException When [Catch] = True
        '                                        Throw New KeyNotFoundException("Objekt ActiveDirectory neposkytl požadovanou vlastnost (Name).", ex)
        '                                    End Try
        '                                End Try
        '                            Case Else
        '                                Throw New OperationCanceledException("Uživatel stornoval pøihlašování.")
        '                        End Select
        '                    Else
        '                        Throw New UserNotFoundException("Uživatel nebyl nalezen v databázi EOS Login.")
        '                    End If
        '                Else
        '                    Id = DBUsers(0).ID
        '                End If
        '                Me.ADUser = ADUser.GetDirectoryEntry
        '                UpdateMySQL()
        '            Catch ex As IndexOutOfRangeException
        '                Throw New KeyNotFoundException("Object ActiveDirectory neposkytl požadovanou vlastnost (userPrincipalName).", ex)
        '            End Try
        '        End Sub
        '        '''' <summary>Updatuje záznam o katuálním uživateli v MySQL podle ActiveDirectory</summary>
        'Private Sub UpdateMySQL()
        '    Dim ta As New LoginDataTableAdapters.taUser
        '    Dim Notes$ = ""
        '    Try
        '        Notes = ADUser.Properties!description(0)
        '    Catch : End Try
        '    Try
        '        ta.UpdateFromAD(ADUser.Properties!cn(0), ADUser.Properties!givenName(0), ADUser.Properties!sn(0), Notes, ADUser.Properties!userPrincipalName(0), Id)
        '    Catch : End Try
        'End Sub
        ''' <summary>Pøihlášení uživatele s výchozí identitou poskytnutou systémem</summary>
        ''' <exception cref="UserNotFoundException">Nepodaøilo se ovìøit uživatele vùèi ActiveDirectory =nebo= Nepodaøilo se ovìøit uživatele vùèi MySQL a <paramref name="AllowInteractive"/> je false</exception>
        ''' <exception cref="KeyNotFoundException">Object ActiveDirectory neposkytl požadovanou vlastnost (userPrincipalName; nebo Name, givenName, sn, sAMAcountName pøi vytváøení nového uživatele)</exception>
        ''' <exception cref="OperationCanceledException">Uživatel stornoval pøihlašování ke staré databázi =nebo= uživatel stornoval dozat na propojení starého úètu s ActiveDirectory</exception>
        ''' <exception cref="COMException">Chyba Active Directory</exception>
        Private Sub LoginInternal(Optional ByVal AllowInteractive As Boolean = True)
            Dim ADUser As SearchResult = GetUser()
            If ADUser Is Nothing Then Throw New UserNotFoundException("Nepodaøilo se oveøit uživatale vùèi ActiveDirectory.")
            'VerifyInMySQL(ADUser, AllowInteractive)
        End Sub
        '''' <summary>Vytvoøení nového úètu EOS login a navázání na úèet ActiveDirectory</summary>
        '''' <param name="User">Výsledek hledání uživatele v active directory</param>
        '''' <exception cref="KeyNotFoundException"><paramref name="User"/> neposkytl požadovanou vlastnost (Name, givenName, sn, sAMAcountName)</exception>
        'Private Sub AssociateNew(ByVal User As SearchResult)
        '    Try
        '        Dim ta As New LoginDataTableAdapters.taUser
        '        ta.AddUser(User.Properties!Name(0), User.Properties!givenName(0), User.Properties!sn(0), "Úèet importován z ActiveDirectory, heslo nenastaveno.", User.Properties!userPrincipalName(0))
        '        Me.Id = ta.GetByUID(User.Properties!userPrincipalName(0))(0).ID
        '    Catch ex As IndexOutOfRangeException
        '        Throw New KeyNotFoundException("Objekt uživatel z ActiveDirectory neposkytl požadovanou vlastnost (Name, givenName, sn, sAMAcountName).", ex)
        '    End Try
        'End Sub

        ''' <summary>Koøenový element ActiveDirectory</summary>
        Private Shared ReadOnly Property DirectoryEntry() As DirectoryEntry
            Get
                Dim ret As New DirectoryEntry
                ret.Path = ADPath
                ret.AuthenticationType = AuthenticationTypes.Secure
                Return ret
            End Get
        End Property



        ''' <summary>Seznam jednoduchých názvù skupin pro tohoto uživatele</summary>
        ''' <returns>Seznam skupin typu OU=Groups, nichž je èlenem</returns>
        Public Function GetGroups() As List(Of String)
            Return ADHelpers.GetGroups(ADUser)
        End Function
        ''' <summary>Zjistí jestli uživatel má dané oprávnìní</summary>
        ''' <param name="Right">Oprávnìní ke kontrole</param>
        ''' <returns>True pokud uživatel má oprávnìní (je èlenem skupiny) <paramref name="Right"/> nebo má superoprávnìní (viz <see cref="Admin"/>)</returns>
        Public Function HasRight(ByVal Right As String) As Boolean Implements IUser.HasRight
            Return GroupOverride.Contains(Right) 'OrElse GroupOverride.Contains(Admin)
        End Function

        Public Overrides Function ToString() As String Implements IUser.ToString
            Return String.Format("{0} {1} {2}", Jmeno, Prijmeni, PrincipalName)
        End Function

        '''' <summary>Asociování existujícího úètu EOS login s úètem ActiveDirectory</summary>
        '''' <param name="User">Informace o uživateli z ActiveDirectory</param>
        '''' <exception cref="IndexOutOfRangeException"><paramref name="User"/> neobsahuje vlastnost userPrincipalName</exception>
        '''' <exception cref="OperationCanceledException">Uživatel stornoval staré pøihlášení</exception>
        'Private Sub AssociateOld(ByVal User As SearchResult)
        '    Dim L As Type = Type.GetType("EOS.Login.Login")
        '    Dim OldUser As User = L.InvokeMember("Login", Reflection.BindingFlags.Static Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.InvokeMethod, Nothing, Nothing, New Object() {Nothing, True, "EOS login - Pøihlášení ke starému úètu"})
        '    '= EOS.Login.Login.Login(, True, "EOS login - Pøihlášení ke starému úètu")
        '    If OldUser Is Nothing Then Throw New OperationCanceledException("Uživatel stornoval pøihlašování")
        '    Dim ta As New LoginDataTableAdapters.LoginQ
        '    ta.SetUID(User.Properties!userPrincipalName(0), OldUser.ID)
        '    Id = OldUser.ID
        'End Sub

#Region "Properties"
        ''' <summary>Jméno</summary>
        ''' <returns>Jméno nebo null</returns>
        Public ReadOnly Property Jmeno$() Implements IUser.Jmeno
            Get
                Try
                    Return ADUser.Properties!givenName(0)
                Catch : Return Nothing : End Try
            End Get
        End Property
        ''' <summary>Pøíjmení</summary>
        ''' <returns>Pøíjmení nebo null</returns>
        Public ReadOnly Property Prijmeni$() Implements IUser.Prijmeni
            Get
                Try
                    Return ADUser.Properties!sn(0)
                Catch : Return Nothing : End Try
            End Get
        End Property
        ''' <summary>ID uživatele v MySQL</summary>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
        Private ReadOnly Property _id_() As Integer Implements IUser.ID
            Get
                Return Id
            End Get
        End Property
        ''' <summary>ID uživatele v MySQL</summary>
        Public Property Id() As Integer
            Get
                Return _Id
            End Get
            Private Set(ByVal value As Integer)
                _Id = value
            End Set
        End Property
        ''' <summary>Active Directory jméno uživatele</summary>
        Public ReadOnly Property PrincipalName$() Implements IUser.UserName, IUser.ADName
            Get
                Try
                    Return ADUser.Properties!userPrincipalName(0)
                Catch : Return Nothing : End Try
            End Get
        End Property
#End Region

#Region " IDisposable Support "
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False

        ''' <summary><see cref="IDisposable"/></summary>
        <DebuggerStepThrough()> _
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If TrayIcon IsNot Nothing Then
                        With TrayIcon
                            Try
                                .Visible = False
                                .Dispose()
                            Catch ex As ObjectDisposedException : End Try
                        End With
                        ADUser.Dispose()
                        Id = -1
                    End If
                End If
            End If
            Me.disposedValue = True
        End Sub

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <remarks>This code added by Visual Basic to correctly implement the disposable pattern.</remarks>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
        ''' <summary>Allows an System.Object to attempt to free resources and perform other cleanup operations before the System.Object is reclaimed by garbage collection.</summary>
        Protected Overrides Sub Finalize()
            Dispose()
            MyBase.Finalize()
        End Sub
        ''' <summary>Indicates if this instance is disposed</summary>
        Public ReadOnly Property IsDisposed() As Boolean
            Get
                Return disposedValue
            End Get
        End Property
#End Region

        Private Sub TrayIcon_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _TrayIcon.Disposed
            TrayIcon.Visible = False
        End Sub

        'Private Sub TrayIcon_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _TrayIcon.DoubleClick
        '    Static Shown As Boolean
        '    If Shown Then Exit Sub
        '    Dim f As New frmUserTools(New OnlineUser(Id))
        '    Shown = True
        '    Dim tmiTools_ As ToolStripMenuItem = Nothing
        '    If TrayIcon IsNot Nothing AndAlso TrayIcon.ContextMenuStrip IsNot Nothing AndAlso TrayIcon.ContextMenuStrip.Items(tmiTools) IsNot Nothing Then
        '        tmiTools_ = Me.TrayIcon.ContextMenuStrip.Items(tmiTools)
        '    End If
        '    If tmiTools_ IsNot Nothing Then tmiTools_.Enabled = False
        '    Try
        '        f.ShowDialog()
        '    Finally
        '        Shown = False
        '        If tmiTools_ IsNot Nothing Then tmiTools_.Enabled = True
        '    End Try
        'End Sub

        Private Sub TrayIcon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _TrayIcon.MouseDown
            TrayIcon.ShowBalloonTip(2000, "EOS Login - Active Directory", String.Format("Pøihlášen jako {0} {1}", Jmeno, Prijmeni, vbCrLf, PrincipalName), ToolTipIcon.Info) '({3}){2}Pro pøihlášení s jinými údaji ukonèete aplikaci a pø pøíštím pøístupu do zaheslované zóny mìjte zaplý Caps Lock a držte Shift.
        End Sub
    End Class

End Namespace
