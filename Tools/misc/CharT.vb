#If Config <= RC Then 'Stage:RC
''' <summary>Contains character constants</summary>
''' <version version="1.5.3">Module vas mad public</version>
Public Module Chars
    ''' <summary>Carriage return (CR) character (\r, code 13 = 0xD)</summary>
    ''' <seealso cref="vbCrLf"/>
    Public Const Cr As Char = vbCr
    ''' <summary>Line feed (LF) caharcter (\n, code 10 = 0xA)</summary>
    ''' <seealso cref="vbLf"/>
    Public Const Lf As Char = vbLf
    ''' <summary>Null character (code 0)</summary>
    ''' <seeaso cref="vbNullChar"/>
    Public Const NullChar As Char = vbNullChar
    ''' <summary>Horizontal tabulator character (\t, code 9)</summary>
    ''' <seeaso cref="vbTab"/>
    Public Const Tab As Char = vbTab
    ''' <summary>Vertical tabulator character (\v, code 11 = 0xB)</summary>
    ''' <seealso cref="vbVerticalTab"/>
    Public Const VerticalTab As Char = vbVerticalTab
    ''' <summary>Backspace character (\b, code 8)</summary>
    ''' <seealso cref="vbBack"/>
    Public Const Back As Char = vbBack
    ''' <summary>Alert character (\a, code 7)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const Alert As Char = ChrW(&H7)
    ''' <summary>Form Feed character (\f, code 0xC)</summary>
    ''' <seealso cref="vbFormFeed"/>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const FormFeed As Char = vbFormFeed
    ''' <summary>Escape character (\e, code 0x1B)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const Escape As Char = ChrW(&H1B)
    ''' <summary>Delete character (code 0x7F)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const Delete As Char = ChrW(&H7F)

    ''' <summary>Control character Start Of Header (SOH, 0x1)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const StartOfHeader As Char = ChrW(&H1)
    ''' <summary>Control character Start Of Text (STX, 0x1)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const StartOfText As Char = ChrW(&H2)
    ''' <summary>Control character End Of text (ETX, 0x3)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const EndOfText As Char = ChrW(&H3)
    ''' <summary>Control character End Of Transmission (EOT, 0x4)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const EndOfTransmission As Char = ChrW(&H4)
    ''' <summary>Control character Enquiry (ENQ, 0x5)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const Enquiry As Char = ChrW(&H5)
    ''' <summary>Control character Acknowledge (ACK, 0x6)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const Acknowledge As Char = ChrW(&H6)
    ''' <summary>Control character Shift Out (SO, 0xE)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const ShiftOut As Char = ChrW(&HE)
    ''' <summary>Control character Shift In (SI, 0xF)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const ShiftIn As Char = ChrW(&HF)
    ''' <summary>Control character data Link Escape (DLE, 0x10)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const DataLinkEscape As Char = ChrW(&H10)
    ''' <summary>Control character Device Control One (DC1, 0x11)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const DeviceControl1 As Char = ChrW(&H11)
    ''' <summary>Control character Device Control Two (DC2, 0x12)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const DeviceControl2 As Char = ChrW(&H12)
    ''' <summary>Control character Device Control Three (DC1, 0x13)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const DeviceControl3 As Char = ChrW(&H13)
    ''' <summary>Control character Device Control Four (DC4, 0x11)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const DeviceControl4 As Char = ChrW(&H14)
    ''' <summary>Control character Negative Acknowledge (NAK, 0x15)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const NegativeAcknowledge As Char = ChrW(&H15)
    ''' <summary>Control character Synchronous Idle (SYN, 0x16)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const SynchronousIdle As Char = ChrW(&H16)
    ''' <summary>Control character End of Transmission Block (ETB, 0x17)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const EndOfTransmissionBlock As Char = ChrW(&H17)
    ''' <summary>Control character Cancel (CAN, 0x18)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const Cancel As Char = ChrW(&H18)
    ''' <summary>Control character End of Medium (EM, 0x19)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const EndOfMedium As Char = ChrW(&H19)
    ''' <summary>Control character Substitute (SUB, 0x1A)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const Substitute As Char = ChrW(&H1A)

    ''' <summary>Control character FileSeparator (FS, 0x1C)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const FileSeparator As Char = ChrW(&H1C)
    ''' <summary>Control character Group Separator (GS, 0x1D)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const GroupSeparator As Char = ChrW(&H1D)
    ''' <summary>Control character Record Separator (RS, 0x1E)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const RecordSeparator As Char = ChrW(&H1E)
    ''' <summary>Control character Unit Separator (US, 0x1F)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const UnitSeparator As Char = ChrW(&H1F)

    ''' <summary>Unicode character Next Line (NEL, 0x85)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const NextLine As Char = ChrW(&H85)
    ''' <summary>Unicode character Line Separator (LS, 0x2028)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const LineSeparator As Char = ChrW(&H2028)
    ''' <summary>Unicode character Paragraph Separator (PS, 0x2029)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const ParagraphSeparator As Char = ChrW(&H2029)

    ''' <summary>Unicode character No-break space (NBSP, 0xA0)</summary>
    ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
    Public Const NoBreakSpace As Char = ChrW(&HA0)
End Module
#End If