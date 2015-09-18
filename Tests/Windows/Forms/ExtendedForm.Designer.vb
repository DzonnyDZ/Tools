Namespace WindowsT.FormsT
    '#If TrueStage conditional compilation of this file is set in Tests.vbproj
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmExtendedForm
        Inherits Tools.WindowsT.FormsT.ExtendedForm

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
            Me.pgrProperty = New System.Windows.Forms.PropertyGrid
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.lstEvents = New System.Windows.Forms.ListBox
            Me.cmdClear = New System.Windows.Forms.Button
            Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.tlpButtons.SuspendLayout()
            Me.SuspendLayout()
            '
            'pgrProperty
            '
            Me.pgrProperty.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pgrProperty.Location = New System.Drawing.Point(0, 0)
            Me.pgrProperty.Name = "pgrProperty"
            Me.pgrProperty.SelectedObject = Me
            Me.pgrProperty.Size = New System.Drawing.Size(442, 270)
            Me.pgrProperty.TabIndex = 6
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.Location = New System.Drawing.Point(0, 0)
            Me.splMain.Name = "splMain"
            Me.splMain.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.pgrProperty)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.lstEvents)
            Me.splMain.Panel2.Controls.Add(Me.tlpButtons)
            Me.splMain.Size = New System.Drawing.Size(442, 446)
            Me.splMain.SplitterDistance = 270
            Me.splMain.TabIndex = 7
            '
            'lstEvents
            '
            Me.lstEvents.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstEvents.FormattingEnabled = True
            Me.lstEvents.IntegralHeight = False
            Me.lstEvents.Location = New System.Drawing.Point(0, 0)
            Me.lstEvents.Name = "lstEvents"
            Me.lstEvents.Size = New System.Drawing.Size(442, 149)
            Me.lstEvents.TabIndex = 0
            '
            'cmdClear
            '
            Me.cmdClear.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdClear.AutoSize = True
            Me.cmdClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdClear.Location = New System.Drawing.Point(200, 0)
            Me.cmdClear.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdClear.Name = "cmdClear"
            Me.cmdClear.Size = New System.Drawing.Size(41, 23)
            Me.cmdClear.TabIndex = 0
            Me.cmdClear.Text = "Clear"
            Me.cmdClear.UseVisualStyleBackColor = True
            '
            'tlpButtons
            '
            Me.tlpButtons.AutoSize = True
            Me.tlpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpButtons.ColumnCount = 1
            Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpButtons.Controls.Add(Me.cmdClear, 0, 0)
            Me.tlpButtons.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tlpButtons.Location = New System.Drawing.Point(0, 149)
            Me.tlpButtons.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpButtons.Name = "tlpButtons"
            Me.tlpButtons.RowCount = 1
            Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpButtons.Size = New System.Drawing.Size(442, 23)
            Me.tlpButtons.TabIndex = 1
            '
            'frmExtendedForm
            '
            Me.ClientSize = New System.Drawing.Size(442, 446)
            Me.Controls.Add(Me.splMain)
            Me.Name = "frmExtendedForm"
            Me.Text = "Testing Tools.Windows.Forms.ExtendedForm"
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.Panel2.PerformLayout()
            Me.splMain.ResumeLayout(False)
            Me.tlpButtons.ResumeLayout(False)
            Me.tlpButtons.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents pgrProperty As System.Windows.Forms.PropertyGrid
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents lstEvents As System.Windows.Forms.ListBox
        Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents cmdClear As System.Windows.Forms.Button

    End Class
End Namespace