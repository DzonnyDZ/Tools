Imports System.Runtime.InteropServices
Imports System.ComponentModel

#If Config <= Nightly Then 'Stage:Nightly
Namespace API
    ''' <summary>Contains API decalarations related to file system</summary>
    Module FileSystem
#Region "Structures"
        ''' <summary>Contains information about a file object.</summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SHFILEINFO 'ASAP:MSDN
            ''' <summary>A handle to the icon that represents the file. You are responsible for destroying this handle with DestroyIcon when you no longer need it.</summary>
            Public hIcon As IntPtr
            ''' <summary>The index of the icon image within the system image list.</summary>
            Public iIcon As IntPtr
            ''' <summary>An array of values that indicates the attributes of the file object.</summary>
            Public dwAttributes As FileFlags
            ''' <summary>A string that contains the name of the file as it appears in the Microsoft Windows Shell, or the path and file name of the file that contains the icon representing the file.</summary>
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)> _
            Public szDisplayName As String
            ''' <summary>A string that describes the type of file.</summary>
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
            Public szTypeName As String
        End Structure
#End Region
#Region "Constants"
        ''' <summary>Maximum length of path for the <see cref="SHGetFileInfo"/> function</summary>
        Public Const MAX_PATH As Int32 = 260
#End Region
#Region "Functions"
        ''' <summary>Retrieves information about an object in the file system, such as a file, folder, directory, or drive root.</summary>
        ''' <param name="pszPath">[in] A pointer to a null-terminated string of maximum length <see cref="MAX_PATH"/> that contains the path and file name. Both absolute and relative paths are valid.
        ''' <para>If the <paramref name="uFlags"/> parameter includes the <see cref="FileInformationFlags.SHGFI_PIDL"/> flag, this parameter must be the address of an ITEMIDLIST (PIDL) structure that contains the list of item identifiers that uniquely identifies the file within the Shell's namespace. The pointer to an item identifier list (PIDL) must be a fully qualified PIDL. Relative PIDLs are not allowed.</para>
        ''' <para>If the uFlags parameter includes the <see cref="FileInformationFlags.SHGFI_USEFILEATTRIBUTES"/> flag, this parameter does not have to be a valid file name. The function will proceed as if the file exists with the specified name and with the file attributes passed in the dwFileAttributes parameter. This allows you to obtain information about a file type by passing just the extension for pszPath and passing <see cref="fileattributes.FILE_ATTRIBUTE_NORMAL"/> in dwFileAttributes.</para>
        ''' <para>This string can use either short (the 8.3 form) or long file names.</para>
        ''' </param>
        ''' <param name="dwFileAttributes">[in] A combination of one or more file attribute flags (<see cref="FileAttributes">FILE_ATTRIBUTE_</see> values as defined in Winnt.h). If uFlags does not include the <see cref="FileInformationFlags.shgfi_USEFILEATTRIBUTES"/> flag, this parameter is ignored.</param>
        ''' <param name="psfi">[out] The address of a <see cref="SHFILEINFO"/> structure to receive the file information.</param>
        ''' <param name="cbFileInfo">[in] The size, in bytes, of the <see cref="SHFILEINFO"/> structure pointed to by the psfi parameter.</param>
        ''' <param name="uFlags">[in] The flags that specify the file information to retrieve. This parameter can be a combination of the <see cref="FileInformationFlags">SHGFI_</see>  values.</param>
        ''' <returns>Returns a value whose meaning depends on the uFlags parameter.<para>If uFlags does not contain SHGFI_EXETYPE or SHGFI_SYSICONINDEX, the return value is nonzero if successful, or zero otherwise.</para><para>If uFlags contains the SHGFI_EXETYPE flag, the return value specifies the type of the executable file.</para></returns>
        Public Declare Auto Function SHGetFileInfo Lib "shell32.dll" ( _
            ByVal pszPath As String, _
            ByVal dwFileAttributes As FileAttributes, _
            ByRef psfi As SHFILEINFO, _
            ByVal cbFileInfo As UInt32, _
            ByVal uFlags As FileInformationFlags) As IntPtr 'ASAP:MSDN
#End Region
#Region "Enumerations"
        ''' <summary>The flags that specify the file information to retrieve. USed by <see cref="SHGetFileInfo"/>.</summary>
        <Flags()> _
        Public Enum FileInformationFlags As UInt32 'ASAP:MSDN
            <EditorBrowsable(EditorBrowsableState.Advanced)> zeor = 0UI
            ''' <summary>Version 5.0. Apply the appropriate overlays to the file's icon. The <see cref="SHGFI_ICON"/> flag must also be set.</summary>
            SHGFI_ADDOVERLAYS = &H20UI
            ''' <summary>Modify <see cref="SHGFI_ATTRIBUTES"/> to indicate that the dwAttributes member of the <see cref="SHFILEINFO"/> structure at psfi contains the specific attributes that are desired.</summary>
            SHGFI_ATTR_SPECIFIED = &H20000UI
            ''' <summary>Retrieve the item attributes. The attributes are copied to the dwAttributes member of the structure specified in the psfi parameter. These are the same attributes that are obtained from IShellFolder::GetAttributesOf.</summary>
            SHGFI_ATTRIBUTES = &H800UI
            ''' <summary>Retrieve the display name for the file. The name is copied to the szDisplayName member of the structure specified in psfi. The returned display name uses the long file name, if there is one, rather than the 8.3 form of the file name.</summary>
            SHGFI_DISPLAYNAME = &H200UI
            ''' <summary>Retrieve the type of the executable file if pszPath identifies an executable file. The information is packed into the return value. This flag cannot be specified with any other flags.</summary>
            SHGFI_EXETYPE = &H2000UI
            ''' <summary>Retrieve the handle to the icon that represents the file and the index of the icon within the system image list. The handle is copied to the hIcon member of the structure specified by psfi, and the index is copied to the iIcon member.</summary>
            SHGFI_ICON = &H100UI
            ''' <summary>Retrieve the name of the file that contains the icon representing the file specified by pszPath, as returned by the IExtractIcon::GetIconLocation method of the file's icon handler. Also retrieve the icon index within that file. The name of the file containing the icon is copied to the szDisplayName member of the structure specified by psfi. The icon's index is copied to that structure's iIcon member.</summary>
            SHGFI_ICONLOCATION = &H1000UI
            ''' <summary>Modify SHGFI_ICON, causing the function to retrieve the file's large icon. The <see cref="SHGFI_ICON"/> flag must also be set.</summary>
            SHGFI_LARGEICON = &H0UI
            ''' <summary>Modify <see cref="SHGFI_ICON"/>, causing the function to add the link overlay to the file's icon. The <see cref="SHGFI_ICON"/> flag must also be set.</summary>
            SHGFI_LINKOVERLAY = &H8000UI
            ''' <summary>Modify <see cref="SHGFI_ICON"/>, causing the function to retrieve the file's open icon. Also used to modify <see cref="SHGFI_SYSICONINDEX"/>, causing the function to return the handle to the system image list that contains the file's small open icon. A container object displays an open icon to indicate that the container is open. The <see cref="SHGFI_ICON"/> and/or <see cref="SHGFI_SYSICONINDEX"/> flag must also be set.</summary>
            SHGFI_OPENICON = &H2UI
            ''' <summary>Version 5.0. Return the index of the overlay icon. The value of the overlay index is returned in the upper eight bits of the iIcon member of the structure specified by psfi. This flag requires that the <see cref="SHGFI_ICON"/> be set as well.</summary>
            SHGFI_OVERLAYINDEX = &H40UI
            ''' <summary>Indicate that pszPath is the address of an ITEMIDLIST structure rather than a path name.</summary>
            SHGFI_PIDL = &H8UI
            ''' <summary>Modify <see cref="SHGFI_ICON"/>, causing the function to blend the file's icon with the system highlight color. The <see cref="SHGFI_ICON"/> flag must also be set.</summary>
            SHGFI_SELECTED = &H10000UI
            ''' <summary>Modify <see cref="SHGFI_ICON"/>, causing the function to retrieve a Shell-sized icon. If this flag is not specified the function sizes the icon according to the system metric values. The <see cref="SHGFI_ICON"/> flag must also be set.</summary>
            SHGFI_SHELLICONSIZE = &H4UI
            ''' <summary>Modify <see cref="SHGFI_ICON"/>, causing the function to retrieve the file's small icon. Also used to modify <see cref="SHGFI_SYSICONINDEX"/>, causing the function to return the handle to the system image list that contains small icon images. The <see cref="SHGFI_ICON"/> and/or <see cref="SHGFI_SYSICONINDEX"/> flag must also be set.</summary>
            SHGFI_SMALLICON = &H1UI
            ''' <summary>Retrieve the index of a system image list icon. If successful, the index is copied to the iIcon member of psfi. The return value is a handle to the system image list. Only those images whose indices are successfully copied to iIcon are valid. Attempting to access other images in the system image list will result in undefined behavior.</summary>
            SHGFI_SYSICONINDEX = &H4000UI
            ''' <summary>Retrieve the string that describes the file's type. The string is copied to the szTypeName member of the structure specified in psfi.</summary>
            SHGFI_TYPENAME = &H400UI
            ''' <summary>Indicates that the function should not attempt to access the file specified by pszPath. Rather, it should act as if the file specified by pszPath exists with the file attributes passed in dwFileAttributes. This flag cannot be combined with the <see cref="SHGFI_ATTRIBUTES"/>, <see cref="SHGFI_EXETYPE"/>, or <see cref="SHGFI_PIDL"/> flags.</summary>
            SHGFI_USEFILEATTRIBUTES = &H10UI
        End Enum

        ''' <summary>File attributes</summary>
        <Flags()> _
        Public Enum FileAttributes As UInt32 'ASAP:MSDN
            ''' <summary>A file or directory that is an archive file or directory. Applications use this attribute to mark files for backup or removal.</summary>
            FILE_ATTRIBUTE_ARCHIVE = &H20
            ''' <summary>A file or directory that is compressed. For a file, all of the data in the file is compressed. For a directory, compression is the default for newly created files and subdirectories.</summary>
            FILE_ATTRIBUTE_COMPRESSED = &H800
            ''' <summary>Reserved; do not use.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> FILE_ATTRIBUTE_DEVICE = &H40
            ''' <summary>The handle that identifies a directory.</summary>
            FILE_ATTRIBUTE_DIRECTORY = &H10
            ''' <summary>A file or directory that is encrypted. For a file, all data streams in the file are encrypted. For a directory, encryption is the default for newly created files and subdirectories.</summary>
            FILE_ATTRIBUTE_ENCRYPTED = &H4000
            ''' <summary>The file or directory is hidden. It is not included in an ordinary directory listing.</summary>
            FILE_ATTRIBUTE_HIDDEN = &H2
            ''' <summary>A file or directory that does not have other attributes set. This attribute is valid only when used alone.</summary>
            FILE_ATTRIBUTE_NORMAL = &H80
            ''' <summary>The file is not to be indexed by the content indexing service.</summary>
            FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = &H2000
            ''' <summary>The data of a file is not available immediately. This attribute indicates that the file data is physically moved to offline storage. This attribute is used by Remote Storage, which is the hierarchical storage management software. Applications should not arbitrarily change this attribute.</summary>
            FILE_ATTRIBUTE_OFFLINE = &H1000
            ''' <summary>A file or directory that is read-only. For a file, applications can read the file, but cannot write to it or delete it. For a directory, applications cannot delete it.</summary>
            FILE_ATTRIBUTE_READONLY = &H1
            ''' <summary>A file or directory that has an associated reparse point, or a file that is a symbolic link.</summary>
            FILE_ATTRIBUTE_REPARSE_POINT = &H400
            ''' <summary>A file that is a sparse file.</summary>
            FILE_ATTRIBUTE_SPARSE_FILE = &H200
            ''' <summary>A file or directory that the operating system uses a part of, or uses exclusively.</summary>
            FILE_ATTRIBUTE_SYSTEM = &H4
            ''' <summary>A file that is being used for temporary storage. File systems avoid writing data back to mass storage if sufficient cache memory is available, because typically, an application deletes a temporary file after the handle is closed. In that scenario, the system can entirely avoid writing the data. Otherwise, the data is written after the handle is closed.</summary>
            FILE_ATTRIBUTE_TEMPORARY = &H100
            ''' <summary>A file is a virtual file.</summary>
            FILE_ATTRIBUTE_VIRTUAL = &H10000
        End Enum

        ''' <summary>Valid drop-effect values are the result of applying the OR operation to the values contained in the <see cref="DropEfects">DROPEFFECT</see> enumeration</summary>
        <Flags()> _
        Public Enum DropEfects As UInt32
            ''' <summary>Drop results in a copy. The original data is untouched by the drag source.</summary>
            DROPEFFECT_COPY = 1UI
            ''' <summary>Drag source should create a link to the original data.</summary>
            DROPEFFECT_LINK = 4UI
            ''' <summary>Drag source should remove the data.</summary>
            DROPEFFECT_MOVE = 2UI
            ''' <summary>Drop target cannot accept the data.</summary>
            DROPEFFECT_NONE = 0UI
            ''' <summary>Scrolling is about to start or is currently occurring in the target. This value is used in addition to the other values.</summary>
            DROPEFFECT_SCROLL = &H80000000UI
        End Enum
        ''' <summary>File flags</summary>
        <Flags()> _
        Public Enum FileFlags As UInt32   'ASAP:MSDN
            ''' <summary>The specified items can be browsed in place. This implies that the client can bind to this object as shown in a general form here.</summary>
            SFGAO_BROWSABLE = &H8000000UI
            ''' <summary>The specified items can be copied (same value as the <see cref="DropEfects.DROPEFFECT_COPY"/>) flag).</summary>
            SFGAO_CANCOPY = DropEfects.DROPEFFECT_COPY
            ''' <summary>The specified items can be deleted by selecting Delete from their context menus.</summary>
            SFGAO_CANDELETE = &H20UI
            ''' <summary>Shortcuts can be created for the specified items. This flag has the same value as <see cref="DropEfects.DROPEFFECT_LINK"/>. The normal use of this flag is to add a Create Shortcut item to the shortcut menu that is displayed during drag-and-drop operations. However, SFGAO_CANLINK also adds a Create Shortcut item to the Microsoft Windows Explorer's File menu and to normal shortcut menus. If this item is selected, your application's IContextMenu::InvokeCommand is invoked with the lpVerb member of the CMINVOKECOMMANDINFO structure set to "link." Your application is responsible for creating the link.</summary>
            SFGAO_CANLINK = DropEfects.DROPEFFECT_LINK
            ''' <summary>It is possible to create monikers for the specified items.</summary>
            SFGAO_CANMONIKER = &H400000UI
            ''' <summary>The specified items can be moved (same value as the <see cref="DropEfects.DROPEFFECT_MOVE"/> flag).</summary>
            SFGAO_CANMOVE = DropEfects.DROPEFFECT_MOVE
            ''' <summary>The specified items can be renamed. Note that this flag is essentially a suggestion. It does not guarantee that a namespace client will rename the file or folder object.</summary>
            SFGAO_CANRENAME = &H10UI
            ''' <summary>Do not use.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> SFGAO_CAPABILITYMASK = &H177UI
            ''' <summary>The specified items are compressed.</summary>
            SFGAO_COMPRESSED = &H4000000UI
            ''' <summary>Do not use.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> SFGAO_CONTENTSMASK = &H80000000UI
            ''' <summary>Do not use.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> SFGAO_DISPLAYATTRMASK = &HF0000UI
            ''' <summary>The specified items are drop targets.</summary>
            SFGAO_DROPTARGET = &H100UI
            ''' <summary>The item is encrypted and may require special presentation.</summary>
            SFGAO_ENCRYPTED = &H2000UI
            ''' <summary>The specified folder objects are either file system folders or have at least one descendant (child, grandchild, or later) that is a file system folder.</summary>
            SFGAO_FILESYSANCESTOR = &H10000000UI
            ''' <summary>The specified items are part of the file system (that is, they are files, directories, or root directories). The parsed names of the items can be assumed to be valid Win32 file system paths. These paths can be either Universal Naming Convention (UNC) or drive-letter based.</summary>
            SFGAO_FILESYSTEM = &H40000000UI
            ''' <summary>The specified items are folders. Some items can be flagged with both <see cref="SFGAO_STREAM"/> and <see cref="SFGAO_FOLDER"/>, such as a .zip file. Some applications may include this flag when testing for items that are both files and containers.</summary>
            SFGAO_FOLDER = &H20000000UI
            ''' <summary>The specified items should be displayed using a ghosted icon.</summary>
            SFGAO_GHOSTED = &H80000UI
            ''' <summary>The specified items have property sheets.</summary>
            SFGAO_HASPROPSHEET = &H40UI
            ''' <summary>Not supported.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> SFGAO_HASSTORAGE = &H400000UI
            ''' <summary>The specified folder objects may have subfolders and are, therefore, expandable in the left pane of Windows Explorer.</summary>
            ''' <remarks>Note  The SFGAO_HASSUBFOLDER attribute is only advisory and may be returned by Shell folder implementations even if they do not contain subfolders. Note, however, that the converse—failing to return SFGAO_HASSUBFOLDER—definitively states that the folder objects do not have subfolders.<para>Returning SFGAO_HASSUBFOLDER is recommended whenever a significant amount of time is required to determine whether or not any subfolders exist. For example, the Shell always returns SFGAO_HASSUBFOLDER when a folder is located on a network drive.</para></remarks>
            SFGAO_HASSUBFOLDER = &H80000000UI
            ''' <summary>The item is hidden and should not be displayed unless the Show hidden files and folders option is enabled in Folder Settings.</summary>
            SFGAO_HIDDEN = &H80000UI
            ''' <summary>Indicates that accessing the object (through IStream or other storage interfaces) is a slow operation. Applications should avoid accessing items flagged with <see cref="SFGAO_ISSLOW"/>.</summary>
            SFGAO_ISSLOW = &H4000UI
            ''' <summary>The specified items are shortcuts.</summary>
            SFGAO_LINK = &H10000UI
            ''' <summary>The specified items contain new content.</summary>
            SFGAO_NEWCONTENT = &H200000UI
            ''' <summary>The specified items are nonenumerated items. That is, they are not returned by the enumerator created by the IShellFolder::EnumObjects method.</summary>
            SFGAO_NONENUMERATED = &H100000UI
            ''' <summary>Mask for PKEY_SFGAOFlags attributes such as SFGAO_VALIDATE, SFGAO_ISSLOW, and SFGAO_HASSUBFOLDER. They are considered to cause slow calculations or lack context.</summary>
            SFGAO_PKEYSFGAOMASK = &H81044000UI
            ''' <summary>The specified items are read-only. In the case of folders, this means that new items cannot be created in those folders. </summary>
            [SFGAO_READONLY] = &H40000UI
            ''' <summary>The specified items are on removable media or are themselves removable devices.</summary>
            SFGAO_REMOVABLE = &H2000000UI
            ''' <summary>The specified folder objects are shared.</summary>
            SFGAO_SHARE = &H20000UI
            ''' <summary>The item can be bound to an IStorage interface through IShellFolder::BindToObject.</summary>
            SFGAO_STORAGEANCESTOR = &H10000000UI
            ''' <summary>This flag is a mask for the storage capability attributes.</summary>
            SFGAO_STORAGECAPMASK = &H70C50008UI
            ''' <summary>Indicates that the item has a stream associated with it that can be accessed by a call to IShellFolder::BindToObject with IID_IStream in the riid parameter. The pbc parameter in that same call provides the IBindCtx interface that specifies the access mode, such as read-only or read-write. Some items can be flagged with both SFGAO_STREAM and SFGAO_FOLDER, such as a .zip file. Some applications may include this flag when testing for items that are both files and containers.</summary>
            SFGAO_STREAM = &H400000UI
            ''' <summary>When specified as input, SFGAO_VALIDATE instructs the folder to validate that the items pointed to by the contents of apidl exist. If one or more of those items do not exist, IShellFolder::GetAttributesOf returns a failure code. When used with the file system folder, SFGAO_VALIDATE instructs the folder to discard cached properties retrieved by clients of IShellFolder2::GetDetailsEx that may have accumulated for the specified items.</summary>
            SFGAO_VALIDATE = &H1000000UI
        End Enum
#End Region
    End Module
End Namespace
#End If