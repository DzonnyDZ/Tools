Imports Tools.CollectionsT.GenericT, Tools, System.ComponentModel
Imports tools.WindowsT.FormsT.UtilitiesT
Imports System.Drawing.Design, System.Windows.Forms, System.Drawing

Partial Class DayView
    ''' <summary>Parametry operace provedené na øádku <see cref="DayView"/></summary>
    Public Class RowOperationEventArgs(Of T As EventArgs) : Inherits EventArgs
        ''' <summary>CTor</summary>
        ''' <param name="RowIndex">Index øádku v kolekci <see cref="DayView.Rows"/></param>
        ''' <param name="OriginalArgs">Parametry nastalé operace</param>
        Public Sub New(ByVal RowIndex As Integer, ByVal OriginalArgs As T)
            Me.RowIndex = RowIndex
            Me.OriginalArgs = OriginalArgs
        End Sub
        ''' <summary>Index øádku v kolekci <see cref="DayView.Rows"/></summary>
        Public ReadOnly RowIndex As Integer
        ''' <summary>Parametry provedené operace</summary>
        Public ReadOnly OriginalArgs As T
    End Class

    ''' <summary>Parametry události <see cref="beforeendMoveItem"/></summary>
    Public Class ItemMoveCancelEventArgs : Inherits CancelEventArgs
        ''' <summary>CTor (<see cref="DateRangeEventArgs"/>)</summary>
        ''' <param name="RowIndex">Index øádku</param>
        ''' <param name="ItemIndex">Index položky v øádku</param>
        ''' <param name="Old">Pùvodní (souèasná) pozice</param>
        ''' <param name="New">Nová (nadcházející) pozice</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="ItemIndex"/> nebo <paramref name="RowIndex"/> je menší než 0</exception>
        ''' <exception cref="ArgumentException"><see cref="DateRangeEventArgs.Start"/> je menší nebo roven <see cref="DateRangeEventArgs.[End]"/> (platí pro <paramref name="New"/> i <paramref name="Old"/>)</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="New"/> nebo <paramref name="Old"/> je null</exception>
        Public Sub New(ByVal RowIndex As Integer, ByVal ItemIndex As Integer, ByVal Old As DateRangeEventArgs, ByVal [New] As DateRangeEventArgs)
            If [New] Is Nothing Then Throw New ArgumentNullException("New")
            If [Old] Is Nothing Then Throw New ArgumentNullException("Old")
            If [New].Start >= [New].End OrElse Old.Start >= Old.End Then Throw New ArgumentException("Start must be less then end")
            _NewStart = [New].Start
            _NewEnd = [New].End
            _OldStart = Old.Start
            _OldEnd = Old.End
            If RowIndex < 0 Then Throw New ArgumentOutOfRangeException("RowIndex", RowIndex, "RowIndex must be 0 or greater")
            _RowIndex = RowIndex
            If ItemIndex < 0 Then Throw New ArgumentOutOfRangeException("ItemIndex", ItemIndex, "ItemIndex must be 0 or greater")
            _ItemIndex = ItemIndex
        End Sub
        ''' <summary>CTor (zaèátek a konec)</summary>
        ''' <param name="RowIndex">Index øádku</param>
        ''' <param name="ItemIndex">Index položky v øádku</param>
        ''' <param name="OldStart">Pùvodní (souèasný) zaèátek</param>
        ''' <param name="OldEnd">Pùvodní (souèasný) konec</param>
        ''' <param name="NewStart">Nový (nadcházející) zaèátek</param>
        ''' <param name="NewEnd">Nový (nadcházející) konec</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="ItemIndex"/> nebo <paramref name="RowIndex"/> je menší než 0</exception>
        ''' <exception cref="ArgumentException"><paramref name="NewStart"/> je vìtší nebo roven <see cref="NewEnd"/> -nebo- <paramref name="OldStart"/> je vìtší nebo roven <paramref name="OldEnd"/></exception>
        Public Sub New(ByVal RowIndex As Integer, ByVal ItemIndex As Integer, ByVal OldStart As Date, ByVal OldEnd As Date, ByVal NewStart As Date, ByVal NewEnd As Date)
            Me.New(RowIndex, ItemIndex, New DateRangeEventArgs(OldStart, OldEnd), New DateRangeEventArgs(NewStart, NewEnd))
        End Sub
        ''' <summary>CTor (zaèátek a délka)</summary>
        ''' <param name="RowIndex">Index øádku</param>
        ''' <param name="ItemIndex">Index položky v øádku</param>
        ''' <param name="OldStart">Pùvodní (souèasný) zaèátek</param>
        ''' <param name="OldDuration">Pùvodní (souèasná) délka</param>
        ''' <param name="NewStart">Nový (nadcházející) zaèátek</param>
        ''' <param name="NewDuration">Nová (nadcházející) délka</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="ItemIndex"/> nebo <paramref name="RowIndex"/> je menší než 0</exception>
        ''' <exception cref="ArgumentException"><paramref name="NewDuration"/> nebo <paramref name="OldDuration"/> je menší než nebo rovno nule</exception>
        Public Sub New(ByVal RowIndex As Integer, ByVal ItemIndex As Integer, ByVal OldStart As Date, ByVal OldDuration As TimeSpan, ByVal NewStart As Date, ByVal NewDuration As TimeSpan)
            Me.New(RowIndex, ItemIndex, OldStart, OldStart + OldDuration, NewStart, NewStart + NewDuration)
        End Sub
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="OldStart"/></summary>
        Private ReadOnly _OldStart As Date
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="OldEnd"/></summary>
        Private ReadOnly _OldEnd As Date
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="NewStart"/></summary>
        Private ReadOnly _NewStart As Date
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="NewEnd"/></summary>
        Private ReadOnly _NewEnd As Date
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="RowIndex"/></summary>
        Private ReadOnly _RowIndex As Integer
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="ItemIndex"/></summary>
        Private ReadOnly _ItemIndex As Integer
        ''' <summary>Pùvodní (souèasný) zaèátek</summary>
        Public ReadOnly Property OldStart() As Date
            Get
                Return _OldStart
            End Get
        End Property
        ''' <summary>Pùvodní (souèasný) konec</summary>
        Public ReadOnly Property OldEnd() As Date
            Get
                Return _OldEnd
            End Get
        End Property
        ''' <summary>Nový (nastavající) zaèátek</summary>
        Public ReadOnly Property NewStart() As Date
            Get
                Return _NewStart
            End Get
        End Property
        ''' <summary>Nový (nastávající) konec</summary>
        Public ReadOnly Property NewEnd() As Date
            Get
                Return _NewEnd
            End Get
        End Property
        ''' <summary>Index øádku kde dochází ke zmìnì</summary>
        Public ReadOnly Property RowIndex() As Integer
            Get
                Return _RowIndex
            End Get
        End Property
        ''' <summary>Index položky v øádku u které dochází ke zmìnì</summary>
        Public ReadOnly Property ItemIndex() As Integer
            Get
                Return _ItemIndex
            End Get
        End Property
        ''' <summary>Pùvodní (stávající) trvání</summary>
        Public ReadOnly Property OldDuration() As TimeSpan
            Get
                Return OldEnd - OldStart
            End Get
        End Property
        ''' <summary>Nové (nastávající) trvání</summary>
        Public ReadOnly Property NewDuration() As TimeSpan
            Get
                Return NewEnd - NewStart
            End Get
        End Property
    End Class

    ''' <summary>Parametr událostí s velikostí a pozicí prvku</summary>
    Public Class MoveEventArgs : Inherits EventArgs
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Rectangle"/></summary>
        Private _Rectangle As Rectangle
        ''' <summary>CTor z <see cref="Rectangle">Rectanglu</see></summary>
        ''' <param name="Rectangle">Rozmìry a pozice</param>
        Public Sub New(ByVal Rectangle As Rectangle)
            _Rectangle = Rectangle
        End Sub
        ''' <summary>CTor z pozice a velikosti</summary>
        ''' <param name="Location">Nová pozice</param>
        ''' <param name="Size">Nová velikost</param>
        Public Sub New(ByVal Location As Point, ByVal Size As Size)
            Me.New(New Rectangle(Location, Size))
        End Sub
        ''' <summary>CTor ze souøadnic a rozmìrù</summary>
        ''' <param name="x">Pozice X</param>
        ''' <param name="y">Pozice Y</param>
        ''' <param name="Width">Šíøka</param>
        ''' <param name="Height">Výška</param>
        Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal Width As Integer, ByVal Height As Integer)
            Me.New(New Rectangle(x, y, Width, Height))
        End Sub
        ''' <summary>Obdélník zaujímaný prvkem</summary>
        Public Property Rectangle() As Rectangle
            Get
                Return _Rectangle
            End Get
            Set(ByVal value As Rectangle)
                _Rectangle = value
            End Set
        End Property
        ''' <summary>Pozice X</summary>
        Public Property x() As Integer
            Get
                Return _Rectangle.X
            End Get
            Set(ByVal value As Integer)
                _Rectangle.X = value
            End Set
        End Property
        ''' <summary>Pozice Y</summary>
        Public Property y() As Integer
            Get
                Return _Rectangle.Y
            End Get
            Set(ByVal value As Integer)
                _Rectangle.Y = value
            End Set
        End Property
        ''' <summary>Šíøka</summary>
        Public Property Width() As Integer
            Get
                Return _Rectangle.Width
            End Get
            Set(ByVal value As Integer)
                _Rectangle.Width = value
            End Set
        End Property
        ''' <summary>Výška</summary>
        Public Property Height() As Integer
            Get
                Return _Rectangle.Height
            End Get
            Set(ByVal value As Integer)
                _Rectangle.Height = value
            End Set
        End Property
        ''' <summary>Pozice</summary>
        Public Property Location() As Point
            Get
                Return _Rectangle.Location
            End Get
            Set(ByVal value As Point)
                _Rectangle.Location = value
            End Set
        End Property
        ''' <summary>Rozmìry</summary>
        Public Property Size() As Size
            Get
                Return _Rectangle.Size
            End Get
            Set(ByVal value As Size)
                _Rectangle.Size = value
            End Set
        End Property
    End Class

    ''' <summary>Parametr stornovatelných událostí s velikostí a pozicí prvku</summary>
    Public Class CancelMoveEventArgs : Inherits MoveEventArgs
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Cancel"/></summary>
        Private _Cancel As Boolean = False
        ''' <summary>Pøi nastavení na True stornuje událost</summary>
        Public Property Cancel() As Boolean
            Get
                Return _Cancel
            End Get
            Set(ByVal value As Boolean)
                _Cancel = value
            End Set
        End Property
        ''' <summary>CTor z <see cref="Rectangle">Rectanglu</see></summary>
        ''' <param name="Rectangle">Rozmìry a pozice</param>
        Public Sub New(ByVal Rectangle As Rectangle)
            MyBase.New(Rectangle)
        End Sub
        ''' <summary>CTor z pozice a velikosti</summary>
        ''' <param name="Location">Nová pozice</param>
        ''' <param name="Size">Nová velikost</param>
        Public Sub New(ByVal Location As Point, ByVal Size As Size)
            MyBase.New(Location, Size)
        End Sub
        ''' <summary>CTor ze souøadnic a rozmìrù</summary>
        ''' <param name="x">Pozice X</param>
        ''' <param name="y">Pozice Y</param>
        ''' <param name="Width">Šíøka</param>
        ''' <param name="Height">Výška</param>
        Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal Width As Integer, ByVal Height As Integer)
            MyBase.New(x, y, Width, Height)
        End Sub
    End Class
    ''' <summary><see cref="CancelMoveEventArgs"/>, který navíc obsahuje i index øádku</summary>
    Friend Class CancelMoveRowEventArgs : Inherits CancelMoveEventArgs
        ''' <summary>Index øádku</summary>
        Public ReadOnly Row As Integer
        ''' <summary>CTor z <see cref="Rectangle">Rectanglu</see></summary>
        ''' <param name="Rectangle">Rozmìry a pozice</param>
        Public Sub New(ByVal Rectangle As Rectangle, ByVal Row As Integer)
            MyBase.New(Rectangle)
            Me.Row = Row
        End Sub
        ''' <summary>CTor z pozice a velikosti</summary>
        ''' <param name="Location">Nová pozice</param>
        ''' <param name="Size">Nová velikost</param>
        Public Sub New(ByVal Location As Point, ByVal Size As Size, ByVal Row As Integer)
            MyBase.New(Location, Size)
            Me.Row = Row
        End Sub
        ''' <summary>CTor ze souøadnic a rozmìrù</summary>
        ''' <param name="x">Pozice X</param>
        ''' <param name="y">Pozice Y</param>
        ''' <param name="Width">Šíøka</param>
        ''' <param name="Height">Výška</param>
        Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal Width As Integer, ByVal Height As Integer, ByVal Row As Integer)
            MyBase.New(x, y, Width, Height)
            Me.Row = Row
        End Sub
    End Class

    Partial Class ItemToolStrip : Inherits ToolStrip
        ''' <summary>Parametr události <see cref="BeginResize"/></summary>
        Public Class BeginResizeEventArgs : Inherits MouseEventArgs
            ''' <summary>Smìry zmìny velikosti</summary>
            Public Enum Directions
                ''' <summary>Nalevo (zaèátek)</summary>
                Left
                ''' <summary>Napravo (konec)</summary>
                Right
            End Enum
            ''' <summary>CTor</summary>
            ''' <param name="clicks">The number of times a mouse button was pressed.</param>
            ''' <param name="delta">A signed count of the number of detents the wheel has rotated.</param>
            ''' <param name="Y">The y-coordinate of a mouse click, in pixels.</param>
            ''' <param name="button">One of the <see cref="System.Windows.Forms.MouseButtons"/> values indicating which mouse button was pressed.</param>
            ''' <param name="x">The x-coordinate of a mouse click, in pixels</param>
            ''' <param name="Direction">Smìr zmìny velikosti</param>
            Public Sub New(ByVal button As MouseButtons, ByVal clicks As Integer, ByVal x As Integer, ByVal y As Integer, ByVal delta As Integer, ByVal Direction As Directions)
                MyBase.New(button, clicks, x, y, delta)
                _Direction = Direction
            End Sub
            ''' <summary>CTor z <see cref="MouseEventArgs"/></summary>
            ''' <param name="MouseEventArgs"><see cref="MouseEventArgs"/></param>
            ''' <param name="Direction">Smìr zmìny velikosti</param>
            Public Sub New(ByVal MouseEventArgs As MouseEventArgs, ByVal Direction As Directions)
                Me.New(MouseEventArgs.Button, MouseEventArgs.Clicks, MouseEventArgs.X, MouseEventArgs.Y, MouseEventArgs.Delta, Direction)
            End Sub
            ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Direction"/></summary>
            Private _Direction As Directions
            ''' <summary>Smìr zmìny velikosti</summary>
            Public ReadOnly Property Direction() As Directions
                Get
                    Return _Direction
                End Get
            End Property
        End Class
    End Class

