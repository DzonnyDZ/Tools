#pragma once
#include "Common.h"
#include "DefaultParams.h"

namespace Tools {
    namespace TotalCommanderT {
        using namespace System;
        using namespace System::ComponentModel;

        /// <summary>Common base class for all Total Commander plug-ins</summary>
        /// <remarks><note type="inheritinfo">Do not derive directly from this class as it does not represent any concrete plug-in</note>
        /// <para>See <see2 cref2="T:Tools.TotalCommanderT.PluginBuilder.Generator"/> for more information about how to generate Total Commander plug-in from .NET.</para></remarks>
        public ref class PluginBase abstract {
        internal:
            /// <summary>CTor - creates new instance of the <see cref="PluginBase"/> class</summary>
            PluginBase();
        public:
            /// <summary>When overridden in derived class gets name of plug-in</summary>
            /// <remarks>If this returns null or an empty string Total Commander uses file name. Do not include nullchars in name, Total Commander will utilize only pert of name before first nullchar.</remarks>
            virtual property String^ Name {String^ get() abstract; }
            /// <summary>When overridden in derived class gets basic type of the plug-in</summary>
            /// <version version="1.5.4">This property is new in version 1.5.4</version>
            virtual property Tools::TotalCommanderT::PluginType PluginType {Tools::TotalCommanderT::PluginType get() abstract; }
            /// <summary>When overridden in derived class gets a base class of plug-in type</summary>
            /// <version version="1.5.4">This property is new version 1.5.4</version>
            virtual property Type^ PluginBaseClass {Type^ get() abstract; }

        protected private:
            /// <summary>Computes flags indicating which functions are supported by current plug-in</summary>
            /// <returns>Flags indicating which functions are supported by current plug-in</returns>
            /// <version version="1.5.4">This function is new in version 1.5.4</version>
            generic <class T> where T : Enum, gcnew()
                T GetSupportedFunctions();

        public:
            /// <summary>When override in derived class gets, when plug-in is initialized, value indicating if it lives in Unicode or ANSI environment</summary>
            /// <returns>True if plug-in is used from version of Total Commander which supports Unicode (7.5 or newer, version 2.0 or newer of unmanaged plug-in interface); false if not. Also returns true if <see cref="IsInTotalCommander"/> is false (plug-in is used from nanaged code). Also returns true before the plug-in is initialized.</returns>
            /// <remarks>
            /// Derived class detects Unicode usage depending on which function was used to initialize the plug-in.
            /// This way it is possible to initialize plug-in in ANSI mode even from managed code (when initialization function intended for unmanaged ANSI code is called).
            /// </remarks>
            /// <version version="1.5.4">This property is new in version 1.5.4</version>
            virtual property bool Unicode {bool get() abstract; }
            /// <summary>When overridden in derived class gets, when plug-in is initialized, value indicating if it was initialized by Total Commander or .NET application</summary>
            /// <returns>True if plug-in is used from Total Commander, false if it is used from managed code. Also returns false before the plug-in instance is initialized.</returns>
            /// <remarks>
            /// Derived class detects whether plug-in is in Total Commander or on depending on which function was used to initialize the plug-in.
            /// Therefore if the plug-in is initialized unmanaged way from managed code this property will return true.
            /// This property also returns true if plug-in is used form any unmanaged application capable of using Total Commander plug-ins.
            /// </remarks>
            /// <version version="1.5.4">This property is new in version 1.5.4</version>
            [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
            virtual property bool IsInTotalCommander {bool get() abstract; }
            /// <summary>When overridden in derived class gets value indicating if the plug-in was already initialized</summary>
            /// <remarks>
            /// Because Total Commander itself does not use managed objects (or any objects in terms of classes as a matter of fact) in it's plug-in interface, Total Commander plug-ins are not initialized using constructor but rather using special instance methods.
            /// After this plug-in-type-specific method is called by Total Commander this property is set to true.
            /// </remarks>
            /// <version version="1.5.4">This property is new in version 1.5.4</version>
            virtual property bool Initialized {bool get() abstract; }
        protected:
            /// <summary>When overridden in derived class provides custom code invoked when plug-in is initialized.</summary>
            /// <remarks>When this method is called the <see cref="Initialized"/> property has value true.
            /// <para>Default implementation of this method does nothing.</para></remarks>
            /// <version version="1.5.4">This method is new in version 1.5.4 (it was previously implemented at <see cref="FileSystemPlugin"/> level).</version>
            virtual void OnInit();
        private:
            Nullable<DefaultParams> pluginParams;
        public:
            /// <summary>Gets default parameters of the plug-in</summary>
            /// <returns>Default parameters of the plug-in, null if the property has not been initialized yet.</returns>
            /// <remarks>
            /// Value of this property is only valid after the <see cref="SetDefaultParams"/> method was called.
            /// This happens in different (but always early) phases of plug-in instance live cycle for different plug-in types.
            /// Some older versions of Total Commander (different for different plug-in types) do initialize this method at all.
            /// </remarks>
            /// <sealso cref="SetDefaultParams"/>
            /// <version version="1.5.4">This property is new in version 1.5.4. It was previously declared at <see cref="FileSystemPlugin"/> level and <see langword="protected"/>.</version>
            property Nullable<DefaultParams> PluginParams {Nullable<DefaultParams> get(); }
        public:
            /// <summary>Called to initialize the <see cref="DefaultParams"/> property</summary>
            /// <param name="dps">
            /// This structure currently contains the version number of the plug-in interface, and the suggested location for the settings file (INI.
            /// </param>
            /// <remarks>
            /// Make sure to use a unique header when storing data in this file, because it is shared by other plug-ins!
            /// If your plug-in needs more than 1kbyte of data, you should use your own INI file because INI files are limited to 64k.
            /// <note type="inheritinfo">Do not throw any exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
            /// <note type="inheritinfo">Always call base class method. When base class method is not called, the <see cref="PluginParams"/> property does not have valid value.</note>
            /// For different plug-in types this method is called at different stages of their live-cycle.
            /// Some older versions of Total Commander (different for different plug-in types) do not call this method at all.
            /// <para>This is managed-signature implementation of Total Commander plug-in interface function. Native-signature functions are implemented in individual plug-in types because their signatures are different. They all call this method.</para>
            /// </remarks>
            /// <exception cref="InvalidOperationException">This method is called when it was already called. This method can be called only once on each instance.</exception>
            /// <version version="1.5.4">This method is new in version 1.5.4. It was previously declared at <see cref="FileSystemPlugin"/> level.</version>
            virtual void SetDefaultParams(DefaultParams dps);
        };
    }
}