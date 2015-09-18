
#If True
''' <summary>Represents tri-state "boolean"</summary>
''' <author www="http://dzonny.cz">Đonny</author>
''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
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