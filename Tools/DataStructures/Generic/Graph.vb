#If Config <= Nightly Then 'Stage: Nightly
Namespace DataStructuresT.GenericT
    Public Interface IGraphNode
        ReadOnly Property Edges() As IList(Of IGraphEdge)
        ReadOnly Property Graph() As Graph
    End Interface
    Public Interface IGraphEdge
        ReadOnly Property Graph() As Graph
        Property Left() As IGraphNode
        Property Right() As IGraphNode
        Property Direction() As EdgeDirections
        Property Weight As 
    End Interface

    Public Enum EdgeDirections
        Both = 0
        LtR = 1
        RtL = 2
    End Enum

    ''' <summary></summary>
    Public Class Graph


    End Class
End Namespace
#End If