//Updated by Ðonny to
// * Work with UTF-8 instead of ASCII
// * Properly resolve relative-path schema imports
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Collections.Generic;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator
{
	/// <summary>
	/// Processes WXS files and builds code for them.
	/// </summary>
	public sealed class Processor
	{
		public const string ExtensionNamespace = "http://weblogs.asp.net/cazzu";
		private static XPathExpression Extensions;
        private static XPathExpression PostExts;

		static Processor() 
		{
			XPathNavigator nav = new XmlDocument().CreateNavigator();
			// Select all extension types.
			Extensions = nav.Compile( "/processing-instruction('extension')" );
            PostExts = nav.Compile("/processing-instruction('post-process')");
			// Create and set namespace resolution context.
			XmlNamespaceManager nsmgr = new XmlNamespaceManager( nav.NameTable );
			nsmgr.AddNamespace( "xs", XmlSchema.Namespace );
			nsmgr.AddNamespace( "kzu", ExtensionNamespace );
			Extensions.SetContext( nsmgr );
		}

		private Processor() {}



		/// <summary>
		/// Processes the schema.
		/// </summary>
		/// <param name="xsdFile">The full path to the WXS file to process.</param>
		/// <param name="targetNamespace">The namespace to put generated classes in.</param>
		/// <returns>The CodeDom tree generated from the schema.</returns>
		public static CodeNamespace Process( string xsdFile, string targetNamespace,CodeDomProvider Provider )
		{
			// Load the XmlSchema and its collection.
			//XmlSchema xsd;
            //using ( FileStream fs = new FileStream( xsdFile, FileMode.Open ) )
            //{
            //    xsd = XmlSchema.Read( fs,  VH1 );
            //    xsd.Compile( VH2  );
            //}
            XmlSchemaSet set = new XmlSchemaSet();
            XmlSchema xsd = set.Add(null, xsdFile);
            set.Compile(); 

			XmlSchemas schemas = new XmlSchemas();
            schemas.Add( xsd);

			// Create the importer for these schemas.
			XmlSchemaImporter importer = new XmlSchemaImporter( schemas );

			// System.CodeDom namespace for the XmlCodeExporter to put classes in.
			CodeNamespace ns = new CodeNamespace( targetNamespace );
			XmlCodeExporter exporter = new XmlCodeExporter( ns );
			// Iterate schema top-level elements and export code for each.
			foreach ( XmlSchemaElement element in set.GlobalElements.Values )
			{
				// Import the mapping first.
				XmlTypeMapping mapping = importer.ImportTypeMapping(  
					element.QualifiedName  );
				// Export the code finally.
				exporter.ExportTypeMapping( mapping  );
			}

			#region Execute extensions
			
			XPathNavigator nav;
			using ( FileStream fs = new FileStream( xsdFile, FileMode.Open ,  FileAccess.Read   ) )
			{ 
				nav = new XPathDocument( fs ).CreateNavigator(); 
			}

			XPathNodeIterator it = nav.Select( Extensions );
			while ( it.MoveNext() )
			{
                Dictionary<string, string> values = ParsePEValue(it.Current.Value);
                Type t = Type.GetType(values["extension-type"], true);
				// Is the type an ICodeExtension?
				Type iface = t.GetInterface( typeof( ICodeExtension ).Name );
				if (iface == null)
					throw new ArgumentException( "Invalid extension type '" + it.Current.Value + "'." );

				ICodeExtension ext = ( ICodeExtension ) Activator.CreateInstance( t );
                ext.Initialize(values);
				// Run it!
				ext.Process( ns, xsd ,Provider);
			}

			#endregion Execute extensions

			return ns;
		}

        /// <summary>
        /// Parses value of processing instruction
        /// </summary>
        /// <param name="PEvalue">Value in format type-of-extension or "type-of-extension" attr1="value" attr2="value2" ...</param>
        /// <returns>Dictionary of attribute - value. 1st item has always key extension-type and contains type of extension</returns>
        private static Dictionary<string,string> ParsePEValue(string PEvalue) {
            Dictionary<string, string> ret =new Dictionary<string, string>();
            if(PEvalue.TrimStart().StartsWith("\"")){
                string PeValue2="<x extension-type="+PEvalue+"/>";
                XmlDocument tmpdoc = new XmlDocument();
                tmpdoc.LoadXml(PeValue2);
                foreach(XmlAttribute attr in tmpdoc.DocumentElement.Attributes){
                    ret.Add(attr.Name,attr.Value);
                    }
                }else{
                ret.Add("extension-type",PEvalue);
                }
            return ret;
            }

        public static void PostProcess(string xsdFile, ref string generatedCode, CodeDomProvider Provider){
            XPathNavigator nav;
            using (FileStream fs = new FileStream(xsdFile, FileMode.Open, FileAccess.Read)) {
                nav = new XPathDocument(fs).CreateNavigator();
            }

            XPathNodeIterator it = nav.Select(PostExts);
            while (it.MoveNext()) {
                Dictionary<string, string> values = ParsePEValue(it.Current.Value);
                Type t = Type.GetType(values["extension-type"], true);
                // Is the type an ICodeExtension?
                Type iface = t.GetInterface(typeof(IPostExtension).Name);
                if (iface == null)
                    throw new ArgumentException("Invalid extension type '" + it.Current.Value + "'.");

                IPostExtension ext = (IPostExtension)Activator.CreateInstance(t);
                ext.Initialize(values);
                // Run it!
                ext.PostProcess(ref generatedCode, Provider);
            }
        }
	}
   
}
