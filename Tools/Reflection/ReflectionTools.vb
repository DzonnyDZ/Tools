Imports System.Runtime.CompilerServices, Tools.ExtensionsT
Imports System.Reflection
Imports System.Linq, Tools.LinqT
Imports System.Runtime.InteropServices

#If True Then
Namespace ReflectionT
    'TODO: UnitTests
    ''' <summary>Various reflection tools</summary>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2">Added overloaded functions <see cref="ReflectionTools.GetOperators"/>.</version>
    ''' <version version="1.5.2">Added <see cref="ReflectionTools.IsMemberOf"/> overloaded methods.</version>
    Public Module ReflectionTools
#Region "Namespaces"
        ''' <summary>Gets namespaces in given module</summary>
        ''' <param name="module">Module to get namespaces in</param>
        ''' <returns>Array of namespaces in <paramref name="module"/></returns>
        ''' <param name="includeGlobal">True to include global namespace (with empty name)</param>
        ''' <param name="flat">True to list all namespaces even if their name contains dot (.), False to list only top-level namespaces</param>
        ''' <exception cref="ArgumentNullException"><paramref name="module"/> is null</exception>
        ''' <exception cref="ReflectionTypeLoadException">One or more classes in a module could not be loaded.</exception>
        ''' <exception cref="Security.SecurityException">The caller does not have the required permission.</exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function GetNamespaces(ByVal [module] As [Module], Optional ByVal includeGlobal As Boolean = False, Optional ByVal flat As Boolean = True) As NamespaceInfo()
            Return [module].GetNamespaces(Function(t As Type) True, includeGlobal, flat)
            'If [Module] Is Nothing Then Throw New ArgumentNullException(NameOf([module]))
            'Dim NamespaceNames As New List(Of String)
            'Dim Namespaces As New List(Of NamespaceInfo)
            'For Each Type In [Module].GetTypes
            '    Dim NamespaceName As String
            '    If Not Flat OrElse Type.Namespace = "" OrElse Not Type.Namespace.Contains("."c) Then
            '        NamespaceName = Type.Namespace
            '    Else
            '        NamespaceName = Type.Namespace.Substring(0, Type.Namespace.IndexOf("."c))
            '    End If
            '    If (NamespaceName <> "" OrElse IncludeGlobal) AndAlso Not NamespaceNames.Contains(NamespaceName) Then
            '        NamespaceNames.Add(Type.Namespace)
            '        Namespaces.Add(New NamespaceInfo([Module], Type.Namespace))
            '    End If
            'Next Type
            'Return Namespaces.ToArray()
        End Function
        ''' <summary>Gets namespaces in given module</summary>
        ''' <param name="module">Module to get namespaces in</param>
        ''' <returns>Array of namespaces in <paramref name="module"/></returns>
        ''' <param name="typeFilter">Predicate. Onyl those types for which the predicate returns true will be observed for namespaces.</param>
        ''' <param name="includeGlobal">True to include global namespace (with empty name)</param>
        ''' <param name="flat">True to list all namespaces even if their name contains dot (.), False to list only top-level namespaces</param>
        ''' <exception cref="ArgumentNullException"><paramref name="module"/> or <paramref name="typeFilter"/> is null</exception>
        ''' <exception cref="ReflectionTypeLoadException">One or more classes in a module could not be loaded.</exception>
        ''' <exception cref="Security.SecurityException">The caller does not have the required permission.</exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function GetNamespaces(ByVal [module] As [Module], ByVal typeFilter As Predicate(Of Type), Optional ByVal includeGlobal As Boolean = False, Optional ByVal flat As Boolean = True) As NamespaceInfo()
            If [module] Is Nothing Then Throw New ArgumentNullException(NameOf([module]))
            If typeFilter Is Nothing Then Throw New ArgumentNullException(NameOf(typeFilter))
            Dim NamespaceNames As New List(Of String)
            Dim Namespaces As New List(Of NamespaceInfo)
            For Each Type In [module].GetTypes
                Dim NamespaceName As String
                If flat OrElse Type.Namespace = "" OrElse Not Type.Namespace.Contains("."c) Then
                    NamespaceName = Type.Namespace
                Else
                    NamespaceName = Type.Namespace.Substring(0, Type.Namespace.IndexOf("."c))
                End If
                If ((NamespaceName <> "" OrElse includeGlobal) AndAlso Not NamespaceNames.Contains(NamespaceName)) AndAlso typeFilter(Type) Then
                    NamespaceNames.Add(NamespaceName)
                    Namespaces.Add(New NamespaceInfo([module], NamespaceName))
                End If
            Next Type
            Return Namespaces.ToArray()
        End Function
        ''' <summary> defined in given module</summary>
        ''' <param name="module">Module to get types from</param>
        ''' <param name="fromNamespaces">True to get only types from global namespace. False to get all types (same as <see cref="[Module].GetTypes"/>)</param>
        ''' <returns>Array of types from module <paramref name="module"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="module"/> is null</exception>
        ''' <exception cref="ReflectionTypeLoadException">One or more classes in a module could not be loaded.</exception>
        ''' <exception cref="Security.SecurityException">The caller does not have the required permission.</exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function GetTypes(ByVal [module] As [Module], ByVal fromNamespaces As Boolean) As Type()
            If [module] Is Nothing Then Throw New ArgumentNullException(NameOf([module]))
            If fromNamespaces Then Return [module].GetTypes
            Return (From Type In [module].GetTypes Where Type.Namespace = "" Select Type).ToArray
        End Function
        ''' <summary>Gets declaring namespace of global method</summary>
        ''' <param name="method">Global method to get namespace of</param>
        ''' <returns>Namespace <paramref name="method"/> contains in its name; or null when <paramref name="method"/> is not global method</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension(), EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Function GetNamespace(ByVal method As MethodInfo) As NamespaceInfo
            If method Is Nothing Then Throw New ArgumentNullException(NameOf(method))
            If method.DeclaringType IsNot Nothing Then Return Nothing
            Dim NameParts = method.Name.Split("."c)
            If NameParts.Length = 1 Then Return New NamespaceInfo(method.[Module], "")
            Return New NamespaceInfo(method.Module, String.Join("."c, NameParts, 0, NameParts.Length - 2))
        End Function
        ''' <summary>Gets declaring namespace of global field</summary>
        ''' <param name="field">Global field to get namespace of</param>
        ''' <returns>Namespace <paramref name="field"/> contains in its name; or null when <paramref name="field"/> is not global field</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="field"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension(), EditorBrowsable(EditorBrowsableState.Advanced)> Public Function GetNamespace(ByVal field As FieldInfo) As NamespaceInfo
            If field Is Nothing Then Throw New ArgumentNullException(NameOf(field))
            If field.DeclaringType IsNot Nothing Then Return Nothing
            Dim NameParts = field.Name.Split("."c)
            If NameParts.Length = 1 Then Return New NamespaceInfo(field.[Module], "")
            Return New NamespaceInfo(field.Module, String.Join("."c, NameParts, 0, NameParts.Length - 2))
        End Function
        ''' <summary>Gets namespace of given <see cref="Type"/> as instance of <see cref="NamespaceInfo"/></summary>
        ''' <param name="type">Type to get namespace of</param>
        ''' <returns><see cref="NamespaceInfo"/> constructed from <paramref name="type"/>.<see cref="Type.[Module]">Module</see> and <paramref name="type"/>.<see cref="Type.[Namespace]">Namespace</see>.</returns>
        ''' <remarks>Each type has namespace even when name of the namespace is an empty <see cref="String"/>.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="type"/> is null</exception>
        ''' <version version="1.5.2">Added <see cref="ArgumentNullException"/></version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function GetNamespace(ByVal type As Type) As NamespaceInfo
            If type Is Nothing Then Throw New ArgumentNullException(NameOf(type))
            Return New NamespaceInfo(type.Module, type.Namespace)
        End Function
