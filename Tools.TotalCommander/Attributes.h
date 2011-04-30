#pragma once
#include "Common.h"

using namespace System;

namespace Tools{namespace TotalCommanderT{

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

}}
    