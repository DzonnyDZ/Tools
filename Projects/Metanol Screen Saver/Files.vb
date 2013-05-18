Friend Class FileSystemEnumerator : Implements IEnumerator(Of String)
    Private Root As String
    Private Folders As Stack(Of IEnumerator(Of String))
    Private Files As IEnumerator(Of String)
    Private FoldersFirst As Boolean
    ''' <summary></summary>
    ''' <param name="Root"></param>
    ''' <param name="FoldersFirst"></param>
    Public Sub New(ByVal Root As String, Optional ByVal FoldersFirst As Boolean = False)
        Me.FoldersFirst = FoldersFirst
    End Sub

    ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
    ''' <returns>The element in the collection at the current position of the enumerator.</returns>
    Public ReadOnly Property Current() As String Implements System.Collections.Generic.IEnumerator(Of String).Current
        Get
            Return _Current
        End Get
    End Property
    ''' <summary>Gets the current element in the collection.</summary>
    ''' <returns>The current element in the collection.</returns>
    ''' <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.-or- The collection was modified after the enumerator was created.</exception>
    ''' <filterpriority>2</filterpriority>
    <Obsolete("Use type.safe Current instead")> _
    Private ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
        Get
            Return Current
        End Get
    End Property

    Private _Current As String

    ''' <summary>Advances the enumerator to the next element of the collection.</summary>
    ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
    ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
    ''' <filterpriority>2</filterpriority>
    Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
        If Folders Is Nothing Then 'Not started yet
            Folders = New Stack(Of IEnumerator(Of String))
            Folders.Push(New String() {Root}.GetEnumerator)
            Return Folders.Peek.MoveNext()
        ElseIf Folders.Count = 0 Then 'Finished
            Return False
        ElseIf Files IsNot Nothing Then 'Now going through files
            If Files.MoveNext Then 'Next file in current folder
                _Current = Files.Current 'To be returned
                Return True
            ElseIf FoldersFirst Then 'Folder finished
                Folders.Pop()
                Files = Nothing
                Return MoveNext
            Else 'Now files
                Files = IO.Directory.GetFiles(Folders.Peek.Current).GetEnumerator
                Return MoveNext
            End If
        Else 'Now going through folders
            If Folders.Peek.MoveNext Then 'Next folder in current folder
                _Current = Folders.Peek.Current 'To be returned
                Folders.Push(IO.Directory.GetDirectories(Folders.Peek.Current).GetEnumerator) 'To be enumerated
                If Not FoldersFirst Then 'Enumerate files from that folder first
                    Files = IO.Directory.GetFiles(Folders.Peek.Current).GetEnumerator
                End If
                Return True
            Else
                If FoldersFirst Then 'Now files
                    Files = IO.Directory.GetFiles(Folders.Peek.Current).GetEnumerator
                    Return MoveNext
                Else 'Folder finished
                    Folders.Pop()
                    Return MoveNext
                End If
            End If
        End If
    End Function


    ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
    ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
    ''' <filterpriority>2</filterpriority>
    Public Sub Reset() Implements System.Collections.IEnumerator.Reset
        Files = Nothing
        Folders = Nothing
    End Sub


#Region " IDisposable Support "

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub
    
    ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
    ''' <filterpriority>2</filterpriority>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

