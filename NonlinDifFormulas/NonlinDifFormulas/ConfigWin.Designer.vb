<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigWin
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
        Me.txtProgram = New System.Windows.Forms.TextBox
        Me.btnRun = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txtProgram
        '
        Me.txtProgram.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.NonlinDifFormulas.My.MySettings.Default, "TestProgram", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtProgram.Location = New System.Drawing.Point(72, 96)
        Me.txtProgram.Name = "txtProgram"
        Me.txtProgram.Size = New System.Drawing.Size(320, 20)
        Me.txtProgram.TabIndex = 0
        Me.txtProgram.Text = Global.NonlinDifFormulas.My.MySettings.Default.TestProgram
        '
        'btnRun
        '
        Me.btnRun.Location = New System.Drawing.Point(360, 264)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(75, 23)
        Me.btnRun.TabIndex = 1
        Me.btnRun.Text = "Button1"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'ConfigWin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(551, 430)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.txtProgram)
        Me.Name = "ConfigWin"
        Me.Text = "ConfigWin"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtProgram As System.Windows.Forms.TextBox
    Friend WithEvents btnRun As System.Windows.Forms.Button
End Class
