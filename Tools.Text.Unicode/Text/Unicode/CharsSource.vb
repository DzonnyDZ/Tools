Imports System.ComponentModel
Imports Tools.CollectionsT.GenericT
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports Tools.LinqT
Imports System.Linq

Namespace TextT.UnicodeT
    ''' <summary>Transforms flat list of characters in form of <see cref="CharsList"/> to list of character groups (e.g. lines, 16 chars per line)</summary>
    ''' <remarks>
    ''' This class is not CLS-compliant and no CLS-compliant alternative is provided.
    ''' <para>This class is intended to be used with charmap-like controls that display characters in a form of grid.</para>
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <CLSCompliant(False), EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class CharsSource
        Implements IList(Of CharsLine), IReadOnlyIndexableCollection(Of CharsLine, Integer)
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
                If Chars.Count = 0 Then Throw New InvalidOperationException("No characters in collection")
                Return Chars(0)
            End Get
        End Property
        ''' <summary>Gets last character in the <see cref="Chars"/> collection</summary>
        ''' <exception cref="InvalidOperationException"><see cref="Chars"/>.<see cref="CharsList.Count">Count</see> is zero</exception>
        ''' <remarks>If <see cref="Continuous"/> is true this is also character with highest code-point</remarks>
        Public ReadOnly Property LastChar As UInteger
            Get
                If Chars.Count = 0 Then Throw New InvalidOperationException("No characters in collection")
                Return Chars(Chars.Count - 1)
            End Get
        End Property

        ''' <summary>Gets index to the <see cref="Chars"/> collection that points to firts character in first line</summary>
        ''' <remarks>
        ''' If <see cref="Continuous"/> is true the index cann be negative, in this case first -<see cref="VirtualFirstIndex"/> characters in first group are ignored (null).
        ''' This is to property align the characters in a grid.
        ''' </remarks>
        Protected Friend ReadOnly Property VirtualFirstIndex As Long
            Get
                If Chars.Count = 0 Then Throw New InvalidOperationException("No characters in collection")
                If Not Continuous Then Return 0
                Return -(FirstChar Mod Columns)
            End Get
        End Property

        ''' <summary>Gets index to the <see cref="Chars"/> collection that points to last character in last line</summary>
        ''' <remarks>
        ''' If <see cref="Continuous"/> is true the index cann be greater than or eaqual to <see cref="Chars"/>.<see cref="CharsList.Count">Count</see>, in this case last  <see cref="VirtualLastIndex"/> - <see cref="Chars"/>.<see cref="CharList.Count">Count</see> + 1 characters in last group are ignored (null).
        ''' This is to property align the characters in a grid.
        ''' </remarks>
        Protected Friend ReadOnly Property VirtualLastIndex As Long
            Get
                If Chars.Count = 0 Then Throw New InvalidOperationException("No characters in collection")
                If Not Continuous Then
                    Return Chars.Count - 1
                Else
                    Return Math.Ceiling((LastChar + 1) / Columns) * Columns - FirstChar - 1
                End If
            End Get
        End Property


        ''' <summary>Gets value indicating if all characters in the collection are guaranteeed to for a continuous (uninterrupted, incremental) range</summary>
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
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
        Default Public Overloads ReadOnly Property Item(index As Integer) As CharsLine Implements CollectionsT.GenericT.IReadOnlyIndexable(Of CharsLine, Integer).Item
            Get
                If index < 0 OrElse index >= Count Then Throw New ArgumentOutOfRangeException("index")
                Return New CharsLine(Me, index)
            End Get
        End Property

        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of CharsLine) Implements System.Collections.Generic.IEnumerable(Of CharsLine).GetEnumerator
            Return New IndexableEnumerator(Of Integer, CharsLine)(New ForLoopCollection(Of Integer)(0, Count - 1, Function(i) i), Me)
        End Function


