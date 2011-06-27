Imports System.Xml.Linq
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
        Implements IXDocumentWrapper
#Region "Static loading"
        ''' <summary>Name of assembly resource that contains Unicode Character Database XML</summary>
        ''' <remarks>
        ''' <para>The resource is contained in Tools.dll assembly (same assembly as <see cref="UnicodeCharacterDatabase"/> class).</para>
        ''' <para>Because of size of Unicode Character Database XML this resource is GZipped (use <see cref="IO.Compression.GZipStream"/> to unGZip it).</para>
        ''' <para>Grouped version of Unicode Character Database XML is used.</para>
        ''' <para>This resource is not embedded, it's linked. It means that you must distribute file this resource points to with Tools.dll assembly to be able to access it.</para>
        ''' </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Const UnicodeXmlDatabaseResourceName As String = "Tools.TextT.UnicodeT.ucd.all.grouped.xml.gz"
        ''' <summary>Name of file that contains Unicode Character Database XML</summary>
        Friend Const UnicodeXmlDatabaseFileName As String = "ucd.all.grouped.xml.gz"

        ''' <summary>Gets Unicode Character Database in XML format</summary>
        ''' <returns>Content of linked resource <c>Tools.TextT.UnicodeT.ucd.all.grouped.xml.gz</c> (file ucd.all.grouped.xml.gz) as <see cref="XDocument"/>.</returns>
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
        Public Shared ReadOnly Property [Default] As UnicodeCharacterDatabase
            Get
                If _default Is Nothing Then
                    SyncLock defaultLock
                        If _default Is Nothing Then
                            Try
                                _default = New UnicodeCharacterDatabase(GetXml)
                            Catch ex As IO.FileNotFoundException
                            End Try
                        End If
                    End SyncLock
                End If
                Return _default
            End Get
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
            If Not _xml.Root.Name = GetXmlNamespace().GetName("ucd") Then Throw New ArgumentException(ResourcesT.Exceptions.RootElementMustBe0.f(GetXmlNamespace().GetName("ucd")))
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

#Region "Element accessors"

        ''' <summary>Gets description provided for this instance of Unicode Character Database</summary>
        ''' <returns>Description of loaded Unicode Character Database, null of description is not provided</returns>
        Public ReadOnly Property Description As String
            Get
                Return _xml.<description>.Value
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
                Return From g In Xml.<repertoire>.<group> Select New UnicodeCharacterGroup(g)
            End Get
        End Property

        ''' <summary>Gets all code points in current Unicode Character Database</summary>
        ''' <returns>All code points in current Unicode Character Database (groupped or not)</returns>
        Public ReadOnly Property CodePoints As IEnumerable(Of UnicodeCodePoint)
            Get
                Return From el In CodePointXmlElements.InDocumentOrder
                       Select UnicodeCodePoint.Create(el)
            End Get
        End Property

        ''' <summary>Gets XML elements representing all code points in this instance of Unicode Character Database</summary>
        Protected ReadOnly Property CodePointXmlElements As IEnumerable(Of XElement)
            Get
                Return Xml.<repertoire>...<char>.Union(Xml.<repertoire>...<reserved>).Union(Xml.<repertoire>...<noncharacter>).Union(Xml.<repertoire>...<surrogate>)
            End Get
        End Property

#End Region

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

        ''' <summary>Gets information from Unicode Character database for single code point</summary>
        ''' <param name="codePoint">A codepoint to get information for</param>
        ''' <returns>Information about given code point. Null if current instance of Unicode Character Database does not provide information about the character.</returns>
        ''' <remarks>This function is not CLS-compliant. CLS-compliant overload exists.</remarks>
        <CLSCompliant(False)>
        Public Function FindCodePoint(codePoint As UInteger) As UnicodeCodePoint
            Dim [single] = (From el In CodePointXmlElements Where el.@cp = codePoint.ToString("X", InvariantCulture)).SingleOrDefault
            If [single] IsNot Nothing Then Return UnicodeCodePoint.Create([single])
            Dim range = (From el In CodePointXmlElements Where el.@<cp-first> IsNot Nothing AndAlso el.@<cp-last> IsNot Nothing AndAlso
                                                                UInteger.Parse("0x" & el.@<cp-first>, Globalization.NumberStyles.HexNumber, InvariantCulture) <= codePoint AndAlso
                                                                UInteger.Parse("0x" & el.@<cp-last>, Globalization.NumberStyles.HexNumber, InvariantCulture) >= codePoint
                        ).SingleOrDefault
            If range IsNot Nothing Then Return UnicodeCodePoint.Create(range).MakeSingle(codePoint)
            Return Nothing
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
    End Class
End Namespace
#End If