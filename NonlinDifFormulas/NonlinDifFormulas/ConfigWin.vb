Public Class ConfigWin



    Private Sub btnRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRun.Click
        Dim Parser As SyntaktickyAnalyzator.Analyzer
        'Try
        Parser = New SyntaktickyAnalyzator.Analyzer(Me.txtProgram.Text, New String() {"x", "y", "z"})

        Dim ret As Double = Parser.Calculate(New KeyValuePair(Of String, Double)() {New KeyValuePair(Of String, Double)("x", 1), New KeyValuePair(Of String, Double)("y", 2), New KeyValuePair(Of String, Double)("z", 3)})
        MsgBox(ret)
        'Catch ex As SyntaktickyAnalyzator.SynAnalyzer.WrongSyntaxException
        '   MsgBox("Chyba """ & ex.Message & """")
        'End Try
    End Sub
End Class