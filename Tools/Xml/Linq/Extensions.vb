Imports System.Xml.Linq, Tools.ExtensionsT
Imports System.Runtime.CompilerServices

#If Config <= Nightly Then 'Stage:Nightly
Namespace XmlT.LinqT
    ''' <summary>Provides extension methods related to <see cref="System.Xml.Linq"/></summary>
    Module Extensions
        ''' <summary>Retruns value indicating if two <see cref="XElement">XElements</see> has same name (this means <see cref="XName.LocalName"/> are same as well as <see cref="XName.NamespaceName"/>.</summary>
        ''' <param name="el">A <see cref="XElement"/> to test name of</param>
        ''' <param name="other">A <see cref="XElement"/> to compare name with</param>
        ''' <returns>Ture if <paramref name="el"/> and <paramref name="other"/> have same <see cref="XElement.Name"/></returns>
        ''' <seealso cref="XName.op_Equality"/>
        <Extension()> Public Function HasSameName(ByVal el As XElement, ByVal other As XElement) As Boolean
            Return el.Name = other.Name
        End Function
        ''' <summary>Converts given string to <see cref="XName"/> in context of given <see cref="XElement"/></summary>
        ''' <param name="Element">Element which provides context for namespace prefixes definition</param>
        ''' <param name="Name">Name if form "<c>prefix:local-name</c>" or "<c>local-name</c>" to get <see cref="XName"/> for</param>
        ''' <returns><see cref="XName"/> obtained by interpreting <paramref name="Name"/> as XML qualified name in context of <paramref name="Element"/></returns>
        ''' <exception cref="ArgumentException"><paramref name="Name"/> contains namespace prefix, but it is not known in context of <paramref name="Element"/> (this exception is also thrown when name contains colon (:) and sub-string al left side of it is not valid name for prefix.</exception>
        ''' <exception cref="Xml.XmlException"><paramref name="Name"/> is invalid XML name.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Element"/> or <paramref name="Name"/> is null</exception>
        <Extension()> Public Function InterpretAsName(ByVal Element As XElement, ByVal Name As String) As XName
            If Element Is Nothing Then Throw New ArgumentNullException("Element")
            If Name Is Nothing Then Throw New ArgumentNullException("Name")
            If Name.Contains(":"c) Then
                Dim Parts As String() = Name.Split(New Char() {":"c}, 2)
                Dim ns = Element.GetNamespaceOfPrefix(Parts(0))
                If ns Is Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.NamespacePrefix0IsNotDefined.f(Parts(0)), "Name")
                Return ns.GetName(Parts(1))
            Else
                Dim ns = Element.GetDefaultNamespace
                If ns Is Nothing Then Return XName.Get(Name)
                Return ns.GetName(Name)
            End If
        End Function
        ''' <summary>Interprets value of given <see cref="XAttribute"/> as <see cref="XName"/></summary>
        ''' <param name="Attr">Attribute to interpret as <see cref="XName"/> value of</param>
        ''' <returns>Value of the <paramref name="Attr"/> attribute interpreted as XML qualified name i context of its parent element</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Attr"/> is null</exception>
        ''' <seelaso cref="InterpretAsName"/>
        ''' <exception cref="ArgumentException"><paramref name="Attr"/> has no parent element -or- Value of <paramref name="Attr"/> contains namespace prefix but is is invalid (ill-formed) or unknown in context of its parent element.</exception>
        ''' <exception cref="Xml.XmlException">Value of <paramref name="Attr"/> is not valid XML name</exception>
        <Extension()> Public Function AsName(ByVal Attr As XAttribute) As XName
            If Attr Is Nothing Then Throw New ArgumentNullException("Attr")
            If Attr.Parent Is Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.XMLAttributeHasNoParentElement, "Attr")
            Return Attr.Parent.InterpretAsName(Attr.Value)
        End Function
        ''' <summary>Gets inner XML of given XML element</summary>
        ''' <param name="Element">Element to get inner XML of</param>
        ''' <param name="Options">Specifies formatting behavior</param>
        ''' <returns>XML markup as string representing all the nodes inside <paramref name="Element"/></returns>
        <Extension()> Public Function InnerXml(ByVal Element As XElement, Optional ByVal Options As SaveOptions = SaveOptions.None) As String
            Dim b As New System.Text.StringBuilder
            For Each Node In Element.Nodes
                b.Append(Node.ToString(Options))
            Next
            Return b.ToString
        End Function
        ''' <summary>Replaces inner XML of given <see cref="XElement"/> with XML loaded from given string</summary>
        ''' <param name="Element"><see cref="XElement"/> to replace content of</param>
        ''' <param name="InnerXML">New inner XML</param>
        ''' <exception cref="xml.XmlException"><paramref name="InnerXml"/> is invalid XML fragment</exception>
        <Extension()> Public Sub SetInnerXml(ByVal Element As XElement, ByVal InnerXml As String)
            Dim Doc As New Xml.XmlDocument
            Doc.Load(Element.CreateReader)
            Doc.DocumentElement.InnerXml = InnerXml
            'Dim XDoc = New XDocument(New Xml.XmlNodeReader(Doc))
            Element.RemoveNodes()
            'For Each node In XDoc.Root.Nodes
            '    Element.Add(node)
            'Next
            For Each Node As Xml.XmlNode In Doc.DocumentElement.ChildNodes
                Select Case Node.NodeType
                    Case Xml.XmlNodeType.CDATA : Element.Add(New XCData(Node.Value))
                    Case Xml.XmlNodeType.Comment : Element.Add(New XComment(Node.Value))
                    Case Xml.XmlNodeType.Element : Element.Add(XElement.Parse(Node.OuterXml))
                    Case Xml.XmlNodeType.EntityReference : Element.Add(Node.Value)
                    Case Xml.XmlNodeType.ProcessingInstruction : Element.Add(New XProcessingInstruction(DirectCast(Node, Xml.XmlProcessingInstruction).Target, Node.Value))
                    Case Xml.XmlNodeType.SignificantWhitespace : Element.Add(New XText(Node.Value))
                    Case Xml.XmlNodeType.Text : Element.Add(New XText(Node.Value))
                    Case Xml.XmlNodeType.Whitespace : Element.Add(New XText(Node.Value))
                End Select
            Next
        End Sub
    End Module
End Namespace
#End If