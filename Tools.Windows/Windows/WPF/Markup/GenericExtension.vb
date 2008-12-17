Imports System.Windows, Tools.ExtensionsT
Imports System.Windows.Markup

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.MarkupT
    ''' <summary>Markup extension that can create instance of generic type</summary>
    ''' <author www="http://blogs.msdn.com/mikehillberg/archive/2006/10/06/LimitedGenericsSupportInXaml.aspx">Mike Hillberg</author>
    ''' <version stage="Nightly" version="1.5.2">Class introduced</version>
    <ContentPropertyAttribute("TypeArguments")> _
    Public Class GenericExtension : Inherits MarkupExtension
        ''' <summary>Contains value of the <see cref="TypeArguments"/> proeprty</summary>
        Private _typeArguments As New System.Collections.ObjectModel.Collection(Of Type)
        ''' <summary>Gets the collection of type arguments for the generic type</summary>
        ''' <returns>The collection of type arguments for the generic type</returns>
        Public ReadOnly Property TypeArguments() As System.Collections.ObjectModel.Collection(Of Type)
            Get
                Return _typeArguments
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="TypeName"/> property</summary>
        Private _typeName$
        ''' <summary>Gets or sets the generic type name (e.g. Dictionary, for the Dictionary`2 case)</summary>
        ''' <returns>The generic type name (e.g. Dictionary, for the Dictionary`2 case)</returns>
        Public Property TypeName$()
            Get
                Return _typeName
            End Get
            Set(ByVal value$)
                _typeName = value
            End Set
        End Property
        ''' <summary>Default CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTof ffrom type name</summary>
        ''' <param name="typeName">The generic type name (e.g. Dictionary, for the Dictionary`2 case)</param>
        Public Sub New(ByVal typeName$)
            typeName = typeName
        End Sub

        ''' <summary>Returns an object - instance of the constructed generic type that is set as the value of the target property for this markup extension.</summary>
        ''' <returns>An object instance of the constructed generic type</returns>
        ''' <param name="serviceProvider">Object that can provide services for the markup extension.</param>
        Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
            Dim xamlTypeResolver = TryCast(serviceProvider.GetService(GetType(IXamlTypeResolver)), IXamlTypeResolver)
            If xamlTypeResolver Is Nothing Then Throw New Exception(ResourcesT.Exceptions.The0MarkupExtensionRequiresAn1ServiceProvider.f("Generic", "IXamlTypeResolver"))
            ' Get e.g. "Collection`1" type
            Dim genericType = xamlTypeResolver.Resolve(_typeName + "`"c + TypeArguments.Count.ToString())
            ' Get an array of the type arguments
            Dim typeArgumentArray(TypeArguments.Count - 1) As Type
            TypeArguments.CopyTo(typeArgumentArray, 0)
            ' Create the conrete type, e.g. Collection<String>
            Dim constructedType = genericType.MakeGenericType(typeArgumentArray)
            ' Create an instance of that type
            Return Activator.CreateInstance(constructedType)
        End Function
    End Class


End Namespace
#End If