#if Nightly || Alpha || Beta || RC || Release
namespace Tools.Generators {
    using System;
    using System.Runtime.InteropServices;
    /// <summary>Transforms a single input file into a single output file that can be compiled or added to a project. Any COM component that implements the IVsSingleFileGenerator is a custom tool.</summary>
    /// <remarks>
    /// <para>Any custom tool that is a COM component must implement the IVsSingleFileGenerator interface.</para>
    /// <para>See <a href="http://msdn2.microsoft.com/en-us/library/microsoft.visualstudio.shell.interop.ivssinglefilegenerator(VS.80).aspx">IVsSingleFileGenerator Interface</a></para>
    /// </remarks>
    [
    ComImport,
    Guid("3634494C-492F-4F91-8009-4541234E4E99"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
    ]
    public interface IVsSingleFileGenerator {
        /// <summary>Retrieves the file extension that is given to the output file name.</summary>
        /// <returns>Returns the file extension that is to be given to the output file name. The returned extension must include a leading period.</returns>
        /// <remarks>The project system invokes DefaultExtension in order to determine what extension to give to the generated output file.
        /// <code>[propget]   HRESULT DefaultExtension( [out,retval] BSTR* pbstrDefaultExtension );</code></remarks>
        [return: MarshalAs(UnmanagedType.BStr)]
        string GetDefaultExtension();


        /// <summary>Executes the transformation and returns the newly generated output file, whenever a custom tool is loaded, or the input file is saved.</summary>
        /// <param name="wszInputFilePath">The full path of the input file. May be a null reference (Nothing in Visual Basic) in future releases of Visual Studio, so generators should not rely on this value.</param>
        /// <param name="bstrInputFileContents">The contents of the input file. This is either a UNICODE BSTR (if the input file is text) or a binary BSTR (if the input file is binary). If the input file is a text file, the project system automatically converts the BSTR to UNICODE.</param>
        /// <param name="wszDefaultNamespace">This parameter is meaningful only for custom tools that generate code. It represents the namespace into which the generated code will be placed. If the parameter is not a null reference (Nothing in Visual Basic) and not empty, the custom tool can use the following syntax to enclose the generated code.
        /// <code> ' Visual Basic Namespace [default namespace]
        /// ... End Namespace
        /// // Visual C#
        /// namespace [default namespace] { ... }</code></param>
        /// <param name="rgbOutputFileContents">Returns an array of bytes to be written to the generated file. You must include UNICODE or UTF-8 signature bytes in the returned byte array, as this is a raw stream. The memory for rgbOutputFileContents must be allocated using the .NET Framework call, System.Runtime.InteropServices.AllocCoTaskMem, or the equivalent Win32 system call, CoTaskMemAlloc. The project system is responsible for freeing this memory.</param>
        /// <param name="pcbOutput">Returns the count of bytes in the rgbOutputFileContent array.</param>
        /// <param name="pGenerateProgress">A reference to the <see craf="IVsGeneratorProgress"/> interface through which the generator can report its progress to the project system.</param>
        /// <remarks><code>HRESULT Generate( [in] LPCOLESTR wszInputFilePath, [in] BSTR bstrInputFileContents, [in] LPCOLESTR wszDefaultNamespace,  [out] BYTE**    rgbOutputFileContents, [out] ULONG*    pcbOutput, [in] IVsGeneratorProgress* pGenerateProgress );</code></remarks>
        void Generate(
          [MarshalAs(UnmanagedType.LPWStr)] string wszInputFilePath,
          [MarshalAs(UnmanagedType.BStr)] string bstrInputFileContents,
          [MarshalAs(UnmanagedType.LPWStr)] string wszDefaultNamespace,
          out IntPtr rgbOutputFileContents,
          [MarshalAs(UnmanagedType.U4)] out int pcbOutput,
          IVsGeneratorProgress pGenerateProgress);
    }
}
#endif