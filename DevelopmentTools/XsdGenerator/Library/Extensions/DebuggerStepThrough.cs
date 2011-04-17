using System;
using System.CodeDom;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>This extension adds the <see cref="DebuggerStepThroughAttribute"/> to property accessors, removes it from types and methods.</summary>
    /// <remarks>
    /// This class implements CodeDOM-based and text-based post-processing extensions for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.
    /// </remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.DebuggerStepThrough?>]]></code>
    /// or
    /// <code language="xml"><![CDATA[<?post-process Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.DebuggerStepThrough?>]]></code>
    /// </example>
    public class DebuggerStepThrough : ICodeExtension, IPostExtension {
        /// <summary>Initializes the extension (this implementation does nothing)</summary>
        /// <param name="parameters">Initialization parameters (ignored)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        /// <version version="1.5.4">Implementations of <c>Initialize</c> method for <see cref="ICodeExtension"/> and <see cref="IPostExtension"/> merged.</version>
        void IExtensionBase.Initialize(System.Collections.Generic.IDictionary<string, string> parameters) { }
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        public void Process(System.CodeDom.CodeNamespace code, System.Xml.Schema.XmlSchema schema, CodeDomProvider provider) {
            foreach (CodeTypeDeclaration type in code.Types) {
                Process(type);
            }
        }
        private void Process(CodeTypeMember member) {
            CodeAttributeDeclaration memberAttr = GetAttribute(member);
            if (member is CodeTypeDeclaration) {
                if (memberAttr != null) member.CustomAttributes.Remove(memberAttr);
                foreach (CodeTypeMember membermember in ((CodeTypeDeclaration)member).Members)
                    Process(membermember);
            } else if (member is CodeMemberMethod) {
                if (memberAttr == null) member.CustomAttributes.Add(NewAttribute);
            } else if (member is CodeMemberProperty) {
                CodeMemberProperty prp = (CodeMemberProperty)member;
                if (prp.HasGet) prp.GetStatements.Insert(0, new CodeCommentStatement(FirtsLineOfAccessor));
                if (prp.HasSet) prp.SetStatements.Insert(0, new CodeCommentStatement(FirtsLineOfAccessor));
            }
        }

        const string FirtsLineOfAccessor = "{56DBB847-DCC7-4227-9557-28B2DC23C09C}";

        private CodeAttributeDeclaration GetAttribute(CodeTypeMember member) {
            if (member.CustomAttributes != null)
                foreach (CodeAttributeDeclaration attr in member.CustomAttributes)
                    if (attr.AttributeType.BaseType == typeof(DebuggerStepThroughAttribute).FullName)
                        return attr;
            return null;
        }
        private static CodeAttributeDeclaration NewAttribute { get { return new CodeAttributeDeclaration(new CodeTypeReference(typeof(System.Diagnostics.DebuggerStepThroughAttribute))); } }


        /// <summary>Called when extension shall process generated code</summary>
        /// <param name="code">The code</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        public void PostProcess(ref string code, CodeDomProvider provider) {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            provider.GenerateCodeFromStatement(new CodeCommentStatement(FirtsLineOfAccessor), tw, new System.CodeDom.Compiler.CodeGeneratorOptions());
            string srch = tw.GetStringBuilder().ToString();
            if (srch.EndsWith("\r\n")) srch = srch.Substring(0, srch.Length - 2);
            else if (srch.EndsWith("\r") || srch.EndsWith("\n")) srch = srch.Substring(0, srch.Length - 1);
            tw = new System.IO.StringWriter();
            CodeTypeDeclaration foo = new CodeTypeDeclaration("foo");
            foo.CustomAttributes.Add(NewAttribute);
            provider.GenerateCodeFromType(foo, tw, new System.CodeDom.Compiler.CodeGeneratorOptions());
            string attr = new System.IO.StringReader(tw.GetStringBuilder().ToString()).ReadLine();
            System.IO.StringReader sr = new System.IO.StringReader(code);
            List<String> Lines = new List<string>();
            do {
                string line = sr.ReadLine();
                if (line == null) break;
                if (line.EndsWith(srch))
                    Lines[Lines.Count - 1] = attr + "\r\n" + Lines[Lines.Count - 1];
                else
                    Lines.Add(line);
            } while (true);
            System.Text.StringBuilder b = new System.Text.StringBuilder();
            foreach (string line in Lines)
                b.AppendLine(line);
            code = b.ToString();
        }


    }
}
