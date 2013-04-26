Imports Tools.RuntimeT.CompilerServicesT

Namespace RuntimeT.CompilerServicesT

    ''' <summary>
    ''' When applied on a method, property or event indicates that postprocessing tool should indicate that the member overrides base class member or implements interface member.
    ''' When applied on a type indicates that postprocessing tool should add base class or interface to the type.
    ''' </summary>
    ''' <remarks>Applying this attribute on a member or type causes nothing on itself. You must run supporting post-processsing tool (such as AssemblyPostprocessoer) on your assembly once it's compiled to apply changes denoted by this attributes.</remarks>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <Postprocessor("Tools.RuntimeT.CompilerServicesT.Postprocessors,AssemblyPostprocessor", "Implements")>
    <AttributeUsage(AttributeTargets.Method Or AttributeTargets.Event Or AttributeTargets.Property Or AttributeTargets.Class Or AttributeTargets.Delegate Or AttributeTargets.Enum Or AttributeTargets.Interface Or AttributeTargets.Struct, AllowMultiple:=True, Inherited:=False)>
    Public Class ImplementsAttribute
        Inherits PostprocessingAttribute

        Private ReadOnly _base As Type
        Private ReadOnly _member As String

        ''' <summary>CTor - creates a new instance of the <see cref="ImplementsAttribute"/> class, specifies base class and implemented member name</summary>
        ''' <param name="base">Base type to override member of</param>
        ''' <param name="member">Name of the member to override. The member must have same signature as member this attribute is applied on.
        ''' Can be null if name of member this attribute is applied on is same as name of interface member.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="base"/> is null</exception>
        ''' <remarks>Don't use this constructor when the attribute is applied on a type.</remarks>
        Public Sub New(base As Type, member As String)
            If base Is Nothing Then Throw New ArgumentNullException("base")
            _base = base
            _member = member
        End Sub

        ''' <summary>Crteates a new instance of the <see cref="ImplementsAttribute"/> class without specifying implemented member name</summary>
        ''' <param name="base">
        ''' Base type to override member of (when applied on a member) or a type to add to types current type implements/inherits from (when applied on a type)
        ''' <note>
        ''' When this attribute is applied on a type and <paramref name="base"/> is not interface current base type of the type is replaced by a tool.
        ''' <para>When this attribute is applied on a type and <paramref name="base"/> is an open generic type and the type attributte is applied on is open or closed generic type with same number of generic parameters as <paramref name="base"/> typeparameters from the type this attribute is applied on are supplied to <paramref name="base"/>.</para>
        ''' </note>
        ''' </param>
        ''' <exception cref="ArgumentNullException"><paramref name="base"/> is null</exception>
        ''' <remarks>Use this CTor only if name of member this attribute is applied on is same as name of member being overriden/implemented or use this CTor whan the attribute is applied on a type.</remarks>
        Public Sub New(base As Type)
            Me.New(base, Nothing)
        End Sub

        ''' <summary>
        ''' Gets base class or interface this instance indicates override or implementation of member of (when applied on a member)
        ''' or type to add to lostz of types current type implements/inherits from (when applied on a type).
        ''' </summary>
        ''' <remarks>When this attribute is applied on a type and <see cref="Base"/> is not an interface current base type of the type is replaced by a tool.</remarks>
        Public ReadOnly Property Base() As Type
            Get
                Return _base
            End Get
        End Property

        ''' <summary>Gets name of member to override / implement</summary>
        ''' <returns>Name of member to implement/override. Null if name of member to implement/override is same as name of member this attribute is applied on.</returns>
        ''' <remarks>
        ''' When lookup is done in <see cref="Base"/> only members of same type and with same signature as member this attribute is applied on are considered.
        ''' <para>Not used when thei attribute is applied on a type</para>
        ''' </remarks>
        Public ReadOnly Property Member() As String
            Get
                Return _member
            End Get
        End Property

        ''' <summary>Gets or sets accessor to be implemented.</summary>
        ''' <remarks>
        ''' If <see cref="Acceessor"/> is not <see cref="Accessor.All"/> <see cref="Member"/> should point to a property or an event (depends on <see cref="Accessor"/> value) and this attribute should be applied on a method.
        ''' <para>Not used when this attribute is used on a type.</para>
        ''' </remarks>
        Public Property Acceessor As Accessor

        ''' <summary>Gets or sets generic parameters mapping for <see cref="Base"/> substituting generic parameters from declaring type</summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>
        ''' If <see cref="Base"/> is an open generic type and type this attribute is applied to is also open generic type, you can use this array to pass generic parameters of target type to <see cref="Base"/>.
        ''' Every item in this array passes one generic argument to <see cref="Base"/>. Generic arguments are refferenced by position.
        ''' In case <see cref="Base"/> is nested type parent type arguments are before nested type arguments.
        ''' When an item is null value at corresponding index in <see cref="GenericParameterSubstitutionWithTypes"/> is used.
        ''' Values of item arrays are names of generic arguments of type this attribute is applied on (or parent type of member thia attribute is applied on).
        ''' In case this attribute is applied on nested type generic parameters of parent type can be also referenced if they have different names then generic arguments of nested type.
        ''' </remarks>
        Public Property GenericParameterSubstitutionWithGenericParameters As String()

        ''' <summary>Gets or sets generic parameters mapping for <see cref="Base"/> substituting types</summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>
        ''' If <see cref="Base"/> is an open generic type you can pass generic parameters to it using this property.
        ''' You don't have to use this property when you don't use <see cref="GenericParameterSubstitutionWithGenericParameters"/>.
        ''' This property is mainly intended to be used in combination with <see cref="GenericParameterSubstitutionWithGenericParameters"/>.
        ''' Every item in this array passes one generic argument to <see cref="Base"/>. Generic arguments are refferenced by position.
        ''' In case <see cref="Base"/> is nested type parent type arguments are before nested type arguments.
        ''' When an item is null value at corresponding index in <see cref="GenericParameterSubstitutionWithGenericParameters"/> is used.
        ''' </remarks>
        Public Property GenericParameterSubstitutionWithTypes As Type()
    End Class

    ''' <summary>Enumeration fo comnbined members accessors</summary>
    ''' <version version="1.5.4">The enumeration is new in version 1.5.4</version>
    Public Enum Accessor
        ''' <summary>Consider all implemented accessors</summary>
        All
        ''' <summary>A method is impelmenting property Get accessor</summary>
        [Get]
        ''' <summary>A method is implementing property Set accessor</summary>
        [Set]
        ''' <summary>A method is implementing event Add accessor</summary>
        Add
        ''' <summary>A method is implementing event Remove accessor</summary>
        Remove
        ''' <summary>A method is implemnenting event Raise accessor</summary>
        Raise
        ''' <summary>An event or property is implementing only standard (Get/Set or Add/Remove/Raise) accessors. Ignores other accessors.</summary>
        Standard
    End Enum
End Namespace