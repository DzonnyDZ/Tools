<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLEBE
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
        Me.txtNumber = New System.Windows.Forms.TextBox
        Me.cmdS = New System.Windows.Forms.Button
        Me.flpMain = New System.Windows.Forms.FlowLayoutPanel
        Me.cmdUS = New System.Windows.Forms.Button
        Me.cmdI = New System.Windows.Forms.Button
        Me.cmdUI = New System.Windows.Forms.Button
        Me.cmdL = New System.Windows.Forms.Button
        Me.cmdUL = New System.Windows.Forms.Button
        Me.lblSrc = New System.Windows.Forms.Label
        Me.lblSrcDec = New System.Windows.Forms.Label
        Me.lblSrcHex = New System.Windows.Forms.Label
        Me.lblRes = New System.Windows.Forms.Label
        Me.lblResDec = New System.Windows.Forms.Label
        Me.lblResHex = New System.Windows.Forms.Label
        Me.flpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtNumber
        '
        Me.txtNumber.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.flpMain.SetFlowBreak(Me.txtNumber, True)
        Me.txtNumber.Location = New System.Drawing.Point(3, 3)
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.Size = New System.Drawing.Size(563, 20)
        Me.txtNumber.TabIndex = 0
        Me.txtNumber.Text = "&h0"
        '
        'cmdS
        '
        Me.cmdS.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdS.AutoSize = True
        Me.cmdS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdS.Location = New System.Drawing.Point(3, 29)
        Me.cmdS.Name = "cmdS"
        Me.cmdS.Size = New System.Drawing.Size(41, 23)
        Me.cmdS.TabIndex = 1
        Me.cmdS.Text = "Int16"
        Me.cmdS.UseVisualStyleBackColor = True
        '
        'flpMain
        '
        Me.flpMain.Controls.Add(Me.txtNumber)
        Me.flpMain.Controls.Add(Me.cmdS)
        Me.flpMain.Controls.Add(Me.cmdUS)
        Me.flpMain.Controls.Add(Me.cmdI)
        Me.flpMain.Controls.Add(Me.cmdUI)
        Me.flpMain.Controls.Add(Me.cmdL)
        Me.flpMain.Controls.Add(Me.cmdUL)
        Me.flpMain.Controls.Add(Me.lblSrc)
        Me.flpMain.Controls.Add(Me.lblSrcDec)
        Me.flpMain.Controls.Add(Me.lblSrcHex)
        Me.flpMain.Controls.Add(Me.lblRes)
        Me.flpMain.Controls.Add(Me.lblResDec)
        Me.flpMain.Controls.Add(Me.lblResHex)
        Me.flpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpMain.Location = New System.Drawing.Point(0, 0)
        Me.flpMain.Name = "flpMain"
        Me.flpMain.Size = New System.Drawing.Size(578, 97)
        Me.flpMain.TabIndex = 2
        '
        'cmdUS
        '
        Me.cmdUS.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdUS.AutoSize = True
        Me.cmdUS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdUS.Location = New System.Drawing.Point(50, 29)
        Me.cmdUS.Name = "cmdUS"
        Me.cmdUS.Size = New System.Drawing.Size(49, 23)
        Me.cmdUS.TabIndex = 2
        Me.cmdUS.Text = "UInt16"
        Me.cmdUS.UseVisualStyleBackColor = True
        '
        'cmdI
        '
        Me.cmdI.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdI.AutoSize = True
        Me.cmdI.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdI.Location = New System.Drawing.Point(105, 29)
        Me.cmdI.Name = "cmdI"
        Me.cmdI.Size = New System.Drawing.Size(41, 23)
        Me.cmdI.TabIndex = 3
        Me.cmdI.Text = "Int32"
        Me.cmdI.UseVisualStyleBackColor = True
        '
        'cmdUI
        '
        Me.cmdUI.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdUI.AutoSize = True
        Me.cmdUI.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdUI.Location = New System.Drawing.Point(152, 29)
        Me.cmdUI.Name = "cmdUI"
        Me.cmdUI.Size = New System.Drawing.Size(49, 23)
        Me.cmdUI.TabIndex = 4
        Me.cmdUI.Text = "UInt32"
        Me.cmdUI.UseVisualStyleBackColor = True
        '
        'cmdL
        '
        Me.cmdL.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdL.AutoSize = True
        Me.cmdL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdL.Location = New System.Drawing.Point(207, 29)
        Me.cmdL.Name = "cmdL"
        Me.cmdL.Size = New System.Drawing.Size(41, 23)
        Me.cmdL.TabIndex = 5
        Me.cmdL.Text = "Int64"
        Me.cmdL.UseVisualStyleBackColor = True
        '
        'cmdUL
        '
        Me.cmdUL.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdUL.AutoSize = True
        Me.cmdUL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpMain.SetFlowBreak(Me.cmdUL, True)
        Me.cmdUL.Location = New System.Drawing.Point(254, 29)
        Me.cmdUL.Name = "cmdUL"
        Me.cmdUL.Size = New System.Drawing.Size(49, 23)
        Me.cmdUL.TabIndex = 6
        Me.cmdUL.Text = "UInt64"
        Me.cmdUL.UseVisualStyleBackColor = True
        '
        'lblSrc
        '
        Me.lblSrc.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSrc.AutoSize = True
        Me.lblSrc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSrc.Location = New System.Drawing.Point(3, 56)
        Me.lblSrc.MaximumSize = New System.Drawing.Size(50, 0)
        Me.lblSrc.MinimumSize = New System.Drawing.Size(50, 0)
        Me.lblSrc.Name = "lblSrc"
        Me.lblSrc.Size = New System.Drawing.Size(50, 13)
        Me.lblSrc.TabIndex = 9
        Me.lblSrc.Text = "Source"
        '
        'lblSrcDec
        '
        Me.lblSrcDec.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSrcDec.AutoSize = True
        Me.lblSrcDec.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSrcDec.Location = New System.Drawing.Point(59, 55)
        Me.lblSrcDec.Name = "lblSrcDec"
        Me.lblSrcDec.Size = New System.Drawing.Size(16, 16)
        Me.lblSrcDec.TabIndex = 7
        Me.lblSrcDec.Text = "0"
        '
        'lblSrcHex
        '
        Me.lblSrcHex.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSrcHex.AutoSize = True
        Me.flpMain.SetFlowBreak(Me.lblSrcHex, True)
        Me.lblSrcHex.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSrcHex.Location = New System.Drawing.Point(81, 55)
        Me.lblSrcHex.Name = "lblSrcHex"
        Me.lblSrcHex.Size = New System.Drawing.Size(40, 16)
        Me.lblSrcHex.TabIndex = 8
        Me.lblSrcHex.Text = "0000"
        '
        'lblRes
        '
        Me.lblRes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRes.AutoSize = True
        Me.lblRes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRes.Location = New System.Drawing.Point(3, 72)
        Me.lblRes.MaximumSize = New System.Drawing.Size(50, 0)
        Me.lblRes.MinimumSize = New System.Drawing.Size(50, 0)
        Me.lblRes.Name = "lblRes"
        Me.lblRes.Size = New System.Drawing.Size(50, 13)
        Me.lblRes.TabIndex = 10
        Me.lblRes.Text = "Result"
        '
        'lblResDec
        '
        Me.lblResDec.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblResDec.AutoSize = True
        Me.lblResDec.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblResDec.Location = New System.Drawing.Point(59, 71)
        Me.lblResDec.Name = "lblResDec"
        Me.lblResDec.Size = New System.Drawing.Size(16, 16)
        Me.lblResDec.TabIndex = 11
        Me.lblResDec.Text = "0"
        '
        'lblResHex
        '
        Me.lblResHex.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblResHex.AutoSize = True
        Me.lblResHex.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblResHex.Location = New System.Drawing.Point(81, 71)
        Me.lblResHex.Name = "lblResHex"
        Me.lblResHex.Size = New System.Drawing.Size(40, 16)
        Me.lblResHex.TabIndex = 12
        Me.lblResHex.Text = "0000"
        '
        'frmLEBE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(578, 97)
        Me.Controls.Add(Me.flpMain)
        Me.Name = "frmLEBE"
        Me.Text = "Testing Tools.Math.LEBE"
        Me.flpMain.ResumeLayout(False)
        Me.flpMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtNumber As System.Windows.Forms.TextBox
    Friend WithEvents flpMain As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cmdS As System.Windows.Forms.Button
    Friend WithEvents cmdUS As System.Windows.Forms.Button
    Friend WithEvents cmdI As System.Windows.Forms.Button
    Friend WithEvents cmdUI As System.Windows.Forms.Button
    Friend WithEvents cmdL As System.Windows.Forms.Button
    Friend WithEvents cmdUL As System.Windows.Forms.Button
    Friend WithEvents lblSrcDec As System.Windows.Forms.Label
    Friend WithEvents lblSrcHex As System.Windows.Forms.Label
    Friend WithEvents lblSrc As System.Windows.Forms.Label
    Friend WithEvents lblRes As System.Windows.Forms.Label
    Friend WithEvents lblResDec As System.Windows.Forms.Label
    Friend WithEvents lblResHex As System.Windows.Forms.Label
End Class
