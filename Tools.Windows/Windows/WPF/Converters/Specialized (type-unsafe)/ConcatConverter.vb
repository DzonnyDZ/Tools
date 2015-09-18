Imports Tools.TypeTools, Tools.ReflectionT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter that converts <see cref="IEnumerable"/> to comma-seperated list (or another seperator can be chosen)</summary>
    ''' <remarks>This converter is designed as one-way, howver <see cref="IValueConverter.ConvertBack"/> is implemented.</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class ConcatConverter
        Implements IValueConverter
        ''' <summary>Gets or sets name of property of item in collection to caoncat value got from. If null (default) entire object is used.</summary>
        Public Property PropertyName As String
        ''' <summary>Converts value</summary>
        ''' <param name="value">Value to be converted. Shall be null or <see cref="IEnumerable"/></param>
        ''' <param name="targetType">Ignored. This method always returns null or <see cref="String"/></param>
        ''' <param name="parameter">Any objects which string representation will be used as item seperator. If null ', ' is used.</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns>Stirng representations of objects obrained form <see cref="IEnumerable"/> <paramref name="value"/> concatenated to string using <paramref name="parameter"/> (or ', ' if parameter is null).</returns>
        ''' <exception cref="TypeMismatchException"><paramref name="value"/> is neither null nor <see cref="IEnumerable"/>.</exception>
        ''' <exception cref="MissingMemberException">Property name is specified in <paramref name="parameter"/>, but there is no such property or field on item in <paramref name="value"/> collection.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Property name is specified in <paramref name="parameter"/> and the property is overloaded</exception>
        ''' <exception cref="Reflection.TargetParameterCountException">Property name is specified in <paramref name="parameter"/> and property is indexed</exception>
        ''' <exception cref="MethodAccessException">Property name is specified in <paramref name="parameter"/> and property getter is not public</exception>
        ''' <exception cref="Reflection.TargetInvocationException">Property name is specified in <paramref name="parameter"/> and an error occurred while retrieving the property value. The <see cref="System.Exception.InnerException"/> property indicates the reason for the error.</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return ""
            If Not TypeOf value Is IEnumerable Then Throw New TypeMismatchException("value", value, GetType(IEnumerable))
            Dim sb As New System.Text.StringBuilder
            Dim separator$
            separator = If(parameter, ", ").ToString
            For Each item As Object In DirectCast(value, IEnumerable)
                If sb.Length <> 0 Then sb.Append(separator)
                If item Is Nothing Then
                    'DoNothing
                ElseIf PropertyName = "" Then
                    sb.Append(item.ToString)
                Else
                    Dim prp = item.GetType.GetProperty(PropertyName, Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
                    Dim field = If(prp Is Nothing, item.GetType.GetField(PropertyName, Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance), Nothing)
                    If prp Is Nothing AndAlso field Is Nothing Then Throw New MissingMemberException(item.GetType.FullName, PropertyName)
                    Dim pvalue = prp.GetValue(item, Nothing)
                    If pvalue IsNot Nothing Then sb.Append(pvalue.ToString) 'Else DoNothing
                End If
            Next
            Return sb.ToString
        End Function
        ''' <summary>Converts value back from concateenated list to string array</summary>
        ''' <param name="value">Value to be converted. It shall be <see cref="String"/> or null.</param>
        ''' <param name="targetType">Ignored. This method always returns <see cref="String()"/> array</param>
        ''' <param name="parameter">Any objects which string representation will be used as item seperator. If null ', ' is used.</param>
        ''' <param name="culture">Ignored.</param>
        ''' <returns><paramref name="value"/> <see cref="String.Split">splitted</see> using <paramref name="parameter"/> (', ' if <paramref name="parameter"/> is null)</returns>
        ''' <exception cref="TypeMismatchException"><paramref name="value"/> is neither null nor <see cref="String"/></exception>
        Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If value Is Nothing OrElse (TypeOf value Is String AndAlso DirectCast(value, String) = "") Then Return New String() {}
            If Not TypeOf value Is String Then Throw New TypeMismatchException("value", value, GetType(String))
            Return DirectCast(value, String).Split(If(parameter, ", ").ToString)
        End Function
    End Class
End Namespace
#End If