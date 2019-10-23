Imports System.Windows, Tools.ExtensionsT, System.Linq
Imports System.Windows.Markup
Imports System.Xaml
Imports System.Windows.Data
Imports System.ComponentModel.Design.Serialization


Namespace WindowsT.WPF.MarkupT

    ''' <summary>This markup extension provides instance of <see cref="XmlNamespaceMappingCollection"/> object containing all or some of namespace prefixes registered at place where it is used.</summary>
    ''' <remarks>Because of bug in Visual Studio (see <a href="https://connect.microsoft.com/VisualStudio/feedback/details/614411">IXamlTypeResolver is not provided in design mode</a>) this markup extension may return null in design mode.</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    <ContentPropertyAttribute("Prefixes"), MarkupExtensionReturnType(GetType(XmlNamespaceMappingCollection))> _
    <TypeConverter(GetType(NamespacesExtension.NamespacesExtensionConverter))>
    Public Class NamespacesExtension : Inherits MarkupExtensionBase
        ''' <summary>CTor - creates a new instance of the <see cref="NamespacesExtension"/> class without prefix filter</summary>
        ''' <remarks>Prefix filter can be set later using the <see cref="Prefixes"/> property</remarks>
        Public Sub New()
        End Sub
        ''' <summary>CTor - initializes a new instance of the <see cref="NamespacesExtension"/> class with prefix filter (as string)</summary>
        ''' <param name="prefixes">Prefix filter - whitespace-separated list of XML namespace prefixes registered in current XAML file (context)</param>
        Public Sub New(ByVal prefixes$)
            Me.Prefixes = prefixes
        End Sub

        ''' <summary>Returns an object that is set as the value of the target property for this markup extension (<see cref="XmlNamespaceMappingCollection"/>). </summary>
        ''' <returns>The object value to set on the property where the extension is applied. This implementation always returns <see cref="XmlNamespaceMappingCollection"/>.
        ''' <note>Because of bug in Visual Studio (see <a href="https://connect.microsoft.com/VisualStudio/feedback/details/614411">IXamlTypeResolver is not provided in design mode</a>) null is returned when <paramref name="serviceProvider"/> does not provide <see cref="IXamlNamespaceResolver"/> and <paramref name="serviceProvider"/>.<see cref="XamlServiceProvider.InnerProvider">InnerProvider</see> is <see cref="T:Microsoft.Expression.DesignModel.Core.InstanceBuilderOperations+InstanceBuilderServiceProvider"/>.</note>
        ''' </returns>
        ''' <param name="serviceProvider">Object that can provide services for the markup extension. This markup extension requires <see cref="IXamlTypeResolver"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="serviceProvider"/> is null. This never happens unless you call this method manually from derived class.</exception>
        ''' <exception cref="InvalidOperationException"><paramref name="serviceProvider"/> does not provide <see cref="IXamlTypeResolver"/> service.
        ''' <note>This exception is not thrown when <paramref name="serviceProvider"/>.<see cref="XamlServiceProvider.InnerProvider">InnerProvider</see> is <see cref="T:Microsoft.Expression.DesignModel.Core.InstanceBuilderOperations+InstanceBuilderServiceProvider"/>.</note></exception>
        Protected Overrides Function ProvideValue(ByVal serviceProvider As XamlServiceProvider) As Object
            If serviceProvider Is Nothing Then Throw New ArgumentNullException("serviceProvider")
            Dim nsResolver = serviceProvider.XamlNamespaceResolver
            If nsResolver Is Nothing Then
                If serviceProvider.InnerProvider.GetType.FullName = "Microsoft.Expression.DesignModel.Core.InstanceBuilderOperations+InstanceBuilderServiceProvider" Then Return Nothing
                Throw New InvalidOperationException(ResourcesT.Exceptions.The0MarkupExtensionRequiresAn1ServiceProvider.f(GetType(NamespacesExtension).Name, GetType(IXamlNamespaceResolver).Name))
            End If
            Dim ret As New XmlNamespaceMappingCollection
            Dim prefixes = If(Not Me.Prefixes.IsNullOrWhiteSpace, Me.Prefixes.WhiteSpaceSplit, Nothing)
            For Each ns In nsResolver.GetNamespacePrefixes()
                If prefixes IsNot Nothing AndAlso Not prefixes.Contains(ns.Prefix) Then Continue For
                ret.AddNamespace(ns.Prefix, ns.Namespace)
            Next
            Return ret
        End Function

        ''' <summary>Gets or sets prefixes (whitespace-separated list) of XML namespaces to be included in <see cref="XmlNamespaceMappingCollection"/> returned by <see cref="ProvideValue"/>.</summary>
        ''' <value>Whitespace-separated list of namespace prefixes which are valid in place where the extension is used. List of valid namespace prefixes is obtained via <see cref="IXamlNamespaceResolver"/>. Unknown prefixes are ignored.</value>
        ''' <returns>Current list of XML namespace prefixes to include in <see cref="XmlNamespaceMappingCollection"/>.</returns>
        ''' <remarks><see cref="ProvideValue"/> returns available namespaces when this property is null, an empty string or contains only whitespaces.</remarks>
        <ConstructorArgument("prefixes")>
        Public Property Prefixes As String

        ''' <summary>Implements <see cref="TypeConverter"/> for <see cref="NamespacesExtension"/></summary>
        Friend Class NamespacesExtensionConverter
            Inherits TypeConverter
            ''' <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
            ''' <returns>true if this converter can perform the conversion; otherwise, false.
            ''' This implementation returns true when <paramref name="destinationType"/> is <see cref="InstanceDescriptor"/>, otherwise base class method <see cref="TypeConverter.CanConvertTo"/> is called.</returns>
            ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
            ''' <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
            Public Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
                Return destinationType = GetType(InstanceDescriptor) OrElse MyBase.CanConvertTo(context, destinationType)
            End Function
            ''' <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
            ''' <returns>
            ''' An <see cref="T:System.Object" /> that represents the converted value.
            ''' This implementation converts <see cref="NamespacesExtension"/> to <see cref="InstanceDescriptor"/>.
            ''' If <paramref name="value"/> is null, returns null.
            ''' If <paramref name="destinationType"/> is not <see cref="InstanceDescriptor"/> then base class method <see cref="TypeConverter.ConvertTo"/> is called.
            ''' </returns>
            ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
            ''' <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
            ''' <param name="value">The <see cref="T:System.Object" /> to convert. </param>
            ''' <param name="destinationType">The <see cref="T:System.Type" /> to convert the  <paramref name="value" /> parameter to. </param>
            ''' <exception cref="ArgumentNullException">The  <paramref name="destinationType" /> parameter is null. </exception>
            ''' <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
            ''' <exception cref="TypeMismatchException"><paramref name="value"/> is neither null nor <see cref="NamespacesExtension"/>.</exception>
            Public Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
                If destinationType <> GetType(InstanceDescriptor) Then
                    Return MyBase.ConvertTo(context, culture, value, destinationType)
                End If
                If value Is Nothing Then Return Nothing
                Dim extension = TryCast(value, NamespacesExtension)
                If (extension Is Nothing) Then Throw New TypeMismatchException(value, "value", GetType(NamespacesExtension))
                Return New InstanceDescriptor(GetType(NamespacesExtension).GetConstructor({GetType(String)}), {extension.Prefixes})
            End Function
        End Class
    End Class

End Namespace
