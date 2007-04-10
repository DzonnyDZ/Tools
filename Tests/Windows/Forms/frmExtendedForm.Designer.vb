Namespace Windows.Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmExtendedForm
        Inherits Tools.Windows.Forms.ExtendedForm

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.flpMain = New System.Windows.Forms.FlowLayoutPanel
            Me.lblItem = New System.Windows.Forms.Label
            Me.cmbItem = New System.Windows.Forms.ComboBox
            Me.lblState = New System.Windows.Forms.Label
            Me.cmbState = New System.Windows.Forms.ComboBox
            Me.cmdApply = New System.Windows.Forms.Button
            Me.cmdGet = New System.Windows.Forms.Button
            Me.pgrProperty = New System.Windows.Forms.PropertyGrid
            Me.flpMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'flpMain
            '
            Me.flpMain.AutoSize = True
            Me.flpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpMain.Controls.Add(Me.lblItem)
            Me.flpMain.Controls.Add(Me.cmbItem)
            Me.flpMain.Controls.Add(Me.lblState)
            Me.flpMain.Controls.Add(Me.cmbState)
            Me.flpMain.Controls.Add(Me.cmdApply)
            Me.flpMain.Controls.Add(Me.cmdGet)
            Me.flpMain.Dock = System.Windows.Forms.DockStyle.Top
            Me.flpMain.Location = New System.Drawing.Point(0, 0)
            Me.flpMain.Name = "flpMain"
            Me.flpMain.Size = New System.Drawing.Size(442, 29)
            Me.flpMain.TabIndex = 0
            '
            'lblItem
            '
            Me.lblItem.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblItem.AutoSize = True
            Me.lblItem.Location = New System.Drawing.Point(3, 8)
            Me.lblItem.Name = "lblItem"
            Me.lblItem.Size = New System.Drawing.Size(27, 13)
            Me.lblItem.TabIndex = 0
            Me.lblItem.Text = "Item"
            '
            'cmbItem
            '
            Me.cmbItem.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.cmbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbItem.FormattingEnabled = True
            Me.cmbItem.Location = New System.Drawing.Point(36, 4)
            Me.cmbItem.Name = "cmbItem"
            Me.cmbItem.Size = New System.Drawing.Size(121, 21)
            Me.cmbItem.TabIndex = 1
            '
            'lblState
            '
            Me.lblState.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblState.AutoSize = True
            Me.lblState.Location = New System.Drawing.Point(163, 8)
            Me.lblState.Name = "lblState"
            Me.lblState.Size = New System.Drawing.Size(32, 13)
            Me.lblState.TabIndex = 2
            Me.lblState.Text = "State"
            '
            'cmbState
            '
            Me.cmbState.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbState.FormattingEnabled = True
            Me.cmbState.Location = New System.Drawing.Point(201, 4)
            Me.cmbState.Name = "cmbState"
            Me.cmbState.Size = New System.Drawing.Size(121, 21)
            Me.cmbState.TabIndex = 3
            '
            'cmdApply
            '
            Me.cmdApply.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.cmdApply.AutoSize = True
            Me.cmdApply.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdApply.Location = New System.Drawing.Point(328, 3)
            Me.cmdApply.Name = "cmdApply"
            Me.cmdApply.Size = New System.Drawing.Size(43, 23)
            Me.cmdApply.TabIndex = 4
            Me.cmdApply.Text = "&Apply"
            Me.cmdApply.UseVisualStyleBackColor = True
            '
            'cmdGet
            '
            Me.cmdGet.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.cmdGet.AutoSize = True
            Me.cmdGet.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpMain.SetFlowBreak(Me.cmdGet, True)
            Me.cmdGet.Location = New System.Drawing.Point(377, 3)
            Me.cmdGet.Name = "cmdGet"
            Me.cmdGet.Size = New System.Drawing.Size(34, 23)
            Me.cmdGet.TabIndex = 5
            Me.cmdGet.Text = "&Get"
            Me.cmdGet.UseVisualStyleBackColor = True
            '
            'pgrProperty
            '
            Me.pgrProperty.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pgrProperty.Location = New System.Drawing.Point(0, 29)
            Me.pgrProperty.Name = "pgrProperty"
            Me.pgrProperty.SelectedObject = Me
            Me.pgrProperty.Size = New System.Drawing.Size(442, 316)
            Me.pgrProperty.TabIndex = 6
            '
            'frmExtendedForm
            '
            Me.ClientSize = New System.Drawing.Size(442, 345)
            Me.Controls.Add(Me.pgrProperty)
            Me.Controls.Add(Me.flpMain)
            Me.Location = New System.Drawing.Point(0, 0)
            Me.Name = "frmExtendedForm"
            Me.Text = "Testing Tools.Windows.Forms.ExtendedForm"
            Me.flpMain.ResumeLayout(False)
            Me.flpMain.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents flpMain As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents lblItem As System.Windows.Forms.Label
        Friend WithEvents cmbItem As System.Windows.Forms.ComboBox
        Friend WithEvents lblState As System.Windows.Forms.Label
        Friend WithEvents cmbState As System.Windows.Forms.ComboBox
        Friend WithEvents cmdApply As System.Windows.Forms.Button
        Friend WithEvents cmdGet As System.Windows.Forms.Button
        Friend WithEvents pgrProperty As System.Windows.Forms.PropertyGrid

    End Class
End Namespace