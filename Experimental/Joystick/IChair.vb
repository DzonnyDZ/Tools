Namespace DevicesT.JoystickT
    ''' <summary>Interface křesla</summary>
    Public Interface IChair
        ''' <summary>Počet os</summary>
        ReadOnly Property AxesCount() As Byte
        ''' <summary>Počet tlačítek</summary>
        ReadOnly Property ButtonsCount() As Byte
        ''' <summary>Aktuální pozice osy</summary>
        ''' <param name="index">Index osy pro zjištění pozice</param>
        ReadOnly Property Position(ByVal index As Byte) As Integer
        ''' <summary>Schopnosti os</summary>
        ReadOnly Property AxeCap() As AxeCap()
        ''' <summary>Nastane ihned po změně polohy osy</summary>
        Event AxeChanged As dAxeChanged
        ''' <summary>Nastane ihned po stlačení tlačítka</summary>
        Event ButtonDown As dButtonEvenet
        ''' <summary>Nastane ihned po uvolnění tlačítka</summary>
        Event ButtonUp As dButtonEvenet
        ''' <summary>Zapsat se pro odběr bufferovaných událostí</summary>
        ''' <param name="Handler">Delegát, který bude události odebírat</param>
        ''' <param name="Interval">Mini¨mální interval mezi dvěma událostmi</param>
        Sub SubscribeBufferedEvents(ByVal Handler As dBufferedEvent, ByVal Interval As UInteger)
        ''' <summary>Odhlásit delegáta z odběru bufferovaných událostí</summary>
        ''' <param name="Handler">Delegát pro odhlášení</param>
        Sub UnsubscribeBufferedEvents(ByVal Handler As dBufferedEvent)
        ''' <summary>Stav tlačítka</summary>
        ''' <param name="index">Index tlačítka pro zjištěbí stavu</param>
        ReadOnly Property ButtonStatus(ByVal index As Byte) As ButtonAction
        ''' <summary>ID křesla v rámci serveru</summary>
        ReadOnly Property ID() As ULong
        ''' <summary>Vrací informaci jestli může být k <see cref="SubscribeBufferedEvents"/> přihlášeno více klientů najednou</summary>
        ReadOnly Property MultiUse() As Boolean
    End Interface

    ''' <summary>Schopnosti osy křesla</summary>
    Public Class AxeCap
        ''' <summary>Podporuje absolutní pozicování</summary>
        Public ReadOnly SupportsAbsolute As Boolean
        ''' <summary>Podporuje přetočení</summary>
        Public ReadOnly Overflows As Boolean
        ''' <summary>Minimální hodnota pokud křeslo podporuje absutní pozicování</summary>
        Public ReadOnly Min As Integer
        ''' <summary>Maximální hodnota, pokud křeslo podporuje absolutní pozicování</summary>
        Public ReadOnly Max As Integer
        ''' <summary>CTor - bez podpory absolutního pozicování</summary>
        ''' <param name="Overflows">Podporuje přetočení</param>
        Public Sub New(Optional ByVal Overflows As Boolean = False)
            Me.SupportsAbsolute = False
            Me.Overflows = Overflows
        End Sub
        ''' <summary>CTor - s podporou absolutního pozicování</summary>
        ''' <param name="Min">Minimální hodnota osy</param>
        ''' <param name="Max">Maximální hodnota osy</param>
        ''' <param name="Overflows">Podporuje přetočení</param>
        Public Sub New(ByVal Min As Integer, ByVal Max As Integer, Optional ByVal Overflows As Boolean = False)
            Me.SupportsAbsolute = True
            Me.Overflows = Overflows
            Me.Min = Min
            Me.Max = Max
        End Sub
    End Class

    ''' <summary>Delegát události <see cref="IKřeslo.AxeChanged"/></summary>
    ''' <param name="e">Parametry události</param>
    ''' <param name="sender">Zdroj události</param>
    Public Delegate Sub dAxeChanged(ByVal sender As IChair, ByVal e As AxeEventArgs)
    ''' <summary>Delegát událostti <see cref="IKřeslo.ButtonDown"/> a <see cref="IKřeslo.ButtonUp"/></summary>
    ''' <param name="sender">zdroj události</param>
    ''' <param name="e">parametry události</param>
    Public Delegate Sub dButtonEvenet(ByVal sender As IChair, ByVal e As ButtonEventArgs)
    ''' <summary>Delegát bufferované události</summary>
    ''' <param name="sender">zdroj události</param>
    ''' <param name="e">parametry události</param>
    Public Delegate Sub dBufferedEvent(ByVal sender As IChair, ByVal e As BufferedEventArgs)

    ''' <summary>Parametry události stisknutí tlačítka</summary>
    Public Class ButtonEventArgs : Inherits EventArgs
        ''' <summary>Číslo tlačítka (0-based)</summary>
        Public ReadOnly Number As Byte
        ''' <summary>CTor</summary>
        ''' <param name="Number">Číslo tlačítka</param>
        Public Sub New(ByVal Number As Byte)
            Me.Number = Number
        End Sub
    End Class
    ''' <summary>Parametry bufferované události tlačítka</summary>
    Public Class BufferedButtonEventArgs : Inherits ButtonEventArgs
        ''' <summary>Akce tlačítka</summary>
        Public ReadOnly Action As ButtonAction
        ''' <summary>CTor</summary>
        ''' <param name="Number">Číslo tlačítka</param>
        ''' <param name="Action">Akce</param>
        Public Sub New(ByVal Number As Byte, ByVal Action As ButtonAction)
            MyBase.New(Number)
            Me.Action = Action
        End Sub
    End Class

    Public Class AxeEventArgs : Inherits EventArgs
        ''' <summary>Číslo osy</summary>
        Public ReadOnly Number As Byte
        ''' <summary>Absolutní pozice osy</summary>
        Public ReadOnly Position As Integer
        ''' <summary>Relativní pozice osy od poslední události</summary>
        ''' <remarks>Delta může být větší než 0 i když rozdíl oproti předchozímu stavu je záporný a naopak (při přetočení křesla přes 360°) (pro <see cref="AxeCap.Overflows"/> true)</remarks>
        Public ReadOnly Delta As Integer
        ''' <summary>Podporuje absolutní pozivání</summary>
        Public ReadOnly SupportsAbsolute As Boolean
        ''' <summary>CTor</summary>
        ''' <param name="Number">číslo osy</param>
        ''' <param name="Position">Absolutní pozice (pokud je podporována, jinak 0)</param>
        ''' <param name="Delta">Rozdíl oproti poslední pozici</param>
        Public Sub New(ByVal Number As Byte, ByVal Position As Integer, ByVal Delta As Integer)
            Me.Number = Number
            Me.Position = Position
            Me.Delta = Delta
            Me.SupportsAbsolute = True
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="Number">číslo osy</param>
        ''' <param name="Delta">Rozdíl oproti poslední pozici</param>
        Public Sub New(ByVal Number As Byte, ByVal Delta As Integer)
            Me.Number = Number
            Me.Delta = Delta
            Me.SupportsAbsolute = False
        End Sub
    End Class

    ''' <summary>Parametry bufferované události</summary>
    Public Class BufferedEventArgs : Inherits EventArgs
        ''' <summary>Souhrnná změna os</summary>
        Public ReadOnly Axes As AxeEventArgs()
        ''' <summary>Posloupnost akcí provedených na tlačítkách</summary>
        Public ReadOnly Buttons As BufferedButtonEventArgs()
        ''' <summary>CTor</summary>
        ''' <param name="Axes">Změny os (všec, i těch kde kezměnám nedošlo)</param>
        ''' <param name="Buttons">Akce na tlačítkách</param>
        Public Sub New(ByVal Axes As IEnumerable(Of AxeBuffer), ByVal Buttons As IEnumerable(Of BufferedButtonEventArgs))
            Me.Buttons = New List(Of BufferedButtonEventArgs)(Buttons).ToArray
            Dim Ax As New List(Of AxeEventArgs)
            Dim i As Integer = 0
            For Each Axe As AxeBuffer In Axes
                If Axe.Changed Then Ax.Add(New AxeEventArgs(i, Axe.Position, Axe.DeltaTotal))
                i += 1
            Next Axe
            Me.Axes = Ax.ToArray
        End Sub
        ''' <summary>True pokud nedošlo k žádné změně osy ani akci na tlačítku</summary>
        Public ReadOnly Property IsEmpty() As Boolean
            Get
                Return (Axes Is Nothing OrElse Axes.Length = 0) AndAlso (Buttons Is Nothing OrElse Buttons.Length = 0)
            End Get
        End Property
    End Class

    ''' <summary>Záznam o souhrnné změně osy</summary>
    Public Class AxeBuffer
        ''' <summary>Aktuální absolutní pozice, pokud ji zdroj podporuje (jinak 0)</summary>
        Public Position As Integer
        ''' <summary>Součet rozdílů změn polohy</summary>
        ''' <remarks>Pro osy, které podporují přetočení může překročit jejich maximálníhodnotu</remarks>
        Public DeltaTotal As Integer
        ''' <summary>True pokud došlo k nějaké změně (ke změně mohlo dojít i pokud je souhrnná Δ 0)</summary>
        Public Changed As Boolean
        ''' <summary>CTor</summary>
        ''' <param name="Position">Absolutní pozice, pokud ji zdroj podporuje (jinak 0)</param>
        ''' <param name="Delta">Souhrnná delta (viz <see cref="DeltaTotal"/>)</param>
        ''' <param name="Changed">Došlo ke změně (viz <see cref="Changed"/>)</param>
        Public Sub New(ByVal Position As Integer, Optional ByVal Delta As Integer = 0, Optional ByVal Changed As Boolean = False)
            Me.Position = Position
            Me.DeltaTotal = Delta
            Me.Changed = Changed
        End Sub
        ''' <summary>Do nothing CTor</summary>
        Public Sub New()
            Me.New(0)
        End Sub
    End Class

    ''' <summary>Obecný bufferovač událostí křesla</summary>
    Public Class EventBuffer : Implements IDisposable
        ''' <summary>Akce na jednotlivých tlačítkách</summary>
        Private Buttons As List(Of BufferedButtonEventArgs)
        ''' <summary>Změny jednotlivých os</summary>
        Private Axes As AxeBuffer()
        ''' <summary>Delegát, který je cílem událostí</summary>
        Private Handler As dBufferedEvent
        ''' <summary>Křeslo, které je zdrojem událostí</summary>
        Private Křeslo As IChair
        ''' <summary>Interval generování událostí</summary>
        Private Interval As Integer
        ''' <summary>čas posledního vegenerování události</summary>
        Private LastRaised As Date
        ''' <summary>Časovač</summary>
        Private WithEvents Timer As New Windows.Forms.Timer
        ''' <summary>Tato metoda je volána křeslem, pokud dojde k nějaké divočárně, která se by se u fyzického křesla neměla dít (změna počtu os a tlačítek)</summary>
        Public Sub Reset()
            Buttons = New List(Of BufferedButtonEventArgs)
            ReDim Axes(Křeslo.AxesCount)
            For i As Byte = 0 To Křeslo.AxesCount
                Axes(i) = New AxeBuffer()
            Next i
            LastRaised = Date.MinValue
        End Sub
        ''' <summary>CToír</summary>
        ''' <param name="Handler">delekát, který bude cílem událostí</param>
        ''' <param name="Křeslo">Křeslo, které je zdrojem událostí</param>
        ''' <param name="Interval">Minimální iterval zasíláníá událostí</param>
        Public Sub New(ByVal Handler As dBufferedEvent, ByVal Křeslo As IChair, ByVal Interval As Integer)
            Me.Křeslo = Křeslo
            Me.Handler = Handler
            Me.Interval = Interval
            Reset()
            AddHandler Křeslo.AxeChanged, AddressOf OnAxeChanged
            AddHandler Křeslo.ButtonDown, AddressOf OnButtonDown
            AddHandler Křeslo.ButtonUp, AddressOf OnButtonUp
        End Sub
        ''' <summary>Handler události křesla <see cref="IKřeslo.AxeChanged"/></summary>
        Private Sub OnAxeChanged(ByVal sender As IChair, ByVal e As AxeEventArgs)
            With Axes(e.Number)
                .Changed = True
                .Position = e.Position
                .DeltaTotal += e.Delta
            End With
            Changed()
        End Sub
        ''' <summary>Handler události křesla <see cref="IKřeslo.ButtonDown"/></summary>
        Private Sub OnButtonDown(ByVal sender As IChair, ByVal e As ButtonEventArgs)
            Buttons.Add(New BufferedButtonEventArgs(e.Number, ButtonAction.Down))
            Changed()
        End Sub
        ''' <summary>Handler události křesla <see cref="IKřeslo.ButtonUp"/></summary>
        Private Sub OnButtonUp(ByVal sender As IChair, ByVal e As ButtonEventArgs)
            Buttons.Add(New BufferedButtonEventArgs(e.Number, ButtonAction.Up))
            Changed()
        End Sub
        ''' <summary>Interně voláno při jakékoliv události křesla</summary>
        ''' <remarks>Zkontroluje, kdy byla naposledy vyvolána událost a případně ji vyvolá nebo naplánuje její vyvolání</remarks>
        Private Sub Changed()
            Dim Now As Date = Date.Now
            If (Now - LastRaised).TotalMilliseconds >= Interval Then
                Timer.Enabled = False
                Raise()
            ElseIf Not Timer.Enabled Then
                Timer.Interval = Interval - (Now - LastRaised).TotalMilliseconds
                Timer.Start()
            End If
        End Sub
        ''' <summary>Vyvolá naplánovanou událost s časovým odstupem</summary>
        Private Sub Timer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer.Tick
            Timer.Stop()
            Raise()
        End Sub
        ''' <summary>Vyvolá událost</summary>
        Private Sub Raise()
            Dim e As New BufferedEventArgs(Axes, Buttons)
            LastRaised = Now
            If Not e.IsEmpty Then
                Handler(Křeslo, e)
            End If
            ResetState()
        End Sub
        ''' <summary>REsetuje stav událostí po vyvolání události</summary>
        Private Sub ResetState()
            For i As Integer = 0 To Axes.Length - 1
                Axes(i).DeltaTotal = 0
                Axes(i).Changed = False
            Next i
            Buttons.Clear()
        End Sub
#Region " IDisposable Support "
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False

        ''' <summary>IDisposable</summary>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    RemoveHandler Křeslo.AxeChanged, AddressOf OnAxeChanged
                    RemoveHandler Křeslo.ButtonDown, AddressOf OnButtonDown
                    RemoveHandler Křeslo.ButtonUp, AddressOf OnButtonUp
                End If
            End If
            Me.disposedValue = True
        End Sub

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <remarks>This code added by Visual Basic to correctly implement the disposable pattern.</remarks>
        <DebuggerNonUserCode()> _
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        ''' <summary>True pokud <paramref name="Handler"/> je stejný jako <see cref="Handler"/></summary>
        ''' <param name="Handler">Handler k ověření</param>
        ''' <remarks>Pokužíváno <see cref="IKřeslo">křeslem</see> v metodě <see cref="IKřeslo.UnsubscribeBufferedEvents"/></remarks>
        Public Function HasHandler(ByVal Handler As dBufferedEvent) As Boolean
            Return Handler = Me.Handler
        End Function

    End Class
    ''' <summary>Akce tlačítka</summary>
    Public Enum ButtonAction As Byte
        ''' <summary>Uvolnění</summary>
        Up = False
        ''' <summary>Stisknutí</summary>
        Down = True
    End Enum
End Namespace