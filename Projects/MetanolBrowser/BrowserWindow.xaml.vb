Imports Tools.WindowsT.WPF, Tools.LinqT, Tools.ExtensionsT
Imports mBox = Tools.WindowsT.IndependentT.MessageBox, System.Linq
Imports Microsoft.Win32
Imports Tools.MetadataT
Imports Tools.DrawingT.DrawingIOt.JPEG
Imports MessageBoxButton = Tools.WindowsT.IndependentT.MessageBox.MessageBoxButton

''' <summary>A windows used for browsing pictures</summary>
Class BrowserWindow
    Private Const JpegMask As String = "*.jpg;*.jpeg;*.jfif"
    ''' <summary>CTor - creates a new instance of the <see cref="BrowserWindow"/> class</summary>
    Public Sub New()
        InitializeComponent()
        Title = String.Format("{0} {1}", My.Application.Info.Title, My.Application.Info.Version)
    End Sub
    Private Sub BrowserWindow_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Dim args = Environment.GetCommandLineArgs
        Metadata = New MetadataCollection
        If args.Length > 1 Then
            If Not OpenFile(args(1)) Then Me.Close()
        Else
            If Not OpenFile() Then Me.Close()
        End If
    End Sub

    ''' <summary>Gets or sets current metadata</summary>
    Friend Property Metadata As MetadataCollection
        Get
            Return DataContext
        End Get
        Set(value As MetadataCollection)
            DataContext = value
        End Set
    End Property

#Region "Index"
    ''' <summary>Gets or sets index of current file in directory</summary>      
    Public Property Index As Integer
        Get
            Return GetValue(IndexProperty)
        End Get
        Set(ByVal value As Integer)
            SetValue(IndexProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="Index"/> dependency property</summary>                                                   
    Public Shared ReadOnly IndexProperty As DependencyProperty = DependencyProperty.Register(
        "Index", GetType(Integer), GetType(BrowserWindow), New FrameworkPropertyMetadata(-1, AddressOf OnIndexChanged))
    ''' <summary>Called when value of the <see cref="Index"/> property changes</summary>
    ''' <param name="d">Source of the event  - must be <see cref="BrowserWindow"/></param>
    ''' <param name="e">Event arguments</param>
    Private Shared Sub OnIndexChanged(ByVal d As System.Windows.DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        DirectCast(d, BrowserWindow).OnIndexChanged(e)
    End Sub
    ''' <summary>Callled when value of the <see cref="Index"/> property changes</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnIndexChanged(e As DependencyPropertyChangedEventArgs)
        Me.SetValue(DisplayIndexPropertyKey, Index + 1)
        Try
            Title = String.Format(My.Resources.txt_WindowTitle, My.Application.Info.Title, My.Application.Info.Version, IO.Path.GetFileName(Directory(Index)))
        Catch :End Try
    End Sub

    ''' <summary>Gets 1-based index of current file in directory</summary>
    Public ReadOnly Property DisplayIndex As Integer
        Get
            Return GetValue(BrowserWindow.DisplayIndexProperty)
        End Get
    End Property
    ''' <summary>Metadata for the <see cref="DisplayIndex"/> property</summary>
    Private Shared ReadOnly DisplayIndexPropertyKey As DependencyPropertyKey = DependencyProperty.RegisterReadOnly("DisplayIndex", GetType(Integer), GetType(BrowserWindow), New FrameworkPropertyMetadata(0))
    ''' <summary>Key of the <see cref="DisplayIndex"/> property</summary>
    Public Shared ReadOnly DisplayIndexProperty As DependencyProperty = DisplayIndexPropertyKey.DependencyProperty
#End Region

#Region "Directory"
    ''' <summary>Gets or sets array of files in current directory</summary>      
    Public Property Directory As String()
        Get
            Return GetValue(DirectoryProperty)
        End Get
        Set(ByVal value As String())
            SetValue(DirectoryProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="Directory"/> dependency property</summary>                                                   
    Public Shared ReadOnly DirectoryProperty As DependencyProperty = DependencyProperty.Register(
        "Directory", GetType(String()), GetType(BrowserWindow), New FrameworkPropertyMetadata(New String() {}))
#End Region

#Region "DirectoryName"
    ''' <summary>Gets or sets path of current directory</summary>      
    Public Property DirectoryName As String
        Get
            Return GetValue(DirectoryNameProperty)
        End Get
        Set(ByVal value As String)
            SetValue(DirectoryNameProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="DirectoryName"/> dependency property</summary>                                                   
    Public Shared ReadOnly DirectoryNameProperty As DependencyProperty = DependencyProperty.Register(
        "DirectoryName", GetType(String), GetType(BrowserWindow), New FrameworkPropertyMetadata(Nothing))
#End Region

#Region "IsFullscreen"
    ''' <summary>Gets or sets value indicating if this window is displayed fullscreen</summary>
    Public Property IsFullscreen() As Boolean
        <DebuggerStepThrough()> Get
            Return GetValue(IsFullscreenProperty)
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Boolean)
            SetValue(IsFullscreenProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="IsFullscreen"/> dependency property</summary>
    Public Shared ReadOnly IsFullscreenProperty As DependencyProperty =
                           DependencyProperty.Register("IsFullscreen", GetType(Boolean), GetType(BrowserWindow),
                           New FrameworkPropertyMetadata(False, AddressOf OnIsFullscreenChanged))
    Private Shared Sub OnIsFullscreenChanged(ByVal d As System.Windows.DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        DirectCast(d, BrowserWindow).OnIsFullscreenChanged(e)
    End Sub
    Private oldWindowState As WindowState?
    ''' <summary>Called when value of the <see cref="IsFullscreenProperty"/> changes</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnIsFullscreenChanged(e As DependencyPropertyChangedEventArgs)
        'Note: This is done via DataTrigger in XAML, but it was not reliable, so this code is here to ensure
        If IsFullscreen Then
            oldWindowState = WindowState
            If ResizeMode <> ResizeMode.NoResize Then ResizeMode = ResizeMode.NoResize
            If WindowStyle <> WindowStyle.None Then WindowStyle = WindowStyle.None
            If WindowState = Windows.WindowState.Maximized Then WindowState = Windows.WindowState.Normal 'To ensure normal-maximized change (required to cover taskbar)
            If WindowState <> WindowState.Maximized Then WindowState = WindowState.Maximized
        Else
            If ResizeMode <> ResizeMode.CanResize AndAlso ResizeMode <> ResizeMode.CanResizeWithGrip Then ResizeMode = ResizeMode.CanResize
            If WindowStyle <> WindowStyle.SingleBorderWindow Then WindowStyle = WindowStyle.SingleBorderWindow
            If oldWindowState.HasValue AndAlso oldWindowState <> WindowState Then WindowState = oldWindowState
            oldWindowState = Nothing
        End If
    End Sub
#End Region

    Private Sub grdIptc_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles grdIptc.MouseDown
        If e.ClickCount = 2 AndAlso e.ChangedButton = MouseButton.Left Then
            ShowIptc()
            e.Handled = True
        End If
    End Sub
    Private Sub lblRating_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles lblRating.MouseDoubleClick
        ShowRating()
        e.Handled = True
    End Sub

#Region "Command methods"
    ''' <summary>Shows next image</summary>
    Private Sub GoNext()
        If Index + 1 >= Directory.Length Then
            OnDirectoryEnd(1)
        Else
            Index += 1
            Try
                LoadFile(Directory(Index))
            Catch ex As Exception
                mBox.Error_XW(ex, Me)
                GoNext()
            End Try

        End If
    End Sub

    ''' <summary>Shows previous image</summary>
    Private Sub GoPrev()
        If Index <= 0 Then
            OnDirectoryEnd(-1)
        Else
            Index -= 1
            Try
                LoadFile(Directory(Index))
            Catch ex As Exception
                mBox.Error_XW(ex, Me)
                GoPrev()
            End Try
        End If
    End Sub

    ''' <summary>Called when directory end or start is reached</summary>
    ''' <param name="direction">1 for end, -1 for start</param>
    Private Sub OnDirectoryEnd(direction%)
        If direction.NotIn(-1, 1) Then Throw New ArgumentException(My.Resources.ex_1orMinus1, "direction")
        Dim dl As New DirectoryEndDialog(DirectoryName, direction)
        If dl.ShowDialog(Me) Then
            Try
                Directory = GetSupportedFilesFromDirectory(dl.Folder)
            Catch ex As Exception
                mBox.Error_XW(ex, Me)
                OnDirectoryEnd(direction)
            End Try
            DirectoryName = dl.Folder
            If Directory.Length = 0 Then
                OnDirectoryEnd(direction)
            Else
                Index = If(direction = 1, 0, Directory.Length - 1)
                Title = String.Format(My.Resources.txt_WindowTitle, My.Application.Info.Title, My.Application.Info.Version, IO.Path.GetFileName(Directory(Index)))
                Try
                    LoadFile(Directory(Index))
                Catch ex As Exception
                    mBox.Error_XW(ex, Me)
                    If direction = 1 Then GoNext() Else GoPrev()
                End Try
            End If
        End If
    End Sub
    ''' <summary>Opens a file</summary>
    ''' <param name="fileName">A name of file to open, if null asks user</param>
    Private Function OpenFile(Optional fileName As String = Nothing) As Boolean
        If fileName = "" Then
            Dim dlg As New OpenFileDialog() With {.Filter = My.Resources.fil_Jpeg + "|" + JpegMask}
            If dlg.ShowDialog(Me).Value Then
                fileName = dlg.FileName
            Else
                Return False
            End If
        End If
        If Not IO.Path.GetExtension(fileName).ToLower.In(".jpeg", ".jpg", ".jfif") Then
            If mBox.MsgBoxFW(My.Resources.err_NotAJpegFile, MsgBoxStyle.RetryCancel Or MsgBoxStyle.Exclamation, My.Resources.txt_OpenFile, Me, fileName) = MsgBoxResult.Retry Then
                Return OpenFile()
            Else
                Return False
            End If
        End If
        If Not IO.File.Exists(fileName) Then
            If mBox.MsgBoxFW(My.Resources.err_FileDoesNotExist, MsgBoxStyle.RetryCancel Or MsgBoxStyle.Exclamation, My.Resources.txt_OpenFile, Me, fileName) = MsgBoxResult.Retry Then
                Return OpenFile()
            Else
                Return False
            End If
        End If
        Try
            LoadFile(fileName)
            Directory = GetSupportedFilesFromDirectory(IO.Path.GetDirectoryName(fileName))
            DirectoryName = IO.Path.GetDirectoryName(fileName)
            Index = Directory.IndexOf(fileName)
            Title = String.Format(My.Resources.txt_WindowTitle, My.Application.Info.Title, My.Application.Info.Version, IO.Path.GetFileName(Directory(Index)))
            Return True
        Catch ex As Exception
            If mBox.Error_XPTIBWO(ex, String.Format(My.Resources.err_LoadFile, fileName), My.Resources.txt_OpenFile, Buttons:=mBox.MessageBoxButton.Buttons.Retry Or mBox.MessageBoxButton.Buttons.Cancel, Owner:=Me) = Forms.DialogResult.Retry Then
                Return OpenFile()
            Else
                Return False
            End If
        End Try
    End Function

    ''' <summary>Gets files supported for browsing from given directory</summary>
    ''' <param name="directory">Directory to get files from</param>
    ''' <returns>Array of file paths</returns>
    Private Function GetSupportedFilesFromDirectory(directory$) As String()
        Return (From file In IO.Directory.EnumerateFiles(directory) Where IO.Path.GetExtension(file).ToLowerInvariant.In(".jpg", ".jpeg", ".jfif")).ToArray
    End Function

    ''' <summary>Copies a file</summary>
    Private Sub Copy()
        'TODO:
    End Sub
    ''' <summary>Creates link to a file</summary>
    Private Sub CreateLink()
        'TODO:
    End Sub
    ''' <summary>Toggles fullscreen mode</summary>
    Private Sub ToggleFullscreen()
        IsFullscreen = Not IsFullscreen
    End Sub
    ''' <summary>Quits application</summary>
    Private Sub [Exit]()
        Me.Close()
    End Sub

    ''' <summary>Shows IPTC dialog</summary>
    Private Sub ShowIptc()
        ShowIptcEditDialog(New IptcEditor(Metadata.Iptc))
    End Sub
    ''' <summary>Shows rating dialog</summary>
    Private Sub ShowRating()
        Dim win As RatingEditor = New RatingEditor(Metadata.Iptc)
        AddHandler win.Loaded, Sub(sender, e)
                                   win.Left = lblRating.PointToScreen(New Point(lblRating.ActualWidth / 2, 0)).X - win.ActualWidth / 2
                                   win.Top = lblRating.PointToScreen(New Point(0, lblRating.ActualHeight)).Y - win.ActualHeight
                               End Sub
        ShowIptcEditDialog(win)
    End Sub

    ''' <summary>Shows dialog for IPTC editing, when dialog closes either saves or discards IPTC changes</summary>
    ''' <param name="dialog">The dialog to be shown</param>
    ''' <exception cref="ArgumentNullException"><paramref name="dialog"/> is null</exception>
    Private Sub ShowIptcEditDialog(dialog As Window)
        If dialog Is Nothing Then Throw New ArgumentException("dialog")
        Dim result = dialog.ShowDialog(Me)
        If result.HasValue Then
            If result Then 'Note: Dialog can change current picture
                If Metadata IsNot Nothing AndAlso Metadata.Iptc IsNot Nothing AndAlso Metadata.Iptc.IsChanged Then SaveIptc()
            Else
                If Metadata IsNot Nothing AndAlso Metadata.Iptc IsNot Nothing AndAlso Metadata.Iptc.IsChanged Then ReloadIptc()
            End If
        End If
    End Sub

    ''' <summary>Saves IPTC data to file</summary>
    ''' <param name="onErrorButtons">
    ''' Buttons to be shown in case of save error.
    ''' <see cref="MessageBoxButton.Buttons.Abort"/> results in reload of original IPTC data from file.
    ''' <see cref="MessageBoxButton.Buttons.Retry"/> is handled internally and causes new attempt to save IPTC data.
    ''' Other buttons are just shown and appropriate <see cref="Forms.DialogResult"/> value is returned on click. No action is taken.
    ''' </param>
    ''' <returns>Value indicating which button was clicked</returns>
    Friend Function SaveIptc(Optional onErrorButtons As mBox.MessageBoxButton.Buttons = MessageBoxButton.Buttons.Abort Or MessageBoxButton.Buttons.Retry) As Forms.DialogResult
retry:  Dim jpeg As JPEGReader = Nothing
        Try
            jpeg = New JPEGReader(Directory(Index), True)
            jpeg.IPTCEmbed(Metadata.Iptc.GetBytes())
        Catch ex As Exception
            Dim result = mBox.Error_XPTIBWO(ex, My.Resources.err_IptcSave, My.Resources.txt_SaveIptc, mBox.MessageBoxIcons.Error, onErrorButtons, Me)
            Select Case result
                Case Forms.DialogResult.Retry : GoTo retry
                Case Forms.DialogResult.Abort : ReloadIptc()
                    'Case Else: 'Do nothing
            End Select
            Return result
        Finally
            If jpeg IsNot Nothing Then jpeg.Dispose()
        End Try
        Return Forms.DialogResult.OK
    End Function

    ''' <summary>Reloads IPTC data from file</summary>
    Private Sub ReloadIptc()
retry:  Dim jpeg As JPEGReader = Nothing
        Try
            jpeg = New JPEGReader(Directory(Index))
            If jpeg.ContainsIptc Then
                Metadata.Iptc = New IptcInternal(jpeg)
            Else
                Metadata.Iptc = New IptcInternal
            End If
        Catch ex As Exception
            If mBox.Error_XPTIBWO(ex, My.Resources.err_IptcReload, My.Resources.txt_ReloadIptc, mBox.MessageBoxIcons.Error, mBox.MessageBoxButton.Buttons.Abort Or mBox.MessageBoxButton.Buttons.Retry, Me) = Forms.DialogResult.Retry Then
                GoTo retry
            Else
                Metadata.Iptc = New IptcInternal
            End If
        End Try
    End Sub

    ''' <summary>Loads file</summary>
    ''' <param name="fileName">Name of file to load</param>
    Private Sub LoadFile(fileName$)
        Metadata.Exif = Nothing
        Metadata.Iptc = Nothing
        Metadata.System = New SystemMetadata(fileName)
        Dim bmp As BitmapImage = New BitmapImage() 'With {.StreamSource = IO.File.Open(fileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)}
        bmp.BeginInit()
        Try
            bmp.CacheOption = BitmapCacheOption.OnLoad
            bmp.UriSource = New Uri(fileName)
        Finally
            bmp.EndInit()
        End Try
        imgImage.Source = bmp
        Using jpeg As New Tools.DrawingT.DrawingIOt.JPEG.JPEGReader(fileName)
            If jpeg.ContainsExif Then
                Metadata.Exif = jpeg.GetExif()
            End If
            If jpeg.ContainsIptc Then
                Metadata.Iptc = New IptcInternal(jpeg)
            Else
                Metadata.Iptc = New IptcInternal
            End If
        End Using
    End Sub
#End Region

#Region "Commands"
    'Keyboard shortcuts
    '←          Left
    '→          Right
    'F5         Copy
    'F12        Link
    'Enter      Fullscreen (Also Alt+Enter)
    'Esc        Exit
    'I          IPTC
    '* / Ctrl+R Rating
    'Ctrl+O / O Open file

    Private Sub NextPage_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        GoNext()
    End Sub

    Private Sub PreviousPage_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        GoPrev()
    End Sub

    Private Sub FileCopy_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        Copy()
    End Sub

    Private Sub FileLink_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        CreateLink()
    End Sub

    Private Sub ToggleFullscreen_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        ToggleFullscreen()
    End Sub

    Private Sub Close_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        [Exit]()
    End Sub

    Private Sub EditIptc_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        ShowIptc()
    End Sub

    Private Sub EditRating_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        ShowRating()
    End Sub

    Private Sub Open_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        OpenFile()
    End Sub
#End Region
End Class
