Imports Tools.InternalT
Namespace InternalT
#If True
    Public Class VersionAttributeTest
        Public Shared Sub Test()
            Dim cp As New Microsoft.VisualBasic.VBCodeProvider
            Dim cParams As System.CodeDom.Compiler.CompilerParameters = New CodeDom.Compiler.CompilerParameters()
            cParams.GenerateExecutable = False
            cParams.GenerateInMemory = True
            cParams.IncludeDebugInformation = True
            Dim vType As Type = Type.GetType("Tools.InternalT.VersionAttribute, Tools")
            cParams.ReferencedAssemblies.Add(vType.Assembly.Location)
            Dim compiled = cp.CompileAssemblyFromSource(cParams, New String() { _
                                            "Namespace Tools.Tests.InternalT" & vbCrLf & _
                                            "<Tools.InternalT.VersionAttribute(1,0,GetType(ClassWithVersionAttribute), LastChange:=""11/26/2008"")> _" & vbCrLf & _
                                            "Public Class ClassWithVersionAttribute" & vbCrLf & _
                                            "End Class" & vbCrLf & _
                                            "End Namespace"})

            Dim t = compiled.CompiledAssembly.GetType("Tools.Tests.InternalT.ClassWithVersionAttribute")


            Dim Attrs As Object() = t.GetCustomAttributes(vType, False)
            Dim attr As Attribute = Attrs(0)
            MsgBox( _
                vType.GetProperty("LastChange").GetValue(attr, Nothing).ToString & vbCrLf & _
                vType.GetProperty("LastChangeDate").GetValue(attr, Nothing).ToString & vbCrLf & _
                vType.GetProperty("Major").GetValue(attr, Nothing).ToString & "." & _
                vType.GetProperty("Minor").GetValue(attr, Nothing).ToString & "." & _
                vType.GetProperty("Build").GetValue(attr, Nothing).ToString & "." & _
                vType.GetProperty("Revision").GetValue(attr, Nothing).ToString)
        End Sub

    End Class
#End If
End Namespace