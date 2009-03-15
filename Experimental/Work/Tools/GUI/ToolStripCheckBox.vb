Imports System.ComponentModel, Tools.Experimental.Resources
Imports System.Windows.Forms
Imports System.Drawing

Namespace GUI
    ''' <summary>Embdeds <see cref="NumericUpDown"/> into <see cref="ToolStrip"/></summary>
    <DefaultEvent("CheckedChanged")> _
    <ToolboxBitmap(GetType(CheckBox))> _
    <DefaultProperty("Checked"), DefaultBindingProperty("CheckState")> _
    Public Class ToolStripCheckBox
        Inherits ToolStripControlHost
        Implements IBindableComponent
        ''' <summary>CTor</summary>
        Public Sub New()
            MyBase.New(New CheckBox)
            With Me.CheckBox
                .FlatStyle = FlatStyle.Flat
                .Dock = DockStyle.Fill
                .BackColor = Color.Transparent
                AddHandler .CheckedChanged, AddressOf CheckBox_CheckedChanged
                AddHandler .CheckStateChanged, AddressOf CheckBox_CheckStateChanged
                AddHandler .BindingContextChanged, AddressOf CheckBox_BindingContextChanged
            End With
        End Sub
        ''' <summary><see cref="NumericUpDown"/> which provides functionality of this instance</summary>
        ''' <returns><see cref="NumericUpDown"/> same as <see cref="Control"/></returns>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property CheckBox() As CheckBox
            Get
                Return Me.Control
            End Get
        End Property
