Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace GUI
    ''' <summary>Represents <see cref="ProgressBar"/> whichs progress indicator can leave progressbar's bounds when <see cref="OverflowProgressBar.Value"/> is greater than <see cref="ProgressBar.Maximum"/></summary>
    <ToolboxBitmap(GetType(ProgressBar))> _
    Public Class OverflowProgressBar : Inherits ProgressBar
        ''' <summary><see cref="Form"/> which realizes overflow of the progress indicator</summary>
        Protected ReadOnly ProgressForm As Form
        ''' <summary>Gets new instance of <see cref="Form"/> for <see cref="ProgressForm"/></summary>
        ''' <returns><see cref="Form"/> that will serve as progress indicator overflow</returns>
        ''' <remarks>
        ''' <see cref="OverflowProgressBar"/> call this method once in CTor in order to obtain form that will shouw progress overflow.
        ''' <see cref="ProgressBar"/> only sets size and position of the form. Its up to form implementation to draw its content.
        ''' <para>When overriden, override <see cref="MinimalStep"/> as well</para>
        ''' </remarks>
        Protected Overridable Function GetProgressForm() As Form
            Return New ProgressBarForm
        End Function
        ''' <summary>Gets minimal step of graphical implementation of ProgressBar on <see cref="ProgressForm"/></summary>
        ''' <exception cref="InvalidOperationException"><see cref="GetProgressForm"/> was overriden and <see cref="MinimalStep"/> was not overriden.</exception>
        ''' <remarks>When <see cref="GetProgressForm"/> is overriden <see cref="MinimalStep"/> must be overriden as well.
        ''' <para>Used only when <see cref="Style"/> is <see cref="ProgressBarStyle.Blocks"/>.</para></remarks>
        Protected Overridable ReadOnly Property MinimalStep() As Integer
            Get
                If Not TypeOf ProgressForm Is ProgressBarForm Then Throw New InvalidOperationException("ProgressForm is of unexpected type. When GetProgressForm is overloaded, minimal step must be overloaded as well.")
                Return DirectCast(ProgressForm, ProgressBarForm).MinimalStep
            End Get
        End Property
        ''' <summary>CTor</summary>
        Public Sub New()
            ProgressForm = GetProgressForm()
            BaseValue = MyBase.Value
        End Sub
        ''' <summary>Contains value of the <see cref="Value"/> property</summary>
        Private _Value%
        ''' <summary>Keeps current value of <see cref="ProgressBar.Value"/> base class property in order to detect changes</summary>
        Private BaseValue As Integer
        ''' <summary>Raised when the <see cref="Value"/> property changes</summary>
        Public Event ValueChanged As EventHandler
        ''' <summary>Raises the <see cref="ValueChanged"/> property</summary>
        Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
            RaiseEvent ValueChanged(Me, e)
        End Sub
        '''' <summary>Is set to true when value of the <see cref="ProgressBar.Value"/> base class property is being set to <see cref="Maximum"/> because of <see cref="Value"/> property is being set to value greater than <see cref="Maximum"/>; otherwise false</summary>
        'Private SettingMaximum As Boolean
        ''' <summary>Gets or sets the current position of the progress bar.</summary>
        ''' <returns>The position within the range of the progress bar. The default is 0.</returns>
        ''' <exception cref="System.ArgumentException">The value specified is less than the value of the <see cref="System.Windows.Forms.ProgressBar.Minimum"/> property.</exception>
        <Category("Behavior"), DefaultValue(0), Description("Gets or sets the current position of the progress bar."), Bindable(True)> _
        Public Shadows Property Value%()
            <DebuggerStepThrough()> Get
                Return _Value
            End Get
            Set(ByVal value%)
                If value = Me.Value Then Exit Property
                Dim old% = _Value
                If value <= Maximum Then
                    ProgressForm.Hide()
                    BaseValue = value
                    Try
                        MyBase.Value = value
                    Catch
                        BaseValue = MyBase.Value
                        Throw
                    End Try
                End If
                _Value = value
                If value > Maximum Then
                    'SettingMaximum = True
                    'Try
                    BaseValue = Maximum
                    MyBase.Value = Maximum
                    'Finally
                    '    SettingMaximum = False
                    'End Try
                    ShowProgressForm()
                End If
                If old <> value Then OnValueChanged(New EventArgs)
            End Set
        End Property
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.VisibleChanged"/> event.</summary>
        ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data.</param>
        Protected Overrides Sub OnVisibleChanged(ByVal e As System.EventArgs)
            ShowProgressForm()
            MyBase.OnVisibleChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.Resize"/> event.</summary>
        ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            ShowProgressForm()
            MyBase.OnResize(e)
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.Move"/> event.</summary>
        ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnMove(ByVal e As System.EventArgs)
            ShowProgressForm()
            MyBase.OnMove(e)
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.ParentChanged"/> event.</summary>
        ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data.</param>
        Protected Overrides Sub OnParentChanged(ByVal e As System.EventArgs)
            BuildParentTree()
            PositionProgressBar()
            MyBase.OnParentChanged(e)
        End Sub
        ''' <summary>Refreshes list of parents of this control, the <see cref="ParentTree"/> field, removes handlers form old parents and attaches hanlers to new parents.</summary>
        Private Sub BuildParentTree()
            For Each item As Control In ParentTree
                RemoveHandler item.Resize, AddressOf Parent_Resize
                RemoveHandler item.Move, AddressOf Parent_Resize
                RemoveHandler item.ParentChanged, AddressOf Parent_Changed
            Next item
            Dim parent As Control = Me.Parent
            ParentTree.Clear()
            While parent IsNot Nothing
                ParentTree.Add(parent)
                AddHandler parent.Resize, AddressOf Parent_Resize
                AddHandler parent.Move, AddressOf Parent_Resize
                AddHandler parent.ParentChanged, AddressOf Parent_Changed
                parent = parent.Parent
            End While
        End Sub
        ''' <summary>Handles <see cref="Control.Resize"/> and <see cref="Control.Move"/> events of any control in <see cref="ParentTree"/></summary>
        Private Sub Parent_Resize(ByVal sender As Object, ByVal e As EventArgs)
            PositionProgressBar()
        End Sub
        ''' <summary>Handles the <see cref="Control.ParentChanged"/> of any control in <see cref="ParentTree"/></summary>
        Private Sub Parent_Changed(ByVal sender As Object, ByVal e As EventArgs)
            BuildParentTree()
            PositionProgressBar()
        End Sub
        ''' <summary>Contains <see cref="OverflowProgressBar.Parent"/> of this control, <see cref="Control.Parent">Parent</see> or <see cref="OverflowProgressBar.Parent"/> ... (ordered from current instance to form)</summary>
        Private ParentTree As New List(Of Control)

        ''' <summary>Decides wheather to show <see cref="ProgressForm"/> or not, shows or hides it and in case of showing calls <see cref="PositionProgressBar"/> and <see cref="Form.Invalidate">invalidates</see> the form.</summary>
        Protected Overridable Sub ShowProgressForm()
            If DesignMode Then Exit Sub
            If Me.Visible = False OrElse Me.Style = ProgressBarStyle.Marquee OrElse Value <= Maximum Then ProgressForm.Hide() : Exit Sub
            PositionProgressBar()
            If Not ProgressForm.Visible Then ProgressForm.Show(Me) : PositionProgressBar()
            ProgressForm.Invalidate()
        End Sub
        ''' <summary>Sets <see cref="Form.Location"/> and <see cref="Form.Size"/> of <see cref="ProgressForm"/> in order to match current <see cref="Value"/> and current <see cref="OverflowProgressBar.Location"/> and <see cref="OverflowProgressBar.Size"/> of this instance. <see cref="Form.Invalidate">Invalidates</see> the form.</summary>
        Protected Overridable Sub PositionProgressBar()
            'Dim ThemeInfo As New UxThemeTool.CThemeInfo
            'Dim Chunk As UxThemeTool.CThemePart = ThemeInfo.Item("PROGRESS").Parts("CHUNK")
            'Dim Theme As UxThemeTool.CUxTheme = Chunk.UxTheme
            'Dim Margins As UxThemeTool.SMargins = Theme.GetMargins(Graphics.FromHwnd(ProgressForm.Handle), Chunk.ThemePartId, 0, 0, New Rectangle(0, 0, 25, 25))

            Dim RequiredWidth% = (Value - Maximum) / (Maximum - Minimum) * Me.ClientSize.Width
            If Style = ProgressBarStyle.Blocks Then
                ProgressForm.Width = (RequiredWidth \ MinimalStep) * MinimalStep
            Else
                ProgressForm.Width = RequiredWidth
            End If
            ProgressForm.Height = Me.ClientSize.Height
            ProgressForm.Location = Me.PointToScreen(New Point(Me.ClientSize.Width, 0))
            ProgressForm.Invalidate()
        End Sub
        ''' <summary>Processes Windows messages.</summary>
        ''' <param name="m">The Windows <see cref="System.Windows.Forms.Message"/> to process.</param>
        ''' <remarks>Handles messages:
        ''' <list type="table">
        ''' <listheader><term>Message</term><description>Action taken</description></listheader>
        ''' <item><term>1030</term><description><see cref="Minimum"/> or <see cref="Maximum"/> changed - calls <see cref="ShowProgressForm"/></description></item>
        ''' <item><term>1026</term><description><see cref="ProgressBar.Value"/> base class property changed - tracks changes, calls <see cref="OnValueChanged"/></description></item>
        ''' </list>
        ''' </remarks>
        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            MyBase.WndProc(m)
            If m.Msg = 1030 Then ShowProgressForm()
            If m.Msg = 1026 Then
                If BaseValue <> MyBase.Value Then 'AndAlso Not SettingMaximum Then
                    Me.Value = MyBase.Value
                    BaseValue = MyBase.Value
                    OnValueChanged(New MessageEventArgs(m))
                End If
            End If
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.StyleChanged"/> event.</summary>
        ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data.</param>
        Protected Overrides Sub OnStyleChanged(ByVal e As System.EventArgs)
            ShowProgressForm()
            MyBase.OnStyleChanged(e)
        End Sub
        ''' <summary>Represents <see cref="ProgressForm"/></summary>
        Private Class ProgressBarForm : Inherits Form
            ''' <summary>Processes Windows messages.</summary>
            ''' <param name="m">The Windows System.Windows.Forms.Message to process.</param>
            Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
                MyBase.WndProc(m)
                Select Case m.Msg
                    Case &H31A, &H15 'WM_THEMECHANGED, WM_SYSCOLORCHANGE
                        Brush = Nothing
                End Select
            End Sub
            ''' <summary>CTor</summary>
            Public Sub New()
                'If VisualStyles.VisualStyleRenderer.IsSupported Then
                '    Dim el As VisualStyles.VisualStyleElement = VisualStyles.VisualStyleElement.ProgressBar.Chunk.Normal
                '    If VisualStyles.VisualStyleRenderer.IsElementDefined(el) Then
                '        vsr = New VisualStyles.VisualStyleRenderer(el)
                '    End If
                'End If
                Me.FormBorderStyle = FormBorderStyle.None
                Me.ShowInTaskbar = False
            End Sub
            ''' <summary>Minimal step supported by progressbar graphic</summary>
            Public ReadOnly Property MinimalStep() As Integer
                Get
                    'If vsr IsNot Nothing Then
                    '    Return vsr.GetPartSize(Me.CreateGraphics, VisualStyles.ThemeSizeType.Minimum).Width
                    'Else
                    If ProgressBarRenderer.IsSupported Then
                        Return ProgressBarRenderer.ChunkThickness + ProgressBarRenderer.ChunkSpaceThickness
                    Else
                        Return Chunk + Space
                    End If
                    'End If
                End Get
            End Property
            'Private vsr As VisualStyles.VisualStyleRenderer
            ''' <summary>Paints the background of the control.</summary>
            ''' <param name="e">A <see cref="System.Windows.Forms.PaintEventArgs"/> that contains information about the control to paint.</param>
            Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
                'Dim width As Integer = vsr.GetPartSize(e.Graphics, Me.ClientRectangle, VisualStyles.ThemeSizeType.Draw).Width
                'For Left As Integer = 0 To Me.ClientSize.Width Step width
                '    vsr.DrawBackground(e.Graphics, New Rectangle(Left, 0, width, Me.ClientSize.Height))
                'Next Left
                'vsr.DrawBackground(e.Graphics, Me.ClientRectangle)
                If ProgressBarRenderer.IsSupported Then
                    ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, e.ClipRectangle)
                Else
                    DrawProgressBar(e.Graphics, e.ClipRectangle)
                End If
            End Sub
            ''' <summary>Progressbar chunk width when themes are not applied</summary>
            Private Const Chunk% = 14
            ''' <summary>Width of space between progressbar chunks where themes are not applied</summary>
            Private Const Space% = 2
            ''' <summary>Brush to draw progressbar with when themes are not applied</summary>
            Private Brush As TextureBrush
            ''' <summary>Drwas progressbar when themes are not applied</summary>
            ''' <param name="g"><see cref="Graphics"/> to draw onto</param>
            ''' <param name="Bounds">Bounds to be refreshed</param>
            Private Sub DrawProgressBar(ByVal g As Graphics, ByVal Bounds As Rectangle)
                If Brush Is Nothing Then
                    Dim bmp As New Bitmap(Chunk + Space, 1)
                    Dim bg As Graphics = Graphics.FromImage(bmp)
                    bg.FillRectangle(New SolidBrush(SystemColors.Highlight), 0, 0, Chunk, 1)
                    bg.FillRectangle(New SolidBrush(SystemColors.Control), Chunk + 1, 0, Space, 1)
                    bg.Flush()
                    Brush = New TextureBrush(bmp)
                End If


                If g.IsVisibleClipEmpty Then Return
                If Bounds.IsEmpty Then Return
                'g.FillRectangle(New SolidBrush(SystemColors.Control), Bounds)

                'For Left As Integer = _
                '    g.RenderingOrigin.X + (Bounds.Left - g.RenderingOrigin.X) \ (Chunk + Space) * (Chunk + Space) _
                '    To Bounds.Right - 1 Step Chunk + Space
                '    Dim rect As New Rectangle(Math.Max(Left, g.ClipBounds.Left), Bounds.Top, Math.Min(Left + Space, Bounds.Right), Bounds.Bottom)
                '    g.FillRectangle(New SolidBrush(SystemColors.Highlight), rect)
                'Next Left
                g.FillRectangle(Brush, Bounds)
            End Sub

            ''' <summary>Prevents from from getting focus.</summary>
            ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data</param>
            Protected Overrides Sub OnGotFocus(ByVal e As System.EventArgs)
                Me.Owner.Select()
            End Sub

        End Class

    End Class
    ''' <summary>Represents <see cref="EventArgs"/> which carries <see cref="Message"/></summary>
    Public Class MessageEventArgs : Inherits EventArgs
        ''' <summary>CTor</summary>
        ''' <param name="Message">Message to be encapsulated</param>
        Public Sub New(ByVal Message As Message)
            Me._Message = Message
        End Sub
        ''' <summary>Contains value of the <see cref="Message"/> property</summary>
        Private ReadOnly _Message As Message
        ''' <summary><see cref="System.Windows.Forms.Message"/></summary>
        Public ReadOnly Property Message() As Message
            <DebuggerStepThrough()> Get
                Return _Message
            End Get
        End Property
        ''' <summary>Converts <see cref="System.Windows.Forms.Message"/> to <see cref="MessageEventArgs"/></summary>
        ''' <param name="a">A <see cref="System.Windows.Forms.Message"/></param>
        ''' <returns><see cref="MessageEventArgs"/> which encaptulates <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As Message) As MessageEventArgs
            Return New MessageEventArgs(a)
        End Operator
        ''' <summary>Converts <see cref="MessageEventArgs"/> to <see cref="System.Windows.Forms.Message"/></summary>
        ''' <param name="a">A <see cref="MessageEventArgs"/></param>
        ''' <returns><see cref="System.Windows.Forms.Message"/> encapsulated in <paramref name="a"/>.<see cref="Message">Message</see></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
        Public Shared Widening Operator CType(ByVal a As MessageEventArgs) As Message
            If a Is Nothing Then Throw New ArgumentNullException("a")
            Return a.Message
        End Operator
    End Class
End Namespace