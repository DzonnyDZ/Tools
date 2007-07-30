Imports Tools.DrawingT.MetadataT, Tools.WindowsT.FormsT.StatusMarker
Imports Tools.DrawingT.IO.JPEG
Imports Tools.DrawingT.MetadataT.IPTC
Imports Tools.VisualBasicT
''' <summary>Main form of Methanol application</summary>
Friend Class frmMain
    ''' <summary>reference to <see cref="frmLarge"/> that shows large image</summary>
    Private WithEvents frmLarge As frmLarge
    'Private AutoComplete As New Tools.CollectionsT.GenericT.ListWithEvents(Of String)
    'Private Synonyms As List(Of KeyValuePair(Of String(), String()))
    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        SaveChanged()
        If My.Settings.Folder IsNot Nothing AndAlso My.Settings.Folder <> "" Then
            Try
                fbdBrowse.SelectedPath = My.Settings.Folder
            Catch : End Try
        End If
        If fbdBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then
            My.Settings.Folder = fbdBrowse.SelectedPath
            LoadFolder()
            txtPath.Text = My.Settings.Folder
        End If
    End Sub
    ''' <summary>Defines if <see cref="bgwThumb_RunWorkerCompleted"/> should call <see cref="LoadFolder"/></summary>
    Private LoadOnEnd As Boolean = False
    ''' <summary>Loads and shows content of folder</summary>
    Private Sub LoadFolder()
        'If OldSelected.Count > 0 Then
        '    If SaveCurrent() Then
        '        OldSelected.Clear()
        '    Else
        '        Exit Sub
        '    End If
        'End If
        If bgwThumb.IsBusy Then
            bgwThumb.CancelAsync()
            LoadOnEnd = True
            Me.Cursor = Cursors.WaitCursor
            Me.Enabled = False
            Exit Sub
        End If
        tsgLoadImages.Value = 0
        tsgLoadImages.Visible = True
        lvwFolder.Items.Clear()
        lvwImages.Items.Clear()
        Dim IW As Image = imlImages.Images("IrfanView")
        imlImages.Images.Clear()
        imlImages.Images.Add("IrfanView", IW)
        If IO.Path.GetDirectoryName(My.Settings.Folder) <> "" Then
            lvwFolder.Items.Add(IO.Path.GetDirectoryName(My.Settings.Folder), "...", "Up")
        End If
        Try
            Dim Images As New List(Of String)
            For Each Folder As String In IO.Directory.GetDirectories(My.Settings.Folder)
                lvwFolder.Items.Add(Folder, IO.Path.GetFileName(Folder), "Folder")
            Next Folder
            For Each file As String In IO.Directory.GetFiles(My.Settings.Folder, "*.jpg")
                lvwImages.Items.Add(file, IO.Path.GetFileName(file), "IrfanView")
                Images.Add(file)
            Next file
            For Each file As String In IO.Directory.GetFiles(My.Settings.Folder, "*.jpeg")
                lvwImages.Items.Add(file, IO.Path.GetFileName(file), "IrfanView")
                Images.Add(file)
            Next file
            bgwThumb.RunWorkerAsync(Images)
        Catch ex As Exception
            MsgBox("Cannot load folder content", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub bgwThumb_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwThumb.DoWork
        Dim d As New dSetThumb(AddressOf SetThumb)
        Dim ThSize As Size = imlImages.ImageSize
        Dim i As Integer = 0
        Threading.Thread.CurrentThread.Priority = Threading.ThreadPriority.BelowNormal
        For Each Path As String In DirectCast(e.Argument, List(Of String))
            bgwThumb.ReportProgress(i / DirectCast(e.Argument, List(Of String)).Count * 100)
            Dim Img As Bitmap
            Try
                Img = New Bitmap(Path)
            Catch
                Continue For
            End Try
            Dim NewSize As Size = ThumbSize(ThSize, Img.Size)
            Dim th As Image = Img.GetThumbnailImage(NewSize.Width, NewSize.Height, AddressOf CancelThumb, IntPtr.Zero)
            Dim NewImage As New Bitmap(ThSize.Width, ThSize.Height)
            Dim g As Graphics = Graphics.FromImage(NewImage)
            g.DrawImage(th, CSng(ThSize.Width / 2 - NewSize.Width / 2), CSng(ThSize.Height / 2 - NewSize.Height / 2), NewSize.Width, NewSize.Height)
            g.Flush()
            If th IsNot Nothing AndAlso Not bgwThumb.CancellationPending Then
                Me.Invoke(d, Path, NewImage)
            End If
            If bgwThumb.CancellationPending Then
                e.Cancel = True
                Exit Sub
            End If
            i += 1
        Next Path
        bgwThumb.ReportProgress(100)
    End Sub
    ''' <summary>Used by thumbnail generator to stop it's work when <see cref="bgwThumb">bgwThumb</see>.<see cref="System.ComponentModel.BackgroundWorker.CancellationPending">CancellationPending</see> is true</summary>
    ''' <returns><see cref="bgwThumb">bgwThumb</see>.<see cref="System.ComponentModel.BackgroundWorker.CancellationPending">CancellationPending</see></returns>
    Private Function CancelThumb() As Boolean
        Return bgwThumb.CancellationPending
    End Function
    ''' <summary>Delegate of <see cref="SetThumb"/> method</summary>
    ''' <param name="Image">Thumbnail to be shown</param>
    ''' <param name="Path">Path of image <paramref name="Image"/> is thumbnail of</param>
    Private Delegate Sub dSetThumb(ByVal Path As String, ByVal Image As Image)
    ''' <summary>Assigns thumbnail image to item of <see cref="lvwImages"/></summary>
    ''' <param name="Image">Thumbnail to be shown</param>
    ''' <param name="Path">Path of image <paramref name="Image"/> is thumbnail of</param>
    Private Sub SetThumb(ByVal Path As String, ByVal Image As Image)
        imlImages.Images.Add(Path, Image)
        lvwImages.Items(Path).ImageKey = Path
    End Sub
    'TODO:Extract as tool
    ''' <summary>Computes the best-fit size of thumbnail image</summary>
    ''' <param name="Th">Maximal bounds of thumbnail</param>
    ''' <param name="Img">Size of image to get thumbnail size of</param>
    ''' <returns>The maximal size of thumbnail computed in such way that width/haight ration of <paramref name="Img"/> is kept and returned size fits into <paramref name="Th"/>. At least one dimesion (height or width) of size returned is the same as appropriate dimension of <paramref name="Th"/></returns>
    Private Shared Function ThumbSize(ByVal Th As Size, ByVal Img As Size) As Size
        Dim NewS As Size
        If Img.Width <= Th.Width AndAlso Img.Height <= Th.Width Then
            Return Img
        ElseIf Img.Height > Th.Height Then
            NewS = New Size(Th.Width, Img.Height / (Img.Width / Th.Width))
        Else
            NewS = New Size(Img.Width / (Img.Height / Th.Height), Th.Height)
        End If
        If NewS.Width > Th.Width Then
            Return New Size(Th.Width, NewS.Height / (NewS.Width / Th.Width))
        ElseIf NewS.Height > Img.Height Then
            Return New Size(NewS.Width / (NewS.Height / Th.Height), Th.Height)
        End If
        Return NewS
    End Function
    Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
        SaveChanged()
        My.Settings.Folder = txtPath.Text
        LoadFolder()
    End Sub

    Private Sub txtPath_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPath.KeyDown
        If e.KeyCode = Keys.Return Then
            cmdGo.PerformClick()
        End If
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' SaveCurrent()
        If MsgBox("Close?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Close?") <> MsgBoxResult.Yes Then
            e.Cancel = True
            Exit Sub
        End If
        StoreSettings()
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If My.Settings.Folder <> "" Then
            LoadFolder()
        End If
        LoadSettings()
        txtPath.Text = My.Settings.Folder
    End Sub


    Private Sub lvwFolder_ItemActivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwFolder.ItemActivate
        SaveChanged()
        My.Settings.Folder = lvwFolder.SelectedItems(0).Name
        LoadFolder()
    End Sub
    ''' <summary>Applies <see cref="My.Settings"/> onto this form and its controls</summary>
    Private Sub LoadSettings()
        kweKeyWords.Synonyms = My.Settings.Synonyms
        kweKeyWords.AutoCompleteStable = My.Settings.AutoComplete
        'Me.Synonyms = My.Settings.KwSynonyms
        Me.Location = My.Settings.MainLocation
        Me.Size = My.Settings.MainSize
        Me.WindowState = My.Settings.MainState
        If Me.WindowState = FormWindowState.Normal Then
            Dim Intersects As Boolean = False
            For Each scr As Screen In Screen.AllScreens
                If scr.WorkingArea.IntersectsWith(Me.DesktopBounds) Then
                    Intersects = True
                    Exit For
                End If
            Next scr
            If Not Intersects Then Me.DesktopLocation = My.Computer.Screen.Bounds.Location
        End If
        splMain.SplitterDistance = My.Settings.MainSplitter
        splFolder.SplitterDistance = My.Settings.LeftSplitter
        splVertical.SplitterDistance = My.Settings.VerticalSplitter
        splHorizontal.SplitterDistance = My.Settings.HorizontalSplitter
        If My.Settings.LargeShown Then
            cmdLarge.Visible = False
            frmLarge = New frmLarge
            frmLarge.Show()
            frmLarge.BackgroundImage = picPreview.Image
        End If
    End Sub
    ''' <summary>Writes current state of this form and its controls into <see cref="My.Settings"/></summary>
    Private Sub StoreSettings()
        My.Settings.MainLocation = Me.Location
        My.Settings.MainSize = Me.Size
        My.Settings.MainState = Me.WindowState
        My.Settings.MainSplitter = splMain.SplitterDistance
        My.Settings.LeftSplitter = splFolder.SplitterDistance
        My.Settings.HorizontalSplitter = splHorizontal.SplitterDistance
        My.Settings.VerticalSplitter = splVertical.SplitterDistance
        My.Settings.LargeShown = frmLarge IsNot Nothing
        If frmLarge IsNot Nothing Then
            My.Settings.LargeLocation = frmLarge.Location
            My.Settings.LargeSize = frmLarge.Size
            My.Settings.LargeState = frmLarge.WindowState
        End If
        'My.Settings.KwAutoComplete = AutoComplete '.ToArray
    End Sub

    Private Sub bgwThumb_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwThumb.RunWorkerCompleted
        If LoadOnEnd Then
            LoadOnEnd = False
            Me.Enabled = True
            Me.Cursor = Cursors.Default
            LoadFolder()
        Else
            tsgLoadImages.Visible = False
        End If
    End Sub

    Private Sub bgwThumb_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwThumb.ProgressChanged
        tsgLoadImages.Value = e.ProgressPercentage
    End Sub

    'Private Sub [SelectIndices](ByVal Indices As IEnumerable(Of Integer))
    '    lvwImages.SelectedIndices.Clear()
    '    For Each sel As Integer In Indices
    '        lvwImages.SelectedIndices.Add(sel)
    '    Next sel
    'End Sub

    'Private SuppressSelectedIndexChanged As Boolean = False
    'Private Sub lvwImages_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwImages.SelectedIndexChanged
    '    If SuppressSelectedIndexChanged Then Exit Sub
    '    SuppressSelectedIndexChanged = True
    '    Try
    '        If OldSelected.Count > 0 Then
    '            Dim NowSelected As New List(Of Integer)(New Tools.CollectionsT.GenericT.Wrapper(Of Integer)(lvwImages.SelectedIndices))
    '            [SelectIndices](OldSelected)
    '            If Not SaveCurrent() Then
    '                Exit Sub
    '            Else
    '                [SelectIndices](NowSelected)
    '            End If
    '        End If
    '    Finally
    '        SuppressSelectedIndexChanged = False
    '    End Try
    '    picPreview.CancelAsync()
    '    If frmLarge IsNot Nothing Then frmLarge.BackgroundImage = Nothing
    '    If lvwImages.SelectedItems.Count > 0 Then
    '        Try
    '            picPreview.LoadAsync(lvwImages.SelectedItems(0).Name)
    '        Catch
    '            picPreview.Image = Nothing
    '        End Try
    '        If frmLarge IsNot Nothing Then frmLarge.Text = "Metanol large " & IO.Path.GetFileName(lvwImages.SelectedItems(0).Name)
    '    Else
    '        picPreview.Image = Nothing
    '        If frmLarge IsNot Nothing Then frmLarge.Text = "Metanol large"
    '    End If
    '    If lvwImages.SelectedItems.Count = 1 Then
    '        Me.Text = "Metanol " & IO.Path.GetFileName(lvwImages.SelectedItems(0).Name)
    '    Else
    '        Me.Text = "Metanol"
    '    End If
    '    OldSelected.Clear()
    '    OldSelected.AddRange(New Tools.CollectionsT.GenericT.Wrapper(Of Integer)(lvwImages.SelectedIndices))
    '    LoadCurrent()
    'End Sub

    'Private OldSelected As New List(Of Integer)

    Private Sub frmLarge_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles frmLarge.FormClosed
        frmLarge = Nothing
        cmdLarge.Visible = True
    End Sub

    Private Sub cmdLarge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLarge.Click
        cmdLarge.Visible = False
        frmLarge = New frmLarge
        frmLarge.Show()
        frmLarge.BackgroundImage = picPreview.Image
        If lvwImages.SelectedItems.Count > 0 Then
            frmLarge.Text = "Metanol large " & IO.Path.GetFileName(lvwImages.SelectedItems(0).Name)
        End If
    End Sub

    Private Sub picPreview_LoadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles picPreview.LoadCompleted
        If frmLarge IsNot Nothing Then frmLarge.BackgroundImage = picPreview.Image
    End Sub

    'Private Sub tabChoices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabChoices.SelectedIndexChanged
    '    SaveCurrent(Tools.VisualBasicT.iif(tabChoices.SelectedTab Is tapCommon, tapAll, tapCommon))
    '    kweKeyWords.Enabled = tabChoices.SelectedTab Is tapCommon
    '    LoadCurrent()
    'End Sub

    'Private Function SaveCurrent(Optional ByVal Tab As TabPage = Nothing) As Boolean
    '    If Tab Is Nothing Then Tab = tabChoices.SelectedTab
    '    If Tab Is tapCommon Then
    '        Return SaveCommon()
    '    Else
    '        SaveAll()
    '        Return True
    '    End If
    'End Function

    'Private Sub LoadCurrent(Optional ByVal Tab As TabPage = Nothing)
    '    If Tab Is Nothing Then Tab = tabChoices.SelectedTab
    '    IPTC = Nothing
    '    Try
    '        If Tab Is tapCommon Then
    '            LoadCommon()
    '        Else
    '            LoadAll()
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
    '    End Try
    '    EnableDisable()
    'End Sub

    'Private Sub LoadAll()
    '    LoadIPTC()
    '    prgAll.SelectedObject = IPTC
    'End Sub
    ''' <summary>Loads so-called common properties into editing controls</summary>
    ''' <param name="IPTC"><see cref="IPTC"/> to obtain values from</param>
    Private Sub LoadCommon(ByVal IPTC As IPTC)
        'LoadIPTC()
        'If IPTC Is Nothing Then Exit Sub
        With txwCopyright
            If 0 >= IPTC.Contains(DataSetIdentification.CopyrightNotice) Then
                .Text = ""
                .Status.Status = Statuses.Null
            Else
                Try
                    .Text = IPTC.CopyrightNotice
                    .Status.Status = Statuses.Normal
                Catch : .Text = "" : .Status.Status = Statuses.Error : End Try
            End If
        End With
        With txwCredit
            If 0 >= IPTC.Contains(DataSetIdentification.Credit) Then
                .Text = ""
                .Status.Status = Statuses.Null
            Else
                Try
                    .Text = IPTC.Credit
                    .Status.Status = Statuses.Normal
                Catch : .Text = "" : .Status.Status = Statuses.Error : End Try
            End If
        End With
        With txwCity
            If 0 >= IPTC.Contains(DataSetIdentification.City) Then
                .Text = ""
                .Status.Status = Statuses.Null
            Else
                Try
                    .Text = IPTC.City
                    .Status.Status = Statuses.Normal
                Catch : .Text = "" : .Status.Status = Statuses.Error : End Try
            End If
        End With
        With cbwCountryCode
            If 0 >= IPTC.Contains(DataSetIdentification.CountryPrimaryLocationCode) Then
                .Text = ""
                .Status.Status = Statuses.Null
            Else
                Try
                    .Text = IPTC.CountryPrimaryLocationCode
                    .Status.Status = Statuses.Normal
                Catch : .Text = "" : .Status.Status = Statuses.Error : End Try
            End If
        End With
        With txwCountry
            If 0 >= IPTC.Contains(DataSetIdentification.CountryPrimaryLocationName) Then
                .Text = ""
                .Status.Status = Statuses.Null
            Else
                Try
                    .Text = IPTC.CountryPrimaryLocationName
                    .Status.Status = Statuses.Normal
                Catch : .Text = "" : .Status.Status = Statuses.Error : End Try
            End If
        End With
        With txwProvince
            If 0 >= IPTC.Contains(DataSetIdentification.ProvinceState) Then
                .Text = ""
                .Status.Status = Statuses.Null
            Else
                Try
                    .Text = IPTC.ProvinceState
                    .Status.Status = Statuses.Normal
                Catch : .Text = "" : .Status.Status = Statuses.Error : End Try
            End If
        End With
        With txwSubLocation
            If 0 >= IPTC.Contains(DataSetIdentification.SubLocation) Then
                .Text = ""
                .Status.Status = Statuses.Null
            Else
                Try
                    .Text = IPTC.SubLocation
                    .Status.Status = Statuses.Normal
                Catch : .Text = "" : .Status.Status = Statuses.Error : End Try
            End If
        End With
        With txwEditStatus
            If 0 >= IPTC.Contains(DataSetIdentification.EditStatus) Then
                .Text = ""
                .Status.Status = Statuses.Null
            Else
                Try
                    .Text = IPTC.EditStatus
                    .Status.Status = Statuses.Normal
                Catch : .Text = "" : .Status.Status = Statuses.Error : End Try
            End If
        End With
        With nwsUrgency
            If 0 >= IPTC.Contains(DataSetIdentification.Urgency) Then
                .Value = 0
                .Status.Status = Statuses.Null
            Else
                Try
                    .Value = IPTC.Urgency
                    .Status.Status = Statuses.Normal
                Catch : .Value = 0 : .Status.Status = Statuses.Error : End Try
            End If
        End With
        With txwObjectName
            If 0 >= IPTC.Contains(DataSetIdentification.ObjectName) Then
                .Text = ""
                .Status.Status = Statuses.Null
            Else
                Try
                    .Text = IPTC.ObjectName
                    .Status.Status = Statuses.Normal
                Catch : .Text = "" : .Status.Status = Statuses.Error : End Try
            End If
        End With
        With mxwCaptionAbstract
            If 0 >= IPTC.Contains(DataSetIdentification.CaptionAbstract) Then
                .Text = ""
                .Status.Status = Statuses.Null
            Else
                Try
                    .Text = IPTC.CaptionAbstract
                    .Status.Status = Statuses.Normal
                Catch : .Text = "" : .Status.Status = Statuses.Error : End Try
            End If
        End With
        With kweKeyWords
            .KeyWords.Clear()
            If 0 >= IPTC.Contains(DataSetIdentification.Keywords) Then
                .Status.Status = Statuses.Null
            Else
                .Status.Status = Statuses.Normal
                Try
                    For Each Kw As String In IPTC.Keywords
                        .KeyWords.Add(Kw)
                    Next Kw
                Catch : End Try
            End If
        End With
    End Sub
    'Private Sub SaveAll()
    '    For Each Item As ListViewItem In lvwImages.SelectedItems
    '        Try
    '            Using MyJPEG As New Tools.DrawingT.IO.JPEG.JPEGReader(Item.Name, True)
    '                MyJPEG.IPTCEmbed(IPTC.GetBytes)
    '            End Using
    '        Catch ex As Exception
    '            MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name & " " & IO.Path.GetFileName(Item.Name))
    '        End Try
    '    Next Item
    'End Sub
    ''' <summary>Writes so-called common settings from controls back to <see cref="IPTC"/></summary>
    Private Sub SaveCommon()
        'Try
        '  For Each item As ListViewItem In lvwImages.SelectedItems
        '    Dim MyIPTC As IPTC
        '    Dim MyJPEG As JPEGReader
        '    Try
        '        MyJPEG = New JPEGReader(item.Name, True)
        '        MyIPTC = New IPTC(MyJPEG)
        '    Catch ex As Exception
        '        MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name & " " & IO.Path.GetFileName(item.Name))
        '        Continue For
        '    End Try
        ' Try
        Try
            With txwCopyright
                Try
                    For Each item As ListViewItem In lvwImages.SelectedItems
                        If .Status.Status = Statuses.Changed OrElse .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.[New] Then
                            DirectCast(item.Tag, Cont).CopyrightNotice = .Text
                        ElseIf .Status.Status = Statuses.Deleted Then
                            DirectCast(item.Tag, Cont).Clear(DataSetIdentification.CopyrightNotice)
                        End If
                    Next item
                    .Status.Status = Statuses.Normal
                Catch ex As Exception : MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name) : End Try
            End With
            With txwCredit
                Try
                    For Each item As ListViewItem In lvwImages.SelectedItems
                        If .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Changed OrElse .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.[New] Then
                            DirectCast(item.Tag, Cont).Credit = .Text
                        ElseIf .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Deleted Then
                            DirectCast(item.Tag, Cont).Clear(IPTC.DataSetIdentification.Credit)
                        End If
                    Next item
                    .Status.Status = Statuses.Normal
                Catch ex As Exception : MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name) : End Try
            End With
            With txwCity
                Try
                    For Each item As ListViewItem In lvwImages.SelectedItems
                        If .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Changed OrElse .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.[New] Then
                            DirectCast(item.Tag, Cont).City = .Text
                        ElseIf .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Deleted Then
                            DirectCast(item.Tag, Cont).Clear(Tools.DrawingT.MetadataT.IPTC.DataSetIdentification.City)
                        End If
                    Next item
                    .Status.Status = Statuses.Normal
                Catch ex As Exception : MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name) : End Try
            End With
            With cbwCountryCode
                Try
                    For Each item As ListViewItem In lvwImages.SelectedItems
                        If .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Changed OrElse .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.[New] Then
                            DirectCast(item.Tag, Cont).CountryPrimaryLocationCode = .Text
                        ElseIf .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Deleted Then
                            DirectCast(item.Tag, Cont).Clear(Tools.DrawingT.MetadataT.IPTC.DataSetIdentification.CountryPrimaryLocationCode)
                        End If
                    Next item
                    .Status.Status = Statuses.Normal
                Catch ex As Exception : MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name) : End Try
            End With
            With txwCountry
                Try
                    For Each item As ListViewItem In lvwImages.SelectedItems
                        If .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Changed OrElse .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.[New] Then
                            DirectCast(item.Tag, Cont).CountryPrimaryLocationName = .Text
                        ElseIf .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Deleted Then
                            DirectCast(item.Tag, Cont).Clear(Tools.DrawingT.MetadataT.IPTC.DataSetIdentification.CountryPrimaryLocationName)
                        End If
                    Next item
                    .Status.Status = Statuses.Normal
                Catch ex As Exception : MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name) : End Try
            End With
            With txwProvince
                Try
                    For Each item As ListViewItem In lvwImages.SelectedItems
                        If .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Changed OrElse .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.[New] Then
                            DirectCast(item.Tag, Cont).ProvinceState = .Text
                        ElseIf .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Deleted Then
                            DirectCast(item.Tag, Cont).Clear(Tools.DrawingT.MetadataT.IPTC.DataSetIdentification.ProvinceState)
                        End If
                    Next item
                    .Status.Status = Statuses.Normal
                Catch ex As Exception : MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name) : End Try
            End With
            With txwSubLocation
                Try
                    For Each item As ListViewItem In lvwImages.SelectedItems
                        If .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Changed OrElse .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.[New] Then
                            DirectCast(item.Tag, Cont).SubLocation = .Text
                        ElseIf .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Deleted Then
                            DirectCast(item.Tag, Cont).Clear(Tools.DrawingT.MetadataT.IPTC.DataSetIdentification.SubLocation)
                        End If
                    Next item
                    .Status.Status = Statuses.Normal
                Catch ex As Exception : MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name) : End Try
            End With
            With txwEditStatus
                Try
                    For Each item As ListViewItem In lvwImages.SelectedItems
                        If .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Changed OrElse .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.[New] Then
                            DirectCast(item.Tag, Cont).EditStatus = .Text
                        ElseIf .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Deleted Then
                            DirectCast(item.Tag, Cont).Clear(Tools.DrawingT.MetadataT.IPTC.DataSetIdentification.EditStatus)
                        End If
                    Next item
                    .Status.Status = Statuses.Normal
                Catch ex As Exception : MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name) : End Try
            End With
            With nwsUrgency
                Try
                    For Each item As ListViewItem In lvwImages.SelectedItems
                        If .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Changed OrElse .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.[New] Then
                            DirectCast(item.Tag, Cont).Urgency = .Value
                        ElseIf .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Deleted Then
                            DirectCast(item.Tag, Cont).Clear(Tools.DrawingT.MetadataT.IPTC.DataSetIdentification.Urgency)
                        End If
                    Next item
                    .Status.Status = Statuses.Normal
                Catch ex As Exception : MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name) : End Try
            End With
            With txwObjectName
                Try
                    For Each item As ListViewItem In lvwImages.SelectedItems
                        If .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Changed OrElse .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.[New] Then
                            DirectCast(item.Tag, Cont).ObjectName = .Text
                        ElseIf .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Deleted Then
                            DirectCast(item.Tag, Cont).Clear(Tools.DrawingT.MetadataT.IPTC.DataSetIdentification.ObjectName)
                        End If
                    Next item
                    .Status.Status = Statuses.Normal
                Catch ex As Exception : MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name) : End Try
            End With
            With mxwCaptionAbstract
                Try
                    For Each item As ListViewItem In lvwImages.SelectedItems
                        If .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Changed OrElse .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.[New] Then
                            DirectCast(item.Tag, Cont).CaptionAbstract = .Text
                        ElseIf .Status.Status = Tools.WindowsT.FormsT.StatusMarker.Statuses.Deleted Then
                            DirectCast(item.Tag, Cont).Clear(Tools.DrawingT.MetadataT.IPTC.DataSetIdentification.CaptionAbstract)
                        End If
                    Next item
                    .Status.Status = Statuses.Normal
                Catch ex As Exception : MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name) : End Try
            End With
            If kweKeyWords.Status.Status = Statuses.Changed OrElse kweKeyWords.Status.Status = Statuses.[New] Then
                Try
                    For Each item As ListViewItem In lvwImages.SelectedItems
                        Dim CurrentKw As List(Of String)
                        If Not kweKeyWords.Merge OrElse lvwImages.SelectedItems.Count = 1 Then 'lvwImages.SelectedItems.Count = 1 OrElse Not kweKeyWords.Merge Then
                            CurrentKw = New List(Of String)
                        Else
                            Dim kw As String() = DirectCast(item.Tag, Cont).Keywords
                            If kw Is Nothing Then
                                CurrentKw = New List(Of String)
                            Else
                                CurrentKw = New List(Of String)(kw)
                            End If
                        End If
                        For Each kw As String In kweKeyWords.KeyWords
                            Dim IsIn As Boolean = False

                            For Each InKw As String In CurrentKw
                                If LCase(InKw) = LCase(kw) Then IsIn = True : Exit For
                            Next InKw
                            If Not IsIn Then CurrentKw.Add(kw)
                        Next kw
                        DirectCast(item.Tag, Cont).Keywords = CurrentKw.ToArray
                    Next item
                    kweKeyWords.Status.Status = Statuses.Normal
                Catch ex As Exception : MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name) : End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            'Return False
        End Try
        'Try
        '    MyJPEG.IPTCEmbed(Mydirectcast(item.tag,cont).GetBytes)
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name & " " & IO.Path.GetFileName(item.Name))
        'End Try
        'Finally
        '    MyJPEG.Dispose()
        'End Try
        'Next item
        'Return True
    End Sub

    ''' <summary>Stores changes of changed <see cref="IPTC"/>'s into files</summary>
    Private Sub SaveChanged()
        For Each item As ListViewItem In lvwImages.SelectedItems
            With DirectCast(item.Tag, Cont)
                If .Changed Then
                    Try
                        Using Reader As New JPEGReader(item.Name, True)
                            Reader.IPTCEmbed(.GetBytes)
                        End Using
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                    End Try
                End If
            End With
            item.Tag = Nothing
        Next item
        tslChange.Text = ""
        lvwImages_SelectedIndexChanged(Me, EventArgs.Empty)
    End Sub

    'Private Sub LoadIPTC()
    '    If lvwImages.SelectedItems.Count > 0 Then
    '        Using JPEg As New Tools.DrawingT.IO.JPEG.JPEGReader(lvwImages.SelectedItems(0).Name)
    '            IPTC = New Tools.DrawingT.MetadataT.IPTC(JPEg)
    '        End Using
    '    Else
    '        IPTC = Nothing
    '    End If
    'End Sub

    'Private IPTC As Tools.DrawingT.MetadataT.IPTC
    ''' <summary>Enables or disables controls depending on if image is selected</summary>
    Private Sub EnableDisable()
        kweKeyWords.Enabled = lvwImages.SelectedItems.Count > 0 AndAlso tabChoices.SelectedTab Is tapCommon
        tabChoices.Enabled = lvwImages.SelectedItems.Count > 0
    End Sub

    Private Sub frmMain_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Right Then
            If lvwImages.SelectedItems.Count = 0 AndAlso lvwImages.Items.Count > 0 Then
                lvwImages.SelectedIndices.Add(0)
            ElseIf lvwImages.SelectedItems.Count > 0 AndAlso lvwImages.SelectedIndices(0) < lvwImages.Items.Count - 1 Then
                Dim Index As Integer = lvwImages.SelectedIndices(0)
                lvwImages.SelectedItems.Clear()
                lvwImages.SelectedIndices.Add(Index + 1)
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.Left Then
            If lvwImages.SelectedItems.Count = 0 AndAlso lvwImages.Items.Count > 0 Then
                lvwImages.SelectedIndices.Add(lvwImages.Items.Count - 1)
            ElseIf lvwImages.SelectedIndices.Count > 0 AndAlso lvwImages.SelectedIndices(0) > 0 Then
                Dim Index As Integer = lvwImages.SelectedIndices(0)
                lvwImages.SelectedItems.Clear()
                lvwImages.SelectedIndices.Add(Index - 1)
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            SaveChanged()
            MsgBox("Saved.", MsgBoxStyle.Information, "Saved")
        End If
    End Sub

    Private Sub lvwImages_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwImages.SelectedIndexChanged
        For Each item As ListViewItem In lvwImages.Items
            If item.Selected Then item.StateImageIndex = 1 Else item.StateImageIndex = 0
        Next item
        Dim Remove As New List(Of ListViewItem)
        For Each item As ListViewItem In lvwImages.SelectedItems
            If item.Tag Is Nothing Then
                Try
                    Using Reader As New JPEGReader(item.Name)
                        item.Tag = New Cont(Reader, AddressOf Ch)
                    End Using
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, IO.Path.GetFileName(item.Name) & " " & ex.GetType.Name)
                    Remove.Add(item)
                End Try
            End If
        Next item
        For Each item As ListViewItem In Remove
            item.Selected = False
        Next item
        If lvwImages.SelectedItems.Count > 0 Then
            LoadCommon(DirectCast(lvwImages.SelectedItems(0).Tag, Cont))
        End If
        If tabChoices.SelectedTab Is tapAll Then
            Dim Objects(lvwImages.SelectedItems.Count - 1) As Object
            Dim i As Integer = 0
            For Each item As ListViewItem In lvwImages.SelectedItems
                Objects(i) = DirectCast(item.Tag, Cont)
                i += 1
            Next item
            prgAll.SelectedObjects = Objects
        End If
        EnableDisable()

        If lvwImages.SelectedItems.Count > 0 Then
            If lvwImages.SelectedItems.Count = 1 Then
                Me.Text = "Metanol " & lvwImages.SelectedItems(0).Name
            Else
                Me.Text = "Metanol (multiple)"
            End If
            Try
                picPreview.LoadAsync(lvwImages.SelectedItems(0).Name)
            Catch
                picPreview.Image = Nothing
            End Try
            If frmLarge IsNot Nothing Then frmLarge.Text = "Metanol large " & IO.Path.GetFileName(lvwImages.SelectedItems(0).Name)
        Else
            Me.Text = "Metanol"
            picPreview.Image = Nothing
            If frmLarge IsNot Nothing Then frmLarge.Text = "Metanol large"
        End If
    End Sub

    'Private Sub prgAll_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles prgAll.PropertyValueChanged
    '    For Each item As Cont In prgAll.SelectedObjects
    '        item.Changed = True
    '    Next item
    'End Sub

    Private Sub tabChoices_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabChoices.SelectedIndexChanged
        If tabChoices.SelectedTab IsNot tapCommon Then
            SaveCommon()
        End If
        lvwImages_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub Edit_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles txwSubLocation.Leave, txwProvince.Leave, txwObjectName.Leave, txwEditStatus.Leave, txwCredit.Leave, txwCountry.Leave, txwCopyright.Leave, txwCity.Leave, nwsUrgency.Leave, mxwCaptionAbstract.Leave, cbwCountryCode.Leave, kweKeyWords.Leave
        SaveCommon()
    End Sub
    ''' <summary>Graphically notifies user that there are some unsaved changes</summary>
    Private Sub Ch()
        tslChange.Text = "*"
    End Sub

    Private Sub tmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiExport.Click
        If lvwImages.SelectedItems.Count < 1 Then
            MsgBox("No images selected")
            Exit Sub
        End If
        If sfdExport.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim ret As New System.Text.StringBuilder
            Dim i As Integer = 0
            ret.AppendLine(CreateCSV( _
                "Path", "File name", "Size", "Changed", "Created", _
                "Exif Date", "Make", "Model", _
                "Width", "Height", "Horiz. res.", "Vert. res.", _
                "Object Name", "Credit", "Copyright Notice", "Caption/Abstract", "County Code", "Country", "Province/State", "City", "Sublocation", "Digital Creation Time", "Digital Creation Date", "Digital Creation Time", "Urgency" _
            ))
            Try
                For Each item As ListViewItem In lvwImages.SelectedItems
                    Dim IPTC As Cont
                    If item.Tag Is Nothing Then
                        item.Tag = New Cont(New JPEGReader(item.Name), AddressOf Ch)
                    End If
                    IPTC = item.Tag
                    Dim Exif As New Exif(New ExifReader(New JPEGReader(item.Name)))
                    Dim System As New Tools.IOt.Path(item.Name)
                    Dim Img As New Bitmap(item.Name)
                    With System
                        ret.Append(CreateCSV( _
                            .Path, .FileName, .GetFile.Length, .GetFile.LastWriteTime.ToString("G"), .GetFile.CreationTime.ToString("G")))
                    End With
                    With Exif
                        ret.Append(";" & CreateCSV( _
                            .ExifSubIFD.DateTimeDigitized, .MainIFD.Make, .MainIFD.Model))
                    End With
                    With Img
                        ret.Append(";" & CreateCSV( _
                            .Width, .Height, .HorizontalResolution, .VerticalResolution))
                    End With
                    With IPTC
                        ret.Append(";" & CreateCSV( _
                            .ObjectName, .Credit, .CopyrightNotice, .CaptionAbstract, .CountryPrimaryLocationCode, .CountryPrimaryLocationName, .ProvinceState, .City, .SubLocation, .DigitalCreationTime.Time.ToString & "+" & .DigitalCreationTime.Offset.ToString, .DigitalCreationDate.ToString("d"), .Urgency))
                    End With
                    ret.Append(vbCrLf)
                    i += 1
                    Debug.Print(i)
                Next item
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Exit Sub
            End Try
            Try
                My.Computer.FileSystem.WriteAllText(sfdExport.FileName, ret.ToString, False)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Do While sfdExport.ShowDialog = Windows.Forms.DialogResult.OK
                    Try
                        My.Computer.FileSystem.WriteAllText(sfdExport.FileName, ret.ToString, False)
                        Exit Do
                    Catch ex2 As Exception
                        MsgBox(ex2.Message, MsgBoxStyle.Critical, ex2.GetType.Name)
                    End Try
                Loop
            End Try
        End If
    End Sub
    Private Function CreateCSV(ByVal ParamArray Values As String()) As String
        Dim b As New System.Text.StringBuilder
        For Each value As String In Values
            If b.Length > 0 Then b.Append(";"c)
            If value Is Nothing Then value = ""
            b.Append("""" & value.Replace("""", """""").Replace("\", "\\").Replace(vbCr, "\r").Replace(vbLf, "\n") & """")
        Next value
        Return b.ToString
    End Function
End Class

''' <summary>Delegate that notifies change</summary>
Friend Delegate Sub dChange()
''' <summary>Inherits from <see cref="IPTC"/> in order to notify owner of changes in it</summary>
Friend Class Cont : Inherits IPTC
    ''' <summary>True if this instance was changed since its load fromk filer</summary>
    Public Changed As Boolean
    ''' <summary>Delegate to be notified when change occures</summary>
    Private Changes As dChange
    ''' <summary>CTor</summary>
    ''' <param name="Reader"><see cref="IIPTCGetter"/> that contains IPTC stream</param>
    ''' <param name="Ch">Delegate to be notified when change occures</param>
    Public Sub New(ByVal Reader As JPEGReader, ByVal Ch As dChange)
        MyBase.New(Reader)
        Me.Changes = Ch
    End Sub
    ''' <summary>Called when value of any tag changes</summary>
    ''' <param name="Tag">Recod and dataset number</param>
    Protected Overrides Sub OnValueChanged(ByVal Tag As Tools.DrawingT.MetadataT.IPTC.DataSetIdentification)
        MyBase.OnValueChanged(Tag)
        Changed = True
        Changes.Invoke()
    End Sub
    ''' <summary>Removes all occurences of specified tag</summary>         
    ''' <param name="Key">Tag to remove</param>                            
    Public Overrides Sub Clear(ByVal Key As Tools.DrawingT.MetadataT.IPTC.DataSetIdentification)
        MyBase.Clear(Key)
        Changed = True
        Changes.Invoke()
    End Sub
    ''' <summary>Converts <see cref="ListViewItem"/> into <see cref="Cont"/> by returning <see cref="ListViewItem.Tag"/></summary>
    ''' <param name="from"><see cref="ListViewItem"/> which's <see cref="ListViewItem.Tag">Tag</see> will be returned</param>
    ''' <returns><paramref name="from"/>.<see cref="ListViewItem.Tag">Tag</see></returns>
    ''' <exception cref="InvalidCastException"><paramref name="from"/>.<see cref="ListViewItem.Tag">Tag</see> is not <see cref="Cont"/></exception>
    Public Shared Narrowing Operator CType(ByVal from As ListViewItem) As Cont
        Return from.Tag
    End Operator
End Class