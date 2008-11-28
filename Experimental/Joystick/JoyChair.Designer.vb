Namespace DevicesT.JoystickT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class JoyChair
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JoyChair))
            Me.stsStatus = New System.Windows.Forms.StatusStrip
            Me.tlbIDI = New System.Windows.Forms.ToolStripStatusLabel
            Me.tlbID = New System.Windows.Forms.ToolStripStatusLabel
            Me.tosJoy = New System.Windows.Forms.ToolStrip
            Me.tslJoy = New System.Windows.Forms.ToolStripLabel
            Me.tscJoy = New System.Windows.Forms.ToolStripComboBox
            Me.tsbJoyRefresh = New System.Windows.Forms.ToolStripButton
            Me.panJoy = New System.Windows.Forms.Panel
            Me.panAxes = New System.Windows.Forms.Panel
            Me.flpAxes = New System.Windows.Forms.FlowLayoutPanel
            Me.tosButtons = New System.Windows.Forms.ToolStrip
            Me.tmr10ms = New System.Windows.Forms.Timer(Me.components)
            Me.cmsCopy = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.tmiCopy = New System.Windows.Forms.ToolStripMenuItem
            Me.stsStatus.SuspendLayout()
            Me.tosJoy.SuspendLayout()
            Me.panJoy.SuspendLayout()
            Me.panAxes.SuspendLayout()
            Me.cmsCopy.SuspendLayout()
            Me.SuspendLayout()
            '
            'stsStatus
            '
            Me.stsStatus.AccessibleDescription = Nothing
            Me.stsStatus.AccessibleName = Nothing
            resources.ApplyResources(Me.stsStatus, "stsStatus")
            Me.stsStatus.BackgroundImage = Nothing
            Me.stsStatus.Font = Nothing
            Me.stsStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbIDI, Me.tlbID})
            Me.stsStatus.Name = "stsStatus"
            Me.stsStatus.SizingGrip = False
            '
            'tlbIDI
            '
            Me.tlbIDI.AccessibleDescription = Nothing
            Me.tlbIDI.AccessibleName = Nothing
            resources.ApplyResources(Me.tlbIDI, "tlbIDI")
            Me.tlbIDI.BackgroundImage = Nothing
            Me.tlbIDI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.tlbIDI.Name = "tlbIDI"
            '
            'tlbID
            '
            Me.tlbID.AccessibleDescription = Nothing
            Me.tlbID.AccessibleName = Nothing
            resources.ApplyResources(Me.tlbID, "tlbID")
            Me.tlbID.BackgroundImage = Nothing
            Me.tlbID.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.tlbID.Name = "tlbID"
            '
            'tosJoy
            '
            Me.tosJoy.AccessibleDescription = Nothing
            Me.tosJoy.AccessibleName = Nothing
            resources.ApplyResources(Me.tosJoy, "tosJoy")
            Me.tosJoy.BackgroundImage = Nothing
            Me.tosJoy.Font = Nothing
            Me.tosJoy.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.tosJoy.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslJoy, Me.tscJoy, Me.tsbJoyRefresh})
            Me.tosJoy.Name = "tosJoy"
            '
            'tslJoy
            '
            Me.tslJoy.AccessibleDescription = Nothing
            Me.tslJoy.AccessibleName = Nothing
            resources.ApplyResources(Me.tslJoy, "tslJoy")
            Me.tslJoy.BackgroundImage = Nothing
            Me.tslJoy.Name = "tslJoy"
            '
            'tscJoy
            '
            Me.tscJoy.AccessibleDescription = Nothing
            Me.tscJoy.AccessibleName = Nothing
            resources.ApplyResources(Me.tscJoy, "tscJoy")
            Me.tscJoy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.tscJoy.DropDownWidth = 300
            Me.tscJoy.Name = "tscJoy"
            '
            'tsbJoyRefresh
            '
            Me.tsbJoyRefresh.AccessibleDescription = Nothing
            Me.tsbJoyRefresh.AccessibleName = Nothing
            resources.ApplyResources(Me.tsbJoyRefresh, "tsbJoyRefresh")
            Me.tsbJoyRefresh.BackgroundImage = Nothing
            Me.tsbJoyRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbJoyRefresh.Image = My.Resources.Resources.RefreshDocViewHS
            Me.tsbJoyRefresh.Name = "tsbJoyRefresh"
            '
            'panJoy
            '
            Me.panJoy.AccessibleDescription = Nothing
            Me.panJoy.AccessibleName = Nothing
            resources.ApplyResources(Me.panJoy, "panJoy")
            Me.panJoy.BackgroundImage = Nothing
            Me.panJoy.Controls.Add(Me.panAxes)
            Me.panJoy.Controls.Add(Me.tosButtons)
            Me.panJoy.Font = Nothing
            Me.panJoy.Name = "panJoy"
            '
            'panAxes
            '
            Me.panAxes.AccessibleDescription = Nothing
            Me.panAxes.AccessibleName = Nothing
            resources.ApplyResources(Me.panAxes, "panAxes")
            Me.panAxes.BackgroundImage = Nothing
            Me.panAxes.Controls.Add(Me.flpAxes)
            Me.panAxes.Font = Nothing
            Me.panAxes.Name = "panAxes"
            '
            'flpAxes
            '
            Me.flpAxes.AccessibleDescription = Nothing
            Me.flpAxes.AccessibleName = Nothing
            resources.ApplyResources(Me.flpAxes, "flpAxes")
            Me.flpAxes.BackgroundImage = Nothing
            Me.flpAxes.Font = Nothing
            Me.flpAxes.Name = "flpAxes"
            '
            'tosButtons
            '
            Me.tosButtons.AccessibleDescription = Nothing
            Me.tosButtons.AccessibleName = Nothing
            resources.ApplyResources(Me.tosButtons, "tosButtons")
            Me.tosButtons.BackgroundImage = Nothing
            Me.tosButtons.Font = Nothing
            Me.tosButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.tosButtons.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
            Me.tosButtons.Name = "tosButtons"
            Me.tosButtons.ShowItemToolTips = False
            '
            'tmr10ms
            '
            Me.tmr10ms.Enabled = True
            Me.tmr10ms.Interval = 10
            '
            'cmsCopy
            '
            Me.cmsCopy.AccessibleDescription = Nothing
            Me.cmsCopy.AccessibleName = Nothing
            resources.ApplyResources(Me.cmsCopy, "cmsCopy")
            Me.cmsCopy.BackgroundImage = Nothing
            Me.cmsCopy.Font = Nothing
            Me.cmsCopy.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiCopy})
            Me.cmsCopy.Name = "cmsCopy"
            Me.cmsCopy.ShowImageMargin = False
            '
            'tmiCopy
            '
            Me.tmiCopy.AccessibleDescription = Nothing
            Me.tmiCopy.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiCopy, "tmiCopy")
            Me.tmiCopy.BackgroundImage = Nothing
            Me.tmiCopy.Name = "tmiCopy"
            Me.tmiCopy.ShortcutKeyDisplayString = Nothing
            '
            'JoyKřeslo
            '
            Me.AccessibleDescription = Nothing
            Me.AccessibleName = Nothing
            resources.ApplyResources(Me, "$this")
            Me.BackgroundImage = Nothing
            Me.Controls.Add(Me.panJoy)
            Me.Controls.Add(Me.tosJoy)
            Me.Controls.Add(Me.stsStatus)
            Me.Font = Nothing
            Me.Name = "JoyKřeslo"
            Me.stsStatus.ResumeLayout(False)
            Me.stsStatus.PerformLayout()
            Me.tosJoy.ResumeLayout(False)
            Me.tosJoy.PerformLayout()
            Me.panJoy.ResumeLayout(False)
            Me.panJoy.PerformLayout()
            Me.panAxes.ResumeLayout(False)
            Me.panAxes.PerformLayout()
            Me.cmsCopy.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Protected Friend WithEvents stsStatus As System.Windows.Forms.StatusStrip
        Protected Friend WithEvents tlbIDI As System.Windows.Forms.ToolStripStatusLabel
        Protected Friend WithEvents tlbID As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents tosJoy As System.Windows.Forms.ToolStrip
        Friend WithEvents tslJoy As System.Windows.Forms.ToolStripLabel
        Friend WithEvents tscJoy As System.Windows.Forms.ToolStripComboBox
        Friend WithEvents tsbJoyRefresh As System.Windows.Forms.ToolStripButton
        Friend WithEvents panJoy As System.Windows.Forms.Panel
        Friend WithEvents tosButtons As System.Windows.Forms.ToolStrip
        Friend WithEvents flpAxes As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents panAxes As System.Windows.Forms.Panel
        Friend WithEvents tmr10ms As System.Windows.Forms.Timer
        Protected Friend WithEvents cmsCopy As System.Windows.Forms.ContextMenuStrip
        Protected Friend WithEvents tmiCopy As System.Windows.Forms.ToolStripMenuItem

    End Class
End Namespace