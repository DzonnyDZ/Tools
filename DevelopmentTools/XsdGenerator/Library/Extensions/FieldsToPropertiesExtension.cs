using System;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions
{
	/// <summary>
	/// Converts the default public fields into properties backed by a private field.
	/// </summary>
	public class FieldsToPropertiesExtension : ICodeExtension
	{
		#region ICodeExtension Members

        public void Process(System.CodeDom.CodeNamespace code, System.Xml.Schema.XmlSchema schema, CodeDomProvider Provider)
		{
			foreach ( CodeTypeDeclaration type in code.Types )
			{
				if ( type.IsClass || type.IsStruct )
				{
					// Copy the colletion to an array for safety. We will be 
					// changing this collection.
					CodeTypeMember[] members = new CodeTypeMember[type.Members.Count];
					type.Members.CopyTo( members, 0 );

					foreach ( CodeTypeMember member in members )
					{
						// Process fields only.
						if ( member is CodeMemberField )
						{
							CodeMemberProperty prop = new CodeMemberProperty();
							prop.Name = member.Name;

							prop.Attributes = member.Attributes;
							prop.Type = ( ( CodeMemberField )member ).Type;

							// Copy attributes from field to the property.
							prop.CustomAttributes.AddRange( member.CustomAttributes );
							member.CustomAttributes.Clear();

							// Copy comments from field to the property.
							prop.Comments.AddRange( member.Comments );
							member.Comments.Clear();

							// Modify the field.
							member.Attributes = MemberAttributes.Private;
							Char[] letters = member.Name.ToCharArray();
							letters[0] = Char.ToLower(letters[0]);
							member.Name = String.Concat( "_", new string(letters ) );

							prop.HasGet = true;
							prop.HasSet = true;

							// Add get/set statements pointing to field. Generates:
							// return this._fieldname;
							prop.GetStatements.Add( 
								new CodeMethodReturnStatement( 
								new CodeFieldReferenceExpression( new CodeThisReferenceExpression(), 
								member.Name ) ) );
							// Generates:
							// this._fieldname = value;
							prop.SetStatements.Add(
								new CodeAssignStatement(
								new CodeFieldReferenceExpression( new CodeThisReferenceExpression(),
								member.Name ), 
								new CodeArgumentReferenceExpression( "value" ) ) );

							// Finally add the property to the type
							type.Members.Add( prop );
						}
					}
				}
			}

		}
			#endregion Turn Public Fields into Properties
        void ICodeExtension.Initialize(System.Collections.Generic.IDictionary<string, string> Parameters) { }
	}
}
