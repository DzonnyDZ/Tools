Namespace Collections.Generic
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmHashTable
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
            Me.lstItems = New System.Windows.Forms.ListBox
            Me.flpItems = New System.Windows.Forms.FlowLayoutPanel
            Me.txtAdd = New System.Windows.Forms.TextBox
            Me.cmdAdd = New System.Windows.Forms.Button
            Me.cmdDel = New System.Windows.Forms.Button
            Me.flpItems.SuspendLayout()
            Me.SuspendLayout()
            '
            'lstItems
            '
            Me.lstItems.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstItems.FormattingEnabled = True
            Me.lstItems.Location = New System.Drawing.Point(0, 0)
            Me.lstItems.Name = "lstItems"
            Me.lstItems.Size = New System.Drawing.Size(292, 264)
            Me.lstItems.TabIndex = 0
            '
            'flpItems
            '
            Me.flpItems.AutoSize = True
            Me.flpItems.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpItems.Controls.Add(Me.txtAdd)
            Me.flpItems.Controls.Add(Me.cmdAdd)
            Me.flpItems.Controls.Add(Me.cmdDel)
            Me.flpItems.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.flpItems.Location = New System.Drawing.Point(0, 244)
            Me.flpItems.Name = "flpItems"
            Me.flpItems.Size = New System.Drawing.Size(292, 29)
            Me.flpItems.TabIndex = 1
            '
            'txtAdd
            '
            Me.txtAdd.AcceptsReturn = True
            Me.txtAdd.Location = New System.Drawing.Point(3, 3)
            Me.txtAdd.Name = "txtAdd"
            Me.txtAdd.Size = New System.Drawing.Size(100, 20)
            Me.txtAdd.TabIndex = 0
            '
            'cmdAdd
            '
            Me.cmdAdd.AutoSize = True
            Me.cmdAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdAdd.Location = New System.Drawing.Point(109, 3)
            Me.cmdAdd.Name = "cmdAdd"
            Me.cmdAdd.Size = New System.Drawing.Size(36, 23)
            Me.cmdAdd.TabIndex = 1
            Me.cmdAdd.Text = "&Add"
            Me.cmdAdd.UseVisualStyleBackColor = True
            '
            'cmdDel
            '
            Me.cmdDel.AutoSize = True
            Me.cmdDel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdDel.Location = New System.Drawing.Point(151, 3)
            Me.cmdDel.Name = "cmdDel"
            Me.cmdDel.Size = New System.Drawing.Size(57, 23)
            Me.cmdDel.TabIndex = 2
            Me.cmdDel.Text = "&Remove"
            Me.cmdDel.UseVisualStyleBackColor = True
            '
            'frmHashTable
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(292, 273)
            Me.Controls.Add(Me.flpItems)
            Me.Controls.Add(Me.lstItems)
            Me.Name = "frmHashTable"
            Me.Text = "Testing HashTable(Of T)"
            Me.flpItems.ResumeLayout(False)
            Me.flpItems.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents lstItems As System.Windows.Forms.ListBox
        Friend WithEvents flpItems As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents txtAdd As System.Windows.Forms.TextBox
        Friend WithEvents cmdAdd As System.Windows.Forms.Button
        Friend WithEvents cmdDel As System.Windows.Forms.Button
    End Class
End Namespace