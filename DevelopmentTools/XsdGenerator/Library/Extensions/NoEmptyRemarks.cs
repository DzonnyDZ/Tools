using System;
using System.CodeDom;
using System.Collections.Generic;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions
{
	/// <summary>
	/// Removes anny XML comments that consists of &lt;remaks/> only
	/// </summary>
	public class NoEmptyRemarks : ICodeExtension
	{
        void ICodeExtension.Initialize(System.Collections.Generic.IDictionary<string, string> Parameters) { }
        public void Process(System.CodeDom.CodeNamespace code, System.Xml.Schema.XmlSchema schema, CodeDomProvider Provider)
		{
			foreach ( CodeTypeDeclaration type in code.Types ){
                ClearMember(type);
			}
		}
        private void ClearMember(CodeTypeMember  member) {
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
