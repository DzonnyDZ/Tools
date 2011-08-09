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
    Private Property Metadata As MetadataCollection
        Get
            Return DataContext
        End Get
        Set(value As MetadataCollection)
            DataContext = value
        End Set
    End Property
    Private index%
    Private directory() As String
    Private directoryName As String

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


    'Keyboard shortcuts
    '←          Left
    '→          Right
    'F5         Copy
    'F12        Link
    'Enter      Fullscreen
    'Esc        Exit
    'I          IPTC
    '*/ Ctrl+R  Rating
    'Ctrl+O     Open file
    Private Sub BrowserWindow_PreviewKeyDown(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles Me.PreviewKeyDown
        Select Case Keyboard.Modifiers
            Case ModifierKeys.None
                Select Case e.Key
                    Case Key.Left : If Me.FlowDirection = Windows.FlowDirection.LeftToRight Then GoPrev() Else GoNext()
                    Case Key.Right : If Me.FlowDirection = Windows.FlowDirection.RightToLeft Then GoPrev() Else GoNext()
                    Case Key.F5 : Copy()
                    Case Key.F12 : CreateLink()
                    Case Key.Return : ToggleFullscreen()
                    Case Key.Escape : [Exit]()
                    Case Key.I : ShowIptc()
                    Case Key.Multiply : ShowRating()
                    Case Else : Return
                End Select
            Case ModifierKeys.Control
                Select Case e.Key
                    Case Key.O : OpenFile()
                    Case Key.R : ShowRating()
                    Case Else : Return
                End Select
            Case Else : Return
        End Select
        e.Handled = True
    End Sub

#Region "Command methods"
    ''' <summary>Shows next image</summary>
    Private Sub GoNext()
        If index + 1 >= directory.Length Then
            OnDirectoryEnd(1)
        Else
            index += 1
            Try
                LoadFile(directory(index))
            Catch ex As Exception
                mBox.Error_XW(ex, Me)
                GoNext()
            End Try

        End If
    End Sub

    ''' <summary>Shows previous image</summary>
    Private Sub GoPrev()
        If index <= 0 Then
            OnDirectoryEnd(-1)
        Else
            index -= 1
            Try
                LoadFile(directory(index))
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
        Dim dl As New DirectoryEndDialog(directoryName, direction)
        If dl.ShowDialog(Me) Then
            Try
                directory = IO.Directory.GetFiles(dl.Folder, JpegMask)
            Catch ex As Exception
                mBox.Error_XW(ex, Me)
                OnDirectoryEnd(direction)
            End Try
            directoryName = dl.Folder
            If directory.Length = 0 Then
                OnDirectoryEnd(direction)
            Else
                index = If(direction = 1, 0, directory.Length - 1)
                Try
                    LoadFile(directory(index))
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
            directory = IO.Directory.GetFiles(IO.Path.GetDirectoryName(fileName), JpegMask)
            directoryName = IO.Path.GetDirectoryName(fileName)
            index = directory.IndexOf(fileName)
            Return True
        Catch ex As Exception
            If mBox.Error_XPTIBWO(ex, String.Format(My.Resources.err_LoadFile, fileName), My.Resources.txt_OpenFile, Buttons:=mBox.MessageBoxButton.Buttons.Retry Or mBox.MessageBoxButton.Buttons.Cancel, Owner:=Me) = Forms.DialogResult.Retry Then
                Return OpenFile()
            Else
                Return False
            End If
        End Try
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
        'TODO:
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
        ShowIptcEditDialog(New RatingEditor(Metadata.Iptc))
    End Sub

    ''' <summary>Shows dialog for IPTC editing, when dialog closes either saves or discards IPTC changes</summary>
    ''' <param name="dialog">The dialog to be shown</param>
    ''' <exception cref="ArgumentNullException"><paramref name="dialog"/> is null</exception>
    Private Sub ShowIptcEditDialog(dialog As Window)
        If dialog Is Nothing Then Throw New ArgumentException("dialog")
        If dialog.ShowDialog(Me) Then
            If Metadata.Iptc.IsChanged Then SaveIptc()
        Else
            If Metadata.Iptc.IsChanged Then ReloadIptc()
        End If
    End Sub

    ''' <summary>Saves IPTC data to file</summary>
    Private Sub SaveIptc()
retry:  Dim jpeg As JPEGReader = Nothing
        Try
            jpeg = New JPEGReader(directory(index), True)
            jpeg.IPTCEmbed(Metadata.Iptc.GetBytes())
        Catch ex As Exception
            If mBox.Error_XPTIBWO(ex, My.Resources.err_IptcSave, My.Resources.txt_SaveIptc, mBox.MessageBoxIcons.Error, mBox.MessageBoxButton.Buttons.Abort Or mBox.MessageBoxButton.Buttons.Retry, Me) = Forms.DialogResult.Retry Then
                GoTo retry
            Else
                ReloadIptc()
            End If
        Finally
            If jpeg IsNot Nothing Then jpeg.Dispose()
        End Try
    End Sub

    ''' <summary>Reloads IPTC data from file</summary>
    Private Sub ReloadIptc()
retry:  Dim jpeg As JPEGReader = Nothing
        Try
            jpeg = New JPEGReader(directory(index))
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
        Dim bmp As BitmapImage = New BitmapImage() With {.StreamSource = IO.File.Open(fileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)}
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

End Class
