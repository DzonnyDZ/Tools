using System;
using System.CodeDom;
using System.Collections.Generic;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions
{
	/// <summary>
	/// Enum items which's values are numeric will get same numeric values for enum members
	/// </summary>
	public class NumericEnums : ICodeExtension{
        void ICodeExtension.Initialize(System.Collections.Generic.IDictionary<string, string> Parameters) { }
        public void Process(System.CodeDom.CodeNamespace code, System.Xml.Schema.XmlSchema schema, CodeDomProvider Provider)
		{
			foreach ( CodeTypeDeclaration type in code.Types ){
                ParseType(type);
			}
		}
        private void ParseType(CodeTypeDeclaration  type) {
            if (type.IsEnum) ParseEnum(type);
            else
                foreach (CodeTypeMember member in type.Members)
                    if (member is CodeTypeDeclaration)
                        ParseType((CodeTypeDeclaration)member);
        }
        private void ParseEnum(CodeTypeDeclaration type) {
            foreach(CodeTypeMember member in type.Members)
                if (member is CodeMemberField) {
                    CodeMemberField field = (CodeMemberField)member;
                    if (/*(field.Attributes & MemberAttributes.Static) == MemberAttributes.Static && (field.Attributes & MemberAttributes.Const) == MemberAttributes.Const &&*/
                        field.CustomAttributes != null && field.InitExpression==null) {
                        foreach (CodeAttributeDeclaration attr in field.CustomAttributes) {
                            int attrval;
                            if (attr.AttributeType.BaseType == "System.Xml.Serialization.XmlEnumAttribute" && attr.Arguments[0].Value is CodePrimitiveExpression && ((CodePrimitiveExpression)attr.Arguments[0].Value).Value is string && int.TryParse((string)((CodePrimitiveExpression)attr.Arguments[0].Value).Value, out attrval))
                                field.InitExpression = new CodePrimitiveExpression(attrval);
                        }
                    }
                }
        }
	}
}
