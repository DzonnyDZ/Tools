Imports System.Linq
'#If Config <= RC Then Stage conditional compilation of this file is set in Tests.vbproj
''' <summary>Testing <see cref="TimeSpanFormattable"/></summary>
Public Class frmTimeSpanFormattable
    Private Sub cmdFormat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFormat.Click
        Try
            lblRes.Text = New Tools.TimeSpanFormattable(txtDays.Text, txtHours.Text, txtMinutes.Text, txtSeconds.Text, txtMilliseconds.Text).ToString(txtFormat.Text)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
        End Try
    End Sub

    Private Sub cmdTicks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTicks.Click
        Try
            lblTicksRes.Text = New Tools.TimeSpanFormattable(CLng(txtTicks.Text)).ToString(txtTicksFormat.Text)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
        End Try
    End Sub
    ''' <summary>CTor</summary>
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = Tools.ResourcesT.ToolsIcon

        cmsFormats.Items.AddRange( _
            (From item In TimeSpanFormattable.PredefinedFormats Select _
            DirectCast(New ToolStripMenuItem(item.Key & " " & vbTab & " " & item.Value) With {.Tag = item}, ToolStripItem)) _
            .ToArray)
    End Sub
    ''' <summary>Runs test</summary>
    Public Shared Sub Test()
        Dim frm As New frmTimeSpanFormattable
        frm.Show()
    End Sub

    Private Sub cmsFormats_ItemClicked(ByVal sender As ContextMenuStrip, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles cmsFormats.ItemClicked
        'Static clicks As New Dictionary(Of ToolStripItem, Boolean)
        'If clicks.ContainsKey(e.ClickedItem) Then clicks(e.ClickedItem) = Not clicks(e.ClickedItem) _
        'Else clicks.Add(e.ClickedItem, False)
        Dim item As KeyValuePair(Of Char, String) = e.ClickedItem.Tag
        txtFormat.Text = If(My.Computer.Keyboard.ShiftKeyDown, item.Value, item.Key)
    End Sub

    Private Sub cmdDropDown_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdDropDown.Click
        cmsFormats.Show(sender, 0, sender.height)
    End Sub
End Class
