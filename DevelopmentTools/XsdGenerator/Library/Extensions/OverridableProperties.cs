using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions
{
	/// <summary>
	/// Converts all properties to virtual ones
	/// </summary>
	public class OverridableProperties : ICodeExtension
	{
        void ICodeExtension.Initialize(System.Collections.Generic.IDictionary<string, string> Parameters) { }	
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider Provider)
		{
            foreach(CodeTypeDeclaration type in code.Types)
			{
				if ( type.IsClass || type.IsStruct )
				{
					foreach ( CodeTypeMember member in type.Members )
					{
						if(member is CodeMemberProperty) {
                            CodeMemberProperty prp = (CodeMemberProperty)member;
                            prp.Attributes = (prp.Attributes & ~MemberAttributes.ScopeMask); 
                        }
					}
				}
			}
		}

	}
}
