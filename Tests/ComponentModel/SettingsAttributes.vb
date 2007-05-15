Imports Tools.ComponentModel, System.ComponentModel
Namespace ComponentModel
    '#If Config <= RC Then Stage conditional compilation of this file is set in Tests.vbproj
    ''' <summary>Test form for <see cref="SettingsInheritDescriptionAttribute"/> and <see cref="SettingsInheritDefaultValueAttribute"/></summary>
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Friend Class frmSettingsAttributes : Inherits Form
        Friend WithEvents pgrMain As System.Windows.Forms.PropertyGrid
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.Resources.ToolsIcon
            pgrMain.SelectedObject = New SettingsWrapper
        End Sub
        ''' <summary>Shows test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmSettingsAttributes
            frm.ShowDialog()
        End Sub
#Region "Designer generated"
        Private Sub InitializeComponent()
            Me.pgrMain = New System.Windows.Forms.PropertyGrid
            Me.SuspendLayout()
            '
            'pgrMain
            '
            Me.pgrMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pgrMain.Location = New System.Drawing.Point(0, 0)
            Me.pgrMain.Name = "pgrMain"
            Me.pgrMain.Size = New System.Drawing.Size(292, 266)
            Me.pgrMain.TabIndex = 0
            '
            'frmSettingsAttributes
            '
            Me.ClientSize = New System.Drawing.Size(292, 266)
            Me.Controls.Add(Me.pgrMain)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmSettingsAttributes"
            Me.ShowInTaskbar = False
            Me.Text = "Testing SettingsInheritAttributes"
            Me.ResumeLayout(False)

        End Sub
#End Region
        ''' <summary>Test wrapper of <see cref="SettingsAttRibutesTestSettings"/></summary>
        Private Class SettingsWrapper
            ''' <summary>Wraps <see cref="SettingsAttributesTestSettings.TestSetting1"/> property</summary>
            <SettingsInheritDescription(GetType(SettingsAttributesTestSettings), "TestSetting1")> _
            <SettingsInheritDefaultValue(GetType(SettingsAttributesTestSettings), "TestSetting1")> _
            Public Property TestSetting1() As String
                Get
                    Return SettingsAttributesTestSettings.Default.TestSetting1
                End Get
                Set(ByVal value As String)
                    SettingsAttributesTestSettings.Default.TestSetting1 = value
                End Set
            End Property
            ''' <summary>Wraps <see cref="SettingsAttributesTestSettings.TestSetting2"/> property</summary>
            <SettingsInheritDescription(GetType(SettingsAttributesTestSettings), "TestSetting2")> _
            <SettingsInheritDefaultValue(GetType(SettingsAttributesTestSettings), "TestSetting2", GetType(Point))> _
            Public Property TestSetting2() As Point
                Get
                    Return SettingsAttributesTestSettings.Default.TestSetting2
                End Get
                Set(ByVal value As Point)
                    SettingsAttributesTestSettings.Default.TestSetting2 = value
                End Set
            End Property
            ''' <summary>Wraps <see cref="SettingsAttributesTestSettings.TestSetting3"/> property</summary>
            <SettingsInheritDescription(GetType(SettingsAttributesTestSettings), "TestSetting3")> _
            <SettingsInheritDefaultValue(GetType(SettingsAttributesTestSettings), "TestSetting3", GetType(System.Collections.Specialized.StringCollection))> _
            Public Property TestSetting3() As System.Collections.Specialized.StringCollection
                Get
                    Return SettingsAttributesTestSettings.Default.TestSetting3
                End Get
                Set(ByVal value As System.Collections.Specialized.StringCollection)
                    SettingsAttributesTestSettings.Default.TestSetting3 = value
                End Set
            End Property
        End Class
    End Class
End Namespace

