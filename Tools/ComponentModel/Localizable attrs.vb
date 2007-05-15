Imports System.ComponentModel
#If Config <= RC Then 'Stage: RC
Namespace ComponentModel
    ''' <summary>Localizable version of <see cref="DescriptionAttribute"/>. Defines description shown in <see cref="System.Windows.Forms.PropertyGrid"/>.</summary>
    ''' <remarks>Localizable means that value can be loaded from resources (any Public Static (Shared in Visual Basic) Property).</remarks>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, LastChMMDDYYYY:="01/21/2007")> _
    Public Class LDescriptionAttribute : Inherits DescriptionAttribute
        ''' <summary>Contains value of the <see cref="Resource"/> property</summary>
        Private _Resource As Type
        ''' <summary>Contains value of the <see cref="PropertyName"/> property</summary>
        Private _PropertyName As String
        ''' <summary>CTor</summary>
        ''' <param name="Resource"><see cref="Type"/> of type that contains property with name specified in the <paramref name="PropertyName"/> parameter. Though it is assumed that type is resource, it is not necessary.</param>
        ''' <param name="PropertyName">Name of Static (Shared in Visual Basic) property of type specified in <paramref name="Resource"/>. It is not necessary for the property to be public. Return type must be <see cref="String"/>.</param>
        ''' <param name="AlternativeValue">Alternative value used when property cannot be invoked</param>
        Public Sub New(ByVal Resource As Type, ByVal PropertyName As String, Optional ByVal AlternativeValue As String = "")
            MyBase.New(AlternativeValue)
            _Resource = Resource
            _PropertyName = PropertyName
        End Sub
        ''' <summary>Gets the description stored in this attribute.</summary>
        ''' <returns>The description stored in property with name stored in <see cref="PropertyName"/> of type stored in <see cref="Resource"/>. If this failt returns alternative value if specified.</returns>
        Public Overrides ReadOnly Property Description() As String
            Get
                Try
                    Return Resource.GetProperty(PropertyName, Reflection.BindingFlags.Static Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.NonPublic, Type.DefaultBinder, GetType(String), New Type() {}, Nothing).GetValue(Nothing, Nothing)
                Catch
                    Return MyBase.Description
                End Try
            End Get
        End Property
        ''' <summary>Data type that contains property with name specified in the <see cref="PropertyName"/> property.</summary>
        ''' <remarks>Theere is no need for the data type to be public.</remarks>
        Public Overridable ReadOnly Property Resource() As Type
            Get
                Return _Resource
            End Get
        End Property
        ''' <summary>Name of Static (Shared in Visual Basic) property of type specified in <see cref="Resource"/></summary>
        ''' <remarks>
        ''' The property doesn't need to pe public.
        ''' Return type of the property must be <see cref="String"/>
        ''' </remarks>
        Public Overridable ReadOnly Property PropertyName() As String
            Get
                Return _PropertyName
            End Get
        End Property
    End Class

    ''' <summary>Localizable version of <see cref="DisplayNameAttribute"/>. Defines name shown in <see cref="System.Windows.Forms.PropertyGrid"/>.</summary>
    ''' <remarks>Localizable means that value can be loaded from resources.</remarks>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, LastChMMDDYYYY:="01/21/2007")> _
    Public Class LDisplayNameAttribute : Inherits DisplayNameAttribute
        ''' <summary>Contains value of the <see cref="Resource"/> property</summary>
        Private _Resource As Type
        ''' <summary>Contains value of the <see cref="PropertyName"/> property</summary>
        Private _PropertyName As String
        ''' <summary>CTor</summary>
        ''' <param name="Resource"><see cref="Type"/> of type that contains property with name specified in the <paramref name="PropertyName"/> parameter. Though it is assumed that type is resource, it is not necessary.</param>
        ''' <param name="PropertyName">Name of Static (Shared in Visual Basic) property of type specified in <paramref name="Resource"/>. It is not necessary for the property to be public. Return type must be <see cref="String"/>.</param>
        ''' <param name="AlternativeValue">Alternative value used when property cannot be invoked</param>
        Public Sub New(ByVal Resource As Type, ByVal PropertyName As String, Optional ByVal AlternativeValue As String = "")
            MyBase.New(AlternativeValue)
            _Resource = Resource
            _PropertyName = PropertyName
        End Sub
        ''' <summary>Gets the display name for a property, event, or public void method that takes no arguments stored in this attribute.</summary>
        ''' <returns>The display name  stored in property with name stored in <see cref="PropertyName"/> of type stored in <see cref="Resource"/>. If this failt returns alternative value if specified.</returns>
        Public Overrides ReadOnly Property DisplayName() As String
            Get
                Try
                    Return Resource.GetProperty(PropertyName, Reflection.BindingFlags.Static Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.NonPublic, Type.DefaultBinder, GetType(String), New Type() {}, Nothing).GetValue(Nothing, Nothing)
                Catch
                    Return MyBase.DisplayName
                End Try
            End Get
        End Property
        ''' <summary>Data type that contains property with name specified in the <see cref="PropertyName"/> property.</summary>
        ''' <remarks>Theere is no need for the data type to be public.</remarks>
        Public Overridable ReadOnly Property Resource() As Type
            Get
                Return _Resource
            End Get
        End Property
        ''' <summary>Name of Static (Shared in Visual Basic) property of type specified in <see cref="Resource"/></summary>
        ''' <remarks>
        ''' The property doesn't need to pe public.
        ''' Return type of the property must be <see cref="String"/>
        ''' </remarks>
        Public Overridable ReadOnly Property PropertyName() As String
            Get
                Return _PropertyName
            End Get
        End Property
    End Class
    ''' <summary>Localizable version of <see cref="CategoryAttribute"/>. Defines category shown in <see cref="System.Windows.Forms.PropertyGrid"/>.</summary>
    ''' <remarks>
    ''' Localizable means that value can be loaded from resources (any Public Static (Shared in Visual Basic) Property).
    ''' Note that some categories can be localized by .NET Framework itself.
    ''' </remarks>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, LastChMMDDYYYY:="01/21/2007")> _
    Public Class LCategoryAttribute : Inherits CategoryAttribute
        ''' <summary>Contains value of the <see cref="Resource"/> property</summary>
        Private _Resource As Type
        ''' <summary>Contains value of the <see cref="PropertyName"/> property</summary>
        Private _PropertyName As String
        ''' <summary>Contains value of the <see cref="LookUpOrder"/> property</summary>
        Private _LookUpOrder As enmLookUpOrder
        ''' <summary>Stores alternative value to be returned when getting value from <see cref="Resource"/> fails.</summary>
        Private _AlternativeValue As String
        ''' <summary>CTor</summary>
        ''' <param name="Resource"><see cref="Type"/> of type that contains property with name specified in the <paramref name="PropertyName"/> parameter. Though it is assumed that type is resource, it is not necessary.</param>
        ''' <param name="PropertyName">Name of Static (Shared in Visual Basic) property of type specified in <paramref name="Resource"/>. It is not necessary for the property to be public. Return type must be <see cref="String"/>. The property shouldn't return an empty string.</param>
        ''' <param name="AlternativeValue">
        ''' Alternative value used when property cannot be invoked.
        ''' This value is also used when lookup order <see cref="enmLookUpOrder.NETFirst"/> (or <see cref="enmLookUpOrder.ResourceFirst"/> and no resource is found) as value to be localized by the .NET Framework.
        ''' </param>
        ''' <param name="LookupOrder">Defines order of sources of localized string</param>
        Public Sub New(ByVal Resource As Type, ByVal PropertyName As String, ByVal AlternativeValue As String, Optional ByVal LookupOrder As enmLookUpOrder = enmLookUpOrder.ResourceFirst)
            MyBase.New(AlternativeValue)
            _AlternativeValue = AlternativeValue
            _Resource = Resource
            _PropertyName = PropertyName
            _LookUpOrder = LookupOrder
        End Sub
        ''' <summary>Possible orders of source of localized string</summary>
        ''' <remarks>Determines the behavior of the <see cref="GetLocalizedString"/> function.</remarks>
        Public Enum enmLookUpOrder
            ''' <summary>
            ''' The <see cref="GetLocalizedString"/> function looks in resource specified in the <see cref="Resource"/> first.
            ''' If Static (Shared in Visual Basic) <see cref="String"/> property with name <see cref="PropertyName"/> is not found then return result of <see cref="CategoryAttribute.GetLocalizedString"/>.
            ''' </summary>
            ResourceFirst
            ''' <summary>
            ''' The <see cref="GetLocalizedString"/> function first calls <see cref="CategoryAttribute.GetLocalizedString"/>.
            ''' If the result is an empty string then looks for Static (Shared in Visual Basic) <see cref="String"/> property with name spacifiedn in <see cref="PropertyName"/> of type specified in <see cref="Resource"/> and returns its value of found or an empty string if not.
            ''' </summary>
            NETFirst
            ''' <summary>The <see cref="GetLocalizedString"/> function looks only for Static (Shared in Visual Basic) <see cref="String"/> property with name specified in <see cref="PropertyName"/> of type specified in <see cref="Resource"/>. If the property is found then returns its value otherwise returns an empty string.</summary>
            ResourceOnly
        End Enum
        ''' <summary>Looks up the localized name of the specified category.</summary>
        ''' <param name="value">The identifer for the category to look up.</param>
        ''' <returns>The localized name of the category, or null if a localized name does not exist.</returns>
        ''' <remarks>
        ''' The behavior of this function is affected by value of the <see cref="LookUpOrder"/> property.
        ''' The <paramref name="value"/> must have the same value as alternative value passed to the CTor otherwise <see cref="CategoryAttribute.GetLocalizedString"/> is returned.
        ''' <list>
        ''' <item>
        ''' If <see cref="LookUpOrder"/> is <see cref="enmLookUpOrder.NETFirst"/> then
        ''' Function returns value of <see cref="CategoryAttribute.GetLocalizedString"/> if it is not an empty string. Othervise returns value of property specified by <see cref="PropertyName"/> of type specified by <see cref="Resource"/>. If obtaining this value fails returns an empty string.
        ''' </item>
        ''' <item>
        ''' If <see cref="LookUpOrder"/> is <see cref="enmLookUpOrder.ResourceOnly"/> then
        ''' Function returns value of property specified by <see cref="PropertyName"/> of type specified by <see cref="Resource"/>. If obtaining this value failf an empty string is returned.
        ''' </item>
        ''' <item>
        ''' If <see cref="LookUpOrder"/> is <see cref="enmLookUpOrder.ResourceFirst"/> (or other value not mentioned here) then
        ''' Function returns value of property specified by <see cref="PropertyName"/> of type specified by <see cref="Resource"/>. If obraining this value fails the resilt of <see cref="CategoryAttribute.GetLocalizedString"/> is returned.
        ''' </item>
        ''' </list>
        ''' If this function is going to return an empty string (eg. because of resource lookup failure) it returns alternative value specified in CTor instead.
        ''' </remarks>
        Protected Overrides Function GetLocalizedString(ByVal value As String) As String
            GetLocalizedString = ""
            Try
                If _AlternativeValue = value Then
                    Select Case LookUpOrder
                        Case enmLookUpOrder.NETFirst
                            Dim ret As String = MyBase.GetLocalizedString(value)
                            If ret = "" Then
                                Try
                                    Return Resource.GetProperty(PropertyName, Reflection.BindingFlags.Static Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.NonPublic, Type.DefaultBinder, GetType(String), New Type() {}, Nothing).GetValue(Nothing, Nothing)
                                Catch
                                    Return ""
                                End Try
                            Else
                                Return ret
                            End If
                        Case enmLookUpOrder.ResourceOnly
                            Try
                                Return Resource.GetProperty(PropertyName, Reflection.BindingFlags.Static Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.NonPublic, Type.DefaultBinder, GetType(String), New Type() {}, Nothing).GetValue(Nothing, Nothing)
                            Catch
                                Return ""
                            End Try
                        Case Else 'enmLookUpOrder.ResourceFirst 
                            Dim ret As String = ""
                            Try
                                ret = Resource.GetProperty(PropertyName, Reflection.BindingFlags.Static Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.NonPublic, Type.DefaultBinder, GetType(String), New Type() {}, Nothing).GetValue(Nothing, Nothing)
                            Catch : End Try
                            If ret = "" Then
                                Return MyBase.GetLocalizedString(value)
                            Else
                                Return ret
                            End If
                    End Select
                Else
                    Return MyBase.GetLocalizedString(value)
                End If
            Finally
                If GetLocalizedString = "" Then GetLocalizedString = _AlternativeValue
            End Try
        End Function
        ''' <summary>Data type that contains property with name specified in the <see cref="PropertyName"/> property.</summary>
        ''' <remarks>Theere is no need for the data type to be public.</remarks>
        Public Overridable ReadOnly Property Resource() As Type
            Get
                Return _Resource
            End Get
        End Property
        ''' <summary>Defines the order of sources for looking for value of this property</summary>
        Public Overridable ReadOnly Property LookUpOrder() As enmLookUpOrder
            Get
                Return _LookUpOrder
            End Get
        End Property
        ''' <summary>Name of Static (Shared in Visual Basic) property of type specified in <see cref="Resource"/></summary>
        ''' <remarks>
        ''' The property doesn't need to pe public.
        ''' Return type of the property must be <see cref="String"/>.
        ''' The property should not return an empty string becose empty string is treated as failure of the property by the <see cref="GetLocalizedString"/> function.
        ''' </remarks>
        Public Overridable ReadOnly Property PropertyName() As String
            Get
                Return _PropertyName
            End Get
        End Property
    End Class

    ''' <summary>Localizable version of <see cref="DefaultValueAttribute"/>. Defines default value of property. Used by <see cref="System.Windows.Forms.PropertyGrid"/> to visually indicate user that value was changed and by Windows Forms Designer to determine if property should be serialized or not.</summary>
    ''' <remarks>
    ''' Localizable means that value can be loaded from resources (any Public Static (Shared in Visual Basic) Property).
    ''' This attribute can be used in simple cases. In more complicated cases use ShouldSerialize... and Reset... methods. <seealso>http://msdn2.microsoft.com/en-us/library/53b8022e.aspx</seealso>
    ''' </remarks>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, LastChMMDDYYYY:="01/25/2007")> _
    Public Class LDefaultValueAttribute : Inherits DefaultValueAttribute
        ''' <summary>Contains value of the <see cref="Resource"/> property</summary>
        Private _Resource As Type
        ''' <summary>Contains value of the <see cref="[Property]"/> property</summary>
        Private _Property As String
        ''' <summary>Type of default value</summary>
        Private Type As Type
        ''' <summary>CTor - only for default values of <see cref="String"/> type</summary>
        ''' <param name="Resource">Type that contains property with name spacified in <paramref name="Property"/></param>
        ''' <param name="Property">Name of Static (Shared in Visual Basic) Public property of type specified in <paramref name="Resource"/>. This property cannot be indexed.</param>
        ''' <param name="Alternative">Alternative value used when obrainin from <paramref name="Resource"/> fails</param>
        Public Sub New(ByVal Resource As Type, ByVal [Property] As String, Optional ByVal Alternative As String = "")
            MyBase.New(Alternative)
            Me.Resource = Resource
            Me.Property = [Property]
            Type = GetType(String)
        End Sub
        ''' <summary>CTor - only for default values of any type</summary>
        ''' <param name="Resource">Type that contains property with name spacified in <paramref name="Property"/></param>
        ''' <param name="Property">Name of Static (Shared in Visual Basic) Public property of type specified in <paramref name="Resource"/>. This property cannot be indexed.</param>
        ''' <param name="Type">Type of default value</param>
        ''' <param name="Alternative">Alternative value used when obrainin from <paramref name="Resource"/> fails</param>
        Public Sub New(ByVal Resource As Type, ByVal [Property] As String, ByVal Type As Type, Optional ByVal Alternative As String = "")
            MyBase.New(Type, Alternative)
            Me.Resource = Resource
            Me.Property = [Property]
            Type = Type
        End Sub
        ''' <summary>Gets or sets type tah contains property named with name specified in the <see cref="[Property]"/> property</summary>
        Public Property Resource() As Type
            Get
                Return _Resource
            End Get
            Set(ByVal value As Type)
                _Resource = value
            End Set
        End Property
        ''' <summary>Specifies name of Static (Shared in Visual Basic) Public property of type specified in the <see cref="Resource"/> property. This property returns the default value returned.</summary>
        ''' <remarks>Property cannot be indexed (event with optional index)</remarks>
        Public Property [Property]() As String
            Get
                Return _Property
            End Get
            Set(ByVal value As String)
                _Property = value
            End Set
        End Property
        ''' <summary>Gets the default value of the property this attribute is bound to.</summary>
        ''' <returns>An <see cref="System.Object"/> that represents the default value of the property this attribute is bound to.</returns>
        Public Overrides ReadOnly Property Value() As Object
            Get
                Try
                    Return Resource.GetProperty([Property], Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static Or Reflection.BindingFlags.GetProperty, Type.DefaultBinder, Type, New Type() {}, Nothing).GetValue(Nothing, Nothing)
                Catch
                    Return MyBase.Value
                End Try
            End Get
        End Property
    End Class
End Namespace
#End If