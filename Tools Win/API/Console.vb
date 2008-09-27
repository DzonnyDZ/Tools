Imports System.Runtime.InteropServices
Namespace API
    ''' <summary>Console-related API declarations</summary>
    Friend Module Console
        ''' <summary>Allocates a new console for the calling process.</summary>
        ''' <returns>If the function succeeds, the return value is nonzero.</returns>
        ''' <remarks>A process can be associated with only one console, so the AllocConsole function fails if the calling process already has a console. A process can use the FreeConsole function to detach itself from its current console, then it can call AllocConsole to create a new console or AttachConsole to attach to another console.</remarks>
        Public Declare Function AllocConsole Lib "kernel32.dll" () As Boolean
        ''' <summary>Detaches the calling process from its console.</summary>
        ''' <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        Public Declare Function FreeConsole Lib "kernel32.dll" () As Boolean
        ''' <summary>Sets console icon</summary>
        ''' <param name="hicon">Handle of icon (<see cref="Icon.Handle"/>)</param>
        ''' <returns>True of succes, false on error</returns>
        ''' <remarks>This function is undocumented</remarks>
        Public Declare Function SetConsoleIcon Lib "Kernel32.dll" (ByVal hicon As IntPtr) As Boolean
    End Module
End Namespace
