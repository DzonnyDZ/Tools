Imports Tools.VisualBasicT, System.IO, System.Linq, Tools.LinqT
#If Config <= RC Then 'Stage:RC
Namespace IOt
    ''' <summary>Wraps <see cref="String"/> into separet class representing path and allows operation with it</summary>
    ''' <remarks>There are no check of validity of paths in current file system during operations, so you can operate with nonexisting paths (unless specified otherwise).</remarks>
    ''' <seelaso cref="M:Tools.IOt.FileTystemTools.GetIcon(Tools.IOt.Path,System.Boolean,System.Boolean)"/>
    ''' <author www="http://dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="RC"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    <DebuggerDisplay("{Path}")> _
    Public Class Path
        Implements IEnumerable(Of Path), IPathProvider
#Region "Basic behavior"
        ''' <summary>Contains value of the <see cref="Path"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Path As String
        ''' <summary>CTor from String</summary>
        ''' <param name="Path">Addres in file system from which create the path</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Path"/> is null</exception>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Path"/> is an empty string or consists only of whitespace characters
        ''' -or-
        ''' <paramref name="Path"/> contains invalid character as defined in <see cref="System.IO.Path.GetInvalidPathChars"/>
        ''' </exception>
        Public Sub New(ByVal Path As String)
            Me.Path = Path
        End Sub
        ''' <summary>Copy CTor</summary>
        ''' <param name="a">Instance which's copy to create</param>
        Public Sub New(ByVal a As Path)
            Path = a.Path
        End Sub
        ''' <summary>String representaion of path</summary>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="value"/> is an empty string or consists only of whitespace characters
        ''' -or-
        ''' <paramref name="value"/> contains invalid character as defined in <see cref="System.IO.Path.GetInvalidPathChars"/>
        ''' </exception>
        Public Property Path() As String
            <DebuggerStepThrough()> Get
                Return _Path
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then Throw New ArgumentNullException("value", ResourcesT.Exceptions.PathCannotBeBasedOnNullString)
                If value.Trim = "" Then Throw New ArgumentException(ResourcesT.Exceptions.PathCannotBeBasedOnAnEmptyStringOrStringContainingOnlyWhitespaces)
                For Each ch As Char In System.IO.Path.GetInvalidPathChars
                    If value.Contains(ch) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.PathStringContainsInvalidCharacter0, ch))
                Next ch
                _Path = value
            End Set
        End Property
        ''' <summary>String representaion of path</summary>
        Private ReadOnly Property IPathProvider_Path() As String Implements IPathProvider.Path
            <DebuggerStepThrough()> Get
                Return Path
            End Get
        End Property
        ''' <summary>Returns a <see cref="System.String"/> that represents the current <see cref="Path"/>.</summary>
        ''' <returns>A <see cref="System.String"/> that represents the current <see cref="Path"/></returns>
        Public Overrides Function ToString() As String
            Return Path
        End Function
#End Region
#Region "Operators and conversions"
        ''' <summary>Conbines to paths - Left and right part</summary>
        ''' <param name="Left">Left part of path</param>
        ''' <param name="Right">Right part of path (should be relative to <paramref name="Left"/></param>
        ''' <returns>Paths combined according to rules of current operating system</returns>
        ''' <remarks>
        ''' <para><see cref="ArgumentException"/> caused by empty, null or invalid <see cref="Path.Path"/> should occure only due to wildcart, because other cases are made impossible by setter of <see cref="Path.Path"/></para>
        ''' <para>See also <seealso cref="System.IO.Path.Combine"/></para>
        ''' </remarks>
        ''' <exception cref="ArgumentNullException">
        '''     <see cref="Path.Path"/> of <paramref name="Left"/> or of <paramref name="Right"/> is null</exception>
        ''' <exception cref="ArgumentException">
        '''     <see cref="Path.Path"/> of <paramref name="Left"/> or of <paramref name="Right"/> contain one or more of the invalid characters defined in <see cref="System.IO.Path.InvalidPathChars"/>, or contains a wildcard character.</exception>
        ''' <exception cref="NullReferenceException">
        '''     <paramref name="Left"/> or <paramref name="Right"/> is null</exception>
        Public Shared Operator +(ByVal Left As Path, ByVal Right As Path) As Path
            Return System.IO.Path.Combine(Left.Path, Right.Path)
        End Operator
        ''' <summary>Conbines to paths - Left and right part</summary>
        ''' <param name="Left">Left part of path</param>
        ''' <param name="Right">Right part of path (should be relative to <paramref name="Left"/></param>
        ''' <returns>Paths combined according to rules of current operating system</returns>
        ''' <remarks>
        ''' <para><see cref="ArgumentException"/> caused by empty, null or invalid <see cref="Path.Path"/> should occure only due to wildcart, because other cases are made impossible by setter of <see cref="Path.Path"/></para>
        ''' <para>See also <seealso cref="System.IO.Path.Combine"/></para>
        ''' </remarks>
        ''' <exception cref="ArgumentNullException">
        '''     <see cref="Path.Path"/> of <paramref name="Left"/> or <paramref name="Right"/> is null</exception>
        ''' <exception cref="ArgumentException">
        '''     <see cref="Path.Path"/> of <paramref name="Left"/> or <paramref name="Right"/> contain one or more of the invalid characters defined in <see cref="System.IO.Path.InvalidPathChars"/>, or contains a wildcard character.</exception>
        ''' <exception cref="NullReferenceException">
        '''     <paramref name="Left"/> is null</exception>
        Public Shared Operator +(ByVal Left As Path, ByVal Right As String) As Path
            Return Left + New Path(Right)
        End Operator
        ''' <summary>Conbines to paths - Left and right part</summary>
        ''' <param name="Left">Left part of path</param>
        ''' <param name="Right">Right part of path (should be relative to <paramref name="Left"/></param>
        ''' <returns>Paths combined according to rules of current operating system</returns>
        ''' <remarks>
        ''' <para><see cref="ArgumentException"/> caused by empty, null or invalid <see cref="Path.Path"/> should occure only due to wildcart, because other cases are made impossible by setter of <see cref="Path.Path"/></para>
        ''' <para>See also <seealso cref="System.IO.Path.Combine"/></para>
        ''' </remarks>
        ''' <exception cref="ArgumentNullException">
        '''     <paramref name="Left"/> or <see cref="Path.Path"/> of <paramref name="Right"/> is null</exception>
        ''' <exception cref="ArgumentException">
        '''     <paramref name="Left"/> or <see cref="Path.Path"/> of <paramref name="Right"/> contain one or more of the invalid characters defined in <see cref="System.IO.Path.InvalidPathChars"/>, or contains a wildcard character.</exception>
        ''' <exception cref="NullReferenceException">
        '''     <paramref name="Right"/> is null</exception>
        Public Shared Operator +(ByVal Left As String, ByVal Right As Path) As Path
            Return New Path(Left) + Right
        End Operator
        ''' <summary>Converts <see cref="String"/> into <see cref="IO.Path"/></summary>
        ''' <param name="a"><see cref="String"/> to be converted</param>
        ''' <returns>A new instance of <see cref="Path"/> initialized with <paramref name="a"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="a"/> is an empty string or consists only of whitespace characters
        ''' -or-
        ''' <paramref name="a"/> contains invalid character as defined in <see cref="System.IO.Path.GetInvalidPathChars"/>
        ''' </exception>
        Public Shared Narrowing Operator CType(ByVal a As String) As Path
            Return New Path(a)
        End Operator
        ''' <summary>Converts <see cref="IO.Path"/> into <see cref="String"/></summary>
        ''' <param name="a"><see cref="IO.Path"/> to be converted</param>
        ''' <returns>Value of <see cref="Path.Path"/> property of <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As Path) As String
            Return a.Path
        End Operator
#End Region
#Region "Commpon path function"
        ''' <summary>Directory information of <see cref="IO.Path"/></summary>
        ''' <returns>A <see cref="System.String"/> containing directory information for path, or null if path denotes a root directory, is the empty string (""), or is null. Returns <see cref="System.String.Empty"/> if path does not contain directory information.</returns>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> contains invalid characters, is empty, or contains only white spaces, or contains a wildcard character.</exception>
        ''' <exception cref="System.IO.PathTooLongException"><see cref="Path.Path"/> is longer than the system-defined maximum length</exception>
        ''' <remarks>
        ''' <para><see cref="ArgumentException"/> caused by empty, null or invalid <see cref="Path.Path"/> should occure only due to wildcart, because other cases are made impossible by setter of <see cref="Path.Path"/></para>
        ''' <para>See also<seealso cref="System.IO.Path.GetDirectoryName"/></para>
        ''' </remarks>
        Public ReadOnly Property DirectoryName() As String
            Get
                Return System.IO.Path.GetDirectoryName(Path)
            End Get
        End Property
        ''' <summary>Returns the extension of the <see cref="IO.Path"/>.</summary>
        ''' <returns>A <see cref="System.String"/> containing the extension of the specified path (including the "."), null, or <see cref="System.String.Empty"/>. If <see cref="Path.Path"/> is null, <see cref="Extension"/> returns null. If <see cref="Path.Path"/> does not have extension information, <see cref="Extension"/> returns <see cref="String.Empty"/>.</returns>
        ''' <value>The new extension (with a leading period). Specify null to remove an existing extension from <see cref="IO.Path"/></value>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> contains one or more of the invalid characters defined in <see cref="System.IO.Path.InvalidPathChars"/>, or contains a wildcard character.</exception>
        ''' <remarks>
        ''' <para><see cref="ArgumentException"/> caused by empty, null or invalid <see cref="Path.Path"/> should occure only due to wildcart, because other cases are made impossible by setter of <see cref="Path.Path"/></para>
        ''' <para>See also <seealso cref="System.IO.Path.GetExtension"/>, <seealso cref="System.IO.Path.ChangeExtension"/></para>
        ''' </remarks>
        Public Property Extension() As String
            Get
                Return System.IO.Path.GetExtension(Path)
            End Get
            Set(ByVal value As String)
                Path = System.IO.Path.ChangeExtension(Path, value)
            End Set
        End Property
        ''' <summary>Returns the file name and extension of the <see cref="IO.Path"/>.</summary>
        ''' <returns>A <see cref="System.String"/> consisting of the characters after the last directory character in <see cref="Path.Path"/>. If the last character of <see cref="Path.Path"/> is a directory or volume separator character, this property returns <see cref="System.String.Empty"/>. If <see cref="Path.Path"/> is null, this method returns null.</returns>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> contains one or more of the invalid characters defined in <see cref="System.IO.Path.InvalidPathChars"/>, or contains a wildcard character.</exception>
        ''' <remarks>
        ''' <para><see cref="ArgumentException"/> caused by empty, null or invalid <see cref="Path.Path"/> should occure only due to wildcart, because other cases are made impossible by setter of <see cref="Path.Path"/></para>
        ''' <para>See also <seealso cref="System.IO.Path.GetFileName"/></para>
        ''' </remarks>
        Public ReadOnly Property FileName() As String
            Get
                Return System.IO.Path.GetFileName(Path)
            End Get
        End Property
        ''' <summary>Returns the file name of the <see cref="IO.Path"/> without the extension.</summary>
        ''' <returns>A <see cref="System.String"/> containing the string returned by <see cref="FileName"/>, minus the last period (.) and all characters following it.</returns>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> contains one or more of the invalid characters defined in <see cref="System.IO.Path.InvalidPathChars"/>, or contains a wildcard character.</exception>
        ''' <remarks>
        ''' <para><see cref="ArgumentException"/> caused by empty, null or invalid <see cref="Path.Path"/> should occure only due to wildcart, because other cases are made impossible by setter of <see cref="Path.Path"/></para>
        ''' <para>See also <seealso cref="System.IO.Path.GetFileNameWithoutExtension"/></para>
        ''' </remarks>
        Public ReadOnly Property FileNameWithoutExtension() As String
            Get
                Return System.IO.Path.GetFileNameWithoutExtension(Path)
            End Get
        End Property
        ''' <summary>Returns the absolute path for the <see cref="IO.Path"/>.</summary>
        ''' <returns>A string containing the fully qualified location of <see cref="IO.Path"/>, such as <example>"C:\MyFile.txt"</example>.</returns>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permissions.</exception>
        ''' <exception cref="System.ArgumentNullException"><see cref="Path.Path"/> is null.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>, or contains a wildcard character.-or- The system could not retrieve the absolute path.</exception>
        ''' <exception cref="System.NotSupportedException"><see cref="Path.Path"/> contains a colon (":").</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters</exception>
        ''' <remarks>
        ''' <para>Looks at file system</para>
        ''' <para><see cref="ArgumentException"/> caused by empty, or invalid <see cref="Path.Path"/> should occure only due to wildcart, because other cases are made impossible by setter of <see cref="Path.Path"/>, <see cref="ArgumentNullException"/> should not occure due to same reason.</para>
        ''' <para>See also <seealso cref="System.IO.Path.GetFullPath"/></para>
        ''' </remarks>
        Public Function GetFullPath() As String
            Return System.IO.Path.GetFullPath(Path)
        End Function
        ''' <summary>Gets value indicating wheather <see cref="Path.Path"/> contains¨character not allowed in path as defined by <see cref="System.IO.Path.GetInvalidPathChars"/></summary>
        ''' <returns>Should always retrun false because invalid path chars are vlocked by the <see cref="Path.Path"/> setter</returns>
        ''' <exception cref="NullReferenceException"><see cref="Path.Path"/> is Null - Should not occure because null values fo <see cref="Path.Path"/> are invalid</exception>
        Public ReadOnly Property ContainsInvalidPathChar() As Boolean
            Get
                For Each ch As Char In System.IO.Path.GetInvalidPathChars
                    If Path.Contains(ch) Then Return True
                Next ch
                Return False
            End Get
        End Property
        ''' <summary>Gets the root directory information of the <see cref="io.Path"/>.</summary>
        ''' <returns>A string containing the root directory of <see cref="IO.Path"/>, such as "C:\", or null if path is null, or an empty string if path does not contain root directory information.</returns>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> contains one or more of the invalid characters defined in <see cref="System.IO.Path.InvalidPathChars"/>, or contains a wildcard character.-or- <see cref="System.String.Empty"/> was passed to path. <para>This exception should occure only due to wildcards because other invalid values or blocked by <see cref="Path.Path"/>'s setter.</para></exception>
        ''' <remarks>See also <seealso cref="System.IO.Path.GetPathRoot"/></remarks>
        Public ReadOnly Property PathRoot() As String
            Get
                Return System.IO.Path.GetPathRoot(Path)
            End Get
        End Property
        ''' <summary>Returns random path</summary>
        ''' <remarks>See also <seealso cref="System.IO.Path.GetRandomFileName"/></remarks>
        Public Shared Function Random() As Path
            Return New Path(System.IO.Path.GetRandomFileName)
        End Function
        ''' <summary>Returns path of a uniquely named, zero-byte temporary file on disk</summary>
        ''' <exception cref="System.IO.IOException">An I/O error occurs, such as no unique temporary file name is available.- or -This method was unable to create a temporary file.</exception>
        ''' <remarks>See also <seealso cref="System.IO.Path.GetTempFileName"/></remarks>
        Public Shared Function TempFile() As Path
            Return New Path(System.IO.Path.GetTempFileName)
        End Function
        ''' <summary>Determines whether a path includes a file name extension.</summary>
        ''' <returns>true if the characters that follow the last directory separator (\\ or /) or volume separator (:) in the path include a period (.) followed by one or more characters; otherwise, false.</returns>
        ''' <exception cref="System.ArgumentException">path contains one or more of the invalid characters defined in <see cref="System.IO.Path.InvalidPathChars"/>, or contains a wildcard character.<para>This exception should occure only due to wildcards because another invalid characters are blocked by the <see cref="Path.Path"/> setter</para></exception>
        ''' <remarks>See also <seealso cref="System.IO.Path.[HasExtension]"/></remarks>
        Public ReadOnly Property HasExtension() As Boolean
            Get
                Return System.IO.Path.HasExtension(Path)
            End Get
        End Property
        ''' <summary>Gets a value indicating whether the specified <see cref="IO.Path"/> contains absolute or relative path information.</summary>
        ''' <returns>true if <see cref="IO.Path"/> contains an absolute path; otherwise, false.</returns>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> contains one or more of the invalid characters defined in <see cref="System.IO.Path.InvalidPathChars"/>, or contains a wildcard character.<para>This exception should occure only due to wildcards becouse another invalid characters are blocked by the <see cref="Path.Path"/> setter.</para></exception>
        ''' <remarks>See also <seealso cref="System.IO.Path.IsPathRooted"/></remarks>
        Public ReadOnly Property IsRooted() As Boolean
            Get
                Return System.IO.Path.IsPathRooted(Path)
            End Get
        End Property
#End Region
#Region "Special paths"
        ''' <summary>Returns various system path as instance of <see cref="IO.Path"/></summary>
        Public Class SystemPaths
            ''' <summary>Protected CTor</summary>
            ''' <remarks>Protected in order not this class to be instantiable but in order to be deriveable</remarks>
            Protected Sub New()
            End Sub
            ''' <summary>Returns the path of the current users's temporary folder.</summary>
            ''' <exception cref="System.Security.SecurityException">The caller does not have the required permissions</exception>
            Public Shared ReadOnly Property Temp() As Path
                Get
                    Return Global.System.IO.Path.GetTempPath
                End Get
            End Property
            ''' <summary>Gets path of directory specific for current application taht should be used to store shared data</summary>
            ''' <remarks>This is application-specific directory</remarks>
            ''' <exception cref="DirectoryNotFoundException">Path is empty usually because the operation systems doesn't support the directory</exception>
            Public Shared ReadOnly Property AllUserAppDataCurrent() As Path
                Get
                    Return My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData
                End Get
            End Property
            ''' <summary>Gets path of directory specific for current application taht should be used to store user specific data</summary>
            ''' <remarks>This is application-specific directory</remarks>
            ''' <exception cref="DirectoryNotFoundException">Path is empty usually because the operation systems doesn't support the directory</exception>
            Public Shared ReadOnly Property UserAppDataCurrent() As Path
                Get
                    Return My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData
                End Get
            End Property
            ''' <summary>Gets path of Desktop of current user</summary>
            ''' <exception cref="DirectoryNotFoundException">Path is empty usually because the operation systems doesn't support the directory</exception>
            Public Shared ReadOnly Property Desktop() As Path
                Get
                    Return My.Computer.FileSystem.SpecialDirectories.Desktop
                End Get
            End Property
            ''' <summary>Gets path of current user's Documents directory</summary>
            ''' <exception cref="DirectoryNotFoundException">Path is empty usually because the operation systems doesn't support the directory</exception>
            Public Shared ReadOnly Property Documments() As Path
                Get
                    Return My.Computer.FileSystem.SpecialDirectories.MyDocuments
                End Get
            End Property
            ''' <summary>Gets path of My Music directory of current user</summary>
            ''' <exception cref="DirectoryNotFoundException">Path is empty usually because the operation systems doesn't support the directory</exception>
            Public Shared ReadOnly Property Music() As Path
                Get
                    Return My.Computer.FileSystem.SpecialDirectories.MyMusic
                End Get
            End Property
            ''' <summary>Gets path of MyMy Pictures directory of current user</summary>
            ''' <exception cref="DirectoryNotFoundException">Path is empty usually because the operation systems doesn't support the directory</exception>
            Public Shared ReadOnly Property Pictures() As Path
                Get
                    Return My.Computer.FileSystem.SpecialDirectories.MyPictures
                End Get
            End Property
            ''' <summary>Gets path of the Program Files directory</summary>
            ''' <exception cref="DirectoryNotFoundException">Path is empty usually because the operation systems doesn't support the directory</exception>
            Public Shared ReadOnly Property ProgramFiles() As Path
                Get
                    Return My.Computer.FileSystem.SpecialDirectories.ProgramFiles
                End Get
            End Property
            ''' <summary>Gets path of Programs directory (In the Start menu) of current user</summary>
            ''' <exception cref="DirectoryNotFoundException">Path is empty usually because the operation systems doesn't support the directory</exception>
            Public Shared ReadOnly Property Programs() As Path
                Get
                    Return My.Computer.FileSystem.SpecialDirectories.Programs
                End Get
            End Property
            ''' <summary>Gets path of operating system directory (typically C:\Windows)</summary>
            Public Shared ReadOnly Property OS() As Path
                Get
                    Return Global.System.IO.Path.GetDirectoryName(Environment.SystemDirectory)
                End Get
            End Property
            ''' <summary>Gets path of system directory (typically C:\Windows\System32)</summary>
            Public Shared ReadOnly Property System() As Path
                Get
                    Return Environment.SystemDirectory
                End Get
            End Property
            ''' <summary>Gets path of user directory for Cookies</summary>
            Public Shared ReadOnly Property Cookies() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.Cookies)
                End Get
            End Property
            ''' <summary>Gets path of logical desktop (possibly same as <see cref="Desktop"/>)</summary>
            Public Shared ReadOnly Property DesktopLogical() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                End Get
            End Property
            ''' <summary>Gets path of user directory for Favorites</summary>
            Public Shared ReadOnly Property Favorites() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.Favorites)
                End Get
            End Property
            ''' <summary>Gets path of user directory for History</summary>
            Public Shared ReadOnly Property History() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.History)
                End Get
            End Property
            ''' <summary>Gets path of user directory for Internet cache</summary>
            Public Shared ReadOnly Property InternetCache() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.InternetCache)
                End Get
            End Property
            ''' <summary>Gets path of the directory that serves as a common repository for application-specific data that is used by the current, non-roaming user.</summary>
            Public Shared ReadOnly Property ApplicationData() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                End Get
            End Property
            ''' <summary>The directory that serves as a common repository for application-specific data that is used by all users.</summary>
            Public Shared ReadOnly Property AllUsersApplicationData() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
                End Get
            End Property
            ''' <summary>Path of the My Computer</summary>
            ''' <exception cref="ArgumentException"> When path is invalid, eg. empty which is common for Windows systems</exception>
            Public Shared ReadOnly Property MyComputer() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)
                End Get
            End Property
            ''' <summary>Gets path of recent files directory</summary>
            Public Shared ReadOnly Property Recent() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.Recent)
                End Get
            End Property
            ''' <summary>Gets path of sent to directory</summary>
            Public Shared ReadOnly Property SendTo() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.SendTo)
                End Get
            End Property
            ''' <summary>Gets path of Start menu directory</summary>
            Public Shared ReadOnly Property Start() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.StartMenu)
                End Get
            End Property
            ''' <summary>Gets path of startup directory</summary>
            Public Shared ReadOnly Property Startup() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.Startup)
                End Get
            End Property
            ''' <summary>Gets path of templates directory</summary>
            Public Shared ReadOnly Property Templates() As Path
                Get
                    Return Environment.GetFolderPath(Environment.SpecialFolder.Templates)
                End Get
            End Property
        End Class
