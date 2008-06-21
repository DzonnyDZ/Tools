Imports System.ComponentModel
Imports Tools.ComponentModelT

''' <summary>Spoleèné rozhraní pro tabulky importované tøídou OracleImporter</summary>
Public Interface ITable(Of T) : Inherits IEnumerable(Of IEnumerable(Of T))
    ''' <summary>Poèet øádek tabulky</summary>
    ReadOnly Property RowsCount() As Integer
    ''' <summary>Poèet sloupcù tabulky - musí být ve všech øádcích stejný</summary>
    ReadOnly Property ColumnsCount() As Integer
End Interface


''' <summary>Implementuje CSV formát</summary>
Public Class CSV
    Implements IList 'Aby DataGridView pochopil, že toto je kolekce
    Implements IList(Of CSV.ListInternal)
    Implements IBindingList
    Implements ITable(Of String)
    'Implements ITypedList
#Region "Properties"
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="RowsInternal"/></summary>
    Private _Rows As New List(Of ListInternal)
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Separator"/></summary>
    Private _Separator As Char
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Quote"/></summary>
    Private _Quote As Char = """"c
    ''' <summary>Módy oøezání</summary>
    <Flags()> Public Enum TrimMode
        ''' <summary>Neoøezávat</summary>
        None = 0
        ''' <summary>Oøezat zaèátek</summary>
        Start = 1
        ''' <summary>Oøezat konec</summary>
        [End] = 2
        ''' <summary>Ožezat konec a zaèátek</summary>
        Both = Start Or [End]
    End Enum
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="TrimItems"/></summary>
    Private _TrimItems As TrimMode = TrimMode.None
    'Private _DecimalSeparator As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
    'Private _ThousandSeparator As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator

    ''' <summary>Øádky v souboru</summary>
    Public ReadOnly Property RowsInternal() As List(Of ListInternal)
        Get
            Return _Rows
        End Get
    End Property
    ''' <summary>Zpùsob úpravy býlých znakù na konci a zaèátku položek</summary>
    Public Property TrimItems() As TrimMode
        Get
            Return _TrimItems
        End Get
        Set(ByVal value As TrimMode)
            _TrimItems = value
        End Set
    End Property
    ''' <summary>Oddìlovaè sloupcù</summary>
    Public Property Separator() As Char
        Get
            Return _Separator
        End Get
        Set(ByVal value As Char)
            _Separator = value
        End Set
    End Property
    ''' <summary>Specifikátor øetìzcù</summary>
    Public Property Quote() As Char
        Get
            Return _Quote
        End Get
        Set(ByVal value As Char)
            _Quote = value
        End Set
    End Property
#End Region
    ''' <summary>CTor</summary>
    ''' <exception cref="InvalidOperationException">System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator is empty or contains more than 1 character</exception>
    Public Sub New()
        Try
            Separator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator
        Catch
            Throw New InvalidOperationException("Cannot set separator to multiple characters")
        End Try
    End Sub
    ''' <summary>CTor bez naètení obsahu</summary>
    ''' <param name="Separator">Oddìlovaè sloupcù</param>
    ''' <param name="Quote">Textový kvalifikátor</param>
    Public Sub New(ByVal Separator As String, Optional ByVal Quote As Char = """"c)
        Me.Separator = Separator
        Me.Quote = Quote
    End Sub
    'Private Enum LineStates
    '    Text
    '    Cr
    'End Enum


    ''' <summary>Naèíst CSV</summary>
    ''' <param name="r">Odkud</param>
    ''' <param name="EmptyLines">Zpùsob chování k prázdným øádkùm</param>
    Public Overridable Sub Load(ByVal r As IO.TextReader, Optional ByVal EmptyLines As EmptyLines = EmptyLines.IgnoreOnlyOne)
        Dim i As Integer = 0
        Errors.Clear()
        'Dim state As LineStates = LineStates.Text
        'Dim b As New System.Text.StringBuilder
        Do
            'Dim chcode As Integer = r.Read
            'Dim LineEnd As Boolean = False
            'Try
            '    Select Case state
            '        Case LineStates.Text
            '            Select Case chcode
            '                Case AscW(vbLf) : LineEnd = True
            '                Case -1 : LineEnd = True : Exit Do
            '                Case AscW(vbCr) : LineEnd = True : state = LineStates.Cr
            '                Case Else : b.Append(ChrW(chcode))
            '            End Select
            '        Case LineStates.Cr
            '            Select Case chcode
            '                Case -1 : LineEnd = True : Exit Do
            '                Case AscW(vbLf) : state = LineStates.Text
            '                Case Else : b.Append(ChrW(chcode)) : state = LineStates.Text
            '            End Select
            '    End Select
            'Finally
            '   If LineEnd Then
            Dim Line As String = r.ReadLine ' b.ToString
            'b = New System.Text.StringBuilder
            If Line Is Nothing Then Exit Do
            Dim Row As New ListInternal(Me)
            RowsInternal.Add(Row)
            Row.AddRange(ParseLine(Line, i + 1))
            i = i + 1
            'End If
            'End Try
        Loop
        If ((EmptyLines And CSV.EmptyLines.IgnoreOnlyOne) = CSV.EmptyLines.IgnoreOnlyOne) AndAlso RowsInternal.Count = 1 AndAlso (RowsInternal(0).Count = 0 OrElse (RowsInternal(0).Count = 1 AndAlso (RowsInternal(0)(0) = "" OrElse RowsInternal(0)(0) Is Nothing))) Then
            RowsInternal.Clear()
        End If
        If (EmptyLines And CSV.EmptyLines.IgnoreAtBeginning) = CSV.EmptyLines.IgnoreAtBeginning Then
            i = 0
            Do While i < RowsInternal.Count AndAlso (RowsInternal(i).Count = 0 OrElse (RowsInternal(i).Count = 1 AndAlso (RowsInternal(i)(0) = "" OrElse RowsInternal(i)(0) Is Nothing)))
                i += 1
            Loop
            If i > 0 Then RowsInternal.RemoveRange(0, i)
        End If
        If (EmptyLines And CSV.EmptyLines.IgnoreAtEnd) = CSV.EmptyLines.IgnoreAtEnd Then
            i = 0
            Do While i < RowsInternal.Count AndAlso (RowsInternal(RowsInternal.Count - i - 1).Count = 0 OrElse (RowsInternal(RowsInternal.Count - i - 1).Count = 1 AndAlso (RowsInternal(RowsInternal.Count - i - 1)(0) = "" OrElse RowsInternal(RowsInternal.Count - i - 1)(0) Is Nothing)))
                i += 1
            Loop
            If i > 0 Then RowsInternal.RemoveRange(RowsInternal.Count - i, i)
        End If
        If (EmptyLines And CSV.EmptyLines.IgnoreInside) = CSV.EmptyLines.IgnoreInside Then
            Dim AtStart As Integer = 0
            Do While AtStart < RowsInternal.Count AndAlso (RowsInternal(AtStart).Count = 0 OrElse (RowsInternal(AtStart).Count = 1 AndAlso (RowsInternal(AtStart)(0) = "" OrElse RowsInternal(AtStart)(0) Is Nothing)))
                AtStart += 1
            Loop
            Dim AtEnd As Integer = 0
            Do While AtEnd < RowsInternal.Count AndAlso (RowsInternal(RowsInternal.Count - AtEnd - 1).Count = 0 OrElse (RowsInternal(RowsInternal.Count - AtEnd - 1).Count = 1 AndAlso (RowsInternal(RowsInternal.Count - AtEnd - 1)(0) = "" OrElse RowsInternal(RowsInternal.Count - AtEnd - 1)(0) Is Nothing)))
                AtEnd += 1
            Loop
            Dim [Rem] As New List(Of Integer)
            For i = AtStart To RowsInternal.Count - 1 - AtEnd
                If RowsInternal(RowsInternal.Count - i - 1).Count = 0 OrElse (RowsInternal(RowsInternal.Count - i - 1).Count = 1 AndAlso (RowsInternal(RowsInternal.Count - i - 1)(0) = "" OrElse RowsInternal(RowsInternal.Count - i - 1)(0) Is Nothing)) Then _
                  [Rem].Add(i)
            Next i
            For i = [Rem].Count - 1 To 0 Step -1
                RowsInternal.RemoveAt([Rem](i))
            Next i
        End If
    End Sub
    ''' <summary>Stavy automatu na parzování øádku</summary>
    ''' <remarks>V popiskách ovažováno CSV oddìlená èárkou (,) a delimitované uvozovkou (")</remarks>
    Private Enum LineParseState
        ''' <summary>Zaèátek položky - na zaèátku stringu nebo pøi nalezení ,</summary>
        Start
        ''' <summary>Mezera (tabulátor) na zaèátku položky</summary>
        Space
        ''' <summary>Po nalezení " na zaèátku položky nebo po mezerách (tabech) na zaèátku položky</summary>
        Quote
        ''' <summary>Neuvozovkovaný text položky</summary>
        Item
        ''' <summary>Za uzavírací uvozovkou</summary>
        After
    End Enum
    ''' <summary>Rozparzuje øádek textu na jednotlivé položky</summary>
    ''' <param name="Line">Øádek k rozparzování</param>
    ''' <param name="LNo">Èíslo aktuální øádky (1-based)</param>
    ''' <returns>Rozparzovaný øádek</returns>
    Protected Overridable Function ParseLine(ByVal Line As String, ByVal LNo As Integer) As IEnumerable(Of String)
        If Line = "" Then Return New String() {}
        Dim State As LineParseState = LineParseState.Start
        Dim i As Integer = 0
        Dim ret As New List(Of String)
        Dim ColStart As Integer
        Dim Reported As Boolean = False
        Do
            Select Case State
                Case LineParseState.Start 'Zaèátek
                    Select Case Line(i)
                        Case " "c, CChar(vbTab)
                            ColStart = i
                            State = LineParseState.Space
                        Case Quote
                            ColStart = i + 1
                            State = LineParseState.Quote
                        Case Separator
                            ret.Add("")
                        Case Else
                            ColStart = i
                            State = LineParseState.Item
                    End Select
                Case LineParseState.Item 'Neuvozovkovaná položka
                    Select Case Line(i)
                        Case Separator
                            ret.Add(Line.Substring(ColStart, i - ColStart))
                            State = LineParseState.Start
                    End Select
                Case LineParseState.Space 'Mezera na zaèátku položky
                    Select Case Line(i)
                        Case " "c, CChar(vbTab) 'Do hothing
                        Case Separator
                            ret.Add(Line.Substring(ColStart, i - ColStart))
                            State = LineParseState.Start
                        Case Quote
                            State = LineParseState.Quote
                            ColStart = i + 1
                        Case Else
                            State = LineParseState.Item
                    End Select
                Case LineParseState.Quote 'Uvozovkovaná položka
                    Select Case Line(i)
                        Case Quote
                            ret.Add(Line.Substring(ColStart, i - ColStart))
                            State = LineParseState.After
                    End Select
                Case LineParseState.After 'Za uvozovkovanou položkou
                    Select Case Line(i)
                        Case " "c, CChar(vbTab) 'Do nothing
                        Case Separator
                            Reported = False
                            State = LineParseState.Start
                        Case Else
                            If Not Reported Then
                                Reported = True
                                Errors.Add(New CSVErrorException(LNo, i + 1, ret.Count, "Za uzavírací uvozovkou položky uzavøené v uvozovkách byl nalezen text."))
                            End If
                    End Select
            End Select
            i = i + 1
            If i = Line.Length Then
                Select Case State
                    Case LineParseState.After 'Do nothing
                    Case LineParseState.Item : ret.Add(Line.Substring(ColStart, i - ColStart))
                    Case LineParseState.Quote
                        ret.Add(Line.Substring(ColStart, i - ColStart))
                        Errors.Add(New CSVErrorException(LNo, i - 1, ret.Count, "Chybí uzavírací uvozovka poslední položky øádku."))
                    Case LineParseState.Space : ret.Add(Line.Substring(ColStart, i - 1 - ColStart))
                    Case LineParseState.Start : ret.Add("")
                End Select
                Exit Do
            End If
        Loop
        If TrimItems <> TrimMode.None Then
            For ti As Integer = 0 To ret.Count - 1
                Select Case TrimItems And TrimMode.Both
                    Case TrimMode.Both
                        ret(ti) = ret(ti).Trim
                    Case TrimMode.End
                        ret(ti) = ret(ti).TrimEnd
                    Case TrimMode.Start
                        ret(ti) = ret(ti).TrimStart
                End Select
            Next ti
        End If
        Return ret
    End Function
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Errors"/></summary>
    Private _Errors As New List(Of CSVErrorException)
    ''' <summary>Seznam chyb pøi naèítání</summary>
    <CLSCompliant(False)> _
    Public ReadOnly Property Errors() As List(Of CSVErrorException)
        Get
            Return _Errors
        End Get
    End Property
    ''' <summary>Naèíst CSV ze stringu</summary>
    ''' <param name="Str">Øetìzec obsahující CSV</param>
    ''' <param name="EmptyLines">Zpùsob chování k prázdným øádkùm</param>
    Public Sub Load(ByVal Str As String, Optional ByVal EmptyLines As EmptyLines = EmptyLines.IgnoreOnlyOne)
        Load(New IO.StringReader(Str), EmptyLines)
    End Sub
    ''' <summary>Zpùsob chování se k prázdným øádkùm</summary>
    <Flags()> Public Enum EmptyLines As Byte
        ''' <summary>Èíst všechny</summary>
        ReadAll = 0
        ''' <summary>Ignorovat na zaèátku souboru</summary>
        IgnoreAtBeginning = 1
        ''' <summary>Ignorovat na konci souboru</summary>
        IgnoreAtEnd = 2
        ''' <summary>Ignorovat na zaèátku a na konci souboru</summary>
        IgnoreAtEdges = IgnoreAtBeginning Or IgnoreAtEnd
        ''' <summary>Ignorovat uvnitø souboru</summary>
        IgnoreInside = 4
        ''' <summary>Ignorovat všechny prázdné øádky</summary>
        IgnoreAll = IgnoreInside Or IgnoreAtEdges
        ''' <summary>Ignorovat pokud se soubor stává z jediného prázdného øádku</summary>
        IgnoreOnlyOne = 8
    End Enum
    ''' <summary>Maximální poèet sloupcù v øádku</summary>
    Public ReadOnly Property MaxColumns() As UInteger
        Get
            Dim Max As UInteger = 0
            For Each Row As List(Of String) In RowsInternal
                If Row IsNot Nothing AndAlso Row.Count > Max Then Max = Row.Count
            Next Row
            Return Max
        End Get
    End Property
    ''' <summary>Minimální poèet sloupcù v øádku</summary>
    Public ReadOnly Property MinColumns() As UInteger
        Get
            If RowsInternal.Count = 0 Then Return 0
            Dim Min As UInteger = UInteger.MaxValue
            For Each Row As List(Of String) In RowsInternal
                If Row Is Nothing Then
                    Min = 0
                ElseIf Row IsNot Nothing AndAlso Row.Count < Min Then
                    Min = Row.Count
                End If
                If Min = 0 Then Exit For
            Next Row
            Return Min
        End Get
    End Property
    ''' <summary>Zjistí nejèastìji se vyskytující poèet sloupcù</summary>
    <CLSCompliant(False)> _
    Public ReadOnly Property MeanColCount() As UInteger
        Get
            Dim ColCounts As New Dictionary(Of UInteger, UInteger)
            For Each row As List(Of String) In RowsInternal
                If ColCounts.ContainsKey(row.Count) Then ColCounts(row.Count) += 1 Else ColCounts.Add(row.Count, 1)
            Next row
            Dim Max As UInteger = 0, MaxN As UInteger = 0
            For Each kvp As KeyValuePair(Of UInteger, UInteger) In ColCounts
                If kvp.Value > Max Then
                    Max = kvp.Value
                    MaxN = kvp.Key
                End If
            Next kvp
            Return MaxN
        End Get
    End Property


    ''' <summary>Indikuje chybu v CSV souboru</summary>
    <CLSCompliant(False)> _
    Public Class CSVErrorException : Inherits Exception
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Line"/></summary>
        Private ReadOnly _Line As Integer
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Char"/></summary>
        Private ReadOnly _Char As Integer
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="CSVColumn"/></summary>
        Private ReadOnly _CSVColumn As Integer
        ''' <summary>CTor</summary>
        ''' <param name="Line">Øádek chyby</param>
        ''' <param name="Character">Znak chyby (1-based)</param>
        ''' <param name="CSVColumn">CSV sloupec s chybou (1-based)</param>
        ''' <param name="Message">Popis chyby (0-based)</param>
        ''' <param name="InnerException">Vnitøní chyba</param>
        Public Sub New(ByVal Line As Integer, ByVal Character As Integer, ByVal CSVColumn As Integer, ByVal Message As String, Optional ByVal InnerException As Exception = Nothing)
            MyBase.New(Message, InnerException)
            _Line = Line
            _Char = Character
            _CSVColumn = CSVColumn
        End Sub
        ''' <summary>Øádek chyby (1-based)</summary>
        Public ReadOnly Property Line() As UInteger
            Get
                Return _Line
            End Get
        End Property
        ''' <summary>Znak chyby na øádku (1-based)</summary>
        Public ReadOnly Property Character() As UInteger
            Get
                Return _Char
            End Get
        End Property
        ''' <summary>CSV sloupec chyby</summary>
        Public ReadOnly Property CSVColumn() As Integer
            Get
                Return _CSVColumn
            End Get
        End Property
        ''' <summary>Textová reprezantace</summary>
        Public Overrides Function ToString() As String
            Return String.Format("{0} (Øádek {1}, Znak {2}, Sloupec {3})", Message, Line, Character, CSVColumn)
        End Function
    End Class
