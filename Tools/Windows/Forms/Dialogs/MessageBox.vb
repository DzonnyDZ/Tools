Imports System.Windows.Forms, Tools.WindowsT, System.ComponentModel, System.Linq
Imports Tools.WindowsT.FormsT.UtilitiesT.Misc, Tools.CollectionsT.SpecializedT, Tools.CollectionsT.GenericT
#If Config <= Nightly Then
Namespace WindowsT.FormsT
    ''' <summary>Implements GUI (form) for <see cref="MessageBox"/></summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Class MessageBoxForm : Inherits Form
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
        ''' <summary>Initializes tis form by <see cref="MessageBox"/>, called from CTor</summary>
        Protected Overridable Sub Initialize()
            'Title
            If MessageBox.Title Is Nothing Then
                Dim App As New Microsoft.VisualBasic.ApplicationServices.AssemblyInfo(System.Reflection.Assembly.GetEntryAssembly)
                Me.Text = App.Title
            Else
                Me.Text = MessageBox.Title
            End If
            'Top control
            tlpMain.ReplaceControl(lblPlhTop, MessageBox.TopControlControl)
            'Icon
            If MessageBox.Icon Is Nothing Then
                picPicture.Visible = False
            Else
                picPicture.Image = MessageBox.Icon
            End If
            'Propmpt
            lblPrompt.Text = MessageBox.Prompt
            'Checkbox
            If MessageBox.CheckBox Is Nothing Then
                chkCheckBox.Visible = False
            Else
                chkCheckBox.Text = MessageBox.CheckBox.Text
                MessageBox.CheckBox.Control = chkCheckBox
                chkCheckBox.CheckState = MessageBox.CheckBox.State
                chkCheckBox.ThreeState = MessageBox.CheckBox.ThreeState
                If MessageBox.CheckBox.ToolTip <> "" Then totToolTip.SetToolTip(chkCheckBox, MessageBox.CheckBox.ToolTip)
                chkCheckBox.Enabled = MessageBox.CheckBox.Enabled
            End If
            'Combobox
            If MessageBox.ComboBox Is Nothing Then
                cmbCombo.Visible = False
            Else
                MessageBox.CheckBox.Control = cmbCombo
                cmbCombo.DropDownStyle = If(MessageBox.ComboBox.Editable, ComboBoxStyle.DropDown, ComboBoxStyle.DropDownList)
                cmbCombo.Enabled = MessageBox.ComboBox.Enabled
                cmbCombo.DisplayMember = MessageBox.ComboBox.DisplayMember
                For Each item In MessageBox.ComboBox.Items
                    cmbCombo.Items.Add(item)
                Next item
                If MessageBox.ComboBox.SelectedItem IsNot Nothing Then cmbCombo.SelectedItem = MessageBox.ComboBox.SelectedItem
                If MessageBox.ComboBox.SelectedIndex >= 0 Then cmbCombo.SelectedIndex = MessageBox.ComboBox.SelectedIndex
                If MessageBox.ComboBox.Editable AndAlso MessageBox.CheckBox.Text <> "" Then cmbCombo.Text = MessageBox.ComboBox.Text
                If MessageBox.ComboBox.ToolTip <> "" Then totToolTip.SetToolTip(cmbCombo, MessageBox.ComboBox.ToolTip)
            End If
            'Radios
            flpRadio.Controls.Clear()
            If MessageBox.Radios.Count <= 0 Then
                flpRadio.Visible = False
            Else
                For Each Radio In MessageBox.Radios
                    flpRadio.Controls.Add(New RadioButton With {.Text = Radio.Text, .Enabled = Radio.Enabled, .Checked = Radio.Checked, .UseMnemonic = False, .Tag = Radio})
                    If Radio.ToolTip <> "" Then totToolTip.SetToolTip(flpRadio.Controls.Last, Radio.ToolTip)
                    Radio.Control = flpRadio.Controls.Last
                    AddHandler DirectCast(flpRadio.Controls.Last, RadioButton).CheckedChanged, AddressOf Radio_CheckedChanged_ 'TODO: Why there must be _? VB bug?
                Next Radio
            End If
            'Mic control
            tlpMain.ReplaceControl(lblPlhMid, MessageBox.MidControlControl)
            'Buttons
            flpButtons.Controls.Clear()
            If MessageBox.Buttons.Count <= 0 Then
                flpButtons.Visible = False
            Else
                Dim CancelButton As Button = Nothing
                Dim i As Integer = 0
                For Each Button In MessageBox.Buttons
                    Dim text As String = Button.Text.Replace("&", "&&")
                    If Button.AccessKey <> vbNullChar AndAlso text.IndexOf(Button.AccessKey) >= 0 Then Mid(text, text.IndexOf(Button.AccessKey), 0) = "&"
                    flpButtons.Controls.Add(New Button With {.Text = text, .Enabled = Button.Enabled, .DialogResult = Button.Result, .Tag = Button})
                    If Button.ToolTip <> "" Then totToolTip.SetToolTip(flpButtons.Controls.Last, Button.ToolTip)
                    If MessageBox.DefaultButton = i Then Me.AcceptButton = flpButtons.Controls.Last
                    If MessageBox.CloseResponse = Button.Result AndAlso CancelButton Is Nothing Then CancelButton = flpButtons.Controls.Last
                    AddHandler flpButtons.Controls.Last.Click, AddressOf Button_Click
                    Button.Control = flpButtons.Controls.Last
                    i += 1
                Next Button
                Me.CancelButton = CancelButton
            End If
            'Bottom control
            tlpMain.ReplaceControl(lblPlhBottom, MessageBox.BottomControlControl)
            AddHandler MessageBox.BottomControlChanged, AddressOf MessageBox_BottomControlChanged
            AddHandler MessageBox.CloseResponseChanged, AddressOf MessageBox_CloseResponseChanged
            AddHandler MessageBox.ComboBoxChanged, AddressOf MessageBox_ComboBoxChanged
            AddHandler MessageBox.CountDown, AddressOf MessageBox_CountDown
            AddHandler MessageBox.DefaultButtonChanged, AddressOf MessageBox_DefaultButtonChanged
            AddHandler MessageBox.CheckBoxChanged, AddressOf MessageBox_CheckBoxChanged
            AddHandler MessageBox.PromptChanged, AddressOf MessageBox_PromptChanged
            AddHandler MessageBox.TitleChanged, AddressOf MessageBox_TitleChanged
            AttachComboBoxHandlers(MessageBox.ComboBox)
            AttachCheckBoxHandlers(MessageBox.CheckBox)
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
                AddHandler ComboBox.SelectedItemChanged, AddressOf ComboBox_SelectedItemChanged
                AddHandler ComboBox.TextChanged, AddressOf Control_TextChanged
                AddHandler ComboBox.ToolTipChanged, AddressOf Control_ToolTipChanged
            Else
                RemoveHandler ComboBox.DisplayMemberChanged, AddressOf ComboBox_DisplayMemberChanged
                RemoveHandler ComboBox.EditableChanged, AddressOf ComboBox_EditableChanged
                RemoveHandler ComboBox.EnabledChanged, AddressOf Control_EnabledChanged
                RemoveHandler ComboBox.ItemsChanged, AddressOf ComboBox_ItemsChanged
                RemoveHandler ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
                RemoveHandler ComboBox.SelectedItemChanged, AddressOf ComboBox_SelectedItemChanged
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
#End Region
#Region "Handlers"
#Region "Generic"
        Private Sub Control_EnabledChanged(ByVal sender As MessageBox.MessageBoxControl, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            With DirectCast(sender.Control, Control)
                If .Enabled <> sender.Enabled Then .Enabled = sender.Enabled
            End With
        End Sub
        Private Sub Control_TextChanged(ByVal sender As MessageBox.MessageBoxControl, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            With DirectCast(sender.Control, Control)
                If .Text <> sender.Text Then .Text = sender.Text
            End With
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
            With DirectCast(sender.Control, MessageBox.MessageBoxRadioButton)
                If .Checked <> sender.Checked Then .Checked = sender.Checked
            End With
        End Sub
        ''' <summary>Hanles the <see cref="RadioButton.CheckedChanged"/> event of radio buttons</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub Radio_CheckedChanged_(ByVal sender As RadioButton, ByVal e As EventArgs)
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
                    sender.SelectedItem = cmbCombo.SelectedItem
                    sender.SelectedIndex = cmbCombo.SelectedIndex
                End Try
            End If
        End Sub
        Private Sub cmbCombo_SelectedIndexChanged(ByVal sender As ComboBox, ByVal e As System.EventArgs) Handles cmbCombo.SelectedIndexChanged
            If MessageBox.CheckBox Is Nothing Then Exit Sub
            If MessageBox.ComboBox.SelectedIndex <> sender.SelectedIndex OrElse (sender.SelectedItem Is Nothing Xor MessageBox.ComboBox.SelectedItem Is Nothing) OrElse (sender.SelectedItem IsNot Nothing AndAlso Not sender.SelectedItem.Equals(MessageBox.ComboBox.SelectedItem)) Then
                sender.SelectedIndex = cmbCombo.SelectedIndex
                sender.SelectedItem = cmbCombo.SelectedItem
            End If
        End Sub
        Private Sub cmbCombo_TextChanged(ByVal sender As combobox, ByVal e As System.EventArgs) Handles cmbCombo.TextChanged
            If MessageBox.ComboBox Is Nothing Then Exit Sub
            If MessageBox.ComboBox.Text <> sender.Text Then MessageBox.ComboBox.Text = sender.Text
        End Sub
#End Region
#Region "CheckBox"
        Private Sub CheckBox_ThreeStateChanged(ByVal sender As MessageBox.MessageBoxCheckBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            chkCheckBox.ThreeState = sender.ThreeState
        End Sub
        Private Sub CheckBox_StateChanged(ByVal sender As MessageBox.MessageBoxCheckBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of CheckState))
            If chkCheckBox.CheckState <> sender.State Then chkCheckBox.CheckState = sender.State
        End Sub
        Private Sub chkCheckBox_CheckStateChanged(ByVal sender As CheckBox, ByVal e As System.EventArgs) Handles chkCheckBox.CheckStateChanged
            If MessageBox.CheckBox IsNot Nothing AndAlso MessageBox.CheckBox.State <> sender.CheckState Then MessageBox.CheckBox.State = sender.CheckState
        End Sub
#End Region
#Region "Button"
        Private Sub Button_AccessKeyChanged(ByVal sender As MessageBox.MessageBoxButton, ByVal e As IReportsChange.ValueChangedEventArgs(Of Char))
            With DirectCast(sender.Control, Button)
                Dim text As String = sender.Text.Replace("&", "&&")
                If sender.AccessKey <> vbNullChar AndAlso text.IndexOf(sender.AccessKey) >= 0 Then Mid(text, text.IndexOf(sender.AccessKey), 0) = "&"
                .Text = text
            End With
        End Sub
        Private Sub Button_ResultChanged(ByVal sender As MessageBox.MessageBoxButton, ByVal e As IReportsChange.ValueChangedEventArgs(Of DialogResult))
            DirectCast(sender.Control, Button).DialogResult = sender.Result
        End Sub
#End Region
#Region "MessageBox"
        Private Sub MessageBox_BottomControlChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            tlpMain.ReplaceControl(If(BottomControl, lblPlhBottom), If(MessageBox.BottomControlControl, lblPlhBottom))
        End Sub
        Private Sub MessageBox_MidControlChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            tlpMain.ReplaceControl(If(MidControl, lblPlhMid), If(MessageBox.MidControlControl, lblPlhMid))
        End Sub
        Private Sub MessageBox_TopControlChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            tlpMain.ReplaceControl(If(MidControl, lblPlhTop), If(MessageBox.TopControlControl, lblPlhTop))
        End Sub
        Private Sub MessageBox_PromptChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            lblPrompt.Text = sender.Prompt
        End Sub
        Private Sub MessageBox_TitleChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            Me.Text = sender.Title
        End Sub
        Private Sub MessageBox_CountDown(ByVal sender As MessageBox, ByVal e As EventArgs)
            Const Format As String = "{0} ({1:})" 'TODO: Formating
            Select Case MessageBox.TimeButton
                Case -1
                    For Each Button In MessageBox.Buttons
                        If Button.Result = MessageBox.CloseResponse Then
                            DirectCast(Button.Control, Button).Text = String.Format("{0} ({1})", Button.Text, MessageBox.TimeButton)
                            Exit Select
                        End If
                    Next
                    Me.Text = String.Format(Format, MessageBox.Title, MessageBox.CurrentTimer)
                Case Is < -1 'Title
                    Me.Text = String.Format(Format, MessageBox.Title, MessageBox.CurrentTimer)
                Case Is >= MessageBox.Buttons.Count 'Do nothing
                Case Else
                    Me.flpButtons.Controls(MessageBox.TimeButton).Text = String.Format("{0} ({1})", MessageBox.Buttons(MessageBox.TimeButton).Text, MessageBox.CurrentTimer)
            End Select
        End Sub
        Private Sub MessageBox_CloseResponseChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of DialogResult))
            'TODO:Implement
        End Sub
        Private Sub MessageBox_ComboBoxChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBox.MessageBoxComboBox))
            'TODO:Implement
        End Sub
        Private Sub MessageBox_DefaultButtonChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Integer))
            'TODO:Implement
        End Sub
        Private Sub MessageBox_CheckBoxChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBox.MessageBoxCheckBox))
            'TODO:Implement
        End Sub
#End Region
        Private Sub Button_Click(ByVal sender As Button, ByVal e As EventArgs)
            'TODO:Implement
        End Sub
#End Region
    End Class

    ''' <summary>Implements <see cref="WindowsT.DialogsT.MessageBox"/> for as <see cref="Form"/></summary>
    Public Class MessageBox
        Inherits DialogsT.MessageBox
        Public Overrides ReadOnly Property ClickedButton() As DialogsT.MessageBox.MessageBoxButton
            Get

            End Get
        End Property

        Public Overloads Overrides Sub Close(ByVal Response As System.Windows.Forms.DialogResult)

        End Sub

        Public Overrides ReadOnly Property DialogResult() As System.Windows.Forms.DialogResult
            Get

            End Get
        End Property
        ''' <summary>Contains value of the <see cref="Form"/> property</summary>
        Private _Form As MessageBoxForm
        ''' <summary>Gets or sets instance of <see cref="MessageBoxForm"/> that currently shows the message box</summary>
        ''' <value>This property should be set only from overriden <see cref="PerformDialog"/> when it does not calls base class method</value>
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
        Protected Overrides Sub PerformDialog(ByVal Modal As Boolean, ByVal Owner As System.Windows.Forms.IWin32Window)
            Form = New MessageBoxForm(Me)
            If Modal Then
                Form.ShowDialog(Owner)
            Else
                Form.Show(Owner)
            End If
        End Sub
        ''' <summary>gets <see cref="Control"/> representation of <see cref="TopControl"/> if possible</summary>
        ''' <returns><see cref="Control"/> which represents <see cref="TopControl"/> if possible, null otherwise</returns>
        ''' <seealso cref="GetControl"/><seealso cref="MidControlControl"/><seealso cref="BottomControlControl"/>
        ''' <seealso cref="TopControl"/>
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
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Friend ReadOnly Property BottomControlControl() As Control
            Get
                Return GetControl(Me.BottomControl)
            End Get
        End Property
        ''' <summary>Gets control from object</summary>
        ''' <param name="Control">Object that represents a control. It can be <see cref="Control"/>, <see cref="Windows.UIElement"/></param>
        ''' <returns><see cref="Control"/> which represents <paramref name="Control"/>. For same <paramref name="Control"/> returns same <see cref="Control"/>. Returns null if <paramref name="Control"/> is null or it is of unsupported type.</returns>
        Protected Overridable Function GetControl(ByVal Control As Object) As Control
            If Control Is Nothing Then Return Nothing
            If TypeOf Control Is Control Then Return Control
            If TypeOf Control Is Windows.UIElement Then
                Static WPFHosts As New Dictionary(Of Windows.FrameworkElement, Windows.Forms.Integration.ElementHost)
                If WPFHosts.ContainsKey(Control) Then Return WPFHosts(Control)
                Dim WPFHost As New Integration.ElementHost With {.Dock = DockStyle.Fill}
                WPFHost.HostContainer.Children.Add(Control)
                WPFHosts.Add(Control, WPFHost)
            End If
            Return Nothing
        End Function
    End Class
End Namespace
#End If