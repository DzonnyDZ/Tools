Imports System.Text
Imports System.Globalization

Namespace TextT.UnicodeT

    ''' <summary>Provides binding to Windows-native Unicode-related functions</summary>
    ''' <version version="1.5.4">This module is new in version 1.5.4</version>
    Public Module UnicodeNative

        ''' <summary>Gets localized name uf Unicode character</summary>
        ''' <param name="codePoint">Code point to get localizaed name of</param>
        ''' <returns>
        ''' Localized name of character represented by <paramref name="codePoint"/>. The name is localized to current system locale, not to current thread UI culture.
        ''' Returns null if <paramref name="codePoint"/> is not supported by Windows function for getting character names (i.e. <paramref name="codePoint"/> is greater than <see cref="UInt16.MaxValue"/>) or if name cannot be obtained or windows function call failed.
        ''' </returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="codePoint"/> is greater than 0x10FFFF</exception>
        ''' <remarks>
        ''' <para>This function is not CLS-Compliant. CLS-compliant overload exists.</para>
        ''' <para>This function relies on undocumented Windows API <c>GetUName</c>. It is possible that this API will be changed or removed in future version of Windows and this function will become slow and start returning nullls for all calls. It can also possibly carsh your application or system. Use on own risk.</para>
        ''' </remarks>
        <CLSCompliant(False)>
        Public Function GetCharacterName(codePoint As UInteger) As String
            If codePoint > &H10FFFFUI Then Throw New ArgumentOutOfRangeException("codePoint")
            If codePoint > UShort.MaxValue Then Return Nothing

            Dim buff As New StringBuilder(1024)

            Dim ret%
            Try
                ret = API.Misc.GetUName(codePoint, buff)
            Catch
                Return Nothing
            End Try
            If ret <= 0 Then Return Nothing
            Return buff.ToString

        End Function


        ''' <summary>Gets localized name uf Unicode character (CLS-compliant version)</summary>
        ''' <param name="codePoint">Code point to get localizaed name of</param>
        ''' <returns>
        ''' Localized name of character represented by <paramref name="codePoint"/>. The name is localized to current system locale, not to current thread UI culture.
        ''' Returns null if <paramref name="codePoint"/> is not supported by Windows function for getting character names (i.e. <paramref name="codePoint"/> is greater than <see cref="UInt16.MaxValue"/>) or if name cannot be obtained or windows function call failed.
        ''' </returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="codePoint"/> is greater than 0x10FFFF or less than zero.</exception>
        ''' <remarks>
        ''' <para>This function is not CLS-Compliant. CLS-compliant overload exists.</para>
        ''' <para>This function relies on undocumented Windows API <c>GetUName</c>. It is possible that this API will be changed or removed in future version of Windows and this function will become slow and start returning nullls for all calls. It can also possibly carsh your application or system. Use on own risk.</para>
        ''' </remarks>
        Public Function GetCharacterName(codePoint As Integer) As String
            If codePoint < 0 Then Throw New ArgumentOutOfRangeException("codePoint")
            Return GetCharacterName(CUInt(codePoint))
        End Function


    End Module
End Namespace
