Namespace IOt
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmFileSystemEnumeratorWithMask
        Inherits Tests.IOt.frmFileSystemEnumerator

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
            Me.fraMasks = New System.Windows.Forms.GroupBox
            Me.tlpMasks = New System.Windows.Forms.TableLayoutPanel
            Me.lblFileMask = New System.Windows.Forms.Label
            Me.txtFileMask = New System.Windows.Forms.TextBox
            Me.lblFolderContentMask = New System.Windows.Forms.Label
            Me.txtFolderContentMask = New System.Windows.Forms.TextBox
            Me.lblFolderItselfMask = New System.Windows.Forms.Label
            Me.txtFolderItselfMask = New System.Windows.Forms.TextBox
            Me.fraMasks.SuspendLayout()
            Me.tlpMasks.SuspendLayout()
            Me.SuspendLayout()
            '
            'fraMasks
            '
            Me.fraMasks.Controls.Add(Me.tlpMasks)
            Me.fraMasks.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.fraMasks.Location = New System.Drawing.Point(0, 224)
            Me.fraMasks.Name = "fraMasks"
            Me.fraMasks.Size = New System.Drawing.Size(321, 100)
            Me.fraMasks.TabIndex = 1
            Me.fraMasks.TabStop = False
            Me.fraMasks.Text = "Masks (separate by semicolon;)"
            '
            'tlpMasks
            '
            Me.tlpMasks.AutoSize = True
            Me.tlpMasks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMasks.ColumnCount = 2
            Me.tlpMasks.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMasks.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMasks.Controls.Add(Me.lblFileMask, 0, 0)
            Me.tlpMasks.Controls.Add(Me.txtFileMask, 1, 0)
            Me.tlpMasks.Controls.Add(Me.lblFolderContentMask, 0, 1)
            Me.tlpMasks.Controls.Add(Me.txtFolderContentMask, 1, 1)
            Me.tlpMasks.Controls.Add(Me.lblFolderItselfMask, 0, 2)
            Me.tlpMasks.Controls.Add(Me.txtFolderItselfMask, 1, 2)
            Me.tlpMasks.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMasks.Location = New System.Drawing.Point(3, 16)
            Me.tlpMasks.Name = "tlpMasks"
            Me.tlpMasks.RowCount = 3
            Me.tlpMasks.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMasks.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMasks.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMasks.Size = New System.Drawing.Size(315, 81)
            Me.tlpMasks.TabIndex = 0
            '
            'lblFileMask
            '
            Me.lblFileMask.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblFileMask.AutoSize = True
            Me.lblFileMask.Location = New System.Drawing.Point(3, 6)
            Me.lblFileMask.Name = "lblFileMask"
            Me.lblFileMask.Size = New System.Drawing.Size(23, 13)
            Me.lblFileMask.TabIndex = 0
            Me.lblFileMask.Text = "File"
            '
            'txtFileMask
            '
            Me.txtFileMask.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtFileMask.Location = New System.Drawing.Point(140, 3)
            Me.txtFileMask.Name = "txtFileMask"
            Me.txtFileMask.Size = New System.Drawing.Size(172, 20)
            Me.txtFileMask.TabIndex = 1
            Me.txtFileMask.Text = "*"
            '
            'lblFolderContentMask
            '
            Me.lblFolderContentMask.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblFolderContentMask.AutoSize = True
            Me.lblFolderContentMask.Location = New System.Drawing.Point(3, 32)
            Me.lblFolderContentMask.Name = "lblFolderContentMask"
            Me.lblFolderContentMask.Size = New System.Drawing.Size(120, 13)
            Me.lblFolderContentMask.TabIndex = 2
            Me.lblFolderContentMask.Text = "Folder (to enlist content)"
            '
            'txtFolderContentMask
            '
            Me.txtFolderContentMask.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtFolderContentMask.Location = New System.Drawing.Point(140, 29)
            Me.txtFolderContentMask.Name = "txtFolderContentMask"
            Me.txtFolderContentMask.Size = New System.Drawing.Size(172, 20)
            Me.txtFolderContentMask.TabIndex = 3
            Me.txtFolderContentMask.Text = "*"
            '
            'lblFolderItselfMask
            '
            Me.lblFolderItselfMask.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblFolderItselfMask.AutoSize = True
            Me.lblFolderItselfMask.Location = New System.Drawing.Point(3, 60)
            Me.lblFolderItselfMask.Name = "lblFolderItselfMask"
            Me.lblFolderItselfMask.Size = New System.Drawing.Size(131, 13)
            Me.lblFolderItselfMask.TabIndex = 4
            Me.lblFolderItselfMask.Text = "Folder (to include in listing)"
            '
            'txtFolderItselfMask
            '
            Me.txtFolderItselfMask.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtFolderItselfMask.Location = New System.Drawing.Point(140, 56)
            Me.txtFolderItselfMask.Name = "txtFolderItselfMask"
            Me.txtFolderItselfMask.Size = New System.Drawing.Size(172, 20)
            Me.txtFolderItselfMask.TabIndex = 5
            '
            'frmFileSystemEnumeratorWithMask
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.ClientSize = New System.Drawing.Size(321, 324)
            Me.Controls.Add(Me.fraMasks)
            Me.Name = "frmFileSystemEnumeratorWithMask"
            Me.Controls.SetChildIndex(Me.fraMasks, 0)
            Me.fraMasks.ResumeLayout(False)
            Me.fraMasks.PerformLayout()
            Me.tlpMasks.ResumeLayout(False)
            Me.tlpMasks.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents fraMasks As System.Windows.Forms.GroupBox
        Friend WithEvents tlpMasks As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents lblFileMask As System.Windows.Forms.Label
        Friend WithEvents txtFileMask As System.Windows.Forms.TextBox
        Friend WithEvents lblFolderContentMask As System.Windows.Forms.Label
        Friend WithEvents txtFolderContentMask As System.Windows.Forms.TextBox
        Friend WithEvents lblFolderItselfMask As System.Windows.Forms.Label
        Friend WithEvents txtFolderItselfMask As System.Windows.Forms.TextBox

    End Class
End Namespace