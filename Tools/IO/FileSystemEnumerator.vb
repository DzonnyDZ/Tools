#If True
Imports Tools.CollectionsT.GenericT, Tools.DataStructuresT.GenericT
'#If Framework >= 3.5 Then
Imports System.Linq
'#End If
Namespace IOt
    'ASAP: Forum, Wiki, Mark
    ''' <summary>Enumerates through files and folders within specified folder</summary>
    Public Class FileSystemEnumerator
        Implements IEnumerator(Of String), IEnumerator(Of Path)
        ''' <summary>Folder to start enumeration with. Used to initialize the enumerator.</summary>
        Protected ReadOnly Root As String

        ''' <summary>Levels of recursive enumeration in file system</summary>
        Private Levels As Stack(Of IEnumerator(Of String))

        ''' <summary>Contains value of the <see cref="FoldersFirst"/> property</summary>
        Private _FoldersFirst As Boolean
        ''' <summary>Gets value idication if folders are listed before files</summary>
        Public ReadOnly Property FoldersFirst() As Boolean
            <DebuggerStepThrough()> Get
                Return _FoldersFirst
            End Get : End Property
        ''' <summary>CTor</summary>
        ''' <param name="Root">The folder to start enumeration with. This folder is not included in list. All files and folders in it are recursivelly included.</param>
        ''' <param name="FoldersFirst">True to enlist folders and its content before files of each current folder</param>
        Public Sub New(ByVal Root As String, Optional ByVal FoldersFirst As Boolean = False)
            Me._FoldersFirst = FoldersFirst
            Me.Root = Root
            Reset()
        End Sub

        ''' <summary>Gets string that represents current file or folder enumerator points to.</summary>
        ''' <returns>String that represents full path of file or folder enumerator points to</returns>
        ''' <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
        Public Overridable ReadOnly Property Current() As String Implements System.Collections.Generic.IEnumerator(Of String).Current
            Get
                'If Folders Is Nothing OrElse Folders.Count = 0 Then Throw New InvalidOperationException("Enumerator has not been initialized or it already has enumerated all the items")
                'Return _Current
                If _Current IsNot Nothing Then
                    Return _Current
                Else
                    Throw New InvalidOperationException(ResourcesT.Exceptions.TheEnumerationHasNotStartedYetOrHasAlreadyFinished)
                End If
            End Get
        End Property
        ''' <summary>Gets <see cref="Path"/> that represents current file or folder enumerator points to.</summary>
        ''' <returns><see cref="Path"/> that represents full path of file or folder enumerator points to</returns>
        ''' <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
        Public Overridable ReadOnly Property CurrentPath() As Path Implements System.Collections.Generic.IEnumerator(Of Path).Current
            Get
                Return New Path(Current)
            End Get
        End Property
        ''' <summary>Same as <see cref="Current"/>, but type unsafe</summary>
        ''' <returns><see cref="Current"/></returns>
        ''' <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
        ''' <remarks>Use type-safe <see cref="Current"/> or <see cref="CurrentPath"/> instead</remarks>
        <Obsolete("Use type-safe Current or CurrentPath instead")> _
        Private ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Current
            End Get
        End Property
        ''' <summary>Gets curent level of recursion (0-based)</summary>
        ''' <returns>Current level of recursion. -1 if enumerator is either before first or after last item.</returns>
        Public Overridable ReadOnly Property CurrentLevel() As Integer
            Get
                Return Levels.Count - If(CurrentPath.IsDirectory, 2, 1)
            End Get
        End Property

        ''' <summary>Stores current path as string. If null, enumeration has not been initialized yer or has already finished</summary>
        Private _Current As String

        ''' <summary>Advances the enumerator to the next file or folder.</summary>
        ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has alredy enumerated all the files and folders.</returns>
        Public Overridable Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            If Levels.Count <= 0 Then
                _Current = Nothing
                Return False
            ElseIf Levels.Peek.MoveNext Then
                _Current = Levels.Peek.Current
                If IO.Directory.Exists(_Current) Then
                    Levels.Push(GetEnumeratorForFolder(_Current))
                End If
                Return True
            Else
                Levels.Pop()
                Return MoveNext()
            End If
        End Function

        ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        Public Overridable Sub Reset() Implements System.Collections.IEnumerator.Reset
            Levels = New Stack(Of IEnumerator(Of String))
            Levels.Push(GetEnumeratorForFolder(Root))
            _Current = Nothing
        End Sub
        ''' <summary>Returns <see cref="IEnumerator(Of String)"/> that enumerates files and folders in given directory in order depending on <see cref="FoldersFirst"/>.</summary>
        ''' <param name="Path">Directory to get files and folders for</param>
        Protected Overridable Function GetEnumeratorForFolder(ByVal Path As String) As IEnumerator(Of String)
            Dim Files As IEnumerator(Of String)
            Try : Files = IO.Directory.GetFiles(Path).GetTypedEnumerator()
            Catch ex As Exception When TypeOf ex Is IO.DirectoryNotFoundException OrElse TypeOf ex Is IO.PathTooLongException OrElse TypeOf ex Is UnauthorizedAccessException
                Files = New String() {}.GetTypedEnumerator
            End Try
            Dim Folders As IEnumerator(Of String)
            Try : Folders = IO.Directory.GetDirectories(Path).GetTypedEnumerator()
            Catch ex As Exception When TypeOf ex Is IO.DirectoryNotFoundException OrElse TypeOf ex Is IO.PathTooLongException OrElse TypeOf ex Is UnauthorizedAccessException
                Folders = New String() {}.GetTypedEnumerator
            End Try
            Dim First As IEnumerator(Of String)
            Dim Second As IEnumerator(Of String)
            If FoldersFirst Then
                First = Folders
                Second = Files
            Else
                First = Files
                Second = Folders
            End If
            Return New UnionEnumerator(Of String)(First, Second)
        End Function

