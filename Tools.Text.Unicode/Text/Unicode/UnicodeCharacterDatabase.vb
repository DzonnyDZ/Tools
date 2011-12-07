Imports System.Xml.Linq
Imports Tools.LinqT
Imports System.Xml.XPath
Imports System.Linq
Imports Tools.RuntimeT.CompilerServicesT
Imports Tools.TextT.UnicodeT
Imports Tools.ExtensionsT
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>
Imports System.Xml
Imports Tools.ComponentModelT
Imports System.Globalization.CultureInfo

#If Config <= Nightly Then
<Module: AddResource(unicodecharacterdatabase.UnicodeXmlDatabaseResourceName, unicodecharacterdatabase.UnicodeXmlDatabaseFileName, False, False, Remove:=True)> 

Namespace TextT.UnicodeT
    ''' <summary>Provides information from Unicode Charatcer Database</summary>
    ''' <remarks>See http://www.unicode.org/ucd/.
    ''' <para>
    ''' To obtain data from Unicode Character Database you must have access to copy of it.
    ''' You can either depend on copy of Unicode Character Database that is distributed with Tools.dll assembly as linked resource ("ucd.all.grouped.xml.gz")
    ''' or you can load Unicode XML data from file or URL.
    ''' </para>
    ''' <note>
    ''' Unicode Charcter Database XML is distributed with Tools.dll as linked resource in file named ucd.all.grouped.xml.gz.
    ''' If you do not need it you may chose not to distribute this file with your application.
    ''' However some methods will fail if this file is not in the same directory as Tools.dll assembly.
    ''' <para>
    ''' Rationale: ucd.all.grouped.xml.gz (7MB GZipped) is such large that it will significantly increase site of Tools.dll when embedded as embedded resource.
    ''' So, it's rather distributed as linked resource giving developers option to redistribute it or not to redistribute it.
    ''' </para>
    ''' </note>
    ''' <note>Linked resource name for file ucd.all.grouped.xml.gz is <c>Tools.TextT.UnicodeT.ucd.all.grouped.xml.gz</c>.</note>
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class UnicodeCharacterDatabase
        Implements IXDocumentWrapper, IXElementWrapper, ICharNameProvider
        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <ucd/>.Name
        ''' <summary>URI of XML namespace of Unicode Character Database XML</summary>
        Public Const XmlNamespace$ = "http://www.unicode.org/ns/2003/ucd/1.0"
        ''' <summary>Code of highest code-point supported by the Unicode standard</summary>
        <CLSCompliant(False)>
        Public Const MaxCodePoint As UInteger = &H10FFFFUI

#Region "Static loading"
        ''' <summary>Name of assembly resource that contains Unicode Character Database XML</summary>
        ''' <remarks>
        ''' <para>The resource is contained in Tools.Text.Unicode.dll assembly (same assembly as the <see cref="UnicodeCharacterDatabase"/> class).</para>
        ''' <para>Because of size of Unicode Character Database XML this resource is GZipped (use <see cref="IO.Compression.GZipStream"/> to unGZip it).</para>
        ''' <para>Grouped version of Unicode Character Database XML is used.</para>
        ''' <para>This resource is not embedded, it's linked. It means that you must distribute file this resource points to with Tools.Text.Unicode.dll assembly to be able to access it.</para>
        ''' </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Const UnicodeXmlDatabaseResourceName As String = "Tools.TextT.UnicodeT.ucd.all.grouped.xml.gz"
        ''' <summary>Name of file that contains Unicode Character Database XML</summary>
        Friend Const UnicodeXmlDatabaseFileName As String = "ucd.all.grouped.xml.gz"
        ''' <summary>Name of assembly resource that contains default NameAliases.txt file from UCD</summary>
        ''' <remarks>This resource is embeded resource in same assembly as <see cref="UnicodeCharacterDatabase"/> (Tools.Text.Unicode.txt)</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Const NameAlisesResourceName$ = "Tools.TextT.UnicodeT.NameAliases.txt"

        ''' <summary>Gets Unicode Character Database in XML format</summary>
        ''' <returns>Content of linked resource <c>Tools.TextT.UnicodeT.ucd.all.grouped.xml.gz</c> (<see cref="UnicodeXmlDatabaseResourceName"/>; file ucd.all.grouped.xml.gz) as <see cref="XDocument"/>.</returns>
        ''' <exception cref="IO.FileNotFoundException">File ucd.all.grouped.xml.gz was not correctly distributed with Tools.dll assembly.</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shared Function GetXml() As XDocument
            Try
                Using resourceStream = GetType(UnicodeCharacterDatabase).Assembly.GetManifestResourceStream(UnicodeXmlDatabaseResourceName)
                    If resourceStream Is Nothing Then
                        Dim ret = GetXmlAlternative()
                        If ret Is Nothing Then Throw New IO.FileNotFoundException(ResourcesT.Exceptions.CannotLoadManifestResourceStream.f(UnicodeXmlDatabaseResourceName), UnicodeXmlDatabaseFileName)
                    End If
                    Using decompressedStream As New IO.Compression.GZipStream(resourceStream, IO.Compression.CompressionMode.Decompress)
                        Return XDocument.Load(decompressedStream)
                    End Using
                End Using
            Catch ex As IO.FileNotFoundException
                Dim ret = GetXmlAlternative()
                If ret Is Nothing Then Throw
                Return ret
            End Try
        End Function

        ''' <summary>Gets XML an alternative vay (as file in same directory as assembly)</summary>
        ''' <returns>Content of Unicode Character Database XML in form of <see cref="XDocument"/>; null if file does not exists</returns>
        Private Shared Function GetXmlAlternative() As XDocument
            Dim xmlPath = IO.Path.Combine(IO.Path.GetDirectoryName(GetType(UnicodeCharacterDatabase).Assembly.Location), UnicodeXmlDatabaseFileName)
            If IO.File.Exists(xmlPath) Then 'This curious situation happens in Unit Tests and I dunno why
                Using fileStream = IO.File.Open(xmlPath, IO.FileMode.Open, IO.FileAccess.Read),
                      decompressesStream As New IO.Compression.GZipStream(fileStream, IO.Compression.CompressionMode.Decompress)
                    Return XDocument.Load(decompressesStream)
                End Using
            Else
                Return Nothing
            End If
        End Function

        Private Shared _default As UnicodeCharacterDatabase
        Private Shared ReadOnly defaultLock As New Object
        ''' <summary>Gets default instance of <see cref="UnicodeCharacterDatabase"/> class initialized with default copy of Unicode Character Database XML</summary>
        ''' <returns>Default instance of Unicode Character Database; null if it cannot be initialized because default Unicode Character Database xml is not assessible (i.e. <see cref="GetXml"/> throws <see cref="IO.FileNotFoundException"/>).</returns>
        ''' <remarks>
        ''' This instance of Unicode character database is pre-loaded with ConScript Unicode Registry (CSUR) data (see <see cref="CsurExtensions"/>) and
        ''' name aliases (see <see cref="NameAliases"/>).
        ''' </remarks>
        Public Shared ReadOnly Property [Default] As UnicodeCharacterDatabase
            Get
                If _default Is Nothing Then
                    SyncLock defaultLock
                        If _default Is Nothing Then
                            Try
                                _default = New UnicodeCharacterDatabase(GetXml)
                            Catch ex As IO.FileNotFoundException
                            End Try
                            _default.Extensions.Add(CsurExtensions.XmlNamespace, CsurExtensions.DefaultCsurDatabase)
                            _default.Extensions.Add(NamesListExtensions.XmlNamespace, NamesListExtensions.DefaultNamesList)
                            _default.LoadNameAliases()
                            _default.LocalizationProvider = UcdLocalizationProvider.default
                        End If
                    End SyncLock
                End If
                Return _default
            End Get
        End Property

        ''' <summary>Gets or sets provider that provides localizations for unicode character database</summary>
        ''' <remarks>Localizations are stored as <see cref="XObject.Annotation"/> of type <see cref="UcdLocalizationProvider"/>.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Property LocalizationProvider As UcdLocalizationProvider
            Get
                Return Xml.Annotation(Of UcdLocalizationProvider)()
            End Get
            Set(value As UcdLocalizationProvider)
                If Xml.Annotation(Of UcdLocalizationProvider)() IsNot Nothing Then
                    Xml.RemoveAnnotations(Of UcdLocalizationProvider)()
                End If
                If value IsNot Nothing Then Xml.AddAnnotation(value)
            End Set
        End Property
