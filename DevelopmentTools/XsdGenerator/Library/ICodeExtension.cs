using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator {
    /// <summary>Base interface for post-processing extensions for XSD generator Custom Tool</summary>
    /// <version version="1.5.4">This interface is new in version 1.5.4</version>
    public interface IExtensionBase {
        /// <summary>Initializes the extension</summary>
        /// <param name="parameters">Initialization parameters</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        /// <version version="1.5.4">Method moved and merged from interfaces <see cref="ICodeExtension"/> and <see cref="IPostExtension"/></version>
        void Initialize(IDictionary<string, string> parameters);
    }
    /// <summary>Interface for extension which processes generated CodeDOM</summary>
    /// <version version="1.5.4">Method <c></c>Initialize</version> moved to <see cref="IExtensionBase"/> interface</version>
    public interface ICodeExtension : IExtensionBase {
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        void Process(System.CodeDom.CodeNamespace code, System.Xml.Schema.XmlSchema schema, CodeDomProvider provider);
    }

    /// <summary>Interface for extension which processes generated code</summary>
    /// <version version="1.5.3">Added documentation</version>
    /// <version version="1.5.4">Method <c></c>Initialize</version> moved to <see cref="IExtensionBase"/> interface</version>
    public interface IPostExtension : IExtensionBase {
        /// <summary>Called when extension shall process generated code</summary>
        /// <param name="code">The code</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        void PostProcess(ref string code, CodeDomProvider provider);
    }
}
