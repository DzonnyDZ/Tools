namespace Tools.VisualStudioT.GeneratorsT {

    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using EnvDTE;
    using VSLangProj;
    using Microsoft.VisualStudio.Designer.Interfaces;
    using Microsoft.VisualStudio.OLE.Interop;

    /// <summary>This class exists to be co-created a in a preprocessor build step.</summary>
    /// <version version="1.5.2">Moved from namespace Tools.GeneratorsT to <see cref="Tools.VisualStudioT.GeneratorsT"/></version>
    /// <version version="1.5.3">Base class changed from <see cref="Tools.VisualStudioT.GeneratorsT.BaseCodeGenerator"/> to <see cref="Microsoft.VisualStudio.TextTemplating.VSHost.BaseCodeGeneratorWithSite"/>. Methods implemented by <see cref="Microsoft.VisualStudio.TextTemplating.VSHost.BaseCodeGeneratorWithSite"/> removed from this class.</version>
    public abstract class BaseCodeGeneratorWithSite : Microsoft.VisualStudio.TextTemplating.VSHost.BaseCodeGeneratorWithSite {
        /// <summary>GUID of DOM interface</summary>
        private static Guid CodeDomInterfaceGuid = new Guid("{73E59688-C7C4-4a85-AF64-A538754784C5}");
        /// <summary>Same as <see cref="CodeDomInterfaceGuid"/></summary>
        private static Guid CodeDomServiceGuid = CodeDomInterfaceGuid;
        /// <summary>Caches value of the <see cref="CodeProvider"/> property</summary>
        private CodeDomProvider codeProvider;
        /// <summary>demand-creates a <see cref="CodeDomProvider"/></summary>
        protected virtual CodeDomProvider CodeProvider {
            get {
                if(codeProvider == null) {
                    IVSMDCodeDomProvider vsmdCodeDomProvider = (IVSMDCodeDomProvider)GetService(CodeDomServiceGuid);
                    if(vsmdCodeDomProvider != null) {
                        codeProvider = (CodeDomProvider)vsmdCodeDomProvider.CodeDomProvider;
                    }
                    Debug.Assert(codeProvider != null, Tools.ResourcesT.ExceptionsVsCs.GetCodeDomProviderInterfaceFailedGetServiceQueryServiceCodeDomProviderReturnedNull);
                }
                return codeProvider;
            }
            set {
                if(value == null) {
                    throw new ArgumentNullException();
                }

                codeProvider = value;
            }
        }
                          
        /// <summary>Gets the default extension of the output file by asking the <see cref="CodeDomProvider"/> what its default extension is.</summary>
        public override string GetDefaultExtension() {
            CodeDomProvider codeDom = CodeProvider;
            Debug.Assert(codeDom != null, Tools.ResourcesT.ExceptionsVsCs.CodeDomProviderIsNULL);
            string extension = codeDom.FileExtension;
            if(extension != null && extension.Length > 0) {
                if(extension[0] != '.') {
                    extension = "." + extension;
                }
            }

            return extension;
        }
       
        /// <summary>Gets a string containing the DLL names to add.</summary>
        /// <param name="DLLToAdd">DLLs to be added</param>
        /// <returns>Names of DLLs</returns>
        private string GetDLLNames(string[] DLLToAdd) {

            if(DLLToAdd == null || DLLToAdd.Length == 0) {
                return string.Empty;
            }

            string dllNames = DLLToAdd[0];
            for(int i = 1; i < DLLToAdd.Length; i++) {
                dllNames = dllNames + ", " + DLLToAdd[i];
            }
            return dllNames;
        }

        /// <summary>Adds a reference to the project for each required DLL</summary>
        /// <param name="referenceDLL">DLLs to add references to</param>
        protected void AddReferenceDLLToProject(string[] referenceDLL) {

            if(referenceDLL.Length == 0) {
                return;
            }

            object serviceObject = GetService(typeof(ProjectItem));
            Debug.Assert(serviceObject != null, Tools.ResourcesT.ExceptionsVsCs.UnableToGetProjectItem);
            if(serviceObject == null) {
                string errorMessage = String.Format(Tools.ResourcesT.ExceptionsVsCs.UnableToAddDLLToProjectReferences0PleaseAddThemManually, GetDLLNames(referenceDLL));
                GeneratorErrorCallback(false, 1, errorMessage, 0, 0);
                return;
            }

            Project containingProject = ((ProjectItem)serviceObject).ContainingProject;
            Debug.Assert(containingProject != null, Tools.ResourcesT.ExceptionsVsCs.GetServiceTypeofProjectReturnNull);
            if(containingProject == null) {
                string errorMessage = String.Format(Tools.ResourcesT.ExceptionsVsCs.UnableToAddDLLToProjectReferences0PleaseAddThemManually, GetDLLNames(referenceDLL));
                GeneratorErrorCallback(false, 1, errorMessage, 0, 0);
                return;
            }

            VSProject vsProj = containingProject.Object as VSProject;
            Debug.Assert(vsProj != null, Tools.ResourcesT.ExceptionsVsCs.UnableToADDDLLToCurrentProjectProjectObjectDoesNotImplementVSProject);
            if(vsProj == null) {
                string errorMessage = String.Format(Tools.ResourcesT.ExceptionsVsCs.UnableToAddDLLToProjectReferences0PleaseAddThemManually, GetDLLNames(referenceDLL));
                GeneratorErrorCallback(false, 1, errorMessage, 0, 0);
                return;
            }

            try {
                for(int i = 0; i < referenceDLL.Length; i++) {
                    vsProj.References.Add(referenceDLL[i]);
                }
            } catch(Exception e) {
                Debug.Fail(Tools.ResourcesT.ExceptionsVsCs.ERRORVsProjReferencesAddThrowsException + e.ToString());

                string errorMessage = String.Format(Tools.ResourcesT.ExceptionsVsCs.UnableToAddDLLToProjectReferences0PleaseAddThemManually, GetDLLNames(referenceDLL));
                GeneratorErrorCallback(false, 1, errorMessage, 0, 0);
                return;
            }
        }

        /// <summary>Method to create a version comment</summary>
        /// <param name="codeNamespace">CodeDOM unit to place comments inside</param>
        protected virtual void GenerateVersionComment(System.CodeDom.CodeNamespace codeNamespace) {
            codeNamespace.Comments.Add(new CodeCommentStatement(string.Empty));
            codeNamespace.Comments.Add(new CodeCommentStatement(String.Format(Tools.ResourcesT.ResourcesVsCs.ThisSourceCodeWasAutoGeneratedBy0Version1,
              System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
              System.Environment.Version.ToString())));
            codeNamespace.Comments.Add(new CodeCommentStatement(string.Empty));
        }

        /// <summary>Interface to the VS shell object we use to tell our progress while we are generating.</summary>
        /// <version version="1.5.3">Method copyied from <see cref="BaseCodeGenerator"/></version>
        protected internal Microsoft.VisualStudio.Shell.Interop.IVsGeneratorProgress CodeGeneratorProgress {
            get {
                return (Microsoft.VisualStudio.Shell.Interop.IVsGeneratorProgress)typeof(Microsoft.VisualStudio.TextTemplating.VSHost.BaseCodeGenerator).GetProperty("CodeGeneratorProgress", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic).GetValue(this, null);
            }
        }
        /// <summary>Method to return a byte-array given a Stream</summary>
        /// <param name="stream">stream to convert to a byte-array</param>
        /// <returns>the stream's contents as a byte-array</returns>
        /// <version version="1.5.3">Method copied from <see cref="BaseCodeGenerator"/> and changed from instance to static</version>
        protected internal static byte[] StreamToBytes(System.IO.Stream stream) {
            return BaseCodeGenerator.StreamToBytes(stream);
        }
    }
}