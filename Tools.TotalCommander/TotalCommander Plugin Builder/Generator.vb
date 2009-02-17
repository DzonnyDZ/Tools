Imports System.Reflection, Tools.ExtensionsT, Tools.ReflectionT, System.CodeDom

''' <summary>Generates a plugin</summary>
Public Class Generator
    ''' <summary>CTor from assembly</summary>
    ''' <param name="Assembly">Assembly to generate plugin for</param>
    ''' <exception cref="ArgumentNullException"><paramref name="Assembly"/> is null</exception>
    ''' <remarks><see cref="OutputDirectory"/> is initilaized to <see cref="IO.Path.GetDirectoryName"/>(<paramref name="Assembly"/>.<see cref="Assembly.Location">Location</see>) when <paramref name="Assembly"/>.<see cref="Assembly.Location">Location</see> is not null; to <see cref="Environment.CurrentDirectory"/> otherwise.</remarks>
    Public Sub New(ByVal Assembly As Assembly)
        Me.Assembly = Assembly
        If Assembly.Location Is Nothing Then
            OutputDirectory = Environment.CurrentDirectory
        Else
            OutputDirectory = IO.Path.GetDirectoryName(Assembly.Location)
        End If
    End Sub
    ''' <summary>CTor from assembly and output directory</summary>
    ''' <param name="Assembly">Assembly to generate plugin for</param>
    ''' <param name="OutputDirectory">Output directory to generate plugin into</param>
    ''' <exception cref="ArgumentNullException"><paramref name="Assembly"/> or <paramref name="OutputDirectory"/> is null</exception>
    Public Sub New(ByVal Assembly As Assembly, ByVal OutputDirectory As String)
        Me.Assembly = Assembly
        Me.OutputDirectory = OutputDirectory
    End Sub
    ''' <summary>CTro from type and output directory</summary>
    ''' <param name="Type">Type to generate plugin for. Type must represent Total Commander plugin class - this is not checked in CTor but later.</param>
    ''' <param name="OutputDirectory">Output directory to generate plugin into</param>
    ''' <exception cref="ArgumentNullException"><paramref name="Type"/> or <paramref name="OutputDirectory"/> is null</exception>
    Public Sub New(ByVal Type As Type, ByVal OutputDirectory As String)
        If Type Is Nothing Then Throw New ArgumentNullException("Type")
        Assembly = Type.Assembly
        Types = New Type() {Type}
        OutputDirectory = OutputDirectory
    End Sub
    ''' <summary>Contains value of the <see cref="Assembly"> property</see></summary>
    Private _Assembly As Assembly
    ''' <summary>Gets or sets assembly to generate plugin for</summary>
    ''' <exception cref="ArgumentNullException">Value beign set is null</exception>
    Public Property Assembly() As Assembly
        Get
            Return _Assembly
        End Get
        Set(ByVal value As Assembly)
            If value Is Nothing Then Throw New ArgumentNullException("value")
            _Assembly = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="ProjectTemplateDirectory"/> property</summary>
    Private _ProjectTemplateDirectory As String
    ''' <summary>Gets or sets directory to use project template from</summary>
    ''' <remarks>When null, build-in template is used</remarks>
    Public Property ProjectTemplateDirectory() As String
        Get
            Return _ProjectTemplateDirectory
        End Get
        Set(ByVal value As String)
            _ProjectTemplateDirectory = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="OutputDirectory"/> property</summary>
    Private _OutputDirectory As String
    ''' <summary>Gets or sets output directory to wtire plugin into</summary>
    ''' <exception cref="ArgumentNullException">Value being set is null</exception>
    Public Property OutputDirectory() As String
        Get
            Return _OutputDirectory
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then Throw New ArgumentNullException("value")
            _OutputDirectory = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="IntermediateDirectory"/> property</summary>
    Private _IntermediateDirectory$
    ''' <summary>Gets or sets intermediate directory where temporary files are stored</summary>
    ''' <returns>Intermediate directory to store temporary files in</returns>
    ''' <value>Intermediate directory to store temporary files in. Null to chose directory automatically</value>
    ''' <remarks>{0} is replaced by plugin name. When such directory does not exist, it is created. Setting this property to null, sets <see cref="CleanIntermediateDirectory"/> to true.</remarks>
    Public Property IntermediateDirectory() As String
        Get
            Return _IntermediateDirectory
        End Get
        Set(ByVal value As String)
            _IntermediateDirectory = value
            If value Is Nothing Then CleanIntermediateDirectory = True
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="CleanIntermediateDirectory"/> property</summary>
    Private _CleanIntermediateDirectory As Boolean = True
    ''' <summary>Gets or sets value indicating if intermediate directory specified in <see cref="IntermediateDirectory"/> is deled after plugin generation</summary>
    ''' <returns>True when directory will be deleted after plugin generation, false when intermediate files will remain after generation.</returns>
    ''' <value>True to ensure that intermediate directory is deleted after compile, false to let files in intermediate directory. This property cannot be false when <see cref="IntermediateDirectory"/> is null.</value>
    ''' <exception cref="InvalidOperationException">Value is being set to false when <see cref="IntermediateDirectory"/> is null</exception>
    Public Property CleanIntermediateDirectory() As Boolean
        Get
            Return _CleanIntermediateDirectory
        End Get
        Set(ByVal value As Boolean)
            If Not value AndAlso IntermediateDirectory Is Nothing Then Throw New ArgumentNullException(String.Format(My.Resources.XcannotbeYwhenZisW, "CleanIntermediateDirectory", "false", "IntermediateDirectory", "null"))
            _CleanIntermediateDirectory = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="Types"/> property</summary>
    Private _Types As Type()
    ''' <summary>Gets or sets types to generate plugins for</summary>
    ''' <value>All the types in array must be in assembly <see cref="Assembly"/>; when null all public types that qualify to be Total Commander plugins will be used.</value>
    ''' <returns>Type plugins will be generated for. Null when all possible types will be used.</returns>
    ''' <remarks>When null, this property is set during generation process to actual types plugins are generated for.
    ''' <para>Passing constructed generic type here is only way to generate plugin of generic type. Otherwise plugin generator skips generic types.</para></remarks>
    Public Property Types() As Type()
        Get
            Return _Types
        End Get
        Set(ByVal value As Type())
            _Types = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="Filter"/> property</summary>
    Private _Filer As PluginType = PluginType.Content Or PluginType.FileSystem Or PluginType.Lister Or PluginType.Packer
    ''' <summary>Gets or sets plugin type filter. Only plugins of types according to given OR-mask will be generated.</summary>
    ''' <returns>OR-ed values of <see cref="PluginType"/> type indicating which plugin types are generated</returns>
    ''' <value>Set plugin types to generate plugins for. By default all the plugin types are generated.</value>
    Public Property Filer() As PluginType
        Get
            Return _Filer
        End Get
        Set(ByVal value As PluginType)
            _Filer = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="RenamingDictionary"/> property</summary>
    Private _RenamingDictionary As New Dictionary(Of String, String)
    ''' <summary>Dictionary containing <see cref="Type.FullName"/>s as key and plugin files names (witout extension) as values.</summary>
    ''' <remarks>Generated plugin files are named according to this dictionary. When dictionary entry for any type is missing, plugin is named according to full name of type.
    ''' <para><see cref="RenamingDictionary"/> takes precedence to <see cref="TotalCommanderPluginAttribute.Name"/>.</para></remarks>
    Public ReadOnly Property RenamingDictionary() As Dictionary(Of String, String)
        Get
            Return _RenamingDictionary
        End Get
    End Property

    ''' <summary>Generates plugins</summary>
    Public Sub Generate()
        If Types Is Nothing Then
            Dim iTypes As New List(Of Type)
            For Each Type In Assembly.GetTypes                       'TODO: All plugin types
                If Type.IsPublic OrElse Type.IsNestedPublic AndAlso GetType(FileSystemPlugin).IsAssignableFrom(Type) AndAlso Not Type.IsAbstract AndAlso Not Type.IsGenericTypeDefinition AndAlso Type.HasDefaultCTor Then
                    iTypes.Add(Type)
                End If
            Next
            Types = iTypes.ToArray
        End If
        Dim Ret As New List(Of String)
        For Each Type In Types
            Generate(Type)
        Next
    End Sub
    ''' <summary>Contains value of the <see cref="LogToConsole"/> property</summary>
    Private _LogToConsole As Boolean = False
    ''' <summary>Gets or sets value indicationg if generator will log its progress to console</summary>
    Friend Property LogToConsole() As Boolean
        Get
            Return _LogToConsole
        End Get
        Set(ByVal value As Boolean)
            _LogToConsole = value
        End Set
    End Property

    ''' <summary>Log a log information</summary>
    ''' <param name="Text">Formating string</param>
    ''' <param name="Obj">Format patameters</param>
    ''' <seelaso cref="System.String.Format"/>
    Private Sub Log(ByVal Text$, ByVal ParamArray Obj As Object())
        If LogToConsole Then Console.WriteLine(Text, Obj)
    End Sub

    ''' <summary>Generates plugin from type</summary>
    ''' <param name="Type">Type to generate plugin for</param>
    ''' <returns>Path to generated plugin file</returns>
    Private Function Generate(ByVal Type As Type) As String
        Dim Thrown As Exception = Nothing
        Log("Creating plugin for type {0}.", Type.Name)
        If Type Is Nothing Then Throw New ArgumentNullException("Type")
        If Type.IsGenericTypeDefinition Then Throw New ArgumentException(My.Resources.e_OpenGeneric.f(Type.FullName))
        If Type.IsAbstract Then Throw New ArgumentException(My.Resources.e_Abstract.f(Type.FullName))
        If Not Type.HasDefaultCTor Then Throw New ArgumentException(My.Resources.e_NoDefaultCTor.f(Type.FullName))
        If Not Type.IsPublic AndAlso Not Type.IsNestedPublic Then Throw New ArgumentException(My.Resources.e_NotPublic.f(Type.FullName))
        'Set directory
        Dim IntermediateDirectory$
        If Me.IntermediateDirectory Is Nothing Then
            Dim TempPath$
            Dim i% = 1
            Do
                TempPath = IO.Path.Combine(IO.Path.GetTempPath, String.Format("TCPluginGenerator_{0}_{1}", GetSafeName(Type.FullName), i))
                i += 1
            Loop While IO.Directory.Exists(TempPath) OrElse IO.File.Exists(TempPath)
            Log("Creating temporary intermediate directory {0}", IntermediateDirectory)
            IntermediateDirectory = TempPath
        Else
            IntermediateDirectory = String.Format(Me.IntermediateDirectory, GetSafeName(Type.FullName))
            If IO.File.Exists(IntermediateDirectory) Then IO.File.Delete(IntermediateDirectory)
            If IO.Directory.Exists(IntermediateDirectory) Then
                Log("Using predefined intermediate directory {0}.", IntermediateDirectory)
                Try
                    For Each file In IO.Directory.GetFiles(IntermediateDirectory)
                        IO.File.Delete(file)
                    Next
                    For Each SubDir In IO.Directory.GetDirectories(IntermediateDirectory)
                        IO.Directory.Delete(SubDir, True)
                    Next
                Catch ex As Exception
                    Log("Error while clening intermediate directory.")
                    Throw
                End Try
            Else
                Log("Creating user-defined intermediate directory {0}", IntermediateDirectory)
            End If
        End If
        If Not IO.Directory.Exists(IntermediateDirectory) Then IO.Directory.CreateDirectory(IntermediateDirectory)
        Try
            'Copy template
            Dim ProjectDirectory = IO.Path.Combine(IntermediateDirectory, "Project")
            Log("Create project directory {0}", ProjectDirectory)
            IO.Directory.CreateDirectory(ProjectDirectory)
            If Me.ProjectTemplateDirectory IsNot Nothing Then
                Log("Copy template from {0}", Me.ProjectTemplateDirectory)
                My.Computer.FileSystem.CopyDirectory(Me.ProjectTemplateDirectory, ProjectDirectory)
            Else
                Log("Extract built-in template", Me.ProjectTemplateDirectory)
                'TODO: Template
                Throw New NotImplementedException("Currently project template directory must be set. Feature versions will be able to operate wizthout given template directory.")
            End If
            'Prepare projet
            If GetType(FileSystemPlugin).IsAssignableFrom(Type) Then
                Log("Plugin type: File System Plugin (wfx)")
                PrepareWfx(Type, ProjectDirectory)
            Else 'TODO: Support all plugin types
                Throw New ArgumentException(My.Resources.e_NotAPluginType.f(Type.FullName))
            End If
            'Compile
            'Copy result
        Catch ex As Exception
            Thrown = ex
            Throw
        Finally
            If CleanIntermediateDirectory Then
                Log("Removing intermediate directory {0}", IntermediateDirectory)
                Try
                    IO.Directory.Delete(IntermediateDirectory, True)
                Catch ex As Exception
                    Log("Error while removing intermediate directory {0}", ex.Message)
                    If Thrown Is Nothing Then Throw
                End Try
            End If
        End Try
    End Function
    ''' <summary>Removes file-name-invalid characters from string</summary>
    ''' <param name="Name">String to remove chaRACTERS FROM</param>
    ''' <returns><paramref name="Name"/> with <see cref="IO.Path.GetInvalidFileNameChars"/> removed</returns>
    Private Function GetSafeName(ByVal Name$) As String
        For Each forbidden In IO.Path.GetInvalidFileNameChars
            Name = Name.Replace(forbidden, "")
        Next
        Return Name
    End Function
    ''' <summary>Prepares files for wfx plugin</summary>
    ''' <param name="Type">Type to prepare project for</param>
    ''' <param name="ProjectDirectory">Directory peoject is stored in</param>
    Private Sub PrepareWfx(ByVal Type As Type, ByVal ProjectDirectory$)
        Using defineh = IO.File.Open(IO.Path.Combine(ProjectDirectory, "define.h"), IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read), _
                w As New IO.StringWriter(defineh)
            w.WriteLine("#define TC_WFX " & GetTypeSignature(Type))
            For Each method In GetType(FileSystemPlugin).GetMethods(BindingFlags.Instance Or BindingFlags.Public)
                Dim attr = method.GetAttribute(Of PluginMethodAttribute)(False)
                Dim Define As Boolean
                If attr.DefinedBy Is Nothing Then
                    Define = True
                Else
                    Dim DefinedByBase = GetType(FileSystemPlugin).GetMethod(attr.DefinedBy, BindingFlags.Public Or BindingFlags.Instance)
                    Dim DefinedBy = DefinedByBase.GetOverridingMethod(Type) 'TODO: Implement
                    Dim NotSupported = DefinedBy.GetAttribute(Of MethodNotSupportedAttribute)(False)
                    Define = NotSupported Is Nothing
                End If
                If Define Then
                    w.WriteLine("#define " & attr.DefinedBy)
                End If
            Next
        End Using
    End Sub
    ''' <summary>C++ code provider</summary>
    Private Provider As New Microsoft.VisualC.CppCodeProvider
    ''' <summary>Gets expression to ceate instance of type in C++</summary>
    ''' <param name="Type">Type to get expression for</param>
    Private Function GetTypeSignature(ByVal Type As Type) As String
        Dim b As New System.Text.StringBuilder
        Dim w As New IO.StringWriter(b)
        Provider.GenerateCodeFromExpression(New CodeObjectCreateExpression(Type, New CodeExpression() {}), w, New Compiler.CodeGeneratorOptions())
        w.Flush()
        Return b.ToString
    End Function
End Class
