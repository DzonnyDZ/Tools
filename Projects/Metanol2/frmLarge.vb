''' <summary>Big preview form</summary>
Public Class frmLarge
    '''' <summary>Shows image form given folder</summary>
    'Public Sub LoadImage(ByVal Path As String)
    '    Try
    '        Me.BackgroundImage = New Bitmap(Path)
    '    Catch ex As Exception
    '        Me.BackgroundImage = Nothing
    '    End Try
    'End Sub

    Private Sub frmLarge_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.LargePosition = Me.Location
        My.Settings.LargeSize = Me.Size
        My.Settings.LargeState = Me.WindowState
    End Sub

    Private Sub frmLarge_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = My.Settings.LargePosition
        Me.Size = My.Settings.LargeSize
        Me.WindowState = My.Settings.LargeState
        If Me.Owner IsNot Nothing Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
            Me.MinimizeBox = False
            Me.MaximizeBox = False
            Me.ShowInTaskbar = False
            Me.ShowIcon = False
        End If
    End Sub
End Class