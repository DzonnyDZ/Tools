Imports EnvDTE

''' <summary>Contains misceleneous Visual Studio macros</summary>
Public Module Misc
    ''' <summary>replaces selection with GUID</summary>
    Public Sub Guid()
        Dim selection As EnvDTE.TextSelection = CType(DTE.ActiveWindow.Selection, TextSelection)
        Dim st = selection.Text
        DTE.UndoContext.Open("Insert GUID")
        Try
            selection.Text = System.Guid.NewGuid().ToString()
        Finally
            DTE.UndoContext.Close()
        End Try
    End Sub
End Module
