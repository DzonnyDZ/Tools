using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DMKSoftware.CodeGenerators
{
    [Guid("FF2F2841-D6A2-42b5-9E14-86AD00A2917E")]
	[Description("Extended ResX Public File Code Generator")]
	public class ResXFileCodeGeneratorEx : BaseResXFileCodeGeneratorEx
    {
		/// <summary>
		/// Initializes a new instance of the ResXFileCodeGeneratorEx class.
		/// </summary>
		public ResXFileCodeGeneratorEx()
		{
		}

		/// <summary>
		/// Gets the boolean flag indicating whether the internal class is generated.
		/// </summary>
		protected override bool GenerateInternalClass
		{
			get
			{
				return false;
			}
		}
    }
}

