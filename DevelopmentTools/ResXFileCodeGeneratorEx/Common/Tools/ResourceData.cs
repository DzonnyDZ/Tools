using System;
using System.Collections.Generic;
using System.Text;

namespace DMKSoftware.CodeGenerators.Tools
{
    internal sealed class ResourceData
    {
        private Type _type;
        private string _valueIfString;

        internal ResourceData(System.Type type, string valueIfItWasAString)
        {
            _type = type;
            _valueIfString = valueIfItWasAString;
        }

        internal System.Type Type
        {
            get
            {
                return _type;
            }
        }

        internal string ValueIfString
        {
            get
            {
                return _valueIfString;
            }
        }
    }
}
