Imports System.Runtime.CompilerServices, System.Globalization.CultureInfo
Imports System.Globalization
Imports Tools.GlobalizationT

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

        ''' <summary>Gets instance of <see cref="NumberFormatInfo"/> form <see cref="IFormatProvider"/></summary>
        ''' <param name="provider">An <see cref="IFormatProvider"/> (e.g. <see cref="CultureInfo"/>)to get <see cref="NumberFormatInfo"/> from.</param>
        ''' <param name="default">AN instance to be returned when <paramref name="provider"/> does not provide <see cref="NumberFormatInfo"/>.</param>
        ''' <returns>Three ways of getting<see cref="NumberFormatInfo"/> are tried:
        ''' <list type="bullet">
        ''' <item><paramref name="provider"/>.<see cref="IFormatProvider.GetFormat">GetFormat</see></item>
        ''' <item>If <paramref name="provider"/> is <see cref="CultureInfo"/>: <see cref="CultureInfo.NumberFormat"/></item>
        ''' <item><see cref="NumberFormatInfo.GetInstance"/></item>
        ''' </list>
        ''' Whichever first succeds it's result is used. If none succeeds <paramref name="default"/> is returned.
        ''' </returns>
        ''' <exception cref="ArgumentNullException"><paramref name="provider"/> is null</exception>
        ''' <version version="1.5.4">This class is new in version 1.5.4</version>
        <Extension()>
        Public Function GetNumberFormatInfo(provider As IFormatProvider, Optional [default] As NumberFormatInfo = Nothing) As NumberFormatInfo
            If provider Is Nothing Then Throw New ArgumentNullException("provider")
            Dim ret = TryCast(provider.GetFormat(GetType(NumberFormatInfo)), NumberFormatInfo)
            If ret Is Nothing AndAlso TypeOf provider Is CultureInfo Then ret = DirectCast(provider, CultureInfo).NumberFormat
            If ret Is Nothing Then ret = NumberFormatInfo.GetInstance(provider)
            Return If(ret, [default])
        End Function
        ''' <summary>Gets instance of <see cref="DateTimeFormatInfo"/> form <see cref="IFormatProvider"/></summary>
        ''' <param name="provider">An <see cref="IFormatProvider"/> (e.g. <see cref="CultureInfo"/>)to get <see cref="DateTimeFormatInfo"/> from.</param>
        ''' <param name="default">AN instance to be returned when <paramref name="provider"/> does not provide <see cref="DateTimeFormatInfo"/>.</param>
        ''' <returns>Three ways of getting<see cref="DateTimeFormatInfo"/> are tried:
        ''' <list type="bullet">
        ''' <item><paramref name="provider"/>.<see cref="IFormatProvider.GetFormat">GetFormat</see></item>
        ''' <item>If <paramref name="provider"/> is <see cref="CultureInfo"/>: <see cref="CultureInfo.DateTimeFormat"/></item>
        ''' <item><see cref="DateTimeFormatInfo.GetInstance"/></item>
        ''' </list>
        ''' Whichever first succeds it's result is used. If none succeeds <paramref name="default"/> is returned.
        ''' </returns>
        ''' <exception cref="ArgumentNullException"><paramref name="provider"/> is null</exception>
        ''' <version version="1.5.4">This class is new in version 1.5.4</version>
        <Extension()>
        Public Function GetDateTimeFormatInfo(provider As IFormatProvider, Optional [default] As DateTimeFormatInfo = Nothing) As DateTimeFormatInfo
            If provider Is Nothing Then Throw New ArgumentNullException("provider")
            Dim ret = TryCast(provider.GetFormat(GetType(DateTimeFormatInfo)), DateTimeFormatInfo)
            If ret Is Nothing AndAlso TypeOf provider Is CultureInfo Then ret = DirectCast(provider, CultureInfo).DateTimeFormat
            If ret Is Nothing Then ret = DateTimeFormatInfo.GetInstance(provider)
            Return If(ret, [default])
        End Function

        ''' <summary>Gets instance of <see cref="TextInfo"/> form <see cref="IFormatProvider"/></summary>
        ''' <param name="provider">An <see cref="IFormatProvider"/> (e.g. <see cref="CultureInfo"/>)to get <see cref="TextInfo"/> from.</param>
        ''' <param name="default">AN instance to be returned when <paramref name="provider"/> does not provide <see cref="TextInfo"/>.</param>
        ''' <returns>Two ways of getting<see cref="TextInfo"/> are tried:
        ''' <list type="bullet">
        ''' <item><paramref name="provider"/>.<see cref="IFormatProvider.GetFormat">GetFormat</see></item>
        ''' <item>If <paramref name="provider"/> is <see cref="CultureInfo"/>: <see cref="CultureInfo.TextInfo"/></item>
        ''' </list>
        ''' Whichever first succeds it's result is used. If none succeeds <paramref name="default"/> is returned.
        ''' </returns>
        ''' <exception cref="ArgumentNullException"><paramref name="provider"/> is null</exception>
        ''' <version version="1.5.4">This class is new in version 1.5.4</version>
        <Extension()>
        Public Function GetTextInfo(provider As IFormatProvider, Optional [default] As TextInfo = Nothing) As TextInfo
            If provider Is Nothing Then Throw New ArgumentNullException("provider")
            Dim ret = TryCast(provider.GetFormat(GetType(TextInfo)), TextInfo)
            If ret Is Nothing AndAlso TypeOf provider Is CultureInfo Then ret = DirectCast(provider, CultureInfo).TextInfo
            Return If(ret, [default])
        End Function

        ''' <summary>Gets instance of <see cref="CompareInfo"/> form <see cref="IFormatProvider"/></summary>
        ''' <param name="provider">An <see cref="IFormatProvider"/> (e.g. <see cref="CultureInfo"/>)to get <see cref="CompareInfo"/> from.</param>
        ''' <param name="default">AN instance to be returned when <paramref name="provider"/> does not provide <see cref="CompareInfo"/>.</param>
        ''' <returns>Two ways of getting<see cref="CompareInfo"/> are tried:
        ''' <list type="bullet">
        ''' <item><paramref name="provider"/>.<see cref="IFormatProvider.GetFormat">GetFormat</see></item>
        ''' <item>If <paramref name="provider"/> is <see cref="CultureInfo"/>: <see cref="CultureInfo.CompareInfo"/></item>
        ''' </list>
        ''' Whichever first succeds it's result is used. If none succeeds <paramref name="default"/> is returned.
        ''' </returns>
        ''' <exception cref="ArgumentNullException"><paramref name="provider"/> is null</exception>
        ''' <version version="1.5.4">This class is new in version 1.5.4</version>
        <Extension()>
        Public Function GetCompareInfo(provider As IFormatProvider, Optional [default] As CompareInfo = Nothing) As CompareInfo
            If provider Is Nothing Then Throw New ArgumentNullException("provider")
            Dim ret = TryCast(provider.GetFormat(GetType(CompareInfo)), CompareInfo)
            If ret Is Nothing AndAlso TypeOf provider Is CultureInfo Then ret = DirectCast(provider, CultureInfo).CompareInfo
            Return If(ret, [default])
        End Function

        ''' <summary>Gets instance of <see cref="AngleFormatInfo"/> form <see cref="IFormatProvider"/></summary>
        ''' <param name="provider">An <see cref="IFormatProvider"/> (e.g. <see cref="CultureInfo"/>)to get <see cref="AngleFormatInfo"/> from.</param>
        ''' <param name="default">AN instance to be returned when <paramref name="provider"/> does not provide <see cref="AngleFormatInfo"/>.</param>
        ''' <returns>Two ways of getting<see cref="AngleFormatInfo"/> are tried:
        ''' <list type="bullet">
        ''' <item><paramref name="provider"/>.<see cref="IFormatProvider.GetFormat">GetFormat</see></item>
        ''' <item>If <paramref name="provider"/> is <see cref="CultureInfo"/>: <see cref="AngleFormatInfo.[Get]"/>(<paramref name="provider"/>)</item>
        ''' </list>
        ''' Whichever first succeds it's result is used. If none succeeds <paramref name="default"/> is returned.
        ''' </returns>
        ''' <exception cref="ArgumentNullException"><paramref name="provider"/> is null</exception>
        ''' <version version="1.5.4">This class is new in version 1.5.4</version>
        <Extension()>
        Public Function GetAngleFormatInfo(provider As IFormatProvider, Optional [default] As AngleFormatInfo = Nothing) As AngleFormatInfo
            If provider Is Nothing Then Throw New ArgumentNullException("provider")
            Dim ret = TryCast(provider.GetFormat(GetType(AngleFormatInfo)), AngleFormatInfo)
            If ret Is Nothing AndAlso TypeOf provider Is CultureInfo Then ret = AngleFormatInfo.Get(DirectCast(provider, CultureInfo))
            Return If(ret, [default])
        End Function
    End Module
End Namespace
#End If