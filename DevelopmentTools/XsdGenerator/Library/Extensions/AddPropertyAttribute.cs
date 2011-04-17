using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>Adds attribute to property</summary>
    /// <remarks>
    /// This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.
    /// </remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension "Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.AddPropertyAttribute" PropertyName="..." TypeName="..." Name="..." p-0="..." p-xxx="..."?>]]></code></example>
    /// <version version="1.5.3">This class was re-introduced in version 1.5.3</version>
    public class AddPropertyAttribute : ICodeExtension {
        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters: This class expects following parameters:
        /// <list type="table"><listheader><term>Parameter</term><description>Description</description></listheader>
        /// <item><term><c>PropertyName</c></term><description>Name of property to add attribute to. Required.</description></item>
        /// <item><term><c>TypeName</c></term><description>Name of type property <c>PropertyName</c> is property of. Required.</description></item>
        /// <item><term><c>Name</c></term><description>Name of the attribute</description></item>
        /// <item><term><c>p-&lt;number></c></term><description>Positional attribute constructor parameter. Optional. See remarks.</description></item>
        /// <item><term><c>p-&lt;string></c></term><description>Named attribute parameter. Optional. See remarks.</description></item>
        /// </list></param>
        /// <remarks>
        /// Positional and named parameters of constructor have name in form p-&lt;number or name> and value in form "&lt;TypeName> &lt;value>".
        /// TypeName can be one of supported attribute parameter types (<see cref="String"/>, <see cref="Byte"/>, <see cref="SByte"/>, <see cref="Int16"/>, <see cref="UInt16"/>, <see cref="Int32"/>, <see cref="UInt32"/>, <see cref="Int64"/>, <see cref="UInt64"/>, <see cref="Decimnal"/>, <see cref="Single"/>, <see cref="Double"/>, <see cref="Boolean"/>, <see cref="DateTime"/>, <see cref="Char"/>, <see cref="Type"/>).
        /// If it is not one of them it's treated a name of enumeration type. For supported types value is parsed in invariant culture to that type using appropriate <c>Parse</c> method.
        /// With exception of <see cref="String"/> which is used directly and <see cref="Type"/> where value part is treated as type name.
        /// For enumerations value part is treated as name of enumeration memner of enumeration specified in type part.
        /// <para>
        /// Positional parameters are added as constuctor parameters in order of their position numbers. Missing position numbers are ignored. Skipped optional parameters are not supported.
        /// Named parameters are added at the end of parameters list as named parameters.
        /// </para></remarks>
        /// <exception cref="KeyNotFoundException">A required parameter is not present in the <paramref name="parameters"/> dictionary.</exception>
        /// <exception cref="FormatException">Value of numeric type cannot be parsed.</exception>
        /// <exception cref="OverflowException">Value of numeric type is out of range of given numberic type.</exception>
        public void Initialize(System.Collections.Generic.IDictionary<string, string> parameters) {
            propertyName = parameters["PropertyName"];
            typeName = parameters["TypeName"];
            attributeName = parameters["Name"];
            //Params in arguments name p-Name or p-Number
            //Content typename-space-value (typename cannot contain space)
            List<CodeAttributeArgument> namedParams = new List<CodeAttributeArgument>();
            Dictionary<int, CodeAttributeArgument> posParams = new Dictionary<int, CodeAttributeArgument>();
            foreach (var item in parameters) {
                if (item.Key.StartsWith("p-")) {
                    string pName = item.Key.Substring(2);
                    CodeExpression pValue;
                    int pnum;
                    string TypeName = item.Value.Substring(0, item.Value.IndexOf(' '));
                    string valuePart = item.Value.Substring(item.Value.IndexOf(' ') + 1);
                    switch (TypeName) {
                        case "System.String": pValue = new CodePrimitiveExpression(valuePart); break;
                        case "System.Byte": pValue = new CodePrimitiveExpression(byte.Parse(valuePart, System.Globalization.CultureInfo.InvariantCulture)); break;
                        case "System.SByte": pValue = new CodePrimitiveExpression(SByte.Parse(valuePart, System.Globalization.CultureInfo.InvariantCulture)); break;
                        case "System.Int16": pValue = new CodePrimitiveExpression(Int16.Parse(valuePart, System.Globalization.CultureInfo.InvariantCulture)); break;
                        case "System.UInt16": pValue = new CodePrimitiveExpression(UInt16.Parse(valuePart, System.Globalization.CultureInfo.InvariantCulture)); break;
                        case "System.Int32": pValue = new CodePrimitiveExpression(Int32.Parse(valuePart, System.Globalization.CultureInfo.InvariantCulture)); break;
                        case "System.UInt32": pValue = new CodePrimitiveExpression(UInt32.Parse(valuePart, System.Globalization.CultureInfo.InvariantCulture)); break;
                        case "System.Int64": pValue = new CodePrimitiveExpression(Int64.Parse(valuePart, System.Globalization.CultureInfo.InvariantCulture)); break;
                        case "System.UInt64": pValue = new CodePrimitiveExpression(UInt64.Parse(valuePart, System.Globalization.CultureInfo.InvariantCulture)); break;
                        case "System.Decimal": pValue = new CodePrimitiveExpression(Decimal.Parse(valuePart, System.Globalization.CultureInfo.InvariantCulture)); break;
                        case "System.Single": pValue = new CodePrimitiveExpression(Single.Parse(valuePart, System.Globalization.CultureInfo.InvariantCulture)); break;
                        case "System.Double": pValue = new CodePrimitiveExpression(Double.Parse(valuePart, System.Globalization.CultureInfo.InvariantCulture)); break;
                        case "System.Boolean": pValue = new CodePrimitiveExpression(bool.Parse(valuePart)); break;
                        case "System.DateTime": pValue = new CodePrimitiveExpression((DateTime)new XAttribute("x", valuePart)); break;
                        case "System.Char": pValue = new CodePrimitiveExpression(valuePart[0]); break;
                        case "System.Type": pValue = new CodeTypeOfExpression(valuePart); break;
                        default://Enum
                            pValue = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(TypeName), valuePart); break;
                    }

                    if (int.TryParse(pName, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out pnum)) {
                        posParams.Add(pnum, new CodeAttributeArgument(pValue));
                    } else {
                        namedParams.Add(new CodeAttributeArgument(pName, pValue));
                    }
                }
            }
            attrParams.AddRange(from itm in posParams orderby itm.Key select itm.Value);
            attrParams.AddRange(namedParams);
        }
        private string typeName;
        private string propertyName;
        private string attributeName;
        private List<CodeAttributeArgument> attrParams = new List<CodeAttributeArgument>();
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <exception name="InvalidOperationException">
        /// Property with name <c>PropertyName</c> passed to the <see cref="Initialize"/> method was not found. -or- 
        /// Type with name <c>TypeName</c> passed to the <see cref="Initialize"/> method was not found.
        /// </exception>
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

            CodeAttributeDeclaration atrd;
            property.CustomAttributes.Add(atrd = new CodeAttributeDeclaration(attributeName));
            if (attrParams != null) {
                atrd.Arguments.AddRange(attrParams.ToArray());
            }
        }

    }
}
