Imports System.Linq, Tools.DrawingT, Tools.DataStructuresT.GenericT, Tools.CollectionsT.SpecializedT, Tools.IOt.FileTystemTools, Tools.CollectionsT.GenericT
Imports System.ComponentModel, Tools.WindowsT, Tools.ExtensionsT
Imports Tools.DrawingT.MetadataT, Tools.DrawingT.DrawingIOt
Imports System.Reflection
Imports MBox = Tools.WindowsT.IndependentT.MessageBox, MButton = Tools.WindowsT.IndependentT.MessageBox.MessageBoxButton
Imports Tools.WindowsT.FormsT

''' <summary>Main form</summary>
Public Class frmMain
    ''' <summary>Imake gey of ... item</summary>
    Private Const UpKey As String = ".\.."
    Private Const CommonColumns% = 2
    ''' <summary>CTor</summary>
    Friend Sub New()
        InitializeComponent()
        Me.Text = String.Format("{0} {1}", My.Application.Info.Title, My.Application.Info.Version)
        imlFolders.Images.Add(UpKey, My.Resources.Up)
        imlImages.ImageSize = My.Settings.ThumbSize

        cmdErrInfo.Parent = picPreview
        llbLarge.Parent = picPreview
        llbLarge.TabStop = False
        InitializeEditors()
        For Each item As Control In flpCommon.Controls
            item.AutoSize = False
        Next
        SizeInFlpCommon()
    End Sub
    ''' <summary>Contains controls used for editing single properties</summary>
    Private Editors As Control()
    ''' <summary>Initializes tags of editor controls and the <see cref="Editors"/> field</summary>
    Private Sub InitializeEditors()
        txtSublocation.Tag = CommonProperties.Sublocation
        txtProvince.Tag = CommonProperties.Province
        txtObjectName.Tag = CommonProperties.ObjectName
        txtEditStatus.Tag = CommonProperties.EditStatus
        txtCredit.Tag = CommonProperties.Credit
        txtCountry.Tag = CommonProperties.Country
        txtCopyright.Tag = CommonProperties.Copyright
        txtCity.Tag = CommonProperties.City
        txtCaption.Tag = CommonProperties.Caption
        nudUrgency.Tag = CommonProperties.Urgency
        kweKeywords.Tag = CommonProperties.Keywords
        cmbCountryCode.Tag = CommonProperties.CountryCode
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
                MBox.Error(ex, My.Resources.Error_)
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
        ChangedIPTCs.Clear()
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
            MBox.Error(ex, My.Resources.Error_)
            Exit Sub
        End Try
        CurrentFolder = Path
        tslFolder.Text = Path
        lvwFolders.Items.Clear()
        imlFolders.Images.Clear()
        imlFolders.Images.Add(UpKey, My.Resources.Up)
        Try
            If Path.DirectoryName <> "" Then
                lvwFolders.Items.Add("...", UpKey).Tag = Path.DirectoryName
            End If
        Catch : End Try
        For Each subfolder In subfolders
            Dim icon = subfolder.GetIcon
            If icon IsNot Nothing Then imlFolders.Images.Add(subfolder.FileName, icon)
            lvwFolders.Items.Add(subfolder.FileName, subfolder.FileName).Tag = subfolder.Path
        Next subfolder
        imlImages.Images.Clear()
        lvwImages.Items.Clear()
        'Load files
        Dim ImagesToLoad As New List(Of String)
        Try
            For Each file In From f In Path.GetFiles("*.jpg", "*.jpeg") Order By f.FileName
                lvwImages.Items.Add(file.FileName, file.FileName).Tag = file.Path
                ImagesToLoad.Add(file.Path)
            Next
        Catch : End Try
        bgwImages.RunWorkerAsync(ImagesToLoad)
        If old IsNot Nothing Then
            If isBack Then ForwardStack.Push(old) Else BackwardStack.Push(old)
            ApplyStacks()
        End If
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
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not OnBeforeFolderChange() Then e.Cancel = True : Exit Sub
        If bgwImages.IsBusy Then bgwImages.CancelAsync()
        My.Settings.LargeShown = Me.Large IsNot Nothing
    End Sub

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.BrowserBack : NavigateBackward()
            Case Keys.BrowserForward : NavigateForward()
            Case Keys.BrowserRefresh, Keys.F5 : RefreshFolder()
            Case Keys.BrowserHome : LoadFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures))
            Case Keys.BrowserStop : If bgwImages.IsBusy Then bgwImages.CancelAsync()
        End Select
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
        ToolStripManager.LoadSettings(Me, "tosMain")
        If My.Settings.Folder = "" Then
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
            Catch
                Try
                    Dim icon = IOt.FileTystemTools.GetIcon(path, True).ToBitmap
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

    Private Sub tsbRefresh_Click(ByVal sender As ToolStripButton, ByVal e As System.EventArgs) Handles tsbRefresh.Click
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

    Private Sub lvwImages_SelectedIndexChanged(ByVal sender As ListView, ByVal e As System.EventArgs) Handles lvwImages.SelectedIndexChanged
        cmdErrInfo.Visible = False
        If lvwImages.FocusedItem IsNot Nothing Then
            Try
                'panImage.BackgroundImage = New Bitmap(lvwImages.FocusedItem.Tag.ToString)
                picPreview.LoadAsync(lvwImages.FocusedItem.Tag.ToString)
                'If Large IsNot Nothing Then Large.BackgroundImage = panImage.BackgroundImage
            Catch ex As Exception
                panImage.BackgroundImage = Nothing
                cmdErrInfo.Visible = True
                cmdErrInfo.Text = ex.Message
                cmdErrInfo.Tag = ex
            End Try
        Else
            picPreview.Image = Nothing
            If Large IsNot Nothing Then Large.BackgroundImage = Nothing
        End If
        ShowInfo()
    End Sub
    ''' <summary>Selected IPTCs</summary>
    Private SelectedIPTCs As New List(Of IPTCInternal)
    ''' <summary>Metadatas that was changed</summary>
    Private ChangedIPTCs As New List(Of IPTCInternal)
    ''' <summary>Shows info about selected images</summary>
    Private Sub ShowInfo()
        For Each item In SelectedIPTCs
            RemoveHandler item.ValueChanged, AddressOf IPTC_ValueChanged
        Next item
        SelectedIPTCs.Clear()
        splMain.Panel2.Enabled = lvwImages.SelectedItems.Count > 0
        If lvwImages.SelectedItems.Count > 0 Then
            For Each item As ListViewItem In lvwImages.SelectedItems
                If TypeOf item.Tag Is String Then
                    'TODO:Handle exception
                    item.Tag = New IPTCInternal(item.Tag)
                End If
                SelectedIPTCs.Add(item.Tag)
                AddHandler DirectCast(item.Tag, IPTCInternal).ValueChanged, AddressOf IPTC_ValueChanged
            Next
            ShowValues(SelectedIPTCs)
            prgIPTC.SelectedObjects = SelectedIPTCs.ToArray
        Else
            prgIPTC.SelectedObjects = New Object() {}
        End If
    End Sub
    Private Enum CommonProperties
        <EditorBrowsable(EditorBrowsableState.Never)> None = 0
        All = Copyright Or Credit Or City Or CountryCode Or Country Or Province Or Sublocation Or EditStatus Or Urgency Or ObjectName Or Caption Or Keywords
        Copyright = 1
        Credit = 2
        City = 4
        CountryCode = 8
        Country = 16
        Province = 32
        Sublocation = 64
        EditStatus = 128
        Urgency = 256
        ObjectName = 1024
        Caption = 2048
        Keywords = 1096
    End Enum
    ''' <summary>Shows common values</summary>
    ''' <param name="IPTCs">IPTCs to load values from</param>
    ''' <param name="Filter">Filter properties (load only those which ors with <paramref name="Filter"/></param>
    Private Sub ShowValues(ByVal IPTCs As List(Of IPTCInternal), Optional ByVal Filter As CommonProperties = CommonProperties.All)
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
                If Filter And CommonProperties.Copyright Then Copyright.Value = IPTC.CopyrightNotice
                If Filter And CommonProperties.Credit Then Credit.Value = IPTC.Credit
                If Filter And CommonProperties.City Then City.Value = IPTC.City
                If Filter And CommonProperties.CountryCode Then CountryCode.Value = IPTC.CountryPrimaryLocationCode
                If Filter And CommonProperties.Country Then Country.Value = IPTC.CountryPrimaryLocationName
                If Filter And CommonProperties.Province Then Province.Value = IPTC.ProvinceState
                If Filter And CommonProperties.Sublocation Then Sublocation.Value = IPTC.SubLocation
                If Filter And CommonProperties.EditStatus Then EditStatus.Value = IPTC.EditStatus
                If Filter And CommonProperties.Urgency Then Urgency.Value = IPTC.Urgency
                If Filter And CommonProperties.ObjectName Then ObjectName.Value = IPTC.ObjectName
                If Filter And CommonProperties.Caption Then Caption.Value = IPTC.CaptionAbstract
                If Filter And CommonProperties.Keywords Then Keywords.AddRange(IPTC.Keywords.NewIfNull)
            Else
                If Filter And CommonProperties.Copyright Then Copyright.Same = IPTC.CopyrightNotice = Copyright.Value
                If Filter And CommonProperties.Credit Then Credit.Same = IPTC.Credit = Credit.Value
                If Filter And CommonProperties.City Then City.Same = IPTC.City = City.Value
                If Filter And CommonProperties.CountryCode Then CountryCode.Same = IPTC.CountryPrimaryLocationCode = CountryCode.Value
                If Filter And CommonProperties.Country Then Country.Same = IPTC.CountryPrimaryLocationName = Country.Value
                If Filter And CommonProperties.Province Then Province.Same = IPTC.ProvinceState = Province.Value
                If Filter And CommonProperties.Sublocation Then Sublocation.Same = IPTC.SubLocation = Sublocation.Value
                If Filter And CommonProperties.EditStatus Then EditStatus.Same = IPTC.EditStatus = EditStatus.Value
                If Filter And CommonProperties.Urgency Then Urgency.Same = IPTC.Urgency = Urgency.Value
                If Filter And CommonProperties.ObjectName Then ObjectName.Same = IPTC.ObjectName = ObjectName.Value
                If Filter And CommonProperties.Caption Then Caption.Same = IPTC.CaptionAbstract = Caption.Value
                If Filter And CommonProperties.Keywords Then
                    Dim kws As New List(Of String)(IPTC.Keywords)
                    Keywords.RemoveAll(Function(kw As String) Not kws.Contains(kw))
                End If
            End If
            i += 1
        Next
        If Filter And CommonProperties.Copyright Then txtCopyright.Text = If(Copyright.Same, Copyright.Value, "")
        If Filter And CommonProperties.Credit Then txtCredit.Text = If(Credit.Same, Credit.Value, "")
        If Filter And CommonProperties.City Then txtCity.Text = If(City.Same, City.Value, "")
        If Filter And CommonProperties.CountryCode Then cmbCountryCode.Text = If(CountryCode.Same, CountryCode.Value, "")
        If Filter And CommonProperties.Country Then txtCountry.Text = If(Country.Same, Country.Value, "")
        If Filter And CommonProperties.Province Then txtProvince.Text = If(Province.Same, Province.Value, "")
        If Filter And CommonProperties.Sublocation Then txtSublocation.Text = If(Sublocation.Same, Sublocation.Value, "")
        If Filter And CommonProperties.EditStatus Then txtEditStatus.Text = If(EditStatus.Same, EditStatus.Value, "")
        If Filter And CommonProperties.Urgency Then nudUrgency.Text = If(Urgency.Same, Urgency.Value.ToString, "") 'TODO: Does it work?
        If Filter And CommonProperties.ObjectName Then txtObjectName.Text = If(ObjectName.Same, ObjectName.Value, "")
        If Filter And CommonProperties.Caption Then txtCaption.Text = If(Caption.Same, Caption.Value, "")
        If Filter And CommonProperties.Keywords Then
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
    Private Sub IPTC_ValueChanged(ByVal sender As IPTCInternal, ByVal e As EventArgs)
        If Not ChangedIPTCs.Contains(sender) Then ChangedIPTCs.Add(sender)
        Changed = True
        With lvwImages.Items(sender.ImagePath)
            .Text = System.IO.Path.GetFileName(sender.ImagePath) & "*"
        End With
    End Sub

    Private Sub cmdErrInfo_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdErrInfo.Click
        MBox.Error(sender.Tag)
    End Sub

    Private Sub llbLarge_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbLarge.LinkClicked
        ShowLarge()
        e.Link.Visited = True
    End Sub
    ''' <summary>Shows large preview form</summary>
    Public Sub ShowLarge()
        If Large Is Nothing Then _Large = New frmLarge : Large.BackgroundImage = picPreview.Image
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
        If Large IsNot Nothing Then Large.BackgroundImage = sender.Image
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

    ''' <summary>Extends <see cref="IPTC"/> with some extra stuff</summary>
    <DebuggerDisplay("{ImagePath}")> _
    Private Class IPTCInternal
        Inherits IPTC
        ''' <summary>CTor</summary>
        ''' <param name="ImagePath">Path of JPEG file</param>
        Public Sub New(ByVal ImagePath As String)
            MyBase.New(New JPEG.JPEGReader(ImagePath, False))
            _ImagePath = ImagePath
            _Changed = False
        End Sub
        ''' <summary>Contains value of the <see cref="ImagePath"/> property</summary>
        Private _ImagePath As String
        ''' <summary>Path of image this instance holds information for</summary>
        Public ReadOnly Property ImagePath() As String
            Get
                Return _ImagePath
            End Get
        End Property
        ''' <summary>String representation</summary>
        ''' <returns><see cref="ImagePath"/></returns>
        Public Overrides Function ToString() As String
            Return _ImagePath
        End Function
        ''' <summary>Raises the <see cref="ValueChanged"/> event</summary>
        ''' <param name="Tag">Recod and dataset number</param>
        ''' <remarks>
        ''' <para>Called by <see cref="Tag"/>'s setter.</para>
        ''' <para>Note for inheritors: Call base class method in order to automatically compute size of embdeded file and invalidate cache for <see cref="BW460_Value"/></para>
        ''' </remarks>
        Protected Overrides Sub OnValueChanged(ByVal Tag As DrawingT.MetadataT.IPTC.DataSetIdentification)
            MyBase.OnValueChanged(Tag)
            _Changed = True
            RaiseEvent ValueChanged(Me, EventArgs.Empty)
        End Sub
        ''' <summary>Contains value of the <see cref="Changed"/> property</summary>
        Private _Changed As Boolean
        ''' <summary>Gets value indicating if this instance is dirty</summary>
        ''' <returns>True if instance was changed since save/load</returns>
        Public ReadOnly Property Changed() As Boolean
            Get
                Return _Changed
            End Get
        End Property
        ''' <summary>Raised when value of any tag changes</summary>
        Public Event ValueChanged As EventHandler(Of IPTCInternal, EventArgs)
        ''' <summary>Gets or sets value of common property identified by value of <see cref="CommonProperties"/></summary>
        ''' <param name="Property">Property tpo get/set</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Property"/> is none of predefined <see cref="CommonProperties"/> values or it is <see cref="CommonProperties.None"/> or <see cref="CommonProperties.All"/>.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="Property"/> is <see cref="CommonProperties.Keywords"/></exception>
        Friend Property Common(ByVal [Property] As CommonProperties) As String
            Get
                Select Case [Property]
                    Case CommonProperties.Caption : Return CaptionAbstract
                    Case CommonProperties.City : Return City
                    Case CommonProperties.Copyright : Return CopyrightNotice
                    Case CommonProperties.Country : Return CountryPrimaryLocationName
                    Case CommonProperties.CountryCode : Return CountryPrimaryLocationCode
                    Case CommonProperties.Credit : Return Credit
                    Case CommonProperties.EditStatus : Return EditStatus
                    Case CommonProperties.Keywords : Throw New NotSupportedException(My.Resources.KeywordsAreNotSupportedByCommponProperty_internalError)
                    Case CommonProperties.ObjectName : Return ObjectName
                    Case CommonProperties.Province : Return ProvinceState
                    Case CommonProperties.Sublocation : Return SubLocation
                    Case CommonProperties.ObjectName : Return ObjectName
                    Case Else : Throw New InvalidEnumArgumentException("Property", [Property], [Property].GetType)
                End Select
            End Get
            Set(ByVal value As String)
                Select Case [Property]
                    Case CommonProperties.Caption : If value <> CaptionAbstract Then CaptionAbstract = value
                    Case CommonProperties.City : If value <> City Then City = value
                    Case CommonProperties.Copyright : If value <> CopyrightNotice Then CopyrightNotice = value
                    Case CommonProperties.Country : If value <> CountryPrimaryLocationName Then CountryPrimaryLocationName = value
                    Case CommonProperties.CountryCode : If value <> CountryPrimaryLocationCode Then CountryPrimaryLocationCode = value
                    Case CommonProperties.Credit : If value <> Credit Then Credit = value
                    Case CommonProperties.EditStatus : If value <> EditStatus Then EditStatus = value
                    Case CommonProperties.Keywords : Throw New NotSupportedException(My.Resources.KeywordsAreNotSupportedByCommponProperty_internalError)
                    Case CommonProperties.ObjectName : If value <> ObjectName Then ObjectName = value
                    Case CommonProperties.Province : If value <> ProvinceState Then ProvinceState = value
                    Case CommonProperties.Sublocation : If value <> SubLocation Then SubLocation = value
                    Case CommonProperties.Urgency : If value <> Urgency Then Urgency = value
                    Case Else : Throw New InvalidEnumArgumentException("Property", [Property], [Property].GetType)
                End Select
            End Set
        End Property
        ''' <summary>Saves current IPTC stream to file <see cref="ImagePath"/></summary>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The specified <see cref="ImagePath"/> is invalid, such as being on an unmapped drive.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The access requested (readonly) is not permitted by the operating system for the specified path.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.IO.FileNotFoundException">The file cannot be found.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="IO.InvalidDataException">
        ''' Invalid JPEG marker found (code doesn't start with FFh, length set to 0 or 2) -or-
        ''' JPEG stream doesn't start with corect SOI marker -or-
        ''' JPEG stream doesn't end with corect EOI marker
        ''' </exception>
        ''' <exception cref="InvalidOperationException">No JPEG marker found</exception>
        Friend Sub Save()
            Using jw As New JPEG.JPEGReader(Me.ImagePath, True)
                jw.IPTCEmbed(Me.GetBytes)
            End Using
        End Sub
    End Class

    Private Sub Control_Validating(ByVal sender As Control, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles txtSublocation.Validating, txtProvince.Validating, txtObjectName.Validating, _
            txtEditStatus.Validating, txtCredit.Validating, txtCountry.Validating, _
            txtCopyright.Validating, txtCity.Validating, txtCaption.Validating, _
            nudUrgency.Validating, kweKeywords.Validating, cmbCountryCode.Validating
        e.Cancel = Not StoreControl(sender)
    End Sub
    ''' <summary>Stores value of given editor control to all items in <see cref="SelectedIPTCs"/></summary>
    ''' <param name="ctl">Control to store value of</param>
    ''' <remarks>True if value was stored, false if value was not stored (there was an exception. It was already reported to user).</remarks>
    Private Function StoreControl(ByVal ctl As Control) As Boolean
        Dim Prp As CommonProperties = ctl.Tag
        Dim ctc As StringComparer = Nothing
        If ctl Is kweKeywords Then _
            ctc = StringComparer.Create(System.Globalization.CultureInfo.CurrentCulture, Not kweKeywords.CaseSensitive)
        Dim i As Integer = 0
        Try
            For Each currentitem In SelectedIPTCs
                If ctl Is nudUrgency Then 'Needs special handling
                    If currentitem.Urgency <> nudUrgency.Value Then _
                        currentitem.Urgency = nudUrgency.Value
                ElseIf ctl Is kweKeywords Then 'Needs very special handling
                    Dim currKws = currentitem.Keywords
                    If SelectedIPTCs.Count = 1 OrElse Not kweKeywords.Merge Then
                        'Do not make unnecessary changes
                        If currKws.Length <> kweKeywords.KeyWords.Count Then
                            currentitem.Keywords = kweKeywords.KeyWords.ToArray
                        Else
                            Dim diffFound As Boolean = False
                            For Each currentKeyword In currKws
                                If Not kweKeywords.KeyWords.Contains(currentKeyword, ctc) Then diffFound = True : Exit For
                            Next
                            If Not diffFound Then
                                For Each editorKeyword In kweKeywords.KeyWords
                                    If Not currKws.Contains(editorKeyword, ctc) Then diffFound = True : Exit For
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
            Select Case MBox.Modal(msg, My.Resources.Error_, MButton.Buttons.OK Or MButton.Buttons.Cancel, , MBox.MessageBoxIcons.Error)
                Case Windows.Forms.DialogResult.OK : Return False
                Case Else
                    ShowValues(SelectedIPTCs, Prp)
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
                If TypeOf ac Is ContainerControl Then c = ac Else Return ac
            End While
            Return c
        End Get
    End Property
    ''' <summary>If curent <see cref="InnerActiveControl"/> is editing control stores its value using <see cref="StoreControl"/></summary>
    Private Sub StoreActiveConrol()
        If TypeOf Me.InnerActiveControl.Tag Is CommonProperties Then
            StoreControl(Me.InnerActiveControl)
        End If
    End Sub
    ''' <summary>Called before current folder changes or before application is closed. If necessary save changes.</summary>
    ''' <returns>True if folder can be changed. False if it cannot.</returns>
    ''' <remarks>Note for plugin implementers: If you are changin folder manually you should always call this method before calling <see cref="LoadFolder"/> anc besed on return value decide whether load that folder or not. If you are implementing some wizard, for example, which's last step changes folder, that you should call this before invoking the wizard.</remarks>
    Public Function OnBeforeFolderChange() As Boolean
        If Not Me.Changed Then Return True
        Select Case MBox.Modal(My.Resources.UnsavedChanges, My.Resources.SaveChanges_dlgTitle, MBox.MessageBoxOptions.AlignLeft, MBox.GetIconDelegate.Invoke(MBox.MessageBoxIcons.Question), _
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
        If ChangedIPTCs.Count <= 3 Then
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
        Try
            Dim i As Integer = 0
            For Each item In ChangedIPTCs
                bgw.ReportProgress(-1, item.ImagePath)
                item.save()
                'TODO: Catch error and show SYSNC messagebox
                i += 1
                bgw.ReportProgress(i / ChangedIPTCs.Count * 100)
            Next
        Finally
            If e IsNot Nothing Then e.Result = DoSave
        End Try
    End Function
End Class