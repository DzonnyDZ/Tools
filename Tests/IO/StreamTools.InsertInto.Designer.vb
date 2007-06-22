Namespace IOt.StreamTools
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmInsertInto
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
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
            Me.lblBeforeI = New System.Windows.Forms.Label
            Me.lblBefore2 = New System.Windows.Forms.Label
            Me.txtBefore = New System.Windows.Forms.TextBox
            Me.lblInsertI = New System.Windows.Forms.Label
            Me.lblInsert2 = New System.Windows.Forms.Label
            Me.txtInsert = New System.Windows.Forms.TextBox
            Me.lblAfterI = New System.Windows.Forms.Label
            Me.lblAfter2 = New System.Windows.Forms.Label
            Me.txtAfter = New System.Windows.Forms.TextBox
            Me.lblAfter1 = New System.Windows.Forms.Label
            Me.lblInsert1 = New System.Windows.Forms.Label
            Me.lblBefore1 = New System.Windows.Forms.Label
            Me.flpInsert = New System.Windows.Forms.FlowLayoutPanel
            Me.lblPosition = New System.Windows.Forms.Label
            Me.nudPosition = New System.Windows.Forms.NumericUpDown
            Me.lblBytesToReplace = New System.Windows.Forms.Label
            Me.nudBytesToreplace = New System.Windows.Forms.NumericUpDown
            Me.cmdInsert = New System.Windows.Forms.Button
            Me.tlpMain.SuspendLayout()
            Me.flpInsert.SuspendLayout()
            CType(Me.nudPosition, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.nudBytesToreplace, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'tlpMain
            '
            Me.tlpMain.ColumnCount = 2
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.Controls.Add(Me.lblBeforeI, 0, 0)
            Me.tlpMain.Controls.Add(Me.lblBefore2, 1, 1)
            Me.tlpMain.Controls.Add(Me.txtBefore, 1, 2)
            Me.tlpMain.Controls.Add(Me.lblInsertI, 0, 3)
            Me.tlpMain.Controls.Add(Me.lblInsert2, 1, 4)
            Me.tlpMain.Controls.Add(Me.txtInsert, 1, 5)
            Me.tlpMain.Controls.Add(Me.lblAfterI, 0, 7)
            Me.tlpMain.Controls.Add(Me.lblAfter2, 1, 8)
            Me.tlpMain.Controls.Add(Me.txtAfter, 1, 9)
            Me.tlpMain.Controls.Add(Me.lblAfter1, 1, 7)
            Me.tlpMain.Controls.Add(Me.lblInsert1, 1, 3)
            Me.tlpMain.Controls.Add(Me.lblBefore1, 1, 0)
            Me.tlpMain.Controls.Add(Me.flpInsert, 0, 6)
            Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMain.Location = New System.Drawing.Point(0, 0)
            Me.tlpMain.Name = "tlpMain"
            Me.tlpMain.RowCount = 11
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.Size = New System.Drawing.Size(860, 221)
            Me.tlpMain.TabIndex = 0
            '
            'lblBeforeI
            '
            Me.lblBeforeI.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblBeforeI.AutoSize = True
            Me.lblBeforeI.Location = New System.Drawing.Point(3, 17)
            Me.lblBeforeI.Name = "lblBeforeI"
            Me.tlpMain.SetRowSpan(Me.lblBeforeI, 3)
            Me.lblBeforeI.Size = New System.Drawing.Size(38, 13)
            Me.lblBeforeI.TabIndex = 0
            Me.lblBeforeI.Text = "Before"
            '
            'lblBefore2
            '
            Me.lblBefore2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblBefore2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.lblBefore2.Location = New System.Drawing.Point(44, 15)
            Me.lblBefore2.Margin = New System.Windows.Forms.Padding(0)
            Me.lblBefore2.Name = "lblBefore2"
            Me.lblBefore2.Size = New System.Drawing.Size(816, 15)
            Me.lblBefore2.TabIndex = 1
            Me.lblBefore2.Text = "012345678901234567890123456789012345678901234567890123456789012345678901234567890" & _
                "1234567890123456789"
            '
            'txtBefore
            '
            Me.txtBefore.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtBefore.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.txtBefore.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.txtBefore.Location = New System.Drawing.Point(47, 30)
            Me.txtBefore.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
            Me.txtBefore.MaxLength = 100
            Me.txtBefore.Name = "txtBefore"
            Me.txtBefore.Size = New System.Drawing.Size(810, 15)
            Me.txtBefore.TabIndex = 2
            '
            'lblInsertI
            '
            Me.lblInsertI.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblInsertI.AutoSize = True
            Me.lblInsertI.Location = New System.Drawing.Point(3, 65)
            Me.lblInsertI.Name = "lblInsertI"
            Me.tlpMain.SetRowSpan(Me.lblInsertI, 3)
            Me.lblInsertI.Size = New System.Drawing.Size(38, 13)
            Me.lblInsertI.TabIndex = 3
            Me.lblInsertI.Text = "Insert"
            '
            'lblInsert2
            '
            Me.lblInsert2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblInsert2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.lblInsert2.Location = New System.Drawing.Point(44, 63)
            Me.lblInsert2.Margin = New System.Windows.Forms.Padding(0)
            Me.lblInsert2.Name = "lblInsert2"
            Me.lblInsert2.Size = New System.Drawing.Size(816, 15)
            Me.lblInsert2.TabIndex = 4
            Me.lblInsert2.Text = "012345678901234567890123456789012345678901234567890123456789012345678901234567890" & _
                "1234567890123456789"
            '
            'txtInsert
            '
            Me.txtInsert.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtInsert.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.txtInsert.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.txtInsert.Location = New System.Drawing.Point(47, 78)
            Me.txtInsert.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
            Me.txtInsert.MaxLength = 100
            Me.txtInsert.Name = "txtInsert"
            Me.txtInsert.Size = New System.Drawing.Size(810, 15)
            Me.txtInsert.TabIndex = 5
            '
            'lblAfterI
            '
            Me.lblAfterI.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblAfterI.AutoSize = True
            Me.lblAfterI.Location = New System.Drawing.Point(3, 142)
            Me.lblAfterI.Name = "lblAfterI"
            Me.tlpMain.SetRowSpan(Me.lblAfterI, 3)
            Me.lblAfterI.Size = New System.Drawing.Size(38, 13)
            Me.lblAfterI.TabIndex = 6
            Me.lblAfterI.Text = "After"
            '
            'lblAfter2
            '
            Me.lblAfter2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblAfter2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.lblAfter2.Location = New System.Drawing.Point(44, 140)
            Me.lblAfter2.Margin = New System.Windows.Forms.Padding(0)
            Me.lblAfter2.Name = "lblAfter2"
            Me.lblAfter2.Size = New System.Drawing.Size(816, 15)
            Me.lblAfter2.TabIndex = 7
            Me.lblAfter2.Text = "012345678901234567890123456789012345678901234567890123456789012345678901234567890" & _
                "1234567890123456789"
            '
            'txtAfter
            '
            Me.txtAfter.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtAfter.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.txtAfter.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.txtAfter.Location = New System.Drawing.Point(47, 155)
            Me.txtAfter.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
            Me.txtAfter.MaxLength = 100
            Me.txtAfter.Name = "txtAfter"
            Me.txtAfter.ReadOnly = True
            Me.txtAfter.Size = New System.Drawing.Size(810, 15)
            Me.txtAfter.TabIndex = 8
            '
            'lblAfter1
            '
            Me.lblAfter1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblAfter1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.lblAfter1.Location = New System.Drawing.Point(44, 125)
            Me.lblAfter1.Margin = New System.Windows.Forms.Padding(0)
            Me.lblAfter1.Name = "lblAfter1"
            Me.lblAfter1.Size = New System.Drawing.Size(816, 15)
            Me.lblAfter1.TabIndex = 7
            Me.lblAfter1.Text = "000000000011111111112222222222333333333344444444445555555555666666666677777777778" & _
                "8888888889999999999"
            '
            'lblInsert1
            '
            Me.lblInsert1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblInsert1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.lblInsert1.Location = New System.Drawing.Point(44, 48)
            Me.lblInsert1.Margin = New System.Windows.Forms.Padding(0)
            Me.lblInsert1.Name = "lblInsert1"
            Me.lblInsert1.Size = New System.Drawing.Size(816, 15)
            Me.lblInsert1.TabIndex = 7
            Me.lblInsert1.Text = "000000000011111111112222222222333333333344444444445555555555666666666677777777778" & _
                "8888888889999999999"
            '
            'lblBefore1
            '
            Me.lblBefore1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblBefore1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.lblBefore1.Location = New System.Drawing.Point(44, 0)
            Me.lblBefore1.Margin = New System.Windows.Forms.Padding(0)
            Me.lblBefore1.Name = "lblBefore1"
            Me.lblBefore1.Size = New System.Drawing.Size(816, 15)
            Me.lblBefore1.TabIndex = 7
            Me.lblBefore1.Text = "000000000011111111112222222222333333333344444444445555555555666666666677777777778" & _
                "8888888889999999999"
            '
            'flpInsert
            '
            Me.flpInsert.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.flpInsert.AutoSize = True
            Me.flpInsert.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMain.SetColumnSpan(Me.flpInsert, 2)
            Me.flpInsert.Controls.Add(Me.lblPosition)
            Me.flpInsert.Controls.Add(Me.nudPosition)
            Me.flpInsert.Controls.Add(Me.lblBytesToReplace)
            Me.flpInsert.Controls.Add(Me.nudBytesToreplace)
            Me.flpInsert.Controls.Add(Me.cmdInsert)
            Me.flpInsert.Location = New System.Drawing.Point(0, 96)
            Me.flpInsert.Margin = New System.Windows.Forms.Padding(0)
            Me.flpInsert.Name = "flpInsert"
            Me.flpInsert.Size = New System.Drawing.Size(860, 29)
            Me.flpInsert.TabIndex = 9
            '
            'lblPosition
            '
            Me.lblPosition.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblPosition.AutoSize = True
            Me.lblPosition.Location = New System.Drawing.Point(3, 8)
            Me.lblPosition.Name = "lblPosition"
            Me.lblPosition.Size = New System.Drawing.Size(44, 13)
            Me.lblPosition.TabIndex = 0
            Me.lblPosition.Text = "Position"
            '
            'nudPosition
            '
            Me.nudPosition.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.nudPosition.Location = New System.Drawing.Point(53, 4)
            Me.nudPosition.Name = "nudPosition"
            Me.nudPosition.Size = New System.Drawing.Size(62, 20)
            Me.nudPosition.TabIndex = 1
            '
            'lblBytesToReplace
            '
            Me.lblBytesToReplace.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblBytesToReplace.AutoSize = True
            Me.lblBytesToReplace.Location = New System.Drawing.Point(121, 8)
            Me.lblBytesToReplace.Name = "lblBytesToReplace"
            Me.lblBytesToReplace.Size = New System.Drawing.Size(86, 13)
            Me.lblBytesToReplace.TabIndex = 2
            Me.lblBytesToReplace.Text = "BytesToReplace"
            '
            'nudBytesToreplace
            '
            Me.nudBytesToreplace.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.nudBytesToreplace.Location = New System.Drawing.Point(213, 4)
            Me.nudBytesToreplace.Name = "nudBytesToreplace"
            Me.nudBytesToreplace.Size = New System.Drawing.Size(74, 20)
            Me.nudBytesToreplace.TabIndex = 3
            '
            'cmdInsert
            '
            Me.cmdInsert.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.cmdInsert.AutoSize = True
            Me.cmdInsert.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdInsert.Location = New System.Drawing.Point(293, 3)
            Me.cmdInsert.Name = "cmdInsert"
            Me.cmdInsert.Size = New System.Drawing.Size(43, 23)
            Me.cmdInsert.TabIndex = 4
            Me.cmdInsert.Text = "&Insert"
            Me.cmdInsert.UseVisualStyleBackColor = True
            '
            'frmInsertInto
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(860, 221)
            Me.Controls.Add(Me.tlpMain)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Name = "frmInsertInto"
            Me.Text = "Testing Tools.IOt.StreamTools.InsertInto"
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            Me.flpInsert.ResumeLayout(False)
            Me.flpInsert.PerformLayout()
            CType(Me.nudPosition, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.nudBytesToreplace, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents lblBeforeI As System.Windows.Forms.Label
        Friend WithEvents lblBefore2 As System.Windows.Forms.Label
        Friend WithEvents txtBefore As System.Windows.Forms.TextBox
        Friend WithEvents lblInsertI As System.Windows.Forms.Label
        Friend WithEvents txtInsert As System.Windows.Forms.TextBox
        Friend WithEvents lblAfterI As System.Windows.Forms.Label
        Friend WithEvents lblAfter2 As System.Windows.Forms.Label
        Friend WithEvents txtAfter As System.Windows.Forms.TextBox
        Friend WithEvents lblAfter1 As System.Windows.Forms.Label
        Friend WithEvents lblInsert2 As System.Windows.Forms.Label
        Friend WithEvents lblInsert1 As System.Windows.Forms.Label
        Friend WithEvents lblBefore1 As System.Windows.Forms.Label
        Friend WithEvents flpInsert As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents lblPosition As System.Windows.Forms.Label
        Friend WithEvents lblBytesToReplace As System.Windows.Forms.Label
        Friend WithEvents cmdInsert As System.Windows.Forms.Button
        Friend WithEvents nudPosition As System.Windows.Forms.NumericUpDown
        Friend WithEvents nudBytesToreplace As System.Windows.Forms.NumericUpDown
    End Class
End Namespace