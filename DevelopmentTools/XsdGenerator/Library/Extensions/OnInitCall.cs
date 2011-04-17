using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {

    /// <summary>Adds a call to <c>OnBeforeInit</c> at the very beignning of each CTor</summary>
    /// <remarks>
    /// This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.
    /// </remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.OnBeforeInitCall?>]]></code></example>
    public class OnBeforeInitCall : ICodeExtension {
        /// <summary>Initializes the extension (this implementation does nothing)</summary>
        /// <param name="parameters">Initialization parameters (ignored)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        void IExtensionBase.Initialize(System.Collections.Generic.IDictionary<string, string> parameters) { }
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider provider) {
            foreach (CodeTypeDeclaration type in code.Types) {
                ProcessType(type);
            }
        }
        private void ProcessType(CodeTypeDeclaration type) {
            foreach (CodeTypeMember member in type.Members) {
                if (member is CodeTypeDeclaration) ProcessType((CodeTypeDeclaration)member);
                else if (member is CodeConstructor) {
                    CodeConstructor ctor = (CodeConstructor)member;
                    ctor.Statements.Insert(0, new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "OnBeforeInit", new CodeExpression[] { })));
                }
            }
        }
    }

    /// <summary>Adds a call to <c>OnAfterInit</c> at the end of each CTor</summary>
    public class OnAfterInitCall : ICodeExtension {
        /// <summary>Initializes the extension (this implementation does nothing)</summary>
        /// <param name="parameters">Initialization parameters (ignored)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        void IExtensionBase.Initialize(System.Collections.Generic.IDictionary<string, string> parameters) { }
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider provider) {
            foreach (CodeTypeDeclaration type in code.Types) {
                ProcessType(type);
            }
        }
        private void ProcessType(CodeTypeDeclaration type) {
            foreach (CodeTypeMember member in type.Members) {
                if (member is CodeTypeDeclaration) ProcessType((CodeTypeDeclaration)member);
                else if (member is CodeConstructor) {
                    CodeConstructor ctor = (CodeConstructor)member;
                    ctor.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "OnAfterInit", new CodeExpression[] { })));
                }
            }
        }
    }


}
