'UpgradeIssue: Uncomment TypeTools.vb
'Imports Tools.SpecialT.TypeTools
'Namespace SpecialT
'    ''' <summary>Tests for <see cref="Tools.SpecialT.TypeTools"/></summary>
'    Friend Module TypeTools
'        ''' <summary>Tests <see cref="Tools.SpecialT.TypeTools.IsDefined"/></summary>
'        Public Sub IsDefined()
'            Dim var As en
'            var = 14
'            MsgBox(If(var.IsDefined, "14 is defined", "14 is not defined"))
'            var = 3
'            MsgBox(If(var.IsDefined, "3 is defined", "3 is not defined"))
'        End Sub
'        Public Sub GetConstant()
'            MsgBox("Name of 2 is " & Tools.SpecialT.TypeTools.GetConstant(en.Second).Name)
'        End Sub
'    End Module
'    Friend Enum en
'        First = 1
'        Second = 2
'        Third = 3
'    End Enum

'End Namespace