'#If Config <= Nightly Then 'set in project file
Namespace WindowsT.NativeT
    Friend Class frmTestForm
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        Public WndProcWrites As Boolean
        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            Dim mcode As Tools.API.Messages.WindowMessages = m.Msg
            MyBase.WndProc(m)
            If WndProcWrites Then
                Debug.Print("Old: {0}", mcode)
            End If
        End Sub
    End Class
End Namespace
'#End If