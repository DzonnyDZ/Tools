Imports Tools.WindowsT.WPF.ConvertersT
Imports System.Windows.Data


Namespace WindowsT.InteropT
    ''' <summary>Converter that converts any value of type <see cref="System.Windows.Forms.Control"/> to <see cref="System.Windows.Visibility.Visible"/> and any other to <see cref="System.Windows.Visibility.Hidden"/></summary>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public NotInheritable Class IsWindowsFormsControlVisibilityConverter
        Inherits StronglyTypedConverter(Of Object, System.Windows.Visibility)
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        Public Overrides Function Convert(ByVal value As Object, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As System.Windows.Visibility
            Return If(TypeOf value Is System.Windows.Forms.Control, System.Windows.Visibility.Visible, System.Windows.Visibility.Hidden)
        End Function

        ''' <summary>Does norhing</summary>
        ''' <param name="value">ignored</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <returns><see cref="Binding.DoNothing"/></returns>
        Public Overrides Function ConvertBack(ByVal value As System.Windows.Visibility, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object
            Return Binding.DoNothing
        End Function
    End Class
End Namespace
