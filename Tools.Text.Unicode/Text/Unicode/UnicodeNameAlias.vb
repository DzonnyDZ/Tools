Imports System.Xml.Serialization

Namespace TextT.UnicodeT
    ''' <summary>Represents Unicode name alias (from UCD's NameAliases.txt)</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <Serializable>
    <DebuggerDisplay("{Alias};{Type}")>
    Public Class UnicodeNameAlias
        ''' <summary>Gets the alias name</summary>
        <XmlAttribute("alias")>
        Public ReadOnly Property [Alias] As String
        ''' <summary>Gets alias type</summary>
        ''' <remarks>If name aliases were loaded form older version of UCD's NameAliases.txt this property can have value of <see cref="UnicodeNameAliasType.Unknown"/></remarks>
        <XmlAttribute("type")>
        Public ReadOnly Property Type As UnicodeNameAliasType

        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeNameAlias"/> class</summary>
        ''' <param name="alias$">Alias name</param>
        ''' <param name="type">Alias type</param>
        ''' <exception cref="ArgumentNullException"><paramref name="alias"/> is null</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="type"/> is not one of pre-defined <see cref="UnicodeNameAliasType"/> values</exception>
        Public Sub New(alias$, type As UnicodeNameAliasType)
            If [alias] Is Nothing Then Throw New ArgumentNullException(NameOf([alias]))
            If Not type.IsDefined Then Throw New InvalidEnumArgumentException(NameOf(type), type, type.GetType)
            Me.Alias = [alias]
            Me.Type = type
        End Sub

        ''' <summary>Gets string representation of this object</summary>
        ''' <returns>String representation of this object in form <see cref="[Alias]"/>;<see cref="Type"/></returns>
        Public Overrides Function ToString() As String
            If Type = UnicodeNameAliasType.Unknown Then Return [Alias]
            Return String.Format("{0};{1}", [Alias], Type.ToString().ToLowerInvariant)
        End Function
    End Class

    ''' <summary>Types of Unicode name aliases</summary>
    ''' <version version="1.5.4">This enum is new in version 1.5.4</version>
    Public Enum UnicodeNameAliasType
        ''' <summary>The alias type is not known or was not loaded</summary>
        ''' <remarks>This can happen when older version of UCD NameAliases.txt is loaded</remarks>
        Unknown = 0
        ''' <summary>Corrections for serious problems in the character names</summary>
        Correction = 1
        ''' <summary>ISO 6429 names for C0 and C1 control functions, and other commonly occurring names For control codes</summary>
        Control = 2
        ''' <summary>A few widely used alternate names for format characters</summary>
        Alternate = 3
        ''' <summary>Several documented labels for C1 control code points which were never actually approved In any standard</summary>
        Figment = 4
        ''' <summary>Commonly occurring abbreviations (or acronyms) for control codes, format characters, spaces, And variation selectors</summary>
        Abbreviation = 5
    End Enum
End Namespace
