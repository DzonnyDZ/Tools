Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ConvertersT

    ''' <summary>Base class for comparison converters</summary>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public MustInherit Class ComapreConverterBase
        Inherits OneWayBooleanConverterBase

        ''' <summary>Performs the comparison</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <remarks>For Greater, Greater or equal, Less and Less or equal comparisons either <paramref name="value"/> or <paramref name="parameter"/> must be <see cref="System.IComparable"/>.</remarks>
        Protected NotOverridable Overloads Overrides Function Convert(ByVal value As Object, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Boolean?
            Select Case Operation
                Case ComparisonOperation.True : Return True
                Case ComparisonOperation.False : Return False
            End Select
            If value Is Nothing Then
                Select Case Operation
                    Case ComparisonOperation.Equals, ComparisonOperation.IsSame, ComparisonOperation.IsSameType : Return parameter Is Nothing
                    Case ComparisonOperation.NotEquals, ComparisonOperation.IsNotSame, ComparisonOperation.IsNotSameType : Return parameter IsNot Nothing
                    Case ComparisonOperation.GreaterOrEqual, ComparisonOperation.LessOrEqual : Return If(parameter Is Nothing, True, New Boolean?)
                    Case Else : Return Nothing
                End Select
            End If
            If parameter Is Nothing Then
                Select Case Operation
                    Case ComparisonOperation.Equals, ComparisonOperation.IsSame, ComparisonOperation.IsSameType : Return value Is Nothing
                    Case ComparisonOperation.NotEquals, ComparisonOperation.IsNotSame, ComparisonOperation.IsNotSameType : Return value IsNot Nothing
                    Case ComparisonOperation.GreaterOrEqual, ComparisonOperation.LessOrEqual : Return If(value Is Nothing, True, New Boolean?)
                    Case Else : Return Nothing
                End Select
            End If
            Select Case Operation
                Case ComparisonOperation.IsSame : If Not value.GetType.Equals(parameter.GetType) Then Return False
                Case ComparisonOperation.IsNotSameType : If Not value.GetType.Equals(parameter.GetType) Then Return True
                Case ComparisonOperation.IsSameType : Return value.GetType.Equals(parameter.GetType)
                Case ComparisonOperation.IsNotSameType : Return Not value.GetType.Equals(parameter.GetType)
            End Select
            Select Case Operation
                Case ComparisonOperation.Equals : Return value.Equals(parameter)
                Case ComparisonOperation.NotEquals : Return Not value.Equals(parameter)
            End Select
            Dim comparisonResult As Integer
            If TypeOf value Is IComparable Then
                comparisonResult = DirectCast(value, IComparable).CompareTo(DynamicCast(parameter, value.GetType))     'TODO: This should be more clever
            ElseIf TypeOf parameter Is IComparable Then
                comparisonResult = -DirectCast(parameter, IComparable).CompareTo(value)
            Else
                Return Nothing
            End If
            Select Case Operation
                Case ComparisonOperation.Less : Return comparisonResult < 0
                Case ComparisonOperation.LessOrEqual : Return comparisonResult <= 0
                Case ComparisonOperation.Equals : Return comparisonResult = 0
                Case ComparisonOperation.GreaterOrEqual : Return comparisonResult >= 0
                Case ComparisonOperation.Greater : Return comparisonResult > 0
                Case ComparisonOperation.NotEquals : Return comparisonResult <> 0
            End Select
            Return Nothing
        End Function
        ''' <summary>When overriden in derived class gets comparison implemented by derived class</summary>
        Public MustOverride ReadOnly Property Operation As ComparisonOperation
    End Class

    ''' <summary>Comparison operations supported by <see cref="ComapreConverterBase"/></summary>
    ''' <version version="1.5.3">This enumeration is new in version 1.5.3</version>
    Public Enum ComparisonOperation
        ''' <summary>True when instances are equal based on <see cref="System.Object.Equals"/>. null = null, null != non-null</summary>
        Equals
        ''' <summary>True when instances are equal based on <see cref="System.Object.Equals"/> and are of same type. null = null, null != non-null</summary>
        IsSame
        ''' <summary>True when instances are not equal based on <see cref="System.Object.Equals"/>. null = null, null != non-null</summary>
        NotEquals
        ''' <summary>True when instances are not equal based on <see cref="System.Object.Equals"/> or are not of same type. null = null, null != non-null</summary>
        IsNotSame
        ''' <summary>True when one instance is greater than the other  (any operand null => null)</summary>
        Greater
        ''' <summary>True when one instance is less than the other  (any operand null => null)</summary>
        Less
        ''' <summary>True when one instance is greater than or same as the other  (one operand null => null, null = null)</summary>
        GreaterOrEqual
        ''' <summary>True when one instance is less than or same as the other  (one operand null => null, null = null)</summary>
        LessOrEqual
        ''' <summary>Tautology - always returns true (any operand null => null)</summary>
        [True]
        ''' <summary>Always returns false</summary>
        [False]
        ''' <summary>True when two instance s are of same type. (null = null (true), null != non-null (false))</summary>
        IsSameType
        ''' <summary>True if type of two instances is different (null = null (false), null != non-null (true))</summary>
        IsNotSameType
    End Enum

    ''' <summary>Generic comparison converter</summary>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public NotInheritable Class AnyComparisonConverter
        Inherits ComapreConverterBase
        ''' <summary>CTor - creates a new instance of the <see cref="AnyComparisonConverter"/> initialized to <see cref="ComparisonOperation.Equals"/> operation</summary>
        Public Sub New()
            Me.New(ComparisonOperation.Equals)
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="AnyComparisonConverter"/> and sets the operatopn</summary>
        ''' <param name="operation">Comparison operation to be performed by this converter</param>
        Public Sub New(ByVal operation As ComparisonOperation)
            ActualOperation = operation
        End Sub
        ''' <summary>Gets or sets comparision operation performed by this converter</summary>
        <DefaultValue(ComparisonOperation.Equals)>
        Public Property ActualOperation As ComparisonOperation
        ''' <summary>Gets comparison implemented by derived class</summary>
        ''' <remarks>To set value of this property use <see cref="ActualOperation"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public Overrides ReadOnly Property Operation As ComparisonOperation
            Get
                Return ActualOperation
            End Get
        End Property
    End Class

    ''' <summary>Converter that can be used to detect wheather value is less than parameter</summary>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public NotInheritable Class LessThanConverter
        Inherits ComapreConverterBase
        ''' <summary>When overriden in derived class gets comparison implemented by derived class</summary>
        ''' <returns><see cref="ComparisonOperation.Less"/></returns>
        Public Overrides ReadOnly Property Operation As ComparisonOperation
            Get
                Return ComparisonOperation.Less
            End Get
        End Property
    End Class

    ''' <summary>Converter that can be used to detect wheather value is greater than parameter</summary>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public NotInheritable Class GreaterThanConverter
        Inherits ComapreConverterBase
        ''' <summary>When overriden in derived class gets comparison implemented by derived class</summary>
        ''' <returns><see cref="ComparisonOperation.Greater"/></returns>
        Public Overrides ReadOnly Property Operation As ComparisonOperation
            Get
                Return ComparisonOperation.Greater
            End Get
        End Property
    End Class

End Namespace
#End If
