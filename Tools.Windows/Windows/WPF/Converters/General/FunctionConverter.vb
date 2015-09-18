Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Delegate that represents <see cref="IValueConverter.Convert"/> and <see cref="IValueConverter.ConvertBack"/> functions</summary>
    ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    ''' <param name="value">The value produced by the binding source.</param>
    ''' <param name="targetType">The type of the binding target property.</param>
    ''' <param name="parameter">The converter parameter to use.</param>
    ''' <param name="culture">The culture to use in the converter.</param>
    ''' <version version="1.5.3">This delegate is new in version 1.5.3</version>
    Public Delegate Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object

    ''' <summary>Converter that performs conversion by calling any function</summary>
    ''' <remarks>
    ''' This is general purpose implementation of <see cref="IValueConverter"/> which's goal is to simplify creation of WPF converters by not needing to have class for each simple convertor. Using this class it is possible to just implement 2 (or one forn one-way) functions for each convertors.
    ''' <para>Alternativly convertor can be delegate-powerd. But delegates cannot be AFAIK set in XAML.</para>
    ''' </remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class FunctionConverter
        Implements IValueConverter
        ''' <summary>Contains value of the <see cref="Type"/> property</summary>
        Private _Type As Type
        ''' <summary>Contains value of the <see cref="[Function]"/> property</summary>
        Private _Function As String
        ''' <summary>Contains value of the <see cref="FunctionBack"/> property</summary>
        Private _FunctionBack As String
        ''' <summary>Contains value of the <see cref="[Delegate]"/> property</summary>
        Private _Delegate As Convert
        ''' <summary>Contains value of the <see cref="DelegateBack"/> property</summary>
        Private _DelegateBack As Convert

        ''' <summary>Gets or sets type function to be called is defined in.</summary>
        ''' <value>Type <see cref="[Function]"/> and <see cref="FunctionBack"/> is defined in.</value>
        ''' <remarks>Ignored for particular conversion direction if <see cref="[Delegate]"/> or <see cref="DelegateBack"/> is set</remarks>
        Public Property Type() As Type
            Get
                Return _Type
            End Get
            Set(ByVal value As Type)
                _Type = value
                _Delegate = Nothing
            End Set
        End Property
        ''' <summary>Name of public statis function in type <see cref="Type"/> to be called when <see cref="Convert"/> is called.</summary>
        ''' <value>Function called when <see cref="Convert"/> is called. Ignored when <see cref="[Delegate]"/> is set.</value>
        ''' <remarks>The function must have same signature as the <see cref="ConvertersT.Convert"/> delegate.
        ''' <para>Setting this property cause <see cref="[Delegate]"/> to be set to null.</para></remarks>
        Public Property [Function]() As String
            Get
                Return _Function
            End Get
            Set(ByVal value As String)
                _Function = value
                _Delegate = Nothing
            End Set
        End Property
        ''' <summary>Name of public statis function in type <see cref="Type"/> to be called when <see cref="ConvertBack"/> is called.</summary>
        ''' <value>Function called when <see cref="ConvertBack"/> is called. Ignored when <see cref="[DelegateBack]"/> is set.</value>
        ''' <remarks>The function must have same signature as the <see cref="ConvertersT.Convert"/> delegate.
        ''' <para>Setting this property cause <see cref="DelegateBack"/> to be set to null.</para></remarks>
        Public Property [FunctionBack]() As String
            Get
                Return _FunctionBack
            End Get
            Set(ByVal value As String)
                _FunctionBack = value
                _DelegateBack = Nothing
            End Set
        End Property
        ''' <summary>Gets or sets value of the <see cref="Type"/> property using type <see cref="Type.AssemblyQualifiedName">assembly qualified name</see>.</summary>
        ''' <returns><see cref="Type"/>.<see cref="Type.AssemblyQualifiedName">AssemblyQualifiedName</see></returns>
        ''' <value>Sets value of the <see cref="Type"/> property by requesting type using <see cref="Type.[GetType]"/>.</value>
        ''' <exception cref="Reflection.TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
        ''' <exception cref="ArgumentException">Value being set is a pointer, passed by reference, or is a generic class with a <see cref="System.Void" /> as its type parameter.</exception>
        ''' <exception cref="TypeLoadException">Value being set is invalid.  -or- Value being set is an empty string.  -or- Value being set represents an array of <see cref="System.TypedReference" />.</exception>
        ''' <exception cref="IO.FileNotFoundException">The assembly or one of its dependencies was not found.</exception>
        ''' <exception cref="IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded.</exception>
        ''' <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
        Public Property TypeName() As String
            Get
                If Type Is Nothing Then Return Nothing
                Return Type.AssemblyQualifiedName
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then Type = Nothing Else Type = Type.GetType(value)
            End Set
        End Property
        ''' <summary>Delegate to be called when <see cref="Convert"/> is called</summary>
        ''' <value>Delegate called when <see cref="Convert"/> is called</value>
        ''' <remarks>Setting this property causes <see cref="[Function]"/> to be set to null.</remarks>
        Public Property [Delegate]() As Convert
            Get
                Return _Delegate
            End Get
            Set(ByVal value As Convert)
                _Delegate = value
                _Function = Nothing
            End Set
        End Property
        ''' <summary>Delegate to be called when <see cref="ConvertBack"/> is called</summary>
        ''' <value>Delegate called when <see cref="ConvertBack"/> is called</value>
        ''' <remarks>Setting this property causes <see cref="[FunctionBack]"/> to be set to null.</remarks>
        Public Property [DelegateBack]() As Convert
            Get
                Return _DelegateBack
            End Get
            Set(ByVal value As Convert)
                _DelegateBack = value
                _FunctionBack = Nothing
            End Set
        End Property
        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <remarks>If <see cref="[Delegate]"/> is not null, this function calls <see cref="[Delegate]"/>; otherwise function attempts to call the <see cref="[Function]"/> function in type <see cref="Type"/>.</remarks>
        ''' <exception cref="InvalidOperationException">Both <see cref="[Delegate]"/> and <see cref="[Function]"/> are null. -or- <see cref="[Delegate]"/> is null and <see cref="Type"/> is null.</exception> 
        ''' <exception cref="Reflection.AmbiguousMatchException">More than one method is found with the name <see cref="[Function]"/> and matching the specified binding constraints (<see cref="ConvertersT.Convert"/> delegate signature).</exception>
        ''' <exception cref="ArgumentException">Parameters of this method do not match the signature of the method <see cref="[Function]"/>.</exception>
        ''' <exception cref="Reflection.TargetInvocationException">The invoked method throws an exception.</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to execute the method.</exception>
        ''' <exception cref="InvalidOperationException">The type that declares the method is an open generic type.</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If [Delegate] IsNot Nothing Then
                Return [Delegate].Invoke(value, targetType, parameter, culture)
            ElseIf [Function] Is Nothing Then
                Throw New InvalidOperationException(ConverterResources.ex_FunctionForForwardConversionHaveNotBeenSet)
            ElseIf Type Is Nothing Then
                Throw New InvalidOperationException(ConverterResources.ex_IsNull.f("Type"))
            Else
                Dim f = Type.GetMethod([Function], Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static, Nothing, _
                           New Type() {GetType(Object), GetType(Type), GetType(Object), GetType(System.Globalization.CultureInfo)}, New System.Reflection.ParameterModifier() {})
                Return f.Invoke(Nothing, New Object() {value, targetType, parameter, culture})
            End If
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="targetType">The type to convert to.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <remarks>If <see cref="[DelegateBack]"/> is not null, this function calls <see cref="[DelegateBack]"/>; otherwise function attempts to call the <see cref="[FunctionBack]"/> function in type <see cref="Type"/>.</remarks>
        ''' <exception cref="InvalidOperationException">Both <see cref="[DelegateBack]"/> and <see cref="[FunctionBack]"/> are null. -or- <see cref="[DelegateBack]"/> is null and <see cref="Type"/> is null.</exception> 
        ''' <exception cref="System.Reflection.AmbiguousMatchException">More than one method is found with the name <see cref="[FunctionBack]"/> and matching the specified binding constraints (<see cref="ConvertersT.Convert"/> delegate signature).</exception>
        ''' <exception cref="ArgumentException">Parameters of this method do not match the signature of the method <see cref="[FunctionBack]"/>.</exception>
        ''' <exception cref="Reflection.TargetInvocationException">The invoked method throws an exception.</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to execute the method.</exception>
        ''' <exception cref="InvalidOperationException">The type that declares the method is an open generic type.</exception>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If DelegateBack IsNot Nothing Then
                Return DelegateBack.Invoke(value, targetType, parameter, culture)
            ElseIf FunctionBack Is Nothing Then
                Throw New InvalidOperationException(ConverterResources.ex_FunctionForBackwardConversionHasNotBeenSet)
            ElseIf Type Is Nothing Then
                Throw New InvalidOperationException(ConverterResources.ex_IsNull.f("Type"))
            Else
                Dim f = Type.GetMethod([FunctionBack], Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static, Nothing, _
                                       New Type() {GetType(Object), GetType(Type), GetType(Object), GetType(System.Globalization.CultureInfo)}, New System.Reflection.ParameterModifier() {})
                Return f.Invoke(Nothing, New Object() {value, targetType, parameter, culture})
            End If
        End Function
    End Class
End Namespace
#End If