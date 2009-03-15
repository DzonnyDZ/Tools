'Extracted
Imports System.Windows.Forms, Tools.WindowsT, System.ComponentModel, System.Linq
Imports Tools.WindowsT.FormsT.UtilitiesT.WinFormsExtensions, Tools.CollectionsT.SpecializedT, Tools.CollectionsT.GenericT
Imports iMsg = Tools.WindowsT.IndependentT.MessageBox
'#If Config <= Nightly Then 'Set in project file
'Stage:Nightly
Namespace WindowsT.FormsT
    ''' <summary>Implements GUI (form) for <see cref="MessageBox"/></summary>
    ''' <remarks>
    ''' <para>This class implements <see cref="iMsg"/> in fully dynamic way. You can change all it's properties and all properties of its controls and those change are immediatelly displayed to user.</para>
    ''' If you are passing some very custom implementation of <see cref="MessageBox"/> to this form, you must ensure that :
    ''' <list type="list">
    ''' <item>Argument e of the <see cref="MessageBox.ComboBox"/>.<see cref="MessageBox.MessageBoxComboBox.Items">Items</see>.<see cref="ListWithEvents.CollectionChanged"/> event has always <see cref="ListWithEvents.ListChangedEventArgs.Action">Action</see> which is member of <see cref="CollectionsT.GenericT.CollectionChangeAction"/>.</item>
    ''' <item>Argument e of the <see cref="MessageBox.ButtonsChanged"/> and <see cref="MessageBox.RadiosChanged"/> events has always <see cref="ListWithEvents.ListChangedEventArgs.Action">Action</see> which is member of <see cref="CollectionsT.GenericT.CollectionChangeAction"/> and is not <see cref="CollectionsT.GenericT.CollectionChangeAction.Other"/>.</item>
    ''' </list>
    ''' Violating these rules can lead to uncatchable <see cref="InvalidOperationException"/> being thrown when event occurs.
    ''' </remarks>
    ''' <version version="1.5.2">Fixed: Check box has not enough size.</version>
    ''' <version version="1.5.2"><see cref="Button.DialogResult"/> is no longer set (it caused dialog to be closed even when event was cancelled).</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Class MessageBoxForm : Inherits Form
        ''' <summary>Form overrides dispose to clean up the component list.</summary>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    CommonDisposeActions()
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub
        ''' <summary>Common actions taken when form is closed or disposed</summary>
        Private Sub CommonDisposeActions()
            For Each CheckBox In MessageBox.CheckBoxes
                CheckBox.Control = Nothing
                AttachCheckBoxHandlers(CheckBox, False)
            Next
            If MessageBox.ComboBox IsNot Nothing Then MessageBox.ComboBox.Control = Nothing : AttachComboBoxHandlers(MessageBox.ComboBox, False)
            For Each Button In MessageBox.Buttons
                Button.Control = Nothing
                AttachButtonHandlers(Button, False)
            Next
            For Each Radio In MessageBox.Radios
                Radio.Control = Nothing
                AttachRadioHandlers(Radio, False)
            Next
            AttachMessageBoxHandlers(False)
        End Sub
        ''' <summary>Contains value of the <see cref="MessageBox"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _MessageBox As MessageBox
        ''' <summary>Gets <see cref="FormsT.MessageBox"/> this forms is GUI for</summary>
        Public ReadOnly Property MessageBox() As MessageBox
            <DebuggerStepThroughAttribute()> Get
                Return _MessageBox
            End Get
        End Property
        ''' <summary>CTor</summary>
        ''' <param name="MessageBox"><see cref="FormsT.MessageBox"/> to initialize this form by</param>
        Public Sub New(ByVal MessageBox As MessageBox)
            Me._MessageBox = MessageBox
            Me.InitializeComponent()
            Initialize()
        End Sub
        ''' <summary>Applies <see cref="MessageBox"/>.<see cref="MessageBox.Options">Options</see></summary>
        Private Sub ApplyOptions()
            Select Case MessageBox.Options And IndependentT.MessageBox.MessageBoxOptions.AlignMask
                Case IndependentT.MessageBox.MessageBoxOptions.AlignLeft, IndependentT.MessageBox.MessageBoxOptions.AlignJustify
                    lblPrompt.TextAlign = Drawing.ContentAlignment.TopLeft
                    lblPrompt.Anchor = AnchorStyles.Left Or AnchorStyles.Top
                Case IndependentT.MessageBox.MessageBoxOptions.AlignRight
                    lblPrompt.TextAlign = Drawing.ContentAlignment.TopRight
                    lblPrompt.Anchor = AnchorStyles.Right Or AnchorStyles.Top
                Case IndependentT.MessageBox.MessageBoxOptions.AlignCenter
                    lblPrompt.TextAlign = Drawing.ContentAlignment.TopCenter
                    lblPrompt.Anchor = AnchorStyles.Top
            End Select
            If (MessageBox.Options And IndependentT.MessageBox.MessageBoxOptions.Rtl) = IndependentT.MessageBox.MessageBoxOptions.Rtl Then _
                Me.RightToLeft = Windows.Forms.RightToLeft.Yes _
                Else Me.RightToLeft = Windows.Forms.RightToLeft.No
        End Sub
        ''' <summary>Initializes tis form by <see cref="MessageBox"/>, called from CTor</summary>
        Protected Overridable Sub Initialize()
            'Options
            ApplyOptions()
            'Title
            If MessageBox.Title Is Nothing Then
                Dim App As New Microsoft.VisualBasic.ApplicationServices.AssemblyInfo(System.Reflection.Assembly.GetEntryAssembly)
                Me.Text = App.Title
            Else
                Me.Text = MessageBox.Title
            End If
            'Top control
            TopControl = MessageBox.TopControlControl
            If TopControl IsNot Nothing Then tlpMain.ReplaceControl(lblPlhTop, TopControl)
            'Icon
            If MessageBox.Icon Is Nothing Then
                picPicture.Visible = False
            Else
                picPicture.Image = MessageBox.Icon
            End If
            'Propmpt
            lblPrompt.Text = MessageBox.Prompt
            'Checkbox
            flpChecks.Controls.Clear()
            If MessageBox.CheckBoxes.Count <= 0 Then flpChecks.Visible = False _
            Else flpChecks.Controls.AddRange((From Check In MessageBox.CheckBoxes Select CreateCheckBox(Check)).ToArray)
            'Combobox
            ApplyComboBox()
            'Radios
            flpRadio.Controls.Clear()
            If MessageBox.Radios.Count <= 0 Then flpRadio.Visible = False _
            Else flpRadio.Controls.AddRange((From Radio In MessageBox.Radios Select CreateRadio(Radio)).ToArray)
            'Mic control
            MidControl = MessageBox.MidControlControl
            If MidControl IsNot Nothing Then tlpMain.ReplaceControl(lblPlhMid, MidControl)
            'Buttons
            flpButtons.Controls.Clear()
            If MessageBox.Buttons.Count <= 0 Then
                flpButtons.Visible = False
            Else
                Dim CancelButton As Button = Nothing
                Dim i As Integer = 0
                For Each Button In MessageBox.Buttons
                    flpButtons.Controls.Add(CreateButton(Button))
                    If MessageBox.CloseResponse = Button.Result AndAlso CancelButton Is Nothing Then CancelButton = flpButtons.Controls.Last
                    If MessageBox.DefaultButton = i Then Me.AcceptButton = flpButtons.Controls.Last
                    i += 1
                Next Button
                Me.CancelButton = CancelButton
            End If
            'Bottom control
            BottomControl = MessageBox.BottomControlControl
            If BottomControl IsNot Nothing Then tlpMain.ReplaceControl(lblPlhBottom, BottomControl)
            'Handlers
            AttachMessageBoxHandlers()
            AttachComboBoxHandlers(MessageBox.ComboBox)
            For Each CheckBox In MessageBox.CheckBoxes
                AttachCheckBoxHandlers(CheckBox)
            Next
            For Each Button In MessageBox.Buttons
                AttachButtonHandlers(Button)
            Next
            For Each Radio In MessageBox.Radios
                AttachRadioHandlers(Radio)
            Next
        End Sub
        ''' <summary>Additional control on messagebox below buttons</summary>
        Private BottomControl As Control
        ''' <summary>Additional control on messagebox abow prompt</summary>
        Private TopControl As Control
        ''' <summary>Additional control on messagebox abow buttons</summary>
        Private MidControl As Control
#Region "Attach / detach"
        ''' <summary>Attachs or detachs handlers for <see cref="MessageBox.MessageBoxComboBox"/></summary>
        ''' <param name="ComboBox"><see cref="MessageBox.MessageBoxComboBox"/> to attach handlers to</param>
        ''' <param name="attach">True to attach, false so detach</param>
        Private Sub AttachComboBoxHandlers(ByVal ComboBox As MessageBox.MessageBoxComboBox, Optional ByVal attach As Boolean = True)
            If ComboBox Is Nothing Then Exit Sub
            If attach Then
                AddHandler ComboBox.DisplayMemberChanged, AddressOf ComboBox_DisplayMemberChanged
                AddHandler ComboBox.EditableChanged, AddressOf ComboBox_EditableChanged
                AddHandler ComboBox.EnabledChanged, AddressOf Control_EnabledChanged
                AddHandler ComboBox.ItemsChanged, AddressOf ComboBox_ItemsChanged
                AddHandler ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
                AddHandler ComboBox.SelectedItemChanged, AddressOf ComboBox_selectedItemChanged
                AddHandler ComboBox.TextChanged, AddressOf Control_TextChanged
                AddHandler ComboBox.ToolTipChanged, AddressOf Control_ToolTipChanged
            Else
                RemoveHandler ComboBox.DisplayMemberChanged, AddressOf ComboBox_DisplayMemberChanged
                RemoveHandler ComboBox.EditableChanged, AddressOf ComboBox_EditableChanged
                RemoveHandler ComboBox.EnabledChanged, AddressOf Control_EnabledChanged
                RemoveHandler ComboBox.ItemsChanged, AddressOf ComboBox_ItemsChanged
                RemoveHandler ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
                RemoveHandler ComboBox.SelectedItemChanged, AddressOf ComboBox_selectedItemChanged
                RemoveHandler ComboBox.TextChanged, AddressOf Control_TextChanged
                RemoveHandler ComboBox.ToolTipChanged, AddressOf Control_ToolTipChanged
            End If
        End Sub
        ''' <summary>Attachs or detachs handlers for <see cref="MessageBox.MessageBoxCheckBox"/></summary>
        ''' <param name="CheckBox"><see cref="MessageBox.MessageBoxCheckBox"/> to attach handlers to</param>
        ''' <param name="attach">True to attach, false so detach</param>
        Private Sub AttachCheckBoxHandlers(ByVal CheckBox As MessageBox.MessageBoxCheckBox, Optional ByVal attach As Boolean = True)
            If CheckBox Is Nothing Then Exit Sub
            If attach Then
                AddHandler CheckBox.EnabledChanged, AddressOf Control_EnabledChanged
                AddHandler CheckBox.StateChanged, AddressOf CheckBox_StateChanged
                AddHandler CheckBox.TextChanged, AddressOf Control_TextChanged
                AddHandler CheckBox.ThreeStateChanged, AddressOf CheckBox_ThreeStateChanged
                AddHandler CheckBox.ToolTipChanged, AddressOf Control_ToolTipChanged
            Else
                RemoveHandler CheckBox.EnabledChanged, AddressOf Control_EnabledChanged
                RemoveHandler CheckBox.StateChanged, AddressOf CheckBox_StateChanged
                RemoveHandler CheckBox.TextChanged, AddressOf Control_TextChanged
                RemoveHandler CheckBox.ThreeStateChanged, AddressOf CheckBox_ThreeStateChanged
                RemoveHandler CheckBox.ToolTipChanged, AddressOf Control_ToolTipChanged
            End If
        End Sub
        ''' <summary>Attachs or detachs handlers for <see cref="MessageBox.MessageBoxButton"/></summary>
        ''' <param name="Button"><see cref="MessageBox.MessageBoxButton"/> to attach handlers to</param>
        ''' <param name="attach">True to attach, false so detach</param>
        Private Sub AttachButtonHandlers(ByVal Button As MessageBox.MessageBoxButton, Optional ByVal attach As Boolean = True)
            If Button Is Nothing Then Exit Sub
            If attach Then
                AddHandler Button.AccessKeyChanged, AddressOf Button_AccessKeyChanged
                AddHandler Button.EnabledChanged, AddressOf Control_EnabledChanged
                AddHandler Button.ResultChanged, AddressOf Button_ResultChanged
                AddHandler Button.TextChanged, AddressOf Control_TextChanged
                AddHandler Button.ToolTipChanged, AddressOf Control_ToolTipChanged
            Else
                RemoveHandler Button.AccessKeyChanged, AddressOf Button_AccessKeyChanged
                RemoveHandler Button.EnabledChanged, AddressOf Control_EnabledChanged
                RemoveHandler Button.ResultChanged, AddressOf Button_ResultChanged
                RemoveHandler Button.TextChanged, AddressOf Control_TextChanged
                RemoveHandler Button.ToolTipChanged, AddressOf Control_ToolTipChanged
            End If
        End Sub
        ''' <summary>Attachs or detachs handlers for <see cref="MessageBox.MessageBoxRadioButton"/></summary>
        ''' <param name="Radio"><see cref="MessageBox.MessageBoxRadioButton"/> to attach handlers to</param>
        ''' <param name="attach">True to attach, false so detach</param>
        Private Sub AttachRadioHandlers(ByVal Radio As MessageBox.MessageBoxRadioButton, Optional ByVal attach As Boolean = True)
            If Radio Is Nothing Then Exit Sub
            If attach Then
                AddHandler Radio.EnabledChanged, AddressOf Control_EnabledChanged
                AddHandler Radio.TextChanged, AddressOf Control_TextChanged
                AddHandler Radio.ToolTipChanged, AddressOf Control_ToolTipChanged
                AddHandler Radio.CheckedChanged, AddressOf Radio_CheckedChanged
            Else
                RemoveHandler Radio.EnabledChanged, AddressOf Control_EnabledChanged
                RemoveHandler Radio.TextChanged, AddressOf Control_TextChanged
                RemoveHandler Radio.ToolTipChanged, AddressOf Control_ToolTipChanged
                RemoveHandler Radio.CheckedChanged, AddressOf Radio_CheckedChanged
            End If
        End Sub
        ''' <summary>Attachs or tetachs handlers for <see cref="MessageBox"/></summary>
        ''' <param name="attach">True (default) to attach, false to detach</param>
        Private Sub AttachMessageBoxHandlers(Optional ByVal attach As Boolean = True)
            If attach Then
                AddHandler MessageBox.BottomControlChanged, AddressOf__MessageBox_BottomControlChanged
                AddHandler MessageBox.TopControlChanged, AddressOf__MessageBox_TopControlChanged
                AddHandler MessageBox.MidControlChanged, AddressOf__MessageBox_MidControlChanged
                AddHandler MessageBox.CloseResponseChanged, AddressOf__MessageBox_CloseResponseChanged
                AddHandler MessageBox.ComboBoxChanged, AddressOf__MessageBox_ComboBoxChanged
                AddHandler MessageBox.CountDown, AddressOf__MessageBox_CountDown
                AddHandler MessageBox.DefaultButtonChanged, AddressOf__MessageBox_DefaultButtonChanged
                AddHandler MessageBox.CheckBoxesChanged, AddressOf__MessageBox_CheckBoxesChanged
                AddHandler MessageBox.PromptChanged, AddressOf__MessageBox_PromptChanged
                AddHandler MessageBox.TitleChanged, AddressOf__MessageBox_TitleChanged
                AddHandler MessageBox.ButtonsChanged, AddressOf__MessageBox_ButtonsChanged
                AddHandler MessageBox.RadiosChanged, AddressOf__MessageBox_RadiosChanged
                AddHandler MessageBox.IconChanged, AddressOf__MessageBox_IconChanged
                AddHandler MessageBox.OptionsChanged, AddressOf__MessageBox_OptionsChanged
            Else
                RemoveHandler MessageBox.BottomControlChanged, AddressOf__MessageBox_BottomControlChanged
                RemoveHandler MessageBox.TopControlChanged, AddressOf__MessageBox_TopControlChanged
                RemoveHandler MessageBox.MidControlChanged, AddressOf__MessageBox_MidControlChanged
                RemoveHandler MessageBox.CloseResponseChanged, AddressOf__MessageBox_CloseResponseChanged
                RemoveHandler MessageBox.ComboBoxChanged, AddressOf__MessageBox_ComboBoxChanged
                RemoveHandler MessageBox.CountDown, AddressOf__MessageBox_CountDown
                RemoveHandler MessageBox.DefaultButtonChanged, AddressOf__MessageBox_DefaultButtonChanged
                RemoveHandler MessageBox.CheckBoxesChanged, AddressOf__MessageBox_CheckBoxesChanged
                RemoveHandler MessageBox.PromptChanged, AddressOf__MessageBox_PromptChanged
                RemoveHandler MessageBox.TitleChanged, AddressOf__MessageBox_TitleChanged
                RemoveHandler MessageBox.ButtonsChanged, AddressOf__MessageBox_ButtonsChanged
                RemoveHandler MessageBox.RadiosChanged, AddressOf__MessageBox_RadiosChanged
                RemoveHandler MessageBox.IconChanged, AddressOf__MessageBox_IconChanged
                RemoveHandler MessageBox.OptionsChanged, AddressOf__MessageBox_OptionsChanged
            End If
        End Sub
#End Region
        ''' <summary>Applyes the <see cref="ComboBox"/> onto <see cref="cmbCombo"/></summary>
        Private Sub ApplyComboBox()
            If MessageBox.ComboBox Is Nothing Then
                cmbCombo.Visible = False
            Else
                cmbCombo.Visible = True
                MessageBox.ComboBox.Control = cmbCombo
                cmbCombo.DropDownStyle = If(MessageBox.ComboBox.Editable, ComboBoxStyle.DropDown, ComboBoxStyle.DropDownList)
                cmbCombo.Enabled = MessageBox.ComboBox.Enabled
                cmbCombo.DisplayMember = MessageBox.ComboBox.DisplayMember
                For Each item In MessageBox.ComboBox.Items
                    cmbCombo.Items.Add(item)
                Next item
                If MessageBox.ComboBox.SelectedItem IsNot Nothing Then cmbCombo.SelectedItem = MessageBox.ComboBox.SelectedItem
                If MessageBox.ComboBox.SelectedIndex >= 0 Then cmbCombo.SelectedIndex = MessageBox.ComboBox.SelectedIndex
                If MessageBox.ComboBox.Editable AndAlso MessageBox.ComboBox.Text <> "" Then cmbCombo.Text = MessageBox.ComboBox.Text
                If MessageBox.ComboBox.ToolTip <> "" Then totToolTip.SetToolTip(cmbCombo, MessageBox.ComboBox.ToolTip) _
                Else totToolTip.SetToolTip(cmbCombo, "")
            End If
        End Sub
        ''' <summary>Creates <see cref="CheckBox"/> from given <see cref="iMsg.MessageBoxCheckBox"/></summary>
        ''' <param name="Chk">A <see cref="iMsg.MessageBoxCheckBox"/> to create <see cref="CheckBox"/> from</param>
        ''' <returns><see cref="CheckBox"/> initiated by <paramref name="Chk"/></returns>
        Private Function CreateCheckBox(ByVal Chk As MessageBox.MessageBoxCheckBox) As CheckBox
            Dim ret As New CheckBox With {.Text = Chk.Text, .Tag = Chk, .CheckState = Chk.State, .ThreeState = Chk.ThreeState, .Enabled = Chk.Enabled, .Anchor = AnchorStyles.None, .AutoSize = True}
            If Chk.ToolTip <> "" Then totToolTip.SetToolTip(ret, Chk.ToolTip)
            Chk.Control = ret
            AddHandler ret.CheckStateChanged, AddressOf__CheckBox_CheckStateChanged
            Return ret
        End Function
        ''' <summary>Creates <see cref="Button"/> from <see cref="MessageBox.MessageBoxButton"/></summary>
        ''' <param name="Button"><see cref="MessageBox.MessageBoxButton"/> to initialize new <see cref="Button"/> with</param>
        ''' <returns>Newly created <see cref="Button"/> with attached <see cref="Button.Click"/> event to <see cref="Button_Click"/></returns>
        Private Function CreateButton(ByVal Button As MessageBox.MessageBoxButton) As Button
            Dim text As String = If(Button.Text IsNot Nothing, Button.Text.Replace("&", "&&"), "")
            If Button.AccessKey <> vbNullChar AndAlso text.IndexOf(Button.AccessKey) >= 0 Then text = text.Insert(text.IndexOf(Button.AccessKey), "&")
            Dim CmdButton As New Button With {.Text = text, .Enabled = Button.Enabled, .Tag = Button, .AutoSize = True, .AutoSizeMode = Windows.Forms.AutoSizeMode.GrowAndShrink, .Anchor = AnchorStyles.None} '.DialogResult = Button.Result
            If Button.ToolTip <> "" Then totToolTip.SetToolTip(CmdButton, Button.ToolTip)
            Button.Control = CmdButton
            AddHandler CmdButton.Click, AddressOf Button_Click
            Return CmdButton
        End Function
        ''' <summary>Creates <see cref="RadioButton"/> from <see cref="iMsg.MessageBoxRadioButton"/></summary>
        ''' <param name="Radio"><see cref="iMsg.MessageBoxRadioButton"/> to initialize new <see cref="RadioButton"/> with</param>
        ''' <returns>New created <see cref="RadioButton"/> with attached <see cref="RadioButton.CheckedChanged"/> event to <see cref="Radio_CheckedChanged"/></returns>
        Private Function CreateRadio(ByVal Radio As iMsg.MessageBoxRadioButton) As RadioButton
            Dim NewRadio As RadioButton = New RadioButton With {.Text = Radio.Text, .Enabled = Radio.Enabled, .Checked = Radio.Checked, .UseMnemonic = False, .Tag = Radio, .Anchor = AnchorStyles.None}
            If Radio.ToolTip <> "" Then totToolTip.SetToolTip(NewRadio, Radio.ToolTip)
            Radio.Control = NewRadio
            AddHandler NewRadio.CheckedChanged, AddressOf__Radio_CheckedChanged
            Return NewRadio
        End Function
#Region "Handlers"
#Region "Generic"
        Private Sub Control_EnabledChanged(ByVal sender As MessageBox.MessageBoxControl, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            With DirectCast(sender.Control, Control)
                If .Enabled <> sender.Enabled Then .Enabled = sender.Enabled
            End With
        End Sub
        Private Sub Control_TextChanged(ByVal sender As MessageBox.MessageBoxControl, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            If TypeOf sender Is MessageBox.MessageBoxButton Then
                With DirectCast(sender.Control, Button)
                    If .Text <> sender.Text Then
                        Dim text As String = If(sender.Text, "").Replace("&", "&&")
                        If DirectCast(sender, MessageBox.MessageBoxButton).AccessKey <> vbNullChar AndAlso text.IndexOf(DirectCast(sender, MessageBox.MessageBoxButton).AccessKey) >= 0 Then text = text.Insert(text.IndexOf(DirectCast(sender, MessageBox.MessageBoxButton).AccessKey), "&")
                        .Text = text
                    End If
                End With
            Else
                With DirectCast(sender.Control, Control)
                    If .Text <> sender.Text Then .Text = sender.Text
                End With
            End If
        End Sub
        Private Sub Control_ToolTipChanged(ByVal sender As MessageBox.MessageBoxControl, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            If totToolTip.GetToolTip(sender.Control) <> sender.ToolTip Then totToolTip.SetToolTip(sender.Control, sender.ToolTip)
        End Sub
#End Region
#Region "Radio"
        ''' <summary>Handles the <see cref="MessageBox.MessageBoxRadioButton.CheckedChanged">CheckedChanged</see> event of items of the <see cref="MessageBox">MessageBox</see>.<see cref="MessageBox.Radios">Radios</see> collection</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub Radio_CheckedChanged(ByVal sender As MessageBox.MessageBoxRadioButton, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            With DirectCast(sender.Control, RadioButton)
                If .Checked <> sender.Checked Then .Checked = sender.Checked
            End With
        End Sub
        ''' <summary>Hanles the <see cref="RadioButton.CheckedChanged"/> event of radio buttons</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub Radio_CheckedChanged(ByVal sender As RadioButton, ByVal e As EventArgs)
            DirectCast(sender.Tag, MessageBox.MessageBoxRadioButton).Checked = sender.Checked
        End Sub
#End Region
#Region "ComboBox"
        Private Sub ComboBox_EditableChanged(ByVal sender As MessageBox.MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            cmbCombo.DropDownStyle = If(sender.Editable, ComboBoxStyle.DropDown, ComboBoxStyle.DropDownList)
        End Sub
        Private Sub ComboBox_DisplayMemberChanged(ByVal sender As MessageBox.MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            cmbCombo.DisplayMember = sender.DisplayMember
        End Sub
        ''' <exception cref="InvalidOperationException"><paramref name="e"/>.<see cref="ListWithEvents.ListChangedEventArgs.Action">Action</see> is not member of <see cref="CollectionsT.GenericT.CollectionChangeAction"/>.</exception>
        Private Sub ComboBox_ItemsChanged(ByVal sender As MessageBox.MessageBoxComboBox, ByVal e As ListWithEvents(Of Object).ListChangedEventArgs)
            Select Case e.Action
                Case CollectionsT.GenericT.CollectionChangeAction.Add
                    cmbCombo.Items.Insert(e.Index, e.NewValue)
                Case CollectionsT.GenericT.CollectionChangeAction.Clear
                    cmbCombo.Items.Clear()
                Case CollectionsT.GenericT.CollectionChangeAction.Other
                    cmbCombo.Items.Clear()
                    cmbCombo.Items.AddRange(sender.Items.ToArray)
                Case CollectionsT.GenericT.CollectionChangeAction.Remove
                    cmbCombo.Items.RemoveAt(e.Index)
                Case CollectionsT.GenericT.CollectionChangeAction.Replace
                    cmbCombo.Items(e.Index) = e.NewValue
                Case Else
                    Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.WasNotMemberOf, "Action", "CollectionChangeAction"))
            End Select
        End Sub
        Private Sub ComboBox_SelectedIndexChanged(ByVal sender As MessageBox.MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Integer))
            If sender.SelectedIndex <> cmbCombo.SelectedIndex Then
                Try
                    cmbCombo.SelectedIndex = sender.SelectedIndex
                Finally
                    sender.SelectedItem = cmbCombo.SelectedItem
                    sender.SelectedIndex = cmbCombo.SelectedIndex
                End Try
            End If
        End Sub
        Private Sub ComboBox_selectedItemChanged(ByVal sender As MessageBox.MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            If (sender.SelectedItem Is Nothing Xor cmbCombo.SelectedItem IsNot Nothing) OrElse (sender.SelectedItem IsNot Nothing AndAlso Not sender.SelectedItem.Equals(cmbCombo.SelectedItem)) Then
                Try
                    cmbCombo.SelectedItem = sender.SelectedItem
                Finally
                    MessageBox.ComboBox.SelectedItem = cmbCombo.SelectedItem
                    MessageBox.ComboBox.SelectedIndex = cmbCombo.SelectedIndex
                End Try
            End If
        End Sub
        Private Sub cmbCombo_SelectedIndexChanged(ByVal sender As ComboBox, ByVal e As System.EventArgs) Handles cmbCombo.SelectedIndexChanged
            If MessageBox.ComboBox Is Nothing Then Exit Sub
            If MessageBox.ComboBox.SelectedIndex <> sender.SelectedIndex OrElse (sender.SelectedItem Is Nothing Xor MessageBox.ComboBox.SelectedItem Is Nothing) OrElse (sender.SelectedItem IsNot Nothing AndAlso Not sender.SelectedItem.Equals(MessageBox.ComboBox.SelectedItem)) Then
                MessageBox.ComboBox.SelectedItem = cmbCombo.SelectedItem
                MessageBox.ComboBox.SelectedIndex = cmbCombo.SelectedIndex
            End If
        End Sub
        Private Sub cmbCombo_TextChanged(ByVal sender As ComboBox, ByVal e As System.EventArgs) Handles cmbCombo.TextChanged
            If MessageBox.ComboBox Is Nothing Then Exit Sub
            If MessageBox.ComboBox.Text <> sender.Text Then MessageBox.ComboBox.Text = sender.Text
        End Sub
#End Region
#Region "CheckBox"
        Private Sub CheckBox_ThreeStateChanged(ByVal sender As MessageBox.MessageBoxCheckBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            CheckPlh1.ThreeState = sender.ThreeState
        End Sub
        Private Sub CheckBox_StateChanged(ByVal sender As MessageBox.MessageBoxCheckBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of CheckState))
            Dim chk As CheckBox = sender.Control
            If chk.CheckState <> sender.State Then chk.CheckState = sender.State
        End Sub
        Private Sub CheckBox_CheckStateChanged(ByVal sender As CheckBox, ByVal e As System.EventArgs)
            Dim mChk As MessageBox.MessageBoxCheckBox = sender.Tag
            If mChk.State <> sender.CheckState Then mChk.State = sender.CheckState
        End Sub
#End Region
#Region "Button"
        Private Sub Button_AccessKeyChanged(ByVal sender As MessageBox.MessageBoxButton, ByVal e As IReportsChange.ValueChangedEventArgs(Of Char))
            With DirectCast(sender.Control, Button)
                Dim text As String = sender.Text.Replace("&", "&&")
                If sender.AccessKey <> vbNullChar AndAlso text.IndexOf(sender.AccessKey) >= 0 Then text = text.Insert(text.IndexOf(sender.AccessKey), "&")
                .Text = text
            End With
        End Sub
        Private Sub Button_ResultChanged(ByVal sender As MessageBox.MessageBoxButton, ByVal e As IReportsChange.ValueChangedEventArgs(Of DialogResult))
            DirectCast(sender.Control, Button).DialogResult = sender.Result
            If e.OldValue = MessageBox.CloseResponse Xor e.NewValue = MessageBox.CloseResponse Then EnsureCancelButton()
        End Sub
#End Region
#Region "MessageBox"
        Private Sub MessageBox_BottomControlChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            tlpMain.ReplaceControl(If(BottomControl, lblPlhBottom), If(MessageBox.BottomControlControl, lblPlhBottom))
            BottomControl = MessageBox.BottomControlControl
        End Sub
        Private Sub MessageBox_MidControlChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            tlpMain.ReplaceControl(If(MidControl, lblPlhMid), If(MessageBox.MidControlControl, lblPlhMid))
            MidControl = MessageBox.MidControlControl
        End Sub
        Private Sub MessageBox_TopControlChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            tlpMain.ReplaceControl(If(TopControl, lblPlhTop), If(MessageBox.TopControlControl, lblPlhTop))
            TopControl = MessageBox.TopControlControl
        End Sub
        Private Sub MessageBox_PromptChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            lblPrompt.Text = sender.Prompt
        End Sub
        Private Sub MessageBox_TitleChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            Me.Text = sender.Title
        End Sub
        Private Sub MessageBox_CountDown(ByVal sender As MessageBox, ByVal e As EventArgs)
            Dim Format As String = ResourcesT.Components.MessageBoxCounDownTimeFormat
            Select Case MessageBox.TimeButton
                Case -1
                    For Each Button In MessageBox.Buttons
                        If Button.Result = MessageBox.CloseResponse Then
                            DirectCast(Button.Control, Button).Text = String.Format(Format, Button.Text, MessageBox.CurrentTimer)
                            Exit Select
                        End If
                    Next
                    Me.Text = String.Format(Format, MessageBox.Title, MessageBox.CurrentTimer)
                Case Is < -1 'Title
                    Me.Text = String.Format(Format, MessageBox.Title, MessageBox.CurrentTimer)
                Case Is >= MessageBox.Buttons.Count 'Do nothing
                Case Else
                    Me.flpButtons.Controls(MessageBox.TimeButton).Text = String.Format(Format, MessageBox.Buttons(MessageBox.TimeButton).Text, MessageBox.CurrentTimer)
            End Select
        End Sub
        Private Sub MessageBox_CloseResponseChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of DialogResult))
            EnsureCancelButton()
        End Sub
        Private Sub MessageBox_ComboBoxChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBox.MessageBoxComboBox))
            If e.OldValue IsNot e.NewValue Then
                If e.OldValue IsNot Nothing Then AttachComboBoxHandlers(e.OldValue, False)
                ApplyComboBox()
                If e.NewValue IsNot Nothing Then AttachComboBoxHandlers(e.NewValue)
            End If
        End Sub
        Private Sub MessageBox_DefaultButtonChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Integer))
            Me.AcceptButton = Nothing
            If e.NewValue >= 0 AndAlso e.NewValue < MessageBox.Buttons.Count Then Me.AcceptButton = MessageBox.Buttons(e.NewValue).Control
        End Sub
        ''' <exception cref="InvalidOperationException"><paramref name="e"/>.<see cref="ListWithEvents.ListChangedEventArgs.Action">Action</see> is <see cref="CollectionsT.GenericT.CollectionChangeAction.Other"/> or is not member of <see cref="CollectionsT.GenericT.CollectionChangeAction"/>.</exception>
        Private Sub MessageBox_CheckBoxesChanged(ByVal sender As MessageBox, ByVal e As ListWithEvents(Of MessageBox.MessageBoxCheckBox).ListChangedEventArgs)
            Select Case e.Action
                Case CollectionsT.GenericT.CollectionChangeAction.Add
                    Dim NewCheck = CreateCheckBox(e.NewValue)
                    flpChecks.Controls.Insert(e.Index, NewCheck)
                    AttachCheckBoxHandlers(e.NewValue)
                    flpChecks.Visible = True
                Case CollectionsT.GenericT.CollectionChangeAction.Clear
                    Dim e2 = DirectCast(e.ChangeEventArgs, ListWithEvents(Of iMsg.MessageBoxCheckBox).ItemsEventArgs)
                    flpChecks.Controls.Clear()
                    For Each old In e2.Items
                        PerformCheckBoxRemoval(old)
                    Next
                    Me.flpChecks.Visible = False
                Case CollectionsT.GenericT.CollectionChangeAction.ItemChange 'Do nothing
                Case CollectionsT.GenericT.CollectionChangeAction.Remove
                    Me.flpChecks.Controls.RemoveAt(e.Index)
                    PerformCheckBoxRemoval(e.OldValue.Control)
                    flpChecks.Visible = flpChecks.Controls.Count > 0
                Case CollectionsT.GenericT.CollectionChangeAction.Replace
                    Dim NewCheck = CreateCheckBox(e.NewValue)
                    PerformButtonRemoval(e.OldValue.Control)
                    Me.flpChecks.Controls.Replace(e.Index, NewCheck)
                    AttachCheckBoxHandlers(e.NewValue)
                    EnsureCancelButton()
                Case Else : Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.TheCollectionChangeActionOtherActionAndActionsThatAreNotMembersOfTheCollectionActionEnumerationAreNotSupportedOn0Collection, "CheckBoxes"))
            End Select
        End Sub
        ''' <exception cref="InvalidOperationException"><paramref name="e"/>.<see cref="ListWithEvents.ListChangedEventArgs.Action">Action</see> is <see cref="CollectionsT.GenericT.CollectionChangeAction.Other"/> or is not member of <see cref="CollectionsT.GenericT.CollectionChangeAction"/>.</exception>
        Private Sub MessageBox_RadiosChanged(ByVal sender As MessageBox, ByVal e As ListWithEvents(Of MessageBox.MessageBoxRadioButton).ListChangedEventArgs)
            Select Case e.Action
                Case CollectionsT.GenericT.CollectionChangeAction.Add
                    Dim NewRadio = CreateRadio(e.NewValue)
                    flpRadio.Controls.Insert(e.Index, NewRadio)
                    AttachRadioHandlers(e.NewValue)
                    flpRadio.Visible = True
                Case CollectionsT.GenericT.CollectionChangeAction.Clear
                    Dim e2 = DirectCast(e.ChangeEventArgs, ListWithEvents(Of iMsg.MessageBoxRadioButton).ItemsEventArgs)
                    flpRadio.Controls.Clear()
                    For Each old In e2.Items
                        PerformRadioRemoval(old)
                    Next
                    Me.flpRadio.Visible = False
                Case CollectionsT.GenericT.CollectionChangeAction.ItemChange 'Do nothing
                Case CollectionsT.GenericT.CollectionChangeAction.Remove
                    Me.flpRadio.Controls.RemoveAt(e.Index)
                    PerformRadioRemoval(e.OldValue.Control)
                    flpRadio.Visible = flpRadio.Controls.Count > 0
                Case CollectionsT.GenericT.CollectionChangeAction.Replace
                    Dim NewRadio = CreateRadio(e.NewValue)
                    PerformButtonRemoval(e.OldValue.Control)
                    Me.flpRadio.Controls.Replace(e.Index, NewRadio)
                    AttachRadioHandlers(e.NewValue)
                    EnsureCancelButton()
                Case Else : Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.TheCollectionChangeActionOtherActionAndActionsThatAreNotMembersOfTheCollectionActionEnumerationAreNotSupportedOn0Collection, "Radios"))
            End Select
        End Sub
        ''' <exception cref="InvalidOperationException"><paramref name="e"/>.<see cref="ListWithEvents.ListChangedEventArgs.Action">Action</see> is <see cref="CollectionsT.GenericT.CollectionChangeAction.Other"/> or is not member of <see cref="CollectionsT.GenericT.CollectionChangeAction"/>.</exception>
        Private Sub MessageBox_ButtonsChanged(ByVal sender As MessageBox, ByVal e As ListWithEvents(Of MessageBox.MessageBoxButton).ListChangedEventArgs)
            Select Case e.Action
                Case CollectionsT.GenericT.CollectionChangeAction.Add
                    Dim NewButton = CreateButton(e.NewValue)
                    AttachButtonHandlers(e.NewValue)
                    flpButtons.Controls.Insert(e.Index, NewButton)
                    flpButtons.Visible = True
                    If MessageBox.DefaultButton >= e.Index Then MessageBox.DefaultButton += 1
                    EnsureCancelButton()
                Case CollectionsT.GenericT.CollectionChangeAction.Clear
                    Dim e2 = DirectCast(e.ChangeEventArgs, ListWithEvents(Of MessageBox.MessageBoxButton).ItemsEventArgs)
                    flpButtons.Controls.Clear()
                    For Each Old In e2.Items
                        PerformButtonRemoval(Old)
                    Next
                    flpButtons.Visible = False
                    Me.AcceptButton = Nothing
                    Me.CancelButton = Nothing
                Case CollectionsT.GenericT.CollectionChangeAction.ItemChange 'Do nothing
                Case CollectionsT.GenericT.CollectionChangeAction.Remove
                    If Me.AcceptButton Is e.OldValue.Control Then Me.AcceptButton = Nothing
                    If Me.CancelButton Is e.OldValue.Control Then Me.CancelButton = Nothing
                    Me.flpButtons.Controls.RemoveAt(e.Index)
                    PerformButtonRemoval(e.OldValue.Control)
                    flpButtons.Visible = flpButtons.Controls.Count > 0
                    If MessageBox.DefaultButton = e.Index Then MessageBox.DefaultButton = -1
                    If MessageBox.DefaultButton > e.Index Then MessageBox.DefaultButton -= 1
                    EnsureCancelButton()
                Case CollectionsT.GenericT.CollectionChangeAction.Replace
                    Dim NewButton = CreateButton(e.NewValue)
                    If Me.AcceptButton Is e.OldValue.Control Then Me.AcceptButton = NewButton
                    If Me.CancelButton Is e.OldValue.Control Then Me.CancelButton = Nothing
                    PerformButtonRemoval(e.OldValue.Control)
                    Me.flpButtons.Controls.Replace(e.Index, NewButton)
                    AttachButtonHandlers(e.NewValue)
                    EnsureCancelButton()
                Case Else : Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.TheCollectionChangeActionOtherActionAndActionsThatAreNotMembersOfTheCollectionActionEnumerationAreNotSupportedOn0Collection, "Buttons"))
            End Select
        End Sub
        ''' <summary>Performs actions needed when button is about to be removed. Does not remove button from <see cref="flpButtons"/>.</summary>
        ''' <param name="Button">Button to be removed</param>
        Private Sub PerformButtonRemoval(ByVal Button As MessageBox.MessageBoxButton)
            Dim cmd As Button = Button.Control
            cmd.Tag = Nothing
            Button.Control = Nothing
            RemoveHandler cmd.Click, AddressOf__Button_Click
            AttachButtonHandlers(Button, False)
        End Sub
        ''' <summary>Performs actions needed when radio button is about to be removed. Does not remove radio button from <see cref="flpRadio"/>.</summary>
        ''' <param name="Button"><see cref="iMsg.MessageBoxRadioButton"/> to be removed</param>
        Private Sub PerformRadioRemoval(ByVal Button As iMsg.MessageBoxRadioButton)
            Dim opt As RadioButton = Button.Control
            opt.Tag = Nothing
            Button.Control = Nothing
            RemoveHandler opt.CheckedChanged, AddressOf__Radio_CheckedChanged
            AttachRadioHandlers(Button, False)
        End Sub
        ''' <summary>Performs actions needed when check box is about to be removed. Does not remove radio button from <see cref="flpChecks"/>.</summary>
        ''' <param name="mChk"><see cref="iMsg.MessageBoxCheckBox"/> to be removed</param>
        Private Sub PerformCheckBoxRemoval(ByVal mChk As iMsg.MessageBoxCheckBox)
            Dim chk As CheckBox = mChk.Control
            chk.Tag = Nothing
            mChk.Control = Nothing
            RemoveHandler chk.CheckStateChanged, AddressOf__CheckBox_CheckStateChanged
            AttachCheckBoxHandlers(mChk, False)
        End Sub
        ''' <summary>Ensures that <see cref="CancelButton"/> is set to button with <see cref="Button.DialogResult">DialogResult</see> same as <see cref="MessageBox">MessageBox</see>.<see cref="iMsg.CloseResponse">CloseResponse</see> or to null if appropriate button does not exist</summary>
        Private Sub EnsureCancelButton()
            Me.CancelButton = Nothing
            For Each Button In MessageBox.Buttons
                If Button.Result = MessageBox.CloseResponse Then
                    Me.CancelButton = Button.Control
                    Exit Sub
                End If
            Next
        End Sub
        Private Sub MessageBox_IconChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Drawing.Image))
            picPicture.Image = e.NewValue
            picPicture.Visible = e.NewValue IsNot Nothing
        End Sub
        Private Sub MessageBox_OptionsChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBox.MessageBoxOptions))
            ApplyOptions()
        End Sub
