Imports System.ComponentModel, System.Globalization, System.Reflection
#If Config <= Nightly Then 'Stage: Nightly
Namespace ComponentModelT 'ASAP:Mark, Wiki,Comment
    ''' <summary>Represents base class for type-safe <see cref="ComponentModel.TypeConverter"/>'s</summary>
    ''' <typeparam name="T">Type that is converted from and to other types</typeparam>
    ''' <remarks>It's not enough to derive from this class to get working type-safe <see cref="ComponentModel.TypeConverter"/>. After deriving from this class you must implement one or more type converter interfaces (protected nested interfaces in this class). Those interfaces tells this class which conversions are available and provides conversion methods.</remarks>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(TypeConverter(Of )), LastChMMDDYYYY:="06/17/2007")> _
    <MainTool(FirstVerMMDDYYYY:="06/16/2007")> _
    Public MustInherit Class TypeConverter(Of T) : Inherits TypeConverter
#Region "Interfaces"
        ''' <summary>Interface for type-safe <see cref="ComponentModel.TypeConverter"/>s (read-only conversion)</summary>
        ''' <typeparam name="TOther">Other type (e.g. <see cref="String"/> - most common). Value of <paramref name="T"/> are mostly converted to this type in order to be show to user and are converted from this type in mostly in order to get user input</typeparam>
        ''' <remarks>By implementing this interface you tells to your base class (<see cref="TypeConverter(Of T)"/>) that you are able to convert from type <paramref name="TOther"/></remarks>
        Protected Interface ITypeConverterFrom(Of TOther)
            ''' <summary>Performs conversion from type <see cref="TOther"/> to type <see cref="T"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to type <see cref="T"/></param>
            ''' <returns>Value of type <see cref="T"/> initialized by <paramref name="value"/></returns>
            Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As TOther) As T
            ''' <summary>Delegate to <see cref="ConverterFrom"/> function</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to type <see cref="T"/></param>
            ''' <returns>Value of type <see cref="T"/> initialized by <paramref name="value"/></returns>
            Delegate Function dConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As TOther) As T
        End Interface
        ''' <summary>Interface for type-safe <see cref="ComponentModel.TypeConverter"/>s (write-only conversion)</summary>
        ''' <typeparam name="TOther">Other type (e.g. <see cref="String"/> - most common). Value of <paramref name="T"/> are mostly converted to this type in order to be show to user and are converted from this type in mostly in order to get user input</typeparam>
        ''' <remarks>By implementing this interface you tells to your base class (<see cref="TypeConverter(Of T)"/>) that you are able to convert to type <paramref name="TOther"/></remarks>
        Protected Interface ITypeConverterTo(Of TOther)
            ''' <summary>Performs conversion from type <see cref="T"/> to type <see cref="TOther"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in type <see cref="TOther"/></returns>
            Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As T) As TOther
            ''' <summary>delegate to <see cref="ConvertTo"/> function</summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in type <see cref="TOther"/></returns>
            Delegate Function dConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As T) As TOther
        End Interface
        ''' <summary>Interface for type-safe <see cref="ComponentModel.TypeConverter"/>s (read-write conversion)</summary>
        ''' <typeparam name="TOther">Other type (e.g. <see cref="String"/> - most common). Value of <paramref name="T"/> are mostly converted to this type in order to be show to user and are converted from this type in mostly in order to get user input</typeparam>
        ''' <remarks>By implementing this interface you tells to your base class (<see cref="TypeConverter(Of T)"/>) that you are able to convert to and from type <paramref name="TOther"/></remarks>
        Protected Interface ITypeConverter(Of TOther)
            Inherits ITypeConverterFrom(Of TOther), ITypeConverterTo(Of TOther)
        End Interface
        ''' <summary>Interface for type-safe <see cref="ComponentModel.TypeConverter"/>s (read-write conversion with validation)</summary>
        ''' <typeparam name="TOther">Other type (e.g. <see cref="String"/> - most common). Value of <paramref name="T"/> are mostly converted to this type in order to be show to user and are converted from this type in mostly in order to get user input</typeparam>
        ''' <remarks>By implementing this interface you tells to your base class (<see cref="TypeConverter(Of T)"/>) that you are able to convert to and from type <paramref name="TOther"/></remarks>
        Protected Interface ITypeConverterWithValidation(Of TOther)
            Inherits ITypeConverter(Of TOther)
            ''' <summary>Returns whether the given instance of <see cref="TOther"/> is valid for type <see cref="T"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="value">Value to test validity</param>
            ''' <returns>true if the specified value is valid for this type <see cref="T"/>; otherwise, false.</returns>
            Function IsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As TOther) As Boolean
            ''' <summary>Delegate to the <see cref="IsValid"/> function</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="value">Value to test validity</param>
            ''' <returns>true if the specified value is valid for this type <see cref="T"/>; otherwise, false.</returns>
            Delegate Function dIsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As TOther) As Boolean
        End Interface
#End Region
#Region "Delegates"
        ''' <summary>Semi type-safe delegate of the <see cref="ConvertFrom"/> function</summary>
        ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
        ''' <param name="value">The <see cref="System.Object"/> to convert.</param>
        ''' <returns>Converted value</returns>
        Protected Delegate Function dConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object) As T
        ''' <summary>Semi type-safe delegate of the <see cref="ConvertTo"/> function</summary>
        ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
        ''' <param name="value">Value to be converted</param>
        ''' <returns>An <see cref="System.Object"/> that represents the converted value.</returns>
        Protected Delegate Function dConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As T) As Object
        ''' <summary>Semi type-safe delegate of the <see cref="IsValid"/> function</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="value">The <see cref="System.Object"/> to test for validity.</param>
        ''' <returns>true if the specified value is valid for this object; otherwise, false.</returns>
        Protected Delegate Function dIsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object) As Boolean
        ''' <summary>Adapts type-safe delegates from <see cref="ITypeConverterWithValidation(Of TOther)"/> to semi type-safe delegates from <see cref="TypeConverter(Of T)"/></summary>
        ''' <typeparam name="TOther">TOther type of delegates</typeparam>
        Protected NotInheritable Class DAdaptor(Of TOther)
            ''' <summary><see cref="ITypeConverterFrom(Of TOther).dConvertFrom"/> delegate of <see cref="ITypeConverterFrom(Of TOther).ConvertFrom"/> to be adapted</summary>
            Public dConvertFrom As ITypeConverterWithValidation(Of TOther).dConvertFrom
            ''' <summary><see cref="ITypeConverterTo(Of TOther).dConvertTo"/> delegate of <see cref="ITypeConverterTo(Of TOther).ConvertTo"/> to be adapted</summary>
            Public dConvertTo As ITypeConverterWithValidation(Of TOther).dConvertTo
            ''' <summary><see cref="ITypeConverterWithValidation(Of TOther).dIsValid"/> delegate of <see cref="ITypeConverterWithValidation(Of TOther).IsValid"/> to be adapted</summary>
            Public dIsValid As ITypeConverterWithValidation(Of TOther).dIsValid
            ''' <summary>Function with sighature of <see cref="TypeConverter(Of T).dConvertFrom"/> that invokes <see cref="dConvertFrom"/> delegate</summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">The <see cref="System.Object"/> to convert.</param>
            ''' <returns>Converted value</returns>
            ''' <exception cref="NullReferenceException"><see cref="dConvertFrom"/> is null</exception>
            Public Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object) As T
                Return dConvertFrom.Invoke(context, culture, value)
            End Function
            ''' <summary>Function with sighature of <see cref="TypeConverter(Of T).dConvertTo"/> that invokes <see cref="dConvertTo"/> delegate</summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>An <see cref="System.Object"/> that represents the converted value.</returns>
            ''' <exception cref="NullReferenceException"><see cref="dConvertTo"/> is null</exception>
            Public Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As T) As Object
                Return dConvertTo.Invoke(context, culture, value)
            End Function
            ''' <summary>Function with sighature of <see cref="TypeConverter(Of T).dIsValid"/> that invokes <see cref="dIsValid"/> delegate</summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="value">The <see cref="System.Object"/> to test for validity.</param>
            ''' <returns>true if the specified value is valid for this object; otherwise, false.</returns>
            ''' <exception cref="NullReferenceException"><see cref="dIsValid"/> is null</exception>
            Public Function IsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object) As Boolean
                Return dIsValid.Invoke(context, value)
            End Function
            ''' <summary>CTor with <see cref="ITypeConverterWithValidation(Of TOther).dConvertFrom"/> delegate</summary>
            ''' <param name="d">Delegate to be adapted (value for <see cref="dConvertFrom"/>)</param>
            Public Sub New(ByVal d As ITypeConverterWithValidation(Of TOther).dConvertFrom)
                dConvertFrom = d
            End Sub
            ''' <summary>CTor with <see cref="ITypeConverterWithValidation(Of TOther).dConvertTo"/> delegate</summary>
            ''' <param name="d">Delegate to be adapted (value for <see cref="dConvertTo"/>)</param>
            Public Sub New(ByVal d As ITypeConverterWithValidation(Of TOther).dConvertTo)
                dConvertTo = d
            End Sub
            ''' <summary>CTor with <see cref="ITypeConverterWithValidation(Of TOther).dIsValid"/> delegate</summary>
            ''' <param name="d">Delegate to be adapted (value for <see cref="dIsValid"/>)</param>
            Public Sub New(ByVal d As ITypeConverterWithValidation(Of TOther).dIsValid)
                dIsValid = d
            End Sub
            ''' <summary>CTor with all delegates</summary>
            ''' <param name="dcfrom"><see cref="ITypeConverterWithValidation(Of TOther).dConvertFrom"/> delegate to be adapted (value for <see cref="dConvertFrom"/>). Can be null.</param>
            ''' <param name="dcto"><see cref="ITypeConverterWithValidation(Of TOther).dConvertTo"/> delegate to be adapted (value for <see cref="dConvertTo"/>). Can be null.</param>
            ''' <param name="dvalid"><see cref="ITypeConverterWithValidation(Of TOther).dIsValid"/> delegate to be adapted (value for <see cref="dIsValid"/>). Can be null or ommited.</param>
            Public Sub New(ByVal dcfrom As ITypeConverterWithValidation(Of TOther).dConvertFrom, ByVal dcto As ITypeConverterWithValidation(Of TOther).dConvertTo, Optional ByVal dvalid As ITypeConverterWithValidation(Of TOther).dIsValid = Nothing)
                dConvertFrom = dcfrom
                dConvertTo = dcto
                dIsValid = dvalid
            End Sub
            ''' <summary>CTor with no delegate</summary>
            Public Sub New()
            End Sub
        End Class
#End Region
        ''' <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="sourceType">A <see cref="System.Type"/> that represents the type you want to convert from.</param>
        ''' <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        ''' <remarks>This function searches for implementation of <see cref="ITypeConverterFrom(Of TOther)"/> interface where TOther is <paramref name="sourceType"/> or type it implements/derives from using <see cref="ConverterFrom"/></remarks>
        Public NotOverridable Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
            Return ConverterFrom(sourceType) IsNot Nothing OrElse MyBase.CanConvertFrom(context, sourceType)
        End Function
        ''' <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="destinationType">A <see cref="System.Type"/> that represents the type you want to convert to.</param>
        ''' <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        ''' <remarks>This function searches for implementation of <see cref="ITypeConverterTo(Of TOther)"/> interface where TOther is <paramref name="destinationType"/> or type it implements/derives from using <see cref="ConverterTo"/></remarks>
        Public NotOverridable Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
            Return ConverterTo(destinationType) IsNot Nothing OrElse MyBase.CanConvertTo(context, destinationType)
        End Function
        ''' <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
        ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="value">The <see cref="System.Object"/> to convert.</param>
        ''' <returns>An <see cref="System.Object"/> that represents the converted value.</returns>
        ''' <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        ''' <remarks>This function searches for <see cref="ITypeConverterFrom(Of TOther)"/> implementation and calls its <see cref="ITypeConverterFrom.ConvertFrom"/> method if found. Otherwise it calls <see cref="ComponentModel.TypeConverter.ConvertFrom"/></remarks>
        Public NotOverridable Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
            Dim Conv As dConvertFrom = ConverterFrom(value.GetType)
            If Conv Is Nothing Then
                Return MyBase.ConvertFrom(context, culture, value)
            Else
                Return Conv.Invoke(context, culture, value)
            End If
        End Function
        ''' <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
        ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="destinationType">The <see cref="System.Type"/> to convert the value parameter to.</param>
        ''' <param name="value">The <see cref="System.Object"/> to convert.</param>
        ''' <returns>An <see cref="System.Object"/> that represents the converted value.</returns>
        ''' <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        ''' <exception cref="System.ArgumentNullException">The destinationType parameter is null</exception>
        ''' <remarks>This function searches for <see cref="ITypeConverterTo(Of TOther)"/> implementation and calls its <see cref="ITypeConverterTo.ConvertTo"/> method if found. Otherwise it calls <see cref="ComponentModel.TypeConverter.ConvertTo"/></remarks>
        Public NotOverridable Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
            If Not TypeOf value Is T Then Return MyBase.ConvertTo(context, culture, value, destinationType)
            Dim Conv As dConvertTo = ConverterTo(destinationType)
            If Conv Is Nothing Then
                Return MyBase.ConvertTo(context, culture, value, destinationType)
            Else
                Return Conv.Invoke(context, culture, value)
            End If
        End Function
        ''' <summary>Re-creates an <see cref="System.Object"/> given a set of property values for the object.</summary>
        ''' <param name="propertyValues">An <see cref="System.Collections.IDictionary"/> that represents a dictionary of new property values.</param>
        ''' <returns>An <see cref="System.Object"/> representing the given <see cref="System.Collections.IDictionary"/>, or null if the object cannot be created. This method always returns null.</returns>
        Public NotOverridable Overloads Overrides Function CreateInstance(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal propertyValues As System.Collections.IDictionary) As Object
            Return CreateInstance(propertyValues, context)
        End Function
        ''' <summary>Re-creates instance of <see cref="T"/> given a set of property values for it.</summary>
        ''' <param name="propertyValues">An <see cref="System.Collections.IDictionary"/> that represents a dictionary of new property values.</param>
        ''' <returns>Instance of <see cref="T"/> representing the given <see cref="System.Collections.IDictionary"/>, or null if the object cannot be created. This method calls <see cref="ComponentModel.TypeConverter.CreateInstance"/> unless it is overriden in derived class.</returns>
        Public Overridable Overloads Function CreateInstance(ByVal propertyValues As System.Collections.IDictionary, ByVal context As System.ComponentModel.ITypeDescriptorContext) As T
            Return MyBase.CreateInstance(context, propertyValues)
        End Function
        ''' <summary>Returns whether the given value object is valid for this type and for the specified context.</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="value">The <see cref="System.Object"/> to test for validity.</param>
        ''' <returns>true if the specified value is valid for this object; otherwise, false.</returns>
        ''' <remarks>This function searches for <see cref="ITypeConverterWithValidation(Of TOther)"/> implementation and calls its <see cref="ITypeConverterWithValidation.IsValid"/> method if found. Otherwise it calls <see cref="ComponentModel.TypeConverter.IsValid"/></remarks>
        Public NotOverridable Overrides Function IsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object) As Boolean
            Dim Validator As dIsValid = Me.Validator(value.GetType)
            If Validator Is Nothing Then
                Return MyBase.IsValid(context, value)
            Else
                Return Validator.Invoke(context, value)
            End If
        End Function
        ''' <summary>Searches derived class implementation for implementation of <see cref="ITypeConverterFrom(Of TOther)"/> where TOther is <paramref name="sourceType"/> or type it implements/derives from</summary>
        ''' <param name="sourceType">Type of <see cref="ITypeConverterFrom(Of TOther)"/> to search for</param>
        ''' <returns><see cref="dConvertFrom"/> delegate that invokes appropriate <see cref="ITypeConverterFrom.dConvertFrom"/> delegate function that performs conversion</returns>
        ''' <remarks>
        ''' <para>This function first searchse for <paramref name="sourceType"/> implementation of <see cref="ITypeConverterFrom(Of TOther)"/> interface. If it is not found it searches for implementation of any interface implemented by <paramref name="sourceType"/> and then for any base class of <paramref name="sourceType"/></para>
        ''' <para>Note for inheritors: This function uses reflection, so it is not very efficient. You can improve an efficiency by overriding this function and returning required delegate. If you want to keep semantics of this function (delegates are automatically found) for classses derived from yours one call base class function <see cref="ConverterFrom"/> if you are asked for delegate for type that you don't provide converter of.
        ''' Because your implementation has signature of <see cref="ITypeConverterFrom.dConvertFrom"/> and you need signature of <see cref="dConvertFrom"/> you should youse <see cref="DAdaptor(Of TOther)"/> to adapt your delegate and return delegate to <see cref="DAdaptor.ConvertFrom"/>.</para>
        ''' </remarks>
        Protected Overridable Function ConverterFrom(ByVal sourceType As Type) As dConvertFrom
            'TODO: Test if all 3 functions work correctly for subclasses/interfaces
            Dim MyType As Type = Me.GetType
            Dim InterfaceType As Type = Nothing
            Dim [Implements] As Boolean = False
            Dim BaseType As Type = sourceType
            Dim Level As Integer = 0
            While Not [Implements] AndAlso BaseType IsNot Nothing
                If Level = 1 Then
                    For Each TypeInt As Type In sourceType.GetInterfaces
                        InterfaceType = GetType(ITypeConverterFrom(Of )).MakeGenericType(GetType(T), TypeInt)
                        For Each int As Type In MyType.GetInterfaces
                            If int.Equals(InterfaceType) Then [Implements] = True : Exit While
                        Next int
                    Next TypeInt
                End If
                InterfaceType = GetType(ITypeConverterFrom(Of )).MakeGenericType(GetType(T), BaseType)
                For Each int As Type In MyType.GetInterfaces
                    If int.Equals(InterfaceType) Then [Implements] = True : Exit While
                Next int
                BaseType = BaseType.BaseType
                Level += 1
            End While
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
        ''' <summary>Searches derived class implementation for implementation of <see cref="ITypeConverterTo(Of TOther)"/> where TOther is <paramref name="destinationType"/> or type derived from it/implementing it</summary>
        ''' <param name="destinationType">Type of <see cref="ITypeConverterTo(Of TOther)"/> to search for</param>
        ''' <returns><see cref="dConvertTo"/> delegate that invokes appropriate <see cref="ITypeConverterTo.dConvertTo"/> delegate function that performs conversion.</returns>
        ''' <remarks>
        ''' <para>This function first searchse for <paramref name="destinationType"/> implementation of <see cref="ITypeConverterTo(Of TOther)"/> interface. If it is not found it searches for implementation of any class derived from <paramref name="sourceType"/> and if <paramref name="sourceType"/> is interface for any of its implementations</para>
        ''' <para>Note for inheritors: This function uses reflection, so it is not very efficient. You can improve an efficiency by overriding this function and returning required delegate. If you want to keep semantics of this function (delegates are automatically found) for classses derived from yours one call base class function <see cref="ConverterTo"/> if you are asked for delegate for type that you don't provide converter of.
        ''' Because your implementation has signature of <see cref="ITypeConverterTo.dConvertTo"/> and you need signature of <see cref="dConvertTo"/> you should youse <see cref="DAdaptor(Of TOther)"/> to adapt your delegate and return delegate to <see cref="DAdaptor.ConvertTo"/>.</para>
        ''' </remarks>
        Protected Overridable Function ConverterTo(ByVal destinationType As Type) As dConvertTo
            Dim MyType As Type = Me.GetType
            Dim InterfaceType As Type = GetType(ITypeConverterTo(Of )).MakeGenericType(New Type() {GetType(T), destinationType})
            Dim [Implements] As Boolean = False
            For Each int As Type In MyType.GetInterfaces
                If int.Equals(InterfaceType) Then [Implements] = True : Exit For
            Next int
            For Each int As Type In MyType.GetInterfaces
                If int.IsGenericType Then
                    Dim GenInt As Type = int.GetGenericTypeDefinition
                    If GenInt.Equals(GetType(ITypeConverterTo(Of ))) Then
                        Dim TOtherType As Type = int.GetGenericArguments()(1)
                        If TOtherType.IsSubclassOf(destinationType) Then
                            InterfaceType = int
                            [Implements] = True
                            Exit For
                        ElseIf TOtherType.IsInterface Then
                            For Each destInt As Type In destinationType.GetInterfaces
                                If destInt.Equals(TOtherType) Then
                                    InterfaceType = int
                                    [Implements] = True
                                    Exit For
                                End If
                            Next destInt
                            If [Implements] Then Exit For
                        End If
                    End If
                End If
            Next int
            If Not [Implements] Then Return Nothing
            Dim Map As InterfaceMapping
            Try
                Map = MyType.GetInterfaceMap(InterfaceType)
            Catch ex As ArgumentException
                Return Nothing
            End Try
            Dim dType As Type = InterfaceType.GetNestedType("dConvertTo").MakeGenericType(New Type() {GetType(T), destinationType})
            Dim DAdaptorType As Type = GetType(DAdaptor(Of )).MakeGenericType(GetType(T), destinationType)
            Dim i As Integer = 0
            For Each Method As MethodInfo In Map.InterfaceMethods
                If Method.Name = "ConvertTo" Then Return [Delegate].CreateDelegate(GetType(dConvertTo), Activator.CreateInstance(DAdaptorType, [Delegate].CreateDelegate(dType, Me, Map.TargetMethods(i))), "ConvertTo")
                i += 1
            Next Method
            Return Nothing
        End Function
        ''' <summary>Provides simple implementation of <see cref="IsValid"/> method derived class implements <see cref="ITypeConverterFrom(Of TOther)"/> but does not implement <see cref="ITypeConverterWithValidation(Of TOther)"/> for the same type</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="value">The <see cref="System.Object"/> to test for validity.</param>
        ''' <returns>True if <see cref="ITypeConverterFrom.ConvertFrom"/> does not thow an exception, otherwise false</returns>
        Protected Function IsValidSimple(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object) As Boolean
            Dim ConvertFrom As dConvertFrom = ConverterFrom(value.GetType)
            If ConvertFrom IsNot Nothing Then
                Try
                    ConvertFrom.Invoke(context, Threading.Thread.CurrentThread.CurrentCulture, value)
                    Return True
                Catch
                    Return False
                End Try
            End If
            Return True
        End Function
        ''' <summary>Searches derived class implementation for implementation of <see cref="ITypeConverterWithValidation(Of TOther)"/> where TOther is <paramref name="validateType"/> or type it implements/derives from</summary>
        ''' <param name="validateType">Type of <see cref="ITypeConverterWithValidation(Of TOther)"/> to search for</param>
        ''' <returns><see cref="dIsValid"/> delegate that invokes appropriate <see cref="ITypeConverterWithValidation.dIsValid"/> delegate function that performs validation. If no method is found but <see cref="ConverterFrom"/> returns non-null this function returns delegate to <see cref="IsValidSimple"/></returns>
        ''' <remarks>
        ''' <para>This function first searchse for <paramref name="validateType"/> implementation of <see cref="ITypeConverterWithValidation(Of TOther)"/> interface. If it is not found it searches for implementation of any interface implemented by <paramref name="validateType"/> and then for any base class of <paramref name="validateType"/></para>
        ''' <para>Note for inheritors: This function uses reflection, so it is not very efficient. You can improve an efficiency by overriding this function and returning required delegate. If you want to keep semantics of this function (delegates are automatically found) for classses derived from yours one call base class function <see cref="Validator"/> if you are asked for delegate for type that you don't provide converter of.
        ''' Because your implementation has signature of <see cref="ITypeConverterWithValidation.dIsValid"/> and you need signature of <see cref="dConvertFrom"/> you should youse <see cref="DAdaptor(Of TOther)"/> to adapt your delegate and return delegate to <see cref="DAdaptor.IsValid"/>.</para>
        ''' </remarks>
        Protected Overridable Function Validator(ByVal validateType As Type) As dIsValid
            Dim MyType As Type = Me.GetType
            Dim InterfaceType As Type = Nothing
            Dim [Implements] As Boolean = False
            Dim BaseType As Type = validateType
            Dim Level As Integer = 0
            While Not [Implements] AndAlso BaseType IsNot Nothing
                If Level = 1 Then
                    For Each TypeInt As Type In validateType.GetInterfaces
                        InterfaceType = GetType(ITypeConverterWithValidation(Of )).MakeGenericType(GetType(T), TypeInt)
                        For Each int As Type In MyType.GetInterfaces
                            If int.Equals(InterfaceType) Then [Implements] = True : Exit While
                        Next int
                    Next TypeInt
                End If
                InterfaceType = GetType(ITypeConverterWithValidation(Of )).MakeGenericType(GetType(T), BaseType)
                For Each int As Type In MyType.GetInterfaces
                    If int.Equals(InterfaceType) Then [Implements] = True : Exit While
                Next int
                BaseType = BaseType.BaseType
                Level += 1
            End While
            If Not [Implements] Then
                Dim ConvertFrom As dConvertFrom = ConverterFrom(validateType)
                If ConvertFrom IsNot Nothing Then
                    Return AddressOf IsValidSimple
                End If
            End If
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
    ''' <summary>Fully type-safe <see cref="ComponentModel.TypeConverter"/></summary>
    ''' <typeparam name="T">Main type that will be conberted to <paramref name="TOther"/> and from <paramref name="TOther"/></typeparam>
    ''' <typeparam name="TOther">Type the <paramref name="T"/> will be converted from and to</typeparam>
    ''' <remarks>This class provides type-safe base of <see cref="ComponentModel.TypeConverter"/> for two types. You can extend its support for another types by implementing another <see cref="TypeConverter(Of T)"/> nested interfaces.</remarks>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(TypeConverter(Of )), LastChMMDDYYYY:="06/17/2007")> _
    <Tool(GetType(TypeConverter(Of )), FirstVerMMDDYYYY:="06/16/2007")> _
    Public MustInherit Class TypeConverter(Of T, TOther)
        Inherits TypeConverter(Of T)
        Implements ITypeConverterWithValidation(Of TOther)

        ''' <summary>Performs conversion from type <see cref="TOther"/> to type <see cref="T"/></summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
        ''' <param name="value">Value to be converted to type <see cref="T"/></param>
        ''' <returns>Value of type <see cref="T"/> initialized by <paramref name="value"/></returns>
        Public MustOverride Shadows Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As TOther) As T Implements ITypeConverter(Of TOther).ConvertFrom

        ''' <summary>Performs conversion from type <see cref="T"/> to type <see cref="TOther"/></summary>
        ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
        ''' <param name="value">Value to be converted</param>
        ''' <returns>Representation of <paramref name="value"/> in type <see cref="TOther"/></returns>
        Public MustOverride Shadows Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As T) As TOther Implements ITypeConverter(Of TOther).ConvertTo

        ''' <summary>Returns whether the given instance of <see cref="TOther"/> is valid for type <see cref="T"/></summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="value">Value to test validity</param>
        ''' <returns>true if the specified value is valid for this type <see cref="T"/>; otherwise, false.</returns>
        ''' <remarks>If not overriden in derived class thi method calls <see cref="ConvertFrom"/> and checks if it throws an exception or not.</remarks>
        Public Overridable Shadows Function IsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As TOther) As Boolean Implements ITypeConverterWithValidation(Of TOther).IsValid
            Try
                ConvertFrom(context, Threading.Thread.CurrentThread.CurrentCulture, value)
            Catch
                Return False
            End Try
            Return True
        End Function
#Region "Delegates"
        ''' <summary>Adapts methods <see cref="IsValid"/>, <see cref="ConvertFrom"/> and <see cref="ConvertTo"/> from <see cref="ITypeConverterWithValidation(Of TOther)"/>'s delegates into <see cref="TypeConverter(Of T)"/>'s delegates</summary>
        Private Adaptor As New DAdaptor(Of TOther)(AddressOf ConvertFrom, AddressOf ConvertTo, AddressOf IsValid)
        ''' <summary>Increases efficiency of base class method by returning delegate directly</summary>
        ''' <param name="sourceType">Type to return delegate for</param>
        ''' <returns>If <paramref name="sourceType"/> is <see cref="TOther"/> then returns <see cref="ConvertFrom"/> otherwise calls base class's method</returns>
        Protected Overrides Function ConverterFrom(ByVal sourceType As System.Type) As dConvertFrom
            If sourceType.Equals(GetType(TOther)) Then
                Return AddressOf Adaptor.ConvertFrom
            End If
            Return MyBase.ConverterFrom(sourceType)
        End Function
        ''' <summary>Increases efficiency of base class method by returning delegate directly</summary>
        ''' <param name="destinationType">Type to return delegate for</param>
        ''' <returns>If <paramref name="destinationType"/> is <see cref="TOther"/> then returns <see cref="ConvertTo"/> otherwise calls base class's method</returns>
        Protected Overrides Function ConverterTo(ByVal destinationType As System.Type) As dConvertTo
            If destinationType.Equals(GetType(TOther)) Then Return AddressOf Adaptor.ConvertTo
            Return MyBase.ConverterTo(destinationType)
        End Function
        ''' <summary>Increases efficiency of base class method by returning delegate directly</summary>
        ''' <param name="validateType">Type to return delegate for</param>
        ''' <returns>If <paramref name="validateType"/> is <see cref="TOther"/> then returns <see cref="IsValid"/> otherwise calls base class's method</returns>
        Protected Overrides Function Validator(ByVal validateType As System.Type) As dIsValid
            If validateType.Equals(GetType(TOther)) Then Return AddressOf Adaptor.IsValid
            Return MyBase.Validator(validateType)
        End Function
#End Region
        ''' <summary>Converts value of type <see cref="TOther"/> to <see cref="T"/></summary>
        ''' <param name="value">value to be converted</param>
        Public Shadows Function ConvertFrom(ByVal value As TOther) As T
            Return ConvertFrom(Nothing, Threading.Thread.CurrentThread.CurrentCulture, value)
        End Function
        ''' <summary>Converts value of type <see cref="T"/> to <see cref="TOther"/></summary>
        ''' <param name="value">value to be converted</param>
        Public Shadows Function ConvertTo(ByVal value As T) As TOther
            Return ConvertTo(Nothing, Threading.Thread.CurrentThread.CurrentCulture, value)
        End Function
        ''' <summary>Checks if value of type <see cref="TOther"/> can be converted to <see cref="T"/></summary>
        ''' <param name="value">value to be converted</param>
        Public Shadows Function IsValid(ByVal value As TOther) As Boolean
            Return IsValid(Nothing, value)
        End Function
    End Class

    ''' <summary>Simple <see cref="ComponentModel.TypeConverter"/> for <see cref="Byte()"/></summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(FileByteConverter), LastChMMDDYYYY:="06/19/2007")> _
    <MainTool(FirstVerMMDDYYYY:="06/19/2007")> _
    Public Class FileByteConverter
        Inherits TypeConverter(Of Byte())
        Implements ITypeConverterTo(Of String)
        ''' <summary>Performs conversion from <see cref="Byte()"/> to <see cref="String"/></summary>
        ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
        ''' <param name="value">Value to be converted</param>
        ''' <returns>An empty <see cref="String"/> if <paramref name="value"/> is null; otherwise <see cref="Array.Length"/> followed by the 'B' letter</returns>
        Public Shadows Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value() As Byte) As String Implements ITypeConverterTo(Of String).ConvertTo
            If value Is Nothing Then Return "" Else Return String.Format("{0}B", value.Length)
        End Function
    End Class
    ''' <summary>Provides base class for type-safe <see cref="ComponentModel.ExpandableObjectConverter"/> with support for interface based conversion as <see cref="TypeConverter(Of T)"/></summary>
    ''' <typeparam name="T">Main type conversion is providfed from and to</typeparam>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ExpandableObjectConverter(Of )), LastChMMDDYYYY:="06/19/2007")> _
    <MainTool(FirstVerMMDDYYYY:="06/19/2007")> _
    Public Class ExpandableObjectConverter(Of T) : Inherits TypeConverter(Of T)
        ''' <summary>Returns whether this object supports properties, using the specified context.</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <returns>True</returns>
        Public Overrides Function GetPropertiesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
            Return True
        End Function
        ''' <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="attributes">An array of type <see cref="System.Attribute"/> that is used as a filter.</param>
        ''' <param name="value">An <see cref="System.Object"/> that specifies the type of array for which to get properties.</param>
        ''' <returns>A <see cref="System.ComponentModel.PropertyDescriptorCollection"/> with the properties that are exposed for this data type, or null if there are no properties.</returns>
        Public Overrides Function GetProperties(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object, ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
            Return TypeDescriptor.GetProperties(GetType(T), attributes)
        End Function
    End Class
    ''' <summary>Provides base class fro type-safe <see cref="ComponentModel.ExpandableObjectConverter"/> with direct support for conversion to/from one type as <see cref="TypeConverter(Of T, TOther)"/> and interface-based type-safe converters implementation as <see cref="TypeConverter(Of T)"/></summary>
    ''' <typeparam name="T">Main type conversion is providfed from and to</typeparam>
    ''' <typeparam name="TOther">The other type to which and from which main type <paramref name="T"/> is converted</typeparam>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ExpandableObjectConverter(Of ,)), LastChMMDDYYYY:="06/19/2007")> _
    <MainTool(FirstVerMMDDYYYY:="06/19/2007")> _
    Public MustInherit Class ExpandableObjectConverter(Of T, TOther) : Inherits TypeConverter(Of T, TOther)
        ''' <summary>Returns whether this object supports properties, using the specified context.</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <returns>True</returns>
        Public Overrides Function GetPropertiesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
            Return True
        End Function
        ''' <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="attributes">An array of type <see cref="System.Attribute"/> that is used as a filter.</param>
        ''' <param name="value">An <see cref="System.Object"/> that specifies the type of array for which to get properties.</param>
        ''' <returns>A <see cref="System.ComponentModel.PropertyDescriptorCollection"/> with the properties that are exposed for this data type, or null if there are no properties.</returns>
        Public Overrides Function GetProperties(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object, ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
            Return TypeDescriptor.GetProperties(GetType(T), attributes)
        End Function
    End Class

    ''' <summary><see cref="TypeConverter"/> for <see cref="Byte()"/> as hexasring</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
  <Version(1, 0, GetType(HexaConverter), LastChMMDDYYYY:="06/19/2007")> _
  <MainTool(FirstVerMMDDYYYY:="06/19/2007")> _
  Public Class HexaConverter : Inherits TypeConverter(Of Byte(), String)
        ''' <summary>Performs conversion from <see cref="String"/> to <see cref="Byte()"/></summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
        ''' <param name="value">Value to be converted to <see cref="Byte()"/></param>
        ''' <returns><see cref="Byte()"/> initialized by <paramref name="value"/></returns>
        ''' <exception cref="ArgumentException">Length of <paramref name="value"/> is odd -or- <paramref name="value"/> contaions non-hexa character</exception>
        Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As String) As Byte()
            If value Is Nothing Then Return Nothing
            If value.Length Mod 2 <> 0 Then Throw New ArgumentException("String must consist of event number of hexadeimal numerals")
            Dim ret(value.Length \ 2 - 1) As Byte
            For i As Integer = 0 To value.Length - 1 Step 2
                Try
                    ret(i \ 2) = "&h" & value(i) & value(i + 1)
                Catch ex As Exception
                    Throw New ArgumentException(String.Format("Invalid character near ""{0}{1}""", value(i), value(i + 1)), ex)
                End Try
            Next i
            Return ret
        End Function
        ''' <summary>Performs conversion from <see cref="Byte()"/> to type <see cref="String"/></summary>
        ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
        ''' <param name="value">Value to be converted</param>
        ''' <returns>Representation of <paramref name="value"/> in <see cref="String"/></returns>
        Public Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value() As Byte) As String
            If value Is Nothing Then Return Nothing
            Dim ret As New System.Text.StringBuilder(value.Length * 2)
            For Each item As Byte In value
                ret.Append(item.ToString("X2"))
            Next item
            Return ret.ToString
        End Function
    End Class
    <CLSCompliant(False)> _
        <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
  <Version(1, 0, GetType(EnumConvertorWithAttributes(Of )), LastChMMDDYYYY:="06/19/2007")> _
  <MainTool(FirstVerMMDDYYYY:="06/19/2007")> _
    Public Class EnumConvertorWithAttributes(Of T As {IConvertible, Structure})
        Inherits TypeConverter(Of T, String)

        Public Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
            Dim Rest As RestrictAttribute = GetAttribute(Of RestrictAttribute)(GetType(T))
            Return Rest Is Nothing OrElse Rest.Restrict
        End Function

        Public Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
            Return True
        End Function

        Public Overrides Function IsValid(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As String) As Boolean
            Try
                ConvertFrom(context, Nothing, value)
                Return True
            Catch
                Return False
            End Try
        End Function

        Public Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
            Dim ret As New List(Of T)
            For Each cns As Reflection.FieldInfo In GetType(T).GetFields
                If cns.IsLiteral AndAlso cns.IsPublic Then
                    ret.Add(cns.GetValue(Nothing))
                End If
            Next cns
            Return New TypeConverter.StandardValuesCollection(ret)
        End Function

        Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As String) As T
            For Each cns As Reflection.FieldInfo In GetType(T).GetFields
                If cns.IsLiteral AndAlso cns.IsPublic Then
                    Dim DispName As DisplayNameAttribute = GetAttribute(Of DisplayNameAttribute)(cns)
                    If DispName IsNot Nothing Then
                        If DispName.DisplayName = value Then Return cns.GetValue(Nothing)
                    Else
                        If cns.Name = value Then Return cns.GetValue(Nothing)
                    End If
                End If
            Next cns
            Dim Rest As restrictattribute = GetAttribute(Of restrictattribute)(GetType(T))
            If Rest Is Nothing OrElse Rest.Restrict Then
                Throw New InvalidEnumArgumentException(String.Format("Cannot interpret value ""{0}"" as {1}", value, GetType(T).Name))
            Else
                Dim EType As Type = [Enum].GetUnderlyingType(GetType(T))
                Dim EValue As Object = Nothing
                If GetType(Byte).Equals(EType) Then : EValue = CByte(value)
                ElseIf GetType(SByte).Equals(EType) Then : EValue = CSByte(value)
                ElseIf GetType(Short).Equals(EType) Then : EValue = CShort(value)
                ElseIf GetType(UShort).Equals(EType) Then : EValue = CUShort(value)
                ElseIf GetType(Integer).Equals(EType) Then : EValue = CInt(value)
                ElseIf GetType(UInteger).Equals(EType) Then : EValue = CUInt(value)
                ElseIf GetType(Long).Equals(EType) Then : EValue = CLng(value)
                ElseIf GetType(ULong).Equals(EType) Then : EValue = CULng(value)
                End If
                Return [Enum].ToObject(GetType(T), EValue)
            End If
        End Function

        Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As T) As String
            Dim cns As FieldInfo = Nothing
            Try
                cns = GetConstant(value)
            Catch : End Try
            If cns IsNot Nothing Then
                Dim DispName As DisplayNameAttribute = GetAttribute(Of DisplayNameAttribute)(cns)
                If DispName IsNot Nothing Then
                    Return DispName.DisplayName
                Else
                    Return cns.Name
                End If
            Else
                Dim EType As Type = [Enum].GetUnderlyingType(GetType(T))
                Dim EValue As IFormattable = Nothing
                If GetType(Byte).Equals(EType) Then : EValue = CByte(CObj(value))
                ElseIf GetType(SByte).Equals(EType) Then : EValue = CSByte(CObj(value))
                ElseIf GetType(Short).Equals(EType) Then : EValue = CShort(CObj(value))
                ElseIf GetType(UShort).Equals(EType) Then : EValue = CUShort(CObj(value))
                ElseIf GetType(Integer).Equals(EType) Then : EValue = CInt(CObj(value))
                ElseIf GetType(UInteger).Equals(EType) Then : EValue = CUInt(CObj(value))
                ElseIf GetType(Long).Equals(EType) Then : EValue = CLng(CObj(value))
                ElseIf GetType(ULong).Equals(EType) Then : EValue = CULng(CObj(value))
                End If
                Return EValue.ToString("0", System.Globalization.CultureInfo.InvariantCulture)
            End If
        End Function
    End Class
End Namespace
#End If