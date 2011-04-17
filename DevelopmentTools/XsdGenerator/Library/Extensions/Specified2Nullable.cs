using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>Turns Property-PropertySpecified pairs to nullable properties</summary>
    /// <remarks>
    /// This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.
    /// </remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension "Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.Specified2Nullable" Type="..."?>]]></code></example>
    /// <version version="1.5.3">This class was re-introduced in version 1.5.3</version>
    public class Specified2Nullable : ICodeExtension {
        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters: Expects one optional parameter <c>Type</c>. If specified processes only properties of type with given name.</param>
        public void Initialize(System.Collections.Generic.IDictionary<string, string> parameters) {
            if (parameters.ContainsKey("Type")) typeName = parameters["Type"];
        }
        private string typeName;
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <exception cref="InvalidOperationException">Type provided in the <c>Type</c> parameter was not found.</exception>
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider provider) {
            if (typeName != null) {
                CodeTypeDeclaration extenstionHelpersFindType;
                try {
                    extenstionHelpersFindType = ExtenstionHelpers.FindType(typeName, code);
                } catch (ArgumentException ex) {
                    throw new InvalidOperationException(ex.Message, ex);
                }
                processType(extenstionHelpersFindType);
            } else
                foreach (CodeTypeDeclaration t in code.Types)
                    processType(t);

        }
        private void processType(CodeTypeDeclaration type) {
            List<CodeMemberProperty> properties = new List<CodeMemberProperty>();
            foreach (CodeTypeMember m in type.Members)
                if (m is CodeMemberProperty && m.Name.EndsWith("Specified"))
                    properties.Add((CodeMemberProperty)m);
            foreach (var prp in properties) processProperty(prp, type);

            if (type == null)
                foreach (CodeTypeMember m in type.Members)
                    if (m is CodeTypeDeclaration)
                        processType((CodeTypeDeclaration)m);
        }
        private void processProperty(CodeMemberProperty propertySpecified, CodeTypeDeclaration type) {
            CodeMemberProperty property = null;
            foreach (CodeTypeMember m in type.Members)
                if (m is CodeMemberProperty && propertySpecified.Name == m.Name + "Specified") {
                    property = (CodeMemberProperty)m;
                    break;
                }
            if (property == null) return;
            string nnname = property.Name + "_NonNullable";
            //Rename specified property
            propertySpecified.Name = nnname + "Specified";
            //Create Nullable property
            CodeMemberProperty nullableProperty = new CodeMemberProperty();
            nullableProperty.Name = property.Name;
            nullableProperty.Type = new CodeTypeReference(typeof(Nullable<>));
            nullableProperty.Type.TypeArguments.Add(property.Type);
            nullableProperty.Attributes = property.Attributes;
            nullableProperty.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(XmlIgnoreAttribute))));//XmlSerialization-hidden
            nullableProperty.GetStatements.Add(//Get
                new CodeConditionStatement(
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), propertySpecified.Name),
                    new CodeStatement[]{
                        new CodeMethodReturnStatement(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(),nnname))
                    },
                    new CodeStatement[]{
                        new CodeMethodReturnStatement(new CodeDefaultValueExpression(nullableProperty.Type))
                    }
            ));
            nullableProperty.SetStatements.Add(//Set
                new CodeConditionStatement(
                    new CodePropertyReferenceExpression(new CodePropertySetValueReferenceExpression(), "HasValue"),
                    new CodeStatement[]{
                        new CodeAssignStatement(
                            new CodePropertyReferenceExpression(new CodeThisReferenceExpression(),nnname),
                            new CodePropertyReferenceExpression(new CodePropertySetValueReferenceExpression(),"Value")),
                        new CodeAssignStatement(
                            new CodePropertyReferenceExpression(new CodeThisReferenceExpression(),propertySpecified.Name),
                            new CodePrimitiveExpression(true))
                    },
                    new CodeStatement[]{
                        new CodeAssignStatement(
                            new CodePropertyReferenceExpression(new CodeThisReferenceExpression(),nnname),
                            new CodeDefaultValueExpression(property.Type)),
                        new CodeAssignStatement(
                            new CodePropertyReferenceExpression(new CodeThisReferenceExpression(),propertySpecified.Name),
                            new CodePrimitiveExpression(false))
            }));
            type.Members.Add(nullableProperty);
            //Hide Specified property
            CodeAttributeDeclaration editorBrowsable = new CodeAttributeDeclaration(new CodeTypeReference(typeof(EditorBrowsableAttribute)),
                            new CodeAttributeArgument[] { new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(EditorBrowsableState)), "Never")) });
            propertySpecified.CustomAttributes.Add(editorBrowsable);
            CodeAttributeDeclaration browsable = new CodeAttributeDeclaration(new CodeTypeReference(typeof(BrowsableAttribute)),
                            new CodeAttributeArgument[] { new CodeAttributeArgument(new CodePrimitiveExpression(false)) });
            propertySpecified.CustomAttributes.Add(browsable);
            //Ensure that original property is Xml-Serialized under original name
            bool needSpecifyName = true;
            foreach (CodeAttributeDeclaration attr in property.CustomAttributes)
                if (attr.AttributeType.BaseType == typeof(XmlElementAttribute).FullName || attr.AttributeType.BaseType == typeof(XmlAttributeAttribute).FullName) {
                    int i = 0;
                    bool nameSpecified = false;
                    foreach (CodeAttributeArgument arg in attr.Arguments) {
                        if ((string.IsNullOrEmpty(arg.Name) && i == 0) || (arg.Name == "ElementName" || arg.Name == "AttributeName")) {
                            nameSpecified = true;
                            break;
                        }
                        if (string.IsNullOrEmpty(arg.Name)) i++;
                    }
                    if (!nameSpecified && attr.AttributeType.BaseType == typeof(XmlElementAttribute).FullName)
                        attr.Arguments.Add(new CodeAttributeArgument("ElementName", new CodePrimitiveExpression(property.Name)));
                    else if (!nameSpecified && attr.AttributeType.BaseType == typeof(XmlAttributeAttribute).FullName)
                        attr.Arguments.Add(new CodeAttributeArgument("AttributeName", new CodePrimitiveExpression(property.Name)));
                    needSpecifyName = false;
                }
            if (needSpecifyName)
                property.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(XmlElementAttribute)),
                    new CodeAttributeArgument[] { new CodeAttributeArgument(new CodePrimitiveExpression(property.Name)) }));
            //Hide original property
            property.Name = nnname;
            property.CustomAttributes.Add(editorBrowsable);
            property.CustomAttributes.Add(browsable);
        }

    }
}
