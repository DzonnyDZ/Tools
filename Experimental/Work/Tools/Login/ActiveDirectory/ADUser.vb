Imports System.DirectoryServices.ActiveDirectory, System.DirectoryServices
Imports System.Security.Principal, System.Threading
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace Login
    ''' <summary>Reprezentuje u�ivatele z ActiveDirectory propojen�ho na tabulku LoginUser</summary>
    Public Class ADUser : Implements IDisposable, IUser
#Region "Shared"
        ''' <summary>Instance t��dy. M��e b�t jen jedna �iv� instance</summary>
        Private Shared Instance As ADUser
        ''' <summary>N�zev aplikace, pokud byl v <see cref="Login"/> zm�n�n, jinak null nebo <see cref="String.Empty"/></summary>
        Private _ApplicationTitle As String
        ''' <summary>Vrac� n�zev aplikace (standartn� My.Application.Info.Title, pokud nebyl v <see cref="Login"/> zm�n�n)</summary>
        Private ReadOnly Property AppTitle() As String
            Get
                If _ApplicationTitle Is Nothing OrElse _ApplicationTitle = "" Then
                    Return My.Application.Info.Title
                Else
                    Return _ApplicationTitle
                End If
            End Get
        End Property
        ''' <summary>Zaloguje u�ivatele a vr�t� jeho instanci</summary>
        ''' <param name="AllowInteractive">Povolit interaktivn� p�ihl�en�</param>
        ''' <param name="AllowTray">Povolit SysTray ikonu</param>
        ''' <param name="DisplayAppName">Zobrazovan� n�zev aplikace, pokud m� b�t jin� ne� My.Application.Info.Title</param>
        ''' <returns>Zalogovan� u�ivatel, nebo null, pokud zalogov�n� selhalo</returns>
        ''' <exception cref="UserNotFoundException">U�ivatele se nepoda�ilo ov��it v ActiveDirectory a <paramref name="AllowInteractive"/> je false =nebo= Nepoda�ilo se ov��it u�ivatele v��i MySQL a <paramref name="AllowInteractive"/> je false</exception>
        ''' <exception cref="KeyNotFoundException">Object ActiveDirectory neposkytl po�adovanou vlastnost (userPrincipalName; nebo Name, givenName, sn, sAMAcountName p�i vytv��en� nov�ho u�ivatele) a <paramref name="AllowInteractive"/> je false</exception>
        <Obsolete("Pou�ijte t��du OracleADUser")> _
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
        ''' <summary>Inicializ�tor - nastav� <see cref="AppDomain.CurrentDomain">AppDomain.CurrentDomain</see>.<see cref="AppDomain.SetPrincipalPolicy">SetPrincipalPolicy</see> na <see cref="PrincipalPolicy.WindowsPrincipal"/></summary>
        Shared Sub New()
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal)
        End Sub
        ''' <summary>Vrac� jedinou instanci <see cref="ADUser"/>, kter� je v r�mci palikace</summary>
        ''' <returns>Instance nebo null, pokud ��dn� instance neexistuje, nebo je disposed</returns>
        <Obsolete("Pou�ijte t��du OracleADUser")> _
        Public Shared ReadOnly Property [Default]() As ADUser
            Get
                If Instance Is Nothing OrElse Instance.IsDisposed Then Return Nothing
                Return Instance
            End Get
        End Property

