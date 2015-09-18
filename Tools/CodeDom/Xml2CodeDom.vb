Imports System.CodeDom
Imports System.Linq
Imports System.Reflection
Imports System.Xml.Linq
Imports System.Xml.Schema
Imports CultureInfo = System.CultureInfo
Imports NumberStyles = System.NumberStyles
Imports Tools.CollectionsT.SpecializedT.AsTypeSafe
Imports Tools.ExtensionsT
Imports Tools.LinqT
Imports Tools.XmlT.LinqT
Imports <xmlns="http://dzonny.cz/xml/schemas/CodeDom">
Imports <xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
Imports CultureInfo = System.Globalization.CultureInfo
Imports NumberStyles = System.Globalization.NumberStyles
Imports DateTimeStyles = System.Globalization.DateTimeStyles

'#If True
'Stage:Nightly
Namespace CodeDomT
    ''' <summary>Contains methods for converting various elements form the http://dzonny.cz/xml/schemas/CodeDom XML namespace to objects from <see cref="CodeDom"/> namespace.</summary>
    Public Class Xml2CodeDom
        Implements CodeDom.Compiler.ICodeGenerator
        Implements CodeDom.Compiler.ICodeParser
        ''' <summary>Name of namespace which is used for validation XML documents</summary>
        ''' <remarks>Value of this field is always http://dzonny.cz/xml/schemas/CodeDom</remarks>
        Public Shared ReadOnly CodeDomXmlNamespace$ = GetXmlNamespace().NamespaceName
        ''' <summary>Name of the xsi:type attribute</summary>
        Private Shared ReadOnly xsiType As XName = GetXmlNamespace(xsi).GetName("type")
        ''' <summary>Gets XML-Schema for the http://dzonny.cz/xml/schemas/CodeDom namespace.</summary>
        ''' <returns>Xml schema for the http://dzonny.cz/xml/schemas/CodeDom namespace</returns>
        ''' <remarks>This schema is used by the <see cref="Xml2CodeDom"/> class for validation XML documents being converted to <see cref="CodeDom"/> objects.
        ''' The schema can be obtained from embdeded resource Tools.CodeDomT.CodeDom.xsd or from source code of ĐTools library.</remarks>
        Public Shared Function GetXmlSchema() As Xml.Schema.XmlSchema
            Dim veh As ValidationEventHandler = AddressOf ValidationHandler
            Return XmlSchema.Read(GetType(Xml2CodeDom).Assembly.GetManifestResourceStream("Tools.CodeDomT.CodeDom.xsd"), veh)
        End Function
#Region "Publix XML 2 CodeDom"
#Region "CompileUnit"
        ''' <summary>Converts given <see cref="Xml.XmlDocument"/> to <see cref="CodeCompileUnit"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeCompileUnit"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> against XML-Schema</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="Xml"/> is neither <c>&lt;CompileUnit></c> nor <c>&lt;SnippetCompileUnit></c>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Xml"/> is null</exception>
        Public Function Xml2CompileUnit(ByVal Xml As Xml.XmlDocument) As CodeCompileUnit
            If Xml Is Nothing Then Throw New ArgumentNullException("Xml")
            Return Xml2CompileUnit(XDocument.Load(New Xml.XmlNodeReader(Xml)))
        End Function
        ''' <summary>Converts given <see cref="XDocument"/> to <see cref="CodeCompileUnit"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeCompileUnit"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> against XML-Schema</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="Xml"/> is neither <c>&lt;CompileUnit></c> nor <c>&lt;SnippetCompileUnit></c>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Xml"/> is null</exception>
        Public Function Xml2CompileUnit(ByVal Xml As XDocument) As CodeCompileUnit
            If Xml Is Nothing Then Throw New ArgumentNullException("Xml")
            Dim SchSet As New XmlSchemaSet()
            Dim veh As ValidationEventHandler = AddressOf ValidationHandler
            SchSet.Add(GetXmlSchema)
            Xml.Validate(SchSet, veh)
            If Xml.Root.Name <> Names.CompileUnit AndAlso Xml.Root.Name <> Names.SnippetCompileUnit Then _
                Throw New ArgumentException(String.Format(ResourcesT.Exceptions.RootElementMustBe0Or1, Names.CompileUnit.LocalName, Names.SnippetCompileUnit.LocalName), "Xml")
            Return GetCompileUnit(Xml.Root)
        End Function
#End Region
#Region "Namespace"
        ''' <summary>Converts given <see cref="Xml.XmlDocument"/> to <see cref="CodeNamespace"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeNamespace"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> against XML-Schema</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="Xml"/> is not <c>&lt;Namespace></c>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Xml"/> is null</exception>
        Public Function Xml2Namespace(ByVal Xml As Xml.XmlDocument) As CodeNamespace
            If Xml Is Nothing Then Throw New ArgumentNullException("Xml")
            Return Xml2Namespace(XDocument.Load(New Xml.XmlNodeReader(Xml)))
        End Function
        ''' <summary>Converts given <see cref="XDocument"/> to <see cref="CodeNamespace"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeNamespace"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> against XML-Schema</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="Xml"/> is not <c>&lt;Namespace></c>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Xml"/> is null</exception>
        Public Function Xml2Namespace(ByVal Xml As XDocument) As CodeNamespace
            If Xml Is Nothing Then Throw New ArgumentNullException("Xml")
            Dim SchSet As New XmlSchemaSet()
            Dim veh As ValidationEventHandler = AddressOf ValidationHandler
            SchSet.Add(GetXmlSchema)
            Xml.Validate(SchSet, veh)
            If Xml.Root.Name <> Names.Namespace Then _
                Throw New ArgumentException(ResourcesT.Exceptions.RootElementMustBe0.f(Names.Namespace.LocalName), "Xml")
            Return GetNamespace(Xml.Root)
        End Function
#End Region
#Region "Type"
        ''' <summary>Converts given <see cref="Xml.XmlDocument"/> to <see cref="CodeTypeDeclaration"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeTypeDeclaration"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> against XML-Schema</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="Xml"/> is not <c>&lt;Type></c>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Xml"/> is null</exception>
        Public Function Xml2Type(ByVal Xml As Xml.XmlDocument) As CodeTypeDeclaration
            If Xml Is Nothing Then Throw New ArgumentNullException("Xml")
            Return Xml2Type(XDocument.Load(New Xml.XmlNodeReader(Xml)))
        End Function
        ''' <summary>Converts given <see cref="XDocument"/> to <see cref="CodeTypeDeclaration"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeTypeDeclaration"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> against XML-Schema</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="Xml"/> is not <c>&lt;Type></c>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Xml"/> is null</exception>
        Public Function Xml2Type(ByVal Xml As XDocument) As CodeTypeDeclaration
            If Xml Is Nothing Then Throw New ArgumentNullException("Xml")
            Dim SchSet As New XmlSchemaSet()
            Dim veh As ValidationEventHandler = AddressOf ValidationHandler
            SchSet.Add(GetXmlSchema)
            Xml.Validate(SchSet, veh)
            If Xml.Root.Name <> Names.TypeDeclaration AndAlso Xml.Root.Name <> Names.TypeDelegate Then _
                Throw New ArgumentException(ResourcesT.Exceptions.RootElementMustBe0Or1.f(Names.TypeDeclaration.LocalName, Names.TypeDelegate.LocalName), "Xml")
            Return GetTypeDeclaration(Xml.Root)
        End Function
#End Region
#Region "Method"
        ''' <summary>Converts given <see cref="Xml.XmlDocument"/> to <see cref="CodeMemberMethod"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeMemberMethod"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> against XML-Schema</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="Xml"/> is not <c>&lt;MemberMethod></c>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Xml"/> is null</exception>
        Public Function Xml2Method(ByVal Xml As Xml.XmlDocument) As CodeMemberMethod
            If Xml Is Nothing Then Throw New ArgumentNullException("Xml")
            Return Xml2Method(XDocument.Load(New Xml.XmlNodeReader(Xml)))
        End Function
        ''' <summary>Converts given <see cref="XDocument"/> to <see cref="CodeMemberMethod"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeMemberMethod"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> against XML-Schema</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="Xml"/> is not <c>&lt;MemberMethod></c>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Xml"/> is null</exception>
        Public Function Xml2Method(ByVal Xml As XDocument) As CodeMemberMethod
            If Xml Is Nothing Then Throw New ArgumentNullException("Xml")
            Dim SchSet As New XmlSchemaSet()
            Dim veh As ValidationEventHandler = AddressOf ValidationHandler
            SchSet.Add(GetXmlSchema)
            Xml.Validate(SchSet, veh)
            If Xml.Root.Name <> Names.MemberMethod Then _
                Throw New ArgumentException(ResourcesT.Exceptions.RootElementMustBe0.f(Names.MemberMethod.LocalName), "Xml")
            Return GetMemberMethod(Xml.Root)
        End Function
#End Region
#Region "Expression"
        ''' <summary>Converts given <see cref="Xml.XmlDocument"/> to <see cref="CodeExpression"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeExpression"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> against XML-Schema</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="Xml"/> is does not represent supported expression.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Xml"/> is null</exception>
        Public Function Xml2Expression(ByVal Xml As Xml.XmlDocument) As CodeExpression
            If Xml Is Nothing Then Throw New ArgumentNullException("Xml")
            Return Xml2Expression(XDocument.Load(New Xml.XmlNodeReader(Xml)))
        End Function
        ''' <summary>Converts given <see cref="XDocument"/> to <see cref="CodeExpression"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeExpression"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> against XML-Schema</exception>
        ''' <exception cref="ArgumentException">Root element of <paramref name="Xml"/> is does not represent supported expression.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Xml"/> is null</exception>
        Public Function Xml2Expression(ByVal Xml As XDocument) As CodeExpression
            If Xml Is Nothing Then Throw New ArgumentNullException("Xml")
            Dim SchSet As New XmlSchemaSet()
            Dim veh As ValidationEventHandler = AddressOf ValidationHandler
            SchSet.Add(GetXmlSchema)
            Xml.Validate(SchSet, veh)
            Return GetExpression(Xml.Root)
        End Function
#End Region
#Region "Generic"
        ''' <summary>Converts given <see cref="Xml.XmlDocument"/> to <see cref="CodeObject"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeObject"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> against XML-Schema</exception>
        ''' <exception cref="NotSupportedException">Root element of <paramref name="Xml"/> cannot be deserialized because it is unknown or its object representation does not derive from <see cref="CodeObject"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Xml"/> is null</exception>
        Public Function Xml2CodeDom(ByVal Xml As Xml.XmlDocument) As CodeExpression
            If Xml Is Nothing Then Throw New ArgumentNullException("Xml")
            Return Xml2Expression(XDocument.Load(New Xml.XmlNodeReader(Xml)))
        End Function
        ''' <summary>Converts given <see cref="XDocument"/> to <see cref="CodeObject"/></summary>
        ''' <param name="Xml">Document ot convert</param>
        ''' <returns><see cref="CodeObject"/> constructed from <paramref name="Xml"/></returns>
        ''' <exception cref="Xml.XmlException">An error ocured while validating <paramref name="Xml"/> against XML-Schema</exception>
        ''' <exception cref="NotSupportedException">Root element of <paramref name="Xml"/> cannot be deserialized because it is unknown or its object representation does not derive from <see cref="CodeObject"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Xml"/> is null</exception>
        Public Function Xml2CodeDom(ByVal Xml As XDocument) As CodeObject
            If Xml Is Nothing Then Throw New ArgumentNullException("Xml")
            Dim SchSet As New XmlSchemaSet()
            Dim veh As ValidationEventHandler = AddressOf ValidationHandler
            SchSet.Add(GetXmlSchema)
            Xml.Validate(SchSet, veh)
            Dim Name As XName
            If Xml.Root.@xsi:type IsNot Nothing Then Name = Xml.Root.Attribute(xsiType).AsName Else Name = Xml.Root.Name
            Select Case Name
                Case Names.ArgumentReferenceExpression, Names.ArrayCreateExpression, Names.ArrayIndexerExpression, Names.BaseReferenceExpression, Names.BinaryOperatorExpression, Names.CastExpression, Names.DefaultValueExpression, Names.DelegateCreateExpression, Names.DelegateInvokeExpression, Names.DirectionExpression, Names.EventReferenceExpression, Names.Expression, Names.FieldReferenceExpression, Names.IndexerExpression, Names.MethodInvokeExpression, Names.MethodReferenceExpression, Names.ObjectCreateExpression, Names.ParameterDeclarationExpression, Names.PrimitiveExpression, Names.PropertyReferenceExpression, Names.PropertySetValueReferenceExpression, Names.SnippetExpression, Names.ThisReferenceExpression, Names.TypeOfExpression, Names.TypeReferenceExpression, Names.VariableReferenceExpression
                    Return GetExpression(Xml.Root)
                Case Names.AssemblyReference
                    Throw New NotSupportedException(ResourcesT.Exceptions.ValueOfType0CannotBeDeserializedStandAloneBecauseIts.f(Name))
                    'Return GetAssemblyReference(Xml.Root)
                Case Names.AssignStatement, Names.AttachEventStatement, Names.CommentStatement, Names.ConditionStatement, Names.ExpressionStatement, Names.GotoStatement, Names.IterationStatement, Names.LabeledStatement, Names.MethodReturnStatement, Names.RemoveEventStatement, Names.SnippetStatement, Names.Statement, Names.ThrowExceptionStatement, Names.TryCatchFinallyStatement, Names.VariableDeclarationStatement
                    Return GetStatement(Xml.Root)
                    Throw New NotSupportedException(ResourcesT.Exceptions.ValueOfType0CannotBeDeserializedStandAloneBecauseIts.f(Name))
                Case Names.AttributeArgument
                    'Return GetAttributeArgument(Xml.Root)
                    Throw New NotSupportedException(ResourcesT.Exceptions.ValueOfType0CannotBeDeserializedStandAloneBecauseIts.f(Name))
                Case Names.AttributeDeclaration
                    'Return GetAttributeDeclaration(Xml.Root)
                    Throw New NotSupportedException(ResourcesT.Exceptions.ValueOfType0CannotBeDeserializedStandAloneBecauseIts.f(Name))
                Case Names.CatchClause
                    'Return GetCatchClause(Xml.Root)
                    Throw New NotSupportedException(ResourcesT.Exceptions.ValueOfType0CannotBeDeserializedStandAloneBecauseIts.f(Name))
                Case Names.Comment
                    Return GetComment(Xml.Root)
                Case Names.CompileUnit, Names.SnippetCompileUnit
                    Return GetCompileUnit(Xml.Root)
                Case Names.Constructor, Names.EntryPointMethod, Names.MemberEvent, Names.MemberField, Names.MemberMethod, Names.MemberProperty, Names.TypeConstructor, Names.TypeDeclaration, Names.TypeDelegate, Names.TypeMember
                    Return GetTypeMember(Xml.Root)
                Case Names.ChecksumPragma, Names.Directive, Names.RegionDirective
                    Return GetDirective(Xml.Root)
                Case Names.LinePragma
                    'Return GetLinePragma(Xml.Root)
                    Throw New NotSupportedException(ResourcesT.Exceptions.ValueOfType0CannotBeDeserializedStandAloneBecauseIts.f(Name))
                Case Names.Namespace
                    Return GetNamespace(Xml.Root)
                Case Names.NamespaceImport
                    Return GetNamespaceImport(Xml.Root)
                Case Names.TypeParameter
                    Return GetTypeParameter(Xml.Root)
                Case Names.TypeReference
                    Return GetTypeReference(Xml.Root)
                Case Else : Throw New NotSupportedException(ResourcesT.Exceptions.IsNotSupportedTypeOfXMLSerializedCodeDOMObjectFormat(Name))
            End Select
        End Function
