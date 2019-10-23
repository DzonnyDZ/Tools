Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization


Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter that converts <see cref="System.Windows.Forms.CheckState"/> to <see cref="Nullable(Of T)"/>[<see cref="Boolean"/>]</summary>
    ''' <version version="1.5.2" stage="Alpha">Class introduced</version>
    Public NotInheritable Class CheckStateConverter
        Inherits StronglyTypedConverter(Of System.Windows.Forms.CheckState, Boolean?)

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
End Namespace
