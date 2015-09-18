Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization, System.Globalization.CultureInfo
Imports System.Xml.XPath
Imports System.Xml.Linq
Imports System.Xml, Tools.LinqT, System.Linq

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter which gets value and uses it as parameter of XPath query. Then returns result of that query.</summary>
    ''' <remarks>This converter is one-way.</remarks>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public Class XmlXPathConverter
        Implements IValueConverter

        Private _source As Object
        ''' <summary>Gets or sets XML object to execute XPath query againts</summary>
        ''' <remarks>Supported object types are <see cref="IXPathNavigable"/> and <see cref="XNode"/>.</remarks>
        ''' <exception cref="TypeMismatchException">Value is being set to value thet is neither null nor <see cref="IXPathNavigable"/> nor <see cref="XNode"/>.</exception>
        Public Property Source As Object
            Get
                Return _source
            End Get
            Set(ByVal value As Object)
                If value IsNot Nothing AndAlso Not TypeOf value Is IXPathNavigable AndAlso Not TypeOf value Is XNode Then
                    Throw New TypeMismatchException(value, "value", GetType(IXPathNavigable), GetType(XNode))
                End If
                _source = value
            End Set
        End Property
        ''' <summary>Gets or sets <see cref="XmlNamespaceManager"/> to be used for resolving namespaces</summary>
        Public Property XmlNamespaceManager As XmlNamespaceManager

        ''' <summary>Converts a value. (Executes XPtaht query agains <see cref="Source"/> with <paramref name="value"/> as parameter.</summary>
        ''' <returns>
        ''' A converted value. If the method returns null, the valid null value is used. Returns value which's type depends on type of XPath result.
        ''' <para>
        ''' If XPtah results in empty enumeration, returns null.
        ''' If XPtah results in enumeration with juts one element, treates the element as result and processes it as described below.
        ''' </para>
        ''' <para>
        ''' If result is one of following objects returns value of that object rathter than object itself:
        ''' <see cref="XmlAttribute"/>, <see cref="XAttribute"/>, <see cref="XmlText"/>, <see cref="XText"/>, <see cref="XmlCDataSection"/>, <see cref="XCData"/>, <see cref="XmlComment"/>, <see cref="XComment"/>.
        ''' </para>
        ''' </returns>
        ''' <param name="value">The value produced by the binding source. Used as XPath parameter</param>
        ''' <param name="targetType">The type of the binding target property. Ignored.</param>
        ''' <param name="parameter">The converter parameter to use. Must be <see cref="String"/> - represents template of XPath query. Placeholder {0} is replaces with string representation of <paramref name="value"/> using <see cref="System.String.Format"/>.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="parameter"/> is null.</exception>
        ''' <exception cref="TypeMismatchException"><paramref name="parameter"/> is not string.</exception>
        ''' <exception cref="XPathException"><paramref name="parameter"/> contains namespace prefix which is not resolved by <see cref="XmlNamespaceManager"/>.</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If parameter Is Nothing Then Throw New ArgumentNullException("parameter")
            If Not TypeOf parameter Is String Then Throw New TypeMismatchException(parameter, "parameter", GetType(String))
            Dim xpath = String.Format(culture, CStr(parameter), value)
            If Source Is Nothing Then Throw New InvalidOperationException(ResourcesT.Exceptions.PropertyMustBeSet.f("Source"))
            Dim ret As Object
            If TypeOf Source Is XmlNode Then
                ret = DirectCast(Source, XmlNode).SelectNodes(xpath, XmlNamespaceManager)
            ElseIf TypeOf Source Is IXPathNavigable Then
                Dim navigator = DirectCast(Source, IXPathNavigable).CreateNavigator
                ret = navigator.Evaluate(xpath, XmlNamespaceManager)
            ElseIf TypeOf Source Is XNode Then
                ret = DirectCast(Source, XNode).XPathEvaluate(xpath, XmlNamespaceManager)
            Else 'Cannot happen
                ret = Nothing
            End If
            If TypeOf ret Is IEnumerable AndAlso Not TypeOf ret Is String Then
                Dim rienum As IEnumerable = ret
                If rienum.IsEmpty Then
                    Return Nothing
                ElseIf rienum.Single Then
                    ret = rienum.OfType(Of Object).First
                End If
            End If
            If TypeOf ret Is XmlAttribute Then
                Return DirectCast(ret, XmlAttribute).Value
            ElseIf TypeOf ret Is XAttribute Then
                Return DirectCast(ret, XAttribute).Value
            ElseIf TypeOf ret Is XmlText Then
                Return DirectCast(ret, XmlText).Value
            ElseIf TypeOf ret Is XText Then
                Return DirectCast(ret, XText).Value
            ElseIf TypeOf ret Is XmlCDataSection Then
                Return DirectCast(ret, XmlCDataSection).Value
            ElseIf TypeOf ret Is XCData Then
                Return DirectCast(ret, XCData).Value
            ElseIf TypeOf ret Is XmlComment Then
                Return DirectCast(ret, XmlComment).Value
            ElseIf TypeOf ret Is XComment Then
                Return DirectCast(ret, XComment).Value
            End If
            Return ret
        End Function

        ''' <summary>Throws a <see cref="NotSupportedException"/> - this converter is only one-way</summary>
        ''' <param name="value">ignored</param>
        ''' <param name="targetType">ignored</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <returns>Never returns - always throws a <see cref="NotSupportedException"/></returns>
        ''' <exception cref="NotSupportedException">Always - this converter is one-way only.</exception>
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_CannotConvertBack.f([GetType].Name))
        End Function
    End Class
End Namespace
#End If