#End Region
        ''' <summary>Internal XML validation handler - throws on anny error, ignores warnings</summary>
        ''' <param name="sender">Validation source</param>
        ''' <param name="e">Validation arguments</param>
        Private Shared Sub ValidationHandler(ByVal sender As Object, ByVal e As ValidationEventArgs)
            If e.Severity = XmlSeverityType.Error Then Throw e.Exception
        End Sub
#End Region
#Region "Public CodeDom 2 XML"
        ''' <summary>Serializes given <see cref="CodeCompileUnit"/> to <see cref="XDocument"/></summary>
        ''' <param name="CompileUnit">Object to serialize</param>
        ''' <returns>XML document reperesenting given <see cref="CodeCompileUnit"/></returns>
        ''' <exception cref="NotSupportedException">Unsupported CodeDOM object or primitive type found.</exception>
        Public Function CompileUnit2Xml(ByVal CompileUnit As CodeCompileUnit) As XDocument
            Return <?xml version="1.0"?>
                   <%= SerializeCompileUnit(CompileUnit) %>
        End Function
        ''' <summary>Serializes given <see cref="CodeNamespace"/> to <see cref="XDocument"/></summary>
        ''' <param name="Namespace">Object to serialize</param>
        ''' <returns>XML document reperesenting given <see cref="CodeNamespace"/></returns>
        ''' <exception cref="NotSupportedException">Unsupported CodeDOM object or primitive type found.</exception>
        Public Function Namespace2Xml(ByVal [Namespace] As CodeNamespace) As XDocument
            Return <?xml version="1.0"?>
                   <%= SerializeNamespace([Namespace]) %>
        End Function
        ''' <summary>Serializes given <see cref="CodeTypeDeclaration"/> to <see cref="XDocument"/></summary>
        ''' <param name="TypeDeclaration">Object to serialize</param>
        ''' <returns>XML document reperesenting given <see cref="CodeTypeDeclaration"/></returns>
        ''' <exception cref="NotSupportedException">Unsupported CodeDOM object or primitive type found.</exception>
        Public Function Type2Xml(ByVal TypeDeclaration As CodeTypeDeclaration) As XDocument
            Return <?xml version="1.0"?>
                   <%= SerializeTypeDeclaration(TypeDeclaration) %>
        End Function
        ''' <summary>Serializes given <see cref="CodeMemberMethod"/> to <see cref="XDocument"/></summary>
        ''' <param name="MemberMethod">Object to serialize</param>
        ''' <returns>XML document reperesenting given <see cref="CodeMemberMethod"/></returns>
        ''' <exception cref="NotSupportedException">Unsupported CodeDOM object or primitive type found.</exception>
        Public Function Method2Xml(ByVal MemberMethod As CodeMemberMethod) As XDocument
            Return <?xml version="1.0"?>
                   <%= SerializeMemberMethod(MemberMethod) %>
        End Function
        ''' <summary>Serializes given <see cref="CodeExpression"/> to <see cref="XDocument"/></summary>
        ''' <param name="Expression">Object to serialize</param>
        ''' <returns>XML document reperesenting given <see cref="CodeExpression"/></returns>
        ''' <exception cref="NotSupportedException">Unsupported CodeDOM object or primitive type found.</exception>
        Public Function Expression2Xml(ByVal Expression As CodeExpression) As XDocument
            Return <?xml version="1.0"?>
                   <%= SerializeExpression(Expression) %>
        End Function
        ''' <summary>Serializes given <see cref="CodeObject"/> to <see cref="XDocument"/></summary>
        ''' <param name="CodeDom">Object to serialize</param>
        ''' <returns>XML document reperesenting given <see cref="CodeObject"/></returns>
        ''' <exception cref="NotSupportedException">Unsupported CodeDOM object or primitive type found.</exception>
        Public Function CodeDom2Xml(ByVal CodeDom As CodeObject) As XDocument
            Dim element As XElement
            If CodeDom Is Nothing Then : element = Nothing
            ElseIf TypeOf CodeDom Is CodeComment Then : element = SerializeComment(CodeDom)
            ElseIf TypeOf CodeDom Is CodeCompileUnit Then : element = SerializeCompileUnit(CodeDom)
            ElseIf TypeOf CodeDom Is CodeDirective Then : element = SerializeDirective(CodeDom)
            ElseIf TypeOf CodeDom Is CodeExpression Then : element = SerializeExpression(CodeDom)
            ElseIf TypeOf CodeDom Is CodeNamespace Then : element = SerializeNamespace(CodeDom)
            ElseIf TypeOf CodeDom Is CodeNamespaceImport Then : element = SerializeNamespaceImport(CodeDom)
            ElseIf TypeOf CodeDom Is CodeStatement Then : element = SerializeStatement(CodeDom)
            ElseIf TypeOf CodeDom Is CodeTypeMember Then : element = SerializeTypeMember(CodeDom)
            ElseIf TypeOf CodeDom Is CodeTypeParameter Then : element = SerializeTypeParameter(CodeDom)
            ElseIf TypeOf CodeDom Is CodeTypeReference Then : element = SerializeTypeReference(CodeDom)
            Else : Throw New NotSupportedException(ResourcesT.Exceptions.UnsupportedCodeDomObject0.f(CodeDom.GetType.Name))
            End If
            Return <?xml version="1.0"?>
                   <%= element %>
        End Function
#End Region
#Region "Xml 2 CodeDom"
        ''' <summary>Gets <see cref="CodeCompileUnit"/> from XML element</summary>
        ''' <param name="Xml">XML element <c>&lt;CompileUnit></c> or <c>&lt;SnippetCompileUnit></c></param>
        ''' <returns><see cref="CodeCompileUnit"/> represented by <paramref name="Xml"/></returns>
        Private Function GetCompileUnit(ByVal Xml As XElement) As CodeCompileUnit
            Dim CompileUnit As CodeCompileUnit
            If Xml.Name = Names.SnippetCompileUnit OrElse (Xml.Attribute(xsiType) IsNot Nothing AndAlso Xml.Attribute(xsiType).AsName = Names.SnippetCompileUnit) Then
                CompileUnit = New CodeSnippetCompileUnit(Xml.<Value>.Value)
                For Each lp In Xml.<LinePragma> 'Only once
                    DirectCast(CompileUnit, CodeSnippetCompileUnit).LinePragma = GetLinePragma(Xml)
                Next
            Else
                CompileUnit = New CodeCompileUnit
            End If
            For Each sd In Xml.<StartDirectives>.Elements
                CompileUnit.StartDirectives.Add(GetDirective(sd))
            Next
            For Each ra In Xml.<ReferencedAssemblies>.Elements
                CompileUnit.ReferencedAssemblies.Add(GetAssemblyReference(ra))
            Next
            For Each ca In Xml.<AssemblyCustomAttributes>.Elements
                CompileUnit.AssemblyCustomAttributes.Add(GetAttributeDeclaration(ca))
            Next
            For Each ns In Xml.<Namespaces>.Elements
                CompileUnit.Namespaces.Add(GetNamespace(ns))
            Next
            For Each ed In Xml.<EndDirectives>.Elements
                CompileUnit.EndDirectives.Add(GetDirective(ed))
            Next
            PopulateUserData(CompileUnit, Xml)
            Return CompileUnit
        End Function
        ''' <summary>Populates <see cref="CodeObject.UserData"/> from element <c>&lt;UserData></c> in given <see cref="XElement"/></summary>
        ''' <param name="Obj"><see cref="CodeObject"/> to popualte <see cref="CodeObject.UserData"/> of</param>
        ''' <param name="Xml">Element of type <c>CodeObject</c> to populate <paramref name="Obj"/> from</param>
        Private Sub PopulateUserData(ByVal Obj As CodeObject, ByVal Xml As XElement)
            For Each kwp In Xml.<UserData>.<item>
                Obj.UserData.Add(kwp.@key, kwp.Value)
            Next
        End Sub
        ''' <summary>Gets <see cref="CodeLinePragma"/> represented by XML element</summary>
        ''' <param name="Xml">XML element <c>&lt;LinePragma></c></param>
        ''' <returns><see cref="CodeLinePragma"/> represented by <paramref name="Xml"/></returns>
        Private Function GetLinePragma(ByVal Xml As XElement) As CodeLinePragma
            Dim ret As New CodeLinePragma(Xml.@FileName, Xml.@LineNumber)
            Return ret
        End Function
        ''' <summary>Gets <see cref="CodeDirective"/> form XML element <c>&lt;Directive></c> of type <c>ChecksumPragma</c> or <c>RegionDirective</c> or form element <c>&lt;ChecksumPragma></c> or <c>&lt;RegionDirective></c></summary>
        ''' <param name="Xml">XML element which represents directive</param>
        ''' <returns><see cref="CodeDirective"/> represented by <paramref name="Xml"/></returns>
        Private Function GetDirective(ByVal Xml As XElement) As CodeDirective
            Dim dir As CodeDirective = Nothing
            If Xml.Name = Names.ChecksumPragma OrElse (Xml.Attribute(xsiType) IsNot Nothing AndAlso Xml.Attribute(xsiType).AsName = Names.ChecksumPragma) Then
                dir = New CodeChecksumPragma(Xml.@FieldName, New Guid(Xml.@ChecksumAlgorithmId), Nothing)
                For Each chd In Xml.<ChecksumData> 'Only once
                    DirectCast(dir, CodeChecksumPragma).ChecksumData = Convert.FromBase64String(chd.Value)
                Next
            ElseIf Xml.Name = Names.RegionDirective OrElse (Xml.Attribute(xsiType) IsNot Nothing AndAlso Xml.Attribute(xsiType).AsName = Names.RegionDirective) Then
                dir = New CodeRegionDirective()
                Select Case Xml.@RegionMode
                    Case "None" : DirectCast(dir, CodeRegionDirective).RegionMode = CodeRegionMode.None
                    Case "Start" : DirectCast(dir, CodeRegionDirective).RegionMode = CodeRegionMode.Start
                    Case "End" : DirectCast(dir, CodeRegionDirective).RegionMode = CodeRegionMode.End
                End Select
                If Xml.@RegionText IsNot Nothing Then DirectCast(dir, CodeRegionDirective).RegionText = Xml.@RegionText
            End If
            PopulateUserData(dir, Xml)
            Return dir
        End Function
        ''' <summary>Gets value of the <c>&lt;AssemblyReference></c> element</summary>
        ''' <param name="Xml">The <c>&lt;AssemblyReference></c> element</param>
        ''' <returns><see cref="XElement.Value"/> of <paramref name="Xml"/></returns>
        Private Function GetAssemblyReference(ByVal Xml As XElement) As String
            Return Xml.Value
        End Function
        ''' <summary>Gets <see cref="CodeAttributeDeclaration"/> from <c>&lt;AttributeDeclaration></c> element</summary>
        ''' <param name="Xml">The <c>&lt;AttributeDeclaration></c> element</param>
        ''' <returns><see cref="CodeAttributeDeclaration"/> represented by <paramref name="Xml"/></returns>
        Private Function GetAttributeDeclaration(ByVal Xml As XElement) As CodeAttributeDeclaration
            Dim attrd As CodeAttributeDeclaration = Nothing
            If Not Xml.<Name>.IsEmpty Then
                attrd = New CodeAttributeDeclaration(Xml.<Name>.Value)
            ElseIf Not Xml.<AttributeType>.IsEmpty Then
                attrd = New CodeAttributeDeclaration(GetTypeReference(Xml.<AttributeType>.First))
            End If
            For Each arg In Xml.<Arguments>.Elements
                attrd.Arguments.Add(GetAttributeArgument(arg))
            Next
            Return attrd
        End Function
        ''' <summary>Gets <see cref="CodeNamespace"/> from <c>&lt;Namespace></c> element</summary>
        ''' <param name="Xml">The <c>&lt;Namespace></c> element</param>
        ''' <returns><see cref="CodeNamespace"/> represented by <paramref name="Xml"/></returns>
        Private Function GetNamespace(ByVal Xml As XElement) As CodeNamespace
            Dim ns As CodeNamespace
            If Xml.@Name IsNot Nothing Then
                ns = New CodeNamespace(Xml.@Name)
            Else
                ns = New CodeNamespace
            End If
            For Each imp In Xml.<Imports>.Elements
                ns.Imports.Add(GetNamespaceImport(imp))
            Next
            For Each comm In Xml.<Comments>.Elements
                ns.Comments.Add(GetCommentStatement(comm))
            Next
            For Each type In Xml.<Types>.Elements
                ns.Types.Add(GetTypeDeclaration(type))
            Next
            PopulateUserData(ns, Xml)
            Return ns
        End Function
        ''' <summary>Gets <see cref="CodeTypeReference"/> from <c>&lt;TypeReference></c> element</summary>
        ''' <param name="Xml">The <c>&lt;TypeReference></c> element</param>
        ''' <returns><see cref="CodeTypeReference"/> represented by <paramref name="Xml"/></returns>
        Private Function GetTypeReference(ByVal Xml As XElement) As CodeTypeReference
            Dim tr As New CodeTypeReference()
            For Each aet In Xml.<ArrayElementType> 'Maxmimally once
                tr.ArrayElementType = GetTypeReference(aet)
            Next
            For Each tya In Xml.<TypeArguments>.Elements
                tr.TypeArguments.Add(GetTypeReference(tya))
            Next
            If Xml.@BaseType IsNot Nothing Then tr.BaseType = Xml.@BaseType
            If Xml.@ArrayRank IsNot Nothing Then tr.ArrayRank = Integer.Parse(Xml.@ArrayRank, CultureInfo.InvariantCulture)
            If Xml.@Options IsNot Nothing Then
                Select Case Xml.@Options
                    Case "GlobalReference" : tr.Options = CodeTypeReferenceOptions.GlobalReference
                    Case "GenericTypeParameter" : tr.Options = CodeTypeReferenceOptions.GenericTypeParameter
                End Select
            End If
            PopulateUserData(tr, Xml)
            Return tr
        End Function
        ''' <summary>Gets <see cref="CodeAttributeArgument"/> from <c>&lt;AttributeArgument></c> element</summary>
        ''' <param name="Xml">The <c>&lt;AttributeArgument></c> element</param>
        ''' <returns><see cref="CodeAttributeArgument"/> represented by <paramref name="Xml"/></returns>
        Private Function GetAttributeArgument(ByVal Xml As XElement) As CodeAttributeArgument
            Dim aa As New CodeAttributeArgument()
            If Xml.@Name IsNot Nothing Then aa.Name = Xml.@Name
            aa.Value = GetExpression(Xml.<Value>.First)
            Return aa
        End Function
        ''' <summary>Gets <see cref="CodeNamespaceImport"/> from <c>&lt;NamespaceImport></c> element</summary>
        ''' <param name="Xml">The <c>&lt;NamespaceImport></c> element</param>
        ''' <returns><see cref="CodeNamespaceImport"/> represented by <paramref name="Xml"/></returns>
        Private Function GetNamespaceImport(ByVal Xml As XElement) As CodeNamespaceImport
            Dim ns As New CodeNamespaceImport(Xml.@Namespace)
            For Each lp In Xml.<LinePragma> 'Maximally once
                ns.LinePragma = GetLinePragma(lp)
            Next
            PopulateUserData(ns, Xml)
            Return ns
        End Function
        ''' <summary>Gets <see cref="CodeCommentStatement"/> from <c>&lt;CommentStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;CommentStatement></c> element</param>
        ''' <returns><see cref="CodeCommentStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetCommentStatement(ByVal Xml As XElement) As CodeCommentStatement
            Dim cs As New CodeCommentStatement(GetComment(Xml.<Comment>.First))
            PopulateStatement(cs, Xml)
            Return cs
        End Function
        ''' <summary>Populates properties of given <see cref="CodeStatement"/> from given <see cref="XElement"/></summary>
        ''' <param name="Statement">Statement to populate</param>
        ''' <param name="Xml">Source of properties</param>
        Private Sub PopulateStatement(ByVal Statement As CodeStatement, ByVal Xml As XElement)
            For Each lp In Xml.<LinePragma> 'Maximally once
                Statement.LinePragma = GetLinePragma(lp)
            Next
            For Each sd In Xml.<StartDirectives>.Elements
                Statement.StartDirectives.Add(GetDirective(sd))
            Next
            For Each ed In Xml.<EndDirectives>.Elements
                Statement.EndDirectives.Add(GetDirective(ed))
            Next
            PopulateUserData(Statement, Xml)
        End Sub
        ''' <summary>Gets <see cref="CodeTypeDeclaration"/> from <c>&lt;TypeDeclaration></c> element</summary>
        ''' <param name="Xml">The <c>&lt;TypeDeclaration></c> element</param>
        ''' <returns><see cref="CodeTypeDeclaration"/> represented by <paramref name="Xml"/></returns>
        Private Function GetTypeDeclaration(ByVal Xml As XElement) As CodeTypeDeclaration
            Dim td As CodeTypeDeclaration
            If Xml.Name = Names.TypeDelegate OrElse (Xml.Attribute(xsiType) IsNot Nothing AndAlso Xml.Attribute(xsiType).AsName = Names.TypeDelegate) Then
                td = New CodeTypeDelegate()
                For Each par In Xml.<Parameters>.Elements
                    DirectCast(td, CodeTypeDelegate).Parameters.Add(GetParameterDeclarationExpression(par))
                Next
                For Each rt In Xml.<ReturnType> 'Maximally once
                    DirectCast(td, CodeTypeDelegate).ReturnType = GetTypeReference(rt)
                Next
            Else
                td = New CodeTypeDeclaration
            End If
            For Each tp In Xml.<TypeParameters>.Elements
                td.TypeParameters.Add(GetTypeParameter(tp))
            Next
            For Each bt In Xml.<BaseTypes>.Elements
                td.BaseTypes.Add(GetTypeReference(bt))
            Next
            For Each m In Xml.<Members>.Elements
                td.Members.Add(GetTypeMember(m))
            Next
            If Xml.@TypeAttributes IsNot Nothing Then
                Dim First As Boolean = True
                For Each ta In Xml.@TypeAttributes.WhiteSpaceSplit
                    Dim Attr As TypeAttributes = TypeTools.GetValue(Of TypeAttributes)(ta)
                    If First Then td.TypeAttributes = Attr Else td.TypeAttributes = td.TypeAttributes Or Attr
                    First = False
                Next
            End If
            If Xml.@IsClass IsNot Nothing Then td.IsClass = GetBool(Xml.@IsClass)
            If Xml.@IsEnum IsNot Nothing Then td.IsEnum = GetBool(Xml.@IsEnum)
            If Xml.@IsInterface IsNot Nothing Then td.IsInterface = GetBool(Xml.@IsInterface)
            If Xml.@IsPartial IsNot Nothing Then td.IsPartial = GetBool(Xml.@IsPartial)
            If Xml.@IsStruct IsNot Nothing Then td.IsStruct = GetBool(Xml.@IsStruct)
            PopulateMember(td, Xml)
            Return td
        End Function
        ''' <summary>Populates properties of given <see cref="CodeTypeMember"/> from given <see cref="XElement"/></summary>
        ''' <param name="Member">Member to populate properties of</param>
        ''' <param name="Xml">Xml element containing values of properties</param>
        Private Sub PopulateMember(ByVal Member As CodeTypeMember, ByVal Xml As XElement)
            For Each ca In Xml.<CustomAttributes>.Elements
                Member.CustomAttributes.Add(GetAttributeDeclaration(ca))
            Next
            For Each cmt In Xml.<Comments>.Elements
                Member.Comments.Add(GetCommentStatement(cmt))
            Next
            For Each sd In Xml.<StartDirectives>.Elements
                Member.StartDirectives.Add(GetDirective(sd))
            Next
            For Each lp In Xml.<LinePragma> 'Maximally once
                Member.LinePragma = GetLinePragma(lp)
            Next
            For Each ed In Xml.<EndDirectives>
                Member.EndDirectives.Add(GetDirective(ed))
            Next
            If Xml.@Attributes IsNot Nothing Then
                Dim IsFirst As Boolean = True
                For Each ma In Xml.@Attributes.WhiteSpaceSplit
                    Dim att As MemberAttributes = TypeTools.GetValue(Of MemberAttributes)(ma)
                    If IsFirst Then Member.Attributes = att Else Member.Attributes = Member.Attributes Or att
                    IsFirst = False
                Next
            End If
            Member.Name = Xml.@Name
            PopulateUserData(Member, Xml)
        End Sub
        ''' <summary>Converts XML boolean value to <see cref="Boolean"/></summary>
        ''' <param name="XmlValue">Value to be converted</param>
        ''' <returns><see cref="Boolean"/> equivalent of <paramref name="XmlValue"/></returns>
        ''' <exception cref="ArgumentException"><paramref name="XmlValue"/> does not represent valid XML boolean value</exception>
        Private Function GetBool(ByVal XmlValue$) As Boolean
            Select Case XmlValue
                Case "1", "true" : Return True
                Case "0", "false" : Return False
            End Select
            Throw New ArgumentException("Invalid XML boolean value")
        End Function
        ''' <summary>Gets <see cref="CodeExpression"/> from <c>&lt;Expression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;Expression></c> element</param>
        ''' <returns><see cref="CodeExpression"/> represented by <paramref name="Xml"/></returns>
        ''' <exception cref="ArgumentException"><paramref name="Xml"/> does not represent supported type of expression.</exception>
        Private Function GetExpression(ByVal Xml As XElement) As CodeExpression
            Dim name As XName
            If Xml.Attribute(xsiType) IsNot Nothing Then name = Xml.Attribute(xsiType).AsName Else name = Xml.Name
            Select Case name
                Case Names.ArgumentReferenceExpression : Return GetArgumentReferenceExpression(Xml)
                Case Names.ArrayCreateExpression : Return GetArrayCreateExpression(Xml)
                Case Names.ArrayIndexerExpression : Return GetArrayIndexerExpression(Xml)
                Case Names.BaseReferenceExpression : Return GetBaseReferenceExpression(Xml)
                Case Names.BinaryOperatorExpression : Return GetBinaryOperatorExpression(Xml)
                Case Names.CastExpression : Return GetCastExpression(Xml)
                Case Names.DefaultValueExpression : Return GetDefaultValueExpression(Xml)
                Case Names.DelegateCreateExpression : Return GetDelegateCreateExpression(Xml)
                Case Names.DelegateInvokeExpression : Return GetDelegateInvokeExpression(Xml)
                Case Names.DirectionExpression : Return GetDirectionExpression(Xml)
                Case Names.EventReferenceExpression : Return GetEventReferenceExpression(Xml)
                Case Names.FieldReferenceExpression : Return GetFieldReferenceExpression(Xml)
                Case Names.IndexerExpression : Return GetIndexerExpression(Xml)
                Case Names.MethodInvokeExpression : Return GetMethodInvokeExpression(Xml)
                Case Names.MethodReferenceExpression : Return GetMethodReferenceExpression(Xml)
                Case Names.ObjectCreateExpression : Return GetObjectCreateExpression(Xml)
                Case Names.ParameterDeclarationExpression : Return GetParameterDeclarationExpression(Xml)
                Case Names.PrimitiveExpression : Return GetPrimitiveExpression(Xml)
                Case Names.PropertyReferenceExpression : Return GetPropertyReferenceExpression(Xml)
                Case Names.PropertySetValueReferenceExpression : Return GetPropertySetValueReferenceExpression(Xml)
                Case Names.SnippetExpression : Return GetSnippetExpression(Xml)
                Case Names.ThisReferenceExpression : Return GetThisReferenceExpression(Xml)
                Case Names.TypeOfExpression : Return GetTypeOfExpression(Xml)
                Case Names.TypeReferenceExpression : Return GetTypeReferenceExpression(Xml)
                Case Names.VariableReferenceExpression : Return GetVariableReferenceExpression(Xml)
            End Select
            Throw New ArgumentException(ResourcesT.Exceptions.DoesNotRepresentSupportedExpression.f(name.LocalName), "Xml")
        End Function
        ''' <summary>Gets <see cref="CodeComment"/> from <c>&lt;Comment></c> element</summary>
        ''' <param name="Xml">The <c>&lt;Comment></c> element</param>
        ''' <returns><see cref="CodeComment"/> represented by <paramref name="Xml"/></returns>
        Private Function GetComment(ByVal Xml As XElement) As CodeComment
            Dim cmt As New CodeComment
            If Not Xml.<Text>.IsEmpty Then
                cmt.Text = Xml.<Text>.Value
            ElseIf Not Xml.<TextXml>.IsEmpty Then
                cmt.Text = Xml.<TextXml>.First.InnerXml
            End If
            If Xml.@DocComment IsNot Nothing Then cmt.DocComment = GetBool(Xml.@DocComment)
            Return cmt
        End Function
        ''' <summary>Gets <see cref="CodeParameterDeclarationExpression"/> from <c>&lt;ParameterDeclarationExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;ParameterDeclarationExpression></c> element</param>
        ''' <returns><see cref="CodeParameterDeclarationExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetParameterDeclarationExpression(ByVal Xml As XElement) As CodeParameterDeclarationExpression
            Dim pde As New CodeParameterDeclarationExpression()
            For Each ca In Xml.<CustomAttributes>.Elements
                pde.CustomAttributes.Add(GetAttributeDeclaration(ca))
            Next
            pde.Type = GetTypeReference(Xml.<Type>.First)
            If Xml.@Name IsNot Nothing Then pde.Name = Xml.@Name
            If Xml.@Direction IsNot Nothing Then
                Dim First As Boolean = True
                For Each fd In Xml.@Direction.WhiteSpaceSplit
                    Dim dir = TypeTools.GetValue(Of FieldDirection)(fd)
                    If First Then pde.Direction = dir OrElse pde.Direction = pde.Direction Or dir
                    First = False
                Next
            End If
            PopulateExpression(pde, Xml)
            Return pde
        End Function
        ''' <summary>Populates properties of given <see cref="CodeExpression"/> from given <see cref="XElement"/></summary>
        ''' <param name="Exp">Expression to populate properties of</param>
        ''' <param name="Xml">XML element containing property data</param>
        Private Sub PopulateExpression(ByVal Exp As CodeExpression, ByVal Xml As XElement)
            PopulateUserData(Exp, Xml)
        End Sub
        ''' <summary>Gets <see cref="CodeTypeParameter"/> from <c>&lt;TypeParameter></c> element</summary>
        ''' <param name="Xml">The <c>&lt;TypeParameter></c> element</param>
        ''' <returns><see cref="CodeTypeParameter"/> represented by <paramref name="Xml"/></returns>
        Private Function GetTypeParameter(ByVal Xml As XElement) As CodeTypeParameter
            Dim tp As New CodeTypeParameter(Xml.@Name)
            For Each cns In Xml.<Constraints>.Elements
                tp.Constraints.Add(GetTypeReference(cns))
            Next
            For Each ca In Xml.<CustomAttributes>.Elements
                tp.CustomAttributes.Add(GetAttributeDeclaration(ca))
            Next
            If Not Xml.@HasConstructorConstraint Is Nothing Then tp.HasConstructorConstraint = GetBool(Xml.@HasConstructorConstraint)
            PopulateUserData(tp, Xml)
            Return tp
        End Function
        ''' <summary>Gets <see cref="CodeTypeMember"/> from <c>&lt;TypeMember></c> element</summary>
        ''' <param name="Xml">The <c>&lt;TypeMember></c> element</param>
        ''' <returns><see cref="CodeTypeMember"/> represented by <paramref name="Xml"/></returns>
        Private Function GetTypeMember(ByVal Xml As XElement) As CodeTypeMember
            Dim name As XName
            If Xml.Attribute(xsiType) IsNot Nothing Then name = Xml.Attribute(xsiType).AsName Else name = Xml.Name
            Select Case name
                Case Names.TypeDeclaration, Names.TypeDelegate : Return GetTypeDeclaration(Xml)
                Case Names.MemberEvent : Return GetMemberEvent(Xml)
                Case Names.MemberField : Return GetMemberField(Xml)
                Case Names.MemberMethod, Names.EntryPointMethod, Names.Constructor, Names.TypeConstructor : Return GetMemberMethod(Xml)
                Case Names.SnippetTypeMember : Return GetSnippetTypeMember(Xml)
                Case Names.MemberProperty : Return GetMemberProperty(Xml)
            End Select
            Return Nothing
        End Function
        ''' <summary>Gets <see cref="CodeArgumentReferenceExpression"/> from <c>&lt;ArgumentReferenceExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;ArgumentReferenceExpression></c> element</param>
        ''' <returns><see cref="CodeArgumentReferenceExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetArgumentReferenceExpression(ByVal Xml As XElement) As CodeArgumentReferenceExpression
            Dim are As New CodeArgumentReferenceExpression(Xml.@ParameterName)
            PopulateExpression(are, Xml)
            Return are
        End Function
        ''' <summary>Gets <see cref="CodeArrayCreateExpression"/> from <c>&lt;ArrayCreateExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;ArrayCreateExpression></c> element</param>
        ''' <returns><see cref="CodeArrayCreateExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetArrayCreateExpression(ByVal Xml As XElement) As CodeArrayCreateExpression
            Dim ace As New CodeArrayCreateExpression
            For Each ct In Xml.<CreateType> 'Only once
                ace.CreateType = GetTypeReference(ct)
            Next
            For Each siz In Xml.<SizeExpression> 'Maximally once
                ace.SizeExpression = GetExpression(siz)
            Next
            For Each init In Xml.<Initializers>.Elements
                ace.Initializers.Add(GetExpression(init))
            Next
            If Xml.@Size IsNot Nothing Then ace.Size = Integer.Parse(Xml.@Size, CultureInfo.InvariantCulture)
            PopulateExpression(ace, Xml)
            Return ace
        End Function
        ''' <summary>Gets <see cref="CodeArrayIndexerExpression"/> from <c>&lt;ArrayIndexerExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;ArrayIndexerExpression></c> element</param>
        ''' <returns><see cref="CodeArrayIndexerExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetArrayIndexerExpression(ByVal Xml As XElement) As CodeArrayIndexerExpression
            Dim aie As New CodeArrayIndexerExpression(GetExpression(Xml.<TagretObject>.First))
            For Each ind In Xml.<Indices>.Elements
                aie.Indices.Add(GetExpression(ind))
            Next
            PopulateExpression(aie, Xml)
            Return aie
        End Function
        ''' <summary>Gets <see cref="CodeBaseReferenceExpression"/> from <c>&lt;BaseReferenceExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;BaseReferenceExpression></c> element</param>
        ''' <returns><see cref="CodeBaseReferenceExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetBaseReferenceExpression(ByVal Xml As XElement) As CodeBaseReferenceExpression
            Dim bre As New CodeBaseReferenceExpression
            PopulateExpression(bre, Xml)
            Return bre
        End Function
        ''' <summary>Gets <see cref="CodeBinaryOperatorExpression"/> from <c>&lt;BinaryOperatorExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;BinaryOperatorExpression></c> element</param>
        ''' <returns><see cref="CodeBinaryOperatorExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetBinaryOperatorExpression(ByVal Xml As XElement) As CodeBinaryOperatorExpression
            Dim boe As New CodeBinaryOperatorExpression(GetExpression(Xml.<Left>.First), TypeTools.GetValue(Of CodeBinaryOperatorType)(Xml.@Operator), GetExpression(Xml.<Right>.First))
            PopulateExpression(boe, Xml)
            Return boe
        End Function
        ''' <summary>Gets <see cref="CodeCastExpression"/> from <c>&lt;CastExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;CastExpression></c> element</param>
        ''' <returns><see cref="CodeCastExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetCastExpression(ByVal Xml As XElement) As CodeCastExpression
            Dim ce As New CodeCastExpression(GetTypeReference(Xml.<TargetType>.First), GetExpression(Xml.<Expression>.First))
            PopulateExpression(ce, Xml)
            Return ce
        End Function
        ''' <summary>Gets <see cref="CodeDefaultValueExpression"/> from <c>&lt;DefaultValueExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;DefaultValueExpression></c> element</param>
        ''' <returns><see cref="CodeDefaultValueExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetDefaultValueExpression(ByVal Xml As XElement) As CodeDefaultValueExpression
            Dim dve As New CodeDefaultValueExpression(GetTypeReference(Xml.<Type>.First))
            PopulateExpression(dve, Xml)
            Return dve
        End Function
        ''' <summary>Gets <see cref="CodeDelegateCreateExpression"/> from <c>&lt;DelegateCreateExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;DelegateCreateExpression></c> element</param>
        ''' <returns><see cref="CodeDelegateCreateExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetDelegateCreateExpression(ByVal Xml As XElement) As CodeDelegateCreateExpression
            Dim dce As New CodeDelegateCreateExpression(GetTypeReference(Xml.<DelegateType>.First), GetExpression(Xml.<TargetObject>.First), Xml.@MethodName)
            PopulateExpression(dce, Xml)
            Return dce
        End Function
        ''' <summary>Gets <see cref="CodeDelegateInvokeExpression"/> from <c>&lt;DelegateInvokeExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;DelegateInvokeExpression></c> element</param>
        ''' <returns><see cref="CodeDelegateInvokeExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetDelegateInvokeExpression(ByVal Xml As XElement) As CodeDelegateInvokeExpression
            Dim die As New CodeDelegateInvokeExpression(GetExpression(Xml.<TargetObject>.First))
            For Each par In Xml.<Parameters>.Elements
                die.Parameters.Add(GetExpression(par))
            Next
            PopulateExpression(die, Xml)
            Return die
        End Function
        ''' <summary>Gets <see cref="CodeDirectionExpression"/> from <c>&lt;DirectionExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;DirectionExpression></c> element</param>
        ''' <returns><see cref="CodeDirectionExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetDirectionExpression(ByVal Xml As XElement) As CodeDirectionExpression
            Dim die As New CodeDirectionExpression()
            For Each ex In Xml.<Expression> 'Maximally once
                die.Expression = GetExpression(ex)
            Next
            If Xml.@Direction IsNot Nothing Then die.Direction = TypeTools.GetValue(Of FieldDirection)(Xml.@Direction)
            PopulateExpression(die, Xml)
            Return die
        End Function
        ''' <summary>Gets <see cref="CodeEventReferenceExpression"/> from <c>&lt;EventReferenceExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;EventReferenceExpression></c> element</param>
        ''' <returns><see cref="CodeEventReferenceExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetEventReferenceExpression(ByVal Xml As XElement) As CodeEventReferenceExpression
            Dim ere As New CodeEventReferenceExpression(GetExpression(Xml.<TargetObject>.First), Xml.@EventName)
            PopulateExpression(ere, Xml)
            Return ere
        End Function
        ''' <summary>Gets <see cref="CodeFieldReferenceExpression"/> from <c>&lt;FieldReferenceExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;FieldReferenceExpression></c> element</param>
        ''' <returns><see cref="CodeFieldReferenceExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetFieldReferenceExpression(ByVal Xml As XElement) As CodeFieldReferenceExpression
            Dim fre As New CodeFieldReferenceExpression(GetExpression(Xml.<TargetObject>.First), Xml.@FieldName)
            PopulateExpression(fre, Xml)
            Return fre
        End Function
        ''' <summary>Gets <see cref="CodeIndexerExpression"/> from <c>&lt;IndexerExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;IndexerExpression></c> element</param>
        ''' <returns><see cref="CodeIndexerExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetIndexerExpression(ByVal Xml As XElement) As CodeIndexerExpression
            Dim ixe As New CodeIndexerExpression(GetExpression(Xml.<TargetObject>.First))
            For Each ix In Xml.<Indices>.Elements
                ixe.Indices.Add(GetExpression(ix))
            Next
            PopulateExpression(ixe, Xml)
            Return ixe
        End Function
        ''' <summary>Gets <see cref="CodeMethodInvokeExpression"/> from <c>&lt;MethodInvokeExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;MethodInvokeExpression></c> element</param>
        ''' <returns><see cref="CodeMethodInvokeExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetMethodInvokeExpression(ByVal Xml As XElement) As CodeMethodInvokeExpression
            Dim mie As New CodeMethodInvokeExpression(GetMethodReferenceExpression(Xml.<Method>.First))
            For Each par In Xml.<Parameters>.Elements
                mie.Parameters.Add(GetExpression(par))
            Next
            PopulateExpression(mie, Xml)
            Return mie
        End Function
        ''' <summary>Gets <see cref="CodeMethodReferenceExpression"/> from <c>&lt;MethodReferenceExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;MethodReferenceExpression></c> element</param>
        ''' <returns><see cref="CodeMethodReferenceExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetMethodReferenceExpression(ByVal Xml As XElement) As CodeMethodReferenceExpression
            Dim mre As New CodeMethodReferenceExpression(GetExpression(Xml.<TargetObject>.First), Xml.@MethodName)
            For Each tya In Xml.<TypeArguments>.Elements
                mre.TypeArguments.Add(GetTypeReference(tya))
            Next
            PopulateExpression(mre, Xml)
            Return mre
        End Function
        ''' <summary>Gets <see cref="CodeObjectCreateExpression"/> from <c>&lt;ObjectCreateExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;ObjectCreateExpression></c> element</param>
        ''' <returns><see cref="CodeObjectCreateExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetObjectCreateExpression(ByVal Xml As XElement) As CodeObjectCreateExpression
            Dim oce As New CodeObjectCreateExpression(GetTypeReference(Xml.<CreateType>.First))
            For Each param In Xml.<Parameters>.Elements
                oce.Parameters.Add(GetExpression(param))
            Next
            PopulateExpression(oce, Xml)
            Return oce
        End Function
        ''' <summary>Gets <see cref="CodePrimitiveExpression"/> from <c>&lt;PrimitiveExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;PrimitiveExpression></c> element</param>
        ''' <returns><see cref="CodePrimitiveExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetPrimitiveExpression(ByVal Xml As XElement) As CodePrimitiveExpression
            Dim pie As New CodePrimitiveExpression()
            With Xml.Elements.First
                Select Case .Name
                    Case Names.String : pie.Value = .Value
                    Case Names.Char : pie.Value = CChar(.Value)
                    Case Names.Byte : pie.Value = Byte.Parse(.Value, NumberStyles.Integer And Not NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture)
                    Case Names.SByte : pie.Value = SByte.Parse(.Value, NumberStyles.Integer, CultureInfo.InvariantCulture)
                    Case Names.Int16 : pie.Value = Int16.Parse(.Value, NumberStyles.Integer, CultureInfo.InvariantCulture)
                    Case Names.UInt16 : pie.Value = UInt16.Parse(.Value, NumberStyles.Integer And Not NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture)
                    Case Names.Int32 : pie.Value = Int32.Parse(.Value, NumberStyles.Integer, CultureInfo.InvariantCulture)
                    Case Names.UInt32 : pie.Value = UInt32.Parse(.Value, NumberStyles.Integer And Not NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture)
                    Case Names.Int64 : pie.Value = Int64.Parse(.Value, NumberStyles.Integer, CultureInfo.InvariantCulture)
                    Case Names.UInt64 : pie.Value = UInt64.Parse(.Value, NumberStyles.Integer And Not NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture)
                    Case Names.Decimal : pie.Value = Decimal.Parse(.Value, NumberStyles.Integer Or NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture)
                    Case Names.Single
                        Select Case .Value
                            Case "-INF" : pie.Value = Single.NegativeInfinity
                            Case "INF" : pie.Value = Single.PositiveInfinity
                            Case "NaN" : pie.Value = Single.NaN
                            Case Else : pie.Value = Single.Parse(.Value, NumberStyles.Float, CultureInfo.InvariantCulture)
                        End Select
                    Case Names.Double
                        Select Case .Value
                            Case "-INF" : pie.Value = Double.NegativeInfinity
                            Case "INF" : pie.Value = Double.PositiveInfinity
                            Case "NaN" : pie.Value = Double.NaN
                            Case Else : pie.Value = Double.Parse(.Value, NumberStyles.Float, CultureInfo.InvariantCulture)
                        End Select
                    Case Names.DateTime
                        Dim [date] = DateTime.ParseExact(.Value, New String() {"yyyy-MM-dd'T'hh:mm:ss.ffffffff", "yyyy-MM-dd'T'hh:mm:ss.fffffff", "yyyy-MM-dd'T'hh:mm:ss.ffffff", "yyyy-MM-dd'T'hh:mm:ss.fffff", "yyyy-MM-dd'T'hh:mm:ss.ffff", "yyyy-MM-dd'T'hh:mm:ss.fff", "yyyy-MM-dd'T'hh:mm:ss.ff", "yyyy-MM-dd'T'hh:mm:ss.f", "yyyy-MM-dd'T'hh:mm:ss", "yyyy-MM-dd'T'hh:mm:ss.ffffffff'Z'", "yyyy-MM-dd'T'hh:mm:ss.fffffff'Z'", "yyyy-MM-dd'T'hh:mm:ss.ffffff'Z'", "yyyy-MM-dd'T'hh:mm:ss.fffff'Z'", "yyyy-MM-dd'T'hh:mm:ss.ffff'Z'", "yyyy-MM-dd'T'hh:mm:ss.fff'Z'", "yyyy-MM-dd'T'hh:mm:ss.ff'Z'", "yyyy-MM-dd'T'hh:mm:ss.f'Z'", "yyyy-MM-dd'T'hh:mm:ss'Z'", "yyyy-MM-dd'T'hh:mm:ss.ffffffffzzz", "yyyy-MM-dd'T'hh:mm:ss.fffffffzzz", "yyyy-MM-dd'T'hh:mm:ss.ffffffzzz", "yyyy-MM-dd'T'hh:mm:ss.fffffzzz", "yyyy-MM-dd'T'hh:mm:ss.ffffzzz", "yyyy-MM-dd'T'hh:mm:ss.fffzzz", "yyyy-MM-dd'T'hh:mm:ss.ffzzz", "yyyy-MM-dd'T'hh:mm:ss.fzzz", "yyyy-MM-dd'T'hh:mm:sszzz"}, CultureInfo.InvariantCulture, DateTimeStyles.AllowLeadingWhite Or DateTimeStyles.AllowTrailingWhite)
                        If .Value.EndsWith("Z"c) Then [date] = New DateTime([date].Year, [date].Month, [date].Day, [date].Hour, [date].Minute, [date].Second, [date].Millisecond, DateTimeKind.Utc)
                        pie.Value = [date]
                    Case Names.Null : pie.Value = Nothing
                    Case Names.Boolean : pie.Value = GetBool(.Value)
                End Select
            End With
            PopulateExpression(pie, Xml)
            Return pie
        End Function
        ''' <summary>Gets <see cref="CodePropertyReferenceExpression"/> from <c>&lt;PropertyReferenceExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;PropertyReferenceExpression></c> element</param>
        ''' <returns><see cref="CodePropertyReferenceExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetPropertyReferenceExpression(ByVal Xml As XElement) As CodePropertyReferenceExpression
            Dim pre As New CodePropertyReferenceExpression(GetExpression(Xml.<TargetObject>.First), Xml.@PropertyName)
            PopulateExpression(pre, Xml)
            Return pre
        End Function
        ''' <summary>Gets <see cref="CodePropertySetValueReferenceExpression"/> from <c>&lt;PropertySetValueReferenceExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;PropertySetValueReferenceExpression></c> element</param>
        ''' <returns><see cref="CodePropertySetValueReferenceExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetPropertySetValueReferenceExpression(ByVal Xml As XElement) As CodePropertySetValueReferenceExpression
            Dim sve As New CodePropertySetValueReferenceExpression()
            PopulateExpression(sve, Xml)
            Return sve
        End Function
        ''' <summary>Gets <see cref="CodeSnippetExpression"/> from <c>&lt;SnippetExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;SnippetExpression></c> element</param>
        ''' <returns><see cref="CodeSnippetExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetSnippetExpression(ByVal Xml As XElement) As CodeSnippetExpression
            Dim sne As New CodeSnippetExpression(Xml.<Value>.Value)
            PopulateExpression(sne, Xml)
            Return sne
        End Function
        ''' <summary>Gets <see cref="CodeThisReferenceExpression"/> from <c>&lt;ThisReferenceExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;ThisReferenceExpression></c> element</param>
        ''' <returns><see cref="CodeThisReferenceExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetThisReferenceExpression(ByVal Xml As XElement) As CodeThisReferenceExpression
            Dim the As New CodeThisReferenceExpression()
            PopulateExpression(the, Xml)
            Return the
        End Function
        ''' <summary>Gets <see cref="CodeTypeOfExpression"/> from <c>&lt;TypeOfExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;TypeOfExpression></c> element</param>
        ''' <returns><see cref="CodeTypeOfExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetTypeOfExpression(ByVal Xml As XElement) As CodeTypeOfExpression
            Dim toe As New CodeTypeOfExpression(GetTypeReference(Xml.<Type>.First))
            PopulateExpression(toe, Xml)
            Return toe
        End Function
        ''' <summary>Gets <see cref="CodeTypeReferenceExpression"/> from <c>&lt;TypeReferenceExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;TypeReferenceExpression></c> element</param>
        ''' <returns><see cref="CodeTypeReferenceExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetTypeReferenceExpression(ByVal Xml As XElement) As CodeTypeReferenceExpression
            Dim tre As New CodeTypeReferenceExpression(GetTypeReference(Xml.<Type>.First))
            PopulateExpression(tre, Xml)
            Return tre
        End Function
        ''' <summary>Gets <see cref="CodeVariableReferenceExpression"/> from <c>&lt;VariableReferenceExpression></c> element</summary>
        ''' <param name="Xml">The <c>&lt;VariableReferenceExpression></c> element</param>
        ''' <returns><see cref="CodeVariableReferenceExpression"/> represented by <paramref name="Xml"/></returns>
        Private Function GetVariableReferenceExpression(ByVal Xml As XElement) As CodeVariableReferenceExpression
            Dim vre As New CodeVariableReferenceExpression(Xml.@VariableName)
            PopulateExpression(vre, Xml)
            Return vre
        End Function
        ''' <summary>Gets <see cref="CodeMemberEvent"/> from <c>&lt;MemberEvent></c> element</summary>
        ''' <param name="Xml">The <c>&lt;MemberEvent></c> element</param>
        ''' <returns><see cref="CodeMemberEvent"/> represented by <paramref name="Xml"/></returns>
        Private Function GetMemberEvent(ByVal Xml As XElement) As CodeMemberEvent
            Dim [me] As New CodeMemberEvent()
            [me].Type = GetTypeReference(Xml.<Type>.First)
            For Each it In Xml.<ImplementationTypes>.Elements
                [me].ImplementationTypes.Add(GetTypeReference(it))
            Next
            For Each pit In Xml.<PrivateImplementationType> 'Maximally once
                [me].PrivateImplementationType = GetTypeReference(pit)
            Next
            PopulateMember([me], Xml)
            Return [me]
        End Function
        ''' <summary>Gets <see cref="CodeMemberField"/> from <c>&lt;MemberField></c> element</summary>
        ''' <param name="Xml">The <c>&lt;MemberField></c> element</param>
        ''' <returns><see cref="CodeMemberField"/> represented by <paramref name="Xml"/></returns>
        Private Function GetMemberField(ByVal Xml As XElement) As CodeMemberField
            Dim mf As New CodeMemberField
            mf.Type = GetTypeReference(Xml.<Type>.First)
            For Each ie In Xml.<InitExpression> 'Maximally once
                mf.InitExpression = GetExpression(ie)
            Next
            PopulateMember(mf, Xml)
            Return mf
        End Function
        ''' <summary>Gets <see cref="CodeMemberMethod"/> from <c>&lt;MemberMethod></c> element</summary>
        ''' <param name="Xml">The <c>&lt;MemberMethod></c> element</param>
        ''' <returns><see cref="CodeMemberMethod"/> represented by <paramref name="Xml"/></returns>
        Private Function GetMemberMethod(ByVal Xml As XElement) As CodeMemberMethod
            Dim mm As CodeMemberMethod
            Dim name As XName
            If Xml.Attribute(xsiType) IsNot Nothing Then name = Xml.Attribute(xsiType).AsName Else name = Xml.Name
            Select Case name
                Case Names.EntryPointMethod : mm = New CodeEntryPointMethod
                Case Names.Constructor
                    mm = New CodeConstructor
                    With DirectCast(mm, CodeConstructor)
                        For Each bca In Xml.<BaseConstructorArgs>.Elements
                            .BaseConstructorArgs.Add(GetExpression(bca))
                        Next
                        For Each cca In Xml.<ChainedConstructorArgs>.Elements
                            .ChainedConstructorArgs.Add(GetExpression(cca))
                        Next
                    End With
                Case Names.TypeConstructor : mm = New CodeTypeConstructor
                Case Else : mm = New CodeMemberMethod
            End Select
            For Each param In Xml.<Parameters>.Elements
                mm.Parameters.Add(GetParameterDeclarationExpression(param))
            Next
            For Each rt In Xml.<ReturnType> 'Maximally once
                mm.ReturnType = GetTypeReference(rt)
            Next
            For Each tp In Xml.<TypeParameters>.Elements
                mm.TypeParameters.Add(GetTypeParameter(tp))
            Next
            For Each it In Xml.<ImplementationTypes>.Elements
                mm.ImplementationTypes.Add(GetTypeReference(it))
            Next
            For Each pit In Xml.<PrivateImplementationType> 'Maximally once
                mm.PrivateImplementationType = GetTypeReference(pit)
            Next
            For Each rtca In Xml.<ReturnTypeCustomAttributes>.Elements
                mm.ReturnTypeCustomAttributes.Add(GetAttributeDeclaration(rtca))
            Next
            For Each stm In Xml.<Statements>.Elements
                mm.Statements.Add(GetStatement(stm))
            Next
            PopulateMember(mm, Xml)
            Return mm
        End Function
        ''' <summary>Gets <see cref="CodeMemberProperty"/> from <c>&lt;MemberProperty></c> element</summary>
        ''' <param name="Xml">The <c>&lt;MemberProperty></c> element</param>
        ''' <returns><see cref="CodeMemberProperty"/> represented by <paramref name="Xml"/></returns>
        Private Function GetMemberProperty(ByVal Xml As XElement) As CodeMemberProperty
            Dim mp As New CodeMemberProperty
            For Each par In Xml.<Parameters>.Elements
                mp.Parameters.Add(GetParameterDeclarationExpression(par))
            Next
            mp.Type = GetTypeReference(Xml.<Type>.First)
            For Each it In Xml.<ImplementationTypes>.Elements
                mp.ImplementationTypes.Add(GetTypeReference(it))
            Next
            For Each pit In Xml.<PrivateImplementationType> 'Maximally once
                mp.PrivateImplementationType = GetTypeReference(pit)
            Next
            For Each gs In Xml.<GetStatements>.Elements
                mp.GetStatements.Add(GetStatement(gs))
            Next
            For Each ss In Xml.<SetStatements>.Elements
                mp.SetStatements.Add(GetStatement(ss))
            Next
            If Xml.@HasSet IsNot Nothing Then mp.HasSet = GetBool(Xml.@HasSet) Else mp.HasSet = Not Xml.<SetStatements>.IsEmpty
            If Xml.@HasGet IsNot Nothing Then mp.HasGet = GetBool(Xml.@HasGet) Else mp.HasGet = Not Xml.<GetStatements>.IsEmpty
            PopulateMember(mp, Xml)
            Return mp
        End Function
        ''' <summary>Gets <see cref="CodeSnippetTypeMember"/> from <c>&lt;SnippetTypeMember></c> element</summary>
        ''' <param name="Xml">The <c>&lt;SnippetTypeMember></c> element</param>
        ''' <returns><see cref="CodeSnippetTypeMember"/> represented by <paramref name="Xml"/></returns>
        Private Function GetSnippetTypeMember(ByVal Xml As XElement) As CodeSnippetTypeMember
            Dim sm As New CodeSnippetTypeMember(Xml.<Text>.Value)
            PopulateMember(sm, Xml)
            Return sm
        End Function
        ''' <summary>Gets <see cref="CodeStatement"/> from <c>&lt;Statement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;Statement></c> element</param>
        ''' <returns><see cref="CodeStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetStatement(ByVal Xml As XElement) As CodeStatement
            Dim name As XName
            If Xml.Attribute(xsiType) IsNot Nothing Then name = Xml.Attribute(xsiType).AsName Else name = Xml.Name
            Select Case name
                Case Names.AssignStatement : Return GetAssignStatement(Xml)
                Case Names.AttachEventStatement : Return GetAttachEventStatement(Xml)
                Case Names.CommentStatement : Return GetCommentStatement(Xml)
                Case Names.ConditionStatement : Return GetConditionStatement(Xml)
                Case Names.ExpressionStatement : Return GetExpressionStatement(Xml)
                Case Names.GotoStatement : Return GetGotoStatement(Xml)
                Case Names.IterationStatement : Return GetIterationStatement(Xml)
                Case Names.LabeledStatement : Return GetLabeledStatement(Xml)
                Case Names.MethodReturnStatement : Return GetMethodReturnStatement(Xml)
                Case Names.RemoveEventStatement : Return GetRemoveEventStatement(Xml)
                Case Names.SnippetStatement : Return GetSnippetStatement(Xml)
                Case Names.ThrowExceptionStatement : Return GetThrowExceptionStatement(Xml)
                Case Names.TryCatchFinallyStatement : Return GetTryCatchFinallyStatement(Xml)
                Case Names.VariableDeclarationStatement : Return GetVariableDeclarationStatement(Xml)
            End Select
            Return Nothing
        End Function
        ''' <summary>Gets <see cref="CodeAssignStatement"/> from <c>&lt;AssignStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;AssignStatement></c> element</param>
        ''' <returns><see cref="CodeAssignStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetAssignStatement(ByVal Xml As XElement) As CodeAssignStatement
            Dim ass As New CodeAssignStatement(GetExpression(Xml.<Left>.First), GetExpression(Xml.<Right>.First))
            PopulateStatement(ass, Xml)
            Return ass
        End Function
        ''' <summary>Gets <see cref="CodeAttachEventStatement"/> from <c>&lt;AttachEventStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;AttachEventStatement></c> element</param>
        ''' <returns><see cref="CodeAttachEventStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetAttachEventStatement(ByVal Xml As XElement) As CodeAttachEventStatement
            Dim aes As New CodeAttachEventStatement(GetEventReferenceExpression(Xml.<Event>.First), GetExpression(Xml.<Listener>.First))
            PopulateStatement(aes, Xml)
            Return aes
        End Function
        ''' <summary>Gets <see cref="CodeConditionStatement"/> from <c>&lt;ConditionStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;ConditionStatement></c> element</param>
        ''' <returns><see cref="CodeConditionStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetConditionStatement(ByVal Xml As XElement) As CodeConditionStatement
            Dim cs As New CodeConditionStatement(GetExpression(Xml.<Condition>.First))
            For Each ts In Xml.<TrueStatements>.Elements
                cs.TrueStatements.Add(GetStatement(ts))
            Next
            For Each fs In Xml.<FalseStatements>.Elements
                cs.FalseStatements.Add(GetStatement(fs))
            Next
            PopulateStatement(cs, Xml)
            Return cs
        End Function
        ''' <summary>Gets <see cref="CodeExpressionStatement"/> from <c>&lt;ExpressionStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;ExpressionStatement></c> element</param>
        ''' <returns><see cref="CodeExpressionStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetExpressionStatement(ByVal Xml As XElement) As CodeExpressionStatement
            Dim es As New CodeExpressionStatement(GetExpression(Xml.<Expression>.First))
            PopulateStatement(es, Xml)
            Return es
        End Function
        ''' <summary>Gets <see cref="CodeGotoStatement"/> from <c>&lt;GotoStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;GotoStatement></c> element</param>
        ''' <returns><see cref="CodeGotoStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetGotoStatement(ByVal Xml As XElement) As CodeGotoStatement
            Dim gs As New CodeGotoStatement(Xml.@Label)
            PopulateStatement(gs, Xml)
            Return gs
        End Function
        ''' <summary>Gets <see cref="CodeIterationStatement"/> from <c>&lt;IterationStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;IterationStatement></c> element</param>
        ''' <returns><see cref="CodeIterationStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetIterationStatement(ByVal Xml As XElement) As CodeIterationStatement
            Dim [is] As New CodeIterationStatement()
            For Each ins In Xml.<InitStatement> 'Maximally once
                [is].InitStatement = GetStatement(ins)
            Next
            For Each incs In Xml.<IncrementStatement> 'Maximally once
                [is].IncrementStatement = GetStatement(incs)
            Next
            [is].TestExpression = GetExpression(Xml.<TestExpression>.First)
            For Each stm In Xml.<Statements>.Elements
                [is].Statements.Add(GetStatement(stm))
            Next
            PopulateStatement([is], Xml)
            Return [is]
        End Function
        ''' <summary>Gets <see cref="CodeLabeledStatement"/> from <c>&lt;LabeledStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;LabeledStatement></c> element</param>
        ''' <returns><see cref="CodeLabeledStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetLabeledStatement(ByVal Xml As XElement) As CodeLabeledStatement
            Dim ls As New CodeLabeledStatement(Xml.@Label)
            For Each stm In Xml.<Statement> 'Maximally once
                ls.Statement = GetStatement(stm)
            Next
            PopulateStatement(ls, Xml)
            Return ls
        End Function
        ''' <summary>Gets <see cref="CodeMethodReturnStatement"/> from <c>&lt;MethodReturnStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;MethodReturnStatement></c> element</param>
        ''' <returns><see cref="CodeMethodReturnStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetMethodReturnStatement(ByVal Xml As XElement) As CodeMethodReturnStatement
            Dim mrs As New CodeMethodReturnStatement()
            For Each ex In Xml.<Expression> 'Maximally once
                mrs.Expression = GetExpression(ex)
            Next
            PopulateStatement(mrs, Xml)
            Return mrs
        End Function
        ''' <summary>Gets <see cref="CodeRemoveEventStatement"/> from <c>&lt;RemoveEventStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;RemoveEventStatement></c> element</param>
        ''' <returns><see cref="CodeRemoveEventStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetRemoveEventStatement(ByVal Xml As XElement) As CodeRemoveEventStatement
            Dim res As New CodeRemoveEventStatement(GetEventReferenceExpression(Xml.<Event>.First), GetExpression(Xml.<Listener>.First))
            PopulateStatement(res, Xml)
            Return res
        End Function
        ''' <summary>Gets <see cref="CodeSnippetStatement"/> from <c>&lt;SnippetStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;SnippetStatement></c> element</param>
        ''' <returns><see cref="CodeSnippetStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetSnippetStatement(ByVal Xml As XElement) As CodeSnippetStatement
            Dim ss As New CodeSnippetStatement(Xml.<Value>.Value)
            PopulateStatement(ss, Xml)
            Return ss
        End Function
        ''' <summary>Gets <see cref="CodeThrowExceptionStatement"/> from <c>&lt;ThrowExceptionStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;ThrowExceptionStatement></c> element</param>
        ''' <returns><see cref="CodeThrowExceptionStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetThrowExceptionStatement(ByVal Xml As XElement) As CodeThrowExceptionStatement
            Dim thexs As New CodeThrowExceptionStatement()
            For Each tt In Xml.<ToThrow> 'Maximally once
                thexs.ToThrow = GetExpression(tt)
            Next
            PopulateStatement(thexs, Xml)
            Return thexs
        End Function
        ''' <summary>Gets <see cref="CodeTryCatchFinallyStatement"/> from <c>&lt;TryCatchFinallyStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;TryCatchFinallyStatement></c> element</param>
        ''' <returns><see cref="CodeTryCatchFinallyStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetTryCatchFinallyStatement(ByVal Xml As XElement) As CodeTryCatchFinallyStatement
            Dim tcfs As New CodeTryCatchFinallyStatement
            For Each trys In Xml.<TryStatements>.Elements
                tcfs.TryStatements.Add(GetStatement(trys))
            Next
            For Each cc In Xml.<CatchClause>.Elements
                tcfs.CatchClauses.Add(GetCatchClause(cc))
            Next
            For Each fs In Xml.<FinallyStatements>.Elements
                tcfs.FinallyStatements.Add(GetStatement(fs))
            Next
            PopulateStatement(tcfs, Xml)
            Return tcfs
        End Function
        ''' <summary>Gets <see cref="CodeCatchClause"/> from <c>&lt;CatchClause></c> element</summary>
        ''' <param name="Xml">The <c>&lt;CatchClause></c> element</param>
        ''' <returns><see cref="CodeCatchClause"/> represented by <paramref name="Xml"/></returns>
        Private Function GetCatchClause(ByVal Xml As XElement) As CodeCatchClause
            Dim cc As New CodeCatchClause
            If Xml.@LocalName IsNot Nothing Then cc.LocalName = Xml.@LocalName
            For Each et In Xml.<CatchExceptionType> 'Maximally once
                cc.CatchExceptionType = GetTypeReference(et)
            Next
            For Each stm In Xml.<Statements>.Elements
                cc.Statements.Add(GetStatement(stm))
            Next
            Return cc
        End Function
        ''' <summary>Gets <see cref="CodeVariableDeclarationStatement"/> from <c>&lt;VariableDeclarationStatement></c> element</summary>
        ''' <param name="Xml">The <c>&lt;VariableDeclarationStatement></c> element</param>
        ''' <returns><see cref="CodeVariableDeclarationStatement"/> represented by <paramref name="Xml"/></returns>
        Private Function GetVariableDeclarationStatement(ByVal Xml As XElement) As CodeVariableDeclarationStatement
            Dim vds As New CodeVariableDeclarationStatement()
            vds.Name = Xml.@Name
            For Each t In Xml.<Type> 'Maximally once
                vds.Type = GetTypeReference(t)
            Next
            For Each ine In Xml.<InitExpression>
                vds.InitExpression = GetExpression(ine)
            Next
            PopulateStatement(vds, Xml)
            Return vds
        End Function
#End Region
#Region "CodeDom 2 Xml"
#Region "Helpers"
        ''' <summary>Serializes given collection to XML embddeding it in given envelope and serializing its member using given serializer</summary>
        ''' <typeparam name="T">Type of items in collection</typeparam>
        ''' <param name="Envelope"><see cref="XElement"/> to embded serialized items in</param>
        ''' <param name="Collection">Collection to serialize</param>
        ''' <param name="ItemSerializer">Delegate to serialize individual items in collection</param>
        ''' <returns><paramref name="Envelope"/> with serialized items from <paramref name="Collection"/> added as its children. Null when <paramref name="Collection"/> is null or is empty.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Envelope"/> or <paramref name="ItemSerializer"/> is null</exception>
        Private Function SerializeCollection(Of T)(ByVal Envelope As XElement, ByVal Collection As ICollection(Of T), ByVal ItemSerializer As Func(Of T, XElement)) As XElement
            If Envelope Is Nothing Then Throw New ArgumentNullException("Envelope")
            If ItemSerializer Is Nothing Then Throw New ArgumentNullException("ItemSerializer")
            If Collection IsNot Nothing AndAlso Collection.Count > 0 Then
                Envelope.Add(From item In Collection Select ItemSerializer.Invoke(item))
                Return Envelope
            End If
            Return Nothing
        End Function
        ''' <summary>Serializes user data <see cref="IDictionary"/> to <see cref="XElement"/></summary>
        ''' <param name="UserData">User data to serialize</param>
        ''' <param name="Envelope"><see cref="XElement"/> to serialize user data to. If ommited &lt;UserData> element is created.</param>
        ''' <remarks><paramref name="Envelope"/> or newly created &lt;UserData> element with <paramref name="UserData"/> serialized to. Null when <paramref name="UserData"/> is null or it is empy.</remarks>
        Private Function SerializeUserData(ByVal UserData As IDictionary, Optional ByVal Envelope As XElement = Nothing) As XElement
            If UserData Is Nothing OrElse UserData.Count = 0 Then Return Nothing
            If Envelope Is Nothing Then Envelope = <UserData/>
            For Each key In UserData.Keys
                Envelope.Add(<item key=<%= key.ToString %>><%= UserData(key).ToString %></item>)
            Next
            Return Envelope
        End Function
        ''' <summary>Serializes <see cref="CodeObject.UserData"/> from given <see cref="CodeObject"/> to <see cref="XElement"/></summary>
        ''' <param name="Object">Object containing user data to serialize</param>
        ''' <param name="Envelope"><see cref="XElement"/> to serialize user data to. If ommited &lt;UserData> element is created.</param>
        ''' <remarks><paramref name="Envelope"/> or newly created &lt;UserData> element with user data serialized to. Null when <paramref name="Object"/> is null or it is empy.</remarks>
        Private Function SerializeUserData(ByVal [Object] As CodeObject, Optional ByVal Envelope As XElement = Nothing) As XElement
            If [Object] Is Nothing Then Return Nothing
            Return SerializeUserData([Object].UserData, Envelope)
        End Function
        ''' <summary>Gets or-ed value of enumeration type as list of names of or-ed values</summary>
        ''' <param name="enm">Value to get list for</param>
        ''' <returns>Space-separated list of names of constants from type of <paramref name="enm"/> which when or-ed gives value of <paramref name="enm"/></returns>
        Private Function Enum2List(ByVal enm As [Enum]) As String
            Dim ret As New System.Text.StringBuilder
            For Each value As [Enum] In [Enum].GetValues(enm.GetType)
                If (enm.GetValue.ToInt64(CultureInfo.InvariantCulture) And value.GetValue.ToInt64(CultureInfo.InvariantCulture)) = value.GetValue.ToInt64(CultureInfo.InvariantCulture) Then
                    Dim name = value.GetName
                    If Not name.EndsWith("Mask") Then
                        If ret.Length <> 0 Then ret.Append(" ")
                        ret.Append(name)
                    End If
                End If
            Next
            Return ret.ToString
        End Function
