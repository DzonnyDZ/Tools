#include "stdafx.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;
using namespace System::Runtime::InteropServices;
using namespace System::Security::Permissions;

//
// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly:AssemblyTitleAttribute("Tools.Hardcore")];
[assembly:AssemblyDescriptionAttribute("Common and useful functionaly that must be writting in C++/CLI becaue no other high-level language supports it.")];
[assembly:AssemblyConfigurationAttribute("")];
[assembly:AssemblyCompanyAttribute("ÐTools teas")];
[assembly:AssemblyProductAttribute("ÐTools")];
[assembly:AssemblyCopyrightAttribute("© Ðonny 2010")];
[assembly:AssemblyTrademarkAttribute("")];
[assembly:Resources::NeutralResourcesLanguageAttribute("en")]

#include "..\SharedFiles\Version.cpp"

[assembly:ComVisible(false)];

[assembly:CLSCompliantAttribute(true)];

[assembly:SecurityPermission(SecurityAction::RequestMinimum, UnmanagedCode = false)];
