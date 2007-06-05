Imports Tools.CollectionsT.GenericT
Namespace DrawingT.MetadataT
#If Congig <= Nightly Then 'Stage: Nightly
    Partial Public Class IPTC
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
            ''' <summary>Unique Object Identifier (represented by <see cref="UNO"/>)</summary>
            UNO
            ''' <summary>Combination of 2-digits number and optional <see cref="String"/> (represented by <see cref="NumStr2"/>)</summary>
            Num2_Str
            ''' <summary>Combination of 3-digits number and optional <see cref="String"/> (represented by <see cref="NumStr3"/>)</summary>
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
        ''' <summary>Indicates if given string contains only graphic characters</summary>
        ''' <param name="Str">String to be verified</param>
        ''' <returns>True if string contains only graphic characters, false otherwise</returns>
        ''' <remarks>All characters with ASCII code higher than space are considered graphic</remarks>
        Public Shared Function IsGraphicCharacters(ByVal Str As String) As Boolean
            For Each ch As Char In Str
                If AscW(ch) <= AscW(" "c) Then Return False
            Next ch
            Return True
        End Function
#Region "Implementation"
        ''' <summary>IPTC Subject Reference</summary>
        Public Class SubjectReference : Inherits WithIPR
            ''' <summary>Gets lenght limit for <see cref="IPR"/></summary>
            ''' <returns>32</returns>
            Protected Overrides ReadOnly Property IPRLengthLimit() As Byte
                Get
                    Return 32
                End Get
            End Property
            ''' <summary>Contains value of the <see cref="SubjectReferenceNumber"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _SubjectReferenceNumber As Integer
            ''' <summary>Provides a numeric code to indicate the Subject Name plus optional Subject Matter and Subject Detail Names in the language of the service.</summary>
            ''' <remarks>Subject Reference Numbers consist of 8 octets in the range 01000000 to 17999999 and represent a language independent international reference to a Subject. A Subject is identified by its Reference Number and corresponding Names taken from a standard lists given in Appendix H,I &amp; J.These lists are the English language reference versions.</remarks>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is member neither of <see cref="SubjectMatterNumbers"/> nor of <see cref="SubjectReferenceNumbers"/> nor of <see cref="EconomySubjectDetail"/> nor it is 0</exception>
            Public Property SubjectReferenceNumber() As Integer
                Get
                    Return _SubjectReferenceNumber
                End Get
                Set(ByVal value As Integer)
                    If Array.IndexOf([Enum].GetValues(GetType(SubjectReferenceNumbers)), value) < 0 AndAlso _
                            Array.IndexOf([Enum].GetValues(GetType(SubjectMatterNumbers)), value) < 0 AndAlso _
                            Array.IndexOf([Enum].GetValues(GetType(EconomySubjectDetail)), value) < 0 AndAlso _
                            value <> 0 Then
                        Throw New InvalidEnumArgumentException("SubjectReferenceNumber must be member of either SubjectReferenceNumbers, SubjectMatterNumbers or EconomySubjectDetail")
                    End If
                End Set
            End Property
            'TODO:Names!!!
''' <summary>Contains value of the <see cref="SubjectName"/> property</summary>            
<EditorBrowsable(EditorBrowsableState.Never)>Private _SubjectName As String
            Public Property SubjectName() As String
                Get
                    Return _SubjectName
                End Get
                Set(ByVal value As String)
                    _SubjectName = value
                End Set
            End Property
