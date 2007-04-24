Namespace Drawing.Metadata 'TODO:Wiki
#If Config <= Nightly Then 'Stage: Nightly
    ''' <summary>Represents provider that provides stream of Exif data</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(IExifGetter), LastChange:="4/24/2007")> _
    Public Interface IExifGetter
        ''' <summary>Gets stream of Exif data</summary>
        ''' <remarks>
        ''' <para>Stream content must start with TIFF header</para>
        ''' <para>If there is no Exif data in file stream can be null or have zero length</para>
        ''' <para>Stream must support reading and seeking</para>
        ''' </remarks>
        Function GetExifStream() As System.IO.Stream
    End Interface
#End If
End Namespace
