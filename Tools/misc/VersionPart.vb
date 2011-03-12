Imports System.Globalization.CultureInfo
Imports System.Text.RegularExpressions, Tools.ExtensionsT
Imports System.Runtime.InteropServices

#If Config <= Nightly Then

''' <summary>Represents a part of version information</summary>
''' <version version="1.5.3">This class is new in version 1.5.3</version>
''' <remarks>
''' This class is intended for storing and comparing version information containing only some parts from left.
''' <see cref="Version"/> class can be often used for this purpose, unless you wan to ommit <see cref="Version.Minor"/> part.
''' <para><see cref="IFormattable"/> info:</para>
''' <para>This implementation of <see cref="IFormattable"/> supports following formatting strings:</para>
''' <list type="table"><listheader><term>Formatting string</term><description>Meaning</description></listheader>
''' <item><term>Null, an empty string, "G" or "g"</term><description>Default formatting, same as "4"</description></item>
''' <item><term>An integral number</term><description>Maximum number of components to render. Components are rendered in following order: <see cref="VersionPart.Major"/>, <see cref="VersionPart.Minor"/>, <see cref="VersionPart.Build"/>, <see cref="VersionPart.Revision"/>. Value zero or less producess an empty string. Value greater than 4 is treated as 4.</description></item>
''' <item><term>Anything else</term><description><see cref="FormatException"/> is thrown.</description></item>
''' </list></remarks>
Public NotInheritable Class VersionPart
    Implements IFormattable, IComparable(Of Version), IComparable(Of VersionPart), IEquatable(Of Version), IEquatable(Of VersionPart)

#Region "Properties"
    Private ReadOnly _major%
    Private ReadOnly _minor%?
    Private ReadOnly _build%?
    Private ReadOnly _revision%?
    ''' <summary>Gets the value of the major component of the version number.</summary>
    Public ReadOnly Property Major%
        Get
            Return _major
        End Get
    End Property
    ''' <summary>Gets the value of the minor component of the version number.</summary>
    Public ReadOnly Property Minor As Integer?
        Get
            Return _minor
        End Get
    End Property
    ''' <summary>Gets the value of the build component of the version number.</summary>
    Public ReadOnly Property Build As Integer?
        Get
            Return _build
        End Get
    End Property
    ''' <summary>Gets the value of the revision component of the version numer.</summary>
    Public ReadOnly Property Revision As Integer?
        Get
            Return _revision
        End Get
    End Property
#End Region

#Region "CTors"
    ''' <summary>Creates a new instance of the <see cref="VersionPart"/> class from a <see cref="Version"/> object</summary>
    ''' <param name="version">A <see cref="Version"/></param>
    ''' <exception cref="ArgumentNullException"><paramref name="version"/> is null</exception>
    Public Sub New(version As Version)
        Me.New(version.Major, version.Minor, If(version.ThrowIfNull("version").Build < 0, New Integer?, version.Build), If(version.Revision < 0, New Integer?, version.Revision))
    End Sub
    ''' <summary>Creates a new instance of the <see cref="VersionPart"/> class from version numbers</summary>
    ''' <param name="major">The major version number.</param>
    ''' <param name="minor">The minor version number.</param>
    ''' <param name="build">The build number.</param>
    ''' <param name="revision">The revision number.</param>
    ''' <exception cref="ArgumentOutOfRangeException">Any parameter is less than zero</exception>
    Public Sub New(major%, Optional minor%? = Nothing, Optional build%? = Nothing, Optional revision%? = Nothing)
        If major < 0 Then Throw New ArgumentOutOfRangeException("major")
        If minor.HasValue AndAlso minor.Value < 0 Then Throw New ArgumentOutOfRangeException("minor")
        If build.HasValue AndAlso build.Value < 0 Then Throw New ArgumentOutOfRangeException("build")
        If revision.HasValue AndAlso revision.Value < 0 Then Throw New ArgumentOutOfRangeException("revision")
        _major = major
        _minor = minor
        _build = build
        _revision = revision
    End Sub

    ''' <summary>Creates a new instance of the <see cref="VersionPart"/> class from its string representation</summary>
    ''' <param name="version">A string representation of version information. It is in format major[.minor[.build[.revision]]]</param>
    ''' <exception cref="FormatException"><paramref name="version"/> is in incorrect format</exception>
    ''' <exception cref="OverflowException">Part of version information is out of range of the <see cref="Integer"/> data type</exception>
    ''' <exception cref="ArgumentNullException"><paramref name="version"/> is null</exception>
    Public Sub New(version As String)
        Dim match = versionRegExp.Match(version)
        If match.Success Then
            _major = Integer.Parse(match.Groups!major.Value, InvariantCulture)
            If match.Groups!minor.Captures.Count > 0 Then
                _minor = Integer.Parse(match.Groups!minor.Value, InvariantCulture)
                If match.Groups!build.Captures.Count > 0 Then
                    _build = Integer.Parse(match.Groups!build.Value, InvariantCulture)
                    If match.Groups!revision.Captures.Count > 0 Then
                        _revision = Integer.Parse(match.Groups!revision.Value, InvariantCulture)
                    End If
                End If
            End If
        Else
            Throw New FormatException(ResourcesT.Exceptions.InvalidVersionFormat.f(version))
        End If
    End Sub
