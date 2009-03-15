using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions
{
	/// <summary>
	/// Converts array-based properties into List`1-based ones
	/// </summary>
	public class ArraysToGenericCollectionsExtension : ICodeExtension
	{
		#region ICodeExtension Members

        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider Provider)
		{
			// Copy as we will be adding types.
			CodeTypeDeclaration[] types = 
				new CodeTypeDeclaration[code.Types.Count];
			code.Types.CopyTo( types, 0 );

			foreach ( CodeTypeDeclaration type in types )
			{
				if ( type.IsClass || type.IsStruct )
				{
					foreach ( CodeTypeMember member in type.Members )
					{
						if ( member is CodeMemberField && ( ( CodeMemberField )member ).Type.ArrayElementType != null ){
							CodeMemberField field = ( CodeMemberField ) member;
							// Change field type to collection.
                            field.Type = GetCollection(field.Type.ArrayElementType);
                            field.InitExpression = new CodeObjectCreateExpression(field.Type);
                            if(field.Name.EndsWith("Field")) 
                                field.Comments.Add(new CodeCommentStatement(string.Format("<summary>Contains value of the <see cref='{0}'/> property</summary>", field.Name.Substring(0, field.Name.Length - "field".Length)), true));
                       } else if(member is CodeMemberProperty && ((CodeMemberProperty)member).Type.ArrayElementType != null) {
                                CodeMemberProperty Property = (CodeMemberProperty)member;
                                Property.Type = GetCollection(Property.Type.ArrayElementType);
                                Property.HasSet = false;
                            }
					}
				}
			}
		}

		#endregion
        void ICodeExtension.Initialize(System.Collections.Generic.IDictionary<string, string> Parameters) { }
		public CodeTypeReference GetCollection(CodeTypeReference forType )
		{
			CodeTypeReference col = new CodeTypeReference(typeof(System.Collections.Generic.List<>));
            col.TypeArguments.Add(forType);
			return col;
		}
	}
}
