Namespace IOt
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmFileSystemEnumerator
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
            Me.fbdRoot = New System.Windows.Forms.FolderBrowserDialog
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
            Me.lblRoot = New System.Windows.Forms.Label
            Me.txtRoot = New System.Windows.Forms.TextBox
            Me.cmdRoot = New System.Windows.Forms.Button
            Me.lstList = New System.Windows.Forms.ListBox
            Me.chkFoldersFirst = New System.Windows.Forms.CheckBox
            Me.cmdGo = New System.Windows.Forms.Button
            Me.tlpMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'fbdRoot
            '
            Me.fbdRoot.ShowNewFolderButton = False
            '
            'tlpMain
            '
            Me.tlpMain.ColumnCount = 3
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMain.Controls.Add(Me.lblRoot, 0, 0)
            Me.tlpMain.Controls.Add(Me.txtRoot, 1, 0)
            Me.tlpMain.Controls.Add(Me.cmdRoot, 2, 0)
            Me.tlpMain.Controls.Add(Me.lstList, 0, 2)
            Me.tlpMain.Controls.Add(Me.chkFoldersFirst, 0, 1)
            Me.tlpMain.Controls.Add(Me.cmdGo, 2, 1)
            Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMain.Location = New System.Drawing.Point(0, 0)
            Me.tlpMain.Name = "tlpMain"
            Me.tlpMain.RowCount = 3
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.Size = New System.Drawing.Size(284, 264)
            Me.tlpMain.TabIndex = 0
            '
            'lblRoot
            '
            Me.lblRoot.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblRoot.AutoSize = True
            Me.lblRoot.Location = New System.Drawing.Point(3, 8)
            Me.lblRoot.Name = "lblRoot"
            Me.lblRoot.Size = New System.Drawing.Size(30, 13)
            Me.lblRoot.TabIndex = 0
            Me.lblRoot.Text = "Root"
            '
            'txtRoot
            '
            Me.txtRoot.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtRoot.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.txtRoot.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
            Me.txtRoot.Location = New System.Drawing.Point(39, 4)
            Me.txtRoot.Name = "txtRoot"
            Me.txtRoot.Size = New System.Drawing.Size(205, 20)
            Me.txtRoot.TabIndex = 1
            '
            'cmdRoot
            '
            Me.cmdRoot.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdRoot.AutoSize = True
            Me.cmdRoot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdRoot.Location = New System.Drawing.Point(250, 3)
            Me.cmdRoot.Name = "cmdRoot"
            Me.cmdRoot.Size = New System.Drawing.Size(31, 23)
            Me.cmdRoot.TabIndex = 2
            Me.cmdRoot.Text = "..."
            Me.cmdRoot.UseVisualStyleBackColor = True
            '
            'lstList
            '
            Me.tlpMain.SetColumnSpan(Me.lstList, 3)
            Me.lstList.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstList.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.lstList.FormattingEnabled = True
            Me.lstList.Location = New System.Drawing.Point(3, 61)
            Me.lstList.Name = "lstList"
            Me.lstList.Size = New System.Drawing.Size(278, 199)
            Me.lstList.TabIndex = 4
            '
            'chkFoldersFirst
            '
            Me.chkFoldersFirst.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.chkFoldersFirst.AutoSize = True
            Me.tlpMain.SetColumnSpan(Me.chkFoldersFirst, 2)
            Me.chkFoldersFirst.Location = New System.Drawing.Point(3, 35)
            Me.chkFoldersFirst.Name = "chkFoldersFirst"
            Me.chkFoldersFirst.Size = New System.Drawing.Size(241, 17)
            Me.chkFoldersFirst.TabIndex = 5
            Me.chkFoldersFirst.Text = "&Folders first"
            Me.chkFoldersFirst.UseVisualStyleBackColor = True
            '
            'cmdGo
            '
            Me.cmdGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdGo.AutoSize = True
            Me.cmdGo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdGo.Location = New System.Drawing.Point(250, 32)
            Me.cmdGo.Name = "cmdGo"
            Me.cmdGo.Size = New System.Drawing.Size(31, 23)
            Me.cmdGo.TabIndex = 3
            Me.cmdGo.Text = "&Go"
            Me.cmdGo.UseVisualStyleBackColor = True
            '
            'frmFileSystemEnumerator
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(284, 264)
            Me.Controls.Add(Me.tlpMain)
            Me.Name = "frmFileSystemEnumerator"
            Me.Text = "Testing Tools.IOt.FileSystemEnumerator"
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents fbdRoot As System.Windows.Forms.FolderBrowserDialog
        Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents lblRoot As System.Windows.Forms.Label
        Friend WithEvents txtRoot As System.Windows.Forms.TextBox
        Friend WithEvents cmdRoot As System.Windows.Forms.Button
        Friend WithEvents cmdGo As System.Windows.Forms.Button
        Friend WithEvents lstList As System.Windows.Forms.ListBox
        Friend WithEvents chkFoldersFirst As System.Windows.Forms.CheckBox
    End Class
End Namespace