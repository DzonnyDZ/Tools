Namespace Projects
    ''' <summary>Tests the Metanol project</summary>
    Module Metanol
        ''' <summary>Runs metanol project</summary>
        Public Sub Test()
            Try
                Process.Start(New Tools.IOt.Path(My.Application.Info.DirectoryPath) - 3 + "Projects" + "Metanol" + "bin" + "Metanol.exe")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            End Try
        End Sub
    End Module
End Namespace
