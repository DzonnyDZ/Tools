Namespace Windows.Forms
    ''' <summary>Tests <see cref="Tools.Windows.Forms.TransparentTextBox"/></summary>
    Public Class frmTransparentTextBox
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmTransparentTextBox
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.Resources.ToolsIcon
        End Sub

    End Class
End Namespace