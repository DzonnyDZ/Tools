Namespace MetadataT.ExifT
#If Config <= Beta Then 'Stage: Beta
    ''' <summary>Represents provider that provides stream of Exif data</summary>
    ''' <seealso cref="IExifWriter"/><seealso cref="IptcT.iiptcgetter"/>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Beta"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Interface IExifGetter
        ''' <summary>Gets stream of Exif data</summary>
        ''' <returns>Stream of Exif data. Can return null or an empty stream where there are no Exif data.</returns>
        ''' <remarks>
        ''' <para>Stream content must start with TIFF header</para>
        ''' <para>If there is no Exif data in file stream can be null or have zero length</para>
        ''' <para>Stream must support reading and seeking</para>
        ''' </remarks>
        Function GetExifStream() As System.IO.Stream
    End Interface
    ''' <summary>Represents provider that provides stream of Exif data for reading and writing</summary>
    ''' <seealso cref="IExifGetter"/><seealso cref="IptcT.iiptcwriter"/>
    ''' <author www="htp://dzonny.cz">Ðonny</author>
    ''' <version stage="Beta" version="1.5.2">Added <see cref="M:Tools.MetadataT.ExifT.IExifWriter.ExifEmbded(System.IO.Stream)"/> overload</version>
    Public Interface IExifWriter
        ''' <summary>Gets stream of Exif data</summary>
        ''' <returns>Stream of Exif data which supports reading as well as writing. Return value can be null or zero-lenght stream if there is currently no Exif data.
        ''' <para>If this function returns null, caller shall use <see cref="ExifEmbded"/> instead.</para></returns>
        ''' <remarks>
        ''' <para>If class implements <see cref="IExifGetter"/> as well as <see cref="IExifWriter"/> the <see cref="IExifGetter.GetExifStream"/> and <see cref="GetWritableExifStream"/> functions can be (but have not to be) implemented by same function.
        ''' Note: You can find it better to implement both methods separatelly, because stream returned by <see cref="IExifGetter.GetExifStream"/> can have simplier implementation.</para>
        ''' <para>Stream content must start with TIFF header.</para>
        ''' <para>If there is no exif data in file stream can have zero lenght.</para>
        ''' <para>Stream must support reading, writing and seeking.</para>
        ''' <para>Stream must support changes of its lenght - both growing and shrinking. Namely when stream is returned as constrained stream which represents part of another stream it must properly handle situation when new Exif data are shorter or longer than original.
        ''' The tools library provides class <see cref="IOt.OverflowStream"/> which implements stream that operates over another base stream and supports changes of lenght.</para>
        ''' </remarks>
        Function GetWritableExifStream() As IO.Stream
        ''' <summary>Gets value indicating if there is any Exif data currently embdeded in this instance.</summary>
        ''' <returns>True if thie instance contains Exif data and <see cref="GetWritableExifStream"/> returns valid stream of Exif data, false where there is no Exif data, so it is safe to use <see cref="ExifEmbded"/> without possible data loss of unknown Exif content.</returns>
        ReadOnly Property ContainsExif() As Boolean
        ''' <summary>Replaces whole Exif data with given aray of bytes</summary>
        ''' <param name="ExifData">New Exif data to be embdeded in this instance. <paramref name="ExifData"/> contains full Exif data block including TIFF header. If <paramref name="ExifData"/> is null all Exif data shall be erased.</param>
        Sub ExifEmbded(ByVal ExifData As Byte())
        ''' <summary>Replaces whole Exif data with content of given stream.</summary>
        ''' <param name="ExifData">Contains exif data. Stream must be on its initial position. This methods does not seek. Mus support reading. Contains full Exif data including TIFF header. If null Exif data should be erased.</param>
        ''' <version version="1.5.2">This overload added</version>
        Sub ExifEmbded(ByVal ExifData As IO.Stream)
    End Interface
#End If
End Namespace
