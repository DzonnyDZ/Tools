using System;
using System.CodeDom;
using System.Collections.Generic;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>Removes anny XML comments that consists of &lt;remaks/> only</summary>
    public class NoEmptyRemarks : ICodeExtension {
        /// <summary>Initializes the extension (this implementation does nothing)</summary>
        /// <param name="parameters">Initialization parameters (ignored)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        void ICodeExtension.Initialize(System.Collections.Generic.IDictionary<string, string> parameters) { }
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        public void Process(System.CodeDom.CodeNamespace code, System.Xml.Schema.XmlSchema schema, CodeDomProvider provider) {
            foreach (CodeTypeDeclaration type in code.Types) {
                ClearMember(type);
            }
        }
        private void ClearMember(CodeTypeMember member) {
            List<CodeCommentStatement> delcomm = new List<CodeCommentStatement>();
            foreach (CodeCommentStatement cmnt in member.Comments)
                if (cmnt.Comment.DocComment && cmnt.Comment.Text == "<remarks/>")
                    delcomm.Add(cmnt);
            foreach (CodeCommentStatement delcom in delcomm)
                member.Comments.Remove(delcom);
            if (member is CodeTypeDeclaration)
                foreach (CodeTypeMember membermember in ((CodeTypeDeclaration)member).Members)
                    ClearMember(membermember);
        }
    }
}
