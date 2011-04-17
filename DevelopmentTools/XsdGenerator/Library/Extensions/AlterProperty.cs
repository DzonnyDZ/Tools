using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>Aters property in various ways</summary>
    /// <remarks>
    /// NewType recognizes last character ? as System.Nullable`1.
    /// FieldName is used only with NewType.
    /// <para>This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.</para>
    /// </remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension "Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.AlterProperty" PropertyName="..." TypeName="..." NewName=".." OrAttributes="..." AndAttributes="..." NewType="..." FieldName="..."?>]]></code></example>
    /// <version version="1.5.3">This class was re-introduced in version 1.5.3</version>
    public class AlterProperty : ICodeExtension {
        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters: This class expects following parameters:
        /// <list type="table"><listheader><term>Parameter</term><description>Description</description>
        /// <item><term><c>PropertyName</c></term><description>Name of the property</description>. Required.</item>
        /// <item><term><c>TypeName</c></term><description>Name of type property <c>PropertyName</c> is property of. Required.</description></item>
        /// <item><term><c>NewName</c></term><description>Specified a new name of the property. Optional.</description></item>
        /// <item><term><c>OrAttributes</c></term><description>Represents attributes of the property as <see cref="MemberAttributes"/> value. These new attribuets are OR-ed with exising property attributes. Must be string representation of integer value in invariant culture. Optional.</description></item>
        /// <item><term><c>AndAttributes</c></term><description>Represents attributes of the property as <see cref="MemberAttributes"/> value. These new attributes are AND-ed with exising property attributes. Must be string representation of integer value in invariant culture. Optional.</description></item>
        /// <item><term><c>NewType</c></term><description>Change type of the property. Recognizes last character ? as <see cref="Nullable{T}"/>. Optional.</description></item>
        /// <item><term><c>FieldName</c></term><description>Only used with <c>NewType</c>. Name of backing field. Optional.</description></item>
        /// </listheader></list></param>
        /// <exception cref="KeyNotFoundException">A required parameter is not present in the <paramref name="parameters"/> dictionary.</exception>
        /// <exception cref="FormatException">Value for <c>AndAttributes</c> or <c>OrAttributes</c> cannot be parsed as integer in invariant culture.</exception>
        /// <exception cref="OverflowException">Value for <c>AndAttributes</c> or <c>OrAttributes</c> does not fall to range of <see cref="int"/>.</exception>
        public void Initialize(System.Collections.Generic.IDictionary<string, string> parameters) {
            propertyName = parameters["PropertyName"];
            typeName = parameters["TypeName"];
            if (parameters.ContainsKey("NewName")) newName = parameters["NewName"];
            if (parameters.ContainsKey("OrAttributes")) orAttributes = (MemberAttributes)int.Parse(parameters["OrAttributes"], System.Globalization.CultureInfo.InvariantCulture);
            if (parameters.ContainsKey("AndAttributes")) andAttributes = (MemberAttributes)int.Parse(parameters["AndAttributes"], System.Globalization.CultureInfo.InvariantCulture);
            if (parameters.ContainsKey("NewType")) newPropertyType = parameters["NewType"];
            if (parameters.ContainsKey("NewRank")) newArrayRank = int.Parse(parameters["NewRank"]);
            if (parameters.ContainsKey("FieldName")) backingField = parameters["FieldName"];//Only to change type if property type changes
        }
        private string typeName;
        private string propertyName;
        private string newName;
        private string newPropertyType;
        private int? newArrayRank;
        private string backingField;
        private MemberAttributes orAttributes = 0x0;
        private MemberAttributes andAttributes = (MemberAttributes)~0x0;
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <exception cref="InvalidOperationException">Type, property or filed cannot be found.</exception>
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
            CodeMemberField field = null;
            if (backingField != null) {
                foreach (CodeTypeMember m in type.Members) {
                    if (m is CodeMemberField && m.Name == backingField) {
                        field = (CodeMemberField)m;
                        break;
                    }
                }
                if (field == null) throw new InvalidOperationException(string.Format(Resources.ex_FieldNotFound, backingField));
            }
            if (newName != null) property.Name = newName;
            property.Attributes &= andAttributes;
            property.Attributes |= orAttributes;
            if (newPropertyType != null) {
                CodeTypeReference newType;
                if (newPropertyType.EndsWith("?")) {
                    newType = new CodeTypeReference(typeof(System.Nullable<>));
                    newType.TypeArguments.Add(new CodeTypeReference(newPropertyType.Substring(0, newPropertyType.Length - 1)));
                } else newType = new CodeTypeReference(newPropertyType);
                if (!newArrayRank.HasValue)
                    property.Type = new CodeTypeReference(newPropertyType);
                else
                    property.Type = new CodeTypeReference(newPropertyType, newArrayRank.Value);
                if (field != null) field.Type = property.Type;
            }
        }

    }
}
