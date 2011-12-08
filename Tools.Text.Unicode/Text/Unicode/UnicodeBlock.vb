Imports System.Globalization.CultureInfo
Imports System.Linq
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports Tools.ComponentModelT
Imports Tools.ExtensionsT
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>
Imports System.Globalization

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

        ''' <summary>Gets a XML document that represents whole Unicode Character Database</summary>
        ''' <remarks><note type="inheritinfo">Override this property in derived class if UCD document differs from <see cref="Element"/>.<see cref="XElement.Document">Document</see>.</note></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable ReadOnly Property Document As XDocument
            Get
                Return Element.Document
            End Get
        End Property

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
        <XmlElement("first-cp")>
        Public ReadOnly Property FirstCodePoint As CodePointInfo
            Get
                Return New CodePointInfo(Document, UInteger.Parse(Element.@<first-cp>, Globalization.NumberStyles.HexNumber, InvariantCulture))
            End Get
        End Property

        ''' <summary>Gets last code point of this block</summary>
        <XmlElement("last-cp")>
        Public ReadOnly Property LastCodePoint As CodePointInfo
            Get
                Return New CodePointInfo(Document, UInteger.Parse(Element.@<last-cp>, Globalization.NumberStyles.HexNumber, InvariantCulture))
            End Get
        End Property

        ''' <summary>Gets name of this block</summary>
        <XmlElement("name")>
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
                Return From el In UnicodeCharacterDatabase.GetCodePointXmlElements(Document)
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
                Function(i) New CodePointInfo(Document, i)
            )
        End Function
        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="UnicodeBlock" />.</summary>
        ''' <returns><see cref="Name"/></returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function ToString() As String
            Return Name
        End Function


        ''' <summary>Gets localized name for current UI culture</summary>
        ''' <remarks>For details see <see cref="GetLocalizedName"/>.</remarks>
        ''' <seelaso cref="GetLocalizedName"/>
        Public ReadOnly Property LocalizedName As String
            Get
                Return GetLocalizedName(Nothing)
            End Get
        End Property

        ''' <summary>Gets localized name of this block (if available)</summary>
        ''' <param name="culture">Culture to get localized name for. If null current UI culture is used.</param>
        ''' <returns>Localized or other name of the block. See remarks for details.</returns>
        ''' <remarks>
        ''' This method atempts to obtain code-point name in following order:
        ''' <list type="list">
        ''' <item>From localization. If localization for the culture is not loaded and localization provider is available localizations are loaded using the provider.</item>
        ''' <item><see cref="Name"/>.</item>
        ''' </list>
        ''' </remarks>
        Public Function GetLocalizedName(culture As CultureInfo) As String
            If culture Is Nothing Then culture = CultureInfo.CurrentUICulture
            Dim ns = UcdLocalizationProvider.GetCultureNamespace(culture.Name)
            Dim extensions = Element.Document.Annotation(Of IDictionary(Of String, UnicodeCharacterDatabase))()
            Dim locucd As UnicodeCharacterDatabase
            If extensions IsNot Nothing AndAlso extensions.Containskey(ns) Then
                locucd = extensions(ns)
            End If
            If locucd Is Nothing Then
                Dim tmpucd = New UnicodeCharacterDatabase(Element.Document)
                tmpucd.GetLocalization(culture)
                locucd = extensions(ns)
            End If

            If locucd IsNot Nothing Then
                Dim lblock = locucd.FindBlock(FirstCodePoint.CodePoint)
                If lblock IsNot Nothing AndAlso lblock.FirstCodePoint.CodePoint = Me.FirstCodePoint.CodePoint AndAlso lblock.LastCodePoint.CodePoint = Me.LastCodePoint.CodePoint Then Return lblock.Name
            End If

            Return Name
        End Function
    End Class
    End Class
End Namespace