#End Region
#Region "AddressOf"
        'Following delegates required because methods does not have exactly same signatures as events they are handling
        Private ReadOnly AddressOf__Button_Click As EventHandler = AddressOf Button_Click
        Private ReadOnly AddressOf__MessageBox_BottomControlChanged As MessageBox.ControlChangedEventHandler = AddressOf MessageBox_BottomControlChanged
        Private ReadOnly AddressOf__MessageBox_TopControlChanged As MessageBox.ControlChangedEventHandler = AddressOf MessageBox_TopControlChanged
        Private ReadOnly AddressOf__MessageBox_MidControlChanged As MessageBox.ControlChangedEventHandler = AddressOf MessageBox_MidControlChanged
        Private ReadOnly AddressOf__MessageBox_CloseResponseChanged As EventHandler(Of iMsg, IReportsChange.ValueChangedEventArgs(Of DialogResult)) = AddressOf MessageBox_CloseResponseChanged
        Private ReadOnly AddressOf__MessageBox_ComboBoxChanged As EventHandler(Of iMsg, IReportsChange.ValueChangedEventArgs(Of iMsg.MessageBoxComboBox)) = AddressOf MessageBox_ComboBoxChanged
        Private ReadOnly AddressOf__MessageBox_CountDown As EventHandler(Of iMsg, EventArgs) = AddressOf MessageBox_CountDown
        Private ReadOnly AddressOf__MessageBox_DefaultButtonChanged As EventHandler(Of iMsg, IReportsChange.ValueChangedEventArgs(Of Integer)) = AddressOf MessageBox_DefaultButtonChanged
        Private ReadOnly AddressOf__MessageBox_CheckBoxesChanged As iMsg.CheckBoxesChangedEventHandler = AddressOf MessageBox_CheckBoxesChanged
        Private ReadOnly AddressOf__MessageBox_PromptChanged As EventHandler(Of iMsg, IReportsChange.ValueChangedEventArgs(Of String)) = AddressOf MessageBox_PromptChanged
        Private ReadOnly AddressOf__MessageBox_TitleChanged As EventHandler(Of iMsg, IReportsChange.ValueChangedEventArgs(Of String)) = AddressOf MessageBox_TitleChanged
        Private ReadOnly AddressOf__MessageBox_ButtonsChanged As iMsg.ButtonsChangedEventHandler = AddressOf MessageBox_ButtonsChanged
        Private ReadOnly AddressOf__MessageBox_RadiosChanged As iMsg.RadiosChangedEventHandler = AddressOf MessageBox_RadiosChanged
        Private ReadOnly AddressOf__Radio_CheckedChanged As EventHandler = AddressOf New EventHandler(Of RadioButton, EventArgs)(AddressOf Radio_CheckedChanged).Invoke
        Private ReadOnly AddressOf__MessageBox_IconChanged As EventHandler(Of iMsg, IReportsChange.ValueChangedEventArgs(Of Drawing.Image)) = AddressOf MessageBox_IconChanged
        Private ReadOnly AddressOf__MessageBox_OptionsChanged As EventHandler(Of iMsg, IReportsChange.ValueChangedEventArgs(Of MessageBox.MessageBoxOptions)) = AddressOf MessageBox_OptionsChanged
        Private ReadOnly AddressOf__CheckBox_CheckStateChanged As EventHandler = AddressOf CheckBox_CheckStateChanged
