Imports System.Windows.Forms
Imports Tools.ComponentModelT

'#If Config <= Nightly Then Set in project file
'Stage: Nightly
Namespace WindowsT.FormsT
    ''' <summary>This <see cref="Form"/> serves as predefined progress monitor with <see cref="ProgressBar"/> for <see cref="BackgroundWorker"/></summary>
    ''' <remarks>See documentation of the <see cref="ProgressMonitor.OnProgressChanged"/> method in order to see rich options for reporting progress.</remarks>
    Public Class ProgressMonitor
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
        ''' <summary>Shows progress form and runs worker</summary>
        ''' <param name="bgw">Worker to run</param>
        ''' <param name="Text">Title text of window (see <see  cref="Text"/>)</param>
        ''' <param name="Prompt">Text prompt (see <see cref="Prompt"/>)</param>
        ''' <param name="Owner">Any object that implements <see cref="System.Windows.Forms.IWin32Window"/> that represents the top-level window that will own the modal dialog box.</param>
        ''' <param name="WorkerArgument">Optional parameter for background worker</param>
        ''' <returns>Result of work of <paramref name="bgw"/></returns>
        Public Overloads Shared Function Show(ByVal bgw As BackgroundWorker, ByVal Text As String, ByVal Prompt As String, Optional ByVal Owner As IWin32Window = Nothing, Optional ByVal WorkerArgument As Object = Nothing) As RunWorkerCompletedEventArgs
            Using frm As New ProgressMonitor(bgw, Text, Prompt)
                frm.WorkerArgument = WorkerArgument
                frm.ShowDialog(Owner)
                Return frm.WorkerResult
            End Using
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
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property BackgroundWorker() As BackgroundWorker
            <DebuggerStepThrough()> Get
                Return bgw
            End Get
            Private Set(ByVal value As BackgroundWorker)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                If value IsNot bgw Then
                    bgw = value
                    CanCancel = bgw.WorkerSupportsCancellation
                    pgbProgress.Style = If(bgw.WorkerReportsProgress, ProgressBarStyle.Blocks, ProgressBarStyle.Marquee)
                End If
            End Set
        End Property
        ''' <summary>Gets or sets style of <see cref="ProgressBar"/> that indicates progress of process</summary>
        ''' <seelaso cref="ProgressBar.Style"/>
        <DefaultValue(GetType(ProgressBarStyle), "Blocks")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <LDescription(GetType(CompositeControls), "ProgressBarStyle_d")> _
        Public Property ProgressBarStyle() As ProgressBarStyle
            <DebuggerStepThrough()> Get
                Return pgbProgress.Style
            End Get
            <DebuggerStepThrough()> Set(ByVal value As ProgressBarStyle)
                pgbProgress.Style = value
            End Set
        End Property
        ''' <summary>Gets or sets current value of <see cref="ProgressBar"/> that reports progress</summary>
        ''' <exception cref="ArgumentException">Value being set is smaller than 0 or greater than 100.</exception>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property Progress() As Integer
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
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(CompositeControls), "CloseOnFinish_d")> _
        Public Property CloseOnFinish() As Boolean
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
        ''' <remarks>Default implementation works in following way:
        ''' <list type="bullet">
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.ProgressPercentage">ProgressPercentage</see> is greater than or equal to zero then sets this value to the <see cref="Progress"/> property. Values smaller than zero are ignored.</item>
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> is <see cref="Windows.Forms.ProgressBarStyle"/> passes that value to the <see cref="ProgressBarStyle"/> property.</item>
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> is <see cref="String"/> passes that value to the <see cref="Information"/> property.</item>
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> is <see cref="Boolean"/> passes that value to the <see cref="CanCancel"/> property.</item>
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> is <see cref="BackgroundWorker"/> (same instance) than <see cref="Reset"/> method is called.</item>
        ''' </list>
        ''' </remarks>
        ''' <exception cref="ArgumentException"><paramref name="e"/>.<see cref="ProgressChangedEventArgs.ProgressPercentage">ProgressPercentage</see> is greater than 100.</exception>
        Protected Overridable Sub OnProgressChanged(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgw.ProgressChanged
            If e.ProgressPercentage >= 0 Then Progress = e.ProgressPercentage
            If TypeOf e.UserState Is ProgressBarStyle Then
                ProgressBarStyle = e.UserState
            ElseIf TypeOf e.UserState Is String Then
                Information = e.UserState
            ElseIf TypeOf e.UserState Is Boolean Then
                CanCancel = e.UserState
            ElseIf TypeOf e.UserState Is BackgroundWorker AndAlso BackgroundWorker Is e.UserState Then
                Reset()
            End If
        End Sub
        ''' <summary>Handles <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.RunWorkerCompleted">RunWorkerCompleted</see> event.</summary>
        ''' <param name="sender"><see cref="BackgroundWorker"/></param>
        ''' <param name="e">event arguments</param>
        ''' <remarks>This implementation sets <see cref="DialogResult"/> to <see cref="DialogResult.Cancel"/> when <paramref name="e"/>.<see cref="RunWorkerCompletedEventArgs.Cancelled">Cancelled</see> is true; to <see cref="DialogResult.Abort"/> when <paramref name="e"/>.<see cref="RunWorkerCompletedEventArgs.[Error]"/> isnot nothing and to <see cref="DialogResult.OK"/> in all other cases. Then sets <see cref="WorkerResult"/>. If <see cref="CloseOnFinish"/> is true, closes the form.</remarks>
        Protected Overridable Sub OnRunWorkerCompleted(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw.RunWorkerCompleted
            If e.Cancelled Then : Me.DialogResult = Windows.Forms.DialogResult.Cancel
            ElseIf e.Error IsNot Nothing Then : Me.DialogResult = Windows.Forms.DialogResult.Abort
            Else : Me.DialogResult = Windows.Forms.DialogResult.OK : End If
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
        Public Property WorkerResult() As RunWorkerCompletedEventArgs
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
        Public Property Prompt$()
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
        Public Property Information$()
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
        Public Property CanCancel() As Boolean
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
        Public Overridable Sub Reset()
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
        Public Property DoWorkOnShow() As Boolean
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
        Public Property WorkerArgument() As Object
            <DebuggerStepThrough()> Get
                Return _WorkerArgument
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Object)
                _WorkerArgument = value
            End Set
        End Property
    End Class
End Namespace
'#End If