typedef unsigned __int64 QWORD; 
#pragma once
#include "Plugin\fsplugin.h"
#include "Plugin\contplug.h"
#include "Date.h"

#pragma make_public(WIN32_FIND_DATA)
#pragma make_public(RemoteInfoStruct)
#pragma make_public(FILETIME)
#pragma make_public(HICON__)
#pragma make_public(FsDefaultParamStruct)
#pragma make_public(HBITMAP__)

#define MAKE_PUBLIC [Tools::RuntimeT::CompilerServicesT::MakePublicAttribute(Remove = true)] 

namespace Tools{namespace TotalCommanderT{
    using namespace System;

    /// <summary>Converts <see cref="FILETIME"/> to <see cref="DateTime"/></summary>
    /// <param name="value">A <see cref="FILETIME"/></param>
    /// <returns>Corresponding <see cref="DateTime"/></returns>
    /// <version version="1.5.4">The limitation of C++/CLI (unable to create public methods) was worked around (via <see cref="Tools::RuntimeT::CompilerServicesT::MakePublicAttribute"/>), so this method is now public.</version>
    MAKE_PUBLIC Nullable<DateTime> FileTimeToDateTime(FILETIME value);
    /// <summary>Converts <see cref="DateTime"/> to <see cref="FILETIME"/></summary>
    /// <param name="value">A <see cref="DateTime"/></param>
    /// <returns>Corresponding <see cref="FILETIME"/></returns>
    /// <version version="1.5.4">The limitation of C++/CLI (unable to create public methods) was worked around (via <see cref="Tools::RuntimeT::CompilerServicesT::MakePublicAttribute"/>), so this method is now public.</version>
    MAKE_PUBLIC FILETIME DateTimeToFileTime(Nullable<DateTime> value);

    /// <summary>Copies ANSI characters from string to character array</summary>
    /// <param name="source"><see cref="String"/> to copy characters from. If null <paramref name="target"/>[0] is set to 0.</param>
    /// <param name="target">Pointer to first character of unmanaged character array to copy charatcers to.</param>
    /// <param name="maxlen">Maximum capacity of the <paramref name="target"/> character array, including terminating null char. If &lt;= 0 nothing is copyied.</param>
    /// <remarks>No more than <paramref name="maxlen"/> - 1 characters are copied</remarks>
    /// <version version="1.5.4">Function now correctly handles <paramref name="maxlen"/> &lt;= 0.</version>
    /// <version version="1.5.4">The limitation of C++/CLI (unable to create public methods) was worked around (via <see cref="Tools::RuntimeT::CompilerServicesT::MakePublicAttribute"/>), so this method is now public.</version>
    MAKE_PUBLIC void StringCopy(String^ source, char* target, int maxlen);
    /// <summary>Copies Unicode characters from string to character array</summary>
    /// <param name="source"><see cref="String"/> to copy characters from. If null <paramref name="target"/>[0] is set to 0.</param>
    /// <param name="target">Pointer to first character of unmanaged character array to copy charatcers to. Array must be initialized to size <paramref name="maxlen"/>.</param>
    /// <param name="maxlen">Maximum capacity of the <paramref name="target"/> character array, including terminating null char. If &lt;= 0 nothing is copyied.</param>
    /// <remarks>No more than <paramref name="maxlen"/> - 1 characters are copied</remarks>
    /// <version version="1.5.4">Method reimplemented. Now it reliably works with Unicode characters and correctly deals with <paramref name="maxlen"/> &lt;= 0 or <paramref name="source"/> null.</version>
    /// <version version="1.5.4">The limitation of C++/CLI (unable to create public methods) was worked around (via <see cref="Tools::RuntimeT::CompilerServicesT::MakePublicAttribute"/>), so this method is now public.</version>
    MAKE_PUBLIC void StringCopy(String^ source, wchar_t* target, int maxlen);

