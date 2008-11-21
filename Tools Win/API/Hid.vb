Imports System.Runtime.InteropServices
Imports Microsoft.Win32.SafeHandles

Namespace API
    ''' <summary>Contains declarations related to hid.dll library for Human Interface Devices</summary>
    Friend Module Hid
        ''' <summary>used to specify a HID report type.</summary>
        Public Enum HIDP_REPORT_TYPE As Integer
            ''' <summary>Indicates an input report.</summary>
            HidP_Input = 0
            ''' <summary>Indicates an output report.</summary>
            HidP_Output = 1
            ''' <summary>Indicates a feature report.</summary>
            HidP_Feature = 2
        End Enum

        '<StructLayout(LayoutKind.Sequential)> _
        'Friend Structure HIDD_ATTRIBUTES
        '    Friend Size As Int32
        '    Friend VendorID As Int16
        '    Friend ProductID As Int16
        '    Friend VersionNumber As Int16
        'End Structure

        ''' <summary>contains information about a top-level collection's capability.</summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure HIDP_CAPS    'ASAP:MSDN
            ''' <summary>Specifies a top-level collection's usage ID.</summary>
            Public Usage As Int16
            ''' <summary>Specifies the top-level collection's usage page.</summary>
            ''' <seelaso cref="DevicesT.RawInputT.UsagePages"/>
            Public UsagePage As Int16
            ''' <summary>Specifies the maximum size, in bytes, of all the input reports (including the report ID, if report IDs are used, which is prepended to the report data).</summary>
            Public InputReportByteLength As Int16
            ''' <summary>Specifies the maximum size, in bytes, of all the output reports (including the report ID, if report IDs are used, which is prepended to the report data).</summary>
            Public OutputReportByteLength As Int16
            ''' <summary>Specifies the maximum length, in bytes, of all the feature reports (including the report ID, if report IDs are used, which is prepended to the report data).</summary>
            Public FeatureReportByteLength As Int16
            ''' <summary>Reserved for internal system use.</summary>
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=17)> Dim Reserved() As Int16
            ''' <summary>Specifies the number of HIDP_LINK_COLLECTION_NODE structures that are returned for this top-level collection by HidP_GetLinkCollectionNodes.</summary>
            Public NumberLinkCollectionNodes As Int16
            ''' <summary>Specifies the number of input <see cref="HIDP_BUTTON_CAPS"/> structures that <see cref="HidP_GetButtonCaps"/> returns.</summary>
            Public NumberInputButtonCaps As Int16
            ''' <summary>Specifies the number of input <see cref="HIDP_VALUE_CAPS"/> structures that <see cref="HidP_GetValueCaps"/> returns.</summary>
            Public NumberInputValueCaps As Int16
            ''' <summary>Specifies the number of data indices assigned to buttons and values in all input reports.</summary>
            Public NumberInputDataIndices As Int16
            ''' <summary>Specifies the number of output <see cref="HIDP_BUTTON_CAPS"/> structures that <see cref="HidP_GetButtonCaps"/> returns.</summary>
            Public NumberOutputButtonCaps As Int16
            ''' <summary>Specifies the number of output <see cref="HIDP_VALUE_CAPS"/> structures that <see cref="HidP_GetValueCaps"/> returns.</summary>
            Public NumberOutputValueCaps As Int16
            ''' <summary>Specifies the number of data indices assigned to buttons and values in all output reports.</summary>
            Public NumberOutputDataIndices As Int16
            ''' <summary>Specifies the total number of feature <see cref="HIDP_BUTTON_CAPS"/> structures that <see cref="HidP_GetButtonCaps"/> returns.</summary>
            Public NumberFeatureButtonCaps As Int16
            ''' <summary>Specifies the total number of feature <see cref="HIDP_VALUE_CAPS"/> structures that <see cref="HidP_GetValueCaps"/> returns.</summary>
            Public NumberFeatureValueCaps As Int16
            ''' <summary>Specifies the number of data indices assigned to buttons and values in all feature reports.</summary>
            Public NumberFeatureDataIndices As Int16
        End Structure

        '' If IsRange is false, UsageMin is the Usage and UsageMax is unused.
        '' If IsStringRange is false, StringMin is the string index and StringMax is unused.
        '' If IsDesignatorRange is false, DesignatorMin is the designator index and DesignatorMax is unused.

        ''' <summary>contains information that describes the capability of a set of HID control values (either a single usage or a usage range).</summary>
        <StructLayout(LayoutKind.Explicit, Pack:=1)> _
        Public Structure HIDP_VALUE_CAPS 'ASAP:MSDN
            ''' <summary>Specifies the usage page of the usage or usage range.</summary>
            ''' <seelaso cref="DevicesT.RawInputT.UsagePages"/>
            <FieldOffset(0)> Public UsagePage As Int16
            ''' <summary>Specifies the report ID of the HID report that contains the usage or usage range.</summary>
            <FieldOffset(2)> Public ReportID As Byte
            ''' <summary>Indicates, if TRUE, that the usage is member of a set of aliased usages. Otherwise, if <see cref="IsAlias"/> is FALSE, the value has only one usage.</summary>
            <FieldOffset(3), MarshalAs(UnmanagedType.Bool)> Public IsAlias As Boolean
            ''' <summary>Contains the data fields (one or two bytes) associated with an input, output, or feature main item.</summary>
            <FieldOffset(7)> Public BitField As Int16
            ''' <summary>Specifies the index of the link collection in a top-level collection's link collection array that contains the usage or usage range. If <see cref="LinkCollection"/> is zero, the usage or usage range is contained in the top-level collection.</summary>
            <FieldOffset(9)> Public LinkCollection As Int16
            ''' <summary>Specifies the usage of the link collection that contains the usage or usage range. If <see cref="LinkCollection"/> is zero, <see cref="LinkUsage"/> specifies the usage of the top-level collection.</summary>
            <FieldOffset(11)> Public LinkUsage As Int16
            ''' <summary>Specifies the usage page of the link collection that contains the usage or usage range. If <see cref="LinkCollection"/> is zero, <see cref="LinkUsagePage"/> specifies the usage page of the top-level collection.</summary>
            ''' <seelaso cref="DevicesT.RawInputT.UsagePages"/>
            <FieldOffset(13)> Public LinkUsagePage As Int16
            ''' <summary>Specifies, if TRUE, that the structure describes a usage range. Otherwise, if <see cref="IsRange"/> is FALSE, the structure describes a single usage.</summary>
            <FieldOffset(15), MarshalAs(UnmanagedType.Bool)> Public IsRange As Boolean
            ''' <summary>Specifies, if TRUE, that the usage or usage range has a set of string descriptors. Otherwise, if <see cref="IsStringRange"/> is FALSE, the usage or usage range has zero or one string descriptor.</summary>
            <FieldOffset(19), MarshalAs(UnmanagedType.Bool)> Public IsStringRange As Boolean
            ''' <summary>Specifies, if TRUE, that the usage or usage range has a set of designators. Otherwise, if <see cref="IsDesignatorRange"/> is FALSE, the usage or usage range has zero or one designator.</summary>
            <FieldOffset(23), MarshalAs(UnmanagedType.Bool)> Public IsDesignatorRange As Boolean
            ''' <summary>Specifies, if TRUE, that the usage or usage range provides absolute data. Otherwise, if <see cref="IsAbsolute"/> is FALSE, the value is the change in state from the previous value.</summary>
            <FieldOffset(27), MarshalAs(UnmanagedType.Bool)> Public IsAbsolute As Boolean
            ''' <summary>Specifies, if TRUE, that the usage supports a NULL value, which indicates that the data is not valid and should be ignored. Otherwise, if <see cref="HasNull"/> is FALSE, the usage does not have a NULL value.</summary>
            <FieldOffset(31), MarshalAs(UnmanagedType.Bool)> Public HasNull As Boolean
            ''' <summary>Reserved for internal system use.</summary>
            <FieldOffset(35)> Private Reserved As Byte
            ''' <summary>Specifies the size, in bits, of a usage's data field in a report. If <see cref="ReportCount"/> is greater than one, each usage has a separate data field of this size.</summary>
            <FieldOffset(36)> Public BitSize As Int16
            ''' <summary>Specifies the number of usages that this structure describes.</summary>
            <FieldOffset(38)> Public ReportCount As Int16
            ''' <summary>Reserved for internal system use.</summary>
            <FieldOffset(40), MarshalAs(UnmanagedType.ByValArray, SizeConst:=5, arraysubtype:=UnmanagedType.I2)> Private Reserved2 As Int16()
            ''' <summary>Specifies the usage's exponent, as described by the USB HID standard.</summary>
            <FieldOffset(50)> Public UnitsExp As UInt32
            ''' <summary>Specifies the usage's units, as described by the USB HID Standard.</summary>
            <FieldOffset(54)> Public Units As UInt32
            ''' <summary>Specifies a usage's signed lower bound.</summary>
            <FieldOffset(58)> Public LogicalMin As Int32
            ''' <summary>Specifies a usage's signed upper bound.</summary>
            <FieldOffset(62)> Public LogicalMax As Int32
            ''' <summary>Specifies a usage's signed lower bound after scaling is applied to the logical minimum value.</summary>
            <FieldOffset(66)> Public PhysicalMin As Int32
            ''' <summary>Specifies a usage's signed upper bound after scaling is applied to the logical maximum value.</summary>
            <FieldOffset(70)> Public PhysicalMax As Int32
            ''' <summary>Specifies, if <see cref="IsRange"/> is TRUE, information about a usage range. Otherwise, if <see cref="IsRange"/> is FALSE, <see cref="NotRange"/> contains information about a single usage.</summary>
            <FieldOffset(74)> Public Range As RangeStruct
            ''' <summary>Specifies, if <see cref="IsRange"/> is FALSE, information about a single usage. Otherwise, if <see cref="IsRange"/> is TRUE, <see cref="Range"/> contains information about a usage range.</summary>
            <FieldOffset(74)> Public NotRange As NotRangeStruct
            ''' <summary>Type of <see cref="Range"/> field</summary>
            <StructLayout(LayoutKind.Sequential)> _
            Public Structure RangeStruct
                ''' <summary>Indicates the inclusive lower bound of usage range whose inclusive upper bound is specified by <see cref="UsageMax"/>.</summary>
                Public UsageMin As Int16
                ''' <summary>Indicates the inclusive upper bound of a usage range whose inclusive lower bound is indicated by <see cref="UsageMin"/>.</summary>
                Public UsageMax As Int16
                ''' <summary>Indicates the inclusive lower bound of a range of string descriptors (specified by string minimum and string maximum items) whose inclusive upper bound is indicated by <see cref="StringMax"/>.</summary>
                Public StringMin As Int16
                ''' <summary>Indicates the inclusive upper bound of a range of string descriptors (specified by string minimum and string maximum items) whose inclusive lower bound is indicated by <see cref="StringMax"/>.</summary>
                Public StringMax As Int16
                ''' <summary>Indicates the inclusive lower bound of a range of designators (specified by designator minimum and designator maximum items) whose inclusive upper bound is indicated by <see cref="DesignatorMax"/>.</summary>
                Public DesignatorMin As Int16
                ''' <summary>Indicates the inclusive upper bound of a range of designators (specified by designator minimum and designator maximum items) whose inclusive lower bound is indicated by <see cref="DesignatorMax"/>.</summary>
                Public DesignatorMax As Int16
                ''' <summary>Indicates the inclusive lower bound of a sequential range of data indices that correspond, one-to-one and in the same order, to the usages specified by the usage range <see cref="UsageMin"/> to <see cref="UsageMax"/>.</summary>
                Public DataIndexMin As Int16
                ''' <summary>Indicates the inclusive upper bound of a sequential range of data indices that correspond, one-to-one and in the same order, to the usages specified by the usage range <see cref="UsageMin"/> to <see cref="UsageMax"/>.</summary>
                Public DataIndexMax As Int16
            End Structure
            ''' <summary>Type of <see cref="NotRange"/> field</summary>
            <StructLayout(LayoutKind.Sequential)> _
            Public Structure NotRangeStruct
                ''' <summary>Indicates a usage ID.</summary>
                Public Usage As Int16
                ''' <summary>Reserved for internal system use.</summary>
                Private Reserved1 As Int16
                ''' <summary>Indicates a string descriptor ID for the usage specified by <see cref="Usage"/>.</summary>
                Public StringIndex As Int16
                ''' <summary>Reserved for internal system use.</summary>
                Private Reserved2 As Int16
                ''' <summary>Indicates a designator ID for the usage specified by <see cref="Usage"/>.</summary>
                Public DesignatorIndex As Int16
                ''' <summary>Reserved for internal system use.</summary>
                Private Reserved3 As Int16
                ''' <summary>Indicates the data index of the usage specified by <see cref="Usage"/>.</summary>
                Public DataIndex As Int16
                ''' <summary>Reserved for internal system use.</summary>
                Private Reserved4 As Int16
            End Structure
        End Structure
        ''' <summary>contains information about the capability of a HID control button usage (or a set of buttons associated with a usage range).</summary>
        <StructLayout(LayoutKind.Explicit, Pack:=1)> _
        Public Structure HIDP_BUTTON_CAPS  'ASAP:MSDN
            ''' <summary>Specifies the usage page for a usage or usage range.</summary>
            ''' <seelaso cref="DevicesT.RawInputT.UsagePages"/> 
            <FieldOffset(0)> Public UsagePage As Int16
            ''' <summary>Specifies the report ID of the HID report that contains the usage or usage range.</summary>
            <FieldOffset(2), MarshalAs(UnmanagedType.U1)> Public ReportID As Byte
            ''' <summary>Indicates, if TRUE, that a button has a set of aliased usages. Otherwise, if <see cref="IsAlias"/> is FALSE, the button has only one usage.</summary>
            <FieldOffset(3), MarshalAs(UnmanagedType.Bool)> Public IsAlias As Boolean
            ''' <summary>Contains the data fields (one or two bytes) associated with an input, output, or feature main item.</summary>
            <FieldOffset(7)> Public BitField As Int16
            ''' <summary>Specifies the index of the link collection in a top-level collection's link collection array that contains the usage or usage range. If <see cref="LinkCollection"/> is zero, the usage or usage range is contained in the top-level collection.</summary>
            <FieldOffset(9)> Public LinkCollection As Int16
            ''' <summary>Specifies the usage of the link collection that contains the usage or usage range. If <see cref="LinkCollection"/> is zero, <see cref="LinkUsage"/> specifies the usage of the top-level collection.</summary>
            <FieldOffset(11)> Public LinkUsage As Int16
            ''' <summary>Specifies the usage page of the link collection that contains the usage or usage range. If <see cref="LinkCollection"/> is zero, <see cref="LinkUsagePage"/> specifies the usage page of the top-level collection.</summary>
            ''' <seelaso cref="DevicesT.RawInputT.UsagePages"/>
            <FieldOffset(13)> Public LinkUsagePage As Int16
            ''' <summary>Specifies, if TRUE, that the structure describes a usage range. Otherwise, if <see cref="IsRange"/> is FALSE, the structure describes a single usage.</summary>
            <FieldOffset(15), MarshalAs(UnmanagedType.Bool)> Public IsRange As Boolean
            ''' <summary>Specifies, if TRUE, that the usage or usage range has a set of string descriptors. Otherwise, if <see cref="IsStringRange"/> is FALSE, the usage or usage range has zero or one string descriptor.</summary>
            <FieldOffset(19), MarshalAs(UnmanagedType.Bool)> Public IsStringRange As Boolean
            ''' <summary>Specifies, if TRUE, that the usage or usage range has a set of designators. Otherwise, if <see cref="IsDesignatorRange"/> is FALSE, the usage or usage range has zero or one designator.</summary>
            <FieldOffset(23), MarshalAs(UnmanagedType.Bool)> Public IsDesignatorRange As Boolean
            ''' <summary>Specifies, if TRUE, that the button usage or usage range provides absolute data. Otherwise, if <see cref="IsAbsolute"/> is FALSE, the button data is the change in state from the previous value.</summary>
            <FieldOffset(27), MarshalAs(UnmanagedType.Bool)> Public IsAbsolute As Boolean
            ''' <summary>Contains element 0 of <see cref="Reserved2"/> pseudo-array</summary>
            <FieldOffset(31)> Private r0 As Integer
            ''' <summary>Contains element 1 of <see cref="Reserved2"/> pseudo-array</summary>
            <FieldOffset(35)> Private r1 As Integer
            ''' <summary>Contains element 2 of <see cref="Reserved2"/> pseudo-array</summary>
            <FieldOffset(39)> Private r2 As Integer
            ''' <summary>Contains element 3 of <see cref="Reserved2"/> pseudo-array</summary>
            <FieldOffset(43)> Private r3 As Integer
            ''' <summary>Contains element 4 of <see cref="Reserved2"/> pseudo-array</summary>
            <FieldOffset(47)> Private r4 As Integer
            ''' <summary>Contains element 5 of <see cref="Reserved2"/> pseudo-array</summary>
            <FieldOffset(51)> Private r5 As Integer
            ''' <summary>Contains element 6 of <see cref="Reserved2"/> pseudo-array</summary>
            <FieldOffset(55)> Private r6 As Integer
            ''' <summary>Contains element 7 of <see cref="Reserved2"/> pseudo-array</summary>
            <FieldOffset(59)> Private r7 As Integer
            ''' <summary>Contains element 8 of <see cref="Reserved2"/> pseudo-array</summary>
            <FieldOffset(63)> Private r8 As Integer
            ''' <summary>Contains element 9 of <see cref="Reserved2"/> pseudo-array</summary>
            <FieldOffset(67)> Private r9 As Integer
            ''' <summary>Mimics fixed-size array of integers used by this unmanaged structure</summary>
            ''' <param name="Index">Index from 0 to 9</param>
            ''' <returns>Pseudo-array element</returns>
            ''' <value>New value to store in pseudo-array</value>
            ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Index"/> is not within range 0÷9</exception>
            Private Property Reserved2(ByVal Index As Integer) As Integer
                Get
                    Select Case Index
                        Case 0 : Return r0
                        Case 1 : Return r1
                        Case 2 : Return r2
                        Case 3 : Return r3
                        Case 4 : Return r4
                        Case 5 : Return r5
                        Case 6 : Return r6
                        Case 7 : Return r7
                        Case 8 : Return r8
                        Case 9 : Return r9
                        Case Else : Throw New ArgumentOutOfRangeException("Index", Index)
                    End Select
                End Get
                Set(ByVal value As Integer)
                    Select Case Index
                        Case 0 : r0 = value
                        Case 1 : r1 = value
                        Case 2 : r2 = value
                        Case 3 : r3 = value
                        Case 4 : r4 = value
                        Case 5 : r5 = value
                        Case 6 : r6 = value
                        Case 7 : r7 = value
                        Case 8 : r8 = value
                        Case 9 : r9 = value
                        Case Else : Throw New ArgumentOutOfRangeException("Index", Index)
                    End Select
                End Set
            End Property
            ''' <summary>    Specifies, if <see cref="IsRange"/> is TRUE, information about a usage range. Otherwise, if <see cref="IsRange"/> is FALSE, <see cref="NotRange"/> contains information about a single usage.</summary>
            <FieldOffset(71)> Public Range As HIDP_VALUE_CAPS.RangeStruct
            ''' <summary>Specifies, if <see cref="IsRange"/> is FALSE, information about a single usage. Otherwise, if <see cref="IsRange"/> is TRUE, <see cref="Range"/> contains information about a usage range.</summary>
            <FieldOffset(71)> Public NotRange As HIDP_VALUE_CAPS.NotRangeStruct
        End Structure


        '<DllImport("hid.dll", SetLastError:=True)> _
        'Shared Function HidD_FlushQueue _
        '    (ByVal HidDeviceObject As SafeFileHandle) _
        '    As Boolean
        'End Function

        'ASAP: MSDN
        ''' <summary>releases the resources that the HID class driver allocated to hold a top-level collection's preparsed data.</summary>
        ''' <param name="PreparsedData">Pointer to the buffer, returned by <see cref="HidD_GetPreparsedData"/>, that is freed.</param>
        ''' <returns>TRUE if it succeeds. Otherwise, it returns FALSE if the buffer was not a preparsed data buffer.</returns>
        Public Declare Function HidD_FreePreparsedData Lib "hid.dll" (ByVal PreparsedData As IntPtr) As Boolean

        '<DllImport("hid.dll", SetLastError:=True)> _
        'Shared Function HidD_GetAttributes _
        ' (ByVal HidDeviceObject As SafeFileHandle, _
        ' ByRef Attributes As HIDD_ATTRIBUTES) _
        ' As Boolean
        'End Function

        '<DllImport("hid.dll", SetLastError:=True)> _
        'Shared Function HidD_GetFeature _
        ' (ByVal HidDeviceObject As SafeFileHandle, _
        ' ByVal lpReportBuffer() As Byte, _
        ' ByVal ReportBufferLength As Int32) _
        ' As Boolean
        'End Function

        '<DllImport("hid.dll", SetLastError:=True)> _
        'Shared Function HidD_GetInputReport _
        ' (ByVal HidDeviceObject As SafeFileHandle, _
        ' ByVal lpReportBuffer() As Byte, _
        ' ByVal ReportBufferLength As Int32) _
        ' As Boolean
        'End Function

        '<DllImport("hid.dll", SetLastError:=True)> _
        'Shared Sub HidD_GetHidGuid _
        '    (ByRef HidGuid As System.Guid)
        'End Sub

        '<DllImport("hid.dll", SetLastError:=True)> _
        'Shared Function HidD_GetNumInputBuffers _
        '    (ByVal HidDeviceObject As SafeFileHandle, _
        '    ByRef NumberBuffers As Int32) _
        '    As Boolean
        'End Function

        'ASAP: MSDN
        ''' <summary>returns a top-level collection's preparsed data.</summary>
        ''' <param name="HidDeviceObject">Specifies an open handle to a top-level collection.</param>
        ''' <param name="PreparsedData">Pointer to the address of a routine-allocated buffer that contains a collection's preparsed data in a _HIDP_PREPARSED_DATA structure.</param>
        ''' <returns>TRUE if it succeeds; otherwise, it returns FALSE.</returns>
        ''' <remarks>When an application no longer requires the preparsed data, it should call <see cref="HidD_FreePreparsedData"/> to free the preparsed data buffer.</remarks>
        Public Declare Function HidD_GetPreparsedData Lib "hid.dll" (ByVal HidDeviceObject As SafeFileHandle, ByRef PreparsedData As IntPtr) As Boolean


        '<DllImport("hid.dll", SetLastError:=True)> _
        'Shared Function HidD_SetFeature _
        ' (ByVal HidDeviceObject As SafeFileHandle, _
        ' ByVal lpReportBuffer() As Byte, _
        ' ByVal ReportBufferLength As Int32) _
        ' As Boolean
        'End Function

        '<DllImport("hid.dll", SetLastError:=True)> _
        'Shared Function HidD_SetNumInputBuffers _
        '    (ByVal HidDeviceObject As SafeFileHandle, _
        '    ByVal NumberBuffers As Int32) _
        '    As Boolean
        'End Function

        '<DllImport("hid.dll", SetLastError:=True)> _
        'Shared Function HidD_SetOutputReport _
        '    (ByVal HidDeviceObject As SafeFileHandle, _
        '    ByRef lpReportBuffer As Byte, _
        '    ByVal ReportBufferLength As Int32) _
        '    As Boolean
        'End Function

        ''' <summary>returns a top-level collection's <see cref="HIDP_CAPS"/> structure.</summary>
        ''' <param name="PreparsedData">Pointer to a top-level collection's preparsed data.</param>
        ''' <param name="Capabilities">Pointer to a caller-allocated buffer that the routine uses to return a collection's <see cref="HIDP_CAPS"/> structure.</param>
        ''' <returns>One of the following values: <see cref="HidErrorCode.HIDP_STATUS_SUCCESS"/>, <see cref="HidErrorCode.HIDP_STATUS_INVALID_PREPARSED_DATA"/>.</returns>
        Public Declare Function HidP_GetCaps Lib "hid.dll" (ByVal PreparsedData As IntPtr, ByRef Capabilities As HIDP_CAPS) As HidErrorCode

        Private Const FACILITY_HID_ERROR_CODE% = &H11

        ''' <summary>Error codes returned by some HID functions</summary>
        Public Enum HidErrorCode As Int32
            ''' <summary>The routine successfully returned the collection capability information.</summary>
            HIDP_STATUS_SUCCESS = (&H0I << 28) Or (FACILITY_HID_ERROR_CODE% << 16) Or &H0
            ''' <summary>The specified preparsed data is invalid.</summary>
            HIDP_STATUS_INVALID_PREPARSED_DATA = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &H1
            'HIDP_STATUS_NULL = (&H8I << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &H1
            'HIDP_STATUS_INVALID_REPORT_TYPE = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &H2
            'HIDP_STATUS_INVALID_REPORT_LENGTH = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &H3
            'HIDP_STATUS_USAGE_NOT_FOUND = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &H4
            'HIDP_STATUS_VALUE_OUT_OF_RANGE = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &H5
            'HIDP_STATUS_BAD_LOG_PHY_VALUES = (&HCI << 28) Or (facility_hid_error_code << 16) Or &H6
            'HIDP_STATUS_BUFFER_TOO_SMALL = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &H7
            'HIDP_STATUS_INTERNAL_ERROR = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &H8
            'HIDP_STATUS_I8042_TRANS_UNKNOWN = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &H9
            'HIDP_STATUS_INCOMPATIBLE_REPORT_ID = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &HA
            'HIDP_STATUS_NOT_VALUE_ARRAY = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &HB
            'HIDP_STATUS_IS_VALUE_ARRAY = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &HC
            'HIDP_STATUS_DATA_INDEX_NOT_FOUND = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &HD
            'HIDP_STATUS_DATA_INDEX_OUT_OF_RANGE = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &HE
            'HIDP_STATUS_BUTTON_NOT_PRESSED = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &HF
            'HIDP_STATUS_REPORT_DOES_NOT_EXIST = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &H10
            'HIDP_STATUS_NOT_IMPLEMENTED = (&HCI << 28) Or (FACILITY_HID_ERROR_CODE << 16) Or &H20
            'HIDP_STATUS_I8242_TRANS_UNKNOWN = HIDP_STATUS_I8042_TRANS_UNKNOWN
        End Enum


        ''' <summary>returns a value capability array that describes all the HID control values in a top-level collection for a specified type of HID report.</summary>
        ''' <param name="ReportType">Specifies a <see cref="HIDP_REPORT_TYPE"/> enumerator value that identifies the report type.</param>
        ''' <param name="ValueCaps">Pointer to a caller-allocated buffer in which the routine returns a value capability array for the specified report type.</param>
        ''' <param name="ValueCapsLength">Specifies the length, on input, in array elements, of the <paramref name="ValueCaps"/> buffer. On output, the routine sets <paramref name="ValueCapsLength"/> to the number of elements that the it actually returns.</param>
        ''' <param name="PreparsedData">Pointer to a top-level collection's preparsed data.</param>
        ''' <returns>One of the following values: <see cref="HidErrorCode.HIDP_STATUS_SUCCESS"/>, <see cref="HidErrorCode.HIDP_STATUS_INVALID_PREPARSED_DATA"/>.</returns>
        Public Declare Function HidP_GetValueCaps Lib "hid.dll" _
            (ByVal ReportType As HIDP_REPORT_TYPE, _
             ByVal ValueCaps As IntPtr, _
            ByRef ValueCapsLength As Int16, _
            ByVal PreparsedData As IntPtr) _
            As HidErrorCode            'ASAP:MSDN
        ''' <summary>returns a button capability array that describes all the HID control buttons in a top-level collection for a specified type of HID report.</summary>
        ''' <param name="ReportType">Specifies a <see cref="HIDP_REPORT_TYPE"/> enumerator value that identifies the report type.</param>
        ''' <param name="ButtonCaps">Pointer to a caller-allocated buffer that the routine uses to return a button capability array for the specified report type.</param>
        ''' <param name="ButtonCapsLength">Specifies the length on input, in array elements, of the buffer provided at ButtonCaps. On output, this parameter is set to the actual number of elements that the routine returns.</param>
        ''' <param name="PreparsedData">Pointer to a top-level collection's preparsed data.</param>
        ''' <returns>One of the following values: <see cref="HidErrorCode.HIDP_STATUS_SUCCESS"/>, <see cref="HidErrorCode.HIDP_STATUS_INVALID_PREPARSED_DATA"/>.</returns>
        Public Declare Function HidP_GetButtonCaps Lib "hid.dll" _
            (ByVal ReportType As HIDP_REPORT_TYPE, _
             ByVal ButtonCaps As IntPtr, _
            ByRef ButtonCapsLength As Int16, _
            ByVal PreparsedData As IntPtr) _
            As HidErrorCode   'ASAP:MSDN
    End Module
End Namespace
