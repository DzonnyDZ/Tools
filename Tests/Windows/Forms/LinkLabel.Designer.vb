Namespace Windows.Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmLinkLabel
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
            Dim TextItem1 As Tools.Windows.Forms.LinkLabel.TextItem = New Tools.Windows.Forms.LinkLabel.TextItem
            Dim TextItem2 As Tools.Windows.Forms.LinkLabel.TextItem = New Tools.Windows.Forms.LinkLabel.TextItem
            Dim LinkItem1 As Tools.Windows.Forms.LinkLabel.LinkItem = New Tools.Windows.Forms.LinkLabel.LinkItem
            Dim TextItem3 As Tools.Windows.Forms.LinkLabel.TextItem = New Tools.Windows.Forms.LinkLabel.TextItem
            Dim TextItem4 As Tools.Windows.Forms.LinkLabel.TextItem = New Tools.Windows.Forms.LinkLabel.TextItem
            Me.llbLabel = New Tools.Windows.Forms.LinkLabel
            Me.pgrLabel = New System.Windows.Forms.PropertyGrid
            Me.SuspendLayout()
            '
            'llbLabel
            '
            Me.llbLabel.Dock = System.Windows.Forms.DockStyle.Fill
            TextItem1.Text = "This is smart LinkLabel from ÐTools." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "It contains link to "
            TextItem2.Text = " and link to run "
            LinkItem1.LinkData = "notepad.exe"
            LinkItem1.Text = "notepad"
            TextItem3.Text = "."
            TextItem4.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "That's all."
            Me.llbLabel.Items.Add(TextItem1)
            Me.llbLabel.Items.Add(TextItem2)
            Me.llbLabel.Items.Add(LinkItem1)
            Me.llbLabel.Items.Add(TextItem3)
            Me.llbLabel.Items.Add(TextItem4)
            Me.llbLabel.Location = New System.Drawing.Point(0, 0)
            Me.llbLabel.Name = "llbLabel"
            Me.llbLabel.Size = New System.Drawing.Size(200, 564)
            Me.llbLabel.TabIndex = 2
            Me.llbLabel.TabStop = True
            Me.llbLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            Me.llbLabel.UseCompatibleTextRendering = True
            '
            'pgrLabel
            '
            Me.pgrLabel.Dock = System.Windows.Forms.DockStyle.Right
            Me.pgrLabel.Location = New System.Drawing.Point(200, 0)
            Me.pgrLabel.Name = "pgrLabel"
            Me.pgrLabel.SelectedObject = Me.llbLabel
            Me.pgrLabel.Size = New System.Drawing.Size(356, 564)
            Me.pgrLabel.TabIndex = 1
            '
            'frmLinkLabel
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(556, 564)
            Me.Controls.Add(Me.llbLabel)
            Me.Controls.Add(Me.pgrLabel)
            Me.Name = "frmLinkLabel"
            Me.ShowInTaskbar = False
            Me.Text = "Testing Tools.Windows.Forms.LinkLabel"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents pgrLabel As System.Windows.Forms.PropertyGrid
        Friend WithEvents llbLabel As Tools.Windows.Forms.LinkLabel
    End Class
End Namespace