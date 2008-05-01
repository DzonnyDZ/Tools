Imports System.Runtime.CompilerServices
Imports System.Globalization

Namespace ExtensionsT
    ''' <summary>Contains extension methods for working with <see cref="System.String"/></summary>
    <Author("functionĐonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
 <FirstVersion(2008, 5, 1), Version(1, 0, GetType([String]))> _
  Module [String]
        ''' <summary>Indicates whether the specified <see cref="System.String" /> object is null or an <see cref="System.String.Empty" /> string.</summary>
        ''' <param name="s">A <see cref="System.String" /> reference.</param>
        ''' <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
        ''' <seealso cref="System.String.IsNullOrEmpty"/>
        <Extension()> Public Function IsNullOrEmpry(ByVal s As String) As Boolean
            Return String.IsNullOrEmpty(s)
        End Function
        ''' <summary>Replaces the format item in a specified <see cref="System.String" /> with the text equivalent of the value of a corresponding <see cref="System.Object" /> instance in a specified array. A specified parameter supplies culture-specific formatting information.</summary>
        ''' <param name="s">A composite format string.</param>
        ''' <param name="args">An <see cref="System.Object" /> array containing zero or more objects to format.</param>
        ''' <returns>A copy of format in which the format items have been replaced by the <see cref="System.String" /> equivalent of the corresponding instances of <see cref="System.Object" /> in args.</returns>
        ''' <exception cref="System.ArgumentNullException">format or args is null.</exception>
        ''' <exception cref="System.FormatException"><paramref name="s"/> is invalid composite format string.-or- The number indicating an argument to format is less than zero, or greater than or equal to the length of the args array.</exception>
        ''' <seealso cref="System.String.Format"/>
        <Extension()> Public Function Format(ByVal s As String, ByVal ParamArray args As Object()) As String
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
            Next
            Return b.ToString
        End Function
    End Module
End Namespace