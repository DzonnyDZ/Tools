Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converts relative path to absolute</summary>
    ''' <remarks>This converter is one way</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class RelativePathConverter
        Implements IValueConverter

        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. Absolute path made from relative path given in <paramref name="value"/>. Relative path root to make it absolute is either <paramref name="parameter"/> is specified or <see cref="Environment.CurrentDirectory"/>. If <paramref name="value"/> <see cref="IO.Path.IsPathRooted">is absolute path</see>, <paramref name="value"/> is vreturned without any change. if <paramref name="value"/> is null, returns null.</returns>
        ''' <param name="value">The value produced by the binding source. It must be valid relative or absolute path (<see cref="String"/>). Additionaly it can be 1-dimensional array of <see cref="String"/> with at least 1 item. Items of such aray are converter using <see cref="IO.Path.Combine"/> left to right.</param>
        ''' <param name="targetType">Ignored. Always returns <see cref="String"/> or null.</param>
        ''' <param name="parameter">String path to prepend to <paramref name="value"/>. If null <see cref="Environment.CurrentDirectory"/> is used instead.</param>
        ''' <param name="culture">ignored</param>
        ''' <exception cref="TypeMismatchException">Either <paramref name="value"/> or <paramref name="parameter"/> is neither <see cref="String"/> nor null nor one dimensional array of <see cref="String"/> with at least one item.</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            If TypeOf parameter Is String() AndAlso DirectCast(parameter, String()).Length > 0 Then
                Dim str = DirectCast(parameter, String())(0)
                For i As Integer = 0 To DirectCast(parameter, String()).Length - 1
                    str = IO.Path.Combine(str, DirectCast(parameter, String())(i))
                Next i
                parameter = str
            End If
            If Not TypeOf value Is String Then Throw New TypeMismatchException("value", value, GetType(String))
            If parameter IsNot Nothing AndAlso Not TypeOf parameter Is String Then Throw New TypeMismatchException("parameter", parameter, GetType(String))
            Dim path = If(DirectCast(parameter, String), Environment.CurrentDirectory)
            If (IO.Path.IsPathRooted(value.ToString)) Then Return value
            Return IO.Path.Combine(path, value)
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <returns>Never returns, always throws <see cref="NotSupportedException"/>.</returns>
        ''' <param name="value">ignored</param>
        ''' <param name="targetType">ignored</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <exception cref="NotSupportedException">Always thrown</exception>
        Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Throw New NotSupportedException(ConverterResources.ex_CannotConvertBack.f(Me.[GetType].Name))
        End Function
    End Class
End Namespace
#End If