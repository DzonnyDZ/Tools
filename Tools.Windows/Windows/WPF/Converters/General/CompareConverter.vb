Imports System.Windows.Data, Tools.ExtensionsT

Namespace WindowsT.WPF.ConvertersT



    ''' <summary>Converter that test if value being converter equals to parameter</summary>
    ''' <remarks>This converter is intended as is one-way. This converter used <see cref="M:System.Object.Equals(System.Object)"/>.</remarks>
    ''' <seelaso cref="System.Object.Equals"/>
    ''' <version version="1.5.3" stage="Nightly">This converter is new in version 1.5.3</version>
    Public Class CompareConverter
        Implements IValueConverter

        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. If <paramref name="value"/> is null or <paramref name="parameter"/> is null, returns null. Otherwise returns boolean value indicating if <paramref name="value"/> equals to <paramref name="parameter"/> using <see cref="System.Object.Equals"/>.</returns>
        ''' <param name="value">The value produced by the binding source. Thsi value will be compared for equality with <paramref name="parameter"/>.</param>
        ''' <param name="targetType">Ignored. Always returns null or <see cref="Boolean"/></param>
        ''' <param name="parameter">Value to compare <paramref name="value"/> with.</param>
        ''' <param name="culture">Ignored.</param>
        ''' <remarks>No type conversion is performed on <paramref name="value"/> and <paramref name="parameter"/> arguments.
        ''' Simply <c><paramref name="value"/>.<see cref="System.Object.Equals">Equals</see>(<paramref name="parameter"/>)</c> is called.</remarks>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return parameter Is Nothing
            If parameter Is Nothing Then Return value Is Nothing
            Return value.Equals(parameter)
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <returns>If <paramref name="value"/> is true returns <paramref name="parameter"/>; otherwise throws an exception</returns>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="targetType">ignored</param>
        ''' <param name="parameter">Value this converter compares values to</param>
        ''' <param name="culture">ignored</param>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is not true</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If TypeOf value Is Boolean AndAlso DirectCast(value, Boolean) Then Return parameter
            Throw New NotSupportedException(Resources.ex_CannotConvertBack.f(Me.GetType.Name))
        End Function
    End Class
End Namespace