Imports System.Linq, Tools.DrawingT, Tools.DataStructuresT.GenericT, Tools.CollectionsT.SpecializedT, Tools.IOt.FileSystemTools, Tools.CollectionsT.GenericT
Imports System.ComponentModel, Tools.WindowsT, Tools.ExtensionsT
Imports Tools.MetadataT.IptcT, Tools.MetadataT, Tools.DrawingT.DrawingIOt, Tools.LinqT
Imports System.Reflection
Imports MBox = Tools.WindowsT.IndependentT.MessageBox, MButton = Tools.WindowsT.IndependentT.MessageBox.MessageBoxButton
Imports Tools.WindowsT.FormsT, Tools, Tools.ReflectionT
Imports <xmlns="http://www.w3.org/1999/xhtml">
''' <summary>Main form</summary>
Public Class frmMain
    ''' <summary>Imake gey of ... item</summary>
    Private Const UpKey As String = ".\.."
    ''' <summary>Number of columns in <see cref="flpCommon"/></summary>
    Private Const CommonColumns% = 2
    ''' <summary>Selected IPTCs</summary>
    Private WithEvents SelectedMetadata As New ListWithEvents(Of MetadataItem)
    ''' <summary>Metadatas that was changed</summary>
    Private WithEvents ChangedMetadata As New ListWithEvents(Of MetadataItem)
    ''' <summary>If nonzero item properties are not shown as selection in <see cref="lvwImages"/> changes</summary>
    ''' <seelaso cref="IsChangingSuspended"/>
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private ChangingSuspendedCounter As Byte = 0
    ''' <summary>Gets value indicationg if properties of items are updated as selection in <see cref="lvwImages"/> changes</summary>
    ''' <returns>True if properties are not updated, false if the are</returns>
    Private ReadOnly Property IsChangingSuspended() As Boolean
        Get
            Return ChangingSuspendedCounter <> 0
        End Get
    End Property
    ''' <summary>Suspends updating of properties as selected image changes</summary>
    ''' <remarks>Calls to <see cref="SuspendUpdate"/> and <see cref="ResumeUpdate"/> chan be stacked up to 255 levels</remarks>
    Private Sub SuspendUpdate()
        ChangingSuspendedCounter += 1
    End Sub
    ''' <summary>Resume updating of properties as selected image changes</summary>
    ''' <remarks>Calls to <see cref="SuspendUpdate"/> and <see cref="ResumeUpdate"/> chan be stacked up to 255 levels</remarks>
    ''' <param name="Force">True to ignore stacked calls and resume updating immediatelly</param>
    Private Sub ResumeUpdate(Optional ByVal Force As Boolean = False)
        If Force Then ChangingSuspendedCounter = 0 Else ChangingSuspendedCounter -= 1
        DoSelectedImageChanged()
    End Sub

    ''' <summary>CTor</summary>
    Friend Sub New()
        InitializeComponent()
        Me.Text = String.Format("{0} {1}", My.Application.Info.Title, My.Application.Info.Version)
        imlFolders.Images.Add(UpKey, My.Resources.Up)
        imlImages.ImageSize = My.Settings.ThumbSize

        cmdErrInfo.Parent = picPreview
        llbLarge.Parent = picPreview
        llbLarge.TabStop = False
        lblExifDateTime.Parent = picPreview
        InitializeEditors()
        For Each item As Control In flpCommon.Controls
            item.AutoSize = False
        Next
        SizeInFlpCommon()
        SetCountryCodes()
    End Sub
    ''' <summary>Prepares country codes</summary>
    Private Sub SetCountryCodes()
        Dim c As New IptcT.IptcDataTypes.StringEnum(Of IptcT.Iptc.ISO3166).Converter
        cmbCountryCode.Items.AddRange(c.GetStandardValues.OfType(Of Object).ToArray)
    End Sub

    ''' <summary>Contains controls used for editing single properties</summary>
    Private Editors As Control()
    ''' <summary>Initializes tags of editor controls and the <see cref="Editors"/> field</summary>
    Private Sub InitializeEditors()
        txtSublocation.Tag = CommonIPTCProperties.Sublocation
        txtProvince.Tag = CommonIPTCProperties.Province
        txtObjectName.Tag = CommonIPTCProperties.ObjectName
        txtEditStatus.Tag = CommonIPTCProperties.EditStatus
        txtCredit.Tag = CommonIPTCProperties.Credit
        txtCountry.Tag = CommonIPTCProperties.Country
        txtCopyright.Tag = CommonIPTCProperties.Copyright
        txtCity.Tag = CommonIPTCProperties.City
        txtCaption.Tag = CommonIPTCProperties.Caption
        nudUrgency.Tag = CommonIPTCProperties.Urgency
        kweKeywords.Tag = CommonIPTCProperties.Keywords
        cmbCountryCode.Tag = CommonIPTCProperties.CountryCode
        Editors = New Control() {txtSublocation, txtProvince, txtObjectName, _
            txtEditStatus, txtCredit, txtCountry, _
            txtCopyright, txtCity, txtCaption, _
            nudUrgency, kweKeywords, cmbCountryCode}
    End Sub
    ''' <summary>Current folder</summary>
    Private CurrentFolder$
    ''' <summary>Contains value of the <see cref="Large"/> property</summary>
    Private WithEvents _Large As frmLarge
    ''' <summary>Instance of <see cref="frmLarge"/> used for big preview</summary>
    Public ReadOnly Property Large() As frmLarge
        Get
            Return _Large
        End Get
    End Property

    Private Sub tmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiExit.Click
        Me.Close()
    End Sub

    Private Sub tmiBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiBrowse.Click
        If Not OnBeforeFolderChange() Then Exit Sub
        If fbdGoTo.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim spath$
            Try
                spath = fbdGoTo.SelectedPath
            Catch ex As Exception
                MBox.[Error_XT](ex, My.Resources.Error_)
                Exit Sub
            End Try
            LoadFolder(spath)
        End If
    End Sub
    ''' <summary>If not null indicates that when <see cref="bgwImages"/> is canceled <see cref="LoadFolder"/> should occur with this folder</summary>
    Private LoadOnCancel As IOt.Path = Nothing
    ''' <summary>When <see cref="LoadOnCancel"/> takes effect indicates if it is backward navigation</summary>
    Private LoadOnCancelIsBack As Boolean = False
    ''' <summary>Loads content of folder</summary>
    ''' <param name="Path">Path of folder to load. May also be apth of link to follow.</param>
    ''' <param name="isBack">True when backward navigation is occuring</param>
    Private Sub LoadFolder(ByVal Path As IOt.Path, Optional ByVal isBack As Boolean = False)
        ChangedMetadata.Clear()
        Changed = False
        Dim old = CurrentFolder
        If bgwImages.IsBusy Then
            bgwImages.CancelAsync()
            LoadOnCancel = Path
            LoadOnCancelIsBack = isBack
            Exit Sub
        End If
        If Path.IsFile Then 'Links
            Try
                Path = IOt.ShellLink.ResolveLink(Path)
            Catch
                Path = Nothing
            End Try
            If Path Is Nothing Then Exit Sub
        End If
        'Load subfolders
        Dim subfolders As IEnumerable(Of IOt.Path)
        Try
            subfolders = From sf In Path.GetChildren( _
                Function(p As IOt.Path) _
                    p.IsDirectory OrElse ( _
                        p.IsFile AndAlso _
                        p.Extension.Equals(".lnk", StringComparison.InvariantCultureIgnoreCase) AndAlso _
                        IOt.ShellLink.ResolveLink(p) IsNot Nothing AndAlso _
                        IOt.ShellLink.ResolveLink(p).IsDirectory _
                    )) _
                Order By sf.IsDirectory Descending, sf.FileName Ascending
        Catch ex As Exception
            MBox.[Error_XT](ex, My.Resources.Error_)
            Exit Sub
        End Try
        CurrentFolder = Path
        tslFolder.Text = Path
        lvwFolders.Items.Clear()
        imlFolders.Images.Clear()
        imlFolders.Images.Add(UpKey, My.Resources.Up)
        Try
            If Path.DirectoryName <> "" Then
                Dim parent = Path.DirectoryName
                If Path.Path.Length > 1 AndAlso Path.Path(Path.Path.Length - 1) = "\"c AndAlso Path.Path.Substring(0, Path.Path.Length - 1) = parent Then _
                    parent = New IOt.Path(parent).DirectoryName
                lvwFolders.Items.Add("...", UpKey).Tag = Parent
            End If
        Catch : End Try
        For Each subfolder In subfolders
            Dim icon = subfolder.GetIcon
            If icon IsNot Nothing Then imlFolders.Images.Add(subfolder.FileName, icon)
            lvwFolders.Items.Add(subfolder.FileName, subfolder.FileName).Tag = subfolder.Path
        Next subfolder
        imlImages.Images.Clear()
        SuspendUpdate()
        Try
            lvwImages.Items.Clear()
            'Load files
            Dim ImagesToLoad As New List(Of String)
            Try
                For Each file In From f In Path.GetFiles("*.jpg", "*.jpeg") Order By f.FileName
                    'lvwImages.Items.Add(file.FileName, file.FileName, file.FileName).Tag = file.Path
                    lvwImages.Items.Add(New MetadataItem(file))
                    ImagesToLoad.Add(file.Path)
                Next
            Catch : End Try
            bgwImages.RunWorkerAsync(ImagesToLoad)
            If old IsNot Nothing Then
                If isBack Then ForwardStack.Push(old) Else BackwardStack.Push(old)
                ApplyStacks()
            End If
        Finally
            ResumeUpdate()
        End Try
        tslNoFiles.Text = String.Format("{0} files", lvwImages.Items.Count)
    End Sub

    Private Sub frmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        My.Settings.Folder = CurrentFolder
        My.Settings.FormSize = Me.Size
        My.Settings.FormLocation = Me.Location
        My.Settings.FormState = Me.WindowState
        My.Settings.MainSplitter = Me.splMain.SplitterDistance
        My.Settings.BrowserSplitter = Me.splBrowser.SplitterDistance
        My.Settings.PreviewHeight = panImage.Height
        My.Settings.KeywordsHeight = fraKeywords.Height
        My.Settings.TextHeight = fraTitle.Height
        Dim doc As New System.Xml.XmlDocument
        doc.Load(kweKeywords.GetKeywordsAsXML.CreateReader)
        My.Settings.Keywords = doc
        ToolStripManager.SaveSettings(Me, "tosMain")
        If Large IsNot Nothing Then Large.Close()
        Try : My.Settings.Save()
        Catch ex As Exception
            MBox.[Error_X](ex)
        End Try
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not OnBeforeFolderChange() Then e.Cancel = True : Exit Sub
        If bgwImages.IsBusy Then bgwImages.CancelAsync()
        My.Settings.LargeShown = Me.Large IsNot Nothing
    End Sub

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Not e.Alt AndAlso Not e.Control AndAlso Not e.Shift Then
            'None
            Select Case e.KeyCode
                Case Keys.BrowserBack : NavigateBackward()
                Case Keys.BrowserForward : NavigateForward()
                Case Keys.BrowserRefresh, Keys.F5 : RefreshFolder()
                Case Keys.BrowserHome : LoadFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures))
                Case Keys.BrowserStop : If bgwImages.IsBusy Then bgwImages.CancelAsync()
            End Select
        ElseIf e.Alt AndAlso Not e.Control AndAlso e.Shift Then
            'Alt+Shift
            Select Case e.KeyCode
                Case Keys.Left : GoPrevious(False)
                Case Keys.Right : GoNext(False)
            End Select
        End If
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Size = My.Settings.FormSize
        Me.Location = My.Settings.FormLocation
        Me.WindowState = My.Settings.FormState
        Me.splMain.SplitterDistance = My.Settings.MainSplitter
        Me.splBrowser.SplitterDistance = My.Settings.BrowserSplitter
        Me.panImage.Height = My.Settings.PreviewHeight
        Me.fraKeywords.Height = My.Settings.KeywordsHeight
        Me.fraTitle.Height = My.Settings.TextHeight
        Me.lvwImages.TCBehaviour = My.Settings.TCBehavior
        ToolStripManager.LoadSettings(Me, "tosMain")
        Me.tosMain.Visible = True
        Me.stsStatus.Visible = True
        If My.Settings.Folder = "" OrElse Not IO.Directory.Exists(My.Settings.Folder) Then
            LoadFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures))
        Else
            LoadFolder(My.Settings.Folder)
        End If
        If My.Settings.Keywords IsNot Nothing Then
            Using r = New Xml.XmlNodeReader(My.Settings.Keywords)
                kweKeywords.LoadFromXML(XDocument.Load(r))
            End Using
        End If
    End Sub

    Private Sub bgwImages_DoWork(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwImages.DoWork
        Dim Paths As List(Of String) = e.Argument
        Dim i As Integer = 0
        For Each path In Paths
            i += 1
            Try
                Dim img As New Bitmap(path)
                Dim thimg = img.GetThumbnail(My.Settings.ThumbSize, Color.Transparent, Function() sender.CancellationPending)
                bgwImages.ReportProgress(i / Paths.Count * 100, New Pair(Of String, Image)(System.IO.Path.GetFileName(path), thimg))
            Catch ex As Runtime.InteropServices.ExternalException
                If bgwImages.CancellationPending Then e.Cancel = True : Exit Sub _
                Else Throw
            Catch
                Try
                    Dim icon = IOt.FileSystemTools.GetIcon(path, True).ToBitmap
                    Dim thicon = icon.GetThumbnail(My.Settings.ThumbSize, Color.Transparent, Function() sender.CancellationPending)
                    bgwImages.ReportProgress(i / Paths.Count * 100, New Pair(Of String, Image)(System.IO.Path.GetFileName(path), thicon))
                Catch : End Try
            End Try
            If bgwImages.CancellationPending Then e.Cancel = True : Exit Sub
        Next path
        bgwImages.ReportProgress(100)
    End Sub

    Private Sub bgwImages_ProgressChanged(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwImages.ProgressChanged
        tpbLoading.Visible = Not (e.UserState Is Nothing AndAlso e.ProgressPercentage = 100)
        If e.UserState IsNot Nothing Then
            Dim pair As Pair(Of String, Image) = e.UserState
            imlImages.Images.Add(pair.Value1, pair.Value2)
        End If
        tpbLoading.Value = e.ProgressPercentage
    End Sub

    Private Sub lvwFolders_ItemActivate(ByVal sender As ListView, ByVal e As System.EventArgs) Handles lvwFolders.ItemActivate
        If sender.SelectedItems.Count = 0 Then Exit Sub
        If Not OnBeforeFolderChange() Then Exit Sub
        With sender.SelectedItems(0)
            Dim oldf As String = Nothing
            Try
                oldf = System.IO.Path.GetFileName(CurrentFolder)
            Catch : End Try
            LoadFolder(DirectCast(.Tag, String))
            If .ImageKey = UpKey Then
                AfterUpFolder(sender, oldf)
            End If
        End With
    End Sub

    Private Sub tmiGoTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiGoTo.Click
        If Not OnBeforeFolderChange() Then Exit Sub
        Dim dlg As New frmFolderDialog(CurrentFolder)
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            LoadFolder(dlg.txtPath.Text)
        End If
    End Sub

    Private Sub bgwImages_RunWorkerCompleted(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwImages.RunWorkerCompleted
        If e.Cancelled AndAlso LoadOnCancel IsNot Nothing Then
            Dim loc = LoadOnCancel
            LoadOnCancel = Nothing
            LoadFolder(loc, LoadOnCancelIsBack)
        End If
    End Sub

    Private Sub lvwFolders_KeyDown(ByVal sender As ListView, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvwFolders.KeyDown
        If e.KeyCode = Keys.Back AndAlso sender.Items.Count > 0 AndAlso sender.Items(0).ImageKey = UpKey Then
            Dim oldf As String = Nothing
            Try
                oldf = System.IO.Path.GetFileName(CurrentFolder)
            Catch : End Try
            LoadFolder(DirectCast(sender.Items(0).Tag, String))
            AfterUpFolder(sender, oldf)
        End If
    End Sub
    Private Sub AfterUpFolder(ByVal sender As ListView, ByVal oldf As String)
        Dim prev = (From itm In lvwFolders.Items.AsTypeSafe Where itm.Text = oldf).FirstOrDefault
        If prev IsNot Nothing Then
            lvwFolders.SelectedItems.Clear()
            prev.Selected = True
            prev.EnsureVisible()
            prev.Focused = True
        End If
    End Sub

    Private Sub tsbBack_Click(ByVal sender As ToolStripButton, ByVal e As System.EventArgs) Handles tsbBack.Click
        NavigateBackward()
    End Sub
#Region "Commands"
    ''' <summary>Navigates backward</summary>
    Public Sub NavigateBackward()
        If BackwardStack.Count > 0 Then LoadFolder(BackwardStack.Pop, True)
        ApplyStacks()
    End Sub
    ''' <summary>Navigates forward</summary>
    Public Sub NavigateForward()
        If ForwardStack.Count > 0 Then LoadFolder(ForwardStack.Pop)
        ApplyStacks()
    End Sub
    ''' <summary>Stack for backward navigation</summary>
    Private BackwardStack As New Stack(Of String)
    ''' <summary>Stack for forward navigation</summary>
    Private ForwardStack As New Stack(Of String)

    ''' <summary>Reoads content of current folder</summary>
    Public Sub RefreshFolder()
        If Not OnBeforeFolderChange() Then Exit Sub
        LoadFolder(CurrentFolder)
    End Sub
    ''' <summary>Loads folder identified by path</summary>
    ''' <param name="Path">Path of folder to load or link to follow</param>
    Public Sub LoadFolder(ByVal Path As String)
        LoadFolder(New IOt.Path(Path))
    End Sub
#End Region
    ''' <summary>Applies states of backward/forward stack to appropriate buttons</summary>
    Private Sub ApplyStacks()
        tsbBack.Enabled = BackwardStack.Count > 0
        tsbForward.Enabled = ForwardStack.Count > 0
    End Sub

    Private Sub tsbForward_Click(ByVal sender As ToolStripButton, ByVal e As System.EventArgs) Handles tsbForward.Click
        NavigateForward()
    End Sub

    Private Sub tsbRefresh_Click(ByVal sender As Component, ByVal e As System.EventArgs) _
        Handles tsbRefresh.Click, tmiRefresh.Click
        RefreshFolder()
    End Sub

    Private Sub tmiAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiAbout.Click
        WindowsT.FormsT.AboutDialog.ShowModalDialog(Me)
    End Sub

    Private Sub tmiOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiOptions.Click
        Dim frm As New frmSettings
        Dim oldFloating As Boolean = My.Settings.LargeFloating
        If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            If imlImages.ImageSize <> My.Settings.ThumbSize Then
                imlImages.Images.Clear()
                imlImages.ImageSize = My.Settings.ThumbSize
                LoadFolder(CurrentFolder)
            End If
            If oldFloating <> My.Settings.LargeFloating AndAlso Large IsNot Nothing Then
                Large.Close()
                ShowLarge()
            End If
            If lvwImages.TCBehaviour <> My.Settings.TCBehavior Then _
                lvwImages.TCBehaviour = My.Settings.TCBehavior
        End If
    End Sub

    Private Sub flpCommon_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flpCommon.Resize
        SizeInFlpCommon()
    End Sub
    ''' <summary>Resizes items in <see cref="flpCommon"/></summary>
    Private Sub SizeInFlpCommon()
        For Each Control As Control In flpCommon.Controls
            Control.Width = flpCommon.ClientSize.Width / CommonColumns
        Next
    End Sub

    Private Sub lvwImages_AfterSelectionChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwImages.AfterSelectionChange
        ResumeUpdate()
    End Sub

    Private Sub lvwImages_BeforeSelectionChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwImages.BeforeSelectionChange
        SuspendUpdate()
    End Sub

    Private Sub lvwImages_DrawItem(ByVal sender As ListView, ByVal e As System.Windows.Forms.DrawListViewItemEventArgs) Handles lvwImages.DrawItem
        Dim item As MetadataItem = e.Item
        item.Draw(e)
    End Sub
    Private Sub lvwImages_SelectedIndexChanged(ByVal sender As ListView, ByVal e As System.EventArgs) Handles lvwImages.SelectedIndexChanged
        ' DoSelectedImageChanged() moved to lvwImages_ItemSelectionChanged
    End Sub
    Private Sub lvwImages_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvwImages.ItemSelectionChanged
        DirectCast(e.Item, MetadataItem).OnSelectedChanged()
        DoSelectedImageChanged()
    End Sub
    ''' <summary>Handles the <see cref="lvwImages"/>.<see cref="ListView.SelectedIndexChanged">SelectedIndexChange</see> event</summary>
    Private Sub DoSelectedImageChanged()
        If IsChangingSuspended Then Return
        cmdErrInfo.Visible = False
        Dim prevItem As MetadataItem = lvwImages.FocusedItem
        If ((prevItem IsNot Nothing AndAlso Not prevItem.Selected) OrElse (prevItem Is Nothing)) AndAlso lvwImages.SelectedItems.Count > 0 Then _
            prevItem = lvwImages.SelectedItems(0)
        If prevItem IsNot Nothing Then
            Try
                picPreview.LoadAsync(prevItem.Path.Path)
            Catch ex As Exception
                panImage.BackgroundImage = Nothing
                cmdErrInfo.Visible = True
                cmdErrInfo.Text = ex.Message
                cmdErrInfo.Tag = ex
            End Try
        Else
            picPreview.Image = Nothing
            If Large IsNot Nothing Then Large.Image = Nothing : Large.ImagePath = ""
        End If
        ShowInfo()
    End Sub
    ''' <summary>Shows info about selected images</summary>
    Private Sub ShowInfo()
        'For Each item In SelectedIPTCs
        '    RemoveHandler item.ValueChanged, AddressOf IPTC_ValueChanged
        'Next item
        SelectedMetadata.Clear()
        splMain.Panel2.Enabled = lvwImages.SelectedItems.Count > 0
        lblExifDateTime.Text = ""
        If lvwImages.SelectedItems.Count > 0 Then
            For Each item As MetadataItem In lvwImages.SelectedItems
                If Not item.IPTCLoaded Then
                    Try
                        item.IPTCLoad()
                    Catch ex As Exception
                        MBox.MsgBox(String.Format(My.Resources.ErrorWhileLoading0, item.Path.Path) & vbCrLf & ex.Message, MsgBoxStyle.Critical, My.Resources.Error_)
                        item.Selected = False 'This causes this sub to be recalled
                        'TODO: Line abowe may not work when metadata showing is optimalized
                        Exit Sub
                    End Try
                End If
                ''TODO: Rewrite!
                'If TypeOf item.Tag Is String Then
                '    Try
                '        item.Tag = New IPTCInternal(item.Tag)
                '    Catch ex As Exception
                '        MBox.MsgBox(String.Format(My.Resources.ErrorWhileLoading0, item.Tag) & vbCrLf & ex.Message, MsgBoxStyle.Critical, My.Resources.Error_)
                '        item.Selected = False 'This causes this sub to be recalled
                '        Exit Sub
                '    End Try
                'End If
                SelectedMetadata.Add(item)
            Next
            ShowIPTCValues(From item In SelectedMetadata Select item.IPTC)
            prgIPTC.SelectedObjects = (From mtd In SelectedMetadata Select mtd.IPTC).ToArray
            SetExifPropertyGrids(From mtd In SelectedMetadata Select mtd.Exif)
            Dim exifTime As DateTime?
            For Each item In From mtd In SelectedMetadata Select mtd.Exif
                If item IsNot Nothing AndAlso item.IFD0 IsNot Nothing AndAlso item.IFD0.ExifSubIFD IsNot Nothing AndAlso item.IFD0.ExifSubIFD.DateTimeDigitizedDate IsNot Nothing Then
                    If exifTime Is Nothing Then
                        exifTime = item.IFD0.ExifSubIFD.DateTimeDigitizedDate
                    ElseIf exifTime.Value <> item.IFD0.ExifSubIFD.DateTimeDigitizedDate.Value Then
                        exifTime = Nothing
                        Exit For
                    End If
                End If
            Next
            If exifTime IsNot Nothing Then lblExifDateTime.Text = exifTime.Value.ToString("F")
        Else
            prgIPTC.SelectedObjects = New Object() {}
            SetExifPropertyGrids(New ExifInternal() {})
        End If
    End Sub
    ''' <summary>Sets Exif selected objects</summary>
    ''' <param name="Exifs">Objects to select</param>
    Private Sub SetExifPropertyGrids(ByVal Exifs As IEnumerable(Of ExifInternal))
        prgExifMain.SelectedObjects = (From Exif In Exifs Where Exif IsNot Nothing AndAlso Exif.IFD0 IsNot Nothing Select CObj(Exif.IFD0)).ToArray
        prgExifExif.SelectedObjects = (From Exif In Exifs Where Exif IsNot Nothing AndAlso Exif.IFD0 IsNot Nothing AndAlso Exif.IFD0.ExifSubIFD IsNot Nothing Select CObj(Exif.IFD0.ExifSubIFD)).ToArray
        prgExifGPS.SelectedObjects = (From Exif In Exifs Where Exif IsNot Nothing AndAlso Exif.IFD0 IsNot Nothing AndAlso Exif.IFD0.GPSSubIFD IsNot Nothing Select CObj(Exif.IFD0.GPSSubIFD)).ToArray
        prgExifInterop.SelectedObjects = (From Exif In Exifs Where Exif IsNot Nothing AndAlso Exif.IFD0 IsNot Nothing AndAlso Exif.IFD0.ExifSubIFD IsNot Nothing AndAlso Exif.IFD0.ExifSubIFD.InteropSubIFD IsNot Nothing Select CObj(Exif.IFD0.ExifSubIFD.InteropSubIFD)).ToArray
        prgExifThumbnail.SelectedObjects = (From Exif In Exifs Where Exif IsNot Nothing AndAlso Exif.ThumbnailIFD IsNot Nothing Select CObj(Exif.ThumbnailIFD)).ToArray
    End Sub
    ''' <summary>Shows common values</summary>
    ''' <param name="IPTCs">IPTCs to load values from</param>
    ''' <param name="Filter">Filter properties (load only those which ors with <paramref name="Filter"/></param>
    Private Sub ShowIPTCValues(ByVal IPTCs As IEnumerable(Of IPTCInternal), Optional ByVal Filter As CommonIPTCProperties = CommonIPTCProperties.All)
        Dim Copyright = New With {.Value = CStr(Nothing), .Same = True}
        Dim Credit = New With {.Value = CStr(Nothing), .Same = True}
        Dim City = New With {.Value = CStr(Nothing), .Same = True}
        Dim CountryCode = New With {.Value = CStr(Nothing), .Same = True}
        Dim Country = New With {.Value = CStr(Nothing), .Same = True}
        Dim Province = New With {.Value = CStr(Nothing), .Same = True}
        Dim Sublocation = New With {.Value = CStr(Nothing), .Same = True}
        Dim EditStatus = New With {.Value = CStr(Nothing), .Same = True}
        Dim Urgency = New With {.Value = CType(Nothing, Decimal?), .Same = True}
        Dim ObjectName = New With {.Value = CStr(Nothing), .Same = True}
        Dim Caption = New With {.Value = CStr(Nothing), .Same = True}
        Dim Keywords As New List(Of String)
        Dim i = 0
        For Each IPTC In IPTCs
            If i = 0 Then
                If Filter And CommonIPTCProperties.Copyright Then Copyright.Value = IPTC.CopyrightNotice
                If Filter And CommonIPTCProperties.Credit Then Credit.Value = IPTC.Credit
                If Filter And CommonIPTCProperties.City Then City.Value = IPTC.City
                If Filter And CommonIPTCProperties.CountryCode Then CountryCode.Value = IPTC.CountryPrimaryLocationCode
                If Filter And CommonIPTCProperties.Country Then Country.Value = IPTC.CountryPrimaryLocationName
                If Filter And CommonIPTCProperties.Province Then Province.Value = IPTC.ProvinceState
                If Filter And CommonIPTCProperties.Sublocation Then Sublocation.Value = IPTC.SubLocation
                If Filter And CommonIPTCProperties.EditStatus Then EditStatus.Value = IPTC.EditStatus
                If Filter And CommonIPTCProperties.Urgency Then Urgency.Value = IPTC.Urgency
                If Filter And CommonIPTCProperties.ObjectName Then ObjectName.Value = IPTC.ObjectName
                If Filter And CommonIPTCProperties.Caption Then Caption.Value = IPTC.CaptionAbstract
                If Filter And CommonIPTCProperties.Keywords Then Keywords.AddRange(IPTC.Keywords.NewIfNull)
            Else
                If Filter And CommonIPTCProperties.Copyright Then Copyright.Same = Copyright.Same AndAlso IPTC.CopyrightNotice = Copyright.Value
                If Filter And CommonIPTCProperties.Credit Then Credit.Same = Credit.Same AndAlso IPTC.Credit = Credit.Value
                If Filter And CommonIPTCProperties.City Then City.Same = City.Same AndAlso IPTC.City = City.Value
                If Filter And CommonIPTCProperties.CountryCode Then CountryCode.Same = CountryCode.Same AndAlso IPTC.CountryPrimaryLocationCode = CountryCode.Value
                If Filter And CommonIPTCProperties.Country Then Country.Same = Country.Same AndAlso IPTC.CountryPrimaryLocationName = Country.Value
                If Filter And CommonIPTCProperties.Province Then Province.Same = Province.Same AndAlso IPTC.ProvinceState = Province.Value
                If Filter And CommonIPTCProperties.Sublocation Then Sublocation.Same = Sublocation.Same AndAlso IPTC.SubLocation = Sublocation.Value
                If Filter And CommonIPTCProperties.EditStatus Then EditStatus.Same = EditStatus.Same AndAlso IPTC.EditStatus = EditStatus.Value
                If Filter And CommonIPTCProperties.Urgency Then Urgency.Same = Urgency.Same AndAlso IPTC.Urgency = Urgency.Value
                If Filter And CommonIPTCProperties.ObjectName Then ObjectName.Same = ObjectName.Same AndAlso IPTC.ObjectName = ObjectName.Value
                If Filter And CommonIPTCProperties.Caption Then Caption.Same = Caption.Same AndAlso IPTC.CaptionAbstract = Caption.Value
                If Filter And CommonIPTCProperties.Keywords Then
                    Dim kws As New List(Of String)(If(IPTC.Keywords, New String() {}))
                    Keywords.RemoveAll(Function(kw As String) Not kws.Contains(kw))
                End If
            End If
            i += 1
        Next
        If Filter And CommonIPTCProperties.Copyright Then txtCopyright.Text = If(Copyright.Same, Copyright.Value, "")
        If Filter And CommonIPTCProperties.Credit Then txtCredit.Text = If(Credit.Same, Credit.Value, "")
        If Filter And CommonIPTCProperties.City Then txtCity.Text = If(City.Same, City.Value, "")
        If Filter And CommonIPTCProperties.CountryCode Then cmbCountryCode.Text = If(CountryCode.Same, CountryCode.Value, "")
        If Filter And CommonIPTCProperties.Country Then txtCountry.Text = If(Country.Same, Country.Value, "")
        If Filter And CommonIPTCProperties.Province Then txtProvince.Text = If(Province.Same, Province.Value, "")
        If Filter And CommonIPTCProperties.Sublocation Then txtSublocation.Text = If(Sublocation.Same, Sublocation.Value, "")
        If Filter And CommonIPTCProperties.EditStatus Then txtEditStatus.Text = If(EditStatus.Same, EditStatus.Value, "")
        If Filter And CommonIPTCProperties.Urgency Then nudUrgency.Text = If(Urgency.Same, Urgency.Value.ToString, "") 'TODO: Does it work?
        If Filter And CommonIPTCProperties.ObjectName Then txtObjectName.Text = If(ObjectName.Same, ObjectName.Value, "")
        If Filter And CommonIPTCProperties.Caption Then txtCaption.Text = If(Caption.Same, Caption.Value, "")
        If Filter And CommonIPTCProperties.Keywords Then
            kweKeywords.KeyWords.Clear()
            kweKeywords.KeyWords.AddRange(Keywords)
        End If
    End Sub
    ''' <summary>Contains value of the <see cref="Changed"/> property</summary>
    Private _Changed As Boolean
    ''' <summary>Gets value indicating if there is any unsaved change</summary>
    Public Property Changed() As Boolean
        Get
            Return _Changed
        End Get
        Private Set(ByVal value As Boolean)
            If value <> Changed Then
                If value Then
                    Me.Text = String.Format("{0} {1} *", My.Application.Info.Title, My.Application.Info.Version)
                Else
                    Me.Text = String.Format("{0} {1}", My.Application.Info.Title, My.Application.Info.Version)
                End If
            End If
            _Changed = value
        End Set
    End Property
    ''' <summary>Handles <see cref="IPTCInternal.ValueChanged"/></summary>
    Private Sub Item_Changed(ByVal sender As MetadataItem, ByVal e As MetadataItem.PartEventArgs)
        If Not ChangedMetadata.Contains(sender) Then
            ChangedMetadata.Add(sender)
        End If
        Changed = True
    End Sub

    Private Sub cmdErrInfo_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdErrInfo.Click
        MBox.[Error_X](sender.Tag)
    End Sub

    Private Sub llbLarge_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbLarge.LinkClicked
        ShowLarge()
        e.Link.Visited = True
    End Sub
    ''' <summary>Shows large preview form</summary>
    Public Sub ShowLarge()
        If Large Is Nothing Then _Large = New frmLarge(Me) : Large.Image = picPreview.Image : Large.ImagePath = picPreview.ImageLocation
        If My.Settings.LargeFloating Then
            Large.Show(Me)
        Else
            Large.Show()
        End If
    End Sub

    Private Sub Large_FormClosed(ByVal sender As frmLarge, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles _Large.FormClosed
        _Large = Nothing
    End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If My.Settings.LargeShown AndAlso Large Is Nothing Then
            ShowLarge()
        End If
    End Sub

    Private Sub picPreview_LoadCompleted(ByVal sender As PictureBox, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles picPreview.LoadCompleted
        If Large IsNot Nothing Then Large.Image = sender.Image : Large.ImagePath = sender.ImageLocation
    End Sub
    ''' <summary>Mouse down positions on splitters</summary>
    Private SplitterDowns As New Dictionary(Of Splitter, Point)
    Private Sub splMain_MouseDown(ByVal sender As Splitter, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles sptKeywords.MouseDown, sptImage.MouseDown, sptTitle.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If Not SplitterDowns.ContainsKey(sender) Then _
                SplitterDowns.Add(sender, e.Location) _
            Else _
                SplitterDowns(sender) = e.Location
            sender.Capture = True
        End If
    End Sub
    ''' <summary>Gets control controlled by given <see cref="Splitter"/></summary>
    Private ReadOnly Property SplittedControl(ByVal splitter As Splitter) As Control
        Get
            If splitter Is sptImage Then : Return panImage
            ElseIf splitter Is sptTitle Then : Return fraTitle
            ElseIf splitter Is sptKeywords Then : Return fraKeywords
            End If
            Throw New ApplicationException(My.Resources.UnknownSplitter)
        End Get
    End Property

    Private Sub sptImage_MouseMove(ByVal sender As Splitter, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles sptImage.MouseMove, sptKeywords.MouseMove, sptTitle.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left AndAlso SplitterDowns.ContainsKey(sender) AndAlso SplitterDowns(sender).Y <> e.Location.Y Then
            Dim DeltaY = e.Location.Y - SplitterDowns(sender).Y
            With SplittedControl(sender)
                .Height += DeltaY
                SplitterDowns(sender) = e.Location
            End With
        End If
    End Sub

    Private Sub sptImage_MouseUp(ByVal sender As Splitter, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles sptImage.MouseUp, sptKeywords.MouseUp, sptTitle.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Left Then sender.Capture = False
    End Sub

    Private Sub cmbCountryCode_SelectedIndexChanged(ByVal sender As ComboBox, ByVal e As System.EventArgs) Handles cmbCountryCode.SelectedIndexChanged
        If sender.SelectedIndex >= 0 Then
            Dim selectedItem As IptcT.IptcDataTypes.StringEnum(Of IptcT.Iptc.ISO3166) = sender.SelectedItem
            If selectedItem.ContainsEnum Then
                Dim desc = selectedItem.EnumValue.GetConstant.GetAttribute(Of DisplayNameAttribute)()
                If desc IsNot Nothing Then
                    txtCountry.Text = desc.DisplayName
                    StoreControl_IPTC(sender)
                    StoreControl_IPTC(txtCountry)
                End If
            End If
        End If
    End Sub

    Private Sub Control_Validating(ByVal sender As Control, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles txtSublocation.Validating, txtProvince.Validating, txtObjectName.Validating, _
            txtEditStatus.Validating, txtCredit.Validating, txtCountry.Validating, _
            txtCopyright.Validating, txtCity.Validating, txtCaption.Validating, _
            nudUrgency.Validating, kweKeywords.Validating, cmbCountryCode.Validating
        e.Cancel = Not StoreControl_IPTC(sender)
    End Sub
    ''' <summary>Stores value of given editor control to all items in <see cref="SelectedMetadata"/></summary>
    ''' <param name="ctl">Control to store value of</param>
    ''' <remarks>True if value was stored, false if value was not stored (there was an exception. It was already reported to user).</remarks>
    Private Function StoreControl_IPTC(ByVal ctl As Control) As Boolean
        Dim Prp As CommonIPTCProperties = ctl.Tag
        Dim ctc As StringComparer = Nothing
        If ctl Is kweKeywords Then _
            ctc = StringComparer.Create(System.Globalization.CultureInfo.CurrentCulture, Not kweKeywords.CaseSensitive)
        Dim i As Integer = 0
        Try
            For Each currentitem In From item In SelectedMetadata Select item.IPTC
                If ctl Is nudUrgency Then 'Needs special handling
                    If currentitem.Urgency <> nudUrgency.Value Then _
                        currentitem.Urgency = nudUrgency.Value
                ElseIf ctl Is kweKeywords Then 'Needs very special handling
                    Dim currKws = currentitem.Keywords
                    If SelectedMetadata.Count = 1 OrElse Not kweKeywords.Merge Then
                        'Do not make unnecessary changes
                        If (currKws Is Nothing AndAlso kweKeywords.KeyWords.Count <> 0) OrElse (currKws IsNot Nothing AndAlso currKws.Length <> kweKeywords.KeyWords.Count) Then
                            currentitem.Keywords = kweKeywords.KeyWords.ToArray
                        Else
                            Dim diffFound As Boolean = False
                            If currKws IsNot Nothing Then
                                For Each currentKeyword In currKws
                                    If Not kweKeywords.KeyWords.Contains(currentKeyword, ctc) Then diffFound = True : Exit For
                                Next
                            End If
                            If Not diffFound Then
                                For Each editorKeyword In kweKeywords.KeyWords
                                    If currKws Is Nothing OrElse Not currKws.Contains(editorKeyword, ctc) Then diffFound = True : Exit For
                                Next
                            End If
                            If diffFound Then _
                                currentitem.Keywords = kweKeywords.KeyWords.ToArray
                        End If
                    Else
                        Dim newList As New List(Of String)
                        If Not currKws Is Nothing Then newList.AddRange(currKws)
                        Dim added As Boolean = False
                        For Each kw In kweKeywords.KeyWords
                            If Not newList.Contains(kw, ctc) Then _
                                newList.Add(kw) : added = True
                        Next
                        If added Then currentitem.Keywords = newList.ToArray
                    End If
                Else
                    Try
                        currentitem.Common(Prp) = ctl.Text
                    Catch ex As TargetInvocationException
                        If ex.InnerException IsNot Nothing Then Throw ex.InnerException Else Throw
                    End Try
                End If
                i += 1
            Next
        Catch ex As Exception
            Dim msg$ = ex.Message
            If i > 0 Then msg &= vbCrLf & My.Resources.SomeChangedSomeNot
            Select Case MBox.Modal_PTBI(msg, My.Resources.Error_, MButton.Buttons.OK Or MButton.Buttons.Cancel, MBox.MessageBoxIcons.Error)
                Case Windows.Forms.DialogResult.OK : Return False
                Case Else
                    ShowIPTCValues((From item In SelectedMetadata Select item.IPTC), Prp)
                    Return True
            End Select
        End Try
        Return True
    End Function

    ''' <summary>Gets the inner-most active control on form</summary>
    Private ReadOnly Property InnerActiveControl() As Control
        Get
            Dim c As ContainerControl = Me
            While c.ActiveControl IsNot Nothing
                Dim ac As Control = c.ActiveControl
                If ac Is kweKeywords Then Return ac
                If TypeOf ac Is ContainerControl Then c = ac Else Return ac
            End While
            Return c
        End Get
    End Property
    ''' <summary>If curent <see cref="InnerActiveControl"/> is editing control stores its value using <see cref="StoreControl_IPTC"/></summary>
    Private Sub StoreActiveConrol()
        If TypeOf Me.InnerActiveControl.Tag Is CommonIPTCProperties Then
            StoreControl_IPTC(Me.InnerActiveControl)
        End If
    End Sub
    ''' <summary>Called before current folder changes or before application is closed. If necessary save changes.</summary>
    ''' <returns>True if folder can be changed. False if it cannot.</returns>
    ''' <remarks>Note for plugin implementers: If you are changin folder manually you should always call this method before calling <see cref="LoadFolder"/> anc besed on return value decide whether load that folder or not. If you are implementing some wizard, for example, which's last step changes folder, that you should call this before invoking the wizard.</remarks>
    Public Function OnBeforeFolderChange() As Boolean
        StoreActiveConrol()
        If Not Me.Changed Then Return True
        Select Case MBox.Modal_PTOIB(My.Resources.UnsavedChanges, My.Resources.SaveChanges_dlgTitle, MBox.MessageBoxOptions.AlignLeft, MBox.GetIconDelegate.Invoke(MBox.MessageBoxIcons.Question), _
                New MButton(My.Resources.Save_cmd, Nothing, Windows.Forms.DialogResult.OK, My.Resources.Save_access), _
                New MButton(My.Resources.DontSave_cmd, Nothing, Windows.Forms.DialogResult.No, My.Resources.DontSave_access), _
                MButton.Cancel)
            Case Windows.Forms.DialogResult.OK : Return SaveAll()
            Case Windows.Forms.DialogResult.Cancel : Return False
            Case Else 'No
                Return True
        End Select
    End Function
    ''' <summary>Saves all changes</summary>
    ''' <remarks>True if all changes have been saved or whan it have not been saved but user confirmed that changes may be lost.</remarks>
    Private Function SaveAll() As Boolean
        If Not Changed Then Return True
        If ChangedMetadata.Count <= 3 Then
            Dim result = DoSave()
            Return result
        Else
            Dim result = ProgressMonitor.Show(bgwSave, My.Resources.SavingChangedImages, "", Me)
            Return result.Result
        End If
    End Function

    Private Sub bgwSave_DoWork(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwSave.DoWork
        DoSave(sender, e)
    End Sub
    ''' <summary>Performs saving operations</summary>
    ''' <param name="bgw">If called assynchronously <see cref="bgwSave"/></param>
    ''' <param name="e">If called assynchronously argument e of <see cref="bgwSave"/>.<see cref="BackgroundWorker.DoWork">DoWork</see></param>
    ''' <remarks>True if all changes have been saved or whan it have not been saved but user confirmed that changes may be lost.</remarks>
    Private Function DoSave(Optional ByVal bgw As BackgroundWorker = Nothing, Optional ByVal e As DoWorkEventArgs = Nothing) As Boolean
        DoSave = False
        Try
            Dim i As Integer = 0
            Dim OldCount = ChangedMetadata.Count
            For Each item In ChangedMetadata.ToArray 'Walking on copy!
                If bgw IsNot Nothing Then bgw.ReportProgress(-1, item.Path)
                Try
Retry:              item.Save()
                Catch ex As Exception
                    Select Case MBox.ModalSyncTemplate(Me, _
                            New MBox.FakeBox(MButton.Buttons.Cancel Or MButton.Buttons.Retry Or MButton.Buttons.Ignore), _
                            String.Format(My.Resources.ErrorWhileSaving0, item.Path) & vbCrLf & ex.Message, My.Resources.Error_, Me)
                        Case Windows.Forms.DialogResult.Cancel : Return False
                        Case Windows.Forms.DialogResult.Retry : GoTo Retry
                            'Case Else do nothing
                    End Select
                End Try
                i += 1
                If bgw IsNot Nothing Then bgw.ReportProgress(i / OldCount * 100)
            Next
            DoSave = True
        Finally
            If e IsNot Nothing Then e.Result = DoSave
        End Try
    End Function

    Private Sub tsbSaveAll_Click(ByVal sender As Component, ByVal e As System.EventArgs) _
        Handles tsbSaveAll.Click, tmiSaveAll.Click
        StoreActiveConrol()
        SaveAll()
    End Sub
#Region "ChangedIPTCs handlers"
    Private Sub ChangedMetadata_Added(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of MetadataItem), ByVal e As CollectionsT.GenericT.ListWithEvents(Of MetadataItem).ItemIndexEventArgs) Handles ChangedMetadata.Added
        AddHandler e.Item.PartSaved, AddressOf Changed_Saved
        Changed = True
    End Sub

    Private Sub ChangedMetadata_Cleared(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of MetadataItem), ByVal e As CollectionsT.GenericT.ListWithEvents(Of MetadataItem).ItemsEventArgs) Handles ChangedMetadata.Cleared
        For Each item In e.Items
            RemoveHandler item.PartSaved, AddressOf Changed_Saved
        Next
        Changed = False
    End Sub
    Private Sub ChangedMetadata_Removed(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of MetadataItem), ByVal e As CollectionsT.GenericT.ListWithEvents(Of MetadataItem).ItemIndexEventArgs) Handles ChangedMetadata.Removed
        RemoveHandler e.Item.PartSaved, AddressOf Changed_Saved
        Changed = sender.Count > 0
    End Sub
    ''' <summary>Handles <see cref="IPTCInternal.Saved"/> event</summary>
    ''' <param name="sender">The saved item</param>
    ''' <remarks>May occur in different thread</remarks>
    Private Sub Changed_Saved(ByVal sender As MetadataItem, ByVal e As MetadataItem.PartEventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New Action(Of MetadataItem, MetadataItem.PartEventArgs)(AddressOf Changed_Saved), sender, e)
            Exit Sub
        End If
        ChangedMetadata.Remove(sender)
    End Sub
#End Region
#Region "SelectedIPTCs handlers"
    Private Sub SelectedMetadata_Added(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of MetadataItem), ByVal e As CollectionsT.GenericT.ListWithEvents(Of MetadataItem).ItemIndexEventArgs) Handles SelectedMetadata.Added
        AddHandler e.Item.Change, AddressOf Item_Changed
    End Sub
    Private Sub SelectedMetadata_Cleared(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of MetadataItem), ByVal e As CollectionsT.GenericT.ListWithEvents(Of MetadataItem).ItemsEventArgs) Handles SelectedMetadata.Cleared
        For Each item In e.Items
            RemoveHandler item.Change, AddressOf Item_Changed
        Next
    End Sub
    Private Sub SelectedMetadata_Removed(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of MetadataItem), ByVal e As CollectionsT.GenericT.ListWithEvents(Of MetadataItem).ItemIndexEventArgs) Handles SelectedMetadata.Removed
        RemoveHandler e.Item.Change, AddressOf Item_Changed
    End Sub
#End Region


    Private Sub txtCaption_KeyDown(ByVal sender As TextBox, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCaption.KeyDown
        If e.KeyCode = Keys.A AndAlso e.Control = True AndAlso e.Shift = False AndAlso e.Alt = False Then
            e.Handled = True
            sender.SelectAll()
        End If
    End Sub
    ''' <summary>Selects next image</summary>
    ''' <param name="ResetActiveControl">True to reset active control of main form to Object Name text fiels, false to keep current</param>
    Public Sub GoNext(Optional ByVal ResetActiveControl As Boolean = True)
        StoreActiveConrol()
        Dim item As ListViewItem = lvwImages.FocusedItem
        If (item Is Nothing OrElse Not item.Selected) AndAlso lvwImages.SelectedItems.Count > 0 Then item = lvwImages.SelectedItems(lvwImages.SelectedItems.Count - 1)
        If item Is Nothing Then
            If lvwImages.Items.Count > 0 Then
                lvwImages.SelectedItems.Clear()
                lvwImages.Items(0).Selected = True
                lvwImages.SelectedItems(0).EnsureVisible()
            Else : Beep()
            End If
        Else
            Dim index = lvwImages.Items.IndexOf(item)
            If index >= lvwImages.Items.Count - 1 Then
                Beep()
            Else
                SuspendUpdate()
                Try
                    lvwImages.SelectedItems.Clear()
                    lvwImages.Items(index + 1).Selected = True
                    lvwImages.SelectedItems(0).EnsureVisible()
                Finally
                    ResumeUpdate()
                End Try
            End If
        End If
        If ResetActiveControl Then txtObjectName.Select()
    End Sub
    ''' <summary>Selects previous image</summary>
    ''' <param name="ResetActiveControl">True to reset active control of main form to Object Name text fiels, false to keep current</param>
    Public Sub GoPrevious(Optional ByVal ResetActiveControl As Boolean = True)
        StoreActiveConrol()
        Dim item As ListViewItem = lvwImages.FocusedItem
        If (item Is Nothing OrElse Not item.Selected) AndAlso lvwImages.SelectedItems.Count > 0 Then item = lvwImages.SelectedItems(0)
        If item Is Nothing Then
            If lvwImages.Items.Count > 0 Then
                lvwImages.SelectedItems.Clear()
                lvwImages.Items(lvwImages.Items.Count - 1).Selected = True
                lvwImages.SelectedItems(0).EnsureVisible()
            Else : Beep()
            End If
        Else
            Dim index = lvwImages.Items.IndexOf(item)
            If index <= 0 Then
                Beep()
            Else
                SuspendUpdate()
                Try
                    lvwImages.SelectedItems.Clear()
                    lvwImages.Items(index - 1).Selected = True
                    lvwImages.SelectedItems(0).EnsureVisible()
                Finally
                    ResumeUpdate()
                End Try
            End If
        End If
        If ResetActiveControl Then txtObjectName.Select()
    End Sub

    Private Sub tmiNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiNext.Click
        GoNext()
    End Sub

    Private Sub tmiPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiPrevious.Click
        GoPrevious()
    End Sub

    Private Sub Large_ImageAltered(ByVal sender As frmLarge) Handles _Large.ImageAltered
        picPreview.Image = sender.Image
        picPreview.Invalidate()
    End Sub

    Private Sub cmsImages_Opening(ByVal sender As ContextMenuStrip, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsImages.Opening
        tmiMerge.Visible = MergeKeywordsPossible
        'If sender.Items.AsTypeSafe.FirstOrDefault(Function(i As ToolStripMenuItem) i.Visible) Is Nothing Then _
        '    e.Cancel = True : Exit Sub
    End Sub
    Private ReadOnly Property MergeKeywordsPossible() As Boolean
        Get
            Return lvwImages.SelectedItems.Count >= 3 AndAlso _
                (From i As MetadataItem In lvwImages.SelectedItems _
                    Where (Not i.Focused AndAlso i.IPTCContains AndAlso i.IPTC.Keywords IsNot Nothing AndAlso i.IPTC.Keywords.Length > 0) _
                ).Count >= 2 AndAlso _
                lvwImages.FocusedItem IsNot Nothing AndAlso lvwImages.FocusedItem.Selected
        End Get
    End Property

    Private Sub tmiMerge_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmiMerge.Click
        If Not MergeKeywordsPossible Then
            MBox.MsgBox(My.Resources.WhyIsNotMergePossible, MsgBoxStyle.Information, My.Resources.Error_)
            Exit Sub
        End If
        Dim newKw = (From i As MetadataItem In lvwImages.SelectedItems Where Not i.Focused AndAlso i.IPTCContains AndAlso i.IPTC.Keywords IsNot Nothing Select DirectCast(i.IPTC.Keywords, IEnumerable(Of String))).FlatDistinct
        With DirectCast(lvwImages.FocusedItem, MetadataItem)
            If .IPTC.Keywords IsNot Nothing Then newKw = newKw.Union(.IPTC.Keywords)
            .IPTC.Keywords = newKw.ToArray
            SuspendUpdate()
            Try
                For Each src In From Selected As MetadataItem In lvwImages.SelectedItems Where Not Selected.Focused
                    src.Selected = False
                Next
            Finally
                ResumeUpdate()
            End Try
            .EnsureVisible()
        End With
        kweKeywords.Focus()
    End Sub

    Private Sub tmiVersionHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiVersionHistory.Click
        Dim d = XDocument.Parse(My.Resources.VersionHistoryEnvelope)
        Dim div = (From el In d.<html>.<body>.<div> Where el.@id = "history").First
        Dim i As Integer = 1
        Dim CurrentString$
        Do
            Currentstring = Nothing
            'Try
            CurrentString = My.Resources.Resources.ResourceManager.GetString(String.Format("VersionHistory_{0}", i))
            'Catch ex As Exception
            'End Try
            If CurrentString IsNot Nothing Then
                Dim CurrentXml = XDocument.Parse(CurrentString)
                div.AddFirst(CurrentXml.<div>.First)
            End If
            i += 1
        Loop While CurrentString IsNot Nothing
        HTMLDialog.ShowModal(d.ToString, Me)
    End Sub

    Private Sub lvwImages_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwImages.SelectedIndexChanged

    End Sub

    Private Sub lvwImages_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawListViewItemEventArgs) Handles lvwImages.DrawItem

    End Sub

    Private Sub tmiSynchronizeWithDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiSynchronizeWithDatabase.Click
        SynchronizeWithDatabase()
    End Sub
    ''' <summary>Launches the "Synchronize with database" wizard</summary>
    Public Sub SynchronizeWithDatabase()
        Dim Wizard As New Wizard(Of Data.SelectDatabaseStep)
        Wizard.Text = My.Resources.SynchronizeWithDatabase
        Wizard.ShowDialog(Me)
    End Sub
End Class

''' <summary>Common IPTC properties displayed at common tab of <see cref="frmMain"/></summary>
<Flags()> _
Friend Enum CommonIPTCProperties
    ''' <summary>No property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> None = 0
    ''' <summary>All properties</summary>
    All = Copyright Or Credit Or City Or CountryCode Or Country Or Province Or Sublocation Or EditStatus Or Urgency Or ObjectName Or Caption Or Keywords
    ''' <summary><see cref="IPTC.CopyrightNotice"/></summary>
    Copyright = 1
    ''' <summary><see cref="IPTC.Credit"/></summary>
    Credit = 2
    ''' <summary><see cref="IPTC.City"/></summary>
    City = 4
    ''' <summary><see cref="IPTC.CountryPrimaryLocationCode"/></summary>
    CountryCode = 8
    ''' <summary><see cref="IPTC.CountryPrimaryLocationName"/></summary>
    Country = 16
    ''' <summary><see cref="IPTC.ProvinceState"/></summary>
    Province = 32
    ''' <summary><see cref="IPTC.SubLocation"/></summary>
    Sublocation = 64
    ''' <summary><see cref="IPTC.EditStatus"/></summary>
    EditStatus = 128
    ''' <summary><see cref="IPTC.Urgency"/></summary>
    Urgency = 256
    ''' <summary><see cref="IPTC.ObjectName"/></summary>
    ObjectName = 1024
    ''' <summary><see cref="IPTC.CaptionAbstract"/></summary>
    Caption = 2048
    ''' <summary><see cref="IPTC.Keywords"/></summary>
    Keywords = 1096
End Enum