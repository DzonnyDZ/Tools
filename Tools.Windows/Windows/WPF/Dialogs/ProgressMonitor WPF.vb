Imports System.Windows, System.Windows.Controls
Imports System.Windows.Input
Imports Tools.WindowsT.IndependentT
Imports Tools.ComponentModelT
Imports Tools.ThreadingT

#If Config <= Nightly Then  'Stage: Nightly
Namespace WindowsT.WPF.DialogsT
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    <TemplatePart(Name:=ProgressMonitorImplementationControl.PART_MainInfo, Type:=GetType(TextBlock))> _
    <TemplatePart(Name:=ProgressMonitorImplementationControl.PART_ProgressBar, Type:=GetType(ProgressBar))> _
    <TemplatePart(Name:=ProgressMonitorImplementationControl.PART_Info, Type:=GetType(TextBlock))> _
    <TemplatePart(Name:=ProgressMonitorImplementationControl.PART_Cancel, Type:=GetType(Button))> _
    Public Class ProgressMonitorImplementationControl
        Inherits Windows.Controls.Control
        ''' <summary>Identifies placeholder for main information text block</summary>
        Public Const PART_MainInfo As String = "PART_MainInfo"
        ''' <summary>Identifies placeholder for progress bar</summary>
        Public Const PART_ProgressBar As String = "PART_ProgressBar"
        ''' <summary>Identifies placeholder for secondary information text block</summary>
        Public Const PART_Info As String = "PART_Info"
        ''' <summary>Identifies placeholder for cancel button</summary>
        Public Const PART_Cancel As String = "PART_Cancel"
        ''' <summary>Initializer</summary>
        Shared Sub New()
            DefaultStyleKeyProperty.OverrideMetadata(GetType(ProgressMonitorImplementationControl), New FrameworkPropertyMetadata(GetType(ProgressMonitorImplementationControl)))
            InitializeCommands()
        End Sub
#Region "Commands"
#Region "Cancel"
        ''' <summary>Gets command to be executed when button is clicked.</summary>
        ''' <returns>Command to be execued when button is clicked</returns>
        Public Shared ReadOnly Property CancelCommand() As RoutedCommand
            Get
                Return _CancelCommand
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="CancelCommand"/> property</summary>
        Private Shared _CancelCommand As RoutedCommand
        ''' <summary>Initializes comands</summary>
        Private Shared Sub InitializeCommands()
            _CancelCommand = New RoutedCommand("CancelCommand", GetType(ProgressMonitorImplementationControl))
            CommandManager.RegisterClassCommandBinding(GetType(ProgressMonitorImplementationControl), New CommandBinding(_CancelCommand, AddressOf OnCancel, AddressOf CanCancel))
        End Sub
        ''' <summary>Called to determine wheather the <see cref="CancelCommand"/> can be executed</summary>
        ''' <param name="sender">Source of event (<see cref="ProgressMonitorImplementationControl"/>)</param>
        ''' <param name="e">Event arguments</param>
        Private Shared Sub CanCancel(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
            Dim control As ProgressMonitorImplementationControl = TryCast(sender, ProgressMonitorImplementationControl)
            If control IsNot Nothing Then
                control.CanCancel(e)
            End If
        End Sub
        ''' <summary>Called to determine wheather the <see cref="CancelCommand"/> can be executed</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub CanCancel(ByVal e As CanExecuteRoutedEventArgs)
            e.CanExecute = CancelEnabled
        End Sub
        ''' <summary>Called when <see cref="CancelCommand"/> is invoked</summary>
        ''' <param name="sender">Source of event (<see cref="ProgressMonitorImplementationControl"/>)</param>
        ''' <param name="e">Event arguments</param>
        Private Shared Sub OnCancel(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            Dim control As ProgressMonitorImplementationControl = TryCast(sender, ProgressMonitorImplementationControl)
            If control IsNot Nothing Then
                control.OnCancel(e)
            End If
        End Sub
        ''' <summary>Called when the <see cref="CancelCommand"/> is executed; raises the <see cref="Cancel"/> event</summary>
        ''' <param name="e">Event arguments.</param>
        Protected Overridable Sub OnCancel(ByVal e As ExecutedRoutedEventArgs)
            RaiseEvent Cancel(Me, e)
        End Sub
#End Region
#End Region
#Region "Events"
        ''' <summary>Raised when user requests operation to be cancelled</summary>
        Public Event Cancel As EventHandler(Of ExecutedRoutedEventArgs)