#End Region
#Region "Visibility/Accessibility, Is..."
        ''' <summary>For <see cref="Type"/>, <see cref="MethodBase"/> (<see cref="MethodInfo"/> or <see cref="ConstructorInfo"/>), <see cref="PropertyInfo"/>, <see cref="EventInfo"/> or <see cref="FieldInfo"/> indicates its accessibility</summary>
        ''' <param name="member">Member to indicate accessibility of</param>
        ''' <returns>True if accessibility of member is public</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> is null</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        ''' <version version="1.5.2">Fixed: When <paramref name="member"/> is <see cref="Type"/> with <see cref="Type.IsNested"/> = True, <see cref="Type.IsNestedPublic"/> = False and <see cref="Type.IsPublic"/> = True function returns false (now it returns true - for type it simply returns <see cref="Type.IsNestedPublic"/> OR <see cref="Type.IsPublic"/>)</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsPublic(ByVal member As MemberInfo) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            Select Case member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(member, Type)
                        Return .IsNestedPublic OrElse .IsPublic
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(member, MethodBase).IsPublic
                Case MemberTypes.Event
                    Return DirectCast(member, EventInfo).GetAccessibility = MethodAttributes.Public
                Case MemberTypes.Field : Return DirectCast(member, FieldInfo).IsPublic
                Case MemberTypes.Property
                    Return DirectCast(member, PropertyInfo).GetAccessibility = MethodAttributes.Public
                Case Else : Return False
            End Select
        End Function
        ''' <summary>For <see cref="Type"/>, <see cref="MethodBase"/> (<see cref="MethodInfo"/> or <see cref="ConstructorInfo"/>), <see cref="PropertyInfo"/>, <see cref="EventInfo"/> or <see cref="FieldInfo"/> indicates its accessibility</summary>
        ''' <param name="member">Member to indicate accessibility of</param>
        ''' <returns>True if accessibility of member is private</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> is null</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsPrivate(ByVal member As MemberInfo) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            Select Case member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(member, Type)
                        Return .IsNested AndAlso .IsNestedPrivate
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(member, MethodBase).IsPrivate
                Case MemberTypes.Event
                    Return DirectCast(member, EventInfo).GetAccessibility = MethodAttributes.Private
                Case MemberTypes.Field : Return DirectCast(member, FieldInfo).IsPrivate
                Case MemberTypes.Property
                    Return DirectCast(member, PropertyInfo).GetAccessibility = MethodAttributes.Private
                Case Else : Return False
            End Select
        End Function
        ''' <summary>For <see cref="Type"/>, <see cref="MethodBase"/> (<see cref="MethodInfo"/> or <see cref="ConstructorInfo"/>), <see cref="PropertyInfo"/>, <see cref="EventInfo"/> or <see cref="FieldInfo"/> indicates its accessibility</summary>
        ''' <param name="member">Member to indicate accessibility of</param>
        ''' <returns>True if accessibility of member is assembly (friend, internal)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> is null</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsAssembly(ByVal member As MemberInfo) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            Select Case member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(member, Type)
                        Return (.IsNested AndAlso .IsNestedAssembly) OrElse (Not .IsNested AndAlso .IsNotPublic)
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(member, MethodBase).IsAssembly
                Case MemberTypes.Event
                    Return DirectCast(member, EventInfo).GetAccessibility = MethodAttributes.Assembly
                Case MemberTypes.Field : Return DirectCast(member, FieldInfo).IsAssembly
                Case MemberTypes.Property
                    Return DirectCast(member, PropertyInfo).GetAccessibility = MethodAttributes.Assembly
                Case Else : Return False
            End Select
        End Function
        ''' <summary>For <see cref="Type"/>, <see cref="MethodBase"/> (<see cref="MethodInfo"/> or <see cref="ConstructorInfo"/>), <see cref="PropertyInfo"/>, <see cref="EventInfo"/> or <see cref="FieldInfo"/> indicates its accessibility</summary>
        ''' <param name="member">Member to indicate accessibility of</param>
        ''' <returns>True if accessibility of member is family-and-assembly</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> is null</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsFamilyAndAssembly(ByVal member As MemberInfo) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            Select Case member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(member, Type)
                        Return (.IsNested AndAlso .IsNestedFamANDAssem)
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(member, MethodBase).IsFamilyAndAssembly
                Case MemberTypes.Event
                    Return DirectCast(member, EventInfo).GetAccessibility = MethodAttributes.FamANDAssem
                Case MemberTypes.Field : Return DirectCast(member, FieldInfo).IsFamilyAndAssembly
                Case MemberTypes.Property
                    Return DirectCast(member, PropertyInfo).GetAccessibility = MethodAttributes.FamANDAssem
                Case Else : Return False
            End Select
        End Function
        ''' <summary>For <see cref="Type"/>, <see cref="MethodBase"/> (<see cref="MethodInfo"/> or <see cref="ConstructorInfo"/>), <see cref="PropertyInfo"/>, <see cref="EventInfo"/> or <see cref="FieldInfo"/> indicates its accessibility</summary>
        ''' <param name="member">Member to indicate accessibility of</param>
        ''' <returns>True if accessibility of member is family-or-assembly (protected friend)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> is null</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        ''' <version version="1.5.2">Fixed: For <paramref name="member"/> being <see cref="MethodBase"/> behaves like <see cref="IsFamilyAndAssembly"/></version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsFamilyOrAssembly(ByVal member As MemberInfo) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            Select Case member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(member, Type)
                        Return (.IsNested AndAlso .IsNestedFamORAssem)
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(member, MethodBase).IsFamilyOrAssembly
                Case MemberTypes.Event
                    Return DirectCast(member, EventInfo).GetAccessibility = MethodAttributes.FamORAssem
                Case MemberTypes.Field : Return DirectCast(member, FieldInfo).IsFamilyOrAssembly
                Case MemberTypes.Property
                    Return DirectCast(member, PropertyInfo).GetAccessibility = MethodAttributes.FamORAssem
                Case Else : Return False
            End Select
        End Function
        ''' <summary>For <see cref="Type"/>, <see cref="MethodBase"/> (<see cref="MethodInfo"/> or <see cref="ConstructorInfo"/>), <see cref="PropertyInfo"/>, <see cref="EventInfo"/> or <see cref="FieldInfo"/> indicates its accessibility</summary>
        ''' <param name="member">Member to indicate accessibility of</param>
        ''' <returns>True if accessibility of member is family (protected)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> is null</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsFamily(ByVal member As MemberInfo) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            Select Case member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(member, Type)
                        Return (.IsNested AndAlso .IsNestedFamily)
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(member, MethodBase).IsFamily
                Case MemberTypes.Event
                    Return DirectCast(member, EventInfo).GetAccessibility = MethodAttributes.Family
                Case MemberTypes.Field : Return DirectCast(member, FieldInfo).IsFamily
                Case MemberTypes.Property
                    Return DirectCast(member, PropertyInfo).GetAccessibility = MethodAttributes.Family
                Case Else : Return False
            End Select
        End Function
        ''' <summary>Gets maximum visibility of getter and setter of property</summary>
        ''' <param name="prp">Property to check accessibility of</param>
        ''' <returns>Accessibility that is union of accessibilities of getter and setter</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="prp"/> is null</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods.</exception>
        <Extension()> Public Function GetAccessibility(ByVal prp As PropertyInfo) As MethodAttributes
            If prp Is Nothing Then Throw New ArgumentNullException(NameOf(prp))
            Return MaxVisibility(If(prp.GetGetMethod(True) Is Nothing, 0, prp.GetGetMethod(True).Attributes), If(prp.GetSetMethod(True) Is Nothing, 0, prp.GetSetMethod(True).Attributes))
        End Function
        ''' <summary>Gets maximum visibility of adder, remover and raiser of event</summary>
        ''' <param name="ev">Event to check accessibility of</param>
        ''' <returns>Accessibility that is union of accessibilities of adder, remover and raiser</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="ev"/> is null</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods.</exception>
        <Extension()> Public Function GetAccessibility(ByVal ev As EventInfo) As MethodAttributes
            If ev Is Nothing Then Throw New ArgumentNullException(NameOf(ev))
            Return MaxVisibility(If(ev.GetAddMethod(True) Is Nothing, 0, ev.GetAddMethod(True).Attributes), If(ev.GetRemoveMethod(True) Is Nothing, 0, ev.GetRemoveMethod(True).Attributes), If(ev.GetRaiseMethod(True) Is Nothing, 0, ev.GetRaiseMethod(True).Attributes))
        End Function

        ''' <summary>Gets maximum visibility from visibilities of methods</summary>
        ''' <param name="visibility">Array of visibilities to test (it can contain any valid value of <see cref="MethodAttributes"/>, non-visibity part will be ignored)</param>
        ''' <returns>Maximum visibility as union of all visibilities in <paramref name="visibility"/></returns>
        ''' <version version="1.5.2">Fixed: <see cref="MethodAttributes.Assembly"/>, <see cref="MethodAttributes.Assembly"/> returns <see cref="MethodAttributes.FamORAssem"/> (now returns correctly <see cref="MethodAttributes.Assembly"/>)</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        Private Function MaxVisibility(ByVal ParamArray visibility As MethodAttributes()) As MethodAttributes
            Dim ret As MethodAttributes = 0
            For Each vis In visibility
                Select Case ret
                    Case MethodAttributes.Private
                        Select Case vis And MethodAttributes.MemberAccessMask
                            Case MethodAttributes.Family, MethodAttributes.FamORAssem, MethodAttributes.FamANDAssem, MethodAttributes.Assembly, MethodAttributes.Public
                                ret = vis And MethodAttributes.MemberAccessMask
                        End Select
                    Case MethodAttributes.Family
                        Select Case vis And MethodAttributes.MemberAccessMask
                            Case MethodAttributes.FamORAssem, MethodAttributes.Assembly
                                ret = MethodAttributes.FamORAssem
                            Case MethodAttributes.Public : ret = MethodAttributes.Public
                        End Select
                    Case MethodAttributes.FamORAssem
                        Select Case vis And MethodAttributes.MemberAccessMask
                            Case MethodAttributes.Public : ret = MethodAttributes.Public
                        End Select
                    Case MethodAttributes.FamANDAssem
                        Select Case vis And MethodAttributes.MemberAccessMask
                            Case MethodAttributes.Family : ret = MethodAttributes.Family
                            Case MethodAttributes.FamORAssem : ret = MethodAttributes.FamORAssem
                            Case MethodAttributes.Assembly : ret = MethodAttributes.Assembly
                            Case MethodAttributes.Public : ret = MethodAttributes.Public
                        End Select
                    Case MethodAttributes.Assembly
                        Select Case vis And MethodAttributes.MemberAccessMask
                            Case MethodAttributes.FamORAssem : ret = MethodAttributes.FamORAssem
                            Case MethodAttributes.Assembly : ret = MethodAttributes.Assembly
                            Case MethodAttributes.Public : ret = MethodAttributes.Public
                        End Select
                    Case Else
                        ret = vis And MethodAttributes.MemberAccessMask
                End Select
                If ret = MethodAttributes.Public Then Return ret 'It cannot be better
            Next vis
            Return ret
        End Function

        ''' <summary>Gets value indicating if member should be considered static</summary>
        ''' <param name="member">Member to check</param>
        ''' <returns>True if member should or can be considered static</returns>
        ''' <remarks>For <see cref="Type"/> always returns true. For <see cref="PropertyInfo"/> and <see cref="MethodInfo"/> returns true only if all non-other accessors are static</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> is null</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="member"/> is <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsStatic(ByVal member As MemberInfo) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            Select Case member.MemberType
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(member, MethodBase).IsStatic
                Case MemberTypes.Event
                    With DirectCast(member, EventInfo)
                        Return _
                            ((.GetAddMethod(True) IsNot Nothing AndAlso .GetAddMethod(True).IsStatic) OrElse .GetAddMethod(True) Is Nothing) AndAlso
                            ((.GetRemoveMethod(True) IsNot Nothing AndAlso .GetRemoveMethod(True).IsStatic) OrElse .GetRemoveMethod(True) Is Nothing) AndAlso
                            ((.GetRaiseMethod(True) IsNot Nothing AndAlso .GetRaiseMethod(True).IsStatic) OrElse .GetRaiseMethod(True) Is Nothing)
                    End With
                Case MemberTypes.Field : Return DirectCast(member, FieldInfo).IsStatic
                Case MemberTypes.Property
                    With DirectCast(member, PropertyInfo)
                        Return _
                            ((.GetGetMethod(True) IsNot Nothing AndAlso .GetGetMethod(True).IsStatic) OrElse .GetGetMethod(True) Is Nothing) AndAlso
                            ((.GetSetMethod(True) IsNot Nothing AndAlso .GetSetMethod(True).IsStatic) OrElse .GetSetMethod(True) Is Nothing)
                    End With
                Case Else : Return True
            End Select
        End Function
        ''' <summary>Gets value indicating if member should be considered final (it cannot be overridden or inherited)</summary>
        ''' <param name="member">Member to check</param>
        ''' <returns>True if member is final</returns>
        ''' <remarks>For <see cref="FieldInfo"/> always returns true. For <see cref="EventInfo"/> and <see cref="PropertyInfo"/> all non-other members must be final to return true.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> is null</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="member"/> is <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsFinal(ByVal member As MemberInfo) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            Select Case member.MemberType
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(member, MethodBase).IsFinal
                Case MemberTypes.Event
                    With DirectCast(member, EventInfo)
                        Return _
                            ((.GetAddMethod(True) IsNot Nothing AndAlso .GetAddMethod(True).IsFinal) OrElse .GetAddMethod(True) Is Nothing) AndAlso
                            ((.GetRemoveMethod(True) IsNot Nothing AndAlso .GetRemoveMethod(True).IsFinal) OrElse .GetRemoveMethod(True) Is Nothing) AndAlso
                            ((.GetRaiseMethod(True) IsNot Nothing AndAlso .GetRaiseMethod(True).IsFinal) OrElse .GetRaiseMethod(True) Is Nothing)
                    End With
                Case MemberTypes.Property
                    With DirectCast(member, PropertyInfo)
                        Return _
                            ((.GetGetMethod(True) IsNot Nothing AndAlso .GetGetMethod(True).IsFinal) OrElse .GetGetMethod(True) Is Nothing) AndAlso
                            ((.GetSetMethod(True) IsNot Nothing AndAlso .GetSetMethod(True).IsFinal) OrElse .GetSetMethod(True) Is Nothing)
                    End With
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    Return DirectCast(member, Type).IsSealed
                Case Else : Return True
            End Select
        End Function

        ''' <summary>Indicates which access has member of given type of another member</summary>
        ''' <param name="member">Member to be accessed</param>
        ''' <param name="observer">Type at which level the call to <paramref name="member"/> is about to be done; when null visibility is reported for any type not nedted in <paramref name="member"/>, not derived form <paramref name="member"/> and not in same assembly as <paramref name="member"/>.</param>
        ''' <remarks>Visibility of <paramref name="member"/> form context inside <paramref name="observer"/>. If <paramref name="member"/> can be really called from context inside <paramref name="observer"/> depends on relation of <paramref name="observer"/> and <paramref name="member"/><see cref="MemberInfo.DeclaringType">DeclaringType</see>. Wheather one is nested to another, one is derived by another and one is in same assembly as another.</remarks>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function HowIsSeenBy(ByVal member As MemberInfo, ByVal observer As Type) As Visibility
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            If member.DeclaringType Is Nothing Then Return member.Visibility 'Member is global
            If observer IsNot Nothing AndAlso member.DeclaringType.Equals(observer) Then Return member.Visibility 'Siblings
            If observer IsNot Nothing AndAlso observer.IsMemberOf(member.DeclaringType) Then Return member.Visibility 'Looking up in nesting hierarchy
            If observer IsNot Nothing AndAlso observer.IsDerivedFrom(member.DeclaringType) Then Return member.Visibility 'Looking up inheritence hierarchy
            Return CombineVisibility(member.Visibility, member.DeclaringType.HowIsSeenBy(observer))
        End Function
        ''' <summary>Indicates if member can be accessed from context of given <see cref="Type"/></summary>
        ''' <param name="member">Member to be accessed</param>
        ''' <param name="observer">Type at which level the call to <paramref name="member"/> is about to be done; when null visibility is reported for any type not nedted in <paramref name="member"/>, not derived form <paramref name="member"/> and not in same assembly as <paramref name="member"/> (so tru is returned only when <paramref name="member"/> is publicly accessible).</param>
        ''' <remarks>This method takes visibility of <paramref name="member"/> by <paramref name="observer"/> indicated by <see cref="HowIsSeenBy"/> and applies interrelation of <paramref name="member"/>.<see cref="MemberInfo.DeclaringType">DeclaringType</see> and <paramref name="observer"/>.</remarks>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function CanBeSeenFrom(ByVal member As MemberInfo, ByVal observer As Type) As Boolean
            Dim Visibility = member.HowIsSeenBy(observer)
            If observer Is Nothing Then Return Visibility = ReflectionT.Visibility.Public
            Return Visibility = ReflectionT.Visibility.Public OrElse
                ((Visibility = ReflectionT.Visibility.Assembly OrElse Visibility = ReflectionT.Visibility.FamORAssem) AndAlso member.IsMemberOf(observer.Assembly)) OrElse
                ((Visibility = ReflectionT.Visibility.Family OrElse Visibility = ReflectionT.Visibility.FamORAssem) AndAlso member.DeclaringType IsNot Nothing AndAlso (member.DeclaringType.Equals(observer) OrElse member.DeclaringType.IsBaseClassOf(observer))) OrElse
                (Visibility = ReflectionT.Visibility.FamANDAssem AndAlso member.IsMemberOf(observer.Assembly) AndAlso member.DeclaringType IsNot Nothing AndAlso (member.DeclaringType.Equals(observer) OrElse member.DeclaringType.IsBaseClassOf(observer))) OrElse
                (Visibility = ReflectionT.Visibility.Private AndAlso observer.Equals(member.DeclaringType) OrElse observer.IsMemberOf(member.DeclaringType))
        End Function
        ''' <summary>Indicates if given type is base class of another type</summary>
        ''' <param name="base">Proposed base class of <paramref name="derived"/></param>
        ''' <param name="derived">Type proposedly derived from <paramref name="base"/></param>
        ''' <returns>True if <paramref name="derived"/> derives (inherits) from <paramref name="base"/>; false otherwise</returns>
        ''' <remarks>Test only inheritance hierarchy. Does not test following conditions that can make  assignment <paramref name="base"/> ← <paramref name="derived"/> possible:
        ''' <list type="bullet">
        ''' <item><paramref name="derived"/> implements <paramref name="base"/></item>
        ''' <item><paramref name="base"/> is underlying type of <paramref name="derived"/> and <paramref name="derived"/> is enumeration</item>
        ''' </list>
        ''' In addition to simple base class test, it also test and returns true in followig conditions:
        ''' <list type="bullet">
        ''' <item><paramref name="base"/> is generic type constraint of <paramref name="derived"/> or base of the constraint.</item>
        ''' <item><paramref name="base"/> is open generic type and  <paramref name="derived"/> derived derives from <paramref name="base"/> (in either geneir-open, generic-closed or generic-half-open-half-closed way).</item>
        ''' </list></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="base"/> or <paramref name="derived"/> is null</exception>
        ''' <seelaso cref="IsDerivedFrom"/>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsBaseClassOf(ByVal base As Type, ByVal derived As Type) As Boolean
            Return GetMeAsBaseClassOf(base, derived) IsNot Nothing
        End Function
        ''' <summary>For generic type gets constructed generic type that is base class of given (non)generic type</summary>
        ''' <param name="base">Type representing base class of <paramref name="derived"/></param>
        ''' <param name="derived">Type derived from <paramref name="base"/></param>
        ''' <returns>Null when <paramref name="derived"/> does not derived from <paramref name="base"/>;
        ''' <paramref name="base"/> when <paramref name="base"/> is not generic or it is closed generic type type and <paramref name="derived"/> derives from <paramref name="base"/>;
        ''' Constructed generic type constructed from <paramref name="base"/> when <paramref name="base"/> is open generic type or semi-constructed generic type and <paramref name="derived"/> derives from it.</returns>
        ''' <remarks>When <paramref name="base"/> is semi-constructed generic type, <paramref name="derived"/> is only considered to derive form it when no change is needed for specified type parameters of <paramref name="base"/>.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="base"/> or <paramref name="derived"/> is null</exception>
        ''' <seelaso cref="IsBaseClassOf"/><seelaso cref="IsDerivedFrom"/>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function GetMeAsBaseClassOf(ByVal base As Type, ByVal derived As Type) As Type
            If base Is Nothing Then Throw New ArgumentNullException(NameOf(base))
            If derived Is Nothing Then Throw New ArgumentNullException(NameOf(derived))
            Dim CurrentBase = derived.BaseType
            Dim BagargsOpen As Type() = Nothing
            Dim Bagargs As Type() = Nothing
            While CurrentBase IsNot Nothing
                If CurrentBase.Equals(base) Then Return CurrentBase
                If base.IsGenericTypeDefinition AndAlso CurrentBase.IsGenericType Then
                    'Note Class BaseClass(Of T); Class DerivedClass(Of T) : Inherits BaseClass(Of T) => GetType(DerivedClass(Of T)).BaseType.IsGenericTypeDefinition = False!
                    'This is correct behavior (see https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=95768, http://blogs.msdn.com/weitao/archive/2008/03/19/formal-type-parameters-of-generic-types.aspx)
                    Dim WouldBeBaseClass = base.MakeGenericType(CurrentBase.GetGenericArguments)
                    If CurrentBase.Equals(WouldBeBaseClass) Then Return CurrentBase
                ElseIf base.IsGenericType AndAlso CurrentBase.IsGenericType AndAlso base.GetGenericTypeDefinition.Equals(CurrentBase.GetGenericTypeDefinition) Then
                    'semi-open base
                    'Replace generic arguments that are from open definition with those from currentbase, leave those that does not come from generic type definition
                    If Bagargs Is Nothing Then Bagargs = base.GetGenericArguments
                    If BagargsOpen Is Nothing Then BagargsOpen = base.GetGenericTypeDefinition.GetGenericArguments
                    Dim Cargs = CurrentBase.GetGenericArguments
                    Dim Params(Bagargs.Length - 1) As Type
                    For i As Integer = 0 To Bagargs.Length - 1
                        Params(i) = If(Bagargs(i).Equals(BagargsOpen(i)), Cargs(i), Bagargs(i))
                    Next
                    If CurrentBase.Equals(base.GetGenericTypeDefinition.MakeGenericType(Params)) Then Return CurrentBase
                End If
                CurrentBase = CurrentBase.BaseType
            End While
            Return Nothing
        End Function

        ''' <summary>Indicates if given type is derived form another type</summary>
        ''' <param name="base">Proposed base class of <paramref name="derived"/></param>
        ''' <param name="derived">Type proposedly derived from <paramref name="base"/></param>
        ''' <returns>True if <paramref name="derived"/> derives (inherits) from <paramref name="base"/>; false otherwise</returns>
        ''' <remarks>Test only inheritance hierarchy. Does not test following conditions that can make  assignment <paramref name="base"/> ← <paramref name="derived"/> possible:
        ''' <list type="bullet"><item><paramref name="derived"/> implements <paramref name="base"/></item>
        ''' <item><paramref name="base"/> is underlying type of <paramref name="derived"/> and <paramref name="derived"/> is enumeration</item></list>
        ''' In addition to simple base class test, it also test and returns true when <paramref name="base"/> is generic type constraint of <paramref name="derived"/> or base of the constraint.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="base"/> or <paramref name="derived"/> is null</exception>
        ''' <seelaso cref="IsBaseClassOf"/>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsDerivedFrom(ByVal derived As Type, ByVal base As Type) As Boolean
            Return base.IsBaseClassOf(derived)
        End Function


        ''' <summary>Gets member-type-independent visibility of member</summary>
        ''' <param name="member">Member to get visibility of</param>
        ''' <returns>Visibility of member. In case member reports impossible combination of visibilities returns 0.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> is null</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function Visibility(ByVal member As MemberInfo) As Visibility
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            If member.IsPublic Then : Return ReflectionT.Visibility.Public
            ElseIf member.IsFamilyAndAssembly Then : Return ReflectionT.Visibility.FamANDAssem
            ElseIf member.IsFamilyOrAssembly Then : Return ReflectionT.Visibility.FamORAssem
            ElseIf member.IsFamily Then : Return ReflectionT.Visibility.Family
            ElseIf member.IsPrivate Then : Return ReflectionT.Visibility.Private
            ElseIf member.IsAssembly Then : Return ReflectionT.Visibility.Assembly
            End If
            Return 0
        End Function
        ''' <summary>Combines visibility of parent and member as seen from outside of parent</summary>
        ''' <param name="parentVisibility">Visibility of parent</param>
        ''' <param name="memberVisibility">Visibility of member</param>
        ''' <returns>Visibility of member how it is seen form outside of parent. Indicates who can access the member if the "who" is otside of parent.</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="memberVisibility"/> or <paramref name="parentVisibility"/> is not member of <see cref="Visibility"/></exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        Private Function CombineVisibility(ByVal memberVisibility As Visibility, ByVal parentVisibility As Visibility) As Visibility
            If Not IsDefined(memberVisibility) Then Throw New InvalidEnumArgumentException(NameOf(memberVisibility), memberVisibility, memberVisibility.GetType)
            If Not IsDefined(parentVisibility) Then Throw New InvalidEnumArgumentException(NameOf(parentVisibility), parentVisibility, parentVisibility.GetType)
            Select Case parentVisibility
                Case ReflectionT.Visibility.Public
                    Select Case memberVisibility
                        Case ReflectionT.Visibility.Public : Return ReflectionT.Visibility.Public
                        Case ReflectionT.Visibility.FamORAssem : Return ReflectionT.Visibility.FamORAssem
                        Case ReflectionT.Visibility.Assembly : Return ReflectionT.Visibility.Assembly
                        Case ReflectionT.Visibility.Family : Return ReflectionT.Visibility.Family
                        Case ReflectionT.Visibility.FamANDAssem : Return ReflectionT.Visibility.FamANDAssem
                        Case ReflectionT.Visibility.Private : Return ReflectionT.Visibility.Private
                    End Select
                Case ReflectionT.Visibility.FamORAssem
                    Select Case memberVisibility
                        Case ReflectionT.Visibility.Public : Return ReflectionT.Visibility.FamORAssem
                        Case ReflectionT.Visibility.FamORAssem : Return ReflectionT.Visibility.FamORAssem
                        Case ReflectionT.Visibility.Assembly : Return ReflectionT.Visibility.Assembly
                        Case ReflectionT.Visibility.Family : Return ReflectionT.Visibility.Family
                        Case ReflectionT.Visibility.FamANDAssem : Return ReflectionT.Visibility.FamANDAssem
                        Case ReflectionT.Visibility.Private : Return ReflectionT.Visibility.Private
                    End Select
                Case ReflectionT.Visibility.Assembly
                    Select Case memberVisibility
                        Case ReflectionT.Visibility.Public : Return ReflectionT.Visibility.Assembly
                        Case ReflectionT.Visibility.FamORAssem : Return ReflectionT.Visibility.Assembly
                        Case ReflectionT.Visibility.Assembly : Return ReflectionT.Visibility.Assembly
                        Case ReflectionT.Visibility.Family : Return ReflectionT.Visibility.FamANDAssem
                        Case ReflectionT.Visibility.FamANDAssem : Return ReflectionT.Visibility.FamANDAssem
                        Case ReflectionT.Visibility.Private : Return ReflectionT.Visibility.Private
                    End Select
                Case ReflectionT.Visibility.Family
                    Select Case memberVisibility
                        Case ReflectionT.Visibility.Public : Return ReflectionT.Visibility.Family
                        Case ReflectionT.Visibility.FamORAssem : Return ReflectionT.Visibility.Family
                        Case ReflectionT.Visibility.Assembly : Return ReflectionT.Visibility.FamANDAssem
                        Case ReflectionT.Visibility.Family : Return ReflectionT.Visibility.Family
                        Case ReflectionT.Visibility.FamANDAssem : Return ReflectionT.Visibility.FamANDAssem
                        Case ReflectionT.Visibility.Private : Return ReflectionT.Visibility.Private
                    End Select
                Case ReflectionT.Visibility.FamANDAssem
                    Select Case memberVisibility
                        Case ReflectionT.Visibility.Public : Return ReflectionT.Visibility.FamANDAssem
                        Case ReflectionT.Visibility.FamORAssem : Return ReflectionT.Visibility.FamANDAssem
                        Case ReflectionT.Visibility.Assembly : Return ReflectionT.Visibility.FamANDAssem
                        Case ReflectionT.Visibility.Family : Return ReflectionT.Visibility.FamANDAssem
                        Case ReflectionT.Visibility.FamANDAssem : Return ReflectionT.Visibility.FamANDAssem
                        Case ReflectionT.Visibility.Private : Return ReflectionT.Visibility.Private
                    End Select
                Case ReflectionT.Visibility.Private
                    Select Case memberVisibility
                        Case ReflectionT.Visibility.Public : Return ReflectionT.Visibility.Private
                        Case ReflectionT.Visibility.FamORAssem : Return ReflectionT.Visibility.Private
                        Case ReflectionT.Visibility.Assembly : Return ReflectionT.Visibility.Private
                        Case ReflectionT.Visibility.Family : Return ReflectionT.Visibility.Private
                        Case ReflectionT.Visibility.FamANDAssem : Return ReflectionT.Visibility.Private
                        Case ReflectionT.Visibility.Private : Return ReflectionT.Visibility.Private
                    End Select
            End Select
            Return memberVisibility 'Never happens
        End Function
#End Region
#Region "Events and properties"
        ''' <summary>Searches for property given method belongs to</summary>
        ''' <param name="method">Method to search property for</param>
        ''' <param name="getSetOnly">Search only for getters and setters</param>
        ''' <param name="inherit">Search within methods of base types</param>
        ''' <returns>First property that has <paramref name="method"/> as one of its accessors</returns>
        ''' <remarks>Search is done only within type where <paramref name="method"/> is declared and optionally within it's base types</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function GetProperty(ByVal method As MethodInfo, Optional ByVal getSetOnly As Boolean = False, Optional ByVal inherit As Boolean = False) As PropertyInfo
            If method Is Nothing Then Throw New ArgumentNullException(NameOf(method))
            If method.DeclaringType Is Nothing Then Return Nothing
            For Each prp In method.DeclaringType.GetProperties(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance Or If(inherit, BindingFlags.Default, BindingFlags.DeclaredOnly))
                If getSetOnly Then
                    If method.Equals(prp.GetGetMethod(Not method.IsPublic)) Then Return prp
                    If method.Equals(prp.GetSetMethod(Not method.IsPublic)) Then Return prp
                Else
                    For Each other In prp.GetAccessors(Not method.IsPublic)
                        If method.Equals(other) Then Return prp
                    Next other
                End If
            Next prp
            Return Nothing
        End Function
        ''' <summary>Searches for event given method belongs to</summary>
        ''' <param name="method">Method to search event for</param>
        ''' <param name="standardOnly">Search only for address, removers and raisers</param>
        ''' <param name="inherit">Search within methods of base types</param>
        ''' <returns>First event that has <paramref name="method"/> as one of its accessors</returns>
        ''' <remarks>Search is done only within type where <paramref name="method"/> is declared and optionally within it's base types</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function GetEvent(ByVal method As MethodInfo, Optional ByVal standardOnly As Boolean = False, Optional ByVal inherit As Boolean = False) As EventInfo
            If method Is Nothing Then Throw New ArgumentNullException(NameOf(method))
            If method.DeclaringType Is Nothing Then Return Nothing
            For Each ev In method.DeclaringType.GetEvents(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance Or If(inherit, BindingFlags.Default, BindingFlags.DeclaredOnly))
                Dim add = ev.GetAddMethod(Not method.IsPublic)
                Dim remove = ev.GetRemoveMethod(Not method.IsPublic)
                Dim raise = ev.GetRaiseMethod(Not method.IsPublic)
                If add IsNot Nothing AndAlso method.Equals(add) Then Return ev
                If remove IsNot Nothing AndAlso method.Equals(remove) Then Return ev
                If raise IsNot Nothing AndAlso method.Equals(raise) Then Return ev
                If Not standardOnly Then
                    For Each other In ev.GetOtherMethods(Not method.IsPublic)
                        If method.Equals(other) Then Return ev
                    Next other
                End If
            Next ev
            Return Nothing
        End Function
        ''' <summary>Gets all accessors of given event</summary>
        ''' <param name="event">Event to get accessors of</param>
        ''' <param name="nonPublic">True to get non-public accessors as well as public</param>
        ''' <returns>Array of all accessors of <paramref name="event"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="event"/> is null</exception>
        ''' <exception cref="MethodAccessException"><paramref name="nonPublic"/> is true, event accessor is non-public, and the caller does not have permission to reflect on non-public methods. </exception>
        ''' <remarks>If <paramref name="event"/> does not support <see cref="M:System.Reflection.EventInfo.GetOtherMethods(System.Boolean)"/>, <see cref="M:System.Reflection.EventInfo.GetOtherMethods()"/> is used.</remarks>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Function GetAccessors(ByVal [event] As EventInfo, Optional ByVal nonPublic As Boolean = False) As MethodInfo()
            If [event] Is Nothing Then Throw New ArgumentNullException(NameOf([event]))
            Dim ret As New List(Of MethodInfo)
            If [event].GetAddMethod(nonPublic) IsNot Nothing Then ret.Add([event].GetAddMethod(nonPublic))
            If [event].GetRemoveMethod(nonPublic) IsNot Nothing Then ret.Add([event].GetRemoveMethod(nonPublic))
            If [event].GetRaiseMethod(nonPublic) IsNot Nothing Then ret.Add([event].GetRaiseMethod(nonPublic))
            Try
                ret.AddRange([event].GetOtherMethods(nonPublic))
            Catch ex As NotImplementedException
                ret.AddRange([event].GetOtherMethods())
            End Try
            Return ret.ToArray
        End Function
#End Region
#Region "Operators"
        ''' <summary>Gets value indicating whether and if which the function is operator</summary>
        ''' <param name="method">Method to investigate</param>
        ''' <param name="nonStandard">Also include operators that are not part of CLI standard (currently VB \, ^ and &amp; operators are supported)</param>
        ''' <returns>If function is operator returns one of <see cref="Operators"/> constants. If function is not operator (or it seems to be a operator but does not fit to operator it pretends to be) returns <see cref="Operators.no"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Function IsOperator(ByVal method As MethodInfo, Optional ByVal nonStandard As Boolean = True) As Operators
            If method Is Nothing Then Throw New ArgumentNullException(NameOf(method))
            If method.IsSpecialName AndAlso method.IsStatic AndAlso method.Name.StartsWith("op_") AndAlso [Enum].GetNames(GetType(Operators)).Contains(method.Name.Substring(3)) AndAlso Not method.ReturnType.Equals(GetType(Void)) Then
                Dim Op As Operators = [Enum].Parse(GetType(Operators), method.Name.Substring(3))
                If Op.NumberOfOperands <> method.GetParameters.Length Then Return Operators.no
                If Not nonStandard AndAlso Not Op.IsStandard Then Return Operators.no
                Return Op
            Else
                Return Operators.no
            End If
        End Function
        ''' <summary>Gets number of operands of given operator</summary>
        ''' <param name="operator">Operator to get number of operands of</param>
        ''' <returns>And-combination of <paramref name="operator"/> and <see cref="Operators_masks.NoOfOperands"/></returns>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Function NumberOfOperands(ByVal [operator] As Operators) As Byte
            Return [operator] And Operators_masks.NoOfOperands
        End Function
        ''' <summary>Gets value indicating if given operator is standard CLI operator</summary>
        ''' <param name="operator">Operator to get information for</param>
        ''' <returns>Negation of and-combination of <paramref name="operator"/> and <see cref="Operators_masks.NonStandard"/></returns>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Function IsStandard(ByVal [operator] As Operators) As Boolean
            Return Not ([operator] And Operators_masks.NonStandard)
        End Function
        ''' <summary>Gets value indicating if operator is assignment operator</summary>
        ''' <param name="operator">Operator to get information for</param>
        ''' <returns>And-combination of <paramref name="operator"/> and <see cref="Operators_masks.Assignment"/></returns>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Function IsAssignment(ByVal [operator] As Operators) As Boolean
            Return [operator] And Operators_masks.Assignment
        End Function
