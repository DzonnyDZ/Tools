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
    /// <summary>Makes property backed by string property with given format</summary>
    /// <remarks>Use only for <see cref="IFormattable"/> with Parse static function!</remarks>
    /// <version version="1.5.3">This class was re-introduced in version 1.5.3</version>
    public class StringBackedProperty : ICodeExtension {
        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters: This class expectes following parameters:
        /// <list type="table"><listheader><term>Parameter</term><description>Description</description></listheader>
        /// <item><term><c>PropertyName</c></term><description>Name of property to be changed. Required.</description></item>
        /// <item><term><c>TypeName</c></term><description>Name of type of property. Required.</description></item>
        /// <item><term><c>Format</c></term><description>Formatting string for backing string property. Required.</description></item>
        /// </list></param>
        /// <exception cref="KeyNotFoundException">A required parameter is not present in <paramref name="parameters"/>.</exception>
        public void Initialize(System.Collections.Generic.IDictionary<string, string> parameters) {
            propertyName = parameters["PropertyName"];
            typeName = parameters["TypeName"];
            format = parameters["Format"];
        }
        private string format;
        private string typeName;
        private string propertyName;
        private bool? empty = false;
        //private List<CodeAttributeArgument> attrParams = new List<CodeAttributeArgument>();

        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <exception cref="InvalidOperationException">Type provided in the <c>TypeName</c> parameter or property provided in <c>PropertyName</c> parameter was not found.</exception>
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider provider) {
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
            if (property == null) throw new InvalidOperationException(string.Format(Tools.VisualStudioT.GeneratorsT.XsdGenerator.Resources.ex_PropertyNotFound, propertyName));
            //Get XML-serialization-related attributes
            List<CodeAttributeDeclaration> attrs = new List<CodeAttributeDeclaration>();
            foreach (CodeAttributeDeclaration custAttr in property.CustomAttributes) {
                if (custAttr.AttributeType.BaseType.StartsWith("System.Xml.Serialization.Xml"))
                    attrs.Add(custAttr);
            }
            //Remove it from original property
            foreach (CodeAttributeDeclaration removeIt in attrs)
                property.CustomAttributes.Remove(removeIt);
            //Make it XML-ignored
            property.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(XmlIgnoreAttribute))));
            //Create backing property
            CodeMemberProperty newProperty = new CodeMemberProperty();
            newProperty.Name = property.Name + "_AsString";
            newProperty.Attributes = property.Attributes;
            newProperty.Type = new CodeTypeReference(typeof(string));
            //Getter
            newProperty.GetStatements.AddRange(new CodeStatement[]{
                new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(
                    new CodeCastExpression(
                        new CodeTypeReference(typeof(IFormattable)),
                        new CodePropertyReferenceExpression(
                            (property.Attributes & MemberAttributes.Static)== MemberAttributes.Static ? (CodeExpression)new CodeTypeReferenceExpression(type.Name) : (CodeExpression)new CodeThisReferenceExpression (),
                            property.Name)),"ToString"),new CodeExpression[]{
                    new CodePrimitiveExpression(format),
                    new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(System.Globalization.CultureInfo)),"InvariantCulture")
                }))
            });
            //Setter
            newProperty.SetStatements.AddRange(new CodeStatement[]{
                new CodeAssignStatement(
                    new CodePropertyReferenceExpression(
                            (property.Attributes & MemberAttributes.Static)== MemberAttributes.Static ? (CodeExpression)new CodeTypeReferenceExpression(type.Name) : (CodeExpression)new CodeThisReferenceExpression (),
                            property.Name),
                    new CodeMethodInvokeExpression(
                        new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(property.Type),"Parse"),
                        new CodeExpression[]{
                            new CodePropertySetValueReferenceExpression(),
                            new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(System.Globalization.CultureInfo)),"InvariantCulture")
                        }
                    )
                )
            });
            //Check XML attributes (if name is not set it must be set to name of original property)
            CodeAttributeDeclaration attribute = null;
            foreach (var attr in attrs)
                if (attr.AttributeType.BaseType == typeof(XmlElementAttribute).FullName) {
                    attribute = attr;
                    break;
                } else if (attr.AttributeType.BaseType == typeof(XmlAttributeAttribute).FullName) {
                    attribute = attr;
                    break;
                }
            if (attribute == null)
                newProperty.CustomAttributes.Add(new CodeAttributeDeclaration(
                    new CodeTypeReference(typeof(XmlElementAttribute)),
                    new CodeAttributeArgument(new CodePrimitiveExpression(property.Name))
                ));
            else {
                int i = 0;
                bool NameSet = false;
                foreach (CodeAttributeArgument arg in attribute.Arguments) {
                    if (string.IsNullOrEmpty(arg.Name)) {
                        if (i == 0 && arg.Value is CodePrimitiveExpression && ((CodePrimitiveExpression)arg.Value).Value is string) {
                            NameSet = true;
                            break;
                        }
                        i++;
                    } else if (arg.Name == (attribute.AttributeType.BaseType == typeof(XmlAttributeAttribute).FullName ? "AttributeName" : "ElementName")) {
                        NameSet = true;
                        break;
                    }
                }
                if (!NameSet) {
                    attribute.Arguments.Add(new CodeAttributeArgument(
                        attribute.AttributeType.BaseType == typeof(XmlAttributeAttribute).FullName ? "AttributeName" : "ElementName",
                        new CodePrimitiveExpression(property.Name)
                    ));
                }
            }
            newProperty.CustomAttributes.AddRange(attrs.ToArray());
            newProperty.CustomAttributes.AddRange(new CodeAttributeDeclaration[]{
                new CodeAttributeDeclaration(
                    new CodeTypeReference(typeof(BrowsableAttribute)),
                    new CodeAttributeArgument[]{new CodeAttributeArgument(new CodePrimitiveExpression(false))}
                ),
                new CodeAttributeDeclaration(
                    new CodeTypeReference(typeof(EditorBrowsableAttribute)),
                    new CodeAttributeArgument[]{new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(EditorBrowsableState)),"Never"))}
               ),
               new CodeAttributeDeclaration(
                   new CodeTypeReference(typeof(DesignerSerializationVisibility)),
                   new CodeAttributeArgument[]{new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(DesignerSerializationVisibility)),"Hidden"))} 
               )
            });
            type.Members.Insert(type.Members.IndexOf(property) + 1, newProperty);
        }

    }
}