#Region "IList" 'Implementace rùzných interfacù využívaných DataGridem
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
        CopyTo(DirectCast(array, ListInternal()), index)
    End Sub
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Sub CopyTo(ByVal array() As ListInternal, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of ListInternal).CopyTo
        Throw New NotImplementedException
    End Sub
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count, System.Collections.Generic.ICollection(Of ListInternal).Count
        <DebuggerStepThrough()> Get
            Return RowsInternal.Count
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
        Get
            Throw New NotImplementedException
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
        Get
            Throw New NotImplementedException
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Function _GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of ListInternal) Implements System.Collections.Generic.IEnumerable(Of ListInternal).GetEnumerator
        Return RowsInternal.GetEnumerator
    End Function
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Function _Add(ByVal value As Object) As Integer Implements System.Collections.IList.Add
        Add(value)
        Return Me.Count - 1
    End Function
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Sub Add(ByVal item As ListInternal) Implements System.Collections.Generic.ICollection(Of ListInternal).Add
        Me.RowsInternal.Add(item)
    End Sub
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Sub Clear() Implements System.Collections.IList.Clear, System.Collections.Generic.ICollection(Of ListInternal).Clear
        RowsInternal.Clear()
    End Sub
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Function Contains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
        Return TypeOf value Is ListInternal AndAlso Contains(DirectCast(value, ListInternal))
    End Function
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Public Function Contains(ByVal item As ListInternal) As Boolean Implements System.Collections.Generic.ICollection(Of ListInternal).Contains
        Return Me.RowsInternal.Contains(item)
    End Function
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Function IndexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
        If TypeOf value Is ListInternal Then Return IndexOf(DirectCast(value, ListInternal))
        Return -1
    End Function
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Function IndexOf(ByVal item As ListInternal) As Integer Implements System.Collections.Generic.IList(Of ListInternal).IndexOf
        Return RowsInternal.IndexOf(item)
    End Function
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Sub Insert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
        Insert(index, DirectCast(value, ListInternal))
    End Sub
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Sub Insert(ByVal index As Integer, ByVal item As ListInternal) Implements System.Collections.Generic.IList(Of ListInternal).Insert
        RowsInternal.Insert(index, item)
    End Sub
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property IsFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
        Get
            Return True
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.IList.IsReadOnly, System.Collections.Generic.ICollection(Of ListInternal).IsReadOnly
        Get
            Return True
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Property _Item(ByVal index As Integer) As Object Implements System.Collections.IList.Item
        Get
            Return Item(index)
        End Get
        Set(ByVal value As Object)
            Item(index) = value
        End Set
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Property Item(ByVal index As Integer) As ListInternal Implements System.Collections.Generic.IList(Of ListInternal).Item
        Get
            Return Me.RowsInternal(index)
        End Get
        Set(ByVal value As ListInternal)
            Me.RowsInternal(index) = value
        End Set
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Sub Remove(ByVal value As Object) Implements System.Collections.IList.Remove
        If TypeOf value Is ListInternal Then Remove(DirectCast(value, ListInternal))
    End Sub
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Function Remove(ByVal item As ListInternal) As Boolean Implements System.Collections.Generic.ICollection(Of ListInternal).Remove
        Return RowsInternal.Remove(item)
    End Function
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Sub RemoveAt(ByVal index As Integer) Implements System.Collections.IList.RemoveAt, System.Collections.Generic.IList(Of ListInternal).RemoveAt
        RowsInternal.RemoveAt(index)
    End Sub
