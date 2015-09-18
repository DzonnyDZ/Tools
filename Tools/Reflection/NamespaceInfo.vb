Imports System.Runtime.CompilerServices, Tools.ExtensionsT
Imports System.Reflection
Imports System.Linq, Tools.LinqT
Imports System.Runtime.InteropServices

#If True
Namespace ReflectionT

    ''' <summary>Represents reflection namespace</summary>
    ''' <remarks>
    ''' This class implements interface <see cref="ICustomAttributeProvider"/>. Though it never returns anny custom attributes for a namespace.
    ''' The interface is implemented because it's only common interface common for all code objects ant thus it's natural that namespace implements it, so instance of this class can be passed wherever any other code item.
    ''' <note>CIL metadata does not support custom attributes on namespaces. In fact namespace is not official CIL instruction. However Microsft implementation allows <c>.custom</c> on <c>.namespace</c> but it is AFAIK ignored.</note>
    ''' </remarks>
    ''' <version version="1.5.2" stage="Nightly">Added implementation of <see cref="IEquatable(Of NamespaceInfo)"/></version>
    ''' <version version="1.5.4">Added implementation of <see cref="ICustomAttributeProvider"/></version>
    Public Class NamespaceInfo
        Implements IEquatable(Of NamespaceInfo)
        Implements ICustomAttributeProvider
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
        ''' <summary>Short name of namespace - only part after last dot (.).</summary>
        Public ReadOnly Property ShortName$()
            Get
                If Name = "" Then Return ""
                Dim Parts As String() = Name.Split("."c)
                Return Parts(Parts.Length - 1)
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
#Region "GetMembers"
        ''' <summary>Gets types located within current namespace</summary>
        ''' <param name="Nested">True to get nested types (types declared inside types in current namepace)</param>
        ''' <returns>Array of types defined in this namespace</returns>
        ''' <exception cref="System.Reflection.ReflectionTypeLoadException">One or more classes in a module could not be loaded.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        Public Function GetTypes(Optional ByVal Nested As Boolean = False) As Type()
            Return (From Type In Me.Module.GetTypes() Where (Nested OrElse Not Type.IsNested) AndAlso Type.Namespace = Me.Name Select Type).ToArray
        End Function
        ''' <summary>Gets global methods located in current namespace</summary>
        ''' <returns>Array of global methods defined in current namespace (it is in module <see cref="[Module]"/> with name starting with name of this namespace)</returns>
        ''' <version version="1.5.2">Function introduced</version>
        Public Function GetMethods() As MethodInfo()
            Return (From method In Me.Module.GetMethods Where method.GetNamespace.Equals(Me)).ToArray
        End Function
        ''' <summary>Gets global methods located in current namespace</summary>
        ''' <returns>Array of global methods defined in current namespace (it is in module <see cref="[Module]"/> with name starting with name of this namespace)</returns>
        ''' <version version="1.5.2">Function introduced</version>
        Public Function GetFields() As FieldInfo()
            Return (From field In Me.Module.GetFields Where field.GetNamespace.Equals(Me)).ToArray
        End Function

        ''' <summary>Gets global methods located in current namespace</summary>
        ''' <returns>Array of global methods defined in current namespace (it is in module <see cref="[Module]"/> with name starting with name of this namespace)</returns>
        ''' <param name="BindingFlags">A bitwise combination of <see cref="System.Reflection.BindingFlags"/> values that limit the search.</param>
        ''' <version version="1.5.2">Function introduced</version>
        Public Function GetMethods(ByVal BindingFlags As BindingFlags) As MethodInfo()
            Return (From method In Me.Module.GetMethods(BindingFlags) Where method.GetNamespace.Equals(Me)).ToArray
        End Function
        ''' <summary>Gets global methods located in current namespace</summary>
        ''' <returns>Array of global methods defined in current namespace (it is in module <see cref="[Module]"/> with name starting with name of this namespace)</returns>
        ''' <param name="BindingFlags">A bitwise combination of <see cref="System.Reflection.BindingFlags"/> values that limit the search.</param>
        ''' <version version="1.5.2">Function introduced</version>
        Public Function GetFields(ByVal BindingFlags As BindingFlags) As FieldInfo()
            Return (From field In Me.Module.GetFields(BindingFlags) Where field.GetNamespace.Equals(Me)).ToArray
        End Function
#End Region
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>True if <paramref name="obj"/> is <see cref="NamespaceInfo"/> and its <see cref="[Module]"/> equals to <see cref="[Module]"/> of current <see cref="NamespaceInfo"/> and also <see cref="Name">Names</see> or current <see cref="NamespaceInfo"/> and <paramref name="obj"/> equals.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        ''' <exception cref="T:System.NullReferenceException">The 
        ''' <paramref name="obj" /> parameter is null.</exception>
        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            Return TypeOf obj Is NamespaceInfo AndAlso Me.Module.Equals(DirectCast(obj, NamespaceInfo).Module) AndAlso Me.Name = DirectCast(obj, NamespaceInfo).Name
        End Function
        ''' <summary>Compares two <see cref="NamespaceInfo">NamespaceInfos</see> for equality</summary>
        ''' <param name="a">A <see cref="NamespaceInfo"/></param>
        ''' <param name="b">A <see cref="NamespaceInfo"/></param>
        ''' <returns>True if <paramref name="a"/> equals to <paramref name="b"/>.</returns>
        Public Shared Operator =(ByVal a As NamespaceInfo, ByVal b As NamespaceInfo) As Boolean
            Return a.Equals(b)
        End Operator
        ''' <summary>Compares two <see cref="NamespaceInfo">NamespaceInfos</see> for inequality</summary>
        ''' <param name="a">A <see cref="NamespaceInfo"/></param>
        ''' <param name="b">A <see cref="NamespaceInfo"/></param>
        ''' <returns>False if <paramref name="a"/> equals to <paramref name="b"/>.</returns>
        Public Shared Operator <>(ByVal a As NamespaceInfo, ByVal b As NamespaceInfo) As Boolean
            Return Not (a = b)
        End Operator
        ''' <summary>Gets parent namespace of current namespace</summary>
        ''' <returns>If <see cref="Name"/> of current namespace contains no dot an namespace with empty name is returned. If <see cref="Name"/> of current namespace is an empty string, null is returned.</returns>
        Public ReadOnly Property Parent() As NamespaceInfo
            Get
                If Me.Name = "" Then
                    Return Nothing
                ElseIf Me.Name.Contains("."c) Then
                    Return New NamespaceInfo(Me.Module, Me.Name.Substring(0, Me.Name.LastIndexOf("."c)))
                Else
                    Return New NamespaceInfo(Me.Module, "")
                End If
            End Get
        End Property
        ''' <summary>Gets all namespaces immediately contained in this namespace</summary>
        ''' <returns>Array of namespaces in this namespace</returns>
        ''' <remarks>Whe looking for namespaces all types in curret namespace are considered (even non-public). 
        ''' If you want filer some types use overloaded <see cref="M:Tools.ReflectionT.NamespaceInfo.GetNamespaces(System.Predicate`1[System.Type])"/>.</remarks>
        Public Function GetNamespaces() As NamespaceInfo()
            Return GetNamespaces(Function(t As Type) True)
        End Function
        ''' <summary>Gets namespaces immediatelly contained in this namespace when considering only selected types</summary>
        ''' <param name="TypeFiler">This function returns only such namespaces which contain at leas one type for which delegate function <paramref name="TypeFiler"/> returns true</param>
        ''' <returns>Array of namespaces in this namespace</returns>
        Public Function GetNamespaces(ByVal TypeFiler As System.Predicate(Of Type)) As NamespaceInfo()
            Dim NamespaceNames As New List(Of String)
            For Each t As Type In [Module].GetTypes
                If t.Namespace <> "" AndAlso (Me.Name = "" OrElse t.Namespace.StartsWith(Me.Name & ".")) Then
                    Dim NamePart As String
                    If Me.Name = "" AndAlso t.Namespace.Contains("."c) Then
                        NamePart = t.Namespace.Substring(0, t.Namespace.IndexOf("."c))
                    ElseIf Me.Name = "" Then
                        NamePart = t.Namespace
                    Else
                        NamePart = t.Namespace.Substring(Me.Name.Length + 1)
                        If NamePart.Contains("."c) Then NamePart = NamePart.Substring(0, NamePart.IndexOf("."c))
                    End If
                    If Not NamespaceNames.Contains(NamePart) AndAlso TypeFiler.Invoke(t) Then NamespaceNames.Add(NamePart)
                End If
            Next t
            Return (From np In NamespaceNames _
                Order By np _
                Select New NamespaceInfo(Me.Module, If(Me.Name = "", np, Me.Name & "." & np)) _
                ).ToArray
        End Function

        ''' <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        ''' <returns>true if the current object is equal to the 
        ''' <paramref name="other" /> parameter; otherwise, false.</returns>
        ''' <param name="other">An object to compare with this object.</param>
        ''' <version version="1.5.2">Function added</version>
        Public Overloads Function Equals(ByVal other As NamespaceInfo) As Boolean Implements System.IEquatable(Of NamespaceInfo).Equals
            Return Me.Equals(CObj(other))
        End Function

#Region "ICustomAttributeProvider"
        ''' <summary>Returns an array of all of the custom attributes defined on this member, excluding named attributes, or an empty array if there are no custom attributes.</summary>
        ''' <returns>An array of Objects representing custom attributes, or an empty array. This implementation always returns an empty array.</returns>
        ''' <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute. (ignored)</param>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Private Function GetCustomAttributes(inherit As Boolean) As Object() Implements System.Reflection.ICustomAttributeProvider.GetCustomAttributes
            Return New Object() {}
        End Function

        ''' <summary>Returns an array of custom attributes defined on this member, identified by type, or an empty array if there are no custom attributes of that type.</summary>
        ''' <returns>An array of Objects representing custom attributes, or an empty array. This implementation always returns an empty array.</returns>
        ''' <param name="attributeType">The type of the custom attributes. (ignored)</param>
        ''' <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute. (ignored)</param>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Private Function GetCustomAttributes(attributeType As System.Type, inherit As Boolean) As Object() Implements System.Reflection.ICustomAttributeProvider.GetCustomAttributes
            Return New Object() {}
        End Function

        ''' <summary>Indicates whether one or more instance of <paramref name="attributeType" /> is defined on this member.</summary>
        ''' <returns>true if the <paramref name="attributeType" /> is defined on this member; false otherwise. This implementation always returns false.</returns>
        ''' <param name="attributeType">The type of the custom attributes. (ignored)</param>
        ''' <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute. (ignored)</param>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Private Function IsDefined(attributeType As System.Type, inherit As Boolean) As Boolean Implements System.Reflection.ICustomAttributeProvider.IsDefined
            Return False
        End Function
#End Region
    End Class
End Namespace
#End If