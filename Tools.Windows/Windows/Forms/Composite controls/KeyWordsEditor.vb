'Extracted
Imports Tools.CollectionsT.GenericT, Tools.WindowsT.FormsT.UtilitiesT, System.Windows.Forms, Tools.ComponentModelT
Imports System.Linq, System.Xml.Schema, Tools.ExtensionsT
Imports <xmlns:kw="http://dzonny.cz/xml/Tools.WindowsT.FormsT.KeyWordsEditor">
Imports System.Xml.Linq, Tools.LinqT
Imports Tools.DataStructuresT.GenericT

Namespace WindowsT.FormsT
    'Stage: Alpha
    'ASAP:Bitmap
    ''' <summary>Control that allows eas and very sophisticated editing of set of keywords</summary>
    ''' <remarks>There is a list of known keywords (which can be adited by user and persisted). Synonyms of keywords can be defined and added automatically in list.</remarks>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    <ComponentModelT.Prefix("kwe")> _
    <FirstVersion("06/26/2007")> _
    <Version(1, 1, GetType(KeyWordsEditor), LastChange:="06/27/2008")> _
    Public Class KeyWordsEditor
        Implements IComparer(Of String)
#Region "Auto complete"
        ''' <summary>Contains value of the <see cref="AutoCompleteCacheName"/> property</summary>
        Private _AutoCompleteCacheName As String = ""
        ''' <summary>Autocomplete chache shared across instances with same <see cref="AutoCompleteCacheName"/></summary>
        Protected Shared ReadOnly AutocompleteCache As New Dictionary(Of String, ListWithEvents(Of String))
        ''' <summary>Automatically shared lists of keywords for the <see cref="KeyWords"/> property. Sharred accross instances with same <see cref="AutoCompleteCacheName"/> and <see cref="AutomaticLists"/> = True</summary>
        Protected Shared ReadOnly SharedStableList As New Dictionary(Of String, ListWithEvents(Of String))
        ''' <summary>Automatically shared lists of synonyms for the <see cref="Synonyms"/> property. Sharred accross instances with same <see cref="AutoCompleteCacheName"/> and <see cref="AutomaticLists"/> = True</summary>
        Protected Shared ReadOnly SharedSynonymList As New Dictionary(Of String, List(Of KeyValuePair(Of String(), String())))
        ''' <summary>Contains value of the <see cref="Synonyms"/> property</summary>
        Private _Synonyms As List(Of KeyValuePair(Of String(), String()))
        ''' <summary>Gets or sets synonyms configuration</summary>
        ''' <remarks><para>Synonyms configuration is such that its list of pairs of arrays. Key array contain words one of which is added than also all key-array words are added and all value-array words are added to</para>
        ''' <para>If this property is null then synonym capability of <see cref="KeyWordsEditor"/> is turned off</para></remarks>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property Synonyms() As List(Of KeyValuePair(Of String(), String()))
            Get
                If AutomaticLists AndAlso _Synonyms Is Nothing Then Synonyms = GetSharedSynonymsList(AutoCompleteCacheName)
                Return _Synonyms
            End Get
            Set(ByVal value As List(Of KeyValuePair(Of String(), String())))
                _Synonyms = value
                If AutomaticLists AndAlso SharedSynonymList.ContainsKey(AutoCompleteCacheName) AndAlso value IsNot GetSharedSynonymsList(AutoCompleteCacheName) Then
                    _AutomaticLists = False
                End If
                TmiEnabled()
            End Set
        End Property
        ''' <summary>Gets item of <see cref="SharedSynonymList"/> with given key. Creates new item if given key does not exists.</summary>
        ''' <param name="Name">Key of item to be returned</param>
        ''' <returns>Item form <see cref="SharedSynonymList"/> with given key. If such key is not presents adds an empty collection for it. If <paramref name="Name"/> is <see cref="[String].Empty"/> or null returns null.</returns>
        Protected Shared Function GetSharedSynonymsList(ByVal Name$) As List(Of KeyValuePair(Of String(), String()))
            If Name = "" Then Return Nothing
            If Not SharedSynonymList.ContainsKey(Name) Then SharedSynonymList.Add(Name, New List(Of KeyValuePair(Of String(), String())))
            Return SharedSynonymList(Name)
        End Function
        ''' <summary>Gets item of <see cref="SharedStableList"/> with given key. Creates new item if given key does not exists.</summary>
        ''' <param name="Name">Key of item to be returned</param>
        ''' <returns>Item form <see cref="SharedStableList"/> with given key. If such key is not presents adds an empty collection for it. If <paramref name="Name"/> is <see cref="[String].Empty"/> or null returns null.</returns>
        Protected Shared Function GetSharedStableList(ByVal Name$) As ListWithEvents(Of String)
            If Name = "" Then Return Nothing
            If Not SharedStableList.ContainsKey(Name) Then SharedStableList.Add(Name, New ListWithEvents(Of String))
            Return SharedStableList(Name)
        End Function
        ''' <summary>Contains value of the <see cref="AutomaticLists"/> property</summary>
        Private _AutomaticLists As Boolean = True
        ''' <summary>Gets or sets value if this instance uses automacically shared lists of keywords and synonyms (among instances with same <see cref="AutoCompleteCacheName"/>).</summary>
        ''' <returns>In runtime indicates if both - <see cref="KeyWords"/> and <see cref="Synonyms"/> collections are automatically shared by this instance and all other instnces of <see cref="KeyWordsEditor"/> with same <see cref="AutoCompleteCacheName"/> and <see cref="AutomaticLists"/> = True.</returns>
        ''' <value>In design time predefines run-time behaviour. In runtime chan be only changed form False to True.</value>
        ''' <remarks>If set to false, you must set <see cref="KeyWords"/> and <see cref="Synonyms"/> properties in order editor to be fully functional.
        ''' <para>In runtime this property cannot be changed from true to false. Set <see cref="KeyWords"/> or <see cref="Synonyms"/> property instead. (Exception is not thrown, but value does not change when changing from True to False).</para>
        ''' </remarks>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <DefaultValue(True)> _
        <LDescription(GetType(CompositeControls), "AutomaticLists_d")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property AutomaticLists() As Boolean
            Get
                Return _AutomaticLists
            End Get
            Set(ByVal value As Boolean)
                If DesignMode Then
                    _AutomaticLists = value
                ElseIf value <> AutomaticLists Then
                    If value Then
                        _AutomaticLists = True
                        Synonyms = GetSharedSynonymsList(Me.AutoCompleteCacheName)
                        AutoCompleteStable = GetSharedStableList(Me.AutoCompleteCacheName)
                    Else
                        'Do nothing - property cannot be changed to False in runtime
                    End If
                End If
            End Set
        End Property
        ''' <summary>Designer serialization proxy from <see cref="AutomaticLists"/> property</summary>
        ''' <value>Setting this property to true is same as setting <see cref="AutomaticLists"/> to true. Setting this property to false sets <see cref="AutomaticLists"/> to false and <see cref="AutoCompleteStable"/> and <see cref="Synonyms"/> to null</value>
        ''' <returns><see cref="AutoCompleteStable"/></returns>
        ''' <remarks>This property is not intended to be used by programmer. It supports designt-time serialization fo control.</remarks>
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
        Public Property AutomaticsLists_Designer() As Boolean
            Get
                Return AutomaticLists
            End Get
            Set(ByVal value As Boolean)
                If value = True Then
                    AutomaticLists = value
                    TmiEnabled()
                Else
                    Synonyms = Nothing
                    AutoCompleteStable = Nothing
                    _AutomaticLists = value
                    TmiEnabled()
                End If
            End Set
        End Property
        ''' <summary>Name of per-session cache of keywords used by this instance and name of group of autocomplete items and synonyms (if used)</summary>
        ''' <value>An enmpty <see cref="String"/> to use no temporary chache</value>
        ''' <remarks>When <see cref="AutomaticLists"/> is true sets also name for list of autocomplete words and synonyms.</remarks>
        <DefaultValue(""), KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(CompositeControls), "AutoCompleteCacheName_d")> _
        Public Property AutoCompleteCacheName() As String
            Get
                Return _AutoCompleteCacheName
            End Get
            Set(ByVal value As String)
                RemoveHandlers()
                _AutoCompleteCacheName = value
                AddHandlers()
                If AutomaticLists Then
                    Synonyms = GetSharedSynonymsList(AutoCompleteCacheName)
                    AutoCompleteStable = GetSharedStableList(AutoCompleteCacheName)
                End If
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
        Private Sub Autocomplete_Changed(ByVal sender As ListWithEvents(Of String), ByVal e As ListWithEvents(Of String).OldNewItemEventArgs) _
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
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property AutoCompleteStable() As ListWithEvents(Of String)
            Get
                If _AutoCompleteStable Is Nothing AndAlso AutomaticLists Then AutoCompleteStable = GetSharedStableList(AutoCompleteCacheName)
                Return _AutoCompleteStable
            End Get
            Set(ByVal value As ListWithEvents(Of String))
                _AutoCompleteStable = value
                Me.txtEdit.AutoCompleteCustomSource.Clear()
                If Me.AutoCompleteCacheName <> "" Then Me.txtEdit.AutoCompleteCustomSource.AddRange(Me.InstanceAutoCompleteChache.ToArray)
                If value IsNot Nothing Then _
                    Me.txtEdit.AutoCompleteCustomSource.AddRange(value.ToArray)
                TmiEnabled()
                If AutomaticLists AndAlso SharedStableList.ContainsKey(AutoCompleteCacheName) AndAlso SharedStableList(AutoCompleteCacheName) IsNot value Then _
                    _AutomaticLists = False
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
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
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
        <LDescription(GetType(CompositeControls), "CaseSensitive_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
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
        <LDescription(GetType(CompositeControls), "StatusState_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
        Public Property StatusState() As ControlState
            Get
                Return WinFormsExtensions.ControlState(stmStatus)
            End Get
            Set(ByVal value As ControlState)
                WinFormsExtensions.ControlState(stmStatus) = value
            End Set
        End Property
        ''' <summary><see cref="StatusMarker"/> present on this control</summary>
        <LDescription(GetType(CompositeControls), "Status_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property Status() As StatusMarker
            Get
                Return stmStatus
            End Get
        End Property
        ''' <summary>Gets value indicationg if the <see cref="Status_"/> property should be serialized</summary>
        ''' <returns>True if designer should serialize the <see cref="Status_"/> property</returns>
        Private Function ShouldSerializeStatus_() As Boolean
            If StatusState = ControlState.Hidden Then Return False
            Return Status_.ShouldSerialize
        End Function
        ''' <summary>Design-time proxy for the <see cref="Status"/> property</summary>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        <LDescription(GetType(CompositeControls), "Status_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
        <DisplayName("Status")> _
        <TypeConverter(GetType(ExpandableObjectConverter))> _
        Public ReadOnly Property Status_() As StatusProxy
            Get
                Static px As New StatusProxy(Me)
                Return px
            End Get
        End Property
        ''' <summary>Gets or sets state of thesaurus button</summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <DefaultValue(GetType(ControlState), "Enabled")> _
        <LDescription(GetType(CompositeControls), "ThesaurusButtonAttribute_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property ThesaurusButtonState() As ControlState
            Get
                Return WinFormsExtensions.ControlState(cmdThesaurus)
            End Get
            Set(ByVal value As ControlState)
                WinFormsExtensions.ControlState(cmdThesaurus) = value
            End Set
        End Property
        ''' <summary>Gets or sets state of merge button</summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <LDescription(GetType(CompositeControls), "MergeButtonState_d"), KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <DefaultValue(GetType(ControlState), "Enabled")> _
        Public Property MergeButtonState() As ControlState
            Get
                Return WinFormsExtensions.ControlState(cmdMerge)
            End Get
            Set(ByVal value As ControlState)
                WinFormsExtensions.ControlState(cmdMerge) = value
            End Set
        End Property
        ''' <summary>Gets or sets value indicating if merge button is checked (orange) or not (gray)</summary>
        <DefaultValue(True)> _
        <LDescription(GetType(CompositeControls), "Merge_d"), KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
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
        <LCategory(GetType(My.Resources.Resources), "ValueChanged_cat", "Value Changed"), LDescription(GetType(CompositeControls), "MergeChanged_d")> _
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
                e.Handled = True
                e.SuppressKeyPress = True
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
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Action), Description("Raised when usr adds keyword")> _
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
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property KeyWords() As IList(Of String)
            Get
                Return New ListWrapper(Of String)(lstKW.Items)
            End Get
        End Property

        Private Sub lstKW_KeyDown(ByVal sender As ListBox, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstKW.KeyDown
            Select Case e.KeyData
                Case Keys.Delete : RemoveSelectedItems() : e.Handled = True
                Case Keys.C Or Keys.Control : If ShortcutsEnabled Then Copy() : e.Handled = True : e.SuppressKeyPress = True
                Case Keys.X Or Keys.Control : If ShortcutsEnabled Then Cut() : e.Handled = True : e.SuppressKeyPress = True
                Case Keys.V Or Keys.Control : If ShortcutsEnabled Then Paste() : e.Handled = True : e.SuppressKeyPress = True
                Case Keys.A Or Keys.Control : sender.SelectedItems.Clear() : e.Handled = True : e.SuppressKeyPress = True
                    For i As Integer = 0 To sender.Items.Count - 1 : sender.SelectedIndices.Add(i) : Next
            End Select
        End Sub
        ''' <summary>Copies actually selected keywords to clipboard</summary>
        ''' <seelaso cref="Cut"/><seelaso cref="Paste"/>
        ''' <remarks><see cref="KeyWordsEditor"/> uses clipboard format <see cref="TextDataFormat.UnicodeText"/> - one keyword on line</remarks>
        Public Sub Copy()
            Dim CopyData = lstKW.SelectedItems.OfType(Of String).Join(Environment.NewLine)
            My.Computer.Clipboard.SetText(CopyData, TextDataFormat.UnicodeText)
        End Sub
        ''' <summary>Copies actually selected keywords to clipboard and removes them from editor</summary>
        ''' <seelaso cref="Copy"/><seelaso cref="Paste"/>
        ''' <remarks><see cref="KeyWordsEditor"/> uses clipboard format <see cref="TextDataFormat.UnicodeText"/> - one keyword on line</remarks>
        Public Sub Cut()
            Copy()
            RemoveSelectedItems()
        End Sub
        ''' <summary>pastes keywords form clipboard to editor</summary>
        ''' <seelaso cref="Cut"/><seelaso cref="Copy"/>
        ''' <remarks><see cref="KeyWordsEditor"/> uses clipboard format <see cref="TextDataFormat.UnicodeText"/> - one keyword on line</remarks>
        ''' <param name="SuppressMessageBoxes">True not to show any messages to the ueser (if false user may be promped when pasting more than 50 keywords or when any signgle keyword (line in text) is longer than 32 characters)</param>
        ''' <returns>True if any keyword was added. False may be returned due empy clipboard, incompatible data in clipboard, user revocation or because all keywords wa already in list. Lines are separated by <see cref="Environment.NewLine"/> - any other separator causes improperly separated keywords to be pasted.</returns>
        Public Function Paste(Optional ByVal SuppressMessageBoxes As Boolean = False) As Boolean
            If My.Computer.Clipboard.ContainsText(TextDataFormat.UnicodeText) Then
                Dim LinesA As String
                Try : LinesA = My.Computer.Clipboard.GetText(TextDataFormat.UnicodeText)
                Catch : Return False
                End Try
                Dim Lines = LinesA.Split(Environment.NewLine)
                If Lines IsNot Nothing AndAlso Lines.Length > 0 Then
                    If Lines.Count <= 50 OrElse SuppressMessageBoxes OrElse IndependentT.MessageBox.MsgBox(String.Format(WindowsT.FormsT.CompositeControls.PasteManyLines, Lines.Length), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, My.Resources.PasteIcon) = MsgBoxResult.Yes Then
                        Dim Max = (From line In Lines Select line.Length).Max
                        If Max <= 32 OrElse SuppressMessageBoxes OrElse IndependentT.MessageBox.MsgBox(String.Format("Some lines in text in clipboard are up to {0} characters long. Do you want to paste each line as keyword?", Max), MsgBoxStyle.Question Or MsgBoxStyle.YesNo, My.Resources.PasteIcon) = MsgBoxResult.Yes Then
                            Dim Comparer = StringComparer.Create(System.Globalization.CultureInfo.CurrentCulture, Not CaseSensitive)
                            Dim added As Boolean = False
                            Dim addedLines As New List(Of String)
                            For Each line In Lines
                                If Not Me.KeyWords.Contains(line, Comparer) Then _
                                    Me.KeyWords.Add(line) _
                                   : addedLines.Add(line)
                                : added = True
                            Next
                            If added Then
                                lstKW.ClearSelected()
                                For Each NewlyAdded In addedLines
                                    lstKW.SelectedItems.Add(NewlyAdded)
                                Next

                            End If
                            Return added
                        End If
                    End If
                End If
            End If
            Return False
        End Function
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
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Action), LDescription(GetType(CompositeControls), "KeyWordRemoved_d")> _
        Public Event KeyWordRemoved As EventHandler(Of ListWithEvents(Of String).ItemsEventArgs)
        ''' <summary>Contains value of the <see cref="AutoChange"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _AutoChange As Boolean = True
        ''' <summary>Gets or sets value indicating if <see cref="Status">Status</see>.<see cref="StatusMarker.Status">Status</see> automatically changes when keyword is added or removed</summary>
        <LDescription(GetType(CompositeControls), "AutoChange_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <DefaultValue(True)> _
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
        ''' <summary>Stores collection used by <see cref="KeyWordsEditor"/> in <see cref="XDocument"/></summary>
        ''' <param name="Keywords"><see cref="AutoCompleteStable"/> collection. Can be null.</param>
        ''' <param name="Synonyms"><see cref="Synonyms"/> collection. Can be null.</param>
        ''' <returns><see cref="XDocument"/> that stores given collections</returns>
        ''' <seelaso cref="GetKeywordsAsXML"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function GetXML(ByVal Keywords As IEnumerable(Of String), ByVal Synonyms As IEnumerable(Of KeyValuePair(Of String(), String()))) As XDocument
            Return _
              <?xml version="1.0"?>
              <kw:Keywords>
                  <%= If(Keywords IsNot Nothing AndAlso Not Keywords.IsEmpty, _
                      <kw:keywords>
                          <%= From kw In Keywords Select <kw:kw><%= kw %></kw:kw> %>
                      </kw:keywords>, Nothing) _
                  %>
                  <%= If(Synonyms IsNot Nothing AndAlso Not Synonyms.IsEmpty, _
                      <kw:synonyms>
                          <%= From pair In Synonyms Select _
                              <kw:pair>
                                  <kw:keys>
                                      <%= From key In pair.Key Select <kw:key><%= key %></kw:key> %>
                                  </kw:keys>
                                  <%= If(pair.Value IsNot Nothing AndAlso pair.Value.Length > 0, _
                                      <kw:values>
                                          <%= From word In pair.Value Select <kw:word><%= word %></kw:word> %>
                                      </kw:values>, Nothing) _
                                  %>
                              </kw:pair> _
                          %>
                      </kw:synonyms>, Nothing) _
                  %>
              </kw:Keywords>
        End Function
        ''' <summary>Gets all keywords and synonyms used by this <see cref="KeyWordsEditor"/> as <see cref="Xml.Linq.XDocument"/> that can be saved.</summary>
        ''' <remarks><see cref="Xml.Linq.XDocument"/> that contains all the keywords and synonyms ready to be stored in file.</remarks>
        ''' <seelaso cref="LoadFromXML"/>
        Public Function GetKeywordsAsXML() As System.Xml.Linq.XDocument
            Return GetXML(Me.AutoCompleteStable, Me.Synonyms)
        End Function
        ''' <summary>Gets XML-Schema used for storing keywords and synonyms</summary>
        ''' <seelaso cref="LoadFromXML"/><seelaso cref="GetKeywordsAsXML"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared ReadOnly Property XMLSchema() As XmlSchema
            Get
                Static schema As XmlSchema = Nothing
                If schema Is Nothing Then
                    Using SchemaStream = GetType(KeyWordsEditor).Assembly.GetManifestResourceStream("Tools.WindowsT.FormsT.KeyWordsEditor.xsd")
                        Dim Schemas As New XmlSchemaSet()
                        Dim veh As ValidationEventHandler = Function(sender As Object, e As ValidationEventArgs) e.Exception.[DoThrow]
                        schema = XMLSchema.Read(SchemaStream, veh)
                    End Using
                End If
                Return schema
            End Get
        End Property
        ''' <summary>Loads keywords and synonyms from given XML document</summary>
        ''' <param name="doc">Document to load setting from</param>
        ''' <param name="JustThis">True to ensure that load involves only this instance of <see cref="KeyWordsEditor"/>. False to load keywords and sysnonyms to shared collections (if possible).</param>
        ''' <exception cref="ArgumentNullException"><paramref name="doc"/> is null</exception>
        ''' <exception cref="XmlSchemaException">Document <paramref name="doc"/> does not comply with XML-Schema http://dzonny.cz/xml/Tools.WindowsT.FormsT.KeyWordsEditor. This schema is stored as embdeded resource Tools.WindowsT.FormsT.KeyWordsEditor.xsd in this assembly.</exception>
        ''' <remarks>
        ''' <para>If <paramref name="JustThis"/> is true, collections <see cref="AutoCompleteStable"/> and <see cref="Synonyms"/> are re-created and <see cref="AutomaticLists"/> is ste to false.</para>
        ''' <para>If <paramref name="JustThis"/> is false, those collections are emptied and filled with newly loaded values. So, such load has effect on all instances of <see cref="KeyWordsEditor"/> which share same collections (using <see cref="AutomaticLists"/> and same <see cref="AutoCompleteCacheName"/>, or by seting those collections manuallly tu same instance). <paramref name="JustThis"/> set to false is ignored for particular collection if the collection is null (in such case it is always re-created).</para>
        ''' </remarks>
        Public Sub LoadFromXML(ByVal doc As System.Xml.Linq.XDocument, Optional ByVal JustThis As Boolean = False)
            Dim ParsedData = ParseFromXml(doc)
            If JustThis Then
                AutoCompleteStable = New ListWithEvents(Of String)
                Me.Synonyms = New List(Of KeyValuePair(Of String(), String()))
            Else
                If AutoCompleteStable Is Nothing Then AutoCompleteStable = New ListWithEvents(Of String) _
                Else AutoCompleteStable.Clear()
                If Me.Synonyms Is Nothing Then Me.Synonyms = New List(Of KeyValuePair(Of String(), String())) _
                Else Me.Synonyms.Clear()
            End If
            AutoCompleteStable.AddRange(ParsedData.Value1)
            Me.Synonyms.AddRange(ParsedData.Value2)
        End Sub
        ''' <summary>Parses given <see cref="XDocument"/> to collections used by <see cref="KeyWordsEditor"/></summary>
        ''' <param name="doc">Document to be parsed</param>
        ''' <returns><see cref="IPair(Of IEnumerable(Of String), IEnumerable(Of KeyValuePair(Of String(), String())))"/>. <see cref="IPair(Of IEnumerable(Of String), IEnumerable(Of KeyValuePair(Of String(), String()))).Value1"/> contains list of keywords for autocomplete, <see cref="IPair(Of IEnumerable(Of String), IEnumerable(Of KeyValuePair(Of String(), String()))).Value2"/> contains pairs of synonyms.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="doc"/> is null</exception>
        ''' <exception cref="XmlSchemaException">Document <paramref name="doc"/> does not comply with XML-Schema http://dzonny.cz/xml/Tools.WindowsT.FormsT.KeyWordsEditor. This schema is stored as embdeded resource Tools.WindowsT.FormsT.KeyWordsEditor.xsd in this assembly.</exception>
        ''' <seelaso cref="LoadFromXML"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function ParseFromXml(ByVal doc As XDocument) As IPair(Of IEnumerable(Of String), IEnumerable(Of KeyValuePair(Of String(), String())))
            If doc Is Nothing Then Throw New ArgumentNullException("doc")
            Dim Schemas As New XmlSchemaSet()
            Dim veh As ValidationEventHandler = Function(sender As Object, e As ValidationEventArgs) e.Exception.[DoThrow]
            Schemas.Add(XMLSchema)
            doc.Validate(Schemas, veh)
            Dim Keywords = From kw In doc.<kw:Keywords>.<kw:keywords>.<kw:kw> Select kw.Value
            Dim Synonyms = From pair In doc.<kw:Keywords>.<kw:synonyms>.<kw:pair> Select _
                           New KeyValuePair(Of String(), String())( _
                                (From key In pair.<kw:keys>.<kw:key> Select key.Value).ToArray, _
                                (From word In pair.<kw:values>.<kw:word> Select word.Value).ToArray)
            Return New Pair(Of IEnumerable(Of String), IEnumerable(Of KeyValuePair(Of String(), String())))(Keywords, Synonyms)
        End Function

        ''' <summary>Proxy for the <see cref="Status"/> property in design time</summary>
        ''' <remarks>This class supports design-time behaviour of <see cref="KeyWordsEditor"/> and should not be used directly</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public NotInheritable Class StatusProxy
            ''' <summary><see cref="KeyWordsEditor"/> which owns <see cref="StatusMarker"/> this instance is proxy for</summary>
            Private ReadOnly kwe As KeyWordsEditor
            ''' <summary><see cref="StatusMarker"/> this instance is proxy for</summary>
            Private ReadOnly Property sm() As StatusMarker
                <DebuggerStepThrough()> Get
                    Return kwe.Status
                End Get
            End Property
            ''' <summary>CTor</summary>
            ''' <param name="kwe"><see cref="KeyWordsEditor"/> which owns <see cref="StatusMarker"/> this instance is proxy for</param>
            Friend Sub New(ByVal kwe As KeyWordsEditor)
                If kwe Is Nothing Then Throw New ArgumentNullException("kwe")
                Me.kwe = kwe
            End Sub
            ''' <summary>Proxies <see cref="KeyWordsEditor.Status"/>.<see cref="StatusMarker.AddMenuState">AddMenuState</see></summary>
            <DefaultValue(GetType(UtilitiesT.ControlState), "Hidden")> _
            <LDescription(GetType(CompositeControls), "AddMenuState_d")> _
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
            Public Property AddMenuState() As ControlState
                <DebuggerStepThrough()> Get
                    Return sm.AddMenuState
                End Get
                <DebuggerStepThrough()> Set(ByVal value As ControlState)
                    sm.AddMenuState = value
                End Set
            End Property
            ''' <summary>Proxies <see cref="KeyWordsEditor.Status"/>.<see cref="StatusMarker.DeleteMenuState">DeleteMenuState</see></summary>
            <DefaultValue(GetType(UtilitiesT.ControlState), "Enabled")> _
            <LDescription(GetType(CompositeControls), "DeleteMenuState_d")> _
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
            Public Property DeleteMenuState() As ControlState
                <DebuggerStepThrough()> Get
                    Return sm.DeleteMenuState
                End Get
                <DebuggerStepThrough()> Set(ByVal value As ControlState)
                    sm.DeleteMenuState = value
                End Set
            End Property
            ''' <summary>Proxies <see cref="KeyWordsEditor.Status"/>.<see cref="StatusMarker.MarkAsChangedMenuState">MarkAsChangedMenuState</see></summary>
            <DefaultValue(GetType(UtilitiesT.ControlState), "Enabled")> _
           <LDescription(GetType(CompositeControls), "MarkAsChangedMenuState_d")> _
           <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
            Public Property MarkAsChangedMenuState() As ControlState
                <DebuggerStepThrough()> Get
                    Return sm.MarkAsChangedMenuState
                End Get
                <DebuggerStepThrough()> Set(ByVal value As ControlState)
                    sm.MarkAsChangedMenuState = value
                End Set
            End Property
            ''' <summary>Proxies <see cref="KeyWordsEditor.Status"/>.<see cref="StatusMarker.ResetMenuState">ResetMenuState</see></summary>
            <DefaultValue(GetType(UtilitiesT.ControlState), "Enabled")> _
            <LDescription(GetType(CompositeControls), "ResetMenuState_d")> _
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
            Public Property ResetMenuState() As ControlState
                <DebuggerStepThrough()> Get
                    Return sm.ResetMenuState
                End Get
                <DebuggerStepThrough()> Set(ByVal value As ControlState)
                    sm.ResetMenuState = value
                End Set
            End Property
            ''' <summary>Proxies <see cref="KeyWordsEditor.Status"/>.<see cref="StatusMarker.AutoChanged">AutoChanged</see></summary>
            <DefaultValue(False)> _
            <LDescription(GetType(CompositeControls), "AutoChanged_d")> _
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
            Public Property AutoChanged() As Boolean
                <DebuggerStepThrough()> Get
                    Return sm.AutoChanged
                End Get
                <DebuggerStepThrough()> Set(ByVal value As Boolean)
                    sm.AutoChanged = value
                End Set
            End Property
            ''' <summary>Proxies <see cref="KeyWordsEditor.Status"/>.<see cref="StatusMarker.Status">Status</see></summary>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="StatusMarker.Statuses"/></exception>
            <DefaultValue(GetType(StatusMarker.Statuses), "Normal")> _
            <LDescription(GetType(CompositeControls), "StatusMarkerStatus_d")> _
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
            Public Property Status() As StatusMarker.Statuses
                <DebuggerStepThrough()> Get
                    Return sm.Status
                End Get
                <DebuggerStepThrough()> Set(ByVal value As StatusMarker.Statuses)
                    sm.Status = value
                End Set
            End Property
            ''' <summary>Gets value indicatiiong if any of properties should be serialized</summary>
            ''' <returns>True if any of properties has value different from default</returns>
            Friend ReadOnly Property ShouldSerialize() As Boolean
                Get
                    Return AddMenuState <> ControlState.Hidden OrElse DeleteMenuState <> ControlState.Enabled OrElse MarkAsChangedMenuState <> ControlState.Enabled OrElse ResetMenuState <> ControlState.Enabled OrElse AutoChanged <> False OrElse Status <> StatusMarker.Statuses.Normal
                End Get
            End Property
        End Class
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            TmiEnabled()
        End Sub
        ''' <summary>Contains value of the <see cref="ContextMenuEnabled"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ContextMenuEnabled As Boolean = True
        ''' <summary>Contains value of the <see cref="ShortcutsEnabled"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShortcutsEnabled As Boolean = True
        ''' <summary>Gets or sets value indicating if context menu for <see cref="ListBox"/> which shows keybords is enabled</summary>
        ''' <value>Default value is true</value>
        ''' <returns>True if it is enabled, false if it is not</returns>
        <DefaultValue(True), KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(CompositeControls), "ContextMenuEnabled_d")> _
        Public Property ContextMenuEnabled() As Boolean
            <DebuggerStepThrough()> Get
                Return _ContextMenuEnabled
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Boolean)
                _ContextMenuEnabled = value
            End Set
        End Property
        ''' <summary>Gets or sets value indicating if keyboard shortcuts for clipboard operations are allowed for keywords</summary>
        ''' <value>Default value is true</value>
        ''' <returns>True if shortcuts are enabled, false if they are not</returns>
        <DefaultValue(True), KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(CompositeControls), "ShortcutsEnabled_d")> _
        Public Property ShortcutsEnabled() As Boolean
            <DebuggerStepThrough()> Get
                Return _ShortcutsEnabled
            End Get
            Set(ByVal value As Boolean)
                Dim old = ShortcutsEnabled
                _ShortcutsEnabled = value
                If value <> old Then
                    If value Then
                        tmiCut.ShortcutKeyDisplayString = "Ctrl+X"
                        tmiCopy.ShortcutKeyDisplayString = "Ctrl+C"
                        tmiPaste.ShortcutKeyDisplayString = "Ctrl+V"
                    Else
                        tmiCut.ShortcutKeyDisplayString = ""
                        tmiCopy.ShortcutKeyDisplayString = ""
                        tmiPaste.ShortcutKeyDisplayString = ""
                    End If
                End If
            End Set
        End Property
        Private Sub lstKW_MouseDown(ByVal sender As ListBox, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstKW.MouseDown
            If e.Button = System.Windows.Forms.MouseButtons.Right Then
                If Not ContextMenuEnabled Then Exit Sub
                tmiCopy.Enabled = sender.SelectedItems.Count > 0
                tmiCut.Enabled = tmiCopy.Enabled
                tmiDelete.Enabled = tmiCopy.Enabled
                tmiPaste.Enabled = My.Computer.Clipboard.ContainsText(TextDataFormat.UnicodeText)
                If tmiCopy.Enabled OrElse tmiPaste.Enabled Then
                    cmsContext.Show(sender, e.Location)
                End If
            End If
        End Sub

        Private Sub tmiCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiCut.Click
            Cut()
        End Sub

        Private Sub tmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiCopy.Click
            Copy()
        End Sub

        Private Sub tmiPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiPaste.Click
            Paste()
        End Sub

        Private Sub tmiDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiDelete.Click
            RemoveSelectedItems()
        End Sub
    End Class
End Namespace