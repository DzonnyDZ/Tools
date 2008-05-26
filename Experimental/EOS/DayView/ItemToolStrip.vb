Imports Tools.CollectionsT.GenericT, Tools, System.ComponentModel
Imports Tools.WindowsT.FormsT.UtilitiesT
Imports System.Drawing.Design, System.Drawing
Imports System.Windows.Forms

Partial Class DayView
    ''' <summary><see cref="ToolStrip"/> o jediném <see cref="ToolStripLabelLabel"/> sloužící pro zobrazení položky <see cref="DayView"/></summary>
    Friend Class ItemToolStrip : Inherits ToolStrip
#Region "CTors"
        ''' <summary>CTor</summary>
        ''' <param name="Text">Text</param>
        ''' <param name="Font">Font <see cref="ToolStripLabelLabel">ToolStripLabelLabelu</see></param>
        Public Sub New(ByVal Text As String, ByVal Font As Font)
            MyBase.New()
            Init(Text, Font)
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()
            MyBase.New()
            Init(MyBase.Text, MyBase.Font)
        End Sub
        ''' <summary>Initializuje instanci</summary>
        ''' <param name="Text">Text</param>
        ''' <param name="Font">Font <see cref="ToolStripLabelLabel">ToolStripLabelLabelu</see></param>
        Private Sub Init(ByVal Text As String, ByVal Font As Font)
            Me.CanOverflow = False
            Me.SetStyle(ControlStyles.Selectable, True)
            Me.SetStyle(ControlStyles.SupportsTransparentBackColor, False)
            Me.Items.Add(New ToolStripLabelLabel(Text))
            Me.BackColor = SystemColors.Control
            Me.TabStop = True
            With Label
                .Font = Font
                .AutoSize = False
                '.Size = ret.ClientSize
                '.Dock = DockStyle.Fill
                SetPos()
                .AutoToolTip = False
                '.Margin = New Padding(0)
                '.Padding = New Padding(0)
                .TextAlign = ContentAlignment.MiddleCenter
                .DisplayStyle = ToolStripItemDisplayStyle.Text
                .Label.AutoEllipsis = True
                .Padding = New Padding(3)
            End With
            Me.Dock = DockStyle.None
        End Sub
#End Region
#Region "Properties"
#Region "Custom"
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="CanMove"/></summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _CanMove As Boolean = True
        ''' <summary>Urèuje jestli uživatel mùže tuto instanci pøesouvat a mìnit jeí velikost</summary>
        <DefaultValue("True"), Category(CategoryAttributeValues.Behavior)> _
        Public Property CanMove() As Boolean
            Get
                Return _CanMove
            End Get
            Set(ByVal value As Boolean)
                _CanMove = value
            End Set
        End Property
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="AcceptsLeftRight"/></summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _AcceptsLeftRight As Boolean = False
        ''' <summary>Indikuje jestli položka akceptuje klávesy <see cref="Keys.Left"/> a <see cref="Keys.Right"/></summary>
        <DefaultValue(False)> _
        <Category(WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Property AcceptsLeftRight() As Boolean
            Get
                Return _AcceptsLeftRight
            End Get
            Set(ByVal value As Boolean)
                _AcceptsLeftRight = value
            End Set
        End Property
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="AcceptsTab"/></summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _AcceptsTab As Boolean = False
        ''' <summary>Indikuje jestli položka akceptuje klávesu <see cref="Keys.Tab"/></summary>
        <DefaultValue(False)> _
        <Category(WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Property AcceptsTab() As Boolean
            Get
                Return _AcceptsTab
            End Get
            Set(ByVal value As Boolean)
                _AcceptsTab = value
            End Set
        End Property
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Tag2"/></summary>
        Private _Tag2 As Object
        ''' <summary>Druhý <see cref="Tag"/> ;-)</summary>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property Tag2() As Object
            <DebuggerStepThrough()> Get
                Return _Tag2
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Object)
                _Tag2 = value
            End Set
        End Property
#End Region
#Region "Overrides/Shadows/Backed"
        ''' <summary>Zobrazený text</summary>
        Public Overrides Property Text() As String
            Get
                Return Label.Text
            End Get
            Set(ByVal value As String)
                Label.Text = value
                OnTextChanged(EventArgs.Empty)
            End Set
        End Property
        ''' <summary>Font labelu</summary>
        Public Property LabelFont() As Font
            Get
                Return Label.Font
            End Get
            Set(ByVal value As Font)
                Label.Font = value
            End Set
        End Property
        ''' <summary>Barva pozadí prvku</summary>
        ''' <remarks><see cref="Color.Transparent"/> není podporována</remarks>
        <DefaultValue(GetType(Color), "Control")> _
        Public Shadows Property BackColor() As System.Drawing.Color
            Get
                Return MyBase.BackColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                MyBase.BackColor = value
            End Set
        End Property
        ''' <summary>Resetuje barvu pozadí na výchozí hodnotu</summary>
        Public Overrides Sub ResetBackColor()
            Me.BackColor = SystemColors.Control
            MyBase.ResetBackColor()
        End Sub
        ''' <summary>Urèuje jestli barva pozadí má výchozí hodnotu</summary>
        Protected Overridable Function ShouldSerializeBackColor() As Boolean
            Return Me.BackColor <> SystemColors.Control
        End Function
        ''' <summary>Gets or sets a value indicating whether the user can give the focus to an item in the System.Windows.Forms.ToolStrip using the TAB key.</summary>
        ''' <returns>true if the user can give the focus to an item in the System.Windows.Forms.ToolStrip using the TAB key; otherwise, false. The default is false.</returns>
        <DefaultValue(True)> _
        <Category(WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Overloads Property TabStop() As Boolean
            Get
                Return MyBase.TabStop
            End Get
            Set(ByVal value As Boolean)
                MyBase.TabStop = value
            End Set
        End Property
        ''' <summary><see cref="ToolStripLabelLabel"/></summary>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property Label() As ToolStripLabelLabel
            Get
                Return Me.Items(0)
            End Get
        End Property
#End Region
#End Region
#Region "Events handlers"
        ''' <summary>Processes a command key.</summary>
        ''' <param name="keyData">One of the <see cref="System.Windows.Forms.Keys"/> values that represents the key to process.</param>
        ''' <param name="m">A <see cref="System.Windows.Forms.Message"/>, passed by reference, that represents the window message to process.</param>
        ''' <returns>true if the character was processed by the control; otherwise, false.</returns>
        ''' <remarks>This override processes:
        ''' <list type="table">
        ''' <listheader><term>Key</term><description>Processed if</description></listheader>
        ''' <item><term><see cref="Keys.Left"/> and <see cref="Keys.Right"/></term><description><see cref="AcceptsLeftRight"/> is True</description></item>
        ''' <item><term><see cref="Keys.Tab"/></term><description><see cref="AcceptsTab"/> is True</description></item>
        ''' </list>
        ''' <para>In all other cases base class's method is called.</para></remarks>
        Protected Overrides Function ProcessCmdKey(ByRef m As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
            Const WM_KEYDOWN As Integer = &H100
            Const WM_SYSKEYDOWN As Integer = &H104
            If m.Msg = WM_KEYDOWN OrElse m.Msg = WM_SYSKEYDOWN Then
                Select Case keyData And &HFF
                    Case Keys.Left, Keys.Right : If AcceptsLeftRight Then
                            ProcessKeyEventArgs(m)
                            Return True
                        End If
                    Case Keys.Tab : If AcceptsTab Then
                            ProcessKeyEventArgs(m)
                            Return True
                        End If
                End Select
                Return MyBase.ProcessCmdKey(m, keyData)
            End If
        End Function
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.Resize"/> event.</summary>
        ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data.</param>
        Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
            SetPos()
            MyBase.OnResize(e)
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.MouseMove"/> event.</summary>
        ''' <param name="mea">A <see cref="System.Windows.Forms.MouseEventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnMouseMove(ByVal mea As System.Windows.Forms.MouseEventArgs)
            Dim pPos As Point = mea.Location + Me.Location + Me.ClientRectangle.Location
            Dim e As CancelMoveEventArgs
            Select Case MoveState
                Case MoveStates.Moving
                    Dim NewLeft As Integer = OldPosition.X + pPos.X - OldMouse.X
                    e = New CancelMoveEventArgs(NewLeft, Me.Top, Me.Width, Me.Height)
                Case MoveStates.LeftResizing
                    Dim NewLeft As Integer = OldPosition.X + pPos.X - OldMouse.X
                    Dim NewWidth As Integer = OldWidth - (pPos.X - OldMouse.X)
                    e = New CancelMoveEventArgs(NewLeft, Me.Top, NewWidth, Me.Height)
                Case MoveStates.RightResizing
                    Dim NewWidth As Integer = OldWidth + pPos.X - OldMouse.X
                    e = New CancelMoveEventArgs(Me.Left, Me.Top, NewWidth, Me.Height)
                Case Else
                    If IsGrip(mea.Location) AndAlso CanMove Then
                        Me.Cursor = Cursors.SizeAll
                    ElseIf (IsLeftResize(mea.Location) OrElse IsRightResize(mea.Location)) AndAlso CanMove Then
                        Me.Cursor = Cursors.SizeWE
                    Else
                        Me.Cursor = Cursors.Default
                    End If
                    MyBase.OnMouseMove(mea)
                    Exit Sub
            End Select
            RaiseEvent BeforeMove(Me, e)
            If Not e.Cancel Then
                Dim OldRect As New Rectangle(Me.Location, Me.Size)
                Me.Location = e.Location
                Me.Size = e.Size
                RaiseEvent AfterMove(Me, New MoveEventArgs(OldRect))
            End If
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.MouseDown"/> or starts moving/resizing of item event.</summary>
        ''' <param name="mea">A <see cref="System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        Protected Overrides Sub OnMouseDown(ByVal mea As System.Windows.Forms.MouseEventArgs)
            If FocusOnMouseDown AndAlso Not Me.Focused Then Me.Select()
            If mea.Button = Windows.Forms.MouseButtons.Left Then
                OldPosition = Me.Location
                OldMouse = mea.Location + Me.Location + Me.ClientRectangle.Location
                OldWidth = Me.Width
            End If
            If CanMove AndAlso mea.Button = Windows.Forms.MouseButtons.Left AndAlso IsGrip(mea.Location) Then
                MoveState = MoveStates.Moving
                Me.Capture = True
                RaiseEvent BeginMove(Me, mea)
            ElseIf CanMove AndAlso mea.Button = Windows.Forms.MouseButtons.Left AndAlso IsLeftResize(mea.Location) Then
                MoveState = MoveStates.LeftResizing
                Me.Capture = True
                RaiseEvent BeginResize(Me, New BeginResizeEventArgs(mea, BeginResizeEventArgs.Directions.Left))
            ElseIf CanMove AndAlso mea.Button = Windows.Forms.MouseButtons.Left AndAlso IsRightResize(mea.Location) Then
                MoveState = MoveStates.RightResizing
                Me.Capture = True
                RaiseEvent BeginResize(Me, New BeginResizeEventArgs(mea, BeginResizeEventArgs.Directions.Right))
            Else
                MyBase.OnMouseDown(mea)
                RaiseEvent MouseDownAll(Me, mea)
            End If
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.MouseUp"/> event.</summary>
        ''' <param name="mea">A <see cref="System.Windows.Forms.MouseEventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnMouseUp(ByVal mea As System.Windows.Forms.MouseEventArgs)
            Dim OldMovestate As MoveStates = MoveState
            MoveState = MoveStates.None
            Me.Capture = False
            Me.Cursor = Cursors.Default
            Select Case OldMovestate
                Case MoveStates.LeftResizing, MoveStates.RightResizing
                    RaiseEvent EndMove(Me, mea)
                Case MoveStates.Moving : RaiseEvent EndMove(Me, mea)
            End Select
            MyBase.OnMouseUp(mea)
            RaiseEvent MouseUpAll(Me, mea)
        End Sub

        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.ForeColorChanged"/> event.</summary>
        ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data.</param>
        Protected Overrides Sub OnForeColorChanged(ByVal e As System.EventArgs)
            Label.ForeColor = Me.ForeColor
            Label.Label.ForeColor = Me.ForeColor
            MyBase.OnForeColorChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.BackColorChanged"/> event.</summary>
        ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data.</param>
        Protected Overrides Sub OnBackColorChanged(ByVal e As System.EventArgs)
            MyBase.Invalidate()
            MyBase.OnBackColorChanged(e)
        End Sub
#End Region
#Region "Generated events"
        ''' <summary>Nastane než uživatel zaène mìnit pozici prvku</summary>
        Public Event BeginMove As EventHandler(Of MouseEventArgs)
        ''' <summary>Nastane než uživatel zaène mìnit velikost prvku</summary>
        Public Event BeginResize As EventHandler(Of BeginResizeEventArgs)
        ''' <summary>Nastake když uživatel skonèí se zmìnami pozice prvku</summary>
        Public Event EndMove As EventHandler
        ''' <summary>Nastane když uživatel skonèí se zmìnami velikosti prvku</summary>
        Public Event EndResize As EventHandler
        ''' <summary>Nastane pøed tím než dojde ke zmìnnì pozice nebo velikosti prvku uživatelem</summary>
        ''' <remarks>
        ''' Událost je možno stornovat nebo zmìnit veliksot a pozici prvku.
        ''' Nastává mezi dvojicí událostí <see cref="BeginMove"/> a <see cref="EndMove"/> resp. <see cref="BeginResize"/> a <see cref="EndResize"/>
        ''' </remarks>
        Public Event BeforeMove As EventHandler(Of CancelMoveEventArgs)
        ''' <summary>Nastane po té co uživatel zmìní pozici nebo velikost prvku</summary>
        ''' <remarks>Nastává mezi dvojicí událostí <see cref="BeginMove"/> a <see cref="EndMove"/> resp. <see cref="BeginResize"/> a <see cref="EndResize"/></remarks>
        Public Event AfterMove As EventHandler(Of MoveEventArgs)
#End Region
#Region "Graphic"
        ''' <summary>Zjistí jestli se daná pozice nachází nad gripem</summary>
        ''' <param name="Location">Pozice k ovìøení</param>
        Private Function IsGrip(ByVal Location As Point) As Boolean
            Return Me.GripRectangle.Contains(Location)
        End Function
        ''' <summary>Zjistí jestli se daná pozice nachází nad levým okrajem</summary>
        ''' <param name="Location">Pozice k ovìøení</param>
        Private Function IsLeftResize(ByVal Location As Point) As Boolean
            Dim ret As Boolean = Location.X <= Me.Padding.Left
            If Me.GripDisplayStyle = ToolStripGripDisplayStyle.Horizontal Then
                ret = ret AndAlso (Location.Y < Me.GripRectangle.Top OrElse Location.Y > Me.GripRectangle.Bottom)
            End If
            Return ret
        End Function
        ''' <summary>Zjistí jestli se daná pozice nachází nad pravým okrajem</summary>
        ''' <param name="Location">Pozice k ovìøení</param>
        Private Function IsRightResize(ByVal Location As Point) As Boolean
            Dim ret As Boolean = Location.X >= Me.ClientSize.Width - Me.Padding.Right
            If Me.GripDisplayStyle = ToolStripGripDisplayStyle.Horizontal Then
                ret = ret AndAlso (Location.Y < Me.GripRectangle.Top OrElse Location.Y > Me.GripRectangle.Bottom)
            End If
            Return ret
        End Function
        ''' <summary>Nastaví pozici a rozmìry labelu</summary>
        Private Sub SetPos()
            Dim Left, Top, Bottom, Right As Integer
            'POZOR: Right a Bottom jsou vzdálenosti od prava a odspoda, ne jako v Rectangleu!
            Dim GrR As Rectangle = Me.GripRectangle
            If Me.GripDisplayStyle = ToolStripGripDisplayStyle.Horizontal Then  'Na šíøku nahoøe
                Left = Me.Padding.Left
                Right = Me.Padding.Right
                Bottom = Me.Padding.Bottom
                Top = GrR.Height + Me.GripMargin.Bottom
            Else 'Na vejšku
                If Me.RightToLeft Then 'Vpravo
                    Left = Me.Padding.Left
                    Top = Me.Padding.Top
                    Bottom = Me.Padding.Bottom
                    Right = GrR.Left - Me.GripMargin.Left
                Else 'Vlevo
                    Left = GrR.Right + Me.GripMargin.Right
                    Top = Me.Padding.Top
                    Bottom = Me.Padding.Bottom
                    Right = Me.Padding.Right
                End If
            End If
            With Label
                .Label.Left = Left
                .Label.Top = Top
                .Label.Width = Me.ClientSize.Width - Left - Right
                .Label.Height = Me.ClientSize.Height - Top - Bottom
                .Width = .Label.Width
                .Height = .Label.Height
            End With
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.Paint"/> event for the <see cref="System.Windows.Forms.ToolStrip"/> background.</summary>
        ''' <param name="e">A <see cref="System.Windows.Forms.PaintEventArgs"/> that contains information about the control to paint.</param>
        Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
            Dim LGB As New Drawing2D.LinearGradientBrush(Me.ClientRectangle, Color.White, Color.Black, Drawing2D.LinearGradientMode.Vertical)
            Dim CB As New Drawing2D.ColorBlend(4)
            CB.Colors = New Color() {Color.White, Me.BackColor, Me.BackColor, Color.FromArgb(255, Me.BackColor.R / 1.5, Me.BackColor.G / 1.5, Me.BackColor.B / 1.5)}
            CB.Positions = New Single() {0.0, 0.4, 0.6, 1.0}
            LGB.InterpolationColors = CB
            e.Graphics.FillRectangle(LGB, e.ClipRectangle)

            If Me.BackgroundImage IsNot Nothing Then
                'ElseIf Me.BackColor = Color.Transparent OrElse Me.BackColor = SystemColors.Control Then
                'ToolStripManager.Renderer.DrawToolStripBackground(New ToolStripRenderEventArgs(e.Graphics, Me))
                'MyBase.OnPaintBackground(e)
                Select Case Me.BackgroundImageLayout
                    Case ImageLayout.Center
                        e.Graphics.DrawImage(Me.BackgroundImage, New Point(Me.ClientRectangle.Width / 2 - Me.BackgroundImage.Width / 2, Me.ClientRectangle.Height / 2 - Me.BackgroundImage.Height / 2))
                    Case ImageLayout.None
                        e.Graphics.DrawImage(Me.BackgroundImage, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle, GraphicsUnit.Pixel)
                    Case ImageLayout.Stretch
                        e.Graphics.DrawImage(Me.BackgroundImage, 0, 0, Me.ClientRectangle.Width, Me.ClientRectangle.Height)
                    Case ImageLayout.Tile
                        Dim LocX As Integer = 0, LocY As Integer = 0
                        While LocY < Me.ClientRectangle.Height
                            LocX = 0
                            While LocX < Me.ClientRectangle.Width
                                Dim IRect As New Rectangle(LocX, LocY, Me.BackgroundImage.Width, Me.BackgroundImage.Height)
                                If IRect.IntersectsWith(Me.ClientRectangle) Then
                                    e.Graphics.DrawImage(Me.BackgroundImage, LocX, LocY)
                                End If
                                LocX += Me.BackgroundImage.Width
                            End While
                            LocY += Me.BackgroundImage.Height
                        End While
                    Case ImageLayout.Zoom
                        Dim ZoomSize As Size = ItemToolStrip.ZoomSize(Me.ClientRectangle.Size, Me.BackgroundImage.Size)
                        e.Graphics.DrawImage(Me.BackgroundImage, CInt(Me.ClientRectangle.Width / 2 - ZoomSize.Width / 2), CInt(Me.ClientRectangle.Height / 2 - ZoomSize.Height / 2), ZoomSize.Width, ZoomSize.Height)
                End Select
            End If
        End Sub
        ''' <summary>Spoèítá velikost obrázku pro zoom se zachováním pomìru stran</summary>
        ''' <param name="Img">Rozmìry zdrojového obrázku</param>
        ''' <param name="Th">Maximální rozmìry cílového obrázku</param>
        ''' <returns>Velikost obrázku taková spoètená z <paramref name="Img"/> tak aby byla plocha <paramref name="Th"/> maximálnì využita a nedošlo ke zmìnì pomìru stran obrázku <paramref name="Img"/></returns>
        ''' <remarks>Podporuje zvìtšení i zmìnšení obrázku</remarks>
        Private Shared Function ZoomSize(ByVal Th As Size, ByVal Img As Size) As Size
            Dim NewS As Size
            'If Img.Width <= Th.Width AndAlso Img.Height <= Th.Width Then
            'Return Img
            If Img.Height > Th.Height Then
                NewS = New Size(Th.Width, Img.Height / (Img.Width / Th.Width))
            Else
                NewS = New Size(Img.Width / (Img.Height / Th.Height), Th.Height)
            End If
            If NewS.Width > Th.Width Then
                Return New Size(Th.Width, NewS.Height / (NewS.Width / Th.Width))
            ElseIf NewS.Height > Img.Height Then
                Return New Size(NewS.Width / (NewS.Height / Th.Height), Th.Height)
            End If
            Return NewS
        End Function
#End Region
#Region "Pohyb"
        ''' <summary>Pøed zaèátkem zmìny pozice/velikosti se sem uloží pozice prvku v dobì zaèátku zmìny pozice/velikosti</summary>
        Private OldPosition As Point
        ''' <summary>Pøed zaèátkem zmìny pozice/velikosti se sem uloží šíøka prvku v dobì zaèátku zmìny pozice/velikosti</summary>
        Private OldWidth As Integer
        ''' <summary>Pøed zaèátkem zmìny pozice/velikosti se sem uloží pozice myši v dobì zaèátku zmìny pozice/velikosti (v souøadnicích rodièe)</summary>
        Private OldMouse As Point
        ''' <summary>Aktuální stav zmìny pozice/velikosti prvku</summary>
        Private MoveState As MoveStates = MoveStates.None
        ''' <summary>Aktuální stav zmìny pozice/velikosti prvku</summary>
        <Browsable(False)> _
        Public Property MovingState() As MoveStates
            Get
                Return MoveState
            End Get
            Private Set(ByVal value As MoveStates)
                MoveState = value
            End Set
        End Property
        ''' <summary>Možné stavy zmìny pozice/velikosti prvku</summary>
        Public Enum MoveStates
            ''' <summary>Nic se nedìje</summary>
            None
            ''' <summary>Porbíhá pøesun</summary>
            Moving
            ''' <summary>Probíhá zmìna levého okraje</summary>
            LeftResizing
            ''' <summary>Porobíhá zmìna pravého okraje</summary>
            RightResizing
        End Enum
        ''' <summary>Extarní zahájení pohybu položky</summary>
        ''' <param name="State">Typ pohybu</param>
        ''' <param name="OldPos">Pùvodní pozice</param>
        ''' <param name="OldMouse">Pùvodní pozice myši (v souøadnicích rodièe)</param>
        ''' <param name="OldWidth">Pùvodní šíøka</param>
        Friend Sub SetMoveState(ByVal State As MoveStates, ByVal OldPos As Point, ByVal OldMouse As Point, ByVal OldWidth As Integer)
            Me.MovingState = State
            Me.OldPosition = OldPos
            Me.OldMouse = OldMouse
            Me.OldWidth = OldWidth
            Select Case State
                Case MoveStates.LeftResizing, MoveStates.RightResizing
                    Me.Cursor = Cursors.SizeWE
                Case MoveStates.Moving
                    Me.Cursor = Cursors.SizeAll
                Case Else
                    Me.Cursor = Cursors.Default
            End Select
        End Sub
#End Region
#Region "Focus"
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.Paint"/> event.</summary>
        ''' <param name="e">A <see cref="System.Windows.Forms.PaintEventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            If Me.ContainsFocus AndAlso BorderColor <> Color.Transparent AndAlso BorderWidth <> 0 Then
                Dim Pen As New Pen(Me.BorderColor, Me.BorderWidth)
                Dim LeftRect As New Rectangle(0, BorderWidth, BorderWidth, Me.ClientRectangle.Height - BorderWidth * 2)
                Dim TopRect As New Rectangle(0, 0, Me.ClientRectangle.Width, BorderWidth)
                Dim BottomRect As New Rectangle(0, Me.ClientRectangle.Height - BorderWidth, Me.ClientRectangle.Width, BorderWidth)
                Dim RightRect As New Rectangle(Me.ClientRectangle.Width - BorderWidth, BorderWidth, BorderWidth, Me.ClientRectangle.Height - BorderWidth * 2)

                e.Graphics.FillRectangles(New SolidBrush(BorderColor), New Rectangle() { _
                    Rectangle.Intersect(LeftRect, e.ClipRectangle), _
                    Rectangle.Intersect(TopRect, e.ClipRectangle), _
                    Rectangle.Intersect(BottomRect, e.ClipRectangle), _
                    Rectangle.Intersect(RightRect, e.ClipRectangle)})
            End If
            MyBase.OnPaint(e)
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.GotFocus"/> event.</summary>
        ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnGotFocus(ByVal e As System.EventArgs)
            If BorderColor <> Color.Transparent AndAlso BorderWidth <> 0 Then Me.Invalidate()
            MyBase.OnGotFocus(e)
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.Control.LostFocus"/> event.</summary>
        ''' <param name="e">An <see cref="System.EventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnLostFocus(ByVal e As System.EventArgs)
            If BorderColor <> Color.Transparent AndAlso BorderWidth <> 0 Then Me.Invalidate()
            MyBase.OnLostFocus(e)
        End Sub
        ''' <summary>Obsuluje událost <see cref="ToolStripItem.MouseDown"/> položek tohoto <see cref="ItemToolStrip">ItemToolStripu</see></summary>
        Private Sub Item_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            With DirectCast(sender, ToolStripItem)
                If .CanSelect AndAlso .Selected Then
                    Me.Invalidate()
                ElseIf Not .CanSelect Then
                    If FocusOnMouseDown Then Me.Select()
                End If
                'If TypeOf sender Is ToolStripControlHost Then
                '    With DirectCast(sender, ToolStripControlHost).Control
                '        OnMouseDown(New MouseEventArgs(e.Button, e.Clicks, e.X + .Left, e.Y + .Top, e.Delta))
                '    End With
                'End If
            End With
        End Sub
        ''' <summary>Obsluhuje událost <see cref="Control.KeyDown"/> položek typu <see cref="ToolStripControlHost"/></summary>
        Private Sub Item_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            OnKeyDown(e)
        End Sub
        ''' <summary>Obsluhuje událost <see cref="Control.GotFocus"/> položek typu <see cref="ToolStripControlHost"/></summary>
        Private Sub Item_GotFocus(ByVal sender As Object, ByVal e As EventArgs)
            OnGotFocus(e)
        End Sub
        ''' <summary>Obsluhuje událost <see cref="Control.LostFocus"/> položek typu <see cref="ToolStripControlHost"/></summary>
        Private Sub Item_LostFocus(ByVal sender As Object, ByVal e As EventArgs)
            OnLostFocus(e)
        End Sub
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="FocusOnMouseDown"/></summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _FocusNonMouseDown As Boolean = True
        ''' <summary>Indikuje jestli ovládací prvek obdrží focus když je nad ním stlaèena myš</summary>
        <DefaultValue(True), Category(Tools.WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Property FocusOnMouseDown() As Boolean
            Get
                Return _FocusNonMouseDown
            End Get
            Set(ByVal value As Boolean)
                _FocusNonMouseDown = value
            End Set
        End Property
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="BorderColor"/></summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _BorderColor As Color = Color.FromArgb(128, 255, 255, 0)
        ''' <summary>Barva rámeèku pokud má prvek focus</summary>
        ''' <remarks>Nastavte na <see cref="Color.Transparent"/> pokud nechcete vykreslit rámeèek</remarks>
        <Category(Tools.WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Appearance)> _
        <DefaultValue(GetType(Color), "128,255,255,0")> _
        Public Property BorderColor() As Color
            Get
                Return _BorderColor
            End Get
            Set(ByVal value As Color)
                Dim old As Color = BorderColor
                _BorderColor = value
                If old <> BorderColor Then OnBorderColorChanged(EventArgs.Empty)
            End Set
        End Property
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="BorderWidth"/></summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _BorderWidth As Integer = 3
        ''' <summary>Šíøka rámeèku prvku pokud prvek má focus</summary>
        ''' <remarks>Nastavte na 0 pokud nechcete rámeèek</remarks>
        ''' <exception cref="ArgumentOutOfRangeException">Nastavovaná hodnota je menší než 0</exception>
        <Category(Tools.WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Appearance)> _
        <DefaultValue(3I)> _
        Public Property BorderWidth() As Integer
            Get
                Return _BorderWidth
            End Get
            Set(ByVal value As Integer)
                Dim old As Integer = BorderWidth
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", value, "BorderWidth must be positive or zero")
                _BorderWidth = value
                If old <> BorderWidth Then OnBorderWidthChanged(EventArgs.Empty)
            End Set
        End Property
        ''' <summary>Vyvolává událost <see cref="BorderWidthChanged"/></summary>
        ''' <param name="e">Parametry události</param>
        Protected Overridable Sub OnBorderWidthChanged(ByVal e As EventArgs)
            Me.Invalidate()
            RaiseEvent BorderWidthChanged(Me, e)
        End Sub
        ''' <summary>Vyvolává udáklost <see cref="BorderColorChanged"/></summary>
        ''' <param name="e">Parametry události</param>
        Protected Overridable Sub OnBorderColorChanged(ByVal e As EventArgs)
            Me.Invalidate()
            RaiseEvent BorderColorChanged(Me, e)
        End Sub
        ''' <summary>Nastane po zmìnì vlastnosti <see cref="BorderWidth"/></summary>
        <Category("Property changed")> _
        Public Event BorderWidthChanged As EventHandler
        ''' <summary>Nastane po zmìnì vlastnosti <see cref="BorderColor"/></summary>
        <Category("Property changed")> _
        Public Event BorderColorChanged As EventHandler
#End Region
#Region "Pulled events"
        ''' <summary>Contains value of the <see cref="PullEvents"/> property</summary>
        Private _PullEvents As Boolean = True
        ''' <summary>Gets or sets value indicating if events of items of this <see cref="ItemToolStrip"/> are pulled - re-raised by this <see cref="ItemToolStrip"/>.</summary>
        ''' <returns>True if events are pulled; false othervise</returns>
        ''' <value>New value of the property</value>
        ''' <remarks>
        ''' <para>Pulled events are:</para>
        ''' <para>For all <see cref="ToolStripItem">ToolStripItems</see>:</para>
        ''' <list type="table"><listheader><term>Event being pulled</term><description>Raised as</description></listheader>
        ''' <item><term><see cref="ToolStripItem.Click"/></term><description><see cref="Click"/></description></item>
        ''' <item><term><see cref="ToolStripItem.MouseDown"/></term><description><see cref="MouseDownAll"/></description></item>
        ''' <item><term><see cref="ToolStripItem.MouseEnter"/></term><description><see cref="MouseEnter"/></description></item>
        ''' <item><term><see cref="ToolStripItem.MouseHover"/></term><description><see cref="MouseHover"/></description></item>
        ''' <item><term><see cref="ToolStripItem.MouseLeave"/></term><description><see cref="MouseLeave"/></description></item>
        ''' <item><term><see cref="ToolStripItem.MouseMove"/></term><description><see cref="MouseMove"/></description></item>
        ''' <item><term><see cref="ToolStripItem.MouseUp"/></term><description><see cref="MouseUpAll"/></description></item>
        ''' </list>
        ''' <para>If <see cref="ToolStripItem"/> is <see cref="ToolStripControlHost"/> tha additional events of <see cref="ToolStripControlHost.Control"/> are pulled:</para>
        ''' <list type="table"><listheader><term>Event being pulled</term><description>Raised as</description></listheader>
        ''' <item><term><see cref="Control.KeyDown"/></term><description><see cref="KeyDown"/></description></item>
        ''' <item><term><see cref="Control.KeyPress"/></term><description><see cref="KeyPress"/></description></item>
        ''' <item><term><see cref="Control.KeyUp"/></term><description><see cref="KeyUp"/></description></item>
        ''' <item><term><see cref="Control.MouseClick"/></term><description><see cref="MouseClick"/></description></item>
        ''' <item><term><see cref="Control.MouseDoubleClick"/></term><description><see cref="MouseDoubleClick"/></description></item>
        ''' <item><term><see cref="Control.MouseWheel"/></term><description><see cref="MouseWheel"/></description></item>
        ''' </list>
        ''' Pulled events do not call derived On... methods. Use OnPulled... instead or handle pulled event via event handler.
        ''' </remarks>
        <DefaultValue(True)> _
        <Tools.ComponentModelT.KnownCategory(ComponentModelT.KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if events of items of this ItemToolStrip are pulled - re-raised by this ItemToolStrip.")> _
        Public Property PullEvents() As Boolean
            Get
                Return _PullEvents
            End Get
            Set(ByVal value As Boolean)
                Dim Changed As Boolean = value <> PullEvents
                _PullEvents = value
                If Changed Then OnPullEventsChanged()
            End Set
        End Property
        ''' <summary>Handles change of the <see cref="PullEvents"/> property</summary>
        ''' <remarks>Depending on new value of the property adds or removes handlers of all pulled events</remarks>
        Protected Overridable Sub OnPullEventsChanged()
            For Each Item As ToolStripItem In Me.Items
                If PullEvents Then AddItemHandlers(Item) Else RemoveItemHandlers(Item)
            Next Item
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.ToolStrip.ItemAdded"/> event.</summary>
        ''' <param name="e">A <see cref="System.Windows.Forms.ToolStripItemEventArgs"/> that contains the event data.</param>
        Protected Overrides Sub OnItemAdded(ByVal e As System.Windows.Forms.ToolStripItemEventArgs)
            AddHandler e.Item.MouseDown, AddressOf Item_MouseDown
            If TypeOf e.Item Is ToolStripControlHost Then
                With DirectCast(e.Item, ToolStripControlHost).Control
                    AddHandler .KeyDown, AddressOf Item_KeyDown
                    AddHandler .GotFocus, AddressOf Item_GotFocus
                    AddHandler .LostFocus, AddressOf Item_LostFocus
                End With
            End If
            If PullEvents Then AddItemHandlers(e.Item)
            MyBase.OnItemAdded(e)
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.ToolStrip.ItemRemoved"/> event.</summary>
        ''' <param name="e">A <see cref="System.Windows.Forms.ToolStripItemEventArgs"/> that contains the event data.</param>
        Protected Overrides Sub OnItemRemoved(ByVal e As System.Windows.Forms.ToolStripItemEventArgs)
            RemoveHandler e.Item.MouseDown, AddressOf Item_MouseDown
            If TypeOf e.Item Is ToolStripControlHost Then
                With DirectCast(e.Item, ToolStripControlHost).Control
                    RemoveHandler .KeyDown, AddressOf Item_KeyDown
                    RemoveHandler .GotFocus, AddressOf Item_GotFocus
                    RemoveHandler .LostFocus, AddressOf Item_LostFocus
                End With
            End If
            If PullEvents Then RemoveItemHandlers(e.Item)
            MyBase.OnItemRemoved(e)
        End Sub
        ''' <summary>Adds handlers of all pulled events for given item</summary>
        ''' <param name="Item">Item to add handlers to</param>
        Private Sub AddItemHandlers(ByVal Item As ToolStripItem)
            If TypeOf Item Is ToolStripControlHost Then
                With DirectCast(Item, ToolStripControlHost).Control
                    AddHandler .KeyDown, AddressOf pullItem_KeyDown
                    AddHandler .KeyPress, AddressOf pullItem_KeyPress
                    AddHandler .KeyUp, AddressOf pullItem_KeyUp
                    AddHandler .MouseClick, AddressOf pullItem_MouseClick
                    AddHandler .MouseDoubleClick, AddressOf pullItem_MouseDoubleClick
                    AddHandler .MouseWheel, AddressOf pullItem_MouseWheel
                End With
            End If
            AddHandler Item.Click, AddressOf pullItem_Click
            AddHandler Item.DoubleClick, AddressOf pullItem_DoubleClick
            AddHandler Item.MouseDown, AddressOf pullItem_MouseDown
            AddHandler Item.MouseEnter, AddressOf pullItem_MouseEnter
            AddHandler Item.MouseHover, AddressOf pullItem_MouseHover
            AddHandler Item.MouseLeave, AddressOf pullItem_MouseLeave
            AddHandler Item.MouseMove, AddressOf pullItem_MouseMove
            AddHandler Item.MouseUp, AddressOf pullItem_MouseUp
        End Sub
        ''' <summary>Removes handlers of all pulled events for given item</summary>
        ''' <param name="Item">Item to remove handlers from</param>
        Private Sub RemoveItemHandlers(ByVal Item As ToolStripItem)
            If TypeOf Item Is ToolStripControlHost Then
                With DirectCast(Item, ToolStripControlHost).Control
                    RemoveHandler .KeyDown, AddressOf pullItem_KeyDown
                    RemoveHandler .KeyPress, AddressOf pullItem_KeyPress
                    RemoveHandler .KeyUp, AddressOf pullItem_KeyUp
                    RemoveHandler .MouseClick, AddressOf pullItem_MouseClick
                    RemoveHandler .MouseDoubleClick, AddressOf pullItem_MouseDoubleClick
                    RemoveHandler .MouseWheel, AddressOf pullItem_MouseWheel
                End With
            End If
            RemoveHandler Item.Click, AddressOf pullItem_Click
            RemoveHandler Item.DoubleClick, AddressOf pullItem_DoubleClick
            RemoveHandler Item.MouseDown, AddressOf pullItem_MouseDown
            RemoveHandler Item.MouseEnter, AddressOf pullItem_MouseEnter
            RemoveHandler Item.MouseHover, AddressOf pullItem_MouseHover
            RemoveHandler Item.MouseLeave, AddressOf pullItem_MouseLeave
            RemoveHandler Item.MouseMove, AddressOf pullItem_MouseMove
            RemoveHandler Item.MouseUp, AddressOf pullItem_MouseUp
        End Sub
#Region "Handlers"
#Region "ToolStripControlHost only"
        ''' <summary>Handles pulled event <see cref="ToolStripControlHost.Control"/>.<see cref="Control.KeyDown">KeyDown</see></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            OnPulledKeyDown(e)
        End Sub
        ''' <summary>Raises pulled event <see cref="KeyDown"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnKeyDown"/> is by-passed</remarks>
        Protected Overridable Sub OnPulledKeyDown(ByVal e As KeyEventArgs)
            MyBase.OnKeyDown(e)
        End Sub
        ''' <summary>Handles pulled event <see cref="ToolStripControlHost.Control"/>.<see cref="Control.KeyPress">KeyPress</see></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
            Me.OnPulledKeyPress(e)
        End Sub
        ''' <summary>Raises pulled event <see cref="KeyPress"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnKeyPress"/> is by-passed</remarks>
        Protected Overridable Sub OnPulledKeyPress(ByVal e As KeyPressEventArgs)
            MyBase.OnKeyPress(e)
        End Sub
        ''' <summary>Handles pulled event <see cref="ToolStripControlHost.Control"/>.<see cref="Control.KeyUp">KeyUp</see></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs)
            Me.OnPulledKeyUp(e)
        End Sub
        ''' <summary>Raises pulled event <see cref="KeyUp"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnKeyUp"/> is by-passed</remarks>
        Protected Overridable Sub OnPulledKeyUp(ByVal e As KeyEventArgs)
            MyBase.OnKeyUp(e)
        End Sub
        ''' <summary>Handles pulled event <see cref="ToolStripControlHost.Control"/>.<see cref="Control.MouseClick">MouseClick</see></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs)
            Me.OnPulledMouseClick(TransformMouseEventArgs(e, DirectCast(sender, Control)))
        End Sub
        ''' <summary>Raises pulled event <see cref="MouseClick"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnMouseClick"/> is by-passed</remarks>
        Protected Overridable Sub OnPulledMouseClick(ByVal e As MouseEventArgs)
            MyBase.OnMouseClick(e)
        End Sub
        ''' <summary>Handles pulled event <see cref="ToolStripControlHost.Control"/>.<see cref="Control.MouseDoubleClick">MouseDoubleClick</see></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs)
            Me.OnPulledMouseDoubleClick(TransformMouseEventArgs(e, DirectCast(sender, Control)))
        End Sub
        ''' <summary>Raises pulled event <see cref="MouseDoubleClick"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnMouseDoubleClick"/> is by-passed</remarks>
        Protected Overridable Sub OnPulledMouseDoubleClick(ByVal e As MouseEventArgs)
            MyBase.OnMouseDoubleClick(e)
        End Sub
        ''' <summary>Handles pulled event <see cref="ToolStripControlHost.Control"/>.<see cref="Control.MouseWheel">MouseWheel</see></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs)
            OnPulledMouseWheel(TransformMouseEventArgs(e, DirectCast(sender, Control)))
        End Sub
        ''' <summary>Raises pulled event <see cref="MouseWheel"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnMouseWheel"/> is by-passed</remarks>
        Protected Overridable Sub OnPulledMouseWheel(ByVal e As MouseEventArgs)
            MyBase.OnMouseWheel(e)
        End Sub
        ''' <summary>Transforms <see cref="MouseEventArgs"/> from <see cref="Control"/> coordinates to coordinates of <see cref="Control"/>'s parent</summary>
        ''' <param name="e"><see cref="MouseEventArgs"/> to transform</param>
        ''' <param name="On"><see cref="Control"/> <paramref name="e"/> was raised on</param>
        ''' <returns>New instance of <see cref="MouseEventArgs"/> equal to <paramref name="e"/> as if it was raised on <paramref name="On"/>'s parent.</returns>
        Private Shared Function TransformMouseEventArgs(ByVal e As MouseEventArgs, ByVal [On] As Control) As MouseEventArgs
            Return New MouseEventArgs(e.Button, e.Clicks, e.X + [On].Left, e.Y + [On].Top, e.Delta)
        End Function
#End Region
        ''' <summary>Transforms <see cref="MouseEventArgs"/> from <see cref="ToolStripItem"/> coordinates to coordinates of <see cref="ToolStripItem"/>'s parent</summary>
        ''' <param name="e"><see cref="MouseEventArgs"/> to transform</param>
        ''' <param name="On"><see cref="ToolStripItem"/> <paramref name="e"/> was raised on</param>
        ''' <returns>New instance of <see cref="MouseEventArgs"/> equal to <paramref name="e"/> as if it was raised on <paramref name="On"/>'s parent (<see cref="ToolStrip"/>).</returns>
        Private Shared Function TransformMouseEventArgs(ByVal e As MouseEventArgs, ByVal [On] As ToolStripItem) As MouseEventArgs
            Return New MouseEventArgs(e.Button, e.Clicks, e.X + [On].Bounds.Left, e.Y + [On].Bounds.Top, e.Delta)
        End Function
        ''' <summary>Handles pulled event <see cref="ToolStripItem.MouseDown"/></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
            Static Raising As Boolean
            If Raising Then Return
            Raising = True
            Try
                OnPulledMouseDown(TransformMouseEventArgs(e, DirectCast(sender, ToolStripItem)))
            Finally
                Raising = False
            End Try
        End Sub
        ''' <summary>Raises pulled event <see cref="MouseDown"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnMouseDown"/> is by-passed.
        ''' Unlike other pulled events <see cref="MouseUp"/> and <see cref="MouseDown"/> are not raised throught standart events of <see cref="ToolStrip"/>, but through special events <see cref="MouseUpAll"/> and <see cref="MouseDownAll"/>.</remarks>
        Protected Overridable Sub OnPulledMouseDown(ByVal e As MouseEventArgs)
            RaiseEvent MouseDownAll(Me, e)
        End Sub
        ''' <summary>Raised after mouse id depressed on <see cref="ItemToolStrip"/>. If pulling of events is enabled <see cref="MouseDownAll"/> is also raised when <see cref="ToolStripItem.MouseDown"/> occures on intem of <see cref="ItemToolStrip"/>.</summary>
        <Tools.ComponentModelT.KnownCategory(ComponentModelT.KnownCategoryAttribute.KnownCategories.Mouse)> _
        <Description("Raised after mouse id depressed on ItemToolStrip. If pulling of events is enabled MouseDownAll is also raised when MouseDown occures on intem of ItemToolStrip.")> _
        Public Event MouseDownAll As MouseEventHandler
        ''' <summary>Handles pulled event <see cref="ToolStripItem.MouseUp"/></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
            Static Raising As Boolean
            If Raising Then Return
            Raising = True
            Try
                OnPulledMouseUp(TransformMouseEventArgs(e, DirectCast(sender, ToolStripItem)))
            Finally
                Raising = False
            End Try
        End Sub
        ''' <summary>Raises pulled event <see cref="MouseUpAll"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnMouseUp"/> is by-passed.
        ''' Unlike other pulled events <see cref="MouseUp"/> and <see cref="MouseDown"/> are not raised throught standart events of <see cref="ToolStrip"/>, but through special events <see cref="MouseUpAll"/> and <see cref="MouseDownAll"/>.</remarks>
        Protected Overridable Sub OnPulledMouseUp(ByVal e As MouseEventArgs)
            RaiseEvent MouseUpAll(Me, e)
        End Sub
        ''' <summary>Raised after mouse id released on <see cref="ItemToolStrip"/>. If pulling of events is enabled <see cref="MouseUpAll"/> is also raised when <see cref="ToolStripItem.MouseUp"/> occures on intem of <see cref="ItemToolStrip"/>.</summary>
        <Tools.ComponentModelT.KnownCategory(ComponentModelT.KnownCategoryAttribute.KnownCategories.Mouse)> _
        <Description("Raised after mouse id released on ItemToolStrip. If pulling of events is enabled MouseUpAll is also raised when MouseUp occures on intem of ItemToolStrip.")> _
        Public Event MouseUpAll As MouseEventHandler
        ''' <summary>Handles pulled event <see cref="ToolStripItem.MouseMove"/></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            Static Raising As Boolean
            If Raising Then Return
            Raising = True
            Try
                Me.OnPulledMouseMove(TransformMouseEventArgs(e, DirectCast(sender, ToolStripItem)))
            Finally
                Raising = False
            End Try
        End Sub
        ''' <summary>Raises pulled event <see cref="MouseMove"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnMouseMove"/> is by-passed</remarks>
        Protected Overridable Sub OnPulledMouseMove(ByVal e As MouseEventArgs)
            MyBase.OnMouseMove(e)
        End Sub
        ''' <summary>Handles pulled event <see cref="ToolStripItem.Click"/></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_Click(ByVal sender As Object, ByVal e As EventArgs)
            Static Raising As Boolean
            If Raising Then Return
            Raising = True
            Try
                Me.OnPulledClick(e)
            Finally
                Raising = False
            End Try
        End Sub
        ''' <summary>Raises pulled event <see cref="Click"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnClick"/> is by-passed</remarks>
        Protected Overridable Sub OnPulledClick(ByVal e As EventArgs)
            MyBase.OnClick(e)
        End Sub
        ''' <summary>Handles pulled event <see cref="ToolStripItem.DoubleClick"/></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
            Static Raising As Boolean
            Static L As BlackHoleQueue(Of EventArgs)
            If L Is Nothing Then L = New BlackHoleQueue(Of EventArgs)(2)
            If L.Contains(e) Then Exit Sub
            L.Enqueue(e)
            If Raising Then Return
            Raising = True
            Try
                Me.OnPulledDoubleClick(e)
            Finally
                Raising = False
            End Try
        End Sub
        ''' <summary>Raises pulled event <see cref="DoubleCLick"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnDoubleClick"/> is by-passed</remarks>
        Protected Overridable Sub OnPulledDoubleClick(ByVal e As EventArgs)
            MyBase.OnDoubleClick(e)
        End Sub
        ''' <summary>Handles pulled event <see cref="ToolStripItem.MouseEnter"/></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
            Static Raising As Boolean
            If Raising Then Return
            Raising = True
            Try
                Me.OnPulledMouseEnter(e)
            Finally
                Raising = False
            End Try
        End Sub
        ''' <summary>Raises pulled event <see cref="MouseEnter"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnMouseEnter"/> is by-passed</remarks>
        Protected Overridable Sub OnPulledMouseEnter(ByVal e As EventArgs)
            MyBase.OnMouseEnter(e)
        End Sub
        ''' <summary>Handles pulled event <see cref="ToolStripItem.MouseHover"/></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_MouseHover(ByVal sender As Object, ByVal e As EventArgs)
            Static Raising As Boolean
            If Raising Then Return
            Raising = True
            Try
                Me.OnPulledMouseHover(e)
            Finally
                Raising = False
            End Try
        End Sub
        ''' <summary>Raises pulled event <see cref="MouseHover"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnMouseHover"/> is by-passed</remarks>
        Protected Overridable Sub OnPulledMouseHover(ByVal e As EventArgs)
            MyBase.OnMouseHover(e)
        End Sub
        ''' <summary>Handles pulled event <see cref="ToolStripItem.MouseLeave"/></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event parameters</param>
        Private Sub pullItem_MouseLeave(ByVal sender As Object, ByVal e As EventArgs)
            Static Raising As Boolean
            If Raising Then Return
            Raising = True
            Try
                Me.OnPulledMouseLeave(e)
            Finally
                Raising = False
            End Try
        End Sub
        ''' <summary>Raises pulled event <see cref="MouseLeave"/></summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Handling of event defined in derived <see cref="OnMouseLeave"/> is by-passed</remarks>
        Protected Overridable Sub OnPulledMouseLeave(ByVal e As EventArgs)
            MyBase.OnMouseLeave(e)
        End Sub
#End Region
#End Region
    End Class
End Class
