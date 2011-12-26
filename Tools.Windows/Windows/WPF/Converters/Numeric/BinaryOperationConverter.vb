Imports Tools.ReflectionT
Imports System.Windows.Data, Tools.ExtensionsT
Imports System.Globalization

Namespace WindowsT.WPF.ConvertersT

    ''' <summary>A converter that can invoke any binary operation</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class BinaryOperationConverter
        Implements IValueConverter

        Private _operation As ReflectionT.Operators = ReflectionT.Operators.Addition

        ''' <summary>Gets or sets the operator to be used</summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not one of <see cref="ReflectionT.Operators"/> values</exception>
        ''' <exception cref="ArgumentException">Value being set is <see cref="ReflectionT.Operators.no"/></exception>
        ''' <exception cref="NotSupportedException">Value being set represents assignment operator. -or- Value being set represents operator that is not binary.</exception>
        <DefaultValue(ReflectionT.Operators.Addition)>
        Public Property Operation() As ReflectionT.Operators
            Get
                Return _operation
            End Get
            Set(value As ReflectionT.Operators)
                If Not value.IsDefined Then Throw New InvalidEnumArgumentException("value", value, value.GetType)
                If value = ReflectionT.Operators.no Then Throw New ArgumentException(WPF.Resources.ex_OperatorNotSupported.f(value))
                If value.HasFlag(CType(ReflectionT.Operators_masks.Assignment, ReflectionT.Operators)) Then _
                    Throw New NotSupportedException(WPF.Resources.ex_AssignmentOperator)
                If (value And ReflectionT.Operators_masks.NoOfOperands) <> 2 Then Throw New NotSupportedException(WPF.Resources.ex_OnlyBinaryOperators)
                _operation = value
            End Set
        End Property

        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="InvalidCastException">
        ''' <paramref name="targetType"/> is not null and return value cannot be <see cref="DynamicCast">dynamically casted</see> to <paramref name="targetType"/>. -or-
        ''' Operator accepting <paramref name="value"/> and <paramref name="parameter"/> cannot be found and <paramref name="parameter"/> cannot be <see cref="DynamicCast">dynamically casted</see> to same type as <paramref name="value"/>.
        ''' </exception>
        ''' <exception cref="OverflowException">Error when invoking <see cref="DynamicCast"/> for conversion to <paramref name="targetType"/> or whan converting <paramref name="parameter"/> to same type as <paramref name="value"/>. (same conditions as for <see cref="InvalidCastException"/>, but conversion operator is found and fails).</exception>
        ''' <exception cref="MissingMethodException">Cannot find binary operator</exception>
        Public Function Convert(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            Return [Do](value, targetType, parameter, culture, Operation)
        End Function

        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="targetType">The type to convert to.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="InvalidCastException">
        ''' <paramref name="targetType"/> is not null and return value cannot be <see cref="DynamicCast">dynamically casted</see> to <paramref name="targetType"/>. -or-
        ''' Operator accepting <paramref name="value"/> and <paramref name="parameter"/> cannot be found and <paramref name="parameter"/> cannot be <see cref="DynamicCast">dynamically casted</see> to same type as <paramref name="value"/>.
        ''' </exception>
        ''' <exception cref="OverflowException">Error when invoking <see cref="DynamicCast"/> for conversion to <paramref name="targetType"/> or whan converting <paramref name="parameter"/> to same type as <paramref name="value"/>. (same conditions as for <see cref="InvalidCastException"/>, but conversion operator is found and fails).</exception>
        ''' <exception cref="MissingMethodException">Cannot find binary operator</exception>
        Public Function ConvertBack(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Dim inverse = Operation.Reverse
            If inverse = ReflectionT.Operators.no Then Throw New NotSupportedException(WPF.Resources.ex_CannotInvertOperator.f(Operation))
            Return [Do](value, targetType, parameter, culture, inverse)
        End Function

        ''' <summary>Internally converts a value. </summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <param name="operation">Operator to be used</param>
        ''' <exception cref="InvalidCastException">
        ''' <paramref name="targetType"/> is not null and return value cannot be <see cref="DynamicCast">dynamically casted</see> to <paramref name="targetType"/>. -or-
        ''' Operator accepting <paramref name="value"/> and <paramref name="parameter"/> cannot be found and <paramref name="parameter"/> cannot be <see cref="DynamicCast">dynamically casted</see> to same type as <paramref name="value"/>.
        ''' </exception>
        ''' <exception cref="OverflowException">Error when invoking <see cref="DynamicCast"/> for conversion to <paramref name="targetType"/> or whan converting <paramref name="parameter"/> to same type as <paramref name="value"/>. (same conditions as for <see cref="InvalidCastException"/>, but conversion operator is found and fails).</exception>
        ''' <exception cref="MissingMethodException">Cannot find binary operator</exception>
        ''' <remarks><note type="inheritinfo">When doing your own operator lookup logic in derived class, consider caching ope operator lookup. This implementation does per-instance caching.</note></remarks>
        Protected Overridable Function [Do](value As Object, targetType As Type, parameter As Object, culture As CultureInfo, operation As ReflectionT.Operators) As Object
            Dim ret As Object
            Dim v2 = value
            Dim p2 = parameter
            If v2 Is Nothing Then
                If operation = ReflectionT.Operators.Concatenate AndAlso TypeOf p2 Is String Then
                    ret = p2
                ElseIf operation = ReflectionT.Operators.Concatenate AndAlso p2 Is Nothing Then
                    ret = Nothing
                Else
                    ret = Nothing
                End If
            ElseIf v2 Is Nothing Then
                ret = Nothing
            ElseIf p2 Is Nothing Then
                ret = v2
            Else
                Dim key = String.Format("{0}||{1}||{3}", operation, value.GetType, parameter.GetType)

                Dim op As [Delegate] = Nothing
                If Not opCache.TryGetValue(key, op) Then
                    op = FindBinaryOperator(operation, v2.GetType, p2.GetType, True)
                    If op Is Nothing Then
                        p2 = DynamicCast(p2, v2.GetType, True)
                        If p2 Is Nothing Then
                            ret = v2
                        Else
                            op = FindBinaryOperator(operation, v2.GetType, p2.GetType, True)
                        End If
                    End If
                Else
                    opCache.Add(key, Nothing)
                    If op IsNot Nothing AndAlso Not op.Method.GetParameters()(1).ParameterType.IsAssignableFrom(parameter.GetType) Then
                        p2 = DynamicCast(parameter, value.GetType)
                    End If
                End If
                If op IsNot Nothing Then
                    ret = op.DynamicInvoke(v2, p2)
                Else
                    Throw New MissingMethodException(WPF.Resources.ex_CannotFindOperator.f(operation, value.GetType.FullName, parameter.GetType.FullName))
                End If
            End If

            If targetType Is Nothing Then Return ret
            Return DynamicCast(ret, targetType)
        End Function

        ''' <summary>Used to cache results of operator resolution</summary>
        Private ReadOnly opCache As New Dictionary(Of String, [Delegate])
    End Class
End Namespace