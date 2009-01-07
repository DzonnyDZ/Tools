Imports Tools.WindowsT.FormsT, Tools.ExtensionsT, Tools.CollectionsT.SpecializedT
Imports iMsg = Tools.WindowsT.IndependentT.MessageBox, Tools.IOt.FileSystemTools

Namespace Data
    ''' <summary>Wizard step for synchronize with database process to select folders to synchronize</summary>
    Friend Class SelectFoldersStep
        Implements IWizardControl
        ''' <summary>Contains value of the <see cref="SelectDatabaseStep"/> property</summary>
        Private _SelectDatabaseStep As SelectDatabaseStep
        ''' <summary>CTor</summary>
        ''' <param name="SelectDatabaseStep">Previous step</param>
        Public Sub New(ByVal SelectDatabaseStep As SelectDatabaseStep)
            _SelectDatabaseStep = SelectDatabaseStep
            InitializeComponent()
        End Sub
        ''' <summary>Gets previous step</summary>
        ''' <returns>Previous step passed to CTor</returns>
        Public ReadOnly Property SelectDatabaseStep() As SelectDatabaseStep
            Get
                Return _SelectDatabaseStep
            End Get
        End Property

        ''' <summary>Asks wizard control for control that follows after it.</summary>
        ''' <returns>New instance of <see cref="SynchronizeStep"/> class</returns>
        Public Function GetNext() As System.Windows.Forms.Control Implements WindowsT.FormsT.IWizardControl.GetNext
            Return New SynchronizeStep(Me)
        End Function
        ''' <summary>Contains value of the <see cref="Wizard"/> property</summary>
        Private _Wizard As Wizard
        ''' <summary>This property is being set by <see cref="Wizard"/> when control is added to <see cref="Wizard"/>.</summary>
        ''' <returns>Owning wizard of this instance</returns>
        Public Property Wizard() As WindowsT.FormsT.Wizard Implements WindowsT.FormsT.IWizardControl.Wizard
            Get
                Return _Wizard
            End Get
            Set(ByVal value As WindowsT.FormsT.Wizard)
                _Wizard = value
            End Set
        End Property

        Private Sub cmdPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFolder.Click
            If fbdFolders.ShowDialog = DialogResult.OK Then txtFolder.Text = fbdFolders.SelectedPath
        End Sub

        Private Sub lvwFolders_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvwFolders.KeyDown
            If e.KeyData = Keys.Delete Then
                Dim todel As New List(Of ListViewItem)(lvwFolders.SelectedItems)
                For Each item In todel
                    lvwFolders.Items.Remove(item)
                Next
            End If
        End Sub
        ''' <summary>Gets list of selected folders</summary>
        ''' <returns>Folders currently selected</returns>
        Public ReadOnly Property Folders() As IEnumerable(Of String)
            Get
                Dim del = Function() (From item In lvwFolders.Items.AsTypeSafe Select CStr(item.Tag)).ToArray
                If lvwFolders.InvokeRequired Then
                    Return lvwFolders.Invoke(del)
                Else
                    Return del.Invoke
                End If
            End Get
        End Property

        Private Sub cmdAddFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddFolder.Click
            If Not IO.Directory.Exists(txtFolder.Text) Then
                iMsg.MsgBox(My.Resources.Folder0DoesNotExists.f(txtFolder.Text), MsgBoxStyle.Exclamation, My.Resources.BadFolder)
                Exit Sub
            End If
            Dim NewPath As New IOt.Path(txtFolder.Text)
            For Each item As ListViewItem In lvwFolders.Items
                Dim OldPath As New IOt.Path(CStr(item.Tag))
                If OldPath.IsChildOf(NewPath) Then
                    iMsg.MsgBox(My.Resources.ChildFolder0Of1IsAlreadySelected.f(OldPath, NewPath), MsgBoxStyle.Exclamation, My.Resources.BadFolder)
                    Exit Sub
                ElseIf OldPath.IsparentOf(NewPath) Then
                    iMsg.MsgBox(My.Resources.ParentFolder0Of1IsAlreadySelected.f(OldPath, NewPath), MsgBoxStyle.Exclamation, My.Resources.BadFolder)
                    Exit Sub
                ElseIf OldPath.Equals(NewPath) Then
                    iMsg.MsgBox(My.Resources.Path0IsAlreadySelected.f(NewPath), MsgBoxStyle.Exclamation, My.Resources.BadFolder)
                    Exit Sub
                End If
            Next
            Dim li = lvwFolders.Items.Add(NewPath.Path)
            li.Tag = NewPath.Path
            imlFolderImages.Images.Add(NewPath.Path, NewPath.GetIcon)
            li.ImageKey = NewPath.Path
        End Sub
    End Class
End Namespace