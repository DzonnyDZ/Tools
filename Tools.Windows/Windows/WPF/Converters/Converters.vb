Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization
#If Config <= Alpha Then 'Stage: Aplha
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Implements <see cref="IValueConverter"/> which negates <see cref="Boolean"/> value</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Alpha"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Class NotBooleanValueConverter
        Inherits StronglyTypedConverter(Of Boolean, Boolean)
        ''' <summary>Converts a value - makes boolean negation of it.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">The converter parameter to use. Ignored.</param>
        ''' <param name="culture">The culture to use in the converter. Ignored.</param>
        ''' <returns>Boolean negation of <paramref name="value"/></returns>
        Public Overrides Function Convert(ByVal value As Boolean, ByVal parameter As Object, ByVal culture As CultureInfo) As Boolean
            Return Not value
        End Function
        ''' <summary>Converts a value - makes boolean negation of it.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="parameter">The converter parameter to use. Ignored.</param>
        ''' <param name="culture">The culture to use in the converter. Ignored.</param>
        ''' <returns>Boolean negation of <paramref name="value"/>.</returns>
        Public Overrides Function ConvertBack(ByVal value As Boolean, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Boolean
            Return Not value
        End Function
    End Class

    ''' <summary>Implements <see cref="IValueConverter"/> for converting numeric values to halfs of them</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Alpha"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Friend Class HalfConverter
        Implements IValueConverter
        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="ArgumentException"><paramref name="value"/> or <paramref name="targetType"/> is not supported.
        ''' Supported types are <see cref="Byte"/>, <see cref="SByte"/>, <see cref="Short"/>, <see cref="UShort"/>, <see cref="Integer"/>, <see cref="UInteger"/>, <see cref="Long"/>, <see cref="ULong"/>, <see cref="Single"/>, <see cref="Double"/> and <see cref="Decimal"/>
        ''' </exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return Convert(value, targetType, CSng(0.5))

        End Function

        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="ArgumentException"><paramref name="value"/> or <paramref name="targetType"/> is not supported.
        ''' Supported types are <see cref="Byte"/>, <see cref="SByte"/>, <see cref="Short"/>, <see cref="UShort"/>, <see cref="Integer"/>, <see cref="UInteger"/>, <see cref="Long"/>, <see cref="ULong"/>, <see cref="Single"/>, <see cref="Double"/> and <see cref="Decimal"/>
        ''' </exception>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Convert(value, targetType, CSng(2))
        End Function


        ''' <summary>Performs a conversion</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="targetType">Type of return value</param>
        ''' <param name="param">Multiplication constant</param>
        ''' <returns><paramref name="value"/> * <paramref name="param"/> in type <paramref name="targetType"/></returns>
        ''' <exception cref="ArgumentException"><paramref name="value"/>is of unsupported type or <paramref name="targetType"/> is unsupported</exception>
        Private Shared Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal param As Single) As Object
            Dim multiplied As Double
            If TypeOf value Is SByte Then
                multiplied = CDbl((CDbl(CSByte(value)) * param))
            ElseIf TypeOf value Is Byte Then
                multiplied = CDbl((CDbl(CByte(value)) * param))
            ElseIf TypeOf value Is Short Then
                multiplied = CDbl((CDbl(CShort(value)) * param))
            ElseIf TypeOf value Is UShort Then
                multiplied = CDbl((CDbl(CUShort(value)) * param))
            ElseIf TypeOf value Is Integer Then
                multiplied = CDbl((CDbl(CInt(value)) * param))
            ElseIf TypeOf value Is UInteger Then
                multiplied = CDbl((CDbl(CInt(value)) * param))
            ElseIf TypeOf value Is Long Then
                multiplied = CDbl((CDbl(CLng(value)) * param))
            ElseIf TypeOf value Is ULong Then
                multiplied = CDbl((CDbl(CLng(value)) * param))
            ElseIf TypeOf value Is Single Then
                multiplied = CDbl((CDbl(CSng(value)) * param))
            ElseIf TypeOf value Is Double Then
                multiplied = CDbl((CDbl(CDbl(value)) * param))
            ElseIf TypeOf value Is Decimal Then
                multiplied = CDbl((CDbl(CDec(value)) * param))
            Else
                Throw New ArgumentException(ResourcesT.Exceptions.UnsupportedSourceType, "value")
            End If
            If targetType.Equals(GetType(SByte)) Then
                Return CSByte(multiplied)
            ElseIf targetType.Equals(GetType(Byte)) Then
                Return CByte(multiplied)
            ElseIf targetType.Equals(GetType(Short)) Then
                Return CShort(multiplied)
            ElseIf targetType.Equals(GetType(UShort)) Then
                Return CUShort(multiplied)
            ElseIf targetType.Equals(GetType(Integer)) Then
                Return CInt(multiplied)
            ElseIf targetType.Equals(GetType(UInteger)) Then
                Return CInt(multiplied)
            ElseIf targetType.Equals(GetType(Long)) Then
                Return CLng(multiplied)
            ElseIf targetType.Equals(GetType(ULong)) Then
                Return CLng(multiplied)
            ElseIf targetType.Equals(GetType(Single)) Then
                Return CSng(multiplied)
            ElseIf targetType.Equals(GetType(Double)) Then
                Return CDbl(multiplied)
            ElseIf targetType.Equals(GetType(Decimal)) Then
                Return CDec(multiplied)
            Else
                Throw New ArgumentException(ResourcesT.Exceptions.UnsupportedTargetType, "targetType")
            End If
        End Function
    End Class

    ''' <summary>Onew-way converter that can indicate if value is not null</summary>
    ''' <remarks>Converts null to false and non-null to true</remarks>
    ''' <version stage="Alpha" version="1.5.2">Class introduced</version>
    Public NotInheritable Class IsNotNullConverter
        Inherits StronglyTypedConverter(Of Object, Boolean)

        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns>A converted value. False when <paramref name="value"/> is null; true otherwise.</returns>
        Public Overrides Function Convert(ByVal value As Object, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Boolean
            Return value IsNot Nothing
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns><see cref="Binding.DoNothing"/> unless <paramref name="value"/> is false, in such case returns null.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Function ConvertBack(ByVal value As Boolean, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object
            If value = False Then Return Nothing
            Return Binding.DoNothing
        End Function
    End Class
    ''' <summary>Onew-way converter that can indicate if value is null</summary>
    ''' <remarks>Converts null to true and non-null to false</remarks>
    ''' <version stage="Alpha" version="1.5.2">Class introduced</version>
    Public NotInheritable Class IsNullConverter
        Inherits StronglyTypedConverter(Of Object, Boolean)

        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns>A converted value. True when <paramref name="value"/> is null; false otherwise.</returns>
        Public Overrides Function Convert(ByVal value As Object, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Boolean
            Return value Is Nothing
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns><see cref="Binding.DoNothing"/> unless <paramref name="value"/> is true, in such case returns null.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Function ConvertBack(ByVal value As Boolean, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object
            If value = True Then Return Nothing
            Return Binding.DoNothing
        End Function
    End Class
    ''' <summary>One way converter: Converts null to <see cref="Windows.Visibility.Hidden"/> and non-null to <see cref="Windows.Visibility.Visible"/></summary>
    ''' <version stage="Alpha" version="1.5.2">Class introduced</version>
    Public NotInheritable Class NullInvisibleConverter
        Inherits StronglyTypedConverter(Of Object, System.Windows.Visibility)
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns>A converted value. <see cref="Windows.Visibility.Visible"/> when <paramref name="value"/> is not null; <see cref="Windows.Visibility.Hidden"/> otherwise.</returns>
        Public Overrides Function Convert(ByVal value As Object, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As System.Windows.Visibility
            If value Is Nothing Then Return Windows.Visibility.Hidden Else Return Windows.Visibility.Visible
        End Function
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <returns><see cref="Binding.DoNothing"/> unless <paramref name="value"/> is <see cref="Windows.Visibility.Hidden"/>; in such case returns null.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Function ConvertBack(ByVal value As System.Windows.Visibility, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object
            If value = Windows.Visibility.Hidden Then Return Nothing Else Return Binding.DoNothing
        End Function
    End Class

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
            If (Not TypeOf value Is Windows.HorizontalAlignment AndAlso Not TypeOf value Is Windows.TextAlignment) OrElse (Not targetType.Equals(GetType(Windows.HorizontalAlignment)) AndAlso Not targetType.Equals(GetType(Windows.TextAlignment))) Then _
                Throw New NotSupportedException(ResourcesT.Exceptions.ThisConverterCanConvertOnlyBetweenTypes0And1.f(GetType(Windows.HorizontalAlignment).Name, GetType(Windows.VerticalAlignment).Name))
            If TypeOf value Is Windows.HorizontalAlignment Then
                Return Convert(DirectCast(value, HorizontalAlignment))
            Else
                Return Convert(DirectCast(value, TextAlignment))
            End If
        End Function
    End Class

    ''' <summary>Converter that converts <see cref="Windows.Forms.CheckState"/> to <see cref="Nullable(Of T)"/>[<see cref="Boolean"/>]</summary>
    ''' <version version="1.5.2" stage="Alpha">Class introduced</version>
    Public NotInheritable Class CheckStateConverter
        Inherits StronglyTypedConverter(Of Windows.Forms.CheckState, Boolean?)

        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <returns>A converted value.</returns>
        Public Overrides Function Convert(ByVal value As System.Windows.Forms.CheckState, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Boolean?
            Select Case value
                Case Forms.CheckState.Checked : Return True
                Case Forms.CheckState.Unchecked : Return False
                Case Else : Return Nothing
            End Select
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        Public Overrides Function ConvertBack(ByVal value As Boolean?, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As System.Windows.Forms.CheckState
            If value.HasValue AndAlso value Then
                Return Forms.CheckState.Checked
            ElseIf value.HasValue Then
                Return Forms.CheckState.Unchecked
            Else
                Return Forms.CheckState.Indeterminate
            End If
        End Function
    End Class
    ''' <summary>Converter that returns given value of it is of type of converter; otherwise returns null</summary>
    ''' <typeparam name="T">Type of value to return</typeparam>
    ''' <remarks>Due to lack of support for generic types in XAML this class cannot be instantiated directly. Use the <see cref="MarkupT.GenericExtension"/>.
    ''' <example>Following example shows how to use <see cref="MarkupT.GenericExtension"/> to create instance of <see cref="StaticTryCastConverter(Of T)"/>
    ''' <code lang="XAML"><![CDATA[
    ''' <ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    '''                xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    '''                xmlns:conv="clr-namespace:Tools.WindowsT.WPF.ConvertersT;assembly=Tools.Windows"
    '''                xmlns:WF="clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms"
    '''                xmlns:mu="clr-namespace:Tools.WindowsT.WPF.MarkupT;assembly=Tools.Windows"
    ''' >
    '''     <mu:GenericExtension TypeName="conv:StaticTryCastConverter" x:Key="convTryWindowsForms">
    '''         <x:Type TypeName="WF:Control"/>
    '''     </mu:GenericExtension>
    ''' </ResourceDictionary>
    ''' ]]></code></example></remarks>
    ''' <version version="1.5.2" stage="Alpha">Class introduced</version>
    Public NotInheritable Class StaticTryCastConverter(Of T As Class)
        Inherits StronglyTypedConverter(Of Object, T)
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns>If <paramref name="value"/> is <typeparamref name="T"/> returns <paramref name="value"/>; otherwise null</returns>
        Public Overrides Function Convert(ByVal value As Object, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As T
            Return TryCast(value, T)
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns><paramref name="value"/></returns>
        Public Overrides Function ConvertBack(ByVal value As T, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object
            Return value
        End Function
    End Class

End Namespace
#End If

