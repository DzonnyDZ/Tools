Imports System.Runtime.InteropServices, System.Threading.Thread
Imports System.ComponentModel
Imports System.Threading
Imports Tools.ComponentModelT

#If True
Namespace DevicesT

    ''' <summary>Allows handling system-wide low-level keyboard hooks</summary>
    ''' <remarks>It is highly recomended not to install multiple hooks of same type in one application and to keep hook-handling code as quick as possible. You can significantly slow down user typing experience.
    ''' <para>This class uses Win32 API function SetWindowsHookEx(WH_KEYBOARD_LL).</para></remarks>
    ''' <seealso cref="LowLevelMouseHook"/>
    <DefaultEvent("KeyEvent")> _
    Public Class LowLevelKeyboardHook
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
        ''' <summary>Called when low-level keyboard hok is processed, raises the <see cref="KeyEvent"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note to inheritors: Base class method must be called in order the <see cref="KeyEvent"/> event to be raised.
        ''' <para>Keep event-handling code as shor and as quick as possible or you can significantly involve user experience while typing. System may also chose to skip your hook when it takes too long to be procesed.</para></remarks>
        Protected Overridable Sub OnKeyEvent(ByVal e As LowLevelKeyEventArgs)
            RaiseEvent KeyEvent(Me, e)
        End Sub
        ''' <summary>Raised when class processes low-level keyboard hook callback</summary>
        ''' <remarks>Keep event-handling code as shor and as quick as possible or you can significantly involve user experience while typing. System may also chose to skip your hook when it takes too long to be procesed.</remarks>
        Public Event KeyEvent As EventHandler(Of LowLevelKeyboardHook, LowLevelKeyEventArgs)
        ''' <summary>Application-defined or library-defined callback function used with the SetWindowsHookEx function. The system calls this function every time a new keyboard input event is about to be posted into a thread input queue. The keyboard input can come from the local keyboard driver or from calls to the keybd_event function. If the input comes from a call to keybd_event, the input was "injected". However, the WH_KEYBOARD_LL hook is not injected into another process. Instead, the context switches back to the process that installed the hook and it is called in its original context. Then the context switches back to the application that generated the event.</summary>
        ''' <param name="nCode">[in] Specifies a code the hook procedure uses to determine how to process the message. If nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx.</param>
        ''' <param name="wParam">[in] Specifies the identifier of the keyboard message. This parameter can be one of the following messages:
        ''' WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP. </param>
        ''' <param name="lParam">[in] Pointer to a KBDLLHOOKSTRUCT structure. </param>
        ''' <returns>If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx.
        ''' <para>If nCode is greater than or equal to zero, and the hook procedure did not process the message, it is highly recommended that you call CallNextHookEx and return the value it returns; otherwise, other applications that have installed WH_KEYBOARD_LL hooks will not receive hook notifications and may behave incorrectly as a result.
        ''' If the hook procedure processed the message, it may return a nonzero value to prevent the system from passing the message to the rest of the hook chain or the target window procedure. </para></returns>
        ''' <remarks><para>An application installs the hook procedure by specifying the WH_KEYBOARD_LL hook type and a pointer to the hook procedure in a call to the SetWindowsHookEx function.</para>
        ''' <para>This hook is called in the context of the thread that installed it. The call is made by sending a message to the thread that installed the hook. Therefore, the thread that installed the hook must have a message loop.</para>
        ''' <para>The hook procedure should process a message in less time than the data entry specified in the LowLevelHooksTimeout value in the following registry key:</para>
        ''' <para>HKEY_CURRENT_USER\Control Panel\Desktop</para>
        ''' <para>The value is in milliseconds. If the hook procedure does not return during this interval, the system will pass the message to the next hook.</para>
        ''' <para>Note that debug hooks cannot track this type of hook.</para></remarks>
        Protected Overrides Function HookProc(ByVal nCode As Integer, ByVal wParam As System.IntPtr, ByVal lParam As System.IntPtr) As System.IntPtr
            'If nCode < 0 Then Return API.Hooks.CallNextHookEx(Me.HookHandle, nCode, wParam, lParam) (handled in base class)
            Dim Parameters As API.Hooks.KBDLLHOOKSTRUCT = Marshal.PtrToStructure(lParam, GetType(API.Hooks.KBDLLHOOKSTRUCT))
            Dim e As New LowLevelKeyEventArgs(wParam, Parameters)
            OnKeyEvent(e)
            If Not e.Handled Then
                Return CallNextHook(nCode, wParam, lParam)
            ElseIf e.Suppress Then
                Return 1
            Else
                Return 0
            End If
        End Function
