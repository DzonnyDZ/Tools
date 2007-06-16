Imports Tools.CollectionsT.GenericT, System.Globalization.CultureInfo, Tools.DataStructuresT.GenericT
Imports Tools.VisualBasicT.Interaction, Tools.ComponentModelT
Namespace DrawingT.MetadataT
#If Congig <= Nightly Then 'Stage: Nightly
    Partial Public Class IPTC
        ''' <summary>Types od data used by IPTC tags</summary>
        Public Enum IPTCTypes
            ''' <summary>Unsigned binary number of unknown length (represented by <see cref="ULong"/>)</summary>
            UnsignedBinaryNumber
            ''' <summary>Binary stored boolean value (can be stored in multiple bytes. If any of bytes is nonzero, value is true) (represented by <see cref="Boolean"/></summary>
            Boolean_Binary
            ''' <summary>Binary stored 1 byte long unsigned integer (represented by <see cref="Byte"/>)</summary>
            Byte_Binary
            ''' <summary>Binary stored 2 byte long unsigned integer (represented by <see cref="UShort"/>)</summary>
            UShort_Binary
            ''' <summary>Number of variable length stored as string.</summary>
            ''' <remarks>
            ''' <list type="table"><listheader><term>Length up to characters</term><description>Represented by</description></listheader>
            ''' <item><term>2</term><description><see cref="Byte"/></description></item>
            ''' <item><term>4</term><description><see cref="Short"/></description></item>
            ''' <item><term>9</term><description><see cref="Integer"/></description></item>
            ''' <item><term>19</term><description><see cref="Long"/></description></item>
            ''' <item><term>29</term><description><see cref="Decimal"/></description></item>
            ''' <item><term>unknown</term><description><see cref="Decimal"/></description></item>
            ''' </list>
            ''' </remarks>
            NumericChar
            ''' <summary>Grahic characters (no whitespaces, no control characters) (represented by <see cref="String"/>)</summary>
            GraphicCharacters
            ''' <summary>Graphic characters and spaces (no tabs, no CR, no LF, no control characters) (represented by <see cref="String"/>)</summary>
            TextWithSpaces
            ''' <summary>Printable text (no tabs, no control characters) (represented by <see cref="String"/>)</summary>
            Text
            ''' <summary>Black and white bitmap with width 460px (represented <see cref="Drawing.Bitmap"/>)</summary>
            BW460
            ''' <summary>Enumeration stored as binary number (represented by various enums)</summary>
            Enum_Binary
            ''' <summary>Enumeration stored as numeric string (represented by various enums)</summary>
            Enum_NumChar
            ''' <summary>Date stored as numeric characters in the YYYYMMDD format (represented by <see cref="Date"/>)</summary>
            CCYYMMDD
            ''' <summary>Date stored as numeric characters in the YYYYMMDD format (represented by <see cref="OmmitableDate"/>) Each component YYYY, MM and DD can be set to 0 is unknown</summary>
            CCYYMMDDOmmitable
            ''' <summary>Time stored as numeric characters (and the � sign) in format HHMMSS�HHMM (with time-zone offset from UTC) (represented by <see cref="Time"/></summary>
            HHMMSS_HHMM
            ''' <summary>Generic array of bytes (represented by array of <see cref="Byte"/>)</summary>
            ByteArray
            ''' <summary>Unique Object Identifier (represented by <see cref="UNO"/>)</summary>
            UNO
            ''' <summary>Combination of 2-digits number and optional <see cref="String"/> (represented by <see cref="T:Tools.DrawingT.MetadataT.IPTC.NumStr2"/>)</summary>
            Num2_Str 'TODO:Enum?
            ''' <summary>Combination of 3-digits number and optional <see cref="String"/> (represented by <see cref="T:Tools.DrawingT.MetadataT.IPTC.NumStr3"/>)</summary>
            Num3_Str 'TODO:Enum?
            ''' <summary>Subject reference (combination of IPR, subject number and description) (represented by <see cref="SubjectReference"/>)</summary>
            SubjectReference
            ''' <summary>Alphabetic characters from�latin alphabet (A-Z and a-z) (represented by <see cref="String"/>)</summary>
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
        ''' <summary>Indicates if given string contains only graphic characters and spaces</summary>
        ''' <param name="Str">String to be verified</param>
        ''' <returns>True if string contains only graphic characters and spaces, false otherwise</returns>
        ''' <remarks>All characters with ASCII code higher than space are considered graphic</remarks>
        Public Shared Function IsTextWithSpaces(ByVal Str As String) As Boolean
            For Each ch As Char In Str
                If AscW(ch) <= AscW(" "c) Then Return False
            Next ch
            Return True
        End Function
        ''' <summary>Indicates if given string contains only graphic characters, spaces, Crs and Lfs</summary>
        ''' <param name="Str">String to be verified</param>
        ''' <returns>True if string contains only graphic characters, spaces, Crs and Lfs, false otherwise</returns>
        ''' <remarks>All characters with ASCII code higher than space are considered graphic</remarks>
        Public Shared Function IsText(ByVal Str As String) As Boolean
            For Each ch As Char In Str
                If AscW(ch) <= AscW(" "c) AndAlso AscW(ch) <> AscW(vbCr) AndAlso AscW(ch) <> AscW(vbLf) Then Return False
            Next ch
            Return True
        End Function
        ''' <summary>Indicates if given string contains only graphic characters</summary>
        ''' <param name="Str">String to be verified</param>
        ''' <returns>True if string contains only graphic characters, false otherwise</returns>
        ''' <remarks>All characters with ASCII code higher than space are considered graphic</remarks>
        Public Shared Function IsGraphicCharacters(ByVal Str As String) As Boolean
            For Each ch As Char In Str
                If AscW(ch) < AscW(" "c) Then Return False
            Next ch
            Return True
        End Function
        ''' <summary>Indicates if given string contains only alpha characters</summary>
        ''' <param name="Str">String to be verified</param>
        ''' <returns>True if string contains only alpha characters, false otherwise</returns>
        Public Shared Function IsAlpha(ByVal Str As String) As Boolean
            For Each ch As Char In Str
                Select Case AscW(ch)
                    Case AscW("a"c) To AscW("z"c), AscW("A"c) To AscW("Z"c)
                    Case Else : Return False
                End Select
            Next ch
            Return True
        End Function
