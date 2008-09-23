#if Nightly || Alpha || Beta || RC || Release
namespace Tools.GeneratorsT {

    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using EnvDTE;
    using VSLangProj;
    using Microsoft.VisualStudio.Designer.Interfaces;

    /// <summary>
    ///     This class exists to be cocreated a in a preprocessor build step.
    /// </summary>
    public abstract class BaseCodeGeneratorWithSite:BaseCodeGenerator, IObjectWithSite {
        /// <summary>Error HRESULT for a generic failure.</summary>
        private const int E_FAIL = unchecked((int)0x80004005);
        /// <summary>Error HRESULT for the request of a not implemented interface.</summary>
        private const int E_NOINTERFACE = unchecked((int)0x80004002);
        /// <summary>Contains value of the <see cref="Site"/> property</summary>
        private object site = null;
        /// <summary>Provider for code generation</summary>
        private CodeDomProvider codeDomProvider = null;
        /// <summary>Guid of dom interface</summary>
        private static Guid CodeDomInterfaceGuid = new Guid("{73E59688-C7C4-4a85-AF64-A538754784C5}");
        /// <summary>Same as <see cref="CodeDomInterfaceGuid"/></summary>
        private static Guid CodeDomServiceGuid = CodeDomInterfaceGuid;
        /// <summary>Service provider</summary>
        private ServiceProvider serviceProvider = null;

        /// <summary>
        /// demand-creates a CodeDomProvider
        /// </summary>
        protected virtual CodeDomProvider CodeProvider {
            get {
                if(codeDomProvider == null) {
                    IVSMDCodeDomProvider vsmdCodeDomProvider = (IVSMDCodeDomProvider)GetService(CodeDomServiceGuid);
                    if(vsmdCodeDomProvider != null) {
                        codeDomProvider = (CodeDomProvider)vsmdCodeDomProvider.CodeDomProvider;
                    }
                    Debug.Assert(codeDomProvider != null, Tools.Exceptions.GetCodeDomProviderInterfaceFailedGetServiceQueryServiceCodeDomProviderReturnedNull);
                }
                return codeDomProvider;
            }
            set {
                if(value == null) {
                    throw new ArgumentNullException();
                }

                codeDomProvider = value;
            }
        }

        /// <summary>
        /// demand-creates a ServiceProvider given an IOleServiceProvider
        /// </summary>
        private ServiceProvider SiteServiceProvider {
            get {
                if(serviceProvider == null) {
                    IOleServiceProvider oleServiceProvider = site as IOleServiceProvider;
                    Debug.Assert(oleServiceProvider != null, Tools.Exceptions.UnableToGetIOleServiceProviderFromSiteObject);

                    serviceProvider = new ServiceProvider(oleServiceProvider);
                }
                return serviceProvider;
            }
        }

        /// <summary>
        /// method to get a service by its GUID
        /// </summary>
        /// <param name="serviceGuid">GUID of service to retrieve</param>
        /// <returns>an object that implements the requested service</returns>
        protected object GetService(Guid serviceGuid) {
            return SiteServiceProvider.GetService(serviceGuid);
        }

        /// <summary>
        /// method to get a service by its Type
        /// </summary>
        /// <param name="serviceType">Type of service to retrieve</param>
        /// <returns>an object that implements the requested service</returns>
        protected object GetService(Type serviceType) {
            return SiteServiceProvider.GetService(serviceType);
        }

        /// <summary>
        /// gets the default extension of the output file by asking the CodeDomProvider
        /// what its default extension is.
        /// </summary>
        /// <returns></returns>
        public override string GetDefaultExtension() {
            CodeDomProvider codeDom = CodeProvider;
            Debug.Assert(codeDom != null, Tools.Exceptions.CodeDomProviderIsNULL);
            string extension = codeDom.FileExtension;
            if(extension != null && extension.Length > 0) {
                if(extension[0] != '.') {
                    extension = "." + extension;
                }
            }

            return extension;
        }

        /// <summary>
        /// Method to get an ICodeGenerator with which this class can create code.
        /// </summary>
        /// <returns></returns>
        [Obsolete("This is obsolete, because System.CodeDom.Compiler.CodeDomProvider.CreateGenerator() is obsolete")]
        protected virtual ICodeGenerator GetCodeWriter() {
            CodeDomProvider codeDom = CodeProvider;
            if(codeDom != null) {
                return codeDom.CreateGenerator();
            }

            return null;
        }

        /// <summary>
        /// SetSite method of IOleObjectWithSite
        /// </summary>
        /// <param name="pUnkSite">site for this object to use</param>
        public virtual void SetSite(object pUnkSite) {
            site = pUnkSite;
            codeDomProvider = null;
            serviceProvider = null;
        }
        /// <summary>Gets the last site set with <see cref="IObjectWithSite.SetSite"/>.</summary>
        protected object Site {
            get { return site; }
        }

