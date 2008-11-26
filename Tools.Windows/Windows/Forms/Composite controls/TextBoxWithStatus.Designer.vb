Namespace WindowsT.FormsT
#If Config <= Nightly Then
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class TextBoxWithStatus
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
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
            Me.txtText = New System.Windows.Forms.TextBox
            Me.stmStatus = New Tools.WindowsT.FormsT.StatusMarker
            Me.tlpMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlpMain
            '
            Me.tlpMain.AutoSize = True
            Me.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMain.ColumnCount = 2
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMain.Controls.Add(Me.txtText, 0, 0)
            Me.tlpMain.Controls.Add(Me.stmStatus, 1, 0)
            Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMain.Location = New System.Drawing.Point(0, 0)
            Me.tlpMain.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpMain.Name = "tlpMain"
            Me.tlpMain.RowCount = 1
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.Size = New System.Drawing.Size(259, 24)
            Me.tlpMain.TabIndex = 0
            '
            'txtText
            '
            Me.txtText.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtText.Location = New System.Drawing.Point(0, 2)
            Me.txtText.Margin = New System.Windows.Forms.Padding(0)
            Me.txtText.Name = "txtText"
            Me.txtText.Size = New System.Drawing.Size(235, 20)
            Me.txtText.TabIndex = 0
            '
            'stmStatus
            '
            Me.stmStatus.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.stmStatus.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.stmStatus.AutoChanged = False
            Me.stmStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.stmStatus.Location = New System.Drawing.Point(235, 0)
            Me.stmStatus.Margin = New System.Windows.Forms.Padding(0)
            Me.stmStatus.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.stmStatus.Name = "stmStatus"
            Me.stmStatus.Size = New System.Drawing.Size(24, 24)
            Me.stmStatus.TabIndex = 1
            Me.stmStatus.TabStop = False
            '
            'TextBoxWithStatus
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.tlpMain)
            Me.MinimumSize = New System.Drawing.Size(40, 20)
            Me.Name = "TextBoxWithStatus"
            Me.Size = New System.Drawing.Size(259, 24)
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents txtText As System.Windows.Forms.TextBox
        Friend WithEvents stmStatus As Tools.WindowsT.FormsT.StatusMarker
    End Class
#End If
End Namespace