Imports System.Linq, Tools.DrawingT, Tools.DataStructuresT.GenericT, Tools.CollectionsT.SpecializedT, Tools.IOt.FileTystemTools
Imports System.ComponentModel

Public Class frmMain
    Private Const UpKey As String = ".\.."
    Public Sub New()
        InitializeComponent()
        Me.Text = String.Format("{0} {1}", My.Application.Info.Title, My.Application.Info.Version)
        imlFolders.Images.Add(UpKey, My.Resources.Up)
        imlImages.ImageSize = My.Settings.ThumbSize
    End Sub
    Private CurrentFolder$

    Private Sub tmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiExit.Click
        Me.Close()
    End Sub

    Private Sub tmiBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiBrowse.Click
        If fbdGoTo.ShowDialog = Windows.Forms.DialogResult.OK Then _
            LoadFolder(fbdGoTo.SelectedPath)
    End Sub
    Private LoadOnCancel As IOt.Path = Nothing
    Private Sub LoadFolder(ByVal Path As IOt.Path)
        If bgwImages.IsBusy Then
            bgwImages.CancelAsync()
            LoadOnCancel = Path
            Exit Sub
        End If
        CurrentFolder = Path
        tslFolder.Text = Path
        lvwFolders.Items.Clear()
        Dim subfolders As IEnumerable(Of IOt.Path)
        Try
            If Path.DirectoryName <> "" Then
                lvwFolders.Items.Add("...", UpKey).Tag = Path.DirectoryName
            End If
        Catch : End Try
        imlFolders.Images.Clear()
        imlFolders.Images.Add(UpKey, My.Resources.Up)
        Try
            subfolders = From sf In Path.GetChildren(Function(p As IOt.Path) p.IsDirectory OrElse p.Extension.Equals(".lnk", StringComparison.InvariantCultureIgnoreCase)) Order By sf.FileName
            For Each subfolder In subfolders
                Dim icon = subfolder.GetIcon
                If icon IsNot Nothing Then imlFolders.Images.Add(subfolder.FileName, icon)
                lvwFolders.Items.Add(subfolder.FileName, subfolder.FileName).Tag = subfolder.Path
            Next
        Catch : End Try
        imlImages.Images.Clear()
        lvwImages.Items.Clear()
        Dim ImagesToLoad As New List(Of String)
        Try
            For Each file In From f In Path.GetFiles("*.jpg", "*.jpeg") Order By f.FileName
                lvwImages.Items.Add(file.FileName, file.FileName).Tag = file.Path
                ImagesToLoad.Add(file.Path)
            Next
        Catch : End Try
        bgwImages.RunWorkerAsync(ImagesToLoad)
    End Sub

    Private Sub frmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        My.Settings.Folder = CurrentFolder
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If My.Settings.Folder = "" Then
            LoadFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures))
        Else
            LoadFolder(My.Settings.Folder)
        End If
    End Sub

    Private bgThread As Threading.Thread

    Private Sub bgwImages_DoWork(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwImages.DoWork
        bgThread = Threading.Thread.CurrentThread
        Try
            Dim Paths As List(Of String) = e.Argument
            Dim i As Integer = 0
            For Each path In Paths
                i += 1
                Try
                    Dim img As New Bitmap(path)
                    Dim thimg = img.GetThumbnail(My.Settings.ThumbSize, Color.Transparent, Function() sender.CancellationPending)
                    bgwImages.ReportProgress(i / Paths.Count * 100, New Pair(Of String, Image)(System.IO.Path.GetFileName(path), thimg))
                    If bgwImages.CancellationPending Then e.Cancel = True : Exit Sub
                Catch : End Try
            Next
            bgwImages.ReportProgress(100)
        Finally
            bgThread = Nothing
        End Try
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
            LoadFolder(loc)
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
End Class