#End Region
#Region "Properties"
#Region "ProgressBarShowsProgress"
        ''' <summary>Gets or sets current style of progress bar</summary>
        ''' <value>True to show progressbar which indicates percentage progress of operation, false to show progressbar which only indicates that something is going on but does not indicate actual progress</value>
        Public Property ProgressBarStyle As ProgressBarStyle
            Get
                Return GetValue(ProgressBarStyleProperty)
            End Get
            Set(ByVal value As ProgressBarStyle)
                SetValue(ProgressBarStyleProperty, value)
            End Set
        End Property
        ''' <summary>Metadata of the <see cref="ProgressBarStyle"/> property</summary>
        Public Shared ReadOnly ProgressBarStyleProperty As DependencyProperty = DependencyProperty.Register("ProgressBarStyle", GetType(ProgressBarStyle), GetType(ProgressMonitorImplementationControl), New FrameworkPropertyMetadata(ProgressBarStyle.Definite))
#End Region
#Region "Progress"
        ''' <summary>Gets or sets current progress shown by progressbar</summary>
        Public Property Progress() As Integer
            <DebuggerStepThrough()> Get
                Return GetValue(ProgressProperty)
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Integer)
                SetValue(ProgressProperty, value)
            End Set
        End Property
        ''' <summary>Metadata of the <see cref="Progress"/> property</summary>
        Public Shared ReadOnly ProgressProperty As DependencyProperty = _
                               DependencyProperty.Register("Progress", GetType(Integer), GetType(ProgressMonitorImplementationControl), _
                               New FrameworkPropertyMetadata(0, Nothing, AddressOf CoerceProgressValue))
        ''' <summary>Called whenever a value of the <see cref="Progress"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
        ''' <param name="d">The object that the property exists on. When the callback is invoked, the property system passes this value.</param>
        ''' <param name="baseValue">The new value of the property, prior to any coercion attempt.</param>
        ''' <returns>The coerced value (with appropriate type).</returns>
        ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not of type <see cref="ProgressMonitorImplementationControl"/> -or- <paramref name="baseValue"/> is not of type <see cref="Integer"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
        Private Shared Function CoerceProgressValue(ByVal d As System.Windows.DependencyObject, ByVal baseValue As Object) As Object
            If d Is Nothing Then Throw New ArgumentNullException("d")
            If Not TypeOf d Is ProgressMonitorImplementationControl Then Throw New Tools.TypeMismatchException("d", d, GetType(ProgressMonitorImplementationControl))
            If Not TypeOf baseValue Is Integer AndAlso Not baseValue Is Nothing Then Throw New Tools.TypeMismatchException("baseValue", baseValue, GetType(Integer))
            Return DirectCast(d, ProgressMonitorImplementationControl).CoerceProgressValue(baseValue)
        End Function
        ''' <summary>Called whenever a value of the <see cref="Progress"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
        ''' <param name="baseValue">The new value of the property, prior to any coercion attempt, but ensured to be of correct type.</param>
        ''' <returns>The coerced value (with appropriate type). 0 whan <paramref name="baseValue"/> is lower than 0; 100 when <paramref name="baseValue"/> is greater than 100; <paramref name="baseValue"/> otherwise.</returns>
        Protected Overridable Function CoerceProgressValue(ByVal baseValue As Integer) As Integer
            If baseValue < 0 Then Return 0
            If baseValue > 100 Then Return 100
            Return baseValue
        End Function
