Namespace CodeDomT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmXml2CodeDom
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
            Me.components = New System.ComponentModel.Container
            Me.tmpMain = New System.Windows.Forms.TableLayoutPanel
            Me.fraLeft = New System.Windows.Forms.GroupBox
            Me.txtLeft = New System.Windows.Forms.TextBox
            Me.fraCenter = New System.Windows.Forms.GroupBox
            Me.splCenter = New System.Windows.Forms.SplitContainer
            Me.tvwCodeDom = New System.Windows.Forms.TreeView
            Me.prgCodeDom = New System.Windows.Forms.PropertyGrid
            Me.lblType = New System.Windows.Forms.Label
            Me.fraRight = New System.Windows.Forms.GroupBox
            Me.txtRight = New System.Windows.Forms.TextBox
            Me.cmdXML2CodeDOML = New System.Windows.Forms.Button
            Me.cmdCodeDOM2XMLL = New System.Windows.Forms.Button
            Me.cmdXML2CodeDOMR = New System.Windows.Forms.Button
            Me.cmdCodeDOM2XMLR = New System.Windows.Forms.Button
            Me.cmdBrowseL = New System.Windows.Forms.Button
            Me.cmdBrowseR = New System.Windows.Forms.Button
            Me.cmdInLanguage = New System.Windows.Forms.Button
            Me.ofdOpen = New System.Windows.Forms.OpenFileDialog
            Me.cmsLanguages = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.tmiVB = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiCS = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiCPP = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiCPPStandard = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiCPP7 = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiCPPVS = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiJSP = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiJS = New System.Windows.Forms.ToolStripMenuItem
            Me.tmpMain.SuspendLayout()
            Me.fraLeft.SuspendLayout()
            Me.fraCenter.SuspendLayout()
            Me.splCenter.Panel1.SuspendLayout()
            Me.splCenter.Panel2.SuspendLayout()
            Me.splCenter.SuspendLayout()
            Me.fraRight.SuspendLayout()
            Me.cmsLanguages.SuspendLayout()
            Me.SuspendLayout()
            '
            'tmpMain
            '
            Me.tmpMain.ColumnCount = 5
            Me.tmpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332!))
            Me.tmpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tmpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
            Me.tmpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tmpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
            Me.tmpMain.Controls.Add(Me.fraLeft, 0, 0)
            Me.tmpMain.Controls.Add(Me.fraCenter, 2, 0)
            Me.tmpMain.Controls.Add(Me.fraRight, 4, 0)
            Me.tmpMain.Controls.Add(Me.cmdXML2CodeDOML, 1, 0)
            Me.tmpMain.Controls.Add(Me.cmdCodeDOM2XMLL, 1, 1)
            Me.tmpMain.Controls.Add(Me.cmdXML2CodeDOMR, 3, 0)
            Me.tmpMain.Controls.Add(Me.cmdCodeDOM2XMLR, 3, 1)
            Me.tmpMain.Controls.Add(Me.cmdBrowseL, 1, 2)
            Me.tmpMain.Controls.Add(Me.cmdBrowseR, 3, 2)
            Me.tmpMain.Controls.Add(Me.cmdInLanguage, 3, 3)
            Me.tmpMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tmpMain.Location = New System.Drawing.Point(0, 0)
            Me.tmpMain.Name = "tmpMain"
            Me.tmpMain.RowCount = 5
            Me.tmpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tmpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tmpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tmpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tmpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tmpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
            Me.tmpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
            Me.tmpMain.Size = New System.Drawing.Size(499, 325)
            Me.tmpMain.TabIndex = 0
            '
            'fraLeft
            '
            Me.fraLeft.Controls.Add(Me.txtLeft)
            Me.fraLeft.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraLeft.Location = New System.Drawing.Point(3, 3)
            Me.fraLeft.Name = "fraLeft"
            Me.tmpMain.SetRowSpan(Me.fraLeft, 5)
            Me.fraLeft.Size = New System.Drawing.Size(133, 319)
            Me.fraLeft.TabIndex = 0
            Me.fraLeft.TabStop = False
            Me.fraLeft.Text = "XML"
            '
            'txtLeft
            '
            Me.txtLeft.Dock = System.Windows.Forms.DockStyle.Fill
            Me.txtLeft.Location = New System.Drawing.Point(3, 16)
            Me.txtLeft.Multiline = True
            Me.txtLeft.Name = "txtLeft"
            Me.txtLeft.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.txtLeft.Size = New System.Drawing.Size(127, 300)
            Me.txtLeft.TabIndex = 0
            Me.txtLeft.WordWrap = False
            '
            'fraCenter
            '
            Me.fraCenter.Controls.Add(Me.splCenter)
            Me.fraCenter.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraCenter.Location = New System.Drawing.Point(177, 3)
            Me.fraCenter.Name = "fraCenter"
            Me.tmpMain.SetRowSpan(Me.fraCenter, 5)
            Me.fraCenter.Size = New System.Drawing.Size(134, 319)
            Me.fraCenter.TabIndex = 1
            Me.fraCenter.TabStop = False
            Me.fraCenter.Text = "CodeDOM"
            '
            'splCenter
            '
            Me.splCenter.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splCenter.Location = New System.Drawing.Point(3, 16)
            Me.splCenter.Name = "splCenter"
            Me.splCenter.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'splCenter.Panel1
            '
            Me.splCenter.Panel1.Controls.Add(Me.tvwCodeDom)
            '
            'splCenter.Panel2
            '
            Me.splCenter.Panel2.Controls.Add(Me.prgCodeDom)
            Me.splCenter.Panel2.Controls.Add(Me.lblType)
            Me.splCenter.Size = New System.Drawing.Size(128, 300)
            Me.splCenter.SplitterDistance = 145
            Me.splCenter.TabIndex = 1
            '
            'tvwCodeDom
            '
            Me.tvwCodeDom.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwCodeDom.Location = New System.Drawing.Point(0, 0)
            Me.tvwCodeDom.Name = "tvwCodeDom"
            Me.tvwCodeDom.Size = New System.Drawing.Size(128, 145)
            Me.tvwCodeDom.TabIndex = 0
            '
            'prgCodeDom
            '
            Me.prgCodeDom.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgCodeDom.Location = New System.Drawing.Point(0, 13)
            Me.prgCodeDom.Name = "prgCodeDom"
            Me.prgCodeDom.Size = New System.Drawing.Size(128, 138)
            Me.prgCodeDom.TabIndex = 0
            '
            'lblType
            '
            Me.lblType.AutoSize = True
            Me.lblType.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblType.Location = New System.Drawing.Point(0, 0)
            Me.lblType.Name = "lblType"
            Me.lblType.Size = New System.Drawing.Size(0, 13)
            Me.lblType.TabIndex = 1
            '
            'fraRight
            '
            Me.fraRight.Controls.Add(Me.txtRight)
            Me.fraRight.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraRight.Location = New System.Drawing.Point(361, 3)
            Me.fraRight.Name = "fraRight"
            Me.tmpMain.SetRowSpan(Me.fraRight, 5)
            Me.fraRight.Size = New System.Drawing.Size(135, 319)
            Me.fraRight.TabIndex = 2
            Me.fraRight.TabStop = False
            Me.fraRight.Text = "XML"
            '
            'txtRight
            '
            Me.txtRight.Dock = System.Windows.Forms.DockStyle.Fill
            Me.txtRight.Location = New System.Drawing.Point(3, 16)
            Me.txtRight.Multiline = True
            Me.txtRight.Name = "txtRight"
            Me.txtRight.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.txtRight.Size = New System.Drawing.Size(129, 300)
            Me.txtRight.TabIndex = 0
            Me.txtRight.WordWrap = False
            '
            'cmdXML2CodeDOML
            '
            Me.cmdXML2CodeDOML.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdXML2CodeDOML.AutoSize = True
            Me.cmdXML2CodeDOML.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdXML2CodeDOML.Location = New System.Drawing.Point(142, 3)
            Me.cmdXML2CodeDOML.Name = "cmdXML2CodeDOML"
            Me.cmdXML2CodeDOML.Size = New System.Drawing.Size(29, 23)
            Me.cmdXML2CodeDOML.TabIndex = 3
            Me.cmdXML2CodeDOML.Text = ">>"
            Me.cmdXML2CodeDOML.UseVisualStyleBackColor = True
            '
            'cmdCodeDOM2XMLL
            '
            Me.cmdCodeDOM2XMLL.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdCodeDOM2XMLL.AutoSize = True
            Me.cmdCodeDOM2XMLL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdCodeDOM2XMLL.Location = New System.Drawing.Point(142, 32)
            Me.cmdCodeDOM2XMLL.Name = "cmdCodeDOM2XMLL"
            Me.cmdCodeDOM2XMLL.Size = New System.Drawing.Size(29, 23)
            Me.cmdCodeDOM2XMLL.TabIndex = 3
            Me.cmdCodeDOM2XMLL.Text = "<<"
            Me.cmdCodeDOM2XMLL.UseVisualStyleBackColor = True
            '
            'cmdXML2CodeDOMR
            '
            Me.cmdXML2CodeDOMR.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdXML2CodeDOMR.AutoSize = True
            Me.cmdXML2CodeDOMR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdXML2CodeDOMR.Location = New System.Drawing.Point(321, 3)
            Me.cmdXML2CodeDOMR.Name = "cmdXML2CodeDOMR"
            Me.cmdXML2CodeDOMR.Size = New System.Drawing.Size(29, 23)
            Me.cmdXML2CodeDOMR.TabIndex = 3
            Me.cmdXML2CodeDOMR.Text = "<<"
            Me.cmdXML2CodeDOMR.UseVisualStyleBackColor = True
            '
            'cmdCodeDOM2XMLR
            '
            Me.cmdCodeDOM2XMLR.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdCodeDOM2XMLR.AutoSize = True
            Me.cmdCodeDOM2XMLR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdCodeDOM2XMLR.Location = New System.Drawing.Point(321, 32)
            Me.cmdCodeDOM2XMLR.Name = "cmdCodeDOM2XMLR"
            Me.cmdCodeDOM2XMLR.Size = New System.Drawing.Size(29, 23)
            Me.cmdCodeDOM2XMLR.TabIndex = 3
            Me.cmdCodeDOM2XMLR.Text = ">>"
            Me.cmdCodeDOM2XMLR.UseVisualStyleBackColor = True
            '
            'cmdBrowseL
            '
            Me.cmdBrowseL.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdBrowseL.AutoSize = True
            Me.cmdBrowseL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdBrowseL.Location = New System.Drawing.Point(143, 61)
            Me.cmdBrowseL.Name = "cmdBrowseL"
            Me.cmdBrowseL.Size = New System.Drawing.Size(26, 23)
            Me.cmdBrowseL.TabIndex = 4
            Me.cmdBrowseL.Text = "..."
            Me.cmdBrowseL.UseVisualStyleBackColor = True
            '
            'cmdBrowseR
            '
            Me.cmdBrowseR.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdBrowseR.AutoSize = True
            Me.cmdBrowseR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdBrowseR.Location = New System.Drawing.Point(323, 61)
            Me.cmdBrowseR.Name = "cmdBrowseR"
            Me.cmdBrowseR.Size = New System.Drawing.Size(26, 23)
            Me.cmdBrowseR.TabIndex = 4
            Me.cmdBrowseR.Text = "..."
            Me.cmdBrowseR.UseVisualStyleBackColor = True
            '
            'cmdInLanguage
            '
            Me.cmdInLanguage.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdInLanguage.AutoSize = True
            Me.cmdInLanguage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdInLanguage.Location = New System.Drawing.Point(314, 90)
            Me.cmdInLanguage.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
            Me.cmdInLanguage.Name = "cmdInLanguage"
            Me.cmdInLanguage.Size = New System.Drawing.Size(44, 23)
            Me.cmdInLanguage.TabIndex = 5
            Me.cmdInLanguage.Text = ">> ▼"
            Me.cmdInLanguage.UseVisualStyleBackColor = True
            '
            'ofdOpen
            '
            Me.ofdOpen.DefaultExt = "xml"
            Me.ofdOpen.Filter = "XML Files (*.xml)|*.xml|HolX files (*.HolX)|*.HolX|All files (*.*)|*.*"
            '
            'cmsLanguages
            '
            Me.cmsLanguages.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiVB, Me.tmiCS, Me.tmiCPP, Me.tmiJSP, Me.tmiJS})
            Me.cmsLanguages.Name = "cmsLanguages"
            Me.cmsLanguages.Size = New System.Drawing.Size(136, 114)
            '
            'tmiVB
            '
            Me.tmiVB.Name = "tmiVB"
            Me.tmiVB.Size = New System.Drawing.Size(135, 22)
            Me.tmiVB.Text = "Visual Basic"
            '
            'tmiCS
            '
            Me.tmiCS.Name = "tmiCS"
            Me.tmiCS.Size = New System.Drawing.Size(135, 22)
            Me.tmiCS.Text = "C#"
            '
            'tmiCPP
            '
            Me.tmiCPP.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiCPPStandard, Me.tmiCPP7, Me.tmiCPPVS})
            Me.tmiCPP.Name = "tmiCPP"
            Me.tmiCPP.Size = New System.Drawing.Size(135, 22)
            Me.tmiCPP.Text = "C++"
            '
            'tmiCPPStandard
            '
            Me.tmiCPPStandard.Name = "tmiCPPStandard"
            Me.tmiCPPStandard.Size = New System.Drawing.Size(152, 22)
            Me.tmiCPPStandard.Text = "Standard"
            '
            'tmiCPP7
            '
            Me.tmiCPP7.Name = "tmiCPP7"
            Me.tmiCPP7.Size = New System.Drawing.Size(152, 22)
            Me.tmiCPP7.Text = "7"
            '
            'tmiCPPVS
            '
            Me.tmiCPPVS.Name = "tmiCPPVS"
            Me.tmiCPPVS.Size = New System.Drawing.Size(152, 22)
            Me.tmiCPPVS.Text = "Visual Studio"
            '
            'tmiJSP
            '
            Me.tmiJSP.Name = "tmiJSP"
            Me.tmiJSP.Size = New System.Drawing.Size(135, 22)
            Me.tmiJSP.Text = "J#"
            '
            'tmiJS
            '
            Me.tmiJS.Name = "tmiJS"
            Me.tmiJS.Size = New System.Drawing.Size(135, 22)
            Me.tmiJS.Text = "Java Script"
            '
            'frmXml2CodeDom
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(499, 325)
            Me.Controls.Add(Me.tmpMain)
            Me.Name = "frmXml2CodeDom"
            Me.Text = "Testing Tools.CodeDomT.Xml2CodeDom"
            Me.tmpMain.ResumeLayout(False)
            Me.tmpMain.PerformLayout()
            Me.fraLeft.ResumeLayout(False)
            Me.fraLeft.PerformLayout()
            Me.fraCenter.ResumeLayout(False)
            Me.splCenter.Panel1.ResumeLayout(False)
            Me.splCenter.Panel2.ResumeLayout(False)
            Me.splCenter.Panel2.PerformLayout()
            Me.splCenter.ResumeLayout(False)
            Me.fraRight.ResumeLayout(False)
            Me.fraRight.PerformLayout()
            Me.cmsLanguages.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents tmpMain As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents fraLeft As System.Windows.Forms.GroupBox
        Friend WithEvents fraCenter As System.Windows.Forms.GroupBox
        Friend WithEvents fraRight As System.Windows.Forms.GroupBox
        Friend WithEvents cmdXML2CodeDOML As System.Windows.Forms.Button
        Friend WithEvents cmdCodeDOM2XMLL As System.Windows.Forms.Button
        Friend WithEvents cmdXML2CodeDOMR As System.Windows.Forms.Button
        Friend WithEvents cmdCodeDOM2XMLR As System.Windows.Forms.Button
        Friend WithEvents txtLeft As System.Windows.Forms.TextBox
        Friend WithEvents tvwCodeDom As System.Windows.Forms.TreeView
        Friend WithEvents txtRight As System.Windows.Forms.TextBox
        Friend WithEvents splCenter As System.Windows.Forms.SplitContainer
        Friend WithEvents prgCodeDom As System.Windows.Forms.PropertyGrid
        Friend WithEvents lblType As System.Windows.Forms.Label
        Friend WithEvents cmdBrowseL As System.Windows.Forms.Button
        Friend WithEvents cmdBrowseR As System.Windows.Forms.Button
        Friend WithEvents ofdOpen As System.Windows.Forms.OpenFileDialog
        Friend WithEvents cmdInLanguage As System.Windows.Forms.Button
        Friend WithEvents cmsLanguages As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents tmiVB As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiCS As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiCPP As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiJSP As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiJS As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiCPPStandard As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiCPP7 As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiCPPVS As System.Windows.Forms.ToolStripMenuItem
    End Class
End Namespace