#End Region

#Region "CTors"
        Private ReadOnly _xml As XDocument
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeCharacterDatabase"/> class from <see cref="XDocument"/>.</summary>
        ''' <param name="unicodeCharacterDatabaseXml">XML representation of Unicode Character Database as described by <a href="http://www.unicode.org/reports/tr42/">Unicode Standard Annex #42</a>.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="unicodeCharacterDatabaseXml"/> is null</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="unicodeCharacterDatabaseXml"/> is not <c>&lt;ucd></c> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(unicodeCharacterDatabaseXml As XDocument)
            If unicodeCharacterDatabaseXml Is Nothing Then Throw New ArgumentNullException("unicodeCharacterDatabaseXml")
            If Not unicodeCharacterDatabaseXml.Root.Name = elementName Then Throw New ArgumentException(ResourcesT.Exceptions.RootElementMustBe0.f(elementName))
            _xml = unicodeCharacterDatabaseXml
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeCharacterDatabase"/> class from URL where Unicode Character Database XML is located.</summary>
        ''' <param name="unicodeCharacterDatabaseXmlUrl">URL of XML representation of Unicode Character Database as described by <a href="http://www.unicode.org/reports/tr42/">Unicode Standard Annex #42</a>.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="unicodeCharacterDatabaseXmlUrl"/> is null</exception>
        ''' <exception cref="ArgumentException">Root element of XML document is not <c>&lt;ucd></c> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(unicodeCharacterDatabaseXmlUrl As String)
            Me.New(XDocument.Load(unicodeCharacterDatabaseXmlUrl))
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeCharacterDatabase"/> class from <see cref="IO.Stream"/> containing XML document.</summary>
        ''' <param name="unicodeCharacterDatabaseXml">Stream to read XML representation of Unicode Character Database as described by <a href="http://www.unicode.org/reports/tr42/">Unicode Standard Annex #42</a> from.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="unicodeCharacterDatabaseXml"/> is null</exception>
        ''' <exception cref="ArgumentException">Root element of XML is not <c>&lt;ucd></c> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(unicodeCharacterDatabaseXml As IO.Stream)
            Me.New(XDocument.Load(unicodeCharacterDatabaseXml))
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeCharacterDatabase"/> class from <see cref="IO.TextReader"/>.</summary>
        ''' <param name="unicodeCharacterDatabaseXml">Reader to read XML representation of Unicode Character Database as described by <a href="http://www.unicode.org/reports/tr42/">Unicode Standard Annex #42</a> from.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="unicodeCharacterDatabaseXml"/> is null</exception>
        ''' <exception cref="ArgumentException">Root element of XML is not <c>&lt;ucd></c> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(unicodeCharacterDatabaseXml As IO.TextReader)
            Me.New(XDocument.Load(unicodeCharacterDatabaseXml))
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeCharacterDatabase"/> class from <see cref="XmlReader"/>.</summary>
        ''' <param name="unicodeCharacterDatabaseXml">XML representation of Unicode Character Database as described by <a href="http://www.unicode.org/reports/tr42/">Unicode Standard Annex #42</a>.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="unicodeCharacterDatabaseXml"/> is null</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="unicodeCharacterDatabaseXml"/> is not <c>&lt;ucd></c> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(unicodeCharacterDatabaseXml As XmlReader)
            Me.New(XDocument.Load(unicodeCharacterDatabaseXml))
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeCharacterDatabase"/> class from <see cref="XmlDocument"/>.</summary>
        ''' <param name="unicodeCharacterDatabaseXml">XML representation of Unicode Character Database as described by <a href="http://www.unicode.org/reports/tr42/">Unicode Standard Annex #42</a>.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="unicodeCharacterDatabaseXml"/> is null</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="unicodeCharacterDatabaseXml"/> is not <c>&lt;ucd></c> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(unicodeCharacterDatabaseXml As XmlDocument)
            Me.New(XDocument.Load(New XmlNodeReader(unicodeCharacterDatabaseXml)))
        End Sub
#End Region

#Region "IWhateverWrappers"
        ''' <summary>Gets underlying <see cref="XDocument"/> instance that contains data of loaded Unicode Character Database</summary>
        ''' <remarks>Do not change content of the document, it can have unprecedenter results</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property Xml As XDocument Implements IXDocumentWrapper.Document
            Get
                Return _xml
            End Get
        End Property
        ''' <summary>Gets node this instance wraps</summary>
        Private ReadOnly Property IXNodeWrapper_Node As XNode Implements IXNodeWrapper.Node
            Get
                Return Xml
            End Get
        End Property
        ''' <summary>Gets XML element this instance wraps</summary>
        Private ReadOnly Property RootElement As XElement Implements IXElementWrapper.Element
            Get
                Return Xml.Root
            End Get
        End Property
#End Region

#Region "Extensions"
        ''' <summary>Gets ditionary containing extended properties for characters - the properties not comming from official Unicode character database.</summary>
        ''' <remarks>
        ''' Key of the dictionary are names of XML namespaces of the properties. Values are instances of <see cref="UnicodeCharacterDatabase"/> loaded with these additional properties.
        ''' <para>Value of this property is stored in <see cref="Xml"/>.<see cref="XDocument.Annotation">Annotation</see></para>
        ''' </remarks>
        ''' <seelaso cref="CsurExtensions"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property Extensions As IDictionary(Of String, UnicodeCharacterDatabase)
            Get
                Dim ret = Xml.Annotation(Of IDictionary(Of String, UnicodeCharacterDatabase))()
                If ret Is Nothing Then
                    ret = New Dictionary(Of String, UnicodeCharacterDatabase)
                    Xml.AddAnnotation(ret)
                End If
                Return ret
            End Get
        End Property

        ''' <summary>Gets a dictionary that contains textual extensions to Unicode Character Database XML</summary>
        ''' <remarks>
        ''' This property is stored in <see cref="Xml"/>.<see cref="XDocument.Annotations"/> of type <see cref="IDictionary(Of TKey, TValue)"/>[<see cref="String"/>, <see cref="Object"/>].
        ''' <para>
        ''' Unicode Character Database XML provides most of properties from Unicode Character Database (UCD), but traditionaly Unicode Character Database is bunch of text files.
        ''' Some UCD properties are not provided in UCD XML. That's why this property exists. You can load supported textual extensions here.
        ''' </para>
        ''' <para>Keys of the dictionary are names of files from UCD, values are object representations of that files.</para>
        ''' </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property TextualExtensions As IDictionary(Of String, Object)
            Get
                Dim a = Xml.Annotation(Of IDictionary(Of String, Object))()
                If a Is Nothing Then
                    a = New Dictionary(Of String, Object)
                    Xml.AddAnnotation(a)
                End If
                Return a
            End Get
        End Property
