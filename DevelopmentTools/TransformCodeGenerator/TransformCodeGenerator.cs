/*
 * Copyright (C) 2006 Chris Stefano
 *       cnjs@mweb.co.za
 * I've foundthis great tool dunno where on the Internet and I hope I'm not violating any rights when I'v included it as development tool in ÐTools and tweaked it a little.      
 */
namespace Tools.GeneratorsT
{
  using System;
  using System.IO;
  using System.Reflection;
  using System.Runtime.InteropServices;
  using System.Xml;
  using System.Xml.XPath;
  using System.Xml.Xsl;

  /// <summary>
  /// This is a transform code generator. It performs XSL transform as custom tool in Visual Studio.
  /// </summary>
  [Guid("07834038-5EA7-4d0d-8194-B8E91DC75638")]
  [CustomTool("TransformCodeGenerator", "Transform Code Generator")]
  public class TransformCodeGenerator: CustomToolBase{

    /// <summary>
    /// CTor
    /// </summary>
    public TransformCodeGenerator(){}

    /// <summary>
    /// Performs code generation
    /// </summary>
    /// <param name="inputFileName">Name of file to convert</param>
    /// <param name="inputFileContent">Content of file to convert</param>
    /// <returns>File converted</returns>
    public override string DoGenerateCode(string inputFileName, string inputFileContent){

      string transformerFileName = Tools.Generators.Resources.NOTFOUND;
      StringWriter outputWriter = new StringWriter();
      try
      {

        FileInfo inputFileInfo = new FileInfo(inputFileName);

        // get the source document
        XmlDocument sourceDocument = new XmlDocument();
        sourceDocument.LoadXml(inputFileContent);

        // get the filename of the transformer
        string transformerFile = sourceDocument.DocumentElement.Attributes["transformer"].Value;
        
        if(!File.Exists(transformerFileName))
        {
          // try in the same dir as the file
          transformerFileName = Path.Combine(inputFileInfo.DirectoryName, transformerFile);

          if (!File.Exists(transformerFileName))
          {
            // try in the dir where this dll lives
            FileInfo assemblyFileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            transformerFileName = Path.Combine(assemblyFileInfo.DirectoryName, transformerFile);
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

      }
      catch(Exception ex)
      {
        outputWriter.WriteLine("/*");
        outputWriter.WriteLine("\t"+Tools.Generators.Resources.ERRORUnableToGenerateOutputForTemplate);
        outputWriter.WriteLine("\t'{0}'", inputFileName);
        outputWriter.WriteLine("\t"+Tools.Generators.Resources.UsingTransformer);
        outputWriter.WriteLine("\t'{0}'", transformerFileName);
        outputWriter.WriteLine("");
        outputWriter.WriteLine(ex.ToString());
        outputWriter.WriteLine("*/");
      }

      return outputWriter.ToString();

    }

  }

}
