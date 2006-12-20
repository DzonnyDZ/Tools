Namespace VisualBasic
    ''' <summary>Tests for <see cref="Tools.VisualBasic.Interaction"/></summary>
    Module Interaction
        Public Sub iif()
            MsgBox("iif(True, ""True"", ""False"") = " & Tools.VisualBasic.Interaction.iif(True, "True", "False"), , "Tools.VisualBasic.Interaction.iif(Of T)")
        End Sub
    End Module
End Namespace
