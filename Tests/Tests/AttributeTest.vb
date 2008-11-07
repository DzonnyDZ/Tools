Imports Tools.TestsT, System.Linq, System.Reflection
Imports Tools.ReflectionT, MBox = Tools.WindowsT.IndependentT.MessageBox
Namespace TestsT
    ''' <summary>Tests for <see cref="Tools.TestsT.AttributeTest"/></summary>
    Module AttributeTest
        ''' <summary>Indicates verbose output</summary>
        Private verbose As Boolean = True
        ''' <summary>Performs test</summary>
        Public Sub Test()
            'Cose assembly
            Dim ofd As New OpenFileDialog With {.Title = "Select assembly", .Filter = "Assembly files (*.dll,*.exe)|*.dll;*.exe"}
            If ofd.ShowDialog = DialogResult.OK Then
                Dim Test As New Tools.TestsT.AttributeTest
                'Open console
                ConsoleT.AllocateConsole()
                Dim Statistic As Boolean
                Try
                    Console.Title = "Testing Tools.Tests.AttributeTest"
                    ConsoleT.Icon = Tools.ResourcesT.ToolsIcon
                    Dim asm = Reflection.Assembly.LoadFrom(ofd.FileName)
                    Dim ConsoleWindow As New Tools.WindowsT.NativeT.Win32Window(ConsoleT.GetHandle)
                    'Chose mode
                    Select Case MBox.ModalEx_PTEIOWMHS("Chose output details mode, please.", "AttributeTest", _
                            Items:=DirectCast(New Object() { _
                                New MBox.MessageBoxButton("Full info", DialogResult.OK), _
                                New MBox.MessageBoxButton("No statistic", DialogResult.Cancel), _
                                New MBox.MessageBoxButton("No verbose output", DialogResult.Abort), _
                                New MBox.MessageBoxButton("Short", DialogResult.Retry) _
                            }, IEnumerable(Of Object)), Owner:=ConsoleWindow).DialogResult
                        Case DialogResult.OK
                            Test.CreateStatistic = True
                            Test.PreventReTesting = True
                            Statistic = True
                            verbose = True
                        Case DialogResult.Cancel
                            Test.CreateStatistic = False
                            Test.PreventReTesting = False
                            Statistic = False
                            verbose = True
                        Case DialogResult.Abort
                            Test.CreateStatistic = True
                            Test.PreventReTesting = True
                            Statistic = True
                            verbose = False
                        Case DialogResult.Retry
                            Test.CreateStatistic = False
                            Test.PreventReTesting = False
                            Statistic = False
                            verbose = False
                        Case Else : Exit Sub
                    End Select
                    'Add ahndlers
                    AddHandler Test.ObjectReached, AddressOf Test_ObjectReached
                    AddHandler Test.Error, AddressOf Test_Error
                    AddHandler Test.Warning, AddressOf Test_Warning
                    AddHandler Test.AttributeOk, AddressOf Test_AttributeOk
                    'Run test
                    Test.Test(asm, True)
                    'Conclustion
                    Console.ForegroundColor = ConsoleColor.Green
                    Console.WriteLine()
                    Console.WriteLine("Test finished.")
                    Console.WriteLine()
                    If Statistic Then CreateStatistic(Test) Else ErrorSummary(Test)
                    Console.WriteLine()
                    Console.WriteLine()
                    Console.Write("Press any key to continue...")
                    Console.ReadKey(True)
                Catch ex As OperationCanceledException When ex.Message = oc.ToString 'Esc pressed
                    Console.ForegroundColor = ConsoleColor.Yellow
                    Console.WriteLine("Operation canceled")
                    Console.WriteLine()
                    If Statistic Then CreateStatistic(Test) Else ErrorSummary(Test)
                    Console.WriteLine()
                    Console.WriteLine()
                    Console.Write("Press any key to continue...")
                    Console.ReadKey(True)
                    While Console.ReadKey(True).Key = ConsoleKey.Escape : End While
                Catch ex As Exception 'Error
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("Testing engine error " & ex.GetType.Name)
                    Console.WriteLine(ex.Message)
                    Console.ReadKey(True)
                Finally
                    ConsoleT.DetachConsole()
                End Try
            End If
        End Sub
        ''' <summary>Prints error summary</summary>
        ''' <param name="Test">Test class</param>
        Private Sub ErrorSummary(ByVal Test As Tools.TestsT.AttributeTest)
            Console.ForegroundColor = ConsoleColor.White
            Console.WriteLine("Error summary:")
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("Errors {0}", Test.Errors.Count)
            Console.ForegroundColor = ConsoleColor.Yellow
            Console.WriteLine("Warnings {0}", Test.Warnings.Count)
            If Test.Errors.Count > 0 Then
                Console.ForegroundColor = ConsoleColor.Red
                For Each er In Test.Errors
                    Dim tstr = GetObjectString(er.[Object])
                    Console.WriteLine("{0}: {1} {2}", tstr, er.Exception.GetType.Name, er.Exception.Message)
                Next
            End If
        End Sub
        ''' <summary>Prints statistics</summary><param name="Test">Prtint statistic for this test class</param>
        Private Sub CreateStatistic(ByVal Test As Tools.TestsT.AttributeTest)
            Console.ForegroundColor = ConsoleColor.White
            Console.WriteLine("Statistics:")
            Console.ForegroundColor = ConsoleColor.White
            Console.WriteLine("Objects tested {0}", Test.TestedObjects.Count)
            ErrorSummary(Test)
            Console.ForegroundColor = ConsoleColor.White
            Console.WriteLine()
            Console.WriteLine("Attribute types {0}", Test.CountAttributeTypes.Count)
            Console.WriteLine("Object types {0}", Test.CountObjectTypes.Count)
            Console.WriteLine("By object type:")
            For Each obj In From o As KeyValuePair(Of Type, Integer) In Test.CountObjectTypes Select o Order By o.Value Descending
                Console.WriteLine("{0}{1}{2}", obj.Value, vbTab, obj.Key.FullName)
            Next
            Console.WriteLine("By attribute type:")
            For Each att In From a As KeyValuePair(Of Type, Integer) In Test.CountAttributeTypes Select a Order By a.Value Descending
                Console.WriteLine("{0}{1}{2}", att.Value, vbTab, att.Key.FullName)
            Next
        End Sub
        ''' <summary>Provides signatore for outputing attribute string representation</summary>
        Dim SignatureProvider As ISignatureProvider = New VisualBasicSignatureProvider

        Private Sub Test_AttributeOk(ByVal sender As Tools.TestsT.AttributeTest, ByVal e As Tools.TestsT.AttributeTest.AttributeEventArgs)
            Console.ForegroundColor = ConsoleColor.Green
            Dim AtText As String
            Try
                AtText = SignatureProvider.GetAttribute(e.CustomAttributeData, SignatureFlags.AllAttributes Or SignatureFlags.LongName Or SignatureFlags.NoMultiline)
            Catch
                AtText = e.Attribute.GetType.FullName
            End Try
            If verbose Then
                Console.WriteLine("Attribute OK: " & AtText)
            Else
                If LastObject IsNot e.Object Then
                    Console.ForegroundColor = ConsoleColor.Blue
                    Console.WriteLine(GetObjectString(e.Object))
                    LastObject = e.Object
                End If
                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine(AtText)
            End If
        End Sub
        ''' <summary>Last object used by <see cref="Test_AttributeOk"/> when <see cref="verbose"/> is false</summary>
        Dim LastObject As ICustomAttributeProvider

        Private Sub Test_Error(ByVal sender As Tools.TestsT.AttributeTest, ByVal e As Tools.TestsT.AttributeTest.AttributeTestErrorEventArgs)
            Console.ForegroundColor = ConsoleColor.Red
            If verbose Then
                Console.WriteLine("Error: {0} {1}", e.Exception.GetType.FullName, e.Exception.Message)
                Console.WriteLine("Stage {0}", e.Stage)
                Console.WriteLine(e.Exception.StackTrace)
                Console.WriteLine("░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒░▒▓█▓▒")
            Else
                Console.WriteLine("Error: {0} {1}; stage {2}", e.Exception.GetType.Name, e.Exception.Message, e.Stage)
            End If
        End Sub
        ''' <summary>Uniquely identifies user-caused cancellation</summary>
        Private oc As New Guid
        ''' <summary>Gets string representation for metetadat object</summary>
        ''' <param name="Object">Object</param>
        ''' <returns>String representation of <paramref name="Object"/></returns>
        Private Function GetObjectString(ByVal [Object] As System.Reflection.ICustomAttributeProvider) As String
            Dim tstr = ""
            If TypeOf [Object] Is Type Then
                tstr = DirectCast([Object], Type).FullName
            ElseIf TypeOf [Object] Is MemberInfo Then
                If DirectCast([Object], MemberInfo).DeclaringType IsNot Nothing Then tstr = DirectCast([Object], MemberInfo).DeclaringType.FullName & "."
                tstr &= DirectCast([Object], MemberInfo).Name
            ElseIf TypeOf [Object] Is Assembly Then
                tstr = DirectCast([Object], Assembly).GetName.ToString
            ElseIf TypeOf [Object] Is [Module] Then
                tstr = DirectCast([Object], [Module]).Name
            ElseIf TypeOf [Object] Is ParameterInfo Then
                tstr = DirectCast([Object], ParameterInfo).Name & " (" & DirectCast([Object], ParameterInfo).Position & ")"
            Else
                tstr = [Object].ToString
            End If
            Return tstr
        End Function
        Private Sub Test_ObjectReached(ByVal sender As Tools.TestsT.AttributeTest, ByVal [Object] As System.Reflection.ICustomAttributeProvider)
            If (Tools.DevicesT.Keyboard.KeyStatus(Keys.Escape) And Windows.Input.KeyStates.Down) = Windows.Input.KeyStates.Down Then _
                Throw New OperationCanceledException(oc.ToString())
            If Not verbose Then Exit Sub
            Console.ForegroundColor = ConsoleColor.White
            Dim tstr As String = GetObjectString([Object])
            Console.WriteLine("Testing {0} {1}", [Object].GetType.Name, tstr)
        End Sub

        Private Sub Test_Warning(ByVal sender As Tools.TestsT.AttributeTest, ByVal e As Tools.TestsT.AttributeTest.WarningEventArgs)
            Console.ForegroundColor = ConsoleColor.Yellow
            Console.WriteLine("Warning: {0}", e.Kind)
        End Sub
    End Module
End Namespace