#include "DefaultParams.h"
#include "Common.h"

using namespace System;

namespace Tools {
    namespace TotalCommanderT {

        DefaultParams::DefaultParams(FsDefaultParamStruct& from) {
            this->defaultIniName = gcnew String(from.DefaultIniName);
            this->version = gcnew System::Version(from.PluginInterfaceVersionHi, from.PluginInterfaceVersionLow, 0, 0);
        }

        DefaultParams::DefaultParams(ListDefaultParamStruct& from) {
            this->defaultIniName = gcnew String(from.DefaultIniName);
            this->version = gcnew System::Version(from.PluginInterfaceVersionHi, from.PluginInterfaceVersionLow, 0, 0);
        }

        inline System::Version^ DefaultParams::Version::get() { return this->version; }

        inline String^ DefaultParams::DefaultIniName::get() { return this->defaultIniName; }
     }
}