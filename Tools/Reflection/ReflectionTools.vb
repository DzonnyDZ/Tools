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
        ''' <exception cref="System.Reflection.ReflectionTypeLoadException">One or more classes in a module could not be loaded.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
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
        ''' <summary>Gets namespaces in given module</summary>
        ''' <param name="Module">Module to get namespaces in</param>
        ''' <returns>Array of namespaces in <paramref name="Module"/></returns>
        ''' <param name="TypeFilter">Predicate. Onyl those types for which the predicate returns true will be observed for namespaces.</param>
        ''' <param name="IncludeGlobal">True to include global namespace (with empty name)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Module"/> or <paramref name="TypeFilter"/> is null</exception>
        ''' <exception cref="System.Reflection.ReflectionTypeLoadException">One or more classes in a module could not be loaded.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        <Extension()> Public Function GetNamespaces(ByVal [Module] As Reflection.Module, ByVal TypeFilter As Predicate(Of Type), Optional ByVal IncludeGlobal As Boolean = False) As [NamespaceInfo]()
            If [Module] Is Nothing Then Throw New ArgumentNullException("Module")
            If TypeFilter Is Nothing Then Throw New ArgumentNullException("TypeFilter")
            Dim NamespaceNames As New List(Of String)
            Dim Namespaces As New List(Of NamespaceInfo)
            For Each Type In [Module].GetTypes
                If ((Type.Namespace <> "" OrElse IncludeGlobal) AndAlso Not NamespaceNames.Contains(Type.Namespace)) AndAlso TypeFilter(Type) Then
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
        ''' <exception cref="ArgumentNullException"><paramref name="Module"/> is null</exception>
        ''' <exception cref="System.Reflection.ReflectionTypeLoadException">One or more classes in a module could not be loaded.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        <Extension()> Public Function GetTypes(ByVal [Module] As [Module], ByVal FromNamespaces As Boolean) As Type()
            If [Module] Is Nothing Then Throw New ArgumentNullException("Module")
            If FromNamespaces Then Return [Module].GetTypes
            Return (From Type In [Module].GetTypes Where Type.Namespace = "" Select Type).ToArray
        End Function
#Region "Is..."
        ''' <summary>For <see cref="Type"/>, <see cref="MethodBase"/> (<see cref="MethodInfo"/> or <see cref="ConstructorInfo"/>), <see cref="PropertyInfo"/>, <see cref="EventInfo"/> or <see cref="FieldInfo"/> indicates its accessibility</summary>
        ''' <param name="Member">Member to indicate accesibility of</param>
        ''' <returns>True if accessibility of member is public</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="Member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        <Extension()> Public Function IsPublic(ByVal Member As MemberInfo) As Boolean
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Select Case Member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(Member, Type)
                        Return (Not .IsNested AndAlso .IsPublic) OrElse (.IsNested AndAlso .IsNestedPublic)
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(Member, MethodBase).IsPublic
                Case MemberTypes.Event
                    Return DirectCast(Member, EventInfo).GetAccessibility = MethodAttributes.Public
                Case MemberTypes.Field : Return DirectCast(Member, FieldInfo).IsPublic
                Case MemberTypes.Property
                    Return DirectCast(Member, PropertyInfo).GetAccessibility = MethodAttributes.Public
                Case Else : Return False
            End Select
        End Function
        ''' <summary>For <see cref="Type"/>, <see cref="MethodBase"/> (<see cref="MethodInfo"/> or <see cref="ConstructorInfo"/>), <see cref="PropertyInfo"/>, <see cref="EventInfo"/> or <see cref="FieldInfo"/> indicates its accessibility</summary>
        ''' <param name="Member">Member to indicate accesibility of</param>
        ''' <returns>True if accessibility of member is private</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="Member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        <Extension()> Public Function IsPrivate(ByVal Member As MemberInfo) As Boolean
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Select Case Member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(Member, Type)
                        Return .IsNested AndAlso .IsNestedPrivate
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(Member, MethodBase).IsPrivate
                Case MemberTypes.Event
                    Return DirectCast(Member, EventInfo).GetAccessibility = MethodAttributes.Private
                Case MemberTypes.Field : Return DirectCast(Member, FieldInfo).IsPrivate
                Case MemberTypes.Property
                    Return DirectCast(Member, PropertyInfo).GetAccessibility = MethodAttributes.Private
                Case Else : Return False
            End Select
        End Function
        ''' <summary>For <see cref="Type"/>, <see cref="MethodBase"/> (<see cref="MethodInfo"/> or <see cref="ConstructorInfo"/>), <see cref="PropertyInfo"/>, <see cref="EventInfo"/> or <see cref="FieldInfo"/> indicates its accessibility</summary>
        ''' <param name="Member">Member to indicate accesibility of</param>
        ''' <returns>True if accessibility of member is assembly (friend, internal)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="Member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        <Extension()> Public Function IsAssembly(ByVal Member As MemberInfo) As Boolean
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Select Case Member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(Member, Type)
                        Return (.IsNested AndAlso .IsNestedAssembly) OrElse (Not .IsNested AndAlso .IsNotPublic)
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(Member, MethodBase).IsAssembly
                Case MemberTypes.Event
                    Return DirectCast(Member, EventInfo).GetAccessibility = MethodAttributes.Assembly
                Case MemberTypes.Field : Return DirectCast(Member, FieldInfo).IsAssembly
                Case MemberTypes.Property
                    Return DirectCast(Member, PropertyInfo).GetAccessibility = MethodAttributes.Assembly
                Case Else : Return False
            End Select
        End Function
        ''' <summary>For <see cref="Type"/>, <see cref="MethodBase"/> (<see cref="MethodInfo"/> or <see cref="ConstructorInfo"/>), <see cref="PropertyInfo"/>, <see cref="EventInfo"/> or <see cref="FieldInfo"/> indicates its accessibility</summary>
        ''' <param name="Member">Member to indicate accesibility of</param>
        ''' <returns>True if accessibility of member is family-and-assembly</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="Member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        <Extension()> Public Function IsFamilyAndAssembly(ByVal Member As MemberInfo) As Boolean
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Select Case Member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(Member, Type)
                        Return (.IsNested AndAlso .IsNestedFamANDAssem)
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(Member, MethodBase).IsFamilyAndAssembly
                Case MemberTypes.Event
                    Return DirectCast(Member, EventInfo).GetAccessibility = MethodAttributes.FamANDAssem
                Case MemberTypes.Field : Return DirectCast(Member, FieldInfo).IsFamilyAndAssembly
                Case MemberTypes.Property
                    Return DirectCast(Member, PropertyInfo).GetAccessibility = MethodAttributes.FamANDAssem
                Case Else : Return False
            End Select
        End Function
        ''' <summary>For <see cref="Type"/>, <see cref="MethodBase"/> (<see cref="MethodInfo"/> or <see cref="ConstructorInfo"/>), <see cref="PropertyInfo"/>, <see cref="EventInfo"/> or <see cref="FieldInfo"/> indicates its accessibility</summary>
        ''' <param name="Member">Member to indicate accesibility of</param>
        ''' <returns>True if accessibility of member is family-or-assembly (protected friend)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="Member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        <Extension()> Public Function IsFamilyOrAssembly(ByVal Member As MemberInfo) As Boolean
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Select Case Member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(Member, Type)
                        Return (.IsNested AndAlso .IsNestedFamORAssem)
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(Member, MethodBase).IsFamilyAndAssembly
                Case MemberTypes.Event
                    Return DirectCast(Member, EventInfo).GetAccessibility = MethodAttributes.FamORAssem
                Case MemberTypes.Field : Return DirectCast(Member, FieldInfo).IsFamilyOrAssembly
                Case MemberTypes.Property
                    Return DirectCast(Member, PropertyInfo).GetAccessibility = MethodAttributes.FamORAssem
                Case Else : Return False
            End Select
        End Function
        ''' <summary>For <see cref="Type"/>, <see cref="MethodBase"/> (<see cref="MethodInfo"/> or <see cref="ConstructorInfo"/>), <see cref="PropertyInfo"/>, <see cref="EventInfo"/> or <see cref="FieldInfo"/> indicates its accessibility</summary>
        ''' <param name="Member">Member to indicate accesibility of</param>
        ''' <returns>True if accessibility of member is family (protected)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="Member"/> is either <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        <Extension()> Public Function IsFamily(ByVal Member As MemberInfo) As Boolean
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Select Case Member.MemberType
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    With DirectCast(Member, Type)
                        Return (.IsNested AndAlso .IsNestedFamily)
                    End With
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(Member, MethodBase).IsFamily
                Case MemberTypes.Event
                    Return DirectCast(Member, EventInfo).GetAccessibility = MethodAttributes.Family
                Case MemberTypes.Field : Return DirectCast(Member, FieldInfo).IsFamily
                Case MemberTypes.Property
                    Return DirectCast(Member, PropertyInfo).GetAccessibility = MethodAttributes.Family
                Case Else : Return False
            End Select
        End Function
        ''' <summary>Gets maximum visibility of getter and setter of property</summary>
        ''' <param name="prp">Property to check accessibility of</param>
        ''' <returns>Accessibility that is union of accessibilities of getter and setter</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="prp"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods.</exception>
        <Extension()> Public Function GetAccessibility(ByVal prp As PropertyInfo) As MethodAttributes
            If prp Is Nothing Then Throw New ArgumentNullException("prp")
            Return MaxVisibility(If(prp.GetGetMethod(True) Is Nothing, 0, prp.GetGetMethod(True).Attributes), If(prp.GetSetMethod(True) Is Nothing, 0, prp.GetSetMethod(True).Attributes))
        End Function
        ''' <summary>Gets maximum visibility of adder, remover and raiser of event</summary>
        ''' <param name="ev">Event to check accessibility of</param>
        ''' <returns>Accessibility that is union of accessibilities of adder, remover and raiser</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="ev"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods.</exception>
        <Extension()> Public Function GetAccessibility(ByVal ev As EventInfo) As MethodAttributes
            If ev Is Nothing Then Throw New ArgumentNullException("ev")
            Return MaxVisibility(If(ev.GetAddMethod(True) Is Nothing, 0, ev.GetAddMethod(True).Attributes), If(ev.GetRemoveMethod(True) Is Nothing, 0, ev.GetRemoveMethod(True).Attributes), If(ev.GetRaiseMethod(True) Is Nothing, 0, ev.GetRaiseMethod(True).Attributes))
        End Function

        ''' <summary>Gets maximum visibility from visibilities of methods</summary>
        ''' <param name="Visibility">Array of visibilities to test (it can contain any valid value of <see cref="MethodAttributes"/>, non-visibity part will be ignored)</param>
        ''' <returns>Maximum visibility as union of all visibilities in <paramref name="Visibility"/></returns>
        Private Function MaxVisibility(ByVal ParamArray Visibility As MethodAttributes()) As MethodAttributes
            Dim ret As MethodAttributes = 0
            For Each vis In Visibility
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
                            Case MethodAttributes.FamORAssem, MethodAttributes.Assembly
                                ret = MethodAttributes.FamORAssem
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
        ''' <param name="Member">Member to check</param>
        ''' <returns>True if member should or can be considered static</returns>
        ''' <remarks>For <see cref="Type"/> always returns true. For <see cref="PropertyInfo"/> and <see cref="MethodInfo"/> returns true only if all non-other accessors are static</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="Member"/> is <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        <Extension()> Public Function IsStatic(ByVal Member As MemberInfo) As Boolean
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Select Case Member.MemberType
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(Member, MethodBase).IsStatic
                Case MemberTypes.Event
                    With DirectCast(Member, EventInfo)
                        Return _
                            ((.GetAddMethod(True) IsNot Nothing AndAlso .GetAddMethod(True).IsStatic) OrElse .GetAddMethod(True) Is Nothing) AndAlso _
                            ((.GetRemoveMethod(True) IsNot Nothing AndAlso .GetRemoveMethod(True).IsStatic) OrElse .GetRemoveMethod(True) Is Nothing) AndAlso _
                            ((.GetRaiseMethod(True) IsNot Nothing AndAlso .GetRaiseMethod(True).IsStatic) OrElse .GetRaiseMethod(True) Is Nothing)
                    End With
                Case MemberTypes.Field : Return DirectCast(Member, FieldInfo).IsStatic
                Case MemberTypes.Property
                    With DirectCast(Member, PropertyInfo)
                        Return _
                            ((.GetGetMethod(True) IsNot Nothing AndAlso .GetGetMethod(True).IsStatic) OrElse .GetGetMethod(True) Is Nothing) AndAlso _
                            ((.GetSetMethod(True) IsNot Nothing AndAlso .GetSetMethod(True).IsStatic) OrElse .GetSetMethod(True) Is Nothing)
                    End With
                Case Else : Return True
            End Select
        End Function
        ''' <summary>Gets value indicating if member should be considered final (it cannot be overriden or inherited)</summary>
        ''' <param name="Member">Member to check</param>
        ''' <returns>True if memberis final</returns>
        ''' <remarks>For <see cref="FieldInfo"/> always returns true. For <see cref="EventInfo"/> and <see cref="PropertyInfo"/> all non-other members must be final to return true.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to reflect on non-public methods and <paramref name="Member"/> is <see cref="EventInfo"/> or <see cref="PropertyInfo"/>.</exception>
        <Extension()> Public Function IsFinal(ByVal Member As MemberInfo) As Boolean
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Select Case Member.MemberType
                Case MemberTypes.Constructor, MemberTypes.Method
                    Return DirectCast(Member, MethodBase).IsFinal
                Case MemberTypes.Event
                    With DirectCast(Member, EventInfo)
                        Return _
                            ((.GetAddMethod(True) IsNot Nothing AndAlso .GetAddMethod(True).IsFinal) OrElse .GetAddMethod(True) Is Nothing) AndAlso _
                            ((.GetRemoveMethod(True) IsNot Nothing AndAlso .GetRemoveMethod(True).IsFinal) OrElse .GetRemoveMethod(True) Is Nothing) AndAlso _
                            ((.GetRaiseMethod(True) IsNot Nothing AndAlso .GetRaiseMethod(True).IsFinal) OrElse .GetRaiseMethod(True) Is Nothing)
                    End With
                Case MemberTypes.Property
                    With DirectCast(Member, PropertyInfo)
                        Return _
                            ((.GetGetMethod(True) IsNot Nothing AndAlso .GetGetMethod(True).IsFinal) OrElse .GetGetMethod(True) Is Nothing) AndAlso _
                            ((.GetSetMethod(True) IsNot Nothing AndAlso .GetSetMethod(True).IsFinal) OrElse .GetSetMethod(True) Is Nothing)
                    End With
                Case MemberTypes.TypeInfo, MemberTypes.NestedType
                    Return DirectCast(Member, Type).IsSealed
                Case Else : Return True
            End Select
        End Function
#End Region
        ''' <summary>Searches for property given method belongs to</summary>
        ''' <param name="Method">Method to search property for</param>
        ''' <param name="GetSetOnly">Search only for getters and setters</param>
        ''' <param name="Inherit">Search within methods of base types</param>
        ''' <returns>First property that has <paramref name="Method"/> as one of its accessors</returns>
        ''' <remarks>Search is done only within type where <paramref name="Method"/> is declared and optionally within it's base types</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Method"/> is null</exception>
        <Extension()> Public Function GetProperty(ByVal Method As MethodInfo, Optional ByVal GetSetOnly As Boolean = False, Optional ByVal Inherit As Boolean = False) As PropertyInfo
            If Method Is Nothing Then Throw New ArgumentNullException("Method")
            If Method.DeclaringType Is Nothing Then Return Nothing
            For Each prp In Method.DeclaringType.GetProperties(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance Or If(Inherit, BindingFlags.Default, BindingFlags.DeclaredOnly))
                If GetSetOnly Then
                    If Method.Equals(prp.GetGetMethod(Not Method.IsPublic)) Then Return prp
                    If Method.Equals(prp.GetSetMethod(Not Method.IsPublic)) Then Return prp
                Else
                    For Each other In prp.GetAccessors(Not Method.IsPublic)
                        If Method.Equals(other) Then Return prp
                    Next other
                End If
            Next prp
            Return Nothing
        End Function
        ''' <summary>Searches for event given method belongs to</summary>
        ''' <param name="Method">Method to search event for</param>
        ''' <param name="StandardOnly">Search only for addres, removers and raisers</param>
        ''' <param name="Inherit">Search within methods of base types</param>
        ''' <returns>First event that has <paramref name="Method"/> as one of its accessors</returns>
        ''' <remarks>Search is done only within type where <paramref name="Method"/> is declared and optionally within it's base types</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Method"/> is null</exception>
        <Extension()> Public Function GetEvent(ByVal Method As MethodInfo, Optional ByVal StandardOnly As Boolean = False, Optional ByVal Inherit As Boolean = False) As EventInfo
            If Method Is Nothing Then Throw New ArgumentNullException("Method")
            If Method.DeclaringType Is Nothing Then Return Nothing
            For Each ev In Method.DeclaringType.GetEvents(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance Or If(Inherit, BindingFlags.Default, BindingFlags.DeclaredOnly))
                If Method.Equals(ev.GetAddMethod(Not Method.IsPublic)) Then Return ev
                If Method.Equals(ev.GetRemoveMethod(Not Method.IsPublic)) Then Return ev
                If Method.Equals(ev.GetRaiseMethod(Not Method.IsPublic)) Then Return ev
                If Not StandardOnly Then
                    For Each other In ev.GetOtherMethods(Not Method.IsPublic)
                        If Method.Equals(other) Then Return ev
                    Next other
                End If
            Next ev
            Return Nothing
        End Function
        ''' <summary>Gets namespace of given <see cref="Type"/></summary>
        ''' <param name="Type"><see cref="Type"/> to get namespace of</param>
        ''' <returns>Namespce of given type. Even if it is global (empty) one</returns>
        <Extension()> Function GetNamespace(ByVal Type As Type) As NamespaceInfo
            Return New NamespaceInfo(Type.Module, Type.Namespace)
        End Function
        ''' <summary>Gets value indicating whether and if which the function is operator</summary>
        ''' <param name="Method">Method to investigate</param>
        ''' <param name="NonStandard">Also include operators that are not part of CLI standard (currently VB \, ^ and &amp; operators are supported)</param>
        ''' <returns>If function is operator returns one of <see cref="Operators"/> constants. If function is not operator (or it seems to be a operator but does not fit to operator it pretends to be) returns <see cref="Operators.no"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Method"/> is null</exception>
        <Extension()> Function IsOperator(ByVal Method As MethodInfo, Optional ByVal NonStandard As Boolean = True) As Operators
            If Method Is Nothing Then Throw New ArgumentNullException("Method")
            If Method.IsSpecialName AndAlso Method.IsStatic AndAlso Method.Name.StartsWith("op_") AndAlso [Enum].GetNames(GetType(Operators)).Contains(Method.Name.Substring(3)) AndAlso Not Method.ReturnType.Equals(GetType(System.Void)) Then
                Dim Op As Operators = [Enum].Parse(GetType(Operators), Method.Name.Substring(3))
                If Op.NumberOfOperands <> Method.GetParameters.Length Then Return Operators.no
                If Not NonStandard AndAlso Not Op.IsStandard Then Return Operators.no
                Return Op
            Else
                Return Operators.no
            End If
        End Function
        ''' <summary>Gets number of operands of given operator</summary>
        ''' <param name="Operator">Operator to get number of operands of</param>
        ''' <returns>And-combination of <paramref name="Operator"/> and <see cref="Operators_masks.NoOfOperands"/></returns>
        <Extension()> Function NumberOfOperands(ByVal [Operator] As Operators) As Byte
            Return [Operator] And Operators_masks.NoOfOperands
        End Function
        ''' <summary>Gets value indicating if given operator is standard CLI operator</summary>
        ''' <param name="Operator">Operator to get information for</param>
        ''' <returns>Negation of and-combination of <paramref name="Operator"/> and <see cref="Operators_masks.NonStandard"/></returns>
        <Extension()> Function IsStandard(ByVal [Operator] As Operators) As Boolean
            Return Not ([Operator] And Operators_masks.NonStandard)
        End Function
        ''' <summary>Gets value indicating if operator is sassignment operator</summary>
        ''' <param name="Operator">Operator to get information for</param>
        ''' <returns>And-combination of <paramref name="Operator"/> and <see cref="Operators_masks.Assignment"/></returns>
        <Extension()> Function IsAssignment(ByVal [Operator] As Operators) As Boolean
            Return [Operator] And Operators_masks.Assignment
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
        ''' <summary>s located within current namespace</summary>
        ''' <returns>Array of types defined in this namespace</returns>
        ''' <exception cref="System.Reflection.ReflectionTypeLoadException">One or more classes in a module could not be loaded.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        Public Function GetTypes(Optional ByVal Nested As Boolean = False) As Type()
            Return (From Type In Me.Module.GetTypes() Where (Nested OrElse Not Type.IsNested) AndAlso Type.Namespace = Me.Name Select Type).ToArray
        End Function
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>True if <paramref name="obj"/> is <see cref="NamespaceInfo"/> and its <see cref="[Module]"/> equals to <see cref="[Module]"/> of current <see cref="NamespaceInfo"/> and also <see cref="Name">Names</see> or current <see cref="NamespaceInfo"/> and <paramref name="obj"/> equals.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        ''' <exception cref="T:System.NullReferenceException">The 
        ''' <paramref name="obj" /> parameter is null.</exception>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
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
    End Class
    ''' <summary>Operators supported by CLI</summary>
    ''' <remarks>High order byte (exluding its MSB) is number that uniquely identifies the operator.
    ''' Low-order half-byte represents number of operands of the operator (1 or 2).
    ''' If MSB of low-order byte is set then operator is non-standard.
    ''' If LSB of high-order half-byle of low-order byte (9th LSB bit in whole number) is set then operator is assignment.
    ''' See <seealso cref="Operators_masks"/>.
    ''' Names of items of the enumeration are names of operator methods without 'op_' prefix.</remarks>
    Public Enum Operators As Short
        ''' <summary>No operator</summary>
        no = False
        ''' <summary>Decrement (unary, like C++/C# --)</summary>
        Decrement = &H101
        ''' <summary>Increment (unary, like C++/C# ++)</summary>
        Increment = &H201
        ''' <summary>Unary negation (unary minus operator like C++/C#/VB -)</summary>
        UnaryNegation = &H301
        ''' <summary>Unary plus (like C++/C#/VB +)</summary>
        UnaryPlus = &H401
        ''' <summary>Logical not (unary, like C++/C# !, VB Not)</summary>
        LogicalNot = &H501
        ''' <summary>True operator - if value should be treated as True (unary, like VB IsTrue)</summary>
        [True] = &H601
        ''' <summary>False operator - if value should be treated as False (unary, like VB IsFalse)</summary>
        [False] = &H701
        ''' <summary>Reference operator (unary, like C++ &amp;)</summary>
        [AddressOf] = &H801
        ''' <summary>Bitwise not operator (unary, like C++/C# ~, VB Not)</summary>
        OnesComplement = &H901
        ''' <summary>Pointer dereference (unary, like C++ *)</summary>
        PointerDereference = &HA01

        ''' <summary>Addition (binary, like C++/C#/VB +)</summary>
        Addition = &HB02
        ''' <summary>Subtraction (binary, like C++/C#/VB -)</summary>
        Subtraction = &HC02
        ''' <summary>Multiplication (binary, like C++/C#/VB *)</summary>
        Multiply = &HD02
        ''' <summary>Division (binary, like C++/C#/VB /)</summary>
        Division = &HE02
        ''' <summary>Modulus (division remainder, binary, like C++/C# %, VB Mod)</summary>
        Modulus = &HF02
        ''' <summary>Bitwise xor (exclusive or, binary, like C++/C# ^, VB Xor)</summary>
        ExclusiveOr = &H1002
        ''' <summary>Bitwise and (binary, like C++/C# &amp;, VB And)</summary>
        BitwiseAnd = &H1102
        ''' <summary>Bitwise or (binary, like C++/C# |, VB Or)</summary>
        BitwiseOr = &H1202
        ''' <summary>Logical and (binary, like C++/C# &amp;&amp;, VB AndAlso)</summary>
        LogicalAnd = &H1302
        ''' <summary>Logical or (binary, like C++/C# ||, VB OrElse)</summary>
        LogicalOr = &H1402
        ''' <summary>Assignment(binary, like C++/C#/VB =)</summary>
        Assign = &H1512
        ''' <summary>Left shift (binary, like C++/C#/VB &lt;&lt;)</summary>
        LeftShift = &H1602
        ''' <summary>Right shift (binary, like C++/C#/VB >>)</summary>
        RightShift = &H1702
        ''' <summary>Signed right shift (binary)</summary>
        SignedRightShif = &H1802
        ''' <summary>Unsigned right shift (binary)</summary>
        UnsignedRightShif = &H1902
        ''' <summary>Equality comparison (binary, like C++/C# ==, VB =)</summary>
        Equality = &H1A02
        ''' <summary>Greater than comparison (binary, like C++/C#/VB >)</summary>
        GreaterThan = &H1B02
        ''' <summary>Less than comparison (binary, like C++/C#/VB &lt;)</summary>
        LessThan = &H1C02
        ''' <summary>Inequality comparison (binary, like C++/C# !=; VB &lt;>)</summary>
        Inequality = &H1E02
        ''' <summary>Greater than or equal comparison (binary, like C++/C#/VB >=)</summary>
        GreaterThanOrEqual = &H1F02
        ''' <summary>Less than or equal comparison (binary, like C++/C#/VB &lt;=)</summary>
        LessThanOrEqual = &H200
        ''' <summary>Self-assignment of unsigned right shift (binary)</summary>
        UnsignedRightShiftAssignmen = &H2012
        ''' <summary>Member selection (binary, like C++ ->)</summary>
        MemberSelection = &H2102
        ''' <summary>Self-assignment of right shift (binary, like C++/C#/VB >>=)</summary>
        RightShifAssignment = &H2212
        ''' <summary>Self-assigment of multiplication (binary, like C++/C#/VB *=)</summary>
        MultiplicationAssignment = &H2312
        ''' <summary>Selection of pointer to member (binary, like C++ ->*)</summary>
        PointerToMemberSelection = &H2402
        ''' <summary>Self-assignment of subtraction (binary, like C++/C#/VB -=)</summary>
        SubtractionAssignment = &H2512
        ''' <summary>Bitwise exclusive or self-assigment (binary, like C++/C# ^=)</summary>
        ExclusiveOrAssignment = &H2612
        ''' <summary>Self-assigment of left shift (binary, like C++/C#/VB &lt;&lt;=)</summary>
        LeftShiftAssignment = &H2712
        ''' <summary>Modulus (division remainder) self-assignment (binary, like C++/C# %=)</summary>
        ModulusAssignment = &H2812
        ''' <summary>Self-assigmment of addition (binary, like C++/C#/VB +=)</summary>
        AditionAssignment = &H2912
        ''' <summary>Self-assignment of witwise and (binary, like C++/C# &amp;=)</summary>
        BitwiseAndAssignment = &H2A12
        ''' <summary>Self-assignment of bitwise or (binary, like C++/C# |=)</summary>
        BitwiseOrAssignment = &H2B12
        ''' <summary>Comma (operation grouping, binary, like C++ ,)</summary>
        Comma = &H2C02
        ''' <summary>Self-assignment of division (binary, like C++/C#/VB /=)</summary>
        DivisionAssignment = &H2D12

        ''' <summary>String contactenation (VB specific, binary, like VB &amp;)</summary>
        Concatenate = &H2E82
        ''' <summary>Exponent (VB specific, binary, like VB ^)</summary>
        Exponent = &H2F82
        ''' <summary>Force-integral division (VB specific, binary, like VB \, C++/C# / on integers)</summary>
        IntegerDivision = &H3082

        ''' <summary>Implicit conversion (unary, like C# implicit, VB Narrowing CType)</summary>
        Implicit = &H3101
        ''' <summary>Explicit conversion (unary, like C# explicit, VB Widening CType)</summary>
        Explicit = &H3201
    End Enum
    ''' <summary>Masks for the <see cref="Operators"/> enumeration</summary>
    <Flags()> _
    Public Enum Operators_masks As Short
        ''' <summary>Masks operator number. This number is unique within <see cref="Operators"/>, but has no relation to anything in CLI.</summary>
        OperatorID = &H7F00
        ''' <summary>Masks number of operands</summary>
        NoOfOperands = &HF
        ''' <summary>Masks if operator is standard (0) or non-standard (1)</summary>
        NonStandard = &H80
        ''' <summary>Masks if operator is assignment (1) or not (0)</summary>
        Assignment = &H10
    End Enum
End Namespace