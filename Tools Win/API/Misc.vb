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
    End Class


    ''' <summary>Common Win32 API declarations</summary>
    <DoNotApplyAuthorAndVersionAttributes()> _
    Friend Module Common
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
        ''' <remarks>Visual Basic:  Applications should call <see cref="ErrObject.LastDllError"/> of <see cref="err"/> instead of <see cref="GetLastError"/>.</remarks>
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
            Return LastDllErrorInfo(GetLastError)
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
    End Module
End Namespace


