Imports System.ComponentModel
#If Config <= Nightly Then 'Stage: Nightly
Namespace ComponentModel
    ''' <summary>Localizable version of <see cref="DescriptionAttribute"/>. Defines description shown in <see cref="System.Windows.Forms.PropertyGrid"/>.</summary>
    ''' <remarks>Localizable means that value can be loaded from resources.</remarks>
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
        ''' <returns>The description stored in this attribute</returns>
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
    Public Class LDisplayNameAttribute : Inherits DisplayNameAttribute

    End Class
    ''' <summary>Localizable version of <see cref="CategoryAttribute"/>. Defines category shown in <see cref="System.Windows.Forms.PropertyGrid"/>.</summary>
    ''' <remarks>
    ''' Localizable means that value can be loaded from resources.
    ''' Note that some categories are translated from english into local language by <see cref="System.Windows.Forms.PropertyGrid"/> automatically.
    ''' </remarks>
    Public Class LCategoryAttribute : Inherits CategoryAttribute

    End Class
End Namespace
#End If