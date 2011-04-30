Imports System.Reflection
Imports System.Linq

Namespace RuntimeT.CompilerServicesT
    ''' <summary>
    ''' Apply this attribute on <see cref="Attribute"/>-derived class to indicate how to perform post-processing the attribute class defines.
    ''' You tipically do not use this attrubute unless you are defining your own post-processor.
    ''' </summary>
    ''' <remarks>It's recomended that your post-processing class derives from <see cref="PostprocessingAttribute"/>.</remarks>
    ''' <seelaso cref="T:Tools.RuntimeT.CompilerServicesT.PostprocessorWithContext`1"/>
    ''' <seelaso cref="T:Tools.RuntimeT.CompilerServicesT.PostprocessorWithoutContext`1"/>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <AttributeUsage(AttributeTargets.Class, allowmultiple:=False, inherited:=False)>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public NotInheritable Class PostprocessorAttribute
        Inherits Attribute
        Private _type As Type
        Private _typeName As String
        Private _method As String

        ''' <summary>CTor - creates a new instance of the <see cref="PostprocessorAttribute"/> from type and method name</summary>
        ''' <param name="type">Type method to postprocess this attribute is defined in</param>
        ''' <param name="method">
        ''' Name of postprocessing method. The method must be member of type <paramref name="type"/>, it must be public, static and it must have 2 or 3 parameters.
        ''' 1st one must be of type <see cref="T:Mono.Cecil.ICustomAttributeProvider"/>.
        ''' 2nd one should accept the type this attribute as applied on.
        ''' 3rd (if present) must <see cref="Type.IsAssignableFrom">be asssignable from</see> <see cref="IPostprocessorContext"/>.
        ''' If these conditions are not satisfied postprocessing will fail.
        ''' </param>
        ''' <exception cref="ArgumentNullException">Any argument is null</exception>
        ''' <seelaso cref="T:Tools.RuntimeT.CompilerServicesT.PostprocessorWithContext`1"/>
        ''' <seelaso cref="T:Tools.RuntimeT.CompilerServicesT.PostprocessorWithoutContext`1"/>
        Public Sub New(type As Type, method As String)
            If type Is Nothing Then Throw New ArgumentNullException("type")
            If method Is Nothing Then Throw New ArgumentNullException("method")
            _type = type
            _method = method
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="PostprocessorAttribute"/> from type name and method name</summary>
        ''' <param name="type">Name of type method to postprocess this attribute is defined in. You should use name of type in format suitable for <see cref="Type.AssemblyQualifiedName"/> and <see cref="System.Type.[GetType]"/>.</param>
        ''' <param name="method">
        ''' Name of postprocessing method. The method must be member of type <paramref name="type"/>, it must be public, static and it must have 2 or 3 parameters.
        ''' 1st one must be of type <see cref="T:Mono.Cecil.ICustomAttributeProvider"/>.
        ''' 2nd one should accept the type this attribute as applied on.
        ''' 3rd (if present) must <see cref="Type.IsAssignableFrom">be asssignable from</see> <see cref="IPostprocessorContext"/>.
        ''' If these conditions are not satisfied postprocessing will fail.
        ''' </param>
        ''' <exception cref="ArgumentNullException">Any argument is null</exception>
        ''' <seelaso cref="T:Tools.RuntimeT.CompilerServicesT.PostprocessorWithContext`1"/>
        ''' <seelaso cref="T:Tools.RuntimeT.CompilerServicesT.PostprocessorWithoutContext`1"/>
        Public Sub New(type As String, method As String)
            If type Is Nothing Then Throw New ArgumentNullException("type")
            If method Is Nothing Then Throw New ArgumentNullException("method")
            _typeName = type
            _method = method
        End Sub

        ''' <summary>Gets name of postprocessing method</summary>
        Public ReadOnly Property Method As String
            Get
                Return _method
            End Get
        End Property

        ''' <summary>Gets type postprocessing method is defined in</summary>
        ''' <returns>If <see cref="System.Type"/> was passed to the constructor returns that type. Otherwise returns type obtained using <see cref="System.Type.[GetType]"/> from <see cref="TypeName"/> (which may return nulll if the type cannot be found).</returns>
        Public ReadOnly Property Type As Type
            Get
                If _type Is Nothing Then Return Type.GetType(TypeName)
                Return _type
            End Get
        End Property
        ''' <summary>Gets name of type postpúrocessing method is defined in</summary>
        ''' <returns>If type name was passed to a constructor returns that name. Otherwise returns <see cref="Type"/>.<see cref="Type.AssemblyQualifiedName">AssemblyQualifiedName</see></returns>
        Public ReadOnly Property TypeName As String
            Get
                If _type IsNot Nothing Then Return Type.AssemblyQualifiedName
                Return _typeName
            End Get
        End Property
        ''' <summary>Gets a method to be used for postprocessing</summary>
        ''' <returns>
        ''' A method of type <see cref="Type"/> with name <see cref="Method"/>, which is public and static and has 2 or 3 arguments.
        ''' 1st argument must be <see cref="T:Mono.Cecil.ICustomAttributeProvider"/>. 2nd one is not checked.
        ''' 3rd (if present) must <see cref="Type.IsAssignableFrom">be asssignable from</see> <see cref="IPostprocessorContext"/>.
        ''' Returns null if <see cref="Type"/> is null or if such method cannot be found.
        ''' </returns>
        ''' <exception cref="InvalidOperationException">More than one suitable methods found.</exception>
        ''' <seelaso cref="T:Tools.RuntimeT.CompilerServicesT.PostprocessorWithContext`1"/>
        ''' <seelaso cref="T:Tools.RuntimeT.CompilerServicesT.PostprocessorWithoutContext`1"/>
        Public Function GetMethod() As MethodInfo
            If Type Is Nothing Then Return Nothing
            Return (From m As MethodInfo In Type.GetMethods(BindingFlags.Public Or BindingFlags.Static)
                      Where m.Name = Method
                      Let args = m.GetParameters()
                      Where (args.Length = 2 OrElse args.Length = 3) AndAlso args(0).ParameterType.FullName = "Mono.Cecil.ICustomAttributeProvider" _
                        AndAlso (args.Length = 2 OrElse args(2).ParameterType.IsAssignableFrom(GetType(IPostprocessorContext)))
                      Select m
                   ).SingleOrDefault
        End Function
    End Class
End Namespace
