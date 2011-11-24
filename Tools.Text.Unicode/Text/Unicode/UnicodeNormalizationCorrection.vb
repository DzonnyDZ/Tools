Imports Tools.ComponentModelT, Tools.ExtensionsT, System.Globalization.CultureInfo, System.Linq
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>
Imports System.Xml.Linq


Namespace TextT.UnicodeT
    ''' <summary>Represents a normalization correction - change in normalization that happpened between Unicode versions</summary>
    ''' <version version="1.5.4">THis class is new in version 1.5.4</version>
    Public Class UnicodeNormalizationCorrection
        Implements IXElementWrapper
        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <normalization-correction/>.Name

        Private _element As Xml.Linq.XElement

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

        ''' <summary>Gets Unicode starndard version in which this corrrection was introduced</summary>
        Public ReadOnly Property Version As Version
            Get
                Return Version.Parse(Element.@version)
            End Get
        End Property

        ''' <summary>Gets a code point this correction informs about change of normalization of</summary>
        Public ReadOnly Property CodePoint As CodePointInfo
            Get
                Return New CodePointInfo(Element.Document, UInt32.Parse(Element.@cp, Globalization.NumberStyles.HexNumber, InvariantCulture))
            End Get
        End Property

        ''' <summary>Gets codepoint(s) indicating new normalization</summary>
        Public ReadOnly Property NewNormalization As CodePointInfoCollection
            Get
                Return New CodePointInfoCollection(Element.Document, Element.@old)
            End Get
        End Property

        ''' <summary>Gets codepoint(s) indicating old normalization</summary>
        Public ReadOnly Property OldNormalization As CodePointInfoCollection
            Get
                Return New CodePointInfoCollection(Element.Document, Element.@new)
            End Get
        End Property

    End Class
End Namespace
