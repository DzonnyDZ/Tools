Namespace ComponentModel
#If Config <= Nightly Then 'Stage: Nightly
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(CancelMessageEventArgs), LastChMMDDYYYY:="05/20/2007")> _
    Public Class CancelMessageEventArgs : Inherits CancelEventArgs 'ASAP:Wiki,forum
        ''' <summary>Contains value of the <see cref="CancelMessage"/> property</summary>
        Private _CancelMessage As String
        ''' <summary>Human readabele cancel reason reported to source of the event"</summary>
        Public Overridable Property CancelMessage() As String
            Get
                Return _CancelMessage
            End Get
            Set(ByVal value As String)
                _CancelMessage = value
            End Set
        End Property
    End Class
#End If
End Namespace