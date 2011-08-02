Imports Tools.WindowsT.WPF, Tools.LinqT, Tools.ExtensionsT
Imports mBox = Tools.WindowsT.IndependentT.MessageBox, System.Linq
Imports Microsoft.Win32
Imports Tools.MetadataT

''' <summary>A windows used for browsing pictures</summary>
Class BrowserWindow
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
    'Ctrl'S     Save changes
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
                    Case Key.S : Save()
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
        If IfNecessaryAskAndSave() Then
            'TODO:
        End If
    End Sub

    ''' <summary>Shows previous image</summary>
    Private Sub GoPrev()
        If IfNecessaryAskAndSave() Then
            'TODO:
        End If
    End Sub
    ''' <summary>Opens a file</summary>
    Private Function OpenFile(Optional fileName As String = Nothing) As Boolean
        If IfNecessaryAskAndSave() Then
            If fileName = "" Then
                Dim dlg As New OpenFileDialog() With {.Filter = My.Resources.fil_Jpeg + "|*.jpg;*.jpeg;*.jfif"}
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
                directory = IO.Directory.GetFiles(IO.Path.GetDirectoryName(fileName))
                index = directory.IndexOf(fileName)
                Return True
            Catch ex As Exception
                If mBox.Error_XPTIBWO(ex, String.Format(My.Resources.err_LoadFile, fileName), My.Resources.txt_OpenFile, Buttons:=mBox.MessageBoxButton.Buttons.Retry Or mBox.MessageBoxButton.Buttons.Cancel, Owner:=Me) = Forms.DialogResult.Retry Then
                    Return OpenFile()
                Else
                    Return False
                End If
            End Try
        Else
            Return False
        End If
    End Function
    ''' <summary>Saves changes in IPTC</summary>
    ''' <returns>True if save is OK or user is OK with faiilure, false to cancel cutrrent operation</returns>
    Private Function Save() As Boolean
        If Metadata.Iptc.IsChanged Then
            'TODO:
        Else
            Return True
        End If
    End Function
    ''' <summary>Copies a file</summary>
    Private Sub Copy()
        If IfNecessaryAskAndSave() Then
            'TODO:
        End If
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
        If IfNecessaryAskAndSave() Then Me.Close()
    End Sub
    ''' <summary>If necessary asks user to save changes and saves them</summary>
    ''' <remarks>True if no pending changes or if save was successfull; false to cancel pending operation (user decission)</remarks>
    Private Function IfNecessaryAskAndSave() As Boolean
        Dim answer = AskIfNecessary()
        If Not answer.HasValue Then Return False
        If answer.Value Then
            Return Save()
        Else
            Return True
        End If
    End Function
    ''' <summary>Asks user to save changes</summary>
    ''' <remarks>True to save changes, false to discard changes, null to cancel operation</remarks>
    Private Function AskSave() As Boolean?
        Select Case mBox.MsgBox("Unsaved changes. Save?", MsgBoxStyle.YesNoCancel, "Unsaved changes", Me)
            Case MsgBoxResult.Yes : Return True
            Case MsgBoxResult.No : Return False
            Case Else : Return Nothing
        End Select
    End Function
    ''' <summary>If data were changed asks user to save them</summary>
    ''' <remarks>True to save changes, false to discard changes, null to cancel operation</remarks>
    Private Function AskIfNecessary() As Boolean?
        If Metadata.Iptc Is Nothing Then Return False
        If Metadata.Iptc.IsChanged Then
            Return AskSave()
        Else : Return False
        End If
    End Function
    ''' <summary>Shows IPTC dialog</summary>
    Private Sub ShowIptc()
        Dim dlgIptc As New IptcEditor(Metadata.Iptc)
        dlgIptc.ShowDialog(Me)
    End Sub
    ''' <summary>Shows rating dialog</summary>
    Private Sub ShowRating()
        Dim dlgRating As New RatingEditor(Metadata.Iptc)
        dlgRating.ShowDialog(Me)
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