#Region "Implementation"
        ''' <summary>IPTC Subject Reference (IPTC type <see cref="IPTCTypes.SubjectReference"/>)</summary>
        Public Class iptcSubjectReference : Inherits WithIPR
            ''' <summary>This masks <see cref="SubjectReferenceNumber"/></summary>
            Private Const SubjRefNMask As Integer = 1000000
            ''' <summary>Thsi masks <see cref="SubjectMatterNumber"/></summary>
            Private Const SubjMatterMask As Integer = 1000
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
            <Browsable(False)> _
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
            ''' <summary>Subject component of <see cref="SubjectReferenceNumber"/></summary>
            ''' <remarks>The Subject identifies the general content of the objectdata as determined by the provider.</remarks>
            ''' <value>New value for subject number. Setting this property resets <see cref="SubjectMatterNumber"/> and <see cref="SubjectDetailNumber"/> to zero</value>
            ''' <returns>Subject number value or zero if none specified</returns>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="SubjectReferenceNumbers"/> and it is not zero</exception>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            <DisplayName("Subject Number"), Description("Subject component of Subject Reference Number")> _
            Public Property SubjectNumber() As SubjectReferenceNumbers 'Localize: Description and DisplayName
                Get
                    Return (SubjectReferenceNumber \ SubjRefNMask) * SubjRefNMask
                End Get
                Set(ByVal value As SubjectReferenceNumbers)
                    If value <> 0 AndAlso Array.IndexOf([Enum].GetValues(GetType(SubjectReferenceNumbers)), value) < 0 Then Throw New InvalidEnumArgumentException("value", value, GetType(SubjectReferenceNumbers))
                    SubjectReferenceNumber = value
                End Set
            End Property
            ''' <summary>Matter component of <see cref="SubjectReferenceNumber"/></summary>
            ''' <remarks>A Subject Matter further refines the Subject of a News Object.</remarks>
            ''' <value>New value for subject matter number. Setting this properry resets <see cref="SubjectDetailNumber"/> to zero</value>
            ''' <returns>Subject matter number value or zero if none specified</returns>
            ''' <exception cref="InvalidEnumArgumentException">Val�ue being set is not member of <see cref="SubjectMatterNumbers"/> and it is not zero</exception>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            <DisplayName("Subject Matter Number"), Description("Matter component of Subject Reference Number")> _
            Public Property SubjectMatterNumber() As SubjectMatterNumbers 'Localize: Description and DisplayName
                Get
                    Return ((SubjectReferenceNumber Mod SubjRefNMask) \ SubjMatterMask) * SubjMatterMask
                End Get
                Set(ByVal value As SubjectMatterNumbers)
                    If value <> 0 AndAlso Array.IndexOf([Enum].GetValues(GetType(SubjectMatterNumbers)), value) < 0 Then Throw New InvalidEnumArgumentException("value", value, GetType(SubjectMatterNumbers))
                    SubjectReferenceNumber = SubjectNumber + (value Mod SubjRefNMask)
                End Set
            End Property
            ''' <summary>Detail component of <see cref="SubjectReferenceNumber"/></summary>
            ''' <remarks>A Subject Detail further refines the Subject Matter of a News Object.</remarks>
            ''' <value>New value for subject detail number</value>
            ''' <returns>Subject detail number value or zero if none specified</returns>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="EconomySubjectDetail"/> and it is not zero</exception>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            <DisplayName("Subject Detail Number"), Description("Detail component of Subject Reference Number")> _
            Public Property SubjectDetailNumber() As EconomySubjectDetail 'Localize: Description and DisplayName
                Get
                    Return SubjectReferenceNumber Mod SubjMatterMask
                End Get
                Set(ByVal value As EconomySubjectDetail)
                    If value <> 0 AndAlso Array.IndexOf([Enum].GetValues(GetType(EconomySubjectDetail)), value) < 0 Then Throw New InvalidEnumArgumentException("value", value, GetType(EconomySubjectDetail))
                    SubjectReferenceNumber = SubjectNumber + (SubjectMatterNumber Mod SubjRefNMask) + (value Mod SubjMatterMask)
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="SubjectName"/> property</summary>              
            <EditorBrowsable(EditorBrowsableState.Never)> Private _SubjectName As String
            ''' <summary>A text representation of the <see cref="SubjectNumber"/> (maximum 64 octets) consisting of graphic characters plus spaces either in English, as defined in Appendix H, or in the language of the service as indicated in DataSet <see cref="LanguageIdentifier"/> (2:135)</summary>
            ''' <remarks>The Subject identifies the general content of the objectdata as determined by the provider.</remarks>
            ''' <exception cref="ArgumentException">Value being set is longer than 64 characters -or- value being set contains non-graphic character or * or ? or :</exception>
            <DisplayName("Subject Name"), Description("A text representation of the Subject Number (maximum 64 octets) consisting of graphic characters plus spaces either in English, as defined in Appendix H, or in the language of the service as indicated in DataSet Language Identifier (2:135)")> _
            Public Property SubjectName() As String 'Localize: DisplayName and Description
                Get
                    Return _SubjectName
                End Get
                Set(ByVal value As String)
                    If value.Length > 64 Then Throw New ArgumentException("Lenght of SubjectName must fit into 64")
                    If Not IsGraphicCharacters(value) OrElse value.Contains("*"c) OrElse value.Contains("?"c) OrElse value.Contains(":") Then Throw New ArgumentException("SubjectName can contain only graphic characters except :, ? and *")
                    _SubjectName = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="SubjectMatterName"/> property</summary>                         
            <EditorBrowsable(EditorBrowsableState.Never)> Private _SubjectMatterName As String
            ''' <summary>A text representation of the <see cref="SubjectMatterNumber"/></summary>
            ''' <remarks>Maximum 64 octets consisting of graphic characters plus spaces either in English, as defined in Appendix I, or in the language of the service as indicated in DataSet <see cref="LanguageIdentifier"/> (2:135). A Subject Matter further refines the Subject of a News Object.</remarks>
            ''' <exception cref="ArgumentException">Value being set is longer than 64 characters -or- value being set contains non-graphic character or * or ? or :</exception>
            <DisplayName("Subject Matter Name"), Description("A text representation of the Subject Matter Number")> _
            Public Property SubjectMatterName() As String 'Localize: DisplayName and Description
                Get
                    Return _SubjectMatterName
                End Get
                Set(ByVal value As String)
                    If value.Length > 64 Then Throw New ArgumentException("Lenght of SubjectReference must fit into 64")
                    If Not IsGraphicCharacters(value) OrElse value.Contains("*"c) OrElse value.Contains("?"c) OrElse value.Contains(":") Then Throw New ArgumentException("SubjectReference can contain only graphic characters except :, ? and *")
                    _SubjectMatterName = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="SubjectDetailName"/> property</summary>                         
            <EditorBrowsable(EditorBrowsableState.Never)> Private _SubjectDetailName As String
            ''' <summary>A text representation of the <see cref="SubjectDetailNumber"/></summary>
            ''' <remarks>
            ''' Maximum 64 octets consisting of graphic characters plus spaces either in English, as defined in Appendix J, or in the language of the service as indicated in DataSet <see cref="LanguageIdentifier"/> (2:135)
            ''' <para>A Subject Detail further refines the Subject Matter of a News Object. A registry of Subject Reference Numbers, Subject Matter Names and Subject Detail Names, descriptions (if available) and their corresponding parent Subjects will be held by the IPTC in different languages, with translations as supplied by members. See Appendices I and J.</para></remarks>
            ''' <exception cref="ArgumentException">Value being set is longer than 64 characters -or- value being set contains non-graphic character or * or ? or :</exception>
            <DisplayName("Subject Detail Name"), Description("A text representation of the Subject Detail Number")> _
            Public Property SubjectDetailName() As String 'Localize: DisplayName and Description
                Get
                    Return _SubjectDetailName
                End Get
                Set(ByVal value As String)
                    If value.Length > 64 Then Throw New ArgumentException("Lenght of SubjectDetailName must fit into 64")
                    If Not IsGraphicCharacters(value) OrElse value.Contains("*"c) OrElse value.Contains("?"c) OrElse value.Contains(":") Then Throw New ArgumentException("SubjectDetailName can contain only graphic characters except :, ? and *")
                    _SubjectDetailName = value
                End Set
            End Property
            ''' <summary>String representation if form <see cref="IPR"/>:<see cref="SubjectReferenceNumber"/>:<see cref="SubjectName"/>:<see cref="SubjectMatterName"/>:<see cref="SubjectDetailName"/></summary>
            Public Overrides Function ToString() As String
                Return String.Format(InvariantCulture, "{0}:{1:00000000}:{2}:{3}:{4}", IPR, SubjectReferenceNumber, SubjectName, SubjectMatterName, SubjectDetailName)
            End Function
            ''' <summary>CTor</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor from array of bytes</summary>
            ''' <param name="Bytes">Bytes to construct new instance from</param>
            ''' <param name="Encoding">Encoding used to decode names</param>
            ''' <exception cref="IndexOutOfRangeException">There are more than 5 :-separated parts in <paramref name="Bytes"/></exception>
            ''' <exception cref="ArgumentException">There are less or more :-separated parts in <paramref name="Bytes"/></exception>
            Public Sub New(ByVal Bytes As Byte(), ByVal Encoding As System.Text.Encoding)
                Dim Parts(4) As Pair(Of Integer)
                Dim LastColon As Integer = -1
                Dim PartI As Integer = 0
                For i As Integer = 0 To Bytes.Length - 1
                    If Bytes(i) = AscW(":"c) Then
                        Parts(PartI) = New Pair(Of Integer)(LastColon + 1, i - LastColon + 1)
                        PartI += 1
                        LastColon = i
                    End If
                Next i
                If PartI <> 5 Then Throw New ArgumentException("SubjectReference must contain exactly 5 parts")
                Me.IPR = System.Text.Encoding.ASCII.GetString(Bytes, Parts(0).Value1, Parts(0).Value2)
                Me.SubjectReferenceNumber = System.Text.Encoding.ASCII.GetString(Bytes, Parts(1).Value1, Parts(1).Value2)
                If Parts(2).Value2 > 0 Then
                    Me.SubjectName = Encoding.GetString(Bytes, Parts(2).Value1, Parts(2).Value2)
                End If
                If Parts(3).Value2 > 0 Then
                    Me.SubjectMatterName = Encoding.GetString(Bytes, Parts(3).Value1, Parts(3).Value2)
                End If
                If Parts(4).Value2 > 0 Then
                    Me.SubjectDetailName = Encoding.GetString(Bytes, Parts(4).Value1, Parts(4).Value2)
                End If
            End Sub
            ''' <summary>Serializes current instance into array of bytes</summary>
            ''' <param name="Encoding">Encoding used to encode names</param>
            ''' <returns>Array of bytes containing serialization of this instance according to the IPTC standard</returns>
            ''' <exception cref="InvalidOperationException">Length of any serialized part violates IPTC specification (that is <see cref="IPR"/> must serialize to array of 1�32 items, <see cref="SubjectReferenceNumber"/> must serialize into array of 8 items and names must serialize into array of 0 to 64 items)</exception>
            Public Function ToBytes(ByVal Encoding As System.Text.Encoding) As Byte()
                Dim Bytes(4)() As Byte
                Bytes(0) = System.Text.Encoding.ASCII.GetBytes(Me.IPR)
                Bytes(1) = System.Text.Encoding.ASCII.GetBytes(Me.SubjectReferenceNumber.ToString(New String("0"c, 8), InvariantCulture))
                Bytes(2) = Encoding.GetBytes(Me.SubjectName)
                Bytes(3) = Encoding.GetBytes(Me.SubjectMatterName)
                Bytes(4) = Encoding.GetBytes(Me.SubjectDetailName)
                If Bytes(0).Length > 32 Or Bytes(0).Length < 1 Then Throw New InvalidOperationException("Length of serialized IPR is not within range 1�32 bytes")
                If Bytes(1).Length <> 8 Then Throw New InvalidOperationException("Lenght of serialized SubjectreferenceNumber diffrs from 8 bytes")
                If Bytes(2).Length > 64 OrElse Bytes(3).Length > 64 OrElse Bytes(4).Length > 64 Then Throw New InvalidOperationException("Lenght of serialized name exceeds 64 bytes")
                Dim arr(Bytes(0).Length + Bytes(1).Length + Bytes(2).Length + Bytes(3).Length + Bytes(4).Length - 1) As Byte
                Dim CurrPos As Integer = 0
                For i As Integer = 0 To 4
                    Bytes(i).CopyTo(arr, CurrPos)
                    CurrPos += Bytes(i).Length
                Next i
                Return arr
            End Function
        End Class
        ''' <summary>Common base for classes that have the <see cref="WithIPR.IPR"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public MustInherit Class WithIPR
            ''' <summary>Contains value of the <see cref="IPR"/> property</summary>            
            <EditorBrowsable(EditorBrowsableState.Never)> Private _IPR As String = " "c
            ''' <summary>Information Provider Reference A name, registered with the IPTC/NAA, identifying the provider that guarantees the uniqueness of the UNO</summary>            
            ''' <remarks>A name, registered with the IPTC/NAA, identifying the provider that guarantees the uniqueness of the UNO</remarks>
            ''' <value>A minimum of one and a maximum of 32 octets. A string of graphic characters, except colon �:� solidus �/�, asterisk �*� and question mark �?�, registered with, and approved by, the IPTC.</value>
            ''' <exception cref="ArgumentException">Value being set contains unallowed characters (white space, *, :, /, ? or control characters) -or- value being set is an empty <see cref="String"/> or its <see cref="String.Length"/> if more than 32 -or- length of value being set exceeds <see cref="IPRLengthLimit"/> -or- value being set contains character with code higher than 127</exception>
            <Description("Information Provider Reference A name, registered with the IPTC/NAA, identifying the provider that guarantees the uniqueness of the UNO")> _
            Public Overridable Property IPR() As String 'Localize: Description
                Get
                    Return _IPR
                End Get
                Set(ByVal value As String)
                    If Not IsGraphicCharacters(value) Then Throw New ArgumentException("Only graphic characters are allowd in IPR")
                    If value.Contains("*"c) OrElse value.Contains("/"c) OrElse value.Contains("?"c) OrElse value.Contains(":"c) Then Throw New ArgumentException("IPR cannot contain characters *, /, ? and :")
                    If value = "" OrElse value.Length > 32 Then Throw New ArgumentException("IPR must be string with length from 1 to 32 characters")
                    If value.Length > IPRLengthLimit Then Throw New ArgumentException("The lenght of IPR exceeds limit")
                    For Each ch As Char In value
                        If AscW(ch) > 127 Then Throw New ArgumentException("IPR text must be encodeable by ASCII")
                    Next ch
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
        ''' <summary>Represents IPTC UNO unique object identifier (IPTC type <see cref="IPTCTypes.UNO"/>)</summary>
        ''' <remarks>
        ''' <para>The first three elements of the UNO (the UCD, the IPR and the ODE) together are allocated to the editorial content of the object.</para>
        ''' <para>Any technical variants or changes in the presentation of an object, e.g. a picture being presented by a different file format, does not require the allocation of a new ODE but can be indicated by only generating a new OVI.</para>
        ''' <para>Links may be set up to the complete UNO but the structure provides for linking to selected elements, e.g. to all objects of a specified provider.</para>
        ''' </remarks>
        Public Class iptcUNO : Inherits WithIPR
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
            ''' <summary>CTor</summary>
            ''' <param name="UCD">UNO Creation Date</param>
            ''' <param name="IPR">Information Provider Reference</param>
            ''' <param name="ODE">Object Descriptor element</param>
            ''' <param name="OVI">Object Variant Indicator</param>
            ''' <exception cref="ArgumentException">
            ''' <paramref name="IPR"/> contains unallowed characters (white space, *, :, /, ? or control characters or over code 127) -or- <paramref name="IPR"/> set is an empty <see cref="String"/> or its <see cref="String.Length"/> if more than 32 -or- length of <paramref name="IPR"/> exceeds <see cref="IPRLengthLimit"/> -or-
            ''' <paramref name="OVI"/> contains unallowed characters (white space, *, :, /, ? or control characters or over code 127) -or- <paramref name="OVI"/> is an empty <see cref="String"/> or its lenght is larger than 9
            ''' </exception>
            ''' <exception cref="OperationCanceledException">
            ''' <paramref name="ODE"/> contains and invalid item (containing invalid characters (?,:,?,* or code over 127), too long or an empty string) or accumulated lenght of <see cref="IPR"/> and <see cref="ODE"/> (including <see cref="IPR"/>-<see cref="ODE"/> separator and separators of items of <see cref="ODE"/>) is greater than 61 -or- <paramref name="ODE"/> contains no item</exception>
            Public Sub New(ByVal UCD As Date, ByVal IPR As String, ByVal ODE As IEnumerable(Of String), ByVal OVI As String)
                Me.New()
                Me.UCD = UCD
                Me.IPR = IPR
                For Each item As String In ODE
                    Me.ODE.Add(item)
                Next item
                Me.ODE.RemoveAt(0)
                Me.OVI = OVI
            End Sub
            ''' <summary>CTor from byte array</summary>
            ''' <param name="Bytes">Bytes to initialize new instance by</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Bytes"/> is null or empty</exception>
            ''' <exception cref="ArgumentException">IPR or OVI part is invalid: contains unallowed charactes (white space, *, :, /, ? or over code 127), is empty or violates lenght constraint. See <seealso cref="OVI"/> and <seealso cref="IPR"/> for more information</exception>
            ''' <exception cref="IndexOutOfRangeException">There is not enough (4) parts separated by : in <paramref name="Bytes"/></exception>
            ''' <exception cref="ArgumentException">UCD component is to short or contains invalid date</exception>
            ''' <exception cref="InvalidCastException">UCD component contains non-numeric character</exception>
            ''' <exception cref="OperationCanceledException">ODE part is invalid. See <seealso cref="ODE"/> for more information.</exception>
            Public Sub New(ByVal Bytes As Byte())
                Me.New()
                If Bytes Is Nothing OrElse Bytes.Length = 0 Then Throw New ArgumentNullException("Bytes")
                Dim Text As String = System.Text.Encoding.ASCII.GetString(Bytes)
                Dim Parts As String() = Text.Split(":"c)
                'UCD:IPR:ODE1/ODE2/ODE3:OVI
                Me.UCD = New Date(Parts(0).Substring(0, 4), Parts(0).Substring(4, 2), Parts(0).Substring(6, 2))
                Me.IPR = Parts(1)
                Dim ODEs As String() = Parts(2).Split("/"c)
                For Each ODE As String In ODEs
                    Me.ODE.Add(ODE)
                Next ODE
                Me.ODE.RemoveAt(0)
                Me.OVI = Parts(3)
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
                    For Each ch As Char In e.Item
                        If AscW(ch) > 127 Then
                            e.Cancel = True
                            e.CancelMessage = "ODE component must be encodeable by ASCII"
                        End If
                    Next ch
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
            ''' <value>A minimum of one and a maximum of 60 minus the number of IPR octets, consisting of graphic characters, except colon �:� asterisk �*� and question mark �?�. The provider bears the responsibility for the uniqueness of the ODE within a 24 hour cycle.</value>
            ''' <exception cref="OperationCanceledException">
            ''' The <see cref="ListWithEvents(Of String).Add"/> and <see cref="ListWithEvents(Of String).Item"/>'s setter can throw an <see cref="OperationCanceledException"/> when trying to add invalid item (containing invalid characters (?,:,?,* or with code over 127), too long or an empty string) or accumulated lenght of <see cref="IPR"/> and <see cref="ODE"/> (including <see cref="IPR"/>-<see cref="ODE"/> separator and separators of items of <see cref="ODE"/>) is greater than 61
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
            ''' <value>A minimum of one and a maximum of 9 octets, consisting of graphic characters, except colon �:�, asterisk �*� and question mark �?�. To indicate a technical variation of the object as so far identified by the first three elements. Such variation may be required, for instance, for the indication of part of the object, or variations of the file format, or coded character set. The default value is a single �0� (zero) character indicating no further use of the OVI.</value>
            ''' <exception cref="ArgumentException">Value being set contains unallowed characters (white space, *, :, /, ? or control characters) -or- value being set is an empty <see cref="String"/> or its lenght is larger than 9 -or- value being set contains character with code higher than 127</exception>
            Public Property OVI() As String
                Get
                    Return _OVI
                End Get
                Set(ByVal value As String)
                    If Not IsGraphicCharacters(value) Then Throw New ArgumentException("Only graphic characters are allowd in OVI")
                    If value.Contains("*"c) OrElse value.Contains("/"c) OrElse value.Contains("?"c) OrElse value.Contains(":"c) Then Throw New ArgumentException("OVI cannot contain characters *, /, ? and :")
                    If value.Length > 9 OrElse value = "" Then Throw New ArgumentException("OVI must be string with length from 1 to 9")
                    For Each ch As Char In value
                        If AscW(ch) > 127 Then Throw New ArgumentException("OVI text must be encodeable by ASCII")
                    Next ch
                    _OVI = value
                End Set
            End Property
            ''' <summary>String representation in form UCD:IPR:ODE1/ODE2/ODE3:OVI</summary>
            Overrides Function ToString() As String
                Dim ODEArr(ODE.Count - 1) As String
                ODE.CopyTo(ODEArr, 0)
                Return String.Format(InvariantCulture, "{0:YYYYMMDD}:{1}:{2}:{3}", UCD, IPR, String.Join("/"c, ODEArr), OVI)
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
            ''' <exception cref="ArgumentException">Number being set converted to string is longer than 2 <see cref="NumberDigits"/></exception>
            ''' <exception cref="ArgumentOutOfRangeException">Number beign set is negative</exception>
            Public Overridable Property Number() As Integer
                Get
                    Return _Number
                End Get
                Set(ByVal value As Integer)
                    If Number.ToString(New String("0"c, NumberDigits), InvariantCulture).Length > NumberDigits Then Throw New ArgumentException("Number has to many digits")
                    If Number < 0 Then Throw New ArgumentOutOfRangeException("Number cannot be negative")
                    _Number = value
                End Set
            End Property
            ''' <summary>Text of this <see cref="NumStr"/></summary>                        
            Public Overridable Property [String]() As String
                Get
                    Return _String
                End Get
                Set(ByVal value As String)
                    _String = value
                End Set
            End Property
            ''' <summary>String representation in format number;string</summary>
            Public NotOverridable Overrides Function ToString() As String
                Return String.Format(InvariantCulture, "{0:" & New String("0"c, NumberDigits) & "};{1}", Number, [String])
            End Function
        End Class
        ''' <summary>Represents combination of 2-digits numer and string (IPTC type <see cref="IPTCTypes.Num2_Str"/>)</summary>
        Public Class NumStr2 : Inherits NumStr
            ''' <summary>Number of digits in number</summary>
            ''' <returns>2</returns>
            Protected Overrides ReadOnly Property NumberDigits() As Byte
                Get
                    Return 2
                End Get
            End Property
        End Class
        ''' <summary><see cref="T:Tools.DrawingT.MetadataT.IPTC.NumStr2"/> with numbers from enum</summary>
        <CLSCompliant(False)> _
        Public Class NumStr2(Of T As {IConvertible, Structure}) : Inherits NumStr2
            ''' <summary>Number in this <see cref="NumStr2(Of T)"/></summary>            
            ''' <exception cref="ArgumentException">Number being set converted to string is longer than 2 <see cref="NumberDigits"/> -or- <see cref="T"/> is not <see cref="[Enum]"/></exception>
            ''' <exception cref="ArgumentOutOfRangeException">Number beign set is negative</exception>
            ''' <exception cref="InvalidEnumArgumentException"><see cref="T"/> has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> True or it has no <see cref="RestrictAttribute"/> and value being set is not member of <see cref="T"/></exception>
            Public Overridable Property EnumNumber() As T
                Get
                    Return CObj(MyBase.Number)
                End Get
                Set(ByVal value As T)
                    Dim Attrs As Object() = GetType(T).GetCustomAttributes(GetType(RestrictAttribute), True)
                    If ((Attrs IsNot Nothing AndAlso Attrs.Length > 0 AndAlso DirectCast(Attrs(0), RestrictAttribute).Restrict) OrElse (Attrs Is Nothing OrElse Attrs.Length = 0)) AndAlso Not InEnum(value) Then
                        Throw New InvalidEnumArgumentException("value", value.ToInt32(InvariantCulture), GetType(T))
                    End If
                    MyBase.Number = CObj(value)
                End Set
            End Property
            ''' <summary>Number in this <see cref="NumStr"/></summary>            
            ''' <exception cref="ArgumentException">Number being set converted to string is longer than 2 <see cref="NumberDigits"/> -or- <see cref="T"/> is not <see cref="[Enum]"/></exception>
            ''' <exception cref="ArgumentOutOfRangeException">Number beign set is negative</exception>
            ''' <exception cref="InvalidEnumArgumentException"><see cref="T"/> has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> True or it has no <see cref="RestrictAttribute"/> and value being set is not member of <see cref="T"/></exception>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Public NotOverridable Overrides Property Number() As Integer
                Get
                    Return EnumNumber.ToInt32(InvariantCulture)
                End Get
                Set(ByVal value As Integer)
                    EnumNumber = CObj(Number)
                End Set
            End Property
        End Class
        ''' <summary><see cref="T:Tools.DrawingT.MetadataT.IPTC.NumStr3"/> with numbers from enum</summary>
        <CLSCompliant(False)> _
        Public Class NumStr3(Of T As {IConvertible, Structure}) : Inherits NumStr3
            ''' <summary>Number in this <see cref="NumStr3(Of T)"/></summary>            
            ''' <exception cref="ArgumentException">Number being set converted to string is longer than 3 <see cref="NumberDigits"/> -or- <see cref="T"/> is not <see cref="[Enum]"/></exception>
            ''' <exception cref="ArgumentOutOfRangeException">Number beign set is negative</exception>
            ''' <exception cref="InvalidEnumArgumentException"><see cref="T"/> has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> True or it has no <see cref="RestrictAttribute"/> and value being set is not member of <see cref="T"/></exception>
            Public Overridable Property EnumNumber() As T
                Get
                    Return CObj(MyBase.Number)
                End Get
                Set(ByVal value As T)
                    Dim Attrs As Object() = GetType(T).GetCustomAttributes(GetType(RestrictAttribute), True)
                    If ((Attrs IsNot Nothing AndAlso Attrs.Length > 0 AndAlso DirectCast(Attrs(0), RestrictAttribute).Restrict) OrElse (Attrs Is Nothing OrElse Attrs.Length = 0)) AndAlso Not InEnum(value) Then
                        Throw New InvalidEnumArgumentException("value", value.ToInt32(InvariantCulture), GetType(T))
                    End If
                    MyBase.Number = CObj(value)
                End Set
            End Property
            ''' <summary>Number in this <see cref="NumStr"/></summary>            
            ''' <exception cref="ArgumentException">Number being set converted to string is longer than 3 <see cref="NumberDigits"/> -or- <see cref="T"/> is not <see cref="[Enum]"/></exception>
            ''' <exception cref="ArgumentOutOfRangeException">Number beign set is negative</exception>
            ''' <exception cref="InvalidEnumArgumentException"><see cref="T"/> has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> True or it has no <see cref="RestrictAttribute"/> and value being set is not member of <see cref="T"/></exception>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Public NotOverridable Overrides Property Number() As Integer
                Get
                    Return EnumNumber.ToInt32(InvariantCulture)
                End Get
                Set(ByVal value As Integer)
                    EnumNumber = CObj(Number)
                End Set
            End Property
        End Class
        ''' <summary>Represents combination of 3-digits numer and string (IPTC type <see cref="IPTCTypes.Num3_Str"/>)</summary>
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
        ''' <summary>Represents date (Year, Month and Day) which's parts can be ommited by setting value to 0 (IPTC type <see cref="IPTCTypes.CCYYMMDDOmmitable"/>)</summary>
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
                    If value < 0 OrElse value > 9999 Then Throw New ArgumentOutOfRangeException("value", "Year must be from range 1�9999 or 0 if unknown")
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
                Return String.Format(InvariantCulture, "{0:0000}{1:00}{2:00}", Year, Month, Day)
            End Function
        End Structure

        ''' <summary>Contains time as hours, minutes and seconds and offset to UTC in hours and minutes (IPTC type <see cref="IPTCTypes.HHMMSS_HHMM"/>)</summary>
        <TypeConverter(GetType(Time.Converter))> _
        Public Structure Time
            ''' <summary>Contains value of the <see cref="Time"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Time As TimeSpan
            ''' <summary>Contains value of the <see cref="Offset"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Offset As TimeSpan
            ''' <summary>Minimal allowed value of the <see cref="Offset"/> property</summary>
            Public Shared ReadOnly MinOffset As TimeSpan = TimeSpan.FromHours(-12)
            ''' <summary>Maximal allowed value of the <see cref="Offset"/> property</summary>
            Public Shared ReadOnly MaxOffset As TimeSpan = TimeSpan.FromHours(14)
            ''' <summary>Minimal allowed value of the <see cref="Time"/> property</summary>
            ''' <remarks>It's 23:59:59</remarks>
            Public Shared ReadOnly Maximum As New TimeSpan(23, 59, 59)
            ''' <summary>maximal allowed value of the <see cref="Time"/> property</summary>
            ''' <remarks>It's zero</remarks>
            Public Shared ReadOnly Minimum As TimeSpan = TimeSpan.Zero
            ''' <summary>Local time</summary>
            ''' <value>Sub-second part of value is ignored (truncated)</value>
            ''' <exception cref="ArgumentOutOfRangeException">Settign value otside of range <see cref="Minimum"/>�<see cref="Maximum"/></exception>
            Public Property Time() As TimeSpan
                Get
                    Return _Time
                End Get
                Set(ByVal value As TimeSpan)
                    If value < Minimum OrElse value > Maximum Then Throw New ArgumentOutOfRangeException("value", "Time must be positive and less than 99:59:59")
                    _Time = TimeSpan.FromSeconds(Math.Truncate(value.TotalSeconds))
                End Set
            End Property
            ''' <summary>Time zone offset of <see cref="Time"/></summary>
            ''' <exception cref="ArgumentException">Setting offset to time with non-zero sub-minute component</exception>
            ''' <exception cref="ArgumentOutOfRangeException">Setting offset outside of range <see cref="MinOffset"/>�<see cref="MaxOffset"/></exception>
            Public Property Offset() As TimeSpan
                Get
                    Return _Offset
                End Get
                Set(ByVal value As TimeSpan)
                    If Offset.TotalMinutes <> Int(Offset.TotalMinutes) Then Throw New ArgumentException("Offset must be in whole minutes")
                    If value < MinOffset OrElse value > MaxOffset Then Throw New ArgumentOutOfRangeException(String.Format("value", "Offset must be within range {0}�{1}", MinOffset, MaxOffset))
                    _Offset = value
                End Set
            End Property
#Region "Component properties"
            ''' <summary>Hour component of <see cref="Time"/></summary>
            ''' <exception cref="ArgumentOutOfRangeException">setting such value that <see cref="Time"/> leves range <see cref="Minimum"/>�<see cref="Maximum"/></exception>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
            Public Property Hours() As Byte
                Get
                    Return Time.Hours
                End Get
                Set(ByVal value As Byte)
                    Time = New TimeSpan(value, 0, Math.Truncate(Time.TotalSeconds) - Math.Truncate(Time.TotalHours) * 60 * 60)
                End Set
            End Property
            ''' <summary>Hour component of <see cref="Time"/></summary>
            ''' <exception cref="ArgumentOutOfRangeException">setting such value that <see cref="Time"/> leves range <see cref="Minimum"/>�<see cref="Maximum"/></exception>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
            Public Property Minutes() As Byte
                Get
                    Return Time.Minutes
                End Get
                Set(ByVal value As Byte)
                    Time = New TimeSpan(Math.Truncate(Time.TotalHours), value, Time.Seconds)
                End Set
            End Property
            ''' <summary>Second component of <see cref="Time"/></summary>
            ''' <exception cref="ArgumentOutOfRangeException">setting such value that <see cref="Time"/> leves range <see cref="Minimum"/>�<see cref="Maximum"/></exception>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
            Public Property Seconds() As Byte
                Get
                    Return Time.Seconds
                End Get
                Set(ByVal value As Byte)
                    Time = New TimeSpan(0, Math.Truncate(Time.TotalMinutes), value)
                End Set
            End Property
            ''' <summary>Absolute value of hour component of <see cref="Offset"/></summary>
            ''' <exception cref="ArgumentOutOfRangeException">Setting value such that <see cref="Offset"/> leaves range <see cref="MinOffset"/>�<see cref="MaxOffset"/></exception>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
            Public Property OffsetHourAbs() As Byte
                Get
                    Return Math.Abs(Offset.Hours)
                End Get
                Set(ByVal value As Byte)
                    Offset = New TimeSpan(value, Math.Truncate(Offset.TotalMinutes) - Math.Truncate(Offset.TotalHours) * 60, 0)
                End Set
            End Property
            ''' <summary>Sign of <see cref="Offset"/></summary>
            ''' <exception cref="ArgumentOutOfRangeException">Setting value such that <see cref="Offset"/> leaves range <see cref="MinOffset"/>�<see cref="MaxOffset"/></exception>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
            Public Property NegativeOffset() As Boolean
                Get
                    Return Offset < TimeSpan.Zero
                End Get
                Set(ByVal value As Boolean)
                    If value <> NegativeOffset Then Offset = -Offset
                End Set
            End Property
            ''' <summary>Absolute value of minute part of <see cref="Offset"/></summary>
            ''' <exception cref="ArgumentOutOfRangeException">Setting value such that <see cref="Offset"/> leaves range <see cref="MinOffset"/>�<see cref="MaxOffset"/></exception>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
            Public Property OffsetMinuteAbs() As Byte
                Get
                    Return Math.Abs(Offset.Minutes)
                End Get
                Set(ByVal value As Byte)
                    Offset = New TimeSpan(Math.Truncate(Offset.TotalHours), value, 0)
                End Set
            End Property
#End Region
            ''' <summary>String representation in the HHMMSS�HHMM format</summary>
            Public Overrides Function ToString() As String
                Return String.Format(InvariantCulture, "{0:00}{1:00}{2:00}{3}{4:00}{5:00}", Hours, Minutes, Seconds, iif(Time < TimeSpan.Zero, "-"c, "+"c), OffsetHourAbs, OffsetMinuteAbs)
            End Function