#Region "Hidden"
        ''' <summary>Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.</summary>
        ''' <returns>The index of 
        ''' <paramref name="item" /> if found in the list; otherwise, -1.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        Private Function IndexOf(item As CharsLine) As Integer Implements System.Collections.Generic.IList(Of CharsLine).IndexOf
            If item.Source IsNot Me Then Return -1
            If item.Index >= Count Then Return -1
            Return item.Index
        End Function
        ''' <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        ''' <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        Public ReadOnly Property Count As Integer Implements System.Collections.Generic.ICollection(Of CharsLine).Count, IReadOnlyCollection(Of CharsLine).Count
            Get
                If Chars.Count = 0 Then Return 0
                If Continuous Then
                    Return (VirtualLastIndex - VirtualFirstIndex + 1) / Columns
                Else
                    Return Math.Ceiling(Chars.Count / Columns)
                End If
            End Get
        End Property


        ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
        ''' <returns>true if 
        ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        Private Function Contains(item As CharsLine) As Boolean Implements System.Collections.Generic.ICollection(Of CharsLine).Contains
            If item.Source IsNot Me Then Return False
            Return item.Index < Count
        End Function

        ''' <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in 
        ''' <paramref name="array" /> at which copying begins.</param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="array" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException">
        ''' <paramref name="arrayIndex" /> is less than 0.</exception>
        ''' <exception cref="T:System.ArgumentException">
        ''' <paramref name="array" /> is multidimensional.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from 
        ''' <paramref name="arrayIndex" /> to the end of the destination 
        ''' <paramref name="array" />.-or-Type 
        ''' <paramref name="T" /> cannot be cast automatically to the type of the destination 
        ''' <paramref name="array" />.</exception>
        Private Sub CopyTo(array() As CharsLine, arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of CharsLine).CopyTo, IReadOnlyCollection(Of CharsLine).CopyTo
            If array Is Nothing Then Throw New ArgumentNullException("array")
            If arrayIndex < array.GetLowerBound(0) OrElse arrayIndex > array.GetUpperBound(0) Then Throw New ArgumentOutOfRangeException("arrayIndex")
            If array.Length - arrayIndex < Count Then Throw New ArgumentException("Not enough space in destination array")
            Dim i% = 0
            For Each line In Me
                array(i + arrayIndex) = line
                i += 1
            Next
        End Sub
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Private Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function
#End Region

#Region "NotSupported"
        Private Sub Add(item As CharsLine) Implements System.Collections.Generic.ICollection(Of CharsLine).Add
            Throw New NotSupportedException
        End Sub

        Private Sub Clear() Implements System.Collections.Generic.ICollection(Of CharsLine).Clear
            Throw New NotSupportedException
        End Sub
        Private Sub Insert(index As Integer, item As CharsLine) Implements System.Collections.Generic.IList(Of CharsLine).Insert
            Throw New NotSupportedException
        End Sub
        Private Sub RemoveAt(index As Integer) Implements System.Collections.Generic.IList(Of CharsLine).RemoveAt
            Throw New NotSupportedException
        End Sub

        Private Property IList_Item(index As Integer) As CharsLine Implements System.Collections.Generic.IList(Of CharsLine).Item
            Get
                Return Item(index)
            End Get
            Set(value As CharsLine)
                Throw New NotSupportedException
            End Set
        End Property
        Public ReadOnly Property IsReadOnly As Boolean Implements System.Collections.Generic.ICollection(Of CharsLine).IsReadOnly
            Get
                Return True
            End Get
        End Property
        Private Function Remove(item As CharsLine) As Boolean Implements System.Collections.Generic.ICollection(Of CharsLine).Remove
            Throw New NotSupportedException
        End Function
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


    ''' <summary>Provides characters for single llines of characters</summary>
    ''' <remarks>
    ''' This class is not CLS-compliant and no CLS-compliant alternative is provided.
    ''' <para>This class is intended to be used with charmap-like controls that display characters in a form of grid.</para>
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <TypeDescriptionProvider(GetType(CharsLineTypeDescriptionProvider))>
    <CLSCompliant(False), EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class CharsLine
        Implements IReadOnlyIndexableCollection(Of UInteger?, Integer)

        Private ReadOnly _source As CharsSource
        Private ReadOnly _index As Integer
        ''' <summary>CTor - creates a new instance of the <see cref="CharsLine"/> class</summary>
        ''' <param name="source">Source data for the line</param>
        ''' <param name="index">Line index in <paramref name="source"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than ore equal to number of lines in <paramref name="source"/>.</exception>
        Public Sub New(source As CharsSource, index As Integer)
            If source Is Nothing Then Throw New ArgumentNullException("source")
            If index < 0 OrElse index >= source.Count Then Throw New ArgumentOutOfRangeException("index")
            _source = source
            _index = index
        End Sub

        ''' <summary>Gets source data are being provided for</summary>
        Protected Friend ReadOnly Property Source() As CharsSource
            Get
                Return _source
            End Get
        End Property
        ''' <summary>Gets index of line in source this instance provides data from</summary>
        Public ReadOnly Property Index() As Integer
            Get
                Return _index
            End Get
        End Property
        ''' <summary>Gets index of first character on this line in <see cref="Source"/>.<see cref="CharsSource.Chars">Chars</see></summary>
        ''' <remarks>The value can be negative up to -(<see cref="Source"/>.<see cref="CharsSource.Columns">Columns</see> - 1). It can also be higher than number of characters in <see cref="Source"/>.<see cref="CharsSource.Chars">Chars</see> up to <see cref="Source"/>.<see cref="CharsSource.Chars">Chars</see>.<see cref="CharsList.Count">Count</see> - 1 + <see cref="Source"/>.<see cref="CharsSource.Columns">Columns</see>. For therese indexes <see cref="Item"/> returns null. Such index are also included in enumeration.</remarks>
        Public ReadOnly Property FirstIndex As Long
            Get
                Return Source.VirtualFirstIndex + Source.Columns * Index
            End Get
        End Property
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of UInteger?) Implements System.Collections.Generic.IEnumerable(Of UInteger?).GetEnumerator
            Return New CharsLineEnumerator(Me)
        End Function

        ''' <summary>Implements <see cref="IEnumerator(Of T)"/> for <see cref="CharsLine"/></summary>
        Private Class CharsLineEnumerator
            Implements IEnumerator(Of UInteger?)
            ''' <summary>The <see cref="CharsLine"/> instance to enumerate</summary>
            Private [for] As CharsLine
            ''' <summary>CTor - creates a new instance of the <see cref="CharsLineEnumerator"/> class</summary>
            ''' <param name="for">The <see cref="CharsLine"/> to enumerate</param>
            ''' <exception cref="ArgumentNullException"><paramref name="for"/> is null</exception>
            Public Sub New([for] As CharsLine)
                If [for] Is Nothing Then Throw New ArgumentNullException("for")
                Me.for = [for]
            End Sub
            ''' <summary>Current index to <see cref="[for]"/></summary>
            Private index As Integer = -1

            ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
            ''' <returns>The element in the collection at the current position of the enumerator.</returns>
            ''' <exception cref="InvalidOperationException">Enumeration has not started yet or it has already finished</exception>
            ''' <exception cref="ObjectDisposedException">Enumerator was disposed</exception>
            Public ReadOnly Property Current As UInteger? Implements System.Collections.Generic.IEnumerator(Of UInteger?).Current
                Get
                    If disposedValue Then Throw New ObjectDisposedException([GetType].Name)
                    If index < 0 OrElse index >= [for].Count Then Throw New InvalidOperationException("Enumeration ahs not yet started or it has already finished.")
                    Return [for](index)
                End Get
            End Property

            ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
            ''' <returns>The element in the collection at the current position of the enumerator.</returns> 
            ''' <exception cref="InvalidOperationException">Enumeration has not started yet or it has already finished</exception>
            ''' <exception cref="ObjectDisposedException">Enumerator was disposed</exception>
            Private ReadOnly Property IEnumerator_Current As Object Implements System.Collections.IEnumerator.Current
                Get
                    Return Current
                End Get
            End Property


            ''' <summary>Advances the enumerator to the next element of the collection.</summary>
            ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            ''' <exception cref="ObjectDisposedException">Enumerator was disposed</exception>
            ''' <filterpriority>2</filterpriority>
            Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
                If disposedValue Then Throw New ObjectDisposedException([GetType].Name)
                If index < [for].Size Then index += 1
                Return index < [for].Size
            End Function

            ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
            ''' <exception cref="ObjectDisposedException">Enumerator was disposed</exception>
            Public Sub Reset() Implements System.Collections.IEnumerator.Reset
                If disposedValue Then Throw New ObjectDisposedException([GetType].Name)
                index = -1
            End Sub

