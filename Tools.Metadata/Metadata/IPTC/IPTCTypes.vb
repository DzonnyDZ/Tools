Imports Tools.CollectionsT.GenericT, System.Globalization.CultureInfo, Tools.DataStructuresT.GenericT
Imports Tools.VisualBasicT.Interaction, Tools.ComponentModelT, Tools.DrawingT.DesignT
Imports System.Drawing.Design, System.Windows.Forms, System.Drawing
Imports Tools.MetadataT.IptcT
Imports Tools.MetadataT.IptcT.Iptc
Imports Tools.ExtensionsT

Namespace MetadataT.IptcT.IptcDataTypes

    ''' <summary>IPTC Subject Reference (IPTC type <see cref="IPTCTypes.SubjectReference"/>)</summary>
    Public Class IptcSubjectReference : Inherits WithIpr
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
                If Array.IndexOf([Enum].GetValues(GetType(SubjectReferenceNumbers)), CType(value, SubjectReferenceNumbers)) < 0 AndAlso _
                        Array.IndexOf([Enum].GetValues(GetType(SubjectMatterNumbers)), CType(value, SubjectMatterNumbers)) < 0 AndAlso _
                        Array.IndexOf([Enum].GetValues(GetType(EconomySubjectDetail)), CType(value, EconomySubjectDetail)) < 0 AndAlso _
                        value <> 0 Then
                    Throw New InvalidEnumArgumentException(ResourcesT.Exceptions.SubjectReferenceNumberMustBeMemberOfEitherSubjectReferenceNumbersSubjectMatterNumbersOrEconomySubjectDetail)
                End If
                _SubjectReferenceNumber = value
            End Set
        End Property
        ''' <summary>Subject component of <see cref="SubjectReferenceNumber"/></summary>
        ''' <remarks>The Subject identifies the general content of the objectdata as determined by the provider.</remarks>
        ''' <value>New value for subject number. Setting this property resets <see cref="SubjectMatterNumber"/> and <see cref="SubjectDetailNumber"/> to zero</value>
        ''' <returns>Subject number value or zero if none specified</returns>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="SubjectReferenceNumbers"/> and it is not zero</exception>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <LDisplayName(GetType(IPTCResources), "SubjectNumber_n"), LDescription(GetType(IPTCResources), "SubjectNumber_d")> _
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
        ''' <exception cref="InvalidEnumArgumentException">Valůue being set is not member of <see cref="SubjectMatterNumbers"/> and it is not zero</exception>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <LDisplayName(GetType(IPTCResources), "SubjectMatterNumber_n"), LDescription(GetType(IPTCResources), "SubjectMatterNumber_d")> _
        Public Property SubjectMatterNumber() As SubjectMatterNumbers
            Get
                Return ((SubjectReferenceNumber) \ SubjMatterMask) * SubjMatterMask
            End Get
            Set(ByVal value As SubjectMatterNumbers)
                If value <> 0 AndAlso Array.IndexOf([Enum].GetValues(GetType(SubjectMatterNumbers)), value) < 0 Then Throw New InvalidEnumArgumentException("value", value, GetType(SubjectMatterNumbers))
                SubjectReferenceNumber = value 'SubjectNumber + (value Mod SubjRefNMask)
            End Set
        End Property
        ''' <summary>Detail component of <see cref="SubjectReferenceNumber"/></summary>
        ''' <remarks>A Subject Detail further refines the Subject Matter of a News Object.</remarks>
        ''' <value>New value for subject detail number</value>
        ''' <returns>Subject detail number value or zero if none specified</returns>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="EconomySubjectDetail"/> and it is not zero</exception>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <LDisplayName(GetType(IPTCResources), "SubjectDetailNumber_n"), LDescription(GetType(IPTCResources), "SubjectDetailNumber_d")> _
        Public Property SubjectDetailNumber() As EconomySubjectDetail
            Get
                Return SubjectReferenceNumber 'Mod SubjMatterMask
            End Get
            Set(ByVal value As EconomySubjectDetail)
                If value <> 0 AndAlso Array.IndexOf([Enum].GetValues(GetType(EconomySubjectDetail)), value) < 0 Then Throw New InvalidEnumArgumentException("value", value, GetType(EconomySubjectDetail))
                SubjectReferenceNumber = value '= SubjectNumber + (SubjectMatterNumber Mod SubjRefNMask) + (value Mod SubjMatterMask)
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="SubjectName"/> property</summary>              
        <EditorBrowsable(EditorBrowsableState.Never)> Private _SubjectName As String
        ''' <summary>A text representation of the <see cref="SubjectNumber"/> (maximum 64 octets) consisting of graphic characters plus spaces either in English, as defined in Appendix H, or in the language of the service as indicated in DataSet <see cref="iptc.LanguageIdentifier"/> (2:135)</summary>
        ''' <remarks>The Subject identifies the general content of the objectdata as determined by the provider.</remarks>
        ''' <exception cref="ArgumentException">Value being set is longer than 64 characters -or- value being set contains non-graphic character or * or ? or :</exception>
        <LDisplayName(GetType(IPTCResources), "SubjectName_n"), LDescription(GetType(IPTCResources), "SubjectName_d")> _
        Public Property SubjectName() As String
            Get
                Return _SubjectName
            End Get
            Set(ByVal value As String)
                If value.Length > 64 Then Throw New ArgumentException(ResourcesT.Exceptions.LenghtOfSubjectNameMustFitInto64)
                If Not Iptc.IsGraphicCharacters(value) OrElse value.Contains("*"c) OrElse value.Contains("?"c) OrElse value.Contains(":") Then Throw New ArgumentException(ResourcesT.Exceptions.SubjectNameCanContainOnlyGraphicCharactersExceptAnd)
                _SubjectName = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="SubjectMatterName"/> property</summary>                         
        <EditorBrowsable(EditorBrowsableState.Never)> Private _SubjectMatterName As String
        ''' <summary>A text representation of the <see cref="SubjectMatterNumber"/></summary>
        ''' <remarks>Maximum 64 octets consisting of graphic characters plus spaces either in English, as defined in Appendix I, or in the language of the service as indicated in DataSet <see cref="iptc.LanguageIdentifier"/> (2:135). A Subject Matter further refines the Subject of a News Object.</remarks>
        ''' <exception cref="ArgumentException">Value being set is longer than 64 characters -or- value being set contains non-graphic character or * or ? or :</exception>
        <LDisplayName(GetType(IPTCResources), "SubjectMatterName_n"), LDescription(GetType(IPTCResources), "SubjectMatterName_d")> _
        Public Property SubjectMatterName() As String
            Get
                Return _SubjectMatterName
            End Get
            Set(ByVal value As String)
                If value.Length > 64 Then Throw New ArgumentException(ResourcesT.Exceptions.LenghtOfSubjectReferenceMustFitInto64)
                If Not Iptc.IsGraphicCharacters(value) OrElse value.Contains("*"c) OrElse value.Contains("?"c) OrElse value.Contains(":") Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.CanContainOnlyGraphicCharactersExceptAnd, "SubjectReference"))
                _SubjectMatterName = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="SubjectDetailName"/> property</summary>                         
        <EditorBrowsable(EditorBrowsableState.Never)> Private _SubjectDetailName As String
        ''' <summary>A text representation of the <see cref="SubjectDetailNumber"/></summary>
        ''' <remarks>
        ''' Maximum 64 octets consisting of graphic characters plus spaces either in English, as defined in Appendix J, or in the language of the service as indicated in DataSet <see cref="iptc.LanguageIdentifier"/> (2:135)
        ''' <para>A Subject Detail further refines the Subject Matter of a News Object. A registry of Subject Reference Numbers, Subject Matter Names and Subject Detail Names, descriptions (if available) and their corresponding parent Subjects will be held by the IPTC in different languages, with translations as supplied by members. See Appendices I and J.</para></remarks>
        ''' <exception cref="ArgumentException">Value being set is longer than 64 characters -or- value being set contains non-graphic character or * or ? or :</exception>
        <LDisplayName(GetType(IPTCResources), "SubjectDetailName_n"), LDescription(GetType(IPTCResources), "SubjectDetailName_d")> _
        Public Property SubjectDetailName() As String
            Get
                Return _SubjectDetailName
            End Get
            Set(ByVal value As String)
                If value.Length > 64 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.LenghtOf0MustFitInto1, "SubjectDetailName", 64))
                If Not Iptc.IsGraphicCharacters(value) OrElse value.Contains("*"c) OrElse value.Contains("?"c) OrElse value.Contains(":") Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.CanContainOnlyGraphicCharactersExceptAnd, "SubjectDetailName"))
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
                    Parts(PartI) = New Pair(Of Integer)(LastColon + 1, i - LastColon - 1)
                    PartI += 1
                    LastColon = i
                End If
            Next i
            Parts(PartI) = New Pair(Of Integer)(LastColon + 1, Bytes.Length - 1 - LastColon)
            PartI += 1
            If PartI <> 5 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.MustContainExactly1Parts, "SubjectReference", 5))
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
        ''' <exception cref="InvalidOperationException">Length of any serialized part violates IPTC specification (that is <see cref="IPR"/> must serialize to array of 1÷32 items, <see cref="SubjectReferenceNumber"/> must serialize into array of 8 items and names must serialize into array of 0 to 64 items)</exception>
        Public Function ToBytes(ByVal Encoding As System.Text.Encoding) As Byte()
            Dim Bytes(4)() As Byte
            Bytes(0) = System.Text.Encoding.ASCII.GetBytes(Me.IPR)
            Bytes(1) = System.Text.Encoding.ASCII.GetBytes(Me.SubjectReferenceNumber.ToString(New String("0"c, 8), InvariantCulture))
            If Me.SubjectDetailName IsNot Nothing Then
                Bytes(2) = Encoding.GetBytes(Me.SubjectName)
            Else
                Bytes(2) = New Byte() {}
            End If
            If Me.SubjectMatterName IsNot Nothing Then
                Bytes(3) = Encoding.GetBytes(Me.SubjectMatterName)
            Else
                Bytes(3) = New Byte() {}
            End If
            If Me.SubjectDetailName IsNot Nothing Then
                Bytes(4) = Encoding.GetBytes(Me.SubjectDetailName)
            Else
                Bytes(4) = New Byte() {}
            End If
            If Bytes(0).Length > 32 Or Bytes(0).Length < 1 Then Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.LengthOfSerialized0IsNotWithinRange12Bytes, MetadataT.IptcT.IPTCResources.IPR_n, 1, 32))
            If Bytes(1).Length <> 8 Then Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.LenghtOfSerialized0DiffersFrom1Bytes, "SubjectreferenceNumber", 8))
            If Bytes(2).Length > 64 OrElse Bytes(3).Length > 64 OrElse Bytes(4).Length > 64 Then Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.LenghtOfSerializedNameExceeds0Bytes, 64))
            Dim arr(Bytes(0).Length + Bytes(1).Length + Bytes(2).Length + Bytes(3).Length + Bytes(4).Length - 1 + 4) As Byte
            Dim CurrPos As Integer = 0
            For i As Integer = 0 To 4
                If i > 0 Then
                    System.Text.Encoding.ASCII.GetBytes(":").CopyTo(arr, CurrPos)
                    CurrPos += 1
                End If
                Bytes(i).CopyTo(arr, CurrPos)
                CurrPos += Bytes(i).Length
            Next i
            Return arr
        End Function
    End Class

    ''' <summary>Common base for classes that have the <see cref="WithIPR.IPR"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Public MustInherit Class WithIpr
        ''' <summary>Contains value of the <see cref="IPR"/> property</summary>            
        <EditorBrowsable(EditorBrowsableState.Never)> Private _IPR As String = " "c
        ''' <summary>Information Provider Reference A name, registered with the IPTC/NAA, identifying the provider that guarantees the uniqueness of the UNO</summary>            
        ''' <remarks>A name, registered with the IPTC/NAA, identifying the provider that guarantees the uniqueness of the UNO</remarks>
        ''' <value>A minimum of one and a maximum of 32 octets. A string of graphic characters, except colon ‘:’ solidus ‘/’, asterisk ‘*’ and question mark ‘?’, registered with, and approved by, the IPTC.</value>
        ''' <exception cref="ArgumentException">Value being set contains unallowed characters (white space, *, :, /, ? or control characters) -or- value being set is an empty <see cref="String"/> or its <see cref="String.Length"/> if more than 32 -or- length of value being set exceeds <see cref="IPRLengthLimit"/> -or- value being set contains character with code higher than 127</exception>
        <LDescription(GetType(IPTCResources), "IPR_d")> _
        <Browsable(False)> _
        Public Overridable Property IPR() As String
            Get
                Return _IPR
            End Get
            Set(ByVal value As String)
                If Not Iptc.IsGraphicCharacters(value) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.OnlyGraphicCharactersAreAllowedIn0, MetadataT.IptcT.IPTCResources.IPR_n))
                If value.Contains("*"c) OrElse value.Contains("/"c) OrElse value.Contains("?"c) OrElse value.Contains(":"c) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.CannotContainCharactersAnd, MetadataT.IptcT.IPTCResources.IPR_n))
                If value = "" OrElse value.Length > 32 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.MustBeStringWithLengthFrom1To2Characters, MetadataT.IptcT.IPTCResources.IPR_n, 1, 32))
                If value.Length > IPRLengthLimit Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.TheLenghtOf0ExceedsLimit, MetadataT.IptcT.IPTCResources.IPR_n))
                For Each ch As Char In value
                    If AscW(ch) > 127 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.TextMustBeEncodeableByASCII, MetadataT.IptcT.IPTCResources.IPR_n))
                Next ch
                _IPR = value
            End Set
        End Property
        ''' <summary>Gets or sets value of the <see cref="IPR"/> property as member of <see cref="InformationProviders"/></summary>
        ''' <value>Value that is member of <see cref="InformationProviders"/></value>
        ''' <returns>Value that is member of <see cref="InformationProviders"/> if <see cref="IPR"/> can be represented as member of <see cref="InformationProviders"/>, -1 otherwise</returns>
        ''' <exception cref="InvalidEnumArgumentException">Setting value that is not member of <see cref="InformationProviders"/></exception>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
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
        ''' <summary>Value of either <see cref="IPR"/> or <see cref="ListedIPR"/> depending on if <see cref="IPR"/> is one of <see cref="InformationProviders"/> members</summary>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <LDisplayName(GetType(IPTCResources), "IPR_n"), LDescription(GetType(IPTCResources), "IPR_d2")> _
        <CLSCompliant(False)> _
        Public Property IPRValue() As StringEnum(Of InformationProviders)
            Get
                If ListedIPR = -1 Then
                    Return New StringEnum(Of InformationProviders)(IPR)
                Else
                    Return New StringEnum(Of InformationProviders)(ListedIPR)
                End If
            End Get
            Set(ByVal value As StringEnum(Of InformationProviders))
                If value.ContainsEnum Then ListedIPR = value.EnumValue Else IPR = value.StringValue
            End Set
        End Property
        ''' <summary>When overridden in derived class gets actual lenght limit for <see cref="IPR"/></summary>
        Protected MustOverride ReadOnly Property IPRLengthLimit() As Byte
    End Class

    ''' <summary>Represents IPTC UNO unique object identifier (IPTC type <see cref="IPTCTypes.UNO"/>)</summary>
    ''' <remarks>
    ''' <para>The first three elements of the UNO (the UCD, the IPR and the ODE) together are allocated to the editorial content of the object.</para>
    ''' <para>Any technical variants or changes in the presentation of an object, e.g. a picture being presented by a different file format, does not require the allocation of a new ODE but can be indicated by only generating a new OVI.</para>
    ''' <para>Links may be set up to the complete UNO but the structure provides for linking to selected elements, e.g. to all objects of a specified provider.</para>
    ''' </remarks>

    <DebuggerDisplay("{ToString}")> _
    <TypeConverter(GetType(IptcUno.Converter))> _
   Public Class IptcUno : Inherits WithIpr
        ''' <summary>Contains value of the <see cref="UCD"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _UCD As Date = Now.Date
        ''' <summary>UNO Creation Date Specifies a 24 hour period in which the further elements of the UNO have to be unique.</summary>            
        ''' <remarks>It also provides a search facility.</remarks>
        <LDescription(GetType(IPTCResources), "UCD_d")> _
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
        <CLSCompliant(False)> _
        Public Sub New(ByVal UCD As Date, ByVal IPR As StringEnum(Of InformationProviders), ByVal ODE As IEnumerable(Of String), ByVal OVI As String)
            Me.New()
            Me.UCD = UCD
            Me.IPRValue = IPR
            If ODE IsNot Nothing Then
                For Each item As String In ODE
                    Me.ODE.Add(item)
                Next item
            End If
            Me.ODE.RemoveAt(0)
            Me.OVI = OVI
        End Sub
        ''' <summary>CTor from byte array</summary>
        ''' <param name="Bytes">Bytes to initialize new instance by</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Bytes"/> is null or empty</exception>
        ''' <exception cref="ArgumentException">IPR or OVI part is invalid: contains unallowed characters (white space, *, :, /, ? or over code 127), is empty or violates lenght constraint. See <seealso cref="OVI"/> and <seealso cref="IPR"/> for more information -or- UCD component is to short or contains invalid date</exception>
        ''' <exception cref="IndexOutOfRangeException">There is not enough (4) parts separated by : in <paramref name="Bytes"/></exception>
        ''' <exception cref="InvalidCastException">UCD component contains non-numeric character</exception>
        ''' <exception cref="OperationCanceledException">ODE part is invalid. See <seealso cref="ODE"/> for more information.</exception>
        Public Sub New(ByVal Bytes As Byte())
            Me.New()
            If Bytes Is Nothing OrElse Bytes.Length = 0 Then Throw New ArgumentNullException("Bytes")
            Dim Text As String = System.Text.Encoding.ASCII.GetString(Bytes)
            Init(Text)
        End Sub
        ''' <summary>Pseudo-CTor from string</summary>
        ''' <param name="Text"><see cref="String"/> to initialize instance with</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Text"/> is null or empty</exception>
        ''' <exception cref="ArgumentException">IPR or OVI part is invalid: contains unallowed characters (white space, *, :, /, ? or over code 127), is empty or violates lenght constraint. See <seealso cref="OVI"/> and <seealso cref="IPR"/> for more information -or- UCD component is to short or contains invalid date</exception>
        ''' <exception cref="IndexOutOfRangeException">There is not enough (4) parts separated by : in <paramref name="Text"/></exception>
        ''' <exception cref="InvalidCastException">UCD component contains non-numeric character</exception>
        ''' <exception cref="OperationCanceledException">ODE part is invalid. See <seealso cref="ODE"/> for more information.</exception>
        Private Sub Init(ByVal Text As String)
            If Text Is Nothing OrElse Text = "" Then Throw New ArgumentNullException("Text", String.Format(ResourcesT.Exceptions.CannotBeNullOrEmpty, "Text"))
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
        ''' <summary>Converts <see cref="iptcUNO"/> to <see cref="String"/></summary>
        ''' <param name="From"><see cref="iptcUNO"/> to be converted</param>
        ''' <returns><see cref="ToString"/></returns>
        Public Shared Widening Operator CType(ByVal From As IptcUno) As String
            Return From.ToString
        End Operator
        ''' <summary>Converts <see cref="String"/> to <see cref="iptcUNO"/></summary>
        ''' <param name="Text"><see cref="String"/> to create <see cref="iptcUNO"/> from</param>
        ''' <returns>New instance of <see cref="iptcUNO"/> initialized by <paramref name="Text"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Text"/> is null or empty</exception>
        ''' <exception cref="ArgumentException">IPR or OVI part is invalid: contains unallowed characters (white space, *, :, /, ? or over code 127), is empty or violates lenght constraint. See <seealso cref="OVI"/> and <seealso cref="IPR"/> for more information -or- UCD component is to short or contains invalid date</exception>
        ''' <exception cref="IndexOutOfRangeException">There is not enough (4) parts separated by : in <paramref name="Text"/></exception>
        ''' <exception cref="InvalidCastException">UCD component contains non-numeric character</exception>
        ''' <exception cref="OperationCanceledException">ODE part is invalid. See <seealso cref="ODE"/> for more information.</exception>
        Public Shared Narrowing Operator CType(ByVal Text As String) As IptcUno
            Return New IptcUno(Text)
        End Operator
        ''' <summary>CTor from string</summary>
        ''' <param name="Text"><see cref="String"/> to create instance from</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Text"/> is null or empty</exception>
        ''' <exception cref="ArgumentException">IPR or OVI part is invalid: contains unallowed characters (white space, *, :, /, ? or over code 127), is empty or violates lenght constraint. See <seealso cref="OVI"/> and <seealso cref="IPR"/> for more information -or- UCD component is to short or contains invalid date</exception>
        ''' <exception cref="IndexOutOfRangeException">There is not enough (4) parts separated by : in <paramref name="Text"/></exception>
        ''' <exception cref="InvalidCastException">UCD component contains non-numeric character</exception>
        ''' <exception cref="OperationCanceledException">ODE part is invalid. See <seealso cref="ODE"/> for more information.</exception>
        Public Sub New(ByVal Text As String)
            Me.New()
            Init(Text)
        End Sub
        ''' <summary>Block <see cref="ODE"/>'s last item from being removed</summary>
        ''' <param name="sender"><see cref="_ODE"/></param>
        ''' <param name="e">Event parameters</param>
        Private Sub ODE_Removing(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).CancelableItemIndexEventArgs)
            If _ODE.Count = 0 Then
                e.Cancel = True
                e.CancelMessage = String.Format(ResourcesT.Exceptions.CannotRemoveLastItemFrom0, "ODE")
            End If
        End Sub
        ''' <summary>Block <see cref="ODE"/> from being cleared</summary>
        ''' <param name="sender"><see cref="_ODE"/></param>
        ''' <param name="e">Event parameters</param>
        Private Sub ODE_Clearing(ByVal sender As ListWithEvents(Of String), ByVal e As ComponentModelT.CancelMessageEventArgs)
            e.Cancel = True
            e.CancelMessage = String.Format(ResourcesT.Exceptions.CannotBeCleared, "ODE")
        End Sub
        ''' <summary>Controls if item added to <see cref="ODE"/> are valid</summary>
        ''' <param name="sender"><see cref="_ODE"/></param>
        ''' <param name="e">parameters of event</param>
        Private Sub ODE_Adding(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).CancelableItemIndexEventArgs)
            If Not Iptc.IsGraphicCharacters(e.Item) Then
                e.CancelMessage = String.Format(ResourcesT.Exceptions.CanConsistOnlyOfGraphicCharacters, "ODE")
                e.CancelMessage = True
            ElseIf e.Item.Contains("*"c) OrElse e.Item.Contains("/"c) OrElse e.Item.Contains("?"c) OrElse e.Item.Contains(":"c) Then
                e.Cancel = True
                e.CancelMessage = String.Format(ResourcesT.Exceptions.CannotContainCharactersAnd, "ODE")
            ElseIf e.Item = "" Then
                e.Cancel = True
                e.CancelMessage = String.Format(ResourcesT.Exceptions.ComponentCannotBeAnEmptyString, "ODE")
            Else
                For Each ch As Char In e.Item
                    If AscW(ch) > 127 Then
                        e.Cancel = True
                        e.CancelMessage = String.Format(ResourcesT.Exceptions.ComponentMustBeEncodeableByASCII, "ODE")
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
                    e.CancelMessage = ResourcesT.Exceptions.LengthOfODEAndIPRTogetherWithSeparatorsMustWitInto61
                End If
            End If
        End Sub
        ''' <summary>Object Descriptor Element In conjunction with the UCD and the IPR, a string of characters ensuring the uniqueness of the UNO.</summary>            
        ''' <value>A minimum of one and a maximum of 60 minus the number of IPR octets, consisting of graphic characters, except colon ‘:’ asterisk ‘*’ and question mark ‘?’. The provider bears the responsibility for the uniqueness of the ODE within a 24 hour cycle.</value>
        ''' <exception cref="OperationCanceledException">
        ''' The <see cref="ListWithEvents(Of String).Add"/> and <see cref="ListWithEvents(Of String).Item"/>'s setter can throw an <see cref="OperationCanceledException"/> when trying to add invalid item (containing invalid characters (?,:,?,* or with code over 127), too long or an empty string) or accumulated lenght of <see cref="IPR"/> and <see cref="ODE"/> (including <see cref="IPR"/>-<see cref="ODE"/> separator and separators of items of <see cref="ODE"/>) is greater than 61
        ''' -and- <see cref="ListWithEvents(Of String).Remove"/> and <see cref="ListWithEvents(Of String).RemoveAt"/> throws <see cref="OperationCanceledException"/> when trying to remove last item from <see cref="ODE"/>
        ''' -and- <see cref="ListWithEvents(Of String).Clear"/> throws <see cref="OperationCanceledException"/> everywhen
        ''' </exception>
        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property ODE() As IList(Of String)
            Get
                Return _ODE
            End Get
        End Property
        ''' <summary>Provides design-time support for editing the <see cref="ODE"/> property</summary>
        ''' <exception cref="OperationCanceledException">
        ''' Trying to array that contains an invalid item(s) (containing invalid characters (?,:,?,* or with code over 127), too long or an empty string) or accumulated lenght of <see cref="IPR"/> and <see cref="ODE"/> (including <see cref="IPR"/>-<see cref="ODE"/> separator and separators of items of <see cref="ODE"/>) is greater than 61 -or-
        ''' Trying to set an empty array
        ''' </exception>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        <LDisplayName(GetType(IPTCResources), "ODE_n"), DesignOnly(True), EditorBrowsable(EditorBrowsableState.Never)> _
        <LDescription(GetType(IPTCResources), "ODE_d")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property ODEDesign() As String()
            Get
                Return New List(Of String)(ODE).ToArray
            End Get
            Set(ByVal value As String())
                If value Is Nothing Then Throw New ArgumentNullException("value")
                Dim i As Integer
                For i = 0 To Math.Min(ODE.Count - 1, value.Length - 1)
                    ODE(i) = value(i)
                Next i
                If i < ODE.Count Then
                    While ODE.Count > i
                        ODE.RemoveAt(i)
                    End While
                ElseIf i < value.Length Then
                    For i = i To value.Length - 1
                        ODE.Add(value(i))
                    Next i
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="OVI"/> property</summary>            
        <EditorBrowsable(EditorBrowsableState.Never)> Private _OVI As String = "0"c
        ''' <summary>Object Variant Indicator A string of characters indicating technical variants of the object such as partial objects, or changes of file formats, and coded character sets.</summary>             
        ''' <value>A minimum of one and a maximum of 9 octets, consisting of graphic characters, except colon ‘:’, asterisk ‘*’ and question mark ‘?’. To indicate a technical variation of the object as so far identified by the first three elements. Such variation may be required, for instance, for the indication of part of the object, or variations of the file format, or coded character set. The default value is a single ‘0’ (zero) character indicating no further use of the OVI.</value>
        ''' <exception cref="ArgumentException">Value being set contains unallowed characters (white space, *, :, /, ? or control characters) -or- value being set is an empty <see cref="String"/> or its lenght is larger than 9 -or- value being set contains character with code higher than 127</exception>
        <LDescription(GetType(IPTCResources), "OVI_d")> _
        Public Property OVI() As String
            Get
                Return _OVI
            End Get
            Set(ByVal value As String)
                If Not Iptc.IsGraphicCharacters(value) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.OnlyGraphicCharactersAreAllowedIn0, "OVI"))
                If value.Contains("*"c) OrElse value.Contains("/"c) OrElse value.Contains("?"c) OrElse value.Contains(":"c) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.CannotContainCharactersAnd, "OVI"))
                If value.Length > 9 OrElse value = "" Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.MustBeStringWithLengthFrom1To2Characters, "OVI", 1, 9))
                For Each ch As Char In value
                    If AscW(ch) > 127 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.TextMustBeEncodeableByASCII, "OVI"))
                Next ch
                _OVI = value
            End Set
        End Property
        ''' <summary>String representation in form UCD:IPR:ODE1/ODE2/ODE3:OVI</summary>
        Overrides Function ToString() As String
            Dim ODEArr(ODE.Count - 1) As String
            ODE.CopyTo(ODEArr, 0)
            Return String.Format(InvariantCulture, "{0:yyyyMMdd}:{1}:{2}:{3}", UCD, IPR, String.Join("/"c, ODEArr), OVI)
        End Function
        ''' <summary><see cref="System.ComponentModel.TypeConverter"/> for <see cref="IptcUno"/></summary>
        Public Class Converter : Inherits ExpandableObjectConverter
            ''' <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="destinationType">A <see cref="System.Type"/> that represents the type you want to convert to.</param>
            ''' <returns>True is <paramref name="destinationType"/> is <see cref="[String]"/> otherwise calls <see cref="System.ComponentModel.TypeConverter.CanConvertTo"/></returns>
            Public Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
                Return destinationType.Equals(GetType(String)) OrElse MyBase.CanConvertTo(context, destinationType)
            End Function
            ''' <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="destinationType">The <see cref="System.Type"/> to convert the value parameter to.</param>
            ''' <param name="value">The <see cref="System.Object"/> to convert.</param>
            ''' <returns>An <see cref="System.Object"/> that represents the converted value.</returns>
            ''' <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
            ''' <exception cref="System.ArgumentNullException">The <paramref name="destinationType"/> parameter is null.</exception>
            Public Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
                If destinationType.Equals(GetType(String)) Then
                    If value Is Nothing Then Return ""
                    Return value.ToString
                Else
                    Return MyBase.ConvertTo(context, culture, value, destinationType)
                End If
            End Function
            ''' <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="sourceType">A <see cref="System.Type"/> that represents the type you want to convert from.</param>
            ''' <returns>True if <paramref name="sourceType"/> is <see cref="String"/> otherwice callse <see cref="System.ComponentModel.ExpandableObjectConverter.CanConvertFrom"/></returns>
            Public Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
                Return sourceType.Equals(GetType(String)) OrElse MyBase.CanConvertFrom(context, sourceType)
            End Function
            ''' <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="value">The <see cref="System.Object"/> to convert.</param>
            ''' <returns>An <see cref="System.Object"/> that represents the converted value.</returns>
            ''' <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
            Public Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
                If TypeOf value Is String Then Return New IptcUno(CStr(value))
                Return MyBase.ConvertFrom(context, culture, value)
            End Function
            ''' <summary>Returns whether changing a value on this object requires a call to System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary) to create a new value, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>True</returns>
            Public Overrides Function GetCreateInstanceSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                Return True
            End Function
            ''' <summary>Creates an instance of the type that this System.ComponentModel.TypeConverter is associated with, using the specified context, given a set of property values for the object.</summary>
            ''' <param name="propertyValues">An <see cref="System.Collections.IDictionary"/> of new property values.</param>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>New instance of <see cref="iptcUNO"/></returns>
            Public Overrides Function CreateInstance(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal propertyValues As System.Collections.IDictionary) As Object
                Return New IptcUno(propertyValues!UCD, propertyValues!IPRValue, propertyValues!ODEDesign, propertyValues!OVI)
            End Function
            ''' <summary>Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>True if <see cref="PropertyDescriptor.GetValue"/> of <see cref="ITypeDescriptorContext.PropertyDescriptor"/> of <paramref name="context"/> is null</returns>
            Public Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                Return context IsNot Nothing AndAlso context.PropertyDescriptor.GetValue(context.Container) Is Nothing
            End Function
            ''' <summary>Returns whether the collection of standard values returned from <see cref="System.ComponentModel.TypeConverter.GetStandardValues"/> is an exclusive list of possible values, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>True</returns>
            Public Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                Return False
            End Function
            ''' <summary>Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
            ''' <returns>Instance of <see cref="iptcUNO"/> if <see cref="PropertyDescriptor.GetValue"/> of <see cref="ITypeDescriptorContext.PropertyDescriptor"/> of <paramref name="context"/> is null; null otherwise</returns>
            Public Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
                If context IsNot Nothing AndAlso context.PropertyDescriptor.GetValue(context.Container) Is Nothing Then
                    Return New StandardValuesCollection(New IptcUno() {New IptcUno()})
                Else
                    Return Nothing
                End If
            End Function
        End Class

    End Class

    ''' <summary>Represents combination of number and string</summary>
    ''' <remarks>This class is abstract, derived class mus specify number of digits of <see cref="NumStr.Number"/></remarks>
    <TypeConverter(GetType(NumStr.Converter))> _
    <Editor(GetType(NewEditor), GetType(UITypeEditor))> _
    Public MustInherit Class NumStr
        ''' <summary>Contains value of the <see cref="Number"/> property</summary>            
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Number As Integer
        ''' <summary>Contains value of the <see cref="[String]"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _String As String
        ''' <summary>If overridden in derived class returns number of digits in number. Should not be zero.</summary>            
        Protected MustOverride ReadOnly Property NumberDigits() As Byte
        ''' <summary>Number in this <see cref="NumStr"/></summary>            
        ''' <exception cref="ArgumentException">Number being set converted to string is longer than <see cref="NumberDigits"/></exception>
        ''' <exception cref="ArgumentOutOfRangeException">Number beign set is negative</exception>
        Public Overridable Property Number() As Integer
            Get
                Return _Number
            End Get
            Set(ByVal value As Integer)
                If Number.ToString(New String("0"c, NumberDigits), InvariantCulture).Length > NumberDigits Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.HasToManyDigits, "Number"))
                If Number < 0 Then Throw New ArgumentOutOfRangeException(String.Format(ResourcesT.Exceptions.CannotBeNegative, "Number"))
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
        ''' <summary><see cref="System.ComponentModel.TypeConverter"/> for <see cref="NumStr"/></summary>
        Public Class Converter : Inherits ExpandableObjectConverter
            ''' <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="destinationType">A <see cref="System.Type"/> that represents the type you want to convert to.</param>
            ''' <returns>True if <paramref name="destinationType"/> is <see cref="[String]"/> otherwise calls <see cref="System.ComponentModel.TypeConverter.CanConvertTo"/></returns>
            Public Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
                Return GetType(String).Equals(destinationType) OrElse MyBase.CanConvertTo(context, destinationType)
            End Function
            ''' <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="destinationType">The <see cref="System.Type"/> to convert the value parameter to.</param>
            ''' <param name="value">The <see cref="System.Object"/> to convert.</param>
            ''' <returns>An <see cref="System.Object"/> that represents the converted value.</returns>
            ''' <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
            ''' <exception cref="System.ArgumentNullException">The <paramref name="destinationType"/> parameter is null.</exception>
            Public Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
                If destinationType IsNot Nothing AndAlso destinationType.Equals(GetType(String)) Then
                    If value Is Nothing Then Return ""
                    Return value.ToString
                End If
                Return MyBase.ConvertTo(context, culture, value, destinationType)
            End Function
            ''' <summary>Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)"/> to create a new value, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>True if <see cref="PropertyDescriptor.PropertyType"/> of <see cref="ITypeDescriptorContext"/> of <paramref name="context"/> is not null and is subclass of <see cref="NumStr"/> (not <see cref="NumStr"/> itself)</returns>
            Public Overrides Function GetCreateInstanceSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                Try
                    Return context IsNot Nothing AndAlso context.PropertyDescriptor IsNot Nothing AndAlso context.PropertyDescriptor.PropertyType IsNot Nothing AndAlso context.PropertyDescriptor.PropertyType.IsSubclassOf(GetType(NumStr))
                Catch ex As NullReferenceException
                    Return False
                End Try
            End Function
            ''' <summary>Creates an instance of the type that this <see cref="System.ComponentModel.TypeConverter"/> is associated with, using the specified context, given a set of property values for the object.</summary>
            ''' <param name="propertyValues">An <see cref="System.Collections.IDictionary"/> of new property values.</param>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>Instance of subclass of <see cref="NumStr"/> is type of property can be obtained from <paramref name="context"/> and it's subclass of <see cref="NumStr"/>; null otherwise</returns>
            ''' <exception cref="ArgumentException">Number property converted to string is longer than <see cref="NumberDigits"/></exception>
            ''' <exception cref="ArgumentOutOfRangeException">Number property is negative</exception>
            ''' <exception cref="InvalidEnumArgumentException">Type of property is constrained to enumerations and has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> True or it has no <see cref="RestrictAttribute"/> and Number property is not member of the enumeration</exception>
            Public Overrides Function CreateInstance(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal propertyValues As System.Collections.IDictionary) As Object
                If GetCreateInstanceSupported(context) Then
                    If context.PropertyDescriptor.PropertyType.IsGenericType Then
                        Return Activator.CreateInstance(context.PropertyDescriptor.PropertyType, propertyValues!EnumNumber, propertyValues!String)
                    Else
                        Return Activator.CreateInstance(context.PropertyDescriptor.PropertyType, propertyValues!Number, propertyValues!String)
                    End If
                Else
                    Return Nothing
                End If
            End Function
            ''' <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="value">The <see cref="System.Object"/> to convert.</param>
            ''' <returns>Converted insvance of <see cref="NumStr"/> if <see cref="GetCreateInstanceSupported"/> returns true and <paramref name="value"/> consists of 2 ;-separated components or <paramref name="value"/> consists of 3 or 4 components; calls <see cref="System.ComponentModel.TypeConverter.ConvertFrom"/> otherwise.</returns>
            ''' <exception cref="IndexOutOfRangeException"><paramref name="value"/> does not repsesent <see cref="[String]"/> consisting of 2 ;-separated parts</exception>
            ''' <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
            ''' <exception cref="ArgumentException">Numeric (1st) part of <paramref name="value"/> converted to string is longer than <see cref="NumberDigits"/> -or-  Name of type (in 3 or 4 components-consisting string) is invalid, for example if it contains invalid characters, or if it is a zero-length string. -or- error when creating generic type from 4 components</exception>
            ''' <exception cref="ArgumentOutOfRangeException">Numeric (1st) part of <paramref name="value"/> is negative</exception>
            ''' <exception cref="InvalidEnumArgumentException">Type of property is constrained to enumerations and has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> True or it has no <see cref="RestrictAttribute"/> and first part of <paramref name="value"/> is not member of the enumeration</exception>
            ''' <exception cref="InvalidCastException">First part of <see cref="ValueType"/> cannot be converted to <see cref="Integer"/></exception>
            ''' <exception cref="InvalidOperationException">There are neither 3 nor 4 components in <paramref name="context"/> and <paramref name="context"/> or any of it's values leading to <see cref="PropertyDescriptor.PropertyType"/> or thet value itself is null -or- There are 4 components in <paramref name="value"/> but first component is not generic type definition</exception>
            ''' <remarks>
            ''' If <paramref name="value"/> consists of 2 ;-separated components <paramref name="context"/> is needed to be non-null to obtain type of propery that will be instantiated.
            ''' If <paramref name="value"/> consists of 3 components then first components denotes type. If it consists of 4 components then second componend is passed as typeparameter to first component. Types are expected as full names.
            ''' </remarks>
            Public Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
                If TypeOf value Is String Then
                    Dim Parts As String() = CStr(value).Split(";"c)
                    Dim ItemValue As Object
                    If Parts.Length = 3 Or Parts.Length = 4 Then
                        Dim EType As Type
                        Dim ItemType As Type = GetType(Integer)
                        If Parts.Length = 4 Then 'Type;GenericType;Num;Str
                            ItemType = Type.GetType(Parts(1))
                            EType = Type.GetType(Parts(0))
                            EType = EType.MakeGenericType(ItemType)
                            Dim ActivatorType As Type = GetType(Enumerator(Of )).MakeGenericType(ItemType)
                            Dim c As Object = Activator.CreateInstance(ActivatorType, CInt(Parts(Parts.Length - 2)))
                            ItemValue = ActivatorType.GetField("Value").GetValue(c)
                        Else 'Type;Num;Str
                            EType = Type.GetType(Parts(0))
                            ItemValue = CInt(Parts(Parts.Length - 2))
                        End If
                        Return Activator.CreateInstance(EType, ItemValue, Parts(Parts.Length - 1))
                    ElseIf GetCreateInstanceSupported(context) Then
                        Return Activator.CreateInstance(context.PropertyDescriptor.PropertyType, CInt(Parts(0)), Parts(1))
                    Else
                        Throw New InvalidOperationException(ResourcesT.Exceptions.TypeIsSpecifiedNeitherViaPropertyNorInValue)
                    End If
                End If
                Return MyBase.ConvertFrom(context, culture, value)
            End Function
            Private Class Enumerator(Of T As {IConvertible, Structure})
                Public ReadOnly Value As T
                Public Sub New(ByVal Value As Integer)
                    Me.Value = CObj(Value)
                End Sub
            End Class
            ''' <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="sourceType">A <see cref="System.Type"/> that represents the type you want to convert from.</param>
            ''' <returns>True if <paramref name="sourceType"/> is <see cref="System.String"/>; otherwice calls <see cref="System.ComponentModel.TypeConverter.CanConvertFrom"/></returns>
            Public Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
                Return GetType(String).Equals(sourceType) OrElse MyBase.CanConvertFrom(context, sourceType)
            End Function
        End Class
        ''' <summary>CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor from number and string</summary>
        ''' <param name="Num">Number</param>
        ''' <param name="Str"><see cref="String"/></param>
        ''' <exception cref="ArgumentException"><paramref name="Num"/> converted to string is longer than <see cref="NumberDigits"/></exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Num"/> beign set is negative</exception>
        Public Sub New(ByVal Num As Integer, ByVal Str As String)
            Me.Number = Num
            Me.String = Str
        End Sub
    End Class

    ''' <summary>Represents combination of 2-digits numer and string (IPTC type <see cref="IPTCTypes.Num2_Str"/>)</summary>
    <TypeConverter(GetType(NumStr.Converter))> _
    <Editor(GetType(NewEditor), GetType(UITypeEditor))> _
    Public Class NumStr2 : Inherits NumStr
        ''' <summary>Number of digits in number</summary>
        ''' <returns>2</returns>
        Protected Overrides ReadOnly Property NumberDigits() As Byte
            Get
                Return 2
            End Get
        End Property
        ''' <summary>CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor from number and string</summary>
        ''' <param name="Num">Number</param>
        ''' <param name="Str"><see cref="String"/></param>
        ''' <exception cref="ArgumentException"><paramref name="Num"/> converted to string is longer than <see cref="NumberDigits"/> (2)</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Num"/> beign set is negative</exception>
        Public Sub New(ByVal Num As Integer, ByVal Str As String)
            MyBase.New(Num, Str)
        End Sub
    End Class

    ''' <summary><see cref="T:NumStr2"/> with numbers from enum</summary>
    <CLSCompliant(False), TypeConverter(GetType(NumStr.Converter))> _
    <Editor(GetType(NewEditor), GetType(UITypeEditor))> _
    Public Class NumStr2(Of T As {IConvertible, Structure}) : Inherits NumStr2
        ''' <summary>Number in this <see cref="NumStr2(Of T)"/></summary>            
        ''' <exception cref="ArgumentException">Number being set converted to string is longer than 2 <see cref="NumberDigits"/> -or- <typeparamref name="T"/> is not <see cref="[Enum]"/></exception>
        ''' <exception cref="ArgumentOutOfRangeException">Number beign set is negative</exception>
        ''' <exception cref="InvalidEnumArgumentException"><typeparamref name="T"/> has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> True or it has no <see cref="RestrictAttribute"/> and value being set is not member of <typeparamref name="T"/></exception>
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
        ''' <exception cref="ArgumentException">Number being set converted to string is longer than 2 <see cref="NumberDigits"/> -or- <typeparamref name="T"/> is not <see cref="[Enum]"/></exception>
        ''' <exception cref="ArgumentOutOfRangeException">Number beign set is negative</exception>
        ''' <exception cref="InvalidEnumArgumentException"><typeparamref name="T"/> has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> True or it has no <see cref="RestrictAttribute"/> and value being set is not member of <typeparamref name="T"/></exception>
        <EditorBrowsable(EditorBrowsableState.Never), Browsable(False)> _
        Public NotOverridable Overrides Property Number() As Integer
            Get
                Return EnumNumber.ToInt32(InvariantCulture)
            End Get
            Set(ByVal value As Integer)
                EnumNumber = CObj(value)
            End Set
        End Property
        ''' <summary>CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor from number and string</summary>
        ''' <param name="Num">Number</param>
        ''' <param name="Str"><see cref="String"/></param>
        ''' <exception cref="ArgumentException"><paramref name="Num"/> converted to string is longer than <see cref="NumberDigits"/> (2)</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Num"/> beign set is negative</exception>
        ''' <exception cref="InvalidEnumArgumentException"><typeparamref name="T"/> has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> True or it has no <see cref="RestrictAttribute"/> and <paramref name="Num"/> is not member of <typeparamref name="T"/></exception>
        Public Sub New(ByVal Num As T, ByVal Str As String)
            Me.EnumNumber = Num
            Me.String = Str
        End Sub
    End Class

    ''' <summary><see cref="T:NumStr3"/> with numbers from enum</summary>
    <CLSCompliant(False), TypeConverter(GetType(NumStr.Converter))> _
    <Editor(GetType(NewEditor), GetType(UITypeEditor))> _
    Public Class NumStr3(Of T As {IConvertible, Structure}) : Inherits NumStr3
        ''' <summary>Number in this <see cref="NumStr3(Of T)"/></summary>            
        ''' <exception cref="ArgumentException">Number being set converted to string is longer than 3 <see cref="NumberDigits"/> -or- <typeparamref name="T"/> is not <see cref="[Enum]"/></exception>
        ''' <exception cref="ArgumentOutOfRangeException">Number beign set is negative</exception>
        ''' <exception cref="InvalidEnumArgumentException"><typeparamref name="T"/> has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> True or it has no <see cref="RestrictAttribute"/> and value being set is not member of <typeparamref name="T"/></exception>
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
        ''' <exception cref="ArgumentException">Number being set converted to string is longer than 3 <see cref="NumberDigits"/> -or- <typeparamref name="T"/> is not <see cref="[Enum]"/></exception>
        ''' <exception cref="ArgumentOutOfRangeException">Number beign set is negative</exception>
        ''' <exception cref="InvalidEnumArgumentException"><typeparamref name="T"/> has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> True or it has no <see cref="RestrictAttribute"/> and value being set is not member of <typeparamref name="T"/></exception>
        <EditorBrowsable(EditorBrowsableState.Never), Browsable(False)> _
        Public NotOverridable Overrides Property Number() As Integer
            Get
                Return EnumNumber.ToInt32(InvariantCulture)
            End Get
            Set(ByVal value As Integer)
                EnumNumber = CObj(value)
            End Set
        End Property
        ''' <summary>CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor from number and string</summary>
        ''' <param name="Num">Number</param>
        ''' <param name="Str"><see cref="String"/></param>
        ''' <exception cref="ArgumentException"><paramref name="Num"/> converted to string is longer than <see cref="NumberDigits"/> (3)</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Num"/> beign set is negative</exception>
        ''' <exception cref="InvalidEnumArgumentException"><typeparamref name="T"/> has <see cref="RestrictAttribute"/> with <see cref="RestrictAttribute.Restrict"/> True or it has no <see cref="RestrictAttribute"/> and <paramref name="Num"/> is not member of <typeparamref name="T"/></exception>
        Public Sub New(ByVal Num As T, ByVal Str As String)
            Me.EnumNumber = Num
            Me.String = Str
        End Sub
    End Class

    ''' <summary>Represents combination of 3-digits numer and string (IPTC type <see cref="IPTCTypes.Num3_Str"/>)</summary>
    <TypeConverter(GetType(NumStr.Converter))> _
    <Editor(GetType(NewEditor), GetType(UITypeEditor))> _
    Public Class NumStr3 : Inherits NumStr
        ''' <summary>Number of digits in number</summary>
        ''' <returns>3</returns>
        Protected Overrides ReadOnly Property NumberDigits() As Byte
            Get
                Return 3
            End Get
        End Property
        ''' <summary>CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor from number and string</summary>
        ''' <param name="Num">Number</param>
        ''' <param name="Str"><see cref="String"/></param>
        ''' <exception cref="ArgumentException"><paramref name="Num"/> converted to string is longer than <see cref="NumberDigits"/> (3)</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Num"/> beign set is negative</exception>
        Public Sub New(ByVal Num As Integer, ByVal Str As String)
            MyBase.New(Num, Str)
        End Sub
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
    <Editor(GetType(OmmitableDate.TypeEditor), GetType(UITypeEditor))> _
    <TypeConverter(GetType(OmmitableDate.Converter))> _
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
                If value < 0 OrElse value > 9999 Then Throw New ArgumentOutOfRangeException("value", String.Format(ResourcesT.Exceptions.MustBeFromRange12Or3IfUnknown, "Year", 1, 9999, 0))
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
                If value > 31 Then Throw New ArgumentOutOfRangeException("value", String.Format(ResourcesT.Exceptions.MustBeLessThenOrEqualTo1, "Day", 31))
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
                If value > 12 Then Throw New ArgumentOutOfRangeException("value", String.Format(ResourcesT.Exceptions.MustBeLessThenOrEqualTo1, "Month", 12))
                _Month = value
            End Set
        End Property
        ''' <summary>String representation in YYYYMMDD format</summary>
        Public Overrides Function ToString() As String
            Return String.Format(InvariantCulture, "{0:0000}{1:00}{2:00}", Year, Month, Day)
        End Function
        ''' <summary>Converts <see cref="String"/> to <see cref="OmmitableDate"/></summary>
        ''' <param name="From"><see cref="String"/> to be converted</param>
        ''' <returns><see cref="OmmitableDate"/> created from <paramref name="From"/> in form YYYYMMDD</returns>
        ''' <exception cref="InvalidCastException">Conversion cannot be performed</exception>
        Public Shared Narrowing Operator CType(ByVal From As String) As OmmitableDate
            Try
                Return New OmmitableDate(From.Substring(0, 4), From.Substring(4, 2), From.Substring(6, 2))
            Catch ex As Exception
                Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, From, "OmmitableDate"), ex)
            End Try
        End Operator
        ''' <summary>Converts <see cref="OmmitableDate"/> to <see cref="String"/></summary>
        ''' <param name="From"><see cref="OmmitableDate"/> to be converted</param>
        ''' <returns><see cref="OmmitableDate.ToString"/></returns>
        Public Shared Widening Operator CType(ByVal From As OmmitableDate) As String
            Return From.ToString
        End Operator
        ''' <summary><see cref="System.ComponentModel.TypeConverter"/> of <see cref="OmmitableDate"/> to and from <see cref="String"/> and <see cref="Date"/></summary>
        Public Class Converter
            Inherits TypeConverter(Of OmmitableDate, String)
            Implements ITypeConverter(Of Date)
            ''' <summary>Performs conversion from <see cref="String"/> to <see cref="OmmitableDate"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to <see cref="OmmitableDate"/></param>
            ''' <returns><see cref="OmmitableDate"/> initialized by <paramref name="value"/></returns>
            ''' <exception cref="InvalidCastException">Conversion cannot be performed</exception>
            Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As String) As OmmitableDate
                Return value
            End Function
            ''' <summary>Performs conversion from <see cref="OmmitableDate"/> to <see cref="String"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in <see cref="String"/></returns>
            Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As OmmitableDate) As String
                Return value.ToString
            End Function
            ''' <summary>Performs conversion from <see cref="Date"/> to <see cref="OmmitableDate"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to <see cref="OmmitableDate"/></param>
            ''' <returns><see cref="OmmitableDate"/> initialized by <paramref name="value"/></returns>
            ''' <exception cref="InvalidCastException">Conversion cannot be performed</exception>
            Public Overloads Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Date) As OmmitableDate Implements ITypeConverterFrom(Of Date).ConvertFrom
                Return value
            End Function
            ''' <summary>Performs conversion from <see cref="OmmitableDate"/> to <see cref="Date"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in <see cref="Date"/></returns>
            ''' <exception cref="InvalidCastException">This instance cannot be converted to <see cref="Date"/> because it contains invalid date or 0 in some propery</exception>
            Public Function ConvertToDate(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As OmmitableDate) As Date Implements ITypeConverterTo(Of Date).ConvertTo
                Return value
            End Function
        End Class
        Public Class TypeEditor : Inherits System.ComponentModel.Design.DateTimeEditor
            Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
                Dim DOValue As OmmitableDate = value
                Dim DateValue As Date
                If DOValue.Year = 0 Then
                    DateValue = Now
                ElseIf DOValue.Month = 0 Then
                    DateValue = New Date(DOValue.Year, 1, 1)
                ElseIf DOValue.Day = 0 Then
                    DateValue = New Date(DOValue.Year, DOValue.Month, 1)
                Else
                    DateValue = DOValue
                End If
                Dim NewDateValue As Date = MyBase.EditValue(context, provider, DateValue)
                Return CType(NewDateValue, OmmitableDate)
            End Function

        End Class
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
        ''' <exception cref="ArgumentOutOfRangeException">Settign value otside of range <see cref="Minimum"/>÷<see cref="Maximum"/></exception>
        Public Property Time() As TimeSpan
            Get
                Return _Time
            End Get
            Set(ByVal value As TimeSpan)
                If value < Minimum OrElse value > Maximum Then Throw New ArgumentOutOfRangeException("value", String.Format(ResourcesT.Exceptions.MustBePositiveAndLessThan1H0MmSs, Time, New TimeSpanFormattable(99, 59, 59)))
                _Time = TimeSpan.FromSeconds(Math.Truncate(value.TotalSeconds))
            End Set
        End Property
        ''' <summary>Time zone offset of <see cref="Time"/></summary>
        ''' <exception cref="ArgumentException">Setting offset to time with non-zero sub-minute component</exception>
        ''' <exception cref="ArgumentOutOfRangeException">Setting offset outside of range <see cref="MinOffset"/>÷<see cref="MaxOffset"/></exception>
        Public Property Offset() As TimeSpan
            Get
                Return _Offset
            End Get
            Set(ByVal value As TimeSpan)
                If Offset.TotalMinutes <> Int(Offset.TotalMinutes) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.MustBeInWholeMinutes, "Offset"))
                If value < MinOffset OrElse value > MaxOffset Then Throw New ArgumentOutOfRangeException(String.Format("value", ResourcesT.Exceptions.MustBeWithinRange01, MinOffset, MaxOffset, "Offset"))
                _Offset = value
            End Set
        End Property
#Region "Component properties"
        ''' <summary>Hour component of <see cref="Time"/></summary>
        ''' <exception cref="ArgumentOutOfRangeException">setting such value that <see cref="Time"/> leves range <see cref="Minimum"/>÷<see cref="Maximum"/></exception>
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
        ''' <exception cref="ArgumentOutOfRangeException">setting such value that <see cref="Time"/> leves range <see cref="Minimum"/>÷<see cref="Maximum"/></exception>
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
        ''' <exception cref="ArgumentOutOfRangeException">setting such value that <see cref="Time"/> leves range <see cref="Minimum"/>÷<see cref="Maximum"/></exception>
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
        ''' <exception cref="ArgumentOutOfRangeException">Setting value such that <see cref="Offset"/> leaves range <see cref="MinOffset"/>÷<see cref="MaxOffset"/></exception>
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
        ''' <exception cref="ArgumentOutOfRangeException">Setting value such that <see cref="Offset"/> leaves range <see cref="MinOffset"/>÷<see cref="MaxOffset"/></exception>
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
        ''' <exception cref="ArgumentOutOfRangeException">Setting value such that <see cref="Offset"/> leaves range <see cref="MinOffset"/>÷<see cref="MaxOffset"/></exception>
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
        ''' <summary>String representation in the HHMMSS±HHMM format</summary>
        Public Overrides Function ToString() As String
            Return String.Format(InvariantCulture, "{0:00}{1:00}{2:00}{3}{4:00}{5:00}", Hours, Minutes, Seconds, If(Time < TimeSpan.Zero, "-"c, "+"c), OffsetHourAbs, OffsetMinuteAbs)
        End Function
#Region "CTors"
        ''' <summary>CTor</summary>
        ''' <param name="Hours">Hour component</param>
        ''' <param name="Minutes">Minute component</param>
        ''' <param name="Seconds">Second component</param>
        ''' <param name="HourOffset">Hour component of offset</param>
        ''' <param name="MinuteOffset">Minute component of offset</param>
        ''' <exception cref="ArgumentOutOfRangeException">Time component exceeds range <see cref="Minimum"/>÷<see cref="Maximum"/> -or- offset component exceds range <see cref="MinOffset"/>÷<see cref="MaxOffset"/></exception>
        <CLSCompliant(False)> _
        Public Sub New(ByVal Hours As Byte, ByVal Minutes As Byte, ByVal Seconds As Byte, Optional ByVal HourOffset As SByte = 0, Optional ByVal MinuteOffset As Byte = 0)
            Me.New(New TimeSpan(Hours, Minutes, Seconds), New TimeSpan(HourOffset, MinuteOffset, 0))
        End Sub
        ''' <summary>CTor from <see cref="TimeSpan"/></summary>
        ''' <param name="Time"><see cref="TimeSpan"/> to initialize this instance (time local in UTC+0:00)</param>
        ''' <remarks>Offset is initialized to <see cref="TimeSpan.Zero"/></remarks>
        ''' <exception cref="ArgumentOutOfRangeException">Time component exceeds range <see cref="Minimum"/>÷<see cref="Maximum"/></exception>
        Public Sub New(ByVal Time As TimeSpan)
            Me.New(Time, TimeSpan.Zero)
        End Sub
        ''' <summary>CTor from <see cref="TimeSpan"/></summary>
        ''' <param name="Time"><see cref="TimeSpan"/> to initialize this instance (local time)</param>
        ''' <param name="Offset">Time zone offset</param>
        ''' <exception cref="ArgumentOutOfRangeException">Time component exceeds range <see cref="Minimum"/>÷<see cref="Maximum"/> -or- offset component exceds range <see cref="MinOffset"/>÷<see cref="MaxOffset"/></exception>
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
                ''' <summary>*HH:MM:SS±HH:MM</summary>
                H1
                ''' <summary>H*H:MM:SS±HH:MM</summary>
                H2
                ''' <summary>HH*:MM:SS±HH:MM</summary>
                H3
                ''' <summary>HH:*MM:SS±HH:MM</summary>
                M1
                ''' <summary>HH:M*M:SS±HH:MM</summary>
                M2
                ''' <summary>HH:MM*:SS±HH:MM</summary>
                M3
                ''' <summary>HH:MM:*SS±HH:MM</summary>
                S1
                ''' <summary>HH:MM:S*S±HH:MM</summary>
                S2
                ''' <summary>HH:MM:SS*±HH:MM</summary>
                S3
                ''' <summary>HH:MM:SS±*HH:MM</summary>
                OH1
                ''' <summary>HH:MM:SS±H*H:MM</summary>
                OH2
                ''' <summary>HH:MM:SS±HH*:MM</summary>
                OH3
                ''' <summary>HH:MM:SS±HH:*MM</summary>
                OM1
                ''' <summary>HH:MM:SS±HH:M*M</summary>
                OM2
                ''' <summary>HH:MM:SS±HH:MM*</summary>
                All
            End Enum
            ''' <summary>Performs conversion from <see cref="String"/> to <see cref="Time"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to <see cref="Time"/></param>
            ''' <returns>Value of type <see cref="Time"/> initialized by <paramref name="value"/></returns>
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
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
                            End Select
                        Case ParseAutomat.H2
                            Select Case ch
                                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                    rH = CStr(value).Substring(i - 1, 2)
                                    state = ParseAutomat.H3
                                Case ":"c
                                    rH = CStr(value).Substring(i - 1, 1)
                                    state = ParseAutomat.M1
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
                            End Select
                        Case ParseAutomat.H3
                            Select Case ch
                                Case ":"c : state = ParseAutomat.M1
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
                            End Select
                        Case ParseAutomat.M1
                            Select Case ch
                                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                    state = ParseAutomat.M2
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
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
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
                            End Select
                        Case ParseAutomat.M3
                            Select Case ch
                                Case ":"c : state = ParseAutomat.S1
                                Case "-"c
                                    rMinus = True
                                    state = ParseAutomat.OH1
                                Case "+"c : state = ParseAutomat.OH1
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
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
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
                            End Select
                        Case ParseAutomat.S3
                            Select Case ch
                                Case "-"c
                                    rMinus = True
                                    state = ParseAutomat.OH1
                                Case "+"c : state = ParseAutomat.OH1
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
                            End Select
                        Case ParseAutomat.OH1
                            Select Case ch
                                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                    state = ParseAutomat.OH2
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
                            End Select
                        Case ParseAutomat.OH2
                            Select Case ch
                                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                    rOH = CStr(value).Substring(i - 1, 2)
                                    state = ParseAutomat.OH3
                                Case ":"c
                                    rOH = CStr(value).Substring(i - 1, 1)
                                    state = ParseAutomat.OM1
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
                            End Select
                        Case ParseAutomat.OH3
                            Select Case ch
                                Case ":"c : state = ParseAutomat.OM1
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
                            End Select
                        Case ParseAutomat.OM1
                            Select Case ch
                                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                    state = ParseAutomat.OM2
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
                            End Select
                        Case ParseAutomat.OM2
                            Select Case ch
                                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                    rOM = CStr(value).Substring(i - 1, 2)
                                    state = ParseAutomat.All
                                Case Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
                            End Select
                        Case ParseAutomat.All
                            Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
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
                        Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"))
                End Select
                Try
                    Dim Time As New TimeSpan(rH, rM, rS)
                    Dim Offset As New TimeSpan(rOH, rOM, 0)
                    If rMinus Then Offset = -Offset
                    Return New Time(Time, Offset)
                Catch ex As Exception
                    Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, value, "IPTC.Time"), ex)
                End Try
            End Function
            ''' <summary>Performs conversion from <see cref="Time"/> to <see cref="String"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in <see cref="String"/></returns>
            Public Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Time) As String
                With value
                    Return String.Format(InvariantCulture, "{0:0}:{1:00}:{2:00}{3}{4:0}:{5:00}", .Hours, .Minutes, .Seconds, If(.NegativeOffset, "-"c, "+"c), .OffsetHourAbs, .OffsetMinuteAbs)
                End With
            End Function
        End Class
    End Structure

    ''' <summary>IPTC image type (IPTC type <see cref="IPTCTypes.ImageType"/>)</summary>
    <TypeConverter(GetType(IptcImageType.Converter))> _
    <DebuggerDisplay("{ToString}")> _
    Public Structure IptcImageType : Implements IMediaType(Of ImageTypeComponents, ImageTypeContents)
        ''' <summary>CTor</summary>
        ''' <param name="TypeCode"><see cref="Type"/> as <see cref="Char"/></param>
        ''' <param name="Components">Number of components</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Components"/> is not member of <see cref="ImageTypeComponents"/> -or- <paramref name="TypeCode"/> cannot be interpreted as member of <see cref="ImageTypeContents"/></exception>
        Public Sub New(ByVal Components As Byte, ByVal TypeCode As Char)
            Me.Components = Components
            For Each cns As Reflection.FieldInfo In GetType(ImageTypeContents).GetFields
                If cns.IsLiteral AndAlso cns.IsPublic Then
                    Dim attrs As Object() = cns.GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)
                    If (attrs IsNot Nothing AndAlso attrs.Length > 0 AndAlso DirectCast(attrs(0), Xml.Serialization.XmlEnumAttribute).Name = TypeCode) OrElse ((attrs Is Nothing OrElse attrs.Length = 0) AndAlso cns.Name = TypeCode) Then
                        Me.Type = cns.GetValue(Nothing)
                        Exit Sub
                    End If
                End If
            Next cns
            Throw New InvalidEnumArgumentException(String.Format(ResourcesT.Exceptions.CannotBeInterpretedAsMemberOf1, TypeCode, "ImageTypeContents"))
        End Sub
        ''' <summary>Contains value of the <see cref="Type"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Type As ImageTypeContents
        ''' <summary>Contains value of the <see cref="Component"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Components As ImageTypeComponents
        ''' <summary>Type of components</summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="ImageTypeContents"/></exception>
        <LDescription(GetType(IPTCResources), "Type_d")> _
        Public Property Type() As ImageTypeContents Implements IMediaType(Of ImageTypeComponents, ImageTypeContents).Code
            Get
                Return _Type
            End Get
            Set(ByVal value As ImageTypeContents)
                If Not InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, GetType(ImageTypeContents))
                _Type = value
            End Set
        End Property
        ''' <summary>Number of components</summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="iptc.ImageTypeComponents"/></exception>
        <LDescription(GetType(IPTCResources), "Components_d")> _
        Public Property Components() As Iptc.ImageTypeComponents Implements IMediaType(Of Iptc.ImageTypeComponents, ImageTypeContents).Count
            Get
                Return _Components
            End Get
            Set(ByVal value As Iptc.ImageTypeComponents)
                If Not InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, GetType(iptc.ImageTypeComponents))
                _Components = value
            End Set
        End Property
        ''' <summary>Gets or sets <see cref="Type"/> as <see cref="String"/></summary>
        ''' <exception cref="ArgumentException">Value being set cannot be interpreted member of <see cref="ImageTypeContents"/></exception>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property TypeCode() As Char Implements IMediaType(Of Iptc.ImageTypeComponents, ImageTypeContents).CodeString
            Get
                Return DirectCast(GetConstant(Type).GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)(0), Xml.Serialization.XmlEnumAttribute).Name
            End Get
            Set(ByVal value As Char)
                For Each Constant As Reflection.FieldInfo In GetType(ImageTypeContents).GetFields()
                    Dim Attrs As Object() = Constant.GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)
                    If (Attrs IsNot Nothing AndAlso Attrs.Length > 0 AndAlso DirectCast(Attrs(0), Xml.Serialization.XmlEnumAttribute).Name = value) OrElse ((Attrs Is Nothing OrElse Attrs.Length = 0) AndAlso Constant.Name = value) Then
                        Type = Constant.GetValue(Nothing)
                        Return
                    End If
                Next Constant
                Throw New ArgumentException(String.Format(ResourcesT.Exceptions.CannotBeInterpretedAs1, value, "ImageTypeContents"))
            End Set
        End Property
        ''' <summary>String representation in form 0T (components, type)</summary>
        Public Overrides Function ToString() As String
            Return String.Format(InvariantCulture, "{0}{1}", CByte(Components), TypeCode)
        End Function
        ''' <summary><see cref="System.ComponentModel.TypeConverter"/> for <see cref="iptcImageType"/></summary>
        Public Class Converter
            Inherits ExpandableObjectConverter(Of IptcImageType, String)
            ''' <summary>Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)"/> to create a new value, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>True if <see cref="PropertyDescriptor.PropertyType"/> of <see cref="ITypeDescriptorContext"/> of <paramref name="context"/> is not null and is subclass of <see cref="NumStr"/> (not <see cref="NumStr"/> itself)</returns>
            Public Overrides Function GetCreateInstanceSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                Return True
            End Function
            ''' <summary>Creates an instance of the type that this <see cref="System.ComponentModel.TypeConverter"/> is associated with, using the specified context, given a set of property values for the object.</summary>
            ''' <param name="propertyValues">An <see cref="System.Collections.IDictionary"/> of new property values.</param>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>Instance of <see cref="iptcAudioType"/> initialized by given property values</returns>
            Public Overrides Function CreateInstance(ByVal propertyValues As System.Collections.IDictionary, ByVal context As System.ComponentModel.ITypeDescriptorContext) As IptcImageType
                Dim ret As IptcImageType
                ret.Components = propertyValues!Components
                ret.Type = propertyValues!Type
                Return ret
            End Function
            ''' <summary>Performs conversion from <see cref="String"/> to <see cref="iptcImageType"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to type <see cref="iptcAudioType"/></param>
            ''' <returns>Value of <see cref="iptcAudioType"/> initialized by <paramref name="value"/></returns>
            ''' <exception cref="ArgumentException">Length of <paramref name="value"/> differs from 2</exception>
            ''' <exception cref="InvalidCastException">Second character cannot be interpreted as number</exception>
            ''' <exception cref="InvalidEnumArgumentException">First character cannot be interpreted as <see cref="ImageTypeContents"/> or second character cannot be interpreted as <see cref="ImageTypeComponents"/></exception>
            Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As String) As IptcImageType
                If value.Length <> 2 Then Throw New ArgumentException("Length of value must be 2")
                Return New IptcImageType(CStr(value(0)), CStr(value(1)))
            End Function
            ''' <summary>Performs conversion from type <see cref="iptcImageType"/> to type <see cref="String"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in type <see cref="String"/></returns>
            Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As IptcImageType) As String
                Return value.ToString
            End Function
        End Class
    End Structure

    ''' <summary>IPTC audio type (IPTC type <see cref="IPTCTypes.AudioType"/>)</summary>
    <TypeConverter(GetType(IptcAudioType.Converter))> _
    <DebuggerDisplay("{ToString}")> _
    Public Structure IptcAudioType : Implements IMediaType(Of Byte, AudioDataType)
        ''' <summary>CTor</summary>
        ''' <param name="Components">Number of components</param>
        ''' <param name="TypeCode">
        ''' <see cref="Type"></see> as <see cref="Char"></see></param>
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' <paramref name="Components"></paramref> is not within range 0÷9</exception>
        ''' <exception cref="ArgumentException">Cannot be interpreted as <see cref="AudioDataType"></see></exception>
        Public Sub New(ByVal Components As Byte, ByVal TypeCode As Char)
            Me.Components = Components
            Me.TypeCode = TypeCode
        End Sub
        ''' <summary>Contains value of the <see cref="Type"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Type As AudioDataType
        ''' <summary>Contains value of the <see cref="Component"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Components As Byte
        ''' <summary>Type of components</summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="ImageTypeContents"/></exception>
        <LDescription(GetType(IPTCResources), "Type_d")> _
        Public Property Type() As AudioDataType Implements IMediaType(Of Byte, AudioDataType).Code
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
        <LDescription(GetType(IPTCResources), "Components_d")> _
        Public Property Components() As Byte Implements IMediaType(Of Byte, AudioDataType).Count
            Get
                Return _Components
            End Get
            Set(ByVal value As Byte)
                If Not value >= 0 AndAlso value <= 9 Then Throw New ArgumentOutOfRangeException("value", String.Format(ResourcesT.Exceptions.NumberOfComponentsOf0MustBeFrom1To2, "AudioType", 0, 9))
                _Components = value
            End Set
        End Property
        ''' <summary>Gets or sets <see cref="Type"/> as <see cref="String"/></summary>
        ''' <exception cref="ArgumentException">Value being set cannot be interpreted member of <see cref="AudioDatatype"/></exception>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property TypeCode() As Char Implements IMediaType(Of Byte, AudioDataType).CodeString
            Get
                Return DirectCast(GetConstant(Type).GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)(0), Xml.Serialization.XmlEnumAttribute).Name
            End Get
            Set(ByVal value As Char)
                For Each Constant As Reflection.FieldInfo In GetType(AudioDataType).GetFields()
                    Dim Attrs As Object() = Constant.GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)
                    If (Attrs IsNot Nothing AndAlso Attrs.Length > 0 AndAlso DirectCast(Attrs(0), Xml.Serialization.XmlEnumAttribute).Name = value) OrElse ((Attrs Is Nothing OrElse Attrs.Length = 0) AndAlso Constant.Name = value) Then
                        Type = Constant.GetValue(Nothing)
                        Return
                    End If
                Next Constant
                Throw New ArgumentException(String.Format(ResourcesT.Exceptions.CannotBeInterpretedAs1, value, "AudioDataType"))
            End Set
        End Property
        ''' <summary>String representation in form 0T (components, type)</summary>
        Public Overrides Function ToString() As String
            Return String.Format(InvariantCulture, "{0}{1}", Components, TypeCode)
        End Function
        ''' <summary><see cref="System.ComponentModel.TypeConverter"/> for <see cref="iptcAudioType"/></summary>
        Public Class Converter
            Inherits ExpandableObjectConverter(Of IptcAudioType, String)
            ''' <summary>Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)"/> to create a new value, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>True if <see cref="PropertyDescriptor.PropertyType"/> of <see cref="ITypeDescriptorContext"/> of <paramref name="context"/> is not null and is subclass of <see cref="NumStr"/> (not <see cref="NumStr"/> itself)</returns>
            Public Overrides Function GetCreateInstanceSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                Return True
            End Function
            ''' <summary>Creates an instance of the type that this <see cref="System.ComponentModel.TypeConverter"/> is associated with, using the specified context, given a set of property values for the object.</summary>
            ''' <param name="propertyValues">An <see cref="System.Collections.IDictionary"/> of new property values.</param>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>Instance of <see cref="iptcAudioType"/> initialized by given property values</returns>
            Public Overrides Function CreateInstance(ByVal propertyValues As System.Collections.IDictionary, ByVal context As System.ComponentModel.ITypeDescriptorContext) As IptcAudioType
                Dim ret As IptcAudioType
                ret.Components = propertyValues!Components
                ret.Type = propertyValues!Type
                Return ret
            End Function
            ''' <summary>Performs conversion from <see cref="String"/> to <see cref="iptcaudioType"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to type <see cref="iptcAudioType"/></param>
            ''' <returns>Value of <see cref="iptcAudioType"/> initialized by <paramref name="value"/></returns>
            ''' <exception cref="ArgumentException">First character be interpreted as <see cref="AudioDataType"/> -or- length of <paramref name="value"/> differs from 2</exception>
            ''' <exception cref="InvalidCastException">Second character cannot be interpreted as number</exception>
            Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As String) As IptcAudioType
                If value.Length <> 2 Then Throw New ArgumentException("Length of value must be 2")
                Return New IptcAudioType(CStr(value(0)), CStr(value(1)))
            End Function
            ''' <summary>Performs conversion from type <see cref="iptcAudioType"/> to type <see cref="String"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in type <see cref="String"/></returns>
            Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As IptcAudioType) As String
                Return value.ToString
            End Function
        End Class
    End Structure

    ''' <summary>Common base for all <see cref="StringEnum(Of TEnum)"/>s</summary>
    <DebuggerDisplay("{StringValue}")> _
    Public MustInherit Class StringEnum
        Implements IT1orT2(Of Decimal, String)
        ''' <summary>CTor</summary>
        ''' <remarks>Nobody else can inherit this class</remarks>
        Friend Sub New()
        End Sub
        ''' <summary>String representation</summary>
        ''' <returns><see cref="StringValue"/></returns>
        Public Overrides Function ToString() As String
            Return StringValue
        End Function
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
        ''' <exception cref="InvalidEnumArgumentException">Value being set cannot be represented in underlying enumeration and underlying enumeration is restricted (has no <see cref="RestrictAttribute"/> or <see cref="RestrictAttribute.Restrict"/> is True)</exception>
        Public MustOverride Property StringValue() As String Implements DataStructuresT.GenericT.IPair(Of Decimal, String).Value2, DataStructuresT.GenericT.IT1orT2(Of Decimal, String).value2
        ''' <summary>Gets type of enumeration derived class contains</summary>
        Public MustOverride ReadOnly Property EnumType() As Type
        ''' <summary>Gets value indicating if this instance contains value of specified type</summary>
        ''' <returns>True oif derived class's <see cref="contains"/> returns true for <typeparamref name="T"/> or if <typeparamref name="T"/> is <see cref="Decimal"/> and derived class's <see cref="contains"/> returns true for <see cref="EnumType"/></returns>
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

        ''' <summary><see cref="System.ComponentModel.TypeConverter"/> for <see cref="StringEnum(Of TEnum)"/>'s</summary>
        ''' <version version="1.5.4">Added <see cref="EditorBrowsableAttribute"/></version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Class Converter
            Inherits TypeConverter(Of StringEnum, String)
            ''' <summary>Performs conversion from <see cref="String"/> to <see cref="T:StringEnum"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to <see cref="T:StringEnum"/></param>
            ''' <returns><see cref="T:StringEnum"/> initialized by <paramref name="value"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="context"/> is null</exception>
            ''' <exception cref="System.MissingMethodException">Cannot create an instance of generic class <see cref="StringEnum(Of TEnum)"/>. The constructor is missing.</exception>
            ''' <exception cref="System.MemberAccessException">Cannot create an instance of generic class <see cref="StringEnum(Of TEnum)"/>. E.g. the class is abstract.</exception>
            ''' <exception cref="System.Reflection.TargetInvocationException">Constructor of <see cref="StringEnum(Of TEnum)"/> has thrown an exception.</exception>
            Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As String) As StringEnum
                If context Is Nothing Then Throw New ArgumentNullException("context")
                Return Activator.CreateInstance(context.PropertyDescriptor.PropertyType, value)
            End Function
            ''' <summary>Performs conversion from <see cref="T:StringEnum"/> to <see cref="String"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in <see cref="String"/></returns>
            ''' <remarks>Calls <see cref="M:StringEnum.StringValue"/></remarks>
            Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As StringEnum) As String
                Return value.StringValue
            End Function
            ''' <summary>Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>True when <paramref name="context"/>'s <see cref="ITypeDescriptorContext.PropertyDescriptor"/>'s <see cref="PropertyDescriptor.PropertyType"/> is <see cref="StringEnum(Of TEnum)"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="context"/> is null</exception>
            Public Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                If context Is Nothing Then Throw New ArgumentNullException("context")
                Return context.PropertyDescriptor.PropertyType.IsGenericType AndAlso GetType(StringEnum(Of )).Equals(context.PropertyDescriptor.PropertyType.GetGenericTypeDefinition)
            End Function
            ''' <summary>Returns whether the collection of standard values returned from <see cref="GetStandardValues"/> is an exclusive list of possible values, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>True when underlying enumeration of <paramref name="context"/>'s <see cref="ITypeDescriptorContext.PropertyDescriptor"/>'s <see cref="PropertyDescriptor.PropertyType"/> has no <see cref="RestrictAttribute"/> or its <see cref="RestrictAttribute"/> has <see cref="RestrictAttribute.Restrict"/> True</returns>
            ''' <exception cref="ArgumentNullException"><paramref name="context"/> is null</exception>
            Public Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                If context Is Nothing Then Throw New ArgumentNullException("context")
                Dim Attrs As Object() = context.PropertyDescriptor.PropertyType.GetGenericArguments(0).GetCustomAttributes(GetType(RestrictAttribute), False)
                Return Attrs Is Nothing OrElse Attrs.Length = 0 OrElse DirectCast(Attrs(0), RestrictAttribute).Restrict
            End Function
            ''' <summary>Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter should not be null.</param>
            ''' <returns>A <see cref="System.ComponentModel.TypeConverter.StandardValuesCollection"/> that holds a standard set of valid values obtained from underlying enumeration of <paramref name="context"/>'s <see cref="ITypeDescriptorContext.PropertyDescriptor"/>'s <see cref="PropertyDescriptor.PropertyType"/> as values of <see cref="Xml.Serialization.XmlEnumAttribute"/> (preffred) or names of items</returns>
            ''' <exception cref="ArgumentNullException"><paramref name="context"/> is null</exception>
            Public Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
                If context Is Nothing Then Throw New ArgumentNullException("context")
                Dim ret As New List(Of StringEnum)
                For Each field As Reflection.FieldInfo In context.PropertyDescriptor.PropertyType.GetGenericArguments(0).GetFields(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static Or Reflection.BindingFlags.GetField)
                    If field.IsLiteral Then
                        ret.Add(Activator.CreateInstance(context.PropertyDescriptor.PropertyType, field.GetValue(Nothing)))
                    End If
                Next field
                Return New StandardValuesCollection(ret)
            End Function
        End Class

        ''' <summary>Custom type description provider for <see cref="StringEnum(Of T)"/> generic classes</summary>
        ''' <version version="1.5.4">This class is new in version 1.5.4</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Class StringEnumTypeDescriptionProvider
            Inherits TypeDescriptionProvider
            ''' <summary>Gets a custom type descriptor for the given type and object.</summary>
            ''' <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
            ''' <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
            ''' <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
            ''' <remarks>If <paramref name="objectType"/> or <paramref name="instance"/> is <see cref="StringEnum(Of T)"/> returns <see cref="T: MetadataT.IptcT.IptcDataTypes.StringEnum`1+StringEnumTypeDescriptor"/>.</remarks>
            Public Overrides Function GetTypeDescriptor(objectType As System.Type, instance As Object) As System.ComponentModel.ICustomTypeDescriptor
                Dim t = If(instance Is Nothing, objectType, instance.GetType())
                Do
                    If t.IsGenericType AndAlso t.GetGenericTypeDefinition.Equals(GetType(StringEnum(Of ))) Then _
                        Return Activator.CreateInstance(GetType(StringEnum(Of ).StringEnumTypeDescriptor).MakeGenericType({t.GetGenericArguments()(0)}))
                    t = t.BaseType
                Loop
                Return MyBase.GetTypeDescriptor(objectType, instance)
            End Function
        End Class
    End Class

    ''' <summary>Type that can contain value of "string enum" even when such value is not member of this enum</summary>
    ''' <typeparam name="TEnum">Type of <see cref="P:StringEnum`0.EnumValue"/>. Must inherit from <see cref="[Enum]"/></typeparam>
    ''' <version version="1.5.4">Added <see cref="TypeDescriptionProviderAttribute"/>. This makes this class WPF binding-friendly.</version>
    <CLSCompliant(False), DebuggerDisplay("{ToString}")>
    <TypeConverter(GetType(StringEnum.Converter))>
    <TypeDescriptionProvider(GetType(StringEnum.StringEnumTypeDescriptionProvider))>
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
                If Not InEnum(value) Then Throw New InvalidEnumArgumentException(String.Format(ResourcesT.Exceptions.MustBeMemberOfEnumeration1, EnumValue, GetType(TEnum).Name))
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
        ''' <exception cref="InvalidEnumArgumentException">Value being set cannot be represented in <see cref="TEnum"/> and <see cref="TEnum"/> is restricted (has no <see cref="RestrictAttribute"/> or <see cref="RestrictAttribute.Restrict"/> is True)</exception>
        Public Overrides Property StringValue() As String Implements DataStructuresT.GenericT.IPair(Of TEnum, String).Value2, IT1orT2(Of TEnum, String).value2
            Get
                If ContainsEnum Then
                    Dim Constant As Reflection.FieldInfo = GetConstant(Me.EnumValue)
                    Dim attr As Object() = Constant.GetCustomAttributes(GetType(Xml.Serialization.XmlEnumAttribute), False)
                    If attr IsNot Nothing AndAlso attr.Length > 0 Then
                        Return DirectCast(attr(0), Xml.Serialization.XmlEnumAttribute).Name
                    Else
                        Return Constant.Name
                    End If
                Else : Return _StringValue
                End If
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then Throw New ArgumentNullException("value", String.Format(ResourcesT.Exceptions.CannotBeSetToNull, "StringValue"))
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
                Dim attrs2 As Object() = GetType(TEnum).GetCustomAttributes(GetType(RestrictAttribute), False)
                If attrs2 Is Nothing OrElse attrs2.Length = 0 OrElse DirectCast(attrs2(0), RestrictAttribute).Restrict Then Throw New InvalidEnumArgumentException(String.Format(ResourcesT.Exceptions.CannotBeConvertedTo1, value, GetType(TEnum).Name))
                For Each ch As Char In value
                    If AscW(ch) < AscW(" ") OrElse AscW(ch) > 127 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.CanContainOnlyASCIIEncodableGraphicCharactersAndSpaces, "StringValue"))
                Next ch
                _EnumValue = CObj(0)
                _StringValue = value
                ContainsEnum = False
            End Set
        End Property

        ''' <summary>Identifies whether this instance contains value of specified type</summary>
        ''' <param name="T">Type to be contained</param>
        ''' <returns>True if this instance contais value of type <typeparamref name="T"/> otherwise False</returns>
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
                If value <> ContainsEnum Then Throw New NotSupportedException(String.Format(ResourcesT.Exceptions.CannotBeChanged, "StringEnum.ContainsEnum"))
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
                If value <> True Then Throw New NotSupportedException(String.Format(ResourcesT.Exceptions.CannotBeSetToFalse, "StringEnum.ContainsString"))
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
                    Throw New ArgumentException(ResourcesT.Exceptions.ValueOfIncompatibleTypeIsBeingSet)
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
            If From Is Nothing Then Return Nothing
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
        ''' <summary>CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor from string value</summary>
        ''' <param name="StringValue">String value top initialize new instance</param>
        ''' <exception cref="ArgumentNullException"><paramref name="StringValue"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="StringValue"/> contains unallowed character (non-grapic-non-space-non-ASCII)</exception>
        Public Sub New(ByVal StringValue As String)
            Me.StringValue = StringValue
        End Sub
        Public Sub New(ByVal EnumValue As TEnum)
            Me.EnumValue = EnumValue
        End Sub
        ''' <summary>Extends <see cref="T:StringEnum.Converter"/> so that it works even if it is given with null <see cref="ITypeDescriptorContext"/>. Supplies very simple fake own <see cref="ITypeDescriptorContext"/>.</summary>
        ''' <version version="1.5.4">Added <see cref="EditorBrowsableAttribute"/></version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shadows Class Converter
            Inherits StringEnum.Converter
            ''' <summary>Gets substitution <see cref="ITypeDescriptorContext"/></summary>
            Private Shared Function GetPropertyDescriptor() As EnumPropertyDescriptor
                Return New EnumPropertyDescriptor
            End Function
            ''' <summary>Very simple, context-less, read-only implementation of <see cref="PropertyDescriptor"/> and <see cref="ITypeDescriptorContext"/></summary>
            ''' <remarks>This class simply describes type <see cref="StringEnum(Of T)"/>[<typeparamref name="TEnum"/>]</remarks>
            Private NotInheritable Class EnumPropertyDescriptor
                Inherits PropertyDescriptor
                Implements System.ComponentModel.ITypeDescriptorContext
                ''' <summary>CTor</summary>
                Public Sub New()
                    MyBase.new("property", New Attribute() {})
                End Sub
#Region "ITypeDescriptorContext"
                ''' <summary>Gets the container representing this <see cref="T:System.ComponentModel.TypeDescriptor" /> request.</summary>
                ''' <returns>Null</returns>
                Private ReadOnly Property Container() As System.ComponentModel.IContainer Implements System.ComponentModel.ITypeDescriptorContext.Container
                    Get
                        Return Nothing
                    End Get
                End Property

                ''' <summary>Gets the object that is connected with this type descriptor request.</summary>
                ''' <returns>Null</returns>
                Private ReadOnly Property Instance() As Object Implements System.ComponentModel.ITypeDescriptorContext.Instance
                    Get
                        Return Nothing
                    End Get
                End Property

                ''' <summary>Does nothing</summary>
                Private Sub OnComponentChanged() Implements System.ComponentModel.ITypeDescriptorContext.OnComponentChanged
                End Sub

                ''' <summary>Does nothing</summary>
                ''' <returns>True</returns>
                Private Function OnComponentChanging() As Boolean Implements System.ComponentModel.ITypeDescriptorContext.OnComponentChanging
                    Return True
                End Function

                ''' <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is associated with the given context item.</summary>
                ''' <returns>This instance</returns>
                Public ReadOnly Property PropertyDescriptor() As System.ComponentModel.PropertyDescriptor Implements System.ComponentModel.ITypeDescriptorContext.PropertyDescriptor
                    Get
                        Return Me
                    End Get
                End Property

                ''' <summary>Gets the service object of the specified type.</summary>
                ''' <returns>null</returns>
                ''' <param name="serviceType">Ignored.</param>
                ''' <filterpriority>2</filterpriority>
                Private Function GetService(ByVal serviceType As System.Type) As Object Implements System.IServiceProvider.GetService
                    Return Nothing
                End Function
