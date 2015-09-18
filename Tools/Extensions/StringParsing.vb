Imports System.Runtime.CompilerServices
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Numerics
Imports Tools.RuntimeT.CompilerServicesT

#If True  'Stage: Nightly
Namespace ExtensionsT

    ''' <summary>Contains extension methods usefull for string parsing</summary>
    ''' <remarks>This module does not define <c>TryParse</c> method for <see cref="[Enum]"/>. This method is defined in <see cref="T:Tools.EnumCore"/>.</remarks>
    ''' <version version="1.5.4">This module is new in version 1.5.4</version>
    Public Module StringParsing
#Region "TryParse"
#Region "SByte"
        ''' <summary>Tries to convert the string representation of a number to its <see cref="SByte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="SByte.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, <Out()> ByRef result As SByte) As Boolean
            Return SByte.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="SByte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="SByte.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, style As NumberStyles, provider As IFormatProvider, <Out()> ByRef result As SByte) As Boolean
            Return SByte.TryParse(s, style, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number and culture-specific format to its <see cref="SByte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="SByte.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As SByte) As Boolean
            Return SByte.TryParse(s, NumberStyles.Any, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and current culture-specific format to its <see cref="SByte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="SByte.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, style As NumberStyles, <Out()> ByRef result As SByte) As Boolean
            Return SByte.TryParse(s, style, CultureInfo.InvariantCulture, result)
        End Function
#End Region

#Region "Byte"
        ''' <summary>Tries to convert the string representation of a number to its <see cref="Byte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Byte.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Byte) As Boolean
            Return Byte.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="Byte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Byte.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, provider As IFormatProvider, <Out()> ByRef result As Byte) As Boolean
            Return Byte.TryParse(s, style, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number and culture-specific format to its <see cref="Byte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Byte.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As Byte) As Boolean
            Return Byte.TryParse(s, NumberStyles.Any, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and current culture-specific format to its <see cref="Byte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Byte.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, <Out()> ByRef result As Byte) As Boolean
            Return Byte.TryParse(s, style, CultureInfo.InvariantCulture, result)
        End Function
#End Region

#Region "Short"
        ''' <summary>Tries to convert the string representation of a number to its <see cref="Short" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Short.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Short) As Boolean
            Return Short.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="Short" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Short.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, provider As IFormatProvider, <Out()> ByRef result As Short) As Boolean
            Return Short.TryParse(s, style, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number and culture-specific format to its <see cref="Short" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Short.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As Short) As Boolean
            Return Short.TryParse(s, NumberStyles.Any, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and current culture-specific format to its <see cref="Short" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Short.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, <Out()> ByRef result As Short) As Boolean
            Return Short.TryParse(s, style, CultureInfo.InvariantCulture, result)
        End Function
#End Region

#Region "UShort"
        ''' <summary>Tries to convert the string representation of a number to its <see cref="UShort" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="UShort.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, <Out()> ByRef result As UShort) As Boolean
            Return UShort.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="UShort" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="UShort.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, style As NumberStyles, provider As IFormatProvider, <Out()> ByRef result As UShort) As Boolean
            Return UShort.TryParse(s, style, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number and culture-specific format to its <see cref="UShort" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="UShort.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As UShort) As Boolean
            Return UShort.TryParse(s, NumberStyles.Any, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and current culture-specific format to its <see cref="UShort" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="UShort.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, style As NumberStyles, <Out()> ByRef result As UShort) As Boolean
            Return UShort.TryParse(s, style, CultureInfo.InvariantCulture, result)
        End Function
#End Region

#Region "Integer"
        ''' <summary>Tries to convert the string representation of a number to its <see cref="Integer" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Integer.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Integer) As Boolean
            Return Integer.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="Integer" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Integer.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, provider As IFormatProvider, <Out()> ByRef result As Integer) As Boolean
            Return Integer.TryParse(s, style, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number and culture-specific format to its <see cref="Integer" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Integer.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As Integer) As Boolean
            Return Integer.TryParse(s, NumberStyles.Any, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and current culture-specific format to its <see cref="Integer" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Integer.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, <Out()> ByRef result As Integer) As Boolean
            Return Integer.TryParse(s, style, CultureInfo.InvariantCulture, result)
        End Function
