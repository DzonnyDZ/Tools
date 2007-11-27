Imports System.ComponentModel

Friend Class SysInfo
    Private Path$
    Private Image As Image
    Public Sub New(ByVal Image As Image, ByVal Path As String)
        Me.Image = Image
        Me.Path = Path
    End Sub
    <Category("File"), DisplayName("File name"), Description("Name of file with extension")> _
    Public ReadOnly Property FileName$()
        Get
            Return IO.Path.GetFileName(Path)
        End Get
    End Property
    <Category("File"), DisplayName("File name without extension"), Description("Name of file without extension")> _
    Public ReadOnly Property FileNameWO$()
        Get
            Return IO.Path.GetFileNameWithoutExtension(Path)
        End Get
    End Property
    <Category("File"), DisplayName("Path"), Description("Full path to file")> _
    Public ReadOnly Property FullPath$()
        Get
            Return Path
        End Get
    End Property
    <Category("File"), DisplayName("Directory"), Description("Directory the file lies in")> _
    Public ReadOnly Property Directory$()
        Get
            Return IO.Path.GetDirectoryName(Path)
        End Get
    End Property
    <Category("File"), DisplayName("Extension"), Description("Extension part of file name")> _
    Public ReadOnly Property Extension$()
        Get
            Return IO.Path.GetExtension(Path)
        End Get
    End Property
    <Category("Times"), DisplayName("Created"), Description("Date and time of file creation")> _
    Public ReadOnly Property Created() As Date
        Get
            Return IO.File.GetCreationTime(Path)
        End Get
    End Property
    <Category("Times"), DisplayName("Created (UTC)"), Description("Date and time of file creation in universal time coordinates")> _
    Public ReadOnly Property CretedUTC() As Date
        Get
            Return IO.File.GetCreationTimeUtc(Path)
        End Get
    End Property
    <Category("Times"), DisplayName("Changed"), Description("Date and time of last change of file")> _
    Public ReadOnly Property Changed() As Date
        Get
            Return IO.File.GetLastWriteTime(Path)
        End Get
    End Property
    <Category("Times"), DisplayName("Changed (UTC)"), Description("Date and time of last change of file in universal time coordinates")> _
    Public ReadOnly Property ChangedUTC() As Date
        Get
            Return IO.File.GetLastWriteTimeUtc(Path)
        End Get
    End Property
    <Category("Times"), DisplayName("Accessed"), Description("Date and time of last access to file")> _
    Public ReadOnly Property Accesed() As Date
        Get
            Return IO.File.GetLastAccessTime(Path)
        End Get
    End Property
    <Category("Times"), DisplayName("Accessed (UTC)"), Description("Date and time of last access to file in universal time cordinates")> _
    Public ReadOnly Property AccesedUTC() As Date
        Get
            Return IO.File.GetLastAccessTimeUtc(Path)
        End Get
    End Property
    <Category("File info"), DisplayName("Attributes"), Description("Attributes of file")> _
    Public ReadOnly Property Attributes() As IO.FileAttributes
        Get
            Return IO.File.GetAttributes(Path)
        End Get
    End Property
    <Category("Size"), DisplayName("Size (B)"), Description("Size of file in bytes")> _
    Public ReadOnly Property Size() As Long
        Get
            Return My.Computer.FileSystem.GetFileInfo(Path).Length
        End Get
    End Property
    <Category("Size"), DisplayName("Size (kB)"), Description("Size of file in kilobytes")> _
    Public ReadOnly Property SizekB() As Single
        Get
            Return Size / 1024
        End Get
    End Property
    <Category("Size"), DisplayName("Size (MB)"), Description("Size of file in megabytes")> _
    Public ReadOnly Property SizeMB() As Single
        Get
            Return SizekB / 1024
        End Get
    End Property
    <Category("Image"), DisplayName("Width"), Description("Width of image in pixels")> _
    Public ReadOnly Property x() As Integer
        Get
            Return Image.Width
        End Get
    End Property
    <Category("Image"), DisplayName("Height"), Description("Height of image in pixels")> _
    Public ReadOnly Property y() As Integer
        Get
            Return Image.Height
        End Get
    End Property
    <Category("Image"), DisplayName("Resolution Y"), Description("Resolution of image in vertical axe")> _
    Public ReadOnly Property ResY() As Integer
        Get
            Return Image.VerticalResolution
        End Get
    End Property
    <Category("Image"), DisplayName("Resolution X"), Description("Resolution of image in horizontal axe")> _
    Public ReadOnly Property ResX() As Integer
        Get
            Return Image.HorizontalResolution
        End Get
    End Property
    <Category("Image"), DisplayName("Format"), Description("Format of image")> _
    Public ReadOnly Property Format() As System.Drawing.Imaging.ImageFormat
        Get
            Return Image.RawFormat
        End Get
    End Property
End Class

