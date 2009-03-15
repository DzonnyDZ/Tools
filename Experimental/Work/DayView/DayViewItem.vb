Imports Tools.CollectionsT.GenericT, Tools, System.ComponentModel
Imports Tools.WindowsT.FormsT.UtilitiesT
Imports System.Drawing.Design
Imports System.Drawing

''' <summary>Položka <see cref="DayView"/></summary>
Public Class DayViewItem : Implements IReportsChange
    ''' <summary>Raised when value of member changes</summary>
    ''' <param name="sender">The source of the event</param>
    ''' <param name="e">Event information (<see cref="IReportsChange.ValueChangedEventArgs"/>)</param>
    Public Event Changed(ByVal sender As Tools.IReportsChange, ByVal e As System.EventArgs) Implements Tools.IReportsChange.Changed
#Region "CTors"
    ''' <summary>CTor (bez barev)</summary>
    ''' <param name="Start">Datum a èas zaèátku</param>
    ''' <param name="Length">Délka záznamu [min]</param>
    ''' <param name="Text">Text položky</param>
    ''' <param name="DataID">ID pøidružené položky v databázi</param>
    ''' <param name="Locked">Je položka chránìna proti pøesunùm</param>
    ''' <param name="Font">Písmo položky</param>
    ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Length"/> je 0</exception>
    ''' <exception cref="ArgumentException">Záznam s takovýmito parametry <paramref name="Start"/> a <see cref="Length"/> by pøekraèoval hranici dne.</exception>
    <CLSCompliant(False)> _
    Public Sub New(ByVal Start As Date, ByVal Length As UShort, ByVal Text As String, Optional ByVal DataID As Integer = 0, Optional ByVal Locked As Boolean = False, Optional ByVal Font As Font = Nothing)
        'TODO:Introduce CLS-compliant CTor
        Me.New(Start, Length, Text, SystemColors.Control, SystemColors.ControlText, DataID, Locked, Font)
    End Sub
    ''' <summary>CTor z datové položky</summary>
    ''' <param name="DataItem">Datová položka</param>
    ''' <param name="Font">Písmo (nebo bude použito výchozí)</param>
    ''' <exception cref="PopulateException">Pøi nastavování hodnot vlastností z datové položky došlo k chybì.</exception>
    ''' <remarks>Pokud datová položka neposkytne vlastnost <see cref="IDayViewDataItem.Enabled"/> bude <see cref="Locked"/> nastaveno na false.
    ''' Pokud neposkytne barva, budou použity výchozí.</remarks>
    Public Sub New(ByVal DataItem As IDayViewDataItem, Optional ByVal Font As Font = Nothing)
        With Me
            .BackColor = SystemColors.Control
            .ForeColor = SystemColors.ControlText
            .Locked = Locked
            .DataItem = DataItem
            If Font IsNot Nothing Then .Font = Font
        End With
    End Sub
    '''' <summary>CTor from generic data item</summary>
    '''' <param name="DataItem">Data item to create instance from</param>
    '''' <param name="Parent"><see cref="DayView"/> that provides information how to transform <paramref name="DataItem"/> to <see cref="DayViewItem"/></param>
    '''' <exception cref="ArgumentException">An exception ocuured while populating new instance from <paramref name="DataItem"/>. See <see cref="ArgumentException.InnerException"/> for details.</exception>
    '''' <exception cref="ArgumentNullException"><paramref name="DataItem"/> or <paramref name="Parent"/> is null</exception>
    'Public Sub New(ByVal DataItem As Object, ByVal Parent As DayView)
    '    Me.New(DataItem, Parent, SystemColors.Control, SystemColors.ControlText)
    'End Sub
    '''' <summary>CTor from generic data item</summary>
    '''' <param name="DataItem">Data item to create instance from</param>
    '''' <param name="Parent"><see cref="DayView"/> that provides information how to transform <paramref name="DataItem"/> to <see cref="DayViewItem"/></param>
    '''' <param name="BackColor">Barva pozadí</param>
    '''' <param name="ForeColor">Barva popøedí</param>
    '''' <exception cref="ArgumentException">An exception ocuured while populating new instance from <paramref name="DataItem"/>. See <see cref="ArgumentException.InnerException"/> for details.</exception>
    '''' <exception cref="ArgumentNullException"><paramref name="DataItem"/> or <paramref name="Parent"/> is null</exception>
    'Public Sub New(ByVal DataItem As Object, ByVal Parent As DayView, ByVal ForeColor As Color, ByVal BackColor As Color)
    '    If DataItem Is Nothing Then Throw New ArgumentNullException("DataItem")
    '    If DataItem Is Nothing Then Throw New ArgumentNullException("Parent")
    '    With Me
    '        .BackColor = BackColor
    '        .ForeColor = ForeColor
    '        .DataItem = DataItem
    '        .PopulateFromDataItem(DataItem, Parent)
    '    End With
    'End Sub
    ''' <summary>CTor z datové položky a barev</summary>
    ''' <param name="DataItem">Datová položka</param>
    ''' <param name="BackColor">Barva pozadí</param>
    ''' <param name="ForeColor">Barva popøedí</param>
    ''' <param name="Font">Písmo (nebo bude použito výchozí)</param>
    ''' <exception cref="PopulateException">Pøi nastavování hodnot vlastností z datové položky došlo k chybì.</exception>
    ''' <remarks>Pokud datová položka neposkytne vlastnost <see cref="IDayViewDataItem.Enabled"/> bude <see cref="Locked"/> nastaveno na false.
    ''' Barvy budou nastaveny podle parametrù konstruktor, i když je datová položka poskytne.</remarks>
    Public Sub New(ByVal DataItem As IDayViewDataItem, ByVal ForeColor As Color, ByVal BackColor As Color, Optional ByVal Font As Font = Nothing)
        With Me
            .Locked = False
            .DataItem = DataItem
            .BackColor = ForeColor
            .ForeColor = BackColor
            If Font IsNot Nothing Then .Font = Font
        End With
    End Sub
    ''' <summary>CTor z datové položky, zámku a barev</summary>
    ''' <param name="DataItem">Datová položka</param>
    ''' <param name="Locked">Zámek</param>
    ''' <param name="ForeColor">Barva popøedí</param>
    ''' <param name="BackColor">Barva pozadí</param>
    ''' <param name="Font">Písmo (nebo bude použito výchozí)</param>
    ''' <exception cref="PopulateException">Pøi nastavování hodnot vlastností z datové položky došlo k chybì.</exception>
    ''' <remarks>Hodnoty vlastnosti <see cref="Locked"/> a barev budou nastaveny z parametrù konstruktoru i pokud je datová položka poskytne.</remarks>
    Public Sub New(ByVal DataItem As IDayViewDataItem, ByVal Locked As Boolean, ByVal ForeColor As Color, ByVal BackColor As Color, Optional ByVal Font As Font = Nothing)
        With Me
            .DataItem = DataItem
            .Locked = Locked
            .BackColor = BackColor
            .ForeColor = ForeColor
            If Font IsNot Nothing Then .Font = Font
        End With
    End Sub
    ''' <summary>CTor z datové položky a zámku</summary>
    ''' <param name="DataItem">Datová položka</param>
    ''' <param name="Locked">Zámek</param>
    ''' <param name="Font">Písmo (nebo bude použito výchozí)</param>
    ''' <exception cref="PopulateException">Pøi nastavování hodnot vlastností z datové položky došlo k chybì.</exception>
    ''' <remarks>Pokud datová položka neposkytne baryv budou použity výchozí.
    ''' Hodnota vlastnosti <see cref="Locked"/> bude nastavena z parametrù konstruktoru, i když ji datová položka poskytne.
    ''' </remarks>
    Public Sub New(ByVal DataItem As IDayViewDataItem, ByVal Locked As Boolean, Optional ByVal Font As Font = Nothing)
        With Me
            .BackColor = SystemColors.Control
            .ForeColor = SystemColors.ControlText
            .DataItem = DataItem
            .Locked = Locked
            If Font IsNot Nothing Then .Font = Font
        End With
    End Sub

    ''' <summary>CTor (s barvami)</summary>
    ''' <param name="Start">Datum a èas zaèátku</param>
    ''' <param name="Length">Délka záznamu [min]</param>
    ''' <param name="Text">Text položky</param>
    ''' <param name="DataID">ID pøidružené položky v databázi</param>
    ''' <param name="Locked">Je položka chránìna proti pøesunùm</param>
    ''' <param name="Font">Písmo položky</param>
    ''' <param name="BackColor">Barva pozadí položky</param>
    ''' <param name="ForeColor">Barva popøedí položky</param>
    ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Length"/> je 0</exception>
    ''' <exception cref="ArgumentException">Záznam s takovýmito parametry <paramref name="Start"/> a <see cref="Length"/> by pøekraèoval hranici dne.</exception>
    <CLSCompliant(False)> _
    Public Sub New(ByVal Start As Date, ByVal Length As UShort, ByVal Text As String, ByVal BackColor As Color, ByVal ForeColor As Color, Optional ByVal DataID As Integer = 0, Optional ByVal Locked As Boolean = False, Optional ByVal Font As Font = Nothing)
        'TODO:Introduce CLS-compliant CTor
        With Me
            .Start = Start
            .Length = Length
            .Text = Text
            .BackColor = BackColor
            .ForeColor = ForeColor
            .DataID = DataID
            .Locked = Locked
            If Font IsNot Nothing Then .Font = Font
        End With
    End Sub
