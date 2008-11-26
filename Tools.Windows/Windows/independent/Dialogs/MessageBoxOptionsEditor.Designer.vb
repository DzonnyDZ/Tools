Namespace WindowsT.IndependentT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class MessageBoxOptionsEditor
        Inherits Tools.WindowsT.FormsT.UserControlExtended

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MessageBoxOptionsEditor))
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
            Me.lblTextAlign = New System.Windows.Forms.Label
            Me.lblTextDirection = New System.Windows.Forms.Label
            Me.lblFocus = New System.Windows.Forms.Label
            Me.chkBring = New System.Windows.Forms.CheckBox
            Me.flpAlign = New System.Windows.Forms.TableLayoutPanel
            Me.optLeft = New System.Windows.Forms.RadioButton
            Me.optCenter = New System.Windows.Forms.RadioButton
            Me.optJustify = New System.Windows.Forms.RadioButton
            Me.optRight = New System.Windows.Forms.RadioButton
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
            Me.tlpMain.AccessibleDescription = Nothing
            Me.tlpMain.AccessibleName = Nothing
            resources.ApplyResources(Me.tlpMain, "tlpMain")
            Me.tlpMain.BackgroundImage = Nothing
            Me.tlpMain.Controls.Add(Me.lblTextAlign, 0, 0)
            Me.tlpMain.Controls.Add(Me.lblTextDirection, 0, 1)
            Me.tlpMain.Controls.Add(Me.lblFocus, 0, 2)
            Me.tlpMain.Controls.Add(Me.chkBring, 1, 2)
            Me.tlpMain.Controls.Add(Me.flpAlign, 1, 0)
            Me.tlpMain.Controls.Add(Me.tlpFlow, 1, 1)
            Me.tlpMain.Font = Nothing
            Me.tlpMain.Name = "tlpMain"
            '
            'lblTextAlign
            '
            Me.lblTextAlign.AccessibleDescription = Nothing
            Me.lblTextAlign.AccessibleName = Nothing
            resources.ApplyResources(Me.lblTextAlign, "lblTextAlign")
            Me.lblTextAlign.Font = Nothing
            Me.lblTextAlign.Name = "lblTextAlign"
            '
            'lblTextDirection
            '
            Me.lblTextDirection.AccessibleDescription = Nothing
            Me.lblTextDirection.AccessibleName = Nothing
            resources.ApplyResources(Me.lblTextDirection, "lblTextDirection")
            Me.lblTextDirection.Font = Nothing
            Me.lblTextDirection.Name = "lblTextDirection"
            '
            'lblFocus
            '
            Me.lblFocus.AccessibleDescription = Nothing
            Me.lblFocus.AccessibleName = Nothing
            resources.ApplyResources(Me.lblFocus, "lblFocus")
            Me.lblFocus.Font = Nothing
            Me.lblFocus.Name = "lblFocus"
            '
            'chkBring
            '
            Me.chkBring.AccessibleDescription = Nothing
            Me.chkBring.AccessibleName = Nothing
            resources.ApplyResources(Me.chkBring, "chkBring")
            Me.chkBring.BackgroundImage = Nothing
            Me.chkBring.Font = Nothing
            Me.chkBring.Name = "chkBring"
            Me.chkBring.UseVisualStyleBackColor = True
            '
            'flpAlign
            '
            Me.flpAlign.AccessibleDescription = Nothing
            Me.flpAlign.AccessibleName = Nothing
            resources.ApplyResources(Me.flpAlign, "flpAlign")
            Me.flpAlign.BackgroundImage = Nothing
            Me.flpAlign.Controls.Add(Me.optLeft, 0, 0)
            Me.flpAlign.Controls.Add(Me.optCenter, 0, 1)
            Me.flpAlign.Controls.Add(Me.optJustify, 0, 3)
            Me.flpAlign.Controls.Add(Me.optRight, 0, 2)
            Me.flpAlign.Font = Nothing
            Me.flpAlign.Name = "flpAlign"
            '
            'optLeft
            '
            Me.optLeft.AccessibleDescription = Nothing
            Me.optLeft.AccessibleName = Nothing
            resources.ApplyResources(Me.optLeft, "optLeft")
            Me.optLeft.BackgroundImage = Nothing
            Me.optLeft.Font = Nothing
            Me.optLeft.Name = "optLeft"
            Me.optLeft.TabStop = True
            Me.optLeft.UseVisualStyleBackColor = True
            '
            'optCenter
            '
            Me.optCenter.AccessibleDescription = Nothing
            Me.optCenter.AccessibleName = Nothing
            resources.ApplyResources(Me.optCenter, "optCenter")
            Me.optCenter.BackgroundImage = Nothing
            Me.optCenter.Font = Nothing
            Me.optCenter.Name = "optCenter"
            Me.optCenter.TabStop = True
            Me.optCenter.UseVisualStyleBackColor = True
            '
            'optJustify
            '
            Me.optJustify.AccessibleDescription = Nothing
            Me.optJustify.AccessibleName = Nothing
            resources.ApplyResources(Me.optJustify, "optJustify")
            Me.optJustify.BackgroundImage = Nothing
            Me.optJustify.Font = Nothing
            Me.optJustify.Name = "optJustify"
            Me.optJustify.TabStop = True
            Me.optJustify.UseVisualStyleBackColor = True
            '
            'optRight
            '
            Me.optRight.AccessibleDescription = Nothing
            Me.optRight.AccessibleName = Nothing
            resources.ApplyResources(Me.optRight, "optRight")
            Me.optRight.BackgroundImage = Nothing
            Me.optRight.Font = Nothing
            Me.optRight.Name = "optRight"
            Me.optRight.TabStop = True
            Me.optRight.UseVisualStyleBackColor = True
            '
            'tlpFlow
            '
            Me.tlpFlow.AccessibleDescription = Nothing
            Me.tlpFlow.AccessibleName = Nothing
            resources.ApplyResources(Me.tlpFlow, "tlpFlow")
            Me.tlpFlow.BackgroundImage = Nothing
            Me.tlpFlow.Controls.Add(Me.optLtR, 0, 0)
            Me.tlpFlow.Controls.Add(Me.optRtL, 1, 0)
            Me.tlpFlow.Font = Nothing
            Me.tlpFlow.Name = "tlpFlow"
            '
            'optLtR
            '
            Me.optLtR.AccessibleDescription = Nothing
            Me.optLtR.AccessibleName = Nothing
            resources.ApplyResources(Me.optLtR, "optLtR")
            Me.optLtR.BackgroundImage = Nothing
            Me.optLtR.Font = Nothing
            Me.optLtR.Name = "optLtR"
            Me.optLtR.TabStop = True
            Me.optLtR.UseVisualStyleBackColor = True
            '
            'optRtL
            '
            Me.optRtL.AccessibleDescription = Nothing
            Me.optRtL.AccessibleName = Nothing
            resources.ApplyResources(Me.optRtL, "optRtL")
            Me.optRtL.BackgroundImage = Nothing
            Me.optRtL.Font = Nothing
            Me.optRtL.Name = "optRtL"
            Me.optRtL.TabStop = True
            Me.optRtL.UseVisualStyleBackColor = True
            '
            'MessageBoxOptionsEditor
            '
            Me.AccessibleDescription = Nothing
            Me.AccessibleName = Nothing
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Nothing
            Me.Controls.Add(Me.tlpMain)
            Me.Font = Nothing
            Me.KeyPreview = True
            Me.Name = "MessageBoxOptionsEditor"
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