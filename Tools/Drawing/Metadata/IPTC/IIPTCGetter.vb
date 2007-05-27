Namespace DrawingT.MetadataT
#If Config <= Beta Then 'Stage: Beta
    ''' <summary>Represents provider that provides stream of IPTC data</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
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
#End If
End Namespace
