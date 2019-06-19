Imports System.Xml
Imports System.Runtime.CompilerServices
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>

Namespace TextT.UnicodeT
    ''' <summary>Contains extension methods for <see cref="UnicodeCharacterDatabase"/> and related classes to work with ConScript Unicode Registry (CSUR)</summary>
    ''' <version version="1.5.4">This module is new in version 1.5.4</version>
    Public Module CsurExtensions
        ''' <summary>Name of XML namespace for ConScript Unicode Registry extension properties</summary>
        Public Const XmlNamespace$ = "http://www.evertype.com/standards/csur"
        Private _default As UnicodeCharacterDatabase
        Private ReadOnly defaultLock As New Object
        ''' <summary>Gets default instance of <see cref="UnicodeCharacterDatabase"/> class initialized by default CSUR data. This instance contains only CSUR data.</summary>
        ''' <returns>Default instance of CSUR data.</returns>
        ''' <remarks>
        ''' Instance of <see cref="UnicodeCharacterDatabase"/> returned by this property neither contains all characters not it provides standard UCD properties.
        ''' It contains only characters registered with CSUR and only CSUR properties.
        ''' </remarks>
        ''' <seelaso cref="UnicodeCharacterDatabase.Extensions"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property DefaultCsurDatabase As UnicodeCharacterDatabase
            Get
                If _default Is Nothing Then
                    SyncLock defaultLock
                        If _default Is Nothing Then
                            _default = New UnicodeCharacterDatabase(GetXml)
                        End If
                    End SyncLock
                End If
                Return _default
            End Get
        End Property
        ''' <summary>Name of assembly resource that contains Unicode Character Database XML with only CSUR properties</summary>
        ''' <remarks>
        ''' <para>The resource is contained in Tools.Text.Unicode.dll assembly (same assembly as the <see cref="CsurExtensions"/> module).</para>
        ''' <para>To optimalize DLL size the resource is GZipped (use <see cref="IO.Compression.GZipStream"/> to unGZip it).</para>
        ''' <para>The file uses UCD grouping method.</para>
        ''' <para>Unlike <see cref="UnicodeCharacterDatabase.UnicodeXmlDatabaseResourceName"/> this resource is embedded normally.</para>
        ''' </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Const UnicodeXmlDatabaseResourceName As String = "Tools.TextT.UnicodeT.CSUR.xml.gz"
        ''' <summary>Gets Unicode Character Database containing only CSUR properties in XML format</summary>
        ''' <returns>Content of linked resource <c>Tools.TextT.UnicodeT.CSUR.xml.gz</c> (<see cref="UnicodeXmlDatabaseResourceName"/>) as <see cref="XDocument"/>.</returns>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Function GetXml() As XDocument
            Using resourceStream = GetType(UnicodeCharacterDatabase).Assembly.GetManifestResourceStream(UnicodeXmlDatabaseResourceName)
                Using decompressedStream As New IO.Compression.GZipStream(resourceStream, IO.Compression.CompressionMode.Decompress)
                    Return XDocument.Load(decompressedStream)
                End Using
            End Using
        End Function

#Region "LoadCsur"
        ''' <summary>Loads default ConScript Unicode Registry (CSUR) data as extension data for existing <see cref="UnicodeCharacterDatabase"/></summary>
        ''' <param name="mainDatabase">The instance of <see cref="UnicodeCharacterDatabase"/> to load CSUR extension data into.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="mainDatabase"/> is null</exception>
        ''' <remarks>If <paramref name="mainDatabase"/>.<see cref="UnicodeCharacterDatabase.Extensions">Extensions</see> already contains CSUR database (identified by <see cref="XmlNamespace"/>) it's replaced. You should not do this - it can have unexpected effects on exiting instances of UCD types.</remarks>
        <Extension()>
        Public Sub LoadCsur(mainDatabase As UnicodeCharacterDatabase)
            LoadCsur(mainDatabase, DefaultCsurDatabase)
        End Sub

        ''' <summary>Loads ConScript Unicode Registry (CSUR) data represented as <see cref="UnicodeCharacterDatabase"/> instance as extension data for existing <see cref="UnicodeCharacterDatabase"/></summary>
        ''' <param name="mainDatabase">The instance of <see cref="UnicodeCharacterDatabase"/> to load CSUR extension data into.</param>
        ''' <param name="csurDatabase">An instance of <see cref="UnicodeCharacterDatabase"/> that provides CSUR extension propertires' values</param>
        ''' <exception cref="ArgumentNullException"><paramref name="mainDatabase"/> or <paramref name="csurDatabase"/> is null</exception>
        ''' <remarks>If <paramref name="mainDatabase"/>.<see cref="UnicodeCharacterDatabase.Extensions">Extensions</see> already contains CSUR database (identified by <see cref="XmlNamespace"/>) it's replaced. You should not do this - it can have unexpected effects on exiting instances of UCD types.</remarks>
        <Extension()>
        Public Sub LoadCsur(mainDatabase As UnicodeCharacterDatabase, csurDatabase As UnicodeCharacterDatabase)
            If mainDatabase Is Nothing Then Throw New ArgumentNullException("mainDatabase")
            If csurDatabase Is Nothing Then Throw New InvalidEnumArgumentException("csurDatabase")
            If mainDatabase.Extensions.ContainsKey(XmlNamespace) Then mainDatabase.Extensions(XmlNamespace) = csurDatabase Else mainDatabase.Extensions.Add(XmlNamespace, csurDatabase)
        End Sub

        ''' <summary>Loads ConScript Unicode Registry (CSUR) data represented as <see cref="XDocument"/> isntance as extension data for existing <see cref="UnicodeCharacterDatabase"/></summary>
        ''' <param name="mainDatabase">The instance of <see cref="UnicodeCharacterDatabase"/> to load CSUR extension data into.</param>
        ''' <param name="csurDatabase">An XML document that represents Unicode Character Database XML -like document that contains values of CSUR properties.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="mainDatabase"/> or <paramref name="csurDatabase"/> is null</exception>
        ''' <exception cref="ArgumentException">Root element of XML document is not <c>&lt;ucd></c> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        ''' <remarks>If <paramref name="mainDatabase"/>.<see cref="UnicodeCharacterDatabase.Extensions">Extensions</see> already contains CSUR database (identified by <see cref="XmlNamespace"/>) it's replaced. You should not do this - it can have unexpected effects on exiting instances of UCD types.</remarks>
        <Extension()>
        Public Sub LoadCsur(mainDatabase As UnicodeCharacterDatabase, csurDatabase As XDocument)
            LoadCsur(mainDatabase, New UnicodeCharacterDatabase(csurDatabase))
        End Sub

        ''' <summary>Loads ConScript Unicode Registry (CSUR) data represented as <see cref="XmlDocument"/> instance as extension data for existing <see cref="UnicodeCharacterDatabase"/></summary>
        ''' <param name="mainDatabase">The instance of <see cref="UnicodeCharacterDatabase"/> to load CSUR extension data into.</param>
        ''' <param name="csurDatabase">An XML document that represents Unicode Character Database XML -like document that contains values of CSUR properties.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="mainDatabase"/> or <paramref name="csurDatabase"/> is null</exception>
        ''' <exception cref="ArgumentException">Root element of XML document is not <c>&lt;ucd></c> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        ''' <remarks>If <paramref name="mainDatabase"/>.<see cref="UnicodeCharacterDatabase.Extensions">Extensions</see> already contains CSUR database (identified by <see cref="XmlNamespace"/>) it's replaced. You should not do this - it can have unexpected effects on exiting instances of UCD types.</remarks>
        <Extension()>
        Public Sub LoadCsur(mainDatabase As UnicodeCharacterDatabase, csurDatabase As XmlDocument)
            LoadCsur(mainDatabase, New UnicodeCharacterDatabase(csurDatabase))
        End Sub

        ''' <summary>Loads ConScript Unicode Registry (CSUR) data from a path as extension data for existing <see cref="UnicodeCharacterDatabase"/></summary>
        ''' <param name="mainDatabase">The instance of <see cref="UnicodeCharacterDatabase"/> to load CSUR extension data into.</param>
        ''' <param name="csurDatabasePath">A path to an XML document in Unicode Character Database XML -like format that contains values of CSUR properties</param>
        ''' <exception cref="ArgumentNullException"><paramref name="mainDatabase"/> or <paramref name="csurDatabasePath"/> is null</exception>
        ''' <exception cref="ArgumentException">Root element of XML document is not <c>&lt;ucd></c> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        ''' <remarks>If <paramref name="mainDatabase"/>.<see cref="UnicodeCharacterDatabase.Extensions">Extensions</see> already contains CSUR database (identified by <see cref="XmlNamespace"/>) it's replaced. You should not do this - it can have unexpected effects on exiting instances of UCD types.</remarks>
        <Extension()>
        Public Sub LoadCsur(mainDatabase As UnicodeCharacterDatabase, csurDatabasePath As String)
            LoadCsur(mainDatabase, New UnicodeCharacterDatabase(csurDatabasePath))
        End Sub

        ''' <summary>Loads ConScript Unicode Registry (CSUR) data from an XML reader as extension data for existing <see cref="UnicodeCharacterDatabase"/></summary>
        ''' <param name="mainDatabase">The instance of <see cref="UnicodeCharacterDatabase"/> to load CSUR extension data into.</param>
        ''' <param name="csurDatabaseSource">A reader that reads an XML document in UCDXML-like format that provides values of CSUR properties.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="mainDatabase"/> is null</exception>
        ''' <exception cref="ArgumentException">Root element of XML document is not <c>&lt;ucd></c> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        ''' <remarks>If <paramref name="mainDatabase"/>.<see cref="UnicodeCharacterDatabase.Extensions">Extensions</see> already contains CSUR database (identified by <see cref="XmlNamespace"/>) it's replaced. You should not do this - it can have unexpected effects on exiting instances of UCD types.</remarks>
        <Extension()>
        Public Sub LoadCsur(mainDatabase As UnicodeCharacterDatabase, csurDatabaseSource As XmlReader)
            LoadCsur(mainDatabase, New UnicodeCharacterDatabase(csurDatabaseSource))
        End Sub

        ''' <summary>Loads ConScript Unicode Registry (CSUR) data from a <see cref="IO.TextReader"/> as extension data for existing <see cref="UnicodeCharacterDatabase"/></summary>
        ''' <param name="mainDatabase">The instance of <see cref="UnicodeCharacterDatabase"/> to load CSUR extension data into.</param>
        ''' <param name="csurDatabaseSource">A reader that reads an XML document in UCDXML-like format that provides values of CSUR properties.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="mainDatabase"/> is null</exception>
        ''' <exception cref="ArgumentException">Root element of XML document is not <c>&lt;ucd></c> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        ''' <remarks>If <paramref name="mainDatabase"/>.<see cref="UnicodeCharacterDatabase.Extensions">Extensions</see> already contains CSUR database (identified by <see cref="XmlNamespace"/>) it's replaced. You should not do this - it can have unexpected effects on exiting instances of UCD types.</remarks>
        <Extension()>
        Public Sub LoadCsur(mainDatabase As UnicodeCharacterDatabase, csurDatabaseSource As IO.TextReader)
            LoadCsur(mainDatabase, New UnicodeCharacterDatabase(csurDatabaseSource))
        End Sub

        ''' <summary>Loads ConScript Unicode Registry (CSUR) data from a stream as extension data for existing <see cref="UnicodeCharacterDatabase"/></summary>
        ''' <param name="mainDatabase">The instance of <see cref="UnicodeCharacterDatabase"/> to load CSUR extension data into.</param>
        ''' <param name="csurDatabaseSource">A stream that reads an XML document in UCDXML-like format that provides values of CSUR properties.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="mainDatabase"/> is null</exception>
        ''' <exception cref="ArgumentException">Root element of XML document is not <c>&lt;ucd></c> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        ''' <remarks>If <paramref name="mainDatabase"/>.<see cref="UnicodeCharacterDatabase.Extensions">Extensions</see> already contains CSUR database (identified by <see cref="XmlNamespace"/>) it's replaced. You should not do this - it can have unexpected effects on exiting instances of UCD types.</remarks>
        <Extension()>
        Public Sub LoadCsur(mainDatabase As UnicodeCharacterDatabase, csurDatabaseSource As IO.Stream)
            LoadCsur(mainDatabase, New UnicodeCharacterDatabase(csurDatabaseSource))
        End Sub
