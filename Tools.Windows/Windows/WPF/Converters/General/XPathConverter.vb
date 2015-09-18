Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data
Imports System.Xml.XPath
Imports System.Xml
Imports System.Xml.Linq
Imports System.Windows.Markup
Imports Tools.WindowsT.WPF.MarkupT

#If True
Namespace WindowsT.WPF.ConvertersT

    ''' <summary>Value converter which applies XPath query on binding result.</summary>
    ''' <remarks>This converter is designed as one way.
    ''' <para>When using namespaces in XPath query, use <see cref="XPathConverterExtension"/> instead of <see cref="XPathConverter"/>.</para></remarks>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public Class XPathConverter
        Implements IValueConverter

        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used. Returns result of XPath query callend on <paramref name="value"/>. Nulll when <paramref name="value"/> is null.</returns>
        ''' <param name="value">The value produced by the binding source. This must be one of supported types.</param>
        ''' <param name="targetType">The type of the binding target property. Ignored when null.</param>
        ''' <param name="parameter">The converter parameter to use. Must be string- XPath.</param>
        ''' <param name="culture">Ignored.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="parameter"/> is nothing.</exception>
        ''' <exception cref="TypeMismatchException"><paramref name="parameter"/> is not <see cref="String"/>.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is not one of supported types. Not thrown when <paramref name="parameter"/> is "." (optionally surrounded with whitespaces).</exception>
        ''' <exception cref="InvalidCastException">Cannot <see cref="DynamicCast">dynamically cast result of XPath query to <paramref name="targetType"/>.</see></exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Cast operators from result of XPath query to <paramref name="targetType"/> were found, but no one is most specific.</exception>
        ''' <exception cref="OverflowException">Build in conversion to numeric value (or <see cref="String"/> to <see cref="TimeSpan"/>) failed because result of XPath query cannot be represented in <paramref name="targetType"/> -or- Called cast operator have thrown this exception.</exception>
        ''' <exception cref="FormatException">Conversion of <see cref="String"/> to <see cref="TimeSpan"/> failed because string has bad format. -or- Operator being caled has thrown this exception.</exception>
        ''' <remarks>Supported types are:<list type="bulllet">
        ''' <item><see cref="XPathNavigator"/></item>
        ''' <item><see cref="XmlNode"/></item>
        ''' <item><see cref="XNode"/></item>
        ''' <item><see cref="IXPathNavigable"/></item>
        ''' </list></remarks>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            If parameter Is Nothing Then Throw New ArgumentNullException("parameter")
            If Not TypeOf parameter Is String Then Throw New TypeMismatchException("parameter", parameter, GetType(String))
            Dim xpath$ = parameter
            Dim ret As Object
            If xpath.Trim = "." Then
                ret = value
            ElseIf TypeOf value Is XPathNavigator Then
                ret = DirectCast(value, XPathNavigator).Evaluate(xpath, Namespaces)
            ElseIf TypeOf value Is XmlNode Then
                ret = DirectCast(value, XmlNode).SelectNodes(xpath, Namespaces)
            ElseIf TypeOf value Is XNode Then
                ret = DirectCast(value, XNode).XPathEvaluate(xpath, Namespaces)
            ElseIf TypeOf value Is IXPathNavigable Then
                ret = Convert(DirectCast(value, IXPathNavigable).CreateNavigator, Nothing, parameter, culture)
            Else
                Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_XPathNotSupportedType.f(value.GetType.Name))
            End If
            If targetType Is Nothing Then Return ret
            Return DynamicCast(ret, targetType)
        End Function


        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="targetType">The type to convert to.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">Ignored.</param>
        ''' <exception cref="NotSupportedException">
        ''' This exception is thrown almost always since this converter does not support converting back.
        ''' However under certain circumstances this exception is not thrown and backward conversion is attempted:
        ''' When <paramref name="value"/> is nothing, nothing is returned.
        ''' When <paramref name="parameter"/> is "." (optionally with whitespaces around) <paramref name="value"/> <see cref="DynamicCast">dynamically casted</see> to <paramref name="targetType"/> (if it is not null) is retuned.
        ''' </exception>
        ''' <exception cref="InvalidCastException"><paramref name="parameter"/> is <see cref="String"/> and when trimmed it's ".", <paramref name="value"/> is not null and cannot <see cref="DynamicCast">dynamically cast</see> <paramref name="value"/> to <paramref name="targetType"/>.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException"><paramref name="parameter"/> is <see cref="String"/> and when trimmed it's ".", <paramref name="value"/> is not null and cast operators from <paramref name="value"/> to <paramref name="targetType"/> were found, but no one is most specific.</exception>
        ''' <exception cref="OverflowException"><paramref name="parameter"/> is <see cref="String"/> and when trimmed it's ".", <paramref name="value"/> is not null and build in conversion to numeric value (or <see cref="String"/> to <see cref="TimeSpan"/>) failed because result of <paramref name="value"/> cannot be represented in <paramref name="targetType"/> or called cast operator have thrown this exception.</exception>
        ''' <exception cref="FormatException"><paramref name="parameter"/> is <see cref="String"/> and when trimmed it's ".", <paramref name="value"/> is not null and conversion of <see cref="String"/> to <see cref="TimeSpan"/> failed because string has bad format or operator being caled has thrown this exception.</exception>
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If value Is Nothing Then Return Nothing
            If TypeOf parameter Is String AndAlso DirectCast(parameter, String).Trim = "." Then Return If(targetType Is Nothing, value, DynamicCast(value, targetType))
            Throw New NotSupportedException("{0} cannot convert back".f(GetType(XPathConverter).Name))
        End Function

        Private _namespaces As New XmlNamespaceMappingCollection
        ''' <summary>Gets collection of XML namespace prefix-URI mappings to be used with XPath query</summary>
        Public ReadOnly Property Namespaces As XmlNamespaceMappingCollection
            Get
                Return _namespaces
            End Get
        End Property
    End Class

    ''' <summary>Markup extension provides an instnce of the <see cref="XPathConverter"/> value converter. Resulting converter is aware of all XML namespace registrations in context.</summary>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    <MarkupExtensionReturnType(GetType(XPathConverter))>
    Public Class XPathConverterExtension
        Inherits MarkupExtensionBase
        ''' <summary>Returns an object that is set as the value of the target property for this markup extension. This implementation always returns a new instance of the <see cref="XPathConverter"/> class.</summary>
        ''' <returns>The object value to set on the property where the extension is applied. </returns>
        ''' <param name="serviceProvider">Object that can provide services for the markup extension. This implementation never passes value null here. This class utilizes <see cref="System.Xaml.IXamlNamespaceResolver"/> service when available.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="serviceProvider"/> is null. This can happen only when you are calling this method from derived class.</exception>
        ''' <remarks>In case <paramref name="serviceProvider"/> provides <see cref="System.Xaml.IXamlNamespaceResolver"/> service, it's used to populate <see cref="XPathConverter.Namespaces"/> collection from current context.</remarks>
        Protected Overloads Overrides Function ProvideValue(ByVal serviceProvider As MarkupT.XamlServiceProvider) As Object
            If serviceProvider Is Nothing Then Throw New ArgumentNullException("serviceProvider")
            Dim nsReolver = serviceProvider.XamlNamespaceResolver
            Dim ret As New XPathConverter
            If nsReolver IsNot Nothing Then
                For Each ns In nsReolver.GetNamespacePrefixes
                    ret.Namespaces.AddNamespace(ns.Prefix, ns.Namespace)
                Next
            End If
            Return ret
        End Function
    End Class

End Namespace
#End If