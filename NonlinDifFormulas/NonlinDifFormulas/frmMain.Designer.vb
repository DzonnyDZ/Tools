<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim tslTest As System.Windows.Forms.ToolStripLabel
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.tscMain = New System.Windows.Forms.ToolStripContainer
        Me.picMain = New System.Windows.Forms.PictureBox
        Me.tosRceAdd = New System.Windows.Forms.ToolStrip
        Me.tslUloženéRovnice = New System.Windows.Forms.ToolStripLabel
        Me.tsbAdd = New System.Windows.Forms.ToolStripButton
        Me.tsbDel = New System.Windows.Forms.ToolStripButton
        Me.tosRovnice = New System.Windows.Forms.ToolStrip
        Me.tosMěřítko = New System.Windows.Forms.ToolStrip
        Me.tslMěřítko = New System.Windows.Forms.ToolStripLabel
        Me.tslXmin = New System.Windows.Forms.ToolStripLabel
        Me.tstXmin = New System.Windows.Forms.ToolStripTextBox
        Me.tslXmax = New System.Windows.Forms.ToolStripLabel
        Me.tstXmax = New System.Windows.Forms.ToolStripTextBox
        Me.tslYmin = New System.Windows.Forms.ToolStripLabel
        Me.tstYmin = New System.Windows.Forms.ToolStripTextBox
        Me.tslYmax = New System.Windows.Forms.ToolStripLabel
        Me.tstYmax = New System.Windows.Forms.ToolStripTextBox
        Me.tosZadání = New System.Windows.Forms.ToolStrip
        Me.tslRovnice = New System.Windows.Forms.ToolStripLabel
        Me.tslNázev = New System.Windows.Forms.ToolStripLabel
        Me.tstNázev = New System.Windows.Forms.ToolStripTextBox
        Me.tslDx = New System.Windows.Forms.ToolStripLabel
        Me.tstDx = New System.Windows.Forms.ToolStripTextBox
        Me.tslDy = New System.Windows.Forms.ToolStripLabel
        Me.tstDy = New System.Windows.Forms.ToolStripTextBox
        Me.tosPodmínky = New System.Windows.Forms.ToolStrip
        Me.tslPočátečníPodmínky = New System.Windows.Forms.ToolStripLabel
        Me.tslPPx = New System.Windows.Forms.ToolStripLabel
        Me.tstPPx = New System.Windows.Forms.ToolStripTextBox
        Me.tslPPy = New System.Windows.Forms.ToolStripLabel
        Me.tstPPy = New System.Windows.Forms.ToolStripTextBox
        Me.mnsMain = New System.Windows.Forms.MenuStrip
        Me.mniSoubor = New System.Windows.Forms.ToolStripMenuItem
        Me.mniOtevřít = New System.Windows.Forms.ToolStripMenuItem
        Me.mniUložit = New System.Windows.Forms.ToolStripMenuItem
        Me.mniUložitJako = New System.Windows.Forms.ToolStripMenuItem
        Me.mniNový = New System.Windows.Forms.ToolStripMenuItem
        Me.tssSoubor1 = New System.Windows.Forms.ToolStripSeparator
        Me.tmiKonec = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiVykreslit = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiNápověda = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiAbout = New System.Windows.Forms.ToolStripMenuItem
        Me.ofdOpen = New System.Windows.Forms.OpenFileDialog
        Me.sfdSave = New System.Windows.Forms.SaveFileDialog
        Me.tslTmin = New System.Windows.Forms.ToolStripLabel
        Me.tstTmin = New System.Windows.Forms.ToolStripTextBox
        Me.tslTmax = New System.Windows.Forms.ToolStripLabel
        Me.tstTmax = New System.Windows.Forms.ToolStripTextBox
        Me.tslΔt = New System.Windows.Forms.ToolStripLabel
        Me.tstΔt = New System.Windows.Forms.ToolStripTextBox
        tslTest = New System.Windows.Forms.ToolStripLabel
        Me.tscMain.ContentPanel.SuspendLayout()
        Me.tscMain.LeftToolStripPanel.SuspendLayout()
        Me.tscMain.RightToolStripPanel.SuspendLayout()
        Me.tscMain.TopToolStripPanel.SuspendLayout()
        Me.tscMain.SuspendLayout()
        CType(Me.picMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tosRceAdd.SuspendLayout()
        Me.tosRovnice.SuspendLayout()
        Me.tosMěřítko.SuspendLayout()
        Me.tosZadání.SuspendLayout()
        Me.tosPodmínky.SuspendLayout()
        Me.mnsMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tslTest
        '
        tslTest.Name = "tslTest"
        tslTest.Size = New System.Drawing.Size(95, 13)
        tslTest.Text = "test"
        '
        'tscMain
        '
        '
        'tscMain.ContentPanel
        '
        Me.tscMain.ContentPanel.Controls.Add(Me.picMain)
        Me.tscMain.ContentPanel.Size = New System.Drawing.Size(523, 616)
        Me.tscMain.Dock = System.Windows.Forms.DockStyle.Fill
        '
        'tscMain.LeftToolStripPanel
        '
        Me.tscMain.LeftToolStripPanel.Controls.Add(Me.tosRceAdd)
        Me.tscMain.LeftToolStripPanel.Controls.Add(Me.tosRovnice)
        Me.tscMain.Location = New System.Drawing.Point(0, 0)
        Me.tscMain.Name = "tscMain"
        '
        'tscMain.RightToolStripPanel
        '
        Me.tscMain.RightToolStripPanel.Controls.Add(Me.tosMěřítko)
        Me.tscMain.RightToolStripPanel.Controls.Add(Me.tosZadání)
        Me.tscMain.RightToolStripPanel.Controls.Add(Me.tosPodmínky)
        Me.tscMain.Size = New System.Drawing.Size(743, 640)
        Me.tscMain.TabIndex = 0
        Me.tscMain.Text = "ToolStripContainer1"
        '
        'tscMain.TopToolStripPanel
        '
        Me.tscMain.TopToolStripPanel.Controls.Add(Me.mnsMain)
        '
        'picMain
        '
        Me.picMain.BackColor = System.Drawing.Color.Black
        Me.picMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picMain.Location = New System.Drawing.Point(0, 0)
        Me.picMain.Name = "picMain"
        Me.picMain.Size = New System.Drawing.Size(523, 616)
        Me.picMain.TabIndex = 0
        Me.picMain.TabStop = False
        '
        'tosRceAdd
        '
        Me.tosRceAdd.Dock = System.Windows.Forms.DockStyle.None
        Me.tosRceAdd.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tosRceAdd.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslUloženéRovnice, Me.tsbAdd, Me.tsbDel})
        Me.tosRceAdd.Location = New System.Drawing.Point(0, 3)
        Me.tosRceAdd.Name = "tosRceAdd"
        Me.tosRceAdd.ShowItemToolTips = False
        Me.tosRceAdd.Size = New System.Drawing.Size(98, 64)
        Me.tosRceAdd.TabIndex = 1
        '
        'tslUloženéRovnice
        '
        Me.tslUloženéRovnice.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tslUloženéRovnice.Name = "tslUloženéRovnice"
        Me.tslUloženéRovnice.Size = New System.Drawing.Size(96, 13)
        Me.tslUloženéRovnice.Text = "Uložené rovnice"
        '
        'tsbAdd
        '
        Me.tsbAdd.Image = Global.NonlinDifFormulas.My.Resources.Resources.NewCardHS
        Me.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbAdd.Name = "tsbAdd"
        Me.tsbAdd.Size = New System.Drawing.Size(96, 20)
        Me.tsbAdd.Text = "&Přidat"
        '
        'tsbDel
        '
        Me.tsbDel.Enabled = False
        Me.tsbDel.Image = Global.NonlinDifFormulas.My.Resources.Resources.DeleteHS
        Me.tsbDel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbDel.Name = "tsbDel"
        Me.tsbDel.Size = New System.Drawing.Size(96, 20)
        Me.tsbDel.Text = "&Odstranit"
        '
        'tosRovnice
        '
        Me.tosRovnice.AutoSize = False
        Me.tosRovnice.Dock = System.Windows.Forms.DockStyle.None
        Me.tosRovnice.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tosRovnice.Items.AddRange(New System.Windows.Forms.ToolStripItem() {tslTest})
        Me.tosRovnice.Location = New System.Drawing.Point(0, 83)
        Me.tosRovnice.Name = "tosRovnice"
        Me.tosRovnice.ShowItemToolTips = False
        Me.tosRovnice.Size = New System.Drawing.Size(97, 530)
        Me.tosRovnice.TabIndex = 2
        '
        'tosMěřítko
        '
        Me.tosMěřítko.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tosMěřítko.AutoSize = False
        Me.tosMěřítko.CanOverflow = False
        Me.tosMěřítko.Dock = System.Windows.Forms.DockStyle.None
        Me.tosMěřítko.Enabled = False
        Me.tosMěřítko.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tosMěřítko.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslMěřítko, Me.tslXmin, Me.tstXmin, Me.tslXmax, Me.tstXmax, Me.tslYmin, Me.tstYmin, Me.tslYmax, Me.tstYmax})
        Me.tosMěřítko.Location = New System.Drawing.Point(0, 3)
        Me.tosMěřítko.Name = "tosMěřítko"
        Me.tosMěřítko.ShowItemToolTips = False
        Me.tosMěřítko.Size = New System.Drawing.Size(122, 185)
        Me.tosMěřítko.TabIndex = 0
        '
        'tslMěřítko
        '
        Me.tslMěřítko.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tslMěřítko.Name = "tslMěřítko"
        Me.tslMěřítko.Size = New System.Drawing.Size(120, 13)
        Me.tslMěřítko.Text = "Měřítko"
        '
        'tslXmin
        '
        Me.tslXmin.Name = "tslXmin"
        Me.tslXmin.Size = New System.Drawing.Size(120, 13)
        Me.tslXmin.Text = "X-min"
        '
        'tstXmin
        '
        Me.tstXmin.MaxLength = 50
        Me.tstXmin.Name = "tstXmin"
        Me.tstXmin.Size = New System.Drawing.Size(118, 21)
        Me.tstXmin.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tslXmax
        '
        Me.tslXmax.Name = "tslXmax"
        Me.tslXmax.Size = New System.Drawing.Size(120, 13)
        Me.tslXmax.Text = "X-max"
        '
        'tstXmax
        '
        Me.tstXmax.MaxLength = 50
        Me.tstXmax.Name = "tstXmax"
        Me.tstXmax.Size = New System.Drawing.Size(118, 21)
        Me.tstXmax.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tslYmin
        '
        Me.tslYmin.Name = "tslYmin"
        Me.tslYmin.Size = New System.Drawing.Size(120, 13)
        Me.tslYmin.Text = "Y-min"
        '
        'tstYmin
        '
        Me.tstYmin.MaxLength = 50
        Me.tstYmin.Name = "tstYmin"
        Me.tstYmin.Size = New System.Drawing.Size(118, 21)
        Me.tstYmin.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tslYmax
        '
        Me.tslYmax.Name = "tslYmax"
        Me.tslYmax.Size = New System.Drawing.Size(120, 13)
        Me.tslYmax.Text = "Y-max"
        '
        'tstYmax
        '
        Me.tstYmax.MaxLength = 50
        Me.tstYmax.Name = "tstYmax"
        Me.tstYmax.Size = New System.Drawing.Size(118, 21)
        Me.tstYmax.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tosZadání
        '
        Me.tosZadání.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tosZadání.AutoSize = False
        Me.tosZadání.CanOverflow = False
        Me.tosZadání.Dock = System.Windows.Forms.DockStyle.None
        Me.tosZadání.Enabled = False
        Me.tosZadání.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tosZadání.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslRovnice, Me.tslNázev, Me.tstNázev, Me.tslDx, Me.tstDx, Me.tslDy, Me.tstDy})
        Me.tosZadání.Location = New System.Drawing.Point(0, 189)
        Me.tosZadání.Name = "tosZadání"
        Me.tosZadání.ShowItemToolTips = False
        Me.tosZadání.Size = New System.Drawing.Size(122, 148)
        Me.tosZadání.TabIndex = 0
        '
        'tslRovnice
        '
        Me.tslRovnice.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tslRovnice.Name = "tslRovnice"
        Me.tslRovnice.Size = New System.Drawing.Size(120, 13)
        Me.tslRovnice.Text = "Rovnice"
        '
        'tslNázev
        '
        Me.tslNázev.Name = "tslNázev"
        Me.tslNázev.Size = New System.Drawing.Size(120, 13)
        Me.tslNázev.Text = "Název"
        '
        'tstNázev
        '
        Me.tstNázev.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tstNázev.MaxLength = 50
        Me.tstNázev.Name = "tstNázev"
        Me.tstNázev.Size = New System.Drawing.Size(118, 21)
        Me.tstNázev.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tslDx
        '
        Me.tslDx.Name = "tslDx"
        Me.tslDx.Size = New System.Drawing.Size(120, 13)
        Me.tslDx.Text = "dx"
        '
        'tstDx
        '
        Me.tstDx.Name = "tstDx"
        Me.tstDx.Size = New System.Drawing.Size(118, 21)
        Me.tstDx.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tslDy
        '
        Me.tslDy.Name = "tslDy"
        Me.tslDy.Size = New System.Drawing.Size(120, 13)
        Me.tslDy.Text = "dy"
        '
        'tstDy
        '
        Me.tstDy.Name = "tstDy"
        Me.tstDy.Size = New System.Drawing.Size(118, 21)
        Me.tstDy.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tosPodmínky
        '
        Me.tosPodmínky.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tosPodmínky.CanOverflow = False
        Me.tosPodmínky.Dock = System.Windows.Forms.DockStyle.None
        Me.tosPodmínky.Enabled = False
        Me.tosPodmínky.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tosPodmínky.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslPočátečníPodmínky, Me.tslPPx, Me.tstPPx, Me.tslPPy, Me.tstPPy, Me.tslTmin, Me.tstTmin, Me.tslTmax, Me.tstTmax, Me.tslΔt, Me.tstΔt})
        Me.tosPodmínky.Location = New System.Drawing.Point(0, 338)
        Me.tosPodmínky.Name = "tosPodmínky"
        Me.tosPodmínky.ShowItemToolTips = False
        Me.tosPodmínky.Size = New System.Drawing.Size(121, 222)
        Me.tosPodmínky.TabIndex = 1
        '
        'tslPočátečníPodmínky
        '
        Me.tslPočátečníPodmínky.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.tslPočátečníPodmínky.Name = "tslPočátečníPodmínky"
        Me.tslPočátečníPodmínky.Size = New System.Drawing.Size(119, 13)
        Me.tslPočátečníPodmínky.Text = "Podmínky"
        '
        'tslPPx
        '
        Me.tslPPx.Name = "tslPPx"
        Me.tslPPx.Size = New System.Drawing.Size(119, 13)
        Me.tslPPx.Text = "x"
        '
        'tstPPx
        '
        Me.tstPPx.MaxLength = 50
        Me.tstPPx.Name = "tstPPx"
        Me.tstPPx.Size = New System.Drawing.Size(117, 21)
        Me.tstPPx.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tslPPy
        '
        Me.tslPPy.Name = "tslPPy"
        Me.tslPPy.Size = New System.Drawing.Size(119, 13)
        Me.tslPPy.Text = "y"
        '
        'tstPPy
        '
        Me.tstPPy.MaxLength = 50
        Me.tstPPy.Name = "tstPPy"
        Me.tstPPy.Size = New System.Drawing.Size(117, 21)
        Me.tstPPy.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'mnsMain
        '
        Me.mnsMain.Dock = System.Windows.Forms.DockStyle.None
        Me.mnsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniSoubor, Me.tmiVykreslit, Me.tmiNápověda})
        Me.mnsMain.Location = New System.Drawing.Point(0, 0)
        Me.mnsMain.Name = "mnsMain"
        Me.mnsMain.Size = New System.Drawing.Size(743, 24)
        Me.mnsMain.TabIndex = 0
        Me.mnsMain.Text = "MenuStrip1"
        '
        'mniSoubor
        '
        Me.mniSoubor.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniOtevřít, Me.mniUložit, Me.mniUložitJako, Me.mniNový, Me.tssSoubor1, Me.tmiKonec})
        Me.mniSoubor.Name = "mniSoubor"
        Me.mniSoubor.Size = New System.Drawing.Size(53, 20)
        Me.mniSoubor.Text = "&Soubor"
        '
        'mniOtevřít
        '
        Me.mniOtevřít.Image = Global.NonlinDifFormulas.My.Resources.Resources.openHS
        Me.mniOtevřít.Name = "mniOtevřít"
        Me.mniOtevřít.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.mniOtevřít.Size = New System.Drawing.Size(148, 22)
        Me.mniOtevřít.Text = "&Otevřít"
        '
        'mniUložit
        '
        Me.mniUložit.Image = Global.NonlinDifFormulas.My.Resources.Resources.saveHS
        Me.mniUložit.Name = "mniUložit"
        Me.mniUložit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mniUložit.Size = New System.Drawing.Size(148, 22)
        Me.mniUložit.Text = "&Uložit"
        '
        'mniUložitJako
        '
        Me.mniUložitJako.Name = "mniUložitJako"
        Me.mniUložitJako.Size = New System.Drawing.Size(148, 22)
        Me.mniUložitJako.Text = "Uložit j&ako..."
        '
        'mniNový
        '
        Me.mniNový.Image = Global.NonlinDifFormulas.My.Resources.Resources.NewDocumentHS
        Me.mniNový.Name = "mniNový"
        Me.mniNový.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.mniNový.Size = New System.Drawing.Size(148, 22)
        Me.mniNový.Text = "&Nový"
        '
        'tssSoubor1
        '
        Me.tssSoubor1.Name = "tssSoubor1"
        Me.tssSoubor1.Size = New System.Drawing.Size(145, 6)
        '
        'tmiKonec
        '
        Me.tmiKonec.Name = "tmiKonec"
        Me.tmiKonec.ShortcutKeyDisplayString = "Alt+F4"
        Me.tmiKonec.Size = New System.Drawing.Size(148, 22)
        Me.tmiKonec.Text = "&Konec"
        '
        'tmiVykreslit
        '
        Me.tmiVykreslit.Enabled = False
        Me.tmiVykreslit.ForeColor = System.Drawing.Color.Red
        Me.tmiVykreslit.Name = "tmiVykreslit"
        Me.tmiVykreslit.Size = New System.Drawing.Size(59, 20)
        Me.tmiVykreslit.Text = "&Vykreslit"
        '
        'tmiNápověda
        '
        Me.tmiNápověda.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiAbout})
        Me.tmiNápověda.Name = "tmiNápověda"
        Me.tmiNápověda.Size = New System.Drawing.Size(68, 20)
        Me.tmiNápověda.Text = "&Nápověda"
        '
        'tmiAbout
        '
        Me.tmiAbout.Name = "tmiAbout"
        Me.tmiAbout.Size = New System.Drawing.Size(146, 22)
        Me.tmiAbout.Text = "&O programu ..."
        '
        'ofdOpen
        '
        Me.ofdOpen.DefaultExt = "equa.xml"
        Me.ofdOpen.Filter = "XML soubory rovnic (*.equa.xml)|*.equa.xml|XML soubory (*.xml)|*.xml|Všechny soub" & _
            "ory (*.*)|*.*"
        Me.ofdOpen.SupportMultiDottedExtensions = True
        '
        'sfdSave
        '
        Me.sfdSave.DefaultExt = "equa.xml"
        Me.sfdSave.Filter = "XML soubory rovnic (*.equa.xml)|*.equa.xml|XML soubory (*.xml)|*.xml|Všechny soub" & _
            "ory (*.*)|*.*"
        Me.sfdSave.SupportMultiDottedExtensions = True
        '
        'tslTmin
        '
        Me.tslTmin.Name = "tslTmin"
        Me.tslTmin.Size = New System.Drawing.Size(119, 13)
        Me.tslTmin.Text = "t-min"
        '
        'tstTmin
        '
        Me.tstTmin.Name = "tstTmin"
        Me.tstTmin.Size = New System.Drawing.Size(117, 21)
        '
        'tslTmax
        '
        Me.tslTmax.Name = "tslTmax"
        Me.tslTmax.Size = New System.Drawing.Size(119, 13)
        Me.tslTmax.Text = "t-max"
        '
        'tstTmax
        '
        Me.tstTmax.Name = "tstTmax"
        Me.tstTmax.Size = New System.Drawing.Size(117, 21)
        '
        'tslΔt
        '
        Me.tslΔt.Name = "tslΔt"
        Me.tslΔt.Size = New System.Drawing.Size(119, 13)
        Me.tslΔt.Text = "Δt"
        '
        'tstΔt
        '
        Me.tstΔt.Name = "tstΔt"
        Me.tstΔt.Size = New System.Drawing.Size(117, 21)
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(743, 640)
        Me.Controls.Add(Me.tscMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnsMain
        Me.Name = "frmMain"
        Me.Text = "Diff Equa"
        Me.tscMain.ContentPanel.ResumeLayout(False)
        Me.tscMain.LeftToolStripPanel.ResumeLayout(False)
        Me.tscMain.LeftToolStripPanel.PerformLayout()
        Me.tscMain.RightToolStripPanel.ResumeLayout(False)
        Me.tscMain.RightToolStripPanel.PerformLayout()
        Me.tscMain.TopToolStripPanel.ResumeLayout(False)
        Me.tscMain.TopToolStripPanel.PerformLayout()
        Me.tscMain.ResumeLayout(False)
        Me.tscMain.PerformLayout()
        CType(Me.picMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tosRceAdd.ResumeLayout(False)
        Me.tosRceAdd.PerformLayout()
        Me.tosRovnice.ResumeLayout(False)
        Me.tosRovnice.PerformLayout()
        Me.tosMěřítko.ResumeLayout(False)
        Me.tosMěřítko.PerformLayout()
        Me.tosZadání.ResumeLayout(False)
        Me.tosZadání.PerformLayout()
        Me.tosPodmínky.ResumeLayout(False)
        Me.tosPodmínky.PerformLayout()
        Me.mnsMain.ResumeLayout(False)
        Me.mnsMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tscMain As System.Windows.Forms.ToolStripContainer
    Friend WithEvents tosMěřítko As System.Windows.Forms.ToolStrip
    Friend WithEvents tslMěřítko As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tslXmin As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tslXmax As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tslYmin As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tslYmax As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tstXmin As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tstXmax As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tstYmin As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tstYmax As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tosZadání As System.Windows.Forms.ToolStrip
    Friend WithEvents tslRovnice As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tslNázev As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tstNázev As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tslDx As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tstDx As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tslDy As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tstDy As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tosRceAdd As System.Windows.Forms.ToolStrip
    Friend WithEvents tslUloženéRovnice As System.Windows.Forms.ToolStripLabel
    Friend WithEvents mnsMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mniSoubor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniOtevřít As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniUložit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniUložitJako As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniNový As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiKonec As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiNápověda As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tosPodmínky As System.Windows.Forms.ToolStrip
    Friend WithEvents tslPočátečníPodmínky As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tslPPx As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tstPPx As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tslPPy As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tstPPy As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tssSoubor1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tmiAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tosRovnice As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents picMain As System.Windows.Forms.PictureBox
    Friend WithEvents tmiVykreslit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbDel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ofdOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents sfdSave As System.Windows.Forms.SaveFileDialog
    Friend WithEvents tslTmin As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tstTmin As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tslTmax As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tstTmax As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tslΔt As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tstΔt As System.Windows.Forms.ToolStripTextBox
End Class
