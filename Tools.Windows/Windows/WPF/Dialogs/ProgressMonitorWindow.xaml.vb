#If Config <= Nightly Then  'Stage: Nightly
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
    End Class
End Namespace
#End If