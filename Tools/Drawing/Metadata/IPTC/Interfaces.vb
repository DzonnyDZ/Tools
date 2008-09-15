Namespace DrawingT.MetadataT.IptcT
#If Config <= Beta Then 'Stage: Beta
    ''' <summary>Represents provider that provides stream of IPTC data</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(IIptcGetter), LastChange:="04/24/2007")> _
    Public Interface IIptcGetter
        ''' <summary>Gets stream of IPTC data</summary>
        ''' <remarks>
        ''' <para>Stream content must start with first tag marker 1Ch of IPTC stream</para>
        ''' <para>If there is no IPTC data in file stream can be null or have zero length</para>
        ''' <para>Stream must support reading and seeking</para>
        ''' </remarks>
        Function GetIptcStream() As System.IO.Stream
    End Interface

    ''' <summary>Represents provider that provides method to writed IPTC data into container</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(IIptcWriter), LastChange:="07/22/2007")> _
    Public Interface IIptcWriter
        ''' <summary>Writes IPTC data into container</summary>
        ''' <param name="IPTCData">Data to be written</param>
        Sub IptcEmbded(ByVal IPTCData As Byte())
    End Interface
#End If
End Namespace
