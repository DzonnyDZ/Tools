'Extracted
Imports Tools.CollectionsT.GenericT, System.Windows.Forms, System.Linq
Imports MBox = Tools.WindowsT.IndependentT.MessageBox
Namespace WindowsT.FormsT
    '#If Config <= Alpha Then Set in Tools.vbproj
    'Stage: Alpha
    'Localize: UI
    ''' <summary>Editor of autocomplete list and synonym groups for <see cref="KeyWordsEditor"/></summary>
    Friend NotInheritable Class ThesaurusForm
        ''' <summary><see cref="KeyWordsEditor"/> this instance is for</summary>
        Private [For] As KeyWordsEditor
        ''' <summary>Reference to auto-complete cache of currently edited <see cref="KeyWordsEditor"/></summary>
        Private WithEvents AutoCompleteCache As ListWithEvents(Of String)
        'Private BackupStable As List(Of String)
        Private BackupChache As List(Of String)
        ''' <summary>CTor</summary>
        ''' <param name="For"><see cref="KeyWordsEditor"/> to be dialog for</param>
        Public Sub New(ByVal [For] As KeyWordsEditor)
            InitializeComponent()
            Me.For = [For]
            initialize()
        End Sub
        ''' <summary>Initializes form</summary>
        Private Sub Initialize()
            kweAutoComplete.AutoCompleteCacheName = Me.For.AutoCompleteCacheName
            kweKeys.AutoCompleteCacheName = Me.For.AutoCompleteCacheName
            kweValues.AutoCompleteCacheName = Me.For.AutoCompleteCacheName
            kweKeys.AutoCompleteStable = Me.For.AutoCompleteStable
            kweValues.AutoCompleteStable = Me.For.AutoCompleteStable
            splAutoComplete.Panel1.Enabled = Me.For.AutoCompleteStable IsNot Nothing
            splAutoComplete.Panel2.Enabled = Me.For.AutoCompleteCacheName <> ""
            splVertical.Panel2.Enabled = Me.For.Synonyms IsNot Nothing
            kweAutoComplete.CaseSensitive = Me.For.CaseSensitive
            kweKeys.CaseSensitive = Me.For.CaseSensitive
            kweValues.CaseSensitive = Me.For.CaseSensitive

            kweAutoComplete.KeyWords.Clear()
            If Me.For.AutoCompleteStable IsNot Nothing Then
                'BackupStable = New List(Of String)(Me.For.AutoCompleteStable)
                For Each KW As String In Me.For.AutoCompleteStable
                    kweAutoComplete.KeyWords.Add(KW)
                Next KW
            End If
            If Me.For.AutoCompleteCacheName <> "" Then
                BackupChache = New List(Of String)(Me.For.InstanceAutoCompleteChache)
                ShowCache()
                AutoCompleteCache = Me.For.InstanceAutoCompleteChache
            End If
            cmbSyn.Items.Clear()
            If Me.For.Synonyms IsNot Nothing Then
                cmbSyn.DisplayMember = "DisplayMember"
                For Each Syn As KeyValuePair(Of String(), String()) In Me.For.Synonyms
                    cmbSyn.Items.Add(New SynProxy(Syn))
                Next Syn
                If cmbSyn.Items.Count > 0 Then cmbSyn.SelectedIndex = 0
            End If
        End Sub
        ''' <summary>Proxy of <see cref="KeyValuePair(Of String(), String())"/> for <see cref="ComboBox"/></summary>
        Private NotInheritable Class SynProxy
            ''' <summary>Value being proxied</summary>
            Public Syns As KeyValuePair(Of String(), String())
            ''' <summary>CTor</summary>
            ''' <param name="Syns">Value to be proxied</param>
            Public Sub New(ByVal Syns As KeyValuePair(Of String(), String()))
                Me.Syns = Syns
            End Sub
            ''' <summary>Display member for <see cref="ComboBox"/></summary>
            ''' <returns>First item from <see cref="Syns">Syns</see>.<see cref="KeyValuePair(Of String(), String()).Key">Key</see></returns>
            Public ReadOnly Property DisplayMember() As String
                Get
                    If Me.Syns.Key IsNot Nothing AndAlso Me.Syns.Key.Length > 0 Then
                        Return Me.Syns.Key(0)
                    Else
                        Return ""
                    End If
                End Get
            End Property
        End Class
        ''' <summary>Shows content of autocomplete cache</summary>
        Private Sub ShowCache()
            Dim sb As New System.Text.StringBuilder
            For Each KW As String In Me.For.InstanceAutoCompleteChache
                If sb.Length <> 0 Then sb.Append(", ")
                sb.Append(KW)
            Next KW
            lblCache.Text = sb.ToString
        End Sub

        Private Sub AutoCompleteCache_Added_Removed(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of String), ByVal e As CollectionsT.GenericT.ListWithEvents(Of String).ItemIndexEventArgs) Handles AutoCompleteCache.Added, AutoCompleteCache.Removed
            ShowCache()
        End Sub

        Private Sub AutoCompleteCache_Cleared(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of String), ByVal e As CollectionsT.GenericT.ListWithEvents(Of String).ItemsEventArgs) Handles AutoCompleteCache.Cleared
            ShowCache()
        End Sub

        Private Sub AutoCompleteCache_Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Handles AutoCompleteCache.Changed
            ShowCache()
        End Sub

        Private Sub cmdClearCache_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearCache.Click
            If AutoCompleteCache IsNot Nothing Then AutoCompleteCache.Clear()
        End Sub
        ''' <summary>Previosly selected item in <see cref="cmbSyn"/></summary>
        Private OldItem As SynProxy = Nothing
        Private Sub cmbSyn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSyn.SelectedIndexChanged
            If OldItem IsNot Nothing Then
                StoreOldItem()
            End If
            If cmbSyn.SelectedIndex < 0 Then
                fraKeys.Enabled = False
                fraValues.Enabled = False
                cmdDelSyn.Enabled = False
                OldItem = Nothing
            Else
                fraKeys.Enabled = True
                fraValues.Enabled = True
                cmdDelSyn.Enabled = True
                OldItem = cmbSyn.SelectedItem
                With OldItem
                    kweKeys.KeyWords.Clear()
                    If .Syns.Key IsNot Nothing Then
                        For Each key As String In .Syns.Key
                            kweKeys.KeyWords.Add(key)
                        Next key
                    End If
                    kweValues.KeyWords.Clear()
                    If .Syns.Value IsNot Nothing Then
                        For Each value As String In .Syns.Value
                            kweValues.KeyWords.Add(value)
                        Next value
                    End If
                End With
            End If
        End Sub

        ''' <summary>Stores keywords from <see cref="kweValues"/> and <see cref="kweKeys"/> into <see cref="OldItem"/></summary>
        Private Sub StoreOldItem()
            If OldItem IsNot Nothing Then
                Dim Keys, Values As String()
                If kweKeys.KeyWords.Count > 0 Then
                    ReDim Keys(kweKeys.KeyWords.Count - 1)
                    Dim i As Integer = 0
                    For Each Item As String In kweKeys.KeyWords
                        Keys(i) = Item
                        i += 1
                    Next Item
                Else
                    Keys = New String() {}
                End If
                If kweValues.KeyWords.Count > 0 Then
                    ReDim Values(kweValues.KeyWords.Count - 1)
                    Dim i As Integer = 0
                    For Each Item As String In kweValues.KeyWords
                        Values(i) = Item
                        i += 1
                    Next Item
                Else
                    Values = New String() {}
                End If
                OldItem.Syns = New KeyValuePair(Of String(), String())(Keys, Values)
            End If
        End Sub

        Private Sub cmdAddSyn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddSyn.Click
            Dim NewSyn As New SynProxy(New KeyValuePair(Of String(), String())(New String() {cmbSyn.Text}, New String() {}))
            cmbSyn.Items.Add(NewSyn)
            cmbSyn.SelectedItem = NewSyn
        End Sub

        Private Sub cmdDelSyn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelSyn.Click
            If cmbSyn.SelectedItem IsNot Nothing Then
                Dim Index As Integer = cmbSyn.SelectedIndex
                OldItem = Nothing
                cmbSyn.Items.RemoveAt(cmbSyn.SelectedIndex)
                If cmbSyn.Items.Count > Index Then
                    cmbSyn.SelectedIndex = Index
                ElseIf cmbSyn.Items.Count > 0 Then
                    cmbSyn.SelectedIndex = cmbSyn.Items.Count - 1
                Else
                    cmbSyn.Text = ""
                    fraKeys.Enabled = False
                    fraValues.Enabled = False
                    cmdDelSyn.Enabled = False
                    OldItem = Nothing
                End If
            End If
        End Sub

        Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
            'Store synonyms
            PerformOK()
            Me.Close()
        End Sub
        Private Sub PerformOK()
            StoreOldItem()
            If Me.For.Synonyms IsNot Nothing Then
                Me.For.Synonyms.Clear()
                For Each Syn As SynProxy In cmbSyn.Items
                    Me.For.Synonyms.Add(Syn.Syns)
                Next Syn
            End If
            If Me.For.AutoCompleteStable IsNot Nothing Then
                Me.For.AutoCompleteStable.Clear()
                For Each Stable As String In kweAutoComplete.KeyWords
                    Me.For.AutoCompleteStable.Add(Stable)
                Next Stable
            End If
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            'Repair backups
            'If Me.For.AutoCompleteStable IsNot Nothing Then
            '    Me.For.AutoCompleteStable.Clear()
            '    For Each Backup As String In BackupStable
            '        Me.For.AutoCompleteStable.Add(Backup)
            '    Next Backup
            'End If
            If Me.For.InstanceAutoCompleteChache IsNot Nothing Then
                With Me.For.InstanceAutoCompleteChache
                    .Clear()
                    For Each Backup As String In BackupChache
                        .Add(Backup)
                    Next Backup
                End With
            End If
            Me.Close()
        End Sub

        Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            If MBox.ModalEx(My.Resources.ThisActionRequiresAllChangesToBeConfirmed, My.Resources.ConfirmChanges, New Object() {New MBox.MessageBoxButton(My.Resources.Continue_, Windows.Forms.DialogResult.OK, My.Resources.Continue_AccessKey), New MBox.MessageBoxButton(DialogResult.OK)}).DialogResult = Windows.Forms.DialogResult.OK AndAlso sfdSave.ShowDialog = Windows.Forms.DialogResult.OK Then
                PerformOK()
Retry:          Try
                    Me.For.GetKeywordsAsXML.Save(sfdSave.FileName)
                Catch ex As Exception
                    If MBox.Error(ex, My.Resources.Error_, IndependentT.MessageBox.MessageBoxButton.Buttons.Retry Or IndependentT.MessageBox.MessageBoxButton.Buttons.Cancel) = Windows.Forms.DialogResult.Retry AndAlso sfdSave.ShowDialog = Windows.Forms.DialogResult.OK Then GoTo Retry
                End Try
            End If
        End Sub

        Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
            If ofdLoad.ShowDialog = Windows.Forms.DialogResult.OK Then
                Try
                    Me.For.LoadFromXML(Xml.Linq.XDocument.Load(ofdLoad.FileName))
                Catch ex As Exception
                    MBox.Error(ex, My.Resources.Error_)
                    Exit Sub
                End Try
                Me.Initialize()
            End If
        End Sub
    End Class
    '#End If
End Namespace