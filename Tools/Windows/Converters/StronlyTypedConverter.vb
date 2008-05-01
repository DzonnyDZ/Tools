Imports System.Windows.Data
Imports System.Globalization

Namespace WindowsT.ConvertersT
    Public MustInherit Class StronlyTypedConverter(Of TSource, TTarget)
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If TypeOf value is TSource andalso targetType.
        End Function
        Public MustOverride Function Convert(ByVal value As TSource, ByVal parameter As Object, ByVal culture As CultureInfo) As TTarget

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack

        End Function
        Public MustOverride Function ConvertBack(ByVal value As TTarget, ByVal parameter As Object, ByVal culture As CultureInfo) As TSource
    End Class
        End Namespace 