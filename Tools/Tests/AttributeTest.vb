Imports System.Reflection
Imports Tools.CollectionsT.GenericT
Imports System.ComponentModel, Tools

'TODO: Rework using Mono.Cecil. System.Reflection is not reliable

Namespace TestsT
    ''' <summary>Performs tests of custom attributes</summary>
    Public Class AttributeTest
#Region "General properties"
        ''' <summary>Contains value of the <see cref="TestedObjects"/> property</summary>
        Private ReadOnly _TestedObjects As New List(Of ICustomAttributeProvider)
        ''' <summary>Contains value of the <see cref="Errors"/> properties</summary>
        Private ReadOnly _Errors As New List(Of AttributeTestErrorEventArgs)
        ''' <summary>Contains value of the <see cref="Warnings"/> property</summary>
        Private ReadOnly _Warnings As New List(Of WarningEventArgs)
        ''' <summary>Contains value of the <see cref="PreventReTesting"/> property</summary>
        Private _PreventReTesting As Boolean = True
        ''' <summary>Gets or sets value indicating if metadata objects that were once tested by this instance are not tested again when encountered</summary>
        ''' <returns>True if testing objects again is prevented, false when not</returns>
        ''' <value>Default value is true</value>
        ''' <remarks>This property being set to true makes this instance to remeber all objects tested by it. It has impact on speed of testing.</remarks>
        ''' <seelaso cref="CreateStatistic"/><seelaso cref="TestedObjects"/>
        Public Property PreventReTesting() As Boolean
            Get
                Return _PreventReTesting
            End Get
            Set(ByVal value As Boolean)
                _PreventReTesting = value
            End Set
        End Property
        ''' <summary>Gets metadata objects that were tested by this instance</summary>
        ''' <remarks>This property is valid only for time when <see cref="PreventReTesting"/> was true.</remarks>
        ''' <seelaso cref="PreventReTesting"/>
        Public ReadOnly Property TestedObjects() As IReadOnlyList(Of ICustomAttributeProvider)
            Get
                Return New ReadOnlyListAdapter(Of ICustomAttributeProvider)(_TestedObjects)
            End Get
        End Property
        ''' <summary>Gets errors encountered by this instance while testing</summary>
        ''' <seelaso cref="[Error]"/>
        Public ReadOnly Property Errors() As IReadOnlyList(Of AttributeTestErrorEventArgs)
            Get
                Return New ReadOnlyListAdapter(Of AttributeTestErrorEventArgs)(_Errors)
            End Get
        End Property
        ''' <summary>Gets warnings encountered by this instance while testing</summary>
        ''' <seelaso cref="[Warning]"/>
        Public ReadOnly Property Warnings() As IReadOnlyList(Of WarningEventArgs)
            Get
                Return New ReadOnlyListAdapter(Of WarningEventArgs)(_Warnings)
            End Get
        End Property
#End Region
#Region "Tests"
        ''' <summary>Tests attributes of given metadata objects</summary>
        ''' <param name="Objects">Metadata objects to test attributes of. Can be null (in such case no tests are performed)</param>
        ''' <param name="Private">When <paramref name="Recursive"/> true indicates if private nested items will be tested</param>
        ''' <param name="Recursive">True to test all nested objects of <paramref name="Objects"/></param>
        ''' <version version="1.5.4">Argument names converted tob camelCase</version>
        Public Sub Test(ByVal objects As IEnumerable(Of ICustomAttributeProvider), Optional ByVal [private] As Boolean = False, Optional ByVal recursive As Boolean = True)
            If Objects Is Nothing Then Exit Sub
            For Each [Object] In Objects
                Test([Object], [Private], Recursive)
            Next
        End Sub
        ''' <summary>Searches <see cref="List(Of T)"/> for item in exception-safe way</summary>
        ''' <param name="list"><see cref="List(Of T)"/> to be searched through</param>
        ''' <param name="Obj">Item to be sought</param>
        ''' <returns>True if <paramref name="list"/> cointains <paramref name="Obj"/></returns>
        <DebuggerStepThrough()> _
        Private Function SafeContains(Of T)(ByVal list As List(Of T), ByVal Obj As T) As Boolean
            For Each item In list
                Try
                    If item.Equals(Obj) Then Return True
                Catch
                    If DirectCast(item, Object) Is DirectCast(Obj, Object) Then Return True
                End Try
            Next
            Return False
        End Function

        ''' <summary>Tests attributes of given metadata object</summary>
        ''' <param name="object">Metadata object to test attributes of. Can be null (in such case no tests are performed)</param>
        ''' <param name="private">When <paramref name="Recursive"/> true indicates if private nested items will be tested</param>
        ''' <param name="recursive">True to test all nested objects of <paramref name="Object"/></param>
        ''' <version version="1.5.4">Parameter names converted to camelCase</version>
        Public Sub Test(ByVal [object] As ICustomAttributeProvider, Optional ByVal [private] As Boolean = False, Optional ByVal recursive As Boolean = True)
            If [object] Is Nothing Then Exit Sub
            If PreventReTesting AndAlso SafeContains(_TestedObjects, [object]) Then Exit Sub
            TestObject([object])
            If Recursive AndAlso OnBeforeExpand([object]) Then
                'Flags
                Dim BindingFlagsBase = BindingFlags.Instance Or BindingFlags.Static Or BindingFlags.Public Or BindingFlags.DeclaredOnly
                Dim PrivateFlag = If([Private], BindingFlags.NonPublic, BindingFlags.Default)
                Dim MethodFlags = BindingFlagsBase Or BindingFlags.InvokeMethod Or BindingFlags.CreateInstance Or PrivateFlag
                Dim FieldFlags = BindingFlagsBase Or BindingFlags.GetField Or BindingFlags.SetField Or PrivateFlag
                Dim NestedTypeFlags = BindingFlagsBase Or PrivateFlag
                Dim PropertyFlags = BindingFlagsBase Or BindingFlags.GetProperty Or BindingFlags.SetProperty Or PrivateFlag
                Dim EventFlags = BindingFlagsBase Or PrivateFlag
                'By type of object
                If TypeOf [object] Is Assembly Then 'Assembly
                    For Each [Module] In DirectCast([object], Assembly).GetModules
                        Test([Module], [Private], Recursive)
                    Next
                ElseIf TypeOf [object] Is [Module] Then 'PE Module
                    For Each Type In DirectCast([object], [Module]).GetTypes
                        If Not Type.IsNotPublic OrElse [Private] Then Test(Type, [Private], Recursive)
                    Next
                    For Each Method In DirectCast([object], [Module]).GetMethods(MethodFlags)
                        Test(Method, [Private], Recursive)
                    Next
                    For Each Field In DirectCast([object], [Module]).GetFields(FieldFlags)
                        Test(Field, [Private], Recursive)
                    Next
                ElseIf TypeOf [object] Is Type Then 'Type
                    If Not DirectCast([object], Type).IsGenericParameter Then
                        For Each Type In DirectCast([object], Type).GetNestedTypes(NestedTypeFlags)
                            Test(Type, [Private], Recursive)
                        Next
                        For Each [Property] In DirectCast([object], Type).GetProperties(PropertyFlags)
                            Test([Property], [Private], Recursive)
                        Next
                        For Each [Event] In DirectCast([object], Type).GetEvents(EventFlags)
                            Test([Event], [Private], Recursive)
                        Next
                        For Each Method In DirectCast([object], Type).GetMethods(MethodFlags)
                            Test(Method, [Private], Recursive)
                        Next
                        For Each Field In DirectCast([object], Type).GetFields(FieldFlags)
                            Test(Field, [Private], Recursive)
                        Next
                        For Each TypeParam In DirectCast([object], Type).GetGenericArguments
                            Test(TypeParam, [Private], Recursive)
                        Next
                    End If
                ElseIf TypeOf [object] Is MethodBase Then 'Method (and ctor)
                    For Each TypeParam In DirectCast([object], MethodInfo).GetGenericArguments
                        Test(TypeParam, [Private], Recursive)
                    Next
                    For Each Param In DirectCast([object], MethodInfo).GetParameters
                        Test(Param, [Private], Recursive)
                    Next
                    'TODO: Which use?
                    Test(DirectCast([object], MethodInfo).ReturnParameter, [Private], Recursive)
                    If DirectCast([object], MethodInfo).ReturnParameter IsNot DirectCast([object], MethodInfo).ReturnTypeCustomAttributes Then _
                        Test(DirectCast([object], MethodInfo).ReturnTypeCustomAttributes, [Private], Recursive)
                ElseIf TypeOf [object] Is PropertyInfo Then 'Property
                    Test(DirectCast([object], PropertyInfo).GetGetMethod([Private]), [Private], Recursive)
                    Test(DirectCast([object], PropertyInfo).GetSetMethod([Private]), [Private], Recursive)
                    For Each Accessor In DirectCast([object], PropertyInfo).GetAccessors([Private])
                        Test(Accessor, [Private], Recursive)
                    Next
                ElseIf TypeOf [object] Is EventInfo Then 'Event
                    Test(DirectCast([object], EventInfo).GetAddMethod([Private]), [Private], Recursive)
                    Test(DirectCast([object], EventInfo).GetRemoveMethod([Private]), [Private], Recursive)
                    Test(DirectCast([object], EventInfo).GetRaiseMethod([Private]), [Private], Recursive)
                    For Each Accessor In DirectCast([object], EventInfo).GetOtherMethods([Private])
                        Test(Accessor, [Private], Recursive)
                    Next
                ElseIf TypeOf [object] Is FieldInfo Then 'Field
                    'DirectCast([Object], FieldInfo)
                ElseIf TypeOf [object] Is ParameterInfo Then 'Parameter
                    'DirectCast([Object],ParameterInfo)
                End If
            End If
        End Sub

        ''' <summary>Performs test of single metadata object</summary>
        ''' <param name="Object">Metadat object to be tested</param>
        ''' <version version="1.5.4">Fix: <see cref="InvalidCastException"/> when attributtes cannot be read via <see cref="CustomAttributeData"/></version>
        ''' <version version="1.5.4">Parameter <c>Object</c> renamed to <c>object</c></version>
        Protected Overridable Sub TestObject(ByVal [object] As ICustomAttributeProvider)
            If Not OnBeforeTest([object]) Then Exit Sub
            Dim attributeData As IList(Of CustomAttributeData) = Nothing
            Dim canGetData As Boolean = True
            OnObjectReached([object])
            Dim exCt As Boolean
            Try
                Try
                    If TypeOf [object] Is MemberInfo Then
                        attributeData = CustomAttributeData.GetCustomAttributes(DirectCast([object], MemberInfo))
                    ElseIf TypeOf [object] Is [Module] Then
                        attributeData = CustomAttributeData.GetCustomAttributes(DirectCast([object], [Module]))
                    ElseIf TypeOf [object] Is Assembly Then
                        attributeData = CustomAttributeData.GetCustomAttributes(DirectCast([object], Assembly))
                    ElseIf TypeOf [object] Is ParameterInfo Then
                        attributeData = CustomAttributeData.GetCustomAttributes(DirectCast([object], ParameterInfo))
                    Else
                        canGetData = False
                    End If
                Catch ex As Exception
                    OnError(ErrorStages.GetCustomAttributeData, ex, [object])
                End Try
                If canGetData Then 'Can create instance manually
                    If attributeData Is Nothing OrElse attributeData.Count = 0 Then Exit Sub
                    For Each aData In attributeData 'For each attribute
                        Dim arguments() As Object = New Object() {}
                        Dim cArgs As IList(Of CustomAttributeTypedArgument)
                        Try
                            cArgs = aData.ConstructorArguments
                        Catch ex As Exception
                            OnError(ErrorStages.GetConstructorArguments, ex, [object], aData)
                            Continue For
                        End Try
                        If cArgs.Count > 0 Then 'Attribute CTor arguments
                            ReDim arguments(cArgs.Count - 1)
                            Dim i As Integer = 0
                            exCt = False
                            For Each arg In cArgs 'Get CTor arguments values
                                Try : arguments(i) = arg.Value
                                Catch ex As Exception
                                    OnError(ErrorStages.GetConstructorArgumentValue, ex, [object], aData, i)
                                    exCt = True
                                    Exit For
                                End Try
                                i += 1
                            Next arg
                            If exCt Then Continue For
                        End If

                        Dim instance As Object
                        Try 'Invoke CTor
                            instance = aData.Constructor.Invoke(arguments)
                        Catch ex As TargetInvocationException
                            OnError(ErrorStages.InvokeConstructor, ex.InnerException, [object], aData)
                            Continue For
                        Catch ex As Exception
                            OnError(ErrorStages.InvokeConstructor, ex, [object], aData)
                            Continue For
                        End Try
                        Dim NArgs As IList(Of CustomAttributeNamedArgument)
                        Try
                            NArgs = aData.NamedArguments
                        Catch ex As Exception
                            OnError(ErrorStages.GetNamedArguments, ex, [object], aData)
                            Continue For
                        End Try
                        If NArgs.Count > 0 Then
                            Dim i% = 0
                            For Each NArg In NArgs 'Set named arguents
                                Dim val As Object
                                exCt = False
                                Try
                                    val = NArg.TypedValue.Value
                                Catch ex As Exception
                                    OnError(ErrorStages.GetNamedArgumentValue, ex, [object], aData, i)
                                    exCt = True
                                    Exit For
                                End Try
                                If TypeOf NArg.MemberInfo Is FieldInfo Then
                                    Try
                                        DirectCast(NArg.MemberInfo, FieldInfo).SetValue(instance, val)
                                    Catch ex As TargetInvocationException
                                        OnError(ErrorStages.SetField, ex.InnerException, [object], aData, i)
                                        exCt = True
                                        Exit For
                                    Catch ex As Exception
                                        OnError(ErrorStages.SetField, ex, [object], aData, i)
                                        exCt = True
                                        Exit For
                                    End Try
                                ElseIf TypeOf NArg.MemberInfo Is PropertyInfo Then
                                    Try
                                        DirectCast(NArg.MemberInfo, PropertyInfo).SetValue(instance, val, Nothing)
                                    Catch ex As TargetInvocationException
                                        OnError(ErrorStages.SetProperty, ex.InnerException, [object], aData, i)
                                        exCt = True
                                        Exit For
                                    Catch ex As Exception
                                        OnError(ErrorStages.SetProperty, ex, [object], aData, i)
                                        exCt = True
                                        Exit For
                                    End Try
                                ElseIf TypeOf NArg.MemberInfo Is MethodBase Then 'Unlikely
                                    OnWarning(New WarningEventArgs([object], instance, aData, i))
                                    Try
                                        DirectCast(NArg.MemberInfo, MethodBase).Invoke(instance, New Object() {val})
                                    Catch ex As TargetInvocationException
                                        OnError(ErrorStages.InvokeMethod, ex.InnerException, [object], aData, i)
                                        exCt = True
                                        Exit For
                                    Catch ex As Exception
                                        OnError(ErrorStages.InvokeMethod, ex, [object], aData, i)
                                        exCt = True
                                        Exit For
                                    End Try
                                Else
                                    OnError(ErrorStages.NamedArgumentUnknown, New InvalidOperationException(ResourcesT.Exceptions.UnsupportedTypeOfMemberInfoOfNamedArgumentOfAttribute), [object], aData, i)
                                    exCt = True
                                    Exit For
                                End If
                                i += 1
                            Next NArg
                            If exCt Then Continue For
                        End If
                        If CreateStatistic Then
                            If _CountAttributeTypes.ContainsKey(instance.GetType) Then _CountAttributeTypes(instance.GetType) += 1 Else _CountAttributeTypes.Add(instance.GetType, 1)
                        End If
                        If TypeOf instance Is Attribute Then
                            OnAttributeOk(New AttributeEventArgs([object], aData, instance))
                        Else
                            OnWarning(New WarningEventArgs([object], instance, aData))
                        End If
                    Next aData
                Else 'Must obtain instance from interface
                    Dim Attrs As Object() = Nothing
                    Try
                        Attrs = [object].GetCustomAttributes(False)
                    Catch ex As Exception
                        OnError(ErrorStages.GetCustomAttributes, ex, [object])
                    End Try
                    OnWarning(New WarningEventArgs([object]))
                    If Attrs IsNot Nothing Then
                        For Each attr In Attrs
                            If _CountAttributeTypes.ContainsKey(attr.GetType) Then _CountAttributeTypes(attr.GetType) += 1 Else _CountAttributeTypes.Add(attr.GetType, 1)
                            OnAttributeOk(New AttributeEventArgs([object], Nothing, attr))
                        Next
                    End If
                End If
            Finally
                If PreventReTesting Then _TestedObjects.Add([object])
                If CreateStatistic Then
                    If _CountObjectTypes.ContainsKey([object].GetType) Then _CountObjectTypes([object].GetType) += 1 Else _CountObjectTypes.Add([object].GetType, 1)
                End If
            End Try
        End Sub
#End Region
#Region "Events"
        ''' <summary>Raises the <see cref="ObjectReached"/> event</summary>
        ''' <param name="Object">Metadata object reached</param>
        ''' <version version="1.5.4">Parameter <c>Object</c> renamed to <c>object</c></version>
        Protected Overridable Sub OnObjectReached(ByVal [object] As ICustomAttributeProvider)
            RaiseEvent ObjectReached(Me, [object])
        End Sub

        ''' <summary>Raises the <see cref="[Error]"/> event</summary>
        ''' <param name="Exception"><see cref="Exception"/> that caused this error</param>
        ''' <param name="Object">Metadata object being verified</param>
        ''' <param name="Stage">Identifies stage of parsing and kind of error</param>
        ''' <param name="CustomAttributeData"><see cref="CustomAttributeData"/> if available in this <paramref name="Stage"/></param>
        ''' <param name="ArgumentIndex">Index of attribute argument if relevant in this <paramref name="Stage"/></param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Stage"/> is not member of <see cref="ErrorStages"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> or <paramref name="Object"/> is null -or- <paramref name="Stage"/> is <see cref="ErrorStages.GetConstructorArguments"/>, <see cref="ErrorStages.GetNamedArguments"/>, <see cref="ErrorStages.InvokeConstructor"/>, <see cref="ErrorStages.GetConstructorArgumentValue"/>, <see cref="ErrorStages.SetField"/>, <see cref="ErrorStages.SetProperty"/>, <see cref="ErrorStages.InvokeMethod"/> or <see cref="ErrorStages.GetNamedArgumentValue"/> and <paramref name="CustomAttributeData"/> is null.</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Stage"/> is <see cref="ErrorStages.GetConstructorArgumentValue"/> and <paramref name="ArgumentIndex"/> is not within range of <paramref name="CustomAttributeData"/>.<see cref="CustomAttributeData.ConstructorArguments">ConstructorArguments</see> -or- <paramref name="Stage"/> is <see cref="ErrorStages.SetField"/>, <see cref="ErrorStages.SetProperty"/>, <see cref="ErrorStages.InvokeMethod"/> or <see cref="ErrorStages.GetNamedArgumentValue"/> and <paramref name="ArgumentIndex"/> is not within range of <paramref name="CustomAttributeData"/>.<see cref="CustomAttributeData.NamedArguments">NamedArguments</see>.</exception>
        Private Sub OnError(ByVal stage As ErrorStages, ByVal exception As Exception, ByVal [object] As ICustomAttributeProvider, Optional ByVal customAttributeData As CustomAttributeData = Nothing, Optional ByVal argumentIndex As Integer = -1)
            OnError(New AttributeTestErrorEventArgs(Stage, Exception, [Object], CustomAttributeData, ArgumentIndex))
        End Sub
        ''' <summary>Raises the <see cref="[Error]"/> event, adds error to <see cref="Errors"/>.</summary>
        ''' <param name="e">Event arguments</param>
        ''' <exception cref="ArgumentNullException"><paramref name="e"/> is null</exception>
        Protected Overridable Sub OnError(ByVal e As AttributeTestErrorEventArgs)
            If e Is Nothing Then Throw New ArgumentNullException("e")
            _Errors.Add(e)
            RaiseEvent [Error](Me, e)
        End Sub
        ''' <summary>Raises the <see cref="AttributeOk"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <exception cref="ArgumentNullException"><paramref name="e"/> is null</exception>
        Protected Overridable Sub OnAttributeOk(ByVal e As AttributeEventArgs)
            If e Is Nothing Then Throw New ArgumentNullException("e")
            RaiseEvent AttributeOk(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="BeforeTest"/> event</summary>
        ''' <param name="Object">Object to be tested</param>
        ''' <returns>True to test the object, false to skip it</returns>
        ''' <version version="1.5.4">Parameter <c>Object</c> renamed to <c>object</c></version>
        Protected Overridable Function OnBeforeTest(ByVal [object] As ICustomAttributeProvider) As Boolean
            Dim e As New CancelObjectEventArgs([object])
            RaiseEvent BeforeTest(Me, e)
            Return Not e.Cancel
        End Function
        ''' <summary>Raises the <see cref="BeforeExpand"/> event</summary>
        ''' <param name="Object">Object to be expanded</param>
        ''' <returns>True to expand the object, false to skip it</returns>
        ''' <remarks>Raised only for recursive testing</remarks>
        ''' <version version="1.5.4">Parameter <c>Object</c> renamed to <c>object</c></version>
        Protected Overridable Function OnBeforeExpand(ByVal [object] As ICustomAttributeProvider) As Boolean
            Dim e As New CancelObjectEventArgs([object])
            RaiseEvent BeforeExpand(Me, e)
            Return Not e.Cancel
        End Function
        ''' <summary>Raises the <see cref="Warning"/> event, adds warning to <see cref="Warnings"/> collection</summary>
        ''' <param name="e">event arguments</param>
        ''' <exception cref="ArgumentNullException"><paramref name="e"/> is null</exception>
        Protected Overridable Sub OnWarning(ByVal e As WarningEventArgs)
            If e Is Nothing Then Throw New ArgumentNullException("e")
            _Warnings.Add(e)
            RaiseEvent Warning(Me, e)
        End Sub
        ''' <summary>Raised before metadata object is tested.</summary>
        ''' <remarks>This event can be cancelled.</remarks>
        Public Event BeforeTest As EventHandler(Of AttributeTest, CancelObjectEventArgs)
        ''' <summary>During recursive testing raised before metadata object is queried for its sub-objects</summary>
        ''' <remarks>This event can be canceůlled.</remarks>
        Public Event BeforeExpand As EventHandler(Of AttributeTest, CancelObjectEventArgs)
        ''' <summary>Raised before object is tested</summary>
        ''' <param name="sender">This instance</param>
        ''' <param name="object">Object being tested</param>
        ''' <version version="1.5.4">Parameter <c>Object</c> renamed to <c>object</c></version>
        Public Event ObjectReached(ByVal sender As AttributeTest, ByVal [object] As ICustomAttributeProvider)
        ''' <summary>Raised when an exception occures causing that particular attribute cannot be instantiated</summary>
        ''' <remarks>Errors are stored in the <see cref="Errors"/> collection.</remarks>
        ''' <seelaso cref="Errors"/>
        Public Event [Error] As EventHandler(Of AttributeTest, AttributeTestErrorEventArgs)
        ''' <summary>Raised after attribute is successfully parsed and instantiated</summary>
        ''' <remarks>Not raised of attributes that do not inherit from <see cref="Attribute"/>. See <see cref="Warning"/> for such attributes.</remarks>
        Public Event AttributeOk As EventHandler(Of AttributeTest, AttributeEventArgs)
        ''' <summary>Raised when non-critical issue occures during parse proces</summary>
        ''' <remarks>Warnings are stored in the <see cref="Warnings"/> collection</remarks>
        ''' <seelaso cref="Warnings"/>
        Public Event Warning As EventHandler(Of AttributeTest, WarningEventArgs)
#End Region
#Region "Event args"
        ''' <summary>Identifies places where is expected error and identifies kinds of errors</summary>
        Public Enum ErrorStages
            ''' <summary>The <see cref="CustomAttributeData.GetCustomAttributes"/> method is called</summary>
            ''' <remarks><see cref="AttributeTestErrorEventArgs.CustomAttributeData"/> data is null and <see cref="AttributeTestErrorEventArgs.ArgumentIndex"/> has no meaning</remarks>
            GetCustomAttributeData
            ''' <summary>The <see cref="CustomAttributeData.ConstructorArguments"/> is got</summary>
            ''' <remarks><see cref="AttributeTestErrorEventArgs.CustomAttributeData"/> is not null amd <see cref="AttributeTestErrorEventArgs.ArgumentIndex"/> has no meaning</remarks>
            GetConstructorArguments
            ''' <summary>The <see cref="CustomAttributeTypedArgument.Value"/> is got</summary>
            ''' <remarks><see cref="AttributeTestErrorEventArgs.CustomAttributeData"/> is not null amd <see cref="AttributeTestErrorEventArgs.ArgumentIndex"/> indicates index into <see cref="CustomAttributeData.ConstructorArguments"/> collection</remarks>
            GetConstructorArgumentValue
            ''' <summary>Custom attribute constructor obtained via <see cref="CustomAttributeData.Constructor"/> is invoked</summary>
            ''' <remarks><see cref="AttributeTestErrorEventArgs.CustomAttributeData"/> is not null amd <see cref="AttributeTestErrorEventArgs.ArgumentIndex"/> has no meaning</remarks>
            InvokeConstructor
            ''' <summary>The <see cref="CustomAttributeData.NamedArguments"/> is got</summary>
            ''' <remarks><see cref="AttributeTestErrorEventArgs.CustomAttributeData"/> is not null amd <see cref="AttributeTestErrorEventArgs.ArgumentIndex"/> has no meaning</remarks>
            GetNamedArguments
            ''' <summary>The <see cref="CustomAttributeNamedArgument.TypedValue"/> is got</summary>
            ''' <remarks><see cref="AttributeTestErrorEventArgs.CustomAttributeData"/> is not null amd <see cref="AttributeTestErrorEventArgs.ArgumentIndex"/> indicates index into <see cref="CustomAttributeData.NamedArguments"/> collection.</remarks>
            GetNamedArgumentValue
            ''' <summary>Named argument with <see cref="CustomAttributeNamedArgument.MemberInfo"/> of type <see cref="FieldInfo"/> is set</summary>
            ''' <remarks><see cref="AttributeTestErrorEventArgs.CustomAttributeData"/> is not null amd <see cref="AttributeTestErrorEventArgs.ArgumentIndex"/> indicates index into <see cref="CustomAttributeData.NamedArguments"/> collection.</remarks>
            SetField
            ''' <summary>Named argument with <see cref="CustomAttributeNamedArgument.MemberInfo"/> of type <see cref="PropertyInfo"/> is set</summary>
            ''' <remarks><see cref="AttributeTestErrorEventArgs.CustomAttributeData"/> is not null amd <see cref="AttributeTestErrorEventArgs.ArgumentIndex"/> indicates index into <see cref="CustomAttributeData.NamedArguments"/> collection.</remarks>
            SetProperty
            ''' <summary>Named argument with <see cref="CustomAttributeNamedArgument.MemberInfo"/> of type <see cref="MethodBase"/> is set</summary>
            ''' <remarks><see cref="AttributeTestErrorEventArgs.CustomAttributeData"/> is not null amd <see cref="AttributeTestErrorEventArgs.ArgumentIndex"/> indicates index into <see cref="CustomAttributeData.NamedArguments"/> collection.</remarks>
            InvokeMethod
            ''' <summary><see cref="CustomAttributeNamedArgument.MemberInfo"/> is neither of <see cref="FieldInfo"/>, <see cref="PropertyInfo"/>, <see cref="MethodBase"/></summary>
            ''' <remarks><see cref="AttributeTestErrorEventArgs.CustomAttributeData"/> is not null amd <see cref="AttributeTestErrorEventArgs.ArgumentIndex"/> indicates index into <see cref="CustomAttributeData.NamedArguments"/> collection.</remarks>
            NamedArgumentUnknown
            ''' <summary><see cref="ICustomAttributeProvider.GetCustomAttributes"/> is invoked (because object is of none of types supported by <see cref="CustomAttributeData.GetCustomAttributes"/></summary>
            ''' <remarks><see cref="AttributeTestErrorEventArgs.CustomAttributeData"/> data is null and <see cref="AttributeTestErrorEventArgs.ArgumentIndex"/> has no meaning</remarks>
            GetCustomAttributes
        End Enum
        ''' <summary>Event arguments reporting attributes</summary>
        Public Class AttributeEventArgs : Inherits EventArgs
            ''' <summary>Contains value of the <see cref="[Object]"/> property</summary>
            Private ReadOnly _object As ICustomAttributeProvider
            ''' <summary>Contains value of the <see cref=" CustomAttributeData"/> property</summary>
            Private ReadOnly _customAttributeData As CustomAttributeData
            ''' <summary>Contains value of the <see cref="Attribute"/> property</summary>
            Private ReadOnly _attribute As Attribute
            ''' <summary>Gets the object the attribute is applied onto</summary>
            Public ReadOnly Property [Object]() As ICustomAttributeProvider
                Get
                    Return _Object
                End Get
            End Property
            ''' <summary>If available gets <see cref="Reflection.CustomAttributeData"/> <see cref="Attribute"/> was instantiated from</summary>
            Public ReadOnly Property CustomAttributeData() As CustomAttributeData
                Get
                    Return _CustomAttributeData
                End Get
            End Property
            ''' <summary>The attribute</summary>
            Public ReadOnly Property Attribute() As Attribute
                Get
                    Return _Attribute
                End Get
            End Property
            ''' <summary>CTor</summary>
            ''' <param name="Object">Metadata objetc <paramref name="Attribute"/> is applied onto</param>
            ''' <param name="CustomAttributeData">If available, <see cref="Reflection.CustomAttributeData"/> <paramref name="Attribute"/> was instantiated from</param>
            ''' <param name="Attribute">The attribute</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Object"/> or <paramref name="Attribute"/> is null</exception>
            ''' <version version="1.5.4">Argument names converted tob camelCase</version>
            Public Sub New(ByVal [object] As ICustomAttributeProvider, ByVal customAttributeData As CustomAttributeData, ByVal attribute As Attribute)
                If [Object] Is Nothing Then Throw New ArgumentNullException("Object")
                If Attribute Is Nothing Then Throw New ArgumentNullException("Attrribute")
                _object = [Object]
                _customAttributeData = CustomAttributeData
                _attribute = Attribute
            End Sub
        End Class

        ''' <summary>Arguments of event reporting custom attributes test error</summary>
        Public Class AttributeTestErrorEventArgs : Inherits EventArgs
            ''' <summary>CTor</summary>
            ''' <param name="Stage">Identifies kind of erro and stage of attribute construction process where the error has happened</param>
            ''' <param name="Exception">The exception that caused the error</param>
            ''' <param name="Object">Objets being tested</param>
            ''' <param name="CustomAttributeData">Attribute data attribute is about to be constructed from (if available; otherwise null)</param>
            ''' <param name="ArgumentIndex">Index of argument that caused the error (if relevant; otherwise -1)</param>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Stage"/> is not member of <see cref="ErrorStages"/></exception>
            ''' <exception cref="ArgumentNullException"><paramref name="Exception"/> or <paramref name="Object"/> is null -or- <paramref name="Stage"/> is <see cref="ErrorStages.GetConstructorArguments"/>, <see cref="ErrorStages.GetNamedArguments"/>, <see cref="ErrorStages.InvokeConstructor"/>, <see cref="ErrorStages.GetConstructorArgumentValue"/>, <see cref="ErrorStages.SetField"/>, <see cref="ErrorStages.SetProperty"/>, <see cref="ErrorStages.InvokeMethod"/> or <see cref="ErrorStages.GetNamedArgumentValue"/> and <paramref name="CustomAttributeData"/> is null.</exception>
            ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Stage"/> is <see cref="ErrorStages.GetConstructorArgumentValue"/> and <paramref name="ArgumentIndex"/> is not within range of <paramref name="CustomAttributeData"/>.<see cref="CustomAttributeData.ConstructorArguments">ConstructorArguments</see> -or- <paramref name="Stage"/> is <see cref="ErrorStages.SetField"/>, <see cref="ErrorStages.SetProperty"/>, <see cref="ErrorStages.InvokeMethod"/> or <see cref="ErrorStages.GetNamedArgumentValue"/> and <paramref name="ArgumentIndex"/> is not within range of <paramref name="CustomAttributeData"/>.<see cref="CustomAttributeData.NamedArguments">NamedArguments</see>.</exception>
            ''' <version version="1.5.4">Argument names converted tob camelCase</version>
            Public Sub New(ByVal stage As ErrorStages, ByVal exception As Exception, ByVal [object] As ICustomAttributeProvider, Optional ByVal customAttributeData As CustomAttributeData = Nothing, Optional ByVal argumentIndex As Integer = -1)
                If Not InEnum(stage) Then Throw New InvalidEnumArgumentException("Stage", stage, stage.GetType)
                If exception Is Nothing Then Throw New ArgumentNullException("Exception")
                If [object] Is Nothing Then Throw New ArgumentNullException("Object")
                Select Case stage
                    Case ErrorStages.GetCustomAttributeData, ErrorStages.GetCustomAttributes
                        If customAttributeData IsNot Nothing OrElse argumentIndex <> -1 Then _
                            Throw New ArgumentException(ResourcesT.Exceptions.WhenStageIsGetCustomAttributeDateOrGetCustomAttributesCustomAttribuetDataMustBeNullAndArgumentIndexMustBe1)
                    Case ErrorStages.GetConstructorArguments, ErrorStages.GetNamedArguments, ErrorStages.InvokeConstructor
                        If customAttributeData Is Nothing Then Throw New ArgumentNullException(ResourcesT.Exceptions.WhenStageIsGetConstructorArgumentsGetNamedArgumentsInvokeConstructorGetConstructorArgumentValueSetFieldSetPropertyInvokeMethodOrGetNamedArgumentValueCustomAttributeDataCannotBeNull)
                    Case ErrorStages.GetConstructorArgumentValue
                        If customAttributeData Is Nothing Then Throw New ArgumentNullException(ResourcesT.Exceptions.WhenStageIsGetConstructorArgumentsGetNamedArgumentsInvokeConstructorGetConstructorArgumentValueSetFieldSetPropertyInvokeMethodOrGetNamedArgumentValueCustomAttributeDataCannotBeNull)
                        If argumentIndex < 0 OrElse argumentIndex >= customAttributeData.ConstructorArguments.Count Then _
                            Throw New ArgumentOutOfRangeException("ArgumentIndex", ResourcesT.Exceptions.WhenStageIsGetConstructorArgumentValueArgumentIndexMustBeWithinRangeOfCustomAttributeDateConstructorArguments)
                    Case ErrorStages.SetField, ErrorStages.SetProperty, ErrorStages.InvokeMethod, ErrorStages.GetNamedArgumentValue
                        If customAttributeData Is Nothing Then Throw New ArgumentNullException(ResourcesT.Exceptions.WhenStageIsGetConstructorArgumentsGetNamedArgumentsInvokeConstructorGetConstructorArgumentValueSetFieldSetPropertyInvokeMethodOrGetNamedArgumentValueCustomAttributeDataCannotBeNull)
                        If argumentIndex < 0 OrElse argumentIndex >= customAttributeData.NamedArguments.Count Then _
                            Throw New ArgumentOutOfRangeException("ArgumentIndex", ResourcesT.Exceptions.WhenStageIsGetConstructorArgumentValueArgumentIndexMustBeWithinRangeOfCustomAttributeDateNamedArguments)
                End Select
                _Stage = stage
                _Exception = exception
                _Object = [object]
                _CustomAttributeData = customAttributeData
                _ArgumentIndex = argumentIndex
            End Sub
#Region "Properties"
            ''' <summary>Contains value of the <see cref="Stage"/> property</summary>
            Private ReadOnly _Stage As ErrorStages
            ''' <summary>Contains value of the <see cref="Exception"/> property</summary>
            Private ReadOnly _Exception As Exception
            ''' <summary>Contains value of the <see cref="[Object]"/> property</summary>
            Private ReadOnly _Object As ICustomAttributeProvider
            ''' <summary>Contains value of the <see cref="CustomAttributeData"/> property</summary>
            Private ReadOnly _CustomAttributeData As CustomAttributeData
            ''' <summary>Contains value of the <see cref="ArgumentIndex"/> property</summary>
            Private ReadOnly _ArgumentIndex As Integer
            ''' <summary>Gest value indicating kind of error that has occured and stage of custom attribute initialization in which it has occured.</summary>
            ''' <returns>Stage when which caused the event has error ocured</returns>
            ''' <remarks>Value of this property also indicates meaning of <see cref="CustomAttributeData"/> and <see cref="ArgumentIndex"/> properties</remarks>
            Public ReadOnly Property Stage() As ErrorStages
                Get
                    Return _Stage
                End Get
            End Property
            ''' <summary>Gets exception that caused the error which caused the event</summary>
            ''' <returns>Exception which caused the error that caused the event</returns>
            Public ReadOnly Property Exception() As Exception
                Get
                    Return _Exception
                End Get
            End Property
            ''' <summary>Gets metdatata object the attribute that caused the error was applied onto</summary>
            ''' <returns>Metadata object errorneous attribute is applied onto</returns>
            Public ReadOnly Property [Object]() As ICustomAttributeProvider
                Get
                    Return _Object
                End Get
            End Property
            ''' <summary>If vailable gets <see cref="Reflection.CustomAttributeData"/> the attribute which caused th error have been being created form</summary>
            ''' <returns><see cref="Reflection.CustomAttributeData"/> or null if <see cref="Stage"/> is <see cref="ErrorStages.GetCustomAttributeData"/> or <see cref="ErrorStages.GetCustomAttributes"/>.</returns>
            Public ReadOnly Property CustomAttributeData() As CustomAttributeData
                Get
                    Return _CustomAttributeData
                End Get
            End Property
            ''' <summary>In stages where it has meaning gets index to either <see cref="CustomAttributeData.ConstructorArguments"/> or <see cref="CustomAttributeData.NamedArguments"/> or <see cref="CustomAttributeData"/> indicating value that caused the exception.</summary>
            ''' <returns>If stage is <see cref="ErrorStages.GetConstructorArgumentValue"/> returns index to <see cref="CustomAttributeData.ConstructorArguments"/> collection.
            ''' If <see cref="Stage"/> is <see cref="ErrorStages.GetNamedArgumentValue"/>, <see cref="ErrorStages.SetField"/>, <see cref="ErrorStages.SetProperty"/> or <see cref="ErrorStages.InvokeMethod"/> returns index to <see cref="CustomAttributeData.NamedArguments"/> collection.
            ''' Otherwise return -1.</returns>
            Public ReadOnly Property ArgumentIndex() As Integer
                Get
                    Return _ArgumentIndex
                End Get
            End Property
#End Region
        End Class
        ''' <summary>Cancellable event arguments carrying <see cref="ICustomAttributeProvider"/></summary>
        ''' <seelaso cref="CancelEventArgs"/>
        Public Class CancelObjectEventArgs
            Inherits CancelEventArgs
            ''' <summary>Contains value of the <see cref="[Object]"/> property</summary>
            Private ReadOnly _Object As ICustomAttributeProvider
            ''' <summary>Gets metadata object associated with the event</summary>
            Public ReadOnly Property [Object]() As ICustomAttributeProvider
                Get
                    Return _Object
                End Get
            End Property
            ''' <summary>CTor</summary>
            ''' <param name="Object">Metadata object assocuated with the event</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Object"/> is null</exception>
            Public Sub New(ByVal [Object] As ICustomAttributeProvider)
                If [Object] Is Nothing Then Throw New ArgumentNullException("Object")
                _Object = [Object]
            End Sub
        End Class
        ''' <summary>Kinds of warning used by <see cref="WarningEventArgs.Kind"/></summary>
        ''' <seelaso cref="WarningEventArgs"/><seelaso cref="Warning"/>
        Public Enum WarningKinds
            ''' <summary>Type of <see cref="CustomAttributeNamedArgument.MemberInfo"/> from <see cref="CustomAttributeData.NamedArguments"/> is <see cref="MethodBase"/></summary>
            NamedArgumentMethodInvoke
            ''' <summary>Attribute does not inherit from <see cref="Attribute"/>.</summary>
            NonAttributeAttribute
            ''' <summary>Metadata object being tested is not one of thos supported by <see cref="CustomAttributeData.GetCustomAttributes"/> overloaded function</summary>
            UnknownProvider
        End Enum
        ''' <summary>Arguments of the <see cref="Warning"/> event</summary>
        Public Class WarningEventArgs : Inherits EventArgs
            ''' <summary>CTor for <see cref="WarningKinds.UnknownProvider"/></summary>
            ''' <param name="Object">Object being tested</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Object"/> is null</exception>
            ''' <version version="1.5.4">Parameter <c>Object</c> renamed to <c>object</c></version>
            Public Sub New(ByVal [object] As ICustomAttributeProvider)
                Me.New(WarningKinds.UnknownProvider, [Object])
            End Sub
            ''' <summary>CTor for <see cref="WarningKinds.NonAttributeAttribute"/></summary>
            ''' <param name="Object">Object the attribute is applied onto</param>
            ''' <param name="Attribute">The attrbite that does not derive from <see cref="System.Attribute"/></param>
            ''' <param name="CustomAttributeData"><see cref="Reflection.CustomAttributeData"/> the attribute was created from</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Object"/>, <paramref name="Attribute"/> por <paramref name="CustomAttributeData"/> is null</exception>
            ''' <exception cref="TypeMismatchException"><paramref name="Attribute"/> is of type <see cref="System.Attribute"/></exception>
            ''' <version version="1.5.4">Argument names converted tob camelCase</version>
            Public Sub New(ByVal [object] As ICustomAttributeProvider, ByVal attribute As Object, ByVal customAttributeData As CustomAttributeData)
                Me.New(WarningKinds.NonAttributeAttribute, [Object], Attribute, CustomAttributeData)
                If Attribute Is Nothing Then Throw New ArgumentNullException("Attribute")
                If CustomAttributeData Is Nothing Then Throw New ArgumentNullException("CustomAttributeData")
                If GetType(Attribute).IsAssignableFrom(Attribute.GetType) Then Throw New TypeMismatchException(ResourcesT.Exceptions.AttributeCannotDeriveFromAttributeWhenWarningKindIsNoAttributeAttribute)
            End Sub
            ''' <summary>CTor for <see cref="WarningKinds.NamedArgumentMethodInvoke"/></summary>
            ''' <param name="Object">Object the attribute is applied onto</param>
            ''' <param name="Attribute">Attribute instance being applied. Can be not fully initialized by named arguments.</param>
            ''' <param name="ArgumentIndex">Index into <see cref="CustomAttributeData.NamedArguments"/> pointing to named argument which causd the warning</param>
            ''' <param name="CustomAttributeData"><see cref="Reflection.CustomAttributeData"/> <paramref name="Attribute"/> is being created from</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Object"/> or <paramref name="Attribute"/> or <paramref name="CustomAttributeData"/> is null</exception>
            ''' <exception cref="ArgumentOutOfRangeException"><paramref name="ArgumentIndex"/> is not whthin range of <paramref name="CustomAttributeData"/>.<see cref="CustomAttributeData.NamedArguments">NamedArguments</see></exception>
            ''' <version version="1.5.4">Argument names converted tob camelCase</version>
            Public Sub New(ByVal [object] As ICustomAttributeProvider, ByVal attribute As Object, ByVal customAttributeData As CustomAttributeData, ByVal argumentIndex As Integer)
                Me.New(WarningKinds.NamedArgumentMethodInvoke, [object], attribute, customAttributeData, argumentIndex)
                If attribute Is Nothing Then Throw New ArgumentNullException("Attribute")
                If customAttributeData Is Nothing Then Throw New ArgumentNullException("CustomAttributeData")
                If argumentIndex < 0 OrElse argumentIndex >= customAttributeData.NamedArguments.Count Then _
                    Throw New ArgumentOutOfRangeException("ArgumentIndex")
            End Sub
            ''' <summary>Internal CTor</summary>
            ''' <param name="Kind">Kind of warning</param>
            ''' <param name="Object">Metadata object being tested</param>
            ''' <param name="CustomAttributeData"><see cref="Reflection.CustomAttributeData"/> attribute is being/was created form (if available)</param>
            ''' <param name="Attribute">Attribute instance (if available) which caused the warning</param>
            ''' <param name="ArgumentIndex">Index of argument of attribute which caused the warning</param>
            ''' <version version="1.5.4">Argument names converted tob camelCase</version>
            Private Sub New(ByVal kind As WarningKinds, ByVal [object] As ICustomAttributeProvider, Optional ByVal attribute As Object = Nothing, Optional ByVal customAttributeData As CustomAttributeData = Nothing, Optional ByVal argumentIndex As Integer = -1)
                If Not InEnum(kind) Then Throw New InvalidEnumArgumentException("Kind", kind, kind.GetType)
                If [object] Is Nothing Then Throw New ArgumentNullException("Object")
                _Object = [object]
                _Attribute = attribute
                _CustomAttributeData = customAttributeData
                _ArgumentIndex = argumentIndex
                _Kind = kind
            End Sub
            ''' <summary>Contains value of the <see cref="[Object]"/> property</summary>
            Private ReadOnly _Object As ICustomAttributeProvider
            ''' <summary>Contains value of the <see cref="Attribute"/> proeprty</summary>
            Private ReadOnly _Attribute As Object
            ''' <summary>Contains value of the <see cref="CustomAttributeData"/> property</summary>
            Private ReadOnly _CustomAttributeData As CustomAttributeData
            ''' <summary>Contains value of the <see cref="ArgumentIndex"/> property</summary>
            Private ReadOnly _ArgumentIndex As Integer
            ''' <summary>Contains value of the <see cref="Kind"/> property</summary>
            Private ReadOnly _Kind As WarningKinds
            ''' <summary>Gets metadata object being tested while the warning was generated</summary>
            ''' <returns>Object being tested</returns>
            Public ReadOnly Property [Object]() As ICustomAttributeProvider
                Get
                    Return _Object
                End Get
            End Property
            ''' <summary>Gets attribute that caused the warning (if available)</summary>
            ''' <returns>When <see cref="Kind"/> is <see cref="WarningKinds.NamedArgumentMethodInvoke"/> or <see cref="WarningKinds.NonAttributeAttribute"/> returns arrtibute that caused the warning (Note: For <see cref="WarningKinds.NamedArgumentMethodInvoke"/> the attribute may be not fully initialized by named arguments.). In case of <see cref="WarningKinds.UnknownProvider"/> returns null.</returns>
            Public ReadOnly Property Attribute() As Object
                Get
                    Return _Attribute
                End Get
            End Property
            ''' <summary>Gets (if applicable) <see cref="Reflection.CustomAttributeData"/> attributes is ceated form.</summary>
            ''' <returns>If <see cref="Kind"/> is <see cref="WarningKinds.NamedArgumentMethodInvoke"/> or <see cref="WarningKinds.NonAttributeAttribute"/> returns <see cref="Reflection.CustomAttributeData"/>. For <see cref="Kind"/> = <see cref="WarningKinds.UnknownProvider"/> returns null.</returns>
            ''' <remarks>If <see cref="Kind"/> is <see cref="WarningKinds.NamedArgumentMethodInvoke"/>, <see cref="Attribute"/> may not be fully initialized by named arguments.</remarks>
            Public ReadOnly Property CustomAttributeData() As CustomAttributeData
                Get
                    Return _CustomAttributeData
                End Get
            End Property
            ''' <summary>If applicble getsidex of argument that caused the warning.</summary>
            ''' <returns>If <see cref="Kind"/> is <see cref="WarningKinds.NamedArgumentMethodInvoke"/> returns index of arument into <see cref="CustomAttributeData"/>.<see cref="CustomAttributeData.NamedArguments">NamedArguments</see> which is <see cref="MethodBase"/>. Otherwise returns -1.</returns>
            Public ReadOnly Property ArgumentIndex() As Integer
                Get
                    Return _ArgumentIndex
                End Get
            End Property
            ''' <summary>Gets kind of warning</summary>
            ''' <returns>Kind of warning indicatin why the warning was issued.</returns>
            Public ReadOnly Property Kind() As WarningKinds
                Get
                    Return _Kind
                End Get
            End Property
        End Class
#End Region
#Region "Statistics"
        ''' <summary>Contains value of the <see cref="CreateStatistic"/> property</summary>
        Private _CreateStatistic As Boolean = True
        ''' <summary>Contains value of the <see cref="CountObjectTypes"/> property</summary>
        Private _CountObjectTypes As New Dictionary(Of Type, Integer)
        ''' <summary>Contains value of the <see cref="CountAttributeTypes"/> proeprty</summary>
        Private _CountAttributeTypes As New Dictionary(Of Type, Integer)
        ''' <summary>Gets counts of metadata object types tested</summary>
        ''' <remarks>This property is valid only for part of testing when <see cref="CreateStatistic"/> was true and only from last call of <see cref="ResetStatistic"/></remarks>
        ''' <seealso cref="CountAttributeTypes"/><seelaso cref="ResetStatistic"/><seelaso cref="CreateStatistic"/>
        Public ReadOnly Property CountObjectTypes() As IDictionary(Of Type, Integer)
            Get
                Return New ReadOnlyDictionary(Of Type, Integer)(_CountObjectTypes)
            End Get
        End Property
        ''' <summary>Gets counts of attribute types encountered</summary>
        ''' <remarks>This property is valid only for part of testing when <see cref="CreateStatistic"/> was true and only from last call of <see cref="ResetStatistic"/>.</remarks>
        ''' <seealso cref="CountObjectTypes"/><seelaso cref="ResetStatistic"/><seelaso cref="CreateStatistic"/>
        Public ReadOnly Property CountAttributeTypes() As IDictionary(Of Type, Integer)
            Get
                Return New ReadOnlyDictionary(Of Type, Integer)(_CountAttributeTypes)
            End Get
        End Property
        ''' <summary>Gets or sets value indicating if statistical data are collected</summary>
        ''' <returns>True when statatistical data are collected; false otherwise</returns>
        ''' <value>Default value is true</value>
        ''' <remarks>This property influenses <see cref="CountObjectTypes"/> and <see cref="CountAttributeTypes"/></remarks>
        ''' <seelaso cref="ResetStatistic"/><seelaso cref="CountAttributeTypes"/><seelaso cref="CountObjectTypes"/>
        ''' <seelaso cref="PreventReTesting"/>
        Public Property CreateStatistic() As Boolean
            Get
                Return _CreateStatistic
            End Get
            Set(ByVal value As Boolean)
                _CreateStatistic = value
            End Set
        End Property
        ''' <summary>Resets statistical data collected in <see cref="CountAttributeTypes"/> and <see cref="CountObjectTypes"/></summary>
        ''' <seelaso cref="CreateStatistic"/><seelaso cref="CountAttributeTypes"/><seelaso cref="CountObjectTypes"/>
        Public Sub ResetStatistic()
            _CountAttributeTypes.Clear()
            _CountObjectTypes.Clear()
        End Sub
#End Region
    End Class
End Namespace
