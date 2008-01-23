Imports System.ComponentModel

Partial Friend Class ScreenSettings
    Private ReadOnly Property lsg(ByVal Position As Drawing.ContentAlignment) As LabelSettings
        Get
            Select Case Position
                Case ContentAlignment.BottomCenter : Return lsgBC
                Case ContentAlignment.BottomLeft : Return lsgBL
                Case ContentAlignment.BottomRight : Return lsgBR
                Case ContentAlignment.MiddleCenter : Return lsgMC
                Case ContentAlignment.MiddleLeft : Return lsgML
                Case ContentAlignment.MiddleRight : Return lsgMR
                Case ContentAlignment.TopCenter : Return lsgTC
                Case ContentAlignment.TopLeft : Return lsgTL
                Case ContentAlignment.TopRight : Return lsgTR
                Case Else : Throw New InvalidEnumArgumentException("Position", Position, GetType(Drawing.ContentAlignment))
            End Select
        End Get
    End Property
    Public Property Settings() As Setting_Monitor.Structure
        Get
            Dim ms As New Setting_Monitor.Structure
            ms.Alghoritm = If(optARand.Checked, SSAverAlghoritm.Random, SSAverAlghoritm.Sequintial)
            ms.BgColor = cmdBgColor.BackColor
            ms.FileMask = txtFileMask.Text
            ms.FolderMask = txtFolderMask.Text
            ms.Interval = nudInterval.Value
            ms.RandomizeInterval = nudInterval.Value
            ms.Root = txtRoot.Text
            For Each align As Drawing.ContentAlignment In [Enum].GetValues(GetType(Drawing.ContentAlignment))
                ms.Labels(align) = lsg(align).Settings
            Next align
            Return ms
        End Get
        Set(ByVal value As Setting_Monitor.Structure)
            optARand.Checked = value.Alghoritm = SSAverAlghoritm.Random
            optASeq.Checked = value.Alghoritm = SSAverAlghoritm.Sequintial
            cmdBgColor.BackColor = value.BgColor
            txtFileMask.Text = value.FileMask
            txtFolderMask.Text = value.FolderMask
            nudInterval.Value = value.Interval
            nudIntRand.Value = value.RandomizeInterval
            txtRoot.Text = value.Root
            For Each align As Drawing.ContentAlignment In [Enum].GetValues(GetType(Drawing.ContentAlignment))
                lsg(align).Settings = value.Labels(align)
            Next align
        End Set
    End Property
    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        tlpMain.Width = Me.ClientSize.Width
        tlpMain.Height = Me.ClientSize.Height
        MyBase.OnResize(e)
    End Sub
    Private Sub cmdColor_BackColorChanged(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdBgColor.BackColorChanged
        sender.ForeColor = Color.FromArgb(Not sender.BackColor.R, Not sender.BackColor.G, Not sender.BackColor.B)
    End Sub

    Private Sub cmdColor_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdBgColor.Click
        cdlColor.Color = sender.BackColor
        If cdlColor.ShowDialog = DialogResult.OK Then
            sender.BackColor = cdlColor.Color
        End If
    End Sub

    Private Sub cmdRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRoot.Click
        fbdRoot.SelectedPath = txtRoot.Text
        If fbdRoot.ShowDialog = DialogResult.OK Then
            txtRoot.Text = fbdRoot.SelectedPath
        End If
    End Sub
End Class
