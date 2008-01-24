#if Nightly || Alpha || Beta || RC || Release
namespace Tools.Generators {

    using System;
    using System.Runtime.InteropServices;
    /// <summary>Enables the single file generator to report on its progress and to provide additional warning and/or error information.</summary>
    /// <remarks><para>When a custom tool is loaded, or the input for a custom tool is saved, the Visual Basic or Visual C# project system invokes the Generate Method, and passes a reference to IVsGeneratorProgress that enables the single file generator to report its progress to the user.</para>
    /// <para>See <a href="http://msdn2.microsoft.com/en-us/library/microsoft.visualstudio.shell.interop.ivsgeneratorprogress.aspx">IVsGeneratorProgress Interface</a></para></remarks>
    [
    ComImport,
    Guid("BED89B98-6EC9-43CB-B0A8-41D6E2D6669D"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
    ]
    public interface IVsGeneratorProgress {
        /// <summary>Returns warning and error information to the project system.</summary>
        /// <param name="fWarning">Flag that indicates whether this message is a warning or an error. Set to true to indicate a warning or to false to indicate an error.</param>
        /// <param name="dwLevel">Severity level of the error. The project system currently ignores the value of this parameter.</param>
        /// <param name="bstrError">Text of the error to be displayed to the user by means of the Task List.</param>
        /// <param name="dwLine">Zero-based line number that indicates where in the source file the error occurred. This can be –1 (or, 0xFFFFFFFF) if not needed.</param>
        /// <param name="dwColumn">One-based column number that indicates where in the source file the error occurred. This can be –1 if not needed, but must be –1 if dwLine is –1.</param>
        /// <remarks><code><![CDATA[HRESULT GeneratorError( [in] BOOL fWarning, [in] DWORD dwLevel, [in] BSTR bstrError, in] DWORD dwLine, [in] DWORD dwColumn );]]></code></remarks>
        void GeneratorError(bool fWarning,
          [MarshalAs(UnmanagedType.U4)] int dwLevel,
          [MarshalAs(UnmanagedType.BStr)] string bstrError,
          [MarshalAs(UnmanagedType.U4)] int dwLine,
          [MarshalAs(UnmanagedType.U4)] int dwColumn);

        //
        // Report progress to the caller.
        // HRESULT Progress( [in] ULONG nComplete,        // Current position
        //                  [in] ULONG nTotal );          // Max value
        //
        /// <summary>Sets an index that specifies how much of the generation has been completed.</summary>
        /// <param name="nComplete">Index that specifies how much of the generation has been completed. This value can range from zero to <paramref name="nTotal"/>.</param>
        /// <param name="nTotal">The maximum value for <paramref name="nComplete"/>.</param>
        /// <remarks>
        /// <code>HRESULT Progress( [in] ULONG nComplete, [in] ULONG nTotal );</code>
        /// </remarks>
        void Progress(
          [MarshalAs(UnmanagedType.U4)] int nComplete,
          [MarshalAs(UnmanagedType.U4)] int nTotal);
    }
}
#endif