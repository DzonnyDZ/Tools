Imports System.Reflection, Tools.ExtensionsT, System.Linq
'TODO: Implement
#If Config <= Nightly Then 'Stage:Nightly
Namespace ReflectionT
    ''' <summary>Allows to check if varios items of compiled code are CLS compliant</summary>
    Public Class CLSComplianceChecker
        Public Class CLSViolationEventArgs : Inherits EventArgs
            Private ReadOnly _Message$
            Private ReadOnly _Rule As CLSRule
            Private ReadOnly _Member As ICustomAttributeProvider
            Private readonly _Exception As Exception 
            Public ReadOnly Property Message$()
                Get
                    Return _Message
                End Get
            End Property
            Public ReadOnly Property Rule() As CLSRule
                Get
                    Return _Rule
                End Get
            End Property
            Public ReadOnly Property Member() As ICustomAttributeProvider
                Get
                    Return _Member
                End Get
            End Property
            Public ReadOnly Property Exception() As Exception
                Get
                    Return _Exception
                End Get
            End Property
            Public Sub New(ByVal Message$, ByVal Rule As CLSRule, ByVal Member As ICustomAttributeProvider, Optional ByVal Exception As Exception = Nothing)
                If Message Is Nothing Then Throw New ArgumentNullException("Message")
                If Not Rule.IsDefined Then Throw New InvalidEnumArgumentException(Rule, "Rule", Rule.GetType)
                If Member Is Nothing Then Throw New ArgumentNullException("Member")
                Me._Message = Message
                Me._Rule = Rule
                Me._Member = Member
                Me._Exception = Exception
            End Sub
        End Class
        ''' <summary>CLS compatibility rules as defined in ECMA standard of CLS</summary>
        Public Enum CLSRule
            ''' <summary>1: CLS rules apply only to those parts of a type that are accessible or visible outside of the defining assembly.</summary>
            ''' <remarks>Violation of this rule is never reported</remarks>
            OnlyVisible = 1
            ''' <summary>2: Members of non-CLS compliant types shall not be marked CLS-compliant.</summary>
            NoCompliantMembersInIncompliantTypes = 2
            ''' <summary>3: Boxed value types are not CLS-compliant.</summary>
            ''' <remarks>This rule si not checked because there is no wa hot to expose boxed type by name.</remarks>
            NoBoxedValueTypes = 3
            ''' <summary>4: Assemblies shall follow Annex 7 of Technical Report 15 of the Unicode Standard 3.0 governing the set of characters permitted to start and be included in identifiers, available on-line at http://www.unicode.org/unicode/reports/tr15/tr15-18.html. Identifiers shall be in the canonical format defined by Unicode Normalization Form C. For CLS purposes, two identifiers are the same if their lowercase mappings (as specified by the Unicode locale-insensitive, one-to-one lowercase mappings) are the same. That is, for two identifiers to be considered different under the CLS they shall differ in more than simply their case. However, in order to override an inherited definition the CLI requires the precise encoding of the original declaration be used.</summary>
            UnicodeIdentifiers = 4
            ''' <summary>5: All names introduced in a CLS-compliant scope shall be distinct independent of kind, except where the names are identical and resolved via overloading. That is, while the CTS allows a single type to use the same name for a method and a field, the CLS does not.</summary>
            DistincNames = 5
            ''' <summary>6: Fields and nested types shall be distinct by identifier comparison alone, even though the CTS allows distinct signatures to be distinguished. Methods, properties, and events that have the same name (by identifier comparison) shall differ by more than just the return type, except as specified in CLS Rule 39.</summary>
            NoOverloadByReturnType = 6
            ''' <summary>7: The underlying type of an enum shall be a built-in CLS integer type, the name of the field shall be "value__", and that field shall be marked RTSpecialName.</summary>
            EnumSructure = 7
            ''' <summary>8: There are two distinct kinds of enums, indicated by the presence or absence of the <see cref="System.FlagsAttribute"/> (see Partition IV) custom attribute. One represents named integer values; the other represents named bit flags that can be combined to generate an unnamed value. The value of an enum is not limited to the specified values.</summary>
            ''' <remarks>Violation of this rule is never reported</remarks>
            Flags = 8
            ''' <summary>9: Literal static fields (see §8.6.1) of an enum shall have the type of the enum itself.</summary>
            EnumMembers = 9
            ''' <summary>10: Accessibility shall not be changed when overriding inherited methods, except when overriding a method inherited from a different assembly with accessibility family-or-assembly. In this case, the override shall have accessibility family.</summary>
            NoChangeOfAccessWhenOverrideing = 10
            ''' <summary>11: All types appearing in a signature shall be CLS-compliant. All types composing an instantiated generic type shall be CLS-compliant.</summary>
            Signature = 11
            ''' <summary>12: The visibility and accessibility of types and members shall be such that types in the signature of any member shall be visible and accessible whenever the member itself is visible and accessible. For example, a public method that is visible outside its assembly shall not have an argument whose type is visible only within the assembly. The visibility and accessibility of types composing an instantiated generic type used in the signature of any member shall be visible and accessible whenever the member itself is visible and accessible. For example, an instantiated generic type present in the signature of a member that is visible outside its assembly shall not have a generic argument whose type is visible only within the assembly.</summary>
            Visibility = 12
            ''' <summary>13: The value of a literal static is specified through the use of field initialization metadata (see Partition II). A CLS-compliant literal must have a value specified in field initialization metadata that is of exactly the same type as the literal (or of the underlying type, if that literal is an enum).</summary>
            ValueOfLiteral = 13
            ''' <summary>14: Typed references are not CLS-compliant.</summary>
            NoTypedReferences = 14
            ''' <summary>15: The vararg constraint is not part of the CLS, and the only calling convention supported by the CLS is the standard managed calling convention.</summary>
            CallingConvention = 15
            ''' <summary>16: Arrays shall have elements with a CLS-compliant type, and all dimensions of the array shall have lower bounds of zero. Only the fact that an item is an array and the element type of the array shall be required to distinguish between overloads. When overloading is based on two or more array types the element types shall be named types.</summary>
            Arrays = 16
            ''' <summary>17: Unmanaged pointer types are not CLS-compliant.</summary>
            NoUnmanagedPointer = 17
            ''' <summary>18: CLS-compliant interfaces shall not require the definition of non-CLS compliant methods in order to implement them.</summary>
            NoIncompliantMembersInInterfaces = 18
            ''' <summary>19: CLS-compliant interfaces shall not define static methods, nor shall they define fields.</summary>
            NoStaticMembersAndFieldsInInterfaces = 19
            ''' <summary>20: CLS-compliant classes, value types, and interfaces shall not require the implementation of non-CLS-compliant members.</summary>
            NoNeedToImplementIncompliantMember = 20
            ''' <summary>21: An object constructor shall call some class constructor of its base class before any access occurs to inherited instance data. (This does not apply to value types, which need not have constructors.)</summary>
            CallBaseClassCTor = 21
            ''' <summary>22: An object constructor shall not be called except as part of the creation of an object, and an object shall not be initialized twice.</summary>
            NoCallsToCTor = 22
            ''' <summary>23: System.Object is CLS-compliant. Any other CLS-compliant class shall inherit from a CLS-compliant class.</summary>
            NoIncompliantBase = 23
            ''' <summary>24: The methods that implement the getter and setter methods of a property shall be marked SpecialName in the metadata.</summary>
            SpecialNameGetterAndSetter = 24
            ''' <summary>25: No longer used.</summary>
            ''' <remarks>In an earlier version of this standard, this rule stated “The accessibility of a property and of its accessors shall be identical.” The removal of this rule allows, for example, public access to a getter while restricting access to the setter.
            ''' <para>Violation of this rule is never reported</para></remarks>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            GetterAndSetterSameAccess = 25
            ''' <summary>26: A property’s accessors shall all be static, all be virtual, or all be instance.</summary>
            SameKindOfGetterAndSetter = 26
            ''' <summary>27: The type of a property shall be the return type of the getter and the type of the last argument of the setter. The types of the parameters of the property shall be the types of the parameters to the getter and the types of all but the final parameter of the setter. All of these types shall be CLScompliant, and shall not be managed pointers (i.e., shall not be passed by reference).</summary>
            PropertyType = 27
            ''' <summary>28: Properties shall adhere to a specific naming pattern. See §10.4. The SpecialName attribute referred to in CLS rule 24 shall be ignored in appropriate name comparisons and shall adhere to identifier rules. A property shall have a getter method, a setter method, or both.</summary>
            PropertyNameing = 28
            ''' <summary>29: The methods that implement an event shall be marked SpecialName in the metadata.</summary>
            SpecialNameEvent = 29
            ''' <summary>30: The accessibility of an event and of its accessors shall be identical.</summary>
            EventAccessibility = 30
            ''' <summary>31: The add and remove methods for an event shall both either be present or absent.</summary>
            AddAndRemove = 31
            ''' <summary>32: The add and remove methods for an event shall each take one parameter whose type defines the type of the event and that shall be derived from System.Delegate.</summary>
            AddAndRemoveParameters = 32
            ''' <summary>33: Events shall adhere to a specific naming pattern. See §10.4. The SpecialName attribute referred to in CLS rule 29 shall be ignored in appropriate name comparisons and shall adhere to identifier rules.</summary>
            EventNaming = 33
            ''' <summary>34: The CLS only allows a subset of the encodings of custom attributes. The only types that shall appear in these encodings are (see Partition IV): <see cref="System.Type"/>, <see cref="System.String"/>, <see cref="System.Char"/>, <see cref="System.Boolean"/>, <see cref="System.Byte"/>, <see cref="System.Int16"/>, <see cref="System.Int32"/>, <see cref="System.Int64"/>, <see cref="System.Single"/>, <see cref="System.Double"/>, and any enumeration type based on a CLS-compliant base integer type.</summary>
            AttributeValues = 34
            ''' <summary>35: The CLS does not allow publicly visible required modifiers (modreq, see Partition II), but does allow optional modifiers (modopt, see Partition II) it does not understand.</summary>
            NoModReq = 35
            ''' <summary>36: Global static fields and methods are not CLS-compliant.</summary>
            NoGlobalMembers = 36
            ''' <summary>37: Only properties and methods can be overloaded.</summary>
            OverloadOnlyPropertiesAndMethods = 37
            ''' <summary>38: Properties and methods can be overloaded based only on the number and types of their parameters, except the conversion operators named op_Implicit and op_Explicit, which can also be overloaded based on their return type.</summary>
            ''' <remarks>Violation of this rule is reported only for op_Implicit and op_Explicit otheriwise <see cref="DistingNames"/> (5) or <see cref="NoOverloadByReturnType"/> (6) is reported</remarks>
            OverloadingDistinction = 38
            ''' <summary>39: If either op_Implicit or op_Explicit is provided, an alternate means of providing the coercion shall be provided.</summary>
            ''' <remarks>Violation of this rule may be reported, but it does not mean tha the rule is not fullfilled in way chacker is unable to uncover</remarks>
            AlternativeToOpImplicitAndOpExplicit = 39
            ''' <summary>41: Objects that are thrown shall be of type System.Exception or a type inheriting from it. Nonetheless, CLS-compliant methods are not required to block the propagation of other types of exceptions.</summary>
            ThrowOnlyExceptions = 40
            ''' <summary>41: Attributes shall be of type System.Attribute, or a type inheriting from it.</summary>
            AttributeType = 41
            ''' <summary>42: Nested types shall have at least as many generic parameters as the enclosing type. Generic parameters in a nested type correspond by position to the generic parameters in its enclosing type.</summary>
            NestedGenericTypes = 42
            ''' <summary>43: The name of a generic type shall encode the number of type parameters declared on the non-nested type, or newly introduced to the type if nested, according to the rules defined above.</summary>
            GenericTypeName = 43
            ''' <summary>44: A generic type shall redeclare sufficient constraints to guarantee that any constraints on the base type, or interfaces would be satisfied by the generic type constraints.</summary>
            NestedGenricTypeConstraints = 44
            ''' <summary>45: Types used as constraints on generic parameters shall themselves be CLS-compliant.</summary>
            NoClsIncompliantConstraints = 45
            ''' <summary>46: The visibility and accessibility of members (including nested types) in an instantiated generic type shall be considered to be scoped to the specific instantiation rather than the generic type declaration as a whole. Assuming this, the visibility and accessibility rules of CLS rule 12 still apply.</summary>
            ''' <remarks>This rule is not checked</remarks>
            GenericInstanceVisibilityAndAccessibility = 46
            ''' <summary>47: For each abstract or virtual generic method, there shall be a default concrete (nonabstract) implementation.</summary>
            GenericAbstractMethodsHaveDefaultImplementation = 47
            ''' <summary>48: If two or more CLS-compliant methods declared in a type have the same name and, for a specific set of type instantiations, they have the same parameter and return types, then all these methods shall be semantically equivalent at those type instantiations.</summary>
            ''' <remarks>This rule is not checked</remarks>
            GenericMethodsThatBecomeIndistinguishable = 48
            ''' <summary>Attribute usage is violated. This is not CLS rule.</summary>
            AttributeUsageViolation = -1
            ''' <summary>CSL-incompliant attribute is used. This is not violation of any CLS rule.</summary>
            CLSIncompliantAttribute = -2
            ''' <summary>An error is encountered when checking certain item. This is not CLS rule.</summary>
            [Error] = -1000
        End Enum
        '7,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,40,41,42,3,44,45,47
        ''' <summary>Raised when CLS rules is violated or internal check is violated or error in verified piece of code occurs</summary>
        Public Event Violation As EventHandler(Of CLSComplianceChecker, CLSViolationEventArgs)
        ''' <summary>Raises the <see cref="Violation"/> event</summary>
        ''' <param name="Message">Error message describint the problem</param>
        ''' <param name="Rule">Identifies rule being broken or cause of error condition</param>
        ''' <param name="Item">Item which caused the rule to be broken or error condition to be met</param>
        ''' <param name="Exception">In case of error, contains exception that caused the error condition; null otherwise</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Message"/> or <paramref name="Item"/> is null</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Rule"/> is not member of <see cref="CLSRule"/></exception>
        Protected Overridable Sub OnViolation(ByVal Message$, ByVal Rule As CLSRule, ByVal Item As ICustomAttributeProvider, Optional ByVal Exception As Exception = Nothing)
            RaiseEvent Violation(Me, New CLSViolationEventArgs(Message, Rule, Item, Exception))
        End Sub
        ''' <summary>Regulare expersssion to check if identifier name is valied</summary>
        Private Shared ReadOnly IdentifierRegEx As New System.Text.RegularExpressions.Regex("[{Lu}{Ll}{Lt}{Lm}{Lo}{Nl}]([{Lu}{Ll}{Lt}{Lm}{Lo}{Nl}]|[{Mn}{Mc}{Nd}{Pc}{Cf}])*", Text.RegularExpressions.RegexOptions.CultureInvariant Or Text.RegularExpressions.RegexOptions.Compiled)
        ''' <summary>Checks assemby for CLS compliance</summary>
        ''' <param name="Assembly">Assebly to check</param>
        ''' <returns>Ture if assembly meets CLS rules. Returns tur even when assembly is marked as CLS-incompliant, because this does not mean CLS-rules violation.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Assembly"/> is null</exception>
        ''' <remarks>Actual violations of CLS rules (if any) are reported via the <see cref="Violation"/> event.</remarks>
        Public Function Check(ByVal Assembly As Assembly) As Boolean
            If Assembly Is Nothing Then Throw New ArgumentNullException("Assembly")
            Dim Violated As Boolean
            Violated = Not CheckAttributes(Assembly)
            Dim CLS = GetItemClsCompliance(Assembly)
            If CLS Then
                Violated = Violated Or Not CLSAttributeCheck(Assembly)
                'TODO: Do test on assembly
            End If
            'Check uniqueness of type names
            Dim types As IEnumerable(Of Type) = New Type() {}
            Try
                types = From type In Assembly.GetTypes() Where type.IsPublic AndAlso Not type.IsNested
            Catch ex As Exception
                OnViolation("Error while getting types in assembly. Some types weren't loaded.", CLSRule.Error, Assembly, ex) 'Localize: message
            End Try
            CheckTypeNames(types)
            'Check modules
            Dim Modules() As [Module] = {}
            Try
                Modules = Assembly.GetModules
            Catch ex As Exception
                OnViolation("Error while getting modules in assembly", CLSRule.Error, Assembly, ex) 'Localize:Message
            End Try
            For Each [Module] In Modules
                Violated = Violated Or Not CheckInternal([Module])
            Next
            Return Violated
        End Function

        ''' <summary>Test if item is CLS - Compliant</summary>
        ''' <param name="Item">Item to test. Should be <see cref="Assembly"/>, <see cref="[Module]"/> or <see cref="MemberInfo"/></param>
        ''' <returns>True if <paramref name="Item"/> is declared to be CLS-compliant; false if it is not. It has either attached <see cref="CLSCompliantAttribute"/> or information is inherited; false is also returned when information cannot be got due to error while obtaining attributes.</returns>
        ''' <remarks>When more <see cref="CLSCompliantAttribute">CLSCompliantAttributes</see> are attached to single item they are and-ed</remarks>
        Private Function GetItemClsCompliance(ByVal Item As ICustomAttributeProvider) As Boolean
            If Item Is Nothing Then Throw New ArgumentNullException("Item")
            Dim attrs As CLSCompliantAttribute()
            Try
                attrs = Item.GetAttributes(Of CLSCompliantAttribute)(False)
            Catch ex As Exception
                Return False
            End Try
            If attrs.Length = 1 Then
                Return attrs(0).IsCompliant
            ElseIf attrs.Length > 0 Then
                For Each attr In attrs
                    If Not attr.IsCompliant Then Return False
                Next
                Return True
            Else 'attrs.Length=0
                If TypeOf Item Is Assembly Then
                    Return False
                ElseIf TypeOf Item Is [Module] Then
                    Return GetItemClsCompliance(DirectCast(Item, [Module]).Assembly)
                ElseIf TypeOf Item Is MemberInfo Then
                    With DirectCast(Item, MemberInfo)
                        If .DeclaringType Is Nothing Then Return GetItemClsCompliance(.Module) Else Return GetItemClsCompliance(.DeclaringType)
                    End With
                Else
                    Return False
                End If
            End If

        End Function

        ''' <summary>Checks if attributes applied onto given item are valid for such item</summary>
        ''' <param name="Item">Item to check attributes of</param>
        ''' <returns>True if attribute usage was not violated; false if it was. Also returns true when part of chekc was skipped due to error.</returns>
        ''' <remarks>Detailed informations about rule violations and errors are provided via <see cref="Violation"/>. This method does not check any CLS rules.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Item"/> is null</exception>
        Public Function CheckAttributes(ByVal Item As ICustomAttributeProvider) As Boolean
            If Item Is Nothing Then Throw New ArgumentNullException("Item")
            Dim attrs As Attribute()
            Try
                attrs = Item.GetCustomAttributes(False)
            Catch ex As Exception
                OnViolation("Cannot get custom attributes", CLSRule.Error, Item, ex) 'Localize message
                Return False
            End Try
            Dim Types As New List(Of Type)
            Dim AllowMultipleDic As New Dictionary(Of Type, Boolean)
            Dim Check As AttributeTargets = 0
            If TypeOf Item Is Assembly Then : Check = AttributeTargets.Assembly
            ElseIf TypeOf Item Is [Module] Then : Check = AttributeTargets.Module
            ElseIf TypeOf Item Is Type AndAlso DirectCast(Item, Type).IsGenericParameter Then : Check = AttributeTargets.GenericParameter
            ElseIf TypeOf Item Is Type AndAlso GetType([Delegate]).IsAssignableFrom(Item) Then : Check = AttributeTargets.Delegate
            ElseIf TypeOf Item Is Type AndAlso DirectCast(Item, Type).IsClass Then : Check = AttributeTargets.Class
            ElseIf TypeOf Item Is ConstructorInfo Then : Check = AttributeTargets.Constructor
            ElseIf TypeOf Item Is Type AndAlso DirectCast(Item, Type).IsEnum Then : Check = AttributeTargets.Enum
            ElseIf TypeOf Item Is EventInfo Then : Check = AttributeTargets.Event
            ElseIf TypeOf Item Is FieldInfo Then : Check = AttributeTargets.Field
            ElseIf TypeOf Item Is Type AndAlso DirectCast(Item, Type).IsInterface Then : Check = AttributeTargets.Interface
            ElseIf TypeOf Item Is MethodInfo Then : Check = AttributeTargets.Method
            ElseIf TypeOf Item Is ParameterInfo AndAlso DirectCast(Item, ParameterInfo).IsRetval Then : Check = AttributeTargets.ReturnValue
            ElseIf TypeOf Item Is ParameterInfo Then : Check = AttributeTargets.Parameter
            ElseIf TypeOf Item Is PropertyInfo Then : Check = AttributeTargets.Property
            ElseIf TypeOf Item Is Type AndAlso DirectCast(Item, Type).IsValueType Then : Check = AttributeTargets.Struct
            End If
            Dim Violated As Boolean = False
            For Each attr In attrs
                Dim attributeattributes As AttributeUsageAttribute() = Nothing
                Dim attrType As Type = attr.GetType
                Try
                    attributeattributes = attrType.GetAttributes(Of AttributeUsageAttribute)(False)
                Catch ex As Exception
                    OnViolation("Cannot get AttributeUsageAttribute", CLSRule.Error, attrType, ex) 'Localize: Message
                End Try
                Dim AllowMultiple As Boolean = False
                If AllowMultipleDic.ContainsKey(attrType) Then
                    AllowMultiple = AllowMultipleDic(attrType)
                Else
                    If attributeattributes IsNot Nothing AndAlso attributeattributes.Length = 1 Then
                        AllowMultiple = attributeattributes(0).AllowMultiple
                    ElseIf attributeattributes IsNot Nothing AndAlso attributeattributes.Length > 1 Then
                        For Each attributeattr In attributeattributes
                            If attributeattr.AllowMultiple Then AllowMultiple = True : Exit For
                        Next
                    End If
                    AllowMultipleDic.Add(attrType, AllowMultiple)
                End If
                If Not AllowMultiple AndAlso Types.Contains(attrType) Then
                    OnViolation("AttributeUsage.AllowMultiple violated, attribute {0} is used more than once.".f(attrType.FullName), CLSRule.AttributeUsageViolation, Item) 'Localize: Message
                    Violated = True
                End If
                If Not Types.Contains(attrType) Then Types.Add(attrType)
                If Check <> 0 AndAlso attributeattributes IsNot Nothing AndAlso attributeattributes.Length > 0 Then
                    Dim valid As Boolean = False
                    For Each attributeattr In attributeattributes
                        If (attributeattr.ValidOn And Check) = Check Then valid = True : Exit For
                    Next
                    If Not valid Then
                        OnViolation("AttributeUsage.ValidOn violated, attribute {0} cannot be applied to item of type {1}.".f(attrType.FullName, Check), CLSRule.AttributeUsageViolation, Item) 'Localize: Message
                        Violated = True
                    End If
                End If
            Next
            Return Violated
        End Function

        ''' <summary>Checks module for CLS compliance</summary>
        ''' <param name="Module">Assebly to check</param>
        ''' <returns>Ture if module meets CLS rules. Returns tur even when module is marked as CLS-incompliant, because this does not mean CLS-rules violation.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Module"/> is null</exception>
        ''' <remarks>Actual violations of CLS rules (if any) are reported via the <see cref="Violation"/> event.</remarks>
        Public Function Check(ByVal [Module] As [Module]) As Boolean
            If [Module] Is Nothing Then Throw New ArgumentNullException("Module")
            Dim violated As Boolean = False
            Dim types As IEnumerable(Of Type) = New Type() {}
            Try
                types = From type In [Module].GetTypes() Where type.IsPublic AndAlso Not type.IsNested
            Catch ex As Exception
                OnViolation("Error while getting types in module. Some types weren't loaded.", CLSRule.Error, [Module], ex) 'Localize: message
            End Try
            CheckTypeNames(types)
            violated = violated Or Not CheckInternal([Module])
            Return Not violated
        End Function
        ''' <summary>Internally checks module for CLS compliance</summary>
        ''' <param name="Module">Assebly to check</param>
        ''' <returns>Ture if module meets CLS rules. Returns tur even when module is marked as CLS-incompliant, because this does not mean CLS-rules violation.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Module"/> is null</exception>
        ''' <remarks>Dufference from <see cref="Check"/> is that this function does not call <see cref="CheckTypeNames"/>.</remarks>
        Private Function CheckInternal(ByVal [Module] As [Module]) As Boolean
            If [Module] Is Nothing Then Throw New ArgumentNullException("Module")
            Dim Violated As Boolean
            Violated = Not CheckAttributes([Module])
            Dim CLS = GetItemClsCompliance([Module])
            If CLS Then
                Violated = Violated Or Not CLSAttributeCheck([Module])
                'TODO: Do test on module
            End If
            'Types
            Dim Types() As Type = {}
            Try
                Types = [Module].GetTypes()
            Catch ex As ReflectionTypeLoadException
                Types = From type In ex.Types Where type IsNot Nothing
                OnViolation("Error while getting types in module. Some types weren't loaded.", CLSRule.Error, [Module], ex) 'Localize: message
            Catch ex As Exception
                OnViolation("Error while getting types in module.", CLSRule.Error, [Module], ex) 'Localize: Message
            End Try
            For Each Type In Types
                If Type.IsPublic AndAlso Not Type.IsNested Then Violated = Violated Or Not Check(Type)
            Next
            'Global methods
            Dim Methods() As MethodInfo = {}
            Try
                Methods = [Module].GetMethods
            Catch ex As Exception
                OnViolation("Error while getting methods in module.", CLSRule.Error, [Module], ex) 'Localize: Message
            End Try
            For Each Method In Methods
                If Method.IsPublic Then
                    Dim MethodCLSCompliant = GetItemClsCompliance(Method)
                    If MethodCLSCompliant Then
                        Violated = True
                        OnViolation("Global methods are not CLS-compliant", CLSRule.NoGlobalMembers, Method) 'Localize:Message
                    End If
                End If
            Next
            'Fields
            Dim Fields() As FieldInfo = {}
            Try
                Fields = [Module].GetFields
            Catch ex As Exception
                OnViolation("Error while getting fields in module.", CLSRule.Error, [Module], ex) 'Localize: Message
            End Try
            For Each Field In Fields
                If Field.IsPublic Then
                    Dim MethodCLSCompliant = GetItemClsCompliance(Field)
                    If MethodCLSCompliant Then
                        Violated = True
                        OnViolation("Global fields are not CLS-compliant", CLSRule.NoGlobalMembers, Field) 'Localize:Message
                    End If
                End If
            Next
            Return Not Violated
        End Function

        ''' <summary>Checks type for CLS compliance</summary>
        ''' <param name="Type">TYpe to check</param>
        ''' <returns>Ture if type meets CLS rules. Returns tur even when type is marked as CLS-incompliant, because this does not mean CLS-rules violation.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        ''' <remarks>Actual violations of CLS rules (if any) are reported via the <see cref="Violation"/> event.</remarks>
        Public Function Check(ByVal Type As Type) As Boolean
            If Type Is Nothing Then Throw New ArgumentNullException("Type")
            Dim Violated As Boolean
            Violated = Not CheckAttributes(Type)
            Dim CLS = GetItemClsCompliance(Type)
            If CLS Then
                Violated = Violated Or Not DoCommonTest(Type)
                'TODO: Do tests on type
                Dim Members() As MemberInfo = {}
                Try
                    Members = Type.GetMembers(BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.DeclaredOnly)
                    'TODO: Does it reurn Getters/Setters/Adders/...???
                Catch ex As Exception
                    OnViolation("Error while geting members of type.", CLSRule.Error, Type, ex) 'Localize: Message
                    Return False
                End Try
                Dim MemberSignatures As New List(Of MemberCLSSignature)
                For Each Member In Members
                    If Member.IsPublic OrElse Member.IsFamily OrElse Member.IsFamilyOrAssembly Then _
                        Violated = Violated Or Not CheckInternal(Member)
                    If Member.IsPublic OrElse Member.IsFamily OrElse Member.IsFamilyOrAssembly AndAlso GetItemClsCompliance(Member) Then
                        Dim ms As New MemberCLSSignature(Member)
                        If MemberSignatures.Contains(ms) Then
                            Violated = True
                            Dim r = CLSRule.DistincNames
                            If TypeOf Member Is FieldInfo OrElse TypeOf Member Is Type Then r = CLSRule.NoOverloadByReturnType
                            Dim Original As MemberCLSSignature = Nothing
                            For Each item In MemberSignatures
                                If item.Equals(ms) Then Original = ms
                            Next
                            ms = ms.AddReturnType
                            Original = ms.AddReturnType
                            If Not Me.Equals(Original) Then : r = CLSRule.NoOverloadByReturnType
                            ElseIf TypeOf Member Is MethodInfo AndAlso DirectCast(Member, MethodInfo).IsSpecialName AndAlso (Member.Name = "op_Implicit" OrElse Member.Name = "op_Explicit") Then
                                r = CLSRule.OverloadingDistinction
                            End If
                            OnViolation("Member signature is not unique.", r, Member) 'Localize: Message
                        Else
                            MemberSignatures.Add(ms)
                        End If
                    End If
                Next
            Else
                Violated = Not SearchForCompliantMembers(Type)
            End If
            Return Not Violated
        End Function
        ''' <summary>Stores member singature and allows it comparison (via <see cref="MemberCLSSignature.Equals"/>) for CLS-uniqueness purposes</summary>
        Private Class MemberCLSSignature
            ''' <summary>Name of member</summary>
            Private Name As String
            ''' <summary>Types of atributes</summary>
            Private Types As New List(Of Type)
            ''' <summary>Type of return type. Only for instance CTor, op_Implicit and op_Explicit</summary>
            Private ReturnType As Type
            ''' <summary>Original meber</summary>
            Private Original As MemberInfo
            ''' <summary>CTor</summary>
            ''' <param name="Member">Memer to create instance for</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
            Public Sub New(ByVal Member As MemberInfo)
                If Member Is Nothing Then Throw New ArgumentNullException("Member")
                Original = Member
                Name = Member.Name
                If TypeOf Member Is PropertyInfo Then
                    For Each pparam In DirectCast(Member, PropertyInfo).GetIndexParameters
                        Types.Add(pparam.ParameterType)
                    Next
                ElseIf TypeOf Member Is ConstructorInfo Then
                    For Each pparam In DirectCast(Member, ConstructorInfo).GetParameters
                        Types.Add(pparam.ParameterType)
                    Next
                    If Not DirectCast(Member, ConstructorInfo).IsStatic Then ReturnType = Member.DeclaringType
                ElseIf TypeOf Member Is MethodInfo Then
                    With DirectCast(Member, MethodInfo)
                        For Each pparam In .GetParameters
                            Types.Add(pparam.ParameterType)
                        Next
                        If .IsSpecialName AndAlso (.Name = "op_Explicit" OrElse .Name = "op_Implcit") Then ReturnType = .ReturnType
                    End With
                End If
            End Sub
            ''' <summary>Returns new instance created by adding return type to current instace (if applicable)</summary>
            ''' <returns>Return-type-aware instance</returns>
            Public Function AddReturnType() As MemberCLSSignature
                Dim ret As New MemberCLSSignature(Original)
                If TypeOf Original Is MethodInfo Then : ret.ReturnType = DirectCast(Original, MethodInfo).ReturnType
                ElseIf TypeOf Original Is PropertyInfo Then : ret.ReturnType = DirectCast(Original, PropertyInfo).PropertyType
                End If
                Return ret
            End Function
            ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
            ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
            ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
            ''' <exception cref="T:System.NullReferenceException">The 
            ''' <paramref name="obj" /> parameter is null.</exception>
            ''' <filterpriority>2</filterpriority>
            Public Overrides Function Equals(ByVal obj As Object) As Boolean
                If TypeOf obj Is MemberCLSSignature Then
                    With DirectCast(obj, MemberCLSSignature)
                        If .Name.ToLowerInvariant <> Me.Name.ToLowerInvariant Then Return False
                        If Me.ReturnType Is Nothing Xor .ReturnType Is Nothing Then Return False
                        If Me.ReturnType IsNot Nothing AndAlso .ReturnType IsNot Nothing AndAlso Not .ReturnType.Equals(Me.ReturnType) Then Return False
                        If .Types.Count <> Me.Types.Count Then Return False
                        For i As Integer = 0 To Me.Types.Count - 1
                            If Not .Types(i).Equals(Me.Types(i)) Then Return False
                        Next
                        Return True
                    End With
                Else
                    Return MyBase.Equals(obj)
                End If
            End Function
            ''' <summary>Serves as a hash function for a particular type.</summary>
            ''' <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
            ''' <filterpriority>2</filterpriority>
            Public Overrides Function GetHashCode() As Integer
                Return Name.GetHashCode Or Types.Count
            End Function
        End Class

        ''' <summary>Searches for CLS-compliant members</summary>
        ''' <param name="Type">Type to search for CLS-compliant members in</param>
        ''' <returns>Ture if any CLS-complaint-marked member is found; false otherwise</returns>
        ''' <remarks>USe to search for CLS-compliant memberis in CLS-incompliant types</remarks>
        Private Function SearchForCompliantMembers(ByVal Type As Type) As Boolean
            Dim Members() As MemberInfo = {}
            Try
                Members = Type.GetMembers(BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.DeclaredOnly)
                'TODO: Does it reurn Getters/Setters/Adders/...
            Catch ex As Exception
                OnViolation("Error while gerring members of type.", CLSRule.Error, Type, ex) 'Localize:Message
                Return False
            End Try
            Dim Violated = False
            For Each Member In Members
                If Member.IsPublic OrElse Member.IsFamily OrElse Member.IsFamilyOrAssembly Then
                    If GetItemClsCompliance(Member) Then
                        Violated = True
                        OnViolation("Member of CLS-incompliant type is marked as CLS-compliant", CLSRule.NoCompliantMembersInIncompliantTypes, Member) 'Localize: Message
                    ElseIf TypeOf Member Is Type Then
                        Violated = Violated Or Not SearchForCompliantMembers(Member)
                    End If
                    Violated = Violated Or Not CheckAttributes(Member)
                End If
            Next
            Return Not Violated
        End Function

        ''' <summary>Checks member for CLS compliance</summary>
        ''' <param name="Member">Member to check</param>
        ''' <returns>Ture if member meets CLS rules. Returns tur even when member is marked as CLS-incompliant, because this does not mean CLS-rules violation.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        ''' <remarks>Actual violations of CLS rules (if any) are reported via the <see cref="Violation"/> event.</remarks>
        Public Function Check(ByVal Member As MemberInfo) As Boolean
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            If TypeOf Member Is Type Then Return Check(DirectCast(Member, Type))
            If GetItemClsCompliance(Member) Then
                Return CheckInternal(Member)
            Else : Return True
            End If
        End Function
        ''' <summary>Interbally checks member for CLS compliance</summary>
        ''' <param name="Member">Member to check</param>
        ''' <returns>Ture if member meets CLS rules. Returns tur even when member is marked as CLS-incompliant, because this does not mean CLS-rules violation.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        Private Function CheckInternal(ByVal Member As MemberInfo) As Boolean
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            If GetItemClsCompliance(Member) Then
                If TypeOf Member Is Type Then : Return Check(DirectCast(Member, Type))
                ElseIf TypeOf Member Is PropertyInfo Then : Return Check(DirectCast(Member, PropertyInfo))
                ElseIf TypeOf Member Is EventInfo Then : Return Check(DirectCast(Member, EventInfo))
                ElseIf TypeOf Member Is MethodInfo Then : Return Check(DirectCast(Member, MethodInfo))
                ElseIf TypeOf Member Is FieldInfo Then : Return Check(DirectCast(Member, FieldInfo))
                End If
            Else
                Return CheckAttributes(Member)
            End If
        End Function
        ''' <summary>Interbally checks property for CLS compliance</summary>
        ''' <param name="Property">Property to check</param>
        ''' <returns>Ture if property meets CLS rules. Returns tur even when property is marked as CLS-incompliant, because this does not mean CLS-rules violation.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Property"/> is null</exception>
        Private Function Check(ByVal [Property] As PropertyInfo) As Boolean
            If [Property] Is Nothing Then Throw New ArgumentNullException("Property")
            Dim Violated As Boolean = CheckAttributes([Property])
            If GetItemClsCompliance([Property]) Then
                Violated = Violated Or Not DoCommonTest([Property])
                'TODO:
            End If
            Return Not Violated
        End Function
        ''' <summary>Interbally checks Event for CLS compliance</summary>
        ''' <param name="Event">Event to check</param>
        ''' <returns>Ture if Event meets CLS rules. Returns tur even when Event is marked as CLS-incompliant, because this does not mean CLS-rules violation.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Event"/> is null</exception>
        Private Function Check(ByVal [Event] As EventInfo) As Boolean
            If [Event] Is Nothing Then Throw New ArgumentNullException("Event")
            Dim Violated As Boolean = CheckAttributes([Event])
            If GetItemClsCompliance([Event]) Then
                Violated = Violated Or Not DoCommonTest([Event])
                'TODO:
            End If
            Return Not Violated
        End Function
        ''' <summary>Interbally checks Method for CLS compliance</summary>
        ''' <param name="Method">Method to check</param>
        ''' <returns>Ture if Method meets CLS rules. Returns tur even when Method is marked as CLS-incompliant, because this does not mean CLS-rules violation.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Method"/> is null</exception>
        Private Function Check(ByVal Method As MethodInfo) As Boolean
            If Method Is Nothing Then Throw New ArgumentNullException("Method")
            Dim Violated As Boolean = CheckAttributes(Method)
            If GetItemClsCompliance(Method) Then
                Violated = Violated Or Not DoCommonTest(Method)
                'TODO:
            End If
            Return Not Violated
        End Function
        ''' <summary>Interbally checks Field for CLS compliance</summary>
        ''' <param name="Field">Field to check</param>
        ''' <returns>Ture if Field meets CLS rules. Returns tur even when Field is marked as CLS-incompliant, because this does not mean CLS-rules violation.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Field"/> is null</exception>
        Private Function Check(ByVal Field As FieldInfo) As Boolean
            If Field Is Nothing Then Throw New ArgumentNullException("Field")
            Dim Violated As Boolean = CheckAttributes(Field)
            If GetItemClsCompliance(Field) Then
                Violated = Violated Or Not DoCommonTest(Field)
                'TODO:
            End If
            Return Not Violated
        End Function
        ''' <summary>Checks if atributes applied on item are all CLS-compliant</summary>
        ''' <param name="Item">Item to cehck attributes of</param>
        ''' <returns>True if all the attributes are CLS-compliant; false otheriwise. Returns tru if there are no attributes.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Item"/> is null</exception>
        Private Function CLSAttributeCheck(ByVal Item As ICustomAttributeProvider) As Boolean
            If Item Is Nothing Then Throw New ArgumentNullException("Item")
            'TODO:
        End Function
        ''' <summary>Peprforms common CLS-compliance test on member</summary>
        ''' <param name="Item">Member to do tests on</param>
        ''' <returns>True if no CLS-violation was detected; false otherwise.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Item"/> is null</exception>
        Private Function DoCommonTest(ByVal Item As MemberInfo) As Boolean
            If Item Is Nothing Then Throw New ArgumentNullException("Item")
            Dim violated = Not CLSAttributeCheck(Item)
            If Not TypeOf Item Is ConstructorInfo AndAlso Not IdentifierRegEx.IsMatch(Item.Name) Then
                violated = True
                OnViolation("Name ""{0}"" is not CLS-compliant.".f(Item.Name), CLSRule.UnicodeIdentifiers, Item) 'Localize: Message
            End If
            Dim Normalized = Item.Name.Normalize(Text.NormalizationForm.FormC)
            If Normalized.Length <> Item.Name.Length Then
                violated = True
                OnViolation("Name ""{0}"" is not stored in Unicode Normalization Form C.", CLSRule.UnicodeIdentifiers, Item) 'Localize: Message
            Else
                For i As Integer = 0 To Normalized.Length - 1
                    If AscW(Normalized(i)) <> AscW(Item.Name(i)) Then
                        violated = True
                        OnViolation("Name ""{0}"" is not stored in Unicode Normalization Form C.", CLSRule.UnicodeIdentifiers, Item) 'Localize: Message
                        Exit For
                    End If
                Next
            End If
            'TODO:
            Return Not violated
        End Function
        ''' <summary>Checks uniqueness of type names amongs given enumerations</summary>
        ''' <param name="Types">Types to verify uniqueness of names of</param>
        ''' <returns>True if names of all types are unique (in spicte of CLS)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Types"/> is null</exception>
        Private Function CheckTypeNames(ByVal Types As IEnumerable(Of Type)) As Boolean
            If Types Is Nothing Then Throw New ArgumentNullException("Types")
            Dim list As New List(Of String)
            Dim Violated As Boolean
            For Each Type In Types
                If Not GetItemClsCompliance(Type) Then Continue For
                If list.Contains(Type.Name.ToLowerInvariant, StringComparer.InvariantCultureIgnoreCase) Then
                    Violated = True
                    OnViolation("Culture-invariant lowercase representation of type name ""{0}"" is not unique.".f(Type.Name), CLSRule.UnicodeIdentifiers, Type) 'Localize:Message
                Else
                    list.Add(Type.Name.ToLowerInvariant)
                End If
            Next
            Return Violated
        End Function
    End Class
End Namespace
#End If