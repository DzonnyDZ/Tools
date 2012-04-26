Imports Tools.ExtensionsT, System.Linq
Imports Tools.ComponentModelT

#If Config <= Nightly Then 'Stage: Nightly

Imports IntDescriptor = Tools.MetadataT.MetadataPropertyDescriptor(Of Tools.MetadataT.ImageMetadata, Integer)
Imports SngDescriptor = Tools.MetadataT.MetadataPropertyDescriptor(Of Tools.MetadataT.ImageMetadata, Single)
Imports PxfDescriptor = Tools.MetadataT.MetadataPropertyDescriptor(Of Tools.MetadataT.ImageMetadata, System.Drawing.Imaging.PixelFormat)

Namespace MetadataT
    ''' <summary>Provides acces to common properties of images exposed by the <see cref="Drawing.Image"/> class using <see cref="IMetadata"/> interface</summary>
    ''' <remarks>This implementation of <see cref="IMetadata"/> supports only named values. Possible values for keys are same as possible values for names.</remarks>
    ''' <version version="1.5.3" stage="Nightly">Class introduced</version>
    ''' <version version="1.5.4">Error messages, property names and descriptions localized.</version>
    ''' <version version="1.5.4">This class now internally uses <see cref="MetadataPropertyDescriptor(Of T)"/></version>
    Public Class ImageMetadata
        Implements IMetadata, IDisposable
        ''' <summary>Image, metadata are provided for</summary>
        Private Image As Drawing.Image
        ''' <summary>True wne <see cref="Image"/> was created in CTor and thus should be destroyed in <see cref="Finalize"/>.</summary>
        Private CreatedImage As Boolean
        ''' <summary>CTor creates a new instance of <see cref="ImageMetadata"/> from image file represented by filename</summary>
        ''' <param name="FileName"></param>
        ''' <exception cref="ArgumentException">The file does not have a valid image format.  -or- GDI+ does not support the pixel format of the file. -or- <paramref name="FileName"/> is a System.Uri.</exception>
        ''' <exception cref="IO.FileNotFoundException">The specified file does not exist.</exception>
        ''' <version version="1.5.4">Parameter name </version>
        Public Sub New(ByVal fileName$)
            Try
                Image = Drawing.Image.FromFile(fileName)
            Catch ex As OutOfMemoryException
                Throw New ArgumentException(ResourcesT.Exceptions.TheImageFileIsInvalid, "fileName", ex)
            End Try
            CreatedImage = True
        End Sub
        ''' <summary>Creates a new empty instance of the <see cref="ImageMetadata"/> class</summary>
        ''' <remarks>Instance created by this constructor cannot be used to obtain image metadata. It can be used only to enumerate supported fields.</remarks>
        ''' <version version="1.5.3">This CTor is new in version 1.5.3</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Sub New()
            Me.New(New Drawing.Bitmap(1, 1))
        End Sub
        ''' <summary>CTor creates new instance of <see cref="ImageMetadata"/> form <see cref="Image"/></summary>
        ''' <param name="image"><see cref="Image"/> reperesneting image to provide metadata for</param>
        ''' <exception cref="ArgumentNullException"><paramref name="image"/> is nul</exception>
        ''' <version version="1.5.4">Parameter <c>Image</c> renamed to <c>image</c></version>
        Public Sub New(ByVal image As Drawing.Image)
            If image Is Nothing Then Throw New ArgumentNullException("image")
            Me.Image = image
            CreatedImage = False
        End Sub
#Region "Properties"
        ''' <summary>Gets the width, in pixels, of this <see cref="System.Drawing.Image" />.</summary>
        ''' <returns>The width, in pixels, of this <see cref="System.Drawing.Image" />.</returns>
        ''' <version version="1.5.4">Added <see cref="LDisplayNameAttribute"/> and <see cref="LDescriptionAttribute"/></version>
        <LDisplayName(GetType(My.Resources.Resources), "n_Width"), LDescription(GetType(My.Resources.Resources), "d_Width")>
        Public ReadOnly Property Width As Integer
            Get
                Return Image.Width
            End Get
        End Property
        ''' <summary>Gets the height, in pixels, of this <see cref="System.Drawing.Image" />.</summary>
        ''' <returns>The height, in pixels, of this <see cref="System.Drawing.Image" />.</returns>
        ''' <version version="1.5.4">Added <see cref="LDisplayNameAttribute"/> and <see cref="LDescriptionAttribute"/></version>
        <LDisplayName(GetType(My.Resources.Resources), "n_Height"), LDescription(GetType(My.Resources.Resources), "d_Height")>
        Public ReadOnly Property Height As Integer
            Get
                Return Image.Height
            End Get
        End Property
        ''' <summary>Gets the horizontal resolution, in pixels per inch, of this <see cref="System.Drawing.Image" />.</summary>
        ''' <returns>The horizontal resolution, in pixels per inch, of this <see cref="System.Drawing.Image" />.</returns>
        ''' <version version="1.5.4">Added <see cref="LDisplayNameAttribute"/> and <see cref="LDescriptionAttribute"/></version>
        <LDisplayName(GetType(My.Resources.Resources), "n_HorizontalResolution"), LDescription(GetType(My.Resources.Resources), "d_HorizontalResolution")>
        Public ReadOnly Property HorizontalResolution As Single
            Get
                Return Image.HorizontalResolution
            End Get
        End Property
        ''' <summary>Gets the vertical resolution, in pixels per inch, of this <see cref="System.Drawing.Image" />.</summary>
        ''' <returns>The vertical resolution, in pixels per inch, of this <see cref="System.Drawing.Image" />.</returns>
        ''' <version version="1.5.4">Added <see cref="LDisplayNameAttribute"/> and <see cref="LDescriptionAttribute"/></version>
        <LDisplayName(GetType(My.Resources.Resources), "n_VerticalResolution"), LDescription(GetType(My.Resources.Resources), "d_VerticalResolution")>
        Public ReadOnly Property VerticalResolution As Single
            Get
                Return Image.VerticalResolution
            End Get
        End Property
        ''' <summary>Gets the pixel format for this <see cref="System.Drawing.Image" />.</summary>
        ''' <returns>A <see cref="System.Drawing.Imaging.PixelFormat" /> that represents the pixel format for this <see cref="System.Drawing.Image" />.</returns>
        ''' <version version="1.5.4">Added <see cref="LDisplayNameAttribute"/> and <see cref="LDescriptionAttribute"/></version>
        <LDisplayName(GetType(My.Resources.Resources), "n_PixelFormat"), LDescription(GetType(My.Resources.Resources), "d_PixelFormat")>
        Public ReadOnly Property PixelFormat As Drawing.Imaging.PixelFormat
            Get
                Return Image.PixelFormat
            End Get
        End Property
