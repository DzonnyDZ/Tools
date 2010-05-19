using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Tools.VisualStudioT.GeneratorsT.ResXFileGenerator
{
    /// <summary>RESX file code generator which generates public classes</summary>
    /// <version version="1.5.3">This class is new in version 1.5.3. It was moved from namespace <c>DMKSoftware.CodeGenerators</c>.</version>
    /// <version version="1.5.3">When &lt;LOgicalName> is specified in project file for RESX file, class name is obtained from logical name rather than from file name of RESX file.</version>
    [Guid("83e1b007-5540-4e3a-97b1-394ebaf33f0e")]
	[Description("Extended ResX Public File Code Generator")]
	public class ResXFileCodeGeneratorEx : BaseResXFileCodeGeneratorEx
    {
		/// <summary>CTor - initializes a new instance of the <see cref="ResXFileCodeGeneratorEx"/> class.</summary>
        public ResXFileCodeGeneratorEx() { }

		/// <summary>Gets the boolean flag indicating whether the internal class is generated.</summary>
        /// <returns>This implementation always returns false.</returns>
        protected override bool GenerateInternalClass { get { return false; } }
    }
}

