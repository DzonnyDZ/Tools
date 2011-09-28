Imports System.Linq
Imports System.Xml.Linq
Imports Tools.ComponentModelT
Imports Tools.ExtensionsT
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>

Namespace TextT.UnicodeT

    ''' <summary>Helper class - represents Unicode Character Database XML repertoire - can contain groups and codepoints</summary>
    ''' <remarks>THis is helper class - you usually don't need to use it. <see cref="UnicodeCharacterDatabase"/> is used instead.</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class UcdRepertoire
        Implements IXElementWrapper

        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <repertoire/>.Name

        ''' <summary>CTor - creates a new instance of the <see cref="UcdRepertoire"/> classs</summary>
        ''' <param name="element">XML &lt;repertoire> element</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not element &lt;repertoire> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(element As XElement)
            If element Is Nothing Then Throw New ArgumentNullException("repertoire")
            If element.Name <> elementName Then Throw New ArgumentException(UnicodeResources.ex_InvalidXmlElement.f(elementName))
            _element = element
        End Sub

        Private _element As XElement

        ''' <summary>Gets XML element this instance wraps</summary>
        Public ReadOnly Property Element As System.Xml.Linq.XElement Implements ComponentModelT.IXElementWrapper.Element
            Get
                Return _element
            End Get
        End Property

        ''' <summary>Gets node this instance wraps</summary>
        Private ReadOnly Property Node As System.Xml.Linq.XNode Implements ComponentModelT.IXNodeWrapper.Node
            Get
                Return Element
            End Get
        End Property

        ''' <summary>Gets items - <see cref="UnicodeCharacterGroup"/>s and <see cref="UnicodeCodePoint"/>s in repertoire</summary>
        Public ReadOnly Property Items As IEnumerable(Of UnicodePropertiesProvider)
            Get
                Return From el In Element.Elements
                       Select If(Element.Name = UnicodeCharacterGroup.elementName, DirectCast(New UnicodeCharacterGroup(el), UnicodePropertiesProvider), UnicodeCodePoint.Create(el))
            End Get
        End Property
    End Class
End Namespace
