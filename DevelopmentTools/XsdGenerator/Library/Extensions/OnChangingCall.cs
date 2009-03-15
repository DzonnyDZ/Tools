using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions
{
	/// <summary>To all properties adds call to On{0}Changing method, adds corresponding virtual On{0}Changing methods raising {0}ChangingEvents and adds the events</summary>
	public class OnChangingCall : ICodeExtension{
        void ICodeExtension.Initialize(System.Collections.Generic.IDictionary<string, string> Parameters) { }		
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider Provider){
            foreach(CodeTypeDeclaration type in code.Types) {
                if(type.IsClass || type.IsStruct) {
                    List<CodeTypeMember> addMembers = new List<CodeTypeMember>();
                    foreach(CodeTypeMember member in type.Members) {
                        if(member is CodeMemberProperty) {
                            CodeMemberProperty prp = (CodeMemberProperty)member;
                            if(prp.HasSet) {
                                CodeTypeReference argType = new CodeTypeReference("EOS.PropertyChangingEventArgs");
                                argType.TypeArguments.Add(prp.Type);
                                prp.SetStatements.Insert(0,
                                    new CodeExpressionStatement(
                                        new CodeMethodInvokeExpression(
                                            new CodeThisReferenceExpression(), string.Format("On{0}Changing", prp.Name),
                                                new CodeExpression[]{ new CodeObjectCreateExpression(argType, new CodeExpression[]{  
                                                    new CodePropertySetValueReferenceExpression()})})));
                                CodeMemberMethod on = new CodeMemberMethod();
                                on.Name = string.Format("On{0}Changing", prp.Name);
                                on.Attributes = MemberAttributes.Family;
                                on.Parameters.Add(new CodeParameterDeclarationExpression(argType, "e"));
                                on.Comments.Add(new CodeCommentStatement(string.Format("<summary>Raises the <see cref='{0}Changing'/> event</summary>\r\n<param name='e'>Event erguments</param>", prp.Name),true));
                                CodeTypeReference evType = new CodeTypeReference("System.EventHandler");
                                evType.TypeArguments.Add(argType);
                                on.Statements.Add(new CodeDelegateInvokeExpression(new CodeEventReferenceExpression(new CodeThisReferenceExpression(), string.Format("{0}Changing", prp.Name)), new CodeExpression[] { new CodeThisReferenceExpression(), new CodeArgumentReferenceExpression("e") }));
                                addMembers.Add(on);
                                CodeMemberEvent ev = new CodeMemberEvent();
                                ev.Name = string.Format("{0}Changing", prp.Name);
                                ev.Type = evType;
                                ev.Comments.Add(new CodeCommentStatement(string.Format("<summary>Raises when value of property {0} is about to change.</summary>\r\n<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>", prp.Name), true));
                                addMembers.Add(ev);
                            }
                        }
                    }
                    foreach(CodeTypeMember m in addMembers) {
                        type.Members.Add(m);
                    }
                }
            }
		}

	}
}
