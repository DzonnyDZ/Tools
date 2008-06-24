Imports System.Windows.Forms
Imports System.ComponentModel, System.ComponentModel.Design
Imports System.Drawing, System.Drawing.Design
Imports Tools.ComponentModelT
Imports Tools.WindowsT.FormsT.UtilitiesT
#If Config <= Release Then
Namespace WindowsT.FormsT
    ''' <summary>Implements <see cref="ProgressBar"/> with overlay text</summary>
    <DefaultEvent("ValueChanged"), DefaultProperty("Value")> _
    <DefaultBindingProperty("Value")> _
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(TransparentLabel), LastChange:="05/15/2007")> _
    <Prefix("pwt")> _
    <ToolboxBitmap(GetType(ProgressBarWithText), "ProgressBarWithText.bmp")> _
    Public Class ProgressBarWithText : Inherits ProgressBar
        ''' <summary>Contains value of the <see cref="Label"/> properzy</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private WithEvents _tlb As TransparentLabel
        ''' <summary>gets or sets internal <see cref="TransparentLabel"/> used to display text</summary>
        Protected Property Label() As TransparentLabel
            Get
                If _tlb Is Nothing Then InitLabel()
                Return _tlb
            End Get
            Private Set(ByVal value As TransparentLabel)
                _tlb = value
            End Set
        End Property
        ''' <summary>Initializes the <see cref="Label"/> property</summary>
        Private Sub InitLabel()
            Label = New TransparentLabel
            Me.Controls.Add(Label)
            With Label
                .BringToFront()
                .Text = Me.Text
                .Dock = DockStyle.Fill
                .TextAlign = ContentAlignment.MiddleCenter
                .BackColor = Color.Transparent
                .AutoSize = False
            End With
        End Sub
        ''' <summary>CTor</summary>
        Sub New()
            Me.ForeColor = SystemColors.ControlText
        End Sub
#Region "Property"
        ''' <summary>Contains value of the <see cref="AutoText"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _AutoText As Boolean = True
        ''' <summary>Get or sets value indicating if text automatically displays value of progress bar</summary>
        <Category(CategoryAttributeValues.Behavior), LDescription(GetType(WindowsT.FormsT.DerivedControls),"AutoText_d")> _
        <DefaultValue(True)> _
        Public Property AutoText() As Boolean
            Get
                Return _AutoText
            End Get
            Set(ByVal value As Boolean)
                Dim Changed As Boolean = value <> AutoText
                _AutoText = value
                If Changed Then OnAutoTextChanged(EventArgs.Empty)
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="AutoTextFormat"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _AutoTextFormat As String
        ''' <summary>Format string used to format value when displayed as text of control automatically.</summary>
        ''' <remarks><seealso cref="Integer.ToString(System.String)"/></remarks>
        <Category(CategoryAttributeValues.Appearance), LDescription(GetType(WindowsT.FormsT.DerivedControls),"AutoTextFormat_d")> _
        <Localizable(True)> _
        Public Property AutoTextFormat() As String
            Get
                Return _AutoTextFormat
            End Get
            Set(ByVal value As String)
                Dim Changed As Boolean = value <> AutoTextFormat
                _AutoTextFormat = value
                If Changed Then OnAutoTextFormatChanged(EventArgs.Empty)
            End Set
        End Property
#End Region
        ''' <summary>Processes Windows messages.</summary>
        ''' <param name="m">The Windows <see cref="System.Windows.Forms.Message"/> to process</param>
        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            MyBase.WndProc(m)
            If m.Msg = &H402 Then OnValueChanged(EventArgs.Empty)
        End Sub
#Region "Events"
        ''' <summary>Occurs when <see cref="Value"/> changes</summary>
        <Category(CategoryAttributeValues.PropertyChanged), LDescription(GetType(WindowsT.FormsT.DerivedControls),"ValueChanged_d")> _
        Public Event ValueChanged As EventHandler
        ''' <summary>Ocuurs when <see cref="AutoText"/> changes</summary>
        <Category(CategoryAttributeValues.PropertyChanged), LDescription(GetType(WindowsT.FormsT.DerivedControls),"AutoTextChanged_d")> _
        Public Event AutoTextChanged As EventHandler
        ''' <summary>Ocuurs when <see cref="AutoTextFormat"/> changes</summary>
        <Category(CategoryAttributeValues.PropertyChanged), LDescription(GetType(WindowsT.FormsT.DerivedControls),"AutoTextFormatChanged_d")> _
        Public Event AutoTextFormatChanged As EventHandler
