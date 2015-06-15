Imports System.Data.SqlClient
Imports Microsoft.Data.Schema.Build
Imports Microsoft.Data.Schema
Imports Microsoft.Build.Evaluation
Imports Microsoft.Data.Schema.Extensibility
Imports System.Reflection
Imports System.ComponentModel
Imports Microsoft.Data.Schema.SchemaModel
Imports System.Xml
Imports System.Collections.ObjectModel

''' <summary>Encapsulates functionality of programmatic deployment of database schema to MIcrosft SQL Server database</summary>
''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
Public Class DatabaseDeployment
    Implements IDisposable
#Region "Fields"
    Private _connectionString As String
    Private _databaseName As String
    Private _manifestPath As String
    Private ReadOnly _Properties As New Dictionary(Of String, String)
    Private _deployScriptPath$
    ''' <summary>Indicates if deployment script will be deleted once this instance is disposed</summary>
    ''' <remarks>Deployment script is deleted when value of the <see cref="DeployScriptPath"/> was not provided externally</remarks>
    Private _deleteScriptOnDispose As Boolean = False
    Private ReadOnly _Deployed As Boolean
    Private _errors As New List(Of DataSchemaError)
#End Region

#Region "CTors"
    ''' <summary>Encapsulates CTor functionality</summary>
    ''' <param name="connectionString">Connection string to Microsoft SQL Server</param>
    ''' <param name="databaseName">Name of target database to deploy to</param>
    ''' <param name="manifestPath">Path to database deployment manifest (*.deploymanifest) file</param>
    ''' <exception cref="ArgumentNullException"><paramref name="connectionString"/>, <paramref name="databaseName"/> or <paramref name="manifestPath"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="connectionString"/>, <paramref name="databaseName"/> or <paramref name="manifestPath"/> is an empty string</exception>
    ''' <remarks>The folder deployment manifest is located in must contain all other files required by deployment process.</remarks>
    Private Sub Init(ByVal connectionString$, ByVal databaseName$, ByVal manifestPath$)
        If connectionString Is Nothing Then Throw New ArgumentNullException("connectionString")
        If databaseName Is Nothing Then Throw New ArgumentNullException("databaseName")
        If manifestPath Is Nothing Then Throw New ArgumentNullException("manifestPath")
        If connectionString = "" Then Throw New ArgumentException(String.Format(My.Resources.err_CannotBeEmptyString, "connectionString"), "connectionString")
        If databaseName = "" Then Throw New ArgumentException(String.Format(My.Resources.err_CannotBeEmptyString, "databaseName"), "databaseName")
        If manifestPath = "" Then Throw New ArgumentException(String.Format(My.Resources.err_CannotBeEmptyString, "manifestPath"), "manifestPath")
        _connectionString = connectionString
        _databaseName = databaseName
        _manifestPath = manifestPath
    End Sub
    ''' <summary>CTor - creates a new instance of the <see cref="DatabaseDeployment"/> class</summary>
    ''' <param name="connectionString">Connection string to Microsoft SQL Server</param>
    ''' <param name="databaseName">Name of target database to deploy to</param>
    ''' <param name="manifestPath">Path to database deployment manifest (*.deploymanifest) file</param>
    ''' <exception cref="ArgumentNullException"><paramref name="connectionString"/>, <paramref name="databaseName"/> or <paramref name="manifestPath"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="connectionString"/>, <paramref name="databaseName"/> or <paramref name="manifestPath"/> is an empty string</exception>
    ''' <remarks>The folder deployment manifest is located in must contain all other files required by deployment process.</remarks>
    Public Sub New(ByVal connectionString$, ByVal databaseName$, ByVal manifestPath$, deployed As Boolean)
        _Deployed = deployed
        Init(connectionString, databaseName, manifestPath)
    End Sub
    ''' <summary>CTor - creates a new instance of the <see cref="DatabaseDeployment"/> class. Name of target database if inferred from connection string.</summary>
    ''' <param name="connectionString">Connection string to Microsoft SQL Server. Property <c>Initial Catalog</c> must contain name of database to deploy schema to</param>
    ''' <param name="manifestPath">Path to database deployment manifest (*.deploymanifest) file</param>
    ''' <exception cref="ArgumentNullException"><paramref name="connectionString"/> or <paramref name="manifestPath"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="connectionString"/> or <paramref name="manifestPath"/> is an empty string -or- <paramref name="connectionString"/> is an invalid connection string. -or- Initial Catalog not not cpecified in <paramref name="connectionString"/>.</exception>
    ''' <exception cref="KeyNotFoundException"><paramref name="connectionString"/> contains unrecognized option</exception>
    ''' <exception cref="FormatException"><paramref name="connectionString"/> contains an invalid value (specifically when numeric or boolean value is expected but not specified)</exception>
    ''' <remarks>The folder deployment manifest is located in must contain all other files required by deployment process.</remarks>
    ''' <seealso cref="SqlConnectionStringBuilder.InitialCatalog"/>
    Public Sub New(ByVal connectionString$, ByVal manifestPath$, deployed As Boolean)
        _Deployed = deployed
        If connectionString Is Nothing Then Throw New ArgumentNullException("connectionString")
        If connectionString = "" Then Throw New ArgumentException(String.Format(My.Resources.err_CannotBeEmptyString, "connectionString"), "connectionString")
        Init(connectionString, New SqlConnectionStringBuilder(connectionString).InitialCatalog, manifestPath)
    End Sub
#End Region

#Region "Properties"
    ''' <summary>Gets a connection string used to connect to database</summary>
    Public ReadOnly Property ConnectionString() As String
        Get
            Return _connectionString
        End Get
    End Property
    ''' <summary>Gets name of database to deploy schema to</summary>
    Public ReadOnly Property DatabaseName() As String
        Get
            Return _databaseName
        End Get
    End Property
    ''' <summary>Gets path of database deployment manifest (*.deploymanifest) file</summary>
    Public ReadOnly Property ManifestPath() As String
        Get
            Return _manifestPath
        End Get
    End Property
    ''' <summary>Gets dictionary containing names and values of additional deployment properties</summary>
    Public ReadOnly Property Properties() As Dictionary(Of String, String)
        Get
            Return _Properties
        End Get
    End Property
    ''' <summary>Gets or sets value indicating if model is deployed to database or only deployment script is generated</summary>
    ''' <value>True to deploy model to database, false to generate deployment script file only</value>
    <DefaultValue(True)>
    Public Property DeployToDatabase As Boolean = True

    ''' <summary>Gets or sets path to save deploy script to</summary>
    ''' <exception cref="ObjectDisposedException"><see cref="Deployed"/> is true (this instance has already been used to deploy a database).</exception>
    ''' <remarks>When not set before the <see cref="Deploy"/> method is called, script is stored to a temporary directory and deleted when object is disposed.</remarks>
    Public Property DeployScriptPath$
        Get
            Return _deployScriptPath
        End Get
        Set(ByVal value$)
            If Deployed Then Throw New ObjectDisposedException(String.Format(My.Resources.err_CannotChangeValueOnceDeployed, "DeployScriptPath"))
            _deployScriptPath = value
        End Set
    End Property
    ''' <summary>Contains value indicating deployment was already attempted or not</summary>
    ''' <remarks>One instance of <see cref="DatabaseDeployment"/> class can be used only to one attempt to deploy a database</remarks>
    Public ReadOnly Property Deployed() As Boolean
        Get
            Return _Deployed
        End Get
    End Property
    ''' <summary>Once deployment has started gets all collected errors, warnings and messages of deploy engine</summary>
    ''' <exception cref="ObjectDisposedException">This instance has been already disposed</exception>
    Public ReadOnly Property Errors() As ReadOnlyCollection(Of DataSchemaError)
        Get
            If disposedValue Then Throw New ObjectDisposedException(My.Resources.err_ObjectDisposed)
            Return _errors.AsReadOnly
        End Get
    End Property

