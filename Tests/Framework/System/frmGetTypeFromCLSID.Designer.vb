Namespace Framework.SystemF
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmGetTypeFromCLSID
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
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.tlpGetType = New System.Windows.Forms.TableLayoutPanel
            Me.lglGuid = New System.Windows.Forms.Label
            Me.txtGuid = New System.Windows.Forms.TextBox
            Me.cmdGetType = New System.Windows.Forms.Button
            Me.prgType = New System.Windows.Forms.PropertyGrid
            Me.cmdInstance = New System.Windows.Forms.Button
            Me.prgInstance = New System.Windows.Forms.PropertyGrid
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.tlpGetType.SuspendLayout()
            Me.SuspendLayout()
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.Location = New System.Drawing.Point(0, 0)
            Me.splMain.Name = "splMain"
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.tlpGetType)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.prgInstance)
            Me.splMain.Size = New System.Drawing.Size(600, 386)
            Me.splMain.SplitterDistance = 304
            Me.splMain.TabIndex = 0
            '
            'tlpGetType
            '
            Me.tlpGetType.ColumnCount = 3
            Me.tlpGetType.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpGetType.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpGetType.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpGetType.Controls.Add(Me.lglGuid, 0, 0)
            Me.tlpGetType.Controls.Add(Me.txtGuid, 1, 0)
            Me.tlpGetType.Controls.Add(Me.cmdGetType, 2, 0)
            Me.tlpGetType.Controls.Add(Me.prgType, 0, 1)
            Me.tlpGetType.Controls.Add(Me.cmdInstance, 0, 2)
            Me.tlpGetType.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpGetType.Location = New System.Drawing.Point(0, 0)
            Me.tlpGetType.Name = "tlpGetType"
            Me.tlpGetType.RowCount = 3
            Me.tlpGetType.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpGetType.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpGetType.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpGetType.Size = New System.Drawing.Size(304, 386)
            Me.tlpGetType.TabIndex = 0
            '
            'lglGuid
            '
            Me.lglGuid.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lglGuid.AutoSize = True
            Me.lglGuid.Location = New System.Drawing.Point(3, 8)
            Me.lglGuid.Name = "lglGuid"
            Me.lglGuid.Size = New System.Drawing.Size(34, 13)
            Me.lglGuid.TabIndex = 0
            Me.lglGuid.Text = "GUID"
            '
            'txtGuid
            '
            Me.txtGuid.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtGuid.Location = New System.Drawing.Point(43, 4)
            Me.txtGuid.Name = "txtGuid"
            Me.txtGuid.Size = New System.Drawing.Size(195, 20)
            Me.txtGuid.TabIndex = 1
            '
            'cmdGetType
            '
            Me.cmdGetType.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdGetType.AutoSize = True
            Me.cmdGetType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdGetType.Location = New System.Drawing.Point(244, 3)
            Me.cmdGetType.Name = "cmdGetType"
            Me.cmdGetType.Size = New System.Drawing.Size(57, 23)
            Me.cmdGetType.TabIndex = 2
            Me.cmdGetType.Text = "Get type"
            Me.cmdGetType.UseVisualStyleBackColor = True
            '
            'prgType
            '
            Me.tlpGetType.SetColumnSpan(Me.prgType, 3)
            Me.prgType.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgType.Location = New System.Drawing.Point(3, 32)
            Me.prgType.Name = "prgType"
            Me.prgType.Size = New System.Drawing.Size(298, 322)
            Me.prgType.TabIndex = 3
            '
            'cmdInstance
            '
            Me.cmdInstance.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdInstance.AutoSize = True
            Me.cmdInstance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpGetType.SetColumnSpan(Me.cmdInstance, 3)
            Me.cmdInstance.Location = New System.Drawing.Point(106, 360)
            Me.cmdInstance.Name = "cmdInstance"
            Me.cmdInstance.Size = New System.Drawing.Size(91, 23)
            Me.cmdInstance.TabIndex = 4
            Me.cmdInstance.Text = "Create instance"
            Me.cmdInstance.UseVisualStyleBackColor = True
            '
            'prgInstance
            '
            Me.prgInstance.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgInstance.Location = New System.Drawing.Point(0, 0)
            Me.prgInstance.Name = "prgInstance"
            Me.prgInstance.Size = New System.Drawing.Size(292, 386)
            Me.prgInstance.TabIndex = 4
            '
            'frmGetTypeFromCLSID
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(600, 386)
            Me.Controls.Add(Me.splMain)
            Me.Name = "frmGetTypeFromCLSID"
            Me.Text = "Testing System.Type.GetTypeFromCLSID"
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.ResumeLayout(False)
            Me.tlpGetType.ResumeLayout(False)
            Me.tlpGetType.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents tlpGetType As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents lglGuid As System.Windows.Forms.Label
        Friend WithEvents txtGuid As System.Windows.Forms.TextBox
        Friend WithEvents cmdGetType As System.Windows.Forms.Button
        Friend WithEvents prgType As System.Windows.Forms.PropertyGrid
        Friend WithEvents cmdInstance As System.Windows.Forms.Button
        Friend WithEvents prgInstance As System.Windows.Forms.PropertyGrid
    End Class
End Namespace