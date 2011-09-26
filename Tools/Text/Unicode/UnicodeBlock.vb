Imports System.Globalization.CultureInfo
Imports System.Linq
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports Tools.ComponentModelT
Imports Tools.ExtensionsT
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>

Namespace TextT.UnicodeT
    ''' <summary>Represens a Unicode block from Unicode Character Database XML</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <DebuggerDisplay("{Name}")>
    Public Class UnicodeBlock
        Implements IXElementWrapper
        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <block/>.Name

        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeBlock"/> class</summary>
        ''' <param name="element">A <see cref="XElement"/> to used data of</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not element &lt;block> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(element As XElement)
            If element Is Nothing Then Throw New ArgumentNullException("element")
            If element.Name <> elementName Then Throw New ArgumentException(UnicodeResources.ex_InvalidXmlElement.f(elementName))
            _element = element
        End Sub

        Private ReadOnly _element As XElement

        ''' <summary>Gets XML element this instance wraps</summary>
        <XmlIgnore()>
        Public ReadOnly Property Element As XElement Implements IXElementWrapper.Element
            Get
                Return _element
            End Get
        End Property

        ''' <summary>Gets node this instance wraps</summary>
        Private ReadOnly Property Node As XNode Implements IXNodeWrapper.Node
            Get
                Return Element
            End Get
        End Property

        ''' <summary>Gets first code point of this block</summary>
        Public ReadOnly Property FirstCodePoint As CodePointInfo
            Get
                Return New CodePointInfo(Element.Document, Integer.Parse("0x" & Element.@<first-cp>, Globalization.NumberStyles.HexNumber, InvariantCulture))
            End Get
        End Property
        ''' <summary>Gets last code point of this block</summary>
        Public ReadOnly Property LastCodePoint As CodePointInfo
            Get
                Return New CodePointInfo(Element.Document, Integer.Parse("0x" & Element.@<last-cp>, Globalization.NumberStyles.HexNumber, InvariantCulture))
            End Get
        End Property
        ''' <summary>Gets name of this block</summary>
        Public ReadOnly Property Name As String
            Get
                Return Element.@name
            End Get
        End Property

        ''' <summary>Gets all codepoints in this block</summary>
        ''' <remarks>Codepoints returned by this property are the same codepoints defined in UCD XML. Thus multi-copdepoint codepoints can be returned and it's theoretically possible that such multiple-codepoint codepoint overlaps blocks.</remarks>
        ''' <seelaso cref="EnumerateCodePoints"/>
        Public ReadOnly Property CodePoints As IEnumerable(Of UnicodeCodePoint)
            Get
                Dim hex1 = FirstCodePoint.CodePoint.ToString("X", InvariantCulture)
                Dim hexX = LastCodePoint.CodePoint.ToString("X", InvariantCulture)
                Return From el In UnicodeCharacterDatabase.GetCodePointXmlElements(Element.Document)
                       Where (el.@cp IsNot Nothing AndAlso el.@cp >= hex1 AndAlso el.@cp <= hexX) OrElse
                             (el.@cp Is Nothing AndAlso el.@<first-cp> IsNot Nothing AndAlso el.@<last-cp> IsNot Nothing AndAlso el.@<first-cp> >= hex1 AndAlso el.@<last-cp> <= hexX) OrElse
                             (el.@cp Is Nothing AndAlso el.@<first-cp> IsNot Nothing AndAlso el.@<last-cp> Is Nothing AndAlso el.@<first-cp> >= hex1 AndAlso el.@<first-cp> <= hexX) OrElse
                             (el.@cp Is Nothing AndAlso el.@<first-cp> Is Nothing AndAlso el.@<last-cp> IsNot Nothing AndAlso el.@<last-cp> >= hex1 AndAlso el.@<last-cp> <= hexX)
                       Select UnicodeCodePoint.Create(el)
            End Get
        End Property


        ''' <summary>Enumerates all codepoints in this block separatelly</summary>
        ''' <returns>Iterator over all codepoints in this block.</returns>
        ''' <remarks>In contrast to <see cref="CodePoints"/> this function gets each codepoint as separate instance.</remarks>
        ''' <seelaso cref="CodePoints"/>
        Public Function EnumerateCodePoints() As IEnumerable(Of CodePointInfo)
            Return New Tools.LinqT.ForLoopCollection(Of CodePointInfo, UInteger)(
                Function() FirstCodePoint.CodePoint,
                Function(i) i <= LastCodePoint.CodePoint,
                Function(i) i + 1UI,
                Function(i) New CodePointInfo(Element.Document, i)
            )
        End Function
        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="UnicodeBlock" />.</summary>
        ''' <returns><see cref="Name"/></returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
