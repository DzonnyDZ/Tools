Imports Tools.WindowsT.FormsT, Tools.CollectionsT.SpecializedT, System.Linq, Tools.ExtensionsT
Imports iMsg = Tools.WindowsT.IndependentT.MessageBox
Imports Tools.DrawingT.DrawingIOt, Tools.MetadataT

Namespace Data
    ''' <summary>Wizard step for synchronize with database process to select perform synchronization</summary>
    Friend Class SynchronizeStep
        Implements IWizardControl
        ''' <summary>CTor</summary>
        ''' <param name="SelectFoldersStep">Previous step</param>
        Public Sub New(ByVal SelectFoldersStep As SelectFoldersStep)
            InitializeComponent()
            _SelectFoldersStep = SelectFoldersStep
            With FolderErrorMessageBox
                .Title = My.Resources.Error_
                .Icon = iMsg.GetIcon(WindowsT.IndependentT.MessageBox.MessageBoxIcons.Exclamation)
                .Buttons.Add(iMsg.MessageBoxButton.Abort)
                .Buttons.Add(iMsg.MessageBoxButton.Retry)
                .Buttons.Add(iMsg.MessageBoxButton.Ignore)
                .Buttons.Add(New iMsg.MessageBoxButton(My.Resources.IgnoreAll, DialogResult.OK, My.Resources.IgnoreAll_AccessKey))
            End With
            With ImageInOtherFolderMessageBox
                .Title = My.Resources.ImageInDatabase
                .Icon = iMsg.GetIcon(WindowsT.IndependentT.MessageBox.MessageBoxIcons.Warning)
                .Buttons.Add(WindowsT.IndependentT.MessageBox.MessageBoxButton.Ignore)
                .Buttons.Add(New iMsg.MessageBoxButton(My.Resources.Relocate, DialogResult.OK, My.Resources.Relocate_AccessKey))
                .CheckBoxes.Add(New iMsg.MessageBoxCheckBox(My.Resources.DoNotShowThisDialogAgain))
            End With
        End Sub
        ''' <summary>Contains value of the <see cref="SelectFoldersStep"/></summary>
        Private _SelectFoldersStep As SelectFoldersStep
        ''' <summary>Gets previous step</summary>
        ''' <returns>Previous step</returns>
        Public ReadOnly Property SelectFoldersStep() As SelectFoldersStep
            Get
                Return _SelectFoldersStep
            End Get
        End Property
        Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
            cmdStart.Enabled = False
            cmdCancel.Enabled = True
            flpOnConfilct.Enabled = False
            flpRelocate.Enabled = False
            flpMode.Enabled = False
            lblOnConflict.Enabled = False
            lblMode.Enabled = False
            lblOnDuplicitName.Enabled = False
            lblFileName.Text = My.Resources.Counting
            bgwSynchronize.RunWorkerAsync()
        End Sub
        ''' <summary>Asks wizard control for control that follows after it.</summary>
        ''' <returns>Null</returns>
        Public Function GetNext() As System.Windows.Forms.Control Implements WindowsT.FormsT.IWizardControl.GetNext
            Return Nothing
        End Function
        ''' <summary>Contains value of the <see cref="Wizard"/> property</summary>
        Private WithEvents _Wizard As Wizard
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

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            bgwSynchronize.CancelAsync()
            cmdCancel.Enabled = False
            lblFileName.Text = My.Resources.PleaseWait
        End Sub

        Private FolderErrorMessageBox As New WindowsT.FormsT.MessageBox
        Private ImageInOtherFolderMessageBox As New WindowsT.FormsT.MessageBox

        Private Sub bgwSynchronize_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwSynchronize.DoWork
            Dim Files As New List(Of String)
            Dim IgnoreAll As Boolean = False
            For Each Folder In Me.SelectFoldersStep.Folders
ReTryFolder:
                Try
                    For Each file As String In From f In IO.Directory.GetFiles(Folder, "*.jpeg", IO.SearchOption.AllDirectories).Union(IO.Directory.GetFiles(Folder, "*.jpg", IO.SearchOption.AllDirectories)) Select f Order By f Ascending
                        Files.Add(file)
                    Next
                Catch ex As Exception
                    If Not IgnoreAll Then
                        FolderErrorMessageBox.Prompt = My.Resources.ThereWasAnErrorProcessingFolder012.f(Folder, vbCrLf, ex.Message)
                        Select Case FolderErrorMessageBox.ShowDialogOn(Me)
                            Case DialogResult.Abort : Throw New OperationCanceledException
                            Case DialogResult.Retry : GoTo ReTryFolder
                            Case DialogResult.Ignore 'Do nothing
                            Case DialogResult.OK 'IgnoreAll
                                IgnoreAll = True
                        End Select
                    End If
                End Try
            Next
            Dim i% = 0
            Dim con As New SqlClient.SqlConnection(Me.SelectFoldersStep.SelectDatabaseStep.ConnectionString.ConnectionString)
            Dim dx As New DatabaseAccessDataContext(con)
            For Each File In Files
                bgwSynchronize.ReportProgress(-1, File)
                'Create instances
                Dim BitmapSize As Size
                Using Image As New Bitmap(File)
                    BitmapSize = Image.Size
                End Using
                Using JPEG As New JPEG.JPEGReader(File, SynchronizeMode <> SynchronizeModes.Add)
                    Dim Stg As New ExifT.ExifReaderSettings
                    Dim EMap As New ExifT.ExifMapGenerator(Stg)
                    Dim ExifReader As New ExifT.ExifReader(JPEG.GetExifStream, Stg)
                    Dim Exif As New ExifT.Exif(ExifReader)
                    Dim IPTC As New IptcT.Iptc(JPEG)
                    'Find in database
                    Dim FilePath = IO.Path.GetDirectoryName(File)
                    Dim FilePathL = FilePath.ToLower
                    Dim FileName = IO.Path.GetFileName(File)
                    Dim FileNameL = FileName.ToLower
                    Dim ImageInDB = (From pic In dx.PictureMetadatas _
                                     Where pic.Folder.ToLower = FilePathL AndAlso pic.FileName.ToLower = FileNameL).FirstOrDefault
                    If ImageInDB Is Nothing AndAlso RelocationMode <> RelocateMode.Ignore Then
                        'If not found in same folder, search by filename only
                        Dim ImageInDBOther = (From pic In dx.PictureMetadatas _
                                              Where pic.FileName.ToLower = FileNameL).FirstOrDefault
                        Dim NowRelocate As RelocateMode
                        If ImageInDBOther IsNot Nothing AndAlso RelocationMode = RelocateMode.Ask Then
                            ImageInOtherFolderMessageBox.Prompt = My.Resources.TheFileWithSameNameAs0AlreadyExistsInDatabaseInThe1.f(File, ImageInDBOther.Folder)
                            ImageInOtherFolderMessageBox.ShowDialogOn(Me)
                            If ImageInOtherFolderMessageBox.CheckBoxes(0).State = CheckState.Checked Then
                                Select Case ImageInOtherFolderMessageBox.DialogResult
                                    Case DialogResult.Ignore : RelocationMode = RelocateMode.Ignore
                                    Case DialogResult.OK : RelocationMode = RelocateMode.Relocate
                                End Select
                            End If
                            Select Case ImageInOtherFolderMessageBox.DialogResult
                                Case DialogResult.Ignore : NowRelocate = RelocateMode.Ignore
                                Case DialogResult.OK : NowRelocate = RelocateMode.Relocate
                            End Select
                        ElseIf ImageInDBOther IsNot Nothing Then
                            NowRelocate = RelocationMode
                        End If
                        If ImageInDBOther IsNot Nothing AndAlso NowRelocate = RelocateMode.Relocate Then
                            'Relocate if user wishes it
                            ImageInDBOther.Folder = FilePath
                            ImageInDBOther.FileName = FileName
                            dx.SubmitChanges()
                            ImageInDB = (From pic In dx.PictureMetadatas _
                                         Where pic.Folder.ToLower = FilePathL AndAlso pic.FileName.ToLower = FileNameL).FirstOrDefault
                        End If
                    End If
                    If ImageInDB IsNot Nothing AndAlso SynchronizeMode <> SynchronizeModes.Add Then
                        'Compare
                        'TODO: Implement
                        Throw New NotImplementedException
                    ElseIf ImageInDB Is Nothing AndAlso SynchronizeMode <> SynchronizeModes.Synchronize Then
                        'Insert new
                        Dim NewItem As New PictureMetadata()
                        NewItem.FileName = FileName
                        NewItem.Folder = FilePath
                        NewItem.x = BitmapSize.Width
                        NewItem.y = BitmapSize.Height
                        NewItem.LastSync = Now
                        If Exif.IFD0 IsNot Nothing AndAlso Exif.IFD0.Model IsNot Nothing Then NewItem.Model = Exif.IFD0.Model
                        If Exif.IFD0 IsNot Nothing AndAlso Exif.IFD0.ExifSubIFD IsNot Nothing AndAlso Exif.IFD0.ExifSubIFD.DateTimeDigitizedDate.HasValue Then NewItem.Digitized = Exif.IFD0.ExifSubIFD.DateTimeDigitizedDate
                        If IPTC.Contains(IptcT.DataSetIdentification.ObjectName) Then NewItem.ObjectName = IPTC.ObjectName
                        If IPTC.Contains(IptcT.DataSetIdentification.CaptionAbstract) Then NewItem.Text = IPTC.CaptionAbstract
                        If IPTC.Contains(IptcT.DataSetIdentification.City) Then NewItem.City = IPTC.City
                        If IPTC.Contains(IptcT.DataSetIdentification.CountryPrimaryLocationCode) Then NewItem.CountryCode = IPTC.CountryPrimaryLocationCode
                        If IPTC.Contains(IptcT.DataSetIdentification.CountryPrimaryLocationName) Then NewItem.Country = IPTC.CountryPrimaryLocationName
                        If IPTC.Contains(IptcT.DataSetIdentification.ProvinceState) Then NewItem.Province = IPTC.ProvinceState
                        If IPTC.Contains(IptcT.DataSetIdentification.SubLocation) Then NewItem.Sublocation = IPTC.SubLocation
                        If IPTC.Contains(IptcT.DataSetIdentification.Keywords) Then
                            NewItem.Keywords = IPTC.Keywords.Join(vbCrLf)
                        End If
                        If IPTC.Contains(IptcT.DataSetIdentification.CopyrightNotice) Then NewItem.Copyright = IPTC.CopyrightNotice
                        If IPTC.Contains(IptcT.DataSetIdentification.Credit) Then NewItem.Credit = IPTC.Credit
                        If IPTC.Contains(IptcT.DataSetIdentification.EditStatus) Then NewItem.EditStatus = IPTC.EditStatus
                        If IPTC.Contains(IptcT.DataSetIdentification.Urgency) Then NewItem.Urgence = IPTC.Urgency
                        dx.PictureMetadatas.InsertOnSubmit(NewItem)
                        dx.SubmitChanges()
                    End If
                End Using
                i += 1
                bgwSynchronize.ReportProgress(i / Files.Count * 100)
            Next
        End Sub
#Region "Options"
        ''' <summary>Ask modes enumeration</summary>
        Private Enum AskMode
            ''' <summary>Ask always</summary>
            Ask
            ''' <summary>Overwrite data in database</summary>
            OverwriteDatabase
            ''' <summary>Overwrite data in image file</summary>
            OverwriteFile
        End Enum
        ''' <summary>Relocate modes enumeration</summary>
        Private Enum RelocateMode
            ''' <summary>Ask always</summary>
            Ask
            ''' <summary>Ignore misslocated file</summary>
            Ignore
            ''' <summary>Change location in database</summary>
            Relocate
        End Enum

        ''' <summary>Synchronization modes enumeration</summary>
        Private Enum SynchronizeModes
            ''' <summary>Synchronize existing and add new</summary>
            SynchronizeAndAdd
            ''' <summary>Only synchronize existing</summary>
            Synchronize
            ''' <summary>Only add new</summary>
            Add
        End Enum
        ''' <summary>Overwrite mode when conflict between data in file and database</summary>
        Private Property SynchronizeMode() As SynchronizeModes
            Get
                Dim f = Function() If(optModeSynchronizeAdd.Checked, SynchronizeModes.SynchronizeAndAdd, If(optModeSynchronizeExisting.Checked, SynchronizeModes.Synchronize, SynchronizeModes.Add))
                If Me.InvokeRequired Then Return Me.Invoke(f) _
                Else Return f.Invoke
            End Get
            Set(ByVal value As SynchronizeModes)
                Set__SynchronizeMode(value)
            End Set
        End Property
        ''' <summary>Implements setter of the <see cref="SynchronizeMode"/> property</summary>
        Private Sub Set__SynchronizeMode(ByVal value As SynchronizeModes)
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Of Boolean)(AddressOf Set__SynchronizeMode), value)
            Else
                Select Case value
                    Case SynchronizeModes.SynchronizeAndAdd : optModeSynchronizeAdd.Checked = True
                    Case SynchronizeModes.Synchronize : optModeSynchronizeExisting.Checked = True
                    Case SynchronizeModes.Add : optModeAdd.Checked = True
                End Select
            End If
        End Sub
        ''' <summary>Overwrite mode when conflict between data in file and database</summary>
        Private Property OverwriteMode() As AskMode
            Get
                Dim f = Function() If(optAskOverwrite.Checked, AskMode.Ask, If(optOverwriteDatabase.Checked, AskMode.OverwriteDatabase, AskMode.OverwriteFile))
                If Me.InvokeRequired Then Return Me.Invoke(f) _
                Else Return f.Invoke
            End Get
            Set(ByVal value As AskMode)
                Set__OverwriteMode(value)
            End Set
        End Property
        ''' <summary>Implements setter of the <see cref="OverwriteMode"/> property</summary>
        Private Sub Set__OverwriteMode(ByVal value As AskMode)
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Of Boolean)(AddressOf Set__OverwriteMode), value)
            Else
                Select Case value
                    Case AskMode.Ask : optAskOverwrite.Checked = True
                    Case AskMode.OverwriteDatabase : optOverwriteDatabase.Checked = True
                    Case AskMode.OverwriteFile : optOverwriteFile.Checked = True
                End Select
            End If
        End Sub
        ''' <summary>Relocation mode of file that is in database with same name in different folder</summary>
        Private Property RelocationMode() As RelocateMode
            Get
                Dim f = Function() If(optRelocateAsk.Checked, RelocateMode.Ask, If(optRelocateIgnore.Checked, RelocateMode.Ignore, RelocateMode.Relocate))
                If Me.InvokeRequired Then Return Me.Invoke(f) _
                Else Return f.Invoke
            End Get
            Set(ByVal value As RelocateMode)
                Set__RelocationMode(value)
            End Set
        End Property
        ''' <summary>Implements setter of the <see cref="RelocationMode"/> property</summary>
        Private Sub Set__RelocationMode(ByVal value As RelocateMode)
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Of Boolean)(AddressOf Set__RelocationMode), value)
            Else
                Select Case value
                    Case RelocateMode.Ask : optRelocateAsk.Checked = True
                    Case RelocateMode.Ignore : optRelocateIgnore.Checked = True
                    Case RelocateMode.Relocate : optRelocate.Checked = True
                End Select
            End If
        End Sub
#End Region

        Private Sub optModeAdd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optModeAdd.CheckedChanged
            lblOnConflict.Enabled = Not optModeAdd.Checked
            flpOnConfilct.Enabled = Not optModeAdd.Checked
        End Sub

        Private Sub Wizard_StepEnter(ByVal sender As Object, ByVal e As WindowsT.FormsT.StepEventArgs) Handles _Wizard.StepEnter
            Wizard.NextButton.Enabled = False
        End Sub

        Private Sub bgwSynchronize_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwSynchronize.ProgressChanged
            If e.ProgressPercentage >= 0 AndAlso e.ProgressPercentage <= 100 Then
                prgProgress.Value = e.ProgressPercentage
            ElseIf e.ProgressPercentage > 100 Then
                prgProgress.Value = 100
            End If
            If TypeOf e.UserState Is String Then
                lblFileName.Text = e.UserState
            End If
        End Sub

        Private Sub bgwSynchronize_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwSynchronize.RunWorkerCompleted
            cmdCancel.Enabled = False
            Wizard.NextButton.Enabled = True
        End Sub
    End Class
End Namespace