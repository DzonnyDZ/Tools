Imports Tools.ReflectionT.ReflectionTools, MBox = Tools.WindowsT.IndependentT.MessageBox
Namespace ReflectionT
    ''' <summary>Tests for <see cref="Tools.ReflectionT.ReflectionTools.FindBestFitCastOperator"/></summary>
    Public Class frmFindBestFitCastOperator
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmFindBestFitCastOperator
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
            Dim MyAsm = GetType(frmFindBestFitCastOperator).Assembly
            For Each assembly In MyAsm.GetReferencedAssemblies
                obBrowser.Objects.Add(assembly)
            Next
        End Sub

        Private Sub tmiOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiOpen.Click
            If ofdOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
                Try
                    Dim asm = Reflection.Assembly.LoadFile(ofdOpen.FileName)
                    obBrowser.Objects.Add(asm)
                Catch ex As Exception
                    MBox.Error_X(ex)
                End Try
            End If
        End Sub

        Private Sub txtType_TextChanged(ByVal sender As TextBox, ByVal e As System.EventArgs) Handles txtTo.TextChanged, txtFrom.TextChanged
            sender.Tag = Nothing
        End Sub

        Private Sub cmdType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFrom.Click, cmdTo.Click
            If Not TypeOf obBrowser.SelectedItem Is Type Then
                MBox.Show("Select type, please")
                Exit Sub
            End If
            Dim txt As TextBox = If(sender Is cmdFrom, txtFrom, txtTo)
            txt.Text = DirectCast(obBrowser.SelectedItem, Type).AssemblyQualifiedName
            txt.Tag = obBrowser.SelectedItem
        End Sub

        Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
            Dim TFrom As Type = txtFrom.Tag
            Dim TTo As Type = txtTo.Tag
            Try
                If TFrom Is Nothing Then TFrom = Type.GetType(txtFrom.Text)
                If TTo Is Nothing Then TTo = Type.GetType(txtTo.Text)
                Dim op = FindBestFitCastOperator(TFrom, TTo)
                obBrowser.SelectedItem = op
            Catch ex As Exception
                MBox.Error_X(ex)
            End Try
        End Sub
    End Class
End Namespace