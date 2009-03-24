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
    /// <param name="source"><see cref="String"/> to copy characters from</param>
    /// <param name="target">Pointer to first character of unmanaged character array to copy charatcers to</param>
    /// <param name="maxlen">Maximum capacity of the <paramref name="target"/> character array, including terminating null char</param>
    /// <remarks>No more than <paramref name="maxlen"/> - 1 characters are copied</remarks>
    void StringCopy(String^ source, char* target, int maxlen);
    /// <summary>Copies Unicode characters from string to character array</summary>
    /// <param name="source"><see cref="String"/> to copy characters from</param>
    /// <param name="target">Pointer to first character of unmanaged character array to copy charatcers to</param>
    /// <param name="maxlen">Maximum capacity of the <paramref name="target"/> character array, including terminating null char</param>
    /// <remarks>No more than <paramref name="maxlen"/> - 1 characters are copied</remarks>
    void StringCopy(String^ source, wchar_t* target, int maxlen);

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
    
    /// <summary>When applied onto method, identifies the method as part of plugin contract</summary>
    /// <remarks>All plugin methods (from Total Commander point of view - not the methods derived class implements) must me marked with this attribute. Do not use this attribute of multiple methods serving same purpose (i.e. when method is overloaded to be calable from outside of assembly).</remarks>
    [AttributeUsageAttribute(AttributeTargets::Method, Inherited=false)]
    private ref class PluginMethodAttribute : Attribute{ 
    private:
        /// <summary>Contains value of the <see cref="DefinedBy"/> property</summary>
        initonly String^ definedBy;
        /// <summary>Contains value of the <see cref="ImplementedBy"/> property</summary>
        initonly String^ implementedBy;
        /*/// <summary>Contains value of the <see cref="ExportedAs"/> property</summary>
        String^ exportedAs;
        /// <summary>Initializes newly created instance</summary>
        /// <param name="DefinedBy">Name of macro to be set method to be compiled into plugin wrapper. Generator must define this macro always.</param>
        /// <exception cref="ArgumentNullException"><paramref name="DefinedBy"/> is null</exception>
        /// <exception cref="FormatException"><paramref name="DefinedBy"/> has invalid format for macro name</exception>
        void init(String^ DefinedBy);*/
    public:
        /// <summary>CTor for always-present method</summary>
        /// <param name="DefinedBy">Name of macro to be set method to be compiled into plugin wrapper. Generator must define this macro always.</param>
        /// <exception cref="ArgumentNullException"><paramref name="DefinedBy"/> is null</exception>
        /// <exception cref="FormatException"><paramref name="DefinedBy"/> has invalid format for macro name</exception>
        PluginMethodAttribute(String^ DefinedBy);
        /// <summary>CTor for always-present method</summary>
        /// <param name="DefinedBy">Name of macro to be set method to be compiled into plugin wrapper. Generator must define this macro when method indicated by <paramref name="ImplementedBy"/> has not <see cref="MethodNotSupportedAttribute"/>.</param>
        /// <param name="ImplementedBy">Name of corresponding derived-class implemeneted method, to check for <see cref="MethodNotSupportedAttribute"/> to identify if this method should be included in plugin. See <see cref="ImplementedBy"/> for details.</param>
        /// <exception cref="ArgumentNullException"><paramref name="DefinedBy"/> is null</exception>
        /// <exception cref="ArgumentException"><paramref name="DefinedBy"/> has invalid format for macro name</exception>
        PluginMethodAttribute(String^ ImplementedBy, String^ DefinedBy);
        /// <summary>Gets name of macro that must be defined (using #define) the method to be compiled in plugin wrapper project</summary>
        property String^ DefinedBy{String^ get();}
        /// <summary>Gets name of corresponding derived-class implemeneted method, to check for <see cref="MethodNotSupportedAttribute"/> to identify if this method should be included in plugin (by definin macro which name is defined by <see cref="DefinedBy"/>.</summary>
        /// <remarks>The method with this name must exist. It must not be overloaded in plugin abstract base class. When null, method will be compiled always.</remarks>
        property String^ ImplementedBy{String^ get();}
        /*/// <summary>Gets or sets name of the function under which it is exported to unmanaged code</summary>
        /// <returns>Name to export function under. This name is written by Total Commander Plugin Generator to the *.def file. If null actual name of fuction is used.</returns>
        /// <value>Name of function to write to the *.def file. This property must be se directly, it cannot be set via CTor. Default value is null</value>
        /// <remarks>If function name starts with "<c>*</c>", * is replaced with plugin name perfix acorting to actual plugin type being generated:
        /// <list type="table"><listheader><term>Plugin type</term><description>Prefix replacing {0} in exported function name</descrption></listheader>
        /// <item><term>File system (wfx)</term><description>"<c>Fs</c>"</description></item>
        /// <item><term>Content (wdx)</term><description>"<c>Content</c>"</description></item>
        /// <item><term>Lister (wlx)</term><description>"<c>List</c>"</description></item>
        /// <ite><term>Packer (wcx)</term><description>No prefix (an empty string)</descritpin></item></list>
        /// If function name starts with <c>"[*identifier]"</c> or <c>"[identifier*]"</c> (where <c>identifier</c> is any valid C++ identifier) * is replaced with plugin type identification (see above) and <c>identifier</c> is kept as is. In case <c>identifier</c> is same as * replacement (plugin type code), only <c>identifier</c> is emitted. [] are not emmited, of course.
        /// <para>"<c>#</c>" is replaced with actual function name.</remarks>
        /// <seealso cref="GetExportedAs"/>
        /// <version version="1.5.3">This property is new in version 1.5.3</version>
        property String^ ExportedAs{String^ get(); void set(String^);}
        /// <summary>Gets string representing name under which the function shall be exported to unmanaged code</summary>
        /// <param name="pluginType">Type of plugin being generated (one of the <see cref="PluginType"/> values, not a OR-combination!)</param>
        /// <param name="functionName">Actual name of function being generated</param>
        /// <returns><see cref="ExportedAs"/> formated using rules specified in documentation of <see cref="ExportedAs"/>. Returns <paramref name="functionName"/> when <see cref="ExportedAs"/> is null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="functionName"/> is null</exception>
        /// <exception cref="ComponentModel::InvalidEnumArgumentException"><paramref name="pluginType"/> is not one of the <see cref="PluginType"/> values (i.e. it ORs two or more of them).</exception>
        /// <seealso cref="ExportedAs"/>
        /// <version version="1.5.3">This function is new in version 1.5.3</version>
        String^ GetExportedAs(PluginType pluginType, String^ functionName);
        /// <summary>Gets or sets additiona condition that must be fullfilled the function to be exported</summary>
        /// <returns>The contition as <c>constant-expresstion</c>; null when condition is always true</returns>
        /// <value>Default value is null which mean the condition is always true</value>
        /// <remarks>The condition id any <c>constant-expression</c> that fan be used as parameter of the C++ <c>#if</c> directive. The directive is issued with this condition. If this property is null, no directive is issued.</remarks>
        /// <version version="1.5.3">This property is new in version 1.5.3</version>
        property String^ AdditionalCondition;*/
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