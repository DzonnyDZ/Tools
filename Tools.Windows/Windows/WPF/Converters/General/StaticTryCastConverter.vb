Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter that returns given value of it is of type of converter; otherwise returns null</summary>
    ''' <typeparam name="T">Type of value to return</typeparam>
    ''' <remarks>Due to lack of support for generic types in XAML this class cannot be instantiated directly. Use the <see cref="MarkupT.GenericExtension"/>.
    ''' <example>Following example shows how to use <see cref="MarkupT.GenericExtension"/> to create instance of <see cref="StaticTryCastConverter(Of T)"/>
    ''' <code lang="XAML"><![CDATA[
    ''' <ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    '''                xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    '''                xmlns:conv="clr-namespace:Tools.WindowsT.WPF.ConvertersT;assembly=Tools.Windows"
    '''                xmlns:WF="clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms"
    '''                xmlns:mu="clr-namespace:Tools.WindowsT.WPF.MarkupT;assembly=Tools.Windows"
    ''' >
    '''     <mu:GenericExtension TypeName="conv:StaticTryCastConverter" x:Key="convTryWindowsForms">
    '''         <x:Type TypeName="WF:Control"/>
    '''     </mu:GenericExtension>
    ''' </ResourceDictionary>
    ''' ]]></code></example></remarks>
    ''' <version version="1.5.2" stage="Alpha">Class introduced</version>
    Public NotInheritable Class StaticTryCastConverter(Of T As Class)
        Inherits StronglyTypedConverter(Of Object, T)
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns>If <paramref name="value"/> is <typeparamref name="T"/> returns <paramref name="value"/>; otherwise null</returns>
        Public Overrides Function Convert(ByVal value As Object, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As T
            Return TryCast(value, T)
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <returns><paramref name="value"/></returns>
        Public Overrides Function ConvertBack(ByVal value As T, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object
            Return value
        End Function
    End Class
End Namespace
#End If