Imports System.Windows.Media.Imaging
Imports System.Runtime.CompilerServices
Imports System.Windows
Imports System.Runtime.InteropServices
Imports System.Windows.Interop

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

        ''' <summary>Shows <see cref="Forms.Form"/> floating over <see cref="Window"/></summary>
        ''' <param name="Form"><see cref="Forms.Form"/> to be shown</param>
        ''' <param name="Owner">Owner <see cref="Window"/>; can be null</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Form"/> is null</exception>
        ''' <version version="1.5.2">Method instroduced</version>
        <Extension()> Public Sub Show(ByVal Form As Forms.Form, ByVal Owner As Window)
            If Form Is Nothing Then Throw New ArgumentException("Form")
            If Owner Is Nothing Then
                Form.Show()
            Else
                Form.Show(Forms.NativeWindow.FromHandle(New WindowInteropHelper(Owner).Handle))
            End If
        End Sub
        ''' <summary>Shows <see cref="Forms.Form"/> modally for given <see cref="Window"/></summary>
        ''' <param name="Form"><see cref="Forms.Form"/> to be shown</param>
        ''' <param name="Owner">Owner <see cref="Window"/>. Can be null.</param>
        ''' <returns>One of <see cref="forms.DialogResult"/> values</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Form"/> is null</exception>
        ''' <version version="1.5.2">Method instroduced</version>
        <Extension()> Public Function ShowDialog(ByVal Form As Forms.Form, ByVal Owner As Window) As Forms.DialogResult
            If Form Is Nothing Then Throw New ArgumentException("Form")
            If Owner Is Nothing Then
                Return Form.ShowDialog()
            Else
                Return Form.ShowDialog(Forms.NativeWindow.FromHandle(New WindowInteropHelper(Owner).Handle))
            End If
        End Function
        ''' <summary>Shows <see cref="Window"/> floating over given native window such as <see cref="Forms.Form"/></summary>
        ''' <param name="Window"><see cref="Window"/> to be shown</param>
        ''' <param name="Owner">Owner form or other native window. Can be null.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.2">Method instroduced</version>
        <Extension()> Public Sub Show(ByVal Window As Window, ByVal Owner As IWin32Window)
            If Window Is Nothing Then Throw New ArgumentException("Window")
            Forms.Integration.ElementHost.EnableModelessKeyboardInterop(Window)
            If Owner Is Nothing Then
                Window.Show()
            Else
                Dim ioh As New WindowInteropHelper(Window)
                ioh.Owner = Owner.Handle
                Window.Show()
            End If
        End Sub
        ''' <summary>Shows <see cref="Window"/> modally to given native window such as <see cref="Forms.Form"/></summary>
        ''' <param name="Window"><see cref="Window"/> to be shown</param>
        ''' <param name="Owner">Owner form or other native window. Can be null.</param>
        ''' <returns>A <see cref="System.Nullable(Of T)"/> value of type <see cref="System.Boolean"/> that signifies how <paramref name="Window"/> was closed by the user.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.2">Method instroduced</version>
        <Extension()> Public Function ShowDialog(ByVal Window As Window, ByVal Owner As IWin32Window) As Boolean?
            If Window Is Nothing Then Throw New ArgumentException("Window")
            Forms.Integration.ElementHost.EnableModelessKeyboardInterop(Window)
            If Owner Is Nothing Then
                Return Window.ShowDialog()
            Else
                Dim ioh As New WindowInteropHelper(Window)
                ioh.Owner = Owner.Handle
                Return Window.ShowDialog()
            End If
        End Function
        ''' <summary>Gets handle of <see cref="Window"/></summary>
        ''' <param name="Window"><see cref="Window"/> to get handle of</param>
        ''' <returns>Handle of <paramref name="Window"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.2">Method instroduced</version>
        <Extension()> Public Function GetHandle(ByVal Window As Window) As IntPtr
            If Window Is Nothing Then Throw New ArgumentException("Window")
            Return New WindowInteropHelper(Window).Handle
        End Function
        ''' <summary>Gets owner of <see cref="Window"/> as native <see cref="IWin32Window"/></summary>
        ''' <param name="Window">Window to get owner of</param>
        ''' <returns><see cref="Forms.Control"/> such as <see cref="Forms.Form"/> that is owner of <paramref name="Window"/> or <see cref="Forms.NativeWindow"/> when owner of <paramref name="Window"/> is not <see cref="Forms.Control"/>; <see langword="null"/> when <paramref name="Window"/> has no owner.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.2">Method instroduced</version>
        <Extension()> Public Function GetOwner(ByVal Window As Window) As IWin32Window
            If Window Is Nothing Then Throw New ArgumentException("Window")
            Dim ohwnd As IntPtr = New WindowInteropHelper(Window).Owner
            If ohwnd = IntPtr.Zero Then Return Nothing
            Return If(TryCast(Forms.Control.FromHandle(ohwnd), IWin32Window), DirectCast(Forms.NativeWindow.FromHandle(ohwnd), IWin32Window))
        End Function
        ''' <summary>Sest owner of <see cref="Window"/> to native window such as <see cref="Forms.Form"/></summary>
        ''' <param name="Window"><see cref="Window"/> to set owner of</param>
        ''' <param name="Owner">New owner of <paramref name="Window"/>; null to remove parent of <paramref name="Window"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.2">Method instroduced</version>
        <Extension()> Public Sub SetOwner(ByVal Window As Window, ByVal Owner As IWin32Window)
            If Window Is Nothing Then Throw New ArgumentException("Window")
            Dim ioh As New WindowInteropHelper(Window)
            ioh.Owner = If(Owner IsNot Nothing, Owner.Handle, IntPtr.Zero)
        End Sub
    End Module
End Namespace
#End If