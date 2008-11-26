Imports Tools.MetadataT.ExifT, Tools.DrawingT.DrawingIOt
Public Class ExifInternal
    Inherits Exif
    ''' <summary>CTor</summary>
    ''' <param name="Path">Path of JPEG file</param>
    ''' <exception cref="System.IO.DirectoryNotFoundException">The specified <paramref name="ImagePath"/> is invalid, such as being on an unmapped drive.</exception>
    ''' <exception cref="System.ArgumentNullException"><paramref name="ImagePath"/> is null.</exception>
    ''' <exception cref="System.UnauthorizedAccessException">The access requested (readonly) is not permitted by the operating system for the specified path.</exception>
    ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
    ''' <exception cref="System.ArgumentException"><paramref name="ImagePath"/> is an empty string (""), contains only white space, or contains one or more invalid characters.</exception>
    ''' <exception cref="System.IO.FileNotFoundException">The file cannot be found.</exception>
    ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
    ''' <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
    ''' <exception cref="IO.InvalidDataException">
    ''' Invalid JPEG marker found (code doesn't start with FFh, length set to 0 or 2) -or-
    ''' JPEG stream doesn't start with corect SOI marker -or-
    ''' JPEG stream doesn't end with corect EOI marker -or-
    ''' Invalid byte order mark (other than 'II' or 'MM') at the beginning of stream -or-
    ''' Byte order test (2 bytes next to byte order mark, 3rd and 4th bytes in stream) don't avaluates to value 2Ah
    ''' </exception>
    ''' <exception cref="System.ObjectDisposedException">The source stream is closed.</exception>
    ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached unexpectedly.</exception>
    Friend Sub New(ByVal Path As String)
        MyBase.New(New ExifReader(New JPEG.JPEGReader(Path, False)))
    End Sub

End Class
