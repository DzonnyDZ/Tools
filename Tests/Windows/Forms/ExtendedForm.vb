Namespace Windows.Forms
#If Config <= RC Then
    ''' <summary>Test functionality of <see cref="Tools.Windows.Forms.ExtendedForm"/></summary>
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
            Me.Icon = Tools.Resources.ToolsIcon
        End Sub
    End Class
#End If
End Namespace