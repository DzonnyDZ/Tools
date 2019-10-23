 'Stage: Nightly
Namespace WindowsT.WPF.DialogsT
    ''' <summary>Implements window which shows <see cref="ProgressMonitor"/></summary>
    Partial Friend NotInheritable Class ProgressMonitorWindow
        ''' <summary>A <see cref="DialogsT.ProgressMonitor"/> displayed by this window</summary>
        Private progressMonitor As ProgressMonitor
        ''' <summary>CTor - creates a new instance of the <see cref="ProgressMonitorWindow"/> class</summary>
        ''' <param name="progressMonitor">A <see cref="DialogsT.ProgressMonitor"/> to be shown by this window</param>
        ''' <exception cref="ArgumentNullException"><paramref name="progressMonitor"/> is null</exception>
        Public Sub New(ByVal progressMonitor As ProgressMonitor)
            If progressMonitor Is Nothing Then Throw New ArgumentNullException("progressMonitor")
            InitializeComponent()
            Me.progressMonitor = progressMonitor
            Me.DataContext = progressMonitor
        End Sub

        ''' <summary>When true window can be closed without check if <see cref="ProgressMonitorImplementationControl.CancelCommand"/> can be executed</summary>
        Private allowClose As Boolean
        ''' <summary>Closes the window without check if <see cref="ProgressMonitorImplementationControl.CancelCommand"/> can be executed</summary>
        Public Sub ForceClose()
            Try
                allowClose = True
                Me.Close()
            Catch ex As Exception
                allowClose = False
            End Try
        End Sub

        Private Sub Window_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
            If allowClose OrElse progressMonitor.WorkerFinished Then Exit Sub
            If ProgressMonitorImplementationControl.CancelCommand.CanExecute(e, Control) Then
                ProgressMonitorImplementationControl.CancelCommand.Execute(e, Control)
                e.Cancel = True
            Else
                Media.SystemSounds.Beep.Play()
                e.Cancel = True
            End If
        End Sub

        ''' <summary>Gets control implementing progress monitor UI</summary>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public ReadOnly Property ImplementationControl As ProgressMonitorImplementationControl
            Get
                Return Control
            End Get
        End Property

    End Class
End Namespace
