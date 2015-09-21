#include "stdafx.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;
using namespace System::Runtime::InteropServices;
using namespace System::Security::Permissions;

#include "..\SharedFiles\GlobalAssemblyInfo.cpp"

[assembly:AssemblyTitleAttribute("Tools.Hardcore")];
[assembly:AssemblyDescriptionAttribute("Common and useful functionality that can be implemented neither in C# nor in VB due to language limitations.")];

[assembly:Resources::NeutralResourcesLanguageAttribute("en")]

[assembly:ComVisible(false)];

[assembly:CLSCompliantAttribute(true)];

[assembly:SecurityPermission(SecurityAction::RequestMinimum, UnmanagedCode = false)];

[assembly:Extension()];
