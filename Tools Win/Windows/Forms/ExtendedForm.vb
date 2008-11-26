Imports Tools.API
Imports System.ComponentModel
Imports Tools.ComponentModelt, Tools.Windowst.Formst.Utilitiest
#If Config <= RC Then 'Stage: RC
Namespace WindowsT.FormsT
    ''' <summary><see cref="System.Windows.Forms.Form"/> with additional functionality based on Win32 API</summary>
    ''' <author www="http://dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="RC"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Class ExtendedForm : Inherits System.Windows.Forms.Form
#Region "CloseBox"
        ''' <summary>Possible states of system menu</summary>
        Public Enum SystemMenuState
            ''' <summary>Indicates that the menu item is disabled, but not grayed, so it cannot be selected, but visually seems like it can be selected. Button in title bar is greyed.
            ''' Note: User CANNOT press Alt+F4 (or use another method exluding force methods (kill)) to close window if used by instance member (not static (shared)) <see cref="ExtendedForm.CloseBoxEnabled"/>; if used with static (shared) <see cref="CloseBoxEnabled"/> user CAN use Alt+F4 (or other 'soft' methods) to close window.</summary>
            Disabled = enmEnableMenuItemStatus.MF_DISABLED
            ''' <summary>Indicates that the menu item is enabled and restored from a grayed state so that it can be selected.</summary>
            Enabled = enmEnableMenuItemStatus.MF_ENABLED
            ''' <summary>Indicates that the menu item is disabled and grayed so that it cannot be selected. Button in title bar is greyed.
            ''' Note: User CANNOT press Alt+F4 (or use another method exluding force methods (kill)) to close window if used by instance member (not static (shared)) <see cref="ExtendedForm.CloseBoxEnabled"/>; if used with static (shared) <see cref="CloseBoxEnabled"/> user CAN use Alt+F4 (or other 'soft' methods) to close window.</summary>
            Grayed = enmEnableMenuItemStatus.MF_GRAYED
        End Enum

        ''' <summary>Gets or sets state of selected item of system menu of window represented by <paramref name="WindowHandle"/></summary>
        ''' <param name="MenuItem">Item of system menu (NOTE: Only <see cref="SystemMenuItems.SC_CLOSE"/> works)</param>
        ''' <param name="WindowHandle">Handle to window which's menu should be queryed or altered</param>
        ''' <value>New state of menu item</value>
        ''' <returns>Curent state of menu item</returns>
        ''' <remarks>Getter is little bit destructive - it sets menu state to <see cref="enmEnableMenuItemStatus.MF_ENABLED"/> in order to get its state and then renew its state to just got value</remarks>
        ''' <exception cref="ArgumentException">Given <paramref name="MenuItem"/> doesnt exists</exception> 
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        Private Shared Property SystemMenuItemEnabled(ByVal WindowHandle As Int32, ByVal MenuItem As API.SystemMenuItems) As SystemMenuState
            Get
                Dim Prev As enmPreviousMenuItemStatus = EnableMenuItem(GetSystemMenu(WindowHandle, 0), MenuItem, enmEnableMenuItemStatus.MF_BYCOMMAND Or enmEnableMenuItemStatus.MF_ENABLED)
                If Prev = enmPreviousMenuItemStatus.DoesNotExist Then Throw New ArgumentException(ResourcesT.ExceptionsWin.MenuItemDoesnTExist, "MenuItem")
                SystemMenuItemEnabled = Prev
                SystemMenuItemEnabled(WindowHandle, MenuItem) = Prev
            End Get
            Set(ByVal value As SystemMenuState)
                Select Case EnableMenuItem(GetSystemMenu(WindowHandle, 0), MenuItem, enmEnableMenuItemStatus.MF_BYCOMMAND Or value)
                    Case enmPreviousMenuItemStatus.DoesNotExist
                        Throw New ArgumentException(ResourcesT.ExceptionsWin.MenuItemDoesnTExist, "MenuItem")
                    Case Else
                End Select
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="CloseBoxEnabled"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _CloseBoxEnabled As SystemMenuState = SystemMenuState.Enabled
        ''' <summary>Gets or sets state of the close button ('X') of current <see cref="ExtendedForm"/></summary>
        ''' <value>New state of button</value>
        ''' <returns>Curent state of button</returns>
        ''' <remarks>Status of menu is not re-set after changing <see cref="MinimizeBox"/> or <see cref="MaximizeBox"/> property. You have to refresh user-visible status of this property manually!</remarks>
        ''' <exception cref="ArgumentException">Error while accessing system menu status (may be caused by no close item in system menu - e.g. because <see cref="CloseBox"/> is false)</exception>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.WindowStyle), LDescription(GetType(WindowsT.FormsT.ControlsWin), "CloseBoxEnabled_d")> _
        Public Property CloseBoxEnabled() As SystemMenuState
            Get
                Return _CloseBoxEnabled
            End Get
            Set(ByVal value As SystemMenuState)
                CloseBoxEnabled(Me) = value
                _CloseBoxEnabled = CloseBoxEnabled(Me)
            End Set
        End Property
        ''' <summary>Determines if designer should serialize the <see cref="CloseBoxEnabled"/> property's value</summary>
        ''' <returns>True when <see cref="CloseBoxEnabled"/> is not <see cref="SystemMenuState.Enabled"/> and <see cref="CloseBox"/> is True</returns>
        Private Function ShouldSerializeCloseBoxEnabled() As Boolean
            Return CloseBox AndAlso CloseBoxEnabled <> SystemMenuState.Enabled
        End Function
        ''' <summary>Resets the <see cref="CloseBoxEnabled"/> property to its default value <see cref="SystemMenuState.Enabled"/></summary>
        Private Sub ResetCloseBoxEnabled()
            Try
                CloseBoxEnabled = SystemMenuState.Enabled
            Catch ex As ArgumentException
                _CloseBoxEnabled = SystemMenuState.Enabled
            End Try
        End Sub
        ''' <summary>Gets or sets state of the close button ('X') of given <see cref="System.Windows.Forms.IWin32Window"/></summary>
        ''' <param name="Window">Window (form) to get or set state of close button</param>
        ''' <value>New state of button</value>
        ''' <returns>Curent state of button</returns>
        ''' <remarks>Value set via static (shared) property may be lost when window if minimized, maximized or restored</remarks>
        ''' <exception cref="ArgumentException">Error while accessing system menu status (may be caused by no close item in system menu)</exception>
        Public Shared Property CloseBoxEnabled(ByVal Window As System.Windows.Forms.IWin32Window) As SystemMenuState
            Get
                Return CloseBoxEnabled(Window.Handle)
            End Get
            Set(ByVal value As SystemMenuState)
                CloseBoxEnabled(Window.Handle) = value
            End Set
        End Property
        ''' <summary>Gets or sets state of the close button ('X') of window with given handle</summary>
        ''' <param name="WindowHandle">Handle of window to get or set state of close button</param>
        ''' <value>New state of button</value>
        ''' <returns>Curent state of button</returns>
        ''' <remarks>Value set via static (shared) property may be lost when window if minimized, maximized or restored</remarks>
        ''' <exception cref="ArgumentException">Error while accessing system menu status (may be caused by no close item in system menu)</exception>
        Public Shared Property CloseBoxEnabled(ByVal WindowHandle As IntPtr) As SystemMenuState
            Get
                Return SystemMenuItemEnabled(WindowHandle.ToInt32, SystemMenuItems.SC_CLOSE)
            End Get
            Set(ByVal value As SystemMenuState)
                SystemMenuItemEnabled(WindowHandle.ToInt32, SystemMenuItems.SC_CLOSE) = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="CloseBox"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _CloseBox As Boolean = True
        ''' <summary>Determines if form has close box in upper-right corner in its caption bar</summary>
        ''' <remarks>Setting this property to false causes mennuitem "Close" disappearing and the X button being grayed. User CANNOT use Alt+F4 or other 'non-killing' method to close the window. Property <see cref="CloseBoxEnabled"/> cannot be changed while <see cref="CloseBox"/> is False.
        ''' You'd better use <see cref="CloseBoxEnabled"/>
        ''' Windows Vista: The close (X) button is NOT grayed but does nothing when user click it.</remarks>
        <DefaultValue(True), KnownCategory(KnownCategoryAttribute.KnownCategories.WindowStyle), LDescription(GetType(WindowsT.FormsT.ControlsWin), "CloseBox_d")> _
        Public Property CloseBox() As Boolean
            Get
                Return _CloseBox
            End Get
            Set(ByVal value As Boolean)
                If value <> CloseBox Then
                    If value Then
                        If GetSystemMenu(Me.Handle, APIBool.TRUE) = -1 Then
                            Throw New InvalidOperationException(ResourcesT.ExceptionsWin.UnableToObtainHandleToSystemMenu)
                        End If
                        Me.CloseBoxEnabled = Me.CloseBoxEnabled
                    Else
                        If RemoveMenu(GetSystemMenu(Me.Handle, APIBool.FALSE), SystemMenuItems.SC_CLOSE, enmSelectMenuMethod.MF_BYCOMMAND) = 0 Then
                            Throw New Win32APIException
                        End If
                        DrawMenuBar(Me.Handle)
                    End If
                    _CloseBox = value
                End If
            End Set
        End Property
        ''' <summary>(Re)applies the <see cref="CloseBoxEnabled"/> property on the system menu</summary>
        Private Sub ApplyCloseBoxEnabled()
            If Me.CloseBox Then _
                Me.CloseBoxEnabled = Me.CloseBoxEnabled
        End Sub
