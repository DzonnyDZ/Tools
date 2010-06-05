Imports Tools.ExtensionsT
Imports System.Runtime.InteropServices

Namespace NumericsT
#If Config <= Alpha Then 'Stage: Alpha
    ''' <summary>Represents unsigned rational number with numerator and denominator</summary>
    ''' <author www="http://dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Alpha"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <version version="1.5.2">Added <see cref="URational.Parse"/>, <see cref="URational.TryParse"/> and <see cref="IFormattable"/> implementation.</version>
    ''' <version version="1.5.2"><see cref="TypeConverterAttribute"/> added.</version>
    ''' <version version="1.5.2"><see cref="DebuggerDisplayAttribute"/> added</version>
    ''' <version version="1.5.2">Structure updated to use <see cref="UInt32"/> instead of <see cref="UInt16"/>.</version>
    ''' <version version="1.5.3">Structure renamed from <c>Tools.MetadataT.ExifT.URational</c> to <see cref="URational"/>.</version>
    ''' <version version="1.5.3">Added <see cref="StructLayoutAttribute"/> (<see cref="LayoutKind.Sequential"/>)</version>
    <CLSCompliant(False), TypeConverter(GetType(URational.URationalConverter))>
    <DebuggerDisplay("{Numerator}/{Denominator}")>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure URational : Implements IFormattable
        Implements DataStructuresT.GenericT.IPair(Of UInt32, UInt32)
        ''' <summary>Contains value of the <see cref="Numerator"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Numerator As UInt32
        ''' <summary>Contains value of the <see cref="Denominator"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Denominator As UInt32
        ''' <summary>CTor</summary>
        ''' <param name="Numerator">Numerator</param>
        ''' <param name="Denominator">Denominator</param>
        Public Sub New(ByVal Numerator As UInt32, ByVal Denominator As UInt32)
            Me.Numerator = Numerator
            Me.Denominator = Denominator
        End Sub
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        <Obsolete("Use type safe Clone instead"), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Function Clone1() As Object Implements System.ICloneable.Clone
            Return Me
        End Function
        ''' <summary>Swaps values <see cref="Numerator"/> and <see cref="Denominator"/></summary>
        Public Function Swap() As DataStructuresT.GenericT.IPair(Of UInteger, UInteger) Implements DataStructuresT.GenericT.IPair(Of UInteger, UInteger).Swap
            Return New URational(Denominator, Numerator)
        End Function
        ''' <summary>Numerator (1 in 1/2)</summary>
        Public Property Numerator() As UInteger Implements DataStructuresT.GenericT.IPair(Of UInteger, UInteger).Value1
            Get
                Return _Numerator
            End Get
            Set(ByVal value As UInteger)
                _Numerator = value
            End Set
        End Property
        ''' <summary>Denominator (2 in 1/2)</summary>
        Public Property Denominator() As UInteger Implements DataStructuresT.GenericT.IPair(Of UInteger, UInteger).Value2
            Get
                Return _Denominator
            End Get
            Set(ByVal value As UInteger)
                _Denominator = value
            End Set
        End Property
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Private Function Clone() As DataStructuresT.GenericT.IPair(Of UInteger, UInteger) Implements ICloneable(Of DataStructuresT.GenericT.IPair(Of UInteger, UInteger)).Clone
            Return Clone()
        End Function
        ''' <summary>Simplyfies <see cref="URational"/> to contain smallest possible <see cref="Numerator"/> and <see cref="Denominator"/></summary>
        Public Function Simplyfy() As URational
            If Numerator = 0 Then Return New URational(0, 1)
            If Denominator = 0 Then Return Me
            Dim GCD As UInt32 = MathT.GCD(Numerator, Denominator)
            Return New URational(Numerator / GCD, Denominator / GCD)
        End Function
