Imports Tools.ExtensionsT
Imports System.Runtime.CompilerServices

#If True
Namespace IOt
    ''' <summary>Contains static functions for working with files and directories</summary>
    ''' <version version="1.5.2" stage="Nightly">Module introduced</version>
    Public Module FileSystemOperations
        ''' <summary>Recursivelly copies directory structure from one directory to another</summary>
        ''' <param name="SourcePath">Directory to be copyied</param>
        ''' <param name="TargetPath">Directory content of <paramref name="SourcePath"/> to be copyied to</param>
        ''' <param name="CallBack">Callback function to report progress and allow cancellation. Can be null.</param>
        ''' <param name="ErrorCallBack">Callback function called when an error occures. Allows error recovery. Can be null. If null no error recovery is done - all exceptions are thrown. To overwrite existing file, this callback must be provided.</param>
        ''' <param name="KeepOriginalTime">True to preserve file create/write/access time on copy (applies to directories as well but only to directories created by this function)</param>
        ''' <remarks>This function can copy single file as well - when <paramref name="SourcePath"/> point to file. In such case <paramref name="TargetPath"/> must point to file as well.
        ''' <para>See <see cref="FileCopy"/> for details of how this function works.</para></remarks>
        ''' <exception cref="FileAlreadyExistsException"><paramref name="TargetPath"/> file exists and <paramref name="ErrorCallBack"/> is null or it does not return <see cref="PathCopyCallbackResult.Retry"/> when called with <see cref="FileAlreadyExistsException"/>.</exception>
        ''' <exception cref="IO.IOException">An IO exception occured while openning source or target file or when reading source file or when writing target file or when manipulating directories and <paramref name="ErrorCallBack"/> is null or it returns <see cref="PathCopyCallbackResult.Abort"/> (or <see cref="PathCopyCallbackResult"/> non-mmeber) for the exception.</exception>
        ''' <exception cref="Security.SecurityException">A security exception oocured while openning source or target file or when manipulating directories and <paramref name="ErrorCallBack"/> is null or ire returns <see cref="PathCopyCallbackResult.Abort"/> (or <see cref="PathCopyCallbackResult"/> non-member) for the exception.</exception>
        ''' <exception cref="UnauthorizedAccessException">Access denied exception oocured while openning source or target file or when manipulating directories and <paramref name="ErrorCallBack"/> is null or ire returns <see cref="PathCopyCallbackResult.Abort"/> (or <see cref="PathCopyCallbackResult"/> non-member) for the exception.</exception>
        ''' <exception cref="OperationCanceledException"><paramref name="CallBack"/> returned <see cref="PathCopyCallbackResult.Abort"/></exception>
        ''' <exception cref="InvalidOperationException"><paramref name="CallBack"/> returned <see cref="PathCopyCallbackResult.Ignore"/> when its parameter SourceFileName was null.</exception>
        <Extension()> Public Sub Copy(ByVal SourcePath As Path, ByVal TargetPath As Path, ByVal CallBack As PathCopyCallBack, ByVal ErrorCallBack As PathCopyErrorCallBack, Optional ByVal KeepOriginalTime As Boolean = False)
            If SourcePath.IsFile Then
                If Not CType(TargetPath.DirectoryName, Path).IsDirectory Then
CreateDirectoryForFile: Try
                        IO.Directory.CreateDirectory(TargetPath.DirectoryName)
                    Catch ex As Exception When (TypeOf ex Is IO.IOException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is Security.SecurityException) AndAlso ErrorCallBack IsNot Nothing
                        Select Case ErrorCallBack.Invoke(SourcePath, TargetPath, PathCopyStages.CreateDirectory, ex)
                            Case PathCopyCallbackResult.Cancel : Exit Sub
                            Case PathCopyCallbackResult.Ignore 'Do nothing
                            Case PathCopyCallbackResult.Retry : GoTo CreateDirectoryForFile
                            Case Else : Throw
                        End Select
                    End Try
                End If
                Dim result = FileCopy(SourcePath, TargetPath, CallBack, ErrorCallBack)
                If result And FileCopyResult.Terminate Then Exit Sub
                If result = FileCopyResult.Copyed Then
                    If Not PreserveAttributes(SourcePath, TargetPath, ErrorCallBack, KeepOriginalTime) Then Exit Sub
                End If
            ElseIf SourcePath.IsDirectory Then
                'Create target directory
                If Not TargetPath.IsDirectory Then