#End Region
        ''' <summary>Gets interfaces implemented by given type</summary>
        ''' <param name="type">Type to get interfaces from</param>
        ''' <param name="inherit">True to get all interfaces, false to get only interfaces implemented by this type directly</param>
        ''' <returns>Interfaces implemented by this type. Whether all or only those implemented by this type directly depends on <paramref name="inherit"/>.</returns>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function GetImplementedInterfaces(ByVal type As Type, Optional ByVal inherit As Boolean = False) As IEnumerable(Of Type)
            If inherit Then Return type.GetInterfaces
            Return From MyInterface In type.GetInterfaces
                   Where Not UnionAll(If(type.BaseType Is Nothing, DirectCast(New List(Of Type), IEnumerable(Of Type)), type.BaseType.GetInterfaces),
                       From MyInterface2 In type.GetInterfaces
                       Select DirectCast(MyInterface2.GetInterfaces, IEnumerable(Of Type))
                       ).Contains(MyInterface)
                   Select MyInterface
        End Function

        ''' <summary>Gets value indicating if method is global method</summary>
        ''' <param name="method">Method to test is it is global</param>
        ''' <returns>True when <paramref name="method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see> is null</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="method"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension(), EditorBrowsable(EditorBrowsableState.Advanced)> Function IsGlobal(ByVal method As MethodInfo) As Boolean
            If method Is Nothing Then Throw New ArgumentNullException(NameOf(method))
            Return method.DeclaringType Is Nothing
        End Function
        ''' <summary>Gets value indicating if field is global field</summary>
        ''' <param name="field">Field to test is it is global</param>
        ''' <returns>True when <paramref name="field"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see> is null</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="field"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension(), EditorBrowsable(EditorBrowsableState.Advanced)> Function IsGobal(ByVal field As FieldInfo) As Boolean
            If field Is Nothing Then Throw New ArgumentNullException(NameOf(field))
            Return field.DeclaringType Is Nothing
        End Function

