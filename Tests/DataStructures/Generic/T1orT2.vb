Imports Tools.DataStructuresT.GenericT
Namespace DataStructuresT.GenericT
    Friend Module T1orT2
        Sub Test()
            Dim t1 As T1orT2(Of String, Long)
            Dim t2 As T1orT2(Of String, Long)
            t1 = "Hello world"
            t2 = 1457L
            MsgBox("t1.contains1 = " & t1.contains1 & vbCrLf & _
                    "t1.contains2 = " & t1.contains2 & vbCrLf & _
                    "t2.contains1 = " & t2.contains1 & vbCrLf & _
                    "t2.contains2 = " & t2.contains2 & vbCrLf & _
                    "t1.value1 = " & t1.value1 & vbCrLf & _
                    "t2.value2 = " & t2.value2 & vbCrLf & _
                    "t1.value = " & CStr(t1.value) & vbCrLf & _
                    "t2.value = " & CLng(t2.value) & vbCrLf & _
                    t1.ToString & ", " & t2.ToString & vbCrLf & _
                    t1.contains(GetType(String)) & ", " & t2.contains(GetType(Long)) & vbCrLf & _
                    t1.objValue.ToString & ", " & t2.objValue.ToString)
        End Sub
    End Module
End Namespace