#End Region

#Region "Element accessors"

        ''' <summary>Gets description provided for this instance of Unicode Character Database</summary>
        ''' <returns>Description of loaded Unicode Character Database, null of description is not provided</returns>
        Public ReadOnly Property Description As String
            Get
                Return _xml.<ucd>.<description>.Value
            End Get
        End Property

        ''' <summary>Gets character groups in this Unicode Character Database</summary>
        ''' <remarks>
        ''' Unicode Character Database can be stored in two formats - flat and groupped. Flat format contains no groups.
        ''' <para>Groups are only grouping non-normative construct used merely to reduce size of XML file. Do not confuse groups with blocks.</para>
        ''' </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property Groups As IEnumerable(Of UnicodeCharacterGroup)
            Get
                Return From g In Xml.<ucd>.<repertoire>.<group> Select New UnicodeCharacterGroup(g)
            End Get
        End Property

        ''' <summary>Gets all code points in current Unicode Character Database</summary>
        ''' <returns>All code points in current Unicode Character Database (groupped or not)</returns>
        Public ReadOnly Property CodePoints As IEnumerable(Of UnicodeCodePoint)
            Get
                Return From el In CodePointXmlElements
                       Select UnicodeCodePoint.Create(el)
            End Get
        End Property

        ''' <summary>Gets XML elements representing all code points in this instance of Unicode Character Database</summary>
        Protected ReadOnly Property CodePointXmlElements As IEnumerable(Of XElement)
            Get
                Return GetCodePointXmlElements(Xml)
            End Get
        End Property

        ''' <summary>Gets XML elements representing all code points in given XML document which represents instance of Unicode Character Database</summary>
        ''' <param name="document">The XML document that contains Unicode Character Database XML data</param>
        ''' <returns>XML elements representing all code points in <paramref name="document"/> (in document order).</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="document"/> is null</exception>
        Friend Shared Function GetCodePointXmlElements(document As XDocument) As IEnumerable(Of XElement)
            If document Is Nothing Then Throw New ArgumentNullException("document")
            Return From el In document.<ucd>.<repertoire>.Descendants Where el.Name.In(char_name, reserved_name, noncharacter_name, surrogate_name)
            'Return document.<ucd>.<repertoire>...<char>.Union(document.<ucd>.<repertoire>...<reserved>).Union(document.<ucd>.<repertoire>...<noncharacter>).Union(document.<ucd>.<repertoire>...<surrogate>)
        End Function
        ''' <summary><see cref="XName"/> of &lt;char> element</summary>
        Private Shared ReadOnly char_name As XName = <char/>.Name
        ''' <summary><see cref="XName"/> of &lt;reserved> element</summary>
        Private Shared ReadOnly reserved_name As XName = <reserved/>.Name
        ''' <summary><see cref="XName"/> of &lt;noncharacter> element</summary>
        Private Shared ReadOnly noncharacter_name As XName = <noncharacter/>.Name
        ''' <summary><see cref="XName"/> of &lt;surrogate> element</summary>
        Private Shared ReadOnly surrogate_name As XName = <surrogate/>.Name


        ''' <summary>Gets all Unicode blocks</summary>
        Public ReadOnly Property Blocks As IEnumerable(Of UnicodeBlock)
            Get
                Return From el In Xml.<ucd>.<blocks>.<block> Select New UnicodeBlock(el)
            End Get
        End Property

        ''' <summary>Gets named sequences of characters</summary>
        Public ReadOnly Property NamedSequences As IEnumerable(Of UnicodeNamedSequence)
            Get
                Return From el In Xml.<ucd>.<named-sequences>.<named-sequence> Select New UnicodeNamedSequence(el)
            End Get
        End Property

        ''' <summary>Gets provisional named sequences of characters</summary>
        Public ReadOnly Property ProvisionalNamedSequences As IEnumerable(Of UnicodeNamedSequence)
            Get
                Return From el In Xml.<ucd>.<provisional-named-sequences>.<named-sequence> Select New UnicodeNamedSequence(el)
            End Get
        End Property


        ''' <summary>Gets normalization corrections between Unicode versions</summary>
        Public ReadOnly Property NormalizationCorrections As IEnumerable(Of UnicodeNormalizationCorrection)
            Get
                Return From el In Xml.<ucd>.<normalization-corrections>.<normalization-correction> Select New UnicodeNormalizationCorrection(el)
            End Get
        End Property

        ''' <summary>Gets Unicode standartized variants</summary>
        Public ReadOnly Property StandartizedVariants As IEnumerable(Of UnicodeStandardizedVariant)
            Get
                Return From el In Xml.<ucd>.<standardized-variants>.<standardized-variant> Select New UnicodeStandardizedVariant(el)
            End Get
        End Property

        ''' <summary>Gets list of CJK Radicals</summary>
        Public ReadOnly Property CjkRadicals As IEnumerable(Of CjkRadicalInfo)
            Get
                Return From el In Xml.<ucd>.<cjk-radicals>.<cjk-radical> Select New CjkRadicalInfo(el)
            End Get
        End Property

        ''' <summary>Gets list of Emoji sources</summary>
        Public ReadOnly Property EmojiSources As IEnumerable(Of EmojiSourceInfo)
            Get
                Return From el In Xml.<ucd>.<emoji-sources>.<emoji-source> Select New EmojiSourceInfo(el)
            End Get
        End Property

        ''' <summary>Gets repertoire of groups and code points</summary>
        ''' <remarks>Tis is helper property. You ususally don't need it. You usually use <see cref="Groups"/> and <see cref="CodePoints"/> directly.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property Repertoire As UcdRepertoire
            Get
                Dim el = Xml.<ucd>.<repertoire>.FirstOrDefault
                If el IsNot Nothing Then Return New UcdRepertoire(el)
                Return Nothing
            End Get
        End Property
