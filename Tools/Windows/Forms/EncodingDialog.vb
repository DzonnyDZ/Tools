Imports System.Windows.Forms, System.ComponentModel, System.Text
Imports Tools.ComponentModel, Tools.Windows.Forms.Utilities
'#If Config <= Nightly Then
'Stage: Nightly
'Conditional compilation directive is commented out because its presence caused compiler warning.
'The conditionality of compilation of this file as well as of related files (which's name starts with 'LinkLabel.') is ensured by editing the Tools.vbproj file, where this file is marked as conditionally compiled.
'To edit the Tools.vbproj right-click the Tools project and select Unload Project. Then right-click it again and select Edit Tools.vbproj.
'Search for line like following:
'<Compile Include="Windows\Forms\EncodingDialog.vb" Condition="$(Config)&lt;=$(Release)">
'Its preceded by comment.
Namespace Windows.Forms
    ''' <summary>Representf dialog shown by <see cref="EncodingDialog"/></summary>
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Public Class frmEncodingDialog : Inherits Form  'TODO: Move into EncodingDialog and mark Protected
#Region "Designer generated"
        Protected WithEvents ensMain As Tools.Windows.Forms.EncodingSelector
        Protected WithEvents splMain As System.Windows.Forms.SplitContainer
        Protected WithEvents totToolTip As System.Windows.Forms.ToolTip
        Private components As System.ComponentModel.IContainer
        Protected WithEvents txtPreview As System.Windows.Forms.TextBox
        Protected WithEvents bgwTestEncoding As System.ComponentModel.BackgroundWorker
        Protected WithEvents chkOK As System.Windows.Forms.CheckBox
        Protected WithEvents fraPreview As System.Windows.Forms.GroupBox
        Protected WithEvents cmdCancel As System.Windows.Forms.Button
        Protected WithEvents cmdOK As System.Windows.Forms.Button
        Protected WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
        <DebuggerNonUserCode()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEncodingDialog))
            Me.ensMain = New Tools.Windows.Forms.EncodingSelector
            Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel
            Me.cmdCancel = New System.Windows.Forms.Button
            Me.cmdOK = New System.Windows.Forms.Button
            Me.chkOK = New System.Windows.Forms.CheckBox
            Me.fraPreview = New System.Windows.Forms.GroupBox
            Me.txtPreview = New System.Windows.Forms.TextBox
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.bgwTestEncoding = New System.ComponentModel.BackgroundWorker
            Me.tlpButtons.SuspendLayout()
            Me.fraPreview.SuspendLayout()
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'ensMain
            '
            resources.ApplyResources(Me.ensMain, "ensMain")
            Me.ensMain.Name = "ensMain"
            Me.ensMain.Style = Tools.Windows.Forms.EncodingSelector.EncodingSelectorStyle.ListView
            '
            'tlpButtons
            '
            resources.ApplyResources(Me.tlpButtons, "tlpButtons")
            Me.tlpButtons.Controls.Add(Me.cmdCancel, 2, 0)
            Me.tlpButtons.Controls.Add(Me.cmdOK, 1, 0)
            Me.tlpButtons.Controls.Add(Me.chkOK, 3, 0)
            Me.tlpButtons.Name = "tlpButtons"
            '
            'cmdCancel
            '
            resources.ApplyResources(Me.cmdCancel, "cmdCancel")
            Me.cmdCancel.Name = "cmdCancel"
            Me.cmdCancel.UseVisualStyleBackColor = True
            '
            'cmdOK
            '
            resources.ApplyResources(Me.cmdOK, "cmdOK")
            Me.cmdOK.Name = "cmdOK"
            Me.cmdOK.UseVisualStyleBackColor = True
            '
            'chkOK
            '
            resources.ApplyResources(Me.chkOK, "chkOK")
            Me.chkOK.Name = "chkOK"
            Me.chkOK.TabStop = False
            Me.totToolTip.SetToolTip(Me.chkOK, resources.GetString("chkOK.ToolTip"))
            Me.chkOK.UseVisualStyleBackColor = True
            '
            'fraPreview
            '
            Me.fraPreview.Controls.Add(Me.txtPreview)
            resources.ApplyResources(Me.fraPreview, "fraPreview")
            Me.fraPreview.Name = "fraPreview"
            Me.fraPreview.TabStop = False
            '
            'txtPreview
            '
            Me.txtPreview.BackColor = System.Drawing.SystemColors.Control
            resources.ApplyResources(Me.txtPreview, "txtPreview")
            Me.txtPreview.Name = "txtPreview"
            Me.txtPreview.ReadOnly = True
            Me.txtPreview.TabStop = False
            '
            'splMain
            '
            resources.ApplyResources(Me.splMain, "splMain")
            Me.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
            Me.splMain.Name = "splMain"
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.ensMain)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.fraPreview)
            '
            'bgwTestEncoding
            '
            Me.bgwTestEncoding.WorkerSupportsCancellation = True
            '
            'frmEncodingDialog
            '
            resources.ApplyResources(Me, "$this")
            Me.Controls.Add(Me.splMain)
            Me.Controls.Add(Me.tlpButtons)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmEncodingDialog"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.tlpButtons.ResumeLayout(False)
            Me.tlpButtons.PerformLayout()
            Me.fraPreview.ResumeLayout(False)
            Me.fraPreview.PerformLayout()
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region
        ''' <summary>Instance of <see cref="EncodingDialog"/> that owns this form</summary>
        Protected ReadOnly ServesFor As EncodingDialog
        ''' <summary>CTor</summary>
        ''' <param name="Owner">The <see cref="EncodingDialog"/> that owns this form</param>
        Public Sub New(ByVal Owner As EncodingDialog)
            InitializeComponent()
            ServesFor = Owner
            If ServesFor.PreviewString = "" Then
                splMain.Panel2Collapsed = True
                splMain.Panel2.Enabled = False
            End If
            If ServesFor.PreviewString = "" AndAlso (ServesFor.PreviewBytes Is Nothing OrElse ServesFor.PreviewBytes.Length = 0) Then chkOK.Visible = False
            ensMain.SelectedCodepage = ServesFor.Preselected
            Me.Text = ServesFor.Text
            Me.HelpButton = ServesFor.ShowHelp
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        End Sub

        Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
            If ServesFor.SelectedEncoding IsNot Nothing AndAlso ValidateEncoding Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
            ElseIf ServesFor.SelectedEncoding Is Nothing Then
                MsgBox("Select encoding please.", , "No encoding selected") 'TODO:Localize
            End If
        End Sub
        ''' <summary>Performs action before user is allowed to close the dialog</summary>
        Protected Overridable Function ValidateEncoding() As Boolean
            'TODO: Ownn validation
            Dim e As New EncodingDialog.EncodingCancelEventArgs(ServesFor.SelectedEncoding)
            'TODO: Call EncodingDialog's OnOKClicked
            Return e.Cancel
        End Function

        Private Sub ensMain_SelectedIndexChanged(ByVal sender As EncodingSelector, ByVal e As System.EventArgs) Handles ensMain.SelectedIndexChanged
            'TODO: ServesFor.SelectedEncoding = sender.SelectedEncoding
            If bgwTestEncoding.IsBusy OrElse ValidationRequired Then
                ValidationRequired = True
                bgwTestEncoding.CancelAsync()
            Else
                bgwTestEncoding.RunWorkerAsync(sender.SelectedEncoding)
            End If
        End Sub
        ''' <summary>Set to True when <see cref="bgwTestEncoding"/> was busy when trying to start it in order to validate encoding</summary>
        Private ValidationRequired As Boolean = False

        Private Sub bgwTestEncoding_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwTestEncoding.DoWork
            e.Result = OnTest(e.Argument, e)
        End Sub
        ''' <summary>Called assynchronously when testing of encoding is required</summary>
        ''' <param name="Encoding">Encoding to be tested</param>
        ''' <param name="e">Arguments of <see cref="System.ComponentModel.BackgroundWorker.DoWork"/> event tah can be used to obtain detailed information about invocation</param>
        ''' <remarks>Do not use <paramref name="e"/>'s <see cref="System.ComponentModel.DoWorkEventArgs.Argument"/> it will be raplaced with return value of this function</remarks>
        Protected Overridable Function OnTest(ByVal Encoding As EncodingInfo, ByVal e As System.ComponentModel.DoWorkEventArgs) As TestResult
            Dim Result As String = ""
            Dim CanEncode, CanDecode As Boolean
            If ServesFor.PreviewBytes IsNot Nothing AndAlso ServesFor.PreviewBytes.Length > 0 Then
                Result = Encoding.GetEncoding.GetString(ServesFor.PreviewBytes)
                Dim ResultBytes As Byte() = Encoding.GetEncoding.GetBytes(Result)
                If ServesFor.PreviewBytes.Length = ResultBytes.Length Then
                    CanDecode = True
                    For i As Integer = 0 To ServesFor.PreviewBytes.Length - 1
                        If ServesFor.PreviewBytes(i) <> ResultBytes(i) Then
                            CanDecode = False
                            Exit For
                        End If
                    Next i
                Else
                    CanDecode = False
                End If
            End If
            If ServesFor.PreviewString <> "" Then
                CanEncode = ServesFor.PreviewString = Encoding.GetEncoding.GetString(Encoding.GetEncoding.GetBytes(ServesFor.PreviewString))
            End If
            Return New TestResult(Result, CanDecode, CanEncode)
        End Function
        ''' <summary>Result of encoding test</summary>
        Protected Class TestResult
            ''' <summary><see cref="ServesFor"/>'s <see cref="EncodingDialog.PreviewBytes"/> decoded by selected encoding</summary>
            ''' <remarks>Valid only when <see cref="ServesFor"/>'s <see cref="EncodingDialog.PreviewBytes"/> is set</remarks>
            Public ReadOnly Result As String
            ''' <summary>Indicates if <see cref="ServesFor"/>'s <see cref="EncodingDialog.PreviewBytes"/> can be encoded by selected encoding with no loss of data</summary>
            ''' ''' <remarks>Valid only when <see cref="ServesFor"/>'s <see cref="EncodingDialog.PreviewBytes"/> is set</remarks>
            Public ReadOnly CanDecode As Boolean
            ''' <summary>Indicates if <see cref="ServesFor"/>'s <see cref="EncodingDialog.PreviewString"/> can be encoded by selected encoding with no loss of data</summary>
            ''' <remarks>Valid only when <see cref="ServesFor"/>'s <see cref="EncodingDialog.PreviewString"/> is set</remarks>
            Public ReadOnly CanEncode As Boolean
            ''' <summary>CTor</summary>
            ''' <param name="Result"><see cref="ServesFor"/>'s <see cref="EncodingDialog.PreviewBytes"/> decoded by selected encoding</param>
            ''' <param name="CanDecode">Indicates if <see cref="ServesFor"/>'s <see cref="EncodingDialog.PreviewBytes"/> can be encoded by selected encoding with no loss of data</param>
            ''' <param name="CanEncode">Valid only when <see cref="ServesFor"/>'s <see cref="EncodingDialog.PreviewString"/> is set</param>
            Public Sub New(ByVal Result As String, ByVal CanDecode As Boolean, ByVal CanEncode As Boolean)
                Me.CanDecode = CanDecode
                Me.CanEncode = CanEncode
                Me.Result = Result
            End Sub
        End Class
        ''' <summary>Called synchronously when <see cref="OnTest"/> finishes. Used to disply results of testing.</summary>
        ''' <param name="result">Result of testing</param>
        ''' <param name="e">Argument of <see cref="System.ComponentModel.BackgroundWorker.RunWorkerCompleted"/> that can be used to obtain detailed information</param>
        Protected Overridable Sub OnTestFinished(ByVal result As TestResult, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
            If Not e.Cancelled Then
                If ServesFor.PreviewBytes IsNot Nothing AndAlso ServesFor.PreviewBytes.Length > 0 Then
                    txtPreview.Text = result.Result
                End If
                If ServesFor.PreviewBytes IsNot Nothing AndAlso ServesFor.PreviewBytes.Length > 0 AndAlso ServesFor.PreviewString <> "" Then
                    chkOK.Checked = result.CanEncode AndAlso result.CanDecode
                ElseIf ServesFor.PreviewBytes IsNot Nothing AndAlso ServesFor.PreviewBytes.Length > 0 Then
                    chkOK.Checked = result.CanDecode
                ElseIf ServesFor.PreviewString <> "" Then
                    chkOK.Checked = result.CanEncode
                End If
            End If
        End Sub
        Private Sub bgwTestEncoding_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwTestEncoding.RunWorkerCompleted
            OnTestFinished(e.Result, e)
            If ValidationRequired Then
                ValidationRequired = False
                If ensMain.SelectedEncoding IsNot Nothing Then bgwTestEncoding.RunWorkerAsync(ensMain.SelectedEncoding)
            End If
        End Sub
    End Class

    ''' <summary>Dialog that allows user to select encoding</summary>
    <Author("�onny", "dzonny.dz@gmail.com"), Version(1, 0, LastChange:="1/24/2007")> _
    <Prefix("end")> _
    Public Class EncodingDialog : Inherits CommonDialog
        ''' <summary>CTor</summary>
        Sub New()
            Reset()
        End Sub
        ''' <summary>Resets the properties of a common dialog box to their default values</summary>
        ''' <remarks>Note for inheritors: Call base class method in order to use default reset logic</remarks>
        Public Overrides Sub Reset()
            SelectedEncoding = Nothing
            Preselected = -1
        End Sub
        ''' <summary>The form that realizes the dialog</summary>
        ''' <remarks>This field should be no-null only when form is visible. Set it to instance of <see cref="frmEncodingDialog"/> in <see cref="ShowDialog"/>, the show it and then set it to null again.</remarks>
        Protected WithEvents DialogForm As frmEncodingDialog

#Region "Properties"
        ''' <summary>Contains value of the <see cref="SelectedEncoding"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _SelectedEncoding As EncodingInfo
        ''' <summary>After dialog has ran returns the ancoding selected by user (if any)</summary>
        ''' <value>Note for inheritors: Set this property in the <see cref="ShowDialog"/> function</value>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overridable Property SelectedEncoding() As EncodingInfo
            Get
                Return _SelectedEncoding
            End Get
            Protected Set(ByVal value As EncodingInfo)
                _SelectedEncoding = value
            End Set
        End Property
        ''' <summary>Contgains value of the <see cref="Preselected"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Preselected As Integer
        ''' <summary>Specifies the <see cref="EncodingInfo.CodePage"/> that will be selected when the dialog is shown (when encoding available)</summary>
        ''' <value><see cref="EncodingInfo.CodePage"/> of encoding to preselect or negative number to preslect no encoding</value>
        ''' <returns><see cref="EncodingInfo.CodePage"/> of preselected encoding or negativ value if no encoding will be preselected</returns>
        ''' <remarks>After dialog has ran this property stays unchanged</remarks>
        <DefaultValue(-1I)> _
        <Category(CategoryAttributeValues.Behavior)> _
        <Description("Specifies code page of encoding that will be preselected when dialog is shown (if available). Set to negative value in order to preselect no encoding")> _
        Public Overridable Property Preselected() As Integer
            Get
                Return _Preselected
            End Get
            Set(ByVal value As Integer)
                If value < 0 Then value = -1
                _Preselected = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="PreviewBytes"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _PreviewBytes As Byte()
        ''' <summary>Gets or sets text to decode by selected encoding and show preview to the user</summary>
        ''' <remarks>If set preview will be shown</remarks>
        <System.ComponentModel.DefaultValue(GetType(Byte()), "null pointer")> _
        <Category(CategoryAttributeValues.Behavior)> _
        <Description("Text to decode using selected encoding and show as preview")> _
        Public Overridable Property PreviewBytes() As Byte()
            Get
                Return _PreviewBytes
            End Get
            Set(ByVal value As Byte())
                _PreviewBytes = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="PreviewString"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _PreviewString As String
        ''' <summary>Gets or sets text to encode by selected encoding and inform user if all characters can be encoded using selected encoding</summary>
        <System.ComponentModel.DefaultValue("")> _
        <Category(CategoryAttributeValues.Behavior)> _
        <Description("Text to encode using selected encoding and inform user if all characters can be encoded")> _
        Public Overridable Property PreviewString() As String
            Get
                Return _PreviewString
            End Get
            Set(ByVal value As String)
                _PreviewString = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="RequireCorrect"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _RequireCorrect As Boolean
        ''' <summary>Defines if user can select encoding that cannot be used on specified <see cref="PreviewBytes"/> or <see cref="PreviewString"/> without problems</summary>
        ''' <remarks>Applicable only if at least one of <see cref="PreviewBytes"/> or <see cref="PreviewString"/> properties is set</remarks>
        <DefaultValue(True)> _
        <Category(CategoryAttributeValues.Behavior)> _
        <Description("Defines if user can select encoding that can be used only with problems on given PreviewString on PreviewBytes")> _
        Public Overridable Property RequireCorrect() As Boolean
            Get
                Return _RequireCorrect
            End Get
            Set(ByVal value As Boolean)
                _RequireCorrect = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Text"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Text As String
        ''' <summary>Gets or sets text displayed in title of window of dialog</summary>
        <DefaultValue("Select encoding")> _
        <Category(CategoryAttributeValues.Appearance)> _
        <Description("Text displayed in title of window")> _
        Public Overridable Property Text() As String
            'TODO: Localizable default value
            Get
                Return _Text
            End Get
            Set(ByVal value As String)
                _Text = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ShowHelp"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowHelp As String
        ''' <summary>Determines if help button will be shown</summary>
        <DefaultValue(False)> _
        <Category(CategoryAttributeValues.Appearance)> _
        <Description("Determines if help button will be shown")> _
        Public Overridable Property ShowHelp() As Boolean
            'TODO: Localizable default value
            Get
                Return _ShowHelp
            End Get
            Set(ByVal value As Boolean)
                _ShowHelp = value
            End Set
        End Property
