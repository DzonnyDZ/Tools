Imports System.ComponentModel
Namespace ComponentModelT
    ''' <summary>Wraps a <see cref="PropertyDescriptor"/> ans another <see cref="PropertyDescriptor"/> and allows to add attributes to original one.</summary>
    Public Class WrapPropertyDescriptor
        Inherits PropertyDescriptor
        ''' <summary>Instance to be wrapped</summary>
        Private ReadOnly wraps As PropertyDescriptor
        ''' <summary>CTor - creates a newin instance of the <see cref="WrapPropertyDescriptor"/> class</summary>
        ''' <param name="Wrap">Original <see cref="PropertyDescriptor"/></param>
        ''' <param name="Attributes">Additional attributes added to attributes read from <paramref name="Wrap"/></param>
        Public Sub New(ByVal Wrap As PropertyDescriptor, ByVal ParamArray Attributes As Attribute())
            MyBase.New(Wrap)
            Me.wraps = Wrap
            Dim AllAttributes As New List(Of Attribute)
            For Each attr As Attribute In Wrap.Attributes
                AllAttributes.Add(attr)
            Next
            For Each attr As Attribute In Attributes
                AllAttributes.Add(attr)
            Next
            MyBase.AttributeArray = AllAttributes.ToArray
        End Sub
        ''' <summary>Returns whether resetting an object changes its value.</summary>
        ''' <param name="component">The component to test for reset capability.</param>
        ''' <returns>true if resetting the component changes its value; otherwise, false</returns>
        Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
            Return wraps.CanResetValue(component)
        End Function
        ''' <summary>Gets the type of the component this property is bound to.</summary>
        ''' <returns>A <see cref="System.Type"/> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)"/> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)"/> methods are invoked, the object specified might be an instance of this type</returns>
        Public Overrides ReadOnly Property ComponentType() As System.Type
            Get
                Return wraps.ComponentType
            End Get
        End Property
        ''' <summary>Gets the current value of the property on a component.</summary>
        ''' <param name="component">The component with the property for which to retrieve the value.</param>
        ''' <returns>The value of a property for a given component</returns>
        Public Overrides Function GetValue(ByVal component As Object) As Object
            Return wraps.GetValue(component)
        End Function
        ''' <summary>Gets a value indicating whether this property is read-only.</summary>
        ''' <returns>true if the property is read-only; otherwise, false.</returns>
        Public Overrides ReadOnly Property IsReadOnly() As Boolean
            Get
                Return wraps.IsReadOnly
            End Get
        End Property
        ''' <summary>Gets the type of the property.</summary>
        ''' <returns>A <see cref="System.Type"/> that represents the type of the property.</returns>
        Public Overrides ReadOnly Property PropertyType() As System.Type
            Get
                Return wraps.PropertyType
            End Get
        End Property
        ''' <summary>Resets the value for this property of the component to the default value.</summary>
        ''' <param name="component">The component with the property value that is to be reset to the default value</param>
        Public Overrides Sub ResetValue(ByVal component As Object)
            wraps.ResetValue(component)
        End Sub
        ''' <summary>When overridden in a derived class, sets the value of the component to a different value.</summary>
        ''' <param name="component">The component with the property value that is to be set.</param>
        ''' <param name="value">The new value.</param>
        Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
            wraps.SetValue(component, value)
        End Sub
        ''' <summary>Determines a value indicating whether the value of this property needs to be persisted.</summary>
        ''' <param name="component">The component with the property to be examined for persistence.</param>
        ''' <returns>true if the property should be persisted; otherwise, false</returns>
        Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
            Return wraps.ShouldSerializeValue(component)
        End Function
    End Class
End Namespace