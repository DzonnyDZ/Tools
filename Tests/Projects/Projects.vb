Namespace Projects
    ''' <summary>Miscaleneous tools for testing ptojects</summary>
    Public Module Misc
#If Config = Nightly Then
        Public Const ConfPath$ = "Nightly"
#ElseIf Config = Alpha Then
        public const ConfPath$="Alpha"
#ElseIf Config = Beta Then
        public const ConfPath$="Beta"
#ElseIf Config = RC Then
        public const ConfPath$="RC"
#ElseIf Config = Release Then
        public const ConfPath$="Release"
#End If
    End Module
    ''' <summary>Tests the Metanol project</summary>
    Module Metanol
        ''' <summary>Runs metanol project</summary>
        Public Sub Test()
            Try
                Process.Start(New Tools.IOt.Path(My.Application.Info.DirectoryPath) - 3 + "Projects" + "Metanol" + "bin" + ConfPath + "Metanol.exe")
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            End Try
        End Sub
    End Module
End Namespace
