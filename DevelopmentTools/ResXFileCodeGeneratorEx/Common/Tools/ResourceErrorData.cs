using System;
using System.Collections.Generic;
using System.Text;

namespace DMKSoftware.CodeGenerators.Tools
{
    public class ResourceErrorData
    {
        private string _resourceKey;
        private string _errorString;

        public ResourceErrorData(string resourceKey, string errorString)
        {
            _resourceKey = resourceKey;
            _errorString = errorString;
        }

        public string ResourceKey
        {
            get
            {
                return _resourceKey;
            }
        }

        public string ErrorString
        {
            get
            {
                return _errorString;
            }
        }
    }
}