#Region "CTors"
            ''' <summary>CTor</summary>
            ''' <param name="Hours">Hour component</param>
            ''' <param name="Minutes">Minute component</param>
            ''' <param name="Seconds">Second component</param>
            ''' <param name="HourOffset">Hour component of offset</param>
            ''' <param name="MinuteOffset">Minute component of offset</param>
            ''' <exception cref="ArgumentOutOfRangeException">Time component exceeds range <see cref="Minimum"/>�<see cref="Maximum"/> -or- offset component exceds range <see cref="MinOffset"/>�<see cref="MaxOffset"/></exception>
            <CLSCompliant(False)> _
            Public Sub New(ByVal Hours As Byte, ByVal Minutes As Byte, ByVal Seconds As Byte, Optional ByVal HourOffset As SByte = 0, Optional ByVal MinuteOffset As Byte = 0)
                Me.New(New TimeSpan(Hours, Minutes, Seconds), New TimeSpan(HourOffset, MinuteOffset, 0))
            End Sub
            ''' <summary>CTor from <see cref="TimeSpan"/></summary>
            ''' <param name="Time"><see cref="TimeSpan"/> to initialize this instance (time local in UTC+0:00)</param>
            ''' <remarks>Offset is initialized to <see cref="TimeSpan.Zero"/></remarks>
            ''' <exception cref="ArgumentOutOfRangeException">Time component exceeds range <see cref="Minimum"/>�<see cref="Maximum"/> -or- offset component exceds range <see cref="MinOffset"/>�<see cref="MaxOffset"/></exception>
            Public Sub New(ByVal Time As TimeSpan)
                Me.New(Time, TimeSpan.Zero)
            End Sub
            ''' <summary>CTor from <see cref="TimeSpan"/></summary>
            ''' <param name="Time"><see cref="TimeSpan"/> to initialize this instance (local time)</param>
            ''' <param name="Offset">Time zone offset</param>
            ''' <exception cref="ArgumentOutOfRangeException">Time component exceeds range <see cref="Minimum"/>�<see cref="Maximum"/> -or- offset component exceds range <see cref="MinOffset"/>�<see cref="MaxOffset"/></exception>
            ''' <exception cref="ArgumentException"><paramref name="Offset"/> contains non-zero sub-minute component</exception>
            Public Sub New(ByVal Time As TimeSpan, ByVal Offset As TimeSpan)
                Me.Time = Time
                Me.Offset = Offset
            End Sub
            ''' <summary>CTor from <see cref="Date"/></summary>
            ''' <param name="Date"><see cref="Date"/> which time path will be used to initialize this instance</param>
            ''' <remarks>Offset is initialized to <see cref="TimeSpan.Zero"/> (UTC+0:00)</remarks>
            Public Sub New(ByVal [Date] As Date)
                Me.New([Date].TimeOfDay)
            End Sub
#End Region
            ''' <summary>Converter of <see cref="Time"/> values</summary>
            Public Class Converter : Inherits TypeConverter(Of Time, String)

                ''' <summary>State of automat that parses string</summary>
                Private Enum ParseAutomat
                    ''' <summary>*HH:MM:SS�HH:MM</summary>
                    H1
                    ''' <summary>H*H:MM:SS�HH:MM</summary>
                    H2
                    ''' <summary>HH*:MM:SS�HH:MM</summary>
                    H3
                    ''' <summary>HH:*MM:SS�HH:MM</summary>
                    M1
                    ''' <summary>HH:M*M:SS�HH:MM</summary>
                    M2
                    ''' <summary>HH:MM*:SS�HH:MM</summary>
                    M3
                    ''' <summary>HH:MM:*SS�HH:MM</summary>
                    S1
                    ''' <summary>HH:MM:S*S�HH:MM</summary>
                    S2
                    ''' <summary>HH:MM:SS*�HH:MM</summary>
                    S3
                    ''' <summary>HH:MM:SS�*HH:MM</summary>
                    OH1
                    ''' <summary>HH:MM:SS�H*H:MM</summary>
                    OH2
                    ''' <summary>HH:MM:SS�HH*:MM</summary>
                    OH3
                    ''' <summary>HH:MM:SS�HH:*MM</summary>
                    OM1
                    ''' <summary>HH:MM:SS�HH:M*M</summary>
                    OM2
                    ''' <summary>HH:MM:SS�HH:MM*</summary>
                    All
                End Enum
                Public Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As String) As Time
                    Dim state As ParseAutomat = ParseAutomat.H1
                    Dim i As Integer = 0
                    Dim rMinus As Boolean = False
                    Dim rH, rM, rS, rOH, rOM As Byte
                    For Each ch As Char In CStr(value)
                        Select Case state
                            Case ParseAutomat.H1
                                Select Case ch
                                    Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                        state = ParseAutomat.H2
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.H2
                                Select Case ch
                                    Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                        rH = CStr(value).Substring(i - 1, 2)
                                        state = ParseAutomat.H3
                                    Case ":"c
                                        rH = CStr(value).Substring(i - 1, 1)
                                        state = ParseAutomat.M1
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.H3
                                Select Case ch
                                    Case ":"c : state = ParseAutomat.M1
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.M1
                                Select Case ch
                                    Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                        state = ParseAutomat.M2
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.M2
                                Select Case ch
                                    Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                        rM = CStr(value).Substring(i - 1, 2)
                                        state = ParseAutomat.M3
                                    Case ":"c
                                        rM = CStr(value).Substring(i - 1, 1)
                                        state = ParseAutomat.S1
                                    Case "-"c
                                        rM = CStr(value).Substring(i - 1, 1)
                                        rMinus = True
                                        state = ParseAutomat.OH1
                                    Case "+"c
                                        rM = CStr(value).Substring(i - 1, 1)
                                        state = ParseAutomat.OH1
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.M3
                                Select Case ch
                                    Case ":"c : state = ParseAutomat.S1
                                    Case "-"c
                                        rMinus = True
                                        state = ParseAutomat.OH1
                                    Case "+"c : state = ParseAutomat.OH1
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.S1
                                Select Case ch
                                    Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                        state = ParseAutomat.S2
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.S2
                                Select Case ch
                                    Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                        rS = CStr(value).Substring(i - 1, 2)
                                        state = ParseAutomat.S3
                                    Case "-"c
                                        rS = CStr(value).Substring(i - 1, 1)
                                        rMinus = True
                                        state = ParseAutomat.OH1
                                    Case "+"c
                                        rS = CStr(value).Substring(i - 1, 1)
                                        state = ParseAutomat.OH1
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.S3
                                Select Case ch
                                    Case "-"c
                                        rMinus = True
                                        state = ParseAutomat.OH1
                                    Case "+"c : state = ParseAutomat.OH1
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.OH1
                                Select Case ch
                                    Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                        state = ParseAutomat.OH2
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.OH2
                                Select Case ch
                                    Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                        rOH = CStr(value).Substring(i - 1, 2)
                                        state = ParseAutomat.OH3
                                    Case ":"c
                                        rOH = CStr(value).Substring(i - 1, 1)
                                        state = ParseAutomat.OM1
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.OH3
                                Select Case ch
                                    Case ":"c : state = ParseAutomat.OM1
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.OM1
                                Select Case ch
                                    Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                        state = ParseAutomat.OM2
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.OM2
                                Select Case ch
                                    Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                        rOM = CStr(value).Substring(i - 1, 2)
                                        state = ParseAutomat.All
                                    Case Else : Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                                End Select
                            Case ParseAutomat.All
                                Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                        End Select
                        i += 1
                    Next ch
                    Select Case state
                        Case ParseAutomat.OM2
                            rOM = CStr(value).Substring(CStr(value).Length - 1)
                        Case ParseAutomat.OH2
                            rOH = CStr(value).Substring(CStr(value).Length - 1)
                        Case ParseAutomat.S2
                            rS = CStr(value).Substring(CStr(value).Length - 1)
                        Case ParseAutomat.M2
                            rM = CStr(value).Substring(CStr(value).Length - 1)
                        Case ParseAutomat.All, ParseAutomat.OH3, ParseAutomat.S3, ParseAutomat.M3
                        Case Else
                            Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value))
                    End Select
                    Try
                        Dim Time As New TimeSpan(rH, rM, rS)
                        Dim Offset As New TimeSpan(rOH, rOM, 0)
                        If rMinus Then Offset = -Offset
                        Return New Time(Time, Offset)
                    Catch ex As Exception
                        Throw New InvalidCastException(String.Format("Cannot convert string ""{0}"" into IPTC.Time", value), ex)
                    End Try
                End Function
                Public Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Time) As String
                    With DirectCast(value, Time)
                        Return String.Format(InvariantCulture, "{0:0}:{1:00}:{2:00}{3}{4:0}:{5:00}", .Hours, .Minutes, .Seconds, VisualBasicT.iif(.NegativeOffset, "-"c, "+"c), .OffsetHourAbs, .OffsetMinuteAbs)
                    End With
                End Function
            End Class
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
        ''' <summary>IPTC image type (IPTC type <see cref="IPTCTypes.ImageType"/>)</summary>
        <TypeConverter(GetType(ExpandableObjectConverter))> _
        Public Structure iptcImageType : Implements IMediaType(Of ImageTypeComponents, ImageTypeContents)
            ''' <summary>Contains value of the <see cref="Type"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Type As ImageTypeContents
            ''' <summary>Contains value of the <see cref="Component"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Components As ImageTypeComponents
            ''' <summary>Type of components</summary>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="ImageTypeContents"/></exception>
            <Description("Type of components")> _
            Public Property Type() As ImageTypeContents Implements IMediaType(Of Tools.DrawingT.MetadataT.IPTC.ImageTypeComponents, Tools.DrawingT.MetadataT.IPTC.ImageTypeContents).Code 'Localize:Description
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
            <Description("Number of components")> _
            Public Property Components() As ImageTypeComponents Implements IMediaType(Of Tools.DrawingT.MetadataT.IPTC.ImageTypeComponents, Tools.DrawingT.MetadataT.IPTC.ImageTypeContents).Count 'Localize:Description
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
            <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
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
                Return String.Format(InvariantCulture, "{0}{1}", CByte(Components), TypeCode)
            End Function
        End Structure

        ''' <summary>IPTC audio type (IPTC type <see cref="IPTCTypes.AudioType"/>)</summary>
        <TypeConverter(GetType(ExpandableObjectConverter))> _
        Public Structure iptcAudioType : Implements IMediaType(Of Byte, AudioDataType)
            ''' <summary>Contains value of the <see cref="Type"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Type As AudioDataType
            ''' <summary>Contains value of the <see cref="Component"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Components As Byte
            ''' <summary>Type of components</summary>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="ImageTypeContents"/></exception>
            <Description("Type of components")> _
            Public Property Type() As AudioDataType Implements IMediaType(Of Byte, Tools.DrawingT.MetadataT.IPTC.AudioDataType).Code 'Localize:Description
                Get
                    Return _Type
                End Get
                Set(ByVal value As AudioDataType)
                    If Not InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, GetType(AudioDataType))
                    _Type = value
                End Set
            End Property
            ''' <summary>Number of components</summary>
            ''' <exception cref="ArgumentOutOfRangeException">Value being set is ot of range 0�9</exception>
            <Description("Number of components")> _
            Public Property Components() As Byte Implements IMediaType(Of Byte, Tools.DrawingT.MetadataT.IPTC.AudioDataType).Count 'Localize:Description
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
            <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            Public Property TypeCode() As Char Implements IMediaType(Of Byte, Tools.DrawingT.MetadataT.IPTC.AudioDataType).CodeString
                Get
                    Return DirectCast(GetConstant(Type).GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)(0), Xml.Serialization.XmlEnumAttribute).Name
                End Get
                Set(ByVal value As Char)
                    For Each Constant As Reflection.FieldInfo In GetType(AudioDataType).GetFields()
                        Dim Attrs As Object() = Constant.GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)
                        If Attrs IsNot Nothing AndAlso Attrs.Length > 0 Then
                            Type = Constant.GetValue(Nothing)
                            Return
                        End If
                    Next Constant
                    Throw New ArgumentException(String.Format("{0} cannot be interpreted as AudioDataType", value))
                End Set
            End Property
            ''' <summary>String representation in form 0T (components, type)</summary>
            Public Overrides Function ToString() As String
                Return String.Format(InvariantCulture, "{0}{1}", Components, TypeCode)
            End Function
        End Structure

        ''' <summary>Common base for all <see cref="StringEnum(Of TEnum)"/>s</summary>
        Public MustInherit Class StringEnum
            Implements IT1orT2(Of Decimal, String)
            ''' <summary>CTor</summary>
            ''' <remarks>Nobody else can inherit this class</remarks>
            Friend Sub New()
            End Sub
            ''' <summary>Creates a new object that is a copy of the current instance.</summary>
            ''' <returns>A new object that is a copy of this instance</returns>
            ''' <remarks>Use type-safe <see cref="Clone"/> instead</remarks>
            <Obsolete("Use type-safe Clone instead")> _
            Private Function Clone1() As Object Implements System.ICloneable.Clone
                Return CloneDec()
            End Function
            ''' <summary>Swaps values <see cref="DecimalValue"/> and <see cref="StringValue"/></summary>            
            Private Function Swap() As DataStructuresT.GenericT.IPair(Of String, Decimal) Implements DataStructuresT.GenericT.IPair(Of Decimal, String).Swap
                Return New Pair(Of String, Decimal)(StringValue, DecimalValue)
            End Function
            ''' <summary>Gets or sets enumerated value as <see cref="Decimal"/></summary>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="EnumType"/></exception>
            Public MustOverride Property DecimalValue() As Decimal Implements DataStructuresT.GenericT.IPair(Of Decimal, String).Value1, DataStructuresT.GenericT.IT1orT2(Of Decimal, String).value1
            ''' <summary>Gets or sets string value</summary>
            ''' <exception cref="ArgumentNullException">Value being set is null</exception>
            Public MustOverride Property StringValue() As String Implements DataStructuresT.GenericT.IPair(Of Decimal, String).Value2, DataStructuresT.GenericT.IT1orT2(Of Decimal, String).value2
            ''' <summary>Gets type of enumeration derived class contains</summary>
            Public MustOverride ReadOnly Property EnumType() As Type
            ''' <summary>Gets value indicating if this instance contains value of specified type</summary>
            ''' <returns>True oif derived class's <see cref="contains"/> returns true for <paramref name="T"/> or if <paramref name="T"/> is <see cref="Decimal"/> and derived class's <see cref="contains"/> returns true for <see cref="EnumType"/></returns>
            Private ReadOnly Property containsImpl(ByVal T As System.Type) As Boolean Implements DataStructuresT.GenericT.IT1orT2(Of Decimal, String).contains
                Get
                    Return (T.Equals(GetType(Decimal)) AndAlso contains(EnumType)) OrElse contains(T)
                End Get
            End Property
            ''' <summary>Gets value indicating if derived class contains value of givent type</summary>
            ''' <param name="T">Type of value to be contained</param>
            ''' <remarks>It can return false for <see cref="Decimal"/> even if <see cref="ContainsEnum"/> returns true</remarks>
            Public MustOverride ReadOnly Property contains(ByVal T As Type) As Boolean
            ''' <summary>Gets value indicating if derived class contains enumerated value</summary>
            Public MustOverride Property ContainsEnum() As Boolean Implements DataStructuresT.GenericT.IT1orT2(Of Decimal, String).contains1
            ''' <summary>Gets value indicating if derived class contains <see cref="String"/> value</summary>
            Public MustOverride Property ContainsString() As Boolean Implements DataStructuresT.GenericT.IT1orT2(Of Decimal, String).contains2
            ''' <summary>Gets value indicating if derived class is empty</summary>
            Public MustOverride ReadOnly Property IsEmpty() As Boolean Implements DataStructuresT.GenericT.IT1orT2(Of Decimal, String).IsEmpty
            ''' <summary>Gets value containde in derived class in type-unsafe way</summary>
            Public MustOverride Property objValue() As Object Implements DataStructuresT.GenericT.IT1orT2(Of Decimal, String).objValue
            ''' <summary>Clones instance of derived class as <see cref="IPair(Of Decimal, String)"/></summary>
            Public MustOverride Function CloneDec() As DataStructuresT.GenericT.IPair(Of Decimal, String) Implements ICloneable(Of DataStructuresT.GenericT.IPair(Of Decimal, String)).Clone
            ''' <summary>Creates instance <see cref="StringEnum(Of TEnum)"/> with TEnum set to given <see cref="Type"/> and initialized with given <see cref="String"/></summary>
            ''' <param name="Type">Type to pass to generic type parameter TEnum of <see cref="StringEnum(Of TEnum)"/></param>
            ''' <param name="Value"><see cref="String"/> to initialize new instance with</param>
            ''' <returns>New instance of <see cref="StringEnum(Of TEnum)"/> where TEnum is <paramref name="Type"/> initialized with <paramref name="Value"/></returns>
            ''' <exception cref="ArgumentException">Error while creating generic instance</exception>
            ''' <exception cref="ArgumentNullException"><paramref name="Value"/> or <paramref name="Type"/> is null</exception>
            Public Shared Function GetInstance(ByVal Type As Type, ByVal Value As String) As StringEnum
                Dim SEType As Type = GetType(StringEnum(Of )).MakeGenericType(Type)
                Dim instance As StringEnum = Activator.CreateInstance(SEType)
                instance.StringValue = Value
                Return instance
            End Function
        End Class
        ''' <summary>Type that can contain value of "string enum" even when such value is not member of this enum</summary>
        ''' <typeparam name="TEnum">Type of <see cref="P:Tools.DrawingT.MetadataT.IPTC.StringEnum`0.EnumValue"/>. Must inherit from <see cref="[Enum]"/></typeparam>
        <CLSCompliant(False), DebuggerDisplay("{ToString}")> _
        Public Class StringEnum(Of TEnum As {IConvertible, Structure})
            Inherits StringEnum
            Implements IT1orT2(Of TEnum, String)
            ''' <summary>Contains value of the <see cref="StringValue"/> property</summary>
            Private _StringValue As String
            ''' <summary>Contains value of the <see cref="EnumValue"/> property</summary>
            Private _EnumValue As TEnum
            ''' <summary>Contains value of the <see cref="ContainsEnum"/> property</summary>
            Private _ContainsEnum As Boolean
            ''' <summary>Creates a new object that is a copy of the current instance.</summary>
            ''' <returns>A new object that is a copy of this instance</returns>
            Public Function CloneEnum() As DataStructuresT.GenericT.IPair(Of TEnum, String) Implements ICloneable(Of DataStructuresT.GenericT.IPair(Of TEnum, String)).Clone
                Dim ret As New StringEnum(Of TEnum)
                ret._StringValue = Me._StringValue
                ret._EnumValue = Me._EnumValue
                ret._ContainsEnum = Me._ContainsEnum
                Return ret
            End Function
            ''' <summary>Creates a new object that is a copy of the current instance.</summary>
            ''' <returns>A new object that is a copy of this instance</returns>
            Public Overrides Function CloneDec() As DataStructuresT.GenericT.IPair(Of Decimal, String)
                Return CloneEnum()
            End Function

            ''' <summary>Swaps values <see cref="EnumValue"/> and <see cref="StringValue"/></summary>            
            Private Function Swap() As DataStructuresT.GenericT.IPair(Of String, TEnum) Implements DataStructuresT.GenericT.IPair(Of TEnum, String).Swap
                Return New Pair(Of String, TEnum)(StringValue, EnumValue)
            End Function

            ''' <summary>Gets or sets enumerated value</summary>
            ''' <value>Anything to set enumerated value and delete string value</value>
            ''' <returns>If this instance contains enumerated value then returns it, otherwise return 0</returns>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="TEnum"/></exception>
            Public Property EnumValue() As TEnum Implements DataStructuresT.GenericT.IPair(Of TEnum, String).Value1, IT1orT2(Of TEnum, String).value1
                Get
                    If ContainsEnum Then Return _EnumValue Else Return CObj(0)
                End Get
                Set(ByVal value As TEnum)
                    If Not InEnum(value) Then Throw New InvalidEnumArgumentException("EnumValue must be member of enumeration " & GetType(TEnum).Name)
                    _ContainsEnum = True
                    _EnumValue = value
                    _StringValue = Nothing
                End Set
            End Property
            ''' <summary>Gets or sets string value</summary>
            ''' <value>Anything non-null to set string value and delete enumerated value (if string value is name of enum item then <see cref="EnumValue"/> is set instead of <see cref="StringValue"/>. Value is considered to be name of enum if enum item has <see cref="Xml.Serialization.XmlEnumAttribute"/> and <see cref="Xml.Serialization.XmlEnumAttribute.Name"/> equals to value or when enum member has not <see cref="Xml.Serialization.XmlElementAttribute"/> and it's name is same as value.</value>
            ''' <returns>If this instance contains string value then returns it, otherwise returns name of enum item contained in this instace</returns>
            ''' <exception cref="ArgumentNullException">Value being set is null</exception>
            ''' <exception cref="ArgumentException">Value being set contains unallowed character (non-grapic-non-space-non-ASCII)</exception>
            Public Overrides Property StringValue() As String Implements DataStructuresT.GenericT.IPair(Of TEnum, String).Value2, IT1orT2(Of TEnum, String).value2
                Get
                    If ContainsEnum Then Return [Enum].GetName(GetType(TEnum), EnumValue) Else Return _StringValue
                End Get
                Set(ByVal value As String)
                    If value Is Nothing Then Throw New ArgumentNullException("value", "StringValue cannot be null")
                    Dim Vals As Array = [Enum].GetValues(GetType(TEnum))
                    For Each Constant As TEnum In Vals
                        Dim Cns As Reflection.FieldInfo = GetConstant(Constant)
                        Dim attrs As Object() = Cns.GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)
                        Dim CnsName As String
                        If attrs IsNot Nothing AndAlso attrs.Length > 0 Then
                            CnsName = DirectCast(attrs(0), Xml.Serialization.XmlEnumAttribute).Name
                        Else
                            CnsName = Cns.Name
                        End If
                        If value = CnsName Then
                            EnumValue = Cns.GetValue(Nothing)
                            Exit Property
                        End If
                    Next Constant
                    For Each ch As Char In value
                        If AscW(ch) < AscW(" ") OrElse AscW(ch) > 127 Then Throw New ArgumentException("StringValue can contain only ASCII-encodable graphic characters and spaces")
                    Next ch
                    _EnumValue = CObj(0)
                    _StringValue = value
                    ContainsEnum = False
                End Set
            End Property

            ''' <summary>Identifies whether this instance contains value of specified type</summary>
            ''' <param name="T">Type to be contained</param>
            ''' <returns>True if this instance contais value of type <paramref name="T"/> otherwise False</returns>
            Public Overrides ReadOnly Property contains(ByVal T As System.Type) As Boolean Implements DataStructuresT.GenericT.IT1orT2(Of TEnum, String).contains
                Get
                    Return T.Equals(GetType(TEnum)) AndAlso ContainsEnum OrElse T.Equals(GetType(String))
                End Get
            End Property

            ''' <summary>Determines if currrent instance contains enumerated value</summary>
            ''' <value>This property cannot be set</value>
            ''' <returns>True if this instance contains enumerated value</returns>
            ''' <exception cref="NotSupportedException">An attempt to change value</exception>
            Public Overrides Property ContainsEnum() As Boolean Implements DataStructuresT.GenericT.IT1orT2(Of TEnum, String).contains1
                Get
                    Return _ContainsEnum
                End Get
                <EditorBrowsable(EditorBrowsableState.Never)> _
                Set(ByVal value As Boolean)
                    If value <> ContainsEnum Then Throw New NotSupportedException("StringEnum.ContainsEnum cannot be changed")
                End Set
            End Property

            ''' <summary>Determines if currrent instance contains string value</summary>
            ''' <value>This property cannot be set</value>
            ''' <returns>Always True</returns>
            ''' <exception cref="NotSupportedException">An attempt to set value to false</exception>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Public Overrides Property ContainsString() As Boolean Implements DataStructuresT.GenericT.IT1orT2(Of TEnum, String).contains2
                Get
                    Return True
                End Get
                Set(ByVal value As Boolean)
                    If value <> True Then Throw New NotSupportedException("StringEnum.ContainsString cannot be set to false")
                End Set
            End Property

            ''' <summary>Determines whether instance contains neither string nor enumerated value</summary>
            ''' <returns>True when both values are not present. False if one of values is present (even if it contains null)</returns>
            Public Overrides ReadOnly Property IsEmpty() As Boolean Implements DataStructuresT.GenericT.IT1orT2(Of TEnum, String).IsEmpty
                Get
                    Return Not ContainsEnum AndAlso StringValue Is Nothing
                End Get
            End Property

            ''' <summary>Get or sets stored value in type-unsafe way</summary>
            ''' <value>New value to be stored in this instance</value>
            ''' <returns>Value stored in this instance</returns>
            ''' <exception cref="NullReferenceException">When trying to set null value</exception>
            ''' <exception cref="ArgumentException">When trying to set value of type other than <see cref="String"/> and <see cref="IConvertible"/></exception>
            Public Overrides Property objValue() As Object Implements DataStructuresT.GenericT.IT1orT2(Of TEnum, String).objValue
                Get
                    If ContainsEnum Then Return EnumValue Else Return StringValue
                End Get
                Set(ByVal value As Object)
                    If value Is Nothing Then Throw New ArgumentNullException("value")
                    If TypeOf value Is String Then
                        StringValue = value
                    ElseIf TypeOf value Is IConvertible Then
                        EnumValue = CObj(DirectCast(value, IConvertible).ToDecimal(InvariantCulture))
                    Else
                        Throw New ArgumentException("Value of incompatible type is being set")
                    End If
                End Set
            End Property
            ''' <summary>Converts <see cref="String"/> into <see cref="StringEnum(Of TEnum)"/></summary>
            ''' <param name="From">A <see cref="String"/> to be converted</param>
            ''' <returns>New instance of <see cref="StringEnum(Of TEnum)"/> initialized with <paramref name="From"/> as <see cref="StringValue"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="From"/> is null</exception>
            Public Shared Widening Operator CType(ByVal From As String) As StringEnum(Of TEnum)
                Dim ret As New StringEnum(Of TEnum)
                ret.StringValue = From
                Return ret
            End Operator
            ''' <summary>Converts <see cref="TEnum"/> into <see cref="StringEnum(Of TEnum)"/></summary>
            ''' <param name="From">A <see cref="String"/> to be converted</param>
            ''' <returns>New instance of <see cref="StringEnum(Of TEnum)"/> initialized with <paramref name="From"/> as <see cref="StringValue"/></returns>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="From"/> is not member of <see cref="TEnum"/></exception>
            Public Shared Narrowing Operator CType(ByVal From As TEnum) As StringEnum(Of TEnum)
                Dim ret As New StringEnum(Of TEnum)
                ret.EnumValue = From
                Return ret
            End Operator
            ''' <summary>Converts <see cref="StringEnum(Of TEnum)"/> to <see cref="String"/></summary>
            ''' <param name="From">A <see cref="StringEnum(Of TEnum)"/> to be converted</param>
            ''' <returns><see cref="StringEnum(Of TEnum).StringValue"/> of <paramref name="From"/></returns>
            Public Shared Widening Operator CType(ByVal From As StringEnum(Of TEnum)) As String
                Return From.StringValue
            End Operator
            ''' <summary>Converts <see cref="StringEnum(Of TEnum)"/> to <see cref="TEnum"/></summary>
            ''' <param name="From">A <see cref="StringEnum(Of TEnum)"/> to be converted</param>
            ''' <returns><see cref="StringEnum(Of TEnum).EnumValue"/> of <paramref name="From"/> (it can be 0 if <paramref name="From"/> does not contain enum value)</returns>
            Public Shared Widening Operator CType(ByVal From As StringEnum(Of TEnum)) As TEnum
                Return From.EnumValue
            End Operator
            ''' <summary>String representation</summary>
            ''' <returns><see cref="StringValue"/></returns>
            Public Overrides Function ToString() As String
                Return StringValue
            End Function
            ''' <summary>Gets or sets <see cref="EnumValue"/> as decimal</summary>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="TEnum"/></exception>
            Public Overrides Property DecimalValue() As Decimal
                Get
                    Return EnumValue.ToDecimal(InvariantCulture)
                End Get
                Set(ByVal value As Decimal)
                    EnumValue = CObj(value)
                End Set
            End Property
            ''' <summary>Returns type of <see cref="TEnum"/></summary>
            Public Overrides ReadOnly Property EnumType() As System.Type
                Get
                    Return GetType(TEnum)
                End Get
            End Property
        End Class

