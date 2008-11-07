Imports System.Runtime.InteropServices, System.Threading.Thread
Imports System.ComponentModel
Imports System.Threading


#If Config <= Nightly Then 'Stage:Nightly
Namespace DevicesT
  
    ''' <summary>Allows handling system-wide low-level keyboard hooks</summary>
    ''' <remarks>It is highly recomended not to install multiple hooks of same type in one application and to keep hook-handling code as quick as possible. You can significantly slow down user typing experience.
    ''' <para>This class uses Win32 API function SetWindowsHookEx(WH_KEYBOARD_LL).</para></remarks>
    <DefaultBindingProperty("KeyEvent")> _
    Public Class LowLevelKeyboardHook
        Implements IDisposable
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
        ''' <summary>Next hook to be called after current hook is handled</summary>
        Private HookHandle As IntPtr = IntPtr.Zero

#Region "Registration and unregistration"
#Region "General"
        ''' <summary>Gets value indicating if hook is registered for this instance</summary>
        ''' <returns>True if this instance have successfully registered low-level keyboard hok using <see cref="RegisterHook"/></returns>
        Public ReadOnly Property Registered() As Boolean
            Get
                Return HookHandle <> IntPtr.Zero
            End Get
        End Property
        ''' <summary>Registers a low-level keyboard hook, so keyboard events are obtained by this class.</summary>
        ''' <exception cref="InvalidOperationException">Hook was already registered for this instance (<see cref="Registered"/> is true)</exception>
        ''' <exception cref="API.Win32APIException">An error ocured while obtaining the hook.</exception>
        ''' <exception cref="ObjectDisposedException"><see cref="IsDisposed"/> is true</exception>
        Public Sub RegisterHook()
            RegisterHookInternal(False)
        End Sub
        ''' <summary>Registers a low-level keyboard hook, so keyboard events are obtained by this class.</summary>
        ''' <exception cref="InvalidOperationException">Hook was already registered for this instance (<see cref="Registered"/> is true)</exception>
        ''' <exception cref="API.Win32APIException">An error ocured while obtaining the hook.</exception>
        ''' <exception cref="ObjectDisposedException"><see cref="IsDisposed"/> is true</exception>
        ''' <param name="IsAsync">True when hook is registered from another thread</param>
        Private Sub RegisterHookInternal(ByVal IsAsync As Boolean)
            If IsDisposed Then Throw New ObjectDisposedException(Me.GetType.Name)
            If Me.HookHandle <> IntPtr.Zero Then Throw New InvalidOperationException(ResourcesT.ExceptionsWin.HookIsAlreadyRegisteredForThisInstance)
            Dim NextHook = API.SetWindowsHookEx(API.WindowsHook.KEYBOARD_LL, dLowLevelKeyboardProc, 0, 0) ' API.GetModuleHandle(GetType(LowLevelKeyboardHook).Module.FullyQualifiedName), 0)
            If NextHook = IntPtr.Zero Then Throw New API.Win32APIException
            Me.HookHandle = NextHook
            _IsAsync = IsAsync
            'If Not IsAsync Then HookThread = Nothing : HookCallback = Nothing : dUnregisterHookInternalAsync = Nothing
        End Sub
        ''' <summary>Unregisters registered hook for this instance</summary>
        ''' <exception cref="InvalidOperationException">Hook is not registered for this instance (<see cref="Registered"/> is false).</exception>
        ''' <exception cref="API.Win32APIException">Hook unregistration failed</exception>
        Public Sub UnregisterHook()
            If HookHandle <> IntPtr.Zero Then
                UnregisterHookInternal(True)
            Else
                Throw New InvalidOperationException(ResourcesT.ExceptionsWin.HookIsNotRegisteredForThisInstance)
            End If
        End Sub
        ''' <summary>Unregisters the hook if this instance has registered it.</summary>
        ''' <param name="ThrowException">True to throwan exception when hook unregistration fails</param>
        ''' <exception cref="API.Win32APIException"><paramref name="ThrowException"/> is true and hook unregistration failed</exception>
        Private Sub UnregisterHookInternalCore(ByVal ThrowException As Boolean)
            If HookHandle = IntPtr.Zero Then Exit Sub
            If Not API.UnhookWindowsHookEx(HookHandle) Then
                If ThrowException Then Throw New API.Win32APIException()
            Else
                HookHandle = IntPtr.Zero
            End If
        End Sub
#End Region
#Region "Async"
        '''' <summary>Delegate to <see cref="UnregisterHookInternalAsync"/></summary>
        'Private dUnregisterHookInternalAsync As SendOrPostCallback

        ''' <summary>Contains value of the <see cref="IsAsync"/> property</summary>
        Private _IsAsync As Boolean
        ''' <summary>Gets value indicating if the <see cref="KeyEvent"/> event occures on different thread than hook was registered.</summary>
        ''' <returns>True when <see cref="KeyEvent"/> and <see cref="OnKeyEvent"/> are called on different thread that hook was registered.</returns>
        ''' <remarks>Has no meaning when <see cref="Registered"/> is false</remarks>
        Public ReadOnly Property IsAsync() As Boolean
            Get
                Return _IsAsync
            End Get
        End Property
        ''' <summary>In case this hook is asynchronous contains reference to hook thread</summary>
        Private HookThread As Thread
        '''' <summary>Used for callbacks to hook thread</summary>
        'Private HookCallback As AsyncOperation
