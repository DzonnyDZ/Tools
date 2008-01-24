#if Nightly || Alpha || Beta || RC || Release
/*
 * Copyright (C) 2006 Chris Stefano
 *       cnjs@mweb.co.za
 */
namespace Tools.Generators {
    using System;
    using System.IO;
    using System.Text;
    using Microsoft.Win32;
    using System.Runtime.InteropServices;
    using Tools.Generators;

    /// <summary>
    /// Abstract base for custom tools for text files
    /// </summary>
    /// <remarks>Inheriter must supply the <see cref="GuidAttribute"/> and <see cref="CustomToolAttribute"/> </remarks>
    public abstract class CustomToolBase:BaseCodeGeneratorWithSite {

        #region Static Section
        /// <summary>Guid of C#'s category under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\9.0\Generators\ in registry.</summary>
        /// <remarks>Valid for VS 2008 and 2005 as well</remarks>
        internal static Guid CSharpCategoryGuid = new Guid("FAE04EC1-301F-11D3-BF4B-00C04F79EFBC");
        /// <summary>Guid of VB's category under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\9.0\Generators\ in registry.</summary>
        /// <remarks>Valid for VS 2008 and 2005 as well</remarks>
        internal static Guid VBCategoryGuid = new Guid("{164B10B9-B200-11D0-8C61-00A0C91E29D5}");

        /// <summary>
        /// Registers class
        /// </summary>
        /// <param name="t">Type to register</param>
        [ComRegisterFunction]
        public static void RegisterClass(Type t) {
            GuidAttribute guidAttribute = getGuidAttribute(t);
            CustomToolAttribute customToolAttribute = getCustomToolAttribute(t);
            foreach(string VSVer in new string[] { "9.0", "8.0" })
                foreach(Guid LangGuid in new Guid[] { CSharpCategoryGuid, VBCategoryGuid })
                    using(RegistryKey key = Registry.LocalMachine.CreateSubKey(GetKeyName(LangGuid, customToolAttribute.Name, VSVer))) {
                        key.SetValue("", customToolAttribute.Description);
                        key.SetValue("CLSID", "{" + guidAttribute.Value + "}");
                        key.SetValue("GeneratesDesignTimeSource", 1);
                    }
        }

        /// <summary>
        /// Unregisters class
        /// </summary>
        /// <param name="t">Type to unregister</param>
        [ComUnregisterFunction]
        public static void UnregisterClass(Type t) {
            CustomToolAttribute customToolAttribute = getCustomToolAttribute(t);
            foreach(string VSVer in new string[] { "9.0", "8.0" })
                foreach(Guid LangGuid in new Guid[] { CSharpCategoryGuid, VBCategoryGuid })
                    Registry.LocalMachine.DeleteSubKey(GetKeyName(LangGuid, customToolAttribute.Name, VSVer), false);
        }

        /// <summary>
        /// Gets class's <see cref="GuidAttribute"/>
        /// </summary>
        /// <param name="t">Type to get GUID of</param>
        /// <returns><see cref="GuidAttribute"/> applied on type <paramref name="t"/></returns>
        internal static GuidAttribute getGuidAttribute(Type t) {
            return (GuidAttribute)getAttribute(t, typeof(GuidAttribute));
        }

        /// <summary>
        /// Gets <see cref="CustomToolAttribute"/> of class
        /// </summary>
        /// <param name="t">Class to get attribute of</param>
        /// <returns><see cref="CustomToolAttribute"/> applied on type <paramref name="t"/></returns>
        internal static CustomToolAttribute getCustomToolAttribute(Type t) {
            return (CustomToolAttribute)getAttribute(t, typeof(CustomToolAttribute));
        }

        /// <summary>
        /// Gets attribute of given type which is applied on given type
        /// </summary>
        /// <param name="t">Type to get attribute of</param>
        /// <param name="attributeType">Type of attribute to get</param>
        /// <returns>1st attribute applied</returns>
        /// <exception cref="ArgumentException">Type <paramref name="t"/> is not decorated with attribute <paramref name="attributeType"/>.</exception>
        internal static Attribute getAttribute(Type t, Type attributeType) {
            object[] attributes = t.GetCustomAttributes(attributeType, /* inherit */ true);
            if(attributes.Length == 0)
                throw new ArgumentException(String.Format("Class '{0}' does not provide a '{1}' attribute.", t.FullName, attributeType.FullName));
            return (Attribute)attributes[0];
        }

        /// <summary>
        /// Gets kay name for given VS version, language GUID and tool name
        /// </summary>
        /// <param name="categoryGuid">GUID of language category. <see cref="CSharpCategoryGuid"/> and <see cref="VBCategoryGuid"/>.</param>
        /// <param name="toolName">Name of tool itself</param>
        /// <param name="VSVersion">Version of Visual Studio (i.e. 8.0, 9.0)</param>
        /// <returns>Registy key path (root key not included)</returns>
        internal static string GetKeyName(Guid categoryGuid, string toolName, string VSVersion) {
            return String.Format("SOFTWARE\\Microsoft\\VisualStudio\\{2}\\Generators\\{{{0}}}\\{1}\\", categoryGuid.ToString(), toolName, VSVersion);
        }

        #endregion

        /// <summary>
        /// CTor
        /// </summary>
        protected CustomToolBase() { }

        /// <summary>
        /// Performs code generation
        /// </summary>
        /// <param name="inputFileName">Name of file to convert. Note: May be invalid or inactual. <paramref name="inputFileContent"/> is more important.</param>
        /// <param name="inputFileContent">Content of file to convert</param>
        /// <returns>Converted content</returns>
        protected override byte[] GenerateCode(string inputFileName, string inputFileContent) {

            // get the generated code
            string returnString = this.DoGenerateCode(inputFileName, inputFileContent);

            // return the generated code
            return System.Text.Encoding.UTF8.GetBytes(returnString);

        }

        /// <summary>
        /// Performs content conversion
        /// </summary>
        /// <param name="inputFileName">Name of input file to convert. Note: My be invalid or inactual. <paramref name="inputFileContent"/> always contain actual text of file.</param>
        /// <param name="inputFileContent">Content of file to convert</param>
        /// <returns>File converted</returns>
        public abstract string DoGenerateCode(string inputFileName, string inputFileContent);

    }
}
#endif