CreateDirectory:    Try
                        IO.Directory.CreateDirectory(TargetPath)
                    Catch ex As Exception When (TypeOf ex Is IO.IOException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is Security.SecurityException) AndAlso ErrorCallBack IsNot Nothing
                        Select Case ErrorCallBack.Invoke(SourcePath, TargetPath, PathCopyStages.CreateDirectory, ex)
                            Case PathCopyCallbackResult.Cancel : Exit Sub
                            Case PathCopyCallbackResult.Ignore 'Do nothing
                            Case PathCopyCallbackResult.Retry : GoTo CreateDirectory
                            Case Else : Throw
                        End Select
                    End Try
SetDirectoryAttributes: Try
                        Dim di As New IO.DirectoryInfo(SourcePath)
                        Dim di2 As New IO.DirectoryInfo(TargetPath)
                        di2.Attributes = di.Attributes
                        If KeepOriginalTime Then
                            IO.Directory.SetCreationTimeUtc(TargetPath, IO.Directory.GetCreationTimeUtc(SourcePath))
                            IO.Directory.SetLastAccessTimeUtc(TargetPath, IO.Directory.GetLastAccessTimeUtc(SourcePath))
                            IO.Directory.SetLastWriteTimeUtc(TargetPath, IO.Directory.GetLastWriteTimeUtc(SourcePath))
                        End If
                    Catch ex As Exception When (TypeOf ex Is IO.IOException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is Security.SecurityException) AndAlso ErrorCallBack IsNot Nothing
                        Select Case ErrorCallBack.Invoke(SourcePath, TargetPath, PathCopyStages.SetDirectoryAttributes, ex)
                            Case PathCopyCallbackResult.Cancel : Exit Sub
                            Case PathCopyCallbackResult.Ignore 'Do nothing
                            Case PathCopyCallbackResult.Retry : GoTo SetDirectoryAttributes
                            Case Else : Throw
                        End Select
                    End Try
                End If
                'Copy files
                For Each File In SourcePath.GetFiles()
                    Dim Result = FileCopy(File, TargetPath + File.FileName, CallBack, ErrorCallBack)
                    If Result And FileCopyResult.Terminate Then Exit Sub
                    If Result = FileCopyResult.Copyed Then
                        If Not PreserveAttributes(File, TargetPath + File.FileName, ErrorCallBack, KeepOriginalTime) Then Exit Sub
                    End If
                Next
                'Copy subfolders
                For Each SubDir In SourcePath.GetSubFolders()
                    Copy(SubDir, TargetPath + SubDir.FileName, CallBack, ErrorCallBack)
                Next
            End If
        End Sub
        ''' <summary>Applies attributes and optionaly times form one file to another</summary>
        ''' <param name="SourceFile">Source of attributes and times</param>
        ''' <param name="TargetFile">Target file</param>
        ''' <param name="ErrorCallBack">Error callback function</param>
        ''' <param name="KeepOriginalTime">True to preserve times (create/write/access) as well as attributes</param>
        ''' <returns>True when operation can continue; false otherwise</returns>
        ''' <exception cref="IO.IOException">An IO exception occured not handle by callback</exception>
        ''' <exception cref="Security.SecurityException">A security exception occured not handled by the callback</exception>
        ''' <exception cref="UnauthorizedAccessException">An access denied exception occured not handld by callback</exception>
        Private Function PreserveAttributes(ByVal SourceFile As Path, ByVal TargetFile As Path, ByVal ErrorCallBack As PathCopyErrorCallBack, ByVal KeepOriginalTime As Boolean) As Boolean
            PreserveAttributes = True
            'Attributes
SetFileAttributes: Try
                IO.File.SetAttributes(TargetFile, IO.File.GetAttributes(SourceFile))
                If KeepOriginalTime Then
                    IO.File.SetCreationTimeUtc(TargetFile, IO.File.GetCreationTimeUtc(SourceFile))
                    IO.File.SetLastAccessTimeUtc(TargetFile, IO.File.GetLastAccessTimeUtc(SourceFile))
                    IO.File.SetLastWriteTimeUtc(TargetFile, IO.File.GetLastWriteTimeUtc(SourceFile))
                End If
            Catch ex As Exception When (TypeOf ex Is IO.IOException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is Security.SecurityException) AndAlso ErrorCallBack IsNot Nothing
                Select Case ErrorCallBack.Invoke(SourceFile, TargetFile, PathCopyStages.SetFileAttributes, ex)
                    Case PathCopyCallbackResult.Cancel : Return False
                    Case PathCopyCallbackResult.Ignore 'Do nothing
                    Case PathCopyCallbackResult.Retry : GoTo SetFileAttributes
                    Case Else : Throw
                End Select
            End Try
        End Function
        ''' <summary>Copies single file from source path to target path</summary>
        ''' <param name="SourcePath">Path to source file</param>
        ''' <param name="TargetPath">Path to destination file (not only target directory, shall include file name)</param>
        ''' <param name="CallBack">Callback function to report progress and allow cancellation. Can be null.</param>
        ''' <param name="ErrorCallBack">Callback function called when an error occures. Allows error recovery. Can be null. If null no error recovery is done - all exceptions are thrown. To overwrite existing file, this callback must be provided.</param>
        ''' <returns>Status of operation</returns>
        ''' <exception cref="FileAlreadyExistsException"><paramref name="TargetPath"/> file exists and <paramref name="ErrorCallBack"/> is null or it does not return <see cref="PathCopyCallbackResult.Retry"/> when called with <see cref="FileAlreadyExistsException"/>.</exception>
        ''' <exception cref="IO.IOException">An IO exception occured while openning source or target file or when reading source file or when writing target file and <paramref name="ErrorCallBack"/> is null or it returns <see cref="PathCopyCallbackResult.Abort"/> (or <see cref="PathCopyCallbackResult"/> non-mmeber) for the exception.</exception>
        ''' <exception cref="Security.SecurityException">A security exception oocured while openning source or target file and <paramref name="ErrorCallBack"/> is null or ire returns <see cref="PathCopyCallbackResult.Abort"/> (or <see cref="PathCopyCallbackResult"/> non-member) for the exception.</exception>
        ''' <exception cref="UnauthorizedAccessException">Access denied exception oocured while openning source or target file and <paramref name="ErrorCallBack"/> is null or ire returns <see cref="PathCopyCallbackResult.Abort"/> (or <see cref="PathCopyCallbackResult"/> non-member) for the exception.</exception>
        ''' <exception cref="OperationCanceledException"><paramref name="CallBack"/> returned <see cref="PathCopyCallbackResult.Abort"/></exception>
        ''' <exception cref="InvalidOperationException"><paramref name="CallBack"/> returned <see cref="PathCopyCallbackResult.Ignore"/> when its parameter SourceFileName was null.</exception>
        ''' <remarks>Directory for <paramref name="TargetPath"/> must exist. This function does not attempt to create it.
        ''' <para><paramref name="CallBack"/> function is first time called with file names and zero progress. All subsequent calls are with source and file name null (to recude string copying overhead). <paramref name="CallBack"/> can only return <see cref="PathCopyCallbackResult.Ignore"/> when called with source and target path non-null. This will cause file to be skiped and function to return true immediatelly.</para>
        ''' <para>It cannot be guaranted how often the callback function is called. Typically it is called for every 1024 bytes copyied. At leas it is called two times (with zero and 100% progress).</para>
        ''' <para>The copying proces is stream-based. Simply source file and target file streams are opened and read -> write is done by using <see cref="IO.FileStream.Read"/> and <see cref="IO.FileStream.Write"/> using 1024 bytes buffer. This means that no attributtes associated with the file are preserved for target file.</para></remarks>
        ''' <seelaso cref="Copy"/>
        Public Function FileCopy(ByVal SourcePath As Path, ByVal TargetPath As Path, ByVal CallBack As PathCopyCallBack, ByVal ErrorCallBack As PathCopyErrorCallBack) As FileCopyResult
            If Not SourcePath.IsFile Then Throw New IO.FileNotFoundException(ResourcesT.Exceptions.FileNotFound, SourcePath)
            Dim InFile As IO.FileStream
            'Open source file
