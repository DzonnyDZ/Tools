Imports System.Text
Imports System.Runtime.CompilerServices

Namespace Text
#If Config <= Nightly Then
    ''' <summary>Contains various methods for string escaping</summary>
    ''' <version version="1.5.3">This module is new in version 1.5.3</version>
    Module Escaping
#Region "Enums"
        ''' <summary>Indicates possible types of qotes used in many programming languages</summary>
        <Flags()>
        Public Enum Quotes
            ''' <summary>Single quotes (i.e. apostrophes) (')</summary>
            [Single] = 1
            ''' <summary>Double quotes (")</summary>
            [Double] = 2
            ''' <summary>Expect both - single(') or double (") quotes</summary>
            [SingleOrDouble] = [Single] Or [Double]
        End Enum
        ''' <summary>Defines possible escaping modes that can be used by many programming languages</summary>
        Public Enum Mode
            ''' <summary>Attempts to use character specific escape sequance when defined (like &amp;lt; for &lt; in HTML or \r for carriage return in C-based languages)</summary>
            Native
            ''' <summary>Always uses universal unicode escaping sequence (like &amp;#60; for &lt; in HTML or \u000A for carriage return in JavaScript)</summary>
            Universal
        End Enum
#End Region
        ''' <summary>Makes string safe for XML (and HTML) by turning characters &amp; and &lt; to XML entities</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <param name="mode">Mode of entity creation</param>
        ''' <returns><paramref name="str"/> with characters &amp; and &lt; replaced with corresponding XML entities. Null when <paramref name="str"/> is null.</returns>
        <Extension()>
        Public Function XmlEscape(ByVal str As String, Optional ByVal mode As Mode = Mode.Native) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            For Each ch In str
                Select Case ch
                    Case "<"c : b.Append(If(mode = mode.Native, "&lt;", "&#60;"))
                    Case "&"c : b.Append(If(mode = mode.Native, "&amp;", "&#39;"))
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function
        ''' <summary>Makes string safe for XML (and HTML) attributes by turning characters &amp;, &lt;, " and ' to XML entities</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <param name="mode">Mode of entity creation</param>
        ''' <param name="surroundingQuotes">Determines which quotes attribute is surronded with - allowing to escape only either ' or " or both.</param>
        ''' <returns><paramref name="str"/> with characters &amp; and &lt; and possibly ' and " (depending on <paramref name="surroundingQuotes"/>) replaced with corresponding XML entities. Null when <paramref name="str"/> is null.</returns>
        <Extension()>
        Public Function XmlAttributeEscape(ByVal str As String, Optional ByVal mode As Mode = Mode.Native, Optional ByVal surroundingQuotes As Quotes = Quotes.SingleOrDouble) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            For Each ch In str
                Select Case ch
                    Case "<"c : b.Append(If(mode = mode.Native, "&lt;", "&#60;"))
                    Case "&"c : b.Append(If(mode = mode.Native, "&amp;", "&#39;"))
                    Case """"c : b.Append(If(surroundingQuotes.HasFlag(Quotes.Double), If(mode = mode.Native, "&quot;", "&#34;"), """"))
                    Case "'"c : b.Append(If(surroundingQuotes.HasFlag(Quotes.Single), If(mode = mode.Native, "&apos;", "&#39;"), "'"))
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function

        ''' <summary>Makes string safe for JavaScript quoted string</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <param name="mode">Mode of entity creation</param>
        ''' <param name="surroundingQuotes">Determines which quotes attribute is surronded with - allowing to escape only either ' or " or both.</param>
        ''' <returns><paramref name="str"/> with characters \, <see cref="Cr"/>, <see cref="Lf"/> and possibly ' and " (depending on <paramref name="surroundingQuotes"/>) replaced with corresponding escape sequences. Null when <paramref name="str"/> is null.</returns>
        <Extension()>
        Public Function JavaScriptEscape(ByVal str As String, Optional ByVal mode As Mode = Mode.Native, Optional ByVal surroundingQuotes As Quotes = Quotes.SingleOrDouble) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            For Each ch In str
                Select Case ch
                    Case "\"c : b.Append(If(mode = Escaping.Mode.Native, "\\", "\u005C"))
                    Case """"c : b.Append(If(surroundingQuotes.HasFlag(Quotes.Double), If(mode = mode.Native, "\""", "\u0022"), """"))
                    Case "'"c : b.Append(If(surroundingQuotes.HasFlag(Quotes.Single), If(mode = mode.Native, "\'", "\u0027"), "'"))
                    Case Cr : b.Append(If(mode = Escaping.Mode.Native, "\r", "\u000D"))
                    Case Lf : b.Append(If(mode = Escaping.Mode.Native, "\n", "\u000A"))
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function
        ''' <summary>Makes string part safe for <see cref="String.Format"/> function so it does not recognize any formating placeholders in it. This is achievend by duplicating all {s and }s.</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <returns><paramref name="str"/> witch characters { and } duplicated. Null when <paramref name="str"/> is null.</returns>
        <Extension()>
        Public Function StringFormatEscape(ByVal str As String) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            For Each ch In str
                Select Case ch
                    Case "{"c : b.Append("{{")
                    Case "}"c : b.Append("}}")
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function
        'TODO: SQL, C#, C, C++, PHP, VB LIKE, SQL LIKE, URL, URLArg
    End Module
#End If
End Namespace