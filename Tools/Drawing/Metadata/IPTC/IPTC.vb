Namespace DrawingT.MetadataT
#If Congig <= Nightly Then 'Stage: Nightly
    'ASAP:Wiki, Forum, Mark
    Partial Public Class IPTC
        ''' <summary>Describes IPTC dataset's (tag) properties</summary>
        Public Class IPTCTag
            Private _Number As Byte
            Private _Record As RecordNumbers
            Private _Name As String
            Private _HumanName As String
            Private _Type As IPTCTypes
            Private _Enum As Type
            Private _Mandatory As Boolean
            Private _Repeatable As Boolean
            Private _Fixed As Boolean
            Private _Length As Short
            Private _Category As String
            Private _Description As String
        End Class
#Region "IPTC types"
        ''' <summary>Types od data used by IPTC tags</summary>
        Public Enum IPTCTypes
            ''' <summary>Unsigned binary number of unknown length (represented by <see cref="ULong"/>)</summary>
            UnsignedBinaryNumber
            ''' <summary>Binary stored boolean value (can be stored in multiple bytes. If any of bytes is nonzero, value is true) (represented by <see cref="Boolean"/></summary>
            Boolean_Binary
            ''' <summary>Binary stored 1 byte long unsigned integer (represented by <see cref="Byte"/></summary>
            Byte_Binary
            ''' <summary>Binary stored 2 byte long unsigned integer (represented by <see cref="UShort"/></summary>
            UShort_Binary
            ''' <summary>Number of variable length stored as string.</summary>
            ''' <remarks>
            ''' <list type="table"><listheader><term>Length up to characters</term><description>Represented by</description></listheader>
            ''' <item><term>2</term><description><see cref="Byte"/></description></item>
            ''' <item><term>4</term><description><see cref="Short"/></description></item>
            ''' <item><term>9</term><description><see cref="Integer"/></description></item>
            ''' <item><term>19</term><description><see cref="Long"/></description></item>
            ''' <item><term>29</term><description><see cref="Decimal"/></description></item>
            ''' <item><term>unknown</term><description><see cref="Long"/></description></item>
            ''' </list>
            ''' </remarks>
            NumericChar
            ''' <summary>Grahic characters (no whitespaces, no control characters) (represented by <see cref="String"/>)</summary>
            GraphicCharacters
            ''' <summary>Graphic characters and spaces (no tabs, no CR, no LF, no control characters) (represented by <see cref="String"/>)</summary>
            TextWithSpaces
            ''' <summary>Printable text (no tabs, no control characters) (represented by <see cref="String"/>)</summary>
            Text
            ''' <summary>Black and white bitma with width 460px (represented <see cref="Drawing.Bitmap"/>)</summary>
            BW460
            ''' <summary>Enumeration stored as binary number (represented by various enums)</summary>
            Enum_Binary
            ''' <summary>Enumeration stored as numeric string (represented by various enums)</summary>
            Enum_NumChar
            ''' <summary>Date stored as numeric characters in the YYYYMMDD format (represented by <see cref="Date"/>)</summary>
            CCYYMMDD
            ''' <summary>Date stored as numeric characters in the YYYYMMDD format (represented by <see cref="OmmitableDate"/>) Each component YYYY, MM and DD can be set to 0 is unknown</summary>
            CCYYMMDDOmmitable
            ''' <summary>Time stored as numeric characters (and the ± sign) in format HHMMSS±HHMM (with time-zone offset from UTC) (represented by <see cref="Time"/></summary>
            HHMMSS_HHMM
            ''' <summary>Generic array of bytes (represented by array of <see cref="Byte"/></summary>
            ByteArray
            'TODO:UNO
            UNO
            'TODO:Num2_Str
            Num2_Str
            'TODO:Num3_Str
            Num3_Str
            'TODO:Subject reference
            SubjectReference
            ''' <summary>Alphabetic characters from latin alphabet (A-Z and a-z) (represented by <see cref="String"/>)</summary>
            Alpha
            ''' <summary>Enum which's values are strings (represented by various enums). Actual string value can be obtained via <see cref="Xml.Serialization.XmlEnumAttribute"/></summary>
            StringEnum
            ''' <summary>Type of image stored as numeric character and alphabetic character (represented by <see cref="ImageType"/>)</summary>
            ImageType
            ''' <summary>Type of audio stored as numeric character and alphabetic character (represented by <see cref="AudioType"/>)</summary>
            AudioType
            ''' <summary>Duration in hours, minutes and seconds. Represented by <see cref="TimeSpan"/></summary>
            HHMMSS
        End Enum
#Region "Implementation"
        Public MustInherit Class NumStr
''' <summary>Contains value of the <see cref="Number"/> property</summary>            
<EditorBrowsable(EditorBrowsableState.Never)> Private _Number As Integer
''' <summary>Contains value of the <see cref="[String]"/> property</summary>
<EditorBrowsable(EditorBrowsableState.Never)>             Private _String As String
''' <summary>If overriden in derived class returns number of digits in number. Should not be zero.</summary>            
 Protected MustOverride readonly Property NumberDigits() As Byte
''' <summary>Number in this <see cref="NumStr"/></summary>            
Public Property Number() As Integer
                Get
                    Return _Number
                End Get
                Set(ByVal value As Integer)
if number.ToString(new String("0"c,numberdigits)&).Length >numberdigits then throw new ArgumentException ("Number has to many digits")
_Number = value
                End Set
            End Property
''' <summary>Text of this <see cref="NumStr"/></summary>                        
Public Property [String] As String
                        		Get
                        				Return _String
                        		End Get
                        		Set
                        				_String = value
                        		End Set
                        End Property
''' <summary>string representation in format number;string</summary>
notoverridable Public  Overrides    Function ToString() As String 
            return string.format("{0:"&new string("0"c,numberdigits)&"};{1}",number,[string])
            end funmction
        End Function
        End Class
        ''' <summary>represents common interface for media types</summary>
        <CLSCompliant(False)> _
        Public Interface IMediaType(Of TNumChar As {IConvertible, Structure}, TAlpha As {IConvertible, Structure})
            ''' <summary>Count fo components</summary>
            Property Count() As TNumChar
            ''' <summary>Type code</summary>
            Property Code() As TAlpha
            ''' <summary>Type code as character</summary>
            Property CodeString() As Char
        End Interface
        ''' <summary>Represents date (Year, Month and Day) which's parts can be ommited by setting value to 0</summary>
        ''' <remarks>Date represented by this structure can be invalid (e.g. 31.2.2008)</remarks>
        Public Structure OmmitableDate
            ''' <summary>Contains value of the <see cref="Year"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Year As UShort
            ''' <summary>Contains value of the <see cref="Day"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Day As Byte
            ''' <summary>Contains value of the <see cref="Month"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Month As Byte
            ''' <summary>CTor</summary>
            ''' <param name="Year">Year (or 0 if unknown)</param>
            ''' <param name="Month">Month (or 0 if unknown)</param>
            ''' <param name="Day">Day (or 0 if unknown)</param>
            ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Year"/> is less than zero or greater than 9999 -or- <paramref name="Month"/> is greater than 12 -or- <paramref name="Day"/> is greater than 31</exception>
            Public Sub New(ByVal Year As Short, Optional ByVal Month As Byte = 0, Optional ByVal Day As Byte = 0)
                Me.Year = Year
                Me.Month = Month
                Me.Day = Day
            End Sub
            ''' <summary>CTor from date</summary>
            ''' <param name="Date"><see cref="Date"/> to initialize this instance with</param>
            Public Sub New(ByVal [Date] As Date)
                Me.Year = [Date].Year
                Me.Month = [Date].Month
                Me.Day = [Date].Day
            End Sub
            ''' <summary>Converts <see cref="Date"/> into <see cref="OmmitableDate"/></summary>
            ''' <param name="From"><see cref="Date"/> to be converted</param>
            ''' <returns><see cref="OmmitableDate"/> initialized with <paramref name="From"/></returns>
            Public Shared Widening Operator CType(ByVal From As Date) As OmmitableDate
                Return New OmmitableDate(From)
            End Operator
            ''' <summary>Converts <see cref="OmmitableDate"/> into <see cref="Date"/></summary>
            ''' <param name="From"><see cref="OmmitableDate"/> to be converted</param>
            ''' <returns><see cref="Date"/> with same <see cref="DateTime.Year"/>, <see cref="DateTime.Month"/> and <see cref="DateTime.Day"/> properties as this instance</returns>
            ''' <exception cref="InvalidCastException">This instance cannot be converted to <see cref="Date"/> because it contains invalid date or 0 in some propery</exception>
            Public Shared Narrowing Operator CType(ByVal From As OmmitableDate) As Date
                Try
                    Return New Date(From.Year, From.Month, From.Day)
                Catch ex As Exception
                    Throw New InvalidCastException(ex.Message, ex)
                End Try
            End Operator
            ''' <summary>Year component of date</summary>
            ''' <exception cref="ArgumentOutOfRangeException">Value being set is less than zero or greater than 9999</exception>
            ''' <value>Value of year component or zero if unknown</value>
            ''' <returns>Year component or zero if unknown</returns>
            Public Property Year() As Short
                Get
                    Return _Year
                End Get
                Set(ByVal value As Short)
                    If value < 0 OrElse value > 9999 Then Throw New ArgumentOutOfRangeException("value", "Year must be from range 1÷9999 or 0 if unknown")
                    _Year = value
                End Set
            End Property
            ''' <summary>Day component of date</summary>
            ''' <value>Value of day component or zero if unknown</value>
            ''' <returns>Day component or zero if unknown</returns>
            ''' <exception cref="ArgumentOutOfRangeException">Setting value to value greater than 31</exception>
            Public Property Day() As Byte
                Get
                    Return _Day
                End Get
                Set(ByVal value As Byte)
                    If value > 31 Then Throw New ArgumentOutOfRangeException("value", "Day must be less then or equal to 31")
                    _Day = value
                End Set
            End Property
            ''' <summary>Month component of date</summary>
            ''' <exception cref="ArgumentOutOfRangeException">Setting value to value greater than 12</exception>
            ''' <value>Value of month component or zero if unknown</value>
            ''' <returns>Month component or zero if unknown</returns>
            Public Property Month() As Byte
                Get
                    Return _Month
                End Get
                Set(ByVal value As Byte)
                    If value > 12 Then Throw New ArgumentOutOfRangeException("value", "Month must be less than or equal to 12")
                    _Month = value
                End Set
            End Property
            ''' <summary>String representation in YYYYMMDD format</summary>
            Public Overrides Function ToString() As String
                Return String.Format("{0:0000}{1:00}{2:00}", Year, Month, Day)
            End Function
        End Structure

        ''' <summary>Contains time as hours, minutes and seconds and offset to UTC in hours and minutes</summary>
        Public Structure Time
            ''' <summary>Contains value of the <see cref="Hour"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Hour As Byte
            ''' <summary>Hour component of time</summary>
            ''' <exception cref="ArgumentOutOfRangeException">Setting value greater than 23</exception>
            Public Property Hour() As Byte
                Get
                    Return _Hour
                End Get
                Set(ByVal value As Byte)
                    If value > 23 Then Throw New ArgumentOutOfRangeException("value", "Hour must be less than 24")
                    _Hour = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Minute"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Minute As Byte
            ''' <summary>Hour component of time</summary>
            ''' <exception cref="ArgumentOutOfRangeException">Setting value greater than 59</exception>
            Public Property Minute() As Byte
                Get
                    Return _Minute
                End Get
                Set(ByVal value As Byte)
                    If value > 59 Then Throw New ArgumentOutOfRangeException("value", "Minute must be less than 60")
                    _Minute = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Second"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Second As Byte
            ''' <summary>Second component of time</summary>
            ''' <exception cref="ArgumentOutOfRangeException">Setting value greater than 59</exception>
            Public Property Second() As Byte
                Get
                    Return _Second
                End Get
                Set(ByVal value As Byte)
                    If value > 59 Then Throw New ArgumentOutOfRangeException("value", "Second must be less than 60")
                    _Second = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="OffsetHour"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _OffsetHour As SByte
            ''' <summary>Hour component of offset in range -13 to +12</summary>
            ''' <exception cref="ArgumentOutOfRangeException">Setting value otside of range -13 ÷ +12</exception>
            <CLSCompliant(False)> _
            Public Property OffsetHour() As SByte
                Get
                    Return _OffsetHour
                End Get
                Set(ByVal value As SByte)
                    If value < -13 OrElse value > 12 Then Throw New ArgumentOutOfRangeException("value", "OffsetHours must be within range -13 ÷ +12")
                    _OffsetHour = value
                End Set
            End Property
            ''' <summary>Absolute value of hour component of offset in range -13 to +12</summary>
            ''' <remarks>This property is here in order to keep this structure CLS compliant. Internaly uses <see cref="OffsetHours"/></remarks>
            ''' <exception cref="ArgumentOutOfRangeException">Setting value otside of range -13 ÷ +12</exception>
            Public Property AbsoluteOffsetHour() As Byte
                Get
                    Return Math.Abs(OffsetHour)
                End Get
                Set(ByVal value As Byte)
                    If OffsetHour < 0 Then OffsetHour = -value Else OffsetHour = value
                End Set
            End Property
            ''' <summary>Sign of hour component of offset in range -13 to +12</summary>
            ''' <remarks>This property is here in order to keep this structure CLS compliant. Internaly uses <see cref="OffsetHours"/></remarks>
            ''' <exception cref="ArgumentOutOfRangeException">Setting sign to + when <see cref="AbsoluteOffsetHours"/> is 13</exception>
            Public Property NegativeOffset() As Boolean
                Get
                    Return OffsetHour < 0
                End Get
                Set(ByVal value As Boolean)
                    OffsetHour = Math.Abs(OffsetHour) * VisualBasicT.iif(value, -1, 1)
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="OffsetMinute"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _OffsetMinute As Byte
            ''' <exception cref="ArgumentOutOfRangeException">Setting value greater than 59</exception>
            Public Property OffsetMinute() As Byte
                Get
                    Return _OffsetMinute
                End Get
                Set(ByVal value As Byte)
                    If value > 59 Then Throw New ArgumentOutOfRangeException("value", "OffsetMinutes must be less than 60")
                    _OffsetMinute = value
                End Set
            End Property
            ''' <summary>String representation in the HHMMSS±HHMM format</summary>
            Public Overrides Function ToString() As String
                Return String.Format("{0:00}{1:00}{2:00}{3:+00;-00}{4:00}", Hour, Minute, Second, OffsetHour, OffsetMinute)
            End Function
            ''' <summary>CTor</summary>
            ''' <param name="Hours">Hour component</param>
            ''' <param name="Minutes">Minute component</param>
            ''' <param name="Seconds">Second component</param>
            ''' <param name="HourOffset">Hour component of offset</param>
            ''' <param name="MinuteOffset">Minute component of offset</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Hours"/> is greater than 23 -or- <paramref name="Minutes"/> or <paramref name="Seconds"/> or <paramref name="MinuteOffset"/> is greater than 59 -or- <paramref name="HourOffset"/> is not within range -13 ÷ +12</exception>
            <CLSCompliant(False)> _
            Public Sub New(ByVal Hours As Byte, ByVal Minutes As Byte, ByVal Seconds As Byte, Optional ByVal HourOffset As SByte = 0, Optional ByVal MinuteOffset As Byte = 0)
                Me.Hour = Hours
                Me.Minute = Minutes
                Me.Second = Seconds
                Me.OffsetHour = HourOffset
                Me.OffsetMinute = MinuteOffset
            End Sub
            ''' <summary>CTor from <see cref="TimeSpan"/></summary>
            ''' <param name="Time"><see cref="TimeSpan"/> to initialize zhis instance</param>
            ''' <remarks>Offset is not initialized</remarks>
            Public Sub New(ByVal Time As TimeSpan)
                Me.Hour = Time.Hours
                Me.Second = Time.Seconds
                Me.Minute = Time.Minutes
            End Sub
            ''' <summary>CTor from <see cref="Date"/></summary>
            ''' <param name="Date"><see cref="Date"/> which time path will be used to initialize this instance</param>
            ''' <remarks>Offset is not initialized</remarks>
            Public Sub New(ByVal [Date] As Date)
                Me.Hour = [Date].Hour
                Me.Minute = [Date].Minute
                Me.Second = [Date].Second
            End Sub
        End Structure
        ''' <summary>Checks if specified value is member of an enumeration</summary>
        ''' <param name="value">Value to be chcked</param>
        ''' <returns>True if <paramref name="value"/> is member of <paramref name="T"/></returns>
        ''' <typeparam name="T">Enumeration to be tested</typeparam>
        ''' <exception cref="ArgumentException"><paramref name="T"/> is not <see cref="[Enum]"/></exception>
        Private Shared Function InEnum(Of T As {IConvertible, Structure})(ByVal value As T) As Boolean
            'TODO:Extract this as separate tool
            Return Array.IndexOf([Enum].GetValues(GetType(T)), value) >= 0
        End Function
        ''' <summary>Gets <see cref="Reflection.FieldInfo"/> that represent given constant within an enum</summary>
        ''' <param name="value">Constant to be found</param>
        ''' <returns><see cref="Reflection.FieldInfo"/> of given <paramref name="value"/> if <paramref name="value"/> is member of <paramref name="T"/></returns>
        ''' <typeparam name="T"><see cref="[Enum]"/> to found constant within</typeparam>
        ''' <exception cref="ArgumentException"><paramref name="T"/> is not <see cref="[Enum]"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is not member of <paramref name="T"/></exception>
        Private Shared Function GetConstant(Of T As {IConvertible, Structure})(ByVal value As T) As Reflection.FieldInfo
            'TODO:Extract as separate tool
            Return GetType(T).GetField([Enum].GetName(GetType(ImageTypeContents), value))
        End Function
        ''' <summary>IPTC image type</summary>
        Public Structure ImageType : Implements IMediaType(Of ImageTypeComponents, ImageTypeContents)
            ''' <summary>Contains value of the <see cref="Type"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Type As ImageTypeContents
            ''' <summary>Contains value of the <see cref="Component"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Components As ImageTypeComponents
            ''' <summary>Type of components</summary>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="ImageTypeContents"/></exception>
            Public Property Type() As ImageTypeContents Implements IMediaType(Of Tools.DrawingT.MetadataT.IPTC.ImageTypeComponents, Tools.DrawingT.MetadataT.IPTC.ImageTypeContents).Code
                Get
                    Return _Type
                End Get
                Set(ByVal value As ImageTypeContents)
                    If Not InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, GetType(ImageTypeContents))
                    _Type = value
                End Set
            End Property
            ''' <summary>Number of components</summary>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="ImageTypeComponents"/></exception>
            Public Property Components() As ImageTypeComponents Implements IMediaType(Of Tools.DrawingT.MetadataT.IPTC.ImageTypeComponents, Tools.DrawingT.MetadataT.IPTC.ImageTypeContents).Count
                Get
                    Return _Components
                End Get
                Set(ByVal value As ImageTypeComponents)
                    If Not InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, GetType(ImageTypeComponents))
                    _Components = value
                End Set
            End Property
            ''' <summary>Gets or sets <see cref="Type"/> as <see cref="String"/></summary>
            ''' <exception cref="ArgumentException">Value being set cannot be interpreted member of <see cref="ImageTypeContents"/></exception>
            Public Property TypeCode() As Char Implements IMediaType(Of Tools.DrawingT.MetadataT.IPTC.ImageTypeComponents, Tools.DrawingT.MetadataT.IPTC.ImageTypeContents).CodeString
                Get
                    Return DirectCast(GetConstant(Type).GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)(0), Xml.Serialization.XmlEnumAttribute).Name
                End Get
                Set(ByVal value As Char)
                    For Each Constant As Reflection.FieldInfo In GetType(ImageTypeContents).GetFields()
                        Dim Attrs As Object() = Constant.GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)
                        If Attrs IsNot Nothing AndAlso Attrs.Length > 0 Then
                            Type = Constant.GetValue(Nothing)
                            Return
                        End If
                    Next Constant
                    Throw New ArgumentException(String.Format("{0} cannot be interpreted as ImageTypeContents", value))
                End Set
            End Property
            ''' <summary>String representation in form 0T (components, type)</summary>
            Public Overrides Function ToString() As String
                Return String.Format("{0}{1}", CByte(Components), TypeCode)
            End Function
        End Structure

        ''' <summary>IPTC audio type</summary>
        Public Structure AudioType : Implements IMediaType(Of Byte, AudioDataType)
            ''' <summary>Contains value of the <see cref="Type"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Type As AudioDataType
            ''' <summary>Contains value of the <see cref="Component"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Components As Byte
            ''' <summary>Type of components</summary>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="ImageTypeContents"/></exception>
            Public Property Type() As AudioDataType Implements IMediaType(Of Byte, Tools.DrawingT.MetadataT.IPTC.AudioDataType).Code
                Get
                    Return _Type
                End Get
                Set(ByVal value As AudioDataType)
                    If Not InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, GetType(AudioDataType))
                    _Type = value
                End Set
            End Property
            ''' <summary>Number of components</summary>
            ''' <exception cref="ArgumentOutOfRangeException">Value being set is ot of range 0÷9</exception>
            Public Property Components() As Byte Implements IMediaType(Of Byte, Tools.DrawingT.MetadataT.IPTC.AudioDataType).Count
                Get
                    Return _Components
                End Get
                Set(ByVal value As Byte)
                    If Not value >= 0 AndAlso value <= 9 Then Throw New ArgumentOutOfRangeException("value", "Number of components of AudioType must be from 0 to 9")
                    _Components = value
                End Set
            End Property
            ''' <summary>Gets or sets <see cref="Type"/> as <see cref="String"/></summary>
            ''' <exception cref="ArgumentException">Value being set cannot be interpreted member of <see cref="ImageTypeContents"/></exception>
            Public Property TypeCode() As Char Implements IMediaType(Of Byte, Tools.DrawingT.MetadataT.IPTC.AudioDataType).CodeString
                Get
                    Return DirectCast(GetConstant(Type).GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)(0), Xml.Serialization.XmlEnumAttribute).Name
                End Get
                Set(ByVal value As Char)
                    For Each Constant As Reflection.FieldInfo In GetType(ImageTypeContents).GetFields()
                        Dim Attrs As Object() = Constant.GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)
                        If Attrs IsNot Nothing AndAlso Attrs.Length > 0 Then
                            Type = Constant.GetValue(Nothing)
                            Return
                        End If
                    Next Constant
                    Throw New ArgumentException(String.Format("{0} cannot be interpreted as ImageTypeContents", value))
                End Set
            End Property
            ''' <summary>String representation in form 0T (components, type)</summary>
            Public Overrides Function ToString() As String
                Return String.Format("{0}{1}", Components, TypeCode)
            End Function
        End Structure
#End Region
#End Region
        ''' <summary>Indicates if enum may allow values that are not member of it or not</summary>
        <AttributeUsage(AttributeTargets.Enum)> _
        Public Class RestrictAttribute : Inherits Attribute
            ''' <summary>Contains value of the <see cref="Restrict"/> property</summary>
            Private _Restrict As Boolean
            ''' <summary>CTor</summary>
            ''' <param name="Restrict">State of restriction</param>
            Public Sub New(ByVal Restrict As Boolean)
                _Restrict = Restrict
            End Sub
            ''' <summary>Inidicates if values should be restricted to enum members</summary>
            Public ReadOnly Property Restrict() As Boolean
                Get
                    Return _Restrict
                End Get
            End Property
        End Class
    End Class
#End If
End Namespace