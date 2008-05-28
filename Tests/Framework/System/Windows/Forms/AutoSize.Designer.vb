Namespace Framework.SystemF.WindowsF.FormsF
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmAutoSize
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
            Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
            Me.Button1 = New System.Windows.Forms.Button
            Me.Button2 = New System.Windows.Forms.Button
            Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
            Me.Button3 = New System.Windows.Forms.Button
            Me.Button4 = New System.Windows.Forms.Button
            Me.Button5 = New System.Windows.Forms.Button
            Me.TableLayoutPanel1.SuspendLayout()
            Me.FlowLayoutPanel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'TableLayoutPanel1
            '
            Me.TableLayoutPanel1.AutoSize = True
            Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
            Me.TableLayoutPanel1.ColumnCount = 2
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.TableLayoutPanel1.Controls.Add(Me.Button1, 0, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.Button2, 1, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel1, 0, 1)
            Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 2
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.TableLayoutPanel1.Size = New System.Drawing.Size(190, 69)
            Me.TableLayoutPanel1.TabIndex = 0
            '
            'Button1
            '
            Me.Button1.AutoSize = True
            Me.Button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Button1.Location = New System.Drawing.Point(4, 4)
            Me.Button1.Name = "Button1"
            Me.Button1.Size = New System.Drawing.Size(54, 23)
            Me.Button1.TabIndex = 0
            Me.Button1.Text = "Button1"
            Me.Button1.UseVisualStyleBackColor = True
            '
            'Button2
            '
            Me.Button2.AutoSize = True
            Me.Button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Button2.Location = New System.Drawing.Point(65, 4)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(54, 23)
            Me.Button2.TabIndex = 1
            Me.Button2.Text = "Button2"
            Me.Button2.UseVisualStyleBackColor = True
            '
            'FlowLayoutPanel1
            '
            Me.FlowLayoutPanel1.AutoSize = True
            Me.FlowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.FlowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.TableLayoutPanel1.SetColumnSpan(Me.FlowLayoutPanel1, 2)
            Me.FlowLayoutPanel1.Controls.Add(Me.Button3)
            Me.FlowLayoutPanel1.Controls.Add(Me.Button4)
            Me.FlowLayoutPanel1.Controls.Add(Me.Button5)
            Me.FlowLayoutPanel1.Location = New System.Drawing.Point(4, 34)
            Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
            Me.FlowLayoutPanel1.Size = New System.Drawing.Size(182, 31)
            Me.FlowLayoutPanel1.TabIndex = 2
            '
            'Button3
            '
            Me.Button3.AutoSize = True
            Me.Button3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Button3.Location = New System.Drawing.Point(3, 3)
            Me.Button3.Name = "Button3"
            Me.Button3.Size = New System.Drawing.Size(54, 23)
            Me.Button3.TabIndex = 0
            Me.Button3.Text = "Button3"
            Me.Button3.UseVisualStyleBackColor = True
            '
            'Button4
            '
            Me.Button4.AutoSize = True
            Me.Button4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Button4.Location = New System.Drawing.Point(63, 3)
            Me.Button4.Name = "Button4"
            Me.Button4.Size = New System.Drawing.Size(54, 23)
            Me.Button4.TabIndex = 1
            Me.Button4.Text = "Button4"
            Me.Button4.UseVisualStyleBackColor = True
            '
            'Button5
            '
            Me.Button5.AutoSize = True
            Me.Button5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Button5.Location = New System.Drawing.Point(123, 3)
            Me.Button5.Name = "Button5"
            Me.Button5.Size = New System.Drawing.Size(54, 23)
            Me.Button5.TabIndex = 2
            Me.Button5.Text = "Button5"
            Me.Button5.UseVisualStyleBackColor = True
            '
            'frmAutoSize
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.ClientSize = New System.Drawing.Size(284, 264)
            Me.Controls.Add(Me.TableLayoutPanel1)
            Me.Name = "frmAutoSize"
            Me.Text = "AutoSize"
            Me.TableLayoutPanel1.ResumeLayout(False)
            Me.TableLayoutPanel1.PerformLayout()
            Me.FlowLayoutPanel1.ResumeLayout(False)
            Me.FlowLayoutPanel1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents Button1 As System.Windows.Forms.Button
        Friend WithEvents Button2 As System.Windows.Forms.Button
        Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents Button3 As System.Windows.Forms.Button
        Friend WithEvents Button4 As System.Windows.Forms.Button
        Friend WithEvents Button5 As System.Windows.Forms.Button
    End Class
End Namespace