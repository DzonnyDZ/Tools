Imports Tools.CollectionsT.GenericT, Tools.WindowsT.FormsT.UtilitiesT, System.Windows.Forms
Namespace WindowsT.FormsT
#If Config <= Nightly Then
    'ASAP:Mark,Bitmap,Comment,Forum,wiki, describe propeties
    'Localize: UI
    <ComponentModelT.Prefix("kwe")> _
    Public Class KeyWordsEditor
        Implements IComparer(Of String)
#Region "Auto complete"
        ''' <summary>Contains value of the <see cref="AutoCompleteCacheName"/> property</summary>
        Private _AutoCompleteCacheName As String = ""
        ''' <summary>Autocomplete chache shared across instances with same <see cref="AutoCompleteCacheName"/></summary>
        Protected Shared ReadOnly AutocompleteCache As New Dictionary(Of String, ListWithEvents(Of String))
        ''' <summary>Contains value of the <see cref="Synonyms"/> property</summary>
        Private _Synonyms As List(Of KeyValuePair(Of String(), String()))
        ''' <summary>Gets or sets synonyms configuration</summary>
        ''' <remarks><para>Synonyms configuration is such that its list of pairs of arrays. Key array contain words one of which is added than also all key-array words are added and all value-array words are added to</para>
        ''' <para>If this property is null then synonym capability of <see cref="KeyWordsEditor"/> is turned off</para></remarks>
        <Browsable(False)> _
        Public Property Synonyms() As List(Of KeyValuePair(Of String(), String()))
            Get
                Return _Synonyms
            End Get
            Set(ByVal value As List(Of KeyValuePair(Of String(), String())))
                _Synonyms = value
                TmiEnabled()
            End Set
        End Property
        ''' <summary>Name per-session cache of keywords used by this instance</summary>
        ''' <value>An enmpty <see cref="String"/> to use no temporary chache</value>
        <DefaultValue("")> _
        Public Property AutoCompleteCacheName() As String
            Get
                Return _AutoCompleteCacheName
            End Get
            Set(ByVal value As String)
                RemoveHandlers()
                _AutoCompleteCacheName = value
                AddHandlers()
                TmiEnabled()
            End Set
        End Property
        ''' <summary>Removes handlers for temporary chache</summary>
        Private Sub RemoveHandlers()
            If AutoCompleteCacheName <> "" AndAlso AutocompleteCache.ContainsKey(AutoCompleteCacheName) Then
                With AutocompleteCache(AutoCompleteCacheName)
                    RemoveHandler .Added, AddressOf Autocomplete_Added
                    RemoveHandler .ItemChanged, AddressOf Autocomplete_Changed
                    RemoveHandler .Removed, AddressOf Autocomplete_Removed
                    RemoveHandler .Cleared, AddressOf Autocomplete_Cleared
                End With
            End If
        End Sub
        ''' <summary>Adds handlers to temporary cache</summary>
        Private Sub AddHandlers()
            If AutoCompleteCacheName <> "" Then
                If Not AutocompleteCache.ContainsKey(AutoCompleteCacheName) Then
                    AutocompleteCache.Add(AutoCompleteCacheName, New ListWithEvents(Of String)(True))
                End If
                With AutocompleteCache(AutoCompleteCacheName)
                    AddHandler .Added, AddressOf Autocomplete_Added
                    AddHandler .ItemChanged, AddressOf Autocomplete_Changed
                    AddHandler .Removed, AddressOf Autocomplete_Removed
                    AddHandler .Cleared, AddressOf Autocomplete_Cleared
                End With
            End If
        End Sub
        ''' <summary>Handles adding item to either autocomplete source</summary>
        Private Sub Autocomplete_Added(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).ItemIndexEventArgs) _
            Handles _AutoCompleteStable.Added
            If Not InOtherList(e.Item, sender) Then _
                Me.txtEdit.AutoCompleteCustomSource.Add(e.Item)
        End Sub
        ''' <summary>Handles change of item value in either autocomplete source</summary>
        Private Sub Autocomplete_Changed(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).OldNewItemEvetArgs) _
            Handles _AutoCompleteStable.ItemChanged
            If Not InAnyList(e.OldItem) Then _
                Me.txtEdit.AutoCompleteCustomSource.Remove(e.OldItem)
            If Not InOtherList(e.Item, sender) Then _
                Me.txtEdit.AutoCompleteCustomSource.Add(e.Item)
        End Sub
        ''' <summary>Determines if autocomplete item is in one of autocomplete sources</summary>
        ''' <param name="What">Item to search for</param>
        Private Function InAnyList(ByVal What As String) As Boolean
            Dim IsIn As Boolean = False
            If InstanceAutoCompleteChache IsNot Nothing Then IsIn = InstanceAutoCompleteChache.Contains(What)
            Return IsIn OrElse (AutoCompleteStable IsNot Nothing AndAlso AutoCompleteStable.Contains(What))
        End Function
        ''' <summary>Determines if item is member of autocomplete source other than given</summary>
        ''' <param name="What">Item to search for</param>
        ''' <param name="List">Autocomplete source not to search in</param>
        Private Function InOtherList(ByVal What As String, ByVal List As ListWithEvents(Of String)) As Boolean
            If List Is AutoCompleteStable Then
                Return InstanceAutoCompleteChache IsNot Nothing AndAlso InstanceAutoCompleteChache.Contains(What)
            ElseIf List Is InstanceAutoCompleteChache Then
                Return AutoCompleteStable IsNot Nothing AndAlso AutoCompleteStable.Contains(What)
            Else
                Return False
            End If
        End Function
        ''' <summary>Hanldes removal of single item from either auto complete source</summary>
        Private Sub Autocomplete_Removed(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).ItemIndexEventArgs) _
            Handles _AutoCompleteStable.Removed
            If Not InAnyList(e.Item) Then _
                Me.txtEdit.AutoCompleteCustomSource.Remove(e.Item)
        End Sub
        ''' <summary>Hanles clearing of either auto complete source</summary>
        Private Sub Autocomplete_Cleared(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).ItemsEventArgs) _
            Handles _AutoCompleteStable.Cleared
            For Each item As String In e.Items
                If Not InAnyList(item) Then _
                    Me.txtEdit.AutoCompleteCustomSource.Remove(item)
            Next item
        End Sub
        ''' <summary>contains value of the <see cref="AutoCompleteStable"/> property</summary>
        Private WithEvents _AutoCompleteStable As ListWithEvents(Of String)
        ''' <summary>Permanent autocomplete source</summary>
        <Browsable(False)> _
        Public Property AutoCompleteStable() As ListWithEvents(Of String)
            Get
                Return _AutoCompleteStable
            End Get
            Set(ByVal value As ListWithEvents(Of String))
                _AutoCompleteStable = value
                Me.txtEdit.AutoCompleteCustomSource.Clear()
                If Me.AutoCompleteCacheName <> "" Then Me.txtEdit.AutoCompleteCustomSource.AddRange(Me.InstanceAutoCompleteChache.toarray)
                If value IsNot Nothing Then _
                    Me.txtEdit.AutoCompleteCustomSource.AddRange(value.ToArray)
                TmiEnabled()
            End Set
        End Property
        ''' <summary>Enables/disbaled ans shows/hides items of <see cref="cmsThesaurus"/> according to values of <see cref="AutoCompleteCacheName"/> and <see cref="AutoCompleteStable"/></summary>
        Private Sub TmiEnabled()
            tmiAddSelected.Enabled = AutoCompleteStable IsNot Nothing
            tmiRemoveSelected.Enabled = AutoCompleteStable IsNot Nothing
            tmiManage.Enabled = AutoCompleteStable IsNot Nothing OrElse Synonyms IsNot Nothing
            tmiClearCache.Enabled = AutoCompleteCacheName <> ""
            tmiSynonyms.Enabled = Synonyms IsNot Nothing
            For Each item As ToolStripMenuItem In New ToolStripMenuItem() {tmiAddSelected, tmiRemoveSelected, tmiManage, tmiClearCache, tmiSynonyms}
                item.Visible = item.Enabled
            Next item
        End Sub
        ''' <summary>Gets autocomplete chache used by this instance (if any)</summary>
        <Browsable(False)> _
        Public ReadOnly Property InstanceAutoCompleteChache() As ListWithEvents(Of String)
            Get
                If AutoCompleteCacheName <> "" Then
                    Return AutocompleteCache(AutoCompleteCacheName)
                Else
                    Return Nothing
                End If
            End Get
        End Property
        Private Sub cmdThesaurus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdThesaurus.Click
            OnThesaurusClick(e)
        End Sub
        ''' <summary>Called when theasursu button is clicked. Shows thesaurus menu (if applicable)</summary>
        Protected Overridable Sub OnThesaurusClick(ByVal e As EventArgs)
            For Each item As ToolStripMenuItem In New ToolStripMenuItem() {tmiAddSelected, tmiRemoveSelected, tmiManage, tmiClearCache, tmiSynonyms}
                If item.Enabled Then cmsThesaurus.Show(cmdThesaurus, 0, cmdThesaurus.Height) : Exit Sub
            Next item
        End Sub
        ''' <summary>Contains value of the <see cref="CaseSensitive"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _CaseSensitive As Boolean = False
        ''' <summary>Gets or sets value idicating if keywords are case sensitive</summary>
        <DefaultValue(False)> _
        Public Property CaseSensitive() As Boolean
            Get
                Return _CaseSensitive
            End Get
            Set(ByVal value As Boolean)
                _CaseSensitive = value
            End Set
        End Property
