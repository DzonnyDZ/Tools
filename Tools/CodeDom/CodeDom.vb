
        
        ' GENERATED FILE -- DO NOT EDIT
        ' Generator: TransformCodeGenerator, Version=1.5.0.36089, Culture=neutral, PublicKeyToken=373c02ac923768e6
        ' Version: 1.5.0.36089
        ' Generated code from "CodeDom.xsd"
        ' Created: 8. října 2008 by: DZONNY\Honza
    
        Imports System.Xml.Linq, <xmlns="http://dzonny.cz/xml/schemas/CodeDom">
        Imports System.CodeDom
        #Region "Generated code"
        Namespace  CodeDomT
        
        Partial Class Xml2CodeDom
            ''' <summary>Contains definitions of XML elements used by <see cref="Xml2CodeDom"/> class</summary>
            Private Class Names
                ''' <summary>This method actually does not exists which prevents this class of being instantiated</summary>
                Private Partial Sub New()
                End Sub
                
                    ''' <summary>Represents name of the &lt;CompileUnit> element and CompileUnit type</summary>
                    Public Shared Readonly [CompileUnit] As XName = <CompileUnit/>.Name
                
                    ''' <summary>Represents name of the &lt;Namespace> element and Namespace type</summary>
                    Public Shared Readonly [Namespace] As XName = <Namespace/>.Name
                
                    ''' <summary>Represents name of the &lt;AttributeDeclaration> element and AttributeDeclaration type</summary>
                    Public Shared Readonly [AttributeDeclaration] As XName = <AttributeDeclaration/>.Name
                
                    ''' <summary>Represents name of the &lt;AssemblyReference> element and AssemblyReference type</summary>
                    Public Shared Readonly [AssemblyReference] As XName = <AssemblyReference/>.Name
                
                    ''' <summary>Represents name of the &lt;Directive> element and Directive type</summary>
                    Public Shared Readonly [Directive] As XName = <Directive/>.Name
                
                    ''' <summary>Represents name of the &lt;ChecksumPragma> element and ChecksumPragma type</summary>
                    Public Shared Readonly [ChecksumPragma] As XName = <ChecksumPragma/>.Name
                
                    ''' <summary>Represents name of the &lt;RegionDirective> element and RegionDirective type</summary>
                    Public Shared Readonly [RegionDirective] As XName = <RegionDirective/>.Name
                
                    ''' <summary>Represents name of the &lt;NamespaceImport> element and NamespaceImport type</summary>
                    Public Shared Readonly [NamespaceImport] As XName = <NamespaceImport/>.Name
                
                    ''' <summary>Represents name of the &lt;CommentStatement> element and CommentStatement type</summary>
                    Public Shared Readonly [CommentStatement] As XName = <CommentStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;TypeDeclaration> element and TypeDeclaration type</summary>
                    Public Shared Readonly [TypeDeclaration] As XName = <TypeDeclaration/>.Name
                
                    ''' <summary>Represents name of the &lt;AttributeArgument> element and AttributeArgument type</summary>
                    Public Shared Readonly [AttributeArgument] As XName = <AttributeArgument/>.Name
                
                    ''' <summary>Represents name of the &lt;TypeReference> element and TypeReference type</summary>
                    Public Shared Readonly [TypeReference] As XName = <TypeReference/>.Name
                
                    ''' <summary>Represents name of the &lt;TypeMember> element and TypeMember type</summary>
                    Public Shared Readonly [TypeMember] As XName = <TypeMember/>.Name
                
                    ''' <summary>Represents name of the &lt;TypeParameter> element and TypeParameter type</summary>
                    Public Shared Readonly [TypeParameter] As XName = <TypeParameter/>.Name
                
                    ''' <summary>Represents name of the &lt;MemberEvent> element and MemberEvent type</summary>
                    Public Shared Readonly [MemberEvent] As XName = <MemberEvent/>.Name
                
                    ''' <summary>Represents name of the &lt;MemberField> element and MemberField type</summary>
                    Public Shared Readonly [MemberField] As XName = <MemberField/>.Name
                
                    ''' <summary>Represents name of the &lt;MemberMethod> element and MemberMethod type</summary>
                    Public Shared Readonly [MemberMethod] As XName = <MemberMethod/>.Name
                
                    ''' <summary>Represents name of the &lt;ParameterDeclarationExpression> element and ParameterDeclarationExpression type</summary>
                    Public Shared Readonly [ParameterDeclarationExpression] As XName = <ParameterDeclarationExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;Statement> element and Statement type</summary>
                    Public Shared Readonly [Statement] As XName = <Statement/>.Name
                
                    ''' <summary>Represents name of the &lt;MemberProperty> element and MemberProperty type</summary>
                    Public Shared Readonly [MemberProperty] As XName = <MemberProperty/>.Name
                
                    ''' <summary>Represents name of the &lt;Expression> element and Expression type</summary>
                    Public Shared Readonly [Expression] As XName = <Expression/>.Name
                
                    ''' <summary>Represents name of the &lt;SnippetTypeMember> element and SnippetTypeMember type</summary>
                    Public Shared Readonly [SnippetTypeMember] As XName = <SnippetTypeMember/>.Name
                
                    ''' <summary>Represents name of the &lt;Constructor> element and Constructor type</summary>
                    Public Shared Readonly [Constructor] As XName = <Constructor/>.Name
                
                    ''' <summary>Represents name of the &lt;EntryPointMethod> element and EntryPointMethod type</summary>
                    Public Shared Readonly [EntryPointMethod] As XName = <EntryPointMethod/>.Name
                
                    ''' <summary>Represents name of the &lt;TypeConstructor> element and TypeConstructor type</summary>
                    Public Shared Readonly [TypeConstructor] As XName = <TypeConstructor/>.Name
                
                    ''' <summary>Represents name of the &lt;LinePragma> element and LinePragma type</summary>
                    Public Shared Readonly [LinePragma] As XName = <LinePragma/>.Name
                
                    ''' <summary>Represents name of the &lt;Comment> element and Comment type</summary>
                    Public Shared Readonly [Comment] As XName = <Comment/>.Name
                
                    ''' <summary>Represents name of the &lt;SnippetCompileUnit> element and SnippetCompileUnit type</summary>
                    Public Shared Readonly [SnippetCompileUnit] As XName = <SnippetCompileUnit/>.Name
                
                    ''' <summary>Represents name of the &lt;AssignStatement> element and AssignStatement type</summary>
                    Public Shared Readonly [AssignStatement] As XName = <AssignStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;AttachEventStatement> element and AttachEventStatement type</summary>
                    Public Shared Readonly [AttachEventStatement] As XName = <AttachEventStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;ConditionStatement> element and ConditionStatement type</summary>
                    Public Shared Readonly [ConditionStatement] As XName = <ConditionStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;ExpressionStatement> element and ExpressionStatement type</summary>
                    Public Shared Readonly [ExpressionStatement] As XName = <ExpressionStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;GotoStatement> element and GotoStatement type</summary>
                    Public Shared Readonly [GotoStatement] As XName = <GotoStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;IterationStatement> element and IterationStatement type</summary>
                    Public Shared Readonly [IterationStatement] As XName = <IterationStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;LabeledStatement> element and LabeledStatement type</summary>
                    Public Shared Readonly [LabeledStatement] As XName = <LabeledStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;MethodReturnStatement> element and MethodReturnStatement type</summary>
                    Public Shared Readonly [MethodReturnStatement] As XName = <MethodReturnStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;RemoveEventStatement> element and RemoveEventStatement type</summary>
                    Public Shared Readonly [RemoveEventStatement] As XName = <RemoveEventStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;SnippetStatement> element and SnippetStatement type</summary>
                    Public Shared Readonly [SnippetStatement] As XName = <SnippetStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;ThrowExceptionStatement> element and ThrowExceptionStatement type</summary>
                    Public Shared Readonly [ThrowExceptionStatement] As XName = <ThrowExceptionStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;TryCatchFinallyStatement> element and TryCatchFinallyStatement type</summary>
                    Public Shared Readonly [TryCatchFinallyStatement] As XName = <TryCatchFinallyStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;VariableDeclarationStatement> element and VariableDeclarationStatement type</summary>
                    Public Shared Readonly [VariableDeclarationStatement] As XName = <VariableDeclarationStatement/>.Name
                
                    ''' <summary>Represents name of the &lt;ArgumentReferenceExpression> element and ArgumentReferenceExpression type</summary>
                    Public Shared Readonly [ArgumentReferenceExpression] As XName = <ArgumentReferenceExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;ArrayCreateExpression> element and ArrayCreateExpression type</summary>
                    Public Shared Readonly [ArrayCreateExpression] As XName = <ArrayCreateExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;ArrayIndexerExpression> element and ArrayIndexerExpression type</summary>
                    Public Shared Readonly [ArrayIndexerExpression] As XName = <ArrayIndexerExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;BaseReferenceExpression> element and BaseReferenceExpression type</summary>
                    Public Shared Readonly [BaseReferenceExpression] As XName = <BaseReferenceExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;BinaryOperatorExpression> element and BinaryOperatorExpression type</summary>
                    Public Shared Readonly [BinaryOperatorExpression] As XName = <BinaryOperatorExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;CastExpression> element and CastExpression type</summary>
                    Public Shared Readonly [CastExpression] As XName = <CastExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;DefaultValueExpression> element and DefaultValueExpression type</summary>
                    Public Shared Readonly [DefaultValueExpression] As XName = <DefaultValueExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;DelegateCreateExpression> element and DelegateCreateExpression type</summary>
                    Public Shared Readonly [DelegateCreateExpression] As XName = <DelegateCreateExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;DelegateInvokeExpression> element and DelegateInvokeExpression type</summary>
                    Public Shared Readonly [DelegateInvokeExpression] As XName = <DelegateInvokeExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;DirectionExpression> element and DirectionExpression type</summary>
                    Public Shared Readonly [DirectionExpression] As XName = <DirectionExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;EventReferenceExpression> element and EventReferenceExpression type</summary>
                    Public Shared Readonly [EventReferenceExpression] As XName = <EventReferenceExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;FieldReferenceExpression> element and FieldReferenceExpression type</summary>
                    Public Shared Readonly [FieldReferenceExpression] As XName = <FieldReferenceExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;IndexerExpression> element and IndexerExpression type</summary>
                    Public Shared Readonly [IndexerExpression] As XName = <IndexerExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;MethodInvokeExpression> element and MethodInvokeExpression type</summary>
                    Public Shared Readonly [MethodInvokeExpression] As XName = <MethodInvokeExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;MethodReferenceExpression> element and MethodReferenceExpression type</summary>
                    Public Shared Readonly [MethodReferenceExpression] As XName = <MethodReferenceExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;ObjectCreateExpression> element and ObjectCreateExpression type</summary>
                    Public Shared Readonly [ObjectCreateExpression] As XName = <ObjectCreateExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;PrimitiveExpression> element and PrimitiveExpression type</summary>
                    Public Shared Readonly [PrimitiveExpression] As XName = <PrimitiveExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;PropertyReferenceExpression> element and PropertyReferenceExpression type</summary>
                    Public Shared Readonly [PropertyReferenceExpression] As XName = <PropertyReferenceExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;PropertySetValueReferenceExpression> element and PropertySetValueReferenceExpression type</summary>
                    Public Shared Readonly [PropertySetValueReferenceExpression] As XName = <PropertySetValueReferenceExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;SnippetExpression> element and SnippetExpression type</summary>
                    Public Shared Readonly [SnippetExpression] As XName = <SnippetExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;ThisReferenceExpression> element and ThisReferenceExpression type</summary>
                    Public Shared Readonly [ThisReferenceExpression] As XName = <ThisReferenceExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;TypeOfExpression> element and TypeOfExpression type</summary>
                    Public Shared Readonly [TypeOfExpression] As XName = <TypeOfExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;TypeReferenceExpression> element and TypeReferenceExpression type</summary>
                    Public Shared Readonly [TypeReferenceExpression] As XName = <TypeReferenceExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;VariableReferenceExpression> element and VariableReferenceExpression type</summary>
                    Public Shared Readonly [VariableReferenceExpression] As XName = <VariableReferenceExpression/>.Name
                
                    ''' <summary>Represents name of the &lt;CatchClause> element and CatchClause type</summary>
                    Public Shared Readonly [CatchClause] As XName = <CatchClause/>.Name
                
                    ''' <summary>Represents name of the &lt;TypeDelegate> element and TypeDelegate type</summary>
                    Public Shared Readonly [TypeDelegate] As XName = <TypeDelegate/>.Name
                
                    ''' <summary>Represents name of the &lt;UserData> element and UserData type</summary>
                    Public Shared Readonly [UserData] As XName = <UserData/>.Name
                
                    ''' <summary>Represents name of the &lt;String> element and String type</summary>
                    Public Shared Readonly [String] As XName = <String/>.Name
                
                    ''' <summary>Represents name of the &lt;Char> element and Char type</summary>
                    Public Shared Readonly [Char] As XName = <Char/>.Name
                
                    ''' <summary>Represents name of the &lt;Byte> element and Byte type</summary>
                    Public Shared Readonly [Byte] As XName = <Byte/>.Name
                
                    ''' <summary>Represents name of the &lt;SByte> element and SByte type</summary>
                    Public Shared Readonly [SByte] As XName = <SByte/>.Name
                
                    ''' <summary>Represents name of the &lt;Int16> element and Int16 type</summary>
                    Public Shared Readonly [Int16] As XName = <Int16/>.Name
                
                    ''' <summary>Represents name of the &lt;UInt16> element and UInt16 type</summary>
                    Public Shared Readonly [UInt16] As XName = <UInt16/>.Name
                
                    ''' <summary>Represents name of the &lt;Int32> element and Int32 type</summary>
                    Public Shared Readonly [Int32] As XName = <Int32/>.Name
                
                    ''' <summary>Represents name of the &lt;UInt32> element and UInt32 type</summary>
                    Public Shared Readonly [UInt32] As XName = <UInt32/>.Name
                
                    ''' <summary>Represents name of the &lt;Int64> element and Int64 type</summary>
                    Public Shared Readonly [Int64] As XName = <Int64/>.Name
                
                    ''' <summary>Represents name of the &lt;UInt64> element and UInt64 type</summary>
                    Public Shared Readonly [UInt64] As XName = <UInt64/>.Name
                
                    ''' <summary>Represents name of the &lt;Decimal> element and Decimal type</summary>
                    Public Shared Readonly [Decimal] As XName = <Decimal/>.Name
                
                    ''' <summary>Represents name of the &lt;Single> element and Single type</summary>
                    Public Shared Readonly [Single] As XName = <Single/>.Name
                
                    ''' <summary>Represents name of the &lt;Double> element and Double type</summary>
                    Public Shared Readonly [Double] As XName = <Double/>.Name
                
                    ''' <summary>Represents name of the &lt;DateTime> element and DateTime type</summary>
                    Public Shared Readonly [DateTime] As XName = <DateTime/>.Name
                
                    ''' <summary>Represents name of the &lt;Null> element and Null type</summary>
                    Public Shared Readonly [Null] As XName = <Null/>.Name
                
                    ''' <summary>Represents name of the &lt;Boolean> element and Boolean type</summary>
                    Public Shared Readonly [Boolean] As XName = <Boolean/>.Name
                
            End Class
            
            
        End Class
        
        
    
        End Namespace
        #End Region
    