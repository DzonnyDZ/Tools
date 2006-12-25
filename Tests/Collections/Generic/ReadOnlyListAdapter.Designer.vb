Namespace Collections.Generic
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmReadOnlyListAdapter
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
            Me.lblRW = New System.Windows.Forms.Label
            Me.lblRO = New System.Windows.Forms.Label
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
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
            Me.splMain.Panel1.Controls.Add(Me.lblRW)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.lblRO)
            Me.splMain.Size = New System.Drawing.Size(688, 326)
            Me.splMain.SplitterDistance = 344
            Me.splMain.TabIndex = 0
            '
            'lblRW
            '
            Me.lblRW.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblRW.Location = New System.Drawing.Point(0, 0)
            Me.lblRW.Name = "lblRW"
            Me.lblRW.Size = New System.Drawing.Size(344, 13)
            Me.lblRW.TabIndex = 0
            Me.lblRW.Text = "Read-Write"
            Me.lblRW.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'lblRO
            '
            Me.lblRO.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblRO.Location = New System.Drawing.Point(0, 0)
            Me.lblRO.Name = "lblRO"
            Me.lblRO.Size = New System.Drawing.Size(340, 13)
            Me.lblRO.TabIndex = 0
            Me.lblRO.Text = "Read-Only"
            Me.lblRO.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'frmReadOnlyListAdapter
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(688, 326)
            Me.Controls.Add(Me.splMain)
            Me.Name = "frmReadOnlyListAdapter"
            Me.Text = "Testing Tools.Collections.Generic.ReadOnlyListAdapter"
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents lblRW As System.Windows.Forms.Label
        Friend WithEvents lblRO As System.Windows.Forms.Label
    End Class
End Namespace