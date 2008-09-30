using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DMKSoftware.CodeGenerators
{
	[Guid("AC0A9CFD-1B0A-4e4a-86A1-AB7854A33E23")]
	[Description("Extended ResX Internal File Code Generator")]
	public class InternalResXFileCodeGeneratorEx : BaseResXFileCodeGeneratorEx
    {
		/// <summary>
		/// Initializes a new instance of the InternalResXFileCodeGeneratorEx class.
		/// </summary>
		public InternalResXFileCodeGeneratorEx()
		{
		}

		/// <summary>
		/// Gets the boolean flag indicating whether the internal class is generated.
		/// </summary>
		protected override bool GenerateInternalClass
		{
			get
			{
				return true;
			}
		}
    }
}