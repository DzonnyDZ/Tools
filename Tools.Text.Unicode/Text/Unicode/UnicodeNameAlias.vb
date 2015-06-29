Imports System.Xml.Serialization
Imports Tools.ComponentModelT
Imports Tools.ExtensionsT
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>

Namespace TextT.UnicodeT
    ''' <summary>Represents Unicode name alias (from UCD's NameAliases.txt)</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <Serializable>
    <DebuggerDisplay("{Alias};{Type}")>
    Public Class UnicodeNameAlias
        Implements IXElementWrapper
        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <name-alias/>.Name

        Private ReadOnly _alias$
        ''' <summary>Gets the alias name</summary>
        <XmlAttribute("alias")>
        Public ReadOnly Property [Alias] As String
            Get
                If Element IsNot Nothing Then Return Element.@alias
                Return _alias
            End Get
        End Property

        Private ReadOnly _type As UnicodeNameAliasType
        ''' <summary>Gets alias type</summary>
        ''' <remarks>If name aliases were loaded form older version of UCD's NameAliases.txt this property can have value of <see cref="UnicodeNameAliasType.Unknown"/></remarks>
        <XmlAttribute("type")>
        Public ReadOnly Property Type As UnicodeNameAliasType
            Get
                If Element IsNot Nothing Then
                    Return If(Element.@type Is Nothing, UnicodeNameAliasType.Unknown, EnumCore.Parse(Of UnicodeNameAliasType)(Element.@type, True))
                End If
                Return _type
            End Get
        End Property

        ''' <summary>Gets XML element this instance wraps</summary>
        ''' <returns>An XML element this instance wraps, in case this instance has been constructed form XML element. Null otherwise.</returns>
        Public ReadOnly Property Element As XElement Implements IXElementWrapper.Element

        Private ReadOnly Property Node As XNode Implements IXNodeWrapper.Node
            Get
                Return Element
            End Get
        End Property

        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeNameAlias"/> class</summary>
        ''' <param name="alias$">Alias name</param>
        ''' <param name="type">Alias type</param>
        ''' <exception cref="ArgumentNullException"><paramref name="alias"/> is null</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="type"/> is not one of pre-defined <see cref="UnicodeNameAliasType"/> values</exception>
        Public Sub New(alias$, type As UnicodeNameAliasType)
            If [alias] Is Nothing Then Throw New ArgumentNullException(NameOf([alias]))
            If Not type.IsDefined Then Throw New InvalidEnumArgumentException(NameOf(type), type, type.GetType)
            _alias = [alias]
            _type = type
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeNameAlias"/> class</summary>
        ''' <param name="element">A <see cref="XElement"/> that represents this named sequence</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not element &lt;name-alias> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(element As XElement)
            If element Is Nothing Then Throw New ArgumentNullException(NameOf(element))
            If element.Name <> elementName Then Throw New ArgumentException(UnicodeResources.ex_InvalidXmlElement.f(elementName))
            Me.Element = element
        End Sub

        ''' <summary>Gets string representation of this object</summary>
        ''' <returns>String representation of this object in form <see cref="[Alias]"/>;<see cref="Type"/></returns>
        Public Overrides Function ToString() As String
            If Type = UnicodeNameAliasType.Unknown Then Return [Alias]
            Return String.Format("{0};{1}", [Alias], Type.ToString().ToLowerInvariant)
        End Function
    End Class
End Namespace
