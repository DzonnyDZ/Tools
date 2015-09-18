Imports System.Runtime.InteropServices, System.Threading.Thread
Imports System.ComponentModel
Imports System.Threading


#If True
Namespace API.Hooks

    ''' <summary>Base class for classes used to handle Win32 hooks</summary>
    ''' <remarks>It is highly recomended not to install multiple hooks of same type in one application and to keep hook-handling code as quick as possible. You can significantly slow down user typing experience.
    ''' <para>This class uses Win32 API function SetWindowsHookEx().</para>
    ''' <para>.NET framework basically does not support global hooks to be installed. Mouse and keyboard low-level hooks implemented in <see cref="DevicesT.LowLevelMouseHook"/> and <see cref="DevicesT.LowLevelKeyboardHook"/> are the only exception.
    ''' In case you'll implement hook for example for WH_KEYBOARD, you will receive callbacks only as long as your application will be foreground one. The only workaround to this limitation is to have callback function in unmanaged DLL.</para>
    ''' <para>For more informations see <a href="http://msdn.microsoft.com/en-us/library/ms644990(VS.85).aspx">SetWindowsHookEx Function</a><a href="http://www.codeproject.com/KB/cs/globalhook.aspx?df=90&amp;fid=57596&amp;mpp=25&amp;noise=3&amp;sort=Position&amp;view=Quick&amp;fr=26">Processing Global Mouse and Keyboard Hooks in C#</a> and <a href="http://support.microsoft.com/default.aspx?scid=kb;en-us;318804">How to set a Windows hook in Visual C# .NET</a>.</para></remarks>
    <DefaultBindingProperty("KeyEvent"), EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public MustInherit Class Win32Hook
        Implements IDisposable
        ''' <summary>Contains value of the <see cref="HookHandle"/> property</summary>
        Private _HookHandle As IntPtr = IntPtr.Zero
        ''' <summary>Gets handle of current hook if it is registered</summary>
        ''' <returns>Handle of current hook if it is registered; <see cref="IntPtr.Zero"/> otherwise</returns>
        Protected ReadOnly Property HookHandle() As IntPtr
            Get
                Return _HookHandle
            End Get
        End Property
#Region "Registration and unregistration"
#Region "General"
        ''' <summary>Gets value indicating if hook is registered for this instance</summary>
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
            Dim NextHook = API.Hooks.SetWindowsHookEx(HandledHookType, dHookProcInternal, GetModuleHandle, 0) ' API.GetModuleHandle(GetType(LowLevelKeyboardHook).Module.FullyQualifiedName), 0)
            If NextHook = IntPtr.Zero Then Throw New API.Win32APIException
            Me._HookHandle = NextHook
            _IsAsync = IsAsync
            'If Not IsAsync Then HookThread = Nothing : HookCallback = Nothing : dUnregisterHookInternalAsync = Nothing
        End Sub
        ''' <summary>If implemented in derived class gets module handle pased to hMod parameter of SetWindowsHookEx Win32 API function</summary>
        ''' <returns>This implementation returns <see cref="IntPtr.Zero"/></returns>
        ''' <remarks>For some types of hooks it may be necesary to return correct module ID from this method, while another kinds works with <see cref="IntPtr.Zero"/></remarks>
        ''' <seelaso cref="GetModuleHandleFromType"/>
        Protected Overridable Function GetModuleHandle() As IntPtr
            Return IntPtr.Zero
        End Function
        ''' <summary>Gets module handle for given type</summary>
        ''' <param name="Type">Type to get handle of mudule for</param>
        ''' <returns>Handle to mudule in which type <paramref name="Type"/> is defined.</returns>
        ''' <remarks>You can use this function as return value of <see cref="GetModuleHandle"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        ''' <exception cref="API.Win32APIException">Error while obtaining module handle.</exception>
        ''' <seealso cref="GetModuleHandle"/>
        Protected Shared Function GetModuleHandleFromType(ByVal Type As Type) As IntPtr
            If Type Is Nothing Then Throw New ArgumentNullException("Type")
            Dim ret = API.Common.GetModuleHandle(Type.Module.FullyQualifiedName)
            If ret = IntPtr.Zero Then Throw New Win32APIException
            Return ret
        End Function
        ''' <summary>Delegate to <see cref="HookProcInternal"/></summary>
        Private dHookProcInternal As API.Hooks.HookProc = AddressOf HookProcInternal
        ''' <summary>If overriden in derived class gets type of hook represented by derived class</summary>
        ''' <returns>One of values accepted for SetWindowsHookEx idHook parameter</returns>
        Protected MustOverride ReadOnly Property HandledHookType() As HookType
        ''' <summary>Hook handler procedure. Caled when hook event occures</summary>
        ''' <param name="nCode">Specifies a code the hook procedure uses to determine how to process the message. If nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx.</param>
        ''' <param name="lParam">Meaning depeds on type of hook</param>
        ''' <param name="wParam">Meaning depends on type of hook</param>
        ''' <returns>If the hook procedure did not process the message, it is highly recomended that you call <see cref="CallNextHook"/> and return the value it returns; otherwise, other applications that have installed same hooks will not receive hook notifications and may behave incorrectly as a result.
        ''' If the hook procedure processed the message, it may return a nonzero value to prevent the system from passing the message to the rest of the hook chain or the target window procedure.</returns>
        ''' <remarks>Base class never calls this method when <paramref name="nCode"/> is negative.</remarks>
        Protected MustOverride Function HookProc(ByVal nCode%, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        ''' <summary>Hook handler procedure. Caled when hook event occures</summary>
        ''' <param name="nCode">Specifies a code the hook procedure uses to determine how to process the message. If nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx.</param>
        ''' <param name="lParam">Meaning depeds on type of hook</param>
        ''' <param name="wParam">Meaning depends on type of hook</param>
        ''' <returns>If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx.
        ''' <para>If nCode is greater than or equal to zero, and the hook procedure did not process the message, it is highly recommended that you call <see cref="CallNextHook"/> and return the value it returns; otherwise, other applications that have installed same hooks will not receive hook notifications and may behave incorrectly as a result.
        ''' If the hook procedure processed the message, it may return a nonzero value to prevent the system from passing the message to the rest of the hook chain or the target window procedure. </para></returns>
        Private Function HookProcInternal(ByVal nCode%, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
            If nCode < 0 Then Return CallNextHook(nCode, wParam, lParam)
            Return HookProc(nCode, wParam, lParam)
        End Function
        ''' <summary>The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain. A hook procedure can call this function either before or after processing the hook information.</summary>
        ''' <param name="nCode">[in] Specifies the hook code passed to the current hook procedure. The next hook procedure uses this code to determine how to process the hook information.</param>
        ''' <param name="wParam">[in] Specifies the wParam value passed to the current hook procedure. The meaning of this parameter depends on the type of hook associated with the current hook chain.</param>
        ''' <param name="lParam">[in] Specifies the lParam value passed to the current hook procedure. The meaning of this parameter depends on the type of hook associated with the current hook chain.</param>
        ''' <returns>This value is returned by the next hook procedure in the chain. The current hook procedure must also return this value. The meaning of the return value depends on the hook type. For more information, see the descriptions of the individual hook procedures.</returns>
        ''' <remarks>Hook procedures are installed in chains for particular hook types. CallNextHookEx calls the next hook in the chain.
        ''' <para>Calling CallNextHookEx is optional, but it is highly recommended; otherwise, other applications that have installed hooks will not receive hook notifications and may behave incorrectly as a result. You should call CallNextHookEx unless you absolutely need to prevent the notification from being seen by other applications.</para></remarks>
        Protected Function CallNextHook(ByVal nCode%, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
            API.Hooks.CallNextHookEx(HookHandle, nCode, wParam, lParam)
        End Function

        ''' <summary>Identifies Win32 hooks</summary>
        Public Enum HookType As Integer
            ''' <summary>Installs a hook procedure that monitors messages before the system sends them to the destination window procedure. For more information, see the CallWndProc hook procedure.</summary>
            CallWndProc = API.Hooks.WindowsHook.CALLWNDPROC
            ''' <summary>Installs a hook procedure that monitors messages after they have been processed by the destination window procedure. For more information, see the CallWndRetProc hook procedure.</summary>
            CallWndProcRet = API.Hooks.WindowsHook.CALLWNDPROCRET
            ''' <summary>Installs a hook procedure that receives notifications useful to a computer-based training (CBT) application. For more information, see the CBTProc hook procedure.</summary>
            ComputerBasedTraining = API.Hooks.WindowsHook.CBT
            ''' <summary>Installs a hook procedure useful for debugging other hook procedures. For more information, see the DebugProc hook procedure.</summary>
            Debug = API.Hooks.WindowsHook.DEBUG
            ''' <summary>Installs a hook procedure that will be called when the application's foreground thread is about to become idle. This hook is useful for performing low priority tasks during idle time. For more information, see the ForegroundIdleProc hook procedure. </summary>
            ForegroundIddle = API.Hooks.WindowsHook.FOREGROUNDIDLE
            ''' <summary>Installs a hook procedure that monitors messages posted to a message queue. For more information, see the GetMsgProc hook procedure.</summary>
            GetMessage = API.Hooks.WindowsHook.GETMESSAGE
            ''' <summary>Installs a hook procedure that posts messages previously recorded by a WH_JOURNALRECORD hook procedure. For more information, see the JournalPlaybackProc hook procedure.</summary>
            JournalPlayback = API.Hooks.WindowsHook.JOURNALPLAYBACK
            ''' <summary>Installs a hook procedure that records input messages posted to the system message queue. This hook is useful for recording macros. For more information, see the JournalRecordProc hook procedure.</summary>
            JournalRecord = API.Hooks.WindowsHook.JOURNALRECORD
            ''' <summary>Installs a hook procedure that monitors keystroke messages. For more information, see the KeyboardProc hook procedure.</summary>
            Keyboard = API.Hooks.WindowsHook.KEYBOARD
            ''' <summary>Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard input events. For more information, see the LowLevelKeyboardProc hook procedure.</summary>
            LowLevelKeyboard = API.Hooks.WindowsHook.KEYBOARD_LL
            ''' <summary>Installs a hook procedure that monitors mouse messages. For more information, see the MouseProc hook procedure.</summary>
            Mouse = API.Hooks.WindowsHook.MOUSE
            ''' <summary>Windows NT/2000/XP: Installs a hook procedure that monitors low-level mouse input events. For more information, see the LowLevelMouseProc hook procedure.</summary>
            LowLevelMouse = API.Hooks.WindowsHook.MOUSE_LL
            ''' <summary>Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. For more information, see the MessageProc hook procedure.</summary>
            MessageFilter = API.Hooks.WindowsHook.MSGFILTER
            ''' <summary>Installs a hook procedure that receives notifications useful to shell applications. For more information, see the ShellProc hook procedure.</summary>
            Shell = API.Hooks.WindowsHook.SHELL
            ''' <summary>Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. The hook procedure monitors these messages for all applications in the same desktop as the calling thread. For more information, see the SysMsgProc hook procedure.</summary>
            SysMessageFilter = API.Hooks.WindowsHook.SYSMSGFILTER
        End Enum

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
            If Not API.Hooks.UnhookWindowsHookEx(HookHandle) Then
                If ThrowException Then Throw New API.Win32APIException()
            Else
                _HookHandle = IntPtr.Zero
            End If
        End Sub
#End Region
#Region "Async"
        '''' <summary>Delegate to <see cref="UnregisterHookInternalAsync"/></summary>
        'Private dUnregisterHookInternalAsync As SendOrPostCallback

        ''' <summary>Contains value of the <see cref="IsAsync"/> property</summary>
        Private _IsAsync As Boolean
        ''' <summary>Gets value indicating if hook events occur on different thread than hook was registered.</summary>
        ''' <returns>True when <see cref="HookProc"/> is called on different thread that hook was registered.</returns>
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
        ''' <remarks>When hok is registered as asynchronous the <see cref="HookProc"/> function is called in special thread. You can use this approach to handle the event in your form and use <see cref="Control.Invoke"/> to run event-handling code. The code then does not block keyboard hook which immediately returns.</remarks>
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

End Namespace
#End If
