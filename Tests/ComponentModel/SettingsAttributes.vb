Imports Tools.ComponentModel, System.ComponentModel
Namespace ComponentModel
    ''' <summary>Test form for <see cref="SettingsInheritDescriptionAttribute"/> and <see cref="SettingsInheritDefaultValueAttribute"/></summary>
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Public Class frmSettingsAttributes : Inherits Form
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.Resources.ToolsIcon
        End Sub
        ''' <summary>Shows test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmSettingsAttributes
            frm.ShowDialog()
        End Sub
#Region "Designer generated"
        Private Sub InitializeComponent()
            Me.SuspendLayout()
            '
            'frmSettingsAttributes
            '
            Me.ClientSize = New System.Drawing.Size(292, 266)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmSettingsAttributes"
            Me.ShowInTaskbar = False
            Me.Text = "Testing SettingsInheritAttributes"
            Me.ResumeLayout(False)

        End Sub
#End Region
    End Class
End Namespace

Partial Friend NotInheritable Class SettingsAttributes
End Class