Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter that gets file name part of path</summary>
    ''' <remarks>This converter is one-way.</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class FileNameConverter
        Implements IValueConverter

        ''' <summary>Converts a value.</summary>
        ''' <returns>File name part of path given in <paramref name="parameter"/>. Uses <see cref="IO.Path.GetFileName"/>. When <paramref name="value"/> is null returns null.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">Ignored. Returns null. or <see cref="String"/></param>
        ''' <param name="parameter">ignored.</param>
        ''' <param name="culture">ignored.</param>
        ''' <exception cref="TypeMismatchException"><paramref name="value"/> is neither <see cref="String"/> nor null</exception>
        ''' <remarks>Uses <see cref="IO.Path.Combine"/></remarks>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            If Not TypeOf value Is String Then Throw New TypeMismatchException("value", value, GetType(String))
            Return IO.Path.GetFileName(value.ToString)
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <returns>Never returns, always throws <see cref="NotSupportedException"/>.</returns>
        ''' <param name="value">ignored</param>
        ''' <param name="targetType">ignored</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <exception cref="NotSupportedException">Always thrown</exception>
        Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Throw New NotSupportedException(ConverterResources.ex_CannotConvertBack.f(Me.GetType.Name))
        End Function


    End Class
End Namespace
#End If