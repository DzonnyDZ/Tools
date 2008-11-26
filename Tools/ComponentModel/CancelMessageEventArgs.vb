Namespace ComponentModelT
#If Config <= Beta Then 'Stage: Beta
    ''' <summary><see cref="CancelEventArgs"/> with message witn reason for cancellation</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Beta"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Class CancelMessageEventArgs : Inherits CancelEventArgs
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