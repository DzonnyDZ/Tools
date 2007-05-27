Imports System.Windows.Forms, System.ComponentModel
Namespace WindowsT.FormsT
#If Config <= RC Then 'Stage:RC
    ''' <summary><see cref="ToolStripLabel"/> that allows databinding</summary>
    ''' <remarks><seealso>http://forums.devx.com/archive/index.php/t-153607.html</seealso></remarks>
    <System.Drawing.ToolboxBitmap(GetType(DataBoundToolStripLabel), "DataBoundToolStripLabel.bmp")> _
    <DefaultBindingProperty("Text")> _
    <ComponentModelT.Prefix("dtl")> _
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(DataBoundToolStripLabel), LastChMMDDYYYY:="05/13/2007")> _
    Public Class DataBoundToolStripLabel : Inherits ToolStripLabel
        Implements IBindableComponent
        ''' <summary>Contains value of the <see cref="BindingContext"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _context As BindingContext = Nothing
        ''' <summary>Contains value of the<see cref="DataBindings"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _bindings As ControlBindingsCollection
        ''' <summary>Gets or sets the collection of currency managers for the <see cref="System.Windows.Forms.IBindableComponent"/>.</summary>
        ''' <returns>The collection of <see cref="System.Windows.Forms.BindingManagerBase"/> objects for this <see cref="DataBoundToolStripLabel"/>.</returns>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Property BindingContext() As System.Windows.Forms.BindingContext Implements System.Windows.Forms.IBindableComponent.BindingContext
            Get
                If Nothing Is _context Then
                    _context = New BindingContext()
                End If
                Return _context
            End Get
            Set(ByVal value As BindingContext)
                _context = value
                OnBindingContextChanged(EventArgs.Empty)
            End Set
        End Property
        ''' <summary>Gets the collection of data-binding objects for this <see cref="System.Windows.Forms.IBindableComponent"/>.</summary>
        ''' <returns>The <see cref="System.Windows.Forms.ControlBindingsCollection"/> for this <see cref="DataBoundToolStripLabel"/>.</returns>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <ParenthesizePropertyName(True), RefreshProperties(RefreshProperties.All)> _
        <Category(Tools.WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Data)> _
        <Description("Gets the collection of data-binding objects for this DataBoundToolStripLabel")> _
        Public ReadOnly Property DataBindings() As System.Windows.Forms.ControlBindingsCollection Implements System.Windows.Forms.IBindableComponent.DataBindings
            Get
                If _bindings Is Nothing Then
                    _bindings = New ControlBindingsCollection(Me)
                End If
                Return _bindings
            End Get
        End Property
        ''' <summary>Gets or sets the text that is to be displayed on the item.</summary>
        ''' <returns>A string representing the item's text. The default value is the empty string ("").</returns>
        <Bindable(True)> _
        Public Overrides Property Text() As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
                MyBase.Text = value
            End Set
        End Property
        ''' <summary>Raises the <see cref="BindingContextChanged"/> event</summary>
        ''' <param name="e">Event parameters</param>
        ''' <remarks>Note for inheritors: Always call base class's method in order event to be raised</remarks>
        Protected Overridable Sub OnBindingContextChanged(ByVal e As EventArgs)
            RaiseEvent BindingContextChanged(Me, e)
        End Sub
        ''' <summary>Fired when <see cref="System.Windows.Forms.ComboBox.BindingContextChanged"/> of <see cref="ComboBox"/> occures</summary>
        ''' <param name="sender">Source of the event - rhis isntance of <see cref="DataBoundToolStripComboBox"/></param>
        ''' <param name="e">Event parameters</param>
        <Category(Tools.WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Data), Description("Fired when BindingContextChanged of internal ComboBox occurs")> _
        Public Event BindingContextChanged(ByVal sender As Object, ByVal e As EventArgs)
    End Class
#End If
End Namespace
