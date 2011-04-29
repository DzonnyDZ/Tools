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

namespace Tools{namespace TotalCommanderT{
    using namespace System;

    /// <summary>Converts <see cref="FILETIME"/> to <see cref="DateTime"/></summary>
    /// <param name="value">A <see cref="FILETIME"/></param>
    /// <returns>Corresponding <see cref="DateTime"/></returns>
    Nullable<DateTime> FileTimeToDateTime(FILETIME value);
    /// <summary>Converts <see cref="DateTime"/> to <see cref="FILETIME"/></summary>
    /// <param name="value">A <see cref="DateTime"/></param>
    /// <returns>Corresponding <see cref="FILETIME"/></returns>
    FILETIME DateTimeToFileTime(Nullable<DateTime> value);

    /// <summary>Copies ANSI characters from string to character array</summary>
    /// <param name="source"><see cref="String"/> to copy characters from. If null <paramref name="target"/>[0] is set to 0.</param>
    /// <param name="target">Pointer to first character of unmanaged character array to copy charatcers to.</param>
    /// <param name="maxlen">Maximum capacity of the <paramref name="target"/> character array, including terminating null char. If &lt;= 0 nothing is copyied.</param>
    /// <remarks>No more than <paramref name="maxlen"/> - 1 characters are copied</remarks>
    /// <version version="1.5.4">Function now correctly handles <paramref name="maxlen"/> &lt;= 0.</version>
    void StringCopy(String^ source, char* target, int maxlen);
    /// <summary>Copies Unicode characters from string to character array</summary>
    /// <param name="source"><see cref="String"/> to copy characters from. If null <paramref name="target"/>[0] is set to 0.</param>
    /// <param name="target">Pointer to first character of unmanaged character array to copy charatcers to. Array must be initialized to size <paramref name="maxlen"/>.</param>
    /// <param name="maxlen">Maximum capacity of the <paramref name="target"/> character array, including terminating null char. If &lt;= 0 nothing is copyied.</param>
    /// <remarks>No more than <paramref name="maxlen"/> - 1 characters are copied</remarks>
    /// <version version="1.5.4">Method reimplemented. Now it reliably works with Unicode characters and correctly deals with <paramref name="maxlen"/> &lt;= 0 or <paramref name="source"/> null.</version>
    void StringCopy(String^ source, wchar_t* target, int maxlen);

    /// <summary>Creates a <see cref="wchar_t"/> array from <see cref="String"/></summary>
    /// <param name="source">A string to convert</param>
    /// <returns>A <see cref="wchar_t"/> (Unicode characters) array containing characters from <paramref name="source"/> plus one terminating nullchar; null if <paramref name="source"/> is null.</returns>
    /// <remarks>
    /// Implementations of <see cref="StringCopyW(String^)"/> and <see cref="StringCopyA(String^)"/> are completelly different.
    /// This function uses <see cref="PtrToStringChars"/> internally and then copies result of it to another array. The advantage is that resulting array is not const.
    /// <para>Calller is responsible for diposing returned array</para></remarks>
    /// <seealso cref="StringCopyA(String^)"/>
    /// <version version="1.5.4">This function is new in version 1.5.4</version>
    wchar_t* StringCopyW(String^ source);
    /// <summary>Creates a <see cref="char"/> array from <see cref="String"/></summary>
    /// <param name="source">A string to convert</param>
    /// <returns>A <see cref="char"/> (ANSI characters) array containing characters from <paramref name="source"/> plus one terminating nullchar; null if <paramref name="source"/> is null.</returns>
    /// <remarks>Implementations of <see cref="StringCopyW(String^)"/> and <see cref="StringCopyA(String^)"/> are completelly different. <see cref="StringCopyA(String^)"/> uses <see cref="System::Text::Encoding::Default"/>.
    /// <para>Calller is responsible for diposing returned array</para></remarks>
    /// <seealso cref="StringCopyW(String^)"/>
    /// <version version="1.5.4">This function is new in version 1.5.4</version>
    char* StringCopyA(String^ source);

