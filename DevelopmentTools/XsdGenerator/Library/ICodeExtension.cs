using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator
{
	/// <summary>
	/// Interface for extension types that wish to participate in the 
	/// WXS code generation process.
	/// </summary>
	public interface ICodeExtension
	{
        void Process(System.CodeDom.CodeNamespace code, System.Xml.Schema.XmlSchema schema, CodeDomProvider Provider);
        void Initialize(IDictionary<string,string> Parameters);
	}

    public interface IPostExtension {
        void PostProcess(ref string code, CodeDomProvider Provider);
        void Initialize(IDictionary<string, string> Parameters);
    }
}
