Imports Tools.CollectionsT.GenericT
Namespace WindowsT.FormsT
#If Config <= Nightly Then
    'ASAP:Mark,Bitmap,Comment,Forum,wiki
    Public Class KeyWordsEditor
        Private _AutoCompleteCacheName As String = ""
        Protected Shared ReadOnly AutocompleteCache As New Dictionary(Of String, ListWithEvents(Of String))
        <DefaultValue("")> _
        Public Property AutoCompleteCacheName() As String
            Get
                Return _AutoCompleteCacheName
            End Get
            Set(ByVal value As String)
                RemoveHandlers()
                _AutoCompleteCacheName = value
                AddHandlers()
            End Set
        End Property
        Private Sub RemoveHandlers()
            If AutoCompleteCacheName <> "" AndAlso AutocompleteCache.ContainsKey(AutoCompleteCacheName) Then
                With AutocompleteCache(AutoCompleteCacheName)
                    RemoveHandler .Added, AddressOf AutocompleteCache_Added
                    RemoveHandler .ItemChanged, AddressOf AutocompleteCache_Changed
                    RemoveHandler .Removed, AddressOf AutocompleteCache_Removed
                    RemoveHandler .Cleared, AddressOf AutocompleteCache_Cleared
                End With
            End If
        End Sub
        Private Sub AddHandlers()
            If AutoCompleteCacheName <> "" Then
                If Not AutocompleteCache.ContainsKey(AutoCompleteCacheName) Then
                    AutocompleteCache.Add(AutoCompleteCacheName, New ListWithEvents(Of String)(True))
                End If
                With AutocompleteCache(AutoCompleteCacheName)
                    AddHandler .Added, AddressOf AutocompleteCache_Added
                    AddHandler .ItemChanged, AddressOf AutocompleteCache_Changed
                    AddHandler .Removed, AddressOf AutocompleteCache_Removed
                    AddHandler .Cleared, AddressOf AutocompleteCache_Cleared
                End With
            End If
        End Sub
        Private Sub AutocompleteCache_Added(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).ItemIndexEventArgs) _
            Handles _AutoCompleteStable.Added
            If Not InOtherList(e.Item, sender) Then _
                Me.txtEdit.AutoCompleteCustomSource.Add(e.Item)
        End Sub
        Private Sub AutocompleteCache_Changed(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).OldNewItemEvetArgs) _
            Handles _AutoCompleteStable.ItemChanged
            If Not InAnyList(e.OldItem) Then _
                Me.txtEdit.AutoCompleteCustomSource.Remove(e.OldItem)
            If Not InOtherList(e.Item, sender) Then _
                Me.txtEdit.AutoCompleteCustomSource.Add(e.Item)
        End Sub
        Private Function InAnyList(ByVal What As String) As Boolean
            Dim IsIn As Boolean = False
            If InstanceAutoCompleteChache IsNot Nothing Then IsIn = InstanceAutoCompleteChache.Contains(What)
            Return IsIn OrElse AutoCompleteStable.Contains(What)
        End Function
        Private Function InOtherList(ByVal What As String, ByVal List As ListWithEvents(Of String)) As Boolean
            If List Is AutoCompleteStable Then
                Return InstanceAutoCompleteChache IsNot Nothing AndAlso InstanceAutoCompleteChache.Contains(What)
            ElseIf List Is InstanceAutoCompleteChache Then
                Return AutoCompleteStable.Contains(What)
            Else
                Return False
            End If
        End Function
        Private Sub AutocompleteCache_Removed(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).ItemIndexEventArgs) _
            Handles _AutoCompleteStable.Removed
            If Not InAnyList(e.Item) Then _
                Me.txtEdit.AutoCompleteCustomSource.Remove(e.Item)
        End Sub
        Private Sub AutocompleteCache_Cleared(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).ItemsEventArgs) _
            Handles _AutoCompleteStable.Cleared
            For Each item As String In e.Items
                If Not InAnyList(item) Then _
                    Me.txtEdit.AutoCompleteCustomSource.Remove(item)
            Next item
        End Sub
        Private WithEvents _AutoCompleteStable As ListWithEvents(Of String)
        <Browsable(False)> _
        Public Property AutoCompleteStable() As ListWithEvents(Of String)
            Get
                Return _AutoCompleteStable
            End Get
            Set(ByVal value As ListWithEvents(Of String))
                _AutoCompleteStable = value
                If Me.AutoCompleteCacheName <> "" Then Me.txtEdit.AutoCompleteCustomSource.AddRange(value.ToArray)
                Me.txtEdit.AutoCompleteCustomSource.Clear()
                Me.txtEdit.AutoCompleteCustomSource.AddRange(value.ToArray)
            End Set
        End Property
        <Browsable(False)> _
        Public ReadOnly Property InstanceAutoCompleteChache() As IList(Of String)
            Get
                If AutoCompleteCacheName <> "" Then
                    Return AutocompleteCache(AutoCompleteCacheName)
                Else
                    Return Nothing
                End If
            End Get
        End Property
    End Class
#End If
End Namespace