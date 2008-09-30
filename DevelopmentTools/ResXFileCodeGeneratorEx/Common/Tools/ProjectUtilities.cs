using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;

using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;

namespace DMKSoftware.CodeGenerators.Tools
{
    public sealed class ProjectUtilities
    {
        public static bool IsCriticalException(Exception ex)
        {
            if ((!(ex is NullReferenceException) && !(ex is StackOverflowException)) && !(ex is OutOfMemoryException))
            {
                return (ex is ThreadAbortException);
            }

            return true;
        }
    }
}

