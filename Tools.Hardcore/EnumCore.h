#include "Stdafx.h"
using namespace System;
using namespace System::Runtime::CompilerServices;

namespace Tools{
    /// <summary>Contains basic extension methods for working with enumerations</summary>
    /// <version version="1.5.3">This class is new in version 1.5.3</version>
    [Extension]
    public ref class EnumCore{
    private: 
        /// <summary>Private CTor - this is static class</summary>
        EnumCore(void);
    public:
        /// <summary>Determines whether one or more bit fields are set in the current instance. (type-safe)</summary>
        /// <typeparam name="T">Type of enumeration</typeparam>
        /// <param name="value">An enumeration value to test flags on (this possibly is OR-ed value of multiple flags)</param>
        /// <param name="flag">An enumeration value. - the flag to test</param>
        /// <returns>true if the bit field or bit fields that are set in <paramref name="flag"/> are also set in <paramref name="value"/>; otherwise, false.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is <see cref="Enum"/> (not a derived type) and actual types of <paramref name="value"/> and <paramref name="flag"/> differ.</exception>
        /// <seelaso cref="Enum::HasFlag"/>
        generic <class T> where T:Enum
        [Extension]
        static bool HasFlagSet(T value, T flag);

        /// <summary>Retrieves an array of the values of the constants in an enumeration.</summmary>
        /// <typeparamref name="T">Type of enumeration to retrieve values of</typeparam>
        /// <returns>An array that contains the values of the constants in <typeparamref name="T"/>. The elements of the array are sorted by the binary values of the enumeration constants.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is not actual enum type (i.e. it is <see cref="Enum"/>)</exception>
        generic <class T> where T:Enum
        static array<T>^ GetValues(void);

        //TODO: TryParse
    };
}