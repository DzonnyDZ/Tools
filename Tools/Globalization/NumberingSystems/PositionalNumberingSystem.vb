Imports Tools.ExtensionsT
#If Config <= Nightly Then 'Stage:Nightly
Namespace GlobalizationT.NumberingSystemsT
    ''' <summary>Implements positional numbering system such as binary, octal, decimal or hexadecimal</summary>
    ''' <remarks>This class can implement any positional numbering system including unary numbering system as long as characters are given to constructor.
    ''' <para>There are 5 predefined numbering systems: <see cref="PositionalNumberingSystem.Binary"/>, <see cref="PositionalNumberingSystem.Octal"/>, <see cref="PositionalNumberingSystem.[Decimal]"/> and <see cref="PositionalNumberingSystem.HexadecimalLowerCase"/> and <see cref="PositionalNumberingSystem.HexadecimalUperCase"/>.</para></remarks>
    ''' <version version="1.5.2">Class introduced</version>
    Public Class PositionalNumberingSystem
        Inherits NumberingSystem
        ''' <summary>Negative sign character</summary>
        Protected ReadOnly Minus As Char = "-"c
        ''' <summary>Characters representing numerals</summary>
        ''' <remarks>Firts character is for 0, 2nd is for 2, etc.</remarks>
        Protected ReadOnly Characters As Char()
        ''' <summary>CTor from numerals characters - creates new instance of the <see cref="PositionalNumberingSystem"/> class</summary>
        ''' <param name="Characters">Characters representing the numerals. Number of characters defines radix of numbering system. 1st character is zero, 2nd is one etc.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Characters"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Characters"/> is enmpty</exception>
        Public Sub New(ByVal ParamArray Characters As Char())
            If Characters Is Nothing Then Throw New ArgumentNullException("Characters")
            If Characters.Length = 0 Then Throw New ArgumentException(ResourcesT.Exceptions.ArrayCannotBeEmpty, "Characters")
            Me.Characters = Characters
            CheckCharacters()
        End Sub
        ''' <summary>CTor from numerals characters in string</summary>
        ''' <param name="Characters">Characters representing the numerals. Number of characters defines radix of numbering system. 1st character is zero, 2nd is one etc.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Characters"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Characters"/> is enmpty -or- </exception>
        Public Sub New(ByVal Characters As String)
            If Characters Is Nothing Then Throw New ArgumentNullException("Characters")
            If Characters.Length = 0 Then Throw New ArgumentException(ResourcesT.Exceptions.ArrayCannotBeEmpty, "Characters")
            Me.Characters = Characters.ToCharArray
            CheckCharacters()
        End Sub
        ''' <summary>Checks if all numerals character s are different and if none of them is same as <see cref="Minus"/></summary>
        ''' <exception cref="ArgumentException">Any of <see cref="Characters"/> is same asn another of <see cref="Characters"/> -or- Any or <see cref="Characters"/> is same as <see cref="Minus"/>.</exception>
        Private Sub CheckCharacters()
            For i As Integer = 0 To Characters.Length - 1
                If Characters(i) = Minus Then Throw New ArgumentException(ResourcesT.Exceptions.CharacterRepresntingNumeralCannotBeSameAsCharacterRepresenting)
                For j As Integer = i + 1 To Characters.Length - 1
                    If Characters(i) = Characters(j) Then Throw New ArgumentException(ResourcesT.Exceptions.CharactersRepreentingNumeralsMustBeDifferent)
                Next
            Next
        End Sub
        ''' <summary>CTor from numrals characters and minust sign</summary>
        ''' <param name="Minus">Character to represent minus sign</param>
        ''' <param name="Characters">Characters representing the numerals. Number of characters defines radix of numbering system. 1st character is zero, 2nd is one etc.</param>
        Public Sub New(ByVal Minus As Char, ByVal Characters As Char())
            If Characters Is Nothing Then Throw New ArgumentNullException("Characters")
            If Characters.Length = 0 Then Throw New ArgumentException(ResourcesT.Exceptions.ArrayCannotBeEmpty, "Characters")
            Me.Characters = Characters
            Me.Minus = Minus
            CheckCharacters()
        End Sub

        ''' <summary>CTor from radix - creates new instance of <see cref="PositionalNumberingSystem"/> class representing positional numbering system constructed in standard way</summary>
        ''' <param name="Radix">Radix - the base of numbering system (i.e. 10 for decimal or 16 for hexadecimal)</param>
        ''' <param name="Casing">Casing of letter-based numerals</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Radix"/> id lower than 1 or greater than 60</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Casing"/> is not member of <see cref="TextT.Casing"/></exception>
        ''' <remarks>Numbering system is constructed using decimal numerals 0-9 first, than letters A-Z (or a-z respectivelly) and then Greek letters Α-Ω (excluding code point 0x03A2) (or α-ω respectivelly - excluding ς). </remarks>
        Public Sub New(ByVal Radix As Integer, ByVal Casing As TextT.Casing)
            If Not Casing.IsDefined Then Throw New InvalidEnumArgumentException("Casing", Casing, Casing.GetType)
            If Radix < 1 Then Throw New ArgumentOutOfRangeException("Radix")
            If Radix = 1 Then
                Me.Characters = New Char() {"1"c}
            End If
            ReDim Me.Characters(Radix - 1)
            Dim chars = 0
            For i = AscW("0"c) To AscW("9"c)
                Me.Characters(chars) = ChrW(i)
                chars += 1
                If chars >= Radix Then Return
            Next
            For i = AscW(If(Casing = TextT.Casing.LowerCase, "a"c, "A"c)) To AscW(If(Casing = TextT.Casing.LowerCase, "z"c, "Z"c))
                Me.Characters(chars) = ChrW(i)
                chars += 1
                If chars >= Radix Then Return
            Next
            For i = AscW(If(Casing = TextT.Casing.LowerCase, "α"c, "Α"c)) To AscW(If(Casing = TextT.Casing.LowerCase, "ω"c, "Ω"c))
                If i = AscW("ς"c) OrElse i = &H3A2 Then Continue For
                Me.Characters(chars) = ChrW(i)
                chars += 1
                If chars >= Radix Then Return
            Next
            If chars < Radix Then Throw New ArgumentOutOfRangeException("Radix")
        End Sub
        ''' <summary>CTor from radix - creates new instance of <see cref="PositionalNumberingSystem"/> class representing positional numbering system constructed in standard way</summary>
        ''' <param name="Radix">Radix - the base of numbering system (i.e. 10 for decimal or 16 for hexadecimal)</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Radix"/> id lower than 1 or greater than 61</exception>
        ''' <remarks>Numbering system is constructed using decimal numerals 0-9 first, than letters a-z and then Greek letters α-ω.</remarks>
        Public Sub New(ByVal Radix%)
            Me.New(Radix, TextT.Casing.LowerCase)
        End Sub
        ''' <summary>Gets radix of curent numbering system</summary>
        ''' <returns>Base number of current numbering system. I.e. 10 for decimal or 16 for hexadecimal.</returns>
        Public ReadOnly Property Radix%()
            Get
                Return Characters.Length
            End Get
        End Property
        ''' <summary>Binary numbering system</summary>
        Public Shared Shadows ReadOnly Binary As New PositionalNumberingSystem("01")
        ''' <summary>Octal numbering system</summary>
        Public Shared Shadows ReadOnly Octal As New PositionalNumberingSystem("01234567")
        ''' <summary>Decimal numbering system</summary>
        Public Shared Shadows ReadOnly [Decimal] As New PositionalNumberingSystem("0123456789")
        ''' <summary>Hexadecimal numbering system with upper case letters</summary>
        Public Shared Shadows ReadOnly HexadecimalUperCase As New PositionalNumberingSystem("0123456789ABCDEF")
        ''' <summary>Hexadecimal numbering system with lower case letters</summary>
        Public Shared Shadows ReadOnly HexadecimalLowerCase As New PositionalNumberingSystem("0123456789abcdef")

        ''' <summary>Gets representation of given number in curent numbering system</summary>
        ''' <param name="value">Number to get representation of</param>
        ''' <returns>String representation of given integral number in current numbering system</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than <see cref="Minimum"/> or greater than <see cref="Maximum"/></exception>
        Public Overrides Function GetValue(ByVal value As Integer) As String
            If value < Minimum OrElse value > Maximum Then Throw New ArgumentOutOfRangeException("value")
            If Characters.Length = 1 Then
                Select Case value
                    Case Is < 0 : Return Minus & New String(Characters(0), -value)
                    Case Is > 0 : Return New String(Characters(0), value)
                    Case Else : Return ""
                End Select
            End If
            If value = 0 Then Return Characters(0)

            Dim ret As New System.Text.StringBuilder
            Dim Remaining = Math.Abs(value)
            Do
                Dim NowVal = Remaining - (Remaining \ Characters.Length) * Characters.Length
                ret.Append(Characters(NowVal))
                Remaining = (Remaining - NowVal) \ Characters.Length
            Loop While Remaining <> 0
            Dim ret2 = ret.ToString.Reverse
            Return If(value < 0, Minus & ret2, ret2)
        End Function

        ''' <summary>Gets value indicating if parsing of string representation to integer is suported by current numbering system</summary>
        ''' <returns>True</returns>
        Public Overrides ReadOnly Property SupportsParse() As Boolean
            Get
                Return True
            End Get
        End Property
        ''' <summary>Attempts to parse string representation of number in current numbering system to integer</summary>
        ''' <param name="value">String representation of number to parse</param>
        ''' <param name="result">When function exists with success contains parsed value representing <paramref name="value"/> as number</param>
        ''' <returns>When parsing is successfull returns null; otherwise returns exception describing the error.
        ''' <see cref="OverflowException"/> when number represented by <paramref name="value"/> cannot be represented as <see cref="Integer"/>.
        ''' <see cref="ArgumentNullException"/> when <paramref name="value"/> is null or it is an empty string and <see cref="Radix"/> is not 1.
        ''' <see cref="FormatException"/> when unsupported character is reached.</returns>
        Protected Overrides Function TryParseInternal(ByVal value As String, ByRef result As Integer) As System.Exception
            If value Is Nothing OrElse (value = "" AndAlso Characters.Length > 1) Then Return New ArgumentNullException("value")
            If value = "" AndAlso Characters.Length = 1 Then result = 0 : Return Nothing
            If value.Length = 1 AndAlso value(0) = Minus Then Return New FormatException(ResourcesT.Exceptions.NumberCannotConsistOfSignOnly)
            If Characters.Length = 1 Then
                For i As Integer = If(value(0) = Minus, 1, 0) To value.Length - 1
                    If value(i) <> Characters(0) Then Return New FormatException(ResourcesT.Exceptions.UnexpectedCharacter0.f(value(i)))
                Next
                If value(0) = Minus Then result = -(value.Length - 1) Else result = value.Length
                Return Nothing
            End If
            Dim retval = 0
            For i = value.Length - 1 To 0 Step -1
                If i = 0 AndAlso value(i) = Minus Then result = -retval : Return Nothing
                Dim weight = Array.IndexOf(Characters, value(i))
                If weight = -1 Then Return New FormatException(ResourcesT.Exceptions.UnexpectedCharacter0.f(value(i)))
                retval += weight * Characters.Length ^ (value.Length - i - 1)
            Next
            result = retval
            Return Nothing
        End Function
    End Class
End Namespace
#End If