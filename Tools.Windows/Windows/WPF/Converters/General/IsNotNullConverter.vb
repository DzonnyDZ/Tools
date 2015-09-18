Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization

#If True
Namespace WindowsT.WPF.ConvertersT
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
End Namespace
#End If