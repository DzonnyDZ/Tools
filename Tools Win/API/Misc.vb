Imports System.Runtime.InteropServices, Tools.ExtensionsT

Namespace API
    ''' <summary>Generic exception caused by Win32 API</summary>
    Public Class Win32APIException : Inherits System.ComponentModel.Win32Exception
        ''' <summary>CTor with error number</summary>
        ''' <param name="Number">Error number</param>
        Public Sub New(ByVal Number As Integer)
            MyBase.new(Number)
        End Sub
        ''' <summary>CTor - error number will be obtained automatically via <see cref="GetLastError"/></summary>
        Public Sub New()
            MyBase.new()
        End Sub
        ''' <summary>Gets exception for error caused by last Win32 API call</summary>
        ''' <returns>Exception obtained via <see cref="Marshal.GetExceptionForHR"/>(<see cref="Marshal.GetHRForLastWin32Error"/>)</returns>
        Public Shared Function GetLastWin32Exception() As Exception
            Dim Code = Marshal.GetHRForLastWin32Error
            Return Marshal.GetExceptionForHR(Code)
        End Function
        ''' <summary>Gets exception for error caused by last Win32 API call</summary>
        ''' <typeparam name="T">Type of exception to be returned</typeparam>
        ''' <returns>Obtains exception using non-generic <see cref="M:Tools.API.Win32APIException.GetLastWin32Exception"/> function.
        ''' It the exception returned is <typeparamref name="T"/> or derives from <typeparamref name="T"/> returns it; otherwise returns new instance of <see cref="Win32APIException"/>.</returns>
        Public Shared Function GetLastWin32Exception(Of T As Exception)() As Exception
            Dim Ex = GetLastWin32Exception()
            If Not TypeOf Ex Is T Then Return New Win32APIException Else Return Ex
        End Function
    End Class


    ''' <summary>Common Win32 API declarations</summary>
    Friend Module Common
        ''' <summary>Sets the last-error code for the calling thread.</summary>
        ''' <param name="dwErrCode">The last-error code for the thread.</param>
        Public Declare Sub SetLastError Lib "kernel32" (dwErrCode%)
        ''' <summary>Error code that indicates no error</summary>
        Public Const ERROR_SUCCESS% = 0

        ''' <summary>Value representing NULL</summary>
        Public Const NULL As Integer = 0
        ''' <summary>Boolean type as used in Win32 API</summary>
        Public Enum APIBool As Integer
            ''' <summary>True</summary>
            [TRUE] = 1
            ''' <summary>False</summary>
            [FALSE] = 0
        End Enum

        ''' <summary>Retrieves the calling thread's last-error code value. The last-error code is maintained on a per-thread basis. Multiple threads do not overwrite each other's last-error code.</summary>
        ''' <returns>The return value is the calling thread's last-error code.
        ''' <para>The Return Value section of the documentation for each function that sets the last-error code notes the conditions under which the function sets the last-error code. Most functions that set the thread's last-error code set it when they fail. However, some functions also set the last-error code when they succeed. If the function is not documented to set the last-error code, the value returned by this function is simply the most recent last-error code to have been set; some functions set the last-error code to 0 on success and others do not.</para>
        ''' <para>Windows Me/98/95:  Functions that are actually implemented in 16-bit code do not set the last-error code. You should ignore the last-error code when you call these functions. They include window management functions, GDI functions, and Multimedia functions. For functions that do set the last-error code, you should not rely on GetLastError returning the same value as it does under other versions of Windows.</para></returns>
        ''' <remarks>Visual Basic:  Applications should call <see cref="ErrObject.LastDllError"/> of <see cref="err"/> instead of <see cref="GetLastError"/>.
        ''' <para>Use <see cref="Marshal.GetLastWin32Error"/> instead.</para></remarks>
        <Obsolete("Use Marshal.GetLastWin32Error  instead")> _
        Public Declare Function GetLastError Lib "kernel32" () As Integer
        ''' <summary>Formats a message string. The function requires a message definition as input. The message definition can come from a buffer passed into the function. It can come from a message table resource in an already-loaded module. Or the caller can ask the function to search the system's message table resource(s) for the message definition. The function finds the message definition in a message table resource based on a message identifier and a language identifier. The function copies the formatted message text to an output buffer, processing any embedded insert sequences if requested.</summary>
        ''' <param name="dwFlags">The formatting options, and how to interpret the lpSource parameter. The low-order byte of dwFlags specifies how the function handles line breaks in the output buffer. The low-order byte can also specify the maximum width of a formatted output line. This parameter can be one or more of enumerated values. If the low-order byte is a nonzero value other than FORMAT_MESSAGE_MAX_WIDTH_MASK, it specifies the maximum number of characters in an output line. The function ignores regular line breaks in the message definition text. The function never splits a string delimited by white space across a line break. The function stores hard-coded line breaks in the message definition text into the output buffer. Hard-coded line breaks are coded with the %n escape sequence.</param>
        ''' <param name="lpSource">The location of the message definition. The type of this parameter depends upon the settings in the dwFlags parameter. 
        ''' <para><see cref="FormatMessageFlags.FORMAT_MESSAGE_FROM_HMODULE"/>: A handle to the module that contains the message table to search.</para>
        ''' <para><see cref="FormatMessageFlags.FORMAT_MESSAGE_FROM_STRING"/>: Pointer to a string that consists of unformatted message text. It will be scanned for inserts and formatted accordingly.</para></param>
        ''' <param name="dwMessageId">The message identifier for the requested message. This parameter is ignored if dwFlags includes FORMAT_MESSAGE_FROM_STRING. </param>
        ''' <param name="dwLanguageId">The language identifier for the requested message. This parameter is ignored if dwFlags includes FORMAT_MESSAGE_FROM_STRING. 
        ''' If you pass a specific LANGID in this parameter, FormatMessage will return a message for that LANGID only. If the function cannot find a message for that LANGID, it returns ERROR_RESOURCE_LANG_NOT_FOUND. If you pass in zero, FormatMessage looks for a message for LANGIDs in the following order:
        ''' <list><item>Language neutral</item>
        ''' <item>Thread LANGID, based on the thread's locale value</item> 
        ''' <item>User default LANGID, based on the user's default locale value</item> 
        ''' <item>System default LANGID, based on the system default locale value</item> 
        ''' <item>US English</item> 
        ''' </list>
        ''' If FormatMessage does not locate a message for any of the preceding LANGIDs, it returns any language message string that is present. If that fails, it returns ERROR_RESOURCE_LANG_NOT_FOUND.
        ''' </param>
        ''' <param name="lpBuffer">[out] A pointer to a buffer that receives the null-terminated string that specifies the formatted message. If dwFlags includes <see cref="FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER"/>, the function allocates a buffer using the LocalAlloc function, and places the pointer to the buffer at the address specified in lpBuffer. This buffer cannot be larger than 64K bytes.</param>
        ''' <param name="nSize">If the <see cref="FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER"/> flag is not set, this parameter specifies the size of the output buffer, in TCHARs. If FORMAT_MESSAGE_ALLOCATE_BUFFER is set, this parameter specifies the minimum number of TCHARs to allocate for an output buffer. The output buffer cannot be larger than 64K bytes.</param>
        ''' <param name="Arguments">An array of values that are used as insert values in the formatted message. A %1 in the format string indicates the first value in the Arguments array; a %2 indicates the second argument; and so on.  The interpretation of each value depends on the formatting information associated with the insert in the message definition. The default is to treat each value as a pointer to a null-terminated string. By default, the Arguments parameter is of type va_list*, which is a language- and implementation-specific data type for describing a variable number of arguments. The state of the va_list argument is undefined upon return from the function. If the caller is to use the va_list again, it must destroy the variable argument list pointer using va_end and reinitialize it with va_start. If you do not have a pointer of type va_list*, then specify the FORMAT_MESSAGE_ARGUMENT_ARRAY flag and pass a pointer to an array of DWORD_PTR values; those values are input to the message formatted as the insert values. Each insert must have a corresponding element in the array.</param>
        ''' <returns>If the function succeeds, the return value is the number of TCHARs stored in the output buffer, excluding the terminating null character. If the function fails, the return value is zero. To get extended error information, call <see cref="GetLastError"/>.</returns>
        ''' <remarks>Within the message text, several escape sequences are supported for dynamically formatting the message. These escape sequences and their meanings are shown in the following table. All escape sequences start with the percent character (%).</remarks>
        Public Declare Function FormatMessage Lib "kernel32" Alias "FormatMessageA" (ByVal dwFlags As FormatMessageFlags, ByRef lpSource As Integer, ByVal dwMessageId As Integer, ByVal dwLanguageId As Languages, ByVal lpBuffer As String, ByVal nSize As Integer, ByRef Arguments As Integer) As Integer
        ''' <summary>Flags for the <see cref="FormatMessage"/> function</summary>
        ''' <remarks>If the low-order byte is a nonzero value other than <see cref="FormatMessageFlags.FORMAT_MESSAGE_MAX_WIDTH_MASK"/>, it specifies the maximum number of characters in an output line. The function ignores regular line breaks in the message definition text. The function never splits a string delimited by white space across a line break. The function stores hard-coded line breaks in the message definition text into the output buffer. Hard-coded line breaks are coded with the %n escape sequence.</remarks>
        Public Enum FormatMessageFlags As Integer
            ''' <summary>The lpBuffer parameter is a pointer to a PVOID pointer, and that the nSize parameter specifies the minimum number of TCHARs to allocate for an output message buffer. The function allocates a buffer large enough to hold the formatted message, and places a pointer to the allocated buffer at the address specified by lpBuffer. The caller should use the LocalFree function to free the buffer when it is no longer needed.</summary>
            FORMAT_MESSAGE_ALLOCATE_BUFFER = &H100I
            ''' <summary>Arguments parameter is not a va_list structure, but is a pointer to an array of values that represent the arguments. This flag cannot be used with 64-bit integer values. If you are using a 64-bit integer, you must use the va_list structure.</summary>
            FORMAT_MESSAGE_ARGUMENT_ARRAY = &H2000I
            ''' <summary>The lpSource parameter is a module handle containing the message-table resource(s) to search. If this lpSource handle is NULL, the current process's application image file will be searched. Cannot be used with FORMAT_MESSAGE_FROM_STRING. If the module has no message table resource, the function fails with ERROR_RESOURCE_TYPE_NOT_FOUND.</summary>
            FORMAT_MESSAGE_FROM_HMODULE = &H800I
            ''' <summary>The lpSource parameter is a pointer to a null-terminated message definition. The message definition may contain insert sequences, just as the message text in a message table resource may. Cannot be used with FORMAT_MESSAGE_FROM_HMODULE or FORMAT_MESSAGE_FROM_SYSTEM.</summary>
            FORMAT_MESSAGE_FROM_STRING = &H400I
            ''' <summary>The function should search the system message-table resource(s) for the requested message. If this flag is specified with FORMAT_MESSAGE_FROM_HMODULE, the function searches the system message table if the message is not found in the module specified by lpSource. Cannot be used with FORMAT_MESSAGE_FROM_STRING. If this flag is specified, an application can pass the result of the GetLastError function to retrieve the message text for a system-defined error.</summary>
            FORMAT_MESSAGE_FROM_SYSTEM = &H1000I
            ''' <summary>Insert sequences in the message definition are to be ignored and passed through to the output buffer unchanged. This flag is useful for fetching a message for later formatting. If this flag is set, the Arguments parameter is ignored.</summary>
            FORMAT_MESSAGE_IGNORE_INSERTS = &H200I
            ''' <summary>There are no output line width restrictions. The function stores line breaks that are in the message definition text into the output buffer.</summary>
            Zero = 0I
            ''' <summary>The function ignores regular line breaks in the message definition text. The function stores hard-coded line breaks in the message definition text into the output buffer. The function generates no new line breaks.</summary>
            FORMAT_MESSAGE_MAX_WIDTH_MASK = &HFFI
        End Enum
        ''' <summary>Gets inforemation about error in last API call</summary>
        ''' <returns>Description of error</returns>
        Public Function LastDllErrorInfo() As String
            Return LastDllErrorInfo(Marshal.GetLastWin32Error)
        End Function
        ''' <summary>Gets information about error with specified number</summary>
        ''' <param name="ErrN">Number of error</param>
        ''' <returns>Description of error</returns>
        Public Function LastDllErrorInfo(ByVal ErrN As Integer) As String
            Dim Buffer As String
            Buffer = Space(200)
            FormatMessage(FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM, 0, ErrN, Languages.LANG_NEUTRAL, Buffer, 200, 0)
            Return Buffer.Trim
        End Function

        ''' <summary>Langauage constants used by various API functions</summary>
        Public Enum Languages As Short
            ''' <summary>Neutral language</summary>
            LANG_NEUTRAL = &H0S
            ''' <summary>Default sublanguage</summary>
            SUBLANG_DEFAULT = &H1S
        End Enum

        ''' <summary>Retrieves a module handle for the specified module. The module must have been loaded by the calling process.</summary>
        ''' <param name="lpModuleName">The name of the loaded module (either a .dll or .exe file). If the file name extension is omitted, the default library extension .dll is appended. The file name string can include a trailing point character (.) to indicate that the module name has no extension. The string does not have to specify a path. When specifying a path, be sure to use backslashes (\), not forward slashes (/). The name is compared (case independently) to the names of modules currently mapped into the address space of the calling process.
        ''' <para>If this parameter is NULL, GetModuleHandle returns a handle to the file used to create the calling process (.exe file).</para>
        ''' <para>The GetModuleHandle function does not retrieve handles for modules that were loaded using the LOAD_LIBRARY_AS_DATAFILE flag. For more information, see LoadLibraryEx.</para></param>
        ''' <returns>If the function succeeds, the return value is a handle to the specified module.
        ''' <para>If the function fails, the return value is NULL.</para></returns>
        Public Declare Auto Function GetModuleHandle Lib "kernel32.dll" (ByVal lpModuleName$) As IntPtr

        ''' <summary>Contains some of error codes returned by Win32 API functions</summary>
        Public Enum Errors
            ''' <summary>Target buffer has not enough space for all items that are about to be placed in it</summary>
            ERROR_INSUFFICIENT_BUFFER = 122
            ''' <summary>Invalid handle. Returned when handle is expected to be returned but no hande was created.</summary>
            INVALID_HANDLE_VALUE = -1
            ''' <summary>File already exists</summary>
            ERROR_ALREADY_EXISTS = 183I
            ''' <summary>File not found</summary>
            ERROR_FILE_NOT_FOUND = 2I
            ''' <summary>File exists</summary>
            ERROR_FILE_EXISTS = 80I
            ''' <summary>The specified path was not found.</summary>
            ERROR_PATH_NOT_FOUND = 3I
            ''' <summary>The Dynamic Data Exchange (DDE) transaction failed.</summary>
            ERROR_DDE_FAIL = 1156I
            ''' <summary>There is no application associated with the given file name extension.</summary>
            ERROR_NO_ASSOCIATION = 1155I
            ''' <summary>Access to the specified file is denied.</summary>
            ERROR_ACCESS_DENIED = 5I
            ''' <summary>One of the library files necessary to run the application can't be found.</summary>
            ERROR_DLL_NOT_FOUND = 1157I
            ''' <summary>The function prompted the user for additional information, but the user canceled the request.</summary>
            ERROR_CANCELLED = 1223I
            ''' <summary>There is not enough memory to perform the specified action.</summary>
            ERROR_NOT_ENOUGH_MEMORY = 8
            ''' <summary>A sharing violation occurred.</summary>
            ERROR_SHARING_VIOLATION = 32I
            ''' <summary>Operation succeeded - no error</summary>
            NERR_Success = 0
            ''' <summary>More data available. Enumeration should continue.</summary>
            ERROR_MORE_DATA = 234
        End Enum
#Region "IsFunctionAvalibable"
        ''' <summary>Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.</summary>
        ''' <param name="lpLibFileName"><para>The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file). The name specified is the file name of the module and is not related to the name stored in the library module itself, as specified by the LIBRARY keyword in the module-definition (.def) file.</para>
        ''' <para>If the string specifies a full path, the function searches only that path for the module.</para>
        ''' <para>If the string specifies a relative path or a module name without a path, the function uses a standard search strategy to find the module; for more information, see the Remarks.</para>
        ''' <para>If the function cannot find the module, the function fails. When specifying a path, be sure to use backslashes (\), not forward slashes (/). For more information about paths, see Naming a File or Directory.</para>
        ''' <para>If the string specifies a module name without a path and the file name extension is omitted, the function appends the default library extension .dll to the module name. To prevent the function from appending .dll to the module name, include a trailing point character (.) in the module name string.</para></param>
        ''' <returns>If the function succeeds, the return value is a handle to the module. If the function fails, the return value is NULL.</returns>
        Public Declare Auto Function LoadLibrary Lib "kernel32" (ByVal lpLibFileName As String) As Integer


        ''' <summary>Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).</summary>
        ''' <param name="hModule"><para>A handle to the DLL module that contains the function or variable. The LoadLibrary, LoadLibraryEx, or GetModuleHandle function returns this handle.</para>
        ''' <para>The GetProcAddress function does not retrieve addresses from modules that were loaded using the LOAD_LIBRARY_AS_DATAFILE flag. For more information, see LoadLibraryEx.</para></param>
        ''' <param name="lpProcName">The function or variable name, or the function's ordinal value. If this parameter is an ordinal value, it must be in the low-order word; the high-order word must be zero.</param>
        ''' <returns>If the function succeeds, the return value is the address of the exported function or variable. If the function fails, the return value is NULL. </returns>
        Public Declare Function GetProcAddress Lib "kernel32" (ByVal hModule As Integer, <MarshalAs(UnmanagedType.LPStr)> ByVal lpProcName As String) As Integer
        ''' <summary>Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count. When the reference count reaches zero, the module is unloaded from the address space of the calling process and the handle is no longer valid.</summary>
        ''' <param name="hLibModule">A handle to the loaded library module. The LoadLibrary, LoadLibraryExGetModuleHandle, or GetModuleHandleEx function returns this handle.</param>
        ''' <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
        Public Declare Function FreeLibrary Lib "kernel32" (ByVal hLibModule As Integer) As Integer
