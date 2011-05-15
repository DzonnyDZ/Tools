#pragma once

#include "ListerPluginBase.h"
#include "WLX common.h"

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections::Generic;

namespace Tools{namespace TotalCommanderT{

    /// <summary>Abstract base class for Total Commander lister plugins (wlx)</summary>
    /// <remarks>See <see2 cref2="T:Tools.TotalCommanderT.PluginBuilder.Generator"/> for more information about how to generate Total Commander plugin from .NET.
    /// <para>To keep fingerprint and dependenciew of Tools.TotalCommander.dll assembly low, this class does not provide some utility functions. You may consider deriving from derived class from Tools.TotalCommander.Extensions.dll instead.</para></remarks>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    generic <class T> where T : IListerUI
    public ref class ListerPlugin abstract : ListerPluginBase {
    protected:
        /// <summary>CTor - creates a new instance of the <see cref="ListerPluginBase"/> class</summary>
        /// <remarks>The plugin instance is not ready to be used untill <see cref="ListerPluginBase::OnInit"/> or <see cref="ListerPluginBase::SetDefaultParams"/> is called.</remarks>
        ListerPlugin():ListerPluginBase(){}

        //TODO:
    };
}}
