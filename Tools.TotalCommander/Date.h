#pragma once
#include "Plugin\plug_common.h"
namespace Tools{
    using namespace System;

    /// <summary>Represents date value with only date precission (without time)</summary>
    /// <version version="1.5.3">This structure is new in version 1.5.3</version>
    [Serializable] 
    value class Date
        : IComparable, IFormattable, IComparable<Date>, IEquatable<Date>, IEquatable<DateTime>, IComparable<DateTime>
    {
    private:
        /// <summary>Contains value of the <see cref="Value"/> property</summary>
        DateTime date;
    public:
        /// <summary>CTor from <see cref="DateTime"/></summary>
        /// <param name="value"><see cref="DateTime"/> to initialize new instace with</param>
        Date(DateTime value);
        /// <summary>CTor from year, month and day</summary>
        /// <param name="Year">Year numbe in range 1÷9999</param>
        /// <param name="Month">Month in range 1÷12</param>
        /// <param name="Day">Day in range 1 ÷ number of days in month <paramref name="Month"/></param>
        /// <exception cref="ArgumentOutOfRangeException"><see cref="Year"/> is less than 1 or greater than 9999.-or- <see cref="Month"/> is less than 1 or greater than 12.-or- <see cref="Day"/> is less than 1 or greater than the number of days in month. </exception>
        /// <exception cref="ArgumentException">The specified parameters evaluate to less than <see cref="MinValue"/> or more than <see cref="MaxValue"/>.</exception>
        Date(int Year, int Month, int Day);
        /// <summary>Represents the smallest possible value of <see cref="Date"/>. This field is read-only.</summary>
        static initonly Date MinValue = Date(DateTime::MinValue);
        /// <summary>Represents the greatest possible value of <see cref="Date"/>. This field is read-only.</summary>
        static initonly Date MaxValue = Date(DateTime::MaxValue);
        /// <summary>Gets value of this instance as <see cref="DateTime"/></summary>
        /// <returns><see cref="DateTime"/> with sub-day part set to zero</returns>
        property DateTime Value{DateTime get();}
        /// <summary>Gets the year component of the date represented by this instance.</summary>
        /// <returns>The year, between 1 and 9999.</returns>
        property int Year{int get();}
        /// <summary>Gets the month component of the date represented by this instance.</summary>
        /// <returns>The month component, expressed as a value between 1 and 12.</returns>
        property int Month{int get();}
        /// <summary>Gets the day of the month represented by this instance.</summary>
        /// <returns>The day component, expressed as a value between 1 and 31.</returns>
        property int Day{int get();}
        /// <summary>Gets the day of the week represented by this instance.</summary>
        /// <returns>A <see cref="System::DayOfWeek"/> enumerated constant that indicates the day of the week. This property value ranges from zero, indicating Sunday, to six, indicating Saturday.</returns>
        property System::DayOfWeek DayOfWeek{System::DayOfWeek get();}
        /// <summary>Gets the day of the year represented by this instance.</summary>
        /// <returns>The day of the year, expressed as a value between 1 and 366.</returns>
        property int DayOfYear{int get();}
        /// <summary>Gets the current date.</summary>
        /// <returns>A <see cref="Date"/> set to today's date</returns>
        property Date Today{Date get();}
#pragma region "Operators"
        /// <summary>Adds a specified time interval to a specified date, yielding a new date and time.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="TimeSpan"/></param>
        /// <returns>A <see cref="DateTime"/> that is the sum of the values of <paramref name="a"/> and <paramref name="b"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The resulting <see cref="DateTime"/> is less than <see cref="DateTime::MinValue"/> or greater than <see cref="DateTime::MaxValue"/>. </exception>
        static DateTime operator+(Date a, TimeSpan b);
        /// <summary>Subtracts a specified time interval from a specified date, yielding a new date and time.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="TimeSpan"/></param>
        /// <returns>A <see cref="DateTime"/> whose value is the value of <paramref name="a"/> minus the value of <paramref name="b"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The resulting <see cref="DateTime"/> is less than <see cref="DateTime::MinValue"/> or greater than <see cref="DateTime::MaxValue"/>. </exception>
        static DateTime operator-(Date a, TimeSpan b);
        /// <summary>Subtracts a specified date from another specified date, yielding a time interval.</summary>
        /// <param name="a">A <see cref="Date"/> (the minuend).</param>
        /// <param name="b">A <see cref="Date"/> (the subtrahend).</param> 
        /// <returns>A <see cref="TimeSpan"/> that is the time interval between <paramref name="a"/> and <paramref name="b"/>; that is, <paramref name="a"/> minus <paramref name="b"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The resulting <see cref="TimeSpan"/> is less than <see cref="TimeSpan::MinValue"/> or greater than <see cref="TimeSpan::MaxValue"/>. </exception>
        static TimeSpan operator-(Date a, Date b);
        /// <summary>Subtracts a specified date and time from another specified date, yielding a time interval.</summary>
        /// <param name="a">A <see cref="Date"/> (the minuend).</param>
        /// <param name="b">A <see cref="DateTime"/> (the subtrahend).</param> 
        /// <returns>A <see cref="TimeSpan"/> that is the time interval between <paramref name="a"/> and <paramref name="b"/>; that is, <paramref name="a"/> minus <paramref name="b"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The resulting <see cref="TimeSpan"/> is less than <see cref="TimeSpan::MinValue"/> or greater than <see cref="TimeSpan::MaxValue"/>. </exception>
        static TimeSpan operator-(Date a, DateTime b);
        /// <summary>Subtracts a specified date from another specified date and time, yielding a time interval.</summary>
        /// <param name="a">A <see cref="DateTime"/> (the minuend).</param>
        /// <param name="b">A <see cref="Date"/> (the subtrahend).</param> 
        /// <returns>A <see cref="TimeSpan"/> that is the time interval between <paramref name="a"/> and <paramref name="b"/>; that is, <paramref name="a"/> minus <paramref name="b"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The resulting <see cref="TimeSpan"/> is less than <see cref="TimeSpan::MinValue"/> or greater than <see cref="TimeSpan::MaxValue"/>. </exception>
        static TimeSpan operator-(DateTime a, Date b);

        /// <summary>Determines whether one specified <see cref="Date"/> is greater than another specified <see cref="Date"/>.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="Date"/></param>
        /// <returns>true if <paramref name="a"/> is greater than <paramref name="b"/>; otherwise, false.</returns>
        static bool operator>(Date a, Date b);
        /// <summary>Determines whether one specified <see cref="Date"/> is less than another specified <see cref="Date"/>.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="Date"/></param>
        /// <returns>true if <paramref name="a"/> is less than <paramref name="b"/>; otherwise, false.</returns>
        static bool operator<(Date a, Date b);
        /// <summary>Determines whether one specified <see cref="Date"/> is less than or equal to another specified <see cref="Date"/>.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="Date"/></param>
        /// <returns>true if <paramref name="a"/> is less than equal to <paramref name="b"/>; otherwise, false.</returns>
        static bool operator<=(Date a, Date b);
        /// <summary>Determines whether one specified <see cref="Date"/> is greater than or equal to another specified <see cref="Date"/>.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="Date"/></param>
        /// <returns>true if <paramref name="a"/> is greater than or equal to <paramref name="b"/>; otherwise, false.</returns>
        static bool operator>=(Date a, Date b);
        /// <summary>Determines whether one specified <see cref="Date"/> is equal to another specified <see cref="Date"/>.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="Date"/></param>
        /// <returns>true if <paramref name="a"/> is equal to <paramref name="b"/>; otherwise, false.</returns>
        static bool operator==(Date a, Date b);
        /// <summary>Determines whether one specified <see cref="Date"/> is not equal to another specified <see cref="Date"/>.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="Date"/></param>
        /// <returns>true if <paramref name="a"/> is not equal to <paramref name="b"/>; otherwise, false.</returns>
        static bool operator!=(Date a, Date b);

        /// <summary>Determines whether one specified <see cref="Date"/> is greater than another specified <see cref="DateTime"/>.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="DateTime"/></param>
        /// <returns>true if <paramref name="a"/> is greater than <paramref name="b"/>; otherwise, false.</returns>
        static bool operator>(Date a, DateTime b);
        /// <summary>Determines whether one specified <see cref="Date"/> is less than another specified <see cref="DateTime"/>.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="DateTime"/></param>
        /// <returns>true if <paramref name="a"/> is less than <paramref name="b"/>; otherwise, false.</returns>
        static bool operator<(Date a, DateTime b);
        /// <summary>Determines whether one specified <see cref="Date"/> is less than or equal to another specified <see cref="DateTime"/>.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="DateTime"/></param>
        /// <returns>true if <paramref name="a"/> is less than equal to <paramref name="b"/>; otherwise, false.</returns>
        static bool operator<=(Date a, DateTime b);
        /// <summary>Determines whether one specified <see cref="Date"/> is greater than or equal to another specified <see cref="DateTime"/>.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="DateTime"/></param>
        /// <returns>true if <paramref name="a"/> is greater than or equal to <paramref name="b"/>; otherwise, false.</returns>
        static bool operator>=(Date a, DateTime b);
        /// <summary>Determines whether one specified <see cref="Date"/> is equal to another specified <see cref="DateTime"/>.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="DateTime"/></param>
        /// <returns>true if <paramref name="a"/> is equal to <paramref name="b"/>; otherwise, false.</returns>
        static bool operator==(Date a, DateTime b);
        /// <summary>Determines whether one specified <see cref="Date"/> is not equal to another specified <see cref="DateTime"/>.</summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <param name="b">A <see cref="DateTime"/></param>
        /// <returns>true if <paramref name="a"/> is not equal to <paramref name="b"/>; otherwise, false.</returns>
        static bool operator!=(Date a, DateTime b);

        /// <summary>Determines whether one specified <see cref="DateTime"/> is greater than another specified <see cref="Date"/>.</summary>
        /// <param name="a">A <see cref="DateTime"/></param>
        /// <param name="b">A <see cref="Date"/></param>
        /// <returns>true if <paramref name="a"/> is greater than <paramref name="b"/>; otherwise, false.</returns>
        static bool operator>(DateTime a, Date b);
        /// <summary>Determines whether one specified <see cref="DateTime"/> is less than another specified <see cref="Date"/>.</summary>
        /// <param name="a">A <see cref="DateTime"/></param>
        /// <param name="b">A <see cref="Date"/></param>
        /// <returns>true if <paramref name="a"/> is less than <paramref name="b"/>; otherwise, false.</returns>
        static bool operator<(DateTime a, Date b);
        /// <summary>Determines whether one specified <see cref="DateTime"/> is less than or equal to another specified <see cref="Date"/>.</summary>
        /// <param name="a">A <see cref="DateTime"/></param>
        /// <param name="b">A <see cref="Date"/></param>
        /// <returns>true if <paramref name="a"/> is less than equal to <paramref name="b"/>; otherwise, false.</returns>
        static bool operator<=(DateTime a, Date b);
        /// <summary>Determines whether one specified <see cref="DateTime"/> is greater than or equal to another specified <see cref="Date"/>.</summary>
        /// <param name="a">A <see cref="DateTime"/></param>
        /// <param name="b">A <see cref="Date"/></param>
        /// <returns>true if <paramref name="a"/> is greater than or equal to <paramref name="b"/>; otherwise, false.</returns>
        static bool operator>=(DateTime a, Date b);
        /// <summary>Determines whether one specified <see cref="DateTime"/> is equal to another specified <see cref="Date"/>.</summary>
        /// <param name="a">A <see cref="DateTime"/></param>
        /// <param name="b">A <see cref="Date"/></param>
        /// <returns>true if <paramref name="a"/> is equal to <paramref name="b"/>; otherwise, false.</returns>
        static bool operator==(DateTime a, Date b);
        /// <summary>Determines whether one specified <see cref="DateTime"/> is not equal to another specified <see cref="Date"/>.</summary>
        /// <param name="a">A <see cref="DateTime"/></param>
        /// <param name="b">A <see cref="Date"/></param>
        /// <returns>true if <paramref name="a"/> is not equal to <paramref name="b"/>; otherwise, false.</returns>
        static bool operator!=(DateTime a, Date b);
#pragma endregion

        /// <summary>Converts the value of this instance to its equivalent string representation.</summary>
        /// <returns>A string representation of value of this instance.</returns>
        virtual String^ ToString() override;
        /// <summary>Converts the value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
        /// <returns>A string representation of value of this instance as specified by <paramref name="provider"/>.</returns>
        String^ ToString(IFormatProvider^ provider);
        /// <summary>Converts the value of this instance to its equivalent string representation using the specified format.</summary>
        /// <param name="Format">A format string.</param>
        /// <returns>A string representation of value of this instance as specified by <paramref name="Format"/>.</returns>
        /// <exception cref="FormatException">The length of <paramref name="Format"/> is 1, and it is not one of the format specifier characters defined for <see cref="Globalization::DateTimeFormatInfo"/>.-or- format does not contain a valid custom format pattern. </exception>
        String^ ToString(String^ Format);
        /// <summary>Converts the value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
        /// <param name="Format">A format string.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting information.</param>
        /// <returns>A string representation of value of this instance as specified by format and provider.</returns>
        /// <exception cref="FormatException">The length of <paramref name="Format"/> is 1, and it is not one of the format specifier characters defined for <see cref="Globalization::DateTimeFormatInfo"/>.-or- format does not contain a valid custom format pattern. </exception>
        virtual String^ ToString(String^ Format, IFormatProvider^ provider) sealed;
        /// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
        /// <param name="obj">A boxed <see cref="DateTime"/> or <see cref="Date"/> object to compare, or null.</param>
        /// <returns>A signed number indicating the relative values of this instance and <paramref name="obj"/>. Less than zero This instance is less than <paramref name="obj"/>. Zero This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>, or <paramref name="obj"/> is null. </returns>
        /// <exception cref="ArgumentException"><paramref name="obj"/> is neither <see cref="DateTime"/> nor <see cref="Date"/>.</exception>
        virtual Int32 CompareTo(Object^ obj) sealed;
        /// <summary>Compares this instance to a specified <see cref="Date"/> object and returns an indication of their relative values.</summary>
        /// <param name="obj">A <see cref="Date"/> object to compare.</param>
        /// <returns>A signed number indicating the relative values of this instance and <paramref name="obj"/>. Less than zero This instance is less than <paramref name="obj"/>. Zero This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>, or <paramref name="obj"/> is null. </returns>
        virtual Int32 CompareTo(Date obj) sealed;
        /// <summary>Compares this instance to a specified <see cref="DateTime"/> object and returns an indication of their relative values.</summary>
        /// <param name="obj">A <see cref="DateTime"/> object to compare.</param>
        /// <returns>A signed number indicating the relative values of this instance and <paramref name="obj"/>. Less than zero This instance is less than <paramref name="obj"/>. Zero This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>, or <paramref name="obj"/> is null. </returns>
        virtual Int32 CompareTo(DateTime obj) sealed;
        /// <summary>Returns a value indicating whether this instance is equal to the specified <see cref="Date"/> instance.</summary>
        /// <param name="other">A <see cref="Date"/> instance to compare to this instance.</param>
        /// <returns>true if the <paramref name="other"/> parameter equals the value of this instance; otherwise, false.</returns>
        virtual Boolean Equals(Date other) sealed;
        /// <summary>Returns a value indicating whether this instance is equal to the specified <see cref="DateTime"/> instance.</summary>
        /// <param name="other">A <see cref="DateTime"/> instance to compare to this instance.</param>
        /// <returns>true if the <paramref name="other"/> parameter equals the value of this instance; otherwise, false.</returns>
        virtual Boolean Equals(DateTime other) sealed;
        /// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
        /// <param name="other">An object to compare to this instance.</param>
        /// <returns>true if <paramref name="other"/> is an instance of <see cref="DateTime"/> or <see cref="Date"/> and equals the value of this instance; otherwise, false.</returns>
        virtual bool Equals(Object^ other) override sealed;
        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        virtual int GetHashCode() override sealed;

        /// <summary>Converts <see cref="Date"/> to <see cref="DateTime"/></summary>
        /// <param name="a">A <see cref="Date"/></param>
        /// <returns>A <see cref="DateTime"/></returns>
        static operator DateTime(Date a);
        /// <summary>Converts <see cref="DateTime"/> to <see cref="Date"/></summary>
        /// <param name="a">A <see cref="DateTime"/></param>
        /// <returns>A <see cref="Date"/></returns>
        static operator Date(DateTime a);

    internal:
        /// <summary>Populates <see cref="pdateformat"/> with values of this instance.</summary>
        /// <param name="target">Pointer to populate</param>
        /// <exception cref="ArgumentNullException"><paramref name="target"/> is null pointer</exception>
        void Populate(pdateformat target);
        /// <summary>CTor from <see cref="pdateformat"/></summary>
        /// <param name="a">A <se cref="pdateformat"/></param>
        /// <exception cref="ArgumentNullException"><paramref name="a"/> is null pointer</exception>
        Date(pdateformat a);
    };
}