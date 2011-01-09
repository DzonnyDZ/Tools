Imports System.Windows.Data, Tools.ExtensionsT, Tools.TextT, Tools.LinqT
Imports System.Windows, System.Globalization.CultureInfo

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ConvertersT

    ''' <summary>Converter which combines multiple collection to one <see cref="CompositeCollection"/>. It's possible to decide which collections to be included and which not.</summary>
    ''' <remarks>
    ''' Converter parameter is list of whitespace-separated expressions. Each expression indicates how to process corresponding (by index) collection. Last expression applies to all remaining collections.
    ''' Syntax of expression is following:
    ''' <list type="table"><listheader><term>Indicator</term><description>Meaning</description></listheader>
    ''' <item><term><c>.</c></term><description>Include. Include the collection in result.</description></item>
    ''' <item><term><c>![x]</c></term><description>If not. Include only if none of preceding collections was included.</description></item>
    ''' <item><term><c>|[x]</c></term><description>If or. Include only if at least one preceding collections was included.</description></item>
    ''' <item><term>&amp;[x]</term><description>If and. Include only if all preceding collections was included.</description></item>
    ''' <item><term>^[x]</term><description>If xor. Include only if exactly one preceding collection was included.</description></item>
    ''' <item><term><c>-</c></term><description>Not include. The collection is never included (unconditionally, the <c>#</c> is ignored.)</description></item>
    ''' <item><term>Special prefix <c>#</c></term><description>Prepend to any expression to inlude the collection even if it is empty. When this prefix is used <c>.</c> is optional.</description></item>
    ''' </list>
    ''' <para><c>[x]</c> indicates optional number (integer > 0, written without the []) meaning number of preceding collections to be considered. All preceding collections are considered when ommited.</para>
    ''' <para>
    ''' Null, <see cref="DBNull"/> and  <see cref="F:DependencyProperty.UnsetValue" /> are never included.
    ''' Empty collection is not included unless the <c>#</c> prefix is specified.
    ''' </para>
    ''' <para>If item is not collection it's wrappped to array of corresponding type.</para>
    ''' <para>If only one collection is filtered out, it's returned without being wrapped in <see cref="CompositeCollection"/>.</para>
    ''' <para>This converter is one way. <see cref="IMultiValueConverter.ConvertBack"/> returns only the value passed to it wrapped in array.</para>
    ''' </remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class CollectionCombiningConverter
        Implements IMultiValueConverter
        ''' <summary>Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.</summary>
        ''' <param name="values">
        ''' The array of values that the source bindings in the <see cref="T:System.Windows.Data.MultiBinding" /> produces. The value <see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the source binding has no value to provide for conversion.
        ''' <para>Values are expected to be <see cref="IEnumerable"/>. Nulls, <see cref="DBNull">DBNulls</see> and <see cref="DependencyProperty.UnsetValue" /> values are ignored. Non-<see cref="IEnumerable"/> values are wrapped to one-item arrays of corresponding type.</para>
        ''' </param>
        ''' <param name="parameter">The converter parameter to use. When null or empty string string "." is assumed. For details (synatx) see class documentation.</param>
        ''' <exception cref="FormatException"><paramref name="parameter"/> seems to contain number of preceding collections to be considered, but the number cannot be parsed using <see cref="Int32.Parse"/> (in <see cref="InvariantCulture"/>).</exception>
        ''' <seelaso cref="SyntaxErrorException">Invalid character in expression.</seelaso>
        Public Function Convert(ByVal values() As Object, Optional ByVal parameter As String = ".") As IEnumerable
            Return Convert(values, Nothing, parameter, Nothing)
        End Function
        ''' <summary>Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.</summary>
        ''' <param name="values">
        ''' The array of values that the source bindings in the <see cref="T:System.Windows.Data.MultiBinding" /> produces. The value <see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the source binding has no value to provide for conversion.
        ''' <para>Values are expected to be <see cref="IEnumerable"/>. Nulls, <see cref="DBNull">DBNulls</see> and <see cref="DependencyProperty.UnsetValue" /> values are ignored. Non-<see cref="IEnumerable"/> values are wrapped to one-item arrays of corresponding type.</para>
        ''' </param>
        ''' <param name="targetType">The type of the binding target property. Ignored if null. Target type should <see cref="Type.IsAssignableFrom">assignable from</see> <see cref="IEnumerable"/> otherwise <see cref="DynamicCast"/> is used.</param>
        ''' <param name="parameter">The converter parameter to use. When null or empty string string "." is assumed. Type is expected to be <see cref="String"/> otherwise <see cref="System.Object.ToString"/> is used.
        ''' For details (syntax) see class documentation.</param>
        ''' <param name="culture">Ignored.</param>
        ''' <exception cref="InvalidCastException">Conversion must be performed for <paramref name="targetType"/> and it fails.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Conversion must be performed for target type, conversion operators are found but no one is most specific.</exception>
        ''' <exception cref="FormatException"><paramref name="parameter"/> seems to contain number of preceding collections to be considered, but the number cannot be parsed using <see cref="Int32.Parse"/> (in <see cref="InvariantCulture"/>).</exception>
        ''' <seelaso cref="SyntaxErrorException">Invalid character in expression.</seelaso>
        Public Function Convert(ByVal values() As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
            If values Is Nothing Then Return DependencyProperty.UnsetValue
            Dim included(values.Length - 1) As Boolean
            Dim ret As New CompositeCollection
            Dim ps = If(parameter, ".").ToString
            If String.IsNullOrWhiteSpace(ps) Then ps = "."
            Dim expressions = ps.WhiteSpaceSplit
            Dim firstCollection As IEnumerable = Nothing
            Dim includedCountTotal = 0
            Dim i% = 0
            For Each value In values
                Try
                    If value Is Nothing OrElse TypeOf value Is DBNull OrElse value Is DependencyProperty.UnsetValue Then Continue For
                    Dim expression = If(i >= expressions.Length, expressions.Last, expressions(i))
                    If expression = "-" OrElse expression = "#-" Then Continue For
                    Dim collection As IEnumerable
                    If Not TypeOf value Is IEnumerable Then
                        Dim arr = Array.CreateInstance(value.GetType, 1)
                        arr.SetValue(value, 0)
                        collection = arr
                    Else
                        collection = value
                    End If
                    If collection.IsEmpty AndAlso Not expression.StartsWith("#") Then Continue For
                    If expression = "#" Then : expression = "."
                    ElseIf expression.StartsWith("#") Then : expression = expression.Substring(".") : End If
                    If expression = "." Then
                        included(i) = True
                    Else
                        Dim number = If(expression.Length > 1, Integer.Parse(expression.Substring(1), InvariantCulture), i)
                        Dim numberOfCollections = 0
                        For j As Integer = i - 1 To Math.Max(0, i - number) Step -1
                            If included(j) Then numberOfCollections += 1
                        Next
                        Select Case expression(0)
                            Case "!"c : included(i) = numberOfCollections = 0
                            Case "|"c : included(i) = numberOfCollections > 0
                            Case "&"c : included(i) = numberOfCollections = i
                            Case "^"c : included(i) = numberOfCollections = 1
                            Case Else : Throw New SyntaxErrorException("Expression {0} is invalid - character {1} not recognized.".f(expression, expression(1)))
                        End Select
                    End If
                    If included(i) Then
                        ret.Add(New CollectionContainer With {.Collection = collection})
                        If firstCollection Is Nothing Then firstCollection = collection
                        includedCountTotal += 1
                    End If
                Finally
                    i += 1
                End Try
            Next
            Dim toReturn = If(includedCountTotal = 1, firstCollection, ret)
            If targetType Is Nothing OrElse targetType.IsAssignableFrom(toReturn.GetType) Then Return toReturn
            Return DynamicCast(toReturn, targetType)
        End Function

        ''' <summary>Converts a binding target value to the source binding values.</summary>
        ''' <returns><paramref name="value"/> wrapped in <see cref="Object"/> array.</returns>
        ''' <param name="value">The value that the binding target produces.</param>
        ''' <param name="targetTypes">Ignored.</param>
        ''' <param name="parameter">Ignored.</param>
        ''' <param name="culture">Ignored.</param>
        Private Function ConvertBack(ByVal value As Object, ByVal targetTypes() As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
            Return {value}
        End Function
    End Class
End Namespace
#End If