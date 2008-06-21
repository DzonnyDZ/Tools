Imports System.ComponentModel, System.Threading
''' <summary>FileSystemWtacher dopln�n� o synchronn� ud�losti</summary>
Public Class SyncFSWatcher
    Inherits IO.FileSystemWatcher
    ''' <summary>Asynchronn� operace pro CallBack z ud�lost� FS monitoru</summary>
    Private asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(Nothing)
#Region "Delegates"
    ''' <summary>Deleg�t na metodu OnDeleted pro CallBack z ud�losti</summary>
    Private dDeleted As New SendOrPostCallback(AddressOf OnDeletedSync)
    ''' <summary>Deleg�t na metodu OnDeleted pro CallBack z ud�losti</summary>
    Private dChanged As New SendOrPostCallback(AddressOf OnChangedSync)
    ''' <summary>Deleg�t na metodu OnDeleted pro CallBack z ud�losti</summary>
    Private dRenamed As New SendOrPostCallback(AddressOf OnRenamedSync)
    ''' <summary>Deleg�t na metodu OnCreated pro CallBack z ud�losti</summary>
    Private dCreated As New SendOrPostCallback(AddressOf OnCreatedSync)
#End Region
#Region "Events"
    ''' <summary>Occurs when a file or directory in the specified SyncFSWatcher.Path is created</summary>
    Public Event CreatedSync(ByVal sender As SyncFSWatcher, ByVal e As System.IO.FileSystemEventArgs)
    ''' <summary>Occurs when a file or directory in the specified SyncFSWatcher.Path is deleted.</summary>
    Public Event DeletedSync(ByVal sender As SyncFSWatcher, ByVal e As System.IO.FileSystemEventArgs)
    ''' <summary>Occurs when a file or directory in the specified SyncFSWatcher.Path is renamed.</summary>
    Public Event RenamedSync(ByVal sender As SyncFSWatcher, ByVal e As System.IO.RenamedEventArgs)
    ''' <summary>Occurs when a file or directory in the specified System.IO.FileSystemWatcher.Path is changed</summary>
    Public Event ChangedSync(ByVal sender As SyncFSWatcher, ByVal e As System.IO.FileSystemEventArgs)
#End Region
#Region "CTors"
    ''' <summary>Initializes a new instance of the SyncFSWatcher class.</summary>
    Public Sub New()
        MyBase.New()
    End Sub
    ''' <summary>Initializes a new instance of the SyncFSWatcher class, given the specified directory to monitor.</summary>
    ''' <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation.</param>
    Public Sub New(ByVal path As String)
        MyBase.New(path)
    End Sub
    ''' <summary>Initializes a new instance of the SyncFSWatcher class, given the specified directory and type of files to monitor.</summary>
    ''' <param name="filter">The type of files to watch. For example, "*.txt" watches for changes to all text files.</param>
    ''' <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation.</param>
    Public Sub New(ByVal path As String, ByVal filter As String)
        MyBase.New(path, filter)
    End Sub
#End Region
#Region "FileSystemWatcher event handlers"
    ''' <remarks>Nast�v� v jin�m threadu, nutno prov�st CallBack</remarks>
    Private Sub fswFile_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles MyBase.Created
        OnCreatedAsync(e)
    End Sub
    ''' <remarks>Nast�v� v jin�m threadu, nutno prov�st CallBack</remarks>
    Private Sub fswFile_Deleted(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles MyBase.Deleted
        OnDeletedAsync(e)
    End Sub
    ''' <remarks>Nast�v� v jin�m threadu, nutno prov�st CallBack</remarks>
    Private Sub fswFile_Changed(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles MyBase.Changed
        OnChangedAsync(e)
    End Sub
    ''' <remarks>Nast�v� v jin�m threadu, nutno prov�st CallBack</remarks>
    Private Sub fswFile_Renamed(ByVal sender As Object, ByVal e As System.IO.RenamedEventArgs) Handles MyBase.Renamed
        OnRenamedAsync(e)
    End Sub
#End Region
#Region "Async overridable methods"
    ''' <summary>Asynchronn� metoda vol� synchronn� metodu OnDeletedSync</summary>
    ''' <param name="e">A System.IO.FileSystemEventArgs that contains the event data.</param>
    Protected Overridable Sub OnDeletedAsync(ByVal e As IO.FileSystemEventArgs)
        asyncOp.Post(dDeleted, e)
    End Sub
    ''' <summary>Asynchronn� metoda vol� synchronn� metodu OnRenamedSync</summary>
    ''' <param name="e">A IO.RenamedEventArgs that contains the event data.</param>
    Protected Overridable Sub OnRenamedAsync(ByVal e As IO.RenamedEventArgs)
        asyncOp.Post(dRenamed, e)
    End Sub
    ''' <summary>Asynchronn� metoda vol� synchronn� metodu OnCreatedSync</summary>
    ''' <param name="e">A IO.FileSystemEventArgs that contains the event data.</param>
    Protected Overridable Sub OnCreatedAsync(ByVal e As IO.FileSystemEventArgs)
        asyncOp.Post(dCreated, e)
    End Sub
    ''' <summary>Asynchronn� metoda vol� synchronn� metodu OnChangedSync</summary>
    ''' <param name="e">A IO.FileSystemEventArgs that contains the event data.</param>
    Protected Overridable Sub OnChangedAsync(ByVal e As IO.FileSystemEventArgs)
        asyncOp.Post(dChanged, e)
    End Sub
#End Region
#Region "Sync overridable methods"
    ''' <summary>Vyvol�v� ud�lost DeletedSync</summary>
    ''' <param name="arg">A IO.FileSystemEventArgs that contains the event data.</param>
    Protected Overridable Sub OnDeletedSync(ByVal arg As Object)
        RaiseEvent DeletedSync(Me, arg)
    End Sub
    ''' <summary>Vyvol�v� ud�lost RenamedSync</summary>
    ''' <param name="arg">A IO.RenamedEventArgs that contains the event data.</param>
    Protected Overridable Sub OnRenamedSync(ByVal arg As Object)
        RaiseEvent RenamedSync(Me, arg)
    End Sub
    ''' <summary>Vyvol�v� ud�lost ChangedSync</summary>
    ''' <param name="arg">A IO.FileSystemEventArgs that contains the event data.</param>
    Protected Overridable Sub OnChangedSync(ByVal arg As Object)
        RaiseEvent ChangedSync(Me, arg)
    End Sub
    ''' <summary>Vyvol�v� ud�lost CreatedSync</summary>
    ''' <param name="arg">A IO.FileSystemEventArgs that contains the event data.</param>
    Protected Overridable Sub OnCreatedSync(ByVal arg As Object)
        RaiseEvent CreatedSync(Me, arg)
    End Sub
#End Region
End Class