#End Region
        ''' <summary>If overridden in derived class gets type of hook represented by derived class</summary>
        ''' <returns>One of values accepted for SetWindowsHookEx idHook parameter</returns>
        Protected Overrides ReadOnly Property HandledHookType() As API.Hooks.Win32Hook.HookType
            Get
                Return HookType.LowLevelKeyboard
            End Get
        End Property
        ''' <summary>Gets module handle pased to hMod parameter of SetWindowsHookEx Win32 API function</summary>
        ''' <returns>This implementation uses <see cref="GetModuleHandleFromType"/></returns>
        ''' <seelaso cref="GetModuleHandleFromType"/>
        Protected Overrides Function GetModuleHandle() As System.IntPtr
            Return GetModuleHandleFromType(GetType(LowLevelKeyboardHook))
        End Function
    End Class
    ''' <summary>Event arguments for low-level keyboard hook</summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public NotInheritable Class LowLevelKeyEventArgs : Inherits SuppresHandledEventArgs
        ''' <summary>CTor from keyboard action and parameters</summary>
        ''' <param name="Action">Action taken</param>
        ''' <param name="Parameters">Action parameters</param>
        Friend Sub New(ByVal Action As LowLevelKeyboardEvent, ByVal Parameters As API.Hooks.KBDLLHOOKSTRUCT)
            _Action = Action
            Me.Parameters = Parameters
        End Sub
        ''' <summary>Contains value of the <see cref="Action"/> property</summary>
        Private ReadOnly _Action As LowLevelKeyboardEvent
        ''' <summary>Action parameters</summary>
        Private ReadOnly Parameters As API.Hooks.KBDLLHOOKSTRUCT
        ''' <summary>Indicates action which occured on keyboard</summary>
        Public ReadOnly Property Action() As LowLevelKeyboardEvent
            Get
                Return _Action
            End Get
        End Property
        ''' <summary>Gets key code of key that was pressed.</summary>
        ''' <remarks>Value of this property never contains combining keys <see cref="Keys.Control"/>, <see cref="Keys.Shift"/> and <see cref="Keys.Alt"/>.
        ''' Low-level keyboar messages supports onyl ALT combining key. Its state can be obtained via <see cref="AltState"/></remarks>
        Public ReadOnly Property Key() As Keys
            Get
                Return Parameters.vkCode
            End Get
        End Property
        ''' <summary>Gets hardware scan code of key being pressed or released</summary>
        Public ReadOnly Property ScanCode%()
            Get
                Return Parameters.scanCode
            End Get
        End Property
        ''' <summary>Indicates if the ALT key was pressed</summary>
        Public ReadOnly Property AltState() As Boolean
            Get
                Return Parameters.flags And API.Hooks.KBDLLHOOKSTRUCTFlags.LLKHF_ALTDOWN
            End Get
        End Property
        ''' <summary>Indicates if <see cref="Key"/> is extended key (such as numpad key or F key)</summary>
        Public ReadOnly Property IsExtended() As Boolean
            Get
                Return Parameters.flags And API.Hooks.KBDLLHOOKSTRUCTFlags.LLKHF_EXTENDED
            End Get
        End Property
        ''' <summary>Indicates if kay was injected</summary>
        Public ReadOnly Property IsInjected() As Boolean
            Get
                Return Parameters.flags And API.Hooks.KBDLLHOOKSTRUCTFlags.LLKHF_INJECTED
            End Get
        End Property
    End Class
    ''' <summary>Keyboar event that may occure with <see cref="LowLevelKeyboardHook"/></summary>
    Public Enum LowLevelKeyboardEvent
        ''' <summary>Speifies KeyUp event (<see cref="API.Messages.WindowMessages.WM_KEYUP"/>)</summary>
        KeyUp = API.Messages.WindowMessages.WM_KEYUP
        ''' <summary>Speifies KeyDown event (<see cref="API.Messages.WindowMessages.WM_KEYDOWN"/>)</summary>
        KeyDown = API.Messages.WindowMessages.WM_KEYDOWN
        ''' <summary>Speifies SysKeyUp event (<see cref="API.Messages.WindowMessages.WM_SYSKEYUP"/>)</summary>
        SysKeyDown = API.Messages.WindowMessages.WM_SYSKEYUP
        ''' <summary>Speifies SysKeyDown event (<see cref="API.Messages.WindowMessages.WM_SYSKEYDOWN"/>)</summary>
        SysKeyUp = API.Messages.WindowMessages.WM_SYSKEYDOWN
    End Enum
End Namespace
#End If