#Region "ItemEventArgs"
    ''' <summary>Common interface for <see cref="EventArgs"/> that has <see cref="DayViewItem"/> as property.</summary>
    Public Interface IItemEventArgs
        ''' <summary>Item that caused the event</summary>
        ReadOnly Property Item() As DayViewItem
    End Interface
    ''' <summary>Arguments of event that supplies <see cref="DayViewItem"/> item that caused the event.</summary>
    Public Class ItemEventArgs : Inherits EventArgs : Implements IItemEventArgs
        ''' <summary>Contains value of the <see cref="Item"/> property</summary>
        Private ReadOnly _Item As DayViewItem
        ''' <summary>CTor</summary>
        ''' <param name="Item">Item that caused the event</param>
        Public Sub New(ByVal Item As DayViewItem)
            _Item = Item
        End Sub
        ''' <summary>Item that caused the event</summary>
        Public ReadOnly Property Item() As DayViewItem Implements IItemEventArgs.Item
            Get
                Return _Item
            End Get
        End Property
    End Class
    ''' <summary><see cref="KeyEventArgs"/> extended of information which item caused the event</summary>
    Public Class ItemKeyEventArgs : Inherits KeyEventArgs : Implements IItemEventArgs
        ''' <summary>Contains value of the <see cref="Item"/> property</summary>
        Private ReadOnly _Item As DayViewItem
        ''' <summary>CTor</summary>
        ''' <param name="e">Original <see cref="KeyEventArgs"/></param>
        ''' <param name="Item">Item that caused the event</param>
        Public Sub New(ByVal e As KeyEventArgs, ByVal Item As DayViewItem)
            MyBase.New(e.KeyData)
            _Item = Item
        End Sub
        ''' <summary>Item that caused the event</summary>
        Public ReadOnly Property Item() As DayViewItem Implements IItemEventArgs.Item
            Get
                Return _Item
            End Get
        End Property
    End Class
    ''' <summary><see cref="KeyPressEventArgs"/> extended of information which item caused the event</summary>
    Public Class ItemKeyPressEventArgs : Inherits KeyPressEventArgs : Implements IItemEventArgs
        ''' <summary>Contains value of the <see cref="Item"/> property</summary>
        Private ReadOnly _Item As DayViewItem
        ''' <summary>CTor</summary>
        ''' <param name="e">Original <see cref="KeyPressEventArgs"/></param>
        ''' <param name="Item">Item that caused the event</param>
        Public Sub New(ByVal e As KeyPressEventArgs, ByVal Item As DayViewItem)
            MyBase.New(e.KeyChar)
            _Item = Item
        End Sub
        ''' <summary>Item that caused the event</summary>
        Public ReadOnly Property Item() As DayViewItem Implements IItemEventArgs.Item
            Get
                Return _Item
            End Get
        End Property
    End Class
    ''' <summary><see cref="MouseEventArgs"/> extended of information which item caused the event</summary>
    Public Class ItemMouseEventArgs : Inherits MouseEventArgs : Implements IItemEventArgs
        ''' <summary>Contains value of the <see cref="Item"/> property</summary>
        Private ReadOnly _Item As DayViewItem
        ''' <summary>CTor</summary>
        ''' <param name="e">Original <see cref="MouseEventArgs"/></param>
        ''' <param name="Item">Item that caused the event</param>
        Public Sub New(ByVal e As MouseEventArgs, ByVal Item As DayViewItem)
            MyBase.New(e.Button, e.Clicks, e.X, e.Y, e.Delta)
            _Item = Item
        End Sub
        ''' <summary>Item that caused the event</summary>
        Public ReadOnly Property Item() As DayViewItem Implements IItemEventArgs.Item
            Get
                Return _Item
            End Get
        End Property
    End Class
    ''' <summary><see cref="ToolStripItemClickedEventArgs"/> extended of information which item caused the event</summary>
    Public Class ItemToolStripItemClickedEventArgs : Inherits ToolStripItemClickedEventArgs : Implements IItemEventArgs
        ''' <summary>Contains value of the <see cref="Item"/> property</summary>
        Private ReadOnly _Item As DayViewItem
        ''' <summary>CTor</summary>
        ''' <param name="e">Original <see cref="KeyEventArgs"/></param>
        ''' <param name="Item">Item that caused the event</param>
        Public Sub New(ByVal Item As DayViewItem, ByVal e As ToolStripItemClickedEventArgs)
            MyBase.New(e.ClickedItem)
            _Item = Item
        End Sub
        ''' <summary>Item that caused the event</summary>
        Public ReadOnly Property Item() As DayViewItem Implements IItemEventArgs.Item
            Get
                Return _Item
            End Get
        End Property
    End Class
#End Region
End Class

Partial Class DayViewRow
    ''' <summary>Spoleèný základ pro všechny tøídy <see cref="ItemOperationEventArgs(Of T)"/></summary>
    Public MustInherit Class ItemOperationEventArgsBase
        Inherits EventArgs
        ''' <summary>Operace s prvky seznamu</summary>
        Public Enum Operations
            ''' <summary>Pøidání (<see cref="ListWithEvents(Of DayViewItem).Added"/>)</summary>
            Added
            ''' <summary>Pøidání (<see cref="ListWithEvents(Of DayViewItem).Cleared"/>)</summary>
            Cleared
            ''' <summary>Pøidání (<see cref="ListWithEvents(Of DayViewItem).ItemChanged"/>)</summary>
            ItemChanged
            ''' <summary>Pøidání (<see cref="ListWithEvents(Of DayViewItem).ItemValueChanged"/>)</summary>
            ItemValueChanged
            ''' <summary>Pøidání (<see cref="ListWithEvents(Of DayViewItem).Removed"/>)</summary>
            Removed
        End Enum
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Operation"/></summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private ReadOnly _Operation As Operations
        ''' <summary>Operace, která se seznamem nastala</summary>
        Public ReadOnly Property Operation() As Operations
            Get
                Return _Operation
            End Get
        End Property
        ''' <summary>CTor</summary>
        ''' <param name="Operation">Operace, která se seznammem nastala</param>
        Public Sub New(ByVal Operation As Operations)
            _Operation = Operation
        End Sub
    End Class
    ''' <summary>Parametry události <see cref="Changed"/> pro operace s prvky seznamu <see cref="Records"/></summary>
    ''' <typeparam name="T">Typ události (podle toho jaká operace nastala)</typeparam>
    Public Class ItemOperationEventArgs(Of T As EventArgs)
        Inherits ItemOperationEventArgsBase
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Operationargs"/></summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private ReadOnly _OperationArgs As T
        ''' <summary>Pùvodní argumenty události vyvolané <see cref="ListWithEvents(Of DayViewItem)"/></summary>
        Public ReadOnly Property OperationArgs() As T
            <DebuggerStepThrough()> Get
                Return _OperationArgs
            End Get
        End Property
        ''' <summary>CTor</summary>
        ''' <param name="Operation">Operace, která nad seznamem nastala</param>
        ''' <param name="OperationArgs">Pùvodní argumenty události vyvolané <see cref="ListWithEvents(Of DayViewItem)"/></param>
        Public Sub New(ByVal Operation As Operations, ByVal OperationArgs As T)
            MyBase.New(Operation)
            _OperationArgs = OperationArgs
        End Sub
    End Class
End Class