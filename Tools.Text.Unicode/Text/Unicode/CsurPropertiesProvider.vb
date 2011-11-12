Imports System.Xml.Serialization
Imports Tools.TextT.UnicodeT
Imports System.Globalization.CultureInfo
Imports Tools.ComponentModelT

Namespace TextT.UnicodeT
    ''' <summary>Provides ConScript Unicode Registry (CSUR) extension properties of a character</summary>
    ''' <remarks><note>XML serialization attributes used to decorate properties of this class are not intended for XML serialization, they are rather intended as machine-readable documentation where the property originates from in UCD XML.</note></remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class CsurPropertiesProvider
        ''' <summary>CTor - creates a new instance of the <see cref="CsurPropertiesProvider"/> class specifying a <see cref="UnicodePropertiesProvider"/> to be used to read CSUR data from</summary>
        ''' <param name="csurPropertySource">A <see cref="UnicodePropertiesProvider"/> to use to retireve CSUR data. <note>If instance passed here does not derive from <see cref="UnicodeCodePoint"/> the <see cref="Block"/> property will always return null!</note></param>
        ''' <param name="mainUcdDocument">A XML document that represents main Unicode Character Database XML (not the CSUR UCD XML). <note>You can pass null here, but then the <see cref="Block"/> property will always return null!</note></param>
        ''' <exception cref="ArgumentNullException"><paramref name="csurPropertySource"/> is null.</exception>
        ''' <remarks>
        ''' <para>You usually do not call this CTor directly. Typical way of obtaining <see cref="CsurPropertiesProvider"/> instances is through <see cref="CsurExtensions.Csur"/> extension function.</para>
        ''' <para>
        ''' You can pass either instance of <see cref="UnicodePropertiesProvider"/> that comes from CSUR-specific <see cref="UnicodeCharacterDatabase"/> (<see cref="CsurExtensions.Csur"/> does it). Use of such instance is faster.
        ''' Or you can also pass instance of <see cref="UnicodeCodePoint"/> (<see cref="UnicodeCodePoint.CodePoint"/> must not be null) that comes from a <see cref="UnicodeCharacterDatabase"/> with CSUR extension data loaded.
        ''' The later approach is slower. In case of later approach <paramref name="csurPropertySource"/> is not <see cref="UnicodeCodePoint"/> or <paramref name="csurPropertySource"/>.<see cref="UnicodeCodePoint.CodePoint">CodePoint</see> is null all CSUR properties return null (or default value).
        ''' </para>
        ''' </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Sub New(csurPropertySource As UnicodePropertiesProvider, mainUcdDocument As XDocument)
            If csurPropertySource Is Nothing Then Throw New ArgumentNullException("csurPropertySource")
            _propertySource = csurPropertySource
            Me._mainUcdDocument = mainUcdDocument
        End Sub

