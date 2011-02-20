Imports System.componentmodel
Namespace ComponentModelT
    ''' <summary>A <see cref="TypeDescriptionProvider"/> for one type</summary>
    ''' <typeparam name="T">The type this <see cref="ICustomTypeDescriptor"/> is provided for</typeparam>
    ''' <typeparam name="TDescriptor">Type of <see cref="ICustomTypeDescriptor"/> providing description of type <typeparamref name="T"/>.
    ''' It must have publlic constuctor accepting parameters (<typeparamref name="T"/>, <see cref="ICustomTypeDescriptor"/>)</typeparam>
    Friend Class SingleTypeDescriptionProvider(Of T, TDescriptor As ICustomTypeDescriptor)
        Inherits TypeDescriptionProvider
        ''' <summary>Default provider</summary>
        Private Shared DefaultTypeDescriptorProvider As TypeDescriptionProvider = TypeDescriptor.GetProvider(GetType(T))
        ''' <summary>CTor - creates a new instance of the <see cref="SingleTypeDescriptionProvider(Of T, TDEscriptor)"/> class</summary>
        Public Sub New()
            MyBase.New(DefaultTypeDescriptorProvider)
        End Sub
        ''' <summary>Gets a custom type descriptor for the given type and object.</summary>
        ''' <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
        ''' <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="System.ComponentModel.TypeDescriptor"/>.</param>
        ''' <returns>An <see cref="System.ComponentModel.ICustomTypeDescriptor"/> that can provide metadata for the type.</returns>
        Public Overrides Function GetTypeDescriptor(ByVal objectType As System.Type, ByVal instance As Object) As System.ComponentModel.ICustomTypeDescriptor
            If objectType.Equals(GetType(T)) Then Return Activator.CreateInstance(GetType(TDescriptor), instance, DefaultTypeDescriptorProvider.GetTypeDescriptor(objectType))
            Return MyBase.GetTypeDescriptor(objectType, instance)
        End Function
    End Class
End Namespace