Imports System.Diagnostics, System.Threading
Imports System.Runtime.CompilerServices

Namespace DiagnosticsT
#If True
    ''' <summary>Provides extension functions for working with processes</summary>
    ''' <version version="1.5.3">This module is new in version 1.5.3</version>
    Public Module ProcessExtensions
        ''' <summary>Suspends a process by suspending all threads of the process</summary>
        ''' <param name="process">A process to be suspended</param>
        ''' <exception cref="ArgumentNullException"><paramref name="process"/> is null.</exception>
        ''' <exception cref="API.Win32APIException">Failed to suspend a thread. In this case an attempt is made to restore previous state of all threads which's suspension didn't failed.</exception>
        <Extension()>
        Public Sub Suspend(ByVal process As Process)
            If process Is Nothing Then Throw New ArgumentNullException("process")
            Dim suspendedThreads As New List(Of ProcessThread)
            Try
                For Each thread As ProcessThread In process.Threads
                    thread.Suspend()
                    suspendedThreads.Add(thread)
                Next
            Catch
                For Each thread In suspendedThreads
                    Try
                        thread.Resume()
                    Catch : End Try
                Next
                Throw
            End Try
        End Sub
        ''' <summary>Resumes a process by resuming all threads of the process</summary>
        ''' <param name="process">A process to be resumed</param>
        ''' <exception cref="ArgumentNullException"><paramref name="process"/> is null.</exception>
        ''' <exception cref="API.Win32APIException">Failed to resume a thread. In this case an attempt is made to restore previous state of all threads which's resumal didn't failed.</exception>
        <Extension()>
        Public Sub [Resume](ByVal process As Process)
            If process Is Nothing Then Throw New ArgumentNullException("process")
            Dim resumedThreads As New List(Of ProcessThread)
            Try
                For Each thread As ProcessThread In process.Threads
                    thread.Resume()
                    resumedThreads.Add(thread)
                Next
            Catch
                For Each thread In resumedThreads
                    Try
                        thread.Suspend()
                    Catch : End Try
                Next
                Throw
            End Try
        End Sub

        ''' <summary>Suspends the specified thread</summary>
        ''' <param name="thread">The thread to be suspended</param>
        ''' <returns>Actual thread's suspend count. Returns 1 when thread was suspended by this call, returns number >1 if thread was already suspended.</returns>
        ''' <remarks>You can use pair of <see cref="Suspend"/> and <see cref="[Resume]"/> calls to ensure that thread is suspended (<see cref="Suspend"/>) and resume thread to it's previous state (<see cref="[Resume]"/>).</remarks>
        ''' <exception cref="API.Win32APIException">Failed to obtain access to given thread or to suspend the thread</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="thread"/> is null.</exception>
        <Extension()>
        Public Function Suspend(ByVal thread As ProcessThread) As Integer
            If thread Is Nothing Then Throw New ArgumentNullException("thread")
            Return SuspendThread(thread.Id)
        End Function
        ''' <summary>Decrements a thread's suspend count. When the suspend count is decremented to zero, the execution of the thread is resumed.</summary>
        ''' <param name="thread">The thread to be resumed.</param>
        ''' <returns>Actual thread's suspend count. Returns 0 when thread was actually resumed by this call, returns number >0 if thread was not suspended by this call.</returns>
        ''' <remarks>You can use pair of <see cref="Suspend"/> and <see cref="[Resume]"/> calls to ensure that thread is suspended (<see cref="Suspend"/>) and resume thread to it's previous state (<see cref="[Resume]"/>).</remarks>
        ''' <exception cref="API.Win32APIException">Failed to obtain access to given thread or to resume the thread</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="thread"/> is null.</exception>
        <Extension()>
        Public Function [Resume](ByVal thread As ProcessThread) As Integer
            If thread Is Nothing Then Throw New ArgumentNullException("thread")
            Return ResumeThread(thread.Id)
        End Function

        ''' <summary>Suspends a thread identified by thread ID</summary>
        ''' <param name="threadId">ID of the thread to be suspended.</param>
        ''' <returns>Actual thread's suspend count. Returns 1 when thread was suspended by this call, returns number >1 if thread was already suspended.</returns>
        ''' <remarks>You can use pair of <see cref="SuspendThread"/> and <see cref="[ResumeThread]"/> calls to ensure that thread is suspended (<see cref="SuspendThread"/>) and resume thread to it's previous state (<see cref="[ResumeThread]"/>).</remarks>
        ''' <exception cref="API.Win32APIException">Failed to obtain access to given thread or to suspend the thread</exception>
        Public Function SuspendThread(ByVal threadId As Integer) As Integer
            Dim trh = API.OpenThread(API.ThreadAccess.THREAD_SUSPEND_RESUME, False, threadId)
            If trh = IntPtr.Zero Then Throw New API.Win32APIException
            Try
                Dim suspendCount = API.SuspendThread(trh)
                If suspendCount = -1 Then Throw New API.Win32APIException
                Return suspendCount + 1
            Finally
                API.CloseHandle(trh)
            End Try
        End Function
        ''' <summary>Decrements a thread's suspend count of a thread identified by ID. When the suspend count is decremented to zero, the execution of the thread is resumed.</summary>
        ''' <param name="threadId">ID of the thread to be resumed.</param>
        ''' <returns>Actual thread's suspend count. Returns 0 when thread was actually resumed by this call, returns number >0 if thread was not suspended by this call.</returns>
        ''' <remarks>You can use pair of <see cref="SuspendThread"/> and <see cref="[ResumeThread]"/> calls to ensure that thread is suspended (<see cref="SuspendThread"/>) and resume thread to it's previous state (<see cref="[ResumeThread]"/>).</remarks>
        ''' <exception cref="API.Win32APIException">Failed to obtain access to given thread or to resume the thread</exception>
        Public Function ResumeThread(ByVal threadId As Integer) As Integer
            Dim trh = API.OpenThread(API.ThreadAccess.THREAD_SUSPEND_RESUME, False, threadId)
            If trh = IntPtr.Zero Then Throw New API.Win32APIException
            Try
                Dim suspendCount = API.ResumeThread(trh)
                If suspendCount = -1 Then Throw New API.Win32APIException
                Return suspendCount - 1
            Finally
                API.CloseHandle(trh)
            End Try
        End Function
    End Module
#End If
End Namespace