#End Region
#Region "Extended"
        ''' <summary>Normalizes path to use only one type of directory separators</summary>
        ''' <param name="ToAlternative">If set to true the alternative directory separator (/ on Windows) is used instead of primary (\ on Windows)</param>
        ''' <remarks>See also <seealso cref="System.IO.Path.DirectorySeparatorChar"/>, <seealso cref="System.IO.Path.AltDirectorySeparatorChar"/></remarks>
        Public Sub Normalize(Optional ByVal ToAlternative As Boolean = False)
            '#If Framework >= 3.5 Then
            Dim Old As Char = If(ToAlternative, System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar)
            Dim [New] As Char = If(ToAlternative, System.IO.Path.AltDirectorySeparatorChar, System.IO.Path.DirectorySeparatorChar)
            '#Else
            '            Dim Old As Char = iif(ToAlternative, System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar)
            '            Dim [New] As Char = iif(ToAlternative, System.IO.Path.AltDirectorySeparatorChar, System.IO.Path.DirectorySeparatorChar)
            '#End If
            Path = Path.Replace(Old, [New])
        End Sub
        ''' <summary>Gets segmnents (directories) of path</summary>
        ''' <returns>Normalized (see <see cref="Normalize"/>) paht's <see cref="Path.Path"/> splitted by <see cref="System.IO.Path.DirectorySeparatorChar"/></returns>
        ''' <remarks>In order to be able co re-construct path in <see cref="Join"/> function the paths is splitted by all occurences of the <see cref="System.IO.Path.DirectorySeparatorChar"/>. This means that path like \\Dzonny\C\ hase 5 elements ("","","Dzonny","C","")</remarks>
        Public ReadOnly Property Segments() As String()
            Get
                Dim Path As New Path(Me)
                Path.Normalize()
                Return Path.Path.Split(System.IO.Path.DirectorySeparatorChar)
            End Get
        End Property
        ''' <summary>Removes <paramref name="Levels"/> parts from end of path</summary>
        ''' <param name="Levels">Number of levels to be removed</param>
        ''' <remarks>Note that if path ends with <see cref="System.IO.Path.DirectorySeparatorChar"/> or <see cref="System.IO.Path.AltDirectorySeparatorChar"/> (Like C:/Temp/) then result is only removal of this char (C:\Temp)</remarks>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Levels"/> is less then zero -or- <paramref name="Levels"/> is greater or equal to number of segments in current path</exception>
        Public Sub Up(Optional ByVal Levels As Integer = 1)
            If Levels = 0 Then Exit Sub
            If Levels < 0 Then Throw New ArgumentOutOfRangeException("Levels", String.Format(ResourcesT.Exceptions.ShouldBeGreaterThanZero, "Levels"))
            Dim Segments As String() = Me.Segments
            If Levels >= Segments.Length Then Throw New ArgumentOutOfRangeException("Levels", String.Format(ResourcesT.Exceptions.ThePathSDepthIsNotEnoughToRemove0Levels, Levels))
            Dim NewArr(Segments.Length - Levels - 1) As String
            Array.ConstrainedCopy(Segments, 0, NewArr, 0, NewArr.Length)
            Me.Path = IOt.Path.Join(NewArr).Path
        End Sub
        ''' <summary>Removes <paramref name="Levels"/> parts from end of <paramref name="Path"/></summary>
        ''' <param name="Levels">Number of levels to be removed</param>
        ''' <param name="Path">The <see cref="IO.Path"/> to remove levels from</param>
        ''' <returns>New instance of <see cref="IO.Path"/> with segments removed</returns>
        ''' <remarks>Note that if path ends with <see cref="System.IO.Path.DirectorySeparatorChar"/> or <see cref="System.IO.Path.AltDirectorySeparatorChar"/> (Like C:/Temp/) then result is only removal of this char (C:\Temp)</remarks>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Levels"/> is less then zero -or- <paramref name="Levels"/> is greater or equal to number of segments in current path</exception>
        Public Shared Operator -(ByVal Path As Path, ByVal Levels As Integer) As Path
            Dim p As New Path(Path)
            p.Up(Levels)
            Return p
        End Operator
        ''' <summary>Number of segments of current path</summary>
        ''' <remarks>Note that number of segments is such path on Windows \\Dzonny\C\ if 5. See <see cref="Segments"/></remarks>
        Public ReadOnly Property Depth() As Integer
            Get
                Return Me.Segments.Length
            End Get
        End Property
        ''' <summary>Creates path from its segments</summary>
        ''' <param name="Segments">Parts to maked path of</param>
        ''' <exception cref="ArgumentNullException">Segments is null</exception>
        Public Shared Function Join(ByVal Segments As IEnumerable(Of String)) As Path
            If Segments Is Nothing Then Throw New ArgumentNullException("Segments")
            Dim ret As New System.Text.StringBuilder
            Dim i As Integer = 0
            For Each Part As String In Segments
                If i > 0 Then ret.Append(System.IO.Path.DirectorySeparatorChar)
                ret.Append(Part)
                i = +1
            Next Part
            Return New Path(ret.ToString)
        End Function
        ''' <summary>Creates all directories and subdirectories as specified by <see cref="IO.Path"/>.</summary>
        ''' <returns>A <see cref="System.IO.DirectoryInfo"/> as specified by <see cref="IO.Path"/>.</returns>
        ''' <exception cref="System.ArgumentNullException"><see cref="Path.Path"/> is null. Shouldn't occure.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.IO.IOException">The directory specified by path is read-only or is not empty.-or-A file with the same name and location specified by <see cref="IO.Path"/> exists.</exception>
        ''' <exception cref="System.NotSupportedException">An attempt was made to create a directory with only the colon character (:).</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>. Should not occure.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names must be less than 260 characters.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        Public Function CreateDirectory() As DirectoryInfo
            Return System.IO.Directory.CreateDirectory(Path)
        End Function
#End Region
#Region "Comparison"
        ''' <summary>Gets value indicating if <paramref name="Child"/> is child address of <paramref name="Parent"/></summary>
        ''' <param name="Parent">Possibly parent address</param>
        ''' <param name="Child">Possibly child address</param>
        ''' <returns>True if <paramref name="Child"/> is subdirecrory of <paramref name="Parent"/> or file lying there</returns>
        ''' <remarks>This operator works with no touch to file system - only string comparison is done</remarks>
        Public Shared Operator >(ByVal Parent As Path, ByVal Child As Path) As Boolean
            If Parent Is Nothing Then Throw New ArgumentNullException("Parent")
            If Child Is Nothing Then Throw New ArgumentNullException("Child")
            Dim ParentSegments As String() = Parent.Segments
            Dim ChildSegments As String() = Child.Segments
            If ParentSegments.Length >= ChildSegments.Length Then Return False
            For i As Integer = 0 To ParentSegments.Length - 1
                If ParentSegments(i).ToLower <> ChildSegments(i).ToLower Then Return False
            Next i
            Return True
        End Operator
        ''' <summary>Gets value indicating if <paramref name="Child"/> is child address of <paramref name="Parent"/></summary>
        ''' <param name="Parent">Possibly parent address</param>
        ''' <param name="Child">Possibly child address</param>
        ''' <returns>True if <paramref name="Child"/> is subdirecrory of <paramref name="Parent"/> or file lying there</returns>
        ''' <remarks>This operator works with no touch to file system - only string comparison is done</remarks>
        Public Shared Operator <(ByVal Child As Path, ByVal Parent As Path) As Boolean
            Return Parent > Child
        End Operator
        ''' <summary>Compares two <see cref="Path"/>s for equivalence</summary>
        ''' <param name="a">First path to be combared</param>
        ''' <param name="b">Second path to be compared</param>
        ''' <returns>True if both paths are equivalent</returns>
        ''' <remarks>Only string comparison is done</remarks>
        Public Shared Operator =(ByVal a As Path, ByVal b As Path) As Boolean
            If a Is Nothing AndAlso b Is Nothing Then
                Return True
            ElseIf a Is Nothing OrElse b Is Nothing Then
                Return False
            ElseIf a Is b Then
                Return True
            Else
                Dim ParentSegments As String() = a.Segments
                Dim ChildSegments As String() = b.Segments
                If ParentSegments.Length <> ChildSegments.Length Then Return False
                For i As Integer = 0 To ParentSegments.Length - 1
                    If ParentSegments(i).ToLower <> ChildSegments(i).ToLower Then Return False
                Next i
                Return True
            End If
        End Operator
        ''' <summary>Compares two <see cref="Path"/>s for inequivalence</summary>
        ''' <param name="a">First path to be combared</param>
        ''' <param name="b">Second path to be compared</param>
        ''' <returns>True if path aren't equivalent</returns>
        ''' <remarks>Only string comparison is done</remarks>
        Public Shared Operator <>(ByVal a As Path, ByVal b As Path) As Boolean
            Return Not a = b
        End Operator

        ''' <summary>Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="IO.Path"/>.</summary>
        ''' <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="IO.Path"/>.</param>
        ''' <returns>True if <paramref name="obj"/> represents <see cref="Path"/> or <see cref="String"/> equivalent to current instance or </returns>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is Path Then
                Return Me = DirectCast(obj, Path)
            ElseIf TypeOf obj Is String Then
                Return Me = CType(DirectCast(obj, String), Path)
            Else
                Return MyBase.Equals(obj)
            End If
        End Function
        ''' <summary>Indicates wheather current instance is sub-path of passed instance</summary>
        ''' <param name="Parent">Possible parent of current instace</param>
        ''' <returns>True if <paramref name="Parent"/> is parent of current instance</returns>
        Public Function IsChildOf(ByVal Parent As Path) As Boolean
            Return Me < Parent
        End Function
        ''' <summary>Indicates wheather current instance is parent of passed instance</summary>
        ''' <param name="Child">Posiible child of current instance</param>
        ''' <returns>True if <paramref name="Child"/> is sub-path of current instace</returns>
        Public Function IsparentOf(ByVal Child As Path) As Boolean
            Return Me > Child
        End Function
