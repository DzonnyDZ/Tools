Imports System.ComponentModel, Tools.ExtensionsT
Imports System.Runtime.InteropServices

#If Config <= Nightly Then 'Stage: Nightly
Namespace API.Messages
    ''' <summary>The WindowProc function is an application-defined function that processes messages sent to a window. The WNDPROC type defines a pointer to this callback function. WindowProc is a placeholder for the application-defined function name.</summary>
    ''' <param name="hWnd">Handle to the window.</param>
    ''' <param name="msg">Specifies the message.</param>
    ''' <param name="wParam">Specifies additional message information. The contents of this parameter depend on the value of the <paramref name="uMsg"/> parameter.</param>
    ''' <param name="lParam">Specifies additional message information. The contents of this parameter depend on the value of the <paramref name="uMsg"/> parameter.</param>
    ''' <returns>The return value is the result of the message processing and depends on the message sent.</returns>
    Public Delegate Function WndProc(ByVal hWnd As IntPtr, ByVal msg As WindowMessages, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    ''' <summary>These values are used with ImmGetCompositionString and <see cref="WindowMessages.WM_IME_COMPOSITION"/>.</summary>
    ''' <remarks><seealso>http://msdn2.microsoft.com/en-us/library/ms776087.aspx</seealso></remarks>
    Friend Enum IMECompositionStringValues As Integer
        ''' <summary>Retrieves or updates the attribute of the composition string.</summary>
        GCS_COMPATTR = &H10
        ''' <summary>Retrieves or updates clause information of the composition string.</summary>
        GCS_COMPCLAUSE = &H20
        ''' <summary>Retrieves or updates the attributes of the reading string of the current composition.</summary>
        GCS_COMPREADATTR = &H2
        ''' <summary> 	Retrieves or updates the clause information of the reading string of the composition string.</summary>
        GCS_COMPREADCLAUSE = &H4
        ''' <summary> 	Retrieves or updates the reading string of the current composition.</summary>
        GCS_COMPREADSTR = &H1
        ''' <summary> 	Retrieves or updates the current composition string.</summary>
        GCS_COMPSTR = &H8
        ''' <summary> 	Retrieves or updates the cursor position in composition string.</summary>
        GCS_CURSORPOS = &H80
        ''' <summary>Retrieves or updates clause information of the result string.</summary>
        GCS_DELTASTART = &H100
        ''' <summary>Retrieves or updates clause information of the result string.</summary>
        GCS_RESULTCLAUSE = &H1000
        ''' <summary> 	Retrieves or updates the reading string.</summary>
        GCS_RESULTREADCLAUSE = &H400
        ''' <summary> 	Retrieves or updates the reading string.</summary>
        GCS_RESULTREADSTR = &H200
        ''' <summary>Retrieves or updates the string of the composition result.</summary>
        GCS_RESULTSTR = &H800
    End Enum

#Region "Events"
    ''' <summary>Interface of object that provides event raised when window message processing is required</summary>
    ''' <remarks>This interface uses event with parameter passed by reference. It is is not the option for you to use such event use <see  cref="IWindowsMessagesProviderVal"/> interface.
    ''' <para>While designing class that accepts this interface consider accepting <see cref="IWindowsMessagesProviderVal"/> as well</para></remarks>
    Public Interface IWindowsMessagesProviderRef
        Inherits IWin32Window
        ''' <summary>Raised when window message processing is required</summary>
        ''' <remarks>Caller must pass <paramref name="msg"/>.<see cref="Message.Result">Result</see> as result of message (unless it does own processing of the message and passes own result).
        ''' <para>When this event is raised from <see cref="Control.WndProc"/> than just raising it is enough (unless event rais method is implemented in non-standard way), because it if passed by reference.</para>
        ''' <para>Calle may require to <paramref name="sender"/> be caller and <paramref name="e"/>.<see cref="Message.HWnd">hWnd</see> to be <see cref="Handle"/>.</para></remarks>
        Event WndProc As WndProcEventArgs
    End Interface
    ''' <summary>Generic interface of object that provides event raised when window message processing is required</summary>
    ''' <remarks>This interface uses event with parameter passed by reference. It is is not the option for you to use such event use <see  cref="IWindowsMessagesProviderVal"/> interface.
    ''' <para>While designing class that accepts this interface consider accepting <see cref="IWindowsMessagesProviderVal"/> as well</para>.
    ''' <para>In case you want to implement this interface in non-message-specific way, use <see cref="WindowMessage"/> for <typeparamref name="T"/></para></remarks>
    ''' <typeparam name="T">Concrete type of message provided by interface implementation, or <see cref="WindowMessage"/> for non-message-specific implementations</typeparam>
    Public Interface IWindowsMessagesProviderVal(Of T As WindowMessage)
        Inherits IWin32Window
        ''' <summary>Raised when window message processing is required</summary>
        ''' <remarks>Caller must pass <paramref name="e"/>.<see cref="WindowMessage.ReturnValue">ReturnValue</see> back as result of message (unless it does own processing of the message and passes own result)
        ''' <para>Calle may require to <paramref name="sender"/> be caller and <paramref name="e"/>.<see cref="WindowMessage.hWnd">hWnd</see> to be <see cref="Handle"/>.</para></remarks>
        Event WndProc As EventHandler(Of T)
    End Interface


    ''' <summary>Implements windows message as reference type</summary>
    ''' <seelaso cref="Message"/>
    ''' <version version="1.5.3">Methods for sending messages</version>
    Public Class WindowMessage
        Inherits EventArgs
        Implements IEquatable(Of WindowMessage), IEquatable(Of Message), ICloneable(Of WindowMessage)
#Region "CTors"
        ''' <summary>CTor from all parameters but result</summary>
        ''' <param name="hWnd">Target window handle</param>
        ''' <param name="lParam">lParam</param>
        ''' <param name="wParam">wParam</param>
        ''' <param name="Message">Message code</param>
        Public Sub New(ByVal hWnd As IntPtr, ByVal Message As WindowMessages, ByVal wParam%, ByVal lParam%)
            Me.New(hWnd, Message, wParam, lParam, 0)
        End Sub
        ''' <summary>CTor from <see cref="System.Windows.Forms.Message"/></summary>
        ''' <param name="Message">A <see cref="System.Windows.Forms.Message"/></param>
        Public Sub New(ByVal Message As Message)
            Me.New(Message.HWnd, Message.Msg, Message.WParam, Message.LParam, Message.Result)
        End Sub
        ''' <summary>Copy CTor - clones given instance</summary>
        ''' <param name="Message">Instance to clone</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Message"/> is null</exception>
        Public Sub New(ByVal Message As WindowMessage)
            If Message Is Nothing Then Throw New ArgumentNullException("Message")
            _Message = Message.Message
            _hWnd = Message.hWnd
            _wParam = Message.wParam
            _lParam = Message.lParam
            _ReturnValue = Message.ReturnValue
        End Sub
        ''' <summary>CTor from all the parameters</summary>
        ''' <param name="hWnd">Target window handle</param>
        ''' <param name="lParam">lParam</param>
        ''' <param name="wParam">wParam</param>
        ''' <param name="Message">Message code</param>
        ''' <param name="ReturnValue">Return code</param>
        Public Sub New(ByVal hWnd As IntPtr, ByVal Message As WindowMessages, ByVal wParam%, ByVal lParam%, ByVal ReturnValue%)
            _Message = Message
            _hWnd = hWnd
            _wParam = wParam
            _lParam = lParam
            _ReturnValue = ReturnValue
        End Sub
#End Region
#Region "Properties"
        ''' <summary>Contains value of the <see cref="Message"/> property</summary>
        Private ReadOnly _Message As WindowMessages
        ''' <summary>Gest windows message code, the calue indicating type and meaning of message and its parameters</summary>
        ''' <returns>Windows message code</returns>
        Public ReadOnly Property Message() As WindowMessages
            <DebuggerStepThrough()> Get
                Return _Message
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="wParam"/> property</summary>
        Private ReadOnly _wParam As Integer
        ''' <summary>Gets wParam</summary>
        ''' <returns>wParam</returns>
        ''' <remarks>Meaning depends on <see cref="Message"/></remarks>
        Public ReadOnly Property wParam() As Integer
            <DebuggerStepThrough()> Get
                Return _wParam
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="lParam"/> property</summary>
        Private ReadOnly _lParam As Integer
        ''' <summary>Gets lParam</summary>
        ''' <returns>lParam</returns>
        ''' <remarks>Meaning depends on <see cref="Message"/></remarks>
        Public ReadOnly Property lParam() As Integer
            <DebuggerStepThrough()> Get
                Return _lParam
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="ReturnValue"/> property</summary>
        Private _ReturnValue As Integer
        ''' <summary>Gets or sets message return value</summary>
        ''' <returns>Mesfage return value</returns>
        ''' <value>Message return value</value>
        ''' <remarks>Meaning depends on <see cref="Message"/></remarks>
        Public Property ReturnValue() As Integer
            <DebuggerStepThrough()> Get
                Return _ReturnValue
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Integer)
                _ReturnValue = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="hWnd"/> property</summary>
        Private _hWnd As IntPtr
        ''' <summary>Gets handle of target window of the message</summary>
        ''' <remarks>In Win32 terminology almost any .NET control is a window</remarks>
        Public ReadOnly Property hWnd() As IntPtr
            <DebuggerStepThrough()> Get
                Return _hWnd
            End Get
        End Property