    /// <summary>Converts array of Unicode characters to array of ANSI characters</summary>
    /// <param name="source">An array of Unicode characters to convert to ANSI</param>
    /// <returns>An array of ANSI characters. Null if <paramref name="source"/> is null</returns>
    /// <remarks>Caller is responsible for disposing returned array</remarks>
    /// <version version="1.5.4">This function is new in version 1.5.4</version>
    char* UnicodeToAnsi(const wchar_t* source);
    /// <summary>Converts array of ANSI characters to array of Unicode characters</summary>
    /// <param name="source">An array of ANSI characters to convert to Unicode</param>
    /// <returns>An array of Unicode characters. Null if <paramref name="source"/> is null</returns>
    /// <remarks>Caller is responsible for disposing returned array</remarks>
    /// <version version="1.5.4">This function is new in version 1.5.4</version>
    wchar_t* AnsiToUnicode(const char* source);

    /// <summary>Converts array of Unicode characters to array of ANSI characters and reserves ANSI buffer of given size.</summary>
    /// <param name="source">Pointer to an array of Unicode characters to convert to ANSI</param>
    /// <param name="maxlen">Lenght of buffer to reserve (including terminating nullchar). Maximally <paramref name="maxlen"/> - 1 characters from <paramref name="source"/> is copied to returned array.</param>
    /// <returns>An array of ANSI characters of lenght <paramref name="maxlen"/>. A nullchar is placed after last character from <paramref name="source"/>. If <paramref name="source"/> is null first char of returned array is nullchar. Null if <paramref name="maxlen"/> is &lt;= 0.</returns>
    /// <remarks>Caller is responsible for disposing returned array</remarks>
    /// <version version="1.5.4">This function is new in version 1.5.4</version>
    char* UnicodeToAnsi(const wchar_t* source, int maxlen);
    /// <summary>Converts array of ANSI characters to array of Unicode characters and reserves Unicode buffer of given size.</summary>
    /// <param name="source">Pointer to an array of ANSI characters to convert to Unicode</param>
    /// <param name="maxlen">Lenght of buffer to reserve (including terminating nullchar). Maximally <paramref name="maxlen"/> - 1 characters from <paramref name="source"/> is copied to returned array.</param>
    /// <returns>An array of Unicode characters of lenght <paramref name="maxlen"/>. A nullchar is placed after last character from <paramref name="source"/>. If <paramref name="source"/> is null first char of returned array is nullchar. Null if <paramref name="maxlen"/> is &lt;= 0.</returns>
    /// <remarks>Caller is responsible for disposing returned array</remarks>
    /// <version version="1.5.4">This function is new in version 1.5.4</version>
    wchar_t* AnsiToUnicode(const char* source, int maxlen);

    /// <summary>Copies given number of characters from Unicode character buffer to ANSI character buffer</summary>
    /// <param name="source">Pointer to a Unicode character buffer to copy characters from. If null <paramref name="target"/>[0] is set to 0.</param>
    /// <param name="target">Pointer to an ANSI character buffer of size <paramref name="maxlen"/> to copy characters to. The buffer must be pre-initialized!</param>
    /// <param name="maxlen">Size of <paramref name="target"/> buffer. Maximally <paramref name="maxlen"/> - 1 characters from <paramref name="source"/> is copyied to <paramref name="target"/>. if &lt;= 0 nothing is copyied.</param>
    /// <version version="1.5.4">This method is new in version 1.5.4</version>
    void UnicodeToAnsi(const wchar_t* source, char* target, int maxlen);
    /// <summary>Copies given number of characters from ANSI character buffer to Unicode character buffer</summary>
    /// <param name="source">Pointer to an ANSI character buffer to copy characters from. If null <paramref name="target"/>[0] is set to 0.</param>
    /// <param name="target">Pointer to a Unicode character buffer of size <paramref name="maxlen"/> to copy characters to. The buffer must be pre-initialized!</param>
    /// <param name="maxlen">Size of <paramref name="target"/> buffer. Maximally <paramref name="maxlen"/> - 1 characters from <paramref name="source"/> is copyied to <paramref name="target"/>. if &lt;= 0 nothing is copyied.</param>
    /// <version version="1.5.4">This method is new in version 1.5.4</version>
    void AnsiToUnicode(const char* source, wchar_t* target, int maxlen);
    
