Imports System.Runtime.InteropServices
Imports System.Runtime.CompilerServices
Imports System.ComponentModel

Namespace API
    ''' <summary>Contains API declarations related to Raw Input</summary>
    Friend Module RawInput
#Region "Enumeration"
        ''' <summary>contains information about a raw input device.</summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure RAWINPUTDEVICELIST
            ''' <summary>Handle to the raw input device. </summary>
            Public hDevice As IntPtr
            ''' <summary>Type of device.</summary>
            Public dwType As DeviceTypes
            ''' <summary>Size of this structure</summary>
            Public Shared ReadOnly Size As Integer = Marshal.SizeOf(GetType(RAWINPUTDEVICELIST))
            ''' <summary>Gets string representation of this instance</summary>
            Public Overrides Function ToString() As String
                Return String.Format("{0} {1}", dwType, hDevice)
            End Function
        End Structure
        ''' <summary>Raw input device types</summary>
        Public Enum DeviceTypes As UInteger
            ''' <summary>The device is an Human Interface Device (HID) that is not a keyboard and not a mouse.</summary>
            RIM_TYPEHID = 2
            ''' <summary>The device is a keyboard.</summary>
            RIM_TYPEKEYBOARD = 1
            ''' <summary>The device is a mouse.</summary>
            RIM_TYPEMOUSE = 0
        End Enum

        ''' <summary>enumerates the raw input devices attached to the system. </summary>
        ''' <param name="pRawInputDeviceList">[out] Pointer to buffer that holds an array of <see cref="RAWINPUTDEVICELIST"/> structures for the devices attached to the system. If NULL, the number of devices are returned in *<paramref name="puiNumDevices"/>.</param>
        ''' <param name="puiNumDevices">[in, out] Pointer to a variable. If <paramref name="pRawInputDeviceList"/> is NULL, the function populates this variable with the number of devices attached to the system; otherwise, this variable specifies the number of <see cref="RAWINPUTDEVICELIST"/> structures that can be contained in the buffer to which <paramref name="pRawInputDeviceList"/> points. If this value is less than the number of devices attached to the system, the function returns the actual number of devices in this variable and fails with <see cref="Errors.ERROR_INSUFFICIENT_BUFFER"/>.</param>
        ''' <param name="cbSize">[in] Size of a <see cref="RAWINPUTDEVICELIST"/> structure. </param>
        ''' <returns><para>If the function is successful, the return value is the number of devices stored in the buffer pointed to by <paramref name="pRawInputDeviceList"/>.</para>
        ''' <para>On any other error, the function returns (UINT) -1</para></returns>
        ''' <remarks>The devices returned from this function are the mouse, the keyboard, and other Human Interface Device (HID) devices.
        ''' <para>To get result of this function you must call it twice.</para>
        ''' <list type="numbered">
        ''' <item>With (nothing, Number = 0, Size)</item>
        ''' <item>Then prepare your array to have Number elements</item>
        ''' <item>With (YourArray, Number, Size)</item>
        ''' </list></remarks>
        Public Declare Auto Function GetRawInputDeviceList Lib "user32.dll" ( _
            <MarshalAs(UnmanagedType.LPArray, SizeParamIndex:=1, ArraySubType:=UnmanagedType.Struct), [In](), Out()> ByVal pRawInputDeviceList As RAWINPUTDEVICELIST(), _
            <[In](), Out()> ByRef puiNumDevices As Int32, _
            <[In]()> ByVal cbSize As Int32) As Int32

        ''' <summary>gets information about the raw input device</summary>
        ''' <param name="hDevice">[in] Handle to the raw input device. This comes from the lParam of the <see cref="API.Messages.WindowMessages.WM_INPUT"/> message, from RAWINPUTHEADER.hDevice, or from <see cref="GetRawInputDeviceList"/>. It can also be NULL if an application inserts input data, for example, by using SendInput. </param>
        ''' <param name="uiCommand">[in] Specifies what data will be returned in <paramref name="pData"/>.</param>
        ''' <param name="pData">[in, out] Pointer to a buffer that contains the information specified by <paramref name="uiCommand"/>. If <paramref name="uiCommand"/> is <see cref="DeviceInfoTypes.RIDI_DEVICEINFO"/>, set <see cref="RID_DEVICE_INFO.cbSize"/> to sizeof(<see cref="RID_DEVICE_INFO"/>) before calling <see cref="GetRawInputDeviceInfo"/>. </param>
        ''' <param name="pcbSize">[in, out] Pointer to a variable that contains the size, in bytes, of the data in <paramref name="pData"/>. </param>
        ''' <returns><para>If successful, this function returns a non-negative number indicating the number of bytes copied to <paramref name="pData"/>.</para>
        ''' <para>If <paramref name="pData"/> is not large enough for the data, the function returns -1. If <paramref name="pData"/> is NULL, the function returns a value of zero. In both of these cases, <paramref name="pcbSize"/> is set to the minimum size required for the <paramref name="pData"/> buffer.</para>
        ''' <para>Call GetLastError to identify any other errors.</para></returns>
        <DllImport("User32.dll")> _
        Public Function GetRawInputDeviceInfo(ByVal hDevice As IntPtr, ByVal uiCommand As DeviceInfoTypes, _
            ByVal pData As IntPtr, ByRef pcbSize As UInteger) As Integer
        End Function
        '''' <summary>gets information about the raw input device</summary>
        '''' <param name="hDevice">[in] Handle to the raw input device. This comes from the lParam of the <see cref="API.Messages.WindowMessages.WM_INPUT"/> message, from RAWINPUTHEADER.hDevice, or from <see cref="GetRawInputDeviceList"/>. It can also be NULL if an application inserts input data, for example, by using SendInput. </param>
        '''' <param name="uiCommand">[in] Specifies what data will be returned in <paramref name="pData"/>.</param>
        '''' <param name="pData">[in, out] Pointer to a buffer that contains the information specified by <paramref name="uiCommand"/>. If <paramref name="uiCommand"/> is <see cref="DeviceInfoTypes.RIDI_DEVICEINFO"/>, set <see cref="RID_DEVICE_INFO.cbSize"/> to sizeof(<see cref="RID_DEVICE_INFO"/>) before calling <see cref="GetRawInputDeviceInfo"/>. </param>
        '''' <param name="pcbSize">[in, out] Pointer to a variable that contains the size, in bytes, of the data in <paramref name="pData"/>. </param>
        '''' <returns><para>If successful, this function returns a non-negative number indicating the number of bytes copied to <paramref name="pData"/>.</para>
        '''' <para>If <paramref name="pData"/> is not large enough for the data, the function returns -1. If <paramref name="pData"/> is NULL, the function returns a value of zero. In both of these cases, <paramref name="pcbSize"/> is set to the minimum size required for the <paramref name="pData"/> buffer.</para>
        '''' <para>Call GetLastError to identify any other errors.</para></returns>
        '<DllImport("User32.dll")> _
        'Public Function GetRawInputDeviceInfo( _
        '   ByVal hDevice As IntPtr, _
        '   ByVal uiCommand As DeviceInfoTypes, _
        '   <MarshalAs(UnmanagedType.LPStruct)> ByVal pData As RID_DEVICE_INFO, _
        '   ByRef pcbSize As UInteger) As Integer
        'End Function




        ''' <summary>Specifies different types of information that can be queried using <see cref="GetRawInputDeviceInfo"/></summary>
        Public Enum DeviceInfoTypes As UInteger
            ''' <summary>pData points to the previously parsed data.</summary>
            RIDI_PREPARSEDDATA = &H20000005
            ''' <summary>pData points to a string that contains the device name.</summary>
            ''' <remarks>For this uiCommand only, the value in pcbSize is the character count (not the byte count).</remarks>
            RIDI_DEVICENAME = &H20000007
            ''' <summary>pData points to an RID_DEVICE_INFO structure.</summary>
            RIDI_DEVICEINFO = &H2000000B
        End Enum

        'Adopted from http://topic.csdn.net/t/20041028/14/3499749.html

        ''' <summary>Defines the raw input data coming from the specified mouse.</summary>
        ''' <remarks>For the keyboard, the Usage Page is 1 and the Usage is 2.</remarks>
        <StructLayout(LayoutKind.Sequential, Size:=RID_DEVICE_INFO_MOUSE.Size)> _
       Public Structure RID_DEVICE_INFO_MOUSE
            ''' <summary>ID for the mouse device.</summary>
            Public dwId As Integer
            ''' <summary>Number of buttons for the mouse.</summary>
            Public dwNumberOfButtons As Integer
            ''' <summary>Number of data points per second. This information may not be applicable for every mouse device.</summary>
            Public dwSampleRate As Integer
            ''' <summary>TRUE if the mouse has a wheel for horizontal scrolling; otherwise, FALSE.</summary>
            ''' <remarks>Note:  This member is only supported under Windows Vista and later versions.</remarks>
            Public fHasHorizontalWheel As Boolean
            ''' <summary>Size of this structure</summary>
            Public Const Size% = 16
        End Structure
        ''' <summary>The raw input data coming from the specified keyboard. </summary>
        ''' <remarks>For the keyboard, the Usage Page is 1 and the Usage is 6. </remarks>
        <StructLayout(LayoutKind.Sequential, Size:=RID_DEVICE_INFO_KEYBOARD.Size)> _
        Public Structure RID_DEVICE_INFO_KEYBOARD
            ''' <summary>Type of the keyboard. </summary>
            Public dwType As Integer
            ''' <summary>Subtype of the keyboard. </summary>
            Public dwSubType As Integer
            ''' <summary>Scan code mode. </summary>
            Public dwKeyboardMode As Integer
            ''' <summary>Number of function keys on the keyboard.</summary>
            Public dwNumberOfFunctionKeys As Integer
            ''' <summary>Number of LED indicators on the keyboard.</summary>
            Public dwNumberOfIndicators As Integer
            ''' <summary>Total number of keys on the keyboard. </summary>
            Public dwNumberOfKeysTotal As Integer
            ''' <summary>Size of this structure</summary>
            Public Const Size% = 24
        End Structure
        ''' <summary>Defines the raw input data coming from the specified Human Interface Device (HID)</summary>
        <StructLayout(LayoutKind.Sequential, Size:=RID_DEVICE_INFO_HID.Size)> _
        Public Structure RID_DEVICE_INFO_HID
            ''' <summary>Vendor ID for the HID. </summary>
            Public dwVendorId As Integer
            ''' <summary>Product ID for the HID. </summary>
            Public dwProductId As Integer
            ''' <summary>Version number for the HID. </summary>
            Public dwVersionNumber As Integer
            ''' <summary>Top-level collection Usage Page for the device. </summary>
            Public usUsagePage As UShort
            ''' <summary>Top-level collection Usage for the device. </summary>
            Public usUsage As UShort
            ''' <summary>Size of this structure</summary>
            Public Const Size% = 16
        End Structure
        ''' <summary>defines the raw input data coming from any device.</summary>
        <StructLayout(LayoutKind.Explicit, Size:=RID_DEVICE_INFO.Size)> _
        Public Structure RID_DEVICE_INFO
            ''' <summary>Size, in bytes, of the RID_DEVICE_INFO structure. </summary>
            <FieldOffset(0)> _
            Public cbSize As Integer
            ''' <summary>Type of raw input data. This member can be one of the following values.</summary>
            <FieldOffset(4)> _
            Public dwType As DeviceTypes
            ''' <summary>If <see cref="dwType"/> is <see cref="DeviceTypes.RIM_TYPEMOUSE"/>, this is the <see cref="RID_DEVICE_INFO_MOUSE"/> structure that defines the mouse. </summary>
            <FieldOffset(8)> _
            Public mouse As RID_DEVICE_INFO_MOUSE
            ''' <summary>If <see cref="dwType"/> is <see cref="DeviceTypes.RIM_TYPEKEYBOARD"/>, this is the <see cref="RID_DEVICE_INFO_KEYBOARD"/> structure that defines the keyboard. </summary>
            <FieldOffset(8)> _
            Public keyboard As RID_DEVICE_INFO_KEYBOARD
            ''' <summary>If <see cref="dwType"/> is <see cref="DeviceTypes.RIM_TYPEHID"/>, this is the <see cref="RID_DEVICE_INFO_HID"/> structure that defines the HID device. </summary>
            <FieldOffset(8)> _
            Public hid As RID_DEVICE_INFO_HID
            ''' <summary>Size of this structure</summary>
            Public Const Size% = 32
        End Structure
#End Region
#Region "Registration"
        ''' <summary>defines information for the raw input devices</summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure RAWINPUTDEVICE 'ASAP: MSDN
            ''' <summary>Top level collection Usage page for the raw input device. </summary>
            ''' <seelaso cref="DevicesT.RawInputT.UsagePages"/>
            <MarshalAs(UnmanagedType.U2)> _
            Public usUsagePage As UShort
            ''' <summary>Top level collection Usage for the raw input device. </summary>
            <MarshalAs(UnmanagedType.U2)> _
            Public usUsage As UShort
            ''' <summary>Mode flag that specifies how to interpret the information provided by usUsagePage and usUsage. It can be zero (the default) or one of the following values. By default, the operating system sends raw input from devices with the specified top level collection (TLC) to the registered application as long as it has the window focus.</summary>
            ''' <remarks><para>If <see cref="RAWINPUTDEVICEFlags.RIDEV_NOLEGACY"/> is set for a mouse or a keyboard, the system does not generate any legacy message for that device for the application. For example, if the mouse TLC is set with <see cref="RAWINPUTDEVICEFlags.RIDEV_NOLEGACY"/>, <see cref="API.Messages.WindowMessages.WM_LBUTTONDOWN"/> and related legacy mouse messages are not generated. Likewise, if the keyboard TLC is set with <see cref="RAWINPUTDEVICEFlags.RIDEV_NOLEGACY"/>, <see cref="API.Messages.WindowMessages.WM_KEYDOWN"/> and related legacy keyboard messages are not generated.</para>
            ''' <para>If <see cref="RAWINPUTDEVICEFlags.RIDEV_REMOVE"/> is set and the <see cref="hwndTarget"/> parameter is not set to NULL, then parameter validation will fail.</para></remarks>
            <MarshalAs(UnmanagedType.U4)> _
            Public dwFlags As RAWINPUTDEVICEFlags
            ''' <summary>Handle to the target window. If NULL it follows the keyboard focus.</summary>
            Public hwndTarget As IntPtr
        End Structure
        ''' <summary>Values for the <see cref="RAWINPUTDEVICE.dwFlags"/> field</summary>
        <Flags()> _
        Public Enum RAWINPUTDEVICEFlags As Integer
            ''' <summary>Microsoft Windows XP Service Pack 1 (SP1): If set, the application command keys are handled. <see cref="RIDEV_APPKEYS"/> can be specified only if <see cref="RIDEV_NOLEGACY"/> is specified for a keyboard device.</summary>
            RIDEV_APPKEYS = &H400
            ''' <summary>If set, the mouse button click does not activate the other window.</summary>
            RIDEV_CAPTUREMOUSE = &H200
            ''' <summary>If set, this specifies the top level collections to exclude when reading a complete usage page. This flag only affects a TLC whose usage page is already specified with <see cref="RIDEV_PAGEONLY"/>.</summary>
            RIDEV_EXCLUDE = &H10
            ''' <summary>Windows Vista or later: If set, this enables the caller to receive input in the background only if the foreground application does not process it. In other words, if the foreground application is not registered for raw input, then the background application that is registered will receive the input.</summary>
            RIDEV_EXINPUTSINK = &H1000
            ''' <summary>If set, this enables the caller to receive the input even when the caller is not in the foreground. Note that <see cref="RAWINPUTDEVICE.hwndTarget"/> must be specified.</summary>
            RIDEV_INPUTSINK = &H100
            ''' <summary>If set, the application-defined keyboard device hotkeys are not handled. However, the system hotkeys; for example, ALT+TAB and CTRL+ALT+DEL, are still handled. By default, all keyboard hotkeys are handled. <see cref="RIDEV_NOHOTKEYS"/> can be specified even if <see cref="RIDEV_NOLEGACY"/> is not specified and <see cref="RAWINPUTDEVICE.hwndTarget"/> is NULL.</summary>
            RIDEV_NOHOTKEYS = &H200
            ''' <summary>If set, this prevents any devices specified by <see cref="RAWINPUTDEVICE.usUsagePage"/> or <see cref="RAWINPUTDEVICE.usUsage"/> from generating legacy messages. This is only for the mouse and keyboard. See Remarks.</summary>
            RIDEV_NOLEGACY = &H30
            ''' <summary>If set, this specifies all devices whose top level collection is from the specified <see cref="RAWINPUTDEVICE.usUsagePage"/>. Note that <see cref="RAWINPUTDEVICE.usUsage"/> must be zero. To exclude a particular top level collection, use <see cref="RIDEV_EXCLUDE"/>.</summary>
            RIDEV_PAGEONLY = &H20
            ''' <summary>If set, this removes the top level collection from the inclusion list. This tells the operating system to stop reading from a device which matches the top level collection.</summary>
            RIDEV_REMOVE = &H1
            '            RIDEV_DEVNOTIFY = &H2000
            '            RIDEV_EXMODEMASK = &HF0

            '#define RIDEV_EXMODE(mode) ((mode) & RIDEV_EXMODEMASK)
        End Enum
        ''' <summary>registers the devices that supply the raw input data.</summary>
        ''' <param name="pRawInputDevice">[in] Pointer to an array of <see cref="RAWINPUTDEVICE"/> structures that represent the devices that supply the raw input.</param>
        ''' <param name="uiNumDevices">[in] Number of <see cref="RAWINPUTDEVICE"/> structures pointed to by pRawInputDevices.</param>
        ''' <param name="cbSize">[in] Size, in bytes, of a <see cref="RAWINPUTDEVICE"/> structure.</param>
        ''' <returns>TRUE if the function succeeds; otherwise, FALSE.</returns>
        ''' <remarks><para>To receive <see cref="API.Messages.WindowMessages.WM_INPUT"/> messages, an application must first register the raw input devices using <see cref="RegisterRawInputDevices"/>. By default, an application does not receive raw input.</para>
        ''' <para>If a <see cref="RAWINPUTDEVICE"/> structure has the <see cref="RAWINPUTDEVICEFlags.RIDEV_REMOVE"/> flag set and the <see cref="RAWINPUTDEVICE.hwndTarget"/> parameter is not set to NULL, then parameter validation will fail.</para></remarks>
        Public Declare Auto Function RegisterRawInputDevices Lib "User32.dll" (ByVal pRawInputDevice As RAWINPUTDEVICE(), ByVal uiNumDevices As UInteger, ByVal cbSize As UInteger) As Boolean 'ASAP: MSDN

        ''' <summary>gets the information about the raw input devices for the current application.</summary>
        ''' <param name="pRawInputDevices">[out] Pointer to an array of <see cref="RAWINPUTDEVICE"/> structures for the application.</param>
        ''' <param name="puiNumDevices">[in, out] Number of <see cref="RAWINPUTDEVICE"/> structures in * <paramref name="pRawInputDevices"/>. </param>
        ''' <param name="cbSize">[in] Size, in bytes, of a <see cref="RAWINPUTDEVICE"/> structure. </param>
        ''' <returns><para>If successful, the function returns a non-negative number that is the number of <see cref="RAWINPUTDEVICE"/> structures written to the buffer.</para>
        ''' <para>If the <paramref name="pRawInputDevices"/> buffer is too small or NULL, the function sets the last error as <see cref="api.Common.Errors.ERROR_INSUFFICIENT_BUFFER"/>, returns -1, and sets <paramref name="puiNumDevices"/> to the required number of devices. If the function fails for any other reason, it returns -1.</para></returns>
        Public Declare Function GetRegisteredRawInputDevices Lib "user32.dll" (ByVal pRawInputDevices As IntPtr, ByRef puiNumDevices As UInteger, ByVal cbSize As UInteger) As Integer 'ASAP: MSDN
