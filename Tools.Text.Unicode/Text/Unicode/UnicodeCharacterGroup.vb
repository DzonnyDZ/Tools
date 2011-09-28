Imports System.Xml.Linq
Imports System.Linq
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>
Imports Tools.ExtensionsT

Namespace TextT.UnicodeT
    ''' <summary>Describes a group of Unicode characters which share common properties</summary>
    ''' <remarks>
    ''' Character group is non-normative construct used to describe properties that are common to some code points.
    ''' Do not confuse Group with Block. Main purpose for existence of groups is to reduce size of XML file.
    ''' Unicode provides grouped and flat files. Grouped files contain groups.
    ''' Flat files contain characters directly with all properties indicated for each character separatelly.
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class UnicodeCharacterGroup : Inherits UnicodePropertiesProvider
        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <group/>.Name
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeCharacterGroup"/> class</summary>
        ''' <param name="element">A &lt;group> XML element which contains data for this group</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not element &lt;group> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(element As XElement)
            MyBase.New(element)
            If element.Name <> elementName Then Throw New ArgumentException("Element must be {0}".f(elementName))
        End Sub

        ''' <summary>Gets Unicode code points contained in this group</summary>
        Public ReadOnly Property CodePoints As IEnumerable(Of UnicodeCodePoint)
            Get
                Return From el In Element.<char>.Union(Element.<reserved>).Union(Element.<noncharacter>).Union(Element.<surrogate>).InDocumentOrder
                       Select UnicodeCodePoint.Create(el)
            End Get
        End Property
    End Class
End Namespace
