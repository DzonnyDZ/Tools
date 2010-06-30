Imports System.Windows.Forms
Namespace WindowsT.FormsT
    'ASAP: Wiki, forum, mark
    ''' <summary>Represents about dialog for application</summary>
    ''' <remarks>You can use it as is or derive it</remarks>
    Public Class AboutDialog
        Public Sub New()
            InitializeComponent()

            Me.lblProductName.Text = My.Application.Info.ProductName
            Me.lblTitle.Text = My.Application.Info.Title
            Me.lblVersion.Text = My.Application.Info.Version.ToString
            Me.lblCompany.Text = My.Application.Info.CompanyName
            Me.lblCopyright.Text = My.Application.Info.Copyright
            Me.lblAssembly.Text = My.Application.Info.AssemblyName
            Me.lblDescription.Text = My.Application.Info.Description

            Me.Text = String.Format(WindowsT.FormsT.Dialogs.About0, My.Application.Info.Title)

            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End Sub
        ''' <summary>Displays a modal dialog</summary>
        ''' <param name="owner">Window, dialog will be modal to, or null</param>
        ''' <returns>Result of dialog. <see cref="DialogResult.OK"/> when user clicked on OK button, pressed Enter or Escape; <see cref="DialogResult.Cancel"/> when window was closed another way (e.g. the close (X) button in top right corner)</returns>
        ''' <version version="1.5.3">Fix: Always returns <see cref="DialogResult.None"/>.</version>
        Public Shared Function ShowModalDialog(Optional ByVal owner As IWin32Window = Nothing) As Windows.Forms.DialogResult
            Dim inst As New AboutDialog
            Return inst.ShowDialog(owner)
        End Function

        Private Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        
    End Class
End Namespace