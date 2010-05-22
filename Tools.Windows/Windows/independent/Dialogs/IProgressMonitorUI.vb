Imports System.Windows.Forms
Imports System.Runtime.CompilerServices

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.IndependentT
    ''' <summary>Represents progress monitor dialog with progress description, progress bar and cancel button</summary>
    ''' <version version="1.5.3" stage="Nightly">This interface is new in version 1.5.3</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Interface IProgressMonitorUI
        Inherits ThreadingT.IInvoke
        ''' <summary>Gets <see cref="BackgroundWorker"/> this form repports progress of</summary>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        ''' <remarks></remarks>
        Property BackgroundWorker() As BackgroundWorker
        ''' <summary>Gets or sets current style of progress bar</summary>
        ''' <remarks>When value of this property is not one of values defined in the <see cref="IndependentT.ProgressBarStyle"/> enumeration, it means the prograss bar reports current status in an implementation-specific way. When property is set to unrecognized value, implementation may corce it to one of recognized values.</remarks>
        <DefaultValue(GetType(ProgressBarStyle), "Definite")> _
        Property ProgressBarStyle() As ProgressBarStyle
        ''' <summary>Gets or sets current value of <see cref="ProgressBar"/> that reports progress</summary>
        ''' <exception cref="ArgumentException">Value being set is smaller than 0 or greater than 100.</exception>
        Property Progress() As Integer
        <DefaultValue(True)> _
        Property CloseOnFinish() As Boolean
        ''' <summary>Gets or sets result of <see cref="BackgroundWorker"/> work</summary>
        ''' <returns>Null until <see cref="BackgroundWorker.RunWorkerCompleted"/> event occures. That returns its e parameter.</returns>
        Property WorkerResult() As RunWorkerCompletedEventArgs
        ''' <summary>Gets or sets prompt diaplyed in upper part of form</summary>
        <DefaultValue(GetType(String), Nothing)> _
        Property Prompt$()
        ''' <summary>Gets or sets informative text displayed below the progress bar</summary>
        <DefaultValue(GetType(String), Nothing)> _
        Property Information$()
        ''' <summary>Gets or sets value indicationg if dialog supports cancelation</summary>
        ''' <value>Default value depends on <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.WorkerSupportsCancellation">WorkerSupportsCancellation</see> in time when it is passed to CTor.</value>
        Property CanCancel() As Boolean
        ''' <summary>Resets the dialog</summary>
        ''' <remarks>In case you want to use the dialog from multiple runs of <see cref="BackgroundWorker"/>, you should call this method before each (excluding first, but you can to) runs of <see cref="BackgroundWorker"/>. Alternativly you can report new run using <see cref="BackgroundWorker.ReportProgress"/> - see <see cref="OnProgressChanged"/>.</remarks>
        Sub Reset()
        ''' <summary>Gets or sets value indicationg if form will call <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.RunWorkerAsync">RunWorkerAsync</see> when <see cref="Shown"/> event occures.</summary>
        ''' <seelaso cref="WorkerArgument"/>
        ''' <value>Default value is true</value>
        <DefaultValue(True)> _
        Property DoWorkOnShow() As Boolean
        ''' <summary>Gets or sets value of argument passed to <see cref="BackgroundWorker.DoWork"/> when <see cref="DoWorkOnShow"/> is true</summary>
        <DefaultValue(GetType(Object), Nothing)> _
        Property WorkerArgument() As Object
        ''' <summary>Gets or sets dialog title</summary>
        ''' <value>Title of window showing the progress. Default value is implementation dependent (e.g. localized word "Progress")</value>
        ''' <returns>Current title of window showing progress</returns>
        Property Title As String
        ''' <summary>Shows window modally</summary>
        ''' <param name="owner">Owner object of dialog. Each implementation should support at least objects of type <see cref="System.Windows.Forms.IWin32Window"/> (e.g. <see cref="Form"/>), <see cref="System.Windows.Interop.IWin32Window"/> and <see cref="Windows.Window"/>. When owner is not of recognized type (or is null, it's ignored.</param>
        ''' <returns>True when dialog was closed normally, false if it was closed because of user has cancelled the operation</returns>
        Function ShowDialog(Optional ByVal owner As Object = Nothing) As Boolean
        ''' <summary>Gets an object that can be used as owner for modal windows</summary>
        ''' <returns>Depending on implementation this method returns either <see cref="Windows.Forms.IWin32Window"/>, <see cref="Windows.Interop.IWin32Window"/> or <see cref="Windows.Window"/>. Null when owner object was not created yet or if it was already closed or destroyed.</returns>
        ReadOnly Property OwnerObject As Object
    End Interface

    ''' <summary>Defines different progressbar styles</summary>
    ''' <seelaso cref="Windows.Forms.ProgressBarStyle"/>
    ''' <remarks>Any value different from <see cref="ProgressBarStyle.Indefinite"/> means that progressbar indicates actual progress. When value differes from <see cref="ProgressBarStyle.Definite"/> the style is implementation dependent.</remarks>
    Public Enum ProgressBarStyle
        ''' <summary>Progressbar does not show actual progresss. It only indicates that something is going on.</summary>
        ''' <seelaso cref="Windows.Forms.ProgressBarStyle.Marquee"/>
        Indefinite = Windows.Forms.ProgressBarStyle.Marquee
        ''' <summary>Progressbar indicates actual progress in form of bar or otherwise</summary>
        ''' <seelaso cref="Windows.Forms.ProgressBarStyle.Blocks"/>
        Definite = Windows.Forms.ProgressBarStyle.Blocks
    End Enum

End Namespace
#End If