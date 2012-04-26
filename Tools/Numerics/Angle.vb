Imports System.Runtime.InteropServices
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Globalization.CultureInfo
Imports Tools.ExtensionsT
Imports System.Text
Imports System.Globalization

#If Config <= Alpha Then 'Stage: Alpha
Namespace NumericsT
    ''' <summary>Represents angle or time (such as GPS coordinates)</summary>
    ''' <remarks>
    ''' This class provides common methods for working with angles and it provides angle-specific string formatting.
    ''' <para>
    ''' When an <see cref="Angle"/> is used as a key in <see cref="Dictionary(Of T, U)"/> or <see cref="HashSet(Of T)"/> beaware that only 360°-normalized value is provided by to a collection.
    ''' <see cref="Angle.GetHashCode"/> works with 360°-normalized value only and <see cref="Angle.Equals"/> also compares only 360°-normalized values.
    ''' To change this behavior use alternative comparer such as <see cref="AngleComparer.NonNormalizing"/>.
    ''' </para>
    ''' </remarks>
    ''' <seelaso cref="Angle.Normalize"/>
    ''' <version version="1.5.4">This structure is new in version 1.5.4</version>
    'TODO: Percent / permile
    <StructLayout(LayoutKind.Sequential), Serializable()>
    Public Structure Angle
        Implements IFormattable, IComparable(Of Angle)
        ''' <summary>Angle value in degrees</summary>
        Private _value As Double

#Region "Basic properties"
        ''' <summary>Gets number of whole degrees of this angle</summary>
        Public ReadOnly Property Degrees As Integer
            Get
                Return Math.Floor(_value)
            End Get
        End Property

        ''' <summary>Gets number of whole minutes of this angle. The value exludes <see cref="Degrees"/></summary>
        ''' <remarks>Each degree has 60 minutes</remarks>
        Public ReadOnly Property Minutes As Integer
            Get
                Return Math.Floor((_value - Degrees) * 60.0#)
            End Get
        End Property

        ''' <summary>Gets number of seconds of this angle. The value excludes <see cref="Degrees"/> and <see cref="Minutes"/></summary>
        ''' <remarks>Each minute has 60 seconds</remarks>
        Public ReadOnly Property Seconds As Double
            Get
                Return ((_value - Degrees) * 60.0# - Minutes) * 60.0#
            End Get
        End Property

        ''' <summary>Gets total number of degrees in this angle</summary>
        Public ReadOnly Property TotalDegrees As Double
            Get
                Return _value
            End Get
        End Property

        ''' <summary>Gets total number of minutes in this angle (including <see cref="Degrees"/>)</summary>
        ''' <remarks>Each degree has 60 minutes</remarks>
        Public ReadOnly Property TotalMinutes As Double
            Get
                Return _value * 60.0#
            End Get
        End Property

        ''' <summary>Gets total number of seconds in this nagle (including <see cref="Degrees"/> and <see cref="Minutes"/>)</summary>
        ''' <remarks>Each minute has 60 seconds</remarks>
        Public ReadOnly Property TotalSeconds As Double
            Get
                Return _value * 60.0# * 60.0#
            End Get
        End Property

        ''' <summary>Gets number of rotations this angle represents</summary>
        ''' <remarks>Each rotation is 360 degrees</remarks>
        Public ReadOnly Property Rotations As Double
            Get
                Return _value / 360.0#
            End Get
        End Property
#End Region

