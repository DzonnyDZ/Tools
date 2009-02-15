Imports System.Reflection, Tools.ExtensionsT, System.Linq, Tools.ReflectionT
#If Config <= Nightly Then 'Stage:Nightly
Namespace TestsT
    ''' <summary>Allows to check if varios items of compiled code are CLS compliant</summary>
    ''' <remarks><see cref="CLSComplianceChecker"/> does not check following CLS rules:
    ''' <list type="table"><listheader><term>Rule</term><description>How it is (not) checked</description></listheader>
    ''' <item><term>1 - <see cref="CLSComplianceChecker.CLSRule.OnlyVisible"/></term><description>Not checked - only informative.</description></item>
    ''' <item><term>3 - <see cref="CLSComplianceChecker.CLSRule.NoBoxedValueTypes"/></term><description>Not checked - there is nor way how to expose boxed type by name.</description></item>
    ''' <item><term>8 - <see cref="CLSComplianceChecker.CLSRule.Flags"/></term><description>Not checked - only informative.</description></item>
    ''' <item><term>16 - <see cref="CLSComplianceChecker.CLSRule.Arrays"/></term><description>Checked only for 1-dimensional arrays because 2+-deimsional arrays are physicaly same type not depending on if they are 0-based or anything-other-based while 1-dimensional array type is different when it is 0-based and anytheing-else-based.</description></item>
    ''' <item><term>21 - <see cref="CLSComplianceChecker.CLSRule.CallBaseClassCTor"/></term><description>Not checked - requires eaxmining of method body</description></item>
    ''' <item><term>22 - <see cref="CLSComplianceChecker.CLSRule.NoCallsToCTor"/></term><description>Not checkd - requires examining of method body</description></item>
    ''' <item><term>23 - <see cref="CLSComplianceChecker.CLSRule.GetterAndSetterSameAccess"/></term><description>Not checked - this rule was removed from actual version of CLS</description></item>
    ''' <item><term>38 - <see cref="CLSComplianceChecker.CLSRule.OverloadingDistinction"/></term><description>Reported only for op_Explicit and op_Implicit. Otherwise covered by other rules.</description></item>
    ''' <item><term>39 - <see cref="CLSComplianceChecker.CLSRule.AlternativeToOpImplicitAndOpExplicit"/></term><description>Not checked - almost impossible to check</description></item>
    ''' <item><term>40 - <see cref="CLSComplianceChecker.CLSRule.ThrowOnlyExceptions"/></term><description>Not checked - requires examining of method body and CLS-compliant code is not strictly prohibited from throwing non-exceptions.</description></item>
    ''' <item><term>46 - <see cref="CLSComplianceChecker.CLSRule.GenericInstanceVisibilityAndAccessibility"/></term><description>Not checked - this is guideline for compiler.</description></item>
    ''' <item><term>47 - <see cref="CLSComplianceChecker.CLSRule.GenericAbstractMethodsHaveDefaultImplementation"/></term><description>Not checked - this is guideline for developpers and its hard to avoid false-positives or detect which implementation is default and which is special.</description></item>
    ''' <item><term>48 - <see cref="CLSComplianceChecker.CLSRule.GenericMethodsThatBecomeIndistinguishable"/></term><description>Not checked - cannot check semantics</description></item>
    ''' </list>
    ''' When any method has no <see cref="CLSCompliantAttribute"/> attached and it belong to event or property in same class, declarative CLS-compliance of the method is taken from the event or the property.
    ''' </remarks>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public Class CLSComplianceChecker
        ''' <summary>Reports CLS rule viaolation</summary>
        Public Class CLSViolationEventArgs : Inherits EventArgs
            ''' <summary>Contains value of the <see cref="Message"/> property</summary>
            Private ReadOnly _Message$
            ''' <summary>Contains value of the <see cref="Rule"/> property</summary>
            Private ReadOnly _Rule As CLSRule
            ''' <summary>Contains value of the <see cref="Member"/> property</summary>
            Private ReadOnly _Member As ICustomAttributeProvider
            ''' <summary>Contains value of the <see cref="Exception"/> property</summary>
            Private ReadOnly _Exception As Exception
            ''' <summary>Message describing the violation</summary>
            Public ReadOnly Property Message$()
                Get
                    Return _Message
                End Get
            End Property
            ''' <summary>Identifies CLS-rule or internal rule that was violated</summary>
            Public ReadOnly Property Rule() As CLSRule
                Get
                    Return _Rule
                End Get
            End Property
            ''' <summary>Gets member that caused the violation or error</summary>
            Public ReadOnly Property Member() As ICustomAttributeProvider
                Get
                    Return _Member
                End Get
            End Property
            ''' <summary>Gets exception that caused event to be raied</summary>
            ''' <returns>Exception that caused event to be raied (only when event reports error); null for violation of CLS-rule</returns>
            Public ReadOnly Property Exception() As Exception
                Get
                    Return _Exception
                End Get
            End Property
            ''' <summary>CTor</summary>
            ''' <param name="Message">Message describing the violation</param>
            ''' <param name="Rule">Identifies rule that was violated</param>
            ''' <param name="Member">Identifies member that caused the violation or error</param>
            ''' <param name="Exception">When event is reaised because of error, exception associated with the error; null otherwise</param>
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
            ''' <remarks>This rule is not checked because there is no way how to expose boxed type by name.</remarks>
            NoBoxedValueTypes = 3
            ''' <summary>4: Assemblies shall follow Annex 7 of Technical Report 15 of the Unicode Standard 3.0 governing the set of characters permitted to start and be included in identifiers, available on-line at http://www.unicode.org/unicode/reports/tr15/tr15-18.html. Identifiers shall be in the canonical format defined by Unicode Normalization Form C. For CLS purposes, two identifiers are the same if their lowercase mappings (as specified by the Unicode locale-insensitive, one-to-one lowercase mappings) are the same. That is, for two identifiers to be considered different under the CLS they shall differ in more than simply their case. However, in order to override an inherited definition the CLI requires the precise encoding of the original declaration be used.</summary>
            UnicodeIdentifiers = 4
            ''' <summary>5: All names introduced in a CLS-compliant scope shall be distinct independent of kind, except where the names are identical and resolved via overloading. That is, while the CTS allows a single type to use the same name for a method and a field, the CLS does not.</summary>
            DistinctNames = 5
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
            ''' <remarks>This rule is checked only for 1-dimensional arrays</remarks>
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
            ''' <remarks>This rule is not checked</remarks>
            CallBaseClassCTor = 21
            ''' <summary>22: An object constructor shall not be called except as part of the creation of an object, and an object shall not be initialized twice.</summary>
            ''' <remarks>This rule is not checked</remarks>
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
            PropertyNaming = 28
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
            ''' <remarks>Violation of this rule is reported only for op_Implicit and op_Explicit otheriwise <see cref="DistinctNames"/> (5) or <see cref="NoOverloadByReturnType"/> (6) is reported</remarks>
            OverloadingDistinction = 38
            ''' <summary>39: If either op_Implicit or op_Explicit is provided, an alternate means of providing the coercion shall be provided.</summary>
            ''' <remarks>This rule is not checked</remarks>
            AlternativeToOpImplicitAndOpExplicit = 39
            ''' <summary>40: Objects that are thrown shall be of type System.Exception or a type inheriting from it. Nonetheless, CLS-compliant methods are not required to block the propagation of other types of exceptions.</summary>
            ''' <remarks>This rule is not checked</remarks>
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
            ''' <remarks>This rule is not checked</remarks>
            GenericAbstractMethodsHaveDefaultImplementation = 47
            ''' <summary>48: If two or more CLS-compliant methods declared in a type have the same name and, for a specific set of type instantiations, they have the same parameter and return types, then all these methods shall be semantically equivalent at those type instantiations.</summary>
            ''' <remarks>This rule is not checked</remarks>
            GenericMethodsThatBecomeIndistinguishable = 48
            ''' <summary>Attribute usage is violated. This is not CLS rule.</summary>
            AttributeUsageViolation = -1
            ''' <summary>CSL-incompliant attribute is used. This is not violation of any CLS rule.</summary>
            CLSIncompliantAttribute = -2
            ''' <summary>Custom attribute data cannot be accessed because <see cref="ICustomFormatter"/> is neither <see cref="Assembly"/> nor <see cref="[Module]"/> nor <see cref="MemberInfo"/> nor <see cref="ParameterInfo"/>. You will unlikely encounter this warning.</summary>
            CannotAccessCustomAttributeData = -3
            ''' <summary>An error is encountered when checking certain item. This is not CLS rule.</summary>
            [Error] = -1000
        End Enum
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
        Private Shared ReadOnly IdentifierRegEx As New System.Text.RegularExpressions.Regex("^[\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}][\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}\p{Mn}\p{Mc}\p{Nd}\p{Pc}\p{Cf}]*$", Text.RegularExpressions.RegexOptions.CultureInvariant Or Text.RegularExpressions.RegexOptions.Compiled)
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
            End If
            'Check uniqueness of type names
            Dim types As IEnumerable(Of Type) = New Type() {}
            Try
                types = From type In Assembly.GetTypes() Where type.IsPublic AndAlso Not type.IsNested
            Catch ex As Exception
                OnViolation(ResourcesT.CLSComplianceCheckerResources.e_TypeInAssembly, CLSRule.Error, Assembly, ex)
            End Try
            CheckTypeNames(types, True)
            'Check modules
            Dim Modules() As [Module] = {}
            Try
                Modules = Assembly.GetModules
            Catch ex As Exception
                OnViolation(ResourcesT.CLSComplianceCheckerResources.e_ModulesInAssembly, CLSRule.Error, Assembly, ex)
            End Try
            For Each [Module] In Modules
                Violated = Violated Or Not CheckInternal([Module])
            Next
            Return Violated
        End Function
        ''' <summary>Calls <see cref="[Object].Equals"/> in exception-safe way</summary>
        ''' <typeparam name="T">Type of objects to compare</typeparam>
        ''' <param name="a">An object</param>
        ''' <param name="b">An object</param>
        ''' <returns><paramref name="a"/>.<see cref="[Object].Equals">Equals</see>(<paramref name="b"/>); false whrn it throws an exception; true when both - <paramref name="a"/> and <paramref name="b"/> are null; false when only <paramref name="a"/> is null.</returns>
        ''' <remarks>When <paramref name="b"/> is null, it is passed to <paramref name="a"/>.<see cref="[Object].Equals">Equals</see></remarks>
        <DebuggerStepThrough()> _
        Private Function SafeEquals(Of T)(ByVal a As T, ByVal b As T) As Boolean
            If a Is Nothing AndAlso b Is Nothing Then Return True
            If a Is Nothing Then Return False
            Try
                Return a.Equals(b)
            Catch
                Return False
            End Try
        End Function

        ''' <summary>Test if item is CLS - Compliant</summary>
        ''' <param name="Item">Item to test. Should be <see cref="Assembly"/>, <see cref="[Module]"/> or <see cref="MemberInfo"/></param>
        ''' <returns>True if <paramref name="Item"/> is declared to be CLS-compliant; false if it is not. It has either attached <see cref="CLSCompliantAttribute"/> or information is inherited; false is also returned when information cannot be got due to error while obtaining attributes. For <see cref="TypedReference"/> returns alawys false.</returns>
        ''' <remarks>When more <see cref="CLSCompliantAttribute">CLSCompliantAttributes</see> are attached to single item they are and-ed.
        ''' <para>In case <paramref name="Item"/> is <see cref="Type"/> and it is pointer, array or reference, examines its element instead (for pointer to pointer, array of array, array of pointers etc. examines element instead).</para>
        ''' <para>When <paramref name="Item"/> is <see cref="MethodInfo"/>, has no <see cref="CLSCompliantAttribute"/> attached and belongs to property or event, CLS-compliance of the property or the event is returned.</para>
        ''' </remarks>
        Private Function GetItemClsCompliance(ByVal Item As ICustomAttributeProvider) As Boolean
            If Item Is Nothing Then Throw New ArgumentNullException("Item")
            While TypeOf Item Is Type AndAlso (DirectCast(Item, Type).IsArray OrElse DirectCast(Item, Type).IsByRef OrElse DirectCast(Item, Type).IsPointer)
                Item = DirectCast(Item, Type).GetElementType
            End While
            If SafeEquals(Item, GetType(TypedReference)) Then Return False
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
                If TypeOf Item Is MethodInfo Then
                    With DirectCast(Item, MethodInfo)
                        Dim MethodProperty = .GetProperty
                        If MethodProperty IsNot Nothing Then Return GetItemClsCompliance(MethodProperty)
                        Dim MethodEvent = .GetEvent
                        If MethodEvent IsNot Nothing Then Return GetItemClsCompliance(MethodEvent)
                    End With
                End If
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
            Dim attrs As Object()
            Try
                attrs = Item.GetCustomAttributes(False)
            Catch ex As Exception
                OnViolation(ResourcesT.CLSComplianceCheckerResources.e_CustomAttributes, CLSRule.Error, Item, ex)
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
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.e_AttributeUsageAttribute, CLSRule.Error, attrType, ex)
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
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.AttributeUsage_AllowMultiple.f(attrType.FullName), CLSRule.AttributeUsageViolation, Item)
                    Violated = True
                End If
                If Not Types.Contains(attrType) Then Types.Add(attrType)
                If Check <> 0 AndAlso attributeattributes IsNot Nothing AndAlso attributeattributes.Length > 0 Then
                    Dim valid As Boolean = False
                    For Each attributeattr In attributeattributes
                        If (attributeattr.ValidOn And Check) = Check Then valid = True : Exit For
                    Next
                    If Not valid Then
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.AttributeUsage_ValidOn.f(attrType.FullName, Check), CLSRule.AttributeUsageViolation, Item)
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
                OnViolation(ResourcesT.CLSComplianceCheckerResources.e_TypesInModule_some, CLSRule.Error, [Module], ex)
            End Try
            CheckTypeNames(types, True)
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
            End If
            'Types
            Dim Types() As Type = {}
            Try
                Types = [Module].GetTypes()
            Catch ex As ReflectionTypeLoadException
                Types = (From type In ex.Types Where type IsNot Nothing).ToArray
                OnViolation(ResourcesT.CLSComplianceCheckerResources.e_TypesInModule_some, CLSRule.Error, [Module], ex)
            Catch ex As Exception
                OnViolation(ResourcesT.CLSComplianceCheckerResources.e_TypesInModule, CLSRule.Error, [Module], ex)
            End Try
            For Each Type In Types
                If Type.IsPublic AndAlso Not Type.IsNested Then Violated = Violated Or Not Check(Type)
            Next
            'Global methods
            Dim Methods() As MethodInfo = {}
            Try
                Methods = [Module].GetMethods
            Catch ex As Exception
                OnViolation(ResourcesT.CLSComplianceCheckerResources.e_MethodsInModule, CLSRule.Error, [Module], ex)
            End Try
            For Each Method In Methods
                If Method.IsPublic Then
                    Dim MethodCLSCompliant = GetItemClsCompliance(Method)
                    If MethodCLSCompliant Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.GlobalMethods, CLSRule.NoGlobalMembers, Method)
                    End If
                End If
            Next
            'Fields
            Dim Fields() As FieldInfo = {}
            Try
                Fields = [Module].GetFields
            Catch ex As Exception
                OnViolation(ResourcesT.CLSComplianceCheckerResources.e_FieldsInModule, CLSRule.Error, [Module], ex)
            End Try
            For Each Field In Fields
                If Field.IsPublic Then
                    Dim MethodCLSCompliant = GetItemClsCompliance(Field)
                    If MethodCLSCompliant Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.GlobalFields, CLSRule.NoGlobalMembers, Field)
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
                If Type.IsEnum Then Violated = Violated Or Not TestEnum(Type)
                'Base type
                If Type.BaseType IsNot Nothing AndAlso Not GetItemClsCompliance(Type.BaseType) Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.InheritCLSCompliant, CLSRule.NoIncompliantBase, Type)
                End If
                'Generics
                If Type.IsNested AndAlso Type.DeclaringType.IsGenericTypeDefinition AndAlso Type.GetGenericArguments.Length < Type.DeclaringType.GetGenericArguments.Length Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.NestedGenericTypeParametersCount, CLSRule.NestedGenericTypes, Type)
                End If
                If Type.IsGenericTypeDefinition AndAlso Not Type.IsNested Then
                    If Not Type.Name.EndsWith(String.Format(Globalization.CultureInfo.InvariantCulture, "`{0}", Type.GetGenericArguments.Count)) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.NameOfGenericType, CLSRule.GenericTypeName, Type)
                    End If
                End If
                If Type.IsGenericType AndAlso Type.IsNested Then
                    Dim DGA = Type.DeclaringType.GetGenericArguments
                    Dim MyGA = Type.GetGenericArguments
                    If MyGA.Length > DGA.Length AndAlso Not Type.Name.EndsWith(String.Format(Globalization.CultureInfo.InvariantCulture, "`{0}", MyGA.Length - DGA.Length)) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.NameOfNestedGenericType, CLSRule.GenericTypeName, Type)
                    End If
                End If
                'Nested generic constraints
                If Type.IsNested AndAlso Type.DeclaringType.IsGenericType Then
                    Dim DGA = Type.DeclaringType.GetGenericArguments
                    Dim MyGA = Type.GetGenericArguments
                    For i As Integer = 0 To DGA.Length - 1
                        Dim DGCs = DGA(i).GetGenericParameterConstraints
                        Dim MyGCs = MyGA(i).GetGenericParameterConstraints
                        If DGCs.Length > 0 Then
                            For Each DGC In DGCs
                                If Not MyGCs.Contains(GenericConstraintFromParentToNested(DGC, Type.DeclaringType, Type)) Then
                                    Violated = True
                                    OnViolation(ResourcesT.CLSComplianceCheckerResources.NestedGenericConstraint.f(If(MyGA(i).FullName, MyGA(i).Name), If(Type.FullName, Type.Name), If(DGC.FullName, DGC.Name)), CLSRule.NestedGenricTypeConstraints, MyGA(i))
                                End If
                            Next
                        End If
                        If (DGA(i).GenericParameterAttributes And GenericParameterAttributes.DefaultConstructorConstraint) = GenericParameterAttributes.DefaultConstructorConstraint AndAlso Not (MyGA(i).GenericParameterAttributes And GenericParameterAttributes.DefaultConstructorConstraint) = GenericParameterAttributes.DefaultConstructorConstraint Then
                            Violated = True
                            OnViolation(ResourcesT.CLSComplianceCheckerResources.NestedGenericConstraint_New, CLSRule.NestedGenricTypeConstraints, MyGA(i))
                        End If
                        If (DGA(i).GenericParameterAttributes And GenericParameterAttributes.NotNullableValueTypeConstraint) = GenericParameterAttributes.NotNullableValueTypeConstraint AndAlso Not (MyGA(i).GenericParameterAttributes And GenericParameterAttributes.NotNullableValueTypeConstraint) = GenericParameterAttributes.NotNullableValueTypeConstraint Then
                            Violated = True
                            OnViolation(ResourcesT.CLSComplianceCheckerResources.NestedGenericConstraint_Struct, CLSRule.NestedGenricTypeConstraints, MyGA(i))
                        End If
                        If (DGA(i).GenericParameterAttributes And GenericParameterAttributes.ReferenceTypeConstraint) = GenericParameterAttributes.ReferenceTypeConstraint AndAlso Not (MyGA(i).GenericParameterAttributes And GenericParameterAttributes.ReferenceTypeConstraint) = GenericParameterAttributes.ReferenceTypeConstraint Then
                            Violated = True
                            OnViolation(ResourcesT.CLSComplianceCheckerResources.NestedGenericConstraint_Class, CLSRule.NestedGenricTypeConstraints, MyGA(i))
                        End If
                    Next
                End If
                'Generic constraints
                If Type.IsGenericType Then
                    For Each MyGA In Type.GetGenericArguments
                        For Each MyGC In MyGA.GetGenericParameterConstraints
                            CheckTypeReference(MyGC, MyGA, True, CLSRule.NoClsIncompliantConstraints)
                        Next
                    Next
                End If
                'Mmembers
                Dim Members() As MemberInfo = {}
                Try
                    Members = Type.GetMembers(BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.DeclaredOnly)
                Catch ex As Exception
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.e_MembersInType, CLSRule.Error, Type, ex)
                    Return False
                End Try
                Dim MemberSignatures As New List(Of MemberCLSSignature)
                Dim ValueFound = False 'For enums only
                For Each Member In Members
                    Dim MemberVisible = Member.IsPublic OrElse Member.IsFamily OrElse Member.IsFamilyOrAssembly
                    Dim MemberCompliant = GetItemClsCompliance(Member)
                    If MemberVisible Then _
                        Violated = Violated Or Not CheckInternal(Member)
                    If MemberVisible AndAlso MemberCompliant Then
                        Dim ms As New MemberCLSSignature(Member)
                        If MemberSignatures.Contains(ms) Then
                            Violated = True
                            Dim r = CLSRule.DistinctNames
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
                            ElseIf (TypeOf Member Is EventInfo OrElse TypeOf Member Is EventInfo) AndAlso Not ms.Equals(Original) Then
                                r = CLSRule.OverloadOnlyPropertiesAndMethods
                            End If
                            OnViolation(ResourcesT.CLSComplianceCheckerResources.UniqueSignature, r, Member)
                        Else
                            MemberSignatures.Add(ms)
                        End If
                        'Enum
                        If Type.IsEnum Then
                            If Not TypeOf Member Is FieldInfo Then
                                Violated = True
                                OnViolation(ResourcesT.CLSComplianceCheckerResources.EnumerationOnlyFields, CLSRule.EnumSructure, Member)
                            Else
                                With DirectCast(Member, FieldInfo)
                                    If Not .IsLiteral Then
                                        If .IsStatic OrElse .Name <> "value__" OrElse ((.Attributes And FieldAttributes.RTSpecialName) <> FieldAttributes.RTSpecialName) Then
                                            Violated = True
                                            OnViolation(ResourcesT.CLSComplianceCheckerResources.EnumerationFieldKind, CLSRule.EnumSructure, Member)
                                        ElseIf Not .FieldType.Equals([Enum].GetUnderlyingType(Type)) Then
                                            Violated = True
                                            OnViolation(ResourcesT.CLSComplianceCheckerResources.EnumerationValue__Type, CLSRule.EnumSructure, Member)
                                        Else
                                            ValueFound = True
                                        End If
                                    ElseIf Not .IsStatic Then
                                        Violated = True
                                        OnViolation(ResourcesT.CLSComplianceCheckerResources.EnumerationLiteralStatic, CLSRule.EnumMembers, Member)
                                    ElseIf Not .FieldType.Equals(Type) Then
                                        Violated = True
                                        OnViolation(ResourcesT.CLSComplianceCheckerResources.TypeOfEnumMember, CLSRule.EnumMembers, Member)
                                    End If
                                End With
                            End If
                        End If
                        'special member rules
                        Violated = Violated Or Not CheckInternal(Member)
                    End If
                    If MemberVisible AndAlso Type.IsInterface AndAlso (Member.MemberType = MemberTypes.Method OrElse Member.MemberType = MemberTypes.Property OrElse Member.MemberType = MemberTypes.Event) AndAlso Not Member.IsStatic Then
                        If Not MemberCompliant Then
                            Violated = True
                            OnViolation(ResourcesT.CLSComplianceCheckerResources.InterfaceCLSIncompliantMember, CLSRule.NoIncompliantMembersInInterfaces, Member)
                        End If
                    End If
                    If Type.IsInterface AndAlso Member.IsStatic AndAlso Member.MemberType = MemberTypes.Method AndAlso MemberCompliant AndAlso MemberVisible Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.InterfaceStaticMethod, CLSRule.NoStaticMembersAndFieldsInInterfaces, Member)
                    End If
                    If Type.IsInterface AndAlso Member.MemberType = MemberTypes.Field AndAlso MemberCompliant AndAlso MemberVisible Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.InterfaceField, CLSRule.NoStaticMembersAndFieldsInInterfaces, Member)
                    End If
                    If Type.IsClass AndAlso Type.IsAbstract AndAlso Member.MemberType = MemberTypes.Method AndAlso DirectCast(Member, MethodInfo).IsAbstract AndAlso Not MemberCompliant AndAlso MemberVisible Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.RequireImplementIncompliant, CLSRule.NoNeedToImplementIncompliantMember, Member)
                    End If
                Next
                If Type.IsEnum AndAlso Not ValueFound Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.MissingValue__, CLSRule.EnumSructure, Type)
                End If
            Else
                Violated = Not SearchForCompliantMembers(Type)
            End If
            Return Not Violated
        End Function
        ''' <summary>Gets generic type type argument constraint and replaces all references (including <paramref name="ConstraintType"/> itself) to any type parameter of parent type with reference to corresponding type parameter of nested type</summary>
        ''' <param name="ConstraintType">Type constraint to replace. It is constraint specified on <paramref name="ParentType"/></param>
        ''' <param name="ParentType">Parent type of <paramref name="NestedType"/></param>
        ''' <param name="NestedType">Nested type <paramref name="ConstraintType"/> comes from</param>
        ''' <returns><paramref name="ConstraintType"/> transferred form <paramref name="ParentType"/> to <paramref name="NestedType"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="ConstraintType"/>, <paramref name="ParentType"/> or <paramref name="NestedType"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="NestedType"/>.<see cref="Type.DeclaringType">DeclaringType</see> is not <paramref name="ParentType"/>.</exception>
        Private Function GenericConstraintFromParentToNested(ByVal ConstraintType As Type, ByVal ParentType As Type, ByVal NestedType As Type) As Type
            If ConstraintType Is Nothing Then Throw New ArgumentNullException("ConstraintType")
            If ParentType Is Nothing Then Throw New ArgumentNullException("ParentType")
            If NestedType Is Nothing Then Throw New ArgumentNullException("NestedType")
            If NestedType.DeclaringType IsNot ParentType Then Throw New ArgumentException(ResourcesT.Exceptions.ParentTypeOf0MustBe1.f("NestedType", "ParentType"))
            If ConstraintType.IsPointer Then
                Return GenericConstraintFromParentToNested(ConstraintType.GetElementType, ParentType, NestedType).MakePointerType
            ElseIf ConstraintType.IsByRef Then
                Return GenericConstraintFromParentToNested(ConstraintType.GetElementType, ParentType, NestedType).MakeByRefType
            ElseIf ConstraintType.IsVector Then
                Return GenericConstraintFromParentToNested(ConstraintType.GetElementType, ParentType, NestedType).MakeArrayType
            ElseIf ConstraintType.IsArray Then
                Return GenericConstraintFromParentToNested(ConstraintType.GetElementType, ParentType, NestedType).MakeArrayType(ConstraintType.GetArrayRank)
            ElseIf ConstraintType.IsGenericType Then
                Return ConstraintType.GetGenericTypeDefinition.MakeGenericType((From garg In ConstraintType.GetGenericArguments Select GenericConstraintFromParentToNested(garg, ParentType, NestedType)).ToArray)
            ElseIf ParentType.GetGenericArguments.Contains(ConstraintType) Then
                Return NestedType.GetGenericArguments()(Array.IndexOf(ParentType.GetGenericArguments, ConstraintType))
            Else
                Return ConstraintType
            End If
        End Function

        ''' <summary>Tests if enumeration is CLS-compliant</summary>
        ''' <param name="Type">Enum type to test</param>
        ''' <returns>True if enumeration is CLS-compliant; false otherwise</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Type"/> is not enumeration</exception>
        Private Function TestEnum(ByVal Type As Type) As Boolean
            If Type Is Nothing Then Throw New ArgumentNullException("Type")
            If Not Type.IsEnum Then Throw New ArgumentException(ResourcesT.Exceptions.TypeMustBeEnumeration, "Type")
            'Rule 7:
            Dim ut = [Enum].GetUnderlyingType(Type)
            Dim violated As Boolean = False
            If Not ut.Equals(GetType(Byte)) AndAlso Not ut.Equals(GetType(Short)) AndAlso Not ut.Equals(GetType(Integer)) AndAlso Not ut.Equals(GetType(Long)) Then
                violated = True
                OnViolation(ResourcesT.CLSComplianceCheckerResources.EnumType, CLSRule.EnumSructure, Type)
            End If
            Return Not violated
        End Function

        ''' <summary>Stores member singature and allows it comparison (via <see cref="MemberCLSSignature.Equals"/>) for CLS-uniqueness purposes</summary>
        Private Class MemberCLSSignature
            ''' <summary>Name of member</summary>
            Private Name As String
            ''' <summary>Types of atributes</summary>
            Private Types As New List(Of Type)
            ''' <summary>Number fo generic parameters</summary>
            ''' <remarks>Only for generic methods</remarks>
            Private nGpars% = 0
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
                        If .IsSpecialName AndAlso (.Name = "op_Explicit" OrElse .Name = "op_Implicit") Then ReturnType = .ReturnType
                        If .IsGenericMethod Then Me.nGpars = .GetGenericArguments.Length
                    End With
                End If
            End Sub
            ''' <summary>Returns new instance created by adding return type to current instace (if applicable)</summary>
            ''' <returns>Return-type-aware instance</returns>
            Public Function AddReturnType() As MemberCLSSignature
                Dim ret As New MemberCLSSignature(Original)
                If TypeOf Original Is MethodInfo Then : ret.ReturnType = DirectCast(Original, MethodInfo).ReturnType
                ElseIf TypeOf Original Is PropertyInfo Then : ret.ReturnType = DirectCast(Original, PropertyInfo).PropertyType
                ElseIf TypeOf Original Is FieldInfo Then : ret.ReturnType = DirectCast(Original, FieldInfo).FieldType
                ElseIf TypeOf Original Is EventInfo Then : ret.ReturnType = DirectCast(Original, EventInfo).EventHandlerType
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
                        If Me.nGpars <> .nGpars Then Return False
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
            Catch ex As Exception
                OnViolation(ResourcesT.CLSComplianceCheckerResources.e_MembersInType, CLSRule.Error, Type, ex)
                Return False
            End Try
            Dim Violated = False
            For Each Member In Members
                If Member.IsPublic OrElse Member.IsFamily OrElse Member.IsFamilyOrAssembly Then
                    If GetItemClsCompliance(Member) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.CLSCompliantInCLSIncompliant, CLSRule.NoCompliantMembersInIncompliantTypes, Member)
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
                If Not GetItemClsCompliance([Property].PropertyType) Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.PropertyType, CLSRule.Signature, [Property])
                End If
                Violated = Violated Or Not CheckTypeReference([Property].PropertyType, [Property], False)
                If [Property].PropertyType.IsGenericType Then Violated = Violated Or Not CheckGenericInstance([Property].PropertyType, [Property])
                Violated = Violated Or Not CheckTypeAcessibility([Property], [Property].PropertyType)
                'get parameters
                Dim Params As ParameterInfo() = {}
                Try
                    Params = [Property].GetIndexParameters
                Catch ex As Exception
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.e_IndexesInProperty, CLSRule.Error, [Property], ex)
                End Try
                'Get getter and setter
                Dim Getter = [Property].GetGetMethod(True)
                Dim Setter = [Property].GetSetMethod(True)
                If Getter Is Nothing AndAlso Setter Is Nothing Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.NoPropertyAccesor, CLSRule.PropertyNaming, [Property])
                End If
                'Check setter vs. getter
                If Setter IsNot Nothing AndAlso Getter IsNot Nothing Then
                    If Not Setter.IsSpecialName Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.SetterSpecialName, CLSRule.SpecialNameGetterAndSetter, Setter)
                    End If
                    If Setter.IsStatic <> Getter.IsStatic Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.GetterSetterStatic, CLSRule.SameKindOfGetterAndSetter, [Property])
                    End If
                    If Setter.IsVirtual <> Getter.IsVirtual Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.GetterSetterVirtual, CLSRule.SameKindOfGetterAndSetter, [Property])
                    End If
                End If
                'Check getter
                Dim gpars As ParameterInfo() = Nothing
                If Getter IsNot Nothing Then
                    If Not Getter.IsSpecialName Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.GetterSpecialName, CLSRule.SpecialNameGetterAndSetter, Getter)
                    End If
                    gpars = New ParameterInfo() {}
                    Try
                        gpars = Getter.GetParameters
                    Catch ex As Exception
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.ParametersInGetter, CLSRule.Error, Getter)
                    End Try
                    If Not Getter.ReturnType.Equals([Property].PropertyType) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.GetterType, CLSRule.PropertyType, Getter)
                    End If
                    If gpars.Length <> Params.Length Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.GetterParametersCount, CLSRule.PropertyType, Getter)
                    End If
                    If Getter.Name <> "get_" & [Property].Name Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.GetterNameFormat("get_" & [Property].Name, Getter.Name), CLSRule.PropertyNaming, Getter)
                    End If
                End If
                'Check setter
                Dim spars As ParameterInfo() = Nothing
                If Setter IsNot Nothing Then
                    spars = New ParameterInfo() {}
                    Try
                        spars = Setter.GetParameters
                    Catch ex As Exception
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.e_ParametersInSetter, CLSRule.Error, Setter)
                    End Try
                    If spars.Length = 0 OrElse Not spars(spars.Length - 1).ParameterType.Equals([Property].PropertyType) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.SetterLastParam, CLSRule.PropertyType, Setter)
                    End If
                    If spars.Length - 1 <> Params.Length Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.SetterParametersCount, CLSRule.PropertyType, Setter)
                    End If
                    If Setter.Name <> "set_" & [Property].Name Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.SetterNameFormat("set_" & [Property].Name, Setter.Name), CLSRule.PropertyNaming, Setter)
                    End If
                End If
                'Check params
                Dim i As Integer = 0
                For Each Param In Params
                    If Not GetItemClsCompliance(Param.ParameterType) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.IndexType.f(If(Param.ParameterType.FullName, Param.ParameterType.Name)), CLSRule.Signature, Param)
                    End If
                    Violated = Violated Or Not CheckTypeReference(Param.ParameterType, [Property], False)
                    If Param.ParameterType.IsGenericType Then Violated = Violated Or Not CheckGenericInstance(Param.ParameterType, Param)
                    Violated = Violated Or Not CheckTypeAcessibility([Property], Param.ParameterType)
                    If Param.ParameterType.IsByRef Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.PropertyByRef, CLSRule.PropertyType, Param)
                    End If
                    If spars IsNot Nothing AndAlso spars.Length - 1 = Params.Length AndAlso Not spars(i).ParameterType.Equals(Param.ParameterType) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.PropertySetterParameters, CLSRule.PropertyType, spars(i))
                    End If
                    If gpars IsNot Nothing AndAlso gpars.Length = Params.Length AndAlso Not gpars(i).ParameterType.Equals(Param.ParameterType) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.PropertyGetterParameters, CLSRule.PropertyType, gpars(i))
                    End If
                    i += 1
                Next
                If [Property].GetRequiredCustomModifiers.Length > 0 Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.Modreqs, CLSRule.NoModReq, [Property])
                End If
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
                If Not GetItemClsCompliance([Event].EventHandlerType) Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.EventType, CLSRule.Signature, [Event])
                End If
                Violated = Violated Or Not CheckTypeReference([Event].EventHandlerType, [Event], False)
                If [Event].EventHandlerType.IsGenericType Then Violated = Violated Or Not CheckGenericInstance([Event].EventHandlerType, [Event])
                Violated = Violated Or Not CheckTypeAcessibility([Event], [Event].EventHandlerType)
                'Methods
                Dim Add = [Event].GetAddMethod
                Dim Remove = [Event].GetRemoveMethod
                Dim Raise = [Event].GetRaiseMethod
                'All methods
                If (Add Is Nothing) <> (Remove Is Nothing) Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.AddRemoveBothOrNo, CLSRule.AddAndRemove, [Event])
                End If
                If Not GetType([Delegate]).IsAssignableFrom([Event].EventHandlerType) Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.EventTypeDelegate, CLSRule.AddAndRemoveParameters, [Event])
                End If
                Dim Accessibility As MethodAttributes?
                'Add
                If Add IsNot Nothing Then
                    If Not Add.IsSpecialName Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.AddSpecialName, CLSRule.SpecialNameEvent, Add)
                    End If
                    Accessibility = Add.Attributes And MethodAttributes.MemberAccessMask
                    If Not Add.GetParameters.Length = 1 OrElse Not Add.GetParameters()(0).ParameterType.Equals([Event].EventHandlerType) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.AddParameters, CLSRule.AddAndRemoveParameters, Add)
                    End If
                    If Add.Name <> "add_" & [Event].Name Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.AddName, CLSRule.EventNaming, Add)
                    End If
                End If
                'Remove
                If Remove IsNot Nothing Then
                    If Not Remove.IsSpecialName Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.RemoveSpecialName, CLSRule.SpecialNameEvent, Remove)
                    End If
                    If Not Accessibility.HasValue Then
                        Accessibility = Remove.Attributes And MethodAttributes.MemberAccessMask
                    ElseIf Accessibility <> (Remove.Attributes And MethodAttributes.MemberAccessMask) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.EventAccessorsVisibility, CLSRule.EventAccessibility, [Event])
                    End If
                    If Not Remove.GetParameters.Length = 1 OrElse Not Remove.GetParameters()(0).ParameterType.Equals([Event].EventHandlerType) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.RemoveParameters, CLSRule.AddAndRemoveParameters, Add)
                    End If
                    If Remove.Name <> "remove_" & [Event].Name Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.RemoveName, CLSRule.EventNaming, Remove)
                    End If
                End If
                'Raise
                If Raise IsNot Nothing Then
                    If Not Raise.IsSpecialName Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.RaiseSpecialName, CLSRule.SpecialNameEvent, Raise)
                    End If
                    If Not Accessibility.HasValue Then
                        Accessibility = Raise.Attributes And MethodAttributes.MemberAccessMask
                    ElseIf Accessibility <> (Raise.Attributes And MethodAttributes.MemberAccessMask) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.EventAccessorsVisibility, CLSRule.EventAccessibility, [Event])
                    End If
                    If Raise.Name <> "raise_" & [Event].Name Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.RaiseName, CLSRule.EventNaming, Raise)
                    End If
                End If

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
                If Not Method.DeclaringType IsNot Nothing AndAlso Not Method.DeclaringType.BaseType IsNot Nothing Then
                    Dim BaseMethod = Method.GetBaseClassMethod
                    If BaseMethod IsNot Nothing Then
                        Dim BaseAccess = BaseMethod.Attributes And MethodAttributes.MemberAccessMask
                        Dim MyAccess = Method.Attributes And MethodAttributes.MemberAccessMask
                        If BaseAccess <> MyAccess AndAlso Not (BaseAccess = MethodAttributes.FamORAssem AndAlso MyAccess = MethodAttributes.Family AndAlso Not Method.Module.Assembly.Equals(BaseMethod.Module.Assembly)) Then
                            Violated = True
                            OnViolation(ResourcesT.CLSComplianceCheckerResources.OverrididedLevel, CLSRule.NoChangeOfAccessWhenOverrideing, Method)
                        End If
                    End If
                End If
                Dim Params() As ParameterInfo = {}
                Try
                    Params = Method.GetParameters
                Catch ex As Exception
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.e_ParamsInMethod, CLSRule.Error, Method, ex)
                End Try
                For Each param In Params
                    If Not GetItemClsCompliance(param.ParameterType) Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.ParamType.f(param.Name), CLSRule.Signature, param)
                    End If
                    Violated = Violated Or Not CheckTypeReference(param.ParameterType, Method, False)
                    If param.ParameterType.IsGenericType Then Violated = Violated Or Not CheckGenericInstance(param.ParameterType, param)
                    Violated = Violated Or Not CheckTypeAcessibility(Method, param.ParameterType)
                    If param.GetRequiredCustomModifiers.Length > 0 Then
                        Violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.Modreqs, CLSRule.NoModReq, param)
                    End If
                Next
                If Not GetItemClsCompliance(Method.ReturnType) Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.ReturnType, CLSRule.Signature, Method.ReturnParameter)
                End If
                Violated = Violated Or Not CheckTypeReference(Method.ReturnType, Method, False)
                If Method.ReturnType.IsGenericType Then Violated = Violated Or Not CheckGenericInstance(Method.ReturnType, Method.ReturnParameter)
                Violated = Violated Or Not CheckTypeAcessibility(Method, Method.ReturnType)
                If (Method.CallingConvention And CallingConventions.Standard) <> CallingConventions.Standard Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.StandardCallingConvention, CLSRule.CallingConvention, Method)
                End If
                If (Method.CallingConvention And CallingConventions.VarArgs) = CallingConventions.VarArgs Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.VarArgsAreNotCLSCompliant, CLSRule.CallingConvention, Method)
                End If
                If Method.ReturnParameter.GetRequiredCustomModifiers.Length > 0 Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.Modreqs, CLSRule.NoModReq, Method.ReturnParameter)
                End If
            End If
            Return Not Violated
        End Function
        ''' <summary>Checks if all types pased to instantiated generic type are declared to be CLS compliant</summary>
        ''' <param name="inst">Type to check generic parameters of</param>
        ''' <param name="On">Element to report violation on</param>
        ''' <param name="Rule">CLS rule to report violation of</param>
        ''' <returns>True if all the generic parameters are declared to be CLS-compliant (including nested type parameters); false otherwise</returns>
        ''' <remarks>If <paramref name="inst"/> is array, pointer or rerefernce, examines its element type instead. For array of arrays, pointer to pointer, pointer to array etc. examines the inner most element instead.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="inst"/> is null or <paramref name="On"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="inst"/>.<see cref="Type.IsGenericType">IsGenericType</see> is false</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Rule"/> is not member of <see cref="CLSRule"/></exception>
        Private Function CheckGenericInstance(ByVal inst As Type, ByVal [On] As ICustomAttributeProvider, Optional ByVal Rule As CLSRule = CLSRule.Signature) As Boolean
            If inst Is Nothing Then Throw New ArgumentNullException("inst")
            If [On] Is Nothing Then Throw New ArgumentNullException("On")
            If Not inst.IsGenericType Then Throw New ArgumentException(ResourcesT.Exceptions.MustBeGenericTypeInstance, "inst")
            If Not Rule.IsDefined Then Throw New InvalidEnumArgumentException("Rule", Rule, Rule.GetType)
            While inst.IsPointer OrElse inst.IsByRef OrElse inst.IsArray
                inst = inst.GetElementType
            End While
            Dim violated As Boolean = False
            For Each gparam In inst.GetGenericArguments
                If Not GetItemClsCompliance(gparam) Then
                    violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.GenericParamType.f(If(gparam.FullName, gparam.Name)), Rule, [On])
                End If
                If gparam.IsGenericType Then violated = violated Or Not CheckTypeReference(gparam, [On], True, Rule)
            Next
            Return Not violated
        End Function
        ''' <summary>Checks if type reference is CLS-compliant</summary>
        ''' <param name="Type">Type being referenced</param>
        ''' <param name="On">Repports violations on</param>
        ''' <param name="ReportGetItemClsCompliance">False not to report violation by <see cref="GetItemClsCompliance"/></param>
        ''' <returns>True if type is CLS-compliant (declarativelly); false if it is not</returns>
        Private Function CheckTypeReference(ByVal Type As Type, ByVal [On] As ICustomAttributeProvider, ByVal ReportGetItemClsCompliance As Boolean, Optional ByVal Rule As CLSRule = Integer.MinValue) As Boolean
            If Type Is Nothing Then Throw New ArgumentNullException("inst")
            If [On] Is Nothing Then Throw New ArgumentNullException("On")
            Dim Violated As Boolean = False
            If Not GetItemClsCompliance(Type) Then
                Violated = True
                If ReportGetItemClsCompliance Then OnViolation(ResourcesT.CLSComplianceCheckerResources.IncompliantType.f(If(Type.FullName, Type.Name)), If(Rule = Integer.MinValue, CLSRule.Signature, Rule), [On])
            End If
            If Type.IsGenericType Then Violated = Violated Or Not CheckGenericInstance(Type, [On])
            If Type.Equals(GetType(TypedReference)) Then
                Violated = True
                OnViolation(ResourcesT.CLSComplianceCheckerResources.TypedReference, If(Rule = Integer.MinValue, CLSRule.NoTypedReferences, Rule), [On])
            End If
            If Type.IsArray AndAlso Type.GetArrayRank > 1 AndAlso Not Type.IsVector Then
                Violated = True
                OnViolation(ResourcesT.CLSComplianceCheckerResources.Array0, If(Rule = Integer.MinValue, CLSRule.Arrays, Rule), [On])
            End If
            If Type.IsPointer Then
                Violated = True
                OnViolation(ResourcesT.CLSComplianceCheckerResources.Pointer, If(Rule = Integer.MinValue, CLSRule.NoUnmanagedPointer, Rule), [On])
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
                If Not GetItemClsCompliance(Field.FieldType) Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.FieldType, CLSRule.Signature, Field)
                End If
                Violated = Violated Or Not CheckTypeReference(Field.FieldType, Field, False)
                If Field.FieldType.IsGenericType Then Violated = Violated Or Not CheckGenericInstance(Field.FieldType, Field)
                Violated = Violated Or Not CheckTypeAcessibility(Field, Field.FieldType)
                If Field.IsLiteral Then
                    Dim LiteralValue = Field.GetRawConstantValue
                    If LiteralValue IsNot Nothing Then
                        Dim LiteralType = LiteralValue.GetType
                        Dim FieldType = Field.FieldType
                        If Not FieldType.Equals(LiteralType) AndAlso Not (FieldType.IsEnum AndAlso [Enum].GetUnderlyingType(FieldType).Equals(LiteralType)) Then
                            Violated = True
                            OnViolation(ResourcesT.CLSComplianceCheckerResources.ConstantValue, CLSRule.ValueOfLiteral, Field)
                        End If
                    End If
                End If
                If Field.GetRequiredCustomModifiers.Length > 0 Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.Modreqs, CLSRule.NoModReq, Field)
                End If
            End If
            Return Not Violated
        End Function
        ''' <summary>Checks if atributes applied on item are all CLS-compliant</summary>
        ''' <param name="Item">Item to cehck attributes of</param>
        ''' <returns>True if all the attributes are CLS-compliant; false otheriwise. Returns tru if there are no attributes.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Item"/> is null</exception>
        Private Function CLSAttributeCheck(ByVal Item As ICustomAttributeProvider) As Boolean
            If Item Is Nothing Then Throw New ArgumentNullException("Item")
            Dim Violated As Boolean = False
            Dim attrs() As Object = {}
            Try
                attrs = Item.GetCustomAttributes(False)
            Catch ex As Exception
                OnViolation(ResourcesT.CLSComplianceCheckerResources.e_CustomAttributes, CLSRule.Error, Item, ex)
            End Try
            For Each attr In attrs
                If Not TypeOf attr Is Attribute Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.NoAttributeAttribute.f(attr.GetType.FullName), CLSRule.AttributeType, Item)
                End If
                If Not GetItemClsCompliance(attr.GetType) Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.CLSInclompliantAttribute.f(attr.GetType.FullName), CLSRule.CLSIncompliantAttribute, Item)
                End If
                If attr.GetType.IsGenericType Then Violated = Violated Or Not CheckGenericInstance(attr.GetType, Item, CLSRule.CLSIncompliantAttribute)
            Next
            Dim cad As IList(Of CustomAttributeData) = Nothing
            If TypeOf Item Is Assembly Then
                cad = CustomAttributeData.GetCustomAttributes(DirectCast(Item, Assembly))
            ElseIf TypeOf Item Is [Module] Then
                cad = CustomAttributeData.GetCustomAttributes(DirectCast(Item, [Module]))
            ElseIf TypeOf Item Is MemberInfo Then
                cad = CustomAttributeData.GetCustomAttributes(DirectCast(Item, MemberInfo))
            ElseIf TypeOf Item Is ParameterInfo Then
                cad = CustomAttributeData.GetCustomAttributes(DirectCast(Item, ParameterInfo))
            Else
                OnViolation(ResourcesT.CLSComplianceCheckerResources.CustomAttributeDataInaccessible.f(Item), CLSRule.CannotAccessCustomAttributeData, Item)
            End If
            If cad IsNot Nothing Then
                For Each cadata In cad
                    For Each param In cadata.ConstructorArguments
                        If Not param.ArgumentType.Equals(GetType(Type)) AndAlso Not param.ArgumentType.Equals(GetType(String)) AndAlso Not param.ArgumentType.Equals(GetType(Char)) AndAlso Not param.ArgumentType.Equals(GetType(Boolean)) AndAlso Not param.ArgumentType.Equals(GetType(Byte)) AndAlso Not param.ArgumentType.Equals(GetType(Int16)) AndAlso Not param.ArgumentType.Equals(GetType(Int32)) AndAlso Not param.ArgumentType.Equals(GetType(Int64)) AndAlso Not param.ArgumentType.Equals(GetType(Single)) AndAlso Not param.ArgumentType.Equals(GetType(Double)) AndAlso _
                                Not (param.ArgumentType.IsEnum AndAlso ( _
                                     [Enum].GetUnderlyingType(param.ArgumentType).Equals(GetType(Byte)) OrElse [Enum].GetUnderlyingType(param.ArgumentType).Equals(GetType(Int16)) OrElse [Enum].GetUnderlyingType(param.ArgumentType).Equals(GetType(Int32)) OrElse [Enum].GetUnderlyingType(param.ArgumentType).Equals(GetType(Int64)))) Then
                            Violated = True
                            OnViolation(ResourcesT.CLSComplianceCheckerResources.AttributeParamType.f(cadata.Constructor.DeclaringType.FullName, If(param.ArgumentType.FullName, param.ArgumentType.Name)), CLSRule.AttributeValues, Item)
                        End If
                    Next
                    For Each param In cadata.NamedArguments
                        If Not param.TypedValue.ArgumentType.Equals(GetType(Type)) AndAlso Not param.TypedValue.ArgumentType.Equals(GetType(String)) AndAlso Not param.TypedValue.ArgumentType.Equals(GetType(Char)) AndAlso Not param.TypedValue.ArgumentType.Equals(GetType(Boolean)) AndAlso Not param.TypedValue.ArgumentType.Equals(GetType(Byte)) AndAlso Not param.TypedValue.ArgumentType.Equals(GetType(Int16)) AndAlso Not param.TypedValue.ArgumentType.Equals(GetType(Int32)) AndAlso Not param.TypedValue.ArgumentType.Equals(GetType(Int64)) AndAlso Not param.TypedValue.ArgumentType.Equals(GetType(Single)) AndAlso Not param.TypedValue.ArgumentType.Equals(GetType(Double)) AndAlso _
                              Not (param.TypedValue.ArgumentType.IsEnum AndAlso ( _
                                   [Enum].GetUnderlyingType(param.TypedValue.ArgumentType).Equals(GetType(Byte)) OrElse [Enum].GetUnderlyingType(param.TypedValue.ArgumentType).Equals(GetType(Int16)) OrElse [Enum].GetUnderlyingType(param.TypedValue.ArgumentType).Equals(GetType(Int32)) OrElse [Enum].GetUnderlyingType(param.TypedValue.ArgumentType).Equals(GetType(Int64)))) Then
                            Violated = True
                            OnViolation(ResourcesT.CLSComplianceCheckerResources.AttributeNamedParamType.f(cadata.Constructor.DeclaringType.FullName, param.MemberInfo.Name, If(param.TypedValue.ArgumentType.FullName, param.TypedValue.ArgumentType.Name)), CLSRule.AttributeValues, Item)
                        End If
                    Next
                Next
            End If
            Return Not Violated
        End Function

        Private gTypeNameRegEx As New Text.RegularExpressions.Regex("`\d+$", Text.RegularExpressions.RegexOptions.Compiled Or Text.RegularExpressions.RegexOptions.CultureInvariant)

        ''' <summary>Peprforms common CLS-compliance test on member</summary>
        ''' <param name="Item">Member to do tests on</param>
        ''' <returns>True if no CLS-violation was detected; false otherwise.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Item"/> is null</exception>
        Private Function DoCommonTest(ByVal Item As MemberInfo) As Boolean
            If Item Is Nothing Then Throw New ArgumentNullException("Item")
            Dim violated = Not CLSAttributeCheck(Item)
            If Not TypeOf Item Is ConstructorInfo AndAlso Not TypeOf Item Is Type AndAlso Not IdentifierRegEx.IsMatch(Item.Name) Then
                violated = True
                OnViolation(ResourcesT.CLSComplianceCheckerResources.Name.f(Item.Name), CLSRule.UnicodeIdentifiers, Item)
            ElseIf TypeOf Item Is Type Then
                With DirectCast(Item, Type)
                    Dim vio As Boolean
                    If (.IsGenericType OrElse .IsGenericTypeDefinition) AndAlso gTypeNameRegEx.IsMatch(.Name) Then
                        vio = Not IdentifierRegEx.IsMatch(.Name.Substring(0, .Name.LastIndexOf("`")))
                    Else
                        vio = Not IdentifierRegEx.IsMatch(.Name)
                    End If
                    If vio Then
                        violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.Name.f(Item.Name), CLSRule.UnicodeIdentifiers, Item)
                    End If
                End With
            End If
            Dim Normalized = Item.Name.Normalize(Text.NormalizationForm.FormC)
            If Normalized.Length <> Item.Name.Length Then
                violated = True
                OnViolation(ResourcesT.CLSComplianceCheckerResources.NameC, CLSRule.UnicodeIdentifiers, Item)
            Else
                For i As Integer = 0 To Normalized.Length - 1
                    If AscW(Normalized(i)) <> AscW(Item.Name(i)) Then
                        violated = True
                        OnViolation(ResourcesT.CLSComplianceCheckerResources.NameC, CLSRule.UnicodeIdentifiers, Item)
                        Exit For
                    End If
                Next
            End If
            Return Not violated
        End Function
        ''' <summary>Checks uniqueness of type names amongs given enumerations</summary>
        ''' <param name="Types">Types to verify uniqueness of names of</param>
        ''' <param name="fullname">True to use <see cref="Type.FullName"/>, false to use <see cref="Type.Name"/>. Set true for types in assemblies/modules; false otherwise. When true, but <see cref="Type.FullName"/> is null, <see cref="Type.Name"/> is used instead.</param>
        ''' <returns>True if names of all types are unique (in spicte of CLS)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Types"/> is null</exception>
        Private Function CheckTypeNames(ByVal Types As IEnumerable(Of Type), ByVal FullName As Boolean) As Boolean
            If Types Is Nothing Then Throw New ArgumentNullException("Types")
            Dim list As New List(Of String)
            Dim Violated As Boolean
            For Each Type In Types
                If Not GetItemClsCompliance(Type) Then Continue For
                If list.Contains(If(FullName, If(Type.FullName, Type.Name), Type.Name).ToLowerInvariant, StringComparer.InvariantCultureIgnoreCase) Then
                    Violated = True
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.UniqueName.f(If(FullName, If(Type.FullName, Type.Name), Type.Name)), CLSRule.UnicodeIdentifiers, Type)
                Else
                    list.Add(If(FullName, If(Type.FullName, Type.Name), Type.Name).ToLowerInvariant)
                End If
            Next
            Return Violated
        End Function

        ''' <summary>Check if type exposed by member has enough accessibility</summary>
        ''' <param name="Member">Member exposing <paramref name="Type"/></param>
        ''' <param name="Type">Type exposed by <paramref name="Member"/></param>
        ''' <returns>True if <paramref name="Type"/> has enough accessibility to be exposed by <paramref name="Member"/>; false otherwise</returns>
        ''' <remarks>Does not check exporure rules violations inside single assembly. Check array/pointer/reference/generic elements as well.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> or <paramref name="Type"/> is null</exception>
        Private Function CheckTypeAcessibility(ByVal Member As MemberInfo, ByVal Type As Type) As Boolean
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            If Type Is Nothing Then Throw New ArgumentNullException("Type")
            If Type.IsPointer OrElse Type.IsByRef OrElse Type.IsArray Then Type = Type.GetElementType
            Dim Violated As Boolean = False
            Dim TypePublicVisibility = Type.HowIsSeenBy(Nothing)
            Dim TypeContextVisibility = Type.HowIsSeenBy(Member.DeclaringType)
            Dim MemberPublicVisibility = Member.HowIsSeenBy(Nothing)
            'Check
            If TypePublicVisibility = Visibility.Assembly OrElse TypePublicVisibility = Visibility.FamANDAssem OrElse TypePublicVisibility = Visibility.Private Then
                Violated = True
                OnViolation(ResourcesT.CLSComplianceCheckerResources.ExposeFriend, CLSRule.Visibility, Member)
            ElseIf (TypePublicVisibility = Visibility.Family OrElse TypePublicVisibility = Visibility.FamORAssem) AndAlso MemberPublicVisibility = Visibility.Public Then
                Violated = True
                OnViolation(ResourcesT.CLSComplianceCheckerResources.ExposeNested.f(Member.Name, If(Type.FullName, Type.Name), If(Type.DeclaringType.FullName, Type.DeclaringType.Name)), CLSRule.Visibility, Member)
            End If
            'Check accessibility of generic parameters and constraints
            If Type.IsGenericType OrElse Type.IsGenericTypeDefinition Then
                Dim gPars As Type() = {}
                Try
                    gPars = Type.GetGenericArguments
                Catch ex As Exception
                    OnViolation(ResourcesT.CLSComplianceCheckerResources.e_GenericParametersInType.f(Type), CLSRule.Error, Member, ex)
                End Try
                For Each gPar In gPars
                    If gPar.IsGenericParameter AndAlso Member.DeclaringType.Equals(Type) Then 'Open generic type
                        For Each cons In gPar.GetGenericParameterConstraints
                            Violated = Violated Or Not CheckTypeAcessibility(Member, cons)
                        Next
                    ElseIf Not gPar.IsGenericParameter Then  'Constructed generic type
                        Violated = Violated Or Not CheckTypeAcessibility(Member, gPar)
                    End If
                Next
            End If
            Return Not Violated
        End Function
    End Class
End Namespace
#End If