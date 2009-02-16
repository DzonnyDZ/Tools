typedef unsigned __int64 QWORD; 
#pragma once
#include "fsplugin.h"
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
    };
}}