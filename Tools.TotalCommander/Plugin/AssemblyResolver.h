#pragma once

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;

namespace Tools{namespace TotalCommanderT{
    /// <summary>Resolves reference to current assembly</summary>
    private ref class PluginSelfAssemblyResolver{
    private:    
        /// <summary>Resolves reference to curent assembly. Handles the <see cref="AppDomain::AssemblyResolve"/> event.</summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="args">Event arguments</param>
        /// <returns>When <paramref name="args"/> requests executing assembly returns executing assembly; otherwise null</returns>
        /// <remarks>This class is necessary in because plugins are typically not plced in teh same directory as TOTALCMD.EXE, where .NET looky by plugin dependencies by default.<remarks>
        static Assembly^ OnResolveAssembly(Object^ sender, ResolveEventArgs^ args);
    internal:
        /// <summary>Setups the assembly loader</summary>
        /// <remarks>This method should be called only once per session</remarks>
        static void Setup();
    /*private:
        /// <summary>Attempts to load an assembly from file</summary>
        /// <param name="path">Path of file to load asembly from</param>
        /// <param name="name">Desired assembly name of loaded assembly</param>
        /// <param name="assembly">When function returns true, this parameter is assigned loaded assembly; otherwise null</param>
        /// <returns>True if assembly with name <paramref name="name"/> was successfuly loaded from path <paramref name="path"/> and stored in <paramref name="assembly"/>; false otherwise</returns>
        static bool TryLoadAssembly(String^ path, AssemblyName^ name, [Out] Assembly^% assembly);*/
    };
}}