#End Region
#Region "Operators and equals"
        ''' <summary>Converts <see cref="WindowMessage"/> to <see cref="System.Windows.Forms.Message"/></summary>
        ''' <param name="a">A <see cref="WindowMessage"/></param>
        ''' <returns>Equivalent <see cref="System.Windows.Forms.Message"/> (<see cref="Empty"/> instance when <paramref name="a"/> is null)</returns>
        Public Shared Widening Operator CType(ByVal a As WindowMessage) As Message
            If a Is Nothing Then Return Nothing
            Return New Message() With {.HWnd = a.hWnd, .Msg = a.Message, .LParam = a.lParam, .WParam = a.wParam, .Result = a.ReturnValue}
        End Operator
        ''' <summary>Converts <see cref="System.Windows.Forms.Message"/> to <see cref="WindowMessage"/></summary>
        ''' <param name="a">A <see cref="System.Windows.Forms.Message"/></param>
        ''' <returns>Equivalent <see cref="WindowMessage"/></returns>
        Public Shared Widening Operator CType(ByVal a As Message) As WindowMessage
            Return New WindowMessage(a)
        End Operator
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="WindowMessage" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="WindowMessage" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="WindowMessage" />.</param>
        ''' <remarks>Use type-safe overload instead</remarks>
        <Obsolete("Use type-safe overload instead")> _
        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If obj Is Nothing Then Return False
            If TypeOf obj Is WindowMessage Then Return Equals(DirectCast(obj, WindowMessage))
            If TypeOf obj Is Message Then Return Equals(DirectCast(obj, Message))
            Return MyBase.Equals(obj)
        End Function

        ''' <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        ''' <returns>true if the current object is equal to the 
        ''' <paramref name="other" /> parameter; otherwise, false.</returns>
        ''' <param name="other">An object to compare with this object.</param>
        ''' <remarks><see cref="WindowMessage" /> is consideret equal to <see cref="System.Windows.Forms.Message"/> when appropriate properties has same values</remarks>
        Public Overloads Function Equals(ByVal other As System.Windows.Forms.Message) As Boolean Implements System.IEquatable(Of System.Windows.Forms.Message).Equals
            Return other.HWnd = Me.hWnd AndAlso other.Msg = Me.Message AndAlso other.LParam = Me.lParam AndAlso other.WParam = Me.wParam AndAlso other.Result = Me.ReturnValue
        End Function

        ''' <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        ''' <returns>true if the current object is equal to the 
        ''' <paramref name="other" /> parameter; otherwise, false.</returns>
        ''' <param name="other">An object to compare with this object.</param>
        ''' <remarks><see cref="WindowMessage" /> is considered equal to other <see cref="WindowMessage" /> when all the properties <see cref="hWnd"/>, <see cref="Message"/>, <see cref="wParam"/>, <see cref="lParam"/> and <see cref="ReturnValue"/> has the same value.</remarks>
        Public Overloads Function Equals(ByVal other As WindowMessage) As Boolean Implements System.IEquatable(Of WindowMessage).Equals
            If other Is Nothing Then Return False
            Return other.hWnd = Me.hWnd AndAlso other.Message = Me.Message AndAlso other.lParam = Me.lParam AndAlso other.wParam = Me.wParam AndAlso other.ReturnValue = Me.ReturnValue
        End Function
        ''' <summary>Loosely compare two <see cref="WindowMessage" /> instances</summary>
        ''' <param name="a">A <see cref="WindowMessage" /></param>
        ''' <param name="b">A <see cref="WindowMessage" /></param>
        ''' <returns>True when <paramref name="a"/> and <paramref name="b"/> has same value of <see cref="Message"/>, <see cref="lParam"/> and <see cref="wParam"/> properties or both are null</returns>
        Public Shared Operator Like(ByVal a As WindowMessage, ByVal b As WindowMessage) As Boolean
            Return a Is b OrElse (a IsNot Nothing AndAlso b IsNot Nothing AndAlso a.Message = b.Message AndAlso a.lParam = b.lParam AndAlso a.wParam = b.wParam)
        End Operator
        ''' <summary>Serves as a hash function for <see cref="WindowMessage" />.</summary>
        ''' <returns>A hash code for the current <see cref="WindowMessage" />.</returns>
        ''' <remarks>Returns same value as <see cref="Message.GetHashCode"/> for equal instance</remarks>
        Public Overrides Function GetHashCode() As Integer
            Return ((CInt(Me.hWnd) << 4) Or Me.Message)
        End Function
        ''' <summary>Compares two <see cref="WindowMessage" /> objects</summary>
        ''' <param name="a">A <see cref="WindowMessage" /></param>
        ''' <param name="b">A <see cref="WindowMessage" /></param>
        ''' <returns>True when <paramref name="a"/> and <paramref name="b"/> equals or are both null</returns>
        ''' <seelaso cref="Equals"/>
        Overloads Shared Operator =(ByVal a As WindowMessage, ByVal b As WindowMessage) As Boolean
            Return a IsNot Nothing AndAlso a.Equals(b)
        End Operator
        ''' <summary>Compares two <see cref="WindowMessage" /> objects</summary>
        ''' <param name="a">A <see cref="WindowMessage" /></param>
        ''' <param name="b">A <see cref="WindowMessage" /></param>
        ''' <returns>True when <paramref name="a"/> and <paramref name="b"/> equals</returns>
        ''' <seelaso cref="Equals"/>
        Overloads Shared Operator =(ByVal a As WindowMessage, ByVal b As Message) As Boolean
            Return a IsNot Nothing AndAlso a.Equals(b)
        End Operator
        ''' <summary>Compares two <see cref="WindowMessage" /> objects</summary>
        ''' <param name="a">A <see cref="WindowMessage" /></param>
        ''' <param name="b">A <see cref="WindowMessage" /></param>
        ''' <returns>False when <paramref name="a"/> and <paramref name="b"/> equals or are both null</returns>
        ''' <seelaso cref="Equals"/>
        Overloads Shared Operator <>(ByVal a As WindowMessage, ByVal b As WindowMessage) As Boolean
            Return Not a = b
        End Operator
        ''' <summary>Compares two <see cref="WindowMessage" /> objects</summary>
        ''' <param name="a">A <see cref="WindowMessage" /></param>
        ''' <param name="b">A <see cref="WindowMessage" /></param>
        ''' <returns>False when <paramref name="a"/> and <paramref name="b"/> equals</returns>
        ''' <seelaso cref="Equals"/>
        Overloads Shared Operator <>(ByVal a As WindowMessage, ByVal b As Message) As Boolean
            Return a = b
        End Operator

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        ''' <remarks>Use type-safe <see cref="Clone"/> instead</remarks>
        <Obsolete("Use type-safe Clone instead")> _
        Private Function ICloneable_Clone() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Function Clone() As WindowMessage Implements ICloneable(Of WindowMessage).Clone
            Return New WindowMessage(Me)
        End Function
#End Region
        ''' <summary>Gets an empty instance of the <see cref="WindowMessage" /> class</summary>
        Shared Shadows ReadOnly Property Empty() As WindowMessage
            Get
                Return New WindowMessage(0, 0, 0, 0)
            End Get
        End Property
