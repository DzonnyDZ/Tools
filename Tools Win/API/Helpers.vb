Imports System.Runtime.InteropServices, Tools.ExtensionsT
#If True
Namespace API
    ''' <summary>Contains helper functions to deal with Win32 API</summary>
    ''' <version version="1.5.2" stage="Nightly">Module introduced</version>
    Public Module Helpers
        ''' <summary>Determines if Win32 API function is exported</summary>
        ''' <param name="DllImport">The <see cref="DllImportAttribute"/> specifying the function to be imported. <paramref name="DllImport"/>.<see cref="DllImportAttribute.EntryPoint">EntryPoint</see> must be specified.</param>
        ''' <returns>True if such function is specified, false if not</returns>
        ''' <remarks>When <paramref name="DllImport"/>.<see cref="DllImportAttribute.ExactSpelling">ExactSpelling</see> is true, <paramref name="DllImport"/>.<see cref="DllImportAttribute.CharSet">CharSet</see> is ignored (see <see cref="CharSet.None"/> is used).</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="DllImport"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="DllImport"/>.<see cref="DllImportAttribute.EntryPoint"/> is null or an empty string.</exception>
        Public Function IsFunctionExported(ByVal DllImport As DllImportAttribute) As Boolean
            If DllImport Is Nothing Then Throw New ArgumentException("DllImport")
            If DllImport.EntryPoint = "" Then Throw New ArgumentException(ResourcesT.Exceptions.EntryPointMustBeSpecified)
            Return IsFunctionExported(DllImport.Value, DllImport.EntryPoint, If(DllImport.ExactSpelling, CharSet.None, DllImport.CharSet))
        End Function
        ''' <summary>Check is Win32 API method decorated with <see cref="DllImportAttribute"/> exists in exporting library</summary>
        ''' <param name="Method">Method decorated with <see cref="DllImportAttribute"/></param>
        ''' <returns>True if method declaration is present in unmanaged DLL</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Method"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Method"/> is not decorated with <see cref="DllImportAttribute"/></exception>
        Public Function IsFunctionExported(ByVal Method As Reflection.MethodInfo) As Boolean
            If Method Is Nothing Then Throw New ArgumentNullException("Method")
            Dim DllImport = Method.GetAttribute(Of DllImportAttribute)(False)
            If DllImport Is Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.MustBeDecoratedWith1.f("Method", "DllImportAttribute"))
            If DllImport.EntryPoint = "" Then DllImport.EntryPoint = Method.Name
            Return IsFunctionExported(DllImport)
        End Function
        ''' <summary>Checks if WIn32 API metod decorated with <see cref="DllImportAttribute"/> exists in exporting library</summary>
        ''' <param name="Type">Type method is declared in</param>
        ''' <param name="FunctionName">Name of method as declared in <paramref name="Type"/>. Method must be static.</param>
        ''' <param name="BindingFlags">Overrides default binding flags. <see cref="Reflection.BindingFlags.[Static]"/> is not required (it is OR-ed automatically). <see cref="Reflection.BindingFlags.Instance"/> is prohibited.</param>
        ''' <exception cref="Reflection.AmbiguousMatchException">More than one static method with same <paramref name="FunctionName"/> filtered by <paramref name="BindingFlags"/> is found on <paramref name="Type"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null or <paramref name="FunctionName"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="FunctionName"/> is an empty string -or- <paramref name="BindingFlags"/> contains <see cref="Reflection.BindingFlags.Instance"/> -or- Method is not decorated with <see cref="DllImportAttribute"/>.</exception>
        ''' <exception cref="MissingMethodException">Static method with name <paramref name="FunctionName"/> filtered by <paramref name="BindingFlags"/> was not found on type <paramref name="Type"/>.</exception>
        ''' <returns>True if method declaration is present in unmanaged DLL</returns>
        Public Function IsFunctionExported(ByVal Type As Type, ByVal FunctionName As String, Optional ByVal BindingFlags As Reflection.BindingFlags = Reflection.BindingFlags.Public) As Boolean
            If Type Is Nothing Then Throw New ArgumentNullException("Type")
            If FunctionName Is Nothing Then Throw New ArgumentNullException("FunctionName")
            If BindingFlags And Reflection.BindingFlags.Instance Then Throw New ArgumentException(ResourcesT.ExceptionsWin.InstanceAPIMethodsAreNotAllowed)
            If FunctionName = "" Then Throw New ArgumentException(ResourcesT.Exceptions.CannotBeAnEmptyString.f("FunctionName"))
            Dim proc = Type.GetMethod(FunctionName, Reflection.BindingFlags.Static Or BindingFlags)
            If proc Is Nothing Then Throw New MissingMethodException(Type.FullName, FunctionName)
            Return IsFunctionExported(proc)
        End Function
        ''' <summary>Checks if Win32 API method represented by delegate to <see cref="DllImportAttribute"/>-decorated method is accessible</summary>
        ''' <param name="Method">Delegate targeting <see cref="DllImportAttribute"/>-decorated method</param>
        ''' <returns>True if such method is declared in exporting unmanaged DLL</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Method"/> is null</exception>
        ''' <exception cref="ArgumentException">Invocation list of <paramref name="Method"/> ha not exactly one item -or- Method is nod decorated with <see cref="DllImportAttribute"/></exception>
        ''' <exception cref="MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private; see <see cref="[Delegate].Method"/>).</exception>
        Public Function IsFunctionExported(ByVal Method As [Delegate]) As Boolean
            If Method Is Nothing Then Throw New ArgumentNullException("Method")
            If Method.GetInvocationList.Length <> 1 Then Throw New ArgumentException(ResourcesT.Exceptions.InvocationListOfDelegateMustHaveExactlyOneItem)
            Return IsFunctionExported(Method.Method)
        End Function

        ''' <summary>Determines if Win32 API function is exported</summary>
        ''' <param name="ModuleName">Name of module (as <see cref="DllImportAttribute.Value"/>)</param>
        ''' <param name="ProcName">Name of procedure (as <see cref="DllImportAttribute.EntryPoint"/>)</param>
        ''' <param name="CharSet">Character set of procedure (as <see cref="DllImportAttribute.CharSet"/></param>
        ''' <returns>Ture if function <paramref name="ProcName"/> is specidied in <paramref name="ModuleName"/> DLL</returns>
        ''' <remarks>If <paramref name="CharSet"/> is <see cref="CharSet.Ansi"/> <paramref name="ProcName"/> is appended A. If <paramref name="CharSet"/> is <see cref="CharSet.Unicode"/> <paramref name="ProcName"/> is appended W.
        ''' If <paramref name="CharSet"/> is <see cref="CharSet.Auto"/>, both A and W are tried (as well as no suffix). Otherwise function name is tested as passed to <paramref name="ProcName"/> only.</remarks>
        Public Function IsFunctionExported(ByVal ModuleName As String, ByVal ProcName As String, Optional ByVal CharSet As CharSet = Runtime.InteropServices.CharSet.None) As Boolean
            Dim hModule As Integer
            Dim lpProc As Integer
            Dim FreeLib As Boolean
            ' check first to see if the module is already
            ' mapped into this process.
            hModule = GetModuleHandle(ModuleName)
            If hModule = 0 Then
                ' need to load module into this process.
                hModule = LoadLibrary(ModuleName)
                If hModule = 0 Then Return False
                FreeLib = True
            End If
            Try
                ' if the module is mapped, check procedure
                ' address to verify it's exported.
                Select Case CharSet
                    Case Runtime.InteropServices.CharSet.Ansi : ProcName &= "A"
                    Case Runtime.InteropServices.CharSet.Unicode, Runtime.InteropServices.CharSet.Auto : ProcName &= "W"
                End Select
                lpProc = GetProcAddress(hModule, ProcName)
                If lpProc <> 0 Then Return True
                If CharSet = Runtime.InteropServices.CharSet.Auto Then
                    Mid(ProcName, ProcName.Length, 1) = "A"
                    lpProc = GetProcAddress(hModule, ProcName)
                    If lpProc <> 0 Then Return True
                End If
                If CharSet = Runtime.InteropServices.CharSet.Ansi Or CharSet = Runtime.InteropServices.CharSet.Unicode Or CharSet = Runtime.InteropServices.CharSet.Auto Then
                    ProcName = ProcName.Substring(0, ProcName.Length - 1)
                    Return GetProcAddress(hModule, ProcName) <> 0
                End If
            Finally
                ' unload library if we loaded it here.
                If FreeLib Then Call FreeLibrary(hModule)
            End Try
            Return False
        End Function
    End Module
#End If
End Namespace