#End Region
#Region "Prompt"
        ''' <summary>Gets or sets prompt shown in window</summary>      
        Public Property Prompt As String
            Get
                Return GetValue(PromptProperty)
            End Get
            Set(ByVal value As String)
                SetValue(PromptProperty, value)
            End Set
        End Property
        ''' <summary>Metadata of the <see cref="Prompt"/> property</summary>                                                   
        Public Shared ReadOnly PromptProperty As DependencyProperty = DependencyProperty.Register(
            "Prompt", GetType(String), GetType(ProgressMonitorImplementationControl), New FrameworkPropertyMetadata(Nothing))
#End Region
#Region "Information"
        ''' <summary>Gets or sets additional information shown in the window</summary>      
        Public Property Information As String
            Get
                Return GetValue(InformationProperty)
            End Get
            Set(ByVal value As String)
                SetValue(InformationProperty, value)
            End Set
        End Property
        ''' <summary>Metadata of the <see cref="Information"/> property</summary>                                                   
        Public Shared ReadOnly InformationProperty As DependencyProperty = DependencyProperty.Register(
            "Information", GetType(String), GetType(ProgressMonitorImplementationControl), New FrameworkPropertyMetadata(Nothing))
#End Region
#Region "Title"
        ''' <summary>Gets or sets title of progress monitor window</summary>      
        Public Property Title As String
            Get
                Return GetValue(TitleProperty)
            End Get
            Set(ByVal value As String)
                SetValue(TitleProperty, value)
            End Set
        End Property
        ''' <summary>Metadata of the <see cref="Title"/> property</summary>                                                   
        Public Shared ReadOnly TitleProperty As DependencyProperty = DependencyProperty.Register(
            "Title", GetType(String), GetType(ProgressMonitorImplementationControl), New FrameworkPropertyMetadata(Tools.WindowsT.WPF.Dialogs.Progress))
#End Region
#Region "CancelEnabled"
        ''' <summary>Gets or sets value indicating if cancel button is enabled</summary>      
        Public Property CancelEnabled As Boolean
            Get
                Return GetValue(CancelEnabledProperty)
            End Get
            Set(ByVal value As Boolean)
                SetValue(CancelEnabledProperty, value)
            End Set
        End Property
        ''' <summary>Metadata of the <see cref="CancelEnabled"/> property</summary>                                                   
        Public Shared ReadOnly CancelEnabledProperty As DependencyProperty = DependencyProperty.Register(
            "CancelEnabled", GetType(Boolean), GetType(ProgressMonitorImplementationControl), New FrameworkPropertyMetadata(True))
#End Region
#End Region

   

    End Class

    ''' <summary>This class provides predefined progress monitor with <see cref="ProgressBar"/> for <see cref="BackgroundWorker"/></summary>
    ''' <remarks>See documentation of the <see cref="ProgressMonitor.OnProgressChanged"/> method in order to see rich options for reporting progress.</remarks>
    Public Class ProgressMonitor
        Implements IProgressMonitorUI, INotifyPropertyChanged
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
            Me.BackgroundWorker = bgw
        End Sub
        ''' <summary>CTor with title text and prompt</summary>
        ''' <param name="bgw"><see cref="BackgroundWorker"/> this form will report progress for</param>
        ''' <exception cref="ArgumentNullException" ><paramref name="bgw"/> is null</exception>
        ''' <param name="title">Title text of window (see <see cref="Text"/>)</param>
        ''' <param name="prompt">Prompt text (see <see cref="Prompt"/>)</param>
        ''' <filterpriority>2</filterpriority>
        Public Sub New(ByVal bgw As BackgroundWorker, ByVal title$, Optional ByVal prompt$ = Nothing)
            Me.new(bgw)
            Me.Title = title
            Me.Prompt = prompt
        End Sub
