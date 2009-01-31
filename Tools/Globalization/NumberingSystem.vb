Imports System.Runtime.InteropServices

#If Config <= Nightly Then 'Stage: Nighly
Namespace GlobalizationT
    ''' <summary>Bsae class for numbering systems</summary>
    ''' <completionlist cref="NumberingSystem"/>
    Public MustInherit Class NumberingSystem
        ''' <summary>When overiden in derived class gets representation of given number in curent numbering system</summary>
        ''' <param name="value">Number to get representation of</param>
        ''' <returns>String representation of given integral number in current numbering system</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than <see cref="Minimum"/> or greater than <see cref="Maximum"/></exception>
        Public MustOverride Function GetValue(ByVal value As Integer) As String
        ''' <summary>Gets maximal supported number in current numbering system</summary>
        ''' <returns>Maximal number supported by current numbering system</returns>
        ''' <remarks>Default value is <see cref="Integer.MaxValue"/></remarks>
        Public Overridable ReadOnly Property Maximum() As Integer
            <DebuggerStepThrough()> Get
                Return Integer.MaxValue
            End Get
        End Property
        ''' <summary>Gets minimal supported number in current numbering system</summary>
        ''' <returns>Minimal number supported by current numbering system</returns>
        ''' <remarks>Default value is <see cref="Integer.MinValue"/></remarks>
        Public Overridable ReadOnly Property Minimum() As Integer
            <DebuggerStepThrough()> Get
                Return Integer.MinValue
            End Get
        End Property
        ''' <summary>Gets value indicating if parsing of string representation to integer is suported by current numbering system</summary>
        ''' <returns>True if parsing is supported; false if not. This implementation return false</returns>
        ''' <remarks>Note for inheritors: It is enough to return false from this property in class derived from numbering fromat that supports parsing to indicate that derived class does not support parsing. <see cref="Parse"/> and <see cref="TryParse"/> will throw <see cref="NotSupportedException"/> automatically.</remarks>
        Public Overridable ReadOnly Property SupportsParse() As Boolean
            <DebuggerStepThrough()> Get
                Return False
            End Get
        End Property
        ''' <summary>When overriden in derived class attempts to parse string representation of number in current numbering system to integer</summary>
        ''' <param name="value">String representation of number to parse</param>
        ''' <param name="result">When function exists with success contains parsed value representing <paramref name="value"/> as number</param>
        ''' <returns>When parsing is successfull returns null; otherwise returns exception (<see cref="FormatException"/> is preffered) describing the error.
        ''' <see cref="OverflowException"/> should be used when number higher than <see cref="Maximum"/> or lower than <see cref="Minimum"/> is parsed out.
        ''' <see cref="ArgumentNullException"/> may be used when <paramref name="value"/> is null or an empty string and curent format cannot iterpret it.</returns>
        ''' <remarks>This method should never throw an exception (unless <see cref="SupportsParse"/> is false and it throws <see cref="NotSupportedException"/>)</remarks>
        ''' <exception cref="NotSupportedException"><see cref="SupportsParse"/> is false. This implementation throws it always.</exception>
        Protected Overridable Function TryParseInternal(ByVal value As String, <Out()> ByRef result As Integer) As Exception
            Throw New NotImplementedException()
        End Function
        ''' <summary>When parsing is implemented by derived class parses a string representation of number in current numbering system to integer</summary>
        ''' <param name="value">String representation in number in current numbering format to be parsed</param>
        ''' <returns>Integral repreentation of number parsed fron <paramref name="value"/></returns>
        ''' <exception cref="NotSupportedException"><see cref="SupportsParse"/> is false</exception>
        ''' <exception cref="FormatException"><paramref name="value"/> is in invalid format according to current numbering system or is not recognized by current numbering system</exception>
        ''' <exception cref="OverflowException"><paramref name="value"/> represents number lower than <see cref="Minimum"/> or greater than <see cref="Maximum"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null or an empty string and current numeral format cannot interpret it</exception>
        ''' <exception cref="Exception">Any other exception may be thrown if numbering format defines so.</exception>
        ''' <remarks>For details see documantation of actual numbering format's <see cref="TryParseInternal"/>.</remarks>
        Public Function Parse(ByVal value As String) As Integer
            If Not SupportsParse Then Throw New NotImplementedException
            Dim ret%
            Dim ex = TryParseInternal(value, ret)
            If ex Is Nothing Then Return ret Else Throw ex
        End Function
        ''' <summary>When parsing is implemented by derived class attempts to parse a string representation of number in current numbering system to inter</summary>
        ''' <param name="value">String representation in number in current numbering format to be parsed</param>
        ''' <param name="result">When function exists successfully contains parsed value represeting <paramref name="value"/> as number</param>
        ''' <returns>True if parseing is successful; false otherwise</returns>
        ''' <exception cref="NotSupportedException"><see cref="SupportsParse"/> is false</exception>
        Public Function TryParse(ByVal value As String, ByRef result As Integer) As Boolean
            If Not SupportsParse Then Throw New NotImplementedException
            Return TryParseInternal(value, result) Is Nothing
        End Function

#Region "Systems"
        Public ReadOnly Property RomanUpperCase() As NumberingSystemsT.RomanNumberingSystem
            Get
                Return NumberingSystemsT.RomanNumberingSystem.UpperCase
            End Get
        End Property
        Public ReadOnly Property RomanLowerCase() As NumberingSystemsT.RomanNumberingSystem
            Get
                Return NumberingSystemsT.RomanNumberingSystem.LowerCase
            End Get
        End Property
        Public ReadOnly Property RomanUnicodeUpperCase() As NumberingSystemsT.RomanNumberingSystemUnicode
            Get
                Return NumberingSystemsT.RomanNumberingSystemUnicode.UpperCase
            End Get
        End Property
        Public ReadOnly Property RomanUnicodeLowerCase() As NumberingSystemsT.RomanNumberingSystemUnicode
            Get
                Return NumberingSystemsT.RomanNumberingSystemUnicode.LowerCase
            End Get
        End Property
#End Region
    End Class
End Namespace
#End If

