using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Tools.VisualStudioT.GeneratorsT.XsdGenerator;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>
    /// To all properties adds call to <c>On{0}Changing</c> method.
    /// Adds corresponding virtual <c>On{0}Changing</c> methods raising <c>{0}ChangingEvents</c> and adds the events.
    /// Also implements the <see cref="INotifyPropertyChanging"/> interface.
    /// </summary>
    /// <remarks>
    /// This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.
    /// </remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.OnChangingCall?>]]></code></example>
    /// <version version="1.5.3">Added implemetation for <see cref="INotifyPropertyChanging"/></version>
    /// <version version="1.5.3">Fiexd type of event arguments (changed from <c>EOS.PropertyChangingEventArgs</c> ro <see cref="T:Tools.ComponentModelT.PropertyChangingEventArgsEx"/>.</version>
    public class OnChangingCall : ICodeExtension {
        /// <summary>Initializes the extension (this implementation does nothing)</summary>
        /// <param name="parameters">Initialization parameters (ignored)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        void IExtensionBase.Initialize (System.Collections.Generic.IDictionary<string, string> parameters) { }
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider provider) {
            foreach (CodeTypeDeclaration type in code.Types) {
                if (type.IsClass || type.IsStruct) {
                    bool iNotifyPropertyChangingRequired = false;
                    List<CodeTypeMember> addMembers = new List<CodeTypeMember>();
                    foreach (CodeTypeMember member in type.Members) {
                        if (member is CodeMemberProperty) {
                            CodeMemberProperty prp = (CodeMemberProperty)member;
                            if (prp.HasSet) {
                                CodeTypeReference argType = new CodeTypeReference("Tools.ComponentModelT.PropertyChangingEventArgsEx") {
                                    TypeArguments = { prp.Type }
                                };
                                prp.SetStatements.Insert(0,
                                    new CodeExpressionStatement(
                                        new CodeMethodInvokeExpression(
                                            new CodeThisReferenceExpression(), string.Format("On{0}Changing", prp.Name),
                                                new CodeExpression[]{ new CodeObjectCreateExpression(argType, new CodeExpression[]{  
                                                    new CodePrimitiveExpression(prp.Name),
                                                    new CodePropertySetValueReferenceExpression()
                                                })}
                                    )));
                                CodeMemberMethod on = new CodeMemberMethod();
                                on.Name = string.Format("On{0}Changing", prp.Name);
                                on.Attributes = MemberAttributes.Family;
                                if (prp.Attributes.HasFlag(MemberAttributes.Static)) on.Attributes |= MemberAttributes.Static;
                                else iNotifyPropertyChangingRequired = true;
                                on.Parameters.Add(new CodeParameterDeclarationExpression(argType, "e"));
                                on.Comments.Add(new CodeCommentStatement(string.Format(Resources.Summary_OnChanging, prp.Name), true));
                                CodeTypeReference evType = new CodeTypeReference("System.EventHandler");
                                evType.TypeArguments.Add(argType);
                                on.Statements.Add(new CodeDelegateInvokeExpression(new CodeEventReferenceExpression(new CodeThisReferenceExpression(),
                                    string.Format("{0}Changing", prp.Name)), new CodeExpression[] { 
                                        new CodeThisReferenceExpression(), 
                                        new CodeArgumentReferenceExpression("e") 
                                    }));
                                on.Statements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "OnPropertyChanging", new CodeArgumentReferenceExpression("e")));
                                addMembers.Add(on);
                                CodeMemberEvent ev = new CodeMemberEvent() { Attributes = MemberAttributes.Public };
                                ev.Name = string.Format("{0}Changing", prp.Name);
                                ev.Type = evType;
                                ev.Comments.Add(new CodeCommentStatement(string.Format(Resources.Summary_Changing, prp.Name), true));
                                if (prp.Attributes.HasFlag(MemberAttributes.Static)) ev.Attributes |= MemberAttributes.Static;
                                addMembers.Add(ev);
                            }
                        }
                    }
                    if (iNotifyPropertyChangingRequired) {
                        type.BaseTypes.Add(typeof(INotifyPropertyChanging));
                        addMembers.Add(new CodeMemberEvent() {
                            Attributes = MemberAttributes.Public,
                            Comments = { new CodeCommentStatement(Resources.Summary_PropertyChanging, true) },
                            ImplementationTypes = { new CodeTypeReference(typeof(INotifyPropertyChanging)) },
                            Name = "PropertyChanging",
                            Type = new CodeTypeReference(typeof(PropertyChangingEventHandler))
                        });
                        addMembers.Add(new CodeMemberMethod() {
                            Name = "OnPropertyChanging",
                            Attributes = MemberAttributes.Family,
                            Comments = { 
                                new CodeCommentStatement(Resources.Summary_OnPropertyChanging,true),
                                new CodeCommentStatement(Resources.Param_OnPropertyChanging_e,true)
                            },
                            Parameters = { new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(PropertyChangingEventArgs)), "e") },
                            Statements = {
                                new CodeDelegateInvokeExpression(new CodeEventReferenceExpression(new CodeThisReferenceExpression() , 
                                    "PropertyChanging"), new CodeThisReferenceExpression(),new CodeArgumentReferenceExpression("e"))
                            }
                        });
                    }
                    foreach (CodeTypeMember m in addMembers) {
                        type.Members.Add(m);
                    }
                }
            }
        }
    }
}