    /// <summary>Creates a new instance of the <see cref="String"/> class to the value indicated by a specified pointer to an array of Unicode characters.</summary>
    /// <param name="source">A pointer to a null-terminated array of Unicode characters.</param>
    /// <returns>A new instance of <see cref="String"> initialized to <paramref name="source"/>. Null if <paramref name="source"/> is null.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The current process does not have read access to all the addressed characters.</exception>
    /// <exception cref="ArgumentException"><paramref name="source"> specifies an array that contains an invalid Unicode character, or value specifies an address less than 64000.</exception>
    /// <remarks>This function is basically only a wrapper around <see cref="String"/> CTor</remarks>
    /// <seealso cref="String::.ctor(wchar_t*)"/>
    /// <version version="1.5.4">This overload is new in version 1.5.4</version>
    String^ StringCopy(const wchar_t* source) throw(ArgumentOutOfRangeException, ArgumentException);
    /// <summary>Creates a new instance of the <see cref="String"/> class to the value indicated by a pointer to an array of 8-bit signed integers.</summary>
    /// <param name="source">A pointer to a null-terminated array of 8-bit signed integers.</param>
    /// <returns>A new instance of <see cref="String"> initialized to <paramref name="source"/>. Null if <paramref name="source"/> is null.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The length of the new string to initialize, which is determined by the null termination character of <paramref name="source"/>, is too large to allocate. </exception>
    /// <exception cref="ArgumentException">A new instance of <see cref="String"/> could not be initialized using value, assuming value is encoded in ANSI. </exception>
    /// <exception cref="AccessViolationException"><paramref name="source"/> specifies an invalid address.</exception>
    /// <remarks>This function is basically only a wrapper around <see cref="String"/> CTor</remarks>
    /// <seealso cref="String::.ctor(char*)"/>
    /// <version version="1.5.4">This overload is new in version 1.5.4</version>
    String^ StringCopy(const char* source) throw(ArgumentOutOfRangeException, ArgumentException, AccessViolationException);

    /// <summary>When applied onto method identifies method as not implemented by current implementation of class.</summary>
    /// <remarks>Use this attribute to indicate that your plugin does not implement certain optional method.
    /// <para>By default this attribute is applied on all optional methods, so there is not need to use it if you do not derived your plugin from existing plugin and you want to remove certain functionality.</para>
    /// <para>Is method is decorated with this attribute it mut always throw <see cref="NotSupportedException"/></para>
    /// </remarks>
    [AttributeUsageAttribute(AttributeTargets::Method, Inherited=false)]
    public ref class MethodNotSupportedAttribute : Attribute{
    public:
        /// <summary>CTor - creates new instance of the <see cref="MethodNotSupportedAttribute"/> class</summary>
        MethodNotSupportedAttribute();
    };

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

    ref class PluginIconBaseAttribute;
    ref class FilePluginIconAttribute;
    ref class ResourcePluginIconAttribute;

