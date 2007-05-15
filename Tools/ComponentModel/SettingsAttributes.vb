Imports System.ComponentModel, System.Configuration, Tools.VisualBasic
#If Config <= RC Then 'Stage: RC
Namespace ComponentModel
    ''' <summary><see cref="DescriptionAttribute"/> that takes its value from <see cref="System.Configuration.SettingsDescriptionAttribute"/></summary>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, LastChMMDDYYYY:="01/21/2007")> _
    Public Class SettingsInheritDescriptionAttribute : Inherits DescriptionAttribute
        ''' <summary>CTor</summary>
        ''' <param name="Settings">The data type that contains property with name specified in <paramref name="Property"/></param>
        ''' <param name="Property">Name of the property which's <see cref="SettingsDescriptionAttribute"/> initializes this attribute</param>
        ''' <param name="AlternateDescription">Alternative description used in case of failure of getting description form specified property</param>
        Public Sub New(ByVal Settings As Type, ByVal [Property] As String, Optional ByVal AlternateDescription As String = "")
            MyBase.New(iif(AlternateDescription = "", [Property], AlternateDescription))
            Me.Settings = Settings
            Me.Property = [Property]
        End Sub
        ''' <summary>The data type that contains property with name spacified in <see cref="[Property]"/></summary>
        Private Settings As Type
        ''' <summary>Name of the property which's <see cref="SettingsDescriptionAttribute"/> initializes this attribute</summary>
        Private [Property] As String
        ''' <summary>Gets or sets the string stored as the description.</summary>
        ''' <returns>The string stored as the description. The default value is an empty string ("").</returns>
        Public Overrides ReadOnly Property Description() As String
            Get
                Dim sds As Object() = Settings.GetProperty([Property]).GetCustomAttributes(GetType(SettingsDescriptionAttribute), True)
                If sds IsNot Nothing AndAlso sds.Length > 0 Then
                    Return CType(sds(0), SettingsDescriptionAttribute).Description
                Else
                    Return MyBase.DescriptionValue
                End If
            End Get
        End Property
    End Class
    ''' <summary><see cref="DefaultValueAttribute"/> that takes its value from <see cref="System.Configuration.DefaultSettingValueAttribute"/></summary>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, LastChMMDDYYYY:="01/25/2007")> _
    Public Class SettingsInheritDefaultValueAttribute : Inherits DefaultValueAttribute
        ''' <summary>CTor</summary>
        ''' <param name="Settings">The data type that contains property with name defined in <paramref name="Property"/></param>
        ''' <param name="Property">Name of property from which's <see cref="DefaultSettingValueAttribute"/> this attribute is initialized</param>
        ''' <param name="Type">The data type of the value</param>
        ''' <param name="AlternateDefaultValue">Alternative default value used when fetching fails</param>
        Public Sub New(ByVal Settings As Type, ByVal [Property] As String, ByVal Type As Type, Optional ByVal AlternateDefaultValue As String = "")
            MyBase.New(Type, AlternateDefaultValue)
            Me.Settings = Settings
            Me.Property = [Property]
            Me.ValueType = Type
        End Sub
        ''' <summary>CTor for default values of <see cref="String"/> type</summary>
        ''' <param name="Settings">The data type that contains property with name defined in <paramref name="Property"/></param>
        ''' <param name="Property">Name of property from which's <see cref="DefaultSettingValueAttribute"/> this attribute is initialized</param>
        Public Sub New(ByVal Settings As Type, ByVal [Property] As String)
            Me.New(Settings, [Property], GetType(String))
        End Sub
        ''' <summary>Contains value of the <see cref="Settings"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Settings As Type
        ''' <summary>Contains value of the <see cref="[Property]"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private [_Property] As String
        ''' <summary>Contains value of the <see cref="ValueType"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ValueType As Type
        ''' <summary>Gets the default value of the property this attribute is bound to.</summary>
        ''' <returns>An <see cref="System.Object"/> that represents the default value of the property this attribute is bound to.</returns>
        ''' <remarks>Default values can be obtained if stored in form that can be directly returned or if stored as XML-serialized values.</remarks>
        Public Overrides ReadOnly Property Value() As Object
            Get
                Dim sds As Object() = Settings.GetProperty([Property]).GetCustomAttributes(GetType(DefaultSettingValueAttribute), True)
                If sds IsNot Nothing AndAlso sds.Length > 0 Then
                    Try
                        Dim mySerializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(ValueType)
                        Dim stream As New System.IO.StringReader(CType(sds(0), DefaultSettingValueAttribute).Value)
                        Return mySerializer.Deserialize(stream)
                    Catch
                        Dim a As New DefaultValueAttribute(ValueType, CType(sds(0), DefaultSettingValueAttribute).Value)
                        Return a.Value
                    End Try
                Else
                    Return MyBase.Value
                End If
            End Get
        End Property
        ''' <summary>The data type that contains property with name defined in <see cref="[Property]"/></summary>
        Public Property Settings() As Type
            Get
                Return _Settings
            End Get
            Protected Set(ByVal value As Type)
                _Settings = value
            End Set
        End Property
        ''' <summary>Name of property from which's <see cref="DefaultSettingValueAttribute"/> this attribute is initialized</summary>
        Public Property [Property]() As String
            Get
                Return [_Property]
            End Get
            Protected Set(ByVal value As String)
                [_Property] = value
            End Set
        End Property
        ''' <summary>The data type of the value</summary>
        Public Property ValueType() As Type
            Get
                Return _ValueType
            End Get
            Protected Set(ByVal value As Type)
                _ValueType = value
            End Set
        End Property
    End Class
End Namespace
#End If