#End Region

#Region "UInteger"
        ''' <summary>Tries to convert the string representation of a number to its <see cref="UInteger" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="UInteger.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, <Out()> ByRef result As UInteger) As Boolean
            Return UInteger.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="UInteger" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="UInteger.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, style As NumberStyles, provider As IFormatProvider, <Out()> ByRef result As UInteger) As Boolean
            Return UInteger.TryParse(s, style, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number and culture-specific format to its <see cref="UInteger" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="UInteger.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As UInteger) As Boolean
            Return UInteger.TryParse(s, NumberStyles.Any, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and current culture-specific format to its <see cref="UInteger" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="UInteger.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, style As NumberStyles, <Out()> ByRef result As UInteger) As Boolean
            Return UInteger.TryParse(s, style, CultureInfo.InvariantCulture, result)
        End Function
#End Region

#Region "Long"
        ''' <summary>Tries to convert the string representation of a number to its <see cref="Long" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Long.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Long) As Boolean
            Return Long.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="Long" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Long.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, provider As IFormatProvider, <Out()> ByRef result As Long) As Boolean
            Return Long.TryParse(s, style, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number and culture-specific format to its <see cref="Long" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Long.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As Long) As Boolean
            Return Long.TryParse(s, NumberStyles.Any, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and current culture-specific format to its <see cref="Long" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Long.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, <Out()> ByRef result As Long) As Boolean
            Return Long.TryParse(s, style, CultureInfo.InvariantCulture, result)
        End Function
#End Region

#Region "ULong"
        ''' <summary>Tries to convert the string representation of a number to its <see cref="ULong" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="ULong.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, <Out()> ByRef result As ULong) As Boolean
            Return ULong.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="ULong" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="ULong.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, style As NumberStyles, provider As IFormatProvider, <Out()> ByRef result As ULong) As Boolean
            Return ULong.TryParse(s, style, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number and culture-specific format to its <see cref="ULong" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="ULong.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As ULong) As Boolean
            Return ULong.TryParse(s, NumberStyles.Any, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and current culture-specific format to its <see cref="ULong" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="ULong.TryParse"/>
        <Extension(), CLSCompliant(False)>
        Public Function TryParse(s$, style As NumberStyles, <Out()> ByRef result As ULong) As Boolean
            Return ULong.TryParse(s, style, CultureInfo.InvariantCulture, result)
        End Function
#End Region

#Region "Long"
        ''' <summary>Tries to convert the string representation of a number to its <see cref="Single" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Single.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Single) As Boolean
            Return Single.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="Single" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Single.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, provider As IFormatProvider, <Out()> ByRef result As Single) As Boolean
            Return Single.TryParse(s, style, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number and culture-specific format to its <see cref="Single" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Single.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As Single) As Boolean
            Return Single.TryParse(s, NumberStyles.Any, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and current culture-specific format to its <see cref="Single" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Single.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, <Out()> ByRef result As Single) As Boolean
            Return Single.TryParse(s, style, CultureInfo.InvariantCulture, result)
        End Function
#End Region

