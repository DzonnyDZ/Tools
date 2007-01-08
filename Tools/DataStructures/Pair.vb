Namespace DataStructures.Generic
#If Config <= Release Then
    ''' <summary>Type tha contains value of two distinct types</summary>
    ''' <typeparam name="T1">Type of first value</typeparam>
    ''' <typeparam name="T2">Type of second value</typeparam>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(IPair(Of String, Char)), LastChange:="12/21/2006")> _
    Public Interface IPair(Of T1, T2) : Inherits ICloneable(Of IPair(Of T1, T2))
        ''' <summary>Value of type <see cref="T1"/></summary>
        Property Value1() As T1
        ''' <summary>Value of type <see cref="T2"/></summary>
        Property Value2() As T2
        ''' <summary>Swaps values <see cref="Value1"/> and <see cref="Value2"/></summary>
        Function Swap() As IPair(Of T2, T1)
    End Interface
#End If

#If Config <= Release Then
    ''' <summary>Implements <see cref="IPair(Of T1,T2)"/> as reference type</summary>
    ''' <typeparam name="T1">Type of Value1</typeparam>
    ''' <typeparam name="T2">Type of Value2</typeparam>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(Pair(Of String, Char)), LastChange:="12/21/2006")> _
    Public Class Pair(Of T1, T2)
        Inherits Cloenable(Of Pair(Of T1, T2))
        Implements IPair(Of T1, T2)
        ''' <summary>Contains value of the <see cref="Value1"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Value1 As T1
        ''' <summary>Contains value of the <see cref="Value2"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Value2 As T2
        ''' <summary>CTor - initialize with two values</summary>
        ''' <param name="V1">Initial value for <see cref="Value1"/></param>
        ''' <param name="V2">Initial value for <see cref="Value2"/></param>
        Public Sub New(ByVal V1 As T1, ByVal V2 As T2)
            Value1 = V1
            Value2 = V2
        End Sub
        ''' <summary>CTor - initialize with another instance of <see cref="IPair(Of T1, T2)"/></summary>
        ''' <param name="a">Instance to initialize new istance</param>
        Public Sub New(ByVal a As IPair(Of T1, T2))
            Me.New(a.Value1, a.Value2)
        End Sub
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Function Clone1() As IPair(Of T1, T2) Implements ICloneable(Of IPair(Of T1, T2)).Clone
            Return Clone()
        End Function

        ''' <summary>Swaps values <see cref="Value1"/> and <see cref="Value2"/></summary>        
        Public Function Swap() As IPair(Of T2, T1) Implements IPair(Of T1, T2).Swap
            Return New Pair(Of T2, T1)(Value2, Value1)
        End Function

        ''' <summary>Value of type <see cref="T1"/></summary>        
        Public Overridable Property Value1() As T1 Implements IPair(Of T1, T2).Value1
            Get
                Return _Value1
            End Get
            Set(ByVal value As T1)
                _Value1 = value
            End Set
        End Property

        ''' <summary>Value of type <see cref="T2"/></summary>        
        Public Overridable Property Value2() As T2 Implements IPair(Of T1, T2).Value2
            Get
                Return _Value2
            End Get
            Set(ByVal value As T2)
                _Value2 = value
            End Set
        End Property

        ''' <summary>Returns new instance of <see cref="IPair(Of T1, T2)"/> initialized with current instance</summary>
        ''' <returns>New instance of <see cref="IPair(Of T1, T2)"/> initialized with current instance if either <see cref="T1"/> or <see cref="T2"/> implements <see cref="ICloneable"/> then they are also cloned via <see cref="ICloneable.Clone"/></returns>
        Public Overrides Function Clone() As Pair(Of T1, T2)
            Dim new1 As T1, new2 As T2
            If TypeOf Value1 Is ICloneable Then
                new1 = CType(Value1, ICloneable).Clone
            Else
                new1 = Value1
            End If
            If TypeOf Value2 Is ICloneable Then
                new2 = CType(Value2, ICloneable).Clone
            Else
                new2 = Value2
            End If
            Return New Pair(Of T1, T2)(new1, new2)
        End Function

        ''' <summary>Converts <see cref="Pair(Of T1, T2)"/> into <see cref="KeyValuePair(Of T1, T2)"/></summary>
        ''' <param name="a">Value to be converted</param>
        ''' <returns>Converted <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As Pair(Of T1, T2)) As KeyValuePair(Of T1, T2)
            Return New KeyValuePair(Of T1, T2)(a.Value1, a.Value2)
        End Operator
        ''' <summary>Converts <see cref="KeyValuePair(Of T1, T2)"/> into <see cref="Pair(Of T1, T2)"/></summary>
        ''' <param name="a">Value to be converted</param>
        ''' <returns>Converted <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As KeyValuePair(Of T1, T2)) As Pair(Of T1, T2)
            Return New Pair(Of T1, T2)(a.Key, a.Value)
        End Operator
    End Class

    ''' <summary>Limits <see cref="Pair(Of T1, T2)"/> to contain only values of the same type</summary>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(Pair(Of Long)), LastChange:="12/21/2006")> _
    Public Class Pair(Of T)
        Inherits Pair(Of T, T)
        Implements ICloneable(Of Pair(Of T))
        ''' <summary>CTor - initialize with two values</summary>
        ''' <param name="V1">Initial value for <see cref="Value1"/></param>
        ''' <param name="V2">Initial value for <see cref="Value2"/></param>
        Public Sub New(ByVal V1 As T, ByVal V2 As T)
            MyBase.New(V1, V2)
        End Sub
        ''' <summary>CTor - initialize with another instance of <see cref="IPair(Of T1, T1)"/></summary>
        ''' <param name="a">Instance to initialize new istance</param>
        Public Sub New(ByVal a As IPair(Of T, T))
            MyBase.New(a)
        End Sub
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Shadows Function Clone() As Pair(Of T) Implements ICloneable(Of Pair(Of T)).Clone
            Return MyBase.Clone
        End Function
    End Class
#End If
End Namespace