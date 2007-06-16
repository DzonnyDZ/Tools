Imports System.ComponentModel, System.Globalization, System.Reflection
#If Config <= Nightly Then 'Stage: Nightly
Namespace ComponentModelT 'ASAP:Mark, Wiki,Comment
    Public Interface ITypeConverter(Of T, TOther)
        Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As TOther) As T
        Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As T) As TOther
        Delegate Function dConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As TOther) As T
        Delegate Function dConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As T) As TOther
    End Interface
    Public Interface ITypeConverterWithValidation(Of T, TOther)
        Inherits ITypeConverter(Of T, TOther)
        Function IsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As TOther) As Boolean
        Delegate Function dIsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As TOther) As Boolean
    End Interface
    Public MustInherit Class TypeConverter(Of T) : Inherits TypeConverter
        Delegate Function dConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object) As T
        Delegate Function dConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As T) As Object
        Delegate Function dIsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object) As Boolean
        Protected NotInheritable Class DAdaptor(Of TOther)
            Public dConvertFrom As ITypeConverterWithValidation(Of T, TOther).dConvertFrom
            Public dConvertTo As ITypeConverterWithValidation(Of T, TOther).dConvertTo
            Public dIsValid As ITypeConverterWithValidation(Of T, TOther).dIsValid
            Public Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object) As T
                Return dConvertFrom.Invoke(context, culture, value)
            End Function
            Public Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As T) As Object
                Return dConvertTo.Invoke(context, culture, value)
            End Function
            Public Function IsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object) As Boolean
                Return dIsValid.Invoke(context, value)
            End Function
            Public Sub New(ByVal d As ITypeConverterWithValidation(Of T, TOther).dConvertFrom)
                dConvertFrom = d
            End Sub
            Public Sub New(ByVal d As ITypeConverterWithValidation(Of T, TOther).dConvertTo)
                dConvertTo = d
            End Sub
            Public Sub New(ByVal d As ITypeConverterWithValidation(Of T, TOther).dIsValid)
                dIsValid = d
            End Sub
            Public Sub New(ByVal dcfrom As ITypeConverterWithValidation(Of T, TOther).dConvertFrom, ByVal dcto As ITypeConverterWithValidation(Of T, TOther).dConvertTo, Optional ByVal dvalid As ITypeConverterWithValidation(Of T, TOther).dIsValid = Nothing)
                dConvertFrom = dcfrom
                dConvertTo = dcto
                dIsValid = dvalid
            End Sub
            Public Sub New()
            End Sub
        End Class
        Public NotOverridable Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
            Return ConverterFrom(sourceType) IsNot Nothing OrElse MyBase.CanConvertFrom(context, sourceType)
        End Function
        Public NotOverridable Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
            Return ConverterTo(destinationType) IsNot Nothing OrElse MyBase.CanConvertTo(context, destinationType)
        End Function
        Public NotOverridable Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
            Dim Conv As dConvertFrom = ConverterFrom(value.GetType)
            If Conv Is Nothing Then
                Return MyBase.ConvertFrom(context, culture, value)
            Else
                Return Conv.Invoke(context, culture, value)
            End If
        End Function
        Public NotOverridable Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
            If Not TypeOf value Is T Then Return MyBase.ConvertTo(context, culture, value, destinationType)
            Dim Conv As dConvertTo = ConverterTo(destinationType)
            If Conv Is Nothing Then
                Return MyBase.ConvertTo(context, culture, value, destinationType)
            Else
                Return Conv.Invoke(context, culture, value)
            End If
        End Function
        Public NotOverridable Overloads Overrides Function CreateInstance(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal propertyValues As System.Collections.IDictionary) As Object
            Return CreateInstance(propertyValues, context)
        End Function
        Public Overridable Overloads Function CreateInstance(ByVal propertyValues As System.Collections.IDictionary, ByVal context As System.ComponentModel.ITypeDescriptorContext) As T
            Return MyBase.CreateInstance(context, propertyValues)
        End Function
        Public NotOverridable Overrides Function IsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object) As Boolean
            Dim Validator As dIsValid = Me.Validator(value.GetType)
            If Validator Is Nothing Then
                Return MyBase.IsValid(context, value)
            Else
                Return Validator.Invoke(context, value)
            End If
        End Function
        'TODO:Allow interfaces and derived classes in converters and validators
        Protected Overridable Function ConverterFrom(ByVal sourceType As Type) As dConvertFrom
            Dim MyType As Type = Me.GetType
            Dim InterfaceType As Type = GetType(ITypeConverter(Of ,)).MakeGenericType(GetType(T), sourceType)
            Dim [Implements] As Boolean = False
            For Each int As Type In MyType.GetInterfaces
                If int.Equals(InterfaceType) Then [Implements] = True : Exit For
            Next int
            If Not [Implements] Then Return Nothing
            Dim Map As InterfaceMapping = MyType.GetInterfaceMap(InterfaceType)
            Dim dType As Type = InterfaceType.GetNestedType("dConvertFrom").MakeGenericType(GetType(T), sourceType)
            Dim DAdaptorType As Type = GetType(DAdaptor(Of )).MakeGenericType(GetType(T), sourceType)
            Dim i As Integer = 0
            For Each Method As MethodInfo In Map.InterfaceMethods
                If Method.Name = "ConvertFrom" Then Return [Delegate].CreateDelegate(GetType(dConvertFrom), Activator.CreateInstance(DAdaptorType, [Delegate].CreateDelegate(dType, Me, Map.TargetMethods(i))), "ConvertFrom")
                i += 1
            Next Method
            Return Nothing
        End Function
        Protected Overridable Function ConverterTo(ByVal destinationType As Type) As dConvertTo
            Dim MyType As Type = Me.GetType
            Dim InterfaceType As Type = GetType(ITypeConverter(Of ,)).MakeGenericType(New Type() {GetType(T), destinationType})
            Dim [Implements] As Boolean = False
            For Each int As Type In MyType.GetInterfaces
                If int.Equals(InterfaceType) Then [Implements] = True : Exit For
            Next int
            Dim Map As InterfaceMapping = MyType.GetInterfaceMap(InterfaceType)
            Dim dType As Type = InterfaceType.GetNestedType("dConvertTo").MakeGenericType(New Type() {GetType(T), destinationType})
            Dim DAdaptorType As Type = GetType(DAdaptor(Of )).MakeGenericType(GetType(T), destinationType)
            Dim i As Integer = 0
            For Each Method As MethodInfo In Map.InterfaceMethods
                If Method.Name = "ConvertTo" Then Return [Delegate].CreateDelegate(GetType(dConvertTo), Activator.CreateInstance(DAdaptorType, [Delegate].CreateDelegate(dType, Me, Map.TargetMethods(i))), "ConvertTo")
                i += 1
            Next Method
            Return Nothing
        End Function
        Protected Overridable Function Validator(ByVal validateType As Type) As dIsValid
            'TODO:If no validator is available, validate through thrown error from convert
            Dim MyType As Type = Me.GetType
            Dim InterfaceType As Type = GetType(ITypeConverterWithValidation(Of ,)).MakeGenericType(New Type() {GetType(T), validateType})
            Dim [Implements] As Boolean = False
            For Each int As Type In MyType.GetInterfaces
                If int.Equals(InterfaceType) Then [Implements] = True : Exit For
            Next int
            If Not [Implements] Then Return Nothing
            Dim Map As InterfaceMapping = MyType.GetInterfaceMap(InterfaceType)
            Dim dType As Type = InterfaceType.GetNestedType("dIsValid").MakeGenericType(New Type() {GetType(T), validateType})
            Dim DAdaptorType As Type = GetType(DAdaptor(Of )).MakeGenericType(GetType(T), validateType)
            Dim i As Integer = 0
            For Each Method As MethodInfo In Map.InterfaceMethods
                If Method.Name = "IsValid" Then Return [Delegate].CreateDelegate(GetType(dIsValid), Activator.CreateInstance(DAdaptorType, [Delegate].CreateDelegate(dType, Me, Map.TargetMethods(i))), "IsValid")
                i += 1
            Next Method
            Return Nothing
        End Function
    End Class
    Public MustInherit Class TypeConverter(Of T, TOther)
        Inherits TypeConverter(Of T)
        Implements ITypeConverterWithValidation(Of T, TOther)
        Public MustOverride Shadows Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As TOther) As T Implements ITypeConverter(Of T, TOther).ConvertFrom
        Public MustOverride Shadows Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As T) As TOther Implements ITypeConverter(Of T, TOther).ConvertTo
        Public Overridable Shadows Function IsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As TOther) As Boolean Implements ITypeConverterWithValidation(Of T, TOther).IsValid
            Try
                ConvertFrom(context, CultureInfo.InvariantCulture, value)
            Catch
                Return False
            End Try
            Return True
        End Function
        Private Adaptor As New DAdaptor(Of TOther)(AddressOf ConvertFrom, AddressOf ConvertTo, AddressOf IsValid)
        'Protected Overrides Function ConverterFrom(ByVal sourceType As System.Type) As TypeConverter(Of T).dConvertFrom
        '    If sourceType.Equals(GetType(TOther)) Then
        '        Return AddressOf Adaptor.ConvertFrom
        '    End If
        '    Return MyBase.ConverterFrom(sourceType)
        'End Function
        'Protected Overrides Function ConverterTo(ByVal destinationType As System.Type) As TypeConverter(Of T).dConvertTo
        '    If destinationType.Equals(GetType(TOther)) Then Return AddressOf Adaptor.ConvertTo
        '    Return MyBase.ConverterTo(destinationType)
        'End Function
        'Protected Overrides Function Validator(ByVal validateType As System.Type) As TypeConverter(Of T).dIsValid
        '    If validateType.Equals(GetType(TOther)) Then Return AddressOf Adaptor.IsValid
        '    Return MyBase.Validator(validateType)
        'End Function
    End Class
End Namespace
#End If