#Region "IBindingList"  'Vrací False nebo vyhazuje NotSupportedException
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Sub AddIndex(ByVal [property] As System.ComponentModel.PropertyDescriptor) Implements System.ComponentModel.IBindingList.AddIndex
        Throw New NotSupportedException
    End Sub
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Function AddNew() As Object Implements System.ComponentModel.IBindingList.AddNew
        Return New ListInternal(Me)
    End Function
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property AllowEdit() As Boolean Implements System.ComponentModel.IBindingList.AllowEdit
        Get
            Return True
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property AllowNew() As Boolean Implements System.ComponentModel.IBindingList.AllowNew
        Get
            Return True
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property AllowRemove() As Boolean Implements System.ComponentModel.IBindingList.AllowRemove
        Get
            Return True
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Sub ApplySort(ByVal [property] As System.ComponentModel.PropertyDescriptor, ByVal direction As System.ComponentModel.ListSortDirection) Implements System.ComponentModel.IBindingList.ApplySort
        Throw New NotSupportedException
    End Sub
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Function Find(ByVal [property] As System.ComponentModel.PropertyDescriptor, ByVal key As Object) As Integer Implements System.ComponentModel.IBindingList.Find
        Throw New NotSupportedException
    End Function
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property IsSorted() As Boolean Implements System.ComponentModel.IBindingList.IsSorted
        Get
            Return False
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Event ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Implements System.ComponentModel.IBindingList.ListChanged
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Sub RemoveIndex(ByVal [property] As System.ComponentModel.PropertyDescriptor) Implements System.ComponentModel.IBindingList.RemoveIndex
        Throw New NotSupportedException
    End Sub
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private Sub RemoveSort() Implements System.ComponentModel.IBindingList.RemoveSort
        Throw New NotSupportedException
    End Sub
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property SortDirection() As System.ComponentModel.ListSortDirection Implements System.ComponentModel.IBindingList.SortDirection
        Get
            Throw New NotSupportedException
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property SortProperty() As System.ComponentModel.PropertyDescriptor Implements System.ComponentModel.IBindingList.SortProperty
        Get
            Throw New NotSupportedException
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property SupportsChangeNotification() As Boolean Implements System.ComponentModel.IBindingList.SupportsChangeNotification
        Get
            Return False
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property SupportsSearching() As Boolean Implements System.ComponentModel.IBindingList.SupportsSearching
        Get
            Return False
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ReadOnly Property SupportsSorting() As Boolean Implements System.ComponentModel.IBindingList.SupportsSorting
        Get
            Return False
        End Get
    End Property
