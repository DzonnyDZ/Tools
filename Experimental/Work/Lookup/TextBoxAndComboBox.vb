Imports System.ComponentModel, Tools.ComponentModelT
Imports System.Windows.Forms

Public Class TextBoxAndComboBox
    ''' <summary>A <see cref="TextBox"/> control</summary>
    <TypeConverter(GetType(ExpandableObjectConverter))> _
    <Category("Appearance")> _
    <Description("Text box control")> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public ReadOnly Property TextBox() As TextBox
        <DebuggerStepThrough()> Get
            Return txtTextBox
        End Get
    End Property
    ''' <summary>A <see cref="ComboBox"/> control</summary>
    <TypeConverter(GetType(ExpandableObjectConverter))> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    <Description("Combo box control")> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    Public ReadOnly Property ComboBox() As ComboBox
        <DebuggerStepThrough()> Get
            Return cmbComboBox
        End Get
    End Property

    ''' <summary>Text of text box</summary>
    ''' <returns><see cref="TextBox"/>.<see cref="TextBox.Text">Text</see></returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Overrides Property Text() As String
        Get
            Return TextBox.Text
        End Get
        Set(ByVal value As String)
            TextBox.Text = value
            MyBase.Text = value
        End Set
    End Property
    ''' <summary>Items in combo box</summary>
    ''' <returns><see cref="ComboBox"/>.<see cref="ComboBox.Items">Items</see></returns>
    <Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(Drawing.Design.UITypeEditor))> _
    <Tools.ResourcesT.SRDescription("ComboBoxItemsDescr"), MergableProperty(False)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    <Localizable(True), KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
    <Description("Items in combo box")> _
    Public ReadOnly Property Items() As ComboBox.ObjectCollection
        Get
            Return ComboBox.Items
        End Get
    End Property
    ''' <summary>Gets or sets index of currently selected item in <see cref="ComboBox"/></summary>
    ''' <returns><see cref="ComboBox"/>.<see cref="ComboBox.SelectedIndex">SelectedIndex</see></returns>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property SelectedIndex() As Integer
        Get
            Return ComboBox.SelectedIndex
        End Get
        Set(ByVal value As Integer)
            ComboBox.SelectedIndex = value
        End Set
    End Property
    ''' <summary>Gets or sets item currently selected in <see cref="ComboBox"/></summary>
    ''' <returns><see cref="ComboBox"/>.<see cref="ComboBox.SelectedItem">SelectedItem</see></returns>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property SelectedItem() As Object
        Get
            Return ComboBox.SelectedItem
        End Get
        Set(ByVal value As Object)
            ComboBox.SelectedItem = value
        End Set
    End Property
    ''' <summary>Raised when value of <see cref="ComboBox"/>.<see cref="ComboBox.SelectedIndex">SelectedIndex</see> property changes</summary>
    <Tools.ResourcesT.SRDescription("selectedIndexChangedEventDescr"), Category("Behavior")> _
    Public Event SelectedIndexChanged As EventHandler
    ''' <summary>Raises the <see cref="[SelectedIndexChanged]"/> event</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnSelectedIndexChanged(ByVal e As EventArgs)
        RaiseEvent SelectedIndexChanged(Me, e)
    End Sub
    Private Sub cmbComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComboBox.SelectedIndexChanged
        OnSelectedIndexChanged(e)
    End Sub
End Class
