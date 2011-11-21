Imports System.Text
Imports System.Linq
Imports System.Numerics

Namespace NumericsT
    ''' <summary>Provides static methods for numeric conversions</summary>
    ''' <version version="1.5.4">This module is new in version 1.5.4</version>
    Public Module ConversionsT

        ''' <summary>Converts numeric value to any base</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="toBase">Target base for conversion</param>
        ''' <param name="targetAlphabet">Array of characters to be used to render number in base <paramref name="toBase"/>. When null default is used. Default alphabet is uppercase. (0th character is for 0, 1ts for 1, 2nd for 3 etc.)</param>
        ''' <returns>A string representing number <paramref name="value"/> in base <paramref name="toBase"/></returns>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is negative -or- <paramref name="targetAlphabet"/> is null and <paramref name="toBase"/> is greater than 36.</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="toBase"/> is less than or equal to zero</exception>
        ''' <exception cref="ArgumentException"><paramref name="targetAlphabet"/> is not null and length of <paramref name="targetAlphabet"/> differs form <paramref name="toBase"/>.</exception>
        ''' <remarks>
        ''' This method does not enforce <paramref name="targetAlphabet"/> to contain only distinct cahacters. However in cvase there are duplicate characters in <paramref name="targetAlphabet"/> return value can is not unique per <paramref name="value"/>.
        ''' <para>Base 1 is supported. 0 is converted to an empty string. Non-zero is converted to string containing <paramref name="value"/>-times repeated character <paramref name="targetAlphabet"/>[0] ('1' if <paramref name="targetAlphabet"/> is null).</para>
        ''' </remarks>
        Public Function Dec2Xxx(value As BigInteger, toBase As Integer, Optional targetAlphabet As Char() = Nothing) As String
            If value < 0 Then Throw New NotSupportedException(ResourcesT.Exceptions.NegativeValueUnsupported)
            If toBase <= 0 Then Throw New ArgumentOutOfRangeException("toBase", ResourcesT.Exceptions.InvallidNumberBase)
            If targetAlphabet Is Nothing Then targetAlphabet = GetAlphabet(toBase)
            If targetAlphabet.Length <> toBase Then Throw New ArgumentException(ResourcesT.Exceptions.AlphabetLenghtBaseMismatch, "targetAlphabet")

            If toBase = 1 Then Return New String(targetAlphabet(0), value)
            If value = 0 Then Return targetAlphabet(0)
            Dim ret As New StringBuilder
            Dim remainder = value
            While remainder > 0
                Dim modulo = remainder Mod toBase
                ret.Insert(0, targetAlphabet(modulo))
                remainder = remainder / toBase
            End While
            Return ret.ToString
        End Function

        ''' <summary>Converts a number represneted as string in any base to numeric value</summary>
        ''' <param name="value">String representing number in base <paramref name="fromBase"/></param>
        ''' <param name="fromBase">Base <paramref name="value"/> represents number in</param>
        ''' <param name="sourceAlphabet">An array of characters that defines numerals used in <paramref name="value"/>. Default alphabet is used if this parameter is null. (0th character represents numeral for 0, 1ts for 1, 2nd for 2, 3rd for 3 etc.)</param>
        ''' <param name="comparer">Used to compare characters from <paramref name="value"/> and <paramref name="sourceAlphabet"/>. If null <see cref="CharComparer.InvariantCultureIgnoreCase"/> is used. <note>Default alphabet used when <paramref name="sourceAlphabet"/> is null is uppercase.</note></param>
        ''' <returns>A numeric value representing number <paramref name="value"/> converted form base <paramref name="fromBase"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="value"/> is an empty string and <paramref name="fromBase"/> is not 1. -or-
        ''' <paramref name="sourceAlphabet"/> is not null and length of <paramref name="sourceAlphabet"/> differs from <paramref name="fromBase"/>. -or-
        ''' <paramref name="sourceAlphabet"/> contains duplicate character (as indicated by <paramref name="comparer"/>).
        ''' </exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="fromBase"/> is less than or equal to zero.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="sourceAlphabet"/> is null and <paramref name="fromBase"/> is greater than 36.</exception>
        ''' <exception cref="FormatException"><paramref name="value"/> contains character that is not present in <paramref name="sourceAlphabet"/> (or default aplahabet when <paramref name="sourceAlphabet"/> is null). Equality is tested by <paramref name="comparer"/>.</exception>
        ''' <remarks>Base 1 is supported. Value 0 is represented as empty string. Other value s are representsed as string containing only character <paramref name="sourceAlphabet"/>[0] ('1' if <paramref name="sourceAlphabet"/> isnull) repeated. The lenght of the string is value of the number.</remarks>
        Public Function Xxx2Dec(value As String, fromBase As Integer, Optional sourceAlphabet As Char() = Nothing, Optional comparer As IEqualityComparer(Of Char) = Nothing) As BigInteger
            If value Is Nothing Then Throw New ArgumentNullException("value")
            If value = "" AndAlso fromBase > 1 Then Throw New ArgumentException(ResourcesT.Exceptions.EmptyNumberNotAllowedInBase, "value")
            If fromBase <= 0 Then Throw New ArgumentOutOfRangeException("fromBase", ResourcesT.Exceptions.InvallidNumberBase)
            If sourceAlphabet Is Nothing Then sourceAlphabet = GetAlphabet(fromBase)
            If sourceAlphabet.Length <> fromBase Then Throw New ArgumentException(ResourcesT.Exceptions.AlphabetLenghtBaseMismatch, "sourceAlphabet")
            If comparer Is Nothing Then comparer = CharComparer.InvariantCultureIgnoreCase
            Dim lookup As New Dictionary(Of Char, Integer)(comparer)
            For i = 0 To sourceAlphabet.Length - 1
                lookup.Add(sourceAlphabet(i), i)
            Next

            If fromBase = 1 Then
                If value = "" Then Return 0
                If Not value.All(Function(ch) comparer.Equals(ch, sourceAlphabet(0))) Then Throw New FormatException(ResourcesT.Exceptions.BaseInvalidCharacter)
                Return value.Length
            End If

            Dim ret As BigInteger = 0
            For Each ch In value
                ret *= fromBase
                Dim v As Integer
                If Not lookup.TryGetValue(ch, v) Then Throw New FormatException(ResourcesT.Exceptions.BaseInvalidCharacter)
                ret += v
            Next
            Return ret
        End Function

        ''' <summary>Default alphabet for lowercase bases</summary>
        Private Const alphabetLo$ = "0123456789abcdefghijklmnopqrstuvwxyz"
        ''' <summary>Default alphabet for uppercase bases</summary>
        Private Const alphabetUp$ = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"

        ''' <summary>Gets default alphabet for bases for 0 to 36</summary>
        ''' <param name="base">Base to get default alphabet for</param>
        ''' <param name="lowerCase">True to get lowercase alphabet, false to get uppercase alphabet</param>
        ''' <returns>Array of characters representing numbers for requested base</returns>
        ''' <remarks>
        ''' Alphabets are constructed as follows
        ''' <list type="table">
        ''' <listheader><term>Base</term><description>Alphabet</description></listheader>
        ''' <item><term>1</term><description>Only one charatcer - '1'</description></item>
        ''' <item><term>2 ÷ 10</term><description>Array contains numerals 0 ÷ <paramref name="base"/> - 1</description></item>
        ''' <item><term>11 ÷ 36</term><description>Array contains numerals 0 ÷ 9 followed by uppercase or lowercase (depends on <paramref name="lowerCase"/>) letters for numerals 10 and higher.</description></item>
        ''' <item><term>>= 37</term><description>Bases higher than 36 are not supported by default. You must pass your own array to <see cref="Dec2Xxx"/> or <see cref="Xxx2Dec"/> method. This is becase base 36 uses all western numerals 0÷9 and all basic latin letters A÷Z (resp. a÷z) and I'm not aware of any standartized set of characters to use for such bases.</description></item>
        ''' </list>
        ''' </remarks>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="base"/> is zero or lower</exception>
        ''' <exception cref="NotSupportedException"><paramref name="base"/> is greater than 36</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Function GetAlphabet(base%, Optional lowerCase As Boolean = False) As Char()
            If base <= 0 Then Throw New ArgumentOutOfRangeException("base", ResourcesT.Exceptions.InvallidNumberBase)
            Dim source = If(lowerCase, alphabetLo, alphabetUp)
            If base = 1 Then Return {source(1)}
            If base > source.Length Then Throw New NotSupportedException(String.Format(ResourcesT.Exceptions.MaximumBaseSupported, source.Length))
            Return source.ToCharArray(0, base)
        End Function

        ''' <summary>Converts a number form one base to another</summary>
        ''' <param name="value">String that represents number in base <paramref name="fromBase"/></param>
        ''' <param name="fromBase">Base <paramref name="value"/> represents number in. The base to convert number from.</param>
        ''' <param name="toBase">Base to convert <paramref name="value"/> to</param>
        ''' <param name="sourceAlphabet">Array that defines characters used for numerals in <paramref name="fromBase"/>. Default is used if null.</param>
        ''' <param name="targetAlphabet">Array tha defines characters used for numerals in <paramref name="toBase"/>. Default is used if null.</param>
        ''' <param name="comparer">Used to compare numerals form <paramref name="value"/> to numerals in <paramref name="sourceAlphabet"/>. If null <see cref="CharComparer.InvariantCultureIgnoreCase"/> is used.<note>If <paramref name="sourceAlphabet"/> is null default alphabet is used which is uppercase.</note></param>
        ''' <returns>String that represents <paramref name="value"/> in base <paramref name="toBase"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="fromBase"/> or <paramref name="toBase"/> is less than or equal to zero.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="sourceAlphabet"/> is null and <paramref name="fromBase"/> is greater than 36 -or- <paramref name="targetAlphabet"/> is null and <paramref name="toBase"/> is greater than 36</exception>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="value"/> is an empty string and <paramref name="fromBase"/> is not 1. -or-
        ''' <paramref name="sourceAlphabet"/> is not null and <paramref name="sourceAlphabet"/> contains duplicate character (as compared by <paramref name="comparer"/>). -or-
        ''' <paramref name="sourceAlphabet"/> is not null and length of <paramref name="sourceAlphabet"/> differs form <paramref name="fromBase"/>. -or-
        ''' <paramref name="targetAlphabet"/> is not null and lenght of <paramref name="targetAlphabet"/> differs from <paramref name="toBase"/>.
        ''' </exception>
        ''' <exception cref="FormatException"><paramref name="value"/> contain character that is not contained in <paramref name="sourceAlphabet"/> (or default alphabet if <paramref name="sourceAlphabet"/> is null) as compared by <paramref name="comparer"/>.</exception>
        ''' <remarks>
        ''' <para>This method is combination of <see cref="Dec2Xxx"/> and <see cref="Xxx2Dec"/>. It calls like <c>Dec2Xxx(Xxx2Dec())</c>.</para>
        ''' <para>Unique values in <paramref name="targetAlphabet"/> are not enforced. Hovewer in case <paramref name="targetAlphabet"/> contains duplicate character the meaning of return value can be ambigious.</para>
        ''' <para>Base 1 is supported. Zero is represented asn ampty string. Non-zero value are represented as string that contain the same character repeated. Value is determined as length of the string. Character used for base 1 is <paramref name="sourceAlphabet"/>[0] (if <paramref name="fromBase"/> is 1) and <paramref name="targetAlphabet"/>[0] (if <paramref name="toBase"/> is 1). If either <paramref name="sourceAlphabet"/> or <paramref name="targetAlphabet"/> is null character '1' is used.</para>
        ''' </remarks>
        Public Function Xxx2Xxx(value As String, fromBase As Integer, toBase As Integer, Optional sourceAlphabet As Char() = Nothing, Optional targetAlphabet As Char() = Nothing, Optional comparer As IEqualityComparer(Of Char) = Nothing) As String
            Return Dec2Xxx(Xxx2Dec(value, fromBase, sourceAlphabet, comparer), toBase, targetAlphabet)
        End Function

        ''' <summary>Converts a number to its binary representation</summary>
        ''' <param name="value">A number to be converted</param>
        ''' <returns>Binary representation of <paramref name="value"/></returns>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is negative</exception>
        Public Function Dec2Bin(value As BigInteger) As String
            Return Dec2Xxx(value, 2)
        End Function

        ''' <summary>Gets numeric value from binary representation of a number</summary>
        ''' <param name="value">Binary representation of number</param>
        ''' <returns>Numeric value got by parsing <paramref name="value"/> as binary number</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is an empty string</exception>
        ''' <exception cref="FormatException"><paramref name="value"/> contains characters other than '0' and '1'</exception>
        Public Function Bin2Dec(value As String) As BigInteger
            Return Xxx2Dec(value, 2)
        End Function

        ''' <summary>Converts a number to its octal representation</summary>
        ''' <param name="value">A number to be converted</param>
        ''' <returns>Octal representation of <paramref name="value"/></returns>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is negative</exception>
        Public Function Oct2Bin(value As BigInteger) As String
            Return Dec2Xxx(value, 8)
        End Function

        ''' <summary>Gets numeric value from octal representation of a number</summary>
        ''' <param name="value">Octal representation of number</param>
        ''' <returns>Numeric value got by parsing <paramref name="value"/> as octal number</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is an empty string</exception>
        ''' <exception cref="FormatException"><paramref name="value"/> contains characters other than '0', '1', '2', '3', '4', '5', '6' and '7'</exception>
        Public Function Bin2Oct(value As String) As BigInteger
            Return Xxx2Dec(value, 8)
        End Function

        ''' <summary>Converts a number to its hexadecimal representation</summary>
        ''' <param name="value">A number to be converted</param>
        ''' <param name="lowerCase">True to use lowercase letters a-f, false to use upperfase letters A-F</param>
        ''' <returns>Hexadecimal representation of <paramref name="value"/></returns>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is negative</exception>
        Public Function Dec2Hex(value As BigInteger, Optional lowerCase As Boolean = False) As String
            Return Dec2Xxx(value, 16, GetAlphabet(16, lowerCase))
        End Function

        ''' <summary>Gets numeric value form hexadecimal representation of a number</summary>
        ''' <param name="value">Hexadecimal representation of number</param>
        ''' <returns>Numeric value got by parsing <paramref name="value"/> as hexadecimal number</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is an empty string</exception>
        ''' <exception cref="FormatException"><paramref name="value"/> contains characters other than hexanumerals 0-9, a-f, A-F</exception>
        ''' <remarks>This method is case-insensitive.</remarks>
        Public Function Hex2Dec(value As String) As BigInteger
            Return Xxx2Dec(value, 2)
        End Function
    End Module
End Namespace
