Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization, System.Globalization.CultureInfo
Imports System.Runtime.InteropServices

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter which converts a boolean value to one of given string values</summary>
    ''' <remarks>Values are passed to parameter as two pipe(|)-separated strings or as an array of strings with two elements in order TrueValue|FalseValue.</remarks>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    ''' <version version="1.5.4">Forward conversion is now <see cref="TypeConverter"/>-aware</version>
    Public Class ChoiceBooleanConverter
        Implements IValueConverter

        ''' <summary>Gets or sets value indicating if targe type is honored in conversion</summary>
        Public Property IgnoreTargetType As Boolean

        ''' <summary>Converts a value. </summary>
        ''' <returns>
        ''' A converted value. If the method returns null, the valid null value is used.
        ''' Returned value is one of string values passed to <paramref name="parameter"/>.
        ''' <see cref="DynamicCast">Dynamic convertsion</see> is attempted when target type does not accept <see cref="String"/>.
        ''' Returns null if <paramref name="value"/> is null.
        ''' </returns>
        ''' <param name="value">The value produced by the binding source.
        ''' Supported value types are <see cref="Boolean"/>,
        ''' <see cref="String"/> ("false", "0", an empty string and culture false string representation are considered false all other values are considered true),
        ''' <see cref="Visibility"/> (<see cref="Visibility.Visible"/> is considered true, all other values false),
        ''' <see cref="IConvertible"/> and any type that can be <see cref="DynamicCast">dynamically casted</see> to <see cref="Boolean"/>.</param>
        ''' <param name="targetType">The type of the binding target property. Ignored if null.</param>
        ''' <param name="parameter">The converter parameter to use.
        ''' Either <see cref="String"/> containing two pipe(|)-separated values (for true an false) or <see cref="String()"/> containing two items (0-true, 1-false).
        ''' Culture values fro true and false are used when this parameter is null.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="TypeMismatchException"><paramref name="parameter"/> is neither null, <see cref="String"/> nor <see cref="String()"/></exception>
        ''' <exception cref="IndexOutOfRangeException"><paramref name="parameter"/> is <see cref="String()"/> but it does not have elements 0 and 1.</exception>
        ''' <exception cref="InvalidCastException">Error (cannot convert) when converting <paramref name="value"/> to <see cref="Boolean"/> or return value to <paramref name="targetType"/>.</exception>
        ''' <exception cref="OverflowException">Error (overflow) when converting <paramref name="value"/> to <see cref="Boolean"/> or return value to <paramref name="targetType"/>.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Error (conversion operators found but no one is most specific) when converting <paramref name="value"/> to <see cref="Boolean"/> or return value to <paramref name="targetType"/>.</exception>
        ''' <version version="1.5.4">Final return value conversion is now <see cref="TypeConverter"/>-aware</version>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Dim val As Boolean
            If TypeOf value Is Boolean Then
                val = value
            ElseIf TypeOf value Is String Then
                Select Case CStr(value).ToLower(culture)
                    Case "false", "0", "", False.ToString(culture).ToLower(culture) : val = False
                    Case Else : val = True
                End Select
            ElseIf TypeOf value Is Visibility Then
                val = DirectCast(value, Visibility) = Visibility.Visible
            ElseIf TypeOf value Is IConvertible Then
                val = DirectCast(value, IConvertible).ToBoolean(culture)
            Else
                val = DynamicCast(Of Boolean)(value)
            End If
            Dim trueCh$ = Nothing
            Dim falseCh$ = Nothing
            GetParts(parameter, culture, trueCh, falseCh)
            Dim ret = If(val, trueCh, falseCh)
            If targetType Is Nothing OrElse IgnoreTargetType Then Return ret
            Return DynamicCast(ret, targetType, True)
        End Function

        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used. Returns boolean value according to <paramref name="value"/>. <see cref="DynamicCast">Dynamically converted</see> to <paramref name="targetType"/> when necessary.</returns>
        ''' <param name="value">The value that is produced by the binding target. Any value is converted to <see cref="String"/> using <see cref="System.Object.ToString"/> or <see cref="IConvertible.ToString"/>.</param>
        ''' <param name="targetType">The type to convert to. Ignored when null.</param>
        ''' <param name="parameter">The converter parameter to use. 
        ''' Either <see cref="String"/> containing two pipe(|)-separated values (for true an false) or <see cref="String()"/> containing two items (0-true, 1-false).
        ''' Culture values fro true and false are used when this parameter is null.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <seelaso cref="Convert"/>
        ''' <exception cref="TypeMismatchException"><paramref name="parameter"/> is neither null, <see cref="String"/> nor <see cref="String()"/></exception>
        ''' <exception cref="IndexOutOfRangeException"><paramref name="parameter"/> is <see cref="String()"/> but it does not have elements 0 and 1.</exception>
        ''' <exception cref="InvalidCastException">Error (cannot convert) when converting return value (<see cref="Boolean"/>) to <paramref name="targetType"/>.</exception>
        ''' <exception cref="OverflowException">Error (overflow) when converting return (<see cref="Boolean"/>) value to <paramref name="targetType"/>.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Error (conversion operators found but no one is most specific) when converting return value (<see cref="Boolean"/>) to <paramref name="targetType"/>.</exception>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If value Is Nothing Then Return Nothing
            Dim trueCh$ = Nothing
            Dim falseCh$ = Nothing
            GetParts(parameter, culture, trueCh, falseCh)
            Dim val$
            If TypeOf value Is String Then
                val = value
            ElseIf TypeOf value Is IConvertible Then
                val = DirectCast(value, IConvertible).ToString(culture)
            Else
                val = value.ToString
            End If
            Dim ret As Boolean
            Select Case val
                Case trueCh : ret = True
                Case falseCh : ret = False
                Case Else : Return Nothing
            End Select
            If targetType Is Nothing OrElse IgnoreTargetType Then Return ret
            Return DynamicCast(ret, targetType)
        End Function

        ''' <summary>Gets trua ena false values from parameter</summary>
        ''' <param name="parameter">A parameter passed to converter</param>
        ''' <param name="culture">CUlture to be used</param>
        ''' <param name="trueCh">Output: Returns string for True</param>
        ''' <param name="falseCh">Output: Returns string for false</param>
        ''' <exception cref="TypeMismatchException"><paramref name="parameter"/> is neither null, <see cref="String"/> nor <see cref="String()"/></exception>
        ''' <exception cref="IndexOutOfRangeException"><paramref name="parameter"/> is <see cref="String()"/> but it does not have elements 0 and 1.</exception>
        Private Shared Sub GetParts(ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo, <Out()> ByRef trueCh$, <Out()> ByRef falseCh$)
            If parameter Is Nothing Then
                trueCh = True.ToString(culture)
                falseCh = False.ToString(culture)
            ElseIf TypeOf parameter Is String() Then
                trueCh = DirectCast(parameter, String())(0)
                falseCh = DirectCast(parameter, String())(1)
            ElseIf Not TypeOf parameter Is String Then
                Throw New TypeMismatchException(parameter, "parameter", GetType(String), GetType(String()))
            Else
                Dim parts = CStr(parameter).Split("|", 2)
                trueCh = parts(0)
                If parts.Length > 1 Then falseCh = parts(1) Else falseCh = False.ToString(culture)
            End If
        End Sub
    End Class
End Namespace
#End If