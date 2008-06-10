Namespace Login
    ''' <summary>Spoleèné rozhraní pro uživatele</summary>
    Public Interface IUser
        ''' <summary>Jméno</summary>
        ReadOnly Property Jmeno$()
        ''' <summary>Pøíjmení</summary>
        ReadOnly Property Prijmeni$()
        ''' <summary>ID v MySQL</summary>
        ReadOnly Property ID() As Integer
        ''' <summary>Ovìøení oprávnìní</summary><param name="Right">Oprávnìní k ovìøení</param>
        Function HasRight(ByVal Right$) As Boolean
        ''' <summary>Jméno v ActiveDirectory (vè. domény user@doména)</summary>
        ''' <remarks>Pro AD uživatele stejné jako <see cref="ADName"/></remarks>
        ReadOnly Property ADName$()
        ''' <summary>Uživatelské jméno</summary>
        ReadOnly Property UserName$()
        ''' <summary>Textová reprezentace</summary>
        Function ToString$()
    End Interface
End Namespace