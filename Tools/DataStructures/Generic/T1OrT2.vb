Namespace DataStructures.Generic
#If Config <= Release Then
    ''' <summary>Represents type that can contain either value of type T1 or value of type T2. It cannot contain both values at the same time.</summary>
    ''' <typeparam name="T1">One of alternativelly stored types</typeparam>
    ''' <typeparam name="T2">One of alternativelly stored types</typeparam>
    ''' <remarks>
    ''' Although this interface inherits <see cref="IPair(Of T1, T2)"/> be very careful when utilizing this inheritance because behaviour of <see cref="IT1orT2(Of T1, T2)"/> is different whnen storing values (it can contain only one value at the sam time). Consider utilizing this ihneritance only in read-only way.
    ''' </remarks>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 1, GetType(IT1orT2(Of Integer, Type)), LastChMMDDYYYY:="12/21/2006")> _
    Public Interface IT1orT2(Of T1, T2)
        Inherits IPair(Of T1, T2)
        ''' <summary>Gets or sets value of type <see cref="T1"/></summary>
        ''' <value>Non-null value to set value of type <see cref="T1"/> and delete value of type <see cref="T2"/> or nothing to delete value of type <see cref="T1"/></value>
        ''' <returns>If this instance contains value of type <see cref="T1"/> then returns it, otherwise return null</returns>
        ''' <remarks>
        ''' If <see cref="value1"/> retruns null it means that either value of type <see cref="T1"/> is not present in this instance or it is present but it is null. Check <see cref="contains1"/> property in order to determine actual situation.
        ''' You must set this property to nothing and then set <see cref="contains1"/> property to true in order to store null value of type <see cref="T1"/>.
        ''' </remarks>
        Overloads Property value1() As T1
        ''' <summary>Gets or sets value of type <see cref="T2"/></summary>
        ''' <value>Non-null value to set value of type <see cref="T2"/> and delete value of type <see cref="T2"/> or nothing to delete value of type <see cref="T2"/></value>
        ''' <returns>If this instance contains value of type <see cref="T2"/> then returns it, otherwise return null</returns>
        ''' <remarks>
        ''' If <see cref="value2"/> retruns null it means that either value of type <see cref="T2"/> is not present in this instance or it is present but it is null. Check <see cref="contains2"/> property in order to determine actual situation.
        ''' You must set this property to nothing and then set <see cref="contains1"/> property to true in order to store null value of type <see cref="T2"/>.
        ''' </remarks>
        Overloads Property value2() As T2
        ''' <summary>Get or sets stored value in type-unsafe way</summary>
        ''' <value>New value to be stored in this instance</value>
        ''' <returns>Value stored in this instance</returns>
        ''' <exception cref="NullReferenceException">When trying to obtain value from instance that contains value of type neither <see cref="T1"/> nor <see cref="T2"/></exception>
        ''' <exception cref="ArgumentException">When trying to set value of type other than <see cref="T1"/> or <see cref="T2"/></exception>
        Property objValue() As Object
        ''' <summary>Determines if currrent instance contains value of type <see  cref="T1"/></summary>
        ''' <value>
        ''' True to delete value of type <see cref="T1"/> 
        ''' False to mark this instance as containing value of type <see cref="T1"/> and not containing value of type <see cref="T2"/>.
        ''' </value>
        ''' <returns>True if this instance contains value of type <see cref="T1"/> (even if containde value is null)</returns>
        ''' <remarks>
        ''' When <see cref="contains1"/> is True and is set to True nothing happens.
        ''' When <see cref="contains1"/> is False and is set tor True nothing is stored in <see cref="value1"/> and <see cref="value2"/> is removed.
        ''' </remarks>
        Property contains1() As Boolean
        ''' <summary>Determines if currrent instance contains value of type <see  cref="T2"/></summary>
        ''' <value>
        ''' True to delete value of type <see cref="T2"/> 
        ''' False to mark this instance as containing value of type <see cref="T2"/> and not containing value of type <see cref="T1"/>.
        ''' </value>
        ''' <returns>True if this instance contains value of type <see cref="T2"/> (even if containde value is null)</returns>
        ''' <remarks>
        ''' When <see cref="contains2"/> is True and is set to True nothing happens.
        ''' When <see cref="contains2"/> is False and is set tor True nothing is stored in <see cref="value2"/> and <see cref="value1"/> is removed.
        ''' </remarks>
        Property contains2() As Boolean
        ''' <summary>Identifies whether this instance contains value of specified type</summary>
        ''' <param name="T">Type to be contained</param>
        ''' <returns>True if this instance contais value of type <paramref name="T"/> otherwise False</returns>
        ReadOnly Property contains(ByVal T As Type) As Boolean
        ''' <summary>Determines whether instance contains value of neither type <see cref="T1"/> nor type <see cref="T2"/></summary>
        ''' <returns>True when both values are not present. False if one of values is present (even if it contains null)</returns>
        ReadOnly Property IsEmpty() As Boolean
    End Interface