#Region "High - Low"
        ''' <summary>Gets low-order word of <see cref="lParam"/></summary>
        ''' <returns>Low-orded word of <see cref="lParam"/></returns>
        ''' <seelaso cref="ExtensionsT.Low"/><seelaso cref="lParam"/>
        <CLSCompliant(False)> _
        Public ReadOnly Property lParamLow() As UShort
            Get
                Return lParam.Low
            End Get
        End Property
        ''' <summary>Gets high-order word of <see cref="lParam"/></summary>
        ''' <returns>High-orded word of <see cref="lParam"/></returns>
        ''' <seelaso cref="ExtensionsT.High"/><seelaso cref="lParam"/>
        <CLSCompliant(False)> _
        Public ReadOnly Property lParamHigh() As UShort
            Get
                Return lParam.High
            End Get
        End Property
        ''' <summary>Gets low-order word of <see cref="wParam"/></summary>
        ''' <returns>Low-orded word of <see cref="wParam"/></returns>
        ''' <seelaso cref="ExtensionsT.Low"/><seelaso cref="wParam"/>
        <CLSCompliant(False)> _
        Public ReadOnly Property wParamLow() As UShort
            Get
                Return wParam.Low
            End Get
        End Property
        ''' <summary>Gets high-order word of <see cref="wParam"/></summary>
        ''' <returns>High-orded word of <see cref="wParam"/></returns>
        ''' <seelaso cref="ExtensionsT.High"/><seelaso cref="wParam"/>
        <CLSCompliant(False)> _
        Public ReadOnly Property wParamHigh() As UShort
            Get
                Return wParam.High
            End Get
        End Property
