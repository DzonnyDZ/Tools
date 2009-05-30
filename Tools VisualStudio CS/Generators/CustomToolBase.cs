using System;
using System.IO;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using Tools.VisualStudioT.GeneratorsT;
using Tools.ReflectionT;
using System.Linq;

#if Nightly || Alpha || Beta || RC || Release
/*
 * Copyright (C) 2006 Chris Stefano
 *       cnjs@mweb.co.za
 */
namespace Tools.VisualStudioT.GeneratorsT {


    /// <summary>
    /// Abstract base for custom tools for text files
    /// </summary>
    /// <remarks>Inheriter must supply the <see cref="GuidAttribute"/> and <see cref="CustomToolAttribute"/> </remarks>
    /// <version version="1.5.2">Moved from namespace Tools.GeneratorsT to Tools.VisualStudioT.GeneratorsT</version>
    public abstract class CustomToolBase : BaseCodeGeneratorWithSite {

        #region Static Section
        /// <summary>Guid of C# category under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\&lt;version>\Generators\ in registry.</summary>
        /// <remarks>Valid for VS 2008 and 2005 as well</remarks>
        public readonly static Guid CSharpCategoryGuid = new Guid("{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}");
        /// <summary>Guid of VB category under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\&lt;version>\Generators\ in registry.</summary>
        /// <remarks>Valid for VS 2008 and 2005 as well</remarks>
        public readonly static Guid VBCategoryGuid = new Guid("{164B10B9-B200-11D0-8C61-00A0C91E29D5}");
        /// <summary>Guid of J# category under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\&lt;version>\Generators\ in registry.</summary>
        /// <remarks>Valid for VS 2005 only. J# is not part of VS 2008.</remarks>
        public readonly static Guid JSharpCategoryGuid = new Guid("{E6FDF8B0-F3D1-11D4-8576-0002A516ECE8}");
        /// <summary>Guid of C++ category under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\&lt;version>\Generators\ in registry.</summary>
        /// <remarks>Valid for VS 2008 and 2005 as well</remarks>
        public readonly static Guid CPPCategoryGuid = new Guid("{F1C25864-3097-11D2-A5C5-00C04F7968B4}");
        /// <summary>Guid of Phalanger (PHP) category under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\&lt;version>\Generators\ in registry.</summary>
        /// <remarks>Valid for VS 2008 and 2005 as well</remarks>
        public readonly static Guid PhalangerCategoryGuid = new Guid("{d5493d80-462d-41a7-897d-afad9cb4b757}");
        /// <summary>Guid of IronPython category under HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\&lt;version>\Generators\ in registry.</summary>
        /// <remarks>Valid for VS 2008 and 2005 as well</remarks>
        public readonly static Guid PythonCategoryGuid = new Guid("{38f7eba3-a5c6-4e32-b2c9-b6456f31b129}");

