Imports System.Linq
#If Config <= Nightly Then 'Stage: Nightly
Namespace ComponentModelT
    ''' <summary>Encapsulates any object and makes it read-only</summary>
    ''' <remarks>Supports both - <see cref="TypeDescriptor"/> and <see cref="ICustomTypeDescriptor"/></remarks>
    Public NotInheritable Class ReadOnlyObject
        Implements ICustomTypeDescriptor
        ''' <summary>Contains value of the <see cref="[Object]"/> property</summary>
        ReadOnly Obj As Object
        ''' <summary>CTor</summary>
        ''' <exception cref="ArgumentNullException"><paramref name="Obj"/> is null</exception>
        ''' <param name="Obj">Object to encapsulate</param>
        Public Sub New(ByVal Obj As Object)
            If Obj Is Nothing Then Throw New ArgumentNullException("Obj")
            Me.Obj = Obj
        End Sub
        ''' <summary>Gets currently encapsulated object</summary>
        ''' <returns>Currently encapsulated object</returns>
        Public ReadOnly Property [Object]() As Object
            Get
                Return Obj
            End Get
        End Property
        ''' <summary>Returns a collection of custom attributes for this instance of a component.</summary>
        ''' <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for this object.</returns>
        Public Function GetAttributes() As System.ComponentModel.AttributeCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetAttributes
            If TypeOf Me.Object Is ICustomTypeDescriptor Then Return DirectCast(Me.Obj, ICustomTypeDescriptor).GetAttributes
            Return TypeDescriptor.GetAttributes(Me.Object)
        End Function

        ''' <summary>Returns the class name of this instance of a component.</summary>
        ''' <returns>The class name of the object, or null if the class does not have a name.</returns>
        Public Function GetClassName() As String Implements System.ComponentModel.ICustomTypeDescriptor.GetClassName
            If TypeOf Me.Object Is ICustomTypeDescriptor Then Return DirectCast(Me.Obj, ICustomTypeDescriptor).GetClassName
            Return TypeDescriptor.GetClassName(Me.Object)
        End Function

        ''' <summary>Returns the name of this instance of a component.</summary>
        ''' <returns>The name of the object, or null if the object does not have a name.</returns>
        Public Function GetComponentName() As String Implements System.ComponentModel.ICustomTypeDescriptor.GetComponentName
            If TypeOf Me.Object Is ICustomTypeDescriptor Then Return DirectCast(Me.Obj, ICustomTypeDescriptor).GetComponentName
            Return TypeDescriptor.GetComponentName(Me.Object)
        End Function

        ''' <summary>Returns a type converter for this instance of a component.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or null if there is no <see cref="T:System.ComponentModel.TypeConverter" /> for this object.
        ''' This implementation always returns null</returns>
        Public Function GetConverter() As System.ComponentModel.TypeConverter Implements System.ComponentModel.ICustomTypeDescriptor.GetConverter
            Return Nothing
        End Function

        ''' <summary>Returns the default event for this instance of a component.</summary>
        ''' <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for this object, or null if this object does not have events.</returns>
        Public Function GetDefaultEvent() As System.ComponentModel.EventDescriptor Implements System.ComponentModel.ICustomTypeDescriptor.GetDefaultEvent
            If TypeOf Me.Object Is ICustomTypeDescriptor Then Return DirectCast(Me.Obj, ICustomTypeDescriptor).GetDefaultEvent
            Return TypeDescriptor.GetDefaultEvent(Me.Object)
        End Function

        ''' <summary>Returns the default property for this instance of a component.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for this object, or null if this object does not have properties.
        ''' Returned property (if any) is always read-only.</returns>
        Public Function GetDefaultProperty() As System.ComponentModel.PropertyDescriptor Implements System.ComponentModel.ICustomTypeDescriptor.GetDefaultProperty
            Return ModifyPropertyDescriptor(TypeDescriptor.GetDefaultProperty(Me.Object))
        End Function

        ''' <summary>Returns an editor of the specified type for this instance of a component.</summary>
        ''' <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found.</returns>
        ''' <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object. </param>
        Public Function GetEditor(ByVal editorBaseType As System.Type) As Object Implements System.ComponentModel.ICustomTypeDescriptor.GetEditor
            If TypeOf Me.Object Is ICustomTypeDescriptor Then Return DirectCast(Me.Obj, ICustomTypeDescriptor).GetEditor(editorBaseType)
            Return TypeDescriptor.GetEditor(Me.Object, editorBaseType)
        End Function

        ''' <summary>Returns the events for this instance of a component.</summary>
        ''' <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for this component instance.</returns>
        Public Function GetEvents() As System.ComponentModel.EventDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetEvents
            If TypeOf Me.Object Is ICustomTypeDescriptor Then Return DirectCast(Me.Obj, ICustomTypeDescriptor).GetEvents
            Return TypeDescriptor.GetEvents(Me.Object)
        End Function

        ''' <summary>Returns the events for this instance of a component using the specified attribute array as a filter.</summary>
        ''' <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the filtered events for this component instance.</returns>
        ''' <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
        Public Function GetEvents(ByVal attributes() As System.Attribute) As System.ComponentModel.EventDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetEvents
            If TypeOf Me.Object Is ICustomTypeDescriptor Then Return DirectCast(Me.Obj, ICustomTypeDescriptor).GetEvents(attributes)
            Return TypeDescriptor.GetEvents(Me.Object, attributes)
        End Function


        ''' <summary>Returns the properties for this instance of a component.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties for this component instance.
        ''' Properties returned are read-only.</returns>
        Public Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetProperties
            Return From pd In If(TypeOf Me.Object Is ICustomTypeDescriptor, DirectCast(Me.Obj, ICustomTypeDescriptor).GetProperties, TypeDescriptor.GetProperties(Obj)) _
                Select ModifyPropertyDescriptor(pd)
        End Function
        ''' <summary>Returns the properties for this instance of a component using the attribute array as a filter.</summary>
        ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the filtered properties for this component instance.
        ''' Properties returned are read-only.</returns>
        ''' <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
        Public Function GetProperties(ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetProperties
            Return New PropertyDescriptorCollection((From pd In If(TypeOf Me.Object Is ICustomTypeDescriptor, DirectCast(Me.Object, ICustomTypeDescriptor).GetProperties(attributes), TypeDescriptor.GetProperties(Obj, attributes)) _
                Select ModifyPropertyDescriptor(pd)).ToArray)
        End Function

        ''' <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
        ''' <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
        ''' <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found. </param>
        Public Function GetPropertyOwner(ByVal pd As System.ComponentModel.PropertyDescriptor) As Object Implements System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner
            If TypeOf pd Is ReadOnlyPropertyDescriptor Then Return Me Else Return Obj
        End Function
        ''' <summary>If necessary encapsulates <see cref="PropertyDescriptor"/> to be read-only</summary>
        ''' <param name="pd"><see cref="PropertyDescriptor"/> to encapsulate</param>
        ''' <returns>Read only <see cref="PropertyDescriptor"/></returns>
        Private Function ModifyPropertyDescriptor(ByVal pd As PropertyDescriptor) As PropertyDescriptor
            If pd Is Nothing OrElse pd.IsReadOnly Then Return pd Else Return New ReadOnlyPropertyDescriptor(pd)
        End Function

        ''' <summary>Read-only <see cref="PropertyDescriptor"/></summary>
        ''' <remarks>Encapsulates existing <see cref="PropertyDescriptor"/> to be read-only</remarks>
        Private Class ReadOnlyPropertyDescriptor : Inherits PropertyDescriptor
            ''' <summary>Encapsulated <see cref="PropertyDescriptor"/></summary>
            Private ReadOnly Original As PropertyDescriptor
            ''' <summary>CTor</summary>
            ''' <param name="Original"><see cref="PropertyDescriptor"/> to encapsulate</param>
            Public Sub New(ByVal Original As PropertyDescriptor)
                MyBase.New(Original.Name, Original.Attributes.OfType(Of Attribute).ToArray)
                Me.Original = Original
            End Sub
            ''' <summary>Returns whether resetting an object changes its value.</summary>
            ''' <returns>true if resetting the component changes its value; otherwise, false. This implementation always returns false.</returns>
            ''' <param name="component">The component to test for reset capability. </param>
            Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
                Return False
            End Function

            ''' <summary>Gets the type of the component this property is bound to.</summary>
            ''' <returns>A <see cref="T:System.Type" /> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)" /> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)" /> methods are invoked, the object specified might be an instance of this type.</returns>
            Public Overrides ReadOnly Property ComponentType() As System.Type
                Get
                    Return Me.Original.ComponentType
                End Get
            End Property

            ''' <summary>Gets the current value of the property on a component.</summary>
            ''' <returns>The value of a property for a given component.</returns>
            ''' <param name="component">The component with the property for which to retrieve the value. </param>
            Public Overrides Function GetValue(ByVal component As Object) As Object
                Return Me.Original.GetValue(component)
            End Function

            ''' <summary>Gets a value indicating whether this property is read-only.</summary>
            ''' <returns>This implementation always return true.</returns>
            Public Overrides ReadOnly Property IsReadOnly() As Boolean
                Get
                    Return True
                End Get
            End Property

            ''' <summary>Gets the type of the property.</summary>
            ''' <returns>A <see cref="T:System.Type" /> that represents the type of the property.</returns>
            Public Overrides ReadOnly Property PropertyType() As System.Type
                Get
                    Return Me.Original.PropertyType
                End Get
            End Property

            ''' <summary>Resets the value for this property of the component to the default value.</summary>
            ''' <param name="component">The component with the property value that is to be reset to the default value. </param>
            ''' <remarks>This implementation does nothing.</remarks>
            Public Overrides Sub ResetValue(ByVal component As Object)
                'Do nothing
            End Sub

            ''' <summary>When overridden in a derived class, sets the value of the component to a different value.</summary>
            ''' <param name="component">The component with the property value that is to be set. </param>
            ''' <param name="value">The new value. </param>
            ''' <remarks>This implementation does nothing.</remarks>
            Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
                'Do nothing
            End Sub

            ''' <summary>When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.</summary>
            ''' <returns>true if the property should be persisted; otherwise, false.</returns>
            ''' <param name="component">The component with the property to be examined for persistence. </param>
            Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
                Return Me.Original.ShouldSerializeValue(component)
            End Function
        End Class
    End Class
End Namespace
#End If