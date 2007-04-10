Namespace Windows.Forms
    Public Class frmExtendedForm
        Public Shared Sub Test()
            Dim ef As New frmExtendedForm
            ef.ShowDialog()
        End Sub

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            cmbItem.Items.Add("Close")
            cmbItem.Items.Add("Move")
            cmbItem.Items.Add("Maximize")
            cmbItem.Items.Add("Minimize")
            cmbItem.Items.Add("Size")
            cmbItem.Items.Add("Restore")
            cmbItem.SelectedIndex = 0
            cmbState.Items.Add("Enabled")
            cmbState.Items.Add("Disabled")
            cmbState.Items.Add("Grayed")
            cmbState.SelectedIndex = 0
            Me.Icon = Tools.Resources.ToolsIcon
        End Sub

        Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
            Dim ItemEnType As Type = GetType(SystemMenuItem)
            Dim SelectedItemIndex As Integer = Array.IndexOf([Enum].GetNames(ItemEnType), cmbItem.SelectedItem)
            Dim SelectedItem As SystemMenuItem = [Enum].GetValues(ItemEnType).GetValue(SelectedItemIndex)
            Dim ValueEnType As Type = GetType(SystemMenuState)
            Dim SelectedValueIndex As Integer = Array.IndexOf([Enum].GetNames(ValueEnType), cmbState.SelectedItem)
            Dim SelectedValue As SystemMenuState = [Enum].GetValues(ValueEnType).GetValue(SelectedValueIndex)
            Me.SystemMenuItemEnabled(SelectedItem) = SelectedValue
        End Sub

        Private Sub cmdGet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGet.Click
            Dim ItemEnType As Type = GetType(SystemMenuItem)
            Dim SelectedItemIndex As Integer = Array.IndexOf([Enum].GetNames(ItemEnType), cmbItem.SelectedItem)
            Dim SelectedItem As SystemMenuItem = [Enum].GetValues(ItemEnType).GetValue(SelectedItemIndex)
            MsgBox([Enum].GetName(GetType(SystemMenuState), Me.SystemMenuItemEnabled(SelectedItem)), , cmbItem.SelectedItem)
        End Sub
    End Class
End Namespace