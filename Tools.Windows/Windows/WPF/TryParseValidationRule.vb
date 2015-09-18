Imports System.Windows.Controls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports Tools.ExtensionsT

Namespace WindowsT.WPF
#If True
    'TODO: Add more types (GUID, Version, IPAddress, ...)
    ''' <summary>A <see cref="System.Windows.Controls.ValidationRule"/>-derived class for verifying if given <see cref="String"/> value represents valid values of certain type.</summary>
    ''' <remarks>With excpetion of few specific types validation is performed by appropriate <c>TryParse</c> method.</remarks>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public Class TryParseValidationRule
        Inherits ValidationRule
        ''' <summary>CTor - creates a new instance of the <see cref="TryParseValidationRule"/> class</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="TryParseValidationRule"/> and sets its validation type</summary>
        ''' <param name="validationType">Type of validation</param>
        Public Sub New(ByVal validationType As TryParseValidationType)
            Me.ValidationType = validationType
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="TryParseValidationRule"/> and sets its validation type and number style</summary>
        ''' <param name="validationType">Type of validation</param>
        ''' <param name="numberStyle">Numeric sytle for numeric types</param>
        Public Sub New(ByVal validationType As TryParseValidationType, ByVal numberStyle As NumberStyles)
            Me.New(validationType)
            Me.NumberStyle = numberStyle
        End Sub
        ''' <summary>Gets or sets single date format used with date types and <c>TryParseExact</c> method</summary>
        ''' <value>By setting this property <see cref="DateFormats"/> property is erased and value being set becomes it's only member. If value being set is null the <see cref="DateFormats"/> collection bevomes empty.</value>
        ''' <returns>First member of <see cref="DateFormats"/> collection.</returns>
        ''' <seelaso cref="DateTime.TryParseExact"/><seelaso cref="DateTimeOffset.TryParseExact"/><seelaso cref="TimeSpan.TryParseExact"/>
        Public Property DateFormat As String
            Get
                If DateFormats.Count > 0 Then Return DateFormats(0) Else Return Nothing
            End Get
            Set(ByVal value As String)
                DateFormats.Clear()
                If value IsNot Nothing Then DateFormats.Add(value)
            End Set
        End Property
        Private ReadOnly _dateFormats As New List(Of String)
        ''' <summary>Gets collection fo date formats used with date-time data types <c>TryParseExact</c></summary>
        ''' <remarks>If this collection is empty <c>TryParse</c> is used isntead of <c>TryParseExact</c></remarks>
        ''' <seelaso cref="DateTime.TryParseExact"/><seelaso cref="DateTimeOffset.TryParseExact"/><seelaso cref="TimeSpan.TryParseExact"/>
        Public ReadOnly Property DateFormats As List(Of String)
            Get
                Return _dateFormats
            End Get
        End Property

        Private _enumType As Type
        ''' <summary>If <see cref="ValidationType"/> is <see cref="TryParseValidationType.[Enum]"/> this enum type is used.</summary>
        ''' <remarks>This property mus be set when <see cref="TryParseValidationType.[Enum]"/> is used.</remarks>
        Public Property EnumType() As Type
            Get
                Return _enumType
            End Get
            Set(ByVal value As Type)
                If value IsNot Nothing AndAlso Not value.IsEnum Then Throw New ArgumentException(ResourcesT.Exceptions.TypeMustBeEnumeration, "value")
                _enumType = value
            End Set
        End Property
        ''' <summary>Gets or sets value indicating if enum parsing is case-sensitive</summary>
        ''' <value>True to make enum parsing case-insensitive, false to make it vcase-sensitive</value>
        <DefaultValue(False)>
        Public Property EnumIgnoreCase As Boolean = False
        ''' <summary>Gets or sets validation type</summary>
        <DefaultValue(TryParseValidationType.Int32)>
        Public Property ValidationType As TryParseValidationType = TryParseValidationType.Int32
        ''' <summary>Gets or sets number style used by numeric validation types</summary>
        <DefaultValue(NumberStyles.Any)>
        Public Property NumberStyle As NumberStyles = NumberStyles.Any
        ''' <summary>gets or sets date-time style used by date-time types</summary>
        <DefaultValue(DateTimeStyles.None)>
        Public Property DateTimeStyle As DateTimeStyles = DateTimeStyles.None

        ''' <summary>Gets or sets culture. When set to non-null value this culture is used rather than culture passed to <see cref="Validate"/> function.</summary>
        <DefaultValue(GetType(CultureInfo), Nothing)>
        Public Property OverrideCulture As CultureInfo

        ''' <summary>Regardless of another settings, always allow empty string as valid input (also allows null)</summary>
        ''' <version version="1.5.4">Fix: Property type changed from <see cref="Object"/> to <see cref="Boolean"/>.</version>
        <DefaultValue(False)>
        Public Property AllowEmptyString As Boolean = False

        ''' <summary>Performs validation checks on a value.</summary>
        ''' <returns>A <see cref="T:System.Windows.Controls.ValidationResult" /> object.</returns>
        ''' <param name="value">The value from the binding target to check.</param>
        ''' <param name="cultureInfo">The culture to use in this rule.</param>
        Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As System.Windows.Controls.ValidationResult
            Dim result As Boolean
            Dim msg As String = Nothing
            If allowemptystring AndAlso value Is Nothing OrElse value.tostring = "" Then Return validationresult.validresult
            Select Case ValidationType
                Case TryParseValidationType.Boolean
                    If value Is Nothing Then GoTo Null
                    Dim v As Boolean
                    result = TypeOf value Is Boolean OrElse Boolean.TryParse(value.ToString, v)
                Case TryParseValidationType.Byte
                    If value Is Nothing Then GoTo null
                    Dim v As Byte
                    result = TypeOf value Is Byte OrElse Byte.TryParse(value.ToString, NumberStyle, cultureInfo, v)
                Case TryParseValidationType.DateTime
                    If value Is Nothing Then GoTo null
                    Dim v As DateTime
                    If DateFormats.Count = 0 Then
                        result = DateTime.TryParse(value.ToString, cultureInfo, DateTimeStyle, v)
                    Else
                        result = DateTime.TryParseExact(value.ToString, DateFormats.ToArray, cultureInfo, DateTimeStyle, v)
                    End If
                Case TryParseValidationType.DBNull
                    result = value Is Nothing OrElse TypeOf value Is DBNull OrElse value.ToString() = ""
                Case TryParseValidationType.Decimal
                    If value Is Nothing Then GoTo null
                    Dim v As Decimal
                    result = TypeOf value Is Decimal OrElse Decimal.TryParse(value.ToString, NumberStyle, cultureInfo, v)
                Case TryParseValidationType.Double
                    If value Is Nothing Then GoTo null
                    Dim v As Double
                    result = TypeOf value Is Double OrElse Double.TryParse(value.ToString, NumberStyle, cultureInfo, v)
                Case TryParseValidationType.Empty
                    result = value Is Nothing OrElse value.ToString = ""
                Case TryParseValidationType.Char
                    If value Is Nothing Then GoTo null
                    Dim v As Char
                    result = TypeOf value Is Char OrElse Char.TryParse(value.ToString, v)
                Case TryParseValidationType.Int16
                    If value Is Nothing Then GoTo null
                    Dim v As Int16
                    result = TypeOf value Is Int16 OrElse Int16.TryParse(value.ToString, NumberStyle, cultureInfo, v)
                Case TryParseValidationType.Int32
                    If value Is Nothing Then GoTo null
                    Dim v As Int32
                    result = TypeOf value Is Int32 OrElse Int32.TryParse(value.ToString, NumberStyle, cultureInfo, v)
                Case TryParseValidationType.Int64
                    If value Is Nothing Then GoTo null
                    Dim v As Int64
                    result = TypeOf value Is Int64 OrElse Int64.TryParse(value.ToString, NumberStyle, cultureInfo, v)
                Case TryParseValidationType.Object
                    result = True
                Case TryParseValidationType.SByte
                    If value Is Nothing Then GoTo null
                    Dim v As SByte
                    result = TypeOf value Is SByte OrElse SByte.TryParse(value.ToString, NumberStyle, cultureInfo, v)
                Case TryParseValidationType.Single
                    If value Is Nothing Then GoTo null
                    Dim v As Single
                    result = TypeOf value Is Single OrElse Single.TryParse(value.ToString, NumberStyle, cultureInfo, v)
                Case TryParseValidationType.String
                    result = value IsNot Nothing
                Case TryParseValidationType.UInt16
                    If value Is Nothing Then GoTo null
                    Dim v As UInt16
                    result = TypeOf value Is UInt16 OrElse UInt16.TryParse(value.ToString, NumberStyle, cultureInfo, v)
                Case TryParseValidationType.UInt32
                    If value Is Nothing Then GoTo null
                    Dim v As UInt32
                    result = TypeOf value Is UInt32 OrElse UInt32.TryParse(value.ToString, NumberStyle, cultureInfo, v)
                Case TryParseValidationType.UInt64
                    If value Is Nothing Then GoTo null
                    Dim v As UInt64
                    result = TypeOf value Is UInt64 OrElse UInt64.TryParse(value.ToString, NumberStyle, cultureInfo, v)
                Case TryParseValidationType.DateTimeOffset
                    If value Is Nothing Then GoTo null
                    Dim v As DateTimeOffset
                    If DateFormats.Count = 0 Then
                        result = DateTimeOffset.TryParse(value.ToString, cultureInfo, DateTimeStyle, v)
                    Else
                        result = DateTimeOffset.TryParseExact(value.ToString, DateFormats.ToArray, cultureInfo, DateTimeStyle, v)
                    End If
                Case TryParseValidationType.TimeSpan
                    If value Is Nothing Then GoTo null
                    Dim v As TimeSpan
                    If DateFormats.Count = 0 Then
                        result = TimeSpan.TryParse(value.ToString, cultureInfo, v)
                    Else
                        result = TimeSpan.TryParseExact(value.ToString, DateFormats.ToArray, cultureInfo, DateTimeStyle, v)
                    End If
                Case TryParseValidationType.Enum
                    If EnumType Is Nothing Then Throw New InvalidOperationException("Property {0} was not initialized".f("EnumType"))
                    If value Is Nothing Then GoTo null
                    Try
                        [Enum].Parse(EnumType, value.ToString, EnumIgnoreCase)
                        result = True
                    Catch ex As Exception
                        result = False
                        msg = ex.Message
                    End Try
                Case Else : Throw New InvalidOperationException(WindowsT.WPF.Resources.ex_PropertyHasUnknownValue.f("ValidationType", ValidationType))
            End Select
            If result Then
                Return ValidationResult.ValidResult
            Else
                Return New ValidationResult(False, If(msg, WindowsT.WPF.Resources.ValueHasIncorrectFormat))
            End If
null:
            Return New ValidationResult(False, WindowsT.WPF.Resources.ValueCannotBeNull)
        End Function

    End Class

    ''' <summary>Validation types supported by <see cref="TryParseValidationRule"/></summary>
    ''' <remarks>Values of this enumeration are based and extends <see cref="TypeCode"/>.</remarks>
    ''' <seelaso cref="TypeCode"/>
    ''' <version version="1.5.3" stage="Nightly">This enumeration is new in version 1.5.3</version>
    <Serializable()>
    Public Enum TryParseValidationType
        ''' <summary><see cref="[Boolean]"/>. Ignores culture.</summary>
        [Boolean] = TypeCode.[Boolean]
        ''' <summary><see cref="[Byte]"/></summary>
        [Byte] = TypeCode.[Byte]
        ''' <summary><see cref="DateTime"/> Uses special options for date types.</summary>
        DateTime = TypeCode.DateTime
        ''' <summary><see cref="DBNull"/>. Only null, empty string and <see cref="DBNull"/> passes validation.</summary>
        DBNull = TypeCode.DBNull
        ''' <summary><see cref="[Decimal]"/></summary>
        [Decimal] = TypeCode.[Decimal]
        ''' <summary><see cref="[Double]"/></summary>
        [Double] = TypeCode.[Double]
        ''' <summary><see cref="Empty"/>. Only null and empty string passes validation.</summary>
        Empty = TypeCode.Empty
        ''' <summary><see cref="[Char]"/>. Ignores culture.</summary>
        [Char] = TypeCode.[Char]
        ''' <summary><see cref="Int16"/></summary>
        Int16 = TypeCode.Int16
        ''' <summary><see cref="Int32"/></summary>
        Int32 = TypeCode.Int32
        ''' <summary><see cref="Int64"/></summary>
        Int64 = TypeCode.Int64
        ''' <summary><see cref="[Object]"/>. All values passes validation.</summary>
        [Object] = TypeCode.[Object]
        ''' <summary><see cref="[SByte]"/></summary>
        <CLSCompliant(False)>
        [SByte] = TypeCode.[SByte]
        ''' <summary><see cref="[Single]"/></summary>
        [Single] = TypeCode.[Single]
        ''' <summary><see cref="[String]"/>All values but null passes validation.</summary>
        [String] = TypeCode.[String]
        ''' <summary><see cref="UInt16"/></summary>
        <CLSCompliant(False)>
        UInt16 = TypeCode.UInt16
        ''' <summary><see cref="UInt32"/></summary>
        <CLSCompliant(False)>
        UInt32 = TypeCode.UInt32
        ''' <summary><see cref="UInt64"/></summary>
        <CLSCompliant(False)>
        UInt64 = TypeCode.UInt64
        ''' <summary><see cref="TimeSpan"/>. Uses special date-time options.</summary>
        TimeSpan = 1024
        ''' <summary><see cref="DateTimeOffset"/>. Uses special date-time options.</summary>
        DateTimeOffset = 1025
        ''' <summary><see cref="[Enum]"/>. Uses special options for enum. Ignores culture.</summary>
        [Enum] = 1026
    End Enum




End Namespace
#End If