Imports System.Runtime.CompilerServices
Imports System.Windows
Imports System.Windows.Interop
Imports System.Windows.Media.Imaging
Imports SystemInterop = System.Windows.Interop


Namespace WindowsT.InteropT
    ''' <summary>Contains extension methods for WPF - WinForms interoperability</summary>
    ''' <seealso cref="T:Tools.Tools.WindowsT.InteropT.NativeInteropExtensions"/>
    ''' <version version="1.5.3." stage="Alpha">Introduced new overloads to <see cref="InteropExtensions.Show"/> and <see cref="InteropExtensions.ShowDialog"/> methods to support <see cref="Interop.IWin32Window"/>.</version>
    Public Module InteropExtensions
#Region "Images"
        ''' <summary>Converts <see cref="Drawing.Image"/> to WPF <see cref="BitmapSource"/></summary>
        ''' <param name="Image">An <see cref="Drawing.Image"/></param>
        ''' <returns><see cref="BitmapSource"/> created from <paramref name="Image"/>; null when <paramref name="Image"/> is null.</returns>
        ''' <remarks><see cref="M:Tools.Tools.WindowsT.InteropT.NativeInteropExtensions.ToBitmapSource(System.Drawing.Bitmap)"/> may be more efficient way of converting bitmaps.</remarks>
        ''' <seelaso cref="Interop.Imaging.CreateBitmapSourceFromHBitmap"/>
        ''' <version version="1.5.3">In version 1.5.3 overload for <see cref="Drawing.Image"/> overload was added and since then this overload calls that overload.</version>
        ''' <seelaso cref="M:Tools.Tools.WindowsT.InteropT.NativeInteropExtensions.ToBitmapSourceFast(System.Drawing.Bitmap)"/>
        <Extension()>
        Public Function ToBitmapSource(ByVal Image As Drawing.Bitmap) As BitmapSource
            Return DirectCast(Image, Drawing.Image).ToBitmapSource
        End Function

        ''' <summary>Converts <see cref="Drawing.Image"/> to WPF <see cref="BitmapSource"/></summary>
        ''' <param name="Image">An <see cref="Drawing.Image"/></param>
        ''' <returns><see cref="BitmapSource"/> created from <paramref name="Image"/>; null when <paramref name="Image"/> is null.</returns>
        ''' <remarks>For <see cref="Drawing.Bitmap"/>, you may consider using <see cref="M:Tools.Tools.WindowsT.InteropT.NativeInteropExtensions.ToBitmapSourceFast(System.Drawing.Bitmap)"/> which is faster but depends on Windows API.</remarks>
        ''' <version version="1.5.3">This overload is new in version 1.5.3</version>
        ''' <seelaso cref="M:Tools.Tools.WindowsT.InteropT.NativeInteropExtensions.ToBitmapSourceFast(System.Drawing.Image)"/>
        <Extension()>
        Public Function ToBitmapSource(ByVal image As Drawing.Image) As BitmapSource
            If image Is Nothing Then Return Nothing
            Dim str As New IO.MemoryStream
            Static bmp As Drawing.Imaging.ImageFormat = Drawing.Imaging.ImageFormat.Png
            image.Save(str, bmp)
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
        <Extension()>
        Public Function ToBitmapSource(ByVal Icon As Drawing.Icon) As BitmapSource
            If Icon Is Nothing Then Return Nothing
            'TODO: This may lead to memory leak
            Return Interop.Imaging.CreateBitmapSourceFromHIcon(Icon.Handle, Int32Rect.Empty, Media.Imaging.BitmapSizeOptions.FromEmptyOptions())
        End Function
        ''' <summary>Converts <see cref="BitmapSource"/> to <see cref="Drawing.Bitmap"/></summary>
        ''' <param name="Source">A <see cref="BitmapSource"/></param>
        ''' <returns><see cref="Drawing.Bitmap"/> created from <paramref name="Source"/>; null when <paramref name="Source"/> is null.</returns>
        ''' <remarks>Uses <see cref="BmpBitmapEncoder.Save"/>.</remarks>
        <Extension()>
        Public Function ToBitmap(ByVal Source As BitmapSource) As Drawing.Bitmap
            If Source Is Nothing Then Return Nothing
            Dim enc As New BmpBitmapEncoder()
            enc.Frames.Add(Source)
            Dim ms As New IO.MemoryStream
            enc.Save(ms)
            ms.Flush()
            Return New Drawing.Bitmap(ms)
        End Function

