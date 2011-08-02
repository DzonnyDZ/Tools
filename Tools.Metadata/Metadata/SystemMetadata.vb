'Localize: This file is not localized
#If Config <= Nightly Then  'Stage: Nightly
Imports System.Linq, Tools.ExtensionsT
Namespace MetadataT
    ''' <summary>Provides access to system information about file in form of <see cref="IMetadata"/></summary>
    ''' <remarks>This implementation of <see cref="IMetadata"/> supports only named values. Possible values for keys are same as possible values for names.</remarks>
    ''' <version version="1.5.3" stage="Nightly">Class introduced</version>
    Public Class SystemMetadata
        Implements IMetadata
        ''' <summary>Represents file information about which are provided</summary>
        Private file As IO.FileInfo
        ''' <summary>CTor from file path</summary>
        ''' <param name="FileName">Full path to file</param>
        ''' <exception cref="IO.FileNotFoundException">File with address <paramref name="FileName"/> does not exist.</exception>
        ''' <exception cref="Security.SecurityException">The caller does not have required permission</exception>
        ''' <exception cref="UnauthorizedAccessException">Access to <paramref name="FileName"/> is denied.</exception>
        ''' <version version="1.5.4">Parameter <c>FileName</c> renamed to <c>fileName</c></version>
        Public Sub New(ByVal fileName As String)
            If Not IO.File.Exists(FileName) Then Throw New IO.FileNotFoundException("File not found", FileName)
            file = New IO.FileInfo(FileName)
        End Sub
        ''' <summary>Creates a new empty instance of the <see cref="SystemMetadata"/> class</summary>
        ''' <remarks>Instance created by this contructor cannot be used for obtaining metadata. It can be used just for enumeration of supported metadata tags.</remarks>
        ''' <version version="1.5.3">This CTor is new in version 1.5.3</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Sub New()
            file = New IO.FileInfo("\\.\NUL")
        End Sub

#Region "Properties"
        ''' <summary>Gets the full path of the directory or file.</summary>
        ''' <returns>A string containing the full path.</returns>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        Public ReadOnly Property FullPath As String
            Get
                Return file.FullName
            End Get
        End Property
        ''' <summary>Gets a string representing the directory's full path.</summary>
        ''' <returns>A string representing the directory's full path.</returns>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentNullException">null was passed in for the directory name.</exception>
        Public ReadOnly Property DirectoryName As String
            Get
                Return file.DirectoryName
            End Get
        End Property
        ''' <summary>Gets the name of the file.</summary>
        ''' <returns>The name of the file.</returns>
        Public ReadOnly Property FileName As String
            Get
                Return file.Name
            End Get
        End Property
        ''' <summary>Gets the size, in bytes, of the current file.</summary>
        ''' <returns>The size of the current file in bytes.</returns>
        ''' <exception cref="System.IO.IOException"><see cref="System.IO.FileSystemInfo.Refresh" /> cannot update the state of the file or directory.</exception>
        ''' <exception cref="System.IO.FileNotFoundException">The file does not exist.  -or- The <see cref="Size"/> property is called for a directory.</exception>
        Public ReadOnly Property Size As Long
            Get
                Return file.Length
            End Get
        End Property
        ''' <summary>Gets the creation time of the current <see cref="System.IO.FileSystemInfo" /> object.</summary>
        ''' <returns>The creation date and time of the current <see cref="System.IO.FileSystemInfo" /> object.</returns>
        ''' <exception cref="System.IO.IOException"><see cref="System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        ''' <exception cref="System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        Public ReadOnly Property CreationTime As Date
            Get
                Return file.CreationTime
            End Get
        End Property
        ''' <summary>Gets the creation time, in coordinated universal time (UTC), of the current <see cref="System.IO.FileSystemInfo" /> object.</summary>
        ''' <returns>The creation date and time in UTC format of the current <see cref="System.IO.FileSystemInfo" /> object.</returns>
        ''' <exception cref="System.IO.IOException"><see cref="System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        ''' <exception cref="System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        Public ReadOnly Property CreationTimeUtc As Date
            Get
                Return file.CreationTimeUtc
            End Get
        End Property
        ''' <summary>Gets the time when the current file or directory was last written to.</summary>
        ''' <returns>The time the current file was last written.</returns>
        ''' <exception cref="System.IO.IOException"><see cref="System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
        ''' <exception cref="System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        Public ReadOnly Property LastWriteTime As Date
            Get
                Return file.LastWriteTime
            End Get
        End Property
        ''' <summary>Gets the time, in coordinated universal time (UTC), when the current file or directory was last written to.</summary>
        ''' <returns>The UTC time when the current file was last written to.</returns>
        ''' <exception cref="System.IO.IOException"><see cref="System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
        ''' <exception cref="System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        Public ReadOnly Property LastWriteTimeUtc As Date
            Get
                Return file.LastWriteTimeUtc
            End Get
        End Property
        ''' <summary>Gets the time the current file or directory was last accessed.</summary>
        ''' <returns>The time that the current file or directory was last accessed.</returns>
        ''' <exception cref="System.IO.IOException"><see cref="System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
        ''' <exception cref="System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        Public ReadOnly Property LastAccessTime As Date
            Get
                Return file.LastAccessTime
            End Get
        End Property
        ''' <summary>Gets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.</summary>
        ''' <returns>The UTC time that the current file or directory was last accessed.</returns>
        ''' <exception cref="System.IO.IOException"><see cref="System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
        ''' <exception cref="System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        Public ReadOnly Property LastAccessTimeUtc As Date
            Get
                Return file.LastAccessTimeUtc
            End Get
        End Property
        ''' <summary>Gets the string representing the extension part of the file.</summary>
        ''' <returns>A string containing the <see cref="System.IO.FileSystemInfo" /> extension.</returns>
        Public ReadOnly Property Extension As String
            Get
                Return file.Extension
            End Get
        End Property
        ''' <summary>Returns the file name of the specified path string without the extension.</summary>
        ''' <returns>A <see cref="System.String" /> containing the string returned by <see cref="FileName" /> minus the last period (.) and all characters following it.</returns>
        Public ReadOnly Property NameWithoutExtension As String
            Get
                Return IO.Path.GetFileNameWithoutExtension(file.Name)
            End Get
        End Property
        ''' <summary>Getsa value that determines if the current file is read only.</summary>
        ''' <returns>true if the current file is read only; otherwise, false.</returns>
        ''' <exception cref="System.IO.FileNotFoundException">The file described by the current <see cref="System.IO.FileInfo" /> object could not be found.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The file described by the current <see cref="System.IO.FileInfo" /> object is read-only.  -or- This operation is not supported on the current platform.  -or- The caller does not have the required permission.</exception>
        Public ReadOnly Property [IsReadOnly] As Boolean
            Get
                Return file.IsReadOnly
            End Get
        End Property
        ''' <summary>Gets value that determines if the current file is compressed.</summary>
        ''' <returns>True if the current file is compressed; otherwise false</returns>
        ''' <exception cref="System.IO.FileNotFoundException">The specified file does not exist.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.IO.IOException"><see cref="System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
        Public ReadOnly Property IsCompressed As Boolean
            Get
                Return file.Attributes And IO.FileAttributes.Compressed
            End Get
        End Property
        ''' <summary>Gets value that determines if the current file is system.</summary>
        ''' <returns>True if the current file is system; otherwise false</returns>
        ''' <exception cref="System.IO.FileNotFoundException">The specified file does not exist.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.IO.IOException"><see cref="System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
        Public ReadOnly Property IsSystem As Boolean
            Get
                Return file.Attributes And IO.FileAttributes.System
            End Get
        End Property
        ''' <summary>Gets value that determines if the current file is hidden.</summary>
        ''' <returns>True if the current file is hidden; otherwise false</returns>
        ''' <exception cref="System.IO.FileNotFoundException">The specified file does not exist.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.IO.IOException"><see cref="System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
        Public ReadOnly Property IsHidden As Boolean
            Get
                Return file.Attributes And IO.FileAttributes.Hidden
            End Get
        End Property
        ''' <summary>Gets value that determines if the current file is hiddne.</summary>
        ''' <returns>True if the current file is hidden; otherwise false</returns>
        ''' <exception cref="System.IO.FileNotFoundException">The specified file does not exist.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.IO.IOException"><see cref="System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
        Public ReadOnly Property IsEncrypred As Boolean
            Get
                Return file.Attributes And IO.FileAttributes.Encrypted
            End Get
        End Property
        ''' <summary>Gets the <see cref="System.IO.FileAttributes" /> of the current <see cref="System.IO.FileSystemInfo" />.</summary>
        ''' <returns><see cref="System.IO.FileAttributes" /> of the current <see cref="System.IO.FileSystemInfo" />.</returns>
        ''' <exception cref="System.IO.FileNotFoundException">The specified file does not exist.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.IO.IOException"><see cref="System.IO.FileSystemInfo.Refresh" /> cannot initialize the data.</exception>
        Public ReadOnly Property Attributes As IO.FileAttributes
            Get
                Return file.Attributes
            End Get
        End Property