#Region "Double"
        ''' <summary>Tries to convert the string representation of a number to its <see cref="Double" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Double.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Double) As Boolean
            Return Double.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="Double" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Double.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, provider As IFormatProvider, <Out()> ByRef result As Double) As Boolean
            Return Double.TryParse(s, style, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number and culture-specific format to its <see cref="Double" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Double.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As Double) As Boolean
            Return Double.TryParse(s, NumberStyles.Any, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and current culture-specific format to its <see cref="Double" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Double.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, <Out()> ByRef result As Double) As Boolean
            Return Double.TryParse(s, style, CultureInfo.InvariantCulture, result)
        End Function
#End Region

#Region "Decimal"
        ''' <summary>Tries to convert the string representation of a number to its <see cref="Decimal" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Decimal.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Decimal) As Boolean
            Return Decimal.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="Decimal" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Decimal.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, provider As IFormatProvider, <Out()> ByRef result As Decimal) As Boolean
            Return Decimal.TryParse(s, style, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number and culture-specific format to its <see cref="Decimal" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Decimal.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As Decimal) As Boolean
            Return Decimal.TryParse(s, NumberStyles.Any, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and current culture-specific format to its <see cref="Decimal" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="Decimal.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, <Out()> ByRef result As Decimal) As Boolean
            Return Decimal.TryParse(s, style, CultureInfo.InvariantCulture, result)
        End Function
#End Region

#Region "BigInteger"
        ''' <summary>Tries to convert the string representation of a number to its <see cref="BigInteger" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="BigInteger.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As BigInteger) As Boolean
            Return BigInteger.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="BigInteger" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="BigInteger.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, provider As IFormatProvider, <Out()> ByRef result As BigInteger) As Boolean
            Return BigInteger.TryParse(s, style, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number and culture-specific format to its <see cref="BigInteger" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="BigInteger.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As BigInteger) As Boolean
            Return BigInteger.TryParse(s, NumberStyles.Any, provider, result)
        End Function
        ''' <summary>Tries to convert the string representation of a number in a specified style and current culture-specific format to its <see cref="BigInteger" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a number to convert.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">style is not a <see cref="Globalization.NumberStyles" /> value. -or-style is not a combination of <see cref="Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="Globalization.NumberStyles.HexNumber" /> values.</exception>
        ''' <seelaso cref="BigInteger.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, style As NumberStyles, <Out()> ByRef result As BigInteger) As Boolean
            Return BigInteger.TryParse(s, style, CultureInfo.InvariantCulture, result)
        End Function
#End Region

#Region "Date"
        ''' <summary>Tries to convert the string representation of a date and time to its <see cref="Date" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a date and time to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the date and time contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Date.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Date) As Boolean
            Return Date.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a date and time in a specified style and culture-specific format to its <see cref="Date" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a date and time to convert.</param>
        ''' <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the date and time contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">styles is not a valid <see cref="DateTimeStyles"/> value.-or-<paramref name="styles"/> contains an invalid combination of <see cref="DateTimeStyles"/> values (for example, both <see cref="DateTimeStyles.AssumeLocal"/> and <see cref="DateTimeStyles.AssumeUniversal"/>).</exception>
        ''' <exception cref="NotSupportedException"><paramref name="provider"/> is a neutral culture and cannot be used in a parsing operation.</exception>
        ''' <seelaso cref="Date.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, styles As DateTimeStyles, provider As IFormatProvider, <Out()> ByRef result As Date) As Boolean
            Return Date.TryParse(s, provider, styles, result)
        End Function
        ''' <summary>Tries to convert the string representation of a date and time and culture-specific format to its <see cref="Date" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a date and time to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the date and time contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="NotSupportedException"><paramref name="provider"/> is a neutral culture and cannot be used in a parsing operation.</exception>
        ''' <seelaso cref="Date.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As Date) As Boolean
            Return Date.TryParse(s, provider, DateTimeStyles.None, result)
        End Function
        ''' <summary>Tries to convert the string representation of a date and time in a specified style and current culture-specific format to its <see cref="Date" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a date and time to convert.</param>
        ''' <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the date and time contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">styles is not a valid <see cref="DateTimeStyles"/> value.-or-<paramref name="styles"/> contains an invalid combination of <see cref="DateTimeStyles"/> values (for example, both <see cref="DateTimeStyles.AssumeLocal"/> and <see cref="DateTimeStyles.AssumeUniversal"/>).</exception>
        ''' <seelaso cref="Date.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, styles As DateTimeStyles, <Out()> ByRef result As Date) As Boolean
            Return Date.TryParse(s, CultureInfo.InvariantCulture, styles, result)
        End Function

        ''' <summary>Converts the specified string representation of a date and time to its <see cref="DateTime" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly. The method returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string containing a date and time to convert.</param>
        ''' <param name="format">The required format of <paramref name="s"/>.</param>
        ''' <param name="provider">An <see cref="System.IFormatProvider" /> object that supplies culture-specific formatting information about <paramref name="s"/>. <see cref="CultureInfo.CurrentCulture"/> is used if null.</param>
        ''' <param name="style">A bitwise combination of one or more enumeration values that indicate the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, contains the <see cref="DateTime" /> value equivalent to the date and time contained in <paramref name="s"/>, if the conversion succeeded, or <see cref="DateTime.MinValue" /> if the conversion failed. The conversion fails if either the <paramref name="s"/> or <paramref name="format"/> parameter is null, is an empty string, or does not contain a date and time that correspond to the pattern specified in <paramref name="format"/>. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException"><paramref name="styles"/> is not a valid <see cref="Globalization.DateTimeStyles" /> value.-or-<paramref name="styles"/> contains an invalid combination of <see cref="Globalization.DateTimeStyles" /> values (for example, both <see cref="System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
        <Extension()>
        Public Function TyParseExact(s$, format$, <Out()> ByRef result As Date, Optional provider As IFormatProvider = Nothing, Optional style As DateTimeStyles = DateTimeStyles.None) As Boolean
            If provider Is Nothing Then provider = CultureInfo.CurrentCulture
            Return Date.TryParseExact(s, format, provider, style, result)
        End Function

        ''' <summary>Converts the specified string representation of a date and time to its <see cref="System.DateTime" /> equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match at least one of the specified formats exactly. The method returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string containing one or more dates and times to convert.</param>
        ''' <param name="formats">An array of allowable formats of s<paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific format information about <paramref name="s"/>. <see cref="CultureInfo.CurrentCulture"/> is used if null.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>. A typical value to specify is <see cref="System.Globalization.DateTimeStyles.None" />.</param>
        ''' <param name="result">When this method returns, contains the <see cref="System.DateTime" /> value equivalent to the date and time contained in s, if the conversion succeeded, or <see cref="System.DateTime.MinValue" /> if the conversion failed. The conversion fails if <paramref name="s"/> or <paramref name="formats"/> is null, <paramref name="s"/> or an element of <paramref name="formats"/> is an empty string, or the format of <paramref name="s"/> is not exactly as specified by at least one of the format patterns in <paramref name="formats"/>. This parameter is passed uninitialized.</param>
        ''' <returns>true if the s parameter was converted successfully; otherwise, false.</returns>
        ''' <exception cref="System.ArgumentException"><paramref name="styles"/> is not a valid <see cref="System.Globalization.DateTimeStyles" /> value.-or-<paramref name="styles"/> contains an invalid combination of <see cref="System.Globalization.DateTimeStyles" /> values (for example, both <see cref="System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
        <Extension()>
        Public Function TyParseExact(s$, formats$(), <Out()> ByRef result As Date, Optional provider As IFormatProvider = Nothing, Optional style As DateTimeStyles = DateTimeStyles.None) As Boolean
            If provider Is Nothing Then provider = CultureInfo.CurrentCulture
            Return Date.TryParseExact(s, formats, provider, style, result)
        End Function
#End Region

#Region "DateTimeOffset"
        ''' <summary>Tries to convert the string representation of a date and time to its <see cref="DateTimeOffset" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a date and time to convert.</param>
        ''' <param name="result">When this method returns, value equivalent to the date and time contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="DateTimeOffset.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As DateTimeOffset) As Boolean
            Return DateTimeOffset.TryParse(s, result)
        End Function
        ''' <summary>Tries to convert the string representation of a date and time in a specified style and culture-specific format to its <see cref="DateTimeOffset" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a date and time to convert.</param>
        ''' <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the date and time contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">styles is not a valid <see cref="DateTimeStyles"/> value.-or-<paramref name="styles"/> contains an invalid combination of <see cref="DateTimeStyles"/> values (for example, both <see cref="DateTimeStyles.AssumeLocal"/> and <see cref="DateTimeStyles.AssumeUniversal"/>).</exception>
        ''' <exception cref="NotSupportedException"><paramref name="provider"/> is a neutral culture and cannot be used in a parsing operation.</exception>
        ''' <seelaso cref="DateTimeOffset.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, styles As DateTimeStyles, provider As IFormatProvider, <Out()> ByRef result As DateTimeOffset) As Boolean
            Return DateTimeOffset.TryParse(s, provider, styles, result)
        End Function
        ''' <summary>Tries to convert the string representation of a date and time and culture-specific format to its <see cref="DateTimeOffset" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a date and time to convert.</param>
        ''' <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the date and time contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="NotSupportedException"><paramref name="provider"/> is a neutral culture and cannot be used in a parsing operation.</exception>
        ''' <seelaso cref="DateTimeOffset.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, provider As IFormatProvider, <Out()> ByRef result As DateTimeOffset) As Boolean
            Return DateTimeOffset.TryParse(s, provider, DateTimeStyles.None, result)
        End Function
        ''' <summary>Tries to convert the string representation of a date and time in a specified style and current culture-specific format to its <see cref="DateTimeOffset" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string representing a date and time to convert.</param>
        ''' <param name="styles">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, value equivalent to the date and time contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException">styles is not a valid <see cref="DateTimeStyles"/> value.-or-<paramref name="styles"/> contains an invalid combination of <see cref="DateTimeStyles"/> values (for example, both <see cref="DateTimeStyles.AssumeLocal"/> and <see cref="DateTimeStyles.AssumeUniversal"/>).</exception>
        ''' <seelaso cref="DateTimeOffset.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, styles As DateTimeStyles, <Out()> ByRef result As DateTimeOffset) As Boolean
            Return DateTimeOffset.TryParse(s, CultureInfo.InvariantCulture, styles, result)
        End Function

        ''' <summary>Converts the specified string representation of a date and time to its <see cref="DateTimeOffset" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly. The method returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string containing a date and time to convert.</param>
        ''' <param name="format">The required format of <paramref name="s"/>.</param>
        ''' <param name="provider">An <see cref="System.IFormatProvider" /> object that supplies culture-specific formatting information about <paramref name="s"/>. <see cref="CultureInfo.CurrentCulture"/> is used if null.</param>
        ''' <param name="style">A bitwise combination of one or more enumeration values that indicate the permitted format of <paramref name="s"/>.</param>
        ''' <param name="result">When this method returns, contains the <see cref="DateTimeOffset" /> value equivalent to the date and time contained in <paramref name="s"/>, if the conversion succeeded, or <see cref="DateTimeOffset.MinValue" /> if the conversion failed. The conversion fails if either the <paramref name="s"/> or <paramref name="format"/> parameter is null, is an empty string, or does not contain a date and time that correspond to the pattern specified in <paramref name="format"/>. This parameter is passed uninitialized.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException"><paramref name="styles"/> is not a valid <see cref="Globalization.DateTimeStyles" /> value.-or-<paramref name="styles"/> contains an invalid combination of <see cref="Globalization.DateTimeStyles" /> values (for example, both <see cref="System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
        <Extension()>
        Public Function TyParseExact(s$, format$, <Out()> ByRef result As DateTimeOffset, Optional provider As IFormatProvider = Nothing, Optional style As DateTimeStyles = DateTimeStyles.None) As Boolean
            If provider Is Nothing Then provider = CultureInfo.CurrentCulture
            Return DateTimeOffset.TryParseExact(s, format, provider, style, result)
        End Function

        ''' <summary>Converts the specified string representation of a date and time to its <see cref="System.DateTimeOffset" /> equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match at least one of the specified formats exactly. The method returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string containing one or more dates and times to convert.</param>
        ''' <param name="formats">An array of allowable formats of s<paramref name="s"/>.</param>
        ''' <param name="provider">An object that supplies culture-specific format information about <paramref name="s"/>. <see cref="CultureInfo.CurrentCulture"/> is used if null.</param>
        ''' <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s"/>. A typical value to specify is <see cref="System.Globalization.DateTimeStyles.None" />.</param>
        ''' <param name="result">When this method returns, contains the <see cref="System.DateTimeOffset" /> value equivalent to the date and time contained in s, if the conversion succeeded, or <see cref="System.DateTimeOffset.MinValue" /> if the conversion failed. The conversion fails if <paramref name="s"/> or <paramref name="formats"/> is null, <paramref name="s"/> or an element of <paramref name="formats"/> is an empty string, or the format of <paramref name="s"/> is not exactly as specified by at least one of the format patterns in <paramref name="formats"/>. This parameter is passed uninitialized.</param>
        ''' <returns>true if the s parameter was converted successfully; otherwise, false.</returns>
        ''' <exception cref="System.ArgumentException"><paramref name="styles"/> is not a valid <see cref="System.Globalization.DateTimeStyles" /> value.-or-<paramref name="styles"/> contains an invalid combination of <see cref="System.Globalization.DateTimeStyles" /> values (for example, both <see cref="System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
        <Extension()>
        Public Function TyParseExact(s$, formats$(), <Out()> ByRef result As DateTimeOffset, Optional provider As IFormatProvider = Nothing, Optional style As DateTimeStyles = DateTimeStyles.None) As Boolean
            If provider Is Nothing Then provider = CultureInfo.CurrentCulture
            Return DateTimeOffset.TryParseExact(s, formats, provider, style, result)
        End Function
#End Region

#Region "TimeSpan"
        ''' <summary>Converts the string representation of a time interval to its <see cref="TimeSpan" /> equivalent and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string that specifies the time interval to convert.</param>
        ''' <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="s"/>, or <see cref="TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if <paramref name="s"/> was converted successfully; otherwise, false. This operation returns false if the <paramref name="s"/> parameter is null or <see cref="System.String.Empty" />, has an invalid format, represents a time interval that is less than <see cref="TimeSpan.MinValue" /> or greater than <see cref="TimeSpan.MaxValue" />, or has at least one days, hours, minutes, or seconds component outside its valid range.</returns>
        ''' <seelaso cref="TimeSpan.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As TimeSpan) As Boolean
            Return TimeSpan.TryParse(s, result)
        End Function
        ''' <summary>Converts the string representation of a time interval to its <see cref="TimeSpan" /> equivalent by using the specified culture-specific formatting information, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string that specifies the time interval to convert.</param>
        ''' <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        ''' <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="s"/>, or <see cref="TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if <paramref name="s"/> was converted successfully; otherwise, false. This operation returns false if the <paramref name="s"/> parameter is null or <see cref="System.String.Empty" />, has an invalid format, represents a time interval that is less than <see cref="TimeSpan.MinValue" /> or greater than <see cref="TimeSpan.MaxValue" />, or has at least one days, hours, minutes, or seconds component outside its valid range.</returns>
        ''' <seelaso cref="TimeSpan.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, formatProvider As IFormatProvider, <Out()> ByRef result As TimeSpan) As Boolean
            Return TimeSpan.TryParse(s, formatProvider, result)
        End Function
        ''' <summary>Converts the string representation of a time interval to its <see cref="TimeSpan" /> equivalent by using the specified format and culture-specific format information, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match the specified format exactly.</summary>
        ''' <param name="s">A string that specifies the time interval to convert.</param>
        ''' <param name="format">A standard or custom format string that defines the required format of <paramref name="input"/>.</param>
        ''' <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        ''' <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input"/>, or <see cref="TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if <paramref name="input"/> was converted successfully; otherwise, false.</returns>
        <Extension()>
        Public Function TryParseExact(s$, format$, <Out()> ByRef result As TimeSpan, Optional formatProvider As IFormatProvider = Nothing) As Boolean
            If formatProvider Is Nothing Then formatProvider = CultureInfo.CurrentCulture
            Return TimeSpan.TryParseExact(s, format, formatProvider, result)
        End Function
        ''' <summary>Converts the specified string representation of a time interval to its <see cref="TimeSpan" /> equivalent by using the specified formats and culture-specific format information, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match one of the specified formats exactly.</summary>
        ''' <param name="s">A string that specifies the time interval to convert.</param>
        ''' <param name="formats">A array of standard or custom format strings that define the acceptable formats of <paramref name="input"/>.</param>
        ''' <param name="formatProvider">An object that provides culture-specific formatting information.</param>
        ''' <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input"/>, or <see cref="TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if <paramref name="input"/> was converted successfully; otherwise, false.</returns>
        <Extension()>
        Public Function TryParseExact(s$, formats$(), <Out()> ByRef result As TimeSpan, Optional formatProvider As IFormatProvider = Nothing) As Boolean
            If formatProvider Is Nothing Then formatProvider = CultureInfo.CurrentCulture
            Return TimeSpan.TryParseExact(s, formats, formatProvider, result)
        End Function
        ''' <summary>Converts the string representation of a time interval to its <see cref="TimeSpan" /> equivalent by using the specified format, culture-specific format information, and styles, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match the specified format exactly.</summary>
        ''' <param name="s">A string that specifies the time interval to convert.</param>
        ''' <param name="format">A standard or custom format string that defines the required format of <paramref name="input"/>.</param>
        ''' <param name="formatProvider">An object that provides culture-specific formatting information.</param>
        ''' <param name="styles">One or more enumeration values that indicate the style of <paramref name="input"/>.</param>
        ''' <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input"/>, or <see cref="TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if <paramref name="input"/> was converted successfully; otherwise, false.</returns>     
        <Extension()>
        Public Function TryParseExact(s$, format$, styles As TimeSpanStyles, <Out()> ByRef result As TimeSpan, Optional formatProvider As IFormatProvider = Nothing) As Boolean
            If formatProvider Is Nothing Then formatProvider = CultureInfo.CurrentCulture
            Return TimeSpan.TryParseExact(s, format, formatProvider, styles, result)
        End Function
        ''' <summary>Converts the specified string representation of a time interval to its <see cref="TimeSpan" /> equivalent by using the specified formats, culture-specific format information, and styles, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match one of the specified formats exactly.</summary>
        ''' <param name="s">A string that specifies the time interval to convert.</param>
        ''' <param name="formats">A array of standard or custom format strings that define the acceptable formats of <paramref name="input"/>.</param>
        ''' <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        ''' <param name="styles">One or more enumeration values that indicate the style of <paramref name="input"/>.</param>
        ''' <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input"/>, or <see cref="TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
        ''' <returns>true if <paramref name="input"/> was converted successfully; otherwise, false.</returns>
        <Extension()>
        Public Function TryParseExact(s$, formats$(), styles As TimeSpanStyles, <Out()> ByRef result As TimeSpan, Optional formatProvider As IFormatProvider = Nothing) As Boolean
            If formatProvider Is Nothing Then formatProvider = CultureInfo.CurrentCulture
            Return TimeSpan.TryParseExact(s, formats, formatProvider, styles, result)
        End Function
#End Region

#Region "Boolean"
        ''' <summary>Converts the specified string representation of a logical value to its <see cref="System.Boolean" /> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
        ''' <param name="s">A string containing the value to convert.</param>
        ''' <param name="result">When this method returns, if the conversion succeeded, contains true if <paramref name="s"/> is equivalent to <see cref="System.Boolean.TrueString" /> or false if <paramref name="s"/> is equivalent to <see cref="System.Boolean.FalseString" />. If the conversion failed, contains false. The conversion fails if <paramref name="s"/> is null or is not equivalent to either <see cref="System.Boolean.TrueString" /> or <see cref="System.Boolean.FalseString" />. This parameter is passed uninitialized.</param>
        ''' <returns>true if value was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Boolean.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Boolean) As Boolean
            Return Boolean.TryParse(s, result)
        End Function
#End Region

#Region "Guid"
        ''' <summary>Converts the string representation of a GUID to the equivalent <see cref="System.Guid" /> structure.</summary>
        ''' <param name="s">The GUID to convert.</param>
        ''' <param name="result">The structure that will contain the parsed value.</param>
        ''' <returns>true if the parse operation was successful; otherwise, false.</returns>
        ''' <seelaso cref="Guid.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Guid) As Boolean
            Return Guid.TryParse(s, result)
        End Function
        ''' <summary>Converts the string representation of a GUID to the equivalent <see cref="System.Guid" /> structure, provided that the string is in the specified format.</summary>
        ''' <param name="s">The GUID to convert.</param>
        ''' <param name="format">One of the following specifiers that indicates the exact format to use when interpreting input: "N", "D", "B", "P", or "X".</param>
        ''' <param name="result">The structure that will contain the parsed value.</param>
        ''' <returns>true if the parse operation was successful; otherwise, false.</returns>
        ''' <seelaso cref="Guid.TryParseExact"/>
        <Extension()>
        Public Function TryParseExact(s$, format$, <Out()> ByRef result As Guid) As Boolean
            Return Guid.TryParseExact(s, format, result)
        End Function
