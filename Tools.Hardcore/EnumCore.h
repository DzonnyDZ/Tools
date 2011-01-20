#include "Stdafx.h"
using namespace System;
using namespace System::Runtime::CompilerServices;
using namespace System::Collections::Generic;

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
        /// <param name="flag">An enumeration value. - the flag(s) to test</param>
        /// <returns>true if the bit field or bit fields that are set in <paramref name="flag"/> are also set in <paramref name="value"/>; otherwise, false.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="T"/> is <see cref="Enum"/> (not a derived type) and actual types of <paramref name="value"/> and <paramref name="flag"/> differ.</exception>
        /// <seelaso cref="Enum::HasFlag"/>
        generic <class T> where T:Enum
        [Extension]
        static bool HasFlagSet(T value, T flag);

        /// <summary>Retrieves an array of the values of the constants in an enumeration.</summmary>
        /// <typeparamref name="T">Type of enumeration to retrieve values of</typeparam>
        /// <returns>An array that contains the values of the constants in <typeparamref name="T"/>. The elements of the array are sorted by the binary values of the enumeration constants.</returns>
        generic <class T> where T:Enum, gcnew()
        static array<T>^ GetValues(void);

        /// <summary>Determines whether one or more bit fields are set in at least one of dictionary values.</summary>
        /// <typeparamref name="TKey">Type of dictionary keys</typeparamref>
        /// <typeparamref name="TValue">Enumeration type - type of values in dictionary</typeparamref>
        /// <param name="dic">A dictionary to test values in</param>
        /// <param name="flag">An enumeration value. - the flag(s) to test</param>
        /// <returns>True if the bit field or bit fields that are set in <paramref name="flag"/> are also set in at least one value in <paramref name="dic"/>; false otherwise or if <paramref name="dic"/> is null.</param>
        /// <exception cref="ArgumentException"><typeparamref name="TValue"/> is <see cref="Enum"/> (not a derived type) and actual types of <paramref name="flag"/> and value in <paramref name="dic"/> differ.</exception>
        generic <class TKey, class TValue> where TValue:Enum
        [Extension]
        static bool HasFlagSet(IDictionary<TKey, TValue>^ dic, TValue flag);

        //TODO: TryParse
    };
}