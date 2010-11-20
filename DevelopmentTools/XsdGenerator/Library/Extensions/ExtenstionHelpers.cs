using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions {
    /// <summary>Provides static helper methods for XSD Generator extensions</summary>
    internal static class  ExtenstionHelpers {
        /// <summary>Finds CodeDOM type by name</summary>
        /// <param name="name">name of type to be found</param>
        /// <param name="ns">Namespace to find type in</param>
        /// <exception cref="ArgumentException">Type with given name cannot be found</exception>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> or <paramref name="ns"/> is null.</exception>
        public static  CodeTypeDeclaration FindType(string name, CodeNamespace ns) {
            if (name == null) throw new ArgumentNullException("name");
            if (ns == null) throw new ArgumentNullException("ns");
            string[] parts = name.Split('.');
            foreach (CodeTypeDeclaration t in ns.Types) {
                if (parts[0] == t.Name) {
                    int pi = 1;
                    CodeTypeDeclaration type = t;
                    while (pi < parts.Length) {
                        foreach (CodeTypeMember m in t.Members) {
                            if (m is CodeTypeDeclaration && m.Name == parts[pi]) {
                                type = (CodeTypeDeclaration)m;
                                pi++;
                                break;
                            }
                        }
                        throw new ArgumentException(string.Format(Resources.ex_TypeNotFound, name));
                    }
                    return type;
                }
            }
            throw new ArgumentException(string.Format(Resources.ex_TypeNotFound, name));
        }
    }
}
