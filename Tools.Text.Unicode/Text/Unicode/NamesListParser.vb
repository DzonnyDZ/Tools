Imports System.Runtime.InteropServices
Imports Tools.TextT, Tools, Tools.ExtensionsT
Imports System.Globalization.CultureInfo
Imports System.Text.RegularExpressions
Imports <xmlns="http://www.unicode.org/ns/2003/ucd/1.0">
Imports <xmlns:nl="http://unicode.org/Public/UNIDATA/NamesList.txt">
Imports System.IO.Compression
Imports System.Runtime.CompilerServices

Namespace TextT.UnicodeT
    ''' <summary>This class performs simple and limited parsing of NamesList.txt</summary>
    ''' <remarks>
    ''' The NamesList.txt file is not specifically designed for machine parsing but it provides some unique and usefull data, such as informative aliases and cross-reference.
    ''' <para>Only static members of this class are CLS-Compliant.</para>
    ''' <note>Parsing provided by this class is not exact and complete. Only few types of records are supported. Parsing is based on regular expressions instead of having full parser.</note>
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class NamesListParser
        ''' <summary>Regex for char as defined in NamesList.html</summary>
        Private Const CHAR$ = "([0-9A-F]{4,6})"
        ''' <summary>Reges for tabs as defined in NamesList.html</summary>
        Private Const TAB$ = "(\t+)"
        ''' <summary>Regex fro name as defined in NamesList.html</summary>
        Private Const NAME$ = "([A-Z][A-Z0-9\- ]*)"
        ''' <summary>Regex for lower-case name as defined in NamesList.html</summary>
        Private Const LCNAME$ = "([a-z][a-z0-9\- ]*)"
        ''' <summary>Regex for lower-case tag as defined in NamesList.html</summary>
        Private Const LCTAG$ = "([a-z]+)"
        ''' <summary>Regex for space as defined in NamesList.html</summary>
        Private Const SP$ = "( )"
        ''' <summary>Regex for line feed as defined in NamesList.html</summary>
        Private Const LF$ = "([\r\n]+)"
        ''' <summary>Regex for label as defined in NamesList.html</summary>
        Private Const LABEL$ = "([\p{IsBasicLatin}\p{IsLatin-1Supplement}-[\p{Cc}()]]+)"
        ''' <summary>Regex for comment as defined in NamesList.html</summary>
        Private Const COMMENT$ = "((\(" & LABEL & "\)(" & SP & "\*)?)|\*)"
        ''' <summary>Regex for line as defined in NamesList.html</summary>
        Private Const LINE$ = "(" & [STRING] & LF & ")"
        ''' <summary>Regex for string as defined in NamesList.html</summary>
        Private Const STRING$ = "([\p{IsBasicLatin}\p{IsLatin-1Supplement}-[\p{Cc}]]+)"

        ''' <summary>Regex for name line as defined in NamesList.html</summary>
        Private Const NAME_LINE$ = "(?<Char>" & CHAR$ & ")" & TAB & "(" & NAME & "|(\<" & LCNAME & "\>)(" & SP & COMMENT & ")?)" & LF
        ''' <summary>Regex for reserved line as defined in NamesList.html</summary>
        Private Const RESERVED_LINE$ = "(?<Char>" & [CHAR] & ")" & TAB & "\<reserved\>" & LF
        ''' <summary>Regex for alias line as defined in NamesList.html</summary>
        Private Const ALIAS_LINE$ = TAB & "=" & SP & LINE
        ''' <summary>Regex for corss reference line as defined in NamesList.html</summary>
        Private Const CROSS_REF$ = TAB & "x" & SP & "(" &
            "((?<Char>" & [CHAR] & ")" & SP & "(" & LCNAME & "|(\<" & LCNAME & "\>)))|" &
            "(\((" & LCNAME & "|(\<" & LCNAME & "\>))" & SP & "-" & SP & "(?<Char>" & [CHAR] & ")\))|" &
            "(?<Char>" & CHAR$ & ")" &
            LF & ")"

        ''' <summary>Regex for name line as defined in NamesList.html</summary>
        Private Shared ReadOnly nameLine As New Regex("(" & NAME_LINE & ")|(" & RESERVED_LINE & ")", RegexOptions.Compiled Or RegexOptions.ExplicitCapture Or RegexOptions.CultureInvariant)
        ''' <summary>reges for alias line as defined in NamesList.html</summary>
        Private Shared ReadOnly aliasline As New Regex(ALIAS_LINE, RegexOptions.Compiled Or RegexOptions.ExplicitCapture Or RegexOptions.CultureInvariant)
        ''' <summary>regex for cross-reference line as defined in NamesList.html</summary>
        Private Shared ReadOnly crossRef As New Regex(CROSS_REF, RegexOptions.Compiled Or RegexOptions.ExplicitCapture Or RegexOptions.CultureInvariant)

        ''' <summary>Reads NamesList.txt file and raises events when supported lines are detected</summary>
        ''' <param name="reader">Reader to read NamesList.txt from</param>
        ''' <exception cref="ArgumentNullException"><paramref name="reader"/> is null</exception>
        <CLSCompliant(False)>
        Public Sub Read(reader As IO.TextReader)
            If reader Is Nothing Then Throw New ArgumentNullException("reader")
            Dim line$ = reader.ReadLine
            Dim codePoint As UInteger?
            While line IsNot Nothing
                line = line & vbCrLf
                Dim match = nameLine.Match(line)
                If match.Success Then
                    codePoint = UInteger.Parse(match.Groups!Char.Value, Globalization.NumberStyles.HexNumber, InvariantCulture)
                    RaiseEvent CodePoint(codePoint, line.Substring(0, line.Length - 2))
                ElseIf codePoint.HasValue Then
                    match = aliasline.Match(line)
                    If match.Success Then
                        RaiseEvent Alias(codePoint, line.SubstringAfter(vbTab & "= ").Trim, line.Substring(0, line.Length - 2))
                    Else
                        match = crossRef.Match(line)
                        If match.Success Then
                            RaiseEvent CrossReference(codePoint, UInteger.Parse(match.Groups!Char.Value, Globalization.NumberStyles.HexNumber, InvariantCulture), line.Substring(0, line.Length - 2))
                        End If
                    End If

                End If
                line = reader.ReadLine
            End While
        End Sub

        ''' <summary>Raised when code-point header line is reached</summary>
        ''' <param name="arg1">The code of the code-point</param>
        ''' <param name="arg2">The entire line from NamesList.txt</param>
        <CLSCompliant(False)>
        Public Event CodePoint As Action(Of UInteger, String)
        ''' <summary>Raised when informal alias line is reached</summary>
        ''' <param name="arg1">The code of the code-point (same as in preceding <see cref="CodePoint"/> event)</param>
        ''' <param name="arg2">Alias name</param>
        ''' <param name="arg3">The entire line from NamesList.txt</param>
        <CLSCompliant(False)>
        Public Event [Alias] As Action(Of UInteger, String, String)
        ''' <summary>Raised when cross-reference line is reached</summary>
        ''' <param name="arg1">The code of the code-point (same as in preceding <see cref="CodePoint"/> event)</param>
        ''' <param name="arg2">Code of related code-point</param>
        ''' <param name="arg3">The entire line from NamesList.txt</param>
        <CLSCompliant(False)>
        Public Event CrossReference As Action(Of UInteger, UInteger, String)

        ''' <summary>Parses NamesList.txt and generates an XML document thats suitable for <see cref="UnicodeCharacterDatabase.Extensions"/></summary>
        ''' <param name="reader">A reader to read NamesList.txt from</param>
        ''' <returns>A XML document with UCD-XML-like structure</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="reader"/> is null</exception>
        ''' <remarks>The structure of document si like follows:
        ''' <code lang="xml"><![CDATA[
        ''' <ucd xmlns="http://www.unicode.org/ns/2003/ucd/1.0" xmlns:nl="http://unicode.org/Public/UNIDATA/NamesList.txt">
        '''     <repertoire>
        '''         <char cp="0000" nl:cross-ref="0000 0001 0002">
        '''             <nl:alias>Alias 1</nl>
        '''             <nl:alias>Alias 2</nl>
        '''         </char>
        '''     </repertoire>
        ''' </ucd>
        ''' ]]></code>
        ''' The document can contain multiple &lt;char> elements, each corresponding to one code-point in UCD.
        ''' <note>This parser always generates &lt;char> elements even for non-characters.</note>
        ''' Each &lt;char> element has the @cp attribute which identifies the code-point and allows it to be matched to main UCD XML.
        ''' Optional @nl:cross-ref attributte contains whitespace-separated list of referenced codepoints.
        ''' Optionally &lt;nl:alias> elements inside &lt;char> define informative name aliases for the character. &lt;char> cahn have zero or more aliases.
        ''' <para>Resulting XML can be passed to <see cref="UnicodeCharacterDatabase"/> CTor</para>
        ''' </remarks>
        Public Shared Function MakeExtensionXml(reader As IO.TextReader) As XDocument
            Dim parser As New NamesListParser

            Dim cp As XElement = Nothing
            Dim setAttr As Boolean = False
            Dim ret = <?xml version="1.0"?><ucd><repertoire/></ucd>
            Dim repertoire = ret.<ucd>.<repertoire>.Single

            AddHandler parser.CodePoint,
                Sub(codePoint, line)
                    If setAttr Then repertoire.Add(cp)
                    cp = <char cp=<%= codePoint.ToString("X4", InvariantCulture) %>/>
                    setAttr = False
                End Sub
            AddHandler parser.Alias,
                Sub(codePoint, [alias], line)
                    cp.Add(<nl:alias><%= [alias] %></nl:alias>)
                    setAttr = True
                End Sub
            AddHandler parser.CrossReference,
                Sub(codePoint, reference, line)
                    If cp.@<nl:cross-ref> Is Nothing Then
                        cp.@<nl:cross-ref> = reference.ToString("X4", InvariantCulture)
                    Else
                        cp.@<nl:cross-ref> &= " " & reference.ToString("X4", InvariantCulture)
                    End If
                    setAttr = True
                End Sub
            parser.Read(reader)
            Return ret
        End Function


    End Class

    ''' <summary>Provides extension method for Unicode Character Dataabase related to properties extracted from NamesList.txt</summary>
    Public Module NamesListExtensions
        ''' <summary>An XML namespace used for NamesList UCD extension</summary>
        Public Const XmlNamespace$ = "http://unicode.org/Public/UNIDATA/NamesList.txt"
        ''' <summary>Name of embedded resource in this assembly that contains default copy of UCD NamesList.txt file</summary>
        ''' <remarks>This resources is GZipped.See http://unicode.org/Public/UNIDATA/NamesList.html for details about the file. Lates file can be downloaded from http://unicode.org/Public/UNIDATA/NamesList.txt.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Private Const ResourceName As String = "Tools.TextT.UnicodeT.NamesList.txt.gz"

        Private _default As UnicodeCharacterDatabase
        Private ReadOnly defaultLock As New Object

        ''' <summary>Gets default instance of NamesAliases extension</summary>
        ''' <remarks>Parses default instance of NamesList.txt that is embedded in this assembly as embeded resource named <see cref="ResourceName"/>. Note: This resources is GZipped.</remarks>
        ''' <threadsafety>This property is thread-safe</threadsafety>
        Public ReadOnly Property [DefaultNamesList]() As UnicodeCharacterDatabase
            Get
                If _default Is Nothing Then
                    SyncLock defaultLock
                        If _default Is Nothing Then
                            Using compressedStream = GetType(NamesListParser).Assembly.GetManifestResourceStream(ResourceName),
                                  decompressedStream = New GZipStream(compressedStream, CompressionMode.Decompress),
                                  reader = New IO.StreamReader(decompressedStream, System.Text.Encoding.GetEncoding("Latin1"))
                                _default = New UnicodeCharacterDatabase(NamesListParser.MakeExtensionXml(reader))
                            End Using
                        End If
                    End SyncLock
                End If
                Return _default
            End Get
        End Property

        ''' <summary>gets value indicating if NamesList extension is loaded into given UCD</summary>
        ''' <param name="ucd">UCD to check</param>
        ''' <returns>True if NsmesList XML extension is loaded, false if it is not</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="ucd"/> is null</exception>
        <Extension()>
        Public Function IsNamesListLLoaded(ucd As UnicodeCharacterDatabase) As Boolean
            If ucd Is Nothing Then Throw New ArgumentNullException("ucd")
            Return ucd.Extensions.ContainsKey(XmlNamespace)
        End Function

        ''' <summary>Loads NamesList extension to given UCD</summary>
        ''' <param name="target">UCD to load NamesList to</param>
        ''' <param name="source">A reader to read NamesList extension to (the file must be in NamesList.txt format - see <see cref="NamesListParser"/>)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="target"/> or <paramref name="source"/> is null</exception>
        ''' <exception cref="InvalidOperationException"><paramref name="target"/> is already loaded with NamesList extension</exception>
        <Extension()>
        Public Sub LoadNamesList(target As UnicodeCharacterDatabase, source As IO.TextReader)
            If target Is Nothing Then Throw New ArgumentNullException("target")
            If source Is Nothing Then Throw New ArgumentNullException("source")
            If target.IsNamesListLLoaded Then Throw New InvalidOperationException("NamesList extension is already loaded")
            target.Extensions.Add(XmlNamespace, New UnicodeCharacterDatabase(NamesListParser.MakeExtensionXml(source)))
        End Sub

        ''' <summary>Loads default NamesList extension to given UCD</summary>
        ''' <param name="target">UCD to load NamesList to</param>
        ''' <exception cref="ArgumentNullException"><paramref name="target"/> is null</exception>
        ''' <exception cref="InvalidOperationException"><paramref name="target"/> is already loaded with NamesList extension</exception>
        <Extension()>
        Public Sub LoadNamesList(target As UnicodeCharacterDatabase)
            If target Is Nothing Then Throw New ArgumentNullException("target")
            If target.IsNamesListLLoaded Then Throw New InvalidOperationException("NamesList extension is already loaded")
            target.Extensions.Add(XmlNamespace, [DefaultNamesList])
        End Sub

        ''' <summary>Gets informative name aliases for given code point</summary>
        ''' <param name="cp">A code point to get aliases for</param>
        ''' <returns>Array of aliases. An empty array if this code point does not have any aliases. Null if NamesList extension is n ot loaded or <paramref name="cp"/> does not represent single code point.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="cp"/> is null</exception>
        <Extension()>
        Public Function Aliases(cp As UnicodeCodePoint) As String()
            If cp Is Nothing Then Throw New ArgumentNullException("cp")
            If Not cp.CodePoint.HasValue Then Return Nothing
            Dim ext = cp.GetExtension(XmlNamespace)
            If ext Is Nothing Then Return Nothing
            Dim extcp = ext.FindCodePoint(cp.CodePoint.Value)
            If extcp Is Nothing Then Return New String() {}
            Return (From el In extcp.Element.<nl:alias> Select el.Value).ToArray
        End Function

        ''' <summary>Gets corss-references of given code point</summary>
        ''' <param name="cp">A code point to gete cross-references for</param>
        ''' <returns>An array of cross references. Null if <paramref name="cp"/> does not repersent single code point. An empty array if no cross reefernces were found (i.e. the code point has not cross-references or NamesList extension is not loaded for current UCD)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="cp"/> is nul</exception>
        <Extension()>
        Public Function CrossReferences(cp As UnicodeCodePoint) As CodePointInfo()
            If cp Is Nothing Then Throw New ArgumentNullException("cp")
            If Not cp.CodePoint.HasValue Then Return Nothing
            Dim val = cp.GetPropertyValue(XmlNamespace, "cross-ref", False)
            If val = "" Then Return New CodePointInfo() {}
            Return From str In val.WhiteSpaceSplit Select New CodePointInfo(cp.Element.Document, UInteger.Parse(str, Globalization.NumberStyles.HexNumber, InvariantCulture))
        End Function

    End Module
End Namespace