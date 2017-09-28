Imports System.Text
Imports System.Text.RegularExpressions

Namespace ExtensionsT
    ''' <summary>Wraps <see cref="StringBuilder"/> for purposes of use of appending of name of placeholder in some <see cref="StringFormatting"/> methods</summary>
    ''' <version version="1.5.6">This class is new in 1.5.6</version>
    Friend Class PlaceholderNameBuilder
        ''' <summary>The internal <see cref="StringBuilder"/></summary>
        Private ReadOnly builder As New StringBuilder
        ''' <summary>Optional regular expression which placeholder name must always satisfy.</summary>
        ''' <remarks>Any substring starting at beginning of placeholder name must always satisfy this regex. Ignored if null.</remarks>
        Private ReadOnly identifierPattern As Regex

        ''' <summary>CTor - creates a new instance of the <see cref="PlaceholderNameBuilder"/> class</summary>
        ''' <param name="identifierPattern">Optional regular expression which placeholder name must always satisfy. Any substring starting at beginning of placeholder name must always satisfy this regex. Ignored if null.</param>
        Public Sub New(identifierPattern As Regex)
            Me.identifierPattern = identifierPattern
        End Sub

        ''' <summary>Appends a copy of the specified string to this instance.</summary>
        ''' <param name="str$">The string to append.</param>
        ''' <returns>True if the value was fully accepted, false if it has been (at least partially) rejected</returns>
        ''' <exception cref="ArgumentOutOfRangeException">The internal <see cref="StringBuilder"/> reached <see cref="StringBuilder.MaxCapacity"/></exception>
        Public Function AppendX(str$) As Boolean
            If str <> "" AndAlso identifierPattern IsNot Nothing Then
                If Not identifierPattern.IsMatch(builder.ToString & str) Then 
                    Return False
                End If
            End If
            builder.Append(str)
            Return True
        End Function

        ''' <summary>Appends the string representation of a specified Unicode character to this instance.</summary>
        ''' <param name="character">The Unicode character to append.</param>
        ''' <returns>True if the value was accepted, false if it has been rejected</returns>
        ''' <exception cref="ArgumentOutOfRangeException">The internal <see cref="StringBuilder"/> reached <see cref="StringBuilder.MaxCapacity"/></exception>
        Public Function AppendX(character As Char) As Boolean
            If identifierPattern IsNot Nothing Then
                If Not identifierPattern.IsMatch(builder.ToString & character) Then Return False
            End If
            builder.Append(character)
            Return True
        End Function

        ''' <summary>Converts underlying <see cref="StringBuilder"/> to <see cref="String"/></summary>
        ''' <returns>String whose value is same as of underlying <see cref="StringBuilder"/></returns>
        Public Overrides Function ToString() As String
            Return builder.ToString
        End Function
    End Class
End Namespace