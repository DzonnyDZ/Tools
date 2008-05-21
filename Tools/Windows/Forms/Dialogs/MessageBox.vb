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
                chkCheckBox.CheckState = MessageBox.CheckBox.State
                chkCheckBox.ThreeState = MessageBox.CheckBox.ThreeState
                If MessageBox.CheckBox.ToolTip <> "" Then totToolTip.SetToolTip(chkCheckBox, MessageBox.CheckBox.ToolTip)
                chkCheckBox.Enabled = MessageBox.CheckBox.Enabled
                'TODO:Events
            End If
            'Combobox
            If MessageBox.ComboBox Is Nothing Then
                cmbCombo.Visible = False
            Else
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
            AttachComboBoxHandlers(MessageBox.ComboBox)
            AttachCheckBoxHandlers(MessageBox.CheckBox)
            'TODO: Events of buttons and radios
        End Sub
        Private Sub AttachComboBoxHandlers(ByVal ComboBox As MessageBox.MessageBoxComboBox, Optional ByVal attach As Boolean = True)
            If ComboBox Is Nothing Then Exit Sub
            If attach Then
                AddHandler ComboBox.DisplayMemberChanged, AddressOf ComboBox_DisplayMemberChanged
                AddHandler ComboBox.EditableChanged, AddressOf ComboBox_EditableChanged
                AddHandler ComboBox.EnabledChanged, AddressOf ComboBox_EnabledChanged
                AddHandler ComboBox.ItemsChanged, AddressOf ComboBox_ItemsChanged
                AddHandler ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
                AddHandler ComboBox.SelectedItemChanged, AddressOf ComboBox_SelectedItemChanged
                AddHandler ComboBox.TextChanged, AddressOf ComboBox_TextChanged
                AddHandler ComboBox.ToolTipChanged, AddressOf ComboBox_ToolTipChanged
            Else
                RemoveHandler ComboBox.DisplayMemberChanged, AddressOf ComboBox_DisplayMemberChanged
                RemoveHandler ComboBox.EditableChanged, AddressOf ComboBox_EditableChanged
                RemoveHandler ComboBox.EnabledChanged, AddressOf ComboBox_EnabledChanged
                RemoveHandler ComboBox.ItemsChanged, AddressOf ComboBox_ItemsChanged
                RemoveHandler ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
                RemoveHandler ComboBox.SelectedItemChanged, AddressOf ComboBox_SelectedItemChanged
                RemoveHandler ComboBox.TextChanged, AddressOf ComboBox_TextChanged
                RemoveHandler ComboBox.ToolTipChanged, AddressOf ComboBox_ToolTipChanged
            End If
        End Sub
        Private Sub AttachCheckBoxHandlers(ByVal CheckBox As MessageBox.MessageBoxCheckBox, Optional ByVal attach As Boolean = True)
            If CheckBox Is Nothing Then Exit Sub
            If attach Then
                AddHandler CheckBox.EnabledChanged, AddressOf CheckBox_EnabledChanged
                AddHandler CheckBox.StateChanged, AddressOf CheckBox_StateChanged
                AddHandler CheckBox.TextChanged, AddressOf CheckBox_TextChanged
                AddHandler CheckBox.ThreeStateChanged, AddressOf CheckBox_ThreeStateChanged
                AddHandler CheckBox.ToolTipChanged, AddressOf CheckBox_ToolTipChanged
            Else
                RemoveHandler CheckBox.EnabledChanged, AddressOf CheckBox_EnabledChanged
                RemoveHandler CheckBox.StateChanged, AddressOf CheckBox_StateChanged
                RemoveHandler CheckBox.TextChanged, AddressOf CheckBox_TextChanged
                RemoveHandler CheckBox.ThreeStateChanged, AddressOf CheckBox_ThreeStateChanged
                RemoveHandler CheckBox.ToolTipChanged, AddressOf CheckBox_ToolTipChanged
            End If
        End Sub
        Private Sub MessageBoxForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        End Sub

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