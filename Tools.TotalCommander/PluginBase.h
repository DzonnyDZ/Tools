#pragma once
#include "Common.h"

namespace Tools{namespace TotalCommanderT{
    using namespace System;
    using namespace System::ComponentModel;

    /// <summary>Common base class for all Total Commander plugins</summary>
    /// <remarks><note type="inheritinfo">Do not derive directly from this class as it does not represent any concrete plugin</note>
    /// <para>See <see2 cref2="T:Tools.TotalCommanderT.PluginBuilder.Generator"/> for more information about how to generate Total Commander plugin from .NET.</para></remarks>
    public ref class PluginBase abstract{
    internal:
        /// <summary>CTor - creates new instance of the <see cref="PluginBase"/> class</summary>
        PluginBase();
    public:
        /// <summary>When overriden in derived class gets name of plugin</summary>
        /// <remarks>If this returns null or an empty string Total Commander uses file name. Do not include nullchars in name, Total Commander will utilize only pert of name before first nullchar.</remarks>
        virtual property String^ Name{String^ get() abstract;}
        /// <summary>When overriden in derived class gets basic type of the plugin</summary>
        /// <version version="1.5.4">This property is new in version 1.5.4</version>
        virtual property Tools::TotalCommanderT::PluginType PluginType{Tools::TotalCommanderT::PluginType get() abstract;}
        /// <summary>When overriden in derived class gets a base class of plugin type</summary>
        /// <version version="1.5.4">This property is new version 1.5.4</version>
        virtual property Type^ PluginBaseClass{Type^ get() abstract;}

    protected:
        /// <summary>Computes flags indicating which functions are supported by current plugin</summary>
        /// <returns>Flags indicating which functions are supported by current plugin</returns>
        /// <version version="1.5.4">This function is new in version 1.5.4</version>
        generic <class T> where T:Enum, gcnew()
        T GetSupportedFunctions();
    };
}}