#Region "Conversion"
        ''' <summary>Gets value of this angle as degrees</summary>
        ''' <returns><see cref="Degrees"/></returns>
        Public Function ToDegrees#()
            Return _value
        End Function
        ''' <summary>Gets value as this angle as radians</summary>
        ''' <returns>Value of this angle in radisna</returns>
        ''' <remarks>360 degrees is 2*π radians</remarks>
        Public Function ToRadians#()
            Return _value * (Math.PI / 180.0#)
        End Function
        ''' <summary>Creates an <see cref="Angle"/> value from degrees</summary>
        ''' <param name="value">Number of degrees for new angle</param>
        ''' <returns>A new instance of <see cref="Angle"/> initialized to <paramref name="value"/> degrees.</returns>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is infinity or NaN</exception>
        Public Shared Function FromDegrees(value#) As Angle
            Return New Angle(value)
        End Function

        ''' <summary>Creates an <see cref="Angle"/> value from minutes</summary>
        ''' <param name="value">Number of minutes (1/60 degree) for new angle</param>
        ''' <returns>A new instance of <see cref="Angle"/> initialized to <paramref name="value"/> minutes.</returns>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is infinity or NaN</exception>
        Public Shared Function FromMinutes(value#) As Angle
            Return New Angle(value / 60.0#)
        End Function

        ''' <summary>Creates an <see cref="Angle"/> value from seconds</summary>
        ''' <param name="value">Number of seconds (1/3600 degree) for new angle</param>
        ''' <returns>A new instance of <see cref="Angle"/> initialized to <paramref name="value"/> seconds.</returns>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is infinity or NaN</exception>
        Public Shared Function FromSeconds(value#) As Angle
            Return New Angle(value / 60.0# / 60.0#)
        End Function

        ''' <summary>Creates an <see cref="Angle"/> value from radians</summary>
        ''' <param name="value">Angle in radians</param>
        ''' <returns>A new instance of <see cref="Angle"/> initialized to equivalent of <paramref name="value"/> radians</returns>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is infinity or NaN</exception>
        ''' <remarks>360 degrees is 2*π radians</remarks>
        Public Shared Function FromRadians(value#) As Angle
            Return New Angle(value * (180.0# / Math.PI))
        End Function
        ''' <summary>Creates an <see cref="Angle"/> value from rotations</summary>
        ''' <param name="value">Number of raotations</param>
        ''' <returns>A new instance of <see cref="Angle"/> initialized to equivalent of <paramref name="value"/> rotations</returns>
        ''' <remarks>One rotation is 360 degrees</remarks>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is infinity or NaN</exception>
        Public Shared Function FromRotations(value#) As Angle
            Return New Angle(value / 360.0#)
        End Function
        ''' <summary>Gets value of this angle in gradians (grads)</summary>
        ''' <returns>Value of this angle in gradians (grads)</returns>
        ''' <remarks>360 degrees is 400 grads</remarks>
        Public Function ToGradians#()
            Return Rotations * 400.0#
        End Function
        ''' <summary>Creates an <see cref="Angle"/> value from gradians (grads)</summary>
        ''' <param name="value">Angle in grads</param>
        ''' <returns>A new instance of <see cref="Angle"/> initialized to equaivalent of <paramref name="value"/> gradians</returns>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is infinity or NaN</exception>
        ''' <remarks>100 gradians is 90 degrees</remarks>
        Public Shared Function FromGradians(value#) As Angle
            Return FromRotations(value / 400.0#)
        End Function
        ''' <summary>Gets value of this angle in rotations</summary>
        ''' <returns>Value of this angle in rotations</returns>
        ''' <remarks>One rotation is 360 degrees</remarks>
        Public Function ToRotations() As Double
            Return Rotations
        End Function
#End Region

#Region "Values"
        ''' <summary>A zero angle (0°)</summary>
        Public Shared ReadOnly zero As New Angle(0)
        ''' <summary>Positive right angle (90°)</summary>
        Public Shared ReadOnly right As New Angle(90)
        ''' <summary>Straight angle (180°)</summary>
        Public Shared ReadOnly straing As New Angle(180)
        ''' <summary>Full angle (one rotation; 360°)</summary>
        Public Shared ReadOnly full As New Angle(360)
        ''' <summary>Largest possible negative angle</summary>
        ''' <remarks>Equals <see cref="System.Double.MinValue"/></remarks>
        Public Shared ReadOnly minValue As New Angle(Double.MinValue)
        ''' <summary>Largest possible positive angle</summary>
        ''' <remarks>Equals <see cref="System.Double.MaxValue"/></remarks>
        Public Shared ReadOnly maxValue As New Angle(Double.MaxValue)
        ''' <summary>An angle of 1°</summary>
        Public Shared ReadOnly degree As New Angle(1)
        ''' <summary>An angle of 1 rad</summary>
        Public Shared ReadOnly radian As Angle = Angle.FromRadians(1)
        ''' <summary>An angle of 1 grad</summary>
        Public Shared ReadOnly gradian As Angle = Angle.FromGradians(1)
        ''' <summary>An angle of 1π rad</summary>
        Public Shared ReadOnly πRadians As Angle = Angle.FromGradians(Math.PI)
#End Region

#Region "CTors"
        ''' <summary>CTor - creates a new instance of the <see cref="Angle"/> structure from degrees as <see cref="Single"/></summary>
        ''' <param name="value">Number of degrees</param>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is infinity or NaN</exception>
        Public Sub New(value As Single)
            If value.IsNaN OrElse value.IsInfinity Then Throw New ArgumentException(ResourcesT.Exceptions.ValueCannotBeNaNOrInfinity, "value")
            _value = value
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="Angle"/> structure from degrees as <see cref="Double"/></summary>
        ''' <param name="value">Number of degrees</param>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is infinity or NaN</exception>
        Public Sub New(value As Double)
            If value.IsNaN OrElse value.IsInfinity Then Throw New ArgumentException(ResourcesT.Exceptions.ValueCannotBeNaNOrInfinity, "value")
            _value = value
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="Angle"/> structure from degrees as <see cref="Decimal"/></summary>
        ''' <param name="value">Number of degrees</param>
        Public Sub New(value As Decimal)
            _value = value
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="Angle"/> structure from degrees as <see cref="Integer"/></summary>
        ''' <param name="value">Number of degrees</param>
        Public Sub New(value As Integer)
            _value = value
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="Angle"/> class from degrees, minutes and second</summary>
        ''' <param name="degrees">Number of degrees</param>
        ''' <param name="minutes">Number of minutes. If greater than 60 velue is tranferred to degrees.</param>
        ''' <param name="second">Number of second. If greater than 60 value is transfered to seconds.</param>
        ''' <remarks>Sign of angle is determined by first non-zero argument. If an argument after first non-zero argument is negative angle absolute value is lowered.</remarks>
        ''' <exception cref="ArgumentException"><paramref name="second"/> is NaN or infinity</exception>
        Public Sub New(degrees As Integer, minutes As Integer, second As Double)
            If second.IsNaN OrElse second.IsInfinity Then Throw New ArgumentException(ResourcesT.Exceptions.ValueCannotBeNaNOrInfinity, "second")
            _value = CDbl(degrees)
            _value += If(_value < 0, -1.0#, 1.0#) * (CDbl(minutes) / 60.0#)
            _value += If(_value < 0, -1.0#, 1.0#) * (second / 60.0# / 60.0#)
        End Sub
#End Region

#Region "Methods"
        ''' <summary>Creates a new instance of <see cref="Angle"/> that has value between 0° (inclusive) and 360° (exclusive)</summary>
        ''' <returns>An <see cref="Angle"/> that represents same azimuth and has value in range &lt;0°, 360°)</returns>
        Public Function Normalize() As Angle
            Dim angle = _value
            If angle <= 0.0# Then angle = 360.0# - angle
            Return angle Mod 360.0#
        End Function

        ''' <summary>Creates a new instance of <see cref="Angle"/> that has value in certain range</summary>
        ''' <param name="maxAngle">Maximum allowed (exclusive) angle value</param>
        ''' <returns>An <see cref="Angle"/> that represents same azimuth and has value in range &lt;<paramref name="maxAngle"/>-360, <paramref name="maxAngle"/>)</returns>
        ''' <exception cref="ArgumentException"><paramref name="maxAngle"/> is NaN or infinity</exception>
        Public Function Normalize(maxAngle As Double) As Angle
            If maxAngle.IsNaN OrElse maxAngle.IsInfinity Then Throw New ArgumentException(ResourcesT.Exceptions.ValueCannotBeNaNOrInfinity, "maxAngle")
            Dim angle = Normalize._value
            If angle > maxAngle Then Return angle - ((angle - maxAngle) \ 360) * 360 - 360.0#
            If angle < maxAngle - 360 Then Return angle + ((maxAngle - 360 - angle) \ 360) * 360 + 360.0#
            Return angle
        End Function

        ''' <summary>Gets an absolute value of this angle</summary>
        ''' <returns>Current angle without sign</returns>
        ''' <seelaso cref="Math.Abs"/>
        Public Function Abs() As Angle
            Return New Angle(Math.Abs(_value))
        End Function

        ''' <summary>Gets sign of this angle</summary>
        ''' <returns>-1 if current angle is negative, +1 if current angle is positive; 0 if current angle is zero</returns>
        ''' <seelaso cref="Math.Sign"/>
        Public Function Sign%()
            Return Math.Sign(_value)
        End Function

#Region "Goniometry"
        ''' <summary>Returns the cosine of current angle.</summary>
        ''' <returns>Cosine of current angle</returns>
        ''' <seelaso cref="Math.Cos"/>
        Public Function Cos() As Double
            Return Math.Cos(ToRadians)
        End Function

        ''' <summary>Returns the sine of current angle.</summary>
        ''' <returns>Sine of current angle</returns>
        ''' <seelaso cref="Math.Sin"/>
        Public Function Sin() As Double
            Return Math.Sin(ToRadians)
        End Function

        ''' <summary>Returns the tangent of current angle.</summary>
        ''' <returns>Tangent of current angle</returns>
        ''' <seelaso cref="Math.Tan"/>
        Public Function Tan() As Double
            Return Math.Tan(ToRadians)
        End Function

        ''' <summary>Returns the cotangent of current angle.</summary>
        ''' <returns>Cotangent of current angle</returns>
        ''' <remarks>Cot(x) is defined as 1/Tan(x)</remarks>
        Public Function Cot() As Double
            Return 1.0# / Math.Tan(ToRadians)
        End Function

        ''' <summary>Returns the cosecant of current angle.</summary>
        ''' <returns>Cosecant of current angle</returns>
        ''' <remarks>Csc(x) is defined as 1/Sin(x)</remarks>
        Public Function Csc() As Double
            Return 1.0# / Math.Sin(ToRadians)
        End Function

        ''' <summary>Returns the secant of current angle.</summary>
        ''' <returns>Secant of current angle</returns>
        ''' <remarks>Sec(x) is defined as 1/Cos(x)</remarks>
        Public Function Sec() As Double
            Return 1.0# / Math.Cos(ToRadians)
        End Function

#Region "Inverse"
        ''' <summary>Arcussine: Returns the angle whose sine is the specified number.</summary>
        ''' <param name="d">A number representing a sine, where <paramref name="d"/> must be greater than or equal to -1, but less than or equal to 1.</param>
        ''' <returns>An angle, θ, measured in degrees, such that -90° ≤ θ ≤ 90°.</returns>
        ''' <exception cref="ArgumentException"><paramref name="d"/> &lt; -1 or <paramref name="d"/> > 1 or <paramref name="d"/> equals <see cref="System.Double.NaN"/></exception>
        ''' <seelaso cref="Math.Asin"/>
        Public Shared Function Asin(d As Double) As Angle
            Return Angle.FromRadians(Math.Asin(d))
        End Function

        ''' <summary>Arcuscosine: Returns the angle whose cosine is the specified number.</summary>
        ''' <param name="d">A number representing a cosine, where <paramref name="d"/> must be greater than or equal to -1, but less than or equal to 1.</param>
        ''' <returns>An angle, θ, measured in degrees, such that 0° ≤ θ ≤ 180° -or-.</returns>
        ''' <exception cref="ArgumentException">d<paramref name=" "/>&lt; -1 or <paramref name="d"/> &gt; 1 or d equals <see cref="System.Double.NaN" /></exception>
        ''' <seelaso cref="Math.Acos"/>
        Public Shared Function Acos(d As Double) As Angle
            Return Angle.FromRadians(Math.Acos(d))
        End Function
        ''' <summary>Arcustangent: Returns the angle whose tangent is the specified number.</summary>
        ''' <param name="d">A number representing a tangent.</param>
        ''' <returns>An angle, θ, measured in degrees, such that -90° ≤ θ ≤ 90°. -90° if <paramref name="d"/> equals <see cref="System.Double.NegativeInfinity" />, or 90° if <paramref name="d"/> equals <see cref="System.Double.PositiveInfinity" />.</returns>
        ''' <exception cref="ArgumentException"><paramref name="d"/> equals <see cref="System.Double.NaN" /></exception>
        ''' <seelaso cref="Math.Atan"/>
        Public Shared Function Atan(d As Double) As Angle
            Return Angle.FromRadians(Math.Atan(d))
        End Function

        ''' <summary>Arcuscotangent: Returns the angle whose cotangent is the specified number.</summary>
        ''' <param name="d">A number representing a cotangent.</param>
        ''' <returns>An angle, θ, measured in degrees, such that -90° ≤ θ ≤ 90°.</returns>
        ''' <exception cref="ArgumentException"><paramref name="d"/> equals <see cref="System.Double.NaN" /></exception>
        Public Shared Function Acot(d As Double) As Angle
            Return Angle.FromRadians(Math.Atan(1.0# / d))
        End Function

        ''' <summary>Arcussecant: Returns the angle whose cecant is the specified number.</summary>
        ''' <param name="d">A number representing a secant, where <paramref name="d"/> must be less than or equal to -1, but greater than or equal to 1.</param>
        ''' <returns>An angle, θ, measured in degrees, such that 0° ≤ θ ≤ 180° -or-.</returns>
        ''' <exception cref="ArgumentException"><paramref name="d"/> is > -1 and <paramref name="d"/> &lt; 1 or <paramref name="d"/> equals <see cref="System.Double.NaN"/></exception>
        Public Shared Function Asec(d As Double) As Angle
            Return Angle.FromRadians(Math.Acos(1.0# / d))
        End Function

        ''' <summary>Arcuscosecant: Returns the angle whose cosecant is the specified number.</summary>
        ''' <param name="d">A number representing a cosecant, where <paramref name="d"/> must be less than or equal to -1, but greater than or equal to 1.</param>
        ''' <returns>An angle, θ, measured in degrees, such that -90° ≤ θ ≤ 90°.</returns>
        ''' <exception cref="ArgumentException"><paramref name="d"/> is > -1 and <paramref name="d"/> &lt; 1 or <paramref name="d"/> equals <see cref="System.Double.NaN"/></exception>
        Public Shared Function Acsc(d As Double) As Angle
            Return Angle.FromRadians(Math.Sin(1.0# / d))
        End Function

#End Region
#End Region

        ''' <summary>Rounds current angle to nearest whole degrees</summary>
        ''' <param name="mode">Optionaly defines how to round 30′</param>
        ''' <returns>An angle consisiting of whole degrees only</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="mode"/> is neither <see cref="MidpointRounding.AwayFromZero"/> nor <see cref="MidpointRounding.ToEven"/>.</exception>
        Public Function RoundToDegrees(Optional mode As MidpointRounding = MidpointRounding.AwayFromZero) As Angle
            Select Case mode
                Case MidpointRounding.AwayFromZero
                    Return New Angle(Me.Degrees + If(Me.Minutes >= 30, 1, 0)) * Me.Sign
                Case MidpointRounding.ToEven
                    Return New Angle(Me.Degrees + If(Me.Minutes > 30, 1,
                                                  If(Me.Minutes < 30, 0,
                                                  If(Degrees \ 2 = 0, 0, 1
                                     )))) * Me.Sign
                Case Else : Throw New InvalidEnumArgumentException("mode", mode, mode.GetType)
            End Select
        End Function

        ''' <summary>Truncates current angle to contain only whole degrees</summary>
        ''' <returns>A new instance of <see cref="Angle"/> that contains only <see cref="Degrees"/>.</returns>
        Public Function TruncateToDegrees() As Angle
            Return New Angle(Me.Degrees) * Me.Sign
        End Function

        ''' <summary>Rounds current angle to nearest whole degrees</summary>
        ''' <param name="mode">Optionaly defines how to round 30″</param>
        ''' <returns>An angle consisiting of whole degrees and minutes only</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="mode"/> is neither <see cref="MidpointRounding.AwayFromZero"/> nor <see cref="MidpointRounding.ToEven"/>.</exception>
        Public Function RoundToMinutes(Optional mode As MidpointRounding = MidpointRounding.AwayFromZero) As Angle
            Select Case mode
                Case MidpointRounding.AwayFromZero
                    Return New Angle(Me.Degrees, Me.Minutes + If(Me.Seconds >= 30.0#, 1, 0), 0.0#) * Me.Sign
                Case MidpointRounding.ToEven
                    Return New Angle(Me.Degrees, Me.Minutes + If(Me.Seconds > 30.0#, 1,
                                                              If(Me.Seconds < 30.0#, 0,
                                                              If(Minutes \ 2 = 0, 0, 1)
                                     )), 0.0#) * Me.Sign
                Case Else : Throw New InvalidEnumArgumentException("mode", mode, mode.GetType)
            End Select
        End Function

        ''' <summary>Truncates current angle to contain only whole degrees and minutes</summary>
        ''' <returns>A new instance of <see cref="Angle"/> that contains only <see cref="Degrees"/> and <see cref="Minutes"/>.</returns>
        Public Function TruncateToMinutes() As Angle
            Return New Angle(Me.Degrees, Me.Minutes, 0) * Me.Sign
        End Function

        ''' <summary>Rounds current angle to nearest whole seconds or decimal seconds fraction</summary>
        ''' <param name="digits">Optionally allows to round to second fraction. Defines to how much decimal places of seconds to rounbd.</param>
        ''' <param name="mode">Optionaly defines how to round 0.5″</param>
        ''' <returns>A rounded angle</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="mode"/> is neither <see cref="MidpointRounding.AwayFromZero"/> nor <see cref="MidpointRounding.ToEven"/>.</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="digits"/> is less than 0 or greater than 15.</exception>
        Public Function RoundToSeconds(Optional digits% = 0, Optional mode As MidpointRounding = MidpointRounding.AwayFromZero) As Angle
            Try
                Return New Angle(Me.Degrees, Me.Minutes, Math.Round(Me.Seconds, digits, mode)) * Me.Sign
            Catch ex As ArgumentException When Not TypeOf ex Is ArgumentOutOfRangeException AndAlso mode <> MidpointRounding.AwayFromZero AndAlso mode <> MidpointRounding.ToEven
                Throw New InvalidEnumArgumentException(ex.Message, ex)
            End Try
        End Function

        ''' <summary>Truncates current angle to contain only whole degrees, minutes and seconds</summary>
        ''' <returns>A new instance of <see cref="Angle"/> that contains only <see cref="Degrees"/>, <see cref="Minutes"/> and integral part of <see cref="Seconds"/></returns>
        Public Function TruncateToSeconds() As Angle
            Return New Angle(Me.Degrees, Me.Minutes, Math.Truncate(Me.Seconds)) * Me.Sign
        End Function

        ''' <summary>Converts an angle to nearest greater angle measured in whole degress.</summary>
        ''' <returns>A new instance of angle that contains only whole degrees and whichs absolute value is greater than or equal to current instance. Keeps sign.</returns>
        Public Function CeilingDegrees() As Angle
            Return New Angle(Math.Ceiling(Me.TotalDegrees)) * Me.Sign
        End Function

        ''' <summary>Converts an angle to nearest greater angle measured in whole degress and minutes.</summary>
        ''' <returns>A new instance of angle that contains only whole degrees and minutes and whichs absolute value is greater than or equal to current instance. Keeps sign.</returns>
        Public Function CeilingMinutes() As Angle
            Return Angle.FromMinutes(Math.Ceiling(Me.TotalMinutes)) * Me.Sign
        End Function

        ''' <summary>Converts an angle to nearest greater angle measured in whole degress, minutes and seconds.</summary>
        ''' <returns>A new instance of angle that contains only whole degrees, minutes and seconds and whichs absolute value is greater than or equal to current instance. Keeps sign.</returns>
        Public Function CeilingSeconds() As Angle
            Return Angle.FromSeconds(Math.Ceiling(Me.TotalSeconds)) * Me.Sign
        End Function

#End Region

#Region "Operators"
#Region "Cast"
#Region "From .NET types"
        ''' <summary>Converts a <see cref="Double"/> value to <see cref="Angle"/></summary>
        ''' <param name="a">Angle value in degrees</param>
        ''' <returns>A new instance of <see cref="Angle"/> initialized to <paramref name="a"/> degrees</returns>
        ''' <exception cref="ArgumentException"><paramref name="a"/> is NaN or infinity</exception>
        Public Shared Widening Operator CType(a As Double) As Angle
            Return New Angle(a)
        End Operator
        ''' <summary>Converts a <see cref="Single"/> value to <see cref="Angle"/></summary>
        ''' <param name="a">Angle value in degrees</param>
        ''' <returns>A new instance of <see cref="Angle"/> initialized to <paramref name="a"/> degrees</returns>
        ''' <exception cref="ArgumentException"><paramref name="a"/> is NaN or infinity</exception>
        Public Shared Widening Operator CType(a As Single) As Angle
            Return New Angle(a)
        End Operator
        ''' <summary>Converts a <see cref="Double"/> value to <see cref="Angle"/></summary>
        ''' <param name="a">Angle value in degrees</param>
        ''' <returns>A new instance of <see cref="Angle"/> initialized to <paramref name="a"/> degrees</returns>
        Public Shared Widening Operator CType(a As Decimal) As Angle
            Return New Angle(a)
        End Operator
        ''' <summary>Converts a <see cref="Double"/> value to <see cref="Angle"/></summary>
        ''' <param name="a">Angle value in degrees</param>
        ''' <returns>A new instance of <see cref="Angle"/> initialized to <paramref name="a"/> degrees</returns>
        Public Shared Widening Operator CType(a As Integer) As Angle
            Return New Angle(a)
        End Operator
        ''' <summary>Converts a <see cref="TimeSpan"/> value to <see cref="Angle"/></summary>
        ''' <param name="a">A <see cref="TimeSpan"/> to be converted</param>
        ''' <returns>A new instance of <see cref="Angle"/> initialized to <paramref name="a"/>.<see cref="TimeSpan.TotalHours">TotalHours</see> degrees</returns>
        Public Shared Widening Operator CType(a As TimeSpan) As Angle
            Return New Angle(a.TotalHours)
        End Operator

        ''' <summary>Converts a <see cref="TimeSpanFormattable"/> value to <see cref="Angle"/></summary>
        ''' <param name="a">A <see cref="TimeSpanFormattable"/> to be converted</param>
        ''' <returns>A new instance of <see cref="Angle"/> initialized to <paramref name="a"/>.<see cref="TimeSpanFormattable.TotalHours">TotalHours</see> degrees</returns>
        Public Shared Widening Operator CType(a As TimeSpanFormattable) As Angle
            Return New Angle(a.TotalHours)
        End Operator
#End Region
#Region "To .NET types"
        ''' <summary>Converts an <see cref="Angle"/> value to <see cref="Double"/></summary>
        ''' <param name="a">An angle value</param>
        ''' <returns><paramref name="a"/>.<see cref="TotalDegrees">TotalDegrees</see></returns>
        Public Shared Widening Operator CType(a As Angle) As Double
            Return a.TotalDegrees
        End Operator

        ''' <summary>Converts an <see cref="Angle"/> value to <see cref="Single"/></summary>
        ''' <param name="a">An angle value</param>
        ''' <returns><paramref name="a"/>.<see cref="TotalDegrees">TotalDegrees</see> as <see cref="Single"/></returns>
        ''' <exception cref="OverflowException"><paramref name="a"/>.<see cref="TotalDegrees">TotalDegrees</see> is less than <see cref="System.Single.MinValue"/> or greater than <see cref="System.Single.MaxValue"/>.</exception>
        Public Shared Narrowing Operator CType(a As Angle) As Single
            Return a.TotalDegrees
        End Operator

        ''' <summary>Converts an <see cref="Angle"/> value to <see cref="Decimal"/></summary>
        ''' <param name="a">An angle value</param>
        ''' <returns><paramref name="a"/>.<see cref="TotalDegrees">TotalDegrees</see> as <see cref="Decimal"/></returns>
        ''' <exception cref="OverflowException"><paramref name="a"/>.<see cref="TotalDegrees">TotalDegrees</see> is less than <see cref="System.Decimal.MinValue"/> or greater than <see cref="System.Decimal.MaxValue"/>.</exception>
        Public Shared Narrowing Operator CType(a As Angle) As Decimal
            Return a.TotalDegrees
        End Operator

        ''' <summary>Converts an <see cref="Angle"/> value to <see cref="Integer"/></summary>
        ''' <param name="a">An angle value</param>
        ''' <returns><paramref name="a"/>.<see cref="TotalDegrees">TotalDegrees</see> as <see cref="Integer"/></returns>
        ''' <exception cref="OverflowException"><paramref name="a"/>.<see cref="TotalDegrees">TotalDegrees</see> is less than <see cref="Int32.MinValue"/> or greater than <see cref="Int32.MaxValue"/> (after rounding).</exception>
        Public Shared Narrowing Operator CType(a As Angle) As Integer
            Return a.TotalDegrees
        End Operator

        ''' <summary>Converts an <see cref="Angle"/> value to <see cref="TimeSpan"/></summary>
        ''' <param name="a">An angle value</param>
        ''' <returns>A new instance of <see cref="TimeSpan"/> created <see cref="TimeSpan.FromHours">from hours</see> <paramref name="a"/>.<see cref="TotalDegrees">TotalDegrees</see></returns>
        ''' <exception cref="OverflowException"><paramref name="a"/>.<see cref="TotalDegrees">TotalDegrees</see> is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
        Public Shared Narrowing Operator CType(a As Angle) As TimeSpan
            Return TimeSpan.FromHours(a.TotalDegrees)
        End Operator

        ''' <summary>Converts an <see cref="Angle"/> value to <see cref="TimeSpanFormattable"/></summary>
        ''' <param name="a">An angle value</param>
        ''' <returns>A new instance of <see cref="TimeSpanFormattable"/> created <see cref="TimeSpanFormattable.FromHours">from hours</see> <paramref name="a"/>.<see cref="TotalDegrees">TotalDegrees</see></returns>
        ''' <exception cref="OverflowException"><paramref name="a"/>.<see cref="TotalDegrees">TotalDegrees</see> is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
        Public Shared Narrowing Operator CType(a As Angle) As TimeSpanFormattable
            Return TimeSpanFormattable.FromHours(a.TotalDegrees)
        End Operator
#End Region

        ''' <summary>Converts array of <see cref="URational"/>s as used in Exif to <see cref="Angle"/></summary>
        ''' <param name="a">An array that contains items degrees, minutes, second, second/60, second/60/60 etc.</param>
        ''' <returns>An angle whose value value is computed as <paramref name="a"/>[0] + <paramref name="a"/>[1]/60 + <paramref name="a"/>[2]/60/60 etc.; Zero angle if <paramref name="a"/> is null or an empty array.</returns>
        ''' <remarks>This operator is here to support conversion of Exif latitude/longitude data to <see cref="Angle"/>.</remarks>
        <CLSCompliant(False)>
        Public Shared Widening Operator CType(a As URational()) As Angle
            If a Is Nothing Then Return 0
            Dim value As Double = 0
            Dim multiplier As Double = 1.0#
            For Each v In a
                value += v / multiplier
                multiplier *= 60.0#
            Next
            Return value
        End Operator

        ''' <summary>Converts array of <see cref="SRational"/>s to <see cref="Angle"/></summary>
        ''' <param name="a">An array that contains items degrees, minutes, second, second/60, second/60/60 etc.</param>
        ''' <returns>An angle whose value value is computed as <paramref name="a"/>[0] + <paramref name="a"/>[1]/60 + <paramref name="a"/>[2]/60/60 etc.; Zero angle if <paramref name="a"/> is null or an empty array.</returns>
        ''' <remarks>This operator is CLS-compliant companion to <see cref="URational"/>[] operator.</remarks>
        Public Shared Narrowing Operator CType(a As SRational()) As Angle
            If a Is Nothing Then Throw New ArgumentNullException("a")
            Dim value As Double = 0
            Dim multiplier As Double = 1.0#
            For Each v In a
                value += v / multiplier
                multiplier *= 60.0#
            Next
            Return value
        End Operator
#End Region
#Region "Comparison"
#Region "Angle"
        ''' <summary>Compares two angle values by less-then operator</summary>
        ''' <param name="a">An angle value to be smaller than <paramref name="b"/></param>
        ''' <param name="b">An angle value to be greater than <paramref name="a"/></param>
        ''' <returns>True if normalized <paramref name="a"/> is smaller than normalized <paramref name="b"/>; false othervise.</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator <(a As Angle, b As Angle) As Boolean
            Return a.Normalize._value < b.Normalize._value
        End Operator
        ''' <summary>Compares two angle values by greater-then operator</summary>
        ''' <param name="a">An angle value to be greater than <paramref name="b"/></param>
        ''' <param name="b">An angle value to be smaller than <paramref name="a"/></param>
        ''' <returns>True if normalized <paramref name="a"/> is greater than normalized <paramref name="b"/>; false othervise.</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator >(a As Angle, b As Angle) As Boolean
            Return a.Normalize._value > b.Normalize._value
        End Operator
        ''' <summary>Tests if one angle is less than or equal to another angle</summary>
        ''' <param name="a">An angle value</param>
        ''' <param name="b">An angle value</param>
        ''' <returns>True if normalized <paramref name="a"/> is less than or equal to normalized <paramref name="b"/>; false othervise.</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator <=(a As Angle, b As Angle) As Boolean
            Return a.Normalize._value <= b.Normalize._value
        End Operator
        ''' <summary>Tests if one angle is greater than or equal to another angle</summary>
        ''' <param name="a">An angle value</param>
        ''' <param name="b">An angle value</param>
        ''' <returns>True if normalized <paramref name="a"/> is greater than or equal to normalized <paramref name="b"/>; false othervise.</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator >=(a As Angle, b As Angle) As Boolean
            Return a.Normalize._value >= b.Normalize._value
        End Operator
        ''' <summary>Tests if two angles represents the same normalized angle</summary>
        ''' <param name="a">An angle value</param>
        ''' <param name="b">An angle value</param>
        ''' <returns>True if normalized <paramref name="a"/> is equal to normalized <paramref name="b"/>; false othervise.</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator =(a As Angle, b As Angle) As Boolean
            Return a.Normalize._value = b.Normalize._value
        End Operator
        ''' <summary>Tests if two angles represents different normalized angles</summary>
        ''' <param name="a">An angle value</param>
        ''' <param name="b">An angle value</param>
        ''' <returns>True if normalized <paramref name="a"/> is differs from normalized <paramref name="b"/>; false othervise.</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator <>(a As Angle, b As Angle) As Boolean
            Return a.Normalize._value <> b.Normalize._value
        End Operator
#End Region

#Region "Double"
        ''' <summary>Test if <see cref="Angle"/> value represents an angle that is smaller than another angle given as <see cref="Double"/>.</summary>
        ''' <param name="a">An <see cref="Angle"/> value</param>
        ''' <param name="b">Angle in degrees</param>
        ''' <returns>True if <paramref name="a"/> normalized represents an angle that is smaller than <paramref name="b"/> in degrees normalized; false otherwise</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator <(a As Angle, b As Double) As Boolean
            Return a.Normalize._value < CType(b, Angle).Normalize._value
        End Operator
        ''' <summary>Test if <see cref="Angle"/> value represents an angle that is greater than another angle given as <see cref="Double"/>.</summary>
        ''' <param name="a">An <see cref="Angle"/> value</param>
        ''' <param name="b">Angle in degrees</param>
        ''' <returns>True if <paramref name="a"/> normalized represents an angle that is greater than <paramref name="b"/> in degrees normalized; false otherwise</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator >(a As Angle, b As Double) As Boolean
            Return a.Normalize._value > CType(b, Angle).Normalize._value
        End Operator
        ''' <summary>Test if <see cref="Angle"/> value represents an angle that is smaller than or equal to another angle given as <see cref="Double"/>.</summary>
        ''' <param name="a">An <see cref="Angle"/> value</param>
        ''' <param name="b">Angle in degrees</param>
        ''' <returns>True if <paramref name="a"/> normalized represents an angle that is smaller than or equal to <paramref name="b"/> in degrees normalized; false otherwise</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator <=(a As Angle, b As Double) As Boolean
            Return a.Normalize._value <= CType(b, Angle).Normalize._value
        End Operator
        ''' <summary>Test if <see cref="Angle"/> value represents an angle that is greater than or equal to another angle given as <see cref="Double"/>.</summary>
        ''' <param name="a">An <see cref="Angle"/> value</param>
        ''' <param name="b">Angle in degrees</param>
        ''' <returns>True if <paramref name="a"/> normalized represents an angle that is greater than or equal to <paramref name="b"/> in degrees normalized; false otherwise</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator >=(a As Angle, b As Double) As Boolean
            Return a.Normalize._value >= CType(b, Angle).Normalize._value
        End Operator
        ''' <summary>Test if <see cref="Angle"/> and <see cref="Double"/> represent same normalized angle.</summary>
        ''' <param name="a">An <see cref="Angle"/> value</param>
        ''' <param name="b">Angle in degrees</param>
        ''' <returns>True if <paramref name="a"/> normalized represents the same angle as <paramref name="b"/> in degrees normalized; false otherwise</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator =(a As Angle, b As Double) As Boolean
            Return a.Normalize._value = CType(b, Angle).Normalize._value
        End Operator
        ''' <summary>Test if <see cref="Angle"/> and <see cref="Double"/> represent different normalized angles.</summary>
        ''' <param name="a">An <see cref="Angle"/> value</param>
        ''' <param name="b">Angle in degrees</param>
        ''' <returns>True if <paramref name="a"/> normalized represents different angle than <paramref name="b"/> in degrees normalized; false otherwise</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator <>(a As Angle, b As Double) As Boolean
            Return a.Normalize._value <> CType(b, Angle).Normalize._value
        End Operator
#End Region

#Region "Integer"
        ''' <summary>Test if <see cref="Angle"/> value represents an angle that is smaller than another angle given as <see cref="Integer"/>.</summary>
        ''' <param name="a">An <see cref="Angle"/> value</param>
        ''' <param name="b">Angle in degrees</param>
        ''' <returns>True if <paramref name="a"/> normalized represents an angle that is smaller than <paramref name="b"/> in degrees normalized; false otherwise</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator <(a As Angle, b As Integer) As Boolean
            Return a.Normalize._value < CType(b, Angle).Normalize._value
        End Operator
        ''' <summary>Test if <see cref="Angle"/> value represents an angle that is greater than another angle given as <see cref="Integer"/>.</summary>
        ''' <param name="a">An <see cref="Angle"/> value</param>
        ''' <param name="b">Angle in degrees</param>
        ''' <returns>True if <paramref name="a"/> normalized represents an angle that is greater than <paramref name="b"/> in degrees normalized; false otherwise</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator >(a As Angle, b As Integer) As Boolean
            Return a.Normalize._value > CType(b, Angle).Normalize._value
        End Operator
        ''' <summary>Test if <see cref="Angle"/> value represents an angle that is smaller than or equal to another angle given as <see cref="Integer"/>.</summary>
        ''' <param name="a">An <see cref="Angle"/> value</param>
        ''' <param name="b">Angle in degrees</param>
        ''' <returns>True if <paramref name="a"/> normalized represents an angle that is smaller than or equal to <paramref name="b"/> in degrees normalized; false otherwise</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator <=(a As Angle, b As Integer) As Boolean
            Return a.Normalize._value <= CType(b, Angle).Normalize._value
        End Operator
        ''' <summary>Test if <see cref="Angle"/> value represents an angle that is greater than or equal to another angle given as <see cref="Integer"/>.</summary>
        ''' <param name="a">An <see cref="Angle"/> value</param>
        ''' <param name="b">Angle in degrees</param>
        ''' <returns>True if <paramref name="a"/> normalized represents an angle that is greater than or equal to <paramref name="b"/> in degrees normalized; false otherwise</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator >=(a As Angle, b As Integer) As Boolean
            Return a.Normalize._value >= CType(b, Angle).Normalize._value
        End Operator
        ''' <summary>Test if <see cref="Angle"/> and <see cref="Integer"/> represent same normalized angle.</summary>
        ''' <param name="a">An <see cref="Angle"/> value</param>
        ''' <param name="b">Angle in degrees</param>
        ''' <returns>True if <paramref name="a"/> normalized represents the same angle as <paramref name="b"/> in degrees normalized; false otherwise</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator =(a As Angle, b As Integer) As Boolean
            Return a.Normalize._value = CType(b, Angle).Normalize._value
        End Operator
        ''' <summary>Test if <see cref="Angle"/> and <see cref="Integer"/> represent different normalized angles.</summary>
        ''' <param name="a">An <see cref="Angle"/> value</param>
        ''' <param name="b">Angle in degrees</param>
        ''' <returns>True if <paramref name="a"/> normalized represents different angle than <paramref name="b"/> in degrees normalized; false otherwise</returns>
        ''' <remarks>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</remarks>
        Public Shared Operator <>(a As Angle, b As Integer) As Boolean
            Return a.Normalize._value <> CType(b, Angle).Normalize._value
        End Operator
#End Region
#End Region
#Region "Arithmetic"
        ''' <summary>Multiplies an <see cref="Angle"/> with given <see cref="Double"/> number</summary>
        ''' <param name="a">An <see cref="Angle"/> to be multiplied</param>
        ''' <param name="b">Value to multiply angle with</param>
        ''' <returns>An angle that is <paramref name="b"/>-times greater than <paramref name="a"/>.</returns>
        ''' <exception cref="ArgumentException"><paramref name="b"/> is NaN or infinity</exception>
        ''' <exception cref="OverflowAction">Resulting angle is less than <see cref="System.Double.MinValue"/> or greater than <see cref="System.Double.MaxValue"/></exception>
        ''' <remarks>Resulting value is not normalized.</remarks>
        Public Shared Operator *(a As Angle, b As Double) As Angle
            Return New Angle(a._value * b)
        End Operator

        ''' <summary>Multiplies an <see cref="Angle"/> with given <see cref="Single"/> number</summary>
        ''' <param name="a">An <see cref="Angle"/> to be multiplied</param>
        ''' <param name="b">Value to multiply angle with</param>
        ''' <returns>An angle that is <paramref name="b"/>-times greater than <paramref name="a"/>.</returns>
        ''' <exception cref="ArgumentException"><paramref name="b"/> is NaN or infinity</exception>
        ''' <exception cref="OverflowAction">Resulting angle is less than <see cref="System.Double.MinValue"/> or greater than <see cref="System.Double.MaxValue"/></exception>
        ''' <remarks>Resulting value is not normalized.</remarks>
        Public Shared Operator *(a As Angle, b As Single) As Angle
            Return New Angle(a._value * CDbl(b))
        End Operator
        ''' <summary>Multiplies an <see cref="Angle"/> with given <see cref="Integer"/> number</summary>
        ''' <param name="a">An <see cref="Angle"/> to be multiplied</param>
        ''' <param name="b">Value to multiply angle with</param>
        ''' <returns>An angle that is <paramref name="b"/>-times greater than <paramref name="a"/>.</returns>
        ''' <exception cref="ArgumentException"><paramref name="b"/> is NaN or infinity</exception>
        ''' <exception cref="OverflowAction">Resulting angle is less than <see cref="System.Double.MinValue"/> or greater than <see cref="System.Double.MaxValue"/></exception>
        ''' <remarks>Resulting value is not normalized.</remarks>
        Public Shared Operator *(a As Angle, b As Integer) As Angle
            Return New Angle(a._value * CInt(b))
        End Operator

        ''' <summary>Divides an <see cref="Angle"/> by given <see cref="Double"/> number</summary>
        ''' <param name="a">An <see cref="Angle"/> to be divided</param>
        ''' <param name="b">A number to dividy angle by</param>
        ''' <returns>An angle that is <paramref name="b"/>-times smaller than <paramref name="a"/>.</returns>
        ''' <exception cref="ArgumentException"><paramref name="b"/> is NaN</exception>
        ''' <exception cref="OverflowAction">Resulting angle is less than <see cref="System.Double.MinValue"/> or greater than <see cref="System.Double.MaxValue"/></exception>
        ''' <exception cref="DivideByZeroException"><paramref name="b"/> is 0</exception>
        ''' <remarks>Resulting value is not normalized.</remarks>
        Public Shared Operator /(a As Angle, b As Double) As Angle
            If b = 0.0# Then Throw New DivideByZeroException()
            Return New Angle(a._value / b)
        End Operator
        ''' <summary>Divides an <see cref="Angle"/> by given <see cref="Single"/> number</summary>
        ''' <param name="a">An <see cref="Angle"/> to be divided</param>
        ''' <param name="b">A number to dividy angle by</param>
        ''' <returns>An angle that is <paramref name="b"/>-times smaller than <paramref name="a"/>.</returns>
        ''' <exception cref="ArgumentException"><paramref name="b"/> is NaN</exception>
        ''' <exception cref="OverflowAction">Resulting angle is less than <see cref="System.Double.MinValue"/> or greater than <see cref="System.Double.MaxValue"/></exception>
        ''' <exception cref="DivideByZeroException"><paramref name="b"/> is 0</exception>
        ''' <remarks>Resulting value is not normalized.</remarks>
        Public Shared Operator /(a As Angle, b As Single) As Angle
            If b = 0.0! Then Throw New DivideByZeroException()
            Return New Angle(a._value / CDbl(b))
        End Operator
        ''' <summary>Divides an <see cref="Angle"/> by given <see cref="Integer"/> number</summary>
        ''' <param name="a">An <see cref="Angle"/> to be divided</param>
        ''' <param name="b">A number to dividy angle by</param>
        ''' <returns>An angle that is <paramref name="b"/>-times smaller than <paramref name="a"/>.</returns>
        ''' <exception cref="ArgumentException"><paramref name="b"/> is NaN</exception>
        ''' <exception cref="OverflowAction">Resulting angle is less than <see cref="System.Double.MinValue"/> or greater than <see cref="System.Double.MaxValue"/></exception>
        ''' <exception cref="DivideByZeroException"><paramref name="b"/> is 0</exception>
        ''' <remarks>Resulting value is not normalized.</remarks>
        Public Shared Operator /(a As Angle, b As Integer) As Angle
            If b = 0 Then Throw New DivideByZeroException
            Return New Angle(a._value / CDbl(b))
        End Operator

        ''' <summary>Divides a number of rotations by integer</summary>
        ''' <param name="a">An angle whichs number of rotations to divide</param>
        ''' <param name="b">An integral number to divide number of rotations by</param>
        ''' <returns><paramref name="a"/>.<see cref="Rotations">Rotations</see> integraly divided by <paramref name="b"/></returns>
        ''' <remarks>To get whole number of rotations use aither <c><see cref="Math.Floor"/>(<see cref="Rotations"/>)</c> or <c>x \ 1</c> (VB only).</remarks>
        Public Shared Operator \(a As Angle, b As Integer) As Integer
            Return Math.Floor(a.Rotations) \ b
        End Operator

        ''' <summary>Makes an angle negative</summary>
        ''' <param name="a">An angle to be made negative</param>
        ''' <remarks>Angle whose value is negative of <paramref name="a"/></remarks>
        Public Shared Operator -(a As Angle) As Angle
            Return New Angle(-a._value)
        End Operator
        ''' <summary>Implements unary plus operator</summary>
        ''' <param name="a">An angle</param>
        ''' <returns><paramref name="a"/> unchanged</returns>
        Public Shared Operator +(a As Angle) As Angle
            Return a
        End Operator

        ''' <summary>Adds sizes of two angles</summary>
        ''' <param name="a">An angle</param>
        ''' <param name="b">An angle</param>
        ''' <returns>An angle that is sum of sizes of two angles.</returns>
        ''' <exception cref="OverflowException">Resulting angle is smaller than <see cref="System.Double.MinValue"/> or greater than <see cref="[Double].MaxValue"/>.</exception>
        ''' <remarks>No normalization is done</remarks>
        Public Shared Operator +(a As Angle, b As Angle) As Angle
            Return New Angle(a._value + b._value)
        End Operator

        ''' <summary>Subtracts sizes of two angles</summary>
        ''' <param name="a">An angle to subtract from</param>
        ''' <param name="b">An angle be subtracted</param>
        ''' <returns>An angle that is difference between the two angles. <paramref name="b"/> subtracted form <paramref name="a"/></returns>
        ''' <exception cref="OverflowException">Resulting angle is smaller than <see cref="System.Double.MinValue"/> or greater than <see cref="[Double].MaxValue"/>.</exception>
        ''' <remarks>No normalization is done</remarks>
        Public Shared Operator -(a As Angle, b As Angle) As Angle
            Return New Angle(a._value - b._value)
        End Operator
#End Region
#End Region

#Region "Override"
        ''' <summary>Returns the hash code for this instance.</summary>
        ''' <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        ''' <remarks>Hash code is computed from 360-normalized value</remarks>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function GetHashCode() As Integer
            Return Normalize._value.GetHashCode
        End Function
        ''' <summary>Indicates whether this instance and a specified object are equal.</summary>
        ''' <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
        ''' <param name="obj">Another object to compare to. </param>
        ''' <remarks>
        ''' <see cref="Angle"/> and <paramref name="obj"/> are considered equal if:
        ''' <list type="list">
        ''' <item><paramref name="obj"/> is <see cref="Angle"/> of same 360-normalized value as sthi instance</item>
        ''' <item><paramref name="obj"/> is numeric value (all standard .NET numeric types are supported including <see cref="Decimal"/> and <see cref="Numerics.BigInteger"/>; <see cref="Char"/> is not supported)</item>
        ''' <item><paramref name="obj"/> is <see cref="TimeSpan"/> or <see cref="TimeSpanFormattable"/> with same <see cref="TimeSpan.TotalHours"/> as <see cref="TotalDegrees"/>.</item>
        ''' </list>
        ''' <note>All comaprisons on <see cref="Angle"/> are done after value is normalized to range &lt;0°, 360°> using <see cref="Normalize"/>. Then <see cref="TotalDegrees"/> values are compared.</note>
        ''' </remarks>
        ''' <filterpriority>2</filterpriority>
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
            If TypeOf obj Is Numerics.BigInteger Then Return CType(Me._value, Numerics.BigInteger) = DirectCast(obj, Numerics.BigInteger)
            If TypeOf obj Is TimeSpan Then Return Me = CType(DirectCast(obj, TimeSpan), Angle)
            If TypeOf obj Is TimeSpanFormattable Then Return Me = CType(DirectCast(obj, TimeSpanFormattable), Angle)
            Return False
        End Function
        ''' <summary>Compares the current object with another object of the same type.</summary>
        ''' <param name="other">An object to compare with this object.</param>
        ''' <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the other parameter.Zero This object is equal to other. Greater than zero This object is greater than other.</returns>
        Public Function CompareTo(other As Angle) As Integer Implements System.IComparable(Of Angle).CompareTo
            Return Comparer(Of Double).Default.Compare(Me.Normalize.TotalDegrees, other.Normalize.TotalDegrees)
        End Function
#End Region

#Region "ToString"
        ''' <summary>Returns the fully qualified type name of this instance.</summary>
        ''' <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overloads Overrides Function ToString() As String
            Return ToString(Nothing, Nothing)
        End Function
        ''' <summary>A regular expression to detect standard angle format</summary>
        Private Shared shortFormatRegex As New Regex("$[A-Za-Z][0-9]*^", RegexOptions.Compiled Or RegexOptions.CultureInvariant)

        ''' <summary>Formats the value of the current instance using the specified format.</summary>
        ''' <param name="format">The format to use. Null or an empty string means default format (g). This can be either standard or custom format string.</param>
        ''' <param name="formatProvider">The provider to use to format the value.-or- A null reference (Nothing in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system.</param>
        ''' <returns>The value of the current instance in the specified format.</returns>
        ''' <exception cref="FormatException"><paramref name="format"/> is invalid</exception>
        ''' <remarks>
        ''' This method supports variety of formating specifier, either standard or custom.
        ''' <para>Standard format specifiers are:</para>
        ''' <para>Standard format specifier consist of single letter optionally followed by one or more numerals (parameter).</para>
        ''' <list type="table">
        ''' <listheader><term>Standard format specifier</term><description>Meaning              </description><description>Example       </description><description>Parameter meaning                                </description><description>When no parameter is specified    </description><description>Appropriate custom format                                                                                                                                                                                                                                                                                                                                                                                                                      </description></listheader>
        ''' <item>      <term>G, g, l, L               </term><description>General/long         </description><description>-14°00′05.33″ </description><description>Number of max decimal places for sub-second value</description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>-d°mm'(RestSeconds,R)"     </description></item><item><term>0    </term><description>-d°mm'ss"                                   </description></item><item><term>other</term><description>-d°mm'ss.ffff" (as many fs as is value of parameter)       </description></item></list></description></item>
        ''' <item>      <term>a, φ                     </term><description>GPS latitude short   </description><description>14°00′05″ N   </description><description>Parameter is not allowed.                        </description><description>                                  </description><description>N180d°mm'ss" a                                                                                                                                                                                                                                                                                                                                                                                                                                 </description></item>
        ''' <item>      <term>A, Φ                     </term><description>GPS latitude long    </description><description>14°00′05.33″ N</description><description>Number of max decimal places for sub-second value</description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>N180d°mm'(RestSeconds,R)" a</description></item><item><term>0    </term><description>N180d°mm'ss" a                              </description></item><item><term>other</term><description>N180-d°mm'ss.ffff" a (as many fs as is value of parameter) </description></item></list></description></item>
        ''' <item>      <term>b                        </term><description>GPS latitude decimal </description><description>14.22 N       </description><description>Number of decimal places                         </description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>N180(TotalDegrees,R) a     </description></item><item><term>other</term><description>N180Dx a (where x is actual parameter value)</description></item>                                                                                                                     </list></description></item>
        ''' <item>      <term>d                        </term><description>Time with days, short</description><description>1.1:03:09     </description><description>Parameter is not allowed.                        </description><description>                                  </description><description>-[y.]h:mm:ss                                                                                                                                                                                                                                                                                                                                                                                                                                   </description></item>
        ''' <item>      <term>D                        </term><description>Time with days, long </description><description>1.1:03:09.141 </description><description>Number of max decimal places for sub-second value</description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>-[y.]h:mm:(RestSeconds,R)  </description></item><item><term>0    </term><description>-[y.]h:mm:ss                                </description></item><item><term>other</term><description>-[y.]h:mm:ss.ffff" (as many fs as is value of parameter)   </description></item></list></description></item>
        ''' <item>      <term>e                        </term><description>F normalized         </description><description>-14.975       </description><description>Number of decimal places                         </description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>N(TotalDegrees,R)          </description></item><item><term>other</term><description>NDx (where x is actual parameter value)     </description></item>                                                                                                                     </list></description></item>
        ''' <item>      <term>E                        </term><description>Rotations            </description><description>0.125         </description><description>Number of decimal places                         </description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>-E                         </description></item><item><term>other</term><description>-Ex (where x is actual parameter value)     </description></item>                                                                                                                     </list></description></item>
        ''' <item>      <term>F, f                     </term><description>Decimal              </description><description>-14.975       </description><description>Number of decimal places                         </description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>-(TotalDegrees,R)          </description></item><item><term>other</term><description>-Dx (where x is actual parameter value)     </description></item>                                                                                                                     </list></description></item>
        ''' <item>      <term>n                        </term><description>S normalized         </description><description>-14°09′05″    </description><description>Parameter is not allowed.                        </description><description>                                  </description><description>N-d°mm'ss"                                                                                                                                                                                                                                                                                                                                                                                                                                     </description></item>
        ''' <item>      <term>N                        </term><description>G normalized         </description><description>-14°00′05.33″ </description><description>Number of max decimal places for sub-second value</description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>Nd°mm'(RestSeconds,R)"     </description></item><item><term>0    </term><description>Nd°mm'ss"                                   </description></item><item><term>other</term><description>Nd°mm'ss.ffff" (as many fs as is value of parameter)       </description></item></list></description></item>
        ''' <item>      <term>O, Λ                     </term><description>GPS longitude long   </description><description>14°00′05.33″ E</description><description>Number of max decimal places for sub-second value</description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>N180d°mm'(RestSeconds,R)" o</description></item><item><term>0    </term><description>N180d°mm'ss" o                              </description></item><item><term>other</term><description>N180-d°mm'ss.ffff" o (as many fs as is value of parameter) </description></item></list></description></item>
        ''' <item>      <term>o, λ                     </term><description>GPS longitude short  </description><description>14°00′05″ E   </description><description>Parameter is not allowed.                        </description><description>                                  </description><description>N180d°mm'ss" o                                                                                                                                                                                                                                                                                                                                                                                                                                 </description></item>
        ''' <item>      <term>p                        </term><description>GPS longitude decimal</description><description>14.22 E       </description><description>Number of decimal places                         </description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>N180(TotalDegrees,R) o     </description></item><item><term>other</term><description>N180Dx o (where x is actual parameter value)</description></item>                                                                                                                     </list></description></item>
        ''' <item>      <term>R                        </term><description>Radians              </description><description>6.21465       </description><description>Number of decimal places                         </description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>-R                         </description></item><item><term>other</term><description>-Rx (where x is actual parameter value)     </description></item>                                                                                                                     </list></description></item>
        ''' <item>      <term>S, s                     </term><description>Short                </description><description>-14°09′05″    </description><description>Parameter is not allowed.                        </description><description>                                  </description><description>-d°mm'ss"                                                                                                                                                                                                                                                                                                                                                                                                                                      </description></item>
        ''' <item>      <term>t                        </term><description>Time, short, no days </description><description>25:03:09      </description><description>Parameter is not allowed.                        </description><description>                                  </description><description>-d:mm:ss                                                                                                                                                                                                                                                                                                                                                                                                                                       </description></item>
        ''' <item>      <term>T                        </term><description>Time, long, no days  </description><description>25:03:09.141  </description><description>Number of max decimal places for sub-second value</description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>-d:mm:(RestSeconds,R)      </description></item><item><term>0    </term><description>-d:mm:ss                                    </description></item><item><term>other</term><description>-d:mm:ss.ffff" (as many fs as is value of parameter)       </description></item></list></description></item>
        ''' <item>      <term>Z                        </term><description>Grads                </description><description>122.15        </description><description>Number of decimal places                         </description><description>as much decimal places as required</description><description><list type="table"><listheader><term>Parameter value</term><description>Custom format</description></listheader><item><term>not specified</term><description>-Z                         </description></item><item><term>other</term><description>-Zx (where x is actual parameter value)     </description></item>                                                                                                                     </list></description></item>
        ''' </list>
        ''' <para>Notes:</para>
        ''' <list type="table">
        ''' <item><term>normalized</term><description>Value normalized to max value 360° using <see cref="Normalize"/> is passed to formatting.</description></item>
        ''' </list>
        ''' <para>Custom format specifiers are:</para>
        ''' <para>Some of the custom format specifiers can be repeated, some accept parameters.</para>
        ''' <para>Specifiers that produce value:</para>
        ''' <list type="table">
        ''' <listheader><term>Specifier</term><description>Meaning                                 </description><description>Repeatable (and what does it mean)                    </description><description>Parameter                                                     </description><description>Notes                                                                                                                                                                                             </description></listheader>
        ''' <item>      <term>D        </term><description><see cref="TotalDegrees"/>              </description><description>Yes - minimum number of digits left from decimal point</description><description>Number - number of decimal places (as required if not present)</description><description>If you want to exclude days use H instead.                                                                                                                                                        </description></item>
        ''' <item>      <term>d        </term><description><see cref="Degrees"/>                   </description><description>Yes - minimum number of digits                        </description><description>no                                                            </description><description>If you want to exclude days use h instead.                                                                                                                                                        </description></item>
        ''' <item>      <term>M        </term><description><see cref="TotalMinutes"/>              </description><description>Yes - minimum number of digits left from decimal point</description><description>Number - number of decimal places (as required if not present)</description><description>Excludes degrees if D, d, H or h was presenf before.                                                                                                                                              </description></item>
        ''' <item>      <term>m        </term><description><see cref="Minutes"/>                   </description><description>Yes - minimum number of digits                        </description><description>no                                                            </description><description>                                                                                                                                                                                                  </description></item>
        ''' <item>      <term>S        </term><description><see cref="TotalSeconds"/>              </description><description>Yes - minimum number of digits left from decimal point</description><description>Number - number of decimal places (as required if not present)</description><description>If M or m was present before <see cref="Seconds"/> is used instead. If D, d, H or h was present before (and neither m or M was present) <see cref="TotalSeconds"/> value does not contain degrees.</description></item>
        ''' <item>      <term>s        </term><description><see cref="Seconds"/> - integral part   </description><description>Yes - minimum number of digits                        </description><description>no                                                            </description><description>                                                                                                                                                                                                  </description></item>
        ''' <item>      <term>f        </term><description>Fractional part of <see cref="Seconds"/></description><description>Yes - minimum number of digits                        </description><description>Number - maximum number of digits (as required if not present)</description><description>Contains part of <see cref="Seconds"/> right from decimnal point (&lt;1). Does not contain decimal point or any numerals before it. Use dot (.) to include decimal point.                         </description></item>
        ''' <item>      <term>Y        </term><description>Total days                              </description><description>Yes - minimum number of digits left from decimal point</description><description>Number - number of decimal places (as required if not present)</description><description>Value of <see cref="TotalDegrees"/> / 24 (<see cref="Double"/>)                                                                                                                                   </description></item>
        ''' <item>      <term>y        </term><description>Days (whole)                            </description><description>Yes - minimum number of digits                        </description><description>no                                                            </description><description>Value of <see cref="TotalDegrees"/> / 24, decimal part truncated (<see cref="Integer"/>)                                                                                                          </description></item>
        ''' <item>      <term>H        </term><description><see cref="TotalDegrees"/> (total hours)</description><description>Yes - minimum number of digits left from decimal point</description><description>Number - number of decimal places (as required if not present)</description><description>Excludes days if Y or y was present before.                                                                                                                                                       </description></item>
        ''' <item>      <term>h        </term><description>Degrees (hours) without days (whole)    </description><description>Yes - minimum number of digits                        </description><description>no                                                            </description><description>Always excludes days                                                                                                                                                                              </description></item>
        ''' <item>      <term>E        </term><description><see cref="Rotations"/>                 </description><description>Yes - minimum number of digits left from decimal point</description><description>Number - number of decimal places (max 4 if not present)      </description><description>                                                                                                                                                                                                  </description></item>
        ''' <item>      <term>R        </term><description><see cref="ToRadians"/>                 </description><description>Yes - minimum number of digits left from decimal point</description><description>Number - number of decimal places (max 4 if not present)      </description><description>                                                                                                                                                                                                  </description></item>
        ''' <item>      <term>Z        </term><description><see cref="ToGradians"/>                </description><description>Yes - minimum number of digits left from decimal point</description><description>Number - number of decimal places (max 4 if not present)      </description><description>                                                                                                                                                                                                  </description></item>
        ''' <item>      <term>π, p     </term><description>π-radians (<see cref="ToRadians"/> / π )</description><description>Yes - minimum number of digits left from decimal point</description><description>Number - number of decimal places (max 4 if not present)      </description><description>Value of <see cref="ToRadians"/> (angle in radians) divided by <see cref="Math.PI"/> - because radian values are often given as mupltiples of π (pi)                                              </description></item>
        ''' </list>
        ''' <para>Specifiers that produces symbols:</para>
        ''' <list type="table">
        ''' <listheader><term>Specifier</term><description>Meaning</description></listheader>
        ''' <item><term>A, Φ</term><description>Long latitude (north/south) specifier (e.g. North; <see cref="GlobalizationT.AngleFormatInfo.LatitudeNorthLongSymbol"/> or <see cref="GlobalizationT.AngleFormatInfo.LatitudeSouthLongSymbol"/>)</description></item>
        ''' <item><term>O, Λ</term><description>Long longitude (west/east) specifier (e.g. East; <see cref="GlobalizationT.AngleFormatInfo.LongitudeEastLongSymbol"/> or <see cref="GlobalizationT.AngleFormatInfo.LongitudeWestLongSymbol"/>)</description></item>
        ''' <item><term>a, φ</term><description>Short latitude (north/south) specifier (e.g. N; <see cref="GlobalizationT.AngleFormatInfo.LatitudeNorthShortSymbol"/> or <see cref="GlobalizationT.AngleFormatInfo.LatitudeSouthShortSymbol"/>)</description></item>
        ''' <item><term>o, λ</term><description>Short longitude (west/east) specifier (e.g. E; <see cref="GlobalizationT.AngleFormatInfo.LongitudeEastShortSymbol"/> or <see cref="GlobalizationT.AngleFormatInfo.LongitudeWestShortSymbol"/>)</description></item>
        ''' <item><term>-</term><description>Optinal sign. In case of negative value emits minus sing (e.g. -; <see cref="NumberFormatInfo.NegativeSign"/>). In case of positive or zero value emits nothing.</description></item>
        ''' <item><term>+</term><description>Compulsory sign. In case of negative value emits minus sign (e.g. -; <see cref="NumberFormatInfo.NegativeSign"/>), in case of positive or zero value emits plus sign (e.g. +; <see cref="NumberFormatInfo.PositiveSign"/>).</description></item>
        ''' <item><term>.</term><description>Decimal point (e.g. .; <see cref="NumberFormatInfo.NumberDecimalSeparator"/>)</description></item>
        ''' <item><term>,</term><description>Thousand separator - in normal context emits thousnd separator (e.g. ,; <see cref="NumberFormatInfo.NumberGroupSeparator"/>). If used immediatelly after specifier that supports value; between repeateed letters of specifier that produces value; or between numbers in parameters of specifier that produces value and supports parameter indicates that thousands (group) separators will be used in renderd number (if required). I.e. all following examples specifie to use thousands specifier for total degrees: D,; D,D; D,D,D; D,DD; DD,D; D,0; D,5; D5,; D0,3; D,1,0,.</description></item>
        ''' <item><term>°</term><description>Degree sign (e.g. °; <see cref="GlobalizationT.AngleFormatInfo.DegreeSign"/>)</description></item>
        ''' <item><term>', ′</term><description>Minute sign (e.g. ′; <see cref="GlobalizationT.AngleFormatInfo.MinuteSign"/>)</description></item>
        ''' <item><term>", ″</term><description>Second sign (e.g. ″; <see cref="GlobalizationT.AngleFormatInfo.SecondSign"/>)</description></item>
        ''' <item><term>\</term><description>Escape - any character that immediatelly follows \ is passed to output without being processed.<note>Be carefull in C#, C++/CLI and other languages that use backslash (\) as their escape charatcer. You have to include \\ in format string (or in C# use verbatim strings - i.e. @"").</note></description></item>
        ''' </list>
        ''' <para>Specifiers that affect output:</para>
        ''' <para>These specifiers do not produce any output but affect how output that follows tham is processed</para>
        ''' <list type="table">
        ''' <listheader><term>Specifier</term><description>Meaning</description></listheader>
        ''' <item><term>N</term><description>Causes value of angle to be normalized. Can be followed by a number - parameter for <see cref="Normalize"/>. If parameter is not specified 360 is used. Negative values for parameter are supported. If used more than once in single format string always the original angle is normalized (i.e. not the normalized one). This specifier (if used) is usually placed at the begining of format string. Examples: N; N360; N-45<note>N- alone is not valid normalization specifier and instead means: Normalize to 360, emit optional minus sign.</note></description></item>
        ''' <item><term>%</term><description>Only at beginnign of format string. Causes format string that would otherwise be treated as standard format string to be treated as custom format string. The % charatcer itself is ignored. To include perecent as first charatcer of your string use %% or \%. <note>If percent is used anywhere else but as first charatcer it is passed to output normally.</note></description></item>
        ''' <item><term>c</term><description>Turn compatibility rendering on. Causes that all °, ' (′) and " (″) placeholders following this specifier will use compatibility rendering instead of typographicaly correct rendering. I.e. <see cref="GlobalizationT.AngleFormatInfo.CompatibilityDegreeSign"/>, <see cref="GlobalizationT.AngleFormatInfo.CompatibilityMinuteSign"/> and <see cref="GlobalizationT.AngleFormatInfo.CompatibilitySecondSign"/> will be used instead of <see cref="GlobalizationT.AngleFormatInfo.DegreeSign"/>, <see cref="GlobalizationT.AngleFormatInfo.MinuteSign"/> resp. <see cref="GlobalizationT.AngleFormatInfo.SecondSign"/>.<note>In many cultures including invariant <see cref="GlobalizationT.AngleFormatInfo.DegreeSign"/> and <see cref="GlobalizationT.AngleFormatInfo.CompatibilityDegreeSign"/> are same).</note>Compatibility rendering cannot be turned off once turned on /in same format string). This specififer (if used) is usually placed at begining of format string.</description></item>
        ''' </list>
        ''' <para>Special specifiers:</para>
        ''' <list type="table">
        ''' <listheader><term>Specifier</term><description>Meaning</description><description>Syntax</description><description>Description</description></listheader>
        ''' <item><term>[]</term><description>Optional part</description><description>[any valid format string]</description><description>Anything neclodes in brackets ([]) is processed as normal format string and it is evaluated if any expression in brackets is non-zero. If it is content of bracketed group is included in result. If value in brackets is zero the group is thrown away and nothing from that group is included in results.<note>If a specifier c or N is included in brackers it is not scoped to inside of brackets. I.e. it still has effect after closing ].</note></description></item>
        ''' <item>
        ''' <term>()</term>
        ''' <description>Custom formatting of a property</description>
        ''' <description>([SF]PropertyName,custom format)</description>
        ''' <description>
        ''' In case you want to format one of predefined properties (not necessarily only properties of the <see cref="Angle"/> class - few more values are available) you can do it in this way.
        ''' Each custom property is included in braces (()). There are two parts in the braces - property name and custom format separated by comma (,).
        ''' Property name can be a string consisting of ASCII letters, digits and underscores (_). It can be followed by whitespaces that are ignored.
        ''' Property name is generally case-insensitive with exception of optional SF prefic. If the case-sensitive SF prefix is included at the begining of property name formatting string is passed to <see cref="System.String.Format"/> and it is expected to contain placeholder {0} to be replaced with formatted property value. Otherwise formatting string is passed to <see cref="IFormattable.ToString"/> (all properties return <see cref="IFormattable"/>).
        ''' Formatting string is a raw formatting string that will be passed to <see cref="IFormattable.ToString"/> or <see cref="System.String.Format"/> for property value. It only cannot include closing brace ()). If you want to include closing brace escape it \). Any other character preceded with \ (including \ itself) is passed to formatting string and (first) backslash itself is sttripped.
        ''' In terms of regular expressions custom property specifier can be described as <c><![CDATA[\((?<SF>SF)(?<Name>[A-Za-z0-9_]*)\s*,(?<Format>([^)]|(\\\)))*)\)]]></c>.
        ''' <note><see cref="ToString"/> internally does not use given regular expression. It uses it's own processing based on final state automaton.</note>
        ''' <note>Not all syntactically valid property names are valid property names. Only a few properties are supported.</note>
        ''' <note>There is a whitespace allowed between property name and comma (,) delimiting property name and format. However there is no whitespace allowed after the comma. All character after the comma (including whitespace charatcer) become part of formatting string.</note>
        ''' <para>Supported properties are:</para>
        ''' <list type="table">
        ''' <listheader><term>Property</term><description>Type</description><description>Description</description></listheader>
        ''' <item><term>Degrees</term><description><see cref="Integer"/></description><description><see cref="Degrees"/></description></item>
        ''' <item><term>Minutes</term><description><see cref="Integer"/></description><description><see cref="Minutes"/></description></item>
        ''' <item><term>Seconds</term><description><see cref="Double"/></description><description><see cref="Seconds"/></description></item>
        ''' <item><term>TotalDegrees, TotalHours</term><description><see cref="Double"/></description><description><see cref="TotalDegrees"/></description></item>
        ''' <item><term>TotalMinutes</term><description><see cref="Double"/></description><description><see cref="TotalMinutes"/></description></item>
        ''' <item><term>TotalSeconds</term><description><see cref="Double"/></description><description><see cref="TotalSeconds"/></description></item>
        ''' <item><term>Rotations</term><description><see cref="Double"/></description><description><see cref="Rotations"/></description></item>
        ''' <item><term>Radians</term><description><see cref="Double"/></description><description><see cref="ToRadians"/></description></item>
        ''' <item><term>Gradians</term><description><see cref="Double"/></description><description><see cref="ToGradians"/></description></item>
        ''' <item><term>PiRadians</term><description><see cref="Double"/></description><description><see cref="ToRadians"/> / <see cref="Math.PI"/></description></item>
        ''' <item><term>Days</term><description><see cref="Integer"/></description><description>Whole part of <see cref="TotalDegrees"/> / 24</description></item>
        ''' <item><term>TotalDays</term><description><see cref="Double"/></description><description><see cref="TotalDegrees"/> / 24</description></item>
        ''' <item><term>RestMinutes</term><description><see cref="Double"/></description><description><see cref="TotalMinutes"/> excluding degrees</description></item>
        ''' <item><term>RestSeconds</term><description><see cref="Double"/></description><description><see cref="TotalSeconds"/> excluding minutes and degrees</description></item>
        ''' <item><term>Hours</term><description><see cref="Integer"/></description><description><see cref="Degrees"/> excluding days</description></item>
        ''' <item><term>RestHours</term><description><see cref="Double"/></description><description><see cref="TotalDegrees"/> excluding days</description></item>
        ''' <item><term>Time</term><description><see cref="TimeSpan"/></description><description>Value as <see cref="TimeSpan"/></description></item>
        ''' <item><term>TimeFormattable</term><description><see cref="TimeSpanFormattable"/></description><description>Value as <see cref="TimeSpanFormattable"/></description></item>
        ''' </list>
        ''' <note>All properties are obtained from <see cref="Angle"/> instance that could be affected by previous normalization by the N specifier.</note>
        ''' </description>
        ''' </item>
        ''' </list>
        ''' <para>In case you require custom format specifier that is one character long, precede it with % character. % character is ignored if it is first character of custom format specifier. In other position "%" is passed to resulting string. Note: % itself alone is not valid custom format specifier, use \%.</para>
        ''' </remarks>
        Public Overloads Function ToString(format As String, formatProvider As IFormatProvider) As String Implements IFormattable.ToString
            If format = "" Then format = "G"
            If format.Length = 1 OrElse shortFormatRegex.IsMatch(format) Then
                Dim param As Integer?
                If format.Length > 1 Then param = Integer.Parse(format.Substring(1), InvariantCulture)
                Select Case format(0)           '                                           parameter             no parameter   custom             
                    Case "G"c, "g"c, "l"c, "L"c 'General/long -14°10'15.33"                 no dec places for "   as required    -d°mm'ss[.f]"      
                        format = "-d°mm'ss" + If(param.HasValue, If(param.Value = 0, "", "[." & New String("f", param.Value)) + "]", "(RestSeconds,R)") + """"
                    Case "S"c, "s"c             'Short -14°09'05"                             not allowed                           -d°mm'ss"         
                        If param.HasValue Then Throw New FormatException("Parameter is not allowed for standard numeric format {0}".f(format.Substring(0)))
                        format = "-d°mm'ss"""
                    Case "F"c, "f"c             'Decimal -14.978425                         no decimal places      as required    -D                
                        format = If(param.HasValue, "-D" & param.ToString(InvariantCulture), "-(TotalDegrees,R)")
                    Case "N"c                   'G - normalized 0-360                       no dec places for "    as required    N-d°mm'ss[.f]"    
                        format = "Nd°mm'ss" + If(param.HasValue, If(param.Value = 0, "", "[." & New String("f", param.Value)) + "]", "(RestSeconds,R)") + """"
                    Case "n"c                   'S - normalized 0-360                       not allowed                           N-d°mm'ss"        
                        If param.HasValue Then Throw New FormatException("Parameter is not allowed for standard numeric format {0}".f(format.Substring(0)))
                        format = "Nd°mm'ss"""
                    Case "e"c                   'F - normalized 0-360                       no decimal places      as required    N-D               
                        format = "N" & If(param.HasValue, "D" & param.ToString(InvariantCulture), "(TotalDegrees,R)")
                    Case "A"c, "Φ"c             'GPS latitude 14°10'15.33" N                no dec places for "    as required    N180d°mm'ss[.f]" a
                        format = "N180d°mm'ss" + If(param.HasValue, If(param.Value = 0, "", "[." & New String("f", param.Value)) + "]", "(RestSeconds,R)") + """ a"
                    Case "a"c, "φ"c             'GPS latitude 14°10'15 N                    not allowed                           N180d°mm'ss" a    
                        If param.HasValue Then Throw New FormatException("Parameter is not allowed for standard numeric format {0}".f(format.Substring(0)))
                        format = "N180d°mm'ss"" a"
                    Case "b"c                   'GPS latitude 14.154 N                      no decimal places      as required    N180D a           
                        format = "N180" & If(param.HasValue, "D" & param.ToString(InvariantCulture), "(TotalDegrees,R)") & " a"
                    Case "O"c, "Λ"c             'GPS longitude 14°10'15.33" E               no dec places for "    as required    N180d°mm'ss[.f]" o
                        format = "N180d°mm'ss" + If(param.HasValue, If(param.Value = 0, "", "[." & New String("f", param.Value)) + "]", "(RestSeconds,R)") + """ o"
                    Case "o"c, "λ"c             'GPS longitude 14°10'15" E                  not allowed                           N180d°mm'ss" o    
                        If param.HasValue Then Throw New FormatException("Parameter is not allowed for standard numeric format {0}".f(format.Substring(0)))
                        format = "N180d°mm'ss"" o"
                    Case "p"c                   'GPS longitude 14.33145 E                   no decimal places      as required    N180D o           
                        format = "N180" & If(param.HasValue, "D" & param.ToString(InvariantCulture), "(TotalDegrees,R)") & " o"
                    Case "t"c                   'Time short, no days 90:10:05               not allowed                           -d:mm:ss          
                        If param.HasValue Then Throw New FormatException("Parameter is not allowed for standard numeric format {0}".f(format.Substring(0)))
                        format = "-d:mm:ss"
                    Case "T"c                   'Time long, no days 90:10:05.335            no dec places for "    as required    -d:mm:ss[.f]      
                        format = "-d:mm:ss" + If(param.HasValue, If(param.Value = 0, "", "[." & New String("f", param.Value)) + "]", "(RestSeconds,R)")
                    Case "d"c                   'Time short, with days 7.15:10:05           not allowed                           -[y.]d:mm:ss      
                        If param.HasValue Then Throw New FormatException("Parameter is not allowed for standard numeric format {0}".f(format.Substring(0)))
                        format = "-[y.]h:mm:ss"
                    Case "D"c                   'Time long, with days 7.15:10:05.335        no dec places for "    as required    -[y.]d:mm:ss[.f]  
                        format = "-[y.]h:mm:ss" + If(param.HasValue, If(param.Value = 0, "", "[." & New String("f", param.Value)) + "]", "(RestSeconds,R)")
                    Case "E"c, "e"c             'Rotations                                  no dec places          max 4          -E                
                        format = "-E" & param.ToString(InvariantCulture)
                    Case "R"c, "r"c             'Radians                                    no dec places          max 4          -R                
                        format = "-R" & param.ToString(InvariantCulture)
                    Case "Z"c, "z"c             'Grads                                      no dec places          max 4          -Z                
                        format = "-Z" & param.ToString(InvariantCulture)
                    Case Else : Throw New FormatException("Unknown standard format specifier {0}".f(format(0)))
                End Select
            End If

            'Custom formatter:                                                                                         parameter              no parameter   repeat            
            Dim normalizedValue = Me 'Indicates current value used for rendering
            Dim compatibilityRendering As Boolean = False 'Indicates if compatibility rendering is used
            Dim builders As New Stack(Of MeaningfulStringbuilder) 'String builders. One root, one ofr each nest level of []
            builders.Push(New MeaningfulStringbuilder)
            Dim has_d As Boolean = False, has_m As Boolean = False, has_y As Boolean = False 'Indicates if specific specifier is present
            Dim value As IFormattable = Nothing 'Value to be formatted - Double or Integer
            Dim specifier As Char? = Nothing 'Currently processed specifier
            Dim state As FState = FState.Start 'FSA state
            Dim leftPlaces As Integer? = Nothing 'Places left of . (negative values are recomendation)
            Dim rightPlaces As Integer? = Nothing 'Places right from . (negative values are recomendation)
            Dim ainfo = If(TypeOf formatProvider Is CultureInfo, GlobalizationT.AngleFormatInfo.Get(DirectCast(formatProvider, CultureInfo)), If(TryCast(formatProvider.GetFormat(GetType(GlobalizationT.AngleFormatInfo)), GlobalizationT.AngleFormatInfo), GlobalizationT.AngleFormatInfo.DefaultInvariant))
            Dim ninfo = If(TryCast(formatProvider.GetFormat(GetType(NumberFormatInfo)), NumberFormatInfo), InvariantCulture.NumberFormat)
            Dim dinfo = If(TryCast(formatProvider.GetFormat(GetType(DateTimeFormatInfo)), DateTimeFormatInfo), InvariantCulture.DateTimeFormat)
            Dim useThousandSeparator As Boolean = False 'Use thousand (group) seperator for formatted value?
            Dim customName As StringBuilder = Nothing  'Builds up custom property name
            Dim customFormat As StringBuilder = Nothing 'Builds up custom format
            For i = 0 To format.Length - 1
                Dim ch = format(i)
                Select Case state
                    Case FState.Start, FState.Normal
                        specifier = Nothing : value = Nothing : useThousandSeparator = False : customName = Nothing
                        Select Case ch
                            Case "D"c  'D degrees, decimal                                                             no dec places          as required    min digits        
                                value = normalizedValue.TotalDegrees : state = FState.SpecifierDouble : specifier = ch : has_d = True
                            Case "d"c  'd degrees, whole                                                               not allowed                           min digits        
                                value = normalizedValue.Degrees : state = FState.SpecifierInt : specifier = ch : has_d = True
                            Case "M"c  'M minutes, decimal (excl degrees if d/D or h/H present)                                 no decimal places      as required    min digits        
                                value = If(has_d, normalizedValue.TotalMinutes - CDbl(normalizedValue.Degrees) * 60.0#, normalizedValue.TotalMinutes)
                                specifier = ch : state = FState.SpecifierDouble : has_m = True
                            Case "m"c  'm minutes, whole                                                               not allowed                           min digits        
                                value = normalizedValue.Minutes : state = FState.SpecifierInt : specifier = ch : has_m = True
                            Case "S"c  'S seconds, decimal (excl degrees if d/D or h/H present, excl minutes if m/M present)      no decimal places      as required    min digits        
                                If has_m Then
                                    value = normalizedValue.Seconds
                                ElseIf has_d Then
                                    value = normalizedValue.TotalSeconds - normalizedValue.Degrees * 60.0# * 60.0#
                                Else
                                    value = normalizedValue.TotalSeconds
                                End If
                                specifier = ch : state = FState.SpecifierDouble
                            Case "s"c  's seconds, whole                                                               not allowed                           min digits        
                                value = CInt(Math.Floor(normalizedValue.Seconds))
                                specifier = ch : state = FState.SpecifierInt
                            Case "f"c  'f second fraction                                                              no decimal places max  as required    min digits        
                                value = normalizedValue.Seconds - Math.Floor(normalizedValue.Seconds)
                                specifier = ch : state = FState.SpecifierF : leftPlaces = 1
                            Case "Y"c  'Y days, decimal                                                                no decimal places      as required    min digits        
                                value = normalizedValue.TotalDegrees / 24.0# : state = FState.SpecifierDouble : specifier = ch : has_y = True
                            Case "y"c  'y days, whole                                                                  not allowed                           min digits        
                                value = Math.Floor(normalizedValue.TotalDegrees / 24) : state = FState.SpecifierInt : specifier = ch : has_y = True
                            Case "H"c  'H degrees, decimal (excl days if y/Y present)                                    no decimal places      as required    min digits        
                                value = If(has_y, Math.Floor(normalizedValue.TotalDegrees - Math.Floor(normalizedValue.TotalDegrees / 24.0#) * 24.0#), normalizedValue.Degrees)
                                specifier = ch : state = FState.SpecifierDouble : has_d = True
                            Case "h"c  'h degrees, whole, excl. days                                                      not allowed                        min digits
                                value = CInt(Math.Floor(normalizedValue.TotalDegrees - normalizedValue.TotalDegrees \ 24.0# * 24.0#))
                                specifier = ch : state = FState.SpecifierInt : has_d = True
                            Case "A"c, "Φ"c  'A Latitude specifier (north/south), long                                       not allowed                                             
                                builders.Peek.Append(If(normalizedValue <= Angle.zero, ainfo.LatitudeNorthLongSymbol, ainfo.LatitudeSouthLongSymbol)) : state = FState.Normal
                            Case "O"c, "Λ"c  'O Longitude specifier (east/west), long                                        not allowed                                             
                                builders.Peek.Append(If(normalizedValue <= Angle.zero, ainfo.LongitudeEastLongSymbol, ainfo.LongitudeWestLongSymbol)) : state = FState.Normal
                            Case "a"c, "φ"c  'a Latitude specifier (N/S), short                                              not allowed                                             
                                builders.Peek.Append(If(normalizedValue <= Angle.zero, ainfo.LatitudeNorthShortSymbol, ainfo.LatitudeSouthShortSymbol)) : state = FState.Normal
                            Case "o"c, "λ"c  'o Longitude specifier (E/W), short                                             not allowed                                             
                                builders.Peek.Append(If(normalizedValue <= Angle.zero, ainfo.LongitudeEastShortSymbol, ainfo.LongitudeWestShortSymbol)) : state = FState.Normal
                            Case "E"c  'E Rotations value                                                              no decimal places      max 4          min digits        
                                value = normalizedValue.Rotations : specifier = ch : state = FState.SpecifierDouble : rightPlaces = -4
                            Case "R"c  'R radians value                                                                no decimal places      max 4          min digits        
                                value = normalizedValue.ToRadians : specifier = ch : state = FState.SpecifierDouble : rightPlaces = -4
                            Case "π"c, "p"c  'π-radians value                                                                no decimal places      max 4          min digits        
                                value = normalizedValue.ToRadians / Math.PI : specifier = ch : state = FState.SpecifierDouble : rightPlaces = -4
                            Case "Z"c  'Z grads value                                                                  no decimal places      max 4          min digits        
                                value = normalizedValue.ToGradians : specifier = ch : state = FState.SpecifierDouble : rightPlaces = -4
                            Case "-"c  '- minus sign (if required)                                                     not allowed                                             
                                If normalizedValue._value < 0.0# Then builders.Peek.Append(ninfo.NegativeSign)
                                state = FState.Normal
                            Case "+"c  '+ plus/minus sign (always)                                                     not allowed                                             
                                builders.Peek.Append(If(normalizedValue._value < 0.0#, ninfo.NegativeSign, ninfo.PositiveSign)) : state = FState.Normal
                            Case "."c  '. decimal point                                                                not allowed                                             
                                builders.Peek.Append(ninfo.NumberDecimalSeparator) : state = FState.Normal
                            Case ","c ', thousand separator
                                builders.Peek.Append(ninfo.NumberGroupSeparator) : state = FState.Normal
                            Case "°"c  '° degree sign                                                                  not allowed                                             
                                builders.Peek.Append(If(compatibilityRendering, ainfo.CompatibilityDegreeSign, ainfo.DegreeSign)) : state = FState.Normal
                            Case "'"c, "′"c  '' minute sing                                                                  not allowed                                             
                                builders.Peek.Append(If(compatibilityRendering, ainfo.CompatibilityMinuteSign, ainfo.MinuteSign)) : state = FState.Normal
                            Case """"c, "″"c '" second sing                                                                   not allowed                                             
                                builders.Peek.Append(If(compatibilityRendering, ainfo.CompatibilitySecondSign, ainfo.SecondSign)) : state = FState.Normal
                            Case ":"c  ': time separator                                                               not allowed                                             
                                builders.Peek.Append(dinfo.TimeSeparator) : state = FState.Normal
                            Case "\"c  '\ escape                                                                       single char            error          \                 
                                state = FState.Slash
                            Case "%"c  '% if used as 1st character - ignored                                           rest of string         error          % as first        
                                If state = FState.Start Then state = FState.Normal Else builders.Peek.Append(ch)
                            Case "N"c  'N normalize to range                                                           value for Normalize()  360                              
                                state = FState.Normalize1 : value = Nothing : specifier = ch
                            Case "c"c  'c use comaptibility characters for °,Case ","c ',"                             not allowed                                             
                                compatibilityRendering = True : state = FState.Normal
                            Case "["c  '[] optional component specifier, can be nested. reners only if inside non-zero inside                 empty                            
                                builders.Push(New MeaningfulStringbuilder) : state = FState.Normal
                            Case "]"c
                                If builders.Count <= 1 Then Throw New FormatException("Invalid format - too much ]s")
                                Dim oldBuilder = builders.Pop
                                If oldBuilder.Meaningful Then builders.Peek.Append(oldBuilder.ToString) : builders.Peek.Meaningful = True
                                state = FState.Normal
                            Case "("c '() custom formatted property - format (propertyName,format)
                                state = FState.Custom1
                            Case Else  'anything else emit
                                builders.Peek.Append(ch) : state = FState.Normal
                        End Select
                    Case FState.Slash '\
                        builders.Peek.Append(ch) : state = FState.Normal
                    Case FState.Normalize1 'N
                        Select Case ch
                            Case "-"c : state = FState.NormalizeMinus
                            Case "0"c To "9"c : value = ch.NumericValue : state = FState.Normalize
                            Case Else : normalizedValue = Normalize() : i -= 1 : state = FState.Normal
                        End Select
                    Case FState.NormalizeMinus 'N-
                        Select Case ch
                            Case "0"c To "9"c : value = -ch.NumericValue : state = FState.Normalize
                            Case Else : normalizedValue = Normalize() : i -= 2 : state = FState.Normal
                        End Select
                    Case FState.Normalize 'N0
                        Select Case ch
                            Case "0"c To "9"c
                                If value Is Nothing OrElse Not TypeOf value Is Integer Then value = ch.NumericValue Else value = DirectCast(value, Integer) * 10 + ch.NumericValue * (Math.Sign(DirectCast(value, Integer)))
                            Case Else
                                If value Is Nothing OrElse Not TypeOf value Is Integer Then normalizedValue = Normalize() Else normalizedValue = Normalize(DirectCast(value, Integer))
                                i -= 1 : state = FState.Normal
                        End Select
                    Case FState.SpecifierDouble 'Specifiers which include double value
                        Select Case ch
                            Case specifier.Value
                                If leftPlaces Is Nothing OrElse leftPlaces < 0 Then leftPlaces = 2 Else leftPlaces += 1
                            Case "0"c To "9"c : state = FState.SpecifierDoubleN : rightPlaces = ch.NumericValue
                            Case ","c : useThousandSeparator = True
                            Case Else
                                AppendValue(value, leftPlaces, rightPlaces, builders.Peek, useThousandSeparator, formatProvider)
                                state = FState.Normal : i -= 1
                        End Select
                    Case FState.SpecifierDoubleN 'Number after double-value specifier
                        Select Case ch
                            Case "0"c To "9"c
                                If rightPlaces Is Nothing OrElse rightPlaces < 0 Then rightPlaces = 0
                                rightPlaces = rightPlaces * 10 + ch.NumericValue
                            Case ","c : useThousandSeparator = True
                            Case Else
                                AppendValue(value, leftPlaces, rightPlaces, builders.Peek, useThousandSeparator, formatProvider)
                                state = FState.Normal : i -= 1
                        End Select
                    Case FState.SpecifierInt 'Specifiers which include integer value
                        Select Case ch
                            Case specifier.Value
                                If leftPlaces Is Nothing OrElse leftPlaces < 0 Then leftPlaces = 2 Else leftPlaces += 1
                            Case ","c : useThousandSeparator = True
                            Case Else
                                AppendValue(value, leftPlaces, rightPlaces, builders.Peek, useThousandSeparator, formatProvider)
                                state = FState.Normal : i -= 1
                        End Select
                    Case FState.SpecifierF 'Specifier for sub-second values
                        Select Case ch
                            Case "f"c : leftPlaces += 1
                            Case "0"c To "9"c : rightPlaces = ch.NumericValue : state = FState.SpecifierFN
                            Case Else
                                AppendValueF(value, leftPlaces, 0, builders.Peek, formatProvider)
                                state = FState.Normal : i -= 1
                        End Select
                    Case FState.SpecifierFN 'Places for sub-second values
                        Select Case "c"
                            Case "0"c To "9"c : rightPlaces = rightPlaces * 10 + ch.NumericValue
                            Case Else
                                AppendValueF(value, leftPlaces, rightPlaces, builders.Peek, formatProvider)
                                state = FState.Normal : i -= 1
                        End Select
                    Case FState.Custom1 '(
                        customName = New StringBuilder
                        Select Case ch
                            Case "A"c To "Z"c, "a"c To "z"c, "0"c To "9"c, "_"c : customName.Append(ch) : state = FState.Custom
                            Case Else : Throw New FormatException("Expected identifier")
                        End Select
                    Case FState.Custom 'Identifier in ()
                        Select Case ch
                            Case "A"c To "Z"c, "a"c To "z"c, "0"c To "9"c, "_"c : customName.Append(ch) : state = FState.Custom
                            Case ","c : state = FState.CustomFormat : customFormat = New StringBuilder
                            Case ")" : AppendCustom(customName.ToString, builders.Peek, Nothing, formatProvider, normalizedValue) : state = FState.Normal
                            Case Else
                                If ch.IsWhiteSpace Then state = FState.CustomWH Else Throw New FormatException("Invallid format: Unexpected '{0}', expected , or )".f(ch))
                        End Select
                    Case FState.CustomWH 'Whitespace after identifier in ()
                        Select Case ch
                            Case "," : state = FState.CustomFormat : customFormat = New StringBuilder
                            Case ")" : AppendCustom(customName.ToString, builders.Peek, Nothing, formatProvider, normalizedValue) : state = FState.Normal
                            Case Else
                                If Not ch.IsWhiteSpace Then Throw New FormatException("Invallid format: Unexpected '{0}', expected , or )".f(ch))
                        End Select
                    Case FState.CustomFormat 'In () after ,
                        Select Case ch
                            Case "\"c : state = FState.CustomFormatSlash
                            Case ")"c : AppendCustom(customName.ToString, builders.Peek, customFormat.ToString, formatProvider, normalizedValue) : state = FState.Normal
                            Case Else : customFormat.Append(ch)
                        End Select
                    Case FState.CustomFormatSlash '\ in () after ,
                        customFormat.Append(ch) : state = FState.CustomFormat
                    Case Else : Throw New ApplicationException("Unknown FSA state {0}".f(state)) 'Should never happen
                End Select
            Next
[end]:
            Select Case state
                Case FState.Normal, FState.Start
                    If builders.Count > 1 Then Throw New FormatException("Invalid format, {0} expected".f("]"c))
                Case FState.Slash '\
                    builders.Peek.Append("\") : state = FState.Normal : GoTo [end]
                Case FState.Normalize 'N
                    If value Is Nothing OrElse Not TypeOf value Is Integer Then normalizedValue = Normalize() Else normalizedValue = Normalize(DirectCast(value, Integer))
                    state = FState.Normal : GoTo [end]
                Case FState.SpecifierDouble 'Specifiers which include double value
                    AppendValue(value, leftPlaces, rightPlaces, builders.Peek, useThousandSeparator, formatProvider)
                    state = FState.Normal : GoTo [end]
                Case FState.SpecifierDoubleN 'Number after double-value specifier
                    AppendValue(value, leftPlaces, rightPlaces, builders.Peek, useThousandSeparator, formatProvider)
                    state = FState.Normal : GoTo [end]
                Case FState.SpecifierInt 'Specifiers which include integer value
                    AppendValue(value, leftPlaces, rightPlaces, builders.Peek, useThousandSeparator, formatProvider)
                    state = FState.Normal : GoTo [end]
                Case FState.SpecifierF 'Specifier for sub-second values
                    AppendValueF(value, leftPlaces, 0, builders.Peek, formatProvider)
                    state = FState.Normal : GoTo [end]
                Case FState.SpecifierFN 'Places for sub-second values
                    AppendValueF(value, leftPlaces, rightPlaces, builders.Peek, formatProvider)
                    state = FState.Normal : GoTo [end]
                Case FState.Custom1, FState.Custom, FState.CustomWH, FState.CustomFormat, FState.CustomFormatSlash
                    Throw New FormatException("Invalid format, {0} expected".f("]"c))
                Case Else : Throw New ApplicationException("Unknown FSA state {0}".f(state)) 'Should never happen
            End Select
            Return builders.Single.ToString
        End Function


        ''' <summary>Appends custom-formatted custom value to a builder and sets builder meaningfullnes</summary>
        ''' <param name="name">Name of property to use (case insensitive (with exception of SF prefix)). If name starts with "SF" (case-sensitive) <see cref="System.String.Format"/> is used (value is passed there as first and only parameter; the SF prefix is then ignored when detecting property name), otherwise <see cref="IFormattable.ToString"/> is used. See <see cref="ToString"/> for list of supported property names.</param>
        ''' <param name="builder">A builder to use</param>
        ''' <param name="format">Format for <see cref="IFormattable.ToString"/> or <see cref="System.String.Format"/></param>
        ''' <param name="formatProvider">Format provider to use</param>
        ''' <param name="normalizedValue">Value to obtain property values from</param>
        ''' <exception cref="ArgumentNullException"><paramref name="builder"/> or <paramref name="formatProvider"/> is nothing</exception>
        ''' <exception cref="FormatException"><paramref name="name"/> is not known -or- <paramref name="format"/> is invalid</exception>
        Private Shared Sub AppendCustom(name As String, builder As MeaningfulStringbuilder, format As String, formatProvider As IFormatProvider, normalizedValue As Angle)
            If builder Is Nothing Then Throw New ArgumentNullException("builder")
            If formatProvider Is Nothing Then Throw New ArgumentNullException("formatProvider")
            Dim value As IFormattable
            Dim useStringFormat = False
            If (name.StartsWith("SF")) Then
                useStringFormat = True
                name = name.Substring(2)
            End If
            Select Case name.ToLowerInvariant
                Case "degrees" : value = normalizedValue.Degrees
                Case "minutes" : value = normalizedValue.Minutes
                Case "seconds" : value = normalizedValue.Seconds
                Case "totaldegrees", "totalhours" : value = normalizedValue.TotalDegrees
                Case "totalminutes" : value = normalizedValue.TotalMinutes
                Case "totalseconds" : value = normalizedValue.TotalSeconds
                Case "rotations" : value = normalizedValue.Rotations
                Case "radians" : value = normalizedValue.ToRadians
                Case "gradians" : value = normalizedValue.ToGradians
                Case "piradians" : value = normalizedValue.ToRadians / Math.PI
                Case "days" : value = CInt(Math.Floor(normalizedValue.TotalDegrees / 24.0#))
                Case "totaldays" : value = normalizedValue.TotalDegrees / 24.0#
                Case "restminutes" : value = normalizedValue.TotalMinutes - normalizedValue.TotalDegrees * 60.0#
                Case "restseconds" : value = normalizedValue.TotalSeconds - normalizedValue.TotalMinutes * 60.0#
                Case "hours" : value = CInt(Math.Floor(normalizedValue.Degrees - Math.Floor(normalizedValue.Degrees / 24.0#) * 24.0#))
                Case "resthours" : value = normalizedValue.Degrees - Math.Floor(normalizedValue.Degrees / 24.0#) * 24.0#
                Case "time" : value = CType(normalizedValue, TimeSpan)
                Case "timeformattable" : value = CType(normalizedValue, TimeSpanFormattable)
                Case Else : Throw New FormatException("Unknown custom property name {0}".f(name))
            End Select
            Dim ret$
            If useStringFormat Then
                ret = String.Format(formatProvider, format, value)
            Else
                ret = value.ToString(format, formatProvider)
            End If
            builder.Meaningful = If(TypeOf value Is Integer, DirectCast(value, Integer) <> 0I AndAlso ret <> "",
                                 If(TypeOf value Is Double, DirectCast(value, Double) <> 0.0# AndAlso oneToNine.IsMatch(ret),
                                 If(TypeOf value Is TimeSpan, DirectCast(value, TimeSpan) <> TimeSpan.Zero AndAlso oneToNine.IsMatch(ret),
                                 If(TypeOf value Is TimeSpanFormattable, DirectCast(value, TimeSpanFormattable) <> TimeSpan.Zero AndAlso oneToNine.IsMatch(ret),
                                    oneToNine.IsMatch(ret)
                                 ))))
            builder.Append(ret)
        End Sub
        ''' <summary>A regular expression to detect if string contains non-zero numeral</summary>
        Private Shared ReadOnly oneToNine As New Regex("[1-9]", RegexOptions.Compiled Or RegexOptions.CultureInvariant)

        ''' <summary>Appends formated decimal-only value to given <see cref="MeaningfulStringbuilder"/></summary>
        ''' <param name="value">Value to append</param>
        ''' <param name="maxDecPlaces">Maximal decimal places of formated value</param>
        ''' <param name="minDecPlaces">Minimal decimal places of formated value</param>
        ''' <param name="builder">Builder to append value to and set meaningfulity of</param>
        ''' <param name="formatProvider">Format provider to use to format value</param>
        ''' <exception cref="ArgumentNullException"><paramref name="builder"/> or <paramref name="formatProvider"/> is null.</exception>
        ''' <remarks>Value is appended without leading decimal separator</remarks>
        Private Shared Sub AppendValueF(value As Double, maxDecPlaces%, minDecPlaces%, builder As MeaningfulStringbuilder, formatProvider As IFormatProvider)
            If builder Is Nothing Then Throw New ArgumentNullException("builder")
            If formatProvider Is Nothing Then Throw New ArgumentNullException("formatProvider")
            Dim format As String
            Dim mmfl As Boolean
            If minDecPlaces >= maxDecPlaces Then
                format = "#." & New String("0"c, minDecPlaces)
                mmfl = Math.Round(value, minDecPlaces) <> 0.0#
            Else
                format = "#." & New String("0"c, minDecPlaces) & New String("#"c, maxDecPlaces - minDecPlaces)
                mmfl = Math.Round(value, maxDecPlaces) <> 0.0#
            End If
            Dim ni = If(TryCast(formatProvider.GetFormat(GetType(NumberFormatInfo)), NumberFormatInfo), InvariantCulture.NumberFormat)
            builder.Append(value.ToString(format, formatProvider).TrimStart(ni.NumberDecimalSeparator))
            builder.Meaningful = builder.Meaningful AndAlso mmfl
        End Sub

        ''' <summary>Appends formatted value to given <see cref="MeaningfulStringbuilder"/></summary>
        ''' <param name="value">Value to be appened. Must be either <see cref="Integer"/> or <see cref="Double"/>.</param>
        ''' <param name="leftPlaces">Compulsory places left from decimal point. If negative made absolute. Null treated as 1.</param>
        ''' <param name="rightPlaces">Compulsory right places from decimal point. If negative made absolute and means max decimal places. Null treated as max 128 optional places (-128).</param>
        ''' <param name="builder">A builder to append value to and indicate if it is meaningful</param>
        ''' <param name="useThousandSeparator">True to use throusand separator for formatted <paramref name="value"/>.</param>
        ''' <param name="formatProvider">Format provider for formatting</param>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/>, <paramref name="builder"/> or <paramref name="formatProvider"/> is null</exception>
        ''' <exception cref="TypeMismatchException"><paramref name="value"/> is neither <see cref="Integer"/> nor <see cref="Double"/>.</exception>
        Private Shared Sub AppendValue(value As IFormattable, leftPlaces As Integer?, rightPlaces As Integer?, builder As MeaningfulStringbuilder, useThousandSeparator As Boolean, formatProvider As IFormatProvider)
            If value Is Nothing Then Throw New ArgumentNullException("value")
            If Not TypeOf value Is Integer AndAlso Not TypeOf value Is Double Then Throw New TypeMismatchException(value, "value", GetType(Integer), GetType(Double))
            If builder Is Nothing Then Throw New ArgumentNullException("builder")
            If formatProvider Is Nothing Then Throw New ArgumentNullException("formatProvider")
            If leftPlaces < 0 Then leftPlaces *= -1

            rightPlaces = If(rightPlaces, -128)
            Dim rightFormat = If(rightPlaces.Value > 0, "0"c, "#"c)
            rightPlaces = Math.Abs(rightPlaces.Value)

            Dim format As String = ""
            If useThousandSeparator Then format &= "#,"
            If leftPlaces Is Nothing Then
                format &= "0"
            ElseIf leftPlaces = 0 Then
                format &= "#"
            Else
                format &= New String("0"c, leftPlaces)
            End If
            If TypeOf value Is Double AndAlso (rightPlaces Is Nothing OrElse rightPlaces > 0) Then
                format &= "."
                If rightPlaces.HasValue Then format &= New String(rightFormat, rightPlaces)
            End If
            builder.Append(value.ToString(format, formatProvider))
            builder.Meaningful = builder.Meaningful AndAlso If(
                TypeOf value Is Integer, DirectCast(value, Integer) <> 0,
                Math.Round(DirectCast(value, Double), If(rightPlaces, 128)) <> 0.0#
            )
        End Sub

        ''' <summary>Staes of FSA used in <see cref="ToString"/> to parse custom format string</summary>
        Private Enum FState
            ''' <summary>First characters</summary>
            Start
            ''' <summary>In normal test after 1st character</summary>
            Normal
            ''' <summary>In specifier which produces <see cref="Double"/> value</summary>
            SpecifierDouble
            ''' <summary>In specifier which produces <see cref="Integer"/> value</summary>
            SpecifierInt
            ''' <summary>In sub-second specifier (f)</summary>
            SpecifierF
            ''' <summary>Number after sub-second specifier (f)</summary>
            SpecifierFN
            ''' <summary>Backslash (\) in <see cref="Normal"/> or <see cref="Start"/> state</summary>
            Slash
            ''' <summary>First charatcer of parameter of normalization specifier (N)</summary>
            Normalize1
            ''' <summary>Minust after normalization specifier (N-)</summary>
            NormalizeMinus
            ''' <summary>In normalization specifier (N) after first char of value of parameter</summary>
            Normalize
            ''' <summary>Number after a specifier which produces double number</summary>
            SpecifierDoubleN
            ''' <summary>First charatcer in ()</summary>
            Custom1
            ''' <summary>Name of property in ()</summary>
            Custom
            ''' <summary>Whitespace after name of property in ()</summary>
            CustomWH
            ''' <summary>Custom format in () after ,</summary>
            CustomFormat
            ''' <summary>Backslash in custom format (in () after ,)</summary>
            CustomFormatSlash


        End Enum

        ''' <summary>Wraps <see cref="StringBuilder"/> and adds a <see cref="MeaningfulStringbuilder.Meaningful">Meaningful</see> property</summary>
        <DebuggerDisplay("Builder")>
        Private Class MeaningfulStringbuilder
            Private ReadOnly _builder As New StringBuilder
            ''' <summary>Gets internal <see cref="StringBuilder"/></summary>
            Public ReadOnly Property Builder As StringBuilder
                Get
                    Return _builder
                End Get
            End Property
            ''' <summary>Appends a character to internal <see cref="StringBuilder"/></summary>
            ''' <param name="ch">A character to append</param>
            Public Sub Append(ch As Char)
                Builder.Append(ch)
            End Sub
            ''' <summary>Appends a string to internal <see cref="StringBuilder"/></summary>
            ''' <param name="str">String to append</param>
            Public Sub append(str As String)
                Builder.Append(str)
            End Sub
            ''' <summary>Gets lenght of internal <see cref="StringBuilder"/></summary>
            Public ReadOnly Property Length%
                Get
                    Return Builder.Length
                End Get
            End Property
            ''' <summary>Returns a string that represents the current object.</summary>
            ''' <returns>Characters accumulated in internal <see cref="StringBuilder"/></returns>
            Public Overrides Function ToString() As String
                Return Builder.ToString()
            End Function
            Public Property Meaningful As Boolean
        End Class
#End Region

#Region "Parse"
        'Public Shared Function Parse(value As String) As Angle

        'End Function
        'Public Shared Function Parse(value As String, formatProvider As IFormatProvider)

        'End Function
        'Public Shared Function TryParse(value As String, <Out()> ByRef result As Angle) As Boolean

        'End Function
        'Public Shared Function TryParse(value As String, formatProvider As IFormatProvider, <Out()> ByRef result As Angle) As Boolean

        'End Function
#End Region
    End Structure

    ''' <summary>Implements <see cref="IComparer(Of T)"/> and <see cref="IEqualityComparer(Of T)"/> for <see cref="Angle"/></summary>
    ''' <remarks>Instances of this class cannot be created. Use the two provided comparers: <see cref="AngleComparer.Normalizing"/> and <see cref="AngleComparer.NonNormalizing"/>.</remarks>
    ''' <seelaso cref="Angle"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public NotInheritable Class AngleComparer
        Inherits Comparer(Of Angle)
        Implements IEqualityComparer(Of Angle)

        Private ReadOnly _normalizes As Boolean
        ''' <summary>CTor - creates a new instance of the <see cref="AngleComparer"/> class indicating if it does angle normalization or not</summary>
        ''' <param name="normalizes">True to indicate that normalization is performed, false otherwise.</param>
        Private Sub New(normalizes As Boolean)
            Me._normalizes = normalizes
        End Sub
        ''' <summary>Gets value indicating if this comparer does angle normalization before comparison or not.</summary>
        Public ReadOnly Property Normalizes As Boolean
            Get
                Return _normalizes
            End Get
        End Property

        ''' <summary>Determines whether the specified objects are equal.</summary>
        ''' <param name="x">The first <see cref="Angle"/> to compare.</param>
        ''' <param name="y">The second <see cref="Angle"/> to compare.</param>
        ''' <returns>true if the specified objects are equal; otherwise, false.</returns>
        Public Overloads Function Equals(x As Angle, y As Angle) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of Angle).Equals
            If Normalizes Then Return x.Normalize.TotalDegrees = y.Normalize.TotalDegrees Else Return x.TotalDegrees = y.TotalDegrees
        End Function

        ''' <summary>Returns a hash code for the specified object.</summary>
        ''' <param name="obj">The <see cref="Angle"/> for which a hash code is to be returned.</param>
        ''' <returns>A hash code for the specified object.</returns>
        Public Overloads Function GetHashCode(obj As Angle) As Integer Implements System.Collections.Generic.IEqualityComparer(Of Angle).GetHashCode
            If Normalizes Then Return obj.Normalize.TotalDegrees.GetHashCode Else Return obj.TotalDegrees.GetHashCode
        End Function

        ''' <summary>Performs a comparison of two objects of the same type and returns a value indicating whether one object is less than, equal to, or greater than the other.</summary>
        ''' <param name="x">The first <see cref="Angle"/> to compare.</param>
        ''' <param name="y">The second <see cref="Angle"/> to compare.</param>
        ''' <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero <paramref name="x" /> is less than <paramref name="y" />.Zero <paramref name="x" /> equals <paramref name="y" />.Greater than zero <paramref name="x" /> is greater than <paramref name="y" />.</returns>
        Public Overrides Function Compare(x As Angle, y As Angle) As Integer
            If Normalizes Then
                Return Comparer(Of Double).Default.Compare(x.Normalize.TotalDegrees, y.Normalize.TotalDegrees)
            Else
                Return Comparer(Of Double).Default.Compare(x.TotalDegrees, y.TotalDegrees)
            End If
        End Function

        Private Shared ReadOnly _normalizing As New AngleComparer(True)
        ''' <summary>Gets a normalizing <see cref="AngleComparer"/></summary>
        ''' <returns>A comparer that does 360°-normalization of angle values before comparing them</returns>
        Public Shared ReadOnly Property Normalizing As AngleComparer
            Get
                Return _normalizing
            End Get
        End Property
        Private Shared ReadOnly _nonNormalizing As New AngleComparer(False)
        ''' <summary>Gets a non-normalizing comparer</summary>
        ''' <returns>A comparer that does not normalization and compares <see cref="Angle.TotalDegrees"/> directly.</returns>
        Public Shared ReadOnly Property NonNormalizing As AngleComparer
            Get
                Return _nonNormalizing
            End Get
        End Property
    End Class
End Namespace

Namespace GlobalizationT

    ''' <summary>Provides information how to format <see cref="NumericsT.Angle"/> instances</summary>
    ''' <remarks>
    ''' Unlike e.g. <see cref="NumberFormatInfo"/> instance of this class cannot usually be obtained via <see cref="CultureInfo.GetFormat"/>. Use <see cref="AngleFormatInfo.[Get]"/> instead.
    ''' <para>To localize <see cref="AngleFormatInfo"/> either provide your own instance by cloning and customizing existing instance such as <see cref="AngleFormatInfo.DefaultInvariant"/> or localize <c>Tools.GlobalizationT.AngleFormatInfo.resources</c> embeded resource.</para>
    ''' <note type="inheritinfo">This class is not sealed, but derived classes cannot be created because there are no accessible constructors. This class is not intended to be inherited from externally.</note>
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <Serializable(), ComVisible(True)>
    Public Class AngleFormatInfo
        Implements ICloneable(Of AngleFormatInfo), IFormatProvider

        ''' <summary>Gets instance of <see cref="AngleFormatInfo"/> for current culture</summary>
        ''' <returns>A new instance of <see cref="AngleFormatInfo"/> that provides formating informations for <see  cref="NumericsT.Angle"/> in current culture</returns>
        Public Shared Function [Get]() As AngleFormatInfo
            Return [Get](Nothing)
        End Function

        ''' <summary>Gets instance of <see cref="AngleFormatInfo"/> for given culture</summary>
        ''' <param name="culture">Culture to load <see cref="AngleFormatInfo"/> for. Current culture is used if null.</param>
        ''' <returns>A new instance of <see cref="AngleFormatInfo"/> that provides formating informations for <see  cref="NumericsT.Angle"/> in <paramref name="culture"/>.</returns>
        ''' <remarks>
        ''' If formatting info would be by chance available through <paramref name="culture"/>.<see cref="CultureInfo.GetFormat">GetFormat</see> it is used.
        ''' Otherwise resource-based <see cref="AngleFormatInfo"/> is loaded. If information is not provided for <paramref name="culture"/> parent culture or invariant culture is used.
        ''' </remarks>
        Public Shared Function [Get](culture As CultureInfo) As AngleFormatInfo
            If culture Is Nothing Then culture = System.Globalization.CultureInfo.CurrentCulture
            Dim an = TryCast(culture.GetFormat(GetType(AngleFormatInfo)), AngleFormatInfo)
            If an Is Nothing Then an = New ResourcesBasedAngleFormatInfo(culture)
            Return an
        End Function

#Region "Helper constants"
        ''' <summary>Default symbol used for degree in compatibility rendering. It's a degree sign (U+B0, °).</summary>
        ''' <remarks>Default compatibility degree sign is same as default typographic degree sign.</remarks>
        Public Const DefaultCompatibilityDegreeSign As String = "°"
        ''' <summary>Default symbol used for minutes in compatibility rendering. It's apostrophe (U+27, ').</summary>
        Public Const DefaultCompatibilityMinuteSign As String = "'"
        ''' <summary>Default symbol used for seconds in compatibility rendering. It's double quotation mark (U+22, ").</summary>
        Public Const DefaultCompatibilitySecondSign As String = """"

        ''' <summary>Default symbol used for degrees. It's a degree sign (U+B0, °).</summary>
        Public Const DefaultTypographicDegreeSign As String = ChrW(&HB0) '°
        ''' <summary>Defalt symbol used for minutes. It's prime (U+2032, ′).</summary>
        Public Const DefaultTypographicMinuteSign As String = ChrW(2032) '′
        ''' <summary>Default symbol used for seconds. It's double prime (U+2033, ″)</summary>
        Public Const DefaultTypographicSecondSign As String = ChrW(2033) '″
#End Region

#Region "Infrastructure"
        ''' <summary>Copy CTor - creates a new instance of the <see cref="AngleFormatInfo"/> class by copying values form another instance.</summary>
        ''' <param name="other">An instance ot copy</param>
        ''' <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception>
        Private Sub New(other As AngleFormatInfo)
            If other Is Nothing Then Throw New ArgumentNullException("other")
            DegreeSign = other.DegreeSign
            MinuteSign = other.MinuteSign
            SecondSign = other.SecondSign
            LongitudeEastShortSymbol = other.LongitudeEastShortSymbol
            LongitudeWestShortSymbol = other.LongitudeWestShortSymbol
            LatitudeNorthShortSymbol = other.LatitudeNorthShortSymbol
            LatitudeSouthShortSymbol = other.LatitudeSouthShortSymbol
            LongitudeEastLongSymbol = other.LongitudeEastLongSymbol
            LongitudeWestLongSymbol = other.LongitudeWestLongSymbol
            LatitudeNorthLongSymbol = other.LatitudeNorthLongSymbol
            LatitudeSouthLongSymbol = other.LatitudeSouthLongSymbol
            CompatibilityDegreeSign = other.CompatibilityDegreeSign
            CompatibilityMinuteSign = other.CompatibilityMinuteSign
            CompatibilitySecondSign = other.CompatibilitySecondSign
            _isReadOnly = False
        End Sub

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance.</returns>
        ''' <filterpriority>2</filterpriority>
        Private Function ICloneable_Clone() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Function Clone() As AngleFormatInfo Implements ICloneable(Of AngleFormatInfo).Clone
            Return New AngleFormatInfo(Me)
        End Function

        ''' <summary>Returns an object that provides formatting services for the specified type.</summary>
        ''' <returns>An instance of the object specified by <paramref name="formatType" />, if the <see cref="T:System.IFormatProvider" /> implementation can supply that type of object; otherwise, null.</returns>
        ''' <param name="formatType">An object that specifies the type of format object to return. </param>
        ''' <filterpriority>1</filterpriority>
        ''' <remarks>This implementation returns non-null value only if <paramref name="formatType"/> is <see cref="AngleFormatInfo"/>.</remarks>
        Private Function GetFormat(formatType As System.Type) As Object Implements System.IFormatProvider.GetFormat
            If Not formatType Is GetType(AngleFormatInfo) Then Return Nothing
            Return Me
        End Function

        ''' <summary>Sets the <see cref="IsReadOnly"/> property to true and thus makes this instance read-only (immutable)</summary>
        ''' <remarks>After calling this method no properties of this instance can be changed and thsi read-only state cannot be reverted.</remarks>
        Public Sub MakeReadOnly()
            _isReadOnly = True
        End Sub

        ''' <summary>Default CTor - ctreates a new empty instance of the <see cref="AngleFormatInfo"/> class.</summary>
        Friend Sub New()
        End Sub

        Private _isReadOnly As Boolean
        ''' <summary>Gets value indicating wheather values of properties of this instance can be changed or not</summary>
        ''' <returns>True if values of properties of this instance can be changed, false if values of properties of this instance cannot be changed (it's read-only (immutable))</returns>
        ''' <remarks>To set this property to true call <see cref="MakeReadOnly"/>.</remarks>
        Public ReadOnly Property IsReadOnly As Boolean
            Get
                Return _isReadOnly
            End Get
        End Property
#End Region

#Region "Properties"
        Private _degreeSign As String
        ''' <summary>Gets or sets a symbol for degree - typically degree sign (U+B0, °)</summary>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        ''' <seelaso cref="DefaultTypographicDegreeSign"/>
        Public Overridable Property DegreeSign As String
            Get
                Return _degreeSign
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _degreeSign Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _degreeSign Then _degreeSign = value
            End Set
        End Property

        Private _minuteSign As String
        ''' <summary>Gets or sets a symbol for minute - typically prime (U+2032, ′)</summary>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        ''' <seelaso cref="DefaultTypographicMinuteSign"/>
        Public Overridable Property MinuteSign As String
            Get
                Return _minuteSign
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _minuteSign Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _minuteSign Then _minuteSign = value
            End Set
        End Property

        Private _secondSign As String
        ''' <summary>Gets or sets a symbol for second - typically souble prime (U+2033, ″)</summary>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        ''' <seelaso cref="DefaultTypographicSecondSign"/>
        Public Overridable Property SecondSign As String
            Get
                Return _secondSign
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _secondSign Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _secondSign Then _secondSign = value
            End Set
        End Property

        Private _latitudeNorthShortSymbol As String
        ''' <summary>Gets or sets short symbol used for positive (north) latitude (e.g. "N" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        Public Overridable Property LatitudeNorthShortSymbol As String
            Get
                Return _latitudeNorthShortSymbol
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _latitudeNorthShortSymbol Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _latitudeNorthShortSymbol Then _latitudeNorthShortSymbol = value
            End Set
        End Property

        Private _latitudeNorthLongSymbol As String
        ''' <summary>Gets or sets long name used for positive (north) latitude (e.g. " North" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        Public Overridable Property LatitudeNorthLongSymbol As String
            Get
                Return _latitudeNorthLongSymbol
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _latitudeNorthLongSymbol Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _latitudeNorthLongSymbol Then _latitudeNorthLongSymbol = value
            End Set
        End Property

        Private _latitudeSouthShortSymbol As String
        ''' <summary>Gets or sets short symbol used for negative (south) latitude (e.g. "S" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        Public Overridable Property LatitudeSouthShortSymbol As String
            Get
                Return _latitudeSouthShortSymbol
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _latitudeSouthShortSymbol Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _latitudeSouthShortSymbol Then _latitudeSouthShortSymbol = value
            End Set
        End Property

        Private _latitudeSouthLongSymbol As String
        ''' <summary>Gets or long name symbol used for negative (south) latitude (e.g. " South" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        Public Overridable Property LatitudeSouthLongSymbol As String
            Get
                Return _latitudeSouthLongSymbol
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _latitudeSouthLongSymbol Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _latitudeSouthLongSymbol Then _latitudeSouthLongSymbol = value
            End Set
        End Property

        Private _longitudeEastShortSymbol As String
        ''' <summary>Gets or sets short symbol used for negative (east) longitude (e.g. "E" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        Public Overridable Property LongitudeEastShortSymbol As String
            Get
                Return _longitudeEastShortSymbol
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _longitudeEastShortSymbol Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _longitudeEastShortSymbol Then _longitudeEastShortSymbol = value
            End Set
        End Property

        Private _longitudeEastLongSymbol As String
        ''' <summary>Gets or sets long name used for negative (east) longitude (e.g. " East" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        Public Overridable Property LongitudeEastLongSymbol As String
            Get
                Return _longitudeEastLongSymbol
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _longitudeEastLongSymbol Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _longitudeEastLongSymbol Then _longitudeEastLongSymbol = value
            End Set
        End Property

        Private _longitudeWestShortSymbol As String
        ''' <summary>Gets or sets short symbol used for positive (west) longitude (e.g. "W" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        Public Overridable Property LongitudeWestShortSymbol As String
            Get
                Return _longitudeWestShortSymbol
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _longitudeWestShortSymbol Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _longitudeWestShortSymbol Then _longitudeWestShortSymbol = value
            End Set
        End Property

        Private _longitudeWestLongSymbol As String
        ''' <summary>Gets or sets llong name used for positive (west) longitude (e.g. " West" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        Public Overridable Property LongitudeWestLongSymbol As String
            Get
                Return _longitudeWestLongSymbol
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _longitudeWestLongSymbol Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _longitudeWestLongSymbol Then _longitudeWestLongSymbol = value
            End Set
        End Property

        Private _compatibilityDegreeSign As String
        ''' <summary>Gets or sets symbol used for degrees in compatibility rendering - typically degree sign (U+B0, °)</summary>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        ''' <seelaso cref="DefaultCompatibilityDegreeSign"/>
        Public Overridable Property CompatibilityDegreeSign As String
            Get
                Return _compatibilityDegreeSign
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _compatibilityDegreeSign Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _compatibilityDegreeSign Then _compatibilityDegreeSign = value
            End Set
        End Property

        Private _compatibilityMinuteSign As String
        ''' <summary>Gets or sets symbol used for minutes in compatibility rendering - typically apostrophe (U+27, ')</summary>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        ''' <seelaso cref="DefaultCompatibilityDegreeSign"/>
        Public Overridable Property CompatibilityMinuteSign As String
            Get
                Return _compatibilityMinuteSign
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _compatibilityMinuteSign Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _compatibilityMinuteSign Then _compatibilityMinuteSign = value
            End Set
        End Property

        Private _compatibilitySecondSign As String
        ''' <summary>Gets or sets symbol used for seconds in compatibility rendering - typically double quote (U+22, ")</summary>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value and <see cref="IsReadOnly"/> is true.</exception>
        ''' <seelaso cref="DefaultCompatibilityDegreeSign"/>
        Public Overridable Property CompatibilitySecondSign As String
            Get
                Return _compatibilitySecondSign
            End Get
            Set(value As String)
                If IsReadOnly AndAlso value <> _compatibilitySecondSign Then Throw New InvalidOperationException("This instance is read-only")
                If value <> _compatibilitySecondSign Then _compatibilitySecondSign = value
            End Set
        End Property
#End Region

#Region "Default"
        Private Shared ReadOnly _defaultInvariant As New AngleFormatInfo() With {
            .DegreeSign = DefaultTypographicDegreeSign, .MinuteSign = DefaultTypographicMinuteSign, .SecondSign = DefaultTypographicSecondSign,
            .LongitudeEastShortSymbol = "E", .LongitudeWestShortSymbol = "W", .LatitudeNorthShortSymbol = "N", .LatitudeSouthShortSymbol = "S",
            .LongitudeEastLongSymbol = " east", .LongitudeWestLongSymbol = " west", .LatitudeNorthLongSymbol = " north", .LatitudeSouthLongSymbol = " south",
            .CompatibilityDegreeSign = DefaultCompatibilityDegreeSign, .CompatibilityMinuteSign = DefaultCompatibilityMinuteSign, .CompatibilitySecondSign = DefaultCompatibilitySecondSign,
            ._isReadOnly = True
        }
        ''' <summary>Default instance of <see cref="AngleFormatInfo"/> suitable for invariant and English cultures</summary>
        ''' <remarks>This instance is read-only</remarks>
        Public Shared ReadOnly Property DefaultInvariant As AngleFormatInfo
            Get
                Return _defaultInvariant
            End Get
        End Property
#End Region
    End Class

    ''' <summary>Implements an <see cref="AngleFormatInfo"/> that loads it's data from resources</summary>
    ''' <remarks>Instances of this class are always read-only</remarks>
    Friend NotInheritable Class ResourcesBasedAngleFormatInfo
        Inherits AngleFormatInfo
        ''' <summary>Contains value of the <see cref="ResourceManager"/> property</summary>
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        ''' <summary>Culture to provide data for</summary>
        Private resourceCulture As Global.System.Globalization.CultureInfo

        '''<summary>Returns the cached <see cref="ResourceManager"/> instance used by this class.</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Tools.GlobalizationT.AngleFormatInfo", GetType(AngleFormatInfo).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        ''' <summary>CTor - creates a new instance of the <see cref="ResourcesBasedAngleFormatInfo"/> for given culture</summary>
        ''' <param name="culture">Culture to use</param>
        ''' <exception cref="ArgumentNullException"><paramref name="culture"/> is null</exception>
        Public Sub New(culture As CultureInfo)
            If culture Is Nothing Then Throw New ArgumentNullException("culture")
            Me.resourceCulture = culture
            MakeReadOnly()
        End Sub
        ''' <summary>Gets symbol used for degrees in compatibility rendering - typically degree sign (U+B0, °)</summary>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        ''' <seelaso cref="DefaultCompatibilityDegreeSign"/>
        Public Overrides Property CompatibilityDegreeSign As String
            Get
                Return If(ResourceManager.GetString("CompatibilityDegreeSign", Me.resourceCulture), DefaultInvariant.CompatibilityDegreeSign)
            End Get
            Set(value As String)
                MyBase.CompatibilityDegreeSign = value
            End Set
        End Property
        ''' <summary>Gets symbol used for minutes in compatibility rendering - typically apostrophe (U+27, ')</summary>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        ''' <seelaso cref="DefaultCompatibilityDegreeSign"/>
        Public Overrides Property CompatibilityMinuteSign As String
            Get
                Return If(ResourceManager.GetString("CompatibilityMinuteSign", Me.resourceCulture), DefaultInvariant.CompatibilityMinuteSign)
            End Get
            Set(value As String)
                MyBase.CompatibilityMinuteSign = value
            End Set
        End Property
        ''' <summary>Gets symbol used for seconds in compatibility rendering - typically double quote (U+22, ")</summary>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        ''' <seelaso cref="DefaultCompatibilityDegreeSign"/>
        Public Overrides Property CompatibilitySecondSign As String
            Get
                Return If(ResourceManager.GetString("CompatibilitySecondSign", Me.resourceCulture), DefaultInvariant.CompatibilitySecondSign)
            End Get
            Set(value As String)
                MyBase.CompatibilitySecondSign = value
            End Set
        End Property
        ''' <summary>Gets a symbol for degree - typically degree sign (U+B0, °)</summary>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        ''' <seelaso cref="DefaultTypographicDegreeSign"/>
        Public Overrides Property DegreeSign As String
            Get
                Return If(ResourceManager.GetString("DegreeSign", Me.resourceCulture), DefaultInvariant.DegreeSign)
            End Get
            Set(value As String)
                MyBase.DegreeSign = value
            End Set
        End Property
        ''' <summary>Gets long name used for positive (north) latitude (e.g. " North" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        Public Overrides Property LatitudeNorthLongSymbol As String
            Get
                Return If(ResourceManager.GetString("LatitudeNorthLongSymbol", Me.resourceCulture), DefaultInvariant.LatitudeNorthLongSymbol)
            End Get
            Set(value As String)
                MyBase.LatitudeNorthLongSymbol = value
            End Set
        End Property
        ''' <summary>Gets short symbol used for positive (north) latitude (e.g. "N" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        Public Overrides Property LatitudeNorthShortSymbol As String
            Get
                Return If(ResourceManager.GetString("LatitudeNorthShortSymbol", Me.resourceCulture), DefaultInvariant.LatitudeNorthShortSymbol)
            End Get
            Set(value As String)
                MyBase.LatitudeNorthShortSymbol = value
            End Set
        End Property
        ''' <summary>Gets or long name symbol used for negative (south) latitude (e.g. " South" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        Public Overrides Property LatitudeSouthLongSymbol As String
            Get
                Return If(ResourceManager.GetString("LatitudeSouthLongSymbol", Me.resourceCulture), DefaultInvariant.LatitudeSouthLongSymbol)
            End Get
            Set(value As String)
                MyBase.LatitudeSouthLongSymbol = value
            End Set
        End Property
        ''' <summary>Gets long name used for negative (east) longitude (e.g. " East" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        Public Overrides Property LongitudeEastLongSymbol As String
            Get
                Return If(ResourceManager.GetString("LongitudeEastLongSymbol", Me.resourceCulture), DefaultInvariant.LongitudeEastLongSymbol)
            End Get
            Set(value As String)
                MyBase.LongitudeEastLongSymbol = value
            End Set
        End Property
        ''' <summary>Gets short symbol used for negative (south) latitude (e.g. "S" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        Public Overrides Property LatitudeSouthShortSymbol As String
            Get
                Return If(ResourceManager.GetString("LatitudeSouthShortSymbol", Me.resourceCulture), DefaultInvariant.LatitudeSouthShortSymbol)
            End Get
            Set(value As String)
                MyBase.LatitudeSouthShortSymbol = value
            End Set
        End Property
        ''' <summary>Gets short symbol used for negative (east) longitude (e.g. "E" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        Public Overrides Property LongitudeEastShortSymbol As String
            Get
                Return If(ResourceManager.GetString("LongitudeEastShortSymbol", Me.resourceCulture), DefaultInvariant.LongitudeEastShortSymbol)
            End Get
            Set(value As String)
                MyBase.LongitudeEastShortSymbol = value
            End Set
        End Property
        ''' <summary>Gets llong name used for positive (west) longitude (e.g. " West" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        Public Overrides Property LongitudeWestLongSymbol As String
            Get
                Return If(ResourceManager.GetString("LongitudeWestLongSymbol", Me.resourceCulture), DefaultInvariant.LongitudeWestLongSymbol)
            End Get
            Set(value As String)
                MyBase.LongitudeWestLongSymbol = value
            End Set
        End Property
        ''' <summary>Gets short symbol used for positive (west) longitude (e.g. "W" in English)</summary>
        ''' <remarks>If value of this property starts with space formatting alghoritm ensures space before a symbol in all cases.</remarks>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        Public Overrides Property LongitudeWestShortSymbol As String
            Get
                Return If(ResourceManager.GetString("LongitudeWestShortSymbol", Me.resourceCulture), DefaultInvariant.LongitudeWestShortSymbol)
            End Get
            Set(value As String)
                MyBase.LongitudeWestShortSymbol = value
            End Set
        End Property
        ''' <summary>Gets a symbol for minute - typically prime (U+2032, ′)</summary>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        ''' <seelaso cref="DefaultTypographicMinuteSign"/>
        Public Overrides Property MinuteSign As String
            Get
                Return If(ResourceManager.GetString("MinuteSign", Me.resourceCulture), DefaultInvariant.MinuteSign)
            End Get
            Set(value As String)
                MyBase.MinuteSign = value
            End Set
        End Property
        ''' <summary>Gets a symbol for second - typically souble prime (U+2033, ″)</summary>
        ''' <exception cref="InvalidOperationException">Property is being set to a different value (<see cref="IsReadOnly"/> is always true).</exception>
        ''' <seelaso cref="DefaultTypographicSecondSign"/>
        Public Overrides Property SecondSign As String
            Get
                Return If(ResourceManager.GetString("SecondSign", Me.resourceCulture), DefaultInvariant.SecondSign)
            End Get
            Set(value As String)
                MyBase.SecondSign = value
            End Set
        End Property

    End Class
End Namespace
#End If
