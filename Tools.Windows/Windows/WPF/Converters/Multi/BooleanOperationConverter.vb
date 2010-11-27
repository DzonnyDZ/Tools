Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ConvertersT

    ''' <summary>Multi-value converter that performs a boolean operation on values</summary>
    ''' <remarks>This converter is one-way</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class BooleanOperationConverter
        Implements IMultiValueConverter

        ''' <summary>Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.</summary>
        ''' <returns>A converted value. This converter always produces a <see cref="Boolean"/> value which is then converted to <paramref name="targetType"/>.</returns>
        ''' <param name="values">
        ''' The array of values that the source bindings in the <see cref="T:System.Windows.Data.MultiBinding" /> produces. The value <see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the source binding has no value to provide for conversion (it's ignored).
        ''' Each value in this array should be either <see cref="Boolean"/> (preffere), null or <see cref="DBNull"/> (treated as false), <see cref="DependencyProperty.UnsetValue"/> (ignored - does not participa in a boolean operation), <see cref="Visibility"/> (<see cref="Visibility.Visible"/> is treated as true all other values as false), <see cref="IConvertible"/> (<see cref="IConvertible.ToBoolean"/> is used) or any other type (<see cref="DynamicCast"/> is used).
        ''' </param>
        ''' <param name="targetType">The type of the binding target property. Ignored if null. Primary target type is <see cref="Boolean"/>. Specificly supported type is <see cref="Visibility"/> (true is converted to <see cref="Visibility.Visible"/>, false to <see cref="Visibility.Collapsed"/>). For other types <see cref="DynamicCast"/> is used.</param>
        ''' <param name="parameter">The converter parameter to use. It should be either <see cref="BooleanOperation"/> or <see cref="String"/> (case-insensitive) or <see cref="Integer"/> representing a <see cref="BooleanOperation"/> value. For values of other types <see cref="DynamicCast"/> to <see cref="BooleanOperation"/> is used.</param>
        ''' <param name="culture">The culture to use in the converter. It's used only when converting a value from <paramref name="values"/> which is <see cref="IConvertible"/> to boolean.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="parameter"/> is null.</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="parameter"/> represents invalid <see cref="BooleanOperation"/> value.</exception>
        ''' <exception cref="InvalidCastException"><paramref name="parameter"/> cannot be converted to <see cref="BooleanOperation"/> -or- value form <paramref name="values"/> cannot be converted to <see cref="Boolean"/> -or- return value cannot be converted to <paramref name="targetType"/>.</exception>
        ''' <exception cref="ArgumentException"><paramref name="parameter"/> is <see cref="String"/> but it does not represent one of <see cref="BooleanOperation"/> values (case-insensitive)</exception>
        ''' <exception cref="OverflowException">Arithmetic overflow occured when converting <paramref name="parameter"/> to <see cref="BooleanOperation"/>, value from <paramref name="values"/> to <see cref="Boolean"/> or return value to <paramref name="targetType"/>.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Error performing <see cref="DynamicCast">dynamic casting</see> either from <paramref name="parameter"/> to <see cref="BooleanOperation"/> or from value in <paramref name="values"/> to <see cref="Boolean"/> or from return value to <paramref name="targetType"/>: Casting operators were found but noone is most specific.</exception>
        Public Function Convert(ByVal values() As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
            If parameter Is Nothing Then Throw New ArgumentNullException("parameter")

            Dim operation As BooleanOperation
            If TypeOf parameter Is BooleanOperation Then
                operation = parameter
            ElseIf TypeOf parameter Is String Then
                operation = [Enum].Parse(GetType(BooleanOperation), parameter, True)
            ElseIf TypeOf parameter Is Integer Then
                operation = DirectCast(parameter, Integer)
            Else
                operation = DynamicCast(Of BooleanOperation)(parameter)
            End If

            If values Is Nothing Then values = New Object() {}
            Dim bvals As New List(Of Boolean)
            For Each val As Object In values
                If val Is Nothing OrElse TypeOf val Is DBNull Then
                    bvals.Add(False)
                ElseIf val Is DependencyProperty.UnsetValue Then 'Do nothing - ignore
                ElseIf TypeOf val Is Boolean Then
                    bvals.Add(val)
                ElseIf TypeOf val Is Visibility Then
                    bvals.Add(DirectCast(val, Visibility) = Visibility.Visible)
                ElseIf TypeOf val Is IConvertible Then
                    bvals.Add(DirectCast(val, IConvertible).ToBoolean(culture))
                Else
                    bvals.Add(DynamicCast(Of Boolean)(val))
                End If
            Next
            Dim ret = Convert(operation, bvals.ToArray)
            If targetType Is Nothing OrElse targetType.IsAssignableFrom(GetType(Boolean)) Then Return ret
            If targetType.IsAssignableFrom(GetType(Visibility)) Then Return If(ret, Visibility.Visible, Visibility.Collapsed)
            Return DynamicCast(ret, targetType)
        End Function

        ''' <summary>Performs the boolean operation</summary>
        ''' <param name="operation">An operation to be performed</param>
        ''' <param name="values">Boolean values to be processed by the operation</param>
        ''' <returns>Result of the operation. When <paramref name="values"/> is null or empty all operations but <see cref="BooleanOperation.NAnd"/>, <see cref="BooleanOperation.NOr"/> and <see cref="BooleanOperation.[True]"/> returns false.</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="operation"/> is not one of <see cref="BooleanOperation"/> values.</exception>
        Public Function Convert(ByVal operation As BooleanOperation, ByVal ParamArray values As Boolean()) As Boolean
            If Not operation.IsDefined Then Throw New InvalidEnumArgumentException("operation", operation, GetType(BooleanOperation))
            Select Case operation
                Case BooleanOperation.True : Return True
                Case BooleanOperation.False : Return False
            End Select
            If values Is Nothing OrElse values.Length = 0 Then
                Select Case operation
                    Case BooleanOperation.And, BooleanOperation.Or, BooleanOperation.Xor, BooleanOperation.Xnor : Return False
                    Case BooleanOperation.NAnd, BooleanOperation.NOr : Return True
                End Select
            End If
            Dim ret As Boolean
            Select Case operation
                Case BooleanOperation.And, BooleanOperation.NAnd : ret = True
                Case BooleanOperation.Or, BooleanOperation.NOr, BooleanOperation.Xor, BooleanOperation.Xnor : ret = False
            End Select
            For Each bv In values
                Select Case operation
                    Case BooleanOperation.And, BooleanOperation.NAnd
                        If bv = False Then
                            ret = False
                            Exit For
                        End If
                    Case BooleanOperation.Or, BooleanOperation.NOr
                        If bv = True Then
                            ret = True
                            Exit For
                        End If
                    Case BooleanOperation.Xor
                        If ret AndAlso bv Then
                            ret = False
                            Exit For
                        ElseIf bv Then
                            ret = True
                        End If
                    Case BooleanOperation.Xnor
                        If ret AndAlso bv = False Then
                            ret = False
                            Exit For
                        ElseIf bv = False Then
                            ret = True
                        End If
                End Select
            Next
            Select Case operation
                Case BooleanOperation.NOr, BooleanOperation.NAnd : Return Not ret
                Case Else : Return ret
            End Select
        End Function


        ''' <summary>Always throws <see cref="NotSupportedException"/> - this is one way converter.</summary>
        ''' <returns>Never returns - always throws <see cref="NotSupportedException"/>.</returns>
        ''' <param name="value">Ignored</param>
        ''' <param name="targetTypes">Ignored</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <exception cref="NotSupportedException">Always. This converter is one-way.</exception>
        Private Function IMultiValueConverter_ConvertBack(ByVal value As Object, ByVal targetTypes() As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
            Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_CannotConvertBack.f([GetType].Name))
        End Function
    End Class

    ''' <summary>Booelan operations supported by <see cref="BooleanOperationConverter"/></summary>
    Public Enum BooleanOperation
        ''' <summary>Logical And</summary>
        [And]
        ''' <summary>Logical Or</summary>
        [Or]
        ''' <summary>Logical And, result negated - Not(a And b And c ...)</summary>
        NAnd
        ''' <summary>Logical Or, result negated - Not(a Or b Or c ...)</summary>
        NOr
        ''' <summary>Exclusive Or (returns true when exactly one value is true)</summary>
        [Xor]
        ''' <summary>Exclusive negated or (returns true when exacly one value is false)</summary>
        Xnor
        ''' <summary>Always returns true (tautology)</summary>
        [True]
        ''' <summary>Always returns false</summary>
        [False]
    End Enum
End Namespace
#End If
