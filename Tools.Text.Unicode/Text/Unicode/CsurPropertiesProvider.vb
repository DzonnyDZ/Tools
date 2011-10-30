Imports System.Xml.Serialization

Namespace TextT.UnicodeT
    ''' <summary>Provides ConScript Unicode Registry (CSUR) extension properties of a character</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class CsurPropertiesProvider

        Private _propertySource As UnicodePropertiesProvider

        ''' <summary>CTor - creates a new instance of the <see cref="CsurPropertiesProvider"/> class specifying a <see cref="UnicodePropertiesProvider"/> to be used to read CSUR data from</summary>
        ''' <param name="csurPropertySource">A <see cref="UnicodePropertiesProvider"/> to use to retireve CSUR data</param>
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
        Public Sub New(csurPropertySource As UnicodePropertiesProvider)
            If csurPropertySource Is Nothing Then Throw New ArgumentNullException("csurPropertySource")
            _propertySource = csurPropertySource
        End Sub

        ''' <summary>Gets instance of <see cref="UnicodePropertiesProvider"/> that is used to retrieve CSUR properties' values</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property PropertySource As UnicodePropertiesProvider
            Get
                Return _propertySource
            End Get
        End Property

#Region "Properties"
        'TODO: Implement properties

        ''' <summary>Gets name of CSUR-registered script this character belongs to.</summary>
        ''' <remarks>This is human-readable name of the script. It's not ISO script code.</remarks>
        <XmlElement("script", namespace:=CsurExtensions.XmlNamespace)>
        Public ReadOnly Property Script As String
            Get

            End Get
        End Property

        ''' <summary>Gets status of the code-point in CSUR</summary>
        ''' <remarks>If the character belongs to a script and block the status is usually shared across whole block and script.</remarks>
        <XmlElement("status", namespace:=CsurExtensions.XmlNamespace)>
        Public ReadOnly Property Status As CsurStatus
            Get

            End Get
        End Property

        ''' <summary>In case <see cref="Status"/> is <see cref="CsurStatus.Reserved"/> gets value indicating purpose for which the code point is reserved in CSUR</summary>
        ''' <remarks>This property retuns non-<see cref="CsurReservedFor.notReserved"/> values only for some code-points from BMP private-use area. Thos are codepoints not used by CSUR and given by CSUR for complete private use.</remarks>
        <XmlElement("reserved-for", namespace:=CsurExtensions.XmlNamespace)>
        Public ReadOnly Property ReservedFor As CsurReservedFor
            Get

            End Get
        End Property

        ''' <summary>Gets date of last revision of script in CSUR</summary>
        ''' <remarks>Value of this property is typically shared acrosss whole CSUR script and block.</remarks>
        <XmlElement("last-revision", namespace:=CsurExtensions.XmlNamespace)>
        Public ReadOnly Property LastRevision As Date?
            Get

            End Get
        End Property

        ''' <summary>Gets name of the code-point (character) assigned to it by CSUR</summary>
        ''' <remarks>
        ''' CSUR names are not provided for reserved code-points.
        ''' <para>Underlying XML attribute can use # placeholder as in <see cref="UnicodePropertiesProvider.Name"/>.</para>
        ''' </remarks>
        <XmlElement("name", namespace:=CsurExtensions.XmlNamespace)>
        Public ReadOnly Property Name As String
            Get

            End Get
        End Property

        ''' <summary>Gets BiDi category assigned by CSUR</summary>
        ''' <remarks>CSUR does not provide bidi categories in a normative way. Thus this property is provided only for some code-poits, especially those specifically said to be RtL.</remarks>
        <XmlElement("bc", namespace:=CsurExtensions.XmlNamespace)>
        Public ReadOnly Property BidiCategory As UnicodeBidiCategory?
            Get

            End Get
        End Property

        ''' <summary>Gets a CSUR block this code-point belongs to (in CSUR)</summary>
        ''' <remarks>CSUR blocks can overlap existing Unicode blocks</remarks>
        Public ReadOnly Property Block As CsurBlock
            Get
            End Get
        End Property

        ''' <summary>Gets type of code-point in CSUR</summary>
        Public ReadOnly Property CsurCodePointType As UnicodeCodePointType
            Get

            End Get
        End Property
#End Region

    End Class
End Namespace
