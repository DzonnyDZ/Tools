Imports System.Collections.Specialized
Imports Tools.CollectionsT.GenericT
Imports Tools.LinqT


Namespace TextT.UnicodeT
    ''' <summary>Transforms flat list of characters in form of <see cref="CharsList"/> to list of character groups (e.g. lines, 16 chars per line)</summary>
    ''' <remarks>
    ''' This class is not CLS-compliant and no CLS-compliant alternative is provided.
    ''' <para>This class is intended to be used with charmap-like controls that display characters in a form of grid.</para>
    ''' <para>This classs implements <see cref="IList"/> and <see cref="IList(Of T)"/> however this implementation is always read-only.</para>
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <CLSCompliant(False), EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class CharsSource
        Implements IList(Of CharsLine), IReadOnlyIndexableCollection(Of CharsLine, Integer), IList 'In order WPF DataGrid and other controls to perform DataVirtualization non-generic ILIst must be implementred
        Implements INotifyPropertyChanged, INotifyCollectionChanged
        Implements IReportsChange

        ''' <summary>CTor - creates a new instance of the <see cref="CharsSource"/> class</summary>
        ''' <param name="chars">Characters to use</param>
        ''' <exception cref="ArgumentNullException"><paramref name="chars"/> is null</exception>
        Public Sub New(chars As CharsList)
            If chars Is Nothing Then Throw New ArgumentNullException("chars")
            _chars = chars
            If TypeOf chars Is INotifyCollectionChanged Then AddHandler DirectCast(chars, INotifyCollectionChanged).CollectionChanged, AddressOf OnCharsCollectionChanged
        End Sub

