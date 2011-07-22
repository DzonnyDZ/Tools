Imports Tools.RuntimeT.CompilerServicesT

Namespace RuntimeT.CompilerServicesT

    ''' <summary>When applied on a method, property or event indicates that postprocessing tool should indicate that the member overrides base class member or implements interface member</summary>
    ''' <remarks>Applying this attribute on a member causes nothing on itself. You must run supporting post-processsing tool (such as AssemblyPostprocessoer) on your assembly once it's compiled to apply changes denoted by this attributes.</remarks>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <Postprocessor("Tools.RuntimeT.CompilerServicesT.Postprocessors,AssemblyPostprocessor", "Implements")>
    <AttributeUsage(AttributeTargets.Method Or AttributeTargets.Event Or AttributeTargets.Property, AllowMultiple:=True, Inherited:=False)>
    Public Class ImplementsAttribute
        Inherits PostprocessingAttribute

        Private ReadOnly _base As Type
        Private ReadOnly _member As String

        ''' <summary>CTor - creates a new instance of the <see cref="ImplementsAttribute"/> class, specifies base class and implemented member name</summary>
        ''' <param name="base">Base type to override member of</param>
        ''' <param name="member">Name of the member to override. The member must have same signature as member this attribute is applied on.
        ''' Can be null if name of member this attribute is applied on is same as name of interface member.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="base"/> is null</exception>
        Public Sub New(base As Type, member As String)
            If base Is Nothing Then Throw New ArgumentNullException("base")
            _base = base
            _member = member
        End Sub

        ''' <summary>Crteates a new instance of the <see cref="ImplementsAttribute"/> class without specifying implemented member name</summary>
        ''' <param name="base">Base type to override member of</param>
        ''' <exception cref="ArgumentNullException"><paramref name="base"/> is null</exception>
        ''' <remarks>Use this CTor only if name of member this attribute is applied on is same as name of member being overriden/implemented.</remarks>
        Public Sub New(base As Type)
            Me.New(base, Nothing)
        End Sub

        ''' <summary>Gets base class or interface this instance indicates override or implementation of member of</summary>
        Public ReadOnly Property Base() As Type
            Get
                Return _base
            End Get
        End Property

        ''' <summary>Gets name of member to override / implement</summary>
        ''' <returns>Name of member to implement/override. Null if name of member to implement/override is same as name of member this attribute is applied on.</returns>
        ''' <remarks>When lookup is done in <see cref="Base"/> only members of same type and with same signature as member this attribute is applied on are considered.</remarks>
        Public ReadOnly Property Member() As String
            Get
                Return _member
            End Get
        End Property
    End Class
End Namespace