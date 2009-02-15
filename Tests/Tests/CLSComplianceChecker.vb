Imports Tools.TestsT, System.Linq
Imports System.Reflection

Namespace TestsT
    Public Module CLSComplianceCheckerTest
        Private Statistic As Dictionary(Of CLSComplianceChecker.CLSRule, Integer)
        Public Sub Test()
            Statistic = New Dictionary(Of CLSComplianceChecker.CLSRule, Integer)
            Dim ofd As New OpenFileDialog With {.Filter = "Libraries (*.dll)|*.dll|Executables (*.exe)|*.exe", .Multiselect = True}
            If ofd.ShowDialog <> DialogResult.OK Then Exit Sub
            Dim Checker As New CLSComplianceChecker
            AddHandler Checker.Violation, AddressOf Checker_Violation
            ConsoleT.AllocateConsole()
            Try
                Console.ForegroundColor = ConsoleColor.Gray
                Dim OFC = Console.ForegroundColor
                For Each file In ofd.FileNames
                    Dim asm As Assembly
                    Try
                        asm = Assembly.LoadFile(file)
                    Catch ex As Exception
                        Console.ForegroundColor = ConsoleColor.DarkRed
                        Try : Console.WriteLine("Error while loading assembly {0}: {1} {2}", file, ex.GetType.Name, ex.Message)
                        Finally : Console.ForegroundColor = OFC : End Try
                        Continue For
                    End Try
                    Console.ForegroundColor = ConsoleColor.Green
                    Try : Console.WriteLine("Assembly {0} loaded from {1}.", asm.FullName, file)
                    Finally : Console.ForegroundColor = OFC : End Try
                    Checker.Check(asm)
                    Console.WriteLine() : Console.WriteLine()
                Next
                Console.WriteLine("Statistic:")
                Console.WriteLine("==========")
                Dim CLS% = 0, Errors% = 0, Other% = 0
                Try
                    For Each kwp In From item In Statistic Order By item.Value Descending
                        Select Case kwp.Key
                            Case Is <= -1000 : Console.ForegroundColor = ConsoleColor.DarkRed : Errors += kwp.Value
                            Case Is < 0 : Console.ForegroundColor = ConsoleColor.Yellow : Other += kwp.Value
                            Case Else : Console.ForegroundColor = ConsoleColor.Red : CLS += kwp.Value
                        End Select
                        Console.WriteLine("{0:d}" & vbTab & "{0:f} {1}", kwp.Key, kwp.Value)
                    Next
                    Console.ForegroundColor = OFC
                    Console.WriteLine("Total:")
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine("CLS rules violations total {0}", CLS)
                    Console.ForegroundColor = ConsoleColor.DarkRed
                    Console.WriteLine("Errors total {0}", Errors)
                    Console.ForegroundColor = ConsoleColor.Yellow
                    Console.WriteLine("Other warnings total {0}", Other)
                Finally : Console.ForegroundColor = OFC : End Try
                Console.WriteLine() : Console.WriteLine()
                Console.WriteLine("Press any key to continue...")
                Console.ReadKey()
            Finally
                ConsoleT.DetachConsole()
            End Try
        End Sub
        Private Sub Checker_Violation(ByVal sender As CLSComplianceChecker, ByVal e As CLSComplianceChecker.CLSViolationEventArgs)
            Dim OFC = Console.ForegroundColor
            Select Case e.Rule
                Case Is <= -1000 : Console.ForegroundColor = ConsoleColor.DarkRed
                Case Is < 0 : Console.ForegroundColor = ConsoleColor.Yellow
                Case Else : Console.ForegroundColor = ConsoleColor.Red
            End Select
            Try
                Dim ms = GetMemberString(e.Member, True)
                Console.WriteLine("{0:d} {0:f} {1} {2}{3}", e.Rule, ms, e.Message, If(e.Exception IsNot Nothing, " (" & e.Exception.Message & ")", ""))
                Debug.Print("{0:d} {0:f} {1} {2}{3}", e.Rule, ms, e.Message, If(e.Exception IsNot Nothing, " (" & e.Exception.Message & ")", ""))
            Finally : Console.ForegroundColor = OFC : End Try
            If Statistic.ContainsKey(e.Rule) Then Statistic(e.Rule) += 1 Else Statistic.Add(e.Rule, 1)
        End Sub
        ''' <summary>Gets string representing member</summary>
        ''' <param name="member">Member to represent</param>
        ''' <param name="IncludeType">Include kind of param such as "Asssembly", "Type" etc.</param>
        ''' <param name="AsParam">Only when <paramref name="member"/> is <see cref="Type"/> (otherwise ignored): True when parameter is used as parameter of method/generic type, so if it is generic parameter, declaring method/type will be ommitted.</param>
        Private Function GetMemberString(ByVal member As ICustomAttributeProvider, ByVal IncludeType As Boolean, Optional ByVal AsParam As Boolean = False) As String
            Dim Type$, Name$
            If TypeOf member Is Assembly Then
                Type = "Assembly"
                Name = DirectCast(member, Assembly).FullName
            ElseIf TypeOf member Is [Module] Then
                Type = "Module"
                Name = DirectCast(member, [Module]).Name
            ElseIf TypeOf member Is Type Then
                With DirectCast(member, Type)
                    If .IsGenericParameter Then
                        Type = "Generic parameter"
                        If .DeclaringMethod IsNot Nothing Then
                            Name = If(.FullName, .Name) & If(AsParam, "", " of " & GetMemberString(.DeclaringMethod, False))
                        Else
                            Name = If(.FullName, .Name) & If(AsParam, "", " of " & GetMemberString(.DeclaringType, False))
                        End If
                    ElseIf .IsGenericType Then
                        Type = "Type"
                        Name = If(.FullName, .Name) & "["
                        Dim i% = 0
                        For Each gpar In .GetGenericArguments
                            If i > 0 Then Name &= ","
                            Name &= GetMemberString(gpar, False, True)
                            i += 1
                        Next
                        Name &= "]"
                    ElseIf .IsArray Then
                        Type = "Type"
                        Name = GetMemberString(.GetElementType, False, False) & "[" & New String(","c, .GetArrayRank - 1) & "]"
                    ElseIf .IsByRef Then
                        Type = "Type"
                        Name = GetMemberString(.GetElementType, False, False) & "&"
                    ElseIf .IsPointer Then
                        Type = "Type"
                        Name = GetMemberString(.GetElementType, False, False) & "*"
                    Else
                        Type = "Type"
                        Name = If(.FullName, .Name)
                    End If
                End With
            ElseIf TypeOf member Is PropertyInfo Then
                Type = "Property"
                Name = GetMemberString(DirectCast(member, MemberInfo).DeclaringType, False) & "." & DirectCast(member, MemberInfo).Name
            ElseIf TypeOf member Is FieldInfo Then
                Type = "Field"
                Name = If(DirectCast(member, MemberInfo).DeclaringType IsNot Nothing, GetMemberString(DirectCast(member, MemberInfo).DeclaringType, False) & ".", "") & DirectCast(member, MemberInfo).Name
            ElseIf TypeOf member Is EventInfo Then
                Type = "Event"
                Name = GetMemberString(DirectCast(member, MemberInfo).DeclaringType, False) & "." & DirectCast(member, MemberInfo).Name
            ElseIf TypeOf member Is MethodBase Then
                With DirectCast(member, MethodBase)
                    If .IsConstructor AndAlso .IsStatic Then
                        Type = "Initializer"
                    ElseIf .IsConstructor Then
                        Type = "Constructor"
                    Else
                        Type = "Method"
                    End If
                    Name = If(DirectCast(member, MemberInfo).DeclaringType IsNot Nothing, GetMemberString(DirectCast(member, MemberInfo).DeclaringType, False) & ".", "") & DirectCast(member, MemberInfo).Name & "("
                    Dim i% = 0
                    For Each param In .GetParameters
                        If i > 0 Then Name &= ","
                        Name &= GetMemberString(param.ParameterType, False, True)
                        i += 1
                    Next
                    Name &= ")"
                End With
            ElseIf TypeOf member Is ParameterInfo Then
                With DirectCast(member, ParameterInfo)
                    If .IsRetval Then
                        Type = "Return value"
                        Name = If(.Name = "", "", .Name & " ") & GetMemberString(.Member, True)
                    Else
                        Type = "Parameter"
                        Name = .Name & " of " & GetMemberString(.Member, True)
                    End If
                End With
            Else
                Type = member.GetType.Name
                Name = member.ToString
            End If
            If IncludeType Then Return Type & " " & Name
            Return Name
        End Function
    End Module
End Namespace