#End Region

#Region "Events"
    ''' <summary>Raised when an error occurs or a message is generated by deployment engine</summary>
    ''' <remarks>
    ''' When this event is faired it does not necessarily mean that error has occurred. Check <see cref="DataSchemaErrorEventArgs.[Error]"/>.<see cref="DataSchemaError.Severity">Severity</see>.
    ''' <para>Throw any exception from handler of this event to immediately break execution.</para>
    ''' </remarks>
    ''' <seelaso cref="DataSchemaError"/>
    Public Event ErrorOccured As EventHandler(Of DataSchemaErrorEventArgs)
    ''' <summary>Raises the <see cref="ErrorOccured"/> event</summary>
    ''' <param name="e">Event argument</param>
    Protected Overridable Sub OnErrorOccured(ByVal e As DataSchemaErrorEventArgs)
        RaiseEvent ErrorOccured(Me, e)
    End Sub
#End Region

#Region "Deploy"
    ''' <summary>Deploys database schema to target database</summary>
    ''' <exception cref="InvalidOperationException">This instance has already been used to attempt to deploy a database schema -or-
    ''' <see cref="ManifestPath"/> does not exist -or-
    ''' A property from the <see cref="Properties"/> dictionary was not recognized</exception>
    ''' <exception cref="DeploymentFailedException">An error ocuured while deploying schema to database</exception>
    ''' <remarks>The <see cref="Deploy"/> method can be called only once for each instance of the <see cref="DatabaseDeployment"/> class</remarks>
    Public Sub Deploy()
        If Deployed Then Throw New InvalidOperationException(My.Resources.err_UsedInstanceUsedAgain)
        If Not IO.File.Exists(ManifestPath) Then Throw New InvalidOperationException(My.Resources.err_ManifestFileNotFound, New IO.FileNotFoundException(My.Resources.err_ManifestFileNotFound, ManifestPath))

        'Initialize error handling
        Dim handler As EventHandler(Of DeploymentContributorEventArgs) = Nothing
        Dim manager As New ErrorManager
        Dim errorCount As Integer = 0
        AddHandler manager.ErrorsChanged,
            Sub(sender, args)
                Dim [error] As DataSchemaError
                For Each [error] In args.ErrorsAdded
                    If Not [error].GetType.FullName = "Microsoft.Data.Schema.SchemaModel.ExternalElementResolutionError" Then
                        If [error].Severity = ErrorSeverity.Error Then errorCount += 1
                        _Errors.Add([error])
                        OnErrorOccured([error])
                    End If
                Next
            End Sub
        Dim engine As SchemaDeployment = Nothing
        Try
            Dim [set] As HashSet(Of String) = Nothing
            'Load manifest
            Dim manifest As Project = Nothing
            Dim manifestFile As IO.FileInfo = Nothing
            manifestFile = New IO.FileInfo(ManifestPath)
            manifest = LoadManifest(manifestFile)
            Dim dbSchemaFile As IO.FileInfo = Nothing


            'Load model
            Dim propertyValue As String = manifest.GetPropertyValue("SourceModel")
            If Not String.IsNullOrEmpty(propertyValue) Then
                dbSchemaFile = New IO.FileInfo(IO.Path.Combine(manifestFile.DirectoryName, propertyValue))
                If Not dbSchemaFile.Exists Then
                    Throw New IO.FileNotFoundException(String.Format(My.Resources.err_DatabaseSchemaFileDoesNotExist, dbSchemaFile.FullName))
                    dbSchemaFile = Nothing
                End If
            End If

            'Initialize service & extensions
            Dim em As ExtensionManager = LoadExtensionManagerFromDBSchema(dbSchemaFile)
            Dim cmdServices As VSDBCmdServices = GetCmdServices(em)
            Dim serviceConstructor As SchemaDeploymentConstructor = em.DatabaseSchemaProvider.GetServiceConstructor(Of SchemaDeploymentConstructor)()
            serviceConstructor.Errors = manager
            serviceConstructor.TargetDatabaseName = DatabaseName
            serviceConstructor.Setup(dbSchemaFile, ConnectionString)
            If (Not cmdServices Is Nothing) Then
                'cmdServices.InitializeConstructor(serviceConstructor)
                cmdServices.GetType.GetMethod("InitializeConstructor", BindingFlags.Instance Or BindingFlags.NonPublic, Nothing, {GetType(SchemaDeploymentConstructor)}, Nothing).Invoke(cmdServices, {serviceConstructor})
            End If
            engine = serviceConstructor.ConstructService
            If (Not cmdServices Is Nothing) Then
                'cmdServices.InitializeSchemaDeploymentOptions(engine.Options)
                cmdServices.GetType.GetMethod("InitializeSchemaDeploymentOptions", BindingFlags.Instance Or BindingFlags.NonPublic, Nothing, {GetType(SchemaDeploymentOptions)}, Nothing).Invoke(cmdServices, {engine.Options})
            End If

            'Setup deployment engine
            If (handler Is Nothing) Then
                handler = Sub(sender, args)
                              If args.Message.Severity = ErrorSeverity.Error Then errorCount += 1
                              _Errors.Add(args.Message)
                              OnErrorOccured(args.Message)
                          End Sub
            End If
            AddHandler engine.ContributorMessage, handler
            engine.Configure(manifest, manifestFile.Directory)
            Dim availableProperties As IDictionary(Of String, PropertyInfo) = Nothing
            If (Not cmdServices Is Nothing) Then
                availableProperties = cmdServices.GetSetableDeployProperties(engine.Options.GetType)
            Else
                availableProperties = New Dictionary(Of String, PropertyInfo)
            End If

            'Set additional properties
            If Not VSDBCmdServices.SetProperties(availableProperties, Properties, engine.Options, [set]) Then
                Dim unsetB As New Text.StringBuilder
                For Each [property] In [set]
                    If unsetB.Length > 0 Then unsetB.Append(", ")
                    unsetB.Append([property])
                Next
                Throw New InvalidOperationException(String.Format(My.Resources.err_FailedSetProperties, unsetB))
            End If

            'Final setup
            engine.SetDeployToDatabase(DeployToDatabase)
            If DeployScriptPath = "" Then
                DeployScriptPath = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, String.Format("{0}.sql", Guid.NewGuid))
                _deleteScriptOnDispose = True
            End If
            engine.SetDeployToScript(Not DeployToDatabase, DeployScriptPath)
            engine.Options.TargetConnectionString = ConnectionString
            engine.Options.TargetDatabaseName = DatabaseName

            'Excute deployment engine
            engine.Execute()

        Finally
            If engine IsNot Nothing Then engine.Dispose()
        End Try

        'Check for errors
        For Each cat In manager.GetAllCategories
            For Each [error] In manager.GetAllErrors(cat)
                If [error].GetType.FullName = "Microsoft.Data.Schema.SchemaModel.ExternalElementResolutionError" Then
                    If [error].Severity = ErrorSeverity.Error Then errorCount += 1
                    _Errors.Add([error])
                    OnErrorOccured([error])
                End If
            Next
        Next
        If errorCount > 0 Then
            Throw New DeploymentFailedException(My.Resources.err_Deploy & vbCrLf & GetErrorLog())
        End If
    End Sub

    ''' <summary>Loads an extension managed from a database schema</summary>
    ''' <param name="input"><see cref="IO.FileInfo"/> representing file containing the database schema</param>
    ''' <returns>An <see cref="Extensibility.ExtensionManager"/> containing extensions loaded from the schema</returns>
    ''' <exception cref="DeploymentFailedException">Model cannot be loaded or exceptions cannot be loaded from the model -or- Database scjema provider foes not support current version of schema</exception>
    Private Shared Function LoadExtensionManagerFromDBSchema(ByVal input As IO.FileInfo) As Extensibility.ExtensionManager
        Dim eManager As ExtensionManager
        Try
            Dim header As DataSchemaModelHeader = DataSchemaModel.ReadDataSchemaModelHeader(input.FullName, True)
            Dim manager As New ExtensionManager(header.DatabaseSchemaProviderName)
            If Not manager.DatabaseSchemaProvider.SchemaVersionSupported(header.SchemaVersion) Then
                Throw New DeploymentFailedException(String.Format(My.Resources.err_ProviderVsSchemaVersionConflict, manager.DatabaseSchemaProvider.GetType.Name, header.SchemaVersion)) 'ModelSchema_NotSupportedDbSchemaVersionError
            End If
            eManager = manager
        Catch ex As XmlException
            Throw New DeploymentFailedException(ex.Message, ex)
        Catch ex As ModelSerializationException
            Throw New DeploymentFailedException(ex.Message, ex)
        Catch ex As ExtensibilityException
            Throw New DeploymentFailedException(ex.Message, ex)
        End Try
        Return eManager
    End Function

    ''' <summary>Loads a deployment manifest from file</summary>
    ''' <param name="manifestFile">A <see cref="IO.FileInfo"/> representing file to load manifest from</param>
    ''' <returns>Manifest loaded as MSBuild project</returns>
    ''' <exception cref="DeploymentFailedException">Failed to load a manifest</exception>
    Private Shared Function LoadManifest(ByVal manifestFile As IO.FileInfo) As Project
        Dim project As Project = Nothing
        Try
            project = New Project(manifestFile.FullName, Nothing, "4.0", New ProjectCollection)
        Catch exception As Exception
            Throw New DeploymentFailedException(exception.Message, exception)
        End Try
        Return project
    End Function

    ''' <summary>Gets VSDBCMD services from extention manager</summary>
    ''' <param name="em">An extension manager</param>
    ''' <param name="connectionString">Connection string to database</param>
    ''' <remarks>VSDBCMD services obtained from <paramref name="em"/></remarks>
    Private Shared Function GetCmdServices(ByVal em As ExtensionManager, ByVal connectionString$) As VSDBCmdServices
        Dim cmdServices As VSDBCmdServices = GetCmdServices(em)
        If (Not cmdServices Is Nothing) Then
            'cmdServices.ConnectionString = connectionString
            cmdServices.GetType.GetProperty("ConnectionString").GetSetMethod(True).Invoke(cmdServices, {connectionString})
        End If
        Return cmdServices
    End Function

    ''' <summary>Gets VSDBCMD services from extention manager</summary>
    ''' <param name="em">An extension manager</param>
    ''' <remarks>VSDBCMD services obtained from <paramref name="em"/></remarks>
    Private Shared Function GetCmdServices(ByVal em As ExtensionManager) As VSDBCmdServices
        Dim services As VSDBCmdServices = Nothing
        Dim handle As ExtensionHandle(Of VSDBCmdServices) = Nothing
        If em.TryGetSingleExtension(Of VSDBCmdServices)(handle) Then
            services = handle.Instantiate
        End If
        Return services
    End Function
#End Region

#Region "Utility"
    ''' <summary>Formats <see cref="DataSchemaError"/> to string</summary>
    ''' <param name="error">An error to be formated</param>
    ''' <returns>String representation of <paramref name="error"/></returns>
    ''' <remarks>This is utillity method that can be used to present error messages to user in unified way</remarks>
    Public Shared Function FormatDataSchemaError(ByVal [error] As DataSchemaError) As String
        Dim msg As String = Nothing
        Dim prefix As String = [error].Prefix
        If [error].ErrorCode <> 0 Then
            prefix = [error].BuildErrorCode
        End If
        If Not String.IsNullOrEmpty([error].Document) Then
            msg = String.Format("{0}" & ChrW(9) & "{1}" & ChrW(9) & "({2},{3})" & ChrW(9) & "{4}", New Object() {prefix, [error].Document, [error].Line, [error].Column, [error].Message})
        Else
            msg = String.Format("{0}" & ChrW(9) & "{1}", New Object() {prefix, [error].Message})
        End If
        Return msg
    End Function

    ''' <summary>Gets error log as string</summary>
    ''' <returns>Combined error messages form the <see cref="Errors"/> collection formatted using <see cref="FormatDataSchemaError"/>.</returns>
    ''' <exception cref="ObjectDisposedException">This instance has been already disposed</exception>
    Public Function GetErrorLog() As String
        Dim b As New Text.StringBuilder
        For Each [error] In Errors
            b.AppendLine(FormatDataSchemaError([error]))
        Next
        Return b.ToString
    End Function
