Imports System.Windows, Tools.ExtensionsT, System.Linq
Imports System.Windows.Markup
Imports System.Xaml
Imports System.Windows.Data

Namespace WindowsT.WPF.MarkupT
    ''' <summary>Wraps any <see cref="IServiceProvider"/> and provides properties for easy retrieval of XAML-related services</summary>
    ''' <seelaso cref="Tools.WindowsT.WPF.WpfExtensions.GetXamlService"/>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public Class XamlServiceProvider : Implements IServiceProvider
        ''' <summary>Internal service provider this service provider is based on</summary>
        Private internal As IServiceProvider
        ''' <summary>CTor - creates a new instance of the <see cref="XamlServiceProvider"/> class</summary>
        ''' <param name="provider">Internal service provider. Can be null. In this case this service provider always returns null for any service requested.</param>
        Public Sub New(ByVal provider As IServiceProvider)
            internal = provider
        End Sub

        ''' <summary>Gets the service object of the specified type.</summary>
        ''' <returns>A service object of type <paramref name="serviceType" />.-or- null if there is no service object of type <paramref name="serviceType" />.</returns>
        ''' <param name="serviceType">An object that specifies the type of service object to get. </param>
        Public Function GetService(ByVal serviceType As System.Type) As Object Implements System.IServiceProvider.GetService
            Return If(internal Is Nothing, Nothing, internal.GetService(serviceType))
        End Function
        ''' <summary>Gets <see cref="IServiceProvider"/> this provider internally uses</summary>
        ''' <returns><see cref="IServiceProvider"/> internally used by this service provider. Can be null.</returns>
        Public ReadOnly Property InnerProvider As IServiceProvider
            Get
                Return internal
            End Get
        End Property
#Region "Services"
        ''' <summary><see cref="IProvideValueTarget"/> (Reports situational object-property relationships for markup extension evaluation.)</summary>
        ''' <returns>The <see cref="IProvideValueTarget"/> service. Null if the service is not provided by current service provider.</returns>
        Public ReadOnly Property ProvideValueTarget As IProvideValueTarget
            Get
                Return Me.GetXamlService(XamlService.ProvideValueTarget)
            End Get
        End Property
        ''' <summary><see cref="IXamlTypeResolver"/> (Resolves from named elements in XAML markup to the appropriate CLR type.)</summary>
        ''' <returns>The <see cref="IXamlTypeResolver"/> service. Null if the service is not provided by current service provider.</returns>
        Public ReadOnly Property XamlTypeResolver As IXamlTypeResolver
            Get
                Return Me.GetXamlService(XamlService.XamlTypeResolver)
            End Get
        End Property
        ''' <summary><see cref="IXamlSchemaContextProvider"/> (Provides XAML schema context information to type converters and markup extensions.)</summary>
        ''' <returns>The <see cref="IXamlSchemaContextProvider"/> service. Null if the service is not provided by current service provider.</returns>
        Public ReadOnly Property XamlSchemaContextProvider As IXamlSchemaContextProvider
            Get
                Return Me.GetXamlService(XamlService.XamlSchemaContextProvider)
            End Get
        End Property
        ''' <summary><see cref="IUriContext"/> (Can use application context to resolve a provided relative URI to an absolute URI.)</summary>
        ''' <returns>The <see cref="IUriContext"/> service. Null if the service is not provided by current service provider.</returns>
        Public ReadOnly Property UriContext As IUriContext
            Get
                Return Me.GetXamlService(XamlService.UriContext)
            End Get
        End Property
        ''' <summary><see cref="IAmbientProvider"/> (Can return information items of ambient properties or ambient types to type converters and markup extensions.)</summary>
        ''' <returns>The <see cref="IAmbientProvider"/> service. Null if the service is not provided by current service provider.</returns>
        Public ReadOnly Property AmbientProvider As IAmbientProvider
            Get
                Return Me.GetXamlService(XamlService.AmbientProvider)
            End Get
        End Property
        ''' <summary><see cref="IDestinationTypeProvider"/> (Can return a type system identifier for the destination type. The destination type is relevant for cases where there is an indirect reporting of destination type for a set operation based on reflection or other mechanisms.)</summary>
        ''' <returns>The <see cref="IDestinationTypeProvider"/> service. Null if the service is not provided by current service provider.</returns>
        Public ReadOnly Property DestinationTypeProvider As IDestinationTypeProvider
            Get
                Return Me.GetXamlService(XamlService.DestinationTypeProvider)
            End Get
        End Property
        ''' <summary><see cref="IRootObjectProvider"/> (Can return the root object of markup being parsed.)</summary>
        ''' <returns>The <see cref="IRootObjectProvider"/> service. Null if the service is not provided by current service provider.</returns>
        Public ReadOnly Property RootObjectProvider As IRootObjectProvider
            Get
                Return Me.GetXamlService(XamlService.RootObjectProvider)
            End Get
        End Property
        ''' <summary><see cref="IXamlNameResolver"/> (Can return objects specified by name, or alternatively returns a token. The service can also return an enumerable set of all named objects that are in the XAML namescope.)</summary>
        ''' <returns>The <see cref="IXamlNameResolver"/> service. Null if the service is not provided by current service provider.</returns>
        Public ReadOnly Property XamlNameResolver As IXamlNameResolver
            Get
                Return Me.GetXamlService(XamlService.XamlNameResolver)
            End Get
        End Property
        ''' <summary><see cref="IXamlNamespaceResolver"/> (Can return a XAML namespace based on its prefix as mapped in XAML markup.)</summary>
        ''' <returns>The <see cref="IXamlNamespaceResolver"/> service. Null if the service is not provided by current service provider.</returns>
        Public ReadOnly Property XamlNamespaceResolver As IXamlNamespaceResolver
            Get
                Return Me.GetXamlService(XamlService.XamlNamespaceResolver)
            End Get
        End Property
#End Region
    End Class
End Namespace