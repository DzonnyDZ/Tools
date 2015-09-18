Imports System.Windows.Forms
Imports Tools.ComponentModelT
Imports Tools.WindowsT.IndependentT
Imports Tools.ThreadingT

'#If TrueSet in project file
'Stage: Nightly
Namespace WindowsT.FormsT
    ''' <summary>This <see cref="Form"/> serves as predefined progress monitor with <see cref="ProgressBar"/> for <see cref="BackgroundWorker"/></summary>
    ''' <remarks>See documentation of the <see cref="ProgressMonitor.ApplyUserState"/> method in order to see rich options for reporting progress.</remarks>
    ''' <seelaso cref="WPF.DialogsT.ProgressMonitor"/>
    ''' <version version="1.5.3">This class implements <see cref="IProgressMonitorUI"/> interface</version>
    ''' <version version="1.5.3">Added support fro <see cref="Integer"/>, <see cref="Array"/> and <see cref="IndependentT.ProgressBarStyle"/> passed in user state of background worker <see cref="BackgroundWorker.ProgressChanged"/> event.</version>
    Public Class ProgressMonitor
        Implements IProgressMonitorUI
#Region "CTors"
        ''' <summary>Default CTor</summary>
        ''' <remarks>Value of the <see cref="BackgroundWorker"/> property is populated with new instance of <see cref="System.ComponentModel.BackgroundWorker"/></remarks>
        ''' <filterpriority>3</filterpriority>
        Public Sub New()
            Me.new(New BackgroundWorker With {.WorkerReportsProgress = True, .WorkerSupportsCancellation = True})
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="bgw"><see cref="BackgroundWorker"/> this form will report progress for</param>
        ''' <exception cref="ArgumentNullException" ><paramref name="bgw"/> is null</exception>
        ''' <filterpriority>1</filterpriority>
        Public Sub New(ByVal bgw As BackgroundWorker)
            If bgw Is Nothing Then Throw (New ArgumentNullException("bgw"))
            InitializeComponent()
            Me.BackgroundWorker = bgw
        End Sub
        ''' <summary>CTor with title text and prompt</summary>
        ''' <param name="bgw"><see cref="BackgroundWorker"/> this form will report progress for</param>
        ''' <exception cref="ArgumentNullException" ><paramref name="bgw"/> is null</exception>
        ''' <param name="Text">Title text of window (see <see cref="Text"/>)</param>
        ''' <param name="Prompt">Prompt text (see <see cref="Prompt"/>)</param>
        ''' <filterpriority>2</filterpriority>
        Public Sub New(ByVal bgw As BackgroundWorker, ByVal Text$, Optional ByVal Prompt$ = Nothing)
            Me.new(bgw)
            Me.Text = Text
            Me.Prompt = Prompt
        End Sub
