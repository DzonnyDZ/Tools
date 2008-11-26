Namespace MetadataT.IptcT
#If Config <= Beta Then 'Stage: Beta
    ''' <summary>Represents provider that provides stream of IPTC data</summary>
    ''' <seealso cref="iiptcwriter"/><seealso cref="ExifT.iexifgetter"/>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Beta"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Interface IIptcGetter
        ''' <summary>Gets stream of IPTC data</summary>
        ''' <returns>Stream of IPTC data</returns>
        ''' <remarks>
        ''' <para>Stream content must start with first tag marker 1Ch of IPTC stream</para>
        ''' <para>If there is no IPTC data in file stream can be null or have zero length</para>
        ''' <para>Stream must support reading and seeking</para>
        ''' </remarks>
        Function GetIptcStream() As System.IO.Stream
    End Interface

    ''' <summary>Represents provider that provides method to writed IPTC data into container</summary>
    ''' <seealso cref="IIptcGetter"/><seealso cref="ExifT.iexifwriter"/>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Beta"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Interface IIptcWriter
        ''' <summary>Writes IPTC data into container</summary>
        ''' <param name="IPTCData">Data to be written</param>
        Sub IptcEmbded(ByVal IPTCData As Byte())
    End Interface
#End If
End Namespace
