Imports System.Windows.Markup

Namespace WindowsT.WPF.MarkupT

    ''' <summary>Base class for simple automatic <see cref="ValueSerializer">ValueSerializers</see> based on <see cref="TypeConverter"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public MustInherit Class TypeConverterBasedValueSerializerBase
        Inherits ValueSerializer
        ''' <summary>When overriden in derived class gets a <see cref="TypeConverter"/> to perform type conversion</summary>
        ''' <returns>A <see cref="TypeConverter"/>, null if conversion is not supported in current situation</returns>
        ''' <remarks>The converter should be capable of converting objects of type this serializer is used for from and to <see cref="String"/></remarks>
        Public MustOverride ReadOnly Property Converter As TypeConverter

        ''' <summary>When overriden in derived class gets a <see cref="TypeConverter"/> for specific object to be serialized</summary>
        ''' <param name="component">A component to serialize</param>
        ''' <returns>
        ''' An instance of <see cref="TypeConverter"/> to use to convert <paramref name="component"/> to <see cref="String"/>.
        ''' This implementation always returns same value as the <see cref="Converter"/> property.
        ''' </returns>
        Public Overridable Function GetConverter(component As Object) As TypeConverter
            Return Converter
        End Function

        ''' <summary>Determines whether the specified <see cref="T:System.String" /> can be converted to an instance of the type that the implementation of <see cref="T:System.Windows.Markup.ValueSerializer" /> supports.</summary>
        ''' <param name="value">The string to evaluate for conversion. Ignored by this implementation.</param>
        ''' <param name="context">Context information that is used for conversion. If it implements <see cref="ITypeDescriptorContext"/> it's passed to <see cref="TypeConverter.CanConvertFrom"/>, ignored otherwise.</param>
        ''' <returns>true if the value can be converted; otherwise, false.</returns>
        Public Overrides Function CanConvertFromString(ByVal value As String, ByVal context As IValueSerializerContext) As Boolean
            Return Converter IsNot Nothing AndAlso Converter.CanConvertFrom(TryCast(context, ITypeDescriptorContext), GetType(String))
        End Function

        ''' <summary>Determines whether the specified object can be converted into a <see cref="T:System.String" />.</summary>
        ''' <param name="value">The object to evaluate for conversion.</param>
        ''' <param name="context">Context information that is used for conversion. If it implements <see cref="ITypeDescriptorContext"/> it's passed to <see cref="TypeConverter.CanConvertTo"/>, ignored otherwise.</param>
        ''' <returns>true if the <paramref name="value" /> can be converted into a <see cref="T:System.String" />; otherwise, false.</returns>
        ''' <remarks>Returns true if <see cref="TypeConverter"/> obtained using <see cref="GetConverter"/> indicates that it can convert value to <see cref="String"/>.</remarks>
        Public Overrides Function CanConvertToString(ByVal value As Object, ByVal context As IValueSerializerContext) As Boolean
            Return Converter IsNot Nothing AndAlso GetConverter(value).CanConvertTo(TryCast(context, ITypeDescriptorContext), GetType(String))
        End Function

        ''' <summary>Converts a <see cref="T:System.String" /> to an instance of the type that the implementation of <see cref="T:System.Windows.Markup.ValueSerializer" /> supports.</summary>
        ''' <param name="value">The string to convert.</param>
        ''' <param name="context">Context information that is used for conversion. If it implements <see cref="ITypeDescriptorContext"/> it's passed to <see cref="TypeConverter.ConvertFromString"/>, ignored otherwise.</param>
        ''' <returns>A new instance of the type <typeparamref name="T"/> based on the supplied <paramref name="value" />.</returns>
        ''' <exception cref="T:System.NotSupportedException"><paramref name="value" /> cannot be converted.</exception>
        Public Overrides Function ConvertFromString(ByVal value As String, ByVal context As IValueSerializerContext) As Object
            If Converter IsNot Nothing Then Return Converter.ConvertFromString(TryCast(context, ITypeDescriptorContext), value)
            Return MyBase.ConvertFromString(value, context)
        End Function

        ''' <summary>Converts the specified object to a <see cref="T:System.String" />.</summary>
        ''' <param name="value">The object to convert into a string.</param>
        ''' <param name="context">Context information that is used for conversion. If it implements <see cref="ITypeDescriptorContext"/> it's passed to <see cref="TypeConverter.ConvertToInvariantString"/>, ignored otherwise.</param>
        ''' <returns>A string representation of the specified object.</returns>
        ''' <exception cref="T:System.NotSupportedException"><paramref name="value" /> cannot be converted.</exception>
        Public Overrides Function ConvertToString(ByVal value As Object, ByVal context As IValueSerializerContext) As String
            If Converter IsNot Nothing Then Return GetConverter(value).ConvertToInvariantString(TryCast(context, ITypeDescriptorContext), value)
            Return MyBase.ConvertToString(value, context)
        End Function
    End Class

    ''' <summary>Implements <see cref="ValueSerializer"/> based on default <see cref="TypeConverter"/> of type <typeparamref name="T"/></summary>
    ''' <typeparam name="T">Type of object to be (de)serialized</typeparam>
    ''' <remarks>This implementation uses <see cref="TypeConverter"/> obtained using <see cref="TypeDescriptor.GetConverter"/></remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class DefaultTypeConverterBasedValueSerializer(Of T)
        Inherits TypeConverterBasedValueSerializerBase

        ''' <summary>Gets a <see cref="TypeConverter"/> to perform type conversion</summary>
        ''' <returns>A default <see cref="TypeConverter"/> of type <typeparamref name="T"/> obtained using <see cref="TypeDescriptor.GetConverter"/></returns>
        Public Overrides ReadOnly Property Converter As System.ComponentModel.TypeConverter
            Get
                Return TypeDescriptor.GetConverter(GetType(T))
            End Get
        End Property

        ''' <summary>Gets a <see cref="TypeConverter"/> for specific object to be serialized</summary>
        ''' <param name="component">A component to serialize</param>
        ''' <returns>A default <see cref="TypeConverter"/> ofr <paramref name="component"/> obtained using <see cref="TypeDescriptor.GetConverter"/>. Returns <see cref="Converter"/> if <paramref name="component"/> is null.</returns>
        Public Overrides Function GetConverter(component As Object) As System.ComponentModel.TypeConverter
            If component Is Nothing Then Return Converter
            Return TypeDescriptor.GetConverter(component)
        End Function

        ''' <summary>Determines whether the specified object can be converted into a <see cref="T:System.String" />.</summary>
        ''' <param name="value">The object to evaluate for conversion.</param>
        ''' <param name="context">Context information that is used for conversion. If it implements <see cref="ITypeDescriptorContext"/> it's passed to <see cref="TypeConverter.CanConvertTo"/>, ignored otherwise.</param>
        ''' <returns>true if the <paramref name="value" /> can be converted into a <see cref="T:System.String" />; otherwise, false.</returns>
        ''' <remarks>Returns true if <see cref="TypeConverter"/> obtained using <see cref="GetConverter"/> indicates that it can convert value to <see cref="String"/>.</remarks>
        Public Overrides Function CanConvertToString(value As Object, context As System.Windows.Markup.IValueSerializerContext) As Boolean
            Return TypeOf value Is T AndAlso MyBase.CanConvertToString(value, context)
        End Function
    End Class

    ''' <summary>Implements <see cref="ValueSerializer"/> based on specific <see cref="TypeConverter"/></summary>
    ''' <typeparam name="TConverter">Type of type converter. It must have public default CTor. It should support conversion from and to <see cref="String"/>.</typeparam>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class SpecificTypeConverterBasedValueSerializer(Of TConverter As {New, TypeConverter})
        Inherits TypeConverterBasedValueSerializerBase

        Private _converter As TConverter = New TConverter

        ''' <summary>Gets a <see cref="TypeConverter"/> to perform type conversion</summary>
        ''' <returns>An instance of type <typeparamref name="TConverter"/></returns>
        Public Overrides ReadOnly Property Converter As System.ComponentModel.TypeConverter
            Get
                Return _converter
            End Get
        End Property
    End Class
End Namespace