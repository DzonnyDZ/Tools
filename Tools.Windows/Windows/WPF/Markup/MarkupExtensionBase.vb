Imports System.Windows, Tools.ExtensionsT, System.Linq
Imports System.Windows.Markup
Imports System.Xaml
Imports System.Windows.Data


Namespace WindowsT.WPF.MarkupT
    ''' <summary>Base class for implementations of <see cref="MarkupExtension"/> which makes it easier to implement <see cref="MarkupExtension"/> by providing easier access to some commonly required services.</summary>
    ''' <seelaso cref="MarkupExtension"/><seelaso cref="XamlServiceProvider"/>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public MustInherit Class MarkupExtensionBase
        Inherits MarkupExtension
        ''' <summary>Returns an object that is set as the value of the target property for this markup extension. </summary>
        ''' <returns>The object value to set on the property where the extension is applied. </returns>
        ''' <param name="serviceProvider">Object that can provide services for the markup extension.</param>
        ''' <remarks><note type="inheritinfo">Override overload instead.</note></remarks>
        Public NotOverridable Overloads Overrides Function ProvideValue(ByVal serviceProvider As System.IServiceProvider) As Object
            Return ProvideValue(New XamlServiceProvider(serviceProvider))
        End Function
        ''' <summary>When overridden in derived class returns an object that is set as the value of the target property for this markup extension. </summary>
        ''' <returns>The object value to set on the property where the extension is applied. </returns>
        ''' <param name="serviceProvider">Object that can provide services for the markup extension. This implementation never passes value null here.</param>
        Protected MustOverride Overloads Function ProvideValue(ByVal serviceProvider As XamlServiceProvider) As Object
    End Class
End Namespace