#End Region
#Region "FileSystem"
        ''' <summary>Checks wheather path represented by current instance exists and is directory</summary>
        Public ReadOnly Property IsDirectory() As Boolean
            Get
                Return Directory.Exists(Path)
            End Get
        End Property
        ''' <summary>Gets directory represented by current instance (if exists)</summary>
        ''' <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. The specified path, file name, or both are too long.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> contains invalid characters such as ", &lt;, >, or |. This exception should not occure because invalid characters are filtered by the <see cref="Path.Path"/> setter</exception>
        ''' <exception cref="System.ArgumentNullException"><seealso cref="Path.Path"/> is null. Should not occure.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission</exception>
        Public Function GetDirectory() As DirectoryInfo
            Return New DirectoryInfo(Path)
        End Function
        ''' <summary>Check wheather path represented by current instace exists and is file</summary>
        Public ReadOnly Property IsFile() As Boolean
            Get
                Return File.Exists(Path)
            End Get
        End Property
        ''' <summary>Gets file represented by current instance (if exists)</summary>
        ''' <exception cref="System.UnauthorizedAccessException">Access to file is denied.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentNullException"><see cref="Path.Path"/> is null. Should not occure.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        ''' <exception cref="System.NotSupportedException"><see cref="Path.Path"/> contains a colon (:) in the middle of the string.</exception>
        ''' <exception cref="System.ArgumentException">The <see cref="Path.Path"/> is empty, contains only white spaces, or contains invalid characters. Should not occure because the <see cref="Path.Path"/> setter blocks such values.</exception>
        Public Function GetFile() As FileInfo
            Return New FileInfo(Path)
        End Function
        ''' <summary>Checks wheather path represented by current instance exists</summary>
        ''' <returns>True if <see cref="IsFile"/> or <see cref="IsDirectory"/> returns True</returns>
        Public ReadOnly Property Exists() As Boolean
            Get
                Return IsFile OrElse IsDirectory
            End Get
        End Property
#End Region
#Region "Manipulation"
        ''' <summary>Deletes item represented by <see cref="IO.Path"/> Directories are deleted recursivelly</summary>
        ''' <exception cref="System.IO.IOException">The specified file is in use.</exception>
        ''' <exception cref="NotSupportedException">path is in an invalid format.</exception>
        ''' <exception cref="UnauthorizedAccessException">The caller does not have the required permission. -or- path specified a read-only file.</exception>
        Public Sub Delete()
            If Me.IsFile Then
                System.IO.File.Delete(Path)
            ElseIf Me.IsDirectory Then
                System.IO.Directory.Delete(Path, True)
            End If
        End Sub
        ''' <summary>Copies file or directory from one location to another</summary>
        ''' <param name="Target">Target location</param>
        ''' <param name="SkipErrors">Applies only when copying directory. Suppresses exceptions occuring when copying individual items. It may result not exact copy.</param>
        ''' <returns>When copying file always True, when copying directory and <paramref name="SkipErrors"/> is True returns true if no error occured during procces if <paramref name="SkipErrors"/> is false returns True or throws an exception.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Target"/> is null</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The path specified in <paramref name="Target"/> is invalid (for example, it is on an unmapped drive).</exception>
        ''' <exception cref="System.ArgumentException"><paramref name="Target"/> specifies a directory when copying file</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.NotSupportedException"><see cref="Path.Path"/> of curent instance or of <paramref name="Target"/> is in an invalid format when copying file -or-
        ''' An attempt was made to create a directory with only the colon character (:) when coping directory.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error has occurred when copying file. -or-
        ''' The directory specified by <paramref name="Target"/> or one of its subdirectories is read-only or is not empty or a file with the same name and location specified by <paramref name="Target"/> or one of its subdirectories exists while copying directory.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        ''' <exception cref="System.IO.FileNotFoundException">Source file was not found.</exception>
        Public Function Copy(ByVal Target As Path, Optional ByVal SkipErrors As Boolean = False) As Boolean
            If Target Is Nothing Then Throw New ArgumentNullException("Target")
            If Me.IsFile Then
                If Target.IsFile Then Target.Delete()
                System.IO.File.Copy(Path, Target)
                Return True
            ElseIf Me.IsDirectory Then
                Dim ret As Boolean = True
                If Not Target.IsDirectory Then
                    Try : System.IO.Directory.CreateDirectory(Target)
                    Catch : If Not SkipErrors Then Throw Else ret = False
                    End Try
                End If
                For Each file As Path In System.IO.Directory.GetFiles(Path, "*", SearchOption.TopDirectoryOnly)
                    Try : ret = ret And file.Copy(Target + file.FileName, SkipErrors)
                    Catch : If Not SkipErrors Then Throw Else ret = False
                    End Try
                Next file
                For Each dir As Path In System.IO.Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly)
                    Try : ret = ret And dir.Copy(Target + dir.FileName, SkipErrors)
                    Catch : If Not SkipErrors Then Throw Else ret = False
                    End Try
                Next dir
                Return ret
            Else
                Throw New FileNotFoundException(ResourcesT.Exceptions.SourceFileOrDirectoryNotFound, Path)
            End If
        End Function
        ''' <summary>Moves file or directory from one location to another</summary>
        ''' <param name="Target">Target location</param>
        ''' <param name="SkipErrors">Applies only when moving directory. Suppresses exceptions occuring when moving individual items. It may result not exact copy.</param>
        ''' <returns>When moving file always True, when moving directory and <paramref name="SkipErrors"/> is True returns true if no error occured during procces if <paramref name="SkipErrors"/> is false returns True or throws an exception.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Target"/> is null</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The path specified in <paramref name="Target"/> is invalid (for example, it is on an unmapped drive).</exception>
        ''' <exception cref="System.ArgumentException"><paramref name="Target"/> specifies a directory when copying file</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.NotSupportedException"><see cref="Path.Path"/> of curent instance or of <paramref name="Target"/> is in an invalid format when copying file -or-
        ''' An attempt was made to create a directory with only the colon character (:) when coping directory.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error has occurred when copying file. -or-
        ''' The directory specified by <paramref name="Target"/> or one of its subdirectories is read-only or is not empty or a file with the same name and location specified by <paramref name="Target"/> or one of its subdirectories exists while copying directory.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        ''' <exception cref="System.IO.FileNotFoundException">Sorce file was not found.</exception>
        Public Function Move(ByVal Target As Path, Optional ByVal skiperrors As Boolean = False) As Boolean
            If Target Is Nothing Then Throw New ArgumentNullException("Target")
            If Me.IsFile Then
                If Target.IsFile Then Target.Delete()
                System.IO.File.Move(Path, Target)
                Return True
            ElseIf Me.IsDirectory Then
                Dim ret As Boolean = True
                If Not Target.IsDirectory Then
                    Try : System.IO.Directory.CreateDirectory(Target)
                    Catch : If skiperrors Then ret = False Else Throw
                    End Try
                End If
                For Each file As Path In System.IO.Directory.GetFiles(Path, "*", SearchOption.TopDirectoryOnly)
                    Try : ret = ret And file.Move(Path + file.FileName)
                    Catch : If Not skiperrors Then Throw Else ret = False
                    End Try
                Next file
                For Each dir As Path In System.IO.Directory.GetDirectories(Path, "*", SearchOption.TopDirectoryOnly)
                    Try : ret = ret And dir.Move(Path + dir.FileName)
                    Catch : If Not skiperrors Then Throw Else ret = False
                    End Try
                Next dir
            Else
                Throw New FileNotFoundException(ResourcesT.Exceptions.SourceFileOrDirectoryNotFound, Path)
            End If
        End Function
#End Region

        ''' <summary>Returns an enumerator that iterates through the all files and folder under current folder recursivelly. Current folder is included in enumeration. Files are listed before folders at each level.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the file system recursivelly.</returns>
        ''' <remarks>If current instance points to file instead of folder then return enumerator thet enumerates throught array of exactly one item - current path; otherwise <see cref="FileSystemEnumerator"/> is returned.</remarks>
        Public Overloads Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of Path) Implements System.Collections.Generic.IEnumerable(Of Path).GetEnumerator
            Return GetEnumerator(False)
        End Function
        ''' <summary>Returns an enumerator that iterates through the all files and folders under current folder recursivelly. Current folder is not included in enumeration.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterates through the file system recursivelly.</returns>
        ''' <param name="FoldersFirst">True to list Folders before files at each level.</param>
        ''' <remarks>If current instance points to file instead of folder then return enumerator thet enumerates throught array of exactly one item - current path; otherwise <see cref="FileSystemEnumerator"/> is returned.</remarks>
        Public Overloads Function GetEnumerator(ByVal FoldersFirst As Boolean) As IEnumerator(Of Path)
            If IsDirectory Then
                Return New FileSystemEnumerator(Me.Path, FoldersFirst)
            Else
                Return New Path() {New Path(Me)}.GetEnumerator
            End If
        End Function

        ''' <summary>Returns an enumerator that iterates through the file system.</summary>
        ''' <returns><see cref="M:Tools.IOt.Path.GetEnumerator()"/></returns>
        ''' <remarks>Use type-safe <see cref="M:Tools.IOt.Path.GetEnumerator()"/>instead</remarks>
        <Obsolete("Use type safe GetEnumerator instead")> _
        Private Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function
#Region "GetChildren"
#Region "Simple"
        ''' <summary>Gets all files and folders in this folder</summary>
        ''' <returns>Files and folders in folder represented by this instance</returns>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        Public Function GetChildren() As IEnumerable(Of Path)
            Return From strpath In IO.Directory.GetFiles(Me.Path).Union(IO.Directory.GetDirectories(Me.Path)) Select New Path(strpath)
        End Function
        ''' <summary>Gets all files in this folder</summary>
        ''' <returns>Files in folder represented by this instance</returns>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        Public Function GetFiles() As IEnumerable(Of Path)
            Return GetChildren(Function(p As Path) p.IsFile)
        End Function
        ''' <summary>Gets all folders in this folder</summary>
        ''' <returns>Folders in folder represented by this instance</returns>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        Public Function GetSubFolders() As IEnumerable(Of Path)
            Return GetChildren(Function(p As Path) p.IsDirectory)
        End Function
#End Region
        ''' <summary>Gets all files and folders in this folder restricted by AND operation among given conditions</summary>
        ''' <param name="Conditions">Conditions to be evaluated and ANDed</param>
        ''' <returns>Files and folders in folder represented by this instance</returns>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Function GetChildren(ByVal ParamArray Conditions As Func(Of Path, Boolean)()) As IEnumerable(Of Path)
            Return From strpath In IO.Directory.GetFiles(Me.Path).Union(IO.Directory.GetDirectories(Me.Path)) Select Path = New Path(strpath) Where Conditions.And(Path)
        End Function