#End Region
#Region "UI"
        ''' <summary>Gets or sets state of <see cref="Status"/></summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <DefaultValue(GetType(ControlState), "Enabled")> _
        Public Property StatusState() As ControlState
            Get
                Return Misc.ControlState(stmStatus)
            End Get
            Set(ByVal value As ControlState)
                Misc.ControlState(stmStatus) = value
            End Set
        End Property
        ''' <summary><see cref="StatusMarker"/> present on this control</summary>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property Status() As StatusMarker
            Get
                Return stmStatus
            End Get
        End Property
        ''' <summary>Gets or sets state of thesaurus button</summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <DefaultValue(GetType(ControlState), "Enabled")> _
        Public Property ThesaurusButtonState() As ControlState
            Get
                Return Misc.ControlState(cmdThesaurus)
            End Get
            Set(ByVal value As ControlState)
                Misc.ControlState(cmdThesaurus) = value
            End Set
        End Property
        ''' <summary>Gets or sets state of merge button</summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <DefaultValue(GetType(ControlState), "Enabled")> _
        Public Property MergeButtonState() As ControlState
            Get
                Return Misc.ControlState(cmdMerge)
            End Get
            Set(ByVal value As ControlState)
                Misc.ControlState(cmdMerge) = value
            End Set
        End Property
        ''' <summary>Gets or sets value indicating if merge button is checked (orange) or not (gray)</summary>
        <DefaultValue(True)> _
        Public Property Merge() As Boolean
            Get
                Return cmdMerge.BackColor = Drawing.Color.Orange
            End Get
            Set(ByVal value As Boolean)
                Dim old As Boolean = Merge
                If value = True Then
                    cmdMerge.BackColor = Drawing.Color.Orange
                Else
                    cmdMerge.BackColor = Drawing.Color.Gray
                End If
                If old <> value Then OnMergeChanged(EventArgs.Empty)
            End Set
        End Property
        ''' <summary>Raised after value of the <see cref="Merge"/> property changes</summary>
        Public Event MergeChanged As EventHandler
        ''' <summary>Raises the <see cref="MergeChanged"/> event</summary>
        ''' <param name="e">Event params</param>
        Protected Overridable Sub OnMergeChanged(ByVal e As EventArgs)
            RaiseEvent MergeChanged(Me, e)
        End Sub


        Private Sub cmdMerge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMerge.Click
            Me.Merge = Not Me.Merge
        End Sub
        ''' <summary>Raises the <see cref="TextChanged"/> event</summary>
        ''' <param name="e">Event params</param>
        ''' <remarks>Note for inheritors. If you want to keep <see cref="Text"/> and <see cref="txtEdit">txtEdit</see>.<see cref="System.Windows.Forms.TextBox.Text">Text</see>, call base class method or sync them by your own</remarks>
        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            MyBase.OnTextChanged(e)
            If Me.Text <> txtEdit.Text Then txtEdit.Text = Me.Text
        End Sub
        Private Sub txtEdit_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEdit.TextChanged
            Me.Text = txtEdit.Text
        End Sub
#End Region
#Region "KW manipulation"
        ''' <summary>Compares two <see cref="String">Strings</see> in way determined by the <see cref="CaseSensitive"/> property</summary>
        ''' <param name="Str1"><see cref="String"/> to compare</param>
        ''' <param name="Str2"><see cref="String"/> to compare</param>
        ''' <returns>True if parameters match; false otherwise</returns>
        Protected Function CSCompare(ByVal Str1 As String, ByVal Str2 As String) As Boolean
            If CaseSensitive Then Return Str1 = Str2 Else Return LCase(Str1) = LCase(Str2)
        End Function

        Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
            AddKeyword()
        End Sub
        Private Sub txtEdit_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEdit.KeyDown
            If e.KeyCode = Keys.Return Then
                AddKeyword()
            End If
        End Sub
        ''' <summary>Adds keyword from <see cref="txtEdit"/> into <see cref="lstKW"/> (it it is not there) and to <see cref="InstanceAutoCompleteChache"/> is it is not there and if it is not null</summary>
        ''' <remarks>Also synonyms of that word are added to list</remarks>
        Private Sub AddKeyword()
            If InstanceAutoCompleteChache IsNot Nothing Then
                Dim InCache As Boolean = False
                For Each item As String In InstanceAutoCompleteChache
                    If CSCompare(txtEdit.Text, item) Then InCache = True : Exit For
                Next item
                If Not InCache Then InstanceAutoCompleteChache.Add(txtEdit.Text)
            End If
            Dim Sel As New List(Of String)
            For Each Kw As String In GetSynonyms(txtEdit.Text)
                Dim InList As Boolean = False
                Dim i As Integer = 0
                For Each item As String In KeyWords
                    If CSCompare(Kw, item) Then
                        InList = True
                        Sel.Add(Kw)
                        Exit For
                    End If
                    i += 1
                Next item
                If Not InList Then
                    KeyWords.Add(Kw)
                    Sel.Add(Kw)
                    OnKeywordAdded(New ListWithEvents(Of String).ItemEventArgs(Kw))
                End If
            Next Kw
            txtEdit.Select()
            txtEdit.SelectAll()
            lstKW.SelectedItems.Clear()
            For Each item As String In Sel
                lstKW.SelectedItems.Add(item)
            Next item
        End Sub
        ''' <summary>Raised when user adds keyword</summary>
        ''' <remarks>Not raised when keyword is added programatically</remarks>
        Public Event KeywordAdded As EventHandler(Of ListWithEvents(Of String).ItemEventArgs)
        ''' <summary>Raises the <see cref="KeywordAdded"/> event</summary> 
        ''' <param name="e">event parameters</param>
        Protected Overridable Sub OnKeywordAdded(ByVal e As ListWithEvents(Of String).ItemEventArgs)
            If AutoChange AndAlso Me.Status.Status = StatusMarker.Statuses.Null Then
                Me.Status.Status = StatusMarker.Statuses.[New]
            ElseIf AutoChange Then
                Me.Status.Status = StatusMarker.Statuses.Changed
            End If
            RaiseEvent KeywordAdded(Me, e)
        End Sub
        ''' <summary>List of keywords currenly in list</summary>
        <Browsable(False)> _
        Public ReadOnly Property KeyWords() As IList(Of String)
            Get
                Return New ListWrapper(Of String)(lstKW.Items)
            End Get
        End Property

        Private Sub lstKW_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstKW.KeyDown
            If e.KeyCode = Keys.Delete Then
                RemoveSelectedItems()
            End If
        End Sub
        ''' <summary>Deletes selected items from <see cref="lstKW"/></summary>
        Private Sub RemoveSelectedItems()
            If lstKW.SelectedItems.Count > 0 Then
                Dim RemovedItems(lstKW.SelectedItems.Count - 1) As String
                Dim w As New ListWrapper(Of String)(lstKW.SelectedItems)
                w.CopyTo(RemovedItems, 0)
                While lstKW.SelectedItems.Count > 0
                    lstKW.Items.RemoveAt(lstKW.SelectedIndices(0))
                End While
                OnKeyWordRemoved(New ListWithEvents(Of String).ItemsEventArgs(RemovedItems))
            End If
        End Sub
        ''' <summary>Raises the <see cref="KeyWordRemoved"/> event</summary>
        ''' <param name="e">Event params</param>
        Protected Overridable Sub OnKeyWordRemoved(ByVal e As ListWithEvents(Of String).ItemsEventArgs)
            If AutoChange AndAlso Me.Status.Status = StatusMarker.Statuses.Null Then
                Me.Status.Status = StatusMarker.Statuses.[New]
            ElseIf AutoChange Then
                Me.Status.Status = StatusMarker.Statuses.Changed
            End If
            RaiseEvent KeyWordRemoved(Me, e)
        End Sub
        ''' <summary>Raised after user manually removes keyword(s)</summary>
        ''' <remarks>Not raised when keywords are removed programatically</remarks>
        Public Event KeyWordRemoved As EventHandler(Of ListWithEvents(Of String).ItemsEventArgs)
        Private _AutoChange As Boolean = True
        Public Property AutoChange() As Boolean
            Get
                Return _AutoChange
            End Get
            Set(ByVal value As Boolean)
                _AutoChange = value
            End Set
        End Property
#End Region
#Region "Management"
        Private Sub tmiLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiLabel.Click
            OnThesaurusClick(e)
        End Sub
        Private Sub tmiAddSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiAddSelected.Click
            If AutoCompleteStable IsNot Nothing Then
                With AutoCompleteStable
                    For Each Kw As String In lstKW.SelectedItems
                        Dim IsIn As Boolean = False
                        For Each Item As String In AutoCompleteStable
                            If CSCompare(Item, Kw) Then IsIn = True : Exit For
                        Next Item
                        If Not IsIn Then
                            .Add(Kw)
                        End If
                    Next Kw
                End With
            End If
        End Sub

        Private Sub tmiRemoveSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiRemoveSelected.Click
            If AutoCompleteStable IsNot Nothing Then
                Dim Remove As New List(Of String)
                With AutoCompleteStable
                    For Each Kw As String In lstKW.SelectedItems
                        For Each Item As String In AutoCompleteStable
                            If CSCompare(Item, Kw) Then Remove.Add(Kw)
                        Next Item
                    Next Kw
                    For Each item As String In Remove
                        .Remove(item)
                    Next item
                End With
            End If
        End Sub

        Private Sub tmiClearCache_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiClearCache.Click
            If AutoCompleteCacheName <> "" Then InstanceAutoCompleteChache.Clear()
        End Sub
#End Region
#Region "Synonyms"
        ''' <summary>Gets indexes to <see cref="Synonyms"/> where synonyms of given word are stored in </summary>
        ''' <param name="Word">Word to search for synonyms of</param>
        Protected Function GetSynonymIndexes(ByVal Word As String) As Integer()
            If Synonyms IsNot Nothing Then
                Dim Indices As New List(Of Integer)
                Dim i As Integer = 0
                For Each item As KeyValuePair(Of String(), String()) In Synonyms
                    If item.Key IsNot Nothing Then
                        For Each Key As String In item.Key
                            If CSCompare(Word, Key) Then
                                Indices.Add(i)
                                Exit For
                            End If
                        Next Key
                    End If
                    i += 1
                Next item
                Return Indices.ToArray
            Else
                Return New Integer() {}
            End If
        End Function
        ''' <summary>Gets all synonyms of given word including it</summary>
        ''' <param name="Word">Word to get synonyms for</param>
        ''' <returns>Synonyms of <paramref name="Word"/> (avoiding duplicates). When no synonyms are found <paramref name="Word"/> itself is returned</returns>
        Public Function GetSynonyms(ByVal Word As String) As String()
            Dim Ret As New List(Of String)
            If Synonyms IsNot Nothing Then
                For Each index As Integer In GetSynonymIndexes(Word)
                    If Synonyms(index).Key IsNot Nothing Then Ret.AddRange(Synonyms(index).Key)
                    If Synonyms(index).Value IsNot Nothing Then Ret.AddRange(Synonyms(index).Value)
                Next index
            End If
            Ret.Sort(Me)
            For i As Integer = Ret.Count - 1 To 1 Step -1
                If CSCompare(Ret(i), Ret(i - 1)) Then Ret.RemoveAt(i)
            Next i
            If Ret.Count = 0 Then Ret.Add(Word)
            Return Ret.ToArray
        End Function