#End Region

#Region "Object overrides (Equals, ToString)"
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="UnicodeCharacterDatabase" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="UnicodeCharacterDatabase" /> (it's <see cref="UnicodeCharacterDatabase"/> and it's backed by the same (reference equivalent) <see cref="Xml"/>); otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="UnicodeCharacterDatabase" />. </param>
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is UnicodeCharacterDatabase Then Return Xml Is DirectCast(obj, UnicodeCharacterDatabase).Xml
            Return MyBase.Equals(obj)
        End Function
        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="UnicodeCharacterDatabase" />.</summary>
        ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="UnicodeCharacterDatabase" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function ToString() As String
            If Not Description.IsNullOrWhiteSpace Then Return Description
            Return MyBase.ToString()
        End Function
#End Region

#Region "Querying"
#Region "Find"
        ''' <summary>Gets information from Unicode Character database for single code point</summary>
        ''' <param name="codePoint">A codepoint to get information for</param>
        ''' <returns>Information about given code point. Null if current instance of Unicode Character Database does not provide information about the character.</returns>
        ''' <remarks>This function is not CLS-compliant. CLS-compliant overload exists.</remarks>
        <CLSCompliant(False)>
        Public Function FindCodePoint(codePoint As UInteger) As UnicodeCodePoint
            Dim [single] = (From el In CodePointXmlElements Where CpMatch(el.@cp, codePoint)).SingleOrDefault
            If [single] IsNot Nothing Then Return UnicodeCodePoint.Create([single])
            Dim range = (From el In CodePointXmlElements Where el.@<cp-first> IsNot Nothing AndAlso el.@<cp-last> IsNot Nothing AndAlso
                                                               UInteger.Parse(el.@<cp-first>, Globalization.NumberStyles.HexNumber, InvariantCulture) <= codePoint AndAlso
                                                               UInteger.Parse(el.@<cp-last>, Globalization.NumberStyles.HexNumber, InvariantCulture) >= codePoint
                        ).SingleOrDefault
            If range IsNot Nothing Then Return UnicodeCodePoint.Create(range).MakeSingle(codePoint)
            Return Nothing
        End Function

        ''' <summary>Detects if XML hexa code point number matches code point number</summary>
        ''' <param name="xmlCp">Hexadecimal code of character</param>
        ''' <param name="cp">Code of character</param>
        ''' <returns>True if <paramref name="xmlCp"/> and <paramref name="cp"/> represent the same number (in hex); false otherwise</returns>
        Private Shared Function CpMatch(xmlCp$, cp As UInteger) As Boolean
            If xmlCp = "" Then Return False
            Return UInteger.Parse(xmlCp, Globalization.NumberStyles.HexNumber, InvariantCulture) = cp
        End Function

        ''' <summary>Gets information from Unicode Character database for single code point</summary>
        ''' <param name="codePoint">A codepoint to get information for</param>
        ''' <returns>Information about given code point. Null if current instance of Unicode Character Database does not provide information about the character.</returns>
        Public Function FindCodePoint(codePoint As Integer) As UnicodeCodePoint
            Return FindCodePoint(codePoint.BitwiseSame)
        End Function
        ''' <summary>Gets information from Unicode Character database for single UTF-16 character</summary>
        ''' <param name="character">A character to get information for</param>
        ''' <returns>Information about given code point. Null if current instance of Unicode Character Database does not provide information about the character.</returns>
        Public Function FindCodePoint(character As Char) As UnicodeCodePoint
            Return FindCodePoint(Char.ConvertToUtf32(CStr(character), 0))
        End Function
