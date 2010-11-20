using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>
    /// Converts array-based properties into collection-based ones, and 
    /// creates a typed <see cref="CollectionBase"/> inherited class for it.
    /// </summary>
    public class ArraysToCollectionsExtension : ICodeExtension {
        #region ICodeExtension Members

        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider provider) {
            // Copy as we will be adding types.
            CodeTypeDeclaration[] types =
                new CodeTypeDeclaration[code.Types.Count];
            code.Types.CopyTo(types, 0);

            foreach (CodeTypeDeclaration type in types) {
                if (type.IsClass || type.IsStruct) {
                    foreach (CodeTypeMember member in type.Members) {
                        // Process fields only.
                        CodeTypeDeclaration newType = null;
                        if (member is CodeMemberField &&
                            ((CodeMemberField)member).Type.ArrayElementType != null) {
                            CodeMemberField field = (CodeMemberField)member;
                            CodeTypeDeclaration col = GetCollection(
                                field.Type.ArrayElementType);
                            // Change field type to collection.
                            field.Type = new CodeTypeReference((newType = col).Name);
                        } else if (member is CodeMemberProperty &&
                        ((CodeMemberProperty)member).Type.ArrayElementType != null) {
                            CodeMemberProperty Property = (CodeMemberProperty)member;
                            CodeTypeDeclaration col = GetCollection(
                                Property.Type.ArrayElementType);
                            // Change Property type to collection.
                            Property.Type = new CodeTypeReference((newType = col).Name);
                        }
                        if (newType != null) {
                            bool Found = false;
                            foreach (CodeTypeDeclaration oldType in code.Types) {
                                if (newType.Name == oldType.Name) {
                                    Found = true;
                                    break;
                                }
                            }
                            if (!Found) code.Types.Add(newType);
                        }
                    }
                }
            }
        }

        /// <summary>Initializes the extension (this implementation does nothing)</summary>
        /// <param name="parameters">Initialization parameters (ignored)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        void ICodeExtension.Initialize(System.Collections.Generic.IDictionary<string, string> parameters) { }
        #endregion

        /// <summary>Creates a collection type</summary>
        /// <param name="forType">Type to create collection of</param>
        /// <returns>A <see cref="CodeTypeDeclaration"/> representing collection type</returns>
        /// <version version="1.5.3">Documentation added</version>
        public CodeTypeDeclaration GetCollection(CodeTypeReference forType) {
            CodeTypeDeclaration col = new CodeTypeDeclaration(
                forType.BaseType + "Collection");
            col.BaseTypes.Add(typeof(CollectionBase));
            col.Attributes = MemberAttributes.Final | MemberAttributes.Public;

            // Add method
            CodeMemberMethod add = new CodeMemberMethod();
            add.Attributes = MemberAttributes.Final | MemberAttributes.Public;
            add.Name = "Add";
            add.ReturnType = new CodeTypeReference(typeof(int));
            add.Parameters.Add(new CodeParameterDeclarationExpression(
                forType, "value"));
            // Generates: return base.InnerList.Add( value );
            add.Statements.Add(new CodeMethodReturnStatement(
                new CodeMethodInvokeExpression(
                    new CodePropertyReferenceExpression(
                        new CodeBaseReferenceExpression(), "InnerList"),
                    "Add",
                    new CodeExpression[] { new CodeArgumentReferenceExpression("value") }
                     )
                 )
             );

            // Add to type.
            col.Members.Add(add);

            // Indexer property ( 'this' )
            CodeMemberProperty indexer = new CodeMemberProperty();
            indexer.Attributes = MemberAttributes.Final | MemberAttributes.Public;
            indexer.Name = "Item";
            indexer.Type = forType;
            indexer.Parameters.Add(new CodeParameterDeclarationExpression(
                typeof(int), "idx"));
            indexer.HasGet = true;
            indexer.HasSet = true;
            // Generates: return ( theType ) base.InnerList[idx];
            indexer.GetStatements.Add(
                new CodeMethodReturnStatement(
                    new CodeCastExpression(
                        forType,
                        new CodeIndexerExpression(
                            new CodePropertyReferenceExpression(
                                new CodeBaseReferenceExpression(),
                                "InnerList"),
                            new CodeExpression[] { new CodeArgumentReferenceExpression("idx") })
                         )
                     )
                 );
            // Generates: base.InnerList[idx] = value;
            indexer.SetStatements.Add(
                new CodeAssignStatement(
                    new CodeIndexerExpression(
                        new CodePropertyReferenceExpression(
                            new CodeBaseReferenceExpression(),
                            "InnerList"),
                        new CodeExpression[] { new CodeArgumentReferenceExpression("idx") }),
                    new CodeArgumentReferenceExpression("value")
                 )
             );

            // Add to type.
            col.Members.Add(indexer);

            return col;
        }
       
    }
}
