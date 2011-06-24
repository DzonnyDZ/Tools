Imports System.Xml.Linq
Imports Tools.RuntimeT.CompilerServicesT

#If Config <= Nightly Then
<Module: AddResource("Tools.TextT.UnicodeT.ucd.all.grouped.xml.gz", "ucd.all.grouped.xml.gz", False, False, Remove:=True)> 

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
    Public Class UnicodeCharacterDatabase

        ''' <summary>Gets Unicode Character Database in XML format</summary>
        ''' <returns>Content of linked resource <c>Tools.TextT.UnicodeT.ucd.all.grouped.xml.gz</c> (file ucd.all.grouped.xml.gz) as <see cref="XDocument"/>.</returns>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shared Function GetXml() As XDocument
            Using resourceStream = GetType(UnicodeCharacterDatabase).Assembly.GetManifestResourceStream("Tools.TextT.UnicodeT.ucd.all.grouped.xml.gz"),
                  decompressedStream As New IO.Compression.GZipStream(resourceStream, IO.Compression.CompressionMode.Decompress)
                Return XDocument.Load(decompressedStream)
            End Using
        End Function
    End Class
End Namespace
#End If