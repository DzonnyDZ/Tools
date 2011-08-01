Imports Tools.WindowsT.WPF
Imports mBox = Tools.WindowsT.IndependentT.MessageBox
''' <summary>A windows used for browsing pictures</summary>
Class BrowserWindow

    ''' <summary>Gets or sets current metadata</summary>
    Private Property Metadata As MetadataCollection
        Get
            Return DataContext
        End Get
        Set(value As MetadataCollection)
            DataContext = value
        End Set
    End Property

   


    Private Sub Window_PreviewKeyDown(sender As Grid, e As System.Windows.Input.KeyEventArgs) Handles MyBase.PreviewKeyDown
        Select Case e.Key
            Case Key.Down
                If rtgTech.IsFocused Then
                    rtgArt.Focus()
                ElseIf rtgInfo.IsFocused Then
                    rtgOver.Focus()
                End If
            Case Key.Up
                If rtgArt.IsFocused Then
                    rtgTech.Focus()
                ElseIf rtgOver.IsFocused Then
                    rtgInfo.Focus()
                End If
            Case Key.Left
                If sender.FlowDirection = Windows.FlowDirection.RightToLeft Then GoTo Right
Left:           If rtgInfo.IsFocused Then
                    rtgTech.Focus()
                ElseIf rtgOver.IsFocused Then
                    rtgArt.Focus()
                End If
            Case Key.Right
                If sender.FlowDirection = Windows.FlowDirection.RightToLeft Then GoTo Left
Right:          If rtgTech.IsFocused Then
                    rtgInfo.Focus()
                ElseIf rtgArt.IsFocused Then
                    rtgOver.Focus()
                End If
            Case Else : Return
        End Select
        e.Handled = True
    End Sub

    Private Sub grdIptc_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles grdIptc.MouseDown
        If e.ClickCount = 2 AndAlso e.ChangedButton = MouseButton.Left Then
            Dim dlgIptc As New IptcEditor(Metadata.Iptc)
            dlgIptc.ShowDialog(Me)
        End If
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnSave.Click
        btnSave.ContextMenu.IsOpen = True
    End Sub

    Private Sub btnSave_KeyDown(sender As System.Object, e As System.Windows.Input.KeyEventArgs) Handles btnSave.KeyDown
        Select Case Keyboard.Modifiers
            Case ModifierKeys.None
                Select Case e.Key
                    Case Key.Left : If Me.FlowDirection = Windows.FlowDirection.LeftToRight Then GoPrev() Else GoNext()
                    Case Key.Right : If Me.FlowDirection = Windows.FlowDirection.RightToLeft Then GoPrev() Else GoNext()
                    Case Else : Return
                End Select
            Case Else : Return
        End Select
        e.Handled = True
    End Sub

    Private Sub BrowserWindow_KeyDown(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles Me.KeyDown
        Select Case Keyboard.Modifiers
            Case ModifierKeys.None
                Select Case e.Key
                    Case Key.Left : If Me.FlowDirection = Windows.FlowDirection.LeftToRight Then GoPrev() Else GoNext()
                    Case Key.Right : If Me.FlowDirection = Windows.FlowDirection.RightToLeft Then GoPrev() Else GoNext()
                    Case Key.F5 : Copy()
                    Case Key.F12 : CreateLink()
                    Case Key.Return : ToggleFullscreen()
                    Case Key.Escape : [Exit]()
                    Case Else : Return
                End Select
            Case ModifierKeys.Control
                Select Case e.Key
                    Case Key.O : OpenFile()
                    Case Key.S : Save()
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
    Private Sub OpenFile()
        If IfNecessaryAskAndSave() Then
            'TODO:
        End If
    End Sub
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
        If Metadata.Iptc.IsChanged Then
            Return AskSave()
        Else : Return False
        End If
    End Function
#End Region

#Region "Menu items events"
    Private Sub mniSave_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles mniSave.Click
        Save()
    End Sub

    Private Sub mniPrev_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles mniPrev.Click
        GoPrev()
    End Sub

    Private Sub mniNext_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles mniNext.Click
        GoNext()
    End Sub

    Private Sub mniOpen_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles mniOpen.Click
        OpenFile()
    End Sub

    Private Sub mniCopy_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles mniCopy.Click
        Copy()
    End Sub

    Private Sub mniLink_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles mniLink.Click
        CreateLink()
    End Sub

    Private Sub mniFullScreen_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles mniFullScreen.Click
        ToggleFullscreen()
    End Sub

    Private Sub mniExit_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles mniExit.Click
        [Exit]()
    End Sub
#End Region
End Class