    /// <summary>Apply this attribute to class implementing Total Commander plugin to precise how the plugin is generated.</summary>
    /// <remarks>To set plugin icon use one of <see cref="PluginIconBaseAttribute"/>-derived classes such as <see cref="FilePluginIconAttribute"/> or <see cref="ResourcePluginIconAttribute"/>.</remarks>
    [AttributeUsageAttribute(AttributeTargets::Class, Inherited=false)]
    public ref class TotalCommanderPluginAttribute : Attribute{
    private:
        /// <summary>Contains value of the <see ctef="Name"/> property</summary>
        initonly String^ name;
    public:
        /// <summary>CTor</summary>
        /// <param name="Name">Name (without extension) of plugin generated. Ignored when null.</param>
        TotalCommanderPluginAttribute(String^ Name);
        /// <summary>Gets name (without extension) of plugin when it is generated. Ignored when null.</summary>
        property String^ Name{String^ get();}
        /// <summary>When set to non-null value serves as value of the <see cref="System::Reflection::AssemblyDescriptionAttribute"/> applied to plugin wrapper assembly.</summary>
        /// <remarks>When null, <see cref="System::Reflection::AssemblyDescriptionAttribute"/> from type's assembly is used.</remarks> 
        property String^ AssemblyDescription;
        /// <summary>When set to non-null value serves as value of the <see cref="System::Reflection::AssemblyTitleAttribute"/> applied to plugin wrapper assembly.</summary>
        /// <remarks>When null, <see cref="System::Reflection::AssemblyTitleAttribute"/> from type's assembly is used.</remarks>
        property String^ AssemblyTitle;
        /// <summary>When set to non-null value serves as value of the <see2 cref2="System::Runtime::InteropServices::GuidAttribute"/> applied to plugin wrapper assembly.</summary>
        /// <remarks>When null, no  <see2 cref2="System::Runtime::InteropServices::GuidAttributte"/> is attached</remarks>
        property String^ AssemblyGuid;
    };

    /// <summary>Apply this attribute to Total Commander plugin class to make plugin generator ignore it</summary>
    /// <remarks>This attribute may be usefull when you have non-abstract base class that you don't wan't plugin to be generated for</remarks>
    [AttributeUsage(AttributeTargets::Class, Inherited=false)]
    public ref class NotAPluginAttribute : Attribute{
    public:
        /// <summary>CTor - creates new instance of the <see cref="NotAPluginAttribute"/> class</summary>
        NotAPluginAttribute();
    };

    /// <summary>Base class for plugin icon attributes</summary>
    [AttributeUsage(AttributeTargets::Class, Inherited=false)]
    public ref class PluginIconBaseAttribute abstract : Attribute{
    protected:
        /// <summary>CTor</summary>
        PluginIconBaseAttribute();
    public:
        /// <summary>When overriden in derived class gets the icon</summary>
        /// <param name="AttributeTarget">Type attribute is applied onto. May be ignored by actual implementation.</param>
        /// <returns>The icon</returns>
        /// <exception cref="ArgumentNullException"><paramref name="AttributeTarget"/> is null</exception>
        /// <exception cref="InvalidOperationException">The icon referenced by this attribute cannot be found</exception>
        virtual Drawing::Icon^ getIcon(Type^ AttributeTarget) abstract;
    };

    /// <summary>Specifies plugin icon stored in ICO file</summary>
    [AttributeUsage(AttributeTargets::Class, Inherited=false)]
    public ref class FilePluginIconAttribute : PluginIconBaseAttribute{
    private:
        /// <summary>Contains value of the <see cref="IconPath"/> property</summary>
        initonly String^ iconPath;
    public:
        /// <summary>CTor</summary>
        /// <param name="IconPath">Path of ICO file that contains icon of plugin. Absolute or relative to assembly where type attribute si applied onto is declared.</param>
        FilePluginIconAttribute(String^ IconPath);
        /// <summary>Gets the icon</summary>
        /// <param name="AttributeTarget">Type attribute is applied onto.</param>
        /// <returns>The icon</returns>
        /// <exception cref="ArgumentNullException"><paramref name="AttributeTarget"/> is null</exception>
        /// <exception cref="InvalidOperationException">The icon referenced by this attribute cannot be found. i.e. the file cannot be found.</exception>
        virtual Drawing::Icon^ getIcon(Type^ AttributeTarget) override;
        /// <summary>Gets path of ICO file that contains icon of plugin.</summary>
        /// <returns>Path of ICO file that contains icon of plugin. Absolute or relative to assembly where type attribute si applied onto is declared.</returns>
        property String^ IconPath{String^ get();}
    };

