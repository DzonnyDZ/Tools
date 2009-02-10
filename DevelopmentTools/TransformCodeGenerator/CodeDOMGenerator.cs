using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.GeneratorsT;
using System.Xml.Linq;
using System.CodeDom;
using Tools.CodeDomT;
using System.Reflection;
using System.Runtime.InteropServices;
using Tools.VisualStudioT.GeneratorsT;

namespace Tools.GeneratorsT {
    /// <summary>This is Code DOM generator. It performs code generation from XML-serialized Code DOM.</summary>
    /// <seealso cref="Xml2CodeDom"/>
    /// <seealso cref="CodeDomTransformCodeGenerator"/>
    /// <seealso cref="CodeDomXsltCustomTool"/>
    [Guid("577A80C1-701B-4484-8277-9C6D03D04FFD")]
    [CustomTool("CodeDOMGenerator", "Code DOM generator")]
    public class CodeDOMGenerator:CustomToolBase {
        /// <summary>
        /// Performs code generation
        /// </summary>
        /// <param name="inputFileName">Name of file to convert</param>
        /// <param name="inputFileContent">Content of file to convert</param>
        /// <returns>File converted</returns>
        public override string DoGenerateCode(string inputFileName, string inputFileContent) {
            XDocument x = XDocument.Parse(inputFileContent);
            Xml2CodeDom x2c = new Xml2CodeDom();
            CodeCompileUnit cu = x2c.Xml2CompileUnit(x);
            System.IO.StringWriter outW = new System.IO.StringWriter();
            base.CodeProvider.GenerateCodeFromCompileUnit(cu, outW, new System.CodeDom.Compiler.CodeGeneratorOptions());
            return outW.ToString();
            }

        /// <summary>Called when assembly is registeered with COM</summary>
        /// <param name="t">Type to be registered</param>
        [ComRegisterFunction]
        private static void ComRegister(Type t) {
            if(t.Equals(typeof(CodeDOMGenerator))) {
                RegisterCustomTool(t, true);
                Console.WriteLine("Custom tool {0} registered.", t.FullName);
                }
            }
        /// <summary>Called when assembly is un-registeered with COM</summary>
        /// <param name="t">Type to be un-registered</param>
        [ComUnregisterFunction]
        private static void ComUnRegister(Type t) {
            if(t.Equals(typeof(CodeDOMGenerator))) {
                RegisterCustomTool(t, false);
                Console.WriteLine("Custom tool {0} un-registered.", t.FullName);
                }
            }
        }
    }
        
