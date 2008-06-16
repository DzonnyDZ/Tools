Imports System.Linq, Tools.DrawingT, Tools.DataStructuresT.GenericT, Tools.CollectionsT.SpecializedT, Tools.IOt.FileTystemTools, Tools.CollectionsT.GenericT
Imports System.ComponentModel, Tools.WindowsT, Tools.ExtensionsT
Imports Tools.DrawingT.MetadataT, Tools.DrawingT.DrawingIOt

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

        For Each item As Control In flpCommon.Controls
            item.AutoSize = False
        Next
        SizeInFlpCommon()
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
        If fbdGoTo.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim spath$
            Try
                spath = fbdGoTo.SelectedPath
            Catch ex As Exception
                IndependentT.MessageBox.Error(ex, My.Resources.Error_)
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
            IndependentT.MessageBox.Error(ex, My.Resources.Error_)
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
        ToolStripManager.SaveSettings(Me, "tosMain")
        If Large IsNot Nothing Then Large.Close()
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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
    ''' <summary>Shows common values</summary>
    Private Sub ShowValues(ByVal IPTCs As List(Of IPTCInternal))
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
                Copyright.Value = IPTC.CopyrightNotice
                Credit.Value = IPTC.Credit
                City.Value = IPTC.City
                CountryCode.Value = IPTC.CountryPrimaryLocationCode
                Country.Value = IPTC.CountryPrimaryLocationName
                Province.Value = IPTC.ProvinceState
                Sublocation.Value = IPTC.SubLocation
                EditStatus.Value = IPTC.EditStatus
                Urgency.Value = IPTC.Urgency
                ObjectName.Value = IPTC.ObjectName
                Caption.Value = IPTC.CaptionAbstract
                Keywords.AddRange(IPTC.Keywords.NewIfNull)
            Else
                Copyright.Same = IPTC.CopyrightNotice = Copyright.Value
                Credit.Same = IPTC.Credit = Credit.Value
                City.Same = IPTC.City = City.Value
                CountryCode.Same = IPTC.CountryPrimaryLocationCode = CountryCode.Value
                Country.Same = IPTC.CountryPrimaryLocationName = Country.Value
                Province.Same = IPTC.ProvinceState = Province.Value
                Sublocation.Same = IPTC.SubLocation = Sublocation.Value
                EditStatus.Same = IPTC.EditStatus = EditStatus.Value
                Urgency.Same = IPTC.Urgency = Urgency.Value
                ObjectName.Same = IPTC.ObjectName = ObjectName.Value
                Caption.Same = IPTC.CaptionAbstract = Caption.Value
                Dim kws As New List(Of String)(IPTC.Keywords)
                Keywords.RemoveAll(Function(kw As String) Not kws.Contains(kw))
            End If
            i += 1
        Next
        txtCopyright.Text = If(Copyright.Same, Copyright.Value, "")
        txtCredit.Text = If(Credit.Same, Credit.Value, "")
        txtCity.Text = If(City.Same, City.Value, "")
        cmbCountryCode.Text = If(CountryCode.Same, CountryCode.Value, "")
        txtCountry.Text = If(Country.Same, Country.Value, "")
        txtProvince.Text = If(Province.Same, Province.Value, "")
        txtSublocation.Text = If(Sublocation.Same, Sublocation.Value, "")
        txtEditStatus.Text = If(EditStatus.Same, EditStatus.Value, "")
        nudUrgency.Text = If(Urgency.Same, Urgency.Value.ToString, "") 'TODO: Does it work?
        txtObjectName.Text = If(ObjectName.Same, ObjectName.Value, "")
        txtCaption.Text = If(Caption.Same, Caption.Value, "")
        kweKeywords.KeyWords.Clear()
        kweKeywords.KeyWords.AddRange(Keywords)
    End Sub
    ''' <summary>Contains value of the <see cref="Changed"/> property</summary>
    Private _Changed As Boolean
    ''' <summary>Gets value indicating if there is any unsaved change</summary>
    Public ReadOnly Property Changed() As Boolean
        Get
            Return _Changed
        End Get
    End Property
    Private Sub IPTC_ValueChanged(ByVal sender As IPTCInternal, ByVal e As EventArgs)
        If Not ChangedIPTCs.Contains(sender) Then ChangedIPTCs.Add(sender)
        If Not Changed Then Me.Text = String.Format("{0} {1} *", My.Application.Info.Title, My.Application.Info.Version)
        _Changed = True
        lvwImages.Items(sender.ImagePath).Text = System.IO.Path.GetFileName(sender.ImagePath) & "*"
    End Sub

    Private Sub cmdErrInfo_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdErrInfo.Click
        WindowsT.IndependentT.MessageBox.Error(sender.tag)
    End Sub

    Private Sub llbLarge_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbLarge.LinkClicked
        showlarge()
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
            showlarge()
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
    End Class
End Class