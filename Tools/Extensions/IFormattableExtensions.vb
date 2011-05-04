Imports System.Runtime.CompilerServices, System.Globalization.CultureInfo

#If Config <= Nightly Then 'Stage:Nightly
Namespace ExtensionsT
    ''' <summary>Contains extension methods for <see cref="IFormattable"/></summary>
    ''' <version version="1.5.4">This module is new in version 1.5.4</version>
    Public Module IFormattableExtensions
#Region "ToString"
        ''' <summary>Returns a <see cref="String"/> that represents the current <see cref="IFormattable"/> in <see cref="InvariantCulture">invariant culture</see>.</summary>
        ''' <param name="obj">An object to get string representation of</param>
        ''' <returns>String representation of <paramref name="obj"/> in <see cref="InvariantCulture">invariant culture</see>; null if <paramref name="obj"/> is null.</returns>
        ''' <remarks>Uses <see cref="IFormattable.ToString"/> with <c>format</c> = null</remarks>
        <Extension()>
        Public Function ToStringInvariant(obj As IFormattable) As String
            If obj Is Nothing Then Return Nothing
            Return obj.ToString(Nothing, InvariantCulture)
        End Function

        ''' <summary>Returns a <see cref="String"/> that represents the current <see cref="IFormattable"/> in <see cref="InvariantCulture">invariant culture</see> using given format.</summary>
        ''' <param name="obj">An object to get string representation of</param>
        ''' <param name="format">The format to use.-or-. NUll to use the default format defined for the type of the <see cref="System.IFormattable"/> implementation.</param>
        ''' <returns>String representation of <paramref name="obj"/> in <see cref="InvariantCulture">invariant culture</see>; null if <paramref name="obj"/> is null.</returns>
        <Extension()>
        Public Function ToStringInvariant(obj As IFormattable, format As String) As String
            If obj Is Nothing Then Return Nothing
            Return obj.ToString(format, InvariantCulture)
        End Function

        ''' <summary>Formats the value of the current instance using the specified format.</summary>
        ''' <param name="format"> The format to use.-or- Null to use the default format defined for the type of the <see cref="System.IFormattable"/> implementation.</param>
        ''' <returns>The value of the current instance in the specified format; null if <paramref name="format"/> is null.</returns>
        <Extension()>
        Public Function ToString(obj As IFormattable, format As String) As String
            If obj Is Nothing Then Return Nothing
            Return obj.ToString(format, Nothing)
        End Function

        ''' <summary>Returns a <see cref="String"/> that represents the current <see cref="IFormattable"/> in given culture.</summary>
        ''' <param name="obj">An object to get string representation of</param>
        ''' <param name="formatProvider">The provider to use to format the value.-or- Null to obtain the numeric format information from the current locale setting of the operating system.</param>
        ''' <returns>String representation of <paramref name="obj"/> formatted using <paramref name="formatProvider"/>; null if <paramref name="obj"/> is null.</returns>
        ''' <remarks>Uses <see cref="IFormattable.ToString"/> with <c>format</c> = null</remarks>
        <Extension()>
        Public Function ToString(obj As IFormattable, formatProvider As IFormatProvider) As String
            If obj Is Nothing Then Return Nothing
            Return obj.ToString(Nothing, formatProvider)
        End Function

#Region "Nullable"
        ''' <summary>Returns a <see cref="String"/> that represents the current <see cref="IFormattable"/> in <see cref="InvariantCulture">invariant culture</see> using given format.</summary>
        ''' <typeparam name="T">Type of nullable type</typeparam>
        ''' <param name="obj">An object to get string representation of</param>
        ''' <param name="format">The format to use.-or-. NUll to use the default format defined for the type of the <see cref="System.IFormattable"/> implementation.</param>
        ''' <returns>String representation of <paramref name="obj"/> in <see cref="InvariantCulture">invariant culture</see>; null if <paramref name="obj"/> is null.</returns>
        <Extension()>
        Public Function ToString(Of T As {IFormattable, Structure})(obj As T?, format$) As String
            If obj Is Nothing Then Return Nothing
            Return ToString(obj.Value, format)
        End Function

        ''' <summary>Returns a <see cref="String"/> that represents the current <see cref="IFormattable"/> in given culture.</summary>
        ''' <typeparam name="T">Type of nullable type</typeparam>
        ''' <param name="obj">An object to get string representation of</param>
        ''' <param name="formatProvider">The provider to use to format the value.-or- Null to obtain the numeric format information from the current locale setting of the operating system.</param>
        ''' <returns>String representation of <paramref name="obj"/> formatted using <paramref name="formatProvider"/>; null if <paramref name="obj"/> is null.</returns>
        ''' <remarks>Uses <see cref="IFormattable.ToString"/> with <c>format</c> = null</remarks>
        <Extension()>
        Public Function ToString(Of T As {IFormattable, Structure})(obj As T?, formatProvider As IFormatProvider) As String
            If obj Is Nothing Then Return Nothing
            Return ToString(obj, formatProvider)
        End Function


        ''' <summary>Formats the value of the current instance using the specified format.</summary>
        ''' <typeparam name="T">Type of nullable type</typeparam>
        ''' <param name="obj">An object to get string representation of</param>
        ''' <param name="format">The format to use.-or- Null to use the default format defined for the type of the <see cref="System.IFormattable" /> implementation.</param>
        ''' <param name="formatProvider">The provider to use to format the value.-or- A null reference (Nothing in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system.</param>
        ''' <returns>The value of the current instance in the specified format; null if <paramref name="obj"/> is null.</returns>
        <Extension()>
        Public Function ToString(Of T As {IFormattable, Structure})(obj As T?, format$, formatProvider As IFormatProvider) As String
            If obj Is Nothing Then Return Nothing
            Return obj.Value.ToString(format, formatProvider)
        End Function

        ''' <summary>Returns a <see cref="String"/> that represents the current <see cref="IFormattable"/> in <see cref="InvariantCulture">invariant culture</see>.</summary>
        ''' <typeparam name="T">Type of nullable type</typeparam>
        ''' <param name="obj">An object to get string representation of</param>
        ''' <returns>String representation of <paramref name="obj"/> in <see cref="InvariantCulture">invariant culture</see>; null if <paramref name="obj"/> is null.</returns>
        ''' <remarks>Uses <see cref="IFormattable.ToString"/> with <c>format</c> = null</remarks>
        <Extension()>
        Public Function ToStringInvariant(Of T As {IFormattable, Structure})(obj As T?) As String
            If obj Is Nothing Then Return Nothing
            Return obj.Value.ToStringInvariant
        End Function

        ''' <summary>Returns a <see cref="String"/> that represents the current <see cref="IFormattable"/> in <see cref="InvariantCulture">invariant culture</see> using given format.</summary>
        ''' <typeparam name="T">Type of nullable type</typeparam>
        ''' <param name="obj">An object to get string representation of</param>
        ''' <param name="format">The format to use.-or-. NUll to use the default format defined for the type of the <see cref="System.IFormattable"/> implementation.</param>
        ''' <returns>String representation of <paramref name="obj"/> in <see cref="InvariantCulture">invariant culture</see>; null if <paramref name="obj"/> is null.</returns>
        <Extension()>
        Public Function ToStringInvariant(Of T As {IFormattable, Structure})(obj As T?, format$) As String
            If obj Is Nothing Then Return Nothing
            Return obj.Value.ToStringInvariant(format)
        End Function
#End Region
#End Region
    End Module
End Namespace
#End If