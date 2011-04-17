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
    /// <summary>Sets if empty string is serialized as empty element/attribute or it is not serialized at all.</summary>
    /// <remarks>
    /// This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.
    /// </remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension "Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.EmptyStringMode" PropertyName="..." TypeName="..." Empty="..."?>]]></code></example>
    /// <version version="1.5.3">This class was re-introduced in version 1.5.3</version>
    public class EmptyStringMode : ICodeExtension {

        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters: This class expects following parameters:
        /// <list type="table"><listheader><term>Parameter</term><description>Description</description></listheader>
        /// <item><term><c>PropertyName</c></term><description>Name of property to set behavior for. Required.</description></item>
        /// <item><term><c>TypeName</c></term><description>Name of type property <c>PropertyName</c> is property of. Required.</description></item>
        /// <item><term><c>Empty</c></term><description>Serialization mode. Value <c>Empty</c> mans empty string. Value <c>Null</c> (or anything lese than <c>Empty</c>) means not to serialize when empty. Required.</description></item>
        /// </list></param>
        /// <exception cref="KeyNotFoundException">A required parameter not found in <paramref name="paramatars"/> dictionary.</exception>
        public void Initialize(System.Collections.Generic.IDictionary<string, string> parameters) {
            propertyName = parameters["PropertyName"];
            typeName = parameters["TypeName"];
            switch (parameters["Empty"]) {
                case "Empty": empty = true; break;
                case "Null": empty = false; break;
            }
        }
        private string typeName;
        private string propertyName;
        private bool empty = false;
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
            if (property == null) throw new InvalidOperationException(string.Format(Resources.ex_PropertyNotFound, propertyName));

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
                new CodeStatement[] { new CodeMethodReturnStatement(new CodePrimitiveExpression(empty ? string.Empty : null)) },
                new CodeStatement[] { new CodeMethodReturnStatement(new CodeVariableReferenceExpression("property_return_value")) }));

        }

    }
}
