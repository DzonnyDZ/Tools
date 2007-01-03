#If Config <= Beta Then 'Stage: Nightly
''' <summary>Represents data type that reports change of values of its members</summary>
<Author("Ðonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(IReportsChange), LastChange:="1/3/2007")> _
Public Interface IReportsChange
    ''' <summary>Raised when value of member changes</summary>
    ''' <param name="sender">The source of the event</param>
    ''' <param name="e">Event information</param>
    ''' <remarks><paramref name="e"/>Should contain additional information that can be used in event-handling code</remarks>
    Event Changed(ByVal sender As IReportsChange, ByVal e As EventArgs)

    Class ValueChangedEventArgs(Of T) : Inherits EventArgs
        Private _OldValue As T
        Private _NewValue As T
        Private _ValueName As String
        Private _ValueItem As Reflection.MemberInfo
    End Class
End Interface
#End If