/*
 * Copyright (C) 2006 Chris Stefano
 *       cnjs@mweb.co.za
 * I've foundthis great tool dunno where on the Internet and I hope I'm not violating any rights when I'v included it as development tool in ÐTools and tweaked it a little.      
 */
namespace Tools.VisualStudioT.GeneratorsT {
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Xml;
    using System.Xml.XPath;
    using System.Xml.Xsl;
    using Tools.VisualStudioT.GeneratorsT;

    /// <summary>
    /// This is a transform code generator. It performs XSL transform as custom tool in Visual Studio.
    /// </summary>
    /// <seealso cref="XsltCustomTool"/>
    /// <version version="1.5.3">Class moved from namespace Tools.GeneratorsT to <see cref="Tools.VisualStudioT.GeneratorsT"/></version>
    [Guid("2F8B768B-DBEA-407d-9A43-416BE87FA6A5")]
    [CustomTool("TransformCodeGenerator", "Transform Code Generator")]
    public class TransformCodeGenerator:CustomToolBase {

        /// <summary>
        /// CTor
        /// </summary>
        public TransformCodeGenerator() { }

        /// <summary>
        /// Performs code generation
        /// </summary>
        /// <param name="inputFileName">Name of file to convert</param>
        /// <param name="inputFileContent">Content of file to convert</param>
        /// <returns>File converted</returns>
        public override string DoGenerateCode(string inputFileName, string inputFileContent) {

            string transformerFileName = Tools.ResourcesT.TransforCodeGeneratorResources.NOTFOUND;
            StringWriter outputWriter = new StringWriter();
            try {

                FileInfo inputFileInfo = new FileInfo(inputFileName);

                // get the source document
                XmlDocument sourceDocument = new XmlDocument();
                sourceDocument.LoadXml(inputFileContent);

                // get the filename of the transformer
                var transformerPIs = sourceDocument.SelectNodes("/processing-instruction('transformer')");
                if(transformerPIs.Count > 0) transformerFileName = transformerPIs[0].Value;

                if(!File.Exists(transformerFileName)) {
                    // try in the same dir as the file
                    transformerFileName = Path.Combine(inputFileInfo.DirectoryName, transformerFileName);

                    if(!File.Exists(transformerFileName)) {
                        // try in the dir where this dll lives
                        FileInfo assemblyFileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
                        transformerFileName = Path.Combine(assemblyFileInfo.DirectoryName, transformerFileName);
                        }
                    }

                // get the xslt document
                XPathDocument transformerDoc = new XPathDocument(transformerFileName);

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
                xslTransform.Transform(sourceDocument, args, outputWriter);

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
                outputWriter.WriteLine(lCommentStart + "\t'{0}'", inputFileName);
                outputWriter.WriteLine(lCommentStart + "\t" + Tools.ResourcesT.TransforCodeGeneratorResources.UsingTransformer);
                outputWriter.WriteLine(lCommentStart + "\t'{0}'", transformerFileName);
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
            if(t.Equals(typeof(TransformCodeGenerator))) {
                RegisterCustomTool(t, true);
                Console.WriteLine("Custom tool {0} registered.", t.FullName);
                }
            }
        /// <summary>Called when assembly is un-registeered with COM</summary>
        /// <param name="t">Type to be un-registered</param>
        [ComUnregisterFunction]
        private static void ComUnRegister(Type t) {
            if(t.Equals(typeof(TransformCodeGenerator))) {
                RegisterCustomTool(t, false);
                Console.WriteLine("Custom tool {0} un-registered.", t.FullName);
                }
            }

        }
    }
