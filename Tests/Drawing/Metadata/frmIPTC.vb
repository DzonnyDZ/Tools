Namespace DrawingT.MetadataT
    '#If Config <= Nightly Then Stage conditional compilation of this file is set in Tests.vbproj
    ''' <summary>Tests <see cref="Tools.DrawingT.IO.JPEG"/></summary>
    Public Class frmIPTC
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub test()
            Dim frm As New frmIPTC
            frm.Show()
        End Sub
    End Class
End Namespace