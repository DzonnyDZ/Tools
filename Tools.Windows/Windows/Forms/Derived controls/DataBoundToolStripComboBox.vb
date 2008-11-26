Imports System.Windows.Forms, System.ComponentModel
Imports Tools.ComponentModelT

Namespace WindowsT.FormsT
#If Config <= RC Then 'Stage: RC
    ''' <summary><see cref="System.Windows.Forms.ToolStripComboBox"/> that allows databinding</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="RC"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    <System.Drawing.ToolboxBitmap(GetType(DataBoundToolStripComboBox), "DataBoundToolStripComboBox.bmp")> _
   <DefaultBindingProperty("Text")> _
   <LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")> _
   <ComponentModelT.Prefix("dtc")> _
    Public Class DataBoundToolStripComboBox
        Inherits System.Windows.Forms.ToolStripComboBox
        Implements IBindableComponent
        ''' <summary>Gets or sets the property to display for <see cref="ComboBox"/>.</summary>
        ''' <returns>A <see cref="System.String"/> specifying the name of an object property that is contained in the collection specified by the <see cref="System.Windows.Forms.ListControl.DataSource"/> property. The default is an empty string ("").</returns>
        <DefaultValue(""), TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")> _
        <Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(System.Drawing.Design.UITypeEditor))> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
        <LDescription(GetType(DerivedControls), "DisplayMember_d")> _
        Public Property DisplayMember() As String
            Get
                Return Me.ComboBox.DisplayMember
            End Get
            Set(ByVal value As String)
                Me.ComboBox.DisplayMember = value
            End Set
        End Property
        ''' <summary>Gets or sets the property to use as the actual value for the items in <see cref="ComboBox"/>.</summary>
        ''' <returns>A <see cref="System.String"/> representing the name of an object property that is contained in the collection specified by the <see cref="System.Windows.Forms.ListControl.DataSource"/> property. The default is an empty string ("").</returns>
        ''' <exception cref="System.ArgumentException">The specified property cannot be found on the object specified by the <see cref="System.Windows.Forms.ListControl.DataSource"/> property.</exception>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data), DefaultValue("")> _
        <LDescription(GetType(DerivedControls), "ValueMember_d")> _
        <Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(System.Drawing.Design.UITypeEditor))> _
        Public Property ValueMember() As String
            Get
                Return Me.ComboBox.ValueMember
            End Get
            Set(ByVal value As String)
                Me.ComboBox.ValueMember = value
            End Set
        End Property
        ''' <summary>Gets or sets the data source for <see cref="ComboBox"/>.</summary>
        ''' <returns>An object that implements the <see cref="System.Collections.IList"/> interface, such as a <see cref="System.Data.DataSet"/> or an <see cref="System.Array"/>. The default is null.</returns>
        <DefaultValue(CStr(Nothing)), AttributeProvider(GetType(IListSource))> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data), RefreshProperties(RefreshProperties.Repaint)> _
        <LDescription(GetType(WindowsT.FormsT.DerivedControls), "DataSource_d")> _
        Public Property DataSource() As Object
            Get
                Return Me.ComboBox.DataSource
            End Get
            Set(ByVal value As Object)
                Me.ComboBox.DataSource = value
            End Set
        End Property
        ''' <summary>Gets or sets the index specifying the currently selected item.</summary>
        ''' <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
        <Bindable(True)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
        Public Shadows Property SelectedIndex() As Integer
            Get
                Return MyBase.SelectedIndex
            End Get
            Set(ByVal value As Integer)
                MyBase.SelectedIndex = value
            End Set
        End Property
        ''' <summary>Gets or sets the value of the member property specified by the <see cref="System.Windows.Forms.ListControl.ValueMember"/> property.</summary>
        ''' <returns>An object containing the value of the member of the data source specified by the <see cref="System.Windows.Forms.ListControl.ValueMember"/> property.</returns>
        ''' <exception cref="System.InvalidOperationException">The assigned value is null or the empty string ("").</exception>
        <Bindable(True)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
        Public Property SelectedValue() As Object
            Get
                Return Me.ComboBox.SelectedValue
            End Get
            Set(ByVal value As Object)
                Me.ComboBox.SelectedValue = value
            End Set
        End Property
        ''' <summary>Gets the data bindings for the control.</summary>
        ''' <returns>A <see cref="System.Windows.Forms.ControlBindingsCollection"/> that contains the <see cref="System.Windows.Forms.Binding"/> objects for the control.</returns>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <ParenthesizePropertyName(True), RefreshProperties(RefreshProperties.All)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
        <LDescription(GetType(WindowsT.FormsT.DerivedControls), "DataBindings_d")> _
        Public ReadOnly Property DataBindings() As ControlBindingsCollection Implements System.Windows.Forms.IBindableComponent.DataBindings
            Get
                Return Me.ComboBox.DataBindings
            End Get
        End Property
        ''' <summary>Gets or sets the <see cref="System.Windows.Forms.BindingContext"/> for the control.</summary>
        ''' <returns>A <see cref="System.Windows.Forms.BindingContext"/> for the control.</returns>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public Property BindingContext() As System.Windows.Forms.BindingContext Implements System.Windows.Forms.IBindableComponent.BindingContext
            Get
                Return Me.ComboBox.BindingContext
            End Get
            Set(ByVal value As System.Windows.Forms.BindingContext)
                Me.ComboBox.BindingContext = value
            End Set
        End Property
        ''' <summary>CTor</summary>
        Public Sub New()
            AddHandler Me.ComboBox.BindingContextChanged, AddressOf OnBindingContextChanged
        End Sub
        ''' <summary>Called when <see cref="System.Windows.Forms.ComboBox.BindingContextChanged"/> of <see cref="ComboBox"/> occures</summary>
        ''' <param name="sender">Source of event (always <see cref="ComboBox"/></param>
        ''' <param name="e">Event parameters</param>
        Private Sub OnBindingContextChanged(ByVal sender As Object, ByVal e As EventArgs)
            OnBindingContextChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="BindingContextChanged"/> event</summary>
        ''' <param name="e">Event parameters</param>
        ''' <remarks>Note for inheritors: Always call base class's method in order event to be raised</remarks>
        Protected Overridable Sub OnBindingContextChanged(ByVal e As EventArgs)
            RaiseEvent BindingContextChanged(Me, e)
        End Sub
        ''' <summary>Fired when <see cref="System.Windows.Forms.ComboBox.BindingContextChanged"/> of <see cref="ComboBox"/> occures</summary>
        ''' <param name="sender">Source of the event - rhis isntance of <see cref="DataBoundToolStripComboBox"/></param>
        ''' <param name="e">Event parameters</param>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data), Description("Fired when BindingContextChanged of internal ComboBox occurs")> _
        Public Event BindingContextChanged(ByVal sender As Object, ByVal e As EventArgs)
    End Class
#End If
End Namespace
