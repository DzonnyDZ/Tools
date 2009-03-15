''' <summary>matemetické funkce</summary>
Public Module EosMath
    ''' <summary>Spoèítá sumu èísel v poli</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal ParamArray values As SByte()) As SByte
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Return Sum(DirectCast(values, IEnumerable(Of SByte)))
    End Function
    ''' <summary>Spoèítá sumu èísel v kolekci</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal values As IEnumerable(Of SByte)) As SByte
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Dim ret As SByte = 0
        For Each item As SByte In values
            ret += item
        Next
        Return ret
    End Function
    ''' <summary>Spoèítá sumu èísel v poli</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal ParamArray values As Byte()) As Byte
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Return Sum(DirectCast(values, IEnumerable(Of Byte)))
    End Function
    ''' <summary>Spoèítá sumu èísel v kolekci</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal values As IEnumerable(Of Byte)) As Byte
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Dim ret As Byte = 0
        For Each item As Byte In values
            ret += item
        Next
        Return ret
    End Function

    ''' <summary>Spoèítá sumu èísel v poli</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal ParamArray values As Short()) As Short
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Return Sum(DirectCast(values, IEnumerable(Of Short)))
    End Function
    ''' <summary>Spoèítá sumu èísel v kolekci</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal values As IEnumerable(Of Short)) As Short
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Dim ret As Short = 0
        For Each item As Short In values
            ret += item
        Next
        Return ret
    End Function

    ''' <summary>Spoèítá sumu èísel v poli</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal ParamArray values As UShort()) As UShort
        Return Sum(DirectCast(values, IEnumerable(Of UShort)))
    End Function
    ''' <summary>Spoèítá sumu èísel v kolekci</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal values As IEnumerable(Of UShort)) As UShort
        Dim ret As UShort = 0
        For Each item As UShort In values
            ret += item
        Next
        Return ret
    End Function

    ''' <summary>Spoèítá sumu èísel v poli</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal ParamArray values As Integer()) As Integer
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Return Sum(DirectCast(values, IEnumerable(Of Integer)))
    End Function
    ''' <summary>Spoèítá sumu èísel v kolekci</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal values As IEnumerable(Of Integer)) As Integer
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Dim ret As Integer = 0
        For Each item As Integer In values
            ret += item
        Next
        Return ret
    End Function

    ''' <summary>Spoèítá sumu èísel v poli</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal ParamArray values As UInteger()) As UInteger
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Return Sum(DirectCast(values, IEnumerable(Of UInteger)))
    End Function
    ''' <summary>Spoèítá sumu èísel v kolekci</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal values As IEnumerable(Of UInteger)) As UInteger
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Dim ret As UInteger = 0
        For Each item As UInteger In values
            ret += item
        Next
        Return ret
    End Function

    ''' <summary>Spoèítá sumu èísel v poli</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal ParamArray values As Long()) As Long
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Return Sum(DirectCast(values, IEnumerable(Of Long)))
    End Function
    ''' <summary>Spoèítá sumu èísel v kolekci</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal values As IEnumerable(Of Long)) As Long
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Dim ret As Long = 0
        For Each item As Long In values
            ret += item
        Next
        Return ret
    End Function

    ''' <summary>Spoèítá sumu èísel v poli</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal ParamArray values As ULong()) As ULong
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Return Sum(DirectCast(values, IEnumerable(Of ULong)))
    End Function
    ''' <summary>Spoèítá sumu èísel v kolekci</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal values As IEnumerable(Of ULong)) As ULong
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Dim ret As ULong = 0
        For Each item As ULong In values
            ret += item
        Next
        Return ret
    End Function

    ''' <summary>Spoèítá sumu èísel v poli</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal ParamArray values As Single()) As Single
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Return Sum(DirectCast(values, IEnumerable(Of Single)))
    End Function
    ''' <summary>Spoèítá sumu èísel v kolekci</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal values As IEnumerable(Of Single)) As Single
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Dim ret As Single = 0
        For Each item As Single In values
            ret += item
        Next
        Return ret
    End Function

    ''' <summary>Spoèítá sumu èísel v poli</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal ParamArray values As Double()) As Double
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Return Sum(DirectCast(values, IEnumerable(Of Double)))
    End Function
    ''' <summary>Spoèítá sumu èísel v kolekci</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal values As IEnumerable(Of Double)) As Double
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Dim ret As Double = 0
        For Each item As Double In values
            ret += item
        Next
        Return ret
    End Function

    ''' <summary>Spoèítá sumu èísel v poli</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal ParamArray values As Decimal()) As Decimal
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Return Sum(DirectCast(values, IEnumerable(Of Decimal)))
    End Function
    ''' <summary>Spoèítá sumu èísel v kolekci</summary>
    ''' <param name="values">Èísla k sumaci</param>
    ''' <returns>SUma èísel ve <paramref name="values"/></returns>
    ''' <exception cref="OverflowException">Souèet se nevejde do daného datového typu</exception>
    ''' <exception cref="ArgumentException"><paramref name="values"/> je null</exception>
    Public Function Sum(ByVal values As IEnumerable(Of Decimal)) As Decimal
        If values Is Nothing Then Throw New ArgumentNullException("values")
        Dim ret As Decimal = 0
        For Each item As Decimal In values
            ret += item
        Next
        Return ret
    End Function
End Module
