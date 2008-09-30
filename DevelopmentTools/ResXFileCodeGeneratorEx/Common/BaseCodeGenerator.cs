using System;
using System.IO;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace DMKSoftware.CodeGenerators
{
    public abstract class BaseCodeGenerator : IVsSingleFileGenerator
    {
        private string _codeFileNameSpace;
        private string _codeFilePath;
        private IVsGeneratorProgress _codeGeneratorProgress;

        protected BaseCodeGenerator()
        {
            _codeFileNameSpace = string.Empty;
            _codeFilePath = string.Empty;
        }

        public abstract int DefaultExtension(out string ext);

        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] pbstrOutputFileContents,
            out uint pbstrOutputFileContentSize, IVsGeneratorProgress pGenerateProgress)
        {
            if (null == bstrInputFileContents)
                throw new ArgumentNullException(bstrInputFileContents);

            _codeFilePath = wszInputFilePath;
            _codeFileNameSpace = wszDefaultNamespace;
            _codeGeneratorProgress = pGenerateProgress;
            
            byte[] codeBuffer = this.GenerateCode(wszInputFilePath, bstrInputFileContents);
            if (null == codeBuffer)
            {
                pbstrOutputFileContents[0] = IntPtr.Zero;
                pbstrOutputFileContentSize = 0;
            }
            else
            {
                pbstrOutputFileContents[0] = Marshal.AllocCoTaskMem(codeBuffer.Length);
                Marshal.Copy(codeBuffer, 0, pbstrOutputFileContents[0], codeBuffer.Length);
                pbstrOutputFileContentSize = (uint)codeBuffer.Length;
            }

            return 0;
        }

        protected abstract byte[] GenerateCode(string inputFileName, string inputFileContent);

        protected virtual void GeneratorErrorCallback(int warning, uint level, string message, uint line, uint column)
        {
            IVsGeneratorProgress vsGeneratorProgress = this.CodeGeneratorProgress;
            if (null != vsGeneratorProgress)
                NativeMethods.ThrowOnFailure(vsGeneratorProgress.GeneratorError(warning, level, message, line, column));
        }

        protected byte[] StreamToBytes(Stream stream)
        {
            if (0 == stream.Length)
                return new byte[0];
            
            long initialStreamPosition = stream.Position;
            stream.Position = 0;
            
            byte[] streamBuffer = new byte[(int)stream.Length];
            stream.Read(streamBuffer, 0, streamBuffer.Length);
            stream.Position = initialStreamPosition;
            
            return streamBuffer;
        }

        public IVsGeneratorProgress CodeGeneratorProgress
        {
            get
            {
                return _codeGeneratorProgress;
            }
        }

        protected string FileNameSpace
        {
            get
            {
                return _codeFileNameSpace;
            }
        }

        protected string InputFilePath
        {
            get
            {
                return _codeFilePath;
            }
        }
    }
}