#Region " IDisposable Support "
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False        ' IDisposable
        ''' <summary>Implements <see cref="IDisposable.Dispose"/>'s logic</summary>
        ''' <param name="disposing">Set to true by <see cref="Dispose"/></param>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If Levels IsNot Nothing Then
                        While Levels.Count > 0
                            Levels.Peek.Dispose()
                            Levels.Pop()
                        End While
                        Levels = Nothing
                    End If
                End If
            End If
            Me.disposedValue = True
        End Sub

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <remarks>Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.</remarks>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
    'ASAP: Mark, wiki, forum
    ''' <summary>Implements enumerator through file system with generic call-back filter</summary>
    Public Class FilteredFileSystemEnumerator : Inherits FileSystemEnumerator
        ''' <summary>Contains value of the <see cref="Filter"/> property</summary>
        Private _Filter As Func(Of String, FilterReasons, Boolean)
        ''' <summary>Filter function called when item is about to be included in output.</summary>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        ''' <exception cref="InvalidOperationException">Trying to set this property when it is not null</exception>
        ''' <remarks>This property can be only set when it is null</remarks>
        Protected Property Filter() As Func(Of String, FilterReasons, Boolean)
            <DebuggerStepThrough()> Get
                Return _Filter
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Func(Of String, FilterReasons, Boolean))
                If value Is Nothing Then Throw New ArgumentNullException("value")
                If _Filter IsNot Nothing Then Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.Property0HasAlreadyBeenSetAndItCannotBeChanged, "Filter"))
                _Filter = value
            End Set
        End Property
        ''' <summary>Defines reasons for calling filter function</summary>
        Public Enum FilterReasons
            ''' <summary>Reason is that base class is about to get known about the file or folder</summary>
            ''' <remarks>
            ''' If you want folder to be went through but do not want them to be enlisted, return true. Return False for all files that you want exclude from listing.
            ''' Filter with this argument is called by <see cref="FilteredEnumerator(Of String).MoveNext"/> created as wrapper of result of <see cref="FileSystemEnumerator.GetEnumeratorForFolder"/>. Items filtered by this call are skipped and it it is folder it is not entered and its content is skipped to.
            ''' </remarks>
            See
            ''' <summary>Reason is that base class is about to make the item current item (user called <see cref="MoveNext"/>).</summary>
            ''' <remarks>
            ''' There is no reason to return false for files in this situation since all files should be filtered on <see cref="See"/>.
            ''' Return false for any folder you want to enlist content but not enlist folder itself.
            ''' Filter with this argument is called by <see cref="MoveNext"/> function and items that does not pass it are skipped in output. But folders are entered and their content is listed (it it pass the filter).
            ''' </remarks>
            Show
        End Enum
        ''' <summary>CTor</summary>
        ''' <param name="Root">Folder to start enumeration with. This folder's name is not included in list.</param>
        ''' <param name="Filter">Filter function that is called for all files and folders when they are about to be included in output list. The function must return True for items that should be included.</param>
        ''' <param name="FoldersFirst">True if folders should be enlisted recursively before files or current folder.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Filter"/> is null</exception>
        Public Sub New(ByVal Root As String, ByVal Filter As Func(Of String, FilterReasons, Boolean), Optional ByVal FoldersFirst As Boolean = False)
            MyBase.New(Root, FoldersFirst)
            Me.Filter = Filter
        End Sub

        ''' <summary>CTor without filter</summary>
        ''' <param name="Root">Folder to start enumeration with. This folder's name is not included in list.</param>
        ''' <param name="FoldersFirst">True if folders should be enlisted recursively before files or current folder.</param>
        ''' <remarks>When using this constructor, you must set the <see cref="Filter"/> property as soon as possible</remarks>
        Protected Sub New(ByVal Root As String, ByVal FoldersFirst As Boolean)
            MyBase.New(Root, FoldersFirst)
        End Sub
        ''' <summary>Returns <see cref="IEnumerator(Of String)"/> that enumerates files and folders in given directory in order depending on <see cref="FoldersFirst"/>. Files and folders that does not pass filer are excluded from enumerator (excluded folders are not entered).</summary>
        ''' <param name="Path">Directory to get files and folders for</param>
        Protected NotOverridable Overrides Function GetEnumeratorForFolder(ByVal Path As String) As System.Collections.Generic.IEnumerator(Of String)
            Return New FilteredEnumerator(Of String)(MyBase.GetEnumeratorForFolder(Path), Function(Item As String) Filter(Item, FilterReasons.See))
        End Function
        ''' <summary>Advances the enumerator to the next file or folder. Files and folders that does not pass the filer as skipped, but folders are entered.</summary>
        ''' <returns>True if the enumerator was successfully advanced to the next element; false if the enumerator has alredy enumerated all the files and folders.</returns>
        Public NotOverridable Overrides Function MoveNext() As Boolean
            While MyBase.MoveNext
                If Filter.Invoke(MyBase.Current, FilterReasons.Show) Then Return True
            End While
            Return False
        End Function
    End Class
    'ASAP: Mark, Wiki, Forum
    ''' <summary>Recursive enumerator of files and folder filtered by masks</summary>
    ''' <remarks>Allows to list only files of all folders. Filter separatelly files, folders to be listed and folders to be browsed.</remarks>
    Public Class FileSystemEnumeratorWithMask : Inherits FilteredFileSystemEnumerator
        ''' <summary>List of masks</summary>
        Private Masks As IEnumerable(Of String)
        ''' <summary>Contains value of the <see cref="FoldersListMasks"/> property</summary>
        Private _FoldersListMasks As String() = {}
        ''' <summary>Cont value of the <see cref="FoldersEnterMasks"/> property</summary>
        Private _FoldersEnterMasks As String() = {"*"}
        ''' <summary>Special masks for folders to list in output</summary>
        ''' <remarks>Default value is an empty array. So, by default folders are not listed.</remarks>
        ''' <value>Array of masks in format used by Visual Basic Like operator (wildchars ? and *)</value>
        ''' <exception cref="ArgumentNullException">Value beign set is null</exception>
        Public Property FoldersListMasks() As String()
            Set(ByVal value As String())
                If value Is Nothing Then Throw New ArgumentNullException("value")
                _FoldersListMasks = value
            End Set
            Get
                Return _FoldersListMasks
            End Get
        End Property
        '#If Framework >= 3.5 Then
        ''' <summary>Gets or sets array masks used for files</summary>
        ''' <value>Array of masks used for files. Masks are in formet used by Visual Basic Like operator (wildchars * and ?)</value>
        ''' <returns>Array of maks. Getter can change actual instance stored in masks. Avoid using getter unless it is necessary. Inheritors can use <see cref="MaskForFiles"/>.</returns>
        ''' <exception cref="ArgumentNullException">Value beign set is null</exception>
        Public Property FilesMasks() As String()
            Get
                Dim arr = Masks.ToArray
                Masks = arr
                Return arr
            End Get
            '#Else
            '    ''' <summary>Gets or sets array masks used for files</summary>
            '    ''' <value>Array of masks used for files. Masks are in formet used by Visual Basic Like operator (wildchars * and ?)</value>
            '    ''' <exception cref="ArgumentNullException">Value beign set is null</exception>
            'Public WriteOnly Property FilesMasks() As String()
            '#End If
            Set(ByVal value As String())
                If value Is Nothing Then Throw New ArgumentNullException("value")
                Masks = value
            End Set
        End Property
        ''' <summary>Returns masks for files</summary>
        ''' <returns>Use <see cref="FilesMasks"/> in order to set this property</returns>
        Protected ReadOnly Property MaskForFiles() As IEnumerable(Of String)
            Get
                Return Masks
            End Get
        End Property


        ''' <summary>Special masks for folders to list their content</summary>
        ''' <value>Array of masks in format used by Visual Basic Like operator (wildchars ? and *)</value>
        ''' <exception cref="ArgumentNullException">Value beign set is null</exception>
        Public Property FoldersEnterMasks() As String()
            Get
                Return _FoldersEnterMasks
            End Get
            Set(ByVal value As String())
                If value Is Nothing Then Throw New ArgumentNullException("value")
                _FoldersEnterMasks = value
            End Set
        End Property
        ''' <summary>CTor from root and masks as <see cref="IEnumerable(Of String)"/></summary>
        ''' <param name="Root">Folder to start enumeration with. The folder itself is not included, but its content is.</param>
        ''' <param name="Masks">List of masks for files. Each mask is in format used by Visual Basic Like operator (wildchars * and ?).</param>
        ''' <param name="FoldersFirst"></param>
        Public Sub New(ByVal Root As String, ByVal Masks As IEnumerable(Of String), Optional ByVal FoldersFirst As Boolean = False)
            MyBase.New(Root, FoldersFirst)
            MyBase.Filter = AddressOf Filter
            If Masks Is Nothing Then Throw New ArgumentException("Masks")
            Me.Masks = Masks
        End Sub
        ''' <summary>CTor from root and array of masks</summary>
        ''' <param name="Root">Folder to start enumeration with. The folder itself is not included, but its content is.</param>
        ''' <param name="Masks">Array of masks for files. Each mask is in format used by Visual Basic Like operator (wildchars * and ?).</param>
        Public Sub New(ByVal Root As String, ByVal ParamArray Masks As String())
            Me.New(Root, DirectCast(Masks, IEnumerable(Of String)))
        End Sub
        ''' <summary>CTor from root and one mask</summary>
        ''' <param name="Root">Folder to start enumeration with. The folder itself is not included, but its content is.</param>
        ''' <param name="Mask">Mask for files. Each mask is in format used by Visual Basic Like operator (wildchars * and ?).</param>
        ''' <param name="FoldersFirst"></param>
        Public Sub New(ByVal Root As String, ByVal Mask As String, Optional ByVal FoldersFirst As Boolean = False)
            Me.New(Root, New String() {Mask}, FoldersFirst)
        End Sub

        ''' <summary>CTor from root, boolean for defining order of listing of folders and array of masks</summary>
        ''' <param name="Root">Folder to start enumeration with. The folder itself is not included, but its content is.</param>
        ''' <param name="FoldersFirst"></param>
        ''' <param name="Masks">Array of masks for files. Each mask is in format used by Visual Basic Like operator (wildchars * and ?).</param>
        Public Sub New(ByVal Root As String, ByVal FoldersFirst As Boolean, ByVal ParamArray Masks As String())
            Me.New(Root, DirectCast(Masks, IEnumerable(Of String)), FoldersFirst)
        End Sub
        ''' <summary>CTor from root and masks in one string separated by separator</summary>
        ''' <param name="Root">Folder to start enumeration with. The folder itself is not included, but its content is.</param>
        ''' <param name="Masks">Masks for files separated by <paramref name="MaskSeperator"/>. Each mask is in format used by Visual Basic Like operator (wildchars * and ?).</param>
        ''' <param name="MaskSeperator">Seperator for <see cref="String.Split">splitting</see> <paramref name="Masks"/>.</param>
        ''' <param name="FoldersFirst"></param>
        Public Sub New(ByVal Root As String, ByVal Masks As String, ByVal MaskSeperator As Char, Optional ByVal FoldersFirst As Boolean = False)
            Me.New(Root, Masks.Split(MaskSeperator), FoldersFirst)
        End Sub
        ''' <summary>Returns if file or folder should be included in listing or if folder should be browser</summary>
        ''' <param name="Path">File or folder</param>
        ''' <param name="Reason">Type of filtering</param>
        ''' <returns>True if <paramref name="Path"/> satisfies al least one mask from either <see cref="FoldersEnterMasks"/>, <see cref="FoldersListMasks"/> or <see cref="MaskForFiles"/> depending on <paramref name="Reason"/></returns>
        ''' <exception cref="InvalidEnumArgumentException">This implementation throws <see cref="InvalidEnumArgumentException"/> when <paramref name="Path"/> represents folder and <paramref name="Reason"/> is not one of <see cref="FilterReasons"/> members.</exception>
        Protected Overridable Shadows Function Filter(ByVal Path As String, ByVal Reason As FilterReasons) As Boolean
            Dim Masks As IEnumerable(Of String)
            If IO.Directory.Exists(Path) Then
                Select Case Reason
                    Case FilterReasons.See : Masks = FoldersEnterMasks
                    Case FilterReasons.Show : Masks = FoldersListMasks
                    Case Else : Throw New InvalidEnumArgumentException("Reason", Reason, GetType(FilterReasons))
                End Select
            Else : Masks = MaskForFiles
            End If
            For Each Mask In Masks
                If IO.Path.GetFileName(Path) Like Mask Then Return True
            Next Mask
            Return False
        End Function
    End Class
End Namespace
#End If