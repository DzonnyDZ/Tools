''' <summary>Nástroje pro práci s daty</summary>
Public Module DateTools
    ''' <summary>Pøiète ke dnešku zadaný poèet dní a pokud to víde na víkend pøesune to na pondìlí</summary>
    ''' <param name="Days">Poèet dní k pøiètení</param>
    Public Function DaysFromNow(ByVal Days As UInteger) As Date
        Return DaysFrom(Now, Days)
    End Function
    ''' <summary>Pøiète k zadanému datu zadaný poèet dní a pokud to víde na víkend pøesune to na pondìlí</summary>
    ''' <param name="Day">Poèáteèní datum</param>
    ''' <param name="Days">Poèet dní k pøiètení</param>
    Public Function DaysFrom(ByVal Day As Date, ByVal Days As UInteger) As Date
        Return Weekend2Monday(Day.AddDays(Days))
    End Function
    ''' <summary>Pokdu se zadané datum nachází o víkendu vrátí datum nejbližšího následujícího pondìlku</summary>
    ''' <param name="Day">Datum ke kontrole</param>
    ''' <returns>Datum nejbližšího následujícího pracovního dne po <paramref name="Day"/> vèetnì</returns>
    Public Function Weekend2Monday(ByVal Day As Date) As Date
        Select Case Day.DayOfWeek
            Case DayOfWeek.Saturday
                Return Day.AddDays(2)
            Case DayOfWeek.Monday
                Return Day.AddDays(1)
            Case Else
                Return Day
        End Select
    End Function
    ''' <summary>Vrátí nejbližší následujíci den v mìsíci od zadaného data s urèeným èíslem dne v mìsíci a mimo víkend</summary>
    ''' <param name="Date">Datum od nìhož hledat</param>
    ''' <param name="Day">Èíslo dne v mìsíci, který vrátit</param>
    ''' <returns>Pokud je <paramref name="Day"/> 29, 30 nebo 31 a mìsíc nemá požadovaný poèet dní vrátí den v následujícím mìsíci</returns>
    ''' <exception cref="ArgumentException"><paramref name="Day"/> je 0 nebo víc jak 31</exception>
    Public Function DayInMonth(ByVal [Date] As Date, ByVal Day As Byte) As Date
        If Day = 0 OrElse Day > 31 Then Throw New ArgumentOutOfRangeException("Day", "Day musí být od 1 do 31")
        Dim First As Date
        If [Date].Day >= Day Then
            First = New Date([Date].Year, [Date].Month + 1, 1)
        Else
            First = New Date([Date].Year, [Date].Month, 1)
        End If
        Return Weekend2Monday(First.AddDays(Day - 1))
    End Function
    ''' <summary>Vrátí nejbližší následujíci den v mìsíci od teï s urèeným èíslem dne v mìsíci a mimo víkend</summary>
    ''' <param name="Day">Èíslo dne v mìsíci, který vrátit</param>
    ''' <returns>Pokud je <paramref name="Day"/> 29, 30 nebo 31 a mìsíc nemá požadovaný poèet dní vrátí den v následujícím mìsíci</returns>
    ''' <exception cref="ArgumentException"><paramref name="Day"/> je 0 nebo víc jak 31</exception>
    Public Function DayInMonth(ByVal Day As Byte) As Date
        Return DayInMonth(Now, Day)
    End Function
End Module