#Region "Helper properties"
        Private ReadOnly _mainUcdDocument As XDocument
        ''' <summary>Gets an XML document that defines main UCD XML (not CSUR XML)</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        <XmlIgnore(), Browsable(False)>
        Public ReadOnly Property MainUcdDocument As XDocument
            Get
                Return _mainUcdDocument
            End Get
        End Property

        Private ReadOnly _propertySource As UnicodePropertiesProvider
        ''' <summary>Gets instance of <see cref="UnicodePropertiesProvider"/> that is used to retrieve CSUR properties' values</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        <XmlIgnore(), Browsable(False)>
        Public ReadOnly Property PropertySource As UnicodePropertiesProvider
            Get
                Return _propertySource
            End Get
        End Property

        ''' <summary>Gets a XML document that provides values of CSUR properties</summary>
        <XmlIgnore(), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected ReadOnly Property CsurDocument As XDocument
            Get
                Dim csurExt = PropertySource.GetExtension(CsurExtensions.XmlNamespace)
                If csurExt IsNot Nothing Then Return csurExt.Xml
                Return PropertySource.Element.Document
            End Get
        End Property
#End Region

#Region "CSUR Properties"
#Region "XmlAttribute-backed"
        ''' <summary>Gets name of CSUR-registered script this character belongs to.</summary>
        ''' <remarks>This is human-readable name of the script. It's not ISO script code.</remarks>
        <XmlElement("script", namespace:=CsurExtensions.XmlNamespace)>
        <LDisplayName(GetType(UnicodeResources), "d_CSUR_Script"), LCategory(GetType(UnicodeResources), "propcat_CSUR_CSUR", "CSUR")>
        Public ReadOnly Property Script As String
            Get
                Return PropertySource.GetPropertyValue(CsurExtensions.XmlNamespace, "script")
            End Get
        End Property

        ''' <summary>Gets status of the code-point in CSUR</summary>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute is not one of <see cref="CsurStatus"/> values.</exception>
        ''' <remarks>If the character belongs to a script and block the status is usually shared across whole block and script.</remarks>
        <XmlElement("status", namespace:=CsurExtensions.XmlNamespace)>
        <LDisplayName(GetType(UnicodeResources), "d_CSUR_Status"), LCategory(GetType(UnicodeResources), "propcat_CSUR_CSUR", "CSUR")>
        Public ReadOnly Property Status As CsurStatus
            Get
                Dim val = PropertySource.GetPropertyValue(CsurExtensions.XmlNamespace, "status")
                If val = "" Then Return CsurStatus.NotInCsur
                Try
                    Return EnumCore.Parse(Of CsurStatus)(val)
                Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is OverflowException
                    Throw New InvalidOperationException(String.Format(TextT.UnicodeT.UnicodeResources.ex_ValueCannotBeParsed, val, GetType(CsurStatus).Name), ex)
                End Try
            End Get
        End Property

        ''' <summary>In case <see cref="Status"/> is <see cref="CsurStatus.Reserved"/> gets value indicating purpose for which the code point is reserved in CSUR</summary>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute is not one of <see cref="CsurReservedFor"/> values.</exception>
        ''' <remarks>This property retuns non-<see cref="CsurReservedFor.notReserved"/> values only for some code-points from BMP private-use area. Thos are codepoints not used by CSUR and given by CSUR for complete private use.</remarks>
        <XmlElement("reserved-for", namespace:=CsurExtensions.XmlNamespace)>
        <LDisplayName(GetType(UnicodeResources), "d_CSUR_ReservedFor"), LCategory(GetType(UnicodeResources), "propcat_CSUR_CSUR", "CSUR")>
        Public ReadOnly Property ReservedFor As CsurReservedFor
            Get
                Dim val = PropertySource.GetPropertyValue(CsurExtensions.XmlNamespace, "reserved-for")
                If val = "" Then Return CsurReservedFor.notReserved
                Try
                    Return EnumCore.Parse(Of CsurReservedFor)(val)
                Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is OverflowException
                    Throw New InvalidOperationException(String.Format(UnicodeResources.ex_ValueCannotBeParsed, val, GetType(CsurReservedFor).Name), ex)
                End Try
            End Get
        End Property

        ''' <summary>Gets date of last revision of script in CSUR</summary>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute cannot be parsed as date value in XML fomat (yyyy-MM-dd).</exception>
        ''' <remarks>Value of this property is typically shared acrosss whole CSUR script and block.</remarks>
        <XmlElement("last-revision", namespace:=CsurExtensions.XmlNamespace)>
        <LDisplayName(GetType(UnicodeResources), "d_CSUR_LastRevision"), LCategory(GetType(UnicodeResources), "propcat_CSUR_CSUR", "CSUR")>
        Public ReadOnly Property LastRevision As Date?
            Get
                Dim val = PropertySource.GetPropertyValue(CsurExtensions.XmlNamespace, "last-revision")
                If val = "" Then Return Nothing
                Try
                    Return DateTime.ParseExact(val, "yyyy-MM-dd", InvariantCulture)
                Catch ex As FormatException
                    Throw New InvalidOperationException(String.Format(UnicodeResources.ex_ValueCannotBeParsed, val, GetType(Date).Name), ex)
                End Try
            End Get
        End Property

        ''' <summary>Gets name of the code-point (character) assigned to it by CSUR</summary>
        ''' <remarks>
        ''' CSUR names are not provided for reserved code-points.
        ''' <para>Underlying XML attribute can use # placeholder as in <see cref="UnicodePropertiesProvider.Name"/>.</para>
        ''' </remarks>
        ''' <seelaso cref="UnicodePropertiesProvider.Name"/>
        <XmlElement("name", namespace:=CsurExtensions.XmlNamespace)>
        <LDisplayName(GetType(UnicodeResources), "d_CSUR_Name"), LCategory(GetType(UnicodeResources), "propcat_CSUR_CSUR", "CSUR")>
        Public ReadOnly Property Name As String
            Get
                Return PropertySource.GetPropertyValue(CsurExtensions.XmlNamespace, "name", True)
            End Get
        End Property

        ''' <summary>Gets BiDi category assigned by CSUR</summary>
        ''' <remarks>CSUR does not provide bidi categories in a normative way. Thus this property is provided only for some code-poits, especially those specifically said to be RtL.</remarks>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute is not one of <see cref="UnicodeBidiCategory"/> values.</exception>
        ''' <seelaso cref="UnicodePropertiesProvider.BidiCategory"/>
        <XmlElement("bc", namespace:=CsurExtensions.XmlNamespace)>
        <LDisplayName(GetType(UnicodeResources), "d_CSUR_BidiCategory"), LCategory(GetType(UnicodeResources), "propcat_CSUR_CSUR", "CSUR")>
        Public ReadOnly Property BidiCategory As UnicodeBidiCategory?
            Get
                Dim val = PropertySource.GetPropertyValue(CsurExtensions.XmlNamespace, "bc")
                If val = "" Then Return Nothing
                Try
                    Return EnumCore.Parse(Of UnicodeBidiCategory)(val)
                Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is OverflowException
                    Throw New InvalidOperationException(String.Format(UnicodeResources.ex_ValueCannotBeParsed, val, GetType(UnicodeBidiCategory).Name), ex)
                End Try
            End Get
        End Property
