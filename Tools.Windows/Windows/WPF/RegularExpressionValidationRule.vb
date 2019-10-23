Imports System.Windows.Controls
Imports System.Text.RegularExpressions

Namespace WindowsT.WPF
    ''' <summary>A <see cref="System.Windows.Controls.ValidationRule"/>-derived class which supports the use of regular expressions for validation.</summary>
    ''' <remarks><a href="http://www.codeproject.com/KB/WPF/RegexValidationInWPF.aspx">http://www.codeproject.com/KB/WPF/RegexValidationInWPF.aspx</a></remarks>
    ''' <author www="http://www.codeproject.com/Members/Josh-Smith">Josh Smith</author>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public Class RegularExpressionValidationRule
        Inherits ValidationRule
#Region "Data"
        Private _options As RegexOptions = RegexOptions.Compiled
        Private _pattern As String
        Private _regularExpression As Regex
#End Region

#Region "Constructors"
        ''' <summary>Parameterless constructor.</summary>
        Public Sub New()
        End Sub

        ''' <summary>Creates a RegexValidationRule with the specified regular expression.</summary>
        ''' <param name="regexText">The regular expression used by the new instance.</param>
        Public Sub New(ByVal regexText As String)
            Me.Pattern = regexText
        End Sub

        ''' <summary>Creates a RegexValidationRule with the specified regular expression and error message.</summary>
        ''' <param name="regexText">The regular expression used by the new instance.</param>
        ''' <param name="errorMessage">The error message used when validation fails.</param>
        Public Sub New(ByVal regexText As String, ByVal errorMessage As String)
            Me.New(regexText)
            Me.Options = _options
        End Sub

        ''' <summary>Creates a RegexValidationRule with the specified regular expression, error message, and RegexOptions.</summary>
        ''' <param name="regexText">The regular expression used by the new instance.</param>
        ''' <param name="errorMessage">The error message used when validation fails.</param>
        ''' <param name="regexOptions">The RegexOptions used by the new instance.</param>
        Public Sub New(ByVal regexText As String, ByVal errorMessage As String, ByVal regexOptions As RegexOptions)
            Me.New(regexText)
            Me.Options = regexOptions
        End Sub
#End Region

#Region "Properties"
        ''' <summary>Gets or sets the error message to be used when validation fails.</summary>
        Public Property ErrorMessage() As String

        ''' <summary>Gets or sets value indicating if empty string values are always considered valid</summary>
        Public Property IgnoreEmptyString As Boolean

        ''' <summary>Gets or sets the RegexOptions to be used during validation. This property's default value is <see cref="RegexOptions.Compiled"/>.</summary>
        Public Property Options() As RegexOptions
            Get
                Return _options
            End Get
            Set(ByVal value As RegexOptions)
                If Options <> value Then
                    _regularExpression = CreateRegularExpression(Pattern, value)
                    _options = value
                End If
            End Set
        End Property

        ''' <summary>Gets or sets the regular expression used during validation.</summary>
        Public Property Pattern() As String
            Get
                Return _pattern
            End Get
            Set(ByVal value As String)
                If Pattern <> value Then
                    _regularExpression = CreateRegularExpression(value, Options)
                    _pattern = value
                End If
            End Set
        End Property
        ''' <summary>Gets regular expression created from <see cref="Pattern"/> used during validation</summary>
        ''' <remarks>Regular expression is re-created when value of the <see cref="Pattern"/> or <see cref="Options"/> property changes</remarks>
        Public ReadOnly Property RegularExpression As Regex
            Get
                Return _regularExpression
            End Get
        End Property

#End Region
        ''' <summary>Generates regular expression</summary>
        ''' <param name="pattern">Pattern to generate regular expression from</param>
        ''' <param name="options">Regular expression options</param>
        ''' <returns>Regular exspression created form given <paramref name="pattern"/> and <paramref name="options"/>. Nulll when <paramref name="pattern"/> is null or an empty string.</returns>
        Protected Overridable Function CreateRegularExpression(ByVal pattern As String, ByVal options As RegexOptions) As Regex
            If pattern = "" Then Return Nothing
            Return New Regex(pattern, options)
        End Function

#Region "Validate"
        ''' <summary>Validates the 'value' argument using the regular expression and RegexOptions associated with this object.</summary>
        Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As Globalization.CultureInfo) As ValidationResult
            Dim text = If(value Is Nothing, "", value.ToString)
            If RegularExpression IsNot Nothing AndAlso Not (IgnoreEmptyString AndAlso text = "") Then
                If Not RegularExpression.IsMatch(text) Then Return New ValidationResult(False, Me.ErrorMessage)
            End If
            Return ValidationResult.ValidResult
        End Function
#End Region
    End Class
End Namespace