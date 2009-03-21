Imports <xmlns:is="http://dzonny.cz/xml/schemas/intellisense">, <xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
Imports System.Xml.Linq, System.Xml.Xsl, System.Xml
''' <summary>Implements the CommentsMerge tool which merges 2 files of XML comments into one</summary>
Friend Module CommentsMerge
    ''' <summary>Entry point. Reads command line arguments and performs operation</summary>
    Public Sub Main()
        If My.Application.CommandLineArgs.Count < 3 Then
            Console.WriteLine(My.Resources.Usage)
            Return
        End If
        Console.WriteLine("{0} {1} {2}", My.Application.Info.Title, My.Application.Info.Version, My.Application.Info.Copyright)
        Dim Pri As XDocument
        Try
            Pri = XDocument.Load(My.Application.CommandLineArgs(0))
        Catch ex As Exception
            Console.Error.WriteLine(My.Resources.PrimaryFile01, ex.GetType.Name, ex.Message)
            Environment.Exit(1)
            Exit Sub
        End Try
        Dim mg As XDocument = Nothing
        For i As Integer = 1 To My.Application.CommandLineArgs.Count - 2
            Dim Sec As XDocument
            Console.WriteLine(My.Application.CommandLineArgs(i))
            Try
                Sec = XDocument.Load(My.Application.CommandLineArgs(i))
            Catch ex As Exception
                Console.Error.WriteLine(My.Resources.SecondaryFile01, ex.GetType.Name, ex.Message)
                Environment.Exit(2)
                Exit Sub
            End Try
            Try
                mg = Merge(Pri, Sec)
            Catch ex As Exception
                Console.Error.WriteLine(My.Resources.Transformation01, ex.GetType.Name, ex.Message)
                Environment.Exit(3)
                Exit Sub
            End Try
        Next
        Try
            mg.Save(My.Application.CommandLineArgs(My.Application.CommandLineArgs.Count - 1))
        Catch ex As Exception
            Console.Error.WriteLine(My.Resources.Save01, ex.GetType.Name, ex.Message)
            Environment.Exit(4)
            Exit Sub
        End Try
    End Sub

    ''' <summary>Merges 2 XML comments files together</summary>
    ''' <param name="Pri">Primary file - the file to merge to</param>
    ''' <param name="Sec">Secondary file - the file to be imbdeded to <paramref name="Pri"/></param>
    ''' <remarks>
    ''' Each file should be either namespace-less or had namespace http://dzonny.cz/xml/schemas/intellisense.
    ''' Resulting file will have either no or http://dzonny.cz/xml/schemas/intellisense namespace depending on namespace of <paramref name="Pri"/>
    ''' </remarks>
    ''' <returns><paramref name="Pri"/> with /doc/members/member from <paramref name="Sec"/> placed into its /doc/members</returns>
    Public Function Merge(ByVal Pri As XDocument, ByVal Sec As XDocument) As XDocument
        Pri = New XDocument(Pri)
        Dim Import = Sec.<doc>.<members>.<member>.Union(Sec.<is:doc>.<is:members>.<is:member>)
        Dim Import2 As IEnumerable(Of XElement)
        If Pri.Root.Name.NamespaceName = "http://dzonny.cz/xml/schemas/intellisense" Then
            Import2 = Transform(Import, <?xml version="1.0" encoding="utf-8"?>
                                        <xsl:stylesheet version="1.0">
                                            <xsl:output method="xml" indent="yes"/>
                                            <xsl:template match="@* | node()">
                                                <xsl:copy>
                                                    <xsl:apply-templates select="@* | node()"/>
                                                </xsl:copy>
                                            </xsl:template>
                                            <xsl:template match="is:*">
                                                <xsl:element name="{local-name()}">
                                                    <xsl:apply-templates select="@* | node()"/>
                                                </xsl:element>
                                            </xsl:template>
                                            <xsl:template match="@is:*">
                                                <xsl:attribute name="{local-name()}">
                                                    <xsl:value-of select="."/>
                                                </xsl:attribute>
                                            </xsl:template>
                                        </xsl:stylesheet>)
            Pri.<is:doc>.<is:members>.First.Add(Import2)
        Else
            Import2 = Transform(Import, <?xml version="1.0" encoding="utf-8"?>
                                        <xsl:stylesheet version="1.0">
                                            <xsl:output method="xml" indent="yes"/>
                                            <xsl:template match="@* | node()">
                                                <xsl:copy>
                                                    <xsl:apply-templates select="@* | node()"/>
                                                </xsl:copy>
                                            </xsl:template>
                                            <xsl:template match="*[name()=local-name()]">
                                                <xsl:element name="{local-name()}">
                                                    <xsl:apply-templates select="@* | node()"/>
                                                </xsl:element>
                                            </xsl:template>
                                        </xsl:stylesheet>)
            Pri.<doc>.<members>.First.Add(Import2)
        End If
        Return Pri
    End Function
    ''' <summary>Performs XSLT transformation</summary>
    ''' <param name="Nodes">Nodes to be transformed</param>
    ''' <param name="XSLT">Text of XSLT sheet</param>
    ''' <returns>Elements from root element of result of transformation</returns>
    ''' <remarks>The sheet will be given the nodes form <paramref name="Nodes"/> enclosed in namespace-less element &lt;fake></remarks>
    Private Function Transform(ByVal Nodes As IEnumerable(Of XElement), ByVal XSLT As XDocument) As IEnumerable(Of XElement)
        Dim doc = <?xml version="1.0"?><fake><%= Nodes %></fake>
        Dim t As New XslCompiledTransform
        t.Load(XSLT.CreateReader) 'XmlReader.Create(New IO.StringReader(XSLT)))
        Dim doc2 As New XDocument
        Using w = doc2.CreateWriter
            t.Transform(doc.CreateReader, w)
        End Using
        Return (doc2.Root.Elements)
    End Function

    'Private Class XmlDocumentWriter
    '    Inherits XmlWriter

    '    Private doc As New XDocument
    '    Dim Current As Object

    '    Public ReadOnly Property Document() As XDocument
    '        Get
    '            Return doc
    '        End Get
    '    End Property

    '    Public Overrides Sub Close()
    '        Current = Nothing
    '    End Sub

    '    Public Overrides Sub Flush()
    '        'Do nothing
    '    End Sub

    '    Public Overrides Function LookupPrefix(ByVal ns As String) As String
    '        Return LookupPrefix(ns, Current)
    '    End Function

    '    Private Overloads Function LookupPrefix(ByVal ns$, ByVal Child As XNode) As String
    '        If TypeOf Child Is XElement Then
    '            With DirectCast(Child, XElement)
    '                If .Name.Namespace = ns Then Return .Name.NamespaceName
    '            End With
    '        End If
    '        If Child.Parent IsNot Nothing Then Return LookupPrefix(ns, Child.Parent)
    '        Return Nothing
    '    End Function

    '    Public Overrides Sub WriteBase64(ByVal buffer() As Byte, ByVal index As Integer, ByVal count As Integer)
    '        WriteString(Convert.ToBase64String(buffer, index, count, Base64FormattingOptions.None))
    '    End Sub

    '    Public Overrides Sub WriteCData(ByVal text As String)
    '        If TypeOf Current Is XElement Then
    '            DirectCast(Current, XElement).Add(New XCData(text))
    '        Else : Throw New ArgumentException("CData not allowed at level of " & LevelName(Current))
    '        End If
    '    End Sub

    '    Public Overrides Sub WriteComment(ByVal text As String)
    '        If TypeOf Current Is XContainer Then
    '            DirectCast(Current, XContainer).Add(New XComment(text))
    '        Else : Throw New ArgumentException("Comments are not allowed at level of " & LevelName(Current))
    '        End If
    '    End Sub

    '    Private Shared Function LevelName(ByVal node As Object) As String
    '        If node Is Nothing Then Return "nothing"
    '        If TypeOf node Is XComment Then
    '            Return "comment"
    '        ElseIf TypeOf node Is XDocument Then
    '            Return "document"
    '        ElseIf TypeOf node Is XElement Then
    '            Return "element"
    '        ElseIf TypeOf node Is XDocumentType Then
    '            Return "doctype"
    '        ElseIf TypeOf node Is XProcessingInstruction Then
    '            Return "processing instruction"
    '        ElseIf TypeOf node Is XCData Then
    '            Return "CDate"
    '        ElseIf TypeOf node Is XText Then
    '            Return "text"
    '        ElseIf TypeOf node Is CAttribute Then
    '            Return "attribute"
    '        Else
    '            Return "node"
    '        End If
    '    End Function

    '    Public Overrides Sub WriteDocType(ByVal name As String, ByVal pubid As String, ByVal sysid As String, ByVal subset As String)
    '        Static Writtent As Boolean = False
    '        If TypeOf Current Is XDocument Then
    '            If Writtent Then Throw New InvalidOperationException("Doctype was already written")
    '            With DirectCast(Current, XDocument)
    '                .DocumentType.Name = name
    '                .DocumentType.PublicId = pubid
    '                .DocumentType.SystemId = sysid
    '                .DocumentType.InternalSubset = subset
    '            End With
    '            Writtent = True
    '        Else : Throw New ArgumentException("Doctype is not allowed at level of " & LevelName(Current))
    '        End If
    '    End Sub

    '    Public Overrides Sub WriteEndAttribute()
    '        If TypeOf Current Is CAttribute Then
    '            Current = DirectCast(Current, CAttribute).Parent
    '        Else : Throw New InvalidOperationException("Cannot call WriteEndAttribute when current node is " & LevelName(Current))
    '        End If
    '    End Sub

    '    Public Overrides Sub WriteEndDocument()
    '        Close()
    '    End Sub

    '    Public Overrides Sub WriteEndElement()
    '        If TypeOf Current Is XElement Then
    '            Current = DirectCast(Current, XElement).Parent
    '        Else : Throw New InvalidOperationException("Cannot call WriteEndDocument when current node is " & LevelName(Current))
    '        End If
    '    End Sub

    '    Public Overrides Sub WriteEntityRef(ByVal name As String)

    '    End Sub

    '    Public Overrides Sub WriteFullEndElement()

    '    End Sub

    '    Public Overrides Sub WriteCharEntity(ByVal ch As Char)

    '    End Sub

    '    Public Overrides Sub WriteChars(ByVal buffer() As Char, ByVal index As Integer, ByVal count As Integer)
    '        WriteString(New String(buffer, index, count))
    '    End Sub

    '    Public Overrides Sub WriteProcessingInstruction(ByVal name As String, ByVal text As String)
    '        If TypeOf Current Is XContainer Then
    '            DirectCast(Current, XContainer).Add(New XProcessingInstruction(name, text))
    '        Else : Throw New ArgumentException("Processing instructions are not allowed at level of " & LevelName(Current))
    '        End If
    '    End Sub

    '    Public Overloads Overrides Sub WriteRaw(ByVal buffer() As Char, ByVal index As Integer, ByVal count As Integer)

    '    End Sub

    '    Public Overloads Overrides Sub WriteRaw(ByVal data As String)

    '    End Sub

    '    Public Overloads Overrides Sub WriteStartAttribute(ByVal prefix As String, ByVal localName As String, ByVal ns As String)
    '        If String.IsNullOrEmpty(localName) Then Throw New ArgumentNullException("localName")
    '        If TypeOf Current Is XElement Then
    '            Current = New CAttribute(prefix, localName, ns, Current)
    '        Else : Throw New ArgumentException("Attribute is not allowed at level of " & LevelName(Current))
    '        End If
    '    End Sub

    '    Private Class CAttribute
    '        Public Sub New(ByVal Prefix$, ByVal LocalName$, ByVal Namespace$, ByVal Parent As XElement)
    '            Me.Prefix = Prefix
    '            Me.LocalName = LocalName
    '            Me.Namespace = [Namespace]
    '            Me.Parent = Parent
    '        End Sub
    '        Public ReadOnly Prefix$
    '        Public ReadOnly LocalName$
    '        Public ReadOnly Namespace$
    '        Public ReadOnly Parent As XElement
    '    End Class

    '    Public Overloads Overrides Sub WriteStartDocument()
    '        Current = doc
    '    End Sub

    '    Public Overloads Overrides Sub WriteStartDocument(ByVal standalone As Boolean)
    '        doc.CreateWriter()
    '    End Sub

    '    Public Overloads Overrides Sub WriteStartElement(ByVal prefix As String, ByVal localName As String, ByVal ns As String)

    '    End Sub

    '    Public Overrides ReadOnly Property WriteState() As System.Xml.WriteState
    '        Get

    '        End Get
    '    End Property

    '    Public Overrides Sub WriteString(ByVal text As String)

    '    End Sub

    '    Public Overrides Sub WriteSurrogateCharEntity(ByVal lowChar As Char, ByVal highChar As Char)

    '    End Sub

    '    Public Overrides Sub WriteWhitespace(ByVal ws As String)

    '    End Sub
    'End Class

End Module
