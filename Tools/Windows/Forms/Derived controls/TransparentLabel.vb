Imports System.Windows.Forms
Imports System.ComponentModel, System.ComponentModel.Design
Imports System.Drawing, System.Drawing.Design
Imports Tools.ComponentModelT
#If Config <= Release Then
Namespace WindowsT.FormsT
    ''' <summary><see cref="Label"/> which's background *really* transparent</summary>
    ''' <remarks>To make this contro, transparent or semi-transparent se background color to color with alpha chanel</remarks>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(TransparentLabel), LastChange:="05/13/2007")> _
    <Prefix("tlb")> _
    <DefaultProperty("Text"), DefaultEvent("Click")> _
    <DefaultBindingProperty("Text")> _
    <ToolboxBitmap(GetType(TransparentLabel), "TransparentLabel.bmp")> _
    Public Class TransparentLabel : Inherits Label
#Region "CTors"
        ''' <summary>CTor</summary>
        Public Sub New()
            Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            Me.SetStyle(ControlStyles.ResizeRedraw, True)
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, False)
            Me.ResetBackColor()
        End Sub
        ''' <summary>CTor with text</summary>
        ''' <param name="Text">Default text of control</param>
        Public Sub New(ByVal Text As String)
            Me.New()
            Me.Text = Text
        End Sub
        ''' <summary>CTor with back color</summary>
        ''' <param name="BackColor">Default background color</param>
        Public Sub New(ByVal BackColor As Color)
            Me.New()
            Me.BackColor = BackColor
        End Sub
        ''' <summary>CTor with text and back color</summary>
        ''' <param name="BackColor">Default background color</param>
        ''' <param name="Text">Default text of control</param>
        Public Sub New(ByVal BackColor As Color, ByVal Text As String)
            Me.New()
            Me.Text = Text
            Me.BackColor = BackColor
        End Sub
#End Region
#Region "Paint"
        ''' <summary>Paints the background of the control.</summary>
        ''' <param name="pevent">A <see cref="System.Windows.Forms.PaintEventArgs"/> that contains information about the control to paint</param>
        Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)
            If MyBase.BackgroundImage Is Nothing Then
                pevent.Graphics.FillRectangle(New SolidBrush(Me.BackColor), pevent.ClipRectangle)
            Else
                MyBase.OnPaintBackground(pevent)
            End If
        End Sub
        ''' <summary>Gets the required creation parameters when the control handle is created.</summary>
        ''' <returns>A <see cref="System.Windows.Forms.CreateParams"/> that contains the required creation parameters when the handle to the control is created.</returns>
        Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                cp.ExStyle = cp.ExStyle Or &H20
                Return cp
            End Get
        End Property
        ''' <summary>Raisest the <see cref="TextChanged"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            Me.RecreateHandle()
            MyBase.OnTextChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="BackColorChanged"/> event.</summary>
        ''' <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        Protected Overrides Sub OnBackColorChanged(ByVal e As System.EventArgs)
            RecreateHandle()
            MyBase.OnBackColorChanged(e)
        End Sub
#End Region
#Region "Properties"
        ''' <summary>Gets or sets a value indicating whether the control is automatically resized to display its entire contents.</summary>
        ''' <returns>true if the control adjusts its width to closely fit its contents; otherwise, false. The default is false.</returns>
        <DefaultValue(True), EditorBrowsable(EditorBrowsableState.Always)> _
        <LDescription(GetType(WindowsT.FormsT.DerivedControls), "AutoSize_d")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Layout), RefreshProperties(RefreshProperties.All), Localizable(True)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(True)> _
        Public Overrides Property AutoSize() As Boolean
            Get
                Return MyBase.AutoSize
            End Get
            Set(ByVal value As Boolean)
                MyBase.AutoSize = value
            End Set
        End Property
        ''' <summary>Gets or sets the background color for the control.</summary>
        ''' <returns>A <see cref="System.Drawing.Color"/> that represents the background color of the control.</returns>
        <DefaultValue(GetType(Color), "Transparent")> _
        Public Overrides Property BackColor() As System.Drawing.Color
            Get
                Return MyBase.BackColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                MyBase.BackColor = value
            End Set
        End Property
        ''' <summary>Returns value indicatin if <see cref="BackColor"/> differs from its default value</summary>
        ''' <returns>True if <see cref="BackColor"/> is not <see cref="Color.Transparent"/></returns>
        Private Function ShouldSerializeBackColor() As Boolean
            Return Me.BackColor <> Color.Transparent
        End Function
        ''' <summary>Sets <see cref="BackColor"/> to <see cref="Color.Transparent"/></summary>
        Public Overrides Sub ResetBackColor()
            Me.BackColor = Color.Transparent
        End Sub
#End Region
    End Class
End Namespace
#End If