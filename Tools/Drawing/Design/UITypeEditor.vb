Imports System.Drawing.Design, System.ComponentModel, System.Drawing, System.Windows.Forms
Imports System.Windows.Forms.Design

#If Config <= Nightly Then 'Stage:Nihghtly
Namespace DrawingT.DesignT
    ''' <summary>Base class for type-safe <see cref="UITypeEditor">UITypeEditors</see></summary>
    ''' <typeparam name="T">Type of value being edited</typeparam>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
 ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public MustInherit Class UITypeEditor(Of T)
        Inherits UITypeEditor
        ''' <summary>Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.</summary>
        ''' <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
        ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
        ''' <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services. </param>
        ''' <param name="value">The object to edit. </param>
        ''' <remarks>Use type-safe overload instead</remarks>
        ''' <exception cref="TypeMismatchException"><paramref name="value"/> is not of type <typeparamref name="T"/> and it is not null.</exception>
        <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type safe-overload instead")> _
        Public NotOverridable Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
            If value IsNot Nothing AndAlso Not TypeOf value Is T Then Throw New TypeMismatchException("value", value, GetType(T))
            Return EditValue(context, provider, DirectCast(value, T))
        End Function
        ''' <summary>Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.</summary>
        ''' <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
        ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
        ''' <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services. </param>
        ''' <param name="value">The object to edit. </param>
        Public MustOverride Overloads Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As T) As T
        ''' <summary>Paints a representation of the value of an object using the specified <see cref="T:System.Drawing.Design.PaintValueEventArgs" />.</summary>
        ''' <param name="e">A <see cref="T:System.Drawing.Design.PaintValueEventArgs" /> that indicates what to paint and where to paint it. </param>
        ''' <remarks>Use type-safe overload instead</remarks>
        <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type-safe overload instead")> _
        Public NotOverridable Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
            PaintValue(New PaintValueEventArgs(e))
        End Sub
        ''' <summary>Paints a representation of the value of an object using the specified <see cref="T:System.Drawing.Design.PaintValueEventArgs" />.</summary>
        ''' <param name="e">A <see cref="PaintValueEventArgs" /> that indicates what to paint and where to paint it. </param>
        Public Overridable Overloads Sub PaintValue(ByVal e As PaintValueEventArgs)
            MyBase.PaintValue(e)
        End Sub
        ''' <summary>Type-safe implementation of <see cref="System.Drawing.Design.PaintValueEventArgs"/></summary>
        Public Class PaintValueEventArgs
            Inherits System.Drawing.Design.PaintValueEventArgs
            ''' <summary>Initializes a new instance of the <see cref="PaintValueEventArgs" /> class using the specified values.</summary>
            ''' <param name="context">The context in which the value appears.</param>
            ''' <param name="value">The value to paint.</param>
            ''' <param name="graphics">The <see cref="System.Drawing.Graphics" /> object with which drawing is to be done.</param>
            ''' <param name="bounds">The <see cref="System.Drawing.Rectangle" /> in which drawing is to be done.</param>
            ''' <exception cref="System.ArgumentNullException">graphics is null.</exception>
            Public Sub New(ByVal context As ITypeDescriptorContext, ByVal value As T, ByVal graphics As Graphics, ByVal bounds As Rectangle)
                MyBase.new(context, value, graphics, bounds)
            End Sub
            ''' <summary>Initializes a new instance of the <see cref="PaintValueEventArgs" /> class from another instance of <see cref="System.Drawing.Design.PaintValueEventArgs"/>.</summary>
            ''' <param name="other">A <see cref="System.Drawing.Design.PaintValueEventArgs"/></param>
            ''' <exception cref="TypeMismatchException"><paramref name="other"/>.<see cref="System.Drawing.Design.PaintValueEventArgs.Value">Value</see> is not of type <typeparamref name="T"/>.</exception>
            Public Sub New(ByVal other As System.Drawing.Design.PaintValueEventArgs)
                Me.New(other.Context, VerifyValue(other.Value), other.Graphics, other.Bounds)
            End Sub
            ''' <summary>Verifies that given value is of type <typeparamref name="T"/></summary>
            ''' <param name="value">Value to be verified</param>
            ''' <remarks><paramref name="value"/></remarks>
            ''' <exception cref="TypeMismatchException"><paramref name="value"/> is not of type <typeparamref name="T"/>.</exception>
            Private Shared Function VerifyValue(ByVal value As Object) As T
                If Not TypeOf value Is T Then Throw New TypeMismatchException(String.Format(ResourcesT.Exceptions.OnlyValueSOfType0CanBeEditedBy0, GetType(T).Name, GetType(UITypeEditor(Of T)).Name), value, GetType(T))
                Return value
            End Function
            ''' <summary>Gets the value to paint.</summary>
            ''' <returns>An object indicating what to paint.</returns>
            Shadows ReadOnly Property Value() As T
                Get
                    Return MyBase.Value
                End Get
            End Property
        End Class
    End Class

    ''' <summary>Implements drop-down <see cref="UITypeEditor"/> represented by WinForms <see cref="Control"/></summary>
    ''' <typeparam name="T">Type of value being edited</typeparam>
    ''' <typeparam name=" TControl">Type of <see cref="Control"/> that serves as editor GUI. It must implement <see cref="IEditor(Of T)"/> and has default CTor. You can derive your control from <see cref="DropDownUITypeEditorControlBase(Of T)"/> whic implements basic functionality of <see cref="IEditor(Of T)"/> interface</typeparam>
    ''' <seelaso cref="IEditor(Of T)"/><seelaso cref="DropDownUITypeEditorControlBase(Of T)"/>
    Public Class DropDownControlEditor(Of T, TControl As {IEditor(Of T), New, Control})
        Inherits UITypeEditor(Of T)
        ''' <summary>Performs all tasks needed to show drop down</summary>
        ''' <param name="control">Control to be shown</param>
        ''' <param name="provider"><see cref="IServiceProvider"/> that provides environment for drop down</param>
        ''' <param name="context">Context for this editing session</param>
        ''' <returns>True when drop-down was shown, false when it was not shown due to some condition.</returns>
        ''' <remarks>
        ''' This implementation <paramref name="provider"/> is not null, <paramref name="context"/>.<see cref="ITypeDescriptorContext.Instance">Instance</see> is not null and <paramref name="provider"/> provides <see cref="IWindowsFormsEditorService"/>. If those conditions are true, drop down is shown, otherwise not.
        ''' While showing dropdown <paramref name="control"/>.<see cref="IEditor.Service">Service</see> and <paramref name="control"/>.<see cref="IEditor.Context"/> properties are set and handler is attached to <paramref name="control"/>.<see cref="Control.KeyDown">KeyDown</see>.
        ''' Then <see cref="M:Tools.DrawingT.DesignT.DropDownControlEditor`2.ShowDropDown(!1,System.Windows.Forms.Design.IWindowsFormsEditorService)"/> is called. After that handle is detached.
        ''' <para>Note for inheritors: Inheritor should ensure that each instance of <typeparamref name="TControl"/> is being shown only once at same time.</para>
        ''' </remarks>
        Protected Overridable Function ShowDropDown(ByVal control As TControl, ByVal provider As System.IServiceProvider, ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
            If ((Not provider Is Nothing) AndAlso (Not context.Instance Is Nothing)) Then
                Dim edSvc As IWindowsFormsEditorService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
                If edSvc IsNot Nothing Then
                    control.Service = edSvc
                    control.Context = context
                    control.Result = True
                    ShowDropDown(control, edSvc)
                    Return True
                End If
            End If
            Return False
        End Function
        ''' <summary>Shows drop-down editor for <see cref="IWindowsFormsEditorService"/></summary>
        ''' <param name="Control">Control to be shown</param>
        ''' <param name="edSvc"><see cref="IWindowsFormsEditorService"/> that provides environment for current editing session.</param>
        ''' <remarks>This method is called by overloaded <see cref="M:Tools.DrawingT.DesignT.DesignT.DropDownControlEditor`2.ShowDropDown(!1,System.IServiceProviderSystem.ComponentModel.ITypeDescriptorContext)"/> when all conditions necessary for showin drop-down as WinForms <see cref="Control"/> are fullfilled.
        ''' <para>This implementation calls <paramref name="Control"/>.<see cref="IEditor.OnBeforeShow">OnBeforeShow</see>, shows drop down and calls <paramref name="Control"/>.<see cref="IEditor.OnClosed">OnClosed</see>.</para>
        ''' <para>Note for inheritors: Inheritor should ensure that each instance of <typeparamref name="TControl"/> is being shown only once at same time.</para>
        ''' </remarks>
        Protected Overridable Sub ShowDropdown(ByVal Control As TControl, ByVal edSvc As IWindowsFormsEditorService)
            Control.OnBeforeShow()
            edSvc.DropDownControl(Control)
            Control.OnClosed()
        End Sub
        ''' <summary>Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.</summary>
        ''' <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
        ''' <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
        ''' <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services. </param>
        ''' <param name="value">The object to edit. </param>
        ''' <remarks>This implementation creates instance if <typeparamref name="TControl"/> amd calls <see cref="M:Tools.DrawingT.DesignT.DropDownControlEditor`2.ShowDropDown(!1,System.Windows.Forms.Design.IWindowsFormsEditorService)"/> for it.
        ''' <para>Note for inheritors: Inheritor should ensure that each instance of <typeparamref name="TControl"/> is being shown only once at same time.</para></remarks>
        Public Overloads Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As T) As T
            Dim control As New TControl With {.Value = value}
            If ShowDropDown(control, provider, context) AndAlso control.Result Then
                Return control.Value
            Else
                Return value
            End If
        End Function

        ''' <summary>Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method.</summary>
        ''' <returns><see cref="UITypeEditorEditStyle.DropDown"/></returns>
        Public NotOverridable Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
            Return UITypeEditorEditStyle.DropDown
        End Function
        ''' <summary>Gets a value indicating whether drop-down editors should be resizable by the user.</summary>
        ''' <returns>This implementation returns false unless <see cref="TControl"/> implements <see cref="IEditorExtended(Of T)"/> - in such case it retuns <see cref="IEditorExtended(Of T).IsDropDownResizable"/>.</returns>
        Public Overrides ReadOnly Property IsDropDownResizable() As Boolean
            Get
                If GetType(IEditorExtended(Of T)).IsAssignableFrom(GetType(TControl)) Then
                    Using ctl As New TControl
                        Return DirectCast(ctl, IEditorExtended(Of T)).IsDropDownResizable
                    End Using
                End If
                Return False
            End Get
        End Property
        ''' <summary>Indicates whether this editor supports painting a representation of an object's value.</summary>
        ''' <returns>This implementation always returns false</returns>
        Public Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
            Return False
        End Function
    End Class
    ''' <summary>Interface for control that implements GUI for <see cref="DropDownControlEditor"/></summary>
    ''' <remarks>This type is usually implemnetd by class derived from <see cref="Control"/> (such as <see cref="DropDownUITypeEditorControlBase(Of T)"/>).
    ''' <para>However <see cref="DropDownControlEditor"/> does not do so, control implementation can be recycled. This means shown, closed and the shown again, closed again and then shown again ...</para></remarks>
    ''' <typeparam name="T">Type of value being edited</typeparam>
    Public Interface IEditor(Of T)
        ''' <summary>Gets or sets edited value</summary>
        Property Value() As T
        ''' <summary>Stores <see cref="IWindowsFormsEditorService"/> valid for current editing session</summary>
        ''' <remarks>This property is set by owner of the control and is valid between calls of <see cref="OnBeforeShow"/> and <see cref="OnClosed"/>.</remarks>
        Property Service() As IWindowsFormsEditorService
        ''' <summary>Stores context of current editing session</summary>
        ''' <remarks>This property is set by owner of the control and is valid between calls of <see cref="OnBeforeShow"/> and <see cref="OnClosed"/>.</remarks>
        Property Context() As ITypeDescriptorContext
        ''' <summary>Stores editing result</summary>
        ''' <returns>True if editing was terminated with success, false if it was canceled</returns>
        ''' <remarks>Implementing control is responsible for setting this property typically to false when Esc is pressed and true when Enter is pressed. Value must be valid after <see cref="OnClosed"/> is called.</remarks>
        Property Result() As Boolean
        ''' <summary>Owner of control informs control that it is about to be shown by calling this methos. It is called just befiore the control is shown.</summary>
        Sub OnBeforeShow()
        ''' <summary>Informs control that it was just hidden by calling this method.</summary>
        ''' <remarks>When implementing editor for reference type that is edited by changin its properties instead of changing its instance. Properties shouldbe changed in this method and onyl if <see cref="Result"/> is true.</remarks>
        Sub OnClosed()
    End Interface
    ''' <summary>Extends <see cref="IEditor(Of T)"/> with additional members to control behavior and appearance</summary>
    ''' <typeparam name="T">Type of value being edited</typeparam>
    Public Interface IEditorExtended(Of T)
        Inherits IEditor(Of T)
        ''' <summary>Gets value indicating if editor drop-down control should be resizable</summary>
        ''' <returns>True if editor drop down control should be resizable, false if not</returns>
        ''' <seelaso cref="UITypeEditor.IsDropDownResizable"/>
        ReadOnly Property IsDropDownResizable() As Boolean
    End Interface
    ''' <summary>Provides base abstract implementation of drop down control <see cref="DropDownControlEditor(Of T, TControl)"/></summary>
    ''' <typeparam name="T">Type of value being edited</typeparam>
    Public MustInherit Class DropDownUITypeEditorControlBase(Of T)
        Inherits UserControl
        Implements IEditor(Of T)
        ''' <summary>Processes a dialog key.</summary>
        ''' <returns>True if the key was processed by the control; otherwise, false. This implementation always returns false. It only track keys <see cref="Keys.Enter"/> and <see cref="Keys.Escape"/> to <see cref="Result"/>.</returns>
        ''' <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
        ''' <remarks>This implementation sets <see cref="Result"/> to false if <paramref name="keyData"/> is <see cref="Keys.Escape"/> (and neither Alt, Ctrl nor Shift is pressed). If <paramref name="keyData"/> is <see cref="Keys.Enter"/> (and neitehr Alt, Ctrl nor Shift is pressed) sets <see cref="Result"/> to true sets result to true and then calls <see cref="UpdateValue"/>.</remarks>
        Protected Overrides Function ProcessDialogKey(ByVal keyData As System.Windows.Forms.Keys) As Boolean
            Dim e As New KeyEventArgs(keyData)
            If e.Control = False AndAlso e.Alt = False AndAlso e.Shift = False AndAlso e.KeyCode = Keys.Escape Then Result = False
            If e.Control = False AndAlso e.Alt = False AndAlso e.Shift = False AndAlso e.KeyCode = Keys.Enter Then Result = True : UpdateValue()
            Return MyBase.ProcessDialogKey(keyData)
        End Function
        ''' <summary>When overriden in derived class updates the <see cref="Value"/> property with value generated by actual state of control</summary>
        ''' <remarks>
        ''' <para>You should override and implement this method in case your control does not update the <see cref="Value"/> property whenever control state changes.
        ''' Consider these two simple controls:
        ''' <list type="number">
        ''' <item>Control is implemented by <see  cref="ListBox"/> and the <see cref="Value"/> property is update whenever <see cref="ListBox.SelectedIndex"/> changes.</item>
        ''' <item>Control is implemented by <see cref="ListBox"/> and the <see cref="Value"/> property is not updated when <see cref="ListBox.SelectedIndex"/> changes. You want to set the <see cref="Value"/> when dropdown closes.</item>
        ''' </list>
        ''' In scenario 1 you have not to implement <see cref="UpdateValue"/>.
        ''' In scenario 2 you should implement <see cref="UpdateValue"/>.
        ''' </para>
        ''' <para>In this implementation this method is called by <see cref="ProcessDialogKey"/> when enter key is pressed (<see cref="Result"/> property is valid and has value true). It is not called when you close drop down manually by calling <see cref="Service">Service</see>.<see cref="IWindowsFormsEditorService.CloseDropDown">CloseDropDown</see>.</para>
        ''' <para>This implementation does nothing.</para>
        ''' </remarks>
        Protected Overridable Sub UpdateValue()
        End Sub
        ''' <summary>Contains value of the <see cref="Context"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Context As ITypeDescriptorContext
        ''' <summary>Stores context of current editing session</summary>
        ''' <remarks>This property is set by owner of the control and is valid between calls of <see cref="OnBeforeShow"/> and <see cref="OnClosed"/>.</remarks>
        Protected Overridable Property Context() As System.ComponentModel.ITypeDescriptorContext Implements IEditor(Of T).Context
            <DebuggerStepThrough()> Get
                Return _Context
            End Get
            <DebuggerStepThrough()> Set(ByVal value As System.ComponentModel.ITypeDescriptorContext)
                _Context = value
            End Set
        End Property
        ''' <summary>Owner of control informs control that it is about to be shown by calling this methos. It is called just befiore the control is shown.</summary>
        ''' <remarks>This implementation does nothing</remarks>
        Protected Overridable Sub OnBeforeShow() Implements IEditor(Of T).OnBeforeShow
        End Sub
        ''' <summary>Informs control that it was just hidden by calling this method.</summary>
        ''' <remarks>When implementing editor for reference type that is edited by changin its properties instead of changing its instance. Properties shouldbe changed in this method and onyl if <see cref="Result"/> is true.<para>This implementation does nothing.</para></remarks>
        Protected Overridable Sub OnClosed() Implements IEditor(Of T).OnClosed
        End Sub
        ''' <summary>Contains value of the <see cref="Result"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Result As Boolean
        ''' <summary>Stores editing result</summary>
        ''' <returns>True if editing was terminated with success, false if it was canceled</returns>
        ''' <remarks>
        ''' Implementing control is responsible for setting this property typically to false when Esc is pressed and true when Enter is pressed. Value must be valid after <see cref="OnClosed"/> is called.
        ''' <see cref="DropDownUITypeEditorControlBase(Of T)"/> provides such behavior by <see cref="ProcessDialogKey"/> function.
        ''' </remarks>
        Public Overridable Property Result() As Boolean Implements IEditor(Of T).Result
            <DebuggerStepThrough()> Get
                Return _Result
            End Get
            <DebuggerStepThrough()> Protected Set(ByVal value As Boolean)
                _Result = value
            End Set
        End Property
        ''' <summary>Countains value of the <see cref="Service"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Service As IWindowsFormsEditorService
        ''' <summary>Stores <see cref="IWindowsFormsEditorService"/> valid for current editing session</summary>
        ''' <remarks>This property is set by owner of the control and is valid between calls of <see cref="OnBeforeShow"/> and <see cref="OnClosed"/>.</remarks>
        Protected Overridable Property Service() As System.Windows.Forms.Design.IWindowsFormsEditorService Implements IEditor(Of T).Service
            <DebuggerStepThrough()> Get
                Return _Service
            End Get
            <DebuggerStepThrough()> Set(ByVal value As System.Windows.Forms.Design.IWindowsFormsEditorService)
                _Service = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Value"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Value As T
        ''' <summary>Gets or sets edited value</summary>
        Public Overridable Property Value() As T Implements IEditor(Of T).Value
            <DebuggerStepThrough()> Get
                Return _Value
            End Get
            <DebuggerStepThrough()> Protected Set(ByVal value As T)
                _Value = value
            End Set
        End Property
    End Class
End Namespace
#End If