Namespace WindowsT.NativeT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmWin32Window
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
            Me.prgProperties = New System.Windows.Forms.PropertyGrid
            Me.cmbObject = New System.Windows.Forms.ComboBox
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.flpCommands = New System.Windows.Forms.FlowLayoutPanel
            Me.flpSetParent = New System.Windows.Forms.FlowLayoutPanel
            Me.cmbSetParent = New System.Windows.Forms.ComboBox
            Me.cmdSetParent = New System.Windows.Forms.Button
            Me.cmdLoadParent = New System.Windows.Forms.Button
            Me.cmdLoadDesktop = New System.Windows.Forms.Button
            Me.fraWindowList = New System.Windows.Forms.GroupBox
            Me.tlpWindowList = New System.Windows.Forms.TableLayoutPanel
            Me.cmdLoadTopLevel = New System.Windows.Forms.Button
            Me.cmdLoadChildren = New System.Windows.Forms.Button
            Me.lstWindowList = New System.Windows.Forms.ListBox
            Me.lblPrintMessage = New System.Windows.Forms.Label
            Me.chkPrintMessage1 = New System.Windows.Forms.CheckBox
            Me.chkPrintMessage2 = New System.Windows.Forms.CheckBox
            Me.cmdWndReplace = New System.Windows.Forms.Button
            Me.cmdWndRenew = New System.Windows.Forms.Button
            Me.lblWndProc = New System.Windows.Forms.Label
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.flpCommands.SuspendLayout()
            Me.flpSetParent.SuspendLayout()
            Me.fraWindowList.SuspendLayout()
            Me.tlpWindowList.SuspendLayout()
            Me.SuspendLayout()
            '
            'prgProperties
            '
            Me.prgProperties.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgProperties.Location = New System.Drawing.Point(0, 21)
            Me.prgProperties.Name = "prgProperties"
            Me.prgProperties.Size = New System.Drawing.Size(176, 401)
            Me.prgProperties.TabIndex = 0
            '
            'cmbObject
            '
            Me.cmbObject.Dock = System.Windows.Forms.DockStyle.Top
            Me.cmbObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbObject.FormattingEnabled = True
            Me.cmbObject.Location = New System.Drawing.Point(0, 0)
            Me.cmbObject.Name = "cmbObject"
            Me.cmbObject.Size = New System.Drawing.Size(176, 21)
            Me.cmbObject.TabIndex = 1
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.Location = New System.Drawing.Point(0, 0)
            Me.splMain.Name = "splMain"
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.prgProperties)
            Me.splMain.Panel1.Controls.Add(Me.cmbObject)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.flpCommands)
            Me.splMain.Size = New System.Drawing.Size(355, 422)
            Me.splMain.SplitterDistance = 176
            Me.splMain.TabIndex = 2
            '
            'flpCommands
            '
            Me.flpCommands.AutoSize = True
            Me.flpCommands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpCommands.Controls.Add(Me.flpSetParent)
            Me.flpCommands.Controls.Add(Me.cmdLoadParent)
            Me.flpCommands.Controls.Add(Me.cmdLoadDesktop)
            Me.flpCommands.Controls.Add(Me.fraWindowList)
            Me.flpCommands.Controls.Add(Me.lblPrintMessage)
            Me.flpCommands.Controls.Add(Me.chkPrintMessage1)
            Me.flpCommands.Controls.Add(Me.chkPrintMessage2)
            Me.flpCommands.Controls.Add(Me.lblWndProc)
            Me.flpCommands.Controls.Add(Me.cmdWndReplace)
            Me.flpCommands.Controls.Add(Me.cmdWndRenew)
            Me.flpCommands.Dock = System.Windows.Forms.DockStyle.Fill
            Me.flpCommands.Location = New System.Drawing.Point(0, 0)
            Me.flpCommands.Margin = New System.Windows.Forms.Padding(0)
            Me.flpCommands.Name = "flpCommands"
            Me.flpCommands.Size = New System.Drawing.Size(175, 422)
            Me.flpCommands.TabIndex = 0
            '
            'flpSetParent
            '
            Me.flpSetParent.AutoSize = True
            Me.flpSetParent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpSetParent.Controls.Add(Me.cmbSetParent)
            Me.flpSetParent.Controls.Add(Me.cmdSetParent)
            Me.flpSetParent.Location = New System.Drawing.Point(0, 0)
            Me.flpSetParent.Margin = New System.Windows.Forms.Padding(0)
            Me.flpSetParent.Name = "flpSetParent"
            Me.flpSetParent.Size = New System.Drawing.Size(156, 23)
            Me.flpSetParent.TabIndex = 0
            '
            'cmbSetParent
            '
            Me.cmbSetParent.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.cmbSetParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbSetParent.FormattingEnabled = True
            Me.cmbSetParent.Location = New System.Drawing.Point(0, 1)
            Me.cmbSetParent.Margin = New System.Windows.Forms.Padding(0)
            Me.cmbSetParent.Name = "cmbSetParent"
            Me.cmbSetParent.Size = New System.Drawing.Size(90, 21)
            Me.cmbSetParent.TabIndex = 2
            '
            'cmdSetParent
            '
            Me.cmdSetParent.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.cmdSetParent.AutoSize = True
            Me.cmdSetParent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdSetParent.Location = New System.Drawing.Point(90, 0)
            Me.cmdSetParent.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdSetParent.Name = "cmdSetParent"
            Me.cmdSetParent.Size = New System.Drawing.Size(66, 23)
            Me.cmdSetParent.TabIndex = 3
            Me.cmdSetParent.Text = "Set parent"
            Me.cmdSetParent.UseVisualStyleBackColor = True
            '
            'cmdLoadParent
            '
            Me.cmdLoadParent.AutoSize = True
            Me.cmdLoadParent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdLoadParent.Location = New System.Drawing.Point(0, 23)
            Me.cmdLoadParent.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdLoadParent.Name = "cmdLoadParent"
            Me.cmdLoadParent.Size = New System.Drawing.Size(147, 23)
            Me.cmdLoadParent.TabIndex = 1
            Me.cmdLoadParent.Text = "Load parent to property grid"
            Me.cmdLoadParent.UseVisualStyleBackColor = True
            '
            'cmdLoadDesktop
            '
            Me.cmdLoadDesktop.AutoSize = True
            Me.cmdLoadDesktop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdLoadDesktop.Location = New System.Drawing.Point(0, 46)
            Me.cmdLoadDesktop.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdLoadDesktop.Name = "cmdLoadDesktop"
            Me.cmdLoadDesktop.Size = New System.Drawing.Size(155, 23)
            Me.cmdLoadDesktop.TabIndex = 2
            Me.cmdLoadDesktop.Text = "Load desktop to property grid"
            Me.cmdLoadDesktop.UseVisualStyleBackColor = True
            '
            'fraWindowList
            '
            Me.fraWindowList.AutoSize = True
            Me.fraWindowList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.fraWindowList.Controls.Add(Me.tlpWindowList)
            Me.flpCommands.SetFlowBreak(Me.fraWindowList, True)
            Me.fraWindowList.Location = New System.Drawing.Point(0, 69)
            Me.fraWindowList.Margin = New System.Windows.Forms.Padding(0)
            Me.fraWindowList.Name = "fraWindowList"
            Me.fraWindowList.Padding = New System.Windows.Forms.Padding(0)
            Me.fraWindowList.Size = New System.Drawing.Size(168, 157)
            Me.fraWindowList.TabIndex = 3
            Me.fraWindowList.TabStop = False
            Me.fraWindowList.Text = "Window list"
            '
            'tlpWindowList
            '
            Me.tlpWindowList.AutoSize = True
            Me.tlpWindowList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpWindowList.ColumnCount = 2
            Me.tlpWindowList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpWindowList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpWindowList.Controls.Add(Me.cmdLoadTopLevel, 0, 0)
            Me.tlpWindowList.Controls.Add(Me.cmdLoadChildren, 1, 0)
            Me.tlpWindowList.Controls.Add(Me.lstWindowList, 0, 1)
            Me.tlpWindowList.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpWindowList.Location = New System.Drawing.Point(0, 13)
            Me.tlpWindowList.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpWindowList.Name = "tlpWindowList"
            Me.tlpWindowList.RowCount = 2
            Me.tlpWindowList.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpWindowList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpWindowList.Size = New System.Drawing.Size(168, 144)
            Me.tlpWindowList.TabIndex = 0
            '
            'cmdLoadTopLevel
            '
            Me.cmdLoadTopLevel.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdLoadTopLevel.AutoSize = True
            Me.cmdLoadTopLevel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdLoadTopLevel.Location = New System.Drawing.Point(0, 0)
            Me.cmdLoadTopLevel.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdLoadTopLevel.Name = "cmdLoadTopLevel"
            Me.cmdLoadTopLevel.Size = New System.Drawing.Size(84, 23)
            Me.cmdLoadTopLevel.TabIndex = 0
            Me.cmdLoadTopLevel.Text = "Load top level"
            Me.cmdLoadTopLevel.UseVisualStyleBackColor = True
            '
            'cmdLoadChildren
            '
            Me.cmdLoadChildren.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdLoadChildren.AutoSize = True
            Me.cmdLoadChildren.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdLoadChildren.Location = New System.Drawing.Point(84, 0)
            Me.cmdLoadChildren.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdLoadChildren.Name = "cmdLoadChildren"
            Me.cmdLoadChildren.Size = New System.Drawing.Size(84, 23)
            Me.cmdLoadChildren.TabIndex = 1
            Me.cmdLoadChildren.Text = "Load children"
            Me.cmdLoadChildren.UseVisualStyleBackColor = True
            '
            'lstWindowList
            '
            Me.lstWindowList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tlpWindowList.SetColumnSpan(Me.lstWindowList, 2)
            Me.lstWindowList.FormattingEnabled = True
            Me.lstWindowList.Location = New System.Drawing.Point(0, 23)
            Me.lstWindowList.Margin = New System.Windows.Forms.Padding(0)
            Me.lstWindowList.Name = "lstWindowList"
            Me.lstWindowList.Size = New System.Drawing.Size(168, 121)
            Me.lstWindowList.TabIndex = 2
            '
            'lblPrintMessage
            '
            Me.lblPrintMessage.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblPrintMessage.AutoSize = True
            Me.lblPrintMessage.Location = New System.Drawing.Point(0, 231)
            Me.lblPrintMessage.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
            Me.lblPrintMessage.Name = "lblPrintMessage"
            Me.lblPrintMessage.Size = New System.Drawing.Size(103, 13)
            Me.lblPrintMessage.TabIndex = 4
            Me.lblPrintMessage.Text = "Form prints message"
            '
            'chkPrintMessage1
            '
            Me.chkPrintMessage1.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.chkPrintMessage1.AutoSize = True
            Me.chkPrintMessage1.Location = New System.Drawing.Point(106, 229)
            Me.chkPrintMessage1.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
            Me.chkPrintMessage1.Name = "chkPrintMessage1"
            Me.chkPrintMessage1.Size = New System.Drawing.Size(32, 17)
            Me.chkPrintMessage1.TabIndex = 5
            Me.chkPrintMessage1.Text = "1"
            Me.chkPrintMessage1.UseVisualStyleBackColor = True
            '
            'chkPrintMessage2
            '
            Me.chkPrintMessage2.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.chkPrintMessage2.AutoSize = True
            Me.flpCommands.SetFlowBreak(Me.chkPrintMessage2, True)
            Me.chkPrintMessage2.Location = New System.Drawing.Point(138, 229)
            Me.chkPrintMessage2.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
            Me.chkPrintMessage2.Name = "chkPrintMessage2"
            Me.chkPrintMessage2.Size = New System.Drawing.Size(32, 17)
            Me.chkPrintMessage2.TabIndex = 6
            Me.chkPrintMessage2.Text = "2"
            Me.chkPrintMessage2.UseVisualStyleBackColor = True
            '
            'cmdWndReplace
            '
            Me.cmdWndReplace.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.cmdWndReplace.AutoSize = True
            Me.cmdWndReplace.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdWndReplace.Location = New System.Drawing.Point(52, 252)
            Me.cmdWndReplace.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
            Me.cmdWndReplace.Name = "cmdWndReplace"
            Me.cmdWndReplace.Size = New System.Drawing.Size(57, 23)
            Me.cmdWndReplace.TabIndex = 7
            Me.cmdWndReplace.Text = "Replace"
            Me.cmdWndReplace.UseVisualStyleBackColor = True
            '
            'cmdWndRenew
            '
            Me.cmdWndRenew.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.cmdWndRenew.AutoSize = True
            Me.cmdWndRenew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdWndRenew.Location = New System.Drawing.Point(109, 252)
            Me.cmdWndRenew.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
            Me.cmdWndRenew.Name = "cmdWndRenew"
            Me.cmdWndRenew.Size = New System.Drawing.Size(51, 23)
            Me.cmdWndRenew.TabIndex = 8
            Me.cmdWndRenew.Text = "Renew"
            Me.cmdWndRenew.UseVisualStyleBackColor = True
            '
            'lblWndProc
            '
            Me.lblWndProc.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblWndProc.AutoSize = True
            Me.lblWndProc.Location = New System.Drawing.Point(0, 257)
            Me.lblWndProc.Margin = New System.Windows.Forms.Padding(0)
            Me.lblWndProc.Name = "lblWndProc"
            Me.lblWndProc.Size = New System.Drawing.Size(52, 13)
            Me.lblWndProc.TabIndex = 9
            Me.lblWndProc.Text = "WndProc"
            '
            'frmWin32Window
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(355, 422)
            Me.Controls.Add(Me.splMain)
            Me.Name = "frmWin32Window"
            Me.Text = "Testing Tools.WindowsT.NativeT.Win32Window"
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.Panel2.PerformLayout()
            Me.splMain.ResumeLayout(False)
            Me.flpCommands.ResumeLayout(False)
            Me.flpCommands.PerformLayout()
            Me.flpSetParent.ResumeLayout(False)
            Me.flpSetParent.PerformLayout()
            Me.fraWindowList.ResumeLayout(False)
            Me.fraWindowList.PerformLayout()
            Me.tlpWindowList.ResumeLayout(False)
            Me.tlpWindowList.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents prgProperties As System.Windows.Forms.PropertyGrid
        Friend WithEvents cmbObject As System.Windows.Forms.ComboBox
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents flpCommands As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents flpSetParent As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cmbSetParent As System.Windows.Forms.ComboBox
        Friend WithEvents cmdSetParent As System.Windows.Forms.Button
        Friend WithEvents cmdLoadParent As System.Windows.Forms.Button
        Friend WithEvents cmdLoadDesktop As System.Windows.Forms.Button
        Friend WithEvents fraWindowList As System.Windows.Forms.GroupBox
        Friend WithEvents tlpWindowList As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents cmdLoadTopLevel As System.Windows.Forms.Button
        Friend WithEvents cmdLoadChildren As System.Windows.Forms.Button
        Friend WithEvents lstWindowList As System.Windows.Forms.ListBox
        Friend WithEvents lblPrintMessage As System.Windows.Forms.Label
        Friend WithEvents chkPrintMessage1 As System.Windows.Forms.CheckBox
        Friend WithEvents chkPrintMessage2 As System.Windows.Forms.CheckBox
        Friend WithEvents cmdWndReplace As System.Windows.Forms.Button
        Friend WithEvents lblWndProc As System.Windows.Forms.Label
        Friend WithEvents cmdWndRenew As System.Windows.Forms.Button
    End Class
End Namespace