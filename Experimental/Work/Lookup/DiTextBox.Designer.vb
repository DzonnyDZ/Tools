Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DiTextBox
    Inherits UserControl

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
        Me._Label1 = New System.Windows.Forms.Label
        Me._Label2 = New System.Windows.Forms.Label
        Me._TextBox1 = New System.Windows.Forms.TextBox
        Me._TextBox2 = New System.Windows.Forms.TextBox
        Me.tlpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.AutoSize = True
        Me.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpMain.ColumnCount = 2
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me._Label1, 0, 0)
        Me.tlpMain.Controls.Add(Me._Label2, 0, 1)
        Me.tlpMain.Controls.Add(Me._TextBox1, 1, 0)
        Me.tlpMain.Controls.Add(Me._TextBox2, 1, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 3
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(151, 52)
        Me.tlpMain.TabIndex = 0
        '
        '_Label1
        '
        Me._Label1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me._Label1.AutoSize = True
        Me._Label1.Location = New System.Drawing.Point(3, 6)
        Me._Label1.Name = "_Label1"
        Me._Label1.Size = New System.Drawing.Size(39, 13)
        Me._Label1.TabIndex = 0
        Me._Label1.Text = "Label1"
        '
        '_Label2
        '
        Me._Label2.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me._Label2.AutoSize = True
        Me._Label2.Location = New System.Drawing.Point(3, 32)
        Me._Label2.Name = "_Label2"
        Me._Label2.Size = New System.Drawing.Size(39, 13)
        Me._Label2.TabIndex = 1
        Me._Label2.Text = "Label2"
        '
        '_TextBox1
        '
        Me._TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._TextBox1.Location = New System.Drawing.Point(48, 3)
        Me._TextBox1.Name = "_TextBox1"
        Me._TextBox1.Size = New System.Drawing.Size(100, 20)
        Me._TextBox1.TabIndex = 2
        '
        '_TextBox2
        '
        Me._TextBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me._TextBox2.Location = New System.Drawing.Point(48, 29)
        Me._TextBox2.Name = "_TextBox2"
        Me._TextBox2.Size = New System.Drawing.Size(100, 20)
        Me._TextBox2.TabIndex = 3
        '
        'DiTextBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.tlpMain)
        Me.Name = "DiTextBox"
        Me.Size = New System.Drawing.Size(151, 52)
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Private WithEvents _Label1 As System.Windows.Forms.Label
    Private WithEvents _Label2 As System.Windows.Forms.Label
    Private WithEvents _TextBox1 As System.Windows.Forms.TextBox
    Private WithEvents _TextBox2 As System.Windows.Forms.TextBox

End Class
