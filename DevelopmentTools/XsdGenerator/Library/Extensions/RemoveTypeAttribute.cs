using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;
using System.Collections.Generic;

/// <summary>This extension removed all attributes of given type from all types</summary>
/// <remarks>Attributes are combated only by <see cref="CodeAttributeDeclaration.AttributeType">.<see cref="CodeTypeReference.BaseType">BaseType</see>
/// <para>
/// This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
/// To use it add a processing instruction to your XSD file.
/// </para></remarks>
/// <example>How to use this extension in XSD file.
/// <code language="xml"><![CDATA[<?extension "Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.RemoveTypeAttribute" type="..."?>]]></code></example>
namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>Removes attribute from all types</summary>
    public class RemoveTypeAttribute : ICodeExtension {
        private string AttributeName;
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider provider) {
            foreach (CodeTypeDeclaration Type in code.Types) {
                ProcessType(Type);
            }
        }
        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters. This extension requires on parameter "<c>type</c>"</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c> which contains anme of atttribute type to be removed.</version>
        /// <exception cref="KeyNotFoundException">The key "type" is not present in <paramref name="parameters"/></exception>
        public void Initialize(System.Collections.Generic.IDictionary<string, string> parameters) {
            AttributeName = parameters["type"];
        }
        /// <summary>Removes attribute from type and its nested types</summary>
        /// <param name="Type">Represents attribute type</param>
        private void ProcessType(CodeTypeDeclaration Type) {
            List<CodeAttributeDeclaration> toRemove = new List<CodeAttributeDeclaration>();
            foreach (CodeAttributeDeclaration attr in Type.CustomAttributes) {
                if (attr.AttributeType.BaseType == AttributeName) {
                    toRemove.Add(attr);
                }
            }
            foreach (CodeAttributeDeclaration attr in toRemove) {
                Type.CustomAttributes.Remove(attr);
            }
            foreach (CodeTypeMember member in Type.Members) {
                if (member is CodeTypeDeclaration) ProcessType((CodeTypeDeclaration)member);
            }
        }
    }
}