#Region "Operators"
        ''' <summary>Adds two <see cref="URational"/>s</summary>
        ''' <param name="a">First number to add</param>
        ''' <param name="b">Second number to add</param>
        ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
        Public Shared Operator +(ByVal a As URational, ByVal b As URational) As URational
            If a.Numerator = 0 Then Return b.Simplyfy
            If b.Numerator = 0 Then Return a.Simplyfy
            Dim LCM As UInt32 = MathT.LCM(a.Denominator, b.Denominator)
            Dim ANum As UInt32 = a.Numerator * (LCM / a.Denominator)
            Dim BNum As UInt32 = b.Numerator * (LCM / b.Denominator)
            Return New URational(ANum + BNum, LCM).Simplyfy
        End Operator
        ''' <summary>Converts <see cref="URational"/> to <see cref="SRational"/></summary>
        ''' <param name="a">Number to be converted</param>
        ''' <returns>Signed representation of unsigned rational</returns>
        Public Shared Widening Operator CType(ByVal a As URational) As SRational
            Return New SRational(a.Numerator, a.Denominator)
        End Operator
        ''' <summary>Converts <see cref="SRational"/> to <see cref="URational"/></summary>
        ''' <param name="a">Number to be converted</param>
        ''' <returns>Unsigned representation of signed rational</returns>
        Public Shared Narrowing Operator CType(ByVal a As SRational) As URational
            Return New URational(a.Numerator, a.Denominator)
        End Operator
        ''' <summary>Multiplyes two <see cref="URational"/>s</summary>
        ''' <param name="a">First number to multiply</param>
        ''' <param name="b">Second number to multiply</param>
        ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
        Public Shared Operator *(ByVal a As URational, ByVal b As URational) As URational
            Return New URational(a.Numerator * b.Numerator, a.Denominator * b.Denominator)
        End Operator
        ''' <summary>Divides one number by other</summary>
        ''' <param name="a">Number to be divided</param>
        ''' <param name="b">Number to divide by</param>
        ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
        Public Shared Operator /(ByVal a As URational, ByVal b As URational) As URational
            Return a * b.Swap
        End Operator
        ''' <summary>Substracts two <see cref="URational"/>s</summary>
        ''' <param name="a">Number to substract from</param>
        ''' <param name="b">Number to be substracted</param>
        ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
        Public Shared Operator -(ByVal a As URational, ByVal b As URational) As SRational
            Return CType(a, SRational) - CType(b, SRational)
        End Operator
        ''' <summary>Negative value</summary>
        ''' <param name="a"><see cref="URational"/> to get negative value of</param>
        ''' <returns>Negative value of <paramref name="a"/></returns>
        Public Shared Operator -(ByVal a As URational) As SRational
            Return New SRational(-a.Numerator, a.Denominator)
        End Operator
        ''' <summary>Converts <see cref="Double"/> to <see cref="URational"/></summary>
        ''' <param name="a">Number to be converted</param>
        ''' <returns><see cref="URational"/> representation of <paramref name="a"/></returns>
        Public Shared Narrowing Operator CType(ByVal a As Double) As URational
            'If a < 0 Then Throw New ArgumentOutOfRangeException("Cannot convert negative values to unsigned rational")
            If a = 0 Then Return New URational(0, 1)
            If System.Math.Truncate(a) = a Then Return New URational(a, 1)
            Dim Multiplied As Double = a * UInt32.MaxValue
            Dim GCD As Long = MathT.GCD(Multiplied, UInt32.MaxValue)
            Return New URational(Multiplied / GCD, UInt32.MaxValue / GCD)
        End Operator
        ''' <summary>Converts <see cref="Single"/> to <see cref="URational"/></summary>
        ''' <param name="a">Number to be converted</param>
        ''' <returns><see cref="URational"/> representation of <paramref name="a"/></returns>
        Public Shared Narrowing Operator CType(ByVal a As Single) As URational
            Return CDbl(a)
        End Operator
        ''' <summary>Converts <see cref="URational"/> to <see cref="Double"/></summary>
        ''' <param name="a"><see cref="URational"/> to be converted</param>
        ''' <returns><see cref="Double"/> representation of <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As URational) As Double
            Return a.Denominator / a.Numerator
        End Operator
        ''' <summary>Converts <see cref="URational"/> to <see cref="String"/></summary>
        ''' <param name="a"><see cref="URational"/> to be converted</param>
        ''' <returns><see cref="String"/> representation of <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As URational) As String
            Return String.Format("{0}/{1}", a.Numerator, a.Denominator)
        End Operator
        ''' <summary>String representation</summary>
        Public Overrides Function ToString() As String
            Return Me
        End Function
        '''' <summary>Converts <see cref="String"/> to <see cref="URational"/></summary>
        '''' <param name="a"><see cref="String"/> to converts</param>
        '''' <returns><see cref="URational"/> value represented by <paramref name="a"/></returns>
        '''' <exception cref="InvalidCastException">When error ocures</exception>
        '''' <remarks><paramref name="a"/> must be in format \s*\d+\s*[/\s*\d+\s*]</remarks>
        'Public Shared Narrowing Operator CType(ByVal a As String) As URational
        '    Try
        '        Dim i As Integer
        '        Dim Numerator As Long = 0
        '        Dim Denominator As Long = 1
        '        While a(i) = " "c : i += 1 : End While
        '        For i = i - 1 To a.Length
        '            Select Case a(i)
        '                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
        '                    Numerator = Numerator * 10 + CByte(CStr(a(i)))
        '                Case Else : Exit For
        '            End Select
        '        Next i
        '        If i = a.Length Then Return New URational(Numerator, 1)
        '        While a(i) = " "c : i += 1 : End While
        '        If i = a.Length Then Return New URational(Numerator, 1)
        '        If a(i) <> "/"c Then Throw New InvalidCastException(ResourcesT.Exceptions.SlashExpected)
        '        i += 1
        '        While a(i) = " "c : i += 1 : End While
        '        For i = i - 1 To a.Length
        '            Select Case a(i)
        '                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
        '                    Denominator = Denominator * 10 + CByte(CStr(a(i)))
        '                Case Else : Exit For
        '            End Select
        '        Next i
        '        If i = a.Length Then Return New URational(Numerator, Denominator)
        '        While a(i) = " "c : i += 1 : End While
        '        If i = a.Length Then Return New URational(Numerator, Denominator)
        '        Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.UnexpectedCharacter0, a(i)))
        '    Catch ex As Exception
        '        If TypeOf ex Is InvalidCastException Then Throw
        '        Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, a, "URational"), ex)
        '    End Try
        'End Operator
#End Region
#Region "Parse"
        ''' <summary>Converts <see cref="String"/> to <see cref="URational"/></summary>
        ''' <param name="a"><see cref="String"/> to converts</param>
        ''' <returns><see cref="URational"/> value represented by <paramref name="a"/></returns>
        ''' <remarks><paramref name="a"/> must be in format of <see cref="Double"/> or <see cref="UInteger"/>/<see cref="UInteger"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="str"/> is null</exception>
        ''' <exception cref="FormatException"><paramref name="str"/> is not numeric value, or 2 numeric values separated by /.</exception>
        ''' <exception cref="OverflowException"><paramref name="str"/> represents value lower than <see cref="Double.MinValue"/> or greater than <see cref="Double.MaxValue"/> (for single-part number) -or- <paramref name="str"/> represents value lower than <see cref="UInteger.MinValue"/> or greater than <see cref="UInteger.MaxValue"/> (for double-part number)</exception>
        ''' <seelaso cref="Parse"/>
        ''' <version stage="Alpha" version="1.5.2">Operator behavior changed. Now it uses <see cref="Parse"/>.</version>
        Public Shared Narrowing Operator CType(ByVal a As String) As URational
            Return URational.Parse(a)
        End Operator
        ''' <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="URational"/> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
        ''' <param name="str">A string containing a number to convert.</param>
        ''' <param name="style">A bitwise combination of <see cref="System.Globalization.NumberStyles"/> values that indicates the permitted format of <paramref name="str"/></param>
        ''' <param name="Provider">An <see cref="System.IFormatProvider"/> objectthat supplies culture-specific formatting information about <paramref name="str"/>.</param>
        ''' <param name="Value"> When this method returns, contains the <see cref="URational"/> value equivalent to the number contained in <paramref name="str"/>, if the conversion succeeded.</param>
        ''' <returns>Null when conversion succeds, <see cref="TypeMismatchException"/> when parsing of /-delimited string failed; <see cref="InvalidCastException"/> when parsing of /-less string failed; other <see cref="Exception"/> when parsing failed from other reason.</returns>
        ''' <remarks>Returned <see cref="InvalidCastException"/> and <see cref="TypeMismatchException"/> should never be thrown.</remarks>
        ''' <exception cref="ArgumentException"><paramref name="style"/> is not a <see cref="System.Globalization.NumberStyles"/> value. -or- <paramref name="style"/> is not a combination of <see cref="System.Globalization.NumberStyles.AllowHexSpecifier"/> and <see cref="System.Globalization.NumberStyles.HexNumber"/> values. -or- <paramref name="style"/> is the <see cref="System.Globalization.NumberStyles.AllowHexSpecifier"/> value.</exception>
        ''' <version stage="Alpha" version="1.5.2">Method introduced</version>
        Private Shared Function TryParseInternal(ByVal str$, ByRef Value As URational, ByVal Provider As IFormatProvider, ByVal style As Globalization.NumberStyles) As Exception
            If str Is Nothing Then Return New ArgumentNullException("str")
            If str = "" Then Return New ArgumentException(ResourcesT.Exceptions.ValueCannotBeEmptyString, "str")
            If str.Contains("/"c) Then
                Dim Parts As String() = str.Split("/"c)
                If Parts.Length <> 2 Then Return New FormatException(ResourcesT.Exceptions.Value0CannotBeInterperetedAsRationalToManySlashes.f(str))
                Dim a As Long, b As Long
                If UInteger.TryParse(Parts(0), style, Provider, a) AndAlso UInteger.TryParse(Parts(1), style, Provider, b) Then
                    If a < 0 Xor b < 0 Then Return New OverflowException(ResourcesT.Exceptions.CannotBeNegative.f("URational"))
                    Value = New URational(a, b)
                    Return Nothing
                Else
                    Return New TypeMismatchException
                End If
            Else
                Dim dValue As Double
                If Double.TryParse(str, style, Provider, dValue) Then
                    Value = dValue
                    Return Nothing
                Else
                    Return New InvalidCastException
                End If
            End If
        End Function
        ''' <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="URational"/> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
        ''' <param name="str">A string containing a number to convert.</param>
        ''' <param name="style">A bitwise combination of <see cref="System.Globalization.NumberStyles"/> values that indicates the permitted format of <paramref name="str"/></param>
        ''' <param name="Provider">An <see cref="System.IFormatProvider"/> objectthat supplies culture-specific formatting information about <paramref name="str"/>.</param>
        ''' <param name="Value"> When this method returns, contains the <see cref="URational"/> value equivalent to the number contained in <paramref name="str"/>, if the conversion succeeded.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException"><paramref name="style"/> is not a <see cref="System.Globalization.NumberStyles"/> value. -or- <paramref name="style"/> is not a combination of <see cref="System.Globalization.NumberStyles.AllowHexSpecifier"/> and <see cref="System.Globalization.NumberStyles.HexNumber"/> values. -or- <paramref name="style"/> is the <see cref="System.Globalization.NumberStyles.AllowHexSpecifier"/> value.</exception>
        ''' <version stage="Alpha" version="1.5.2">Method introduced</version>
        Public Shared Function TryParse(ByVal str$, ByRef style As Globalization.NumberStyles, ByVal Provider As IFormatProvider, ByVal Value As URational) As Boolean
            Return TryParseInternal(str, Value, Provider, style) Is Nothing
        End Function
        ''' <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="URational"/> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
        ''' <param name="str">A string containing a number to convert.</param>
        ''' <param name="Value"> When this method returns, contains the <see cref="URational"/> value equivalent to the number contained in <paramref name="str"/>, if the conversion succeeded.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <version stage="Alpha" version="1.5.2">Method introduced</version>
        Public Shared Function TryParse(ByVal str$, ByRef Value As URational) As Boolean
            Return TryParse(str, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, Value)
        End Function
        ''' <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="URational"/> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
        ''' <param name="str">A string containing a number to convert.</param>
        ''' <param name="style">A bitwise combination of <see cref="System.Globalization.NumberStyles"/> values that indicates the permitted format of <paramref name="str"/></param>
        ''' <param name="Provider">An <see cref="System.IFormatProvider"/> objectthat supplies culture-specific formatting information about <paramref name="str"/>.</param>
        ''' <returns>A <see cref="URational"/> number equivalent to the numeric value or symbol specified in <paramref name="str"/>.</returns>
        ''' <exception cref="ArgumentException"><paramref name="style"/> is not a <see cref="System.Globalization.NumberStyles"/> value. -or- <paramref name="style"/> is not a combination of <see cref="System.Globalization.NumberStyles.AllowHexSpecifier"/> and <see cref="System.Globalization.NumberStyles.HexNumber"/> values. -or- <paramref name="style"/> is the <see cref="System.Globalization.NumberStyles.AllowHexSpecifier"/> value.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="str"/> is null</exception>
        ''' <exception cref="FormatException"><paramref name="str"/> is not numeric value, or 2 numeric values separated by /.</exception>
        ''' <exception cref="OverflowException"><paramref name="str"/> represents value lower than <see cref="Double.MinValue"/> or greater than <see cref="Double.MaxValue"/> (for single-part number) -or- <paramref name="str"/> represents value lower than <see cref="UInteger.MinValue"/> or greater than <see cref="UInteger.MaxValue"/> (for double-part number)</exception>
        ''' <version stage="Alpha" version="1.5.2">Method introduced</version>
        Public Shared Function Parse(ByVal str$, ByVal style As Globalization.NumberStyles, ByVal Provider As IFormatProvider) As URational
            Dim value As URational
            Dim ret = TryParseInternal(str, value, Provider, style)
            If ret Is Nothing Then Return value
            If TypeOf ret Is InvalidCastException Then
                Return Double.Parse(str)
            ElseIf TypeOf ret Is TypeMismatchException Then
                Dim parts = str.Split("/"c)
                Return New URational(UInteger.Parse(parts(0)), UInteger.Parse(parts(1)))
            Else
                Throw ret
            End If
        End Function
        ''' <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="URational"/> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
        ''' <param name="str">A string containing a number to convert.</param>
        ''' <returns>A <see cref="URational"/> number equivalent to the numeric value or symbol specified in <paramref name="str"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="str"/> is null</exception>
        ''' <exception cref="FormatException"><paramref name="str"/> is not numeric value, or 2 numeric values separated by /.</exception>
        ''' <exception cref="OverflowException"><paramref name="str"/> represents value lower than <see cref="Double.MinValue"/> or greater than <see cref="Double.MaxValue"/> (for single-part number) -or- <paramref name="str"/> represents value lower than <see cref="UInteger.MinValue"/> or greater than <see cref="UInteger.MaxValue"/> (for double-part number)</exception>
        ''' <version stage="Alpha" version="1.5.2">Method introduced</version>
        Public Shared Function Parse(ByVal str$) As URational
            Return Parse(str, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture)
        End Function
