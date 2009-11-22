#If Config <= Nightly Then  'Stage: Nightly
Namespace WindowsT.WPF.DialogsT
    ''' <summary>Window that impements WPF message box</summary>
    ''' <version version="1.5.3." stage="Nightly">Added ability to copy all text of message box using Ctrl+C</version>
    Partial Friend NotInheritable Class MessageBoxWindow

        ''' <summary>Raises the <see cref="E:System.Windows.FrameworkElement.SizeChanged" /> event, using the specified information as part of the eventual event data.                 </summary>
        ''' <param name="sizeInfo">Details of the old and new size involved in the change.</param>
        Protected Overrides Sub OnRenderSizeChanged(ByVal sizeInfo As System.Windows.SizeChangedInfo)
            MyBase.OnRenderSizeChanged(sizeInfo)
            If Me.RenderSize.Height > Me.RenderSize.Width * 2 Then
                Me.Width = Me.RenderSize.Height
            ElseIf Me.RenderSize.Width > Me.RenderSize.Height * 3 Then
                Me.Width = Me.RenderSize.Height
            End If
        End Sub
        Private Sub MsgBoxControl_FlowDirectionChanged(ByVal sender As Object, ByVal e As InteropT.DependencyPropertyChangedEventArgsEventArgs) Handles MsgBoxControl.FlowDirectionChanged
            Me.FlowDirection = MsgBoxControl.FlowDirection
        End Sub


        Private Sub MsgBoxControl_TitleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MsgBoxControl.TitleChanged
            If MsgBoxControl.Title Is Nothing Then Me.Title = "" Else Me.Title = MsgBoxControl.Title
        End Sub
        Private Sub MessageBoxWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
            If (MsgBoxControl.MessageBox.Options And IndependentT.MessageBox.MessageBoxOptions.BringToFront) = IndependentT.MessageBox.MessageBoxOptions.BringToFront Then
                Me.Focus()
            End If
        End Sub
        ''' <summary>Raises the <see cref="E:System.Windows.Window.Closing" /> event.</summary>
        ''' <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
            If Not MsgBoxControl.MessageBox.AllowClose Then
                e.Cancel = True
                Exit Sub
            End If
            MyBase.OnClosing(e)
        End Sub

        Private Sub Copy_Executed(ByVal sender As System.Object, ByVal e As System.Windows.Input.ExecutedRoutedEventArgs)
            My.Computer.Clipboard.SetText(MsgBoxControl.GetCopyText, Windows.Forms.TextDataFormat.UnicodeText)
        End Sub

        Private Sub Copy_CanExecute(ByVal sender As System.Object, ByVal e As System.Windows.Input.CanExecuteRoutedEventArgs)
            e.CanExecute = True
        End Sub
    End Class
End Namespace
#End If