#End If

#If Config <= Release Then
    ''' <summary>Implements type that can contain either value of type T1 ore value of type T2. It cannot contain both values at the same time.</summary>
    ''' <typeparam name="T1">One of alternativelly stored types</typeparam>
    ''' <typeparam name="T2">One of alternativelly stored types</typeparam>
    ''' <remarks>
    ''' Although rhis class implements <see cref="IPair(Of T1, T2)"/> through <see cref="IT1orT2(Of T1, T2)"/> be careful when utilizing this implementation because behaviour of <see cref="IT1orT2(Of T1, T2)"/> is different when storing values (it can contain only one value at the sam time). Consider utilizing this inheritance only in read-only way.
    ''' </remarks>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 1, GetType(T1orT2(Of Integer, Type)), LastChMMDDYYYY:="03/11/2007")> _
    <DebuggerDisplay("{ToString}")> _
    Public Class T1orT2(Of T1, T2)
        Inherits Cloenable(Of IT1orT2(Of T1, T2))
        Implements IT1orT2(Of T1, T2)
        ''' <summary>Contains value of the <see cref="value1"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _value1 As Box(Of T1) = Nothing
        ''' <summary>Contains value of the <see cref="value2"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _value2 As Box(Of T2) = Nothing
        ''' <summary>CTor - initializes new instance of <see cref="T1orT2(Of T1, T2)"/> with value of type <see cref="T1"/></summary>
        ''' <param name="value">Value to be contained in new instance</param>
        Public Sub New(ByVal value As T1)
            value1 = value
        End Sub
        ''' <summary>CTor - initializes en empty instance of <see cref="T1orT2(Of T1, T2)"/></summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor - initializes new instance of <see cref="T1orT2(Of T1, T2)"/> with value of type <see cref="T2"/></summary>
        ''' <param name="value">Value to be contained in new instance</param>
        Private Sub New(ByVal value As T2)
            value2 = value
        End Sub
        ''' <summary>Gets or sets value of type <see cref="T1"/></summary>
        ''' <value>Non-null value to set value of type <see cref="T1"/> and delete value of type <see cref="T2"/> or nothing to delete value of type <see cref="T1"/></value>
        ''' <returns>If this instance contains value of type <see cref="T1"/> then returns it, otherwise return null</returns>
        ''' <remarks>
        ''' If <see cref="value1"/> retruns null it means that either value of type <see cref="T1"/> is not present in this instance or it is present but it is null. Check <see cref="contains1"/> property in order to determine actual situation.
        ''' You must set this property to nothing and then set <see cref="contains1"/> property to true in order to store null value of type <see cref="T1"/>.
        ''' </remarks>
        Public Overridable Property value1() As T1 Implements IT1orT2(Of T1, T2).value1, IPair(Of T1, T2).Value1
            Get
                If _value1 Is Nothing Then Return Nothing Else Return CType(_value1, T1)
            End Get
            Set(ByVal value As T1)
                If value Is Nothing Then _value1 = Nothing Else _value1 = CType(value, Box(Of T1))
                If _value1 IsNot Nothing Then _value2 = Nothing
            End Set
        End Property
        ''' <summary>Gets or sets value of type <see cref="T2"/></summary>
        ''' <value>Non-null value to set value of type <see cref="T2"/> and delete value of type <see cref="T2"/> or nothing to delete value of type <see cref="T2"/></value>
        ''' <returns>If this instance contains value of type <see cref="T2"/> then returns it, otherwise return null</returns>
        ''' <remarks>
        ''' If <see cref="value2"/> retruns null it means that either value of type <see cref="T2"/> is not present in this instance or it is present but it is null. Check <see cref="contains2"/> property in order to determine actual situation.
        ''' You must set this property to nothing and then set <see cref="contains1"/> property to true in order to store null value of type <see cref="T2"/>.
        ''' </remarks>
        Public Overridable Property value2() As T2 Implements IT1orT2(Of T1, T2).value2, IPair(Of T1, T2).Value2
            Get
                If value2 Is Nothing Then Return Nothing Else Return CType(_value2, T2)
            End Get
            Set(ByVal value As T2)
                If value Is Nothing Then _value2 = Nothing Else _value2 = CType(value, Box(Of T2))
                If _value2 IsNot Nothing Then _value1 = Nothing
            End Set
        End Property
        ''' <summary>Determines if currrent instance contains value of type <see  cref="T1"/></summary>
        ''' <value>
        ''' True to delete value of type <see cref="T1"/> 
        ''' False to mark this instance as containing value of type <see cref="T1"/> and not containing value of type <see cref="T2"/>.
        ''' </value>
        ''' <returns>True if this instance contains value of type <see cref="T1"/> (even if containde value is null)</returns>
        ''' <remarks>
        ''' When <see cref="contains1"/> is True and is set to True nothing happens.
        ''' When <see cref="contains1"/> is False and is set tor True nothing is stored in <see cref="value1"/> and <see cref="value2"/> is removed.
        ''' </remarks>
        Public Overridable Property contains1() As Boolean Implements IT1orT2(Of T1, T2).contains1
            Get
                Return _value1 IsNot Nothing
            End Get
            Set(ByVal value As Boolean)
                If value AndAlso Not contains1 Then
                    _value1 = New Box(Of T1)(Nothing)
                    _value2 = Nothing
                ElseIf Not value Then
                    _value1 = Nothing
                End If
            End Set
        End Property
        ''' <summary>Determines if currrent instance contains value of type <see  cref="T2"/></summary>
        ''' <value>
        ''' True to delete value of type <see cref="T2"/> 
        ''' False to mark this instance as containing value of type <see cref="T2"/> and not containing value of type <see cref="T1"/>.
        ''' </value>
        ''' <returns>True if this instance contains value of type <see cref="T2"/> (even if containde value is null)</returns>
        ''' <remarks>
        ''' When <see cref="contains2"/> is True and is set to True nothing happens.
        ''' When <see cref="contains2"/> is False and is set tor True nothing is stored in <see cref="value2"/> and <see cref="value1"/> is removed.
        ''' </remarks>
        Public Overridable Property contains2() As Boolean Implements IT1orT2(Of T1, T2).contains2
            Get
                Return _value2 IsNot Nothing
            End Get
            Set(ByVal value As Boolean)
                If value AndAlso Not contains1 Then
                    _value1 = New Box(Of T1)(Nothing)
                    _value2 = Nothing
                ElseIf Not value Then
                    _value1 = Nothing
                End If
            End Set
        End Property
        ''' <summary>Determines whether instance contains value of neither type <see cref="T1"/> nor type <see cref="T2"/></summary>
        ''' <returns>True when both values are not present. False if one of values is present (even if it contains null)</returns>
        Public Overridable ReadOnly Property IsEmpty() As Boolean Implements IT1orT2(Of T1, T2).IsEmpty
            Get
                Return Not contains1 And Not contains2
            End Get
        End Property
        ''' <summary>Gets copy if this instance or fills this instance with content of another instance</summary>
        ''' <value>Instance which's content will be used to fill this instance</value>
        ''' <returns>New instance ininialized with content of this instance</returns>
        ''' <exception cref="NullReferenceException"><see cref="contains1"/> is False and <see cref="contains2"/> is False</exception>
        Public Overridable Property value() As T1orT2(Of T1, T2)
            Get
                If contains1 Then
                    Return value1
                ElseIf contains2 Then
                    Return value2
                End If
                Throw New NullReferenceException("Cannot return value for this instance of T1orT2 because it contains neither value1 nor value2.")
            End Get
            Set(ByVal value As T1orT2(Of T1, T2))
                If value.contains1 Then
                    value1 = value
                    contains1 = True
                Else
                    value2 = value
                    contains2 = True
                End If
            End Set
        End Property
        ''' <summary>Boxes value of type <see cref="T1"/> into new instance of <see cref="T1orT2(Of T1, T2)"/></summary>
        ''' <param name="a">Value to be boxed</param>
        ''' <returns>New instance of <see cref="T1orT2(Of T1, T2)"/> initialized with <paramref name="a"/></returns>
        Shared Widening Operator CType(ByVal a As T1) As T1orT2(Of T1, T2)
            Return New T1orT2(Of T1, T2)(a)
        End Operator
        ''' <summary>Boxes value of type <see cref="T2"/> into new instance of <see cref="T1orT2(Of T1, T2)"/></summary>
        ''' <param name="a">Value to be boxed</param>
        ''' <returns>New instance of <see cref="T1orT2(Of T1, T2)"/> initialized with <paramref name="a"/></returns>
        Shared Widening Operator CType(ByVal a As T2) As T1orT2(Of T1, T2)
            Return New T1orT2(Of T1, T2)(a)
        End Operator
        ''' <summary>Unboxes value of type <see cref="T1"/> from <see cref="T1orT2(Of T1, T2)"/> when <paramref name="a"/> contains <see cref="value1"/>.</summary>
        ''' <param name="a">Instance that may contain value to be unboxed</param>
        ''' <returns>Value of <see cref="value1"/> property of <paramref name="a"/></returns>
        ''' <exception cref="InvalidCastException"><paramref name="a"/>doesn't contain value of type <see cref="T1"/></exception>
        Shared Narrowing Operator CType(ByVal a As T1orT2(Of T1, T2)) As T1
            If a.contains1 Then
                Return a.value1
            Else
                Throw New InvalidCastException("This T1orT2 cannot be converted to T1 because it doesn't contain value of T1")
            End If
        End Operator
        ''' <summary>Unboxes value of type <see cref="T2"/> from <see cref="T1orT2(Of T1, T2)"/> when <paramref name="a"/> contains <see cref="value2"/>.</summary>
        ''' <param name="a">Instance that may contain value to be unboxed</param>
        ''' <returns>Value of <see cref="value2"/> property of <paramref name="a"/></returns>
        ''' <exception cref="InvalidCastException"><paramref name="a"/>doesn't contain value of type <see cref="T2"/></exception>
        Shared Narrowing Operator CType(ByVal a As T1orT2(Of T1, T2)) As T2
            If a.contains2 Then
                Return a.value2
            Else
                Throw New InvalidCastException("This T1orT2 cannot be converted to T2 because it doesn't contain value of T2")
            End If
        End Operator
        ''' <summary>Identifies whether this instance contains value of specified type</summary>
        ''' <param name="T">Type to be contained</param>
        ''' <returns>True if this instance contais value of type <paramref name="T"/> otherwise False</returns>
        Public Overridable ReadOnly Property contains(ByVal T As Type) As Boolean Implements IT1orT2(Of T1, T2).contains
            Get
                Return (GetType(T1) Is T AndAlso contains1) OrElse (GetType(T2) Is T AndAlso contains2)
            End Get
        End Property
        ''' <summary>Return new instance of <see cref="T1orT2(Of T2, T1)"/> initialized by value of this instance</summary>
        ''' <returns>Instance of type with swapped types <see cref="T1"/> and <see cref="T2"/></returns>
        Public Overridable Function Swap() As T1orT2(Of T2, T1)
            Dim ret As New T1orT2(Of T2, T1)
            If contains1 Then
                ret.value2 = value1
                ret.contains1 = True
            ElseIf contains2 Then
                ret.value1 = value2
                ret.contains2 = True
            End If
            Return ret
        End Function

        ''' <summary>Swaps values <see cref="Value1"/> and <see cref="Value2"/></summary>        
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Function Swap1() As IPair(Of T2, T1) Implements IPair(Of T1, T2).Swap
            Return Swap()
        End Function

        ''' <summary>Returns new instance of <see cref="T1orT2(Of T1, T2)"/> inicialized by value of current instance</summary>
        Public Overrides Function Clone() As IT1orT2(Of T1, T2)
            Return value
        End Function
        ''' <summary>Get or sets stored value in type-unsafe way</summary>
        ''' <value>New value to be stored in this instance</value>
        ''' <returns>Value stored in this instance</returns>
        ''' <exception cref="NullReferenceException">When trying to obtain value from instance that contains value of type neither <see cref="T1"/> nor <see cref="T2"/></exception>
        ''' <exception cref="ArgumentException">When trying to set value of type other than <see cref="T1"/> or <see cref="T2"/></exception>
        Public Overridable Property objValue() As Object Implements IT1orT2(Of T1, T2).objValue
            Get
                If contains1 Then
                    Return value1
                ElseIf contains2 Then
                    Return value2
                Else
                    Throw New NullReferenceException("Cannot return value for this instance of T1orT2 because it contains neither value1 nor value2.")
                End If
            End Get
            Set(ByVal value As Object)
                If TypeOf value Is T1 Then
                    value1 = value
                ElseIf TypeOf value Is T2 Then
                    value2 = value
                Else
                    Throw New ArgumentException("Value of ObjeValue property must be either of type T1 or of type T1")
                End If
            End Set
        End Property
        ''' <summary>String representation of instance</summary>
        Public Overrides Function ToString() As String
            If contains1 Then
                If value1 Is Nothing Then
                    Return GetType(T1).Name & " ()"
                Else
                    Return GetType(T1).Name & " (" & value1.ToString & ")"
                End If
            ElseIf contains2 Then
                If value2 Is Nothing Then
                    Return GetType(T2).Name & " ()"
                Else
                    Return GetType(T2).Name & " (" & value2.ToString & ")"
                End If
            Else
                Return "()"
            End If
        End Function
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overloads Function Clone1() As IPair(Of T1, T2) Implements ICloneable(Of IPair(Of T1, T2)).Clone
            Return Clone()
        End Function
    End Class
#End If
End Namespace