#End Region

        ''' <summary>Gets a value indicating if ConScript Unicode Registry (CSUR) extension data are loaded for given instance of Unicode Character Database</summary>
        ''' <param name="ucd">A <see cref="UnicodeCharacterDatabase"/> instance to tell about</param>
        ''' <returns>True if CSUR extension data are loaded for <paramref name="ucd"/>; false if they are not.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="ucd"/> is null</exception>
        ''' <seelaso cref="UnicodeCharacterDatabase.Extensions"/>
        <Extension()>
        Public Function IsCsurLoaded(ucd As UnicodeCharacterDatabase) As Boolean
            If ucd Is Nothing Then Throw New ArgumentNullException("ucd")
            Return ucd.Extensions.ContainsKey(XmlNamespace) AndAlso ucd.Extensions(XmlNamespace) IsNot Nothing
        End Function

        ''' <summary>Gets a value indicating if ConScript Unicode Registry (CSUR) extension data are loaded for given instance of <see cref="UnicodePropertiesProvider"/>-derived class</summary>
        ''' <param name="provider">A <see cref="UnicodePropertiesProvider"/> to tell about</param>
        ''' <returns>True if CSUR extension data are loaded for <paramref name="provider"/> (respectively the <see cref="XDocument"/> it reads its data from); false if they are not.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="provider"/> is null</exception>
        ''' <seelaso cref="UnicodePropertiesProvider.GetExtensions"/>
        <Extension()>
        Public Function IsCsurLoaded(provider As UnicodePropertiesProvider) As Boolean
            If provider Is Nothing Then Throw New ArgumentNullException("provider")
            Return provider.GetExtension(XmlNamespace) IsNot Nothing
        End Function

        ''' <summary>Gets ConScript Unicode Registry (CSUR) extension (non-standard) properties for given Unicode Code-Point</summary>
        ''' <param name="codePoint">The code point to get CSUR data for</param>
        ''' <exception cref="ArgumentNullException"><paramref name="codePoint"/> is null.</exception>
        ''' <returns>
        ''' An instance of <see cref="CsurPropertiesProvider"/> that provides CSUR data for <paramref name="codePoint"/>.
        ''' Null if<paramref name="codePoint"/> represents multiple code-points (i.e. <paramref name="codePoint"/>.<see cref="UnicodeCodePoint.CodePoint"/> is null) or,
        ''' CSUR data are not loaded for Unicode Character Database instance <paramref name="codePoint"/> comes from or,
        ''' CSUR does not provide data for given code point.
        ''' </returns>
        ''' <remarks>CSUR provides data only for characters in private use area of Unicode standard.</remarks>
        <Extension()>
        Public Function Csur(codePoint As UnicodeCodePoint) As CsurPropertiesProvider
            If codePoint Is Nothing Then Throw New ArgumentNullException("codePoint")
            If Not codePoint.CodePoint.HasValue Then Return Nothing
            If Not codePoint.IsCsurLoaded Then Return Nothing
            Dim csurCodePoint = codePoint.GetExtension(XmlNamespace).FindCodePoint(codePoint.CodePoint.Value)
            If csurCodePoint Is Nothing Then Return Nothing
            Return New CsurPropertiesProvider(csurCodePoint, codePoint.Element.Document)
        End Function

        ''' <summary>Gets ConScript Unicode Registry (CSUR) blocks</summary>
        ''' <param name="database">A database to get CSUR blocks for</param>
        ''' <returns>CSUR blocks from CSUR extension. An empty enumeration if CSUR extension is not registered.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="database"/> is null</exception>
        <Extension()>
        Public Function CsurBlocks(database As UnicodeCharacterDatabase) As IEnumerable(Of CsurBlock)
            If database Is Nothing Then Throw New ArgumentNullException("database")
            If database.IsCsurLoaded Then
                Return From el In database.Extensions(XmlNamespace).Xml.<ucd>.<blocks>.<block> Select New CsurBlock(el, database.Xml)
            End If
            Return New CsurBlock() {}
        End Function
    End Module
End Namespace
