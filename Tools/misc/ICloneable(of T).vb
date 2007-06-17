#If Config <= Release Then
''' <summary>Type-safe <see cref="ICloneable"/> interface</summary>
<Author("Đonny", "dzony.dz@gmail.com"), Version(1, 0, GetType(ICloneable(Of )), LastChMMDDYYYY:="12/20/2006")> _
Public Interface ICloneable(Of T) : Inherits ICloneable
    ''' <summary>Creates a new object that is a copy of the current instance.</summary>
    ''' <returns>A new object that is a copy of this instance</returns>
    Shadows Function Clone() As T
End Interface
''' <summary>Simple <see cref="ICloneable(Of T)"/> implementation that implements <see cref="ICloneable.Clone"/></summary>
''' <typeparam name="T">The type that is cloned</typeparam>
''' <remarks>
''' Inherit from this class instead of implementing <see cref="ICloneable(Of T)"/> and you will have to only implement <see cref="Tools.ICloneable.Clone"/> because <see cref="ICloneable.Clone"/> is already implemented.
''' <seealso cref="Cloenable.Clone1"/>
''' </remarks>
<Author("Đonny", "dzony.dz@gmail.com"), Version(1, 0, GetType(Cloenable(Of )))> _
Public MustInherit Class Cloenable(Of T) : Implements ICloneable(Of T)
    ''' <summary>Implements <see cref="System.ICloneable.Clone"/></summary>
    ''' <returns>Returns the result of <see cref="Clone"/> function</returns>
    Public Function Clone1() As Object Implements System.ICloneable.Clone
        Return Clone()
    End Function
    ''' <summary>This is the implementation of <see cref="Tools.ICloneable.Clone"/>. Creates a new object that is a copy of the current instance.</summary>
    ''' <returns>A new object that is a copy of this instance</returns>
    Public MustOverride Function Clone() As T Implements ICloneable(Of T).Clone
End Class
#End If