#End Region
        ''' <summary>Returns <see cref="Type"/> that is used to store values of particular <see cref="IPTCTypes">IPTC type</see></summary>
        ''' <param name="IPTCType">IPTC type to get <see cref="Type"/> for</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="IPTCType"/> is not member of <see cref="IPTCTypes"/></exception>
        Public Shared Function GetUnderlyingType(ByVal IPTCType As IPTCTypes) As Type
            Select Case IPTCType
                Case IPTCTypes.Alpha : Return GetType(String)
                Case IPTCTypes.AudioType : Return GetType(iptcAudioType)
                Case IPTCTypes.Boolean_Binary : Return GetType(Boolean)
                Case IPTCTypes.BW460 : Return GetType(Drawing.Bitmap)
                Case IPTCTypes.Byte_Binary : Return GetType(Byte)
                Case IPTCTypes.ByteArray : Return GetType(Byte())
                Case IPTCTypes.CCYYMMDD : Return GetType(Date)
                Case IPTCTypes.CCYYMMDDOmmitable : Return GetType(OmmitableDate)
                Case IPTCTypes.Enum_Binary : Return GetType([Enum])
                Case IPTCTypes.Enum_NumChar : Return GetType([Enum])
                Case IPTCTypes.GraphicCharacters : Return GetType(String)
                Case IPTCTypes.HHMMSS : Return GetType(TimeSpan)
                Case IPTCTypes.HHMMSS_HHMM : Return GetType(Time)
                Case IPTCTypes.ImageType : Return GetType(iptcImageType)
                Case IPTCTypes.Num2_Str : Return GetType(NumStr2)
                Case IPTCTypes.Num3_Str : Return GetType(NumStr3)
                Case IPTCTypes.NumericChar : Return GetType(IConvertible)
                Case IPTCTypes.StringEnum : Return GetType(DataStructuresT.GenericT.T1orT2(Of [Enum], String))
                Case IPTCTypes.SubjectReference : Return GetType(iptcSubjectReference)
                Case IPTCTypes.Text : Return GetType(String)
                Case IPTCTypes.TextWithSpaces : Return GetType(String)
                Case IPTCTypes.UNO : Return GetType(iptcUNO)
                Case IPTCTypes.UnsignedBinaryNumber : Return GetType(ULong)
                Case IPTCTypes.UShort_Binary : Return GetType(UShort)
                Case Else : Throw New InvalidEnumArgumentException("IPTCType", IPTCType, GetType(IPTCTypes))
            End Select
        End Function
        ''' <summary>Gets details about tag format</summary>
        ''' <param name="Tag">tag to get details for</param>
        ''' <exception cref="InvalidEnumArgumentException">Either <see cref="DataSetIdentification.RecordNumber"/> of <paramref name="Tag"/> is unknown or <see cref="DataSetIdentification.DatasetNumber"/> of <paramref name="Tag"/> is unknown within <see cref="DataSetIdentification.RecordNumber"/> of <paramref name="Tag"/></exception>
        Public Shared Function GetTag(ByVal Tag As DataSetIdentification) As IPTCTag
            Return GetTag(Tag.RecordNumber, Tag.DatasetNumber)
        End Function
        ''' <summary>Information about group of tags</summary>
        Public Class GroupInfo
            ''' <summary>Contains value of the <see cref="Tags"/> Proeprty</summary>
            Private _Tags As IPTCTag()
            ''' <summary>Contains value of the <see cref="Mandatory"/> Proeprty</summary>
            Private _Mandatory As Boolean
            ''' <summary>Contains value of the <see cref="Repeatable"/> Proeprty</summary>
            Private _Repeatable As Boolean
            ''' <summary>Contains value of the <see cref="Name"/> Proeprty</summary>
            Private _Name As String
            ''' <summary>Contains value of the <see cref="HumanName"/> Proeprty</summary>
            Private _HumanName As String
            ''' <summary>Contains value of the <see cref="Group"/> Proeprty</summary>
            Private _Group As Groups
            ''' <summary>Contains value of the <see cref="Category"/> Proeprty</summary>
            Private _Category As String
            ''' <summary>Contains value of the <see cref="Description"/> Proeprty</summary>
            Private _Description As String
            ''' <summary>Contains value of the <see cref="Type"/> Proeprty</summary>
            Private _Type As Type
            ''' <summary>CTor</summary>
            ''' <param name="Name">Name of group used in object structure</param>
            ''' <param name="HumanName">Human-friendly name of group</param>
            ''' <param name="Group">Group number</param>
            ''' <param name="Type">Type that represents this group</param>
            ''' <param name="Category">Category of this group</param>
            ''' <param name="Description">Description</param>
            ''' <param name="Mandatory">Group is mandatory according to IPTC standard</param>
            ''' <param name="Repeatable">Group is repeatable</param>
            ''' <param name="Tags">Tags the group consists of</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Group"/> is not member of <see cref="Groups"/></exception>
            ''' <exception cref="ArgumentException"><paramref name="Tags"/> is null or have less than 2 items</exception>
            Public Sub New(ByVal Name As String, ByVal HumanName As String, ByVal Group As Groups, ByVal Type As Type, ByVal Category As String, ByVal Description As String, ByVal Mandatory As Boolean, ByVal Repeatable As Boolean, ByVal ParamArray Tags As IPTCTag())
                If Tags Is Nothing OrElse Tags.Length < 2 Then Throw New ArgumentException("Each group must have at least 2 tags")
                If Not InEnum(Group) Then Throw New InvalidEnumArgumentException("Group", Group, GetType(Groups))
                If Type Is Nothing Then Throw New ArgumentNullException("Type", "Type cannot be null")
                _Tags = Tags
                _Mandatory = Mandatory
                _Repeatable = Repeatable
                _Name = Name
                _HumanName = HumanName
                _Group = Group
                _Category = _Category
                _Description = Description
                _Type = Type
            End Sub
            ''' <summary>Type that realizes object representation of this group</summary>
            Public ReadOnly Property Type() As Type
                Get
                    Return _Type
                End Get
            End Property
            ''' <summary>Tags present in this group</summary>
            Public ReadOnly Property Tags() As IPTCTag()
                Get
                    Dim Arr(_Tags.Length - 1) As IPTCTag
                    _Tags.CopyTo(Arr, 0)
                    Return Arr
                End Get
            End Property
            ''' <summary>True if this group is mandatory according to IPTC�standard</summary>
            Public ReadOnly Property Mandatory() As Boolean
                Get
                    Return _Mandatory
                End Get
            End Property
            ''' <summary>True if this group is repeatable</summary>
            Public ReadOnly Property Repeatable() As Boolean
                Get
                    Return _Repeatable
                End Get
            End Property
            ''' <summary>Name of group used in object structure</summary>
            Public ReadOnly Property Name() As String
                Get
                    Return _Name
                End Get
            End Property
            ''' <summary>Human-friendly name of this group</summary>
            Public ReadOnly Property HumanName() As String
                Get
                    Return _HumanName
                End Get
            End Property
            ''' <summary>Code of this group</summary>
            Public ReadOnly Property Group() As Groups
                Get
                    Return _Group
                End Get
            End Property
            ''' <summary>Name of category of this group</summary>
            Public ReadOnly Property Category() As String
                Get
                    Return _Category
                End Get
            End Property
            ''' <summary>Description of this group</summary>
            Public ReadOnly Property Description() As String
                Get
                    Return _Description
                End Get
            End Property
        End Class
        ''' <summary>Common base for all tag groups</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public MustInherit Class Group
            ''' <summary>Gets assignmenst between group indexes of tags and indexes of groups</summary>
            ''' <param name="Tags">Tags contained in this group</param>
            ''' <param name="IPTC"><see cref="IPTC"/> to create map for</param>
            ''' <returns><see cref="List(Of Integer())"/> where each item of list means one group instance and contains indexes of tags when obtained via tag properties</returns>
            ''' <exception cref="ArgumentNullException"><paramref name="Tags"/> is null or contains less then 2 items -or- <paramref name="IPTC"/> is null</exception>
            Protected Shared Function GetGroupMap(ByVal IPTC As IPTC, ByVal ParamArray Tags As IPTCTag()) As List(Of Integer())
                If Tags Is Nothing OrElse Tags.Length < 2 Then Throw New ArgumentException("tags", "Tags must contain at least 2 items")
                If IPTC Is Nothing Then Throw New ArgumentNullException("IPTC")
                Dim Counters(Tags.Length - 1) As Integer 'Count of searched tags
                Dim Stage As Integer = -1 'Index of tag expected to be found
                Dim ListIndex As Integer = -1 'Current index in ret
                Dim ret As New List(Of Integer())
                For Each item As KeyValuePair(Of DataSetIdentification, Byte()) In IPTC.Tags
                    'Is this tag that I'm searching for?
                    Dim Searching As Integer = -1
                    For i As Integer = 0 To Tags.Length - 1
                        If Tags(i).Identification = item.Key Then Searching = i : Exit For
                    Next i
                    If Searching >= 0 Then
                        If Stage < Searching Then 'New group
                            Dim arr(Tags.Length - 1) As Integer
                            For j As Integer = 0 To Tags.Length - 1 : arr(j) = -1 : Next j
                            ret.Add(arr)
                            ListIndex += 1
                        End If
                        ret(ListIndex)(Searching) = Counters(Searching)
                        Counters(Searching) += 1
                        Stage = Searching + 1
                    End If
                Next item
                If ret.Count = 0 Then Return Nothing Else Return ret
            End Function
        End Class
        '#Region "Verificators"
        '        ''' <summary>Verifies if given value belongs to specific enumeration.</summary>
        '        ''' <param name="verify">Value to be verified</param>
        '        ''' <typeparam name="T">Type of enum to verify <paramref name="verify"/> in</typeparam>
        '        ''' <exception cref="InvalidEnumArgumentException"><paramref name="verify"/> is not member of <paramref name="T"/> and <paramref name="T"/> has no <see cref="RestrictAttribute"/> or it has <see cref="RestrictAttribute"/> se to false.</exception>
        '        ''' <exception cref="ArgumentException"><paramref name="T"/> is not <see cref="[Enum]"/> and <paramref name="T"/> has <see cref="RestrictAttribute"/> set to True or it has no <see cref="RestrictAttribute"/></exception>
        '        <CLSCompliant(False)> _
        '        Public Sub VerifyNumericEnum(Of T As {IConvertible, Structure})(ByVal verify As T)
        '            Dim Attrs As Object() = GetType(T).GetCustomAttributes(GetType(RestrictAttribute), False)
        '            If Attrs Is Nothing OrElse Attrs.Length = 0 OrElse DirectCast(Attrs(0), RestrictAttribute).Restrict = True Then _
        '                If Not InEnum(verify) Then Throw New InvalidEnumArgumentException("verify", verify.ToInt32(InvariantCulture), GetType(T))
        '        End Sub
        '        ''' <summary>Verifies if given value if valid fro unrestricted string enum</summary>
        '        ''' <param name="verify">Value to be verified</param>
        '        ''' <param name="Len">Maximal lenght of string</param>
        '        ''' <param name="Fixed">Is <paramref name="Len"/> fixed lenght</param>
        '        ''' <typeparam name="T">Type of enumeration</typeparam>
        '        ''' <exception cref="ArgumentException"><paramref name="T"/> is not enumeration -or- string value violates lenght constraint -or- string value contains invalid (non-aplha) character</exception>
        '        ''' <exception cref="InvalidEnumArgumentException">Enum value is not member of <paramref name="T"/></exception>
        '        <CLSCompliant(False)> _
        '        Public Sub VerifyStringEnumNR(Of T As {IConvertible, Structure})(ByVal verify As T1orT2(Of T, String), ByVal Len As Byte, ByVal Fixed As Boolean)
        '            If verify.contains1 Then
        '                VerifyNumericEnum(DirectCast(verify.value1, T))
        '            Else
        '                VerifyAlpha(verify.value2, Len, Fixed)
        '            End If
        '        End Sub
        '        ''' <summary>Verifye if given string contains only alpha characters</summary>
        '        ''' <param name="verify"><see cref="String"/> to be verified</param>
        '        ''' <param name="Len">Maximal allowed length of string</param>
        '        ''' <param name="Fixed">Is <paramref name="Len"/> fixed length</param>
        '        ''' <exception cref="ArgumentException"><paramref name="verify"/> contains non-alpha character or violates lenght constraint</exception>
        '        Public Sub VerifyAlpha(ByVal verify As String, ByVal Len As Byte, ByVal Fixed As Boolean)
        '            If (Fixed AndAlso verify.Length <> Len OrElse Len <> 0 AndAlso verify.Length > Len) OrElse Not IsAlpha(verify) Then Throw New ArgumentException("Non alpha character")
        '        End Sub
        '#End Region
