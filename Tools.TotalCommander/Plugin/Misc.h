#include <vcclr.h>
#pragma once
using namespace System;
using namespace Tools::TotalCommanderT;

namespace Tools{namespace TotalCommanderT{
    /// <summary>Holds plugin Application domain. Instantiated in plugin application domain.</summary>
    ref struct AppDomainHolder;
    /// <summary>Indicates if plugin initialization is needed</summary>
    bool RequireInitialize = true;
    /// <summary>Holds instance of the <see cref="AppDomainHolder"/> class</summary>
    gcroot<AppDomainHolder^> holder;
}}