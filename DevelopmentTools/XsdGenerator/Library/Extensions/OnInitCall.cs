using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions{

    /// <summary>Adds a call to OnBeforeInit at tthe very beignning of each CTor</summary>
	public class OnBeforeInitCall : ICodeExtension	{
        void ICodeExtension.Initialize(System.Collections.Generic.IDictionary<string, string> Parameters) { }
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider Provider) {
            foreach(CodeTypeDeclaration type in code.Types) {
                ProcessType(type);
            }
		}
        private void ProcessType(CodeTypeDeclaration type) {
            foreach(CodeTypeMember member in type.Members) {
                if(member is CodeTypeDeclaration) ProcessType((CodeTypeDeclaration)member);
                else if(member is CodeConstructor) {
                    CodeConstructor ctor = (CodeConstructor) member;
                    ctor.Statements.Insert(0, new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "OnBeforeInit", new CodeExpression[] { })));
                }
            }
        }
	}

    /// <summary>Adds a call to OnAfterInit at tthe very beignning of each CTor</summary>
    public class OnAfterInitCall:ICodeExtension {
        void ICodeExtension.Initialize(System.Collections.Generic.IDictionary<string, string> Parameters) { }
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider Provider) {
            foreach(CodeTypeDeclaration type in code.Types) {
                ProcessType(type);
            }
        }
        private void ProcessType(CodeTypeDeclaration type) {
            foreach(CodeTypeMember member in type.Members) {
                if(member is CodeTypeDeclaration) ProcessType((CodeTypeDeclaration)member);
                else if(member is CodeConstructor) {
                    CodeConstructor ctor = (CodeConstructor)member;
                    ctor.Statements.Add( new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "OnAfterInit", new CodeExpression[] { })));
                }
            }
        }
    }


}