#End Region
        ''' <summary>Serializes given <see cref="CodeCompileUnit"/> as </summary>
        ''' <param name="CompileUnit">A <see cref="CodeCompileUnit"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="CompileUnit"/>. Null when <paramref name="CompileUnit"/> is null.</returns>
        Private Function SerializeCompileUnit(ByVal [CompileUnit] As CodeCompileUnit, Optional ByVal ElementName As XName = Nothing) As XElement
            If CompileUnit Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.CompileUnit
            With TryCast(CompileUnit, CodeSnippetCompileUnit)
                Dim xsiType = If(.self Is Nothing, Names.CompileUnit, Names.SnippetCompileUnit)
                Dim Xml = _
                    <<%= ElementName %> xsi:type=<%= If(xsiType <> ElementName, xsiType, Nothing) %>>
                        <%= SerializeUserData(CompileUnit.UserData) %>
                        <%= SerializeCollection(<StartDirectives/>, CompileUnit.StartDirectives.AsTypeSafe, AddressOf SerializeDirective) %>
                        <%= SerializeCollection(<ReferencedAssemblies/>, CompileUnit.ReferencedAssemblies.AsTypeSafe, AddressOf SerializeAssemblyReference) %>
                        <%= SerializeCollection(<AssemblyCustomAttributes/>, CompileUnit.AssemblyCustomAttributes.AsTypeSafe, AddressOf SerializeAttributeDeclaration) %>
                        <%= SerializeCollection(<Namespaces/>, CompileUnit.Namespaces.AsTypeSafe, AddressOf SerializeNamespace) %>
                        <%= SerializeCollection(<EndDirectives/>, CompileUnit.EndDirectives.AsTypeSafe, AddressOf SerializeDirective) %>
                        <%= If(.self Is Nothing OrElse .LinePragma Is Nothing, Nothing, SerializeLinePragma(.LinePragma, Names.LinePragma)) %>
                        <%= If(.self Is Nothing, Nothing, <Value><%= New XCData(.Value) %></Value>) %>
                    </>
                Xml.Attribute(xsiType).CollapseExtendedName()
                Return Xml
            End With
        End Function

        ''' <summary>Serializes given <see cref="CodeNamespace"/> as </summary>
        ''' <param name="Namespace">A <see cref="CodeNamespace"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="Namespace"/>. Null when <paramref name="Namespace"/> is null.</returns>
        Private Function SerializeNamespace(ByVal [Namespace] As CodeNamespace, Optional ByVal ElementName As XName = Nothing) As XElement
            If [Namespace] Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.Namespace
            Dim Xml = _
                <<%= ElementName %>>
                    <%= SerializeUserData([Namespace].UserData) %>
                    <%= SerializeCollection(<Imports/>, [Namespace].Imports.AsTypeSafe, AddressOf SerializeNamespaceImport) %>
                    <%= SerializeCollection(<Comments/>, [Namespace].Comments.AsTypeSafe, AddressOf SerializeCommentStatement) %>
                    <%= SerializeCollection(<Types/>, [Namespace].Types.AsTypeSafe, AddressOf SerializeTypeDeclaration) %>
                </>
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeAttributeDeclaration"/> as </summary>
        ''' <param name="AttributeDeclaration">A <see cref="CodeAttributeDeclaration"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="AttributeDeclaration"/>. Null when <paramref name="AttributeDeclaration"/> is null.</returns>
        Private Function SerializeAttributeDeclaration(ByVal [AttributeDeclaration] As CodeAttributeDeclaration, Optional ByVal ElementName As XName = Nothing) As XElement
            If AttributeDeclaration Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.AttributeDeclaration
            Dim Xml = _
               <<%= ElementName %>>
                   <%= If(AttributeDeclaration.AttributeType Is Nothing, _
                       <Name><%= AttributeDeclaration.Name %></Name>, _
                       SerializeTypeReference(AttributeDeclaration.AttributeType, <AttributeType/>.Name) _
                       ) %>
                   <%= SerializeCollection(<Arguments/>, AttributeDeclaration.Arguments.AsTypeSafe, AddressOf SerializeAttributeArgument) %>
               </>
            Return Xml
        End Function


        ''' <summary>Serializes given string representing AssemblyReference as </summary>
        ''' <param name="AssemblyReference">String to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="AssemblyReference"/>. Null when <paramref name="AssemblyReference"/> is null.</returns>
        Private Function SerializeAssemblyReference(ByVal [AssemblyReference] As String, Optional ByVal ElementName As XName = Nothing) As XElement
            If AssemblyReference Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.AssemblyReference
            Return <<%= ElementName %>><%= AssemblyReference %></>
        End Function


        ''' <summary>Serializes given <see cref="CodeDirective"/> as </summary>
        ''' <param name="Directive">A <see cref="CodeDirective"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="Directive"/>. Null when <paramref name="Directive"/> is null.</returns>
        ''' <exception cref="NotSupportedException"><paramref name="Directive"/> is neither <see cref="CodeChecksumPragma"/> nor <see cref="CodeRegionDirective"/></exception>
        Private Function SerializeDirective(ByVal [Directive] As CodeDirective, Optional ByVal ElementName As XName = Nothing) As XElement
            If Directive Is Nothing Then Return Nothing
            If TypeOf Directive Is CodeChecksumPragma Then
                Return SerializeChecksumPragma(Directive, ElementName)
            ElseIf TypeOf Directive Is CodeRegionDirective Then
                Return SerializeRegionDirective(Directive, ElementName)
            Else
                Throw New NotSupportedException(ResourcesT.Exceptions.UnsupportedCodeDomObject0.f(Directive.GetType.Name))
            End If
        End Function
        ''' <summary>Serializes common properties of given <see cref="CodeDirective"/></summary>
        ''' <param name="Directive"><see cref="CodeDirective"/> to serialize</param>
        ''' <returns><see cref="XElement"/> which contains serialized <see cref="CodeObject.UserData"/> of <paramref name="Directive"/></returns>
        Private Function SerializeDirectiveCommon(ByVal Directive As CodeDirective) As XElement
            Return SerializeUserData(Directive.UserData)
        End Function

        ''' <summary>Serializes given <see cref="CodeChecksumPragma"/> as </summary>
        ''' <param name="ChecksumPragma">A <see cref="CodeChecksumPragma"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="ChecksumPragma"/>. Null when <paramref name="ChecksumPragma"/> is null.</returns>
        Private Function SerializeChecksumPragma(ByVal [ChecksumPragma] As CodeChecksumPragma, Optional ByVal ElementName As XName = Nothing) As XElement
            If ChecksumPragma Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.ChecksumPragma
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.ChecksumPragma, Names.ChecksumPragma, Nothing) %>
                    FileName=<%= ChecksumPragma.FileName %> ChecksumAlgorithmId=<%= ChecksumPragma.ChecksumAlgorithmId.ToString %>
                    >
                    <%= SerializeDirectiveCommon(ChecksumPragma) %>
                    <ChecksumData><%= Convert.ToBase64String(ChecksumPragma.ChecksumData) %></ChecksumData>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeRegionDirective"/> as </summary>
        ''' <param name="RegionDirective">A <see cref="CodeRegionDirective"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="RegionDirective"/>. Null when <paramref name="RegionDirective"/> is null.</returns>
        Private Function SerializeRegionDirective(ByVal [RegionDirective] As CodeRegionDirective, Optional ByVal ElementName As XName = Nothing) As XElement
            If RegionDirective Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.RegionDirective
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.RegionDirective, Names.RegionDirective, Nothing) %>
                    RegionText=<%= RegionDirective.RegionText %> RegionMode=<%= RegionDirective.RegionMode.GetName %>
                    >
                    <%= SerializeDirectiveCommon(RegionDirective) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeNamespaceImport"/> as </summary>
        ''' <param name="NamespaceImport">A <see cref="CodeNamespaceImport"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="NamespaceImport"/>. Null when <paramref name="NamespaceImport"/> is null.</returns>
        Private Function SerializeNamespaceImport(ByVal [NamespaceImport] As CodeNamespaceImport, Optional ByVal ElementName As XName = Nothing) As XElement
            If NamespaceImport Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.NamespaceImport
            Dim Xml = _
                <<%= ElementName %> Namespace=<%= NamespaceImport.Namespace %>>
                    <%= SerializeUserData(NamespaceImport) %>
                    <%= SerializeLinePragma(NamespaceImport.LinePragma, <LinePragma/>.Name) %>
                </>
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeCommentStatement"/> as </summary>
        ''' <param name="CommentStatement">A <see cref="CodeCommentStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="CommentStatement"/>. Null when <paramref name="CommentStatement"/> is null.</returns>
        Private Function SerializeCommentStatement(ByVal [CommentStatement] As CodeCommentStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If CommentStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.CommentStatement
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(xsiType <> Names.CommentStatement, Names.CommentStatement, Nothing) %>>
                    <%= SerializeStatementCommon(CommentStatement) %>
                    <%= SerializeComment(CommentStatement.Comment, <Comment/>.Name) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function
        ''' <summary>Serializes common properties of <see cref="CodeStatement"/></summary>
        ''' <param name="Statement">Statement to serialize properties of</param>
        ''' <returns>Properties of <paramref name="Statement"/> seriaized as array of <see cref="XElement">XElements</see>. Some items in array may be null.</returns>
        Private Function SerializeStatementCommon(ByVal Statement As CodeStatement) As XElement()
            Return New XElement() { _
                SerializeUserData(Statement.UserData), _
                SerializeLinePragma(Statement.LinePragma, <LinePragma/>.Name), _
                SerializeCollection(<StartDirectives/>, Statement.StartDirectives.AsTypeSafe, AddressOf SerializeDirective), _
                SerializeCollection(<EndDirectives/>, Statement.EndDirectives.AsTypeSafe, AddressOf SerializeDirective) _
            }
        End Function


        ''' <summary>Serializes given <see cref="CodeTypeDeclaration"/> as </summary>
        ''' <param name="TypeDeclaration">A <see cref="CodeTypeDeclaration"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="TypeDeclaration"/>. Null when <paramref name="TypeDeclaration"/> is null.</returns>
        Private Function SerializeTypeDeclaration(ByVal [TypeDeclaration] As CodeTypeDeclaration, Optional ByVal ElementName As XName = Nothing) As XElement
            If TypeDeclaration Is Nothing Then Return Nothing
            With TryCast(TypeDeclaration, CodeTypeDelegate)
                If ElementName Is Nothing Then ElementName = If(.self Is Nothing, Names.TypeDeclaration, Names.TypeDelegate)
                Dim xsiType = If(.self Is Nothing, Names.TypeDeclaration, Names.TypeDelegate)
                Dim Xml = _
                    <<%= ElementName %>
                        xsi:type=<%= If(xsiType <> ElementName, xsiType, Nothing) %>
                        <%= SerializeTypeMemberCommonAttributes(TypeDeclaration) %>
                        TypeAttributes=<%= Enum2List(TypeDeclaration.TypeAttributes) %>
                        IsClass=<%= TypeDeclaration.IsClass %>
                        IsEnum=<%= If(TypeDeclaration.IsEnum, "true", Nothing) %>
                        IsInterface=<%= If(TypeDeclaration.IsInterface, "true", Nothing) %>
                        IsPartial=<%= If(TypeDeclaration.IsPartial, "true", Nothing) %>
                        IsStruct=<%= If(TypeDeclaration.IsStruct, "true", Nothing) %>
                        >
                        <%= SerializeTypeMemberCommon(TypeDeclaration) %>
                        <%= SerializeCollection(<TypeParameters/>, TypeDeclaration.TypeParameters.AsTypeSafe, AddressOf SerializeTypeParameter) %>
                        <%= SerializeCollection(<BaseTypes/>, TypeDeclaration.BaseTypes.AsTypeSafe, AddressOf SerializeTypeReference) %>
                        <%= SerializeCollection(<Members/>, TypeDeclaration.Members.AsTypeSafe, AddressOf SerializeTypeMember) %>
                        <%= If(.self IsNot Nothing, SerializeCollection(<Parameters/>, .Parameters.AsTypeSafe, AddressOf SerializeParameterDeclarationExpression), Nothing) %>
                        <%= If(.self IsNot Nothing, SerializeTypeReference(.ReturnType, <ReturnType/>.Name), Nothing) %>
                    </>
                Xml.Attribute(xsiType).CollapseExtendedName()
                Return Xml
            End With
        End Function
        ''' <summary>Serializes common properties of <see cref="CodeTypeMember"/> to array of <see cref="XAttribute">XAttributes</see></summary>
        ''' <param name="TypeMember"><see cref="CodeTypeMember"/> to serialize properties of</param>
        ''' <returns>Array of serialized properties</returns>
        ''' <remarks>Always use in conjunction with <see cref="SerializeTypeMemberCommon"/></remarks>
        Private Function SerializeTypeMemberCommonAttributes(ByVal TypeMember As CodeTypeMember) As XAttribute()
            Return New XAttribute() { _
                New XAttribute("Attributes", Enum2List(TypeMember.Attributes)), _
                New XAttribute("Name", TypeMember.Name)}
        End Function
        ''' <summary>Serializes common properties of <see cref="CodeTypeMember"/> to array of <see cref="XElement">XElements</see></summary>
        ''' <param name="TypeMember"><see cref="CodeTypeMember"/> to serialize properties of</param>
        ''' <returns>Array of serialized properties</returns>
        ''' <remarks>Always use in conjunction with <see cref="SerializeTypeMemberCommonAttributes"/></remarks>
        Private Function SerializeTypeMemberCommon(ByVal TypeMember As CodeTypeMember) As XElement()
            Return New XElement() { _
                SerializeUserData(TypeMember.UserData), _
                SerializeCollection(<CustomAttributes/>, TypeMember.CustomAttributes.AsTypeSafe, AddressOf SerializeAttributeDeclaration), _
                SerializeCollection(<Comments/>, TypeMember.Comments.AsTypeSafe, AddressOf SerializeCommentStatement), _
                SerializeCollection(<StartDirectives/>, TypeMember.StartDirectives.AsTypeSafe, AddressOf SerializeDirective), _
                SerializeLinePragma(TypeMember.LinePragma, <LinePragma/>.Name), _
                SerializeCollection(<EndDirectives/>, TypeMember.EndDirectives.AsTypeSafe, AddressOf SerializeDirective)}
        End Function


        ''' <summary>Serializes given <see cref="CodeAttributeArgument"/> as </summary>
        ''' <param name="AttributeArgument">A <see cref="CodeAttributeArgument"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="AttributeArgument"/>. Null when <paramref name="AttributeArgument"/> is null.</returns>
        Private Function SerializeAttributeArgument(ByVal [AttributeArgument] As CodeAttributeArgument, Optional ByVal ElementName As XName = Nothing) As XElement
            If AttributeArgument Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.AttributeArgument
            Dim Xml = _
                <<%= ElementName %> Name=<%= AttributeArgument.Name %>>
                    <%= SerializeExpression(AttributeArgument.Value, <Value/>.Name) %>
                </>
            Return Xml
        End Function

        ''' <summary>Serializes given <see cref="CodeTypeReference"/> as </summary>
        ''' <param name="TypeReference">A <see cref="CodeTypeReference"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="TypeReference"/>. Null when <paramref name="TypeReference"/> is null.</returns>
        Private Function SerializeTypeReference(ByVal [TypeReference] As CodeTypeReference, Optional ByVal ElementName As XName = Nothing) As XElement
            If TypeReference Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.TypeReference
            Dim Xml = _
                <<%= ElementName %>
                    BaseType=<%= TypeReference.BaseType %>
                    ArrayRank=<%= If(TypeReference.ArrayRank <> 0, TypeReference.ArrayRank, Nothing) %>
                    Options=<%= If(TypeReference.Options = CodeTypeReferenceOptions.GenericTypeParameter, "GenericTypeParameter", If(TypeReference.Options = CodeTypeReferenceOptions.GlobalReference, "GlobalReference", Nothing)) %>
                    >
                    <%= SerializeUserData(TypeReference.UserData) %>
                    <%= SerializeTypeReference(TypeReference.ArrayElementType, <ArrayElementType/>.Name) %>
                    <%= SerializeCollection(<TypeArguments/>, TypeReference.TypeArguments.AsTypeSafe, AddressOf SerializeTypeReference) %>
                </>
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeTypeMember"/> as </summary>
        ''' <param name="TypeMember">A <see cref="CodeTypeMember"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="TypeMember"/>. Null when <paramref name="TypeMember"/> is null.</returns>
        Private Function SerializeTypeMember(ByVal [TypeMember] As CodeTypeMember, Optional ByVal ElementName As XName = Nothing) As XElement
            If TypeMember Is Nothing Then Return Nothing
            If TypeOf TypeMember Is CodeTypeDeclaration Then
                Return SerializeTypeDeclaration(TypeMember, ElementName)
            ElseIf TypeOf TypeMember Is CodeMemberEvent Then
                Return SerializeMemberEvent(TypeMember, ElementName)
            ElseIf TypeOf TypeMember Is CodeMemberField Then
                Return SerializeMemberField(TypeMember, ElementName)
            ElseIf TypeOf TypeMember Is CodeMemberMethod Then
                Return SerializeMemberMethod(TypeMember, ElementName)
            ElseIf TypeOf TypeMember Is CodeMemberProperty Then
                Return SerializeMemberProperty(TypeMember, ElementName)
            ElseIf TypeOf TypeMember Is CodeSnippetTypeMember Then
                Return SerializeSnippetTypeMember(TypeMember, ElementName)
            Else
                Throw New NotSupportedException(ResourcesT.Exceptions.UnsupportedCodeDomObject0.f(TypeMember.GetType.Name))
            End If
        End Function


        ''' <summary>Serializes given <see cref="CodeTypeParameter"/> as </summary>
        ''' <param name="TypeParameter">A <see cref="CodeTypeParameter"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="TypeParameter"/>. Null when <paramref name="TypeParameter"/> is null.</returns>
        Private Function SerializeTypeParameter(ByVal [TypeParameter] As CodeTypeParameter, Optional ByVal ElementName As XName = Nothing) As XElement
            If TypeParameter Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.TypeParameter
            Dim Xml = _
                <<%= ElementName %> HasConstructorConstraint=<%= If(TypeParameter.HasConstructorConstraint, "true", Nothing) %> Name=<%= TypeParameter.Name %>>
                    <%= SerializeUserData(TypeParameter) %>
                    <%= SerializeCollection(<Constraints/>, TypeParameter.Constraints.AsTypeSafe, AddressOf SerializeTypeReference) %>
                    <%= SerializeCollection(<CustomAttributes/>, TypeParameter.CustomAttributes.AsTypeSafe, AddressOf SerializeAttributeDeclaration) %>
                </>
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeMemberEvent"/> as </summary>
        ''' <param name="MemberEvent">A <see cref="CodeMemberEvent"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="MemberEvent"/>. Null when <paramref name="MemberEvent"/> is null.</returns>
        Private Function SerializeMemberEvent(ByVal [MemberEvent] As CodeMemberEvent, Optional ByVal ElementName As XName = Nothing) As XElement
            If MemberEvent Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.MemberEvent
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.MemberEvent, Names.MemberEvent, Nothing) %> <%= SerializeTypeMemberCommonAttributes(MemberEvent) %>>
                    <%= SerializeTypeMemberCommon(MemberEvent) %>
                    <%= SerializeTypeReference(MemberEvent.Type, <Type/>.Name) %>
                    <%= SerializeCollection(<ImplementationTypes/>, MemberEvent.ImplementationTypes.AsTypeSafe, AddressOf SerializeTypeReference) %>
                    <%= SerializeTypeReference(MemberEvent.PrivateImplementationType, <PrivateImplementationType/>.Name) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeMemberField"/> as </summary>
        ''' <param name="MemberField">A <see cref="CodeMemberField"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="MemberField"/>. Null when <paramref name="MemberField"/> is null.</returns>
        Private Function SerializeMemberField(ByVal [MemberField] As CodeMemberField, Optional ByVal ElementName As XName = Nothing) As XElement
            If MemberField Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.MemberField
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.MemberField, Names.MemberField, Nothing) %> <%= SerializeTypeMemberCommonAttributes(MemberField) %>>
                    <%= SerializeTypeMemberCommon(MemberField) %>
                    <%= SerializeTypeReference(MemberField.Type, <Type/>.Name) %>
                    <%= SerializeExpression(MemberField.InitExpression, <InitExpression/>.Name) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeMemberMethod"/> as </summary>
        ''' <param name="MemberMethod">A <see cref="CodeMemberMethod"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="MemberMethod"/>. Null when <paramref name="MemberMethod"/> is null.</returns>
        Private Function SerializeMemberMethod(ByVal [MemberMethod] As CodeMemberMethod, Optional ByVal ElementName As XName = Nothing) As XElement
            If MemberMethod Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = If(TypeOf MemberMethod Is CodeConstructor, Names.Constructor, If(TypeOf MemberMethod Is CodeEntryPointMethod, Names.EntryPointMethod, If(TypeOf MemberMethod Is CodeTypeConstructor, Names.TypeConstructor, Names.MemberMethod)))
            Dim xsiType = If(TypeOf MemberMethod Is CodeEntryPointMethod, Names.EntryPointMethod, If(TypeOf MemberMethod Is CodeConstructor, Names.Constructor, If(TypeOf MemberMethod Is CodeTypeConstructor, Names.TypeConstructor, Names.MemberMethod)))
            Dim Xml = _
                <<%= ElementName %>
                    xsi:type=<%= If(xsiType <> ElementName, xsiType, Nothing) %>
                    <%= SerializeTypeMemberCommonAttributes([MemberMethod]) %>
                    >
                    <%= SerializeTypeMemberCommon([MemberMethod]) %>
                    <%= SerializeCollection(<Parameters/>, MemberMethod.Parameters.AsTypeSafe, AddressOf SerializeParameterDeclarationExpression) %>
                    <%= SerializeTypeReference(MemberMethod.ReturnType, <ReturnType/>.Name) %>
                    <%= SerializeCollection(<TypeParameters/>, MemberMethod.TypeParameters.AsTypeSafe, AddressOf SerializeTypeParameter) %>
                    <%= SerializeCollection(<ImplementationTypes/>, MemberMethod.ImplementationTypes.AsTypeSafe, AddressOf SerializeTypeReference) %>
                    <%= SerializeTypeReference(MemberMethod.PrivateImplementationType, <PrivateImplementationType/>.Name) %>
                    <%= SerializeCollection(<ReturnTypeCustomAttributes/>, MemberMethod.ReturnTypeCustomAttributes.AsTypeSafe, AddressOf SerializeAttributeDeclaration) %>
                    <%= SerializeCollection(<Statements/>, MemberMethod.Statements.AsTypeSafe, AddressOf SerializeStatement) %>
                    <%= If(TypeOf MemberMethod Is CodeConstructor, SerializeCollection(<BaseConstructorArgs/>, DirectCast(MemberMethod, CodeConstructor).BaseConstructorArgs.AsTypeSafe, AddressOf SerializeExpression), Nothing) %>
                    <%= If(TypeOf MemberMethod Is CodeConstructor, SerializeCollection(<ChainedConstructorArgs/>, DirectCast(MemberMethod, CodeConstructor).ChainedConstructorArgs.AsTypeSafe, AddressOf SerializeExpression), Nothing) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeParameterDeclarationExpression"/> as </summary>
        ''' <param name="ParameterDeclarationExpression">A <see cref="CodeParameterDeclarationExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="ParameterDeclarationExpression"/>. Null when <paramref name="ParameterDeclarationExpression"/> is null.</returns>
        Private Function SerializeParameterDeclarationExpression(ByVal [ParameterDeclarationExpression] As CodeParameterDeclarationExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If ParameterDeclarationExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.ParameterDeclarationExpression
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.ParameterDeclarationExpression, Names.ParameterDeclarationExpression, Nothing) %> Name=<%= ParameterDeclarationExpression.Name %> Direction=<%= Enum2List(ParameterDeclarationExpression.Direction) %>>
                    <%= SerializeExpressionCommon(ParameterDeclarationExpression) %>
                    <%= SerializeCollection(<CustomAttributes/>, ParameterDeclarationExpression.CustomAttributes.AsTypeSafe, AddressOf SerializeAttributeDeclaration) %>
                    <%= SerializeTypeReference(ParameterDeclarationExpression.Type, <Type/>.Name) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function
        ''' <summary>Serializes comon properties fo <see cref="CodeExpression"/> to <see cref="XElement"/></summary>
        ''' <param name="Expression"><see cref="CodeExpression"/> to serialize properties of</param>
        ''' <returns>Serialized <paramref name="Expression"/>.<see cref="CodeExpression.UserData">UserData</see></returns>
        Private Function SerializeExpressionCommon(ByVal Expression As CodeExpression) As XElement
            Return SerializeUserData(Expression.UserData)
        End Function

        ''' <summary>Serializes given <see cref="CodeStatement"/> as </summary>
        ''' <param name="Statement">A <see cref="CodeStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="Statement"/>. Null when <paramref name="Statement"/> is null.</returns>
        ''' <exception cref="NotSupportedException"><paramref name="Statement"/> is none of supported statement types.</exception>
        Private Function SerializeStatement(ByVal [Statement] As CodeStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If Statement Is Nothing Then Return Nothing
            If TypeOf Statement Is CodeCommentStatement Then : Return SerializeCommentStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeAssignStatement Then : Return SerializeAssignStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeAttachEventStatement Then : Return SerializeAttachEventStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeConditionStatement Then : Return SerializeConditionStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeExpressionStatement Then : Return SerializeExpressionStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeGotoStatement Then : Return SerializeGotoStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeIterationStatement Then : Return SerializeIterationStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeLabeledStatement Then : Return SerializeLabeledStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeMethodReturnStatement Then : Return SerializeMethodReturnStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeRemoveEventStatement Then : Return SerializeRemoveEventStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeSnippetStatement Then : Return SerializeSnippetStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeThrowExceptionStatement Then : Return SerializeThrowExceptionStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeTryCatchFinallyStatement Then : Return SerializeTryCatchFinallyStatement(Statement, ElementName)
            ElseIf TypeOf Statement Is CodeVariableDeclarationStatement Then : Return SerializeVariableDeclarationStatement(Statement, ElementName)
            Else : Throw New NotSupportedException(ResourcesT.Exceptions.UnsupportedCodeDomObject0.f(Statement.GetType.Name))
            End If
        End Function


        ''' <summary>Serializes given <see cref="CodeMemberProperty"/> as </summary>
        ''' <param name="MemberProperty">A <see cref="CodeMemberProperty"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="MemberProperty"/>. Null when <paramref name="MemberProperty"/> is null.</returns>
        Private Function SerializeMemberProperty(ByVal [MemberProperty] As CodeMemberProperty, Optional ByVal ElementName As XName = Nothing) As XElement
            If MemberProperty Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.MemberProperty
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.MemberProperty, Names.MemberProperty, Nothing) %>
                    <%= SerializeTypeMemberCommonAttributes(MemberProperty) %>
                    HasSet=<%= If(MemberProperty.HasSet, "true", "false") %> HasGet=<%= If(MemberProperty.HasGet, "true", "false") %>
                    >
                    <%= SerializeTypeMemberCommon(MemberProperty) %>
                    <%= SerializeCollection(<Parameters/>, MemberProperty.Parameters.AsTypeSafe, AddressOf SerializeParameterDeclarationExpression) %>
                    <%= SerializeTypeReference(MemberProperty.Type, <Type/>.Name) %>
                    <%= SerializeCollection(<ImplementationTypes/>, MemberProperty.ImplementationTypes.AsTypeSafe, AddressOf SerializeTypeReference) %>
                    <%= SerializeTypeReference(MemberProperty.PrivateImplementationType, <PrivateImplementationType/>.Name) %>
                    <%= SerializeCollection(<GetStatements/>, MemberProperty.GetStatements.AsTypeSafe, AddressOf SerializeStatement) %>
                    <%= SerializeCollection(<SetStatements/>, MemberProperty.SetStatements.AsTypeSafe, AddressOf SerializeStatement) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeExpression"/> as </summary>
        ''' <param name="Expression">A <see cref="CodeExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="Expression"/>. Null when <paramref name="Expression"/> is null.</returns>
        ''' <exception cref="NotSupportedException"><paramref name="Expression"/> is none of supported expression types</exception>
        Private Function SerializeExpression(ByVal [Expression] As CodeExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If Expression Is Nothing Then Return Nothing
            If TypeOf Expression Is CodeParameterDeclarationExpression Then : Return SerializeParameterDeclarationExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeArgumentReferenceExpression Then : Return SerializeArgumentReferenceExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeArrayCreateExpression Then : Return SerializeArrayCreateExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeArrayIndexerExpression Then : Return SerializeArrayIndexerExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeBaseReferenceExpression Then : Return SerializeBaseReferenceExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeBinaryOperatorExpression Then : Return SerializeBinaryOperatorExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeCastExpression Then : Return SerializeCastExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeDefaultValueExpression Then : Return SerializeDefaultValueExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeDelegateCreateExpression Then : Return SerializeDelegateCreateExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeDelegateInvokeExpression Then : Return SerializeDelegateInvokeExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeDirectionExpression Then : Return SerializeDirectionExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeEventReferenceExpression Then : Return SerializeEventReferenceExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeFieldReferenceExpression Then : Return SerializeFieldReferenceExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeIndexerExpression Then : Return SerializeIndexerExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeMethodInvokeExpression Then : Return SerializeMethodInvokeExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeMethodReferenceExpression Then : Return SerializeMethodReferenceExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeObjectCreateExpression Then : Return SerializeObjectCreateExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodePrimitiveExpression Then : Return SerializePrimitiveExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodePropertyReferenceExpression Then : Return SerializePropertyReferenceExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodePropertySetValueReferenceExpression Then : Return SerializePropertySetValueReferenceExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeSnippetExpression Then : Return SerializeSnippetExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeThisReferenceExpression Then : Return SerializeThisReferenceExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeTypeOfExpression Then : Return SerializeTypeOfExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeTypeReferenceExpression Then : Return SerializeTypeReferenceExpression(Expression, ElementName)
            ElseIf TypeOf Expression Is CodeVariableReferenceExpression Then : Return SerializeVariableReferenceExpression(Expression, ElementName)
            Else : Throw New NotSupportedException(ResourcesT.Exceptions.UnsupportedCodeDomObject0.f(Expression.GetType.Name))
            End If
        End Function


        ''' <summary>Serializes given <see cref="CodeSnippetTypeMember"/> as </summary>
        ''' <param name="SnippetTypeMember">A <see cref="CodeSnippetTypeMember"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="SnippetTypeMember"/>. Null when <paramref name="SnippetTypeMember"/> is null.</returns>
        Private Function SerializeSnippetTypeMember(ByVal [SnippetTypeMember] As CodeSnippetTypeMember, Optional ByVal ElementName As XName = Nothing) As XElement
            If SnippetTypeMember Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.SnippetTypeMember
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.SnippetTypeMember, Names.SnippetTypeMember, Nothing) %> <%= SerializeTypeMemberCommonAttributes(SnippetTypeMember) %>>
                    <%= SerializeTypeMemberCommon(SnippetTypeMember) %>
                    <%= New XCData(SnippetTypeMember.Text) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeConstructor"/> as </summary>
        ''' <param name="Constructor">A <see cref="CodeConstructor"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="Constructor"/>. Null when <paramref name="Constructor"/> is null.</returns>
        Private Function SerializeConstructor(ByVal [Constructor] As CodeConstructor, Optional ByVal ElementName As XName = Nothing) As XElement
            If Constructor Is Nothing Then Return Nothing
            Return SerializeMemberMethod(Constructor, ElementName)
        End Function


        ''' <summary>Serializes given <see cref="CodeEntryPointMethod"/> as </summary>
        ''' <param name="EntryPointMethod">A <see cref="CodeEntryPointMethod"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="EntryPointMethod"/>. Null when <paramref name="EntryPointMethod"/> is null.</returns>
        Private Function SerializeEntryPointMethod(ByVal [EntryPointMethod] As CodeEntryPointMethod, Optional ByVal ElementName As XName = Nothing) As XElement
            If EntryPointMethod Is Nothing Then Return Nothing
            Return SerializeMemberMethod(EntryPointMethod, ElementName)
        End Function


        ''' <summary>Serializes given <see cref="CodeTypeConstructor"/> as </summary>
        ''' <param name="TypeConstructor">A <see cref="CodeTypeConstructor"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="TypeConstructor"/>. Null when <paramref name="TypeConstructor"/> is null.</returns>
        Private Function SerializeTypeConstructor(ByVal [TypeConstructor] As CodeTypeConstructor, Optional ByVal ElementName As XName = Nothing) As XElement
            If TypeConstructor Is Nothing Then Return Nothing
            Return SerializeMemberMethod(TypeConstructor, ElementName)
        End Function


        ''' <summary>Serializes given <see cref="CodeLinePragma"/> as </summary>
        ''' <param name="LinePragma">A <see cref="CodeLinePragma"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="LinePragma"/>. Null when <paramref name="LinePragma"/> is null.</returns>
        Private Function SerializeLinePragma(ByVal [LinePragma] As CodeLinePragma, Optional ByVal ElementName As XName = Nothing) As XElement
            If LinePragma Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.LinePragma
            Dim Xml = _
                <<%= ElementName %> FileName=<%= LinePragma.FileName %> LineNumber=<%= LinePragma.LineNumber %>/>
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeComment"/> as </summary>
        ''' <param name="Comment">A <see cref="CodeComment"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="Comment"/>. Null when <paramref name="Comment"/> is null.</returns>
        Private Function SerializeComment(ByVal [Comment] As CodeComment, Optional ByVal ElementName As XName = Nothing) As XElement
            If Comment Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.Comment
            Dim Xml As XElement = Nothing
            If Comment.DocComment Then
                Dim XText = <TextXml/>
                Try
                    XText.SetInnerXml(Comment.Text)
                Catch ex As Xml.XmlException
                    GoTo NonXml
                End Try
                Xml = _
                    <<%= ElementName %> DocComment="true">
                        <%= SerializeUserData(Comment) %>
                        <%= XText %>
                    </>
            End If
            If Xml Is Nothing Then
