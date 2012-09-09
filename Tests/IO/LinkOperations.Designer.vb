Namespace IOt
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class LinkOperations
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
            Me.tlpPaths = New System.Windows.Forms.TableLayoutPanel()
            Me.lblSrc = New System.Windows.Forms.Label()
            Me.txtSrc = New System.Windows.Forms.TextBox()
            Me.btnSrc = New System.Windows.Forms.Button()
            Me.lblDest = New System.Windows.Forms.Label()
            Me.txtDest = New System.Windows.Forms.TextBox()
            Me.btnDest = New System.Windows.Forms.Button()
            Me.tlpOptions = New System.Windows.Forms.TableLayoutPanel()
            Me.lblMask = New System.Windows.Forms.Label()
            Me.txtMask = New System.Windows.Forms.TextBox()
            Me.chkRecursive = New System.Windows.Forms.CheckBox()
            Me.flpButtons = New System.Windows.Forms.FlowLayoutPanel()
            Me.cmdLnk2Sym = New System.Windows.Forms.Button()
            Me.cmdSym2Lnk = New System.Windows.Forms.Button()
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
            Me.fbdSelectFolder = New System.Windows.Forms.FolderBrowserDialog()
            Me.tlpPaths.SuspendLayout()
            Me.tlpOptions.SuspendLayout()
            Me.flpButtons.SuspendLayout()
            Me.tlpMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlpPaths
            '
            Me.tlpPaths.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tlpPaths.AutoSize = True
            Me.tlpPaths.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpPaths.ColumnCount = 4
            Me.tlpPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.tlpPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.tlpPaths.Controls.Add(Me.lblSrc, 0, 0)
            Me.tlpPaths.Controls.Add(Me.txtSrc, 0, 1)
            Me.tlpPaths.Controls.Add(Me.btnSrc, 1, 1)
            Me.tlpPaths.Controls.Add(Me.lblDest, 2, 0)
            Me.tlpPaths.Controls.Add(Me.txtDest, 2, 1)
            Me.tlpPaths.Controls.Add(Me.btnDest, 3, 1)
            Me.tlpPaths.Location = New System.Drawing.Point(3, 3)
            Me.tlpPaths.Name = "tlpPaths"
            Me.tlpPaths.RowCount = 2
            Me.tlpPaths.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpPaths.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpPaths.Size = New System.Drawing.Size(638, 42)
            Me.tlpPaths.TabIndex = 0
            '
            'lblSrc
            '
            Me.lblSrc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lblSrc.AutoSize = True
            Me.tlpPaths.SetColumnSpan(Me.lblSrc, 2)
            Me.lblSrc.Location = New System.Drawing.Point(3, 0)
            Me.lblSrc.Name = "lblSrc"
            Me.lblSrc.Size = New System.Drawing.Size(70, 13)
            Me.lblSrc.TabIndex = 0
            Me.lblSrc.Text = "Source folder"
            '
            'txtSrc
            '
            Me.txtSrc.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtSrc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.txtSrc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
            Me.txtSrc.Location = New System.Drawing.Point(3, 17)
            Me.txtSrc.Name = "txtSrc"
            Me.txtSrc.Size = New System.Drawing.Size(281, 20)
            Me.txtSrc.TabIndex = 2
            '
            'btnSrc
            '
            Me.btnSrc.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.btnSrc.AutoSize = True
            Me.btnSrc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.btnSrc.Location = New System.Drawing.Point(290, 16)
            Me.btnSrc.Name = "btnSrc"
            Me.btnSrc.Size = New System.Drawing.Size(26, 23)
            Me.btnSrc.TabIndex = 3
            Me.btnSrc.Text = "..."
            Me.btnSrc.UseVisualStyleBackColor = True
            '
            'lblDest
            '
            Me.lblDest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.lblDest.AutoSize = True
            Me.tlpPaths.SetColumnSpan(Me.lblDest, 2)
            Me.lblDest.Location = New System.Drawing.Point(322, 0)
            Me.lblDest.Name = "lblDest"
            Me.lblDest.Size = New System.Drawing.Size(67, 13)
            Me.lblDest.TabIndex = 1
            Me.lblDest.Text = "Target folder"
            '
            'txtDest
            '
            Me.txtDest.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtDest.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.txtDest.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
            Me.txtDest.Location = New System.Drawing.Point(322, 17)
            Me.txtDest.Name = "txtDest"
            Me.txtDest.Size = New System.Drawing.Size(281, 20)
            Me.txtDest.TabIndex = 4
            '
            'btnDest
            '
            Me.btnDest.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.btnDest.AutoSize = True
            Me.btnDest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.btnDest.Location = New System.Drawing.Point(609, 16)
            Me.btnDest.Name = "btnDest"
            Me.btnDest.Size = New System.Drawing.Size(26, 23)
            Me.btnDest.TabIndex = 5
            Me.btnDest.Text = "..."
            Me.btnDest.UseVisualStyleBackColor = True
            '
            'tlpOptions
            '
            Me.tlpOptions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tlpOptions.AutoSize = True
            Me.tlpOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpOptions.ColumnCount = 2
            Me.tlpOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.tlpOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpOptions.Controls.Add(Me.lblMask, 0, 0)
            Me.tlpOptions.Controls.Add(Me.txtMask, 1, 0)
            Me.tlpOptions.Controls.Add(Me.chkRecursive, 0, 1)
            Me.tlpOptions.Location = New System.Drawing.Point(3, 51)
            Me.tlpOptions.Name = "tlpOptions"
            Me.tlpOptions.RowCount = 2
            Me.tlpOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpOptions.Size = New System.Drawing.Size(638, 49)
            Me.tlpOptions.TabIndex = 1
            '
            'lblMask
            '
            Me.lblMask.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblMask.AutoSize = True
            Me.lblMask.Location = New System.Drawing.Point(3, 6)
            Me.lblMask.Name = "lblMask"
            Me.lblMask.Size = New System.Drawing.Size(33, 13)
            Me.lblMask.TabIndex = 0
            Me.lblMask.Text = "Mask"
            '
            'txtMask
            '
            Me.txtMask.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtMask.Location = New System.Drawing.Point(42, 3)
            Me.txtMask.Name = "txtMask"
            Me.txtMask.Size = New System.Drawing.Size(593, 20)
            Me.txtMask.TabIndex = 1
            Me.txtMask.Text = "*.*"
            '
            'chkRecursive
            '
            Me.chkRecursive.AutoSize = True
            Me.tlpOptions.SetColumnSpan(Me.chkRecursive, 2)
            Me.chkRecursive.Location = New System.Drawing.Point(3, 29)
            Me.chkRecursive.Name = "chkRecursive"
            Me.chkRecursive.Size = New System.Drawing.Size(74, 17)
            Me.chkRecursive.TabIndex = 2
            Me.chkRecursive.Text = "Recursive"
            Me.chkRecursive.UseVisualStyleBackColor = True
            '
            'flpButtons
            '
            Me.flpButtons.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.flpButtons.Controls.Add(Me.cmdLnk2Sym)
            Me.flpButtons.Controls.Add(Me.cmdSym2Lnk)
            Me.flpButtons.Location = New System.Drawing.Point(3, 106)
            Me.flpButtons.Name = "flpButtons"
            Me.flpButtons.Size = New System.Drawing.Size(638, 153)
            Me.flpButtons.TabIndex = 2
            '
            'cmdLnk2Sym
            '
            Me.cmdLnk2Sym.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdLnk2Sym.AutoSize = True
            Me.cmdLnk2Sym.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdLnk2Sym.Location = New System.Drawing.Point(3, 3)
            Me.cmdLnk2Sym.Name = "cmdLnk2Sym"
            Me.cmdLnk2Sym.Size = New System.Drawing.Size(98, 23)
            Me.cmdLnk2Sym.TabIndex = 0
            Me.cmdLnk2Sym.Text = "Links to Symlinks"
            Me.cmdLnk2Sym.UseVisualStyleBackColor = True
            '
            'cmdSym2Lnk
            '
            Me.cmdSym2Lnk.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdSym2Lnk.AutoSize = True
            Me.cmdSym2Lnk.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdSym2Lnk.Location = New System.Drawing.Point(107, 3)
            Me.cmdSym2Lnk.Name = "cmdSym2Lnk"
            Me.cmdSym2Lnk.Size = New System.Drawing.Size(94, 23)
            Me.cmdSym2Lnk.TabIndex = 1
            Me.cmdSym2Lnk.Text = "Symlinks to links"
            Me.cmdSym2Lnk.UseVisualStyleBackColor = True
            '
            'tlpMain
            '
            Me.tlpMain.ColumnCount = 1
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.Controls.Add(Me.tlpPaths, 0, 0)
            Me.tlpMain.Controls.Add(Me.tlpOptions, 0, 1)
            Me.tlpMain.Controls.Add(Me.flpButtons, 0, 2)
            Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMain.Location = New System.Drawing.Point(0, 0)
            Me.tlpMain.Name = "tlpMain"
            Me.tlpMain.RowCount = 3
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.Size = New System.Drawing.Size(644, 262)
            Me.tlpMain.TabIndex = 3
            '
            'LinkOperations
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(644, 262)
            Me.Controls.Add(Me.tlpMain)
            Me.Name = "LinkOperations"
            Me.Text = "LinkOperations"
            Me.tlpPaths.ResumeLayout(False)
            Me.tlpPaths.PerformLayout()
            Me.tlpOptions.ResumeLayout(False)
            Me.tlpOptions.PerformLayout()
            Me.flpButtons.ResumeLayout(False)
            Me.flpButtons.PerformLayout()
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents tlpPaths As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents lblSrc As System.Windows.Forms.Label
        Friend WithEvents txtSrc As System.Windows.Forms.TextBox
        Friend WithEvents btnSrc As System.Windows.Forms.Button
        Friend WithEvents lblDest As System.Windows.Forms.Label
        Friend WithEvents txtDest As System.Windows.Forms.TextBox
        Friend WithEvents btnDest As System.Windows.Forms.Button
        Friend WithEvents tlpOptions As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents lblMask As System.Windows.Forms.Label
        Friend WithEvents txtMask As System.Windows.Forms.TextBox
        Friend WithEvents chkRecursive As System.Windows.Forms.CheckBox
        Friend WithEvents flpButtons As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents fbdSelectFolder As System.Windows.Forms.FolderBrowserDialog
        Friend WithEvents cmdLnk2Sym As System.Windows.Forms.Button
        Friend WithEvents cmdSym2Lnk As System.Windows.Forms.Button
    End Class
End Namespace