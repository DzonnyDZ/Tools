Imports System.Runtime.CompilerServices

#If True
Namespace TextT
    ''' <summary>Miscaleneous text tools</summary>
    Public Module TextExtensions
        ''' <summary>Normalizes all whitespaces in given <see cref="String"/></summary>
        ''' <param name="Str"><see cref="String"/> to normalize</param>
        ''' <param name="KeepType">True to keep type of whitespace (Endline, Tab, Space; first in group is used) or False to replace all whitespaces with spaces</param>
        ''' <returns><see cref="String"/> with removed white characters at the beginning and at the end and reduced all groups of whitespaces to one white space</returns>
        ''' <author www="http://dzonny.cz">Ðonny</author>
        ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
        <Extension()> _
        Public Function MTrim$(ByVal Str$, Optional ByVal KeepType As Boolean = False)
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
        ''' <summary>Appends a formatted string, which contains zero or more format specifications, and the default line terminator to given <see cref="System.Text.StringBuilder"/>. Each format specification is replaced by the string representation of a corresponding object argument.</summary>
        ''' <param name="Target"><see cref="System.Text.StringBuilder"/> to append formatted string to</param>
        ''' <param name="Format">A composite format string.</param>
        ''' <param name="args">An array of objects to format.</param>
        ''' <returns><paramref name="Target"/></returns>
        ''' <seealso cref="String.Format"/><seealso cref="System.Text.StringBuilder.AppendLine"/><seealso cref="System.Text.StringBuilder.AppendFormat"/>
        <Extension()> _
        Public Function AppendLineFormat(ByVal Target As System.Text.StringBuilder, ByVal Format$, ByVal ParamArray args As Object()) As System.Text.StringBuilder
            Return Target.AppendLine(String.Format(Format, args))
        End Function
        ''' <summary>Appends a formatted string, which contains zero or more format specifications, and the default line terminator to given <see cref="System.Text.StringBuilder"/>. Each format specification is replaced by the string representation of a corresponding object argument.</summary>
        ''' <param name="Provider">An <see cref="System.IFormatProvider"/> that supplies culture-specific formatting information.</param>
        ''' <param name="Target"><see cref="System.Text.StringBuilder"/> to append formatted string to</param>
        ''' <param name="Format">A composite format string.</param>
        ''' <param name="args">An array of objects to format.</param>
        ''' <returns><paramref name="Target"/></returns>
        ''' <seealso cref="String.Format"/><seealso cref="System.Text.StringBuilder.AppendLine"/><seealso cref="System.Text.StringBuilder.AppendFormat"/>
        <Extension()> _
        Public Function AppendLineFormat(ByVal Provider As IFormatProvider, ByVal Target As System.Text.StringBuilder, ByVal Format$, ByVal ParamArray args As Object()) As System.Text.StringBuilder
            Return Target.AppendLine(String.Format(Provider, Format, args))
        End Function
    End Module
End Namespace
#End If