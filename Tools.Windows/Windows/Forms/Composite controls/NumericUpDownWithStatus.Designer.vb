Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class NumericUpDownWithStatus
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
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
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
            Me.stmStatus = New Tools.WindowsT.FormsT.StatusMarker
            Me.nudNumber = New System.Windows.Forms.NumericUpDown
            Me.tlpMain.SuspendLayout()
            CType(Me.nudNumber, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'tlpMain
            '
            Me.tlpMain.AutoSize = True
            Me.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMain.ColumnCount = 2
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMain.Controls.Add(Me.stmStatus, 1, 0)
            Me.tlpMain.Controls.Add(Me.nudNumber, 0, 0)
            Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMain.Location = New System.Drawing.Point(0, 0)
            Me.tlpMain.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpMain.Name = "tlpMain"
            Me.tlpMain.RowCount = 1
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.Size = New System.Drawing.Size(259, 24)
            Me.tlpMain.TabIndex = 0
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
            'nudNumber
            '
            Me.nudNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.nudNumber.Location = New System.Drawing.Point(0, 2)
            Me.nudNumber.Margin = New System.Windows.Forms.Padding(0)
            Me.nudNumber.Name = "nudNumber"
            Me.nudNumber.Size = New System.Drawing.Size(235, 20)
            Me.nudNumber.TabIndex = 2
            '
            'NumericUpDownWithStatus
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.tlpMain)
            Me.MinimumSize = New System.Drawing.Size(40, 20)
            Me.Name = "NumericUpDownWithStatus"
            Me.Size = New System.Drawing.Size(259, 24)
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            CType(Me.nudNumber, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents stmStatus As Tools.WindowsT.FormsT.StatusMarker
        Friend WithEvents nudNumber As System.Windows.Forms.NumericUpDown
    End Class
End Namespace