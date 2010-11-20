using System;
using System.CodeDom;
using System.Collections;
using System.Xml.Schema;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions{
    /// <summary>Creates list of types</summary>
    /// <remarks>
    /// List of types is generated as static class (standard module in VB).
    /// The class is called <c>misc</c> and contains one static field initialized to array of all types in generated namespace.
    /// </remarks>
    public class TypeList : ICodeExtension	{
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
        public void Process(CodeNamespace code, XmlSchema schema, CodeDomProvider provider) {
            CodeTypeDeclaration misc = new CodeTypeDeclaration("misc");
            if (provider.FileExtension == "vb") misc.UserData["Module"] = true;
            else misc.IsPartial = true;
            misc.TypeAttributes = System.Reflection.TypeAttributes.Sealed | System.Reflection.TypeAttributes.NotPublic;
            misc.Comments.Add(new CodeCommentStatement(new CodeComment(Tools.VisualStudioT.GeneratorsT.XsdGenerator.Resources.Summary_misc, true)));
            CodeMemberField types = new CodeMemberField(
                new CodeTypeReference(new CodeTypeReference(typeof(Type)), 1), "types");
            types.Attributes = MemberAttributes.Assembly | MemberAttributes.Static;
            types.InitExpression = new CodeArrayCreateExpression();
            types.Comments.Add(new CodeCommentStatement(new CodeComment(Tools.VisualStudioT.GeneratorsT.XsdGenerator.Resources.Summary_misc_types, true)));
            ((CodeArrayCreateExpression)types.InitExpression).CreateType = types.Type;
            AddTypes("",false, code.Types, ((CodeArrayCreateExpression)types.InitExpression).Initializers);
            misc.Members.Add(types);
            code.Types.Add(misc);
        }
        private static void AddTypes(string prepend, bool nested, CodeTypeDeclarationCollection types, CodeExpressionCollection into){
            foreach (CodeTypeDeclaration t in types) {
                into.Add(new CodeTypeOfExpression(
                    ((prepend == null || prepend == "") ? "" : (prepend + (nested ? "+" : "."))) + t.Name));
                CodeTypeDeclarationCollection ctd = new CodeTypeDeclarationCollection();
                foreach (CodeTypeMember m in t.Members) {
                    if (m is CodeTypeDeclaration) ctd.Add((CodeTypeDeclaration)m);
                }
                AddTypes(
                    ((prepend == null || prepend == "") ? "" : (prepend + (nested ? "+" : "."))) + t.Name,
                    true, ctd, into);
            }
        }
    }
}