        /*/// <summary>
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
                        key.SetValue("GeneratesDesignTimeSource", attr);
                    }
        } */
        /// <summary>Registers or unregisters given class as Visual Studio CustomTool</summary>
        /// <param name="ToolClass">Class to be registered. It must derive from <see cref="CustomToolBase"/> and have <see cref="GuidAttribute"/> and <see cref="CustomToolAttribute"/></param>
        /// <param name="Register">True to register, false to unregister</param>
        /// <exception cref="ArgumentNullException"><paramref name="ToolClass"/> is null</exception>
        /// <exception cref="ArgumentException"><see cref="GuidAttribute"/> or <see cref="CustomToolAttribute"/> is not applied on <paramref name="ToolClass"/> 
        /// -or- <paramref name="ToolClass"/> is not RuntimeType or open generic type</exception>
        /// <exception cref="System.NotSupportedException"><paramref name="ToolClass"/> is <see cref="System.Reflection.Emit.TypeBuilder"/></exception>
        /// <exception cref="TypeMismatchException"><paramref name="ToolClass"/> cannot be assigned to <see cref="CustomToolBase"/></exception>
        /// <exception cref="System.Reflection.TargetInvocationException">CTor of <paramref name="ToolClass"/> throws an exception</exception>
        /// <exception cref="System.MethodAccessException">Caller does not have permission to call default CTor of <paramref name="ToolClass"/></exception>
        /// <exception cref="System.MemberAccessException"><paramref name="ToolClass"/> is abstract</exception>
        /// <exception cref="System.MissingMethodException">Public default CTor of type <paramref name="TollClass"/> wa not found</exception>
        /// <exception cref="System.TypeLoadException"><paramref name="ToolClass"/> is not valid type</exception>
        /// <remarks>This method should be called by <see cref="ComRegisterFunctionAttribute"/> and <see cref="ComUnregisterFunctionAttribute"/> methods of actual custom tool implementation.</remarks>
        /// <example>
        /// Following example shows how to use the <see cref="RegisterCustomTool(System.Type,bool)"/> method with com registration.
        /// <code language="cs"><![CDATA[
        /// [Guid("6071A00A-F725-4b59-86E5-B677CC089158")]
        /// [CustomTool("MyCustomTool", "My custom tool")]
        /// public class MyCustomTool:CustomToolBase {
        ///     //Custom tool implementation goes here
        ///     [ComRegisterFunction]
        ///     private static void ComRegister(Type t) {
        ///         if(t.Equals(typeof(MyCustomTool))) RegisterCustomTool(t, true);
        ///     }
        ///     [ComUnregisterFunction]
        ///     private static void ComRegister(Type t) {
        ///         if(t.Equals(typeof(MyCustomTool))) RegisterCustomTool(t, true);
        ///     }
        /// }
        /// ]]></code>
        /// /// <code language="vb"><![CDATA[
        /// <Guid("6071A00A-F725-4b59-86E5-B677CC089158")> _
        /// <CustomTool("MyCustomTool", "My custom tool")> _
        /// Public Class MyCustomTool : Inherits CustomToolBase
        ///     'Custom tool implementation goes here
        ///     <ComRegisterFunction> _
        ///     Private Shared Sub ComRegister(t As Type)
        ///         If t.Equals(GetType(MyCustomTool)) Then RegisterCustomTool(t, True)
        ///     End Sub
        ///     <ComUnregisterFunction> _
        ///     Private Shared Sub ComRegister(t As Type)
        ///         If t.Equals(GetType(MyCustomTool)) Then RegisterCustomTool(t, True)
        ///     End Sub
        /// End Class
        /// ]]></code>
        /// </example>
        public static void RegisterCustomTool(Type ToolClass, bool Register) {
            if (ToolClass == null) throw new ArgumentNullException("ToolClass");
            var guidAttribute = ToolClass.GetAttribute<GuidAttribute>(false);
            var toolAttribute = ToolClass.GetAttribute<CustomToolAttribute>(false);
            if (guidAttribute == null) throw new ArgumentException(string.Format(ResourcesT.ExceptionsVsCs.CustomToolClassMustHave0Applied, typeof(GuidAttribute).Name));
            if (guidAttribute == null) throw new ArgumentException(ResourcesT.ExceptionsVsCs.CustomToolClassMustHave0Applied, typeof(CustomToolAttribute).Name);
            if (!typeof(CustomToolBase).IsAssignableFrom(ToolClass)) throw new TypeMismatchException(ResourcesT.ExceptionsVsCs.CustomToolMustInheritFromCustomToolBase);
            CustomToolBase ct = (CustomToolBase)Activator.CreateInstance(ToolClass);
            RegisterCustomTool(new Guid(guidAttribute.Value), toolAttribute, ct.GetVisualStudioVersions(), ct.GetLanguages(), Register);
        }
        /// <summary>Registers or unregisters Visual Studio custom tool identified by <see cref="Guid"/>, its attributes, Visual Studio versions and <see cref="Guid"/> of languages.</summary>
        /// <param name="Guid">Guid of custom tool class</param>
        /// <param name="attribute"><see cref="CustomToolAttribute"/> providing custom tool name, desription and flags.</param>
        /// <param name="VSVersions">Versions of Visual Studio to register/unregister tool for (those are names of keys under HKLM\Software\Microsoft\VisualStudio)</param>
        /// <param name="Languages"><see cref="Guid">Guids</see> of languages to register/unregister tool for (those are names of key unser HKLM\Software\Microsoft\VisualStudio\&lt;version>\Generators)</param>
        /// <param name="Register">True to register the tool, false to unregister it</param>
        /// <exception cref="ArgumentException"><paramref name="Guid"/> is <see cref="Guid.Empty"/>, name of custom tool cases that name of registry sub-key is longer tham maximum allowed lenght (255 chars)</exception>
        /// <exception cref="ArgumentNullException"><paramref name="attribute"/>, <paramref name="VSVersions"/> or <paramref name="Languages"/> is null</exception>
        /// <exception cref="System.Security.SecurityException">The user does not have the permissions required to create or open the registry key HKLM\Software\Microsoft\VisualStudio\&lt;version>\Generators\&lt;language>\&lt;name></exception>
        /// <exception cref="System.UnauthorizedAccessException">Cannot write to registry key (The user does not have the necessary access rights.)</exception>
        public static void RegisterCustomTool(Guid Guid, CustomToolAttribute attribute, string[] VSVersions, Guid[] Languages, bool Register) {
            if (Guid == Guid.Empty) throw new ArgumentException(ResourcesT.ExceptionsVsCs.GuidCannotBeEmpty, "Guid");
            if (attribute == null) throw new ArgumentNullException("attribute");
            if (VSVersions == null) throw new ArgumentNullException("VSVersions");
            if (Languages == null) throw new ArgumentNullException("Languages");
            foreach (string vsVer in VSVersions)
                foreach (Guid Lang in Languages) {
                    string keyName = string.Format("Software\\Microsoft\\VisualStudio\\{0}\\Generators\\{1:B}\\{2}", vsVer, Lang, attribute.Name);
                    if (Register)
                        using (RegistryKey key = Registry.LocalMachine.CreateSubKey(keyName)) {
                            key.SetValue("", attribute.Description);
                            key.SetValue("CLSID", Guid.ToString("B"));
                            key.SetValue("GeneratesDesignTimeSource", attribute.GeneratesDesignTimeSource ? 1 : 0, RegistryValueKind.DWord);
                            if (attribute.GeneratesDesignTimeSharedSource)
                                key.SetValue("GeneratesDesignTimeSharedSource", 1, RegistryValueKind.DWord);
                        } else//Unregister
                        Registry.LocalMachine.DeleteSubKey(keyName);
                }
        }
        /*
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
             object[] attributes = t.GetCustomAttributes(attributeType, /* inherit * / true);
             if(attributes.Length == 0)
                 throw new ArgumentException(String.Format(Tools.ResourcesT.ExceptionsVsCs.Class0DoesNotProvideA1Attribute, t.FullName, attributeType.FullName));
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
         } */

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