#End Region

#Region "Parsing"
    ''' <summary>Parses a astring to <see cref="VersionPart"/> value</summary>
    ''' <param name="version">A string representation of version information. It is in format major[.minor[.build[.revision]]]</param>
    ''' <returns>A new instance of <see cref="VersionPart"/> initialized from <paramref name="version"/></returns>
    ''' <exception cref="FormatException"><paramref name="version"/> is in incorrect format</exception>
    ''' <exception cref="OverflowException">Part of version information is out of range of the <see cref="Integer"/> data type</exception>
    ''' <exception cref="ArgumentNullException"><paramref name="version"/> is null</exception>
    Public Shared Function Parse(version As String) As VersionPart
        Return New VersionPart(version)
    End Function
    ''' <summary>Attempts to parse version information from string to <see cref="VersionPart"/> object</summary>
    ''' <param name="str">A string representation of version information (it's expected to be in format major[.minor[.build[.revision]]])</param>
    ''' <param name="value">When the function returns true this parameter contains new instance of <see cref="VersionPart"/> popupated from <paramref name="str"/>.</param>
    ''' <returns>True if <paramref name="str"/> was successfully parsd to <see cref="VersionPart"/> and assigned to <paramref name="value"/>; false otherwise</returns>
    Public Shared Function TryParse(str As String, <Out()> ByRef value As VersionPart) As Boolean
        If str Is Nothing Then Return False
        If versionRegExp.IsMatch(str) Then
            Try
                value = New VersionPart(str)
                Return True
            Catch
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    ''' <summary>A regular expression used for parsing version values</summary>
    ''' <remarks>It parses out 1 to 4 named groups - major, minor, build and revision</remarks>
    Private Shared ReadOnly versionRegExp As New Regex("^(?<major>\d+)(\.(?<minor>\d+)(\.(?<build>\d+)(\.(?<revision>\d+))?)?)?$",
                                                       RegexOptions.Compiled Or RegexOptions.CultureInvariant Or RegexOptions.ExplicitCapture Or RegexOptions.CultureInvariant)
#End Region

#Region "ToString"
    ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:VersionPart" /> with given precision using given formatting provider.</summary>
    ''' <param name="provider">The provider to use to format the value.-or- A null reference (Nothing in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system. </param>
    ''' <param name="parts">Maximum number of parts of version information to be rendered. Zero or less causes en empty string to be returned. 4 or more is considered 4.</param>
    ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="VersionPart" />.</returns>
    Public Overloads Function ToString(parts As Integer, provider As IFormatProvider) As String
        Dim ret As String = ""
        If provider Is Nothing Then provider = CurrentCulture
        If parts >= 1 Then
            ret &= Major.ToString(provider)
            If parts >= 2 AndAlso Minor.HasValue Then
                ret &= "." & Minor.Value.ToString(provider)
                If parts >= 3 AndAlso Build.HasValue Then
                    ret &= "." & Build.Value.ToString(provider)
                    If parts >= 4 AndAlso Revision.HasValue Then
                        ret &= "." & Revision.Value.ToString(provider)
                    End If
                End If
            End If
        End If
        Return ret
    End Function
    ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:VersionPart" /> with given precision.</summary>
    ''' <param name="parts">Maximum number of parts of version information to be rendered. Zero or less causes en empty string to be returned. 4 or more is considered 4.</param>
    ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="VersionPart" />.</returns>
    Public Overloads Function ToString(parts As Integer) As String
        Return ToString(parts, CurrentCulture)
    End Function
    ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="VersionPart" />.</summary>
    ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="VersionPart" />.</returns>
    ''' <filterpriority>2</filterpriority>
    Public Overloads Overrides Function ToString() As String
        Return ToString(4)
    End Function

    ''' <summary>Formats the value of the current instance using the specified format.</summary>
    ''' <returns>The value of the current instance in the specified format.</returns>
    ''' <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the default format defined for the type of the <see cref="T:System.IFormattable" /> implementation. </param>
    ''' <param name="formatProvider">The provider to use to format the value.-or- A null reference (Nothing in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system. </param>
    ''' <exception cref="FormatException"><paramref name="format"/> cannot be persed as <see cref="Integer"/> in <see cref="InvariantCulture"/></exception>
    ''' <filterpriority>2</filterpriority>
    Public Overloads Function ToString(format As String, formatProvider As IFormatProvider) As String Implements System.IFormattable.ToString
        Select Case format
            Case Nothing, "", "G", "g"
                Return ToString(4, formatProvider)
            Case Else
                Dim parts%
                Try
                    parts = Integer.Parse(format, InvariantCulture)
                Catch ex As Exception
                    Throw New FormatException(ex.Message, ex)
                End Try
                Return ToString(parts, formatProvider)
        End Select
    End Function
    ''' <summary>Formats the value of the current instance using the specified format and <see cref="CurrentCulture"/>.</summary>
    ''' <returns>The value of the current instance in the specified format.</returns>
    ''' <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the default format defined for the type of the <see cref="T:System.IFormattable" /> implementation. </param>
    ''' <exception cref="FormatException"><paramref name="format"/> cannot be persed as <see cref="Integer"/> in <see cref="InvariantCulture"/></exception>
    Public Overloads Function ToString(format As String) As String
        Return ToString(format, CurrentCulture)
    End Function
#End Region

#Region "Conversions"
    ''' <summary>Converts this instance of <see cref="VersionPart"/> to <see cref="Version"/></summary>
    ''' <returns>A new instance of <see cref="Version"/> initialized from this instance</returns>
    ''' <exception cref="InvalidOperationException"><see cref="Minor"/> is null.</exception>
    Public Function ToVersion() As Version
        If Not Minor.HasValue Then Throw New InvalidOperationException(ResourcesT.Exceptions.VersionPartToVersionMinorNull)
        If Revision.HasValue Then Return New Version(Major, Minor, Build, Revision)
        If Build.HasValue Then Return New Version(Major, Minor, Build)
        Return New Version(Major, Minor)
    End Function
#End Region
#Region "Operators"
#Region "Cast"
    ''' <summary>Converts <see cref="Version"/> to <see cref="VersionPart"/></summary>
    ''' <param name="a">A <see cref="Version"/></param>
    ''' <returns>A <see cref="VersionPart"/> initialized from <paramref name="a"/>; null if <paramref name="a"/> is null</returns>
    Public Shared Widening Operator CType(a As Version) As VersionPart
        If a Is Nothing Then Return Nothing
        Return New VersionPart(a)
    End Operator
    ''' <summary>Converts <see cref="VersionPart"/> to <see cref="Version"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <returns>A <see cref="Version"/> initialized from <paramref name="a"/>; null if <paramref name="a"/> is null</returns>
    ''' <exception cref="InvalidCastException"><paramref name="a"/>.<see cref="Minor">Minor</see> is null</exception>
    ''' <seelaso cref="ToVersion"/>
    Public Shared Narrowing Operator CType(a As VersionPart) As Version
        If a Is Nothing Then Return Nothing
        Try
            Return a.ToVersion
        Catch ex As InvalidOperationException
            Throw New InvalidCastException(ex.Message, ex)
        End Try
    End Operator
#End Region
#Region "Equality"
    ''' <summary>Compares two <see cref="VersionPart"/> instances for equality</summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="VersionPart"/></param>
    ''' <returns>True if <paramref name="a"/> and <paramref name="b"/> are equal or are both null; false otherwise</returns>
    Public Shared Operator =(a As VersionPart, b As VersionPart) As Boolean
        If a Is Nothing AndAlso b Is Nothing Then Return True
        If a Is Nothing OrElse b Is Nothing Then Return False
        Return a.Equals(b)
    End Operator
    ''' <summary>Compares <see cref="VersionPart"/> and <see cref="Version"/> instances for equality</summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="VersionPart"/></param>
    ''' <returns>True if <paramref name="a"/> and <paramref name="b"/> are equal or are both null; false otherwise</returns>
    Public Shared Operator =(a As VersionPart, b As Version) As Boolean
        If a Is Nothing AndAlso b Is Nothing Then Return True
        If a Is Nothing OrElse b Is Nothing Then Return False
        Return a.Equals(b)
    End Operator
    ''' <summary>Compares <see cref="Version"/> and <see cref="VersionPart"/> instances for equality</summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="VersionPart"/></param>
    ''' <returns>True if <paramref name="a"/> and <paramref name="b"/> are equal or are both null; false otherwise</returns>
    Public Shared Operator =(a As Version, b As VersionPart) As Boolean
        If a Is Nothing AndAlso b Is Nothing Then Return True
        If a Is Nothing OrElse b Is Nothing Then Return False
        Return b.Equals(a)
    End Operator
    ''' <summary>Compares two <see cref="VersionPart"/> instances for inequality</summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="VersionPart"/></param>
    ''' <returns>False if <paramref name="a"/> and <paramref name="b"/> are equal or are both null; true otherwise</returns>
    Public Shared Operator <>(a As VersionPart, b As VersionPart) As Boolean
        Return Not (a = b)
    End Operator
    ''' <summary>Compares <see cref="VersionPart"/> and <see cref="Version"/> instances for inequality</summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="VersionPart"/></param>
    ''' <returns>False if <paramref name="a"/> and <paramref name="b"/> are equal or are both null; true otherwise</returns>
    Public Shared Operator <>(a As VersionPart, b As Version) As Boolean
        Return Not (a = b)
    End Operator
    ''' <summary>Compares <see cref="Version"/> and <see cref="VersionPart"/> instances for inequality</summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="VersionPart"/></param>
    ''' <returns>False if <paramref name="a"/> and <paramref name="b"/> are equal or are both null; true otherwise</returns>
    Public Shared Operator <>(a As Version, b As VersionPart) As Boolean
        Return Not (a = b)
    End Operator
#End Region
#Region "Comparison"
    ''' <summary>Detects if one <see cref="VersionPart"/> is greater than other <see cref="VersionPart"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="VersionPart"/></param>
    ''' <returns>True if <paramref name="a"/> represents version number greater than <paramref name="b"/>; false otherwise</returns>
    Public Shared Operator >(a As VersionPart, b As VersionPart) As Boolean
        If (a Is Nothing) <> (b Is Nothing) OrElse a Is Nothing OrElse b Is Nothing Then Return False
        If a.Major > b.Major Then Return True
        If a.Major = b.Major Then
            If a.Minor Is Nothing OrElse b.Minor Is Nothing Then Return False
            If a.Minor > b.Minor Then Return True
            If a.Minor = b.Minor Then
                If a.Build Is Nothing OrElse b.Build Is Nothing Then Return False
                If a.Build > b.Build Then Return True
                If a.Build = b.Build Then
                    If a.Revision Is Nothing OrElse b.Revision Is Nothing Then Return False
                    Return a.Revision > b.Revision
                End If
            End If
        End If
        Return False
    End Operator
    ''' <summary>Detects if one <see cref="VersionPart"/> is less than other <see cref="VersionPart"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="VersionPart"/></param>
    ''' <returns>True if <paramref name="a"/> represents version number less than <paramref name="b"/>; false otherwise</returns>
    Public Shared Operator <(a As VersionPart, b As VersionPart) As Boolean
        If (a Is Nothing) <> (b Is Nothing) OrElse a Is Nothing OrElse b Is Nothing Then Return False
        If a.Major < b.Major Then Return True
        If a.Major = b.Major Then
            If a.Minor Is Nothing OrElse b.Minor Is Nothing Then Return False
            If a.Minor < b.Minor Then Return True
            If a.Minor = b.Minor Then
                If a.Build Is Nothing OrElse b.Build Is Nothing Then Return False
                If a.Build < b.Build Then Return True
                If a.Build = b.Build Then
                    If a.Revision Is Nothing OrElse b.Revision Is Nothing Then Return False
                    Return a.Revision < b.Revision
                End If
            End If
        End If
        Return False
    End Operator
    ''' <summary>Detects if one <see cref="VersionPart"/> is greater than or equal to other <see cref="VersionPart"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="VersionPart"/></param>
    ''' <returns>True if <paramref name="a"/> represents version number greater than or equal to <paramref name="b"/>; false otherwise</returns>
    ''' <remarks>Unset values are ignored in comparison. For example 1.5 >= 1.5.3 and also 1.5.3 >= 1.5</remarks>
    Public Shared Operator >=(a As VersionPart, b As VersionPart) As Boolean
        If a Is Nothing AndAlso b Is Nothing Then Return True
        If a Is Nothing OrElse b Is Nothing Then Return False
        If a.Major > b.Major Then Return True
        If a.Major = b.Major Then
            If a.Minor Is Nothing OrElse b.Minor Is Nothing Then Return True
            If a.Minor > b.Minor Then Return True
            If a.Minor = b.Minor Then
                If a.Build Is Nothing OrElse b.Build Is Nothing Then Return True
                If a.Build > b.Build Then Return True
                If a.Build = b.Build Then
                    Return a.Revision Is Nothing OrElse b.Revision Is Nothing OrElse a.Revision >= b.Revision
                End If
            End If
        End If
        Return False
    End Operator
    ''' <summary>Detects if one <see cref="VersionPart"/> is less than or equal to other <see cref="VersionPart"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="VersionPart"/></param>
    ''' <returns>True if <paramref name="a"/> represents version number less than or equal to <paramref name="b"/>; false otherwise</returns>
    ''' <remarks>Unset values are ignored in comparison. For example 1.5 &lt;= 1.5.3 and also 1.5.3 &lt;= 1.5</remarks>
    Public Shared Operator <=(a As VersionPart, b As VersionPart) As Boolean
        If a Is Nothing AndAlso b Is Nothing Then Return True
        If a Is Nothing OrElse b Is Nothing Then Return False
        If a.Major < b.Major Then Return True
        If a.Major = b.Major Then
            If a.Minor Is Nothing OrElse b.Minor Is Nothing Then Return True
            If a.Minor < b.Minor Then Return True
            If a.Minor = b.Minor Then
                If a.Build Is Nothing OrElse b.Build Is Nothing Then Return True
                If a.Build < b.Build Then Return True
                If a.Build = b.Build Then
                    Return a.Revision Is Nothing OrElse b.Revision Is Nothing OrElse a.Revision <= b.Revision
                End If
            End If
        End If
        Return False
    End Operator

    ''' <summary>Detects if a <see cref="VersionPart"/> is greater than a <see cref="Version"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="Version"/></param>
    ''' <returns>True if <paramref name="a"/> represents version number greater than <paramref name="b"/>; false otherwise</returns>
    Public Shared Operator >(a As VersionPart, b As Version) As Boolean
        Return a > CType(b, VersionPart)
    End Operator
    ''' <summary>Detects if a <see cref="VersionPart"/> is less than a <see cref="Version"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="Version"/></param>
    ''' <returns>True if <paramref name="a"/> represents version number less than <paramref name="b"/>; false otherwise</returns>
    Public Shared Operator <(a As VersionPart, b As Version) As Boolean
        Return a < CType(b, VersionPart)
    End Operator
    ''' <summary>Detects if a <see cref="VersionPart"/> is greater than or equal to a <see cref="Version"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="Version"/></param>
    ''' <returns>True if <paramref name="a"/> represents version number greater than or equal to <paramref name="b"/>; false otherwise</returns>
    ''' <remarks>Unset values are ignored in comparison. For example 1.5 >= 1.5.3 and also 1.5.3 >= 1.5</remarks>
    Public Shared Operator >=(a As VersionPart, b As Version) As Boolean
        Return a >= CType(b, VersionPart)
    End Operator
    ''' <summary>Detects if a <see cref="VersionPart"/> is less than or equal to a <see cref="Version"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="Version"/></param>
    ''' <returns>True if <paramref name="a"/> represents version number less than or equal to <paramref name="b"/>; false otherwise</returns>
    ''' <remarks>Unset values are ignored in comparison. For example 1.5 &lt;= 1.5.3 and also 1.5.3 &lt;= 1.5</remarks>
    Public Shared Operator <=(a As VersionPart, b As Version) As Boolean
        Return a <= CType(b, VersionPart)
    End Operator

    ''' <summary>Detects if a <see cref="Version"/> is greater than a <see cref="VersionPart"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="Version"/></param>
    ''' <returns>True if <paramref name="a"/> represents version number greater than <paramref name="b"/>; false otherwise</returns>
    Public Shared Operator >(a As Version, b As VersionPart) As Boolean
        Return CType(a, versionpart) > b
    End Operator
    ''' <summary>Detects if a <see cref="Version"/> is less than a <see cref="VersionPart"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="Version"/></param>
    ''' <returns>True if <paramref name="a"/> represents version number less than <paramref name="b"/>; false otherwise</returns>
    Public Shared Operator <(a As Version, b As VersionPart) As Boolean
        Return CType(a, versionpart) < b
    End Operator
    ''' <summary>Detects if a <see cref="Version"/> is greater than or equal to a <see cref="VersionPart"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="Version"/></param>
    ''' <returns>True if <paramref name="a"/> represents version number greater than or equal to <paramref name="b"/>; false otherwise</returns>
    ''' <remarks>Unset values are ignored in comparison. For example 1.5 >= 1.5.3 and also 1.5.3 >= 1.5</remarks>
    Public Shared Operator >=(a As Version, b As VersionPart) As Boolean
        Return CType(a, versionpart) >= b
    End Operator
    ''' <summary>Detects if a <see cref="Version"/> is less than or equal to a <see cref="VersionPart"/></summary>
    ''' <param name="a">A <see cref="VersionPart"/></param>
    ''' <param name="b">A <see cref="Version"/></param>
    ''' <returns>True if <paramref name="a"/> represents version number less than or equal to <paramref name="b"/>; false otherwise</returns>
    ''' <remarks>Unset values are ignored in comparison. For example 1.5 &lt;= 1.5.3 and also 1.5.3 &lt;= 1.5</remarks>
    Public Shared Operator <=(a As Version, b As VersionPart) As Boolean
        Return CType(a, versionpart) <= b
    End Operator
#End Region
#End Region
#Region "Compare & Equals"
    ''' <summary>Compares the current object with another object of the same type.</summary>
    ''' <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings:
    ''' Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.
    ''' Zero This object is equal to <paramref name="other" />.
    ''' Greater than zero This object is greater than <paramref name="other" />.</returns>
    ''' <param name="other">An object to compare with this object.</param>
    ''' <remarks>Zero is also returned for uncomparable values like 1.5.3 and 1.5</remarks>
    Public Function CompareTo(other As System.Version) As Integer Implements System.IComparable(Of System.Version).CompareTo
        If Me <= other AndAlso Me >= other Then Return 0
        If Me < other Then Return -1
        Return 1
    End Function

    ''' <summary>Compares the current object with another object of the same type.</summary>
    ''' <returns>A value that indicates the relative order of the objects being compared. The return value has the following meanings:
    ''' Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.
    ''' Zero This object is equal to <paramref name="other" />.
    ''' Greater than zero This object is greater than <paramref name="other" />.</returns>
    ''' <param name="other">An object to compare with this object.</param>
    ''' <remarks>Zero is also returned for uncomparable values like 1.5.3 and 1.5</remarks>
    Public Function CompareTo(other As VersionPart) As Integer Implements System.IComparable(Of VersionPart).CompareTo
        If Me <= other AndAlso Me >= other Then Return 0
        If Me < other Then Return -1
        Return 1
    End Function

    ''' <summary>Indicates whether the current object is equal to another object of type <see cref="Version"/>.</summary>
    ''' <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
    ''' <param name="other">An object to compare with this object.</param>
    Public Overloads Function Equals(other As System.Version) As Boolean Implements System.IEquatable(Of System.Version).Equals
        If other Is Nothing Then Return False
        Return Me.Major = other.Major AndAlso Me.Minor.HasValue = (other.Minor = -1) AndAlso (Not Me.Minor.HasValue OrElse Me.Minor.Value = other.Minor) AndAlso
                                              Me.Build.HasValue = (other.Build = -1) AndAlso (Not Me.Build.HasValue OrElse Me.Build.Value = other.Build) AndAlso
                                              Me.Revision.HasValue = (other.Revision = -1) AndAlso (Not Me.Revision.HasValue OrElse Me.Revision.Value = other.Revision)
    End Function

    ''' <summary>Indicates whether the current object is equal to another object of the same type.</summary>
    ''' <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
    ''' <param name="other">An object to compare with this object.</param>
    Public Overloads Function Equals(other As VersionPart) As Boolean Implements System.IEquatable(Of VersionPart).Equals
        If other Is Nothing Then Return False
        Return Me.Major = other.Major AndAlso Me.Minor.HasValue = other.Minor.HasValue AndAlso (Not Me.Minor.HasValue OrElse Me.Minor.Value = other.Minor.Value) AndAlso
                                              Me.Build.HasValue = other.Build.HasValue AndAlso (Not Me.Build.HasValue OrElse Me.Build.Value = other.Build.Value) AndAlso
                                              Me.Revision.HasValue = other.Revision.HasValue AndAlso (Not Me.Revision.HasValue OrElse Me.Revision.Value = other.Revision.Value)
    End Function
    ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
    ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
    ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
    ''' <filterpriority>2</filterpriority>
    Public Overloads Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is VersionPart Then Return Equals(DirectCast(obj, VersionPart))
        If TypeOf obj Is Version Then Return Equals(DirectCast(obj, Version))
        Return MyBase.Equals(obj)
    End Function
    ''' <summary>Serves as a hash function for a particular type. </summary>
    ''' <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
    ''' <filterpriority>2</filterpriority>
    Public Overrides Function GetHashCode() As Integer
        Return Major.GetHashCode Or Minor.GetHashCode Or Revision.GetHashCode Or Build.GetHashCode
    End Function
#End Region
End Class

#End If