Imports Tools.ComponentModelT, Tools.ExtensionsT, System.Globalization.CultureInfo, System.Linq
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>
Imports System.Xml.Linq

Namespace TextT.UnicodeT
    ''' <summary>Provides information about Emoji source</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class EmojiSourceInfo
        Implements IXElementWrapper

        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <emoji-source/>.Name

        Private ReadOnly _element As Xml.Linq.XElement

        ''' <summary>CTor - creates a new instance of the <see cref="EmojiSourceInfo"/> class</summary>
        ''' <param name="element">A <see cref="XElement"/> that represents this named sequence</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not element &lt;emoji-source> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
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

        ''' <summary>Gets Unicode code point(s) for this Emoji Source</summary>
        Public ReadOnly Property Unicode As CodePointInfoCollection
            Get
                Return New CodePointInfoCollection(Element.Document, Element.@unicode)
            End Get
        End Property

        ''' <summary>Gets DoCoMo Shift-JIS code</summary>
        Public ReadOnly Property DoCoMo As Integer?
            Get
                Dim val = Element.@docomo
                If val.IsNullOrEmpty Then Return Nothing
                Return Integer.Parse(val, Globalization.NumberStyles.HexNumber, InvariantCulture)
            End Get
        End Property

        ''' <summary>Gets KDDI Shift-JIS code</summary>
        Public ReadOnly Property Kddi As Integer?
            Get
                Dim val = Element.@kddi
                If val.IsNullOrEmpty Then Return Nothing
                Return Integer.Parse(val, Globalization.NumberStyles.HexNumber, InvariantCulture)
            End Get
        End Property

        ''' <summary>Gets SoftBank Shift-JIS code</summary>
        Public ReadOnly Property SoftBank As Integer?
            Get
                Dim val = Element.@softbank
                If val.IsNullOrEmpty Then Return Nothing
                Return Integer.Parse(val, Globalization.NumberStyles.HexNumber, InvariantCulture)
            End Get
        End Property

    End Class
End Namespace
