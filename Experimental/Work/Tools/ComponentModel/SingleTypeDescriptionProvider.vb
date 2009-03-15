Imports System.componentmodel
Namespace ComponentModel
    ''' <summary><see cref="TypeDescriptionProvider"/> pro jeden typ</summary>
    ''' <typeparam name="T">Typ pro nìjž je <see cref="ICustomTypeDescriptor"/> poskytován</typeparam>
    ''' <typeparam name="TDescriptor">Typ <see cref="ICustomTypeDescriptor"/> poskytující ppopis typu <typeparamref name="T"/>.
    ''' Musí mít veøejný konstruktor akceptující parametry (<typeparamref name="T"/>, <see cref="ICustomTypeDescriptor"/>)</typeparam>
    Public Class SingleTypeDescriptionProvider(Of T, TDescriptor As ICustomTypeDescriptor)
        Inherits TypeDescriptionProvider
        ''' <summary>Výchozí provider</summary>
        Private Shared DefaultTypeDescriptorProvider As TypeDescriptionProvider = TypeDescriptor.GetProvider(GetType(T))
        ''' <summary>CTor</summary>
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