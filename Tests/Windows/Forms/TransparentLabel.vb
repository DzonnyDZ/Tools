'#If Config <= Release This conditional compilation is done in Tests.vbproj
Namespace WindowsT.FormsT
    ''' <summary>Tests <see cref="Tools.WindowsT.FormsT.TransparentLabel"/></summary>
    Public Class frmTransparentLabel
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmTransparentLabel
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub

    End Class
End Namespace