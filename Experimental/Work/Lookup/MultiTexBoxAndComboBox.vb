Imports Tools.ComponentModelT, System.ComponentModel, Tools
Imports Tools.CollectionsT.GenericT
Imports System.Windows.Forms

''' <summary>Control consisiting of multiple <see cref="TextBoxAndComboBox">TextBoxAndComboBoxes</see></summary>
Public Class MultiTexBoxAndComboBox
    Inherits MultiControl(Of TextBoxAndComboBoxItem)
#Region "Group events"
    ''' <summary>Prřidává a odebírá ovladače událostí položek</summary>
    ''' <param name="Add">True pro přidání, false pro odebrání</param>
    ''' <param name="Item">Položka</param>
    Protected Overrides Sub Handlers(ByVal Add As Boolean, ByVal item As TextBoxAndComboBoxItem)
        MyBase.Handlers(Add, item)
        If Add Then
            AddHandler item.TextBox.TextChanged, AddressOf TextBox_TextChanged
            AddHandler item.ComboBox.TextChanged, AddressOf ComboBox_TextChanged
            AddHandler item.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        Else
            RemoveHandler item.TextBox.TextChanged, AddressOf TextBox_TextChanged
            RemoveHandler item.ComboBox.TextChanged, AddressOf ComboBox_TextChanged
            RemoveHandler item.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        End If
    End Sub
#Region "Handelrs"
    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim i As Integer = 0
        For Each Item As TextBoxAndComboBoxItem In Me.Items
            If Item.TextBox Is sender Then
                OnTextBoxTextChanged(New ListWithEvents(Of TextBoxAndComboBoxItem).ItemIndexEventArgs(Item, i))
                Exit Sub
            End If
            i += 1
        Next
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim i As Integer = 0
        For Each Item As TextBoxAndComboBoxItem In Me.Items
            If Item.ComboBox Is sender Then
                OnTextBoxTextChanged(New ListWithEvents(Of TextBoxAndComboBoxItem).ItemIndexEventArgs(Item, i))
                Exit Sub
            End If
            i += 1
        Next
    End Sub

    Private Sub ComboBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim i As Integer = 0
        For Each Item As TextBoxAndComboBoxItem In Me.Items
            If Item.ComboBox Is sender Then
                OnTextBoxTextChanged(New ListWithEvents(Of TextBoxAndComboBoxItem).ItemIndexEventArgs(Item, i))
                Exit Sub
            End If
            i += 1
        Next
    End Sub
#End Region
    ''' <summary>Raised when text of any text box changes</summary>
    <KnownCategory(KnownCategoryAttribute.AnotherCategories.PropertyChanged)> _
    <Description("Raised when text of any text box changes")> _
    Public Event TextBoxTextChanged As EventHandler(Of ListWithEvents(Of TextBoxAndComboBoxItem).ItemIndexEventArgs)
    ''' <summary>Raised when text of any combo box changes</summary>
    <KnownCategory(KnownCategoryAttribute.AnotherCategories.PropertyChanged)> _
    <Description("Raised when text of any combo box changes")> _
    Public Event ComboBoxTextChanged As EventHandler(Of ListWithEvents(Of TextBoxAndComboBoxItem).ItemIndexEventArgs)
    ''' <summary>Raised selected insex in aany combo box changes</summary>
    <KnownCategory(KnownCategoryAttribute.AnotherCategories.PropertyChanged)> _
    <Description("Raised selected insex in aany combo box changes")> _
    Public Event SelectedIndexChanged As EventHandler(Of ListWithEvents(Of TextBoxAndComboBoxItem).ItemIndexEventArgs)
    ''' <summary>Raises the <see cref="TextBoxTextChanged"/> event</summary>
    ''' <param name="e">event arguments</param>
    Protected Overridable Sub OnTextBoxTextChanged(ByVal e As ListWithEvents(Of TextBoxAndComboBoxItem).ItemIndexEventArgs)
        RaiseEvent TextBoxTextChanged(Me, e)
    End Sub
    ''' <summary>Raises the <see cref="[SelectedIndexChanged]"/> event</summary>
    ''' <param name="e">event arguments</param>
    Protected Overridable Sub OnSelectedIndexChanged(ByVal e As ListWithEvents(Of TextBoxAndComboBoxItem).ItemIndexEventArgs)
        RaiseEvent [SelectedIndexChanged](Me, e)
    End Sub
    ''' <summary>Raises the <see cref="ComboBoxTextChanged"/> event</summary>
    ''' <param name="e">event arguments</param>
    Protected Overridable Sub OnComboBoxTextChanged(ByVal e As ListWithEvents(Of TextBoxAndComboBoxItem).ItemIndexEventArgs)
        RaiseEvent ComboBoxTextChanged(Me, e)
    End Sub
#End Region
#Region "Group properties"
    ''' <summary>Gets or sets text of all the textboxes togehther</summary>
    ''' <returns>Text of all the textboxes whan all texts are same; null otherwise</returns>
    ''' <value>New text for all the text boxes</value>
    <Description("Gets or sets text of all the textboxes togehther")> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property TextBoxText() As String
        Get
            Dim text As String = Nothing : Dim i% = 0
            For Each item As TextBoxAndComboBoxItem In Me.Items
                If i = 0 Then
                    text = item.Text
                ElseIf text <> item.Text Then
                    Return Nothing
                End If
            Next
            Return text
        End Get
        Set(ByVal value As String)
            For Each item As TextBoxAndComboBoxItem In Me.Items
                item.Text = value
            Next
        End Set
    End Property
    ''' <summary>Gets or sets selected index of all the comboboxes</summary>
    ''' <returns>Selected index of all the combo boxes, it selected indexes are same; null otherwise</returns>
    ''' <value>New selected index in all the comboboxes</value>
    ''' <exception cref="ArgumentException">Value being set is null</exception>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
    Public Property SelectedIndex() As Nullable(Of Integer)
        Get
            Dim sindex As Integer = 0 : Dim i% = 0
            For Each item As TextBoxAndComboBoxItem In Me.Items
                If i = 0 Then
                    sindex = item.SelectedIndex
                ElseIf sindex <> item.SelectedIndex Then
                    Return Nothing
                End If
            Next
            Return sindex
        End Get
        Set(ByVal value As Nullable(Of Integer))
            If Not value.HasValue Then Throw New ArgumentException("value")
            For Each item As TextBoxAndComboBoxItem In Me.Items
                item.SelectedIndex = value
            Next
        End Set
    End Property

    ''' <summary>Gets or sets <see cref="ComboBox.DropDownStyle"/> of all the comboboxes</summary>
    ''' <returns><see cref="ComboBox.DropDownStyle"/> of all the combo boxes, it they are same; null otherwise</returns>
    ''' <value>New <see cref="ComboBox.DropDownStyle"/> of all the comboboxes</value>
    ''' <exception cref="ArgumentException">Value being set is null</exception>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
    Public Property DropDownStyle() As Nullable(Of ComboBoxStyle)
        Get
            Dim sindex As ComboBoxStyle : Dim i% = 0
            For Each item As TextBoxAndComboBoxItem In Me.Items
                If i = 0 Then
                    sindex = item.ComboBox.DropDownStyle
                ElseIf sindex <> item.ComboBox.DropDownStyle Then
                    Return Nothing
                End If
            Next
            Return sindex
        End Get
        Set(ByVal value As Nullable(Of ComboBoxStyle))
            If Not value.HasValue Then Throw New ArgumentException("value")
            For Each item As TextBoxAndComboBoxItem In Me.Items
                item.ComboBox.DropDownStyle = value
            Next
        End Set
    End Property

    ''' <summary>Gets or sets text of all the comboboxes togehther</summary>
    ''' <returns>Text of all the comboboxes when all texts are same; null otherwise</returns>
    ''' <value>New text for all the combo boxes</value>
    <Browsable(False)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property ComboBoxText() As String
        Get
            Dim text As String = Nothing : Dim i% = 0
            For Each item As TextBoxAndComboBoxItem In Me.Items
                If i = 0 Then
                    text = item.ComboBox.Text
                ElseIf text <> item.ComboBox.Text Then
                    Return Nothing
                End If
            Next
            Return text
        End Get
        Set(ByVal value As String)
            For Each item As TextBoxAndComboBoxItem In Me.Items
                item.ComboBox.Text = value
            Next
        End Set
    End Property
    ''' <summary>Sets the <see cref="ComboBox.Items"/> property of all the comboboxes</summary>
    ''' <param name="Items">New items to be set</param>
    ''' <exception cref="ArgumentNullException"><paramref name="Items"/> is null</exception>
    Public Sub SetItems(ByVal Items As Object())
        If Items Is Nothing Then Throw New ArgumentNullException("Items")
        For Each item As TextBoxAndComboBoxItem In Me.Items
            item.Items.Clear()
            item.Items.AddRange(Items)
        Next
    End Sub
