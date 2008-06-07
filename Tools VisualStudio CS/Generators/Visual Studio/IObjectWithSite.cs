#if Nightly || Alpha || Beta || RC || Release
namespace Tools.GeneratorsT {

    using System;
    using System.Runtime.InteropServices;

    /// <summary>Provides simple objects with a lightweight siting mechanism (lighter than IOleObject).</summary>
    /// <remarks>See <a href="http://msdn2.microsoft.com/en-us/library/aa768220.aspx">IObjectWithSite Interface</a> for details.</remarks>
    [
    ComImport,
    Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
    ]
    internal interface IObjectWithSite {

        //
        //    HRESULT SetSite(
        // 				[in] IUnknown * pUnkSite );
        //
        /// <summary>Provides the site's IUnknown pointer to the object.</summary>
        /// <param name="pUnkSite">An interface pointer to the site managing this object. If NULL, the object should call IUnknown.Release to release the existing site.</param>
        /// <remarks><code>HRESULT SetSite(IUnknown *pUnkSite);</code></remarks>
        void SetSite(
          [MarshalAs(UnmanagedType.Interface)] object pUnkSite);

        //
        // HRESULT GetSite(
        //        [in] REFIID riid,
        //        [out, iid_is( riid )] void ** ppvSite );
        //
        /// <summary>Gets the last site set with <see cref="IObjectWithSite.SetSite"/>. If there is no known site, the object returns a failure code.</summary>
        /// <param name="ppvSite">The address of the caller's void* variable in which the object stores the interface pointer of the site last seen in <see cref="IObjectWithSite.SetSite"/>. The specific interface returned depends on the riid argument; the two arguments act identically to those in QueryInterface. If the appropriate interface pointer is available, the object must call AddRef on that pointer before returning successfully. If no site is available, or the requested interface is not supported, the object sets this argument to NULL, and returns a failure code.</param>
        /// <param name="riid">The IID of the interface pointer that should be returned in <paramref name="ppvSite"/>.</param>
        void GetSite([In] ref Guid riid,
[Out, MarshalAs(UnmanagedType.LPArray)] object[] ppvSite);
    }
}
#endif