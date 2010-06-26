Imports System.ComponentModel, Tools.ExtensionsT
#If Config <= Nightly Then 'Stage: Nightly
Namespace API.Messages.lParam
        ''' <summary>Bit-masks used by <see cref="WindowMessages.WM_CHAR"/>, <see cref="WindowMessages.WM_DEADCHAR"/>, <see cref="WindowMessages.WM_IME_CHAR"/>, <see cref="WindowMessages.WM_IME_KEYDOWN"/>, <see cref="WindowMessages.WM_IME_KEYUP"/>, <see cref="WindowMessages.WM_KEYDOWN"/>, <see cref="WindowMessages.WM_KEYUP"/>, <see cref="WindowMessages.WM_SYSCHAR"/>, <see cref="WindowMessages.WM_SYSDEADCHAR"/>, <see cref="WindowMessages.WM_SYSKEYDOWN"/>, <see cref="WindowMessages.WM_SYSKEYUP"/> message for lParam</summary>
        ''' <remarks>And lParam with mask and to get appropriete value. You can use <see cref="CreateWM_CHAR"/> to compose value of this type</remarks>
        ''' <seelaso cref="CreateWM_CHAR"/>
        Public Enum WM_CHAR As Integer
            ''' <summary>Bits 0÷15: Specifies the repeat count for the current message. The value is the number of times the keystroke is autorepeated as a result of the user holding down the key. If the keystroke is held long enough, multiple messages are sent. However, the repeat count is not cumulative.</summary>
            ''' <remarks><see cref="WindowMessages.WM_IME_CHAR"/>: Repeat count. Since the first byte and second byte is continuous, this is always 1.</remarks>
            RepeatCountMask = &HFFFF
            ''' <summary>Bits 16÷23: Specifies the scan code. The value depends on the OEM.</summary>
            ''' <remarks><see cref="WindowMessages.WM_IME_CHAR"/>: Scan code for a complete Asian character.</remarks>
            ScanCodeMask = &HFF0000
            ''' <summary>Bit 24: Specifies whether the key is an extended key, such as the right-hand ALT and CTRL keys that appear on an enhanced 101- or 102-key keyboard. The value is 1 if it is an extended key; otherwise, it is 0.</summary>
            ExtendedMask = &H1000000
            ''' <summary>Bits 25÷28: Reserved; do not use.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> ReservedMask = &H1E000000
            ''' <summary>Bit 29: Specifies the context code. The value is 1 if the ALT key is held down while the key is pressed; otherwise, the value is 0.</summary>
            ContextCodeMask = &H20000000
            ''' <summary>Bit 30: Specifies the previous key state. The value is 1 if the key is down before the message is sent, or it is 0 if the key is up.</summary>
            PreviousKeyStateMask = &H40000000
            ''' <summary>Bit 31: Specifies the transition state. The value is 1 if the key is being released, or it is 0 if the key is being pressed.</summary>
            TransitionStateMask = &H80000000
        End Enum
        ''' <summary>Bit masks used by the <see cref="WindowMessages.WM_ENDSESSION"/> message</summary>
        <Flags()> Public Enum WM_ENDSESSION As Integer
            ''' <summary>the system is shutting down or restarting (it is not possible to determine which event is occurring)</summary>
            <EditorBrowsable(EditorBrowsableState.Advanced)> Zero = 0
            ''' <summary>f wParam is TRUE, the application must shut down. Any data should be saved automatically without prompting the user (for more information, see Remarks). The Restart Manager sends this message when the application is using a file that needs to be replaced, when it must service the system, or when system resources are exhausted. The application will be restarted if it has registered for restart using the RegisterApplicationRestart function. For more information, see Guidelines for Applications.
            ''' If wParam is FALSE, the application should not shut down.</summary>
            ENDSESSION_CLOSEAPP = 1
            ''' <summary>The user is logging off. For more information, see Logging Off (http://msdn2.microsoft.com/en-us/library/aa376876.aspx).</summary>
            ENDSESSION_LOGOFF = &H80000000
        End Enum
        ''' <summary>Values osed for low word of lParam of <see cref="WindowMessages.WM_HOTKEY"/> message</summary>
        <Flags()> Public Enum WM_HOTKEY_low As Short
            ''' <summary>Either ALT key was held down.</summary>
            MOD_ALT = &H1
            ''' <summary>Either CTRL key was held down.</summary>
            MOD_CONTROL = &H2
            ''' <summary>Either SHIFT key was held down.</summary>
            MOD_SHIFT = &H4
            ''' <summary>Either WINDOWS key was held down. These keys are labeled with the Microsoft Windows logo. Hotkeys that involve the Windows key are reserved for use by the operating system.</summary>
            MOD_WIN = &H8
        End Enum
        ''' <summary>Values used for lParam of <see cref="WindowMessages.WM_IME_COMPOSITION"/></summary>
        <Flags()> Public Enum WM_IME_COMPOSITION
            ''' <summary>Retrieves or updates the attribute of the composition string.</summary>
            GCS_COMPATTR = IMECompositionStringValues.GCS_COMPATTR
            ''' <summary>Retrieves or updates clause information of the composition string.</summary>
            GCS_COMPCLAUSE = IMECompositionStringValues.GCS_COMPCLAUSE
            ''' <summary> 	Retrieves or updates the reading string of the current composition.</summary>
            GCS_COMPREADSTR = IMECompositionStringValues.GCS_COMPREADSTR
            ''' <summary>Retrieves or updates the attributes of the reading string of the current composition.</summary>
            GCS_COMPREADATTR = IMECompositionStringValues.GCS_COMPREADATTR
            ''' <summary> 	Retrieves or updates the clause information of the reading string of the composition string.</summary>
            GCS_COMPREADCLAUSE = IMECompositionStringValues.GCS_COMPREADCLAUSE
            ''' <summary> 	Retrieves or updates the current composition string.</summary>
            GCS_COMPSTR = IMECompositionStringValues.GCS_COMPSTR
            ''' <summary> 	Retrieves or updates the cursor position in composition string.</summary>
            GCS_CURSORPOS = IMECompositionStringValues.GCS_CURSORPOS
            ''' <summary> 	Retrieves or updates the cursor position in composition string.</summary>
            GCS_DELTASTART = IMECompositionStringValues.GCS_DELTASTART
            ''' <summary>Retrieves or updates clause information of the result string.</summary>
            GCS_RESULTCLAUSE = IMECompositionStringValues.GCS_RESULTCLAUSE
            ''' <summary> 	Retrieves or updates the reading string.</summary>
            GCS_RESULTREADCLAUSE = IMECompositionStringValues.GCS_RESULTREADCLAUSE
            ''' <summary> 	Retrieves or updates the reading string.</summary>
            GCS_RESULTREADSTR = IMECompositionStringValues.GCS_RESULTREADSTR
            ''' <summary>Retrieves or updates the string of the composition result.</summary>
            GCS_RESULTSTR = IMECompositionStringValues.GCS_RESULTSTR
            ''' <summary> 	Insert the wParam composition character at the current insertion point. An application should display the composition character if it processes this message.</summary>
            CS_INSERTCHAR = &H2000
            ''' <summary>Do not move the caret position as a result of processing the message. For example, if an IME specifies a combination of CS_INSERTCHAR and CS_NOMOVECARET, the application should insert the specified character at the current caret position but should not move the caret to the next position. A subsequent WM_IME_COMPOSITION message with GCS_RESULTSTR will replace this character.</summary>
            CS_NOMOVECARET = &H4000
        End Enum
        ''' <summary>Values used for lparam of <see cref="WindowMessages.WM_IME_SETCONTEXT"/> message</summary>
        <Flags()> Public Enum WM_IME_SETCONTEXT As Integer
            ''' <summary>Show the composition window by user interface window.</summary>
            ISC_SHOWUICOMPOSITIONWINDOW = &H80000000
            'ASAP: Find values of this constants!!!
            ''' <summary>Show the guide window by user interface window.</summary>
            <Obsolete("Value of this constant is unknown, do not use it!", True), EditorBrowsable(EditorBrowsableState.Never)> _
            ISC_SHOWUIGUIDWINDOW = Integer.MinValue
            ''' <summary>Show the candidate window of index 0 by user interface window.</summary>
            ISC_SHOWUICANDIDATEWINDOW = &H1
            'ASAP: Find values of this constants!!!
            ''' <summary>Show the soft keyboard by user interface window.</summary>
            <Obsolete("Value of this constant is unknown, do not use it!", True), EditorBrowsable(EditorBrowsableState.Never)> _
            ISC_SHOWUISOFTKBD = Integer.MinValue
            ''' <summary>Show the candidate window of index 1 by user interface window.</summary>
            ISC_SHOWUICANDIDATEWINDOW_l1 = ISC_SHOWUICANDIDATEWINDOW << 1
            ''' <summary> 	Show the candidate window of index 2 by user interface window.</summary>
            ISC_SHOWUICANDIDATEWINDOW_l2 = ISC_SHOWUICANDIDATEWINDOW << 2
            ''' <summary>Show the candidate window of index 3 by user interface window.</summary>
            ISC_SHOWUICANDIDATEWINDOW_l3 = ISC_SHOWUICANDIDATEWINDOW << 3
        End Enum
        ''' <summary>Values used for lParam of the <see cref="WindowMessages.WM_NOTIFYFORMAT"/> message</summary>
        Public Enum WM_NOTIFYFORMAT As Integer
            ''' <summary>The message is a query to determine whether ANSI or Unicode structures should be used in WM_NOTIFY messages. This command is sent from a control to its parent window during the creation of a control and in response to an NF_REQUERY command.</summary>
            NF_QUERY = 3
            ''' <summary>The message is a request for a control to send an NF_QUERY form of this message to its parent window. This command is sent from the parent window. The parent window is asking the control to requery it about the type of structures to use in WM_NOTIFY messages. If Command is NF_REQUERY, the return value is the result of the requery operation.</summary>
            NF_REQUERY = 4
        End Enum
        ''' <summary>Values used for lParam of the <see cref="WindowMessages.WM_PRINT"/> message</summary>
        <Flags()> Public Enum WM_PRINT As Integer
            ''' <summary>Draws all visible children windows.</summary>
            PRF_CHECKVISIBLE = &H1I
            ''' <summary>Draws all visible children windows.</summary>
            PRF_CHILDREN = &H10I
            ''' <summary>Draws the client area of the window.</summary>
            PRF_CLIENT = &H4I
            ''' <summary>Erases the background before drawing the window.</summary>
            PRF_ERASEBKGND = &H8I
            ''' <summary>Draws the nonclient area of the window.</summary>
            PRF_NONCLIENT = &H2I
            ''' <summary>Draws all owned windows.</summary>
            PRF_OWNED = &H20I
        End Enum
        ''' <summary>Values used for lParam of the <see cref="WindowMessages.WM_QUERYENDSESSION"/> message</summary>
        <Flags()> Public Enum WM_QUERYENDSESSION As Integer
            ''' <summary>The application is using a file that must be replaced, the system is being serviced, or system resources are exhausted. For more information, see Guidelines for Applications (http://msdn2.microsoft.com/en-us/library/aa373651.aspx).</summary>
            ENDSESSION_CLOSEAPP = &H1
            ''' <summary>The user is logging off. For more information, see Logging Off (http://msdn2.microsoft.com/en-us/library/aa376876.aspx).</summary>
            ENDSESSION_LOGOFF = &H80000000
        End Enum
        ''' <summary>Values used for lparam of the <see cref="WindowMessages.WM_SHOWWINDOW"/> message</summary>
        Public Enum WM_SHOWWINDOW As Integer
            ''' <summary>the message was sent because of a call to the ShowWindow function</summary>
            zero = 0
            ''' <summary>The window is being uncovered because a maximize window was restored or minimized.</summary>
            SW_OTHERUNZOOM = 4
            ''' <summary>The window is being covered by another window that has been maximized.</summary>
            SW_OTHERZOOM = 2
            ''' <summary>The window's owner window is being minimized.</summary>
            SW_PARENTCLOSING = 1
            ''' <summary>The window's owner window is being restored.</summary>
            SW_PARENTOPENING = 3
        End Enum

        ''' <summary>Device codes used by <see cref="WindowMessages.WM_APPCOMMAND"/></summary>
        <CLSCompliant(False)> _
        Public Enum WM_APPCOMMANDDevices As UShort
            ''' <summary>User pressed a key.</summary>
            FAPPCOMMAND_KEY = 0
            ''' <summary>Mask used by <see cref="macros.GET_DEVICE_LPARAM"/></summary>
            <EditorBrowsable(EditorBrowsableState.Advanced)> _
            FAPPCOMMAND_MASK = &HF000
            ''' <summary>User clicked a mouse button.</summary>
            FAPPCOMMAND_MOUSE = &H8000
            ''' <summary>An unidentified hardware source generated the event. It could be a mouse or a keyboard event.</summary>
            FAPPCOMMAND_OEM = &H1000
        End Enum
        ''' <summary>Application commands retrieved from lParam of <see cref="WindowMessages.WM_APPCOMMAND"/> by <see cref="GET_APPCOMMAND_LPARAM"/></summary>
        <CLSCompliant(False)> _
        Public Enum WM_APPCOMMANDCommands As UShort
            ''' <summary>Toggle the bass boost on and off.</summary>
            APPCOMMAND_BASS_BOOST = 20
            ''' <summary>Decrease the bass.</summary>
            APPCOMMAND_BASS_DOWN = 19
            ''' <summary>	Increase the bass.</summary>
            APPCOMMAND_BASS_UP = 21
            ''' <summary>	Navigate backward.</summary>
            APPCOMMAND_BROWSER_BACKWARD = 1
            ''' <summary>	Open favorites.</summary>
            APPCOMMAND_BROWSER_FAVORITES = 6
            ''' <summary>	Navigate forward.</summary>
            APPCOMMAND_BROWSER_FORWARD = 2
            ''' <summary>	Navigate home.</summary>
            APPCOMMAND_BROWSER_HOME = 7
            ''' <summary>	Refresh page.</summary>
            APPCOMMAND_BROWSER_REFRESH = 3
            ''' <summary>Open search.</summary>
            APPCOMMAND_BROWSER_SEARCH = 5
            ''' <summary>Stop download.</summary>
            APPCOMMAND_BROWSER_STOP = 4
            ''' <summary>	Start App1.</summary>
            APPCOMMAND_LAUNCH_APP1 = 17
            ''' <summary>	Start App2.</summary>
            APPCOMMAND_LAUNCH_APP2 = 18
            ''' <summary>	Open mail.</summary>
            APPCOMMAND_LAUNCH_MAIL = 15
            ''' <summary>	Go to Media Select mode.</summary>
            APPCOMMAND_LAUNCH_MEDIA_SELECT = 16
            ''' <summary>Go to next track.</summary>
            APPCOMMAND_MEDIA_NEXTTRACK = 11
            ''' <summary>	Play or pause playback.</summary>
            APPCOMMAND_MEDIA_PLAY_PAUSE = 14
            ''' <summary>Go to previous track.</summary>
            APPCOMMAND_MEDIA_PREVIOUSTRACK = 12
            ''' <summary>	Stop playback.</summary>
            APPCOMMAND_MEDIA_STOP = 13
            ''' <summary>	Decrease the treble.</summary>
            APPCOMMAND_TREBLE_DOWN = 22
            ''' <summary>	Increase the treble.</summary>
            APPCOMMAND_TREBLE_UP = 23
            ''' <summary>	Lower the volume.</summary>
            APPCOMMAND_VOLUME_DOWN = 9
            ''' <summary>	Mute the volume.</summary>
            APPCOMMAND_VOLUME_MUTE = 8
            ''' <summary>	Raise the volume.</summary>
            APPCOMMAND_VOLUME_UP = 10
            ''' <summary>Windows XP: Mute the microphone.</summary>
            APPCOMMAND_MICROPHONE_VOLUME_MUTE = 24
            ''' <summary>Windows XP: Decrease microphone volume.</summary>
            APPCOMMAND_MICROPHONE_VOLUME_DOWN = 25
            ''' <summary>	Windows XP: Increase microphone volume.</summary>
            APPCOMMAND_MICROPHONE_VOLUME_UP = 26

            ''' <summary>Windows XP: Close the window (not the application).</summary>
            APPCOMMAND_CLOSE = 31
            ''' <summary>Windows XP: Copy the selection.</summary>
            APPCOMMAND_COPY = 36
            ''' <summary>Windows XP: Brings up the correction list when a word is incorrectly identified during speech input.</summary>
            APPCOMMAND_CORRECTION_LIST = 45
            ''' <summary>Windows XP: Cut the selection.</summary>
            APPCOMMAND_CUT = 37
            ''' <summary>Windows XP: Toggles between two modes of speech input: dictation and command/control (giving commands to an application or accessing menus).</summary>
            APPCOMMAND_DICTATE_OR_COMMAND_CONTROL_TOGGLE = 43
            ''' <summary>Windows XP: Open the Find dialog.</summary>
            APPCOMMAND_FIND = 28
            ''' <summary>Windows XP: Forward a mail message.</summary>
            APPCOMMAND_FORWARD_MAIL = 40
            ''' <summary>Windows XP: Open the Help dialog.</summary>
            APPCOMMAND_HELP = 27
            ''' <summary>Windows XP SP1: Decrement the channel value, for example, for a TV or radio tuner.</summary>
            APPCOMMAND_MEDIA_CHANNEL_DOWN = 52
            ''' <summary>Windows XP SP1: Increment the channel value, for example, for a TV or radio tuner.</summary>
            APPCOMMAND_MEDIA_CHANNEL_UP = 51
            ''' <summary>Windows XP SP1: Increase the speed of stream playback. This can be implemented in many ways, for example, using a fixed speed or toggling through a series of increasing speeds.</summary>
            APPCOMMAND_MEDIA_FASTFORWARD = 49
            ''' <summary>Windows XP SP1: Pause. If already paused, take no further action. This is a direct PAUSE command that has no state. If there are discrete Play and Pause buttons, applications should take action on this command as well as APPCOMMAND_MEDIA_PLAY_PAUSE.</summary>
            APPCOMMAND_MEDIA_PAUSE = 47
            ''' <summary>Windows XP SP1: Begin playing at the current position. If already paused, it will resume. This is a direct PLAY command that has no state. If there are discrete Play and Pause buttons, applications should take action on this command as well as APPCOMMAND_MEDIA_PLAY_PAUSE.</summary>
            APPCOMMAND_MEDIA_PLAY = 46
            ''' <summary>Windows XP SP1: Begin recording the current stream.</summary>
            APPCOMMAND_MEDIA_RECORD = 48
            ''' <summary>Windows XP SP1: Go backward in a stream at a higher rate of speed. This can be implemented in many ways, for example, using a fixed speed or toggling through a series of increasing speeds.</summary>
            APPCOMMAND_MEDIA_REWIND = 50
            ''' <summary>Windows XP: Toggle the microphone.</summary>
            APPCOMMAND_MIC_ON_OFF_TOGGLE = 44
            ''' <summary>Windows XP: Create a new window.</summary>
            APPCOMMAND_NEW = 29
            ''' <summary>Windows XP: Open a window.</summary>
            APPCOMMAND_OPEN = 30
            ''' <summary>Windows XP: Paste</summary>
            APPCOMMAND_PASTE = 38
            ''' <summary>Windows XP: Print current document.</summary>
            APPCOMMAND_PRINT = 33
            ''' <summary>Windows XP: Redo last action.</summary>
            APPCOMMAND_REDO = 35
            ''' <summary>Windows XP: Reply to a mail message.</summary>
            APPCOMMAND_REPLY_TO_MAIL = 39
            ''' <summary>Windows XP: Save current document.</summary>
            APPCOMMAND_SAVE = 32
            ''' <summary>Windows XP: Send a mail message.</summary>
            APPCOMMAND_SEND_MAIL = 41
            ''' <summary>Windows XP: Initiate a spell check.</summary>
            APPCOMMAND_SPELL_CHECK = 42
            ''' <summary>Windows XP: Undo last action.</summary>
            APPCOMMAND_UNDO = 34
            ''' <summary>Windows Vista: Delete</summary>
            APPCOMMAND_DELETE = 53
            ''' <summary>Windows Vista: Flip 3D (as WinKey+Tab)</summary>
            APPCOMMAND_DWM_FLIP3D = 54
        End Enum

        ''' <summary>Defines macros used with lParams of windows messages</summary>
        <HideModuleName()> _
        Public Module Macros
            ''' <summary>retrieves the application command from the specified LPARAM value.</summary>
            ''' <param name="lParam">Specifies the value to be converted. </param>
            ''' <returns>The return value is the bits of the high-order word representing the application command.</returns>
            <CLSCompliant(False)> _
            Public Function GET_APPCOMMAND_LPARAM(ByVal lParam As Integer) As WM_APPCOMMANDCommands
                Return lParam.High And Not WM_APPCOMMANDDevices.FAPPCOMMAND_MASK
            End Function
            ''' <summary>retrieves the input device type from the specified LPARAM value</summary>
            ''' <param name="lParam">Specifies the value to be converted. </param>
            ''' <returns>The return value is the bit of the high-order word representing the input device type.</returns>
            <CLSCompliant(False)> _
            Public Function GET_DEVICE_LPARAM(ByVal lParam As Integer) As WM_APPCOMMANDDevices
                Return lParam.High And WM_APPCOMMANDDevices.FAPPCOMMAND_MASK
            End Function
            ''' <summary>Retrieves the state of certain virtual keys from the specified LPARAM value.</summary>
            ''' <param name="lParam">Specifies the value to be converted. </param>
            ''' <returns>The return value is the low-order word representing the virtual key state.</returns>
            Public Function GET_KEYSTATE_LPARAM(ByVal lParam As Integer) As WM_APPCOMMANDKeyStates
                Return lParam And &HFFFF
            End Function
            ''' <summary>Composes value of type <see cref="WM_CHAR"/> from its components</summary>
            ''' <param name="repeatCount">The repeat count for the current message. The value is the number of times the keystroke is autorepeated as a result of the user holding down the key. If the keystroke is held long enough, multiple messages are sent. However, the repeat count is not cumulative.</param>
            ''' <param name="scanCode">The scan code. The value depends on the OEM.</param>
            ''' <param name="extendedKey">ndicates whether the key is an extended key, such as the right-hand ALT and CTRL keys that appear on an enhanced 101- or 102-key keyboard.</param>
            ''' <param name="contextCode">The context code. For <see cref="WindowMessages.WM_CHAR"/>/<see cref="WindowMessages.WM_SYSCHAR"/> and WM_SYS... this is true when ALT key was hold while the key is pressed.</param>
            ''' <param name="previousState">Indicate previous state of the key. True if it was pressed, false if it was up. So, this is always false fro DOWN messages and always true for UP messages.</param>
            ''' <param name="transitionState">Transition state. True when key is being released, false if key is being pressed. So, this is always true for DOWN messages and always false for UP messages.</param>
            ''' <version version="1.5.3">This function is new in version 1.5.3</version>
            <CLSCompliant(False)>
            Public Function CreateWM_CHAR(ByVal repeatCount As UShort, ByVal scanCode As Byte, ByVal extendedKey As Boolean, ByVal contextCode As Boolean, ByVal previousState As Boolean, ByVal transitionState As Boolean) As WM_CHAR
                Dim ret As WM_CHAR = repeatCount
                ret = ret Or (CInt(scanCode) << 16)
                If extendedKey Then ret = ret Or WM_CHAR.ExtendedMask
                If contextCode Then ret = ret Or WM_CHAR.ContextCodeMask
                If previousState Then ret = ret Or WM_CHAR.PreviousKeyStateMask
                If transitionState Then ret = ret Or WM_CHAR.TransitionStateMask
                Return ret
            End Function
        End Module
        ''' <summary>State of certain virtual keys</summary>
        Public Enum WM_APPCOMMANDKeyStates As Integer
            ''' <summary>The CTRL key is down.</summary>
            MK_CONTROL = &H8
            ''' <summary>The left mouse button is down.</summary>
            MK_LBUTTON = &H1
            ''' <summary>The middle mouse button is down.</summary>
            MK_MBUTTON = &H10
            ''' <summary>The right mouse button is down.</summary>
            MK_RBUTTON = &H2
            ''' <summary>The SHIFT key is down.</summary>
            MK_SHIFT = &H4
            ''' <summary>The first X button is down.</summary>
            MK_XBUTTON1 = &H20
            ''' <summary>The second X button is down.</summary>
            MK_XBUTTON2 = &H40
        End Enum

    End Namespace
#End If