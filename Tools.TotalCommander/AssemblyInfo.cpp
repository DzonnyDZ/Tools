//#include "stdafx.h"

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
[assembly:AssemblyTitleAttribute("Tools.TotalCommander")];
[assembly:AssemblyDescriptionAttribute("Classes for interaction with Total Commander from managed code")];
[assembly:AssemblyConfigurationAttribute("")];
[assembly:AssemblyCompanyAttribute("ÐTooly team")];
[assembly:AssemblyProductAttribute("ÐTools")];
[assembly:AssemblyCopyrightAttribute("© Ðonny 2009")];
[assembly:AssemblyTrademarkAttribute("")];
[assembly:Resources::NeutralResourcesLanguageAttribute("en")]

#include "..\SharedFiles\Version.cpp"

[assembly:ComVisible(false)];

[assembly:CLSCompliantAttribute(true)];

[assembly:SecurityPermission(SecurityAction::RequestMinimum, UnmanagedCode = true)];

[assembly:InternalsVisibleToAttribute("TCPluginBuilder, PublicKey=0024000004800000940000000602000000240000525341310004000001000100236C3D276B0D8D7F5F0EF5F0D4C09BA1A30A06F7132007CE5AD4E3B80DD8ED7152A268DE45D4AD2830229AE3DEB46C6F9F900678AE0D76BE7C5B8F10F444D820DBEDEE6D774EC8138E678035789B3600CDE5E9079849EA5916A4ABDF2CBA44FE7A41A225851369F7E656B44FA9C03D08D8A5E635D3CD4126CF88D33D58426BA3")];