Friend Class FilesEnumerator : Implements IEnumerator(Of String)
    Private Root As String
    Private FoldersFirst As Boolean
    Private Masks As IEnumerable(Of String)
    Private Folders As Stack(Of IEnumerator(Of String))
    Private Files As IEnumerator(Of String)
    Public Sub New(ByVal Root$, ByVal Masks As IEnumerable(Of String), Optional ByVal FoldersFirst As Boolean = False)
        Me.FoldersFirst = FoldersFirst
        Me.Masks = Masks
        Me.Root = Root
    End Sub
    Public Sub New(ByVal Root$, ByVal Mask$, Optional ByVal FoldersFirst As Boolean = False)

    End Sub
    Public ReadOnly Property Current() As String Implements System.Collections.Generic.IEnumerator(Of String).Current
        Get

        End Get
    End Property

    Private ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
        Get
            Return Current
        End Get
    End Property

    Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
        If Folders Is Nothing Then
            Folders = New Stack(Of IEnumerator(Of String))()
            Folders.Push(DirectCast(IO.Directory.GetDirectories(Root), IEnumerable(Of String)).GetEnumerator)
            If Not FoldersFirst Then Files = DirectCast(IO.Directory.GetFiles(Root), IEnumerable(Of String)).GetEnumerator
        End If
        Return MoveNextInternal()
    End Function
    Private Function MoveNextInternal() As Boolean
        'Am I in files?
        '   Yes
        '       Next file?
        '           Yes
        '               Return True
        '           No
        '               Set files to null
        '               Folders first?
        '                   Yes
        '                       CUrrent folder finished. Pop It.
        '                       Any folder on stack?
        '                           No
        '                               Return False
        '               Do it again recursive and return
        '   No
        '       Next folder?
        '           Yes
        '               Folders first?
        '                   Yes
        '                       Push list of folders.
        '                       Do it again recursive and return
        '                   No

        If Files IsNot Nothing Then
            While Files.MoveNext
                Dim Match As Boolean = False
                For Each Mask In Masks
                    If Files.Current Like Mask Then Match = True : Exit For
                Next Mask
                If Match Then Return True
            End While
            Files = Nothing
        End If
        If FoldersFirst Then
            Folders.Pop()
            If Folders.Count = 0 Then Return False
        End If
        If Not Folders.Peek.MoveNext() Then
            If FoldersFirst Then
                Files = DirectCast(IO.Directory.GetFiles(Folders.Peek.Current), IEnumerable(Of String)).GetEnumerator
                Return MoveNextInternal
            Else
                Folders.Pop()
                If Folders.Count > 0 Then
                    Files = DirectCast(IO.Directory.GetFiles(Folders.Peek.Current), IEnumerable(Of String)).GetEnumerator
                    Return MoveNextInternal
                Else
                    Return False
                End If
            End If
        Else
            If FoldersFirst Then
            Else
                Files = DirectCast(IO.Directory.GetFiles(Folders.Peek.Current), IEnumerable(Of String)).GetEnumerator
            End If
        End If
    End Function

    Public Sub Reset() Implements System.Collections.IEnumerator.Reset

    End Sub

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
Friend Class Files : Implements IEnumerable(Of String)
    Private Root As String
    Private Masks As IEnumerable(Of String)
    Private Files As New List(Of String)
    Private Alghoritm As SSAverAlghoritm
    Public Sub New(ByVal Root As String, ByVal Masks As IEnumerable(Of String), ByVal Alghoritm As SSAverAlghoritm)
        Me.Root = Root
        Me.Masks = Masks
        Me.Alghoritm = Alghoritm
        Dim AllFiles = IO.Directory.GetFiles(Root, "*.*", IO.SearchOption.AllDirectories)
        Files.AddRange(From File In AllFiles Where InLike(File) Select File)
    End Sub
    Private Function InLike(ByVal File As String) As Boolean
        For Each Mask In Masks
            If IO.Path.GetFileName(File).ToLower Like Mask.ToLower Then Return True
        Next Mask
        Return False
    End Function

    Private Class Enumerator : Implements IEnumerator(Of String)
        Private Instance As Files
        Private Position As Integer = -1
        Private Rnd As New Random
        Public Sub New(ByVal Collection As Files)
            Instance = Collection
        End Sub
        Public ReadOnly Property Current() As String Implements System.Collections.Generic.IEnumerator(Of String).Current
            Get
                Return Instance.Files(Position)
            End Get
        End Property

        Private ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            If Instance.Files.Count < 1 Then Return False
            If Instance.Alghoritm = SSAverAlghoritm.Sequintial AndAlso Position < Instance.Files.Count - 1 AndAlso Position >= 0 Then
                Position += 1
            ElseIf Instance.Alghoritm = SSAverAlghoritm.Sequintial Then
                Reset()
            Else
                Position = Rnd.Next(0, Instance.Files.Count - 1)
            End If
            Return True
        End Function

        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            If Instance.Alghoritm = SSAverAlghoritm.Sequintial Then
                Position = 0
            Else
                MoveNext()
            End If
        End Sub

#Region " IDisposable Support "
        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    Instance = Nothing
                    Rnd = Nothing
                End If

                ' TODO: free your own state (unmanaged objects).
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of String) Implements System.Collections.Generic.IEnumerable(Of String).GetEnumerator
        Return New Enumerator(Me)
    End Function

    Private Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function
End Class