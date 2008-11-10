Imports System.Runtime.InteropServices

Namespace API
    ''' <summary>Contains API declarations related to Raw Input</summary>
    Friend Module RawInput
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

        ''' <summary>defines information for the raw input devices</summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure RAWINPUTDEVICE
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
        Public Declare Auto Function RegisterRawInputDevices Lib "User32.dll" (ByVal pRawInputDevice As RAWINPUTDEVICE(), ByVal uiNumDevices As UInteger, ByVal cbSize As UInteger) As Boolean

    End Module
End Namespace