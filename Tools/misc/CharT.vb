#If True Then
''' <summary>Contains character constants</summary>
''' <version version="1.5.3">Module was made public</version>
Public Module Chars
    ''' <summary>Carriage return (CR) character (\r, code 13 = 0xD)</summary>
    ''' <seealso cref="vbCrLf"/>
    Public Const Cr As Char = vbCr
    ''' <summary>Line feed (LF) character (\n, code 10 = 0xA)</summary>
    ''' <seealso cref="vbLf"/>
    ''' <version version="1.5.10">Fixed typo in XML comment</version>
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

    ''' <summary>Unicode character Left-To-Right Embedding (LRE, 0x202A)</summary>
    ''' <version version="1.5.4">This constant is new in version 1.5.4</version>
    Public Const LeftToRightEmbedding As Char = ChrW(&H202A)
    ''' <summary>Unicode character Right-To-Left Embedding (RLE, 0x202B)</summary>
    ''' <version version="1.5.4">This constant is new in version 1.5.4</version>
    Public Const RightToLeftEmbedding As Char = ChrW(&H202B)
    ''' <summary>Unicode character Pop Directional Formatting (LRE, 0x202C)</summary>
    ''' <version version="1.5.4">This constant is new in version 1.5.4</version>
    Public Const PopDirectionalFormatting As Char = ChrW(&H202C)
    ''' <summary>Unicode character Left-To-Right Override (LRO, 0x202D)</summary>
    ''' <version version="1.5.4">This constant is new in version 1.5.4</version>
    Public Const LeftToRightOverride As Char = ChrW(&H202D)
    ''' <summary>Unicode character Right-To-Left Override (RLO, 0x202E)</summary>
    ''' <version version="1.5.4">This constant is new in version 1.5.4</version>
    Public Const RightToLeftOverride As Char = ChrW(&H202E)
    ''' <summary>Unicode character Left-To-Right Mark (LRM, 0x200E)</summary>
    ''' <version version="1.5.4">This constant is new in version 1.5.4</version>
    Public Const LeftToRightMark As Char = ChrW(&H200E)
    ''' <summary>Unicode character Right-To-Left Mark (RLM, 0x200F)</summary>
    ''' <version version="1.5.4">This constant is new in version 1.5.4</version>
    Public Const RightToLeftMark As Char = ChrW(&H200F)
    ''' <summary>Unicode character Arabic Letter Mark (ALM, 0x061C)</summary>
    ''' <version version="1.5.10">This constant is new in version 1.5.10</version>
    Public Const ArabicLetterMark As Char = ChrW(&H61C)
    ''' <summary>Unicode character Left-To-Right Isolate (LRI, 0x2066)</summary>
    ''' <version version="1.5.10">This constant is new in version 1.5.10</version>
    Public Const LeftToRightIsolate As Char = ChrW(&H2066)
    ''' <summary>Unicode character Right-To-Left Isolate (RLI, 0x2067)</summary>
    ''' <version version="1.5.10">This constant is new in version 1.5.10</version>
    Public Const RightToLeftIsolate As Char = ChrW(&H2067)
    ''' <summary>Unicode character First Strong Isolate (FSI, 0x2068)</summary>
    ''' <version version="1.5.10">This constant is new in version 1.5.10</version>
    Public Const FirstStrongIsolateIsolate As Char = ChrW(&H2068)
    ''' <summary>Unicode character Pop Directional Isolate (PDI, 0x2069)</summary>
    ''' <version version="1.5.10">This constant is new in version 1.5.10</version>
    Public Const PopDirectionalIsolate As Char = ChrW(&H2069)

    ''' <summary>Unicode character  Inhibit Symmetric Swapping(0x206A)</summary>
    ''' <version version="1.5.10">This constant is new in version 1.5.10</version>
    Public Const InhibitSymmetricSwapping As Char = ChrW(&H206A)
    ''' <summary>Unicode character Activate Symmetric Swapping (0x206B)</summary>
    ''' <version version="1.5.10">This constant is new in version 1.5.10</version>
    Public Const ActivateSymmetricSwapping As Char = ChrW(&H206B)
    ''' <summary>Unicode character Inhibit Arabic Form Shaping (0x206C)</summary>
    ''' <version version="1.5.10">This constant is new in version 1.5.10</version>
    Public Const InhibitArabicFormShaping As Char = ChrW(&H206C)
    ''' <summary>Unicode character Activate Arabic Form Shaping (0x206D)</summary>
    ''' <version version="1.5.10">This constant is new in version 1.5.10</version>
    Public Const ActivateArabicFormShaping As Char = ChrW(&H206D)
    ''' <summary>Unicode character National Digit Shapes (0x206E)</summary>
    ''' <version version="1.5.10">This constant is new in version 1.5.10</version>
    Public Const NationalDigitShapes As Char = ChrW(&H206E)
    ''' <summary>Unicode character NominalDigitShapes (0x206F)</summary>
    ''' <version version="1.5.10">This constant is new in version 1.5.10</version>
    Public Const NominalDigitShapes As Char = ChrW(&H206F)

    ''' <summary>Unicode character Zero Width Non-Joiner (ZWNJ, 0x200C)</summary>
    ''' <version version="1.5.4">This constant is new in version 1.5.4</version>
    Public Const ZeroWidthNonJoiner As Char = ChrW(&H200C)
    ''' <summary>Unicode character Zero Width Joiner (ZWJ, 0x200D)</summary>
    ''' <version version="1.5.4">This constant is new in version 1.5.4</version>
    Public Const ZeroWidthJoiner As Char = ChrW(&H200D)
    ''' <summary>Unicode character Word Joiner (WJ, 0x2060)</summary>
    ''' <version version="1.5.4">This constant is new in version 1.5.4</version>
    Public Const WordJoiner As Char = ChrW(&H2060)
    ''' <summary>Unicode character Narrow No-Break Space (NNBSP, 0x202F)</summary>
    ''' <version version="1.5.10">This constant is new in version 1.5.10</version>
    Public Const NarrowNoBreakSpace As Char = ChrW(&H202F)
End Module
#End If