#End Region
#Region "Events"
        ''' <summary>Raised after user clicks OK and dialog allows him co select encoding</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Arguments of event</param>
        Public Event OKClicked(ByVal sender As EncodingDialog, ByVal e As EncodingCancelEventArgs)
        ''' <summary>Raises the <see cref="OKClicked"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Call base calss method in order the event to be raised</remarks>
        Protected Overridable Sub OnOKClicked(ByVal e As EncodingCancelEventArgs)
            RaiseEvent OKClicked(Me, e)
        End Sub
        ''' <summary>Argument of the <see cref="OKClicked"/> event</summary>
        Public Class EncodingCancelEventArgs : Inherits CancelEventArgs
            ''' <summary>Encoding that was selected in a dialog</summary>
            Public ReadOnly SelectedEncoding As EncodingInfo
            ''' <summary>CTor</summary>
            ''' <param name="Encoding">Encoding selected in dialog to be verified by handling code</param>
            Public Sub New(ByVal Encoding As EncodingInfo)
                SelectedEncoding = Encoding
            End Sub
        End Class
        'TODO: OnHelpRequest
#End Region
        ''' <summary>When overridden in a derived class, specifies a common dialog box.</summary>
        ''' <param name="hwndOwner">A value that represents the window handle of the owner window for the common dialog box.</param>
        ''' <returns>true if the dialog box was successfully run; otherwise, false.</returns>
        Protected NotOverridable Overrides Function RunDialog(ByVal hwndOwner As System.IntPtr) As Boolean
            If hwndOwner <> IntPtr.Zero Then
                Dim w As Form = Form.FromHandle(hwndOwner)
                Return ShowDialog(w) = DialogResult.OK
            Else
                Return ShowDialog() = DialogResult.OK
            End If
        End Function
        ''' <param name="owner"><see cref="Form"/> that will own the dialog</param>
        ''' <returns><see cref="DialogResult.OK"/> if the user clicks OK in the dialog box; otherwise, <see cref="DialogResult.Cancel"/>.</returns>
        ''' <remarks>This function differs from the <see cref="CommonDialog.ShowDialog"/>. Use this shadow function rather than base class's one.</remarks>
        Public Overridable Shadows Function ShowDialog(Optional ByVal Owner As Form = Nothing) As DialogResult
            DialogForm = New frmEncodingDialog(Me)
            ShowDialog = DialogForm.ShowDialog(Owner)
            DialogForm = Nothing
        End Function

        Private Sub DialogForm_HelpButtonClicked(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DialogForm.HelpButtonClicked
            e.Cancel = True
            MyBase.OnHelpRequest(e)
        End Sub

        Private Sub DialogForm_HelpRequested(ByVal sender As Object, ByVal hlpevent As System.Windows.Forms.HelpEventArgs) Handles DialogForm.HelpRequested
            MyBase.OnHelpRequest(hlpevent)
        End Sub
        'TODO: Show detailf of selected encoding
        'TODO: Possibility to hide details programatically
        'TODO: Possibility to preselect system default encoding
        'TODO: Default property and event
        'TODO: Toolbox bitmap
        'TODO: Allow/disallow selecting encoding by test result
        'TODO: Remove encodings that haven't passed the test from list
        ''' <summary>Filters encoding by its parameters</summary>
        ''' <remarks>This enumeration is used to either include only encodings or exclude all encodings that falle into either at least one or all groups specified</remarks>
        Public Enum EncodingFilters As Long
            'TODO:Apply
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/> = True</summary>
            NormalizedYes = 2 ^ 0
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/> = False</summary>
            NormalizedNo = 2 ^ 1
            ''' <summary><see cref="Encoding.IsBrowserDisplay"/> = True</summary>
            BrowserDisplayYes = 2 ^ 2
            ''' <summary><see cref="Encoding.IsBrowserDisplay"/> = False</summary>
            BrowserDisplayNo = 2 ^ 3
            ''' <summary><see cref="Encoding.IsBrowserSave"/> = True</summary>
            BrowserSaveYes = 2 ^ 4
            ''' <summary><see cref="Encoding.IsBrowserSave"/> = False</summary>
            BrowserSaveNo = 2 ^ 5
            ''' <summary>Or combination of <see cref="BrowserDisplayYes"/> and <see cref="BrowserSaveYes"/></summary>
            BrowserYes = BrowserDisplayYes Or BrowserSaveYes
            ''' <summary>Or combination of <see cref="BrowserDisplayNo"/> and <see cref="BrowserSaveNo"/></summary>
            BrowserNo = BrowserDisplayNo Or BrowserSaveNo
            ''' <summary><see cref="Encoding.IsMailNewsDisplay"/> = True</summary>
            MailNewsDisplayYes = 2 ^ 6
            ''' <summary><see cref="Encoding.IsBrowserDisplay"/> = False</summary>
            MailNewsDisplayNo = 2 ^ 7
            ''' <summary><see cref="Encoding.IsMailNewsSave"/> = True</summary>
            MailNewsSaveYes = 2 ^ 8
            ''' <summary><see cref="Encoding.IsMailNewsSave"/> = False</summary>
            MailNewsSaveNo = 2 ^ 9
            ''' <summary>Or combination of <see cref="MailNewsDisplayYes"/> and <see cref="MailNewsSaveYes"/></summary>
            MailNewsYes = MailNewsDisplayYes Or MailNewsSaveYes
            ''' <summary>Or combination of <see cref="MailNewsDisplayNo"/> and <see cref="MailNewsSaveNo"/></summary>
            MailNewsNo = MailNewsDisplayNo Or MailNewsSaveNo
            ''' <summary><see cref="Encoding.IsReadOnly"/> = True</summary>
            ReadOnlyYes = 2 ^ 10
            ''' <summary><see cref="Encoding.IsReadOnly"/> = False</summary>
            ReadOnlyNo = 2 ^ 11
            ''' <summary><see cref="Encoding.IsSingleByte"/> = True</summary>
            SingleByte = 2 ^ 12
            ''' <summary><see cref="Encoding.IsSingleByte"/> = False</summary>
            MultiByte = 2 ^ 13
            ''' <summary><see cref="Encoding.BodyName"/> isn not an empty string</summary>
            MailAgentBodyYes = 2 ^ 14
            ''' <summary><see cref="Encoding.BodyName"/> isn an empty string</summary>
            MailAgentBodyNo = 2 ^ 15
            ''' <summary><see cref="Encoding.HeaderName"/> isn not an empty string</summary>
            MailAgentHeaderYes = 2 ^ 16
            ''' <summary><see cref="Encoding.HeaderName"/> isn an empty string</summary>
            MailAgentHeaderNo = 2 ^ 17
            ''' <summary>Or combination of <see cref="MailAgentBodyYes"/> and <see cref="MailAgentHeaderYes"/></summary>
            MailAgentYes = MailAgentBodyYes Or MailAgentHeaderYes
            ''' <summary>Or combination of <see cref="MailAgentBodyNo"/> and <see cref="MailAgentHeaderNo"/></summary>
            MailAgentNo = MailAgentBodyNo Or MailAgentHeaderNo
            ''' <summary>Or combination of <see cref="MailNewsYes"/> and <see cref="MailAgentYes"/></summary>
            MailYes = MailNewsYes Or MailAgentYes
            ''' <summary>Or combination of <see cref="MailNewsNo"/> and <see cref="MailAgentNo"/></summary>
            MailNo = MailNewsNo Or MailAgentNo
            ''' <summary>Or combination of <see cref="MailYes"/> and <see cref="BrowserYes"/></summary>
            InternetYes = MailYes Or BrowserYes
            ''' <summary>Or combination of <see cref="MailNo"/> and <see cref="BrowserNo"/></summary>
            InternetNo = MailNo Or BrowserNo
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormC"/>) = True</summary>
            NormalizedCYes = 2 ^ 18
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormC"/>) = False</summary>
            NormalizedCNo = 2 ^ 19
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormD"/>) = True</summary>
            NormalizedDYes = 2 ^ 20
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormD"/>) = False</summary>
            NormalizedDNo = 2 ^ 21
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormKC"/>) = True</summary>
            NormalizedKCYes = 2 ^ 22
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormKC"/>) = False</summary>
            NormalizedKCNo = 2 ^ 23
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormKD"/>) = True</summary>
            NormalizedKDYes = 2 ^ 24
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormKD"/>) = False</summary>
            NormalizedKDNo = 2 ^ 25
            ''' <summary>Or combination of <see cref="NormalizedCYes"/> and <see cref="NormalizedDYes"/></summary>
            NormalizedFullCanonicalBothYes = NormalizedCYes Or NormalizedDYes
            ''' <summary>Or combination of <see cref="NormalizedCNo"/> and <see cref="NormalizedDNo"/></summary>
            NormalizedFullCanonicalBothNo = NormalizedCNo Or NormalizedDNo
            ''' <summary>Or combination of <see cref="NormalizedKCYes"/> and <see cref="NormalizedKDYes"/></summary>
            NormalizedFullCompatibilityBothYes = NormalizedKCYes Or NormalizedKDYes
            ''' <summary>Or combination of <see cref="NormalizedKCNo"/> and <see cref="NormalizedKDNo"/></summary>
            NormalizedFullCompatibilityBothNo = NormalizedKCNo Or NormalizedKDNo
            ''' <summary>Or combination of <see cref="NormalizedFullCanonicalBothYes"/> and <see cref="NormalizedFullCompatibilityBothYes"/></summary>
            NormalizedAllYes = NormalizedFullCanonicalBothYes Or NormalizedFullCompatibilityBothYes
            ''' <summary>Or combination of <see cref="NormalizedFullCanonicalBothNo"/> and <see cref="NormalizedFullCompatibilityBothNo"/></summary>
            NormalizedAllNo = NormalizedFullCanonicalBothNo Or NormalizedFullCompatibilityBothNo
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormC"/>) = True -or- <see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormD"/>) = True</summary>
            NormalizedCanonicalAtLeastOneYes = 2 ^ 26
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormC"/>) = False -or- <see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormD"/>) = False</summary>
            NormalizedCanonicalAtLeastOneNo = 2 ^ 27
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormKC"/>) = True -or- <see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormKD"/>) = True</summary>
            NormalizedCompatibilityAtLeastOneYes = 2 ^ 28
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormKC"/>) = False -or- <see cref="Encoding.IsAlwaysNormalized"/>(<see cref="NormalizationForm.FormKD"/>) = False</summary>
            NormalizedCompatibilityAtLeastOneNo = 2 ^ 29
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/> with one of <see cref="NormalizationForm"/> values returns True</summary>
            NormalizedAtLeastOneYes = 2 ^ 30
            ''' <summary><see cref="Encoding.IsAlwaysNormalized"/> with one of <see cref="NormalizationForm"/> values returns False</summary>
            NormalizedAtLeastOneNo = 2 ^ 31
            ''' <summary><see cref="Encoding.CodePage"/> = <see cref="Encoding.WindowsCodePage"/></summary>
            CodePage_EQ_WindowsCodePage = 2 ^ 32
            ''' <summary><see cref="Encoding.CodePage"/> != <see cref="Encoding.WindowsCodePage"/></summary>
            CodePage_NEQ_WindowsCodePage = 2 ^ 33
            ''' <summary><see cref="Encoding.GetPreamble"/>.<see cref="Array.Length"/> > 0</summary>
            PreambleYes = 2 ^ 34
            ''' <summary><see cref="Encoding.GetPreamble"/>.<see cref="Array.Length"/> = 0</summary>
            PrembleNo = 2 ^ 35
        End Enum
    End Class
End Namespace
'#End If 'See note at the beginning of this file