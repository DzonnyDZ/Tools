Namespace WindowsT.IndependentT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class MessageBoxOptionsEditor
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
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
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
            Me.lblTextAlign = New System.Windows.Forms.Label
            Me.lblTextDirection = New System.Windows.Forms.Label
            Me.lblFocus = New System.Windows.Forms.Label
            Me.chkBring = New System.Windows.Forms.CheckBox
            Me.flpAlign = New System.Windows.Forms.TableLayoutPanel
            Me.optLeft = New System.Windows.Forms.RadioButton
            Me.optCenter = New System.Windows.Forms.RadioButton
            Me.optRight = New System.Windows.Forms.RadioButton
            Me.optJustify = New System.Windows.Forms.RadioButton
            Me.tlpFlow = New System.Windows.Forms.TableLayoutPanel
            Me.optLtR = New System.Windows.Forms.RadioButton
            Me.optRtL = New System.Windows.Forms.RadioButton
            Me.tlpMain.SuspendLayout()
            Me.flpAlign.SuspendLayout()
            Me.tlpFlow.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlpMain
            '
            Me.tlpMain.AutoSize = True
            Me.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMain.ColumnCount = 2
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.Controls.Add(Me.lblTextAlign, 0, 0)
            Me.tlpMain.Controls.Add(Me.lblTextDirection, 0, 1)
            Me.tlpMain.Controls.Add(Me.lblFocus, 0, 2)
            Me.tlpMain.Controls.Add(Me.chkBring, 1, 2)
            Me.tlpMain.Controls.Add(Me.flpAlign, 1, 0)
            Me.tlpMain.Controls.Add(Me.tlpFlow, 1, 1)
            Me.tlpMain.Location = New System.Drawing.Point(0, 0)
            Me.tlpMain.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpMain.Name = "tlpMain"
            Me.tlpMain.RowCount = 3
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.Size = New System.Drawing.Size(170, 138)
            Me.tlpMain.TabIndex = 0
            '
            'lblTextAlign
            '
            Me.lblTextAlign.AutoSize = True
            Me.lblTextAlign.Location = New System.Drawing.Point(3, 0)
            Me.lblTextAlign.Name = "lblTextAlign"
            Me.lblTextAlign.Size = New System.Drawing.Size(53, 13)
            Me.lblTextAlign.TabIndex = 0
            Me.lblTextAlign.Text = "Text align"
            '
            'lblTextDirection
            '
            Me.lblTextDirection.AutoSize = True
            Me.lblTextDirection.Location = New System.Drawing.Point(3, 92)
            Me.lblTextDirection.Name = "lblTextDirection"
            Me.lblTextDirection.Size = New System.Drawing.Size(71, 13)
            Me.lblTextDirection.TabIndex = 2
            Me.lblTextDirection.Text = "Text direction"
            '
            'lblFocus
            '
            Me.lblFocus.AutoSize = True
            Me.lblFocus.Location = New System.Drawing.Point(3, 115)
            Me.lblFocus.Name = "lblFocus"
            Me.lblFocus.Size = New System.Drawing.Size(36, 13)
            Me.lblFocus.TabIndex = 4
            Me.lblFocus.Text = "Focus"
            '
            'chkBring
            '
            Me.chkBring.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.chkBring.AutoSize = True
            Me.chkBring.Location = New System.Drawing.Point(80, 118)
            Me.chkBring.Name = "chkBring"
            Me.chkBring.Size = New System.Drawing.Size(87, 17)
            Me.chkBring.TabIndex = 5
            Me.chkBring.Text = "&Bring to front"
            Me.chkBring.UseVisualStyleBackColor = True
            '
            'flpAlign
            '
            Me.flpAlign.AutoSize = True
            Me.flpAlign.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpAlign.ColumnCount = 1
            Me.flpAlign.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.flpAlign.Controls.Add(Me.optLeft, 0, 0)
            Me.flpAlign.Controls.Add(Me.optCenter, 0, 1)
            Me.flpAlign.Controls.Add(Me.optJustify, 0, 3)
            Me.flpAlign.Controls.Add(Me.optRight, 0, 2)
            Me.flpAlign.Dock = System.Windows.Forms.DockStyle.Fill
            Me.flpAlign.Location = New System.Drawing.Point(77, 0)
            Me.flpAlign.Margin = New System.Windows.Forms.Padding(0)
            Me.flpAlign.Name = "flpAlign"
            Me.flpAlign.RowCount = 4
            Me.flpAlign.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.flpAlign.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.flpAlign.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.flpAlign.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.flpAlign.Size = New System.Drawing.Size(93, 92)
            Me.flpAlign.TabIndex = 6
            '
            'optLeft
            '
            Me.optLeft.AutoSize = True
            Me.optLeft.Dock = System.Windows.Forms.DockStyle.Fill
            Me.optLeft.Location = New System.Drawing.Point(3, 3)
            Me.optLeft.Name = "optLeft"
            Me.optLeft.Size = New System.Drawing.Size(87, 17)
            Me.optLeft.TabIndex = 4
            Me.optLeft.TabStop = True
            Me.optLeft.Text = "&Left"
            Me.optLeft.UseVisualStyleBackColor = True
            '
            'optCenter
            '
            Me.optCenter.AutoSize = True
            Me.optCenter.Dock = System.Windows.Forms.DockStyle.Fill
            Me.optCenter.Location = New System.Drawing.Point(3, 26)
            Me.optCenter.Name = "optCenter"
            Me.optCenter.Size = New System.Drawing.Size(87, 17)
            Me.optCenter.TabIndex = 5
            Me.optCenter.TabStop = True
            Me.optCenter.Text = "&Center"
            Me.optCenter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.optCenter.UseVisualStyleBackColor = True
            '
            'optRight
            '
            Me.optRight.AutoSize = True
            Me.optRight.Dock = System.Windows.Forms.DockStyle.Fill
            Me.optRight.Location = New System.Drawing.Point(3, 49)
            Me.optRight.Name = "optRight"
            Me.optRight.Size = New System.Drawing.Size(87, 17)
            Me.optRight.TabIndex = 6
            Me.optRight.TabStop = True
            Me.optRight.Text = "&Right"
            Me.optRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.optRight.UseVisualStyleBackColor = True
            '
            'optJustify
            '
            Me.optJustify.AutoSize = True
            Me.optJustify.Dock = System.Windows.Forms.DockStyle.Fill
            Me.optJustify.Location = New System.Drawing.Point(3, 72)
            Me.optJustify.Name = "optJustify"
            Me.optJustify.Size = New System.Drawing.Size(87, 17)
            Me.optJustify.TabIndex = 7
            Me.optJustify.TabStop = True
            Me.optJustify.Text = "&J u s t i f y"
            Me.optJustify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.optJustify.UseVisualStyleBackColor = True
            '
            'tlpFlow
            '
            Me.tlpFlow.Anchor = System.Windows.Forms.AnchorStyles.Top
            Me.tlpFlow.AutoSize = True
            Me.tlpFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpFlow.ColumnCount = 2
            Me.tlpFlow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpFlow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpFlow.Controls.Add(Me.optLtR, 0, 0)
            Me.tlpFlow.Controls.Add(Me.optRtL, 1, 0)
            Me.tlpFlow.Location = New System.Drawing.Point(84, 92)
            Me.tlpFlow.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpFlow.Name = "tlpFlow"
            Me.tlpFlow.RowCount = 1
            Me.tlpFlow.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpFlow.Size = New System.Drawing.Size(78, 23)
            Me.tlpFlow.TabIndex = 7
            '
            'optLtR
            '
            Me.optLtR.AutoSize = True
            Me.optLtR.Location = New System.Drawing.Point(3, 3)
            Me.optLtR.Name = "optLtR"
            Me.optLtR.Size = New System.Drawing.Size(33, 17)
            Me.optLtR.TabIndex = 2
            Me.optLtR.TabStop = True
            Me.optLtR.Text = "&ltr"
            Me.optLtR.UseVisualStyleBackColor = True
            '
            'optRtL
            '
            Me.optRtL.AutoSize = True
            Me.optRtL.Location = New System.Drawing.Point(42, 3)
            Me.optRtL.Name = "optRtL"
            Me.optRtL.Size = New System.Drawing.Size(33, 17)
            Me.optRtL.TabIndex = 3
            Me.optRtL.TabStop = True
            Me.optRtL.Text = "&rtl"
            Me.optRtL.UseVisualStyleBackColor = True
            '
            'MessageBoxOptionsEditor
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Controls.Add(Me.tlpMain)
            Me.Name = "MessageBoxOptionsEditor"
            Me.Size = New System.Drawing.Size(170, 138)
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            Me.flpAlign.ResumeLayout(False)
            Me.flpAlign.PerformLayout()
            Me.tlpFlow.ResumeLayout(False)
            Me.tlpFlow.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Private WithEvents lblTextAlign As System.Windows.Forms.Label
        Private WithEvents lblTextDirection As System.Windows.Forms.Label
        Private WithEvents lblFocus As System.Windows.Forms.Label
        Private WithEvents chkBring As System.Windows.Forms.CheckBox
        Private WithEvents flpAlign As System.Windows.Forms.TableLayoutPanel
        Private WithEvents optLeft As System.Windows.Forms.RadioButton
        Private WithEvents optCenter As System.Windows.Forms.RadioButton
        Private WithEvents optJustify As System.Windows.Forms.RadioButton
        Private WithEvents optRight As System.Windows.Forms.RadioButton
        Private WithEvents tlpFlow As System.Windows.Forms.TableLayoutPanel
        Private WithEvents optLtR As System.Windows.Forms.RadioButton
        Private WithEvents optRtL As System.Windows.Forms.RadioButton

    End Class
End Namespace