#Region "Operators"
        ''' <summary>Gets operators of given kind defined by given type</summary>
        ''' <param name="Type">Type to look for operators on</param>
        ''' <param name="operator">Type of operator to look for</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        ''' <returns>Array of operators of kind <paramref name="operator"/> specified for <paramref name="Type"/>. An empty aray when no operator was found.</returns>
        ''' <remarks>This overload looks only for public operators.</remarks>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function GetOperators(ByVal Type As Type, ByVal [operator] As Operators) As MethodInfo()
            If Type Is Nothing Then Throw New ArgumentNullException(NameOf(Type))
            Return Type.GetOperators([operator], BindingFlags.Public)
        End Function
        ''' <summary>Gets operators of given kind defined by given type</summary>
        ''' <param name="type">Type to look for operators on</param>
        ''' <param name="operator">Type of operator to look for</param>
        ''' <param name="bindingAttr">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. <see cref="BindingFlags.Instance"/> is ignored.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="type"/> is null</exception>
        ''' <returns>Array of operators of kind <paramref name="operator"/> specified for <paramref name="type"/>. An empty aray when no operator was found.</returns>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()>
        Public Function GetOperators(ByVal type As Type, ByVal [operator] As Operators, ByVal bindingAttr As BindingFlags) As MethodInfo()
            If type Is Nothing Then Throw New ArgumentNullException(NameOf(type))
            Return (From Method In type.GetMethods((BindingFlags.Static Or bindingAttr) And Not BindingFlags.Instance)
                    Where Method.IsOperator = [operator]
                    Select Method
                   ).ToArray
        End Function

        ''' <summary>Gets all operator defined by given type</summary>
        ''' <param name="type">Type to look for operators on</param>
        ''' <exception cref="ArgumentNullException"><paramref name="type"/> is null</exception>
        ''' <returns>Array of operators defined by <paramref name="type"/>. An empty array when <paramref name="type"/> defines no operaors.</returns>
        ''' <remarks>This overload looks only for public operators.</remarks>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function GetOperators(ByVal type As Type) As MethodInfo()
            If type Is Nothing Then Throw New ArgumentNullException(NameOf(type))
            Return type.GetOperators(BindingFlags.Public)
        End Function
        ''' <summary>Gets all operator defined by given type</summary>
        ''' <param name="type">Type to look for operators on</param>
        ''' <exception cref="ArgumentNullException"><paramref name="type"/> is null</exception>
        ''' <returns>Array of operators defined by <paramref name="type"/>. An empty array when <paramref name="type"/> defines no operaors.</returns>
        ''' <remarks>This overload looks only for public operators.</remarks>
        ''' <param name="bindingAttr">A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the search is conducted. <see cref="BindingFlags.Instance"/> is ignored.</param>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function GetOperators(ByVal type As Type, ByVal bindingAttr As BindingFlags) As MethodInfo()
            If type Is Nothing Then Throw New ArgumentNullException(NameOf(type))
            Return (From Method In type.GetMethods((BindingFlags.Static Or bindingAttr) And Not BindingFlags.Instance)
                    Where Method.IsOperator <> Operators.no
                    Select Method).ToArray
        End Function
        ''' <summary>Gets all operators that can be possibly used to cast from one type to another</summary>
        ''' <param name="tFrom">Type to cast from</param>
        ''' <param name="tTo">Type to cast to</param>
        ''' <returns>Array of implicit and explicit public cast operator defined on types <paramref name="tFrom"/> and <paramref name="tTo"/> accepting <paramref name="tFrom"/> (or its base type) as parameter and returning <paramref name="tTo"/> (or derived type). Base and derived type are in meanig of <see cref="Type.IsAssignableFrom"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="tFrom"/> or <paramref name="tTo"/> is null.</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Function GetCastOperators(ByVal tFrom As Type, ByVal tTo As Type) As MethodInfo()
            If tFrom Is Nothing Then Throw New ArgumentNullException(NameOf(tFrom))
            If tTo Is Nothing Then Throw New ArgumentNullException(NameOf(tTo))
            Return (From op In tFrom.GetOperators(Operators.Implicit).Union(tFrom.GetOperators(Operators.Explicit)).Union(tTo.GetOperators(Operators.Implicit)).Union(tTo.GetOperators(Operators.Explicit))
                    Where op.GetParameters()(0).ParameterType.IsAssignableFrom(tFrom) AndAlso tTo.IsAssignableFrom(op.ReturnType)
                    Select op).ToArray
        End Function
        ''' <summary>FInds best-fit (most specific) cast operator from one type to another</summary>
        ''' <param name="tFrom">Type to cast from</param>
        ''' <param name="tTo">Type to cast to</param>
        ''' <returns>The best operator to be used to cast type <paramref name="tFrom"/> to type <paramref name="tTo"/>, null if no operator was found</returns>
        ''' <exception cref="AmbiguousMatchException">Operators were found, but no one is most specific.</exception>
        ''' <remarks>Operators are obtained using <see cref="GetCastOperators"/> and then specificity is evaluated.
        ''' <list type="numbered">
        ''' <item>Only operators which argument is assignable from <paramref name="tFrom"/> and return type can be assigned to <paramref name="tTo"/> are considered. Required custom modifiers (modreq) of argument and return value must not be present.</item>
        ''' <item>Operators are ordered by distance (<see cref="ComputeDistance"/>) of operand and <paramref name="tFrom"/>, then by distance of <paramref name="tTo"/> and return type and then implicit befor explicit.</item>
        ''' <item>First operator in after such ordering is returned. If more operators has same order, ordering continues.</item>
        ''' <item>Operators are ordered by declaring type. First operators declared directly on <paramref name="tFrom"/>, second operators declared directly on <paramref name="tTo"/>, third operators declard directly on immediate base of <paramref name="tFrom"/>, fourth operators declared directly on immediate base of <paramref name="tTo"/>, fifth operators declared directly on immediate base of immediate base of <paramref name="tFrom"/> etc. <see cref="ComputeDistance"/> is used.</item>
        ''' <item>First operator in after such ordering is returned. If more operators has same order, ordering continues.</item>
        ''' <item>Operators are ordered by CLS-compliance. CLS-compliant first.</item>
        ''' <item>First operator in after such ordering is returned. If more operators has same order, ordering continues.</item>
        ''' <item>Operators are ordered by sum of numbers of optional custom modifiers (modopt) on parameter and return value.</item>
        ''' <item>First operator in after such ordering is returned. If more operators has same order, <see cref="AmbiguousMatchException"/> is thrown.</item>
        ''' </list></remarks>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Function FindBestFitCastOperator(ByVal tFrom As Type, ByVal tTo As Type) As MethodInfo
            Dim Operators = (From op In GetCastOperators(tFrom, tTo)
                             Where op.GetParameters.Length = 1 AndAlso op.GetParameters()(0).GetRequiredCustomModifiers.Length = 0 AndAlso op.ReturnParameter.GetRequiredCustomModifiers.Length = 0
                             Select [Operator] = op, DistanceIn = ComputeDistance(op.GetParameters()(0).ParameterType, tFrom), DistanceOut = ComputeDistance(tTo, op.ReturnType), IsImplicit = op.IsOperator = ReflectionT.Operators.Implicit
                             Order By DistanceIn, DistanceOut, If(IsImplicit, 0, 1)).ToArray
            If Operators.Length = 1 Then Return Operators(0).Operator
            If Operators.Length = 0 Then Return Nothing
            Dim Best = (From op In Operators
                        Where op.DistanceIn = Operators(0).DistanceIn AndAlso op.DistanceOut = Operators(0).DistanceOut AndAlso op.IsImplicit = Operators(0).IsImplicit
                        Select [Operator] = op.Operator, Order =
                                 If(op.Operator.DeclaringType.Equals(tFrom), 0,
                                 If(op.Operator.DeclaringType.Equals(tTo), 1,
                                 If(op.Operator.DeclaringType.IsAssignableFrom(tFrom), ComputeDistance(op.Operator.DeclaringType, tFrom) * 2 + 1,
                                 If(op.Operator.DeclaringType.IsAssignableFrom(tTo), ComputeDistance(op.Operator.DeclaringType, tTo) * 2 + 2, Integer.MaxValue))))
                        Order By Order).ToArray
            If Best.Length = 1 Then Return Best(0).Operator
            If Best(0).Order <> Best(1).Order Then Return Best(0).Operator
            Dim Best2 = (From op In Best
                         Where op.Order = Best(0).Order
                         Select [Operator] = op.Operator, CLCA = op.Operator.GetAttribute(Of CLSCompliantAttribute)()
                         Select [Operator], CLCARank = If(CLCA Is Nothing, 0, If(CLCA.IsCompliant, 0, 1))
                         Order By CLCARank).ToArray
            If Best2.Length = 1 Then Return Best2(0).Operator
            If Best2(0).CLCARank <> Best2(1).CLCARank Then Return Best2(0).Operator
            Dim Best3 = (From op In Best2
                         Where op.CLCARank = Best2(0).CLCARank
                         Select op.Operator, Rank = op.Operator.GetParameters()(0).GetOptionalCustomModifiers.Length + op.Operator.ReturnParameter.GetOptionalCustomModifiers.Length
                         Order By Rank).ToArray
            If Best3.Length = 1 Then Return Best3(0).Operator
            If Best3(0).Rank = Best3(1).Rank Then Return Best3(0).Operator
            Throw New AmbiguousMatchException(ResourcesT.Exceptions.NoCastOperatorIsMostSpecific)
        End Function


        ''' <summary>Finds a method that represents operator for given operation</summary>
        ''' <param name="operator">Indicates which operator to retrieve. This method supports only binary operators.</param>
        ''' <param name="typeA">Type of left operand</param>
        ''' <param name="typeB">Type of right operand</param>
        ''' <param name="bindingFlags">Binding flags to find operator. Note: <see cref="BindingFlags.[Static]"/> is always used and <see cref="BindingFlags.Instance"/> is never used.</param>
        ''' <param name="fallbackProviders">In case operator is found neither in type <paramref name="typeA"/> nor in <paramref name="typeB"/> this method allows search in arbitrary types for externally defined operators. Operator methods must have same name as operator methods defined in one ope operands' type. Operator methods must be static. Special name is not required.</param>
        ''' <returns>Delegate to operator, or null if operator cannot be found</returns>
        ''' <exception cref="AmbiguousMatchException">More than one operator with same level specificity found.</exception>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Public Function FindBinaryOperator([operator] As Operators, typeA As Type, typeB As Type, bindingFlags As BindingFlags, fallbackProviders As Type()) As [Delegate]
            If typeA Is Nothing Then Throw New ArgumentNullException(NameOf(typeA))
            If typeB Is Nothing Then Throw New ArgumentNullException(NameOf(typeB))
            Dim operators = (From op In typeA.GetOperators([operator], bindingFlags).UnionAll(typeB.GetOperators([operator], bindingFlags))
                             Where op.GetParameters()(0).ParameterType.IsAssignableFrom(typeA) AndAlso op.GetParameters()(1).ParameterType.IsAssignableFrom(typeB)
                             Select Method = op, DistanceA = ComputeDistance(op.GetParameters()(0).ParameterType, typeA), DistanceB = ComputeDistance(op.GetParameters()(0).ParameterType, typeB)
                             Order By DistanceA + DistanceB
                            ).AsEnumerable
            Dim opMethod As MethodInfo = Nothing
            If operators.Any AndAlso operators.Skip(1).IsEmpty Then 'Single
                opMethod = operators(0).Method
            ElseIf operators.Skip(1).Any AndAlso operators(0).DistanceA + operators(0).DistanceB = operators(1).DistanceA + operators(1).DistanceB Then 'More than one and 1st and 2nd are of same specificity
                Throw New AmbiguousMatchException("No operator is most specific")
            ElseIf operators.Any > 0 Then 'More than one, 1st if most specific
                opMethod = operators(0).Method
            End If
            If opMethod Is Nothing Then
                If fallbackProviders Is Nothing Then Return Nothing
                Dim fallbackOperators As IEnumerable(Of MethodInfo) = New MethodInfo() {}
                For Each fbp In fallbackProviders
                    fallbackOperators = fallbackOperators.UnionAll(
                        From op In fbp.GetMethods((bindingFlags Or Reflection.BindingFlags.Static) And Not BindingFlags.Instance)
                        Where op.GetParameters().Length = 2 AndAlso op.ReturnType <> GetType(Void) AndAlso
                              op.Name = "op_" & [operator].ToString AndAlso
                              op.GetParameters()(0).ParameterType.IsAssignableFrom(typeA) AndAlso op.GetParameters()(1).ParameterType.IsAssignableFrom(typeB)
                    )
                Next
                operators = From op In fallbackOperators Select Method = op, DistanceA = ComputeDistance(op.GetParameters()(0).ParameterType, typeA), DistanceB = ComputeDistance(op.GetParameters()(1).ParameterType, typeB)
                If operators.Any AndAlso operators.Skip(1).IsEmpty Then 'Single
                    opMethod = operators(0).Method
                ElseIf operators.Skip(1).Any AndAlso operators(0).DistanceA + operators(0).DistanceB = operators(1).DistanceA + operators(1).DistanceB Then 'More than one and 1st and 2nd are of same specificity
                    Throw New AmbiguousMatchException("No fallback operator is most specific")
                ElseIf operators.Any > 0 Then 'More than one, 1st if most specific
                    opMethod = operators(0).Method
                End If
            End If

            If opMethod Is Nothing Then Return Nothing
            Dim opDelType = GetType(Func(Of ,,)).MakeGenericType(opMethod.GetParameters()(0).ParameterType, opMethod.GetParameters()(1).ParameterType, opMethod.ReturnType)
            Return [Delegate].CreateDelegate(opDelType, opMethod)
        End Function
        ''' <summary>Finds a method that represents operator for given operation</summary>
        ''' <param name="operator">Indicates which operator to retrieve. This method supports only binary operators.</param>
        ''' <param name="typeA">Type of left operand</param>
        ''' <param name="typeB">Type of right operand</param>
        ''' <param name="bindingFlags">Binding flags to find operator. Note: <see cref="BindingFlags.[Static]"/> is always used and <see cref="BindingFlags.Instance"/> is never used.</param>
        ''' <param name="useFallback">In case operator is found neither in type <paramref name="typeA"/> nor in <paramref name="typeB"/> when this parameter is true type <see cref="NumericsT.Operators"/> is sought ofr externaly defined operators.</param>
        ''' <returns>Delegate to operator, or null if operator cannot be found</returns>
        ''' <exception cref="AmbiguousMatchException">More than one operator with same level specificity found.</exception>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Public Function FindBinaryOperator([operator] As Operators, typeA As Type, typeB As Type, bindingFlags As BindingFlags, useFallback As Boolean) As [Delegate]
            Return FindBinaryOperator([operator], typeA, typeB, bindingFlags, If(useFallback, New Type() {GetType(Tools.Operators)}, Type.EmptyTypes))
        End Function
        ''' <summary>Finds a method that represents operator for given operation</summary>
        ''' <param name="operator">Indicates which operator to retrieve. This method supports only binary operators.</param>
        ''' <param name="typeA">Type of left operand</param>
        ''' <param name="typeB">Type of right operand</param>
        ''' <param name="useFallback">In case operator is found neither in type <paramref name="typeA"/> nor in <paramref name="typeB"/> when this parameter is true type <see cref="NumericsT.Operators"/> is sought ofr externaly defined operators.</param>
        ''' <returns>Delegate to operator, or null if operator cannot be found</returns>
        ''' <exception cref="AmbiguousMatchException">More than one operator with same level specificity found.</exception>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Public Function FindBinaryOperator([operator] As Operators, typeA As Type, typeB As Type, useFallback As Boolean) As [Delegate]
            Return FindBinaryOperator([operator], typeA, typeB, BindingFlags.Public, If(useFallback, New Type() {GetType(Tools.Operators)}, Type.EmptyTypes))
        End Function
        ''' <summary>Finds a method that represents operator for given operation</summary>
        ''' <param name="operator">Indicates which operator to retrieve. This method supports only binary operators.</param>
        ''' <param name="typeA">Type of left operand</param>
        ''' <param name="typeB">Type of right operand</param>
        ''' <param name="fallbackProviders">In case operator is found neither in type <paramref name="typeA"/> nor in <paramref name="typeB"/> this method allows search in arbitrary types for externally defined operators. Operator methods must have same name as operator methods defined in one ope operands' type. Operator methods must be static. Special name is not required.</param>
        ''' <returns>Delegate to operator, or null if operator cannot be found</returns>
        ''' <exception cref="AmbiguousMatchException">More than one operator with same level specificity found.</exception>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Public Function FindBinaryOperator([operator] As Operators, typeA As Type, typeB As Type, ParamArray fallbackProviders As Type()) As [Delegate]
            Return FindBinaryOperator([operator], typeA, typeB, BindingFlags.Public, fallbackProviders)
        End Function
        ''' <summary>Finds a method that represents operator for given operation</summary>
        ''' <param name="operator">Indicates which operator to retrieve. This method supports only binary operators.</param>
        ''' <param name="typeA">Type of left operand</param>
        ''' <param name="typeB">Type of right operand</param>
        ''' <returns>Delegate to operator, or null if operator cannot be found</returns>
        ''' <exception cref="AmbiguousMatchException">More than one operator with same level specificity found.</exception>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Public Function FindBinaryOperator([operator] As Operators, typeA As Type, typeB As Type) As [Delegate]
            Return FindBinaryOperator([operator], typeA, typeB, False)
        End Function
        ''' <summary>Finds a method that represents operator for given operation</summary>
        ''' <typeparam name="TA">Type of left operand</typeparam>
        ''' <typeparam name="TB">Type of right operand</typeparam>
        ''' <param name="operator">Indicates which operator to retrieve. This method supports only binary operators.</param>
        ''' <returns>Delegate to operator, or null if operator cannot be found</returns>
        ''' <exception cref="AmbiguousMatchException">More than one operator with same level specificity found.</exception>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Public Function FindBinaryOperator(Of TA, TB)([operator] As Operators) As Func(Of TA, TB, Object)
            Return CType(FindBinaryOperator([operator], GetType(TA), GetType(TB)), Func(Of TA, TB, Object))
        End Function
        ''' <summary>Finds a method that represents operator for given operation</summary>
        ''' <typeparam name="TA">Type of left operand</typeparam>
        ''' <typeparam name="TB">Type of right operand</typeparam>
        ''' <param name="operator">Indicates which operator to retrieve. This method supports only binary operators.</param>
        ''' <param name="bindingFlags">Binding flags to find operator. Note: <see cref="BindingFlags.[Static]"/> is always used and <see cref="BindingFlags.Instance"/> is never used.</param>
        ''' <param name="fallbackProviders">In case operator is found neither in type <typeparamref name="TA"/> nor in <typeparamref name="TB"/> this method allows search in arbitrary types for externally defined operators. Operator methods must have same name as operator methods defined in one ope operands' type. Operator methods must be static. Special name is not required.</param>
        ''' <returns>Delegate to operator, or null if operator cannot be found</returns>
        ''' <exception cref="AmbiguousMatchException">More than one operator with same level specificity found.</exception>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Public Function FindBinaryOperator(Of TA, TB)([operator] As Operators, bindingFlags As BindingFlags, fallbackProviders As Type()) As Func(Of TA, TB, Object)
            Return CType(FindBinaryOperator([operator], GetType(TA), GetType(TB), bindingFlags, fallbackProviders), Func(Of TA, TB, Object))
        End Function
        ''' <summary>Finds a method that represents operator for given operation</summary>
        ''' <typeparam name="TA">Type of left operand</typeparam>
        ''' <typeparam name="TB">Type of right operand</typeparam>
        ''' <param name="operator">Indicates which operator to retrieve. This method supports only binary operators.</param>
        ''' <param name="useFallback">In case operator is found neither in type <typeparamref name="TA"/> nor in <typeparamref name="TB"/> when this parameter is true type <see cref="NumericsT.Operators"/> is sought ofr externaly defined operators.</param>
        ''' <returns>Delegate to operator, or null if operator cannot be found</returns>
        ''' <exception cref="AmbiguousMatchException">More than one operator with same level specificity found.</exception>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Public Function FindBinaryOperator(Of TA, TB)([operator] As Operators, useFallback As Boolean) As Func(Of TA, TB, Object)
            Return CType(FindBinaryOperator([operator], GetType(TA), GetType(TB), useFallback), Func(Of TA, TB, Object))
        End Function

        ''' <summary>Numerically evaluates distance between base type and derived type</summary>
        ''' <param name="baseType">Type to be base of <paramref name="derivedType"/></param>
        ''' <param name="derivedType">Type to be derived from <paramref name="baseType"/></param>
        ''' <returns>Value numerically evaluating distance in inheritance hierarchy of types <paramref name="baseType"/> and <paramref name="derivedType"/>.</returns>
        ''' <remarks><paramref name="derivedType"/> should be derived from <paramref name="baseType"/> in way of <see cref="Type.IsAssignableFrom"/> or <paramref name="baseType"/> should be underlying type of enumeration, when <paramref name="derivedType"/> is enumeration.
        ''' <para>In case <paramref name="baseType"/> and <paramref name="derivedType"/> are swapped, negative value is returned.</para>
        ''' <para>Following rules apply</para>
        ''' <list type="table"><listheader><term>Rule</term><description>Return value</description></listheader>
        ''' <item><term><paramref name="baseType"/> equals to <paramref name="derivedType"/></term><description>0 (zero)</description></item>
        ''' <item><term><paramref name="baseType"/> is not assignable from <paramref name="derivedType"/>, but <paramref name="derivedType"/> is assignable from <paramref name="baseType"/></term><description>Function is called with parameters swapped and negated result is returned.</description></item>
        ''' <item><term><paramref name="derivedType"/> is enum and its underlying type equals to <paramref name="baseType"/></term><description>1</description></item>
        ''' <item><term><paramref name="baseType"/> is enum and its underlying type equals to <paramref name="derivedType"/></term><description>-1</description></item>
        ''' <item><term><paramref name="baseType"/> is <see cref="Object"/></term><description><see cref="Integer.MaxValue"/></description></item>
        ''' <item><term><paramref name="baseType"/> is <see cref="ValueType"/> and <paramref name="derivedType"/> is value type</term><description><see cref="Integer.MaxValue"/> - 1</description></item>
        ''' <item><term><paramref name="baseType"/> is class and <paramref name="derivedType"/> derives from <paramref name="baseType"/></term><description>Number of inheritance levels between <paramref name="derivedType"/> and <paramref name="baseType"/>. 1 direct inheritance, 2 when <paramref name="derivedType"/> directly derives from class which directly derives from <paramref name="baseType"/> etc.</description></item>
        ''' <item><term><paramref name="baseType"/> is interface and <paramref name="derivedType"/> directly implements it</term><description><see cref="Integer.MaxValue"/> - 4</description></item>
        ''' <item><term><paramref name="baseType"/> is interface and <paramref name="derivedType"/> derives from type which implements it</term><see cref="Integer.MaxValue"/> - 3</item>
        ''' <item><term><paramref name="baseType"/> is generic parameter and <paramref name="derivedType"/> is one of its constraints</term><description>1</description></item>
        ''' <item><term><paramref name="baseType"/> is generic parameter and it has constraint which is not <paramref name="derivedType"/>, but is assignable from <paramref name="derivedType"/></term>Function is called for the constraint and <paramref name="derivedType"/> and its result is incremented by 1. Minimum from all these situations (in case more constraints is assignable from <paramref name="derivedType"/>) is returned.</item>
        ''' <item><term>None of 2 situations above are true and <paramref name="baseType"/> is generic parameter assignable from <paramref name="derivedType"/></term><description><see cref="Integer.MaxValue"/> - 4</description></item>
        ''' <item><term>Neither <paramref name="baseType"/> is assignable from <paramref name="derivedType"/> nor <paramref name="derivedType"/> is assignable from <paramref name="baseType"/> and neither <paramref name="baseType"/> is underlying type of enumeration <paramref name="derivedType"/> nor <paramref name="derivedType"/> is underlying type of enumeration <paramref name="baseType"/></term><description><see cref="ArgumentException"/> is thrown.</description></item>
        ''' </list></remarks>
        ''' <exception cref="ArgumentException">Types <paramref name="baseType"/> and <paramref name="derivedType"/> are not related in terms of class inheritance, iterface implementation, generic constraints and enum underlying type.</exception>
        ''' <seelaso cref="Type.IsAssignableFrom"/><seelaso cref="Type.IsInterface"/><seelaso cref="Type.IsValueType"/><seelaso cref="Type.Equals"/><seelaso cref="GetImplementedInterfaces"/><seelaso cref="Type.IsGenericParameter"/><seelaso cref="Type.GetGenericParameterConstraints"/><seelaso cref="[Enum].GetUnderlyingType"/>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Function ComputeDistance(ByVal baseType As Type, ByVal derivedType As Type) As Integer
            'Simple tests
            If baseType.Equals(derivedType) Then Return 0
            If Not baseType.IsAssignableFrom(derivedType) AndAlso derivedType.IsAssignableFrom(baseType) Then _
                Return -ComputeDistance(derivedType, baseType)
            If derivedType.IsEnum AndAlso baseType.Equals([Enum].GetUnderlyingType(derivedType)) Then Return 1
            If baseType.IsEnum AndAlso derivedType.Equals([Enum].GetUnderlyingType(baseType)) Then Return -1
            If Not baseType.IsAssignableFrom(derivedType) Then Throw New ArgumentException(ResourcesT.Exceptions.Types0And1AreNotCompatible.f(baseType.FullName, derivedType.FullName))
            If baseType.Equals(GetType(Object)) Then Return Integer.MaxValue
            If baseType.Equals(GetType(ValueType)) AndAlso baseType.IsValueType Then Return Integer.MaxValue - 1
            If Not baseType.IsInterface AndAlso Not baseType.IsGenericParameter Then
                'Base class lookup
                Dim Level = 1I
                Dim CurrentBase = derivedType.BaseType
                While CurrentBase IsNot Nothing
                    If CurrentBase.Equals(GetType(Object)) Then Return Integer.MaxValue 'Shoudl not happen
                    If CurrentBase.Equals(baseType) Then Return Level
                    Level += 1
                    CurrentBase = CurrentBase.BaseType
                End While
            ElseIf baseType.IsInterface Then
                'Interface lookup
                For Each DirectInterface In derivedType.GetImplementedInterfaces(False)
                    If DirectInterface.Equals(baseType) Then Return Integer.MaxValue - 4
                Next
                For Each IndirectInterface In derivedType.GetImplementedInterfaces(True)
                    If IndirectInterface.Equals(baseType) Then Return Integer.MaxValue - 3
                Next
            ElseIf baseType.IsGenericParameter Then
                'Generic constraint lookup
                For Each Constraint In baseType.GetGenericParameterConstraints
                    If Constraint.Equals(baseType) Then Return 1
                Next
                Dim Min As Integer?
                For Each Constraint In baseType.GetGenericParameterConstraints
                    If Constraint.IsGenericParameter AndAlso Constraint.IsAssignableFrom(derivedType) Then
                        Dim Current = ComputeDistance(Constraint, derivedType) + 1
                        If (Not Min.HasValue OrElse Min > Current) AndAlso Current >= 0 Then Min = Current
                    End If
                Next
                If Min.HasValue Then Return Min
                If baseType.IsAssignableFrom(derivedType) Then Return Integer.MaxValue - 4
            End If
            Throw New InvalidOperationException 'SHould not hapen
        End Function


        'TODO: Get any operator
        'Public Function GetBinaryOperatorDelegate(ByVal [operator] As Operators, ByVal type1 As Type, ByVal type2 As Type) As [Delegate]

        'End Function
        'Public Function GetBinaryOperator(ByVal [operator] As Operators, ByVal type1 As Type, ByVal type2 As Type) As Func(Of Object, Object, Object)
        '    Return GetBinaryOperatorDelegate([operator], type1, type2)
        'End Function
        'Public Function GetBinaryOperator(ByVal [operator] As Operators, ByVal type As Type) As Func(Of Object, Object, Object)
        '    Return GetBinaryOperator([operator], type, type)
        'End Function
        'Public Function GetBinaryOperator(Of T1, T2)(ByVal [operator] As Operators) As Func(Of T1, T2, Object)
        '    Return GetBinaryOperatorDelegate([operator], GetType(T1), GetType(T2))
        'End Function
        'Public Function GetBinaryOperator(Of T)(ByVal [operator] As Operators) As Func(Of T, T, Object)
        '    Return GetBinaryOperator(Of T, T)([operator])
        'End Function
#End Region
#Region "IsMemberOf"
#Region "Type"
        ''' <summary>Gets value indicating if given <see cref="Type"/> or object it is declared on is member of given <see cref="Assembly"/></summary>
        ''' <param name="Type"><see cref="Type"/> to observe parent of</param>
        ''' <param name="assembly"><see cref="Assembly"/> to test if it is parent of <paramref name="Type"/></param>
        ''' <returns>True if <paramref name="assembly"/> is declared inside <paramref name="Type"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> or <paramref name="assembly"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal type As Type, ByVal assembly As Assembly) As Boolean
            If type Is Nothing Then Throw New ArgumentNullException(NameOf(type))
            If assembly Is Nothing Then Throw New ArgumentNullException(NameOf(assembly))
            Return type.Assembly.Equals(assembly)
        End Function
        ''' <summary>Gets value indicating if given <see cref="Type"/> or object it is declared on is member of given <see cref="Module"/></summary>
        ''' <param name="type"><see cref="Type"/> to observe parent of</param>
        ''' <param name="module"><see cref="Module"/> to test if it is parent of <paramref name="type"/></param>
        ''' <returns>True if <paramref name="module"/> is declared inside <paramref name="type"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="module"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal type As Type, ByVal [module] As [Module]) As Boolean
            If type Is Nothing Then Throw New ArgumentNullException(NameOf(type))
            If [module] Is Nothing Then Throw New ArgumentNullException(NameOf([module]))
            Return type.Module.Equals([module])
        End Function
        ''' <summary>Gets value indicating if given <see cref="Type"/> or object it is declared on is member of given <see cref="Type"/></summary>
        ''' <param name="type"><see cref="Type"/> to observe parent of</param>
        ''' <param name="declaringType"><see cref="Type"/> to test if it is parent of <paramref name="type"/></param>
        ''' <returns>True if <paramref name="declaringType"/> is declared inside <paramref name="type"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="declaringType"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal type As Type, ByVal declaringType As Type) As Boolean
            If type Is Nothing Then Throw New ArgumentNullException(NameOf(type))
            If declaringType Is Nothing Then Throw New ArgumentNullException(NameOf(declaringType))
            If type.IsNested Then
                Return type.DeclaringType.Equals(declaringType) OrElse type.DeclaringType.IsMemberOf(declaringType)
            ElseIf type.IsGenericParameter AndAlso type.DeclaringType IsNot Nothing Then
                Return type.DeclaringType.Equals(declaringType) OrElse type.DeclaringType.IsMemberOf(declaringType)
            ElseIf type.IsGenericParameter AndAlso type.DeclaringMethod IsNot Nothing Then
                Return type.DeclaringMethod.IsMemberOf(declaringType)
            Else : Return False
            End If
        End Function
        ''' <summary>Gets value indicating if given <see cref="Type"/> or object it is declared on is member of given <see cref="NamespaceInfo"/></summary>
        ''' <param name="type"><see cref="Type"/> to observe parent of</param>
        ''' <param name="namespace"><see cref="NamespaceInfo"/> to test if it is parent of <paramref name="type"/></param>
        ''' <returns>True if <paramref name="namespace"/> is declared inside <paramref name="type"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="namespace"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal type As Type, ByVal [namespace] As NamespaceInfo) As Boolean
            If type Is Nothing Then Throw New ArgumentNullException(NameOf(type))
            If [namespace] Is Nothing Then Throw New ArgumentNullException(NameOf([namespace]))
            If type.IsNested Then Return type.DeclaringType.IsMemberOf([namespace])
            For Each type In [namespace].GetTypes
                If type.Equals(type) Then Return True
            Next
            Return False
        End Function
        ''' <summary>Gets value indicating if given <see cref="Type"/> or object it is declared on is member of given <see cref="MethodInfo"/></summary>
        ''' <param name="type"><see cref="Type"/> to observe parent of</param>
        ''' <param name="method"><see cref="MethodInfo"/> to test if it is parent of <paramref name="type"/></param>
        ''' <returns>True if <paramref name="method"/> is declared inside <paramref name="type"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="method"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal type As Type, ByVal method As MethodInfo) As Boolean
            If type Is Nothing Then Throw New ArgumentNullException(NameOf(type))
            If method Is Nothing Then Throw New ArgumentNullException(NameOf(method))
            Return type.IsGenericParameter AndAlso type.DeclaringMethod IsNot Nothing AndAlso type.DeclaringMethod.Equals(method)
        End Function
#End Region
        ''' <summary>Gets value indicating if given <see cref="NamespaceInfo"/> or object it is declared on is member of given <see cref="NamespaceInfo"/></summary>
        ''' <param name="namespace"><see cref="NamespaceInfo"/> to observe parent of</param>
        ''' <param name="parentNamespace"><see cref="NamespaceInfo"/> to test if it is parent of <paramref name="namespace"/></param>
        ''' <returns>True if <paramref name="parentNamespace"/> is declared inside <paramref name="namespace"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="namespace"/> or <paramref name="parentNamespace"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal [namespace] As NamespaceInfo, ByVal parentNamespace As NamespaceInfo) As Boolean
            If [namespace] Is Nothing Then Throw New ArgumentNullException(NameOf([namespace]))
            If parentNamespace Is Nothing Then Throw New ArgumentNullException(NameOf(parentNamespace))
            For Each ns In parentNamespace.GetNamespaces
                If ns.Equals([namespace]) Then Return True
            Next
            Return False
        End Function
        ''' <summary>Gets value indicating if given <see cref="Module"/> or object it is declared on is member of given <see cref="Assembly"/></summary>
        ''' <param name="module"><see cref="Module"/> to observe parent of</param>
        ''' <param name="assembly"><see cref="Assembly"/> to test if it is parent of <paramref name="module"/></param>
        ''' <returns>True if <paramref name="assembly"/> is declared inside <paramref name="module"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="module"/> or <paramref name="assembly"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal [module] As [Module], ByVal assembly As Assembly) As Boolean
            If [module] Is Nothing Then Throw New ArgumentNullException(NameOf([module]))
            If assembly Is Nothing Then Throw New ArgumentNullException(NameOf(assembly))
            Return [module].Assembly.Equals(assembly)
        End Function
#Region "IsMemberOf"
        ''' <summary>Gets value indicating if given <see cref="MemberInfo"/> or object it is declared on or is member of given <see cref="Type"/></summary>
        ''' <param name="member"><see cref="MemberInfo"/> to observe parent of</param>
        ''' <param name="type"><see cref="Type"/> to test if it is parent of <paramref name="member"/></param>
        ''' <returns>True if <paramref name="type"/> is declared inside <paramref name="member"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> or <paramref name="type"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal member As MemberInfo, ByVal type As Type) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            If type Is Nothing Then Throw New ArgumentNullException(NameOf(type))
            If TypeOf member Is Type Then Return DirectCast(member, Type).IsMemberOf(type)
            If member.DeclaringType Is Nothing Then Return False
            Return member.DeclaringType.Equals(type) OrElse member.DeclaringType.IsMemberOf(type)
        End Function
        ''' <summary>Gets value indicating if given <see cref="MemberInfo"/> or object it is declared on is member of given <see cref="Module"/></summary>
        ''' <param name="member"><see cref="MemberInfo"/> to observe parent of</param>
        ''' <param name="module"><see cref="Module"/> to test if it is parent of <paramref name="member"/></param>
        ''' <returns>True if <paramref name="module"/> is declared inside <paramref name="member"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> or <paramref name="module"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal member As MemberInfo, ByVal [module] As [Module]) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            If [module] Is Nothing Then Throw New ArgumentNullException(NameOf([module]))
            Return member.Module.Equals([module])
        End Function
        ''' <summary>Gets value indicating if given <see cref="MemberInfo"/> or object it is declared on is member of given <see cref="Assembly"/></summary>
        ''' <param name="member"><see cref="MemberInfo"/> to observe parent of</param>
        ''' <param name="assembly"><see cref="Assembly"/> to test if it is parent of <paramref name="member"/></param>
        ''' <returns>True if <paramref name="assembly"/> is declared inside <paramref name="member"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> or <paramref name="assembly"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camleCase</version>
        <Extension()> Public Function IsMemberOf(ByVal member As MemberInfo, ByVal assembly As Assembly) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            If assembly Is Nothing Then Throw New ArgumentNullException(NameOf(assembly))
            Return member.Module.IsMemberOf(assembly)
        End Function
        ''' <summary>Gets value indicating if given <see cref="MemberInfo"/> or object it is declared on is member of given <see cref="NamespaceInfo"/></summary>
        ''' <param name="member"><see cref="MemberInfo"/> to observe parent of</param>
        ''' <param name="namespace"><see cref="NamespaceInfo"/> to test if it is parent of <paramref name="member"/></param>
        ''' <returns>True if <paramref name="namespace"/> is declared inside <paramref name="member"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> or <paramref name="namespace"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camleCase</version>
        <Extension()> Public Function IsMemberOf(ByVal member As MemberInfo, ByVal [namespace] As NamespaceInfo) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            If [namespace] Is Nothing Then Throw New ArgumentNullException(NameOf([namespace]))
            If TypeOf member Is Type Then
                Return DirectCast(member, Type).IsMemberOf([namespace])
            ElseIf member.DeclaringType Is Nothing AndAlso (TypeOf member Is MethodInfo OrElse TypeOf member Is FieldInfo) Then
                Return If(TypeOf member Is MethodInfo, DirectCast(member, MethodInfo).GetNamespace, DirectCast(member, FieldInfo).GetNamespace).Equals([namespace])
            ElseIf member.DeclaringType Is Nothing Then
                Return member.DeclaringType.IsMemberOf([namespace])
            End If
            For Each Type In [namespace].GetTypes(False)
                If member.IsMemberOf(Type) Then Return True
            Next
            For Each Method In [namespace].GetMethods(BindingFlags.NonPublic Or BindingFlags.Public)
                If member.IsMemberOf(Method) Then Return True
            Next
            Return False
        End Function
        ''' <summary>Gets value indicating if given <see cref="NamespaceInfo"/> or object it is declared on is member of given <see cref="Module"/></summary>
        ''' <param name="namespace"><see cref="NamespaceInfo"/> to observe parent of</param>
        ''' <param name="module"><see cref="Module"/> to test if it is parent of <paramref name="namespace"/></param>
        ''' <returns>True if <paramref name="module"/> is declared inside <paramref name="namespace"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="namespace"/> or <paramref name="module"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal [namespace] As NamespaceInfo, ByVal [module] As [Module]) As Boolean
            If [namespace] Is Nothing Then Throw New ArgumentNullException(NameOf([namespace]))
            If [module] Is Nothing Then Throw New ArgumentNullException(NameOf([module]))
            Return [namespace].Module.Equals([module])
        End Function
        ''' <summary>Gets value indicating if given <see cref="NamespaceInfo"/> or object it is declared on is member of given <see cref="Assembly"/></summary>
        ''' <param name="namespace"><see cref="NamespaceInfo"/> to observe parent of</param>
        ''' <param name="assembly"><see cref="Assembly"/> to test if it is parent of <paramref name="namespace"/></param>
        ''' <returns>True if <paramref name="assembly"/> is declared inside <paramref name="namespace"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="namespace"/> or <paramref name="assembly"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camleCase</version>
        <Extension()> Public Function IsMemberOf(ByVal [namespace] As NamespaceInfo, ByVal assembly As Assembly) As Boolean
            If [namespace] Is Nothing Then Throw New ArgumentNullException(NameOf([namespace]))
            If assembly Is Nothing Then Throw New ArgumentNullException(NameOf(assembly))
            Return [namespace].Module.IsMemberOf(assembly)
        End Function
        ''' <summary>Gets value indicating if given <see cref="MethodInfo"/> or object it is declared on is member of given <see cref="PropertyInfo"/></summary>
        ''' <param name="method"><see cref="MethodInfo"/> to observe parent of</param>
        ''' <param name="property"><see cref="PropertyInfo"/> to test if it is parent of <paramref name="method"/></param>
        ''' <returns>True if <paramref name="property"/> is declared inside <paramref name="method"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="method"/> or <paramref name="property"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camleCase</version>
        <Extension()> Public Function IsMemberOf(ByVal method As MethodInfo, ByVal [property] As PropertyInfo) As Boolean
            If method Is Nothing Then Throw New ArgumentNullException(NameOf(method))
            If [property] Is Nothing Then Throw New ArgumentNullException(NameOf([property]))
            For Each acc In [property].GetAccessors(True)
                If acc.Equals(method) Then Return True
            Next
            Return False
        End Function
        ''' <summary>Gets value indicating if given <see cref="MethodInfo"/> or object it is declared on is member of given <see cref="EventInfo"/></summary>
        ''' <param name="method"><see cref="MethodInfo"/> to observe parent of</param>
        ''' <param name="event"><see cref="EventInfo"/> to test if it is parent of <paramref name="method"/></param>
        ''' <returns>True if <paramref name="event"/> is declared inside <paramref name="method"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="method"/> or <paramref name="event"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camleCase</version>
        <Extension()> Public Function IsMemberOf(ByVal method As MethodInfo, ByVal [event] As EventInfo) As Boolean
            If method Is Nothing Then Throw New ArgumentNullException(NameOf(method))
            If [event] Is Nothing Then Throw New ArgumentNullException(NameOf([event]))
            For Each acc In [event].GetAccessors(True)
                If acc.Equals(method) Then Return True
            Next
            Return False
        End Function
        ''' <summary>Gets value indicating if given <see cref="MemberInfo"/> or object it is declared on is member of given <see cref="MethodInfo"/></summary>
        ''' <param name="member"><see cref="MemberInfo"/> to observe parent of</param>
        ''' <param name="method"><see cref="MethodInfo"/> to test if it is parent of <paramref name="member"/></param>
        ''' <returns>True if <paramref name="method"/> is declared inside <paramref name="member"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> or <paramref name="method"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camleCase</version>
        <Extension()> Public Function IsMemberOf(ByVal member As MemberInfo, ByVal method As MethodInfo) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            If method Is Nothing Then Throw New ArgumentNullException(NameOf(method))
            If member.DeclaringType Is Nothing Then Return False
            Return member.DeclaringType.IsMemberOf(method)
        End Function
        ''' <summary>Gets value indicating if given <see cref="MemberInfo"/> or object it is declared on is member of given <see cref="PropertyInfo"/></summary>
        ''' <param name="member"><see cref="MemberInfo"/> to observe parent of</param>
        ''' <param name="property"><see cref="PropertyInfo"/> to test if it is parent of <paramref name="member"/></param>
        ''' <returns>True if <paramref name="property"/> is declared inside <paramref name="member"/></returns>
        ''' <remarks>This function is unlikely to return true when <paramref name="member"/> is not <see cref="MethodInfo"/> because it is improbable that generic property exists.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> or <paramref name="property"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camleCase</version>
        <Extension(), EditorBrowsable(EditorBrowsableState.Never)>
        Public Function IsMemberOf(ByVal member As MemberInfo, ByVal [property] As PropertyInfo) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            If [property] Is Nothing Then Throw New ArgumentNullException(NameOf([property]))
            If member.DeclaringType Is Nothing Then Return False
            If TypeOf member Is MethodInfo AndAlso DirectCast(member, MethodInfo).IsMemberOf([property]) Then Return True
            Dim dc As Type = If(TypeOf member Is Type, member, member.DeclaringType)
            If dc Is Nothing Then Return False
            For Each Method In [property].GetAccessors(True)
                If dc.IsMemberOf(Method) Then Return True
            Next
            Return False
        End Function
        ''' <summary>Gets value indicating if given <see cref="MemberInfo"/> or object it is declared on is member of given <see cref="EventInfo"/></summary>
        ''' <param name="member"><see cref="MemberInfo"/> to observe parent of</param>
        ''' <param name="event"><see cref="EventInfo"/> to test if it is parent of <paramref name="member"/></param>
        ''' <returns>True if <paramref name="event"/> is declared inside <paramref name="member"/></returns>
        ''' <remarks>This function is unlikely to return true when <paramref name="member"/> isnot <see cref="MethodInfo"/> because it is improbable that generic event exists.</remarks>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> or <paramref name="event"/> is null</exception>
        <Extension(), EditorBrowsable(EditorBrowsableState.Never)> Public Function IsMemberOf(ByVal member As MemberInfo, ByVal [event] As EventInfo) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            If [event] Is Nothing Then Throw New ArgumentNullException(NameOf([event]))
            If member.DeclaringType Is Nothing Then Return False
            If member.DeclaringType Is Nothing Then Return False
            If TypeOf member Is MethodInfo AndAlso DirectCast(member, MethodInfo).IsMemberOf([event]) Then Return True
            Dim dc As Type = If(TypeOf member Is Type, member, member.DeclaringType)
            If dc Is Nothing Then Return False
            Try
                For Each Method In [event].GetAccessors(True)
                    If dc.IsMemberOf(Method) Then Return True
                Next
            Catch ex As MethodAccessException
                For Each Method In [event].GetAccessors(False)
                    If dc.IsMemberOf(Method) Then Return True
                Next
            End Try
            Return False
        End Function
        ''' <summary>Gets value indicating if given <see cref="ParameterInfo"/> or object it is declared on is member of given <see cref="MethodInfo"/></summary>
        ''' <param name="param"><see cref="ParameterInfo"/> to observe parent of</param>
        ''' <param name="method"><see cref="MethodInfo"/> to test if it is parent of <paramref name="param"/></param>
        ''' <returns>True if <paramref name="method"/> is declared inside <paramref name="param"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="param"/> or <paramref name="method"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal param As ParameterInfo, ByVal method As MethodInfo) As Boolean
            If param Is Nothing Then Throw New ArgumentNullException(NameOf(param))
            If method Is Nothing Then Throw New ArgumentNullException(NameOf(method))
            For Each mParam In method.GetParameters
                If mParam.Equals(param) Then Return True
            Next
            Return param.Equals(method.ReturnParameter)
        End Function
        ''' <summary>Gets value indicating if given <see cref="ParameterInfo"/> or object it is declared on is member of given <see cref="Type"/></summary>
        ''' <param name="param"><see cref="ParameterInfo"/> to observe parent of</param>
        ''' <param name="type"><see cref="Type"/> to test if it is parent of <paramref name="param"/></param>
        ''' <returns>True if <paramref name="type"/> is declared inside <paramref name="param"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="param"/> or <paramref name="type"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal param As ParameterInfo, ByVal type As Type) As Boolean
            If param Is Nothing Then Throw New ArgumentNullException(NameOf(param))
            If type Is Nothing Then Throw New ArgumentNullException(NameOf(type))
            Return param.Member.IsMemberOf(type)
        End Function
        ''' <summary>Gets value indicating if given <see cref="ParameterInfo"/> or object it is declared on is member of given <see cref="MemberInfo"/></summary>
        ''' <param name="param"><see cref="ParameterInfo"/> to observe parent of</param>
        ''' <param name="member"><see cref="MemberInfo"/> to test if it is parent of <paramref name="param"/></param>
        ''' <returns>True if <paramref name="member"/> is declared inside <paramref name="param"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="param"/> or <paramref name="member"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal param As ParameterInfo, ByVal member As MemberInfo) As Boolean
            If param Is Nothing Then Throw New ArgumentNullException(NameOf(param))
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            Return param.Member.Equals(member) OrElse param.Member.IsMemberOf(member)
        End Function
        ''' <summary>Gets value indicating if given <see cref="ParameterInfo"/> or object it is declared on is member of given <see cref="NamespaceInfo"/></summary>
        ''' <param name="param"><see cref="ParameterInfo"/> to observe parent of</param>
        ''' <param name="namespace"><see cref="NamespaceInfo"/> to test if it is parent of <paramref name="param"/></param>
        ''' <returns>True if <paramref name="namespace"/> is declared inside <paramref name="param"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="param"/> or <paramref name="namespace"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal param As ParameterInfo, ByVal [namespace] As NamespaceInfo) As Boolean
            If param Is Nothing Then Throw New ArgumentNullException(NameOf(param))
            If [namespace] Is Nothing Then Throw New ArgumentNullException(NameOf([namespace]))
            Return param.Member.IsMemberOf([namespace])
        End Function
        ''' <summary>Gets value indicating if given <see cref="ParameterInfo"/> or object it is declared on is member of given <see cref="Module"/></summary>
        ''' <param name="param"><see cref="ParameterInfo"/> to observe parent of</param>
        ''' <param name="module"><see cref="Module"/> to test if it is parent of <paramref name="param"/></param>
        ''' <returns>True if <paramref name="module"/> is declared inside <paramref name="param"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="param"/> or <paramref name="module"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal param As ParameterInfo, ByVal [module] As [Module]) As Boolean
            If param Is Nothing Then Throw New ArgumentNullException(NameOf(param))
            If [module] Is Nothing Then Throw New ArgumentNullException(NameOf([module]))
            Return param.Member.IsMemberOf([module])
        End Function
        ''' <summary>Gets value indicating if given <see cref="ParameterInfo"/> or object it is declared on is member of given <see cref="Assembly"/></summary>
        ''' <param name="param"><see cref="ParameterInfo"/> to observe parent of</param>
        ''' <param name="assembly"><see cref="Assembly"/> to test if it is parent of <paramref name="param"/></param>
        ''' <returns>True if <paramref name="assembly"/> is declared inside <paramref name="param"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="param"/> or <paramref name="assembly"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal param As ParameterInfo, ByVal assembly As Assembly) As Boolean
            If param Is Nothing Then Throw New ArgumentNullException(NameOf(param))
            If assembly Is Nothing Then Throw New ArgumentNullException(NameOf(assembly))
            Return param.Member.IsMemberOf(assembly)
        End Function
#End Region
#Region "Generic"
        ''' <summary>Gets value indicating if given <see cref="ParameterInfo"/> or object it is declared on is member of given CLI object</summary>
        ''' <param name="param"><see cref="ParameterInfo"/> to observe parent of</param>
        ''' <param name="parent"><see cref="Object"/> to test if it is parent of <paramref name="param"/></param>
        ''' <returns>True if <paramref name="parent"/> is declared inside <paramref name="param"/></returns>
        ''' <remarks>Supported types of <paramref name="parent"/> are <see cref="Assembly"/>, <see cref="Module"/>, <see cref="NamespaceInfo"/>, <see cref="MemberInfo"/>. For any other type, this function returns false.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="param"/> or <paramref name="parent"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        ''' <version version="1.6.0">Fix: <see cref="ArgumentNullException"/> with wrong <see cref="ArgumentException.ParamName"/> thrown when <paramref name="param"/> was null.</version>
        <Extension()> Public Function IsMemberOf(ByVal param As ParameterInfo, ByVal parent As Object) As Boolean
            If param Is Nothing Then Throw New ArgumentNullException(NameOf(param))
            If parent Is Nothing Then Throw New ArgumentNullException(NameOf(parent))
            If TypeOf parent Is Assembly Then : Return param.IsMemberOf(DirectCast(parent, Assembly))
            ElseIf TypeOf parent Is [Module] Then : Return param.IsMemberOf(DirectCast(parent, [Module]))
            ElseIf TypeOf parent Is NamespaceInfo Then : Return param.IsMemberOf(DirectCast(parent, NamespaceInfo))
            ElseIf TypeOf parent Is Type Then : Return param.IsMemberOf(DirectCast(parent, Type))
            ElseIf TypeOf parent Is MemberInfo Then : Return param.IsMemberOf(DirectCast(parent, MethodInfo))
            Else : Return False
            End If
        End Function
        ''' <summary>Gets value indicating if given <see cref="MemberInfo"/> or object it is declared on is member of given CLI object</summary>
        ''' <param name="member"><see cref="MemberInfo"/> to observe parent of</param>
        ''' <param name="parent"><see cref="Object"/> to test if it is parent of <paramref name="member"/></param>
        ''' <returns>True if <paramref name="parent"/> is declared inside <paramref name="member"/></returns>
        ''' <remarks>Supported types of <paramref name="parent"/> are <see cref="Assembly"/>, <see cref="Module"/>, <see cref="NamespaceInfo"/>, <see cref="Type"/>, <see cref="MethodInfo"/>, <see cref="PropertyInfo"/>, <see cref="EventInfo"/>. For any other type, this function returns false.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="member"/> or <paramref name="parent"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal member As MemberInfo, ByVal parent As Object) As Boolean
            If member Is Nothing Then Throw New ArgumentNullException(NameOf(member))
            If parent Is Nothing Then Throw New ArgumentNullException(NameOf(parent))
            If TypeOf parent Is Assembly Then : Return member.IsMemberOf(DirectCast(parent, Assembly))
            ElseIf TypeOf parent Is [Module] Then : Return member.IsMemberOf(DirectCast(parent, [Module]))
            ElseIf TypeOf parent Is NamespaceInfo Then : Return member.IsMemberOf(DirectCast(parent, NamespaceInfo))
            ElseIf TypeOf parent Is Type Then : Return member.IsMemberOf(DirectCast(parent, Type))
            ElseIf TypeOf parent Is MethodInfo Then : Return member.IsMemberOf(DirectCast(parent, MethodInfo))
            ElseIf TypeOf parent Is PropertyInfo Then : Return member.IsMemberOf(DirectCast(parent, PropertyInfo))
            ElseIf TypeOf parent Is EventInfo Then : Return member.IsMemberOf(DirectCast(parent, EventInfo))
            Else : Return False
            End If
        End Function
        ''' <summary>Gets value indicating if given <see cref="NamespaceInfo"/> or object it is declared on is member of given CLI object</summary>
        ''' <param name="namespace"><see cref="NamespaceInfo"/> to observe parent of</param>
        ''' <param name="parent"><see cref="Object"/> to test if it is parent of <paramref name="namespace"/></param>
        ''' <returns>True if <paramref name="parent"/> is declared inside <paramref name="namespace"/></returns>
        ''' <remarks>Supported types of <paramref name="parent"/> are <see cref="Assembly"/>, <see cref="Module"/>, <see cref="NamespaceInfo"/>. For any other type, this function returns false.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="namespace"/> or <paramref name="parent"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsMemberOf(ByVal [namespace] As NamespaceInfo, ByVal parent As Object) As Boolean
            If [namespace] Is Nothing Then Throw New ArgumentNullException(NameOf([namespace]))
            If parent Is Nothing Then Throw New ArgumentNullException(NameOf(parent))
            If TypeOf parent Is Assembly Then : Return [namespace].IsMemberOf(DirectCast(parent, Assembly))
            ElseIf TypeOf parent Is [Module] Then : Return [namespace].IsMemberOf(DirectCast(parent, [Module]))
            ElseIf TypeOf parent Is NamespaceInfo Then : Return [namespace].IsMemberOf(DirectCast(parent, NamespaceInfo))
            Else : Return False
            End If
        End Function
        ''' <summary>Gets value indicating if given <see cref="Module"/> or object it is declared on is member of given CLI object</summary>
        ''' <param name="module"><see cref="Module"/> to observe parent of</param>
        ''' <param name="parent"><see cref="Object"/> to test if it is parent of <paramref name="module"/></param>
        ''' <returns>True if <paramref name="parent"/> is declared inside <paramref name="module"/></returns>
        ''' <remarks>Supported types of <paramref name="parent"/> are <see cref="Assembly"/>. For any other type, this function returns false.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="module"/> or <paramref name="parent"/> is null</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        ''' <version version="1.6.0">Fix: <see cref="ArgumentNullException"/> with wrong <see cref="ArgumentException.ParamName"/> thrown when <paramref name="module"/> was null.</version>
        <Extension()> Public Function IsMemberOf(ByVal [module] As [Module], ByVal parent As Object) As Boolean
            If [module] Is Nothing Then Throw New ArgumentNullException(NameOf([module]))
            If parent Is Nothing Then Throw New ArgumentNullException(NameOf(parent))
            If TypeOf parent Is Assembly Then : Return [module].IsMemberOf(DirectCast(parent, Assembly))
            Else : Return False
            End If
        End Function
#End Region
#End Region
        'Note: This method was commented because it has equivalent in Type.GetBaseDefinition. It was tested to work in exactly same way as that mehod. It was not tested to work with generic methods (it was tested to work with generic types).
        '''' <summary>Searches for method given method overrides</summary>
        '''' <param name="Method">Method do find method it overrides</param>
        '''' <returns>Method in base class (or base base class etc.) of class <paramref name="Method"/> is defined in <paramref name="Method"/> overrides; null when no such method is found</returns>
        '''' <exception cref="ArgumentNullException"><paramref name="Method"/> is null</exception>
        '''' <exception cref="ArgumentException"><paramref name="Method"/> is global method (its <see cref="MethodInfo.DeclaringType"/> is null)</exception>
        '''' <remarks>This function searches for method with same name and signature as <paramref name="Method"/> has. Search is done in base class of class <paramref name="Method"/> is defined in, if not found in base class of base class etc.</remarks>
        '''' <seelaso cref="MethodInfo.GetBaseDefinition"/>
        '''' <version version="1.5.2">Function introduced</version>
        '<Extension()> _
        'Public Function GetBaseClassMethod(ByVal Method As MethodInfo) As MethodInfo
        '    'Unit test done
        '    If Method Is Nothing Then Throw New ArgumentNullException(nameof(Method))
        '    If Method.DeclaringType Is Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.CannotGetBaseClassMethodOfGlobalMethod)
        '    Dim type As Type = Method.DeclaringType
        '    Dim base As Type = type.BaseType
        '    Dim ret As MethodInfo = Nothing
        '    If (Method.Attributes And MethodAttributes.NewSlot) = MethodAttributes.NewSlot Then Return Nothing 'Shadows
        '    If Method.IsStatic Then Return Nothing
        '    Do Until base Is Nothing
        '        If base Is Nothing Then Return Nothing
        '        For Each BaseMethod In base.GetMethods(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.DeclaredOnly) '
        '            If BaseMethod.Name.Equals(Method.Name, StringComparison.InvariantCulture) AndAlso BaseMethod.IsVirtual AndAlso Not BaseMethod.IsFinal Then
        '                If HasSameSignature(Method, BaseMethod, SignatureComparisonStrictness.Strict) Then
        '                    If ret IsNot Nothing Then Throw New AmbiguousMatchException()
        '                    ret = BaseMethod
        '                End If
        '            End If
        '        Next
        '        base = base.BaseType
        '        If ret IsNot Nothing Then Exit Do
        '    Loop
        '    Return ret
        'End Function
        ''' <summary>Gets method that overrides given method in given type</summary>
        ''' <param name="Method">Method to get overriding method for</param>
        ''' <param name="derivedType">Type derived from <paramref name="Method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see> to get method of (or base of)</param>
        ''' <returns><paramref name="Method"/> when <paramref name="Method"/> is not overridden in <paramref name=" derivedType"/> or one of its base classed between <paramref name="Method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see> and <paramref name="derivedType"/>;
        ''' Overriding method of <paramref name="derivedType"/> or one of its base types (between <paramref name="Method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see> and <paramref name="derivedType"/> otherwise.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Method"/> or <paramref name="derivedType"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see> is null (it is global method) -or-
        ''' <paramref name="derivedType"/> does not derive from <paramref name="Method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see>.</exception>
        ''' <remarks>Note that no type really derives from open generic type. <see cref="Type.BaseType"/> for type deriving from open generic type always returns closed generic type with generic parameters passed from deriving type to base type. This means that determining overriding method for member method of open generic type is impossible - <see cref="ArgumentException"/> is thrown when <paramref name="Method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see> is open or semi-constructed generic type, because <paramref name="derivedType"/>.<see cref="Type.BaseType">BaseType</see> never returns such type.</remarks>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()>
        Public Function GetOverridingMethod(ByVal method As MethodInfo, ByVal derivedType As Type) As MethodInfo
            If method Is Nothing Then Throw New ArgumentNullException(NameOf(method))
            If derivedType Is Nothing Then Throw New ArgumentNullException(NameOf(derivedType))
            If method.DeclaringType Is Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.CannotGetDerivedClassMethodForGlobalMethod)
            If method.DeclaringType.Equals(derivedType) Then Return method
            Dim base = derivedType.BaseType
            Do Until base Is Nothing OrElse base.Equals(method.DeclaringType)
                base = base.BaseType
            Loop
            If base Is Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.DerivedTypeDoesNotDeriveFromMethodDeclaringType)
            If method.IsStatic OrElse Not method.IsVirtual Then Return method
            Dim MethodParams = method.GetParameters
            Dim MethodGParams = method.GetGenericArguments
            Dim dtype = derivedType
            Do Until dtype.Equals(method.DeclaringType)
                For Each dMethod In dtype.GetMethods(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.DeclaredOnly)
                    If dMethod.Name.Equals(method.Name, StringComparison.InvariantCultureIgnoreCase) AndAlso dMethod.GetParameters.Length = MethodParams.Length AndAlso dMethod.GetGenericArguments.Length = MethodGParams.Length Then
                        Dim BaseDefinition = dMethod.GetBaseDefinition
                        Do
                            If BaseDefinition.Equals(method) Then Return dMethod
                            Dim newBaseDefinition = BaseDefinition.GetBaseDefinition
                            If newBaseDefinition.Equals(BaseDefinition) Then Exit Do
                            BaseDefinition = newBaseDefinition
                        Loop
                    End If
                Next
                dtype = dtype.BaseType
            Loop
            Return method
        End Function

        '''' <summary>Gets method that is physically impemented by the same method as given method in another incarnation of same generic type as method declaring type</summary>
        '''' <param name="Method">Method to get corresponding method of</param>
        '''' <param name="Type">Type to get method of. The type must be either open generic type, <paramref name="Method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see> is constructed from; or it must be (semi-)constructed generic type constructed from the same open generic type as <paramref name="Method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see>; or it must be same type as <paramref name="Method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see> (in this case this method returns <paramref name="Method"/>).</param>
        '''' <returns>Method that is physically implemented by the same method as <paramref name="Method"/>, but on another genric incarnation of <paramref name="Method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see> represented by <paramref name="Type"/></returns>
        '''' <exception cref="ArgumentNullException"><paramref name="Method"/> or <paramref name="Type"/> is null</exception>
        '''' <exception cref="ArgumentException"><paramref name="Type"/> or <paramref name="Method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see> is not generic type (open, constructed or semi-constructed) -or- <paramref name="Type"/> and <paramref name="Method"/>.<see cref="MethodInfo.DeclaringType">DeclaringType</see> are generic type created from different open generic types.</exception>
        '''' <version version="1.5.2">Function introduced</version>
        '<Extension()> Function GetSameMethod(ByVal Method As MethodInfo, ByVal Type As Type) As MethodInfo
        '    If Method Is Nothing Then Throw New ArgumentNullException(nameof(Method))
        '    If Type Is Nothing Then Throw New ArgumentNullException(nameof(Type))
        '    If Not Method.DeclaringType.IsGenericTypeDefinition AndAlso Not Method.DeclaringType.IsGenericType Then Throw New ArgumentException(ResourcesT.Exceptions.DeclaringTypeOfMethodIsNotGeneric, "Method")
        '    If Not Type.IsGenericTypeDefinition AndAlso Not Type.IsGenericType Then Throw New ArgumentException(ResourcesT.Exceptions.TypeIsNotGeneric, "Type")
        '    If Not Type.GetGenericTypeDefinition.Equals(Method.DeclaringType.GetGenericTypeDefinition) Then Throw New ArgumentException(ResourcesT.Exceptions.MethodDeclaringTypeAndTypeToGetMethodOfMustBeCreated)
        '    For Each tmethod In Type.GetMethods(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.DeclaredOnly)
        '        'TODO: COmpare signatures
        '    Next
        'End Function

        ''' <summary>Gets value indicating if constructed or semi-constructed generic type is constructed from given open semi-constructed generic type</summary>
        ''' <param name="Instance">Constructed or semi-constructed generic type</param>
        ''' <param name="Definition">Open or semi-constructed generic type</param>
        ''' <returns>True when <paramref name="Instance"/> represents closed or semi-constructed generic type created from <paramref name="Definition"/> by replacing all not-yet-replaced type arguments by corresponding arguments of <paramref name="Instance"/>.</returns>
        ''' <remarks>
        ''' Terminology note:
        ''' <list type="table"><listheader><term>term</term><description>description</description></listheader>
        ''' <item><term>Generic type</term><description>Type that accepts one or more generic arguments, such as <see cref="List(Of T)"/></description></item>
        ''' <item><term>Open generic type</term><description>Generic type without type arguments supplied. In Visual Basic you can obtain such type by <c>GetType(List(Of ))</c> or <c>GetType(Dictionary(Of ,))</c></description></item>
        ''' <item><term>Closed generic type</term><description>Generic type with all the type arguments supplied. Note: Supplied types can be generic arguments of another (even open) generic type, such as derived class. So, base class of derived class is never open generic type, it's always closed generic type with appropriate type arguments supplied.</description></item>
        ''' <item><term>Constructed generic type</term><description>Same meaning as closed generic type</description></item>
        ''' <item><term>Semi-constructed generic type</term><description>Generic type with some type arguments supplied and some not supplied. E.g. declaring type of nested generic type (with own type arguments of nested type) is semi-constructed.</description></item>
        ''' <item><term>Generic type definition</term><description>Generic type definition is open generic type</description></item>
        ''' <item><term>Generic type instance</term><description>Generic type instance is constructed generic type</description></item>
        ''' </list>
        ''' This method returns true when <paramref name="Definition"/> open generic type and <paramref name="Instance"/> is any closed or semi-constructed generic type made form it.
        ''' Or it returns tru when <paramref name="Definition"/> is semi-constructed generic type and <paramref name="Instance"/> is "more" constructed generic type which has all specified type arguments of <paramref name="Definition"/> passed (and at leas one more).
        ''' This method returns false when <paramref name="Definition"/> and <paramref name="Instance"/> represents same types.
        ''' </remarks>
        ''' <version version="1.5.2">Method introduced</version>
        ''' <version version="1.6.0">Parameters renamed to camelCase</version>
        <Extension()> Public Function IsCreatedFrom(ByVal instance As Type, ByVal definition As Type) As Boolean 'TODO:Test
            If instance Is Nothing Then Throw New ArgumentNullException(NameOf(instance))
            If definition Is Nothing Then Throw New ArgumentNullException(NameOf(definition))
            If instance.IsArray AndAlso definition.GetType.Equals(GetType(Array)) Then Return True
            If Not instance.IsGenericType Then Return False
            If Not definition.IsGenericTypeDefinition AndAlso Not definition.IsGenericType Then Return False
            If Not instance.GetGenericTypeDefinition.Equals(definition.GetGenericTypeDefinition) Then Return False
            If instance.Equals(definition) Then Return False
            If definition.IsGenericTypeDefinition Then
                Return definition.Equals(instance.GetGenericTypeDefinition)
            Else
                Dim gargs = definition.GetGenericArguments
                Dim ogargs = definition.GetGenericTypeDefinition.GetGenericArguments
                Dim cgargs = instance.GetGenericArguments
                For i As Integer = 0 To gargs.Length - 1
                    If gargs(i).Equals(ogargs(i)) Then gargs(i) = cgargs(i)
                Next
                Return definition.GetGenericTypeDefinition.MakeGenericType(gargs).Equals(instance)
            End If
        End Function

        ''' <summary>Determines if two methods have same signatures. Several levels of signature comparison are available.</summary>
        ''' <param name="a">A <see cref="MethodInfo"/></param>
        ''' <param name="b">A <see cref="MethodInfo"/></param>
        ''' <param name="strictness">Defines level of comparison</param>
        ''' <returns>True if <paramref name="a"/> and <paramref name="b"/> have same signature in meaning of <paramref name="strictness"/>; false otherwise</returns>
        ''' <remarks>Signature comparison does not include comparison of custom attributes (with exception of <see cref="InAttribute"/> and <see cref="OutAttribute"/>) and does not include comparison of method attributes (such as if it is private, public or specialname). Callig convention is ignored as well.
        ''' <para>When comparing modreqs and modopts only first-level modifiers are taken in effect. Modifiers applied onto element-type of pointer, refernce or array and onto types of generic type are ignored. This is due to limitation of <see cref="ParameterInfo.GetOptionalCustomModifiers"/> and <see cref="ParameterInfo.GetRequiredCustomModifiers"/>.</para></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="a"/> or <paramref name="b"/> is null.</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.6.0">Parameter <c>Strictness</c> renamed to <c>strictness</c></version>
        <Extension()>
        Public Function HasSameSignature(ByVal a As MethodInfo, ByVal b As MethodInfo, ByVal strictness As SignatureComparisonStrictness) As Boolean
            'Unt test done
            If a Is Nothing Then Throw New ArgumentNullException(NameOf(a))
            If b Is Nothing Then Throw New ArgumentNullException(NameOf(b))
            Dim pa = a.GetParameters
            Dim pb = b.GetParameters
            If pa.Length <> pb.Length Then Return False
            ReDim Preserve pa(pa.Length)
            ReDim Preserve pb(pb.Length)
            pa(pa.Length - 1) = a.ReturnParameter
            pb(pb.Length - 1) = b.ReturnParameter
            For i As Integer = 0 To pb.Length - 1
                If i = pb.Length - 1 AndAlso (strictness And SignatureComparisonStrictness.IgnoreReturn) Then Exit For
                Dim patypefc = pa(i).ParameterType
                Dim pbtypefc = pb(i).ParameterType
                If strictness And SignatureComparisonStrictness.TreatPointerAsReference Then
                    If patypefc.IsPointer Then patypefc = patypefc.GetElementType.MakeByRefType
                    If pbtypefc.IsPointer Then pbtypefc = pbtypefc.GetElementType.MakeByRefType
                End If
                If strictness And SignatureComparisonStrictness.IgnoreByRef Then
                    If patypefc.IsByRef Then patypefc = patypefc.GetElementType
                    If pbtypefc.IsByRef Then pbtypefc = pbtypefc.GetElementType
                End If
                If Not patypefc.Equals(pbtypefc) Then Return False
                If (strictness And SignatureComparisonStrictness.IgnoreDirection) = 0 Then
                    If pa(i).IsOut <> pb(i).IsOut Then Return False
                    If pa(i).IsIn <> pb(i).IsIn Then Return False
                End If
                If (strictness And SignatureComparisonStrictness.IgnoreModReq) = 0 Then
                    Dim moda = pa(i).GetRequiredCustomModifiers
                    Dim modb = pb(i).GetRequiredCustomModifiers
                    If moda.Length <> modb.Length Then Return False
                    For Each [mod] In moda
                        If Not modb.Contains([mod]) Then Return False
                    Next
                End If
                If (strictness And SignatureComparisonStrictness.IgnoreModOpt) = 0 Then
                    Dim moda = pa(i).GetOptionalCustomModifiers
                    Dim modb = pb(i).GetOptionalCustomModifiers
                    If moda.Length <> modb.Length Then Return False
                    For Each [mod] In moda
                        If Not modb.Contains([mod]) Then Return False
                    Next
                End If
            Next
            Return True
        End Function


        ''' <summary>Gets immediate parent code object of another code object</summary>
        ''' <param name="item">Item to get paret of</param>
        ''' <returns>Parent of <paramref name="item"/>, null if <paramref name="item"/> does not have a parent.</returns>
        ''' <remarks>
        ''' For property and event accessors returns the property/event if it's declared in the same type.
        ''' If method serves as accessor for more properties or events (in the same type), first is taken.
        ''' If method serves as accessor for property/ies and event(s (in the same type), 1ts property found takes precendence.
        ''' <para>For <see cref="Assembly"/> always returns null.</para>
        ''' </remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="item"/> is null</exception>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension()> Public Function Parent(item As ICustomAttributeProvider) As ICustomAttributeProvider
            If item Is Nothing Then Throw New ArgumentNullException(NameOf(item))
            If TypeOf item Is Assembly Then Return Nothing
            If TypeOf item Is [Module] Then Return DirectCast(item, [Module]).Assembly
            If TypeOf item Is Type Then
                Dim t As Type = item
                If t.IsGenericParameter Then Return If(t.DeclaringMethod, DirectCast(t.DeclaringType, ICustomAttributeProvider))
                If t.IsNested Then Return t.DeclaringType
                Return t.Module
            End If
            If TypeOf item Is MethodInfo Then
                Dim m As MethodInfo = item
                Dim p As ICustomAttributeProvider = m.GetProperty
                If p IsNot Nothing Then Return p
                p = m.GetEvent
                If p IsNot Nothing Then Return p
                Return If(m.DeclaringType, DirectCast(m.Module, ICustomAttributeProvider))
            End If
            If TypeOf item Is FieldInfo Then Return If(DirectCast(item, FieldInfo).DeclaringType, DirectCast(DirectCast(item, FieldInfo).Module, ICustomAttributeProvider))
            If TypeOf item Is MemberInfo Then Return DirectCast(item, MemberInfo).DeclaringType
            If TypeOf item Is ParameterInfo Then Return DirectCast(item, ParameterInfo).Member
            Return Nothing
        End Function

    End Module

    ''' <summary>Any member visibility</summary>
    ''' <seelaso cref="MethodAttributes"/><seelaso cref="TypeAttributes"/>
    ''' <remarks>Values of members of this enumeration are same as values of corresponding members of <see cref="MethodAttributes"/>.</remarks>
    Public Enum Visibility
        ''' <summary>Indicates that the member is accessible only to the current class.</summary>
        ''' <seelaso cref="MethodAttributes.[Private]"/><seelaso cref="TypeAttributes.NestedPrivate"/>
        [Private] = MethodAttributes.Private '1
        ''' <summary>Indicates that the member is accessible to any object for which this object is in scope.</summary>
        ''' <seelaso cref="MethodAttributes.[Public]"/><seelaso cref="TypeAttributes.[Public]"/><seelaso cref="TypeAttributes.NestedPublic"/>
        ''' <remarks>For <see cref="Type"/> includes <see cref="TypeAttributes.[Public]"/> and <see cref="TypeAttributes.NestedPublic"/></remarks>
        [Public] = MethodAttributes.Public '6
        ''' <summary>Indicates that the member is accessible only to members of this class and its derived classes.</summary>
        ''' <seelaso cref="MethodAttributes.Family "/><seelaso cref="TypeAttributes.NestedFamily"/>
        Family = MethodAttributes.Family '4
        ''' <summary>Indicates that the member is accessible to derived classes anywhere, as well as to any class in the assembly.</summary>
        ''' <seelaso cref="MethodAttributes.FamORAssem"/><seelaso cref="TypeAttributes.NestedFamORAssem"/>
        FamORAssem = MethodAttributes.FamORAssem  '5
        ''' <summary>Indicates that the member is accessible to members of this type and its derived types that are in this assembly only.</summary>
        ''' <seelaso cref="MethodAttributes.FamANDAssem"/><seelaso cref="TypeAttributes.NestedFamANDAssem"/>
        FamANDAssem = MethodAttributes.FamANDAssem  '2
        ''' <summary>Indicates that the member is accessible to any class of this assembly.</summary>
        ''' <seelaso cref="MethodAttributes.Assembly"/><seelaso cref="TypeAttributes.NotPublic"/><seelaso cref="TypeAttributes.NestedAssembly"/>
        ''' <remarks>For <see cref="Type"/> includes <see cref="TypeAttributes.NotPublic"/> and <see cref="TypeAttributes.NestedAssembly"/></remarks>
        Assembly = MethodAttributes.Assembly '3
    End Enum

    ''' <summary>Defines how method signature comparison is performed</summary>
    ''' <remarks>This enumeration is treaded as flags, each set or unset. Several predefined combinations of flags also exists.
    ''' <para>When <see cref="SignatureComparisonStrictness.IgnoreByRef"/> and <see cref="SignatureComparisonStrictness.TreatPointerAsReference"/> are both set:
    ''' Both - T* and T&amp; are treated as T. T*&amp; (reference to pointer) is treated as T* and T&amp;* (pointer to reference) is treated as T.</para></remarks>
    ''' <version version="1.5.2">Enumeration introduced</version>
    <Flags()>
    Public Enum SignatureComparisonStrictness
        ''' <summary>Set this flag to ignore direction of method parameter. <see cref="InAttribute"/> and <see cref="OutAttribute"/> are ignored. Does not affect testing if parameter is passed by reference or by value.</summary>
        IgnoreDirection = 1
        ''' <summary>Ignore optional modifiers on parameters (modopts). Nested modopts are always ignored i.e. modopts on pointer/reference/array/generic internal type(s).</summary>
        IgnoreModOpt = 2
        ''' <summary>Ignore required modifiers on parameters (modreqs). Nested modreqs are always ignored i.e. modreqs on pointer/reference/array/generic internal type(s).</summary>
        IgnoreModReq = 4
        ''' <summary>Ignore return value completely (ignores return type and return modopts and modreqs)</summary>
        IgnoreReturn = 8
        ''' <summary>Consider parameter passed by value and by reference to by of same type. Note: Physically the type of such parameters differs.</summary>
        IgnoreByRef = 16
        ''' <summary>Treat pointer to type (*) in same way as reference to type (&amp;, ByRef) - see <see cref="Type.IsByRef"/> and <see cref="Type.IsPointer"/>.
        ''' When combined with <see cref="IgnoreByRef"/>, pointer to type is treated as type itself.</summary>
        TreatPointerAsReference = 32
        ''' <summary>Default. Comparison includes type of parameter, direction, custpm and optional modifiers and does consider parameters passed by value and by reference to be of different type.</summary>
        Strict = 0
        ''' <summary>This how method signatures are compared according to CLS-rules - direction, modopts, modreqs and retun type are ignored. Note: CLS does not ignore return type for op_Implicit and op_Explicit operator methods (use <see cref="CLS">CLS</see> AND NOT <see cref="IgnoreReturn">IgnoreReturn</see> for them).</summary>
        ''' <seelaso cref="IgnoreDirection"/><seelaso cref="IgnoreModOpt"/><seelaso cref="IgnoreModReq"/> <seelaso cref="IgnoreReturn"/>
        CLS = IgnoreDirection Or IgnoreModOpt Or IgnoreModReq Or IgnoreReturn
        ''' <summary>Ignore both - optional and required modifiers (modopts and modreqs)</summary>
        ''' <seelaso cref="IgnoreModOpt"/><seelaso cref="IgnoreModReq"/>
        IgnoreModifiers = IgnoreModOpt Or IgnoreModReq
    End Enum
End Namespace
#End If