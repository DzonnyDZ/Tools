using System;

namespace DMKSoftware.CodeGenerators
{
	/// <summary>
	/// Specifies the VS.NET language GUID.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	internal class LanguageGuidAttribute : Attribute
	{
		#region Private Variables
		private Guid _guid;
		#endregion

		#region Contruction
		/// <summary>
		/// Initializes a new instance of the LanguageGuidAttribute class.
		/// </summary>
		/// <param name="guidString">The string representation of tha language GUID.</param>
		public LanguageGuidAttribute(string guidString)
		{
			if (string.IsNullOrEmpty(guidString))
				throw new ArgumentNullException("guidString");

			_guid = new Guid(guidString);
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets the language GUID.
		/// </summary>
		public Guid Guid
		{
			get
			{
				return _guid;
			}
		}
		#endregion
	}
}
