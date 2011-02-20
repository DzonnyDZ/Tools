#include "Stdafx.h"
using namespace System;
using namespace System::Runtime::CompilerServices;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;
using namespace System::Reflection;

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

        /// <summary>Retrieves an array of the values of the constants in an enumeration.</summary>
        /// <typeparam name="T">Type of enumeration to retrieve values of</typeparam>
        /// <returns>An array that contains the values of the constants in <typeparamref name="T"/>. The elements of the array are sorted by the binary values of the enumeration constants.</returns>
        generic <class T> where T:Enum, gcnew()
        static array<T>^ GetValues(void);

        /// <summary>Determines whether one or more bit fields are set in at least one of dictionary values.</summary>
        /// <typeparam name="TKey">Type of dictionary keys</typeparam>
        /// <typeparam name="TValue">Enumeration type - type of values in dictionary</typeparam>
        /// <param name="dic">A dictionary to test values in</param>
        /// <param name="flag">An enumeration value. - the flag(s) to test</param>
        /// <returns>True if the bit field or bit fields that are set in <paramref name="flag"/> are also set in at least one value in <paramref name="dic"/>; false otherwise or if <paramref name="dic"/> is null.</returns>
        /// <exception cref="ArgumentException"><typeparamref name="TValue"/> is <see cref="Enum"/> (not a derived type) and actual types of <paramref name="flag"/> and value in <paramref name="dic"/> differ.</exception>
        generic <class TKey, class TValue> where TValue:Enum
        [Extension]
        static bool HasFlagSet(IDictionary<TKey, TValue>^ dic, TValue flag);

        /// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object (type-safe).</summary>
        /// <typeparam name="T">An enumeration type.</typeparam>
        /// <param name="value"> A string containing the name or value to convert.</param>
        /// <returns>An object of type enumType whose value is represented by value.</returns>
        /// <exception cref="ArgumentException"><paramref name="value"/> is either an empty string or only contains white space.-or- <paramref name="value"/> is a name, but not one of the named constants defined for the enumeration.</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> is outside the range of the underlying type of <typeparamref name="T"/>.</exception>
        generic <class T> where T:Enum, gcnew()
        static T Parse(String^ value);

        /// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. A parameter specifies whether the operation is case-sensitive. (type-safe)</summary>
        /// <typeparam name="T">An enumeration type.</typeparam>
        /// <param name="value"> A string containing the name or value to convert.</param>
        /// <param name="ignoreCase">true to ignore case; false to regard case.</param>
        /// <returns>An object of type enumType whose value is represented by value.</returns>
        /// <exception cref="ArgumentException"><paramref name="value"/> is either an empty string or only contains white space.-or- <paramref name="value"/> is a name, but not one of the named constants defined for the enumeration.</exception>
        /// <exception cref="OverflowException"><paramref name="value"/> is outside the range of the underlying type of <typeparamref name="T"/>.</exception>
        generic <class T> where T:Enum, gcnew()
        static T Parse(String^ value, bool ignoreCase);

        /// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. The return value indicates whether the conversion succeeded. (type-safe)</summary>
        /// <typeparam name="T">The enumeration type to which to convert value.</typeparam>
        /// <param nem="value">The string representation of the enumeration name or underlying value to convert.</param>
        /// <param name="result">When this method returns, contains an object of type <typeparamref name="T"/> whose value is represented by value. This parameter is passed uninitialized.</param>
        /// <returns>true if the value parameter was converted successfully; otherwise, false.</returns>
        generic <class T> where T:Enum, value class
        static bool TryParse(String^ value, [Out]T% result);

        /// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. A parameter specifies whether the operation is case-sensitive. The return value indicates whether the conversion succeeded. (type-safe)</summary>
        /// <typeparam name="T">The enumeration type to which to convert value.</typeparam>
        /// <param nem="value">The string representation of the enumeration name or underlying value to convert.</param>
        /// <param name="ignoreCase">true to ignore case; false to consider case.</param>
        /// <param name="result">When this method returns, contains an object of type <typeparamref name="T"/> whose value is represented by value. This parameter is passed uninitialized.</param>
        /// <returns>true if the value parameter was converted successfully; otherwise, false.</returns>
        generic <class T> where T:Enum, value class
        static bool TryParse(String^ value, bool ignoreCase, [Out]T% result);

        /// <summary>Gets value indicating if given value is defined as member of an enumeration</summary>
        /// <typeparam name="T">Type of the enumeration</typeparam>
        /// <param name="value">Value to verify</param>
        /// <returns>True if enumeration <typeparamref name="T"/> contains constant with value <paramref name="value"/>; false otherwise.</returns>
        /// <remarks>There is a companion method <see cref2="M:Tools.TypeTools.IsDefined(System.ENum)"/> in assembly Tools.</remarks>
        generic <class T> where T:Enum, gcnew()
        [Extension]
        static bool IsDefined(T value);

        /// <summary>Gets <see cref="FieldInfo"/> that represent given constant within an enum</summary>
        /// <param name="value">Constant to be found</param>
        /// <returns><see cref="FieldInfo"/> of given <paramref name="value"/> if <paramref name="value"/> is member of <typeparamref name="T"/>; null instead</returns>
        /// <typeparam name="T"><see cref="Enum"/> to found constant within</typeparam>
        generic <class T> where T:Enum, gcnew()
        [Extension]
        static FieldInfo^ GetConstant(T value);
    };
}