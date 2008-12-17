Imports Tools.WindowsT.WPF.ConvertersT
Imports System.Windows.Data

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.InteropT
    ''' <summary>Converter that converts any value of type <see cref="Windows.Forms.Control"/> to <see cref="Windows.Visibility.Visible"/> and any other to <see cref="Windows.Visibility.Hidden"/></summary>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public NotInheritable Class IsWindowsFormsControlVisibilityConverter
        Inherits StronglyTypedConverter(Of Object, Windows.Visibility)
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        Public Overrides Function Convert(ByVal value As Object, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As System.Windows.Visibility
            Return If(TypeOf value Is Windows.Forms.Control, Windows.Visibility.Visible, Windows.Visibility.Hidden)
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
#End If