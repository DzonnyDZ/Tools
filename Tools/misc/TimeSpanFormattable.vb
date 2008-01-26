Imports System.Text, system.Math
#If Config <= rc Then 'Stage:RC
''' <summary><see cref="TimeSpan"/> that implements <see cref="IFormattable"/></summary>
<Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
<DebuggerDisplay("{ToString}")> _
<Version(1, 0, GetType(TimeSpanFormattable), LastChange:="10/15/2007")> _
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
    ''' <summary>Indicates whether two <see cref="TimeSpanFormattable"/> instances are equal.</summary>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the values of <paramref name="t1"/> and <paramref name="t2"/> are equal; otherwise, false.</returns>
    Public Shared Operator =(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1.Inner = t2.Inner
    End Operator
    ''' <summary>Indicates whether two <see cref="TimeSpanFormattable"/> instances are not equal.</summary>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the values of <paramref name="t1"/> and <paramref name="t2"/> are not equal; otherwise, false.</returns>
    Public Shared Operator <>(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1.Inner <> t2.Inner
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpanFormattable"/> is less than another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is less than the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator <(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1.Inner < t2.Inner
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpanFormattable"/> is less than or equal to another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is less than or equal to the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator <=(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1.Inner <= t2.Inner
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpanFormattable"/> is greater than another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is greater than the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator >(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1.Inner > t2.Inner
    End Operator
    ''' <summary>Indicates whether a specified <see cref="TimeSpanFormattable"/> is greater than or equal to another specified <see cref="TimeSpanFormattable"/>.</summary>
    ''' <param name="t2">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <param name="t1">A <see cref="TimeSpanFormattable"/>.</param>
    ''' <returns>true if the value of <paramref name="t1"/> is greater than or equal to the value of <paramref name="t2"/>; otherwise, false.</returns>
    Public Shared Operator >=(ByVal t1 As TimeSpanFormattable, ByVal t2 As TimeSpanFormattable) As Boolean
        Return t1.Inner >= t2.Inner
    End Operator
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
    '''     <item><term>T (long time pattern)</term>-hh:mm:ss</item>
    ''' </list>
    ''' <list type="table">
    '''     <listheader>Custom format strings</listheader>
    '''     <listheader><term>Format string</term><description>Description</description></listheader>
    '''     <item><term>d, dd, ddd, ...</term>
    '''         <description>Any number of lowercase ds represents days (value of <see cref="Days"/>). Number of ds determines minimal number of digits that will represent number of days.</description>
    '''     </item>
    '''     <item><term>d()</term>
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
    '''     <item><term>H()</term>
    '''         <description>Custom-formated fractional hours. Value of <see cref="TotalHours"/> formated with format specified in braces. For more information about formats in braces see below.</description>
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
    '''     <item><term>L()</term>
    '''         <description>Custom-formated fractional milliseconds. Value of <see cref="TotalMilliseconds"/> formated with formate specified in braces. For more information about formats in braces see below.</description>
    '''     </item>
    '''     <item><term>t, tt, ttt, ...</term>
    '''         <description>Any number of owercase ts represents value of <see cref="Ticks"/>. The umber of ts determines minimal number of digits copyed to output.</description>
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
    ''' <para>Note: Format string is parsed from left to right by finite deterministic state automaton and thus format string like mmm on time span of value 1:14:00 will produce "1414" etc.</para>
    ''' <para>Note: All numbers are rendered as absolute value, you must use +/- to display a sign</para>
    ''' <para>See also: Custom numeric formats <seealso>http://msdn2.microsoft.com/en-us/library/0c899ak8.aspx</seealso>, Standard numeric formats <seealso>http://msdn2.microsoft.com/en-us/library/dwhawy9k.aspx</seealso></para>
    ''' </remarks>
    ''' <exception cref="FormatException">Unknown predefined format -or- syntax error in format string</exception>
    ''' <exception cref="ArgumentOutOfRangeException">The 'T()' patter is used on negative <see cref="TimeSpanFormattable"/> or value of current <see cref="TimeSpanFormattable"/> added to <see cref="DateTime.MinValue"/> causes <see cref="DateTime.MaxValue"/> to be exceeded.</exception>
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

    ''' <summary>Gets custom formats that represents given predefined format</summary>
    ''' <param name="format">Predefined format</param>
    ''' <returns>Custom format that predefined format <paramref name="format"/> expands to.</returns>
    ''' <exception cref="FormatException">Given predefined format <paramref name="format"/> is not known. Know predefined formats are G, g, T and t.</exception>
    ''' <remarks>For more information about formating <see cref="TimeSpanFormattable"/> see <seealso cref="ToString"/></remarks>
    Private Shared Function ExpandPredefinedFormat(ByVal format As Char) As String
        Select Case format
            Case "g"c : Return "-h(00):mm"
            Case "G"c : Return "-h(00):mm:ss"
            Case "t"c : Return "-hh:mm"
            Case "T"c : Return "-hh:mm:ss"
            Case Else : Throw New FormatException("Unknow predefined format")
        End Select
    End Function

    ''' <summary>State of finete state deterministic automaton used to parse custom format string in <see cref="FormatCustomized"/></summary>
    Private Enum FormatAutomatState
        ''' <summary>Normal state</summary>
        nth
#Region "d"
        ''' <summary>After d</summary>
        d1
        ''' <summary>After dd</summary>
        d2
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
    End Enum



    ''' <summary>Handles backslash in nested format in braces. Extracted repeatedly used part of <see cref="FormatCustomized"/>.</summary>
    ''' <param name="format">Format string</param>
    ''' <param name="InFormat">Nested format string being produced.</param>
    ''' <param name="i">Current position in <paramref name="format"/></param>
    Private Shared Sub PartCustomFormatBS(ByVal format As String, ByRef InFormat As String, ByVal i As Integer)
        Select Case format(i)
            Case "\"c, ")"c
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
    ''' <exception cref="ArgumentOutOfRangeException">The 'T()' patter is used on negative <see cref="TimeSpanFormattable"/> or value of current <see cref="TimeSpanFormattable"/> added to <see cref="DateTime.MinValue"/> causes <see cref="DateTime.MaxValue"/> to be exceeded.</exception>
    Private Shared Function FormatCustomized(ByVal TS As TimeSpan, ByVal format As String, ByVal prov As System.IFormatProvider) As String
        Dim ret As New StringBuilder
        If format.Length = 2 AndAlso format(0) = "%"c Then format = format.Substring(1)
        format = format.Replace(ChrW(0), "\" & ChrW(0))
        format &= CChar(vbNullChar)
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
        For i As Integer = 0 To format.Length - 1
            Select Case state
                Case FormatAutomatState.nth 'Basic
                    Select Case format(i)
                        Case "d"c : state = FormatAutomatState.d1 : StartIndex = i
                        Case "D"c : state = FormatAutomatState.D_
                        Case "["c : state = FormatAutomatState.leftB
                        Case "h"c : state = FormatAutomatState.h1
                        Case "H"c : state = FormatAutomatState.H_
                        Case "m"c : state = FormatAutomatState.m1
                        Case "M"c : state = FormatAutomatState.M_
                        Case "s"c : state = FormatAutomatState.s1
                        Case "S"c : state = FormatAutomatState.S_
                        Case "l"c : state = FormatAutomatState.l1
                        Case "L"c : state = FormatAutomatState.L_
                        Case "t"c : state = FormatAutomatState.t1 : StartIndex = i
                        Case "T"c : state = FormatAutomatState.T_
                        Case ":"c : ret.Append(TProvider.TimeSeparator)
                        Case """"c : state = FormatAutomatState.doubleQ : StartIndex = i
                        Case "'"c : state = FormatAutomatState.singleQ : StartIndex = i
                        Case "\"c : state = FormatAutomatState.Back
                        Case "."c : ret.Append(NProvider.NumberDecimalSeparator)
                        Case "-"c : If TS.TotalMilliseconds < 0 Then ret.Append(NProvider.NegativeSign)
                        Case "+"
                            If TS.TotalMilliseconds < 0 Then
                                ret.Append(NProvider.NegativeSign)
                            ElseIf TS.TotalMilliseconds > 0 Then
                                ret.Append(NProvider.PositiveSign)
                            End If
                        Case ChrW(0) : Exit For
                        Case Else : ret.Append(format(i))
                    End Select
                Case FormatAutomatState.d1 'd
                    Select Case format(i)
                        Case "d"c : state = FormatAutomatState.d2
                        Case "("c : state = FormatAutomatState.dFormat : InFormat = ""
                        Case Else
                            i -= 1
                            ret.Append(Abs(TS.Days).ToString("0", prov))
                            state = FormatAutomatState.nth
                    End Select
                Case FormatAutomatState.d2 'dd
                    Select Case format(i)
                        Case "d"c
                        Case Else
                            i -= 1
                            ret.Append(Abs(TS.Days).ToString(New String("0"c, i - StartIndex + 1), prov))
                            state = FormatAutomatState.nth
                    End Select
                Case FormatAutomatState.dFormat 'd(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.dFormatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            ret.Append(Abs(TS.Days).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.dFormatb 'd(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.dFormat
                Case FormatAutomatState.D_ 'D
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.D_Format : InFormat = ""
                        Case Else : Throw New FormatException(String.Format("Syntax error in format string. 'D' must be followed with '(' at {0}.", i + 1))
                    End Select
                Case FormatAutomatState.D_Format 'D(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.D_Formatb
                        Case ")"c : state = FormatAutomatState.nth
                            ret.Append(Abs(TS.TotalDays).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.D_Formatb 'D(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.D_Format
                Case FormatAutomatState.leftB '[
                    Select Case format(i)
                        Case "d"c : state = FormatAutomatState.leftBd1
                        Case "D"c : state = FormatAutomatState.leftBD_
                        Case Else : Throw New FormatException(String.Format("Syntax error in format string. '[' must be followed with 'd' or 'D' at {0}.", i + 1))
                    End Select
                Case FormatAutomatState.leftBd1 '[d
                    Select Case format(i)
                        Case "d"c : state = FormatAutomatState.leftBd2
                        Case "("c : state = FormatAutomatState.leftBdFormat : InFormat = ""
                        Case Else
                            i -= 1
                            If TS.Days <> 0 Then ret.Append(Abs(TS.Days).ToString("0", prov))
                            state = FormatAutomatState.leftBEnd
                    End Select
                Case FormatAutomatState.leftBd2 '[dd
                    Select Case format(i)
                        Case "d"c
                        Case Else
                            i -= 1
                            If TS.Days <> 0 Then _
                                ret.Append(Abs(TS.Days).ToString(New String("0"c, i - StartIndex + 1), prov))
                            state = FormatAutomatState.leftBEnd
                    End Select
                Case FormatAutomatState.leftBdFormat '[d(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.leftBdFormatb
                        Case ")"c
                            state = FormatAutomatState.leftBEnd
                            If TS.Days <> 0 Then _
                                ret.Append(Abs(TS.Days).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.leftBdFormatb '[d(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.leftBdFormat
                Case FormatAutomatState.leftBD_ '[D
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.leftBD_Format : InFormat = ""
                        Case Else : Throw New FormatException(String.Format("Syntax error in format string. 'D' must be followed with '(' at {0}.", i + 1))
                    End Select
                Case FormatAutomatState.leftBD_Format '[D(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.leftBD_Formatb
                        Case ")"c : state = FormatAutomatState.leftBEnd
                            If TS.Days <> 0 Then _
                                ret.Append(Abs(TS.TotalDays).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
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
                            Throw New FormatException(String.Format("Syntax error in formast string. ']' expected at {0}.", i + 1))
                    End Select
                Case FormatAutomatState.h1 'h
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.hFormat : InFormat = ""
                        Case "h" : ret.Append(Abs(TS.Hours).ToString("00", prov)) : state = FormatAutomatState.nth
                        Case Else : ret.Append(Abs(TS.Hours).ToString("0", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.H_ 'H
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.H_Format : InFormat = ""
                        Case Else : Throw New FormatException(String.Format("Syntax error in format string. 'H' must be followed with '(' at {0}.", i + 1))
                    End Select
                Case FormatAutomatState.hFormat 'h(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.hFormatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            ret.Append(Truncate(Abs(TS.TotalHours)).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.hFormatb 'h(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.hFormat
                Case FormatAutomatState.H_Format 'H(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.H_Formatb
                        Case ")"c : state = FormatAutomatState.nth
                            ret.Append(Abs(TS.TotalHours).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.H_Formatb 'H(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.H_Format
                Case FormatAutomatState.m1 'm
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.mFormat : InFormat = ""
                        Case "m" : ret.Append(Abs(TS.Minutes).ToString("00", prov)) : state = FormatAutomatState.nth
                        Case Else : ret.Append(Abs(TS.Minutes).ToString("0", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.M_ 'M
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.M_Format : InFormat = ""
                        Case Else : Throw New FormatException(String.Format("Syntax error in format string. 'M' must be followed with '(' at {0}.", i + 1))
                    End Select
                Case FormatAutomatState.mFormat 'm(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.mFormatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            ret.Append(Truncate(Abs(TS.TotalMinutes)).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.mFormatb 'm(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.mFormat
                Case FormatAutomatState.M_Format 'M(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.M_Formatb
                        Case ")"c : state = FormatAutomatState.nth
                            ret.Append(Abs(TS.TotalMinutes).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.M_Formatb 'M(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.M_Format
                Case FormatAutomatState.s1 's
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.sFormat : InFormat = ""
                        Case "s" : ret.Append(Abs(TS.Seconds).ToString("00", prov)) : state = FormatAutomatState.nth
                        Case Else : ret.Append(Abs(TS.Seconds).ToString("0", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.S_ 'S
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.S_Format : InFormat = ""
                        Case Else : Throw New FormatException(String.Format("Syntax error in format string. 'S' must be followed with '(' at {0}.", i + 1))
                    End Select
                Case FormatAutomatState.sFormat 's(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.sFormatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            ret.Append(Truncate(Abs(TS.TotalSeconds)).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.sFormatb 's(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.sFormat
                Case FormatAutomatState.S_Format 'S(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.M_Formatb
                        Case ")"c : state = FormatAutomatState.nth
                            ret.Append(Abs(TS.TotalSeconds).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.M_Formatb 'S(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.S_Format
                Case FormatAutomatState.l1 'l
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.sFormat : InFormat = ""
                        Case "l" : state = FormatAutomatState.l2
                        Case Else : ret.Append(Abs(TS.Milliseconds).ToString("0", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.l2 'll
                    Select Case format(i)
                        Case "l" : ret.Append(Abs(TS.Milliseconds).ToString("000", prov)) : state = FormatAutomatState.nth
                        Case Else : ret.Append(Abs(TS.Milliseconds).ToString("00", prov)) : state = FormatAutomatState.nth : i -= 1
                    End Select
                Case FormatAutomatState.L_ 'L
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.L_Format : InFormat = ""
                        Case Else : Throw New FormatException(String.Format("Syntax error in format string. 'L' must be followed with '(' at {0}.", i + 1))
                    End Select
                Case FormatAutomatState.lFormat 'l(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.lFormatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            ret.Append(Truncate(Abs(TS.TotalMilliseconds)).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.lFormatb 'l(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.lFormat
                Case FormatAutomatState.L_Format 'L(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.L_Formatb
                        Case ")"c : state = FormatAutomatState.nth
                            ret.Append(Abs(TS.TotalMilliseconds).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
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
                            ret.Append(Abs(TS.Ticks).ToString("0", prov))
                            state = FormatAutomatState.nth
                    End Select
                Case FormatAutomatState.t2 'tt
                    Select Case format(i)
                        Case "t"c
                        Case Else
                            i -= 1
                            ret.Append(Abs(TS.Ticks).ToString(New String("0"c, i - StartIndex + 1), prov))
                            state = FormatAutomatState.nth
                    End Select
                Case FormatAutomatState.T_ 'T
                    Select Case format(i)
                        Case "("c : state = FormatAutomatState.T_Format
                        Case Else : Throw New FormatException(String.Format("Syntax error in format string. 'T' must be followed by '(' at {0}.", i + 1))
                    End Select
                Case FormatAutomatState.tFormat 't(
                    Select Case format(i)
                        Case "\"c : state = FormatAutomatState.tFormatb
                        Case ")"c
                            state = FormatAutomatState.nth
                            ret.Append(Abs(TS.Ticks).ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
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
                            ret.Append(used2.ToString(InFormat, prov))
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in inner format specification at {0}.", i + 1))
                        Case Else : InFormat &= format(i)
                    End Select
                Case FormatAutomatState.T_Formatb 'T(\
                    PartCustomFormatBS(format, InFormat, i)
                    state = FormatAutomatState.T_Format
                Case FormatAutomatState.singleQ ''
                    Select Case format(i)
                        Case "\" : state = FormatAutomatState.singleQb
                        Case "'"c : state = FormatAutomatState.nth
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in quoted string at {0}.", i + 1))
                        Case Else : ret.Append(format(i))
                    End Select
                Case FormatAutomatState.singleQb ''\
                    ret.Append(format(i))
                    state = FormatAutomatState.singleQ
                Case FormatAutomatState.doubleQ '"
                    Select Case format(i)
                        Case "\" : state = FormatAutomatState.singleQb
                        Case """"c : state = FormatAutomatState.nth
                        Case ChrW(0) : Throw New FormatException(String.Format("Syntax error in format string. Unexpected end of format string in quoted string at {0}.", i + 1))
                        Case Else : ret.Append(format(i))
                    End Select
                Case FormatAutomatState.doubleQb
                    ret.Append(format(i))
                    state = FormatAutomatState.doubleQ
                Case FormatAutomatState.Back
                    ret.Append(format(i))
                    state = FormatAutomatState.nth
                Case Else : Stop 'This must not happen!
            End Select
        Next i
        Return ret.ToString
    End Function
#End Region
End Structure
#End If
