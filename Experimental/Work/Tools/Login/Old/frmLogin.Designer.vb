Namespace Login
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmLogin
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
            Me.picLogo = New System.Windows.Forms.PictureBox
            Me.lblUN = New System.Windows.Forms.Label
            Me.lblPwd = New System.Windows.Forms.Label
            Me.txtUN = New System.Windows.Forms.TextBox
            Me.txtPwd = New System.Windows.Forms.TextBox
            Me.cmdOK = New System.Windows.Forms.Button
            Me.cmdCancel = New System.Windows.Forms.Button
            Me.cmdButton = New System.Windows.Forms.Button
            CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'picLogo
            '
            Me.picLogo.Image = CType(resources.GetObject("picLogo.Image"), System.Drawing.Image)
            Me.picLogo.Location = New System.Drawing.Point(0, 0)
            Me.picLogo.Name = "picLogo"
            Me.picLogo.Size = New System.Drawing.Size(168, 166)
            Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picLogo.TabIndex = 0
            Me.picLogo.TabStop = False
            '
            'lblUN
            '
            Me.lblUN.Location = New System.Drawing.Point(172, 24)
            Me.lblUN.Name = "lblUN"
            Me.lblUN.Size = New System.Drawing.Size(220, 23)
            Me.lblUN.TabIndex = 0
            Me.lblUN.Text = "&Uživatelské jméno"
            Me.lblUN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'lblPwd
            '
            Me.lblPwd.Location = New System.Drawing.Point(172, 81)
            Me.lblPwd.Name = "lblPwd"
            Me.lblPwd.Size = New System.Drawing.Size(220, 23)
            Me.lblPwd.TabIndex = 2
            Me.lblPwd.Text = "&Heslo"
            Me.lblPwd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'txtUN
            '
            Me.txtUN.Location = New System.Drawing.Point(174, 44)
            Me.txtUN.Name = "txtUN"
            Me.txtUN.Size = New System.Drawing.Size(220, 20)
            Me.txtUN.TabIndex = 1
            '
            'txtPwd
            '
            Me.txtPwd.Location = New System.Drawing.Point(174, 101)
            Me.txtPwd.Name = "txtPwd"
            Me.txtPwd.Size = New System.Drawing.Size(220, 20)
            Me.txtPwd.TabIndex = 3
            Me.txtPwd.UseSystemPasswordChar = True
            '
            'cmdOK
            '
            Me.cmdOK.Location = New System.Drawing.Point(197, 161)
            Me.cmdOK.Name = "cmdOK"
            Me.cmdOK.Size = New System.Drawing.Size(94, 23)
            Me.cmdOK.TabIndex = 4
            Me.cmdOK.Text = "&OK"
            '
            'cmdCancel
            '
            Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdCancel.Location = New System.Drawing.Point(300, 161)
            Me.cmdCancel.Name = "cmdCancel"
            Me.cmdCancel.Size = New System.Drawing.Size(94, 23)
            Me.cmdCancel.TabIndex = 5
            Me.cmdCancel.Text = "&Storno"
            '
            'cmdButton
            '
            Me.cmdButton.AutoSize = True
            Me.cmdButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdButton.Location = New System.Drawing.Point(0, 161)
            Me.cmdButton.Name = "cmdButton"
            Me.cmdButton.Size = New System.Drawing.Size(71, 23)
            Me.cmdButton.TabIndex = 6
            Me.cmdButton.Text = "&Možnosti ..."
            Me.cmdButton.UseVisualStyleBackColor = True
            Me.cmdButton.Visible = False
            '
            'frmLogin
            '
            Me.AcceptButton = Me.cmdOK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.cmdCancel
            Me.ClientSize = New System.Drawing.Size(400, 192)
            Me.Controls.Add(Me.cmdButton)
            Me.Controls.Add(Me.cmdCancel)
            Me.Controls.Add(Me.cmdOK)
            Me.Controls.Add(Me.txtPwd)
            Me.Controls.Add(Me.txtUN)
            Me.Controls.Add(Me.lblPwd)
            Me.Controls.Add(Me.lblUN)
            Me.Controls.Add(Me.picLogo)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmLogin"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "EOS login"
            CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Protected Friend WithEvents picLogo As System.Windows.Forms.PictureBox
        Protected Friend WithEvents lblUN As System.Windows.Forms.Label
        Protected Friend WithEvents lblPwd As System.Windows.Forms.Label
        Protected Friend WithEvents txtUN As System.Windows.Forms.TextBox
        Protected Friend WithEvents txtPwd As System.Windows.Forms.TextBox
        Protected Friend WithEvents cmdOK As System.Windows.Forms.Button
        Protected Friend WithEvents cmdCancel As System.Windows.Forms.Button
        Private WithEvents cmdButton As System.Windows.Forms.Button

    End Class
End Namespace