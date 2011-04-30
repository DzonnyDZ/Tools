#pragma once

using namespace ::System::Text::RegularExpressions;

namespace Tools{namespace TotalCommanderT{
    using namespace System;

    /// <summary>When applied onto method, identifies the method as part of plugin contract</summary>
    /// <remarks>All plugin methods (from Total Commander point of view - not the methods derived class implements) must me marked with this attribute. Do not use this attribute of multiple methods serving same purpose (i.e. when method is overloaded to be calable from outside of assembly).</remarks>
    [AttributeUsageAttribute(AttributeTargets::Method, Inherited=false)]
    private ref class PluginMethodAttribute : Attribute{ 
    private:
        /// <summary>Contains value of the <see cref="DefinedBy"/> property</summary>
        initonly String^ definedBy;
        /// <summary>Contains value of the <see cref="ImplementedBy"/> property</summary>
        initonly String^ implementedBy;
        /// <summary>A reguler expression to detect if macro name is OK</summary>
        static initonly Regex^ macroRegex = gcnew Regex("^[A-Za-z_][A-Za-z_0-9]*$", RegexOptions::Compiled | RegexOptions::CultureInvariant);
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
}}