#Region "Mask"
        ''' <summary>Gets all files and folders in this folder restricted by mask</summary>
        ''' <returns>Files and folders in folder represented by this instance</returns>
        ''' <param name="Mask">Mask used to restrict files and folders. The mask is in format of <see cref="Microsoft.VisualBasic.CompilerServices.StringType.StrLikeText">Visual Basic operator Like</see> and is case-insensitive</param>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        Public Function GetChildren(ByVal Mask As String) As IEnumerable(Of Path)
            Return GetChildren(Function(p As Path) LikeFS(p.FileName, Mask))
        End Function
        ''' <summary>Gets all files in this folder restricted by mask</summary>
        ''' <returns>Files in folder represented by this instance</returns>
        ''' <param name="Mask">Mask used to restrict files. The mask is in format of <see cref="Microsoft.VisualBasic.CompilerServices.StringType.StrLikeText">Visual Basic operator Like</see> and is case insensitive</param>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        Public Function GetFiles(ByVal Mask As String) As IEnumerable(Of Path)
            Return GetChildren(Function(p As Path) p.IsFile, Function(p As Path) LikeFS(p.FileName, Mask))
        End Function
        ''' <summary>Gets all subfolders in this folder restricked by mask</summary>
        ''' <returns>Files in folder represented by this instance</returns>
        ''' <param name="Mask">Mask used to restrict folders. The mask is in format of <see cref="Microsoft.VisualBasic.CompilerServices.StringType.StrLikeText">Visual Basic operator Like</see> and is case insensitive</param>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        Public Function GetSubFolders(ByVal Mask As String) As IEnumerable(Of Path)
            Return GetChildren(Function(p As Path) p.IsDirectory, Function(p As Path) LikeFS(p.FileName, Mask))
        End Function
#End Region
#Region "Condition"
        ''' <summary>Gets all files and folders in this folder restricted by condition</summary>
        ''' <returns>Files and folders in folder represented by this instance</returns>
        ''' <param name="Condition">Condition to be avaluated for each child. Only those children for which the condition is true are returned.</param>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        Public Function GetChildren(ByVal Condition As Func(Of Path, Boolean)) As IEnumerable(Of Path)
            Return GetChildren(New Func(Of Path, Boolean)() {Condition})
        End Function
        ''' <summary>Gets all files in this folder restricted by condition</summary>
        ''' <returns>Files in folder represented by this instance</returns>
        ''' <param name="Condition">Condition to be avaluated for each file. Only those files for which the condition is true are returned.</param>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        Public Function GetFiles(ByVal Condition As Func(Of Path, Boolean)) As IEnumerable(Of Path)
            Return GetChildren(Function(p As Path) p.IsFile, Condition)
        End Function
        ''' <summary>Gets all subfolders in this folder restricked by condition</summary>
        ''' <param name="Condition">Condition to be avaluated for each subfolder. Only those subfolders for which the condition is true are returned.</param>
        ''' <returns>Files in folder represented by this instance</returns>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        Public Function GetSubFolders(ByVal Condition As Func(Of Path, Boolean)) As IEnumerable(Of Path)
            Return GetChildren(Function(p As Path) p.IsDirectory, Condition)
        End Function
