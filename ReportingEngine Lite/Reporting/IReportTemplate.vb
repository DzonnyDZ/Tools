Imports System.Data.Common
Imports System.Xml.Linq
Imports System.Runtime.InteropServices

Namespace ReportingT.ReportingEngineLite
    ''' <summary>Interface of report template</summary>
    Public Interface IReportTemplate
        Inherits ICloneable, ICloneable(Of IReportTemplate)
        ''' <summary>Initializes template settings from a XML document</summary>
        ''' <param name="settings">A settings</param>
        ''' <exception cref="ArgumentNullException"><paramref name="settings"/> is null</exception>
        Sub Init(ByVal settings As Xml.XmlDocument)
        ''' <summary>Initializes template settings from a XML element in form of <see cref="XElement"/></summary>
        ''' <param name="settings">A settings</param>
        ''' <exception cref="ArgumentNullException"><paramref name="settings"/> is null</exception>
        Sub init(ByVal settings As XElement)
        ''' <summary>Gets an instance of settings editor control</summary>
        ''' <returns>A new instance of control for editing current report settings</returns>
        ''' <remarks>The control must be prepared for being resized to any size.</remarks>
        Function GetConfigEditor() As Control
        ''' <summary>Human-friendly Template name (as shown to the user)</summary>
        ReadOnly Property Name$()
        ''' <summary>Machine-friendly template name (as identified by computer)</summary>
        ReadOnly Property ShortName$()
        ''' <summary>Gets or sets a logging object</summary>
        ''' <remarks>When set tamplate may use it for logging</remarks>
        Property Log() As ILogger
        ''' <summary>Performs the export</summary>
        ''' <param name="data">Data comming form database to be exported</param>
        ''' <param name="count">Expected number of rows in <paramref name="data"/> to be exported. Used for progress reporting. -1 if unknown.</param>
        ''' <param name="templatePath">Path of template file (if used)</param>
        ''' <param name="targetFilePath">Path of file to save results to (if used)</param>
        ''' <param name="openWhenDone">True to open file when export finishes</param>
        ''' <param name="context">Report context. The template MUST call <see cref="IReportGeneratorContext.OnRead"/> for every line read from <paramref name="data"/>!</param>
        ''' <exception cref="Exception">There was an error during export</exception>
        ''' <exception cref="OperationCanceledException">Background operation was cancelled (using <paramref name="Context"/>.<see cref="IReportGeneratorContext.BackgroundWorker">BackgroundWorker</see>.<see cref="System.ComponentModel.BackgroundWorker.CancellationPending">CancellationPending</see>)</exception>
        Sub Export(ByVal Data As IDataReader, ByVal Count%, ByVal TemplatePath As String, ByVal TargetFilePath$, ByVal OpenWhenDone As Boolean, ByVal Context As IReportGeneratorContext)
        ''' <summary>A mask for looking for template files</summary>
        ''' <remarks>In cas null or an empty string is returned user will not be allowed to serach for ttempla file</remarks>
        ''' <seealso cref="OpenFileDialog.Filter"/>
        ReadOnly Property TemplateMask$()
        ''' <summary>A mask for saving files</summary>
        ''' <remarks>In case null or an empty string is returned user will not be alloewed to select file for saving</remarks>
        ''' <seealso cref="OpenFileDialog.Filter"/>
        ReadOnly Property SaveFileMask$()
        ''' <summary>Default extension (without leading tod (.)) foir saving a file</summary>
        ''' <remarks>Not used when <see cref="SaveFileMask"/> is null or an empty string</remarks>
        ReadOnly Property DefaultExtension() As String
        ''' <summary>Retturns template settings as XML document</summary>
        ''' <returns>A XML document representing template settings</returns>
        Function GetSettings() As Xml.XmlDocument
        ''' <summary>Gets a value which indicates if the template is intended for sub-reports or top-level reports</summary>
        ReadOnly Property Subreport() As SubreportType
        ''' <summary>Gets a list of <see cref="ShortName"/>s of templates. Reports of types returned can be used as sub-reports of this report.</summary>
        ''' <remarks>Ignored if <see cref="Subreport"/> is <see cref="SubreportType.TopLevelOnly"/></remarks>
        ''' <returns>Array of <see cref="ShortName"/>s. In case the template does not support subreports returns an empty array (not null!)</returns>
        ReadOnly Property ChildrenTemplateTypes() As String()
    End Interface

    ''' <summary>Possible types of template and subreports</summary>
    Public Enum SubreportType
        ''' <summary>The template generates only top-level reports</summary>
        TopLevelOnly
        ''' <summary>The template generates sub-reports only</summary>
        SubreportOnly
        ''' <summary>The templace can generate either sub-reports or top-level reports</summary>
        Both
    End Enum

    ''' <summary>Report generation context interface</summary>
    Public Interface IReportGeneratorContext
        ''' <summary>Gets subreports of current report</summary> 
        ReadOnly Property Subreports() As ISubReport()
        ''' <summary>Number of items in the <see cref="DataSources"/> collection</summary>
        ReadOnly Property DataSourcesCount%()
        ''' <summary>Gets additional datasources accessibly from parent reports</summary>
        ''' <param name="index">Index 0 ÷ <see cref="DataSourcesCount"/> - 1</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is out fo range 0 ÷ <see cref="DataSourcesCount"/> - 1</exception>
        ''' <remarks>Each data source can be used only once!</remarks>
        ReadOnly Property DataSources(ByVal index%) As IDataReader
        ''' <summary>All user-inerface operations must be invoked using this method</summary>
        ''' <param name="method">A delegate to a method that takes parameters of the same number and type that are contained in the args parameter.</param>
        ''' <param name="args">An array of objects to pass as arguments to the specified method. This parameter can be null if the method takes no arguments.</param>
        ''' <returns>An <see cref="System.Object"/> that contains the return value from the delegate being invoked, or null if the delegate has no return value.</returns>
        Function Invoke(ByVal method As System.Delegate, ByVal ParamArray args() As Object) As Object
        ''' <summary>Gets a logger</summary>
        ''' <returns>A logger instance or null.</returns>
        ReadOnly Property Log() As ILogger
        ''' <summary>Gets a background worker, used when report-generation is done in background thread.</summary>
        ''' <returns>A <see cref="System.ComponentModel.BackgroundWorker"/> if report-generation is ran in background thread, nulll if it is ran synchronously.</returns>
        ReadOnly Property BackgroundWorker() As System.ComponentModel.BackgroundWorker
        ''' <summary>Gets disctionary of report gllobal variables</summary>
        ReadOnly Property GlobalVariables() As Dictionary(Of String, Object)
        ''' <summary>Called for each row when template reads next row from reader</summary>
        ''' <param name="Reader">Reader data are read from</param>
        ''' <remarks>Template MUST call this method fro each row it reads from data source</remarks>
        Sub OnRead(ByVal Reader As IDataReader)
    End Interface

    ''' <summary>A subreport interface</summary>
    Public Interface ISubReport
        ''' <summary>Rubreport template</summary>
        ReadOnly Property Template() As IReportTemplate
        ''' <summary>Obtains a data for the subreport</summary>
        ''' <param name="Count">When the function finishes this parameter returns number of rows in subreport. -1 if it cannnot be determined.</param>
        ''' <exception cref="System.Data.Common.DbException">There was an error while obtaining subreport data</exception>
        ''' <exception cref="NotSupportedException">A stored procedure for data preparation requires a parameter of type curosr or count.</exception>
        Function GetData(<Out> ByRef Count%) As IDataReader
    End Interface
End Namespace