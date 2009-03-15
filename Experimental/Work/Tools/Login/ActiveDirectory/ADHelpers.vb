Imports System.DirectoryServices.ActiveDirectory, System.DirectoryServices
Imports System.Security.Principal, System.Threading
Imports System.Runtime.InteropServices

Namespace Login
    Public Module ADHelpers
        ''' <summary>Seznam jednoduch�ch n�zv� skupin pro u�ivatele</summary>
        ''' <param name="User">U�ivatel</param>
        ''' <returns>Seznam skupin typu OU=Groups, nich� je �lenem</returns>
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
        ''' <summary>Rozparzu string, kter� vrac� ActiveDirectory jako skupinu, j� je u�ivatel �lenem</summary>
        ''' <param name="ADGString">String k rozparzov�n� ve form�tu ��rkou odd�lan�ch dvojic n�zev=hodnota</param>
        ''' <returns>Rozparzovan� string jako <see cref="[Dictionary](Of String, List(Of String))"/>, kde kl��e obsahuj� n�zvy hodnot a hodnoty seznam hodnot k p��slu�n�m n�zv�m</returns>
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
        ''' <summary>N�zev skupiny se superopr�vn�n�m</summary>
        Public Const Admin$ = "EOSCZPR-GL-IT"
        ''' <summary>Z�sk� z AD informace o u�ivateli</summary>
        ''' <param name="Name">Voliteln� u�ivatelsk� jm�no</param>
        ''' <param name="Domain">Voliteln� dom�na</param>
        ''' <param name="Password">Voliteln� heslo</param>
        ''' <returns>V�sledek p�tr�n� po u�ivateli v AD - m��e b�t null</returns>
        ''' <exception cref="ArgumentException">Pr�v� jeden z parametr� <paramref name="Name"/>, <paramref name="Password"/> je null</exception>
        ''' <exception cref="DirectoryServicesCOMException">Chyba Active Directory</exception>
        Friend Function GetUser(Optional ByVal Name As String = Nothing, Optional ByVal Password As String = Nothing) As SearchResult
            If (Name Is Nothing AndAlso Password IsNot Nothing) OrElse (Password Is Nothing AndAlso Name IsNot Nothing) Then
                Throw New ArgumentException("Kd� je nastaveno jm�no mus� b�t nastaveno i heslo. Kdy� nen� nastaveno heslo nesm� b�t nastaveno ani jm�no.")
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
        ''' <summary>Obsluhuje kliknut� na tla��tko OK dialogu interaktivn�ho p�ihl�en� k ActiveDirectory</summary>
        ''' <exception cref="COMException">Chyba Active Directory</exception>
        Friend Sub InteractiveClick(ByVal sender As LoginDialog, ByVal e As LoginDialog.FormCancelEventArgs)
            Try
                Dim u As SearchResult = GetUser(sender.UserName, sender.Password)
                If u Is Nothing Then
                    MsgBox("U�ivatelsk� jm�no a/nebo heslo, kter� jset zadali je �patn�." & vbCrLf & vbCrLf & "Pokud se chcete p�ihl�sit pod jinou ne� v�choz� dom�nou zadejte u�ivatelsk� jm�no ve tvaru <jm�no>@<dom�na>.", MsgBoxStyle.Critical, "ActiveDirectory login - chyba")
                    e.Cancel = True
                End If
            Catch ex As DirectoryServicesCOMException
                MsgBox("Active Directory hl�s�:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "ActiveDirectory login - chyba")
                e.Cancel = True
            End Try
        End Sub
    End Module
End Namespace