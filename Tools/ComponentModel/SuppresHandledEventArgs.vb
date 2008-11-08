#If Config <= Nightly Then 'Stage: Nightly
Namespace ComponentModelT
    ''' <summary>Provides event arguments for events that supports marking as handled and suppressing of action event may result to.</summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Class SuppresHandledEventArgs
        Inherits EventArgs
        ''' <summary>Contains value of the <see cref="Handled"/> property</summary>
        Private _Handled As Boolean
        ''' <summary>Gets or sets value indicating if calle handles the message</summary>
        Public Property Handled() As Boolean
            Get
                Return _Handled
            End Get
            Set(ByVal value As Boolean)
                _Handled = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Suppress"/> property</summary>
        Private _Suppress As Boolean
        ''' <summary>Gets or sets value indicating if hooks following this one and windows message may be suppressed</summary>
        ''' <value>True to suppress calling of next hooks and ocurence of windows message</value>
        ''' <remarks>In most scenarios this property is ignored when <see cref="Handled"/> is false</remarks>
        Public Property Suppress() As Boolean
            Get
                Return _Suppress
            End Get
            Set(ByVal value As Boolean)
                _Suppress = value
            End Set
        End Property
    End Class
End Namespace
#End If