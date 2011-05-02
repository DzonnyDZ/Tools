#pragma once

#include "PluginMethodAttribute.h"

using namespace System;
using namespace System::ComponentModel;

namespace Tools{namespace TotalCommanderT{

    /// <summary>This enumerations enumerates all Total Commander plugin functions from TC plugin interface point-of-view common to content-supporing plugins</summary>
    /// <seelaso cref="ContentPluginBase"/>
    /// <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    [Flags]
    public enum class ContentFunctions{
        /// <summary><see cref="ContentPluginBase::SupportedFields"/> (getter)</summary>
        [PluginMethod("get_SupportedFields","TC_C_GETSUPPORTEDFIELD")] GetSupportedField = 1<<22,
        /// <summary><see cref="ContentPluginBase::GetValue"/></summary>
        [PluginMethod("GetValue","TC_C_GETVALUE")] GetValue = 1<<23,
        /// <summary><see cref="ContentPluginBase::StopGetValue"/></summary>
        [PluginMethod("StopGetValue","TC_C_STOPGETVALUE")] StopGetValue = 1<<24,
        /// <summary><see cref="ContentPluginBase::GetDefaultSortOrder"/></summary>
        [PluginMethod("GetDefaultSortOrder","TC_C_GETDEFAULTSORTORDER")] GetDefaultSortOrder = 1<<25,
        /// <summary><see cref="ContentPluginBase::OnContentPluginUnloading"/></summary>
        [PluginMethod("OnContentPluginUnloading","TC_C_PLUGINUNLOADING")] PluginUnloading = 1<<26,
        /// <summary><see cref="ContentPluginBase::GetSupportedFieldFlags"/></summary>
        [PluginMethod("GetSupportedFieldFlags","TC_C_GETSUPPORTEDFIELDFLAGS")] GetSupportedFieldFlags = 1<<27,
        /// <summary><see cref="ContentPluginBase::SetValue"/></summary>
        [PluginMethod("SetValue","TC_C_SETVALUE")] SetValue = 1<<28,
        /// <summary>Reserved for future used when <c>ContentGetDefaultView<c/> would be implemented</summary>
        /// <remarks>Name of the enum member will change in future</remarks>
        _GetDefaultView = 1<<29
    };

}}