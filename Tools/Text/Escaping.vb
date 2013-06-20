Imports System.Text
Imports System.Runtime.CompilerServices
Imports System.Web

Namespace TextT
#If Config <= Nightly Then
    ''' <summary>Contains various methods for string escaping</summary>
    ''' <version version="1.5.3">This module is new in version 1.5.3</version>
    ''' <version version="1.5.4">Changed namespace from <c>Text</c> to <see cref="TextT"/>.</version>
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

#Region "XML"
        ''' <summary>Makes string safe for XML (and HTML) by turning characters &amp; and &lt; to XML entities</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <param name="mode">Mode of entity creation</param>
        ''' <returns><paramref name="str"/> with characters &amp; and &lt; replaced with corresponding XML entities. Null when <paramref name="str"/> is null.</returns>
        ''' <seealso href="http://www.w3.org/TR/xml11/#sec-references">XML Character and Entity References</seealso>
        <Extension()>
        Public Function EscapeXml(ByVal str As String, Optional ByVal mode As Mode = Mode.Native) As String
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
        ''' <seealso href="http://www.w3.org/TR/xml11/#sec-references">XML Character and Entity References</seealso>
        <Extension()>
        Public Function EscapeXmlAttribute(ByVal str As String, Optional ByVal mode As Mode = Mode.Native, Optional ByVal surroundingQuotes As Quotes = Quotes.SingleOrDouble) As String
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
#End Region

        ''' <summary>Makes string safe for JavaScript (ECMA script) quoted string</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <param name="mode">Mode of escape sequence creation</param>
        ''' <param name="surroundingQuotes">Determines which quotes attribute is surronded with - allowing to escape only either ' or " or both.</param>
        ''' <returns><paramref name="str"/> with characters \, <see cref="Cr"/>, <see cref="Lf"/> and possibly ' and " (depending on <paramref name="surroundingQuotes"/>) replaced with corresponding escape sequences. Null when <paramref name="str"/> is null.</returns>
        ''' <seealso href="http://www.ecma-international.org/publications/files/ECMA-ST/ECMA-262.pdf#page=32">ECMA Script String Literals</seealso>
        <Extension()>
        Public Function EscapeJavaScript(ByVal str As String, Optional ByVal mode As Mode = Mode.Native, Optional ByVal surroundingQuotes As Quotes = Quotes.SingleOrDouble) As String
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
        ''' <seealso cref="String.Format"/>
        <Extension()>
        Public Function EscapeStringFormat(ByVal str As String) As String
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