#Region "IDisposable Support"
            ''' <summary>To detect redundant calls</summary>
            Private disposedValue As Boolean

            ''' <summary>Implements <see cref="IDisposable.Dispose"/></summary>
            Protected Overridable Sub Dispose(disposing As Boolean)
                Me.disposedValue = True
                [for] = Nothing
            End Sub

            ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
            ''' <filterpriority>2</filterpriority>
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region

        End Class

        ''' <summary>Gets all code points in this line as array</summary>
        ''' <remarks>For aligned lines, characters that are before start of source and after end of source are null.</remarks>
        Public ReadOnly Property Characters As UInteger?()
            Get
                Return Me.ToArray
            End Get
        End Property

        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Private Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>
        ''' true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false. 
        ''' This implementation returns true only when <paramref name="obj"/> is <see cref="CharsLine"/> which belongs to the same <see cref="CharsSource"/> as this instance and has same <see cref="Index"/>.
        ''' </returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is CharsLine Then
                Dim other As CharsLine = obj
                Return other.Source Is Source AndAlso other.Index = Index
            Else
                Return MyBase.Equals(obj)
            End If
        End Function


        ''' <summary>Copies the elements of the <see cref="IReadOnlyCollection(Of T)"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="IReadOnlyCollection(Of T)"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing. </param>
        ''' <param name="index">The zero-based index in array at which copying begins. </param>
        ''' <exception cref="T:System.ArgumentNullException">array is null. </exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero. </exception>
        ''' <exception cref="T:System.ArgumentException">array is multidimensional.-or- index is equal to or greater than the length of array.-or- The number of elements in the source <see cref="T:System.Collections.ICollection"></see> is greater than the available space from index to the end of the destination array. </exception>
        ''' <exception cref="T:System.InvalidCastException">The type of the source <see cref="IReadOnlyCollection(Of T)"></see> cannot be cast automatically to the type of the destination array. </exception>
        ''' <filterpriority>2</filterpriority>
        Private Sub CopyTo(array As UInteger?(), index As Integer) Implements CollectionsT.GenericT.IReadOnlyCollection(Of UInteger?).CopyTo
            Characters.CopyTo(array, index)
        End Sub

        ''' <summary>Gets the number of characters contained in this line.</summary>
        ''' <returns>The number of characters in this line which equals to <see cref="Source"/>.<see cref="CharsSource.Columns"/>.</returns>
        ''' <filterpriority>2</filterpriority>
        Public ReadOnly Property Size As Integer Implements CollectionsT.GenericT.IReadOnlyCollection(Of UInteger?).Count
            Get
                Return Source.Columns
            End Get
        End Property

        ''' <summary>Gets value on specified index</summary>
        ''' <param name="index">Index to obtain value</param>
        ''' <returns>value lying on specified <paramref name="index"/></returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than or equal to <see cref="Size"/>.</exception>
        Default Public ReadOnly Property Item(index As Integer) As UInteger? Implements CollectionsT.GenericT.IReadOnlyIndexable(Of UInteger?, Integer).Item
            Get
                If index < 0 OrElse index >= Size Then Throw New ArgumentOutOfRangeException("index")
                Dim realIndex = FirstIndex + index
                If realIndex < 0 OrElse realIndex >= Source.Chars.Count Then Return Nothing
                Return Source.Chars(realIndex)
            End Get
        End Property
    End Class

    ''' <summary>A type description provider of <see cref="CharsLine"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Friend Class CharsLineTypeDescriptionProvider
        Inherits TypeDescriptionProvider
        ''' <summary>Gets a custom type descriptor for the given type and object.</summary>
        ''' <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
        ''' <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
        ''' <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
        Public Overrides Function GetTypeDescriptor(objectType As System.Type, instance As Object) As System.ComponentModel.ICustomTypeDescriptor
            If objectType.Equals(GetType(CharsLine)) AndAlso instance IsNot Nothing AndAlso TypeOf instance Is CharsLine Then Return New CharsLineTypeDescriptor(instance)
            Return MyBase.GetTypeDescriptor(objectType, instance)
        End Function

        ''' <summary>Gets a value that indicates whether the specified type is compatible with the type description and its chain of type description providers. </summary>
        ''' <returns>true if 
        ''' <paramref name="type" /> is compatible with the type description and its chain of type description providers; otherwise, false. </returns>
        ''' <param name="type">The type to test for compatibility.</param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="type" /> is null.</exception>
        Public Overrides Function IsSupportedType(type As System.Type) As Boolean
            Return type.Equals(GetType(CharsLine))
        End Function
    End Class

    ''' <summary>A type descriptor for <see cref="CharsLine"/></summary>
    ''' <remarks>Main putpose is to provide properties for individual columns (indexes)</remarks>
    Friend Class CharsLineTypeDescriptor
        Inherits CustomTypeDescriptor
        ''' <summary>Instance of <see cref="CharsLine"/> being described</summary>
        Private instance As CharsLine
        ''' <summary>CTor - creates a new instance of the <see cref="CharsLine"/> class</summary>
        ''' <param name="instance">An instance do describe</param>
        ''' <exception cref="ArgumentNullException"><paramref name="instance"/> is null</exception>
        Public Sub New(instance As CharsLine)
            If instance Is Nothing Then Throw New ArgumentNullException("instance")
            Me.instance = instance
        End Sub

        ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
        Public Overrides Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection
            Dim properties = New ForLoopCollection(Of PropertyDescriptor, Integer)(Function() 0, Function(i) i < instance.Size, Function(i) i + 1, Function(i) New CharsLineColumnDescriptor(instance, i))
            Return New PropertyDescriptorCollection(properties.ToArray, True)
        End Function
        ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
        Public Overrides Function GetProperties(attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
            If attributes Is Nothing OrElse attributes.Length = 0 Then Return GetProperties()
            Dim properties = From prp As PropertyDescriptor In GetProperties() Where attributes.All(Function(a) prp.Attributes.Contains(a))
            Return New PropertyDescriptorCollection(properties.ToArray, True)
        End Function
        ''' <summary>Returns a collection of custom attributes for the type represented by this type descriptor.</summary>
        ''' <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the type. The default is <see cref="F:System.ComponentModel.AttributeCollection.Empty" />.</returns>
        Public Overrides Function GetAttributes() As System.ComponentModel.AttributeCollection
            Return TypeDescriptor.GetAttributes(instance, True)
        End Function
        ''' <summary>Returns the fully qualified name of the class represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.String" /> containing the fully qualified class name of the type this type descriptor is describing. The default is null.</returns>
        Public Overrides Function GetClassName() As String
            Return TypeDescriptor.GetClassName(instance, True)
        End Function
        ''' <summary>Returns a collection of event descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptors for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
        Public Overrides Function GetEvents() As System.ComponentModel.EventDescriptorCollection
            Return TypeDescriptor.GetEvents(instance, True)
        End Function
        ''' <summary>Returns a collection of event descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptors for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
        Public Overrides Function GetEvents(attributes() As System.Attribute) As System.ComponentModel.EventDescriptorCollection
            Return TypeDescriptor.GetEvents(instance, attributes, True)
        End Function
        ''' <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
        ''' <returns>An <see cref="T:System.Object" /> that owns the given property specified by the type descriptor. The default is null.</returns>
        ''' <param name="pd">The property descriptor for which to retrieve the owning object.</param>
        Public Overrides Function GetPropertyOwner(pd As System.ComponentModel.PropertyDescriptor) As Object
            If TypeOf pd Is CharsLineColumnDescriptor Then Return DirectCast(pd, CharsLineColumnDescriptor).Instance
            Return instance
        End Function
        ''' <summary>Returns a type converter for the type represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the type represented by this type descriptor. The default is a newly created <see cref="T:System.ComponentModel.TypeConverter" />.</returns>
        Public Overrides Function GetConverter() As System.ComponentModel.TypeConverter
            Return TypeDescriptor.GetConverter(instance, True)
        End Function
        ''' <summary>Returns the name of the class represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.String" /> containing the name of the component instance this type descriptor is describing. The default is null.</returns>
        Public Overrides Function GetComponentName() As String
            Return TypeDescriptor.GetComponentName(instance, True)
        End Function
        ''' <summary>Returns the event descriptor for the default event of the object represented by this type descriptor.</summary>
        ''' <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> for the default event on the object represented by this type descriptor. The default is null.</returns>
        Public Overrides Function GetDefaultEvent() As System.ComponentModel.EventDescriptor
            Return TypeDescriptor.GetDefaultEvent(instance, True)
        End Function
        ''' <summary>Returns the property descriptor for the default property of the object represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the default property on the object represented by this type descriptor. The default is null. This implementation always returns null.</returns>
        Public Overrides Function GetDefaultProperty() As System.ComponentModel.PropertyDescriptor
            Return Nothing
        End Function
        ''' <summary>Returns an editor of the specified type that is to be associated with the class represented by this type descriptor.</summary>
        ''' <returns>An editor of the given type that is to be associated with the class represented by this type descriptor. The default is null.</returns>
        ''' <param name="editorBaseType">The base type of the editor to retrieve.</param>
        Public Overrides Function GetEditor(editorBaseType As System.Type) As Object
            Return TypeDescriptor.GetEditor(instance, editorBaseType, True)
        End Function
    End Class

    ''' <summary>A property descriptor that column of <see cref="CharsLine"/></summary>
    Friend Class CharsLineColumnDescriptor
        Inherits PropertyDescriptor
        ''' <summary>Current char line to provide property for</summary>
        Private ReadOnly _instance As CharsLine
        ''' <summary>Index of column to describe</summary>
        Private ReadOnly columnIndex As Integer
        ''' <summary>CTor - creates a new instance of the <see cref="CharsLineColumnDescriptor"/> class</summary>
        ''' <param name="instance">Instance of <see cref="CharsLine"/> to created property descriptor for</param>
        ''' <param name="columnIndex">Index (0-based) of column co create property descriptor for</param>
        ''' <exception cref="ArgumentNullException"><paramref name="instance"/> is null</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than zero or greater than or equal to <see cref="CharsLine.Size">size</see> of <paramref name="instance"/></exception>
        Public Sub New(instance As CharsLine, columnIndex As Integer)
            MyBase.New(GetName(instance, columnIndex), GetAttributes())
            If columnIndex < 0 OrElse columnIndex >= instance.Size Then Throw New ArgumentOutOfRangeException("columnIndex")
            Me._instance = instance
            Me.columnIndex = columnIndex
        End Sub

        ''' <summary>Gets current char line this property descriptor ptovides description of property of</summary>
        Public ReadOnly Property Instance As Object
            Get
                Return _instance
            End Get
        End Property

        ''' <summary>Gets name of newly created property</summary>
        ''' <param name="instance">Instance of <see cref="CharsLine"/> the property belongs to</param>
        ''' <param name="columnIndex">Index of column the instance belongs to</param>
        ''' <returns>Name of the propety-colum. I.e. number of the column in base specified by <paramref name="instance"/>.<see cref="CharsLine.Size">Size</see>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="instance"/> is null</exception>
        Private Shared Function GetName(instance As CharsLine, columnIndex As Integer) As String
            If instance Is Nothing Then Throw New ArgumentNullException("instance")
            Return NumericsT.ConversionsT.Dec2Xxx(columnIndex, instance.Size)
        End Function

        ''' <summary>Gets attributes for newly created instance of <see cref="CharsLineColumnDescriptor"/></summary>
        ''' <returns>Attributes of a virtual property described by newly created instace</returns>
        Private Shared Function GetAttributes() As Attribute()
            Return New Attribute() {New BrowsableAttribute(True), New BindableAttribute(True, BindingDirection.OneWay)}
        End Function

        ''' <summary>When overridden in a derived class, returns whether resetting an object changes its value.</summary>
        ''' <param name="component">The component to test for reset capability. </param>
        ''' <returns>true if resetting the component changes its value; otherwise, false. This implementation always returns false.</returns>
        Public Overrides Function CanResetValue(component As Object) As Boolean
            Return False
        End Function

        ''' <summary>When overridden in a derived class, gets the type of the component this property is bound to.</summary>
        ''' <returns>
        ''' A <see cref="T:System.Type" /> that represents the type of component this property is bound to.
        ''' This implementation always returns type of item passed to CTor which is always <see cref="CharsLine"/> or derived type.
        ''' </returns>
        Public Overrides ReadOnly Property ComponentType As System.Type
            Get
                Return instance.GetType
            End Get
        End Property

        ''' <summary>When overridden in a derived class, gets the current value of the property on a component.</summary>
        ''' <returns>The value of a property for a given component.</returns>
        ''' <param name="component">The component with the property for which to retrieve the value. </param>
        Public Overrides Function GetValue(component As Object) As Object
            If TypeOf component Is CharsLine Then Return DirectCast(component, CharsLine)(columnIndex)
            Throw New TypeMismatchException(component, "component", GetType(CharsLine))
        End Function

        ''' <summary>When overridden in a derived class, gets a value indicating whether this property is read-only.</summary>
        ''' <returns>true if the property is read-only; otherwise, false. This implementation always returns true.</returns>
        Public Overrides ReadOnly Property IsReadOnly As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>When overridden in a derived class, gets the type of the property.</summary>
        ''' <returns>A <see cref="T:System.Type" /> that represents the type of the property. This implementation always returns <see cref="UInteger"/>.</returns>
        Public Overrides ReadOnly Property PropertyType As System.Type
            Get
                Return GetType(UInteger)
            End Get
        End Property

        ''' <summary>When overridden in a derived class, resets the value for this property of the component to the default value.</summary>
        ''' <param name="component">The component with the property value that is to be reset to the default value. </param>
        ''' <exception cref="NotSupportedException">This implementation always throws this exception</exception>
        Public Overrides Sub ResetValue(component As Object)
            Throw New NotSupportedException
        End Sub

        ''' <summary>When overridden in a derived class, sets the value of the component to a different value.</summary>
        ''' <param name="component">The component with the property value that is to be set. </param>
        ''' <param name="value">The new value. </param>
        ''' <exception cref="NotSupportedException">This implementation always throws this exception</exception>
        Public Overrides Sub SetValue(component As Object, value As Object)
            Throw New NotSupportedException
        End Sub

        ''' <summary>When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.</summary>
        ''' <param name="component">The component with the property to be examined for persistence. </param>
        ''' <returns>true if the property should be persisted; otherwise, false. This implementation always returns true.</returns>
        Public Overrides Function ShouldSerializeValue(component As Object) As Boolean
            Return True
        End Function
    End Class
End Namespace