#End Region
#Region "On"
        ''' <summary>Raises the <see cref="ValueChanged"/> event</summary>
        ''' <param name="e">Property data</param>
        Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
            If AutoText Then Text = CreateAutoText()
        End Sub
        ''' <summary>Raises the <see cref="AutoTextChanged"/> event</summary>
        Protected Overridable Sub OnAutoTextChanged(ByVal e As EventArgs)
            Me.Text = CreateAutoText()
            RaiseEvent AutoTextChanged(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="AutoTextFormatChanged"/> event</summary>
        Protected Overridable Sub OnAutoTextFormatChanged(ByVal e As EventArgs)
            Me.Text = CreateAutoText()
            RaiseEvent AutoTextChanged(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="ForeColorChanged"/> event.</summary>
        ''' <param name="e">An <see cref="EventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnForeColorChanged(ByVal e As System.EventArgs)
            Label.ForeColor = ForeColor
            MyBase.OnForeColorChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="TextChanged"/> event.</summary>
        ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            Label.Text = Text
            MyBase.OnTextChanged(e)
        End Sub
#End Region
        ''' <summary>Formats <see cref="Value"/> by <see cref="AutoTextFormat"/></summary>
        Private Function CreateAutoText() As String
            Return Value.ToString(AutoTextFormat)
        End Function
#Region "Only attributes property"
        ''' <summary>Gets or sets text of control.</summary>
        <Bindable(True), Browsable(True), EditorBrowsable(EditorBrowsableState.Always)> _
        Overrides Property Text() As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
                MyBase.Text = value
            End Set
        End Property
        Private Function ShouldSerializeText() As Boolean
            Return Not AutoText AndAlso Text <> ""
        End Function
        Public Overrides Sub ResetText()
            If Not AutoText Then Text = ""
        End Sub
        ''' <summary>Gets or sets the alignment of text in the label.</summary>
        ''' <returns>One of the <see cref="ContentAlignment"/> values. The default is <see cref="ContentAlignment.MiddleCenter"/>.</returns>
        ''' <exception cref="InvalidEnumArgumentException">The value assigned is not one of the <see cref="ContentAlignment"/> values. </exception>
        <Category(CategoryAttributeValues.Appearance)> _
        <DefaultValue(GetType(ContentAlignment), "MiddleCenter")> _
        <LDescription(GetType(WindowsT.FormsT.DerivedControls),"TextAlign_d")> _
        <Localizable(True)> _
        Public Property TextAlign() As ContentAlignment
            Get
                Return Label.TextAlign
            End Get
            Set(ByVal value As ContentAlignment)
                Label.TextAlign = value
            End Set
        End Property
        ''' <summary>Gets or sets the foreground color of the control.</summary>
        ''' <returns>The foreground System.Drawing.Color of the control. The default is the value of the System.Windows.Forms.Control.DefaultForeColor property.</returns>
        <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
        <DefaultValue(GetType(Color), "ControlText")> _
        Public Overrides Property ForeColor() As System.Drawing.Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                MyBase.ForeColor = value
            End Set
        End Property
        ''' <summary>Gets or sets the font of text in the <see cref="ProgressBarWithText"/>.</summary>
        ''' <returns>The <see cref="System.Drawing.Font"/> of the text. The default is the font set by the container</returns>
        <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
        <Localizable(True), EditorBrowsable(EditorBrowsableState.Always)> _
        Public Overrides Property Font() As Font
            Get
                Return MyBase.Font
            End Get
            Set(ByVal value As Font)
                MyBase.Font = value
            End Set
        End Property
        ''' <summary>Gets or sets the background color for the control.</summary>
        ''' <returns>A <see cref="System.Drawing.Color"/> that represents the background color of the control. The default is the value of the <see cref="System.Windows.Forms.Control.DefaultBackColor"/> property.</returns>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property BackColor() As System.Drawing.Color
            Get
                Return MyBase.BackColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                MyBase.BackColor = value
            End Set
        End Property
#End Region
#Region "Label Events"
        Private Sub _tlb_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles _tlb.DragDrop
            OnDragDrop(e)
        End Sub

        Private Sub _tlb_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles _tlb.DragEnter
            OnDragEnter(e)
        End Sub

        Private Sub _tlb_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tlb.DragLeave
            OnDragLeave(e)
        End Sub

        Private Sub _tlb_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles _tlb.DragOver
            OnDragOver(e)
        End Sub

#Region "This never occurs"
        Private Sub _tlb_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tlb.Enter
        End Sub
        Private Sub _tlb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tlb.GotFocus
        End Sub
        Private Sub _tlb_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tlb.Leave
        End Sub
        Private Sub _tlb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tlb.LostFocus
        End Sub
#End Region

        Private Sub _tlb_MouseCaptureChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tlb.MouseCaptureChanged
            OnMouseCaptureChanged(e)
        End Sub

        Private Sub _tlb_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _tlb.MouseClick
            OnMouseClick(New MouseEventArgs(e.Button, e.Clicks, e.X + Label.Left, e.Y + Label.Top, e.Delta))
        End Sub

        Private Sub _tlb_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _tlb.MouseDoubleClick
            OnMouseDoubleClick(New MouseEventArgs(e.Button, e.Clicks, e.X + Label.Left, e.Y + Label.Top, e.Delta))
        End Sub

        Private Sub _tlb_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _tlb.MouseDown
            OnMouseDown(New MouseEventArgs(e.Button, e.Clicks, e.X + Label.Left, e.Y + Label.Top, e.Delta))
        End Sub

        Private Sub _tlb_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tlb.MouseEnter
            OnMouseEnter(e)
        End Sub

        Private Sub _tlb_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tlb.MouseHover
            OnMouseHover(e)
        End Sub

        Private Sub _tlb_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tlb.MouseLeave
            OnMouseLeave(e)
        End Sub

        Private Sub _tlb_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _tlb.MouseMove
            OnMouseMove(New MouseEventArgs(e.Button, e.Clicks, e.X + Label.Left, e.Y + Label.Top, e.Delta))
        End Sub

        Private Sub _tlb_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _tlb.MouseUp
            OnMouseUp(New MouseEventArgs(e.Button, e.Clicks, e.X + Label.Left, e.Y + Label.Top, e.Delta))
        End Sub

        Private Sub _tlb_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles _tlb.MouseWheel
            OnMouseWheel(New MouseEventArgs(e.Button, e.Clicks, e.X + Label.Left, e.Y + Label.Top, e.Delta))
        End Sub

        Private Sub _tlb_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles _tlb.PreviewKeyDown
            OnPreviewKeyDown(e)
        End Sub
        Private Sub _tlb_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles _tlb.KeyDown
            OnKeyDown(e)
        End Sub
        Private Sub _tlb_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles _tlb.KeyUp
            OnKeyUp(e)
        End Sub
        Private Sub _tlb_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles _tlb.KeyPress
            OnKeyPress(e)
        End Sub
        Private Sub _tlb_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _tlb.Click
            OnClick(e)
        End Sub
        Private Sub _tlb_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles _tlb.DoubleClick
            OnDoubleClick(e)
        End Sub
#End Region
    End Class
End Namespace
#End If