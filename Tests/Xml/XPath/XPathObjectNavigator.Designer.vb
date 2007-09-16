Namespace XmlT.XPathT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmXPathObjectNavigator
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
            Me.lblSource = New System.Windows.Forms.Label
            Me.cmbSource = New System.Windows.Forms.ComboBox
            Me.tlpTop = New System.Windows.Forms.TableLayoutPanel
            Me.lblQuery = New System.Windows.Forms.Label
            Me.txtQuery = New System.Windows.Forms.TextBox
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.lblSourceProp = New System.Windows.Forms.Label
            Me.lblResult = New System.Windows.Forms.Label
            Me.prgSource = New System.Windows.Forms.PropertyGrid
            Me.tvwResult = New System.Windows.Forms.TreeView
            Me.stsResult = New System.Windows.Forms.StatusStrip
            Me.tslResult = New System.Windows.Forms.ToolStripStatusLabel
            Me.cmdDo = New System.Windows.Forms.Button
            Me.cmdWalk = New System.Windows.Forms.Button
            Me.tlpTop.SuspendLayout()
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.stsResult.SuspendLayout()
            Me.SuspendLayout()
            '
            'lblSource
            '
            Me.lblSource.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSource.AutoSize = True
            Me.lblSource.Location = New System.Drawing.Point(3, 8)
            Me.lblSource.Name = "lblSource"
            Me.lblSource.Size = New System.Drawing.Size(41, 13)
            Me.lblSource.TabIndex = 0
            Me.lblSource.Text = "Source"
            '
            'cmbSource
            '
            Me.cmbSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbSource.FormattingEnabled = True
            Me.cmbSource.Location = New System.Drawing.Point(50, 4)
            Me.cmbSource.Name = "cmbSource"
            Me.cmbSource.Size = New System.Drawing.Size(432, 21)
            Me.cmbSource.TabIndex = 1
            '
            'tlpTop
            '
            Me.tlpTop.AutoSize = True
            Me.tlpTop.ColumnCount = 3
            Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpTop.Controls.Add(Me.cmbSource, 1, 0)
            Me.tlpTop.Controls.Add(Me.lblSource, 0, 0)
            Me.tlpTop.Controls.Add(Me.lblQuery, 0, 1)
            Me.tlpTop.Controls.Add(Me.txtQuery, 1, 1)
            Me.tlpTop.Controls.Add(Me.cmdDo, 2, 1)
            Me.tlpTop.Controls.Add(Me.cmdWalk, 2, 0)
            Me.tlpTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.tlpTop.Location = New System.Drawing.Point(0, 0)
            Me.tlpTop.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpTop.Name = "tlpTop"
            Me.tlpTop.RowCount = 2
            Me.tlpTop.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpTop.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpTop.Size = New System.Drawing.Size(533, 58)
            Me.tlpTop.TabIndex = 1
            '
            'lblQuery
            '
            Me.lblQuery.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblQuery.AutoSize = True
            Me.lblQuery.Location = New System.Drawing.Point(9, 37)
            Me.lblQuery.Name = "lblQuery"
            Me.lblQuery.Size = New System.Drawing.Size(35, 13)
            Me.lblQuery.TabIndex = 2
            Me.lblQuery.Text = "Query"
            '
            'txtQuery
            '
            Me.txtQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtQuery.Location = New System.Drawing.Point(50, 33)
            Me.txtQuery.Name = "txtQuery"
            Me.txtQuery.Size = New System.Drawing.Size(432, 20)
            Me.txtQuery.TabIndex = 3
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.Location = New System.Drawing.Point(0, 58)
            Me.splMain.Name = "splMain"
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.prgSource)
            Me.splMain.Panel1.Controls.Add(Me.lblSourceProp)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.stsResult)
            Me.splMain.Panel2.Controls.Add(Me.tvwResult)
            Me.splMain.Panel2.Controls.Add(Me.lblResult)
            Me.splMain.Size = New System.Drawing.Size(533, 397)
            Me.splMain.SplitterDistance = 234
            Me.splMain.TabIndex = 2
            '
            'lblSourceProp
            '
            Me.lblSourceProp.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblSourceProp.Location = New System.Drawing.Point(0, 0)
            Me.lblSourceProp.Name = "lblSourceProp"
            Me.lblSourceProp.Size = New System.Drawing.Size(234, 13)
            Me.lblSourceProp.TabIndex = 0
            Me.lblSourceProp.Text = "Source properties"
            Me.lblSourceProp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblResult
            '
            Me.lblResult.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblResult.Location = New System.Drawing.Point(0, 0)
            Me.lblResult.Name = "lblResult"
            Me.lblResult.Size = New System.Drawing.Size(295, 13)
            Me.lblResult.TabIndex = 0
            Me.lblResult.Text = "Result"
            Me.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'prgSource
            '
            Me.prgSource.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgSource.Location = New System.Drawing.Point(0, 13)
            Me.prgSource.Name = "prgSource"
            Me.prgSource.Size = New System.Drawing.Size(234, 384)
            Me.prgSource.TabIndex = 1
            '
            'tvwResult
            '
            Me.tvwResult.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwResult.Location = New System.Drawing.Point(0, 13)
            Me.tvwResult.Name = "tvwResult"
            Me.tvwResult.Size = New System.Drawing.Size(295, 384)
            Me.tvwResult.TabIndex = 1
            '
            'stsResult
            '
            Me.stsResult.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslResult})
            Me.stsResult.Location = New System.Drawing.Point(0, 375)
            Me.stsResult.Name = "stsResult"
            Me.stsResult.Size = New System.Drawing.Size(295, 22)
            Me.stsResult.TabIndex = 2
            Me.stsResult.Text = "StatusStrip1"
            '
            'tslResult
            '
            Me.tslResult.Name = "tslResult"
            Me.tslResult.Size = New System.Drawing.Size(280, 17)
            Me.tslResult.Spring = True
            '
            'cmdDo
            '
            Me.cmdDo.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdDo.AutoSize = True
            Me.cmdDo.Location = New System.Drawing.Point(493, 32)
            Me.cmdDo.Name = "cmdDo"
            Me.cmdDo.Size = New System.Drawing.Size(31, 23)
            Me.cmdDo.TabIndex = 4
            Me.cmdDo.Text = "&Do"
            Me.cmdDo.UseVisualStyleBackColor = True
            '
            'cmdWalk
            '
            Me.cmdWalk.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdWalk.AutoSize = True
            Me.cmdWalk.Location = New System.Drawing.Point(488, 3)
            Me.cmdWalk.Name = "cmdWalk"
            Me.cmdWalk.Size = New System.Drawing.Size(42, 23)
            Me.cmdWalk.TabIndex = 5
            Me.cmdWalk.Text = "&Walk"
            Me.cmdWalk.UseVisualStyleBackColor = True
            '
            'frmXPathObjectNavigator
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(533, 455)
            Me.Controls.Add(Me.splMain)
            Me.Controls.Add(Me.tlpTop)
            Me.Name = "frmXPathObjectNavigator"
            Me.Text = "Testing XPathObjectNavigator"
            Me.tlpTop.ResumeLayout(False)
            Me.tlpTop.PerformLayout()
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.Panel2.PerformLayout()
            Me.splMain.ResumeLayout(False)
            Me.stsResult.ResumeLayout(False)
            Me.stsResult.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents lblSource As System.Windows.Forms.Label
        Friend WithEvents cmbSource As System.Windows.Forms.ComboBox
        Friend WithEvents tlpTop As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents lblQuery As System.Windows.Forms.Label
        Friend WithEvents txtQuery As System.Windows.Forms.TextBox
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents prgSource As System.Windows.Forms.PropertyGrid
        Friend WithEvents lblSourceProp As System.Windows.Forms.Label
        Friend WithEvents stsResult As System.Windows.Forms.StatusStrip
        Friend WithEvents tslResult As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents tvwResult As System.Windows.Forms.TreeView
        Friend WithEvents lblResult As System.Windows.Forms.Label
        Friend WithEvents cmdDo As System.Windows.Forms.Button
        Friend WithEvents cmdWalk As System.Windows.Forms.Button
    End Class
End Namespace