#End Region
#Region "Window State Events"
        ''' <summary>Raised after the <see cref="WindowState"/> property is changed</summary>
        ''' <param name="sender">source of the event</param>
        ''' <param name="e">Event params (always <see cref="EventArgs.Empty"/>)</param>
        <LDescription(GetType(WindowsT.FormsT.ControlsWin), "WindowStateChanged_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
        Public Event WindowStateChanged As EventHandler
        ''' <summary>Raises the <see cref="WindowStateChanged"/> event</summary>
        ''' <param name="e">Event arguments (always <see cref="EventArgs.Empty"/>)</param>
        ''' <remarks>Note to inheritors: Always call base class's method in order the event to be raised and additional functionalidy maintained in base class's method to work properly</remarks>
        Protected Overridable Sub OnWindowStateChanged(ByVal e As EventArgs)
            ApplyCloseBoxEnabled()
            RaiseEvent WindowStateChanged(Me, e)
        End Sub
#End Region
#Region "Events"
        ''' <summary>Processes Windows messages.</summary>
        ''' <param name="m">The Windows <see cref="Message"/> to process. </param>
        ''' <remarks>Note for inheritors: Always call base class's method <see cref="WndProc"/> unless you should block certain base class's functionality</remarks>
        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            'Pre process
            Select Case m.Msg
                Case Messages.WindowMessages.WM_SYSCOMMAND
                    Select Case m.WParam
                        Case SystemMenuItems.SC_CLOSE
                            If CloseBox = False OrElse CloseBoxEnabled <> SystemMenuState.Enabled Then _
                                Return 'Disable possibility to close window using Alt+F4 when close button is disabled. This is not because the Vista issue related to the CloseBox property
                    End Select
            End Select

            MyBase.WndProc(m)

            'Post process
            Select Case m.Msg
                Case Messages.WindowMessages.WM_SYSCOMMAND
                    Select Case m.WParam
                        Case Messages.wParam.WM_SYSCOMMAND.SC_RESTORE, Messages.wParam.WM_SYSCOMMAND.SC_MAXIMIZE, Messages.wParam.WM_SYSCOMMAND.SC_MINIMIZE
                            OnWindowStateChanged(EventArgs.Empty) 'State change events
                    End Select
                Case Messages.WindowMessages.WM_APPCOMMAND
                    Dim e As New ApplicationCommandEventArgs(m.LParam)
                    OnApplicationCommand(e)
                    If e.Handled Then m.Result = 1 Else m.Result = 0
            End Select
        End Sub
        ''' <summary>Raised when an application command is received by form</summary>
        ''' <remarks>When form ius not foreground it receives the event only when there is no other application that handles it.</remarks>
        <LDescription(GetType(WindowsT.FormsT.ControlsWin), "ApplicationCommand_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
        Public Event ApplicationCommand As EventHandler(Of ApplicationCommandEventArgs)
        ''' <summary>Raises the <see cref="ApplicationCommand"/> event</summary>
        ''' <param name="e">Event erguments</param>
        ''' <remarks>Note to inheritors: In order the event to be raised call base class method</remarks>
        Protected Overridable Sub OnApplicationCommand(ByVal e As ApplicationCommandEventArgs)
            RaiseEvent ApplicationCommand(Me, e)
        End Sub
