Imports Tools.ComponentModelT, System.ComponentModel, Tools
Imports Tools.CollectionsT.GenericT
Imports System.Windows.Forms
Imports System.Drawing

''' <summary>Ovládací prvek skládající se z více textovýcxh polí</summary>
Public Class MultiTextBox
    Inherits MultiControl(Of MultiTextBoxItem)

#Region "Properties"
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="RemoveEmptyOnLeave"/></summary>
    Private _RemoveEmptyOnLeave As Boolean = True
    ''' <summary>Pokud je textové pole prázdné, odstraní jej, když jej uživatel opustí.</summary>
    ''' <remarks>Nesníží však počet polí pod <see cref="MinimumItems"/>.</remarks>
    <DefaultValue(True)> _
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
    <Description("Pokud je textové pole prázdné, odstraní jej, když je uživatel opustí. Nesníží však počet polí pod MinimumItems.")> _
    Public Property RemoveEmptyOnLeave() As Boolean
        <DebuggerStepThrough()> Get
            Return _RemoveEmptyOnLeave
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Boolean)
            _RemoveEmptyOnLeave = value
        End Set
    End Property
#End Region
#Region "Controls"
    Protected Overrides Sub Handlers(ByVal Add As Boolean, ByVal Item As MultiTextBoxItem)
        MyBase.Handlers(Add, Item)
        If Add Then
            AddHandler Item.TextBox.Validated, AddressOf TextBox_Validated
            AddHandler Item.TextBox.TextChanged, AddressOf TextBox_TextChanged
        Else
            RemoveHandler Item.TextBox.Validated, AddressOf TextBox_Validated
            RemoveHandler Item.TextBox.TextChanged, AddressOf TextBox_TextChanged
        End If
    End Sub
#Region "FindItemByControl"
    ''' <summary>Nalezne položku kolekce <see cref="Items"/> podle <see cref="MultiTextBoxItem.TextBox"/></summary>
    ''' <param name="TextBox"><see cref="TextBox"/> k hledání</param>
    ''' <returns>Naelezená položka nebo null</returns>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Overloads Function FindItemByControl(ByVal TextBox As TextBox) As MultiTextBoxItem
        For Each item As MultiTextBoxItem In Items
            If item.TextBox Is TextBox Then Return item
        Next
        Return Nothing
    End Function
#End Region
#End Region
#Region "Event handlers"
    Private Sub TextBox_Validated(ByVal sender As Object, ByVal e As EventArgs)
        Dim TextBox As TextBox = sender
        If RemoveEmptyOnLeave AndAlso TextBox.Text = "" AndAlso Items.Count > MinimumItems Then Items.Remove(FindItemByControl(TextBox))
    End Sub
    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim i% = 0
        For Each item As MultiTextBoxItem In Me.Items
            If item.TextBox Is sender Then
                OnTextBoxTextChanged(New ListWithEvents(Of MultiTextBoxItem).ItemIndexEventArgs(item, i))
                Exit Sub
            End If
            i += 1
        Next
    End Sub
#End Region

#Region "Events"
    ''' <summary>Raises the <see cref="TextBoxTextChanged"/> event</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnTextBoxTextChanged(ByVal e As ListWithEvents(Of MultiTextBoxItem).ItemIndexEventArgs)
        RaiseEvent TextBoxTextChanged(Me, e)
    End Sub
    ''' <summary>Raised when text of nay textbox changes</summary>
    <Description("Vyvolána, když se změní text libovolného textboxu")> _
    <Category("Property Changed")> _
    Public Event TextBoxTextChanged As EventHandler(Of ListWithEvents(Of MultiTextBoxItem).ItemIndexEventArgs)
