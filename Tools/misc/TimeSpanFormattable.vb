Imports System.Text, System.Math, Tools.ExtensionsT, System.Linq
#If Config <= rc Then 'Stage:RC
''' <summary><see cref="TimeSpan"/> that implements <see cref="IFormattable"/></summary>
''' <remarks>This class has plenty of formating possibilities which can be used via <see cref="String.Format"/> or <see cref="TimeSpanFormattable.ToString"/>.</remarks>
''' <seealso cref="TimeSpan"/>
''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
''' <version version="1.5.2" stage="RC"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
<DebuggerDisplay("{ToString}")> _
Public Structure TimeSpanFormattable
    Implements IComparable(Of TimeSpanFormattable), IEquatable(Of TimeSpanFormattable)
    Implements IFormattable
    ''' <summary>Internal <see cref="TimeSpan"/></summary>
    Private Inner As TimeSpan
#Region "CTors"
    ''' <summary>Copy CTor</summary>
    ''' <param name="a">Instance to be used to initialize a new instance</param>
    Public Sub New(ByVal a As TimeSpanFormattable)
        Me.New(a.Inner)
    End Sub
    ''' <summary>Initializes a new <see cref="TimeSpanFormattable"/> to a specified number of hours, minutes, and seconds.</summary>
    ''' <param name="seconds">Number of seconds.</param>
    ''' <param name="hours">Number of hours.</param>
    ''' <param name="minutes">Number of minutes.</param>
    ''' <exception cref="System.ArgumentOutOfRangeException">The parameters specify a <see cref="TimeSpanFormattable"/> value less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Sub New(ByVal hours As Integer, ByVal minutes As Integer, ByVal seconds As Integer)
        Me.New(New TimeSpan(hours, minutes, seconds))
    End Sub
    ''' <summary>Initializes a new <see cref="TimeSpanFormattable"/> to a specified number of days, hours, minutes, seconds, and milliseconds.</summary>
    ''' <param name="seconds">Number of seconds.</param>
    ''' <param name="hours">Number of hours.</param>
    ''' <param name="minutes">Number of minutes.</param>
    ''' <param name="days">Number of days.</param>
    ''' <param name="milliseconds">Number of milliseconds.</param>
    ''' <exception cref="System.ArgumentOutOfRangeException">The parameters specify a <see cref="TimeSpanFormattable"/> value less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Sub New(ByVal days As Integer, ByVal hours As Integer, ByVal minutes As Integer, ByVal seconds As Integer, Optional ByVal milliseconds As Integer = 0)
        Me.New(New TimeSpan(days, hours, minutes, seconds, milliseconds))
    End Sub
    ''' <summary>Initializes a new <see cref="TimeSpanFormattable"/> to the specified number of ticks.</summary>
    ''' <param name="ticks">A time period expressed in 100-nanosecond units.</param>
    Public Sub New(ByVal ticks As Long)
        Me.New(New TimeSpan(ticks))
    End Sub
    ''' <summary>Returns a <see cref="TimeSpanFormattable"/> that represents a specified number of days, where the specification is accurate to the nearest millisecond.</summary>
    ''' <param name="value">A number of days, accurate to the nearest millisecond.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> that represents <paramref name="value"/>.</returns>
    ''' <exception cref="System.ArgumentException"><paramref name="value"/> is equal to <see cref="System.Double.NaN"/>.</exception>
    ''' <exception cref="System.OverflowException"><paramref name="value"/> is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Shared Function FromDays(ByVal value As Double) As TimeSpanFormattable
        Return New TimeSpanFormattable(TimeSpan.FromDays(value))
    End Function
    ''' <summary>Returns a <see cref="TimeSpanFormattable"/> that represents a specified number of hours, where the specification is accurate to the nearest millisecond.</summary>
    ''' <param name="value">A number of hours, accurate to the nearest millisecond.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> that represents <paramref name="value"/>.</returns>
    ''' <exception cref="System.ArgumentException"><paramref name="value"/> is equal to <see cref="System.Double.NaN"/>.</exception>
    ''' <exception cref="System.OverflowException"><paramref name="value"/> is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Shared Function FromHours(ByVal value As Double) As TimeSpanFormattable
        Return New TimeSpanFormattable(TimeSpan.FromHours(value))
    End Function
    ''' <summary>Returns a <see cref="TimeSpanFormattable"/> that represents a specified number of minutes, where the specification is accurate to the nearest millisecond.</summary>
    ''' <param name="value">A number of minutes, accurate to the nearest millisecond.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> that represents <paramref name="value"/>.</returns>
    ''' <exception cref="System.ArgumentException"><paramref name="value"/> is equal to <see cref="System.Double.NaN"/>.</exception>
    ''' <exception cref="System.OverflowException"><paramref name="value"/> is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Shared Function FromMinutes(ByVal value As Double) As TimeSpanFormattable
        Return New TimeSpanFormattable(TimeSpan.FromMinutes(value))
    End Function
    ''' <summary>Returns a <see cref="TimeSpanFormattable"/> that represents a specified number of seconds, where the specification is accurate to the nearest millisecond.</summary>
    ''' <param name="value">A number of seconds, accurate to the nearest millisecond.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> that represents <paramref name="value"/>.</returns>
    ''' <exception cref="System.ArgumentException"><paramref name="value"/> is equal to <see cref="System.Double.NaN"/>.</exception>
    ''' <exception cref="System.OverflowException"><paramref name="value"/> is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Shared Function FromSeconds(ByVal value As Double) As TimeSpanFormattable
        Return New TimeSpanFormattable(TimeSpan.FromSeconds(value))
    End Function
    ''' <summary>Returns a <see cref="TimeSpanFormattable"/> that represents a specified number of milliseconds.</summary>
    ''' <param name="value">A number of milliseconds.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> that represents <paramref name="value"/>.</returns>
    ''' <exception cref="System.ArgumentException"><paramref name="value"/> is equal to <see cref="System.Double.NaN"/>.</exception>
    ''' <exception cref="System.OverflowException"><paramref name="value"/> is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Shared Function FromMillseconds(ByVal value As Double) As TimeSpanFormattable
        Return New TimeSpanFormattable(TimeSpan.FromMilliseconds(value))
    End Function
    ''' <summary>Returns a <see cref="TimeSpanFormattable"/> that represents a specified time, where the specification is in units of ticks.</summary>
    ''' <param name="value">A number of ticks that represent a time.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> with a value of <paramref name="value"/>.</returns>
    Public Shared Function FromTicks(ByVal value As Long) As TimeSpanFormattable
        Return New TimeSpanFormattable(TimeSpan.FromTicks(value))
    End Function
    ''' <summary>Constructs a new <see cref="TimeSpanFormattable"/> object from a time interval specified in a string.</summary>
    ''' <param name="s">A string that specifies a time interval.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> that corresponds to <paramref name="s"/>.</returns>
    ''' <exception cref="System.FormatException"><paramref name="s"/> has an invalid format.</exception>
    ''' <exception cref="System.ArgumentNullException"><paramref name="s"/> is null.</exception>
    ''' <exception cref="System.OverflowException"><paramref name="s"/> represents a number less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.-or- At least one of the days, hours, minutes, or seconds components is outside its valid range.</exception>
    Public Shared Function Parse(ByVal s As String) As TimeSpanFormattable
        Return New TimeSpanFormattable(TimeSpan.Parse(s))
    End Function
    ''' <summary>Constructs a new <see cref="TimeSpanFormattable"/> object from a time interval specified in a string. Parameters specify the time interval and the variable where the new <see cref="TimeSpanFormattable"/> object is returned.</summary>
    ''' <param name="s">A string that specifies a time interval.</param>
    ''' <param name="result">When this method returns, contains an object that represents the time interval specified by s, or <see cref="System.TimeSpan.Zero"/> if the conversion failed. This parameter is passed uninitialized.</param>
    ''' <returns>true if s was converted successfully; otherwise, false. This operation returns false if the s parameter is null, has an invalid format,represents a time interval less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>, or has at least one days, hours, minutes, or seconds component outside its valid range.</returns>
    Public Shared Function TryParse(ByVal s As String, ByRef result As TimeSpanFormattable) As Boolean
        Return TimeSpan.TryParse(s, result.Inner)
    End Function
    ''' <summary>Constructs a new <see cref="TimeSpanFormattable"/> object from a time interval specified in a string. Parameters specify the time interval and the variable where the new <see cref="TimeSpanFormattable"/> object is returned.</summary>
    ''' <param name="s">A string that specifies a time interval.</param>
    ''' <param name="result">When this method returns, contains an object that represents the time interval specified by s, or <see cref="System.TimeSpan.Zero"/> if the conversion failed. This parameter is passed uninitialized.</param>
    ''' <returns>true if s was converted successfully; otherwise, false. This operation returns false if the s parameter is null, has an invalid format,represents a time interval less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>, or has at least one days, hours, minutes, or seconds component outside its valid range.</returns>
    Public Shared Function TryParse(ByVal s As String, ByVal formatProvider As IFormatProvider, ByRef result As TimeSpanFormattable) As Boolean
        Return TimeSpan.TryParse(s, formatProvider, result.Inner)
    End Function
#End Region
#Region "Properties"
    ''' <summary>Gets the number of ticks that represent the value of the current <see cref="TimeSpanFormattable"/> structure.</summary>
    ''' <returns>Value of <see cref="TimeSpan.Ticks"/> of internal <see cref="TimeSpan"/></returns>
    Public ReadOnly Property Ticks() As Long
        Get
            Return Inner.Ticks
        End Get
    End Property
    ''' <summary>Gets the number of whole days represented by the current <see cref="TimeSpanFormattable"/> structure.</summary>
    ''' <returns>Value of <see cref="TimeSpan.Days"/> of internal <see cref="TimeSpan"/>. The return value can be positive or negative.</returns>
    Public ReadOnly Property Days() As Integer
        Get
            Return Inner.Days
        End Get
    End Property
    ''' <summary>Gets the number of whole hours represented by the current<see cref="TimeSpanFormattable"/> structure.</summary>
    ''' <returns>Value of <see cref="TimeSpan.Hours"/> of internal <see cref="TimeSpan"/>. The return value ranges from -23 through 23.</returns>
    Public ReadOnly Property Hours() As Integer
        Get
            Return Inner.Hours
        End Get
    End Property
    ''' <summary>Gets the number of whole milliseconds represented by the current <see cref="TimeSpanFormattable"/> structure.</summary>
    ''' <returns>Value of <see cref="TimeSpan.Milliseconds"/> of internal <see cref="TimeSpan"/>. The return value ranges from -999 through 999.</returns>
    Public ReadOnly Property Milliseconds() As Integer
        Get
            Return Inner.Milliseconds
        End Get
    End Property
    ''' <summary>Gets the number of whole minutes represented by the current <see cref="TimeSpanFormattable"/> structure.</summary>
    ''' <returns>Value of <see cref="TimeSpan.Minutes"/> of internal <see cref="TimeSpan"/>. The return value ranges from -59 through 59.</returns>
    Public ReadOnly Property Minutes() As Integer
        Get
            Return Inner.Minutes
        End Get
    End Property
    ''' <summary>Gets the number of whole seconds represented by the current <see cref="TimeSpanFormattable"/> structure.</summary>
    ''' <returns>Value of <see cref="TimeSpan.Seconds"/> of internal <see cref="TimeSpan"/>. The return value ranges from -59 through 59.</returns>
    Public ReadOnly Property Seconds() As Integer
        Get
            Return Inner.Seconds
        End Get
    End Property
    ''' <summary>Gets the value of the current <see cref="TimeSpanFormattable"/> structure expressed in whole and fractional days.</summary>
    ''' <returns>Value of <see cref="TimeSpan.TotalDays"/> of internal <see cref="TimeSpan"/></returns>
    Public ReadOnly Property TotalDays() As Double
        Get
            Return Inner.TotalDays
        End Get
    End Property
    ''' <summary>Gets the value of the current <see cref="TimeSpanFormattable"/> structure expressed in whole and fractional hours.</summary>
    ''' <returns>Value of <see cref="TimeSpan.TotalHours"/> of internal <see cref="TimeSpan"/></returns>
    Public ReadOnly Property TotalHours() As Double
        Get
            Return Inner.TotalHours
        End Get
    End Property
    ''' <summary>Gets the value of the current <see cref="TimeSpanFormattable"/> structure expressed in whole and fractional milliseconds.</summary>
    ''' <returns>Value of <see cref="TimeSpan.TotalMilliseconds"/> of internal <see cref="TimeSpan"/></returns>
    Public ReadOnly Property TotalMilliseconds() As Double
        Get
            Return Inner.TotalMilliseconds
        End Get
    End Property
    ''' <summary>Gets the value of the current <see cref="TimeSpanFormattable"/> structure expressed in whole and fractional minutes.</summary>
    ''' <returns>Value of <see cref="TimeSpan.TotalMinutes"/> of internal <see cref="TimeSpan"/></returns>
    Public ReadOnly Property TotalMinutes() As Double
        Get
            Return Inner.TotalMinutes
        End Get
    End Property
    ''' <summary>Gets the value of the current <see cref="TimeSpanFormattable"/> structure expressed in whole and fractional seconds.</summary>
    ''' <returns>Value of <see cref="TimeSpan.TotalSeconds"/> of internal <see cref="TimeSpan"/></returns>
    Public ReadOnly Property TotalSeconds() As Double
        Get
            Return Inner.TotalSeconds
        End Get
    End Property
