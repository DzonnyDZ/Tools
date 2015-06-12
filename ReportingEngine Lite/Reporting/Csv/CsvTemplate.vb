Imports System.Data.Common
Imports Tools

Namespace ReportingT.ReportingEngineLite
    ''' <summary>Export template for CSV (Comma-Separated Values) format</summary>
    Public Class CsvTemplate
        Implements IReportTemplate
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Function Clone() As IReportTemplate Implements IReportTemplate.Clone
            Dim ret As New CsvTemplate
            ret._Settings = Me.Settings.Clone
            ret.Log = Me.Log
            Return ret
        End Function
        Private _settings As New CsvTemplateSettings
        ''' <summary>Gets current settings</summary>
        Public ReadOnly Property Settings() As CsvTemplateSettings
            <DebuggerStepThrough()> Get
                Return _Settings
            End Get
        End Property

        ''' <summary>Gets an instance of settings editor control</summary>
        ''' <returns>A new instance of control for editing current report settings</returns>
        Public Function GetConfigEditor() As System.Windows.Forms.Control Implements IReportTemplate.GetConfigEditor
            Dim ret As New CsvSettingsEditor(Me.Settings)
            Return ret
        End Function
        ''' <summary>Retturns template settings as XML document</summary>
        ''' <returns>A XML document representing template settings</returns>
        Public Function GetSettings() As System.Xml.XmlDocument Implements IReportTemplate.GetSettings
            Dim s As New Xml.Serialization.XmlSerializer(GetType(CsvTemplateSettings))
            Dim ms As New IO.MemoryStream
            s.Serialize(ms, Settings)
            ms.Flush()
            ms.Position = 0
            Dim ret As New Xml.XmlDocument()
            ret.Load(ms)
            Return ret
        End Function
        ''' <summary>Gets a list of <see cref="ShortName"/>s of templates. Reports of types returned can be used as sub-reports of this report.</summary>
        ''' <remarks>Ignored if <see cref="Subreport"/> is <see cref="SubreportType.TopLevelOnly"/></remarks>
        ''' <returns>This implementation returns an empty array</returns>
        Public ReadOnly Property ChildrenTemplateTypes() As String() Implements IReportTemplate.ChildrenTemplateTypes
            Get
                Return New String() {}
            End Get
        End Property
        ''' <summary>Initializes template settings from a XML document</summary>
        ''' <param name="settings">A settings</param>
        ''' <exception cref="ArgumentNullException"><paramref name="settings"/> is null</exception>
        Public Sub Init(ByVal settings As System.Xml.XmlDocument) Implements IReportTemplate.Init
            If settings Is Nothing Then Throw New ArgumentNullException("settings")
            Dim s As New Xml.Serialization.XmlSerializer(GetType(CsvTemplateSettings))
            Dim str As New IO.MemoryStream
            settings.Save(str)
            str.Flush()
            str.Position = 0
            _settings = s.Deserialize(str)
        End Sub
        ''' <summary>Initializes template settings from a XML element in form of <see cref="Xml.Linq.XElement"/></summary>
        ''' <param name="settings">A settings</param>
        ''' <exception cref="ArgumentNullException"><paramref name="settings"/> is null</exception>
        Public Sub Init(ByVal settings As Xml.Linq.XElement) Implements IReportTemplate.Init
            If settings Is Nothing Then Throw New ArgumentNullException("settings")
            Dim s As New Xml.Serialization.XmlSerializer(GetType(CsvTemplateSettings))
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
        Public ReadOnly Property Name() As String Implements IReportTemplate.Name
            Get
                Return My.Resources.CSV
            End Get
        End Property

        ''' <summary>a mask for saving files</summary>
        ''' <remarks>In case null or an empty string is returned user will not be alloewed to select file for saving</remarks>
        ''' <seealso cref="OpenFileDialog.Filter"/>
        Public ReadOnly Property SaveFileMask() As String Implements IReportTemplate.SaveFileMask
            Get
                Return My.Resources.filter_CSVTSVTSXT
            End Get
        End Property

        ''' <summary>Machine-friendly template name (as identified by computer)</summary>
        Public ReadOnly Property ShortName() As String Implements IReportTemplate.ShortName
            Get
                Return "CSV"
            End Get
        End Property

        ''' <summary>Gets a value which indicates if the template is intended for sub-reports or top-level reports</summary>
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
                Return My.Resources.filter_CSVTSVTSXT
            End Get
        End Property
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Function Clone_ICloneable() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function


        ''' <summary>Performs the export</summary>
        ''' <param name="data">Data comming form database to be exported</param>
        ''' <param name="count">Expected number of rows in <paramref name="data"/> to be exported. Used for progress reporting. -1 if unknown.</param>
        ''' <param name="templatePath">Path of template file (if used)</param>
        ''' <param name="targetFilePath">Path of file to save results to (if used)</param>
        ''' <param name="openWhenDone">True to open file when export finishes</param>
        ''' <param name="context">Report context. The template MUST call <see cref="IReportGeneratorContext.OnRead"/> for every line read from <paramref name="data"/>!</param>
        ''' <exception cref="Exception">There was an error during export</exception>
        ''' <exception cref="OperationCanceledException">Background operation was cancelled (using <paramref name="Context"/>.<see cref="IReportGeneratorContext.BackgroundWorker">BackgroundWorker</see>.<see cref="System.ComponentModel.BackgroundWorker.CancellationPending">CancellationPending</see>)</exception>
        Public Sub Export(ByVal Data As IDataReader, ByVal Count As Integer, ByVal TemplatePath As String, ByVal TargetFilePath As String, ByVal OpenWhenDone As Boolean, ByVal Context As IReportGeneratorContext) Implements IReportTemplate.Export
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

            Dim Encoding As System.Text.Encoding = Settings.GetEncoding
            Using OutFile As IO.FileStream = IO.File.Open(TargetFilePath, IO.FileMode.Create, IO.FileAccess.ReadWrite, IO.FileShare.Read), _
                    w As New IO.StreamWriter(OutFile, Encoding)
                Dim Culture As System.Globalization.CultureInfo = Settings.GetCulture
                Dim NewLine$ = Settings.GetNewLine
                Dim AfterWrite$ = ""
                If TemplatePath <> "" Then
                    Dim Template$ = My.Computer.FileSystem.ReadAllText(TemplatePath, Encoding)
                    Dim Lines() As String = Template.Split(New String() {NewLine}, StringSplitOptions.None)
                    For i As Integer = 0 To Math.Max(Settings.headersize - 1, Lines.Length - 1)
                        w.Write(Lines(i) & NewLine)
                    Next
                    Dim ABB As New System.Text.StringBuilder
                    For i As Integer = Settings.headersize To Lines.Length - 1
                        ABB.Append(Lines(i) & NewLine)
                    Next
                    If ABB.Length <> 0 Then
                        AfterWrite = NewLine & ABB.ToString
                    End If
                End If

                If Settings.header Then
                    For i As Integer = 0 To Data.FieldCount - 1
                        If i > 0 Then w.Write(Settings.Separator)
                        WriteString(Data.GetName(i), w)
                    Next
                    w.Write(NewLine)
                End If

                If Context.BackgroundWorker IsNot Nothing Then
                    If Count >= 0 Then Context.BackgroundWorker.ReportProgress(0, ProgressBarStyle.Blocks)
                    Context.BackgroundWorker.ReportProgress(-1, String.Format(My.Resources.msg_GeneratingTemplate, Me.Name))
                End If

                Dim datai% = 0
                Do While Data.Read
                    If Context.BackgroundWorker IsNot Nothing AndAlso Context.BackgroundWorker.CancellationPending Then Throw New OperationCanceledException(My.Resources.ex_OperationCancelled)
                    Context.OnRead(Data)
                    If datai > 0 Then w.Write(NewLine)
                    For i As Integer = 0 To Data.FieldCount - 1
                        If i > 0 Then w.Write(Settings.Separator)
                        Dim Value As Object = Data(i) 'If(TypeOf Data Is OracleDataReader, Helper.GetValueFromOracle(Data, i), Data(i))
                        If TypeOf Value Is DBNull Then
                            Value = Settings.nullvalue
                            'ElseIf TypeOf Value Is OracleClient.OracleDateTime Then
                            '    Value = DirectCast(Value, OracleDateTime).Value
                            'ElseIf TypeOf Value Is OracleClient.OracleMonthSpan Then
                            '    Value = DirectCast(Value, OracleMonthSpan).Value
                            'ElseIf TypeOf Value Is OracleClient.OracleNumber Then
                            '    Value = EOS.Data.ToDecimal(Value)
                            'ElseIf TypeOf Value Is OracleClient.OracleString Then
                            '    Value = DirectCast(Value, OracleString).Value
                            'ElseIf TypeOf Value Is OracleClient.OracleTimeSpan Then
                            '    Value = DirectCast(Value, OracleTimeSpan).Value
                        End If

                        If TypeOf Value Is Date Then
                            WriteNumber(DirectCast(Value, Date).ToString(Settings.dateformat, Culture), w)
                        ElseIf TypeOf Value Is TimeSpan Then
                            WriteNumber(CType(DirectCast(Value, TimeSpan), TimeSpanFormattable).ToString(Settings.timeformat, Culture), w)
                        ElseIf TypeOf Value Is Byte OrElse TypeOf Value Is SByte OrElse TypeOf Value Is Short OrElse TypeOf Value Is UShort OrElse TypeOf Value Is Integer OrElse TypeOf Value Is UInteger OrElse TypeOf Value Is Long OrElse TypeOf Value Is ULong OrElse TypeOf Value Is Single OrElse TypeOf Value Is Double OrElse TypeOf Value Is Decimal Then
                            WriteNumber(DirectCast(Value, IFormattable).ToString(Settings.numberformat, Culture), w)
                        ElseIf Value Is Nothing Then
                            WriteString("", w)
                        Else
                            WriteString(Value.ToString, w)
                        End If
                    Next

                    datai += 1

                    If Context.BackgroundWorker IsNot Nothing AndAlso Count > 0 Then
                        Context.BackgroundWorker.ReportProgress(datai / Count * 100)
                    End If
                Loop

                If Context.BackgroundWorker IsNot Nothing AndAlso Count >= 0 Then
                    Context.BackgroundWorker.ReportProgress(100)
                End If

                If Settings.footer Then
                    For i As Integer = 0 To Data.FieldCount - 1
                        If i > 0 Then w.Write(Settings.Separator)
                        WriteString(Data.GetName(i), w)
                    Next
                End If
                w.Write(AfterWrite)
            End Using
            If OpenWhenDone Then
                Try : Process.Start(TargetFilePath) : Catch : End Try
            End If
        End Sub
        ''' <summary>Writes a string</summary>
        ''' <param name="str">A string</param>
        ''' <param name="w">Where to write <paramref name="w"/></param>
        Private Sub WriteString(ByVal str As String, ByVal w As IO.TextWriter)
            w.Write(Escape(str, Settings.textqualifierusage = CSVTemplateSettingsTextqualifierusage.always OrElse Settings.textqualifierusage = CSVTemplateSettingsTextqualifierusage.alwaysontext))
        End Sub
        ''' <summary>Writes date or number</summary>
        ''' <param name="str">Formatted date or number</param>
        ''' <param name="w">Where to write <paramref name="str"/></param>
        Private Sub WriteNumber(ByVal str As String, ByVal w As IO.TextWriter)
            w.Write(Escape(str, Settings.textqualifierusage = CSVTemplateSettingsTextqualifierusage.always))
        End Sub
        ''' <summary>Escapes a srting</summary>
        ''' <param name="str">A string</param>
        ''' <param name="Wrap">True to quote</param>
        ''' <returns>Esacped and possibly also quoted <paramref name="str"/></returns>
        Private Function Escape(ByVal str As String, ByVal Wrap As Boolean) As String
            Wrap = Wrap OrElse str.Contains(Settings.Separator)
            Dim EscDone As Boolean = False
            Dim AmpDone As Boolean = False
            Dim EscQ$ = ""
            Dim EscN$ = ""
            If str.Contains(Settings.textqualifier) Then
                Wrap = True
                Select Case Settings.textqualifierescape
                    Case CSVTemplateSettingsTextqualifierescape.escape
                        str = str.Replace("\", "\\")
                        str = str.Replace(CStr(Settings.textqualifier), "\" & Settings.textqualifier)
                        EscDone = True
                        EscQ = "\"
                    Case CSVTemplateSettingsTextqualifierescape.html
                        str = str.Replace("&", "&amp;")
                        str.Replace(CStr(Settings.textqualifier), "&#x" & Hex(AscW(Settings.textqualifier)) & ";")
                        AmpDone = True
                        EscQ = "&"
                    Case CSVTemplateSettingsTextqualifierescape.double : str = str.Replace(CStr(Settings.textqualifier), Settings.textqualifier & Settings.textqualifier)
                    Case Else 'donothing 
                End Select
            End If
            Dim NlReplace As New Dictionary(Of String, String)
            Dim NewLine As String = Settings.GetNewLine
            Select Case Settings.nlescape
                Case CSVTemplateSettingsNlescape.escape : EscN = "\"
                    If NewLine = vbCrLf Then NlReplace.Add(NewLine, "\" & vbCr & "\" & vbLf) Else NlReplace.Add(NewLine, "\" & NewLine)
                Case CSVTemplateSettingsNlescape.escapeall : EscN = "\"
                    If Not EscDone Then NlReplace.Add("\", "\\")
                    NlReplace.Add(vbCr, "\" & vbCr)
                    NlReplace.Add(vbLf, "\" & vbLf)
                    NlReplace.Add(ChrW(&HC), "\" & ChrW(&HC))
                    NlReplace.Add(ChrW(&H2028), "\" & ChrW(&H2028))
                    NlReplace.Add(ChrW(&H85), "\" & ChrW(&H85))
                    NlReplace.Add(ChrW(&H2029), "\" & ChrW(&H2029))
                Case CSVTemplateSettingsNlescape.replaceallwithescape : EscN = "\"
                    If Not EscDone Then NlReplace.Add("\", "\\")
                    NlReplace.Add(vbCr, "\r")
                    NlReplace.Add(vbLf, "\n")
                    NlReplace.Add(ChrW(&HC), "\0xC")
                    NlReplace.Add(ChrW(&H2028), "\0x2028")
                    NlReplace.Add(ChrW(&H85), "\0x85")
                    NlReplace.Add(ChrW(&H2029), "\0x2029")
                Case CSVTemplateSettingsNlescape.replacewithescape : EscN = "\"
                    If Not EscDone Then NlReplace.Add("\", "\\")
                    If NewLine = vbCrLf Then
                        NlReplace.Add(vbCrLf, "\r\n")
                    Else
                        NlReplace.Add(NewLine, "\0x" & Hex(AscW(NewLine)))
                    End If
                Case CSVTemplateSettingsNlescape.specialreplace
                    Select Case Settings.textqualifierescape
                        Case CSVTemplateSettingsTextqualifierescape.double
                            NlReplace.Add(Settings.specialstring, Settings.specialstring & Settings.specialstring)
                        Case CSVTemplateSettingsTextqualifierescape.escape
                            NlReplace.Add(Settings.specialstring, "\" & Settings.specialstring)
                        Case CSVTemplateSettingsTextqualifierescape.html
                            Dim rstr$ = ""
                            For Each ch As Char In Settings.specialstring
                                rstr &= "&#x" & Hex(AscW(ch)) & ";"
                            Next
                            NlReplace.Add(Settings.specialstring, rstr)
                    End Select
                    NlReplace.Add(NewLine, Settings.specialstring)
                Case CSVTemplateSettingsNlescape.specialreplaceall
                    Select Case Settings.textqualifierescape
                        Case CSVTemplateSettingsTextqualifierescape.double
                            NlReplace.Add(Settings.specialstring, Settings.specialstring & Settings.specialstring)
                        Case CSVTemplateSettingsTextqualifierescape.escape
                            NlReplace.Add(Settings.specialstring, "\" & Settings.specialstring)
                        Case CSVTemplateSettingsTextqualifierescape.html
                            Dim rstr$ = ""
                            For Each ch As Char In Settings.specialstring
                                rstr &= "&#x" & Hex(AscW(ch)) & ";"
                            Next
                            NlReplace.Add(Settings.specialstring, rstr)
                    End Select
                    NlReplace.Add(vbCr, Settings.specialstring)
                    NlReplace.Add(vbLf, Settings.specialstring)
                    NlReplace.Add(ChrW(&HC), Settings.specialstring)
                    NlReplace.Add(ChrW(&H2028), Settings.specialstring)
                    NlReplace.Add(ChrW(&H85), Settings.specialstring)
                    NlReplace.Add(ChrW(&H2029), Settings.specialstring)
                Case CSVTemplateSettingsNlescape.strip
                    NlReplace.Add(NewLine, " ")
                Case CSVTemplateSettingsNlescape.stripall
                    NlReplace.Add(vbCr, " ")
                    NlReplace.Add(vbLf, " ")
                    NlReplace.Add(ChrW(&HC), " ")
                    NlReplace.Add(ChrW(&H2028), " ")
                    NlReplace.Add(ChrW(&H85), " ")
                    NlReplace.Add(ChrW(&H2029), " ")
                Case CSVTemplateSettingsNlescape.html
                    If Not AmpDone Then NlReplace.Add("&", "&amp;") : EscN = "&"
                    If NewLine = vbCrLf Then
                        NlReplace.Add(vbCrLf, "&#xD;&#xA;")
                    Else
                        NlReplace.Add(NewLine, "&#x" & Hex(ChrW(NewLine)) & ";")
                    End If
                Case CSVTemplateSettingsNlescape.htmlall
                    If Not AmpDone Then NlReplace.Add("&", "&amp;") : EscN = "&"
                    NlReplace.Add(vbCr, "&#xD;")
                    NlReplace.Add(vbLf, "&#xA;")
                    NlReplace.Add(ChrW(&HC), "&#xC;")
                    NlReplace.Add(ChrW(&H2028), "&#x2028;")
                    NlReplace.Add(ChrW(&H85), "&#x85;")
                    NlReplace.Add(ChrW(&H2029), "&#x2029;")
                Case CSVTemplateSettingsNlescape.donothing
                    Wrap = True
            End Select
            Dim NeedReplace As Boolean = False
            For Each kwp As KeyValuePair(Of String, String) In NlReplace
                If kwp.Key = "\" OrElse kwp.Key = "&" Then Continue For
                NeedReplace = NeedReplace Or str.Contains(kwp.Key)
            Next
            If NeedReplace Then
                If EscQ = EscN AndAlso EscQ <> "" Then Wrap = True
                For Each kwp As KeyValuePair(Of String, String) In NlReplace
                    str = str.Replace(kwp.Key, kwp.Value)
                Next
            End If
            If Wrap Then Return Settings.textqualifier & str & Settings.textqualifier
            Return str
        End Function

        ''' <summary>Default extension (without leading tod (.)) foir saving a file</summary>
        ''' <remarks>Not used when <see cref="SaveFileMask"/> is null or an empty string</remarks>
        Public ReadOnly Property DefaultExtension() As String Implements IReportTemplate.DefaultExtension
            Get
                Select Case Settings.Separator
                    Case vbTab : Return "tsv"
                    Case Else : Return "csv"
                End Select
            End Get
        End Property

        Private Shared _xmlSchema As Xml.Schema.XmlSchema
        Private Shared xmlSchemaLockObject As New Object
        ''' <summary>Gets XML schema used for storing settings of <see cref="SimpleXlsSettings"/> and <see cref="RepeatedXlsSettings"/>.</summary>
        Public Shared ReadOnly Property XmlSchema As Xml.Schema.XmlSchema
            Get
                If _xmlSchema Is Nothing Then
                    SyncLock xmlSchemaLockObject
                        If _xmlSchema Is Nothing Then
                            Dim stream = GetType(SimpleXlsTemplate).Assembly.GetManifestResourceStream("Tools.ReportingT.ReportingEngineLite.CsvTemplateSettings.xsd")
                            _xmlSchema = Xml.Schema.XmlSchema.Read(stream, Nothing)
                        End If
                    End SyncLock
                End If
                Return _xmlSchema
            End Get
        End Property
    End Class
End Namespace