#End Region
#Region "Handling"
        ''' <summary>contains the raw input from a device. </summary>
        ''' <remarks>Use this structure for marshalling, but remember that is does not contain the <see cref="RAWHID_NonMarshalling.bRawData"/> member</remarks>
        <StructLayout(LayoutKind.Explicit)> _
        Public Structure RAWINPUT_Marshalling   'ASAP: MSDN
            ''' <summary>A <see cref="RAWINPUTHEADER"/> structure for the raw input data. </summary>
            <FieldOffset(0)> Public header As RAWINPUTHEADER
            ''' <summary>If the data comes from a mouse, this is the <see cref="RAWMOUSE"/> structure for the raw input data. </summary>
            <FieldOffset(RAWINPUTHEADER.Size)> Public mouse As RAWMOUSE
            ''' <summary>If the data comes from a keyboard, this is the <see cref="RAWKEYBOARD"/> structure for the raw input data. </summary>
            <FieldOffset(RAWINPUTHEADER.Size)> Public keyboard As RAWKEYBOARD
            ''' <summary>If the data comes from an Human Interface Device (HID), this is the <see cref="RAWINPUT_Marshalling"/> structure for the raw input data. </summary>
            <FieldOffset(RAWINPUTHEADER.Size)> Public hid As RAWHID_Marshalling
        End Structure
        ''' <summary>contains the raw input from a device. </summary>
        ''' <remarks>Do not use this structure for marshalling</remarks>
        Public Structure RAWINPUT_NonMarshalling
            ''' <summary>A <see cref="RAWINPUTHEADER"/> structure for the raw input data. </summary>
            Public header As RAWINPUTHEADER
            ''' <summary>If the data comes from a mouse, this is the <see cref="RAWMOUSE"/> structure for the raw input data. </summary>
            Public mouse As RAWMOUSE
            ''' <summary>If the data comes from a keyboard, this is the <see cref="RAWKEYBOARD"/> structure for the raw input data. </summary>
            Public keyboard As RAWKEYBOARD
            ''' <summary>If the data comes from an Human Interface Device (HID), this is the <see cref="RAWINPUT_NonMarshalling"/> structure for the raw input data. </summary>
            Public hid As RAWHID_NonMarshalling
            ''' <summary>Converts <see cref="RAWINPUT_Marshalling"/> structure to <see cref="RAWINPUT_NonMarshalling"/> structure</summary>
            ''' <param name="a">A <see cref="RAWINPUT_Marshalling"/> structure</param>
            ''' <returns><see cref="RAWINPUT_NonMarshalling"/> structure</returns>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="a"/>.<see cref="RAWINPUT_Marshalling.header">header</see>.<see cref="RAWINPUTHEADER.dwType">dwType</see> is not member of <see cref="DeviceTypes"/></exception>
            ''' <exception cref="NotSupportedException"><paramref name="a"/>.<see cref="RAWINPUT_Marshalling.header">header</see>.<see cref="RAWINPUTHEADER.dwType">dwType</see> is <see cref="DeviceTypes.RIM_TYPEHID"/></exception>
            Public Shared Narrowing Operator CType(ByVal a As RAWINPUT_Marshalling) As RAWINPUT_NonMarshalling
                Dim ret As New RAWINPUT_NonMarshalling
                ret.header = a.header
                Select Case ret.header.dwType
                    Case DeviceTypes.RIM_TYPEHID : Throw New NotSupportedException(ResourcesT.ExceptionsWin.CannotConvertRAWINPUTMarshallingToRAWINPUTNonMarshalling)
                    Case DeviceTypes.RIM_TYPEKEYBOARD : ret.keyboard = a.keyboard
                    Case DeviceTypes.RIM_TYPEMOUSE : ret.mouse = a.mouse
                    Case Else : Throw New InvalidEnumArgumentException("a.header.dwType", a.header.dwType, a.header.dwType.GetType)
                End Select
                Return ret
            End Operator
        End Structure
        ''' <summary>contains information about the state of the mouse. </summary>
        <StructLayout(LayoutKind.Explicit)> _
        Public Structure RAWMOUSE             'ASAP: MSDN
            ''' <summary>Mouse state. This member can be any reasonable combination of <see cref="RAWMOUSEFlags"/>. </summary>
            <FieldOffset(0)> Public usFlags As RAWMOUSEFlags
            ''' <summary>Reserved</summary>
            <FieldOffset(2)> Public ulButtons As ULong
            ''' <summary>Transition state of the mouse buttons. This member can be one or more of the <see cref="RAWMOUSEButtonFlags"/> values. </summary>
            <FieldOffset(2)> Public usButtonFlags As RAWMOUSEButtonFlags
            ''' <summary>If <see cref="usButtonFlags"/> is <see cref="RAWMOUSEButtonFlags.RI_MOUSE_WHEEL"/>, this member is a signed value that specifies the wheel delta. </summary>
            <FieldOffset(4)> Public usButtonData As UShort
            ''' <summary>Raw state of the mouse buttons. </summary>
            <FieldOffset(10)> Public ulRawButtons As ULong
            ''' <summary>Motion in the X direction. This is signed relative motion or absolute motion, depending on the value of <see cref="usFlags"/>. </summary>
            <FieldOffset(18)> Public lLastX As Long
            ''' <summary>Motion in the Y direction. This is signed relative motion or absolute motion, depending on the value of <see cref="usFlags"/>. </summary>
            <FieldOffset(26)> Public lLastY As Long
            ''' <summary>Device-specific additional information for the event. </summary>
            <FieldOffset(34)> Public ulExtraInformation As ULong
        End Structure
        ''' <summary>Values for <see cref="RAWMOUSE.usFlags"/></summary>
        <Flags()> _
        Public Enum RAWMOUSEFlags As UShort
            ''' <summary>Mouse attributes changed; application needs to query the mouse attributes.</summary>
            MOUSE_ATTRIBUTES_CHANGED = 4
            ''' <summary>Mouse movement data is relative to the last mouse position.</summary>
            MOUSE_MOVE_RELATIVE = 0
            ''' <summary>Mouse movement data is based on absolute position.</summary>
            MOUSE_MOVE_ABSOLUTE = 1
            ''' <summary>Mouse coordinates are mapped to the virtual desktop (for a multiple monitor system).</summary>
            MOUSE_VIRTUAL_DESKTOP = 2
        End Enum
        ''' <summary>Values for <see cref="RAWMOUSE.usButtonFlags"/></summary>
        <Flags()> _
        Public Enum RAWMOUSEButtonFlags As UShort
            ''' <summary>Left button changed to down.</summary>
            RI_MOUSE_LEFT_BUTTON_DOWN = 1
            ''' <summary>Left button changed to up.</summary>
            RI_MOUSE_LEFT_BUTTON_UP = 2
            ''' <summary>Middle button changed to down.</summary>
            RI_MOUSE_MIDDLE_BUTTON_DOWN = &H10
            ''' <summary>Middle button changed to up.</summary>
            RI_MOUSE_MIDDLE_BUTTON_UP = &H20
            ''' <summary>Right button changed to down.</summary>
            RI_MOUSE_RIGHT_BUTTON_DOWN = 4
            ''' <summary>Right button changed to up.</summary>
            RI_MOUSE_RIGHT_BUTTON_UP = 8
            ''' <summary><see cref="RI_MOUSE_LEFT_BUTTON_DOWN"/></summary>
            RI_MOUSE_BUTTON_1_DOWN
            ''' <summary><see cref="RI_MOUSE_LEFT_BUTTON_UP"/></summary>
            RI_MOUSE_BUTTON_1_UP
            ''' <summary><see cref="RI_MOUSE_RIGHT_BUTTON_DOWN"/></summary>
            RI_MOUSE_BUTTON_2_DOWN
            ''' <summary><see cref="RI_MOUSE_RIGHT_BUTTON_UP"/></summary>
            RI_MOUSE_BUTTON_2_UP
            ''' <summary><see cref="RI_MOUSE_MIDDLE_BUTTON_DOWN"/></summary>
            RI_MOUSE_BUTTON_3_DOWN
            ''' <summary><see cref="RI_MOUSE_MIDDLE_BUTTON_UP"/></summary>
            RI_MOUSE_BUTTON_3_UP
            ''' <summary>XBUTTON1 changed to down.</summary>
            RI_MOUSE_BUTTON_4_DOWN = &H40
            ''' <summary>XBUTTON1 changed to up.</summary>
            RI_MOUSE_BUTTON_4_UP = &H80
            ''' <summary>XBUTTON2 changed to down.</summary>
            RI_MOUSE_BUTTON_5_DOWN = &H100
            ''' <summary>XBUTTON2 changed to up.</summary>
            RI_MOUSE_BUTTON_5_UP = &H200
            ''' <summary>Raw input comes from a mouse wheel. The wheel delta is stored in <see cref="RAWMOUSE.usButtonData"/>.</summary>
            RI_MOUSE_WHEEL = &H400
        End Enum

        ''' <summary>The make scan code for keyboard overrun</summary>
        Public Const KEYBOARD_OVERRUN_MAKE_CODE As UShort = &HFF

        ''' <summary>contains information about the state of the keyboard. </summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure RAWKEYBOARD     'ASAP: MSDN
            ''' <summary>Scan code from the key depression. The scan code for keyboard overrun is <see cref="KEYBOARD_OVERRUN_MAKE_CODE"/>. </summary>
            Public MakeCode As UShort
            ''' <summary>Flags for scan code information. It can be one or more of the <see cref="RAWKEYBOARDFlags"/>.</summary>
            Public Flags As RAWKEYBOARDFlags
            ''' <summary>Reserved; must be zero. </summary>
            Public Reserved As UShort
            ''' <summary>Microsoft Windows message compatible virtual-key code. For more information, see Virtual-Key Codes. </summary>
            Public VKey As UShort
            ''' <summary>Corresponding window message, for example <see cref="Messages.WindowMessages.WM_KEYDOWN"/>, <see cref="Messages.WindowMessages.WM_SYSKEYDOWN"/>, and so forth. </summary>
            Public Message As Messages.WindowMessages
            ''' <summary></summary>
            Public ExtraInformation As ULong
        End Structure
        ''' <summary>Values of <see cref="RAWKEYBOARD.Flags"/></summary>
        <Flags()> _
        Public Enum RAWKEYBOARDFlags As UShort
            ''' <summary>Make code</summary>
            RI_KEY_MAKE = 0
            ''' <summary>Break code</summary>
            RI_KEY_BREAK = 1
            ''' <summary>E0</summary>
            RI_KEY_E0 = 2
            ''' <summary>E1</summary>
            RI_KEY_E1 = 4
            ''' <summary>Set led</summary>
            RI_KEY_TERMSRV_SET_LED = 8
            ''' <summary>Termibal server shadow</summary>
            RI_KEY_TERMSRV_SHADOW = &H10
        End Enum
        ''' <summary>describes the format of the raw input from a Human Interface Device (HID). </summary>
        ''' <remarks>Use this declaration of RAWHID structure for marshalling, but rememebre that it is missing <see cref="RAWHID_NonMarshalling.bRawData"/> member.</remarks>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure RAWHID_Marshalling
            'ASAP: MSDN
            'For marshalling purposes this structure cannot be declared identically as it is declarred in C++ at this page. This is because of dynamic-lenght arrays are not supported for marshalling as fields in structures. And event if there would be a way how to marshall dynamic-lenght arrays inside structures, the way in which this structure is used makes marshalling more difficult than usual. RAWHID structure is usually used as field inside RAWINPUT structure. This field is overlapped with another fields (of type RAWMOUSE and RAWKEYBOARD). Marshalling does not support overlapping of reference-type and value-type fields. All fields in both other structures are value-type, but bRawData is array - refernce type in .NET. So, this structure must be, for marshalling purposes, declared without bRawData and this field must be marshalled manually.  For example of manual marshalling from unmanaged to managed code see GetRawInputData function.
            'The marshalling declaration is:
            '<StructLayout(LayoutKind.Sequential)> _
            'Public Structure RAWHID_Marshalling
            '   Public dwSizeHid As Integer
            '   Public dwCount As Integer
            'End Structure
            'The non-marshalling declaration is:
            'Public Structure RAWHID_NonMarshalling
            '   Public dwSizeHid As Integer
            '   Public dwCount As Integer
            '   Public bRawData As Byte()
            'End Structure
            'But it is usefull only when you want to keep RAWHID structure in managed memory in the same form as it is kept in unmanaged memory.
            ''' <summary>Size, in bytes, of each HID input in <see cref="RAWHID_NonMarshalling.bRawData"/>. </summary>
            Public dwSizeHid As Integer
            ''' <summary>Number of HID inputs in <see cref="RAWHID_NonMarshalling.bRawData"/>.</summary>
            Public dwCount As Integer
            '''' <summary>Raw input data as an array of bytes. </summary>
            '''' <remarks>This field is not marshalled automatically</remarks>
            '<MarshalAs(UnmanagedType.ByValArray, SizeConst:=0)> _
            'Public bRawData As Byte()
        End Structure
        ''' <summary>describes the format of the raw input from a Human Interface Device (HID). </summary>
        ''' <remarks>Do not use this structure for marshalling</remarks>
        Public Structure RAWHID_NonMarshalling
            ''' <summary>Size, in bytes, of each HID input in <see cref="bRawData"/>. </summary>
            Public dwSizeHid As Integer
            ''' <summary>Number of HID inputs in <see cref="bRawData"/>.</summary>
            Public dwCount As Integer
            ''' <summary>Raw input data as an array of bytes. </summary>
            ''' <remarks>This field is not marshalled automatically</remarks>
            Public bRawData As Byte()
        End Structure

        ''' <summary>contains the header information that is part of the raw input data. </summary>
        <StructLayout(LayoutKind.Sequential, Size:=RAWINPUTHEADER.Size)> _
        Public Structure RAWINPUTHEADER   'ASAP: MSDN
            ''' <summary>Type of raw input. It can be one of the following values.</summary>
            Public dwType As DeviceTypes
            ''' <summary>Size, in bytes, of the entire input packet of data. This includes <see cref="RAWINPUT"/> plus possible extra input reports in the <see cref="RAWHID_NonMarshalling"/> variable length array. </summary>
            Public dwSize As Integer
            ''' <summary>Handle to the device generating the raw input data. </summary>
            Public hDevice As IntPtr
            ''' <summary>Value passed in the wParam parameter of the <see cref="API.Messages.WindowMessages.WM_INPUT"/> message. </summary>
            Public wParam As API.Messages.wParam.WM_INPUT
            ''' <summary>Size of this structure</summary>
            Public Const Size% = 16
        End Structure

        ''' <summary>gets the input code from <paramref name="wParam"/> in <see cref="API.Messages.WindowMessages.WM_INPUT"/>.</summary>
        ''' <param name="wParam">Input code.</param>
        ''' <returns>The return value is the input code for the raw input data.</returns>
        <Extension()> _
        Public Function GET_RAWINPUT_CODE_WPARAM(ByVal wParam As API.Messages.wParam.WM_INPUT) As API.Messages.wParam.WM_INPUT
            Return wParam And &HFF
        End Function
        ''' <summary>gets the raw input from the specified device.</summary>
        ''' <param name="hRawInput">[in] Handle to the <see cref="RAWINPUT"/> structure. This comes from the lParam in <see cref="API.Messages.WindowMessages.WM_INPUT"/>. </param>
        ''' <param name="uiCommnad">[in] Command flag. This parameter can be one of the <see cref="GetRawInputDataCommand"/> values. </param>
        ''' <param name="pData">[out] Pointer to the data that comes from the <see cref="RAWINPUT"/> structure. This depends on the value of <paramref name="uiCommand"/>. If <paramref name="pData"/> is NULL, the required size of the buffer is returned in <paramref name="pcbSize"/>. </param>
        ''' <param name="pcbSize">[in, out] Pointer to a variable that specifies the size, in bytes, of the data in <paramref name="pData"/>. </param>
        ''' <param name="cbSizeHeader">[in] Size, in bytes, of <see cref="RAWINPUTHEADER"/>. </param>
        ''' <returns><para>If <paramref name="pData"/> is NULL and the function is successful, the return value is 0. If <paramref name="pData"/> is not NULL and the function is successful, the return value is the number of bytes copied into <paramref name="pData"/>.</para>
        ''' <para>If there is an error, the return value is (UINT)-1.</para></returns>
        ''' <remarks><see cref="GetRawInputData"/> gets the raw input one <see cref="RAWINPUT"/> structure at a time. In contrast, GetRawInputBuffer gets an array of <see cref="RAWINPUT"/> structures.</remarks>
        Public Declare Function GetRawInputData Lib "user32.dll" ( _
            ByVal hRawInput As IntPtr, _
            ByVal uiCommnad As GetRawInputDataCommand, _
            ByVal pData As IntPtr, _
            ByRef pcbSize As UInteger, _
            ByVal cbSizeHeader As UInteger) _
            As Integer     'ASAP: MSDN


        '''' <summary>does a buffered read of the raw input data.</summary>
        '''' <param name="pData">[out] Pointer to a buffer of <see cref="RAWINPUT"/> structures that contain the raw input data. If NULL, the minimum required buffer, in bytes, is returned in *<paramref name="pcbSize"/>. </param>
        '''' <param name="pcbSize">[in, out] Pointer to a variable that specifies the size, in bytes, of a <see cref="RAWINPUT"/> structure. </param>
        '''' <param name="cbSizeHeader">[in] Size, in bytes, of <see cref="RAWINPUTHEADER"/>. </param>
        '''' <returns><para>If <paramref name="pData"/> is NULL and the function is successful, the return value is zero. If <paramref name="pData"/> is not NULL and the function is successful, the return value is the number of <see cref="RAWINPUT"/> structures written to <paramref name="pData"/>.</para>
        '''' <para>If an error occurs, the return value is (UINT)-1.</para></returns>
        '''' <remarks><para>Using <see cref="GetRawInputBuffer"/>, the raw input data is buffered in the array of <see cref="RAWINPUT"/> structures. For an unbuffered read, use the GetMessage function to read in the raw input data.</para>
        '''' <para>The <see cref="NEXTRAWINPUTBLOCK"/> macro allows an application to traverse an array of <see cref="RAWINPUT"/> structures.</para></remarks>
        'Public Declare Function GetRawInputBuffer Lib "user32.dll" (ByVal pData As IntPtr, ByRef pcbSize As UInteger, ByVal cbSizeHeader As UInteger) As Integer

        ''' <summary>Commands for <see cref="GetRawInputData"/> function</summary>
        Public Enum GetRawInputDataCommand As UInteger
            ''' <summary>Get the raw data from the <see cref="RAWINPUT"/> structure.</summary>
            RID_INPUT = &H10000003
            ''' <summary>Get the header information from the <see cref="RAWINPUT"/> structure.</summary>
            RID_HEADER = &H10000005
        End Enum
        '        ''' <summary>gets the location of the next structure in an array of <see cref="RAWINPUT"/> structures. </summary>
        '        ''' <param name="pRawInput">Pointer to a structure in an array of <see cref="RAWINPUT"/> structures. </param>
        '        ''' <returns>The return value is a pointer to the next structure in the array of <see cref="RAWINPUT"/> structures.</returns>
        '        ''' <remarks>This macro is called repeatedly to traverse an array of <see cref="RAWINPUT"/> structures.</remarks>
        '        Public Function NEXTRAWINPUTBLOCK(ByVal pRawInput As IntPtr) As IntPtr
        '            '((PRAWINPUT)RAWINPUT_ALIGN((ULONG_PTR)((PBYTE)(ptr) + (ptr)->header.dwSize)))
        '            Dim thisHeader As RAWINPUTHEADER = Marshal.PtrToStructure(pRawInput, GetType(RAWINPUTHEADER))
        '            Return RAWINPUT_ALIGN(pRawInput.ToInt64 + thisHeader.dwSize)
        '        End Function
        '        ''' <summary>Implements the <see cref="RAWINPUT_ALIGN"/> macro</summary>
        '        ''' <param name="x">Pointer to alin after addition</param>
        '        ''' <returns>Aligned pointer</returns>
        '        Public Function RAWINPUT_ALIGN(ByVal x As IntPtr) As IntPtr
        '            If IntPtr.Size = 4 Then '32
        '                Return (CInt(x.ToInt32) + Marshal.SizeOf(GetType(Integer)) - 1) And Not CInt(Marshal.SizeOf(GetType(Integer)) - 1)
        '            Else '64
        '                Return (CLng(x.ToInt64) + Marshal.SizeOf(GetType(Long)) - 1) And Not CLng(Marshal.SizeOf(GetType(Long)) - 1)
        '            End If
        '        End Function
#End Region
    End Module
End Namespace