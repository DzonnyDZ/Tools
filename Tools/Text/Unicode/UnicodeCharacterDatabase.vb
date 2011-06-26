Imports System.Xml.Linq
Imports Tools.RuntimeT.CompilerServicesT
Imports Tools.TextT.UnicodeT
Imports Tools.ExtensionsT

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
    Public Class UnicodeCharacterDatabase
        ''' <summary>Gets name of assembly resource that contains Unicode Character Database XML</summary>
        ''' <remarks>
        ''' <para>The resource is contained in Tools.dll assembly (same assembly as <see cref="UnicodeCharacterDatabase"/> class).</para>
        ''' <para>Because of size of Unicode Character Database XML this resource is GZipped (use <see cref="IO.Compression.GZipStream"/> to unGZip it).</para>
        ''' <para>Grouped version of Unicode Character Database XML is used.</para>
        ''' <para>This resource is not embedded, it's linked. It means that you must distribute file this resource points to with Tools.dll assembly to be able to access it.</para>
        ''' </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Const UnicodeXmlDatabaseResourceName As String = "Tools.TextT.UnicodeT.ucd.all.grouped.xml.gz"
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
                Dim ret = GetXmlAlternative
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
    End Class
End Namespace
#End If