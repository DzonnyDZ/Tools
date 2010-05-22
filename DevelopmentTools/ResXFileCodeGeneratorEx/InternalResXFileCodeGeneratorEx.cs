using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Tools.VisualStudioT.GeneratorsT.ResXFileGenerator
{
    /// <summary>RESX file code generator which generates internal (Friend in Visual Basic) classes</summary>
    /// <version version="1.5.3">This class is new in version 1.5.3. It was moved from namespace <c>DMKSoftware.CodeGenerators</c>.</version>
    /// <version version="1.5.3">When &lt;LogicalName> is specified in project file for RESX file, class name is obtained from logical name rather than from file name of RESX file.</version>
    /// <version version="1.5.3">Modules are generated for Visual Basic. For other languages class constructor is made <see langword="private"/> (previously <see langword="internal"/>).</version>
    /// <version version="1.5.3">Members of resource class (module for VB) are <see langword="public"/>.</version>
    [Guid("26e153bc-81cf-41cf-a8aa-fe2969b4fde3")]
	[Description("Extended ResX Internal File Code Generator")]
	public class InternalResXFileCodeGeneratorEx : BaseResXFileCodeGeneratorEx
    {
		/// <summary>CTor - initializes a new instance of the <see cref="InternalResXFileCodeGeneratorEx"/> class.</summary>
        public InternalResXFileCodeGeneratorEx() { }

        /// <summary>Gets the boolean flag indicating whether the internal class is generated.</summary>
        /// <returns>This implementation always returns true.</returns>
        protected override bool GenerateInternalClass { get { return true; } }
    }
}