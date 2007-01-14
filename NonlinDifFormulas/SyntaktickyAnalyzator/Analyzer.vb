Public Class Analyzer
    Dim Syn As SynAnalyzer
    Sub New(ByVal Program As String, ByVal VarArr As IEnumerable(Of String))
        Dim fun As New List(Of SynAnalyzer.Funct)
        fun.Add(New SynAnalyzer.Funct1Param("Abs", AddressOf Math.Abs))
        fun.Add(New SynAnalyzer.Funct1Param("Acos", AddressOf Math.Acos))
        fun.Add(New SynAnalyzer.Funct1Param("Asin", AddressOf Math.Asin))
        fun.Add(New SynAnalyzer.Funct1Param("Atan", AddressOf Math.Atan))
        fun.Add(New SynAnalyzer.Funct1Param("Ceiling", AddressOf Math.Ceiling))
        fun.Add(New SynAnalyzer.Funct1Param("Cos", AddressOf Math.Cos))
        fun.Add(New SynAnalyzer.Funct1Param("Cosh", AddressOf Math.Cosh))
        fun.Add(New SynAnalyzer.Funct1Param("Exp", AddressOf Math.Exp))
        fun.Add(New SynAnalyzer.Funct1Param("Floor", AddressOf Math.Floor))
        fun.Add(New SynAnalyzer.Funct1Param("Log", AddressOf Math.Log))
        fun.Add(New SynAnalyzer.Funct1Param("Log10", AddressOf Math.Log10))
        fun.Add(New SynAnalyzer.Funct1Param("Round", AddressOf Math.Round))
        fun.Add(New SynAnalyzer.Funct1Param("Sin", AddressOf Math.Sin))
        fun.Add(New SynAnalyzer.Funct1Param("Sinh", AddressOf Math.Sinh))
        fun.Add(New SynAnalyzer.Funct1Param("Sqrt", AddressOf Math.Sqrt))
        fun.Add(New SynAnalyzer.Funct1Param("Tan", AddressOf Math.Tan))
        fun.Add(New SynAnalyzer.Funct1Param("Truncate", AddressOf Math.Truncate))
        Syn = New SynAnalyzer(Program, fun, VarArr)
    End Sub
    Function Calculate(ByVal VarVal As IEnumerable(Of KeyValuePair(Of String, Double))) As Double
        'Dim vlist As New List(Of KeyValuePair(Of String, SynAnalyzer.Val))
        'For Each vv As KeyValuePair(Of String, Double) In VarVal
        '    vlist.Add(New KeyValuePair(Of String, SynAnalyzer.Val)(vv.Key, New SynAnalyzer.Val(vv.Value)))
        'Next
        Return Syn.Calculate(VarVal)
    End Function
End Class




