#if Nightly || Alpha || Beta || RC || Release
namespace Tools.GeneratorsT {

    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
 

    /// <summary>
    ///     This wraps the <see cref="IOleServiceProvider"/> interface and provides an easy COM+ way to get at
    ///     services.
    /// </summary>
    public class ServiceProvider:IServiceProvider, Microsoft.VisualStudio.OLE.Interop.IObjectWithSite {
        /// <summary>GUID of IUnknown</summary>
        private static Guid IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");
        /// <summary>The <see cref="IOleServiceProvider"/> interface used.</summary>
        private IOleServiceProvider serviceProvider;

        /// <summary>
        ///     Creates a new <see cref="ServiceProvider"/> object and uses the given interface to resolve
        ///     services.
        /// </summary>
        /// <param name='sp'>
        ///     The <see cref="IOleServiceProvider"/> interface to use.
        /// </param>
        public ServiceProvider(IOleServiceProvider sp) {
            serviceProvider = sp;
        }

        /// <summary>
        /// gives this class a chance to free its references.
        /// </summary>
        public virtual void Dispose() {
            if(serviceProvider != null) {
                serviceProvider = null;
            }
        }

        /// <summary>
        /// returns true if the given HRESULT is a failure HRESULT
        /// </summary>
        /// <param name="hr">HRESULT to test</param>
        /// <returns>true if the HRESULT is a failure, false if not.</returns>
        public static bool Failed(int hr) {
            return (hr < 0);
        }

        /// <summary>
        ///     Retrieves the requested service.
        /// </summary>
        /// <param name='serviceClass'>
        ///     The class of the service to retrieve.
        /// </param>
        /// <returns>
        ///     an instance of serviceClass or null if no
        ///     such service exists.
        /// </returns>
        public virtual object GetService(Type serviceClass) {

            if(serviceClass == null) {
                return null;
            }

            return GetService(serviceClass.GUID, serviceClass);
        }

        /// <summary>
        ///     Retrieves the requested service.
        /// </summary>
        /// <param name='guid'>
        ///     The GUID of the service to retrieve.
        /// </param>
        /// <returns>
        ///     an instance of the service or null if no
        ///     such service exists.
        /// </returns>
        public virtual object GetService(Guid guid) {
            return GetService(guid, null);
        }

        /// <summary>
        ///     Retrieves the requested service.  The guid must be specified; the class is only
        ///     used when debugging and it may be null.
        /// </summary>
        private object GetService(Guid guid, Type serviceClass) {

            // Valid, but wierd for caller to init us with a NULL sp
            //
            if(serviceProvider == null) {
                return null;
            }

            object service = null;

            // No valid guid on the passed in class, so there is no service for it.
            //
            if(guid.Equals(Guid.Empty)) {
                return null;
            }

            // We provide a couple of services of our own.
            //
            if(guid.Equals(typeof(IOleServiceProvider).GUID)) {
                return serviceProvider;
            }
            if(guid.Equals(typeof(Microsoft.VisualStudio.OLE.Interop.IObjectWithSite).GUID)) {
                return (Microsoft.VisualStudio.OLE.Interop.IObjectWithSite)this;
            }

            IntPtr pUnk;
            int hr = serviceProvider.QueryService(ref guid, ref IID_IUnknown, out pUnk);

            if(Succeeded(hr) && (pUnk != IntPtr.Zero)) {
                service = Marshal.GetObjectForIUnknown(pUnk);
                Marshal.Release(pUnk);
            }

            return service;
        }

        /// <summary>
        ///     Retrieves the current site object we're using to
        ///     resolve services.
        /// </summary>
        /// <param name='riid'>
        ///     Must be IServiceProvider.class.GUID
        /// </param>
        /// <param name='ppvSite'>
        ///     Outparam that will contain the site object.
        /// </param>
        /// <seealso cref='Microsoft.VisualStudio.OLE.Interop.IObjectWithSite'/>
        void Microsoft.VisualStudio.OLE.Interop.IObjectWithSite.GetSite(ref Guid riid, out IntPtr ppvSite) {
            ppvSite = Marshal.GetIUnknownForObject(GetService(riid));
            Marshal.GetIUnknownForObject(GetService(riid));
        }

        /// <summary>
        ///     Sets the site object we will be using to resolve services.
        /// </summary>
        /// <param name='pUnkSite'>
        ///     The site we will use.  This site will only be
        ///     used if it also implements IOleServiceProvider.
        /// </param>
        /// <seealso cref='Microsoft.VisualStudio.OLE.Interop.IObjectWithSite'/>
        void Microsoft.VisualStudio.OLE.Interop.IObjectWithSite.SetSite(object pUnkSite) {
            if(pUnkSite is IOleServiceProvider) {
                serviceProvider = (IOleServiceProvider)pUnkSite;
            }
        }

        /// <summary>
        /// returns true if the given HRESULT is a success HRESULT
        /// </summary>
        /// <param name="hr">HRESULT to test</param>
        /// <returns>true if the HRESULT is a success, false if not.</returns>
        public static bool Succeeded(int hr) {
            return (hr >= 0);
        }
    }
}

#endif