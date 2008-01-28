Imports System.Runtime.CompilerServices
Imports System.Reflection
Imports System.Linq
'ASAP:
Namespace ReflectionT
    ''' <summary>Various reflection tools</summary>
    Public Module ReflectionTools
        ''' <summary>Gets namespaces in given module</summary>
        ''' <param name="Module">Module to get namespaces in</param>
        ''' <returns>Array of namespaces in <paramref name="Module"/></returns>
        ''' <param name="IncludeGlobal">True to include global namespace (with empty name)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Module"/> is null</exception>
        <Extension()> Public Function GetNamespaces(ByVal [Module] As Reflection.Module, Optional ByVal IncludeGlobal As Boolean = False) As [NamespaceInfo]()
            If [Module] Is Nothing Then Throw New ArgumentNullException("Module")
            Dim NamespaceNames As New List(Of String)
            Dim Namespaces As New List(Of NamespaceInfo)
            For Each Type In [Module].GetTypes
                If (Type.Namespace <> "" OrElse IncludeGlobal) AndAlso Not NamespaceNames.Contains(Type.Namespace) Then
                    NamespaceNames.Add(Type.Namespace)
                    Namespaces.Add(New NamespaceInfo([Module], Type.Namespace))
                End If
            Next Type
            Return Namespaces.ToArray()
        End Function
        ''' <summary> defined in given module</summary>
        ''' <param name="Module">Module to get types from</param>
        ''' <param name="FromNamespaces">True to get only types from global namespace. False to get all types (same as <see cref="[Module].GetTypes"/>)</param>
        ''' <returns>Array of types from module <paramref name="Module"/>.</returns>
        <Extension()> Public Function GetTypes(ByVal [Module] As [Module], ByVal FromNamespaces As Boolean) As Type()
            If FromNamespaces Then Return [Module].GetTypes
            Return (From Type In [Module].GetTypes Where Type.Namespace = "" Select Type).ToArray
        End Function

        <Extension()> Public Function IsPublic(ByVal Member As MemberInfo) As Boolean
            Select Case Member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(Member, Type)
                        Return (Not .IsNested AndAlso .IsPublic) OrElse (.IsNested AndAlso .IsNestedPublic)
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(Member, MethodBase).IsPublic
                Case MemberTypes.Event
                    With DirectCast(Member, EventInfo)
                        Return (.GetAddMethod IsNot Nothing AndAlso .GetAddMethod.IsPublic) OrElse (.GetRemoveMethod IsNot Nothing AndAlso .GetRemoveMethod.IsPublic) OrElse (.GetRaiseMethod IsNot Nothing AndAlso .GetRaiseMethod.IsPublic)
                    End With
                Case MemberTypes.Field : Return DirectCast(Member, FieldInfo).IsPublic
                Case MemberTypes.Property
                    With DirectCast(Member, PropertyInfo)
                        Return (.GetGetMethod IsNot Nothing AndAlso .GetGetMethod.IsPublic) OrElse (.GetSetMethod IsNot Nothing AndAlso .GetSetMethod.IsPublic)
                    End With
                Case Else : Return False
            End Select
        End Function
        <Extension()> Public Function IsPrivate(ByVal Member As MemberInfo) As Boolean
            Select Case Member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(Member, Type)
                        Return .IsNested AndAlso .IsNestedPrivate
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(Member, MethodBase).IsPrivate
                Case MemberTypes.Event
                    With DirectCast(Member, EventInfo)
                        Return (.GetAddMethod IsNot Nothing AndAlso .GetAddMethod.IsPrivate) AndAlso (.GetRemoveMethod IsNot Nothing AndAlso .GetRemoveMethod.IsPrivate) OrElse (.GetRaiseMethod IsNot Nothing AndAlso .GetRaiseMethod.IsPrivate)
                    End With
                Case MemberTypes.Field : Return DirectCast(Member, FieldInfo).IsPrivate
                Case MemberTypes.Property
                    With DirectCast(Member, PropertyInfo)
                        Return (.GetGetMethod IsNot Nothing AndAlso .GetGetMethod.IsPrivate) OrElse (.GetSetMethod IsNot Nothing AndAlso .GetSetMethod.IsPrivate)
                    End With
                Case Else : Return False
            End Select
        End Function
    End Module
    ''' <summary>Represents reflection namespace</summary>
    Public Class NamespaceInfo
        ''' <summary>Contains value of the <see cref="[Module]"/> property</summary>
        Private ReadOnly _Module As [Module]
        ''' <summary>Contains value of the <see cref="Name"/> property</summary>
        Private ReadOnly _Name As String
        ''' <summary>Module the namespace is located in</summary>
        Public ReadOnly Property [Module]() As [Module]
            Get
                Return _Module
            End Get
        End Property
        ''' <summary>Name of namespace. Can be an empty string for global namespace</summary>
        Public ReadOnly Property Name$()
            Get
                Return _Name
            End Get
        End Property
        ''' <summary>CTor</summary>
        ''' <param name="Module">Module the namespace is defined in</param>
        ''' <param name="Name">Name of namespace</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Module"/> or <paramref name="Name"/> is null</exception>
        Public Sub New(ByVal [Module] As [Module], ByVal Name As String)
            If [Module] Is Nothing Then Throw New ArgumentNullException("Module")
            If Name Is Nothing Then Throw New ArgumentNullException("Name")
            Me._Module = [Module]
            Me._Name = Name
        End Sub
        ''' <summary>s located within current namespace</summary>
        ''' <returns>Array of types defined in this namespace</returns>
        Public Function GetTypes(Optional ByVal Nested As Boolean = False) As Type()
            Return (From Type In Me.Module.GetTypes() Where (Nested OrElse Not Type.IsNested) AndAlso Type.Namespace = Me.Name Select Type).ToArray
        End Function
    End Class
End Namespace