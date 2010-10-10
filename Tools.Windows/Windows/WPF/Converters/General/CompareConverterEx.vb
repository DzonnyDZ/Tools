Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ConvertersT

    ''' <summary>Converter that test if value being converted relates to parameter</summary>
    ''' <remarks>This converter is intended as one-way.</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class CompareConverterEx
        Implements IValueConverter
        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. If <paramref name="value"/> is null or <paramref name="parameter"/> is null, returns null. Otherwise returns boolean value indicating if <paramref name="value"/> equals to <paramref name="parameter"/> using <see cref="System.Object.Equals"/>.</returns>
        ''' <param name="value">The value produced by the binding source. This value will be compared with <paramref name="parameter"/>.</param>
        ''' <param name="targetType">Type returned by conversion. Supported types are <see cref="Boolean"/>, <see cref="Windows.Visibility"/> and <see cref="Nullable(Of T)"/> of these types</param>
        ''' <param name="parameter">Value to compare <paramref name="value"/> with. If parametr is <see cref="String"/> extended comparison is performed. Othervise parameter is tested for equality with <paramref name="value"/>.</param>
        ''' <param name="culture">Culture to convert string values to numbers/dates etc. Ignored when <paramref name="parameter"/> is not string.</param>
        ''' <remarks>Fully supported types are:
        ''' <list type="bullet">
        ''' <item>Null</item>
        ''' <item><see cref="Char"/></item>
        ''' <item><see cref="String"/></item>
        ''' <item><see cref="Boolean"/></item>
        ''' <item><see cref="SByte"/></item>
        ''' <item><see cref="Byte"/></item>
        ''' <item><see cref="Short"/></item>
        ''' <item><see cref="UShort"/></item>
        ''' <item><see cref="Integer"/></item>
        ''' <item><see cref="UInteger"/></item>
        ''' <item><see cref="Long"/></item>
        ''' <item><see cref="ULong"/></item>
        ''' <item><see cref="BigInteger"/></item>
        ''' <item><see cref="Single"/></item>
        ''' <item><see cref="Double"/></item>
        ''' <item><see cref="Decimal"/></item>
        ''' <item><see cref="DateTime"/></item>
        ''' <item><see cref="DateTimeOffset"/></item>
        ''' <item><see cref="TimeSpan"/></item>
        ''' <item><see cref="TimeSpanFormattable"/></item>
        ''' <item><see cref="IntPtr"/></item>
        ''' <item><see cref="UIntPtr"/></item>
        ''' <item><see cref="[Enum]"/></item>
        ''' </list>
        ''' When <paramref name="parameter"/> is <see cref="String"/> and starts with one of recognized substrings extended comparison is performed.
        ''' Recognized substrings (comparison operators) are >=, &lt;=, ==, =, &lt;>, !=, &lt;, >, ===, !==.
        ''' This is collection of comparison operator collected from Visual Basic, C# and JavaScript languages with expected behavior (==, =, !=, and &lt;> are type-independent; === and !== require type identity; enumeration and underlying type are treated as identical types).
        ''' If rest of the string after operator (or entire string when it does not start with recognized operator) starts and ends with either " (double quote) or ' (apostrophe) and is longer than one character first and last characters are ignored.
        ''' <note>
        ''' Quotes and aopostrophes on other places than first and last of string to compare with are treated as normal characters.
        ''' For example to test wheather <paramref name="value"/> equals to single apostrophe (') following values of <paramref name="parameter"/> can be used:
        ''' <c>'</c>, <c>'''</c>, <c>"'"</c>, <c>='</c>, <c>='''</c>, <c>="'"</c>. <c>=='</c>, <c>=='''</c>, <c>=="'"</c>.
        ''' </note>
        ''' </remarks>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If culture Is Nothing Then culture = System.Globalization.CultureInfo.CurrentCulture
            Dim ret As Boolean
            If value Is Nothing AndAlso Not TypeOf parameter Is String Then ret = parameter Is Nothing : GoTo [return]
            If parameter Is Nothing Then ret = value Is Nothing : GoTo [return]
            Dim castEx As Exception
            Dim DynamicCastAllowed = True
            Dim op$
            If TypeOf parameter Is String Then
                Dim parstr As String = parameter
                Dim compVal As String
                If parstr.StartsWith(">=") Then
                    op = ">=" : compVal = parstr.Substring(2)
                ElseIf parstr.StartsWith("<=") Then
                    op = "<=" : compVal = parstr.Substring(2)
                ElseIf parstr.StartsWith("==") Then
                    op = "=" : compVal = parstr.Substring(2)
                ElseIf parstr.StartsWith("=") Then
                    op = "=" : compVal = parstr.Substring(1)
                ElseIf parstr.StartsWith("<>") OrElse parstr.StartsWith("!=") Then
                    op = "<>" : compVal = parstr.Substring(2)
                ElseIf parstr.StartsWith("<") Then
                    op = "<" : compVal = parstr.Substring(1)
                ElseIf parstr.StartsWith(">") Then
                    op = ">" : compVal = parstr.Substring(1)
                ElseIf parstr.StartsWith("===") Then
                    op = "===" : compVal = parstr.Substring(3) : DynamicCastAllowed = False
                ElseIf parstr.StartsWith("!==") Then
                    op = "!==" : compVal = parstr.Substring(3) : DynamicCastAllowed = False
                Else
                    op = "="
                    compVal = parstr
                    GoTo SkipQuotes
                End If
                Dim isString As Boolean = False
                If (compVal.StartsWith("""") AndAlso compVal.EndsWith("""") AndAlso compVal <> """") OrElse (compVal.StartsWith("'") AndAlso compVal.EndsWith("'") AndAlso compVal <> "'") Then
                    isString = True
                    compVal = compVal.Substring(1, compVal.Length - 2)
                End If
SkipQuotes:
                'TODO: Comparison must be done in better way
                If value Is Nothing Then 'When value is null
                    Dim isNull = (Not isString AndAlso (compVal.ToLowerInvariant = "null" OrElse compVal.ToLowerInvariant = "nothing")) OrElse compVal = ""
                    If op = "=" Then Return isNull
                    If op = "<>" Then Return Not isNull
                    Return False
                ElseIf TypeOf value Is String Then 'When value is string
                    Dim ivalue$
                    Try
                        ivalue = If(DynamicCastAllowed, DynamicCast(Of String)(value), DirectCast(value, String))
                    Catch castEx
                        GoTo castEx
                    End Try
                    Select Case op
                        Case "===" : op = "="
                        Case "!==" : op = "<>"
                    End Select
                    If Not isString AndAlso (compVal.ToLowerInvariant = "null" OrElse compVal.ToLowerInvariant = "nothing") Then
                        If op = "=" Then ret = ivalue = "" : GoTo [return]
                        If op = "<>" Then ret = ivalue <> "" : GoTo [return]
                    End If
                    Dim res = StringComparer.Create(culture, False).Compare(ivalue, compVal)
                    ret = (res < 0 AndAlso (op = "<" OrElse op = "<=" OrElse op = "<>")) OrElse
                           (res = 0 AndAlso (op = "=" OrElse op = "<=" OrElse op = ">=")) OrElse
                           (res > 0 AndAlso (op = ">" OrElse op = ">=" OrElse op = "<>"))
                    GoTo [return]
                Else 'Numeric comparison
                    Dim res As Integer
                    If TypeOf value Is [Enum] Then
                        Dim enumType = value.GetType
                        value = DirectCast(value, [Enum]).GetValue
                        Dim enVal As [Enum]
                        Try
                            enVal = [Enum].Parse(enumType, compVal, True)
                        Catch ex As Exception
                            ret = False : GoTo [return]
                        End Try
                        compVal = enVal.GetValue.ToString(culture)
                    End If
                    Try
                        If TypeOf value Is Integer Then
                            Dim ival As Integer
                            If Integer.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of Integer)(value), DirectCast(value, Integer)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is UInteger Then
                            Dim ival As UInteger
                            If UInteger.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of UInteger)(value), DirectCast(value, UInteger)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is Short Then
                            Dim ival As Short
                            If Short.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of Short)(value), DirectCast(value, Short)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is UShort Then
                            Dim ival As UShort
                            If UShort.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of UShort)(value), DirectCast(value, UShort)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is Byte Then
                            Dim ival As Byte
                            If Byte.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of Byte)(value), DirectCast(value, Byte)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is SByte Then
                            Dim ival As SByte
                            If SByte.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of SByte)(value), DirectCast(value, SByte)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is UShort Then
                            Dim ival As ULong
                            If ULong.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of ULong)(value), DirectCast(value, ULong)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is Long Then
                            Dim ival As Long
                            If Long.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of Long)(value), DirectCast(value, Long)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is Single Then
                            Dim ival As Single
                            If Single.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of Single)(value), DirectCast(value, Single)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is Double Then
                            Dim ival As Double
                            If Double.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of Double)(value), DirectCast(value, Double)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is Decimal Then
                            Dim ival As Decimal
                            If Decimal.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of Decimal)(value), DirectCast(value, Decimal)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is DateTime Then
                            Dim ival As DateTime
                            If DateTime.TryParse(compVal, culture, Globalization.DateTimeStyles.AllowInnerWhite, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of DateTime)(value), DirectCast(value, DateTime)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is DateTimeOffset Then
                            Dim ival As DateTimeOffset
                            If DateTimeOffset.TryParse(compVal, culture, Globalization.DateTimeStyles.AllowWhiteSpaces, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of DateTimeOffset)(value), DirectCast(value, DateTimeOffset)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is TimeSpanFormattable Then
                            Dim ival As TimeSpanFormattable
                            If TimeSpanFormattable.TryParse(compVal, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of TimeSpanFormattable)(value), DirectCast(value, TimeSpanFormattable)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is TimeSpan Then
                            Dim ival As TimeSpan
                            If TimeSpan.TryParse(compVal, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of TimeSpan)(value), DirectCast(value, TimeSpan)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is Char Then
                            Dim ival As Char
                            If Char.TryParse(compVal, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of Char)(value), DirectCast(value, Char)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is Boolean Then
                            Dim ival As Boolean
                            If Boolean.TryParse(compVal, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of Boolean)(value), DirectCast(value, Boolean)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf TypeOf value Is IntPtr Then
                            Dim ival As Long
                            If Long.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of IntPtr)(value), DirectCast(value, IntPtr)).ToInt64.CompareTo(ival) Else Return False
                        ElseIf TypeOf value Is UIntPtr Then
                            Dim ival As ULong
                            If ULong.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of UIntPtr)(value), DirectCast(value, UIntPtr)).ToUInt64.CompareTo(ival) Else Return False
                        ElseIf TypeOf value Is BigInteger Then
                            Dim ival As BigInteger = BigInteger.Zero
                            If BigInteger.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = If(DynamicCastAllowed, DynamicCast(Of BigInteger)(value), DirectCast(value, BigInteger)).CompareTo(ival) Else ret = False : GoTo [return]
                        ElseIf op = "=" Then : ret = value.Equals(compVal) : GoTo [return]
                        ElseIf op = "===" Then : ret = (value.GetType.IsAssignableFrom(compVal.GetType) OrElse compVal.GetType.IsAssignableFrom(value.GetType)) AndAlso value.Equals(compVal) : GoTo [return]
                        ElseIf op = "<>" Then : ret = Not value.Equals(compVal) : GoTo [return]
                        ElseIf op = "!==" Then : ret = (Not value.GetType.IsAssignableFrom(compVal.GetType) OrElse Not compVal.GetType.IsAssignableFrom(value.GetType)) OrElse Not value.Equals(compVal) : GoTo [return]
                        Else : ret = False : GoTo [return]
                        End If
                    Catch castEx:GoTo castEx:End Try
                    'Interpret comparison result
                    Select Case op
                        Case "===" : op = "="
                        Case "!==" : op = "<>"
                    End Select
                    ret = (res < 0 AndAlso (op = "<" OrElse op = "<=" OrElse op = "<>")) OrElse
                     (res = 0 AndAlso (op = "=" OrElse op = "<=" OrElse op = ">=")) OrElse
                     (res > 0 AndAlso (op = ">" OrElse op = ">=" OrElse op = "<>"))
                End If
            Else
                ret = value.Equals(parameter)
            End If
[return]:   If targetType Is Nothing OrElse targetType.IsAssignableFrom(GetType(Boolean)) OrElse targetType.IsAssignableFrom(GetType(Boolean?)) Then
                Return ret
            ElseIf targetType.IsAssignableFrom(GetType(Windows.Visibility)) OrElse targetType.IsAssignableFrom(GetType(Windows.Visibility?)) Then
                Return If(ret, Windows.Visibility.Visible, Windows.Visibility.Collapsed)
            Else
                Throw New NotSupportedException("{0} can convert only to {0} and {1} and corresponding nullable types.")
            End If
castEx:
            'TODO:
            Select Case op
                Case "<=", ">=", "<>", "!==", "<", ">" : ret = True
                Case Else : ret = False                                 '=, ===
            End Select
            GoTo [return]
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <returns>If <paramref name="value"/> is true returns <paramref name="parameter"/>; otherwise throws an exception</returns>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="targetType">ignored</param>
        ''' <param name="parameter">Value this converter compares values to</param>
        ''' <param name="culture">ignored</param>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is not true</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If TypeOf value Is Boolean AndAlso DirectCast(value, Boolean) Then Return parameter
            Throw New NotSupportedException(ConverterResources.ex_CannotConvertBack.f(Me.GetType.Name))
        End Function
    End Class
End Namespace
#End If