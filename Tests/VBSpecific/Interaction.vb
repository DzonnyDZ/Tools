#If VBC_VER < 9.0 Then
Namespace VisualBasicT
    ''' <summary>Tests for <see cref="Tools.VisualBasicT.Interaction"/></summary>
    Friend Module Interaction
        Public Sub iif()
            MsgBox("iif(True, ""True"", ""False"") = " & Tools.VisualBasicT.Interaction.iif(True, "True", "False"), , "Tools.VisualBasic.Interaction.iif(Of T)")
        End Sub
    End Module
End Namespace
#End If
