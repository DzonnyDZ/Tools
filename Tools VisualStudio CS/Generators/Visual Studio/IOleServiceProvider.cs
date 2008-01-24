#if Nightly || Alpha || Beta || RC || Release
namespace Tools.Generators {

    using System;
    using System.Runtime.InteropServices;
    /// <summary>OLE Service provider</summary>
    [
    ComImport,
    Guid("6D5140C1-7436-11CE-8034-00AA006009FA"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
    ]
    public interface IOleServiceProvider {
        /// <summary>Queries the OLE service</summary>
        /// <param name="guidService">Sefice GUID</param>
        /// <param name="ppvObject">Object queried</param>
        /// <param name="riid">IID of the interface pointer that should be returned in <paramref name="ppvObject"/></param>
        [PreserveSig]
        int QueryService([In]ref Guid guidService,
         [In]ref Guid riid,
         out IntPtr ppvObject);
    }
}
#endif