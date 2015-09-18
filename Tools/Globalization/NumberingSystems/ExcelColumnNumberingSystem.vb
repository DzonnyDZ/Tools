Imports Tools.ExtensionsT
#If True
Namespace GlobalizationT.NumberingSystemsT
    ''' <summary>Implements numbering system used for column numbering in spreadsheet editors like Microsft Excel</summary>
    ''' <remarks>This class has no public constructor. Use <see cref="ExcelColumnNumberingSystem.[Default]"/> instead; or you can use static methods.
    ''' <para>This class supports up to <see cref="Integer.MaxValue"/> (FXSHRXW) columns, but Microsft Excel supports only 256 (IV) columns in versions prior to 2007 and only 16384 (XFD) columns in version 2007.</para></remarks>
    ''' <version version="1.5.2">Class introduced</version>
    Public NotInheritable Class ExcelColumnNumberingSystem
        Inherits NumberingSystem
        ''' <summary>CTor</summary>
        Private Sub New()
        End Sub
        ''' <summary>Default instance of <see cref="ExcelColumnNumberingSystem"/> class</summary>
        Public Shared ReadOnly [Default] As New ExcelColumnNumberingSystem

        ''' <summary>Gets representation of given number in Excel columns numbering system</summary>
        ''' <param name="value">Number to get representation of</param>
        ''' <returns>String representation of given integral number in Excel columns numbering system</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than <see cref="Minimum"/> or greater than <see cref="Maximum"/></exception>
        ''' <remarks>This function provides same functionality as <see cref="GetColumnName"/>.</remarks>
        Public Overrides Function GetValue(ByVal value As Integer) As String
            If value < Minimum OrElse value > Maximum Then Throw New ArgumentOutOfRangeException("value")
            Dim remaining = value
            Dim ret = ""
            Do
                Dim now = remaining Mod 26
                If now = 0 Then ret = "Z"c & ret _
                Else ret = ChrW(AscW("A"c) + now - 1) & ret
                remaining = (remaining - 1) \ 26
            Loop While remaining > 0
            Return ret
        End Function
        ''' <summary>Gets Excel column name from its number</summary>
        ''' <param name="number">Number of column to get name of</param>
        ''' <returns>Name of column with number <paramref name="number"/></returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="number"/> is less than 1 or greater than <see cref="Integer.MaxValue"/></exception>
        ''' <remarks>This function provides same functionality as <see cref="GetValue"/>.</remarks>
        ''' <seelaso cref="GetValue"/>
        Public Shared Function GetColumnName(ByVal number As Integer) As String
            Return [Default].GetValue(number)
        End Function
        ''' <summary>Gets minimal supported number in Excel columns numbering system</summary>
        ''' <returns>1</returns>
        Public Overrides ReadOnly Property Minimum() As Integer
            Get
                Return 1
            End Get
        End Property
        ''' <summary>Gets value indicating if parsing of string representation to integer is suported by current numbering system</summary>
        ''' <returns>True</returns>
        Public Overrides ReadOnly Property SupportsParse() As Boolean
            Get
                Return True
            End Get
        End Property
        ''' <summary>Attempts to parse string representation of number in Excel column numbering system to integer</summary>
        ''' <param name="value">String representation of number to parse</param>
        ''' <param name="result">When function exists with success contains parsed value representing <paramref name="value"/> as number</param>
        ''' <returns>When parsing is successfull returns null; otherwise returns exception:
        ''' <list type="table">
        ''' <item><term><see cref="FormatException"/></term><description>Uexpected character (non A-Z) reached</description></item>
        ''' <item><term><see cref="ArgumentNullException"/></term><description><paramref name="value"/> is null or an empty string.</description></item>
        ''' <item><term><see cref="OverflowException"/></term><description><paramref name="value"/> represents number greater than <see cref="Maximum"/></description></item>
        ''' </list>
        ''' </returns>
        Protected Overrides Function TryParseInternal(ByVal value As String, ByRef result As Integer) As System.Exception
            If value.IsNullOrEmpty Then Throw New ArgumentNullException("value")
            Dim ret% = 0
            Dim multiplier = 1
            For i As Integer = value.Length - 1 To 0 Step -1
                If AscW(value(i)) < AscW("A"c) OrElse AscW(value(i)) > AscW("Z"c) Then Return New FormatException(ResourcesT.Exceptions.UnexpectedCharacter0.f(value(i)))
                Dim current = AscW(value(i)) - AscW("A"c) + 1
                ret += current * multiplier
                multiplier *= 26
            Next
            result = ret
            Return Nothing
        End Function

        ''' <summary>Gets number of column from its name</summary>
        ''' <param name="Name">Column name (uppercase)</param>
        ''' <returns>Column number of column named <paramref name="Name"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Name"/> null or an empty string</exception>
        ''' <exception cref="FormatException"><paramref name="Name"/> contains unexpected (non A-Z) character.</exception>   
        ''' <exception cref="OverflowException"><paramref name="Name"/> represents column number higher than <see cref="Integer.MaxValue"/></exception>
        ''' <remarks>This function provides same functionality as <see cref="Parse"/> for <see cref="ExcelColumnNumberingSystem"/>.</remarks>
        Public Shared Function GetColumnNumber(ByVal Name As String) As Integer
            Return [Default].Parse(Name)
        End Function
        ''' <summary>Attempts to get number of column fom its name</summary>
        ''' <param name="Name">Column name (upper case)</param>
        ''' <param name="Number">When function returns successfully contains column number</param>
        ''' <returns>True when <paramref name="Name"/> was successfully parsed to <paramref name="Number"/>; false when <paramref name="Name"/> is invalid column name.</returns>
        ''' <remarks>This function provides same functionality as <see cref="TryParse"/> for <see cref="ExcelColumnNumberingSystem"/>.</remarks>
        Public Shared Function TryGetColumnNumber(ByVal Name As String, <Runtime.InteropServices.Out()> ByRef Number As Integer) As Boolean
            Return [Default].TryParse(Name, Number)
        End Function
    End Class
End Namespace
#End If