#Region "SQL"
        ''' <summary>Makes string safe for SQL quoted string</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <returns><paramref name="str"/> with all occurences of single quote (') duplicated. Null when <paramref name="str"/> is null.</returns>
        ''' <remarks>
        ''' This function supports minimal SQL escaping (of single quote (') only). It does not support double-quoted strings supported by some database engines. It also does not support additional escape sequences supported by some database engines such as MySQL.
        ''' <para>
        ''' Following databases do not support additional escape sequences and it's safe to use this function with them: Microsft SQL, Oracle, Firebird, SQLite (list is not complete, only a few commonly used database engines' documentations were checked).
        ''' Some of databases supporting additional escaping are MySQL, PostgreSQL.
        ''' </para>
        ''' <note>No additional escaping is part of SQL standard.</note>
        ''' </remarks>
        ''' <seealso href="http://msdn.microsoft.com/en-us/library/ms179899.aspx">Constants (Transact-SQL)</seealso>
        ''' <seealso href="http://download.oracle.com/docs/cd/E11882_01/server.112/e10592/sql_elements003.htm#i42617">Oracle Text Literals</seealso>
        ''' <seelaso href="http://publib.boulder.ibm.com/infocenter/db2luw/v9r8/index.jsp?topic=/com.ibm.db2.luw.sql.ref.doc/doc/r0000731.html">DB2 Constants</seelaso>
        ''' <seealso href="http://www.firebirdsql.org/manual/qsg2-databases.html#qsg2-strings">Firebird - Thinks to know about strings</seealso>
        ''' <seelaso href="http://www.sqlite.org/lang_expr.html">SQLite expressions</seelaso>
        ''' <seelaso href="http://dev.mysql.com/doc/refman/5.0/en/string-syntax.html">MySQL Strings</seelaso>
        ''' <seelaso href="http://www.postgresql.org/docs/8.4/interactive/sql-syntax-lexical.html#SQL-SYNTAX-CONSTANTS">PostgreSQL String Constants</seelaso>
        <Extension()>
        Public Function EscapeSql(ByVal str As String) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            For Each ch In str
                Select Case ch
                    Case "'"c : b.Append("''")
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function

        ''' <summary>Makes string safe for MySQL quoted string</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <param name="surroundingQuotes">Determines which quotes attribute is surronded with - allowing to escape only either ' or " or both.</param>
        ''' <returns><paramref name="str"/> with some characters replaced by appropriate backslash escapes. Null when <paramref name="str"/> is null.</returns>
        ''' <remarks>This function escapes string for MySQL database engine with <c><see href="http://dev.mysql.com/doc/refman/5.0/en/server-sql-mode.html#sqlmode_no_backslash_escapes">NO_BACKSLASH_ESCAPES</see></c> off.
        ''' <para>
        ''' Characters replaced are: single quote ('; if <paramref name="surroundingQuotes"/> requires it),
        ''' double quote ("; if <paramref name="surroundingQuotes"/> requires it), null char, bacspace (\b, 0x8),
        ''' line feed (new line, \n, 0xA), carriage return (\r, 0xD), vertical tab (\t, 0x9), Control-Z (0x26), backslash (\).
        ''' </para>
        ''' </remarks>
        ''' <seelaso href="http://dev.mysql.com/doc/refman/5.0/en/string-syntax.html">MySQL Strings</seelaso>
        <Extension()>
        Public Function EscapeMySql(ByVal str As String, Optional ByVal surroundingQuotes As Quotes = Quotes.SingleOrDouble) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            For Each ch In str
                Select Case ch
                    Case "'"c : b.Append(If(surroundingQuotes.HasFlag(Quotes.Single), "\'", "'"))
                    Case """"c : b.Append(If(surroundingQuotes.HasFlag(Quotes.Single), "\""", """"))
                    Case NullChar : b.Append("\0")
                    Case Back : b.Append("\b")
                    Case Lf : b.Append("\n")
                    Case Cr : b.Append("\r")
                    Case Tab : b.Append("\t")
                    Case ChrW(26) : b.Append("\Z")
                    Case "\"c : b.Append("\\")
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function

        ''' <summary>Makes string safe for PostgreSQL quoted string</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <param name="mode">Mode of escape sequence creation. Mode <see cref="Mode.Universal"/> requires that resulting string is specified as Unicode escaped string in PostgreSQL code (U&amp;'').</param>
        ''' <returns><paramref name="str"/> with all occurences of single-quote (') duplicated (only when <paramref name="mode"/> is <see cref="Mode.Native"/>) and a few other charactres replaced with appropriate backslash escape sequences. Null when <paramref name="str"/> is null.</returns>
        ''' <remarks>
        ''' Characters replaces by escape sequences are: single quote ('; when <paramref name="mode"/> is <see cref="Mode.Native"/> replaced with '' otherwise with \0027),
        ''' backspace (\b, 0x8), line feed (new line, \n, 0xA), carriage return (\r, 0xD), horizontal tab (\t, 0x9), backshash (\).
        ''' <note>This requires PostrgeSQL option <see href="http://www.postgresql.org/docs/8.4/interactive/runtime-config-compatible.html#GUC-STANDARD-CONFORMING-STRINGS">standard_conforming_strings</see> to be off.</note>
        ''' <note>Whan <paramref name="mode"/> is <see cref="Mode.Universal"/> value returned by this function is notappropriate for "normal" PostgeSQL string literals. It must be used in Unicode string literals (U&amp;'').</note>
        ''' </remarks>
        ''' <seelaso href="http://www.postgresql.org/docs/8.4/interactive/sql-syntax-lexical.html#SQL-SYNTAX-CONSTANTS">PostgreSQL String Constants</seelaso>
        Public Function EscapePostgreSql(ByVal str As String, ByVal mode As Mode) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            For Each ch In str
                Select Case ch
                    Case "'"c : b.Append(If(mode = mode.Native, "''", "\0027"))
                    Case Back : b.Append(If(mode = mode.Native, "\b", "\0008"))
                    Case Lf : b.Append(If(mode = mode.Native, "\n", "\000A"))
                    Case Cr : b.Append(If(mode = mode.Native, "\r", "\000D"))
                    Case Tab : b.Append(If(mode = mode.Native, "\t", "\0009"))
                    Case "\"c : b.Append("\\")
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function

        ''' <summary>Escapes given string for right side of SQL LIKE operator to be treated as exact match.</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <returns><paramref name="str"/> with all ocurences of underscore (_) and percent sign (%) backslash-escaped. Null when <paramref name="str"/> is null.</returns>
        ''' <remarks>In conformat SQL implementations this string can be used on right side of LIKE operator only when followed by <c>ESCAPE '\'</c>. Some implementations (e.g. MySQL) may not require it.
        ''' <para>This function does not make string safe for any kind of SQL sttring literal. It only makes it safe for right side of the LIKE operator (if passed there in proper form). Use <see cref="EscapeSql"/> or SQL-engine-specific function to make string safe for SQL string.</para></remarks>
        ''' <version version="1.5.4">Fix: Backslash (\) was not escaped</version>
        Public Function EscapeSqlLike(ByVal str As String) As String
            Return EscapeSqlLike(str, "\"c)
        End Function

        ''' <summary>Escapes given string for right side of SQL LIKE operator to be treated as exact match.</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <returns><paramref name="str"/> with all ocurences of underscore (_) and percent sign (%) escaped. Null when <paramref name="str"/> is null.</returns>
        ''' <remarks>In conformat SQL implementations this string can be used on right side of LIKE operator only when followed by <c>ESCAPE '<paramref name="escape"/>'</c>. Some implementations (e.g. MySQL) may not require it.
        ''' <para>This function does not make string safe for any kind of SQL sttring literal. It only makes it safe for right side of the LIKE operator (if passed there in proper form). Use <see cref="EscapeSql"/> or SQL-engine-specific function to make string safe for SQL string.</para></remarks>
        ''' <version version="1.5.4">This overload is new in version 1.5.4</version>
        Public Function EscapeSqlLike(str As String, escape As Char) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            For Each ch In str
                Select Case ch
                    Case "_"c : b.Append(escape & "_")
                    Case "%"c : b.Append(escape & "%")
                    Case escape : b.Append(escape & escape)
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function
#End Region

        ''' <summary>Makes string safe for regular (non-verbatim) C# string (double-quoted)</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <param name="mode">Mode of escape sequence creation</param>
        ''' <returns><paramref name="str"/> with some characters replaced by native or universal escape sequences (depending on value of the <paramref name="mode"/>) parameter. Null when <paramref name="str"/> is null.</returns>
        ''' <remarks>
        ''' This function should be used for escaping strings that will be placed inside double-quoted C# string literal. It's not suitable for single-quoted character literals and it's not suitable for C# verbatim strings (@"").
        ''' <para>This function replaces following characters with appropriate C# escape sequences: double quote ("), backslash (\), null character, alert (0x7, \a), backspace (0x8, \b), form feed (0xC, \f), new line (line feed, 0xA, \n), carriage return (0xD, \r), horizontal tab (0x9, \t), vertical tab (0xB, \v).</para>
        ''' </remarks>
        <Extension()>
        Public Function EscapeCSharp(ByVal str As String, Optional ByVal mode As Mode = Mode.Native) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            For Each ch In str
                Select Case ch
                    'Case "'"c : b.Append(If(mode = mode.Native, "\'", "\u0027"))
                    Case """"c : b.Append(If(mode = mode.Native, "\""", "\u0022"))
                    Case "\"c : b.Append(If(mode = mode.Native, "\\", "\u005C"))
                    Case NullChar : b.Append(If(mode = mode.Native, "\0", "\u0000"))
                    Case Alert : b.Append(If(mode = mode.Native, "\a", "\u0007"))
                    Case Back : b.Append(If(mode = mode.Native, "\b", "\u0008"))
                    Case FormFeed : b.Append(If(mode = mode.Native, "\f", "\u000C"))
                    Case Lf : b.Append(If(mode = mode.Native, "\n", "\u000A"))
                    Case Cr : b.Append(If(mode = mode.Native, "\r", "\u000D"))
                    Case Tab : b.Append(If(mode = mode.Native, "\t", "\u0009"))
                    Case VerticalTab : b.Append(If(mode = mode.Native, "\v", "\u000B"))
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function

        ''' <summary>Makes string safe for C/C++ string</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <param name="mode">Mode of escape sequence creation</param>
        ''' <param name="surroundingQuotes">Determines which quotes attribute is surronded with - allowing to escape only either ' or " or both.</param>
        ''' <returns><paramref name="str"/> with some characters replaced by native or universal escape sequences (depending on value of the <paramref name="mode"/>) parameter. Null when <paramref name="str"/> is null.</returns>
        ''' <remarks>
        ''' This function replaces following characters: single quote ('; only when <paramref name="surroundingQuotes"/> requires it),
        ''' double quote (", only when <paramref name="surroundingQuotes"/> requires it, backslash (\), alert (bell, \a, 0x7),
        ''' backspace (\b, 0x8), form feed (\f, 0xC), line feed (new line, \n, 0xA), carriage return (\r, 0xD), horizontal tab (\t, 0x9),
        ''' vertical tab (\v, 0xB), question mark when it is 2nd question mark in trigraph (??=, ??(, ??/, ??), ??', ??&lt;, ??!, ??>, ??-).</remarks>
        ''' <seealso href="http://msdn.microsoft.com/en-us/library/h21280bw.aspx">C++ Escape sequences</seealso>
        ''' <seealso href="http://msdn.microsoft.com/en-us/library/bt0y4awe.aspx">C++ Trigraphs</seealso>
        <Extension()>
        Public Function EscapeC(ByVal str As String, Optional ByVal mode As Mode = Mode.Native, Optional ByVal surroundingQuotes As Quotes = Quotes.SingleOrDouble) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            Dim i As Integer = 0
            For Each ch In str
                Select Case ch
                    Case "'"c : b.Append(If(surroundingQuotes.HasFlag(Quotes.Single), If(mode = mode.Native, "\'", "\x0027"), "'"))
                    Case """"c : b.Append(If(surroundingQuotes.HasFlag(Quotes.Double), If(mode = mode.Native, "\""", "\x0022"), "'"))
                    Case "\"c : b.Append(If(mode = mode.Native, "\\", "\x005C"))
                    Case Alert : b.Append(If(mode = mode.Native, "\a", "\x0007"))
                    Case Back : b.Append(If(mode = mode.Native, "\b", "\x0008"))
                    Case FormFeed : b.Append(If(mode = mode.Native, "\f", "\x000C"))
                    Case Lf : b.Append(If(mode = mode.Native, "\n", "\x000A"))
                    Case Cr : b.Append(If(mode = mode.Native, "\r", "\x000D"))
                    Case Tab : b.Append(If(mode = mode.Native, "\t", "\x0009"))
                    Case VerticalTab : b.Append(If(mode = mode.Native, "\v", "\x000B"))
                    Case "?"c
                        If i > 0 AndAlso str(i - 1) = "?"c AndAlso i < str.Length - 1 Then
                            Select Case str(i + 1)
                                Case "="c, "("c, "/"c, ")"c, "'"c, "<"c, "!"c, ">"c, "-"c
                                    b.Append(If(mode = mode.Native, "\?", "\x003F"))
                                Case Else : b.Append("?"c)
                            End Select
                        Else : b.Append("?"c)
                        End If
                    Case Else : b.Append(ch)
                End Select
                i += 1
            Next
            Return b.ToString
        End Function

#Region "PHP"
        ''' <summary>Makes string safe for PHP single-quoted string</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <returns><paramref name="str"/> with backslash-escaped all occurences of backslash (\) and single quote (').</returns>
        ''' <remarks>Use this function only for single-quoted PHP strings as PHP handles single-quoted and double-quoted string "literals" different way.
        ''' <note>PHP has not native support of Unicode. It can deal with UTF-8-encoded strings as if being ANSI.</note></remarks>
        ''' <seealso href="http://www.php.net/manual/en/language.types.string.php#language.types.string.syntax.single">PHP Sigle quoted strings</seealso>
        <Extension()>
        Public Function EscapePhpSingle(ByVal str As String) As String
            If str = "" Then Return ""
            Dim b As New StringBuilder(str.Length)
            Dim i As Integer = 0
            For Each ch In str
                Select Case ch
                    Case "\"c : b.Append("\\")
                    Case "'" : b.Append("\'")
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function

        ''' <summary>Makes string safe for PHP double-quoted string</summary>
        ''' <param name="str">String to be made safe</param>
        ''' <param name="mode">Mode of escape sequence creation</param>
        ''' <returns><paramref name="str"/> with some characters backslash-escaped with either special or universal ASCII escape sequences (depending on <paramref name="mode"/>).</returns>
        ''' <remarks>Use this function only for double-quoted PHP strings as PHP handles single-quoted and double-quoted string "literals" different way.
        ''' <para>Characters escaped are: backslash (\), line feed (new line, \n, 0xA), carriage return (\r, 0xD),
        ''' horizontal tab (\t, 0x9), vertical tab (\v, 0xB), form feed (\f, 0xC), double quote ("), dollar sign ($).</para>
        ''' <note>PHP support for escape sequences \v (vertical tab) and \f (form feed) is new in version 5.2.5. If you are targeting older version of PHP interpreter you must use <see cref="Mode.Universal">Universal</see> <paramref name="mode"/> to make PHP interpreter understand the string literal properly.</note>
        ''' <note>PHP has not native support of Unicode. It can deal with UTF-8-encoded strings as if being ANSI. Universal escape sequences genereted when <paramref name="mode"/> is <see cref="Mode.Universal"/> represent ASCII rather than Unicode values.</note></remarks>
        ''' <seealso href="http://www.php.net/manual/en/language.types.string.php#language.types.string.syntax.double">PHP Double quoted strings</seealso>
        <Extension()>
        Public Function EscapePhpDouble(ByVal str As String, Optional ByVal mode As Mode = Mode.Native) As String
            If str = "" Then Return ""
            Dim b As New StringBuilder(str.Length)
            Dim i As Integer = 0
            For Each ch In str
                Select Case ch
                    Case "\"c : b.Append(If(mode = mode.Native, "\\", "\x5C"))
                    Case Lf : b.Append(If(mode = mode.Native, "\n", "\x0A"))
                    Case Cr : b.Append(If(mode = mode.Native, "\r", "\x0D"))
                    Case Tab : b.Append(If(mode = mode.Native, "\t", "\x09"))
                    Case VerticalTab : b.Append(If(mode = mode.Native, "\v", "\x0B"))
                    Case FormFeed : b.Append(If(mode = mode.Native, "\f", "\x0C"))
                    Case """"c : b.Append(If(mode = mode.Native, "\""", "\x22"))
                    Case "$" : b.Append(If(mode = mode.Native, "\$", "\x24"))
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function
#End Region

        ''' <summary>Makes string safe for Visual Basic Like operator right side</summary>
        ''' <param name="str">String to be mader safe</param>
        ''' <returns><paramref name="str"/> with occurences of cpecial characters turned to one-character-long character groups. Null when <paramref name="str"/> is null.</returns>
        ''' <remarks>Characters replaced are:
        ''' <list type="table"><listheader><term>Character</term><description>Replaced with</description></listheader>
        ''' <item><term>?</term><description>[?]</description></item>
        ''' <item><term>*</term><description>[*]</description></item>
        ''' <item><term>#</term><description>[#]</description></item>
        ''' <item><term>[</term><description>[[]</description></item>
        ''' </list>
        ''' This function does not make <paramref name="str"/> safe for Visual Basic string literals. It's only made safe to represent exact match for Visual Basic like operator when right hand of the operator is variable or function implementing the operator is called.
        ''' <note>The <see cref="Escaping"/> module does not provide any function for escaping of Visual Basic strings (like it is provided for C#, PHP and other languages. This is because there is no universal way how to make string literal safe for Visual Basic. Certain characters simply cannot be present within Visual Basic string - even not as escape sequences. The only escape sequence supported by Visual Basic is " becomes "".</note>
        ''' </remarks>
        ''' <seealso href="http://msdn.microsoft.com/en-us/library/swf8kaxw.aspx">Like Operator (Visual Basic)</seealso>
        ''' <seealso cref="Microsoft.VisualBasic.CompilerServices.LikeOperator.LikeString"/>
        ''' <seealso cref="Microsoft.VisualBasic.CompilerServices.Operators.LikeString"/>
        ''' <seealso cref="Microsoft.VisualBasic.CompilerServices.StringType.StrLike"/>
        <Extension()>
        Public Function EscapeVBLike(ByVal str As String) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            For Each ch In str
                Select Case ch
                    Case "?"c : b.Append("[?]")
                    Case "*"c : b.Append("[*]")
                    Case "#"c : b.Append("[#]")
                    Case "["c : b.Append("[[]")
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function

#Region "URL"
        ''' <summary>Encodes a URL string.</summary>
        ''' <param name="str">The text to encode.</param>
        ''' <returns>An encoded string.</returns>
        ''' <remarks>This function internally uses <see cref="HttpUtility.UrlEncode"/>. In future releases this might be changed to use own implementation of URL encoding.</remarks>
        ''' <seealso cref="HttpUtility.UrlEncode"/>
        <Extension()>
        Public Function EscapeUrl(ByVal str As String) As String
            If str = "" Then Return str
            Return HttpUtility.UrlEncode(str)
        End Function
        ''' <summary>Encodes the path portion of a URL string for reliable HTTP transmission from the Web server to a client.</summary>
        ''' <param name="str">The text to URL-encode.</param>
        ''' <returns>The URL-encoded text.</returns>
        ''' <remarks>This function internally uses <see cref="HttpUtility.UrlPathEncode"/>. In future releases this might be changed to use own implementation of URL encoding.</remarks>
        ''' <seealso cref="HttpUtility.UrlPathEncode"/>
        <Extension()>
        Public Function EscapeUrlPath(ByVal str As String) As String
            If str = "" Then Return str
            Return HttpUtility.UrlPathEncode(str)
        End Function
#End Region

        ''' <summary>Makes string safe for CSS single- or double-quoted string literal</summary>
        ''' <param name="str">String to me made safe</param>
        ''' <param name="mode">Mode of escape sequence creation</param>
        ''' <param name="surroundingQuotes">Determines which quotes attribute is surronded with - allowing to escape only either ' or " or both.</param>
        ''' <returns><paramref name="str"/> with some characters replaced with escape sequences. Null when <paramref name="str"/> is null.</returns>
        ''' <remarks>Following characters are encoded:
        ''' <list type="table"><listheader><term>Character</term><description>Encoded as</description></listheader>
        ''' <item><term>Single quote (')</term><description>\' or \27 (depending on <paramref name="mode"/>; not encoded when <paramref name="surroundingQuotes"/> does not contain <see cref="Quotes.[Single]"/>)</description></item>
        ''' <item><term>Double quote (")</term><description>\" or \22 (depending on <paramref name="mode"/>; not encoded when <paramref name="surroundingQuotes"/> does not contain <see cref="Quotes.[Double]"/>)</description></item>
        ''' <item><term>Backslash (\)</term><description>\\ or \5C (depending on <paramref name="mode"/>)</description></item>
        ''' <item><term>Null char</term><description>\0</description></item>
        ''' <item><term>Alert (bell, 0x7)</term><description>\7</description></item>
        ''' <item><term>Form feed (\f, 0x8)</term><description>\8</description></item>
        ''' <item><term>Line feed (new line, \n, 0xA)</term><description>\A</description></item>
        ''' <item><term>Carriage return (\r, 0xD)</term><description>\D</description></item>
        ''' <item><term>Vertical tab (\v, 0xB)</term><description>\B</description></item>
        ''' <item><term>Horizontal tab (\t, 0x9)</term><description>\9</description></item>
        ''' <item><term>Ampresand (&amp;)</term><description>\26</description></item>
        ''' <item><term>Less than (&lt;)</term><description>\3C</description></item>
        ''' </list>
        ''' <para>
        ''' Only single quote ('), double quote (") and backslash (\) support native mode escaping. All other characters are always escaped using numeric escapes.
        ''' All numeric escapes are always followed by space. This is CSS standard feature how to terminate variable-lenght escape sequence. Conformant User agent ignores such space.
        ''' </para>
        ''' <para>
        ''' Many CSS escapes are not necessayr and are provided for convenience.
        ''' Espacially &amp; and &lt; escape sequences ensure that resulting CSS string can be also safely embdeded in HTML (XML) text node (with proper value of <paramref name="surroundingQuotes"/> in attribute value as well).
        ''' </para>
        ''' <para>Way of escaping provided by this function is compatible with CSS2 specification and CSS3 draft.</para>
        ''' </remarks>
        <Extension()>
        Public Function EscapeCss(ByVal str As String, Optional ByVal mode As Mode = Mode.Native, Optional ByVal surroundingQuotes As Quotes = Quotes.SingleOrDouble) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            For Each ch In str
                Select Case ch
                    Case "'"c : b.Append(If(surroundingQuotes.HasFlag(Quotes.Single), If(mode = mode.Native, "\'", "\27 "), "'"))
                    Case """"c : b.Append(If(surroundingQuotes.HasFlag(Quotes.Double), If(mode = mode.Native, "\""", "\22 "), """"))
                    Case "\"c : b.Append(If(mode = mode.Native, "\\", "\5C "))
                    Case NullChar : b.Append("\0 ")
                    Case Alert : b.Append("\7 ")
                    Case Back : b.Append("\8 ")
                    Case FormFeed : b.Append("\C ")
                    Case Lf : b.Append("\A ")
                    Case Cr : b.Append("\D ")
                    Case Tab : b.Append("\9 ")
                    Case VerticalTab : b.Append("\B ")
                    Case Tab : b.Append("\9 ")
                    Case "&"c : b.Append("\26 ")
                    Case "<" : b.Append("\3C ")
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function

        ''' <summary>Escapes given string for .NET regular expressions. All characters with special meaning in regular expressions are replaced wint appropriate escape sequences.</summary>
        ''' <param name="str">String to me made safe</param>
        ''' <param name="mode">Mode of escape sequence creation</param>
        ''' <returns><paramref name="str"/> with some characters replaced. Null when <paramref name="str"/> is null.</returns>
        ''' <remarks>
        ''' Characters replaced are special characters bell: (\a, 0x7), backspace (\b, 0x8), vertical tab (\t, 0x9), line feed (new line, \n, 0xA), 
        ''' vertical tab (\v, 0xB), form feed (\f, 0xC), carriage return (\r, 0xD), escape (\e, 0x1B). And characters with
        ''' special meaning for regular expressions: dot (.), dollar sign ($), circumflex (carret, ^), braces ({}), square brackets ([]), parentheses (()),
        ''' pipe (vertical bar, |), asterisk (*), plus sign (+), question mark (?), backslash (\).
        ''' </remarks>
        <Extension()>
        Public Function EscapeRegEx(ByVal str As String, Optional ByVal mode As Mode = Mode.Native) As String
            If str = "" Then Return str
            Dim b As New StringBuilder(str.Length)
            For Each ch In str
                Select Case ch
                    Case Alert : b.Append(If(mode = mode.Native, "\a", "\u0007"))
                    Case Back : b.Append(If(mode = mode.Native, "\b", "\u0008"))
                    Case Tab : b.Append(If(mode = mode.Native, "\t", "\u0009"))
                    Case Cr : b.Append(If(mode = mode.Native, "\r", "\u000D"))
                    Case VerticalTab : b.Append(If(mode = mode.Native, "\v", "\u000B"))
                    Case FormFeed : b.Append(If(mode = mode.Native, "\f", "\u000C"))
                    Case Lf : b.Append(If(mode = mode.Native, "\n", "\u000A"))
                    Case Escape : b.Append(If(mode = mode.Native, "\e", "\u001B"))
                    Case "."c, "$"c, "^"c, "{"c, "}"c, "["c, "]"c, "("c, ")"c, "|"c, "*"c, "+"c, "?"c, "\"c
                        b.Append(If(mode = mode.Native, "\" & ch, String.Format("\u{0:H4}", AscW(ch))))
                    Case Else : b.Append(ch)
                End Select
            Next
            Return b.ToString
        End Function
    End Module
#End If
End Namespace