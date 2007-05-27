''' <summary>Contains tests for <see cref="Tools.MathT"/></summary>
Partial Friend Class MathT
    ''' <summary>Test for <see cref="Tools.MathT.Min"/></summary>
    Public Shared Sub Min()
        Dim arr As Long() = {7, 6, 5, -8, 12, 120, 501}
        Dim Sample As String = ""
        For Each Number As Long In arr
            If Sample <> "" Then Sample &= ", "
            Sample &= Number
        Next Number
        Dim result As Long = Tools.MathT.Min(arr)
        MsgBox("Min(" & Sample & ") = " & result, , "Tools.Math.Min")
    End Sub
    ''' <summary>Test for <see cref="Tools.MathT.Max"/></summary>
    Public Shared Sub Max()
        Dim arr As Long() = {7, 6, 5, -8, 12, 120, 501}
        Dim Sample As String = ""
        For Each Number As Long In arr
            If Sample <> "" Then Sample &= ", "
            Sample &= Number
        Next Number
        Dim result As Long = Tools.MathT.Max(arr)
        MsgBox("Max(" & Sample & ") = " & result, , "Tools.Math.Max")
    End Sub
End Class
