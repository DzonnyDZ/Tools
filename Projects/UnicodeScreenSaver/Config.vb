Imports System.Globalization.CultureInfo

Module Config
    Private ReadOnly defaultRanges As Tuple(Of UInteger, UInteger)() = {Tuple.Create(0UI, &HD800UI - 1UI), Tuple.Create(&HDFFUI + 1UI, &H10FFFFUI)} 'Excludes just surrogates

    Public ReadOnly Property CompositeFont As FontFamily
        Get
            Dim font = New FontFamily(New Uri("pack://application:,,,/"), "./#Unicode composite font")
            Return font
        End Get
    End Property

    Private ReadOnly Iterator Property RangesInFont As IEnumerable(Of Tuple(Of UInteger, UInteger, String))
        Get
            For Each map In CompositeFont.FamilyMaps
                For Each range In map.Unicode.Split(",")
                    If range.Contains("-") Then
                        Dim parts = range.Split("-")
                        Yield Tuple.Create(
                            UInteger.Parse(parts(0), Globalization.NumberStyles.AllowHexSpecifier, InvariantCulture),
                            UInteger.Parse(parts(1), Globalization.NumberStyles.AllowHexSpecifier, InvariantCulture),
                            map.Target
                        )
                    Else
                        Dim value = UInteger.Parse(range, Globalization.NumberStyles.AllowHexSpecifier, InvariantCulture)
                        Yield Tuple.Create(value, value, map.Target)
                    End If
                Next
            Next
        End Get
    End Property

    Public Function GetRange(code As UInteger) As Tuple(Of UInteger, UInteger, String)
        For Each range In RangesInFont
            If code >= range.Item1 AndAlso code <= range.Item2 Then Return range
        Next
        Return Nothing
    End Function

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

    Public ReadOnly Property Ranges As IEnumerable(Of Tuple(Of UInteger, UInteger, String))
        Get
            'Return {Tuple.Create(0UI, 1024UI, "aaa")}
            Return RangesInFont
        End Get
    End Property

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