OpenSource: Try
                InFile = IO.File.Open(SourcePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
            Catch ex As Exception When (TypeOf ex Is IO.IOException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is Security.SecurityException) AndAlso ErrorCallBack IsNot Nothing
                Select Case ErrorCallBack(SourcePath, TargetPath, PathCopyStages.OpenSourceFile, ex)
                    Case PathCopyCallbackResult.Cancel : Return FileCopyResult.PreCancelled Or FileCopyResult.Terminate
                    Case PathCopyCallbackResult.Ignore : Return FileCopyResult.PreCancelled
                    Case PathCopyCallbackResult.Retry : GoTo OpenSource
                    Case Else : Throw
                End Select
            End Try
            Try
                'Check if target exists
                If TargetPath.IsFile Then
                    Dim ex As FileAlreadyExistsException = New FileAlreadyExistsException(TargetPath)
                    If ErrorCallBack Is Nothing Then : Throw ex
                    Else
                        Select Case ErrorCallBack(SourcePath, TargetPath, PathCopyStages.CheckTagretFileExists, ex)
                            Case PathCopyCallbackResult.Ignore : Return FileCopyResult.PreCancelled
                            Case PathCopyCallbackResult.Cancel : Return FileCopyResult.PreCancelled Or FileCopyResult.Terminate
                            Case PathCopyCallbackResult.Retry 'Do nothing
                            Case Else : Throw ex
                        End Select
                    End If
                End If
                'Advertise copy beginning
                If CallBack IsNot Nothing Then
                    Select Case CallBack(SourcePath, InFile.Length, 0, TargetPath)
                        Case PathCopyCallbackResult.Cancel : Return FileCopyResult.PreCancelled Or FileCopyResult.Terminate
                        Case PathCopyCallbackResult.Retry : Return FileCopyResult.PreCancelled
                        Case PathCopyCallbackResult.Ignore 'do nothing
                        Case Else : Throw New OperationCanceledException(ResourcesT.Exceptions.OperationWasCancelled)
                    End Select
                End If
                Dim OutFile As IO.FileStream
                'Open target file
OpenTarget:     Try
                    OutFile = IO.File.Open(TargetPath, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)
                Catch ex As Exception When (TypeOf ex Is IO.IOException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is Security.SecurityException) AndAlso ErrorCallBack IsNot Nothing
                    Select Case ErrorCallBack(SourcePath, TargetPath, PathCopyStages.OpenTargetFile, ex)
                        Case PathCopyCallbackResult.Cancel : Return FileCopyResult.PreCancelled Or FileCopyResult.Terminate
                        Case PathCopyCallbackResult.Ignore : Return FileCopyResult.PreCancelled
                        Case PathCopyCallbackResult.Retry : GoTo OpenTarget
                        Case Else : Throw
                    End Select
                End Try
                Try
                    Dim Buffer(1024) As Byte
                    Dim bRead%
                    Dim Total As Long = 0
                    Do
                        'Read bytes
Read:                   Try
                            bRead = InFile.Read(Buffer, 0, Buffer.Length)
                        Catch ex As Exception When (TypeOf ex Is IO.IOException OrElse TypeOf ex Is ObjectDisposedException) AndAlso ErrorCallBack IsNot Nothing
                            Select Case ErrorCallBack.Invoke(SourcePath, TargetPath, PathCopyStages.Read, ex)
                                Case PathCopyCallbackResult.Cancel : Return FileCopyResult.Cancelled Or FileCopyResult.Terminate
                                Case PathCopyCallbackResult.Ignore : Return FileCopyResult.Cancelled
                                Case PathCopyCallbackResult.Retry : GoTo Read
                                Case Else : Throw
                            End Select
                        End Try
                        If bRead > 0 Then
                            'Write bytes
Write:                      Try
                                OutFile.Write(Buffer, 0, bRead)
                            Catch ex As Exception When (TypeOf ex Is IO.IOException OrElse TypeOf ex Is ObjectDisposedException) AndAlso ErrorCallBack IsNot Nothing
                                Select Case ErrorCallBack.Invoke(SourcePath, TargetPath, PathCopyStages.Write, ex)
                                    Case PathCopyCallbackResult.Cancel : Return FileCopyResult.Cancelled Or FileCopyResult.Terminate
                                    Case PathCopyCallbackResult.Ignore : Return FileCopyResult.Cancelled
                                    Case PathCopyCallbackResult.Retry : GoTo Write
                                    Case Else : Throw
                                End Select
                            End Try
                            Total += bRead
                            'Report progress
                            If CallBack IsNot Nothing Then
                                Select Case CallBack(Nothing, InFile.Length, Total, Nothing)
                                    Case PathCopyCallbackResult.Cancel : Return FileCopyResult.Cancelled Or FileCopyResult.Terminate
                                    Case PathCopyCallbackResult.Ignore 'Do nothing
                                    Case PathCopyCallbackResult.Retry : Throw New InvalidOperationException(ResourcesT.Exceptions.Retuned1.f("CallBack", "Retry"))
                                    Case Else : Throw New OperationCanceledException(ResourcesT.Exceptions.OperationWasCancelled)
                                End Select
                            End If
                        End If
                    Loop While bRead > 0
                Finally
                    OutFile.Close()
                End Try
            Finally
                InFile.Close()
            End Try
            Return FileCopyResult.Copyed
        End Function
    End Module
    ''' <summary>Provides callback function for file/directory copy operation</summary>
    ''' <param name="SourceFileName">Full path of source file currently being copyied.
    ''' <note>This argument is usually null. It is non-unly only when callback function is called for the first tame for single file. Subsequent calls for the same file has this argument null.</note></param>
    ''' <param name="TargetFileName">Full path of targte file <paramref name="SourceFileName"/> is being copied to.
    ''' <note>Null when <paramref name="SourceFileName"/> is null.</note></param>
    ''' <param name="TotalSize">Total size, in bytes, of source file</param>
    ''' <param name="BytesCopyed">Number of bytes already succesfully copied from <paramref name="SourceFileName"/> to <paramref name="TargetFileName"/>.</param>
    ''' <returns>Value indicating how to continue the operation. By default this delegate should return <see cref="PathCopyCallbackResult.Ignore"/> (which meants that operation will continue). This delegate shall not return <see cref="PathCopyCallbackResult.Retry"/> unless <paramref name="SourceFileName"/> is not null.</returns>
    ''' <remarks>When <paramref name="SourceFileName"/> is not null <paramref name="BytesCopyed"/> is always zero. When callback function returns <see cref="PathCopyCallbackResult.Retry"/> in such situation, copying of file <paramref name="SourceFileName"/> is skipped.
    ''' <para>When callback function returns <see cref="PathCopyCallbackResult.Cancel"/> or <see cref="PathCopyCallbackResult.Abort"/> no cleenup is done (only all opened files are closed). Half-copyied target files is leftin file system. You should delete the file.</para></remarks>
    Public Delegate Function PathCopyCallBack(ByVal SourceFileName As String, ByVal TotalSize As Long, ByVal BytesCopyed As Long, ByVal TargetFileName As String) As PathCopyCallbackResult
    ''' <summary>Used to provide custom error handling during file/directory copy operation</summary>
    ''' <param name="SourceFileName">Full path of source file or directory currently being copyied</param>
    ''' <param name="TargetFileName">Full path of target file or directory - target of <paramref name="SourceFileName"/></param>
    ''' <param name="Stage">Identifies process stage where exception have occured</param>
    ''' <param name="Exception">The exception.<para>When <paramref name="Exception"/> is <see cref="FileAlreadyExistsException"/> return <see cref="PathCopyCallbackResult.Retry"/> to overwrite the file.</para></param>
    ''' <returns>A value indicating how to recover from the error</returns>
    ''' <remarks>When copy operation fails during source-read -> target-write process, no cleenup is done (but all opened files are closed). Caller you should delete half-copyied target file.</remarks>
    Public Delegate Function PathCopyErrorCallBack(ByVal SourceFileName$, ByVal TargetFileName$, ByVal Stage As PathCopyStages, ByVal Exception As Exception) As PathCopyCallbackResult
    ''' <summary>Ways of solving path copy errors</summary>
    Public Enum PathCopyCallbackResult
        ''' <summary>Siletnly cancel the operation</summary>
        Cancel
        ''' <summary>Aborth the operation. Exception will be thrown.</summary>
        Abort
        ''' <summary>Try failed sub-operation again</summary>
        Retry
        ''' <summary>Ignore failure and procede to next file/directory</summary>
        Ignore
    End Enum
    ''' <summary>Stages of file/directory copy process when exception can occure</summary>
    Public Enum PathCopyStages
        ''' <summary>Target directory (or one of its subdirectories required for subdirectories of source directory) does not exist and it is being created</summary>
        ''' <remarks>Not used by <see cref="FileCopy"/></remarks>
        CreateDirectory
        ''' <summary>Source file is being opened</summary>
        OpenSourceFile
        ''' <summary>Check if tagret file exists</summary>
        CheckTagretFileExists
        ''' <summary>tagrte file is being opened</summary>
        OpenTargetFile
        ''' <summary>Reading bytes from source file</summary>
        Read
        ''' <summary>Writing bytes to tagret file</summary>
        Write
        ''' <summary>File attributes or times are being set</summary>
        ''' <remarks>Not used by <see cref="FileCopy"/></remarks>
        SetFileAttributes
        ''' <summary>Directory attributes or times are being set</summary>
        ''' <remarks>Not used by <see cref="FileCopy"/></remarks>
        SetDirectoryAttributes
    End Enum
    ''' <summary>Excpetion throw when target path is already used by a file</summary>
    Public Class FileAlreadyExistsException : Inherits IO.IOException
        ''' <summary>CTfor from path</summary>
        ''' <param name="Path">Path to existent target file</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Path"/> is null.</exception>
        Public Sub New(ByVal Path As Path)
            Me.New(ResourcesT.Exceptions.File0AlreadyExists.f(Path.ThrowIfNull("Path").Path))
        End Sub
        ''' <summary>CTor form message and optionally inner exception</summary>
        ''' <param name="Message">The error message that explains the reason for the exception.</param>
        ''' <param name="InnerException">The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
        Public Sub New(ByVal Message As String, Optional ByVal InnerException As Boolean = Nothing)
            MyBase.New(Message, InnerException)
        End Sub
    End Class
    ''' <summary>Result of file copy operation</summary>
    <Flags()> _
    Public Enum FileCopyResult
        ''' <summary>File was copied</summary>
        Copyed = 1
        ''' <summary>Copying was cancelled before it started</summary>
        PreCancelled = 3
        ''' <summary>Copying was cancelled after it started</summary>
        Cancelled = 2
        ''' <summary>If this falg is set, the whole batch copying operation was cancelled by callback function and should not continue. This is never combined with <see cref="Copyed"/></summary>
        Terminate = 1024
    End Enum
End Namespace
#End If