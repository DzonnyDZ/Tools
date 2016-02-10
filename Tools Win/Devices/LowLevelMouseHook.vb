Imports System.Runtime.InteropServices, System.Threading.Thread
Imports System.ComponentModel
Imports System.Threading
Imports Tools.ComponentModelT

#If True
Namespace DevicesT

    ''' <summary>Allows handling system-wide low-level mouse hooks</summary>
    ''' <remarks>It is highly recomended not to install multiple hooks of same type in one application and to keep hook-handling code as quick as possible. You can significantly slow down user typing experience.
    ''' <para>This class uses Win32 API function SetWindowsHookEx(WH_MOUSE_LL).</para></remarks>
    ''' <seealso cref="LowLevelKeyboardHook"/>
    <DefaultEvent("ButtonEvent")> _
    Public Class LowLevelMouseHook
        Inherits API.Hooks.Win32Hook
#Region "CTors"
        ''' <summary>Default CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor with possibility of immediate hook activation</summary>
        ''' <param name="RegisterImmediatelly">True to register hook immediatelly</param>
        ''' <exception cref="API.Win32APIException">An error ocured while obtaining the hook.</exception>
        Public Sub New(ByVal RegisterImmediatelly As Boolean)
            If RegisterImmediatelly Then Me.RegisterHook()
        End Sub
#End Region
#Region "Processing"
        ''' <summary><para>The LowLevelMouseProc hook procedure is an application-defined or library-defined callback function used with the SetWindowsHookEx function. The system call this function every time a new mouse input event is about to be posted into a thread input queue. The mouse input can come from the local mouse driver or from calls to the mouse_event function. If the input comes from a call to mouse_event, the input was "injected". However, the WH_MOUSE_LL hook is not injected into another process. Instead, the context switches back to the process that installed the hook and it is called in its original context. Then the context switches back to the application that generated the event.</para>
        ''' <para>The HOOKPROC type defines a pointer to this callback function. LowLevelMouseProc is a placeholder for the application-defined or library-defined function name.</para></summary>
        ''' <param name="nCode">Specifies a code the hook procedure uses to determine how to process the message. If nCode is less than zero, the hook procedure must pass the message to the <see cref="CallNextHook"/> function without further processing and should return the value returned by <see cref="CallNextHook"/>.</param>
        ''' <param name="wParam">Specifies the identifier of the mouse message. This parameter can be one of the following messages:
        ''' <see cref="api.Messages.WindowMessages.WM_LBUTTONDOWN"/>, <see cref="api.Messages.WindowMessages.WM_LBUTTONUP"/>, <see cref="api.Messages.WindowMessages.WM_MOUSEMOVE"/>, <see cref="api.Messages.WindowMessages.WM_MOUSEWHEEL"/>, <see cref="api.Messages.WindowMessages.WM_MOUSEHWHEEL"/>, <see cref="api.Messages.WindowMessages.WM_RBUTTONDOWN"/>, or <see cref="api.Messages.WindowMessages.WM_RBUTTONUP"/>.</param>
        ''' <param name="lParam">[in] Pointer to an MSLLHOOKSTRUCT structure. </param>
        ''' <returns><para>If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx.</para>
        ''' <para>If nCode is greater than or equal to zero, and the hook procedure did not process the message, it is highly recommended that you call CallNextHookEx and return the value it returns; otherwise, other applications that have installed WH_MOUSE_LL hooks will not receive hook notifications and may behave incorrectly as a result. If the hook procedure processed the message, it may return a nonzero value to prevent the system from passing the message to the rest of the hook chain or the target window procedure.</para></returns>
        Protected Overrides Function HookProc(ByVal nCode As Integer, ByVal wParam As System.IntPtr, ByVal lParam As System.IntPtr) As System.IntPtr
            Dim Parameters As API.Hooks.MSLLHOOKSTRUCT = Marshal.PtrToStructure(lParam, GetType(API.Hooks.MSLLHOOKSTRUCT))
            Dim e As LowLevelMouseEventArgs
            Select Case CType(wParam, API.Messages.WindowMessages)
                Case API.Messages.WindowMessages.WM_LBUTTONDOWN
                    e = New LowLevelMouseButtonEventArgs(Parameters, False, MouseButtons.Left)
                Case API.Messages.WindowMessages.WM_LBUTTONUP
                    e = New LowLevelMouseButtonEventArgs(Parameters, True, MouseButtons.Left)
                Case API.Messages.WindowMessages.WM_MOUSEMOVE
                    e = New LowLevelMouseEventArgs(Parameters)
                Case API.Messages.WindowMessages.WM_MOUSEWHEEL
                    e = New LowLevelMouseWheelEventArgs(Parameters, False)
                Case API.Messages.WindowMessages.WM_MOUSEHWHEEL
                    e = New LowLevelMouseWheelEventArgs(Parameters, True)
                Case API.Messages.WindowMessages.WM_RBUTTONDOWN
                    e = New LowLevelMouseButtonEventArgs(Parameters, False, MouseButtons.Right)
                Case API.Messages.WindowMessages.WM_RBUTTONUP
                    e = New LowLevelMouseButtonEventArgs(Parameters, True, MouseButtons.Right)
                Case API.Messages.WindowMessages.WM_MBUTTONDOWN
                    e = New LowLevelMouseButtonEventArgs(Parameters, False, MouseButtons.Middle)
                Case API.Messages.WindowMessages.WM_MBUTTONUP
                    e = New LowLevelMouseButtonEventArgs(Parameters, True, MouseButtons.Middle)
                Case API.Messages.WindowMessages.WM_XBUTTONDOWN
                    Dim xButton As MouseButtons
                    Select Case Parameters.mouseData_high
                        Case API.Messages.wParam.WM_PARENTNOTIFY_WM_XBUTTONDOWN_high.XBUTTON1 : xButton = MouseButtons.XButton1
                        Case API.Messages.wParam.WM_PARENTNOTIFY_WM_XBUTTONDOWN_high.XBUTTON2 : xButton = MouseButtons.XButton2
                        Case Else : xButton = Parameters.mouseData_high 'Should not ocur
                    End Select
                    e = New LowLevelMouseButtonEventArgs(Parameters, False, xButton)
                Case API.Messages.WindowMessages.WM_XBUTTONUP
                    Dim xButton As MouseButtons
                    Select Case Parameters.mouseData_high
                        Case API.Messages.wParam.WM_PARENTNOTIFY_WM_XBUTTONDOWN_high.XBUTTON1 : xButton = MouseButtons.XButton1
                        Case API.Messages.wParam.WM_PARENTNOTIFY_WM_XBUTTONDOWN_high.XBUTTON2 : xButton = MouseButtons.XButton2
                        Case Else : xButton = Parameters.mouseData_high 'Should not ocur
                    End Select
                    e = New LowLevelMouseButtonEventArgs(Parameters, True, xButton)
                Case Else
                    Return CallNextHook(nCode, wParam, lParam)
            End Select
            Select Case CType(wParam, API.Messages.WindowMessages)
                Case API.Messages.WindowMessages.WM_LBUTTONDOWN, API.Messages.WindowMessages.WM_LBUTTONUP, API.Messages.WindowMessages.WM_RBUTTONDOWN, _
                    API.Messages.WindowMessages.WM_RBUTTONUP, API.Messages.WindowMessages.WM_MBUTTONDOWN, API.Messages.WindowMessages.WM_MBUTTONUP, _
                    API.Messages.WindowMessages.WM_XBUTTONDOWN, API.Messages.WindowMessages.WM_XBUTTONUP
                    OnButtonEvent(e)
                Case API.Messages.WindowMessages.WM_MOUSEMOVE
                    OnMouseMove(e)
                Case API.Messages.WindowMessages.WM_MOUSEWHEEL, API.Messages.WindowMessages.WM_MOUSEHWHEEL
                    OnWheel(e)
            End Select
            If Not e.Handled Then
                Return CallNextHook(nCode, wParam, lParam)
            ElseIf e.Suppress Then
                Return 1
            Else
                Return 0
            End If
        End Function
        ''' <summary>Called when mouse button status changes. Raises the <see cref="ButtonEvent"/> event.</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: In order to the <see cref="ButtonEvent"/> event to be raised, call base class method.</remarks>
        Protected Overridable Sub OnButtonEvent(ByVal e As LowLevelMouseButtonEventArgs)
            RaiseEvent ButtonEvent(Me, e)
        End Sub
        ''' <summary>Called when mouse wheel rotates. Raises the <see cref="Wheel"/> event.</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: In order to the <see cref="Wheel"/> event to be raised, call base class method.
        ''' <para>Windows Vista and latter: Handles also horizontal wheel. See <see cref="LowLevelMouseWheelEventArgs.Horizontal"/>.</para></remarks>
        Protected Overridable Sub OnWheel(ByVal e As LowLevelMouseWheelEventArgs)
            RaiseEvent Wheel(Me, e)
        End Sub
        ''' <summary>Called when mouse moves. Raises the <see cref="MouseMove"/> event.</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: In order to the <see cref="MouseMove"/> event to be raised, call base class method.</remarks>
        Protected Overridable Sub OnMouseMove(ByVal e As LowLevelMouseEventArgs)
            RaiseEvent MouseMove(Me, e)
        End Sub
        ''' <summary>Raised when status of any mouse button changes</summary>
        ''' <remarks>Handles Left, Middle, Right, X1 and X2 buttons</remarks>
        Public Event ButtonEvent As EventHandler(Of LowLevelMouseHook, LowLevelMouseButtonEventArgs)
        ''' <summary>Raised when mouse wheel rotates.</summary>
        ''' <remarks>Windows Vista and later: Handles also horizontal wheel. See <see cref="LowLevelMouseWheelEventArgs.Horizontal"/>.</remarks>
        Public Event Wheel As EventHandler(Of LowLevelMouseHook, LowLevelMouseWheelEventArgs)
        ''' <summary>Raised when mouse moves</summary>
        Public Event MouseMove As EventHandler(Of LowLevelMouseHook, LowLevelMouseEventArgs)
