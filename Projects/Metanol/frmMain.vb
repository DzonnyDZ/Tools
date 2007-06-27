Public Class frmMain
    'ASAP:Comments
    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
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
    ''' <summary>Loads and shows content of folder</summary>
    Private Sub LoadFolder()
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
        For Each Path As String In DirectCast(e.Argument, List(Of String))
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
            g.DrawImage(NewImage, CSng(ThSize.Width / 2 - NewSize.Width / 2), CSng(ThSize.Height / 2 - NewSize.Height / 2))
            g.Flush(Drawing2D.FlushIntention.Sync)
            If th IsNot Nothing Then
                Me.Invoke(d, Path, NewImage)
            End If
            If bgwThumb.CancellationPending Then
                e.Cancel = True
                Exit Sub
            End If
        Next Path
    End Sub
    Private Function CancelThumb() As Boolean
        Return bgwThumb.CancellationPending
    End Function
    Private Delegate Sub dSetThumb(ByVal Path As String, ByVal Image As Image)
    Private Sub SetThumb(ByVal Path As String, ByVal Image As Image)
        imlImages.Images.Add(Path, Image)
        lvwImages.Items(Path).ImageKey = Path
    End Sub
    'TODO:Extract as tool
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
        My.Settings.Folder = txtPath.Text
        LoadFolder()
    End Sub

    Private Sub txtPath_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPath.KeyDown
        If e.KeyCode = Keys.Return Then
            cmdGo.PerformClick()
        End If
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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
        My.Settings.Folder = lvwFolder.SelectedItems(0).Name
        LoadFolder()
    End Sub
    Private Sub LoadSettings()
        Me.Location = My.Settings.MainLocation
        Me.Size = My.Settings.MainSize
        Me.WindowState = My.Settings.MainState
        If Me.WindowState = FormWindowState.Normal Then
            Dim Intersects As Boolean = False
            For Each scr As Screen In Screen.AllScreens
                If scr.Bounds.IntersectsWith(Me.DesktopBounds) Then
                    Intersects = True
                    Exit For
                End If
            Next scr
            If Not Intersects Then Me.DesktopLocation = My.Computer.Screen.Bounds.Location
        End If
        splMain.SplitterDistance = My.Settings.MainSplitter
        splFolder.SplitterDistance = My.Settings.LeftSplitter
    End Sub
    Private Sub StoreSettings()
        My.Settings.MainLocation = Me.Location
        My.Settings.MainSize = Me.Size
        My.Settings.MainState = Me.WindowState
        My.Settings.MainSplitter = splMain.SplitterDistance
        My.Settings.LeftSplitter = splFolder.SplitterDistance
    End Sub


End Class
