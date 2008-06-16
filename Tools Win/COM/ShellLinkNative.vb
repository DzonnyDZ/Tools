' Author:       Mattias Sjögren (mattias@mvps.org)
'               http://www.msjogren.net/dotnet/
' Copyright ©2001-2002, Mattias Sjögren
Imports System
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports Tools.COM.ShellLink
Imports Tools.API.FileSystem
Imports System.ComponentModel

Namespace COM.ShellLink

    ''' <summary><see cref="IShellLinkW.Resolve"/> flags</summary>
    <Flags()> _
    Friend Enum SLR_FLAGS
        ''' <summary>Do not display a dialog box if the link cannot be resolved. When <see cref="SLR_NO_UI"/> is set, the high-order word of fFlags can be set to a time-out value that specifies the maximum amount of time to be spent resolving the link. The function returns if the link cannot be resolved within the time-out duration. If the high-order word is set to zero, the time-out duration will be set to the default value of 3,000 milliseconds (3 seconds). To specify a value, set the high word of fFlags to the desired time-out duration, in milliseconds.</summary>
        SLR_NO_UI = &H1
        ''' <summary><see cref="SLR_ANY_MATCH"/></summary>
        SLR_ANY_MATCH = &H2
        ''' <summary>If the link object has changed, update its path and list of identifiers. If <see cref="SLR_UPDATE"/> is set, you do not need to call <see cref="IPersistFile.IsDirty"/> to determine whether or not the link object has changed.</summary>
        SLR_UPDATE = &H4
        ''' <summary>Do not update the link information.</summary>
        SLR_NOUPDATE = &H8
        ''' <summary>Do not execute the search heuristics.</summary>
        SLR_NOSEARCH = &H10
        ''' <summary>Do not use distributed link tracking.</summary>
        SLR_NOTRACK = &H20
        ''' <summary>Disable distributed link tracking. By default, distributed link tracking tracks removable media across multiple devices based on the volume name. It also uses the Universal Naming Convention (UNC) path to track remote file systems whose drive letter has changed. Setting <see cref="SLR_NOLINKINFO"/> disables both types of tracking.</summary>
        SLR_NOLINKINFO = &H40
        ''' <summary>Call the Microsoft Windows Installer.</summary>
        SLR_INVOKE_MSI = &H80
    End Enum

    ''' <summary><see cref="IShellLinkW.GetPath"/> flags</summary>
    <Flags()> _
    Friend Enum SLGP_FLAGS
        ''' <summary>Retrieves the standard short (8.3 format) file name.</summary>
        SLGP_SHORTPATH = &H1
        ''' <summary>Retrieves the Universal Naming Convention (UNC) path name of the file.</summary>
        SLGP_UNCPRIORITY = &H2
        ''' <summary>Retrieves the raw path name. A raw path is something that might not exist and may include environment variables that need to be expanded.</summary>
        SLGP_RAWPATH = &H4
    End Enum

    ''' <summary>Contains information about the file</summary>
    <StructLayoutAttribute(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Friend Structure WIN32_FIND_DATAW
        ''' <summary>The file attributes of a file.</summary>
        Public dwFileAttributes As Integer
        ''' <summary>A <see cref="ComTypes.FILETIME"/> structure that specifies when a file or directory was created. If the underlying file system does not support creation time, this member is zero (0).</summary>
        Public ftCreationTime As ComTypes.FILETIME
        ''' <summary>A <see cref="ComTypes.FILETIME"/> structure. For a file, the structure specifies when the file was last read from, written to, or for executable files, run. For a directory, the structure specifies when the directory is created. If the underlying file system does not support last access time, this member is zero (0). On the FAT file system, the specified date for both files and directories is correct, but the time of day is always set to midnight.</summary>
        Public ftLastAccessTime As ComTypes.FILETIME
        ''' <summary>A <see cref="ComTypes.FILETIME"/> structure. For a file, the structure specifies when the file was last written to, truncated, or overwritten, for example, when WriteFile or SetEndOfFile are used. The date and time are not updated when file attributes or security descriptors are changed. For a directory, the structure specifies when the directory is created. If the underlying file system does not support last write time, this member is zero (0).</summary>
        Public ftLastWriteTime As ComTypes.FILETIME
        ''' <summary>The high-order DWORD value of the file size, in bytes. This value is zero (0) unless the file size is greater than MAXDWORD. The size of the file is equal to (<see cref="nFileSizeHigh">nFileSizeHigh</see> * (MAXDWORD+1)) + <see cref="nFileSizeLow">nFileSizeLow</see>.</summary>
        Public nFileSizeHigh As Integer
        ''' <summary></summary>
        Public nFileSizeLow As Integer
        ''' <summary>If the dwFileAttributes member includes the FILE_ATTRIBUTE_REPARSE_POINT attribute, this member specifies the reparse point tag. Otherwise, this value is undefined and should not be used.</summary>
        Public dwReserved0 As Integer
        ''' <summary>Reserved for future use.</summary>
        Public dwReserved1 As Integer
        ''' <summary>The name of the file.</summary>
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=API.MAX_PATH)> Public cFileName As String
        ''' <summary>    An alternative name for the file. This name is in the classic 8.3 (filename.ext) file name format.</summary>
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=14)> Public cAlternateFileName As String
    End Structure

    ''' <summary>The <see cref="IPersistFile"/> interface provides methods that permit an object to be loaded from or saved to a disk file, rather than a storage object or stream. Because the information needed to open a file varies greatly from one application to another, the implementation of <see cref="IPersistFile.Load"/> on the object must also open its disk file.</summary>
    <ComImport(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000010B-0000-0000-C000-000000000046")> _
    Friend Interface IPersistFile

#Region "Methods inherited from IPersist"

        ''' <summary>Retrieves the class identifier (CLSID) of an object. The CLSID is a unique value that identifies the code that can manipulate the persistent data.</summary>
        ''' <param name="pClassID">[out] Points to the location of the CLSID on return. The CLSID is a globally unique identifier (GUID) that uniquely represents an object class that defines the code that can manipulate the object's data.</param>
        Sub GetClassID( _
          <Out()> ByRef pClassID As Guid)

#End Region

        ''' <summary>Checks an object for changes since it was last saved to its current file.</summary>
        ''' <returns>Non-zero if the object has changed since it was last saved. Zero if the object has not changed since the last save.</returns>
        <PreserveSig()> _
        Function IsDirty() As Integer

        ''' <summary>Opens the specified file and initializes an object from the file contents.</summary>
        ''' <param name="pszFileName">[in] Points to a zero-terminated string containing the absolute path of the file to open.</param>
        ''' <param name="dwMode">[in] Specifies some combination of the values from the STGM enumeration to indicate the access mode to use when opening the file. IPersistFile::Load can treat this value as a suggestion, adding more restrictive permissions if necessary. If dwMode is zero, the implementation should open the file using whatever default permissions are used when a user opens the file.</param>
        Sub Load( _
          <MarshalAs(UnmanagedType.LPWStr)> ByVal pszFileName As String, _
          ByVal dwMode As Integer)

        ''' <summary>Saves a copy of the object into the specified file.</summary>
        ''' <param name="pszFileName">[in] Points to a zero-terminated string containing the absolute path of the file to which the object should be saved. If pszFileName is NULL, the object should save its data to the current file, if there is one.</param>
        ''' <param name="fRemember">[in] Indicates whether the pszFileName parameter is to be used as the current working file. If TRUE, pszFileName becomes the current file and the object should clear its dirty flag after the save. If FALSE, this save operation is a "Save A Copy As ..." operation. In this case, the current file is unchanged and the object should not clear its dirty flag. If pszFileName is NULL, the implementation should ignore the fRemember flag.</param>
        Sub Save( _
          <MarshalAs(UnmanagedType.LPWStr)> ByVal pszFileName As String, _
          <MarshalAs(UnmanagedType.Bool)> ByVal fRemember As Boolean)

        ''' <summary>Notifies the object that it can write to its file. It does this by notifying the object that it can revert from NoScribble mode (in which it must not write to its file), to Normal mode (in which it can). The component enters NoScribble mode when it receives an <see cref="IPersistFile.Save"/> call.</summary>
        ''' <param name="pszFileName">[in] Points to the absolute path of the file where the object was previously saved.</param>
        Sub SaveCompleted( _
          <MarshalAs(UnmanagedType.LPWStr)> ByVal pszFileName As String)

        ''' <summary>Retrieves either the absolute path to the object's current working file or, if there is no current working file, the object's default filename prompt.</summary>        ''' <param name="ppszFileName">[out] Points to the location of a pointer to a zero-terminated string containing the path for the current file or the default filename prompt (such as *.txt). If an error occurs, ppszFileName is set to NULL.</param>
        Sub GetCurFile( _
          ByRef ppszFileName As IntPtr)

    End Interface

    ''' <summary>Exposes methods that create, modify, and resolve Shell links. UNICODE Version</summary>
    ''' <remarks>Note  This interface cannot be used to create a link to a URL.</remarks>
    <ComImport(), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
    <Guid("000214F9-0000-0000-C000-000000000046")> _
    Friend Interface IShellLinkW
        ''' <summary>Gets the path and file name of a Shell link object.</summary>
        ''' <param name="pszFile">The address of a buffer that receives the path and file name of the Shell link object.</param>
        ''' <param name="cchMaxPath">The maximum number of characters to copy to the buffer pointed to by the pszFile parameter.</param>
        ''' <param name="pfd">The address of a <see cref="WIN32_FIND_DATAW"/> structure that receives information about the Shell link object. If this parameter is NULL, then no additional information is returned.</param>
        ''' <param name="fFlags">Flags that specify the type of path information to retrieve.</param>
        Sub GetPath( _
          <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszFile As StringBuilder, _
          ByVal cchMaxPath As Integer, _
          <Out()> ByRef pfd As WIN32_FIND_DATAW, _
          ByVal fFlags As SLGP_FLAGS)

        ''' <summary>Gets the list of item identifiers for a Shell link object.</summary>
        ''' <param name="ppidl">[out] When this method returns, contains the address of a pointer to an item identifier list (PIDL).</param>
        Sub GetIDList( _
          ByRef ppidl As IntPtr)

        ''' <summary>Sets the pointer to an item identifier list (PIDL) for a Shell link object.</summary>
        ''' <param name="pidl">[in] The object's fully-qualified PIDL.</param>
        Sub SetIDList( _
          ByVal pidl As IntPtr)

        ''' <summary>Gets the description string for a Shell link object.</summary>
        ''' <param name="pszName">A pointer to the buffer that receives the description string.</param>
        ''' <param name="cchMaxName">The maximum number of characters to copy to the buffer pointed to by the pszName parameter.</param>
        Sub GetDescription( _
          <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As StringBuilder, _
          ByVal cchMaxName As Integer)

        ''' <summary>Sets the description for a Shell link object. The description can be any application-defined string.</summary>
        ''' <param name="pszName">A pointer to a buffer containing the new description string.</param>
        Sub SetDescription( _
          <MarshalAs(UnmanagedType.LPWStr)> ByVal pszName As String)

        ''' <summary>Gets the name of the working directory for a Shell link object.</summary>
        ''' <param name="pszDir">The address of a buffer that receives the name of the working directory.</param>
        ''' <param name="cchMaxPath">The maximum number of characters to copy to the buffer pointed to by the pszDir parameter. The name of the working directory is truncated if it is longer than the maximum specified by this parameter.</param>
        Sub GetWorkingDirectory( _
          <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszDir As StringBuilder, _
          ByVal cchMaxPath As Integer)

        ''' <summary>Sets the name of the working directory for a Shell link object.</summary>
        ''' <param name="pszDir">The address of a buffer that contains the name of the new working directory.</param>
        ''' <remarks>The working directory is optional unless the target requires a working directory. For example, if an application creates a Shell link to a Microsoft Word document that uses a template residing in a different directory, the application would use this method to set the working directory.</remarks>
        Sub SetWorkingDirectory( _
          <MarshalAs(UnmanagedType.LPWStr)> ByVal pszDir As String)


        ''' <summary>Note  This interface cannot be used to create a link to a URL.</summary>
        ''' <param name="pszArgs">A pointer to the buffer that receives the command-line arguments.</param>
        ''' <param name="cchMaxPath">The maximum number of characters that can be copied to the buffer supplied by the pszArgs parameter.</param>
        Sub GetArguments( _
          <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszArgs As StringBuilder, _
          ByVal cchMaxPath As Integer)

        ''' <summary>Sets the command-line arguments for a Shell link object.</summary>
        ''' <param name="pszArgs">A pointer to a buffer that contains the new command-line arguments.</param>
        Sub SetArguments( _
          <MarshalAs(UnmanagedType.LPWStr)> ByVal pszArgs As String)

        ''' <summary>Gets the hot key for a Shell link object.</summary>
        ''' <param name="pwHotkey">The address of the hot key. The virtual key code is in the low-order byte, and the modifier flags are in the high-order byte.</param>
        Sub GetHotkey( _
          ByRef pwHotkey As Short)

        ''' <summary>Sets a hot key for a Shell link object.</summary>
        ''' <param name="wHotkey">The new hot key. The virtual key code is in the low-order byte, and the modifier flags are in the high-order byte. The modifier flags can be a combination of the values specified in the description of the <see cref="IShellLinkW.GetHotkey"/> method.</param>
        Sub SetHotkey( _
          ByVal wHotkey As Short)

        ''' <summary>Gets the show command for a Shell link object.</summary>
        ''' <param name="piShowCmd">A pointer to the command.</param>
        ''' <remarks>The show command is used to set the initial show state of the corresponding object.</remarks>
        Sub GetShowCmd( _
          ByRef piShowCmd As Integer)

        ''' <summary>Sets the show command for a Shell link object. The show command sets the initial show state of the window.</summary>
        ''' <param name="iShowCmd">Command</param>
        Sub SetShowCmd( _
          ByVal iShowCmd As Integer)

        ''' <summary>Gets the location (path and index) of the icon for a Shell link object.</summary>
        ''' <param name="pszIconPath">The address of a buffer that receives the path of the file containing the icon.</param>
        ''' <param name="cchIconPath">The maximum number of characters to copy to the buffer pointed to by the pszIconPath parameter.</param>
        ''' <param name="piIcon">The address of a value that receives the index of the icon.</param>
        Sub GetIconLocation( _
          <Out(), MarshalAs(UnmanagedType.LPWStr)> ByVal pszIconPath As StringBuilder, _
          ByVal cchIconPath As Integer, _
          ByRef piIcon As Integer)

        ''' <summary>Sets the location (path and index) of the icon for a Shell link object.</summary>
        ''' <param name="pszIconPath">The address of a buffer to contain the path of the file containing the icon.</param>
        ''' <param name="iIcon">The index of the icon.</param>
        Sub SetIconLocation( _
          <MarshalAs(UnmanagedType.LPWStr)> ByVal pszIconPath As String, _
          ByVal iIcon As Integer)

        ''' <summary>Sets the relative path to the Shell link object.</summary>
        ''' <param name="pszPathRel">The address of a buffer that contains the new relative path. It should be a file name, not a folder name.</param>
        ''' <param name="dwReserved">Reserved. Set this parameter to zero.</param>
        ''' <remarks>Clients commonly define a relative link when it may be moved along with its target, causing the absolute path to become invalid. The SetRelativePath method can be used to help the link resolution process find its target based on a common path prefix between the target and the relative path. To assist in the resolution process, clients should set the relative path as part of the link creation process.</remarks>
        Sub SetRelativePath( _
          <MarshalAs(UnmanagedType.LPWStr)> ByVal pszPathRel As String, _
          ByVal dwReserved As Integer)

        ''' <summary>Attempts to find the target of a Shell link, even if it has been moved or renamed.</summary>
        ''' <param name="hwnd">A handle to the window that the Shell will use as the parent for a dialog box. The Shell displays the dialog box if it needs to prompt the user for more information while resolving a Shell link.</param>
        ''' <param name="fFlags">Action flags.</param>
        Sub Resolve( _
          ByVal hwnd As IntPtr, _
          ByVal fFlags As SLR_FLAGS)

        ''' <summary>Sets the path and file name of a Shell link object.</summary>
        ''' <param name="pszFile">The address of a buffer that contains the new path.</param>
        Sub SetPath( _
          <MarshalAs(UnmanagedType.LPWStr)> ByVal pszFile As String)

    End Interface
    ''' <summary>COM-imported class which implements the <see cref="IShellLinkW"/> interface</summary>
    <ComImport()> _
    <Guid("00021401-0000-0000-C000-000000000046")> _
    Friend NotInheritable Class ShellLink
        Implements IShellLinkW, IPersistFile
        ''' <summary>Implements <see cref="IPersistFile.GetClassID"/></summary>
        Private Sub GetClassID(ByRef pClassID As System.Guid) Implements IPersistFile.GetClassID
        End Sub
        ''' <summary>Implements <see cref="IPersistFile.GetCurFile"/></summary>
        Private Sub GetCurFile(ByRef ppszFileName As System.IntPtr) Implements IPersistFile.GetCurFile
        End Sub
        Private Function IsDirty() As Integer Implements IPersistFile.IsDirty
        End Function
        ''' <summary>Implements <see cref="IPersistFile.Load"/></summary>
        Private Sub Load(ByVal pszFileName As String, ByVal dwMode As Integer) Implements IPersistFile.Load
        End Sub
        ''' <summary>Implements <see cref="IPersistFile.Save"/></summary>
        Private Sub Save(ByVal pszFileName As String, ByVal fRemember As Boolean) Implements IPersistFile.Save
        End Sub
        ''' <summary>Implements <see cref="IPersistFile.SaveCompleted"/></summary>
        Private Sub SaveCompleted(ByVal pszFileName As String) Implements IPersistFile.SaveCompleted
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.GetArguments"/></summary>
        Private Sub GetArguments(ByVal pszArgs As System.Text.StringBuilder, ByVal cchMaxPath As Integer) Implements IShellLinkW.GetArguments
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.GetDescription"/></summary>
        Private Sub GetDescription(ByVal pszName As System.Text.StringBuilder, ByVal cchMaxName As Integer) Implements IShellLinkW.GetDescription
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.GetHotkey"/></summary>
        Private Sub GetHotkey(ByRef pwHotkey As Short) Implements IShellLinkW.GetHotkey
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.GetIconLocation"/></summary>
        Private Sub GetIconLocation(ByVal pszIconPath As System.Text.StringBuilder, ByVal cchIconPath As Integer, ByRef piIcon As Integer) Implements IShellLinkW.GetIconLocation
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.GetIDList"/></summary>
        Private Sub GetIDList(ByRef ppidl As System.IntPtr) Implements IShellLinkW.GetIDList
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.GetPath"/></summary>
        Private Sub GetPath(ByVal pszFile As System.Text.StringBuilder, ByVal cchMaxPath As Integer, ByRef pfd As WIN32_FIND_DATAW, ByVal fFlags As SLGP_FLAGS) Implements IShellLinkW.GetPath
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.GetShowCmd"/></summary>
        Private Sub GetShowCmd(ByRef piShowCmd As Integer) Implements IShellLinkW.GetShowCmd
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.GetWorkingDirectory"/></summary>
        Private Sub GetWorkingDirectory(ByVal pszDir As System.Text.StringBuilder, ByVal cchMaxPath As Integer) Implements IShellLinkW.GetWorkingDirectory
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.Resolve"/></summary>
        Private Sub Resolve(ByVal hwnd As System.IntPtr, ByVal fFlags As SLR_FLAGS) Implements IShellLinkW.Resolve
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.SetArguments"/></summary>
        Private Sub SetArguments(ByVal pszArgs As String) Implements IShellLinkW.SetArguments
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.SetDescription"/></summary>
        Private Sub SetDescription(ByVal pszName As String) Implements IShellLinkW.SetDescription
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.SetHotkey"/></summary>
        Private Sub SetHotkey(ByVal wHotkey As Short) Implements IShellLinkW.SetHotkey
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.SetIconLocation"/></summary>
        Private Sub SetIconLocation(ByVal pszIconPath As String, ByVal iIcon As Integer) Implements IShellLinkW.SetIconLocation
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.SetIDList"/></summary>
        Private Sub SetIDList(ByVal pidl As System.IntPtr) Implements IShellLinkW.SetIDList
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.SetPath"/></summary>
        Private Sub SetPath(ByVal pszFile As String) Implements IShellLinkW.SetPath
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.SetRelativePath"/></summary>
        Private Sub SetRelativePath(ByVal pszPathRel As String, ByVal dwReserved As Integer) Implements IShellLinkW.SetRelativePath
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.SetShowCmd"/></summary>
        Private Sub SetShowCmd(ByVal iShowCmd As Integer) Implements IShellLinkW.SetShowCmd
        End Sub
        ''' <summary>Implements <see cref="IShellLinkW.SetWorkingDirectory"/></summary>
        Private Sub SetWorkingDirectory(ByVal pszDir As String) Implements IShellLinkW.SetWorkingDirectory
        End Sub
    End Class

    ''' <summary>Window styles <see cref="IShellLinkW.SetShowCmd"/>, <see cref="IShellLinkW.GetShowCmd"/></summary>
    Friend Enum ShellLinkWindowStyle As Integer
        ''' <summary>Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position.</summary>
        SW_SHOWNORMAL = 1
        ''' <summary>Activates the window and displays it as a maximized window.</summary>
        SW_SHOWMAXIMIZED = 3
        ''' <summary>Minimizes the window and activates the next top-level window.</summary>
        SW_SHOWMINIMIZED = 2
        ''' <summary>Window will not be activated</summary>
        SW_SHOWMINNOACTIVE = 7
    End Enum
End Namespace