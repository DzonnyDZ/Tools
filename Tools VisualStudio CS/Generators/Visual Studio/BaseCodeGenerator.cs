namespace Tools.VisualStudioT.GeneratorsT {

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell.Interop;

    /// <summary>
    /// A managed wrapper for VS's concept of an <see cref="IVsSingleFileGenerator"/> which is
    /// a custom tool invoked during the build which can take any file as an input
    /// and provide a compilable code file as output.
    /// </summary>
    /// <version version="1.5.2">Moved from namespace Tools.GeneratorsT to Tools.VisualStudioT.GeneratorsT</version>
    /// <version version="1.5.3">Base class changed from <see cref="Object"/> to <see cref="Microsoft.VisualStudio.TextTemplating.VSHost.BaseCodeGenerator"/>. Methods implemented by <see cref="Microsoft.VisualStudio.TextTemplating.VSHost.BaseCodeGenerator"/> removed from this class.</version>
    public abstract class BaseCodeGenerator : Microsoft.VisualStudio.TextTemplating.VSHost.BaseCodeGenerator {

        /// <summary>Interface to the VS shell object we use to tell our progress while we are generating.</summary>
        protected internal IVsGeneratorProgress CodeGeneratorProgress {
            get {
                return (IVsGeneratorProgress)typeof(Microsoft.VisualStudio.TextTemplating.VSHost.BaseCodeGenerator).GetProperty("CodeGeneratorProgress").GetValue(this, null);
            }
        }

        /// <summary>Method to return a byte-array given a Stream</summary>
        /// <param name="stream">stream to convert to a byte-array</param>
        /// <returns>the stream's contents as a byte-array</returns>
        /// <version version="1.5.3">Method changed from instance to static</version>
        protected internal static byte[] StreamToBytes(Stream stream) {

            if(stream.Length == 0) {
                return new byte[] { };
            }

            long position = stream.Position;
            stream.Position = 0;
            byte[] bytes = new byte[(int)stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Position = position;

            return bytes;
        }
    }
}