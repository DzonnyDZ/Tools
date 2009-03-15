Imports System.Windows.Forms
Imports System.Drawing

Namespace GUI
    Public Class TabControlEx
        Inherits TabControl

        Protected Overrides Sub OnSelecting(ByVal e As System.Windows.Forms.TabControlCancelEventArgs)
            e.Cancel = Not e.TabPage.Enabled
            If Not e.Cancel Then MyBase.OnSelecting(e)
        End Sub

#Region "Grafika"

        Protected Overrides Sub OnDrawItem(ByVal e As System.Windows.Forms.DrawItemEventArgs)
            MyBase.OnDrawItem(e)
            Dim page As TabPage = Me.TabPages(e.Index)
            Dim State As VisualStyles.TabItemState = VisualStyles.TabItemState.Normal
            If Not page.Enabled Then State = VisualStyles.TabItemState.Disabled
            If e.Index = Me.SelectedIndex Then State = VisualStyles.TabItemState.Selected
            Dim bnds As Rectangle = e.Bounds
            If VisualStyles.VisualStyleRenderer.IsSupported Then
                Dim r As VisualStyles.VisualStyleRenderer
                If Not page.Enabled Then
                    r = New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Tab.TabItem.Disabled)
                ElseIf e.Index = Me.SelectedIndex Then
                    r = New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Tab.TabItem.Pressed)
                ElseIf e.Bounds.Contains(Me.PointToClient(Control.MousePosition)) Then
                    r = New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Tab.TabItem.Hot)
                Else
                    r = New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Tab.TabItem.Normal)
                End If
                r.DrawBackground(e.Graphics, bnds)
            End If
            Dim brush As SolidBrush
            If page.Enabled Then brush = New SolidBrush(page.ForeColor) Else brush = New SolidBrush(SystemColors.GrayText)
            Dim sf As New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.HotkeyPrefix = Drawing.Text.HotkeyPrefix.Show
            sf.LineAlignment = StringAlignment.Center
            e.Graphics.DrawString(page.Text, page.Font, brush, bnds, sf)
            'If page.Enabled AndAlso e.Index = Me.SelectedIndex AndAlso Me.Focused Then
            '    ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds)
            'End If
        End Sub

        Dim LastHot As Integer = -1

        Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
            If LastHot >= 0 AndAlso LastHot < Me.TabPages.Count Then _
                Me.Invalidate(Me.GetTabRect(LastHot))
            LastHot = -1
            MyBase.OnMouseLeave(e)
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
            Dim OldLastHot As Integer = LastHot
            For i As Integer = 0 To Me.TabPages.Count - 1
                If Me.GetTabRect(i).Contains(e.Location) Then
                    If Not Me.SelectedIndex = i AndAlso Me.TabPages(i).Enabled Then
                        If LastHot <> i Then
                            Me.Invalidate(Me.GetTabRect(i))
                        End If
                        LastHot = i
                    Else
                        LastHot = -1
                    End If
                    If OldLastHot >= 0 AndAlso OldLastHot < Me.TabPages.Count Then _
                        Me.Invalidate(Me.GetTabRect(OldLastHot))
                    Exit Sub
                End If
            Next i
            If LastHot >= 0 AndAlso LastHot < Me.TabPages.Count Then _
                Me.Invalidate(Me.GetTabRect(LastHot))
            LastHot = -1
            MyBase.OnMouseMove(e)
        End Sub
#End Region
    End Class
End Namespace