#End Region
#Region "IMetadata"
        ''' <summary>Contains definition of <see cref="IMetadata"/> properties</summary>
        Private Shared ReadOnly properties As New Dictionary(Of String, MetadataPropertyDescriptor(Of ImageMetadata)) From {
            {"Width", New IntDescriptor("Width", Function(m) m.Width, Function() My.Resources.n_Width, Function() My.Resources.d_Width)},
            {"Height", New IntDescriptor("Height", Function(m) m.Height, Function() My.Resources.n_Hight, Function() My.Resources.d_Height)},
            {"HorizontalResolution", New SngDescriptor("HorizontalResolution", Function(m) m.HorizontalResolution, Function() My.Resources.n_HorizontalResolution, Function() My.Resources.d_HorizontalResolution)},
            {"VerticalResolution", New SngDescriptor("VerticalResolution", Function(m) m.VerticalResolution, Function() My.Resources.n_VerticalResolution, Function() My.Resources.d_VerticalResolution)},
            {"PixelFormat", New PxfDescriptor("PixelFormat", Function(m) m.PixelFormat, Function() My.Resources.n_PixelFormat, Function() My.Resources.d_PixelFormat)}
        }

        ''' <summary>Gets value indicating wheather metadata value with given key is present in current instance</summary>
        ''' <param name="key">Key (or name) to check presence of (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>True when value for given key is present; false otherwise</returns>
        ''' <remarks>The <paramref name="key"/> parameter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).
        ''' <para>This implementation returns true forr all predefined keys.</para></remarks>
        ''' <version version="1.5.4">Parameter name <c>Key</c> changed to <c>key</c></version>
        Public Function ContainsKey(ByVal key As String) As Boolean Implements IMetadata.ContainsKey
            Return properties.ContainsKey(key)
        End Function

        ''' <summary>Gets keys of all the metadata present in curent instance</summary>
        ''' <returns>Keys in metadata-specific format of all the metadata present in curent instance. Never returns null; may return anempt enumeration.</returns>
        Public Function GetContainedKeys() As System.Collections.Generic.IEnumerable(Of String) Implements IMetadata.GetContainedKeys
            Return properties.Keys
        End Function

        ''' <summary>Gets localized description for given key (or name)</summary>
        ''' <param name="key">Key (or name) to get description of</param>
        ''' <returns>Localized description of purpose of metadata item identified by <paramref name="key"/>; null when description is not available.</returns>
        ''' <exception cref="ArgumentException"><paramref name="key"/> is in invalid format or it is not one of predefined names.</exception>
        ''' <version version="1.5.4">Fixed typo in English description of <see cref="HorizontalResolution"/></version>
        ''' <version version="1.5.4">Parameter name <c>Key</c> changed to <c>key</c></version>
        ''' <version version="1.5.4"><see cref="ArgumentException"/> is now really thrown as documented. Previously returned null.</version>
        ''' <version version="1.5.4">Descriptions extracted to resources and made localizable</version>
        Public Function GetDescription(ByVal key As String) As String Implements IMetadata.GetDescription
            If properties.ContainsKey(key) Then
                Return properties(key).Description
            Else
                Throw New ArgumentException(My.Resources.ex_NotKnownImageMetadataItemName.f(key))
            End If
        End Function

        ''' <summary>Gets localized human-readable name for given key (or name)</summary>
        ''' <param name="key">Key (or name) to get name for</param>
        ''' <returns>Human-readable descriptive name of metadata item identified by <paramref name="key"/>; null when no such name is defined/known.</returns>
        ''' <exception cref="ArgumentException"><paramref name="key"/> has invalid formar or it is not one of predefined names.</exception>
        ''' <version version="1.5.4">Parameter name <c>Key</c> changed to <c>key</c></version>
        ''' <version version="1.5.4"><see cref="ArgumentException"/> is now really thrown as documented. Previously returned null.</version>
        ''' <version version="1.5.4">Human names extracted to resources and made localizable</version>
        Public Function GetHumanName(ByVal key As String) As String Implements IMetadata.GetHumanName
            If properties.ContainsKey(key) Then
                Return properties(key).HumanName
            Else
                Throw New ArgumentException(My.Resources.ex_NotKnownImageMetadataItemName.f(key))
            End If
        End Function

        ''' <summary>Gets key for predefined name</summary>
        ''' <param name="name">Name to get key for</param>
        ''' <returns>Key in metadata-specific format for given predefined metadata item name</returns>
        ''' <exception cref="ArgumentException"><paramref name="name"/> is not one of predefined names retuened by <see cref="GetPredefinedNames"/>.</exception>
        ''' <remarks>This implementation either returns <paramref name="name"/> or throws <see cref="ArgumentException"/></remarks>
        ''' <version version="1.5.4">Parameter name <c>Name</c> changed to <c>name</c>.</version>
        Private Function GetKeyOfName(ByVal name As String) As String Implements IMetadata.GetKeyOfName
            If properties.ContainsKey(name) Then Return name
            Throw New ArgumentException(My.Resources.ex_NotKnownImageMetadataItemName.f(name))
        End Function

        ''' <summary>Gets name for key</summary>
        ''' <param name="key">Key to get name for</param>
        ''' <returns>One of predefined names to use instead of <paramref name="key"/>; null when given key has no corresponding name.</returns>
        ''' <exception cref="ArgumentException"><paramref name="key"/> has invalid format</exception>
        ''' <remarks>This implementation either returns <paramref name="Name"/> or throws <see cref="ArgumentException"/></remarks>
        ''' <version version="1.5.4">Parameter name <c>Key</c> changed to <c>key</c>.</version>
        Private Function GetNameOfKey(ByVal key As String) As String Implements IMetadata.GetNameOfKey
            If properties.ContainsKey(key) Then Return key
            Throw New ArgumentException(My.Resources.ex_NotKnownImageMetadataItemName.f(key))
        End Function

        ''' <summary>Gets all keys predefined for curent metadata format</summary>
        ''' <returns>Eumeration containing all predefined (well-known) keys of metadata for this metadata format. Returns always the same enumeration even when values for some keys are not present. Never returns null; may return an empty enumeration.</returns>
        ''' <remarks>Not all predefined keys are required to have corresponding names obtainable via <see cref="GetNameOfKey"/>.
        ''' <para>This implementation returns exactly the same collection as <see cref="GetPredefinedNames"/>.</para></remarks>
        Private Function GetPredefinedKeys() As System.Collections.Generic.IEnumerable(Of String) Implements IMetadata.GetPredefinedKeys
            Return properties.Keys
        End Function

        ''' <summary>Gets all predefined names for metadata keys</summary>
        ''' <returns>Enumeration containing all predefined names of metadata items for this metadada format. Never returns null; may return an empty enumeration.</returns>
        ''' <remarks>This implementation of <see cref="IMetadata"/> in fact supports only retrieval of metadata by name and thus keys keys are same as names.</remarks>
        Public Function GetPredefinedNames() As System.Collections.Generic.IEnumerable(Of String) Implements IMetadata.GetPredefinedNames
            Return properties.Keys
        End Function

        ''' <summary>Gets metadata value with given key as string</summary>
        ''' <param name="key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>Value of metadata item with given key as string; or null if given metadata value cannot be obtained</returns>
        ''' <exception cref="ArgumentException"><paramref name="key"/> is not one of predefined names</exception>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        Public Function GetStringValue(ByVal key As String) As String Implements IMetadata.GetStringValue
            Return GetStringValue(key, Nothing)
        End Function

        ''' <summary>Gets metadata value with given key as string</summary>
        ''' <param name="key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <param name="provider">Culture to be used. If null current is used.</param>
        ''' <returns>Value of metadata item with given key as string; or null if given metadata value cannot be obtained</returns>
        ''' <exception cref="ArgumentException"><paramref name="key"/> is not one of predefined names</exception>
        ''' <version version="1.5.4">This overload is new in version 1.5.4</version>
        Public Function GetStringValue(ByVal key As String, provider As IFormatProvider) As String Implements IMetadata.GetStringValue
            If provider Is Nothing Then provider = Globalization.CultureInfo.CurrentCulture
            Dim value = Me.Value(key)
            If value Is Nothing Then Return Nothing
            If TypeOf value Is IFormattable Then Return DirectCast(value, IFormattable).ToString(provider)
            Return value.ToString
        End Function

        ''' <summary>Gets name of metadata format represented by implementation</summary>
        ''' <returns>Name to identify this metadata format by. Always returns <see cref="ImageName"/>.</returns>
        ''' <remarks>All <see cref="IMetadataProvider">IMetadataProviders</see> returning this format should identify the format by this name.</remarks>
        Private ReadOnly Property Name As String Implements IMetadata.Name
            Get
                Return ImageName$
            End Get
        End Property
        ''' <summary>Used to identify system metadata in <see cref="IMetadataProvider"/></summary>
        ''' <seelaso cref="Name"/>
        Public Const ImageName$ = "Image"

        ''' <summary>Gets medata value with given key</summary>
        ''' <param name="key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>Value of metadata item with given key; or null if given metadata value cannot be obtained</returns>
        ''' <exception cref="ArgumentException"><paramref name="key"/> is not one of predefined names</exception>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        Default Public ReadOnly Property Value(ByVal key As String) As Object Implements IMetadata.Value
            Get
                If Not properties.ContainsKey(key) Then Throw New ArgumentException(My.Resources.ex_NotKnownImageMetadataItemName.f(key))
                Try
                    Return properties(key).GetValue(Me)
                Catch
                    Return Nothing
                End Try
            End Get
        End Property
#End Region
#Region " IDisposable Support "
        ''' <summary>Allows an System.Object to attempt to free resources and perform other cleanup operations before the System.Object is reclaimed by garbage collection.</summary>
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            Dispose()
        End Sub
        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        Public Overridable Sub Dispose() Implements IDisposable.Dispose
            If CreatedImage AndAlso Image IsNot Nothing Then Image.Dispose()
            Image = Nothing
        End Sub
#End Region

    End Class
End Namespace
#End If