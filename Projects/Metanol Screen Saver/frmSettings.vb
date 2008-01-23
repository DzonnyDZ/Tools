Imports System.Xml.Serialization

Friend Class frmSettings
    Private WithEvents frmMonitors As frmMonitors
    Private ScreenSettingss As New List(Of ScreenSettings)
    Private Sub tsbShowMonitors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbShowMonitors.Click
        If frmMonitors Is Nothing Then frmMonitors = New frmMonitors
        frmMonitors.Visible = False
        frmMonitors.Show(Me)
    End Sub

    Private Sub frmMonitors_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frmMonitors.FormClosed
        frmMonitors = Nothing
    End Sub

    Private Sub tmiAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiAbout.Click
        Tools.WindowsT.FormsT.AboutDialog.ShowModalDialog(Me)
    End Sub

    Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If My.Settings.Screens Is Nothing OrElse My.Settings.Screens.Count = 0 Then
            My.Settings.Screens = New List(Of Setting_Monitor)(New Setting_Monitor() {New Setting_Monitor()})
        End If
        scsScreen1.Settings = My.Settings.Screens(0)
        ScreenSettingss.Add(scsScreen1)
        For i = 1 To My.Settings.Screens.Count - 1
            tabScreens.TabPages.Add(i + 1)
            Dim sts As New ScreenSettings
            sts.Dock = DockStyle.Fill
            ScreenSettingss.Add(sts)
            tabScreens.TabPages(tabScreens.TabPages.Count - 1).Controls.Add(sts)
            sts.Settings = My.Settings.Screens(i)
        Next i
        tsbRemove.Enabled = ScreenSettingss.Count > 1
    End Sub


    Private Sub tsbCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub New()
        InitializeComponent()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub tsbAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbAdd.Click
        tabScreens.TabPages.Add(tabScreens.TabPages.Count + 1)
        Dim sts As New ScreenSettings
        sts.Dock = DockStyle.Fill
        ScreenSettingss.Add(sts)
        tabScreens.TabPages(tabScreens.TabPages.Count - 1).Controls.Add(sts)
        sts.Settings = ScreenSettingss(ScreenSettingss.Count - 2).Settings
    End Sub

    Private Sub tsbRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbRemove.Click
        If ScreenSettingss.Count > 1 Then
            ScreenSettingss.RemoveAt(ScreenSettingss.Count - 1)
            tabScreens.TabPages.RemoveAt(tabScreens.TabPages.Count - 1)
        End If
        tsbRemove.Enabled = ScreenSettingss.Count > 1
    End Sub

    Private Sub tsbOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbOK.Click
        Dim Sts As New List(Of Setting_Monitor)
        For Each Stgs In ScreenSettingss
            Sts.Add(Stgs.Settings)
        Next Stgs
        My.Settings.Screens = Sts
        My.Settings.Test = New AnyClass
        My.Settings.Test.A1 = 208
        My.Settings.Test.A2 = "Ahoj"
        Dim s As New XmlSerializer(GetType(AnyClass))
        Dim sb As New System.Text.StringBuilder()
        Dim w As New IO.StringWriter(sb)
        s.Serialize(w, My.Settings.Test)
        Debug.Print(sb.ToString)

        My.Settings.Save()
        Me.Close()
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class