#Region "Color"
        ''' <summary>Gets <see cref="Media.Color"/> from ARGB value</summary>
        ''' <param name="argb">COlor ARGB value</param>
        ''' <returns>A color instance initialized from <paramref name="argb"/>.</returns>
        ''' <version version="1.5.4">This method is new in version 1.5.4</version>
        Public Function ColorFromArgb(argb As Integer) As Media.Color
            Return Drawing.Color.FromArgb(argb).ToColor
        End Function
        ''' <summary>Gets the 32-bit ARGB value of given <see cref="Media.Color"/> structure.</summary>
        ''' <param name="Color">Color to get ARGB value of</param>
        ''' <returns>The 32-bit ARGB value of <paramref name="Color"/>.</returns>
        ''' <version version="1.5.3.">This function is new in version 1.5.3</version>
        <Extension()>
        Public Function ToArgb(ByVal Color As Media.Color) As Integer
            Return System.Drawing.Color.FromArgb(Color.A, Color.R, Color.G, Color.B).ToArgb
        End Function
        ''' <summary>Gets the 32-bit ARGB value of given <see cref="Media.Color"/> structure.</summary>
        ''' <param name="Color">COlor to get ARGB value of or null</param>
        ''' <returns>The 32-bit ARGB value of <paramref name="Color"/>, or null when <paramref name="Color"/> is null.</returns>
        ''' <version version="1.5.3.">This function is new in version 1.5.3</version>
        <Extension()>
        Public Function ToArgb(ByVal Color As Media.Color?) As Integer?
            If Color.HasValue Then
                Return Color.Value.ToArgb
            Else
                Return Nothing
            End If
        End Function
        ''' <summary>Gets <see cref="Drawing.Color"/> equivalent to given <see cref="Media.Color"/></summary>
        ''' <param name="Color"><see cref="Media.Color"/> to get <see cref="Drawing.Color"/> for</param>
        ''' <returns><see cref="Drawing.Color"/> initialized to same ARGB as <paramref name="Color"/></returns>
        ''' <version version="1.5.3.">This function is new in version 1.5.3</version>
        <Extension()>
        Function ToColor(ByVal Color As Media.Color) As Drawing.Color
            Return System.Drawing.Color.FromArgb(Color.A, Color.R, Color.G, Color.B)
        End Function
        ''' <summary>Gets <see cref="Drawing.Color"/> equivalent to given <see cref="Media.Color"/></summary>
        ''' <param name="Color"><see cref="Media.Color"/> to get <see cref="Drawing.Color"/> for</param>
        ''' <returns><see cref="Drawing.Color"/> initialized to same ARGB as <paramref name="Color"/>; null when <paramref name="Color"/> is null</returns>
        ''' <version version="1.5.3.">This function is new in version 1.5.3</version>
        <Extension()>
        Function ToColor(ByVal Color As Media.Color?) As Drawing.Color?
            If Color Is Nothing Then Return Nothing
            Return System.Drawing.Color.FromArgb(Color.Value.A, Color.Value.R, Color.Value.G, Color.Value.B)
        End Function
        ''' <summary>Gets <see cref="Media.Color"/> equivalent to given <see cref="Drawing.Color"/></summary>
        ''' <param name="Color"><see cref="Drawing.Color"/> to get <see cref="Media.Color"/> for</param>
        ''' <returns><see cref="Media.Color"/> initialized to same ARGB as <paramref name="Color"/></returns>
        ''' <version version="1.5.3.">This function is new in version 1.5.3</version>
        <Extension()>
        Function ToColor(ByVal Color As Drawing.Color) As Media.Color
            Return System.Windows.Media.Color.FromArgb(Color.A, Color.R, Color.G, Color.B)
        End Function
        ''' <summary>Gets <see cref="Media.Color"/> equivalent to given <see cref="Drawing.Color"/></summary>
        ''' <param name="Color"><see cref="Drawing.Color"/> to get <see cref="Media.Color"/> for</param>
        ''' <returns><see cref="Media.Color"/> initialized to same ARGB as <paramref name="Color"/>; null when <paramref name="Color"/> is null</returns>
        ''' <version version="1.5.3.">This function is new in version 1.5.3</version>
        <Extension()>
        Function ToColor(ByVal Color As Drawing.Color?) As Media.Color?
            If Color Is Nothing Then Return Nothing
            Return System.Windows.Media.Color.FromArgb(Color.Value.A, Color.Value.R, Color.Value.G, Color.Value.B)
        End Function
