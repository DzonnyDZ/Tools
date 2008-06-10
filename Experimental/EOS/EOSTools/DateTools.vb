''' <summary>N�stroje pro pr�ci s daty</summary>
Public Module DateTools
    ''' <summary>P�i�te ke dne�ku zadan� po�et dn� a pokud to v�de na v�kend p�esune to na pond�l�</summary>
    ''' <param name="Days">Po�et dn� k p�i�ten�</param>
    Public Function DaysFromNow(ByVal Days As UInteger) As Date
        Return DaysFrom(Now, Days)
    End Function
    ''' <summary>P�i�te k zadan�mu datu zadan� po�et dn� a pokud to v�de na v�kend p�esune to na pond�l�</summary>
    ''' <param name="Day">Po��te�n� datum</param>
    ''' <param name="Days">Po�et dn� k p�i�ten�</param>
    Public Function DaysFrom(ByVal Day As Date, ByVal Days As UInteger) As Date
        Return Weekend2Monday(Day.AddDays(Days))
    End Function
    ''' <summary>Pokdu se zadan� datum nach�z� o v�kendu vr�t� datum nejbli���ho n�sleduj�c�ho pond�lku</summary>
    ''' <param name="Day">Datum ke kontrole</param>
    ''' <returns>Datum nejbli���ho n�sleduj�c�ho pracovn�ho dne po <paramref name="Day"/> v�etn�</returns>
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
    ''' <summary>Vr�t� nejbli��� n�sleduj�ci den v m�s�ci od zadan�ho data s ur�en�m ��slem dne v m�s�ci a mimo v�kend</summary>
    ''' <param name="Date">Datum od n�ho� hledat</param>
    ''' <param name="Day">��slo dne v m�s�ci, kter� vr�tit</param>
    ''' <returns>Pokud je <paramref name="Day"/> 29, 30 nebo 31 a m�s�c nem� po�adovan� po�et dn� vr�t� den v n�sleduj�c�m m�s�ci</returns>
    ''' <exception cref="ArgumentException"><paramref name="Day"/> je 0 nebo v�c jak 31</exception>
    Public Function DayInMonth(ByVal [Date] As Date, ByVal Day As Byte) As Date
        If Day = 0 OrElse Day > 31 Then Throw New ArgumentOutOfRangeException("Day", "Day mus� b�t od 1 do 31")
        Dim First As Date
        If [Date].Day >= Day Then
            First = New Date([Date].Year, [Date].Month + 1, 1)
        Else
            First = New Date([Date].Year, [Date].Month, 1)
        End If
        Return Weekend2Monday(First.AddDays(Day - 1))
    End Function
    ''' <summary>Vr�t� nejbli��� n�sleduj�ci den v m�s�ci od te� s ur�en�m ��slem dne v m�s�ci a mimo v�kend</summary>
    ''' <param name="Day">��slo dne v m�s�ci, kter� vr�tit</param>
    ''' <returns>Pokud je <paramref name="Day"/> 29, 30 nebo 31 a m�s�c nem� po�adovan� po�et dn� vr�t� den v n�sleduj�c�m m�s�ci</returns>
    ''' <exception cref="ArgumentException"><paramref name="Day"/> je 0 nebo v�c jak 31</exception>
    Public Function DayInMonth(ByVal Day As Byte) As Date
        Return DayInMonth(Now, Day)
    End Function
End Module
