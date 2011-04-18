<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CryptTestForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CryptTestForm))
        Me.lblI = New System.Windows.Forms.Label()
        Me.panI = New System.Windows.Forms.Panel()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblConnection = New System.Windows.Forms.Label()
        Me.txtConnection = New System.Windows.Forms.TextBox()
        Me.cmdGet = New System.Windows.Forms.Button()
        Me.cmdGetNoUI = New System.Windows.Forms.Button()
        Me.cmdDel = New System.Windows.Forms.Button()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.lblOther = New System.Windows.Forms.Label()
        Me.txtOther = New System.Windows.Forms.TextBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdCopy = New System.Windows.Forms.Button()
        Me.cmdMove = New System.Windows.Forms.Button()
        Me.totTooltip = New System.Windows.Forms.ToolTip(Me.components)
        Me.panI.SuspendLayout()
        Me.tlpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblI
        '
        Me.lblI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblI.Location = New System.Drawing.Point(0, 0)
        Me.lblI.Name = "lblI"
        Me.lblI.Size = New System.Drawing.Size(597, 38)
        Me.lblI.TabIndex = 0
        Me.lblI.Text = resources.GetString("lblI.Text")
        '
        'panI
        '
        Me.panI.Controls.Add(Me.lblI)
        Me.panI.Dock = System.Windows.Forms.DockStyle.Top
        Me.panI.Location = New System.Drawing.Point(0, 0)
        Me.panI.Name = "panI"
        Me.panI.Size = New System.Drawing.Size(597, 38)
        Me.panI.TabIndex = 1
        '
        'tlpMain
        '
        Me.tlpMain.AutoSize = True
        Me.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpMain.ColumnCount = 4
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpMain.Controls.Add(Me.lblConnection, 0, 0)
        Me.tlpMain.Controls.Add(Me.txtConnection, 0, 1)
        Me.tlpMain.Controls.Add(Me.cmdGet, 1, 1)
        Me.tlpMain.Controls.Add(Me.cmdGetNoUI, 1, 2)
        Me.tlpMain.Controls.Add(Me.cmdDel, 1, 3)
        Me.tlpMain.Controls.Add(Me.lblPassword, 2, 0)
        Me.tlpMain.Controls.Add(Me.txtPassword, 2, 1)
        Me.tlpMain.Controls.Add(Me.cmdSave, 3, 1)
        Me.tlpMain.Controls.Add(Me.lblOther, 2, 2)
        Me.tlpMain.Controls.Add(Me.txtOther, 2, 3)
        Me.tlpMain.Controls.Add(Me.cmdClose, 0, 5)
        Me.tlpMain.Controls.Add(Me.cmdCopy, 3, 4)
        Me.tlpMain.Controls.Add(Me.cmdMove, 3, 3)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 38)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 6
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(597, 188)
        Me.tlpMain.TabIndex = 0
        '
        'lblConnection
        '
        Me.lblConnection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblConnection.AutoSize = True
        Me.lblConnection.Location = New System.Drawing.Point(3, 0)
        Me.lblConnection.Name = "lblConnection"
        Me.lblConnection.Size = New System.Drawing.Size(92, 13)
        Me.lblConnection.TabIndex = 0
        Me.lblConnection.Text = "Connection Name"
        Me.totTooltip.SetToolTip(Me.lblConnection, "Name of primary connection to work with")
        '
        'txtConnection
        '
        Me.txtConnection.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtConnection.Location = New System.Drawing.Point(3, 13)
        Me.txtConnection.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.txtConnection.Name = "txtConnection"
        Me.txtConnection.Size = New System.Drawing.Size(183, 20)
        Me.txtConnection.TabIndex = 1
        Me.totTooltip.SetToolTip(Me.txtConnection, "Name of primary connection to work with")
        '
        'cmdGet
        '
        Me.cmdGet.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdGet.AutoSize = True
        Me.cmdGet.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdGet.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdGet.Location = New System.Drawing.Point(197, 13)
        Me.cmdGet.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdGet.Name = "cmdGet"
        Me.cmdGet.Size = New System.Drawing.Size(105, 23)
        Me.cmdGet.TabIndex = 2
        Me.cmdGet.Text = "Retrieve password"
        Me.totTooltip.SetToolTip(Me.cmdGet, "Retrieves password for current connection from store (asks to main password if ne" & _
                "cessary)")
        Me.cmdGet.UseVisualStyleBackColor = True
        '
        'cmdGetNoUI
        '
        Me.cmdGetNoUI.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdGetNoUI.AutoSize = True
        Me.cmdGetNoUI.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdGetNoUI.Location = New System.Drawing.Point(192, 36)
        Me.cmdGetNoUI.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdGetNoUI.Name = "cmdGetNoUI"
        Me.cmdGetNoUI.Size = New System.Drawing.Size(115, 23)
        Me.cmdGetNoUI.TabIndex = 3
        Me.cmdGetNoUI.Text = "Retireve pwd (no UI)"
        Me.totTooltip.SetToolTip(Me.cmdGetNoUI, "Retrieves password for current connection from store (fails if main password was " & _
                "not entered previously)")
        Me.cmdGetNoUI.UseVisualStyleBackColor = True
        '
        'cmdDel
        '
        Me.cmdDel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdDel.AutoSize = True
        Me.cmdDel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdDel.Location = New System.Drawing.Point(201, 59)
        Me.cmdDel.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdDel.Name = "cmdDel"
        Me.cmdDel.Size = New System.Drawing.Size(96, 23)
        Me.cmdDel.TabIndex = 4
        Me.cmdDel.Text = "Delete password"
        Me.totTooltip.SetToolTip(Me.cmdDel, "Deletes password of current connection from store")
        Me.cmdDel.UseVisualStyleBackColor = True
        '
        'lblPassword
        '
        Me.lblPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(313, 0)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(53, 13)
        Me.lblPassword.TabIndex = 5
        Me.lblPassword.Text = "Password"
        Me.totTooltip.SetToolTip(Me.lblPassword, "Enter password to save")
        '
        'txtPassword
        '
        Me.txtPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPassword.Location = New System.Drawing.Point(313, 13)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(183, 20)
        Me.txtPassword.TabIndex = 6
        Me.totTooltip.SetToolTip(Me.txtPassword, "Enter password to save")
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdSave.AutoSize = True
        Me.cmdSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdSave.Location = New System.Drawing.Point(503, 13)
        Me.cmdSave.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(90, 23)
        Me.cmdSave.TabIndex = 7
        Me.cmdSave.Text = "Save password"
        Me.totTooltip.SetToolTip(Me.cmdSave, "Saves a new password for current connection to store")
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'lblOther
        '
        Me.lblOther.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOther.AutoSize = True
        Me.lblOther.Location = New System.Drawing.Point(313, 46)
        Me.lblOther.Name = "lblOther"
        Me.lblOther.Size = New System.Drawing.Size(89, 13)
        Me.lblOther.TabIndex = 8
        Me.lblOther.Text = "Other connection"
        Me.totTooltip.SetToolTip(Me.lblOther, "Name of secondary connection to work with" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(connection to copy password to)")
        '
        'txtOther
        '
        Me.txtOther.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOther.Location = New System.Drawing.Point(313, 59)
        Me.txtOther.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.txtOther.Name = "txtOther"
        Me.txtOther.Size = New System.Drawing.Size(183, 20)
        Me.txtOther.TabIndex = 9
        Me.totTooltip.SetToolTip(Me.txtOther, "Name of secondary connection to work with" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(connection to copy password to)")
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdClose.AutoSize = True
        Me.cmdClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpMain.SetColumnSpan(Me.cmdClose, 4)
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Location = New System.Drawing.Point(277, 135)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(43, 23)
        Me.cmdClose.TabIndex = 12
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdCopy
        '
        Me.cmdCopy.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdCopy.AutoSize = True
        Me.cmdCopy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdCopy.Location = New System.Drawing.Point(503, 82)
        Me.cmdCopy.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdCopy.Name = "cmdCopy"
        Me.cmdCopy.Size = New System.Drawing.Size(89, 23)
        Me.cmdCopy.TabIndex = 11
        Me.cmdCopy.Text = "Copy password"
        Me.totTooltip.SetToolTip(Me.cmdCopy, "Copies password in store from primary connection to the other (duplicates it)")
        Me.cmdCopy.UseVisualStyleBackColor = True
        '
        'cmdMove
        '
        Me.cmdMove.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdMove.AutoSize = True
        Me.cmdMove.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdMove.Location = New System.Drawing.Point(502, 59)
        Me.cmdMove.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdMove.Name = "cmdMove"
        Me.cmdMove.Size = New System.Drawing.Size(92, 23)
        Me.cmdMove.TabIndex = 10
        Me.cmdMove.Text = "Move password"
        Me.totTooltip.SetToolTip(Me.cmdMove, "Copies password in store from primary connection to the other and removes it from" & _
                " the primary one")
        Me.cmdMove.UseVisualStyleBackColor = True
        '
        'CryptTestForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(597, 226)
        Me.Controls.Add(Me.tlpMain)
        Me.Controls.Add(Me.panI)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CryptTestForm"
        Me.ShowInTaskbar = False
        Me.Text = "Crypt test"
        Me.panI.ResumeLayout(False)
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblI As System.Windows.Forms.Label
    Friend WithEvents panI As System.Windows.Forms.Panel
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtConnection As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents lblConnection As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents cmdGet As System.Windows.Forms.Button
    Friend WithEvents cmdGetNoUI As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdDel As System.Windows.Forms.Button
    Friend WithEvents totTooltip As System.Windows.Forms.ToolTip
    Friend WithEvents lblOther As System.Windows.Forms.Label
    Friend WithEvents txtOther As System.Windows.Forms.TextBox
    Friend WithEvents cmdMove As System.Windows.Forms.Button
    Friend WithEvents cmdCopy As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
End Class
