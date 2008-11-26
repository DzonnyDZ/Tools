Namespace WindowsT.FormsT
#If Config <= Nightly Then
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class MultiLineTextBoxWithStatus
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
            Me.txtText = New System.Windows.Forms.TextBox
            Me.stmStatus = New Tools.WindowsT.FormsT.StatusMarker
            Me.SuspendLayout()
            '
            'txtText
            '
            Me.txtText.Dock = System.Windows.Forms.DockStyle.Fill
            Me.txtText.Location = New System.Drawing.Point(0, 0)
            Me.txtText.Margin = New System.Windows.Forms.Padding(0)
            Me.txtText.Multiline = True
            Me.txtText.Name = "txtText"
            Me.txtText.Size = New System.Drawing.Size(200, 91)
            Me.txtText.TabIndex = 0
            '
            'stmStatus
            '
            Me.stmStatus.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.stmStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.stmStatus.AutoChanged = False
            Me.stmStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.stmStatus.Location = New System.Drawing.Point(176, 0)
            Me.stmStatus.Margin = New System.Windows.Forms.Padding(0)
            Me.stmStatus.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.stmStatus.Name = "stmStatus"
            Me.stmStatus.Size = New System.Drawing.Size(24, 24)
            Me.stmStatus.TabIndex = 1
            Me.stmStatus.TabStop = False
            '
            'MultiLineTextBoxWithStatus
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.stmStatus)
            Me.Controls.Add(Me.txtText)
            Me.MinimumSize = New System.Drawing.Size(40, 40)
            Me.Name = "MultiLineTextBoxWithStatus"
            Me.Size = New System.Drawing.Size(200, 91)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents txtText As System.Windows.Forms.TextBox
        Friend WithEvents stmStatus As Tools.WindowsT.FormsT.StatusMarker
    End Class
#End If
End Namespace