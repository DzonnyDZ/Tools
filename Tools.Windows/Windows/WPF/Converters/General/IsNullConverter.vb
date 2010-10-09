Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization

#If Config <= Alpha Then 'Stage: Aplha
Namespace WindowsT.WPF.ConvertersT
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
End Namespace
#End If