#End Region

    ''' <summary>RTeprezentuje položku multitextboxu</summary>
    Public Class MultiTextBoxItem : Inherits MultiControlItemWithOwner(Of TextBox, MultiTextBox)
        ''' <summary>CTor</summary>
        Public Sub New()
            MyBase.New()
            With RemoveButton
                .Parent = RemoveButtonForm
                .Visible = True
                .Dock = DockStyle.None
                .Location = New Point(0, 0)
                .Anchor = AnchorStyles.Top Or AnchorStyles.Left
                .Margin = New Padding(0)
            End With
            With TextBox
                AddHandler .MouseEnter, AddressOf TextBox_MouseEnter
                AddHandler .MouseMove, AddressOf TextBox_MouseMove
            End With
        End Sub
        Protected Overrides Sub OnRemoveButtonStateChanged()
            If RemoveButtonStateEffective <> ControlState.Enabled Then
                RemoveButtonForm.Hide()
                TextBox.Capture = False
            End If
        End Sub
#Region "Contrtols"
        ''' <summary>Textbox that represents current instance</summary>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        Public ReadOnly Property TextBox() As TextBox
            <DebuggerStepThrough()> Get
                Return Control
            End Get
        End Property
#End Region
#Region "Properties"
        ''' <summary>Text zadávaný uživatelem</summary>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <Description("Text zadávaný uživatelem")> _
        <DefaultValue(GetType(String), Nothing)> _
        Public Property Text$()
            <DebuggerStepThrough()> Get
                Return TextBox.Text
            End Get
            <DebuggerStepThrough()> Set(ByVal value$)
                TextBox.Text = value
            End Set
        End Property
#End Region
#Region "RemoveForm"
        Private WithEvents RemoveButtonForm As New frmRemoveButtonForm
        Private Const FormOpacity As Double = 0.5
        Private Class frmRemoveButtonForm : Inherits Form
            Public Sub New()
                With Me
                    .FormBorderStyle = FormBorderStyle.None
                    .Padding = New Padding(0)
                    .ShowInTaskbar = False
                    .ShowIcon = False
                    .Opacity = FormOpacity
                End With
            End Sub
            Protected Overrides ReadOnly Property ShowWithoutActivation() As Boolean
                Get
                    Return True
                End Get
            End Property
            Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
                MyBase.OnResize(e)
                If Me.Controls.Count > 0 AndAlso Me.Size <> Me.Controls(0).Size Then Me.Size = Me.Controls(0).Size
            End Sub
            Protected Overrides Sub OnControlAdded(ByVal e As System.Windows.Forms.ControlEventArgs)
                If Me.Size <> Me.Controls(0).Size Then Me.Size = Me.Controls(0).Size
            End Sub
        End Class
        Private ReadOnly Property HasTextBoxMouse() As Boolean
            Get
                Return TextBox.Parent.RectangleToScreen(New Rectangle(TextBox.Location, TextBox.Size)).Contains(Cursor.Position)
            End Get
        End Property
        Private ReadOnly Property HasRemoveButtonFromMouse() As Boolean
            Get
                If Not RemoveButtonForm.Visible Then Return False
                Return RemoveButtonForm.DesktopBounds.Contains(Cursor.Position)
            End Get
        End Property
        Private Sub TextBox_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
            If RemoveButtonStateEffective = ControlState.Enabled Then
                Dim Position As Point = New Point( _
                    TextBox.Left + TextBox.Width - RemoveButtonForm.Width, _
                    TextBox.Height / 2 - RemoveButtonForm.Height / 2 + TextBox.Top)
                If Not RemoveButtonForm.Visible Then RemoveButtonForm.Show(TextBox)
                RemoveButtonForm.Location = _
                    TextBox.Parent.PointToScreen(Position)
                TextBox.Capture = True
            End If
        End Sub
        Private Sub TextBox_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            If Not HasRemoveButtonFromMouse AndAlso Not HasTextBoxMouse Then
                TextBox.Capture = False
                RemoveButtonForm.Visible = False
            ElseIf HasRemoveButtonFromMouse Then
                RemoveButtonForm.Opacity = 1
            Else
                RemoveButtonForm.Opacity = FormOpacity
            End If
        End Sub
#End Region
    End Class
End Class