#End Region
#Region "Masks"
        ''' <summary>Gets all files and folders in this folder restricted by mask</summary>
        ''' <returns>Files and folders in folder represented by this instance</returns>
        ''' <param name="Masks">Masks used to restrict files and folders. The mask is in format of <see cref="Microsoft.VisualBasic.CompilerServices.StringType.StrLikeText">Visual Basic operator Like</see> and is case insensitive. The OR operation is pefromed between all masks.</param>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        Public Function GetChildren(ByVal ParamArray Masks As String()) As IEnumerable(Of Path)
            Return GetChildren(Function(p As Path) (From m In Masks Select LikeFS(p.Path, m)).Or)
        End Function
        ''' <summary>Gets all files in this folder restricted by masks</summary>
        ''' <returns>Files in folder represented by this instance</returns>
        ''' <param name="Masks">Masks used to restrict files. The mask is in format of <see cref="Microsoft.VisualBasic.CompilerServices.StringType.StrLikeText">Visual Basic operator Like</see> and is case insinsitive. The OR operation is pefromed between all masks.</param>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        Public Function GetFiles(ByVal ParamArray Masks As String()) As IEnumerable(Of Path)
            Return GetChildren(Function(p As Path) p.IsFile, Function(p As Path) (From m In Masks Select LikeFS(p.Path, m)).Or)
        End Function
        ''' <summary>Gets all subfolders in this folder restricked by masks</summary>
        ''' <returns>Files in folder represented by this instance</returns>
        ''' <param name="Masks">Masks used to restrict folders. The mask is in format of <see cref="Microsoft.VisualBasic.CompilerServices.StringType.StrLikeText">Visual Basic operator Like</see> nad is case insensitive. The OR operation is pefromed between all masks.</param>
        ''' <exception cref="System.IO.IOException">This instance represents a file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><see cref="Path.Path"/> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <see cref="Path.Path"/>, name of children, or both exceed the system-defined maximum length.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException"><see cref="Path.Path"/> is invalid (for example, it is on an unmapped drive).</exception>
        Public Function GetSubFolders(ByVal ParamArray Masks As String()) As IEnumerable(Of Path)
            Return GetChildren(Function(p As Path) p.IsDirectory, Function(p As Path) (From m In Masks Select LikeFS(p.Path, m)).Or)
        End Function
#End Region
        ''' <summary>Represents <see cref="Microsoft.VisualBasic.CompilerServices.StringType.StrLikeText">Visual Basic Like operator</see> in Option Compare Text (case insensitive) mode</summary>
        ''' <param name="pattern">Patter to text <paramref name="text"/> against</param>
        ''' <param name="text">Text to test agains <paramref name="pattern"/></param>
        ''' <returns>True or false depending on if <paramref name="text"/> matches <paramref name="pattern"/></returns>
        Private Function LikeFS(ByVal text$, ByVal pattern$) As Boolean
            Return Microsoft.VisualBasic.CompilerServices.StringType.StrLikeText(text, pattern)
        End Function
#End Region
    End Class

    ''' <summary>Interface of object that provides path</summary>
    Public Interface IPathProvider
        ''' <summary>Path of file or directory represented by this object</summary>
        ReadOnly Property Path$()
    End Interface
End Namespace
#End If
