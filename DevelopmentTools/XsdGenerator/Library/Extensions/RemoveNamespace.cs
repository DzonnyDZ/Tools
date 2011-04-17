using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;
using System.Collections.Generic;


namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>Removes specified XML namespace from XML serialization attribute <see cref="XmlTypeAttribute"/></summary>
    /// <remarks>
    /// This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.
    /// </remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension "Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.RemoveNamespace" ns="..."?>]]></code></example>
    /// <version version="1.5.3">This class was re-introduced in version 1.5.3</version>
    public class RemoveNamespace : ICodeExtension {
        private string ns;

        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider provider) {
            List<CodeTypeDeclaration> torem = new List<CodeTypeDeclaration>();
            foreach (CodeTypeDeclaration Type in code.Types) {
                foreach (CodeAttributeDeclaration attr in Type.CustomAttributes) {
                    if (attr.AttributeType.BaseType == typeof(System.Xml.Serialization.XmlTypeAttribute).FullName) {
                        foreach (CodeAttributeArgument arg in attr.Arguments) {
                            if (arg.Name == "Namespace" && arg.Value is CodePrimitiveExpression && ((CodePrimitiveExpression)arg.Value).Value is string && (string)((CodePrimitiveExpression)arg.Value).Value == ns)
                                torem.Add(Type);
                        }
                    }
                }
            }
            foreach (var rem in torem)
                code.Types.Remove(rem);
        }
        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters. This class expects one required parameter - <c>ns</c> which's value is URI of namespace to be removed.</param>
        /// <exception cref="KeyNotFoundException"><paramref name="parameters"/> does not contain key <c>ns</c>.</exception>
        public void Initialize(System.Collections.Generic.IDictionary<string, string> parameters) {
            ns = parameters["ns"];
        }
    }
}
