Imports System.ComponentModel, System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing

Namespace GUI
    ''' <summary><see cref="ListBox"/>, který umí disablova položku</summary>
    Public Class ListBoxWithDisabledItems
        Inherits ListBox
        ''' <summary>Argument události <see cref="DetectItemDisabled"/></summary>
        Public Class DetectItemDisabledEventArgs
            Inherits EventArgs
            ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Disabled"/></summary>
            Private _Disabled As Boolean
            ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Index"/></summary>
            Private ReadOnly _Index%
            ''' <summary>CTor</summary>
            ''' <param name="Index">Index položky pro kterou je událost vyvolána</param>
            Public Sub New(ByVal Index As Integer)
                _Index = Index
            End Sub
            ''' <summary>Index položk ypro kterou je událost vyvolána</summary>
            Public ReadOnly Property Index%()
                Get
                    Return _Index
                End Get
            End Property
            ''' <summary>Nastavuje jestli je daná položka disadlována</summary>
            Public Property Disabled() As Boolean
                Get
                    Return _Disabled
                End Get
                Set(ByVal value As Boolean)
                    _Disabled = value
                End Set
            End Property
        End Class
        ''' <summary>Vyvolána ve chvíli, kdy je potøeba zjistit jesli je daná položka disablována nebo enablována</summary>
        <Description("Vyvolána ve chvíli, kdy je potøeba zjistit jesli je daná položka disablována nebo enablována")> _
        Public Event DetectItemDisabled As EventHandler(Of DetectItemDisabledEventArgs)
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="DisabledItemColor"/></summary>
        Private _DisabledItemColor As Color = SystemColors.GrayText
        ''' <summary>Barva textu disablované položky</summary>
        <DefaultValue(GetType(Color), "GrayText")> _
        <Description("Barva textu disablované položky")> _
        Public Property DisabledItemColor() As Color
            Get
                Return _DisabledItemColor
            End Get
            Set(ByVal value As Color)
                If value <> DisabledItemColor Then
                    _DisabledItemColor = value
                    Me.Invalidate()
                End If
            End Set
        End Property
        Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
            'For i As Integer = 0 To Me.Items.Count - 1
            '    If Me.GetItemRectangle(i).Contains(e.Location) AndAlso IsDisabled(i) Then
            '        e.
            '        Exit Sub
            '    End If
            'Next
            'MyBase.OnMouseDown(e)
        End Sub
        Private Sub Me_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles Me.DrawItem
            If e.Index < 0 OrElse e.Index >= Me.Items.Count Then Return
            e.DrawBackground()
            Dim brush As SolidBrush
            If IsDisabled(e.Index) Then
                brush = New SolidBrush(DisabledItemColor)
            Else
                brush = New SolidBrush(Me.ForeColor)
            End If
            e.Graphics.DrawString(GetItemText(Me.Items(e.Index)), Me.Font, brush, New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))
            e.DrawFocusRectangle()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()
            'This is super important. If you miss it... you won't be able to Draw the item.
            'If you make it OwnerDrawFixed you won't be able to measure the item.
            Me.DrawMode = DrawMode.OwnerDrawFixed
        End Sub
        Private OldSelectedIndex As Integer = -1
        Private IgnoreOnSelectedIndexChanged As Boolean
        Public Sub DisbledItemsChanged()
            Me.Invalidate()
            DeselectDisabled()
        End Sub
        Private Function DeselectDisabled() As Boolean
            Dim remove As New List(Of Integer)
            For Each i As Integer In Me.SelectedIndices
                If IsDisabled(i) Then remove.Add(i)
            Next
            IgnoreOnSelectedIndexChanged = True
            Try
                For Each i As Integer In remove
                    Me.SelectedIndices.Remove(i)
                Next
                If remove.Count <> 0 AndAlso Me.OldSelectedIndex < Me.Items.Count AndAlso Me.OldSelectedIndex >= 0 AndAlso Not IsDisabled(Me.OldSelectedIndex) Then _
                    Me.SelectedIndex = Me.OldSelectedIndex
                Return remove.Count <> 0
            Finally
                IgnoreOnSelectedIndexChanged = False
            End Try
        End Function
        Protected Overrides Sub OnSelectedIndexChanged(ByVal e As System.EventArgs)
            If IgnoreOnSelectedIndexChanged Then Exit Sub
            If Not DeselectDisabled() Then
                OldSelectedIndex = Me.SelectedIndex
                MyBase.OnSelectedIndexChanged(e)
            End If
        End Sub
        Private Function IsDisabled(ByVal i%) As Integer
            Dim ea As New DetectItemDisabledEventArgs(i)
            RaiseEvent DetectItemDisabled(Me, ea)
            Return ea.Disabled
        End Function

        Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
            MyBase.OnKeyDown(e)
            If e.Handled Then Exit Sub
            Select Case e.KeyData
                Case Keys.Up, Keys.Up Or Keys.Shift
                    SelectItems(0, e.Shift, True)
                Case Keys.Down, Keys.Down Or Keys.Shift
                    SelectItems(Me.Items.Count - 1, e.Shift, True)
                Case Keys.Home, Keys.Home Or Keys.Up
                    SelectItems(0, e.Shift, False)
                Case Keys.End, Keys.End Or Keys.Shift
                    SelectItems(Me.Items.Count - 1, e.Shift, False)
            End Select
            e.Handled = True
            e.SuppressKeyPress = True
        End Sub

        Public Sub SelectItems(ByVal EndI%, ByVal Add As Boolean, ByVal First As Boolean)
            If Me.SelectionMode = Windows.Forms.SelectionMode.None Then Exit Sub
            If Me.Items.Count = 0 Then Exit Sub
            Add = Add AndAlso Me.SelectionMode >= Windows.Forms.SelectionMode.MultiSimple

            Dim Direction As SByte = IIf(EndI > Me.SelectedIndex, +1, -1)
            Dim StartI% = Me.SelectedIndex + Direction
            If StartI < 0 Then StartI = 0
            If EndI < 0 Then EndI = 0
            If StartI >= Me.Items.Count Then StartI = Me.Items.Count - 1
            If EndI >= Me.Items.Count Then EndI = Me.Items.Count - 1
            If (Direction > 0 AndAlso EndI < StartI) OrElse (Direction < 0 AndAlso EndI > StartI) Then _
                Exit Sub

            If Add And Not First Then
                For i As Integer = StartI To EndI Step Direction
                    If Not IsDisabled(i) Then Me.SelectedIndex = i
                Next
            ElseIf Add Then
                For i As Integer = StartI To EndI Step Direction
                    If Not IsDisabled(i) Then Me.SelectedIndex = i : Exit For
                Next
            Else
                If Not First Then
                    Dim tmp As Integer
                    tmp = StartI
                    StartI = EndI
                    EndI = tmp
                    Direction *= -1
                End If
                For i As Integer = StartI To EndI Step Direction
                    If Not IsDisabled(i) Then
                        Me.SelectedIndices.Clear()
                        Me.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
        End Sub


        'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        '    Dim iHw, iLW As Short
        '    Dim lCurind As Integer

        '    Select Case m.Msg
        '        Case WM_COMMAND
        '            If m.LParam = Me.Handle Then
        '                LongInt2Int(m.WParam, iHw, iLW)
        '                Select Case (iHw)
        '                    Case LBN_SELCHANGE
        '                    Case LBN_SELCANCEL
        '                        lCurind = SendMessage(m.LParam, LB_GETCURSEL, 0, 0)
        '                        Debug.Print(" lbnselcancel for:" & Hex(lCurind))
        '                    Case LBN_DBLCLK
        '                        'Case LBN_KILLFOCUS
        '                        'Case LBN_SETFOCUS
        '                End Select
        '            End If
        '        Case WM_LBUTTONDOWN, WM_LBUTTONDBLCLK
        '            LongInt2Int(m.LParam, iHw, iLW)
        '            System.Diagnostics.Debug.Write(" Mouse down at(" & iHw & "," & iLW & ")")
        '            lCurind = SendMessage(m.HWnd, LB_ITEMFROMPOINT, 0, m.LParam)
        '            Debug.Print("Index of btn down:" & Hex(lCurind))
        '            If (lCurind Mod 3) = 0 Then
        '                m.Result = 1
        '                Exit Sub
        '            End If
        '        Case WM_KEYDOWN
        '            LongInt2Int(m.WParam, iHw, iLW)
        '            Select Case (iLW)
        '                Case System.Windows.Forms.Keys.Down
        '                    If Me.SelectedIndex < Me.Items.Count - 1 AndAlso IsDisabled(Me.SelectedIndex + 1) Then
        '                        m.Result = 0
        '                    End If
        '                Case System.Windows.Forms.Keys.Up
        '                    If Me.SelectedIndex > 0 AndAlso IsDisabled(Me.SelectedIndex - 1) Then
        '                        m.Result = 0
        '                    End If
        '                Case System.Windows.Forms.Keys.PageUp
        '                Case System.Windows.Forms.Keys.PageDown
        '            End Select
        '    End Select
        '    MyBase.WndProc(m)
        'End Sub
    End Class
End Namespace