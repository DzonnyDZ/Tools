Namespace Login
    ''' <summary>Spole�n� rozhran� pro u�ivatele</summary>
    Public Interface IUser
        ''' <summary>Jm�no</summary>
        ReadOnly Property Jmeno$()
        ''' <summary>P��jmen�</summary>
        ReadOnly Property Prijmeni$()
        ''' <summary>ID v MySQL</summary>
        ReadOnly Property ID() As Integer
        ''' <summary>Ov��en� opr�vn�n�</summary><param name="Right">Opr�vn�n� k ov��en�</param>
        Function HasRight(ByVal Right$) As Boolean
        ''' <summary>Jm�no v ActiveDirectory (v�. dom�ny user@dom�na)</summary>
        ''' <remarks>Pro AD u�ivatele stejn� jako <see cref="ADName"/></remarks>
        ReadOnly Property ADName$()
        ''' <summary>U�ivatelsk� jm�no</summary>
        ReadOnly Property UserName$()
        ''' <summary>Textov� reprezentace</summary>
        Function ToString$()
    End Interface
End Namespace