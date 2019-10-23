#region Usage
/* Registration:
 * c:/> regasm /codebase XsdCodeGenerator.CustomTool.dll
 * 
 * Unregistration:
 * c:/> regasm /unregister XsdCodeGenerator.CustomTool
 *
 * Usage:
 * Add an .xsd file to the project and set:
 *  Build Action: Content
 *  Custom Tool: XsdCodeGen
 */
#endregion Usage

#region using
using System;
using System.CodeDom;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Xml.Schema;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.CodeDom.Compiler;
using System.Text;
using System.Collections;
using Microsoft.Win32;  // Registry
#endregion

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator {
    /// <summary>Uses the XsdGeneratorLibrary to process XSD files and generate the corresponding classes.</summary>
    /// <version version="1.5.3">Added support for Visual Studio 2010</version>
    [Guid(XsdCodeGenerator._customToolGuid)]
    [ComVisible(true)]
    public class XsdCodeGenerator : BaseCodeGeneratorWithSite {
        #region Constants

        /// <summary>VS Generator Category for C# Language.</summary>
        private static Guid CSharpCategory = new Guid("{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}");

        /// <summary>VS Generator Category for VB Language.</summary>
        private static Guid VBCategory = new Guid("{164B10B9-B200-11D0-8C61-00A0C91E29D5}");

        /// <summary>String version fo tool GUID.</summary>
        /// <remarks>Use <see cref="CustomToolGuid"/> instead.</remarks>
        /// <version version="1.5.3">This constant is new in version 1.5.3</version>
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal const string _customToolGuid = "9B7CF25A-1782-433b-B534-0B94E76A7D62";
        /// <summary>The tool Guid.</summary>
        private static Guid CustomToolGuid = new Guid(_customToolGuid);

        /// <summary>Name of the custom tool to register.</summary>
        private const string CustomToolName = "XsdCodeGen";

        /// <summary>Description for registration.</summary>
        /// <version version="1.5.3">Changed from constant to literal</version>
        private readonly string CustomToolDescription = Resources.XSDClassesGenerator;

        #endregion Constants

        #region Ctor & Assembly resolution
        /// <summary>Type initializer - initializes the <see cref="XsdCodeGenerator"/> class</summary>
        [Obsolete]
        static XsdCodeGenerator() {
            // Add our path to the private path.
            AppDomain.CurrentDomain.AppendPrivatePath(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>CTor - creates a new instance of the <see cref="XsdCodeGenerator"/> class</summary>
        public XsdCodeGenerator() {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(OnAssemblyResolve);
        }

        /// <summary>
        /// Keeps assemblies we have already loaded and resolved.
        /// </summary>
        static Hashtable _resolved = new Hashtable();

        /// <summary>
        /// Resolves assembly references, pointing them to the current project references.
        /// </summary>
        /// <returns>The resolved assembly or null.</returns>
        private Assembly OnAssemblyResolve(object sender, ResolveEventArgs args) {
            string simplename = args.Name.IndexOf(',') == -1 ?
                args.Name : args.Name.Substring(0, args.Name.IndexOf(','));

            // Check if we haven't resolved this before.
            if (_resolved.ContainsKey(simplename)) return _resolved[simplename] as Assembly;

            EnvDTE.ProjectItem item = GetService(typeof(EnvDTE.ProjectItem))
                as EnvDTE.ProjectItem;

            Assembly asm = null;
            string asmpath = String.Empty;
            string projectname = item.ContainingProject.Properties.Item("AssemblyName").Value.ToString();
            string projectasm = item.ContainingProject.Properties.Item("OutputFileName").Value.ToString();

            if (simplename == projectname) {
                #region Reference is to the same project the item lives in

                // Load from project output folder. 
                string outpath =
                    item.ContainingProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();
                if (!Path.IsPathRooted(outpath)) {
                    outpath = Path.Combine(
                        Path.GetDirectoryName(item.ContainingProject.FullName),
                        outpath);
                }
                asmpath = Path.Combine(outpath, projectasm);
                // asm = Assembly.LoadFrom( asmpath );
                asm = LoadAssemblyFromBytes(asmpath);

                #endregion Reference is to the same project the item lives in
            } else {
                // Try to resolve to loaded assemblies if it's not the project output itself.
                foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies()) {
                    if (a.FullName == args.Name || a.GetName().Name == args.Name) {
                        // Ensure monitoring.
                        MonitorAssembly(a.GetName().CodeBase);
                        return a;
                    }
                }

                // Specifies whether to use full assembly name.
                bool usefull = (args.Name.IndexOf(',') != -1);
                bool islocal = true;

                #region Look for references

                // We only resolve manually those assemblies referenced in the current project.
                foreach (VSLangProj.Reference reference in
                    ((VSLangProj.VSProject)item.ContainingProject.Object).References) {
                    bool found = usefull ?
                        // Full assembly name comparison.
                        AssemblyName.GetAssemblyName(reference.Path).ToString() == args.Name :
                        // Only reference name.
                        reference.Name == args.Name;
                    if (found) {
                        asmpath = reference.Path;
                        islocal = reference.CopyLocal;
                        break;
                    }
                }

                #endregion Look for references

                #region Load the assembly

                // Non-local assemblies. Assume they are GAC'ed
                if (!islocal) {
                    // Full name or partial name?
                    if (usefull)
                        asm = Assembly.Load(args.Name);
                    else
                        asm = Assembly.LoadWithPartialName(args.Name);
                } else {
                    if (asmpath != String.Empty) {
                        // Load from project output folder. CopyLocal references will be there.
                        string outpath =
                            item.ContainingProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();
                        if (!Path.IsPathRooted(outpath)) {
                            outpath = Path.Combine(
                                Path.GetDirectoryName(item.ContainingProject.FullName),
                                outpath);
                        }

                        asm = LoadAssemblyFromBytes(Path.Combine(outpath, Path.GetFileName(asmpath)));
                        // asm = Assembly.LoadFrom( Path.Combine( outpath, Path.GetFileName( asmpath ) ) );
                        // asm = Assembly.LoadFrom( asmpath );
                        // Load the assembly from original location and from raw bytes to avoid locking?						
                    }
                }

                #endregion Load the assembly
            }

            // Cache assembly we found ( or didn't, but we won't try each time! )
            _resolved[simplename] = asm;
            return asm;
        }

        private Assembly LoadAssemblyFromBytes(string path) {
            Assembly asm = null;
            bool debug = File.Exists(Path.ChangeExtension(path, "pdb"));
            byte[] asmbytes = null;
            byte[] pdbbytes = null;

            // Load assembly from in-memory bytes to avoid locking the file.
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                asmbytes = new byte[fs.Length];
                if (fs.Length > int.MaxValue)
                    throw new TypeLoadException(Resources.ex_AssemblyTooBig);
                else
                    fs.Read(asmbytes, 0, (int)fs.Length);
            }

            if (debug) {
                // Load debug symbols.
                using (FileStream fs = new FileStream(Path.ChangeExtension(path, "pdb"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                    pdbbytes = new byte[fs.Length];
                    if (fs.Length > int.MaxValue)
                        throw new TypeLoadException(Resources.ex_DebugSymbolsToooBig);
                    else
                        fs.Read(pdbbytes, 0, (int)fs.Length);
                }
                // Load with symbols.
                asm = Assembly.Load(asmbytes, pdbbytes);
            } else {
                // Load assembly alone.
                asm = Assembly.Load(asmbytes);
            }

            MonitorAssembly(path);
            return asm;
        }

        #endregion Ctor & Assembly resolution

        #region Assembly change monitoring

        Hashtable _monitors = new Hashtable();
        DateTime _lastupdate = DateTime.MinValue;
        string _lastfile = String.Empty;

        /// <summary>We won't refresh changes raised with less that 5 secs. between events.</summary>
        TimeSpan _updateinterval = new TimeSpan(0, 0, 5);

        private void MonitorAssembly(string file) {
            // Uri-like syntax is not pathrooted ( file:// scheme ). Decode to filesystem.
            if (!Path.IsPathRooted(file)) {
                UriBuilder b = new UriBuilder(file);
                file = System.Web.HttpUtility.UrlDecode(b.Path);
            }

            string folder = Path.GetDirectoryName(file);
            // Check if we're already monitoring the folder.
            if (_monitors.ContainsKey(folder)) return;

            ArrayList mon = new ArrayList(2);

            // Monitor DLL assembly changes from no on.
            FileSystemWatcher fsw = new FileSystemWatcher(folder);
            fsw.Filter = "*.dll";
            fsw.NotifyFilter = NotifyFilters.LastWrite;
            fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            fsw.EnableRaisingEvents = true;
            mon.Add(fsw);

            // Monitor EXE assembly changes from no on.
            fsw = new FileSystemWatcher(folder);
            fsw.Filter = "*.exe";
            fsw.NotifyFilter = NotifyFilters.LastWrite;
            fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            fsw.EnableRaisingEvents = true;
            mon.Add(fsw);

            _monitors.Add(folder, mon);
        }

        private void fsw_Changed(object sender, FileSystemEventArgs e) {
            if (_lastfile == e.FullPath && _lastupdate.Add(_updateinterval) > DateTime.Now)
                return;

            try {
                string ext = Path.GetExtension(e.FullPath);

                string simplename = AssemblyName.GetAssemblyName(e.FullPath).Name;
                if (_resolved.ContainsKey(simplename)) {
                    // The change is fired before actual writing. Delay 3''.
                    System.Threading.Thread.Sleep(3000);

                    // Reload it and replace the existing one.
                    Assembly asm = LoadAssemblyFromBytes(e.FullPath);
                    _resolved[simplename] = asm;
                    // TODO: note that we're leaking by leaving the old assembly floating around. 
                    // This is a CLR limitation, in that we can't get rid of it once it has been loaded.
                }
                _lastupdate = DateTime.Now;
                _lastfile = e.FullPath;
            } catch {
                // Swallow the exception as we don't want to interfere with the compiler.
            }
        }

        #endregion Assembly change monitoring

        #region GenerateCode

        /// <summary>Generates the output.</summary>
        /// <param name="inputFileName">Name of file to generate input from</param>
        /// <param name="inputFileContent">Content of file to generate input from</param>
        protected override byte[] GenerateCode(string inputFileName, string inputFileContent) {
            string code = "";
            try {
                // Process the file.
                CodeDomProvider Provider = CodeProvider;
                CodeNamespace ns = Processor.Process(inputFileName, FileNamespace, Provider);

                // Generate code for it.
                CodeGeneratorOptions opt = new CodeGeneratorOptions();
                opt.BracingStyle = "C";
                StringWriter sw = new StringWriter();
                Provider.GenerateCodeFromNamespace(ns, sw, opt);

                // Finaly assign it to the result to return.
                code = sw.ToString();

                Processor.PostProcess(inputFileName, ref code, Provider);
            } catch (Exception e) {
                code = String.Format(Resources.err_CouldNotGenerateCode, e);
            }
            // Convert to bytes.
            return System.Text.Encoding.UTF8.GetBytes(code);
        }

        #endregion GenerateCode

        #region Registration

        /// <summary>Formating string for Visual Studio registry key</summary>
        /// <remarks>Value is HKLM\SOFTWARE\Microsoft\VisualStudio\{Version}\Generators\{C#/VB GUID}\{ToolName}</remarks>
        private const string KeyFormat = @"SOFTWARE\Microsoft\VisualStudio\{0}\Generators\{1}\{2}";

        /// <summary>Registers the tool for a VS version and a language category.</summary>
        /// <param name="vsVersion">Version of Visual Studio to register for</param>
        /// <param name="categoryGuid">Category GUID - represents language to register for</param>
        /// <remarks>
        /// Creates registry key HKLM\SOFTWARE\Microsoft\VisualStudio\{Version}\Generators\{C#/VB GUID}\{ToolName} and 
        /// setrs properties "", CLSID and GeneratesDesignTimeSource
        /// </remarks>
        protected static void Register(Version vsVersion, Guid categoryGuid) {
            string keyPath = String.Format(KeyFormat, vsVersion, categoryGuid.ToString("B"), CustomToolName);
            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(keyPath)) {
                key.SetValue("", Resources.XSDClassesGenerator);
                key.SetValue("CLSID", CustomToolGuid.ToString("B"));
                key.SetValue("GeneratesDesignTimeSource", 1);
                Console.WriteLine(Resources.Registered, "HKLM\\" + keyPath);
            }
        }

        /// <summary>Unregisters the custom tool.</summary>
        /// <param name="vsVersion">Version of visual studio to unregister tool from</param>
        /// <param name="categoryGuid">Category GUID - represents language to unregister from</param>
        protected static void Unregister(Version vsVersion, Guid categoryGuid) {
            string keyPath = String.Format(KeyFormat, vsVersion, categoryGuid.ToString("B"), CustomToolName);
            Registry.LocalMachine.DeleteSubKey(keyPath, false);
            Console.WriteLine(Resources.Unregistered, "HKLM\\" + keyPath);
        }

        /// <summary>Registers the generator.</summary>
        /// <param name="t">ignored</param>
        /// <version version="1.5.3">Registers for VS 2010</version>
        [ComRegisterFunction]
        public static void RegisterClass(Type t) {
            // Register for both VS.NET 2002 & 2003 ( C# )
            //Register(new Version(7, 0), CSharpCategory);
            //Register(new Version(7, 1), CSharpCategory);
            Register(new Version(8, 0), CSharpCategory);
            Register(new Version(9, 0), CSharpCategory);
            Register(new Version(10, 0), CSharpCategory);

            // Register for both VS.NET 2002 & 2003 ( VB )
            //Register(new Version(7, 0), VBCategory);
            //Register(new Version(7, 1), VBCategory);
            Register(new Version(8, 0), VBCategory);
            Register(new Version(9, 0), VBCategory);
            Register(new Version(10, 0), VBCategory);
        }

        /// <summary>Unregisters the generator.</summary>
        /// <param name="t">ignored</param>
        /// <version version="1.5.3">Unregisters from VS 2010</version>
        [ComUnregisterFunction]
        public static void UnregisterClass(Type t) {
            // Unregister for both VS.NET 2002 & 2003 ( C# )
            //Unregister(new Version(7, 0), CSharpCategory);
            //Unregister(new Version(7, 1), CSharpCategory);
            Unregister(new Version(8, 0), CSharpCategory);
            Unregister(new Version(9, 0), CSharpCategory);
            Unregister(new Version(10, 0), CSharpCategory);

            // Unregister for both VS.NET 2002 & 2003 ( VB )
            //Unregister(new Version(7, 0), VBCategory);
            //Unregister(new Version(7, 1), VBCategory);
            Unregister(new Version(8, 0), VBCategory);
            Unregister(new Version(9, 0), VBCategory);
            Unregister(new Version(10, 0), VBCategory);
        }

        #endregion Registration
    }
}