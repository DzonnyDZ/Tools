Namespace API
    ''' <summary>Contains Win32 API declarations related to threads</summary>
    Friend Module Threads
        <Flags()>
        Public Enum ThreadAccess As Integer
            ''' <summary>Required to delete the object.</summary>
            ''' <remarks>This is standard access right.</remarks>
            DELETE = &H10000
            ''' <summary>Required to read information in the security descriptor for the object, not including the information in the SACL. To read or write the SACL, you must request the ACCESS_SYSTEM_SECURITY access right. For more information, see SACL Access Right.</summary>
            ''' <remarks>This is standard access right.</remarks>
            READ_CONTROL = &H20000
            ''' <summary>Required to modify the DACL in the security descriptor for the object.</summary>
            ''' <remarks>This is standard access right.</remarks>
            WRITE_DAC = &H40000
            ''' <summary>Required to change the owner in the security descriptor for the object.</summary>
            ''' <remarks>This is standard access right.</remarks>
            WRITE_OWNER = &H80000
            ''' <summary>Enables the use of the thread handle in any of the wait functions.</summary>
            ''' <remarks>This both - standard and thread-specific access right. The standard access right is described as: The right to use the object for synchronization. This enables a thread to wait until the object is in the signaled state.</remarks>
            SYNCHRONIZE = &H100000
            ''' <summary>Required for a server thread that impersonates a client.</summary>
            ''' <remarks>This is thread-specific access right.</remarks>
            THREAD_DIRECT_IMPERSONATION = &H200
            ''' <summary>Required to read the context of a thread using GetThreadContext.</summary>
            ''' <remarks>This is thread-specific access right.</remarks>
            THREAD_GET_CONTEXT = &H8
            ''' <summary>Required to use a thread's security information directly without calling it by using a communication mechanism that provides impersonation services.</summary>
            ''' <remarks>This is thread-specific access right.</remarks>
            THREAD_IMPERSONATE = &H100
            ''' <summary>Required to read certain information from the thread object, such as the exit code (see GetExitCodeThread).</summary>
            ''' <remarks>This is thread-specific access right.</remarks>
            THREAD_QUERY_INFORMATION = &H40
            ''' <summary>Required to read certain information from the thread objects (see GetProcessIdOfThread). A handle that has the <see cref="THREAD_QUERY_INFORMATION"/> access right is automatically granted <see cref="THREAD_QUERY_LIMITED_INFORMATION"/>.</summary>
            ''' <remarks>Windows Server 2003 and Windows XP/2000:  This access right is not supported.
            ''' <para>This is thread-specific access right.</para></remarks>
            THREAD_QUERY_LIMITED_INFORMATION = &H800
            ''' <summary>Required to write the context of a thread using SetThreadContext.</summary>
            ''' <remarks>This is thread-specific access right.</remarks>
            THREAD_SET_CONTEXT = &H10
            ''' <summary>Required to set certain information in the thread object.</summary>
            ''' <remarks>This is thread-specific access right.</remarks>
            THREAD_SET_INFORMATION = &H20
            ''' <summary>Required to set certain information in the thread object. A handle that has the <see cref="THREAD_SET_INFORMATION"/> access right is automatically granted <see cref="THREAD_SET_LIMITED_INFORMATION"/>.</summary>
            ''' <remarks>Windows Server 2003 and Windows XP/2000:  This access right is not supported.
            ''' <para>This is thread-specific access right.</para></remarks>
            THREAD_SET_LIMITED_INFORMATION = &H400
            ''' <summary>Required to set the impersonation token for a thread using SetThreadToken.</summary>
            ''' <remarks>This is thread-specific access right.</remarks>
            THREAD_SET_THREAD_TOKEN = &H80
            ''' <summary>Required to suspend or resume a thread (see <see cref="SuspendThread"/> and <see cref="ResumeThread"/>).</summary>
            ''' <remarks>This is thread-specific access right.</remarks>
            THREAD_SUSPEND_RESUME = &H2
            ''' <summary>Required to terminate a thread using TerminateThread.</summary>
            ''' <remarks>This is thread-specific access right.</remarks>
            THREAD_TERMINATE = &H1
        End Enum
        ''' <summary>Opens an existing thread object.</summary>
        ''' <param name="dwDesiredAccess">The access to the thread object. This access right is checked against the security descriptor for the thread. This parameter can be one or more of the thread access rights.
        ''' <para>If the caller has enabled the SeDebugPrivilege privilege, the requested access is granted regardless of the contents of the security descriptor.</para></param>
        ''' <param name="bInheritHandle">If this value is TRUE, processes created by this process will inherit the handle. Otherwise, the processes do not inherit this handle.</param>
        ''' <param name="dwThreadId">The identifier of the thread to be opened.</param>
        ''' <returns>If the function succeeds, the return value is an open handle to the specified thread. If the function fails, the return value is <see cref="IntPtr.Zero"/>.</returns>
        Public Declare Function OpenThread Lib "kernel32.dll" (ByVal dwDesiredAccess As ThreadAccess, ByVal bInheritHandle As Boolean, ByVal dwThreadId As UInteger) As IntPtr
        ''' <summary>Suspends the specified thread.</summary>
        ''' <param name="hThread">A handle to the thread that is to be suspended. </param>
        ''' <returns>If the function succeeds, the return value is the thread's previous suspend count; otherwise, it is -1. </returns>
        Public Declare Function SuspendThread Lib "kernel32.dll" (ByVal hThread As IntPtr) As Integer
        ''' <summary>Decrements a thread's suspend count. When the suspend count is decremented to zero, the execution of the thread is resumed.</summary>
        ''' <param name="hThread">A handle to the thread to be restarted.</param>
        ''' <returns>If the function succeeds, the return value is the thread's previous suspend count. If the function fails, the return value is -1</returns>
        Public Declare Function ResumeThread Lib "kernel32.dll" (ByVal hThread As IntPtr) As Integer
    End Module
End Namespace