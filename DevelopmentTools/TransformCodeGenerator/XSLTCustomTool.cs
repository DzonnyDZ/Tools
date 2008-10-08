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

    /// <summary>
    /// This is a XSLT custom tool for Visual Studio. It performs XSL transform as custom tool in Visual Studio.
    /// </summary>
    /// <remarks>Unlike <see cref="TransformCodeGenerator"/> this tool should be attached to XSLT file, not to XML file.</remarks>
    /// <seealso cref="TransformCodeGenerator"/>
    [Guid("D6CC6AA7-6B02-4e5c-98EA-847725A020D1")]
    [CustomTool("XsltCustomTool", "XSLT custom tool")]
    public class XsltCustomTool:CustomToolBase {

        /// <summary>
        /// CTor
        /// </summary>
        public XsltCustomTool() { }

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

        }
    }
