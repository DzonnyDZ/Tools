Imports System.Reflection
Imports Tools.ExtensionsT, Tools.ReflectionT
Imports System.Resources
Imports System.Globalization
Imports System.IO.Compression

Namespace TextT.UnicodeT

    ''' <summary>A base class for localization providers to provide localized (translated) data for Unicode Character Database</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public MustInherit Class UcdLocalizationProvider
        ''' <summary>Gets default UCD locallization provider that uses built-in localizations</summary>
        Public Shared ReadOnly Property [Default] As UcdLocalizationProvider
            Get
                Return New ResourceUcdLocalizationProvider(resourceName, GetType(UcdLocalizationProvider), AddressOf UnGZipResource)
            End Get
        End Property

        ''' <summary>Name of default resource that contains localized character names for UCD</summary>
        Private Const ResourceName$ = "Tools.TextT.UnicodeT.ucd.xml.gz"

        ''' <summary>When overriden in derived class gets value indivating if localized document for given culture exists</summary>
        ''' <param name="culture">Name of culture to check</param>
        ''' <returns>True if document for given culture exists, false if it does not exist. Only exact culture given in <paramref name="culture"/> is tested. No parent cultureds are considered.</returns>
        Public MustOverride Function DocumentExists(culture$) As Boolean

        ''' <summary>When overriden in derived class gets an XML document in UCD localization format that contains localized values for given culture</summary>
        ''' <param name="culture">Culture to get document for</param>
        ''' <returns>An XML document in UCD XML localization format that contains localization data for given culture. Null if localization data for given culture do not exist. No parent cultures are considered. Only data for exactly requested culture are returned (if available).</returns>
        ''' <remarks>
        ''' Formal of localization XML document is basically same as general UCD XML format + extensions. But only some data are used for localization. Currently character names (via aliases) and block names can be localized.
        ''' The structure of the document is following:
        ''' <code lang="XML"><![CDATA[
        ''' <ucd xml:lang="cs" xmlns:nl="http://unicode.org/Public/UNIDATA/NamesList.txt#loc" xmlns="http://www.unicode.org/ns/2003/ucd/1.0">
        '''     <repertoire>
        '''         <char cp="000E">
        '''             <nl:alias>Posun ven</nl:alias>
        '''         </char>
        '''     </repertoire>
        '''     <blocks>
        '''         <block first-cp="1F200" last-cp="1F2FF" name="Uzavřené ideografy, dodatek" />
        '''     </blocks>
        ''' </ucd>
        ''' ]]></code>
        ''' <list type="table">
        ''' <item><term><c>ucd/@xml:lang</c></term><description>Indicates document lnaguage. Just informative value. Not checked.</description></item>
        ''' <item><term><c>char/@cp</c></term><description>Identifies character which name will be localized</description></item>
        ''' <item><term><c>char/nl:alias</c></term><description>Localized name of character. Unlike in NamesList extension, only one (1st) &lt;nl:alias> is used.</description></item>
        ''' <item><term><c>block/@first-cp</c> and <c>block/@last-cp</c></term><description>Identify block which name will be localized.</description></item>
        ''' <item><term><c>block/@name</c></term><description>Localized block name</description></item>
        ''' </list>
        ''' Only attributtes and elements listed above are currently used by UCD localizations. Other attributtes and elements are ignored.
        ''' It's not possible to localize name of multi-codepoint code-point.
        ''' Use only &lt;char> element to localize names of code-points (even for non-characters etc.).
        ''' Both sections - &lt;repertoire> and &lt;blocks> are optional.
        ''' It!s not necessary to provide localized names of all code-points and blocks.
        ''' </remarks>
        Public MustOverride Function GetDocument(culture$) As XDocument

        ''' <summary>When overriden in derived class gets array of cultures available</summary>
        ''' <returns>Arra of names of cultures available for localization.</returns>
        ''' <remarks><note>Localization of particular culture can be just partial. In extreme case it can be empty or contain just few code-points or blocks.</note></remarks>
        Public MustOverride Function GetCulturesSupported() As String()

        ''' <summary>Helper method that can be passed to <see cref="ResourceUcdLocalizationProvider"/> or <see cref="FileUcdLocalizationProvider"/> CTor to decompress GZipped stream</summary>
        ''' <param name="gzipped">A GZipped stream</param>
        ''' <returns>Decompressed stream</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="gzipped"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="gzipped"/>.<see cref="IO.Stream.CanRead">CanRead</see> is false</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shared Function UnGZipResource(gzipped As IO.Stream) As IO.Stream
            If gzipped Is Nothing Then Throw New ArgumentNullException("gzipped")
            Return New GZipStream(gzipped, CompressionMode.Decompress)
        End Function
    End Class

    ''' <summary>Implements resource-based <see cref="UcdLocalizationProvider"/>.</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class ResourceUcdLocalizationProvider
        Inherits UcdLocalizationProvider

        Private ReadOnly _assembly As Assembly
        Private ReadOnly _resourceName As String
        Private ReadOnly _streamTransformer As Func(Of IO.Stream, IO.Stream)

        ''' <summary>CTor - creates a new instance of the <see cref="ResourceUcdLocalizationProvider"/> from assembly</summary>
        ''' <param name="resourceName">Name of resource that contains an XML codument with UCD localization data</param>
        ''' <param name="assembly">An assembly that contains the resource</param>
        ''' <param name="streamTransformer">Optional function that transforms assembly manifest resource stream to another stream (e.g. decompresses it)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="resourceName"/> is null or <paramref name="assembly"/> is null</exception>
        Public Sub New(resourceName As String, assembly As Assembly, Optional streamTransformer As Func(Of IO.Stream, IO.Stream) = Nothing)
            If resourceName Is Nothing Then Throw New ArgumentNullException("resourceName")
            If assembly Is Nothing Then Throw New ArgumentNullException("assembly")
            _assembly = assembly
            _resourceName = resourceName
            _streamTransformer = If(streamTransformer, Function(s) s)
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="ResourceUcdLocalizationProvider"/> from type</summary>
        ''' <param name="resourceName">Name of resource that contains an XML codument with UCD localization data</param>
        ''' <param name="typeFromAssembly">Any type form an assembly that contains the resource</param>
        ''' <param name="streamTransformer">Optional function that transforms assembly manifest resource stream to another stream (e.g. decompresses it)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="resourceName"/> is null or <paramref name="typeFromAssembly"/> is null</exception>
        Public Sub New(resourceName As String, typeFromAssembly As Type, Optional streamTransformer As Func(Of IO.Stream, IO.Stream) = Nothing)
            Me.New(resourceName, typeFromAssembly.ThrowIfNull("typeFromAssembly").Assembly, streamTransformer)
        End Sub

        ''' <summary>Gets value indivating if localized document for given culture exists</summary>
        ''' <param name="culture">Name of culture to check</param>
        ''' <returns>True if document for given culture exists, false if it does not exist. Only exact culture given in <paramref name="culture"/> is tested. No parent cultureds are considered.</returns>
        Public Overrides Function DocumentExists(culture As String) As Boolean
            Dim ci As CultureInfo
            Try
                ci = CultureInfo.GetCultureInfo(culture)
            Catch ex As ArgumentException
                Return False
            End Try
            If ci Is Nothing Then Return False
            Dim satelliteAssembly As Assembly
            Try
                satelliteAssembly = Assembly.GetSatelliteAssembly(ci)
            Catch ex As IO.FileNotFoundException
                Return False
            End Try
            If satelliteAssembly Is Nothing Then Return Nothing
            Return satelliteAssembly.GetManifestResourceNames.Contains(ResourceName)
        End Function

        ''' <summary>Gets array of cultures available</summary>
        ''' <returns>Arra of names of cultures available for localization.</returns>
        ''' <remarks><note>Localization of particular culture can be just partial. In extreme case it can be empty or contain just few code-points or blocks.</note></remarks>
        Public Overrides Function GetCulturesSupported() As String()
            Dim ret As New List(Of String)
            For Each fld In IO.Directory.EnumerateDirectories(IO.Path.GetDirectoryName(Assembly.Location))
                If IO.File.Exists(IO.Path.Combine(fld, String.Format("{0}.resources.dll", Assembly.GetName().Name))) Then
                    If DocumentExists(IO.Path.GetFileName(fld)) Then ret.Add(fld)
                End If
            Next
            Return ret.ToArray
        End Function

        ''' <summary>Gets an XML document in UCD localization format that contains localized values for given culture</summary>
        ''' <param name="culture">Culture to get document for</param>
        ''' <returns>An XML document in UCD XML localization format that contains localization data for given culture. Null if localization data for given culture do not exist. No parent cultures are considered. Only data for exactly requested culture are returned (if available).</returns>
        Public Overrides Function GetDocument(culture As String) As System.Xml.Linq.XDocument
            Dim ci As CultureInfo
            Try
                ci = CultureInfo.GetCultureInfo(culture)
            Catch ex As ArgumentException
                Return Nothing
            End Try
            If ci Is Nothing Then Return Nothing
            Dim satelliteAssembly As Assembly
            Try
                satelliteAssembly = Assembly.GetSatelliteAssembly(ci)
            Catch ex As IO.FileNotFoundException
                Return Nothing
            End Try
            If satelliteAssembly Is Nothing Then Return Nothing
            Try
                Using resStream = satelliteAssembly.GetManifestResourceStream(ResourceName)
                    If resStream Is Nothing Then Return Nothing
                    Using str2 = StreamTransformer(resStream)
                        Return XDocument.Load(str2)
                    End Using
                End Using
            Catch ex As IO.FileNotFoundException
                Return Nothing
            End Try
        End Function

        ''' <summary>Gets an assembly that contains resource for UCD localization</summary>
        Public ReadOnly Property [Assembly]() As Assembly
            Get
                Return _assembly
            End Get
        End Property

        ''' <summary>Gets name of resource from <see cref="Assembly"/> that contains data for UCD localization</summary>
        Public ReadOnly Property ResourceName() As String
            Get
                Return _resourceName
            End Get
        End Property

        ''' <summary>Gets stream transformation function to be used on assembly manifest resource stream before XML docume is read from it</summary>
        Protected ReadOnly Property StreamTransformer() As Func(Of IO.Stream, IO.Stream)
            Get
                Return _streamTransformer
            End Get
        End Property
    End Class

    ''' <summary>Implements file-based <see cref="UcdLocalizationProvider"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class FileUcdLocalizationProvider
        Inherits UcdLocalizationProvider
        Private ReadOnly _baseName As String
        Private ReadOnly _useSubfolders As String
        Private ReadOnly _streamTransformer As Func(Of IO.Stream, IO.Stream)

        ''' <summary>CTor - creates a new instance of the <see cref="FileUcdLocalizationProvider"/> class</summary>
        ''' <param name="baseName">Name of non-localized (or default localization) file (XML document). This can be file with default names, an empty XML file or the file does not have to exist at all.</param>
        ''' <param name="useSubfolders">
        ''' True to look for localized resources in paths like {baseFolder}\{culture}\{baseName}.
        ''' False to look for localized resources in paths like {baseFolder}\{baseNameWithoutExtension}.{culture}.{baseExtension}.
        ''' </param>
        ''' <param name="streamTransformer">Optional stream transformation. Transforms a file stream before XML document is loaded from it (e.g. decompresses it).</param>
        ''' <exception cref="ArgumentNullException"><paramref name="streamTransformer"/> is null</exception>
        Public Sub New(baseName$, useSubfolders As Boolean, Optional streamTransformer As Func(Of IO.Stream, IO.Stream) = Nothing)
            If baseName Is Nothing Then Throw New ArgumentNullException("baseName")
            _baseName = baseName
            _useSubfolders = useSubfolders
            _streamTransformer = If(streamTransformer, Function(s) s)
        End Sub

        ''' <summary>Gets path fo file to load localized data from</summary>
        ''' <param name="culture">Culture to generate file path for</param>
        ''' <returns>Path of file that contains localized data for given culture (in case it exists).</returns>
        Protected Overridable Function GetFilePath(culture As String) As String
            If culture = "" Then Return BaseName
            If UseSubfolders Then
                Return IO.Path.Combine(IO.Path.GetDirectoryName(BaseName), culture, IO.Path.GetFileName(BaseName))
            Else
                Return IO.Path.Combine(IO.Path.GetDirectoryName(BaseName), String.Format("{0}.{1}{3}", IO.Path.GetFileNameWithoutExtension(BaseName), culture, IO.Path.GetExtension(BaseName)))
            End If
        End Function

        ''' <summary>Gets value indivating if localized document for given culture exists</summary>
        ''' <param name="culture">Name of culture to check</param>
        ''' <returns>True if document for given culture exists, false if it does not exist. Only exact culture given in <paramref name="culture"/> is tested. No parent cultureds are considered.</returns>
        Public Overrides Function DocumentExists(culture As String) As Boolean
            Return IO.File.Exists(GetFilePath(culture))
        End Function

        ''' <summary>Gets array of cultures available</summary>
        ''' <returns>Arra of names of cultures available for localization.</returns>
        ''' <remarks><note>Localization of particular culture can be just partial. In extreme case it can be empty or contain just few code-points or blocks.</note></remarks>
        Public Overrides Function GetCulturesSupported() As String()
            Dim ret As New List(Of String)
            If UseSubfolders Then
                For Each subfolder In IO.Directory.EnumerateDirectories(IO.Path.GetDirectoryName(BaseName))
                    If IO.File.Exists(IO.Path.Combine(subfolder, IO.Path.GetFileName(BaseName))) Then ret.Add(IO.Path.GetFileName(subfolder))
                Next
            Else
                For Each file In IO.Directory.EnumerateFiles(IO.Path.GetDirectoryName(BaseName), String.Format("{0}.*{1}", IO.Path.GetFileNameWithoutExtension(BaseName), IO.Path.GetExtension(BaseName)))
                    ret.Add(If(IO.Path.GetExtension(BaseName) = "", IO.Path.GetExtension(file), IO.Path.GetExtension(IO.Path.GetFileNameWithoutExtension(file)).Substring(1)))
                Next
            End If
            Return ret.ToArray
        End Function

        ''' <summary>When overriden in derived class gets an XML document in UCD localization format that contains localized values for given culture</summary>
        ''' <param name="culture">Culture to get document for</param>
        ''' <returns>An XML document in UCD XML localization format that contains localization data for given culture. Null if localization data for given culture do not exist. No parent cultures are considered. Only data for exactly requested culture are returned (if available).</returns>
        Public Overrides Function GetDocument(culture As String) As System.Xml.Linq.XDocument
            If Not DocumentExists(culture) Then Return Nothing
            Using baseStream = IO.File.Open(GetFilePath(culture), IO.FileMode.Open, IO.FileAccess.Read),
                  stream2 = StreamTransformer(baseStream)
                Return XDocument.Load(stream2)
            End Using
        End Function

        ''' <summary>Gets name of file thats used as base name of all localized resources</summary>
        ''' <remarks>This file may contain default values, can be an empty XML file or may not even exist. This value is used to determine folder, file name and extension for localized files.</remarks>
        Public ReadOnly Property BaseName() As String
            Get
                Return _baseName
            End Get
        End Property
        ''' <summary>Gets value indicating if localized files are stored in subfolders or along with main file.</summary>
        ''' <returns>True if subfolders are used, false otherwise</returns>
        ''' <remarks>If subfobfolders aree used localized file path looks like: {baseFolder}\{culture}\{baseName}; otherwise it looks like: {baseFolder}\{baseNameWithoutExtension}.{culture}.{baseExtension}.</remarks>
        Public ReadOnly Property UseSubfolders() As String
            Get
                Return _useSubfolders
            End Get
        End Property

        ''' <summary>Gets stream transformation function to be used on assembly manifest resource stream before XML docume is read from it</summary>
        Protected ReadOnly Property StreamTransformer() As Func(Of IO.Stream, IO.Stream)
            Get
                Return _streamTransformer
            End Get
        End Property
    End Class

End Namespace