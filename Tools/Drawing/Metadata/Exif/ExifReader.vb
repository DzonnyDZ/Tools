Namespace Drawing.Metadata
#If Config <= Nightly Then 'Stage: Nigtly
    ''' <summary>Provides low level access to stream of Exif data</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ExifReader), LastChange:="4/24/2007")> _
    Public Class ExifReader
        ''' <summary>CTor from <see cref="System.IO.Stream"/></summary>
        ''' <param name="Stream"><see cref="System.IO.Stream"/> that contains Exif data</param>
        Public Sub New(ByVal Stream As System.IO.Stream)
            _Stream = Stream
            If Stream Is Nothing OrElse Stream.Length = 0 Then Exit Sub
            Parse()
        End Sub
        ''' <summary>CTor from <see cref="IExifGetter"/></summary>
        ''' <param name="Container">Object that contains <see cref="System.IO.Stream"/> with Exif data</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Container"/> is null</exception>
        Public Sub New(ByVal Container As IExifGetter)
            If Container Is Nothing Then Throw New ArgumentNullException("Container", "Container cannot be null")
            _Stream = Container.GetExifStream
            If _Stream Is Nothing OrElse _Stream.Length = 0 Then Exit Sub
            Parse()
        End Sub
        ''' <summary>Parses stream of Exif data</summary>
        Private Sub Parse()

        End Sub
        ''' <summary>Contains value of the <see cref="Stream"/> property</summary>
        Private _Stream As System.IO.Stream
        ''' <summary>Stream used to obtain Exif data</summary>
        Public ReadOnly Property Stream() As System.IO.Stream
            Get
                Return _Stream
            End Get
        End Property
    End Class
#End If
End Namespace
