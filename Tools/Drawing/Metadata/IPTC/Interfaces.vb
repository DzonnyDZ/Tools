Namespace DrawingT.MetadataT
#If Config <= Beta Then 'Stage: Beta
    ''' <summary>Represents provider that provides stream of IPTC data</summary>
    <Author("�onny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(IIPTCGetter), LastChMMDDYYYY:="04/24/2007")> _
    Public Interface IIPTCGetter
        ''' <summary>Gets stream of IPTC data</summary>
        ''' <remarks>
        ''' <para>Stream content must start with first tag marker 1Ch of IPTC stream</para>
        ''' <para>If there is no IPTC data in file stream can be null or have zero length</para>
        ''' <para>Stream must support reading and seeking</para>
        ''' </remarks>
        Function GetIPTCStream() As System.IO.Stream
    End Interface

    'ASAP:Wiki, Forum,Mark
    ''' <summary>Represents provider that provides method to writed IPTC data into container</summary>
    Public Interface IIPTCWriter
        ''' <summary>Writes IPTC data into container</summary>
        ''' <param name="IPTCData">Data to be written</param>
        Sub IPTCEmbed(ByVal IPTCData As Byte())
    End Interface
#End If
End Namespace