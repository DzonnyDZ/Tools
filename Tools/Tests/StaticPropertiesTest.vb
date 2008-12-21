Imports Tools.ReflectionT, System.Linq
Imports System.Reflection

#If Config <= Nightly Then
Namespace TestsT
    ''' <summary>This class tests if static properties of given type or types in given assembly/module/namespace returns value without throwing an exception.</summary>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public Class StaticPropertiesTest
        ''' <summary>Target of test</summary>
        Private _Target As Object
        ''' <summary>CTor from type</summary>
        ''' <param name="Type">Type to be tested</param>
        Public Sub New(ByVal Type As Type)
            _Target = Type
        End Sub
        ''' <summary>CTor from assembly</summary>
        ''' <param name="Assembly">Assembly to test types from</param>
        Public Sub New(ByVal Assembly As Reflection.Assembly)
            _Target = Assembly
        End Sub
        ''' <summary>CTor from module</summary>
        ''' <param name="Module">Module to test types from</param>
        Public Sub New(ByVal [Module] As Reflection.Module)
            _Target = [Module]
        End Sub
        ''' <summary>CTor from namespace</summary>
        ''' <param name="Namespace">Namespace to test types from</param>
        Public Sub New(ByVal [Namespace] As NamespaceInfo)
            _Target = [Namespace]
        End Sub
        ''' <summary>Performs a test</summary>
        Public Sub RunTest()
            If TypeOf _Target Is Assembly Then
                TestAssembly(_Target)
            ElseIf TypeOf _Target Is [Module] Then
                TestModule(_Target)
            ElseIf TypeOf _Target Is NamespaceInfo Then
                TestNamespce(_Target)
            ElseIf TypeOf _Target Is Type Then
                TestType(_Target)
            End If
        End Sub
        ''' <summary>Tests modules from assembly</summary>
        ''' <param name="asm">Assembly to test</param>
        ''' <remarks>This implementation simply calls <see cref="TestModule"/> for each module in <paramref name="asm"/>.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="asm"/> is null</exception>
        Protected Overridable Sub TestAssembly(ByVal asm As Assembly)
            If asm Is Nothing Then Throw New ArgumentNullException("asm")
            For Each [mod] In asm.GetModules
                TestModule([mod])
            Next
        End Sub
        ''' <summary>Tests types from module</summary>
        ''' <param name="mod">Module to test types from</param>
        ''' <remarks>This implementation takes all typef from module, verifies if type should be tested using <see cref="ShouldTestType"/> and if so, calls <see cref="TestType"/>.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="mod"/> is null</exception>
        Protected Overridable Sub TestModule(ByVal [mod] As [Module])
            If [mod] Is Nothing Then Throw New ArgumentNullException("mod")
            For Each t In [mod].GetTypes
                If Not ShouldTestType(t) Then Continue For
                TestType(t)
            Next
        End Sub
        ''' <summary>Tests types from namespace</summary>
        ''' <param name="ns">Namespace to test types from</param>
        ''' <remarks>This implementation takes all typef from namespace, verifies if type should be tested using <see cref="ShouldTestType"/> and if so, calls <see cref="TestType"/>.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="ns"/> is null</exception>
        Protected Overridable Sub TestNamespce(ByVal ns As NamespaceInfo)
            If ns Is Nothing Then Throw New ArgumentNullException("ns")
            For Each t In ns.GetTypes(True)
                If Not ShouldTestType(t) Then Continue For
                TestType(t)
            Next
        End Sub
        ''' <summary>Determines if given type should be tested</summary>
        ''' <param name="t">Type to determiny if it should be tested</param>
        ''' <returns>True when type shoudl be tested; false when it shoudl not be tested; For <see cref="Type.IsGenericTypeDefinition">generit type definitions</see> returns always false.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="t"/> is null</exception>
        ''' <remarks>This implementation utlizes <see cref="TypeFlags"/></remarks>
        Protected Overridable Function ShouldTestType(ByVal t As Type) As Boolean
            If t Is Nothing Then Throw New ArgumentNullException("t")
            If t.IsGenericTypeDefinition Then Return False
            Dim ShouldTest As Boolean = False
            ShouldTest = (TypeFlags And TypeBindingAttributes.AllVisibilty) = TypeBindingAttributes.none _
                OrElse ((t.Attributes And TypeAttributes.Public) = TypeAttributes.Public AndAlso (TypeFlags And TypeBindingAttributes.Public)) _
                OrElse ((t.Attributes And TypeAttributes.NotPublic) = TypeAttributes.NotPublic AndAlso (TypeFlags And TypeBindingAttributes.Assembly)) _
                OrElse ((t.Attributes And TypeAttributes.NestedAssembly) = TypeAttributes.NestedAssembly AndAlso (TypeFlags And TypeBindingAttributes.NestedAssembly)) _
                OrElse ((t.Attributes And TypeAttributes.NestedFamANDAssem) = TypeAttributes.NestedFamANDAssem AndAlso (TypeFlags And TypeBindingAttributes.NestedFamilyAndAssembly)) _
                OrElse ((t.Attributes And TypeAttributes.NestedFamORAssem) = TypeAttributes.NestedFamORAssem AndAlso (TypeFlags And TypeBindingAttributes.NestedFamilyOrAssembly)) _
                OrElse ((t.Attributes And TypeAttributes.NestedFamily) = TypeAttributes.NestedFamily AndAlso (TypeFlags And TypeBindingAttributes.NestedFamily)) _
                OrElse ((t.Attributes And TypeAttributes.NestedPrivate) = TypeAttributes.NestedPrivate AndAlso (TypeFlags And TypeBindingAttributes.NestedPrivate)) _
                OrElse ((t.Attributes And TypeAttributes.NestedPublic) = TypeAttributes.NestedPublic AndAlso (TypeFlags And TypeBindingAttributes.NestedPublic))
            ShouldTest = ShouldTest AndAlso ((TypeFlags And TypeBindingAttributes.VirtualAll) = TypeBindingAttributes.none _
                OrElse ((t.Attributes And TypeAttributes.Abstract) = TypeAttributes.Abstract AndAlso (TypeFlags And TypeBindingAttributes.Abstract)) _
                OrElse ((t.Attributes And TypeAttributes.Abstract) = 0 AndAlso (TypeFlags And TypeBindingAttributes.NonAbstract)) _
                OrElse ((t.Attributes And TypeAttributes.Sealed) = 0 AndAlso (TypeFlags And TypeBindingAttributes.Sealed)))
            ShouldTest = ShouldTest AndAlso ((TypeFlags And TypeBindingAttributes.AllClassTypes) = TypeBindingAttributes.none _
                OrElse ((t.Attributes And TypeAttributes.Class) = TypeAttributes.Class AndAlso (TypeFlags And TypeBindingAttributes.Class)) _
                OrElse ((t.Attributes And TypeAttributes.Interface) = TypeAttributes.Interface AndAlso (TypeFlags And TypeBindingAttributes.Interface)) _
                OrElse (t.IsValueType AndAlso (TypeFlags And TypeBindingAttributes.Structure)))
            ShouldTest = ShouldTest AndAlso ((TypeFlags And TypeBindingAttributes.AllNameTypes) = TypeBindingAttributes.none _
                OrElse ((t.Attributes And TypeAttributes.SpecialName) = TypeAttributes.SpecialName AndAlso (TypeFlags And TypeBindingAttributes.SpecialName)) _
                OrElse ((t.Attributes And TypeAttributes.RTSpecialName) = TypeAttributes.RTSpecialName AndAlso (TypeFlags And TypeBindingAttributes.RTSpecialName)) _
                OrElse ((t.Attributes And (TypeAttributes.SpecialName Or TypeAttributes.RTSpecialName)) = 0 AndAlso (TypeFlags And TypeBindingAttributes.NoSpecialName)))
            Return ShouldTest
        End Function
        ''' <summary>Specified flags for filtering <see cref="Type"/> by its <see cref="Type.Attributes"/></summary>
        ''' <remarks>There are 4 major groups of flags masked by <see cref="TypeBindingAttributes.AllVisibilty"/>, <see cref="TypeBindingAttributes.VirtualAll"/>, <see cref="TypeBindingAttributes.AllClassTypes"/>, <see cref="TypeBindingAttributes.AllNameTypes"/>. Setting all flags in group to true has same effect as setting them to zero.</remarks>
        <Flags()> _
        Public Enum TypeBindingAttributes
            ''' <summary>Specifies no filter. If specific group (masked by <see cref="AllVisibilty"/>, <see cref="VirtualAll"/>, <see cref="AllClassTypes"/>, <see cref="AllNameTypes"/>)
            ''' AND-ed with actual value is <see cref="none"/> (zero), it is ignored when detrmining filter.</summary>
            none = 0
            ''' <summary>Not nested public types (<see cref="TypeAttributes.[Public]"/>)</summary>
            [Public] = 1
            ''' <summary>Not nested not public types (<see cref="TypeAttributes.NotPublic"/>)</summary>
            Assembly = 2
            ''' <summary>Nested public types (<see cref="TypeAttributes.NestedPublic"/>)</summary>
            [NestedPublic] = 4
            ''' <summary>Nested family (protected) types (<see cref="TypeAttributes.NestedFamily"/>)</summary>
            [NestedFamily] = 8
            ''' <summary>Nested assembly (friend) types (<see cref="TypeAttributes.NestedAssembly"/>)</summary>
            [NestedAssembly] = 16
            ''' <summary>Nested family-and-assembly types (<see cref="TypeAttributes.NestedFamANDAssem"/>)</summary>
            [NestedFamilyAndAssembly] = 32
            ''' <summary>Nested family-or-assembly (protected friend) types (<see cref="TypeAttributes.NestedFamORAssem"/>)</summary>
            [NestedFamilyOrAssembly] = 64
            ''' <summary>Nested private types (<see cref="TypeAttributes.NestedPrivate"/>)</summary>
            [NestedPrivate] = 128
            ''' <summary>Not nested types (or-combination of <see cref="[Public]"/> and <see cref="Assembly"/>)</summary>
            NotNested = [Public] Or Assembly
            ''' <summary>All nested types (or-combination of <see cref="NestedPublic"/>, <see cref="NestedFamily"/>, <see cref="NestedAssembly"/>, <see cref="NestedFamilyAndAssembly"/>, <see cref="NestedFamilyOrAssembly"/>, <see cref="NestedPrivate"/>)</summary>
            [Nested] = NestedPublic Or NestedFamily Or NestedAssembly Or NestedFamilyAndAssembly Or NestedFamilyOrAssembly Or NestedPrivate
            ''' <summary>All publicly visible types (or-combination of <see cref="[Public]"/> and <see cref="NestedPublic"/>)</summary>
            [AllPublic] = [Public] Or NestedPublic
            ''' <summary>All assembly(friend)-visible types (or-combination of <see cref="Assembly"/>, <see cref="NestedAssembly"/> and <see cref="NestedFamilyOrAssembly"/>)</summary>
            AllAssembly = Assembly Or NestedAssembly Or NestedFamilyOrAssembly
            ''' <summary>All nested falimy(protected)-visible types (or-combination of <see cref="NestedFamily"/> and <see cref="NestedFamilyOrAssembly"/>)</summary>
            NestedAndFamily = NestedFamily Or NestedFamilyOrAssembly
            ''' <summary>All nested asembly(friend)-visible types (or-combination of <see cref="NestedAssembly"/> and <see cref="NestedFamilyOrAssembly"/>)</summary>
            NestedAndAssembly = NestedAssembly Or NestedFamilyOrAssembly
            ''' <summary>All possible visibilities (visibility mask). Specifying this has the same effect as leaving visibility group at zero. (or-combination of <see cref="NotNested"/> and <see cref="Nested"/>)</summary>
            AllVisibilty = NotNested Or Nested

            ''' <summary>Abstract class (<see cref="TypeAttributes.Abstract"/>)</summary>
            Abstract = 256
            ''' <summary>Non-abstract class</summary>
            NonAbstract = 512
            ''' <summary>Sealed (NotInheritable in Visual Basic) class (<see cref="TypeAttributes.Sealed"/>)</summary>
            Sealed = 1024
            ''' <summary>All possible virtualization flags (virtual mask). Specifying this has the same effect as leaving virtual group at zero. (or-combination of <see cref="Abstract"/>, <see cref="NonAbstract"/> and <see cref="Virtual"/>)</summary>
            VirtualAll = Abstract Or NonAbstract Or Sealed

            ''' <summary>Class (reference type; <see cref="TypeAttributes.[Class]"/>)</summary>
            [Class] = 2048
            ''' <summary>Structure (value type; <see cref="Type.IsValueType"/>)</summary>
            [Structure] = 4096
            ''' <summary>Interface (<see cref="TypeAttributes.[Interface]"/>)</summary>
            [Interface] = 8192
            ''' <summary>All possible class types (class type mask). Specifying this has the same efect as leaving type group at zero. (or-combination of <see cref="[Class]"/>, <see cref="[Structure]"/>, <see cref="[Interface]"/>)</summary>
            AllClassTypes = [Class] Or [Structure] Or [Interface]

            ''' <summary>Type with special name (<see cref="TypeAttributes.SpecialName"/>)</summary>
            SpecialName = 16384
            ''' <summary>Type with runtime-special name (<see cref="TypeAttributes.RTSpecialName"/>)</summary>
            RTSpecialName = 32768
            ''' <summary>Type without special name</summary>
            NoSpecialName = 65536
            ''' <summary>All possible name types (name mask). Specifying this has the same effect as leaving name type group at zero. (or-combination of <see cref="SpecialName"/>, <see cref="RTSpecialName"/> and <see cref="NoSpecialName"/>)</summary>
            AllNameTypes = SpecialName Or RTSpecialName Or NoSpecialName
        End Enum
        ''' <summary>Contains value of the <see cref="TypeFlags"/> property</summary>
        Private _TypeFlags As TypeBindingAttributes = TypeBindingAttributes.AllPublic Or TypeBindingAttributes.NestedAndFamily
        ''' <summary>Gets or sets flags indicating which types hsould be tested.</summary>
        ''' <remarks>This property is ignored when this instance tests only single type.</remarks>
        Public Property TypeFlags() As TypeBindingAttributes
            Get
                Return _TypeFlags
            End Get
            Set(ByVal value As TypeBindingAttributes)
                _TypeFlags = value
            End Set
        End Property
        ''' <summary>Conzains value of the <see cref="PropertyBindingFlags"/> property</summary>
        Private _PropertyBindingFlags As BindingFlags = BindingFlags.Public
        ''' <summary>Gets or sets flags indicating which properties should be tested</summary>
        Public Property PropertyBindingFlags() As BindingFlags
            Get
                Return _PropertyBindingFlags
            End Get
            Set(ByVal value As BindingFlags)
                _PropertyBindingFlags = value
            End Set
        End Property
        ''' <summary>Tests all static properties of given type</summary>
        ''' <param name="t">Type to test static properties from</param>
        ''' <exception cref="ArgumentNullException"><paramref name="t"/> is null</exception>
        ''' <remarks>This implementation gets properties of type utilizing <see cref="PropertyBindingFlags"/> and passes then to <see cref="TestProperty"/>.</remarks>
        Protected Overridable Sub TestType(ByVal t As Type)
            If t Is Nothing Then Throw New ArgumentNullException("t")
            For Each prp In t.GetProperties(PropertyBindingFlags Or BindingFlags.Static)
                TestProperty(prp)
            Next
        End Sub
        ''' <summary>Test single property</summary>
        ''' <param name="prp">Property to be tested</param>
        ''' <exception cref="ArgumentNullException"><paramref name="prp"/> is null</exception>
        Protected Overridable Sub TestProperty(ByVal prp As PropertyInfo)
            If prp Is Nothing Then Throw New ArgumentNullException("prp")
            Dim getter As MethodInfo
            Try
                getter = prp.GetGetMethod(PropertyBindingFlags And BindingFlags.NonPublic)
            Catch ex As Exception
                OnError(prp, TestStages.GetGetMethod, ex)
                Exit Sub
            End Try
            If getter Is Nothing Then
                OnError(prp, TestStages.HasGetMethod)
                Exit Sub
            End If
            If getter.GetParameters.Length <> 0 Then
                OnError(prp, TestStages.IsIndexed)
                Exit Sub
            End If
            Dim value As Object
            Try
                value = getter.Invoke(Nothing, Nothing)
            Catch ex As TargetInvocationException
                OnError(prp, TestStages.GetterBeingInvoked, ex.InnerException)
                Exit Sub
            Catch ex As Exception
                OnError(prp, TestStages.InvokeGetter, ex)
                Exit Sub
            End Try
            If value Is Nothing Then
                OnError(prp, TestStages.ValueIsNull)
                Exit Sub
            End If
            OnSuccess(New TestSuccessEventArgs(prp, value))
        End Sub
        ''' <summary>Raises the <see cref="[Error]"/> event</summary>
        ''' <param name="Property">Property being tested</param>
        ''' <param name="Stage">Stage of testing</param>
        ''' <param name="ex">Exception thrown</param>
        Private Sub OnError(ByVal [Property] As PropertyInfo, ByVal Stage As TestStages, Optional ByVal ex As Exception = Nothing)
            OnError(New TestErrorEventArgs([Property], Stage, ex))
        End Sub
        ''' <summary>Contains value of the <see cref="SuccessCount"/> property</summary>
        Private _SuccessCount%
        ''' <summary>Count of errors divided by stages</summary>
        Private _ErrorCounts As New Dictionary(Of TestStages, Integer)
        ''' <summary>Gets count of successfully tested properties</summary>
        ''' <returns>Count of successfully tested properties</returns>
        Public ReadOnly Property SuccessCount%()
            Get
                Return _SuccessCount
            End Get
        End Property
        ''' <summary>Gets count of properties tested with error/warning</summary>
        ''' <returns>Count of properties tested with error/warning</returns>
        Public ReadOnly Property ErrorsCount%()
            Get
                Return (From ec In _ErrorCounts Select ec.Value).Sum
            End Get
        End Property
        ''' <summary>Gets count of tested properties</summary>
        ''' <returns>Count of tested properties</returns>
        Public ReadOnly Property PropertiesTestedCount%()
            Get
                Return SuccessCount + ErrorsCount
            End Get
        End Property
        ''' <summary>Gets count of errors at <see cref="TestStages.GetterBeingInvoked"/> stage</summary>
        ''' <returns>Count of errors at <see cref="TestStages.GetterBeingInvoked"/> stage</returns>
        Public ReadOnly Property PropertyErrorsCount%()
            Get
                Return ErrorsCount(TestStages.GetterBeingInvoked)
            End Get
        End Property
        ''' <summary>Gets count of errors by stage</summary>
        ''' <returns>Count of errors in stage <paramref name="Stage"/></returns>
        ''' <param name="Stage">Stage to get count of errors in</param>
        Public ReadOnly Property ErrorsCount(ByVal Stage As TestStages) As Integer
            Get
                If _ErrorCounts.ContainsKey(Stage) Then Return _ErrorCounts(Stage) Else Return 0
            End Get
        End Property

        ''' <summary>Raised when error occurs</summary>
        ''' <remarks>Some only <see cref="TestStages.GetterBeingInvoked"/> is really serious error. Other errors are rather warnings.</remarks>
        Public Event [Error] As EventHandler(Of TestErrorEventArgs)
        ''' <summary>Raised when no error (warning) occurs during test of single property</summary>
        Public Event Success As EventHandler(Of TestSuccessEventArgs)
        ''' <summary>Raises the <see cref="[Error]"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnError(ByVal e As TestErrorEventArgs)
            If Not _ErrorCounts.ContainsKey(e.Stage) Then _ErrorCounts.Add(e.Stage, 0)
            _ErrorCounts(e.Stage) += 1
            RaiseEvent [Error](Me, e)
        End Sub
        ''' <summary>Raises the <see cref="Success"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnSuccess(ByVal e As TestSuccessEventArgs)
            _SuccessCount += 1
            RaiseEvent Success(Me, e)
        End Sub
        ''' <summary>Event arguments of the <see cref="[Error]"/> event</summary>
        Public Class TestErrorEventArgs : Inherits EventArgs
            ''' <summary>Exception being thrown. Can be null. Only <see cref="TestStages.GetGetMethod"/>, <see cref="TestStages.InvokeGetter"/> and <see cref="TestStages.GetterBeingInvoked"/> carrys error.</summary>
            Public ReadOnly Exception As Exception
            ''' <summary>Stage of testing</summary>
            Public ReadOnly Stage As TestStages
            ''' <summary>Property being tested</summary>
            Public ReadOnly [Property] As PropertyInfo
            ''' <summary>CTor</summary>
            ''' <param name="Property">Property being tested</param>
            ''' <param name="Stage">Stage of testing</param>
            ''' <param name="ex">Exception that caused this error,if any</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Property"/> is null</exception>
            Public Sub New(ByVal [Property] As PropertyInfo, ByVal Stage As TestStages, ByVal ex As Exception)
                If [Property] Is Nothing Then Throw New ArgumentException("Property")
                Me.Property = [Property]
                Me.Stage = Stage
                Me.Exception = ex
            End Sub
        End Class
        ''' <summary>Event arguments of the <see cref="Success"/> event</summary>
        Public Class TestSuccessEventArgs : Inherits EventArgs
            ''' <summary>Property bing tested</summary>
            Public ReadOnly [Property] As PropertyInfo
            ''' <summary>Value got from getter</summary>
            Public ReadOnly Value As Object
            ''' <summary>CTor</summary>
            ''' <param name="Property">Propertybeing tested</param>
            ''' <param name="Value">Value got from getter</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Property"/> is null</exception>
            Public Sub New(ByVal [Property] As PropertyInfo, ByVal Value As Object)
                If [Property] Is Nothing Then Throw New ArgumentException("Property")
                Me.Property = [Property]
                Me.Value = Value
            End Sub
        End Class
        ''' <summary>Identifies statges and error reasons of testing</summary>
        Public Enum TestStages
            ''' <summary>Attempt to get property getter failed because of attempt to get non-public getter was unsuccessfull due to <see cref="Security.SecurityException"/> meaning that caller doe not have right to reflect over non-public method. This does not mean that property is buggy; this means that test cannot be performed.</summary>
            GetGetMethod
            ''' <summary>Property begin tested has no getter method or getter method is not public and <see cref="PropertyBindingFlags"/> does not include <see cref="BindingFlags.NonPublic"/>. This is rather warning then error. No exception is carried.</summary>
            HasGetMethod
            ''' <summary>Property getter method has peremeter(s). This is rather warning tha error. Indexted properties canot be tested.</summary>
            IsIndexed
            ''' <summary>Attempt to invoke getter failed fro technical reasons. This does not indicate bug in getter. For more reasons see <see cref="MethodInfo.Invoke"/>.</summary>
            InvokeGetter
            ''' <summary>There was an error during getter execution. <see cref="MethodInfo.Invoke"/> has thrown an <see cref="TargetInvocationException"/>. This may indicate bug in property being tested.</summary>
            GetterBeingInvoked
            ''' <summary>Value returned from property is null. This is rather warning than error.</summary>
            ValueIsNull
        End Enum
    End Class
End Namespace
#End If