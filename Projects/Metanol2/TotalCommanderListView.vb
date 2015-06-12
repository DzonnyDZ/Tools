Imports Tools.CollectionsT.SpecializedT
Imports System.ComponentModel

''' <summary><see cref="ListView"/> with Total-Commander-like behaviour</summary>
<ToolboxBitmap(GetType(ListView))> _
Public Class TotalCommanderListView
    Inherits ListView
#Region "Selection change events"
    ''' <summary>Raised before changes in the <see cref="SelectedItems"/> collection are dome.</summary>
    ''' <remarks>The <see cref="BeforeSelectionChange"/> and <see cref="AfterSelectionChange"/> events are also raised in pair.</remarks>
    ''' <seealso cref="AfterSelectionChange"/>
    <ComponentModelT.KnownCategory(ComponentModelT.KnownCategoryAttribute.KnownCategories.Action)> _
    Public Event BeforeSelectionChange As EventHandler
    ''' <summary>Raised after changes in the <see cref="SelectedItems"/> collection are done</summary>
    ''' <remarks>The <see cref="BeforeSelectionChange"/> and <see cref="AfterSelectionChange"/> events are also raised in pair.</remarks>
    ''' <seealso cref="BeforeSelectionChange"/>
    <ComponentModelT.KnownCategory(ComponentModelT.KnownCategoryAttribute.KnownCategories.Action)> _
    Public Event AfterSelectionChange As EventHandler
    ''' <summary>Raises the <see cref="BeforeSelectionChange"/> event</summary>
    ''' <param name="e">Event arguments</param>
    ''' <seealso cref="OnAfterSelectionChange"/>
    Protected Overridable Sub OnBeforeSelectionChange(ByVal e As EventArgs)
        RaiseEvent BeforeSelectionChange(Me, e)
    End Sub
    ''' <summary>Raises the <see cref="AfterSelectionChange"/> event</summary>
    ''' <param name="e">Event arguments</param>
    ''' <seealso cref="OnBeforeSelectionChange"/>
    Protected Overridable Sub OnAfterSelectionChange(ByVal e As EventArgs)
        RaiseEvent AfterSelectionChange(Me, e)
    End Sub
#End Region
    ''' <summary>Contains value of the <see cref="TCBehaviour"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private _TCBehaviour As Boolean = True
    ''' <summary>Gets or sets value indicating if this instance bihaves like list view in Total Commander</summary>
    <DefaultValue(True)> _
    <ComponentModelT.KnownCategory(ComponentModelT.KnownCategoryAttribute.KnownCategories.Behavior)> _
    Public Property TCBehaviour() As Boolean
        Get
            Return _TCBehaviour
        End Get
        Set(ByVal value As Boolean)
            _TCBehaviour = value
        End Set
    End Property
    ''' <summary>Occurs when a key is pressed while the control has focus.</summary>
    <ComponentModelT.KnownCategory(ComponentModelT.KnownCategoryAttribute.KnownCategories.Key)> _
    Public Shadows Event KeyDown As KeyEventHandler
    ''' <summary>Occurs when a key is pressed while the control has focus.</summary>
    <ComponentModelT.KnownCategory(ComponentModelT.KnownCategoryAttribute.KnownCategories.Key)> _
    Public Shadows Event KeyPress As KeyPressEventHandler

    ''' <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
    ''' <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        RaiseEvent KeyDown(Me, e)
        If TCBehaviour AndAlso Not e.Handled Then
            Select Case e.KeyData
                Case Keys.A Or Keys.Control
                    OnBeforeSelectionChange(New EventArgs)
                    Try : For Each item In Me.Items.AsTypeSafe : item.Selected = True : Next
                    Finally : OnAfterSelectionChange(New EventArgs) : End Try
                    e.Handled = True : e.SuppressKeyPress = True
                Case Keys.Space
                    If Me.FocusedItem IsNot Nothing Then
                        OnBeforeSelectionChange(New EventArgs)
                        Try : Me.FocusedItem.Selected = Not Me.FocusedItem.Selected
                        Finally : OnAfterSelectionChange(New EventArgs) : End Try
                        Me.FocusedItem.EnsureVisible()
                    End If
                    e.Handled = True : e.SuppressKeyPress = True
                Case Keys.Insert
                    If Me.FocusedItem IsNot Nothing Then
                        OnBeforeSelectionChange(New EventArgs)
                        Try : Me.FocusedItem.Selected = Not Me.FocusedItem.Selected
                            If Me.FocusedItem.Index < Me.Items.Count - 1 Then Me.FocusedItem = Me.Items(Me.FocusedItem.Index + 1)
                        Finally : OnAfterSelectionChange(New EventArgs) : End Try
                        Me.FocusedItem.EnsureVisible()
                    End If
                    e.Handled = True : e.SuppressKeyPress = True
                Case Keys.Insert Or Keys.Shift
                    If Me.FocusedItem IsNot Nothing Then
                        OnBeforeSelectionChange(New EventArgs)
                        Try : Me.FocusedItem.Selected = Not Me.FocusedItem.Selected
                            If Me.FocusedItem.Index > 0 Then Me.FocusedItem = Me.Items(Me.FocusedItem.Index - 1)
                        Finally : OnAfterSelectionChange(New EventArgs) : End Try
                        Me.FocusedItem.EnsureVisible()
                    End If
                    e.Handled = True : e.SuppressKeyPress = True
                Case Keys.Left
                    If Me.FocusedItem IsNot Nothing AndAlso Me.FocusedItem.Index > 0 Then
                        Me.FocusedItem = Me.Items(Me.FocusedItem.Index - 1)
                        Me.FocusedItem.EnsureVisible()
                    ElseIf Me.FocusedItem Is Nothing AndAlso Me.Items.Count > 0 Then
                        Me.FocusedItem = Me.Items(0)
                        Me.FocusedItem.EnsureVisible()
                    End If
                    e.Handled = True : e.SuppressKeyPress = True
                Case Keys.Right
                    If Me.FocusedItem IsNot Nothing AndAlso Me.FocusedItem.Index < Me.Items.Count - 1 Then
                        Me.FocusedItem = Me.Items(Me.FocusedItem.Index + 1)
                        Me.FocusedItem.EnsureVisible()
                    ElseIf Me.FocusedItem Is Nothing AndAlso Me.Items.Count > 0 Then
                        Me.FocusedItem = Me.Items(0)
                        Me.FocusedItem.EnsureVisible()
                    End If
                    e.Handled = True : e.SuppressKeyPress = True
                Case Keys.Up, Keys.Down
                    If Me.FocusedItem IsNot Nothing Then
                        Dim Direction = If(e.KeyData = Keys.Up, SearchDirectionHint.Up, SearchDirectionHint.Down)
                        Dim nearest = Me.FocusedItem.FindNearestItem(Direction)
                        If nearest IsNot Nothing Then
                            Me.FocusedItem = nearest
                            Me.FocusedItem.EnsureVisible()
                        End If
                    ElseIf Me.FocusedItem Is Nothing AndAlso Me.Items.Count > 0 Then
                        Me.FocusedItem = Me.Items(0)
                        Me.FocusedItem.EnsureVisible()
                    End If
                    e.Handled = True : e.SuppressKeyPress = True
                Case Keys.Home, Keys.End
                    If Me.Items.Count <> 0 Then Me.FocusedItem = Me.Items(If(e.KeyData = Keys.Home, 0, Me.Items.Count - 1))
                    e.Handled = True : e.SuppressKeyPress = True
                Case Keys.PageUp, Keys.PageDown
                    If Me.FocusedItem IsNot Nothing Then
                        Dim Direction = If(e.KeyData = Keys.PageUp, SearchDirectionHint.Up, SearchDirectionHint.Down)
                        Dim current = Me.FocusedItem
                        While Math.Abs(current.GetBounds(ItemBoundsPortion.Entire).Top - Me.FocusedItem.GetBounds(ItemBoundsPortion.Entire).Top) < Me.ClientSize.Height
                            Dim current2 = current.FindNearestItem(Direction)
                            If current2 Is Nothing OrElse current Is current2 Then Exit While
                            current = current2
                        End While
                        If current IsNot Me.FocusedItem Then
                            Me.FocusedItem = current
                            Me.FocusedItem.EnsureVisible()
                        End If
                    End If
                    e.Handled = True : e.SuppressKeyPress = True
                Case Else
                    MyBase.OnKeyDown(e)
            End Select
        Else
            MyBase.OnKeyDown(e)
        End If
    End Sub

    ''' <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
    ''' <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
    Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        RaiseEvent KeyPress(Me, e)
        If TCBehaviour AndAlso Not e.Handled Then
            Static Buffer As String = ""
            Static LastKeyPress As Date = Date.MinValue
            Const pause% = 500
            Select Case e.KeyChar
                Case "*"c
                    OnBeforeSelectionChange(New EventArgs)
                    Try
                        For Each item As MetadataItem In Me.Items
                            item.Selected = Not item.Selected
                        Next
                    Finally : OnAfterSelectionChange(New EventArgs) : End Try
                    e.Handled = True
                Case Else
                    If (Now - LastKeyPress).TotalMilliseconds > pause Then Buffer = e.KeyChar Else Buffer &= e.KeyChar
                    Dim startindex As Integer
                    If Me.FocusedItem IsNot Nothing Then
                        startindex = Me.FocusedItem.Index
                        If (Now - LastKeyPress).TotalMilliseconds > pause Then
                            startindex += 1
                            If startindex >= Me.Items.Count Then startindex = 0
                        End If
                    Else
                        startindex = 0
                    End If
                    Dim item = Me.FindItemWithText(Buffer, False, startindex, True)
                    If item IsNot Nothing Then Me.FocusedItem = item : Me.FocusedItem.EnsureVisible()
                    e.Handled = True
                    LastKeyPress = Now
            End Select
        Else
            MyBase.OnKeyPress(e)
        End If
    End Sub
    ''' <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
    ''' <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If (e.Button And System.Windows.Forms.MouseButtons.Left) = System.Windows.Forms.MouseButtons.Left OrElse (e.Button And System.Windows.Forms.MouseButtons.Right) = System.Windows.Forms.MouseButtons.Right Then
            OnBeforeSelectionChange(e)
        End If
        MyBase.OnMouseDown(e)
    End Sub
    ''' <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
    ''' <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        If (e.Button And System.Windows.Forms.MouseButtons.Left) = System.Windows.Forms.MouseButtons.Left OrElse (e.Button And System.Windows.Forms.MouseButtons.Right) = System.Windows.Forms.MouseButtons.Right Then
            OnAfterSelectionChange(e)
        End If
        MyBase.OnMouseUp(e)
    End Sub
End Class
