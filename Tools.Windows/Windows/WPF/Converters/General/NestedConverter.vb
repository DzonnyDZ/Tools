Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter that combines two <see cref="IValueConverter">IValueConverters</see> - passes output of in ner converter to outer converter</summary>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class NestedConverter
        Implements IValueConverter
        ''' <summary>Defines possible values used by the <see cref="ParameterRule"/> property to determine to which converter(s) pass converter parameter</summary>
        Public Enum ParameterRules
            ''' <summary>Converter parameter is passed to both, <see cref="Inner">Inner</see> and <see cref="Outer">Outer</see> converters</summary>
            Both
            ''' <summary>Converter parameter is passed to <see cref="Inner">Inner</see> converter only</summary>
            InnerOnly
            ''' <summary>Converter parameter is passed to <see cref="Outer">Outer</see> converter only</summary>
            OuterOnly
        End Enum
        ''' <summary>Contains value of the <see cref="ParameterRule"/> property</summary>
        Private _ParameterRule As ParameterRules = ParameterRules.Both
        ''' <summary>Contains value of the <see cref="Outer"/> property</summary>
        Private _Outer As IValueConverter
        ''' <summary>Contains value of the <see cref="Inner"/> property</summary>
        Private _Inner As IValueConverter
        ''' <summary>Contains value of the <see cref="IntermediateTargetType"/> property</summary>
        Private _IntermediateTargetType As Type = GetType(Object)
        ''' <summary>Contains value of the <see cref="IntermediateTargetTypeBack"/> property</summary>
        Private _IntermediateTargetTypeBack As Type = GetType(Object)
        ''' <summary>Contains value of the <see cref="InnerParam"/> property</summary>
        Private _InnerParam As Object
        ''' <summary>Conrains value of the <see cref="OuterParam"/> property</summary>
        Private _OuterParam As Object
        ''' <summary>CTor form inner and outer converters</summary>
        ''' <param name="Inner">Inner converter. This converter is called first and its output is passed to <paramref name="Outer"/> converter.</param>
        ''' <param name="Outer">Outer converter. This converter converts value returned from <paramref name="Inner"/> converter to output.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Inner"/> is null or <paramref name="Outer"/> is null</exception>
        Public Sub New(ByVal Inner As IValueConverter, ByVal Outer As IValueConverter)
            If Inner Is Nothing Then Throw New ArgumentNullException("Inner")
            If Outer Is Nothing Then Throw New ArgumentNullException("Outer")
            _Inner = Inner
            _Outer = Outer
        End Sub
        ''' <summary>Parameter-less CTor to be used by XAML</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Sub New()
        End Sub
        ''' <summary>CTor form inner and outer converters and intermediate target types</summary>
        ''' <param name="Inner">Inner converter. This converter is called first and its output is passed to <paramref name="Outer"/> converter.</param>
        ''' <param name="Outer">Outer converter. This converter converts value returned from <paramref name="Inner"/> converter to output.</param>
        ''' <param name="IntermediateTargetType">Target type of <paramref name="Inner"/> converter when <see cref="Convert"/> is called.</param>
        ''' <param name="ItntermediateTargetTypeBack">Target type of <paramref name="Outer"/> converter when <see cref="ConvertBack"/> is called. If null <see cref="System.Object"/> is assumed.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Inner"/>, <paramref name="Outer"/> or <paramref name="IntermediateTargetType"/> is null</exception>
        Public Sub New(ByVal Inner As IValueConverter, ByVal Outer As IValueConverter, ByVal IntermediateTargetType As Type, Optional ByVal ItntermediateTargetTypeBack As Type = Nothing)
            Me.New(Inner, Outer)
            If IntermediateTargetType Is Nothing Then Throw New ArgumentNullException("IntermediateTargetType")
            _IntermediateTargetType = IntermediateTargetType
            If ItntermediateTargetTypeBack IsNot Nothing Then _IntermediateTargetTypeBack = IntermediateTargetTypeBack
        End Sub
        ''' <summary>Inner converter</summary>
        ''' <remarks>
        ''' <para>When calling <see cref="Convert">Convert</see> this converter's <see cref="IValueConverter.Convert">Convert</see> is called firts and its result is passed to <see cref="IValueConverter.Convert">Convert</see> of <see cref="Outer">Outer</see> converter. When <see cref="Convert">Convert</see> is called, target type of this converter is <see cref="IntermediateTargetType">IntermediateTargetType</see>.</para>
        ''' <para>When calling <see cref="ConvertBack">ConvertBack</see> this converter is called with value returned by <see cref="Outer">Outer</see> converter. Tagret type is target type passed to <see cref="ConvertBack">ConvertBack</see>.</para>
        ''' </remarks>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        Public Property Inner() As IValueConverter
            <DebuggerStepThrough()> Get
                Return _Inner
            End Get
            Set(ByVal value As IValueConverter)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                _Inner = value
            End Set
        End Property
        ''' <summary>Outer converter</summary>
        ''' <remarks>
        ''' <para>When calling <see cref="Convert">Convert</see> this converter is called to convert value returned by <see cref="Inner">Inner</see> converter to tagret type passed to <see cref="Convert">Convert</see>.</para>
        ''' <para>When calling <see cref="ConvertBack">ConvertBack</see> this converter is called to first with target type <see cref="IntermediateTargetTypeBack">IntermediateTargetTypeBack</see>. Return value of this converter's <see cref="IValueConverter.Convert">Convert</see> is passed to <see cref="IValueConverter.Convert">Convert</see> of <see cref="Inner">Inner</see> converter.</para>
        ''' </remarks>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        Public Property Outer() As IValueConverter
            <DebuggerStepThrough()> Get
                Return _Outer
            End Get
            Set(ByVal value As IValueConverter)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                _Outer = value
            End Set
        End Property
        ''' <summary>Target type of <see cref="Inner">Inner</see> converter when <see cref="Convert"/> is called</summary>
        ''' <value>Default value is <see cref="System.Object"/></value>
        ''' <remarks>This property can be set only via constructor</remarks>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        <DefaultValue(GetType(Object))> _
        Public Property IntermediateTargetTypeBack() As Type
            <DebuggerStepThrough()> Get
                Return _IntermediateTargetTypeBack
            End Get
            Set(ByVal value As Type)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                _IntermediateTargetTypeBack = value
            End Set
        End Property
        ''' <summary>Target type of <see cref="Outer">Outer</see> converter when <see cref="ConvertBack"/> is called</summary>
        ''' <value>Default value is <see cref="System.Object"/></value>
        ''' <remarks>This property can be set only via constructor</remarks>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        <DefaultValue(GetType(Object))> _
        Public Property IntermediateTargetType() As Type
            <DebuggerStepThrough()> Get
                Return _IntermediateTargetType
            End Get
            Set(ByVal value As Type)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                _IntermediateTargetType = value
            End Set
        End Property
        ''' <summary>Gets or sets value to be passed to converter parameter of <see cref="Inner">Inner</see> converter instead of value passed to <see cref="Convert">Convert</see> or <see cref="ConvertBack">ConvertBack.</see></summary>
        ''' <value>Value to be passed to converter parameter of <see cref="IValueConverter.Convert">Convert</see> or <see cref="IValueConverter.ConvertBack">ConvertBack</see> of <see cref="Inner">Inner</see> converter. Null to obey <see cref="ParameterRule"/>.</value>
        ''' <returns>Value being passed to converter parameter of <see cref="IValueConverter.Convert">Convert</see> and <see cref="IValueConverter.ConvertBack">ConvertBack</see> of <see cref="Inner">Inner</see> converter. Null when <see cref="ParameterRule"/> shall determine value passed to the methods.</returns>
        Public Property InnerParam() As Object
            <DebuggerStepThrough()> Get
                Return _InnerParam
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Object)
                _InnerParam = value
            End Set
        End Property
        ''' <summary>Gets or sets value to be passed to converter parameter of <see cref="Outer">Outer</see> converter instead of value passed to <see cref="Convert">Convert</see> or <see cref="ConvertBack">ConvertBack.</see></summary>
        ''' <value>Value to be passed to converter parameter of <see cref="IValueConverter.Convert">Convert</see> or <see cref="IValueConverter.ConvertBack">ConvertBack</see> of <see cref="Outer">Outer</see> converter. Null to obey <see cref="ParameterRule"/>.</value>
        ''' <returns>Value being passed to converter parameter of <see cref="IValueConverter.Convert">Convert</see> and <see cref="IValueConverter.ConvertBack">ConvertBack</see> of <see cref="Outer">Outer</see> converter. Null when <see cref="ParameterRule"/> shall determine value passed to the methods.</returns>
        Public Property OuterParam() As Object
            <DebuggerStepThrough()> Get
                Return _OuterParam
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Object)
                _OuterParam = value
            End Set
        End Property
        ''' <summary>Gets or sets rule used to determine which converter(s) converter parameter is passed to when <see cref="Convert"/> or <see cref="ConvertBack"/> is called</summary>
        ''' <value>Rule to be used by <see cref="Convert"/> and <see cref="ConvertBack"/> to determine which converter(s) pass converter parameter to. Default value is <see cref="ParameterRules.Both"/></value>
        ''' <returns>RUle currently used by <see cref="Convert"/> and <see cref="ConvertBack"/> to determine which converter(s) pass converter parameter to.</returns>
        ''' <remarks>Converter which is exluded by the rule from receiving converter parameter to its <see cref="IValueConverter.Convert">Convert</see> or <see cref="IValueConverter.ConvertBack">ConvertBack</see> receives null insted.
        ''' <para>This rule can be overridden by setting converter parameter value directly using <see cref="InnerParam"/> and <see cref="OuterParam"/> properties.</para></remarks>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not one of <see cref="ParameterRules"/> values.</exception>
        Public Property ParameterRule() As ParameterRules
            <DebuggerStepThrough()> Get
                Return _ParameterRule
            End Get
            Set(ByVal value As ParameterRules)
                If Not value.IsDefined Then Throw New InvalidEnumArgumentException("value", value, value.GetType)
                _ParameterRule = value
            End Set
        End Property
        ''' <summary>Converts a value using <see cref="IValueConverter.Convert"/> of <see cref="Inner">Inner</see> converter first and then of <see cref="Outer">Outer</see> one.</summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">The converter parameter to use. <see cref="ParameterRule"/> determines which converter(s) receive this value.</param>
        ''' <param name="culture">The culture to use in converters.</param>
        ''' <exception cref="InvalidOperationException"><see cref="Inner"/> or <see cref="Outer"/> is null</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If Inner Is Nothing Then Throw New InvalidOperationException(ConverterResources.ex_ValueMustBeSetPriorCalling.f("Inner", "Convert"))
            If Outer Is Nothing Then Throw New InvalidOperationException(ConverterResources.ex_ValueMustBeSetPriorCalling.f("Outer", "Convert"))
            Return Outer.Convert(Inner.Convert(value, IntermediateTargetType, If(InnerParam IsNot Nothing, InnerParam, If(ParameterRule <> ParameterRules.OuterOnly, parameter, Nothing)), culture), targetType, If(OuterParam IsNot Nothing, OuterParam, If(ParameterRule <> ParameterRules.InnerOnly, parameter, Nothing)), culture)
        End Function

        ''' <summary>Converts a value using <see cref="IValueConverter.ConvertBack"/> of <see cref="Outer">Outer</see> converter first and then of <see cref="Inner">Inner</see> one.</summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="targetType">The type to convert to.</param>
        ''' <param name="parameter">The converter parameter to use. <see cref="ParameterRule"/> determines which converter(s) receive this value.</param>
        ''' <param name="culture">The culture to use in converters.</param>
        ''' <exception cref="InvalidOperationException"><see cref="Inner"/> or <see cref="Outer"/> is null</exception>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If Inner Is Nothing Then Throw New InvalidOperationException(ConverterResources.ex_ValueMustBeSetPriorCalling.f("Inner", "ConvertBack"))
            If Outer Is Nothing Then Throw New InvalidOperationException(ConverterResources.ex_ValueMustBeSetPriorCalling.f("Outer", "ConvertBack"))
            Return Inner.ConvertBack(Outer.Convert(value, IntermediateTargetTypeBack, If(OuterParam IsNot Nothing, OuterParam, If(ParameterRule <> ParameterRules.InnerOnly, parameter, Nothing)), culture), targetType, If(InnerParam IsNot Nothing, InnerParam, If(ParameterRule <> ParameterRules.OuterOnly, parameter, Nothing)), culture)
        End Function
    End Class
End Namespace
#End If