<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTranslateBlockNames
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
        Me.dgwData = New System.Windows.Forms.DataGridView()
        Me.colUcdName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLocName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.tlpMainAndOnly = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.dgwData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpMainAndOnly.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgwData
        '
        Me.dgwData.AllowUserToAddRows = False
        Me.dgwData.AllowUserToDeleteRows = False
        Me.dgwData.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgwData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgwData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colUcdName, Me.colLocName})
        Me.dgwData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgwData.Location = New System.Drawing.Point(3, 3)
        Me.dgwData.Name = "dgwData"
        Me.dgwData.Size = New System.Drawing.Size(710, 337)
        Me.dgwData.TabIndex = 0
        '
        'colUcdName
        '
        Me.colUcdName.DataPropertyName = "UcdName"
        Me.colUcdName.HeaderText = "UCD Name"
        Me.colUcdName.Name = "colUcdName"
        Me.colUcdName.ReadOnly = True
        '
        'colLocName
        '
        Me.colLocName.DataPropertyName = "LocalizedName"
        Me.colLocName.HeaderText = "Localized name"
        Me.colLocName.Name = "colLocName"
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdOK.Location = New System.Drawing.Point(320, 346)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "Done"
        '
        'tlpMainAndOnly
        '
        Me.tlpMainAndOnly.ColumnCount = 1
        Me.tlpMainAndOnly.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMainAndOnly.Controls.Add(Me.dgwData, 0, 0)
        Me.tlpMainAndOnly.Controls.Add(Me.cmdOK, 0, 1)
        Me.tlpMainAndOnly.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMainAndOnly.Location = New System.Drawing.Point(0, 0)
        Me.tlpMainAndOnly.Name = "tlpMainAndOnly"
        Me.tlpMainAndOnly.RowCount = 2
        Me.tlpMainAndOnly.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMainAndOnly.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMainAndOnly.Size = New System.Drawing.Size(716, 372)
        Me.tlpMainAndOnly.TabIndex = 2
        '
        'frmTranslateBlockNames
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(716, 372)
        Me.Controls.Add(Me.tlpMainAndOnly)
        Me.MinimizeBox = False
        Me.Name = "frmTranslateBlockNames"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Block names translator"
        CType(Me.dgwData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpMainAndOnly.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgwData As System.Windows.Forms.DataGridView
    Friend WithEvents colUcdName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLocName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents tlpMainAndOnly As System.Windows.Forms.TableLayoutPanel
End Class
