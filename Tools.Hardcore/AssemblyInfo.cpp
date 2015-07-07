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
[assembly:AssemblyDescriptionAttribute("Common and useful functionality that can be implemented neither in C# nor in VB due to language limitations.")];
[assembly:AssemblyConfigurationAttribute("")];
[assembly:AssemblyCompanyAttribute("ÐTools team")];
[assembly:AssemblyProductAttribute("ÐTools")];
[assembly:AssemblyCopyrightAttribute("© Ðonny 2010-2015")];
[assembly:Resources::NeutralResourcesLanguageAttribute("en")]

#include "..\SharedFiles\Version.cpp"

[assembly:ComVisible(false)];

[assembly:CLSCompliantAttribute(true)];

[assembly:SecurityPermission(SecurityAction::RequestMinimum, UnmanagedCode = false)];

[assembly:Extension()];
