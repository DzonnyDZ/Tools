using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.VisualStudioT.GeneratorsT.ResXFileGenerator
{
    /// <summary>Represents resource errors</summary>
    /// <version version="1.5.3">This class is new in version 1.5.3. It was moved from namespace <c>DMKSoftware.CodeGenerators</c>.</version>
    public class ResourceErrorData
    {
        private string _resourceKey;
        private string _errorString;
        /// <summary>CTor - creates a new instance of the <see cref="ResourceErrorData"/> class</summary>
        /// <param name="resourceKey">Resource key</param>
        /// <param name="errorString">Error message</param>
        public ResourceErrorData(string resourceKey, string errorString)
        {
            _resourceKey = resourceKey;
            _errorString = errorString;
        }
        /// <summary>Gets resource key</summary>
        public string ResourceKey
        {
            get
            {
                return _resourceKey;
            }
        }
        /// <summary>Gets error message</summary>
        public string ErrorString
        {
            get
            {
                return _errorString;
            }
        }
    }
}
