Imports System.Drawing
Imports System.Windows.Forms

Namespace GUI
    Public Module Misc
        ''' <summary>Vrac� deleg�ta handleru ud�losti <see cref="Control.Paint"/>, kter� kresl� �</summary>
        Public ReadOnly Property Paint�Handler() As PaintEventHandler
            Get
                Return AddressOf Paint�Sub
            End Get
        End Property
        ''' <summary>Automaticky o�et�� kreslen� �</summary>
        ''' <param name="Form">Formul��, p�i jeho� zav�en� kreslen� zru�it</param>
        ''' <param name="Control">Ovl�dac� prvek, na kter� kreslit (pokud vynech�n, pou�ije se <paramref name="Form"/>)</param>
        Public Sub HandlePaint(ByVal Form As Form, Optional ByVal Control As Control = Nothing)
            If Control Is Nothing Then Control = Form
            Form_Control.Add(Form, Control)
            AddHandler Control.Paint, Paint�Handler
            AddHandler Form.FormClosed, AddressOf FormClosed
        End Sub
        ''' <summary>Automaticky o�et�� kreslen� �</summary>
        ''' <param name="Offset">Offset kreslen� od standartn� pozice (tj. prav�ho doln�ho rohu)</param>
        ''' <param name="Form">Formul��, p�i jeho� zav�en� kreslen� zru�it</param>
        ''' <param name="Control">Ovl�dac� prvek, na kter� kreslit (pokud vynech�n, pou�ije se <paramref name="Form"/>)</param>
        Public Sub HandlePaint(ByVal Offset As Size, ByVal Form As Form, Optional ByVal Control As Control = Nothing)
            If Control Is Nothing Then Control = Form
            Form_Control.Add(Form, Control)
            AddHandler Control.Paint, Paint�Handler
            AddHandler Form.FormClosed, AddressOf FormClosed
            Offsets.Add(Control, Offset)
        End Sub
        Private ReadOnly Offsets As New Dictionary(Of Control, Size)
        ''' <summary>Seznam dvojic formul��-ovl�dac� prvek pro odstra�ov�n� handler�</summary>
        Private ReadOnly Form_Control As New Dictionary(Of Form, Control)
        ''' <summary>Reaguje na zav�en� formul��e a odstran� handlery</summary>
        Private Sub FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
            If Form_Control.ContainsKey(sender) Then
                If Offsets.ContainsKey(Form_Control(sender)) Then Offsets.Remove(Form_Control(sender))
                RemoveHandler Form_Control(sender).Paint, Paint�Handler
                RemoveHandler DirectCast(sender, Form).FormClosed, AddressOf FormClosed
                Form_Control.Remove(sender)
            End If
        End Sub
        ''' <summary>Nakresl� �</summary>
        ''' <remarks>Handler ud�losti <see cref="Control.Paint"/></remarks>
        <DebuggerStepThrough()> _
        Private Sub Paint�Sub(ByVal sender As Object, ByVal e As PaintEventArgs)
            Static �Font As Font
            If �Font Is Nothing Then �Font = New Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Point)
            Static �Brush As Brush
            If �Brush Is Nothing Then �Brush = New SolidBrush(Color.LightGray)
            With DirectCast(sender, Control)
                Dim �Size As SizeF = e.Graphics.MeasureString("�", .Font)
                Dim �Rect As New Rectangle(New Point(.ClientSize.Width - �Size.Width, .ClientSize.Height - �Size.Height), �Size.ToSize)
                If Offsets.ContainsKey(sender) Then �Rect.Location += Offsets(sender)
                If e.ClipRectangle.IntersectsWith(�Rect) Then
                    e.Graphics.DrawString("�", �Font, �Brush, �Rect)
                End If
            End With
        End Sub
        ''' <summary>Invokes <see cref="Interaction.MsgBox"/> on specified control</summary>
        ''' <param name="Ctl">Control to invoke <see cref="Interaction.MsgBox"/> in context of</param>
        ''' <param name="Prompt">Message box prompt</param>
        ''' <param name="Buttons">Defines buttons and icons of message box</param>
        ''' <param name="title">Message box title</param>
        ''' <returns>Pressed button</returns>
        Public Function MsgBoxOn(ByVal Ctl As Control, ByVal Prompt As String, Optional ByVal Buttons As MsgBoxStyle = MsgBoxStyle.OkOnly, Optional ByVal title As String = Nothing) As MsgBoxResult
            Dim mDel As New dMsgbox(AddressOf MsgBox)
            Return Ctl.Invoke(mDel, Prompt, Buttons, title)
        End Function
        ''' <summary>Deleg�t na <see cref="MsgBox"/></summary>
        ''' <param name="Prompt">Message box prompt</param>
        ''' <param name="Buttons">Defines buttons and icons of message box</param>
        ''' <param name="title">Message box title</param>
        ''' <returns>Pressed button</returns>
        Private Delegate Function dMsgbox(ByVal Prompt As Object, ByVal Buttons As MsgBoxStyle, ByVal title As Object) As MsgBoxResult
    End Module
End Namespace