Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization, Tools.LinqT


Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Multi value converter that gets first non-null non-empty itemm from collection</summary>
    ''' <remarks>This converter is primarily intended to seleft first non-null non-empty collection form list of collections.
    ''' <para>This converter is one-way</para></remarks>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public Class FirstNonEmptyConverter
        Implements IMultiValueConverter

        ''' <summary>Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target. This implementation obtains first non-null, non-empty value form <paramref name="values"/>.</summary>
        ''' <returns>
        ''' First non-null non-empty item from <paramref name="values"/>.
        ''' All items from <paramref name="values"/> which are null, <see cref="DependencyProperty.UnsetValue"/> or represent empty array, <see cref="ICollection"/> or <see cref="IEnumerable"/> are ignored.
        ''' First non-ignored value is returned.
        ''' If there is non non-ignored value in the collection, <see cref="DependencyProperty.UnsetValue"/> is returned.
        ''' </returns>
        ''' <param name="values">The array of values that the source bindings in the <see cref="T:System.Windows.Data.MultiBinding" /> produces. The value <see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the source binding has no value to provide for conversion.</param>
        ''' <param name="targetType">Ignored</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        Public Function Convert(ByVal values() As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
            For Each value In values
                If value IsNot Nothing Then
                    If value Is DependencyProperty.UnsetValue Then Continue For
                    If TypeOf value Is ICollection AndAlso DirectCast(value, ICollection).Count = 0 Then Continue For
                    If TypeOf value Is Array AndAlso DirectCast(value, Array).Length = 0 Then Continue For
                    If TypeOf value Is IEnumerable AndAlso DirectCast(value, IEnumerable).IsEmpty Then Continue For
                    Return value
                End If
            Next
            Return System.Windows.DependencyProperty.UnsetValue
        End Function

        ''' <summary>Returns given value as only item in object array</summary>
        ''' <returns>Array containing one item - <paramref name="value"/>. If <paramref name="value"/> is null an ampty array is returned. The arrry is of type <see cref="System.Object"/>[].</returns>
        ''' <param name="value">Value to be encapsulated in array</param>
        ''' <param name="targetTypes">Ignored</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        Private Function IMultiValueConverter_ConvertBack(ByVal value As Object, ByVal targetTypes() As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
            If value Is Nothing Then Return New Object() {}
            Return {value}
        End Function
    End Class
End Namespace
