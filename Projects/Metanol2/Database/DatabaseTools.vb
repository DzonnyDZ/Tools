Namespace Data
    ''' <summary>Contains methods for working with database</summary>
    Public Module DatabaseTools
        ''' <summary>Creates new database file at specified path</summary>
        ''' <param name="Path">Path to create database file at</param>
        ''' <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException">sourceFileName or destFileName is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars" />.  -or- <paramref name="Path"/> specifies a directory.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="Path"/> is null.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The path specified is invalid (for example, it is on an unmapped drive).</exception>
        ''' <exception cref="System.IO.FileNotFoundException">File Data\Metanol.mdf in application directory was not found.</exception>
        ''' <exception cref="System.IO.IOException"><paramref name="Path"/> exists.  -or- An I/O error has occurred.</exception>
        ''' <exception cref="System.NotSupportedException"><paramref name="Path"/> is in an invalid format.</exception>
        Public Sub CreateNewDatabaseFile(ByVal Path$)
            IO.File.Copy(IO.Path.Combine(IO.Path.Combine(My.Application.Info.DirectoryPath, "Database"), "Metanol.mdf"), Path)
        End Sub
    End Module
End Namespace