#End Region
        ''' <summary>If overridden in derived class gets type of hook represented by derived class</summary>
        ''' <returns>One of values accepted for SetWindowsHookEx idHook parameter</returns>
        Protected Overrides ReadOnly Property HandledHookType() As API.Hooks.Win32Hook.HookType
            Get
                Return HookType.LowLevelMouse
            End Get
        End Property
        ''' <summary>Gets module handle pased to hMod parameter of SetWindowsHookEx Win32 API function</summary>
        ''' <returns>This implementation uses <see cref="GetModuleHandleFromType"/></returns>
        ''' <seelaso cref="GetModuleHandleFromType"/>
        Protected Overrides Function GetModuleHandle() As System.IntPtr
            Return GetModuleHandleFromType(GetType(LowLevelMouseHook))
        End Function
    End Class
    ''' <summary>Base class for low-level mouse event arguments</summary>
    ''' <remarks>This class is not intended to be derived</remarks>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Class LowLevelMouseEventArgs : Inherits SuppresHandledEventArgs
        ''' <summary>CTor</summary>
        ''' <param name="Parameters">Parameters of low-level hook call</param>
        Friend Sub New(ByVal Parameters As API.Hooks.MSLLHOOKSTRUCT)
            Me.Parameters = Parameters
        End Sub
        ''' <summary>Event parameters</summary>
        Friend ReadOnly Parameters As API.Hooks.MSLLHOOKSTRUCT
        ''' <summary>Gets current position of mouse cursor in screen coordinates</summary>
        Public ReadOnly Property Location() As Point
            Get
                Return Parameters.pt
            End Get
        End Property
        ''' <summary>Gets value indicating if this message was injected</summary>
        Public ReadOnly Property IsInjected() As Boolean
            Get
                Return Parameters.flags And API.Hooks.MSLLHOOKSTRUCTFlags.LLMHF_INJECTED
            End Get
        End Property
    End Class
    ''' <summary>Low-level mouse event arguments related to button up and down events</summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public NotInheritable Class LowLevelMouseButtonEventArgs
        Inherits LowLevelMouseEventArgs
        ''' <summary>CTor</summary>
        ''' <param name="Parameters">Event parameters</param>
        ''' <param name="MouseUp">True when event was releasing of the button</param>
        ''' <param name="Button">Identifies the button</param>
        Friend Sub New(ByVal Parameters As API.Hooks.MSLLHOOKSTRUCT, ByVal MouseUp As Boolean, ByVal Button As MouseButtons)
            MyBase.New(Parameters)
            _MouseUp = MouseUp
            _Button = Button
        End Sub
        ''' <summary>Contains value of the <see cref="MouseUp"/> property</summary>
        Private ReadOnly _MouseUp As Boolean
        ''' <summary>Gest value indicating if event was generated when mouse button was released</summary>
        ''' <returns>True if event was generated for mouse-up situation; false for mouse-down situation</returns>
        Public ReadOnly Property MouseUp() As Boolean
            Get
                Return _MouseUp
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="Button"/> property</summary>
        Private ReadOnly _Button As MouseButtons
        ''' <summary>Gets value indicating for which of mouse buttons the event was generated</summary>
        Public ReadOnly Property Button() As MouseButtons
            Get
                Return _Button
            End Get
        End Property
    End Class
    ''' <summary>Low level mouse event arguments related to wheel events</summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public NotInheritable Class LowLevelMouseWheelEventArgs
        Inherits LowLevelMouseEventArgs
        ''' <summary>CTor</summary>
        ''' <param name="Parameters">Event parameters</param>
        ''' <param name="Horizontal">True for horizontal wheel (Windows Vista only)</param>
        Friend Sub New(ByVal Parameters As API.Hooks.MSLLHOOKSTRUCT, ByVal Horizontal As Boolean)
            MyBase.New(Parameters)
        End Sub
        ''' <summary>Contains value of the <see cref="Horizontal"/> property</summary>
        Private _Horizontal As Boolean
        ''' <summary>Gets value indicating if this event was generated for mouse horizontal wheel</summary>
        ''' <returns>True when event was generated for mous horizontal wheel; false for "normal" vertical wheel</returns>
        ''' <remarks>This property can be true only at Windows Vista and later</remarks>
        Public ReadOnly Property Horizontal() As Boolean
            Get
                Return _Horizontal
            End Get
        End Property
        ''' <summary>Gets mouse wheel delta</summary>
        ''' <returns>Value of wheel delta. A positive value indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the user.</returns>
        ''' <remarks>One wheel click is defined as  <see cref="Mouse.WheelStep"/>, which is 120.</remarks>
        Public ReadOnly Property Delta() As Short
            Get
                Return Parameters.mouseData_high
            End Get
        End Property
    End Class
End Namespace
#End If
