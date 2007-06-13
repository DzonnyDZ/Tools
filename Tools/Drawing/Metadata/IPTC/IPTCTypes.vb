Imports Tools.CollectionsT.GenericT, System.Globalization.CultureInfo, Tools.DataStructuresT.GenericT
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
            ''' <summary>Time stored as numeric characters (and the ± sign) in format HHMMSS±HHMM (with time-zone offset from UTC) (represented by <see cref="Time"/></summary>
            HHMMSS_HHMM
            ''' <summary>Generic array of bytes (represented by array of <see cref="Byte"/>)</summary>
            ByteArray
            ''' <summary>Unique Object Identifier (represented by <see cref="UNO"/>)</summary>
            UNO
            ''' <summary>Combination of 2-digits number and optional <see cref="String"/> (represented by <see cref="NumStr2"/>)</summary>
            Num2_Str 'TODO:Enum?
            ''' <summary>Combination of 3-digits number and optional <see cref="String"/> (represented by <see cref="NumStr3"/>)</summary>
            Num3_Str 'TODO:Enum?
            ''' <summary>Subject reference (combination of IPR, subject number and description) (represented by <see cref="SubjectReference"/>)</summary>
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
        ''' <summary>IPTC Subject Reference</summary>
        Public Class SubjectReference : Inherits WithIPR
            Private Const SubjRefNMask As Integer = 1000000
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
            Public Property SubjectNumber() As SubjectReferenceNumbers
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
            ''' <exception cref="InvalidEnumArgumentException">Valùue being set is not member of <see cref="SubjectMatterNumbers"/> and it is not zero</exception>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            Public Property SubjectMatterNumber() As SubjectMatterNumbers
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
            Public Property SubjectDetailNumber() As EconomySubjectDetail
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
            ''' <summary>A text representation of the Subject Number (maximum 64 octets) consisting of graphic characters plus spaces either in English, as defined in Appendix H, or in the language of the service as indicated in DataSet <see cref="LanguageIdentifier"/> (2:135)</summary>
            ''' <remarks>The Subject identifies the general content of the objectdata as determined by the provider.</remarks>
            ''' <exception cref="ArgumentException">Value being set is longer than 64 characters -or- value being set contains non-graphic character or * or ? or :</exception>
            Public Property SubjectName() As String
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
            ''' <summary>a text representation of the Subject Matter Number</summary>
            ''' <remarks>Maximum 64 octets consisting of graphic characters plus spaces either in English, as defined in Appendix I, or in the language of the service as indicated in DataSet <see cref="LanguageIdentifier"/> (2:135). A Subject Matter further refines the Subject of a News Object.</remarks>
            ''' <exception cref="ArgumentException">Value being set is longer than 64 characters -or- value being set contains non-graphic character or * or ? or :</exception>
            Public Property SubjectMatterName() As String
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
            ''' <summary>A text representation of the SubjectDetail Number</summary>
            ''' <remarks>
            ''' Maximum 64 octets consisting of graphic characters plus spaces either in English, as defined in Appendix J, or in the language of the service as indicated in DataSet <see cref="LanguageIdentifier"/> (2:135)
            ''' <para>A Subject Detail further refines the Subject Matter of a News Object. A registry of Subject Reference Numbers, Subject Matter Names and Subject Detail Names, descriptions (if available) and their corresponding parent Subjects will be held by the IPTC in different languages, with translations as supplied by members. See Appendices I and J.</para></remarks>
            ''' <exception cref="ArgumentException">Value being set is longer than 64 characters -or- value being set contains non-graphic character or * or ? or :</exception>
            Public Property SubjectDetailName() As String
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
                Return String.Format(InvariantCulture, "{0:" & New String("0"c, NumberDigits) & "};{1}", Number, [String])
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
                Return String.Format(InvariantCulture, "{0:0000}{1:00}{2:00}", Year, Month, Day)
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
                Return String.Format(InvariantCulture, "{0:00}{1:00}{2:00}{3:+00;-00}{4:00}", Hour, Minute, Second, OffsetHour, OffsetMinute)
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
                Return String.Format(InvariantCulture, "{0}{1}", CByte(Components), TypeCode)
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
                Return String.Format(InvariantCulture, "{0}{1}", Components, TypeCode)
            End Function
        End Structure
#End Region
        ''' <summary>Returns <see cref="Type"/> that is used to store values of particular <see cref="IPTCTypes">IPTC type</see></summary>
        ''' <param name="IPTCType">IPTC type to get <see cref="Type"/> for</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="IPTCType"/> is not member of <see cref="IPTCTypes"/></exception>
        Public Shared Function GetUnderlyingType(ByVal IPTCType As IPTCTypes) As Type
            Select Case IPTCType
                Case IPTCTypes.Alpha : Return GetType(String)
                Case IPTCTypes.AudioType : Return GetType(AudioType)
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
                Case IPTCTypes.ImageType : Return GetType(ImageType)
                Case IPTCTypes.Num2_Str : Return GetType(NumStr2)
                Case IPTCTypes.Num3_Str : Return GetType(NumStr3)
                Case IPTCTypes.NumericChar : Return GetType(IConvertible)
                Case IPTCTypes.StringEnum : Return GetType(DataStructuresT.GenericT.T1orT2(Of [Enum], String))
                Case IPTCTypes.SubjectReference : Return GetType(SubjectReference)
                Case IPTCTypes.Text : Return GetType(String)
                Case IPTCTypes.TextWithSpaces : Return GetType(String)
                Case IPTCTypes.UNO : Return GetType(UNO)
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
            ''' <summary>True if this group is mandatory according to IPTC standard</summary>
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

        End Class
