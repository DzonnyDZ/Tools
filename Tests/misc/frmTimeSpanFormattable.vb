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
    End Sub
    ''' <summary>Runs test</summary>
    Public Shared Sub Test()
        Dim frm As New frmTimeSpanFormattable
        frm.Show()
    End Sub
End Class
