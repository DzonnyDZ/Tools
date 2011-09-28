Imports Tools.ComponentModelT, Tools.ExtensionsT, System.Globalization.CultureInfo, System.Linq
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>
Imports System.Xml.Linq

Namespace TextT.UnicodeT
    ''' <summary>Represents a named sequence of unicode characters</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <DebuggerDisplay("{Name} {Sequence}")>
    Public Class UnicodeNamedSequence
        Implements IXElementWrapper

        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <named-sequence/>.Name

        Private ReadOnly _element As Xml.Linq.XElement

        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeNamedSequence"/> class</summary>
        ''' <param name="element">A <see cref="XElement"/> that represents this named sequence</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not element &lt;named-sequence> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Sub New(element As Xml.Linq.XElement)
            If element Is Nothing Then Throw New ArgumentNullException("element")
            If element.Name <> elementName Then Throw New ArgumentException(UnicodeResources.ex_InvalidXmlElement.f(elementName))
            _element = element
        End Sub



        ''' <summary>Gets XML element this instance wraps</summary>
        Public ReadOnly Property Element As System.Xml.Linq.XElement Implements IXElementWrapper.Element
            Get
                Return _element
            End Get
        End Property

        ''' <summary>Gets node this instance wraps</summary>
        Private ReadOnly Property Node As System.Xml.Linq.XNode Implements IXNodeWrapper.Node
            Get
                Return Element
            End Get
        End Property

        ''' <summary>Gets name of this named sequence</summary>
        Public ReadOnly Property Name$
            Get
                Return Element.@name
            End Get
        End Property

        ''' <summary>Gets array of code points that belong to this named sequence</summary>
        Public ReadOnly Property CodePoints As CodePointInfoCollection
            Get
                Return New CodePointInfoCollection(Element.Document, Element.@cps)
            End Get
        End Property

        ''' <summary>Gets a string that represents characters in this named sequence</summary>
        ''' <seelaso cref="CodePointInfoCollection.ToString"/>
        Public ReadOnly Property Sequence As String
            Get
                Return CodePoints.ToString("s") ' String.Join("", From cp In Element.@cps.WhiteSpaceSplit Select Char.ConvertFromUtf32(UInteger.Parse("0x" & cp, Globalization.NumberStyles.HexNumber, InvariantCulture)))
            End Get
        End Property

        ''' <summary>Gets a string that represents this <see cref="UnicodeNamedSequence"/></summary>
        ''' <returns><see cref="Name"/> and <see cref="Sequence"/> concatenated using space</returns>
        Public Overrides Function ToString() As String
            Return String.Format("{0} {1}", Name, Sequence)
        End Function
    End Class
End Namespace
