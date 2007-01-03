#If Config <= Beta Then 'Stage: Nightly
''' <summary>Represents data type that reports change of values of its members</summary>
<Author("Ðonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(IReportsChange), LastChange:="1/3/2007")> _
Public Interface IReportsChange
    ''' <summary>Raised when value of member changes</summary>
    ''' <param name="sender">The source of the event</param>
    ''' <param name="e">Event information</param>
    ''' <remarks><paramref name="e"/>Should contain additional information that can be used in event-handling code (e.g. use <see cref="ValueChangedEventArgs(Of T)"/> class)</remarks>
    Event Changed(ByVal sender As IReportsChange, ByVal e As EventArgs)

    ''' <summary>Represents information about change of value</summary>
    ''' <typeparam name="T">Type of value contained in old and new value properties</typeparam>
    Class ValueChangedEventArgs(Of T) : Inherits EventArgs
        ''' <summary>Contains value of the<see cref="OldValue"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _OldValue As T
        ''' <summary>Contains value of the<see cref="NewValue"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _NewValue As T
        ''' <summary>Contains value of the<see cref="ValueName"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ValueName As String
        Public Sub New(ByVal OldValue As T, ByVal NewValue As T, ByVal ValueName As String)
            _OldValue = OldValue
            _NewValue = NewValue
            _ValueName = ValueName
        End Sub
        ''' <summary>Value fo item before change</summary>
        Public ReadOnly Property OldValue() As T
            <DebuggerStepThrough()> Get
                Return _OldValue
            End Get
        End Property
        ''' <summary>Current value of item (after chenge)</summary>
        Public ReadOnly Property NewValue() As T
            <DebuggerStepThrough()> Get
                Return _NewValue
            End Get
        End Property
        ''' <summary>Name of changed value</summary>
        Public ReadOnly Property ValueName() As String
            <DebuggerStepThrough()> Get
                Return _ValueName
            End Get
        End Property
    End Class
End Interface
#End If