#End Region

    ''' <summary>Item of <see cref="MultiTexBoxAndComboBox"/> control</summary>
    Public Class TextBoxAndComboBoxItem
        Inherits MultiControlItemWithOwner(Of TextBoxAndComboBox, MultiTexBoxAndComboBox)
#Region "Quick properties"
        ''' <summary>Text textového pole</summary>
        ''' <returns><see cref="Control"/>.<see cref="TextBoxAndComboBox.Text">Text</see></returns>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <Description("text textového pole")> _
        <DefaultValue(GetType(String), Nothing)> _
        Public Property Text() As String
            Get
                Return Control.Text
            End Get
            Set(ByVal value As String)
                Control.Text = value
            End Set
        End Property
        <Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(Drawing.Design.UITypeEditor))> _
        <Tools.ResourcesT.SRDescription("ComboBoxItemsDescr"), MergableProperty(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Localizable(True), KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
        <Description("Items in combo box")> _
        Public ReadOnly Property Items() As ComboBox.ObjectCollection
            Get
                Return Control.Items
            End Get
        End Property
        ''' <summary>Gets or sets index of item currently selected in combo box</summary>
        ''' <returns><see cref="Control"/>.<see cref="TextBoxAndComboBox.SelectedIndex">SelectedIndex</see></returns>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property SelectedIndex() As Integer
            Get
                Return Control.SelectedIndex
            End Get
            Set(ByVal value As Integer)
                Control.SelectedIndex = value
            End Set
        End Property
        ''' <summary>Gets or sets item currently selected in combo box</summary>
        ''' <returns><see cref="Control"/>.<see cref="TextBoxAndComboBox.SelectedItem">SelectedItem</see></returns>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property SelecedItem() As Object
            Get
                Return Control.SelectedItem
            End Get
            Set(ByVal value As Object)
                Control.SelectedItem = value
            End Set
        End Property

#End Region
#Region "Controls"
        ''' <summary>Text box</summary>
        ''' <returns><see cref="Control"/>.<see cref="TextBoxAndComboBox.TextBox">TextBox</see></returns> 
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property TextBox() As TextBox
            Get
                Return Me.Control.TextBox
            End Get
        End Property
        ''' <summary>Combo box</summary>
        ''' <returns><see cref="Control"/>.<see cref="TextBoxAndComboBox.ComboBox">ComboBox</see></returns>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property ComboBox() As ComboBox
            Get
                Return Me.Control.ComboBox
            End Get
        End Property
#End Region
    End Class
End Class


