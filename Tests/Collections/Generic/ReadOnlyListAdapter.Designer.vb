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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReadOnlyListAdapter))
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.lstRW = New System.Windows.Forms.ListBox
            Me.tosRW = New System.Windows.Forms.ToolStrip
            Me.tstAdd = New System.Windows.Forms.ToolStripTextBox
            Me.tsbAdd = New System.Windows.Forms.ToolStripButton
            Me.tsbShowRO = New System.Windows.Forms.ToolStripButton
            Me.lblRW = New System.Windows.Forms.Label
            Me.lstRO = New System.Windows.Forms.ListBox
            Me.tosRO = New System.Windows.Forms.ToolStrip
            Me.tstSrch = New System.Windows.Forms.ToolStripTextBox
            Me.cmdSrch = New System.Windows.Forms.ToolStripButton
            Me.tsbQAll = New System.Windows.Forms.ToolStripButton
            Me.lblRO = New System.Windows.Forms.Label
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.tosRW.SuspendLayout()
            Me.tosRO.SuspendLayout()
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
            Me.splMain.Panel1.Controls.Add(Me.lstRW)
            Me.splMain.Panel1.Controls.Add(Me.tosRW)
            Me.splMain.Panel1.Controls.Add(Me.lblRW)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.lstRO)
            Me.splMain.Panel2.Controls.Add(Me.tosRO)
            Me.splMain.Panel2.Controls.Add(Me.lblRO)
            Me.splMain.Size = New System.Drawing.Size(688, 326)
            Me.splMain.SplitterDistance = 344
            Me.splMain.TabIndex = 0
            '
            'lstRW
            '
            Me.lstRW.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstRW.FormattingEnabled = True
            Me.lstRW.Location = New System.Drawing.Point(0, 13)
            Me.lstRW.Name = "lstRW"
            Me.lstRW.Size = New System.Drawing.Size(344, 277)
            Me.lstRW.TabIndex = 1
            '
            'tosRW
            '
            Me.tosRW.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tosRW.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.tosRW.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tstAdd, Me.tsbAdd, Me.tsbShowRO})
            Me.tosRW.Location = New System.Drawing.Point(0, 301)
            Me.tosRW.Name = "tosRW"
            Me.tosRW.Size = New System.Drawing.Size(344, 25)
            Me.tosRW.TabIndex = 2
            Me.tosRW.Text = "ToolStrip1"
            '
            'tstAdd
            '
            Me.tstAdd.Name = "tstAdd"
            Me.tstAdd.Size = New System.Drawing.Size(100, 25)
            '
            'tsbAdd
            '
            Me.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbAdd.Image = Global.Tools.Tests.My.Resources.Resources.AddTableHS
            Me.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbAdd.Name = "tsbAdd"
            Me.tsbAdd.Size = New System.Drawing.Size(23, 22)
            Me.tsbAdd.Text = "Add"
            '
            'tsbShowRO
            '
            Me.tsbShowRO.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.tsbShowRO.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.tsbShowRO.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbShowRO.Name = "tsbShowRO"
            Me.tsbShowRO.Size = New System.Drawing.Size(27, 22)
            Me.tsbShowRO.Text = ">>"
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
            'lstRO
            '
            Me.lstRO.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstRO.FormattingEnabled = True
            Me.lstRO.Location = New System.Drawing.Point(0, 13)
            Me.lstRO.Name = "lstRO"
            Me.lstRO.Size = New System.Drawing.Size(340, 277)
            Me.lstRO.TabIndex = 1
            '
            'tosRO
            '
            Me.tosRO.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tosRO.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.tosRO.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tstSrch, Me.cmdSrch, Me.tsbQAll})
            Me.tosRO.Location = New System.Drawing.Point(0, 301)
            Me.tosRO.Name = "tosRO"
            Me.tosRO.Size = New System.Drawing.Size(340, 25)
            Me.tosRO.TabIndex = 2
            Me.tosRO.Text = "ToolStrip2"
            '
            'tstSrch
            '
            Me.tstSrch.Name = "tstSrch"
            Me.tstSrch.Size = New System.Drawing.Size(100, 25)
            '
            'cmdSrch
            '
            Me.cmdSrch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.cmdSrch.Image = Global.Tools.Tests.My.Resources.Resources.FindHS
            Me.cmdSrch.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.cmdSrch.Name = "cmdSrch"
            Me.cmdSrch.Size = New System.Drawing.Size(23, 22)
            Me.cmdSrch.Text = "Search"
            '
            'tsbQAll
            '
            Me.tsbQAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.tsbQAll.Image = CType(resources.GetObject("tsbQAll.Image"), System.Drawing.Image)
            Me.tsbQAll.Name = "tsbQAll"
            Me.tsbQAll.Size = New System.Drawing.Size(31, 22)
            Me.tsbQAll.Text = "'%'?"
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
            Me.ShowInTaskbar = False
            Me.Text = "Testing Tools.Collections.Generic.ReadOnlyListAdapter"
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel1.PerformLayout()
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.Panel2.PerformLayout()
            Me.splMain.ResumeLayout(False)
            Me.tosRW.ResumeLayout(False)
            Me.tosRW.PerformLayout()
            Me.tosRO.ResumeLayout(False)
            Me.tosRO.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents lblRW As System.Windows.Forms.Label
        Friend WithEvents lblRO As System.Windows.Forms.Label
        Friend WithEvents lstRW As System.Windows.Forms.ListBox
        Friend WithEvents lstRO As System.Windows.Forms.ListBox
        Friend WithEvents tosRW As System.Windows.Forms.ToolStrip
        Friend WithEvents tstAdd As System.Windows.Forms.ToolStripTextBox
        Friend WithEvents tsbAdd As System.Windows.Forms.ToolStripButton
        Friend WithEvents tosRO As System.Windows.Forms.ToolStrip
        Friend WithEvents tsbShowRO As System.Windows.Forms.ToolStripButton
        Friend WithEvents tstSrch As System.Windows.Forms.ToolStripTextBox
        Friend WithEvents cmdSrch As System.Windows.Forms.ToolStripButton
        Friend WithEvents tsbQAll As System.Windows.Forms.ToolStripButton
    End Class
End Namespace