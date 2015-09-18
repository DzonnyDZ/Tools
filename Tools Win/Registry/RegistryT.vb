Imports Microsoft.Win32, Tools.ExtensionsT
Imports Tools.DataStructuresT.GenericT
Imports System.ComponentModel

#If True
Namespace RegistryT
    Public Module RegistryT
        ''' <summary>Represents <see cref="Registry.ClassesRoot"/> key</summary>
        Public Const HKEY_CLASSES_ROOT$ = "HKEY_CLASSES_ROOT"
        ''' <summary>Represents <see cref="Registry.CurrentUser"/> key</summary>
        Public Const HKEY_CURRENT_USER$ = "HKEY_CURRENT_USER"
        ''' <summary>Represents <see cref="Registry.LocalMachine"/> key</summary>
        Public Const HKEY_LOCAL_MACHINE$ = "HKEY_LOCAL_MACHINE"
        ''' <summary>Represents <see cref="Registry.Users"/> key</summary>
        Public Const HKEY_USERS$ = "HKEY_USERS"
        ''' <summary>Represents <see cref="Registry.CurrentConfig"/> key</summary>
        Public Const HKEY_CURRENT_CONFIG$ = "HKEY_CURRENT_CONFIG"
        ''' <summary>Represents <see cref="Registry.DynData"/> key</summary>
        ''' <remarks>Obsolete. SUpported only on Win9x systems where neither .NET Framework 4 nor ĐTools are supported. Use <see cref="HKEY_PERFORMANCE_DATA"/> insted on NT-based systems.</remarks>
        ''' <version version="1.5.3">This constant was made obsolete and hidden because HKEY_DYN_DATA registry key is supported only on Win9x system as ĐTools are now compiled for .NET Framework 4 ehich is not supported on Windows 9x.</version>
        <Obsolete("HKEY_DYN_DATA key is only supported on Windws 9x and you cannot run .NET 4+ applications on Windows 9x. You can use HKEY_PERFORMANCE_DATA on NT-based systems instead.")>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Const HKEY_DYN_DATA$ = "HKEY_DYN_DATA"
        ''' <summary>Represents <see cref="Registry.PerformanceData"/> key</summary>
        Public Const HKEY_PERFORMANCE_DATA$ = "HKEY_PERFORMANCE_DATA"
        ''' <summary>Splits full registry key path to base registry key and path of subkey</summary>
        ''' <param name="KeyPath">Full registry key path. Parts separated by backslashes (\), starting with base key name</param>
        ''' <returns>Pair of <see cref="RegistryKey"/> representing base key and string representing path of sub key. If path consists only of base key name, sub-key path is null.</returns>
        ''' <exception cref="ArgumentException">First segment of <paramref name="KeyPath"/> is not one of well-known registry base keys (as defined by HKEY_ constants from in <see cref="RegistryT"/>)</exception>
        ''' <exception cref="NotSupportedException">First segment of <paramref name="KeyPath"/> is HKEY_DYN_DATA (see <see cref="HKEY_DYN_DATA"/>).</exception>
        ''' <version version="1.5.3">Removed support for HKEY_DYN_DATA registry hive. It's bacuse that hive is supported only on Win9x systems and ĐTools are now compiled for .NET Framework 4 which is not supported on Win9x system.</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Function SplitKeyPath(ByVal KeyPath$) As IPair(Of RegistryKey, String)
            Dim ret As New Pair(Of RegistryKey, String)(Nothing, Nothing)
            Dim parts = Split(KeyPath, "\", 2)
            Select Case parts(0).ToUpper
                Case HKEY_CLASSES_ROOT : ret.Value1 = Registry.ClassesRoot
                Case HKEY_CURRENT_CONFIG : ret.Value1 = Registry.CurrentConfig
                Case HKEY_CURRENT_USER : ret.Value1 = Registry.CurrentUser
                Case HKEY_LOCAL_MACHINE : ret.Value1 = Registry.LocalMachine
                Case HKEY_USERS : ret.Value1 = Registry.Users
#If Framework < 3 Then
                Case HKEY_DYN_DATA : ret.Value1 = Registry.DynData
#Else
                Case "HKEY_DYN_DATA" : Throw New NotSupportedException(ResourcesT.Exceptions.RegistryHiveIsNoLongerSupported.f("HKEY_DYN_DATA")) 'Const not used to avoid obsolete warning
#End If
                Case HKEY_PERFORMANCE_DATA : ret.Value1 = Registry.PerformanceData
                Case Else : Throw New ArgumentException(ResourcesT.ExceptionsWin.IsUnknownRegistryBaseKey.f(parts(0)))
            End Select
            If parts.Length > 1 Then ret.Value2 = parts(1)
            Return ret
        End Function
        ''' <summary>Retrieves a specified registry key.</summary>
        ''' <param name="KeyPath">Full path of the key to open.</param>
        ''' <param name="writable">Set to true if you need write access to the key.</param>
        ''' <returns>The subkey requested, or null if the operation failed.</returns>
        ''' <exception cref="System.ArgumentNullException"><paramref name="KeyPath"/> is null.</exception>
        ''' <exception cref="System.ArgumentException"><paramref name="KeyPath"/> (excluding first segment) is longer than the maximum length allowed (255 characters).</exception>
        ''' <exception cref="System.Security.SecurityException">The user does not have the permissions required to access the registry key in the specified mode.</exception>
        Public Function OpenKey(ByVal KeyPath$, Optional ByVal writable As Boolean = False) As RegistryKey
            If KeyPath Is Nothing Then Throw New ArgumentNullException("KeyPath")
            Dim Parts = SplitKeyPath(KeyPath)
            If Parts.Value2 <> "" Then Return Parts.Value1.OpenSubKey(Parts.Value2)
            Return Parts.Value1
        End Function
        ''' <summary>Retrieves a specified registry key.</summary>
        ''' <param name="KeyPath">Full path of the key to open.</param>
        ''' <param name="permissionCheck">One of the <see cref="Microsoft.Win32.RegistryKeyPermissionCheck"/> values that specifies whether the key is opened for read or read/write access.</param>
        ''' <returns>The subkey requested, or null if the operation failed.</returns>
        ''' <exception cref="System.ArgumentNullException"><paramref name="KeyPath"/> is null.</exception>
        ''' <exception cref="System.ArgumentException"><paramref name="KeyPath"/> (excluding first segment) is longer than the maximum length allowed (255 characters).</exception>
        ''' <exception cref="System.Security.SecurityException">The user does not have the permissions required to access the registry key in the specified mode.</exception>
        Public Function OpenKey(ByVal KeyPath$, ByVal permissionCheck As RegistryKeyPermissionCheck) As RegistryKey
            Dim Parts = SplitKeyPath(KeyPath)
            If Parts.Value2 <> "" Then Return Parts.Value1.OpenSubKey(Parts.Value2, permissionCheck)
            Return Parts.Value1
        End Function
        ''' <summary>Retrieves a specified registry key.</summary>
        ''' <param name="KeyPath">Full path of the key to open.</param>
        ''' <param name="permissionCheck">One of the <see cref="Microsoft.Win32.RegistryKeyPermissionCheck"/> values that specifies whether the key is opened for read or read/write access.</param>
        ''' <param name="rights"> A bitwise combination of <see cref="System.Security.AccessControl.RegistryRights"/> values that specifies the desired security access.</param>
        ''' <returns>The subkey requested, or null if the operation failed.</returns>
        ''' <exception cref="System.ArgumentNullException"><paramref name="KeyPath"/> is null.</exception>
        ''' <exception cref="System.ArgumentException"><paramref name="KeyPath"/> (excluding first segment) is longer than the maximum length allowed (255 characters).</exception>
        ''' <exception cref="System.Security.SecurityException"><paramref name="rights"/> includes invalid registry rights values.  -or- The user does not have the requested permissions.</exception>
        Public Function OpenKey(ByVal KeyPath$, ByVal permissionCheck As RegistryKeyPermissionCheck, ByVal rights As Security.AccessControl.RegistryRights) As RegistryKey
            Dim Parts = SplitKeyPath(KeyPath)
            If Parts.Value2 <> "" Then Return Parts.Value1.OpenSubKey(Parts.Value2, permissionCheck, rights)
            Return Parts.Value1
        End Function

    End Module
End Namespace
#End If