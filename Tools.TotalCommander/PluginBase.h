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

    protected private:
        /// <summary>Computes flags indicating which functions are supported by current plugin</summary>
        /// <returns>Flags indicating which functions are supported by current plugin</returns>
        /// <version version="1.5.4">This function is new in version 1.5.4</version>
        generic <class T> where T:Enum, gcnew()
        T GetSupportedFunctions();
    
    public:
        /// <summary>When override in derived class gest, when plugin is initialized, value indicating if it lives in Unicode or ANSI environment</summary>
        /// <returns>True if plugin is used from version of Total Commander which supports Unicode (7.5 or newer, version 2.0 or newer of unmanaged plugin interface); false if not. Also returns true if <see cref="IsInTotalCommander"/> is false (plugin is used from nanaged code). Also returns true before the plugin is initialized.</returns>
        /// <remarks>
        /// Derived class detects Unicode usage depending on which function was used to initialize the plugin.
        /// This way it is possible to initialize plugin in ANSI mode even from managed code (when initialization function intended for unamnaged ANSI code is called).
        /// </remarks>
        /// <version version="1.5.4">This property is new in version 1.5.4</version>
        virtual property bool Unicode{bool get() abstract;}
        /// <summary>When overriden in derived class gets, when plugin is initialized, value indicating if it was initialized by Total Commander or .NET application</summary>
        /// <returns>True if plugin is used from Total Commander, false if it is used from managed code. Also feruens false before the plugin instance is initialized.</returns>
        /// <remarks>
        /// Derived class detects whether plugin is in Total Commander or on depending on which function was used to initiallize the plugin.
        /// Therefore if the plugin is initialized unmanaged way from managed code this property will return true.
        /// This property also returns true if plugin is used form any unmanaged application capable of using Total Commander plugins.
        /// </remarks>
        /// <version version="1.5.4">This property is new in version 1.5.4</version>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        virtual property bool IsInTotalCommander{bool get() abstract;}
        /// <summary>When overriden in derived class gets value indicating if the plugin was already initialized</summary>
        /// <remarks>
        /// Because Total Commander iztself does not use managed objects (or any objects in terms of classes as a matter of fact) in it's plugin interface, Total Commander plugins are not initialized using constructor but rather using special instance methods.
        /// After this plugin-type-specific method is called by Total Commander this property is set to true.
        /// <version version="1.5.4">This property is new in version 1.5.4</version>
        virtual property bool Initialized{bool get() abstract;}
    protected:
        /// <summary>When overriden in derived class provides custom code invoked when plugin is initialized.</summary>
        /// <remarks>When this method is called the <see cref="Initialized"/> property has value true.
        /// <para>Default implementation of this method does nothing.</para></remarks>
        /// <version version="1.5.4">This method is new in version 1.5.4 (it was previously implemented at <see cref="FileSystemPlugin"/> level).</version>
        virtual void OnInit();
    };
}}