#End Region
#Region "Show"
        ''' <summary>Shows progress form and runs worker</summary>
        ''' <param name="bgw">Worker to run</param>
        ''' <param name="Text">Title text of window (see <see  cref="Text"/>)</param>
        ''' <param name="Prompt">Text prompt (see <see cref="Prompt"/>)</param>
        ''' <param name="Owner">Any object that implements <see cref="System.Windows.Forms.IWin32Window"/> or <see cref="Windows.Interop.IWin32Window"/>, or <see cref="Windows.Window"/> that represents the top-level window that will own the modal dialog box.</param>
        ''' <param name="WorkerArgument">Optional parameter for background worker</param>
        ''' <returns>Result of work of <paramref name="bgw"/></returns>
        Public Overloads Shared Function Show(ByVal bgw As BackgroundWorker, ByVal text As String, ByVal prompt As String, Optional ByVal owner As Object = Nothing, Optional ByVal workerArgument As Object = Nothing) As RunWorkerCompletedEventArgs
            Dim mon As New ProgressMonitor(bgw, text, prompt)
            mon.WorkerArgument = workerArgument
            mon.ShowDialog(owner)
            Return mon.WorkerResult
        End Function

        ''' <summary>Window being currently shown</summary>
        Private WithEvents _window As ProgressMonitorWindow
        'Private WithEvents control As ProgressMonitorImplementationControl

        ''' <summary>Shows window modally</summary>
        ''' <param name="owner">Owner object of dialog. It can be either <see cref="System.Windows.Forms.IWin32Window"/> (e.g. <see cref="Windows.Forms.Form"/>), <see cref="System.Windows.Interop.IWin32Window"/> or <see cref="Windows.Window"/>. When owner is not of recognized type (or is null), it's ignored.</param>
        ''' <returns>True when dialog was closed normally, false if it was closed because of user has cancelled the operation</returns>
        Public Overloads Function ShowDialog(Optional ByVal owner As Object = Nothing) As Boolean Implements IProgressMonitorUI.ShowDialog
            _window = New ProgressMonitorWindow(Me)
            If TypeOf owner Is Windows.Window Then
                Return _window.ShowDialog(DirectCast(owner, Window))
            ElseIf TypeOf owner Is Windows.Forms.IWin32Window Then
                Return WindowsT.InteropT.InteropExtensions.ShowDialog(_window, DirectCast(owner, Forms.IWin32Window))
            ElseIf TypeOf owner Is Windows.Interop.IWin32Window Then
                Return WindowsT.InteropT.InteropExtensions.ShowDialog(_window, DirectCast(owner, Interop.IWin32Window))
            Else
                Return _window.ShowDialog
            End If
        End Function

        Private Sub window_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _window.Closed
            _window = Nothing
        End Sub
        Private Sub window_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles _window.Loaded
            If DoWorkOnShow Then BackgroundWorker.RunWorkerAsync(WorkerArgument)
        End Sub
        Private Sub window_ImplementationControl_Cancel(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs) Handles _window.ImplementationControl.Cancel
            If CancelEnabled Then
                CancelPending = True
                BackgroundWorker.CancelAsync()
            End If
        End Sub
#End Region
#Region "Properties"
        ''' <summary>A background worker which does the work</summary>
        Private WithEvents bgw As BackgroundWorker
        ''' <summary>Gets <see cref="BackgroundWorker"/> this form repports progress of</summary>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property BackgroundWorker() As BackgroundWorker Implements IProgressMonitorUI.BackgroundWorker
            <DebuggerStepThrough()> Get
                Return bgw
            End Get
            Private Set(ByVal value As BackgroundWorker)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                If value IsNot bgw Then
                    bgw = value
                    CanCancel = bgw.WorkerSupportsCancellation
                    ProgressBarStyle = If(bgw.WorkerReportsProgress, ProgressBarStyle.Definite, ProgressBarStyle.Indefinite)
                    OnPropertyChanged("BackgroundWorker")
                End If
            End Set
        End Property
        Private _ProgressBarStyle As ProgressBarStyle = IndependentT.ProgressBarStyle.Definite
        ''' <summary>Gets or sets current style of progress bar</summary>
        ''' <remarks>When value of this property is not one of values defined in the <see cref="IndependentT.ProgressBarStyle"/> enumeration, it means the prograss bar reports current status in an implementation-specific way. When property is set to unrecognized value this implementation does not corce it and treats it as <see cref="ProgressBarStyle.Definite"/>. Custom control style can take benefit of such value.</remarks>
        <DefaultValue(GetType(ProgressBarStyle), "Definite")>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)>
        Public Property ProgressBarStyle As IndependentT.ProgressBarStyle Implements IndependentT.IProgressMonitorUI.ProgressBarStyle
            Get
                Return _ProgressBarStyle
            End Get
            Set(ByVal value As IndependentT.ProgressBarStyle)
                If value <> ProgressBarStyle Then
                    _ProgressBarStyle = value
                    OnPropertyChanged("ProgressBarStyle")
                End If
            End Set
        End Property
        Private _progress As Boolean = 0
        ''' <summary>Gets or sets current value of <see cref="ProgressBar"/> that reports progress</summary>
        ''' <exception cref="ArgumentOutOfRangeException">Value being set is smaller than 0 or greater than 100.</exception>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property Progress() As Integer Implements IProgressMonitorUI.Progress
            <DebuggerStepThrough()> Get
                Return _progress
            End Get
            <DebuggerStepThrough()> Protected Set(ByVal value As Integer)
                If value <> Progress Then
                    If value < 0 OrElse value > 100 Then Throw New ArgumentOutOfRangeException("value")
                    _progress = value
                    OnPropertyChanged("Progress")
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="CloseOnFinish"/> property</summary>
        Private _CloseOnFinish As Boolean = True
        ''' <summary>Gets or sets value indicating if form automatically closes when <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.RunWorkerCompleted">RunWorkerCompleted</see> event occures.</summary>
        ''' <value>New behavoir. Defalt value is true.</value>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property CloseOnFinish() As Boolean Implements IProgressMonitorUI.CloseOnFinish
            Get
                Return _CloseOnFinish
            End Get
            Set(ByVal value As Boolean)
                If CloseOnFinish <> value Then
                    _CloseOnFinish = value
                    OnPropertyChanged("CloseOnFinish")
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="WorkerResult"/> property</summary>
        Private _WorkerResult As RunWorkerCompletedEventArgs
        ''' <summary>Gets or sets result of <see cref="BackgroundWorker"/> work</summary>
        ''' <returns>Null until <see cref="BackgroundWorker.RunWorkerCompleted"/> event occures. That returns its e parameter.</returns>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property WorkerResult() As RunWorkerCompletedEventArgs Implements IProgressMonitorUI.WorkerResult
            <DebuggerStepThrough()> Get
                Return _WorkerResult
            End Get
            Protected Set(ByVal value As RunWorkerCompletedEventArgs)
                If WorkerResult IsNot value Then
                    _WorkerResult = value
                    OnPropertyChanged("WorkerResult")
                End If
            End Set
        End Property
        Private _Prompt$
        ''' <summary>Gets or sets prompt diaplyed in upper part of form</summary>
        <DefaultValue(GetType(String), Nothing)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        Public Property Prompt$() Implements IProgressMonitorUI.Prompt
            <DebuggerStepThrough()> Get
                Return _Prompt
            End Get
            Set(ByVal value$)
                If value <> Prompt Then
                    _Prompt = value
                    OnPropertyChanged("Prompt")
                End If
            End Set
        End Property
        Private _Information$
        ''' <summary>Gets or sets informative text displayed below the progress bar</summary>
        <DefaultValue(GetType(String), Nothing)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        Public Property Information$() Implements IProgressMonitorUI.Information
            <DebuggerStepThrough()> Get
                Return _Information
            End Get
            Set(ByVal value$)
                If value <> _Information$ Then
                    _Information$ = value
                    OnPropertyChanged("Information")
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="CanCancel"/> property</summary>
        Private _CanCancel As Boolean
        ''' <summary>Gets or sets value indicationg if dialog supports cancelation</summary>
        ''' <value>Default value depends on <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.WorkerSupportsCancellation">WorkerSupportsCancellation</see> in time when it is passed to CTor.</value>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property CanCancel() As Boolean Implements IProgressMonitorUI.CanCancel
            <DebuggerStepThrough()> Get
                Return _CanCancel
            End Get
            Set(ByVal value As Boolean)
                If value <> CanCancel Then
                    _CanCancel = value
                    OnPropertyChanged("CanCancel")
                    OnPropertyChanged("CancelEnabled")
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="DoWorkOnShow"/> property</summary>
        Private _DoWorkOnShow As Boolean = True
        ''' <summary>Gets or sets value indicationg if form will call <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.RunWorkerAsync">RunWorkerAsync</see> when <see cref="Shown"/> event occures.</summary>
        ''' <seelaso cref="WorkerArgument"/>
        ''' <value>Default value is true</value>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <DefaultValue(True)> _
        Public Property DoWorkOnShow() As Boolean Implements IProgressMonitorUI.DoWorkOnShow
            <DebuggerStepThrough()> Get
                Return _DoWorkOnShow
            End Get
            Set(ByVal value As Boolean)
                If value <> DoWorkOnShow Then
                    _DoWorkOnShow = value
                    OnPropertyChanged("DoWorkOnShow")
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="WorkerArgument"/> property</summary>
        Private _WorkerArgument As Object
        ''' <summary>Gets or sets value of argument passed to <see cref="BackgroundWorker.DoWork"/> when <see cref="DoWorkOnShow"/> is true</summary>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
        <DefaultValue(GetType(Object), Nothing)> _
        Public Property WorkerArgument() As Object Implements IProgressMonitorUI.WorkerArgument
            <DebuggerStepThrough()> Get
                Return _WorkerArgument
            End Get
            Set(ByVal value As Object)
                If WorkerArgument IsNot value Then
                    _WorkerArgument = value
                    OnPropertyChanged("WorkerArgument")
                End If
            End Set
        End Property
        Private _Title$ = Tools.WindowsT.WPF.Dialogs.Progress
        ''' <summary>Gets or sets dialog title</summary>
        ''' <value>Title of window showing the progress. Default value is localized word "Progress"</value>
        ''' <returns>Current title of window showing progress</returns>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <LDefaultValue(GetType(Tools.WindowsT.WPF.Dialogs), "Progress")> _
        Public Property Title As String Implements IProgressMonitorUI.Title
            Get
                Return _Title
            End Get
            Set(ByVal value As String)
                If value <> Title Then
                    _Title = value
                    OnPropertyChanged("Title")
                End If
            End Set
        End Property

        ''' <summary>Gets value indicating if Cancle button is enabled</summary>
        ''' <returns>True when both - <see cref="CanCancel"/> and <see cref="CancelPending"/> are enabled</returns>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property CancelEnabled As Boolean
            Get
                Return CanCancel AndAlso Not CancelPending AndAlso Not WorkerFinished
            End Get
        End Property
        Private _CancelPending As Boolean = False
        ''' <summary>Gets or sets value indicating if user has pressed the Cancel button and dialog is now waiting for process to cancel itself</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Property CancelPending As Boolean
            Get
                Return _CancelPending
            End Get
            Protected Set(ByVal value As Boolean)
                If CancelPending <> value Then
                    _CancelPending = value
                    OnPropertyChanged("CancelPending")
                    OnPropertyChanged("CancelEnabled")
                End If
            End Set
        End Property
        ''' <summary>Gets an object that can be used as owner for modal windows</summary>
        ''' <returns>When progress monitor dialog is currently shown, returns a <see cref="Windows.Window"/> representing the dialog; otherwise null.</returns>
        Public ReadOnly Property Window As Object Implements IndependentT.IProgressMonitorUI.OwnerObject
            Get
                Return _window
            End Get
        End Property
        Private _WorkerFinished As Boolean = False
        ''' <summary>Gets or sets value indicating if worker has finished it's work</summary>
        Public Property WorkerFinished As Boolean
            Get
                Return _WorkerFinished
            End Get
            Protected Set(ByVal value As Boolean)
                If WorkerFinished <> value Then
                    _WorkerFinished = value
                    OnPropertyChanged("WorkerFinished")
                    OnPropertyChanged("CancelEnabled")
                End If
            End Set
        End Property
