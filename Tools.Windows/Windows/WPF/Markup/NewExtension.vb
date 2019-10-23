Imports System.Windows.Markup


Namespace WindowsT.WPF.MarkupT
    ''' <summary>A markup extension that can create instance of any type which has default (parameter-less constructor)</summary>
    ''' <seelaso cref="Activator.CreateInstance"/>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    <MarkupExtensionReturnType(GetType(Object)), ContentProperty("Type")>
    Public Class NewExtension
        Inherits MarkupExtensionBase
        ''' <summary>CTor - creates an enw instance of the <see cref="NewExtension"/> class</summary>
        Public Sub New()
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="NewExtension"/> class and initializes the <see cref="Type"/> property.</summary>
        ''' <param name="type">Type to create instance of</param>
        Public Sub New(ByVal type As Type)
            Me.Type = type
        End Sub

        ''' <summary>Returns an object that is set as the value of the target property for this markup extension. </summary>
        ''' <returns>The object value to set on the property where the extension is applied. Returns new instance of type <see cref="Type"/>. Null if <see cref="Type"/> is null.</returns>
        ''' <param name="serviceProvider">ignored</param>
        ''' <exception cref="System.ArgumentException"><see cref="Type"/> is not a <see cref="RuntimeType"/>. -or- <see cref="Type"/> is an open generic type (that is, the <see cref="System.Type.ContainsGenericParameters" /> property returns true).</exception>
        ''' <exception cref="System.NotSupportedException"><see cref="Type"/> cannot be a <see cref="System.Reflection.Emit.TypeBuilder" />. -or- Creation of <see cref="System.TypedReference" />, <see cref="System.ArgIterator" />, <see cref="System.Void" />, and <see cref="System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported. -or- The assembly that contains type is a dynamic assembly that was created with <see cref="System.Reflection.Emit.AssemblyBuilderAccess.Save" />.</exception>
        ''' <exception cref="System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
        ''' <exception cref="System.MemberAccessException">Cannot create an instance of an abstract class.</exception>
        ''' <exception cref="System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through Overload:<see cref="System.Type.GetTypeFromProgID" /> or Overload:<see cref="System.Type.GetTypeFromCLSID" />.</exception>
        ''' <exception cref="System.MissingMethodException">No matching public constructor was found.</exception>
        ''' <exception cref="System.Runtime.InteropServices.COMException"><see cref="Type"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
        ''' <exception cref="System.TypeLoadException"><see cref="Type"/> is not a valid type.</exception>
        ''' <seelaso cref="Activator.CreateInstance"/>
        Protected Overloads Overrides Function ProvideValue(ByVal serviceProvider As XamlServiceProvider) As Object
            If Type Is Nothing Then Return Nothing
            Return Activator.CreateInstance(Type)
        End Function

        ''' <summary>Gets or sets type to create instance of</summary>
        ''' <remarks>The type must be class or structure with public default (parameter-less constructor). It shall be neither abstract nor interface, nor open generic type.</remarks>
        <ConstructorArgument("type")>
        Public Property Type As Type
    End Class
End Namespace