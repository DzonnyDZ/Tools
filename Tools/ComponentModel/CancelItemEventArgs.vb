#If True
Namespace ComponentModelT
    ''' <summary>Cancelable event argumens carrying item</summary>
    ''' <typeparam name="T">Item being carried</typeparam>
    ''' <version version="1.5.2">Class introduced</version>
    Public Class CancelItemEventArgs(Of T)
        Inherits CancelEventArgs
        ''' <summary>Contains value of the <see cref="Item"/> property</summary>
        Private ReadOnly _Item As T
        ''' <summary>CTor from item</summary>
        ''' <param name="Item">Item to carry</param>
        Public Sub New(ByVal Item As T)
            _Item = Item
        End Sub
        ''' <summary>CTtor ftom item and <see cref="Cancel"/> value</summary>
        ''' <param name="Item">Item to carry</param>
        ''' <param name="Cancel"><see cref="Cancel"/> default value</param>
        Public Sub New(ByVal Item As T, ByVal Cancel As Boolean)
            MyBase.New(Cancel)
            _Item = Item
        End Sub
        ''' <summary>Gets item connected with event</summary>
        ''' <returns>Item connected with event</returns>
        ''' <remarks>The connection between item and event depends on event source. It may be item being added to collection or object that caused the event etc.</remarks>
        Public ReadOnly Property Item() As T
            Get
                Return _Item
            End Get
        End Property
    End Class
End Namespace
#End If