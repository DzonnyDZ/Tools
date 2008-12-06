Namespace ReflectionT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmFindBestFitCastOperator
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
            Me.obBrowser = New Tools.WindowsT.FormsT.ObjectBrowser
            Me.mnsMain = New System.Windows.Forms.MenuStrip
            Me.tmiFile = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiOpen = New System.Windows.Forms.ToolStripMenuItem
            Me.ofdOpen = New System.Windows.Forms.OpenFileDialog
            Me.lblFrom = New System.Windows.Forms.Label
            Me.cmdFrom = New System.Windows.Forms.Button
            Me.txtFrom = New System.Windows.Forms.TextBox
            Me.lblTo = New System.Windows.Forms.Label
            Me.cmdTo = New System.Windows.Forms.Button
            Me.txtTo = New System.Windows.Forms.TextBox
            Me.cmdFind = New System.Windows.Forms.Button
            Me.tlpControls = New System.Windows.Forms.TableLayoutPanel
            Me.mnsMain.SuspendLayout()
            Me.tlpControls.SuspendLayout()
            Me.SuspendLayout()
            '
            'obBrowser
            '
            Me.obBrowser.Dock = System.Windows.Forms.DockStyle.Fill
            Me.obBrowser.Location = New System.Drawing.Point(0, 24)
            Me.obBrowser.Name = "obBrowser"
            Me.obBrowser.ShowCTors = False
            Me.obBrowser.ShowEvents = False
            Me.obBrowser.ShowFields = False
            Me.obBrowser.ShowFlatNamespaces = True
            Me.obBrowser.ShowGlobalMembers = False
            Me.obBrowser.ShowInitializers = False
            Me.obBrowser.ShowInternalMembers = False
            Me.obBrowser.ShowPrivateMembers = False
            Me.obBrowser.ShowProperties = False
            Me.obBrowser.ShowProtectedMembers = False
            Me.obBrowser.ShowReferences = False
            Me.obBrowser.ShowSpecialMembers = False
            Me.obBrowser.Size = New System.Drawing.Size(837, 318)
            Me.obBrowser.TabIndex = 0
            '
            'mnsMain
            '
            Me.mnsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiFile})
            Me.mnsMain.Location = New System.Drawing.Point(0, 0)
            Me.mnsMain.Name = "mnsMain"
            Me.mnsMain.Size = New System.Drawing.Size(837, 24)
            Me.mnsMain.TabIndex = 1
            Me.mnsMain.Text = "MenuStrip1"
            '
            'tmiFile
            '
            Me.tmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiOpen})
            Me.tmiFile.Name = "tmiFile"
            Me.tmiFile.Size = New System.Drawing.Size(37, 20)
            Me.tmiFile.Text = "&File"
            '
            'tmiOpen
            '
            Me.tmiOpen.Name = "tmiOpen"
            Me.tmiOpen.Size = New System.Drawing.Size(115, 22)
            Me.tmiOpen.Text = "&Open ..."
            '
            'ofdOpen
            '
            Me.ofdOpen.DefaultExt = "dll"
            Me.ofdOpen.Filter = "Assemblies (*.dll, *.exe)|*.dll;*.exe;*.scr"
            Me.ofdOpen.Title = "Open assembly"
            '
            'lblFrom
            '
            Me.lblFrom.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.lblFrom.AutoSize = True
            Me.lblFrom.Location = New System.Drawing.Point(3, 8)
            Me.lblFrom.Name = "lblFrom"
            Me.lblFrom.Size = New System.Drawing.Size(30, 13)
            Me.lblFrom.TabIndex = 0
            Me.lblFrom.Text = "From"
            '
            'cmdFrom
            '
            Me.cmdFrom.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdFrom.AutoSize = True
            Me.cmdFrom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdFrom.Location = New System.Drawing.Point(39, 3)
            Me.cmdFrom.Name = "cmdFrom"
            Me.cmdFrom.Size = New System.Drawing.Size(29, 23)
            Me.cmdFrom.TabIndex = 5
            Me.cmdFrom.Text = ">>"
            Me.cmdFrom.UseVisualStyleBackColor = True
            '
            'txtFrom
            '
            Me.txtFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtFrom.Location = New System.Drawing.Point(74, 4)
            Me.txtFrom.Name = "txtFrom"
            Me.txtFrom.Size = New System.Drawing.Size(325, 20)
            Me.txtFrom.TabIndex = 1
            '
            'lblTo
            '
            Me.lblTo.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.lblTo.AutoSize = True
            Me.lblTo.Location = New System.Drawing.Point(405, 8)
            Me.lblTo.Name = "lblTo"
            Me.lblTo.Size = New System.Drawing.Size(20, 13)
            Me.lblTo.TabIndex = 2
            Me.lblTo.Text = "To"
            '
            'cmdTo
            '
            Me.cmdTo.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdTo.AutoSize = True
            Me.cmdTo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdTo.Location = New System.Drawing.Point(431, 3)
            Me.cmdTo.Name = "cmdTo"
            Me.cmdTo.Size = New System.Drawing.Size(29, 23)
            Me.cmdTo.TabIndex = 5
            Me.cmdTo.Text = ">>"
            Me.cmdTo.UseVisualStyleBackColor = True
            '
            'txtTo
            '
            Me.txtTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtTo.Location = New System.Drawing.Point(466, 4)
            Me.txtTo.Name = "txtTo"
            Me.txtTo.Size = New System.Drawing.Size(325, 20)
            Me.txtTo.TabIndex = 3
            '
            'cmdFind
            '
            Me.cmdFind.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdFind.AutoSize = True
            Me.cmdFind.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdFind.Location = New System.Drawing.Point(797, 3)
            Me.cmdFind.Name = "cmdFind"
            Me.cmdFind.Size = New System.Drawing.Size(37, 23)
            Me.cmdFind.TabIndex = 4
            Me.cmdFind.Text = "Find"
            Me.cmdFind.UseVisualStyleBackColor = True
            '
            'tlpControls
            '
            Me.tlpControls.AutoSize = True
            Me.tlpControls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpControls.ColumnCount = 7
            Me.tlpControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpControls.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpControls.Controls.Add(Me.cmdFind, 6, 0)
            Me.tlpControls.Controls.Add(Me.txtTo, 5, 0)
            Me.tlpControls.Controls.Add(Me.cmdTo, 4, 0)
            Me.tlpControls.Controls.Add(Me.lblTo, 3, 0)
            Me.tlpControls.Controls.Add(Me.txtFrom, 2, 0)
            Me.tlpControls.Controls.Add(Me.cmdFrom, 1, 0)
            Me.tlpControls.Controls.Add(Me.lblFrom, 0, 0)
            Me.tlpControls.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tlpControls.Location = New System.Drawing.Point(0, 342)
            Me.tlpControls.Name = "tlpControls"
            Me.tlpControls.RowCount = 1
            Me.tlpControls.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpControls.Size = New System.Drawing.Size(837, 29)
            Me.tlpControls.TabIndex = 3
            '
            'frmFindBestFitCastOperator
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(837, 371)
            Me.Controls.Add(Me.obBrowser)
            Me.Controls.Add(Me.tlpControls)
            Me.Controls.Add(Me.mnsMain)
            Me.MainMenuStrip = Me.mnsMain
            Me.Name = "frmFindBestFitCastOperator"
            Me.Text = "Testing Tools.ReflectionT.ReflectionTools.FindBestFitCastOperator"
            Me.mnsMain.ResumeLayout(False)
            Me.mnsMain.PerformLayout()
            Me.tlpControls.ResumeLayout(False)
            Me.tlpControls.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents obBrowser As Tools.WindowsT.FormsT.ObjectBrowser
        Friend WithEvents mnsMain As System.Windows.Forms.MenuStrip
        Friend WithEvents tmiFile As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiOpen As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ofdOpen As System.Windows.Forms.OpenFileDialog
        Friend WithEvents lblFrom As System.Windows.Forms.Label
        Friend WithEvents cmdFrom As System.Windows.Forms.Button
        Friend WithEvents txtFrom As System.Windows.Forms.TextBox
        Friend WithEvents cmdTo As System.Windows.Forms.Button
        Friend WithEvents lblTo As System.Windows.Forms.Label
        Friend WithEvents txtTo As System.Windows.Forms.TextBox
        Friend WithEvents cmdFind As System.Windows.Forms.Button
        Friend WithEvents tlpControls As System.Windows.Forms.TableLayoutPanel
    End Class
End Namespace