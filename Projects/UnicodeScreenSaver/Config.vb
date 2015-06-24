Imports System.Globalization.CultureInfo

''' <summary>Provides global functionality of the application</summary>
Friend Module Config
    ''' <summary>Ranges of all code pints defined by Unicode (excluding surrogates)</summary>
    Private ReadOnly defaultRanges As Tuple(Of UInteger, UInteger)() = {Tuple.Create(0UI, &HD800UI - 1UI), Tuple.Create(&HDFFUI + 1UI, &H10FFFFUI)} 'Excludes just surrogates

    ''' <summary>Gets instance of composite font that contains many Unicode character definitions</summary>
    ''' <returns>Instance of composite font form application resources</returns>
    Public ReadOnly Property CompositeFont As FontFamily
        Get
            Dim font = New FontFamily(New Uri("pack://application:,,,/"), "./#Unicode composite font")
            Return font
        End Get
    End Property

    ''' <summary>Gets character ranges covered by <see cref="CompositeFont"/></summary>
    ''' <returns>Character ranges covered by <see cref="CompositeFont"/>. Each range contains 1ts code point, last code point, name of font used</returns>
    Private ReadOnly Iterator Property RangesInFont As IEnumerable(Of Tuple(Of UInteger, UInteger, String))
        Get
            For Each map In CompositeFont.FamilyMaps
                For Each r In ParseRange(map.Unicode)
                    Yield Tuple.Create(r.Item1, r.Item2, map.Target)
                Next
            Next
        End Get
    End Property

    ''' <summary>Parses Unicode range specified in hexa ranges or code-points</summary>
    ''' <param name="map">Hexa ranges or code points like AH14-AF00,BC00,DE11-DF00</param>
    ''' <returns>Ranges contained in <paramref name="map"/></returns>
    Public Iterator Function ParseRange(map As String) As IEnumerable(Of Tuple(Of UInteger, UInteger))
        If map Is Nothing Then Throw New ArgumentNullException(NameOf(map))
        For Each range In map.Split(",")
            If range.Contains("-") Then
                Dim parts = range.Split("-")
                Yield Tuple.Create(
                    UInteger.Parse(parts(0), Globalization.NumberStyles.AllowHexSpecifier, InvariantCulture),
                    UInteger.Parse(parts(1), Globalization.NumberStyles.AllowHexSpecifier, InvariantCulture)
                    )
            Else
                Dim value = UInteger.Parse(range, Globalization.NumberStyles.AllowHexSpecifier, InvariantCulture)
                Yield Tuple.Create(value, value)
            End If
        Next
    End Function

    ''' <summary>Finds which range form <see cref="RangesInFont"/> given code point belongs to</summary>
    ''' <param name="code">A code point to find range for</param>
    ''' <returns>Range <paramref name="code"/> belongs to. Null if appropriate range is not defined by <see cref="RangesInFont"/>.</returns>
    Public Function GetRange(code As UInteger) As Tuple(Of UInteger, UInteger, String)
        For Each range In RangesInFont
            If code >= range.Item1 AndAlso code <= range.Item2 Then Return range
        Next
        Return Nothing
    End Function

    ''' <summary>Gets Unicode code point by index to all code points defined in <see cref="Ranges"/></summary>
    ''' <param name="index">0-based index</param>
    ''' <returns>Code point in one of the ranges defined in <see cref="Ranges"/> and appropriate 0-based index counted across all ranges.</returns>
    ''' <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is greater or equal to <see cref="TotalCharacters"/></exception>
    Public Function GetCharacter(index As UInteger) As UInteger
        Dim i As UInteger = 0
        For Each range In Ranges
            If range.Item2 >= range.Item1 Then
                Dim size = range.Item2 - range.Item1 + 1
                If index < i + size Then Return range.Item1 + index - i
                i += range.Item2 - range.Item1 + 1
            End If
        Next
        Throw New ArgumentOutOfRangeException("index")
    End Function

    ''' <summary>Gets ranges configured to be shown</summary>
    Public ReadOnly Property Ranges As IEnumerable(Of Tuple(Of UInteger, UInteger, String))
        Get
            'Return {Tuple.Create(0UI, 1024UI, "aaa")}
            Return RangesInFont
        End Get
    End Property

    ''' <summary>Gets total count of characters in all <see cref="Ranges"/></summary>
    Public ReadOnly Property TotalCharacters As UInteger
        Get
            Dim ret As UInteger = 0
            For Each range In Ranges
                If range.Item2 >= range.Item1 Then ret += range.Item2 - range.Item1 + 1
            Next
            Return ret
        End Get
    End Property

End Module
