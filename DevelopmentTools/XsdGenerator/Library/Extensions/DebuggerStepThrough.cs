using System;
using System.CodeDom;
using System.Collections.Generic;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions
{
	public class DebuggerStepThrough : ICodeExtension,IPostExtension
	{
        void ICodeExtension.Initialize(System.Collections.Generic.IDictionary<string, string> Parameters) { }
        void IPostExtension.Initialize(System.Collections.Generic.IDictionary<string, string> Parameters) { }
		public void Process( System.CodeDom.CodeNamespace code, System.Xml.Schema.XmlSchema schema,CodeDomProvider Provider)
		{
			foreach ( CodeTypeDeclaration type in code.Types ){
                Process(type);
			}
		}
        private void Process(CodeTypeMember member) {
            CodeAttributeDeclaration  memberAttr = GetAttribute(member);
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
                    if (attr.AttributeType.BaseType == "System.Diagnostics.DebuggerStepThroughAttribute")
                        return attr;
            return null;
        }
        private static CodeAttributeDeclaration NewAttribute{get{return new CodeAttributeDeclaration(new CodeTypeReference(typeof(System.Diagnostics.DebuggerStepThroughAttribute)));}}


        public void PostProcess(ref string code, CodeDomProvider Provider) {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            Provider.GenerateCodeFromStatement(new CodeCommentStatement(FirtsLineOfAccessor),tw,new System.CodeDom.Compiler.CodeGeneratorOptions());
            string srch = tw.GetStringBuilder().ToString();
            if(srch.EndsWith("\r\n")) srch = srch.Substring(0,srch.Length-2);
            else if(srch.EndsWith("\r") || srch.EndsWith("\n")) srch=srch.Substring(0,srch.Length-1);
            tw = new System.IO.StringWriter();
            CodeTypeDeclaration foo = new CodeTypeDeclaration("foo");
            foo.CustomAttributes.Add(NewAttribute);
            Provider.GenerateCodeFromType(foo,tw,new System.CodeDom.Compiler.CodeGeneratorOptions());
            string attr = new System.IO.StringReader(tw.GetStringBuilder().ToString()).ReadLine();
            System.IO.StringReader sr = new System.IO.StringReader(code);
            List<String> Lines = new List<string>();
            do{
                string line=sr.ReadLine();
                if(line == null) break;
                if(line.EndsWith(srch))
                    Lines[Lines.Count-1] = attr + "\r\n" +  Lines[Lines.Count-1];
                else
                    Lines.Add(line);
            } while(true);                                                         
            System.Text.StringBuilder b=new System.Text.StringBuilder();
            foreach (string line in Lines)
                b.AppendLine(line);
            code = b.ToString();
        }


    }
}
