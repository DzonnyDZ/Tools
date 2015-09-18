Imports System.Data.SqlTypes
Imports System.Text.RegularExpressions

Namespace DataT.SqlServerT
#If True
    ''' <summary>Contains user-defined functions for SQL server</summary>
    ''' <version version="1.5.3">Class introduced</version>
    Partial Public NotInheritable Class UserDefinedFunctions
        ''' <summary>Private CTor - quasi-static class (C#-style)</summary>
        ''' <version version="1.5.4">Constructor added, because in Roslyn version of VB it's no longer possible to have class without any constructor</version>
        ''' <exception cref="NotImplementedException">Always</exception>
        Private Sub New()
            Throw New NotImplementedException("Do not construct static class")
        End Sub
        ''' <summary>Regular expression options used by <see cref="RegexMatch"/></summary>
        Public Shared ReadOnly Options As RegexOptions = RegexOptions.IgnorePatternWhitespace Or RegexOptions.Singleline Or RegexOptions.CultureInvariant
        ''' <summary>Runs regular expression match</summary>
        ''' <param name="Input">Characters to be matched</param>
        ''' <param name="Pattern">Regular expression pattern to test</param>
        ''' <returns>A <see cref="SqlBoolean"/> value; <see ctef="SqlBoolean.True"/> if <paramref name="Input"/> matches <paramref name="Pattern"/>; <see cref="SqlBoolean.False"/> otherwise.
        ''' When <paramref name="Input"/> is null or <paramref name="Input"/>.<see cref="SqlChars.IsNull">IsNull</see> is true patter is run against an empty string instead.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Pattern"/>.<see cref="SqlString.IsNull">IsNull</see> is true</exception>
        <Microsoft.SqlServer.Server.SqlFunction()>
        Public Shared Function RegexMatch(ByVal Input As SqlChars, ByVal Pattern As SqlString) As SqlBoolean
            If Pattern.IsNull Then Throw New ArgumentNullException("Pattern")
            Dim regex As New Regex(Pattern.Value, Options)
            If Input Is Nothing OrElse Input.IsNull Then
                Return regex.IsMatch("")
            End If
            Return regex.IsMatch(New String(Input.Value))
        End Function
    End Class
#End If
End Namespace
