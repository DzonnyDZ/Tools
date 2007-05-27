Namespace WindowsT.FormsT
    '#If Config <= RC Then Stage conditional compilation of this file is set in Tests.vbproj
    ''' <summary>Test functionality of <see cref="Tools.WindowsT.FormsT.ExtendedForm"/></summary>
    Public Class frmExtendedForm
        ''' <summary>Runs test GUI</summary>
        Public Shared Sub Test()
            Dim ef As New frmExtendedForm
            ef.ShowDialog()
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