using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>Removes member of type</summary>
    /// <remarks>This extension can remove multiple members of one type. If you need to remove members from multiple types use this extension multiple times.
    /// <para>
    /// This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.
    /// </para></remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension "Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.RemoveMembers" Name="..." Type="..."?>]]></code></example>
    /// <version version="1.5.3">This class was re-introduced in version 1.5.3</version>
    public class RemoveMembers : ICodeExtension {
        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters: Thic classs expects following parameters:
        /// <list type="table"><listheader><term>Parameter</term><description>Description</description></listheader>
        /// <item><term><c>Name</c></term><description>Name(s) of member to be removed. Multiple members separated by comma (,) - no whitespaces around. Required.</description></item>
        /// <item><term><c>Type</c></term><description>Name of type to remove members from. Required.</description></item>
        /// </list></param>
        /// <exception cref="KeyNotFoundException">A required ky is not present in <paramref name="parameters"/> dictionary.</exception>
        public void Initialize(System.Collections.Generic.IDictionary<string, string> parameters) {
            names = parameters["Name"].Split(',');
            type = parameters["Type"];
        }
        private string type;
        private string[] names;
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <exception cref="InvalidOperationException">Type provided in the <c>Type</c> parameter was not found.</exception>
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider provider) {
            CodeTypeDeclaration type;
            try {
                type = ExtenstionHelpers.FindType(this.type, code);
            } catch (ArgumentException ex) {
                throw new InvalidOperationException(ex.Message, ex);
            }
            List<CodeTypeMember> rem = new List<CodeTypeMember>();
            foreach (CodeTypeMember m in type.Members)
                if (Array.IndexOf(names, m.Name) >= 0) rem.Add(m);
            foreach (var item in rem) type.Members.Remove(item);
        }

    }
}