#End Region
#Region "IMetadata"
        ''' <summary>Contains names of properties</summary>
        Private Shared ReadOnly Keys As String() = {"FullPath", "DirectoryName", "FileName", "Size", "CreationTime", "CreationTimeUtc", "LastWriteTime", "LastWriteTimeUtc", "LastAccessTime", "LastAccessTimeUtc", "Extension", "NameWithoutExtension", "IsReadOnly", "IsCompresses", "IsSystem", "IsHidden", "IsEncrypted", "Attributes"}

        ''' <summary>Gets value indicating wheather metadata value with given key is present in current instance</summary>
        ''' <param name="Key">Key (or name) to check presence of (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>True when value for given key is present; false otherwise</returns>
        ''' <remarks>The <paramref name="Key"/> parameter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).
        ''' <para>This implementation returns true forr all predefined keys.</para></remarks>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        Public Function ContainsKey(ByVal key As String) As Boolean Implements IMetadata.ContainsKey
            Return Keys.Contains(Key)
        End Function

        ''' <summary>Gets keys of all the metadata present in curent instance</summary>
        ''' <returns>Keys in metadata-specific format of all the metadata present in curent instance. Never returns null; may return anempt enumeration.</returns>
        Public Function GetContainedKeys() As System.Collections.Generic.IEnumerable(Of String) Implements IMetadata.GetContainedKeys
            Return Array.AsReadOnly(Keys)
        End Function

        ''' <summary>Gets localized description for given key (or name)</summary>
        ''' <param name="Key">Key (or name) to get description of</param>
        ''' <returns>Localized description of purpose of metadata item identified by <paramref name="Key"/>; null when description is not available.</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> is in invalid format or it is not one of predefined names.</exception>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        ''' <version version="1.5.4">Descriptions extracted to resource and localized to Czech</version>
        Public Function GetDescription(ByVal key As String) As String Implements IMetadata.GetDescription
            Select Case Key
                Case "FullPath" : Return My.Resources.d_FullPath
                Case "DirectoryName" : Return My.Resources.d_DirectoryName
                Case "FileName" : Return My.Resources.d_FileName
                Case "Size" : Return My.Resources.d_Size
                Case "CreationTime" : Return My.Resources.d_CreationTime
                Case "CreationTimeUtc" : Return My.Resources.d_CreationTimeUtc
                Case "LastWriteTime" : Return My.Resources.d_LastWriteTime
                Case "LastWriteTimeUtc" : Return My.Resources.d_LastWriteTimeUtc
                Case "LastAccessTime" : Return My.Resources.d_LastAccessTime
                Case "LastAccessTimeUtc" : Return My.Resources.d_LastAccessTimeUtc
                Case "Extension" : Return My.Resources.d_Extension
                Case "NameWithoutExtension" : Return My.Resources.d_NameWithoutExtension
                Case "IsReadOnly" : Return My.Resources.d_IsReadOnly
                Case "IsCompresses" : Return My.Resources.d_IsCompresses
                Case "IsSystem" : Return My.Resources.d_IsSystem
                Case "IsHidden" : Return My.Resources.d_IsHidden
                Case "IsEncrypted" : Return My.Resources.d_IsEncrypted
                Case "Attributes" : Return My.Resources.d_Attributes
                Case Else : Return Nothing
            End Select
        End Function

        ''' <summary>Gets localized human-readable name for given key (or name)</summary>
        ''' <param name="Key">Key (or name) to get name for</param>
        ''' <returns>Human-readable descriptive name of metadata item identified by <paramref name="Key"/>; null when no such name is defined/known.</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid formar or it is not one of predefined names.</exception>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        ''' <version version="1.5.4">Display names extracted to resource and localized to Czech</version>
        Public Function GetHumanName(ByVal key As String) As String Implements IMetadata.GetHumanName
            Select Case Key
                Case "FullPath" : Return My.Resources.n_FullPath
                Case "DirectoryName" : Return My.Resources.n_DirectoryName
                Case "FileName" : Return My.Resources.n_FileName
                Case "Size" : Return My.Resources.n_Size
                Case "CreationTime" : Return My.Resources.n_CreationTime
                Case "CreationTimeUtc" : Return My.Resources.n_CreationTimeUtc
                Case "LastWriteTime" : Return My.Resources.n_LastWriteTime
                Case "LastWriteTimeUtc" : Return My.Resources.n_LastWriteTimeUtc
                Case "LastAccessTime" : Return My.Resources.n_LastAccessTime
                Case "LastAccessTimeUtc" : Return My.Resources.n_LastAccessTimeUtc
                Case "Extension" : Return My.Resources.n_Extension
                Case "NameWithoutExtension" : Return My.Resources.n_NameWithoutExtension
                Case "IsReadOnly" : Return My.Resources.n_IsReadOnly
                Case "IsCompresses" : Return My.Resources.n_IsCompresses
                Case "IsSystem" : Return My.Resources.n_IsSystem
                Case "IsHidden" : Return My.Resources.n_IsHidden
                Case "IsEncrypted" : Return My.Resources.n_IsEncrypted
                Case "Attributes" : Return My.Resources.n_Attributes
                Case Else : Return Nothing
            End Select
        End Function

        ''' <summary>Gets key for predefined name</summary>
        ''' <param name="Name">Name to get key for</param>
        ''' <returns>Key in metadata-specific format for given predefined metadata item name</returns>
        ''' <exception cref="ArgumentException"><paramref name="Name"/> is not one of predefined names retuened by <see cref="GetPredefinedNames"/>.</exception>
        ''' <remarks>This implementation either returns <paramref name="Name"/> or throws <see cref="ArgumentException"/></remarks>
        ''' <version version="1.5.4">Message of <see cref="ArgumentException"/> made localizable and localized to Czech</version>
        Private Function GetKeyOfName(ByVal Name As String) As String Implements IMetadata.GetKeyOfName
            If Keys.Contains(Name) Then Return Name
            Throw New ArgumentException(ResourcesT.Exceptions.UnknownSystemMetadataItemName.f(Name))
        End Function

        ''' <summary>Gets name for key</summary>
        ''' <param name="Key">Key to get name for</param>
        ''' <returns>One of predefined names to use instead of <paramref name="Key"/>; null when given key has no corresponding name.</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid format</exception>
        ''' <remarks>This implementation either returns <paramref name="Name"/> or throws <see cref="ArgumentException"/></remarks>
        ''' <version version="1.5.4">Message of <see cref="ArgumentException"/> made localizable and localized to Czech</version>
        Private Function GetNameOfKey(ByVal Key As String) As String Implements IMetadata.GetNameOfKey
            If Keys.Contains(Key) Then Return Key
            Throw New ArgumentException(ResourcesT.Exceptions.UnknownSystemMetadataItemName.f(Key))
        End Function

        ''' <summary>Gets all keys predefined for curent metadata format</summary>
        ''' <returns>Eumeration containing all predefined (well-known) keys of metadata for this metadata format. Returns always the same enumeration even when values for some keys are not present. Never returns null; may return an empty enumeration.</returns>
        ''' <remarks>Not all predefined keys are required to have corresponding names obtainable via <see cref="GetNameOfKey"/>.
        ''' <para>This implementation returns aexactly the same collection as <see cref="GetPredefinedNames"/>.</para></remarks>
        Private Function GetPredefinedKeys() As System.Collections.Generic.IEnumerable(Of String) Implements IMetadata.GetPredefinedKeys
            Return Array.AsReadOnly(Keys)
        End Function

        ''' <summary>Gets all predefined names for metadata keys</summary>
        ''' <returns>Enumeration containing all predefined names of metadata items for this metadada format. Never returns null; may return an empty enumeration.</returns>
        ''' <remarks>This implementation of <see cref="IMetadata"/> in fact supports only retrieval of metadata by name and thus keys keys are same as names.</remarks>
        Public Function GetPredefinedNames() As System.Collections.Generic.IEnumerable(Of String) Implements IMetadata.GetPredefinedNames
            Return Array.AsReadOnly(Keys)
        End Function

        ''' <summary>Gets metadata value with given key as string</summary>
        ''' <param name="Key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>Value of metadata item with given key as string; or null if given metadata value cannot be obtained</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> is not one of predefined names</exception>
        ''' <version version="1.5.4">Message of <see cref="ArgumentException"/> made localizable and localized to Czech</version>
        Public Function GetStringValue(ByVal Key As String) As String Implements IMetadata.GetStringValue
            Dim [error] As Boolean = False
            Try
                Select Case Key
                    Case "FullPath" : Return FullPath
                    Case "DirectoryName" : Return DirectoryName
                    Case "FileName" : Return FileName
                    Case "Size" : Return Size.ToString
                    Case "CreationTime" : Return CreationTime.ToString
                    Case "CreationTimeUtc" : Return CreationTimeUtc.ToString
                    Case "LastWriteTime" : Return LastWriteTime.ToString
                    Case "LastWriteTimeUtc" : Return LastWriteTimeUtc.ToString
                    Case "LastAccessTime" : Return LastAccessTime.ToString
                    Case "LastAccessTimeUtc" : Return LastAccessTimeUtc.ToString
                    Case "Extension" : Return Extension
                    Case "NameWithoutExtension" : Return NameWithoutExtension
                    Case "IsReadOnly" : Return IsReadOnly.ToString
                    Case "IsCompresses" : Return IsCompressed.ToString
                    Case "IsSystem" : Return IsSystem.ToString
                    Case "IsHidden" : Return IsHidden.ToString
                    Case "IsEncrypted" : Return IsEncrypred.ToString
                    Case "Attributes" : Return Attributes.ToString("F")
                    Case Else : [error] = True : Throw New ArgumentException(ResourcesT.Exceptions.UnknownSystemMetadataItemName.f(Key))
                End Select
            Catch When Not [error]
                Return Nothing
            End Try
        End Function

        ''' <summary>Gets name of metadata format represented by implementation</summary>
        ''' <returns>Name to identify this metadata format by. Always returns <see cref="SystemName"/>.</returns>
        ''' <remarks>All <see cref="IMetadataProvider">IMetadataProviders</see> returning this format should identify the format by this name.</remarks>
        Private ReadOnly Property Name As String Implements IMetadata.Name
            Get
                Return SystemName$
            End Get
        End Property
        ''' <summary>Used to identify system metadata in <see cref="IMetadataProvider"/></summary>
        ''' <seelaso cref="Name"/>
        Public Const SystemName$ = "System"

        ''' <summary>Gets medata value with given key</summary>
        ''' <param name="Key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>Value of metadata item with given key; or null if given metadata value cannot be obtained</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> is not one of predefined names</exception>
        ''' <version version="1.5.4">Message of <see cref="ArgumentException"/> made localizable and localized to Czech</version>
        Default Public ReadOnly Property Value(ByVal key As String) As Object Implements IMetadata.Value
            Get
                Dim [error] As Boolean = False
                Try
                    Select Case Key
                        Case "FullPath" : Return FullPath
                        Case "DirectoryName" : Return DirectoryName
                        Case "FileName" : Return FileName
                        Case "Size" : Return Size
                        Case "CreationTime" : Return CreationTime
                        Case "CreationTimeUtc" : Return CreationTimeUtc
                        Case "LastWriteTime" : Return LastWriteTime
                        Case "LastWriteTimeUtc" : Return LastWriteTimeUtc
                        Case "LastAccessTime" : Return LastAccessTime
                        Case "LastAccessTimeUtc" : Return LastAccessTimeUtc
                        Case "Extension" : Return Extension
                        Case "NameWithoutExtension" : Return NameWithoutExtension
                        Case "IsReadOnly" : Return IsReadOnly
                        Case "IsCompresses" : Return IsCompressed
                        Case "IsSystem" : Return IsSystem
                        Case "IsHidden" : Return IsHidden
                        Case "IsEncrypted" : Return IsEncrypred
                        Case "Attributes" : Return Attributes
                        Case Else : [error] = True : Throw New ArgumentException(ResourcesT.Exceptions.UnknownSystemMetadataItemName.f(Key))
                    End Select
                Catch When Not [error]
                    Return Nothing
                End Try
            End Get
        End Property
#End Region

    End Class
End Namespace
#End If