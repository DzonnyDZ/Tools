Imports System.DirectoryServices.ActiveDirectory, System.DirectoryServices
Imports System.Security.Principal, System.Threading
Imports System.Runtime.InteropServices

Namespace Login
    Public Module ADHelpers
        ''' <summary>Seznam jednoduchých názvù skupin pro uživatele</summary>
        ''' <param name="User">Uživatel</param>
        ''' <returns>Seznam skupin typu OU=Groups, nichž je èlenem</returns>
        Friend Function GetGroups(ByVal User As DirectoryEntry) As List(Of String)
            Dim ret As New List(Of String)
            For Each g As String In User.Properties!memberOf
                Dim GInfo As Dictionary(Of String, List(Of String)) = ParseADGString(g)
                If GInfo.ContainsKey("CN") AndAlso GInfo.ContainsKey("OU") AndAlso GInfo!OU.Contains("Groups") Then
                    For Each Group As String In GInfo!CN
                        ret.Add(Group)
                    Next Group
                End If
            Next g
            Return ret
        End Function
        Public ADPath$
        Public Domain As String
        ''' <summary>Rozparzu string, který vrací ActiveDirectory jako skupinu, jíž je uživatel èlenem</summary>
        ''' <param name="ADGString">String k rozparzování ve formátu èárkou oddìlaných dvojic název=hodnota</param>
        ''' <returns>Rozparzovaný string jako <see cref="[Dictionary](Of String, List(Of String))"/>, kde klíèe obsahují názvy hodnot a hodnoty seznam hodnot k pøíslušným názvùm</returns>
        Public Function ParseADGString(ByVal ADGString$) As Dictionary(Of String, List(Of String))
            Dim pArr As String() = ADGString.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
            Dim ret As New Dictionary(Of String, List(Of String))
            For Each Pair As String In pArr
                Dim pParts() As String = Pair.Split("="c)
                If pParts.Length = 2 Then
                    If Not ret.ContainsKey(pParts(0)) Then
                        ret.Add(pParts(0), New List(Of String))
                    End If
                    ret(pParts(0)).Add(pParts(1))
                End If
            Next Pair
            Return ret
        End Function
        ''' <summary>Název skupiny se superoprávnìním</summary>
        Public Const Admin$ = "EOSCZPR-GL-IT"
        ''' <summary>Získá z AD informace o uživateli</summary>
        ''' <param name="Name">Volitelné uživatelské jméno</param>
        ''' <param name="Domain">Volitelná doména</param>
        ''' <param name="Password">Volitelné heslo</param>
        ''' <returns>Výsledek pátrání po uživateli v AD - mùže být null</returns>
        ''' <exception cref="ArgumentException">Právì jeden z parametrù <paramref name="Name"/>, <paramref name="Password"/> je null</exception>
        ''' <exception cref="DirectoryServicesCOMException">Chyba Active Directory</exception>
        Friend Function GetUser(Optional ByVal Name As String = Nothing, Optional ByVal Password As String = Nothing) As SearchResult
            If (Name Is Nothing AndAlso Password IsNot Nothing) OrElse (Password Is Nothing AndAlso Name IsNot Nothing) Then
                Throw New ArgumentException("Kdž je nastaveno jméno musí být nastaveno i heslo. Když není nastaveno heslo nesmí být nastaveno ani jméno.")
            End If
            Dim Entry As DirectoryEntry
            If Domain Is Nothing Then Throw New ArgumentNullException("Domain") 'Domain = My.Settings.ADDefaultDomain
            If Name Is Nothing Then
                Entry = New DirectoryEntry(ADPath)
            Else
                Dim FullName$
                If Name.Contains("@"c) Then FullName = Name Else FullName = Name & "@" & Domain
                Entry = New DirectoryEntry(ADPath, FullName, Password)
            End If
            Dim dSearch As New DirectorySearcher(Entry)
            If Name Is Nothing Then Name = Environment.UserName ' & "@" & Environment.UserDomainName
            If Not Name.Contains("@") Then Name &= "@" & Domain
            dSearch.Filter = "(&(objectClass=user)(userPrincipalName=" & Name & "))"
            Dim sr As SearchResult = dSearch.FindOne
            Return sr
        End Function
        ''' <summary>Obsluhuje kliknutí na tlaèítko OK dialogu interaktivního pøihlášení k ActiveDirectory</summary>
        ''' <exception cref="COMException">Chyba Active Directory</exception>
        Friend Sub InteractiveClick(ByVal sender As LoginDialog, ByVal e As LoginDialog.FormCancelEventArgs)
            Try
                Dim u As SearchResult = GetUser(sender.UserName, sender.Password)
                If u Is Nothing Then
                    MsgBox("Uživatelské jméno a/nebo heslo, které jset zadali je špatné." & vbCrLf & vbCrLf & "Pokud se chcete pøihlásit pod jinou než výchozí doménou zadejte uživatelské jméno ve tvaru <jméno>@<doména>.", MsgBoxStyle.Critical, "ActiveDirectory login - chyba")
                    e.Cancel = True
                End If
            Catch ex As DirectoryServicesCOMException
                MsgBox("Active Directory hlásí:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "ActiveDirectory login - chyba")
                e.Cancel = True
            End Try
        End Sub
    End Module
End Namespace