#End Region
#Region "Properties"
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Locked"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _Locked As Boolean
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Tag"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _Tag As Object
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Start"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _Start As Date
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Length"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _Length As UShort
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="BackColor"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _BackColor As Color
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="ForeColor"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _ForeColor As Color
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Font"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _Font As Font
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Text"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _Text As String
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="UserChanged"/></summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private _UserChanged As String
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="DataID"/></summary>
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> Private _DataID As Integer
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="DataItem"/></summary>
    <DebuggerBrowsable(DebuggerBrowsableState.Never)> Private _DataItem As Object
    ''' <summary>Urèuje jestli je položka odemèena - lze s ní pohybovat a smazat ji</summary>
    Public Property Locked() As Boolean
        Get
            Return _Locked
        End Get
        Set(ByVal value As Boolean)
            Dim Old As Boolean = value
            _Locked = value
            If Old <> value Then OnChanged(New IReportsChange.ValueChangedEventArgs(Of Boolean)(Old, value, "Locked"))
        End Set
    End Property
    ''' <summary>Tag k položce</summary>
    Public Property Tag() As Object
        Get
            Return _Tag
        End Get
        Set(ByVal value As Object)
            Dim Old As Object = value
            _Tag = value
            If Not Old Is value Then OnChanged(New IReportsChange.ValueChangedEventArgs(Of Object)(Old, value, "Tag"))
        End Set
    End Property
    ''' <summary>Datum a èas zaèátku záznamu</summary>
    ''' <exception cref="ArgumentException">Záznam má takovou délku, že nastavení jeho zaèátku na nastavovanou hodnotu by zpùsobilo, že pøekroèí hranici dne</exception>
    Public Property Start() As Date
        <DebuggerStepThrough()> Get
            Return _Start
        End Get
        Set(ByVal value As Date)
            Dim EndDate As Date = value + TimeSpan.FromMinutes(Length)
            If value.Date <> EndDate.Date AndAlso Not (EndDate.AddDays(-1) = Start.Date AndAlso EndDate.TimeOfDay = TimeSpan.Zero) Then
                Throw New ArgumentException("Záznam nemùže pøekroèit hranici dne", "value")
            End If
            Dim Old As Date = value
            _Start = value
            If Old <> value Then OnChanged(New IReportsChange.ValueChangedEventArgs(Of Date)(Old, value, "Start"))
        End Set
    End Property
    ''' <summary>Trvání záznamu v minutách</summary>
    ''' <exception cref="ArgumentOutOfRangeException">Nastavovaná hodnota je 0</exception>
    ''' <exception cref="ArgumentException">Záznam s nastavovanou délkou by zasahoval do následujícího dne</exception>
    <CLSCompliant(False)> _
    Public Property Length() As UShort
        <DebuggerStepThrough()> Get
            Return _Length
        End Get
        Set(ByVal value As UShort)
            Dim EndDate As Date = Start + TimeSpan.FromMinutes(value)
            If Start.Date <> EndDate.Date AndAlso Not (EndDate.AddDays(-1) = Start.Date AndAlso EndDate.TimeOfDay = TimeSpan.Zero) Then
                Throw New ArgumentException("Záznam nemùže pøekroèit hranici dne", "value")
            ElseIf value = 0 Then
                Throw New ArgumentOutOfRangeException("value", "Záznam musí mít nenulovou délku")
            End If
            Dim Old As UShort = value
            _Length = value
            If Old <> value Then OnChanged(New IReportsChange.ValueChangedEventArgs(Of UShort)(Old, value, "Length"))
        End Set
    End Property
    ''' <summary>Konec záznamu</summary>
    Public ReadOnly Property [End]() As Date
        Get
            Return Start + TimeSpan.FromMinutes(Length)
        End Get
    End Property
    ''' <summary>Barva pozadí záznamu</summary>
    Public Property BackColor() As Color
        <DebuggerStepThrough()> Get
            Return _BackColor
        End Get
        Set(ByVal value As Color)
            Dim Old As Color = value
            _BackColor = value
            If Old <> value Then OnChanged(New IReportsChange.ValueChangedEventArgs(Of Color)(Old, value, "BackColor"))
        End Set
    End Property
    ''' <summary>Barva popøedí záznamu</summary>
    Public Property ForeColor() As Color
        <DebuggerStepThrough()> Get
            Return _ForeColor
        End Get
        Set(ByVal value As Color)
            Dim Old As Color = value
            _ForeColor = value
            If Old <> value Then OnChanged(New IReportsChange.ValueChangedEventArgs(Of Color)(Old, value, "ForeColor"))
        End Set
    End Property
    ''' <summary>Font záznamu</summary>
    Public Property Font() As Font
        Get
            Return _Font
        End Get
        Set(ByVal value As Font)
            Dim Old As Font = value
            _Font = value
            If Not Old.Equals(value) Then OnChanged(New IReportsChange.ValueChangedEventArgs(Of Font)(Old, value, "Font"))
        End Set
    End Property
    ''' <summary>Text záznamu</summary>
    Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            Dim Old As String = value
            _Text = value
            If Old <> value Then OnChanged(New IReportsChange.ValueChangedEventArgs(Of String)(Old, value, "Text"))
        End Set
    End Property
    ''' <summary>Indikuje jestli záznam byl zmìnìn uživatelem</summary>
    Public Property UserChanged() As Boolean
        Get
            Return _UserChanged
        End Get
        Set(ByVal value As Boolean)
            Dim Old As Boolean = value
            _UserChanged = value
            If Old <> value Then OnChanged(New IReportsChange.ValueChangedEventArgs(Of Boolean)(Old, value, "UserChanged"))
        End Set
    End Property
    ''' <summary>ID datové položky øádky</summary>
    Public Property DataID() As Integer
        <DebuggerStepThrough()> Get
            Return _DataID
        End Get
        Set(ByVal value As Integer)
            Dim Old As Integer = _DataID
            _DataID = value
            If Old <> value Then OnChanged(New IReportsChange.ValueChangedEventArgs(Of Integer)(Old, value, "RowID"))
        End Set
    End Property
    ''' <summary>Datová položka, která naplnila tento øádek</summary>
    ''' <value>Pokud používáte dataBinding, nemìòte hodnotu véto vlastnosti!</value>
    ''' <exception cref="PopulateException">Value beign set is <see cref="IDayViewDataItem"/> and <see cref="PopulateException"/> ocuured in <see cref="PopulateFromDataItem"/>. In such case value of the <see cref="DataItem"/> property is changed, but populated properties remains unchanged.</exception>
    Public Property DataItem() As Object
        <DebuggerStepThrough()> Get
            Return _DataItem
        End Get
        Set(ByVal value As Object)
            Static Setting As Boolean
            If Setting Then Throw New RecursionException("Property DataItem cannot be set while setter is already on call-stack.")
            Setting = True
            Try
                Dim Old As Object = _DataItem
                If TypeOf value Is IDayViewDataItem AndAlso DirectCast(value, IDayViewDataItem).IsWrapper Then _
                    _DataItem = DirectCast(value, IDayViewDataItem).Item _
                Else _DataItem = value
                If Old IsNot value Then
                    OnChanged(New IReportsChange.ValueChangedEventArgs(Of Object)(Old, value, "RowItem"))
                    If TypeOf value Is IDayViewDataItem Then
                        PopulateFromDataItem(value)
                    End If
                End If
            Finally
                Setting = False
            End Try
        End Set
    End Property
    ''' <summary>Populates properties of item by properties of <see cref="IDayViewDataItem"/></summary>
    ''' <param name="DataItem">Item to populate this <see cref="DayViewItem"/> from</param>
    ''' <exception cref="PopulateException">Exception ocured while populating this <see cref="DayViewItem"/> from <paramref name="DataItem"/>. In such case population is aborted and populated properties are reset to their original values.</exception>
    ''' <remarks>
    ''' The <see cref="Changed"/> events are buffered during populating of properties and are raised only when populating is successfull.
    ''' When <paramref name="DataItem"/>.<see cref="IDayViewDataItem.IsWrapper">ISWrapper</see> is true the caller is responsible of assigning <paramref name="DataItem"/>.<see cref="IDayViewDataItem.Item">Item</see> to <see cref="DataItem"/> instead of <paramref name="DataItem"/> itself.
    ''' </remarks>
    Protected Overridable Sub PopulateFromDataItem(ByVal DataItem As IDayViewDataItem)
        Dim bStart As Date = Me.Start
        Dim bLength As UShort = Me.Length
        Dim bText As String = Me.Text
        Dim bBackColor As Color = Me.BackColor
        Dim bForeColor As Color = Me.ForeColor
        Dim bLocked As Boolean = Me.Locked
        Dim prp As String = ""
        Try
            Me.BufferChangedEvent = True
            With DataItem
                prp = "Start"
                Me.Start = .Date.Date + .StartTime
                prp = "Length"
                Me.Length = (.EndTime - .StartTime).TotalMinutes
                prp = "Text"
                Me.Text = .ToString
                prp = "Locked"
                If .EnabledImplemented Then Me.Locked = Not .Enabled
                prp = "ForeColor"
                If .ForeColorImplemented Then Me.ForeColor = .ForeColor
                prp = "BackColor"
                If .BackColorImplemented Then Me.BackColor = .BackColor
            End With
        Catch ex As Exception
            Me.Start = bStart
            Me.Length = bLength
            Me.Text = bText
            Me.BackColor = bBackColor
            Me.ForeColor = bForeColor
            Me.Locked = bLocked
            Me.ChangedBuffer.Clear()
            Throw New PopulateException(String.Format("Error while populating DayViewItem from {0}.", ex.GetType.Name), prp, ex)
        Finally
            Me.BufferChangedEvent = False
        End Try
    End Sub
    ''' <summary>Exception thrown when there was an exception while populating <see cref="DayViewItem"/>'s properties from data item</summary>
    ''' <remarks><see cref="PopulateException.ParamName"/> contains name of <see cref="DayView"/>'s property which's population caused the exception. Exception could be caused either by setter of this property or by getter of corresponding property of data item.</remarks>
    Public Class PopulateException : Inherits ArgumentException
        ''' <summary>CTor</summary>
        ''' <param name="message">Error message</param>
        ''' <param name="paramname">Name of <see cref="DayView"/>'s property which's population caused the exception</param>
        ''' <param name="InnerException">Exception taht caused this exception to be thrown</param>
        Public Sub New(ByVal message As String, ByVal paramname As String, ByVal InnerException As Exception)
            MyBase.New(message, paramname, InnerException)
        End Sub
    End Class
    ''' <summary>Contains value of the <see cref="BufferChangedEvent"/> property</summary>
    Private _BufferChagedEvent As Boolean
    ''' <summary>Gets or sets value indicating if <see cref="Changed"/> is buffered</summary>
    ''' <value>
    ''' When set to true the <see cref="Changed"/> event stops raising on change of property and is instead dtored in <see cref="ChangedBuffer"/>.
    ''' When set to false the <see cref="ChangedBuffer"/> is emptied by raising all the events in buffer and the <see cref="Changed"/> event starts raising on change of property."
    ''' </value>
    ''' <returns>Value idicating if <see cref="OnChanged"/> buffers the <see cref="Changed"/> event when called (true) or raises it immediately (false).</returns>
    ''' <remarks>When exception occures in event handler of buffered event while raising events from buffer on value-change from true to false the <see cref="ChangedBuffer"/> is emptied without raising remaining events in the buffer and the exception is rethrown.</remarks>
    Protected Property BufferChangedEvent() As Boolean
        <DebuggerStepThrough()> Get
            Return _BufferChagedEvent
        End Get
        Set(ByVal value As Boolean)
            _BufferChagedEvent = value
            If Not value Then
                While ChangedBuffer.Count > 0
                    Try
                        RaiseEvent Changed(Me, ChangedBuffer.Dequeue)
                    Catch ex As Exception
                        ChangedBuffer.Clear()
                        Throw
                    End Try
                End While
            End If
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="ChangedBuffer"/> rpoperty</summary>
    Private _ChangedBuffer As Queue(Of EventArgs)
    ''' <summary>Gets buffer for buffering the <see cref="Changed"/> events when <see cref="BufferChangedEvent"/> is true</summary>
    Protected ReadOnly Property ChangedBuffer() As Queue(Of EventArgs)
        Get
            If _ChangedBuffer Is Nothing Then _ChangedBuffer = New Queue(Of EventArgs)
            Return _ChangedBuffer
        End Get
    End Property
    ''' <summary>Raises the <see cref="Changed"/> event</summary>
    ''' <param name="e">Event arguments</param>
    ''' <remarks>If <see cref="BufferChangedEvent"/> is true the event is not raised but rather buffered in <see cref="ChangedBuffer"/> and raised when value of <see cref="BufferChangedEvent"/> changes from true to false.</remarks>
    Protected Overridable Sub OnChanged(ByVal e As System.EventArgs)
        If BufferChangedEvent Then
            ChangedBuffer.Enqueue(e)
        Else
            RaiseEvent Changed(Me, e)
        End If
    End Sub
#End Region
End Class