using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>Converts array-based properties into List`1-based ones</summary>
    /// <remarks>
    /// This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.
    /// </remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.ArraysToGenericCollectionsExtension?>]]></code></example>
    public class ArraysToGenericCollectionsExtension : ICodeExtension {
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
                        if (member is CodeMemberField && ((CodeMemberField)member).Type.ArrayElementType != null) {
                            CodeMemberField field = (CodeMemberField)member;
                            // Change field type to collection.
                            field.Type = GetCollection(field.Type.ArrayElementType);
                            field.InitExpression = new CodeObjectCreateExpression(field.Type);
                            if (field.Name.EndsWith("Field"))
                                field.Comments.Add(new CodeCommentStatement(string.Format("<summary>Contains value of the <see cref='{0}'/> property</summary>", field.Name.Substring(0, field.Name.Length - "field".Length)), true));
                        } else if (member is CodeMemberProperty && ((CodeMemberProperty)member).Type.ArrayElementType != null) {
                            CodeMemberProperty Property = (CodeMemberProperty)member;
                            Property.Type = GetCollection(Property.Type.ArrayElementType);
                            Property.HasSet = false;
                        }
                    }
                }
            }
        }

        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        void IExtensionBase.Initialize(System.Collections.Generic.IDictionary<string, string> parameters) { }
        public CodeTypeReference GetCollection(CodeTypeReference forType) {
            CodeTypeReference col = new CodeTypeReference(typeof(System.Collections.Generic.List<>));
            col.TypeArguments.Add(forType);
            return col;
        }
    }
}