#End Region
        Private Sub Button_Click(ByVal sender As Button, ByVal e As EventArgs)
            Dim button As iMsg.MessageBoxButton = sender.Tag
            If button.OnClick() Then
                MessageBox.ClickedButton = button
                Me.DialogResult = button.Result
                MessageBox.DialogResult = button.Result
                Me.Close()
            End If
        End Sub
        ''' <summary>Force closes the form</summary>
        Public Shadows Sub Close()
            AllowClose = True
            Try
                MyBase.Close()
            Finally : AllowClose = False : End Try
        End Sub
        Private Sub MessageBoxForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
            CommonDisposeActions()
            MessageBox.OnClosed(e)
        End Sub
#End Region
        ''' <summary>When true <see cref="MessageBoxForm_FormClosing"/> allows form to be closed on whatever reason without any action (use for programatic closes)</summary>
        Private AllowClose As Boolean = False
        Private Sub MessageBoxForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
            Try
                If AllowClose Then Exit Sub
                Select Case e.CloseReason
                    Case CloseReason.UserClosing, CloseReason.None
                        If Not MessageBox.AllowClose Then e.Cancel = True : Exit Sub
                        Me.DialogResult = MessageBox.CloseResponse
                        MessageBox.DialogResult = MessageBox.CloseResponse
                        MessageBox.ClickedButton = Nothing
                    Case Else : e.Cancel = True : Exit Sub
                End Select
                If Not e.Cancel Then
                    AllowClose = True
                    MyBase.Close()
                End If
            Finally
                If Not e.Cancel Then 'Prevents controls from being disposed
                    If Me.TopControl IsNot Nothing Then tlpMain.ReplaceControl(Me.TopControl, lblPlhTop)
                    If Me.MidControl IsNot Nothing Then tlpMain.ReplaceControl(Me.MidControl, lblPlhMid)
                    If Me.BottomControl IsNot Nothing Then tlpMain.ReplaceControl(Me.BottomControl, lblPlhBottom)
                End If
            End Try
        End Sub

        Private Sub MessageBoxForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Me.MaximumSize = DesktopBounds.Size
        End Sub

        Private Sub MessageBoxForm_Shown(ByVal sender As MessageBoxForm, ByVal e As System.EventArgs) Handles Me.Shown
            lblPrompt.MaximumSize = New Drawing.Size(Screen.FromControl(lblPrompt).WorkingArea.Width / 2, 0)
            MessageBox.OnShown()
            If (MessageBox.Options And IndependentT.MessageBox.MessageBoxOptions.BringToFront) = IndependentT.MessageBox.MessageBoxOptions.BringToFront Then
                Me.BringToFront()
            End If
        End Sub
    End Class

    ''' <summary>Implements <see cref="WindowsT.IndependentT.MessageBox"/> for as <see cref="Form"/></summary>
    ''' <remarks>The <see cref="IndependentT.MessageBox"/> is implemented with following notes:
    ''' <list>
    ''' <item><see cref="iMsg.MessageBoxOptions.AlignJustify"/> is not supported, <see cref="iMsg.MessageBoxOptions.AlignLeft"/> is used instead.</item>
    ''' </list>
    ''' </remarks>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <version version="1.5.2">Fixed: Check box has not enough size.</version>
    ''' <version version="1.5.2">Fixed: Custom controls (<see cref="iMsg.TopControl"/>, <see cref="iMsg.MidControl"/>, <see cref="iMsg.BottomControl"/>) derived from <see cref="Windows.UIElement"/> are not shown.</version>
    ''' <version version="1.5.2">Fixed: Custom controls (<see cref="iMsg.TopControl"/>, <see cref="iMsg.MidControl"/>, <see cref="iMsg.BottomControl"/>) get disposed when message box is closed.</version>
    ''' <version version="1.5.2">Fixed: When custom control (<see cref="iMsg.TopControl"/>, <see cref="iMsg.MidControl"/>, <see cref="iMsg.BottomControl"/>) is replaced wne message box is shown, the change does not take effect.</version>
    ''' <version version="1.5.2">Fixed: Dialog closes even when button click operation is cancelled (see <see cref="iMsg.MessageBoxButton.ClickPreview"/>)</version>
    <System.Drawing.ToolboxBitmap(GetType(EncodingSelector), "MessageBox.bmp")> _
       Public Class MessageBox
        Inherits iMsg
        ''' <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Component" />.</summary>
        ''' <param name="disposing"> true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Form IsNot Nothing AndAlso Not Form.IsDisposed Then
                    Form.Close()
                    Form.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub
        ''' <summary>If overriden in derived class closes the message box with given response</summary>
        ''' <param name="Response">Response returned by the <see cref="Show"/> function</param>
        Public Overloads Overrides Sub Close(ByVal Response As System.Windows.Forms.DialogResult)
            Me.DialogResult = Response
            Me.ClickedButton = Nothing
            Form.Close()
        End Sub

        ''' <summary>Contains value of the <see cref="Form"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Form As MessageBoxForm
        ''' <summary>Gets or sets instance of <see cref="MessageBoxForm"/> that currently shows the message box</summary>
        ''' <value>This property should be set only from overriden <see cref="PerformDialog"/> when it does not calls base class method</value>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property Form() As MessageBoxForm
            Get
                Return _Form
            End Get
            Protected Set(ByVal value As MessageBoxForm)
                _Form = value
            End Set
        End Property

        ''' <summary>Shows the dialog</summary>
        ''' <param name="Modal">Indicates if dialog should be shown modally (true) or modells (false)</param>
        ''' <param name="Owner">Parent window of dialog (may be null)</param>
        ''' <remarks>Note for inheritors: If you override thie method and do not call base class method, you must set value of the <see cref="Form"/> property</remarks>
        ''' <exception cref="InvalidOperationException"><see cref="State"/> is not <see cref="States.Created"/></exception>
        Protected Overrides Sub PerformDialog(ByVal Modal As Boolean, ByVal Owner As System.Windows.Forms.IWin32Window)
            If State <> States.Created Then Throw New InvalidOperationException(ResourcesT.Exceptions.MessageBoxMustBeInCreatedStateInOrderToBeDisplyedByPerformDialog)
            Form = New MessageBoxForm(Me)
            If Modal Then
                Form.ShowDialog(Owner)
            Else
                Form.Show(Owner)
            End If
        End Sub
        ''' <summary>Shows dialog in sync with thread ow given control modally to give control</summary>
        ''' <param name="Owner">Control to show dialog on thread of and modally to (null to ignore)</param>
        ''' <returns>Dialog result</returns>
        Public Function ShowDialogOn(ByVal Owner As Control) As DialogResult
            If Owner Is Nothing Then
                PerformDialog(True, Nothing)
                Return Me.DialogResult
            ElseIf Owner.InvokeRequired Then
                Return Owner.Invoke(New Func(Of Control, DialogResult)(AddressOf ShowDialogOn), Owner)
            Else
                PerformDialog(True, Owner)
                Return Me.DialogResult
            End If
        End Function
        ''' <summary>gets <see cref="Control"/> representation of <see cref="TopControl"/> if possible</summary>
        ''' <returns><see cref="Control"/> which represents <see cref="TopControl"/> if possible, null otherwise</returns>
        ''' <seealso cref="GetControl"/><seealso cref="MidControlControl"/><seealso cref="BottomControlControl"/>
        ''' <seealso cref="TopControl"/>
        ''' <version version="1.5.2">Fixed: First call for <see cref="TopControl"/> being <see cref="Windows.UIElement"/> returns null.</version>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Friend ReadOnly Property TopControlControl() As Control
            Get
                Return GetControl(Me.TopControl)
            End Get
        End Property
        ''' <summary>Gets <see cref="Control"/> representation of <see cref="MidControl"/> if possible</summary>
        ''' <returns><see cref="Control"/> which represents <see cref="MidControl"/> if possible, null otherwise</returns>
        ''' <seealso cref="GetControl"/><seealso cref="TopControlControl"/><seealso cref="BottomControlControl"/>
        ''' <seealso cref="MidControl"/>
        ''' <version version="1.5.2">Fixed: First call for <see cref="TopControl"/> being <see cref="Windows.UIElement"/> returns null.</version>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Friend ReadOnly Property MidControlControl() As Control
            Get
                Return GetControl(Me.MidControl)
            End Get
        End Property
        ''' <summary>Gets <see cref="Control"/> representation of <see cref="BottomControl"/> if possible</summary>
        ''' <returns><see cref="Control"/> which represents <see cref="BottomControl"/> if possible, null otherwise</returns>
        ''' <seealso cref="GetControl"/><seealso cref="TopControlControl"/><seealso cref="MidControlControl"/>
        ''' <seealso cref="BottomControl"/>
        ''' <version version="1.5.2">Fixed: First call for <see cref="TopControl"/> being <see cref="Windows.UIElement"/> returns null.</version>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Friend ReadOnly Property BottomControlControl() As Control
            Get
                Return GetControl(Me.BottomControl)
            End Get
        End Property
        ''' <summary>Gets control from object</summary>
        ''' <param name="Control">Object that represents a control. It can be <see cref="Control"/>, <see cref="Windows.UIElement"/></param>
        ''' <returns><see cref="Control"/> which represents <paramref name="Control"/>. For same <paramref name="Control"/> returns same <see cref="Control"/>. Returns null if <paramref name="Control"/> is null or it is of unsupported type.</returns>
        ''' <version version="1.5.2">Fixed: First call for <paramref name="Control"/> being <see cref="Windows.UIElement"/> returns null.</version>
        Protected Overridable Function GetControl(ByVal Control As Object) As Control
            If Control Is Nothing Then Return Nothing
            If TypeOf Control Is Control Then Return Control
            If TypeOf Control Is Windows.UIElement Then
                Static WPFHosts As Dictionary(Of Windows.FrameworkElement, Windows.Forms.Integration.ElementHost)
                If WPFHosts Is Nothing Then WPFHosts = New Dictionary(Of Windows.FrameworkElement, Windows.Forms.Integration.ElementHost)
                If WPFHosts.ContainsKey(Control) Then Return WPFHosts(Control)
                Dim WPFHost As New Integration.ElementHost With {.Dock = DockStyle.Fill}
                WPFHost.HostContainer.Children.Add(Control)
                WPFHosts.Add(Control, WPFHost)
                Return WPFHost
            End If
            Return Nothing
        End Function
        ''' <summary>Performs all operations needed to switch <see cref="MessageBox"/> form <see cref="State"/> <see cref="States.Closed"/> to <see cref="States.Created"/></summary>
        ''' <remarks>Called by <see cref="Recycle"/>.
        ''' <para>Note to inheritors: Always call base-class method <see cref="RecycleInternal"/>.</para></remarks>
        ''' <exception cref="InvalidOperationException"><see cref="State"/> is not <see cref="States.Closed"/>. This exception never occures in this implementation because <see cref="Recycle"/> ensures that <see cref="RecycleInternal"/> is caled only when <see cref="State"/> is <see cref="States.Closed"/>.</exception>
        Protected Overrides Sub RecycleInternal()
            MyBase.RecycleInternal()
            Form = Nothing
        End Sub

        ''' <summary>Gets text which contains Accesskey marker (&amp;)</summary>
        ''' <param name="Text">Text (if it contains character used as accesskey markers they must be escaped)</param>
        ''' <param name="AccessKey">Char representing accesskey (if char is not in <paramref name="Text"/> no accesskey marker should be inserted)</param>
        ''' <returns><paramref name="Text"/> with accesskey denoted in it by &amp;.</returns>
        ''' <version version="1.5.2">Function added</version>
        Protected Overloads Overrides Function GetTextWithAccessKey(ByVal Text As String, ByVal AccessKey As Char) As String
            Return GetTextWithAccessKey(Text, AccessKey, "&"c)
        End Function
    End Class
End Namespace
