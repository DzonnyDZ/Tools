#If Config <= Release Then
''' <summary>Type tha contains value of two distinct types</summary>
''' <typeparam name="T1">Type of first value</typeparam>
''' <typeparam name="T2">Type of second value</typeparam>
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
Public Class Pair(Of T1, T2)
    Inherits Cloenable(Of Pair(Of T1, T2))
    Implements IPair(Of T1, T2)

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

    Public  Overrides Function Clone() As Pair(Of T1, T2)

    End Function
End Class
#End If
