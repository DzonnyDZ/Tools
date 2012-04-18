Imports System.Runtime.InteropServices

Namespace NumericsT
#If Config <= Alpha Then 'Stage: Alpha
    ''' <summary>Represents angle or time (such as GPS coordinates)</summary>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure Angle
        Implements IFormattable

        Private _value As Double

#Region "Basic properties"
        Public ReadOnly Property Degrees As Integer
            Get
                Return Math.Floor(_value)
            End Get
        End Property

        Public ReadOnly Property Minutes As Integer
            Get
                Return Math.Floor((_value - Degrees) * 60.0#)
            End Get
        End Property
        Public ReadOnly Property Seconds As Double
            Get
                Return ((_value - Degrees) * 60.0# - Minutes) * 60.0#
            End Get
        End Property


        Public ReadOnly Property TotalDegrees As Double
            Get
                Return _value
            End Get
        End Property

        Public ReadOnly Property TotalMinutes As Double
            Get
                Return _value * 60.0#
            End Get
        End Property

        Public ReadOnly Property TotalSeconds As Double
            Get
                Return _value * 60.0# * 60.0#
            End Get
        End Property
#End Region

#Region "Values"
        Public Shared ReadOnly zero As New Angle(0)
        Public Shared ReadOnly right As New Angle(90)
        Public Shared ReadOnly straing As New Angle(180)
        Public Shared ReadOnly full As New Angle(360)
#End Region

#Region "CTors"
        Public Sub New(value As Single)
            If Single.IsNaN(value) OrElse Single.IsInfinity(value) Then Throw New ArgumentException("Value cannot be NaN or infinity", "value")
            _value = value
        End Sub
        Public Sub New(value As Double)
            If Double.IsNaN(value) OrElse Double.IsInfinity(value) Then Throw New ArgumentException("Value cannot be NaN or infinity", "value")
            _value = value
        End Sub

        Public Sub New(value As Decimal)
            _value = value
        End Sub

        Public Sub New(value As Integer)
            _value = value
        End Sub

        Public Sub New(degrees As Integer, minutes As Integer, second As Double)
            _value = CDbl(degrees) + CDbl(minutes) / 60.0# + second / 60.0# / 60.0#
        End Sub
#End Region

#Region "Methods"
        Public Function Normalize() As Angle
            Dim angle = _value
            If angle <= 0.0# Then angle = 360.0# - angle
            Return angle Mod 360.0#
        End Function

        Public Function Normalize(maxAngle As Double) As Angle
            Dim angle = Normalize._value
            If angle > maxAngle Then Return angle - ((angle - maxAngle) \ 360) * 360 - 360.0#
            If angle < maxAngle - 360 Then Return angle + ((maxAngle - 360 - angle) \ 360) * 360 + 360.0#
            Return angle
        End Function
#End Region

#Region "Operators"
#Region "Cast"
#Region "From .NET types"

        Public Shared Widening Operator CType(a As Double) As Angle
            Return New Angle(a)
        End Operator
        Public Shared Widening Operator CType(a As Single) As Angle
            Return New Angle(a)
        End Operator
        Public Shared Widening Operator CType(a As Decimal) As Angle
            Return New Angle(a)
        End Operator
        Public Shared Widening Operator CType(a As Integer) As Angle
            Return New Angle(a)
        End Operator

        Public Shared Widening Operator CType(a As TimeSpan) As Angle
            Return New Angle(a.TotalHours)
        End Operator

        Public Shared Widening Operator CType(a As TimeSpanFormattable) As Angle
            Return New Angle(a.TotalHours)
        End Operator
#End Region
#Region "To .NET types"
        Public Shared Widening Operator CType(a As Angle) As Double
            Return a.TotalDegrees
        End Operator

        Public Shared Widening Operator CType(a As Angle) As Single
            Return a.TotalDegrees
        End Operator

        Public Shared Widening Operator CType(a As Angle) As Decimal
            Return a.TotalDegrees
        End Operator

        Public Shared Narrowing Operator CType(a As Angle) As Integer
            Return a.TotalDegrees
        End Operator

        Public Shared Widening Operator CType(a As Angle) As TimeSpan
            Return TimeSpan.FromHours(a.TotalDegrees)
        End Operator

        Public Shared Widening Operator CType(a As Angle) As TimeSpanFormattable
            Return TimeSpanFormattable.FromHours(a.TotalDegrees)
        End Operator
#End Region
#End Region
#Region "Comparison"
        Public Shared Operator <(a As Angle, b As Angle) As Boolean
            Return a.Normalize._value < b.Normalize._value
        End Operator
        Public Shared Operator >(a As Angle, b As Angle) As Boolean
            Return a.Normalize._value > b.Normalize._value
        End Operator
        Public Shared Operator <=(a As Angle, b As Angle) As Boolean
            Return a.Normalize._value <= b.Normalize._value
        End Operator
        Public Shared Operator >=(a As Angle, b As Angle) As Boolean
            Return a.Normalize._value >= b.Normalize._value
        End Operator
        Public Shared Operator =(a As Angle, b As Angle) As Boolean
            Return a.Normalize._value = b.Normalize._value
        End Operator
        Public Shared Operator <>(a As Angle, b As Angle) As Boolean
            Return a.Normalize._value <> b.Normalize._value
        End Operator

        Public Shared Operator <(a As Angle, b As Double) As Boolean
            Return a.Normalize._value < CType(b, Angle).Normalize._value
        End Operator
        Public Shared Operator >(a As Angle, b As Double) As Boolean
            Return a.Normalize._value > CType(b, Angle).Normalize._value
        End Operator
        Public Shared Operator <=(a As Angle, b As Double) As Boolean
            Return a.Normalize._value <= CType(b, Angle).Normalize._value
        End Operator
        Public Shared Operator >=(a As Angle, b As Double) As Boolean
            Return a.Normalize._value >= CType(b, Angle).Normalize._value
        End Operator
        Public Shared Operator =(a As Angle, b As Double) As Boolean
            Return a.Normalize._value = CType(b, Angle).Normalize._value
        End Operator
        Public Shared Operator <>(a As Angle, b As Double) As Boolean
            Return a.Normalize._value <> CType(b, Angle).Normalize._value
        End Operator
#End Region
#Region "Arithmetic"
        Public Shared Operator *(a As Angle, b As Double) As Angle
            Return New Angle(a._value * b)
        End Operator
        Public Shared Operator *(a As Angle, b As Single) As Angle
            Return New Angle(a._value * b)
        End Operator
        Public Shared Operator *(a As Angle, b As Integer) As Angle
            Return New Angle(a._value * b)
        End Operator

        Public Shared Operator /(a As Angle, b As Double) As Angle
            Return New Angle(a._value / b)
        End Operator
        Public Shared Operator /(a As Angle, b As Single) As Angle
            Return New Angle(a._value / b)
        End Operator
        Public Shared Operator /(a As Angle, b As Integer) As Angle
            Return New Angle(a._value / b)
        End Operator
#End Region
#End Region

#Region "Override"
        Public Overrides Function GetHashCode() As Integer
            Return Normalize._value.GetHashCode
        End Function
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is Angle Then Return Me = DirectCast(obj, Angle)
            If TypeOf obj Is Double Then Return Me = DirectCast(obj, Double)
            If TypeOf obj Is Single Then Return Me = CDbl(DirectCast(obj, Single))
            If TypeOf obj Is Integer Then Return Me = CDbl(DirectCast(obj, Integer))
            If TypeOf obj Is Decimal Then Return Me = CDbl(DirectCast(obj, Decimal))
            If TypeOf obj Is Byte Then Return Me = CDbl(DirectCast(obj, Byte))
            If TypeOf obj Is Short Then Return Me = CDbl(DirectCast(obj, Short))
            If TypeOf obj Is Long Then Return Me = CDbl(DirectCast(obj, Long))
            If TypeOf obj Is SByte Then Return Me = CDbl(DirectCast(obj, SByte))
            If TypeOf obj Is UShort Then Return Me = CDbl(DirectCast(obj, UShort))
            If TypeOf obj Is UInteger Then Return Me = CDbl(DirectCast(obj, UInteger))
            If TypeOf obj Is ULong Then Return Me = CDbl(DirectCast(obj, ULong))
            If TypeOf obj Is TimeSpan Then Return Me = CType(DirectCast(obj, TimeSpan), Angle)
            If TypeOf obj Is TimeSpanFormattable Then Return Me = CType(DirectCast(obj, TimeSpanFormattable), Angle)
            Return False
        End Function
#End Region

#Region "ToString"
        Public Overloads Overrides Function ToString() As String
            Return ToString(Nothing, Nothing)
        End Function
        Public Overloads Function ToString(format As String, formatProvider As IFormatProvider) As String Implements IFormattable.ToString
            If format Is Nothing Then format = "G"
            Select Case format                                                                    'parameter            no parameter    custom
                Case "G", "g", "", "l", "L" 'General/long -14°10'15.33"                           no dec places for "   as required     -d°mm'ss[.f]"
                Case "S", "s" 'Short -14°9'5"                                                     not allowed                           -d°mm'ss"
                Case "F", "f" 'Decimal -14.978425                                                 no decimal places      as required    -D
                Case "N" 'G - normalized 0-360                                                    no dec places for "    as required    N-d°mm'ss[.f]"
                Case "n" 'S - normalized 0-360                                                    not allowed                           N-d°mm'ss"
                Case "e" 'F - normalized 0-360                                                    no decimal places      as required    N-D
                Case "A" 'GPS latitude 14°10'15.33" N                                             no dec places for "    as required    N180d°mm'ss[.f]" a
                Case "a" 'GPS latitude 14°10'15 N                                                 not allowed                           N180d°mm'ss" a
                Case "b" 'GPS latitude 14.154 N                                                   no decimal places      as required    N180D a
                Case "O" 'GPS longitude 14°10'15.33" E                                            no dec places for "    as required    N180d°mm'ss[.f]" o
                Case "o" 'GPS longitude 14°10'15" E                                               not allowed                           N180d°mm'ss" o
                Case "p" 'GPS longitude 14.33145 E                                                no decimal places      as required    N180D o
                Case "t" 'Time short, no days 90:10:05                                            not allowed                           -d:mm:ss
                Case "T" 'Time long, no days 90:10:05.335                                         no dec places for "    as required    -d:mm:ss[.f]
                Case "d" 'Time short, with days 7.15:10:05                                        not allowed                           -[y.]d:mm:ss
                Case "D" 'Time long, with days 7.15:10:05.335                                     no dec places for "    as required    -[y.]d:mm:ss[.f]
                Case "E", "e" 'Rotations                                                          no dec places          max 4          -E
                Case "R", "r" 'Radians                                                            no dec places          max 4          -R
                Case "Z", "z" 'Grads                                                              no dec places          max 4          -Z
            End Select
            'Custom formatter:                                                                                                          repeat
            'D degrees, decimal                                                                   no dec places          as required    min digits
            'd degrees, whole                                                                     not allowed                           min digits
            'M minutes, decimal (excl degrees if d present)                                       no decimal places      as required    min digits
            'm minutes, whole                                                                     not allowed                           min digits
            'S seconds, decimal (excl degrees if d present, excl minutes if m present)            no decimal places      as required    min digits
            's seconds, whole                                                                     not allowed                           min digits
            'f second fraction                                                                    no decimal places      as required    min digits
            'Y days, decimal                                                                      no decimal places      as required    min digits
            'y days, whole                                                                        not allowed                           min digits
            'H degrees, decimal (excl days if y present)                                          no decimal places      as required    min digits
            'A Latitude specifier (north/south), long                                             not allowed
            'O Longitude specifier (east/west), long                                              not allowed
            'a Latitude specifier (N/S), short                                                    not allowed
            'o Longitude specifier (E/W), short                                                   not allowed
            'E Rotations value                                                                    no decimal places      max 4          min digits
            'R radians value                                                                      no decimal places      max 4          min digits
            'Z grads value                                                                        no decimal places      max 4          min digits
            '- minus sign (if required)                                                           not allowed
            '+ plus/minus sign (always)                                                           not allowed
            '. decimal point                                                                      not allowed
            '° degree sign                                                                        not allowed
            '' minute sing                                                                        not allowed
            '" second sing                                                                        not allowed
            ': time separator                                                                     not allowed
            '\ escape                                                                             single char            error          \
            '% if used as 1st character - ignored                                                 rest of string         error          % as first
            'N normalize to range                                                                 value for Normalize()  360
            'c use comaptibility characters for °,',"                                             not allowed
            '[] optional component specifier, can be nested. reners only if inside non-zero       inside                 empty
        End Function
#End Region
#Region "Parse"
        Public Shared Function Parse(value As String) As Angle

        End Function
        Public Shared Function Parse(value As String, formatProvider As IFormatProvider)

        End Function
        Public Shared Function TryParse(value As String, <Out()> ByRef result As Angle) As Boolean

        End Function
        Public Shared Function TryParse(value As String, formatProvider As IFormatProvider, <Out()> ByRef result As Angle) As Boolean

        End Function
#End Region
    End Structure
#End If
End Namespace