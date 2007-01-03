''' <summary>Represents data type that reports change of values of its members</summary>
Public Interface IReportsChange
    ''' <summary>Raised when value of member changes</summary>
    ''' <param name="sender">The source of the event</param>
    ''' <param name="e">Event information</param>
    ''' <remarks><paramref name="e"/>Should contain additional information that can be used in event-handling code</remarks>
    Event Changed(ByVal sender As IReportsChange, ByVal e As EventArgs)
End Interface
