Imports System.DirectoryServices, System.Text
Namespace Login
    Friend Class frmGroupOverride
        ''' <summary>CTor</summary>
        ''' <param name="Groups">Skupiny na které má uživatel právo</param>
        Public Sub New(ByVal Groups As List(Of String))

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.DialogResult = Windows.Forms.DialogResult.Cancel

            clbGroups.Items.Clear()
            For Each Item As String In Groups
                clbGroups.Items.Add(Item, True)
            Next Item

        End Sub

        Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        Private Sub cmdKO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKO.Click
            Me.Close()
        End Sub
    End Class
End Namespace