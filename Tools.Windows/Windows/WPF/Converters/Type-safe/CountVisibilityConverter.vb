Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization

#If True
Namespace WindowsT.WPF.ConvertersT

    ''' <summary>Converter that converts count of items to <see cref="Visibility"/>. Zero to <see cref="Visibility.Collapsed"/> anything else to <see cref="Visibility.Visible"/>.</summary>
    Public NotInheritable Class CountVisibilityConverter
        Inherits StronglyTypedConverter(Of Integer, Visibility)

        ''' <summary>Converts count to <see cref="Visibility"/></summary>
        ''' <param name="value">Count of items</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <returns><see cref="Visibility.Visible"/> whrn <paramref name="value"/> is greater than zero; <see cref="Visibility.Collapsed"/> otherwise.</returns>
        Public Overrides Function Convert(ByVal value As Integer, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As System.Windows.Visibility
            If value > 0 Then Return Visibility.Visible Else Return Visibility.Collapsed
        End Function

        ''' <summary>This way of conversion should not be used</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <returns>Zero when <paramref name="value"/> i<see cref="Visibility.Collapsed"/>; <see cref="Binding.DoNothing"/> otherwise.</returns>
        Public Overrides Function ConvertBack(ByVal value As System.Windows.Visibility, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Integer
            If value = Visibility.Collapsed Then Return 0 Else Return Binding.DoNothing
        End Function
    End Class
End Namespace
#End If