#End Region

        ''' <summary>Gets a CSUR block this code-point belongs to (in CSUR)</summary>
        ''' <returns>
        ''' A CSUR block code-point represented (pointed to) by this instance belongs to (if any).
        ''' Null if <see cref="PropertySource"/> is not <see cref="UnicodeCodePoint"/>; or <see cref="MainUcdDocument"/> is null; or
        ''' <see cref="PropertySource"/>.<see cref="UnicodeCodePoint.CodePoint"/> is null and either
        ''' either <see cref="PropertySource"/>.<see cref="UnicodeCodePoint.FirstCodePoint">FirstCodePoint</see> or <see cref="PropertySource"/>.<see cref="UnicodeCodePoint.LastCodePoint">LastCodePoint</see> is null,
        ''' or <see cref="PropertySource"/>.<see cref="UnicodeCodePoint.FirstCodePoint">FirstCodePoint</see> and <see cref="PropertySource"/>.<see cref="UnicodeCodePoint.LastCodePoint">LastCodePoint</see> belong to different blocks.
        ''' </returns>
        ''' <exception cref="InvalidOperationException">Underlying CSUR XML data are invalid. See <see cref="Exception.InnerException"/> for details. See <see cref="M:Tools.TextT.UnicodeT.UnicodeCharacterDatabase.FindBlock(System.Collections.Generic.IEnumerable`1[System.Xml.Linq.XElement],System.UInt32,System.Boolean)"/> for detailed explanation of <see cref="Exception.InnerException"/>.</exception>
        ''' <remarks>CSUR blocks can overlap existing Unicode blocks. CSUR blocks are CSUR-specific and are not the same as Unicode blocks. But they serve the same purpose CSUR as Unicode blocks in Unicode (UCD).</remarks>
        ''' <seelaso cref="UnicodeCodePoint.Block"/>
        ''' <seelaso cref="UnicodeCharacterDatabase.FindBlock"/>
        <XmlIgnore(), Browsable(False)>
        Public ReadOnly Property Block As CsurBlock
            Get
                If Not TypeOf PropertySource Is UnicodeCodePoint OrElse MainUcdDocument Is Nothing Then Return Nothing
                Dim codePoint As UnicodeCodePoint = PropertySource
                If codePoint.CodePoint.HasValue Then
                    Dim bel As XElement
                    Try
                        bel = UnicodeCharacterDatabase.FindBlock(CsurDocument.<ucd>.<blocks>.<block>, codePoint.CodePoint.Value, False)
                    Catch ex As Exception When TypeOf ex Is ArgumentNullException OrElse TypeOf ex Is FormatException OrElse TypeOf ex Is OverflowException
                        Throw New InvalidOperationException(ex.Message, ex)
                    End Try
                    If bel IsNot Nothing Then Return New CsurBlock(bel, MainUcdDocument)
                ElseIf codePoint.FirstCodePoint.HasValue AndAlso codePoint.LastCodePoint.HasValue Then
                    Dim bel1 As XElement
                    Dim bel2 As XElement
                    Try
                        bel1 = UnicodeCharacterDatabase.FindBlock(CsurDocument.<ucd>.<blocks>.<block>, codePoint.FirstCodePoint.Value, False)
                        bel2 = UnicodeCharacterDatabase.FindBlock(CsurDocument.<ucd>.<blocks>.<block>, codePoint.LastCodePoint.Value, False)
                    Catch ex As Exception When TypeOf ex Is ArgumentNullException OrElse TypeOf ex Is FormatException OrElse TypeOf ex Is OverflowException
                        Throw New InvalidOperationException(ex.Message, ex)
                    End Try
                    If bel1 Is bel2 Then Return New CsurBlock(bel1, MainUcdDocument)
                End If
                Return Nothing
            End Get
        End Property

        ''' <summary>Gets type of code-point in CSUR</summary>
        ''' <returns>Type of this code-point in CSUR. Null if it cannot be determined.</returns>
        ''' <remarks>
        ''' CSUR code-point can never be determined if <see cref="PropertySource"/> is not <see cref="UnicodeCodePoint"/>.
        ''' It also cannot be determined if <see cref="PropertySource"/> is <see cref="UnicodeCodePoint"/> that belongs to "normal" Unicode Character Database XML (rather than to ConScript Unicode Registry XML) and
        ''' either <see cref="PropertySource"/>.<see cref="UnicodeCodePoint.CodePoint">CodePoint</see> is null or it's not null but corresponding CSUR code point cannot be found.
        ''' </remarks>
        ''' <seelaso cref="UnicodeCodePoint.Type"/>
        <XmlIgnore()>
        <LDisplayName(GetType(UnicodeResources), "d_CSUR_Type"), LCategory(GetType(UnicodeResources), "propcat_CSUR_CSUR", "CSUR")>
        Public ReadOnly Property CsurCodePointType As UnicodeCodePointType?
            Get
                If Not TypeOf PropertySource Is UnicodeCodePoint Then Return Nothing
                Dim codePoint As UnicodeCodePoint = PropertySource
                Dim csurExt = PropertySource.GetExtension(CsurExtensions.XmlNamespace)
                If csurExt Is Nothing Then
                    Return codePoint.Type
                Else
                    If Not codePoint.CodePoint.HasValue Then Return Nothing
                    Dim csurCodePoint = csurExt.FindCodePoint(codePoint.CodePoint.Value)
                    If csurCodePoint Is Nothing Then Return Nothing
                    Return csurCodePoint.Type
                End If
            End Get
        End Property
#End Region

    End Class
End Namespace
