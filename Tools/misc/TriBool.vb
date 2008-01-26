''' <summary>Represents tri-state "boolean"</summary>
#If Config <= Release Then
<Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(enmTriBool), LastChange:="12/21/2006")> _
<CLSCompliant(False)> _
Public Enum enmTriBool As SByte
    ''' <summary>False value</summary>
    [False] = False
    ''' <summary>True value</summary>
    [True] = True
    ''' <summary>Third value (called Unknowm, Default etc.)</summary>
    Unknown = 127
End Enum
#End If