#End Region

        ''' <summary>Queries Unicode Character Database XML using XPath</summary>
        ''' <param name="xPath">XPath query</param>
        ''' <returns>Objects retrieved suing given query</returns>
        ''' <remarks>Default namespace is mapped to http://www.unicode.org/ns/2003/ucd/1.0</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="xPath"/> is nulll</exception>
        Public Function Query(xPath As String) As IEnumerable
            If xPath Is Nothing Then Throw New ArgumentNullException("xPath")
            Dim r As New XmlNamespaceManager(New NameTable)
            r.AddNamespace(String.Empty, GetXmlNamespace().NamespaceName)
            Return QueryToObjects(Xml.XPathSelectElements(xPath, r))
        End Function

        ''' <summary>Queries Unicode Character Database XML using Linq-to-XML (XLINQ)</summary>
        ''' <param name="path">A delegate (typically λ-function) specifying a query to select XML Elements from Unicode Character Database XML</param>
        ''' <returns>Objects retrieved suing given query</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="path"/> is nulll</exception>
        Public Function Query(path As Func(Of XDocument, IEnumerable(Of XElement))) As IEnumerable
            If path Is Nothing Then Throw New ArgumentNullException("path")
            Return QueryToObjects(path(Xml))
        End Function

        ''' <summary>Converts each object in enumeration of <see cref="XElement"/>s to UCD object</summary>
        ''' <param name="query">Represents a query to Unicode Character Database XML</param>
        ''' <returns>An enumeration of objects instantiated from elements in <paramref name="query"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="query"/> is nulll</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shared Function QueryToObjects(query As IEnumerable(Of XElement)) As IEnumerable(Of IXmlElementWrapper)
            If query Is Nothing Then Throw New ArgumentNullException("query")
            Return From el In query Where el IsNot Nothing Let ret = ElementToObject(el) Where ret IsNot Nothing Select ret
        End Function

        ''' <summary>Converts a XML elemetn to one of Unicode Character Database object from <see cref="Tools.TextT.UnicodeT"/> namespace</summary>
        ''' <param name="element">A <see cref="XElement"/> create wrapper object for</param>
        ''' <returns>A wrapper object for given element. Null if <paramref name="element"/> is null. <see cref="XElementWrapper"/> if element type is unknown.</returns>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shared Function ElementToObject(element As XElement) As IXElementWrapper
            Select Case element.Name
                Case UnicodeCharacterDatabase.elementName
                    If element.Document IsNot Nothing AndAlso element.Document.Root Is element Then Return New UnicodeCharacterDatabase(element.Document)
                    Return New XElementWrapper(element)
                Case UcdRepertoire.elementName : Return New UcdRepertoire(element)
                Case ReservedUnicodeCodePoint.elementName, UnicodeNoncharacter.elementName, UnicodeSurrogate.elementName, UnicodeCharacter.elementName
                    Return UnicodeCodePoint.Create(element)
                Case UnicodeCharacterGroup.elementName : Return New UnicodeCharacterGroup(element)
                Case (<blocks/>).Name : Return New UcdCollection(Of UnicodeBlock)(element)
                Case UnicodeBlock.elementName : Return New UnicodeBlock(element)
                Case (<named-sequences/>).Name : Return New UcdCollection(Of UnicodeNamedSequence)(element)
                Case (<provisional-named-sequences/>).Name : Return New UcdCollection(Of UnicodeNamedSequence)(element)
                Case UnicodeNamedSequence.elementName : Return New UnicodeNamedSequence(element)
                Case (<normalization-corrections/>).Name : Return New UcdCollection(Of UnicodeNormalizationCorrection)(element)
                Case UnicodeNormalizationCorrection.elementName : Return New UnicodeNormalizationCorrection(element)
                Case (<standardized-variants/>).Name : Return New UcdCollection(Of UnicodeStandardizedVariant)(element)
                Case UnicodeStandardizedVariant.elementName : Return New UnicodeStandardizedVariant(element)
                Case (<cjk-radicals/>).Name : Return New UcdCollection(Of CjkRadicalInfo)(element)
                Case CjkRadicalInfo.elementName : Return New CjkRadicalInfo(element)
                Case (<emoji-sources/>).Name : Return New UcdCollection(Of EmojiSourceInfo)(element)
                Case EmojiSourceInfo.elementName : Return New EmojiSourceInfo(element)
                Case Else : Return New XElementWrapper(element)
            End Select
        End Function

#Region "Blocks"
        ''' <summary>Finds a block a code point belongs to</summary>
        ''' <param name="blocks">A collection of XML elements representing blocks to search within. The collection should contain &lt;block> elements from the http://www.unicode.org/ns/2003/ucd/1.0 namespace (this precondition is not checked).</param>
        ''' <param name="codePoint">Code point (character) to find block it belongs to</param>
        ''' <param name="checked">True to check for duplicate block, false to skip this check. If <paramref name="checked"/> is true <see cref="Enumerable.SingleOrDefault"/> is used to select a block, if it is false <see cref="Enumerable.FirstOrDefault"/> is used.</param>
        ''' <returns>
        ''' An element from the <paramref name="blocks"/> collection <paramref name="codePoint"/> falls in range of codepoints of.
        ''' Null if no such element can ba found.
        ''' <note>What happens when more than such element is found depends on value of the <paramref name="checked"/> parameter. When <paramref name="checked"/> is true an <see cref="InvalidOperationException"/> is thrown. When <paramref name="checked"/> is false first matching block is returned and the other bllocks (elements) are ignored.</note>
        ''' </returns>
        ''' <remarks>
        ''' Attributes forst-cp and last-cp are used to determine block range.
        ''' <para>This method is not CLS-compliant and no CLS-compliant alternative is provided. Consumers not capable of using <see cref="UInteger"/> type should use one of instance overloads instead.</para>
        ''' </remarks>
        ''' <exception cref="ArgumentNullException">Attribute first-cp or last-cp of an element is not provided.</exception>
        ''' <exception cref="FormatException">Value of attribute first-cp or last-cp cannot be parsed as hexadecimal number.</exception>
        ''' <exception cref="OverflowException">Value of attribute first-cp or last-cp can be parsed as hexadecimal number but it does not fall into range of the <see cref="UInteger"/> type.</exception>
        ''' <exception cref="InvalidOperationException"><paramref name="checked"/> is true and more than one block is found.</exception>
        <CLSCompliant(False)>
        Protected Friend Shared Function FindBlock(blocks As IEnumerable(Of XElement), codePoint As UInteger, checked As Boolean) As XElement
            Dim suitableBlocks =
                From b In blocks
                Where
                    UInteger.Parse(b.@<first-cp>, Globalization.NumberStyles.HexNumber, InvariantCulture) <= codePoint AndAlso
                    UInteger.Parse(b.@<last-cp>, Globalization.NumberStyles.HexNumber, InvariantCulture) >= codePoint
            Dim oneBlock = If(checked, suitableBlocks.SingleOrDefault, suitableBlocks.FirstOrDefault)
            Return oneBlock
        End Function

        ''' <summary>Gets a Unicode block that covers given code point</summary>
        ''' <param name="codePoint">A code point to get block that contains it</param>
        ''' <returns>A Unicode block that cotains code-point identified by it's code given in <paramref name="codePoint"/>.</returns>
        ''' <remarks>This method is not CLS-compliant. CLS compliant alternative (overload) is provided.</remarks>
        ''' <exception cref="InvalidOperationException">The data in UCD XML are invalid - see <see cref="Exception.InnerException"/> for details. See <see cref="M:Tools.TextT.UnicodeT.UnicodeCharacterDatabase.FindBlock(System.Collections.Generic.IEnumerable`1[System.Xml.Linq.XElement],System.UInt32,System.Boolean)"/> for detailed explanation of <see cref="Exception.InnerException"/>.</exception>
        ''' <seelaso cref="UnicodeCodePoint.Block"/>
        <CLSCompliant(False)>
        Public Function FindBlock(codePoint As UInteger) As UnicodeBlock
            Dim bel As XElement
            Try
                bel = FindBlock(Xml.<ucd>.<blocks>.<block>, codePoint, False)
            Catch ex As Exception When TypeOf ex Is ArgumentNullException OrElse TypeOf ex Is FormatException OrElse TypeOf ex Is OverflowException
                Throw New InvalidOperationException(ex.Message, ex)
            End Try
            If bel Is Nothing Then Return Nothing
            Return New UnicodeBlock(bel)
        End Function

        ''' <summary>Gets a Unicode block that covers given code point (CLS-compliant alternative</summary>
        ''' <param name="codePoint">A code point to get block that contains it</param>
        ''' <returns>A Unicode block that cotains code-point identified by it's code given in <paramref name="codePoint"/>.</returns>
        ''' <remarks>This method is not CLS-compliant. CLS compliant alternative (overload) is provided.</remarks>
        ''' <exception cref="InvalidOperationException">The data in UCD XML are invalid - see <see cref="Exception.InnerException"/> for details. See <see cref="M:Tools.TextT.UnicodeT.UnicodeCharacterDatabase.FindBlock(System.Collections.Generic.IEnumerable`1[System.Xml.Linq.XElement],System.UInt32,System.Boolean)"/> for detailed explanation of <see cref="Exception.InnerException"/>.</exception>
        ''' <seelaso cref="UnicodeCodePoint.Block"/>
        Public Function FindBlock(codePoint As Integer) As UnicodeBlock
            Return FindBlock(codePoint.BitwiseSame)
        End Function
