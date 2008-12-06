Imports System.Windows.Media.Imaging
Imports System.Runtime.CompilerServices
Imports System.Windows
Imports System.Runtime.InteropServices

#If Config <= Nightly Then 'Stage nightly
Namespace WindowsT.InteropT
    ''' <summary>Contains extension methods for WPF - WinForms interoperability</summary>
    Public Module InteropExtensions
        ''' <summary>Converts <see cref="Drawing.Image"/> to WPF <see cref="BitmapSource"/></summary>
        ''' <param name="Image">An <see cref="Drawing.Image"/></param>
        ''' <returns><see cref="BitmapSource"/> created from <paramref name="Image"/>; null when <paramref name="Image"/> is null.</returns>
        ''' <seelaso cref="Interop.Imaging.CreateBitmapSourceFromHBitmap"/>
        <Extension()> _
       Public Function ToBitmapSource(ByVal Image As Drawing.Bitmap) As BitmapSource
            If Image Is Nothing Then Return Nothing
            'Return Interop.Imaging.CreateBitmapSourceFromHBitmap(Image.GetHbitmap, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions())  'TODO: This may lead to memory leak
            Dim str As New IO.MemoryStream
            Static bmp As Drawing.Imaging.ImageFormat = Drawing.Imaging.ImageFormat.Png
            Image.Save(str, bmp)
            Dim ret As New BitmapImage
            ret.BeginInit()
            ret.StreamSource = str
            ret.EndInit()
            Return ret

        End Function
        ''' <summary>Converts <see cref="Drawing.Icon"/> to WPF <see cref="BitmapSource"/></summary>
        ''' <param name="Icon">An <see cref="Drawing.Icon"/></param>
        ''' <returns><see cref="BitmapSource"/> created from <paramref name="Icon"/>; null when <paramref name="Icon"/> is null.</returns>
        ''' <seelaso cref="Interop.Imaging.CreateBitmapSourceFromHIcon"/>
        <Extension()> _
        Public Function ToBitmapSource(ByVal Icon As Drawing.Icon) As BitmapSource
            If Icon Is Nothing Then Return Nothing
            'TODO: This may lead to memory leak
            Return Interop.Imaging.CreateBitmapSourceFromHIcon(Icon.Handle, Int32Rect.Empty, Media.Imaging.BitmapSizeOptions.FromEmptyOptions())
        End Function
        ''' <summary>Converts <see cref="BitmapSource"/> to <see cref="Drawing.Bitmap"/></summary>
        ''' <param name="Source">A <see cref="BitmapSource"/></param>
        ''' <returns><see cref="Drawing.Bitmap"/> created from <paramref name="Source"/>; null when <paramref name="Source"/> is null.</returns>
        ''' <remarks>Uses <see cref="BmpBitmapEncoder.Save"/>.</remarks>
        <Extension()> _
        Public Function ToBitmap(ByVal Source As BitmapSource) As Drawing.Bitmap
            If Source Is Nothing Then Return Nothing
            Dim enc As New BmpBitmapEncoder()
            enc.Frames.Add(Source)
            Dim ms As New IO.MemoryStream
            enc.Save(ms)
            ms.Flush()
            Return New Drawing.Bitmap(ms)
        End Function
    End Module
End Namespace
#End If