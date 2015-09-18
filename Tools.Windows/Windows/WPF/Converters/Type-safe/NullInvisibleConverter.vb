Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>One way converter: Converts null to <see cref="System.Windows.Visibility.Collapsed"/> and non-null to <see cref="System.Windows.Visibility.Visible"/></summary>
    ''' <version stage="Alpha" version="1.5.2">Class introduced</version>
    Public NotInheritable Class NullInvisibleConverter
        Inherits StronglyTypedConverter(Of Object, System.Windows.Visibility)
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns>A converted value. <see cref="System.Windows.Visibility.Visible"/> when <paramref name="value"/> is not null; <see cref="System.Windows.Visibility.Collapsed"/> otherwise.</returns>
        Public Overrides Function Convert(ByVal value As Object, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As System.Windows.Visibility
            If value Is Nothing Then Return System.Windows.Visibility.Collapsed Else Return System.Windows.Visibility.Visible
        End Function
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <returns><see cref="Binding.DoNothing"/> unless <paramref name="value"/> is <see cref="System.Windows.Visibility.Collapsed"/>; in such case returns null.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Function ConvertBack(ByVal value As System.Windows.Visibility, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object
            If value = System.Windows.Visibility.Collapsed Then Return Nothing Else Return Binding.DoNothing
        End Function
    End Class
End Namespace
#End If