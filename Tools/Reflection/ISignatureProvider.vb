Imports System.Reflection, System.Runtime.InteropServices
Imports System.Runtime.CompilerServices, System.Linq, Tools.LinqT

#If Config <= Nightly Then 'Stage:Nightly
'ASAP
Namespace ReflectionT
    ''' <summary>Provides interface of object that provides string representation of various reflection object</summary>
    Public Interface ISignatureProvider
        ''' <summary>Gets name of current provider</summary>
        ReadOnly Property Name$()
        ''' <summary>Gets string representation of an assembly name</summary>
        ''' <param name="Assembly">Assembly to represent</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of <paramref name="Assembly"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Assembly"/> is null</exception>
        Function GetSignature(ByVal Assembly As AssemblyName, Optional ByVal Flags As SignatureFlags = SignatureFlags.ShortNameOnly) As String
        ''' <summary>Gets string representation of an assembly</summary>
        ''' <param name="Assembly">Assembly to represent</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of <paramref name="Assembly"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Assembly"/> is null</exception>
        Function GetSignature(ByVal Assembly As Assembly, Optional ByVal Flags As SignatureFlags = SignatureFlags.ShortNameOnly) As String
        ''' <summary>gets representation of a module</summary>
        ''' <param name="Module">Module to represent</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of <paramref name="Module"/></returns>
        ''' ''' <exception cref="ArgumentNullException"><paramref name="Module"/> is null</exception>
        Function GetSignature(ByVal [Module] As [Module], Optional ByVal Flags As SignatureFlags = SignatureFlags.ShortNameOnly) As String
        ''' <summary>Gets string representation of a namespace</summary>
        ''' <param name="Namespace">Namespace to represent</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of <paramref name="Namespace"/></returns>
        ''' ''' <exception cref="ArgumentNullException"><paramref name="Namespace"/> is null</exception>
        Function GetSignature(ByVal [Namespace] As NamespaceInfo, Optional ByVal Flags As SignatureFlags = SignatureFlags.ObjectType Or SignatureFlags.LongName) As String
        ''' <summary>Gets string representation of a member</summary>
        ''' <param name="Member">Member to represent</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of <paramref name="Member"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        Function GetSignature(ByVal Member As MemberInfo, ByVal Flags As SignatureFlags) As String
        ''' <summary>Gets string representation of attached custom attribute</summary>
        ''' <param name="AttributeData"><see cref="CustomAttributeData"/> to show</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of given custom attribute</returns>
        Function GetAttribute(ByVal AttributeData As CustomAttributeData, ByVal Flags As SignatureFlags) As String
        ''' <summary>Gets string representation of attached custom attributes</summary>
        ''' <param name="AttributeData">Collection of <see cref="CustomAttributeData"/> to show</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of given custom attributes</returns>
        Function GetAttributes(ByVal AttributeData As IEnumerable(Of CustomAttributeData), ByVal Flags As SignatureFlags) As String
    End Interface
    ''' <summary>Instructions for signature provider to which parts of signature to include in resulting string</summary>
    ''' <remarks>Signature provider may chose to not generate some parts even when asked to and generate some parts even when not asked to</remarks>
    <Flags()> Public Enum SignatureFlags
        ''' <summary>Only short name included. This has value 0.</summary>
        ShortNameOnly = 0
        ''' <summary>Include access modifiers shuch ase Private, Public, etc.</summary>
        AccessModifiers = 1
        ''' <summary>Include other modifiers such as Static, Overrides, etc.</summary>
        OtherModifiers = AccessModifiers << 1
        ''' <summary>Include object type such as Class, Function, etc.</summary>
        ObjectType = OtherModifiers << 1
        ''' <summary>Use fullly qualified name (otherwise short name is used)</summary>
        LongName = ObjectType << 1
        ''' <summary>Include generic parameter names</summary>
        GenericParameters = LongName << 1
        ''' <summary>Include generic parameter details (constraints). Works only when <see cref="GenericParameters"/> is set.</summary>
        GenericParametersDetails = GenericParameters << 1
        ''' <summary>Include generic parameters with detauils (constraints). This is combination of <see cref="Genericparameters"/> and <see cref="GenericParametersDetails"/></summary>
        FullGenericparameters = GenericParameters Or GenericParametersDetails
        ''' <summary>Include method signature (types of arguments only)</summary>
        Signature = GenericParametersDetails << 1
        ''' <summary>Include names and other details in signarure. valid only if <see cref="Signature"/> is set</summary>
        SignatureDetails = Signature << 1
        ''' <summary>Include full signature with all details. Tis is combination of <see cref="Signature"/> and <see cref="SignatureDetails"/>.</summary>
        FullSignature = Signature Or SignatureDetails
        ''' <summary>Include (return) type. (Also type of enumeration or delegate type of event.)</summary>
        Type = SignatureDetails << 1
        ''' <summary>Include inheritance and implementatipon info</summary>
        Inheritance = Type << 1
        ''' <summary>Include some attribute commonly used by the provider</summary>
        SomeAttributes = Inheritance << 1
        ''' <summary>Incklude all attributes. When set <see cref="SomeAttributes"/> has no effect.</summary>
        ''' <remarks>This is advanced and provider may not support it and ignore.
        ''' If provider does not suppiort this but support <see cref="SomeAttributes"/> it should use it instead.</remarks>
        AllAttributes = SomeAttributes << 1
        ''' <summary>Short but meaningful signature. This is combination of <see cref="Genericparameters"/>, <see cref="Signature"/>, <see cref="NoEmptyBraces"/>, <see cref="AllShortNames"/> and (in fact or) <see cref="NoMultiline"/>.</summary>
        [Short] = GenericParameters Or Signature Or NoEmptyBraces Or AllShortNames Or NoMultiline
        ''' <summary>Detailed signature. Only flags not set are <see cref="LongName"/>, <see cref="AllAttributes"/>, <see cref="NoEmptyBraces"/>, <see cref="AllShortNames"/> and <see cref="NoMultiline"/></summary>
        Detailed = AccessModifiers Or OtherModifiers Or ObjectType Or FullGenericparameters _
            Or FullSignature Or Type Or Inheritance Or SomeAttributes
        ''' <summary>Full details. This is combination of <see cref="Detailed"/> and <see cref="AllAttributes"/> with <see cref="SomeAttributes"/> not set</summary>
        Full = (Detailed Or AllAttributes) And Not SomeAttributes
        ''' <summary>Instructs provider tom emmit possible empty braces in signature (e.g. for method with no parameters)</summary>
        NoEmptyBraces = AllAttributes << 1
        ''' <summary>Instructs provider to render only short names of types in signatures (for generic argumens and method parameters, omits namespace part of type name)</summary>
        AllShortNames = NoEmptyBraces << 1
        ''' <summary>Instructs provider not to create multiline code</summary>
        NoMultiline = AllShortNames << 1
        ''' <summary>Instructs provider to create only such fragments that are supported by provider's language.</summary>
        ''' <remarks> This does not necesarily mean that provider will provide code that is valid in target language (i.e. if it is also instructed to ommit element type definition (<see cref="ObjectType"/> is not set) or omit empty braces (<see cref="NoEmptyBraces"/>).
        ''' This mean that unsupported constructs will not be provided - it will be writtent in other way. I.e. VB does not support overloading of <c>AndAlso</c> (&amp;&amp; in C#) operator, so in strict mode the provider will return <c>Public Shared Function op_LogicalAnd</c> instead of <c>Public Shared Operator AndAlso</c>.</remarks>
        Strict = NoMultiline << 1
    End Enum
End Namespace
#End If