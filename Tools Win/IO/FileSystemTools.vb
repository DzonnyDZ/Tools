Imports Tools.IOt, Tools.API, System.Drawing, Tools.ExtensionsT.StringExtensions
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

#If Config <= Nightly Then 'Stage:Nightly
Namespace IOt
    ''' <summary>Contains file system-related methods and extension methods</summary>
    Public Module FileTystemTools
        ''' <summary>Gets icon for given file or folder</summary>
        ''' <param name="PathP">Provides path to get icon for</param>
        ''' <param name="Large">True to get large icon (false to get small icon)</param>
        ''' <returns>Icon that represents given file or folder in operating system. Null if icon obtainment failed.</returns>
        ''' <param name="Overlays">True to add all applicable overlay icons</param>
        ''' <seelaso cref="Drawing.Icon.ExtractAssociatedIcon"/>
        ''' <exception cref="IO.FileNotFoundException">File <paramref name="Path"/> does not exists.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Path"/> is null</exception>
        <Extension()> _
        Public Function GetIcon(ByVal PathP As IPathProvider, Optional ByVal Large As Boolean = False, Optional ByVal Overlays As Boolean = True) As Drawing.Icon
            If PathP Is Nothing Then Throw New ArgumentNullException("PathP")
            Dim Path = New Path(PathP.Path)
            If Not Path.Exists Then Throw New IO.FileNotFoundException(String.Format(ResourcesT.ExceptionsWin.Path0DoesNotExist, Path))
            Dim shInfo As New SHFILEINFO
            Dim ret = SHGetFileInfo(Path, 0, shInfo, Marshal.SizeOf(shInfo), _
                FileInformationFlags.SHGFI_ICON Or If(Large, FileInformationFlags.SHGFI_LARGEICON, FileInformationFlags.SHGFI_SMALLICON) Or If(Overlays, FileInformationFlags.SHGFI_ADDOVERLAYS, CType(0, FileInformationFlags)))
            If shInfo.hIcon = IntPtr.Zero Then Return Nothing
            Dim icon = Drawing.Icon.FromHandle(shInfo.hIcon)
            Return icon
        End Function
        ''' <summary>Gets icon for given file or folder</summary>
        ''' <param name="Path">Path to get icon for</param>
        ''' <param name="Large">True to get large icon (false to get small icon)</param>
        ''' <returns>Icon that represents given file or folder in operating system</returns>
        ''' <param name="Overlays">True to add all applicable overlay icons</param>
        ''' <seelaso cref="Drawing.Icon.ExtractAssociatedIcon"/>
        ''' <exception cref="IO.FileNotFoundException">File <paramref name="Path"/> does not exists.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Path"/> is null</exception>
        Public Function GetIcon(ByVal Path As String, Optional ByVal Large As Boolean = False, Optional ByVal Overlays As Boolean = True) As Drawing.Icon
            If Path Is Nothing Then Throw New ArgumentNullException("Path")
            Return GetIcon(DirectCast(New Path(Path), IPathProvider), Large)
        End Function
    End Module
End Namespace
#End If