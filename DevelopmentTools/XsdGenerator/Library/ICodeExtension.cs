using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator {
    /// <summary>Interface for extension which processes generated CodeDOM</summary>
    public interface ICodeExtension {
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        void Process(System.CodeDom.CodeNamespace code, System.Xml.Schema.XmlSchema schema, CodeDomProvider provider);
        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        void Initialize(IDictionary<string, string> parameters);
    }

    /// <summary>Interface for extension which processes generated code</summary>
    /// <version version="1.5.3">Added documentation</version>
    public interface IPostExtension {
        /// <summary>Called when extension shall process generated code</summary>
        /// <param name="code">The code</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        void PostProcess(ref string code, CodeDomProvider provider);
        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters</param>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        void Initialize(IDictionary<string, string> parameters);
    }
}
