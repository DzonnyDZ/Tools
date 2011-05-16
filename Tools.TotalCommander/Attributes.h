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
        /// <summary>Gets value indicating if given method is supported.</summary>
        /// <param name="method">A method to be checked. Usually the most derived method is passed here</param>
        /// <returns>
        /// True if the method is supported, false if it is not supported.
        /// Returns tur if the method is not decorated with <see cref="MethodNotSupportedAttribute"/>.
        /// If it is decorated with <see cref="MethodNotSupportedAttribute"/> or derived attribute returns its <see cref="IsSupported"/>.
        /// </returns>
        /// <exception cref="ArgumentNulllException"><paramref name="method"/> is null</exception>
        /// <exception cref="InvalidOperationException"><see cref="MethodNotSupportedAttribute"/> is specified more than once on <paramref name="method"/></exception>
        /// <version version="1.5.4">This function is new in version 1.5.4</version>
        static bool Supported(Reflection::MethodInfo^ method);
        /// <summary>Gets value indicating if this instance of <see cref="MethodNotSupportedAttribute"/> indicates supported or unsupported method</summary>
        /// <param name="method">Pass actual method this instance was applied on here. Derived implementation may use it or may ignore it. This implementation ignores it.</param>
        /// <exception cref="ArgumentNulllException">Derived class may throw this exception if <paramref name="method"/> is null</exception>
        /// <returns>True if this instance indicates supported method, false if this instance indicates non supported method. This implementation always returns true.</returns>
        /// <version version="1.5.4">This function is new in version 1.5.4</version>
        virtual bool IsSupported(Reflection::MethodInfo^ method);
    };

    /// <summary>Allows to read <see cref="MethodNotSupportedAttribute"/> from different code member (redirect it)</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    [AttributeUsageAttribute(AttributeTargets::Method, Inherited=false)]
    public ref class MethodNotSupportedRedirectAttribute sealed: MethodNotSupportedAttribute{
    private:
        initonly String^ method;
    public:
        /// <summary>CTor - creates a new instance of the <see cref="MethodNotSupportedRedirectAttribute"/> class</summary>
        /// <param name="method">Name of method to read <see cref="MethodNotSupportedAttribute"/> from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        /// <remarks>You should specify target type using various properties of this class. If no target target type is not specified actual type method this attribute is applied on is member of will be used.</remarks>
        MethodNotSupportedRedirectAttribute(String^ method);

        /// <summary>Gets or sets type to redirect attribute lookup to</summary>
        /// <remarks>If null the type method the attribute is specified on is member of is used</remarks>
        property System::Type^ Type;
        /// <summary>Gets or sets name of <see cref="Type"/>'s generic parameter to redirect lookup to actual type of.</summary>
        /// <remarks>
        /// If this property is not null the attribute is redirected to method of type supplied for generic parameter named <see cref="GPar"/> of type <see cref="Type"/>.
        /// If this property is null the attribute is redirected to method of type <see cref="Type"/>.
        /// </remarks>
        property String^ GPar;
        /// <summary>Name of method to redirect attribute to</summary>
        /// <remarks>If multiple methods of this name are difined on traget type specify method signature using <see cref="MethodParameters"/> otherwise <see cref="System::Reflection::AmbiguousMatchException"/> will be thrown.</remarks>
        property String^ Method{String^ get();}
        /// <summary>Gets or sets types of method parameters used to find appropriate method to read <see cref="MethodNotSupportedAttribute"/> from</summary>
        /// <value>An array of types indicating method signature. Null to ignore method parameter types.</value>
        /// <remarks>
        /// When not null <see cref="IsSupported"/> looks only for methods that have the same number and types of arguments.
        /// <note>Number any types of arguments are theoretically not unique identification of method by signature, so <see cref="System::Reflection::AmbiguousMatchException"/> can still be thrown.</note>
        /// </remarks>
        property cli::array<System::Type^>^ MethodParameters;
        /// <summary>Gets or sets base type which is used for method lookup</summary>
        /// <remarks>If <see cref="Hint"/> is not null method to provide attributes is firts sought at this type and then method which overrides/implements this method is located on target type.</remarks>
        property System::Type^ Hint;
        
        /// <summary>Gets value indicating if this instance of <see cref="MethodNotSupportedAttribute"/> indicates supported or unsupported method</summary>
        /// <param name="method">Pass actual method this instance was applied on here.</param>
        /// <exception cref="ArgumentNulllException"><paramref name="method"/> is null</exception>
        /// <returns>True if this instance indicates supported method, false if this instance indicates non supported method.</returns>
        /// <exception cref="System::Reflection::AmbiguousMatchException">Either <see cref="MethodParameters"/> is null and more methods with name <see cref="Method"/> exist on target type or method identification using parameter types is not unique.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="method"/> is global method and <see cref="Type"/> is null. -or-
        /// One of items in <see cref="MethodParameters"/> is null. -or-
        /// <see cref="Hint"/> is not null and target type neither inherits from nor implements it.
        /// </exception>
        /// <exception cref="MissingMethodException">Method not found on tagre type or <see cref="Hint"/> -or- Method was found on <see cref="Hint"/> but method which overrides or implements in was not found on target type (it might be found of one of target type's base types, but such methods are excluded from lookup).</exception>
        virtual bool IsSupported(Reflection::MethodInfo^ method) override;
        /// <summary>Gets method this attribute redirects to</summary>
        /// <param name="attributeMethod">Pass actual method this instance was applied on here.</param>
        /// <exception cref="ArgumentNulllException"><paramref name="method"/> is null</exception>
        /// <returns>A method this attribute redirects to</returns>
        /// <exception cref="System::Reflection::AmbiguousMatchException">Either <see cref="MethodParameters"/> is null and more methods with name <see cref="Method"/> exist on target type or method identification using parameter types is not unique.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="method"/> is global method and <see cref="Type"/> is null. -or-
        /// One of items in <see cref="MethodParameters"/> is null. -or-
        /// <see cref="Hint"/> is not null and target type neither inherits from nor implements it.
        /// </exception>
        /// <exception cref="MissingMethodException">Method not found on tagre type or <see cref="Hint"/> -or- Method was found on <see cref="Hint"/> but method which overrides or implements in was not found on target type (it might be found of one of target type's base types, but such methods are excluded from lookup).</exception>
        System::Reflection::MethodInfo^ GetTargetMethod(Reflection::MethodInfo^ attributeMethod);
    };

    ref class PluginIconBaseAttribute;
    ref class FilePluginIconAttribute;
    ref class ResourcePluginIconAttribute;

    /// <summary>Apply this attribute to class implementing Total Commander plugin to precise how the plugin is generated.</summary>
    /// <remarks>To set plugin icon use one of <see cref="PluginIconBaseAttribute"/>-derived classes such as <see cref="FilePluginIconAttribute"/> or <see cref="ResourcePluginIconAttribute"/>.</remarks>
    /// <version version="1.5.4">The class is now <see langword="selaed"/></version>
    [AttributeUsageAttribute(AttributeTargets::Class, Inherited=false)]
    public ref class TotalCommanderPluginAttribute sealed: Attribute{
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
    /// <version version="1.5.4">The class is now <see langword="selaed"/></version>
    [AttributeUsage(AttributeTargets::Class, Inherited=false)]
    public ref class NotAPluginAttribute sealed: Attribute{
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
    