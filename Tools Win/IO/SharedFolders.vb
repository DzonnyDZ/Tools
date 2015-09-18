Imports System.Runtime.InteropServices
Namespace IOt
#If True
    ''' <summary>Contains static functions for working with Windows (Samba) shared folders</summary>
    ''' <version version="1.5.2" stage="Nightly">Module introduced</version>
    Public Module SharedFolders
        ''' <summary>Gets list of names of shared folders of given server</summary>
        ''' <param name="server">Name of server (or IP address; excluding leading \\)</param>
        ''' <returns>List of names of folders shared by server. List contains only folder names. To use, each nam must be preppended by \\<paramref name="server"/>\</returns>
        ''' <exception cref="API.Win32APIException">An error occured</exception>
        Public Function GetSharedFolders(ByVal server As String) As String()
            Dim dataptr As IntPtr
            Dim num%, total%, hresume%
            If API.FileSystem.NetShareEnum(server, API.NetShareLevel.Names, dataptr, API.FileSystem.MAX_PREFERRED_LENGTH, num, total, hresume) <> API.Errors.NERR_Success Then
                Throw New API.Win32APIException
            End If
            Dim ret(num - 1) As String
            For i = 0 To num - 1
                Dim Item As API.FileSystem.SHARE_INFO_0 = Marshal.PtrToStructure(CType(dataptr.ToInt32 + Marshal.SizeOf(GetType(API.FileSystem.SHARE_INFO_0)) * i, IntPtr), GetType(API.FileSystem.SHARE_INFO_0))
                ret(i) = Item.shi0_netname
            Next
            Return ret
        End Function
    End Module
#End If
End Namespace