#End Region
#End Region

#Region "NameALiases.txt"
        ''' <summary>Gets dictionary of name aliases for Unicode code-points</summary>
        ''' <returns>A dictionary of name aliases. Null if name aliases were not loded.</returns>
        ''' <remarks>
        ''' Aliases are stored in <see cref="TextualExtensions"/> under key <c>NameAliases.txt</c>.
        ''' <para>Name aliases are not part of Unicode Character Database XML and thus they are not loaded by default when loading <see cref="UnicodeCharacterDatabase"/>. You must load them explicitly uisng some of <see cref="LoadNameAliases"/> overloads or set them directly to <see cref="TextualExtensions"/> directly.</para>
        ''' <para>This property is not CLS-compliant. Direct CLS-compliant alternative is not provided. Use aliases-related properties of <see cref="UnicodeCodePoint"/>.</para>
        ''' <para>Keys of the dictionary are code-point codes, values are alias names. Each code-point can have zero or more aliases. Currently there are only few (11 as of Unicode 6.0) aliases defined in the Unicode standard and no character has more than one alias.</para>
        ''' </remarks>
        <CLSCompliant(False)>
        Public ReadOnly Property NameAliases As IDictionary(Of UInteger, String())
            Get
                Dim ret As Object = Nothing
                TextualExtensions.TryGetValue("NameAliases.txt", ret)
                Return TryCast(ret, IDictionary(Of UInteger, String()))
            End Get
        End Property

        ''' <summary>Loads default aliases names that are stored in an embeded resource inside Tools.Text.Unicode.dll</summary>
        ''' <exception cref="InvalidOperationException">Name aliases were already loaded for this instance. You must unlloaded them from <see cref="TextualExtensions"/> first.</exception>
        ''' <seelaso cref="NameAliases"/>
        Public Sub LoadNameAliases()
            LoadNameAliases(New IO.StreamReader(GetType(UnicodeCharacterDatabase).Assembly.GetManifestResourceStream(NameAlisesResourceName)))
        End Sub

        ''' <summary>Loads aliases from a file</summary>
        ''' <param name="path">Path to NameAliases.txt file from Unicode Character Database (UCD)</param>
        ''' <remarks>For Unicode 6.0 the file is publicly available at <a href="http://www.unicode.org/Public/6.0.0/ucd/NameAliases.txt">http://www.unicode.org/Public/6.0.0/ucd/NameAliases.txt</a>.</remarks>
        ''' <exception cref="InvalidOperationException">Name aliases were already loaded for this instance. You must unlloaded them from <see cref="TextualExtensions"/> first.</exception>
        ''' <exception cref="FormatException">Unexpeced format of data in file being read. See inner exception for details.</exception>
        ''' <seelaso cref="NameAliases"/>
        Public Sub LoadNameAliases(path As String)
            LoadNameAliases(IO.File.OpenText(path))
        End Sub

        ''' <summary>Loads aliases from a text reader</summary>
        ''' <param name="reader">A reader to read aliases from. The reader should point to firs tile of file in NameAlias.txt format (as used in UCD)</param>
        ''' <exception cref="InvalidOperationException">Name aliases were already loaded for this instance. You must unlloaded them from <see cref="TextualExtensions"/> first.</exception>
        ''' <exception cref="FormatException">Unexpeced format of data in file being read. See inner exception for details.</exception>
        ''' <seelaso cref="NameAliases"/>
        Public Sub LoadNameAliases(reader As IO.TextReader)
            Dim a = ReadNameAliases(reader)
            TextualExtensions.Add("NameAliases.txt", a)
        End Sub

        ''' <summary>Parses contant of NameAliases.txt file from Unicode Character Database to a dictionary of name aliases</summary>
        ''' <param name="reader">A reader that points to first line of NameAliases.txt-like file (a file in compatible format)</param>
        ''' <returns>A dictionary keyed by code-points and valued by name aliases for that code-point</returns>
        ''' <remarks>
        ''' This function is not CLS-compliant. No direct CLS-compliant counterpart is provided. Use some of instance overloads of <see cref="LoadNameAliases"/>.
        ''' <para>Parsing alghoritm provided by this method will work with proposed etxension of Unicode 6.1.0 but will ignore the Type field.</para>
        ''' </remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="reader"/> is null.</exception>
        ''' <exception cref="FormatException">Unexpeced format of data in file being read. See inner exception for details.</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced), CLSCompliant(False)>
        Public Shared Function ReadNameAliases(reader As IO.TextReader) As IDictionary(Of UInteger, String())
            If reader Is Nothing Then Throw New ArgumentNullException("reader")
            Dim aliases As New Dictionary(Of UInteger, List(Of String))

            Dim line As String
            Dim lineNumber% = 0
            Do
                line = reader.ReadLine
                lineNumber += 1
                If line.IsNullOrWhiteSpace OrElse line.StartsWith("#") Then Continue Do
                If line.Contains("#") Then line = line.SubstringBefore("#")
                Dim parts = line.Split({";"c})
                Try
                    Dim code = UInteger.Parse(parts(0), Globalization.NumberStyles.HexNumber, InvariantCulture)
                    If aliases.ContainsKey(code) Then aliases(code).Add(parts(1)) Else aliases.Add(code, New List(Of String) From {parts(0)})
                Catch ex As Exception
                    Throw New FormatException(TextT.UnicodeT.UnicodeResources.ex_ErrorReadingNameAliases.f(lineNumber, ex.Message), ex)
                End Try
            Loop Until line Is Nothing

            Dim ret As New Dictionary(Of UInteger, String())(aliases.Count)
            For Each itm In aliases
                ret.Add(itm.Key, itm.Value.ToArray)
            Next
            Return ret
        End Function
#End Region

#Region "NameList.txt"

#End Region

        ''' <summary>Gets name of a character</summary>
        ''' <param name="codePoint">A Unicode (UTF-32) code-point</param>
        ''' <returns>Name of the character, nulll of the source is not capable of providing character name</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="codePoint"/> is less than zero or greater than <see cref="UnicodeCharacterDatabase.MaxCodePoint"/>.</exception>
        Private Function GetName(codePoint As Integer) As String Implements ICharNameProvider.GetName
            Dim ch = FindCodePoint(codePoint)
            If ch IsNot Nothing Then Return ch.UniversalName
            Return Nothing
        End Function
    End Class
End Namespace
#End If