#Region "CheckBox"
        ''' <summary>Occurs when the <see cref="System.Windows.Forms.CheckBox.Checked"/> property has been changed.</summary>
        <Category("Action")> _
        Public Event CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        ''' <summary>Occurs when the <see cref="System.Windows.Forms.CheckBox.CheckState"/> property has been changed.</summary>
        <Category("Action")> _
        Public Event CheckStateChanged(ByVal sender As Object, ByVal e As EventArgs)
        ''' <summary>Handles <see cref="CheckBox"/>.<see cref="CheckBox.CheckedChanged">CheckedChanged</see></summary>
        ''' <param name="sender"><see cref="CheckBox"/></param>
        ''' <param name="e">Event arguments</param>
        Private Sub CheckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            OnCheckedChanged(e)
        End Sub
        ''' <summary>Handles <see cref="CheckBox"/>.<see cref="CheckBox.CheckStateChanged">CheckStateChanged</see></summary>
        ''' <param name="sender"><see cref="CheckBox"/></param>
        ''' <param name="e">Event arguments</param>
        Private Sub CheckBox_CheckStateChanged(ByVal sender As Object, ByVal e As EventArgs)
            OnCheckStateChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="CheckedChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnCheckedChanged(ByVal e As EventArgs)
            RaiseEvent CheckedChanged(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="CheckStateChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnCheckStateChanged(ByVal e As EventArgs)
            RaiseEvent CheckStateChanged(Me, e)
        End Sub
        ''' <summary>Gets or set a value indicating whether the System.Windows.Forms.CheckBox is in the checked state.</summary>
        ''' <returns>true if the System.Windows.Forms.CheckBox is in the checked state; otherwise, false. The default value is false.  Note: If the System.Windows.Forms.CheckBox.ThreeState property is set to true, the System.Windows.Forms.CheckBox.Checked property will return true for either a Checked or IndeterminateSystem.Windows.Forms.CheckBox.CheckState.</returns>
        <DefaultValue(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Category("Appearance"), Bindable(True), Description("Gets or set a value indicating whether the System.Windows.Forms.CheckBox is in the checked state.")> _
        Public Property Checked() As Boolean
            Get
                Return CheckBox.Checked
            End Get
            Set(ByVal value As Boolean)
                CheckBox.Checked = value
            End Set
        End Property
        ''' <summary>Gets or sets the state of the System.Windows.Forms.CheckBox.</summary>
        ''' <returns>One of the System.Windows.Forms.CheckState enumeration values. The default value is Unchecked.</returns>
        ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the System.Windows.Forms.CheckState enumeration values.</exception>
        <DefaultValue(GetType(CheckState), "Unchecked")> _
        <Bindable(True), Category("Appearance"), RefreshProperties(RefreshProperties.All)> _
        <Description("Gets or sets the state of the System.Windows.Forms.CheckBox.")> _
        Public Property CheckState() As CheckState
            Get
                Return CheckBox.CheckState
            End Get
            Set(ByVal value As CheckState)
                CheckBox.CheckState = value
            End Set
        End Property
        ''' <summary>Gets or sets a value indicating whether the System.Windows.Forms.CheckBox will allow three check states rather than two.</summary>
        ''' <returns>true if the System.Windows.Forms.CheckBox is able to display three check states; otherwise, false. The default value is false.</returns>
        <DefaultValue(False), Category("Behavior")> _
        Public Property ThreeState() As Boolean
            Get
                Return CheckBox.ThreeState
            End Get
            Set(ByVal value As Boolean)
                CheckBox.ThreeState = value
            End Set
        End Property
        ''' <summary>Gets or sets the horizontal and vertical alignment of the check mark on a System.Windows.Forms.CheckBox control.</summary>
        ''' <returns>One of the System.Drawing.ContentAlignment values. The default value is MiddleLeft.</returns>
        ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the System.Drawing.ContentAlignment enumeration values.</exception>
        <DefaultValue(&H10), Localizable(True), Bindable(True), Category("Appearance")> _
        <Description("Gets or sets the horizontal and vertical alignment of the check mark on a System.Windows.Forms.CheckBox control.")> _
        Public Property CheckAlign() As ContentAlignment
            Get
                Return CheckBox.CheckAlign
            End Get
            Set(ByVal value As ContentAlignment)
                CheckBox.ImageAlign = value
            End Set
        End Property
#End Region
        ''' <summary>Gets or sets the <see cref="System.Windows.Forms.BindingContext"/> for the control.</summary>
        ''' <returns>A <see cref="System.Windows.Forms.BindingContext"/> for the control</returns>
        Public Property BindingContext() As System.Windows.Forms.BindingContext Implements System.Windows.Forms.IBindableComponent.BindingContext
            Get
                Return CheckBox.BindingContext
            End Get
            Set(ByVal value As System.Windows.Forms.BindingContext)
                CheckBox.BindingContext = value
            End Set
        End Property

        <Description("Fired when BindingContextChanged of internal ComboBox occurs"), Category("Data")> _
        Public Event BindingContextChanged(ByVal sender As Object, ByVal e As EventArgs)
        ''' <summary>Handles the <see cref="NumericUpDown"/>.<see cref="NumericUpDown.BindingContextChanged">BindingContextChanged</see> event</summary>
        ''' <param name="sender"><see cref="NumericUpDown"/></param>
        ''' <param name="e">Event arguments</param>
        Private Sub CheckBox_BindingContextChanged(ByVal sender As Object, ByVal e As EventArgs)
            OnBindingContextChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="BindingContextChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnBindingContextChanged(ByVal e As EventArgs)
            RaiseEvent BindingContextChanged(Me, e)
        End Sub

        ''' <summary>Gets the data bindings for the control.</summary>
        ''' <returns>A <see cref="System.Windows.Forms.ControlBindingsCollection"/> that contains the <see cref="System.Windows.Forms.Binding"/> objects for the control.</returns>
        ''' <seelaso cref="NumericUpDown.DataBindings"/>
        <WinFormsSRDescriptionAttribute("ControlBindingsDescr")> _
        <ParenthesizePropertyName(True), RefreshProperties(RefreshProperties.All), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Data")> _
        Public ReadOnly Property DataBindings() As ControlBindingsCollection Implements System.Windows.Forms.IBindableComponent.DataBindings
            Get
                Return CheckBox.DataBindings
            End Get
        End Property
    End Class
End Namespace