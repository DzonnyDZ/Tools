Imports System.Windows.Forms
Imports System.ComponentModel, System.ComponentModel.Design
Imports System.Drawing, System.Drawing.Design
Imports Tools.ComponentModelT
Imports System.Reflection
#If Config <= Beta Then 'Stage:Beta
Namespace WindowsT.FormsT
    ''' <summary><see cref="RichTextBox"/> with transparent background</summary>
    ''' <remarks>This control is 100% transaprent and cannot have any other than transparent <see cref="TransparentTextBox.BackColor"/>. To make it semi-transparent, put it onto semitransparent panel.</remarks>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(TransparentTextBox), LastChMMDDYYYY:="05/21/2007")> _
    <StandAloneTool(FirstVerMMDDYYYY:="05/19/2007")> _
    <Prefix("trb")> _
    <DefaultProperty("Text"), DefaultEvent("Click")> _
    <DefaultBindingProperty("Text")> _
    <ToolboxBitmap(GetType(TransparentLabel), "TransparentTextBox.bmp")> _
    Public Class TransparentTextBox : Inherits RichTextBox
#Region "CTors"
        ''' <summary>CTor</summary>
        Public Sub New()
            'Configures current control - I've tryed almost eth and this works the best
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True) 'This reduces some flickering a little
            Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            Me.SetStyle(ControlStyles.ResizeRedraw, True)
            Me.SetStyle(ControlStyles.UserPaint, True)
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
        ''' <summary>Gets the required creation parameters when the control handle is created.</summary>
        ''' <returns>A <see cref="System.Windows.Forms.CreateParams"/> that contains the required creation parameters when the handle to the control is created.</returns>
        Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                cp.ExStyle = cp.ExStyle Or &H20 'Transparent
                Return cp
            End Get
        End Property
        '''' <summary>Raises the <see cref="BackColorChanged"/> event.</summary>
        '''' <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        'Protected Overrides Sub OnBackColorChanged(ByVal e As System.EventArgs)
        '    RecreateHandle()
        '    MyBase.OnBackColorChanged(e)
        'End Sub
        ''' <summary>Determines if <see cref="InvalidateParent"/> is currently on call stack to avoid recursive calls</summary>
        Private InInvalidateParent As Boolean = False
        ''' <summary>Invalidates given parent rectrangle</summary>
        ''' <param name="rect">Rectangle to be invalidated</param>
        Private Sub InvalidateParent(ByVal rect As Rectangle)
            If Me.Parent Is Nothing OrElse InInvalidateParent Then Return
            Dim pr As Rectangle
            Try
                InInvalidateParent = True
                If rect.Width = 0 OrElse rect.Height = 0 Then Exit Sub
                pr = New Rectangle(Me.Left + rect.Left, Me.Top + rect.Top, rect.Width, rect.Height)
                Parent.Invalidate(pr, True)
            Finally
                InInvalidateParent = False
            End Try
        End Sub
        ''' <summary>Processes Windows messages.</summary>
        ''' <param name="m">The Windows System.Windows.Forms.Message to process</param>
        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            Const WM_PAINT As Int32 = &HF
            Const WM_PRINT_CLIENT As Int32 = &H318
            Const WM_KEYDOWN As Int32 = &H100
            Const WM_KEYUP As Int32 = &H101
            Const WM_PASTE As Int32 = &H302
            Const WM_CUT As Int32 = &H300

            Select Case m.Msg
                Case WM_PRINT_CLIENT, WM_PAINT
                    If InInvalidateParent Then Return
                    Me.DefWndProc(m) 'This draws the RichTextBox
            End Select
            Dim KbdProcessOnStack As Boolean = KbdProcess
            Try 'Detect keyboard interaction
                KbdProcess = m.Msg = WM_KEYDOWN OrElse m.Msg = WM_KEYUP OrElse m.Msg = WM_PASTE OrElse m.Msg = WM_CUT
                MyBase.WndProc(m)
            Finally
                If Not KbdProcessOnStack Then KbdProcess = False
            End Try
        End Sub
        ''' <summary>True when <see cref="WndProc"/> with keyboard message is on stack. Used in <see cref="OnTextChanged"/> to decide wheather to invalidate whole control (False) or only starting at current line (True)</summary>
        Private KbdProcess As Boolean = False
        ''' <summary>Raises the <see cref="TextChanged"/> event.</summary>
        ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            If KbdProcess Then 'Text changed by user
                Dim pos As Point = Me.GetPositionFromCharIndex(Me.SelectionStart)
                InvalidateParent(New Rectangle(Me.ClientRectangle.Left, Me.ClientRectangle.Top + pos.Y, Me.ClientRectangle.Width, Me.ClientRectangle.Height - (Me.ClientRectangle.Top + pos.Y)))
            Else 'Text changed programatically
                InvalidateParent(Me.ClientRectangle)
            End If
            MyBase.OnTextChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="HScroll"/> event.</summary>
        ''' <param name="e">An <see cref="EventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnHScroll(ByVal e As System.EventArgs)
            InvalidateParent(Me.ClientRectangle)
            MyBase.OnHScroll(e)
        End Sub
        ''' <summary>Raises the <see cref="VScroll"/> event.</summary>
        ''' <param name="e">An <see cref="EventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnVScroll(ByVal e As System.EventArgs)
            InvalidateParent(Me.ClientRectangle)
            MyBase.OnVScroll(e)
        End Sub
        ''' <summary>Raises the <see cref="SelectionChanged"/> event.</summary>
        ''' <param name="e">An <see cref="EventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnSelectionChanged(ByVal e As System.EventArgs)
            InvalidateParent(Me.ClientRectangle)
            MyBase.OnSelectionChanged(e)
        End Sub
        ''' <summary>Raises the System.Windows.Forms.Control.Move event.</summary>
        ''' <param name="e">An System.EventArgs that contains the event data.</param>
        Protected Overrides Sub OnMove(ByVal e As System.EventArgs)
            RecreateHandle()
            MyBase.OnMove(e)
        End Sub
#End Region
#Region "Properties"
        ''' <summary>Gets or sets the background color for the control.</summary>
        ''' <returns>A <see cref="System.Drawing.Color"/> that represents the background color of the control.</returns>
        <DefaultValue(GetType(Color), "Transparent"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property BackColor() As System.Drawing.Color
            Get
                Return MyBase.BackColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                MyBase.BackColor = value
            End Set
        End Property
        ''' <summary>Sets <see cref="BackColor"/> to <see cref="Color.Transparent"/></summary>
        Public Overrides Sub ResetBackColor()
            Me.BackColor = Color.Transparent
        End Sub
#End Region
    End Class
End Namespace
#End If











