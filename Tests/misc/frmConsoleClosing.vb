Imports MBox = Tools.WindowsT.IndependentT.MessageBox
Public Class frmConsoleClosing
    ''' <summary>CTor</summary>
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = Tools.ResourcesT.ToolsIcon


    End Sub
    ''' <summary>Runs test</summary>
    Public Shared Sub Test()
        Dim frm As New frmConsoleClosing
        frm.ShowDialog()
    End Sub

    Private Sub cmdAllocate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAllocate.Click
        Try
            ConsoleT.AllocateConsole()
        Catch ex As Exception
            MBox.Error_X(ex)
        End Try
    End Sub

    Private Sub ConsoleT_Closing(ByVal sender As Object, ByVal e As ConsoleT.ConsoleClosingEventArgs)
        e.Cancel = IsCancleClosingChecked()
        If IsDetachCLosingChecked() Then DetachConsole()
    End Sub

    Private Function IsCancleClosingChecked() As Boolean
        If Me.InvokeRequired Then Return Me.Invoke(New Func(Of Boolean)(AddressOf IsCancleClosingChecked))
        Return chkCancelClosing.Checked
    End Function

    Private Function IsDetachCLosingChecked() As Boolean
        If Me.InvokeRequired Then Return Me.Invoke(New Func(Of Boolean)(AddressOf IsDetachCLosingChecked))
        Return chkDetachOnClosing.Checked
    End Function

    Private Sub DetachConsole()
        If Me.InvokeRequired Then Me.Invoke(New Action(AddressOf DetachConsole))
        ConsoleT.DetachConsole()
    End Sub

    Private Sub cmdDealoccate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDealoccate.Click
        Try
            ConsoleT.DetachConsole()
        Catch ex As Exception
            MBox.Error_X(ex)
        End Try
    End Sub

    Private Sub cmdAddHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddHandler.Click
        Try
            AddHandler ConsoleT.Closing, AddressOf ConsoleT_Closing
        Catch ex As Exception
            MBox.Error_X(ex)
        End Try
    End Sub

    Private Sub cmdRemoveHandler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveHandler.Click
        Try
            RemoveHandler ConsoleT.Closing, AddressOf ConsoleT_Closing
        Catch ex As Exception
            MBox.Error_X(ex)
        End Try
    End Sub

    Private Sub cmdWrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWrite.Click
        Try
            Console.Write(txtWrite.Text)
        Catch ex As Exception
            MBox.Error_X(ex)
        End Try
    End Sub

    Private Sub cmdWriteLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWriteLine.Click
        Try
            Console.WriteLine(txtWrite.Text)
        Catch ex As Exception
            MBox.Error_X(ex)
        End Try
    End Sub
End Class