#End Region
#End Region

#Region "Windows"
        ''' <summary>Shows <see cref="Forms.Form"/> floating over <see cref="Window"/></summary>
        ''' <param name="Form"><see cref="Forms.Form"/> to be shown</param>
        ''' <param name="Owner">Owner <see cref="Window"/>; can be null</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Form"/> is null</exception>
        ''' <version version="1.5.2">Method introduced</version>
        <Extension()> Public Sub Show(ByVal Form As Forms.Form, ByVal Owner As Window)
            If Form Is Nothing Then Throw New ArgumentException("Form")
            If Owner Is Nothing Then
                Form.Show()
            Else
                Form.Show(Forms.NativeWindow.FromHandle(New SystemInterop.WindowInteropHelper(Owner).Handle))
            End If
        End Sub
        ''' <summary>Shows <see cref="Forms.Form"/> modally for given <see cref="Window"/></summary>
        ''' <param name="Form"><see cref="Forms.Form"/> to be shown</param>
        ''' <param name="Owner">Owner <see cref="Window"/>. Can be null.</param>
        ''' <returns>One of <see cref="forms.DialogResult"/> values</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Form"/> is null</exception>
        ''' <version version="1.5.2">Method introduced</version>
        <Extension()> Public Function ShowDialog(ByVal Form As Forms.Form, ByVal Owner As Window) As Forms.DialogResult
            If Form Is Nothing Then Throw New ArgumentException("Form")
            If Owner Is Nothing Then
                Return Form.ShowDialog()
            Else
                Return Form.ShowDialog(Forms.NativeWindow.FromHandle(New SystemInterop.WindowInteropHelper(Owner).Handle))
            End If
        End Function
        ''' <summary>Shows <see cref="Window"/> floating over given native window such as <see cref="Forms.Form"/></summary>
        ''' <param name="Window"><see cref="Window"/> to be shown</param>
        ''' <param name="Owner">Owner form or other native window. Can be null.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.2">Method introduced</version>
        <Extension()> Public Sub Show(ByVal Window As Window, ByVal Owner As Forms.IWin32Window)
            If Window Is Nothing Then Throw New ArgumentException("Window")
            Forms.Integration.ElementHost.EnableModelessKeyboardInterop(Window)
            If Owner Is Nothing Then
                Window.Show()
            Else
                Dim ioh As New SystemInterop.WindowInteropHelper(Window)
                ioh.Owner = Owner.Handle
                Window.Show()
            End If
        End Sub
        ''' <summary>Shows <see cref="Forms.Form"/> floating over <see cref="SystemInterop.IWin32Window"/></summary>
        ''' <param name="Form"><see cref="Forms.Form"/> to be shown</param>
        ''' <param name="Owner">Owner <see cref="SystemInterop.IWin32Window"/>; can be null</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Form"/> is null</exception>
        ''' <version version="1.5.3">Method introduced</version>
        <Extension()> Public Sub Show(ByVal Form As Forms.Form, ByVal Owner As SystemInterop.IWin32Window)
            If Form Is Nothing Then Throw New ArgumentException("Form")
            If Owner IsNot Nothing Then Form.Show(Forms.NativeWindow.FromHandle(Owner.Handle)) Else Form.Show()
        End Sub
        ''' <summary>Shows <see cref="Window"/> floating over given native window such as <see cref="Forms.Form"/></summary>
        ''' <param name="Window"><see cref="Window"/> to be shown</param>
        ''' <param name="Owner">Owner form or other native window. Can be null.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.3">Method introduced</version>
        <Extension()> Public Sub Show(ByVal Window As Window, ByVal Owner As SystemInterop.IWin32Window)
            If Window Is Nothing Then Throw New ArgumentException("Window")
            If Owner IsNot Nothing Then Window.Show(Forms.NativeWindow.FromHandle(Owner.Handle)) Else Window.Show()
        End Sub
        ''' <summary>Shows <see cref="Forms.Form"/> modally for given <see cref="SystemInterop.IWin32Window"/></summary>
        ''' <param name="Form"><see cref="Forms.Form"/> to be shown</param>
        ''' <param name="Owner">Owner <see cref="SystemInterop.IWin32Window"/>. Can be null.</param>
        ''' <returns>One of <see cref="forms.DialogResult"/> values</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Form"/> is null</exception>
        ''' <version version="1.5.3">Method introduced</version>
        <Extension()> Public Function ShowDialog(ByVal Form As Forms.Form, ByVal Owner As SystemInterop.IWin32Window) As Forms.DialogResult
            If Form Is Nothing Then Throw New ArgumentException("Form")
            If Owner IsNot Nothing Then Return Form.ShowDialog(Forms.NativeWindow.FromHandle(Owner.Handle)) Else Return Form.ShowDialog()
        End Function
        ''' <summary>Shows <see cref="Window"/> modally to given native window such as <see cref="Forms.Form"/></summary>
        ''' <param name="Window"><see cref="Window"/> to be shown</param>
        ''' <param name="Owner">Owner form or other native window. Can be null.</param>
        ''' <returns>A <see cref="Nullable(Of T)"/> value of type <see cref="Boolean"/> that signifies how <paramref name="Window"/> was closed by the user.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.3">Method introduced</version>
        <Extension()> Public Function ShowDialog(ByVal Window As Window, ByVal Owner As SystemInterop.IWin32Window) As Boolean?
            If Window Is Nothing Then Throw New ArgumentException("Window")
            If Owner IsNot Nothing Then Return Window.ShowDialog(Forms.NativeWindow.FromHandle(Owner.Handle)) Else Return Window.ShowDialog()
        End Function

        ''' <summary>Shows <see cref="Window"/> modally to given native window such as <see cref="Forms.Form"/></summary>
        ''' <param name="Window"><see cref="Window"/> to be shown</param>
        ''' <param name="Owner">Owner form or other native window. Can be null.</param>
        ''' <returns>A <see cref="Nullable(Of T)"/> value of type <see cref="Boolean"/> that signifies how <paramref name="Window"/> was closed by the user.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.2">Method introduced</version>
        <Extension()> Public Function ShowDialog(ByVal Window As Window, ByVal Owner As Forms.IWin32Window) As Boolean?
            If Window Is Nothing Then Throw New ArgumentException("Window")
            Forms.Integration.ElementHost.EnableModelessKeyboardInterop(Window)
            If Owner Is Nothing Then
                Return Window.ShowDialog()
            Else
                Dim ioh As New SystemInterop.WindowInteropHelper(Window)
                ioh.Owner = Owner.Handle
                Return Window.ShowDialog()
            End If
        End Function
        ''' <summary>Gets handle of <see cref="Window"/></summary>
        ''' <param name="Window"><see cref="Window"/> to get handle of</param>
        ''' <returns>Handle of <paramref name="Window"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.2">Method introduced</version>
        <Extension()> Public Function GetHandle(ByVal Window As Window) As IntPtr
            If Window Is Nothing Then Throw New ArgumentException("Window")
            Return New SystemInterop.WindowInteropHelper(Window).Handle
        End Function
        ''' <summary>Gets owner of <see cref="Window"/> as native <see cref="IWin32Window"/></summary>
        ''' <param name="Window">Window to get owner of</param>
        ''' <returns><see cref="Forms.Control"/> such as <see cref="Forms.Form"/> that is owner of <paramref name="Window"/> or <see cref="Forms.NativeWindow"/> when owner of <paramref name="Window"/> is not <see cref="Forms.Control"/>; <see langword="null"/> when <paramref name="Window"/> has no owner.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.2">Method introduced</version>
        <Extension()> Public Function GetOwner(ByVal Window As Window) As Forms.IWin32Window
            If Window Is Nothing Then Throw New ArgumentException("Window")
            Dim ohwnd As IntPtr = New SystemInterop.WindowInteropHelper(Window).Owner
            If ohwnd = IntPtr.Zero Then Return Nothing
            Return If(TryCast(Forms.Control.FromHandle(ohwnd), Forms.IWin32Window), DirectCast(Forms.NativeWindow.FromHandle(ohwnd), Forms.IWin32Window))
        End Function
        ''' <summary>Sest owner of <see cref="Window"/> to native window such as <see cref="Forms.Form"/></summary>
        ''' <param name="Window"><see cref="Window"/> to set owner of</param>
        ''' <param name="Owner">New owner of <paramref name="Window"/>; null to remove parent of <paramref name="Window"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.2">Method introduced</version>
        <Extension()> Public Sub SetOwner(ByVal Window As Window, ByVal Owner As Forms.IWin32Window)
            If Window Is Nothing Then Throw New ArgumentException("Window")
            Dim ioh As New SystemInterop.WindowInteropHelper(Window)
            ioh.Owner = If(Owner IsNot Nothing, Owner.Handle, IntPtr.Zero)
        End Sub
        ''' <summary>Sest owner of <see cref="Window"/> to native window such as <see cref="Forms.Form"/></summary>
        ''' <param name="Window"><see cref="Window"/> to set owner of</param>
        ''' <param name="Owner">New owner of <paramref name="Window"/>; null to remove parent of <paramref name="Window"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        ''' <version version="1.5.3">Method introduced</version>
        <Extension()> Public Sub SetOwner(ByVal Window As Window, ByVal Owner As SystemInterop.IWin32Window)
            If Window Is Nothing Then Throw New ArgumentException("Window")
            Dim ioh As New SystemInterop.WindowInteropHelper(Window)
            ioh.Owner = If(Owner IsNot Nothing, Owner.Handle, IntPtr.Zero)
        End Sub
#End Region

#Region "CommonDialogs"
        ''' <summary>Runs a common dialog box with the specified owner (<see cref="Window"/>).</summary>
        ''' <param name="dlg">A dialog to be shown</param>
        ''' <param name="owner">A <see cref="Window"/> that will own modal dialog. If null modal dialog is shown without explicit parent.</param>
        ''' <returns>True if the user clicks OK in the dialog box; otherwise, False.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="dlg"/> is null</exception>
        ''' <version version="1.3.5">This function is new in version 1.3.5</version>
        <Extension()>
        Public Function ShowDialog(ByVal dlg As Forms.CommonDialog, ByVal owner As Window) As Boolean
            If owner Is Nothing Then Return dlg.ShowDialog() = Forms.DialogResult.OK
            Return dlg.ShowDialog(New Win32WindowHelper(owner)) = Forms.DialogResult.OK
        End Function
        ''' <summary>Runs a common dialog box with the specified owner (<see cref="IWin32Window"/>).</summary>
        ''' <param name="owner">An <see cref="IWin32Window"/> that will own modal dialog. If null modal dialog is shown without explicit parent.</param>
        ''' <param name="dlg">A dialog to be shown</param>
        ''' <returns>True if the user clicks OK in the dialog box; otherwise, False.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="dlg"/> is null</exception>
        ''' <version version="1.3.5">This function is new in version 1.3.5</version>
        <Extension()>
        Public Function ShowDialog(ByVal dlg As Forms.CommonDialog, ByVal owner As IWin32Window) As Boolean
            If owner Is Nothing Then Return dlg.ShowDialog() = Forms.DialogResult.OK
            Return dlg.ShowDialog(New Win32WindowHelper(owner)) = Forms.DialogResult.OK
        End Function

#Region "Helpers"
        ''' <summary>Helper class which implements <see cref="Forms.IWin32Window"/> and <see cref="Interop.IWin32Window"/> interfaces for various purposes</summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Private Class Win32WindowHelper
            Implements Forms.IWin32Window, SystemInterop.IWin32Window
            ''' <summary>CTor - creates a new instance of the <see cref="Win32WindowHelper"/> class from <see cref="Window"/></summary>
            ''' <param name="window">A <see cref="Window"/></param>
            ''' <exception cref="ArgumentNullException"><paramref name="window"/> is null</exception>
            Public Sub New(ByVal window As Window)
                If window Is Nothing Then Throw New ArgumentNullException("window")
                _Handle = New SystemInterop.WindowInteropHelper(window).Handle
            End Sub
            ''' <summary>CTor - creates a new instance of the <see cref="Win32WindowHelper"/> class from <see cref="IWin32Window"/></summary>
            ''' <param name="window">A <see cref="IWin32Window"/></param>
            ''' <exception cref="ArgumentNullException"><paramref name="window"/> is null</exception>
            Public Sub New(ByVal window As IWin32Window)
                If window Is Nothing Then Throw New ArgumentNullException("window")
                _Handle = window.Handle
            End Sub
            Private ReadOnly _Handle As IntPtr
            ''' <summary>Gets the handle to the window represented by the implementer.</summary>
            ''' <returns>A handle to the window represented by the implementer.</returns>
            Public ReadOnly Property Handle As IntPtr Implements Forms.IWin32Window.Handle, IWin32Window.Handle
                Get
                    Return _Handle
                End Get
            End Property
        End Class
#End Region
#End Region
    End Module
End Namespace