Imports System.ComponentModel
Imports Tools.CollectionsT.GenericT
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports Tools.LinqT
Imports System.Linq

Namespace TextT.UnicodeT
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

#If DEBUG Then
            instCount += 1
            Debug.Print("{0} {1}", [GetType].Name, instCount)
#End If
        End Sub

#If DEBUG Then
        ''' <summary>Counts number of instances created - debug-only</summary>
        Private Shared instCount% = 0
#End If

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
                    If index < 0 OrElse index >= [for].Count Then Throw New InvalidOperationException(TextT.UnicodeT.UnicodeResources.ex_EnumeratorState)
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
