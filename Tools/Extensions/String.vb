Imports System.Runtime.CompilerServices
Imports System.Globalization
#If True  'Stage: Nightly
Namespace ExtensionsT
    ''' <summary>Contains extension methods for working with <see cref="System.String"/></summary>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Module [StringExtensions]
        ''' <summary>Indicates whether the specified <see cref="System.String" /> object is null or an <see cref="System.String.Empty" /> string.</summary>
        ''' <param name="s">A <see cref="System.String" /> reference.</param>
        ''' <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
        ''' <seealso cref="System.String.IsNullOrEmpty"/>
        <Extension()> Public Function IsNullOrEmpty(ByVal s As String) As Boolean
            Return String.IsNullOrEmpty(s)
        End Function
        ''' <summary>Indicates whether a specified string is null, empty, or consists only of white-space characters.</summary>
        ''' <param name="s">The string to test.</param>
        ''' <returns>true if the <paramref name="s"/> parameter is null or <see cref="System.String.Empty" />, or if <paramref name="s"/> consists exclusively of white-space characters.</returns>
        ''' <seelaso cref="System.String.IsNullOrWhiteSpace"/>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()> Public Function IsNullOrWhiteSpace(ByVal s As String) As Boolean
            Return String.IsNullOrWhiteSpace(s)
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
        ''' <param name="provider"> An object that supplies culture-specific formatting information.</param>
        ''' <param name="args">An <see cref="System.Object" /> array containing zero or more objects to format.</param>
        ''' <returns>A copy of format in which the format items have been replaced by the <see cref="System.String" /> equivalent of the corresponding instances of <see cref="System.Object" /> in args.</returns>
        ''' <exception cref="System.ArgumentNullException">format or args is null.</exception>
        ''' <exception cref="System.FormatException"><paramref name="s"/> is invalid composite format string.-or- The number indicating an argument to format is less than zero, or greater than or equal to the length of the args array.</exception>
        ''' <seealso cref="System.String.Format"/><seealso cref="f"/>
        ''' <version version="1.5.3">This overload is new in version 1.5.3</version>
        <Extension()> Public Function Format(ByVal s As String, ByVal provider As IFormatProvider, ByVal ParamArray args As Object()) As String
            Return String.Format(provider, s, args)
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
        ''' <summary>Replaces the format item in a specified <see cref="System.String" /> with the text equivalent of the value of a corresponding <see cref="System.Object" /> instance in a specified array. A specified parameter supplies culture-specific formatting information.</summary>
        ''' <param name="s">A composite format string.</param>
        ''' <param name="provider"> An object that supplies culture-specific formatting information.</param>
        ''' <param name="args">An <see cref="System.Object" /> array containing zero or more objects to format.</param>
        ''' <returns>A copy of format in which the format items have been replaced by the <see cref="System.String" /> equivalent of the corresponding instances of <see cref="System.Object" /> in args.</returns>
        ''' <exception cref="System.ArgumentNullException">format or args is null.</exception>
        ''' <exception cref="System.FormatException"><paramref name="s"/> is invalid composite format string.-or- The number indicating an argument to format is less than zero, or greater than or equal to the length of the args array.</exception>
        ''' <seealso cref="System.String.Format"/><seealso cref="Format"/>
        ''' <remarks>This function is shortcut alias of <see cref="Format"/></remarks>
        ''' <version version="1.5.3">This overload is new in version 1.5.3</version>
        <Extension()> Public Function f(ByVal s As String, ByVal provider As IFormatProvider, ByVal ParamArray args As Object()) As String
            Return String.Format(provider, s, args)
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
        ''' <summary>Reverses a string</summary>
        ''' <param name="value">String to reverse</param>
        ''' <returns><paramref name="value"/> with reversed order of characters. Null when <paramref name="value"/> is null.</returns>
        ''' <version version="1.5.2">Function introduced</version>
        <Extension()> _
        Public Function Reverse(ByVal value As String) As String
            If value Is Nothing Then Return Nothing
            If value = "" Then Return ""
            Dim ret As New System.Text.StringBuilder(value.Length)
            For i As Integer = value.Length - 1 To 0 Step -1
                ret.Append(value(i))
            Next
            Return ret.ToString
        End Function

        ''' <summary>Returns the substring of the value of <paramref name="value"/> that follows in the value of <paramref name="delimiter"/> the first occurrence</summary>
        ''' <param name="value">String to return substring of</param>
        ''' <param name="delimiter">Substring to search for</param>
        ''' <returns>
        ''' Rest of <paramref name="value"/> following first occurence of <paramref name="delimiter"/>.
        ''' If <paramref name="delimiter"/> is null or an empty string returns <paramref name="value"/>.
        ''' If <paramref name="value"/> does not contain <paramref name="delimiter"/> (or <paramref name="value"/> is null) returns an empty string.
        ''' </returns>
        ''' <remarks>This function implements XPath function <a href="http://www.w3.org/TR/xpath-functions/#func-substring-after">substring-after</a>.</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()>
        Function SubstringAfter(ByVal value As String, ByVal delimiter As String) As String
            If delimiter = "" OrElse value = "" Then Return value
            If Not value.Contains(delimiter) Then Return ""
            Return value.Substring(value.IndexOf(delimiter) + delimiter.Length)
        End Function
        ''' <summary>Returns the substring of the value of <paramref name="value"/> that precedes in the value of <paramref name="delimiter"/> the first occurrence</summary>
        ''' <param name="value">String to return substring of</param>
        ''' <param name="delimiter">Substring to search for</param>
        ''' <returns>
        ''' Part of <paramref name="value"/> from start to start of first occurence of <paramref name="delimiter"/> (exluding it).
        ''' If <paramref name="delimiter"/> is null or an empty string or <paramref name="value"/> does not contain <paramref name="delimiter"/> (or <paramref name="value"/> is null) returns an empty string.
        ''' </returns>
        ''' <remarks>This function implements XPath function <a href="http://www.w3.org/TR/xpath-functions/#func-substring-before">substring-before</a>.</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()>
        Function SubstringBefore(ByVal value As String, ByVal delimiter As String) As String
            If delimiter = "" OrElse value = "" OrElse Not value.Contains(delimiter) Then Return ""
            Return value.Substring(0, value.IndexOf(delimiter))
        End Function
    End Module
End Namespace
#End If