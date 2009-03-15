Imports System.Windows.Forms, System.ComponentModel
Namespace GUI
    '''' <summary><see cref="System.Windows.Forms.ToolStripComboBox"/> s Databindingem</summary>
    '<Drawing.ToolboxBitmap(GetType(System.Windows.Forms.ToolStripComboBox))> _
    '<DefaultBindingProperty("Text")> _
    '<LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")> _
    'Public Class DataBoundToolStripComboBox
    '    Inherits System.Windows.Forms.ToolStripComboBox
    '    Implements IBindableComponent
    '    ''' <summary>Gets or sets the property to display for <see cref="ComboBox"/>.</summary>
    '    ''' <returns>A <see cref="System.String"/> specifying the name of an object property that is contained in the collection specified by the <see cref="System.Windows.Forms.ListControl.DataSource"/> property. The default is an empty string ("").</returns>
    '    <DefaultValue(""), TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")> _
    '    <Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(Drawing.Design.UITypeEditor))> _
    '    <Category("Data")> _
    '    Public Property DisplayMember() As String
    '        Get
    '            Return Me.ComboBox.DisplayMember
    '        End Get
    '        Set(ByVal value As String)
    '            Me.ComboBox.DisplayMember = value
    '        End Set
    '    End Property
    '    ''' <summary>Gets or sets the property to use as the actual value for the items in <see cref="ComboBox"/>.</summary>
    '    ''' <returns>A <see cref="System.String"/> representing the name of an object property that is contained in the collection specified by the <see cref="System.Windows.Forms.ListControl.DataSource"/> property. The default is an empty string ("").</returns>
    '    ''' <exception cref="System.ArgumentException">The specified property cannot be found on the object specified by the <see cref="System.Windows.Forms.ListControl.DataSource"/> property.</exception>
    '    <Category("Data"), DefaultValue("")> _
    '    <Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(Drawing.Design.UITypeEditor))> _
    '    Public Property ValueMember() As String
    '        Get
    '            Return Me.ComboBox.ValueMember
    '        End Get
    '        Set(ByVal value As String)
    '            Me.ComboBox.ValueMember = value
    '        End Set
    '    End Property
    '    ''' <summary>Gets or sets the data source for this <see cref="System.Windows.Forms.ComboBox"/>.</summary>
    '    ''' <returns>An object that implements the <see cref="System.Collections.IList"/> interface, such as a <see cref="System.Data.DataSet"/> or an <see cref="System.Array"/>. The default is null.</returns>
    '    <DefaultValue(CStr(Nothing)), AttributeProvider(GetType(IListSource))> _
    '    <Category("Data"), RefreshProperties(RefreshProperties.Repaint)> _
    '    Public Property DataSource() As Object
    '        Get
    '            Return Me.ComboBox.DataSource
    '        End Get
    '        Set(ByVal value As Object)
    '            Me.ComboBox.DataSource = value
    '        End Set
    '    End Property
    '    ''' <summary>Gets or sets the index specifying the currently selected item.</summary>
    '    ''' <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
    '    <Bindable(True)> _
    '    Public Shadows Property SelectedIndex() As Integer
    '        Get
    '            Return MyBase.SelectedIndex
    '        End Get
    '        Set(ByVal value As Integer)
    '            MyBase.SelectedIndex = value
    '        End Set
    '    End Property
    '    ''' <summary>Gets or sets the value of the member property specified by the <see cref="System.Windows.Forms.ListControl.ValueMember"/> property.</summary>
    '    ''' <returns>An object containing the value of the member of the data source specified by the <see cref="System.Windows.Forms.ListControl.ValueMember"/> property.</returns>
    '    ''' <exception cref="System.InvalidOperationException">The assigned value is null or the empty string ("").</exception>
    '    <Bindable(True)> _
    '    Public Property SelectedValue() As Object
    '        Get
    '            Return Me.ComboBox.SelectedValue
    '        End Get
    '        Set(ByVal value As Object)
    '            Me.ComboBox.SelectedValue = value
    '        End Set
    '    End Property
    '    ''' <summary>Gets the data bindings for the control.</summary>
    '    ''' <returns>A <see cref="System.Windows.Forms.ControlBindingsCollection"/> that contains the <see cref="System.Windows.Forms.Binding"/> objects for the control.</returns>
    '    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    '    <ParenthesizePropertyName(True), RefreshProperties(RefreshProperties.All)> _
    '    <Category("Data")> _
    '    Public ReadOnly Property DataBindings() As ControlBindingsCollection Implements System.Windows.Forms.IBindableComponent.DataBindings
    '        Get
    '            Return Me.ComboBox.DataBindings
    '        End Get
    '    End Property
    '    ''' <summary>Gets or sets the <see cref="System.Windows.Forms.BindingContext"/> for the control.</summary>
    '    ''' <returns>A <see cref="System.Windows.Forms.BindingContext"/> for the control.</returns>
    '    Public Property BindingContext() As System.Windows.Forms.BindingContext Implements System.Windows.Forms.IBindableComponent.BindingContext
    '        Get
    '            Return Me.ComboBox.BindingContext
    '        End Get
    '        Set(ByVal value As System.Windows.Forms.BindingContext)
    '            Me.ComboBox.BindingContext = value
    '        End Set
    '    End Property
    'End Class

    '''' <summary><see cref="ToolStripLabel"/> který umožòuje databinding</summary>
    '''' <remarks><seealso>http://forums.devx.com/archive/index.php/t-153607.html</seealso></remarks>
    '<Drawing.ToolboxBitmap(GetType(System.Windows.Forms.ToolStripLabel))> _
    '<DefaultBindingProperty("Text")> _
    'Public Class DataBoundToolStripLabel : Inherits ToolStripLabel
    '    Implements IBindableComponent
    '    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="BindingContext"/></summary>
    '    <EditorBrowsable(EditorBrowsableState.Never)> _
    '    Private _context As BindingContext = Nothing
    '    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="DataBindings"/></summary>
    '    <EditorBrowsable(EditorBrowsableState.Never)> _
    '    Private _bindings As ControlBindingsCollection
    '    ''' <summary>Gets or sets the collection of currency managers for the <see cref="System.Windows.Forms.IBindableComponent"/>.</summary>
    '    ''' <returns>The collection of <see cref="System.Windows.Forms.BindingManagerBase"/> objects for this <see cref="DataBoundToolStripLabel"/>.</returns>
    '    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
    '    Public Property BindingContext() As System.Windows.Forms.BindingContext Implements System.Windows.Forms.IBindableComponent.BindingContext
    '        Get
    '            If Nothing Is _context Then
    '                _context = New BindingContext()
    '            End If
    '            Return _context
    '        End Get
    '        Set(ByVal value As BindingContext)
    '            _context = value
    '        End Set
    '    End Property
    '    ''' <summary>Gets the collection of data-binding objects for this <see cref="System.Windows.Forms.IBindableComponent"/>.</summary>
    '    ''' <returns>The <see cref="System.Windows.Forms.ControlBindingsCollection"/> for this <see cref="DataBoundToolStripLabel"/>.</returns>
    '    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
    '    <ParenthesizePropertyName(True), RefreshProperties(RefreshProperties.All)> _
    '    <Category("Data")> _
    '    Public ReadOnly Property DataBindings() As System.Windows.Forms.ControlBindingsCollection Implements System.Windows.Forms.IBindableComponent.DataBindings
    '        Get
    '            If _bindings Is Nothing Then
    '                _bindings = New ControlBindingsCollection(Me)
    '            End If
    '            Return _bindings
    '        End Get
    '    End Property
    '    ''' <summary>Gets or sets the text that is to be displayed on the item.</summary>
    '    ''' <returns>A string representing the item's text. The default value is the empty string ("").</returns>
    '    <Bindable(True)> _
    '    Public Overrides Property Text() As String
    '        Get
    '            Return MyBase.Text
    '        End Get
    '        Set(ByVal value As String)
    '            MyBase.Text = value
    '        End Set
    '    End Property
    'End Class

End Namespace