#Region "Serializers and deserializers"
#Region "Deserializers"
        ''' <summary>Ready signed ingere from byte array</summary>
        ''' <param name="Count">Number of bytes to read (can be 1,2,4,8)</param>
        ''' <param name="Bytes">Byte array to read from</param>
        ''' <returns>Signed integer stored in given byte array</returns>
        ''' <exception cref="ArgumentException"><paramref name="Count"/> is not 1,2,4 or 8</exception>
        ''' <exception cref="System.IO.EndOfStreamException">There are not enough bytes in <paramref name="Bytes"/></exception>
        Protected Shared Function IntFromBytes(ByVal Count As Byte, ByVal Bytes As Byte()) As Long
            Dim Str As New IOt.BinaryReader(New System.IO.MemoryStream(Bytes), IOt.BinaryReader.ByteAling.BigEndian)
            Select Case Count
                Case 1 'SByte
                    Return Str.ReadSByte
                Case 2 'Short
                    Return Str.ReadInt16
                Case 4 'Integer
                    Return Str.ReadInt32
                Case 8 'Long
                    Return Str.ReadInt64
                Case Else
                    Throw New ArgumentException("Only 1,2,4 and 8-bytes integers can be read via IntFromBytes")
            End Select
        End Function
        ''' <summary>Ready unsigned ingere from byte array</summary>
        ''' <param name="Count">Number of bytes to read (can be 1,2,4,8)</param>
        ''' <param name="Bytes">Byte array to read from</param>
        ''' <returns>Unsigned integer stored in given byte array</returns>
        ''' <exception cref="ArgumentException"><paramref name="Count"/> is not 1,2,4 or 8</exception>
        ''' <exception cref="System.IO.EndOfStreamException">There are not enough bytes in <paramref name="Bytes"/></exception>
        <CLSCompliant(False)> _
        Protected Shared Function UIntFromBytes(ByVal Count As Byte, ByVal Bytes As Byte()) As ULong
            Dim Str As New IOt.BinaryReader(New System.IO.MemoryStream(Bytes), IOt.BinaryReader.ByteAling.BigEndian)
            Select Case Count
                Case 1 'Byte
                    Return Str.ReadByte
                Case 2 'UShort
                    Return Str.ReadUInt16
                Case 4 'UInteger
                    Return Str.ReadUInt32
                Case 8 'ULong
                    Return Str.ReadUInt64
                Case Else
                    Throw New ArgumentException("Only 1,2,4 and 8-bytes integers can be read via UIntFromBytes")
            End Select
        End Function
        ''' <summary>Converts array of bytes that contains string to number</summary>
        ''' <param name="Bytes">Bytest to be converted</param>
        ''' <returns>Number that can be converted at least to <see cref="Long"/> or <see cref="ULong"/></returns>
        ''' <exception cref="InvalidCastException">Cannot convert string stored in <paramref name="Bytes"/> to <see cref="Decimal"/></exception>
        Protected Shared Function NumCharFromBytes(ByVal Bytes As Byte()) As Decimal
            Dim Str As String = System.Text.Encoding.ASCII.GetString(Bytes)
            Return Str
        End Function
