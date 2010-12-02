
#If Config <= Nightly Then 'Stage:Nightly
Namespace ComponentModelT

    ''' <summary>Extends <see cref="PropertyChangingEventArgs"/> by adding the <see cref="P:Tools.ComponentModelT.PropertyChangingEventArgsEx.ProposedNewValue"/> property</summary>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public Class PropertyChangingEventArgsEx
        Inherits PropertyChangingEventArgs
        ''' <summary>Initializes a new instance of the <see cref="PropertyChangingEventArgsEx" /> class.</summary>
        ''' <param name="propertyName">The name of the property whose value is changing.</param>
        ''' <param name="proposedNewValue">Proposed new value of the property</param>
        Public Sub New(ByVal propertyName$, ByVal proposedNewValue As Object)
            MyBase.New(propertyName)
            _proposedNewValue = proposedNewValue
        End Sub
        Private ReadOnly _proposedNewValue As Object
        ''' <summary>Gets proposed new value of the property</summary>
        Public ReadOnly Property ProposedNewValue() As Object
            Get
                Return _proposedNewValue
            End Get
        End Property
    End Class
    ''' <summary>Type-safe version of the <see cref="PropertyChangingEventArgsEx"/> class</summary>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public Class PropertyChangingEventArgsEx(Of TValue)
        Inherits PropertyChangingEventArgsEx
        ''' <summary>Initializes a new instance of the <see cref="PropertyChangingEventArgsEx(Of T)" /> class.</summary>
        ''' <param name="propertyName">The name of the property whose value is changing.</param>
        ''' <param name="proposedNewValue">Proposed new value of the property</param>
        Public Sub New(ByVal propertyName$, ByVal proposedNewValue As TValue)
            MyBase.New(propertyName, proposedNewValue)
        End Sub
        ''' <summary>Gets proposed new value of the property</summary>
        Public Shadows ReadOnly Property ProposedNewValue As TValue
            Get
                Return MyBase.ProposedNewValue
            End Get
        End Property
    End Class
End Namespace
#End If