#Region "Support properties"
        Private ReadOnly _chars As CharsList
        ''' <summary>Gets all characters in this collection as flat collection</summary>
        Public ReadOnly Property Chars() As CharsList
            Get
                Return _chars
            End Get
        End Property

        Private _columns As Integer = 16
        ''' <summary>Gets or sets value indicating how many colums (characters per line/group) this class uses</summary>
        ''' <remarks>Typical value is 16 because hexanumbers are used for Unicode charatcer codes</remarks>
        <DefaultValue(16I)>
        Public Property Columns As Integer
            Get
                Return _columns
            End Get
            Set(value As Integer)
                If value < 0 Then Throw New ArgumentOutOfRangeException("value")
                If value <> _columns Then
                    _columns = value
                    OnChanged("Columns")
                    OnCollectionChanged(New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset))
                End If
            End Set
        End Property
        ''' <summary>Gets first character in the <see cref="Chars"/> collection</summary>
        ''' <exception cref="InvalidOperationException"><see cref="Chars"/>.<see cref="CharsList.Count">Count</see> is zero</exception>
        ''' <remarks>If <see cref="Continuous"/> is true this is also character with lowest code-point</remarks>
        Public ReadOnly Property FirstChar As UInteger
            Get
                If Chars.Count = 0 Then Throw New InvalidOperationException(TextT.UnicodeT.UnicodeResources.ex_NoCharsInCollection)
                Return Chars(0)
            End Get
        End Property
        ''' <summary>Gets last character in the <see cref="Chars"/> collection</summary>
        ''' <exception cref="InvalidOperationException"><see cref="Chars"/>.<see cref="CharsList.Count">Count</see> is zero</exception>
        ''' <remarks>If <see cref="Continuous"/> is true this is also character with highest code-point</remarks>
        Public ReadOnly Property LastChar As UInteger
            Get
                If Chars.Count = 0 Then Throw New InvalidOperationException(TextT.UnicodeT.UnicodeResources.ex_NoCharsInCollection)
                Return Chars(Chars.Count - 1)
            End Get
        End Property

        ''' <summary>Gets index to the <see cref="Chars"/> collection that points to first character in first line</summary>
        ''' <remarks>
        ''' If <see cref="Continuous"/> is true the index can be negative, in this case first -<see cref="VirtualFirstIndex"/> characters in first group are ignored (null).
        ''' This is to property align the characters in a grid.
        ''' </remarks>
        Protected Friend ReadOnly Property VirtualFirstIndex As Long
            Get
                If Chars.Count = 0 Then Throw New InvalidOperationException(TextT.UnicodeT.UnicodeResources.ex_NoCharsInCollection)
                If Not Continuous Then Return 0
                Return -(FirstChar Mod Columns)
            End Get
        End Property

        ''' <summary>Gets index to the <see cref="Chars"/> collection that points to last character in last line</summary>
        ''' <remarks>
        ''' If <see cref="Continuous"/> is true the index can be greater than or equal to <see cref="Chars"/>.<see cref="CharsList.Count">Count</see>, in this case last  <see cref="VirtualLastIndex"/> - <see cref="Chars"/>.<see cref="CharsList.Count">Count</see> + 1 characters in last group are ignored (null).
        ''' This is to property align the characters in a grid.
        ''' </remarks>
        Protected Friend ReadOnly Property VirtualLastIndex As Long
            Get
                If Chars.Count = 0 Then Throw New InvalidOperationException(TextT.UnicodeT.UnicodeResources.ex_NoCharsInCollection)
                If Not Continuous Then
                    Return Chars.Count - 1
                Else
                    Return Math.Ceiling((LastChar + 1) / Columns) * Columns - FirstChar - 1
                End If
            End Get
        End Property


        ''' <summary>Gets value indicating if all characters in the collection are guaranteed to for a continuous (uninterrupted, incremental) range</summary>
        ''' <seelaso cref="CharsList.Continuous"/>
        Public ReadOnly Property Continuous As Boolean
            Get
                Return Chars.Continuous
            End Get
        End Property
#End Region

        ''' <summary>Gets the element at the specified index.</summary>
        ''' <returns>The element at the specified index.</returns>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="IList(Of T)" />.</exception>
        Default Public Overloads ReadOnly Property Item(index As Integer) As CharsLine Implements CollectionsT.GenericT.IReadOnlyIndexable(Of CharsLine, Integer).Item
            Get
                If index < 0 OrElse index >= Count Then Throw New ArgumentOutOfRangeException("index")
                Return New CharsLine(Me, index)
            End Get
        End Property

        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="IEnumerator(Of T)" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Function GetEnumerator() As IEnumerator(Of CharsLine) Implements IEnumerable(Of CharsLine).GetEnumerator
            Return New IndexableEnumerator(Of Integer, CharsLine)(New ForLoopCollection(Of Integer)(0, Count - 1, Function(i) i), Me)
        End Function


#Region "Hidden"
        ''' <summary>Determines the index of a specific item in the <see cref="T:IList`1" />.</summary>
        ''' <returns>The index of 
        ''' <paramref name="item" /> if found in the list; otherwise, -1.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:IList`1" />.</param>
        Private Function IndexOf(item As CharsLine) As Integer Implements IList(Of CharsLine).IndexOf
            If item.Source IsNot Me Then Return -1
            If item.Index >= Count Then Return -1
            Return item.Index
        End Function
        ''' <summary>Gets the number of elements contained in the <see cref="T:ICollection`1" />.</summary>
        ''' <returns>The number of elements contained in the <see cref="T:ICollection`1" />.</returns>
        Public ReadOnly Property Count As Integer Implements ICollection(Of CharsLine).Count, IReadOnlyCollection(Of CharsLine).Count, ICollection.Count
            Get
                If Chars.Count = 0 Then Return 0
                If Continuous Then
                    Return (VirtualLastIndex - VirtualFirstIndex + 1) / Columns
                Else
                    Return Math.Ceiling(Chars.Count / Columns)
                End If
            End Get
        End Property


        ''' <summary>Determines whether the <see cref="T:ICollection`1" /> contains a specific value.</summary>
        ''' <returns>true if 
        ''' <paramref name="item" /> is found in the <see cref="T:ICollection`1" />; otherwise, false.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:ICollection`1" />.</param>
        Private Function Contains(item As CharsLine) As Boolean Implements ICollection(Of CharsLine).Contains
            If item.Source IsNot Me Then Return False
            Return item.Index < Count
        End Function

        ''' <summary>Copies the elements of the <see cref="T:ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="array" /> is null.</exception>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
        ''' <exception cref="System.ArgumentException">
        ''' <paramref name="array" /> is multidimensional. -or-
        ''' The number of elements in the source <see cref="CharsLine" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />. -or-
        ''' Type <see cref="CharsLine"/> cannot be cast automatically to the type of the destination <paramref name="array" />.
        ''' </exception>
        Private Sub CopyTo(array() As CharsLine, arrayIndex As Integer) Implements ICollection(Of CharsLine).CopyTo, IReadOnlyCollection(Of CharsLine).CopyTo
            If array Is Nothing Then Throw New ArgumentNullException("array")
            If arrayIndex < array.GetLowerBound(0) OrElse arrayIndex > array.GetUpperBound(0) Then Throw New ArgumentOutOfRangeException("arrayIndex")
            If array.Length - arrayIndex < Count Then Throw New ArgumentException(TextT.UnicodeT.UnicodeResources.ex_CollectionCopyToSmallArray)
            Dim i% = 0
            For Each line In Me
                array(i + arrayIndex) = line
                i += 1
            Next
        End Sub
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

#Region "ICollection"
        ''' <summary>Copies the elements of the <see cref="T:ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
        ''' <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
        ''' <exception cref="ArgumentNullException"><paramref name="array" /> is null. </exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is less than zero. </exception>
        ''' <exception cref="T:System.ArgumentException"><paramref name="array" /> is multidimensional.-or- The number of elements in the source <see cref="T:ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. </exception>
        ''' <exception cref="T:System.ArgumentException">Type <see cref="CharsLine"/> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
        ''' <filterpriority>2</filterpriority>
        Private Sub ICollection_CopyTo(array As Array, index As Integer) Implements ICollection.CopyTo
            If array Is Nothing Then Throw New ArgumentNullException("array")
            If array.Rank <> 1 Then Throw New ArgumentException(TextT.UnicodeT.UnicodeResources.ex_MultidimensionalArray, "array")
            If index < array.GetLowerBound(0) OrElse index > array.GetUpperBound(0) Then Throw New ArgumentOutOfRangeException("index")
            If array.Length - index < Count Then Throw New ArgumentException(TextT.UnicodeT.UnicodeResources.ex_CollectionCopyToSmallArray)
            Dim i% = 0
            For Each line In Me
                array.SetValue(line, i + index)
                i += 1
            Next
        End Sub


        ''' <summary>Gets a value indicating whether access to the <see cref="T:ICollection" /> is synchronized (thread safe).</summary>
        ''' <returns>true if access to the <see cref="T:ICollection" /> is synchronized (thread safe); otherwise, false. This implementation always returns false.</returns>
        ''' <filterpriority>2</filterpriority>
        Private ReadOnly Property IsSynchronized As Boolean Implements ICollection.IsSynchronized
            Get
                Return False
            End Get
        End Property

        ''' <summary>Gets an object that can be used to synchronize access to the <see cref="T:ICollection" />.</summary>
        ''' <returns>An object that can be used to synchronize access to the <see cref="T:ICollection" />. This implementation always returns current instance of <see cref="CharsSource"/>.</returns>
        ''' <filterpriority>2</filterpriority>
        Private ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
            Get
                Return Me
            End Get
        End Property
#End Region

#Region "IList"
        Private Function Contains(value As Object) As Boolean Implements IList.Contains
            Return TypeOf value Is CharsLine AndAlso Contains(DirectCast(value, CharsLine))
        End Function

        Private Function IndexOf(value As Object) As Integer Implements IList.IndexOf
            If Not TypeOf value Is CharsLine Then Return -1
            Return IndexOf(DirectCast(value, CharsLine))
        End Function

        Public ReadOnly Property IsFixedSize As Boolean Implements IList.IsFixedSize
            Get
                Return True
            End Get
        End Property
#End Region
#End Region

#Region "NotSupported"
        Private Sub Add(item As CharsLine) Implements ICollection(Of CharsLine).Add
            Throw New NotSupportedException
        End Sub

        Private Function Add(value As Object) As Integer Implements IList.Add
            Throw New NotSupportedException
        End Function
        Private Sub Clear() Implements ICollection(Of CharsLine).Clear, IList.Clear
            Throw New NotSupportedException
        End Sub
        Private Sub Insert(index As Integer, item As CharsLine) Implements IList(Of CharsLine).Insert
            Throw New NotSupportedException
        End Sub

        Private Sub Insert(index As Integer, value As Object) Implements IList.Insert
            Throw New NotSupportedException
        End Sub

        Private Sub RemoveAt(index As Integer) Implements IList(Of CharsLine).RemoveAt, IList.RemoveAt
            Throw New NotSupportedException
        End Sub

        Private Property IList1_Item(index As Integer) As CharsLine Implements IList(Of CharsLine).Item
            Get
                Return Item(index)
            End Get
            Set(value As CharsLine)
                Throw New NotSupportedException
            End Set
        End Property
        Private Property IList_Item(index As Integer) As Object Implements IList.Item
            Get
                Return Item(index)
            End Get
            Set
                Throw New NotSupportedException
            End Set
        End Property
        Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of CharsLine).IsReadOnly, IList.IsReadOnly
            Get
                Return True
            End Get
        End Property
        Private Function Remove(item As CharsLine) As Boolean Implements ICollection(Of CharsLine).Remove
            If Not Me.Contains(item) Then Return False
            Throw New NotSupportedException
        End Function

        Private Sub Remove(value As Object) Implements IList.Remove
            If Not TypeOf value Is CharsLine Then Return
            DirectCast(Me, ICollection(Of CharsLine)).Remove(value)
        End Sub
#End Region

#Region "Events"
        ''' <summary>Raises the <see cref="PropertyChanged"/> and <see cref="IReportsChange.Changed"/> events</summary>
        ''' <param name="propertyName">Name of changed property</param>
        Protected Overridable Sub OnChanged(propertyName$)
            Dim e As New PropertyChangedEventArgs(propertyName)
            RaiseEvent PropertyChanged(Me, e)
            RaiseEvent Changed(Me, e)
        End Sub

        ''' <summary>Occurs when a property value changes.</summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ''' <summary>Raises the <see cref="CollectionChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnCollectionChanged(e As NotifyCollectionChangedEventArgs)
            RaiseEvent CollectionChanged(Me, e)
        End Sub
        ''' <summary>Occurs when the collection changes.</summary>
        Public Event CollectionChanged As NotifyCollectionChangedEventHandler Implements INotifyCollectionChanged.CollectionChanged

        ''' <summary>Raises the <see cref="Changed"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnChanged(e As IReportsChange.ValueChangedEventArgsBase)
            RaiseEvent Changed(Me, e)
        End Sub
        ''' <summary>Occurs when a property value changes.</summary>
        ''' <remarks>This implementation uses <paramref name="e"/> of type <see cref="PropertyChangedEventArgs"/></remarks>
        Private Event Changed As IReportsChange.ChangedEventHandler Implements IReportsChange.Changed

        ''' <summary>Called when the <see cref="Chars"/> collection changed</summary>
        ''' <param name="sender">Source of the event - the <see cref="Chars"/> collection</param>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Only clled when <see cref="Chars"/> implements <see cref="INotifyCollectionChanged"/></remarks>
        Protected Overridable Sub OnCharsCollectionChanged(sender As CharsList, e As NotifyCollectionChangedEventArgs)
            OnCollectionChanged(New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset))
        End Sub
#End Region
    End Class
End Namespace