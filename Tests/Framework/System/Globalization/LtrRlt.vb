Imports System.Reflection
Namespace Framework.SystemF.GlobalizationF
    Public Class frmLtrRtl : Inherits Form
        Shared Sub Test()
            Dim frm As New frmLtrRtl
            frm.ShowDialog()
        End Sub
        Private Sub cmdMsgLtR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMsgLtR.Click
            MsgBox("This is prompt", MsgBoxStyle.Exclamation, "Left aligned LtR")
            MsgBox("This is prompt", MsgBoxStyle.Exclamation Or MsgBoxStyle.MsgBoxRight, "Right aligned LtR")
        End Sub

        Private Sub cmdMsgRtL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMsgRtL.Click
            MsgBox("This is prompt", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.Exclamation, "Left aligned RtL")
            MsgBox("This is prompt", MsgBoxStyle.MsgBoxRtlReading Or MsgBoxStyle.Exclamation Or MsgBoxStyle.MsgBoxRight, "Right aligner RtL")
        End Sub


        Private Sub frmLtrRtl_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
            If e.CloseReason = CloseReason.None Then e.Cancel = True
        End Sub

        Private Sub frmLtrRtl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            chkRightToLeftLayout.Checked = Me.RightToLeftLayout
            Select Case Me.RightToLeft
                Case Windows.Forms.RightToLeft.Yes : chkRightToLeft.CheckState = CheckState.Checked
                Case Windows.Forms.RightToLeft.No : chkRightToLeft.CheckState = CheckState.Unchecked
                Case Windows.Forms.RightToLeft.Inherit : chkRightToLeft.CheckState = CheckState.Indeterminate
            End Select
            Loaded = True
        End Sub
        Dim Loaded As Boolean = False
        Private Sub chkRightToLeft_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRightToLeft.CheckedChanged
            If Not Loaded Then Exit Sub
            Select Case chkRightToLeft.CheckState
                Case CheckState.Checked : Me.RightToLeft = Windows.Forms.RightToLeft.Yes
                Case CheckState.Indeterminate : Me.RightToLeft = Windows.Forms.RightToLeft.Inherit
                Case CheckState.Unchecked : Me.RightToLeft = Windows.Forms.RightToLeft.No
            End Select
        End Sub

        Private Sub chkRightToLeftLayout_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRightToLeftLayout.CheckedChanged
            If Not Loaded Then Exit Sub
            Me.RightToLeftLayout = chkRightToLeftLayout.Checked
        End Sub

    End Class
End Namespace