#Region "Registration"
        ''' <summary>Registers a low-level keyboard hook on curent thread. The thread will be exited when hook is unregistered.</summary>
        ''' <exception cref="InvalidOperationException">Hook was already registered for this instance (<see cref="Registered"/> is true)</exception>
        ''' <exception cref="API.Win32APIException">An error ocured while obtaining the hook.</exception>
        ''' <exception cref="ObjectDisposedException"><see cref="IsDisposed"/> is true</exception>
        ''' <remarks>When hok is registered as asynchronous the <see cref="KeyEvent"/> event and <see cref="OnKeyEvent"/> methods are called in special thread. U can use this approach to handle the event in your form and use <see cref="Control.Invoke"/> to run event-handling code. The code then does not block keyboard hook which immediately returns.</remarks>
        Public Sub RegisterAsyncHook()
            If IsDisposed Then Throw New ObjectDisposedException(Me.GetType.Name)
            If Me.HookHandle <> IntPtr.Zero Then Throw New InvalidOperationException(ResourcesT.ExceptionsWin.HookIsAlreadyRegisteredForThisInstance)
            _IsAsync = True
            HookThread = New Thread(AddressOf RegisterAsyncHookInternal)
            HookThread.Name = "HookThread"
            HookThread.Start()
        End Sub
        ''' <summary>Application context used when hooks are obtained for separate thread</summary>
        Private AsyncAppContext As ApplicationContext

        ''' <summary>Registers a low-level keyboard hook on curent thread. The thread is another thread than hook registrator thread.</summary>
        ''' <exception cref="InvalidOperationException">Hook was already registered for this instance (<see cref="Registered"/> is true)</exception>
        ''' <exception cref="API.Win32APIException">An error ocured while obtaining the hook.</exception>
        ''' <exception cref="ObjectDisposedException"><see cref="IsDisposed"/> is true</exception>
        Private Sub RegisterAsyncHookInternal()
            RegisterHookInternal(True)
            'HookCallback = AsyncOperationManager.CreateOperation(Nothing )
            'dUnregisterHookInternalAsync = New SendOrPostCallback(AddressOf UnregisterHookInternalAsync)
            AsyncAppContext = New ApplicationContext
            Application.Run(AsyncAppContext)
        End Sub
#End Region
#Region "Unregistration"
        ''' <summary>Unregisters assynchronously registered hook</summary>
        ''' <param name="ThrowException">True to throwan exception when hook unregistration fails</param>
        ''' <exception cref="API.Win32APIException"><paramref name="ThrowException"/> is true and hook unregistration failed</exception>
        Private Sub UnregisterHookInternalAsync(ByVal ThrowException As Boolean)
            UnregisterHookInternalCore(ThrowException)
            AsyncAppContext.ExitThread()
            HookThread = Nothing : AsyncAppContext = Nothing
        End Sub
        ''' <summary>Unregisters the hook if this instance has registered it</summary>
        ''' <param name="ThrowException">True to throwan exception when hook unregistration fails</param>
        ''' <exception cref="API.Win32APIException"><paramref name="ThrowException"/> is true and hook unregistration failed</exception>
        Private Sub UnregisterHookInternal(ByVal ThrowException As Boolean)
            If IsAsync Then
                UnregisterHookInternalAsync(ThrowException)
            Else
                UnregisterHookInternalCore(ThrowException)
            End If
        End Sub
