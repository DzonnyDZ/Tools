/* © Ðonny 2008, based on
 * Copyright (C) 2006 Chris Stefano
 *       cnjs@mweb.co.za
  */
namespace Tools.GeneratorsT {
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Xml;
    using System.Xml.XPath;
    using System.Xml.Xsl;
    using System.Xml.Linq;
    using Tools.VisualStudioT.GeneratorsT;

    /// <summary>
    /// This is a Code DOM XSLT custom tool for Visual Studio. It performs XSL transform as custom tool in Visual Studio and interprets its result as XML-serialuzed CodeDOM.
    /// </summary>
    /// <remarks>Unlike <see cref="CodeDomTransformCodeGenerator"/> this tool should be attached to XSLT file, not to XML file.</remarks>
    /// <seealso cref="Tools.CodeDomT.Xml2CodeDom"/>
    /// <seealso cref="CodeDomTransformCodeGenerator"/>
    /// <see cref="CodeDOMGenerator"/>
    [Guid("88185642-6CBD-45a6-9447-3D6C19AAE4B6")]
    [CustomTool("CodeDomXsltCustomTool", "Code DOM XSLT custom tool")]
    public class CodeDomXsltCustomTool:CustomToolBase {

        /// <summary>
        /// CTor
        /// </summary>
        public CodeDomXsltCustomTool() { }

        /// <summary>
        /// Performs code generation
        /// </summary>
        /// <param name="inputFileName">Name of XSLT template file</param>
        /// <param name="inputFileContent">Content of XSLT template file</param>
        /// <returns>File converted</returns>
        public override string DoGenerateCode(string inputFileName, string inputFileContent) {

            string XmlFileName = Tools.ResourcesT.TransforCodeGeneratorResources.NOTFOUND;
            StringWriter outputWriter = new StringWriter();
            try {

                FileInfo inputFileInfo = new FileInfo(inputFileName);

                // get the XML document for XSLT file
                XmlDocument XsltDocument = new XmlDocument();
                XsltDocument.LoadXml(inputFileContent);

                // get the filename of xml file
                var inputPIs = XsltDocument.SelectNodes("/processing-instruction('input')");
                if(inputPIs.Count > 0) XmlFileName = inputPIs[0].Value;

                if(!File.Exists(XmlFileName)) {
                    // try in the same dir as the file
                    XmlFileName = Path.Combine(inputFileInfo.DirectoryName, XmlFileName);

                    if(!File.Exists(XmlFileName)) {
                        // try in the dir where this dll lives
                        FileInfo assemblyFileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
                        XmlFileName = Path.Combine(assemblyFileInfo.DirectoryName, XmlFileName);
                        }
                    }

                    // get the xslt document
                    XPathDocument transformerDoc = new XPathDocument(inputFileContent);
                    // get input document
                    XmlDocument inputDocument = new XmlDocument();
                    inputDocument.Load(XmlFileName);

                    // create the transform
                    XslCompiledTransform xslTransform = new XslCompiledTransform();
                    xslTransform.Load(transformerDoc.CreateNavigator(), XsltSettings.Default, null);

                    FileInfo fi = new FileInfo(inputFileName);

                    XsltArgumentList args = new XsltArgumentList();

                    AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();

                    args.AddParam("generator", String.Empty, assemblyName.FullName);
                    args.AddParam("version", String.Empty, assemblyName.Version.ToString());
                    args.AddParam("fullfilename", String.Empty, inputFileName);
                    args.AddParam("filename", String.Empty, fi.Name);
                    args.AddParam("date-created", String.Empty, DateTime.Today.ToLongDateString());
                    args.AddParam("created-by", String.Empty, String.Format("{0}\\{1}", Environment.UserDomainName, Environment.UserName));
                    args.AddParam("namespace", String.Empty, base.FileNameSpace);
                    args.AddParam("classname", String.Empty, fi.Name.Substring(0, fi.Name.LastIndexOf(".")));

                    // do the transform
                    xslTransform.Transform(inputDocument, args, outputWriter);

                    XDocument result = XDocument.Parse(outputWriter.ToString());
                    Tools.CodeDomT.Xml2CodeDom x2d = new Tools.CodeDomT.Xml2CodeDom();
                    outputWriter = new StringWriter();
                    base.CodeProvider.GenerateCodeFromCompileUnit(x2d.Xml2CompileUnit(result), outputWriter, new System.CodeDom.Compiler.CodeGeneratorOptions());

                } catch(Exception ex) {
                    string bCommentStart;
                    string bCommentEnd;
                    string lCommentStart;
                    if(this.GetDefaultExtension().ToLower() == ".vb") {
                        bCommentStart = "'";
                        bCommentEnd = "'";
                        lCommentStart = "'";
                        } else {
                        bCommentStart = "/*";
                        bCommentEnd = "*/";
                        lCommentStart = "";
                        }
                    outputWriter.WriteLine(bCommentStart);
                    outputWriter.WriteLine(lCommentStart + "\t" + Tools.ResourcesT.TransforCodeGeneratorResources.ERRORUnableToGenerateOutputForTemplate);
                    outputWriter.WriteLine(lCommentStart + "\t'{0}'", XmlFileName);
                    outputWriter.WriteLine(lCommentStart + "\t" + Tools.ResourcesT.TransforCodeGeneratorResources.UsingTransformer);
                    outputWriter.WriteLine(lCommentStart + "\t'{0}'", inputFileName);
                    outputWriter.WriteLine(lCommentStart + "");
                    outputWriter.WriteLine(lCommentStart + ex.ToString());
                    outputWriter.WriteLine(bCommentEnd);
                }

            return outputWriter.ToString();

            }

        /// <summary>Called when assembly is registeered with COM</summary>
        /// <param name="t">Type to be registered</param>
        [ComRegisterFunction]
        private static void ComRegister(Type t) {
            if(t.Equals(typeof(CodeDomXsltCustomTool))) {
                RegisterCustomTool(t, true);
                Console.WriteLine("Custom tool {0} registered.", t.FullName);
            }
        }
        /// <summary>Called when assembly is un-registeered with COM</summary>
        /// <param name="t">Type to be un-registered</param>
        [ComUnregisterFunction]
        private static void ComUnRegister(Type t) {
            if(t.Equals(typeof(CodeDomXsltCustomTool))) {
                RegisterCustomTool(t, false);
                Console.WriteLine("Custom tool {0} un-registered.", t.FullName);
            }
        }
        }
    }
