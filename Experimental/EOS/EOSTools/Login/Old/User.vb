Namespace Login
    '''' <summary>reprezentuje u�ivatele</summary>
    'Public MustInherit Class User : Implements IUser
    '    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="ID"/></summary>
    '    Protected _ID As Integer
    '    ''' <summary>ID v datab�zi</summary>
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
    '    ''' <summary>U�ivatelsk� jm�no</summary>
    '    Public MustOverride ReadOnly Property UserName() As String Implements IUser.UserName
    '    ''' <summary>Nastav� heslo</summary>
    '    ''' <remarks>V�dy pracuje p��mo nad datab�z�</remarks>
    '    ''' <exception cref="MySql.Data.MySqlClient.MySqlException">Cyhba p�i nastaven� hesla</exception>
    '    Public WriteOnly Property Password() As String
    '        Set(ByVal value As String)
    '            Dim tbl As New LoginDataTableAdapters.LoginQ
    '            tbl.SetPwd(value, ID)
    '        End Set
    '    End Property
    '    ''' <summary>Jm�no</summary>
    '    Public MustOverride ReadOnly Property Jmeno() As String Implements IUser.Jmeno
    '    ''' <summary>P��jmen�</summary>
    '    Public MustOverride ReadOnly Property Prijmeni() As String Implements IUser.Prijmeni
    '    ''' <summary>Pozn�mka</summary>
    '    Public MustOverride ReadOnly Property Poznamka() As String
    '    ''' <summary>N�zev asociovan�ho ��tu ActiveDirectory</summary>
    '    Public MustOverride ReadOnly Property ADName() As String Implements IUser.ADName

    '    ''' <summary>Ov��� u�ivatele na z�klad� jm�na <paramref name="UserName"/> a hesla <paramref name="Password"/></summary>
    '    ''' <param name="UserName">U�ivatelsk� jm�no</param>
    '    ''' <param name="Password">Heslo</param>
    '    ''' <returns>ID u�ivatele se zadan�m jm�nem a heslem</returns>
    '    ''' <exception cref="UserNotFoundException">U�ivatel se zadan�m jm�nem a heslem neexistuje</exception>
    '    Public Shared Function Verify(ByVal UserName As String, ByVal Password As String) As Integer
    '        Dim tbl As New LoginDataTableAdapters.LoginQ
    '        Try
    '            Return tbl.VerifyLogin(UserName, Password).Value
    '        Catch ex As Exception
    '            Throw New UserNotFoundException("U�ivatel se zadan�m jm�nem neexistuje nebo heslo nen� spr�vn�", ex)
    '        End Try
    '    End Function
    '    ''' <summary>Ov��� opr�vn�n� u�ivatele</summary>
    '    ''' <param name="Right">N�zev opr�vn�n�</param>
    '    ''' <returns>True pokud u�ivatel m� zadan� opr�vn�n�</returns>
    '    ''' <remarks>�ah� do datab�ze</remarks>
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


    '''' <summary>Reprezentuje u�ivatele syst�mu</summary>
    '''' <remarks>Tato t��da tah� data v�dy p��mo z datab�ze</remarks>
    'Public Class OnlineUser : Inherits User
    '    ''' <summary>CTor podle ID</summary>
    '    ''' <param name="ID">ID u�ivatele</param>
    '    ''' <exception cref="UserNotFoundException">U�ivatel se zadan�m ID <paramref name="ID"/> nenalezen</exception>
    '    Public Sub New(ByVal ID As Integer)
    '        Dim tbl As New LoginDataTableAdapters.LoginQ
    '        Try
    '            If Not tbl.VerifyUserID(ID).Value Then Throw New UserNotFoundException(String.Format("U�ivatel ID {0} nenalezen", ID))
    '        Catch ex As Exception
    '            Throw New UserNotFoundException(String.Format("U�ivatel ID {0} nenalezen", ID), ex)
    '        End Try
    '        _ID = ID
    '    End Sub
    '    ''' <summary>CTor pod u�ivatelsk�ho jm�na</summary>
    '    ''' <param name="Name">U�ivatelsk� jm�no</param>
    '    ''' <exception cref="UserNotFoundException">U�ivatele se zadan�m jm�nem <paramref name="Name"/> nenalezen</exception>
    '    Public Sub New(ByVal Name As String)
    '        Dim tbl As New LoginDataTableAdapters.LoginQ
    '        Try
    '            _ID = tbl.GetIDByName(Name).Value
    '        Catch ex As Exception
    '            Throw New UserNotFoundException(String.Format("U�ivatel {0} nenalezen", Name), ex)
    '        End Try
    '    End Sub
    '    ''' <summary>U�ivatelsk� jm�no</summary>
    '    Public Overrides ReadOnly Property UserName() As String
    '        Get
    '            Dim tbl As New LoginDataTableAdapters.LoginQ
    '            Return tbl.GetUserName(ID)
    '        End Get
    '    End Property
    '    ''' <summary>Jm�no</summary>
    '    Public Overrides ReadOnly Property Jmeno() As String
    '        Get
    '            Dim tbl As New LoginDataTableAdapters.LoginQ
    '            Return tbl.GetJmeno(ID)
    '        End Get
    '    End Property
    '    ''' <summary>P��jmen�</summary>
    '    Public Overrides ReadOnly Property Prijmeni() As String
    '        Get
    '            Dim tbl As New LoginDataTableAdapters.LoginQ
    '            Return tbl.GetPrijmeni(ID)
    '        End Get
    '    End Property
    '    ''' <summary>Pozn�mka</summary>
    '    Public Overrides ReadOnly Property Poznamka() As String
    '        Get
    '            Dim tbl As New LoginDataTableAdapters.LoginQ
    '            Return tbl.GetPozn(ID)
    '        End Get
    '    End Property
    '    ''' <summary>N�zev asociovan�ho ��tu AD</summary>
    '    Public Overrides ReadOnly Property ADName() As String
    '        Get
    '            Dim tbl As New LoginDataTableAdapters.LoginQ
    '            Return tbl.GetUID(ID)
    '        End Get
    '    End Property
    'End Class

    'Public Class OfflineUser : Inherits User
    '    ''' <summary>CTor podle ID</summary>
    '    ''' <param name="ID">ID u�ivatele</param>
    '    ''' <exception cref="UserNotFoundException">U�ivatel se zadan�m ID <paramref name="ID"/> nenalezen</exception>
    '    Public Sub New(ByVal ID As Integer)
    '        _ID = ID
    '        Init(ID)
    '    End Sub
    '    ''' <summary>CTor pod u�ivatelsk�ho jm�na</summary>
    '    ''' <param name="Name">U�ivatelsk� jm�no</param>
    '    ''' <exception cref="UserNotFoundException">U�ivatele se zadan�m jm�nem <paramref name="Name"/> nenalezen</exception>
    '    Public Sub New(ByVal Name As String)
    '        Dim tbl As New LoginDataTableAdapters.LoginQ
    '        Try
    '            _ID = tbl.GetIDByName(Name).Value
    '        Catch ex As Exception
    '            Throw New UserNotFoundException(String.Format("U�ivatel {0} nenalezen", Name), ex)
    '        End Try
    '        init(ID)
    '    End Sub
    '    ''' <summary>Inicilaizuje u�ivatele ID�kem</summary>
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
    '            Throw New UserNotFoundException(String.Format("U�ivatel s ID {0} nenalezen", ID), ex)
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
    '    ''' <summary>Jm�no</summary>
    '    Public Overrides ReadOnly Property Jmeno() As String
    '        Get
    '            Return _Jmeno
    '        End Get
    '    End Property
    '    ''' <summary>P��jmen�</summary>
    '    Public Overrides ReadOnly Property Prijmeni() As String
    '        Get
    '            Return _Prijmeni
    '        End Get
    '    End Property
    '    ''' <summary>U�ivatelsk� jm�no</summary>
    '    Public Overrides ReadOnly Property UserName() As String
    '        Get
    '            Return _UserName
    '        End Get
    '    End Property
    '    ''' <summary>Pozn�mka</summary>
    '    Public Overrides ReadOnly Property Poznamka() As String
    '        Get
    '            Return _Poznamka
    '        End Get
    '    End Property
    '    ''' <summary>N�uev asociovan�ho ��tu Active Directory</summary>
    '    Public Overrides ReadOnly Property ADName() As String
    '        Get
    '            Return _AD
    '        End Get
    '    End Property
    'End Class

    ''' <summary>Nast�v� p�i nenalezen� u�ivatele</summary>
    Public Class UserNotFoundException : Inherits Exception
        ''' <summary>Initializes a new instance of the System.Exception class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        ''' <param name="message">The error message that explains the reason for the exception.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        Public Sub New(ByVal Message As String, Optional ByVal InnerException As Exception = Nothing)
            MyBase.New(Message, InnerException)
        End Sub
    End Class
End Namespace