#End Region
        ''' <summary>Formats the value of the current instance using the specified format.</summary>
        ''' <param name="format">The <see cref="System.String" /> specifying the format to use.-or- null to use the default format defined for the type of the <see cref="System.IFormattable" /> implementation.</param>
        ''' <param name="formatProvider">The <see cref="System.IFormatProvider" /> to use to format the value.-or- null to obtain the numeric format information from the current locale setting of the operating system.</param>
        ''' <returns>A <see cref="System.String" /> containing the value of the current instance in the specified format.</returns>
        ''' <remarks>
        ''' Use sigle format string to format value as <see cref="Double"/>. Use two /-separated format strings to format this value as two <see cref="UInteger"/> values separated by /.
        ''' Format(s) passed to <see cref="Double.ToString"/> or <see cref="UInteger.ToString"/> can be empty, predefined (one letter) or custom.
        ''' If two formats are specified, delimited by /, only first slash encountered is treatead as delimitter. Other slashes are passed to <see cref="UInteger.Format"/>. In order to escape firts slahs, precede it with \.
        ''' If <paramref name="format"/> is null or <see cref="String.Empty"/> G/G is used. 
        ''' </remarks>
        ''' <version stage="Alpha" version="1.5.2">Method introduced</version>
        Public Overloads Function ToString(ByVal format As String, ByVal formatProvider As System.IFormatProvider) As String Implements System.IFormattable.ToString
            If format = "" Then format = "G/G"
            Dim FSplit As Integer = -1
            For i As Integer = 0 To format.Length - 1
                If format(i) = "/"c AndAlso (i = 0 OrElse format(i - 1) <> "\"c) Then
                    FSplit = i
                    Exit For
                End If
            Next
            If FSplit >= 0 Then
                Return Me.Numerator.ToString(format.Substring(0, FSplit), formatProvider) & "/" & Me.Denominator.ToString(format.Substring(FSplit + 1), formatProvider)
            Else
                If Me.Denominator = 0 AndAlso Me.Numerator = 0 Then
                    Return 0.ToString(format, formatProvider)
                ElseIf Me.Denominator = 0 AndAlso Me.Numerator > 0 Then
                    Return Double.PositiveInfinity.ToString(format, formatProvider)
                ElseIf Me.Denominator = 0 Then
                    Return Double.NegativeInfinity.ToString(format, formatProvider)
                Else
                    Return CType(Me, Double).ToString(format, formatProvider)
                End If
            End If
        End Function
        ''' <summary>Implemenst <see cref="ComponentModel.TypeConverter"/> for <see cref="String"/> and <see cref="URational"/></summary>
        ''' <version stage="Alpha" version="1.5.2">Class introduced</version>
        Public Class URationalConverter
            Inherits ComponentModelT.TypeConverter(Of URational, String)
            ''' <summary>Performs conversion from type <see cref="String"/> to type <see cref="URational"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to type <see cref="URational"/></param>
            ''' <returns>Value of type <see cref="URational"/> initialized by <paramref name="value"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
            ''' <exception cref="FormatException"><paramref name="str"/> is not numeric value, or 2 numeric values separated by /.</exception>
            ''' <exception cref="OverflowException"><paramref name="str"/> represents value lower than <see cref="Double.MinValue"/> or greater than <see cref="Double.MaxValue"/> (for single-part number) -or- <paramref name="str"/> represents value lower than <see cref="UInteger.MinValue"/> or greater than <see cref="UInteger.MaxValue"/> (for double-part number)</exception>
            Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As String) As URational
                Return URational.Parse(value, Globalization.NumberStyles.Any, culture)
            End Function
            ''' <summary>Performs conversion from type <see cref="URational"/> to type <see cref="String"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in type <see cref="String"/></returns>
            Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As URational) As String
                Return value.ToString(Nothing, culture)
            End Function
        End Class
    End Structure

    ''' <summary>Represents signed rational number with numerator and denominator</summary>
    ''' <author www="http://dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Alpha"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <version version="1.5.2" stage="Alpha">Added <see cref="SRational.Parse"/>, <see cref="SRational.TryParse"/> and <see cref="IFormattable"/> implementation.</version>
    ''' <version stage="Alpha" version="1.5.2"><see cref="TypeConverterAttribute"/> added.</version>
    ''' <version version="1.5.2"><see cref="DebuggerDisplayAttribute"/> added</version>
    ''' <version version="1.5.2">Structure updated to use <see cref="Int32"/> instead of <see cref="Int16"/></version>
    ''' <version version="1.5.3">Structure renamed from <c>Tools.MetadataT.ExifT.SRational</c> to <see cref="SRational"/>.</version>
    ''' <version version="1.5.3">Added <see cref="StructLayoutAttribute"/> (<see cref="LayoutKind.Sequential"/>)</version>
    <DebuggerDisplay("{Numerator}/{Denominator}")>
    <TypeConverter(GetType(URational.URationalConverter))>
     <StructLayout(LayoutKind.Sequential)>
    Public Structure SRational
        Implements DataStructuresT.GenericT.IPair(Of Int32, Int32), IFormattable
        ''' <summary>Contains value of the <see cref="Numerator"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Numerator As Int32
        ''' <summary>Contains value of the <see cref="Denominator"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Denominator As Int32
        ''' <summary>CTor</summary>
        ''' <param name="Numerator">Numerator</param>
        ''' <param name="Denominator">Denominator</param>
        Public Sub New(ByVal Numerator As Int32, ByVal Denominator As Int32)
            Me.Numerator = Numerator
            Me.Denominator = Denominator
        End Sub
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        <Obsolete("Use type safe Clone instead"), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Function Clone1() As Object Implements System.ICloneable.Clone
            Return Me
        End Function
        ''' <summary>Swaps values <see cref="Numerator"/> and <see cref="Denominator"/></summary>
        Public Function Swap() As DataStructuresT.GenericT.IPair(Of Integer, Integer) Implements DataStructuresT.GenericT.IPair(Of Integer, Integer).Swap
            Return New SRational(Denominator, Numerator)
        End Function
        ''' <summary>Numerator (1 in 1/2)</summary>
        Public Property Numerator() As Integer Implements DataStructuresT.GenericT.IPair(Of Integer, Integer).Value1
            Get
                Return _Numerator
            End Get
            Set(ByVal value As Integer)
                _Numerator = value
            End Set
        End Property
        ''' <summary>Denominator (2 in 1/2)</summary>
        Public Property Denominator() As Integer Implements DataStructuresT.GenericT.IPair(Of Integer, Integer).Value2
            Get
                Return _Denominator
            End Get
            Set(ByVal value As Integer)
                _Denominator = value
            End Set
        End Property
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Private Function Clone() As DataStructuresT.GenericT.IPair(Of Integer, Integer) Implements ICloneable(Of DataStructuresT.GenericT.IPair(Of Integer, Integer)).Clone
            Return Clone()
        End Function
        ''' <summary>Simplyfies <see cref="SRational"/> to contain smallest possible <see cref="Numerator"/> and <see cref="Denominator"/></summary>
        Public Function Simplyfy() As SRational
            If Numerator = 0 Then Return New SRational(0, 1)
            If Denominator = 0 Then Return Me
            Dim Negative As Boolean = Numerator < 0 Xor Denominator < 0
            Dim GCD As UInt32 = MathT.GCD(System.Math.Abs(Numerator), System.Math.Abs(Denominator))
            '#If Framework >= 3.5 Then
            Return New SRational(If(Negative, -1, 1) * Numerator / GCD, Denominator / GCD)
            '#Else
            '            Return New SRational(Tools.VisualBasicT.iif(Negative, -1, 1) * Numerator / GCD, Denominator / GCD)
            '#End If
        End Function