#End Region
        ''' <summary>Id u�ivatele v tabulce LoginUser na MySQL</summary>
        Private _Id As Integer
        ''' <summary>Skupiny, kter� vstupuj� v platnost jako skupiny pro u�ivatele</summary>
        Private GroupOverride As New List(Of String)
        ''' <summary>U�ivatel z active directory</summary>
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
                    'menu.Items.Add("N�stroje ...", Nothing, AddressOf Me.TrayIcon_DoubleClick)
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
        ''' <param name="AllowInteractive">Povoluje interaktivn� mo�nosti p�ihl�en�: Pokud u�ivatel m� zapl� CapsLock a dr�� Shif p�ihl� s� jej pod jin�mi pr�vy; pokud nelze u�ivatele p�ihl�sit pod pr�vy OS vyzve jej k zad�n� hesla; pokud se nepoda�� u�ivatele nap�rovat na MySQL vyzve jej k vytvo�en� p�ru nebo k vytvo�en� nov�ho konta.</param>
        ''' <param name="AllowTray">Povol� zobrazit ikonu EOS login v SysTray</param>
        ''' <param name="AppDisplayName">Alternativn� zobrazovan� jm�no aplikace</param>
        ''' <exception cref="UserNotFoundException">U�ivatele se nepoda�ilo ov��it v ActiveDirectory =nebo= Nepoda�ilo se ov��it u�ivatele v��i MySQL a <paramref name="AllowInteractive"/> je false</exception>
        ''' <exception cref="OperationCanceledException">U�ivatel p�ihl�en� stornoval nebo stronoval n�kterou z d�l��ch operac� (U�ivatel stornoval p�ihla�ov�n� ke star� datab�zi =nebo= u�ivatel stornoval dozat na propojen� star�ho ��tu s ActiveDirectory) (p�i interaktivn�m p�ihl�en�; u�ivatel o tom v�, �e n�co stornoval, nen� pot�eba jej informovat)</exception>
        ''' <exception cref="KeyNotFoundException">Object ActiveDirectory neposkytl po�adovanou vlastnost (userPrincipalName; nebo Name, givenName, sn, sAMAcountName p�i vytv��en� nov�ho u�ivatele)</exception>
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
                    If MsgBox("Nepoda�ilo se v�s ov��it v��i ActiveDirectory. Chcete se p�ihl�sit ru�n�?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "EOS Login @ " & My.Application.Info.Title) = MsgBoxResult.Yes Then
                        LoginInteractive()
                    Else
                        Throw New OperationCanceledException("U�ivatel si nep��l interaktivn� p�ihla�ov�n� po t� co automatick� selhalo.", ex)
                    End If
                End Try
            End If
            GroupOverride = ADHelpers.GetGroups(ADUser)
            If AllowTray Then SysTray()
        End Sub
        ''' <summary>Zobraz� ikonu v SysTray</summary>
        Private Sub SysTray()
            TrayIcon = New NotifyIcon()
            'TrayIcon.Icon = My.Resources.EOS
            TrayIcon.Text = "Login @ " & Me.AppTitle
            TrayIcon.Visible = True
            Try
                TrayIcon.BalloonTipIcon = ToolTipIcon.Info
                TrayIcon.BalloonTipText = "P�ihl�en jako " & CStr(ADUser.Properties!displayName(0))
                TrayIcon.BalloonTipTitle = "Login @ " & Me.AppTitle
                TrayIcon.ShowBalloonTip(15000)
            Catch : End Try
        End Sub
        ''' <summary>Interaktivn� p�ihl�en� k ActiveDirectory</summary>
        ''' <exception cref="OperationCanceledException">U�ivatel p�ihl�en� stornoval nebo stronoval n�kterou z d�l��ch operac� (U�ivatel stornoval p�ihla�ov�n� ke star� datab�zi =nebo= u�ivatel stornoval dozat na propojen� star�ho ��tu s ActiveDirectory)</exception>
        ''' <exception cref="UserNotFoundException">U�ivatele se nepoda�ilo ov��it v ActiveDirectory</exception>
        ''' <exception cref="KeyNotFoundException">Object ActiveDirectory neposkytl po�adovanou vlastnost (userPrincipalName; nebo Name, givenName, sn, sAMAcountName p�i vytv��en� nov�ho u�ivatele)</exception>
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
                    If u Is Nothing Then Throw New UserNotFoundException("U�ivatel nebyl v ActiveDirectory nalezen nebo heslo je �pan�")
                    'VerifyInMySQL(u)
                Else
                    Throw New OperationCanceledException("U�ivatel stronoval interaktivn� p�ihla�ov�n�.")
                End If
            Finally
                ld.Dispose()
            End Try
        End Sub



        '        ''' <summary>Ov��� u�ivatele z ActiveDirectory proti MySQL (EOS logi, LoginUser)</summary>
        '        ''' <param name="ADUser">V�sledek hled�n� u�ivatele v ActiveDirectory</param>
        '        ''' <param name="AllowInteractive">Povolen� interaktivn�ho re�imu</param>
        '        ''' <exception cref="UserNotFoundException">Nepoda�ilo se ov��it u�ivatele v��i MySQL a <paramref name="AllowInteractive"/> je false</exception>
        '        ''' <exception cref="KeyNotFoundException">Object ActiveDirectory neposkytl po�adovanou vlastnost (userPrincipalName; nebo Name, givenName, sn, sAMAcountName p�i vytv��en� nov�ho u�ivatele)</exception>
        '        ''' <exception cref="OperationCanceledException">U�ivatel stornoval p�ihla�ov�n� ke star� datab�zi =nebo= u�ivatel stornoval dozat na propojen� star�ho ��tu s ActiveDirectory</exception>
        '        Private Sub VerifyInMySQL(ByVal ADUser As SearchResult, Optional ByVal AllowInteractive As Boolean = True)
        '            Dim ta As New LoginDataTableAdapters.taUser
        '            Dim taD As New LoginDataTableAdapters.taDeletedUsers
        '            Try
        '                Dim DBUsers As LoginData.UserDataTable = ta.GetByUID(ADUser.Properties!userPrincipalName(0))
        '                Dim DeletedDBUsers As LoginData.DeletedUsersDataTable = taD.GetByUid(ADUser.Properties!userPrincipalName(0))
        '                If DBUsers.Count = 0 AndAlso DeletedDBUsers.Count <> 0 Then
        '                    'Obnova smazan�ho
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
        '                        Select Case MsgBox(String.Format("{0}, pravd�podobn� se k aplikaci p�ihla�ujete poprv�.{1}Pou��vali jste aplikaci ji� d��ve a chcete se p�ipojit ke sv�mu star�mu ��tu?{1}{1}Pozn�mka: Star� ��et = ten co nebyl CZxxxx.", ADUser.Properties!userPrincipalName(0), vbCrLf), MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.ApplicationModal Or MsgBoxStyle.MsgBoxSetForeground Or MsgBoxStyle.YesNoCancel, "Prvn� p�ihl�en�: EOS login @ " & Me.AppTitle)
        '                            Case MsgBoxResult.Yes
        '                                AssociateOld(ADUser)
        '                            Case MsgBoxResult.No
        '                                Try
        '                                    AssociateNew(ADUser)
        '                                Catch ex As MySql.Data.MySqlClient.MySqlException When ex.Message = String.Format("Duplicate entry '{0}' for key 2", ADUser.Properties!Name(0))
        '                                    Dim [Catch] As Boolean = True
        '                                    Try
        '                                        If MsgBox(String.Format("Nov�ho u�ivatele se nepoda�ilo, zalo�it proto�e u�ivatelsk� jm�no {0} ji� existuje.{1}Chcete se s t�mto u�ivatelsk�m jm�nem a p�vodn�m heslem p�ihl�sit ke sv�mu star�mu ��tu?", ADUser.Properties!Name(0), vbCrLf), MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation, "Chyba - nelze zalo�it nov� ��et") = MsgBoxResult.Yes Then
        '                                            [Catch] = True
        '                                            AssociateOld(ADUser)
        '                                        Else
        '                                            Throw New OperationCanceledException("U�ivatel se rozhodl nep�ipojit k MySQL ve chv�li, kdy zalo�en� nov�ho ��tu nebylo mo�n� z d�vodu ji� existuj�c�ho u�ivatelsk�ho jm�na.", ex)
        '                                        End If
        '                                    Catch ex2 As IndexOutOfRangeException When [Catch] = True
        '                                        Throw New KeyNotFoundException("Objekt ActiveDirectory neposkytl po�adovanou vlastnost (Name).", ex)
        '                                    End Try
        '                                End Try
        '                            Case Else
        '                                Throw New OperationCanceledException("U�ivatel stornoval p�ihla�ov�n�.")
        '                        End Select
        '                    Else
        '                        Throw New UserNotFoundException("U�ivatel nebyl nalezen v datab�zi EOS Login.")
        '                    End If
        '                Else
        '                    Id = DBUsers(0).ID
        '                End If
        '                Me.ADUser = ADUser.GetDirectoryEntry
        '                UpdateMySQL()
        '            Catch ex As IndexOutOfRangeException
        '                Throw New KeyNotFoundException("Object ActiveDirectory neposkytl po�adovanou vlastnost (userPrincipalName).", ex)
        '            End Try
        '        End Sub
        '        '''' <summary>Updatuje z�znam o katu�ln�m u�ivateli v MySQL podle ActiveDirectory</summary>
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
        ''' <summary>P�ihl�en� u�ivatele s v�choz� identitou poskytnutou syst�mem</summary>
        ''' <exception cref="UserNotFoundException">Nepoda�ilo se ov��it u�ivatele v��i ActiveDirectory =nebo= Nepoda�ilo se ov��it u�ivatele v��i MySQL a <paramref name="AllowInteractive"/> je false</exception>
        ''' <exception cref="KeyNotFoundException">Object ActiveDirectory neposkytl po�adovanou vlastnost (userPrincipalName; nebo Name, givenName, sn, sAMAcountName p�i vytv��en� nov�ho u�ivatele)</exception>
        ''' <exception cref="OperationCanceledException">U�ivatel stornoval p�ihla�ov�n� ke star� datab�zi =nebo= u�ivatel stornoval dozat na propojen� star�ho ��tu s ActiveDirectory</exception>
        ''' <exception cref="COMException">Chyba Active Directory</exception>
        Private Sub LoginInternal(Optional ByVal AllowInteractive As Boolean = True)
            Dim ADUser As SearchResult = GetUser()
            If ADUser Is Nothing Then Throw New UserNotFoundException("Nepoda�ilo se ove�it u�ivatale v��i ActiveDirectory.")
            'VerifyInMySQL(ADUser, AllowInteractive)
        End Sub
        '''' <summary>Vytvo�en� nov�ho ��tu EOS login a nav�z�n� na ��et ActiveDirectory</summary>
        '''' <param name="User">V�sledek hled�n� u�ivatele v active directory</param>
        '''' <exception cref="KeyNotFoundException"><paramref name="User"/> neposkytl po�adovanou vlastnost (Name, givenName, sn, sAMAcountName)</exception>
        'Private Sub AssociateNew(ByVal User As SearchResult)
        '    Try
        '        Dim ta As New LoginDataTableAdapters.taUser
        '        ta.AddUser(User.Properties!Name(0), User.Properties!givenName(0), User.Properties!sn(0), "��et importov�n z ActiveDirectory, heslo nenastaveno.", User.Properties!userPrincipalName(0))
        '        Me.Id = ta.GetByUID(User.Properties!userPrincipalName(0))(0).ID
        '    Catch ex As IndexOutOfRangeException
        '        Throw New KeyNotFoundException("Objekt u�ivatel z ActiveDirectory neposkytl po�adovanou vlastnost (Name, givenName, sn, sAMAcountName).", ex)
        '    End Try
        'End Sub

        ''' <summary>Ko�enov� element ActiveDirectory</summary>
        Private Shared ReadOnly Property DirectoryEntry() As DirectoryEntry
            Get
                Dim ret As New DirectoryEntry
                ret.Path = ADPath
                ret.AuthenticationType = AuthenticationTypes.Secure
                Return ret
            End Get
        End Property



        ''' <summary>Seznam jednoduch�ch n�zv� skupin pro tohoto u�ivatele</summary>
        ''' <returns>Seznam skupin typu OU=Groups, nich� je �lenem</returns>
        Public Function GetGroups() As List(Of String)
            Return ADHelpers.GetGroups(ADUser)
        End Function
        ''' <summary>Zjist� jestli u�ivatel m� dan� opr�vn�n�</summary>
        ''' <param name="Right">Opr�vn�n� ke kontrole</param>
        ''' <returns>True pokud u�ivatel m� opr�vn�n� (je �lenem skupiny) <paramref name="Right"/> nebo m� superopr�vn�n� (viz <see cref="Admin"/>)</returns>
        Public Function HasRight(ByVal Right As String) As Boolean Implements IUser.HasRight
            Return GroupOverride.Contains(Right) 'OrElse GroupOverride.Contains(Admin)
        End Function

        Public Overrides Function ToString() As String Implements IUser.ToString
            Return String.Format("{0} {1} {2}", Jmeno, Prijmeni, PrincipalName)
        End Function

        '''' <summary>Asociov�n� existuj�c�ho ��tu EOS login s ��tem ActiveDirectory</summary>
        '''' <param name="User">Informace o u�ivateli z ActiveDirectory</param>
        '''' <exception cref="IndexOutOfRangeException"><paramref name="User"/> neobsahuje vlastnost userPrincipalName</exception>
        '''' <exception cref="OperationCanceledException">U�ivatel stornoval star� p�ihl�en�</exception>
        'Private Sub AssociateOld(ByVal User As SearchResult)
        '    Dim L As Type = Type.GetType("EOS.Login.Login")
        '    Dim OldUser As User = L.InvokeMember("Login", Reflection.BindingFlags.Static Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.InvokeMethod, Nothing, Nothing, New Object() {Nothing, True, "EOS login - P�ihl�en� ke star�mu ��tu"})
        '    '= EOS.Login.Login.Login(, True, "EOS login - P�ihl�en� ke star�mu ��tu")
        '    If OldUser Is Nothing Then Throw New OperationCanceledException("U�ivatel stornoval p�ihla�ov�n�")
        '    Dim ta As New LoginDataTableAdapters.LoginQ
        '    ta.SetUID(User.Properties!userPrincipalName(0), OldUser.ID)
        '    Id = OldUser.ID
        'End Sub

#Region "Properties"
        ''' <summary>Jm�no</summary>
        ''' <returns>Jm�no nebo null</returns>
        Public ReadOnly Property Jmeno$() Implements IUser.Jmeno
            Get
                Try
                    Return ADUser.Properties!givenName(0)
                Catch : Return Nothing : End Try
            End Get
        End Property
        ''' <summary>P��jmen�</summary>
        ''' <returns>P��jmen� nebo null</returns>
        Public ReadOnly Property Prijmeni$() Implements IUser.Prijmeni
            Get
                Try
                    Return ADUser.Properties!sn(0)
                Catch : Return Nothing : End Try
            End Get
        End Property
        ''' <summary>ID u�ivatele v MySQL</summary>
        <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
        Private ReadOnly Property _id_() As Integer Implements IUser.ID
            Get
                Return Id
            End Get
        End Property
        ''' <summary>ID u�ivatele v MySQL</summary>
        Public Property Id() As Integer
            Get
                Return _Id
            End Get
            Private Set(ByVal value As Integer)
                _Id = value
            End Set
        End Property
        ''' <summary>Active Directory jm�no u�ivatele</summary>
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
            TrayIcon.ShowBalloonTip(2000, "EOS Login - Active Directory", String.Format("P�ihl�en jako {0} {1}", Jmeno, Prijmeni, vbCrLf, PrincipalName), ToolTipIcon.Info) '({3}){2}Pro p�ihl�en� s jin�mi �daji ukon�ete aplikaci a p� p��t�m p��stupu do zaheslovan� z�ny m�jte zapl� Caps Lock a dr�te Shift.
        End Sub
    End Class

End Namespace