        /// <summary>
        /// GetSite method of IOleObjectWithSite
        /// </summary>
        /// <param name="riid">interface to get</param>
        /// <param name="ppvSite">array in which to stuff return value</param>
        public virtual void GetSite(ref Guid riid, object[] ppvSite) {

            if(ppvSite == null) {
                throw new ArgumentNullException("ppvSite");
            }
            if(ppvSite.Length < 1) {
                throw new ArgumentException(String.Format(Tools.Exceptions.ArrayMustHaveAtLeast1Member, "ppvSite"), "ppvSite");
            }

            if(site == null) {
                throw new COMException(Tools.Exceptions.ObjectIsNotSited, E_FAIL);
            }

            IntPtr pUnknownPointer = Marshal.GetIUnknownForObject(site);
            IntPtr intPointer = IntPtr.Zero;
            Marshal.QueryInterface(pUnknownPointer, ref riid, out intPointer);

            if(intPointer == IntPtr.Zero) {
                throw new COMException(Tools.Exceptions.SiteDoesNotSupportRequestedInterface, E_NOINTERFACE);
            }

            ppvSite[0] = Marshal.GetObjectForIUnknown(intPointer);
        }

        /// <summary>
        /// gets a string containing the DLL names to add.
        /// </summary>
        /// <param name="DLLToAdd"></param>
        /// <returns></returns>
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

        /// <summary>
        /// adds a reference to the project for each required DLL
        /// </summary>
        /// <param name="referenceDLL"></param>
        protected void AddReferenceDLLToProject(string[] referenceDLL) {

            if(referenceDLL.Length == 0) {
                return;
            }

            object serviceObject = GetService(typeof(ProjectItem));
            Debug.Assert(serviceObject != null, Tools.Exceptions.UnableToGetProjectItem);
            if(serviceObject == null) {
                string errorMessage = String.Format(Tools.Exceptions.UnableToAddDLLToProjectReferences0PleaseAddThemManually, GetDLLNames(referenceDLL));
                GeneratorErrorCallback(false, 1, errorMessage, 0, 0);
                return;
            }

            Project containingProject = ((ProjectItem)serviceObject).ContainingProject;
            Debug.Assert(containingProject != null, Tools.Exceptions.GetServiceTypeofProjectReturnNull);
            if(containingProject == null) {
                string errorMessage = String.Format(Tools.Exceptions.UnableToAddDLLToProjectReferences0PleaseAddThemManually, GetDLLNames(referenceDLL));
                GeneratorErrorCallback(false, 1, errorMessage, 0, 0);
                return;
            }

            VSProject vsProj = containingProject.Object as VSProject;
            Debug.Assert(vsProj != null, Tools.Exceptions.UnableToADDDLLToCurrentProjectProjectObjectDoesNotImplementVSProject);
            if(vsProj == null) {
                string errorMessage = String.Format(Tools.Exceptions.UnableToAddDLLToProjectReferences0PleaseAddThemManually, GetDLLNames(referenceDLL));
                GeneratorErrorCallback(false, 1, errorMessage, 0, 0);
                return;
            }

            try {
                for(int i = 0; i < referenceDLL.Length; i++) {
                    vsProj.References.Add(referenceDLL[i]);
                }
            } catch(Exception e) {
                Debug.Fail(Tools.Exceptions.ERRORVsProjReferencesAddThrowsException + e.ToString());

                string errorMessage = String.Format(Tools.Exceptions.UnableToAddDLLToProjectReferences0PleaseAddThemManually, GetDLLNames(referenceDLL));
                GeneratorErrorCallback(false, 1, errorMessage, 0, 0);
                return;
            }
        }

        /// <summary>
        /// method to create an exception message given an exception
        /// </summary>
        /// <param name="e">exception caught</param>
        /// <returns>message to display to the user</returns>
        protected virtual string CreateExceptionMessage(Exception e) {

            string message = (e.Message != null ? e.Message : string.Empty);

            Exception innerException = e.InnerException;
            while(innerException != null) {
                string innerMessage = innerException.Message;
                if(innerMessage != null && innerMessage.Length > 0) {
                    message = message + " " + innerMessage;
                }
                innerException = innerException.InnerException;
            }

            return message;
        }

        /// <summary>
        /// method to create a version comment
        /// </summary>
        /// <param name="codeNamespace"></param>
        protected virtual void GenerateVersionComment(System.CodeDom.CodeNamespace codeNamespace) {
            codeNamespace.Comments.Add(new CodeCommentStatement(string.Empty));
            codeNamespace.Comments.Add(new CodeCommentStatement(String.Format(Tools.Resources.ThisSourceCodeWasAutoGeneratedBy0Version1,
              System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
              System.Environment.Version.ToString())));
            codeNamespace.Comments.Add(new CodeCommentStatement(string.Empty));
        }

    }
}
#endif