#Region "Operators"
        ''' <summary>Adds two <see cref="SRational"/>s</summary>
        ''' <param name="a">First number to add</param>
        ''' <param name="b">Second number to add</param>
        ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
        Public Shared Operator +(ByVal a As SRational, ByVal b As SRational) As SRational
            If a.Numerator = 0 Then Return b.Simplyfy
            If b.Numerator = 0 Then Return a.Simplyfy
            If a.Denominator < 0 Then a.Numerator *= -1
            If b.Denominator < 0 Then b.Numerator *= -1
            Dim LCM As Int32 = MathT.LCM(a.Denominator, b.Denominator)
            Dim ANum As Int32 = a.Numerator * (LCM / a.Denominator)
            Dim BNum As Int32 = b.Numerator * (LCM / b.Denominator)
            Return New SRational(ANum + BNum, LCM).Simplyfy
        End Operator
        ''' <summary>Creates negative value of given <see cref="SRational"/></summary>
        ''' <param name="a">Value to negativize</param>
        ''' <returns>- <paramref name="a"/></returns>
        Public Shared Operator -(ByVal a As SRational) As SRational
            Return New SRational(a.Numerator * -1, a.Denominator)
        End Operator
        ''' <summary>Substracts two <see cref="SRational"/>s</summary>
        ''' <param name="a">Number to substract from</param>
        ''' <param name="b">Number to be substracted</param>
        ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
        Public Shared Operator -(ByVal a As SRational, ByVal b As SRational) As SRational
            Return a + -b
        End Operator
        ''' <summary>Multiplyes two <see cref="SRational"/>s</summary>
        ''' <param name="a">First number to multiply</param>
        ''' <param name="b">Second number to multiply</param>
        ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
        Public Shared Operator *(ByVal a As SRational, ByVal b As SRational) As SRational
            Return New SRational(a.Numerator * b.Numerator, a.Denominator * b.Denominator)
        End Operator
        ''' <summary>Divides one number by other</summary>
        ''' <param name="a">Number to be divided</param>
        ''' <param name="b">Number to divide by</param>
        ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
        Public Shared Operator /(ByVal a As SRational, ByVal b As SRational) As SRational
            Return a * b.Swap
        End Operator
        ''' <summary>Converts <see cref="Double"/> to <see cref="SRational"/></summary>
        ''' <param name="a">Number to be converted</param>
        ''' <returns><see cref="URational"/> representation of <paramref name="a"/></returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="a"/> is negative</exception>
        Public Shared Narrowing Operator CType(ByVal a As Double) As SRational
            If a < 0 Then Throw New ArgumentOutOfRangeException(ResourcesT.Exceptions.CannotConvertNegativeValuesToUnsignedRational)
            If a = 0 Then Return New SRational(0, 1)
            If System.Math.Truncate(a) = a Then Return New SRational(a, 1)
            Dim Multiplied As Double = a * UInt32.MaxValue
            Dim GCD As Long = MathT.GCD(System.Math.Abs(Multiplied), UInt32.MaxValue)
            Return New SRational(Multiplied / GCD, UInt32.MaxValue / GCD)
        End Operator
        ''' <summary>Converts <see cref="Single"/> to <see cref="URational"/></summary>
        ''' <param name="a">Number to be converted</param>
        ''' <returns><see cref="URational"/> representation of <paramref name="a"/></returns>
        Public Shared Narrowing Operator CType(ByVal a As Single) As SRational
            Return CDbl(a)
        End Operator
        ''' <summary>Converts <see cref="URational"/> to <see cref="Double"/></summary>
        ''' <param name="a"><see cref="URational"/> to be converted</param>
        ''' <returns><see cref="Double"/> representation of <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As SRational) As Double
            Return a.Denominator / a.Numerator
        End Operator
        ''' <summary>Converts <see cref="URational"/> to <see cref="String"/></summary>
        ''' <param name="a"><see cref="URational"/> to be converted</param>
        ''' <returns><see cref="String"/> representation of <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As SRational) As String
            Return String.Format("{0}/{1}", a.Numerator, a.Denominator)
        End Operator
        ''' <summary>String representation</summary>
        Public Overrides Function ToString() As String
            Return Me
        End Function
        '''' <summary>Converts <see cref="String"/> to <see cref="SRational"/></summary>
        '''' <param name="a"><see cref="String"/> to converts</param>
        '''' <returns><see cref="URational"/> value represented by <paramref name="a"/></returns>
        '''' <exception cref="InvalidCastException">When error ocures</exception>
        '''' <remarks><paramref name="a"/> must be in format \s*-\s*?\d+\s*[/\s*-?\s*\d+\s*]</remarks>
        'Public Shared Narrowing Operator CType(ByVal a As String) As SRational
        '    Try
        '        Dim i As Integer
        '        Dim Numerator As Long = 0
        '        Dim Denominator As Long = 1
        '        Dim NumM As SByte = 1
        '        Dim DenM As SByte = 1
        '        While a(i) = " "c : i += 1 : End While
        '        If a(i) = "-"c Then
        '            i += 1
        '            NumM = -1
        '            While a(i) = " "c : i += 1 : End While
        '        End If
        '        For i = i - 1 To a.Length
        '            Select Case a(i)
        '                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
        '                    Numerator = Numerator * 10 + CByte(CStr(a(i)))
        '                Case Else : Exit For
        '            End Select
        '        Next i
        '        If i = a.Length Then Return New SRational(NumM * Numerator, 1)
        '        While a(i) = " "c : i += 1 : End While
        '        If i = a.Length Then Return New SRational(NumM * Numerator, 1)
        '        If a(i) <> "/"c Then Throw New InvalidCastException(ResourcesT.Exceptions.SlashExpected)
        '        i += 1
        '        While a(i) = " "c : i += 1 : End While
        '        If a(i) = "-"c Then
        '            i += 1
        '            DenM = -1
        '            While a(i) = " "c : i += 1 : End While
        '        End If
        '        For i = i - 1 To a.Length
        '            Select Case a(i)
        '                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
        '                    Denominator = Denominator * 10 + CByte(CStr(a(i)))
        '                Case Else : Exit For
        '            End Select
        '        Next i
        '        If i = a.Length Then Return New SRational(NumM * Numerator, DenM * Denominator)
        '        While a(i) = " "c : i += 1 : End While
        '        If i = a.Length Then Return New SRational(NumM * Numerator, DenM * Denominator)
        '        Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.UnexpectedCharacter0, a(i)))
        '    Catch ex As Exception
        '        If TypeOf ex Is InvalidCastException Then Throw
        '        Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertString0To1, a, "SRational"), ex)
        '    End Try
        'End Operator

#End Region
#Region "Parse"
        ''' <summary>Converts <see cref="String"/> to <see cref="SRational"/></summary>
        ''' <param name="a"><see cref="String"/> to converts</param>
        ''' <returns><see cref="SRational"/> value represented by <paramref name="a"/></returns>
        ''' <remarks><paramref name="a"/> must be in format of <see cref="Double"/> or <see cref="Integer"/>/<see cref="Integer"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="str"/> is null</exception>
        ''' <exception cref="FormatException"><paramref name="str"/> is not numeric value, or 2 numeric values separated by /.</exception>
        ''' <exception cref="OverflowException"><paramref name="str"/> represents value lower than <see cref="Double.MinValue"/> or greater than <see cref="Double.MaxValue"/> (for single-part number) -or- <paramref name="str"/> represents value lower than <see cref="Integer.MinValue"/> or greater than <see cref="Integer.MaxValue"/> (for double-part number)</exception>
        ''' <seelaso cref="Parse"/>
        ''' <version stage="Alpha" version="1.5.2">Operator behavior changed. Now it uses <see cref="Parse"/>.</version>
        Public Shared Narrowing Operator CType(ByVal a As String) As SRational
            Return SRational.Parse(a)
        End Operator
        ''' <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="SRational"/> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
        ''' <param name="str">A string containing a number to convert.</param>
        ''' <param name="style">A bitwise combination of <see cref="System.Globalization.NumberStyles"/> values that indicates the permitted format of <paramref name="str"/></param>
        ''' <param name="Provider">An <see cref="System.IFormatProvider"/> objectthat supplies culture-specific formatting information about <paramref name="str"/>.</param>
        ''' <param name="Value"> When this method returns, contains the <see cref="SRational"/> value equivalent to the number contained in <paramref name="str"/>, if the conversion succeeded.</param>
        ''' <returns>Null when conversion succeds, <see cref="TypeMismatchException"/> when parsing of /-delimited string failed; <see cref="InvalidCastException"/> when parsing of /-less string failed; other <see cref="Exception"/> when parsing failed from other reason.</returns>
        ''' <remarks>Returned <see cref="InvalidCastException"/> and <see cref="TypeMismatchException"/> should never be thrown.</remarks>
        ''' <exception cref="ArgumentException"><paramref name="style"/> is not a <see cref="System.Globalization.NumberStyles"/> value. -or- <paramref name="style"/> is not a combination of <see cref="System.Globalization.NumberStyles.AllowHexSpecifier"/> and <see cref="System.Globalization.NumberStyles.HexNumber"/> values. -or- <paramref name="style"/> is the <see cref="System.Globalization.NumberStyles.AllowHexSpecifier"/> value.</exception>
        ''' <version stage="Alpha" version="1.5.2">Method introduced</version>
        Private Shared Function TryParseInternal(ByVal str$, ByRef Value As SRational, ByVal Provider As IFormatProvider, ByVal style As Globalization.NumberStyles) As Exception
            If str Is Nothing Then Return New ArgumentNullException("str")
            If str = "" Then Return New ArgumentException(ResourcesT.Exceptions.ValueCannotBeEmptyString, "str")
            If str.Contains("/") Then
                Dim Parts As String() = str.Split("/"c)
                If Parts.Length <> 2 Then Return New FormatException(ResourcesT.Exceptions.Value0CannotBeInterperetedAsRationalToManySlashes.f(str))
                Dim a As Long, b As Long
                If Integer.TryParse(Parts(0), style, Provider, a) AndAlso Integer.TryParse(Parts(1), style, Provider, b) Then
                    Value = New SRational(a, b)
                    Return Nothing
                Else
                    Return New TypeMismatchException
                End If
            Else
                Dim dValue As Double
                If Double.TryParse(str, style, Provider, dValue) Then
                    Value = dValue
                    Return Nothing
                Else
                    Return New InvalidCastException
                End If
            End If
        End Function
        ''' <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="SRational"/> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
        ''' <param name="str">A string containing a number to convert.</param>
        ''' <param name="style">A bitwise combination of <see cref="System.Globalization.NumberStyles"/> values that indicates the permitted format of <paramref name="str"/></param>
        ''' <param name="Provider">An <see cref="System.IFormatProvider"/> objectthat supplies culture-specific formatting information about <paramref name="str"/>.</param>
        ''' <param name="Value"> When this method returns, contains the <see cref="SRational"/> value equivalent to the number contained in <paramref name="str"/>, if the conversion succeeded.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <exception cref="ArgumentException"><paramref name="style"/> is not a <see cref="System.Globalization.NumberStyles"/> value. -or- <paramref name="style"/> is not a combination of <see cref="System.Globalization.NumberStyles.AllowHexSpecifier"/> and <see cref="System.Globalization.NumberStyles.HexNumber"/> values. -or- <paramref name="style"/> is the <see cref="System.Globalization.NumberStyles.AllowHexSpecifier"/> value.</exception>
        ''' <version stage="Alpha" version="1.5.2">Method introduced</version>
        Public Shared Function TryParse(ByVal str$, ByRef style As Globalization.NumberStyles, ByVal Provider As IFormatProvider, ByVal Value As SRational) As Boolean
            Return TryParseInternal(str, Value, Provider, style) Is Nothing
        End Function
        ''' <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="SRational"/> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
        ''' <param name="str">A string containing a number to convert.</param>
        ''' <param name="Value"> When this method returns, contains the <see cref="SRational"/> value equivalent to the number contained in <paramref name="str"/>, if the conversion succeeded.</param>
        ''' <returns>true if s was converted successfully; otherwise, false.</returns>
        ''' <version stage="Alpha" version="1.5.2">Method introduced</version>
        Public Shared Function TryParse(ByVal str$, ByRef Value As SRational) As Boolean
            Return TryParse(str, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, Value)
        End Function
        ''' <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="SRational"/> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
        ''' <param name="str">A string containing a number to convert.</param>
        ''' <param name="style">A bitwise combination of <see cref="System.Globalization.NumberStyles"/> values that indicates the permitted format of <paramref name="str"/></param>
        ''' <param name="Provider">An <see cref="System.IFormatProvider"/> objectthat supplies culture-specific formatting information about <paramref name="str"/>.</param>
        ''' <returns>A <see cref="SRational"/> number equivalent to the numeric value or symbol specified in <paramref name="str"/>.</returns>
        ''' <exception cref="ArgumentException"><paramref name="style"/> is not a <see cref="System.Globalization.NumberStyles"/> value. -or- <paramref name="style"/> is not a combination of <see cref="System.Globalization.NumberStyles.AllowHexSpecifier"/> and <see cref="System.Globalization.NumberStyles.HexNumber"/> values. -or- <paramref name="style"/> is the <see cref="System.Globalization.NumberStyles.AllowHexSpecifier"/> value.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="str"/> is null</exception>
        ''' <exception cref="FormatException"><paramref name="str"/> is not numeric value, or 2 numeric values separated by /.</exception>
        ''' <exception cref="OverflowException"><paramref name="str"/> represents value lower than <see cref="Double.MinValue"/> or greater than <see cref="Double.MaxValue"/> (for single-part number) -or- <paramref name="str"/> represents value lower than <see cref="Integer.MinValue"/> or greater than <see cref="Integer.MaxValue"/> (for double-part number)</exception>
        ''' <version stage="Alpha" version="1.5.2">Method introduced</version>
        Public Shared Function Parse(ByVal str$, ByVal style As Globalization.NumberStyles, ByVal Provider As IFormatProvider) As SRational
            Dim value As SRational
            Dim ret = TryParseInternal(str, value, Provider, style)
            If ret Is Nothing Then Return value
            If TypeOf ret Is InvalidCastException Then
                Return Double.Parse(str)
            ElseIf TypeOf ret Is TypeMismatchException Then
                Dim parts = str.Split("/"c)
                Return New SRational(Integer.Parse(parts(0)), Integer.Parse(parts(1)))
            Else
                Throw ret
            End If
        End Function
        ''' <summary>Converts the string representation of a number in a specified style and culture-specific format to its <see cref="SRational"/> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
        ''' <param name="str">A string containing a number to convert.</param>
        ''' <returns>A <see cref="SRational"/> number equivalent to the numeric value or symbol specified in <paramref name="str"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="str"/> is null</exception>
        ''' <exception cref="FormatException"><paramref name="str"/> is not numeric value, or 2 numeric values separated by /.</exception>
        ''' <exception cref="OverflowException"><paramref name="str"/> represents value lower than <see cref="Double.MinValue"/> or greater than <see cref="Double.MaxValue"/> (for single-part number) -or- <paramref name="str"/> represents value lower than <see cref="Integer.MinValue"/> or greater than <see cref="Integer.MaxValue"/> (for double-part number)</exception>
        ''' <version stage="Alpha" version="1.5.2">Method introduced</version>
        Public Shared Function Parse(ByVal str$) As SRational
            Return Parse(str, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture)
        End Function
