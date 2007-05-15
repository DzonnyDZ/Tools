#If Config <= Release Then
''' <summary>This class can wrap everything to be class</summary>
''' <remarks>This removes limitation of <see cref="Nullable(Of T)"/> that limits wrapped object to be structure</remarks>
<Author("Đonny", "dzony.dz@gmail.com"), Version(1, 0, GetType(Box(Of Boolean)), LastChMMDDYYYY:="12/20/2006")> _
<DebuggerDisplay("Box ({Item})")> _
Public Class Box(Of T) : Inherits Cloenable(Of Box(Of T))
    ''' <summary>Contains value of the <see cref="Item"/> property</summary>
    Private _Item As T
    ''' <summary>The value of boxed type</summary>
    ''' <returns>Current boxed value</returns>
    ''' <value>New boxed value</value>
    Public Overridable Property Item() As T
        Get
            Return _Item
        End Get
        Set(ByVal value As T)
            _Item = value
        End Set
    End Property
    ''' <summary>CTor</summary>
    ''' <param name="item">Initial value of boxed type</param>
    Public Sub New(ByVal item As T)
        Me.Item = item
    End Sub
    ''' <summary>Unboxes boxed value</summary>
    ''' <param name="a">Boxed value</param>
    ''' <returns>Unboxed value</returns>
    ''' <remarks><seealso cref="Item"/></remarks>
    Public Shared Widening Operator CType(ByVal a As Box(Of T)) As T
        Return a.Item
    End Operator
    ''' <summary>Boxes value</summary>
    ''' <param name="a">Not boxed value</param>
    ''' <returns>New instance of <see cref="Box(Of T)"/> that boxes value <paramref name="a"/></returns>
    Public Shared Widening Operator CType(ByVal a As T) As Box(Of T)
        Return New Box(Of T)(a)
    End Operator

    ''' <summary>Creates a new object that is a copy of the current instance.</summary>
    ''' <returns>A new object that is a copy of this instance</returns>
    ''' <remarks>If <see cref="T"/> implements <see cref="System.ICloneable"/> then also boxed object is cloned (deep copy), othervise only reference is passed to the new instance.</remarks>
    Public Overrides Function Clone() As Box(Of T)
        If TypeOf Item Is ICloneable Then
            Return New Box(Of T)(CType(Item, ICloneable).Clone)
        Else
            Return New Box(Of T)(Item)
        End If
    End Function
    ''' <summary>String representationm of current instance</summary>
    Public Overrides Function ToString() As String
        If Item IsNot Nothing Then Return Item.ToString Else Return ""
    End Function
End Class
#End If