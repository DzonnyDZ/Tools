Imports System, System.Diagnostics, System.Windows.Forms, System.Runtime.InteropServices

Namespace API.Hooks
    ''' <summary>Contains API declarations related to Win32 hooks</summary>
    Friend Module Win32Hooks
        ''' <summary>Application-defined or library-defined callback function used with the SetWindowsHookEx function. The system calls this function every time a new keyboard input event is about to be posted into a thread input queue. The keyboard input can come from the local keyboard driver or from calls to the keybd_event function. If the input comes from a call to keybd_event, the input was "injected". However, the <see cref="WindowsHook.KEYBOARD_LL"/> hook is not injected into another process. Instead, the context switches back to the process that installed the hook and it is called in its original context. Then the context switches back to the application that generated the event.</summary>
        ''' <param name="nCode">[in] Specifies a code the hook procedure uses to determine how to process the message. If <paramref name="nCode"/> is less than zero, the hook procedure must pass the message to the <see cref="CallNextHookEx"/> function without further processing and should return the value returned by <see cref="CallNextHookEx"/>.</param>
        ''' <param name="wParam">[in] Specifies the identifier of the keyboard message. This parameter can be one of the following messages: <see cref="Messages.WindowMessages.WM_KEYDOWN"/>, <see cref="Messages.WindowMessages.WM_KEYUP"/>, <see cref="Messages.WindowMessages.WM_SYSKEYDOWN"/>, or <see cref="Messages.WindowMessages.WM_SYSKEYUP"/>. </param>
        ''' <param name="lParam">[in] Pointer to a <see cref="KBDLLHOOKSTRUCT"/> structure. </param>
        ''' <returns>If <paramref name="nCode"/> is less than zero, the hook procedure must return the value returned by <see cref="CallNextHookEx"/>.
        ''' <para>If <paramref name="nCode"/> is greater than or equal to zero, and the hook procedure did not process the message, it is highly recommended that you call <see cref="CallNextHookEx"/> and return the value it returns; otherwise, other applications that have installed <see cref="WindowsHook.KEYBOARD_LL"/> hooks will not receive hook notifications and may behave incorrectly as a result. If the hook procedure processed the message, it may return a nonzero value to prevent the system from passing the message to the rest of the hook chain or the target window procedure. </para></returns>
        ''' <remarks><para>An application installs the hook procedure by specifying the <see cref="WindowsHook.KEYBOARD_LL"/> hook type and a pointer to the hook procedure in a call to the <see cref="SetWindowsHookEx"/> function.</para>
        ''' <para>This hook is called in the context of the thread that installed it. The call is made by sending a message to the thread that installed the hook. Therefore, the thread that installed the hook must have a message loop.</para>
        ''' <para>The hook procedure should process a message in less time than the data entry specified in the LowLevelHooksTimeout value in the following registry key:</para>
        ''' <para>HKEY_CURRENT_USER\Control Panel\Desktop</para>
        ''' <para>The value is in milliseconds. If the hook procedure does not return during this interval, the system will pass the message to the next hook.</para>
        ''' <para>Note that debug hooks cannot track this type of hook.</para></remarks>
        Public Delegate Function LowLevelKeyboardProc(ByVal nCode As LowLevelKeyboardProcHookCode, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        ''' <summary>Generic hook hendler procedure</summary>
        ''' <param name="nCode">Specifies a code the hook procedure uses to determine how to process the message. If <paramref name="nCode"/> is less than zero, the hook procedure must pass the message to the <see cref="CallNextHookEx"/> function without further processing and should return the value returned by <see cref="CallNextHookEx"/>.</param>
        ''' <param name="wParam">[in] Specifies the wParam value passed to the current hook procedure. The meaning of this parameter depends on the type of hook associated with the current hook chain.</param>
        ''' <param name="lParam">[in] Specifies the lParam value passed to the current hook procedure. The meaning of this parameter depends on the type of hook associated with the current hook chain.</param>
        ''' <returns>If <paramref name="nCode"/> is less than zero, the hook procedure must return the value returned by <see cref="CallNextHookEx"/>.
        ''' <para>If <paramref name="nCode"/> is greater than or equal to zero, and the hook procedure did not process the message, it is highly recommended that you call <see cref="CallNextHookEx"/> and return the value it returns; otherwise, other applications that have installed this hooks will not receive hook notifications and may behave incorrectly as a result. If the hook procedure processed the message, it may return a nonzero value to prevent the system from passing the message to the rest of the hook chain or the target window procedure. </para></returns>
        Public Delegate Function HookProc(ByVal nCode%, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        ''' <summary>Hook codes used by low level keyboard and mouse hooks</summary>
        Public Enum LowLevelKeyboardProcHookCode As Integer
            ''' <summary>The wParam and lParam parameters contain information about a keyboard message.</summary>
            ACTION = 0
        End Enum
        ''' <summary>Hook codes used by keyboard hook</summary>
        Public Enum KeyboardProcHookCode
            ''' <summary>The wParam and lParam parameters contain information about a keystroke message.</summary>
            HC_ACTION = LowLevelKeyboardProcHookCode.ACTION
            ''' <summary>The wParam and lParam parameters contain information about a keystroke message, and the keystroke message has not been removed from the message queue. (An application called the PeekMessage function, specifying the PM_NOREMOVE flag.)</summary>
            HC_NOREMOVE = 3
        End Enum


        ''' <summary>Windows hooks IDs</summary>
        Public Enum WindowsHook As Integer
            ''' <summary>Installs a hook procedure that monitors messages before the system sends them to the destination window procedure. For more information, see the <see cref="CallWndProc"/> hook procedure.</summary>
            CALLWNDPROC = 4
            ''' <summary>Installs a hook procedure that monitors messages after they have been processed by the destination window procedure. For more information, see the CallWndRetProc hook procedure.</summary>
            CALLWNDPROCRET = 12
            ''' <summary>Installs a hook procedure that receives notifications useful to a computer-based training (CBT) application. For more information, see the CBTProc hook procedure.</summary>
            CBT = 5
            ''' <summary>Installs a hook procedure useful for debugging other hook procedures. For more information, see the DebugProc hook procedure.</summary>
            DEBUG = 9
            ''' <summary>Installs a hook procedure that will be called when the application's foreground thread is about to become idle. This hook is useful for performing low priority tasks during idle time. For more information, see the ForegroundIdleProc hook procedure. </summary>
            FOREGROUNDIDLE = 11
            ''' <summary>Installs a hook procedure that monitors messages posted to a message queue. For more information, see the GetMsgProc hook procedure.</summary>
            GETMESSAGE = 3
            ''' <summary>Installs a hook procedure that posts messages previously recorded by a <see cref="WindowsHook.JOURNALRECORD"/> hook procedure. For more information, see the JournalPlaybackProc hook procedure.</summary>
            JOURNALPLAYBACK = 1
            ''' <summary>Installs a hook procedure that records input messages posted to the system message queue. This hook is useful for recording macros. For more information, see the JournalRecordProc hook procedure.</summary>
            JOURNALRECORD = 0
            ''' <summary>Installs a hook procedure that monitors keystroke messages. For more information, see the KeyboardProc hook procedure.</summary>
            KEYBOARD = 2
            ''' <summary>Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard input events. For more information, see the <see cref="LowLevelKeyboardProc"/> hook procedure.</summary>
            KEYBOARD_LL = 13
            ''' <summary>Installs a hook procedure that monitors mouse messages. For more information, see the MouseProc hook procedure.</summary>
            MOUSE = 7
            ''' <summary>Windows NT/2000/XP: Installs a hook procedure that monitors low-level mouse input events. For more information, see the LowLevelMouseProc hook procedure.</summary>
            MOUSE_LL = 14
            ''' <summary>Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. For more information, see the MessageProc hook procedure.</summary>
            MSGFILTER = -1
            ''' <summary>Installs a hook procedure that receives notifications useful to shell applications. For more information, see the ShellProc hook procedure.</summary>
            SHELL = 10
            ''' <summary>Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. The hook procedure monitors these messages for all applications in the same desktop as the calling thread. For more information, see the SysMsgProc hook procedure.</summary>
            SYSMSGFILTER = 6
        End Enum

        ''' <summary>Installs an application-defined hook procedure into a hook chain. You would install a hook procedure to monitor the system for certain types of events. These events are associated either with a specific thread or with all threads in the same desktop as the calling thread.</summary>
        ''' <param name="idHook">Specifies the type of hook procedure to be installed.</param>
        ''' <param name="lpfn">[in] Pointer to the hook procedure. If the <paramref name="dwThreadId"/> parameter is zero or specifies the identifier of a thread created by a different process, the lpfn parameter must point to a hook procedure in a DLL. Otherwise, lpfn can point to a hook procedure in the code associated with the current process. </param>
        ''' <param name="hMod">[in] Handle to the DLL containing the hook procedure pointed to by the lpfn parameter. The hMod parameter must be set to NULL if the <paramref name="dwThreadId"/> parameter specifies a thread created by the current process and if the hook procedure is within the code associated with the current process.</param>
        ''' <param name="dwThreadId">[in] Specifies the identifier of the thread with which the hook procedure is to be associated. If this parameter is zero, the hook procedure is associated with all existing threads running in the same desktop as the calling thread.</param>
        ''' <returns>If the function succeeds, the return value is the handle to the hook procedure. If the function fails, the return value is NULL.</returns>
        Public Declare Auto Function SetWindowsHookEx Lib "user32.dll" (ByVal idHook As WindowsHook, ByVal lpfn As HookProc, ByVal hMod As IntPtr, ByVal dwThreadId As UInteger) As IntPtr

        ''' <summary>Removes a hook procedure installed in a hook chain by the <see cref="SetWindowsHookEx"/> function. </summary>
        ''' <param name="hhk">[in] Handle to the hook to be removed. This parameter is a hook handle obtained by a previous call to <see cref="SetWindowsHookEx"/>. </param>
        ''' <returns>If the function succeeds, the return value is true. If the function fails, the return value is zero.</returns>
        Public Declare Auto Function UnhookWindowsHookEx Lib "user32.dll" (ByVal hhk As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean

        ''' <summary>Passes the hook information to the next hook procedure in the current hook chain. A hook procedure can call this function either before or after processing the hook information.</summary>
        ''' <param name="hhk">[in] Windows 95/98/ME: Handle to the current hook. An application receives this handle as a result of a previous call to the <see cref="SetWindowsHookEx"/> function.
        ''' <para>Windows NT/XP/2003: Ignored.</para></param>
        ''' <param name="nCode">[in] Specifies the hook code passed to the current hook procedure. The next hook procedure uses this code to determine how to process the hook information.</param>
        ''' <param name="wParam">[in] Specifies the wParam value passed to the current hook procedure. The meaning of this parameter depends on the type of hook associated with the current hook chain.</param>
        ''' <param name="lParam">[in] Specifies the lParam value passed to the current hook procedure. The meaning of this parameter depends on the type of hook associated with the current hook chain.</param>
        ''' <returns>This value is returned by the next hook procedure in the chain. The current hook procedure must also return this value. The meaning of the return value depends on the hook type. For more information, see the descriptions of the individual hook procedures.</returns>
        ''' <remarks>Hook procedures are installed in chains for particular hook types. <see cref="CallNextHookEx"/> calls the next hook in the chain.
        ''' <para>Calling <see cref="CallNextHookEx"/> is optional, but it is highly recommended; otherwise, other applications that have installed hooks will not receive hook notifications and may behave incorrectly as a result. You should call <see cref="CallNextHookEx"/> unless you absolutely need to prevent the notification from being seen by other applications.</para></remarks>
        Public Declare Auto Function CallNextHookEx Lib "user32.dll" (ByVal hhk As IntPtr, ByVal nCode%, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        ''' <summary>Contains information about a low-level keyboard input event. </summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure KBDLLHOOKSTRUCT
            ''' <summary>Specifies a virtual-key code. The code must be a value in the range 1 to 254. </summary>
            Public vkCode As Integer
            ''' <summary>Specifies a hardware scan code for the key.</summary>
            Public scanCode As Integer
            ''' <summary>Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.</summary>
            Public flags As KBDLLHOOKSTRUCTFlags
            ''' <summary>Specifies the time stamp for this message, equivalent to what GetMessageTime would return for this message.</summary>
            Public time As Integer
            ''' <summary>Specifies extra information associated with the message. </summary>
            Public dwExtraInfo As IntPtr
        End Structure
        ''' <summary><see cref="KBDLLHOOKSTRUCT.flags"/> masks</summary>
        <Flags()> _
        Public Enum KBDLLHOOKSTRUCTFlags As Integer
            ''' <summary>Specifies whether the key is an extended key, such as a function key or a key on the numeric keypad. The value is 1 if the key is an extended key; otherwise, it is 0.</summary>
            LLKHF_EXTENDED = &H1
            ''' <summary>Specifies whether the event was injected. The value is 1 if the event was injected; otherwise, it is 0.</summary>
            LLKHF_INJECTED = &H10
            ''' <summary>Specifies the context code. The value is 1 if the ALT key is pressed; otherwise, it is 0.</summary>
            LLKHF_ALTDOWN = &H20
            ''' <summary>Specifies the transition state. The value is 0 if the key is pressed and 1 if it is being released.</summary>
            LLKHF_UP = &H80
        End Enum
        ''' <summary>Contains information about a low-level keyboard input event. </summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure MSLLHOOKSTRUCT
            ''' <summary>Specifies a POINT structure that contains the x- and y-coordinates of the cursor, in screen coordinates. </summary>
            Public pt As POINTAPI
            ''' <summary><para>If the message is WM_MOUSEWHEEL, the high-order word of this member is the wheel delta. The low-order word is reserved. A positive value indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the user. One wheel click is defined as WHEEL_DELTA, which is 120.</para>
            ''' <para>If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released, and the low-order word is reserved. This value can be one or more of the following values. Otherwise, mouseData is not used.</para></summary>
            Public mouseData As Integer
            ''' <summary>Gets high word of <see cref="mouseData"/>. wheel delta, x button number</summary>
            Public ReadOnly Property mouseData_high() As Short
                Get
                    Return (mouseData And &HFFFF0000) >> 16
                End Get
            End Property
            ''' <summary>Gets low word of <see cref="mouseData"/></summary>
            Public ReadOnly Property mouseData_low() As Short
                Get
                    Return mouseData And &HFFFF
                End Get
            End Property
            ''' <summary>Specifies the event-injected flag.</summary>
            Public flags As MSLLHOOKSTRUCTFlags
            ''' <summary>Specifies the time stamp for this message. </summary>
            Public time As Integer
            ''' <summary>Specifies extra information associated with the message. </summary>
            Public dwExtraInfo As IntPtr
        End Structure
        ''' <summary><see cref="MSLLHOOKSTRUCT.flags"/> values</summary>
        <Flags()> _
        Public Enum MSLLHOOKSTRUCTFlags As Integer
            ''' <summary>Specifies whether the event was injected. The value is 1 if the event was injected; otherwise, it is 0.</summary>
            LLMHF_INJECTED = 1
        End Enum
    End Module
End Namespace
