Imports System.ComponentModel
#If Config <= Nightly Then
'ASAP: WiKi, 
Namespace ComponentModelT
    ''' <summary>Provides abstract base class for custom type descriptors</summary>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public MustInherit Class CustomTypeDescriptorAbstractBase : Inherits CustomTypeDescriptor
        ''' <summary>CTor</summary>
        Protected Sub New()
            MyBase.New()
        End Sub
        ''' <summary>CTor with parent custom type descriptor</summary>
        ''' <param name="parent">The parent custom type descriptor.</param>
        Protected Sub New(ByVal parent As ICustomTypeDescriptor)
            MyBase.New(parent)
        End Sub
        ''' <summary>Returns a collection of custom attributes for the type represented by this type descriptor.</summary>
        ''' <returns>An <see cref="System.ComponentModel.AttributeCollection"/> containing the attributes for the type.</returns>
        Public MustOverride Overrides Function GetAttributes() As System.ComponentModel.AttributeCollection
        ''' <summary>Returns the fully qualified name of the class represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.String" /> containing the fully qualified class name of the type this type descriptor is describing. The default is null.</returns>
        Public MustOverride Overrides Function GetClassName() As String
        ''' <summary>Returns the name of the class represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.String" /> containing the name of the component instance this type descriptor is describing. The default is null.</returns>
        Public MustOverride Overrides Function GetComponentName() As String
        ''' <summary>Returns a type converter for the type represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the type represented by this type descriptor. The default is a newly created <see cref="T:System.ComponentModel.TypeConverter" />.</returns>
        Public MustOverride Overrides Function GetConverter() As System.ComponentModel.TypeConverter
        ''' <summary>Returns the event descriptor for the default event of the object represented by this type descriptor.</summary>
        ''' <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> for the default event on the object represented by this type descriptor. The default is null.</returns>
        Public MustOverride Overrides Function GetDefaultEvent() As System.ComponentModel.EventDescriptor
        ''' <summary>Returns the property descriptor for the default property of the object represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the default property on the object represented by this type descriptor. The default is null.</returns>
        Public MustOverride Overrides Function GetDefaultProperty() As System.ComponentModel.PropertyDescriptor
        ''' <summary>Returns an editor of the specified type that is to be associated with the class represented by this type descriptor.</summary>
        ''' <returns>An editor of the given type that is to be associated with the class represented by this type descriptor. The default is null.</returns>
        ''' <param name="editorBaseType">The base type of the editor to retrieve.</param>
        Public MustOverride Overrides Function GetEditor(ByVal editorBaseType As System.Type) As Object
        ''' <summary>Returns a collection of event descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptors for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
        Public MustOverride Overrides Function GetEvents() As System.ComponentModel.EventDescriptorCollection
        ''' <summary>Returns a collection of event descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptors for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
        Public MustOverride Overrides Function GetEvents(ByVal attributes() As System.Attribute) As System.ComponentModel.EventDescriptorCollection
        ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
        Public MustOverride Overrides Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection
        ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
        Public MustOverride Overrides Function GetProperties(ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
        ''' <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
        ''' <returns>An <see cref="T:System.Object" /> that owns the given property specified by the type descriptor. The default is null.</returns>
        ''' <param name="pd">The property descriptor for which to retrieve the owning object.</param>
        Public MustOverride Overrides Function GetPropertyOwner(ByVal pd As System.ComponentModel.PropertyDescriptor) As Object
    End Class
    ''' <summary>Provides base class for "top-level" custom type descriptors</summary>
    ''' <remarks>All method in this class, if not overriden in derived class, uses <see cref="TypeDescriptor"/> to obtain information about instance.</remarks>
    ''' <typeparam name="T">Type the descriptor is implemented for. Use <see cref="System.Object"/> to create univarsal one.</typeparam>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Class CustomTypeDescriptorBase(Of T) : Inherits CustomTypeDescriptorAbstractBase
        ''' <summary>Contains value of the <see cref="Instance"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _instance As T
        ''' <summary>Instance of type <see cref="T"/> described by this instance of <see cref="CustomTypeDescriptorBase(Of T)"/></summary>
        Protected Property Instance() As T
            Get
                Return _instance
            End Get
            Private Set(ByVal value As T)
                _instance = value
            End Set
        End Property
        ''' <summary>CTor</summary>
        ''' <param name="instance">Instance to be described</param>
        Protected Sub New(ByVal instance As T)
            MyBase.New()
            Me.Instance = instance
        End Sub

        ''' <summary>Returns a collection of custom attributes for the type represented by this type descriptor.</summary>
        ''' <returns>An <see cref="System.ComponentModel.AttributeCollection"/> containing the attributes for the type.</returns>
        Public Overrides Function GetAttributes() As System.ComponentModel.AttributeCollection
            Return TypeDescriptor.GetAttributes(Instance, False)
        End Function
        ''' <summary>Returns the fully qualified name of the class represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.String" /> containing the fully qualified class name of the type this type descriptor is describing. The default is null.</returns>
        Public Overrides Function GetClassName() As String
            Return TypeDescriptor.GetClassName(Instance, False)
        End Function

        ''' <summary>Returns the name of the class represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.String" /> containing the name of the component instance this type descriptor is describing. The default is null.</returns>
        Public Overrides Function GetComponentName() As String
            Return TypeDescriptor.GetComponentName(Instance, False)
        End Function

        ''' <summary>Returns a type converter for the type represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the type represented by this type descriptor. The default is a newly created <see cref="T:System.ComponentModel.TypeConverter" />.</returns>
        Public Overrides Function GetConverter() As System.ComponentModel.TypeConverter
            Return TypeDescriptor.GetConverter(Instance, False)
        End Function

        ''' <summary>Returns the event descriptor for the default event of the object represented by this type descriptor.</summary>
        ''' <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> for the default event on the object represented by this type descriptor. The default is null.</returns>
        Public Overrides Function GetDefaultEvent() As System.ComponentModel.EventDescriptor
            Return TypeDescriptor.GetDefaultEvent(Instance, False)
        End Function

        ''' <summary>Returns the property descriptor for the default property of the object represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the default property on the object represented by this type descriptor. The default is null.</returns>
        Public Overrides Function GetDefaultProperty() As System.ComponentModel.PropertyDescriptor
            Return TypeDescriptor.GetDefaultProperty(Instance, False)
        End Function

        ''' <summary>Returns an editor of the specified type that is to be associated with the class represented by this type descriptor.</summary>
        ''' <returns>An editor of the given type that is to be associated with the class represented by this type descriptor. The default is null.</returns>
        ''' <param name="editorBaseType">The base type of the editor to retrieve.</param>
        Public Overrides Function GetEditor(ByVal editorBaseType As System.Type) As Object
            Return TypeDescriptor.GetEditor(Instance, editorBaseType, False)
        End Function

        ''' <summary>Returns a collection of event descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptors for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
        Public Overloads Overrides Function GetEvents() As System.ComponentModel.EventDescriptorCollection
            Return TypeDescriptor.GetEvents(Instance, False)
        End Function

        ''' <summary>Returns a collection of event descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptors for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
        Public Overloads Overrides Function GetEvents(ByVal attributes() As System.Attribute) As System.ComponentModel.EventDescriptorCollection
            Return TypeDescriptor.GetEvents(Instance, attributes, False)
        End Function

        ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
        Public Overloads Overrides Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection
            Return TypeDescriptor.GetProperties(Instance, False)
        End Function

        ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
        Public Overloads Overrides Function GetProperties(ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
            Return TypeDescriptor.GetProperties(Instance, attributes, False)
        End Function

        ''' <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
        ''' <returns>An <see cref="T:System.Object" /> that owns the given property specified by the type descriptor. The default is null.</returns>
        ''' <param name="pd">The property descriptor for which to retrieve the owning object.</param>
        Public Overrides Function GetPropertyOwner(ByVal pd As System.ComponentModel.PropertyDescriptor) As Object
            Return Nothing
        End Function
    End Class
End Namespace
#End If