    /// <summary>Specifies plugin icon stored in resource</summary>
    [AttributeUsage(AttributeTargets::Class, Inherited=false)]
    public ref class ResourcePluginIconAttribute : PluginIconBaseAttribute{
    private:
        /// <summary>Contains value of the <see cref="Assembly"/> property</summary>
        initonly Reflection::Assembly^ assembly;
        /// <summary>Contains value of the <see cref="ResourceName"/> property</summary>
        initonly String^ resourceName;
        /// <summary>Contains value of the <see cref="ItemName"/> property</summary>
        initonly String^ itemName;
    public:
        /// <summary>CTor for embdeded ico resource</summary>
        /// <param name="TypeInAssembly">Any type in assembly resource is defined in</param>
        /// <param name="ResourceName">Name of embdeded resource of type icon</param>
        /// <exception cref="ArgumentNullException"><paramref name="TypeInAssembly"/> is null</exception>
        ResourcePluginIconAttribute(Type^ TypeInAssembly, String^ ResourceName);
        /// <summary>CTor for embdeded resx resource</summary>
        /// <param name="TypeInAssembly">Any type in assembly resource is defined in</param>
        /// <param name="ResourceName">Name of resource of type resx</param>
        /// <param name="ResourceItem">Name of item in resource. The type of item must be <see cref="System::Drawing::Icon"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="TypeInAssembly"/> is null</exception>
        ResourcePluginIconAttribute(Type^ TypeInAssembly, String^ ResourceName, String^ ResourceItem);
        /// <summary>Gets the icon</summary>
        /// <param name="AttributeTarget">Ignored, may be null.</param>
        /// <returns>The icon</returns>
        /// <exception cref="InvalidOperationException">The icon referenced by this attribute cannot be found. I.e. the resource name is invalid.</exception>
        virtual Drawing::Icon^ getIcon(Type^ AttributeTarget) override;
        /// <summary>Gets assembly icon resource is defined in</summary>
        property Reflection::Assembly^ Assembly{Reflection::Assembly^ get();}
        /// <summary>Get name of embdeded resource that contains the icon</summary>
        property String^ ResourceName{String^ get();}
        /// <summary>Gets name of item of resource of type resx that contains the icon. If null resource <see cref="ResourceName"/> must be icon itself. If not null resource <see cref="ResourceName"/> must be resx resource.</summary>
        property String^ ItemName{String^ get();}
    };

#pragma warning (push)
#pragma warning(disable : 4290)
    /// <summary>Populates given <see cref="ptimeformat"/> pointer with values of given <see cref="TimeSpan"/></summary>
    /// <param name="target">Pointer to populate values of</param>
    /// <param name="source">Instance to populate <paramref name="target"/> with values of</param>
    /// <exception cref="ArgumentNullException"><paramref name="target"/> is null pointer</exception>
    /// <version version="1.5.3">This method is new in version 1.5.3</version>
    void PopulateWith(ptimeformat target, TimeSpan source) throw(ArgumentNullException);
    /// <summary>Converts <see cref="ptimeformat"/> to <see cref="TimeSpan"/></summary>
    /// <param name="source">A <see cref="ptimeformat"/></param>
    /// <returns><see cref="TimeSpan"/> populated from <paramref name="source"/></returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is null pointer</exception>
    /// <version version="1.5.3">This cast operator is new in version 1.5.3</version>
    TimeSpan TimeToTimeSpan(ptimeformat source) throw(ArgumentNullException);
#pragma warning(default : 4290)
#pragma warning (pop)
    

}}