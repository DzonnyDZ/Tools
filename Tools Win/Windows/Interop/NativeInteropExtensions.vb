Imports System.Runtime.CompilerServices
Imports System.Windows.Media.Imaging
Imports System.Windows

Namespace WindowsT.InteropT
#If Config <= Nightly Then 'Stage: Nightly
    ''' <summary>Provides extension functions for interoperability between Windows Forms and WPF. Functions provided by this module are dependend on Windows API calls and thus are not portable.</summary>
    ''' <seealso cref="T:Tools.WindowsT.InteropT.InteropExtensions"/>
    ''' <version version="1.5.3">This module is new in version 1.5.3</version>
    Public Module NativeInteropExtensions
        ''' <summary>Converts <see cref="System.Drawing.Bitmap"/> to <see cref="BitmapSource"/></summary>
        ''' <param name="bitmap">A <see cref="System.Drawing.Bitmap"/></param>
        ''' <returns><see cref="BitmapSource"/> providing access to <paramref name="bitmap"/>. Null when <paramref name="bitmap"/> is null.</returns>
        ''' <remarks>Based on <a href="http://stackoverflow.com/questions/94456/load-a-wpf-bitmapimage-from-a-system-drawing-bitmap">http://stackoverflow.com/questions/94456/load-a-wpf-bitmapimage-from-a-system-drawing-bitmap</a>.
        ''' <para>Version not dependent on Windows API is provided by <see cref="InteropExtensions.ToBitmapSource"/>, but this implementation is supposed to be more effficient.</para>
        ''' </remarks>
        ''' <seelaso cref="Tools.WindowsT.InteropT.InteropExtensions.ToBitmapSource"/>
        <Extension()>
        Public Function ToBitmapSourceFast(ByVal bitmap As System.Drawing.Bitmap) As BitmapSource
            If bitmap Is Nothing Then Return Nothing
            Dim hBitmap = bitmap.GetHbitmap
            Try
                Return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
            Finally
                API.Common.DeleteObject(hBitmap)
            End Try
        End Function

        ''' <summary>Converts <see cref="System.Drawing.Image"/> to <see cref="BitmapSource"/></summary>
        ''' <param name="image">A <see cref="System.Drawing.Image"/></param>
        ''' <returns><see cref="BitmapSource"/> providing access to <paramref name="bitmap"/>. Null when <paramref name="bitmap"/> is null.</returns>
        ''' <remarks>Version not dependent on Windows API is provided by <see cref="InteropExtensions.ToBitmapSource"/>, but this implementation is supposed to be more effficient.
        ''' <see cref="Tools.WindowsT.InteropT.InteropExtensions.ToBitmapSource"/> is called when actual type of <paramref name="image"/> is not <see cref="System.Drawing.Bitmap"/>, when it is overload for <see cref="Drawing.Bitmap"/> is called.
        ''' </remarks>
        ''' <seelaso cref="Tools.WindowsT.InteropT.InteropExtensions.ToBitmapSource"/>
        <Extension()>
        Public Function ToBitmapSourceFast(ByVal image As System.Drawing.Image) As BitmapSource
            If image Is Nothing Then Return Nothing
            If TypeOf image Is System.Drawing.Bitmap Then DirectCast(image, System.Drawing.Bitmap).ToBitmapSource()
            Return Tools.WindowsT.InteropT.InteropExtensions.ToBitmapSource(image)
        End Function
    End Module
#End If
End Namespace
