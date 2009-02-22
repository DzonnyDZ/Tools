typedef unsigned __int64 QWORD; 
#pragma once
#include "Plugin\fsplugin.h"
#include "Common.h"

namespace Tools{namespace TotalCommanderT{
    using namespace System;
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
    
    /// <summary>When applied onto method, identifies the method as part of plugin contract</summary>
    /// <remarks>All plugin methods (from Total Commander point of view - not the methods derived class implements) must me marked with this attribute. Do not use this attribute of multiple methods serving same purpose (i.e. when method is overloaded to be calable from outside of assembly).</remarks>
    [AttributeUsageAttribute(AttributeTargets::Method, Inherited=false)]
    private ref class PluginMethodAttribute : Attribute{ 
    private:
        /// <summary>Contains value of the <see cref="DefinedBy"/> property</summary>
        String^ definedBy;
        /// <summary>Contains value of the <see cref="ImplementedBy"/> property</summary>
        String^ implementedBy;
        /// <summary>Initializes newly created instance</sumary>
        /// <param name="DefinedBy">Name of macro to be set method to be compiled into plugin wrapper. Generator must define this macro always.</param>
        /// <exception cref="ArgumentNullException"><paramref name="DefinedBy"/> is null</exception>
        /// <exception cref="FormatException"><paramref name="DefinedBy"/> has invalid format for macro name</exception>
        void init(String^ DefinedBy);
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
    
    /// <summary>Apply this attribute to class implementing Total Commander plugin to precise how the plugin is generated.</summary>
    [AttributeUsageAttribute(AttributeTargets::Class, Inherited=false)]
    public ref class TotalCommanderPluginAttribute : Attribute{
    private:
        /// <summary>Contains value of the <see ctef="Name"/> property</summary>
        String^ name;
    public:
        /// <summary>CTor</summary>
        /// <param name="Name">Name (without extension) of plugin generated. Ignored when null.</param>
        TotalCommanderPluginAttribute(String^ Name);
        /// <summary>Gets name (without extension) of plugin when it is generated. Ignored when null.</summary>
        property String^ Name{String^ get();}
        /// <summary>When set to non-null value serves as value of the <see cref="AssemblyDescriptionAttribute"/> applied to plugin wrapper assembly.</summary>
        /// <remarks>When null, <see cref="AssemblyDescriptionAttribute"/> from type's assembly is used.</remarks> 
        property String^ AssemblyDescription;
        /// <summary>When set to non-null value serves as value of the <see cref="AssemblyTitleAttribute"/> applied to plugin wrapper assembly.</summary>
        /// <remarks>When null, <see cref="AssemblyTitleAttribute"/> from type's assembly is used.</remarks>
        property String^ AssemblyTitle;
        /// <summary>When set to non-null value serves as value of the <see cref="AssemblyGuidAttribute"/> applied to plugin wrapper assembly.</summary>
        /// <remarks>When null, no  <see cref="AssemblyGuidAttribute"/> is attached</remarks>
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
}}