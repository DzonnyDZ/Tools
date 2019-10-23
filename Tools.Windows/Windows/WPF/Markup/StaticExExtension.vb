Imports System.Windows.Markup
Imports System.ComponentModel, System.ComponentModel.Design.Serialization
Imports System.Reflection, Tools.ExtensionsT

Namespace WindowsT.WPF.MarkupT
    ''' <summary>Implements markup extension to access static properties and fields and their members</summary>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    ''' <version version="1.5.4">DynamicCast now also considers type converters</version>
    <MarkupExtensionReturnType(GetType(Object))>
    <TypeConverter(GetType(StaticExExtension.StaticExTypeConverter))>
    Public Class StaticExExtension
        Inherits MarkupExtension
        ''' <summary>Contains value of the <see cref="Member"/> property</summary>
        Private _Member As String
        ''' <summary>Default constructor</summary>
        ''' <remarks>This constructor is intended to be used by XAML only. Use parametrized constructor overload instead.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Sub New()
        End Sub

        ''' <summary>CTor with member</summary>
        ''' <param name="Member">Name and path of member this extension provides value of. See <see cref="Member"/> for more information.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null.</exception>
        Public Sub New(ByVal member As String)
            Me.New()
            If member Is Nothing Then Throw New ArgumentNullException("Member")
            Me.Member = member
        End Sub

        ''' <summary>CTor with member and <see cref="Reflection.BindingFlags"/></summary>
        ''' <param name="Member">Name and path of member this extension provides value of. See <see cref="Member"/> for more information.</param>
        ''' <param name="bindingFlags">Binding flags used when looking for field or property</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null.</exception>
        Public Sub New(ByVal member As String, ByVal bindingFlags As BindingFlags)
            Me.New(member)
            Me.BindingFlags = bindingFlags
        End Sub
        ''' <summary>Gets or sets name and path of member this extension provides value of</summary>
        ''' <value>Name and path of member this extension provides value of</value>
        ''' <remarks>
        ''' This property shall be set to string containing several (at least 2) dot(.)-separated substring. (No leading and terminating dots!)
        ''' First part is interpreted as name of type and is resolved via <see cref="IXamlTypeResolver.Resolve"/> (so it can contain XML namespace prefix).
        ''' Second part must me name of public static property or field of type specified in first part.
        ''' Third and all subsequent parts must be names of public instance fields of type returned by field or property from preceding part (type descriptors are not utilized).
        ''' If any member is not found or (with exception of last) returns null, an exception is thrown.
        ''' </remarks>
        <ConstructorArgument("Member")>
        Public Property Member() As String
            Get
                Return _Member
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                _Member = value
            End Set
        End Property
        Private _bindingFlags As BindingFlags = Reflection.BindingFlags.Public
        ''' <summary>Gets or sets binding flags used when looking for field or property</summary>
        ''' <value>A <see cref="Reflection.BindingFlags"/> value indicating which members are considered when looking for field or property to get value of. Default value is <see cref="BindingFlags.[Public]"/>.</value>
        ''' <remarks>Use only binding flags related to member visibility. <see cref="BindingFlags.Instance"/> and <see cref="BindingFlags.[Static]"/> are ignored by setter and used as needed depending on context</remarks>
        <DefaultValue(BindingFlags.Public)>
        Public Property BindingFlags As BindingFlags
            Get
                Return _bindingFlags
            End Get
            Set(ByVal value As BindingFlags)
                _bindingFlags = value And Not Reflection.BindingFlags.Static And Not BindingFlags.Instance
            End Set
        End Property

        ''' <summary>Returns an object that is set as the value of the target property for this markup extension.</summary>
        ''' <returns>The object value to set on the property where the extension is applied.
        ''' If <paramref name="serviceProvider"/> provides <see cref="IProvideValueTarget"/> service and its <see cref="IProvideValueTarget.TargetProperty"/> is 
        '''     <see cref="PropertyInfo"/> with non-null <see cref="PropertyInfo.PropertyType"/> or <see cref="System.Windows.DependencyProperty"/> with non-null <see cref="System.Windows.DependencyProperty.PropertyType"/> 
        '''     <see cref="Tools.TypeTools.DynamicCast">dynamic cast</see> or result of properties/fields evaluation is attempted.</returns>
        ''' <param name="serviceProvider">Object that can provide services for the markup extension.</param>
        ''' <exception cref="InvalidOperationException"><see cref="Member"/> is null, an empty string or contains fewer than 2 dot-separated parts.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="serviceProvider"/> is null.</exception>
        ''' <exception cref="ArgumentException"><paramref name="serviceProvider"/> does not provide service of type <see cref="IXamlTypeResolver"/>.</exception>
        ''' <exception cref="NullReferenceException">Value returned by any field or property but the last is null.</exception>
        ''' <exception cref="MissingMemberException">Neither public static property nor public static field of type from 1st segment and with name from 2nd segment was not found. -or- Neither public instance property nor public instance field with name specified in 3rd or any subsequent segment was not found on type returned by field or property of preceding segment.</exception>
        ''' <exception cref="AmbiguousMatchException">More that one public static (2nd level) or public instance (3rd+) level property with same name found. -or- <see cref="Tools.TypeTools.DynamicCast">Dynamic cast</see> wa attempted (see return value documentation for conditions when it is attempted): Cast operators were found, but no one is most specific.</exception>
        ''' <exception cref="MemberAccessException">Property being accessed has no getter. (<see cref="PropertyInfo.GetValue"/> throws an <see cref="ArgumentException"/>.) -or- <see cref="FieldInfo.GetValue"/> throws an <see cref="ArgumentException"/>.</exception> 
        ''' <exception cref="TargetParameterCountException">Property being accessed is indexed</exception>
        ''' <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. I.e. property is public but getter is not public.</exception>
        ''' <exception cref="TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="System.Exception.InnerException"/> property indicates the reason for the error.</exception>
        ''' <exception cref="NotSupportedException">A field being accessed is marked literal, but the field does not have one of the accepted literal types.</exception>
        ''' <exception cref="FieldAccessException">The caller does not have permission to access a field.</exception>
        ''' <exception cref="InvalidCastException"><see cref="Tools.TypeTools.DynamicCast">Dynamic cast</see> was attempted (see return value documentation for conditions when it is attempted): No casting method from type of obj to Type was found -or- build in conversion from System.String to numeric type failed.</exception>
        ''' <exception cref="FormatException"><see cref="Tools.TypeTools.DynamicCast">Dynamic cast</see> was attempted (see return value documentation for conditions when it is attempted): Conversion of <see cref="System.String"/> to <see cref="System.TimeSpan"/> failed because string has bad format. -or- Operator being caled has thrown this exception.</exception>
        ''' <version version="1.5.4">DynamicCast now also considers type converters</version>
        Public Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            If Member Is Nothing Then Throw New InvalidOperationException(Resources.ex_IsNull.f("Member"))
            If serviceProvider Is Nothing Then Throw New ArgumentNullException("serviceProvider")
            Dim ValueTarget As IProvideValueTarget = serviceProvider.GetService(GetType(IProvideValueTarget))
            Dim Resolver As IXamlTypeResolver = serviceProvider.GetService(GetType(IXamlTypeResolver))
            If Resolver Is Nothing Then Return Me

            'If Resolver Is Nothing Then Throw New ArgumentException(Resources.ex_ServiceProviderDoesntProvide.f(GetType(IXamlTypeResolver).Name))
            Dim parts = Member.Split(".")
            If parts Is Nothing OrElse parts.Length = 0 Then Throw New InvalidOperationException(Resources.ex_CannotBeAnEmptyString.f("Member"))
            Dim type = Resolver.Resolve(parts(0))
            If parts.Length = 1 Then Throw New InvalidOperationException(Resources.ex_DoesNotContainMemberNamePart.f("Member"))
            Dim CurrProp As Reflection.MemberInfo
            CurrProp = type.GetProperty(parts(1), BindingFlags Or Reflection.BindingFlags.Static)
            If CurrProp Is Nothing Then CurrProp = type.GetField(parts(1), BindingFlags Or Reflection.BindingFlags.Static)
            If CurrProp Is Nothing Then Throw New MissingMemberException(type.FullName, parts(1))
            Dim CurrVal As Object = Nothing
            If TypeOf CurrProp Is PropertyInfo Then
                Try
                    CurrVal = DirectCast(CurrProp, PropertyInfo).GetValue(Nothing, Nothing)
                Catch ex As ArgumentException
                    Throw New MemberAccessException(ex.Message, ex)
                End Try
            ElseIf TypeOf CurrProp Is FieldInfo Then
                Try
                    CurrVal = DirectCast(CurrProp, FieldInfo).GetValue(Nothing)
                Catch ex As ArgumentException
                    Throw New MemberAccessException(ex.Message, ex)
                End Try
            End If
            For i As Integer = 2 To parts.Length - 1
                If CurrVal Is Nothing Then Throw New NullReferenceException(Resources.ex_MemberHasValueNull.f(parts(i - 1)))
                CurrProp = CurrVal.GetType.GetProperty(parts(i), BindingFlags Or BindingFlags.Instance)
                If CurrProp Is Nothing Then CurrProp = CurrVal.GetType.GetField(parts(i), BindingFlags Or BindingFlags.Instance)
                If CurrProp Is Nothing Then Throw New MissingMemberException(CurrVal.GetType.FullName, parts(i))
                If TypeOf CurrProp Is PropertyInfo Then
                    CurrVal = DirectCast(CurrProp, PropertyInfo).GetValue(CurrVal, Nothing)
                ElseIf TypeOf CurrProp Is FieldInfo Then
                    CurrVal = DirectCast(CurrProp, FieldInfo).GetValue(CurrVal)
                End If
            Next
            If ValueTarget IsNot Nothing AndAlso ValueTarget.TargetProperty IsNot Nothing AndAlso TypeOf ValueTarget.TargetProperty Is Reflection.PropertyInfo AndAlso DirectCast(ValueTarget.TargetProperty, Reflection.PropertyInfo).PropertyType IsNot Nothing Then
                Return Tools.TypeTools.DynamicCast(CurrVal, DirectCast(ValueTarget.TargetProperty, Reflection.PropertyInfo).PropertyType, True)
            ElseIf ValueTarget IsNot Nothing AndAlso ValueTarget.TargetProperty IsNot Nothing AndAlso TypeOf ValueTarget.TargetProperty Is System.Windows.DependencyProperty AndAlso DirectCast(ValueTarget.TargetProperty, System.Windows.DependencyProperty).PropertyType IsNot Nothing Then
                Return Tools.TypeTools.DynamicCast(CurrVal, DirectCast(ValueTarget.TargetProperty, System.Windows.DependencyProperty).PropertyType, True)
            End If
            Return CurrVal
        End Function

        ''' <summary>Converter for type <see cref="MarkupExtension"/> and <see cref="InstanceDescriptor"/></summary>
        Friend Class StaticExTypeConverter
            Inherits TypeConverter
            ''' <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
            ''' <returns>true if this converter can perform the conversion; otherwise, false. This implementation returns true if <paramref name="destinationType"/> is <see cref="InstanceDescriptor"/>; otherwise calls <see cref="TypeConverter.CanConvertTo"/>.</returns>
            ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
            ''' <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
            Public Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
                Return destinationType.Equals(GetType(InstanceDescriptor)) OrElse MyBase.CanConvertTo(context, destinationType)
            End Function
            ''' <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
            ''' <returns>An <see cref="T:System.Object" /> that represents the converted value. If <paramref name="destinationType"/> is <see cref="InstanceDescriptor"/> and <paramref name="value"/> is <see cref="StaticExExtension"/> this implementation returns <see cref="InstanceDescriptor"/>.</returns>
            ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
            ''' <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">The <see cref="T:System.Object" /> to convert.</param>
            ''' <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.</param>
            ''' <exception cref="ArgumentNullException">The <paramref name="destinationType" /> parameter is null.</exception>
            ''' <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
            ''' <remarks>This implementation performs conversion from <see cref="StaticExExtension"/> to <see cref="InstanceDescriptor"/>; otherwise calls <see cref="TypeConverter.ConvertTo"/>.</remarks>
            Public Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
                If destinationType.Equals(GetType(InstanceCreationEditor)) AndAlso TypeOf value Is StaticExExtension Then
                    Dim id = New InstanceDescriptor(Me.GetType.GetConstructor(BindingFlags.Public Or BindingFlags.Instance, Nothing,
                                                                              New Type() {GetType(String), GetType(BindingFlags)}, New ParameterModifier() {}),
                                                    New Object() {DirectCast(value, StaticExExtension).Member, DirectCast(value, StaticExExtension).BindingFlags})
                    Return id
                Else
                    Return MyBase.ConvertTo(context, culture, value, destinationType)
                End If
            End Function
        End Class
    End Class
End Namespace