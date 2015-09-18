Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter that can convert between <see cref="HorizontalAlignment"/> and <see cref="VerticalAlignment"/></summary>
    ''' <remarks>Conversion can be done in both was equivalently.</remarks>
    ''' <version stage="Alpha" version="1.5.2">Class introduced</version>
    Public NotInheritable Class HorizontalTextAlingConverter
        Implements IValueConverter
        ''' <summary>Converts <see cref="HorizontalAlignment"/> to <see cref="TextAlignment"/></summary>
        ''' <param name="a">A <see cref="HorizontalAlignment"/></param>
        ''' <returns><see cref="TextAlignment"/> with logically coresponding value to <paramref name="a"/></returns>
        Public Shared Function Convert(ByVal a As HorizontalAlignment) As TextAlignment
            Select Case a
                Case HorizontalAlignment.Center : Return TextAlignment.Center
                Case HorizontalAlignment.Right : Return TextAlignment.Right
                Case HorizontalAlignment.Stretch : Return TextAlignment.Justify
                Case Else : Return TextAlignment.Left
            End Select
        End Function
        ''' <summary>Converts <see cref="TextAlignment"/> to <see cref="HorizontalAlignment"/></summary>
        ''' <param name="a">A <see cref="TextAlignment"/></param>
        ''' <returns><see cref="HorizontalAlignment"/> with logically coresponding value to <paramref name="a"/></returns>
        Public Shared Function Convert(ByVal a As TextAlignment) As HorizontalAlignment
            Select Case a
                Case TextAlignment.Center : Return HorizontalAlignment.Left
                Case TextAlignment.Justify : Return HorizontalAlignment.Stretch
                Case TextAlignment.Right : Return HorizontalAlignment.Right
                Case Else : Return HorizontalAlignment.Left
            End Select
        End Function
        ''' <summary>Converts either <see cref="TextAlignment"/> to <see cref="HorizontalAlignment"/> or <see cref="HorizontalAlignment"/> to <see cref="TextAlignment"/></summary>
        ''' <param name="value">Either <see cref="TextAlignment"/> or <see cref="HorizontalAlignment"/> value</param>
        ''' <param name="targetType">Either <see cref="TextAlignment"/> or <see cref="HorizontalAlignment"/> type</param>
        ''' <param name="culture">Ignored</param>
        ''' <param name="parameter">Ignored</param>
        ''' <returns>When <paramref name="value"/> is of same type as <paramref name="targetType"/> returns <paramref name="value"/>; otherwise returns logically equivalent value of <paramref name="value"/> represented in <paramref name="targetType"/> type.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> or <paramref name="targetType"/> is null</exception>
        ''' <exception cref="NotSupportedException">Type of <paramref name="value"/> is not <paramref name="targetType"/> and either type of <paramref name="value"/> or <paramref name="targetType"/> is neither <see cref="HorizontalAlignment"/> nor <see cref="TextAlignment"/>.</exception>
        ''' <remarks>This converter never throws an exception when type of <paramref name="value"/> is same as <paramref name="targetType"/>.</remarks>
        Private Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert, System.Windows.Data.IValueConverter.ConvertBack
            If value Is Nothing Then Throw New ArgumentNullException("value")
            If targetType Is Nothing Then Throw New ArgumentNullException("targetType")
            If value.GetType.Equals(targetType) Then Return value
            If (Not TypeOf value Is System.Windows.HorizontalAlignment AndAlso Not TypeOf value Is System.Windows.TextAlignment) OrElse (Not targetType.Equals(GetType(System.Windows.HorizontalAlignment)) AndAlso Not targetType.Equals(GetType(System.Windows.TextAlignment))) Then _
                Throw New NotSupportedException(ResourcesT.Exceptions.ThisConverterCanConvertOnlyBetweenTypes0And1.f(GetType(System.Windows.HorizontalAlignment).Name, GetType(System.Windows.VerticalAlignment).Name))
            If TypeOf value Is System.Windows.HorizontalAlignment Then
                Return Convert(DirectCast(value, HorizontalAlignment))
            Else
                Return Convert(DirectCast(value, TextAlignment))
            End If
        End Function
    End Class
End Namespace
#End If