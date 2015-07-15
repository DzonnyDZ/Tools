Imports System.Globalization.CultureInfo
Imports System.Runtime.CompilerServices
Imports System.Windows.Markup

''' <summary>Provides global functionality of the application</summary>
Friend Module Config
    Public Const DefaultFontPath$ = "pack://application:,,,/"
    Public Const DefaultFontName$ = "Unicode composite font"

    ''' <summary>Ranges of all code pints defined by Unicode (excluding surrogates)</summary>
    Private ReadOnly defaultRanges As Tuple(Of UInteger, UInteger)() = {Tuple.Create(0UI, &HD800UI - 1UI), Tuple.Create(&HDFFUI + 1UI, &H10FFFFUI)} 'Excludes just surrogates

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
                If SelectedFont.FamilyMaps.Count > 0 Then    'Composite font
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
                Yield Tuple.Create(r.Item1, r.Item2, map.Target)
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

End Module
