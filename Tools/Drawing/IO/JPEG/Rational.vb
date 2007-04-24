Namespace Drawing.Metadata
#If Config <= Nightly Then 'Stage:Nightly
    ''' <summary>Represents unsigned rational number with numerator and denominator as used in Exif</summary>
    <Author("Ðonny", "dzony@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(Math), LastChange:="04/24/2007")> _
    <CLSCompliant(False)> _
    Public Structure URational 'ASAP:Wiki
        Implements DataStructures.Generic.IPair(Of UInt16, UInt16)
        ''' <summary>Contains value of the <see cref="Numerator"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Numerator As UInt16
        ''' <summary>Contains value of the <see cref="Denominator"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Denominator As UInt16
        ''' <summary>CTor</summary>
        ''' <param name="Numerator">Numerator</param>
        ''' <param name="Denominator">Denominator</param>
        Public Sub New(ByVal Numerator As UInt16, ByVal Denominator As UInt16)
            Me.Numerator = Numerator
            Me.Denominator = Denominator
        End Sub
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        <Obsolete("Use type safe Clone instead"), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Function Clone1() As Object Implements System.ICloneable.Clone
            Return Me
        End Function
        ''' <summary>Swaps values <see cref="Numerator"/> and <see cref="Denominator"/></summary>
        Public Function Swap() As DataStructures.Generic.IPair(Of UShort, UShort) Implements DataStructures.Generic.IPair(Of UShort, UShort).Swap
            Return New URational(Denominator, Numerator)
        End Function
        ''' <summary>Numerator (1 in 1/2)</summary>
        Public Property Numerator() As UShort Implements DataStructures.Generic.IPair(Of UShort, UShort).Value1
            Get
                Return _Numerator
            End Get
            Set(ByVal value As UShort)
                _Numerator = value
            End Set
        End Property
        ''' <summary>Denominator (2 in 1/2)</summary>
        Public Property Denominator() As UShort Implements DataStructures.Generic.IPair(Of UShort, UShort).Value2
            Get
                Return _Denominator
            End Get
            Set(ByVal value As UShort)
                _Denominator = value
            End Set
        End Property
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Private Function Clone() As DataStructures.Generic.IPair(Of UShort, UShort) Implements ICloneable(Of DataStructures.Generic.IPair(Of UShort, UShort)).Clone
            Return Clone()
        End Function
        ''' <summary>Simplyfies <see cref="URational"/> to contain smallest possible <see cref="Numerator"/> and <see cref="Denominator"/></summary>
        Public Function Simplyfy() As URational
            If Numerator = 0 Then Return New URational(0, 1)
            If Denominator = 0 Then Return Me
            Dim GCD As UInt32 = Math.GCD(Numerator, Denominator)
            Return New URational(Numerator / GCD, Denominator / GCD)
        End Function
