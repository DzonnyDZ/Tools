Namespace Drawing.IO
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmJPEG
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
            Me.cmdParse = New System.Windows.Forms.Button
            Me.ofdOpen = New System.Windows.Forms.OpenFileDialog
            Me.tvwResults = New System.Windows.Forms.TreeView
            Me.lblI = New System.Windows.Forms.Label
            Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
            Me.TableLayoutPanel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'cmdParse
            '
            Me.cmdParse.AutoSize = True
            Me.cmdParse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdParse.Dock = System.Windows.Forms.DockStyle.Top
            Me.cmdParse.Location = New System.Drawing.Point(0, 0)
            Me.cmdParse.Name = "cmdParse"
            Me.cmdParse.Size = New System.Drawing.Size(543, 23)
            Me.cmdParse.TabIndex = 0
            Me.cmdParse.Text = "Parse"
            Me.cmdParse.UseVisualStyleBackColor = True
            '
            'ofdOpen
            '
            Me.ofdOpen.Filter = "JPEG (*.jpg,*.jpeg,*.jfif)|*.jpg;*.jpeg;*.jfif"
            Me.ofdOpen.Title = "Parse JPEG"
            '
            'tvwResults
            '
            Me.tvwResults.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwResults.Location = New System.Drawing.Point(0, 23)
            Me.tvwResults.Name = "tvwResults"
            Me.tvwResults.Size = New System.Drawing.Size(543, 343)
            Me.tvwResults.TabIndex = 1
            '
            'lblI
            '
            Me.lblI.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.lblI.AutoSize = True
            Me.lblI.Location = New System.Drawing.Point(26, 0)
            Me.lblI.Name = "lblI"
            Me.lblI.Size = New System.Drawing.Size(491, 26)
            Me.lblI.TabIndex = 2
            Me.lblI.Text = "This from tests Tools.Drawing.IO.JPEG, Tools.IO.BinaryReader, Tools.IO.Constraine" & _
                "dReadOnlyStream, Tools.Math.LEBE"
            Me.lblI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'TableLayoutPanel1
            '
            Me.TableLayoutPanel1.AutoSize = True
            Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel1.ColumnCount = 1
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel1.Controls.Add(Me.lblI, 0, 0)
            Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 366)
            Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 1
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel1.Size = New System.Drawing.Size(543, 26)
            Me.TableLayoutPanel1.TabIndex = 3
            '
            'frmJPEG
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(543, 392)
            Me.Controls.Add(Me.tvwResults)
            Me.Controls.Add(Me.cmdParse)
            Me.Controls.Add(Me.TableLayoutPanel1)
            Me.Name = "frmJPEG"
            Me.Text = "Testing Tools.IO.Drawing.JPEG"
            Me.TableLayoutPanel1.ResumeLayout(False)
            Me.TableLayoutPanel1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents cmdParse As System.Windows.Forms.Button
        Friend WithEvents ofdOpen As System.Windows.Forms.OpenFileDialog
        Friend WithEvents tvwResults As System.Windows.Forms.TreeView
        Friend WithEvents lblI As System.Windows.Forms.Label
        Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    End Class
End Namespace