#End Region
#Region "Operators"
    ''' <summary>Constructs a new <see cref="TimeSpanFormattable"/> object from a time interval specified in a string.</summary>
    ''' <param name="s">A string that specifies a time interval.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> that corresponds to <paramref name="s"/>.</returns>
    ''' <exception cref="System.FormatException"><paramref name="s"/> has an invalid format.</exception>
    ''' <exception cref="System.ArgumentNullException"><paramref name="s"/> is null.</exception>
    ''' <exception cref="System.OverflowException"><paramref name="s"/> represents a number less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.-or- At least one of the days, hours, minutes, or seconds components is outside its valid range.</exception>
    Public Shared Narrowing Operator CType(ByVal s As String) As TimeSpanFormattable
        Return Parse(s)
    End Operator
    ''' <summary>Converts <see cref="TimeSpanFormattable"/> to <see cref="String"/></summary>
    ''' <param name="a">A <see cref="TimeSpanFormattable"/></param>
    ''' <returns><see cref="ToString"/></returns>
    ''' <version version="1.5.2">Operator introduced</version>
    Public Shared Widening Operator CType(ByVal a As TimeSpanFormattable) As String
        Return a.ToString
    End Operator
    ''' <summary>Returns a new <see cref="TimeSpanFormattable"/> object whose value is the absolute value of the current <see cref="TimeSpanFormattable"/> object.</summary>
    ''' <returns>A new <see cref="TimeSpanFormattable"/> whose value is the absolute value of the current <see cref="TimeSpanFormattable"/> object.</returns>
    ''' <exception cref="System.OverflowException">The value of this instance is <see cref="System.TimeSpan.MinValue"/>.</exception>
    Public Function Duration() As TimeSpanFormattable
        Return New TimeSpanFormattable(Me.Inner.Duration)
    End Function
    ''' <summary>Compares this instance to a specified <see cref="TimeSpanFormattable"/> object and returns an indication of their relative values.</summary>
    ''' <param name="other">A <see cref="TimeSpanFormattable"/> object to compare to this instance.</param>
    ''' <returns>A signed number indicating the relative values of this instance and <paramref name="other"/>. Value Description: A negative integer This instance is less than <paramref name="other"/>. Zero This instance is equal to <paramref name="other"/>. A positive integer This instance is greater than <paramref name="other"/>.</returns>
    Public Function CompareTo(ByVal other As TimeSpanFormattable) As Integer Implements System.IComparable(Of TimeSpanFormattable).CompareTo
        Return Me.Inner.CompareTo(other.Inner)
    End Function
    ''' <summary>Converts <see cref="TimeSpan"/> into <see cref="TimeSpanFormattable"/></summary>
    ''' <param name="a"><see cref="TimeSpan"/> to be converted</param>
    ''' <returns><see cref="TimeSpanFormattable"/> with the same value as <paramref name="a"/></returns>
    Public Shared Widening Operator CType(ByVal a As TimeSpan) As TimeSpanFormattable
        Return New TimeSpanFormattable(a)
    End Operator
    ''' <summary>CTor from <see cref="TimeSpan"/></summary>
    ''' <param name="a"><see cref="TimeSpan"/> to initialize value of newly created instance with</param>
    Public Sub New(ByVal a As TimeSpan)
        Me.Inner = a
    End Sub
    ''' <summary>Converts <see cref="TimeSpanFormattable"/> into <see cref="TimeSpan"/></summary>
    ''' <param name="a"><see cref="TimeSpanFormattable"/> to be converted</param>
    ''' <returns><see cref="TimeSpan"/> with same value as <paramref name="a"/></returns>
    Public Shared Widening Operator CType(ByVal a As TimeSpanFormattable) As TimeSpan
        Return a.Inner
    End Operator
    ''' <summary>Returns a <see cref="TimeSpanFormattable"/> whose value is the negated value of the specified instance.</summary>
    ''' <param name="t">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> with the same numeric value as this instance, but the opposite sign.</returns>
    ''' <exception cref="System.OverflowException">The negated value of this instance cannot be represented by a <see cref="System.TimeSpan"/>; that is, the value of this instance is <see cref="System.TimeSpan.MinValue"/>.</exception>
    Public Shared Operator -(ByVal t As TimeSpanFormattable) As TimeSpanFormattable
        Return -t.Inner
    End Operator
    ''' <summary>Subtracts a specified <see cref="TimeSpanFormattable"/> from another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> whose value is the result of the value of <paramref name="t1"/> minus the value of <paramref name="t2"/>.</returns>
    ''' <exception cref="System.OverflowException">The return value is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Shared Operator -(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As TimeSpanFormattable
        Return t1.Inner - t2.Inner
    End Operator
    ''' <summary>Subtracts a specified <see cref="TimeSpanFormattable"/> from another specified <see cref="TimeSpan"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpan"/>.</param>
    ''' <returns>A <see cref="TimeSpan"/> whose value is the result of the value of <paramref name="t1"/> minus the value of <paramref name="t2"/>.</returns>
    ''' <exception cref="System.OverflowException">The return value is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Shared Operator -(ByVal t1 As TimeSpan, ByVal t2 As TimeSpanFormattable) As TimeSpan
        Return t1 - t2.Inner
    End Operator
    ''' <summary>Subtracts a specified <see cref="TimeSpan"/> from another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpan"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> whose value is the result of the value of <paramref name="t1"/> minus the value of <paramref name="t2"/>.</returns>
    ''' <exception cref="System.OverflowException">The return value is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Shared Operator -(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpan) As TimeSpanFormattable
        Return t1.Inner - t2
    End Operator
    ''' <summary>Returns the specified instance of <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>Returns <paramref name="t"/>.</returns>
    Public Shared Operator +(ByVal t As TimeSpanFormattable) As TimeSpanFormattable
        Return +t.Inner
    End Operator
    ''' <summary>Adds two specified <see cref="TimeSpanFormattable"/> instances.</summary>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> whose value is the sum of the values of <paramref name="t1"/> and <paramref name="t2"/>.</returns>
    ''' <exception cref="System.OverflowException">The resulting <see cref="TimeSpanFormattable"/> is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Shared Operator +(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As TimeSpanFormattable
        Return t1.Inner + t2.Inner
    End Operator
    ''' <summary>Adds specified <see cref="TimeSpan"/> to another <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpan"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> whose value is the sum of the values of <paramref name="t1"/> and <paramref name="t2"/>.</returns>
    ''' <exception cref="System.OverflowException">The resulting <see cref="TimeSpanFormattable"/> is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Shared Operator +(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpan) As TimeSpanFormattable
        Return t1.Inner + t2
    End Operator
    ''' <summary>Adds specified <see cref="TimeSpanFormattable"/> to another <see cref="TimeSpan"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpan"/>.</param>
    ''' <returns>A <see cref="TimeSpanFormattable"/> whose value is the sum of the values of <paramref name="t1"/> and <paramref name="t2"/>.</returns>
    ''' <exception cref="System.OverflowException">The resulting <see cref="TimeSpan"/> is less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/>.</exception>
    Public Shared Operator +(ByVal t1 As TimeSpan, ByVal t2 As TimeSpanFormattable) As TimeSpan
        Return t1 + t2.Inner
    End Operator
#Region "<=>"
#Region "TimeSpanFormattable, TimeSpanFormattable"
    ''' <summary>Indicates whether two <see cref="TimeSpanFormattable"/> instances are equal.</summary>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the values of <paramref name="t1"/> and <paramref name="t2"/> are equal; otherwise, false.</returns>
    Public Shared Operator =(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1.Inner = t2.Inner
    End Operator
    ''' <summary>Indicates whether two <see cref="TimeSpanFormattable"/> instances are not equal.</summary>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the values of <paramref name="t1"/> and <paramref name="t2"/> are not equal; otherwise, false.</returns>
    Public Shared Operator <>(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1.Inner <> t2.Inner
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpanFormattable"/> is less than another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is less than the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator <(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1.Inner < t2.Inner
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpanFormattable"/> is less than or equal to another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is less than or equal to the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator <=(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1.Inner <= t2.Inner
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpanFormattable"/> is greater than another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is greater than the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator >(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1.Inner > t2.Inner
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpanFormattable"/> is greater than or equal to another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is greater than or equal to the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator >=(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1.Inner >= t2.Inner
    End Operator
#End Region
#Region "TimeSpan, TimeSpanFormattable"
    ''' <summary>Indicates whether <see cref="TimeSpan"/> and <see cref="TimeSpanFormattable"/> instances are equal.</summary>
    ''' <param name="t1">A <see cref="timespan"/>.</param>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the values of <paramref name="t1"/> and <paramref name="t2"/> are equal; otherwise, false.</returns>
    Public Shared Operator =(ByVal t1 As TimeSpan, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1 = t2.Inner
    End Operator
    ''' <summary>Indicates whether <see cref="TimeSpan"/> and <see cref="TimeSpanFormattable"/> instances are not equal.</summary>
    ''' <param name="t1">A <see cref="timespan"/>.</param>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the values of <paramref name="t1"/> and <paramref name="t2"/> are not equal; otherwise, false.</returns>
    Public Shared Operator <>(ByVal t1 As TimeSpan, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1 <> t2.Inner
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpan"/> is less than another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t1">A <see cref="TimeSpan"/>.</param>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is less than the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator <(ByVal t1 As TimeSpan, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1 < t2.Inner
    End Operator
    ''' <summary>Indicates whether a specified <see cref="Timespan"/> is less than or equal to another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t1">A <see cref="timespan"/>.</param>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is less than or equal to the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator <=(ByVal t1 As TimeSpan, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1 <= t2.Inner
    End Operator
    ''' <summary>Indicates whether a specified <see cref="timespan"/> is greater than another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t1">A <see cref="timespan"/>.</param>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is greater than the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator >(ByVal t1 As TimeSpan, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1 > t2.Inner
    End Operator
    ''' <summary>Indicates whether a specified <see cref="timespan"/> is greater than or equal to another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t1">A <see cref="timespan"/>.</param>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is greater than or equal to the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator >=(ByVal t1 As TimeSpan, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1 >= t2.Inner
    End Operator
#End Region
#Region "TimeSpanFormattable, TimeSpan"
    ''' <summary>Indicates whether <see cref="TimeSpanFormattable"/> and <see cref="TimeSpan"/> instances are equal.</summary>
    ''' <param name="t2">A <see cref="timespan"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the values of <paramref name="t1"/> and <paramref name="t2"/> are equal; otherwise, false.</returns>
    Public Shared Operator =(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpan) As Boolean
        Return t1.Inner = t2
    End Operator
    ''' <summary>Indicates whether  <see cref="TimeSpanFormattable"/> nad <see cref="TimeSpan"/> instances are not equal.</summary>
    ''' <param name="t2">A <see cref="TimeSpan"/>"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the values of <paramref name="t1"/> and <paramref name="t2"/> are not equal; otherwise, false.</returns>
    Public Shared Operator <>(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpan) As Boolean
        Return t1.Inner <> t2
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpanFormattable"/> is less than another specified <see cref="TimeSpan"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpan"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is less than the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator <(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpan) As Boolean
        Return t1.Inner < t2
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpanFormattable"/> is less than or equal to another specified <see cref="TimeSpan"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpan"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is less than or equal to the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator <=(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpan) As Boolean
        Return t1.Inner <= t2
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpanFormattable"/> is greater than another specified <see cref="TimeSpan"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpan"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is greater than the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator >(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpan) As Boolean
        Return t1.Inner > t2
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpanFormattable"/> is greater than or equal to another specified <see cref="TimeSpan"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpan"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is greater than or equal to the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator >=(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpan) As Boolean
        Return t1.Inner >= t2
    End Operator
#End Region
#End Region
#Region "*"
    ''' <summary>Multiplies given <see cref="TimeSpanFormattable"/> with given multiplier</summary>
    ''' <param name="Time">A <see cref="TimeSpanFormattable"/></param>
    ''' <param name="multiplier">Multiplier</param>
    ''' <returns><paramref name="Time"/> multiplied <paramref name="multiplier"/> times. Accurancy of theis operation is milliseconds</returns>
    ''' <exception cref="OverflowException">Resulting value is up to be less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/></exception>
    Public Shared Operator *(ByVal multiplier As Integer, ByVal Time As TimeSpanFormattable) As TimeSpanFormattable
        Return TimeSpan.FromMilliseconds(Time.TotalMilliseconds * multiplier)
    End Operator
    ''' <summary>Multiplies given <see cref="TimeSpanFormattable"/> with given multiplier</summary>
    ''' <param name="Time">A <see cref="TimeSpanFormattable"/></param>
    ''' <param name="multiplier">Multiplier</param>
    ''' <returns><paramref name="Time"/> multiplied <paramref name="multiplier"/> times. Accurancy of theis operation is milliseconds</returns>
    ''' <exception cref="OverflowException">Resulting value is up to be less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/></exception>
    Public Shared Operator *(ByVal Time As TimeSpanFormattable, ByVal multiplier As Integer) As TimeSpanFormattable
        Return TimeSpan.FromMilliseconds(Time.TotalMilliseconds * multiplier)
    End Operator
    ''' <summary>Multiplies given <see cref="TimeSpanFormattable"/> with given multiplier</summary>
    ''' <param name="Time">A <see cref="TimeSpanFormattable"/></param>
    ''' <param name="multiplier">Multiplier</param>
    ''' <returns><paramref name="Time"/> multiplied <paramref name="multiplier"/> times. Accurancy of theis operation is milliseconds</returns>
    ''' <exception cref="OverflowException">Resulting value is up to be less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/></exception>
    Public Shared Operator *(ByVal multiplier As Double, ByVal Time As TimeSpanFormattable) As TimeSpanFormattable
        If multiplier.IsInfinity Then Throw New OverflowException(String.Format(ResourcesT.Exceptions.WasInfinity, "multiplier"))
        If multiplier.IsNaN Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.WasNaN, "multiplier"), "multiplier")
        Return TimeSpan.FromMilliseconds(Time.TotalMilliseconds * multiplier)
    End Operator
    ''' <summary>Multiplies given <see cref="TimeSpanFormattable"/> with given multiplier</summary>
    ''' <param name="Time">A <see cref="TimeSpanFormattable"/></param>
    ''' <param name="multiplier">Multiplier</param>
    ''' <returns><paramref name="Time"/> multiplied <paramref name="multiplier"/> times. Accurancy of theis operation is milliseconds</returns>
    ''' <exception cref="OverflowException">Resulting value is up to be less than <see cref="System.TimeSpan.MinValue"/> or greater than <see cref="System.TimeSpan.MaxValue"/></exception>
    Public Shared Operator *(ByVal Time As TimeSpanFormattable, ByVal multiplier As Double) As TimeSpanFormattable
        If multiplier.IsInfinity Then Throw New OverflowException(String.Format(ResourcesT.Exceptions.WasInfinity, "multiplier"))
        If multiplier.IsNaN Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.WasNaN, "multiplier"), "multiplier")
        Return TimeSpan.FromMilliseconds(Time.TotalMilliseconds * multiplier)
    End Operator
#End Region
    'TODO:Operator /
    ''' <summary>Indicates whether the current <see cref="TimeSpanFormattable"/> is equal to another <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="other">A <see cref="TimeSpanFormattable"/> to compare with this object.</param>
    ''' <returns>true if the current <see cref="TimeSpanFormattable"/> is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
    Public Overloads Function Equals(ByVal other As TimeSpanFormattable) As Boolean Implements System.IEquatable(Of TimeSpanFormattable).Equals
        Return Me = other
    End Function
    ''' <summary>Returns a value indicating whether this instance is equal to a specified <see cref="TimeSpanFormattable"/> object.</summary>
    ''' <param name="obj">An <see cref="TimeSpanFormattable"/> or <see cref="TimeSpan"/> object to compare with this instance.</param>
    ''' <returns>true if obj represents the same time interval as this instance; otherwise, false.</returns>
    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf obj Is TimeSpanFormattable Then
            Return Me = DirectCast(obj, TimeSpanFormattable)
        ElseIf TypeOf obj Is TimeSpan Then
            Return Inner = DirectCast(obj, TimeSpan)
        Else
            Return Inner.Equals(obj)
        End If
    End Function
    ''' <summary>Returns a hash code for this instance.</summary>
    ''' <returns>A 32-bit signed integer hash code.</returns>
    Public Overrides Function GetHashCode() As Integer
        Return Me.Inner.GetHashCode
    End Function
    ''' <summary>Returns the string representation of the value of this instance.</summary>
    ''' <returns>A string that represents the value of this instance. The return value is of the form: [-][d.]hh:mm:ss[.ff] Items in square brackets ([ and ]) are optional, colons and periods (: and.) are literal characters; and the other items are as follows.Item Description "-" optional minus sign indicating a negative time "d" optional days "hh" hours, ranging from 0 to 23 "mm" minutes, ranging from 0 to 59 "ss" seconds, ranging from 0 to 59 "ff" optional fractional seconds, from 1 to 7 decimal digits For more information about comparing the string representation of <see cref="System.TimeSpan"/> and Oracle data types, see article Q324577, "<see cref="System.TimeSpan"/> Does Not Match Oracle 9i INTERVAL DAY TO SECOND Data Type," in the Microsoft Knowledge Base at <see>http://support.microsoft.com</see></returns>
    Public Overrides Function ToString() As String
        Return Me.Inner.ToString
    End Function
#End Region
#Region "Formating"
    ''' <summary>Formats the value of the current instance using the specified format.</summary>
    ''' <param name="format">The <see cref="System.String"/> specifying the format to use.-or- null to use the default format defined for the type of the <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="formatProvider">The <see cref="System.IFormatProvider"/> to use to format the value.-or- null to obtain the numeric format information from the current locale setting of the operating system.
    ''' <para>If null is pased then <see cref="System.Globalization.CultureInfo.CurrentCulture"/> is used. This argument is used to obtain decimal separators, positive and negative signs and time separators in custom format and is also passed to custom subformats in braces. In order this parameter to work it's <see cref="IFormatProvider.GetFormat">GetFormat</see> method must return non-null value for <see cref="System.Globalization.NumberFormatInfo"/> and/or <see cref="System.Globalization.DateTimeFormatInfo"/>. If one of returned values is null <see cref="System.Globalization.NumberFormatInfo.CurrentInfo"/> resp. <see cref="System.Globalization.DateTimeFormatInfo.CurrentInfo"/> is used.</para>
    ''' </param>
    ''' <returns>A <see cref="System.String"/> containing the value of the current instance in the specified format.</returns>
    ''' <remarks>For more information about formating of <see cref="TimeSpanFormattable"/> see documentation of overloaded <seealso cref="ToString"/></remarks>
    ''' <exception cref="FormatException">Unknown predefined format -or- syntax error in format string</exception>
    ''' <exception cref="ArgumentOutOfRangeException">The 'T()' patter is used on negative <see cref="TimeSpanFormattable"/> or value of current <see cref="TimeSpanFormattable"/> added to <see cref="DateTime.MinValue"/> causes <see cref="DateTime.MaxValue"/> to be exceeded.</exception>
    Public Overloads Function ToString(ByVal format As String, ByVal formatProvider As System.IFormatProvider) As String Implements System.IFormattable.ToString
        Return TimeSpanFormattable.Format(Me, format, formatProvider)
    End Function
    ''' <summary>Formats the value of the current instance using the specified format, numeric format information is obtained from current locale setting of the operating system. If you want to use custom <see cref="IFormatProvider"/> use overloaded <see cref="ToString"/> function.</summary>
    ''' <param name="format">The <see cref="System.String"/> specifying the format to use.-or- null to use the default format defined for the type of the <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>A <see cref="System.String"/> containing the value of the current instance in the specified format.</returns>
    ''' <remarks>
    ''' <para><see cref="TimeSpanFormattable"/> provides large pallete of formats that can be used to represent current instance as <see cref="String"/>. Predefined and custom formats can be used.</para>
    ''' <list type="table">
    '''     <listheader>Predefined fromat strings</listheader>
    '''     <listheader><term>Predefined format string</term><description>Treated as this custom format string</description></listheader>
    '''     <item><term>g (short with full hours)</term><description>-h(00):mm</description></item>
    '''     <item><term>G (long with full hours)</term><description>-h(00):mm:ss (This is default format used when no format string is specified)</description></item>
    '''     <item><term>t (short time pattern)</term><description>-hh:mm</description></item>
    '''     <item><term>T (long time pattern)</term><description>-hh:mm:ss</description></item>
    '''     <item><term>l (shortest possible from hours to milliseconds)</term><description>-((h&lt;>0)h(0):mm:ss|(m&lt;>0)m(0):ss|s(0))((ll&lt;>0).lll)</description></item>
    '''     <item><term>L (shortest possible from days to milliseconds)</term><description>-((d&lt;>0)d.)((h&lt;>0)hh:mm:ss|(m&lt;>0)m(0):ss|s(0))((ll&lt;>0).lll)</description></item>
    '''     <item><term>s (shortest possible from hours to seconds)</term><description>-((h&lt;>0)h(0):mm:ss|(m&lt;>0)m(0):ss|s(0))</description></item>
    '''     <item><term>S (shortest possible from days to seconds)</term><description>-((d&lt;>0)d.)((h&lt;>0)hh:mm:ss|(m&lt;>0)m(0):ss|s(0))</description></item>
    ''' </list>
    ''' <list type="table">
    '''     <listheader>Custom format strings</listheader>
    '''     <listheader><term>Format string</term><description>Description</description></listheader>
    '''     <item><term>d, dd, ddd, ...</term>
    '''         <description>Any number of lowercase ds represents days (value of <see cref="Days"/>). Number of ds determines minimal number of digits that will represent number of days.</description>
    '''     </item>
    '''     <item><term>d(), dd()</term>
    '''         <description>Custom-formated days. Integral part of <see cref="TotalDays"/> which equals to <see cref="Days"/> fromated by format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>D()</term>
    '''         <description>Custom-formated fractional days. Value of <see cref="TotalDays"/> fromated with format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>[d], [dd], [ddd], ... or [d()] or [D()]</term>
    '''         <description>Optional days. Same formats as described above but output is ommited when number of whole days (<see cref="Days"/>) is zero.</description>
    '''     </item>
    '''     <item><term>h</term>
    '''         <description>Short hours. Value of <see cref="Hours"/> in 24 hour format from range 0÷23 as 1 or 2 digits.</description>
    '''     </item>
    '''     <item><term>hh</term>
    '''         <description>Long hours. Value of <see cref="Hours"/> in 24 hour format from range 0÷23 always as 2 digits.</description>
    '''     </item>
    '''     <item><term>h()</term>
    '''         <description>Custom-formated hours. Integral part of <see cref="TotalHours"/> formated with format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>hh()</term>
    '''         <description>Custom-formated hours part. <see cref="Hours"/> formated with format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>H()</term>
    '''         <description>Custom-formated fractional hours. Value of <see cref="TotalHours"/> formated with format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>H, HH, HHH, ...</term>
    '''         <description>Integral part of <see cref="TotalHours"/>, minimum number of digits same as number of Hs. Note: When multiple Hs are followed by '(' it's not treated as custom formatter for preceding Hs (like in case of 'H(')</description>
    '''     </item>
    '''     <item><term>m</term>
    '''         <description>Short minutes. Value of <see cref="Minutes"/> from range 0÷59 as 1 or 2 digits.</description>
    '''     </item>
    '''     <item><term>mm</term>
    '''         <description>Long minutes. Value of <see cref="Minutes"/> from range 0÷59 always as 2 digits.</description>
    '''     </item>
    '''     <item><term>m()</term>
    '''         <description>Custom-formated minutes. Integral part of <see cref="TotalMinutes"/> formated with format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>mm()</term>
    '''         <description>Custom-formated whole minutes. <see cref="Minutes"/> formated with format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>M()</term>
    '''         <description>Custom-formated fractional minutes. Value of <see cref="TotalMinutes"/> formated with formate specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>s</term>
    '''         <description>Short seconds. Value of <see cref="Seconds"/> from range 0÷59 as 1 or 2 digits.</description>
    '''     </item>
    '''     <item><term>ss</term>
    '''         <description>Long seconds. Value of <see cref="Seconds"/> from range 0÷59 always as 2 digits.</description>
    '''     </item>
    '''     <item><term>s()</term>
    '''         <description>Custom-formated seconds. Integral part of <see cref="TotalSeconds"/> formated with format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>ss()</term>
    '''         <description>Custom-formated whole seconds. <see cref="Seconds"/> formated with format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>S()</term>
    '''         <description>Custom-formated fractional seconds. Value of <see cref="TotalSeconds"/> fromated with format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>l</term>
    '''         <description>Short milliseconds. Value of <see cref="Milliseconds"/> from range 0÷999 as 1, 2 or 3 digits.</description>
    '''     </item>
    '''     <item><term>ll</term>
    '''         <description>Middle milliseconds. Value of <see cref="Milliseconds"/> from range 0÷999 as 2 or 3 digits.</description>
    '''     </item>
    '''     <item><term>lll</term>
    '''         <description>Long milliseconds. Value of <see cref="Milliseconds"/> from range 0÷999 always as 3 digits.</description>
    '''     </item>
    '''     <item><term>l()</term>
    '''         <description>Custom-formated milliseconds. Integral part of <see cref="TotalMilliseconds"/> formated with format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>ll()</term>
    '''         <description>Custom-formated whole milliseconds. <see cref="TotalMilliseconds"/> formated with format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>L()</term>
    '''         <description>Custom-formated fractional milliseconds. Value of <see cref="TotalMilliseconds"/> formated with formate specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>t, tt, ttt, ...</term>
    '''         <description>Any number of lowercase ts represents value of <see cref="Ticks"/>. The umber of ts determines minimal number of digits copyed to output.</description>
    '''     </item>
    '''     <item><term>t()</term>
    '''         <description>Custom-formated ticks. Value of <see cref="Ticks"/> formated with format specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>T()</term>
    '''         <description>
    '''             Custom-formated time. Time part represented as <see cref="System.DateTime"/> formated with format specified in braces. For more information about formats in braces see below.
    '''             The <see cref="System.DateTime"/> value if made as result of addition of <see cref="System.DateTime.MinValue"/> and current value of this <see cref="TimeSpanFormattable"/> and thus this pattern should be used only for positive <see cref="TimeSpanFormattable"/> values otherwise <see cref="System.ArgumentOutOfRangeException"/> may be thrown.
    '''         </description>
    '''     </item>
    '''     <item><term>:</term>
    '''         <description>The time separator defined in the current <see cref="System.Globalization.DateTimeFormatInfo.TimeSeparator"/> property that is used to differentiate hours, minutes, and seconds.</description>
    '''     </item>
    '''     <item><term>.</term>
    '''         <description>The actual character used as the decimal separator is determined by the <see cref="Globalization.NumberFormatInfo.NumberDecimalSeparator"/> property of the <see cref="Globalization.NumberFormatInfo"/> that controls formatting.</description>
    '''     </item>
    '''     <item><term>"</term>
    '''         <description>Quoted string (quotation mark). Displays the literal value of any string between two quotation marks (").</description>
    '''     </item>
    '''     <item><term>'</term>
    '''         <description>Quoted string (apostrophe). Displays the literal value of any string between two apostrophe (') characters.</description>
    '''     </item>
    '''     <item><term>%c</term>
    '''         <description>Represents the result associated with a custom format specifier "c", when the custom DateTime format string consists solely of that custom format specifier. This is used to determine between predefined and custom format string. Each format string that's length is 1 is treated as predefined format string. If you want to treat 1-character long custom format string as custom format string, precede it with the '%' character (otherwise it will be always treated as predefined format string even when such predefined format string doesn't exist which may lead to unexpected results or exceptions). In any other case ('%' is not first character of 2-characters long custom format string) the '%' is treated as any other unknown character and thus it is copyed to output (or causes <see cref="FormatException"/> if placed where it is not allowed).</description>
    '''     </item>
    '''     <item><term>\c</term>
    '''         <description>
    '''             <para>The escape character. Displays the character "c" as a literal when that character is preceded by the escape character (\). To insert the backslash character itself in the result string, use two escape characters ("\\").</para>
    '''             <para>This described behavior takes effect in context of quoted literals (" and ') and at root level of pattern. For information how the backslash escape character is treated in format in braces see below.</para>
    '''         </description>
    '''     </item>
    '''     <item><term>-</term>
    '''         <description>Optional sign. The minus sign (defined by current culture) is copyed to output when value of this <see cref="TimeSpanFormattable"/> is negative.</description>
    '''     </item>
    '''     <item><term>+</term>
    '''         <description>Compulsory sign. The minus or plus sign (defined by current culture) is copyed to output if value of this <see cref="TimeSpanFormattable"/> is non-zero.</description>
    '''     </item>
    '''     <item><term>(()|()|)</term>
    '''         <description>
    '''             Conditional formating. See below.
    '''         </description>
    '''     </item>
    '''     <item><term>Any other character</term>
    '''         <description>Any unknown character is copyed to output</description>
    '''     </item>
    ''' </list>
    ''' <para>Formats in braces:</para>
    ''' <para>
    '''     There are several contexts where you can use other nested format string to format part of rendered string. Those nested format strings are also enclosed with braces (). The format string expected in braces depends on value being formated. d,h,m,s,l and t are integral numbers, D,H,M,S and L are floating point numbers and T is <see cref="DateTime"/>. You can use any format string valied for appropriate data type - predefined or custom or you can leave braces empty to use default format.
    '''     Because you may want to pass closing brace ')' into custom format, you must understand how the closing brace is being esacped. If you want to pass the ')' into custom format instead of using it to close custom format block escape it with '\' (type '\)'). If you want to pass backslash ('\') into nested custom format string escape it with another '\' (type '\\'). Note if you will type any other chracter than ')' or '\' after '\' inside braces-delimited nested custom format string the whole sequence will be passed into underlying nested custom format string.
    '''     For example if you want to use custom format string 'hh"(hours) and "mm"(minutes)"' (which produces something like '13(hours) and 33(minutes)' you should type 
    '''     <example>T(hh"(hours\) and "mm"(minutes\)")</example>(Note: Same effect can be reached with format string 'hh"(hours) and "mm"(minutes)"' directly.
    ''' </para>
    ''' <para>Conditional formating:</para>
    ''' <para>
    '''     Any part of formating string can be surrounded by conditional formating construct. Sub-string in each part of conditional formatting must be valid formating string. Sub-string outside conditional formatting must be valid formating string. Conditional formatings can be nested.
    ''' </para>
    ''' <para>
    '''     Conditional formating has similar structure as if-elseif-else statement. It begins with brace '(' and ends with brace ')'. Conditions are stated in another braces '()' just at start of conditional part. 2nd and next conditional parts are delimited by pipe '|' (followsed by braces with condition). Last conditional part (the else) does not need condition.
    '''     Syntax of conditional segment of formating string is:
    ''' </para>
    ''' <code>((condition1)format1|(condition2)format2|(condition3)format3|format4)</code>
    ''' <para>There can be 1 or more condition-format pairs. There can be or can be not last fall-backl format. Conditions are evaluated from left to right. Format associated with firts condition that evaluates to true is emited to output. False evaluated formats and skipped formats are still parsed and must be valid. But are not emitted to output. Syntax of condition is:</para>
    ''' <code>value operator literal</code>
    ''' <para>Value can be one of following:</para>
    ''' <list type="table"><listheader><term>Value string</term><description>Meaning</description></listheader>
    '''     <item><term>d</term><description>Integral part of <see cref="TotalDays"/></description></item>
    '''     <item><term>D</term><description><see cref="TotalDays"/></description></item>
    '''     <item><term>dd</term><description><see cref="Days"/></description></item>
    '''     <item><term>h</term><description>Integral part of <see cref="TotalHours"/></description></item>
    '''     <item><term>H</term><description><see cref="TotalHours"/></description></item>
    '''     <item><term>hh</term><description><see cref="Hours"/></description></item>
    '''     <item><term>m</term><description>Integral part of <see cref="TotalMinutes"/></description></item>
    '''     <item><term>M</term><description><see cref="TotalMinutes"/></description></item>
    '''     <item><term>mm</term><description><see cref="Minutes"/></description></item>
    '''     <item><term>s</term><description>Integral part of <see cref="TotalSeconds"/></description></item>
    '''     <item><term>S</term><description><see cref="TotalSeconds"/></description></item>
    '''     <item><term>ss</term><description><see cref="Seconds"/></description></item>
    '''     <item><term>l</term><description>Integral part of <see cref="TotalMilliseconds"/></description></item>
    '''     <item><term>L</term><description><see cref="TotalMilliseconds"/></description></item>
    '''     <item><term>ll</term><description><see cref="Milliseconds"/></description></item>
    '''     <item><term>T</term><description><see cref="TimeSpan"/> with same value as current instance of <see cref="TimeSpanFormattable"/></description></item>
    '''     <item><term>t</term><description><see cref="Ticks"/></description></item>
    ''' </list>
    ''' <para>Any value can be surronded by pipes (||) in order to make absolute value of it.</para>
    ''' <para>Operator can be one of following comparison operators: &lt;, &lt;=, =, >, >=, &lt;></para>
    ''' <para>Literal is numeric literal in like 123.148. Can be preceded with negative sing (-). Parts on the left and on the right side of decimal dot are optinal. Examples of valid number are: 128, -128, 128., .128, 128.128, -.128, -128., -128, 128. When comparing to T (whole <see cref="TimeSpan"/>) right operand shall not be number but time value in format D.h:m:s.l.
    ''' Time literal have not to be fully specified. If it is specified as number only, it is treated as hours. Days can be ommited (including first dot). Minutes, seconds and milliseconds can be ommited including leading colons (or dots). Examples of valid times are: 14 (14 hours), 14.3 (14 days, 3 hours), 14.25:10 (14 days, 25 hours, 10 minutes), 10 (10 hours), 10:00:01 (10 hours, 1 second), 0:0:0000001.321 (1.321 seconds)</para>
    ''' <para>Conditions does not allow any spaces in them. Any character that is not understood is treated as error. Technically you can specify multiple fall down conditions and specify |() condition after | condition. Practically no such condition is ever emitted because some of prevous conditions (|) have evaluetad to true. However syntactically it is OK, error oe not thrown. Any formating that can be used outside conditions (including so-called formats in braces) can be used inside conditions.</para>
    ''' <para>Note: Format string is parsed from left to right by finite deterministic state automaton and thus format string like mmm on time span of value 1:14:00 will produce "1414" etc.</para>
    ''' <para>Examples (both have same results):</para>
    ''' <example><code>Time is\: ((M>=60)hh:mm:ss|(S>=60)mm:ss|ss).l</code></example>
    ''' <example><code>Time is\: ((T>=1)hh:mm:ss|(T>=0:1)mm:ss|ss).l</code></example>
    ''' <para>Note: All numbers are rendered as absolute value, you must use +/- to display a sign</para>
    ''' <para>See also: <a href="http://msdn2.microsoft.com/en-us/library/0c899ak8.aspx">Custom numeric formats</a>, <a href="http://msdn2.microsoft.com/en-us/library/dwhawy9k.aspx">Standard numeric formats</a></para>
    ''' </remarks>
    ''' <exception cref="FormatException">Unknown predefined format -or- syntax error in format string</exception>
    ''' <exception cref="ArgumentOutOfRangeException">The 'T()' patter is used on negative <see cref="TimeSpanFormattable"/> or value of current <see cref="TimeSpanFormattable"/> added to <see cref="DateTime.MinValue"/> causes <see cref="DateTime.MaxValue"/> to be exceeded.</exception>
    ''' <version version="1.5.3">Added new formating options capital H without braces, HH, HHH, ...</version>
    Public Overloads Function ToString(ByVal format As String) As String
        Return ToString(format, Nothing)
    End Function
    ''' <summary>Formats given <see cref="TimeSpanFormattable"/> using given format string</summary>
    ''' <param name="TS">A <see cref="TimeSpanFormattable"/> to be formated</param>
    ''' <param name="formatStr">A format string (Can be predefined or custom)</param>
    ''' <param name="formatProvider">The <see cref="System.IFormatProvider"/> to use to format the value.-or- null to obtain the numeric format information from the current locale setting of the operating system.
    ''' <para>If null is pased then <see cref="System.Globalization.CultureInfo.CurrentCulture"/> is used. This argument is used to obtain decimal separators, positive and negative signs and time separators in custom format and is also passed to custom subformats in braces. In order this parameter to work it's <see cref="IFormatProvider.GetFormat">GetFormat</see> method must return non-null value for <see cref="System.Globalization.NumberFormatInfo"/> and/or <see cref="System.Globalization.DateTimeFormatInfo"/>. If one of returned values is null <see cref="System.Globalization.NumberFormatInfo.CurrentInfo"/> resp. <see cref="System.Globalization.DateTimeFormatInfo.CurrentInfo"/> is used.</para></param>
    ''' <returns><paramref name="TS"/> fromated using <paramref name="formatStr"/></returns>
    ''' <exception cref="FormatException">Unknown predefined format -or- syntax error in format string</exception>
    ''' <exception cref="ArgumentOutOfRangeException">The 'T()' patter is used on negative <see cref="TimeSpanFormattable"/> or value of current <see cref="TimeSpanFormattable"/> added to <see cref="DateTime.MinValue"/> causes <see cref="DateTime.MaxValue"/> to be exceeded.</exception>
    ''' <remarks>For more information about formating <see cref="TimeSpanFormattable"/> see <seealso cref="ToString"/></remarks>
    Private Shared Function Format(ByVal TS As TimeSpan, ByVal formatStr As String, ByVal formatProvider As System.IFormatProvider) As String
        If ((formatStr Is Nothing) OrElse (formatStr.Length = 0)) Then
            formatStr = "G"
        End If
        If (formatStr.Length = 1) Then
            formatStr = ExpandPredefinedFormat(formatStr)
        End If
        Return FormatCustomized(TS, formatStr, formatProvider)
    End Function
#Region "Predefined formats"
    ''' <summary>Gets custom formats that represents given predefined format</summary>
    ''' <param name="format">Predefined format</param>
    ''' <returns>Custom format that predefined format <paramref name="format"/> expands to.</returns>
    ''' <exception cref="FormatException">Given predefined format <paramref name="format"/> is not known. Know predefined formats are G, g, T and t.</exception>
    ''' <remarks>For more information about formating <see cref="TimeSpanFormattable"/> see <seealso cref="ToString"/></remarks>
    Private Shared Function ExpandPredefinedFormat(ByVal format As Char) As String
        With PredefinedFormats
            If .ContainsKey(format) Then Return .Item(format) Else Throw New FormatException("Unknow predefined format")
        End With
        'Select Case format
        '    Case "g"c : Return "-h(00):mm"
        '    Case "G"c : Return "-h(00):mm:ss"
        '    Case "t"c : Return "-hh:mm"
        '    Case "T"c : Return "-hh:mm:ss"
        '    Case "l"c : Return "-((h<>0)h(0):mm:ss|(m<>0)m(0):ss|s(0))((ll<>0).lll)"
        '    Case "L"c : Return "-((d<>0)d.)((h<>0)hh:mm:ss|(m<>0)m(0):ss|s(0))((ll<>0).lll)"
        '    Case "s"c : Return "-((h<>0)h(0):mm:ss|(m<>0)m(0):ss|s(0))"
        '    Case "S"c : Return "-((d<>0)d.)((h<>0)hh:mm:ss|(m<>0)m(0):ss|s(0))"
        '    Case Else : Throw New FormatException("Unknow predefined format")
        'End Select
    End Function
    ''' <summary>Short code of predefined format g - short with full hours</summary>
    ''' <seealso cref="efShort"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const pfShort As Char = "g"c
    ''' <summary>Short code of predefined format G - long with full hours</summary>
    ''' <seealso cref="efLong"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const pfLong As Char = "G"c
    ''' <summary>Short code of predefined format t - short time pattern</summary>
    ''' <seealso cref="efShortTime"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const pfShortTime As Char = "t"c
    ''' <summary>Short code of predefined format T - long time patern</summary>
    ''' <seealso cref="efLongTime"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const pfLongTime As Char = "T"c
    ''' <summary>Short code of predefined format l - shortest possible from hours to milliseconds</summary>
    ''' <seealso cref="efShortest_hl"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const pfShortest_hl As Char = "l"c
    ''' <summary>Short code of predefined format L - shortest possible from days to milliseconds</summary>
    ''' <seealso cref="efShortest_dl"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const pfShortest_dl As Char = "L"c
    ''' <summary>Short code of predefined format L - shortest possible hours days to seconds</summary>
    ''' <seealso cref="efShortest_hs"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const pfShortest_hs As Char = "s"c
    ''' <summary>Short code of predefined format L - shortest possible from days to seconds</summary>
    ''' <seealso cref="efShortest_ds"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const pfShortest_ds As Char = "S"c

    ''' <summary>Expanded pattern of predefined format g - short with full hours</summary>
    ''' <seealso cref="pfShort"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const efShort$ = "-h(00):mm"
    ''' <summary>Expanded pattern of predefined format G - long with full hours</summary>
    ''' <seealso cref="pfLong"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const efLong$ = "-h(00):mm:ss"
    ''' <summary>Expanded pattern of predefined format t - short time pattern</summary>
    ''' <seealso cref="pfShortTime"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const efShortTime$ = "-hh:mm"
    ''' <summary>Expanded pattern of predefined format T - long time patern</summary>
    ''' <seealso cref="pfLongTime"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const efLongTime$ = "-hh:mm:ss"
    ''' <summary>Expanded pattern of predefined format l - shortest possible from hours to milliseconds</summary>
    ''' <seealso cref="pfShortest_hl"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const efShortest_hl$ = "-((h<>0)h(0):mm:ss|(m<>0)m(0):ss|s(0))((ll<>0).lll)"
    ''' <summary>Expanded pattern of predefined format L - shortest possible from days to milliseconds</summary>
    ''' <seealso cref="pfShortest_dl"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const efShortest_dl$ = "-((d<>0)d.)((h<>0)hh:mm:ss|(m<>0)m(0):ss|s(0))((ll<>0).lll)"
    ''' <summary>Expanded pattern of predefined format L - shortest possible hours days to seconds</summary>
    ''' <seealso cref="pfShortest_hs"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const efShortest_hs$ = "-((h<>0)h(0):mm:ss|(m<>0)m(0):ss|s(0))"
    ''' <summary>Expanded pattern of predefined format L - shortest possible from days to seconds</summary>
    ''' <seealso cref="pfShortest_ds"/>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Const efShortest_ds$ = "-((d<>0)d.)((h<>0)hh:mm:ss|(m<>0)m(0):ss|s(0))"
    ''' <summary>Gets dictionary of all predefined formats. Keys contain short codes, values contain expanded patterns.</summary>
    ''' <returns>New instance of <see cref="Dictionary(Of Char, String)"/> that contains all the predefined formats</returns>
    Public Shared ReadOnly Property PredefinedFormats() As Dictionary(Of Char, String)
        Get
            Dim ret As New Dictionary(Of Char, String)
            ret.Add(pfShort, efShort)
            ret.Add(pfLong, efLong)
            ret.Add(pfShortTime, efShortTime)
            ret.Add(pfLongTime, efLongTime)
            ret.Add(pfShortest_hl, efShortest_hl)
            ret.Add(pfShortest_dl, efShortest_dl)
            ret.Add(pfShortest_hs, efShortest_hs)
            ret.Add(pfShortest_ds, efShortest_ds)
            Return ret
        End Get
    End Property
#End Region

    ''' <summary>States of finite state deterministic automaton used to parse custom format string in <see cref="FormatCustomized"/></summary>
    Private Enum FormatAutomatState
        ''' <summary>Normal state</summary>
        nth
#Region "d"
        ''' <summary>After d</summary>
        d1
        ''' <summary>After dd</summary>
        d2
        ''' <summary>Ar ddd (and any number of ds)</summary>
        d3
        ''' <summary>Afterd d(</summary>
        dFormat
        ''' <summary>Afterd d(\</summary>
        dFormatb
        ''' <summary>After D</summary>
        D_
        ''' <summary>After D(</summary>
        D_Format
        ''' <summary>After D(\</summary>
        D_Formatb
        ''' <summary>After [</summary>
        leftB
        ''' <summary>After [d</summary>
        leftBd1
        ''' <summary>After [dd</summary>
        leftBd2
        ''' <summary>After [d(</summary>
        leftBdFormat
        ''' <summary>After [d(\</summary>
        leftBdFormatb
        ''' <summary>After [D</summary>
        leftBD_
        ''' <summary>After [D(</summary>
        leftBD_Format
        ''' <summary>After [D(\</summary>
        leftBD_Formatb
        ''' <summary>After [D(...) or [d(...)</summary>
        leftBEnd
#End Region
#Region "h"
        ''' <summary>After h</summary>
        h1
        ''' <summary>After H</summary>
        H_
        ''' <summary>After h(</summary>
        hFormat
        ''' <summary>After h(\</summary>
        hFormatb
        ''' <summary>After H(</summary>
        H_Format
        ''' <summary>After H(\</summary>
        H_Formatb
        ''' <summary>After hh</summary>
        h2
        ''' <summary>After hh(</summary>
        h2Format
        ''' <summary>After hh(\</summary>
        h2Formatb
        ''' <summary>After HH</summary>
        H2_
#End Region
#Region "m"
        ''' <summary>After m</summary>
        m1
        ''' <summary>After M</summary>
        M_
        ''' <summary>After m(</summary>
        mFormat
        ''' <summary>After m(\</summary>
        mFormatb
        ''' <summary>After M(</summary>
        M_Format
        ''' <summary>After M(\</summary>
        M_Formatb
        ''' <summary>After mm</summary>
        m2
        ''' <summary>After mm(</summary>
        m2Format
        ''' <summary>After mm(\</summary>
        m2Formatb
#End Region
#Region "s"
        ''' <summary>After s</summary>
        s1
        ''' <summary>After S</summary>
        S_
        ''' <summary>After s(</summary>
        sFormat
        ''' <summary>After s(\</summary>
        sFormatb
        ''' <summary>After S(</summary>
        S_Format
        ''' <summary>After S(\</summary>
        S_Formatb
        ''' <summary>After ss</summary>
        s2
        ''' <summary>After ss(</summary>
        s2Format
        ''' <summary>After ss(\</summary>
        s2Formatb
#End Region
#Region "l"
        ''' <summary>After l</summary>
        l1
        ''' <summary>After ll</summary>
        l2
        ''' <summary>After L</summary>
        L_
        ''' <summary>After l(</summary>
        lFormat
        ''' <summary>After l(\</summary>
        lFormatb
        ''' <summary>After L(</summary>
        L_Format
        ''' <summary>After L(\</summary>
        L_Formatb
        ''' <summary>After ll(</summary>
        l2Format
        ''' <summary>After ll(\</summary>
        l2Formatb
#End Region
#Region "t"
        ''' <summary>After t</summary>
        t1
        ''' <summary>After tt</summary>
        t2
        ''' <summary>After T</summary>
        T_
        ''' <summary>After t(</summary>
        tFormat
        ''' <summary>After t(\</summary>
        tFormatb
        ''' <summary>After T(</summary>
        T_Format
        ''' <summary>After T(\</summary>
        T_Formatb
#End Region
        ''' <summary>After '</summary>
        singleQ
        ''' <summary>After '\</summary>
        singleQb
        ''' <summary>After "</summary>
        doubleQ
        ''' <summary>After "\</summary>
        doubleQb
        ''' <summary>After \</summary>
        Back
#Region "Conditions"
        ''' <summary>After (</summary>
        Open
        ''' <summary>After (( or |( in condition</summary>
        Open2
        ''' <summary>In condition body after |</summary>
        Pipe
        ''' <summary>Condition expression, part 1, after |</summary>
        OpenPipe
        ''' <summary>Condition expression, after 1st part, expects comparison operator</summary>
        AwaitComparison
        ''' <summary>Condition expression, part 1, expects closing pipe</summary>
        AwaitPipe
        ''' <summary>Condition expression, part 1, after h</summary>
        Openh
        ''' <summary>Condition expression, part 1, after m</summary>
        Openm
        ''' <summary>Condition expression, part 1, after s</summary>
        Opens
        ''' <summary>Condition expression, part 1, after d</summary>
        Opend
        ''' <summary>Condition expression, part 1, after l</summary>
        Openl
        ''' <summary>Condition expression, part 1, after h (in pipes)</summary>
        OpenPipeh
        ''' <summary>Condition expression, part 1, after m (in pipes)</summary>
        OpenPipem
        ''' <summary>Condition expression, part 1, after s (in pipes)</summary>
        OpenPipes
        ''' <summary>Condition expression, part 1, after d (in pipes)</summary>
        OpenPiped
        ''' <summary>Condition expression, part 1, after l (in pipes)</summary>
        OpenPipel
        ''' <summary>Condition expression, comparison operator, after &lt;</summary>
        Clt
        ''' <summary>Condition expression, comparison operator, after ></summary>
        Cgt
        ''' <summary>Condition expression, after operator &lt; or >=</summary>
        AwaitNumber
        ''' <summary>Condition expression, part 2, after minus sign</summary>
        AfterMinus
        ''' <summary>Condition expression, part 2, after dot</summary>
        AfterDot
        ''' <summary>Condition expression, part 2, in number before dot</summary>
        BeforeDot
        ''' <summary>Condition expression, before oparetor after t or |t|</summary>
        OpenT
        ''' <summary>Condition expression, part1, after |t</summary>
        OpenPipeT
        ''' <summary>Condition expression, operator, after &lt; or |t|&lt;</summary>
        Tlt
        ''' <summary>Condition expression, operator, after t> or |t|></summary>
        Tgt
        ''' <summary>Condition expression, part 2, after -</summary>
        TMinus
        ''' <summary>Condition expression, after comparison operator</summary>
        TAwait1
        ''' <summary>Condition expression, part 2, first number of time</summary>
        TNumber1
        ''' <summary>Condition expression, part 2, before hour bart</summary>
        TAwaitH
        ''' <summary>Condition expression, part2, hour part</summary>
        TH
        ''' <summary>Condition expression, part 2, before minute part</summary>
        TAwaitM
        ''' <summary>Condition expression, part 2, minute part</summary>
        TM
        ''' <summary>Condition expression, part 2, before second part</summary>
        TAwaitS
        ''' <summary>Condition expression, part 2, second part</summary>
        TS
        ''' <summary>Condition expression, part 2, before millisecond part</summary>
        TAwaitL
        ''' <summary>Condition expression, part 2, millisecond part</summary>
        TL
#End Region
    End Enum



    ''' <summary>Handles backslash in nested format in braces. Extracted repeatedly used part of <see cref="FormatCustomized"/>.</summary>
    ''' <param name="format">Format string</param>
    ''' <param name="InFormat">Nested format string being produced.</param>
    ''' <param name="i">Current position in <paramref name="format"/></param>
    Private Shared Sub PartCustomFormatBS(ByVal format As String, ByRef InFormat As String, ByVal i As Integer)
        Select Case format(i)
            Case "\"c, ")"c, CChar(NullChar)
                InFormat &= format(i)
            Case Else
                InFormat &= "\"c & format(i)
        End Select
    End Sub
    ''' <summary>Realizes Finite Deterministic State Automaton that parses format string and produces resulting output string</summary>
    ''' <param name="TS">A <see cref="TimeSpanFormattable"/> to be formated</param>
    ''' <param name="format">Format string</param>
    ''' <param name="prov">The <see cref="System.IFormatProvider"/> to use to format the value.-or- null to obtain the numeric format information from the current locale setting of the operating system.
    ''' <para>If null is pased then <see cref="System.Globalization.CultureInfo.CurrentCulture"/> is used. This argument is used to obtain decimal separators, positive and negative signs and time separators in custom format and is also passed to custom subformats in braces. In order this parameter to work it's <see cref="IFormatProvider.GetFormat">GetFormat</see> method must return non-null value for <see cref="System.Globalization.NumberFormatInfo"/> and/or <see cref="System.Globalization.DateTimeFormatInfo"/>. If one of returned values is null <see cref="System.Globalization.NumberFormatInfo.CurrentInfo"/> resp. <see cref="System.Globalization.DateTimeFormatInfo.CurrentInfo"/> is used.</para>
    ''' </param>
    ''' <returns><paramref name="TS"/> formated using <paramref name="format"/></returns>
    ''' <remarks>For more information about formating <see cref="TimeSpanFormattable"/> see <seealso cref="ToString"/></remarks>
    ''' <exception cref="FormatException">Unknown predefined format -or- syntax error in format string</exception>
    ''' <exception cref="ArgumentOutOfRangeException">The 'T()' pattern is used on negative <see cref="TimeSpanFormattable"/> or value of current <see cref="TimeSpanFormattable"/> added to <see cref="DateTime.MinValue"/> causes <see cref="DateTime.MaxValue"/> to be exceeded.</exception>
    Private Shared Function FormatCustomized(ByVal TS As TimeSpan, ByVal format As String, ByVal prov As System.IFormatProvider) As String
        Dim ret As New StringBuilder
        If format.Length = 2 AndAlso format(0) = "%"c Then format = format.Substring(1)
        format = format.Replace(NullChar, "\" & NullChar)
        format &= CChar(NullChar)
        Dim state As FormatAutomatState = FormatAutomatState.nth
        Dim StartIndex As Integer = -1
        Dim InFormat As String = ""
        Dim NProvider As Globalization.NumberFormatInfo = Nothing
        Dim TProvider As Globalization.DateTimeFormatInfo = Nothing
        If prov IsNot Nothing Then
            NProvider = prov.GetFormat(GetType(Globalization.NumberFormatInfo))
            TProvider = prov.GetFormat(GetType(Globalization.DateTimeFormatInfo))
        End If
        If NProvider Is Nothing Then NProvider = Globalization.NumberFormatInfo.CurrentInfo
        If TProvider Is Nothing Then TProvider = Globalization.DateTimeFormatInfo.CurrentInfo
        Dim Conditions As New Stack(Of Boolean)
        Conditions.Push(True)
        Dim PreviousConditions As New Stack(Of List(Of Boolean))
        PreviousConditions.Push(New List(Of Boolean))
        Dim Append = Function(str$) If(Conditions.Peek AndAlso Not PreviousConditions.Peek.Contains(True), ret.Append(str), Nothing)
        Dim CompareValue As Object = Nothing
        Dim CompareOperator As ComparisonOperators
        Dim NumberBuilder As Double
        Dim DecimalPlace%
        Dim TimeSpanBuilder As TimeSpan
        Dim NegativeZero As Boolean
        For i As Integer = 0 To format.Length - 1
            Select Case state
                Case FormatAutomatState.nth 'Basic
                    Select Case format(i)
                        Case "d"c : state = FormatAutomatState.d1 : StartIndex = i
                        Case "D"c : state = FormatAutomatState.D_
                        Case "["c : state = FormatAutomatState.leftB
                        Case "h"c : state = FormatAutomatState.h1
                        Case "H"c : state = FormatAutomatState.H_ : StartIndex = i
                        Case "m"c : state = FormatAutomatState.m1
                        Case "M"c : state = FormatAutomatState.M_
                        Case "s"c : state = FormatAutomatState.s1
                        Case "S"c : state = FormatAutomatState.S_
                        Case "l"c : state = FormatAutomatState.l1
                        Case "L"c : state = FormatAutomatState.L_
                        Case "t"c : state = FormatAutomatState.t1 : StartIndex = i
                        Case "T"c : state = FormatAutomatState.T_
                        Case ":"c : Append.Invoke(TProvider.TimeSeparator)
                        Case """"c : state = FormatAutomatState.doubleQ : StartIndex = i
                        Case "'"c : state = FormatAutomatState.singleQ : StartIndex = i
                        Case "\"c : state = FormatAutomatState.Back
                        Case "."c : Append.Invoke(NProvider.NumberDecimalSeparator)
                        Case "-"c : If TS.TotalMilliseconds < 0 Then Append.Invoke(NProvider.NegativeSign)
                        Case "+"c
                            If TS.TotalMilliseconds < 0 Then
                                Append.Invoke(NProvider.NegativeSign)
                            ElseIf TS.TotalMilliseconds > 0 Then
                                Append.Invoke(NProvider.PositiveSign)
                            End If
                        Case NullChar : Exit For
                        Case "("c : state = FormatAutomatState.Open
                        Case ")"c : If Conditions.Count > 1 Then Conditions.Pop() : PreviousConditions.Pop() Else Append.Invoke(format(i))
                        Case "|"c : If Conditions.Count > 1 Then state = FormatAutomatState.Pipe Else Append.Invoke(format(i))
                        Case Else : Append.Invoke(format(i))
                    End Select
                Case FormatAutomatState.d1 'd
                    Select Case format(i)
                        Case "d"c : state = FormatAutomatState.d2
                        Case "("c : state = FormatAutomatState.dFormat : InFormat = ""
                        Case Else
                            i -= 1
                            Append.Invoke(Abs(TS.Days).ToString("0", prov))
                            state = FormatAutomatState.nth
                    End Select
                Case FormatAutomatState.d2 'dd
                    Select Case format(i)
                        Case "d"c : state = FormatAutomatState.d3
                        Case "("c : state = FormatAutomatState.dFormat : InFormat = ""
                        Case Else : i -= 1
                            Append.Invoke(Abs(TS.Days).ToString(New String("0"c, i - StartIndex + 1), prov))
                            state = FormatAutomatState.nth
                    End Select
                Case FormatAutomatState.d3 'ddd...
                    Select Case format(i)
                        Case "d"c 'Do nothing
                        Case Else : i -= 1
                            Append.Invoke(Abs(TS.Days).ToString(New String("0"c, i - StartIndex + 1), prov))
                            state = FormatAutomatState.nth
                    End Select
                Case FormatAutomatState.dFormat 'd(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.dFormatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            Append.Invoke(Abs(TS.Days).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.dFormatb 'd(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.dFormat
                Case FormatAutomatState.D_ 'D
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.D_Format : InFormat = ""
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringDMustBeFollowedWithAt0, i + 1))
                    End Select
                Case FormatAutomatState.D_Format 'D(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.D_Formatb
                        Case ")"c : state = FormatAutomatState.nth
                            Append.Invoke(Abs(TS.TotalDays).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.D_Formatb 'D(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.D_Format
                Case FormatAutomatState.leftB '[
                    Select Case format(i)
                        Case "d"c : state = FormatAutomatState.leftBd1
                        Case "D"c : state = FormatAutomatState.leftBD_
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringMustBeFollowedWithDOrDAt0, i + 1))
                    End Select
                Case FormatAutomatState.leftBd1 '[d
                    Select Case format(i)
                        Case "d"c : state = FormatAutomatState.leftBd2
                        Case "("c : state = FormatAutomatState.leftBdFormat : InFormat = ""
                        Case Else
                            i -= 1
                            If TS.Days <> 0 Then Append.Invoke(Abs(TS.Days).ToString("0", prov))
                            state = FormatAutomatState.leftBEnd
                    End Select
                Case FormatAutomatState.leftBd2 '[dd
                    Select Case format(i)
                        Case "d"c
                        Case Else
                            i -= 1
                            If TS.Days <> 0 Then _
                                Append.Invoke(Abs(TS.Days).ToString(New String("0"c, i - StartIndex + 1), prov))
                            state = FormatAutomatState.leftBEnd
                    End Select
                Case FormatAutomatState.leftBdFormat '[d(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.leftBdFormatb
                        Case ")"c
                            state = FormatAutomatState.leftBEnd
                            If TS.Days <> 0 Then _
                                Append.Invoke(Abs(TS.Days).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.leftBdFormatb '[d(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.leftBdFormat
                Case FormatAutomatState.leftBD_ '[D
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.leftBD_Format : InFormat = ""
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringDMustBeFollowedWithAt0, i + 1))
                    End Select
                Case FormatAutomatState.leftBD_Format '[D(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.leftBD_Formatb
                        Case ")"c : state = FormatAutomatState.leftBEnd
                            If TS.Days <> 0 Then _
                                Append.Invoke(Abs(TS.TotalDays).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.leftBD_Formatb '[D(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.leftBD_Format
                Case FormatAutomatState.leftBEnd '[D(...) or [d(...)
                    Select Case format(i)
                        Case "]"c
                            state = FormatAutomatState.nth
                        Case Else
                            Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormastStringExpectedAt0, i + 1))
                    End Select
                Case FormatAutomatState.h1 'h
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.hFormat : InFormat = ""
                        Case "h" : state = FormatAutomatState.h2
                        Case Else : Append.Invoke(Abs(TS.Hours).ToString("0", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.h2 'hh
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.h2Format : InFormat = ""
                        Case Else : Append.Invoke(Abs(TS.Hours).ToString("00", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.H_ 'H
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.H_Format : InFormat = ""
                        Case "H"c : state = FormatAutomatState.H2_
                        Case Else : Append.Invoke(Abs(CInt(Math.Floor(TS.TotalHours))).ToString("0", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.H2_ 'HH..
                    Select Case format(i)
                        Case "H"c
                        Case Else : i -= 1
                            Append.Invoke(Abs(TS.TotalHours).ToString(New String("0"c, i - StartIndex + 1), prov))
                            state = FormatAutomatState.nth
                    End Select
                Case FormatAutomatState.hFormat 'h(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.hFormatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            Append.Invoke(Truncate(Abs(TS.TotalHours)).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.hFormatb 'h(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.hFormat
                Case FormatAutomatState.h2Format 'hh(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.h2Formatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            Append.Invoke(Truncate(Abs(TS.Hours)).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.h2Formatb 'hh(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.h2Format
                Case FormatAutomatState.H_Format 'H(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.H_Formatb
                        Case ")"c : state = FormatAutomatState.nth
                            Append.Invoke(Abs(TS.TotalHours).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.H_Formatb 'H(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.H_Format
                Case FormatAutomatState.m1 'm
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.mFormat : InFormat = ""
                        Case "m" : state = FormatAutomatState.m2
                        Case Else : Append.Invoke(Abs(TS.Minutes).ToString("0", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.m2 'mm
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.m2Format : InFormat = ""
                        Case Else : Append.Invoke(Abs(TS.Minutes).ToString("00", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.M_ 'M
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.M_Format : InFormat = ""
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringMMustBeFollowedWithAt0, i + 1))
                    End Select
                Case FormatAutomatState.mFormat 'm(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.mFormatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            Append.Invoke(Truncate(Abs(TS.TotalMinutes)).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.m2Formatb 'm(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.mFormat
                Case FormatAutomatState.m2Format 'mm(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.m2Formatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            Append.Invoke(Truncate(Abs(TS.Minutes)).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.m2Formatb 'mm(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.m2Format
                Case FormatAutomatState.M_Format 'M(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.M_Formatb
                        Case ")"c : state = FormatAutomatState.nth
                            Append.Invoke(Abs(TS.TotalMinutes).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.M_Formatb 'M(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.M_Format
                Case FormatAutomatState.s1 's
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.sFormat : InFormat = ""
                        Case "s" : state = FormatAutomatState.s2
                        Case Else : Append.Invoke(Abs(TS.Seconds).ToString("0", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.s2 'ss
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.s2Format : InFormat = ""
                        Case Else : Append.Invoke(Abs(TS.Seconds).ToString("00", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.S_ 'S
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.S_Format : InFormat = ""
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringSMustBeFollowedWithAt0, i + 1))
                    End Select
                Case FormatAutomatState.sFormat 's(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.sFormatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            Append.Invoke(Truncate(Abs(TS.TotalSeconds)).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.sFormatb 's(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.sFormat
                Case FormatAutomatState.s2Format 'ss(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.s2Formatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            Append.Invoke(Truncate(Abs(TS.Seconds)).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.s2Formatb 'ss(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.s2Format
                Case FormatAutomatState.S_Format 'S(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.M_Formatb
                        Case ")"c : state = FormatAutomatState.nth
                            Append.Invoke(Abs(TS.TotalSeconds).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.M_Formatb 'S(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.S_Format
                Case FormatAutomatState.l1 'l
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.sFormat : InFormat = ""
                        Case "l" : state = FormatAutomatState.l2
                        Case Else : Append.Invoke(Abs(TS.Milliseconds).ToString("0", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.l2 'll
                    Select Case format(i)
                        Case "l" : Append.Invoke(Abs(TS.Milliseconds).ToString("000", prov)) : state = FormatAutomatState.nth
                        Case "("c : state = FormatAutomatState.l2Format : InFormat = ""
                        Case Else : Append.Invoke(Abs(TS.Milliseconds).ToString("00", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.L_ 'L
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.L_Format : InFormat = ""
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringLMustBeFollowedWithAt0, i + 1))
                    End Select
                Case FormatAutomatState.lFormat 'l(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.lFormatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            Append.Invoke(Truncate(Abs(TS.TotalMilliseconds)).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.lFormatb 'l(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.lFormat
                Case FormatAutomatState.l2Format 'll(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.l2Formatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            Append.Invoke(Truncate(Abs(TS.Milliseconds)).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.l2Formatb 'll(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.l2Format
                Case FormatAutomatState.L_Format 'L(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.L_Formatb
                        Case ")"c : state = FormatAutomatState.nth
                            Append.Invoke(Abs(TS.TotalMilliseconds).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.L_Formatb 'L(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.L_Format
                Case FormatAutomatState.t1 't
                    Select Case format(i)
                        Case "t"c : state = FormatAutomatState.t2
                        Case "("c : state = FormatAutomatState.tFormat : InFormat = ""
                        Case Else
                            i -= 1
                            Append.Invoke(Abs(TS.Ticks).ToString("0", prov))
                            state = FormatAutomatState.nth
                    End Select
                Case FormatAutomatState.t2 'tt
                    Select Case format(i)
                        Case "t"c
                        Case Else
                            i -= 1
                            Append.Invoke(Abs(TS.Ticks).ToString(New String("0"c, i - StartIndex + 1), prov))
                            state = FormatAutomatState.nth
                    End Select
                Case FormatAutomatState.T_ 'T
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.T_Format
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringTMustBeFollowedByAt0, i + 1))
                    End Select
                Case FormatAutomatState.tFormat 't(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.tFormatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            Append.Invoke(Abs(TS.Ticks).ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.tFormatb 't(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.tFormat
                Case FormatAutomatState.T_Format 'T(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.T_Formatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            Dim used As TimeSpan = TS
                            If used.TotalMilliseconds < 0 Then used = -used
                            Dim used2 As Date = Date.MinValue + used
                            Append.Invoke(used2.ToString(InFormat, prov))
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInInnerFormatSpecificationAt0, i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.T_Formatb 'T(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.T_Format
                Case FormatAutomatState.singleQ ''
                    Select Case format(i)
                        Case "\" : state = FormatAutomatState.singleQb
                        Case "'"c : state = FormatAutomatState.nth
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInQuotedStringAt0, i + 1))
                        Case Else : Append.Invoke(format(i))
                    End Select
                Case FormatAutomatState.singleQb ''\
                    Append.Invoke(format(i))
                    state = FormatAutomatState.singleQ
                Case FormatAutomatState.doubleQ '"
                    Select Case format(i)
                        Case "\" : state = FormatAutomatState.singleQb
                        Case """"c : state = FormatAutomatState.nth
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInQuotedStringAt0, i + 1))
                        Case Else : Append.Invoke(format(i))
                    End Select
                Case FormatAutomatState.doubleQb '"\
                    Append.Invoke(format(i))
                    state = FormatAutomatState.doubleQ
                Case FormatAutomatState.Back   '\
                    Append.Invoke(format(i))
                    state = FormatAutomatState.nth
                Case FormatAutomatState.Open '(
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.Open2 : PreviousConditions.Push(New List(Of Boolean))
                        Case Else : Append.Invoke("("c) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.Pipe '|
                    PreviousConditions.Peek.Add(Conditions.Pop)
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.Open2
                        Case Else : Conditions.Push(True) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.Open2  '((, |(
                    Select Case format(i)
                        Case "T"c : state = FormatAutomatState.OpenT : CompareValue = TS
                        Case "h"c : state = FormatAutomatState.Openh
                        Case "m"c : state = FormatAutomatState.Openm
                        Case "s"c : state = FormatAutomatState.Opens
                        Case "d"c : state = FormatAutomatState.Opend
                        Case "l"c : state = FormatAutomatState.Openl
                        Case "|"c : state = FormatAutomatState.OpenPipe
                        Case "H"c : state = FormatAutomatState.AwaitComparison : CompareValue = TS.TotalHours
                        Case "M"c : state = FormatAutomatState.AwaitComparison : CompareValue = TS.TotalMinutes
                        Case "S"c : state = FormatAutomatState.AwaitComparison : CompareValue = TS.TotalSeconds
                        Case "D"c : state = FormatAutomatState.AwaitComparison : CompareValue = TS.TotalDays
                        Case "L"c : state = FormatAutomatState.AwaitComparison : CompareValue = TS.TotalMilliseconds
                        Case "t" : state = FormatAutomatState.AwaitComparison : CompareValue = TS.Ticks
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionLeftSide0, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedCharacterInFormatStringConditionLeftSide0At1, format(i), i + 1))
                    End Select
                Case FormatAutomatState.OpenPipe
                    Select Case format(i)
                        Case "H"c : state = FormatAutomatState.AwaitPipe : CompareValue = Abs(TS.TotalHours)
                        Case "M"c : state = FormatAutomatState.AwaitPipe : CompareValue = Abs(TS.TotalMinutes)
                        Case "S"c : state = FormatAutomatState.AwaitPipe : CompareValue = Abs(TS.TotalSeconds)
                        Case "L"c : state = FormatAutomatState.AwaitPipe : CompareValue = Abs(TS.TotalMilliseconds)
                        Case "D"c : state = FormatAutomatState.AwaitPipe : CompareValue = Abs(TS.TotalDays)
                        Case "t"c : state = FormatAutomatState.AwaitPipe : CompareValue = Abs(TS.Ticks)
                        Case "T"c : state = FormatAutomatState.OpenPipeT : CompareValue = If(TS < TimeSpan.Zero, -TS, TS)
                        Case "h"c : state = FormatAutomatState.OpenPipeh
                        Case "m"c : state = FormatAutomatState.OpenPipem
                        Case "s"c : state = FormatAutomatState.OpenPipes
                        Case "d"c : state = FormatAutomatState.OpenPiped
                        Case "l"c : state = FormatAutomatState.OpenPipel
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionLeftSideAbsoluteValue0, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedCharacterInFormatStringConditionLeftSideAbsoluteValue0At1, format(i), i + 1))
                    End Select
                Case FormatAutomatState.Openh
                    Select Case format(i)
                        Case "h"c : state = FormatAutomatState.AwaitComparison : CompareValue = TS.Hours
                        Case Else : CompareValue = Int(TS.TotalHours) : state = FormatAutomatState.AwaitComparison : i -= 1
                    End Select
                Case FormatAutomatState.Openm
                    Select Case format(i)
                        Case "m"c : state = FormatAutomatState.AwaitComparison : CompareValue = TS.Minutes
                        Case Else : CompareValue = Int(TS.TotalMinutes) : state = FormatAutomatState.AwaitComparison : i -= 1
                    End Select
                Case FormatAutomatState.Opens
                    Select Case format(i)
                        Case "s"c : state = FormatAutomatState.AwaitComparison : CompareValue = TS.Seconds
                        Case Else : CompareValue = Int(TS.TotalSeconds) : state = FormatAutomatState.AwaitComparison : i -= 1
                    End Select
                Case FormatAutomatState.Openl
                    Select Case format(i)
                        Case "l"c : state = FormatAutomatState.AwaitComparison : CompareValue = TS.Milliseconds
                        Case Else : CompareValue = Int(TS.TotalMilliseconds) : state = FormatAutomatState.AwaitComparison : i -= 1
                    End Select
                Case FormatAutomatState.Opend
                    Select Case format(i)
                        Case "d"c : state = FormatAutomatState.AwaitComparison : CompareValue = TS.Days
                        Case Else : CompareValue = Int(TS.TotalDays) : state = FormatAutomatState.AwaitComparison : i -= 1
                    End Select
                Case FormatAutomatState.OpenPipeh
                    Select Case format(i)
                        Case "h"c : state = FormatAutomatState.AwaitPipe : CompareValue = Abs(TS.Hours)
                        Case Else : CompareValue = Int(Abs(TS.TotalHours)) : state = FormatAutomatState.AwaitPipe : i -= 1
                    End Select
                Case FormatAutomatState.OpenPipel
                    Select Case format(i)
                        Case "l"c : state = FormatAutomatState.AwaitPipe : CompareValue = Abs(TS.Milliseconds)
                        Case Else : CompareValue = Int(Abs(TS.TotalMilliseconds)) : state = FormatAutomatState.AwaitPipe : i -= 1
                    End Select
                Case FormatAutomatState.OpenPipem
                    Select Case format(i)
                        Case "m"c : state = FormatAutomatState.AwaitPipe : CompareValue = Abs(TS.Minutes)
                        Case Else : CompareValue = Int(Abs(TS.TotalMinutes)) : state = FormatAutomatState.AwaitPipe : i -= 1
                    End Select
                Case FormatAutomatState.OpenPipes
                    Select Case format(i)
                        Case "s"c : state = FormatAutomatState.AwaitPipe : CompareValue = Abs(TS.Seconds)
                        Case Else : CompareValue = Int(Abs(TS.TotalSeconds)) : state = FormatAutomatState.AwaitPipe : i -= 1
                    End Select
                Case FormatAutomatState.OpenPiped
                    Select Case format(i)
                        Case "d"c : state = FormatAutomatState.AwaitPipe : CompareValue = Abs(TS.Days)
                        Case Else : CompareValue = Int(Abs(TS.TotalDays)) : state = FormatAutomatState.AwaitPipe : i -= 1
                    End Select
                Case FormatAutomatState.AwaitPipe
                    Select Case format(i)
                        Case "|"c : state = FormatAutomatState.AwaitComparison
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionLeftSideAbsoluteValueAt0Expected1, i + 1, "|"c))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedCharacter0InConditionLeftSideAbsoluteValueAt1Expected2, format(i), i + 1, "|"c))
                    End Select
                Case FormatAutomatState.AwaitComparison
                    Select Case format(i)
                        Case "<"c : state = FormatAutomatState.Clt
                        Case ">"c : state = FormatAutomatState.Cgt
                        Case "="c : state = FormatAutomatState.AwaitNumber : CompareOperator = ComparisonOperators.Equal
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0ExpectedComparisonOperator, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedCharacter0InConditionAt1ExpectedComparisonOperator, format(i), i + 1))
                    End Select
                Case FormatAutomatState.Clt
                    Select Case format(i)
                        Case ">"c : state = FormatAutomatState.AwaitNumber : CompareOperator = ComparisonOperators.NotEqual
                        Case "="c : state = FormatAutomatState.AwaitNumber : CompareOperator = ComparisonOperators.LessEqual
                        Case Else : state = FormatAutomatState.AwaitNumber : CompareOperator = ComparisonOperators.Less : i -= 1
                    End Select
                Case FormatAutomatState.Cgt
                    Select Case format(i)
                        Case "="c : state = FormatAutomatState.AwaitNumber : CompareOperator = ComparisonOperators.GreaterEqual
                        Case Else : state = FormatAutomatState.AwaitNumber : CompareOperator = ComparisonOperators.Greater : i -= 1
                    End Select
                Case FormatAutomatState.AwaitNumber
                    NumberBuilder = 0
                    DecimalPlace = 1
                    Select Case format(i)
                        Case "-"c : state = FormatAutomatState.AfterMinus
                        Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c : NumberBuilder = format(i).NumericValue : state = FormatAutomatState.BeforeDot
                        Case "."c : state = FormatAutomatState.AfterDot
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberExpected, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberExpected, format(i), i + 1))
                    End Select
                Case FormatAutomatState.AfterMinus
                    Select Case format(i)
                        Case "0"c : state = FormatAutomatState.BeforeDot : NumberBuilder = Double.NegativeInfinity
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c : state = FormatAutomatState.BeforeDot : NumberBuilder = -format(i).NumericValue
                        Case "."c : state = FormatAutomatState.AfterDot : NumberBuilder = Double.NegativeInfinity
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberExpected, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberExpected, format(i), i + 1))
                    End Select
                Case FormatAutomatState.BeforeDot
                    Select Case format(i)
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                            If NumberBuilder <> Double.NegativeInfinity Then NumberBuilder = NumberBuilder * 10 + format(i).NumericValue * Sign(NumberBuilder) _
                            Else NumberBuilder = -format(i).NumericValue
                        Case "0"c : If NumberBuilder <> Double.NegativeInfinity Then NumberBuilder *= 10
                        Case "."c : state = FormatAutomatState.AfterDot
                        Case ")"c : Conditions.Push(GetCondition(CompareValue, NumberBuilder, CompareOperator)) : state = FormatAutomatState.nth
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0ExpectedNumberOr, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1ExpectedNumberOr, format(i), i + 1))
                    End Select
                Case FormatAutomatState.AfterDot
                    Select Case format(i)
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                            If NumberBuilder = Double.NegativeInfinity Then NumberBuilder = format(i).NumericValue * 10 ^ -DecimalPlace _
                            Else NumberBuilder += Sign(NumberBuilder) * format(i).NumericValue * 10 ^ -DecimalPlace
                            DecimalPlace += 1
                        Case "0"c : DecimalPlace += 1
                        Case ")"c : NumberBuilder = If(NumberBuilder = Double.NegativeInfinity, 0, NumberBuilder) : Conditions.Push(GetCondition(CompareValue, NumberBuilder, CompareOperator)) : state = FormatAutomatState.nth
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0ExpectedNumberOr, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1ExpectedNumberOr, format(i), i + 1))
                    End Select
                Case FormatAutomatState.OpenPipeT
                    Select Case format(i)
                        Case "|"c : state = FormatAutomatState.OpenT
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionLeftSideAbsoluteValueAt0Expected1, i + 1, "|"c))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedCharacter0InConditionLeftSideAbsoluteValueAt1Expected2, format(i), i + 1, "|"c))
                    End Select
                Case FormatAutomatState.OpenT
                    Select Case format(i)
                        Case "<"c : state = FormatAutomatState.Tlt
                        Case ">"c : state = FormatAutomatState.Tgt
                        Case "="c : state = FormatAutomatState.AwaitNumber : CompareOperator = ComparisonOperators.Equal
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0ExpectedComparisonOperator, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedCharacter0InConditionAt1ExpectedComparisonOperator, format(i), i + 1))
                    End Select
                Case FormatAutomatState.Tlt
                    Select Case format(i)
                        Case ">"c : state = FormatAutomatState.TAwait1 : CompareOperator = ComparisonOperators.NotEqual
                        Case "="c : state = FormatAutomatState.TAwait1 : CompareOperator = ComparisonOperators.LessEqual
                        Case Else : state = FormatAutomatState.TAwait1 : CompareOperator = ComparisonOperators.Less : i -= 1
                    End Select
                Case FormatAutomatState.Tgt
                    Select Case format(i)
                        Case "="c : state = FormatAutomatState.TAwait1 : CompareOperator = ComparisonOperators.GreaterEqual
                        Case Else : state = FormatAutomatState.TAwait1 : CompareOperator = ComparisonOperators.Greater : i -= 1
                    End Select
                Case FormatAutomatState.TAwait1
                    NegativeZero = False : TimeSpanBuilder = TimeSpan.Zero
                    Select Case format(i)
                        Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c : TimeSpanBuilder = TimeSpan.FromDays(format(i).NumericValue) : state = FormatAutomatState.TNumber1
                        Case "-"c : state = FormatAutomatState.TMinus
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberExpected, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberExpected, format(i), i + 1))
                    End Select
                Case FormatAutomatState.TMinus
                    Select Case format(i)
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c : TimeSpanBuilder = TimeSpan.FromDays(-format(i).NumericValue) : state = FormatAutomatState.TNumber1
                        Case "0"c : state = FormatAutomatState.TNumber1 : NegativeZero = True
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberExpected, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberExpected, format(i), i + 1))
                    End Select
                Case FormatAutomatState.TNumber1
                    Select Case format(i)
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                            If NegativeZero Then TimeSpanBuilder = TimeSpan.FromDays(-format(i).NumericValue) : NegativeZero = False _
                            Else TimeSpanBuilder = TimeSpan.FromDays(TimeSpanBuilder.Days * 10 + If(TimeSpanBuilder < TimeSpan.Zero, -1, 1) * format(i).NumericValue)
                        Case "0"c : If Not NegativeZero Then TimeSpanBuilder = TimeSpan.FromDays(If(TimeSpanBuilder < TimeSpan.Zero, -1, 1) * TimeSpanBuilder.Days * 10)
                        Case "."c : state = FormatAutomatState.TAwaitH
                        Case ":"c : TimeSpanBuilder = TimeSpan.FromHours(If(TimeSpanBuilder < TimeSpan.Zero, -1, 1) * TimeSpanBuilder.Days) : state = FormatAutomatState.TAwaitM
                        Case ")"c : TimeSpanBuilder = TimeSpan.FromHours(If(TimeSpanBuilder < TimeSpan.Zero, -1, 1) * TimeSpanBuilder.Days) : Conditions.Push(GetCondition(TimeSpanBuilder, CompareValue, CompareOperator)) : state = FormatAutomatState.nth
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberOrExpected, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberOrExpected, format(i), i + 1))
                    End Select
                Case FormatAutomatState.TAwaitH
                    NumberBuilder = 0
                    Select Case format(i)
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "0"c : NumberBuilder = format(i).NumericValue : state = FormatAutomatState.TH
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberExpected, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberExpected, format(i), i + 1))
                    End Select
                Case FormatAutomatState.TH
                    Select Case format(i)
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "0"c : NumberBuilder = NumberBuilder * 10 + format(i).NumericValue
                        Case ")"c
                            If NegativeZero AndAlso NumberBuilder <> 0 Then NegativeZero = False : TimeSpanBuilder = TimeSpan.FromHours(-NumberBuilder) _
                            Else TimeSpanBuilder = TimeSpan.FromHours(TimeSpanBuilder.TotalHours + If(TimeSpanBuilder < TimeSpan.Zero, -1, 1) * NumberBuilder)
                            If NegativeZero Then TimeSpanBuilder = TimeSpan.Zero
                            Conditions.Push(GetCondition(TimeSpanBuilder, CompareValue, CompareOperator)) : state = FormatAutomatState.nth
                        Case ":"c
                            If NegativeZero AndAlso NumberBuilder <> 0 Then NegativeZero = False : TimeSpanBuilder = TimeSpan.FromHours(-NumberBuilder) _
                            Else TimeSpanBuilder = TimeSpan.FromHours(TimeSpanBuilder.TotalHours + If(TimeSpanBuilder < TimeSpan.Zero, -1, 1) * NumberBuilder)
                            state = FormatAutomatState.TAwaitM
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberOrExpected_, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberOrExpected_, format(i), i + 1))
                    End Select
                Case FormatAutomatState.TAwaitM
                    NumberBuilder = 0
                    Select Case format(i)
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "0"c : NumberBuilder = format(i).NumericValue : state = FormatAutomatState.TM
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberExpected, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberExpected, format(i), i + 1))
                    End Select
                Case FormatAutomatState.TM
                    Select Case format(i)
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "0"c : NumberBuilder = NumberBuilder * 10 + format(i).NumericValue
                        Case ")"c
                            If NegativeZero AndAlso NumberBuilder <> 0 Then NegativeZero = False : TimeSpanBuilder = TimeSpan.FromMinutes(-NumberBuilder) _
                            Else TimeSpanBuilder = TimeSpan.FromMinutes(TimeSpanBuilder.TotalMinutes + If(TimeSpanBuilder < TimeSpan.Zero, -1, 1) * NumberBuilder)
                            If NegativeZero Then TimeSpanBuilder = TimeSpan.Zero
                            Conditions.Push(GetCondition(TimeSpanBuilder, CompareValue, CompareOperator)) : state = FormatAutomatState.nth
                        Case ":"c
                            If NegativeZero AndAlso NumberBuilder <> 0 Then NegativeZero = False : TimeSpanBuilder = TimeSpan.FromMinutes(-NumberBuilder) _
                            Else TimeSpanBuilder = TimeSpan.FromMinutes(TimeSpanBuilder.TotalMinutes + If(TimeSpanBuilder < TimeSpan.Zero, -1, 1) * NumberBuilder)
                            state = FormatAutomatState.TAwaitS
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberOrExpected_, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberOrExpected_, format(i), i + 1))
                    End Select
                Case FormatAutomatState.TAwaitS
                    NumberBuilder = 0
                    Select Case format(i)
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "0"c : NumberBuilder = format(i).NumericValue : state = FormatAutomatState.TS
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberExpected, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberExpected, format(i), i + 1))
                    End Select
                Case FormatAutomatState.TS
                    Select Case format(i)
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "0"c : NumberBuilder = NumberBuilder * 10 + format(i).NumericValue
                        Case ")"c
                            If NegativeZero AndAlso NumberBuilder <> 0 Then NegativeZero = False : TimeSpanBuilder = TimeSpan.FromSeconds(-NumberBuilder) _
                            Else TimeSpanBuilder = TimeSpan.FromSeconds(TimeSpanBuilder.TotalSeconds + If(TimeSpanBuilder < TimeSpan.Zero, -1, 1) * NumberBuilder)
                            If NegativeZero Then TimeSpanBuilder = TimeSpan.Zero
                            Conditions.Push(GetCondition(TimeSpanBuilder, CompareValue, CompareOperator)) : state = FormatAutomatState.nth
                        Case "."c
                            If NegativeZero AndAlso NumberBuilder <> 0 Then NegativeZero = False : TimeSpanBuilder = TimeSpan.FromSeconds(-NumberBuilder) _
                            Else TimeSpanBuilder = TimeSpan.FromSeconds(TimeSpanBuilder.TotalSeconds + If(TimeSpanBuilder < TimeSpan.Zero, -1, 1) * NumberBuilder)
                            state = FormatAutomatState.TAwaitL
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberOrExpected__, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberOrExpected__, format(i), i + 1))
                    End Select
                Case FormatAutomatState.TAwaitL
                    NumberBuilder = 0 : DecimalPlace = 1
                    Select Case format(i)
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "0"c : NumberBuilder = format(i).NumericValue / 10 : state = FormatAutomatState.TS : DecimalPlace += 1
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberExpected, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberExpected, format(i), i + 1))
                    End Select
                Case FormatAutomatState.TL
                    Select Case format(i)
                        Case "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "0"c : NumberBuilder += format(i).NumericValue ^ -DecimalPlace : DecimalPlace += 1
                        Case ")"c
                            If NegativeZero AndAlso NumberBuilder <> 0 Then NegativeZero = False : TimeSpanBuilder = TimeSpan.FromSeconds(-NumberBuilder) _
                            Else TimeSpanBuilder = TimeSpan.FromSeconds(TimeSpanBuilder.TotalSeconds + If(TimeSpanBuilder < TimeSpan.Zero, -1, 1) * NumberBuilder)
                            If NegativeZero Then TimeSpanBuilder = TimeSpan.Zero
                            Conditions.Push(GetCondition(TimeSpanBuilder, CompareValue, CompareOperator)) : state = FormatAutomatState.nth
                        Case NullChar : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFormatStringInConditionAt0NumberExpected, i + 1))
                        Case Else : Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnextectedCharacter0InConditionAt1NumberExpected, format(i), i + 1))
                    End Select
                Case Else : Stop 'This must not happen!
            End Select
        Next i
        If Conditions.Count > 1 Then Throw New FormatException(String.Format(ResourcesT.Exceptions.SyntaxErrorInFormatStringUnexpectedEndOfFomatStringAt0ExpectedForCondition, format.Length))
        Return ret.ToString
    End Function
    ''' <summary>Creates boolean condition for <see cref="TimeSpan"/></summary>
    ''' <param name="v">Value to be compared at left side</param>
    ''' <param name="CompareTo">Value to be compared at right side</param>
    ''' <param name="op">Comparizon operator</param>
    ''' <returns>Result of comparison <paramref name="Value"/> <paramref name="op"/> <paramref name="CompareTo"/></returns>
    ''' <exception cref="InvalidEnumArgumentException"><paramref name="op"/> is not memebr of <see cref="ComparisonOperators"/></exception>
    Private Shared Function GetCondition(ByVal v As TimeSpan, ByVal compareto As TimeSpan, ByVal op As ComparisonOperators) As Boolean
        Select Case op
            Case ComparisonOperators.Equal : Return v = compareto
            Case ComparisonOperators.Greater : Return v > compareto
            Case ComparisonOperators.GreaterEqual : Return v >= compareto
            Case ComparisonOperators.Less : Return v < compareto
            Case ComparisonOperators.LessEqual : Return v < compareto
            Case ComparisonOperators.NotEqual : Return v <> compareto
            Case Else : Throw New InvalidEnumArgumentException("op", op, op.GetType)
        End Select
    End Function
    ''' <summary>Creates boolean condition for number</summary>
    ''' <param name="Value">Value to be compared at left side</param>
    ''' <param name="CompareTo">Value to be compared at right side</param>
    ''' <param name="op">Comparizon operator</param>
    ''' <returns>Result of comparison <paramref name="Value"/> <paramref name="op"/> <paramref name="CompareTo"/></returns>
    ''' <exception cref="InvalidEnumArgumentException"><paramref name="op"/> is not memebr of <see cref="ComparisonOperators"/></exception>
    ''' <exception cref="ArgumentException"><paramref name="Value"/> is not of one of following types: <see cref="Integer"/>, <see cref="Long"/>, <see cref="Single"/>, <see cref="Double"/></exception>
    Private Shared Function GetCondition(ByVal Value As IConvertible, ByVal CompareTo As Double, ByVal op As ComparisonOperators) As Boolean
        If TypeOf Value Is Integer Then
            Dim v As Integer = Value
            Select Case op
                Case ComparisonOperators.Equal : Return v = CompareTo
                Case ComparisonOperators.Greater : Return v > CompareTo
                Case ComparisonOperators.GreaterEqual : Return v >= CompareTo
                Case ComparisonOperators.Less : Return v < CompareTo
                Case ComparisonOperators.LessEqual : Return v < CompareTo
                Case ComparisonOperators.NotEqual : Return v <> CompareTo
                Case Else : Throw New InvalidEnumArgumentException("op", op, op.GetType)
            End Select
        ElseIf TypeOf Value Is Long Then
            Dim v As Long = Value
            Select Case op
                Case ComparisonOperators.Equal : Return v = CompareTo
                Case ComparisonOperators.Greater : Return v > CompareTo
                Case ComparisonOperators.GreaterEqual : Return v >= CompareTo
                Case ComparisonOperators.Less : Return v < CompareTo
                Case ComparisonOperators.LessEqual : Return v < CompareTo
                Case ComparisonOperators.NotEqual : Return v <> CompareTo
                Case Else : Throw New InvalidEnumArgumentException("op", op, op.GetType)
            End Select
        ElseIf TypeOf Value Is Single Then
            Dim v As Single = Value
            Select Case op
                Case ComparisonOperators.Equal : Return v = CompareTo
                Case ComparisonOperators.Greater : Return v > CompareTo
                Case ComparisonOperators.GreaterEqual : Return v >= CompareTo
                Case ComparisonOperators.Less : Return v < CompareTo
                Case ComparisonOperators.LessEqual : Return v < CompareTo
                Case ComparisonOperators.NotEqual : Return v <> CompareTo
                Case Else : Throw New InvalidEnumArgumentException("op", op, op.GetType)
            End Select
        ElseIf TypeOf Value Is Double Then
            Dim v As Double = Value
            Select Case op
                Case ComparisonOperators.Equal : Return v = CompareTo
                Case ComparisonOperators.Greater : Return v > CompareTo
                Case ComparisonOperators.GreaterEqual : Return v >= CompareTo
                Case ComparisonOperators.Less : Return v < CompareTo
                Case ComparisonOperators.LessEqual : Return v < CompareTo
                Case ComparisonOperators.NotEqual : Return v <> CompareTo
                Case Else : Throw New InvalidEnumArgumentException("op", op, op.GetType)
            End Select
        Else : Throw New ArgumentException(String.Format(ResourcesT.Exceptions.UnsupportedTypeForComparison0, Value.GetType.Name), "Value")
        End If
    End Function
    ''' <summary>Comparison operators for format string conditions</summary>
    Private Enum ComparisonOperators
        ''' <summary>Less than (&lt;)</summary>
        Less
        ''' <summary>Greater than (>)</summary>
        Greater
        ''' <summary>Equal to (=)</summary>
        Equal
        ''' <summary>Less than or equal to (&lt;=)</summary>
        LessEqual
        ''' <summary>Greater than or equal to (>=)</summary>
        GreaterEqual
        ''' <summary>Not equal to (&lt;)</summary>
        NotEqual
    End Enum
#End Region
End Structure
#End If
