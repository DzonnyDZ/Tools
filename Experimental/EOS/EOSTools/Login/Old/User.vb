Namespace Login
    '''' <summary>reprezentuje uživatele</summary>
    'Public MustInherit Class User : Implements IUser
    '    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="ID"/></summary>
    '    Protected _ID As Integer
    '    ''' <summary>ID v databázi</summary>
    '    Public Overridable ReadOnly Property ID() As Integer Implements IUser.ID
    '        Get
    '            Return _ID
    '        End Get
    '    End Property
    '    ''' <summary>Returns a <see cref="System.String"/> that represents the current <see cref="System.Object"/>.</summary>
    '    ''' <returns>A <see cref="System.String"/> that represents the current <see cref="System.Object"/></returns>
    '    Public Overrides Function ToString() As String Implements IUser.ToString
    '        Return UserName
    '    End Function
    '    ''' <summary>Uživatelské jméno</summary>
    '    Public MustOverride ReadOnly Property UserName() As String Implements IUser.UserName
    '    ''' <summary>Nastaví heslo</summary>
    '    ''' <remarks>Vždy pracuje pøímo nad databází</remarks>
    '    ''' <exception cref="MySql.Data.MySqlClient.MySqlException">Cyhba pøi nastavení hesla</exception>
    '    Public WriteOnly Property Password() As String
    '        Set(ByVal value As String)
    '            Dim tbl As New LoginDataTableAdapters.LoginQ
    '            tbl.SetPwd(value, ID)
    '        End Set
    '    End Property
    '    ''' <summary>Jméno</summary>
    '    Public MustOverride ReadOnly Property Jmeno() As String Implements IUser.Jmeno
    '    ''' <summary>Pøíjmení</summary>
    '    Public MustOverride ReadOnly Property Prijmeni() As String Implements IUser.Prijmeni
    '    ''' <summary>Poznámka</summary>
    '    Public MustOverride ReadOnly Property Poznamka() As String
    '    ''' <summary>Název asociovaného úètu ActiveDirectory</summary>
    '    Public MustOverride ReadOnly Property ADName() As String Implements IUser.ADName

    '    ''' <summary>Ovìøí uživatele na základì jména <paramref name="UserName"/> a hesla <paramref name="Password"/></summary>
    '    ''' <param name="UserName">Uživatelské jméno</param>
    '    ''' <param name="Password">Heslo</param>
    '    ''' <returns>ID uživatele se zadaným jménem a heslem</returns>
    '    ''' <exception cref="UserNotFoundException">Uživatel se zadaným jménem a heslem neexistuje</exception>
    '    Public Shared Function Verify(ByVal UserName As String, ByVal Password As String) As Integer
    '        Dim tbl As New LoginDataTableAdapters.LoginQ
    '        Try
    '            Return tbl.VerifyLogin(UserName, Password).Value
    '        Catch ex As Exception
    '            Throw New UserNotFoundException("Uživatel se zadaným jménem neexistuje nebo heslo není správné", ex)
    '        End Try
    '    End Function
    '    ''' <summary>Ovìøí oprávnìní uživatele</summary>
    '    ''' <param name="Right">Název oprávnìní</param>
    '    ''' <returns>True pokud uživatel má zadané oprávnìní</returns>
    '    ''' <remarks>Šahá do databáze</remarks>
    '    Public Overridable Function HasRight(ByVal Right As String) As Boolean Implements IUser.HasRight
    '        Dim tbl As New LoginDataTableAdapters.LoginQ
    '        Try
    '            If tbl.HasUserRight(ID, Right).Value Then
    '                Return True
    '            Else
    '                Return tbl.HasUserRight(ID, "Admin")
    '            End If
    '        Catch
    '            Return False
    '        End Try
    '    End Function
    'End Class


    '''' <summary>Reprezentuje uživatele systému</summary>
    '''' <remarks>Tato tøída tahá data vždy pøímo z databáze</remarks>
    'Public Class OnlineUser : Inherits User
    '    ''' <summary>CTor podle ID</summary>
    '    ''' <param name="ID">ID uživatele</param>
    '    ''' <exception cref="UserNotFoundException">Uživatel se zadaným ID <paramref name="ID"/> nenalezen</exception>
    '    Public Sub New(ByVal ID As Integer)
    '        Dim tbl As New LoginDataTableAdapters.LoginQ
    '        Try
    '            If Not tbl.VerifyUserID(ID).Value Then Throw New UserNotFoundException(String.Format("Uživatel ID {0} nenalezen", ID))
    '        Catch ex As Exception
    '            Throw New UserNotFoundException(String.Format("Uživatel ID {0} nenalezen", ID), ex)
    '        End Try
    '        _ID = ID
    '    End Sub
    '    ''' <summary>CTor pod uživatelského jména</summary>
    '    ''' <param name="Name">Uživatelské jméno</param>
    '    ''' <exception cref="UserNotFoundException">Uživatele se zadaným jménem <paramref name="Name"/> nenalezen</exception>
    '    Public Sub New(ByVal Name As String)
    '        Dim tbl As New LoginDataTableAdapters.LoginQ
    '        Try
    '            _ID = tbl.GetIDByName(Name).Value
    '        Catch ex As Exception
    '            Throw New UserNotFoundException(String.Format("Uživatel {0} nenalezen", Name), ex)
    '        End Try
    '    End Sub
    '    ''' <summary>Uživatelské jméno</summary>
    '    Public Overrides ReadOnly Property UserName() As String
    '        Get
    '            Dim tbl As New LoginDataTableAdapters.LoginQ
    '            Return tbl.GetUserName(ID)
    '        End Get
    '    End Property
    '    ''' <summary>Jméno</summary>
    '    Public Overrides ReadOnly Property Jmeno() As String
    '        Get
    '            Dim tbl As New LoginDataTableAdapters.LoginQ
    '            Return tbl.GetJmeno(ID)
    '        End Get
    '    End Property
    '    ''' <summary>Pøíjmení</summary>
    '    Public Overrides ReadOnly Property Prijmeni() As String
    '        Get
    '            Dim tbl As New LoginDataTableAdapters.LoginQ
    '            Return tbl.GetPrijmeni(ID)
    '        End Get
    '    End Property
    '    ''' <summary>Poznámka</summary>
    '    Public Overrides ReadOnly Property Poznamka() As String
    '        Get
    '            Dim tbl As New LoginDataTableAdapters.LoginQ
    '            Return tbl.GetPozn(ID)
    '        End Get
    '    End Property
    '    ''' <summary>Název asociovaného úètu AD</summary>
    '    Public Overrides ReadOnly Property ADName() As String
    '        Get
    '            Dim tbl As New LoginDataTableAdapters.LoginQ
    '            Return tbl.GetUID(ID)
    '        End Get
    '    End Property
    'End Class

    'Public Class OfflineUser : Inherits User
    '    ''' <summary>CTor podle ID</summary>
    '    ''' <param name="ID">ID uživatele</param>
    '    ''' <exception cref="UserNotFoundException">Uživatel se zadaným ID <paramref name="ID"/> nenalezen</exception>
    '    Public Sub New(ByVal ID As Integer)
    '        _ID = ID
    '        Init(ID)
    '    End Sub
    '    ''' <summary>CTor pod uživatelského jména</summary>
    '    ''' <param name="Name">Uživatelské jméno</param>
    '    ''' <exception cref="UserNotFoundException">Uživatele se zadaným jménem <paramref name="Name"/> nenalezen</exception>
    '    Public Sub New(ByVal Name As String)
    '        Dim tbl As New LoginDataTableAdapters.LoginQ
    '        Try
    '            _ID = tbl.GetIDByName(Name).Value
    '        Catch ex As Exception
    '            Throw New UserNotFoundException(String.Format("Uživatel {0} nenalezen", Name), ex)
    '        End Try
    '        init(ID)
    '    End Sub
    '    ''' <summary>Inicilaizuje uživatele IDèkem</summary>
    '    Protected Sub Init(ByVal ID As Integer)
    '        Dim dt As New LoginDataTableAdapters.taUser
    '        Try
    '            With dt.Get1User(ID).Item(0)
    '                _Jmeno = .Jmeno
    '                _Prijmeni = .Prijmeni
    '                _UserName = .username
    '                _Poznamka = .Pozn
    '                _AD = .uid
    '            End With
    '        Catch ex As Exception
    '            Throw New UserNotFoundException(String.Format("Uživatel s ID {0} nenalezen", ID), ex)
    '        End Try
    '    End Sub
    '    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Jmeno"/></summary>
    '    Private _Jmeno As String
    '    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Prijmeni"/></summary>
    '    Private _Prijmeni As String
    '    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="UserName"/></summary>
    '    Private _UserName As String
    '    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Poznamka"/></summary>
    '    Private _Poznamka As String
    '    Private _AD As String
    '    ''' <summary>Jméno</summary>
    '    Public Overrides ReadOnly Property Jmeno() As String
    '        Get
    '            Return _Jmeno
    '        End Get
    '    End Property
    '    ''' <summary>Pøíjmení</summary>
    '    Public Overrides ReadOnly Property Prijmeni() As String
    '        Get
    '            Return _Prijmeni
    '        End Get
    '    End Property
    '    ''' <summary>Uživatelské jméno</summary>
    '    Public Overrides ReadOnly Property UserName() As String
    '        Get
    '            Return _UserName
    '        End Get
    '    End Property
    '    ''' <summary>Poznámka</summary>
    '    Public Overrides ReadOnly Property Poznamka() As String
    '        Get
    '            Return _Poznamka
    '        End Get
    '    End Property
    '    ''' <summary>Náuev asociovaného úètu Active Directory</summary>
    '    Public Overrides ReadOnly Property ADName() As String
    '        Get
    '            Return _AD
    '        End Get
    '    End Property
    'End Class

    ''' <summary>Nastává pøi nenalezení uživatele</summary>
    Public Class UserNotFoundException : Inherits Exception
        ''' <summary>Initializes a new instance of the System.Exception class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        ''' <param name="message">The error message that explains the reason for the exception.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        Public Sub New(ByVal Message As String, Optional ByVal InnerException As Exception = Nothing)
            MyBase.New(Message, InnerException)
        End Sub
    End Class
End Namespace