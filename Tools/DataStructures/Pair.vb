Namespace DataStructures.Generic
#If Config <= Release Then
    ''' <summary>Type tha contains value of two distinct types</summary>
    ''' <typeparam name="T1">Type of first value</typeparam>
    ''' <typeparam name="T2">Type of second value</typeparam>
    <Author("Ðonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(IPair(Of String, Char)), LastChange:="12/21/2006")> _
    Public Interface IPair(Of T1, T2) : Inherits ICloneable(Of IPair(Of T1, T2))
        ''' <summary>Value of type <see cref="T1"/></summary>
        Property Value1() As T1
        ''' <summary>Value of type <see cref="T2"/></summary>
        Property Value2() As T2
        ''' <summary>Swaps values <see cref="Value1"/> and <see cref="Value2"/></summary>
        Function Swap() As IPair(Of T2, T1)
    End Interface
#End If

#If Config <= Nightly Then
    ''' <summary>Implements <see cref="IPair(Of T1,T2)"/> as reference type</summary>
    ''' <typeparam name="T1">Type of Value1</typeparam>
    ''' <typeparam name="T2">Type of Value2</typeparam>
   <Author("Ðonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(IPair(Of String, Char)), LastChange:="12/21/2006")> _
    Public Class Pair(Of T1, T2)
        Inherits Cloenable(Of Pair(Of T1, T2))
        Implements IPair(Of T1, T2)
        ''' <summary>Contains value of the <see cref="Value1"/> property</summary>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
        Private _Value1 As T1
        ''' <summary>Contains value of the <see cref="Value2"/> property</summary>
        <DebuggerBrowsable(DebuggerBrowsableState.Never)> _
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

        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Function Clone1() As IPair(Of T1, T2) Implements ICloneable(Of IPair(Of T1, T2)).Clone
            Return Clone()
        End Function

        Public Function Swap() As IPair(Of T2, T1) Implements IPair(Of T1, T2).Swap

        End Function

        Public Overridable Property Value1() As T1 Implements IPair(Of T1, T2).Value1
            Get

            End Get
            Set(ByVal value As T1)

            End Set
        End Property

        Public Overridable Property Value2() As T2 Implements IPair(Of T1, T2).Value2
            Get

            End Get
            Set(ByVal value As T2)

            End Set
        End Property

        Public Overrides Function Clone() As Pair(Of T1, T2)

        End Function
    End Class
#End If
End Namespace