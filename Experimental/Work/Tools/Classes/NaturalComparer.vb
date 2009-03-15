Imports System.Collections.Generic, System.Text.RegularExpressions
'http://www.codeproject.com/KB/string/NaturalSortComparer.aspx
''' <summary>Implementuje tzv. pøirozené porovnávaání</summary>
Public Class NaturalComparer
    Inherits Comparer(Of String)
    Implements IDisposable

    Private table As New Dictionary(Of String, String())

    Public Sub Dispose() Implements IDisposable.Dispose
        table.Clear()
        table = Nothing
    End Sub

    Public Overrides Function Compare(ByVal x As String, ByVal y As String) As Integer
        If (x = y) Then
            Return 0
        End If
        Dim y1() As String = Nothing
        Dim x1() As String = Nothing
        If Not table.TryGetValue(x, x1) Then
            x1 = Regex.Split(x.Replace(" ", ""), "([0-9]+)")
            table.Add(x, x1)
        End If
        If Not table.TryGetValue(y, y1) Then
            y1 = Regex.Split(y.Replace(" ", ""), "([0-9]+)")
            table.Add(y, y1)
        End If
        Dim i As Integer = 0
        Do While ((i < x1.Length) _
                    AndAlso (i < y1.Length))
            If (x1(i) <> y1(i)) Then
                Return PartCompare(x1(i), y1(i))
            End If
            i = (i + 1)
        Loop
        If (y1.Length > x1.Length) Then
            Return 1
        ElseIf (x1.Length > y1.Length) Then
            Return -1
        Else
            Return 0
        End If
    End Function

    Private Shared Function PartCompare(ByVal left As String, ByVal right As String) As Integer
        Dim y As Integer
        Dim x As Integer
        If Not Integer.TryParse(left, x) Then
            Return left.CompareTo(right)
        End If
        If Not Integer.TryParse(right, y) Then
            Return left.CompareTo(right)
        End If
        Return x.CompareTo(y)
    End Function
End Class