Imports System.Xml.Linq, System.Xml.Schema
Imports System.CodeDom
Imports <xmlns="http://dzonny.cz/xml/schemas/CodeDom">

#If Config <= Nightly Then 'Stage:Nightly
Namespace CodeDomT
    ''' <summary>Contains methods for converting various elements form the http://dzonny.cz/xml/schemas/CodeDom XML namespace to objects from <see cref="CodeDom"/> namespace.</summary>
    Public Class Xml2CodeDom
        ''' <summary>Name of namespace which is used for validation XML documents</summary>
        Public Const CodeDomXmlNamespace$ = "http://dzonny.cz/xml/schemas/CodeDom"
        ''' <summary>Gets XML-Schema for the http://dzonny.cz/xml/schemas/CodeDom namespace.</summary>
        ''' <returns>Xml schema for the http://dzonny.cz/xml/schemas/CodeDom namespace</returns>
        ''' <remarks>This schema is used by the <see cref="Xml2CodeDom"/> class for validation XML documents being converted to <see cref="CodeDom"/> objects.
        ''' The schema can be obtained from embdeded resource Tools.CodeDomT.CodeDom.xsd or from source code of ĐTools library.</remarks>
        Public Shared Function GetXmlSchema() As Xml.Schema.XmlSchema
            Dim veh As ValidationEventHandler = AddressOf ValidationHandler
            Return XmlSchema.Read(GetType(Xml2CodeDom).Assembly.GetManifestResourceStream("Tools.CodeDomT.CodeDom.xsd"), veh)
        End Function

        Public Function Xml2CompileUnit(ByVal Xml As Xml.XmlDocument) As CodeCompileUnit
            Return Xml2CompileUnit(XDocument.Load(New Xml.XmlNodeReader(Xml)))
        End Function
        ''' <summary>Converts given <see cref="XDocument"/> to <see cref="CodeCompileUnit"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeCompileUnit"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> agains XML-Schema</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="Xml"/> is neither &lt;CompileUnit> nor &lt;SnippetCompileUnit>.</exception>
        Public Function Xml2CompileUnit(ByVal Xml As XDocument) As CodeCompileUnit
            Dim SchSet As New XmlSchemaSet()
            Dim veh As ValidationEventHandler = AddressOf ValidationHandler
            SchSet.Add(GetXmlSchema)
            Xml.Validate(SchSet, veh)
            If Xml.Root.Name <> XName.Get("CompileUnit", CodeDomXmlNamespace) AndAlso Xml.Root.Name <> XName.Get("SnippetCompileUnit", CodeDomXmlNamespace) Then _
                Throw New ArgumentException("Root element must be <CompileUnit> or <SnippetCompileUnit>", "Xml")
            Return GetCompileUnit(Xml.Root)
        End Function
        Private Function GetCompileUnit(ByVal Xml As XElement) As CodeCompileUnit
            Dim CompileUnit As New CodeCompileUnit
            Return CompileUnit
        End Function
        Private Shared Sub ValidationHandler(ByVal sender As Object, ByVal e As ValidationEventArgs)
            If e.Severity = XmlSeverityType.Error Then Throw e.Exception
        End Sub
    End Class
End Namespace
#End If