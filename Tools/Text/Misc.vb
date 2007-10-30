Imports System.Runtime.CompilerServices

#If Config <= Nightly Then 'Stage: Nightly
Namespace TextT
    ''' <summary>Miscaleneous text tools</summary>
    <DoNotApplyAuthorAndVersionAttributes()> _
  Public Module Misc
        ''' <summary>Normalizes all whitespaces in given <see cref="String"/></summary>
        ''' <param name="Str"><see cref="String"/> to normalize</param>
        ''' <param name="KeepType">True to keep type of whitespace (Endline, Tab, Space; first in group is used) or False to replace all whitespaces with spaces</param>
        ''' <returns><see cref="String"/> with removed white characters at the beginning and at the end and reduced all groups of whitespaces to one white space</returns>
#If VBC_VER >= 9 Then
        <Extension()> _
        <Author("�onny", "dzonny.dz@gmail.com", "http://dzonny.cz"), Version(1, 0, GetType(Misc), LastChMMDDYYYY:="10/30/2007")> _
        <StandAloneTool(FirstVerMMDDYYYY:="03/11/2007")> _
        Public Function MTrim$(ByVal Str$, Optional ByVal KeepType As Boolean = False)
#Else
        <Author("�onny", "dzonny.dz@gmail.com", "http://dzonny.cz"), Version(1, 0, GetType(Misc), LastChMMDDYYYY:="10/30/2007")> _
	    <StandAloneTool(FirstVerMMDDYYYY:="03/11/2007")> _
        Public Function MTrim$(ByVal Str$, Optional ByVal KeepType As Boolean = False)
#End If
            Str = Str.Trim(New Char() {" "c, vbTab, vbCr, vbLf})
            Dim ret As New System.Text.StringBuilder
            Dim InWh As Boolean = False
            For i As Integer = 0 To Str.Length - 1
                Select Case InWh
                    Case False
                        Select Case Str(i)
                            Case " "c, vbLf, vbTab
                                If KeepType Then ret.Append(Str(i)) Else ret.Append(" "c)
                                InWh = True
                            Case vbCr
                                If KeepType Then
                                    If Str(i + 1) = vbLf Then ret.Append(vbCrLf) Else ret.Append(vbCr)
                                Else
                                    ret.Append(" "c)
                                End If
                                InWh = True
                            Case Else : ret.Append(Str(i))
                        End Select
                    Case True
                        Select Case Str(i)
                            Case " "c, vbCr, vbTab, vbLf
                            Case Else
                                ret.Append(Str(i))
                                InWh = False
                        End Select
                End Select
            Next i
            Return ret.ToString
        End Function
    End Module
End Namespace
#End If