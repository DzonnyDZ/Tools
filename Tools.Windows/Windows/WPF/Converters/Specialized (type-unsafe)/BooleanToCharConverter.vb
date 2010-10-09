Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter to convert <see cref="Boolean"/> values to <see cref="Char"/>. It also supports null values instead of booleand and <see cref="String"/> isntead of <see cref="Char"/>.</summary>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class BooleanToCharConverter
        Implements IValueConverter
        ''' <summary>Default value used for parameter of <see cref="BooleanToCharConverter"/> when null is supplied</summary>
        ''' <remarks><see cref="BooleanToCharConverter"/> parameter consists of 3 comma-separated strings each containing characters representing one nullable boolean value: 1st - True (checked), 2nd - False (unchecked), 3rd - unknow/null (intermediate).
        ''' If you are sure any value is never used, you cann ommit (leave empty) appropriate part of parameter. First character in each group is considered default.
        ''' There is no way to escape comma to pecify it as one of characters.</remarks>
        Public Const DefaultParameter = "☑☒☓✓✔✕✖✗✘◉⌧1+,☐❍❏❐❑❒□▢◯0-,▣◎●■•⌼⌻?"
        ''' <summary>Converts value of type <see cref="Boolean"/> or <see cref="Nullable(Of Boolean)"/>[<see cref="Boolean"/>] to <see cref="Char"/> or <see cref="String"/>.</summary>
        ''' <param name="value">Value to be converted. It shall be <see cref="Boolean"/> value or null</param>
        ''' <param name="targetType">Type to convert value to. It shall be <see cref="Char"/>, <see cref="String"/> or <see cref="Nullable(Of Char)"/>[<see cref="Char"/>] or type <see cref="Type.IsAssignableFrom">assignable</see> from that.</param>
        ''' <param name="parameter">Either null or string containing 3 comma-separated strings defining characters used fot true, false and unknown values. See <see cref="DefaultParameter"/> for details.</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns>One of default characters (first character of group) specified in <paramref name="parameter"/> or <see cref="DefaultParameter"/> depending on <see cref="Boolean"/> value <paramref name="value"/>. Return type depends on requested <paramref name="targetType"/>.</returns>
        ''' <exception cref="TypeMismatchException"><paramref name="parameter"/> is neither null nor <see cref="String"/>. -or- <paramref name="value"/> is neither <see cref="Boolean"/> nor null nor <see cref="Nullable(Of Boolean)"/>[<see cref="Boolean"/>].</exception>
        ''' <exception cref="ArgumentException"><paramref name="parameter"/> does not contain at least one character for group requested by <paramref name="value"/> or does not contain the group at ll.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="targetType"/> is neither <see cref="Char"/> nor <see cref="String"/> nor <see cref="Nullable(Of Char)"/>[<see cref="Char"/>] nor <see cref="Type.IsAssignableFrom">is assignable</see> froma ny of them.</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            Dim par As String
            If parameter Is Nothing Then
                par = DefaultParameter
            ElseIf TypeOf parameter Is String Then
                par = parameter
            Else
                Throw New TypeMismatchException("parameter", parameter, GetType(String))
            End If
            Dim val As Boolean?
            If TypeOf value Is Boolean Then
                val = DirectCast(value, Boolean)
            ElseIf value Is Nothing Then
                val = New Boolean?
            Else
                Throw New TypeMismatchException("value", value, GetType(Boolean?))
            End If
            Dim groups = par.Split(",")
            Dim ret As Char
            If Not val.HasValue Then
                If groups.Length < 3 Then Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_IntermediateValueUndefined, "parameter")
                If String.IsNullOrEmpty(groups(2)) Then Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_IntermediateEmpty, "parameter")
                ret = groups(2)(0)
            ElseIf val.Value Then
                If groups.Length < 1 Then Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_CheckedUndefined, "parameter")
                If String.IsNullOrEmpty(groups(0)) Then Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_CheckedEmpty, "parameter")
                ret = groups(0)(0)
            Else
                If groups.Length < 2 Then Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_UncheckedUndefined, "parameter")
                If String.IsNullOrEmpty(groups(1)) Then Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_UncheckedEmpty, "parameter")
                ret = groups(1)(0)
            End If
            If targetType.IsAssignableFrom(GetType(Char)) Then
                Return ret
            ElseIf targetType.IsAssignableFrom(GetType(String)) Then
                Return New String(ret, 1)
            ElseIf targetType.IsAssignableFrom(GetType(Char?)) Then
                Return CType(ret, Char?)
            Else
                Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_ConverterSupportsOnlyTargetTypes_2AndOneGeneric.f(Me.GetType.Name, GetType(Char).Name, GetType(String).Name, GetType(Nullable(Of )).Name))
            End If
        End Function
        ''' <summary>Converts value of type <see cref="Char"/> or <see cref="String"/> bact to type <see cref="Boolean"/>.</summary>
        ''' <param name="value">Value to be converted. It shall be null, <see cref="Char"/> or <see cref="String"/>. In case <paramref name="value"/> is <see cref="String"/> it shall be non-empty and only firts character is taken from it.</param>
        ''' <param name="targetType">Requested return type. It shall be <see cref="Boolean"/> or <see cref="Nullable(Of Bollean)"/>[<see cref="Boolean"/>] or type assignable from it. When <paramref name="targetType"/> is <see cref="Boolean"/> this method still can return null if character for intermediate state is passed to <paramref name="value"/>.</param>
        ''' <param name="parameter">Either null or string containing 3 comma-separated strings defining characters used fot true, false and unknown values. See <see cref="DefaultParameter"/> for details.</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns>True, False or null depending on which of comma-separated groups in <paramref name="parameter"/> or <see cref="DefaultParameter"/> <paramref name="value"/> (or first character from <paramref name="value"/> when it is <see cref="String"/>) falls into.</returns>
        ''' <exception cref="NotSupportedException"><paramref name="targetType"/> is neither <see cref="Boolean"/> nor <see cref="Nullable(Of Boolean)"/>[<see cref="Boolean"/>] nor type <see cref="Type.IsAssignableFrom">assignable</see> from it.</exception>
        ''' <exception cref="TypeMismatchException"><paramref name="value"/> is neither null nor <see cref="Char"/> nor <see cref="String"/>. -or- <paramref name="parameter"/> is neither null nor <see cref="String"/>.</exception>
        ''' <exception cref="ArgumentException"><paramref name="value"/> none of characters specified in <paramref name="parameter"/> (or <see cref="DefaultParameter"/> in case <paramref name="parameter"/> is null) except comas.</exception>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Dim val As String
            If Not targetType.IsAssignableFrom(GetType(Boolean)) AndAlso Not targetType.IsAssignableFrom(GetType(Boolean?)) Then _
              Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_ConverterCanConvertBackOnlyTo_1And1Generic.f(Me.GetType.Name, GetType(Boolean).Name, GetType(Nullable(Of )).Name))
            If value Is Nothing OrElse (TypeOf value Is String AndAlso DirectCast(value, String) = "") Then
                Return New Boolean?
            ElseIf TypeOf value Is Char Then
                val = DirectCast(value, Char)
            ElseIf TypeOf value Is String Then
                val = value
            Else
                Throw New TypeMismatchException(WindowsT.WPF.ConvertersT.ConverterResources.ex_CanConvertBackOnlyFromColors.f(Me.GetType.Name, GetType(Char).Name, GetType(String).Name), value)
            End If
            Dim par As String
            If parameter Is Nothing Then
                par = DefaultParameter
            ElseIf TypeOf parameter Is String Then
                par = parameter
            Else
                Throw New TypeMismatchException("parameter", parameter, GetType(String))
            End If
            Dim parts = par.Split(",")
            If parts.Length >= 1 AndAlso parts(0).Contains(val(0)) Then
                Return True
            ElseIf parts.Length >= 2 AndAlso parts(1).Contains(val(0)) Then
                Return False
            ElseIf parts.Length >= 3 AndAlso parts(2).Contains(val(0)) Then
                Return New Boolean?
            Else
                Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_CharacterNotBoolean.f(val(0)), "value")
            End If
        End Function
    End Class
End Namespace
#End If