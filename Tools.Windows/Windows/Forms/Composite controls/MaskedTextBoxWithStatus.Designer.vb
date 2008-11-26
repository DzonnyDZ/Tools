Namespace WindowsT.FormsT
#If Config <= Nightly Then
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class MaskedTextBoxWithStatus
        Inherits Tools.WindowsT.FormsT.ControlWithStatus

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
            Me.mtbText = New System.Windows.Forms.MaskedTextBox
            Me.tlpMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'stmStatus
            '
            Me.stmStatus.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.stmStatus.Location = New System.Drawing.Point(235, 0)
            Me.stmStatus.Margin = New System.Windows.Forms.Padding(0)
            Me.stmStatus.TabIndex = 1
            '
            'tlpMain
            '
            Me.tlpMain.AutoSize = True
            Me.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMain.ColumnCount = 2
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMain.Controls.Add(Me.mtbText, 0, 0)
            Me.tlpMain.Controls.Add(Me.stmStatus, 1, 0)
            Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMain.Location = New System.Drawing.Point(0, 0)
            Me.tlpMain.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpMain.Name = "tlpMain"
            Me.tlpMain.RowCount = 1
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.Size = New System.Drawing.Size(259, 24)
            Me.tlpMain.TabIndex = 0
            Me.tlpMain.Controls.SetChildIndex(Me.stmStatus, 0)
            Me.tlpMain.Controls.SetChildIndex(Me.mtbText, 0)
            '
            'mtbText
            '
            Me.mtbText.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.mtbText.Location = New System.Drawing.Point(0, 2)
            Me.mtbText.Margin = New System.Windows.Forms.Padding(0)
            Me.mtbText.Name = "mtbText"
            Me.mtbText.Size = New System.Drawing.Size(235, 20)
            Me.mtbText.TabIndex = 0
            '
            'MaskedTextBoxWithStatus
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.tlpMain)
            Me.MinimumSize = New System.Drawing.Size(40, 20)
            Me.Name = "MaskedTextBoxWithStatus"
            Me.Size = New System.Drawing.Size(259, 24)
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents mtbText As System.Windows.Forms.MaskedTextBox
    End Class
#End If
End Namespace