#End Region

#Region "Char"
        ''' <summary>Converts the value of the specified string to its equivalent Unicode character. A return code indicates whether the conversion succeeded or failed.</summary>
        ''' <param name="s">A string that contains a single character, or null.</param>
        ''' <param name="result">When this method returns, contains a Unicode character equivalent to the sole character in <paramref name="s"/>, if the conversion succeeded, or an undefined value if the conversion failed. The conversion fails if the <paramref name="s"/> parameter is null or the length of <paramref name="s"/> is not 1. This parameter is passed uninitialized.</param>
        ''' <returns>true if the <paramref name="s"/> parameter was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Char.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Char) As Boolean
            Return Char.TryParse(s, result)
        End Function
#End Region

#Region "IPAddress"
        ''' <summary>Determines whether a string is a valid IP address.</summary>
        ''' <param name="s">The string to validate.</param>
        ''' <param name="result">The <see cref="System.Net.IPAddress" /> version of the string.</param>
        ''' <returns>true if <paramref name="s"/> is a valid IP address; otherwise, false.</returns>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Net.IPAddress) As Boolean
            Return Net.IPAddress.TryParse(s, result)
        End Function
#End Region

#Region "Version"
        ''' <summary>Tries to convert the string representation of a version number to an equivalent <see cref="System.Version" /> object, and returns a value that indicates whether the conversion succeeded.</summary>
        ''' <param name="s">A string that contains a version number to convert.</param>
        ''' <param name="result">When this method returns, contains the <see cref="System.Version" /> equivalent of the number that is contained in <paramref name="s"/>, if the conversion succeeded, or a <see cref="System.Version" /> object whose major and minor version numbers are 0 if the conversion failed.</param>
        ''' <returns>true if the input parameter was converted successfully; otherwise, false.</returns>
        ''' <seelaso cref="Version.TryParse"/>
        <Extension()>
        Public Function TryParse(s$, <Out()> ByRef result As Version) As Boolean
            Return Version.TryParse(s, result)
        End Function
#End Region
#End Region
    End Module

End Namespace
#End If