#End Region
#Region "PropertyDescriptor"
                ''' <summary>Returns whether resetting an object changes its value.</summary>
                ''' <returns>false</returns>
                ''' <param name="component">Ignored.</param>
                Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
                    Return False
                End Function

                ''' <summary>Gets the type of the component this property is bound to.</summary>
                ''' <returns>The <see cref="Object"/> type</returns>
                Public Overrides ReadOnly Property ComponentType() As System.Type
                    Get
                        Return GetType(Object)
                    End Get
                End Property

                ''' <summary>Gets the current value of the property on a component.</summary>
                ''' <returns>Default value of type <see cref="StringEnum(Of T)"/>[<typeparamref name="TEnum"/>]</returns>
                ''' <param name="component">Ignored</param>
                Public Overrides Function GetValue(ByVal component As Object) As Object
                    Return Activator.CreateInstance(GetType(StringEnum(Of TEnum)), New Object() {})
                End Function

                ''' <summary>Gets a value indicating whether this property is read-only.</summary>
                ''' <returns>true</returns>
                Public Overrides ReadOnly Property IsReadOnly() As Boolean
                    Get
                        Return True
                    End Get
                End Property

                ''' <summary>Gets the type of the property.</summary>
                ''' <returns>Type <see cref="StringEnum(Of T)"/>[<typeparamref name="TEnum"/>]</returns>
                Public Overrides ReadOnly Property PropertyType() As System.Type
                    Get
                        Return GetType(StringEnum(Of TEnum))
                    End Get
                End Property

                ''' <summary>Not supported</summary>
                ''' <param name="component">Ignored</param>
                ''' <exception cref="NotSupportedException">Always</exception>
                Public Overrides Sub ResetValue(ByVal component As Object)
                    Throw New NotSupportedException(ResourcesT.Exceptions.PropertyIsReadOnly)
                End Sub

                ''' <summary>Not supported</summary>
                ''' <param name="component">Ignored</param>
                ''' <param name="value">Ignored</param>
                ''' <exception cref="NotSupportedException">Always</exception>
                Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
                    Throw New NotSupportedException(ResourcesT.Exceptions.PropertyIsReadOnly)
                End Sub

                ''' <summary>Determines a value indicating whether the value of this property needs to be persisted.</summary>
                ''' <returns>true</returns>
                ''' <param name="component">Ignored</param>
                Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
                    Return True
                End Function