    /// <summary>Creates a <see cref="wchar_t"/> array from <see cref="String"/></summary>
    /// <param name="source">A string to convert</param>
    /// <returns>A <see cref="wchar_t"/> (Unicode characters) array containing characters from <paramref name="source"/> plus one terminating nullchar; null if <paramref name="source"/> is null.</returns>
    /// <remarks>
    /// Implementations of <see cref="StringCopyW(String^)"/> and <see cref="StringCopyA(String^)"/> are completelly different.
    /// This function uses <see cref="PtrToStringChars"/> internally and then copies result of it to another array. The advantage is that resulting array is not const.
    /// <para>Calller is responsible for diposing returned array</para></remarks>
    /// <seealso cref="StringCopyA(String^)"/>
    /// <version version="1.5.4">This function is new in version 1.5.4</version>
    MAKE_PUBLIC wchar_t* StringCopyW(String^ source);
    /// <summary>Creates a <see cref="char"/> array from <see cref="String"/></summary>
    /// <param name="source">A string to convert</param>
    /// <returns>A <see cref="char"/> (ANSI characters) array containing characters from <paramref name="source"/> plus one terminating nullchar; null if <paramref name="source"/> is null.</returns>
    /// <remarks>Implementations of <see cref="StringCopyW(String^)"/> and <see cref="StringCopyA(String^)"/> are completelly different. <see cref="StringCopyA(String^)"/> uses <see cref="System::Text::Encoding::Default"/>.
    /// <para>Calller is responsible for diposing returned array</para></remarks>
    /// <seealso cref="StringCopyW(String^)"/>
    /// <version version="1.5.4">This function is new in version 1.5.4</version>
    MAKE_PUBLIC char* StringCopyA(String^ source);

    /// <summary>Converts array of Unicode characters to array of ANSI characters</summary>
    /// <param name="source">An array of Unicode characters to convert to ANSI</param>
    /// <returns>An array of ANSI characters. Null if <paramref name="source"/> is null</returns>
    /// <remarks>Caller is responsible for disposing returned array</remarks>
    /// <version version="1.5.4">This function is new in version 1.5.4</version>
    MAKE_PUBLIC char* UnicodeToAnsi(const wchar_t* source);
    /// <summary>Converts array of ANSI characters to array of Unicode characters</summary>
    /// <param name="source">An array of ANSI characters to convert to Unicode</param>
    /// <returns>An array of Unicode characters. Null if <paramref name="source"/> is null</returns>
    /// <remarks>Caller is responsible for disposing returned array</remarks>
    /// <version version="1.5.4">This function is new in version 1.5.4</version>
    MAKE_PUBLIC wchar_t* AnsiToUnicode(const char* source);

    /// <summary>Converts array of Unicode characters to array of ANSI characters and reserves ANSI buffer of given size.</summary>
    /// <param name="source">Pointer to an array of Unicode characters to convert to ANSI</param>
    /// <param name="maxlen">Lenght of buffer to reserve (including terminating nullchar). Maximally <paramref name="maxlen"/> - 1 characters from <paramref name="source"/> is copied to returned array.</param>
    /// <returns>An array of ANSI characters of lenght <paramref name="maxlen"/>. A nullchar is placed after last character from <paramref name="source"/>. If <paramref name="source"/> is null first char of returned array is nullchar. Null if <paramref name="maxlen"/> is &lt;= 0.</returns>
    /// <remarks>Caller is responsible for disposing returned array</remarks>
    /// <version version="1.5.4">This function is new in version 1.5.4</version>
    MAKE_PUBLIC char* UnicodeToAnsi(const wchar_t* source, int maxlen);
    /// <summary>Converts array of ANSI characters to array of Unicode characters and reserves Unicode buffer of given size.</summary>
    /// <param name="source">Pointer to an array of ANSI characters to convert to Unicode</param>
    /// <param name="maxlen">Lenght of buffer to reserve (including terminating nullchar). Maximally <paramref name="maxlen"/> - 1 characters from <paramref name="source"/> is copied to returned array.</param>
    /// <returns>An array of Unicode characters of lenght <paramref name="maxlen"/>. A nullchar is placed after last character from <paramref name="source"/>. If <paramref name="source"/> is null first char of returned array is nullchar. Null if <paramref name="maxlen"/> is &lt;= 0.</returns>
    /// <remarks>Caller is responsible for disposing returned array</remarks>
    /// <version version="1.5.4">This function is new in version 1.5.4</version>
    MAKE_PUBLIC wchar_t* AnsiToUnicode(const char* source, int maxlen);

    /// <summary>Copies given number of characters from Unicode character buffer to ANSI character buffer</summary>
    /// <param name="source">Pointer to a Unicode character buffer to copy characters from. If null <paramref name="target"/>[0] is set to 0.</param>
    /// <param name="target">Pointer to an ANSI character buffer of size <paramref name="maxlen"/> to copy characters to. The buffer must be pre-initialized!</param>
    /// <param name="maxlen">Size of <paramref name="target"/> buffer. Maximally <paramref name="maxlen"/> - 1 characters from <paramref name="source"/> is copyied to <paramref name="target"/>. if &lt;= 0 nothing is copyied.</param>
    /// <version version="1.5.4">This method is new in version 1.5.4</version>
    MAKE_PUBLIC void UnicodeToAnsi(const wchar_t* source, char* target, int maxlen);
    /// <summary>Copies given number of characters from ANSI character buffer to Unicode character buffer</summary>
    /// <param name="source">Pointer to an ANSI character buffer to copy characters from. If null <paramref name="target"/>[0] is set to 0.</param>
    /// <param name="target">Pointer to a Unicode character buffer of size <paramref name="maxlen"/> to copy characters to. The buffer must be pre-initialized!</param>
    /// <param name="maxlen">Size of <paramref name="target"/> buffer. Maximally <paramref name="maxlen"/> - 1 characters from <paramref name="source"/> is copyied to <paramref name="target"/>. if &lt;= 0 nothing is copyied.</param>
    /// <version version="1.5.4">This method is new in version 1.5.4</version>
    MAKE_PUBLIC void AnsiToUnicode(const char* source, wchar_t* target, int maxlen);
    