#End Region
#Region "Serializers"
        ''' <summary>Converts number to array of bytes in which the number is stored as ASCII string</summary>
        ''' <param name="Count">Number of bytes to get (0 means no limit)</param>
        ''' <param name="Number">Number to be stored</param>
        ''' <returns>Array of bytes that contains <paramref name="Count"/> bytes consisting of string representation of <paramref name="Number"/></returns>
        ''' <param name="Fixed"><paramref name="Count"/> represents fixed lenght of returned byte array (True) or maximal variable lenght (False)</param>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Count"/> is 0 and <paramref name="Fixed"/> is True -or-
        ''' <paramref name="Count"/> is not 0 and number cannot be stored in number of bytes specified in <paramref name="Count"/>
        ''' </exception>
        Protected Shared Function ToBytes(ByVal Count As Byte, ByVal Number As Decimal, Optional ByVal Fixed As Boolean = True) As Byte()
            If Count = 0 And Fixed = True Then Throw New ArgumentException("Len cannot be 0 when Fixed is true")
            ToBytes = Nothing
            Try
                If Fixed Then
                    Return System.Text.Encoding.ASCII.GetBytes(Number.ToString(New String("0"c, Count), InvariantCulture))
                Else
                    Return System.Text.Encoding.ASCII.GetBytes(Number.ToString("0", InvariantCulture))
                End If
            Finally
                If ToBytes Is Nothing Then ToBytes = New Byte() {}
                If Count <> 0 AndAlso ToBytes.Length > Count Then Throw New ArgumentException(String.Format("Number {0} cannot be stored in {1} bytes", Number, Count))
            End Try
        End Function
        ''' <summary>Converts signed integer to array of bytes</summary>
        ''' <param name="Count">Length of integral value and returned array (can be 1,2,4,8)</param>
        ''' <param name="Int">Value to be converted</param>
        ''' <returns>Array of bytes representing <paramref name="Int"/></returns>
        ''' <exception cref="ArgumentException"><paramref name="Count"/> is not 1,2,4 or 8</exception>
        ''' <exception cref="OverflowAction"><paramref name="Int"/>'s value does not fit into integral value of <paramref name="Count"/> bytes</exception>
        Protected Shared Function ToBytes(ByVal Count As Byte, ByVal Int As Long) As Byte()
            Dim Str As New System.IO.BinaryWriter(New System.IO.MemoryStream(Count))
            Select Case Count
                Case 1 'SByte
                    Str.Write(MathT.LEBE(CSByte(Int)))
                Case 2 'Short
                    Str.Write(MathT.LEBE(CShort(Int)))
                Case 4 'Integer
                    Str.Write(MathT.LEBE(CInt(Int)))
                Case 5 'Long
                    Str.Write(MathT.LEBE(CLng(Int)))
                Case Else
                    Throw New ArgumentException("Only 1,2,4 and 8-bytes integers can be read via IntToBytes")
            End Select
            Dim Arr(Count - 1) As Byte
            Dim Buff As Byte() = DirectCast(Str.BaseStream, System.IO.MemoryStream).GetBuffer
            Array.ConstrainedCopy(Buff, 0, Arr, 0, Count)
            Return Arr
        End Function
        ''' <summary>Converts unsigned integer to array of bytes</summary>
        ''' <param name="Count">Length of integral value and returned array (can be 1,2,4,8)</param>
        ''' <param name="Int">Value to be converted</param>
        ''' <returns>Array of bytes representing <paramref name="Int"/></returns>
        ''' <exception cref="ArgumentException"><paramref name="Count"/> is not 1,2,4 or 8</exception>
        ''' <exception cref="OverflowAction"><paramref name="Int"/>'s value does not fit into integral value of <paramref name="Count"/> bytes</exception>
        <CLSCompliant(False)> _
        Protected Shared Function ToBytes(ByVal Count As Byte, ByVal Int As ULong) As Byte()
            Dim Str As New System.IO.BinaryWriter(New System.IO.MemoryStream(Count))
            Select Case Count
                Case 1 'SByte
                    Str.Write(MathT.LEBE(CByte(Int)))
                Case 2 'Short
                    Str.Write(MathT.LEBE(CUShort(Int)))
                Case 4 'Integer
                    Str.Write(MathT.LEBE(CUInt(Int)))
                Case 8 'Long
                    Str.Write(MathT.LEBE(CULng(Int)))
                Case Else
                    Throw New ArgumentException("Only 1,2,4 and 8-bytes integers can be read via ToBytes")
            End Select
            Dim Arr(Count - 1) As Byte
            Dim Buff As Byte() = DirectCast(Str.BaseStream, System.IO.MemoryStream).GetBuffer
            Array.ConstrainedCopy(Buff, 0, Arr, 0, Count)
            Return Arr
        End Function
#End Region
#End Region
    End Class
#End If
End Namespace