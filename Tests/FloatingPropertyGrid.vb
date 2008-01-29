''' <summary>Floating property grid</summary>
Public Class FloatingPropertyGrid
    ''' <summary>CTor</summary>
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    ''' <summary>CTor with object to edit</summary>
    ''' <param name="obj">Object to edit</param>
    Public Sub New(ByVal obj As Object)
        Me.New()
        prgPrg.SelectedObject = obj
    End Sub
End Class