#End Region

        ''' <summary>Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.</summary>
        ''' <param name="lpFileName">A string that specifies the file name of the module to load. This name is not related to the name stored in a library module itself, as specified by the LIBRARY keyword in the module-definition (.def) file.</param>
        ''' <param name="hFile">This parameter is reserved for future use. It must be NULL.</param>
        ''' <param name="dwFlags">The action to be taken when loading the module. If no flags are specified, the behavior of this function is identical to that of the <see cref="LoadLibrary"/> function.</param>
        ''' <returns>If the function succeeds, the return value is a handle to the loaded module. If the function fails, the return value is NULL.</returns>
        Public Declare Auto Function LoadLibraryEx Lib "kernel32" (ByVal lpFileName$, hFile As IntPtr, dwFlags As LoadLibraryMode) As IntPtr

#Region "Resources"
        ''' <summary>Determines the location of a resource with the specified type and name in the specified module.</summary>
        ''' <param name="hModule">A handle to the module whose portable executable file or an accompanying MUI file contains the resource. If this parameter is NULL, the function searches the module used to create the current process.</param>
        ''' <param name="lpName">The name of the resource.</param>
        ''' <param name="lpType">The resource type.</param>
        ''' <returns>If the function succeeds, the return value is a handle to the specified resource's information block. To obtain a handle to the resource, pass this handle to the LoadResource function. If the function fails, the return value is NULL. </returns>
        Public Declare Auto Function FindResource Lib "Kernel32" (hModule As IntPtr, lpName As String, lpType As String) As IntPtr
        ''' <summary>Determines the location of a resource with the specified type and name in the specified module.</summary>
        ''' <param name="hModule">A handle to the module whose portable executable file or an accompanying MUI file contains the resource. If this parameter is NULL, the function searches the module used to create the current process.</param>
        ''' <param name="lpId">ID of the resource.</param>
        ''' <param name="lpType">The resource type.</param>
        ''' <returns>If the function succeeds, the return value is a handle to the specified resource's information block. To obtain a handle to the resource, pass this handle to the LoadResource function. If the function fails, the return value is NULL. </returns>
        Public Declare Auto Function FindResource Lib "Kernel32" (hModule As IntPtr, lpId As IntPtr, lpType As String) As IntPtr
        ''' <summary>Determines the location of a resource with the specified type and name in the specified module.</summary>
        ''' <param name="hModule">A handle to the module whose portable executable file or an accompanying MUI file contains the resource. If this parameter is NULL, the function searches the module used to create the current process.</param>
        ''' <param name="lpId">ID of the resource.</param>
        ''' <param name="lpType">The resource type.</param>
        ''' <returns>If the function succeeds, the return value is a handle to the specified resource's information block. To obtain a handle to the resource, pass this handle to the LoadResource function. If the function fails, the return value is NULL. </returns>
        Public Declare Auto Function FindResource Lib "Kernel32" (hModule As IntPtr, lpId As IntPtr, lpType As IntPtr) As IntPtr
        ''' <summary>Determines the location of a resource with the specified type and name in the specified module.</summary>
        ''' <param name="hModule">A handle to the module whose portable executable file or an accompanying MUI file contains the resource. If this parameter is NULL, the function searches the module used to create the current process.</param>
        ''' <param name="lpName">The name of the resource.</param>
        ''' <param name="lpType">The resource type.</param>
        ''' <returns>If the function succeeds, the return value is a handle to the specified resource's information block. To obtain a handle to the resource, pass this handle to the LoadResource function. If the function fails, the return value is NULL. </returns>
        Public Declare Auto Function FindResource Lib "Kernel32" (hModule As IntPtr, lpName As String, lpType As IntPtr) As IntPtr

        ''' <summary>Retrieves a handle that can be used to obtain a pointer to the first byte of the specified resource in memory.</summary>
        ''' <param name="hModule">A handle to the module whose executable file contains the resource. If hModule is NULL, the system loads the resource from the module that was used to create the current process.</param>
        ''' <param name="hResInfo">A handle to the resource to be loaded. This handle is returned by the FindResource or FindResourceEx function.</param>
        ''' <returns>If the function succeeds, the return value is a handle to the data associated with the resource. If the function fails, the return value is NULL.</returns>
        ''' <remarks>It is not necessary to free the resources loaded using <see cref="LoadResource"/>.</remarks>
        Public Declare Function LoadResource Lib "kernel32" (hModule As IntPtr, hResInfo As IntPtr) As IntPtr

        ''' <summary>Retrieves the size, in bytes, of the specified resource.</summary>
        ''' <param name="hModule">A handle to the module whose executable file contains the resource.</param>
        ''' <param name="hResInfo">A handle to the resource. This handle must be created by using the FindResource or FindResourceEx function.</param>
        ''' <returns>If the function succeeds, the return value is the number of bytes in the resource. If the function fails, the return value is zero.</returns>
        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Function SizeofResource(hModule As IntPtr, hResInfo As IntPtr) As UInteger
        End Function
        ''' <summary>Enumerates resource types within a binary module. Starting with Windows Vista, this is typically a language-neutral Portable Executable (LN file), and the enumeration also includes resources from one of the corresponding language-specific resource files (.mui files)—if one exists—that contain localizable language resources. It is also possible to use hModule to specify a .mui file, in which case only that file is searched for resource types.</summary>
        ''' <param name="hModule">A handle to a module to be searched. This handle must be obtained through LoadLibrary or LoadLibraryEx. See Remarks for more information. If this parameter is NULL, that is equivalent to passing in a handle to the module used to create the current process.</param>
        ''' <param name="lpEnumFunc">A pointer to the callback function to be called for each enumerated resource type. For more information, see the <see cref="EnumResTypeProc"/> function.</param>
        ''' <param name="lParam">An application-defined value passed to the callback function.</param>
        ''' <returns>Returns TRUE if successful; otherwise, FALSE.</returns>
        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Function EnumResourceTypes(ByVal hModule As IntPtr, ByVal lpEnumFunc As EnumResTypeProc, ByVal lParam As IntPtr) As Boolean
        End Function
        ''' <summary>An application-defined callback function used with the EnumResourceTypes and EnumResourceTypesEx functions. It receives resource types. The ENUMRESTYPEPROC type defines a pointer to this callback function. EnumResTypeProc is a placeholder for the application-defined function name.</summary>
        ''' <param name="hModule">A handle to the module whose executable file contains the resources for which the types are to be enumerated. If this parameter is NULL, the function enumerates the resource types in the module used to create the current process.</param>
        ''' <param name="lpszType">The type of resource for which the type is being enumerated.</param>
        ''' <param name="lParam">An application-defined parameter passed to the EnumResourceTypes or EnumResourceTypesEx function. This parameter can be used in error checking.</param>
        ''' <returns>Returns TRUE to continue enumeration or FALSE to stop enumeration.</returns>
        Public Delegate Function EnumResTypeProc(hModule As IntPtr, lpszType As IntPtr, lParam As IntPtr) As Boolean

        ''' <summary>Enumerates resources of a specified type within a binary module. For Windows Vista and later, this is typically a language-neutral Portable Executable (LN file), and the enumeration will also include resources from the corresponding language-specific resource files (.mui files) that contain localizable language resources. It is also possible for hModule to specify an .mui file, in which case only that file is searched for resources.</summary>
        ''' <param name="hModule">A handle to a module to be searched. Starting with Windows Vista, if this is an LN file, then appropriate .mui files (if any exist) are included in the search. If this parameter is NULL, that is equivalent to passing in a handle to the module used to create the current process.</param>
        ''' <param name="lpszType">The type of the resource for which the name is being enumerated.</param>
        ''' <param name="lpEnumFunc">A pointer to the callback function to be called for each enumerated resource name or ID. </param>
        ''' <param name="lParam">An application-defined value passed to the callback function. This parameter can be used in error checking.</param>
        ''' <returns>The return value is TRUE if the function succeeds or FALSE if the function does not find a resource of the type specified, or if the function fails for another reason.</returns>
        <DllImport("kernel32.dll", CharSet:=CharSet.Unicode, EntryPoint:="EnumResourceNamesW", SetLastError:=True)> _
        Public Function EnumResourceNames(ByVal hModule As IntPtr, ByVal lpszType As String, ByVal lpEnumFunc As EnumResNameProc, ByVal lParam As IntPtr) As Boolean
        End Function
        ''' <summary>Enumerates resources of a specified type within a binary module. For Windows Vista and later, this is typically a language-neutral Portable Executable (LN file), and the enumeration will also include resources from the corresponding language-specific resource files (.mui files) that contain localizable language resources. It is also possible for hModule to specify an .mui file, in which case only that file is searched for resources.</summary>
        ''' <param name="hModule">A handle to a module to be searched. Starting with Windows Vista, if this is an LN file, then appropriate .mui files (if any exist) are included in the search. If this parameter is NULL, that is equivalent to passing in a handle to the module used to create the current process.</param>
        ''' <param name="lpszType">The type of the resource for which the name is being enumerated.</param>
        ''' <param name="lpEnumFunc">A pointer to the callback function to be called for each enumerated resource name or ID. </param>
        ''' <param name="lParam">An application-defined value passed to the callback function. This parameter can be used in error checking.</param>
        ''' <returns>The return value is TRUE if the function succeeds or FALSE if the function does not find a resource of the type specified, or if the function fails for another reason.</returns>
        <DllImport("kernel32.dll", CharSet:=CharSet.Unicode, EntryPoint:="EnumResourceNamesW", SetLastError:=True)> _
        Public Function EnumResourceNames(ByVal hModule As IntPtr, ByVal lpszType As IntPtr, ByVal lpEnumFunc As EnumResNameProc, ByVal lParam As IntPtr) As Boolean
        End Function

        ''' <summary>An application-defined callback function used with the EnumResourceNames and EnumResourceNamesEx functions. It receives the type and name of a resource. The ENUMRESNAMEPROC type defines a pointer to this callback function. EnumResNameProc is a placeholder for the application-defined function name.</summary>
        ''' <param name="hModule">A handle to the module whose executable file contains the resources that are being enumerated. If this parameter is NULL, the function enumerates the resource names in the module used to create the current process.</param>
        ''' <param name="lpszType">The type of resource for which the name is being enumerated.</param>
        ''' <param name="lpszName">The name of a resource of the type being enumerated.</param>
        ''' <param name="lParam">An application-defined parameter passed to the EnumResourceNames or EnumResourceNamesEx function. This parameter can be used in error checking.</param>
        ''' <returns>Returns TRUE to continue enumeration or FALSE to stop enumeration.</returns>
        Public Delegate Function EnumResNameProc(ByVal hModule As IntPtr, ByVal lpszType As IntPtr, ByVal lpszName As IntPtr, ByVal lParam As IntPtr) As Boolean

        ''' <summary>Loads a string resource from the executable file associated with a specified module, copies the string into a buffer, and appends a terminating null character.</summary>
        ''' <param name="hInstance">A handle to an instance of the module whose executable file contains the string resource. To get the handle to the application itself, call the GetModuleHandle function with NULL.</param>
        ''' <param name="uID">The identifier of the string to be loaded.</param>
        ''' <param name="lpBuffer">The buffer is to receive the string.</param>
        ''' <param name="nBufferMax">The size of the buffer, in characters. The string is truncated and null-terminated if it is longer than the number of characters specified. If this parameter is 0, then lpBuffer receives a read-only pointer to the resource itself.</param>
        ''' <returns>If the function succeeds, the return value is the number of characters copied into the buffer, not including the terminating null character, or zero if the string resource does not exist. </returns>
        <DllImport("User32", SetLastError:=True)> _
        Public Function LoadString(ByVal hInstance As IntPtr, ByVal uID As UInt32, ByVal lpBuffer As System.Text.StringBuilder, ByVal nBufferMax As Integer) As Integer
        End Function

        ''' <summary>Loads a string resource from the executable file associated with a specified module, copies the string into a buffer, and appends a terminating null character.</summary>
        ''' <param name="hInstance">A handle to an instance of the module whose executable file contains the string resource. To get the handle to the application itself, call the GetModuleHandle function with NULL.</param>
        ''' <param name="uID">The identifier of the string to be loaded.</param>
        ''' <param name="lpBuffer">The buffer is to receive the string.</param>
        ''' <param name="nBufferMax">The size of the buffer, in characters. The string is truncated and null-terminated if it is longer than the number of characters specified. If this parameter is 0, then lpBuffer receives a read-only pointer to the resource itself.</param>
        ''' <returns>If the function succeeds, the return value is the number of characters copied into the buffer, not including the terminating null character, or zero if the string resource does not exist. </returns>
        <DllImport("User32", SetLastError:=True)> _
        Public Function LoadString(ByVal hInstance As IntPtr, ByVal uID As UInt32, ByRef lpBuffer As IntPtr, ByVal nBufferMax As Integer) As Integer
        End Function

        ''' <summary>Determines whether a value is an integer identifier for a resource.</summary>
        ''' <param name="value">he integer value to be tested.</param>
        ''' <returns>If the value is a resource identifier, the return value is TRUE. Otherwise, the return value is FALSE.</returns>
        ''' <remarks>This macro checks whether all bits except the least 16 bits are zero. When true, wInteger is an integer identifier for a resource. Otherwise it is typically a pointer to a string.</remarks>
        Public Function IsIntRresource(value As IntPtr) As Boolean
            Return value.ToInt64 <= UShort.MaxValue
        End Function
