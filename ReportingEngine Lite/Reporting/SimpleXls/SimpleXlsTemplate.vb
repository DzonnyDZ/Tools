Imports System.Xml.Serialization, System.Data.Common
Imports Microsoft.Office.Interop
Imports System.Linq
Imports Tools.WindowsT.FormsT
Imports System.Runtime.InteropServices
Imports Tools.CodeDomT.CompilerT

Namespace ReportingT.ReportingEngineLite
    ''' <summary>Template for simple export to Excel (XLS)</summary>
    Public Class SimpleXlsTemplate
        Implements IReportTemplate
        Private _Settings As SimpleXlsSettings
        ''' <summary>Creates a new instance of <see cref="SimpleXlsSettings"/></summary>
        ''' <returns>A new instance of <see cref="SimpleXlsSettings"/></returns>
        Protected Overridable Function NewSettings() As SimpleXlsSettings
            Return New SimpleXlsSettings
        End Function

        ''' <summary>Gets or sets settings of this templae</summary>
        Public Property Settings() As SimpleXlsSettings
            Get
                Return _Settings
            End Get
            Protected Set(ByVal value As SimpleXlsSettings)
                _Settings = value
            End Set
        End Property
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Overridable Function Clone() As IReportTemplate Implements IReportTemplate.Clone
            Dim ret As New SimpleXlsTemplate
            ret._Settings = Me.Settings.Clone
            ret.Log = Me.Log
            Return ret
        End Function

        Private Shared _xmlSchema As Xml.Schema.XmlSchema
        Private Shared xmlSchemaLockObject As New Object
        ''' <summary>Gets XML schema used for storing settings of <see cref="SimpleXlsSettings"/> and <see cref="RepeatedXlsSettings"/>.</summary>
        Public Shared ReadOnly Property XmlSchema As Xml.Schema.XmlSchema
            Get
                If _xmlSchema Is Nothing Then
                    SyncLock xmlSchemaLockObject
                        If _xmlSchema Is Nothing Then
                            Dim stream = GetType(SimpleXlsTemplate).Assembly.GetManifestResourceStream("Tools.ReportingT.ReportingEngineLite.SimpleXlsTemplateSettings.xsd")
                            _xmlSchema = Xml.Schema.XmlSchema.Read(stream, Nothing)
                        End If
                    End SyncLock
                End If
                Return _xmlSchema
            End Get
        End Property

        ''' <summary>Gets an instance of settings editor control</summary>
        ''' <returns>A new instance of control for editing current report settings</returns>
        ''' <remarks>The control must be prepared for being resized to any size.</remarks>
        Public Overridable Function GetConfigEditor() As System.Windows.Forms.Control Implements IReportTemplate.GetConfigEditor
            Dim ret As New PropertyGridEditor(Of SimpleXlsSettings)(Me.Settings)
            Return ret
        End Function
        ''' <summary>Initializes template settings from a XML document</summary>
        ''' <param name="settings">A settings</param>
        ''' <exception cref="ArgumentNullException"><paramref name="settings"/> is null</exception>
        Public Sub Init(ByVal settings As System.Xml.XmlDocument) Implements IReportTemplate.Init
            Dim s As New XmlSerializer(Me.Settings.GetType)
            Using str As New IO.MemoryStream
                settings.Save(str)
                str.Flush()
                str.Position = 0
                _Settings = s.Deserialize(str)
            End Using
        End Sub
        ''' <summary>Initializes template settings from a XML document</summary>
        ''' <param name="settings">A settings</param>
        ''' <exception cref="ArgumentNullException"><paramref name="settings"/> is null</exception>
        Public Sub Init(ByVal settings As Xml.Linq.XElement) Implements IReportTemplate.Init
            Dim s As New XmlSerializer(Me.Settings.GetType)
            Using str As New IO.MemoryStream, xw = Xml.XmlWriter.Create(str)
                settings.WriteTo(xw)
                xw.Flush()
                str.Flush()
                str.Position = 0
                _Settings = s.Deserialize(str)
            End Using
        End Sub

        Private _log As ILogger
        ''' <summary>Gets or sets a logging object</summary>
        ''' <remarks>When set tamplate may use it for logging</remarks>
        Public Property Log() As ILogger Implements IReportTemplate.Log
            Get
                Return _log
            End Get
            Set(value As ILogger)
                _log = value
            End Set
        End Property
        ''' <summary>Human-friendly Template name (as shown to the user)</summary>
        Public Overridable ReadOnly Property Name() As String Implements IReportTemplate.Name
            Get
                Return My.Resources.ExcelSimple
            End Get
        End Property
        ''' <summary>Machine-friendly template name (as identified by computer)</summary>
        Public Overridable ReadOnly Property ShortName() As String Implements IReportTemplate.ShortName
            Get
                Return ShortName_XLS
            End Get
        End Property
        Public Const ShortName_XLS$ = "XLS"
        ''' <summary>A mask for looking for template files</summary>
        ''' <seealso cref="OpenFileDialog.Filter"/>
        ''' <returns>The mask depends on installed version of MS Excel</returns>
        Public ReadOnly Property TemplateMask() As String Implements IReportTemplate.TemplateMask
            Get
                Dim masks As New List(Of String)
                If ExcelVersion >= Version2007 Then
                    masks.Add(My.Resources.ExcelName_TemplatesAll & String.Format(" ({0})|{0}", "*.xltx;*.xltm;*.xlt"))
                    masks.Add(My.Resources.ExcelName_TemplateDefault & String.Format(" (*.{0})|*.{0}", "xltx"))
                    masks.Add(My.Resources.ExcelName_XLTM & String.Format(" (*.{0})|*.{0}", "xltm"))
                    masks.Add(My.Resources.ExcelName_XLTOld & String.Format(" (*.{0})|*.{0}", "xlt"))
                Else
                    masks.Add(My.Resources.ExcelName_TemplateDefault & String.Format(" (*.{0})|*.{0}", "xlt"))
                End If

                If ExcelVersion >= Version2007 Then
                    masks.Add(My.Resources.ExcelName_Default & String.Format(" (*.{0})|*.{0}", "xlsx"))
                    masks.Add(My.Resources.ExcelName_XLSM & String.Format(" (*.{0})|*.{0}", "xlsxm"))
                    masks.Add(My.Resources.ExcelName_XLSB & String.Format(" (*.{0})|*.{0}", "xlsb"))
                    masks.Add(My.Resources.ExcelName_XLSOld & String.Format(" (*.{0})|*.{0}", "xls"))
                Else
                    masks.Add(My.Resources.ExcelName_Default & String.Format(" (*.{0})|*.{0}", "xls"))
                End If
                If ExcelVersion > Version2007 Then
                    masks.Add(My.Resources.ExcelName_CommonFiles & "|*.xlsx;*.xlsm;*.xlsb;*.xls;*.xltx;*.xltm;*.xlt")
                Else
                    masks.Add(My.Resources.ExcelName_CommonFiles & "|*.xls;*.xlt")
                End If
                Return String.Join("|", masks)
            End Get
        End Property
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Private Function ICloneable_Clone() As Object Implements System.ICloneable.Clone
            Return Me.Clone
        End Function
        ''' <summary>Retturns template settings as XML document</summary>
        ''' <returns>A XML document representing template settings</returns>
        Public Function GetSettings() As System.Xml.XmlDocument Implements IReportTemplate.GetSettings
            Dim s As New XmlSerializer(Me.Settings.GetType)
            Dim ms As New IO.MemoryStream
            s.Serialize(ms, _Settings)
            ms.Flush()
            ms.Position = 0
            Dim ret As New Xml.XmlDocument()
            ret.Load(ms)
            Return ret
        End Function
        ''' <summary>a mask for saving files</summary>
        ''' <remarks>The mask depends on installed version of MS Excel</remarks>
        ''' <seealso cref="OpenFileDialog.Filter"/>
        Public ReadOnly Property SaveFileMask() As String Implements IReportTemplate.SaveFileMask
            Get
                Dim masks As New List(Of String)
                If ExcelVersion >= Version2007 Then
                    masks.Add(My.Resources.ExcelName_Default & String.Format(" (*.{0})|*.{0}", "xlsx"))
                    masks.Add(My.Resources.ExcelName_XLSM & String.Format(" (*.{0})|*.{0}", "xlsxm"))
                    masks.Add(My.Resources.ExcelName_XLSB & String.Format(" (*.{0})|*.{0}", "xlsb"))
                    masks.Add(My.Resources.ExcelName_XLSOld & String.Format(" (*.{0})|*.{0}", "xls"))
                Else
                    masks.Add(My.Resources.ExcelName_Default & String.Format(" (*.{0})|*.{0}", "xls"))
                End If
                If ExcelVersion >= VersionXP Then
                    masks.Add(My.Resources.ExcelName_XML & String.Format(" (*.{0})|*.{0}", "xml"))
                End If
                masks.Add(My.Resources.ExcelName_MHTML & " (*.mhtm;*.mhtml)|*.mhtm;*.mhtml")
                masks.Add(My.Resources.ExcelName_HTML & " (*.htm;*.html)|*.htm;*.html")
                If ExcelVersion >= Version2007 Then
                    masks.Add(My.Resources.ExcelName_TemplateDefault & String.Format(" (*.{0})|*.{0}", "xltx"))
                    masks.Add(My.Resources.ExcelName_XLTM & String.Format(" (*.{0})|*.{0}", "xltm"))
                    masks.Add(My.Resources.ExcelName_XLTOld & String.Format(" (*.{0})|*.{0}", "xlt"))
                Else
                    masks.Add(My.Resources.ExcelName_TemplateDefault & String.Format(" (*.{0})|*.{0}", "xlt"))
                End If
                masks.Add(My.Resources.ExcelName_TXT & String.Format(" (*.{0})|*.{0}", "txt"))
                masks.Add(My.Resources.ExcelName_CSV & String.Format(" (*.{0})|*.{0}", "csv"))
                masks.Add(My.Resources.ExcelName_PRN & String.Format(" (*.{0})|*.{0}", "prn"))
                masks.Add(My.Resources.ExcelName_DIF & String.Format(" (*.{0})|*.{0}", "dif"))
                masks.Add(My.Resources.ExcelName_SLK & String.Format(" (*.{0})|*.{0}", "slk"))
                If ExcelVersion >= Version2007 Then
                    masks.Add(My.Resources.ExcelName_AddInDefault & String.Format(" (*.{0})|*.{0}", "xlam"))
                    masks.Add(My.Resources.ExcelName_XLAOld & String.Format(" (*.{0})|*.{0}", "xla"))
                Else
                    masks.Add(My.Resources.ExcelName_AddInDefault & String.Format(" (*.{0})|*.{0}", "xla"))
                End If
                If ExcelVersion >= Version2007 Then
                    masks.Add(My.Resources.ExcelName_PDF & String.Format(" (*.{0})|*.{0}", "pdf"))
                    masks.Add(My.Resources.ExcelName_XPS & String.Format(" (*.{0})|*.{0}", "xps"))
                    masks.Add(My.Resources.ExcelName_ODS & String.Format(" (*.{0})|*.{0}", "ods"))
                End If
                Return String.Join("|", masks)
            End Get
        End Property
        ''' <summary>Performs the export</summary>
        ''' <param name="data">Data comming form database to be exported</param>
        ''' <param name="count">Expected number of rows in <paramref name="data"/> to be exported. Used for progress reporting. -1 if unknown.</param>
        ''' <param name="templatePath">Path of template file (if used)</param>
        ''' <param name="targetFilePath">Path of file to save results to (if used)</param>
        ''' <param name="openWhenDone">True to open file when export finishes</param>
        ''' <param name="context">Report context. The template MUST call <see cref="IReportGeneratorContext.OnRead"/> for every line read from <paramref name="data"/>!</param>
        ''' <exception cref="Exception">There was an error during export</exception>
        ''' <exception cref="OperationCanceledException">Background operation was cancelled (using <paramref name="Context"/>.<see cref="IReportGeneratorContext.BackgroundWorker">BackgroundWorker</see>.<see cref="System.ComponentModel.BackgroundWorker.CancellationPending">CancellationPending</see>)</exception>
        ''' <version version="1.5.4">Arguments renamed: All firts letters changed to lowercase</version>
        Public Overridable Sub Export(ByVal data As IDataReader, ByVal count%, ByVal templatePath As String, ByVal targetFilePath As String, ByVal openWhenDone As Boolean, ByVal context As IReportGeneratorContext) Implements IReportTemplate.Export
            If Data Is Nothing Then Throw New ArgumentNullException("data")
            If TemplatePath <> "" AndAlso Not IO.File.Exists(TemplatePath) Then Throw New IO.FileNotFoundException(My.Resources.ex_TemplateNotFound, TemplatePath)
            If Context Is Nothing Then Throw New ArgumentNullException("Context")

            If TargetFilePath = "" Then
                Dim sfd As New SaveFileDialog With {.DefaultExt = DefaultExtension, .Filter = SaveFileMask}
                Dim dialogResult As DialogResult = Context.Invoke(Function() sfd.ShowDialog)
                If dialogResult = System.Windows.Forms.DialogResult.OK Then
                    TargetFilePath = sfd.FileName
                Else
                    Throw New OperationCanceledException
                End If
            End If

            Context = Context
            If Context.BackgroundWorker IsNot Nothing Then
                Context.BackgroundWorker.ReportProgress(-1, ProgressBarStyle.Marquee)
                Context.BackgroundWorker.ReportProgress(-1, String.Format(My.Resources.msg_PreparingTemplate, Me.Name))
            End If
            If Context.BackgroundWorker IsNot Nothing AndAlso Context.BackgroundWorker.CancellationPending Then Throw New OperationCanceledException(My.Resources.ex_OperationCancelled)
            Dim xl As New Excel.Application
            Dim oda As Boolean = xl.DisplayAlerts
            xl.DisplayAlerts = False
            Try
                Dim FirstList As Excel.Worksheet = Nothing 'In case expport is not template-based store 1st list of worksheet here
                Dim TemplateFile As Excel.Workbook
                If TemplatePath <> "" Then
                    TemplateFile = xl.Workbooks.Open(TemplatePath, , ReadOnly:=True, Editable:=True, AddToMru:=False)
                Else
                    TemplateFile = xl.Workbooks.Add()
                    While TemplateFile.Worksheets.Count > 1
                        DirectCast(TemplateFile.Worksheets(TemplateFile.Worksheets.Count), Excel.Worksheet).Delete()
                    End While
                    FirstList = TemplateFile.Worksheets(1)
                End If
                Try
                    Dim List As Excel.Worksheet
                    Dim ListIndex%
                    If Me.Settings.List = "" AndAlso FirstList Is Nothing Then
                        List = TemplateFile.Worksheets.Add(After:=TemplateFile.Worksheets(TemplateFile.Worksheets.Count))
                    ElseIf Me.Settings.List = "" Then
                        List = FirstList
                        FirstList = Nothing
                    ElseIf Integer.TryParse(Me.Settings.List, Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, ListIndex) Then
                        If ListIndex > TemplateFile.Worksheets.Count Then Throw New IndexOutOfRangeException(My.Resources.ex_ExcelListIndexOutOfRange & ListIndex)
                        List = TemplateFile.Worksheets.Item(ListIndex)
                    Else
                        List = TemplateFile.Worksheets.Item(Me.Settings.List)
                    End If
                    'This report
                    Export(Data, Count, List, Context)
                    'Subreport
                    Subreports(TemplateFile, Context)
                    'Activate a list
                    If Me.Settings.SelectList <> "" Then
                        Dim SelectedListIndex%
                        If Integer.TryParse(Me.Settings.SelectList, Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, SelectedListIndex%) AndAlso SelectedListIndex% > 0 Then
                            DirectCast(TemplateFile.Sheets(SelectedListIndex%), Excel.Worksheet).Activate()
                        Else
                            DirectCast(TemplateFile.Sheets(Me.Settings.SelectList), Excel.Worksheet).Activate()
                        End If
                    End If
                    'Run a macro
                    If Me.Settings.RunMacroAfter <> "" Then
                        TemplateFile.Application.Run(Me.Settings.RunMacroAfter)
                    End If
                    'Run a code
                    If Me.Settings.PostProcessingCode <> "" Then
                        DoPostProcessing(TemplateFile, Me.Settings.PostProcessingCode)
                    End If
                    'Save
                    Select Case IO.Path.GetExtension(TargetFilePath).ToLower
                        Case ".pdf", ".xps"
                            TemplateFile.ExportAsFixedFormat(Type:=GetFileTypeByExtension(IO.Path.GetExtension(TargetFilePath)),
                                                             Filename:=TargetFilePath,
                                                             Quality:=Excel.XlFixedFormatQuality.xlQualityStandard,
                                                             IncludeDocProperties:=True, IgnorePrintAreas:=False,
                                                             OpenAfterPublish:=False)
                        Case Else
                            TemplateFile.SaveAs(TargetFilePath, AddToMru:=False, FileFormat:=GetFileTypeByExtension(IO.Path.GetExtension(TargetFilePath)))
                    End Select
                Finally
                    TemplateFile.Close(False)
                End Try
            Finally
                Try : xl.DisplayAlerts = oda : Catch : End Try
                xl.Quit()
            End Try
            If OpenWhenDone Then
                Try : Process.Start(TargetFilePath)
                Catch : End Try
            End If
        End Sub

        ''' <summary>Gets Excel file format associated with giben extension</summary>
        ''' <param name="extension">Extension to get format for. Case-insensitive. Leading dot (.) ignored.</param>
        ''' <returns>One of <see cref="Excel.XlFileFormat"/> values. For PDF and XPS returns <see cref="Excel.XlFixedFormatType"/>.</returns>
        Private Function GetFileTypeByExtension(ByVal extension As String) As Excel.XlFileFormat
            If extension.StartsWith(".") Then extension = extension.Substring(1)
            extension = extension.ToLower
            Select Case extension
                Case "xlsx" : Return Excel.XlFileFormat.xlOpenXMLWorkbook
                Case "xlsm" : Return Excel.XlFileFormat.xlOpenXMLWorkbookMacroEnabled
                Case "xlsb" : Return Excel.XlFileFormat.xlExcel12
                Case "xls" 'xlExcel8 caused problems in v2003
                    If ExcelVersion >= Version2007 Then Return Excel.XlFileFormat.xlExcel8 _
                    Else Return Excel.XlFileFormat.xlWorkbookNormal
                Case "mhtm", "mhtml" : Return Excel.XlFileFormat.xlWebArchive
                Case "xml" : Return Excel.XlFileFormat.xlXMLSpreadsheet
                Case "htm", "html" : Return Excel.XlFileFormat.xlHtml
                Case "xltx" : Return Excel.XlFileFormat.xlOpenXMLTemplate
                Case "xltm" : Return Excel.XlFileFormat.xlOpenXMLTemplateMacroEnabled
                Case "xlt" : Return Excel.XlFileFormat.xlTemplate8
                Case "txt" : Return Excel.XlFileFormat.xlUnicodeText
                Case "csv" : Return Excel.XlFileFormat.xlCSV
                Case "prn" : Return Excel.XlFileFormat.xlTextPrinter
                Case "dif" : Return Excel.XlFileFormat.xlDIF
                Case "slk" : Return Excel.XlFileFormat.xlSYLK
                Case "xlam" : Return Excel.XlFileFormat.xlOpenXMLAddIn
                Case "xla" : Return Excel.XlFileFormat.xlAddIn8
                Case "pdf" : Return Excel.XlFixedFormatType.xlTypePDF
                Case "xps" : Return Excel.XlFixedFormatType.xlTypeXPS
                Case "ods" : Return Excel.XlFileFormat.xlOpenDocumentSpreadsheet
                Case Else : Return Excel.XlFileFormat.xlWorkbookDefault
            End Select
        End Function

        ''' <summary>Gets names of datasource columns to ignore</summary>
        ''' <returns>Names of columns in datasource to ignore</returns>
        Protected Overridable ReadOnly Property SkippedColumns() As List(Of String)
            Get
                Return Settings.GetSkipColumns
            End Get
        End Property
        ''' <summary>Exports data to selected Excel list</summary>
        ''' <param name="data">Data to export</param>
        ''' <param name="count">Number of rows in<paramref name="Data"/>, -1 if unknown</param>
        ''' <param name="worksheet">Target list</param>
        ''' <param name="context">A context</param>
        ''' <exception cref="OperationCanceledException">Background operation was cancelled</exception>
        ''' <version version="1.5.4">Arguments renamed: <c>Data</c> to <c>data</c>, <c>Count</c> to <c>count</c>, <c>List</c> to <c>worksheet</c>, <c>Context</c> to <c>context</c></version>
        ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False) (because <paramref name="worksheet"/> argument is not CLS-compliant</version>
        <CLSCompliant(False)>
        Protected Overridable Sub Export(ByVal data As IDataReader, ByVal count%, ByVal worksheet As Excel.Worksheet, ByVal context As IReportGeneratorContext)
            If data Is Nothing Then Throw New ArgumentNullException("data")
            If worksheet Is Nothing Then Throw New ArgumentNullException("List")
            If context.BackgroundWorker IsNot Nothing Then
                context.BackgroundWorker.ReportProgress(-1, ProgressBarStyle.Marquee)
                context.BackgroundWorker.ReportProgress(-1, String.Format(My.Resources.msg_PreparingTemplateList, Me.Name, worksheet.Name))
            End If
            If context.BackgroundWorker IsNot Nothing AndAlso context.BackgroundWorker.CancellationPending Then Throw New OperationCanceledException(My.Resources.ex_OperationCancelled)
            If TypeOf data Is DbDataReader AndAlso Not DirectCast(data, DbDataReader).HasRows Then Return
            Dim Column As Integer = Me.Settings.Col1
            Dim Row As Integer = Me.Settings.Row1
            Dim ColMap As New Dictionary(Of Integer, Integer)
            Dim Col1%, Coln%, Row1%, Rown% '1st ald last columns/rows
            Row1 = Row
            Dim SkipCollumns As List(Of String) = Me.SkippedColumns
            If Settings.SuspendRecalculations Then
                worksheet.Application.Calculation = Excel.XlCalculation.xlCalculationManual
            End If
            Dim FilledColumns(data.FieldCount - 1) As Integer
            Try
                'Create a column map (valid for all rows)
                If Me.Settings.SkipFilled Then 'Skipping of filled columns, copying
                    Dim coli% = Column
                    For i As Integer = 0 To data.FieldCount - 1
                        If SkipCollumns.Contains(data.GetName(i)) Then Continue For
                        If context.BackgroundWorker IsNot Nothing AndAlso context.BackgroundWorker.CancellationPending Then Throw New OperationCanceledException(My.Resources.ex_OperationCancelled)
                        While DirectCast(worksheet.Cells.Item(Row, coli), Excel.Range).Value IsNot Nothing OrElse CBool(DirectCast(worksheet.Cells.Item(Row, coli), Excel.Range).HasFormula)
                            coli += 1
                        End While
                        ColMap.Add(i, coli) : FilledColumns(i) = coli
                        If i = 1 Then Col1 = coli
                        If i = data.FieldCount - 1 Then Coln = coli
                        If Me.Settings.CopyColumnsFrom > 0 AndAlso coli >= Me.Settings.CopyColumnsFrom Then 'Copy a column
                            'List.Range(String.Format(System.Globalization.CultureInfo.InvariantCulture, "C[{0}]", coli)).EntireColumn.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove)
                            DirectCast(worksheet.Columns(coli), Excel.Range).Insert(Excel.XlInsertShiftDirection.xlShiftDown, Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove)
                            'List.Range(String.Format(System.Globalization.CultureInfo.InvariantCulture, "C[{0}]", coli - 1)).EntireColumn.Copy(List.Range(String.Format(System.Globalization.CultureInfo.InvariantCulture, "C[{0}]", coli)))
                            DirectCast(worksheet.Columns(coli - 1), Excel.Range).Copy(worksheet.Columns(coli))
                        End If
                        coli += 1
                    Next
                Else 'No skipping
                    Dim iminus% = 0
                    For i As Integer = 0 To data.FieldCount - 1
                        If SkipCollumns.Contains(data.GetName(i)) Then iminus += 1 : Continue For
                        If context.BackgroundWorker IsNot Nothing AndAlso context.BackgroundWorker.CancellationPending Then Throw New OperationCanceledException(My.Resources.ex_OperationCancelled)
                        Dim coli% = Column + i - iminus
                        ColMap.Add(i, coli)
                        FilledColumns(i) = coli
                        If Me.Settings.CopyColumnsFrom > 0 AndAlso coli >= Me.Settings.CopyColumnsFrom Then 'Copy a column
                            DirectCast(worksheet.Cells(coli), Excel.Range).EntireColumn.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove)
                            DirectCast(worksheet.Cells(coli - 1), Excel.Range).EntireColumn.Copy(DirectCast(worksheet.Cells(coli), Excel.Range).EntireColumn)
                        End If
                    Next
                    Coln = Column + data.FieldCount - 1
                End If
                'Column names
                If Me.Settings.ColumnNameRow >= 1 Then
                    For i As Integer = 0 To data.FieldCount - 1
                        If SkipCollumns.Contains(data.GetName(i)) Then Continue For
                        If context.BackgroundWorker IsNot Nothing AndAlso context.BackgroundWorker.CancellationPending Then Throw New OperationCanceledException(My.Resources.ex_OperationCancelled)
                        With DirectCast(worksheet.Cells(Me.Settings.ColumnNameRow, ColMap(i)), Excel.Range)
                            If .Value Is Nothing AndAlso Not CBool(.HasFormula) Then _
                                .Value = data.GetName(i)
                        End With
                    Next
                End If
                'Data
                Dim rowi As Integer = 0
                Dim rowiTotal% = 0
                If count >= 0 AndAlso context.BackgroundWorker IsNot Nothing Then
                    context.BackgroundWorker.ReportProgress(0, ProgressBarStyle.Blocks)
                    context.BackgroundWorker.ReportProgress(0, String.Format(My.Resources.msg_WritingTemplateList, Me.Name, worksheet.Name))
                ElseIf context.BackgroundWorker IsNot Nothing Then
                    context.BackgroundWorker.ReportProgress(-1, ProgressBarStyle.Marquee)
                    context.BackgroundWorker.ReportProgress(-1, String.Format(My.Resources.msg_WritingTemplateList, Me.Name, worksheet.Name))
                End If
                While data.Read
                    context.OnRead(data)
                    Dim OldList As Excel.Worksheet = worksheet
                    Select Case BeforeWriteRow(data, worksheet, context)
                        Case BeforeAction.Reset
                            FinishList(count, OldList, context, Col1, Coln, Row1, Rown, FilledColumns, False)
                            rowi = 0
                            Row = Row1
                        Case BeforeAction.Terminate : Exit While
                        Case Else 'Do nothing
                    End Select
                    If rowi = 0 AndAlso Settings.NameColumn <> "" Then
                        worksheet.Name = String.Format(Settings.NameFormat, data(data.GetOrdinal(Settings.NameColumn)))
                        'If(TypeOf Data Is OracleDataReader, _
                        '   GetValueFromOracle(Data, Data.GetOrdinal(Settings.NameColumn)), _
                        '   Data(Data.GetOrdinal(Settings.NameColumn))))
                        If context.BackgroundWorker IsNot Nothing Then context.BackgroundWorker.ReportProgress(-1, String.Format(My.Resources.msg_WritingTemplateList, Me.Name, worksheet.Name))
                    End If
                    If context.BackgroundWorker IsNot Nothing AndAlso context.BackgroundWorker.CancellationPending Then Throw New OperationCanceledException(My.Resources.ex_OperationCancelled)
                    If Me.Settings.InsertRows AndAlso rowi > 0 Then 'Copy a row
                        worksheet.Range(String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}:{0}", Row)).EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown, Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove)
                        worksheet.Range(String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}:{0}", Row - 1)).EntireRow.Copy(worksheet.Range(String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}:{0}", Row)))
                    End If
                    Dim Values As New List(Of Object)
                    Dim FirstJ% = -1
                    For j As Integer = 0 To data.FieldCount - 1
                        If SkipCollumns.Contains(data.GetName(j)) Then
                            'If FirstJ = 0 Then FirstJ = j + 1
                            Continue For
                        ElseIf FirstJ < 0 Then
                            FirstJ = j
                        End If
                        Dim Value As Object = data(j) 'If(TypeOf Data Is OracleDataReader, Helper.GetValueFromOracle(Data, j), Data(j))
                        If TypeOf Value Is Double OrElse TypeOf Value Is Single OrElse TypeOf Value Is Byte OrElse TypeOf Value Is SByte OrElse TypeOf Value Is Int16 OrElse TypeOf Value Is Int32 OrElse TypeOf Value Is Int64 OrElse TypeOf Value Is UInt16 OrElse TypeOf Value Is UInt32 OrElse TypeOf Value Is UInt64 OrElse TypeOf Value Is Decimal OrElse TypeOf Value Is Boolean OrElse TypeOf Value Is DateTime OrElse TypeOf Value Is Char OrElse TypeOf Value Is String Then
                            Values.Add(Value)
                        ElseIf TypeOf Value Is TimeSpan Then
                            Values.Add(DirectCast(Value, TimeSpan).TotalDays)
                        ElseIf TypeOf Value Is DBNull Then
                            Values.Add(Nothing)
                            'ElseIf TypeOf Value Is OracleClient.OracleDateTime Then
                            '    Values.Add(DirectCast(Value, OracleDateTime).Value)
                            'ElseIf TypeOf Value Is OracleClient.OracleMonthSpan Then
                            '    Values.Add(DirectCast(Value, OracleMonthSpan).Value)
                            'ElseIf TypeOf Value Is OracleClient.OracleNumber Then
                            '    Values.Add(EOS.Data.ToDecimal(Value))
                            'ElseIf TypeOf Value Is OracleClient.OracleString Then
                            '    Values.Add(DirectCast(Value, OracleString).Value)
                            'ElseIf TypeOf Value Is OracleClient.OracleTimeSpan Then
                            '    Values.Add(DirectCast(Value, OracleTimeSpan).Value)
                        Else
                            Values.Add(Value.ToString)
                        End If
                        Dim NextJ% = j + 1
                        While NextJ < data.FieldCount AndAlso SkipCollumns.Contains(data.GetName(NextJ))
                            NextJ += 1
                        End While
                        If NextJ >= data.FieldCount OrElse ColMap(j) + 1 <> ColMap(NextJ) Then
                            Dim ArrRange As Excel.Range = worksheet.Range(worksheet.Cells(Row, ColMap(FirstJ)), worksheet.Cells(Row, ColMap(j)))
                            ArrRange.Value = Values.ToArray
                            FirstJ = NextJ
                            Values.Clear()
                        End If
                        'Dim tRange As Excel.Range = DirectCast(List.Cells(Row, ColMap(j)), Excel.Range)
                    Next
                    Rown = Row
                    Row += 1 : rowi += 1 : rowiTotal += 1
                    If context.BackgroundWorker IsNot Nothing AndAlso count > 0 Then
                        context.BackgroundWorker.ReportProgress(rowiTotal / count * 100)
                    End If
                End While
            Finally
                If Settings.SuspendRecalculations Then
                    If context.BackgroundWorker IsNot Nothing Then context.BackgroundWorker.ReportProgress(-1, My.Resources.Recomputing)
                    worksheet.Application.Calculation = Excel.XlCalculation.xlCalculationAutomatic
                End If
            End Try
            FinishList(count, worksheet, context, Col1, Coln, Row1, Rown, FilledColumns, True)
        End Sub
        ''' <summary>Thius method is called before a row is written to a list - in case it's overriden in derived class  it allows to perform various actionsrùzné akce before a row is written</summary>
        ''' <param name="reader">A datasource pointing to current column</param>
        ''' <param name="worksheet">Current list (worksheet), when function returns it can change the list</param>
        ''' <param name="context">Context</param>
        ''' <returns>An action toi be performed by caller. This implementation always returns <see cref="BeforeAction.None"/></returns>
        ''' <version version="1.5.4">Arguments rernamed: <c>Reader</c> to <c>reader</c>, <c>List</c> to <c>worksheet</c>, <c>Context</c> to <c>context</c></version>
        ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False) (because <paramref name="worksheet"/> argument is not CLS-compliant</version>
        <CLSCompliant(False)>
        Protected Overridable Function BeforeWriteRow(ByVal reader As IDataReader, <[In](), Out()> ByRef worksheet As Excel.Worksheet, ByVal context As IReportGeneratorContext) As BeforeAction
            Return BeforeAction.None
        End Function

        ''' <summary>Possible actions to be taken before a row is written</summary>
        Protected Enum BeforeAction
            ''' <summary>Nothing</summary>
            None
            ''' <summary>Start again from 1st row</summary>
            Reset
            ''' <summary>Terminate (do not write this row neither write subsequent rows)</summary>
            Terminate
        End Enum

        ''' <summary>Performs finishbing operations on a worksheet</summary>
        ''' <param name="count">Total number of rows in data source (-1 if unknows)</param>
        ''' <param name="worksheet">A list (worksheet)</param>
        ''' <param name="context">Report-generation context</param>
        ''' <param name="col1">First filled collumn</param>
        ''' <param name="row1">First filled row</param>
        ''' <param name="coln">Number of columns written</param>
        ''' <param name="rown">Last written column</param>
        ''' <param name="filledColumns">Indexes of colllumns data were written to</param>
        ''' <param name="isEnd">True in case this operation is called because the datasource contains no more records, false otherwise</param>
        ''' <version version="1.5.4">Argument <c>list</c> renamed to <c>worksheet</c></version>
        ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False) (because <paramref name="worksheet"/> argument is not CLS-compliant</version>
        <CLSCompliant(False)>
        Protected Sub FinishList(ByVal count%, ByVal worksheet As Excel.Worksheet, ByVal context As IReportGeneratorContext, ByVal col1%, ByVal coln%, ByVal row1%, ByVal rown%, ByVal filledColumns As Integer(), ByVal isEnd As Boolean)
            'Finalizing operations
            If context.BackgroundWorker IsNot Nothing AndAlso isEnd Then
                If count < 0 Then context.BackgroundWorker.ReportProgress(100, ProgressBarStyle.Continuous)
                context.BackgroundWorker.ReportProgress(100, String.Format(My.Resources.msg_FinalizeTemplateList, worksheet.Name, Me.Name))
            End If
            'Autowdths
            For Each ColToSet As Integer In Settings.GetAutoWidthColumns(filledColumns)
                If context.BackgroundWorker IsNot Nothing AndAlso context.BackgroundWorker.CancellationPending Then Throw New OperationCanceledException(My.Resources.ex_OperationCancelled)
                CType(worksheet.Columns(ColToSet), Excel.Range).AutoFit()
            Next
            'Print area
            Dim PrintArea As Nullable(Of Rectangle) = Settings.GetPrintArea(col1, coln, row1, rown)
            If PrintArea.HasValue Then
                worksheet.PageSetup.PrintArea = worksheet.Range(worksheet.Cells(PrintArea.Value.Y, PrintArea.Value.X), worksheet.Cells(PrintArea.Value.Bottom, PrintArea.Value.Right)).Address(True, True, Excel.XlReferenceStyle.xlA1)
            End If
        End Sub
        ''' <summary>Gets a value which indicates if the template is intended for sub-reports or top-level reports</summary>
        ''' <returns>This implementation always returns <see cref="SubreportType.Both"/></returns>
        Private ReadOnly Property Subreport() As SubreportType Implements IReportTemplate.Subreport
            Get
                Return SubreportType.Both
            End Get
        End Property
        ''' <summary>Gets a list of <see cref="ShortName"/>s of templates. Reports of types returned can be used as sub-reports of this report.</summary>
        ''' <remarks>Ignored if <see cref="Subreport"/> is <see cref="SubreportType.TopLevelOnly"/></remarks>
        ''' <returns>Array of <see cref="ShortName"/>s. In case the template does not support subreports returns an empty array (not null!)</returns>
        Public Overridable ReadOnly Property ChildrenTemplateTypes() As String() Implements IReportTemplate.ChildrenTemplateTypes
            Get
                Return New String() {Me.ShortName, RepeatXlsTemplate.ShortName_rXLS}
            End Get
        End Property

        ''' <summary>Generates subreports</summary>
        ''' <param name="Workbook">Excel workbook</param>
        ''' <param name="Context">Context containing subreports</param>
        ''' <exception cref="IndexOutOfRangeException">List (worksheet) is set by index ant there are not enough worksheet in the workbook</exception>
        ''' <exception cref="NotSupportedException">Sub-report template is not suported. -or- The procedure for sub-report data preparation requires a parameter of type cursor or count.</exception>
        ''' <exception cref="OperationCanceledException">Background operation was cancelled</exception>
        ''' <version version="1.5.4">Arguments renamed: All firts letters changed to lowercase</version>
        Private Sub Subreports(ByVal workbook As Excel.Workbook, ByVal context As IReportGeneratorContext)
            For Each subreport As ISubReport In Context.Subreports
                If Context.BackgroundWorker IsNot Nothing AndAlso Context.BackgroundWorker.CancellationPending Then Throw New OperationCanceledException(My.Resources.ex_OperationCancelled)
                If Context.BackgroundWorker IsNot Nothing Then
                    Context.BackgroundWorker.ReportProgress(-1, ProgressBarStyle.Continuous)
                    Context.BackgroundWorker.ReportProgress(-1, String.Format(My.Resources.msg_SubreportsOfTemplate, Me.Name))
                End If
                Select Case subreport.Template.ShortName
                    Case ShortName_XLS, RepeatXlsTemplate.ShortName_rXLS
                        Dim SubTemplate As SimpleXlsTemplate = subreport.Template
                        Dim ListIndex%
                        Dim SubTemplateList As Excel.Worksheet

                        If Me.Settings.List = "" Then
                            SubTemplateList = Workbook.Worksheets.Add(After:=Workbook.Worksheets(Workbook.Worksheets.Count))
                        ElseIf Integer.TryParse(SubTemplate.Settings.List, Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, ListIndex) Then
                            If ListIndex > Workbook.Worksheets.Count Then Throw New IndexOutOfRangeException(My.Resources.ex_ExcelListIndexOutOfRange & ListIndex)
                            SubTemplateList = Workbook.Worksheets.Item(ListIndex)
                        Else
                            SubTemplateList = Workbook.Worksheets.Item(SubTemplate.Settings.List)
                        End If
                        Dim Count%
                        SubTemplate.Export(subreport.GetData(Count), Count, SubTemplateList, Context)
                    Case Else
                        Throw New NotSupportedException(String.Format(My.Resources.ex_UnsupportedSubreportType, subreport.Template.Name, subreport.Template.ShortName, Me.Name, Me.ShortName))
                End Select
            Next
        End Sub

        Public Sub New()
            _Settings = NewSettings()
        End Sub


        ''' <summary>Default extension (without leading tod (.)) foir saving a file</summary>
        ''' <remarks>Wefault extension depends on Excel version installled (xls for 2003 and older, xlsx for 2007 and newer)</remarks>
        Public ReadOnly Property DefaultExtension() As String Implements IReportTemplate.DefaultExtension
            Get
                Return If(ExcelVersion >= Version2007, "xlsx", "xls")
            End Get
        End Property
        Private Shared _ExcelVersion As Version
        Private Shared excelVersionLock As New Object
        ''' <summary>Contains version number for Excel 2010 (14)</summary>
        Protected Shared ReadOnly Version2010 As New Version(14, 0)
        ''' <summary>Contains version number for Excel 2007 (12)</summary>
        Protected Shared ReadOnly Version2007 As New Version(12, 0)
        ''' <summary>Contains version number for Excel 2003 (11)</summary>
        Protected Shared ReadOnly Version2003 As New Version(11, 0)
        ''' <summary>Contains version number for Excel XP (2002, 10)</summary>
        Protected Shared ReadOnly VersionXP As New Version(10, 0)
        ''' <summary>Contains version number for Excel 2000 (9)</summary>
        Protected Shared ReadOnly Version2000 As New Version(9, 0)
        ''' <summary>Contains version number for Excel 97 (8)</summary>
        Protected Shared ReadOnly Version97 As New Version(8, 0)
        ''' <summary>Gets version of Excel used by this report exporter</summary>
        ''' <the>This property is thread-safe</the>
        Public Shared ReadOnly Property ExcelVersion As Version
            Get
                If _ExcelVersion Is Nothing Then
                    SyncLock excelVersionLock
                        If _ExcelVersion Is Nothing Then
                            Dim app As New Excel.Application
                            Try
                                _ExcelVersion = Version.Parse(app.Version)
                            Finally
                                app.Quit()
                            End Try
                        End If
                    End SyncLock
                End If
                Return _ExcelVersion
            End Get
        End Property

        ''' <summary>Postprocesses an Excel file using given Visual Basic code</summary>
        ''' <param name="templateFile">A workbook to postprocess</param>
        ''' <param name="postProcessingCode">A Visual Basic code</param>
        ''' <exception cref="CompilerErrorException">There is an error in the code</exception>
        ''' <remarks><paramref name="postProcessingCode"/> is body of procedure (sub) surrounded by some code (see <c>Tools.ReportingT.ReportingEngineLite.ReportPostProcessingFile.vb</c> embdeded resource), the code is:
        ''' <code lang="VB"><![CDATA[Imports System
        ''' Imports System.Collections
        ''' Imports System.Collections.Generic
        ''' Imports Microsoft.Office.Interop.Excel
        ''' Imports Microsoft.Office.Core
        ''' Public Module ExcelReportPostProcessing
        '''     Public Sub DoPostprocessing(ByVal WorkBook As Workbook)
        '''         ']]><paramref name="postProcessingCode"/><![CDATA[goes here
        '''     End Sub
        ''' End Module]]></code>
        ''' </remarks>
        Private Sub DoPostProcessing(ByVal templateFile As Excel.Workbook, ByVal postProcessingCode As String)
            'ExcelReportPostProcessing.DoPostprocessing(TemplateFile)
            Dim VBCompiler As New Microsoft.VisualBasic.VBCodeProvider
            Dim ReportPostProcessingFile = GetType(SimpleXlsTemplate).Assembly.GetManifestResourceStream("EOS.Reporting.ReportPostProcessingFile.vb")
            Dim Reader As New IO.StreamReader(ReportPostProcessingFile)
            Dim VBCode = String.Format(Reader.ReadToEnd, postProcessingCode)
#If DEBUG Then
            Const Debug As Boolean = True
#Else
                        Const Debug As Boolean = false
#End If
            Dim CompilerParams As System.CodeDom.Compiler.CompilerParameters = New CodeDom.Compiler.CompilerParameters(New String() { _
                                GetType(Microsoft.Office.Interop.Excel.Application).Assembly.Location, _
                                GetType(Microsoft.VisualBasic.VBCodeProvider).Assembly.Location, _
                                GetType(Object).Assembly.Location, _
                                GetType(System.Data.DataException).Assembly.Location, _
                                GetType(System.Xml.XmlDocument).Assembly.Location, _
                                GetType(System.Xml.Linq.XDocument).Assembly.Location, _
                                GetType(System.Linq.Enumerable).Assembly.Location, _
                                GetType(Tools.TimeSpanFormattable).Assembly.Location, _
                                GetType(SimpleXlsTemplate).Assembly.Location})
            CompilerParams.IncludeDebugInformation = Debug
            CompilerParams.GenerateInMemory = True
            CompilerParams.CompilerOptions = "/optionexplicit"
            Dim Compiled = VBCompiler.CompileAssemblyFromSource( _
                CompilerParams, _
                VBCode)
            If Compiled.Errors.Count > 0 Then
                Dim CountErrors% = 0
                Dim FirstError As CodeDom.Compiler.CompilerError = Nothing
                For Each ex As CodeDom.Compiler.CompilerError In Compiled.Errors
                    If Log IsNot Nothing Then Log.Add(If(ex.IsWarning, LogSeverity.Warning, LogSeverity.Error), My.Resources.msg_CompilerWarningLog, ex.Line, ex.Column, ex.ErrorNumber, ex.ErrorText)
                    If Not ex.IsWarning Then
                        CountErrors += 1
                        If FirstError Is Nothing Then FirstError = ex
                    End If
                Next
                If CountErrors > 0 Then
                    Throw New CompilerErrorException(Compiled.Errors)
                End If
            End If
            Try
                Compiled.CompiledAssembly.GetType("ExcelReportPostProcessing").GetMethod("DoPostprocessing").Invoke(Nothing, New Object() {templateFile})
            Catch ex As Reflection.TargetInvocationException When ex.InnerException IsNot Nothing
                If Log IsNot Nothing Then Log.Add(LogSeverity.Error, My.Resources.msg_ErrorInUserCode, ex.InnerException.GetType.Name, ex.InnerException.Message, ex.InnerException.StackTrace)
                Throw New Reflection.TargetInvocationException(String.Format(My.Resources.ex_ErrorInUserPostprocessingCode, ex.InnerException.GetType.Name, ex.InnerException.Message), ex)
            End Try
        End Sub
    End Class

    ''' <summary>Template for Excel-based report containing repeated worksheets (lists)</summary>
    Public Class RepeatXlsTemplate
        Inherits SimpleXlsTemplate
        Public Const ShortName_rXLS As String = "MultiXLS"
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Overrides Function Clone() As IReportTemplate
            Dim ret As New RepeatXlsTemplate
            ret.Settings = Me.Settings.Clone
            ret.Log = Me.Log
            Return ret
        End Function
        ''' <summary>Creates a new instance of <see cref="RepeatXlsTemplate"/></summary>
        ''' <returns>A new instance of <see cref="RepeatXlsTemplate"/></returns>
        Protected Overrides Function NewSettings() As SimpleXlsSettings
            Return New RepeatedXlsSettings
        End Function
        ''' <summary>Human-friendly Template name (as shown to the user)</summary>
        Public Overrides ReadOnly Property Name() As String
            Get
                Return My.Resources.ExcelRepeated
            End Get
        End Property
        ''' <summary>Machine-friendly template name (as identified by computer)</summary>
        Public Overrides ReadOnly Property ShortName() As String
            Get
                Return ShortName_rXLS
            End Get
        End Property
        ''' <summary>Index of current row</summary>
        Dim rowI% = -1
        ''' <summary>Row value</summary>
        Dim rowValue As Object = Nothing
        ''' <summary>reference to original list (worksheet)</summary>
        Dim originalList As Excel.Worksheet
        ''' <summary>Exports data to selected Excel list</summary>
        ''' <param name="data">Data to export</param>
        ''' <param name="count">Number of rows in<paramref name="Data"/>, -1 if unknown</param>
        ''' <param name="list">Target list</param>
        ''' <param name="context">A context</param>
        ''' <exception cref="OperationCanceledException">Background operation was cancelled</exception>
        <CLSCompliant(False)>
        Protected Overrides Sub Export(ByVal data As IDataReader, ByVal count As Integer, ByVal list As Excel.Worksheet, ByVal context As IReportGeneratorContext)
            RowI = -1
            rowValue = Nothing
            originalList = Nothing
            MyBase.Export(data, count, list, context)
            If originalList IsNot Nothing Then
                originalList.Delete()
                originalList = Nothing
            End If
        End Sub

        ''' <summary>Gets names of datasource columns to ignore</summary>
        ''' <returns>Names of columns in datasource to ignore</returns>
        Protected Overrides ReadOnly Property SkippedColumns() As System.Collections.Generic.List(Of String)
            Get
                Dim ret As List(Of String) = MyBase.SkippedColumns
                If Not Settings.WriteBreak Then ret.Add(Settings.BreakColumn)
                If Not Settings.WriteName Then ret.Add(Settings.NameColumn)
                Return ret
            End Get
        End Property
        ''' <summary>Gets or sets settings of this templae</summary>
        Public Overloads Property Settings() As RepeatedXlsSettings
            Get
                Return MyBase.Settings
            End Get
            Protected Set(ByVal value As RepeatedXlsSettings)
                MyBase.Settings = value
            End Set
        End Property
        ''' <summary>Thius method is called before a row is written to a list - in case it's overriden in derived class  it allows to perform various actionsrùzné akce before a row is written</summary>
        ''' <param name="reader">A datasource pointing to current column</param>
        ''' <param name="worksheet">Current list (worksheet), when function returns it can change the list</param>
        ''' <param name="context">Context</param>
        ''' <returns>An action toi be performed by caller. This implementation always returns <see cref="BeforeAction.None"/></returns>
        ''' <version version="1.5.4">Parameters renamed: <c>Reader</c> to <c>reader</c>, <c>List</c> to <c>worksheet</c>, <c>Context</c> to <c>context</c></version>
        ''' <version version="1.5.4">Added <see cref="CLSCompliantAttribute"/>(False) (because <paramref name="worksheet"/> argument is not CLS-compliant</version>
        <CLSCompliant(False)>
        Protected Overrides Function BeforeWriteRow(ByVal reader As IDataReader, ByRef worksheet As Microsoft.Office.Interop.Excel.Worksheet, ByVal context As IReportGeneratorContext) As SimpleXlsTemplate.BeforeAction
            rowI += 1
            If rowI = 0 Then originalList = worksheet
            Dim Reset As Boolean = rowI = 0
            Dim Value As Object = Nothing
            Dim idx%
            Try
                idx = reader.GetOrdinal(Settings.BreakColumn)
            Catch ex As IndexOutOfRangeException
                idx = -1
            End Try
            'If idx >= 0 AndAlso TypeOf Reader Is OracleDataReader Then
            '    Value = DirectCast(Reader, OracleDataReader).GetOracleValue(idx)
            'Else
            If idx >= 0 Then
                Value = reader.GetValue(idx)
            End If
            If idx >= 0 AndAlso rowI > 0 Then
                If (rowValue Is Nothing) <> (Value Is Nothing) OrElse (rowValue IsNot Nothing AndAlso Not rowValue.Equals(Value)) Then
                    Reset = True
                End If
            End If
            Dim ret As BeforeAction = BeforeAction.None
            If Reset Then
                originalList.Copy(After:=worksheet)
                worksheet = worksheet.Next
                'List.Name = Reader(Settings.NameColumn).ToString
                ret = BeforeAction.Reset
                'If Context.BackgroundWorker IsNot Nothing Then Context.BackgroundWorker.ReportProgress(-1, String.Format("Zápis dat šablony {0}, list {1}", Me.Name, List.Name))
            End If
            rowValue = Value
            Return ret
        End Function
        ''' <summary>Gets an instance of settings editor control</summary>
        ''' <returns>A new instance of control for editing current report settings</returns>
        ''' <remarks>The control must be prepared for being resized to any size.</remarks>
        Public Overrides Function GetConfigEditor() As System.Windows.Forms.Control
            Dim ret As New PropertyGridEditor(Of RepeatedXlsSettings)(Me.Settings)
            Return ret
        End Function
    End Class
End Namespace