        #region Custom tool definition
        /// <summary>When overriden in derived class, gets array of <see cref="Guid">Guids</see> identifying programming languages this custom tool should be registered for.</summary>
        /// <returns>Guids of languages under HKLM\Software\Microsoft\VisualStudio\&lt;version>\Generators <see cref="RegisterCustomTool(System.Type,bool)"/> will register/unregister this tool for. This implementation returns <see cref="CSharpCategoryGuid"/> and <see cref="VBCategoryGuid"/>.</returns>
        /// <seealso cref="RegisterCustomTool(System.Type,bool)"/>
        /// <seealso cref="GetVisualStudioVersions"/>
        /// <seealso cref="CSharpCategoryGuid"/>
        /// <seealso cref="VBCategoryGuid"/>
        /// <seealso cref="JSharpCategoryGuid"/>
        /// <seealso cref="CPPCategoryGuid"/>
        /// <seealso cref="PhalangerCategoryGuid"/>
        /// <seealso cref="PythonCategoryGuid"/>
        public virtual Guid[] GetLanguages() {
            return new Guid[] { CSharpCategoryGuid, VBCategoryGuid };
        }
        /// <summary>When overriden in derived class, gets array of srings identifying versions of Visual Studio this custom tool will be registered for</summary>
        /// <returns>String representing versions of Visual Stuido and keys under HKLM\Software\Microsoft\VisualStudio <see cref="RegisterCustomTool(System.Type,bool)"/> will register/unregister this tool for. This implementation returns "8.0", "9.0" and "10.0".</returns>
        /// <seealso cref="RegisterCustomTool(System.Type,bool)"/>
        /// <seealso cref="GetLanguages()"/>
        public virtual string[] GetVisualStudioVersions() {
            return new string[] { "8.0", "9.0", "10.0" };
        }
        #endregion

