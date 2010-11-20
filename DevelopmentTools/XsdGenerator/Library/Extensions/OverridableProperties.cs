using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions
{
    /// <summary>Converts all properties to virtual (overridable in VB) ones</summary>
    public class OverridableProperties : ICodeExtension
    {
        /// <summary>Initializes the extension (this implementation does nothing)</summary>
        /// <param name="parameters">Initialization parameters (ignored)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        void ICodeExtension.Initialize(System.Collections.Generic.IDictionary<string, string> parameters) { }
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider provider)
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
