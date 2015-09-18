Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports CultureInfo = System.Globalization.CultureInfo
Imports UnicodeCategory = System.Globalization.UnicodeCategory

#If True
Namespace ExtensionsT
    ''' <summary>Contains extension methods for working with <see cref="System.Char"/></summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="RC"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Module [CharExtensions]
        ''' <summary>Converts the specified numeric Unicode character to a double-precision floating point number.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>The numeric value of c if that character represents a number; otherwise, -1.0.</returns>
        ''' <seealso cref="System.Char.GetNumericValue"/>
        <Extension()> Public Function NumericValue(ByVal c As Char) As Double
            Return Char.GetNumericValue(c)
        End Function
        ''' <summary>Categorizes a specified Unicode character into a group identified by one of the <see cref="System.UnicodeCategory" /> values.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>A <see cref="System.UnicodeCategory" /> value that identifies the group that contains c.</returns>
        ''' <seealso cref="System.Char.GetUnicodeCategory"/>
        <Extension()> Public Function UnicodeCategory(ByVal c As Char) As UnicodeCategory
            Return Char.GetUnicodeCategory(c)
        End Function
        ' ''' <summary>Gets generalized Unicode category of character</summary>
        ' ''' <param name="c">A Unicode character</param>
        ' ''' <returns>Generalized unicode category <paramref name="c"/> belongs to</returns>
        ' ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        '<Extension()> Public Function GeneralUnicodeCategory(c As Char) As TextT.UnicodeT.UnicodeGeneralCategoryClass
        '    Return TextT.UnicodeT.UnicodeExtensions.GetClass(Char.GetUnicodeCategory(c))
        'End Function
        ''' <summary>Indicates whether the specified Unicode character is categorized as a control character.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>true if c is a control character; otherwise, false.</returns>
        ''' <seealso cref="System.Char.IsControl"/>
        ''' <seealso cref="System.Char.IsControl"/>
        <Extension()> Public Function IsControl(ByVal c As Char) As Boolean
            Return Char.IsControl(c)
        End Function
        ''' <summary>Indicates whether the specified Unicode character is categorized as a decimal digit.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>true if c is a decimal digit; otherwise, false.</returns>
        ''' <seealso cref="System.Char.IsDigit"/>
        <Extension()> Public Function IsDigit(ByVal c As Char) As Boolean
            Return Char.IsDigit(c)
        End Function
        ''' <summary>Indicates whether the specified <see cref="System.Char" /> object is a high surrogate.</summary>
        ''' <param name="c">A character.</param>
        ''' <returns>true if the numeric value of the c parameter ranges from U+D800 through U+DBFF; otherwise, false.</returns>
        ''' <seealso cref="System.Char.IsHighSurrogate"/>
        <Extension()> Public Function IsHighSurrogate(ByVal c As Char) As Boolean
            Return Char.IsHighSurrogate(c)
        End Function
        ''' <summary>Indicates whether the specified Unicode character is categorized as an alphabetic letter.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>true if c is an alphabetic letter; otherwise, false.</returns>
        ''' <seealso cref="System.Char.IsLetter"/>
        <Extension()> Public Function IsLetter(ByVal c As Char) As Boolean
            Return Char.IsLetter(c)
        End Function
        ''' <summary>Indicates whether the specified Unicode character is categorized as an alphabetic letter or a decimal digit.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>true if c is an alphabetic letter or a decimal digit; otherwise, false.</returns>
        ''' <seealso cref="System.Char.IsLetterOrDigit"/>
        <Extension()> Public Function IsLetterOrDigit(ByVal c As Char) As Boolean
            Return Char.IsLetterOrDigit(c)
        End Function
        ''' <summary>Indicates whether the specified Unicode character is categorized as a lowercase letter.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>true if c is a lowercase letter; otherwise, false.</returns>
        ''' <seealso cref="System.Char.ISLower"/>
        <Extension()> Public Function IsLower(ByVal c As Char) As Boolean
            Return Char.IsLower(c)
        End Function
        ''' <summary>Indicates whether the specified <see cref="System.Char" /> object is a low surrogate.</summary>
        ''' <param name="c">A character.</param>
        ''' <returns>true if the numeric value of the c parameter ranges from U+DC00 through U+DFFF; otherwise, false.</returns>
        ''' <seealso cref="System.Char.IsLowSurrogate"/>
        <Extension()> Public Function IsLowSurrogate(ByVal c As Char) As Boolean
            Return Char.IsLowSurrogate(c)
        End Function
        ''' <summary>Indicates whether the specified Unicode character is categorized as a number.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>true if c is a number; otherwise, false.</returns>
        ''' <seealso cref="System.Char.IsNumber"/>
        <Extension()> Public Function IsNumber(ByVal c As Char) As Boolean
            Return Char.IsNumber(c)
        End Function
        ''' <summary>Indicates whether the specified Unicode character is categorized as a punctuation mark.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>true if c is a punctuation mark; otherwise, false.</returns>
        ''' <seealso cref="System.Char.IsPunctuation"/>
        <Extension()> Public Function IsPunctuation(ByVal c As Char) As Boolean
            Return Char.IsPunctuation(c)
        End Function
        ''' <summary>Indicates whether the specified Unicode character is categorized as a separator character.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>true if c is a separator character; otherwise, false.</returns>
        ''' <seealso cref="System.Char.IsSeparator"/>
        <Extension()> Public Function IsSeparator(ByVal c As Char) As Boolean
            Return Char.IsSeparator(c)
        End Function
        ''' <summary>Indicates whether the specified Unicode character is categorized as a surrogate character.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>true if c is a surrogate character; otherwise, false.</returns>
        ''' <seealso cref="System.Char.IsSurrogate"/>
        <Extension()> Public Function IsSurrogate(ByVal c As Char) As Boolean
            Return Char.IsSurrogate(c)
        End Function
        ''' <summary>Indicates whether the specified Unicode character is categorized as an uppercase letter.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>true if c is an uppercase letter; otherwise, false.</returns>
        ''' <seealso cref="System.Char.IsUpper"/>
        <Extension()> Public Function IsUpper(ByVal c As Char) As Boolean
            Return Char.IsUpper(c)
        End Function
        ''' <summary>Indicates whether the specified Unicode character is categorized as white space.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>true if c is white space; otherwise, false.</returns>
        ''' <seealso cref="System.Char.IsWhitespace"/>
        <Extension()> Public Function IsWhiteSpace(ByVal c As Char) As Boolean
            Return Char.IsWhiteSpace(c)
        End Function
        ''' <summary>Converts the value of a Unicode character to its lowercase equivalent.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>The lowercase equivalent of c, or the unchanged value of c, if c is already lowercase or not alphabetic.</returns>
        ''' <seealso cref="System.Char.ToLower"/>
        <Extension()> Public Function ToLower(ByVal c As Char) As Char
            Return Char.ToLower(c)
        End Function
        ''' <summary>Converts the value of a specified Unicode character to its lowercase equivalent using specified culture-specific formatting information.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <param name="culture">A <see cref="System.CultureInfo" /> object that supplies culture-specific casing rules, or null.</param>
        ''' <returns>The lowercase equivalent of c, modified according to culture, or the unchanged value of c, if c is already lowercase or not alphabetic.</returns>
        ''' <exception cref="System.ArgumentNullException">culture is null.</exception>
        ''' <seealso cref="System.Char.ToLower"/>
        <Extension()> Public Function ToLower(ByVal c As Char, ByVal culture As CultureInfo) As Char
            Return Char.ToLower(c, culture)
        End Function
        ''' <summary>Converts the value of a Unicode character to its lowercase equivalent using the casing rules of the invariant culture.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>The lowercase equivalent of the c parameter, or the unchanged value of c, if c is already lowercase or not alphabetic.</returns>
        ''' <seealso cref="System.Char.ToLowerInvariant"/>
        <Extension()> Public Function ToLowerInvariant(ByVal c As Char) As Char
            Return Char.ToLowerInvariant(c)
        End Function
        ''' <summary>Converts the value of a Unicode character to its uppercase equivalent.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>The uppercase equivalent of c, or the unchanged value of c, if c is already uppercase or not alphabetic.</returns>
        ''' <seealso cref="System.Char.ToUpper"/>
        <Extension()> Public Function ToUpper(ByVal c As Char) As Char
            Return Char.ToUpper(c)
        End Function
        ''' <summary>Converts the value of a specified Unicode character to its uppercase equivalent using specified culture-specific formatting information.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <param name="culture">A <see cref="System.CultureInfo" /> object that supplies culture-specific casing rules, or null.</param>
        ''' <returns>The uppercase equivalent of c, modified according to culture, or the unchanged value of c, if c is already uppercase or not alphabetic.</returns>
        ''' <exception cref="System.ArgumentNullException">culture is null.</exception>
        ''' <seealso cref="System.Char.ToUpper"/>
        <Extension()> Public Function ToUpper(ByVal c As Char, ByVal culture As CultureInfo) As Char
            Return Char.ToUpper(c, culture)
        End Function
        ''' <summary>Converts the value of a Unicode character to its uppercase equivalent using the casing rules of the invariant culture.</summary>
        ''' <param name="c">A Unicode character.</param>
        ''' <returns>The uppercase equivalent of the c parameter, or the unchanged value of c, if c is already uppercase or not alphabetic.</returns>
        ''' <seealso cref="System.Char.ToUpperInvariant"/>
        <Extension()> Public Function ToUpperInvariant(ByVal c As Char) As Char
            Return Char.ToUpperInvariant(c)
        End Function
    End Module
End Namespace
#End If