#End Region

#Region "IDisposable Support"
    ''' <summary>To detect redundant calls</summary>
    Private disposedValue As Boolean

    ''' <summary>Implements <see cref="IDisposable.Dispose"/></summary>
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' dispose managed state (managed objects).
            End If

            ' free unmanaged resources (unmanaged objects) and override Finalize() below.
            If _deleteScriptOnDispose AndAlso IO.File.Exists(DeployScriptPath) Then
                Try
                    IO.File.Delete(DeployScriptPath)
                Catch : End Try
            End If
            'set large fields to null.
            _Errors = Nothing
        End If
        Me.disposedValue = True
    End Sub


    ''' <summary>Allows an <see cref="T:System.Object" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object" /> is reclaimed by garbage collection.</summary>
    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub


    ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

''' <summary>Event argumens of the <see cref="DatabaseDeployment.ErrorOccured"/> event</summary>
''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
Public Class DataSchemaErrorEventArgs
    Inherits EventArgs
    Private ReadOnly _Error As DataSchemaError
    ''' <summary>CTor</summary>
    ''' <param name="error">An arrot ahich has occured</param>
    ''' <exception cref="ArgumentNullException"><paramref name="error"/> is null</exception>
    Public Sub New(ByVal [error] As DataSchemaError)
        If [error] Is Nothing Then Throw New ArgumentNullException("error")
        _Error = [error]
    End Sub
    ''' <summary>Gets error which has occured</summary>
    Public ReadOnly Property [Error]() As DataSchemaError
        Get
            Return _Error
        End Get
    End Property
    ''' <summary>Converst instance of the <see cref="DataSchemaError"/> class to <see cref="DataSchemaErrorEventArgs"/></summary>
    ''' <param name="a">A <see cref="DataSchemaError"/></param>
    ''' <returns>A new instance of the <see cref="DataSchemaErrorEventArgs"/> class with <see cref="[Error]"/> initialized to <paramref name="a"/>. Null when <paramref name="a"/> is null.</returns>
    Public Shared Widening Operator CType(ByVal a As DataSchemaError) As DataSchemaErrorEventArgs
        If a Is Nothing Then Return Nothing
        Return New DataSchemaErrorEventArgs(a)
    End Operator
    ''' <summary>Converst instance of the <see cref="DataSchemaErrorEventArgs"/> class to <see cref="DataSchemaError"/></summary>
    ''' <param name="a">A <see cref="DataSchemaErrorEventArgs"/></param>
    ''' <returns><paramref name="a"/>.<see cref="[Error]">Error</see>. Null when <paramref name="a"/> is null.</returns>
    Public Shared Widening Operator CType(ByVal a As DataSchemaErrorEventArgs) As DataSchemaError
        If a Is Nothing Then Return Nothing
        Return a.Error
    End Operator

    ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
    ''' <returns><see cref="[Error]"/>.<see cref="DataSchemaError.Message">Message</see></returns>
    Public Overrides Function ToString() As String
        Return [Error].Message
    End Function
End Class