Imports System.Xml.Linq, Tools.ExtensionsT
Imports System.Runtime.CompilerServices
Imports System.Xml.Schema

#If Config <= Nightly Then 'Stage:Nightly
Namespace XmlT.LinqT
    ''' <summary>Provides extension methods related to <see cref="System.Xml.Linq"/></summary>
    ''' <version version="1.5.3" stage="Nightly">Module made public</version>
    Public Module Extensions
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
        ''' <summary>Gets string representing given <see cref="XName"/> in XML document in context of given <see cref="XElement"/></summary>
        ''' <param name="Name">Name to be represented</param>
        ''' <param name="Context">Element providing informations about namespace prefixes</param>
        ''' <returns>String representation of <paramref name="Name"/> in form "prefix:local-name" or "local-name" (when it is associated with default namespace and default namespace has no prefix associated)</returns>
        ''' <exception cref="ArgumentNullException">Namespace of <paramref name="Name"/> is not defined in context of <paramref name="Context"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Context"/> or <paramref name="Name"/> is null</exception>
        <Extension()> _
        Public Function CollapseInContext(ByVal Name As XName, ByVal Context As XElement) As String
            If Name Is Nothing Then Throw New ArgumentNullException("Name")
            If Context Is Nothing Then Throw New ArgumentNullException("Context")
            Dim prefix = Context.GetNamespacePrefix(Name.Namespace)
            If prefix Is Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.Namespace0HasNoPrefixAssociatedAndItIsNotDefaultNamespace.f(Name.Namespace.NamespaceName))
            If prefix = "" Then Return Name.LocalName Else Return "{0}:{1}".f(prefix, Name.LocalName)
        End Function
        ''' <summary>Gets string representing given <see cref="XName"/> in XML document in context of given <see cref="XElement"/></summary>
        ''' <param name="Name">Name to be represented</param>
        ''' <param name="Context">Element providing informations about namespace prefixes</param>
        ''' <returns>String representation of <paramref name="Name"/> in form "prefix:local-name" or "local-name" (when it is associated with default namespace and default namespace has no prefix associated)</returns>
        ''' <exception cref="ArgumentNullException">Namespace of <paramref name="Name"/> is not defined in context of <paramref name="Context"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Context"/> or <paramref name="Name"/> is null</exception>
        <Extension()> _
        Public Function CollapseName(ByVal Context As XElement, ByVal Name As XName) As String
            Return Name.CollapseInContext(Context)
        End Function
        ''' <summary>Gets prefix of given namespace for given element. Makes it easy to disnguish if namespace is default or undefined.</summary>
        ''' <param name="Element">Eleemnt defining scope of validity of prefix</param>
        ''' <param name="ns">Namespace to get prefix of</param>
        ''' <returns>Prefix of namespace <paramref name="ns"/> valid at level of <paramref name="Element"/>. <see cref="String.Empty"/> if <paramref name="ns"/> is default namespace and has no prefix associated. Null when <paramref name="ns"/> is undefined al level of <paramref name="Element"/> or higher.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Element"/> or <paramref name="ns"/> is null</exception>
        <Extension()> _
        Public Function GetNamespacePrefix(ByVal Element As XElement, ByVal ns As XNamespace) As String
            If Element Is Nothing Then Throw New ArgumentNullException("Element")
            If ns Is Nothing Then Throw New ArgumentNullException("ns")
            Dim prefix = Element.GetPrefixOfNamespace(ns)
            If prefix IsNot Nothing Then Return prefix
            If Element.FindDefaultNamespace = ns Then Return "" Else Return Nothing
        End Function
        ''' <summary>Gets default namespace valid at level of given <see cref="XElement"/> even in sutuation when <see cref="XElement.GetDefaultNamespace"/> fails.</summary>
        ''' <param name="Element">Element to get default namespace for</param>
        ''' <returns>Default namespace for <paramref name="Element"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Element"/> is null</exception>
        ''' <remarks>
        ''' This function calls <paramref name="Element"/>.<see cref="XElement.GetDefaultNamespace">GetDefaultNamespace</see>.
        ''' When it returns <see cref="XNamespace.None"/> it searches document tree from <paramref name="Element"/> upwards to root and:
        ''' <list type="bullet">
        ''' <item>When xmlns attribute with value of empty string is found, returns <see cref="XNamespace.None"/>.</item>
        ''' <item>When element with name belonging to namespace for which <see cref="XElement.GetPrefixOfNamespace"/> does not return prefix is found, that namespace is returned.</item>
        ''' <item>If no such condition is fullfiled and root is reached, <see cref="XNamespace.None"/> is returned.</item>
        ''' </list>
        ''' </remarks>
        <Extension()> _
        Public Function FindDefaultNamespace(ByVal Element As XElement) As XNamespace
            If Element Is Nothing Then Throw New ArgumentNullException("Element")
            Dim ns = Element.GetDefaultNamespace
            If ns <> XNamespace.None Then Return ns
            Dim CurrEl = Element
            Do
                If CurrEl.Attribute("xmlns") IsNot Nothing AndAlso CurrEl.@xmlns = "" Then Return XNamespace.None
                Dim prefix = CurrEl.GetPrefixOfNamespace(CurrEl.Name.Namespace)
                If prefix = "" Then Return CurrEl.Name.Namespace
                CurrEl = CurrEl.Parent
            Loop While CurrEl IsNot Nothing
            Return XNamespace.None
        End Function
        ''' <summary>For attribute which's value is expanded XML name in format "{namespace-uri}local-name" collapses this name to format "prefix:local-name" (or "local-name" when namespace is default namespace and has no prefix associated).</summary>
        ''' <param name="attr">Attribute to collapse value of</param>
        ''' <exception cref="System.Xml.XmlException">Value of attribute <paramref name="attr"/> is neither valied expanded name in format "<c>{namespace-uri}local-name</c>" neither valid local name</exception>
        ''' <exception cref="ArgumentException"><paramref name="attr"/> attribute has no parent element.</exception>
        ''' <remarks>When <paramref name="attr"/> is null, its value is null or its value is an empty string this method exits without doing anything.</remarks>
        <Extension()> _
        Public Sub CollapseExtendedName(ByVal attr As XAttribute)
            If attr Is Nothing Then Return
            If attr.Value Is Nothing Then Return
            If attr.Value = "" Then Return
            If attr.Parent Is Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.XMLAttributeHasNoParentElement)
            Dim name As XName = XName.Get(attr.Value)
            attr.Value = name.CollapseInContext(attr.Parent)
        End Sub

        ''' <summary>Validates that an <see cref="System.Xml.Linq.XDocument" /> conforms to an XSD in an <see cref="System.Xml.Schema.XmlSchemaSet" />, optionally populating the XML tree with the post-schema-validation infoset (PSVI).</summary>
        ''' <param name="document">The <see cref="System.Xml.Linq.XDocument" /> to validate.</param>
        ''' <param name="schema">A <see cref="System.Xml.Schema.XmlSchema" /> to validate against.</param>
        ''' <param name="validationEventHandler">A <see cref="System.Xml.Schema.ValidationEventHandler" /> for an event that occurs when the reader encounters validation errors. If null (default), throws an exception upon validation errors.</param>
        ''' <param name="addSchemaInfo">A <see cref="System.Boolean" /> indicating whether to populate the post-schema-validation infoset (PSVI).</param>
        ''' <exception cref="System.Xml.Schema.XmlSchemaValidationException">Thrown for XML Schema Definition Language (XSD) validation errors.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="document"/> or <paramref name="schema"/> is null.</exception>
        ''' <version version="1.5.3">This method is new in version 1.5.3</version>
        <Extension()>
        Public Sub Validate(ByVal document As XDocument, ByVal schema As XmlSchema, Optional ByVal validationEventHandler As ValidationEventHandler = Nothing, Optional ByVal addSchemaInfo As Boolean = False)
            If document Is Nothing Then Throw New ArgumentNullException("document")
            If schema Is Nothing Then Throw New ArgumentNullException("schema")
            Dim ss As New XmlSchemaSet
            ss.Add(schema)
            document.Validate(ss, validationEventHandler, addSchemaInfo)
        End Sub
    End Module
End Namespace
#End If