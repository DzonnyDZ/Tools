Namespace ComponentModelT
#If True
    ''' <summary>Implements changeable singleto with <see cref="INotifyPropertyChanged"/> support</summary>
    ''' <typeparam name="T">Type of value stored in singleton container</typeparam>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    <Serializable()>
    <DebuggerDisplay("Value = {Value}")>
    Public Class ValueContainer(Of T As Structure)
        Implements INotifyPropertyChanged, IStructuralComparable, IComparable, IStructuralEquatable
        Private _value As T
        ''' <summary>CTor - initalized a new instance of the <see cref="ValueContainer(Of T)"/> class with default value of type <typeparamref name="T"/>.</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor - initializes a new instance of the <see cref="ValueContainer(Of T)"/> class with given value</summary>
        ''' <param name="value">Value to be stored within new singleton container</param>
        Public Sub New(ByVal value As T)
            Me.Value = value
        End Sub

        ''' <summary>gets or sest value stored in this singleton container</summary>
        Public Property Value As T
            Get
                Return _value
            End Get
            Set(ByVal value As T)
                If Not _value.Equals(value) Then
                    _value = value
                    OnPropertyChanged(New PropertyChangedEventArgs("value"))
                End If
            End Set
        End Property
        ''' <summary>Raises the <see cref="PropertyChanged"/> event</summary>
        Protected Overridable Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
            RaiseEvent PropertyChanged(Me, e)
        End Sub
        <NonSerialized()>
        Private propertyChangedHandler As PropertyChangedEventHandler
        ''' <summary>Occurs when a property value changes.</summary>
        Public Custom Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
            AddHandler(ByVal value As PropertyChangedEventHandler)
                propertyChangedHandler = [Delegate].Combine(propertyChangedHandler, value)
            End AddHandler
            RemoveHandler(ByVal value As PropertyChangedEventHandler)
                propertyChangedHandler = [Delegate].Remove(propertyChangedHandler, value)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
                If propertyChangedHandler IsNot Nothing Then propertyChangedHandler(sender, e)
            End RaiseEvent
        End Event

#Region "Tuple Interfaces"
        ''' <summary>Determines whether the current collection object precedes, occurs in the same position as, or follows another object in the sort order.</summary>
        ''' <returns>An integer that indicates the relationship of the current collection object to <paramref name="other" />, as shown in the following table.
        ''' <list type="table"><listheader><term>Return value</term><description>Description</description></listheader>
        ''' <item><term>-1</term><description>The current instance precedes <paramref name="other" />.</description></item>
        ''' <item><term>0</term><description>The current instance and <paramref name="other" /> are equal.</description></item>
        ''' <item><term>1</term><description>The current instance follows <paramref name="other" />.</description></item></list></returns>
        ''' <param name="other">The object to compare with the current instance.</param>
        ''' <param name="comparer">An object that compares members of the current collection object with the corresponding members of <paramref name="other" />.</param>
        ''' <exception cref="TypeMismatchException"><paramref name="other"/> is neither of type <typeparamref name="T"/> nor <see cref="Tuple(Of T)"/> nor <see cref="ValueContainer(Of T)"/>.</exception>
        Private Function IStructuralComparable_CompareTo(ByVal other As Object, ByVal comparer As System.Collections.IComparer) As Integer Implements System.Collections.IStructuralComparable.CompareTo
            If (other Is Nothing) Then
                Return 1
            End If
            If TypeOf other Is Tuple(Of T) Then
                Return comparer.Compare(Me.Value, DirectCast(other, Tuple(Of T)).Item1)
            ElseIf TypeOf other Is ValueContainer(Of T) Then
                Return comparer.Compare(Me.Value, DirectCast(other, ValueContainer(Of T)).Value)
            ElseIf TypeOf other Is T Then
                Return comparer.Compare(Me.Value, DirectCast(other, T))
            Else
                Throw New TypeMismatchException(other, "other", GetType(Tuple(Of T)), GetType(ValueContainer(Of T)))
            End If
        End Function

        ''' <summary>Determines whether an object is structurally equal to the current instance.</summary>
        ''' <returns>true if the two objects are equal; otherwise, false.</returns>
        ''' <param name="other">The object to compare with the current instance.</param>
        ''' <param name="comparer">An object that determines whether the current instance and <paramref name="other" /> are equal. </param>
        Private Function IStructuralEquatable_Equals(ByVal other As Object, ByVal comparer As System.Collections.IEqualityComparer) As Boolean Implements System.Collections.IStructuralEquatable.Equals
            If (other Is Nothing) Then
                Return False
            End If
            If TypeOf other Is Tuple(Of T) Then
                Return comparer.Equals(Me.Value, DirectCast(other, Tuple(Of T)).Item1)
            ElseIf TypeOf other Is ValueContainer(Of T) Then
                Return comparer.Equals(Me.Value, DirectCast(other, ValueContainer(Of T)).Value)
            ElseIf TypeOf other Is T Then
                Return comparer.Equals(Me.Value, DirectCast(other, T))
            End If
            Return False
        End Function

        ''' <summary>Returns a hash code for the current instance.</summary>
        ''' <returns>The hash code for the current instance.</returns>
        ''' <param name="comparer">An object that computes the hash code of the current object.</param>
        Private Function IStructuralEquatable_GetHashCode(ByVal comparer As System.Collections.IEqualityComparer) As Integer Implements System.Collections.IStructuralEquatable.GetHashCode
            Return comparer.GetHashCode(Me.Value)
        End Function

        ''' <summary>Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.</summary>
        ''' <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
        ''' Value Meaning Less than zero This instance is less than <paramref name="obj" />.
        ''' Zero This instance is equal to <paramref name="obj" />.
        ''' Greater than zero This instance is greater than <paramref name="obj" />. </returns>
        ''' <param name="obj">An object to compare with this instance. </param>
        ''' <exception cref="TypeMismatchException"><paramref name="other"/> is neither of type <typeparamref name="T"/> nor <see cref="Tuple(Of T)"/> nor <see cref="ValueContainer(Of T)"/>.</exception>
        ''' <filterpriority>2</filterpriority>
        Private Overloads Function IComparable_CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Return IStructuralComparable_CompareTo(obj, Comparer(Of Object).Default)
        End Function
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return IStructuralEquatable_Equals(obj, EqualityComparer(Of Object).Default)
        End Function
        ''' <summary>Serves as a hash function for a particular type. </summary>
        ''' <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function GetHashCode() As Integer
            Return IStructuralEquatable_GetHashCode(EqualityComparer(Of Object).Default)
        End Function
#End Region
#Region "Operators"
        ''' <summary>Converts value of type <see cref="ValueContainer(Of T)"/> to <typeparamref name="T"/></summary>
        ''' <param name="a">A value to be converted</param>
        ''' <returns><paramref name="a"/>.<see cref="Value">Value</see>. Default value of type <typeparamref name="T"/> when <paramref name="a"/> is null.</returns>
        Public Shared Widening Operator CType(ByVal a As ValueContainer(Of T)) As T
            If a Is Nothing Then Return Nothing
            Return a.Value
        End Operator
        ''' <summary>Converts value of type <see cref="ValueContainer(Of T)"/> to type <see cref="Tuple(of T)"/></summary>
        ''' <param name="a">A value to be converted</param>
        ''' <returns>A new instance of <see cref="Tuple(Of T)"/> initialized with <paramref name="a"/>.<see cref="Value">Value</see>. Null when <paramref name="a"/> is null.</returns>
        Public Shared Widening Operator CType(ByVal a As ValueContainer(Of T)) As Tuple(Of T)
            If a Is Nothing Then Return Nothing
            Return New Tuple(Of T)(a.Value)
        End Operator
        ''' <summary>Converts value of type <typeparamref name="T"/> to <see cref="ValueContainer(Of T)"/></summary>
        ''' <param name="a">A value to be converted</param>
        ''' <returns>A new instance of <see cref="ValueContainer(Of T)"/> initialized with <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As T) As ValueContainer(Of T)
            Return New ValueContainer(Of T)(a)
        End Operator
        ''' <summary>Converts value of type <see cref="Tuple(Of T)"/> to <see cref="ValueContainer(Of T)"/></summary>
        ''' <param name="a">A value to be converted</param>
        ''' <returns>A new instance of <see cref="ValueContainer(Of T)"/> initialized with <paramref name="a"/>.<see cref="Tuple(Of T).Item1">Item1</see></returns>
        Public Shared Widening Operator CType(ByVal a As Tuple(Of T)) As ValueContainer(Of T)
            If a Is Nothing Then Return Nothing
            Return New ValueContainer(Of T)(a.Item1)
        End Operator
#End Region
    End Class
#End If
End Namespace
