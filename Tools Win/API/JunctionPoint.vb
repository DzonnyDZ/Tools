Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports Microsoft.Win32.SafeHandles


Namespace API
    'https://www.codeproject.com/Articles/15633/Manipulating-NTFS-Junction-Points-in-NET
    ''' <summary>Provides access to NTFS junction points in .Net.</summary>
    Friend Module JunctionPoint
        ''' <summary>The file Or directory Is Not a reparse point.</summary>
        Private Const ERROR_NOT_A_REPARSE_POINT% = 4390

        ''' <summary>The reparse point attribute cannot be set because it conflicts with an existing attribute.</summary>
        Private Const ERROR_REPARSE_ATTRIBUTE_CONFLICT% = 4391

        ''' <summary>The data present in the reparse point buffer Is invalid.</summary>
        Private Const ERROR_INVALID_REPARSE_DATA% = 4392

        ''' <summary>The tag present in the reparse point buffer Is invalid.</summary>
        Private Const ERROR_REPARSE_TAG_INVALID% = 4393

        ''' <summary>There Is a mismatch between the tag specified in the request And the tag present in the reparse point.</summary>
        Private Const ERROR_REPARSE_TAG_MISMATCH% = 4394

        ''' <summary>Command to set the reparse point data block.</summary>
        Private Const FSCTL_SET_REPARSE_POINT% = &H900A4

        ''' <summary>Command to get the reparse point data block.</summary>
        Private Const FSCTL_GET_REPARSE_POINT% = &H900A8

        ''' <summary>Command to delete the reparse point data base.</summary>
        Private Const FSCTL_DELETE_REPARSE_POINT% = &H900AC

        ''' <summary>Reparse point tag used to identify mount points And junction points.</summary>
        Private Const IO_REPARSE_TAG_MOUNT_POINT As UInteger = &HA0000003UI

        ''' <summary>This prefix indicates to NTFS that the path Is to be treated as a non-interpreted path in the virtual file system.</summary>
        Private Const NonInterpretedPathPrefix$ = "\??\"

        <Flags>
        Private Enum EFileAccess As UInteger
            GenericRead = &H80000000UI
            GenericWrite = &H40000000
            GenericExecute = &H20000000
            GenericAll = &H10000000
        End Enum

        <Flags>
        Private Enum EFileShare As UInteger
            None = &H0
            Read = &H1
            Write = &H2
            Delete = &H4
        End Enum

        Private Enum ECreationDisposition As UInteger
            [New] = 1
            CreateAlways = 2
            OpenExisting = 3
            OpenAlways = 4
            TruncateExisting = 5
        End Enum

        <Flags>
        Private Enum EFileAttributes As UInteger
            [ReadOnly] = &H1
            Hidden = &H2
            System = &H4
            Directory = &H10
            Archive = &H20
            Device = &H40
            Normal = &H80
            Temporary = &H100
            SparseFile = &H200
            ReparsePoint = &H400
            Compressed = &H800
            Offline = &H1000
            NotContentIndexed = &H2000
            Encrypted = &H4000
            Write_Through = &H80000000UI
            Overlapped = &H40000000
            NoBuffering = &H20000000
            RandomAccess = &H10000000
            SequentialScan = &H8000000
            DeleteOnClose = &H4000000
            BackupSemantics = &H2000000
            PosixSemantics = &H1000000
            OpenReparsePoint = &H200000
            OpenNoRecall = &H100000
            FirstPipeInstance = &H80000
        End Enum

        <StructLayout(LayoutKind.Sequential)>
        Private Structure REPARSE_DATA_BUFFER
            ''' <summary>Reparse point tag. Must be a Microsoft reparse point tag.</summary>
            Public ReparseTag As UInteger

            ''' <summary>
            ''' Size, in bytes, of the data after the Reserved member. This can be calculated by:
            ''' (4 * sizeof(ushort)) + SubstituteNameLength + PrintNameLength + 
            ''' (namesAreNullTerminated ? 2 * sizeof(char) : 0)
            ''' </summary>
            Public ReparseDataLength As UShort

            ''' <summary>Reserved do Not use. </summary>
            Public Reserved As UShort

            ''' <summary>Offset, in bytes, of the substitute name string in the PathBuffer array.</summary>
            Public SubstituteNameOffset As UShort

            ''' <summary>
            ''' Length, in bytes, of the substitute name string. If this string Is null-terminated,
            ''' SubstituteNameLength does Not include space for the null character.
            ''' </summary>
            Public SubstituteNameLength As UShort

            ''' <summary>Offset, in bytes, of the print name string in the PathBuffer array.</summary>
            Public PrintNameOffset As UShort

            ''' <summary>
            ''' Length, in bytes, of the print name string. If this string Is null-terminated,
            ''' PrintNameLength does Not include space for the null character. 
            ''' </summary>
            Public PrintNameLength As UShort

            ''' <summary>
            ''' A buffer containing the Unicode-encoded path string. The path string contains
            ''' the substitute name string And print name string.
            ''' </summary>
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H3FF0)>
            Public PathBuffer As Byte()
        End Structure

        Private Declare Auto Function DeviceIoControl Lib "kernel32.dll" (hDevice As IntPtr, dwIoControlCode As UInteger, InBuffer As IntPtr, nInBufferSize%, OutBuffer As IntPtr, nOutBufferSize%, <Out> ByRef pBytesReturned%, lpOverlapped As IntPtr) As Boolean

        Private Declare Function CreateFile Lib "kernel32.dll" (lpFileName$, dwDesiredAccess As EFileAccess, dwShareMode As EFileShare, lpSecurityAttributes As IntPtr, dwCreationDisposition As ECreationDisposition, dwFlagsAndAttributes As EFileAttributes, hTemplateFile As IntPtr) As IntPtr


        ''' <summary>Creates a junction point from the specified directory to the specified target directory.</summary>
        ''' <param name="junctionPoint">The junction point path</param>
        ''' <param name="targetDir">The target directory</param>
        ''' <param name="overwrite">If true overwrites an existing reparse point Or empty directory</param>
        ''' <exception cref="IOException">Thrown when the junction point could Not be created Or when an existing directory was found And <paramref name="overwrite" /> if false</exception>
        Public Sub Create(junctionPoint$, targetDir$, overwrite As Boolean)
            targetDir = Path.GetFullPath(targetDir)

            If Not Directory.Exists(targetDir) Then Throw New IOException("Target path does not exist or is not a directory.")

            If (Directory.Exists(junctionPoint)) Then
                If Not overwrite Then Throw New IOException("Directory already exists and overwrite parameter is false.")
            Else
                Directory.CreateDirectory(junctionPoint)
            End If

            Using handle As SafeFileHandle = OpenReparsePoint(junctionPoint, EFileAccess.GenericWrite)
                Dim targetDirBytes As Byte() = Encoding.Unicode.GetBytes(NonInterpretedPathPrefix + Path.GetFullPath(targetDir))

                Dim reparseDataBuffer As New REPARSE_DATA_BUFFER()

                reparseDataBuffer.ReparseTag = IO_REPARSE_TAG_MOUNT_POINT
                reparseDataBuffer.ReparseDataLength = CUShort(targetDirBytes.Length + 12)
                reparseDataBuffer.SubstituteNameOffset = 0
                reparseDataBuffer.SubstituteNameLength = CUShort(targetDirBytes.Length)
                reparseDataBuffer.PrintNameOffset = CUShort(targetDirBytes.Length + 2)
                reparseDataBuffer.PrintNameLength = 0
                ReDim reparseDataBuffer.PathBuffer(&H3FF0)
                Array.Copy(targetDirBytes, reparseDataBuffer.PathBuffer, targetDirBytes.Length)

                Dim inBufferSize% = Marshal.SizeOf(reparseDataBuffer)
                Dim inBuffer As IntPtr = Marshal.AllocHGlobal(inBufferSize)

                Try
                    Marshal.StructureToPtr(reparseDataBuffer, inBuffer, False)

                    Dim bytesReturned%
                    Dim result As Boolean = DeviceIoControl(handle.DangerousGetHandle(), FSCTL_SET_REPARSE_POINT, inBuffer, targetDirBytes.Length + 20, IntPtr.Zero, 0, bytesReturned, IntPtr.Zero)

                    If Not result Then ThrowLastWin32Error("Unable to create junction point.")
                Finally
                    Marshal.FreeHGlobal(inBuffer)
                End Try
            End Using
        End Sub

        ''' <summary>Deletes a junction point at the specified source directory along with the directory itself.</summary>
        ''' <remarks>Does nothing if the junction point does Not exist.</remarks>
        ''' <param name="junctionPoint">The junction point path</param>
        Public Sub Delete(junctionPoint$)
            If Not Directory.Exists(junctionPoint) Then
                If File.Exists(junctionPoint) Then Throw New IOException("Path is not a junction point.")
                Return
            End If

            Using handle As SafeFileHandle = OpenReparsePoint(junctionPoint, EFileAccess.GenericWrite)
                Dim reparseDataBuffer = New REPARSE_DATA_BUFFER()

                reparseDataBuffer.ReparseTag = IO_REPARSE_TAG_MOUNT_POINT
                reparseDataBuffer.ReparseDataLength = 0
                ReDim reparseDataBuffer.PathBuffer(&H3FF0)

                Dim inBufferSize% = Marshal.SizeOf(reparseDataBuffer)
                Dim inBuffer As IntPtr = Marshal.AllocHGlobal(inBufferSize)
                Try
                    Marshal.StructureToPtr(reparseDataBuffer, inBuffer, False)

                    Dim bytesReturned%
                    Dim result As Boolean = DeviceIoControl(handle.DangerousGetHandle(), FSCTL_DELETE_REPARSE_POINT, inBuffer, 8, IntPtr.Zero, 0, bytesReturned, IntPtr.Zero)

                    If Not result Then ThrowLastWin32Error("Unable to delete junction point.")
                Finally
                    Marshal.FreeHGlobal(inBuffer)
                End Try

                Try
                    Directory.Delete(junctionPoint)
                Catch ex As IOException
                    Throw New IOException("Unable to delete junction point.", ex)
                End Try
            End Using
        End Sub

        ''' <summary>Determines whether the specified path exists And refers to a junction point.</summary>
        ''' <param name="path">The junction point path</param>
        ''' <returns>True if the specified path represents a junction point</returns>
        ''' <exception cref="IOException">Thrown if the specified path Is invalid Or some other error occurs</exception>
        Public Function Exists(path$) As Boolean
            If Not Directory.Exists(path) Then Return False

            Using handle As SafeFileHandle = OpenReparsePoint(path, EFileAccess.GenericRead)
                Dim target$ = InternalGetTarget(handle)
                Return target <> NULL
            End Using
        End Function

        ''' <summary>Gets the target of the specified junction point.</summary>
        ''' <param name="junctionPoint">The junction point path</param>
        ''' <returns>The target of the junction point</returns>
        ''' <exception cref="IOException">Thrown when the specified path does Not exist, Is invalid, Is Not a junction point, Or some other error occurs</exception>
        Public Function GetTarget$(junctionPoint$)
            Using handle As SafeFileHandle = OpenReparsePoint(junctionPoint, EFileAccess.GenericRead)
                Dim target$ = InternalGetTarget(handle)
                If target = NULL Then Throw New IOException("Path is not a junction point.")

                Return target
            End Using
        End Function

        Private Function InternalGetTarget$(handle As SafeFileHandle)
            Dim outBufferSize% = Marshal.SizeOf(GetType(REPARSE_DATA_BUFFER))
            Dim outBuffer As IntPtr = Marshal.AllocHGlobal(outBufferSize)

            Try
                Dim bytesReturned%
                Dim result As Boolean = DeviceIoControl(handle.DangerousGetHandle(), FSCTL_GET_REPARSE_POINT, IntPtr.Zero, 0, outBuffer, outBufferSize, bytesReturned, IntPtr.Zero)

                If Not result Then
                    Dim [error] As Integer = Marshal.GetLastWin32Error()
                    If [error] = ERROR_NOT_A_REPARSE_POINT Then Return NULL

                    ThrowLastWin32Error("Unable to get information about junction point.")
                End If

                Dim reparseDataBuffer As REPARSE_DATA_BUFFER = Marshal.PtrToStructure(outBuffer, GetType(REPARSE_DATA_BUFFER))

                If reparseDataBuffer.ReparseTag <> IO_REPARSE_TAG_MOUNT_POINT Then Return NULL

                Dim targetDir$ = Encoding.Unicode.GetString(reparseDataBuffer.PathBuffer, reparseDataBuffer.SubstituteNameOffset, reparseDataBuffer.SubstituteNameLength)

                If targetDir.StartsWith(NonInterpretedPathPrefix) Then targetDir = targetDir.Substring(NonInterpretedPathPrefix.Length)

                Return targetDir
            Finally
                Marshal.FreeHGlobal(outBuffer)
            End Try
        End Function

        Private Function OpenReparsePoint(reparsePoint$, accessMode As EFileAccess) As SafeFileHandle
            Dim reparsePointHandle = New SafeFileHandle(CreateFile(reparsePoint, accessMode, EFileShare.Read Or EFileShare.Write Or EFileShare.Delete, IntPtr.Zero, ECreationDisposition.OpenExisting, EFileAttributes.BackupSemantics Or EFileAttributes.OpenReparsePoint, IntPtr.Zero), True)

            If Marshal.GetLastWin32Error() <> 0 Then ThrowLastWin32Error("Unable to open reparse point.")

            Return reparsePointHandle
        End Function

        Private Sub ThrowLastWin32Error(message$)
            Throw New IOException(message, Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()))
        End Sub
    End Module

End Namespace