using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;
using System.Collections.Generic;

///<summary>This extension removed all attributes of given type from all types</summary>
///<summary>Attributes are combated only by <see cref="CodeAttributeDeclaration.AttributeType">.<see cref="CodeTypeReference.BaseType">BaseType</see>
namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions{
	/// <summary>Removes attribute from all types</summary>
    public class RemoveTypeAttribute:ICodeExtension {
        private string AttributeName;
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider Provider) {
            foreach(CodeTypeDeclaration Type in code.Types){
                ProcessType(Type);
            }
		}
    public void Initialize(System.Collections.Generic.IDictionary<string, string> Parameters) {
        AttributeName = Parameters["type"]; 
            }
        /// <summary>Removes attribute from type and its nested types</summary>
        private void ProcessType(CodeTypeDeclaration Type) {
            List<CodeAttributeDeclaration> toRemove  = new List<CodeAttributeDeclaration>();
            foreach(CodeAttributeDeclaration attr in Type.CustomAttributes) {
                if(attr.AttributeType.BaseType == AttributeName) {
                    toRemove.Add(attr);
                    }
                }
            foreach(CodeAttributeDeclaration attr in toRemove) {
                Type.CustomAttributes.Remove(attr);
                }
            foreach(CodeTypeMember member in Type.Members) {
                if(member is CodeTypeDeclaration) ProcessType((CodeTypeDeclaration)member);
                }
            }
	}
}