#End Region
        ''' <summary>Shows progress form and runs worker</summary>
        ''' <param name="bgw">Worker to run</param>
        ''' <param name="Text">Title text of window (see <see  cref="Text"/>)</param>
        ''' <param name="Prompt">Text prompt (see <see cref="Prompt"/>)</param>
        ''' <param name="Owner">Any object that implements <see cref="System.Windows.Forms.IWin32Window"/> or <see cref="System.Windows.Interop.IWin32Window"/>, or <see cref="System.Windows.Window"/> that represents the top-level window that will own the modal dialog box.</param>
        ''' <param name="WorkerArgument">Optional parameter for background worker</param>
        ''' <returns>Result of work of <paramref name="bgw"/></returns>
        ''' <version version="1.5.3">Type of parameter <paramref name="Owner"/> changed from <see cref="IWin32Window"/> to <see cref="Object"/> to support WPF owners.</version>
        Public Overloads Shared Function Show(ByVal bgw As BackgroundWorker, ByVal Text As String, ByVal Prompt As String, Optional ByVal Owner As Object = Nothing, Optional ByVal WorkerArgument As Object = Nothing) As RunWorkerCompletedEventArgs
            Using frm As New ProgressMonitor(bgw, Text, Prompt)
                frm.WorkerArgument = WorkerArgument
                frm.ShowDialog(Owner)
                Return frm.WorkerResult
            End Using
        End Function

        ''' <summary>Shows window modally</summary>
        ''' <param name="owner">Owner object of dialog. It can be either <see cref="System.Windows.Forms.IWin32Window"/> (e.g. <see cref="Form"/>), <see cref="System.Windows.Interop.IWin32Window"/> or <see cref="System.Windows.Window"/>. When owner is not of recognized type (or is null), it's ignored.</param>
        ''' <returns>True when dialog was closed normally, false if it was closed because of user has cancelled the operation</returns>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Private Overloads Function IProgressMonitorUI_ShowDialog(Optional ByVal owner As Object = Nothing) As Boolean Implements IProgressMonitorUI.ShowDialog
            If TypeOf owner Is System.Windows.Window Then
                Return Tools.WindowsT.InteropT.InteropExtensions.ShowDialog(Me, DirectCast(owner, System.Windows.Window)) = System.Windows.Forms.DialogResult.OK
            ElseIf TypeOf owner Is System.Windows.Forms.IWin32Window Then
                Return Me.ShowDialog(DirectCast(owner, System.Windows.Forms.IWin32Window)) = System.Windows.Forms.DialogResult.OK
            ElseIf TypeOf owner Is System.Windows.Interop.IWin32Window Then
                Return Tools.WindowsT.InteropT.InteropExtensions.ShowDialog(Me, DirectCast(owner, System.Windows.Interop.IWin32Window)) = System.Windows.Forms.DialogResult.OK
            Else
                Return Me.ShowDialog = System.Windows.Forms.DialogResult.OK
            End If
        End Function
        ''' <summary>Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.</summary>
        ''' <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
        Protected Overrides Sub OnShown(ByVal e As System.EventArgs)
            MyBase.OnShown(e)
            If DoWorkOnShow Then bgw.RunWorkerAsync(WorkerArgument)
        End Sub
        ''' <summary>Contains value of the <see cref="BackgroundWorker"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private WithEvents bgw As BackgroundWorker
        ''' <summary>Gets <see cref="BackgroundWorker"/> this form repports progress of</summary>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property BackgroundWorker() As BackgroundWorker Implements IProgressMonitorUI.BackgroundWorker
            <DebuggerStepThrough()> Get
                Return bgw
            End Get
            Private Set(ByVal value As BackgroundWorker)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                If value IsNot bgw Then
                    bgw = value
                    CanCancel = bgw.WorkerSupportsCancellation
                    pgbProgress.Style = If(bgw.WorkerReportsProgress, System.Windows.Forms.ProgressBarStyle.Blocks, System.Windows.Forms.ProgressBarStyle.Marquee)
                End If
            End Set
        End Property
        ''' <summary>Gets or sets style of <see cref="ProgressBar"/> that indicates progress of process</summary>
        ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value is not a member of the <see cref="System.Windows.Forms.ProgressBarStyle"/> enumeration.</exception>
        ''' <seelaso cref="ProgressBar.Style"/>
        <DefaultValue(GetType(System.Windows.Forms.ProgressBarStyle), "Blocks")>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)>
        <LDescription(GetType(CompositeControls), "ProgressBarStyle_d")>
        Public Property ProgressBarStyle() As System.Windows.Forms.ProgressBarStyle
            <DebuggerStepThrough()> Get
                Return pgbProgress.Style
            End Get
            <DebuggerStepThrough()> Set(ByVal value As System.Windows.Forms.ProgressBarStyle)
                pgbProgress.Style = value
            End Set
        End Property

        ''' <summary>Gets or sets current style of progress bar</summary>
        ''' <remarks>This implementation supports all values defined in the <see cref="System.Windows.Forms.ProgressBarStyle"/> enumeration. Other values are corced to <see cref="IndependentT.ProgressBarStyle.Definite"/>.</remarks>
        ''' <version version="1.5.3">This property is new in version 1.5.3</version>
        Private Property IProgressMonitorUI_ProgressBarStyle As IndependentT.ProgressBarStyle Implements IProgressMonitorUI.ProgressBarStyle
            Get
                Return ProgressBarStyle
            End Get
            Set(ByVal value As IndependentT.ProgressBarStyle)
                If Not CType(value, System.Windows.Forms.ProgressBarStyle).IsDefined Then value = IndependentT.ProgressBarStyle.Definite
                ProgressBarStyle = value
            End Set
        End Property

        ''' <summary>Gets or sets current value of <see cref="ProgressBar"/> that reports progress</summary>
        ''' <exception cref="ArgumentException">Value being set is smaller than 0 or greater than 100.</exception>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property Progress() As Integer Implements IProgressMonitorUI.Progress
            <DebuggerStepThrough()> Get
                Return pgbProgress.Value
            End Get
            <DebuggerStepThrough()> Protected Set(ByVal value As Integer)
                pgbProgress.Value = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="CloseOnFinish"/> property</summary>
        Private _CloseOnFinish As Boolean = True

        ''' <summary>Gets or sets value indicating if form automatically closes when <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.RunWorkerCompleted">RunWorkerCompleted</see> event occures.</summary>
        ''' <value>New behavoir. Defalt value is true.</value>
        <DefaultValue(True)>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)>
        <LDescription(GetType(CompositeControls), "CloseOnFinish_d")>
        Public Property CloseOnFinish() As Boolean Implements IProgressMonitorUI.CloseOnFinish
            Get
                Return _CloseOnFinish
            End Get
            Set(ByVal value As Boolean)
                _CloseOnFinish = value
            End Set
        End Property
        ''' <summary>Handles <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.ProgressChanged">ProgressChanged</see> event</summary>
        ''' <param name="sender"><see cref="BackgroundWorker"/></param>
        ''' <param name="e">Event erguments</param>
        ''' <remarks>
        ''' This implementation takes value of <paramref name="e"/>.<see cref="ProgressChangedEventArgs.ProgressPercentage">ProgressPercentage</see> and if it is greater than or equal to zero passes it to the <see cref="Progress"/> property.
        ''' For details how thios implementation deals with <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> see <see cref="ApplyUserState"/>.
        ''' </remarks>
        ''' <exception cref="ArgumentException"><paramref name="e"/>.<see cref="ProgressChangedEventArgs.ProgressPercentage">ProgressPercentage</see> is greater than 100.</exception>
        ''' <version version="1.5.3">Major part of functionality of this method - user state application - extracted to a new method - <see cref="ApplyUserState"/>.</version>
        ''' <version version="1.5.3">Added support for <see cref="Integer"/>, <see cref="IndependentT.ProgressBarStyle"/> and <see cref="Array"/> in <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see>.</version>
        Protected Overridable Sub OnProgressChanged(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgw.ProgressChanged
            If e.ProgressPercentage >= 0 Then Progress = e.ProgressPercentage
            ApplyUserState(e.UserState)
        End Sub
        ''' <summary>Applies value passed to <see cref="ProgressChangedEventArgs.UserState"/></summary>
        ''' <param name="userState">Value to apply</param>
        ''' <remarks>This implementation treats values of some types in a special way:
        ''' <list type="table">
        ''' <listheader><term>Type</term><description>Action taken</description></listheader>
        ''' <item><term><see cref="System.Windows.Forms.ProgressBarStyle"/> or <see cref="IndependentT.ProgressBarStyle"/></term><description>The value is passedto the <see cref="ProgressBarStyle"/> property.</description></item>
        ''' <item><term><see cref="String"/></term><description>The value is passed to the <see cref="Information"/> property.</description></item>
        ''' <item><term><see cref="Boolean"/></term><description>The value is passed to the <see cref="CanCancel"/> property.</description></item>
        ''' <item><term><see cref="System.ComponentModel.BackgroundWorker"/> (same instance as <see cref="BackgroundWorker"/>)</term><description>The <see cref="Reset"/> method is called.</description></item>
        ''' <item><term><see cref="Integer"/> (only when form range 0÷100)</term><description>The value is passed to the <see cref="Progress"/> property (same as passing value greater than or equal to zero to <see cref="ProgressChangedEventArgs.ProgressPercentage"/>).</description></item>
        ''' <item><term><see cref="Array"/> (any type)</term><description>Individual items of the array are passed to the <see cref="ApplyUserState"/> method.</description></item>
        ''' </list>
        ''' Null values and values of unsupported types are ignored.</remarks>
        ''' <version version="1.5.3">This method is new in version 1.5.3 (it extracts user state application logic from <see cref="OnProgressChanged"/>.</version>
        Protected Overridable Sub ApplyUserState(ByVal userState As Object)
            If TypeOf userState Is System.Windows.Forms.ProgressBarStyle OrElse TypeOf userState Is IndependentT.ProgressBarStyle Then
                ProgressBarStyle = userState
            ElseIf TypeOf userState Is String Then
                Information = userState
            ElseIf TypeOf userState Is Integer AndAlso DirectCast(userState, Integer) >= 0 AndAlso DirectCast(userState, Integer) <= 100 Then
                Progress = userState
            ElseIf TypeOf userState Is Boolean Then
                CanCancel = userState
            ElseIf TypeOf userState Is BackgroundWorker AndAlso BackgroundWorker Is userState Then
                Reset()
            ElseIf TypeOf userState Is Array Then
                For Each item In DirectCast(userState, Array)
                    ApplyUserState(item)
                Next
            End If
        End Sub
        ''' <summary>Handles <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.RunWorkerCompleted">RunWorkerCompleted</see> event.</summary>
        ''' <param name="sender"><see cref="BackgroundWorker"/></param>
        ''' <param name="e">event arguments</param>
        ''' <remarks>This implementation sets <see cref="DialogResult"/> to <see cref="DialogResult.Cancel"/> when <paramref name="e"/>.<see cref="RunWorkerCompletedEventArgs.Cancelled">Cancelled</see> is true; to <see cref="DialogResult.Abort"/> when <paramref name="e"/>.<see cref="RunWorkerCompletedEventArgs.[Error]"/> isnot nothing and to <see cref="DialogResult.OK"/> in all other cases. Then sets <see cref="WorkerResult"/>. If <see cref="CloseOnFinish"/> is true, closes the form.</remarks>
        Protected Overridable Sub OnRunWorkerCompleted(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw.RunWorkerCompleted
            If e.Cancelled Then : Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            ElseIf e.Error IsNot Nothing Then : Me.DialogResult = System.Windows.Forms.DialogResult.Abort
            Else : Me.DialogResult = System.Windows.Forms.DialogResult.OK : End If
            WorkerResult = e
            If CloseOnFinish Then
                Me.Close()
            End If
        End Sub
        ''' <summary>Contains value of the <see cref="WorkerResult"/> property</summary>
        Private _WorkerResult As RunWorkerCompletedEventArgs
        ''' <summary>Gets or sets result of <see cref="BackgroundWorker"/> work</summary>
        ''' <returns>Null until <see cref="BackgroundWorker.RunWorkerCompleted"/> event occures. That returns its e parameter.</returns>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property WorkerResult() As RunWorkerCompletedEventArgs Implements IProgressMonitorUI.WorkerResult
            <DebuggerStepThrough()> Get
                Return _WorkerResult
            End Get
            <DebuggerStepThrough()> Protected Set(ByVal value As RunWorkerCompletedEventArgs)
                _WorkerResult = value
            End Set
        End Property
        ''' <summary>Gets or sets prompt diaplyed in upper part of form</summary>
        <DefaultValue(GetType(String), Nothing)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <LDescription(GetType(CompositeControls), "Prompt_d")> _
        Public Property Prompt$() Implements IProgressMonitorUI.Prompt
            <DebuggerStepThrough()> Get
                Return lblMainInfo.Text
            End Get
            <DebuggerStepThrough()> Set(ByVal value$)
                lblMainInfo.Text = value
            End Set
        End Property
        ''' <summary>Gets or sets informative text displayed below the progress bar</summary>
        <DefaultValue(GetType(String), Nothing)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <LDescription(GetType(CompositeControls), "Information_d")> _
        Public Property Information$() Implements IProgressMonitorUI.Information
            <DebuggerStepThrough()> Get
                Return lblI.Text
            End Get
            <DebuggerStepThrough()> Set(ByVal value$)
                lblI.Text = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="CanCancel"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _CanCancel As Boolean
        ''' <summary>Gets or sets value indicationg if dialog supports cancelation</summary>
        ''' <value>Default value depends on <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.WorkerSupportsCancellation">WorkerSupportsCancellation</see> in time when it is passed to CTor.</value>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(CompositeControls), "CanCancel_d")> _
        Public Property CanCancel() As Boolean Implements IProgressMonitorUI.CanCancel
            <DebuggerStepThrough()> Get
                Return _CanCancel
            End Get
            Set(ByVal value As Boolean)
                _CanCancel = value
                If Not BackgroundWorker.CancellationPending Then cmdCancel.Enabled = value
            End Set
        End Property
        ''' <summary>Resets value of the <see cref="CanCancel"/> pproperty to its default value according to <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.WorkerSupportsCancellation">WorkerSupportsCancellation</see>.</summary>
        Private Sub ResetCanCancel()
            CanCancel = BackgroundWorker.WorkerSupportsCancellation
        End Sub
        ''' <summary>Gets value indicationg if value of the <see cref="CanCancel"/> property should be serializes</summary>
        ''' <returns>True if value of the <see cref="CanCancel"/> property differs from <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.WorkerSupportsCancellation">WorkerSupportsCancellation</see>.</returns>
        Private Function ShouldSerializeResetCancel() As Boolean
            Return CanCancel <> BackgroundWorker.WorkerSupportsCancellation
        End Function
        ''' <summary>Performs cancellation. Called when the cancel button is clicked.</summary>
        Protected Overridable Sub OnCancel() Handles cmdCancel.Click
            If Not CanCancel Then Return
            If BackgroundWorker.WorkerSupportsCancellation Then
                cmdCancel.Enabled = False
                BackgroundWorker.CancelAsync()
            Else
                Media.SystemSounds.Beep.Play()
            End If
        End Sub
        ''' <summary>Resets the dialog</summary>
        ''' <remarks>In case you want to use the dialog from multiple runs of <see cref="BackgroundWorker"/>, you should call this method before each (excluding first, but you can to) runs of <see cref="BackgroundWorker"/>. Alternativly you can report new run using <see cref="BackgroundWorker.ReportProgress"/> - see <see cref="OnProgressChanged"/>.</remarks>
        Public Overridable Sub Reset() Implements IProgressMonitorUI.Reset
            cmdCancel.Enabled = CanCancel
            pgbProgress.Value = 0
            WorkerResult = Nothing
        End Sub
        ''' <summary>Contains value of the <see cref="DoWorkOnShow"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _DoWorkOnShow As Boolean = True
        ''' <summary>Gets or sets value indicationg if form will call <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.RunWorkerAsync">RunWorkerAsync</see> when <see cref="Shown"/> event occures.</summary>
        ''' <seelaso cref="WorkerArgument"/>
        ''' <value>Default value is true</value>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(CompositeControls), "DoWorkOnShow_d")> _
        <DefaultValue(True)> _
        Public Property DoWorkOnShow() As Boolean Implements IProgressMonitorUI.DoWorkOnShow
            <DebuggerStepThrough()> Get
                Return _DoWorkOnShow
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Boolean)
                _DoWorkOnShow = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="WorkerArgument"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _WorkerArgument As Object
        ''' <summary>Gets or sets value of argument passed to <see cref="BackgroundWorker.DoWork"/> when <see cref="DoWorkOnShow"/> is true</summary>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
        <DefaultValue(GetType(Object), Nothing)> _
        <LDescription(GetType(CompositeControls), "WorkerArgument_d")> _
        Public Property WorkerArgument() As Object Implements IProgressMonitorUI.WorkerArgument
            <DebuggerStepThrough()> Get
                Return _WorkerArgument
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Object)
                _WorkerArgument = value
            End Set
        End Property

        ''' <summary>Implements <see cref="IndependentT.IProgressMonitorUI.Title"/></summary>
        ''' <value>Title of window showing the progress. Default value is localized word "Progress"</value>
        ''' <returns>Current title of window showing progress</returns>
        ''' <version version="1.5.3">This property is new in version 1.5.3</version>
        Private Property IProgressMonitorUI_Title As String Implements IndependentT.IProgressMonitorUI.Title
            <DebuggerStepThrough()> Get
                Return Text
            End Get
            <DebuggerStepThrough()> Set(ByVal value As String)
                Text = value
            End Set
        End Property

        ''' <summary>Synchronously invokes a delegate in UI thread</summary>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="params">Delegate parameters</param>
        ''' <returns>Result of delegate call</returns>
        ''' <remarks>This function invokes <paramref name="delegate"/> in UI thread when <see cref="InvokeRequired"/> denotes it as necessary.</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        ''' <seelaso cref="Invoke"/><seelaso cref="InvokeRequired"/><seelaso cref="IInvokeExtensions"/>
        Private Function IInvoke_Invoke(ByVal [delegate] As System.Delegate, ByVal ParamArray params() As Object) As Object Implements IInvoke.Invoke
            If Me.InvokeRequired Then
                Return Me.Invoke([delegate], params)
            Else
                Return [delegate].DynamicInvoke(params)
            End If
        End Function

        ''' <summary>Gets an object that can be used as owner for modal windows</summary>
        ''' <returns>This instance of <see cref="ProgressMonitor"/></returns>
        Private ReadOnly Property IProgressMonitorUI_OwnerObject As Object Implements IndependentT.IProgressMonitorUI.OwnerObject
            Get
                Return Me
            End Get
        End Property
    End Class
End Namespace
'#End If