#End Region
#End Region
#Region "Type descriptor" 'Implementuje type descriptor pro ListInternal, aby se tváøil, že má vlastnosti èíslované podle svých položek
    ''' <summary>Dìdí od <see cref="List(Of String)"/> za jadiným úèelem - aby na nìm byl aplikována <see cref="TypeDescriptionProviderAttribute"/>.</summary>
    <TypeDescriptionProvider(GetType(ListDescriptionProvider))> _
    Public Class ListInternal : Inherits List(Of String)
        Public Sub New(ByVal Owner As CSV)
            Me.Owner = Owner
        End Sub
        Friend Owner As CSV
    End Class
    ''' <summary>Implementuje <see cref="TypeDescriptionProvider"/> pro <see cref="ListInternal"/></summary>
    Private Class ListDescriptionProvider : Inherits TypeDescriptionProvider
        ''' <summary>Gets a custom type descriptor for the given type and object.</summary>
        ''' <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
        ''' <param name="instance">An instance of the type. Can be null if no instance was passed to the System.ComponentModel.TypeDescriptor.</param>
        ''' <returns>An System.ComponentModel.ICustomTypeDescriptor that can provide metadata for the type.</returns>
        Public Overrides Function GetTypeDescriptor(ByVal objectType As System.Type, ByVal instance As Object) As System.ComponentModel.ICustomTypeDescriptor
            If objectType.Equals(GetType(ListInternal)) Then Return New ListTypeDescriptor(instance, DirectCast(instance, ListInternal).Owner.MaxColumns)
            Return MyBase.GetTypeDescriptor(objectType, instance)
        End Function
    End Class
    ''' <summary>Implementuje <see cref="ICustomTypeDescriptor"/> pro <see cref="ListInternal"/></summary>
    Private Class ListTypeDescriptor : Inherits CustomTypeDescriptorBase(Of ListInternal)
        ''' <summary>Maximální poèet sloupcù</summary>
        Private MaxCols As Integer
        ''' <summary>CTor</summary>
        ''' <param name="Instance">Instance pro, kterou bude popis poskytnut - DataGridView sem s oblibou cpe nothing :-(</param>
        ''' <param name="MaxCols">Maximální oèet sloupcù</param>
        Public Sub New(ByVal Instance As List(Of String), ByVal MaxCols As Integer)
            MyBase.New(Instance)
            Me.MaxCols = MaxCols
        End Sub
        ''' <summary>Vrátí popisy vlastností - jedna vlastnost na jednu položku seznamu</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
        ''' <remarks>Pokud <see cref="Instance"/> je null vrací prázdný seznam</remarks>
        Public Overrides Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection
            If Instance Is Nothing Then Return New PropertyDescriptorCollection(New PropertyDescriptor() {})
            Dim ret(Instance.Owner.MaxColumns) As PropertyDescriptor
            For i As Integer = 0 To MaxCols - 1
                ret(i) = New IndexedPropertyDescriptor(i)
            Next
            Return New PropertyDescriptorCollection(ret)
        End Function
        ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
        ''' <remarks>Atribut <see cref="BrowsableAttribute"/> nebo bez atributu vrací <see cref="GetProperties"/>(), jinak volá bázi</remarks>
        Public Overrides Function GetProperties(ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
            If attributes.Length = 0 OrElse attributes.Length = 1 AndAlso TypeOf attributes(0) Is BrowsableAttribute Then Return GetProperties()
            Return MyBase.GetProperties(attributes)
        End Function
        ''' <summary>Implementuje popis indexované vlastnosti typu <see cref="ListInternal"/></summary>
        Public Class IndexedPropertyDescriptor : Inherits System.ComponentModel.PropertyDescriptor
            ''' <summary>Index na který vlastnost ukazuje</summary>
            Private Index As Integer
            ''' <summary>CTor</summary>
            ''' <param name="Index">Index na který ukazovat</param>
            Public Sub New(ByVal Index As Integer)
                MyBase.New(CStr(Index + 1), New Attribute() {})
                Me.Index = Index
            End Sub
            ''' <summary>When overridden in a derived class, returns whether resetting an object changes its value.</summary>
            ''' <param name="component">The component to test for reset capability.</param>
            ''' <returns>False</returns>
            Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
                Return False
            End Function
            ''' <summary>Gets the type of the component this property is bound to.</summary>
            ''' <returns><see cref="ListInternal"/></returns>
            ''' <remarks>A System.Type that represents the type of component this property is bound to. When the System.ComponentModel.PropertyDescriptor.GetValue(System.Object) or System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object) methods are invoked, the object specified might be an instance of this type.</remarks>
            Public Overrides ReadOnly Property ComponentType() As System.Type
                Get
                    Return GetType(ListInternal)
                End Get
            End Property
            ''' <summary>Gets the current value of the property on a component.</summary>
            ''' <param name="component">The component with the property for which to retrieve the value.</param>
            ''' <returns>The value of a property for a given component.</returns>
            Public Overrides Function GetValue(ByVal component As Object) As Object
                With DirectCast(component, List(Of String))
                    If Index >= .Count Then Return Nothing
                    Return .Item(Index)
                End With
            End Function
            ''' <summary>Gets a value indicating whether this property is read-only.</summary>
            ''' <returns>False</returns>
            Public Overrides ReadOnly Property IsReadOnly() As Boolean
                Get
                    Return False
                End Get
            End Property
            ''' <summary>Gets the type of the property.</summary>
            ''' <returns><see cref="String"/></returns>
            Public Overrides ReadOnly Property PropertyType() As System.Type
                Get
                    Return GetType(String)
                End Get
            End Property
            ''' <summary>Resets the value for this property of the component to the default value.</summary>
            ''' <param name="component">The component with the property value that is to be reset to the default value</param>
            ''' <exception cref="NotImplementedException">Vždy</exception>
            Public Overrides Sub ResetValue(ByVal component As Object)
                Throw New NotImplementedException
            End Sub
            ''' <summary>Sets the value of the component to a different value.</summary>
            ''' <param name="component">The component with the property value that is to be set.</param>
            ''' <param name="value">The new value.</param>
            Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
                With DirectCast(component, List(Of String))
                    If Index >= .Count Then
                        For i As Integer = .Count To Index
                            .Add(Nothing)
                        Next i
                    End If
                    .Item(Index) = value
                End With
            End Sub
            ''' <summary>Determines a value indicating whether the value of this property needs to be persisted.</summary>
            ''' <param name="component">The component with the property to be examined for persistence.</param>
            ''' <returns>true if the property should be persisted; otherwise, false.</returns>
            Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
                Return True
            End Function
            ''' <summary>Gets the collection of attributes for this member.</summary>
            ''' <returns>An System.ComponentModel.AttributeCollection that provides the attributes for this member, or an empty collection if there are no attributes in the System.ComponentModel.MemberDescriptor.AttributeArray.</returns>
            Public Overrides ReadOnly Property Attributes() As System.ComponentModel.AttributeCollection
                Get
                    Return New AttributeCollection(New Attribute() {New BrowsableAttribute(True)})
                End Get
            End Property
        End Class
    End Class
#End Region

#Region "ITable"
    Private ReadOnly Property ColumnsCount() As Integer Implements ITable(Of String).ColumnsCount
        Get
            Return Me.MaxColumns
        End Get
    End Property

    Private ReadOnly Property RowsCount() As Integer Implements ITable(Of String).RowsCount
        Get
            Return Me.RowsInternal.Count
        End Get
    End Property

    Private Function ITable_GetEnumerator() As System.Collections.Generic.IEnumerator(Of System.Collections.Generic.IEnumerable(Of String)) Implements System.Collections.Generic.IEnumerable(Of System.Collections.Generic.IEnumerable(Of String)).GetEnumerator
        Return New iTableRowsEnumerator(Me, Me.RowsInternal.GetEnumerator)
    End Function
    Private Class iTableRowsEnumerator : Implements IEnumerator(Of System.Collections.Generic.IEnumerable(Of String))
        Private CSV As CSV
        Private internal As IEnumerator(Of ListInternal)
        Public Sub New(ByVal CSV As CSV, ByVal enumerator As IEnumerator(Of ListInternal))
            Me.CSV = CSV
            Me.internal = enumerator
        End Sub

        Public ReadOnly Property Current() As System.Collections.Generic.IEnumerable(Of String) Implements System.Collections.Generic.IEnumerator(Of System.Collections.Generic.IEnumerable(Of String)).Current
            Get
                Return New RowEnumerable(CSV, internal.Current)
            End Get
        End Property

        Private ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            Return Me.internal.MoveNext
        End Function

        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            Me.internal.Reset()
        End Sub
#Region " IDisposable Support "
        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    CSV = Nothing
                    internal.Dispose()
                End If
            End If
            Me.disposedValue = True
        End Sub


        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
    Private Class RowEnumerable : Implements IEnumerable(Of String), IDisposable
        Private CSV As CSV
        Private row As ListInternal
        Public Sub New(ByVal CSV As CSV, ByVal row As ListInternal)
            Me.CSV = CSV
            Me.row = row
        End Sub
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of String) Implements System.Collections.Generic.IEnumerable(Of String).GetEnumerator
            Return New RowEnumerator(CSV, row)
        End Function

        Private Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function
#Region " IDisposable Support "
        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    CSV = Nothing
                    row = Nothing
                End If
            End If
            Me.disposedValue = True
        End Sub


        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
    Private Class RowEnumerator : Implements IEnumerator(Of String)
        Private CSV As CSV
        Private row As ListInternal
        Private index As Integer = -1
        Public Sub New(ByVal CSV As CSV, ByVal row As ListInternal)
            Me.CSV = CSV
            Me.row = row
        End Sub
        Public ReadOnly Property Current() As String Implements System.Collections.Generic.IEnumerator(Of String).Current
            Get
                If index < row.Count Then : Return row(index)
                ElseIf index < CSV.MaxColumns Then : Return ""
                Else : Throw New InvalidOperationException("Enumeration finished")
                End If
            End Get
        End Property

        Public ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            index += 1
            Return index < CSV.MaxColumns
        End Function

        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            index = -1
        End Sub
#Region " IDisposable Support "
        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    CSV = Nothing
                    row = Nothing
                End If
            End If
            Me.disposedValue = True
        End Sub
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
#End Region
End Class
