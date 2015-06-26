Imports Tools.ComponentModelT, Tools.ExtensionsT, System.Globalization.CultureInfo, System.Linq
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>
Imports System.Xml.Linq
Imports System.Xml.Serialization

Namespace TextT.UnicodeT
    ''' <summary>Represents Unicode standardized variant</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <DebuggerDisplay("{Description}")>
    Public Class UnicodeStandardizedVariant
        Implements IXElementWrapper

        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <standardized-variant/>.Name

        Private ReadOnly _element As Xml.Linq.XElement

        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeStandardizedVariant"/> class</summary>
        ''' <param name="element">A <see cref="XElement"/> that represents this named sequence</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not element &lt;standardized-variant> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
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

        ''' <summary>Gets description of this standardized variant</summary>
        <XmlAttribute("desc")>
        Public ReadOnly Property Description As String
            Get
                Return Element.@desc
            End Get
        End Property

        ''' <summary>Gets codepoints that participate in this standardized variant</summary>
        ''' <remarks>This collection should always contain exactly two items</remarks>
        <XmlAttribute("cps")>
        Public ReadOnly Property CodePoints As CodePointInfoCollection
            Get
                Return New CodePointInfoCollection(Element.Document, Element.@cps)
            End Get
        End Property

        ''' <summary>Gets a string that indicates when this standardized variant is used</summary>
        <XmlAttribute("when")>
        Public ReadOnly Property [When] As String
            Get
                Return Element.@when
            End Get
        End Property
    End Class
End Namespace
