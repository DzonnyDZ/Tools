Namespace TextT.UnicodeT
    ''' <summary>Represents a CSUR character block and provides additional CSUR properties</summary>
    Public Class CsurBlock
        Inherits UnicodeBlock

        ''' <summary>CTor - creates a new instance of the <see cref="CsurBlock"/> class</summary>
        ''' <param name="element">An XML element that represents the block. The element should come from a document that specifies extension CSUR properties.</param>
        ''' <param name="mainUcdDocument">A XML document that represents the main Unicode Character Database (not CSUR extension database)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not element &lt;block> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(element As XElement, mainUcdDocument As XDocument)
            MyBase.New(element)
            If mainUcdDocument Is Nothing Then Throw New ArgumentNullException("mainUcdDocument")
        End Sub

        Private mainUcdDocument As XDocument
        ''' <summary>Gets a XML document that represents whole Unicode Character Database</summary>
        ''' <remarks>In case of <see cref="CsurBlock"/> this document is typically different from document <see cref="Element"/> belongs to</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overrides ReadOnly Property Document As System.Xml.Linq.XDocument
            Get
                Return mainUcdDocument
            End Get
        End Property


        'TODO: CSUR properties

    End Class
End Namespace
