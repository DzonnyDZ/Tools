<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MultiControlBase
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MultiControlBase))
        Me.flpMain = New System.Windows.Forms.FlowLayoutPanel
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.flpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'flpMain
        '
        Me.flpMain.AutoScroll = True
        Me.flpMain.AutoSize = True
        Me.flpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpMain.Controls.Add(Me.cmdAdd)
        Me.flpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpMain.Location = New System.Drawing.Point(0, 0)
        Me.flpMain.Margin = New System.Windows.Forms.Padding(0)
        Me.flpMain.Name = "flpMain"
        Me.flpMain.Size = New System.Drawing.Size(501, 213)
        Me.flpMain.TabIndex = 0
        '
        'cmdAdd
        '
        Me.cmdAdd.AutoSize = True
        Me.cmdAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(3, 3)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Padding = New System.Windows.Forms.Padding(1)
        Me.cmdAdd.Size = New System.Drawing.Size(24, 24)
        Me.cmdAdd.TabIndex = 0
        Me.totToolTip.SetToolTip(Me.cmdAdd, "Přidat")
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'MultiControlBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.flpMain)
        Me.Name = "MultiControlBase"
        Me.Size = New System.Drawing.Size(501, 213)
        Me.flpMain.ResumeLayout(False)
        Me.flpMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents flpMain As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents totToolTip As System.Windows.Forms.ToolTip

End Class
