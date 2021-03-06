﻿Imports Mono.Cecil
Imports System.ComponentModel

Namespace RuntimeT.CompilerServicesT
    ''' <summary>Implements assembly post-processor. It walks an assembly, reads attributes decorated with <see cref="PostprocessorAttribute"/> and takes actions based on them.</summary>
    ''' <remarks>
    ''' Assembly post-processing is based on attributes. All metadata items that can hold attributes are processed recursively.
    ''' If an compatible attribute is found a task associated with the attribute is performed.
    ''' Compatible attribute is any custom attribute (theoretically even does not have to derive from <see cref="Attribute"/> class) which is itself decorated with <see cref="PostprocessorAttribute"/> (meaning that the class defining the attribute is decorated with <see cref="PostprocessorAttribute"/>).
    ''' Preferred type of attributes used for this purpose are <see cref="PostprocessingAttribute"/>-derived attributes (but it's absolutely not necessary).
    ''' ĐTools library defines some postprocessing attributes in <see cref="Tools.RuntimeT.CompilerServicesT"/> namespace (e.g. <see cref="MakePublicAttribute"/>).
    ''' You can define your own attributes.
    ''' Attribute and postprocessing task can be very loosely coupled (e.g. live in independent assemblies). For example task defined for attributes in ĐTools library are defined in AssemblyPostprocessor.exe assembly (usually in <see cref="Postprocessors"/> module).
    ''' <para>
    ''' This assembly post-processor depends on <see cref="Mono.Cecil"/> API. Other parts of ĐTools library (including Tools.dll where built-in post-processing attributes are defined) does NOT depend on <see cref="Mono.Cecil"/>. That's why loosely coupling is necessary.
    ''' However it's not required to use loosely coupling for your own postprocessing attributes - you can define the postprocessing code even inside the attribute itself.
    ''' </para>
    ''' <note>Altering strong-named assembly causes it to be invalid. However <see cref="Mono.Cecil"/> and <see cref="AssemblyPostporcessor"/> allows you to re-sign the assembly on save.</note>
    ''' <para>This class uses separate application domain running <see cref="ItemPostprocessor"/> to avoid assembly being locked.</para>
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class AssemblyPostporcessor
        ''' <summary>Entry point method for EXE application</summary>
        Friend Shared Sub Main()
            Console.WriteLine("{0} {1} {2} {3}", My.Application.Info.ProductName, My.Application.Info.Title, My.Application.Info.Version, My.Application.Info.Copyright)
            If My.Application.CommandLineArgs.Count = 0 Then
                Console.WriteLine(My.Resources.Usage, My.Application.Info.AssemblyName)
                Exit Sub
            End If

            Dim postProcessor As New AssemblyPostporcessor
            postProcessor.SetMessageProcessor(Of ConsoleMessageProcesor)()

            Dim awaitSnk = False
            Dim snk As String = Nothing

            For Each param In My.Application.CommandLineArgs
                If param = "/snk" Then awaitSnk = True : Continue For
                If awaitSnk Then snk = param : awaitSnk = False : Continue For
                postProcessor.PostProcess(param, If(String.IsNullOrEmpty(snk), Nothing, snk))
                Console.WriteLine()
            Next

        End Sub


        Private _messageProcessor As Type
        ''' <summary>Sets type which's instance is late used for processing messages and errors</summary>
        ''' <typeparam name="T">Type of message processor</typeparam>
        ''' <remarks>Calling this method repeatedly resets message processor to new value.
        ''' <para>
        ''' Because <see cref="PostProcess"/> launches actual post-processing in separate application domain it's necessary to provide a way of passing message from that app domain to default app domain (or domain <see cref="AssemblyPostporcessor"/> was instantiated in).
        ''' The architecture is following: Instance of type <typeparamref name="T"/> is instantiated in post-processing app domain.
        ''' Instance of <see cref="RuntimeT.CompilerServicesT.MessageReceiver"/>-derived class is instantiated by caller in caller's app domain and set to <see cref="MessageReceiver"/>.
        ''' <typeparamref name="T"/> instance is told about current message receiver by setting it's <see cref="MessageProcessor.Receiver"/> property.
        ''' Message processor's job is to convert information passed to its methods to a form that a) is understood by message receiver b) can pass app domain boundary (i.e. it's serializable, primitive type or <see cref="MarshalByRefObject"/>).
        ''' Message processor can also process the message itself and do not use message receiver. (E.g. <see cref="ConsoleMessageProcesor"/> writes the information to a console and does not use receiver.)
        ''' </para><para>AssemblyPostprocessor may create more than one instance of this class.</para></remarks>
        ''' <seelaso cref="ItemPostprocessor.MessageProcessor"/>
        ''' <seelaso cref="MessageProcessorType"/>
        Public Sub SetMessageProcessor(Of T As {MessageProcessor, New})()
            _messageProcessor = GetType(T)
        End Sub
        ''' <summary>Gets type which's instance becomes message processor</summary>
        ''' <remarks>To set this property call <see cref="SetMessageProcessor"/>.</remarks>
        ''' <seelaso cref="SetMessageProcessor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property MessageProcessorType As Type
            Get
                Return _messageProcessor
            End Get
        End Property
        ''' <summary>Gets or sets instance which receives messages processed by message processor</summary>
        ''' <remarks>Only used if <see cref="MessageProcessor"/> is not null.
        ''' <note>Depending on type of <see cref="MessageProcessorType">message processor</see> this property may be optional, required or ignored (e.g. <see cref="ConsoleMessageProcesor"/> ignores it).</note></remarks>
        Public Property MessageReceiver As MessageReceiver

        ''' <summary>Recursively postprocesses a single assembly and saves changes</summary>
        ''' <param name="filename">Name of file of module from assembly</param>
        ''' <param name="snk">Path to SNK key to (re-)sign assembly with (ignored if null)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="filename"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="filename"/> is an empty string, contains only white space, or contains one or more invalid characters. -or- <paramref name="filename"/> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="filename"/> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
        ''' <exception cref="IO.IOException">An I/O error occurred.</exception>
        ''' <exception cref="IO.DirectoryNotFoundException">The specified <paramref name="filename"/> is invalid, such as being on an unmapped drive.</exception>
        ''' <exception cref="UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified <paramref name="filename"/>.</exception>
        ''' <exception cref="IO.PathTooLongException">The specified <paramref name="filename"/> exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        ''' <exception cref="IO.FileNotFoundException">
        ''' Cannot find assembly for attribute type or required dependent type or parameter type. (Under this circumstances this exception is only thrown if <see cref="MessageProcessorType"/> was not set or <see cref="MessageProcessor.ProcessError"/> returns false.) -or-
        ''' The file <paramref name="filename"/> cannot be found.</exception>
        ''' <exception cref="IO.FileLoadException">An assembly file that was found could not be loaded. (This exception is only thrown if <see cref="MessageProcessorType"/> was not set or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="BadImageFormatException">Attempt to load an invalid assembly -or- Version 2.0 or later of the common language runtime is currently loaded and the assembly was compiled with a later version. (This exception is only thrown if <see cref="MessageProcessorType"/> was not set or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="MemberAccessException"><paramref name="attr"/> is abstract class. (This exception is only thrown if <see cref="MessageProcessorType"/> was not set or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="Reflection.TargetInvocationException">Error while invoking attribute class constructor -or- Error when setting property value for named attribute argument. (This exception is only thrown if <see cref="MessageProcessorType"/> was not set or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="Security.SecurityException">The caller does not have the necessary code access permission. (This exception can be caught if <see cref="MessageProcessorType"/> is set if it does not occur for entire module or assembly.)</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">More than one property found for named attribute argument. (This exception is only thrown if <see cref="MessageProcessorType"/> was not set or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="MissingMemberException">Property or filed for named attribute argument was not found. (This exception is only thrown if <see cref="MessageProcessorType"/> was not set or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="InvalidOperationException">Value named attribute argument cannot be converted to field type, or the property is read-only. (This exception is only thrown if <see cref="MessageProcessorType"/> was not set or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="Exception">Many exceptions can occur in IO sub-system, <see cref="Mono.Cecil"/> or processors. Some of them can be caught if <see cref="MessageProcessorType"/> is set.</exception>
        ''' <remarks>If PDB file exists for assembly given in <paramref name="filename"/> it's updated to reflect code changes.</remarks>
        Public Sub PostProcess(filename$, Optional snk As String = Nothing)

            Dim info = New AppDomainSetup()
            info.ApplicationBase = IO.Path.GetDirectoryName(GetType(ItemPostprocessor).Module.FullyQualifiedName)
            Dim processDomain = AppDomain.CreateDomain("postprocessor", AppDomain.CurrentDomain.Evidence, info)
            Dim assemblyname$ = GetType(ItemPostprocessor).Assembly.FullName
            Dim itemPostprocessorName As String = GetType(ItemPostprocessor).FullName
            Dim oh = processDomain.CreateInstance(
                                      assemblyname, itemPostprocessorName, False,
                                      Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public,
                                      Nothing, New Object() {MessageReceiver, _messageProcessor},
                                      System.Globalization.CultureInfo.CurrentCulture,
                                      New Object() {}
                                     )

            Dim myMsgProcessor As MessageProcessor = Activator.CreateInstance(_messageProcessor)
            myMsgProcessor.Receiver = MessageReceiver

            Dim folder$ = Nothing
            Dim pdbTarget = IO.Path.Combine(IO.Path.GetDirectoryName(filename), IO.Path.GetFileNameWithoutExtension(filename) & ".pdb")
            Try
                folder = DirectCast(oh.Unwrap, ItemPostprocessor).PostProcess(filename, snk)
                Dim modulefile$ = IO.Path.Combine(folder, IO.Path.GetFileName(filename))

                AppDomain.Unload(processDomain)

                IO.File.Copy(modulefile, filename, True)
                myMsgProcessor.ProcessInfo(Nothing, IO.Path.GetFileName(filename))
                Dim pdbsource$ = IO.Path.Combine(folder, IO.Path.GetFileNameWithoutExtension(filename) & ".pdb")
                If IO.File.Exists(pdbsource) Then
                    IO.File.Copy(pdbsource, pdbTarget, True)
                    myMsgProcessor.ProcessInfo(Nothing, IO.Path.GetFileName(pdbTarget))
                End If
            Finally
                If folder IsNot Nothing AndAlso IO.Directory.Exists(folder) Then IO.Directory.Delete(folder, True)
            End Try
        End Sub
    End Class

    ''' <summary>Implements assembly-postprocessor in current application domain</summary>
    ''' <seealso cref="AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class ItemPostprocessor
        Inherits MarshalByRefObject
        Implements IPostprocessorContext
#Region "CTors"
        ''' <summary>CTor - creates a new instance of the <see cref="ItemPostprocessor"/> class with "client/server" logging</summary>
        ''' <param name="messageReceiver">Client to receiver messages processed by <paramref name="messageProcessor"/> instance. This instance is passed to <see cref="MessageProcessor.Receiver"/>.</param>
        ''' <param name="messageProcessor">Type of server which receives messages, processes them and passes them to <paramref name="messageReceiver"/>. Must be <see cref="RuntimeT.CompilerServicesT.MessageProcessor"/>-derived non-abstract class. Ignored if null.</param>
        ''' <exception cref="ArgumentException"><paramref name="messageProcessor"/> is neither null nor <see cref="RuntimeT.CompilerServicesT.MessageProcessor"/>-derived class -or- <paramref name="messageProcessor"/> is not RuntimeType -or- <paramref name="messageProcessor"/> is open generic type.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="messageProcessor"/> is not supported for creating instances by <see cref="Activator.CreateInstance(ActivationContext)"/>.</exception>
        ''' <exception cref="Reflection.TargetInvocationException">Constructor <paramref name="messageProcessor"/> throws an exception</exception>
        ''' <exception cref="MethodAccessException">The caller does not have permission to call <paramref name="messageProcessor"/> constructor.</exception>
        ''' <exception cref="MemberAccessException"><paramref name="messageProcessor"/> represents an abstract class.</exception>
        ''' <exception cref="MissingMethodException"><paramref name="messageProcessor"/> does not have public default constructor.</exception>
        ''' <exception cref="TypeLoadException"><paramref name="messageProcessor"/> is not valid type</exception>
        ''' <remarks>This CTor is used by <see cref="AssemblyPostporcessor"/>.</remarks>
        Public Sub New(messageReceiver As MessageReceiver, messageProcessor As Type)
            If messageProcessor IsNot Nothing Then
                If Not GetType(MessageProcessor).IsAssignableFrom(messageProcessor) Then Throw New ArgumentException(String.Format(My.Resources.Resources.MessageProcessorTypeError, GetType(MessageProcessor).Name, "messageProcessor"))
                _messageProcessor = Activator.CreateInstance(messageProcessor)
                _messageProcessor.Receiver = messageReceiver
            End If
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ItemPostprocessor"/> class</summary>
        ''' <param name="messageProcessor">Instance of object for processing messages (ignored if null)</param>
        Public Sub New(messageProcessor As MessageProcessor)
            _messageProcessor = messageProcessor
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ItemPostprocessor"/> class without logging</summary>
        Public Sub New()
        End Sub
#End Region
        ''' <summary>Recursively postprocesses a single assembly and saves changes</summary>
        ''' <param name="filename">Name of file of module from assembly</param>
        ''' <param name="snk">Path to SNK key to (re-)sign assembly with (ignored if null)</param>
        ''' <returns>
        ''' Name of temporary folder where altered module was saved. Caller is responsible of disposing the folder.
        ''' The folder contains file with same name as file passed in <paramref name="filename"/> and PDB file with same name but PDB extension (only if it existed for original assembly).
        ''' </returns>
        ''' <exception cref="ArgumentNullException"><paramref name="filename"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="filename"/> is an empty string, contains only white space, or contains one or more invalid characters. -or- <paramref name="filename"/> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="filename"/> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
        ''' <exception cref="IO.IOException">An I/O error occurred.</exception>
        ''' <exception cref="IO.DirectoryNotFoundException">The specified <paramref name="filename"/> is invalid, such as being on an unmapped drive.</exception>
        ''' <exception cref="UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified <paramref name="filename"/>.</exception>
        ''' <exception cref="IO.PathTooLongException">The specified <paramref name="filename"/> exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        ''' <exception cref="IO.FileNotFoundException">
        ''' Cannot find assembly for attribute type or required dependent type or parameter type. (Under this circumstances this exception is only thrown if <see cref="MessageProcessor.ProcessError"/> is null or returns false.) -or-
        ''' The file <paramref name="filename"/> cannot be found.</exception>
        ''' <exception cref="IO.FileLoadException">An assembly file that was found could not be loaded. (This exception is only thrown if <see cref="MessageProcessor"/> is null or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="BadImageFormatException">Attempt to load an invalid assembly -or- Version 2.0 or later of the common language runtime is currently loaded and the assembly was compiled with a later version. (This exception is only thrown if <see cref="MessageProcessor"/> is null or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="MemberAccessException"><paramref name="attr"/> is abstract class. (This exception is only thrown if <see cref="MessageProcessor"/> is null or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="Reflection.TargetInvocationException">Error while invoking attribute class constructor -or- Error when setting property value for named attribute argument. (This exception is only thrown if <see cref="MessageProcessor"/> is null or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="Security.SecurityException">The caller does not have the necessary code access permission. (This exception can be caught by <see cref="MessageProcessor"/> if it does not occur for entire module or assembly.)</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">More than one property found for named attribute argument. (This exception is only thrown if <see cref="MessageProcessor"/> is null or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="MissingMemberException">Property or filed for named attribute argument was not found. (This exception is only thrown if <see cref="MessageProcessor"/> is null or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="InvalidOperationException">Value named attribute argument cannot be converted to field type, or the property is read-only. (This exception is only thrown if <see cref="MessageProcessor"/> is null or <see cref="MessageProcessor.ProcessError"/> returns false.)</exception>
        ''' <exception cref="Exception">Many exceptions can occur in IO sub-system, <see cref="Mono.Cecil"/> or processors. Some of them can be caught by <see cref="MessageProcessor"/>.</exception>
        ''' <remarks>
        ''' If PDB file exists for assembly given in <paramref name="filename"/> corresponding PDB files is generated with updated debugging info.
        ''' When this function finishes you can locate it in directory indicated by function return value. It has same name if name of file specified in <paramref name="filename"/> but PDB extension.
        ''' </remarks>
        Public Function PostProcess(filename$, snk$) As String
            Dim rp As New ReaderParameters
            Dim ar As New DefaultAssemblyResolver
            ar.AddSearchDirectory(IO.Path.GetDirectoryName(filename))
            rp.AssemblyResolver = ar
            Dim pdb As Boolean = IO.File.Exists(IO.Path.Combine(IO.Path.GetDirectoryName(filename), IO.Path.GetFileNameWithoutExtension(filename) & ".pdb"))
            rp.ReadSymbols = pdb
            Dim [module] = ModuleDefinition.ReadModule(filename, rp)
            If MessageProcessor IsNot Nothing Then MessageProcessor.ProcessInfo([module].Assembly, My.Resources.msg_ProcessingAssembly)

            Me.ProcessItem([module].Assembly, True)
            For Each mod2 In [module].Assembly.Modules.ToArray
                Me.ProcessItem(mod2, True)
                For Each type In mod2.Types
                    Me.ProcessType(type)
                Next
                Me.ProcessItem(mod2)
            Next
            Me.ProcessItem([module].Assembly)

            Dim tmpFolder = IO.Path.Combine(IO.Path.GetTempPath, Guid.NewGuid.ToString)
            IO.Directory.CreateDirectory(tmpFolder)
            Try
                Dim tmpModule = IO.Path.Combine(tmpFolder, IO.Path.GetFileName(filename))

                Using snkStream = If(snk Is Nothing, Nothing, IO.File.OpenRead(snk))
                    Dim wp As WriterParameters = New WriterParameters
                    If snkStream IsNot Nothing Then
                        wp.StrongNameKeyPair = New Reflection.StrongNameKeyPair(snkStream)
                    End If
                    wp.WriteSymbols = pdb
                    [module].Assembly.Write(tmpModule, wp)
                End Using
                Return tmpFolder
            Catch
                Try : IO.Directory.Delete(tmpFolder, True) : Catch : End Try
                Throw
            End Try
        End Function

        ''' <summary>Passes an informative message to context. This is used to inform about post-processing operation.</summary>
        ''' <param name="item">Current item being processed. May be null. Should be <see cref="T:Mono.Cecil.ICustomAttributeProvider"/> otherwise it is treated as null by implementation.</param>
        ''' <param name="message">The message</param>
        Private Sub LogInfo(item As Object, message As String) Implements IPostprocessorContext.LogInfo
            If MessageProcessor IsNot Nothing Then MessageProcessor.ProcessInfo(TryCast(item, ICustomAttributeProvider), message)
        End Sub

        Private ReadOnly _messageProcessor As MessageProcessor
        ''' <summary>Gets instance that processes messages</summary>
        Public ReadOnly Property MessageProcessor As MessageProcessor
            Get
                Return _messageProcessor
            End Get
        End Property

        ''' <summary>Processes single type (recursive)</summary>
        ''' <param name="type">A type to post-process</param>
        ''' <exception cref="IO.FileNotFoundException">Cannot find assembly for attribute type or required dependent type or parameter type. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="IO.FileLoadException">An assembly file that was found could not be loaded. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="BadImageFormatException">Attempt to load an invalid assembly -or- Version 2.0 or later of the common language runtime is currently loaded and the assembly was compiled with a later version. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="Reflection.TargetInvocationException">Error while invoking attribute class constructor -or- Error when setting property value for named attribute argument. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="Security.SecurityException">The caller does not have the necessary code access permission. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">More than one property found for named attribute argument. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="MissingMemberException">Property or filed for named attribute argument was not found. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="InvalidOperationException">Value named attribute argument cannot be converted to field type, or the property is read-only. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        Public Sub ProcessType(ByVal type As TypeDefinition)
            ProcessItem(type, True)
            If type.HasMethods Then
                For Each mtd In type.Methods.ToArray
                    ProcessItem(mtd, True)
                    If mtd.HasParameters Then
                        For Each par In mtd.Parameters.ToArray
                            ProcessItem(par)
                        Next
                    End If
                    If mtd.HasGenericParameters Then
                        For Each gpar In mtd.GenericParameters.ToArray
                            ProcessItem(gpar)
                        Next
                    End If
                    ProcessItem(mtd.MethodReturnType)
                    ProcessItem(mtd)
                Next
            End If
            If type.HasProperties Then
                For Each prp In type.Properties.ToArray
                    ProcessItem(prp, True)
                    If prp.HasParameters Then
                        For Each par In prp.Parameters
                            ProcessItem(par)
                        Next
                    End If
                    ProcessItem(prp)
                Next
            End If
            If type.HasEvents Then
                For Each evt In type.Events.ToArray
                    ProcessItem(evt)
                Next
            End If
            If type.HasFields Then
                For Each fld In type.Fields.ToArray
                    ProcessItem(fld)
                Next
            End If
            If type.HasNestedTypes Then
                For Each ntp In type.NestedTypes.ToArray
                    ProcessType(ntp)
                Next
            End If
            If type.HasGenericParameters Then
                For Each gpar In type.GenericParameters.ToArray
                    ProcessItem(gpar)
                Next
            End If
            ProcessItem(type)
        End Sub
        ''' <summary>Post-processes single item, non-recursive</summary>
        ''' <param name="item">An item to post-process</param>
        ''' <param name="preorder">Process only attributes specifically declared as pre-order (see <see cref="PostprocessingAttribute.Preorder"/>)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="item"/> is null</exception>
        ''' <exception cref="IO.FileNotFoundException">Cannot find assembly for attribute type or required dependent type or parameter type. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="IO.FileLoadException">An assembly file that was found could not be loaded. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="BadImageFormatException">Attempt to load an invalid assembly -or- Version 2.0 or later of the common language runtime is currently loaded and the assembly was compiled with a later version. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="MemberAccessException"><paramref name="attr"/> is abstract class. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="Reflection.TargetInvocationException">Error while invoking attribute class constructor -or- Error when setting property value for named attribute argument. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="Security.SecurityException">The caller does not have the necessary code access permission. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">More than one property found for named attribute argument. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="MissingMemberException">Property or filed for named attribute argument was not found. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        ''' <exception cref="InvalidOperationException">Value named attribute argument cannot be converted to field type, or the property is read-only. (This exception is only thrown if <see cref="MessageProcessor"/> is null or returns false.)</exception>
        Public Sub ProcessItem(item As ICustomAttributeProvider, Optional preorder As Boolean = False)
            If Not item.HasCustomAttributes Then Return
            Dim attributesToRemove As New List(Of CustomAttribute)
            For Each attr In item.CustomAttributes
                Dim atttype As TypeDefinition
                Try
                    atttype = attr.AttributeType.Resolve
                Catch ex As Exception When MessageProcessor IsNot Nothing
                    If Not MessageProcessor.ProcessError(ex, item) Then Throw Else Continue For
                End Try
                If atttype.HasCustomAttributes Then
                    For Each aa In atttype.CustomAttributes
                        If aa.AttributeType.FullName = GetType(PostprocessorAttribute).FullName Then
                            Try
                                Dim pa As PostprocessorAttribute = InstantiateAttribute(aa)
                                Dim what As PostprocessingAttribute = InstantiateAttribute(attr)
                                If preorder = what.Preorder Then
                                    Dim ppMethod As System.Reflection.MethodInfo = pa.GetMethod()
                                    Dim args As Object() = If(ppMethod.GetParameters.Length = 2, New Object() {item, what}, New Object() {item, what, Me})
                                    If MessageProcessor IsNot Nothing Then MessageProcessor.ProcessInfo(item, String.Format(My.Resources.msg_Apply, attr.AttributeType.Name))
                                    ppMethod.Invoke(Nothing, args)
                                    If TypeOf what Is PostprocessingAttribute AndAlso DirectCast(what, PostprocessingAttribute).Remove Then
                                        attributesToRemove.Add(attr)
                                    End If
                                End If
                            Catch ex As Exception When MessageProcessor IsNot Nothing
                                If Not MessageProcessor.ProcessError(ex, item) Then Throw
                            End Try
                            Exit For
                        End If
                    Next
                End If
            Next
            For Each atr In attributesToRemove
                item.CustomAttributes.Remove(atr)
            Next
        End Sub

        ''' <summary>Creates an instance of attribute defined by <see cref="CustomAttribute"/></summary>
        ''' <param name="attr">An attribute to resolve</param>
        ''' <returns>Instance of an object represented by <paramref name="attr"/>. It's typically instance of <see cref="Attribute"/>-derived class.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="attr"/> is null</exception>
        ''' <exception cref="IO.FileNotFoundException">Cannot find assembly for attribute type or required dependent type or parameter type</exception>
        ''' <exception cref="IO.FileLoadException">An assembly file that was found could not be loaded.</exception>
        ''' <exception cref="BadImageFormatException">Attempt to load an invalid assembly -or- Version 2.0 or later of the common language runtime is currently loaded and the assembly was compiled with a later version.</exception>
        ''' <exception cref="MemberAccessException"><paramref name="attr"/> is abstract class</exception>
        ''' <exception cref="Reflection.TargetInvocationException">Error while invoking attribute class constructor -or- Error when setting property value for named attribute argument.</exception>
        ''' <exception cref="Security.SecurityException">The caller does not have the necessary code access permission.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">More than one property found for named attribute argument</exception>
        ''' <exception cref="MissingMemberException">Property or filed for named attribute argument was not found</exception>
        ''' <exception cref="InvalidOperationException">Value named attribute argument cannot be converted to field type, or the property is read-only</exception>
        Private Shared Function InstantiateAttribute(attr As CustomAttribute) As Object
            If attr Is Nothing Then Throw New ArgumentNullException("attr")
            Dim attrType = CecilHelpers.[GetType](attr.AttributeType)
            Dim ctor = attrType.GetConstructor(If(attr.Constructor.HasParameters, (From cpar In attr.Constructor.Parameters Select CecilHelpers.[GetType](cpar.ParameterType)).ToArray, Type.EmptyTypes))
            Dim ctorArgs = (From a In attr.ConstructorArguments
                            Select If(TypeOf a.Value Is TypeReference,
                                     CecilHelpers.[GetType](DirectCast(a.Value, TypeReference)),
                                   If(TypeOf a.Value Is TypeReference(),
                                      (From itm In DirectCast(a.Value, TypeReference()) Select If(itm Is Nothing, Nothing, CecilHelpers.[GetType](itm))).ToArray,
                                      a.Value))
                           ).ToArray
            Dim ainst = ctor.Invoke(ctorArgs)
            If attr.HasProperties Then
                For Each prp In attr.Properties
                    Dim propertyToSet = attrType.GetProperty(prp.Name, Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic, Nothing, Nothing, Type.EmptyTypes, New System.Reflection.ParameterModifier() {})
                    If propertyToSet Is Nothing Then
                        Dim fldToSet = attrType.GetField(prp.Name, Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)
                        If fldToSet IsNot Nothing Then
                            Try
                                fldToSet.SetValue(ainst, prp.Argument.Value)
                            Catch ex As ArgumentException
                                Throw New InvalidOperationException(ex.Message, ex)
                            End Try
                        Else
                            Throw New MissingMemberException(String.Format(My.Resources.ex_PropertyOrFieldNotFound, prp.Name))
                        End If
                    Else
                        Try
                            If prp.Argument.Type.IsArray AndAlso TypeOf prp.Argument.Value Is CustomAttributeArgument() Then
                                Dim values As CustomAttributeArgument() = prp.Argument.Value
                                Dim arr = Array.CreateInstance(CecilHelpers.GetType(prp.Argument.Type).GetElementType, values.Length)
                                For i = 0 To values.Length - 1
                                    arr.SetValue(values(i).Value, i)
                                Next
                                propertyToSet.SetValue(ainst, arr, New Object() {})
                            Else
                                propertyToSet.SetValue(ainst, prp.Argument.Value, New Object() {})
                            End If
                        Catch ex As ArgumentException
                            Throw New InvalidOperationException(ex.Message, ex)
                        End Try
                    End If
                Next
            End If
            Return ainst
        End Function
    End Class

    ''' <summary>Processes messages from post-processing process and passes them to <see cref="MessageReceiver"/></summary>
    ''' <remarks>The job of this class is to convert messages from postprocessing to a form which a) the message is understood by associated message receiver b) the message can pass app-domain boundary.</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public MustInherit Class MessageProcessor
        Inherits MarshalByRefObject

        ''' <summary>Gets or sets instance of <see cref="MessageReceiver"/>-derived class to pass messages to</summary>
        ''' <remarks>Can be null.
        ''' <para>Messages must be passes in a for in which they can pass application domain boundary.</para></remarks>
        ''' <exception cref="ArgumentException">When overridden in derived class derived class implementation can throw this exception in it does not accept receiver being set.</exception>
        Public Overridable Property Receiver As MessageReceiver


        ''' <summary>When overridden in derived class processes an error message</summary>
        ''' <param name="ex">An exception which occurred</param>
        ''' <param name="item">Item which caused the exception (may be null)</param>
        ''' <returns>True if processing should continue</returns>
        Public MustOverride Function ProcessError(ex As Exception, item As ICustomAttributeProvider) As Boolean
        ''' <summary>When overridden in derived class processes a info message</summary>
        ''' <param name="item">Item that caused the message (may be null)</param>
        ''' <param name="message">Message</param>
        Public MustOverride Sub ProcessInfo(item As ICustomAttributeProvider, message As String)
    End Class

    ''' <summary>Base class for message receivers</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public MustInherit Class MessageReceiver
        Inherits MarshalByRefObject
    End Class

    ''' <summary>Processes message to console</summary>
    ''' <remarks>This message processor does not user message receiver</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class ConsoleMessageProcesor
        Inherits MessageProcessor

        ''' <summary>Processes an error message</summary>
        ''' <param name="ex">An exception which occurred</param>
        ''' <param name="item">Item which caused the exception (may be null)</param>
        ''' <returns>True if processing should continue. This implementation always returns true.</returns>
        Public Overrides Function ProcessError(ex As System.Exception, item As Mono.Cecil.ICustomAttributeProvider) As Boolean
            Dim name = item.ToString
            Console.Error.WriteLine(My.Resources.msg_ItemError, ex.GetType.Name, name, ex.Message)
            Console.Error.WriteLine(ex.StackTrace)
            ex = ex.InnerException
            While ex IsNot Nothing
                Console.Error.WriteLine(My.Resources.msg_InnerException)
                Console.Error.WriteLine("{0}: {1}", ex.GetType.Name, ex.Message)
                Console.WriteLine(ex.StackTrace)
                ex = ex.InnerException
            End While
            Console.WriteLine()
            Return True
        End Function

        ''' <summary>Processes a info message</summary>
        ''' <param name="item">Item that caused the message (may be null)</param>
        ''' <param name="message">Message</param>
        Public Overrides Sub ProcessInfo(item As Mono.Cecil.ICustomAttributeProvider, message As String)
            Console.WriteLine("{0}: {1}", item, message)
        End Sub
    End Class

End Namespace