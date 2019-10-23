'Thi is split to another file because of designer stupidity
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace WindowsT.FormsT
    ''' <summary>Implements <see cref="Wizard"/> with specific first-step control</summary>
    ''' <typeparam name="T">Type of control of firts step</typeparam>
    ''' <version version="1.5.2" stage="Nightly">Class moved from experimental (namespace <see cref="N:Tools.Experimental.GUI"/>)</version>
    Public Class Wizard(Of T As {Control, New, IWizardControl})
        Inherits Wizard
        ''' <summary>CTor</summary>
        Public Sub New()
            MyBase.New(New T)
        End Sub
    End Class
End Namespace
