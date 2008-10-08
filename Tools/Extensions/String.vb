Imports System.Runtime.CompilerServices
Imports System.Globalization

Namespace ExtensionsT
    ''' <summary>Contains extension methods for working with <see cref="System.String"/></summary>
    <Author("functionĐonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
 <FirstVersion(2008, 5, 1), Version(1, 0, GetType([StringExtensions]))> _
 Public Module [StringExtensions]
        ''' <summary>Indicates whether the specified <see cref="System.String" /> object is null or an <see cref="System.String.Empty" /> string.</summary>
        ''' <param name="s">A <see cref="System.String" /> reference.</param>
        ''' <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
        ''' <seealso cref="System.String.IsNullOrEmpty"/>
        <Extension()> Public Function IsNullOrEmpty(ByVal s As String) As Boolean
            Return String.IsNullOrEmpty(s)
        End Function
        ''' <summary>Replaces the format item in a specified <see cref="System.String" /> with the text equivalent of the value of a corresponding <see cref="System.Object" /> instance in a specified array. A specified parameter supplies culture-specific formatting information.</summary>
        ''' <param name="s">A composite format string.</param>
        ''' <param name="args">An <see cref="System.Object" /> array containing zero or more objects to format.</param>
        ''' <returns>A copy of format in which the format items have been replaced by the <see cref="System.String" /> equivalent of the corresponding instances of <see cref="System.Object" /> in args.</returns>
        ''' <exception cref="System.ArgumentNullException">format or args is null.</exception>
        ''' <exception cref="System.FormatException"><paramref name="s"/> is invalid composite format string.-or- The number indicating an argument to format is less than zero, or greater than or equal to the length of the args array.</exception>
        ''' <seealso cref="System.String.Format"/><seealso cref="f"/>
        <Extension()> Public Function Format(ByVal s As String, ByVal ParamArray args As Object()) As String
            Return String.Format(s, args)
        End Function
        ''' <summary>Replaces the format item in a specified <see cref="System.String" /> with the text equivalent of the value of a corresponding <see cref="System.Object" /> instance in a specified array. A specified parameter supplies culture-specific formatting information.</summary>
        ''' <param name="s">A composite format string.</param>
        ''' <param name="args">An <see cref="System.Object" /> array containing zero or more objects to format.</param>
        ''' <returns>A copy of format in which the format items have been replaced by the <see cref="System.String" /> equivalent of the corresponding instances of <see cref="System.Object" /> in args.</returns>
        ''' <exception cref="System.ArgumentNullException">format or args is null.</exception>
        ''' <exception cref="System.FormatException"><paramref name="s"/> is invalid composite format string.-or- The number indicating an argument to format is less than zero, or greater than or equal to the length of the args array.</exception>
        ''' <seealso cref="System.String.Format"/><seealso cref="Format"/>
        ''' <remarks>This function is shortcut alias of <see cref="Format"/></remarks>
        <Extension()> Public Function f(ByVal s As String, ByVal ParamArray args As Object()) As String
            Return String.Format(s, args)
        End Function
        ''' <summary>Concatenates a specified separator <see cref="System.String" /> between each element of a specified <see cref="System.String" /> array, yielding a single concatenated string.</summary>
        ''' <param name="separator">A <see cref="System.String" /> to separate items with.</param>
        ''' <param name="strarr">An array of <see cref="System.String" />.</param>
        ''' <returns>A <see cref="System.String" /> consisting of the elements of value interspersed with the separator string.</returns>
        ''' <exception cref="System.ArgumentNullException"><paramref name="strarr"/> is null.</exception>
        ''' <seealso cref="System.String.Join"/>
        <Extension()> Public Function Join(ByVal strarr As String(), ByVal separator As String) As String
            Return String.Join(separator, strarr)
        End Function
        ''' <summary>Concatenates a specified separator <see cref="System.String" /> between each element of a specified <see cref="System.String" /> array, yielding a single concatenated string.</summary>
        ''' <param name="separator">A <see cref="System.String" /> to separate items with.</param>
        ''' <param name="strs">A collection of <see cref="System.String" />.</param>
        ''' <returns>A <see cref="System.String" /> consisting of the elements of value interspersed with the separator string.</returns>
        ''' <exception cref="System.ArgumentNullException"><paramref name="strs"/> is null.</exception>
        <Extension()> Public Function Join(ByVal strs As IEnumerable(Of String), ByVal separator As String) As String
            Dim b As New System.Text.StringBuilder
            Dim i As Integer = 0
            For Each item In strs
                If i > 0 Then b.Append(separator)
                b.Append(item)
                i += 1
            Next
            Return b.ToString
        End Function
        ''' <summary>Returns a zero-based, one-dimensional array containing a specified number of substrings.</summary>
        ''' <param name="Expression">Required. String expression containing substrings and delimiters.</param>
        ''' <param name="Delimiter">Optional. Any single character used to identify substring limits. If Delimiter is omitted, the space character (" ") is assumed to be the delimiter.</param>
        ''' <param name="Limit">Optional. Maximum number of substrings into which the input string should be split. The default, –1, indicates that the input string should be split at every occurrence of the Delimiter string.</param>
        ''' <returns>String array. If Expression is a zero-length string (""), Split returns a single-element array containing a zero-length string. If Delimiter is a zero-length string, or if it does not appear anywhere in Expression, Split returns a single-element array containing the entire Expression string.</returns>
        ''' <seelaso cref="String.Split"/><seelaso cref="Microsoft.VisualBasic.Split"/>
        ''' <remarks>This function internally calls <see cref="Microsoft.VisualBasic.Split"/></remarks>
        <Extension()> Public Function Split(ByVal Expression As String, ByVal Delimiter As String, Optional ByVal Limit As Integer = -1) As String()
            Return Microsoft.VisualBasic.Split(Expression, Delimiter, Limit, CompareMethod.Binary)
        End Function
        ''' <summary>Splits given string by whitespaces</summary>
        ''' <param name="str">String to split</param>
        ''' <returns>Array of non-whitespace blocks in <paramref name="str"/>. In case <paramref name="str"/> is null, returns null.</returns>
        <Extension()> Public Function WhiteSpaceSplit(ByVal str As String) As String()
            If str Is Nothing Then Return Nothing
            If str.Length = 0 Then Return New String() {}
            str &= " "c
            Dim ret As New List(Of String)
            Dim LastPos As Integer = 0
            Dim InWhite As Boolean = True
            For i = 0 To str.Length - 1
                If str(i).IsWhiteSpace AndAlso Not InWhite Then
                    ret.Add(str.Substring(LastPos, i - LastPos))
                    InWhite = True
                ElseIf Not str(i).IsWhiteSpace() AndAlso InWhite Then
                    LastPos = i
                    InWhite = False
                End If
            Next
            Return ret.ToArray
        End Function
    End Module
End Namespace