''' <summary>Contains value of the <see cref="SubjectMatterName"/> property</summary>                        
<EditorBrowsable(EditorBrowsableState.Never)>Private _SubjectMatterName As String
            Public Property SubjectMatterName As String
            		Get
            				Return _SubjectMatterName
            		End Get
            		Set
            				_SubjectMatterName = value
            		End Set
            End Property
''' <summary>Contains value of the <see cref="SubjectDetailName"/> property</summary>                        
<EditorBrowsable(EditorBrowsableState.Never)>Private _SubjectDetailName As String
                                                Public Property SubjectDetailName As String
                                                		Get
                                                				Return _SubjectDetailName
                                                		End Get
                                                		Set
                                                				_SubjectDetailName = value
                                                		End Set
                                                End Property
        End Class
        ''' <summary>Common base for classes that have the <see cref="WithIPR.IPR"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public MustInherit Class WithIPR
            ''' <summary>Contains value of the <see cref="IPR"/> property</summary>            
            <EditorBrowsable(EditorBrowsableState.Never)> Private _IPR As String = " "c
            ''' <summary>Information Provider Reference A name, registered with the IPTC/NAA, identifying the provider that guarantees the uniqueness of the UNO</summary>            
            ''' <remarks>A name, registered with the IPTC/NAA, identifying the provider that guarantees the uniqueness of the UNO</remarks>
            ''' <value>A minimum of one and a maximum of 32 octets. A string of graphic characters, except colon ‘:’ solidus ‘/’, asterisk ‘*’ and question mark ‘?’, registered with, and approved by, the IPTC.</value>
            ''' <exception cref="ArgumentException">Value being set contains unallowed characters (white space, *, :, /, ? or control characters) -or- value being set is an empty <see cref="String"/> or its <see cref="String.Length"/> if more than 32 -or- length of value being set exceeds <see cref="IPRLengthLimit"/></exception>
            Public Overridable Property IPR() As String
                Get
                    Return _IPR
                End Get
                Set(ByVal value As String)
                    If Not IsGraphicCharacters(value) Then Throw New ArgumentException("Only graphic characters are allowd in IPR")
                    If value.Contains("*"c) OrElse value.Contains("/"c) OrElse value.Contains("?"c) OrElse value.Contains(":"c) Then Throw New ArgumentException("IPR cannot contain characters *, /, ? and :")
                    If value = "" OrElse value.Length > 32 Then Throw New ArgumentException("IPR must be string with length from 1 to 32 characters")
                    If value.Length > IPRLengthLimit Then Throw New ArgumentException("The lenght of IPR exceeds limit")
                    _IPR = value
                End Set
            End Property
            ''' <summary>Gets or sets value of the <see cref="IPR"/> property as member of <see cref="InformationProviders"/></summary>
            ''' <value>Value that is member of <see cref="InformationProviders"/></value>
            ''' <returns>Value that is member of <see cref="InformationProviders"/> if <see cref="IPR"/> can be represented as member of <see cref="InformationProviders"/>, -1 otherwise</returns>
            ''' <exception cref="InvalidEnumArgumentException">Setting value that is not member of <see cref="InformationProviders"/></exception>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            Public Overridable Property ListedIPR() As InformationProviders
                Get
                    For Each f As Reflection.FieldInfo In GetType(InformationProviders).GetFields
                        Dim attrs As Object() = f.GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)
                        If attrs IsNot Nothing AndAlso attrs.Length > 0 Then
                            If DirectCast(attrs(0), Xml.Serialization.XmlEnumAttribute).Name = IPR Then Return f.GetValue(Nothing)
                        End If
                    Next f
                    Return -1
                End Get
                Set(ByVal value As InformationProviders)
                    If Array.IndexOf([Enum].GetValues(GetType(InformationProviders)), value) < 0 Then Throw New InvalidEnumArgumentException("value", value, GetType(InformationProviders))
                    IPR = DirectCast(GetConstant(value).GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)(0), Xml.Serialization.XmlEnumAttribute).Name
                End Set
            End Property
            ''' <summary>When overriden in derived class gets actual lenght limit for <see cref="IPR"/></summary>
            Protected MustOverride ReadOnly Property IPRLengthLimit() As Byte
        End Class
        ''' <summary>Represents IPTC UNO unique object identifier</summary>
        ''' <remarks>
        ''' <para>The first three elements of the UNO (the UCD, the IPR and the ODE) together are allocated to the editorial content of the object.</para>
        ''' <para>Any technical variants or changes in the presentation of an object, e.g. a picture being presented by a different file format, does not require the allocation of a new ODE but can be indicated by only generating a new OVI.</para>
        ''' <para>Links may be set up to the complete UNO but the structure provides for linking to selected elements, e.g. to all objects of a specified provider.</para>
        ''' </remarks>
        Public Class UNO : Inherits WithIPR
            ''' <summary>Contains value of the <see cref="UCD"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _UCD As Date = Now.Date
            ''' <summary>UNO Creation Date Specifies a 24 hour period in which the further elements of the UNO have to be unique.</summary>            
            ''' <remarks>It also provides a search facility.</remarks>
            Public Property UCD() As Date
                Get
                    Return _UCD
                End Get
                Set(ByVal value As Date)
                    _UCD = value
                End Set
            End Property
            ''' <summary>Actual length limit of <see cref="IPR"/></summary>
            ''' <returns>61 - (1 + length of <see cref="ODE"/>)</returns>
            Protected Overrides ReadOnly Property IPRLengthLimit() As Byte
                Get
                    Dim Arr(ODE.Count - 1) As String
                    ODE.CopyTo(Arr, 0)
                    Return 61 - 1 - String.Join("/"c, Arr).Length
                End Get
            End Property
            ''' <summary>Contains value of the <see cref="ODE"/> property</summary>            
            <EditorBrowsable(EditorBrowsableState.Never)> Private _ODE As New ListWithEvents(Of String)(New String() {" "c}, , True)
            ''' <summary>CTor</summary>
            Public Sub New()
                AddHandler _ODE.Adding, AddressOf ODE_Adding
                AddHandler _ODE.ItemChanging, AddressOf ODE_Adding
                AddHandler _ODE.Removing, AddressOf ODE_Removing
                AddHandler _ODE.Clearing, AddressOf ODE_Clearing
                _ODE.AllowAddCancelableEventsHandlers = False
            End Sub
            ''' <summary>Block <see cref="ODE"/>'s last item from being removed</summary>
            ''' <param name="sender"><see cref="_ODE"/></param>
            ''' <param name="e">Event parameters</param>
            Private Sub ODE_Removing(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).CancelableItemIndexEventArgs)
                If _ODE.Count = 0 Then
                    e.Cancel = True
                    e.CancelMessage = "Cannot remove last item from ODE"
                End If
            End Sub
            ''' <summary>Block <see cref="ODE"/> from being cleared</summary>
            ''' <param name="sender"><see cref="_ODE"/></param>
            ''' <param name="e">Event parameters</param>
            Private Sub ODE_Clearing(ByVal sender As ListWithEvents(Of String), ByVal e As ComponentModelT.CancelMessageEventArgs)
                e.Cancel = True
                e.CancelMessage = "ODE cannot be cleared"
            End Sub
            ''' <summary>Controls if item added to <see cref="ODE"/> are valid</summary>
            ''' <param name="sender"><see cref="_ODE"/></param>
            ''' <param name="e">parameters of event</param>
            Private Sub ODE_Adding(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).CancelableItemIndexEventArgs)
                If Not IsGraphicCharacters(e.Item) Then
                    e.CancelMessage = "ODE can consist only of graphic characters"
                    e.CancelMessage = True
                ElseIf e.Item.Contains("*"c) OrElse e.Item.Contains("/"c) OrElse e.Item.Contains("?"c) OrElse e.Item.Contains(":"c) Then
                    e.Cancel = True
                    e.CancelMessage = "ODE cannot contain /, *, ? or :"
                ElseIf e.Item = "" Then
                    e.Cancel = True
                    e.CancelMessage = "ODE component cannot be an empty string"
                Else
                    Dim Arr() As String
                    If e.NewIndex >= ODE.Count Then
                        ReDim Arr(ODE.Count)
                    Else
                        ReDim Arr(ODE.Count - 1)
                    End If
                    ODE.CopyTo(Arr, 0)
                    Arr(e.NewIndex) = e.Item
                    Dim WholeOde As String = String.Join("/"c, Arr)
                    If WholeOde.Length + IPR.Length + 1 > 61 Then
                        e.Cancel = True
                        e.CancelMessage = "Length of ODE and IPR together with separators must wit into 61"
                    End If
                End If
            End Sub
            '' <summary>Object Descriptor Element In conjunction with the UCD and the IPR, a string of characters ensuring the uniqueness of the UNO.</summary>            
            ''' <value>A minimum of one and a maximum of 60 minus the number of IPR octets, consisting of graphic characters, except colon ‘:’ asterisk ‘*’ and question mark ‘?’. The provider bears the responsibility for the uniqueness of the ODE within a 24 hour cycle.</value>
            ''' <exception cref="OperationCanceledException">
            ''' The <see cref="ListWithEvents(Of String).Add"/> and <see cref="ListWithEvents(Of String).Item"/>'s setter can throw an <see cref="OperationCanceledException"/> when trying to add invalid item (containing invalid characters (?,:,?,*), too long or an empty string) or accumulated lenght of <see cref="IPR"/> and <see cref="ODE"/> (including <see cref="IPR"/>-<see cref="ODE"/> separator and separators of items of <see cref="ODE"/>) is greater than 61
            ''' -and- <see cref="ListWithEvents(Of String).Remove"/> and <see cref="ListWithEvents(Of String).RemoveAt"/> throws <see cref="OperationCanceledException"/> when trying to remove last item from <see cref="ODE"/>
            ''' -and- <see cref="ListWithEvents(Of String).Clear"/> throws <see cref="OperationCanceledException"/> everywhen
            ''' </exception>
            Public ReadOnly Property ODE() As IList(Of String)
                Get
                    Return _ODE
                End Get
            End Property
            ''' <summary>Contains value of the <see cref="OVI"/> property</summary>            
            <EditorBrowsable(EditorBrowsableState.Never)> Private _OVI As String = "0"c
            ''' <summary>Object Variant Indicator A string of characters indicating technical variants of the object such as partial objects, or changes of file formats, and coded character sets.</summary>             
            ''' <value>A minimum of one and a maximum of 9 octets, consisting of graphic characters, except colon ‘:’, asterisk ‘*’ and question mark ‘?’. To indicate a technical variation of the object as so far identified by the first three elements. Such variation may be required, for instance, for the indication of part of the object, or variations of the file format, or coded character set. The default value is a single ‘0’ (zero) character indicating no further use of the OVI.</value>
            ''' <exception cref="ArgumentException">Value being set contains unallowed characters (white space, *, :, /, ? or control characters) -or- value being set is an empty <see cref="String"/> or its lenght is larger than 9</exception>
            Public Property OVI() As String
                Get
                    Return _OVI
                End Get
                Set(ByVal value As String)
                    If Not IsGraphicCharacters(value) Then Throw New ArgumentException("Only graphic characters are allowd in OVI")
                    If value.Contains("*"c) OrElse value.Contains("/"c) OrElse value.Contains("?"c) OrElse value.Contains(":"c) Then Throw New ArgumentException("OVI cannot contain characters *, /, ? and :")
                    If value.Length > 9 OrElse value = "" Then Throw New ArgumentException("OVI must be string with length from 1 to 9")
                    _OVI = value
                End Set
            End Property
            ''' <summary>String representation in form UCD:IPR:ODE1/ODE2/ODE3:OVI</summary>
            Overrides Function ToString() As String
                Dim ODEArr(ODE.Count - 1) As String
                ODE.CopyTo(ODEArr, 0)
                Return String.Format("{0:YYYYMMDD}:{1}:{2}:{3}", UCD, IPR, String.Join("/"c, ODEArr), OVI)
            End Function
        End Class
        ''' <summary>Represents combination of number and string</summary>
        ''' <remarks>This class is abstract, derived class mus specify number of digits of <see cref="NumStr.Number"/></remarks>
        Public MustInherit Class NumStr
            ''' <summary>Contains value of the <see cref="Number"/> property</summary>            
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Number As Integer
            ''' <summary>Contains value of the <see cref="[String]"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _String As String
            ''' <summary>If overriden in derived class returns number of digits in number. Should not be zero.</summary>            
            Protected MustOverride ReadOnly Property NumberDigits() As Byte
            ''' <summary>Number in this <see cref="NumStr"/></summary>            
            Public Property Number() As Integer
                Get
                    Return _Number
                End Get
                Set(ByVal value As Integer)
                    If Number.ToString(New String("0"c, NumberDigits)).Length > NumberDigits Then Throw New ArgumentException("Number has to many digits")
                    _Number = value
                End Set
            End Property
            ''' <summary>Text of this <see cref="NumStr"/></summary>                        
            Public Property [String]() As String
                Get
                    Return _String
                End Get
                Set(ByVal value As String)
                    _String = value
                End Set
            End Property
            ''' <summary>String representation in format number;string</summary>
            Public NotOverridable Overrides Function ToString() As String
                Return String.Format("{0:" & New String("0"c, NumberDigits) & "};{1}", Number, [String])
            End Function
        End Class
        ''' <summary>Represents combination of 2-digits numer and string</summary>
        Public Class NumStr2 : Inherits NumStr
            ''' <summary>Number of digits in number</summary>
            ''' <returns>2</returns>
            Protected Overrides ReadOnly Property NumberDigits() As Byte
                Get
                    Return 2
                End Get
            End Property
        End Class
        ''' <summary>Represents combination of 3-digits numer and string</summary>
        Public Class NumStr3 : Inherits NumStr
            ''' <summary>Number of digits in number</summary>
            ''' <returns>3</returns>
            Protected Overrides ReadOnly Property NumberDigits() As Byte
                Get
                    Return 2
                End Get
            End Property
        End Class
        ''' <summary>Represents common interface for media types</summary>
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
            Return GetType(T).GetField([Enum].GetName(GetType(T), value))
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
    End Class
#End If
End Namespace