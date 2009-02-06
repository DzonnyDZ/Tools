Imports System.Reflection, Tools.ExtensionsT
'TODO: Implement
'#If Config <= Nightly Then 'Stage:Nightly
'Namespace ReflectionT
'    ''' <summary>Allows to check if varios items of compiled code are CLS compliant</summary>
'    Public Class CLSComplianceChecker
'        Public Class CLSViolationEventArgs : Inherits EventArgs
'            Private ReadOnly _Message$
'            Private ReadOnly _Rule As CLSRule
'            Private ReadOnly _Member As ICustomAttributeProvider
'            Public ReadOnly Property Message$()
'                Get
'                    Return _Message
'                End Get
'            End Property
'            Public ReadOnly Property Rule() As CLSRule
'                Get
'                    Return _Rule
'                End Get
'            End Property
'            Public ReadOnly Property Member() As ICustomAttributeProvider
'                Get
'                    Return _Member
'                End Get
'            End Property
'            Public Sub New(ByVal Message$, ByVal Rule As CLSRule, ByVal Member As ICustomAttributeProvider)
'                If Message Is Nothing Then Throw New ArgumentNullException("Message")
'                If not Rule.IsDefined  then Throw New InvalidEnumArgumentException(Rule,"Rule",Rule.GetType GetType)
'                If Member Is Nothing Then Throw New ArgumentNullException("Member")
'                Me._Message = Message
'                Me._Rule = Rule
'                Me._Member = Member
'            End Sub
'        End Class
'        ''' <summary>CLS compatibility rules as defined in ECMA standard of CLS</summary>
'        Public Enum CLSRule
'            ''' <summary>1: CLS rules apply only to those parts of a type that are accessible or visible outside of the defining assembly.</summary>
'            ''' <remarks>Violation of this rule is never reported</remarks>
'            OnlyVisible = 1
'            ''' <summary>2: Members of non-CLS compliant types shall not be marked CLS-compliant.</summary>
'            NoCompliantMembersInIncompliantTypes = 2
'            ''' <summary>3: Boxed value types are not CLS-compliant.</summary>
'            NoBoxedValueTypes = 3
'            ''' <summary>4: Assemblies shall follow Annex 7 of Technical Report 15 of the Unicode Standard 3.0 governing the set of characters permitted to start and be included in identifiers, available on-line at http://www.unicode.org/unicode/reports/tr15/tr15-18.html. Identifiers shall be in the canonical format defined by Unicode Normalization Form C. For CLS purposes, two identifiers are the same if their lowercase mappings (as specified by the Unicode locale-insensitive, one-to-one lowercase mappings) are the same. That is, for two identifiers to be considered different under the CLS they shall differ in more than simply their case. However, in order to override an inherited definition the CLI requires the precise encoding of the original declaration be used.</summary>
'            UnicodeIdentifiers = 4
'            ''' <summary>5: All names introduced in a CLS-compliant scope shall be distinct independent of kind, except where the names are identical and resolved via overloading. That is, while the CTS allows a single type to use the same name for a method and a field, the CLS does not.</summary>
'            DistincNames = 5
'            ''' <summary>6: Fields and nested types shall be distinct by identifier comparison alone, even though the CTS allows distinct signatures to be distinguished. Methods, properties, and events that have the same name (by identifier comparison) shall differ by more than just the return type, except as specified in CLS Rule 39.</summary>
'            NoOverloadByReturnType = 6
'            ''' <summary>7: The underlying type of an enum shall be a built-in CLS integer type, the name of the field shall be "value__", and that field shall be marked RTSpecialName.</summary>
'            EnumSructure = 7
'            ''' <summary>8: There are two distinct kinds of enums, indicated by the presence or absence of the <see cref="System.FlagsAttribute"/> (see Partition IV) custom attribute. One represents named integer values; the other represents named bit flags that can be combined to generate an unnamed value. The value of an enum is not limited to the specified values.</summary>
'            ''' <remarks>Violation of this rule is never reported</remarks>
'            Flags = 8
'            ''' <summary>9: Literal static fields (see §8.6.1) of an enum shall have the type of the enum itself.</summary>
'            EnumMembers = 9
'            ''' <summary>10: Accessibility shall not be changed when overriding inherited methods, except when overriding a method inherited from a different assembly with accessibility family-or-assembly. In this case, the override shall have accessibility family.</summary>
'            NoChangeOfAccessWhenOverrideing = 10
'            ''' <summary>11: All types appearing in a signature shall be CLS-compliant. All types composing an instantiated generic type shall be CLS-compliant.</summary>
'            Signature = 11
'            ''' <summary>12: The visibility and accessibility of types and members shall be such that types in the signature of any member shall be visible and accessible whenever the member itself is visible and accessible. For example, a public method that is visible outside its assembly shall not have an argument whose type is visible only within the assembly. The visibility and accessibility of types composing an instantiated generic type used in the signature of any member shall be visible and accessible whenever the member itself is visible and accessible. For example, an instantiated generic type present in the signature of a member that is visible outside its assembly shall not have a generic argument whose type is visible only within the assembly.</summary>
'            Visibility = 12
'            ''' <summary>13: The value of a literal static is specified through the use of field initialization metadata (see Partition II). A CLS-compliant literal must have a value specified in field initialization metadata that is of exactly the same type as the literal (or of the underlying type, if that literal is an enum).</summary>
'            ValueOfLiteral = 13
'            ''' <summary>14: Typed references are not CLS-compliant.</summary>
'            NoTypedReferences = 14
'            ''' <summary>15: The vararg constraint is not part of the CLS, and the only calling convention supported by the CLS is the standard managed calling convention.</summary>
'            CallingConvention = 15
'            ''' <summary>16: Arrays shall have elements with a CLS-compliant type, and all dimensions of the array shall have lower bounds of zero. Only the fact that an item is an array and the element type of the array shall be required to distinguish between overloads. When overloading is based on two or more array types the element types shall be named types.</summary>
'            Arrays = 16
'            ''' <summary>17: Unmanaged pointer types are not CLS-compliant.</summary>
'            NoUnmanagedPointer = 17
'            ''' <summary>18: CLS-compliant interfaces shall not require the definition of non-CLS compliant methods in order to implement them.</summary>
'            NoIncompliantMembersInInterfaces = 18
'            ''' <summary>19: CLS-compliant interfaces shall not define static methods, nor shall they define fields.</summary>
'            NoStaticMembersAndFieldsInInterfaces = 19
'            ''' <summary>20: CLS-compliant classes, value types, and interfaces shall not require the implementation of non-CLS-compliant members.</summary>
'            NoNeedToImplementIncompliantMember = 20
'            ''' <summary>21: An object constructor shall call some class constructor of its base class before any access occurs to inherited instance data. (This does not apply to value types, which need not have constructors.)</summary>
'            CallBaseClassCTor = 21
'            ''' <summary>22: An object constructor shall not be called except as part of the creation of an object, and an object shall not be initialized twice.</summary>
'            NoCallsToCTor = 22
'            ''' <summary>23: System.Object is CLS-compliant. Any other CLS-compliant class shall inherit from a CLS-compliant class.</summary>
'            NoIncompliantBase = 23
'            ''' <summary>24: The methods that implement the getter and setter methods of a property shall be marked SpecialName in the metadata.</summary>
'            SpecialNameGetterAndSetter = 24
'            ''' <summary>25: No longer used.</summary>
'            ''' <remarks>In an earlier version of this standard, this rule stated “The accessibility of a property and of its accessors shall be identical.” The removal of this rule allows, for example, public access to a getter while restricting access to the setter.
'            ''' <para>Violation of this rule is never reported</para></remarks>
'            <EditorBrowsable(EditorBrowsableState.Never)> _
'            GetterAndSetterSameAccess = 25
'            ''' <summary>26: A property’s accessors shall all be static, all be virtual, or all be instance.</summary>
'            SameKindOfGetterAndSetter = 26
'            ''' <summary>27: The type of a property shall be the return type of the getter and the type of the last argument of the setter. The types of the parameters of the property shall be the types of the parameters to the getter and the types of all but the final parameter of the setter. All of these types shall be CLScompliant, and shall not be managed pointers (i.e., shall not be passed by reference).</summary>
'            PropertyType = 27
'            ''' <summary>28: Properties shall adhere to a specific naming pattern. See §10.4. The SpecialName attribute referred to in CLS rule 24 shall be ignored in appropriate name comparisons and shall adhere to identifier rules. A property shall have a getter method, a setter method, or both.</summary>
'            PropertyNameing = 28
'            ''' <summary>29: The methods that implement an event shall be marked SpecialName in the metadata.</summary>
'            SpecialNameEvent = 29
'            ''' <summary>30: The accessibility of an event and of its accessors shall be identical.</summary>
'            EventAccessibility = 30
'            ''' <summary>31: The add and remove methods for an event shall both either be present or absent.</summary>
'            AddAndRemove = 31
'            ''' <summary>32: The add and remove methods for an event shall each take one parameter whose type defines the type of the event and that shall be derived from System.Delegate.</summary>
'            AddAndRemoveParameters = 32
'            ''' <summary>33: Events shall adhere to a specific naming pattern. See §10.4. The SpecialName attribute referred to in CLS rule 29 shall be ignored in appropriate name comparisons and shall adhere to identifier rules.</summary>
'            EventNaming = 33
'            ''' <summary>34: The CLS only allows a subset of the encodings of custom attributes. The only types that shall appear in these encodings are (see Partition IV): <see cref="System.Type"/>, <see cref="System.String"/>, <see cref="System.Char"/>, <see cref="System.Boolean"/>, <see cref="System.Byte"/>, <see cref="System.Int16"/>, <see cref="System.Int32"/>, <see cref="System.Int64"/>, <see cref="System.Single"/>, <see cref="System.Double"/>, and any enumeration type based on a CLS-compliant base integer type.</summary>
'            AttributeValues = 34
'            ''' <summary>35: The CLS does not allow publicly visible required modifiers (modreq, see Partition II), but does allow optional modifiers (modopt, see Partition II) it does not understand.</summary>
'            NoModReq = 35
'            ''' <summary>36: Global static fields and methods are not CLS-compliant.</summary>
'            NoGlobalMembers = 36
'            ''' <summary>37: Only properties and methods can be overloaded.</summary>
'            OverloadOnlyPropertiesAndMethods = 37
'            ''' <summary>38: Properties and methods can be overloaded based only on the number and types of their parameters, except the conversion operators named op_Implicit and op_Explicit, which can also be overloaded based on their return type.</summary>
'            OverloadingDistinction = 38
'            ''' <summary>39: If either op_Implicit or op_Explicit is provided, an alternate means of providing the coercion shall be provided.</summary>
'            ''' <remarks>Violation of this rule may be reported, but it does not mean tha the rule is not fullfilled in way chacker is unable to uncover</remarks>
'            AlternativeToOpImplicitAndOpExplicit = 39
'            ''' <summary>41: Objects that are thrown shall be of type System.Exception or a type inheriting from it. Nonetheless, CLS-compliant methods are not required to block the propagation of other types of exceptions.</summary>
'            ThrowOnlyExceptions = 40
'            ''' <summary>41: Attributes shall be of type System.Attribute, or a type inheriting from it.</summary>
'            AttributeType = 41
'            ''' <summary>42: Nested types shall have at least as many generic parameters as the enclosing type. Generic parameters in a nested type correspond by position to the generic parameters in its enclosing type.</summary>
'            NestedGenericTypes = 42
'            ''' <summary>43: The name of a generic type shall encode the number of type parameters declared on the non-nested type, or newly introduced to the type if nested, according to the rules defined above.</summary>
'            GenericTypeName = 43
'            ''' <summary>44: A generic type shall redeclare sufficient constraints to guarantee that any constraints on the base type, or interfaces would be satisfied by the generic type constraints.</summary>
'            NestedGenricTypeConstraints = 44
'            ''' <summary>45: Types used as constraints on generic parameters shall themselves be CLS-compliant.</summary>
'            NoClsIncompliantConstraints = 45
'            ''' <summary>46: The visibility and accessibility of members (including nested types) in an instantiated generic type shall be considered to be scoped to the specific instantiation rather than the generic type declaration as a whole. Assuming this, the visibility and accessibility rules of CLS rule 12 still apply.</summary>
'            ''' <remarks>This rule is not checked</remarks>
'            GenericInstanceVisibilityAndAccessibility = 46
'            ''' <summary>47: For each abstract or virtual generic method, there shall be a default concrete (nonabstract) implementation.</summary>
'            GenericAbstractMethodsHaveDefaultImplementation = 47
'            ''' <summary>48: If two or more CLS-compliant methods declared in a type have the same name and, for a specific set of type instantiations, they have the same parameter and return types, then all these methods shall be semantically equivalent at those type instantiations.</summary>
'            ''' <remarks>This rule is not checked</remarks>
'            GenericMethodsThatBecomeIndistinguishable = 48
'        End Enum
'        Public Event Violation As EventHandler(Of CLSComplianceChecker, CLSViolationEventArgs)
'#Region "Public check"
'        Public Function Check(ByVal Asseembly As Assembly) As Boolean

