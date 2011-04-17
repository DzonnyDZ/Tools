using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>Renames given property of type string and adds property of type <see cref="DateTime"/>? backed by original property holding either an empty string or <see cref="DateTime"/> in XSD format</summary>
    /// <remarks>
    /// This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.
    /// </remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension "Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.DateNullableByEmptyString" PropertyName="..." TypeName="..." Kind=".." Empty="..."?>]]></code></example>
    /// <version version="1.5.3">This class was re-introduced in version 1.5.3</version>
    public class DateNullableByEmptyString : ICodeExtension {
        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters: Thic class expects following parameters:
        /// <list type="table"><listheader><term>Parameter</term><description>Description</description></listheader>
        /// <item><term><c>PropertyName</c></term><description>Name of property to be altered. Required.</description></item>
        /// <item><term><c>TypeName</c></term><description>Name of type property <c>PropertyName</c> is property of. Required.</description></item>
        /// <item><term><c>Kind</c></term><description>Kind of date time value. Either <c>Date</c> (date only) or <c>DateTime</c> (date including time part). Optional. Default <c>DateTime</c>.</description></item>
        /// <item><term><c>Empty</c></term><description>How to deal with empty values. <c>Empty</c> - Empty string, <c>Null</c> - null. Optional. Not specified - null values are not treated in a a special way.</description></item>
        /// </list></param>
        /// <exception cref="KeyNotFoundException">A required parameter is not present in the <paramref name="parameters"/> dictionary.</exception>
        /// <exception cref="ArgumentException">
        /// Key <c>Kind</c> is present in <paramref name="parameters"/> bot value is neither <c>Date</c> nor <c>DateTime</c>. -or-
        /// Key <c>Empty</c> is present in <paramref name="parameters"/> bot value is neither <c>Empty</c> nor <c>Null</c>.
        /// </exception>
        public void Initialize(System.Collections.Generic.IDictionary<string, string> parameters) {
            propertyName = parameters["PropertyName"];
            typeName = parameters["TypeName"];
            if (parameters.ContainsKey("Kind"))
                switch (parameters["Kind"]) {
                    case "Date": dateOnly = true; break;
                    case "DateTime": dateOnly = false; break;
                    default: throw new ArgumentException(string.Format(Resources.ex_UknnownDateSerializationKind));
                }
            if (parameters.ContainsKey("Empty"))
                switch (parameters["Empty"]) {
                    case "Empty": empty = true; break;
                    case "Null": empty = false; break;
                    default: throw new ArgumentException(string.Format(Resources.ex_UknnownNullSerializationOption));
                }
        }
        private string typeName;
        private string propertyName;
        //private string attributeName;
        private bool dateOnly = false;
        private bool? empty = false;
        //private List<CodeAttributeArgument> attrParams = new List<CodeAttributeArgument>();
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
         /// <exception cref="InvalidOperationException">Type provided in the <c>TypeName</c> parameter or property provided in <c>PropertyName</c> parameter was not found.</exception>
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider Provider) {
            CodeTypeDeclaration type;
            try {
                type = ExtenstionHelpers.FindType(typeName, code);
            } catch (ArgumentException ex) {
                throw new InvalidOperationException(ex.Message, ex);
            }
            CodeMemberProperty property = null;
            foreach (CodeTypeMember m in type.Members) {
                if (m is CodeMemberProperty && m.Name == propertyName) {
                    property = (CodeMemberProperty)m;
                    break;
                }
            }
            if (property == null) throw new InvalidOperationException(string.Format(Resources.ex_PropertyNotFound, propertyName));

            //New property name unsafe_...
            string oldName = property.Name;
            property.Name = "unsafe_" + oldName;
            //It must be xml-serialized under old name
            bool found = false;
            foreach (CodeAttributeDeclaration attr in property.CustomAttributes) {
                if (attr.AttributeType.BaseType == typeof(XmlElementAttribute).FullName) {
                    foreach (CodeAttributeArgument aa in attr.Arguments) {
                        if (aa.Name == null && aa.Value is CodePrimitiveExpression && ((CodePrimitiveExpression)aa.Value).Value is string) found = true; break;
                    }
                    if (!found)
                        attr.Arguments.Add(new CodeAttributeArgument("ElementName", new CodePrimitiveExpression(oldName)));
                    found = true;
                } else if (attr.AttributeType.BaseType == typeof(XmlAnyAttributeAttribute).FullName) {
                    foreach (CodeAttributeArgument aa in attr.Arguments) {
                        if (aa.Name == null && aa.Value is CodePrimitiveExpression && ((CodePrimitiveExpression)aa.Value).Value is string) found = true; break;
                    }
                    if (!found)
                        attr.Arguments.Add(new CodeAttributeArgument("AttributeName", new CodePrimitiveExpression(oldName)));
                    found = true;
                    found = true;
                }
            }
            //Hide it form user and developper
            if (!found) {
                property.CustomAttributes.Add(
                    new CodeAttributeDeclaration(new CodeTypeReference(typeof(XmlElementAttribute)),
                        new CodeAttributeArgument[] { new CodeAttributeArgument(new CodePrimitiveExpression(oldName)) }));
            }
            property.CustomAttributes.Add(
                new CodeAttributeDeclaration(new CodeTypeReference(typeof(BrowsableAttribute)),
                    new CodeAttributeArgument[] { new CodeAttributeArgument(new CodePrimitiveExpression(false)) }));
            property.CustomAttributes.Add(
                new CodeAttributeDeclaration(new CodeTypeReference(typeof(EditorBrowsableAttribute)),
                    new CodeAttributeArgument[] { new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(EditorBrowsableState)), "Never")) }));
            //Alter getter
            if (empty.HasValue) {
                List<CodeStatement> getStatements = new List<CodeStatement>(property.GetStatements.Count);
                foreach (CodeStatement statement in property.GetStatements)
                    if (statement is CodeMethodReturnStatement) {
                        getStatements.Add(new CodeAssignStatement(
                            new CodeVariableReferenceExpression("property_return_value"),
                            ((CodeMethodReturnStatement)statement).Expression) {
                                LinePragma = statement.LinePragma
                            });
                        getStatements[getStatements.Count - 1].StartDirectives.AddRange(statement.StartDirectives);
                        getStatements[getStatements.Count - 1].EndDirectives.AddRange(statement.EndDirectives);
                        foreach (DictionaryEntry ud in statement.UserData) getStatements[getStatements.Count - 1].UserData.Add(ud.Key, ud.Value);
                    } else getStatements.Add(statement);
                property.GetStatements.Clear();
                property.GetStatements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference(typeof(string)), "property_return_value"));
                property.GetStatements.AddRange(getStatements.ToArray());
                property.GetStatements.Add(new CodeConditionStatement(
                    new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(string)), "IsNullOrEmpty"),
                        new CodeExpression[] { new CodeVariableReferenceExpression("property_return_value") }),
                    new CodeStatement[] { new CodeMethodReturnStatement(new CodePrimitiveExpression(empty.Value ? string.Empty : null)) },
                    new CodeStatement[] { new CodeMethodReturnStatement(new CodeVariableReferenceExpression("property_return_value")) }));
            }
            //Add new property
            CodeMemberProperty NewProperty = new CodeMemberProperty();
            NewProperty.Name = oldName;
            NewProperty.Type = new CodeTypeReference(typeof(DateTime?));
            NewProperty.Attributes = property.Attributes;
            NewProperty.CustomAttributes.Add(
                new CodeAttributeDeclaration(new CodeTypeReference(typeof(XmlIgnoreAttribute))));
            NewProperty.CustomAttributes.Add(
                new CodeAttributeDeclaration(new CodeTypeReference(typeof(DesignerSerializationVisibilityAttribute)), new CodeAttributeArgument[]{
                    new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(DesignerSerializationVisibility)),"Hidden"))}));
            NewProperty.GetStatements.Add(new CodeConditionStatement(
                new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(string)), "IsNullOrEmpty", new CodeExpression[]{
                    new CodePropertyReferenceExpression(
                        (property.Attributes & MemberAttributes.Static) == MemberAttributes.Static ? (CodeExpression)new CodeTypeReferenceExpression(type.Name) : (CodeExpression)new CodeThisReferenceExpression(),
                        property.Name)}),
                /*True*/ new CodeStatement[]{
                    new CodeMethodReturnStatement(new CodeDefaultValueExpression(new CodeTypeReference(typeof(DateTime?))))},
                /*False*/ new CodeStatement[]{
                    new CodeMethodReturnStatement(new CodeCastExpression(new CodeTypeReference(typeof(DateTime?)), new CodeCastExpression(typeof(DateTime),
                                new CodeObjectCreateExpression(new CodeTypeReference(typeof(XAttribute)), new CodeExpression[]{
                                    new CodePrimitiveExpression("x"),
                                    new CodePropertyReferenceExpression(
                                        (property.Attributes & MemberAttributes.Static) == MemberAttributes.Static ? (CodeExpression)new CodeTypeReferenceExpression(type.Name) : (CodeExpression)new CodeThisReferenceExpression(),
                                        property.Name)
                                }
                         ))))}
            ));
            NewProperty.SetStatements.Add(new CodeConditionStatement(
                new CodePropertyReferenceExpression(new CodePropertySetValueReferenceExpression(), "HasValue"),
                /*True*/ new CodeStatement[] {
                    new CodeAssignStatement(new CodePropertyReferenceExpression(
                            (property.Attributes & MemberAttributes.Static) == MemberAttributes.Static ? (CodeExpression)new CodeTypeReferenceExpression(type.Name) : (CodeExpression)new CodeThisReferenceExpression(),
                            property.Name),
                        new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(new CodePropertySetValueReferenceExpression(),"Value"),"ToString"),new CodeExpression[]{
                            new CodePrimitiveExpression(dateOnly ? "yyyy-MM-dd" : "yyyy-MM-ddTHH:mm:ss"), 
                            new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(System.Globalization.CultureInfo)),"InvariantCulture")
                        })
                )},
                /*False*/ new CodeStatement[] {
                    new CodeAssignStatement(new CodePropertyReferenceExpression(
                            (property.Attributes & MemberAttributes.Static) == MemberAttributes.Static ? (CodeExpression)new CodeTypeReferenceExpression(type.Name) : (CodeExpression)new CodeThisReferenceExpression(),
                            property.Name),
                        new CodePrimitiveExpression(null) 
                )}
            ));
            type.Members.Insert(type.Members.IndexOf(property) + 1, NewProperty);

        }

    }
}