#End Region
#End Region
#End Region
#Region "Processing"
        ''' <summary>Delegate to <see cref="LowLevelKeyboardProc"/></summary>
        Private dLowLevelKeyboardProc As API.Hooks.LowLevelKeyboardProc = AddressOf LowLevelKeyboardProc
        ''' <summary>The LowLevelKeyboardProc hook procedure is an application-defined or library-defined callback function used with the SetWindowsHookEx function. The system calls this function every time a new keyboard input event is about to be posted into a thread input queue. The keyboard input can come from the local keyboard driver or from calls to the keybd_event function. If the input comes from a call to keybd_event, the input was "injected". However, the WH_KEYBOARD_LL hook is not injected into another process. Instead, the context switches back to the process that installed the hook and it is called in its original context. Then the context switches back to the application that generated the event.</summary>
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
        Private Function LowLevelKeyboardProc(ByVal nCode%, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
            If nCode < 0 Then Return API.CallNextHookEx(Me.HookHandle, nCode, wParam, lParam)
            Dim Parameters As API.KBDLLHOOKSTRUCT = Marshal.PtrToStructure(lParam, GetType(API.KBDLLHOOKSTRUCT))
            Dim e As New LowLevelKeyEventArgs(wParam, Parameters)
            OnKeyEvent(e)
            If Not e.Handled Then
                Return API.CallNextHookEx(Me.HookHandle, nCode, wParam, lParam)
            ElseIf e.Suppress Then
                Return 1
            Else
                Return 0
            End If
        End Function
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
#End Region
#Region " IDisposable Support "
        ''' <summary>Gets value indicating if this instance was already disposed</summary>
        Public ReadOnly Property IsDisposed() As Boolean
            Get
                Return disposedValue
            End Get
        End Property

        ''' <summary>Contains value of the <see cref="IsDisposed"/> property</summary>
        Private disposedValue As Boolean = False        ' To detect redundant calls

        ''' <summary>Implements <see cref="IDisposable.Dispose"/></summary>
        ''' <param name="disposing">True to free managed state</param>
        ''' <remarks>Note for inheritoers: ALways call base class method to didpose its state and raise the <see cref="Disposed"/> event</remarks>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    'HookCallback = Nothing
                    'HookThread = Nothing
                End If
                UnregisterHookInternal(True)
                RaiseEvent Disposed(Me, EventArgs.Empty)
            End If
            Me.disposedValue = True
        End Sub

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
        ''' <summary>Raised when object is disposed</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Event Disposed(ByVal sender As Object, ByVal e As EventArgs)

#End Region
    End Class
    ''' <summary>Event arguments for low-level keyboard hook</summary>
    Public Class LowLevelKeyEventArgs : Inherits EventArgs
        ''' <summary>CTor from keyboard action and parameters</summary>
        ''' <param name="Action">Action taken</param>
        ''' <param name="Parameters">Action parameters</param>
        Friend Sub New(ByVal Action As KeyboardEvent, ByVal Parameters As API.KBDLLHOOKSTRUCT)
            _Action = Action
            Me.Parameters = Parameters
        End Sub
        ''' <summary>Contains value of the <see cref="Action"/> property</summary>
        Private ReadOnly _Action As KeyboardEvent
        ''' <summary>Action parameters</summary>
        Private ReadOnly Parameters As API.KBDLLHOOKSTRUCT
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
        Public Property Suppress() As Boolean
            Get
                Return _Suppress
            End Get
            Set(ByVal value As Boolean)
                _Suppress = value
            End Set
        End Property
        ''' <summary>Indicates action which occured on keyboard</summary>
        Public ReadOnly Property Action() As KeyboardEvent
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
        Public ReadOnly Property ScanCode() As Byte
            Get
                Return Parameters.scanCode
            End Get
        End Property
        ''' <summary>Indicates if the ALT key was pressed</summary>
        Public ReadOnly Property AltState() As Boolean
            Get
                Return Parameters.flags And API.KBDLLHOOKSTRUCTFlags.LLKHF_ALTDOWN
            End Get
        End Property
        ''' <summary>Indicates if <see cref="Key"/> is extended key (such as numpad key or F key)</summary>
        Public ReadOnly Property IsExtended() As Boolean
            Get
                Return Parameters.flags And API.KBDLLHOOKSTRUCTFlags.LLKHF_EXTENDED
            End Get
        End Property
        ''' <summary>Indicates if kay was injected</summary>
        Public ReadOnly Property IsInjected() As Boolean
            Get
                Return Parameters.flags And API.KBDLLHOOKSTRUCTFlags.LLKHF_INJECTED
            End Get
        End Property
    End Class

    Public Enum KeyboardEvent
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
