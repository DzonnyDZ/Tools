Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ConvertersT

    ''' <summary>Converter that test if value being converted relates to parameter</summary>
    ''' <remarks>This converter is intended as is one-way.</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class CompareConverterEx
        Implements IValueConverter
        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. If <paramref name="value"/> is null or <paramref name="parameter"/> is null, returns null. Otherwise returns boolean value indicating if <paramref name="value"/> equals to <paramref name="parameter"/> using <see cref="System.Object.Equals"/>.</returns>
        ''' <param name="value">The value produced by the binding source. Thsi value will be compared for equality with <paramref name="parameter"/>.</param>
        ''' <param name="targetType">Ignored. Always returns null or <see cref="Boolean"/></param>
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
        ''' </list></remarks>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If culture Is Nothing Then culture = System.Globalization.CultureInfo.CurrentCulture
            If value Is Nothing AndAlso Not TypeOf parameter Is String Then Return parameter Is Nothing
            If parameter Is Nothing Then Return value Is Nothing
            If TypeOf parameter Is String Then
                Dim parstr As String = parameter
                Dim compVal As String
                Dim op As String
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
                Else
                    op = "="
                    compVal = parstr
                    GoTo SkipQuotes
                End If
                Dim isString As Boolean = False
                If compVal.StartsWith("""") AndAlso compVal.EndsWith("""") AndAlso compVal <> """" Then
                    isString = True
                    compVal = compVal.Substring(1, compVal.Length - 2)
                End If
SkipQuotes:
                If value Is Nothing Then
                    Dim isNull = (Not isString AndAlso (compVal.ToLowerInvariant = "null" OrElse compVal.ToLowerInvariant = "nothing")) OrElse compVal = ""
                    If op = "=" Then Return isNull
                    If op = "<>" Then Return Not isNull
                    Return False
                ElseIf TypeOf value Is String Then
                    Dim ivalue$ = value
                    If Not isString AndAlso (compVal.ToLowerInvariant = "null" OrElse compVal.ToLowerInvariant = "nothing") Then
                        If op = "=" Then Return ivalue = ""
                        If op = "<>" Then Return ivalue <> ""
                    End If
                    Dim res = StringComparer.Create(culture, False).Compare(ivalue, compVal)
                    Return (res < 0 AndAlso (op = "<" OrElse op = "<=" OrElse op = "<>")) OrElse
                           (res = 0 AndAlso (op = "=" OrElse op = "<=" OrElse op = ">=")) OrElse
                           (res > 0 AndAlso (op = ">" OrElse op = ">=" OrElse op = "<>"))
                Else
                    Dim res As Integer
                    If TypeOf value Is [Enum] Then
                        Dim enumType = value.GetType
                        value = DirectCast(value, [Enum]).GetValue
                        Dim enVal As [Enum]
                        Try
                            enVal = [Enum].Parse(enumType, compVal, True)
                        Catch ex As Exception
                            Return False
                        End Try
                        compVal = enVal.GetValue.ToString(culture)
                    End If
                    If TypeOf value Is Integer Then
                        Dim ival As Integer
                        If Integer.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, Integer).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is UInteger Then
                        Dim ival As UInteger
                        If UInteger.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, UInteger).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is Short Then
                        Dim ival As Short
                        If Short.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, Short).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is UShort Then
                        Dim ival As UShort
                        If UShort.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, UShort).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is Byte Then
                        Dim ival As Byte
                        If Byte.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, Byte).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is SByte Then
                        Dim ival As SByte
                        If SByte.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, SByte).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is UShort Then
                        Dim ival As ULong
                        If ULong.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, ULong).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is Long Then
                        Dim ival As Long
                        If Long.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, Long).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is Single Then
                        Dim ival As Single
                        If Single.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, Single).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is Double Then
                        Dim ival As Double
                        If Double.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, Double).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is Decimal Then
                        Dim ival As Decimal
                        If Decimal.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, Decimal).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is DateTime Then
                        Dim ival As DateTime
                        If DateTime.TryParse(compVal, culture, Globalization.DateTimeStyles.AllowInnerWhite, ival) Then res = DirectCast(value, DateTime).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is DateTimeOffset Then
                        Dim ival As DateTimeOffset
                        If DateTimeOffset.TryParse(compVal, culture, Globalization.DateTimeStyles.AllowWhiteSpaces, ival) Then res = DirectCast(value, DateTimeOffset).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is TimeSpanFormattable Then
                        Dim ival As TimeSpanFormattable
                        If TimeSpanFormattable.TryParse(compVal, culture, ival) Then res = DirectCast(value, TimeSpanFormattable).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is TimeSpan Then
                        Dim ival As TimeSpan
                        If TimeSpan.TryParse(compVal, culture, ival) Then res = DirectCast(value, TimeSpan).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is Char Then
                        Dim ival As Char
                        If Char.TryParse(compVal, ival) Then res = DirectCast(value, Char).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is Boolean Then
                        Dim ival As Boolean
                        If Boolean.TryParse(compVal, ival) Then res = DirectCast(value, Boolean).CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is IntPtr Then
                        Dim ival As Long
                        If Long.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, IntPtr).ToInt64.CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is UIntPtr Then
                        Dim ival As ULong
                        If ULong.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, UIntPtr).ToUInt64.CompareTo(ival) Else Return False
                    ElseIf TypeOf value Is BigInteger Then
                        Dim ival As BigInteger = BigInteger.Zero
                        If BigInteger.TryParse(compVal, Globalization.NumberStyles.Any, culture, ival) Then res = DirectCast(value, BigInteger).CompareTo(ival) Else Return False
                    ElseIf op = "=" Then
                        Return value.Equals(compVal)
                    ElseIf op = "<>" Then
                        Return Not value.Equals(compVal)
                    Else
                        Return False
                    End If
                    Return (res < 0 AndAlso (op = "<" OrElse op = "<=" OrElse op = "<>")) OrElse
                         (res = 0 AndAlso (op = "=" OrElse op = "<=" OrElse op = ">=")) OrElse
                         (res > 0 AndAlso (op = ">" OrElse op = ">=" OrElse op = "<>"))
                End If
            End If
            Return value.Equals(parameter)
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