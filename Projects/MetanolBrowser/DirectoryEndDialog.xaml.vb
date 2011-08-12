Imports Tools.ExtensionsT, Tools.LinqT
Imports mBox = Tools.WindowsT.IndependentT.MessageBox

''' <summary>A diallog used to select next folder to use</summary>
Public Class DirectoryEndDialog

    ''' <summary>CTor - creates a new instance of the <see cref="DirectoryEndDialog"/> class</summary>
    ''' <param name="currentFolder">Current folder to start browsidng with</param>
    ''' <param name="direction">Browsing direction</param>
    Public Sub New(currentFolder$, direction%)
        If direction.NotIn(-1, 1) Then Throw New ArgumentException(My.Resources.ex_1orMinus1, "direction")
        InitializeComponent()
        Folder = currentFolder
        If direction = -1 Then txbI1.Text = My.Resources.iq_BeginningOfFolderReached
    End Sub
    Private Sub DirectoryEndDialog_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        If Not LoadFolder(Folder) Then
            If Not LoadFolder(My.Computer.FileSystem.SpecialDirectories.MyPictures) Then
                Me.DialogResult = False
                Me.Close()
            End If
        End If
    End Sub

#Region "Folder"
    ''' <summary>Gets or sets path of current folder</summary>      
    Public Property Folder As String
        Get
            Return GetValue(FolderProperty)
        End Get
        Set(ByVal value As String)
            SetValue(FolderProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="Folder"/> dependency property</summary>                                                   
    Public Shared ReadOnly FolderProperty As DependencyProperty = DependencyProperty.Register(
        "Folder", GetType(String), GetType(DirectoryEndDialog), New FrameworkPropertyMetadata(Nothing))
#End Region

    ''' <summary>Loads subfolders of current folder</summary>
    ''' <param name="folder">Folder to lod subfolders of</param>
    ''' <returns>True if folder was loaded successfully, false otherwise</returns>
    Private Function LoadFolder(folder As String) As Boolean
        If folder <> "" AndAlso Not IO.Directory.Exists(folder) Then Return False
        Dim subdirs As String() = Nothing
        If folder <> "" Then
            Try
                subdirs = IO.Directory.GetDirectories(folder)
            Catch ex As Exception
                mBox.Error_XW(ex, Me)
                Return False
            End Try
        End If
        Me.Folder = folder

        Dim current = If(folder <> "", New With {Key .DisplayName = My.Resources.txt_ThisFolder, Key .Path = folder}, Nothing)
        Dim parent = If(folder = "",
                         Nothing,
                     If(IO.Path.GetPathRoot(folder).ToLowerInvariant = folder.ToLowerInvariant,
                         New With {Key .DisplayName = My.Resources.txt_FolderUp, Key .Path = ""},
                         New With {Key .DisplayName = My.Resources.txt_FolderUp, Key .Path = IO.Path.GetDirectoryName(folder)}
                     ))


        Dim all = {current, parent}.UnionAll(
            If(folder = "",
                (From disk In My.Computer.FileSystem.Drives Select DisplayName = disk.Name, Path = disk.Name),
                (From sd In subdirs Select DisplayName = IO.Path.GetFileName(sd), Path = sd)
        )).Where(Function(i) i IsNot Nothing)

        lstFolders.ItemsSource = all
        If all.Any Then lstFolders.SelectedIndex = 0

        Return True
    End Function


    Private Sub lstFolders_PreviewKeyDown(sender As System.Object, e As System.Windows.Input.KeyEventArgs) Handles lstFolders.PreviewKeyDown
        If e.Handled Then Return
        Select Case e.Key
            Case Key.Right, Key.Space : GoSelectedFolder()
            Case Key.Left, Key.Back : GoUp()
            Case Else : Return
        End Select
        e.Handled = True
    End Sub

    ''' <summary>Loads one folder up</summary>
    Private Sub GoUp()
        If Folder = "" OrElse Folder.ToLowerInvariant = IO.Path.GetPathRoot(Folder).ToLowerInvariant Then Return
        LoadFolder(IO.Path.GetDirectoryName(Folder))
    End Sub

    ''' <summary>Loads highlighted folder</summary>
    Private Sub GoSelectedFolder()
        If lstFolders.SelectedIndex >= 0 Then
            LoadFolder(lstFolders.SelectedValue)
        End If
    End Sub

    Private Sub btnOK_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnOK.Click
        If lstFolders.SelectedIndex >= 0 AndAlso DirectCast(lstFolders.SelectedValue, String) <> "" Then
            Folder = lstFolders.SelectedValue
            Me.DialogResult = True
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnCancel.Click
        DialogResult = False
        Me.Close()
    End Sub

    Private Sub lstFolders_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles lstFolders.MouseDoubleClick
        If lstFolders.SelectedIndex >= 0 Then LoadFolder(lstFolders.SelectedValue)
    End Sub
End Class
