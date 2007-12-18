Namespace WindowsT.NativeT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmTestForm
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
            Me.Label = New System.Windows.Forms.Label
            Me.Button = New System.Windows.Forms.Button
            Me.TextBox = New System.Windows.Forms.TextBox
            Me.ProgressBar = New System.Windows.Forms.ProgressBar
            Me.Frame = New System.Windows.Forms.GroupBox
            Me.SuspendLayout()
            '
            'Label
            '
            Me.Label.AutoSize = True
            Me.Label.Location = New System.Drawing.Point(31, 34)
            Me.Label.Name = "Label"
            Me.Label.Size = New System.Drawing.Size(33, 13)
            Me.Label.TabIndex = 0
            Me.Label.Text = "Label"
            '
            'Button
            '
            Me.Button.Location = New System.Drawing.Point(22, 85)
            Me.Button.Name = "Button"
            Me.Button.Size = New System.Drawing.Size(75, 23)
            Me.Button.TabIndex = 1
            Me.Button.Text = "Button"
            Me.Button.UseVisualStyleBackColor = True
            '
            'TextBox
            '
            Me.TextBox.Location = New System.Drawing.Point(237, 34)
            Me.TextBox.Name = "TextBox"
            Me.TextBox.Size = New System.Drawing.Size(85, 20)
            Me.TextBox.TabIndex = 2
            Me.TextBox.Text = "TextBox"
            '
            'ProgressBar
            '
            Me.ProgressBar.Location = New System.Drawing.Point(182, 96)
            Me.ProgressBar.Name = "ProgressBar"
            Me.ProgressBar.Size = New System.Drawing.Size(98, 21)
            Me.ProgressBar.TabIndex = 3
            '
            'Frame
            '
            Me.Frame.Location = New System.Drawing.Point(86, 11)
            Me.Frame.Name = "Frame"
            Me.Frame.Size = New System.Drawing.Size(145, 68)
            Me.Frame.TabIndex = 4
            Me.Frame.TabStop = False
            Me.Frame.Text = "Frame"
            '
            'frmTestForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(338, 129)
            Me.Controls.Add(Me.Frame)
            Me.Controls.Add(Me.ProgressBar)
            Me.Controls.Add(Me.TextBox)
            Me.Controls.Add(Me.Button)
            Me.Controls.Add(Me.Label)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmTestForm"
            Me.ShowInTaskbar = False
            Me.Text = "TestForm"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents Label As System.Windows.Forms.Label
        Friend WithEvents Button As System.Windows.Forms.Button
        Friend WithEvents TextBox As System.Windows.Forms.TextBox
        Friend WithEvents ProgressBar As System.Windows.Forms.ProgressBar
        Friend WithEvents Frame As System.Windows.Forms.GroupBox
    End Class
End Namespace