#Region "Operators"
        ''' <summary>Adds two <see cref="URational"/>s</summary>
        ''' <param name="a">First number to add</param>
        ''' <param name="b">Second number to add</param>
        ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
        Public Shared Operator +(ByVal a As URational, ByVal b As URational) As URational
            If a.Numerator = 0 Then Return b.Simplyfy
            If b.Numerator = 0 Then Return a.Simplyfy
            Dim LCM As UInt32 = Math.LCM(a.Denominator, b.Denominator)
            Dim ANum As UInt32 = a.Numerator * (LCM / a.Denominator)
            Dim BNum As UInt32 = b.Numerator * (LCM / b.Denominator)
            Return New URational(ANum + BNum, LCM).Simplyfy
        End Operator
        ''' <summary>Converts <see cref="URational"/> to <see cref="SRational"/></summary>
        ''' <param name="a">Number to be converted</param>
        ''' <returns>Signed representation of unsigned rational</returns>
        Public Shared Widening Operator CType(ByVal a As URational) As SRational
            Return New SRational(a.Numerator, a.Denominator)
        End Operator
        ''' <summary>Converts <see cref="SRational"/> to <see cref="URational"/></summary>
        ''' <param name="a">Number to be converted</param>
        ''' <returns>Unsigned representation of signed rational</returns>
        Public Shared Narrowing Operator CType(ByVal a As SRational) As URational
            Return New URational(a.Numerator, a.Denominator)
        End Operator
        ''' <summary>Multiplyes two <see cref="URational"/>s</summary>
        ''' <param name="a">First number to multiply</param>
        ''' <param name="b">Second number to multiply</param>
        ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
        Public Shared Operator *(ByVal a As URational, ByVal b As URational) As URational
            Return New URational(a.Numerator * b.Numerator, a.Denominator * b.Denominator)
        End Operator
        ''' <summary>Divides one number by other</summary>
        ''' <param name="a">Number to be divided</param>
        ''' <param name="b">Number to divide by</param>
        ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
        Public Shared Operator /(ByVal a As URational, ByVal b As URational) As URational
            Return a * b.Swap
        End Operator
        ''' <summary>Substracts two <see cref="URational"/>s</summary>
        ''' <param name="a">Number to substract from</param>
        ''' <param name="b">Number to be substracted</param>
        ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
        Public Shared Operator -(ByVal a As URational, ByVal b As URational) As SRational
            Return CType(a, SRational) - CType(b, SRational)
        End Operator
        ''' <summary>Negative value</summary>
        ''' <param name="a"><see cref="URational"/> to get negative value of</param>
        ''' <returns>Negative value of <paramref name="a"/></returns>
        Public Shared Operator -(ByVal a As URational) As SRational
            Return New SRational(-a.Numerator, a.Denominator)
        End Operator
        ''' <summary>Converts <see cref="Double"/> to <see cref="URational"/></summary>
        ''' <param name="a">Number to be converted</param>
        ''' <returns><see cref="URational"/> representation of <paramref name="a"/></returns>
        Public Shared Narrowing Operator CType(ByVal a As Double) As URational
            If a < 0 Then Throw New ArgumentOutOfRangeException("Cannot convert negative values to unsigned rational")
            If a = 0 Then Return New URational(0, 1)
            If System.Math.Truncate(a) = a Then Return New URational(a, 1)
            Dim Multiplied As Double = a * UInt16.MaxValue
            Dim GCD As Long = Math.GCD(Multiplied, UInt16.MaxValue)
            Return New URational(Multiplied / GCD, UInt16.MaxValue / GCD)
        End Operator
        ''' <summary>Converts <see cref="Single"/> to <see cref="URational"/></summary>
        ''' <param name="a">Number to be converted</param>
        ''' <returns><see cref="URational"/> representation of <paramref name="a"/></returns>
        Public Shared Narrowing Operator CType(ByVal a As Single) As URational
            Return CDbl(a)
        End Operator
        ''' <summary>Converts <see cref="URational"/> to <see cref="Double"/></summary>
        ''' <param name="a"><see cref="URational"/> to be converted</param>
        ''' <returns><see cref="Double"/> representation of <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As URational) As Double
            Return a.Denominator / a.Numerator
        End Operator
        ''' <summary>Converts <see cref="URational"/> to <see cref="String"/></summary>
        ''' <param name="a"><see cref="URational"/> to be converted</param>
        ''' <returns><see cref="String"/> representation of <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As URational) As String
            Return String.Format("{0}/{1}", a.Numerator, a.Denominator)
        End Operator
        ''' <summary>String representation</summary>
        Public Overrides Function ToString() As String
            Return Me
        End Function
        ''' <summary>Converts <see cref="String"/> to <see cref="URational"/></summary>
        ''' <param name="a"><see cref="String"/> to converts</param>
        ''' <returns><see cref="URational"/> value represented by <paramref name="a"/></returns>
        ''' <exception cref="InvalidCastException">When error ocures</exception>
        ''' <remarks><paramref name="a"/> must be in format \s*\d+\s*[/\s*\d+\s*]</remarks>
        Public Shared Narrowing Operator CType(ByVal a As String) As URational
            Try
                Dim i As Integer
                Dim Numerator As Long = 0
                Dim Denominator As Long = 1
                While a(i) = " "c : i += 1 : End While
                For i = i - 1 To a.Length
                    Select Case a(i)
                        Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                            Numerator = Numerator * 10 + CByte(CStr(a(i)))
                        Case Else : Exit For
                    End Select
                Next i
                If i = a.Length Then Return New URational(Numerator, 1)
                While a(i) = " "c : i += 1 : End While
                If i = a.Length Then Return New URational(Numerator, 1)
                If a(i) <> "/"c Then Throw New InvalidCastException("/ expected")
                i += 1
                While a(i) = " "c : i += 1 : End While
                For i = i - 1 To a.Length
                    Select Case a(i)
                        Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                            Denominator = Denominator * 10 + CByte(CStr(a(i)))
                        Case Else : Exit For
                    End Select
                Next i
                If i = a.Length Then Return New URational(Numerator, Denominator)
                While a(i) = " "c : i += 1 : End While
                If i = a.Length Then Return New URational(Numerator, Denominator)
                Throw New InvalidCastException("Unexpected character " & a(i))
            Catch ex As Exception
                If TypeOf ex Is InvalidCastException Then Throw
                Throw New InvalidCastException("Cannot convert string " & a & "into URational", ex)
            End Try
        End Operator
#End Region
    End Structure

    ''' <summary>Represents signed rational number with numerator and denominator as used in Exif</summary>
    <Author("Ðonny", "dzony@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(Math), LastChange:="04/24/2007")> _
    Public Structure SRational 'ASAP:Wiki
        Implements DataStructures.Generic.IPair(Of Int16, Int16)
        ''' <summary>Contains value of the <see cref="Numerator"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Numerator As Int16
        ''' <summary>Contains value of the <see cref="Denominator"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Denominator As Int16
        ''' <summary>CTor</summary>
        ''' <param name="Numerator">Numerator</param>
        ''' <param name="Denominator">Denominator</param>
        Public Sub New(ByVal Numerator As Int16, ByVal Denominator As Int16)
            Me.Numerator = Numerator
            Me.Denominator = Denominator
        End Sub
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        <Obsolete("Use type safe Clone instead"), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Function Clone1() As Object Implements System.ICloneable.Clone
            Return Me
        End Function
        ''' <summary>Swaps values <see cref="Numerator"/> and <see cref="Denominator"/></summary>
        Public Function Swap() As DataStructures.Generic.IPair(Of Short, Short) Implements DataStructures.Generic.IPair(Of Short, Short).Swap
            Return New SRational(Denominator, Numerator)
        End Function
        ''' <summary>Numerator (1 in 1/2)</summary>
        Public Property Numerator() As Short Implements DataStructures.Generic.IPair(Of Short, Short).Value1
            Get
                Return _Numerator
            End Get
            Set(ByVal value As Short)
                _Numerator = value
            End Set
        End Property
        ''' <summary>Denominator (2 in 1/2)</summary>
        Public Property Denominator() As Short Implements DataStructures.Generic.IPair(Of Short, Short).Value2
            Get
                Return _Denominator
            End Get
            Set(ByVal value As Short)
                _Denominator = value
            End Set
        End Property
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Private Function Clone() As DataStructures.Generic.IPair(Of Short, Short) Implements ICloneable(Of DataStructures.Generic.IPair(Of Short, Short)).Clone
            Return Clone()
        End Function
        ''' <summary>Simplyfies <see cref="SRational"/> to contain smallest possible <see cref="Numerator"/> and <see cref="Denominator"/></summary>
        Public Function Simplyfy() As SRational
            If Numerator = 0 Then Return New SRational(0, 1)
            If Denominator = 0 Then Return Me
            Dim Negative As Boolean = Numerator < 0 Xor Denominator < 0
            Dim GCD As UInt32 = Math.GCD(System.Math.Abs(Numerator), System.Math.Abs(Denominator))
            Return New SRational(Tools.VisualBasic.iif(Negative, -1, 1) * Numerator / GCD, Denominator / GCD)
        End Function
#Region "Operators"
        ''' <summary>Adds two <see cref="SRational"/>s</summary>
        ''' <param name="a">First number to add</param>
        ''' <param name="b">Second number to add</param>
        ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
        Public Shared Operator +(ByVal a As SRational, ByVal b As SRational) As SRational
            If a.Numerator = 0 Then Return b.Simplyfy
            If b.Numerator = 0 Then Return a.Simplyfy
            If a.Denominator < 0 Then a.Numerator *= -1
            If b.Denominator < 0 Then b.Numerator *= -1
            Dim LCM As Int32 = Math.LCM(a.Denominator, b.Denominator)
            Dim ANum As Int32 = a.Numerator * (LCM / a.Denominator)
            Dim BNum As Int32 = b.Numerator * (LCM / b.Denominator)
            Return New SRational(ANum + BNum, LCM).Simplyfy
        End Operator
        ''' <summary>Creates negative value of given <see cref="SRational"/></summary>
        ''' <param name="a">Value to negativize</param>
        ''' <returns>- <paramref name="a"/></returns>
       Public Shared Operator -(ByVal a As SRational) As SRational
            Return New SRational(a.Numerator * -1, a.Denominator)
        End Operator
        ''' <summary>Substracts two <see cref="SRational"/>s</summary>
        ''' <param name="a">Number to substract from</param>
        ''' <param name="b">Number to be substracted</param>
        ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
        Public Shared Operator -(ByVal a As SRational, ByVal b As SRational) As SRational
            Return a + -b
        End Operator
        ''' <summary>Multiplyes two <see cref="SRational"/>s</summary>
        ''' <param name="a">First number to multiply</param>
        ''' <param name="b">Second number to multiply</param>
        ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
        Public Shared Operator *(ByVal a As SRational, ByVal b As SRational) As SRational
            Return New SRational(a.Numerator * b.Numerator, a.Denominator * b.Denominator)
        End Operator
        ''' <summary>Divides one number by other</summary>
        ''' <param name="a">Number to be divided</param>
        ''' <param name="b">Number to divide by</param>
        ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
        Public Shared Operator /(ByVal a As SRational, ByVal b As SRational) As SRational
            Return a * b.Swap
        End Operator
        ''' <summary>Converts <see cref="Double"/> to <see cref="SRational"/></summary>
        ''' <param name="a">Number to be converted</param>
        ''' <returns><see cref="URational"/> representation of <paramref name="a"/></returns>
        Public Shared Narrowing Operator CType(ByVal a As Double) As SRational
            If a < 0 Then Throw New ArgumentOutOfRangeException("Cannot convert negative values to unsigned rational")
            If a = 0 Then Return New SRational(0, 1)
            If System.Math.Truncate(a) = a Then Return New SRational(a, 1)
            Dim Multiplied As Double = a * UInt16.MaxValue
            Dim GCD As Long = Math.GCD(System.Math.Abs(Multiplied), UInt16.MaxValue)
            Return New SRational(Multiplied / GCD, UInt16.MaxValue / GCD)
        End Operator
        ''' <summary>Converts <see cref="Single"/> to <see cref="URational"/></summary>
        ''' <param name="a">Number to be converted</param>
        ''' <returns><see cref="URational"/> representation of <paramref name="a"/></returns>
        Public Shared Narrowing Operator CType(ByVal a As Single) As SRational
            Return CDbl(a)
        End Operator
        ''' <summary>Converts <see cref="URational"/> to <see cref="Double"/></summary>
        ''' <param name="a"><see cref="URational"/> to be converted</param>
        ''' <returns><see cref="Double"/> representation of <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As SRational) As Double
            Return a.Denominator / a.Numerator
        End Operator
        ''' <summary>Converts <see cref="URational"/> to <see cref="String"/></summary>
        ''' <param name="a"><see cref="URational"/> to be converted</param>
        ''' <returns><see cref="String"/> representation of <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As SRational) As String
            Return String.Format("{0}/{1}", a.Numerator, a.Denominator)
        End Operator
        ''' <summary>String representation</summary>
        Public Overrides Function ToString() As String
            Return Me
        End Function
        ''' <summary>Converts <see cref="String"/> to <see cref="SRational"/></summary>
        ''' <param name="a"><see cref="String"/> to converts</param>
        ''' <returns><see cref="URational"/> value represented by <paramref name="a"/></returns>
        ''' <exception cref="InvalidCastException">When error ocures</exception>
        ''' <remarks><paramref name="a"/> must be in format \s*-\s*?\d+\s*[/\s*-?\s*\d+\s*]</remarks>
        Public Shared Narrowing Operator CType(ByVal a As String) As SRational
            Try
                Dim i As Integer
                Dim Numerator As Long = 0
                Dim Denominator As Long = 1
                Dim NumM As SByte = 1
                Dim DenM As SByte = 1
                While a(i) = " "c : i += 1 : End While
                If a(i) = "-"c Then
                    i += 1
                    NumM = -1
                    While a(i) = " "c : i += 1 : End While
                End If
                For i = i - 1 To a.Length
                    Select Case a(i)
                        Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                            Numerator = Numerator * 10 + CByte(CStr(a(i)))
                        Case Else : Exit For
                    End Select
                Next i
                If i = a.Length Then Return New SRational(NumM * Numerator, 1)
                While a(i) = " "c : i += 1 : End While
                If i = a.Length Then Return New SRational(NumM * Numerator, 1)
                If a(i) <> "/"c Then Throw New InvalidCastException("/ expected")
                i += 1
                While a(i) = " "c : i += 1 : End While
                If a(i) = "-"c Then
                    i += 1
                    DenM = -1
                    While a(i) = " "c : i += 1 : End While
                End If
                For i = i - 1 To a.Length
                    Select Case a(i)
                        Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                            Denominator = Denominator * 10 + CByte(CStr(a(i)))
                        Case Else : Exit For
                    End Select
                Next i
                If i = a.Length Then Return New SRational(NumM * Numerator, DenM * Denominator)
                While a(i) = " "c : i += 1 : End While
                If i = a.Length Then Return New SRational(NumM * Numerator, DenM * Denominator)
                Throw New InvalidCastException("Unexpected character " & a(i))
            Catch ex As Exception
                If TypeOf ex Is InvalidCastException Then Throw
                Throw New InvalidCastException("Cannot convert string " & a & "into SRational", ex)
            End Try
        End Operator
#End Region
    End Structure
#End If
End Namespace