    /// <summary>Creates a new instance of the <see cref="String"/> class to the value indicated by a specified pointer to an array of Unicode characters.</summary>
    /// <param name="source">A pointer to a null-terminated array of Unicode characters.</param>
    /// <returns>A new instance of <see cref="String"> initialized to <paramref name="source"/>. Null if <paramref name="source"/> is null.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The current process does not have read access to all the addressed characters.</exception>
    /// <exception cref="ArgumentException"><paramref name="source"> specifies an array that contains an invalid Unicode character, or value specifies an address less than 64000.</exception>
    /// <remarks>This function is basically only a wrapper around <see cref="String"/> CTor</remarks>
    /// <seealso cref="String::.ctor(wchar_t*)"/>
    /// <version version="1.5.4">This overload is new in version 1.5.4</version>
    MAKE_PUBLIC String^ StringCopy(const wchar_t* source) throw(ArgumentOutOfRangeException, ArgumentException);
    /// <summary>Creates a new instance of the <see cref="String"/> class to the value indicated by a pointer to an array of 8-bit signed integers.</summary>
    /// <param name="source">A pointer to a null-terminated array of 8-bit signed integers.</param>
    /// <returns>A new instance of <see cref="String"> initialized to <paramref name="source"/>. Null if <paramref name="source"/> is null.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The length of the new string to initialize, which is determined by the null termination character of <paramref name="source"/>, is too large to allocate. </exception>
    /// <exception cref="ArgumentException">A new instance of <see cref="String"/> could not be initialized using value, assuming value is encoded in ANSI. </exception>
    /// <exception cref="AccessViolationException"><paramref name="source"/> specifies an invalid address.</exception>
    /// <remarks>This function is basically only a wrapper around <see cref="String"/> CTor</remarks>
    /// <seealso cref="String::.ctor(char*)"/>
    /// <version version="1.5.4">This overload is new in version 1.5.4</version>
    MAKE_PUBLIC String^ StringCopy(const char* source) throw(ArgumentOutOfRangeException, ArgumentException, AccessViolationException);

    /// <summary>Recognized Total Commander plugin types</summary>
    [FlagsAttribute]
    public enum class PluginType{
        /// <summary>wfx: File system plugin for accessing file on devices, servers etc.</summary>
        FileSystem = 1,
        /// <summary>wlx: Lister plugin for showing file preview (on F3)</summary>
        Lister = 2,
        /// <summary>wdx: Content plugin providing custom properties of files</summary>
        Content = 4,
        /// <summary>wcx: Packer plugin providing access to content of archive files</summary>
        Packer = 8
    };

#pragma warning (push)
#pragma warning(disable : 4290)
    /// <summary>Populates given <see cref="ptimeformat"/> pointer with values of given <see cref="TimeSpan"/></summary>
    /// <param name="target">Pointer to populate values of</param>
    /// <param name="source">Instance to populate <paramref name="target"/> with values of</param>
    /// <exception cref="ArgumentNullException"><paramref name="target"/> is null pointer</exception>
    /// <version version="1.5.3">This method is new in version 1.5.3</version>
    /// <version version="1.5.4">As of version 1.5.4 this method is intensionally non-public. Before it was non-public because of limitations of C++/CLI.</version>
    void PopulateWith(ptimeformat target, TimeSpan source) throw(ArgumentNullException);
    /// <summary>Converts <see cref="ptimeformat"/> to <see cref="TimeSpan"/></summary>
    /// <param name="source">A <see cref="ptimeformat"/></param>
    /// <returns><see cref="TimeSpan"/> populated from <paramref name="source"/></returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is null pointer</exception>
    /// <version version="1.5.3">This cast operator is new in version 1.5.3</version>
    /// <version version="1.5.4">As of version 1.5.4 this method is intensionally non-public. Before it was non-public because of limitations of C++/CLI.</version>
    TimeSpan TimeToTimeSpan(ptimeformat source) throw(ArgumentNullException);
#pragma warning(default : 4290)
#pragma warning (pop)
    

}}