#End Region

        ''' <summary>Deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.</summary>
        ''' <param name="hObject">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
        ''' <returns>If the function succeeds, the return value is true. If the specified handle is not valid or is currently selected into a DC, the return value is false.</returns>
        ''' <remarks>
        ''' <para>Do not delete a drawing object (pen or brush) while it is still selected into a DC.</para>
        ''' <para>When a pattern brush is deleted, the bitmap associated with the brush is not deleted. The bitmap must be deleted independently.</para>
        ''' </remarks>
        Public Declare Function DeleteObject Lib "gdi32.dll" (ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean


        ''' <summary>Closes an open object handle.</summary>
        ''' <param name="hObject">A valid handle to an open object.</param>
        ''' <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
        Public Declare Function CloseHandle Lib "Kernel32.dll" (ByVal hObject As IntPtr) As Boolean

        ''' <summary>Flags for the <see cref="LoadLibraryEx"/> method</summary>
        <Flags()>
        Public Enum LoadLibraryMode As UInteger
            ''' <summary>If this value is used, and the executable module is a DLL, the system does not call DllMain for process and thread initialization and termination. Also, the system does not load additional executable modules that are referenced by the specified module.</summary>
            DontResolveDllReferences = &H1
            ''' <summary>If this value is used, the system does not check AppLocker rules or apply Software Restriction Policies for the DLL. This action applies only to the DLL being loaded and not to its dependencies. This value is recommended for use in setup programs that must run extracted DLLs during installation.</summary>
            IgnoreCodeAuthzLevel = &H10
            ''' <summary>If this value is used, the system maps the file into the calling process's virtual address space as if it were a data file. Nothing is done to execute or prepare to execute the mapped file. Therefore, you cannot call functions like GetModuleFileName, GetModuleHandle or GetProcAddress with this DLL. Using this value causes writes to read-only memory to raise an access violation. Use this flag when you want to load a DLL only to extract messages or resources from it.</summary>
            LoadLibraryAsDatafile = &H2
            ''' <summary>Similar to <see cref="LoadLibraryAsDatafile"/>, except that the DLL file on the disk is opened for exclusive write access. Therefore, other processes cannot open the DLL file for write access while it is in use. However, the DLL can still be opened by other processes.</summary>
            LoadLibraryAsDatafile_EXCLUSIVE = &H40
            ''' <summary>If this value is used, the system maps the file into the process's virtual address space as an image file. However, the loader does not load the static imports or perform the other usual initialization steps. Use this flag when you want to load a DLL only to extract messages or resources from it.</summary>
            LoadLibraryAsImageResource = &H20
            ''' <summary>If this value is used, the application's installation directory is searched for the DLL and its dependencies. Directories in the standard search path are not searched. This value cannot be combined with <see cref="AlteredSearchPath"/>.</summary>
            SearchApplicationDir = &H200
            ''' <summary>This value is a combination of <see cref="SearchApplicationDir"/>, <see cref="SerachSystem32"/>, and <see cref="SearchUserDirs"/>. Directories in the standard search path are not searched. This value cannot be combined with <see cref="AlteredSearchPath"/>.</summary>
            SearchDefaultDirs = &H1000
            ''' <summary>If this value is used, the directory that contains the DLL is temporarily added to the beginning of the list of directories that are searched for the DLL's dependencies. Directories in the standard search path are not searched.</summary>
            SerachDllLoadDir = &H100
            ''' <summary>If this value is used, %windows%\system32 is searched for the DLL and its dependencies. Directories in the standard search path are not searched. This value cannot be combined with <see cref="AlteredSearchPath"/>.</summary>
            SerachSystem32 = &H800
            ''' <summary>If this value is used, directories added using the AddDllDirectory or the SetDllDirectory function are searched for the DLL and its dependencies. If more than one directory has been added, the order in which the directories are searched is unspecified. Directories in the standard search path are not searched. This value cannot be combined with <see cref="AlteredSearchPath"/>.</summary>
            SearchUserDirs = &H400
            ''' <summary>If this value is used and lpFileName specifies an absolute path, the system uses the alternate file search strategy discussed in the Remarks section to find associated executable modules that the specified module causes to be loaded. If this value is used and lpFileName specifies a relative path, the behavior is undefined.</summary>
            AlteredSearchPath = &H8
        End Enum
    End Module

    ''' <summary>Misc Win32 API declrations</summary>
    Friend Module Misc

        ''' <summary>Undocumented Win API function for getting localized character names</summary>
        ''' <param name="wCharCode">Character code (code-point)</param>
        ''' <param name="lpBuf">When function returns returns return value</param>
        ''' <returns>Number of characters returned</returns>
        Public Declare Function GetUName Lib "getuname.dll" (ByVal wCharCode As UShort, <MarshalAs(UnmanagedType.LPWStr)> ByVal lpbuf As System.Text.StringBuilder) As Integer

        'int
        'WINAPI
        'GetUName(IN WORD wCharCode,
        '         OUT LPWSTR lpBuf)
        '{
        '        wcscpy(lpBuf, L"Undefined");
        '    return 0;
        '}

    End Module

End Namespace