#End Region
            End Class
#Region "overrides"
            ''' <summary>Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter should not be null.</param>
            ''' <returns>A <see cref="System.ComponentModel.TypeConverter.StandardValuesCollection"/> that holds a standard set of valid values obtained from underlying enumeration of <paramref name="context"/>'s <see cref="ITypeDescriptorContext.PropertyDescriptor"/>'s <see cref="PropertyDescriptor.PropertyType"/> as values of <see cref="Xml.Serialization.XmlEnumAttribute"/> (preffred) or names of items</returns>
            ''' <remarks>Unlike <see cref="M:StringEnum.Converter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)"/>, this method works even if <paramref name="context"/> is null</remarks>
            Public Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
                If context Is Nothing Then context = GetPropertyDescriptor()
                Return MyBase.GetStandardValues(context)
            End Function
            ''' <summary>Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>True when <paramref name="context"/>'s <see cref="ITypeDescriptorContext.PropertyDescriptor"/>'s <see cref="PropertyDescriptor.PropertyType"/> is <see cref="StringEnum(Of TEnum)"/></returns>
            ''' <remarks>Unlike <see cref="M:StringEnum.Converter.GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext)"/>, this method works even if <paramref name="context"/> is null</remarks>
            Public Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                If context Is Nothing Then context = GetPropertyDescriptor()
                Return MyBase.GetStandardValuesSupported(context)
            End Function
            ''' <summary>Returns whether the collection of standard values returned from <see cref="GetStandardValues"/> is an exclusive list of possible values, using the specified context.</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>True when underlying enumeration of <paramref name="context"/>'s <see cref="ITypeDescriptorContext.PropertyDescriptor"/>'s <see cref="PropertyDescriptor.PropertyType"/> has no <see cref="RestrictAttribute"/> or its <see cref="RestrictAttribute"/> has <see cref="RestrictAttribute.Restrict"/> True</returns>
            ''' <remarks>Unlike <see cref="M:StringEnum.Converter.GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext)"/>, this method works even if <paramref name="context"/> is null</remarks>
            Public Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                If context Is Nothing Then context = GetPropertyDescriptor()
                Return MyBase.GetStandardValuesExclusive(context)
            End Function
            ''' <summary>Performs conversion from <see cref="String"/> to <see cref="T:StringEnum(Of T)"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to <see cref="T:StringEnum"/></param>
            ''' <returns><see cref="T:StringEnum(Of T)"/> initialized by <paramref name="value"/></returns>
            ''' <remarks>Unlike <see cref="M:StringEnum.Converter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.String)"/>, this method works even if <paramref name="context"/> or <paramref name="context"/>.<see cref="ITypeDescriptorContext.PropertyDescriptor">PropertyDescriptor</see> is null because it ignores <paramref name="context"/> and directly creates a new instance of type <see cref="StringEnum(Of T)"/>.</remarks>
            ''' <version version="1.5.4">CHanged implementation. The method now directly creates a new instance of <see cref="StringEnum(Of T)"/>. Previously it called base class method and just ensured that <paramref name="context"/> was not null.</version>
            Public Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As String) As StringEnum
                Return New StringEnum(Of TEnum)(value)
            End Function
            ''' <summary>If overridden in derived class performs conversion form null to type <see cref="T:StringEnum"/></summary>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <returns>Null value converted to type <see cref="T:StringEnum"/></returns>
            Public Overrides Function ConvertFromNull(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo) As StringEnum
                If context Is Nothing Then context = GetPropertyDescriptor()
                Return MyBase.ConvertFromNull(context, culture)
            End Function
            ''' <summary>Performs conversion from <see cref="T:StringEnum"/> to <see cref="String"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in <see cref="String"/></returns>
            ''' <remarks>Calls <see cref="M:StringEnum.StringValue"/></remarks>
            Public Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As StringEnum) As String
                If context Is Nothing Then context = GetPropertyDescriptor()
                Return MyBase.ConvertTo(context, culture, value)
            End Function
            ''' <summary>Returns whether changing a value on this object requires a call to the <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> method to create a new value.</summary>
            ''' <returns>true if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, false.</returns>
            Public Overrides Function GetCreateInstanceSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                If context Is Nothing Then context = GetPropertyDescriptor()
                Return MyBase.GetCreateInstanceSupported(context)
            End Function
            ''' <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
            ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
            ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
            ''' <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
            ''' <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
            Public Overrides Function GetProperties(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object, ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
                If context Is Nothing Then context = GetPropertyDescriptor()
                Return MyBase.GetProperties(context, value, attributes)
            End Function
            ''' <summary>Returns whether this object supports properties.</summary>
            ''' <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, false.</returns>
            Public Overrides Function GetPropertiesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                If context Is Nothing Then context = GetPropertyDescriptor()
                Return MyBase.GetPropertiesSupported(context)
            End Function
            ''' <summary>Returns whether the given instance of <see cref="String"/> is valid for type <see cref="T:StringEnum"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="value">Value to test validity</param>
            ''' <returns>true if the specified value is valid for this type <see cref="T:StringEnum"/>; otherwise, false.</returns>
            ''' <remarks>If not overridden in derived class thi method calls <see cref="ConvertFrom"/> and checks if it throws an exception or not.</remarks>
            Public Overrides Function IsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As String) As Boolean
                If context Is Nothing Then context = GetPropertyDescriptor()
                Return MyBase.IsValid(context, value)
            End Function
            ''' <summary>Re-creates an <see cref="System.Object"/> given a set of property values for the object.</summary>
            ''' <param name="propertyValues">An <see cref="System.Collections.IDictionary"/> that represents a dictionary of new property values.</param>
            ''' <returns>An <see cref="System.Object"/> representing the given <see cref="System.Collections.IDictionary"/>, or null if the object cannot be created. This method always returns null.</returns>
            Public Overrides Function CreateInstance(ByVal propertyValues As System.Collections.IDictionary, ByVal context As System.ComponentModel.ITypeDescriptorContext) As StringEnum
                If context Is Nothing Then context = GetPropertyDescriptor()
                Return MyBase.CreateInstance(propertyValues, context)
            End Function
#End Region
        End Class


        ''' <summary>Custom type descriptor for <see cref="StringEnum(Of T)"/>. It only provides converter.</summary>
        ''' <seelaso cref="StringEnumTypeDescriptionProvider"/>
        ''' <version version="1.5.4">This class is new in version 1.5.4</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Class StringEnumTypeDescriptor
            Inherits CustomTypeDescriptor
            ''' <summary>Returns a type converter for the type represented by this type descriptor.</summary>
            ''' <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the type represented by this type descriptor. 
            ''' This implementation returns a new instance of <see cref="Convert"/>.</returns>
            Public Overrides Function GetConverter() As System.ComponentModel.TypeConverter
                Return New Converter
            End Function
        End Class
    End Class

#Region "Record 3"

    ''' <summary>The picture number provides a universally unique reference to an image</summary>
    ''' <version version="1.5.4">This structure is new in version 1.5.4</version>
    <Serializable()>
    Public Structure IptcPictureNumber
        Private _octets As Byte()
        ''' <summary>Gets octets (bytes) that forms value of  <see cref="PictureNumber"/></summary>
        <Browsable(False)>
        Public ReadOnly Property Octets As Byte()
            Get
                If _octets Is Nothing Then ReDim _octets(0 To 15)
                Return _octets
            End Get
        End Property

        ''' <summary>CTor - creates a new instance of the <see cref="PictureNumber"/> structure from byte array</summary>
        ''' <param name="octets">Array of 16 bytes representing underlying value of this property</param>
        ''' <exception cref="ArgumentNullException"><paramref name="octets"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="octets"/> does not contain exactly 16 bytes</exception>
        Public Sub New(octets As Byte())
            If octets Is Nothing Then Throw New ArgumentNullException("octets")
            If octets.Length <> 16 Then Throw New ArgumentException(ResourcesT.Exceptions.ArrayMustHaveExactlyXBytes.f(16), "octets")
            _octets = octets
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="PictureNumber"/> structure from property values</summary>
        ''' <param name="manufacturer">Identifies manufacturer</param>
        ''' <param name="equipment">Identifies equipment (manufacturer-specific)</param>
        ''' <param name="dateIdentifier">Indicates year, month and day the picture number was generated</param>
        ''' <param name="numericIdentifier">Number generated for each picture by the same manufacturer and equipment in given day</param>
        Public Sub New(manufacturer As ManufacturersIdentificationNumber, equipment As Short, dateIdentifier As Date, numericIdentifier As Short)
            Me.Manufacturer = manufacturer
            Me.Equipment = equipment
            Me.DateIdentifier = dateIdentifier
            Me.NumericIdentifier = numericIdentifier
        End Sub

        ''' <summary>Gets or sets Manufacturer’s Unique Identity (issued by IPTC)</summary>
        <LDisplayName(GetType(IptcResources), "ManufacturersUniqueIdentity_n")>
        <LDescription(GetType(IptcResources), "ManufacturerdUniqueIdentity_d")>
        Public Property Manufacturer As ManufacturersIdentificationNumber
            Get
                Return (CShort(Octets(0)) << 8) Or CShort(Octets(1))
            End Get
            Set(value As ManufacturersIdentificationNumber)
                Octets(0) = (value And &HFF00S) >> 8
                Octets(1) = value And &HFFS
            End Set
        End Property

        ''' <summary>Gets or sets equipment identifier</summary>
        ''' <remarks>Used to indicate equipment type and managed by Manufacturer.</remarks>
        <LDisplayName(GetType(IptcResources), "Equipment_n")>
        <LDescription(GetType(IptcResources), "Equipment_d")>
        Public Property Equipment As Integer
            Get
                Return (CInt(Octets(2)) << 24) Or (CInt(Octets(3)) << 16) Or (CInt(Octets(4)) << 8) Or CInt(Octets(5))
            End Get
            Set(value As Integer)
                Octets(2) = (value And &HFF000000I) >> 24
                Octets(3) = (value And &HFF0000I) >> 16
                Octets(4) = (value And &HFF00I) >> 8
                Octets(5) = value And &HFFI
            End Set
        End Property

        ''' <summary>Gets or sets value indicating year, month and day the picture number was generated.</summary>
        ''' <remarks>Sub-day part of <see cref="Date"/> value is truncated.</remarks>
        ''' <exception cref="FormatException">Octets 6 to 13 does not contain date in yyyyMMdd format.</exception>
        <LDisplayName(GetType(IptcResources), "DateIdentifier_n")>
        <LDescription(GetType(IptcResources), "DateIdentifier_d")>
        Public Property DateIdentifier As DateTime
            Get
                Dim numericCharacters = System.Text.Encoding.ASCII.GetString(Octets, 6, 8)
                Return Date.ParseExact(numericCharacters, "yyyyMMdd", InvariantCulture)
            End Get
            Set(value As DateTime)
                Dim array = System.Text.Encoding.ASCII.GetBytes(value.ToString("yyyyMMdd"))
                For i As Integer = 0 To 7
                    Octets(i + 6) = array(i)
                Next
            End Set
        End Property

        ''' <summary>Gets or sets a binary number generated each time a picture number is created and being unique for the same device and for the date contained in this DataSet.</summary>
        ''' <remarks>When the originating device (scanner) is not able to generate a relevant picture number, each octet of the picture number should be set to value zero, i.e. a null value.</remarks>
        <LDisplayName(GetType(IptcResources), "NumericIdentifier_n")>
        <LDescription(GetType(IptcResources), "NumericIdentifier_d")>
        Public Property NumericIdentifier As Short
            Get
                Return (CShort(Octets(14)) << 8) Or CShort(Octets(15))
            End Get
            Set(value As Short)
                Octets(14) = (value And &HFF00S) >> 8
                Octets(15) = value And &HFFS
            End Set
        End Property

        ''' <summary>Gets string representation of this object</summary>
        ''' <returns>String representation of this object</returns>
        Public Overrides Function ToString() As String
            Return String.Format("{0} - {1} {2} {2}", Manufacturer, Equipment, DateIdentifier, NumericIdentifier)
        End Function
    End Structure

    ''' <summary>Numbers assigned by IPTC-NAA for manufacturers of image originating devices</summary>
    ''' <remarks>The following series of numbers have been assigned by IPTC-NAA for manufacturers of image originating devices.
    ''' <para>These numbers are used for manufacturer identification used in Device Identifier.</para></remarks>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum ManufacturersIdentificationNumber As Short
        ''' <summary>Associated Press, East Brunswick, NJ, USA</summary>
        <FieldDisplayName("Associated Press")>
        AssociatedPress = 1
        ''' <summary>Eastman Kodak Co, Rochester, NY, USA</summary>
        <FieldDisplayName("Eastman Kodak")>
        EastmanKodak = 2
        ''' <summary>Hasselblad Electronic Imaging, Göteborg, Sweden</summary>
        <FieldDisplayName("Hasselblad Electronic Imaging")>
        HaselbaldElectronicImaging = 3
        ''' <summary>Tecnavia SA, Agno, Switzerland</summary>
        <FieldDisplayName("Tecnavia")>
        Tecnavia = 4
        ''' <summary>Nikon Corporation, Tokyo, Japan</summary>
        <FieldDisplayName("Nikon")>
        Nikon = 5
        ''' <summary>Coatsworth Communications Inc. Canada</summary>
        <FieldDisplayName("Coatsworth Communications")>
        CoatsworthCommunications = 6
        ''' <summary>Agence France Presse, Paris, France</summary>
        <FieldDisplayName("Agence France Presse")>
        AgenceFrancePresse = 7
        ''' <summary>T/One Inc. c/o SEG, Cambridge, MA, USA</summary>
        <FieldDisplayName("T/One")>
        TOne = 8
        ''' <summary>Associated Newspapers, UK</summary>
        <FieldDisplayName("Associated Newspapers")>
        AssociatedNewspapers = 9
        ''' <summary>Reuters London</summary>
        <FieldDisplayName("Reuters")>
        Reuters = 10
        ''' <summary>Sandia Imaging Systems Inc, Carrollton, TX, USA</summary>
        <FieldDisplayName("Sandia Imaging Systems")>
        SandiaImagingSystems = 11
        ''' <summary>Deutsche Presse-Agentur GmbH, Hamburg, Germany</summary>
        <FieldDisplayName("Deutsche Presse-Agentur")>
        DeutschePresseAgentur = 12
        ''' <summary>Visualize, Madrid, Spain</summary>
        <FieldDisplayName("Visualize")>
        Visualize = 13
    End Enum
#End Region
End Namespace
