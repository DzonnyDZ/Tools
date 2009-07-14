Imports Tools.ExtensionsT, System.Linq
'Localize: This file is not localized
Namespace MetadataT
#If Config <= Nightly Then 'Stage: Nightly
    ''' <summary>Provides acces to common properties of images exposed by the <see cref="Drawing.Image"/> class using <see cref="IMetadata"/> interface</summary>
    ''' <remarks>This implementation of <see cref="IMetadata"/> supports only named values. Possible values for keys are same as possible values for names.</remarks>
    ''' <version version="1.5.3" stage="Nightly">Class introduced</version>
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
        Public Sub New(ByVal FileName$)
            Try
                Image = Drawing.Image.FromFile(FileName)
            Catch ex As OutOfMemoryException
                Throw New ArgumentException("The image file is invalid.", "FileName", ex)
            End Try
            CreatedImage = True
        End Sub
        ''' <summary>CTor creates new instance of <see cref="ImageMetadata"/> form <see cref="Image"/></summary>
        ''' <param name="Image"><see cref="Image"/> reperesneting image to provide metadata for</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Image"/> is nul</exception>
        Public Sub New(ByVal Image As Drawing.Image)
            If Image Is Nothing Then Throw New ArgumentNullException("Image")
            Me.Image = Image
            CreatedImage = False
        End Sub
#Region "Properties"
        ''' <summary>Gets the width, in pixels, of this <see cref="System.Drawing.Image" />.</summary>
        ''' <returns>The width, in pixels, of this <see cref="System.Drawing.Image" />.</returns>
        Public ReadOnly Property Width As Integer
            Get
                Return Image.Width
            End Get
        End Property
        ''' <summary>Gets the height, in pixels, of this <see cref="System.Drawing.Image" />.</summary>
        ''' <returns>The height, in pixels, of this <see cref="System.Drawing.Image" />.</returns>
        Public ReadOnly Property Height As Integer
            Get
                Return Image.Height
            End Get
        End Property
        ''' <summary>Gets the horizontal resolution, in pixels per inch, of this <see cref="System.Drawing.Image" />.</summary>
        ''' <returns>The horizontal resolution, in pixels per inch, of this <see cref="System.Drawing.Image" />.</returns>
        Public ReadOnly Property HorizontalResolution As Single
            Get
                Return Image.HorizontalResolution
            End Get
        End Property
        ''' <summary>Gets the vertical resolution, in pixels per inch, of this <see cref="System.Drawing.Image" />.</summary>
        ''' <returns>The vertical resolution, in pixels per inch, of this <see cref="System.Drawing.Image" />.</returns>
        Public ReadOnly Property VerticalResolution As Single
            Get
                Return Image.VerticalResolution
            End Get
        End Property
        ''' <summary>Gets the pixel format for this <see cref="System.Drawing.Image" />.</summary>
        ''' <returns>A <see cref="System.Drawing.Imaging.PixelFormat" /> that represents the pixel format for this <see cref="System.Drawing.Image" />.</returns>
        Public ReadOnly Property PixelFormat As Drawing.Imaging.PixelFormat
            Get
                Return Image.PixelFormat
            End Get
        End Property
#End Region
#Region "IMetadata"
        ''' <summary>Contains names of properties</summary>
        Private Shared ReadOnly Keys As String() = {"Width", "Height", "HorizontalResolution", "VerticalResolution", "PixelFormat"}

        ''' <summary>Gets value indicating wheather metadata value with given key is present in current instance</summary>
        ''' <param name="Key">Key (or name) to check presence of (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>True when value for given key is present; false otherwise</returns>
        ''' <remarks>The <paramref name="Key"/> parameter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).
        ''' <para>This implementation returns true forr all predefined keys.</para></remarks>
        Public Function ContainsKey(ByVal Key As String) As Boolean Implements IMetadata.ContainsKey
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
        Public Function GetDescription(ByVal Key As String) As String Implements IMetadata.GetDescription
            Select Case Key
                Case "Width" : Return "Image width in pixels"
                Case "Height" : Return "Image height in pixels"
                Case "HorizontalResolution" : Return "Image horizobtal resolution in pixels per inch"
                Case "VerticalResolution" : Return "Image vertical resolution in pixels per inch"
                Case "PixelFormat" : Return "Image pixel format"
                Case Else : Return Nothing
            End Select
        End Function

        ''' <summary>Gets localized human-readable name for given key (or name)</summary>
        ''' <param name="Key">Key (or name) to get name for</param>
        ''' <returns>Human-readable descriptive name of metadata item identified by <paramref name="Key"/>; null when no such name is defined/known.</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid formar or it is not one of predefined names.</exception>
        Public Function GetHumanName(ByVal Key As String) As String Implements IMetadata.GetHumanName
            Select Case Key
                Case "Width" : Return "Width"
                Case "Height" : Return "Height"
                Case "HorizontalResolution" : Return "Horizontal resolution"
                Case "VerticalResolution" : Return "Vertical resolution"
                Case "PixelFormat" : Return "Pixel format"
                Case Else : Return Nothing
            End Select
        End Function

        ''' <summary>Gets key for predefined name</summary>
        ''' <param name="Name">Name to get key for</param>
        ''' <returns>Key in metadata-specific format for given predefined metadata item name</returns>
        ''' <exception cref="ArgumentException"><paramref name="Name"/> is not one of predefined names retuened by <see cref="GetPredefinedNames"/>.</exception>
        ''' <remarks>This implementation either returns <paramref name="Name"/> or throws <see cref="ArgumentException"/></remarks>
        Private Function GetKeyOfName(ByVal Name As String) As String Implements IMetadata.GetKeyOfName
            If Keys.Contains(Name) Then Return Name
            Throw New ArgumentException("{0} is not known image metadata item name.".f(Name))
        End Function

        ''' <summary>Gets name for key</summary>
        ''' <param name="Key">Key to get name for</param>
        ''' <returns>One of predefined names to use instead of <paramref name="Key"/>; null when given key has no corresponding name.</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid format</exception>
        ''' <remarks>This implementation either returns <paramref name="Name"/> or throws <see cref="ArgumentException"/></remarks>
        Private Function GetNameOfKey(ByVal Key As String) As String Implements IMetadata.GetNameOfKey
            If Keys.Contains(Key) Then Return Key
            Throw New ArgumentException("{0} is not known image metadata item name.".f(Key))
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
        Public Function GetStringValue(ByVal Key As String) As String Implements IMetadata.GetStringValue
            Dim [error] As Boolean = False
            Try
                Select Case Key
                    Case "Width" : Return Width.ToString
                    Case "Height" : Return Height.ToString
                    Case "HorizontalResolution" : Return HorizontalResolution.ToString
                    Case "VerticalResolution" : Return VerticalResolution.ToString
                    Case "PixelFormat" : Return PixelFormat.ToString("F")
                    Case Else : [error] = True : Throw New ArgumentException("{0} is not known image metadata item name.".f(Key))
                End Select
            Catch When Not [error]
                Return Nothing
            End Try
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
        ''' <param name="Key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>Value of metadata item with given key; or null if given metadata value cannot be obtained</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> is not one of predefined names</exception>
        Default Public ReadOnly Property Value(ByVal Key As String) As Object Implements IMetadata.Value
            Get
                Dim [error] As Boolean = False
                Try
                    Select Case Key
                        Case "Width" : Return Width
                        Case "Height" : Return Height
                        Case "HorizontalResolution" : Return HorizontalResolution
                        Case "VerticalResolution" : Return VerticalResolution
                        Case "PixelFormat" : Return PixelFormat
                        Case Else : [error] = True : Throw New ArgumentException("{0} is not known image metadata item name.".f(Key))
                    End Select
                Catch When Not [error]
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
#End If
End Namespace