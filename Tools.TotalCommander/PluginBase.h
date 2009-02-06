#pragma once
#include "Common.h"

namespace Tools{namespace TotalCommanderT{
    using namespace System;
    using namespace System::ComponentModel;

    /// <summary>Common base class for all Total Commander plugins</summary>
    /// <remarks><note type="inheritinfo">Do not derive directly from this class as it does not represent any concrete plugin</note></remarks>
    public ref class PluginBase abstract{
    internal:
        /// <summary>CTor - creates new instance of the <see cref="PluginBase"/> class</summary>
        PluginBase();
    public:
        /// <summary>Gets name of plugin</summary>
        virtual property String^ Name{String^ get() abstract;}
    };
}}