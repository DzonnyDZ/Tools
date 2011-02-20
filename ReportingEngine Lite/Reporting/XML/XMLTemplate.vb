Imports System.Data.Common
Imports System.Xml, System.Xml.Xsl
Imports Tools.WindowsT.FormsT

Namespace ReportingT.ReportingEngineLite
    ''' <summary>Template for XML-based reports (including HTML)</summary>
    Public Class XmlTemplate
        Implements IReportTemplate

        ''' <summary>Performs the export</summary>
        ''' <param name="data">Data comming form database to be exported</param>
        ''' <param name="count">Expected number of rows in <paramref name="data"/> to be exported. Used for progress reporting. -1 if unknown.</param>
        ''' <param name="templatePath">Path of template file (if used)</param>
        ''' <param name="targetFilePath">Path of file to save results to (if used)</param>
        ''' <param name="openWhenDone">True to open file when export finishes</param>
        ''' <param name="context">Report context. The template MUST call <see cref="IReportGeneratorContext.OnRead"/> for every line read from <paramref name="data"/>!</param>
        ''' <exception cref="Exception">There was an error during export</exception>
        ''' <exception cref="OperationCanceledException">Background operation was cancelled (using <paramref name="Context"/>.<see cref="IReportGeneratorContext.BackgroundWorker">BackgroundWorker</see>.<see cref="System.ComponentModel.BackgroundWorker.CancellationPending">CancellationPending</see>)</exception>
        Public Sub Export(ByVal Data As System.Data.IDataReader, ByVal Count As Integer, ByVal TemplatePath As String, ByVal TargetFilePath As String, ByVal OpenWhenDone As Boolean, ByVal Context As IReportGeneratorContext) Implements IReportTemplate.Export
            If Data Is Nothing Then Throw New ArgumentNullException("data")
            If TemplatePath <> "" AndAlso Not IO.File.Exists(TemplatePath) Then Throw New IO.FileNotFoundException(My.Resources.ex_TemplateNotFound, TemplatePath)
            If Context Is Nothing Then Throw New ArgumentNullException("Context")

            If Data Is Nothing Then Throw New ArgumentNullException("data")
            If TemplatePath <> "" AndAlso Not IO.File.Exists(TemplatePath) Then Throw New IO.FileNotFoundException(My.Resources.ex_TemplateNotFound, TemplatePath)
            If Context Is Nothing Then Throw New ArgumentNullException("Context")

            If TargetFilePath = "" Then
                Dim sfd As New SaveFileDialog With {.DefaultExt = DefaultExtension, .Filter = SaveFileMask}
                Dim dialogResult As DialogResult = Context.Invoke(Function() sfd.ShowDialog)
                If dialogResult = Windows.Forms.DialogResult.OK Then
                    TargetFilePath = sfd.FileName
                Else
                    Throw New OperationCanceledException
                End If
            End If

            If Context.BackgroundWorker IsNot Nothing Then Context.BackgroundWorker.ReportProgress(-1, False)
            Try
                If Context.BackgroundWorker IsNot Nothing Then Context.BackgroundWorker.ReportProgress(-1, ProgressBarStyle.Marquee)
                Dim Template As XslCompiledTransform = Nothing
                If TemplatePath <> "" Then
                    'Nastavení XSL transformace
                    Template = New XslCompiledTransform(Debugger.IsAttached)
                    Dim stg As New XsltSettings(True, True)
                    Dim resolver As New XmlUrlResolver
                    'Načtení XSL šablony
                    If Context.BackgroundWorker IsNot Nothing Then Context.BackgroundWorker.ReportProgress(-1, My.Resources.msg_LoadingXsltTemplate)
                    Template.Load(TemplatePath, stg, resolver)
                End If
                'Vytvoření datasetu
                Dim ds As New DataSet
                If Me.Settings.DataSetName <> "" Then ds.DataSetName = Me.Settings.DataSetName
                If Me.Settings.DataSetNamespace <> "" Then ds.Namespace = Me.Settings.DataSetNamespace
                If Me.Settings.DatasetNamespacePrefix <> "" Then ds.Prefix = Me.Settings.DatasetNamespacePrefix
                Dim Table = ds.Tables.Add()
                If Me.Settings.TableName <> "" Then Table.TableName = Me.Settings.TableName
                'Načtení dat do datasetu
                If Context.BackgroundWorker IsNot Nothing Then Context.BackgroundWorker.ReportProgress(-1, My.Resources.msg_LoadingDataFromDatabase)
                Table.Load(Data)
                Table.Namespace = ds.Namespace
                Table.Prefix = ds.Prefix
                If TemplatePath <> "" Then
                    'Vytvoření IXPathNavigable z DataSetu
                    Dim doc As New XmlDataDocument(ds)
                    'Spuštění transformace and datasetem
                    If Context.BackgroundWorker IsNot Nothing Then Context.BackgroundWorker.ReportProgress(-1, My.Resources.msg_ExportingXml)
                    Dim ws As New XmlWriterSettings
                    Using w = XmlWriter.Create(TargetFilePath, Template.OutputSettings)
                        Template.Transform(doc, w)
                    End Using
                Else
                    If Context.BackgroundWorker IsNot Nothing Then Context.BackgroundWorker.ReportProgress(-1, My.Resources.msg_SavingXml)
                    ds.WriteXml(TargetFilePath)
                End If
                If Context.BackgroundWorker IsNot Nothing Then Context.BackgroundWorker.ReportProgress(100)
            Finally
                If Context.BackgroundWorker IsNot Nothing Then Context.BackgroundWorker.ReportProgress(-1, ProgressBarStyle.Blocks)
                If Context.BackgroundWorker IsNot Nothing Then Context.BackgroundWorker.ReportProgress(-1, True)
            End Try
            If OpenWhenDone Then
                Try : Process.Start(TargetFilePath) : Catch : End Try
            End If
        End Sub