NonXml:         Xml = _
                    <<%= ElementName %> DocComment=<%= If(Comment.DocComment, "true", "false") %>>
                        <%= SerializeUserData(Comment) %>
                        <Text><%= New XCData(Comment.Text) %></Text>
                    </>
            End If
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeSnippetCompileUnit"/> as </summary>
        ''' <param name="SnippetCompileUnit">A <see cref="CodeSnippetCompileUnit"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="SnippetCompileUnit"/></returns>
        Private Function SerializeSnippetCompileUnit(ByVal [SnippetCompileUnit] As CodeSnippetCompileUnit, Optional ByVal ElementName As XName = Nothing) As XElement
            If ElementName Is Nothing Then ElementName = Names.SnippetCompileUnit
            Return SerializeCompileUnit(SnippetCompileUnit, ElementName)
        End Function


        ''' <summary>Serializes given <see cref="CodeAssignStatement"/> as </summary>
        ''' <param name="AssignStatement">A <see cref="CodeAssignStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="AssignStatement"/>. Null when <paramref name="AssignStatement"/> is null.</returns>
        Private Function SerializeAssignStatement(ByVal [AssignStatement] As CodeAssignStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If AssignStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.AssignStatement
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.AssignStatement, Names.AssignStatement, Nothing) %>>
                    <%= SerializeStatementCommon(AssignStatement) %>
                    <%= SerializeExpression(AssignStatement.Left, <Left/>.Name) %>
                    <%= SerializeExpression(AssignStatement.Right, <Right/>.Name) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeAttachEventStatement"/> as </summary>
        ''' <param name="AttachEventStatement">A <see cref="CodeAttachEventStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="AttachEventStatement"/>. Null when <paramref name="AttachEventStatement"/> is null.</returns>
        Private Function SerializeAttachEventStatement(ByVal [AttachEventStatement] As CodeAttachEventStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If AttachEventStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.AttachEventStatement
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.AttachEventStatement, Names.AttachEventStatement, Nothing) %>>
                    <%= SerializeStatementCommon(AttachEventStatement) %>
                    <%= SerializeEventReferenceExpression(AttachEventStatement.Event, <Event/>.Name) %>
                    <%= SerializeExpression(AttachEventStatement.Listener, <Listener/>.Name) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeConditionStatement"/> as </summary>
        ''' <param name="ConditionStatement">A <see cref="CodeConditionStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="ConditionStatement"/>. Null when <paramref name="ConditionStatement"/> is null.</returns>
        Private Function SerializeConditionStatement(ByVal [ConditionStatement] As CodeConditionStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If ConditionStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.ConditionStatement
            Dim Xml = _
             <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.ConditionStatement, Names.ConditionStatement, Nothing) %>>
                 <%= SerializeStatementCommon(ConditionStatement) %>
                 <%= SerializeExpression(ConditionStatement.Condition, <Condition/>.Name) %>
                 <%= SerializeCollection(<TrueStatements/>, ConditionStatement.TrueStatements.AsTypeSafe, AddressOf SerializeStatement) %>
                 <%= SerializeCollection(<FalseStatements/>, ConditionStatement.FalseStatements.AsTypeSafe, AddressOf SerializeStatement) %>
             </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeExpressionStatement"/> as </summary>
        ''' <param name="ExpressionStatement">A <see cref="CodeExpressionStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="ExpressionStatement"/>. Null when <paramref name="ExpressionStatement"/> is null.</returns>
        Private Function SerializeExpressionStatement(ByVal [ExpressionStatement] As CodeExpressionStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If ExpressionStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.ExpressionStatement
            Dim Xml = _
             <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.ExpressionStatement, Names.ExpressionStatement, Nothing) %>>
                 <%= SerializeStatementCommon(ExpressionStatement) %>
                 <%= SerializeExpression(ExpressionStatement.Expression, <Expression/>.Name) %>
             </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeGotoStatement"/> as </summary>
        ''' <param name="GotoStatement">A <see cref="CodeGotoStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="GotoStatement"/>. Null when <paramref name="GotoStatement"/> is null.</returns>
        Private Function SerializeGotoStatement(ByVal [GotoStatement] As CodeGotoStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If GotoStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.GotoStatement
            Dim Xml = _
    <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.GotoStatement, Names.GotoStatement, Nothing) %> Label=<%= GotoStatement.Label %>>
        <%= SerializeStatementCommon(GotoStatement) %>
    </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeIterationStatement"/> as </summary>
        ''' <param name="IterationStatement">A <see cref="CodeIterationStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="IterationStatement"/>. Null when <paramref name="IterationStatement"/> is null.</returns>
        Private Function SerializeIterationStatement(ByVal [IterationStatement] As CodeIterationStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If IterationStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.IterationStatement
            Dim Xml = _
             <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.IterationStatement, Names.IterationStatement, Nothing) %>>
                 <%= SerializeStatementCommon(IterationStatement) %>
                 <%= SerializeStatement(IterationStatement.InitStatement, <InitStatement/>.Name) %>
                 <%= SerializeStatement(IterationStatement.IncrementStatement, <IncrementStatement/>.Name) %>
                 <%= SerializeExpression(IterationStatement.TestExpression, <TestExpression/>.Name) %>
                 <%= SerializeCollection(<Statements/>, IterationStatement.Statements.AsTypeSafe, AddressOf SerializeStatement) %>
             </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeLabeledStatement"/> as </summary>
        ''' <param name="LabeledStatement">A <see cref="CodeLabeledStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="LabeledStatement"/>. Null when <paramref name="LabeledStatement"/> is null.</returns>
        Private Function SerializeLabeledStatement(ByVal [LabeledStatement] As CodeLabeledStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If LabeledStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.LabeledStatement
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.LabeledStatement, Names.LabeledStatement, Nothing) %> Label=<%= LabeledStatement.Label %>>
                    <%= SerializeStatementCommon(LabeledStatement) %>
                    <%= SerializeStatement(LabeledStatement.Statement, <Statement/>.Name) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeMethodReturnStatement"/> as </summary>
        ''' <param name="MethodReturnStatement">A <see cref="CodeMethodReturnStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="MethodReturnStatement"/>. Null when <paramref name="MethodReturnStatement"/> is null.</returns>
        Private Function SerializeMethodReturnStatement(ByVal [MethodReturnStatement] As CodeMethodReturnStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If MethodReturnStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.MethodReturnStatement
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.MethodReturnStatement, Names.MethodReturnStatement, Nothing) %>>
                    <%= SerializeStatementCommon(MethodReturnStatement) %>
                    <%= SerializeExpression(MethodReturnStatement.Expression, <Expression/>.Name) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeRemoveEventStatement"/> as </summary>
        ''' <param name="RemoveEventStatement">A <see cref="CodeRemoveEventStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="RemoveEventStatement"/>. Null when <paramref name="RemoveEventStatement"/> is null.</returns>
        Private Function SerializeRemoveEventStatement(ByVal [RemoveEventStatement] As CodeRemoveEventStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If RemoveEventStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.RemoveEventStatement
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.RemoveEventStatement, Names.RemoveEventStatement, Nothing) %>>
                    <%= SerializeStatementCommon(RemoveEventStatement) %>
                    <%= SerializeEventReferenceExpression(RemoveEventStatement.Event, <Event/>.Name) %>
                    <%= SerializeExpression(RemoveEventStatement.Listener, <Listener/>.Name) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeSnippetStatement"/> as </summary>
        ''' <param name="SnippetStatement">A <see cref="CodeSnippetStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="SnippetStatement"/>. Null when <paramref name="SnippetStatement"/> is null.</returns>
        Private Function SerializeSnippetStatement(ByVal [SnippetStatement] As CodeSnippetStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If SnippetStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.SnippetStatement
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.SnippetStatement, Names.SnippetStatement, Nothing) %>>
                    <%= SerializeStatementCommon(SnippetStatement) %>
                    <Value><%= New XCData(SnippetStatement.Value) %></Value>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeThrowExceptionStatement"/> as </summary>
        ''' <param name="ThrowExceptionStatement">A <see cref="CodeThrowExceptionStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="ThrowExceptionStatement"/>. Null when <paramref name="ThrowExceptionStatement"/> is null.</returns>
        Private Function SerializeThrowExceptionStatement(ByVal [ThrowExceptionStatement] As CodeThrowExceptionStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If ThrowExceptionStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.ThrowExceptionStatement
            Dim Xml = _
               <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.ThrowExceptionStatement, Names.ThrowExceptionStatement, Nothing) %>>
                   <%= SerializeStatementCommon(ThrowExceptionStatement) %>
                   <%= SerializeExpression(ThrowExceptionStatement.ToThrow, <ToThrow/>.Name) %>
               </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeTryCatchFinallyStatement"/> as </summary>
        ''' <param name="TryCatchFinallyStatement">A <see cref="CodeTryCatchFinallyStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="TryCatchFinallyStatement"/>. Null when <paramref name="TryCatchFinallyStatement"/> is null.</returns>
        Private Function SerializeTryCatchFinallyStatement(ByVal [TryCatchFinallyStatement] As CodeTryCatchFinallyStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If TryCatchFinallyStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.TryCatchFinallyStatement
            Dim Xml = _
              <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.TryCatchFinallyStatement, Names.TryCatchFinallyStatement, Nothing) %>>
                  <%= SerializeStatementCommon(TryCatchFinallyStatement) %>
                  <%= SerializeCollection(<TryStatements/>, TryCatchFinallyStatement.TryStatements.AsTypeSafe, AddressOf SerializeStatement) %>
                  <%= SerializeCollection(<CatchClauses/>, TryCatchFinallyStatement.CatchClauses.AsTypeSafe, AddressOf SerializeCatchClause) %>
                  <%= SerializeCollection(<FinallyStatements/>, TryCatchFinallyStatement.FinallyStatements.AsTypeSafe, AddressOf SerializeStatement) %>
              </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeVariableDeclarationStatement"/> as </summary>
        ''' <param name="VariableDeclarationStatement">A <see cref="CodeVariableDeclarationStatement"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="VariableDeclarationStatement"/>. Null when <paramref name="VariableDeclarationStatement"/> is null.</returns>
        Private Function SerializeVariableDeclarationStatement(ByVal [VariableDeclarationStatement] As CodeVariableDeclarationStatement, Optional ByVal ElementName As XName = Nothing) As XElement
            If VariableDeclarationStatement Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.VariableDeclarationStatement
            Dim Xml = _
              <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.VariableDeclarationStatement, Names.VariableDeclarationStatement, Nothing) %>
                  Name=<%= VariableDeclarationStatement.Name %>
                  >
                  <%= SerializeStatementCommon(VariableDeclarationStatement) %>
                  <%= SerializeTypeReference(VariableDeclarationStatement.Type, <Type/>.Name) %>
                  <%= SerializeExpression(VariableDeclarationStatement.InitExpression, <InitExpression/>.Name) %>
              </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeArgumentReferenceExpression"/> as </summary>
        ''' <param name="ArgumentReferenceExpression">A <see cref="CodeArgumentReferenceExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="ArgumentReferenceExpression"/>. Null when <paramref name="ArgumentReferenceExpression"/> is null.</returns>
        Private Function SerializeArgumentReferenceExpression(ByVal [ArgumentReferenceExpression] As CodeArgumentReferenceExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If ArgumentReferenceExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.ArgumentReferenceExpression
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.ArgumentReferenceExpression, Names.ArgumentReferenceExpression, Nothing) %>
                    ParameterName=<%= ArgumentReferenceExpression.ParameterName %>>
                    <%= SerializeExpressionCommon(ArgumentReferenceExpression) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeArrayCreateExpression"/> as </summary>
        ''' <param name="ArrayCreateExpression">A <see cref="CodeArrayCreateExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="ArrayCreateExpression"/>. Null when <paramref name="ArrayCreateExpression"/> is null.</returns>
        Private Function SerializeArrayCreateExpression(ByVal [ArrayCreateExpression] As CodeArrayCreateExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If ArrayCreateExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.ArrayCreateExpression
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.ArrayCreateExpression, Names.ArrayCreateExpression, Nothing) %>
                    Size=<%= If(ArrayCreateExpression.SizeExpression Is Nothing AndAlso ArrayCreateExpression.Initializers.Count = 0, ArrayCreateExpression.Size, Nothing) %>>
                    <%= SerializeExpressionCommon(ArrayCreateExpression) %>
                    <%= SerializeTypeReference(ArrayCreateExpression.CreateType, <CreateType/>.Name) %>
                    <%= SerializeExpression(ArrayCreateExpression.SizeExpression, <SizeExpression/>.Name) %>
                    <%= SerializeCollection(<Initializers/>, ArrayCreateExpression.Initializers.AsTypeSafe, AddressOf SerializeExpression) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeArrayIndexerExpression"/> as </summary>
        ''' <param name="ArrayIndexerExpression">A <see cref="CodeArrayIndexerExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="ArrayIndexerExpression"/>. Null when <paramref name="ArrayIndexerExpression"/> is null.</returns>
        Private Function SerializeArrayIndexerExpression(ByVal [ArrayIndexerExpression] As CodeArrayIndexerExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If ArrayIndexerExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.ArrayIndexerExpression
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.ArrayIndexerExpression, Names.ArrayIndexerExpression, Nothing) %>>
                    <%= SerializeExpressionCommon(ArrayIndexerExpression) %>
                    <%= SerializeExpression(ArrayIndexerExpression.TargetObject, <TargetObject/>.Name) %>
                    <%= SerializeCollection(<Indices/>, ArrayIndexerExpression.Indices.AsTypeSafe, AddressOf SerializeExpression) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeBaseReferenceExpression"/> as </summary>
        ''' <param name="BaseReferenceExpression">A <see cref="CodeBaseReferenceExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="BaseReferenceExpression"/>. Null when <paramref name="BaseReferenceExpression"/> is null.</returns>
        Private Function SerializeBaseReferenceExpression(ByVal [BaseReferenceExpression] As CodeBaseReferenceExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If BaseReferenceExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.BaseReferenceExpression
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.BaseReferenceExpression, Names.BaseReferenceExpression, Nothing) %>>
                    <%= SerializeExpressionCommon(BaseReferenceExpression) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeBinaryOperatorExpression"/> as </summary>
        ''' <param name="BinaryOperatorExpression">A <see cref="CodeBinaryOperatorExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="BinaryOperatorExpression"/>. Null when <paramref name="BinaryOperatorExpression"/> is null.</returns>
        Private Function SerializeBinaryOperatorExpression(ByVal [BinaryOperatorExpression] As CodeBinaryOperatorExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If BinaryOperatorExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.BinaryOperatorExpression
            Dim Xml = _
               <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.BinaryOperatorExpression, Names.BinaryOperatorExpression, Nothing) %>
                   Operator=<%= BinaryOperatorExpression.Operator.GetName %>
                   >
                   <%= SerializeExpressionCommon(BinaryOperatorExpression) %>
                   <%= SerializeExpression(BinaryOperatorExpression.Left, <Left/>.Name) %>
                   <%= SerializeExpression(BinaryOperatorExpression.Right, <Right/>.Name) %>
               </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeCastExpression"/> as </summary>
        ''' <param name="CastExpression">A <see cref="CodeCastExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="CastExpression"/>. Null when <paramref name="CastExpression"/> is null.</returns>
        Private Function SerializeCastExpression(ByVal [CastExpression] As CodeCastExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If CastExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.CastExpression
            Dim Xml = _
               <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.CastExpression, Names.CastExpression, Nothing) %>>
                   <%= SerializeExpressionCommon(CastExpression) %>
                   <%= SerializeExpression(CastExpression.Expression, <Expression/>.Name) %>
                   <%= SerializeTypeReference(CastExpression.TargetType, <TargetType/>.Name) %>
               </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeDefaultValueExpression"/> as </summary>
        ''' <param name="DefaultValueExpression">A <see cref="CodeDefaultValueExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="DefaultValueExpression"/>. Null when <paramref name="DefaultValueExpression"/> is null.</returns>
        Private Function SerializeDefaultValueExpression(ByVal [DefaultValueExpression] As CodeDefaultValueExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If DefaultValueExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.DefaultValueExpression
            Dim Xml = _
              <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.DefaultValueExpression, Names.DefaultValueExpression, Nothing) %>>
                  <%= SerializeExpressionCommon(DefaultValueExpression) %>
                  <%= SerializeTypeReference(DefaultValueExpression.Type, <Type/>.Name) %>
              </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeDelegateCreateExpression"/> as </summary>
        ''' <param name="DelegateCreateExpression">A <see cref="CodeDelegateCreateExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="DelegateCreateExpression"/>. Null when <paramref name="DelegateCreateExpression"/> is null.</returns>
        Private Function SerializeDelegateCreateExpression(ByVal [DelegateCreateExpression] As CodeDelegateCreateExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If DelegateCreateExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.DelegateCreateExpression
            Dim Xml = _
              <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.DelegateCreateExpression, Names.DelegateCreateExpression, Nothing) %>
                  MethodName=<%= DelegateCreateExpression.MethodName %>
                  >
                  <%= SerializeExpressionCommon(DelegateCreateExpression) %>
                  <%= SerializeTypeReference(DelegateCreateExpression.DelegateType, <DelegateType/>.Name) %>
                  <%= SerializeExpression(DelegateCreateExpression.TargetObject, <TargetObject/>.Name) %>
              </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeDelegateInvokeExpression"/> as </summary>
        ''' <param name="DelegateInvokeExpression">A <see cref="CodeDelegateInvokeExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="DelegateInvokeExpression"/>. Null when <paramref name="DelegateInvokeExpression"/> is null.</returns>
        Private Function SerializeDelegateInvokeExpression(ByVal [DelegateInvokeExpression] As CodeDelegateInvokeExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If DelegateInvokeExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.DelegateInvokeExpression
            Dim Xml = _
              <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.DelegateInvokeExpression, Names.DelegateInvokeExpression, Nothing) %>>
                  <%= SerializeExpressionCommon(DelegateInvokeExpression) %>
                  <%= SerializeExpression(DelegateInvokeExpression.TargetObject, <TargetObject/>.Name) %>
                  <%= SerializeCollection(<Parameters/>, DelegateInvokeExpression.Parameters.AsTypeSafe, AddressOf SerializeExpression) %>
              </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeDirectionExpression"/> as </summary>
        ''' <param name="DirectionExpression">A <see cref="CodeDirectionExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="DirectionExpression"/>. Null when <paramref name="DirectionExpression"/> is null.</returns>
        Private Function SerializeDirectionExpression(ByVal [DirectionExpression] As CodeDirectionExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If DirectionExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.DirectionExpression
            Dim Xml = _
              <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.DirectionExpression, Names.DirectionExpression, Nothing) %>
                  Direction=<%= DirectionExpression.Direction.GetName %>
                  >
                  <%= SerializeExpressionCommon(DirectionExpression) %>
                  <%= SerializeExpression(DirectionExpression.Expression, <Expression/>.Name) %>
              </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeEventReferenceExpression"/> as </summary>
        ''' <param name="EventReferenceExpression">A <see cref="CodeEventReferenceExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="EventReferenceExpression"/>. Null when <paramref name="EventReferenceExpression"/> is null.</returns>
        Private Function SerializeEventReferenceExpression(ByVal [EventReferenceExpression] As CodeEventReferenceExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If EventReferenceExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.EventReferenceExpression
            Dim Xml = _
              <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.EventReferenceExpression, Names.EventReferenceExpression, Nothing) %>
                  EventName=<%= EventReferenceExpression.EventName %>
                  >
                  <%= SerializeExpressionCommon(EventReferenceExpression) %>
                  <%= SerializeExpression(EventReferenceExpression.TargetObject, <TargetObject/>.Name) %>
              </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeFieldReferenceExpression"/> as </summary>
        ''' <param name="FieldReferenceExpression">A <see cref="CodeFieldReferenceExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="FieldReferenceExpression"/>. Null when <paramref name="FieldReferenceExpression"/> is null.</returns>
        Private Function SerializeFieldReferenceExpression(ByVal [FieldReferenceExpression] As CodeFieldReferenceExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If FieldReferenceExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.FieldReferenceExpression
            Dim Xml = _
             <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.FieldReferenceExpression, Names.FieldReferenceExpression, Nothing) %>
                 FieldName=<%= FieldReferenceExpression.FieldName %>
                 >
                 <%= SerializeExpressionCommon(FieldReferenceExpression) %>
                 <%= SerializeExpression(FieldReferenceExpression.TargetObject, <TargetObject/>.Name) %>
             </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeIndexerExpression"/> as </summary>
        ''' <param name="IndexerExpression">A <see cref="CodeIndexerExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="IndexerExpression"/>. Null when <paramref name="IndexerExpression"/> is null.</returns>
        Private Function SerializeIndexerExpression(ByVal [IndexerExpression] As CodeIndexerExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If IndexerExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.IndexerExpression
            Dim Xml = _
             <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.IndexerExpression, Names.IndexerExpression, Nothing) %>>
                 <%= SerializeExpressionCommon(IndexerExpression) %>
                 <%= SerializeExpression(IndexerExpression.TargetObject, <TargetObject/>.Name) %>
                 <%= SerializeCollection(<Indices/>, IndexerExpression.Indices.AsTypeSafe, AddressOf SerializeExpression) %>
             </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeMethodInvokeExpression"/> as </summary>
        ''' <param name="MethodInvokeExpression">A <see cref="CodeMethodInvokeExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="MethodInvokeExpression"/>. Null when <paramref name="MethodInvokeExpression"/> is null.</returns>
        Private Function SerializeMethodInvokeExpression(ByVal [MethodInvokeExpression] As CodeMethodInvokeExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If MethodInvokeExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.MethodInvokeExpression
            Dim Xml = _
             <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.MethodInvokeExpression, Names.MethodInvokeExpression, Nothing) %>>
                 <%= SerializeExpressionCommon(MethodInvokeExpression) %>
                 <%= SerializeMethodReferenceExpression(MethodInvokeExpression.Method, <Method/>.Name) %>
                 <%= SerializeCollection(<Parameters/>, MethodInvokeExpression.Parameters.AsTypeSafe, AddressOf SerializeExpression) %>
             </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeMethodReferenceExpression"/> as </summary>
        ''' <param name="MethodReferenceExpression">A <see cref="CodeMethodReferenceExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="MethodReferenceExpression"/>. Null when <paramref name="MethodReferenceExpression"/> is null.</returns>
        Private Function SerializeMethodReferenceExpression(ByVal [MethodReferenceExpression] As CodeMethodReferenceExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If MethodReferenceExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.MethodReferenceExpression
            Dim Xml = _
             <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.MethodReferenceExpression, Names.MethodReferenceExpression, Nothing) %>
                 MethodName=<%= MethodReferenceExpression.MethodName %>
                 >
                 <%= SerializeExpressionCommon(MethodReferenceExpression) %>
                 <%= SerializeExpression(MethodReferenceExpression.TargetObject, <TargetObject/>.Name) %>
                 <%= SerializeCollection(<TypeArguments/>, MethodReferenceExpression.TypeArguments.AsTypeSafe, AddressOf SerializeTypeReference) %>
             </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeObjectCreateExpression"/> as </summary>
        ''' <param name="ObjectCreateExpression">A <see cref="CodeObjectCreateExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="ObjectCreateExpression"/>. Null when <paramref name="ObjectCreateExpression"/> is null.</returns>
        Private Function SerializeObjectCreateExpression(ByVal [ObjectCreateExpression] As CodeObjectCreateExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If ObjectCreateExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.ObjectCreateExpression
            Dim Xml = _
             <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.ObjectCreateExpression, Names.ObjectCreateExpression, Nothing) %>>
                 <%= SerializeExpressionCommon(ObjectCreateExpression) %>
                 <%= SerializeTypeReference(ObjectCreateExpression.CreateType, <CreateType/>.Name) %>
                 <%= SerializeCollection(<Parameters/>, ObjectCreateExpression.Parameters.AsTypeSafe, AddressOf SerializeExpression) %>
             </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodePrimitiveExpression"/> as </summary>
        ''' <param name="PrimitiveExpression">A <see cref="CodePrimitiveExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="PrimitiveExpression"/>. Null when <paramref name="PrimitiveExpression"/> is null.</returns>
        ''' <exception cref="NotSupportedException"><paramref name="PrimitiveExpression"/>.<see cref="CodePrimitiveExpression.Value">Value</see> is neither of supported primitive type nor <see cref="[Enum]"/>.</exception>
        Private Function SerializePrimitiveExpression(ByVal [PrimitiveExpression] As CodePrimitiveExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If PrimitiveExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.PrimitiveExpression
            Dim Xml = _
             <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.PrimitiveExpression, Names.PrimitiveExpression, Nothing) %>>
                 <%= SerializeExpressionCommon(PrimitiveExpression) %>
                 <%= SerializePrimitiveObject(PrimitiveExpression.Value) %>
             </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function
        ''' <summary>Serializes primitive object into <see cref="XElement"/></summary>
        ''' <param name="Obj">Object to serialize. Can be null.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="Obj"/></returns>
        ''' <exception cref="NotSupportedException"><paramref name="Obj"/> is neither of supported primitive type nor <see cref="[Enum]"/></exception>
        Private Function SerializePrimitiveObject(ByVal Obj As Object) As XElement
            If Obj IsNot Nothing AndAlso TypeOf Obj Is [Enum] Then Obj = DirectCast(Obj, [Enum]).GetValue
            If Obj Is Nothing Then : Return <Null/>
            ElseIf TypeOf Obj Is String Then : Return <String><%= DirectCast(Obj, String) %></String>
            ElseIf TypeOf Obj Is Char Then : Return <Char><%= DirectCast(Obj, Char) %></Char>
            ElseIf TypeOf Obj Is Byte Then : Return <Byte><%= DirectCast(Obj, Byte) %></Byte>
            ElseIf TypeOf Obj Is SByte Then : Return <SByte><%= DirectCast(Obj, SByte) %></SByte>
            ElseIf TypeOf Obj Is Int16 Then : Return <Int16><%= DirectCast(Obj, Int16) %></Int16>
            ElseIf TypeOf Obj Is UInt16 Then : Return <UInt16><%= DirectCast(Obj, UInt16) %></UInt16>
            ElseIf TypeOf Obj Is Int32 Then : Return <Int32><%= DirectCast(Obj, Int32) %></Int32>
            ElseIf TypeOf Obj Is UInt32 Then : Return <UInt32><%= DirectCast(Obj, UInt32) %></UInt32>
            ElseIf TypeOf Obj Is Int64 Then : Return <Int64><%= DirectCast(Obj, Int64) %></Int64>
            ElseIf TypeOf Obj Is UInt64 Then : Return <UInt64><%= DirectCast(Obj, UInt64) %></UInt64>
            ElseIf TypeOf Obj Is Decimal Then : Return <Decimal><%= DirectCast(Obj, Decimal) %></Decimal>
            ElseIf TypeOf Obj Is Single Then : Return <Single><%= DirectCast(Obj, Single) %></Single>
            ElseIf TypeOf Obj Is Double Then : Return <Double><%= DirectCast(Obj, Double) %></Double>
            ElseIf TypeOf Obj Is DateTime Then : Return <DateTime><%= DirectCast(Obj, DateTime) %></DateTime>
            ElseIf TypeOf Obj Is Boolean Then : Return <Boolean><%= DirectCast(Obj, Boolean) %></Boolean>
            Else : Throw New NotSupportedException(ResourcesT.Exceptions.ObjectOfType0CannotBeSerializedAsPrimitiveObject.f(Obj.GetType.Name))
            End If
        End Function


        ''' <summary>Serializes given <see cref="CodePropertyReferenceExpression"/> as </summary>
        ''' <param name="PropertyReferenceExpression">A <see cref="CodePropertyReferenceExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="PropertyReferenceExpression"/>. Null when <paramref name="PropertyReferenceExpression"/> is null.</returns>
        Private Function SerializePropertyReferenceExpression(ByVal [PropertyReferenceExpression] As CodePropertyReferenceExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If PropertyReferenceExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.PropertyReferenceExpression
            Dim Xml = _
            <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.PropertyReferenceExpression, Names.PropertyReferenceExpression, Nothing) %>
                PropertyName=<%= PropertyReferenceExpression.PropertyName %>
                >
                <%= SerializeExpressionCommon(PropertyReferenceExpression) %>
                <%= SerializeExpression(PropertyReferenceExpression.TargetObject, <TargetObject/>.Name) %>
            </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodePropertySetValueReferenceExpression"/> as </summary>
        ''' <param name="PropertySetValueReferenceExpression">A <see cref="CodePropertySetValueReferenceExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="PropertySetValueReferenceExpression"/>. Null when <paramref name="PropertySetValueReferenceExpression"/> is null.</returns>
        Private Function SerializePropertySetValueReferenceExpression(ByVal [PropertySetValueReferenceExpression] As CodePropertySetValueReferenceExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If PropertySetValueReferenceExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.PropertySetValueReferenceExpression
            Dim Xml = _
            <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.PropertySetValueReferenceExpression, Names.PropertySetValueReferenceExpression, Nothing) %>>
                <%= SerializeExpressionCommon(PropertySetValueReferenceExpression) %>
            </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeSnippetExpression"/> as </summary>
        ''' <param name="SnippetExpression">A <see cref="CodeSnippetExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="SnippetExpression"/>. Null when <paramref name="SnippetExpression"/> is null.</returns>
        Private Function SerializeSnippetExpression(ByVal [SnippetExpression] As CodeSnippetExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If SnippetExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.SnippetExpression
            Dim Xml = _
            <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.SnippetExpression, Names.SnippetExpression, Nothing) %>>
                <%= SerializeExpressionCommon(SnippetExpression) %>
                <Value><%= New XCData(SnippetExpression.Value) %></Value>
            </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeThisReferenceExpression"/> as </summary>
        ''' <param name="ThisReferenceExpression">A <see cref="CodeThisReferenceExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="ThisReferenceExpression"/>. Null when <paramref name="ThisReferenceExpression"/> is null.</returns>
        Private Function SerializeThisReferenceExpression(ByVal [ThisReferenceExpression] As CodeThisReferenceExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If ThisReferenceExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.ThisReferenceExpression
            Dim Xml = _
            <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.ThisReferenceExpression, Names.ThisReferenceExpression, Nothing) %>>
                <%= SerializeExpressionCommon(ThisReferenceExpression) %>
            </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeTypeOfExpression"/> as </summary>
        ''' <param name="TypeOfExpression">A <see cref="CodeTypeOfExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="TypeOfExpression"/>. Null when <paramref name="TypeOfExpression"/> is null.</returns>
        Private Function SerializeTypeOfExpression(ByVal [TypeOfExpression] As CodeTypeOfExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If TypeOfExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.TypeOfExpression
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.TypeOfExpression, Names.TypeOfExpression, Nothing) %>>
                    <%= SerializeExpressionCommon(TypeOfExpression) %>
                    <%= SerializeTypeReference(TypeOfExpression.Type, <Type/>.Name) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeTypeReferenceExpression"/> as </summary>
        ''' <param name="TypeReferenceExpression">A <see cref="CodeTypeReferenceExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="TypeReferenceExpression"/>. Null when <paramref name="TypeReferenceExpression"/> is null.</returns>
        Private Function SerializeTypeReferenceExpression(ByVal [TypeReferenceExpression] As CodeTypeReferenceExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If TypeReferenceExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.TypeReferenceExpression
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.TypeReferenceExpression, Names.TypeReferenceExpression, Nothing) %>>
                    <%= SerializeExpressionCommon(TypeReferenceExpression) %>
                    <%= SerializeTypeReference(TypeReferenceExpression.Type, <Type/>.Name) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeVariableReferenceExpression"/> as </summary>
        ''' <param name="VariableReferenceExpression">A <see cref="CodeVariableReferenceExpression"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="VariableReferenceExpression"/>. Null when <paramref name="VariableReferenceExpression"/> is null.</returns>
        Private Function SerializeVariableReferenceExpression(ByVal [VariableReferenceExpression] As CodeVariableReferenceExpression, Optional ByVal ElementName As XName = Nothing) As XElement
            If VariableReferenceExpression Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.VariableReferenceExpression
            Dim Xml = _
                <<%= ElementName %> xsi:type=<%= If(ElementName <> Names.VariableReferenceExpression, Names.VariableReferenceExpression, Nothing) %>
                    VariableName=<%= VariableReferenceExpression.VariableName %>
                    >
                    <%= SerializeExpressionCommon(VariableReferenceExpression) %>
                </>
            Xml.Attribute(xsiType).CollapseExtendedName()
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeCatchClause"/> as </summary>
        ''' <param name="CatchClause">A <see cref="CodeCatchClause"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="CatchClause"/>. Null when <paramref name="CatchClause"/> is null.</returns>
        Private Function SerializeCatchClause(ByVal [CatchClause] As CodeCatchClause, Optional ByVal ElementName As XName = Nothing) As XElement
            If CatchClause Is Nothing Then Return Nothing
            If ElementName Is Nothing Then ElementName = Names.CatchClause
            Dim Xml = _
                <<%= ElementName %> LocalName=<%= CatchClause.LocalName %>>
                    <%= SerializeTypeReference(CatchClause.CatchExceptionType, <CatchExceptionType/>.Name) %>
                    <%= SerializeCollection(<Statements/>, CatchClause.Statements.AsTypeSafe, AddressOf SerializeStatement) %>
                </>
            Return Xml
        End Function


        ''' <summary>Serializes given <see cref="CodeTypeDelegate"/> as </summary>
        ''' <param name="TypeDelegate">A <see cref="CodeTypeDelegate"/> to serialize</param>
        ''' <param name="ElementName">Name of element to be generated. If null, default name is used.</param>
        ''' <returns><see cref="XElement"/> representing serialized <paramref name="TypeDelegate"/>. Null when <paramref name="TypeDelegate"/> is null.</returns>
        Private Function SerializeTypeDelegate(ByVal [TypeDelegate] As CodeTypeDelegate, Optional ByVal ElementName As XName = Nothing) As XElement
            If TypeDelegate Is Nothing Then Return Nothing
            Return SerializeTypeDeclaration(TypeDelegate)
        End Function
#End Region
#Region "ICodeGenerator, ICodeparser"
        ''' <summary>Creates an escaped identifier for the specified value.</summary>
        ''' <returns><paramref name="value"/></returns>
        ''' <param name="value">The string to create an escaped identifier for.</param>
        ''' <remarks>As this class is language independent it has no knowledge about target language and so it allows all identifiers.</remarks>
        Private Function CreateEscapedIdentifier(ByVal value As String) As String Implements System.CodeDom.Compiler.ICodeGenerator.CreateEscapedIdentifier
            Return value
        End Function

        ''' <summary>Creates a valid identifier for the specified value.</summary>
        ''' <returns><paramref name="value"/></returns>
        ''' <param name="value">The string to generate a valid identifier for.</param>
        ''' <remarks>As this class is language independent it has no knowledge about target language and so it allows all identifiers.</remarks>
        Private Function CreateValidIdentifier(ByVal value As String) As String Implements System.CodeDom.Compiler.ICodeGenerator.CreateValidIdentifier
            Return value
        End Function

        ''' <summary>Generates code for the specified Code Document Object Model (CodeDOM) compilation unit and outputs it to the specified text writer using the specified options.</summary>
        ''' <param name="e">A <see cref="T:System.CodeDom.CodeCompileUnit" /> to generate code for.</param>
        ''' <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to.</param>
        ''' <param name="o">Ignored</param>
        ''' <exception cref="ArgumentNullException"><paramref name="e"/> or <paramref name="w"/> is null</exception>
        ''' <exception cref="NotSupportedException">Unsupported CodeDOM object or primitive type found.</exception>
        Public Sub GenerateCodeFromCompileUnit(ByVal e As System.CodeDom.CodeCompileUnit, ByVal w As System.IO.TextWriter, ByVal o As System.CodeDom.Compiler.CodeGeneratorOptions) Implements System.CodeDom.Compiler.ICodeGenerator.GenerateCodeFromCompileUnit
            If e Is Nothing Then Throw New ArgumentNullException("e")
            If w Is Nothing Then Throw New ArgumentNullException("w")
            Dim XML As XDocument = CompileUnit2Xml(e)
            XML.WriteTo(System.Xml.XmlWriter.Create(w))
        End Sub

        ''' <summary>Generates code for the specified Code Document Object Model (CodeDOM) expression and outputs it to the specified text writer.</summary>
        ''' <param name="e">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to generate code for.</param>
        ''' <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to.</param>
        ''' <param name="o">Ignored</param>
        ''' <exception cref="ArgumentNullException"><paramref name="e"/> or <paramref name="w"/> is null</exception>
        ''' <exception cref="NotSupportedException">Unsupported CodeDOM object or primitive type found.</exception>
        Public Sub GenerateCodeFromExpression(ByVal e As System.CodeDom.CodeExpression, ByVal w As System.IO.TextWriter, ByVal o As System.CodeDom.Compiler.CodeGeneratorOptions) Implements System.CodeDom.Compiler.ICodeGenerator.GenerateCodeFromExpression
            If e Is Nothing Then Throw New ArgumentNullException("e")
            If w Is Nothing Then Throw New ArgumentNullException("w")
            Dim xml As XDocument = Expression2Xml(e)
            xml.WriteTo(System.Xml.XmlWriter.Create(w))
        End Sub

        ''' <summary>Generates code for the specified Code Document Object Model (CodeDOM) namespace and outputs it to the specified text writer using the specified options.</summary>
        ''' <param name="e">A <see cref="T:System.CodeDom.CodeNamespace" /> that indicates the namespace to generate code for.                 </param>
        ''' <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to.</param>
        ''' <param name="o">Ignored</param>
        ''' <exception cref="ArgumentNullException"><paramref name="e"/> or <paramref name="w"/> is null</exception>
        ''' <exception cref="NotSupportedException">Unsupported CodeDOM object or primitive type found.</exception>
        Public Sub GenerateCodeFromNamespace(ByVal e As System.CodeDom.CodeNamespace, ByVal w As System.IO.TextWriter, ByVal o As System.CodeDom.Compiler.CodeGeneratorOptions) Implements System.CodeDom.Compiler.ICodeGenerator.GenerateCodeFromNamespace
            If e Is Nothing Then Throw New ArgumentNullException("e")
            If w Is Nothing Then Throw New ArgumentNullException("w")
            Dim xml As XDocument = Namespace2Xml(e)
            xml.WriteTo(System.Xml.XmlWriter.Create(w))
        End Sub

        ''' <summary>Generates code for the specified Code Document Object Model (CodeDOM) statement and outputs it to the specified text writer using the specified options.</summary>
        ''' <param name="e">A <see cref="T:System.CodeDom.CodeStatement" /> containing the CodeDOM elements to translate.                 </param>
        ''' <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to.</param>
        ''' <param name="o">Ignored</param>
        ''' <exception cref="ArgumentNullException"><paramref name="e"/> or <paramref name="w"/> is null</exception>
        ''' <exception cref="NotSupportedException">Unsupported CodeDOM object or primitive type found.</exception>
        Public Sub GenerateCodeFromStatement(ByVal e As System.CodeDom.CodeStatement, ByVal w As System.IO.TextWriter, ByVal o As System.CodeDom.Compiler.CodeGeneratorOptions) Implements System.CodeDom.Compiler.ICodeGenerator.GenerateCodeFromStatement
            If e Is Nothing Then Throw New ArgumentNullException("e")
            If w Is Nothing Then Throw New ArgumentNullException("w")
            Dim xml As XDocument = CodeDom2Xml(e)
            xml.WriteTo(System.Xml.XmlWriter.Create(w))
        End Sub

        ''' <summary>Generates code for the specified Code Document Object Model (CodeDOM) type declaration and outputs it to the specified text writer using the specified options.</summary>
        ''' <param name="e">A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that indicates the type to generate code for.                 </param>
        ''' <param name="w">The <see cref="T:System.IO.TextWriter" /> to output code to.                 </param>
        ''' <param name="o">Ignored</param>
        ''' <exception cref="ArgumentNullException"><paramref name="e"/> or <paramref name="w"/> is null</exception>
        ''' <exception cref="NotSupportedException">Unsupported CodeDOM object or primitive type found.</exception>
        Public Sub GenerateCodeFromType(ByVal e As System.CodeDom.CodeTypeDeclaration, ByVal w As System.IO.TextWriter, ByVal o As System.CodeDom.Compiler.CodeGeneratorOptions) Implements System.CodeDom.Compiler.ICodeGenerator.GenerateCodeFromType
            If e Is Nothing Then Throw New ArgumentNullException("e")
            If w Is Nothing Then Throw New ArgumentNullException("w")
            Dim xml As XDocument = Type2Xml(e)
            xml.WriteTo(System.Xml.XmlWriter.Create(w))
        End Sub

        ''' <summary>Gets the type indicated by the specified <see cref="T:System.CodeDom.CodeTypeReference" />.</summary>
        ''' <returns>A text representation of the specified type. This returns XML representation of type in form of the &lt;TypeReference> element</returns>
        ''' <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type to return.</param>
        Private Function GetTypeOutput(ByVal type As System.CodeDom.CodeTypeReference) As String Implements System.CodeDom.Compiler.ICodeGenerator.GetTypeOutput
            Return SerializeTypeReference(type).ToString
        End Function

        ''' <summary>Gets a value that indicates whether the specified value is a valid identifier for the current language.</summary>
        ''' <returns>true</returns>
        ''' <param name="value">The value to test for being a valid identifier.</param>
        ''' <remarks>As this class is language-independent and XML allows any sttrings as values of attributes and elements, this function always returns true.</remarks>
        Private Function IsValidIdentifier(ByVal value As String) As Boolean Implements System.CodeDom.Compiler.ICodeGenerator.IsValidIdentifier
            Return True
        End Function

        ''' <summary>Gets a value indicating whether the generator provides support for the language features represented by the specified <see cref="T:System.CodeDom.Compiler.GeneratorSupport" /> object.</summary>
        ''' <returns>true</returns>
        ''' <param name="capability">The capabilities to test the generator for.</param>
        ''' <remarks>As this class is specifically designed to support all the features of CodeDOM, this methods always returns true. However there is no specific support for <see cref="Compiler.GeneratorSupport.Resources"/> and <see cref="Compiler.GeneratorSupport.Win32Resources"/></remarks>
        Private Function Supports(ByVal capability As System.CodeDom.Compiler.GeneratorSupport) As Boolean Implements System.CodeDom.Compiler.ICodeGenerator.Supports
            Return True
        End Function

        ''' <summary>Throws an exception if the specified value is not a valid identifier.</summary>
        ''' <param name="value">The identifier to validate.</param>
        ''' <exception cref="T:System.ArgumentException">Never thrown</exception>
        Private Sub ValidateIdentifier(ByVal value As String) Implements System.CodeDom.Compiler.ICodeGenerator.ValidateIdentifier
            Return
        End Sub

        ''' <summary>Compiles the specified text stream into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
        ''' <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> that contains a representation of the parsed code.</returns>
        ''' <param name="codeStream">A <see cref="T:System.IO.TextReader" /> that can be used to read the code to be compiled.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="codeStream"/> is null</exception>
        ''' <exception cref="Xml.XmlException"><paramref name="codeStream"/> contains invalid XML -or- An error ocured while validating XML document obtained from <paramref name="codeStream"/> against XML-Schema</exception>
        ''' <exception cref="NotSupportedException">Root element of <paramref name="Xml"/> cannot be deserialized because it is unknown or its object representation does not derive from <see cref="CodeObject"/></exception>
        Public Function Parse(ByVal codeStream As System.IO.TextReader) As System.CodeDom.CodeCompileUnit Implements System.CodeDom.Compiler.ICodeParser.Parse
            If codeStream Is Nothing Then Throw New ArgumentException("codeStream")
            Return Xml2CodeDom(XDocument.Load(codeStream))
        End Function
#End Region
    End Class
End Namespace
'#End If