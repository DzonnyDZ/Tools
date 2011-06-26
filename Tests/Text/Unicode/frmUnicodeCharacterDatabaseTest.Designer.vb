Namespace TextT.UnicodeT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmUnicodeCharacterDatabaseTest
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
            Me.cmdGetXml = New System.Windows.Forms.Button()
            Me.fsdSaveXml = New System.Windows.Forms.SaveFileDialog()
            Me.SuspendLayout()
            '
            'cmdGetXml
            '
            Me.cmdGetXml.Location = New System.Drawing.Point(1, 1)
            Me.cmdGetXml.Name = "cmdGetXml"
            Me.cmdGetXml.Size = New System.Drawing.Size(52, 22)
            Me.cmdGetXml.TabIndex = 0
            Me.cmdGetXml.Text = "GetXml"
            Me.cmdGetXml.UseVisualStyleBackColor = True
            '
            'fsdSaveXml
            '
            Me.fsdSaveXml.DefaultExt = "xml"
            Me.fsdSaveXml.Filter = "XML files (*.xml)|*.xml"
            '
            'frmUnicodeCharacterDatabaseTest
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(284, 262)
            Me.Controls.Add(Me.cmdGetXml)
            Me.Name = "frmUnicodeCharacterDatabaseTest"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.Text = "UnicodeCharacterDatabase"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents cmdGetXml As System.Windows.Forms.Button
        Friend WithEvents fsdSaveXml As System.Windows.Forms.SaveFileDialog
    End Class
End Namespace