        #region Tools enumeration
        /// <summary>Gets all languages which have custom tools in given Visual Studio version (from Software\Microsoft\VisualStudio\&lt;version>\Generators)</summary>
        /// <param name="version">Version of visual studio to get languages for. Typical values are 8.0, 9.0, 10.0</param>
        /// <returns>Array of objects representing languages</returns>
        /// <exception cref="ArgumentNullException"><paramref name="version"/> is null</exception>
        /// <exception cref="ArgumentException"><paramref name="version"/> is too long (so total lenght of registry key name exceds 255 characters)</exception>
        /// <exception cref="System.Security.SecurityException">The user has not permission required for Software\Microsoft\VisualStudio\&lt;version>\Generators registry key</exception>
        /// <exception cref="System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
        /// <exception cref="System.IO.IOException">A system error occurred, for example the current key has been deleted.</exception>
        public static VisualStudioCustomToolLanguage[] GetLanguages(string version) {
            if (version == null) throw new ArgumentNullException("version");
            string kName = string.Format("Software\\Microsoft\\VisualStudio\\{0}\\Generators", version);
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(kName)) {
                return (from string kn in key.GetSubKeyNames() select new VisualStudioCustomToolLanguage("HKEY_LOCAL_MACHINE\\" + kName + "\\" + kn)).ToArray();
            }
        }
        #endregion
    }

    /// <summary>Provides information about programming language for which Custom Tools in Visual Studio can be registred</summary>
    /// <version version="1.5.2">Moved to namespace Tools.VisualStudioT.GeneratorsT (from no namespace)</version>
    public sealed class VisualStudioCustomToolLanguage {
        /// <summary>Contains value of the <see cref="KeyPath"/> property</summary>
        private readonly string keyPath;
        /// <summary>CTor</summary>
        /// <param name="keyPath">Full (startring with base key) path of registry key in which the language is defined.</param>
        /// <exception cref="FormatException">Last segment of path does not represent valid <see cref="System.Guid"/></exception>
        /// <exception cref="OverflowException">Last segment of path is invalid <see cref="System.Guid"/></exception>
        /// <exception cref="ArgumentNullException"><paramref name="keypath"/> is null</exception>
        public VisualStudioCustomToolLanguage(string keyPath) {
            if (keyPath == null) throw new ArgumentNullException("keyPath");
            this.keyPath = keyPath;
            var g = this.Guid;
        }
        /// <summary>Gets full path of registry key this language is defined in</summary>
        public string KeyPath { get { return keyPath; } }
        /// <summary>Gets all the custom tools registred for language represented by current instance</summary>
        /// <returns>Array of objects representing custom tools registered for programming language represented by current instance.</returns>
        /// <exception cref="System.Security.SecurityException">The user has not permission required for Software\Microsoft\VisualStudio\&lt;version>\Generators\&lt;laguage> registry key</exception>
        /// <exception cref="System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
        /// <exception cref="System.IO.IOException">A system error occurred, for example the current key has been deleted.</exception>
        /// <exception cref="InvalidOperationException">Registry operation erro caused by invalid registry key name ocured. (This is caused by <see cref="ArgumentException"/> thrown by registry operation. See <see cref="Exception.InnerException"/>.)</exception>
        public VisualStudioCustomToolRegistration[] GetCustomTools() {
            try {
                using (RegistryKey key = Tools.RegistryT.RegistryT.OpenKey(keyPath, false)) {
                    return (from string kn in key.GetSubKeyNames() select new VisualStudioCustomToolRegistration(keyPath + "\\" + kn)).ToArray();
                }
            } catch (ArgumentException ex) {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }
        /// <summary>Gets <see cref="System.Guid"/> of language represented by this instance.</summary>
        public Guid Guid {
            get {
                string[] parts = keyPath.Split(new char[] { '\\' });
                return new Guid(parts[parts.GetUpperBound(0)]);
            }
        }
        /// <summary>Gets name of current language</summary>
        /// <returns>Name of the language or null if it cannot be obtained</returns>
        /// <remarks>Name is obtained from registry key ..\..\InstalledProducts (relative to <see cref="KeyPath"/>) as name of the sub-key with CLSID corresponding  to <see cref="Guid"/></remarks>
        /// <exception cref="System.Security.SecurityException">The user has not permission required for used registry key</exception>
        /// <exception cref="System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
        /// <exception cref="System.IO.IOException">A system error occurred, for example the current key has been deleted.</exception>
        /// <exception cref="InvalidOperationException">Registry operation error caused by invalid registry key name ocured. (This is caused by <see cref="ArgumentException"/> thrown by registry operation. See <see cref="Exception.InnerException"/>.)</exception>
        public string Name {
            get {
                string[] parts = keyPath.Split(new char[] { '\\' });
                if (parts.Length < 2) return null;
                try {
                    using (RegistryKey InstalledProducts = Tools.RegistryT.RegistryT.OpenKey(string.Join("\\", parts, 0, parts.Length - 2) + "\\InstalledProducts", false))
                        foreach (string SubKeyName in InstalledProducts.GetSubKeyNames())
                            using (RegistryKey SubKey = InstalledProducts.OpenSubKey(SubKeyName)) {
                                object Package = SubKey.GetValue("Package");
                                if (Package != null && Package.ToString().ToLower() == Guid.ToString("B").ToLower())
                                    return SubKeyName;
                            }
                } catch (ArgumentException ex) {
                    throw new InvalidOperationException(ex.Message, ex.InnerException);
                }
                return null;
            }
        }
    }
    /// <summary>Provides imnformation about registered Visual Studio cudtom tool (code generator)</summary>
    /// <version version="1.5.2">Moved to namespace Tools.VisualStudioT.GeneratorsT (from no namespace)</version>
    public sealed class VisualStudioCustomToolRegistration {
        /// <summary>Contains value of the <see cref="KeyPath"/> property</summary>
        private readonly string keyPath;
        /// <summary>CTor</summary>
        /// <param name="keyPath">Full (including name of base key) path of registry key that stores information for this custom tool. This is typically HKEY_LOCAL_MACHINE\Software\Microsoft\VisualStudio\&lt;version>Generators\&lt;language guid>\&lt;name></param>
        /// <exception cref="ArgumentNullException"><paramref name="keyPath"/> is null</exception>
        public VisualStudioCustomToolRegistration(string keyPath) {
            if (keyPath == null) throw new ArgumentNullException("keyPath");
            this.keyPath = keyPath;
        }
        /// <summary>Gets full path of registry key which stores information about this custom tool</summary>
        public string KeyPath { get { return keyPath; } }
        /// <summary>Gets obejct representing programming language this custom tool is registered for</summary>
        /// <returns><see cref="VisualStudioCustomToolLanguage"/>; null when <see cref="KeyPath"/> has less than 2 segments</returns>
        /// <remarks>Programming language is about to be represented by parent key of <see cref="KeyPath"/></remarks>
        /// <exception cref="FormatException">Pre-last segment of path does not represent valid <see cref="System.Guid"/></exception>
        /// <exception cref="OverflowException">Pre-last segment of path is invalid <see cref="System.Guid"/></exception>
        public VisualStudioCustomToolLanguage Language {
            get {
                string[] parts = keyPath.Split(new char[] { '\\' });
                if (parts.Length < 2) return null;
                return new VisualStudioCustomToolLanguage(string.Join("\\", parts, 0, parts.Length - 1));
            }
        }
        /// <summary>Gets name of custom tool</summary>
        /// <returns>Last segment of <see cref="KeyPath"/></returns>
        public string Name {
            get {
                string[] parts = keyPath.Split(new char[] { '\\' });
                return parts[parts.GetUpperBound(0)];
            }
        }
        /// <summary>Gets description of custom tool</summary>
        /// <returns>Value of default value of key this custom tool is represented by</returns>
        /// <exception cref="System.Security.SecurityException">The user has not permission required for used registry key</exception>
        /// <exception cref="System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
        /// <exception cref="System.IO.IOException">A system error occurred, for example the current key has been deleted.</exception>
        /// <exception cref="InvalidOperationException">Registry operation error caused by invalid registry key name ocured. (This is caused by <see cref="ArgumentException"/> thrown by registry operation. See <see cref="Exception.InnerException"/>.)</exception>
        public string Description {
            get {
                try {
                    using (RegistryKey key = Tools.RegistryT.RegistryT.OpenKey(keyPath, false))
                        return key.GetValue("").ToString();
                } catch (ArgumentException ex) {
                    throw new InvalidOperationException(ex.Message, ex);
                }
            }
        }
        /// <summary>Gets <see cref="System.Guid"/> under which is registered COM class representing this custom tool</summary>
        /// <returns><see cref="System.Guid"/> constructed form value of value CLSID of key <see cref="KeyPath"/></returns>
        /// <exception cref="System.Security.SecurityException">The user has not permission required for used registry key</exception>
        /// <exception cref="System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
        /// <exception cref="System.IO.IOException">A system error occurred, for example the current key has been deleted.</exception>
        /// <exception cref="InvalidOperationException">Registry operation error caused by invalid registry key name ocured. (This is caused by <see cref="ArgumentException"/> thrown by registry operation. See <see cref="Exception.InnerException"/>.)</exception>
        public Guid ClsId {
            get {
                try {
                    using (RegistryKey key = Tools.RegistryT.RegistryT.OpenKey(keyPath, false)) {
                        object value = key.GetValue("CLSID");
                        if (value == null) return Guid.Empty;
                        return new Guid(value.ToString());
                    }
                } catch (ArgumentException ex) {
                    throw new InvalidOperationException(ex.Message, ex);
                }
            }
        }
        /// <summary>Attempts to create instance of class registered for this custom tool</summary>
        /// <returns>Instance of class representing the custom tool. The instance typically implements <see cref="Microsoft.VisualStudio.Shell.Interop.IVsSingleFileGenerator"/> COM interface, but it is possible that the actual interface implemented by the object is not exactly the same as <see cref="Microsoft.VisualStudio.Shell.Interop.IVsSingleFileGenerator"/>. It may for example implement its own version of the interface which is equivalent of <see cref="Microsoft.VisualStudio.Shell.Interop.IVsSingleFileGenerator"/> at COM-level, but not at .NET-level.</returns>
        /// <exception cref="System.Security.SecurityException">The user has not permission required for used registry key</exception>
        /// <exception cref="System.UnauthorizedAccessException">The user does not have the necessary registry rights.</exception>
        /// <exception cref="System.IO.IOException">A system error occurred, for example the current key has been deleted.</exception>
        /// <exception cref="InvalidOperationException">Registry operation error caused by invalid registry key name ocured. (This is caused by <see cref="ArgumentException"/> thrown by registry operation. See <see cref="Exception.InnerException"/>.)</exception>
        /// <exception cref="TypeLoadException">An exception was thrown by <see cref="Type.GetTypeFromCLSID(System.Guid,System.Boolean)"/> (with <paramref name="throwOnError"/> set to true). See <see cref="Exception.InnerException"/> for details.</exception>
        /// <exception cref="System.Reflection.TargetInvocationException">The constructor of custom tool class being called throws an exception. See <see cref="Exception.InnerException"/> for details.</exception>
        /// <exception cref="System.MethodAccessException">The caller does not have permission to call custom tool class constructor.</exception>
        /// <exception cref="System.MemberAccessException">Custom tool class was abstract .NET class</exception>
        /// <exception cref="System.MissingMethodException">No default public constructor of custom tool class was found.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Custom tool class type is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
        /// <remarks>Typically you will be able to obtain instances only of .NET custom tools (i.e. those you've developped by deriving from this class)</remarks>
        public object CreateInstance() {
            Type cgType;
            try {
                cgType = Type.GetTypeFromCLSID(this.ClsId, true);
            } catch (Exception ex) {
                throw new TypeLoadException(ex.Message, ex);
            }
            return Activator.CreateInstance(cgType);
        }
    }
}
#endif