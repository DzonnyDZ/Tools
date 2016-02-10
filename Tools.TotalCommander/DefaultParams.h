#pragma once

#include "Plugin\fsplugin.h"
#include "Plugin\listplug.h"

using namespace System;

namespace Tools{namespace TotalCommanderT{
    /// <summary>Contains information about current plugin interface version and INI location</summary>
    /// <seelaso cref="FileSystemPlugin::FsSetDefaultParams"/>
    public value class DefaultParams{
    private:
        System::Version^ version;
        String^ defaultIniName;
    internal:
        /// <summary>CTor - populates new instance with data from <see cref="FsDefaultParamStruct"/></summary>
        /// <param name="from">The <see cref="FsDefaultParamStruct"/></param>
        DefaultParams(FsDefaultParamStruct& from);
        /// <summary>CTor - populates new instance with data from <see cref="ListDefaultParamStruct"/></summary>
        /// <param name="from">The <see cref="ListDefaultParamStruct"/></param>
        /// <version version="1.5.4">This CTor is new in version 1.5.4</version>
        DefaultParams(ListDefaultParamStruct& from);
    public:
        /// <summary>Gets the plugin interface version</summary>
        /// <returns>Version of plugin intercase consisting of Major.Minor.0.0</returns>
        property System::Version^ Version{System::Version^ get();}
        /// <summary>Suggested location+name of the INI file where the plugin could store its data.</summary>
        /// <returns>A fully qualified path+file name, in the same directory as the wincmd.ini. It's recommended to store the plugin data in this file or at least in this directory, because the plugin directory or the Windows directory may not be writable!</returns>
        /// <remarks>Since <see cref="FileSystemPlugin::FsSetDefaultParams"/> is ANSI-only function this string always contains only ANSI characters (i.e. those defined in <see cref="System::Text::Encoding::Default"/>)</remarks>
        property String^ DefaultIniName{String^ get();}
    };
}}