#End Region
        ''' <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
        ''' <param name="y">The second object to compare.</param>
        ''' <param name="x">The first object to compare.</param>
        ''' <returns>Value Condition Less than zerox is less than y.Zerox equals y.Greater than zerox is greater than y.</returns>
        Private Function CSCompareI(ByVal x As String, ByVal y As String) As Integer Implements System.Collections.Generic.IComparer(Of String).Compare
            If CSCompare(x, y) Then Return 0
            Return String.Compare(x, y)
        End Function

        Private Sub tmiSynonyms_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiSynonyms.Click
            If lstKW.SelectedItems.Count >= 2 AndAlso Synonyms IsNot Nothing Then
                Dim Key(lstKW.SelectedItems.Count - 1) As String
                Dim w As New ListWrapper(Of String)(lstKW.SelectedItems)
                w.CopyTo(Key, 0)
                Synonyms.Add(New KeyValuePair(Of String(), String())(Key, New String() {}))
            End If
        End Sub
        ''' <summary>Shows forms tprovides dialog for editing settings for this instance</summary>
        Public Sub ShowDialog()
            Dim dlg As New ThesaurusForm(Me)
            dlg.ShowDialog(Me.FindForm)
        End Sub

        Private Sub tmiManage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiManage.Click
            ShowDialog()
        End Sub
    End Class
#End If
End Namespace