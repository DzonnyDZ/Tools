Imports System.ComponentModel, System.Threading
Imports System.Drawing
Imports Tools.ComponentModelT

#If Config <= Nightly Then                    'Stage: Nightly
Namespace IOt
    ''' <summary><see cref="IO.FileSystemWatcher"/> with synchronous events added</summary>
    ''' <seelaso cref="IO.FileSystemWatcher"/>
    <DefaultEvent("ChangedSync"), ToolboxBitmap(GetType(IO.FileSystemWatcher))> _
    Public Class SyncFSWatcher
        Inherits IO.FileSystemWatcher
        ''' <summary>And assynchronous operation for callback from <see cref="IO.FileSystemWatcher"/></summary>
        Private asyncOp As AsyncOperation = AsyncOperationManager.CreateOperation(Nothing)
#Region "Delegates"
        ''' <summary>Delegate of the <see cref="OnDeleted"/> method for event callback</summary>
        Private dDeleted As New SendOrPostCallback(AddressOf OnDeletedSync)
        ''' <summary>Delegate of the <see cref="OnChanged"/> method for event callback</summary>
        Private dChanged As New SendOrPostCallback(AddressOf OnChangedSync)
        ''' <summary>Delegate of the <see cref="OnRenamed"/> method for event callback</summary>
        Private dRenamed As New SendOrPostCallback(AddressOf OnRenamedSync)
        ''' <summary>Delegate of the <see cref="OnCreated"/> method for event callback</summary>
        Private dCreated As New SendOrPostCallback(AddressOf OnCreatedSync)
#End Region
#Region "Events"
        ''' <summary>Occurs when a file or directory in the specified <see cref="Path"/> is created</summary>
        <LDescription(GetType(ResourcesT.Components), "CreatedSync_d")> _
        Public Event CreatedSync As IO.FileSystemEventHandler
        ''' <summary>Occurs when a file or directory in the specified <see cref="Path"/> is deleted.</summary>
        <LDescription(GetType(ResourcesT.Components), "DeletedSync_d")> _
        Public Event DeletedSync As IO.FileSystemEventHandler
        ''' <summary>Occurs when a file or directory in the specified <see cref="Path"/> is renamed.</summary>
        <LDescription(GetType(ResourcesT.Components), "RenamedSync_d")> _
        Public Event RenamedSync As IO.RenamedEventHandler
        ''' <summary>Occurs when a file or directory in the specified <see cref="Path"/> is changed</summary>
        <LDescription(GetType(ResourcesT.Components), "ChangedSync_d")> _
        Public Event ChangedSync As IO.FileSystemEventHandler
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
        ''' <remarks>Occurs in another thread, callback needed</remarks>
        Private Sub fswFile_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles MyBase.Created
            OnCreatedAsync(e)
        End Sub
        ''' <remarks>Occurs in another thread, callback needed</remarks>
        Private Sub fswFile_Deleted(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles MyBase.Deleted
            OnDeletedAsync(e)
        End Sub
        ''' <remarks>Occurs in another thread, callback needed</remarks>
        Private Sub fswFile_Changed(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles MyBase.Changed
            OnChangedAsync(e)
        End Sub
        ''' <remarks>Occurs in another thread, callback needed</remarks>
        Private Sub fswFile_Renamed(ByVal sender As Object, ByVal e As System.IO.RenamedEventArgs) Handles MyBase.Renamed
            OnRenamedAsync(e)
        End Sub
#End Region
#Region "Async overridable methods"
        ''' <summary>Asynchronous method calls synchronous method method <see cref="OnDeletedSync"/></summary>
        ''' <param name="e">A <see cref="System.IO.FileSystemEventArgs"/> that contains the event data.</param>
        Protected Overridable Sub OnDeletedAsync(ByVal e As IO.FileSystemEventArgs)
            asyncOp.Post(dDeleted, e)
        End Sub
        ''' <summary>Asynchronous method calls synchronous method method <see cref="OnRenamedSync"/></summary>
        ''' <param name="e">A <see cref="IO.RenamedEventArgs"/> that contains the event data.</param>
        Protected Overridable Sub OnRenamedAsync(ByVal e As IO.RenamedEventArgs)
            asyncOp.Post(dRenamed, e)
        End Sub
        ''' <summary>Asynchronous method calls synchronous method method <see cref="OnCreatedSync"/></summary>
        ''' <param name="e">A <see cref="IO.FileSystemEventArgs"/> that contains the event data.</param>
        Protected Overridable Sub OnCreatedAsync(ByVal e As IO.FileSystemEventArgs)
            asyncOp.Post(dCreated, e)
        End Sub
        ''' <summary>Asynchronous method calls synchronous method method <see cref="OnChangedSync"/></summary>
        ''' <param name="e">A <see cref="IO.FileSystemEventArgs"/> that contains the event data.</param>
        Protected Overridable Sub OnChangedAsync(ByVal e As IO.FileSystemEventArgs)
            asyncOp.Post(dChanged, e)
        End Sub
#End Region
#Region "Sync overridable methods"
        ''' <summary>Raises the <see cref="DeletedSync"/> event</summary>
        ''' <param name="e">A <see cref="IO.FileSystemEventArgs"/> that contains the event data.</param>
        Protected Overridable Sub OnDeletedSync(ByVal e As IO.FileSystemEventArgs)
            RaiseEvent DeletedSync(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="RenamedSync"/> event</summary>
        ''' <param name="e">A <see cref="IO.RenamedEventArgs"/> that contains the event data.</param>
        Protected Overridable Sub OnRenamedSync(ByVal e As IO.RenamedEventArgs)
            RaiseEvent RenamedSync(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="ChangedSync"/> event</summary>
        ''' <param name="e">A <see cref="IO.FileSystemEventArgs"/> that contains the event data.</param>
        Protected Overridable Sub OnChangedSync(ByVal e As IO.FileSystemEventArgs)
            RaiseEvent ChangedSync(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="CreatedSync"/> event</summary>
        ''' <param name="e">A <see cref="IO.FileSystemEventArgs"/> that contains the event data.</param>
        Protected Overridable Sub OnCreatedSync(ByVal e As IO.FileSystemEventArgs)
            RaiseEvent CreatedSync(Me, e)
        End Sub
#End Region
    End Class
End Namespace
#End If