Imports System.ComponentModel
'ASAP: WiKi, Mark, Forum, Comment
Namespace ComponentModelT
    Public MustInherit Class CustomTypeDescriptorAbstractBase : Inherits CustomTypeDescriptor
        Protected Sub New()
            MyBase.New()
        End Sub
        Protected Sub New(ByVal parent As ICustomTypeDescriptor)
            MyBase.New(parent)
        End Sub
        Public MustOverride Overrides Function GetAttributes() As System.ComponentModel.AttributeCollection
        Public MustOverride Overrides Function GetClassName() As String
        Public MustOverride Overrides Function GetComponentName() As String
        Public MustOverride Overrides Function GetConverter() As System.ComponentModel.TypeConverter
        Public MustOverride Overrides Function GetDefaultEvent() As System.ComponentModel.EventDescriptor
        Public MustOverride Overrides Function GetDefaultProperty() As System.ComponentModel.PropertyDescriptor
        Public MustOverride Overrides Function GetEditor(ByVal editorBaseType As System.Type) As Object
        Public MustOverride Overrides Function GetEvents() As System.ComponentModel.EventDescriptorCollection
        Public MustOverride Overrides Function GetEvents(ByVal attributes() As System.Attribute) As System.ComponentModel.EventDescriptorCollection
        Public MustOverride Overrides Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection
        Public MustOverride Overrides Function GetProperties(ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
        Public MustOverride Overrides Function GetPropertyOwner(ByVal pd As System.ComponentModel.PropertyDescriptor) As Object
    End Class
    Public Class CustomTypeDescriptorBase(Of T) : Inherits CustomTypeDescriptorAbstractBase
        Private _instance As T
        Protected Property Instance() As T
            Get
                Return _instance
            End Get
            Private Set(ByVal value As T)
                _instance = value
            End Set
        End Property
        Protected Sub New(ByVal instance As T)
            MyBase.New()
            Me.instance = instance
        End Sub
        Protected Sub New(ByVal instance As T, ByVal parent As ICustomTypeDescriptor)
            MyBase.New(parent)
            Me.instance = instance
        End Sub
        Public Overrides Function GetAttributes() As System.ComponentModel.AttributeCollection
            Return TypeDescriptor.GetAttributes(Instance, False)
        End Function

        Public Overrides Function GetClassName() As String
            Return TypeDescriptor.GetClassName(Instance, False)
        End Function

        Public Overrides Function GetComponentName() As String
            Return TypeDescriptor.GetComponentName(Instance, False)
        End Function

        Public Overrides Function GetConverter() As System.ComponentModel.TypeConverter
            Return TypeDescriptor.GetConverter(Instance, False)
        End Function

        Public Overrides Function GetDefaultEvent() As System.ComponentModel.EventDescriptor
            Return TypeDescriptor.GetDefaultEvent(Instance, False)
        End Function

        Public Overrides Function GetDefaultProperty() As System.ComponentModel.PropertyDescriptor
            Return TypeDescriptor.GetDefaultProperty(Instance, False)
        End Function

        Public Overrides Function GetEditor(ByVal editorBaseType As System.Type) As Object
            Return TypeDescriptor.GetEditor(Instance, editorBaseType, False)
        End Function

        Public Overloads Overrides Function GetEvents() As System.ComponentModel.EventDescriptorCollection
            Return TypeDescriptor.GetEvents(Instance, False)
        End Function

        Public Overloads Overrides Function GetEvents(ByVal attributes() As System.Attribute) As System.ComponentModel.EventDescriptorCollection
            Return TypeDescriptor.GetEvents(Instance, attributes, False)
        End Function

        Public Overloads Overrides Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection
            Return TypeDescriptor.GetProperties(Instance, False)
        End Function

        Public Overloads Overrides Function GetProperties(ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
            Return TypeDescriptor.GetProperties(Instance, attributes, False)
        End Function

        Public Overrides Function GetPropertyOwner(ByVal pd As System.ComponentModel.PropertyDescriptor) As Object
            Return Nothing
        End Function
    End Class
End Namespace
