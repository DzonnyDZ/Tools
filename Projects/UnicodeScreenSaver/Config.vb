Imports System.Globalization.CultureInfo
Imports System.Runtime.CompilerServices
Imports System.Windows.Markup
Imports Tools.TextT.UnicodeT

''' <summary>Provides global functionality of the application</summary>
Friend Module Config
    Public Const DefaultFontPath$ = "pack://application:,,,/"
    Public Const DefaultFontName$ = "Unicode composite font"

    ''' <summary>Ranges of all code pints defined by Unicode (excluding surrogates)</summary>
    Private ReadOnly defaultRanges As SimpleRange() = {New SimpleRange(0UI, &HD800UI - 1UI), New SimpleRange(&HDFFUI + 1UI, &H10FFFFUI)} 'Excludes just surrogates

    ''' <summary>Gets instance of font to display characters in</summary>
    Public ReadOnly Property SelectedFont As FontFamily
        <MethodImpl(MethodImplOptions.Synchronized)>
        Get
            Static ret As FontFamily
            If ret Is Nothing Then
                If My.Settings.FontPath = "" Then
                    ret = New FontFamily(My.Settings.FontName)
                Else
                    ret = New FontFamily(New Uri(My.Settings.FontPath), $"./#{My.Settings.FontName}")
                End If
            End If
            Return ret
        End Get
    End Property

    ''' <summary>Gets character ranges covered by <see cref="SelectedFont"/></summary>
    ''' <returns>Character ranges covered by <see cref="SelectedFont"/>. Each range contains 1ts code point, last code point, name of font used</returns>
    Private ReadOnly Property RangesInFont As IEnumerable(Of Tuple(Of UInteger, UInteger, String))
        <MethodImpl(MethodImplOptions.Synchronized)>
        Get
            Static ret As IEnumerable(Of Tuple(Of UInteger, UInteger, String))
            If ret Is Nothing Then
                If SelectedFont.FamilyMaps.Count > 0 Then 'Composite font
                    ret = GetRangesFromCompositeFont(SelectedFont)
                Else
                    ret = GetRangesFromPhysicalFont(SelectedFont)
                End If
            End If
            Return ret
        End Get
    End Property

    ''' <summary>Gets character ranges covered by non-composite font</summary>
    ''' <param name="selectedFont">Font to get ranges of</param>
    ''' <exception cref="ArgumentNullException"><paramref name="selectedFont"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="selectedFont"/> represents composite font which is not composite</exception>
    Private Iterator Function GetRangesFromCompositeFont(selectedFont As FontFamily) As IEnumerable(Of Tuple(Of UInteger, UInteger, String))
        If selectedFont Is Nothing Then Throw New ArgumentNullException(NameOf(selectedFont))
        If selectedFont.FamilyMaps.Count = 0 Then Throw New ArgumentException("The font is not composite", NameOf(selectedFont))

        For Each map In selectedFont.FamilyMaps
            For Each r In ParseRange(map.Unicode)
                Yield Tuple.Create(r.First, r.Last, map.Target)
            Next
        Next
    End Function

    ''' <summary>Gets character ranges covered by composite font</summary>
    ''' <param name="selectedFont">Font to get ranges of</param>
    ''' <exception cref="ArgumentNullException"><paramref name="selectedFont"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="selectedFont"/> represents composite font</exception>
    Private Iterator Function GetRangesFromPhysicalFont(selectedFont As FontFamily) As IEnumerable(Of Tuple(Of UInteger, UInteger, String))
        If selectedFont Is Nothing Then Throw New ArgumentNullException(NameOf(selectedFont))
        If selectedFont.FamilyMaps.Count > 0 Then Throw New ArgumentException("The font is composite", NameOf(selectedFont))

        Dim fontFamilyName = If(selectedFont.FamilyNames.ContainsKey(XmlLanguage.Empty), selectedFont.FamilyNames(XmlLanguage.Empty),
                                If(selectedFont.FamilyNames.ContainsKey(XmlLanguage.GetLanguage("en")), selectedFont.FamilyNames(XmlLanguage.GetLanguage("en")),
                                   If(selectedFont.FamilyNames.ContainsKey(XmlLanguage.GetLanguage("en-US")), selectedFont.FamilyNames(XmlLanguage.GetLanguage("en-US")),
                                      selectedFont.FamilyNames.First.Value)))
        Dim faces = selectedFont.GetTypefaces
        Dim glyphFaces As New List(Of GlyphTypeface)(faces.Count)
        Dim charsSupported As New HashSet(Of Integer)
        For Each tf In faces
            Dim gtf As GlyphTypeface = Nothing
            If tf.TryGetGlyphTypeface(gtf) Then
                For Each chs In gtf.CharacterToGlyphMap
                    If Not charsSupported.Contains(chs.Key) Then charsSupported.Add(chs.Key)
                Next
            End If
        Next
        Dim start As UInteger? = Nothing
        Dim last As UInteger? = Nothing
        For Each chs In charsSupported.OrderBy(Function(cp) cp)
            If start Is Nothing Then start = chs
            If last Is Nothing Then last = chs
            If chs > last + 1UI Then
                Yield Tuple.Create(start.Value, last.Value, fontFamilyName)
                start = Nothing
            End If
            last = chs
        Next
        Yield Tuple.Create(start.Value, last.Value, fontFamilyName)
    End Function


    ''' <summary>Parses Unicode range specified in hexa ranges or code-points</summary>
    ''' <param name="map">Hexa ranges or code points like AH14-AF00,BC00,DE11-DF00</param>
    ''' <returns>Ranges contained in <paramref name="map"/></returns>
    Public Iterator Function ParseRange(map As String) As IEnumerable(Of SimpleRange)
        If map Is Nothing Then Throw New ArgumentNullException(NameOf(map))
        For Each range In map.Split(",")
            If range.Contains("-") Then
                Dim parts = range.Split("-")
                Yield New SimpleRange(
                    UInteger.Parse(parts(0), Globalization.NumberStyles.AllowHexSpecifier, InvariantCulture),
                    UInteger.Parse(parts(1), Globalization.NumberStyles.AllowHexSpecifier, InvariantCulture)
                    )
            Else
                Dim value = UInteger.Parse(range, Globalization.NumberStyles.AllowHexSpecifier, InvariantCulture)
                Yield New SimpleRange(value, value)
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
        <MethodImpl(MethodImplOptions.Synchronized)>
        Get
            Static ret2 As UInteger?
            If ret2 Is Nothing Then
                Dim ret As UInteger = 0
                For Each range In Ranges
                    If range.Item2 >= range.Item1 Then ret += range.Item2 - range.Item1 + 1
                Next
                ret2 = ret
            End If
            Return ret2
        End Get
    End Property

    Private _unicodeRanges As List(Of SimpleRange)()
    ''' <summary>Gets Unicode ranges and supported characters in them</summary>
    ''' <returns>Array of arrays of characters supported in the ranges</returns>
    Public ReadOnly Property UnicodeRanges As List(Of SimpleRange)()
        Get
            If _unicodeRanges Is Nothing Then
                _unicodeRanges = GetUnicodeRanges()
            End If
            Return _unicodeRanges
        End Get
    End Property

    Private _unicodeRangesSplit As List(Of SimpleRange)()
    ''' <summary>Gets Unicode ranges and supported characters in them</summary>
    ''' <returns>Array of arrays of characters supported in the ranges (big ranges are split to smaller)</returns>
    Public ReadOnly Property UnicodeRangesSplit As List(Of SimpleRange)()
        Get
            If _unicodeRangesSplit Is Nothing Then
                _unicodeRangesSplit = SplitRanges(UnicodeRanges).ToArray
            End If
            Return _unicodeRangesSplit
        End Get
    End Property

    ''' <summary>Splits big rages to smaller, keeps small ranges intact</summary>
    ''' <param name="ranges">Ranges to possibly split</param>
    ''' <returns>Small ranges from <paramref name="ranges"/> are returned intact, big ranges are split</returns>
    Private Iterator Function SplitRanges(ranges As IEnumerable(Of List(Of SimpleRange))) As IEnumerable(Of List(Of SimpleRange))
        For Each inp In ranges
            If inp.CharacterCount >= My.Settings.BigRangeTreshold Then
                Dim count = 0
                Dim subRanges As New List(Of SimpleRange)
                For Each sr In inp
                    If sr.Last - sr.First + 1 + count >= My.Settings.RangeChunkSize Then
                        subRanges.Add(New SimpleRange(sr.First, sr.Last - (My.Settings.RangeChunkSize - count)))
                        Yield subRanges
                        subRanges = New List(Of SimpleRange)
                        If sr.Last - My.Settings.RangeChunkSize - count + 1 <= sr.Last Then
                            subRanges.Add(New SimpleRange(sr.Last - My.Settings.RangeChunkSize - count + 1, sr.Last))
                            count = subRanges.First.Last - subRanges.First.First + 1
                        Else
                            count = 0
                        End If
                    Else
                        subRanges.Add(sr)
                        count += sr.Last - sr.First + 1
                    End If
                Next
                If subRanges.Count > 0 Then Yield subRanges
            Else
                Yield inp
            End If
        Next
    End Function

    ''' <summary>Gets Unicode ranges and supported characters in them (no caching)</summary>
    ''' <returns>Array of arrays of characters supported in the ranges</returns>
    Private Function GetUnicodeRanges() As List(Of SimpleRange)()

        Dim blocks As New Dictionary(Of UInteger, List(Of SimpleRange))
        Dim ublocks = (
                From b In UnicodeCharacterDatabase.Default.Blocks
                Order By b.FirstCodePoint.CodePoint
                Select FirstCodePoint = b.FirstCodePoint.CodePoint, LastCodePoint = b.LastCodePoint.CodePoint, Block = b
                ).ToArray
        Dim currBlock = ublocks.Take(0).FirstOrDefault
        For Each range In Ranges
            For i = range.Item1 To range.Item2
                If currBlock Is Nothing OrElse currBlock.FirstCodePoint < i OrElse currBlock.LastCodePoint > i Then _
                    currBlock = (From b In ublocks Where b.FirstCodePoint <= i AndAlso b.LastCodePoint >= i).FirstOrDefault
                If currBlock Is Nothing Then Continue For
                Dim info As List(Of SimpleRange) = Nothing
                If Not blocks.TryGetValue(currBlock.FirstCodePoint, info) Then
                    info = New List(Of SimpleRange)
                    blocks.Add(currBlock.FirstCodePoint, info)
                End If
                If info.Count = 0 OrElse info(info.Count - 1).Last < i - 1 Then
                    info.Add(New SimpleRange(i, i))
                Else
                    info(info.Count - 1) = New SimpleRange(info(info.Count - 1).First, i)
                End If
            Next i
        Next
        Return blocks.Values.ToArray
    End Function


    ''' <summary>For given Unicode code point returns index of that code point to list of all characters selected for showing</summary>
    ''' <param name="codePoint">Unicode code point to get index of</param>
    ''' <returns>Index of <paramref name="codePoint"/> in all characters repertoire</returns>
    Public Function GetCharIndex(codePoint As Integer) As Integer
        If codePoint < 0 Then Return -1
        Dim idx = 0
        For Each r In Ranges
            If codePoint < r.Item1 Then Return -1 'Because ranges are sorted
            If codePoint >= r.Item1 AndAlso codePoint <= r.Item2 Then
                Return idx + codePoint - r.Item1
            End If
            idx += r.Item2 - r.Item1 + 1
        Next
        Return -1
    End Function

    ''' <summary>Gets count of characters in ranges</summary>
    ''' <param name="collection">Ranges to sum count of characters in</param>
    ''' <returns>Sum number of characters in all ranges given in <paramref name="collection"/></returns>
    <Extension>
    Public Function CharacterCount(collection As IEnumerable(Of SimpleRange)) As Integer
        If collection Is Nothing Then Throw New ArgumentNullException(NameOf(collection))
        Return (From r In collection Select r.Last - r.First + 1).Sum
    End Function

    ''' <summary>Gets Unicode code point by index to given code points</summary>
    ''' <param name="ranges">Ranges of code points</param>
    ''' <param name="index">0-based index</param>
    ''' <returns>Code point in one of the ranges defined in <see cref="Ranges"/> and appropriate 0-based index counted across all ranges.</returns>
    ''' <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is greater or equal to total number of characters in <paramref name="ranges"/></exception>
    <Extension>
    Public Function GetCharacter(ranges As IEnumerable(Of SimpleRange), index%) As UInt32
        Dim i As UInteger = 0
        For Each range In ranges
            If range.Last >= range.First Then
                Dim size = range.Last - range.First + 1
                If index < i + size Then Return range.First + index - i
                i += range.Last - range.First + 1
            End If
        Next
        Throw New ArgumentOutOfRangeException("index")
    End Function

    ''' <summary>For given Unicode code point returns index of that code point to list of ranges</summary>
    ''' <param name="ranges">Ranges to consider</param>
    ''' <param name="codePoint">Unicode code point to get index of</param>
    ''' <returns>Index of <paramref name="codePoint"/> in all characters repertoire</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="ranges"/> is null</exception>
    <Extension>
    Public Function GetCharIndex(ranges As IEnumerable(Of SimpleRange), codePoint As Integer) As Integer
        If ranges Is Nothing Then Throw New ArgumentNullException(NameOf(ranges))
        If codePoint < 0 Then Return -1
        Dim idx = 0
        For Each r In ranges
            If codePoint < r.First Then Return -1 'Because ranges are sorted
            If codePoint >= r.First AndAlso codePoint <= r.Last Then
                Return idx + codePoint - r.First
            End If
            idx += r.Last - r.First + 1
        Next
        Return -1
    End Function
End Module
