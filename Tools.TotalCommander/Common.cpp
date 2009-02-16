#include "Stdafx.h"
#include "Common.h"

using namespace System;
namespace Tools{namespace TotalCommanderT{
    inline MethodNotSupportedAttribute::MethodNotSupportedAttribute(){/*Do nothing*/}

    inline TotalCommanderPluginAttribute::TotalCommanderPluginAttribute(String^ Name){this->name = Name;}
    inline String^ TotalCommanderPluginAttribute::Name::get(){return this->name;}
}}