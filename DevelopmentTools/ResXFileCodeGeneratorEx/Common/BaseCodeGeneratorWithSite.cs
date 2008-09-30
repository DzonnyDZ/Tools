using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio.Designer.Interfaces;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;

namespace DMKSoftware.CodeGenerators
{
    public abstract class BaseCodeGeneratorWithSite : BaseCodeGenerator, IObjectWithSite
    {
        private static Guid _codeDomInterfaceGuid;
        private CodeDomProvider _codeDomProvider;
        private static Guid _codeDomServiceGuid;
        private ServiceProvider _serviceProvider;
        private object _site;

        static BaseCodeGeneratorWithSite()
        {
            _codeDomInterfaceGuid = new Guid("{73E59688-C7C4-4a85-AF64-A538754784C5}");
            _codeDomServiceGuid = _codeDomInterfaceGuid;
        }

        protected BaseCodeGeneratorWithSite()
        {
        }

        public override int DefaultExtension(out string ext)
        {
            string defaultExtension = CodeProvider.FileExtension;
            if (((defaultExtension != null) && (defaultExtension.Length > 0)) && (defaultExtension[0] != '.'))
                defaultExtension = "." + defaultExtension;

            ext = defaultExtension;
            
            return 0;
        }

        protected virtual ICodeGenerator GetCodeWriter()
        {
            CodeDomProvider codeComProvider = CodeProvider;
            if (null != codeComProvider)
                return codeComProvider.CreateGenerator();

            return null;
        }

        protected object GetService(Guid serviceGuid)
        {
            return SiteServiceProvider.GetService(serviceGuid);
        }

        protected object GetService(Type serviceType)
        {
            return SiteServiceProvider.GetService(serviceType);
        }

        public virtual void GetSite(ref Guid riid, out IntPtr ppvSite)
        {
            if (null == _site)
                throw new Win32Exception(-2147467259);
            
            IntPtr siteIUnknown = Marshal.GetIUnknownForObject(_site);
            try
            {
                Marshal.QueryInterface(siteIUnknown, ref riid, out ppvSite);
                if (IntPtr.Zero == ppvSite)
                    throw new Win32Exception(-2147467262);
            }
            finally
            {
                if (IntPtr.Zero != siteIUnknown)
                {
                    Marshal.Release(siteIUnknown);
                    siteIUnknown = IntPtr.Zero;
                }
            }
        }

        public virtual void SetSite(object pUnkSite)
        {
            _site = pUnkSite;
            _codeDomProvider = null;
            _serviceProvider = null;
        }

        protected virtual CodeDomProvider CodeProvider
        {
            get
            {
                if (null == _codeDomProvider)
                {
                    IVSMDCodeDomProvider vsMDCodeDomProvider = (IVSMDCodeDomProvider)this.GetService(_codeDomServiceGuid);
                    if (null != vsMDCodeDomProvider)
                        _codeDomProvider = (CodeDomProvider)vsMDCodeDomProvider.CodeDomProvider;
                }

                return this._codeDomProvider;
            }
            set
            {
                if (null == value)
                    throw new ArgumentNullException();

                _codeDomProvider = value;
            }
        }

        private ServiceProvider SiteServiceProvider
        {
            get
            {
                if (null == _serviceProvider)
                {
                    Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceProvider = _site as Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
                    _serviceProvider = new ServiceProvider(serviceProvider);
                }

                return this._serviceProvider;
            }
        }
    }
}