#Region "Verificators"
        ''' <summary>Verifies if given value belongs to specific enumeration.</summary>
        ''' <param name="verify">Value to be verified</param>
        ''' <typeparam name="T">Type of enum to verify <paramref name="verify"/> in</typeparam>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="verify"/> is not member of <paramref name="T"/> and <paramref name="T"/> has no <see cref="RestrictAttribute"/> or it has <see cref="RestrictAttribute"/> se to false.</exception>
        ''' <exception cref="ArgumentException"><paramref name="T"/> is not <see cref="[Enum]"/> and <paramref name="T"/> has <see cref="RestrictAttribute"/> set to True or it has no <see cref="RestrictAttribute"/></exception>
        <CLSCompliant(False)> _
        Public Sub VerifyNumericEnum(Of T As {IConvertible, Structure})(ByVal verify As T)
            Dim Attrs As Object() = GetType(T).GetCustomAttributes(GetType(RestrictAttribute), False)
            If Attrs Is Nothing OrElse Attrs.Length = 0 OrElse DirectCast(Attrs(0), RestrictAttribute).Restrict = True Then _
                If Not InEnum(verify) Then Throw New InvalidEnumArgumentException("verify", verify.ToInt32(InvariantCulture), GetType(T))
        End Sub
        ''' <summary>Verifies if given value if valid fro unrestricted string enum</summary>
        ''' <param name="verify">Value to be verified</param>
        ''' <param name="Len">Maximal lenght of string</param>
        ''' <param name="Fixed">Is <paramref name="Len"/> fixed lenght</param>
        ''' <typeparam name="T">Type of enumeration</typeparam>
        ''' <exception cref="ArgumentException"><paramref name="T"/> is not enumeration -or- string value violates lenght constraint -or- string value contains invalid (non-aplha) character</exception>
        ''' <exception cref="InvalidEnumArgumentException">Enum value is not member of <paramref name="T"/></exception>
        <CLSCompliant(False)> _
        Public Sub VerifyStringEnumNR(Of T As {IConvertible, Structure})(ByVal verify As T1orT2(Of T, String), ByVal Len As Byte, ByVal Fixed As Boolean)
            If verify.contains1 Then
                VerifyNumericEnum(DirectCast(verify.value1, T))
            Else
                VerifyAlpha(verify.value2, Len, Fixed)
            End If
        End Sub
        ''' <summary>Verifye if given string contains only alpha characters</summary>
        ''' <param name="verify"><see cref="String"/> to be verified</param>
        ''' <param name="Len">Maximal allowed length of string</param>
        ''' <param name="Fixed">Is <paramref name="Len"/> fixed length</param>
        ''' <exception cref="ArgumentException"><paramref name="verify"/> contains non-alpha character or violates lenght constraint</exception>
        Public Sub VerifyAlpha(ByVal verify As String, ByVal Len As Byte, ByVal Fixed As Boolean)
            If (Fixed AndAlso verify.Length <> Len OrElse Len <> 0 AndAlso verify.Length > Len) OrElse Not IsAlpha(verify) Then Throw New ArgumentException("Non alpha character")
        End Sub
#End Region
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
            Try
                If Fixed Then
                    Return System.Text.Encoding.ASCII.GetBytes(Number.ToString(New String("0"c, Count), InvariantCulture))
                Else
                    Return System.Text.Encoding.ASCII.GetBytes(Number.ToString("0", InvariantCulture))
                End If
            Finally
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