#Region "nedůležité"
        ''' <summary>Gets an instance of settings editor control</summary>
        ''' <returns>A new instance of control for editing current report settings</returns>
        ''' <remarks>The control must be prepared for being resized to any size.</remarks>
        Public Function GetConfigEditor() As System.Windows.Forms.Control Implements IReportTemplate.GetConfigEditor
            Dim ret As New PropertyGridEditor(Of XmlTemplateSettings)(Me.Settings)
            Return ret
        End Function

        ''' <summary>Retturns template settings as XML document</summary>
        ''' <returns>A XML document representing template settings</returns>
        Public Function GetSettings() As System.Xml.XmlDocument Implements IReportTemplate.GetSettings
            Dim s As New Xml.Serialization.XmlSerializer(GetType(XmlTemplateSettings))
            Dim ms As New IO.MemoryStream
            s.Serialize(ms, Settings)
            ms.Flush()
            ms.Position = 0
            Dim ret As New Xml.XmlDocument()
            ret.Load(ms)
            Return ret
        End Function
        ''' <summary>Initializes template settings from a XML document</summary>
        ''' <param name="settings">A settings</param>
        ''' <exception cref="ArgumentNullException"><paramref name="settings"/> is null</exception>
        Public Sub Init(ByVal settings As System.Xml.XmlDocument) Implements IReportTemplate.Init
            Dim s As New Xml.Serialization.XmlSerializer(GetType(XmlTemplateSettings))
            Dim str As New IO.MemoryStream
            settings.Save(str)
            str.Flush()
            str.Position = 0
            _settings = s.Deserialize(str)
        End Sub
        ''' <summary>Initializes template settings from a XML document</summary>
        ''' <param name="settings">A settings</param>
        ''' <exception cref="ArgumentNullException"><paramref name="settings"/> is null</exception>
        Public Sub Init(ByVal settings As Xml.Linq.XElement) Implements IReportTemplate.Init
            Dim s As New Serialization.XmlSerializer(GetType(XmlTemplateSettings))
            Using str As New IO.MemoryStream, xw = Xml.XmlWriter.Create(str)
                settings.WriteTo(xw)
                xw.Flush()
                str.Flush()
                str.Position = 0
                _settings = s.Deserialize(str)
            End Using
        End Sub
        Private _settings As New XmlTemplateSettings
        ''' <summary>gets or sets current setttings</summary>
        Public Property Settings() As XmlTemplateSettings
            Get
                Return _settings
            End Get
            Set(ByVal value As XmlTemplateSettings)
                _settings = value
            End Set
        End Property

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Function Clone() As IReportTemplate Implements IReportTemplate.Clone
            Dim ret As New XmlTemplate
            ret._settings = Me.Settings.Clone
            ret.Log = Me.Log
            Return ret
        End Function
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance.</returns>
        Private Function ICloneable_Clone() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function

        ''' <summary>Default extension (without leading tod (.)) foir saving a file</summary>
        ''' <remarks>Not used when <see cref="SaveFileMask"/> is null or an empty string</remarks>
        Public ReadOnly Property DefaultExtension() As String Implements IReportTemplate.DefaultExtension
            Get
                If Settings IsNot Nothing AndAlso Settings.DefaultExtension <> "" Then Return Settings.DefaultExtension
                Return "xml"
            End Get
        End Property



        ''' <summary>Gets a list of <see cref="ShortName"/>s of templates. Reports of types returned can be used as sub-reports of this report.</summary>
        ''' <remarks>Ignored if <see cref="Subreport"/> is <see cref="SubreportType.TopLevelOnly"/></remarks>
        ''' <returns>Array of <see cref="ShortName"/>s. In case the template does not support subreports returns an empty array (not null!)</returns>
        Public ReadOnly Property ChildrenTemplateTypes() As String() Implements IReportTemplate.ChildrenTemplateTypes
            Get
                Return New String() {}
            End Get
        End Property

        Private _Log As ILogger
        ''' <summary>Gets or sets a logging object</summary>
        ''' <remarks>When set tamplate may use it for logging</remarks>
        Public Property Log() As ILogger Implements IReportTemplate.Log
            Get
                Return _Log
            End Get
            Set(value As ILogger)
                _Log = value
            End Set
        End Property

        ''' <summary>Human-friendly Template name (as shown to the user)</summary>
        Public ReadOnly Property Name() As String Implements IReportTemplate.Name
            Get
                Return ReportingT.ReportingEngineLite.ReportingResources.XML
            End Get
        End Property

        ''' <summary>A mask for saving files</summary>
        ''' <remarks>In case null or an empty string is returned user will not be alloewed to select file for saving</remarks>
        ''' <seealso cref="OpenFileDialog.Filter"/>
        Public ReadOnly Property SaveFileMask() As String Implements IReportTemplate.SaveFileMask
            Get
                If Settings IsNot Nothing AndAlso Settings.Filter <> "" Then Return Settings.Filter
                Return My.Resources.filter_XML
            End Get
        End Property

        ''' <summary>Machine-friendly template name (as identified by computer)</summary>
        Public ReadOnly Property ShortName() As String Implements IReportTemplate.ShortName
            Get
                Return "XML"
            End Get
        End Property

        ''' <summary>Gets a value which indicates if the template is intended for sub-reports or top-level reports</summary>
        ''' <returns>This implementation always returns <see cref="SubreportType.TopLevelOnly"/> - this template is not intended for subreports</returns>
        Public ReadOnly Property Subreport() As SubreportType Implements IReportTemplate.Subreport
            Get
                Return SubreportType.TopLevelOnly
            End Get
        End Property

        ''' <summary>A mask for looking for template files</summary>
        ''' <remarks>In cas null or an empty string is returned user will not be allowed to serach for ttempla file</remarks>
        ''' <seealso cref="OpenFileDialog.Filter"/>
        Public ReadOnly Property TemplateMask() As String Implements IReportTemplate.TemplateMask
            Get
                Return "XML transformace (*.xsl, *.xslt)|*.xsl;*.xslt"
            End Get
        End Property

#End Region

        Private Shared _xmlSchema As Xml.Schema.XmlSchema
        Private Shared xmlSchemaLockObject As New Object
        ''' <summary>Gets XML schema used for storing settings of <see cref="SimpleXlsSettings"/> and <see cref="RepeatedXlsSettings"/>.</summary>
        Public Shared ReadOnly Property XmlSchema As Xml.Schema.XmlSchema
            Get
                If _xmlSchema Is Nothing Then
                    SyncLock xmlSchemaLockObject
                        If _xmlSchema Is Nothing Then
                            Dim stream = GetType(SimpleXlsTemplate).Assembly.GetManifestResourceStream("Tools.ReportingT.ReportingEngineLite.XmlTemplateSettings.xsd")
                            _xmlSchema = Xml.Schema.XmlSchema.Read(stream, Nothing)
                        End If
                    End SyncLock
                End If
                Return _xmlSchema
            End Get
        End Property
    End Class
End Namespace
