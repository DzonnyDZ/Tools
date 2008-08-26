Imports System.Windows.Forms

Namespace WindowsT.FormsT
    ''' <summary>HTML dialog box</summary>
    Public Class HTMLDialog
        ''' <summary>HTML text shown to the user</summary>
        Public Property TextHtml() As String
            Get
                Return webHTML.DocumentText
            End Get
            Set(ByVal value As String)
                webHTML.DocumentText = value
            End Set
        End Property
        ''' <summary>Default CTor</summary>
        Public Sub New()
            InitializeComponent()
        End Sub
        ''' <summary>CTor with HTML text</summary>
        ''' <param name="TextHtml">HTML text</param>
        Public Sub New(ByVal TextHtml$)
            Me.new()
            Me.TextHtml = TextHtml
        End Sub
        ''' <summary>Shows modal dilog with given HTML</summary>
        ''' <param name="TextHtml">HTML text to be shown</param>
        ''' <param name="owner">Owner of the dialog (can be null)</param>
        ''' <returns>Dialog result</returns>
        Public Shared Function ShowModal(ByVal TextHtml$, Optional ByVal owner As iwin32window = Nothing) As dialogresult
            Dim inst As New HTMLDialog With {.TextHtml = TextHtml}
            Return inst.ShowDialog(owner)
        End Function

        Private Sub webHTML_Navigated(ByVal sender As WebBrowser, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles webHTML.Navigated
            Me.Text = sender.Document.Title
        End Sub
    End Class
End Namespace