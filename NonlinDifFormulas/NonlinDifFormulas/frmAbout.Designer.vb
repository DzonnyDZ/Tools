<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbout
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

    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents LabelProductName As System.Windows.Forms.Label
    Friend WithEvents LabelVersion As System.Windows.Forms.Label
    Friend WithEvents LabelCompanyName As System.Windows.Forms.Label
    Friend WithEvents TextBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents LabelCopyright As System.Windows.Forms.Label

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Dim TextItem1 As Tools.Windows.Forms.LinkLabel.TextItem = New Tools.Windows.Forms.LinkLabel.TextItem
        Dim AutoLink1 As Tools.Windows.Forms.LinkLabel.AutoLink = New Tools.Windows.Forms.LinkLabel.AutoLink
        Dim TextItem2 As Tools.Windows.Forms.LinkLabel.TextItem = New Tools.Windows.Forms.LinkLabel.TextItem
        Dim AutoLink2 As Tools.Windows.Forms.LinkLabel.AutoLink = New Tools.Windows.Forms.LinkLabel.AutoLink
        Dim TextItem3 As Tools.Windows.Forms.LinkLabel.TextItem = New Tools.Windows.Forms.LinkLabel.TextItem
        Dim AutoLink3 As Tools.Windows.Forms.LinkLabel.AutoLink = New Tools.Windows.Forms.LinkLabel.AutoLink
        Dim TextItem4 As Tools.Windows.Forms.LinkLabel.TextItem = New Tools.Windows.Forms.LinkLabel.TextItem
        Dim AutoLink4 As Tools.Windows.Forms.LinkLabel.AutoLink = New Tools.Windows.Forms.LinkLabel.AutoLink
        Dim TextItem5 As Tools.Windows.Forms.LinkLabel.TextItem = New Tools.Windows.Forms.LinkLabel.TextItem
        Dim AutoLink5 As Tools.Windows.Forms.LinkLabel.AutoLink = New Tools.Windows.Forms.LinkLabel.AutoLink
        Dim TextItem6 As Tools.Windows.Forms.LinkLabel.TextItem = New Tools.Windows.Forms.LinkLabel.TextItem
        Dim AutoLink6 As Tools.Windows.Forms.LinkLabel.AutoLink = New Tools.Windows.Forms.LinkLabel.AutoLink
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox
        Me.LabelProductName = New System.Windows.Forms.Label
        Me.LabelVersion = New System.Windows.Forms.Label
        Me.LabelCopyright = New System.Windows.Forms.Label
        Me.LabelCompanyName = New System.Windows.Forms.Label
        Me.TextBoxDescription = New System.Windows.Forms.TextBox
        Me.OKButton = New System.Windows.Forms.Button
        Me.LinkLabel1 = New Tools.Windows.Forms.LinkLabel
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.ColumnCount = 2
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.24242!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.75758!))
        Me.TableLayoutPanel.Controls.Add(Me.LogoPictureBox, 0, 4)
        Me.TableLayoutPanel.Controls.Add(Me.LabelProductName, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.LabelVersion, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.LabelCopyright, 1, 2)
        Me.TableLayoutPanel.Controls.Add(Me.LabelCompanyName, 1, 3)
        Me.TableLayoutPanel.Controls.Add(Me.TextBoxDescription, 1, 4)
        Me.TableLayoutPanel.Controls.Add(Me.OKButton, 1, 5)
        Me.TableLayoutPanel.Controls.Add(Me.LinkLabel1, 0, 0)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel.Location = New System.Drawing.Point(9, 9)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 6
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel.Size = New System.Drawing.Size(396, 258)
        Me.TableLayoutPanel.TabIndex = 0
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LogoPictureBox.Image = Global.NonlinDifFormulas.My.Resources.Resources.ÈVUT1
        Me.LogoPictureBox.Location = New System.Drawing.Point(3, 103)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.TableLayoutPanel.SetRowSpan(Me.LogoPictureBox, 2)
        Me.LogoPictureBox.Size = New System.Drawing.Size(188, 152)
        Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.LogoPictureBox.TabIndex = 0
        Me.LogoPictureBox.TabStop = False
        '
        'LabelProductName
        '
        Me.LabelProductName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelProductName.Location = New System.Drawing.Point(200, 0)
        Me.LabelProductName.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
        Me.LabelProductName.MaximumSize = New System.Drawing.Size(0, 17)
        Me.LabelProductName.Name = "LabelProductName"
        Me.LabelProductName.Size = New System.Drawing.Size(193, 17)
        Me.LabelProductName.TabIndex = 0
        Me.LabelProductName.Text = "Product Name"
        Me.LabelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelVersion
        '
        Me.LabelVersion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelVersion.Location = New System.Drawing.Point(200, 25)
        Me.LabelVersion.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
        Me.LabelVersion.MaximumSize = New System.Drawing.Size(0, 17)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New System.Drawing.Size(193, 17)
        Me.LabelVersion.TabIndex = 0
        Me.LabelVersion.Text = "Version"
        Me.LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelCopyright
        '
        Me.LabelCopyright.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelCopyright.Location = New System.Drawing.Point(200, 50)
        Me.LabelCopyright.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
        Me.LabelCopyright.MaximumSize = New System.Drawing.Size(0, 17)
        Me.LabelCopyright.Name = "LabelCopyright"
        Me.LabelCopyright.Size = New System.Drawing.Size(193, 17)
        Me.LabelCopyright.TabIndex = 0
        Me.LabelCopyright.Text = "Copyright"
        Me.LabelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelCompanyName
        '
        Me.LabelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelCompanyName.Location = New System.Drawing.Point(200, 75)
        Me.LabelCompanyName.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
        Me.LabelCompanyName.MaximumSize = New System.Drawing.Size(0, 17)
        Me.LabelCompanyName.Name = "LabelCompanyName"
        Me.LabelCompanyName.Size = New System.Drawing.Size(193, 17)
        Me.LabelCompanyName.TabIndex = 0
        Me.LabelCompanyName.Text = "Company Name"
        Me.LabelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBoxDescription
        '
        Me.TextBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxDescription.Location = New System.Drawing.Point(200, 103)
        Me.TextBoxDescription.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.TextBoxDescription.Multiline = True
        Me.TextBoxDescription.Name = "TextBoxDescription"
        Me.TextBoxDescription.ReadOnly = True
        Me.TextBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxDescription.Size = New System.Drawing.Size(193, 123)
        Me.TextBoxDescription.TabIndex = 0
        Me.TextBoxDescription.TabStop = False
        Me.TextBoxDescription.Text = resources.GetString("TextBoxDescription.Text")
        '
        'OKButton
        '
        Me.OKButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.OKButton.Location = New System.Drawing.Point(318, 232)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(75, 23)
        Me.OKButton.TabIndex = 0
        Me.OKButton.Text = "&OK"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        TextItem1.Text = "Vytvoøeno jako semestrální práce na pøedmìt "
        AutoLink1.LinkURI = New System.Uri("https://www.feld.cvut.cz/education/bk/predmety/01/58/p15894.html", System.UriKind.Absolute)
        AutoLink1.Text = "X02F2P (Fyzika pro výpoèetní techniku)"
        TextItem2.Text = " na "
        AutoLink2.LinkURI = New System.Uri("http://www.fel.cvut.cz", System.UriKind.Absolute)
        AutoLink2.Text = "Fakultì elektrotechnické"
        TextItem3.Text = " "
        AutoLink3.LinkURI = New System.Uri("http://www.cvut.cz", System.UriKind.Absolute)
        AutoLink3.Text = "Èeského vysokého uèení technického v Praze"
        TextItem4.Text = ", "
        AutoLink4.LinkURI = New System.Uri("http://www.aldebaran.cz", System.UriKind.Absolute)
        AutoLink4.Text = "Katedra fyziky"
        TextItem5.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        AutoLink5.LinkURI = New System.Uri("mailto:zarubj1@fel.cvut.cz", System.UriKind.Absolute)
        AutoLink5.Text = "Bc. Jan Záruba"
        TextItem6.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        AutoLink6.LinkURI = New System.Uri("mailto:veselm4@fel.cvut.cz", System.UriKind.Absolute)
        AutoLink6.Text = "Bc. Martin Veselý"
        Me.LinkLabel1.Items.Add(TextItem1)
        Me.LinkLabel1.Items.Add(AutoLink1)
        Me.LinkLabel1.Items.Add(TextItem2)
        Me.LinkLabel1.Items.Add(AutoLink2)
        Me.LinkLabel1.Items.Add(TextItem3)
        Me.LinkLabel1.Items.Add(AutoLink3)
        Me.LinkLabel1.Items.Add(TextItem4)
        Me.LinkLabel1.Items.Add(AutoLink4)
        Me.LinkLabel1.Items.Add(TextItem5)
        Me.LinkLabel1.Items.Add(AutoLink5)
        Me.LinkLabel1.Items.Add(TextItem6)
        Me.LinkLabel1.Items.Add(AutoLink6)
        Me.LinkLabel1.Location = New System.Drawing.Point(3, 0)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.TableLayoutPanel.SetRowSpan(Me.LinkLabel1, 4)
        Me.LinkLabel1.Size = New System.Drawing.Size(188, 100)
        Me.LinkLabel1.TabIndex = 1
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.UseCompatibleTextRendering = True
        '
        'frmAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.OKButton
        Me.ClientSize = New System.Drawing.Size(414, 276)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.Padding = New System.Windows.Forms.Padding(9)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmAbout"
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.TableLayoutPanel.PerformLayout()
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LinkLabel1 As Tools.Windows.Forms.LinkLabel

End Class