#End Region

#Region "Sending"
        ''' <summary>Broadcasts message to all top-level windows</summary>
        ''' <returns>Result of message</returns>
        ''' <remarks>This method sets <see cref="ReturnValue"/> to result of message</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function Broadcast() As Integer
            Return Me.Send(GUI.SpecialWindowHandles.HWND_BROADCAST)
        End Function
#Region "Send"
        ''' <summary>Sends the message to specified window identified by handle</summary>
        ''' <param name="hWnd">Hande of window to send message to</param>
        ''' <returns>Result of message</returns>
        ''' <remarks>This method sets <see cref="ReturnValue"/> to result of message</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function Send(ByVal hWnd As IntPtr) As Integer
            Me.ReturnValue = Messages.SendMessage(hWnd, Me.Message, Me.wParam, Me.lParam)
            Return ReturnValue
        End Function
        ''' <summary>Sends the message to specified window</summary>
        ''' <param name="window">A window to send message to</param>
        ''' <returns>Result of message</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="window"/> is null</exception>
        ''' <remarks>This method sets <see cref="ReturnValue"/> to result of message</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function Send(ByVal window As IWin32Window) As Integer
            If window Is Nothing Then Throw New ArgumentNullException("window")
            Return Send(window.Handle)
        End Function
        ''' <summary>Sends a message to given window</summary>
        ''' <param name="window">A window to send message to</param>
        ''' <param name="message">A message to be sent</param>
        ''' <param name="wParam">Message wParam parameter</param>
        ''' <param name="lParam">Message lParam parameter</param>
        ''' <returns>Message call result</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="window"/> is null</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Shared Function Send(ByVal window As IWin32Window, ByVal message As WindowMessages, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
            If window Is Nothing Then Throw New ArgumentNullException("window")
            Dim msg As New WindowMessage(window.Handle, message, wParam, lParam)
            Return msg.Send()
        End Function
        ''' <summary>Sends a message to given window</summary>
        ''' <param name="hWnd">Hande of window to send message to</param>
        ''' <param name="message">A message to be sent</param>
        ''' <param name="wParam">Message wParam parameter</param>
        ''' <param name="lParam">Message lParam parameter</param>
        ''' <returns>Message call result</returns>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Shared Function Send(ByVal hWnd As IntPtr, ByVal message As WindowMessages, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
            Dim msg As New WindowMessage(hWnd, message, wParam, lParam)
            Return msg.Send()
        End Function
        ''' <summary>Sends the message to window specified in the <see cref="hWnd"/> property</summary>
        ''' <returns>Result of message</returns>
        ''' <remarks>This method sets <see cref="ReturnValue"/> to result of message</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function Send() As Integer
            Return Me.Send(Me.hWnd)
        End Function