'        End Function

'        Public Function Check(ByVal [Module] As [Module]) As Boolean

'        End Function
'        Public Function Check(ByVal Type As Type) As Boolean

'        End Function
'        Public Function Check(ByVal Member As MemberInfo) As Boolean

'        End Function
'        Public Function Check(ByVal [Property] As PropertyInfo) As Boolean

'        End Function
'        Public Function Check(ByVal [Event] As EventInfo) As Boolean

'        End Function
'        Public Function Check(ByVal Method As MethodInfo) As Boolean

'        End Function
'        Public Function Check(ByVal Field As FieldInfo) As Boolean

'        End Function
'        Public Function Check(ByVal Parameter As ParameterInfo) As Boolean

'        End Function
'#End Region
'#Region "Internal check"
'        Private Function CheckInternal(ByVal [Module] As [Module], ByVal [Default] As Boolean) As Boolean

'            For Each t In [Module].GetTypes

'            Next
'        End Function
'        Private Function CheckInternal(ByVal Type As Type, ByVal [Default] As Boolean) As Boolean

'        End Function
'        Private Function CheckInternal(ByVal Member As MemberInfo) As Boolean

'        End Function
'        Private Function CheckInternal(ByVal [Property] As PropertyInfo) As Boolean

'        End Function
'        Private Function CheckInternal(ByVal [Event] As EventInfo) As Boolean

'        End Function
'        Private Function CheckInternal(ByVal Method As MethodInfo) As Boolean

'        End Function
'        Private Function CheckInternal(ByVal Field As FieldInfo) As Boolean

'        End Function
'        Private Function CheckInternal(ByVal Parameter As ParameterInfo) As Boolean

'        End Function
'#End Region


'    End Class
'End Namespace
'#End If