#End Region
#Region "Worker events"
        ''' <summary>Handles <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.ProgressChanged">ProgressChanged</see> event</summary>
        ''' <param name="sender"><see cref="BackgroundWorker"/></param>
        ''' <param name="e">Event erguments</param>
        ''' <remarks>Default implementation works in following way:
        ''' <list type="bullet">
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.ProgressPercentage">ProgressPercentage</see> is greater than or equal to zero then sets this value to the <see cref="Progress"/> property. Values smaller than zero are ignored.</item>
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> is <see cref="Windows.Forms.ProgressBarStyle"/> sets <see cref="ProgressBarStyle"/> to given value</item>
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> is <see cref="String"/> passes that value to the <see cref="Information"/> property.</item>
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> is <see cref="Boolean"/> passes that value to the <see cref="CanCancel"/> property.</item>
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> is <see cref="BackgroundWorker"/> (same instance) than <see cref="Reset"/> method is called.</item>
        ''' </list>
        ''' </remarks>
        ''' <exception cref="ArgumentException"><paramref name="e"/>.<see cref="ProgressChangedEventArgs.ProgressPercentage">ProgressPercentage</see> is greater than 100.</exception>
        Protected Overridable Sub OnProgressChanged(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgw.ProgressChanged
            If e.ProgressPercentage >= 0 Then Progress = e.ProgressPercentage
            If TypeOf e.UserState Is Forms.ProgressBarStyle OrElse TypeOf e.UserState Is ProgressBarStyle Then
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
        Protected Overridable Sub OnRunWorkerCompleted(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw.RunWorkerCompleted
            If e.Cancelled Then : window.DialogResult = False
            ElseIf e.Error IsNot Nothing Then : window.DialogResult = False
            Else : window.DialogResult = True : End If
            WorkerResult = e
            WorkerFinished = True
            If CloseOnFinish Then
                window.Close()
            End If
        End Sub
#End Region

        ''' <summary>Resets the dialog</summary>
        ''' <remarks>In case you want to use the dialog from multiple runs of <see cref="BackgroundWorker"/>, you should call this method before each (excluding first, but you can to) runs of <see cref="BackgroundWorker"/>. Alternativly you can report new run using <see cref="BackgroundWorker.ReportProgress"/> - see <see cref="OnProgressChanged"/>.</remarks>
        Public Overridable Sub Reset() Implements IProgressMonitorUI.Reset
            CancelPending = False
            Progress = 0
            WorkerResult = Nothing
            WorkerFinished = False
        End Sub

#Region "INotifyPropertyChanged"
        ''' <summary>Raises the <see cref="PropertyChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
            RaiseEvent PropertyChanged(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="PropertyChanged"/> event</summary>
        ''' <param name="propertyName">The name of the property that changed</param>
        Protected Sub OnPropertyChanged(ByVal propertyName$)
            OnPropertyChanged(New PropertyChangedEventArgs(propertyName))
        End Sub

        ''' <summary>Occurs when a property value changes.</summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
#End Region

        ''' <summary>Synchronously invokes a delegate in UI thread. Implements <see cref="IInvoke.Invoke"/>.</summary>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="params">Delegate parameters</param>
        ''' <returns>Result of delegate call</returns>
        ''' <remarks>Delegate is invoked in UI thtread only in case UI is shown (window had been shown and has not been closed yet), otherwise (or when <see cref="Window.Dispatcher"/> does not require it) delegate is invoked in current thread.
        ''' <para>This function is named <c>IInvoke_Invoke</c> to make extension functions in <see cref="IInvokeExtensions"/> to be always called instead of this function whan used with <see cref="ProgressMonitor"/>.</para></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Function IInvoke_Invoke(ByVal [delegate] As System.Delegate, ByVal ParamArray params() As Object) As Object Implements IInvoke.Invoke
            If window Is Nothing OrElse window.Dispatcher.CheckAccess Then
                Return [delegate].DynamicInvoke(params)
            Else
                Return window.Dispatcher.Invoke([delegate], params)
            End If
        End Function
    End Class
End Namespace
#End If