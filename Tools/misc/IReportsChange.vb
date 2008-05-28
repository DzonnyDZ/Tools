#If Config <= Release Then
''' <summary>Represents data type that reports change of values of its members</summary>
<Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(IReportsChange), LastChange:="01/03/2007")> _
Public Interface IReportsChange
    ''' <summary>Raised when value of member changes</summary>
    ''' <remarks><paramref name="e"/>Should contain additional information that can be used in event-handling code (e.g. use <see cref="ValueChangedEventArgs(Of T)"/> class)</remarks>
    Event Changed As ChangedEventHandler
    ''' <summary>Delegate for the <see cref="Changed"/> event</summary>
    ''' <param name="sender">The source of the event</param>
    ''' <param name="e">Event information</param>
    Delegate Sub ChangedEventHandler(ByVal sender As IReportsChange, ByVal e As EventArgs)
    ''' <summary>Represents common base for all <see cref="ValueChangedEventArgs"/> generic's instances</summary>
    MustInherit Class ValueChangedEventArgsBase : Inherits EventArgs
        ''' <summary>Name of changed value</summary>
        Public Overridable ReadOnly Property ValueName() As String
            <DebuggerStepThrough()> Get
                Return _ValueName
            End Get
        End Property
        ''' <summary>Contains value of the<see cref="ValueName"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ValueName As String
        ''' <summary>CTor</summary>
        ''' <param name="ValueName">Value of the <see cref="ValueName"/> property</param>
        Public Sub New(ByVal ValueName As String)
            _ValueName = ValueName
        End Sub
    End Class

    ''' <summary>Represents information about change of value</summary>
    ''' <typeparam name="T">Type of value contained in old and new value properties</typeparam>
    Class ValueChangedEventArgs(Of T) : Inherits ValueChangedEventArgsBase
        ''' <summary>Contains value of the<see cref="OldValue"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _OldValue As T
        ''' <summary>Contains value of the<see cref="NewValue"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _NewValue As T
        ''' <summary>CTor</summary>
        ''' <param name="OldValue">Value of item before change</param>
        ''' <param name="NewValue">Value of item after change</param>
        ''' <param name="ValueName">Name of changed value</param>
        Public Sub New(ByVal OldValue As T, ByVal NewValue As T, ByVal ValueName As String) ', ByVal [Property] As [Delegate])
            MyBase.New(ValueName)
            _OldValue = OldValue
            _NewValue = NewValue
        End Sub
        ''' <summary>Value of item before change</summary>
        Public Overridable ReadOnly Property OldValue() As T
            <DebuggerStepThrough()> Get
                Return _OldValue
            End Get
        End Property
        ''' <summary>Current value of item (after chenge)</summary>
        Public Overridable ReadOnly Property NewValue() As T
            <DebuggerStepThrough()> Get
                Return _NewValue
            End Get
        End Property
        Private _Prop As [Delegate]
        Public Overridable ReadOnly Property Prop() As [Delegate]
            Get
                Return _Prop
            End Get
        End Property
    End Class
End Interface
#End If