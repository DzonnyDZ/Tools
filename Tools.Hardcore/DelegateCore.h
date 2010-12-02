#include "Stdafx.h"
using namespace System;
using namespace System::Runtime::CompilerServices;

namespace Tools{
    /// <summary>Contains basic extension methods for working with delegates</summary>
    /// <version version="1.5.3">This class is new in version 1.5.3</version>
    [Extension]
    public ref class DelegateCore{
    private: 
        /// <summary>Private CTor - this is static class</summary>
        DelegateCore(void);
    public:
        /// <summary>Concatenates the invocation lists of two delegates in type-safe way.</summary>
        /// <param name="a">The delegate whose invocation list comes first.</param>
        /// <param name="b">The delegate whose invocation list comes last.</param>
        /// <returns>
        ///     A new delegate with an invocation list that concatenates the invocation lists of <paramref name="a"/> and <paramref name="b"/> in that order.
        ///     Returns <paramref name="a"/> if <paramref name="b"/> is null, returns <paramref name="a"/> if <paramref name="b"/> is a null reference, and returns a null reference if both <paramref name="a"/> and <paramref name="b"/> are null references.
        /// </returns>
        /// <seealso cref="Delegate::Combine(Delegate^,Delegate^)"/>
        generic<class T> where T:Delegate
        [Extension]
        static T CombineWith(T a, T b);

        /// <summary>Concatenates the invocation lists of a delegate and an array of delegates.</summary>
        /// <param name="a">The delegate whose invocation list comes first.</param>
        /// <param name="delegates">The array of delegates to combine.</param>
        /// <returns>
        ///     A new delegate with an invocation list that concatenates the invocation lists of <paramref name="a"/> and the delegates in the <paramref name="delegates"/> array.
        ///     Returns <paramref name="a"/> if <paramref name="delegates"/> is null, contains zero elements, or every entry in <paramref name="a"/> is null.
        ///     Returns null if <paramref name="a"/> is null and <paramref name="delegates"/>, empty or contains only null values.
        /// </returns>
        /// <seealso cref2="M:System.Delegate.Combine(System.Delegate[])"/>
        /// <seelaso cref="Delegate::Combine(array{Delegate^}^)"/>
        generic <class T> where T:Delegate
        [Extension]
        static T CombineWith(T a, ... array<T>^ delegates);

        /// <summary>Removes the last occurrence of the invocation list of a delegate from the invocation list of another delegate.</summary>
        /// <param name="source">The delegate from which to remove the invocation list of <paramref name="value"/>.</param>
        /// <param name="value">The delegate that supplies the invocation list to remove from the invocation list of <paramref name="source"/>.</param>
        /// <returns>
        ///     A new delegate with an invocation list formed by taking the invocation list of <paramref name="source"/> and removing the last occurrence of the invocation list of <paramref name="value"/>,
        ///     if the invocation list of <paramref name="value"/> is found within the invocation list of <paramref name="source"/>.
        ///     Returns <paramref name="source"/> if <paramref name="value"/> is null or if the invocation list of <paramref name="value"/> is not found within the invocation list of <paramref name="source"/>.
        ///     Returns a null reference if the invocation list of <paramref name="source"/> is equal to the invocation list of <paramref name="value"/> or if <paramref name="source"/> is a null reference.
        /// </returns>
        /// <exception cref="System::MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).</exception>
        /// <seealso cref="Delegate::Remove(Delegate,Delegate)"/>
        generic <class T> where T:Delegate
        [Extension]
        static T Remove(T source, T value);

        /// <summary>Removes all occurrences of the invocation list of a delegate from the invocation list of another delegate.</summary>
        /// <param name="source">The delegate from which to remove the invocation list of <paramref name="value"/>.</param>
        /// <param name="value">The delegate that supplies the invocation list to remove from the invocation list of <paramref name="source"/>.</param>
        /// <returns>
        ///     A new delegate with an invocation list formed by taking the invocation list of <paramref name="source"/> and removing all occurrences of the invocation list of <paramref name="value"/>,
        ///     if the invocation list of <paramref name="value"/> is found within the invocation list of <paramref name="source"/>.
        ///     Returns <paramref name="source"/> if <paramref name="value"/> is null or if the invocation list of <paramref name="value"/> is not found within the invocation list of <paramref name="source"/>.
        ///     Returns a null reference if the invocation list of <paramref name="value"/> is equal to the invocation list of <paramref name="source"/>,
        ///     if <paramref name="source"/> contains only a series of invocation lists that are equal to the invocation list of <paramref name="value"/>,
        ///     or if <paramref name="source"/> is a null reference.
        /// </returns>
        /// <exception cref="MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).</exception>
        /// <seealso cref="Delegate::RemoveAll(Delegate,Delegate)"/>
        generic <class T> where T:Delegate
        [Extension]
        static T RemoveAll(T source, T value);

        /// <summary>Returns the invocation list of the delegate (type-safe).</summary>
        /// <param name="delegate">A delegate to get invocation list of</param>
        /// <returns>An array of delegates representing the invocation list of the current delegate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        /// <seealso cref="Delegate::GetInvocationList"/>
        generic <class T> where T:Delegate
        [Extension]
        static array<T>^ InvocationList(T delegate);
    };
}