#End Region
    End Class
    ''' <summary>Argument of the <see cref="ExtendedForm.ApplicationCommand"/> event</summary>
    Public Class ApplicationCommandEventArgs
        Inherits HandledEventArgs
        ''' <summary>CTor from <see cref="API.Messages.WindowMessages.WM_APPCOMMAND"/> lParam</summary>
        ''' <param name="lParam">lParam from the <see cref="API.Messages.WindowMessages.WM_APPCOMMAND"/> message</param>
        Public Sub New(ByVal lParam%)
            Me.lParam = lParam
        End Sub
        ''' <summary>lParam from the <see cref="API.Messages.WindowMessages.WM_APPCOMMAND"/> message</summary>
        Private ReadOnly lParam As Integer
        ''' <summary>Identifies source device kind of application commnd</summary>
        ''' <seelaso cref="API.Messages.lParam.WM_APPCOMMANDDevices"/>
        Public Enum Devices As Integer
            ''' <summary>User pressed a key.</summary>
            Keyboard = API.Messages.lParam.WM_APPCOMMANDDevices.FAPPCOMMAND_KEY
            ''' <summary>User clicked a mouse button.</summary>
            Mouse = API.Messages.lParam.WM_APPCOMMANDDevices.FAPPCOMMAND_MOUSE
            ''' <summary>An unidentified hardware source generated the event. It could be a mouse or a keyboard event.</summary>
            Unknown = API.Messages.lParam.WM_APPCOMMANDDevices.FAPPCOMMAND_OEM
        End Enum
        ''' <summary>Identifies source device of application command</summary>
        Public ReadOnly Property Device() As Devices
            Get
                Return API.Messages.lParam.GET_DEVICE_LPARAM(lParam)
            End Get
        End Property
        ''' <summary>Identifies the application command</summary>
        ''' <returns>Command that was sent to application</returns>
        Public ReadOnly Property Command() As ApplicationComands
            Get
                Return API.Messages.lParam.GET_APPCOMMAND_LPARAM(lParam)
            End Get
        End Property
    End Class
    ''' <summary>Application command</summary>
    ''' <remarks>These commands are sent to applications as windows messages.</remarks>
    ''' <seelaso cref="API.Messages.lParam.WM_APPCOMMANDCommands"/>
    ''' <seelaso cref="ExtendedForm.ApplicationCommand"/>
    ''' <seelaso cref="ExtendedForm.OnApplicationCommand"/>
    ''' <seelaso cref="Api.Messages.WindowMessages.WM_APPCOMMAND"/>
    Public Enum ApplicationComands As Integer
        ''' <summary>Toggle the bass boost on and off.</summary>
        BassBoost = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_BASS_BOOST
        ''' <summary>Decrease the bass.</summary>
        BassDown = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_BASS_DOWN
        ''' <summary>	Increase the bass.</summary>
        BassUp = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_BASS_UP
        ''' <summary>	Navigate backward.</summary>
        BrowserBack = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_BROWSER_BACKWARD
        ''' <summary>	Open favorites.</summary>
        BrowserFavorites = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_BROWSER_FAVORITES
        ''' <summary>	Navigate forward.</summary>
        BrowserForward = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_BROWSER_FORWARD
        ''' <summary>	Navigate home.</summary>
        BrowserHome = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_BROWSER_HOME
        ''' <summary>	Refresh page.</summary>
        BrowserRefresh = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_BROWSER_REFRESH
        ''' <summary>Open search.</summary>
        BrowserSearch = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_BROWSER_SEARCH
        ''' <summary>Stop download.</summary>
        BrowserStop = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_BROWSER_STOP
        ''' <summary>	Start App1.</summary>
        LaunchApp1 = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_LAUNCH_APP1
        ''' <summary>	Start App2.</summary>
        LaunchApp2 = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_LAUNCH_APP2
        ''' <summary>	Open mail.</summary>
        LaunchMail = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_LAUNCH_MAIL
        ''' <summary>	Go to Media Select mode.</summary>
        LaunchMediaSelect = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_LAUNCH_MEDIA_SELECT
        ''' <summary>Go to next track.</summary>
        MediaNextTrack = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MEDIA_NEXTTRACK
        ''' <summary>	Play or pause playback.</summary>
        MediaPlayPause = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MEDIA_PLAY_PAUSE
        ''' <summary>Go to previous track.</summary>
        MediaPreviousTrack = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MEDIA_PREVIOUSTRACK
        ''' <summary>	Stop playback.</summary>
        MediaStop = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MEDIA_STOP
        ''' <summary>	Decrease the treble.</summary>
        TrebleDown = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_TREBLE_DOWN
        ''' <summary>	Increase the treble.</summary>
        TrebleUp = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_TREBLE_UP
        ''' <summary>	Lower the volume.</summary>
        VolumeDown = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_VOLUME_DOWN
        ''' <summary>	Mute the volume.</summary>
        Mute = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_VOLUME_MUTE
        ''' <summary>	Raise the volume.</summary>
        VolumeUp = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_VOLUME_UP
        ''' <summary>Windows XP: Mute the microphone.</summary>
        MicrophoneMute = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MICROPHONE_VOLUME_MUTE
        ''' <summary>Windows XP: Decrease microphone volume.</summary>
        MicrophoneVolumeDown = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MICROPHONE_VOLUME_DOWN
        ''' <summary>	Windows XP: Increase microphone volume.</summary>
        MicrophoneVolumeUp = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MICROPHONE_VOLUME_UP

        ''' <summary>Windows XP: Close the window (not the application).</summary>
        Close = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_CLOSE
        ''' <summary>Windows XP: Copy the selection.</summary>
        Copy = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_COPY
        ''' <summary>Windows XP: Brings up the correction list when a word is incorrectly identified during speech input.</summary>
        CorrectionList = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_CORRECTION_LIST
        ''' <summary>Windows XP: Cut the selection.</summary>
        Cut = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_CUT
        ''' <summary>Windows XP: Toggles between two modes of speech input: dictation and command/control (giving commands to an application or accessing menus).</summary>
        DictateOrCommandControlToggle = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_DICTATE_OR_COMMAND_CONTROL_TOGGLE
        ''' <summary>Windows XP: Open the Find dialog.</summary>
        Find = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_FIND
        ''' <summary>Windows XP: Forward a mail message.</summary>
        ForwardMail = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_FORWARD_MAIL
        ''' <summary>Windows XP: Open the Help dialog.</summary>
        Help = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_HELP
        ''' <summary>Windows XP SP1: Decrement the channel value, for example, for a TV or radio tuner.</summary>
        ChannelDown = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MEDIA_CHANNEL_DOWN
        ''' <summary>Windows XP SP1: Increment the channel value, for example, for a TV or radio tuner.</summary>
        ChannelUp = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MEDIA_CHANNEL_UP
        ''' <summary>Windows XP SP1: Increase the speed of stream playback. This can be implemented in many ways, for example, using a fixed speed or toggling through a series of increasing speeds.</summary>
        FastForward = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MEDIA_FASTFORWARD
        ''' <summary>Windows XP SP1: Pause. If already paused, take no further action. This is a direct PAUSE command that has no state. If there are discrete Play and Pause buttons, applications should take action on this command as well as APPCOMMAND_MEDIA_PLAY_PAUSE.</summary>
        MediaPause = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MEDIA_PAUSE
        ''' <summary>Windows XP SP1: Begin playing at the current position. If already paused, it will resume. This is a direct PLAY command that has no state. If there are discrete Play and Pause buttons, applications should take action on this command as well as APPCOMMAND_MEDIA_PLAY_PAUSE.</summary>
        MediaPlay = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MEDIA_PLAY
        ''' <summary>Windows XP SP1: Begin recording the current stream.</summary>
        MediaRecord = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MEDIA_RECORD
        ''' <summary>Windows XP SP1: Go backward in a stream at a higher rate of speed. This can be implemented in many ways, for example, using a fixed speed or toggling through a series of increasing speeds.</summary>
        MediaRewind = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MEDIA_REWIND
        ''' <summary>Windows XP: Toggle the microphone.</summary>
        MicrophoneOnOffToggle = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_MIC_ON_OFF_TOGGLE
        ''' <summary>Windows XP: Create a new window.</summary>
        [New] = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_NEW
        ''' <summary>Windows XP: Open a window.</summary>
        Open = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_OPEN
        ''' <summary>Windows XP: Paste</summary>
        Pase = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_PASTE
        ''' <summary>Windows XP: Print current document.</summary>
        Print = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_PRINT
        ''' <summary>Windows XP: Redo last action.</summary>
        Redo = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_REDO
        ''' <summary>Windows XP: Reply to a mail message.</summary>
        MailReply = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_REPLY_TO_MAIL
        ''' <summary>Windows XP: Save current document.</summary>
        Save = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_SAVE
        ''' <summary>Windows XP: Send a mail message.</summary>
        SendMail = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_SEND_MAIL
        ''' <summary>Windows XP: Initiate a spell check.</summary>
        SpellCheck = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_SPELL_CHECK
        ''' <summary>Windows XP: Undo last action.</summary>
        Undo = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_UNDO
        ''' <summary>Windows Vista: Delete</summary>
        Delete = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_DELETE
        ''' <summary>Windows Vista: Flip 3D (as WinKey+Tab)</summary>
        Flip3D = API.Messages.lParam.WM_APPCOMMANDCommands.APPCOMMAND_DWM_FLIP3D
    End Enum
End Namespace
#End If