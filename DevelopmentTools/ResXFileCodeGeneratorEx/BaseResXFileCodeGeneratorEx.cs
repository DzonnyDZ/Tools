using System;
using System.CodeDom;
using System.Reflection;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

using Microsoft.Win32;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace Tools.VisualStudioT.GeneratorsT.ResXFileGenerator {
    /// <summary>Common base class for RESX file code generators</summary>
    /// <version version="1.5.3">This class is new in version 1.5.3 It was moved from namespace <c>DMKSoftware.CodeGenerators</c>.</version>
    /// <version version="1.5.3">Base class chaged to <see cref="Tools.VisualStudioT.GeneratorsT.BaseCodeGeneratorWithSite"/></version>
    /// <version version="1.5.3">When &lt;LogicalName> is specified in project file for RESX file, class name is obtained from logical name rather than from file name of RESX file.</version>
    /// <version version="1.5.3">Modules are generated for Visual Basic. For other languages class constructor is made <see langword="private"/> (previously <see langword="internal"/>).</version>
    /// <version version="1.5.3">Members of resource class (module for VB) are always <see langword="public"/>, even when the class (module) is <see langword="internal"/>.</version>
    public abstract class BaseResXFileCodeGeneratorEx : Tools.VisualStudioT.GeneratorsT.BaseCodeGeneratorWithSite , IVsRefactorNotify {
        /// <summary>Known Visual Studio versions</summary>
        private enum VsNetVersion {
            /// <summary>Version 8.0 (2005)</summary>
            [Description("8.0")]
            V80,
            /// <summary>Version 9.0 (2008)</summary>
            [Description("9.0")]
            V90,
            /// <summary>Version 10.0 (2010)</summary>
            [Description("10.0")]
            V100,
            /// <summary>Version 11.0 (2012)</summary>
            [Description("11.0")]
            V110
        }

        /// <summary>Language GUIDs</summary>
        private enum LanguageSpecifier {
            /// <summary>C# language</summary>
            [LanguageGuid("{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}")]
            CSharp,
            /// <summary>Visual Basic language</summary>
            [LanguageGuid("{164B10B9-B200-11D0-8C61-00A0C91E29D5}")]
            VB,
            /// <summary>J# language</summary>
            [LanguageGuid("{E6FDF8B0-F3D1-11D4-8576-0002A516ECE8}")]
            JSharp
        }

        /// <summary>Default extension for designer-generated file</summary>
        private const string DesignerExtension = ".Designer";

        private IVsRefactorNotify refactorNotifyImplementer;
        private static readonly Guid resourceEditorRefactorNotifyServiceGuid= new Guid("0407F754-C199-403e-B89B-1D8E1FF3DC79");          
        private static Guid codeDomInterfaceGuid = new Guid("{73E59688-C7C4-4a85-AF64-A538754784C5}");
        private static Guid codeDomServiceGuid = codeDomInterfaceGuid;

        /// <summary>When implemented in derived class gets the boolean flag indicating whether the internal class is generated.</summary>
        /// <returns>True when internal (friend in VB) class is generated, false if generated class is public</returns>
        protected abstract bool GenerateInternalClass { get; }

        
        protected override byte[] GenerateCode(string inputFileName, string inputFileContent) {
            CodeCompileUnit codeCompileUnit;
            if (string.IsNullOrEmpty(inputFileContent) || IsLocalizedFile(inputFileName)) {
                if (CodeGeneratorProgress != null)
                    NativeMethods.ThrowOnFailure(CodeGeneratorProgress.Progress(100, 100));

                return null;
            }

            StreamWriter streamWriter = new StreamWriter(new MemoryStream(), Encoding.UTF8);
            IDictionary nodeDictionary = new Hashtable(StringComparer.OrdinalIgnoreCase);
            IDictionary nodePositionDictionary = new Hashtable(StringComparer.OrdinalIgnoreCase);
            using (IResourceReader resourceReader = ResXResourceReader.FromFileContents(inputFileContent)) {
                ResXResourceReader resXResourceReader = resourceReader as ResXResourceReader;
                if (resXResourceReader != null) {
                    resXResourceReader.UseResXDataNodes = true;
                    string inputDirectoryName = Path.GetDirectoryName(inputFileName);
                    resXResourceReader.BasePath = Path.GetFullPath(inputDirectoryName);
                }
                foreach (DictionaryEntry resourceEntry in resourceReader) {
                    ResXDataNode resXNode = (ResXDataNode)resourceEntry.Value;
                    nodeDictionary.Add(resourceEntry.Key, resXNode);
                    nodePositionDictionary.Add(resourceEntry.Key, resXNode.GetNodePosition());
                }
            }

            string resourceNamespace = this.GetResourcesNamespace(false);
            string logicalName = this.GetResourcesNamespace(true);//Logical name added by Ðonny
            string inputFileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputFileName);
            string className = inputFileNameWithoutExtension;
            //className from logical name added by Ðonny:
            GetClassNameFromLogicalName(ref className);


            List<ResourceErrorData> unmatchableResources = new List<ResourceErrorData>();
            if (resourceNamespace != null)
                codeCompileUnit = StronglyTypedResourceBuilderEx.Create(this.GetType(),
                    nodeDictionary, className, FileNamespace, resourceNamespace, //className added by Ðonny
                    this.CodeProvider, GenerateInternalClass, unmatchableResources, logicalName);//Logical name added by Ðonny
            else
                codeCompileUnit = StronglyTypedResourceBuilderEx.Create(this.GetType(),
                    nodeDictionary, className, base.FileNamespace, this.CodeProvider, //className added by Ðonny
                    GenerateInternalClass, unmatchableResources, logicalName);//Logical name added by Ðonny

            if (base.CodeGeneratorProgress != null) {
                foreach (var resourceErrorData in unmatchableResources) {
                    Point nodePosition = (Point)nodePositionDictionary[resourceErrorData.ResourceKey];
                    base.CodeGeneratorProgress.GeneratorError(1, 1, resourceErrorData.ErrorString,
                        (uint)nodePosition.Y, (uint)nodePosition.X);
                }

                base.CodeGeneratorProgress.Progress(70, 100);
            }

            this.HandleCodeCompileUnit(codeCompileUnit);

            if (base.CodeGeneratorProgress != null)
                base.CodeGeneratorProgress.Progress(0x4b, 100);

            ICodeGenerator codeGenerator = this.CodeProvider.CreateGenerator();
            if (BeforeGenerateText != null) BeforeGenerateText(codeGenerator);
            codeGenerator.GenerateCodeFromCompileUnit(codeCompileUnit, streamWriter, null);
            if (base.CodeGeneratorProgress != null)
                NativeMethods.ThrowOnFailure(base.CodeGeneratorProgress.Progress(100, 100));

            streamWriter.Flush();

            return StreamToBytes(streamWriter.BaseStream);
        }

        /// <summary>Gets name of resources class from resource logical name</summary>
        /// <param name="className">Original name of class. When this method exists this parameter is set to class name parsed from file logical name. The parameter is unchanged when logical name is not set.</param>
        private void GetClassNameFromLogicalName(ref string className) {
            try {
                IntPtr siteInterfacePointer;
                Guid vsBrowseObjectGuid = typeof(IVsBrowseObject).GUID;
                GetSite(ref vsBrowseObjectGuid, out siteInterfacePointer);
                if (IntPtr.Zero != siteInterfacePointer) {
                    IVsHierarchy vsHierarchy;
                    uint pItemId;
                    IVsBrowseObject vsBrowseObject = Marshal.GetObjectForIUnknown(siteInterfacePointer) as IVsBrowseObject;

                    vsBrowseObject.GetProjectItem(out vsHierarchy, out pItemId);
                    IVsBuildPropertyStorage buildPropertyStorage = vsHierarchy as IVsBuildPropertyStorage;
                    if (buildPropertyStorage != null) {
                        string LogicalName = null;
                        try {
                            buildPropertyStorage.GetItemAttribute(pItemId, "LogicalName", out LogicalName);
                        } catch { }
                        if (LogicalName != null) {
                            if (LogicalName.EndsWith(".resources"))
                                LogicalName = LogicalName.Substring(0, LogicalName.Length - ".resources".Length);
                            if (LogicalName.Contains(".")) LogicalName = LogicalName.Substring(LogicalName.LastIndexOf('.') + 1);
                            className = LogicalName;
                        }
                    }
                }
            }catch{}
        }

        /// <summary>Use when this tool is used from outside of VS to set resource namespace. When null resource namespace is inferred standard way.</summary>
        public string ResourceNamespace { get; set; }//Added by Ðonny

        /// <summary>Fired before <see cref="ICodeGenerator"/> is used to generate actual text</summary>
        public event System.Action<ICodeGenerator> BeforeGenerateText;//Added by Ðonny

       
        /// <summary>Performs additional operations with <see cref="CodeCompileUnit"/></summary>
        /// <param name="ccu">A <see cref="CodeCompileUnit"/> to be processed</param>
        protected virtual void HandleCodeCompileUnit(CodeCompileUnit ccu) { }

        private static bool IsLocalizedFile(string fileName) {
            if (!string.IsNullOrEmpty(fileName) && fileName.EndsWith(".resx", true, CultureInfo.InvariantCulture)) {
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
                if (!string.IsNullOrEmpty(fileNameWithoutExtension)) {
                    int lastDotIndex = fileNameWithoutExtension.LastIndexOf('.');
                    if (lastDotIndex > 0) {
                        string cultureName = fileNameWithoutExtension.Substring(lastDotIndex + 1);
                        if (!string.IsNullOrEmpty(cultureName)) {
                            try {
                                if (new CultureInfo(cultureName) != null) {
                                    return true;
                                }
                            } catch (ArgumentException) {
                            }
                        }
                    }
                }
            }

            return false;
        }
#region IVsRefactorNotify
        int IVsRefactorNotify.OnAddParams(IVsHierarchy phier, uint itemId, string lpszRQName, uint cParams, uint[] rgszParamIndexes, string[] rgszRQTypeNames, string[] rgszParamNames) {
            if (null != RefactorNotifyImplementer)
                return RefactorNotifyImplementer.OnAddParams(phier, itemId, lpszRQName, cParams,
                    rgszParamIndexes, rgszRQTypeNames, rgszParamNames);

            return -2147418113;
        }

        int IVsRefactorNotify.OnBeforeAddParams(IVsHierarchy phier, uint itemId, string lpszRQName, uint cParams, uint[] rgszParamIndexes, string[] rgszRQTypeNames, string[] rgszParamNames, out Array prgAdditionalCheckoutVSITEMIDS) {
            if (null != RefactorNotifyImplementer)
                return RefactorNotifyImplementer.OnBeforeAddParams(phier, itemId, lpszRQName, cParams,
                    rgszParamIndexes, rgszRQTypeNames, rgszParamNames, out prgAdditionalCheckoutVSITEMIDS);

            prgAdditionalCheckoutVSITEMIDS = null;

            return -2147418113;
        }

        int IVsRefactorNotify.OnBeforeGlobalSymbolRenamed(IVsHierarchy phier, uint itemId, uint cRQNames, string[] rglpszRQName, string lpszNewName, out Array prgAdditionalCheckoutVSITEMIDS) {
            if (null != RefactorNotifyImplementer)
                return RefactorNotifyImplementer.OnBeforeGlobalSymbolRenamed(phier, itemId, cRQNames,
                    rglpszRQName, lpszNewName, out prgAdditionalCheckoutVSITEMIDS);

            prgAdditionalCheckoutVSITEMIDS = null;

            return -2147418113;
        }

        int IVsRefactorNotify.OnBeforeRemoveParams(IVsHierarchy phier, uint itemId, string lpszRQName, uint cParamIndexes, uint[] rgParamIndexes, out Array prgAdditionalCheckoutVSITEMIDS) {
            if (null != RefactorNotifyImplementer)
                return RefactorNotifyImplementer.OnBeforeRemoveParams(phier, itemId, lpszRQName,
                    cParamIndexes, rgParamIndexes, out prgAdditionalCheckoutVSITEMIDS);

            prgAdditionalCheckoutVSITEMIDS = null;

            return -2147418113;
        }

        int IVsRefactorNotify.OnBeforeReorderParams(IVsHierarchy phier, uint itemId, string lpszRQName, uint cParamIndexes, uint[] rgParamIndexes, out Array prgAdditionalCheckoutVSITEMIDS) {
            if (null != RefactorNotifyImplementer)
                return RefactorNotifyImplementer.OnBeforeReorderParams(phier, itemId, lpszRQName,
                    cParamIndexes, rgParamIndexes, out prgAdditionalCheckoutVSITEMIDS);

            prgAdditionalCheckoutVSITEMIDS = null;

            return -2147418113;
        }

        int IVsRefactorNotify.OnGlobalSymbolRenamed(IVsHierarchy phier, uint itemId, uint cRQNames, string[] rglpszRQName, string lpszNewName) {
            if (null != RefactorNotifyImplementer)
                return RefactorNotifyImplementer.OnGlobalSymbolRenamed(phier, itemId, cRQNames,
                    rglpszRQName, lpszNewName);

            return -2147418113;
        }

        int IVsRefactorNotify.OnRemoveParams(IVsHierarchy phier, uint itemId, string lpszRQName, uint cParamIndexes, uint[] rgParamIndexes) {
            if (null != RefactorNotifyImplementer)
                return RefactorNotifyImplementer.OnRemoveParams(phier, itemId, lpszRQName,
                    cParamIndexes, rgParamIndexes);

            return -2147418113;
        }

        int IVsRefactorNotify.OnReorderParams(IVsHierarchy phier, uint itemId, string lpszRQName, uint cParamIndexes, uint[] rgParamIndexes) {
            if (null != RefactorNotifyImplementer)
                return RefactorNotifyImplementer.OnReorderParams(phier, itemId, lpszRQName,
                    cParamIndexes, rgParamIndexes);

            return -2147418113;
        }

        private IVsRefactorNotify RefactorNotifyImplementer {
            get {
                if (null == refactorNotifyImplementer) {
                    var serviceProviderInterface = Package.GetGlobalService(typeof(Microsoft.VisualStudio.OLE.Interop.IServiceProvider)) as Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
                    if (null != serviceProviderInterface) {
                        ServiceProvider serviceProvider = new ServiceProvider(serviceProviderInterface);
                        refactorNotifyImplementer = serviceProvider.GetService(resourceEditorRefactorNotifyServiceGuid) as IVsRefactorNotify;
                    }
                }

                return refactorNotifyImplementer;
            }
        }
#endregion

#region COM Registration

        private static string GetVisualStugioRegistryKeyPath(VsNetVersion vsNetVersion) {
            StringBuilder registryKeyPathBuilder = new StringBuilder(100);

            FieldInfo fieldInfo = vsNetVersion.GetType().GetField(vsNetVersion.ToString());

            DescriptionAttribute descriptionAttribute = Attribute.GetCustomAttribute(fieldInfo,
                typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (null == descriptionAttribute)
                throw new InvalidOperationException(string.Format("The Description attribute is not applied to the VsNetVersion.{0} enumeration value.",
                    vsNetVersion.ToString()));

            registryKeyPathBuilder.AppendFormat(CultureInfo.InvariantCulture,
                @"SOFTWARE\Microsoft\VisualStudio\{0}", descriptionAttribute.Description);

            return registryKeyPathBuilder.ToString();
        }

        private static Guid GetLanguageGuid(LanguageSpecifier languageSpecifier) {
            FieldInfo fieldInfo = languageSpecifier.GetType().GetField(languageSpecifier.ToString());

            LanguageGuidAttribute languageGuidAttribute = Attribute.GetCustomAttribute(fieldInfo,
                typeof(LanguageGuidAttribute)) as LanguageGuidAttribute;
            if (null == languageGuidAttribute)
                throw new InvalidOperationException(string.Format("The LanguageGuid attribute is not applied to the LanguageSpecifier.{0} enumeration value.",
                    languageSpecifier.ToString()));

            return languageGuidAttribute.Guid;
        }

        private static IEnumerable<Type> ExtendedGeneratorTypes {
            get {
                Type baseGeneratorType = typeof(BaseResXFileCodeGeneratorEx);

                Type[] types = baseGeneratorType.Assembly.GetTypes();
                foreach (Type type in types) {
                    if (type.IsSubclassOf(baseGeneratorType))
                        yield return type;
                }
            }
        }

        private static string GetTypeDescription(Type type) {
            if (null == type)
                throw new ArgumentNullException("type");

            DescriptionAttribute descriptionAttribute = Attribute.GetCustomAttribute(type,
                typeof(DescriptionAttribute)) as DescriptionAttribute;

            return (null == descriptionAttribute) ? string.Empty : descriptionAttribute.Description;
        }

        /// <summary>Performs additional actions when aa type is registered for COM interop</summary>
        /// <param name="type">Type being registered</param>
        [ComRegisterFunction]
        public static void ComRegisterFunction(Type type) {
            VsNetVersion[] vsNetVersions = (VsNetVersion[])Enum.GetValues(typeof(VsNetVersion));
            foreach (Type generatorType in ExtendedGeneratorTypes) {
                foreach (VsNetVersion vsNetVersion in vsNetVersions) {
                    string vsNetRegistryKeyPath = GetVisualStugioRegistryKeyPath(vsNetVersion);

                    using (RegistryKey vsNetRegistryKey = Registry.LocalMachine.OpenSubKey(vsNetRegistryKeyPath,
                        RegistryKeyPermissionCheck.ReadWriteSubTree)) {
                        if (null == vsNetRegistryKey)
                            continue;

                        LanguageSpecifier[] languageSpecifiers = (LanguageSpecifier[])Enum.GetValues(typeof(LanguageSpecifier));
                        foreach (LanguageSpecifier languageSpecifier in languageSpecifiers) {
                            string generatorRegistryKeyPath = string.Format(@"Generators\{0}\{1}",
                                GetLanguageGuid(languageSpecifier).ToString("B"), generatorType.Name);

                            using (RegistryKey generatorRegistryKey = vsNetRegistryKey.CreateSubKey(generatorRegistryKeyPath,
                                RegistryKeyPermissionCheck.ReadWriteSubTree)) {
                                generatorRegistryKey.SetValue("", GetTypeDescription(generatorType));
                                generatorRegistryKey.SetValue("CLSID", generatorType.GUID.ToString("B"));
                                generatorRegistryKey.SetValue("GeneratesDesignTimeSource", 1);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Performs additional actions when aa type is unregistered from COM interop</summary>
        /// <param name="type">Type being unregistered</param>
        [ComUnregisterFunction]
        public static void ComUnregisterFunction(Type type) {
            VsNetVersion[] vsNetVersions = (VsNetVersion[])Enum.GetValues(typeof(VsNetVersion));
            foreach (Type generatorType in ExtendedGeneratorTypes) {
                foreach (VsNetVersion vsNetVersion in vsNetVersions) {
                    LanguageSpecifier[] languageSpecifiers = (LanguageSpecifier[])Enum.GetValues(typeof(LanguageSpecifier));
                    foreach (LanguageSpecifier languageSpecifier in languageSpecifiers) {
                        string generatorRegistryKeyPath = string.Format(@"{0}\Generators\{1}\{2}", GetVisualStugioRegistryKeyPath(vsNetVersion),
                            GetLanguageGuid(languageSpecifier).ToString("B"), generatorType.Name);

                        Registry.LocalMachine.DeleteSubKey(generatorRegistryKeyPath, false);
                    }
                }
            }
        }

#endregion

#region Utility
        /// <summary>Gets namespace of resource</summary>
        /// <param name="forLogicalName">Use logical name; ignored when <see cref="ResourceNamespace"/> is not null</param>
        /// <returns>Resource namespace</returns>
        protected string GetResourcesNamespace(bool forLogicalName)//forLogicalName added by Ðonny
        {
            if (this.ResourceNamespace != null) return ResourceNamespace;
            string resourcesNamespace = null;

            try {
                IntPtr siteInterfacePointer;
                Guid vsBrowseObjectGuid = typeof(IVsBrowseObject).GUID;
                GetSite(ref vsBrowseObjectGuid, out siteInterfacePointer);
                if (IntPtr.Zero != siteInterfacePointer) {
                    IVsHierarchy vsHierarchy;
                    uint pItemId;
                    object propertyValue;
                    IVsBrowseObject vsBrowseObject = Marshal.GetObjectForIUnknown(siteInterfacePointer) as IVsBrowseObject;

                    vsBrowseObject.GetProjectItem(out vsHierarchy, out pItemId);
                    //Added by Ðonny:
                    //Support for <LogicalName>
                    if (forLogicalName) {
                        IVsBuildPropertyStorage buildPropertyStorage = vsHierarchy as IVsBuildPropertyStorage;
                        if (buildPropertyStorage != null) {
                            string LogicalName = null;
                            try {
                                buildPropertyStorage.GetItemAttribute(pItemId, "LogicalName", out LogicalName);
                            } catch { }
                            if (LogicalName != null) {
                                if (LogicalName.EndsWith(".resources"))
                                    return LogicalName.Substring(0, LogicalName.Length - ".resources".Length);
                                else return LogicalName;
                            }
                        }
                    }

                    Marshal.Release(siteInterfacePointer);
                    if (null == vsBrowseObject)
                        return resourcesNamespace;


                    if (null == vsHierarchy)
                        return resourcesNamespace;

                    vsHierarchy.GetProperty(pItemId, -2049, out propertyValue);
                    string propertyText = propertyValue as string;
                    if (null == propertyText)
                        return resourcesNamespace;

                    resourcesNamespace = propertyText;
                }
            } catch (Exception ex) {
                if (ProjectUtilities.IsCriticalException(ex)) {
                    throw;
                }
            }

            return resourcesNamespace;
        }

        private int GetBaseDefaultExtension(out string ext) {
            string defaultExtension = CodeProvider.FileExtension;
            if (((defaultExtension != null) && (defaultExtension.Length > 0)) && (defaultExtension[0] != '.'))
                defaultExtension = "." + defaultExtension;

            ext = defaultExtension;

            return 0;
        }
        /// <summary>gets the default extension for this generator</summary>
        /// <returns>string with the default extension for this generator</returns>
        public override string GetDefaultExtension() {
            string baseExtension;
            string ext = string.Empty;
            int result = GetBaseDefaultExtension(out baseExtension);
            if (!NativeMethods.Succeeded(result))
                throw new System.SystemException();

            if (!string.IsNullOrEmpty(baseExtension))
                ext = DesignerExtension + baseExtension;

            return ext;
        }
   
#endregion
    }
}

