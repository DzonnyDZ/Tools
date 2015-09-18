Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net,Tools.WindowsT.InteropT
Imports System.Windows.Data
Imports System.Windows.Media.Imaging

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Implements <see cref="IValueConverter"/> that converts URI (path) to cached <see cref="BitmapImage"/></summary>
    ''' <remarks>Use this converter to avoid images to be locked by <see cref="System.Windows.Controls.Image"/> control.</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class CachedImageConverter
        Implements IValueConverter

        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value - <see cref="BitmapImage"/></returns>
        ''' <param name="value">The value produced by the binding source. It must be either <see cref="String"/>, <see cref="Uri"/> or <see cref="Tools.IOt.Path"/>.</param>
        ''' <param name="targetType">The type of the binding target property. It must <see cref="Type.IsAssignableFrom">be assignable from</see> <see cref="BitmapImage"/>.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            If Not targetType.IsAssignableFrom(GetType(BitmapImage)) Then Throw New NotSupportedException(ConverterResources.ex_CanConvertOnlyTo.f(Me.GetType.Name, GetType(BitmapImage).Name))
            If TypeOf value Is Uri OrElse TypeOf value Is Tools.IOt.Path Then value = value.ToString
            If Not TypeOf value Is String Then Throw New TypeMismatchException("value", value, GetType(String))
            If DirectCast(value, String) = "" Then Return Nothing
            Try
                Dim img As New BitmapImage
                img.BeginInit()
                img.CacheOption = BitmapCacheOption.OnLoad
                img.UriSource = New Uri(DirectCast(value, String))
                img.EndInit()
                Return img
            Catch : End Try
            Return Nothing
        End Function

        ''' <summary>Converts a value back.</summary>
        ''' <returns>A converted value - URI of <see cref="BitmapSource"/> passed in value <paramref name="value"/>. Type depends on <paramref name="targetType"/>.</returns>
        ''' <param name="value">The value that is produced by the binding target. It must be <see cref="BitmapSource"/>.</param>
        ''' <param name="targetType">The type to convert to. It must <see cref="Type.IsAssignableFrom">be assignable from</see> either <see cref="String"/>, <see cref="Uri"/> or <see cref="Tools.IOt.Path"/>. </param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If value Is Nothing Then Return Nothing
            If Not TypeOf value Is BitmapImage Then Throw New TypeMismatchException("value", value, GetType(BitmapImage))
            If DirectCast(value, BitmapImage).UriSource Is Nothing Then Return Nothing
            If targetType.IsAssignableFrom(GetType(String)) Then
                Return DirectCast(value, BitmapImage).UriSource.ToString
            ElseIf targetType.IsAssignableFrom(GetType(Uri)) Then
                Return DirectCast(value, BitmapImage).UriSource
            ElseIf targetType.IsAssignableFrom(GetType(Tools.IOt.Path)) Then
                Return New Tools.IOt.Path(DirectCast(value, BitmapImage).UriSource.ToString)
            Else
                Throw New NotSupportedException(ConverterResources.ex_CanConvertBackOnlyTo.f(Me.GetType.Name, GetType(String).Name))
            End If
        End Function
    End Class
End Namespace
#End If