#End Region
        ''' <summary>Formats the value of the current instance using the specified format.</summary>
        ''' <param name="format">The <see cref="System.String" /> specifying the format to use.-or- null to use the default format defined for the type of the <see cref="System.IFormattable" /> implementation.</param>
        ''' <param name="formatProvider">The <see cref="System.IFormatProvider" /> to use to format the value.-or- null to obtain the numeric format information from the current locale setting of the operating system.</param>
        ''' <returns>A <see cref="System.String" /> containing the value of the current instance in the specified format.</returns>
        ''' <remarks>
        ''' Use sigle format string to format value as <see cref="Double"/>. Use two /-separated format strings to format this value as two <see cref="Integer"/> values separated by /.
        ''' Format(s) passed to <see cref="Double.ToString"/> or <see cref="Integer.ToString"/> can be empty, predefined (one letter) or custom.
        ''' If two formats are specified, delimited by /, only first slash encountered is treatead as delimitter. Other slashes are passed to <see cref="Integer.Format"/>. In order to escape firts slahs, precede it with \.
        ''' If <paramref name="format"/> is null or <see cref="String.Empty"/> G/G is used. 
        ''' </remarks>
        ''' <version stage="Alpha" version="1.5.2">Method introduced</version>
        Public Overloads Function ToString(ByVal format As String, ByVal formatProvider As System.IFormatProvider) As String Implements System.IFormattable.ToString
            If format = "" Then format = "G/G"
            Dim FSplit As Integer = -1
            For i As Integer = 0 To format.Length - 1
                If format(i) = "/"c AndAlso (i = 0 OrElse format(i - 1) <> "\"c) Then
                    FSplit = i
                    Exit For
                End If
            Next
            If FSplit >= 0 Then
                Return Me.Numerator.ToString(format.Substring(0, FSplit), formatProvider) & "/" & Me.Denominator.ToString(format.Substring(FSplit + 1), formatProvider)
            Else
                If Me.Denominator = 0 AndAlso Me.Numerator = 0 Then
                    Return 0.ToString(format, formatProvider)
                ElseIf Me.Denominator = 0 AndAlso Me.Numerator > 0 Then
                    Return Double.PositiveInfinity.ToString(format, formatProvider)
                ElseIf Me.Denominator = 0 Then
                    Return Double.NegativeInfinity.ToString(format, formatProvider)
                Else
                    Return CType(Me, Double).ToString(format, formatProvider)
                End If
            End If
        End Function
        ''' <summary>Implemenst <see cref="ComponentModel.TypeConverter"/> for <see cref="String"/> and <see cref="SRational"/></summary>
        ''' <version stage="Alpha" version="1.5.2">Class introduced</version>
        Public Class SRationalConverter
            Inherits ComponentModelT.TypeConverter(Of SRational, String)
            ''' <summary>Performs conversion from type <see cref="String"/> to type <see cref="SRational"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to type <see cref="SRational"/></param>
            ''' <returns>Value of type <see cref="SRational"/> initialized by <paramref name="value"/></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
            ''' <exception cref="FormatException"><paramref name="str"/> is not numeric value, or 2 numeric values separated by /.</exception>
            ''' <exception cref="OverflowException"><paramref name="str"/> represents value lower than <see cref="Double.MinValue"/> or greater than <see cref="Double.MaxValue"/> (for single-part number) -or- <paramref name="str"/> represents value lower than <see cref="Integer.MinValue"/> or greater than <see cref="Integer.MaxValue"/> (for double-part number)</exception>
            Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As String) As SRational
                Return SRational.Parse(value, Globalization.NumberStyles.Any, culture)
            End Function
            ''' <summary>Performs conversion from type <see cref="SRational"/> to type <see cref="String"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in type <see cref="String"/></returns>
            Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As SRational) As String
                Return value.ToString(Nothing, culture)
            End Function
        End Class
    End Structure


#End If
End Namespace