#End Region
#Region "Post"
        ''' <summary>Posts the message to specified window identified by handle without waiting for window to process the message</summary>
        ''' <param name="hWnd">Hande of window to Post message to</param>
        ''' <exception cref="API.Win32APIException">Failed to post the message</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Sub Post(ByVal hWnd As IntPtr)
            If Not Messages.PostMessage(hWnd, Me.Message, Me.wParam, Me.lParam) Then Throw New API.Win32APIException
        End Sub
        ''' <summary>Posts the message to specified window without waiting for window to process the message</summary>
        ''' <param name="window">A window to Post message to</param>
        ''' <exception cref="API.Win32APIException">Failed to post the message</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="window"/> is null</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Sub Post(ByVal window As IWin32Window)
            If window Is Nothing Then Throw New ArgumentNullException("window")
            Post(window.Handle)
        End Sub
        ''' <summary>Posts a message to given window without waiting for window to process the message</summary>
        ''' <param name="window">A window to Post message to</param>
        ''' <param name="message">A message to be sent</param>
        ''' <param name="wParam">Message wParam parameter</param>
        ''' <param name="lParam">Message lParam parameter</param>
        ''' <exception cref="API.Win32APIException">Failed to post the message</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="window"/> is null</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Shared Sub Post(ByVal window As IWin32Window, ByVal message As WindowMessages, ByVal wParam As Integer, ByVal lParam As Integer)
            If window Is Nothing Then Throw New ArgumentNullException("window")
            Dim msg As New WindowMessage(window.Handle, message, wParam, lParam)
            msg.Post()
        End Sub
        ''' <summary>Posts a message to given window without waiting for window to process the message</summary>
        ''' <param name="hWnd">Hande of window to Post message to</param>
        ''' <param name="message">A message to be sent</param>
        ''' <param name="wParam">Message wParam parameter</param>
        ''' <param name="lParam">Message lParam parameter</param>
        ''' <exception cref="API.Win32APIException">Failed to post the message</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Shared Sub Post(ByVal hWnd As IntPtr, ByVal message As WindowMessages, ByVal wParam As Integer, ByVal lParam As Integer)
            Dim msg As New WindowMessage(hWnd, message, wParam, lParam)
            msg.Post()
        End Sub
        ''' <summary>Posts the message to window specified in the <see cref="hWnd"/> property without waiting for window to process the message</summary>
        ''' <exception cref="API.Win32APIException">Failed to post the message</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Sub Post()
            Me.Post(Me.hWnd)
        End Sub
