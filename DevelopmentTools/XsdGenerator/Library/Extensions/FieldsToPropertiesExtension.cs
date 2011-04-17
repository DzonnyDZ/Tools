using System;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>Converts the default public fields into properties backed by a private field.</summary>
    /// <remarks>
    /// This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.
    /// </remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.FieldsToPropertiesExtension?>]]></code></example>
    public class FieldsToPropertiesExtension : ICodeExtension {
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        public void Process(System.CodeDom.CodeNamespace code, System.Xml.Schema.XmlSchema schema, CodeDomProvider provider) {
            foreach (CodeTypeDeclaration type in code.Types) {
                if (type.IsClass || type.IsStruct) {
                    // Copy the colletion to an array for safety. We will be 
                    // changing this collection.
                    CodeTypeMember[] members = new CodeTypeMember[type.Members.Count];
                    type.Members.CopyTo(members, 0);

                    foreach (CodeTypeMember member in members) {
                        // Process fields only.
                        if (member is CodeMemberField) {
                            CodeMemberProperty prop = new CodeMemberProperty();
                            prop.Name = member.Name;

                            prop.Attributes = member.Attributes;
                            prop.Type = ((CodeMemberField)member).Type;

                            // Copy attributes from field to the property.
                            prop.CustomAttributes.AddRange(member.CustomAttributes);
                            member.CustomAttributes.Clear();

                            // Copy comments from field to the property.
                            prop.Comments.AddRange(member.Comments);
                            member.Comments.Clear();

                            // Modify the field.
                            member.Attributes = MemberAttributes.Private;
                            Char[] letters = member.Name.ToCharArray();
                            letters[0] = Char.ToLower(letters[0]);
                            member.Name = String.Concat("_", new string(letters));

                            prop.HasGet = true;
                            prop.HasSet = true;

                            // Add get/set statements pointing to field. Generates:
                            // return this._fieldname;
                            prop.GetStatements.Add(
                                new CodeMethodReturnStatement(
                                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(),
                                member.Name)));
                            // Generates:
                            // this._fieldname = value;
                            prop.SetStatements.Add(
                                new CodeAssignStatement(
                                new CodeFieldReferenceExpression(new CodeThisReferenceExpression(),
                                member.Name),
                                new CodeArgumentReferenceExpression("value")));

                            // Finally add the property to the type
                            type.Members.Add(prop);
                        }
                    }
                }
            }

        }
        /// <summary>Initializes the extension (this implementation does nothing)</summary>
        /// <param name="parameters">Initialization parameters (ignored)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        void IExtensionBase.Initialize(System.Collections.Generic.IDictionary<string, string> parameters) { }
    }
}
