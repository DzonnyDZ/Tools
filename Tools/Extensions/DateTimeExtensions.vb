Imports System.Runtime.CompilerServices

#If Config <= Nightly Then 'Stage:Nightly
Namespace ExtensionsT
    ''' <summary>Provides extension methods for working with date and time values</summary>
    ''' <version version="1.5.3">This module is new in version 1.5.3</version>
    Public Module DateTimeExtensions
#Region "Unix timestamp"
        ''' <summary>Date and time of Unix Epoch (date and time of Unix timestamp 0) - it's Jan 1 1970 0:00:00.0 UTC</summary>
        Public ReadOnly unixEpoch As Date = New Date(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)

        ''' <summary>Converts <see cref="DateTime"/> value to Unix timestamp (number of seconds elapsed from Jan 1st 1970 0:00:00 UTC)</summary>
        ''' <param name="value">A <see cref="DateTime"/> value. It's converted to UTC time using <see cref="DateTime.ToUniversalTime"/>.</param>
        ''' <returns>A long number representing Unix timestamp (number of whole seconds elapsed since Jan 1st 1970 0:00:00 UTC)</returns>
        <Extension()>
        Public Function ToUnixTimestamp(value As Date) As Long
            Return Math.Truncate((value.ToUniversalTime - unixEpoch).TotalSeconds)
        End Function
        ''' <summary>Converts <see cref="DateTimeOffset"/> value to Unix timestamp (number of seconds elapsed from Jan 1 1970 0:00:00 UTC)</summary>
        ''' <param name="value">A <see cref="DateTimeOffset"/> value. It's converted to UTC time using <see cref="DateTimeOffset.ToUniversalTime"/>.</param>
        ''' <returns>A long number representing Unix timestamp (number of whole seconds elapsed since Jan 1 1970 0:00:00 UTC)</returns>
        <Extension()>
        Public Function ToUnixTimestamp(value As DateTimeOffset) As Long
            Return Math.Truncate((value.ToUniversalTime - unixEpoch).TotalSeconds)
        End Function

        ''' <summary>Converts <see cref="DateTime"/> value to short Unix timestamp (number of seconds elapsed from Jan 1 1970 0:00:00 UTC).</summary>
        ''' <param name="value">A <see cref="DateTime"/> value. It's converted to UTC time using <see cref="DateTime.ToUniversalTime"/>.</param>
        ''' <returns>A 32-bit integer number representing Unix timestamp (number of whole seconds elapsed since Jan 1 1970 0:00:00 UTC)</returns>
        ''' <remarks>
        ''' For dates less than Dec 13 1901 20:45:52 UTC or greater than Jan 19 2038 3:14:07 the resulting value overflows (underflows) <see cref="Int32.MaxValue"/> (<see cref="Int32.MinValue"/>).
        ''' <see cref="OverflowException"/> is not throws, instead the value really overflows/underflows.
        ''' </remarks>
        <Extension(), EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Function ToUnixTimestampShort(value As Date) As Integer
            Dim ret = value.ToUnixTimestamp
            Return ret Mod If(ret > 0L, CLng(Integer.MaxValue) + 1, -CLng(Integer.MinValue) + 1)
        End Function
        ''' <summary>Converts <see cref="DateTimeOffset"/> value to short Unix timestamp (number of seconds elapsed from Jan 1 1970 0:00:00 UTC).</summary>
        ''' <param name="value">A <see cref="DateTimeOffset"/> value. It's converted to UTC time using <see cref="DateTimeOffset.ToUniversalTime"/>.</param>
        ''' <returns>A 32-bit integer number representing Unix timestamp (number of whole seconds elapsed since Jan 1 1970 0:00:00 UTC)</returns>
        ''' <remarks>
        ''' For dates less than Dec 13 1901 20:45:52 UTC or greater than Jan 19 2038 3:14:07 the resulting value overflows (underflows) <see cref="Int32.MaxValue"/> (<see cref="Int32.MinValue"/>).
        ''' <see cref="OverflowException"/> is not throws, instead the value really overflows/underflows.
        ''' </remarks>
        <Extension(), EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Function ToUnixTimestampShort(value As DateTimeOffset) As Integer
            Dim ret = value.ToUnixTimestamp
            Return ret Mod If(ret > 0L, CLng(Integer.MaxValue) + 1, -CLng(Integer.MinValue) + 1)
        End Function

        ''' <summary>Converts <see cref="DateTime"/> value to Unix timestamp (number of seconds elapsed from Jan 1 1970 0:00:00 UTC) including second fractions.</summary>
        ''' <param name="value">A <see cref="DateTime"/> value. It's converted to UTC time using <see cref="DateTime.ToUniversalTime"/>.</param>
        ''' <returns>A <see cref="Double"/> number representing Unix timestamp (number of seconds elapsed since Jan 1 1970 0:00:00 UTC)</returns>
        <Extension()>
        Public Function ToUnixTimestampPrecise(value As Date) As Double
            Return (value.ToUniversalTime - unixEpoch).TotalSeconds
        End Function
        ''' <summary>Converts <see cref="DateTimeOffset"/> value to Unix timestamp (number of seconds elapsed from Jan 1 1970 0:00:00.0 UTC) including second fractions.</summary>
        ''' <param name="value">A <see cref="DateTimeOffset"/> value. It's converted to UTC time using <see cref="DateTimeOffset.ToUniversalTime"/>.</param>
        ''' <returns>A <see cref="Double"/> number representing Unix timestamp (number of seconds elapsed since Jan 1 1970 0:00:00.0 UTC)</returns>
        <Extension()>
        Public Function ToUnixTimestampPrecise(value As DateTimeOffset) As Double
            Return (value.ToUniversalTime - unixEpoch).TotalSeconds
        End Function

        ''' <summary>Converts an integer number representing Unix timestamp to <see cref="DateTime"/> value</summary>
        ''' <param name="timestamp">A Unix timestamp (number of seconds elapsed since Jan 1 1970 0:00:00 UTC)</param>
        ''' <returns>A date and time (in UTC coordinates) representing time <paramref name="timestamp"/> seconds after <see cref="unixEpoch"/>.</returns>
        Public Function FromUnixTimestamp(timestamp As Integer) As DateTime
            Return unixEpoch + TimeSpan.FromSeconds(timestamp)
        End Function
        ''' <summary>Converts a long number representing Unix timestamp to <see cref="DateTime"/> value</summary>
        ''' <param name="timestamp">A Unix timestamp (number of seconds elapsed since Jan 1 1970 0:00:00 UTC)</param>
        ''' <returns>A date and time (in UTC coordinates) representing time <paramref name="timestamp"/> seconds after <see cref="unixEpoch"/>.</returns>
        Public Function FromUnixTimestamp(timestamp As Long) As DateTime
            Return unixEpoch + TimeSpan.FromSeconds(timestamp)
        End Function
        ''' <summary>Converts a double number representing Unix timestamp inclusing second fractions to <see cref="DateTime"/> value</summary>
        ''' <param name="timestamp">A Unix timestamp (number of seconds elapsed since Jan 1 1970 0:00:00.0 UTC)</param>
        ''' <returns>A date and time (in UTC coordinates) representing time <paramref name="timestamp"/> seconds after <see cref="unixEpoch"/>.</returns>
        Public Function FromUnixTimestamp(timestamp As Double) As DateTime
            Return unixEpoch + TimeSpan.FromSeconds(timestamp)
        End Function
#End Region
    End Module
End Namespace
#End If