#End Region
#End Region
    End Class

    ''' <summary>Delegate of procedure that serves as event handler for <see cref="IWindowsMessagesProviderRef.WndProc"/></summary>
    ''' <param name="sender">Source of the event</param>
    ''' <param name="msg">Windows message passes by reference so, calle can change it</param>
    Public Delegate Sub WndProcEventArgs(ByVal sender As Object, ByRef msg As Message)
    ''' <summary>Delegate of procedure tah serves as event handler for conrete message provider</summary>
    ''' <param name="sender">Source of the event</param>
    ''' <param name="Message">Windows mesage</param>
    ''' <typeparam name="T">Type of message</typeparam>
    Public Delegate Sub WndProcEventArgs(Of T As WindowMessage)(ByVal sender As Object, ByVal Message As T)
#End Region

    ''' <summary>Defines API functions to worki with windows messages</summary>
    ''' <version version="1.5.3">This module is new in version 1.5.3</version>
    Friend Module Messages
        ''' <summary>Sends the specified message to a window or System.Windows. The <see cref="SendMessage"/>  function calls the window procedure for the specified window and does not return until the window procedure has processed the message.</summary>
        ''' <param name="hWnd">A handle to the window whose window procedure will receive the message. If this parameter is <see cref="GUI.SpecialWindowHandles.HWND_BROADCAST"/>, the message is sent to all top-level windows in the system, including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent to child System.Windows.</param>
        ''' <param name="msg">The message to be sent.</param>
        ''' <param name="wParam">Additional message-specific information.</param>
        ''' <param name="lParam">Additional message-specific information.</param>
        ''' <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        Public Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal msg As WindowMessages, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
        ''' <summary>Sends the specified message to a window or System.Windows. The <see cref="SendMessage"/>  function calls the window procedure for the specified window and does not return until the window procedure has processed the message. (<paramref name="lParam"/> as pointer to string)</summary>
        ''' <param name="hWnd">A handle to the window whose window procedure will receive the message. If this parameter is <see cref="GUI.SpecialWindowHandles.HWND_BROADCAST"/>, the message is sent to all top-level windows in the system, including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent to child System.Windows.</param>
        ''' <param name="msg">The message to be sent.</param>
        ''' <param name="wParam">Additional message-specific information.</param>
        ''' <param name="lParam">Additional message-specific information.</param>
        ''' <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        <DllImport("user32.dll", CharSet:=CharSet.Auto)>
        Public Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As WindowMessages, ByVal wParam As Integer, ByVal lParam As System.Text.StringBuilder) As Integer
        End Function
        ''' <summary>Places (posts) a message in the message queue associated with the thread that created the specified window and returns without waiting for the thread to process the message.</summary>
        ''' <param name="hWnd">A handle to the window whose window procedure will receive the message. If this parameter is <see cref="GUI.SpecialWindowHandles.HWND_BROADCAST"/>, the message is sent to all top-level windows in the system, including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent to child System.Windows.</param>
        ''' <param name="msg">The message to be sent.</param>
        ''' <param name="wParam">Additional message-specific information.</param>
        ''' <param name="lParam">Additional message-specific information.</param>
        ''' <remarks>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</remarks>
        Public Declare Auto Function PostMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal msg As WindowMessages, ByVal wParam As Integer, ByVal lParam As Integer) As Boolean
    End Module
End Namespace
#End If